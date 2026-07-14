using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace panbcdedit
{
    /// <summary>
    /// 高级模式创建启动项的子窗口。
    /// 根据传入的类型动态显示对应的参数控件。
    /// </summary>
    public partial class CreateAdvancedForm : Form
    {
        private readonly string _entryType;
        private readonly string _applicationType;
        private readonly bool _isUefi;
        private readonly Action<string> _logCommand;
        private readonly Action<string> _logOutput;
        private readonly Action<string> _logError;
        private TextBox nameTextBox;

        /// <summary>
        /// Linux/UEFI 模式时记录的 ESP 挂载信息
        /// </summary>
        private EspHelper.EspInfo _espInfo;

        /// <summary>
        /// 存储动态添加的参数控件：key = bcdedit 参数名, value = TextBox。
        /// </summary>
        private readonly Dictionary<string, TextBox> _paramTextBoxes = new Dictionary<string, TextBox>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 存储动态添加的下拉选择参数：key = bcdedit 参数名, value = ComboBox。
        /// </summary>
        private readonly Dictionary<string, ComboBox> _paramCombos = new Dictionary<string, ComboBox>(StringComparer.OrdinalIgnoreCase);

        public CreateAdvancedForm(string entryType, bool isUefi,
            Action<string> logCommand = null,
            Action<string> logOutput = null,
            Action<string> logError = null)
        {
            _entryType = entryType;
            _isUefi = isUefi;
            _logCommand = logCommand;
            _logOutput = logOutput;
            _logError = logError;
            _applicationType = MapTypeToApplication(entryType);

            InitializeComponent();
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            PopulateParams();
        }

        /// <summary>
        /// 将显示类型映射为 bcdedit 的 /application 参数值（用于 PopulateParams）。
        /// </summary>
        private static string MapTypeToApplication(string type)
        {
            if (type == "Windows Boot Manager") return "bootmgr";
            if (type == "Legacy OS loader (ntldr)") return "ntldr";
            if (type == "Ramdisk Options") return "ramdisk";
            if (type == "Linux/UEFI Boot Loader") return "bootapp";
            return "osloader";
        }

        /// <summary>
        /// 判断当前类型是否使用众所周知的标识符（而非自动生成 GUID）。
        /// </summary>
        private bool IsWellKnownType()
        {
            return _applicationType == "bootmgr"
                || _applicationType == "ntldr"
                || _applicationType == "ramdisk";
        }

        /// <summary>
        /// 判断参数名是否为驱动器选择参数。
        /// </summary>
        private static bool IsDriveParam(string paramName)
        {
            return string.Equals(paramName, "device", StringComparison.OrdinalIgnoreCase)
                || string.Equals(paramName, "osdevice", StringComparison.OrdinalIgnoreCase)
                || string.Equals(paramName, "ramdisksdidevice", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 根据类型动态生成参数输入行。
        /// </summary>
        private void PopulateParams()
        {
            this.Text = string.Format("自定义创建 - {0}", _entryType);
            typeValueLabel.Text = _entryType;

            // 清空默认行
            paramsTableLayout.Controls.Clear();
            paramsTableLayout.RowStyles.Clear();
            paramsTableLayout.RowCount = 0;

            // 第一行：名称（标签在上，输入框在下）
            AddFieldLabel("名称：");
            int nameRow = paramsTableLayout.RowCount;
            paramsTableLayout.RowCount = nameRow + 1;
            paramsTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            nameTextBox = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Margin = new Padding(0, 3, 0, 16),
                Size = new System.Drawing.Size(510, 21)
            };
            paramsTableLayout.Controls.Add(nameTextBox, 0, nameRow);

            // 根据类型添加参数行
            switch (_applicationType)
            {
                case "osloader":
                    AddDriveCombo("device", "引导加载器所在分区：", string.Format("partition={0}", GetSystemDrive()));
                    AddDriveCombo("osdevice", "Windows 所在分区：", string.Format("partition={0}", GetSystemDrive()));
                    AddParamRow("path", "引导文件路径：", _isUefi
                        ? @"\Windows\system32\boot\winload.efi"
                        : @"\Windows\system32\winload.exe");
                    AddParamRow("systemroot", "Windows 目录：", "\\Windows");
                    AddLocaleCombo("locale", "语言区域：",
                        System.Globalization.CultureInfo.CurrentCulture.Name);
                    break;

                case "bootmgr":
                    AddDriveCombo("device", "设备分区：", string.Format("partition={0}", GetSystemDrive()));
                    break;

                case "ntldr":
                    AddDriveCombo("device", "设备分区：", string.Format("partition={0}", GetSystemDrive()));
                    break;

                case "ramdisk":
                    AddDriveCombo("device", "设备分区：", string.Format("partition={0}", GetSystemDrive()));
                    AddDriveCombo("ramdisksdidevice", "SDI 文件分区：", string.Format("partition={0}", GetSystemDrive()));
                    AddParamRow("ramdisksdipath", "SDI 镜像路径：", @"\boot\boot.sdi");
                    break;

                case "bootapp":
                    // 自动挂载 ESP
                    _espInfo = EspHelper.FindAndMountEsp();

                    string espDrive = _espInfo != null ? _espInfo.DriveLetter : "未找到 ESP";
                    AddDriveCombo("device", "EFI 系统分区（ESP）：",
                        string.Format("partition={0}", espDrive));

                    // 固定 device 下拉框不让用户修改
                    ComboBox deviceComboBox = null;
                    if (_espInfo != null)
                        _paramCombos.TryGetValue("device", out deviceComboBox);
                    if (deviceComboBox != null)
                        deviceComboBox.Enabled = false;

                    // loadoptions：用户输入目标引导文件路径（带浏览按钮）
                    AddPathWithBrowseRow("loadoptions", "目标引导文件路径（*.efi）：");

                    if (_espInfo == null)
                    {
                        createButton.Enabled = false;
                        MessageBox.Show("未检测到 EFI 系统分区（ESP），请确认您的系统使用 UEFI 引导。",
                            "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;
            }

            // 刷新
            paramsTableLayout.PerformLayout();
        }

        /// <summary>
        /// 添加一行标签。
        /// </summary>
        private void AddFieldLabel(string text)
        {
            int rowIndex = paramsTableLayout.RowCount;
            paramsTableLayout.RowCount = rowIndex + 1;
            paramsTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
            paramsTableLayout.Controls.Add(new Label
            {
                Text = text,
                AutoSize = true,
                Margin = new Padding(0)
            }, 0, rowIndex);
        }

        /// <summary>
        /// 向 paramsTableLayout 添加一个参数：标签行 + 输入框行。
        /// </summary>
        private void AddParamRow(string paramName, string labelText, string defaultValue)
        {
            AddFieldLabel(labelText);

            int rowIndex = paramsTableLayout.RowCount;
            paramsTableLayout.RowCount = rowIndex + 1;
            paramsTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

            TextBox textBox = new TextBox
            {
                Text = defaultValue,
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Margin = new Padding(0, 3, 0, 16),
                Size = new System.Drawing.Size(510, 21),
                Tag = paramName
            };

            paramsTableLayout.Controls.Add(textBox, 0, rowIndex);
            _paramTextBoxes[paramName] = textBox;
        }

        /// <summary>
        /// 添加一行语言区域选择下拉框。
        /// </summary>
        private void AddLocaleCombo(string paramName, string labelText, string defaultValue)
        {
            AddFieldLabel(labelText);

            int rowIndex = paramsTableLayout.RowCount;
            paramsTableLayout.RowCount = rowIndex + 1;
            paramsTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

            ComboBox combo = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Margin = new Padding(0, 3, 0, 16)
            };
            combo.Items.AddRange(new object[] { "zh-CN (简体中文)", "en-US (English)", "ja-JP (日本語)", "ko-KR (한국어)", "zh-TW (繁體中文)" });
            // 匹配默认值
            for (int i = 0; i < combo.Items.Count; i++)
            {
                if (combo.Items[i].ToString().StartsWith(defaultValue, StringComparison.OrdinalIgnoreCase))
                {
                    combo.SelectedIndex = i;
                    break;
                }
            }
            if (combo.SelectedIndex < 0)
                combo.SelectedIndex = 0;

            combo.Tag = paramName;
            // 选中项变化时同步到字典
            combo.SelectedIndexChanged += (s, e) =>
            {
                string selected = combo.SelectedItem.ToString();
                int space = selected.IndexOf(' ');
                string code = space > 0 ? selected.Substring(0, space) : selected;
                _paramTextBoxes[paramName] = null; // 占位
                // 用 Tag 存实际值
                combo.Tag = code;
            };
            // 存初始值
            string initSelected = combo.SelectedItem.ToString();
            int initSpace = initSelected.IndexOf(' ');
            combo.Tag = initSpace > 0 ? initSelected.Substring(0, initSpace) : initSelected;

            paramsTableLayout.Controls.Add(combo, 0, rowIndex);
            _paramTextBoxes[paramName] = null;
            _paramCombos[paramName] = combo;
        }

        /// <summary>
        /// 添加一行带「浏览」按钮的路径输入。
        /// 仅 bootsector 类型使用。
        /// </summary>
        private void AddPathWithBrowseRow(string paramName, string labelText)
        {
            AddFieldLabel(labelText);

            int rowIndex = paramsTableLayout.RowCount;
            paramsTableLayout.RowCount = rowIndex + 1;
            paramsTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));

            // 用 TableLayoutPanel 把输入框和按钮放一行，文本框自动拉伸
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 3, 0, 16),  // 与 AddParamRow 的文本框边距一致
                ColumnCount = 2,
                RowCount = 1
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85F));

            TextBox textBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 6, 0),
                Tag = paramName
            };

            Button browseButton = new Button
            {
                Text = "浏览...",
                Width = 75,
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                UseVisualStyleBackColor = true
            };

            browseButton.Click += (s, e) =>
            {
                // 确定当前 device 选的盘符，作为浏览的起始目录
                string deviceValue = GetParamValue("device");
                string driveLetter = ExtractDriveLetter(deviceValue);
                string initialDir = driveLetter != null
                    ? driveLetter + @":\EFI"
                    : Environment.SystemDirectory;

                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Title = "选择 EFI 引导文件";
                    dialog.Filter = "EFI 引导文件 (*.efi)|*.efi|所有文件 (*.*)|*.*";
                    dialog.InitialDirectory = initialDir;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string fullPath = dialog.FileName;
                        if (driveLetter != null && fullPath.StartsWith(driveLetter + ":", StringComparison.OrdinalIgnoreCase))
                        {
                            textBox.Text = fullPath.Substring(2);
                        }
                        else
                        {
                            textBox.Text = fullPath;
                        }
                    }
                }
            };

            panel.Controls.Add(textBox);
            panel.Controls.Add(browseButton);
            paramsTableLayout.Controls.Add(panel, 0, rowIndex);
            _paramTextBoxes[paramName] = textBox;
        }

        /// <summary>
        /// 添加一行驱动器下拉选择框。
        /// </summary>
        private void AddDriveCombo(string paramName, string labelText, string defaultDrive)
        {
            AddFieldLabel(labelText);

            int rowIndex = paramsTableLayout.RowCount;
            paramsTableLayout.RowCount = rowIndex + 1;
            paramsTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

            ComboBox combo = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Margin = new Padding(0, 3, 0, 16)
            };

            // 枚举所有可用本地驱动器
            string defaultLetter = defaultDrive.Replace("partition=", "").Replace(":", "").Trim().ToUpperInvariant();
            int selectedIndex = 0;
            try
            {
                System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
                int idx = 0;
                foreach (System.IO.DriveInfo di in drives)
                {
                    if (di.DriveType == System.IO.DriveType.Fixed || di.DriveType == System.IO.DriveType.Removable)
                    {
                        string label = string.IsNullOrWhiteSpace(di.VolumeLabel)
                            ? di.Name.TrimEnd('\\')
                            : string.Format("{0} ({1})", di.Name.TrimEnd('\\'), di.VolumeLabel);
                        combo.Items.Add(label);
                        if (di.Name.StartsWith(defaultLetter + ":", StringComparison.OrdinalIgnoreCase))
                            selectedIndex = idx;
                        idx++;
                    }
                }
            }
            catch
            {
                // 枚举失败时至少添加系统盘
                combo.Items.Add(defaultDrive.Replace("partition=", ""));
            }

            if (combo.Items.Count == 0)
                combo.Items.Add(defaultDrive.Replace("partition=", ""));

            combo.SelectedIndex = selectedIndex;
            combo.Tag = paramName;

            paramsTableLayout.Controls.Add(combo, 0, rowIndex);
            _paramTextBoxes[paramName] = null;
            _paramCombos[paramName] = combo;
        }

        /// <summary>
        /// 获取当前系统盘符（如 "C:"）。
        /// </summary>
        private static string GetSystemDrive()
        {
            string sysDrive = Environment.SystemDirectory; // 如 C:\Windows\System32
            if (sysDrive.Length >= 2)
                return sysDrive.Substring(0, 2); // "C:"
            return "C:";
        }

        /// <summary>
        /// 执行 bcdedit 命令，并将命令和输出回显到主窗口底部日志框。
        /// </summary>
        private async Task<BcdResult> RunLoggedCommandAsync(string arguments)
        {
            if (_logCommand != null)
                _logCommand("bcdedit " + arguments);

            BcdResult result = await BcdHelper.ExecuteBcdEditAsync(arguments);

            if (result.ExitCode == 0)
            {
                if (!string.IsNullOrWhiteSpace(result.Output) && _logOutput != null)
                    _logOutput(result.Output);
            }
            else
            {
                if (_logError != null)
                    _logError(string.IsNullOrWhiteSpace(result.Error) ? result.Output : result.Error);
            }

            return result;
        }

        /// <summary>
        /// 执行 bcdedit /set 命令，失败时弹出提示并返回 false。
        /// </summary>
        private async Task<bool> SetSingleParam(string guid, string paramName, string value)
        {
            BcdResult r = await RunLoggedCommandAsync(
                string.Format("/set {0} {1} {2}", guid, paramName, value));
            if (r.ExitCode != 0)
            {
                string err = string.IsNullOrWhiteSpace(r.Error) ? r.Output : r.Error;
                MessageBox.Show(string.Format("设置 {0} 失败：\n{1}\n\n部分参数可能未生效。", paramName, err),
                    "参数设置失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 从 partition=X: 或单独 X: 格式中提取盘符（如 "D"）。
        /// </summary>
        private static string ExtractDriveLetter(string driveValue)
        {
            if (string.IsNullOrWhiteSpace(driveValue))
                return null;
            // "partition=X:" 格式
            Match m = Regex.Match(driveValue, @"partition=([A-Za-z]):");
            if (m.Success)
                return m.Groups[1].Value.ToUpperInvariant();
            // "X:" 或 "X" 裸格式
            m = Regex.Match(driveValue, @"^([A-Za-z]):?$");
            return m.Success ? m.Groups[1].Value.ToUpperInvariant() : null;
        }

        /// <summary>
        /// 获取指定参数名对应的当前值。
        /// 驱动器参数自动格式化为 "partition=X:"。
        /// </summary>
        private string GetParamValue(string paramName)
        {
            ComboBox combo;
            TextBox box;
            string rawValue = null;
            if (_paramCombos.TryGetValue(paramName, out combo) && combo != null)
            {
                string selected = combo.SelectedItem.ToString();
                int space = selected.IndexOf(' ');
                rawValue = space > 0 ? selected.Substring(0, space) : selected;
            }
            else if (_paramTextBoxes.TryGetValue(paramName, out box) && box != null)
            {
                rawValue = box.Text.Trim();
            }

            if (rawValue == null)
                return null;

            // 驱动器参数自动补全 partition= 前缀
            if (IsDriveParam(paramName) && !rawValue.StartsWith("partition=", StringComparison.OrdinalIgnoreCase))
                return "partition=" + rawValue;

            return rawValue;
        }

        /// <summary>
        /// 创建前验证分区和文件路径是否存在，返回错误信息列表（空列表表示全部通过）。
        /// </summary>
        private List<string> ValidateInputs()
        {
            List<string> errors = new List<string>();
            HashSet<string> checkedDrives = new HashSet<string>();

            foreach (string paramName in _paramTextBoxes.Keys)
            {
                TextBox tb = _paramTextBoxes[paramName];
                string value = (tb != null) ? tb.Text.Trim() : null;
                if (string.IsNullOrEmpty(value))
                    continue;

                // 检查 device / osdevice / ramdisksdidevice 中的分区
                if (IsDriveParam(paramName))
                {
                    // 从 ComboBox 取实际选中的值（裸盘符，如 "C:"）
                    string driveLetter = ExtractDriveLetter(GetParamValue(paramName));
                    if (driveLetter != null && checkedDrives.Add(driveLetter))
                    {
                        if (!System.IO.Directory.Exists(driveLetter + @":\"))
                            errors.Add(string.Format("分区 {0}:\\ 不存在或无法访问（{1}）", driveLetter, paramName));
                    }
                }

                // 检查 path / ramdisksdipath / loadoptions 等文件路径
                if (string.Equals(paramName, "path", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(paramName, "ramdisksdipath", StringComparison.OrdinalIgnoreCase)
                    || (_applicationType == "bootapp" && string.Equals(paramName, "loadoptions", StringComparison.OrdinalIgnoreCase)))
                {
                    // bootapp 类型：检查 .efi 扩展名
                    if (_applicationType == "bootapp")
                    {
                        if (!value.EndsWith(".efi", StringComparison.OrdinalIgnoreCase))
                        {
                            errors.Add("目标引导文件必须以 .efi 结尾");
                            continue;
                        }
                    }

                    string deviceValue = GetParamValue("device");
                    if (string.IsNullOrEmpty(deviceValue))
                        deviceValue = GetParamValue("osdevice");
                    string driveLetter = ExtractDriveLetter(deviceValue);
                    if (driveLetter != null)
                    {
                        string fullPath = driveLetter + ":" + value;
                        if (!System.IO.File.Exists(fullPath))
                            errors.Add(string.Format("文件不存在：{0}（{1}）", fullPath, paramName));
                    }
                }
            }

            return errors;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            EspHelper.UnmountIfOurs(_espInfo);
        }

        /// <summary>
        /// 将内置的 efiloader.efi 部署到 ESP 的 \EFI\pan\ 目录下。
        /// 如果文件已存在则跳过。
        /// </summary>
        private bool DeployEfiLoader()
        {
            if (_espInfo == null) return false;

            string targetDir = Path.Combine(_espInfo.DriveLetter + @"\", @"EFI\pan");
            string targetFile = Path.Combine(targetDir, "efiloader.efi");

                if (File.Exists(targetFile))
                {
                    if (_logCommand != null) _logCommand("efiloader.efi 已存在于 " + targetFile);
                    return true;
                }

            try
            {
                Directory.CreateDirectory(targetDir);
                using (var stream = System.Reflection.Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("panbcdedit.Resources.efiloader.efi"))
                {
                    if (stream == null)
                    {
                        MessageBox.Show("无法加载内置的 efiloader.efi 资源，文件可能未正确嵌入到程序中。",
                            "资源加载失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    using (var fs = new FileStream(targetFile, FileMode.Create, FileAccess.Write))
                    {
                        stream.CopyTo(fs);
                    }
                }
                if (_logCommand != null) _logCommand("已部署 efiloader.efi 到 " + targetFile);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("部署 efiloader.efi 失败：" + ex.Message,
                    "部署失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private async void CreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                string description = nameTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(description))
                {
                    MessageBox.Show("请输入启动项名称。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (_paramTextBoxes.Count == 0)
                {
                    MessageBox.Show("参数配置异常，请重试。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string summary = string.Format("创建启动项\n类型：{0}\n名称：{1}", _entryType, description);
                if (MessageBox.Show(summary, "确认创建", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;

                // 前置校验：分区和文件路径是否存在
                List<string> validationErrors = ValidateInputs();
                if (validationErrors.Count > 0)
                {
                    string msg = "以下参数存在问题：\n\n" + string.Join("\n", validationErrors);
                    MessageBox.Show(msg, "参数验证失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // bootapp 类型：先确保 efiloader.efi 已部署到 ESP
                if (_applicationType == "bootapp")
                {
                    if (!DeployEfiLoader())
                        return;
                }

                // 创建条目（根据类型使用不同的命令语法）
                string createCmd;
                string knownId = null;
                string wellKnownId = null;
                bool isWellKnown = IsWellKnownType();
                if (isWellKnown)
                {
                    switch (_applicationType)
                    {
                        case "bootmgr":
                            wellKnownId = "{bootmgr}";
                            createCmd = string.Format("/create {0} /d \"{1}\"", wellKnownId, description);
                            break;
                        case "ntldr":
                            wellKnownId = "{ntldr}";
                            createCmd = string.Format("/create {0} /d \"{1}\"", wellKnownId, description);
                            break;
                        default: // ramdisk
                            wellKnownId = "{ramdiskoptions}";
                            createCmd = "/create {ramdiskoptions}";
                            break;
                    }
                    knownId = wellKnownId;
                }
                else
                {
                    createCmd = string.Format("/create /d \"{0}\" /application {1}", description, _applicationType);
                }

                BcdResult result = await RunLoggedCommandAsync(createCmd);

                if (result.ExitCode != 0)
                {
                    string errMsg = string.IsNullOrWhiteSpace(result.Error) ? result.Output : result.Error;
                    MessageBox.Show(string.Format("创建启动项失败：\n{0}", errMsg), "创建失败",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 提取标识符
                string createdGuid;
                if (knownId != null)
                {
                    // bootmgr/ntldr/ramdisk 使用众所周知的标识符，直接从预知值获取
                    createdGuid = knownId;
                }
                else
                {
                    // osloader 类型从输出中提取新生成的 GUID
                    Match match = Regex.Match(result.Output, @"({[0-9a-fA-F\-]+})");
                    createdGuid = match.Success ? match.Value : null;
                }

                if (string.IsNullOrWhiteSpace(createdGuid))
                {
                    MessageBox.Show("创建成功但无法获取 GUID，请刷新列表查看。", "警告",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                // 逐条设置参数（遍历所有参数名，从 TextBox 或 ComboBox 取实际值）
                bool allOk = true;
                HashSet<string> allParamNames = new HashSet<string>(_paramTextBoxes.Keys, StringComparer.OrdinalIgnoreCase);
                foreach (string key in _paramCombos.Keys)
                    allParamNames.Add(key);

                foreach (string paramName in allParamNames)
                {
                    string value = null;
                    ComboBox combo;
                    TextBox box;
                    // 优先取 ComboBox 的值
                    if (_paramCombos.TryGetValue(paramName, out combo) && combo != null)
                    {
                        string selected = combo.SelectedItem.ToString();
                        int space = selected.IndexOf(' ');
                        value = space > 0 ? selected.Substring(0, space) : selected;
                    }
                    else if (_paramTextBoxes.TryGetValue(paramName, out box) && box != null)
                    {
                        value = box.Text.Trim();
                    }

                    if (string.IsNullOrEmpty(value))
                        continue;

                    // 驱动器参数自动补全 partition= 前缀
                    if (IsDriveParam(paramName) && !value.StartsWith("partition=", StringComparison.OrdinalIgnoreCase))
                        value = "partition=" + value;

                    if (!await SetSingleParam(createdGuid, paramName, value))
                        allOk = false;
                }

                // bootapp 特殊参数：固定 path 指向 efiloader.efi + 关闭完整性检查
                if (_applicationType == "bootapp" && allOk)
                {
                    string efiLoaderPath = @"\EFI\pan\efiloader.efi";

                    if (!await SetSingleParam(createdGuid, "path", efiLoaderPath))
                        allOk = false;

                    if (allOk && !await SetSingleParam(createdGuid, "nointegritychecks", "Yes"))
                        allOk = false;
                }

                // 将新条目添加到启动菜单显示顺序末尾，否则 /enum 看不到
                if (allOk)
                {
                    BcdResult displayResult = await RunLoggedCommandAsync(
                        string.Format("/displayorder {0} /addlast", createdGuid));
                    allOk = displayResult.ExitCode == 0;
                    if (!allOk)
                    {
                        string err = string.IsNullOrWhiteSpace(displayResult.Error) ? displayResult.Output : displayResult.Error;
                        MessageBox.Show(string.Format("添加新条目到启动菜单失败：\n{0}\n\n条目已存在但需手动添加。", err),
                            "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                if (allOk)
                {
                    MessageBox.Show(string.Format("启动项创建成功！\nGUID：{0}", createdGuid),
                        "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult cleanupChoice = MessageBox.Show(
                        string.Format("启动项已创建但部分参数设置失败，是否删除此残次条目？\nGUID：{0}", createdGuid),
                        "创建不完整", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (cleanupChoice == DialogResult.Yes)
                    {
                        await RunLoggedCommandAsync(string.Format("/delete {0} /f", createdGuid));
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("创建启动项时发生异常：{0}", ex.Message),
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
