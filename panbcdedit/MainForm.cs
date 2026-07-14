using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace panbcdedit
{
    /// <summary>
    /// 主窗体：包含所有 UI 事件与 BCD 操作逻辑。
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// 启动项数据模型。
        /// </summary>
        private class BootEntry
        {
            public string Guid { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
            public string RawText { get; set; }
        }

        private List<BootEntry> _allEntries = new List<BootEntry>();
        private int _committedTimeout = 30;
        private string _currentDefaultGuid;
        private HashSet<string> _displayOrderGuids = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private bool _isBusy = false;
        private bool _isUefi = false;
        private bool _repairUnlocked = false;
        private ToolTip toolTip;


        private readonly string _logDirectory = Path.Combine(Path.GetTempPath(), "panbcdedit", "log");
        private readonly object _logLock = new object();

        private readonly Dictionary<string, string> _advancedInitialValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private string _hypervisorInitialValue;
        private List<string> _pendingAdvancedCommands;


        private readonly string[] _knownEntryTypes = new[]
        {
            "Windows Boot Loader",
            "Windows Boot Manager",
            "Firmware Boot Manager",
            "Legacy OS Loader",
            "RAM Disk Options",
            "Boot Sector Application",
            "Setup",
            "Debugger Settings",
            "Emergency Management Services",
            "Global Memory Diagnostics",
            "Boot Loader Settings",
            "Resume Loader Settings",
            "Hypervisor Settings",
            "Windows 启动加载器",
            "Windows 启动管理器",
            "固件启动管理器",
            "传统 OS 加载器",
            "RAM 磁盘选项"
        };


        public MainForm()
        {
            InitializeComponent();
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            windowsDirTextBox.Text = @"C:\Windows";
            InitializeData();
            WireEvents();
            var _t1 = LoadOverviewAsync();
            var _t2 = LoadBootEntriesAsync();
        }

        /// <summary>
        /// 供 Designer 使用的标签创建辅助方法。
        /// </summary>
        private Label CreateLabel(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = false,
                Dock = DockStyle.Fill,
                Margin = new Padding(5),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Name = string.Format("label_{0}", text.TrimEnd('：').Replace(" ", "_"))
            };
        }

        /// <summary>
        /// 初始化各下拉框与数据表格列。
        /// </summary>
        private void InitializeData()
        {
            createTypeComboBox.Items.AddRange(new object[] {
                "Windows Boot Loader",
                "Windows Boot Manager",
                "Legacy OS loader (ntldr)",
                "Ramdisk Options",
                "Linux/UEFI Boot Loader"
            });
            createTypeComboBox.SelectedIndex = 0;

            // 枚举可用本地驱动器填充盘符下拉
            try
            {
                foreach (System.IO.DriveInfo di in System.IO.DriveInfo.GetDrives())
                {
                    if (di.DriveType == System.IO.DriveType.Fixed
                        || di.DriveType == System.IO.DriveType.Removable)
                    {
                        string label = string.IsNullOrWhiteSpace(di.VolumeLabel)
                            ? di.Name.TrimEnd('\\')
                            : string.Format("{0} ({1})", di.Name.TrimEnd('\\'), di.VolumeLabel);
                        createDriveComboBox.Items.Add(label);
                    }
                }
            }
            catch
            {
                // 枚举失败时至少添加系统盘
                createDriveComboBox.Items.Add("C:");
            }
            if (createDriveComboBox.Items.Count == 0)
                createDriveComboBox.Items.Add("C:");
            if (createDriveComboBox.Items.Count > 0)
                createDriveComboBox.SelectedIndex = 0;

            hypervisorComboBox.Items.AddRange(new object[] { "未设置", "Auto", "Off" });
            hypervisorComboBox.SelectedIndex = 0;

            firmwareComboBox.Items.AddRange(new object[] { "自动", "UEFI", "BIOS" });
            firmwareComboBox.SelectedIndex = 0;

            this.toolTip = new ToolTip();
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ShowAlways = true;

            this.toolTip.SetToolTip(this.testsigningCheckBox, "testsigning：启用测试签名模式，允许加载未签名的驱动程序。");
            this.toolTip.SetToolTip(this.debugCheckBox, "debug：启用内核调试。");
            this.toolTip.SetToolTip(this.bootdebugCheckBox, "bootdebug：启用启动调试。");
            this.toolTip.SetToolTip(this.nointegritychecksCheckBox, "nointegritychecks：禁用完整性检查，允许加载未签名的驱动。");
            this.toolTip.SetToolTip(this.disableelamdriversCheckBox, "disableelamdrivers：禁用 Early Launch Anti-Malware 驱动。");
            this.toolTip.SetToolTip(this.paeCheckBox, "pae：物理地址扩展，强制启用 PAE 以支持 32 位系统大内存。");
            this.toolTip.SetToolTip(this.nxCheckBox, "nx：数据执行保护（No-eXecute），增强系统安全性。");
            this.toolTip.SetToolTip(this.novesaCheckBox, "novesa：禁用 VESA 显示，可能用于排查显示问题。");
            this.toolTip.SetToolTip(this.hypervisorComboBox, "hypervisorlaunchtype：Hyper-V 启动类型。Auto 表示自动启动，Off 表示关闭。");
            this.toolTip.SetToolTip(this.commandPreviewTextBox, "即将执行的 bcdedit 命令预览。");
            this.toolTip.SetToolTip(this.applyAdvancedButton, "应用当前高级开关设置到选中的启动项。");
            this.toolTip.SetToolTip(this.overwriteCheckBox, "不勾选：清空 BCD 并重建（默认，最干净）。\n勾选后：保留已有启动项，仅追加新条目到菜单末尾。");


            languageComboBox.Items.AddRange(new object[] {
                "zh-CN",
                "en-US",
                "ja-JP",
                "ko-KR",
                "de-DE",
                "fr-FR",
                "ru-RU"
            });
            languageComboBox.SelectedIndex = 0;

            // 引导修复为实验性功能，初始全部灰掉
            languageComboBox.Enabled = false;
            windowsDirTextBox.Enabled = false;
            windowsDirBrowseButton.Enabled = false;
            firmwareComboBox.Enabled = false;
            overwriteCheckBox.Enabled = false;
            repairButton.Enabled = false;
            repairResultTextBox.Enabled = false;

            entriesDataGridView.Columns.Clear();
            entriesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Guid",
                HeaderText = "标识符",
                DataPropertyName = "Guid",
                FillWeight = 40
            });
            entriesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Description",
                HeaderText = "名称",
                DataPropertyName = "Description",
                FillWeight = 40
            });
            entriesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Type",
                HeaderText = "类型",
                DataPropertyName = "Type",
                FillWeight = 20
            });
            entriesDataGridView.AutoGenerateColumns = false;

            timeoutNumericUpDown.Minimum = 0;
            timeoutNumericUpDown.Maximum = 999;
        }

        /// <summary>
        /// 绑定所有控件事件。
        /// </summary>
        private void WireEvents()
        {
            exportMenuItem.Click += ExportMenuItem_Click;
            exitMenuItem.Click += (s, e) => Close();
            shutdownMenuItem.Click += ShutdownMenuItem_Click;
            restartMenuItem.Click += RestartMenuItem_Click;
            restartFirmwareMenuItem.Click += RestartFirmwareMenuItem_Click;
            restartRecoveryMenuItem.Click += RestartRecoveryMenuItem_Click;
            aboutMenuItem.Click += AboutMenuItem_Click;
            guideMenuItem.Click += (s, e) => new GuideForm().ShowDialog(this);

            overviewRefreshButton.Click += OverviewRefreshButton_Click;
            showSystemEntriesCheckBox.CheckedChanged += async (s, e) =>
            {
                await LoadBootEntriesAsync(showSystemEntriesCheckBox.Checked);
            };

            copyEntryButton.Click += CopyEntryButton_Click;
            renameEntryButton.Click += RenameEntryButton_Click;
            deleteEntryButton.Click += DeleteEntryButton_Click;
            createButton.Click += CreateButton_Click;
            advancedCreateButton.Click += AdvancedCreateButton_Click;

            applyDefaultEntryButton.Click += ApplyDefaultEntryButton_Click;
            applyTimeoutButton.Click += ApplyTimeoutButton_Click;
            moveUpButton.Click += MoveUpButton_Click;
            moveDownButton.Click += MoveDownButton_Click;
            applyDisplayOrderButton.Click += ApplyDisplayOrderButton_Click;
            applyForceMenuButton.Click += ApplyForceMenuButton_Click;

            targetEntryComboBox.SelectedIndexChanged += (s, e) => InitializeAdvancedSwitches();
            applyAdvancedButton.Click += ApplyAdvancedButton_Click;

            repairButton.Click += RepairButton_Click;
            windowsDirBrowseButton.Click += WindowsDirBrowseButton_Click;
            unlockRepairButton.Click += UnlockRepairButton_Click;

            exportBrowseButton.Click += ExportBrowseButton_Click;
            exportButton.Click += ExportButton_Click;
            importBrowseButton.Click += ImportBrowseButton_Click;
            importButton.Click += ImportButton_Click;

            EventHandler previewHandler = (s, e) => UpdateCommandPreview();
            testsigningCheckBox.CheckedChanged += previewHandler;
            nointegritychecksCheckBox.CheckedChanged += previewHandler;
            debugCheckBox.CheckedChanged += previewHandler;
            bootdebugCheckBox.CheckedChanged += previewHandler;
            paeCheckBox.CheckedChanged += previewHandler;
            nxCheckBox.CheckedChanged += previewHandler;
            novesaCheckBox.CheckedChanged += previewHandler;
            disableelamdriversCheckBox.CheckedChanged += previewHandler;
            hypervisorComboBox.SelectedIndexChanged += previewHandler;

            // 切回设置页时恢复超时为已确认值（防止用户改完数字未应用就切走）
            tabControl.SelectedIndexChanged += (s, e) =>
            {
                if (tabControl.SelectedTab == tabPageSettings)
                    timeoutNumericUpDown.Value = _committedTimeout;
            };
        }

        #region 日志与操作锁定

        internal void LogCommand(string command)
        {
            AppendLog("[cmd] " + command, System.Drawing.Color.Blue);
        }

        internal void LogOutput(string output)
        {
            AppendLog("[out] " + output, System.Drawing.Color.Black);
        }

        internal void LogError(string error)
        {
            AppendLog("[error] " + error, System.Drawing.Color.Red);
        }

        /// <summary>
        /// 以指定颜色向日志区追加一行，同时写入文件日志，超过 200 行时保留最后 10 行。
        /// </summary>
        private void AppendLog(string text, System.Drawing.Color color)
        {
            WriteLogFile(text);

            if (InvokeRequired)
            {
                Invoke(new Action<string, System.Drawing.Color>(AppendLog), text, color);
                return;
            }

            logRichTextBox.SelectionStart = logRichTextBox.TextLength;
            logRichTextBox.SelectionLength = 0;
            logRichTextBox.SelectionColor = color;
            logRichTextBox.AppendText(text + Environment.NewLine);
            logRichTextBox.SelectionColor = logRichTextBox.ForeColor;
            logRichTextBox.ScrollToCaret();

            if (logRichTextBox.Lines.Length > 200)
            {
                string[] lastLines = logRichTextBox.Lines
                    .Skip(logRichTextBox.Lines.Length - 10)
                    .ToArray();
                logRichTextBox.Text = string.Join(Environment.NewLine, lastLines);
                logRichTextBox.SelectionStart = logRichTextBox.TextLength;
                logRichTextBox.ScrollToCaret();
            }
        }

        /// <summary>
        /// 将日志写入 %temp%\panbcdedit\log 文件夹，按天生成文件，并附带时间戳。
        /// </summary>
        private void WriteLogFile(string text)
        {
            try
            {
                Directory.CreateDirectory(_logDirectory);
                string logFile = Path.Combine(_logDirectory, string.Format("panbcdedit_{0}.log", DateTime.Now.ToString("yyyyMMdd")));
                string line = string.Format("[{0}] {1}{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), text, Environment.NewLine);
                lock (_logLock)
                {
                    File.AppendAllText(logFile, line, Encoding.UTF8);
                }
            }
            catch
            {
                // 文件日志失败不应影响主程序运行
            }
        }

        private void SetBusy(bool busy)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(SetBusy), busy);
                return;
            }

            _isBusy = busy;
            this.Enabled = !busy;
        }

        #endregion

        #region 命令执行

        private async Task<BcdResult> RunBcdCommandAsync(string arguments)
        {
            SetBusy(true);
            try
            {
                LogCommand("bcdedit " + arguments);
                BcdResult result = await BcdHelper.ExecuteBcdEditAsync(arguments);

                if (result.ExitCode == 0)
                {
                    if (!string.IsNullOrWhiteSpace(result.Output))
                        LogOutput(result.Output);
                }
                else
                {
                    LogError(string.IsNullOrWhiteSpace(result.Error) ? result.Output : result.Error);
                }

                return result;
            }
            catch (Exception ex)
            {
                LogError(string.Format("执行命令时发生异常：{0}", ex.Message));
                return new BcdResult { ExitCode = -1, Output = string.Empty, Error = ex.Message };
            }
            finally
            {
                SetBusy(false);
            }
        }

        private async Task<BcdResult> RunBcdBootCommandAsync(string arguments)
        {
            SetBusy(true);
            try
            {
                LogCommand("bcdboot " + arguments);
                BcdResult result = await BcdHelper.ExecuteBcdBootAsync(arguments);

                if (result.ExitCode == 0)
                {
                    if (!string.IsNullOrWhiteSpace(result.Output))
                        LogOutput(result.Output);
                }
                else
                {
                    LogError(string.IsNullOrWhiteSpace(result.Error) ? result.Output : result.Error);
                }

                return result;
            }
            catch (Exception ex)
            {
                LogError(string.Format("执行命令时发生异常：{0}", ex.Message));
                return new BcdResult { ExitCode = -1, Output = string.Empty, Error = ex.Message };
            }
            finally
            {
                SetBusy(false);
            }
        }

        #endregion

        #region 菜单事件

        private async void ExportMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmm");
                string defaultFileName = string.Format("bcd_backup_{0}.bcd", timestamp);

                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.FileName = defaultFileName;
                    dialog.Filter = "BCD 文件 (*.bcd)|*.bcd|所有文件 (*.*)|*.*";
                    dialog.DefaultExt = "bcd";

                    if (dialog.ShowDialog() != DialogResult.OK)
                        return;

                    string path = dialog.FileName;
                    string summary = string.Format("导出当前 BCD 配置到：\n{0}", path);
                    if (MessageBox.Show(summary, "确认导出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                        return;

                    await RunBcdCommandAsync(string.Format("/export \"{0}\"", path));
                }
            }
            catch (Exception ex)
            {
                LogError(string.Format("导出配置失败：{0}", ex.Message));
            }
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "作者：zpan\n软件名：panbcdedit\n版本 26.7.14",
                "关于",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ShutdownMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要关机吗？", "关机", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process.Start("shutdown", "/s /t 5");
                    LogCommand("关机（5 秒后执行）");
                }
                catch (Exception ex)
                {
                    LogError("关机失败：" + ex.Message);
                }
            }
        }

        private void RestartMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要重启吗？", "重启", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process.Start("shutdown", "/r /t 5");
                    LogCommand("重启（5 秒后执行）");
                }
                catch (Exception ex)
                {
                    LogError("重启失败：" + ex.Message);
                }
            }
        }

        private void RestartFirmwareMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要重启并进入 BIOS/UEFI 固件设置吗？\n\n（仅 UEFI 引导的 Windows 10/11 支持此功能）", "重启进入固件", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process.Start("shutdown", "/r /fw /t 5");
                    LogCommand("重启进入 BIOS/UEFI（5 秒后执行）");
                }
                catch (Exception ex)
                {
                    LogError("重启进入固件失败：" + ex.Message);
                }
            }
        }

        private void RestartRecoveryMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要重启并进入 Windows 修复界面吗？", "重启进入修复", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process.Start("shutdown", "/r /o /t 5");
                    LogCommand("重启进入 Windows 修复界面（5 秒后执行）");
                }
                catch (Exception ex)
                {
                    LogError("重启进入修复失败：" + ex.Message);
                }
            }
        }

        #endregion

        #region 选项卡 1：启动项总览

        private async Task LoadOverviewAsync()
        {
            try
            {
                SetBusy(true);
                LogCommand("bcdedit /enum all /v");
                BcdResult result = await BcdHelper.ExecuteBcdEditAsync("/enum all /v");
                SetBusy(false);

                string errorPart = string.IsNullOrEmpty(result.Error) ? "" : "\n[error] " + result.Error;
                string text = result.Output + errorPart;
                if (InvokeRequired)
                    Invoke(new Action(() => overviewRichTextBox.Text = text));
                else
                    overviewRichTextBox.Text = text;

                if (result.ExitCode == 0)
                {
                    if (!string.IsNullOrWhiteSpace(result.Output))
                    {
                        LogOutput("已加载启动项总览");

                        // 从输出中解析当前超时时间
                        var match = Regex.Match(result.Output, @"(?:timeout|超时)\s+(\d+)", RegexOptions.IgnoreCase);
                        int parsedTimeout;
                        if (match.Success && int.TryParse(match.Groups[1].Value, out parsedTimeout))
                        {
                            _committedTimeout = parsedTimeout;
                            if (InvokeRequired)
                                Invoke(new Action(() => timeoutNumericUpDown.Value = parsedTimeout));
                            else
                                timeoutNumericUpDown.Value = parsedTimeout;
                        }

                        // 从输出中解析当前默认启动项的 GUID
                        var defaultMatch = Regex.Match(result.Output, @"default\s+({[0-9a-fA-F\-]+})", RegexOptions.IgnoreCase);
                        string newDefault = defaultMatch.Success ? defaultMatch.Groups[1].Value : null;
                        if (!string.Equals(_currentDefaultGuid, newDefault, StringComparison.OrdinalIgnoreCase))
                        {
                            _currentDefaultGuid = newDefault;
                            // 更新默认启动项下拉的选中项
                            if (InvokeRequired)
                                Invoke(new Action(SelectDefaultEntry));
                            else
                                SelectDefaultEntry();
                        }

                        // 检测 UEFI 启动：UEFI 系统的 bcdedit 输出中会有"固件启动管理器"(中文)
                        // 或 "Firmware Boot Manager"(英文) 条目，BIOS 系统没有这个条目
                        _isUefi = result.Output.Contains("固件启动管理器")
                               || result.Output.Contains("Firmware Boot Manager");
                        if (_isUefi)
                        {
                            if (InvokeRequired)
                                Invoke(new Action(DisableForceMenuControls));
                            else
                                DisableForceMenuControls();
                        }
                    }
                }
                else
                {
                    LogError(string.IsNullOrWhiteSpace(result.Error) ? result.Output : result.Error);
                }
            }
            catch (Exception ex)
            {
                SetBusy(false);
                LogError(string.Format("加载总览失败：{0}", ex.Message));
            }
        }

        private async void OverviewRefreshButton_Click(object sender, EventArgs e)
        {
            await LoadOverviewAsync();
        }

        #endregion

        #region 选项卡 2：启动项管理

        private async Task LoadBootEntriesAsync(bool includeAll = false)
        {
            try
            {
                SetBusy(true);
                string args = includeAll ? "/enum all /v" : "/enum /v";
                LogCommand("bcdedit " + args);
                BcdResult result = await BcdHelper.ExecuteBcdEditAsync(args);
                SetBusy(false);

                // 将本次执行结果写入日志，便于后续分析
                string errorSection = string.IsNullOrEmpty(result.Error)
                    ? ""
                    : string.Format("错误输出：{0}{1}{0}", Environment.NewLine, result.Error);
                WriteLogFile(string.Format("---- bcdedit {0} 执行结果 ----{1}命令时间：{2}{1}退出码：{3}{1}原始输出长度：{4}{1}原始输出：{1}{5}{1}{6}---- 结束 ----",
                    args,
                    Environment.NewLine,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    result.ExitCode,
                    result.Output == null ? 0 : result.Output.Length,
                    result.Output ?? string.Empty,
                    errorSection));

                if (result.ExitCode != 0)
                {
                    LogError(string.IsNullOrWhiteSpace(result.Error) ? result.Output : result.Error);
                    return;
                }

                _allEntries = ParseBootEntries(result.Output);

                // 从同样的 bcdedit 输出中同步解析 displayorder GUID
                ParseDisplayOrderFromOutput(result.Output);

                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        RefreshEntriesGrid();
                        PopulateDisplayOrderList();
                        RefreshEntryCombos();
                    }));
                }
                else
                {
                    RefreshEntriesGrid();
                    PopulateDisplayOrderList();
                    RefreshEntryCombos();
                }

                LogOutput(string.Format("已加载 {0} 个启动项", _allEntries.Count));
            }
            catch (Exception ex)
            {
                SetBusy(false);
                LogError(string.Format("加载启动项失败：{0}", ex.Message));
            }
        }

        private List<BootEntry> ParseBootEntries(string raw)
        {
            List<BootEntry> entries = new List<BootEntry>();
            if (string.IsNullOrWhiteSpace(raw))
                return entries;

            // 必须按完整换行符分割，不能单独拆 \r 和 \n，否则 \r\n 会产生空行，
            // 导致标题行和虚线行永远对不上，最终解析为 0 条。
            string[] lines = raw.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            string currentTitle = null;
            List<string> currentParagraph = new List<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string trimmed = line == null ? "" : line.Trim();
                bool nextIsDashLine = (i + 1 < lines.Length) && IsDashLine(lines[i + 1]);

                if (!string.IsNullOrEmpty(trimmed) && nextIsDashLine)
                {
                    if (currentTitle != null && currentParagraph.Count > 0)
                    {
                        entries.Add(ParseEntryParagraph(currentTitle, currentParagraph));
                    }
                    currentTitle = trimmed;
                    currentParagraph = new List<string>();
                    i++;
                }
                else
                {
                    currentParagraph.Add(line);
                }
            }

            if (currentTitle != null && currentParagraph.Count > 0)
            {
                entries.Add(ParseEntryParagraph(currentTitle, currentParagraph));
            }

            return entries;
        }

        private bool IsDashLine(string line)
        {
            if (string.IsNullOrEmpty(line))
                return false;
            string trimmed = line.Trim();
            return trimmed.Length >= 3 && trimmed.All(c => c == '-');
        }

        private BootEntry ParseEntryParagraph(string title, List<string> paragraph)
        {
            string paragraphText = string.Join("\n", paragraph);
            // 支持英文 identifier 与中文“标识符”
            Match idMatch = Regex.Match(paragraphText, @"(?:identifier|标识符)\s+({[0-9a-fA-F\-]+})", RegexOptions.IgnoreCase);
            string identifier = idMatch.Success ? idMatch.Groups[1].Value : "";

            // 支持英文 description 与中文“描述”
            Match descMatch = Regex.Match(paragraphText, @"(?:description|描述)\s+(.+)", RegexOptions.IgnoreCase);
            string description = descMatch.Success ? descMatch.Groups[1].Value.Trim() : "";

            if (string.IsNullOrEmpty(identifier) && string.IsNullOrEmpty(description))
            {
                return new BootEntry
                {
                    Guid = "未知",
                    Description = paragraphText.Trim(),
                    Type = "解析失败",
                    RawText = paragraphText.Trim()
                };
            }

            bool isKnownType = _knownEntryTypes.Any(t => string.Equals(t, title, StringComparison.OrdinalIgnoreCase));
            return new BootEntry
            {
                Guid = string.IsNullOrEmpty(identifier) ? "未知" : identifier,
                Description = string.IsNullOrEmpty(description) ? title : description,
                Type = isKnownType ? title : "其他",
                RawText = paragraphText.Trim()
            };
        }


        private void RefreshEntriesGrid()
        {
            entriesDataGridView.DataSource = null;
            entriesDataGridView.DataSource = _allEntries;
        }

        /// <summary>
        /// 刷新默认启动项下拉、高级开关目标条目下拉。
        /// 默认启动项仅显示 displayorder 中的条目。
        /// </summary>
        private void RefreshEntryCombos()
        {
            // 保存当前高级开关页选中的条目 GUID
            string prevTargetGuid = targetEntryComboBox.SelectedItem == null
                ? null : ExtractGuid(targetEntryComboBox.SelectedItem.ToString());

            defaultEntryComboBox.Items.Clear();
            targetEntryComboBox.Items.Clear();

            int newTargetIndex = -1;
            int idx = 0;
            foreach (BootEntry entry in _allEntries)
            {
                string display = string.Format("{0} ({1})", entry.Description, entry.Guid);
                targetEntryComboBox.Items.Add(display);

                // 尝试匹配之前选中的目标条目（通过 GUID）
                if (prevTargetGuid != null && newTargetIndex < 0 &&
                    entry.Guid.Equals(prevTargetGuid, StringComparison.OrdinalIgnoreCase))
                {
                    newTargetIndex = idx;
                }
                idx++;

                // 默认启动项下拉只显示 displayorder 中存在的条目
                if (_displayOrderGuids.Count == 0 || _displayOrderGuids.Contains(entry.Guid))
                {
                    defaultEntryComboBox.Items.Add(display);
                }
            }

            // 恢复目标条目选择（触发 SelectedIndexChanged → 重新加载高级开关状态）
            if (newTargetIndex >= 0)
                targetEntryComboBox.SelectedIndex = newTargetIndex;

            SelectDefaultEntry();
        }

        /// <summary>
        /// 在默认启动项下拉框选中当前默认项。
        /// </summary>
        private void SelectDefaultEntry()
        {
            if (string.IsNullOrEmpty(_currentDefaultGuid))
                return;

            for (int i = 0; i < defaultEntryComboBox.Items.Count; i++)
            {
                string item = defaultEntryComboBox.Items[i].ToString();
                if (item.IndexOf(_currentDefaultGuid, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    defaultEntryComboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        private string GetSelectedEntryGuid()
        {
            if (entriesDataGridView.SelectedRows.Count == 0)
                return null;

            DataGridViewRow row = entriesDataGridView.SelectedRows[0];
            BootEntry entry = row.DataBoundItem as BootEntry;
            return entry == null ? null : entry.Guid;
        }

        private async void CopyEntryButton_Click(object sender, EventArgs e)
        {
            try
            {
                string guid = GetSelectedEntryGuid();
                if (string.IsNullOrEmpty(guid))
                {
                    MessageBox.Show("请先选择一个要复制的启动项。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                BootEntry entry = _allEntries.FirstOrDefault(x => x.Guid == guid);
                string defaultValue = entry == null ? "" : entry.Description + " 副本";
                string newDescription = InputDialog.Show("请输入新启动项描述：", "复制启动项", defaultValue);
                if (string.IsNullOrWhiteSpace(newDescription))
                    return;

                string summary = string.Format("复制启动项\n源 GUID：{0}\n新描述：{1}", guid, newDescription);
                if (MessageBox.Show(summary, "确认复制", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;

                await RunBcdCommandAsync(string.Format("/copy {0} /d \"{1}\"", guid, newDescription));
                await LoadBootEntriesAsync(showSystemEntriesCheckBox.Checked);
            }
            catch (Exception ex)
            {
                LogError(string.Format("复制启动项失败：{0}", ex.Message));
            }
        }

        private async void RenameEntryButton_Click(object sender, EventArgs e)
        {
            try
            {
                string guid = GetSelectedEntryGuid();
                if (string.IsNullOrEmpty(guid))
                {
                    MessageBox.Show("请先选择一个要修改名称的启动项。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                BootEntry entry = _allEntries.FirstOrDefault(x => x.Guid == guid);
                string oldDescription = entry == null ? "" : entry.Description;
                string newDescription = InputDialog.Show("请输入新名称：", "修改启动项名称", oldDescription);
                if (string.IsNullOrWhiteSpace(newDescription) || newDescription == oldDescription)
                    return;

                await RunBcdCommandAsync(string.Format("/set {0} description \"{1}\"", guid, newDescription));
                await LoadBootEntriesAsync(showSystemEntriesCheckBox.Checked);
            }
            catch (Exception ex)
            {
                LogError(string.Format("修改名称失败：{0}", ex.Message));
            }
        }

        private async void DeleteEntryButton_Click(object sender, EventArgs e)
        {
            try
            {
                string guid = GetSelectedEntryGuid();
                if (string.IsNullOrEmpty(guid))
                {
                    MessageBox.Show("请先选择一个要删除的启动项。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                BcdResult defaultResult = await BcdHelper.ExecuteBcdEditAsync("/default");
                if (defaultResult.ExitCode == 0 && defaultResult.Output.IndexOf(guid, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    MessageBox.Show("该条目是当前默认启动项，请先更改默认项后再删除。", "无法删除", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                BootEntry entry = _allEntries.FirstOrDefault(x => x.Guid == guid);
                string description = entry == null ? "" : entry.Description;
                string summary = string.Format("删除启动项\nGUID：{0}\n描述：{1}", guid, description);
                if (MessageBox.Show(summary, "确认删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                    return;

                await RunBcdCommandAsync(string.Format("/delete {0} /f", guid));
                await LoadBootEntriesAsync(showSystemEntriesCheckBox.Checked);
            }
            catch (Exception ex)
            {
                LogError(string.Format("删除启动项失败：{0}", ex.Message));
            }
        }

        private async void CreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                string description = createNameTextBox.Text.Trim();
                string driveText = createDriveComboBox.SelectedItem == null ? null : createDriveComboBox.SelectedItem.ToString();
                string drive = driveText;
                if (!string.IsNullOrEmpty(driveText) && driveText.Length >= 2)
                    drive = driveText.Substring(0, 2); // 提取盘符 "C:"

                // 预检 1：管理员权限
                if (BcdHelper.ExecuteBcdEdit("/?").ExitCode != 0)
                {
                    MessageBox.Show("bcdedit 无法执行，请以管理员身份运行本程序。", "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 预检 2：名称
                if (string.IsNullOrWhiteSpace(description))
                {
                    MessageBox.Show("请输入启动项名称。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 预检 3：盘符
                if (string.IsNullOrWhiteSpace(drive))
                {
                    MessageBox.Show("请选择盘符。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string drivePath = drive.TrimEnd('\\') + @"\";

                // 预检 4：检测引导文件
                string bootFile = _isUefi
                    ? @"Windows\System32\boot\winload.efi"
                    : @"Windows\system32\winload.exe";
                if (!File.Exists(drivePath + bootFile))
                {
                    string msg = string.Format("在 {0} 未检测到 Windows 引导文件。\n\n" +
                        "期望路径：{1}{2}\n\n" +
                        "该盘可能没有安装 Windows 系统，请确认后重试。",
                        drive.TrimEnd('\\'), drivePath, bootFile);
                    MessageBox.Show(msg, "未找到 Windows 系统", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string summary = string.Format("一键创建 Windows 启动项\n名称：{0}\n盘符：{1}", description, drive);
                if (MessageBox.Show(summary, "确认创建", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;

                // 执行创建
                BcdResult result = await RunBcdCommandAsync(string.Format("/create /d \"{0}\" /application osloader", description));
                if (result.ExitCode != 0)
                {
                    MessageBox.Show("创建启动项失败，请查看日志了解详情。", "创建失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Match match = Regex.Match(result.Output, @"({[0-9a-fA-F\-]+})");
                string createdGuid = match.Success ? match.Value : null;

                if (string.IsNullOrWhiteSpace(createdGuid))
                {
                    LogError("创建成功但无法从输出中提取新条目 GUID。");
                    MessageBox.Show("创建成功但无法获取 GUID，请刷新列表查看。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    createNameTextBox.Clear();
                    await LoadBootEntriesAsync(showSystemEntriesCheckBox.Checked);
                    return;
                }

                bool allSucceeded = true;
                string partition = string.Format("partition={0}", drive.TrimEnd('\\'));
                string bootPath = _isUefi
                    ? @"\Windows\system32\boot\winload.efi"
                    : @"\Windows\system32\winload.exe";
                string locale = System.Globalization.CultureInfo.CurrentCulture.Name;

                allSucceeded &= await TrySetEntryParam(createdGuid, "device", partition);
                allSucceeded &= await TrySetEntryParam(createdGuid, "osdevice", partition);
                allSucceeded &= await TrySetEntryParam(createdGuid, "path", bootPath);
                allSucceeded &= await TrySetEntryParam(createdGuid, "systemroot", "\\Windows");
                allSucceeded &= await TrySetEntryParam(createdGuid, "locale", locale);

                // 将新条目添加到启动菜单显示顺序末尾，否则 /enum 看不到
                if (allSucceeded)
                {
                    BcdResult displayResult = await RunBcdCommandAsync(
                        string.Format("/displayorder {0} /addlast", createdGuid));
                    if (displayResult.ExitCode != 0)
                    {
                        LogError(string.Format("添加新条目到启动菜单失败（不影响条目存在）：{0}", displayResult.Error));
                    }
                }

                if (!allSucceeded)
                {
                    LogError(string.Format("部分参数设置失败，尝试回滚删除新条目 {0}", createdGuid));
                    BcdResult rollback = await BcdHelper.ExecuteBcdEditAsync(string.Format("/delete {0} /f", createdGuid));
                    if (rollback.ExitCode != 0)
                        LogError(string.Format("回滚删除条目 {0} 也失败了，可能需要手动清理。", createdGuid));
                    MessageBox.Show("参数设置失败，已自动回滚删除。请查看日志了解详情。", "创建失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(string.Format("启动项创建成功！\n\n名称：{0}\nGUID：{1}", description, createdGuid),
                        "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                createNameTextBox.Clear();
                await LoadBootEntriesAsync(showSystemEntriesCheckBox.Checked);
            }
            catch (Exception ex)
            {
                LogError(string.Format("创建启动项失败：{0}", ex.Message));
                MessageBox.Show(string.Format("创建启动项时发生异常：{0}", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 执行单个 bcdedit /set 命令，失败时记录日志并返回 false。
        /// </summary>
        private async Task<bool> TrySetEntryParam(string guid, string paramName, string paramValue)
        {
            BcdResult result = await RunBcdCommandAsync(string.Format("/set {0} {1} {2}", guid, paramName, paramValue));
            if (result.ExitCode != 0)
            {
                LogError(string.Format("设置 {0} 失败：{1}", paramName, result.Error));
                return false;
            }
            return true;
        }

        private async void AdvancedCreateButton_Click(object sender, EventArgs e)
        {
            string type = createTypeComboBox.SelectedItem == null ? "Windows Boot Loader" : createTypeComboBox.SelectedItem.ToString();
            using (var form = new CreateAdvancedForm(type, _isUefi, LogCommand, LogOutput, LogError))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                    await LoadBootEntriesAsync(showSystemEntriesCheckBox.Checked);
            }
        }


        private string MapTypeToApplication(string type)
        {
            if (type == "Windows Boot Manager") return "bootmgr";
            if (type == "Legacy OS loader (ntldr)") return "ntldr";
            if (type == "Ramdisk Options") return "ramdisk";
            return "osloader";
        }

        #endregion

        #region 选项卡 3：常见启动项设置

        private async void ApplyDefaultEntryButton_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = defaultEntryComboBox.SelectedItem == null ? null : defaultEntryComboBox.SelectedItem.ToString();
                if (string.IsNullOrEmpty(selected))
                {
                    MessageBox.Show("请选择一个默认启动项。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string guid = ExtractGuid(selected);
                if (string.IsNullOrEmpty(guid))
                {
                    MessageBox.Show("无法提取 GUID。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string summary = string.Format("将默认启动项设置为：\n{0}", selected);
                if (MessageBox.Show(summary, "确认应用", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;

                await RunBcdCommandAsync(string.Format("/default {0}", guid));
                await LoadBootEntriesAsync(showSystemEntriesCheckBox.Checked);
            }
            catch (Exception ex)
            {
                LogError(string.Format("设置默认启动项失败：{0}", ex.Message));
            }
        }

        private async void ApplyTimeoutButton_Click(object sender, EventArgs e)
        {
            try
            {
                int timeout = (int)timeoutNumericUpDown.Value;
                string summary = string.Format("将启动菜单超时时间设置为 {0} 秒。", timeout);
                if (MessageBox.Show(summary, "确认应用", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;

                await RunBcdCommandAsync(string.Format("/timeout {0}", timeout));
                _committedTimeout = timeout;
            }
            catch (Exception ex)
            {
                LogError(string.Format("设置超时失败：{0}", ex.Message));
            }
        }

        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            int index = displayOrderListBox.SelectedIndex;
            if (index > 0)
            {
                object item = displayOrderListBox.Items[index];
                displayOrderListBox.Items.RemoveAt(index);
                displayOrderListBox.Items.Insert(index - 1, item);
                displayOrderListBox.SelectedIndex = index - 1;
            }
        }

        private void MoveDownButton_Click(object sender, EventArgs e)
        {
            int index = displayOrderListBox.SelectedIndex;
            if (index >= 0 && index < displayOrderListBox.Items.Count - 1)
            {
                object item = displayOrderListBox.Items[index];
                displayOrderListBox.Items.RemoveAt(index);
                displayOrderListBox.Items.Insert(index + 1, item);
                displayOrderListBox.SelectedIndex = index + 1;
            }
        }

        private async void ApplyDisplayOrderButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> guids = new List<string>();
                foreach (object item in displayOrderListBox.Items)
                {
                    string guid = ExtractGuid(item.ToString());
                    if (!string.IsNullOrEmpty(guid))
                        guids.Add(guid);
                }

                if (guids.Count == 0)
                {
                    MessageBox.Show("显示顺序列表为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string summary = "应用新的启动项显示顺序：\n" + string.Join("\n", guids);
                if (MessageBox.Show(summary, "确认应用", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;

                string args = "/displayorder " + string.Join(" ", guids);
                await RunBcdCommandAsync(args);
                await LoadBootEntriesAsync(showSystemEntriesCheckBox.Checked);
            }
            catch (Exception ex)
            {
                LogError(string.Format("应用显示顺序失败：{0}", ex.Message));
            }
        }

        private async void ApplyForceMenuButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool force = forceMenuCheckBox.Checked;
                // bcdedit /bootmenupolicy 只接受 Standard 或 Legacy
                string value = force ? "Legacy" : "Standard";
                string summary = string.Format("将启动菜单策略设置为 {0}（{1}）。", value, force ? "强制显示完整菜单" : "标准菜单");
                if (MessageBox.Show(summary, "确认应用", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;

                BcdResult result = await RunBcdCommandAsync(string.Format("/bootmenupolicy {0}", value));
                if (result.ExitCode != 0)
                {
                    MessageBox.Show("此功能仅支持 BIOS/Legacy 启动的系统，您的系统可能为 UEFI 启动。",
                        "不支持此功能", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await LoadBootEntriesAsync(showSystemEntriesCheckBox.Checked);
                }
            }
            catch (Exception ex)
            {
                LogError(string.Format("设置强制菜单失败：{0}", ex.Message));
            }
        }

        private void DisableForceMenuControls()
        {
            forceMenuCheckBox.Enabled = false;
            applyForceMenuButton.Enabled = false;
        }


        private string ExtractGuid(string text)
        {
            Match match = Regex.Match(text ?? "", @"({[0-9a-fA-F\-]+})");
            return match.Success ? match.Value : null;
        }

        /// <summary>
        /// 从 bcdedit 输出文本中解析 displayorder 的 GUID 列表。
        /// </summary>
        private void ParseDisplayOrderFromOutput(string bcdeditOutput)
        {
            string[] lines = bcdeditOutput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            List<string> orderGuids = new List<string>();
            bool inDisplayorder = false;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string trimmed = line == null ? "" : line.Trim();

                if (!inDisplayorder)
                {
                    if (trimmed.StartsWith("displayorder", StringComparison.OrdinalIgnoreCase))
                    {
                        inDisplayorder = true;
                        orderGuids.AddRange(Regex.Matches(trimmed, @"{[0-9a-fA-F\-]+}")
                            .Cast<Match>().Select(m => m.Value));
                    }
                }
                else
                {
                    if (line.Length > 0 && char.IsWhiteSpace(line[0]) && trimmed.Length > 0)
                    {
                        var guids = Regex.Matches(trimmed, @"{[0-9a-fA-F\-]+}")
                            .Cast<Match>().Select(m => m.Value).ToList();
                        if (guids.Count > 0)
                            orderGuids.AddRange(guids);
                        else
                            break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            _displayOrderGuids = new HashSet<string>(orderGuids, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 根据 _displayOrderGuids 和 _allEntries 填充右侧显示顺序列表。
        /// </summary>
        private void PopulateDisplayOrderList()
        {
            displayOrderListBox.Items.Clear();
            foreach (string guid in _displayOrderGuids)
            {
                BootEntry entry = _allEntries.FirstOrDefault(x => x.Guid.Equals(guid, StringComparison.OrdinalIgnoreCase));
                string display = entry == null ? string.Format("未知条目 ({0})", guid) : string.Format("{0} ({1})", entry.Description, guid);
                displayOrderListBox.Items.Add(display);
            }
        }

        /// <summary>
        /// 手动刷新显示顺序（从独立 bcdedit 调用获取最新数据）。
        /// </summary>
        private async void RefreshDisplayOrder()
        {
            try
            {
                BcdResult result = await BcdHelper.ExecuteBcdEditAsync("/enum bootmgr /v");
                if (result.ExitCode == 0)
                {
                    ParseDisplayOrderFromOutput(result.Output);
                    PopulateDisplayOrderList();
                    // 同时刷新默认启动项下拉（可能 displayorder 变了）
                    SelectDefaultEntry();
                }
            }
            catch (Exception ex)
            {
                LogError(string.Format("刷新显示顺序失败：{0}", ex.Message));
            }
        }

        #endregion

        #region 选项卡 4：高级开关

        private async void InitializeAdvancedSwitches()
        {
            try
            {
                _advancedInitialValues.Clear();
                _hypervisorInitialValue = null;

                string selected = targetEntryComboBox.SelectedItem == null ? null : targetEntryComboBox.SelectedItem.ToString();
                if (string.IsNullOrEmpty(selected))
                {
                    UpdateCommandPreview();
                    return;
                }

                string guid = ExtractGuid(selected);
                if (string.IsNullOrEmpty(guid))
                {
                    UpdateCommandPreview();
                    return;
                }

                SetBusy(true);
                BcdResult result = await BcdHelper.ExecuteBcdEditAsync(string.Format("/enum {0} /v", guid));
                SetBusy(false);

                if (result.ExitCode != 0)
                {
                    UpdateCommandPreview();
                    return;
                }

                string text = result.Output;
                SetCheckFromBcdValue(text, "testsigning", testsigningCheckBox);
                SetCheckFromBcdValue(text, "nointegritychecks", nointegritychecksCheckBox);
                SetCheckFromBcdValue(text, "debug", debugCheckBox);
                SetCheckFromBcdValue(text, "bootdebug", bootdebugCheckBox);
                SetCheckFromBcdValue(text, "pae", paeCheckBox);
                SetCheckFromBcdValue(text, "nx", nxCheckBox);
                SetCheckFromBcdValue(text, "novesa", novesaCheckBox);
                SetCheckFromBcdValue(text, "disableelamdrivers", disableelamdriversCheckBox);

                Match hyperMatch = Regex.Match(text, @"hypervisorlaunchtype\s+(\w+)", RegexOptions.IgnoreCase);
                if (hyperMatch.Success)
                {
                    _hypervisorInitialValue = hyperMatch.Groups[1].Value;
                    hypervisorComboBox.SelectedIndex = _hypervisorInitialValue.Equals("Off", StringComparison.OrdinalIgnoreCase) ? 2 : 1;
                }
                else
                {
                    hypervisorComboBox.SelectedIndex = 0;
                }

                UpdateCommandPreview();
            }
            catch (Exception ex)
            {
                SetBusy(false);
                LogError(string.Format("初始化高级开关失败：{0}", ex.Message));
            }
        }


        private bool IsBcdValueOn(string value)
        {
            return !string.IsNullOrEmpty(value) &&
                   (value.Equals("Yes", StringComparison.OrdinalIgnoreCase) ||
                    value.Equals("On", StringComparison.OrdinalIgnoreCase) ||
                    value.Equals("Auto", StringComparison.OrdinalIgnoreCase));
        }

        private int HypervisorValueToIndex(string value)
        {
            if (string.IsNullOrEmpty(value)) return 0;
            if (value.Equals("Off", StringComparison.OrdinalIgnoreCase)) return 2;
            return 1;
        }

        private void SetCheckFromBcdValue(string text, string key, CheckBox checkBox)
        {
            Match match = Regex.Match(text, string.Format("{0}\\s+(\\w+)", key), RegexOptions.IgnoreCase);
            string value = match.Success ? match.Groups[1].Value : null;
            _advancedInitialValues[key] = value;
            checkBox.Checked = IsBcdValueOn(value);
        }


        private void UpdateCommandPreview()
        {
            try
            {
                string selected = targetEntryComboBox.SelectedItem == null ? null : targetEntryComboBox.SelectedItem.ToString();
                string guid = ExtractGuid(selected);
                if (string.IsNullOrEmpty(guid))
                {
                    commandPreviewTextBox.Text = "请先在“目标条目”下拉框中选择一个启动项。";
                    _pendingAdvancedCommands = null;
                    return;
                }

                List<string> commands = new List<string>();
                AddBcdSwitchIfChanged(commands, guid, "testsigning", testsigningCheckBox.Checked);
                AddBcdSwitchIfChanged(commands, guid, "nointegritychecks", nointegritychecksCheckBox.Checked);
                AddBcdSwitchIfChanged(commands, guid, "debug", debugCheckBox.Checked);
                AddBcdSwitchIfChanged(commands, guid, "bootdebug", bootdebugCheckBox.Checked);
                AddBcdSwitchIfChanged(commands, guid, "pae", paeCheckBox.Checked);
                AddBcdSwitchIfChanged(commands, guid, "nx", nxCheckBox.Checked);
                AddBcdSwitchIfChanged(commands, guid, "novesa", novesaCheckBox.Checked);
                AddBcdSwitchIfChanged(commands, guid, "disableelamdrivers", disableelamdriversCheckBox.Checked);

                int hyperIndex = hypervisorComboBox.SelectedIndex;
                int initialHyperIndex = HypervisorValueToIndex(_hypervisorInitialValue);
                if (hyperIndex != initialHyperIndex)
                {
                    if (hyperIndex == 0) commands.Add(string.Format("bcdedit /deletevalue {0} hypervisorlaunchtype", guid));
                    else if (hyperIndex == 1) commands.Add(string.Format("bcdedit /set {0} hypervisorlaunchtype Auto", guid));
                    else if (hyperIndex == 2) commands.Add(string.Format("bcdedit /set {0} hypervisorlaunchtype Off", guid));
                }

                _pendingAdvancedCommands = commands;

                if (commands.Count == 0)
                {
                    commandPreviewTextBox.Text = "没有需要应用的更改。";
                    return;
                }

                commandPreviewTextBox.Text = string.Join(Environment.NewLine, commands);
            }
            catch (Exception ex)
            {
                commandPreviewTextBox.Text = string.Format("预览生成失败：{0}", ex.Message);
                _pendingAdvancedCommands = null;
            }
        }

        private void AddBcdSwitchIfChanged(List<string> commands, string guid, string key, bool currentChecked)
        {
            string initialValue;
            bool initialOn = _advancedInitialValues.TryGetValue(key, out initialValue) && IsBcdValueOn(initialValue);
            if (currentChecked == initialOn) return;
            commands.Add(string.Format("bcdedit /set {0} {1} {2}", guid, key, currentChecked ? "Yes" : "No"));
        }


        private async void ApplyAdvancedButton_Click(object sender, EventArgs e)
        {
            try
            {
                string preview = commandPreviewTextBox.Text;
                if (preview.StartsWith("请先在目标") || preview.StartsWith("预览生成失败"))
                {
                    MessageBox.Show(preview, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (_pendingAdvancedCommands == null || _pendingAdvancedCommands.Count == 0)
                {
                    MessageBox.Show("没有需要应用的更改。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string summary = string.Format("即将执行以下命令：\n{0}", preview);
                if (MessageBox.Show(summary, "确认应用", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                    return;

                foreach (string command in _pendingAdvancedCommands)
                {
                    string args = command.StartsWith("bcdedit ", StringComparison.OrdinalIgnoreCase)
                        ? command.Substring("bcdedit ".Length)
                        : command;
                    await RunBcdCommandAsync(args);
                }

                await LoadBootEntriesAsync(showSystemEntriesCheckBox.Checked);
                InitializeAdvancedSwitches();
            }
            catch (Exception ex)
            {
                LogError(string.Format("应用高级开关失败：{0}", ex.Message));
            }
        }


        #endregion

        #region 选项卡 5：引导修复

        private void UnlockRepairButton_Click(object sender, EventArgs e)
        {
            if (_repairUnlocked) return;

            var result = MessageBox.Show(
                "这是实验性功能，作者没有做过多测试，请谨慎使用！\n\n" +
                "引导修复（bcdboot）会修改系统引导配置，操作不当可能导致系统无法启动。\n" +
                "强烈建议在操作前先通过 [常见启动项设置 > 导出 BCD] 备份当前配置。\n\n" +
                "确定要继续吗？",
                "⚠ 实验性功能警告",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result != DialogResult.OK) return;

            _repairUnlocked = true;
            languageComboBox.Enabled = true;
            windowsDirTextBox.Enabled = true;
            windowsDirBrowseButton.Enabled = true;
            firmwareComboBox.Enabled = true;
            overwriteCheckBox.Enabled = true;
            repairButton.Enabled = true;
            repairResultTextBox.Enabled = true;
        }

        private void WindowsDirBrowseButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "选择 Windows 安装目录（如 C:\\Windows）";
                if (dialog.ShowDialog() == DialogResult.OK)
                    windowsDirTextBox.Text = dialog.SelectedPath;
            }
        }

        private async void RepairButton_Click(object sender, EventArgs e)
        {
            try
            {
                string windowsDir = windowsDirTextBox.Text.Trim();
                if (string.IsNullOrEmpty(windowsDir) || !Directory.Exists(windowsDir))
                {
                    MessageBox.Show("请选择有效的 Windows 目录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string language = languageComboBox.SelectedItem == null ? null : languageComboBox.SelectedItem.ToString();
                string firmware = firmwareComboBox.SelectedItem == null ? null : firmwareComboBox.SelectedItem.ToString();
                bool overwrite = overwriteCheckBox.Checked;

                List<string> args = new List<string> { string.Format("\"{0}\"", windowsDir) };
                if (!string.IsNullOrEmpty(language))
                    args.Add(string.Format("/l {0}", language));

                if (firmware == "UEFI")
                    args.Add("/f UEFI");
                else if (firmware == "BIOS")
                    args.Add("/f BIOS");

                if (overwrite)
                    args.Add("/addlast");

                string summary = string.Format("执行引导修复命令：\nbcdboot {0}", string.Join(" ", args));
                if (MessageBox.Show(summary, "确认执行修复", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                    return;

                BcdResult result = await RunBcdBootCommandAsync(string.Join(" ", args));
                repairResultTextBox.Text = result.ExitCode == 0
                    ? result.Output + "\n修复完成。"
                    : (string.IsNullOrWhiteSpace(result.Error) ? result.Output : result.Error);

                await LoadBootEntriesAsync(showSystemEntriesCheckBox.Checked);
            }
            catch (Exception ex)
            {
                LogError(string.Format("引导修复失败：{0}", ex.Message));
                repairResultTextBox.Text = string.Format("错误：{0}", ex.Message);
            }
        }

        #endregion

        #region 选项卡 3：常见启动项设置 - 导出/导入

        private void ExportBrowseButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.FileName = string.Format("bcd_backup_{0}.bcd", DateTime.Now.ToString("yyyyMMdd_HHmm"));
                dialog.Filter = "BCD 文件 (*.bcd)|*.bcd|所有文件 (*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                    exportPathTextBox.Text = dialog.FileName;
            }
        }

        private async void ExportButton_Click(object sender, EventArgs e)
        {
            try
            {
                string path = exportPathTextBox.Text.Trim();
                if (string.IsNullOrEmpty(path))
                {
                    MessageBox.Show("请选择导出路径。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string summary = string.Format("导出 BCD 配置到：\n{0}", path);
                if (MessageBox.Show(summary, "确认导出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;

                await RunBcdCommandAsync(string.Format("/export \"{0}\"", path));
            }
            catch (Exception ex)
            {
                LogError(string.Format("导出失败：{0}", ex.Message));
            }
        }

        private void ImportBrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "BCD 文件 (*.bcd)|*.bcd|所有文件 (*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                    importPathTextBox.Text = dialog.FileName;
            }
        }

        private async void ImportButton_Click(object sender, EventArgs e)
        {
            try
            {
                string path = importPathTextBox.Text.Trim();
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    MessageBox.Show("请选择有效的 BCD 文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string tempBackup = Path.Combine(Path.GetTempPath(), string.Format("bcd_auto_backup_before_import_{0}.bcd", DateTime.Now.ToString("yyyyMMdd_HHmmss")));
                BcdResult backupResult = await BcdHelper.ExecuteBcdEditAsync(string.Format("/export \"{0}\"", tempBackup));
                if (backupResult.ExitCode == 0)
                    LogOutput(string.Format("导入前自动备份已保存：{0}", tempBackup));
                else
                    LogError(string.Format("导入前自动备份失败：{0}", backupResult.Error));

                string confirmation = InputDialog.Show(
                    "导入 BCD 将覆盖当前系统启动配置，风险极高。\n请在下方输入“确认导入”四个字以继续：",
                    "严重警告：手动确认导入",
                    "");

                if (confirmation != "确认导入")
                {
                    MessageBox.Show("未输入“确认导入”，操作已取消。", "取消", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string summary = string.Format("即将导入 BCD 文件：\n{0}\n\n当前 BCD 已自动备份到：\n{1}", path, tempBackup);
                if (MessageBox.Show(summary, "最终确认导入", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                    return;

                await RunBcdCommandAsync(string.Format("/import \"{0}\" /clean", path));
                await LoadBootEntriesAsync(showSystemEntriesCheckBox.Checked);
                await LoadOverviewAsync();
            }
            catch (Exception ex)
            {
                LogError(string.Format("导入失败：{0}", ex.Message));
            }
        }

        #endregion
    }
    
}
