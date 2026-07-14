using System;
using System.Security.Principal;
using System.Windows.Forms;

namespace panbcdedit
{
    /// <summary>
    /// 程序入口类，负责管理员权限检测并启动主窗体。
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 检测当前进程是否以管理员身份运行
            if (!IsRunningAsAdministrator())
            {
                MessageBox.Show(
                    "本工具需要管理员权限才能操作 BCD 存储。\n请右键选择“以管理员身份运行”后重试。",
                    "权限不足",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            Application.Run(new MainForm());
        }

        /// <summary>
        /// 判断当前进程是否拥有管理员权限。
        /// </summary>
        private static bool IsRunningAsAdministrator()
        {
            try
            {
                using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
                {
                    WindowsPrincipal principal = new WindowsPrincipal(identity);
                    return principal.IsInRole(WindowsBuiltInRole.Administrator);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("检测管理员权限时发生异常：{0}", ex.Message),
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
