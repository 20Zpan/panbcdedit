using System;
using System.Drawing;
using System.Windows.Forms;

namespace panbcdedit
{
    /// <summary>
    /// 新手指南窗口，介绍 panbcdedit 的各项功能和用法。
    /// </summary>
    public class GuideForm : Form
    {
        public GuideForm()
        {
            InitializeGuideForm();
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private void InitializeGuideForm()
        {
            this.Text = "panbcdedit 新手指南";
            this.Size = new Size(820, 640);
            this.MinimumSize = new Size(640, 480);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.KeyPreview = true;
            this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) Close(); };

            var richTextBox = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = new Font("Microsoft YaHei", 10.5F),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Padding = new Padding(16, 16, 16, 16),
                Text = GetGuideText(),
                DetectUrls = true
            };

            var btnClose = new Button
            {
                Text = "关闭 (Esc)",
                Size = new Size(100, 32),
                Anchor = AnchorStyles.None
            };
            btnClose.Click += (s, e) => Close();

            var bottomPanel = new Panel
            {
                Height = 48,
                Dock = DockStyle.Bottom
            };
            bottomPanel.Controls.Add(btnClose);
            btnClose.Location = new Point((bottomPanel.Width - btnClose.Width) / 2, 8);
            bottomPanel.Resize += (s, e) =>
            {
                btnClose.Location = new Point((bottomPanel.Width - btnClose.Width) / 2, 8);
            };

            this.Controls.Add(richTextBox);
            this.Controls.Add(bottomPanel);
        }

        private string GetGuideText()
        {
            return @"panbcdedit 新手指南

═════════════════════════════════════════════════

一、软件简介

panbcdedit 是一款 Windows 启动配置数据（BCD）的图形化管理工具。
BCD（Boot Configuration Data）是 Windows Vista 及之后系统用于替代
boot.ini 的启动配置数据库。通过本工具，您无需记忆复杂的 bcdedit 命令
行参数，即可直观地查看、修改和管理所有 Windows 启动项。


二、⚠ 安全须知

修改 BCD 配置可能直接导致系统无法启动，操作务必谨慎！
操作前强烈建议通过“常见启动项设置 → 导出 BCD”备份当前配置。


三、界面总览

┌──────────────────────────────────┐
│ 菜单：文件（导出、退出）帮助（本指南、关于）      │
├──────────────────────────────────┤
│ [启动项总览] [启动项管理] [常见启动项设置]              │
│ [高级开关] [引导修复]                                                │
├──────────────────────────────────┤
│ 日志窗口（所有操作记录）                                        │
└──────────────────────────────────┘


四、选项卡 1：启动项总览

功能：查看当前系统所有启动项的完整 BCD 配置信息。
数据来源：执行 bcdedit /enum all /v。
操作方法：
  点击“刷新”按钮重新获取最新配置。
  适合排查启动问题时核对各项参数，也可辅助定位故障启动项。


五、选项卡 2：启动项管理

功能：集中管理所有启动项，支持查看、复制、删除和新建。

── 启动项表格 ──
  列出所有启动项的标识符（GUID）、名称和类型。
  勾选“显示固件/系统启动项”可查看隐藏的系统级条目（如固件
  启动管理器、内存诊断等）。默认不显示以保持界面清爽。

── 复制条目 ──
  在表格中选中一个条目，点击“复制”，即可创建一个配置完全相同的
  副本。之后可在常见启动项设置中将副本设为默认。

── 删除条目 ──
  在表格中选中一个条目，点击“删除”，即可删除该启动项。
  注意：当前默认启动项不可直接删除，请先更改默认项后再操作。

── 新建条目 ──
  名称：新启动项在启动菜单中显示的名称。
  类型：支持五种——
    • Windows Boot Loader    （标准 Windows 启动加载器）
    • Windows Boot Manager   （启动管理器，通常一个即可）
    • Legacy OS loader       （兼容旧版 ntldr 引导的系统）
    • Ramdisk Options        （RAM 磁盘启动，如 WinPE）
    • Linux/UEFI Boot Loader（Linux/UEFI 第三方引导，如 Ubuntu）
  设备：可选，指定启动分区（如 partition=C:）。点击“浏览”选择。

── 高级模式创建 ──
  点击“高级模式创建”按钮打开子窗口，可精细配置启动项的完整参数。
  子窗口根据所选的条目类型动态显示对应的参数输入框：

  • Windows Boot Loader：
      device         引导加载器所在分区
      osdevice       Windows 所在分区
      path           引导文件路径（UEFI 自动设为 winload.efi）
      systemroot     Windows 目录
      locale         启动菜单语言区域（从下拉框选择）

  • Windows Boot Manager：
      device         设备分区

  • Legacy OS loader (ntldr)：
      device         设备分区

  • Ramdisk Options：
      device         设备分区
      ramdisksdidevice  SDI 文件分区
      ramdisksdipath    SDI 镜像路径（默认 \boot\boot.sdi）

  • Linux/UEFI Boot Loader：
      device         EFI 系统分区（ESP，自动挂载，不可修改）
      path           自动指向内置 efiloader.efi（用户无需关心）
      目标引导文件   用户指定要启动的 *.efi 文件（如 \EFI\refind\refind_x64.efi）
      原理：通过 efiloader.efi 作为跳板，绕过 Windows Boot Manager
            的签名限制，自动加载用户指定的目标引导文件。
      提示：第一次创建时会自动部署 efiloader.efi 到 ESP 的 \EFI\pan\ 目录
      注意：内置 efiloader.efi 仅支持 amd64 架构，ia32/arm64 需自行替换对应架构的 efiloader.efi

  配置完成后点击“创建”，工具会自动执行 bcdedit /create 创建条目，
  然后逐条设置所有参数，最后将新条目添加到启动菜单末尾。


六、选项卡 3：常见启动项设置

── 默认启动项 ──
  从下拉框选择一个条目，点击“应用”将其设为开机时默认启动的系统。
  对应命令：bcdedit /default {guid}

── 超时时间 ──
  设置启动选择菜单显示的秒数。
  0 = 不显示菜单，直接启动默认系统。
  其他值 = 显示菜单并等待用户选择（取值范围 0-999）。

── 显示顺序 ──
  展示当前启动项在启动菜单中的排列顺序。
  选中某项后，点击 ↑ 上移 或 ↓ 下移 调整顺序，然后点击“应用顺序”。
  对应命令：bcdedit /displayorder {guid1} {guid2} ...

── 强制显示菜单 ──
  勾选后设置 bootmenupolicy 为 Legacy，强制显示传统启动菜单。
  对于需要始终看到启动菜单的用户非常有用。
  注意：此功能仅支持 BIOS/Legacy 启动的系统。如果您的系统为
  UEFI 启动（绝大多数较新电脑），该选项会自动灰掉不可用。

── 导出 BCD ──
  将当前系统完整的 BCD 配置导出为 .bcd 文件。
  强烈建议在任何修改前执行此操作以创建恢复备份。
  对应命令：bcdedit /export <路径>

── 导入 BCD ──
  从 .bcd 文件恢复 BCD 配置。
  ⚠ 极其危险的操作！覆盖当前所有启动配置。
  导入前会自动创建备份。需在弹窗中手动输入“确认导入”方可继续。
  对应命令：bcdedit /import <路径> /clean


七、选项卡 4：高级开关

功能：对选定的单个启动项进行精细化开关设置。

操作步骤：
  1. 在“目标条目”下拉框中选择一个启动项。
  2. 系统自动读取该条目当前各项的 BCD 值并勾选已有配置。
  3. 根据需要勾选/取消需要修改的开关，未修改的项不会执行。
  4. 下方预览区显示即将执行的 bcdedit 命令。
  5. 确认无误后点击“应用高级开关”。

各开关说明：

  ┌─────────────────────────────────────────────────┐
  │ 分组：调试与签名                                                                                         │
  ├─────────────────────────────────────────────────┤
  │ testsigning    启用测试签名模式，允许加载未签名的驱动                              │
  │ debug          启用内核调试（需连接调试器）                                                │
  │ bootdebug      启用启动阶段调试（调试早期启动代码）                               │
  ├─────────────────────────────────────────────────┤
  │ 分组：安全与驱动                                                                                         │
  ├─────────────────────────────────────────────────┤
  │ nointegritychecks  禁用驱动签名完整性检查                                                │
  │ disableelamdrivers 禁用 ELAM（早期反恶意软件）驱动                              │
  ├─────────────────────────────────────────────────┤
  │ 分组：内存与显示                                                                                         │
  ├─────────────────────────────────────────────────┤
  │ pae  物理地址扩展（强制启用 PAE，32位系统支持大内存）                          │
  │ nx   数据执行保护 DEP（防止数据区代码被执行）                                         │
  │ novesa  禁用 VESA 显示模式（排查显示异常时使用）                                   │
  ├─────────────────────────────────────────────────┤
  │ Hyper-V 虚拟机                                                                                            │
  ├─────────────────────────────────────────────────┤
  │ hypervisorlaunchtype  控制 Hyper-V 虚拟机监控程序                                 │
  │   Auto = 自动启动   |   Off = 关闭   |   未设置                                                │
  └─────────────────────────────────────────────────┘


八、选项卡 5：引导修复

功能：通过 bcdboot 命令修复或重建 Windows 引导。

操作步骤：
  1. Windows 目录：选择 Windows 安装目录（通常是 C:\Windows）。
  2. 语言：选择启动菜单显示语言（默认“zh-CN”即可）。
  3. 固件类型：
       自动 = 让 bcdboot 自动检测
       UEFI = 强制生成 UEFI 引导文件
       BIOS = 强制生成传统 BIOS 引导文件
  4. 保留已有启动项：勾选 /addlast，追加新引导项而不替换已有项。
     注意：不勾选时 bcdboot 会清空 BCD 并重建，仅保留当前修复的启动项。
  5. 点击“执行修复引导”。

典型场景：
  • 更换硬盘/克隆系统后引导丢失
  • 升级 BIOS → UEFI 后需要重建引导
  • Windows 更新后启动项损坏


九、底部日志窗口

显示所有 bcdedit / bcdboot 命令的执行记录，包括：
  [cmd] 发出的命令
  [out] 命令的输出结果
  [error] 错误信息
日志文件同时写入 %temp%\panbcdedit\log\ 文件夹，按天生成文件（如 panbcdedit_20260620.log）。
前往该目录可查看历史日志，如果不需要可自行删除，释放磁盘空间。


十、常见问题 FAQ

Q: 为什么某些启动项在表格中不显示？
A: 默认隐藏固件和系统启动项。请勾选“显示固件/系统启动项”。

Q: 修改后系统无法启动怎么办？
A: 使用 Windows 安装 U 盘进入 WinRE 修复环境：
   1. 打开命令提示符
   2. 执行 bcdedit /import <备份文件路径> /clean 恢复
   或执行 bcdboot C:\Windows 重建引导。

Q: 导入 BCD 后启动失败？
A: 导入操作会完全覆盖当前配置。请确保：
   • 导入的是同一台机器上导出的 BCD 文件
   • 磁盘分区结构没有发生变化
   • 跨硬件导入可能导致驱动不兼容

Q: 高级开关中改了某个选项，预览区却提示“没有需要应用的更改”？
A: 说明当前复选框状态与 BCD 中已设置的值相同，无需重复执行。
   本工具采用差异化执行策略，只修改有变化的项。

Q: 想恢复某个高级开关的默认值（未设置状态）？
A: 对于复选框，取消勾选即可（如果之前是开启的）。
   对于 Hypervisor，选“未设置”将执行 /deletevalue 删除该键。

═════════════════════════════════════════════════
panbcdedit  版本 26.7.14  |  zpan
";
        }
    }
}
