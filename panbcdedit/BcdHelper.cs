using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace panbcdedit
{
    /// <summary>
    /// 封装命令执行结果。
    /// </summary>
    public class BcdResult
    {
        public int ExitCode { get; set; }
        public string Output { get; set; }
        public string Error { get; set; }
    }

    /// <summary>
    /// 封装 BCD 相关命令行工具（bcdedit.exe、bcdboot.exe）的调用。
    /// 所有命令通过 System.Diagnostics.Process 执行，重定向标准输出/错误，并设置超时。
    /// </summary>
    public static class BcdHelper
    {
        /// <summary>
        /// bcdedit.exe 的完整路径。
        /// </summary>
        public static readonly string BcdEditPath = Path.Combine(Environment.SystemDirectory, "bcdedit.exe");

        /// <summary>
        /// bcdboot.exe 的完整路径。
        /// </summary>
        public static readonly string BcdBootPath = Path.Combine(Environment.SystemDirectory, "bcdboot.exe");

        /// <summary>
        /// 命令执行超时时间（毫秒）。
        /// </summary>
        private const int CommandTimeoutMs = 30000;

        /// <summary>
        /// 执行 bcdedit.exe 命令。
        /// </summary>
        /// <param name="arguments">要传递给 bcdedit.exe 的参数。</param>
        /// <returns>包含退出代码、标准输出和标准错误的结果。</returns>
        public static BcdResult ExecuteBcdEdit(string arguments)
        {
            return ExecuteCommand(BcdEditPath, arguments);
        }

        /// <summary>
        /// 执行 bcdboot.exe 命令。
        /// </summary>
        /// <param name="arguments">要传递给 bcdboot.exe 的参数。</param>
        /// <returns>包含退出代码、标准输出和标准错误的结果。</returns>
        public static BcdResult ExecuteBcdBoot(string arguments)
        {
            return ExecuteCommand(BcdBootPath, arguments);
        }

        /// <summary>
        /// 异步执行 bcdedit.exe 命令。
        /// </summary>
        /// <param name="arguments">要传递给 bcdedit.exe 的参数。</param>
        /// <returns>包含退出代码、标准输出和标准错误的结果。</returns>
        public static Task<BcdResult> ExecuteBcdEditAsync(string arguments)
        {
            return Task.Run(() => ExecuteBcdEdit(arguments));
        }

        /// <summary>
        /// 异步执行 bcdboot.exe 命令。
        /// </summary>
        /// <param name="arguments">要传递给 bcdboot.exe 的参数。</param>
        /// <returns>包含退出代码、标准输出和标准错误的结果。</returns>
        public static Task<BcdResult> ExecuteBcdBootAsync(string arguments)
        {
            return Task.Run(() => ExecuteBcdBoot(arguments));
        }

        /// <summary>
        /// 执行指定的命令行程序，重定向标准输出与错误，并支持超时取消。
        /// </summary>
        /// <param name="fileName">可执行文件路径。</param>
        /// <param name="arguments">参数。</param>
        /// <returns>包含退出代码、标准输出和标准错误的结果。</returns>
        private static BcdResult ExecuteCommand(string fileName, string arguments)
        {
            StringBuilder outputBuilder = new StringBuilder();
            StringBuilder errorBuilder = new StringBuilder();
            int exitCode = -1;

            using (Process process = new Process())
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding = Encoding.Default,
                    StandardErrorEncoding = Encoding.Default,
                    WorkingDirectory = Environment.SystemDirectory
                };

                // 同步读取输出与错误，避免缓冲区满导致死锁
                process.OutputDataReceived += (sender, e) =>
                {
                    if (e.Data != null)
                    {
                        outputBuilder.AppendLine(e.Data);
                    }
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (e.Data != null)
                    {
                        errorBuilder.AppendLine(e.Data);
                    }
                };

                try
                {
                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    // 使用超时等待进程结束
                    bool exited = process.WaitForExit(CommandTimeoutMs);
                    if (!exited)
                    {
                        try
                        {
                            process.Kill();
                        }
                        catch
                        {
                            // 忽略终止失败
                        }

                        return new BcdResult
                        {
                            ExitCode = -1,
                            Output = string.Empty,
                            Error = string.Format("命令执行超时（超过 {0} 秒）：{1} {2}", CommandTimeoutMs / 1000, fileName, arguments)
                        };
                    }

                    // 额外等待异步事件处理完成
                    process.WaitForExit();
                    exitCode = process.ExitCode;
                }
                catch (Exception ex)
                {
                    return new BcdResult
                    {
                        ExitCode = -1,
                        Output = string.Empty,
                        Error = string.Format("启动命令时发生异常：{0}", ex.Message)
                    };
                }
            }

            return new BcdResult
            {
                ExitCode = exitCode,
                Output = outputBuilder.ToString().TrimEnd(),
                Error = errorBuilder.ToString().TrimEnd()
            };
        }
    }
}
