using System;
using System.Diagnostics;
using System.IO;

namespace panbcdedit
{
    /// <summary>
    /// ESP（EFI System Partition）自动挂载/卸载辅助类。
    /// 使用 mountvol 命令挂载/卸载，比解析 diskpart 输出更可靠。
    /// </summary>
    internal static class EspHelper
    {
        public class EspInfo
        {
            /// <summary>盘符，如 "S:"</summary>
            public string DriveLetter { get; set; }
            /// <summary>是否在打开本工具之前已经挂载</summary>
            public bool WasPreMounted { get; set; }
        }

        /// <summary>
        /// 尝试查找 ESP，若没有盘符则自动挂载到 S:。
        /// 返回 null 表示未找到 ESP（可能是 BIOS 启动，无 ESP 分区）。
        /// </summary>
        public static EspInfo FindAndMountEsp()
        {
            // 1. 先检查已挂载且盘符存在的 FAT32 小分区（极大概率是 ESP）
            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                if (di.DriveType != DriveType.Fixed || !di.IsReady) continue;
                try
                {
                    if (di.DriveFormat.Equals("FAT32", StringComparison.OrdinalIgnoreCase)
                        && di.TotalSize >= 50 * 1024 * 1024
                        && di.TotalSize <= 2L * 1024 * 1024 * 1024)
                    {
                        return new EspInfo
                        {
                            DriveLetter = di.Name.Substring(0, 2).ToUpperInvariant(),
                            WasPreMounted = true
                        };
                    }
                }
                catch { }
            }

            // 2. 用 mountvol /s 挂载 ESP 到 S:（这是 Windows 原生支持的 ESP 挂载方式）
            string espDrive = "S:";
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = "mountvol";
                proc.StartInfo.Arguments = espDrive + " /s";
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.Start();
                string output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit(5000);

                // mountvol 返回 0 表示成功，此时 ESP 已挂载到指定盘符
                if (proc.ExitCode == 0)
                {
                    // 等待一下让系统刷新盘符
                    System.Threading.Thread.Sleep(200);
                    if (Directory.Exists(espDrive + @"\"))
                    {
                        return new EspInfo
                        {
                            DriveLetter = espDrive,
                            WasPreMounted = false
                        };
                    }
                }

                // mountvol 失败时，试试直接写 mountvol /s 不带盘符（某些系统需要）
                // 先尝试 S: 不可用时自动找空闲盘符，先简单处理：再试无盘符的 mountvol
            }
            catch
            {
                // mountvol 不可用，继续尝试其他方法
            }

            // 3. 最后尝试：用 diskpart 搜索并挂载（兜底方案）
            return TryDiskpartMount();
        }

        /// <summary>
        /// 如果 ESP 是本工具挂载的，则卸载盘符。
        /// </summary>
        public static void UnmountIfOurs(EspInfo info)
        {
            if (info == null || info.WasPreMounted) return;
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = "mountvol";
                proc.StartInfo.Arguments = info.DriveLetter + " /d";
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
                proc.WaitForExit(3000);
            }
            catch { }
        }

        /// <summary>
        /// 兜底方法：用 diskpart 查找并挂载 ESP。
        /// </summary>
        private static EspInfo TryDiskpartMount()
        {
            string scriptPath = Path.Combine(Path.GetTempPath(), "pan_esp_dp.txt");
            try
            {
                // diskpart 脚本：枚举所有磁盘和分区，找到 ESP 后挂载到 S:
                string script =
                    "list disk\r\n" +
                    "select disk 0\r\n" +
                    "list partition\r\n" +
                    "select partition 1\r\n" +
                    "detail partition\r\n" +
                    "exit";

                File.WriteAllText(scriptPath, script);
                string output = RunDiskPart(scriptPath);

                // 检查是否为 ESP（不仰赖中文关键词，检查 GUID）
                if (output.Contains("c12a7328") || output.Contains("C12A7328"))
                {
                    // 已在 select partition 1，直接 assign
                    string assignScript =
                        "select disk 0\r\n" +
                        "select partition 1\r\n" +
                        "assign letter=S\r\n" +
                        "exit";
                    File.WriteAllText(scriptPath, assignScript);
                    RunDiskPart(scriptPath);

                    if (Directory.Exists("S:\\"))
                    {
                        return new EspInfo
                        {
                            DriveLetter = "S:",
                            WasPreMounted = false
                        };
                    }
                }

                // 如果磁盘 0 分区 1 不是 ESP，搜索其他磁盘和分区
                return SearchAndMountEspViaDiskpart();
            }
            catch
            {
                return null;
            }
            finally
            {
                try { File.Delete(scriptPath); } catch { }
            }
        }

        /// <summary>
        /// 遍历所有磁盘和分区寻找 ESP。
        /// </summary>
        private static EspInfo SearchAndMountEspViaDiskpart()
        {
            string scriptPath = Path.Combine(Path.GetTempPath(), "pan_esp_dp2.txt");
            try
            {
                // 先获取磁盘数量
                string listDisk = "list disk\r\nexit";
                File.WriteAllText(scriptPath, listDisk);
                string diskOutput = RunDiskPart(scriptPath);

                // 最多支持 8 个磁盘
                for (int disk = 0; disk < 8; disk++)
                {
                    if (!diskOutput.Contains("磁盘 " + disk) && !diskOutput.Contains("Disk " + disk))
                        continue;

                    // 列出该磁盘的分区
                    string listPart = "select disk " + disk + "\r\nlist partition\r\nexit";
                    File.WriteAllText(scriptPath, listPart);
                    string partOutput = RunDiskPart(scriptPath);

                    // 最多支持 32 个分区
                    for (int part = 1; part <= 32; part++)
                    {
                        string detail = "select disk " + disk + "\r\nselect partition " + part + "\r\ndetail partition\r\nexit";
                        File.WriteAllText(scriptPath, detail);
                        string detailOutput = RunDiskPart(scriptPath);

                        if (detailOutput.Contains("c12a7328") || detailOutput.Contains("C12A7328") ||
                            detailOutput.Contains("EFI") || detailOutput.Contains("efi"))
                        {
                            // 挂载到 S:
                            string assign = "select disk " + disk + "\r\nselect partition " + part + "\r\nassign letter=S\r\nexit";
                            File.WriteAllText(scriptPath, assign);
                            RunDiskPart(scriptPath);

                            System.Threading.Thread.Sleep(200);
                            if (Directory.Exists("S:\\"))
                            {
                                return new EspInfo
                                {
                                    DriveLetter = "S:",
                                    WasPreMounted = false
                                };
                            }
                        }
                    }
                }
            }
            catch { }
            finally
            {
                try { File.Delete(scriptPath); } catch { }
            }

            return null;
        }

        private static string RunDiskPart(string scriptPath)
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = Path.Combine(Environment.SystemDirectory, "diskpart.exe");
                proc.StartInfo.Arguments = "/s \"" + scriptPath + "\"";
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.Start();
                string output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit(10000);
                return output;
            }
            catch
            {
                return "";
            }
        }
    }
}
