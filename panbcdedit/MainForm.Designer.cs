namespace panbcdedit
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // 菜单栏
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem systemMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shutdownMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartFirmwareMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartRecoveryMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guideMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;


        // 主容器（上方 TabControl + 下方日志）
        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.RichTextBox logRichTextBox;

        // 选项卡 1：启动项总览
        private System.Windows.Forms.TabPage tabPageOverview;
        private System.Windows.Forms.Panel overviewPanel;
        private System.Windows.Forms.TableLayoutPanel overviewTableLayout;
        private System.Windows.Forms.RichTextBox overviewRichTextBox;
        private System.Windows.Forms.Button overviewRefreshButton;

        // 选项卡 2：启动项管理
        private System.Windows.Forms.TabPage tabPageManagement;
        private System.Windows.Forms.Panel managementPanel;
        private System.Windows.Forms.TableLayoutPanel managementTableLayout;
        private System.Windows.Forms.CheckBox showSystemEntriesCheckBox;
        private System.Windows.Forms.TableLayoutPanel managementInnerTableLayout;
        private System.Windows.Forms.DataGridView entriesDataGridView;
        private System.Windows.Forms.FlowLayoutPanel managementButtonsFlow;
        private System.Windows.Forms.Button copyEntryButton;
        private System.Windows.Forms.Button renameEntryButton;
        private System.Windows.Forms.Button deleteEntryButton;
        private System.Windows.Forms.GroupBox createGroupBox;
        private System.Windows.Forms.TableLayoutPanel createTableLayout;
        private System.Windows.Forms.TextBox createNameTextBox;
        private System.Windows.Forms.Label createSepLabel;
        private System.Windows.Forms.ComboBox createTypeComboBox;
        private System.Windows.Forms.ComboBox createDriveComboBox;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button advancedCreateButton;

        // 选项卡 3：常见启动项设置
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.TableLayoutPanel settingsTableLayout;
        private System.Windows.Forms.GroupBox defaultEntryGroupBox;
        private System.Windows.Forms.TableLayoutPanel defaultEntryFlow;
        private System.Windows.Forms.ComboBox defaultEntryComboBox;
        private System.Windows.Forms.Button applyDefaultEntryButton;
        private System.Windows.Forms.GroupBox timeoutGroupBox;
        private System.Windows.Forms.TableLayoutPanel timeoutFlow;
        private System.Windows.Forms.NumericUpDown timeoutNumericUpDown;
        private System.Windows.Forms.Label timeoutLabel;
        private System.Windows.Forms.Button applyTimeoutButton;
        private System.Windows.Forms.GroupBox displayOrderGroupBox;
        private System.Windows.Forms.TableLayoutPanel displayOrderTableLayout;
        private System.Windows.Forms.ListBox displayOrderListBox;
        private System.Windows.Forms.FlowLayoutPanel displayOrderButtonsFlow;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button applyDisplayOrderButton;
        private System.Windows.Forms.GroupBox forceMenuGroupBox;
        private System.Windows.Forms.TableLayoutPanel forceMenuFlow;
        private System.Windows.Forms.CheckBox forceMenuCheckBox;
        private System.Windows.Forms.Button applyForceMenuButton;
        private System.Windows.Forms.GroupBox exportGroupBox;
        private System.Windows.Forms.TableLayoutPanel exportTableLayout;
        private System.Windows.Forms.TextBox exportPathTextBox;
        private System.Windows.Forms.Button exportBrowseButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.GroupBox importGroupBox;
        private System.Windows.Forms.TableLayoutPanel importTableLayout;
        private System.Windows.Forms.TextBox importPathTextBox;
        private System.Windows.Forms.Button importBrowseButton;
        private System.Windows.Forms.Button importButton;

        // 选项卡 4：高级开关
        private System.Windows.Forms.TabPage tabPageAdvanced;
        private System.Windows.Forms.Panel advancedPanel;
        private System.Windows.Forms.TableLayoutPanel advancedTableLayout;
        private System.Windows.Forms.TableLayoutPanel targetEntryTableLayout;
        private System.Windows.Forms.Label targetEntryLabel;
        private System.Windows.Forms.ComboBox targetEntryComboBox;
        private System.Windows.Forms.TableLayoutPanel advancedSwitchesTableLayout;
        private System.Windows.Forms.GroupBox debugTestsGroupBox;
        private System.Windows.Forms.FlowLayoutPanel debugTestsFlow;
        private System.Windows.Forms.CheckBox testsigningCheckBox;
        private System.Windows.Forms.CheckBox debugCheckBox;
        private System.Windows.Forms.CheckBox bootdebugCheckBox;
        private System.Windows.Forms.GroupBox securityDriversGroupBox;
        private System.Windows.Forms.FlowLayoutPanel securityDriversFlow;
        private System.Windows.Forms.CheckBox nointegritychecksCheckBox;
        private System.Windows.Forms.CheckBox disableelamdriversCheckBox;
        private System.Windows.Forms.GroupBox displayMemoryGroupBox;
        private System.Windows.Forms.FlowLayoutPanel displayMemoryFlow;
        private System.Windows.Forms.CheckBox paeCheckBox;
        private System.Windows.Forms.CheckBox nxCheckBox;
        private System.Windows.Forms.CheckBox novesaCheckBox;
        private System.Windows.Forms.GroupBox virtualizationGroupBox;
        private System.Windows.Forms.FlowLayoutPanel virtualizationFlow;
        private System.Windows.Forms.Label hypervisorLabel;
        private System.Windows.Forms.ComboBox hypervisorComboBox;
        private System.Windows.Forms.TableLayoutPanel advancedPreviewTableLayout;
        private System.Windows.Forms.TextBox commandPreviewTextBox;
        private System.Windows.Forms.Button applyAdvancedButton;


        // 选项卡 5：引导修复
        private System.Windows.Forms.TabPage tabPageRepair;
        private System.Windows.Forms.Panel repairPanel;
        private System.Windows.Forms.TableLayoutPanel repairTableLayout;
        private System.Windows.Forms.Button unlockRepairButton;
        private System.Windows.Forms.GroupBox repairConfigGroupBox;
        private System.Windows.Forms.TableLayoutPanel repairConfigLayout;
        private System.Windows.Forms.ComboBox languageComboBox;
        private System.Windows.Forms.TextBox windowsDirTextBox;
        private System.Windows.Forms.Button windowsDirBrowseButton;
        private System.Windows.Forms.TableLayoutPanel windowsDirPanel;
        private System.Windows.Forms.ComboBox firmwareComboBox;
        private System.Windows.Forms.CheckBox overwriteCheckBox;
        private System.Windows.Forms.Button repairButton;
        private System.Windows.Forms.RichTextBox repairResultTextBox;
        private System.Windows.Forms.Label repairResultLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shutdownMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartFirmwareMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartRecoveryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guideMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();


            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();

            this.tabPageOverview = new System.Windows.Forms.TabPage();
            this.overviewPanel = new System.Windows.Forms.Panel();
            this.overviewTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.overviewRichTextBox = new System.Windows.Forms.RichTextBox();
            this.overviewRefreshButton = new System.Windows.Forms.Button();

            this.tabPageManagement = new System.Windows.Forms.TabPage();
            this.managementPanel = new System.Windows.Forms.Panel();
            this.managementTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.showSystemEntriesCheckBox = new System.Windows.Forms.CheckBox();
            this.managementInnerTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.entriesDataGridView = new System.Windows.Forms.DataGridView();
            this.managementButtonsFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.copyEntryButton = new System.Windows.Forms.Button();
            this.renameEntryButton = new System.Windows.Forms.Button();
            this.deleteEntryButton = new System.Windows.Forms.Button();
            this.createGroupBox = new System.Windows.Forms.GroupBox();
            this.createTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.createNameTextBox = new System.Windows.Forms.TextBox();
            this.createSepLabel = new System.Windows.Forms.Label();
            this.createTypeComboBox = new System.Windows.Forms.ComboBox();
            this.createDriveComboBox = new System.Windows.Forms.ComboBox();
            this.createButton = new System.Windows.Forms.Button();
            this.advancedCreateButton = new System.Windows.Forms.Button();

            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.settingsTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.defaultEntryGroupBox = new System.Windows.Forms.GroupBox();
            this.defaultEntryFlow = new System.Windows.Forms.TableLayoutPanel();
            this.defaultEntryComboBox = new System.Windows.Forms.ComboBox();
            this.applyDefaultEntryButton = new System.Windows.Forms.Button();
            this.timeoutGroupBox = new System.Windows.Forms.GroupBox();
            this.timeoutFlow = new System.Windows.Forms.TableLayoutPanel();
            this.timeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.timeoutLabel = new System.Windows.Forms.Label();
            this.applyTimeoutButton = new System.Windows.Forms.Button();
            this.displayOrderGroupBox = new System.Windows.Forms.GroupBox();
            this.displayOrderTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.displayOrderListBox = new System.Windows.Forms.ListBox();
            this.displayOrderButtonsFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.moveUpButton = new System.Windows.Forms.Button();
            this.moveDownButton = new System.Windows.Forms.Button();
            this.applyDisplayOrderButton = new System.Windows.Forms.Button();
            this.forceMenuGroupBox = new System.Windows.Forms.GroupBox();
            this.forceMenuFlow = new System.Windows.Forms.TableLayoutPanel();
            this.forceMenuCheckBox = new System.Windows.Forms.CheckBox();
            this.applyForceMenuButton = new System.Windows.Forms.Button();

            this.tabPageAdvanced = new System.Windows.Forms.TabPage();
            this.advancedPanel = new System.Windows.Forms.Panel();
            this.advancedTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.targetEntryTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.targetEntryLabel = new System.Windows.Forms.Label();
            this.targetEntryComboBox = new System.Windows.Forms.ComboBox();
            this.advancedSwitchesTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.debugTestsGroupBox = new System.Windows.Forms.GroupBox();
            this.debugTestsFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.testsigningCheckBox = new System.Windows.Forms.CheckBox();
            this.debugCheckBox = new System.Windows.Forms.CheckBox();
            this.bootdebugCheckBox = new System.Windows.Forms.CheckBox();
            this.securityDriversGroupBox = new System.Windows.Forms.GroupBox();
            this.securityDriversFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.nointegritychecksCheckBox = new System.Windows.Forms.CheckBox();
            this.disableelamdriversCheckBox = new System.Windows.Forms.CheckBox();
            this.displayMemoryGroupBox = new System.Windows.Forms.GroupBox();
            this.displayMemoryFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.paeCheckBox = new System.Windows.Forms.CheckBox();
            this.nxCheckBox = new System.Windows.Forms.CheckBox();
            this.novesaCheckBox = new System.Windows.Forms.CheckBox();
            this.virtualizationGroupBox = new System.Windows.Forms.GroupBox();
            this.virtualizationFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.hypervisorLabel = new System.Windows.Forms.Label();
            this.hypervisorComboBox = new System.Windows.Forms.ComboBox();
            this.advancedPreviewTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.commandPreviewTextBox = new System.Windows.Forms.TextBox();
            this.applyAdvancedButton = new System.Windows.Forms.Button();

            this.tabPageRepair = new System.Windows.Forms.TabPage();
            this.repairPanel = new System.Windows.Forms.Panel();
            this.repairTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.unlockRepairButton = new System.Windows.Forms.Button();
            this.languageComboBox = new System.Windows.Forms.ComboBox();
            this.windowsDirTextBox = new System.Windows.Forms.TextBox();
            this.windowsDirBrowseButton = new System.Windows.Forms.Button();
            this.repairConfigGroupBox = new System.Windows.Forms.GroupBox();
            this.repairConfigLayout = new System.Windows.Forms.TableLayoutPanel();
            this.windowsDirPanel = new System.Windows.Forms.TableLayoutPanel();
            this.firmwareComboBox = new System.Windows.Forms.ComboBox();
            this.overwriteCheckBox = new System.Windows.Forms.CheckBox();
            this.repairButton = new System.Windows.Forms.Button();
            this.repairResultTextBox = new System.Windows.Forms.RichTextBox();
            this.repairResultLabel = new System.Windows.Forms.Label();

            this.exportGroupBox = new System.Windows.Forms.GroupBox();
            this.exportTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.exportPathTextBox = new System.Windows.Forms.TextBox();
            this.exportBrowseButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.importGroupBox = new System.Windows.Forms.GroupBox();
            this.importTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.importPathTextBox = new System.Windows.Forms.TextBox();
            this.importBrowseButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();

            this.menuStrip.SuspendLayout();
            this.mainTableLayout.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.overviewPanel.SuspendLayout();
            this.overviewTableLayout.SuspendLayout();
            this.managementPanel.SuspendLayout();
            this.managementTableLayout.SuspendLayout();
            this.managementInnerTableLayout.SuspendLayout();
            this.managementButtonsFlow.SuspendLayout();
            this.createGroupBox.SuspendLayout();
            this.createTableLayout.SuspendLayout();
            this.settingsPanel.SuspendLayout();
            this.settingsTableLayout.SuspendLayout();
            this.defaultEntryGroupBox.SuspendLayout();
            this.defaultEntryFlow.SuspendLayout();
            this.timeoutGroupBox.SuspendLayout();
            this.timeoutFlow.SuspendLayout();
            this.displayOrderGroupBox.SuspendLayout();
            this.displayOrderTableLayout.SuspendLayout();
            this.displayOrderButtonsFlow.SuspendLayout();
            this.forceMenuGroupBox.SuspendLayout();
            this.forceMenuFlow.SuspendLayout();
            this.advancedTableLayout.SuspendLayout();
            this.targetEntryTableLayout.SuspendLayout();
            this.advancedSwitchesTableLayout.SuspendLayout();
            this.debugTestsFlow.SuspendLayout();
            this.securityDriversFlow.SuspendLayout();
            this.displayMemoryFlow.SuspendLayout();
            this.virtualizationFlow.SuspendLayout();
            this.advancedPreviewTableLayout.SuspendLayout();
            this.repairPanel.SuspendLayout();
            this.repairTableLayout.SuspendLayout();
            this.repairConfigGroupBox.SuspendLayout();
            this.repairConfigLayout.SuspendLayout();
            this.windowsDirPanel.SuspendLayout();
            this.exportGroupBox.SuspendLayout();
            this.exportTableLayout.SuspendLayout();
            this.importGroupBox.SuspendLayout();
            this.importTableLayout.SuspendLayout();
            this.SuspendLayout();

            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.fileMenuItem,
                this.systemMenuItem,
                this.helpMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(960, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";

            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.exportMenuItem,
                this.exitMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(58, 20);
            this.fileMenuItem.Text = "文件(&F)";

            // 
            // exportMenuItem
            // 
            this.exportMenuItem.Name = "exportMenuItem";
            this.exportMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportMenuItem.Text = "导出当前配置";

            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitMenuItem.Text = "退出";

            // 
            // systemMenuItem
            // 
            this.systemMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.shutdownMenuItem,
                this.restartMenuItem,
                this.restartFirmwareMenuItem,
                this.restartRecoveryMenuItem});
            this.systemMenuItem.Name = "systemMenuItem";
            this.systemMenuItem.Size = new System.Drawing.Size(58, 20);
            this.systemMenuItem.Text = "系统(&S)";

            // 
            // shutdownMenuItem
            // 
            this.shutdownMenuItem.Name = "shutdownMenuItem";
            this.shutdownMenuItem.Size = new System.Drawing.Size(220, 22);
            this.shutdownMenuItem.Text = "关机";

            // 
            // restartMenuItem
            // 
            this.restartMenuItem.Name = "restartMenuItem";
            this.restartMenuItem.Size = new System.Drawing.Size(220, 22);
            this.restartMenuItem.Text = "重启";

            // 
            // restartFirmwareMenuItem
            // 
            this.restartFirmwareMenuItem.Name = "restartFirmwareMenuItem";
            this.restartFirmwareMenuItem.Size = new System.Drawing.Size(220, 22);
            this.restartFirmwareMenuItem.Text = "重启并进入 BIOS/UEFI";

            // 
            // restartRecoveryMenuItem
            // 
            this.restartRecoveryMenuItem.Name = "restartRecoveryMenuItem";
            this.restartRecoveryMenuItem.Size = new System.Drawing.Size(220, 22);
            this.restartRecoveryMenuItem.Text = "重启并进入 Windows 修复界面";

            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.guideMenuItem,
                this.aboutMenuItem});

            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(61, 20);
            this.helpMenuItem.Text = "帮助(&H)";

            // 
            // guideMenuItem
            // 
            this.guideMenuItem.Name = "guideMenuItem";
            this.guideMenuItem.Size = new System.Drawing.Size(180, 22);
            this.guideMenuItem.Text = "新手指南";

            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutMenuItem.Text = "关于";

            // 
            // mainTableLayout
            // 
            this.mainTableLayout.ColumnCount = 1;
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.Controls.Add(this.tabControl, 0, 0);
            this.mainTableLayout.Controls.Add(this.logRichTextBox, 0, 1);
            this.mainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayout.Location = new System.Drawing.Point(0, 24);
            this.mainTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.RowCount = 2;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.mainTableLayout.Size = new System.Drawing.Size(960, 676);
            this.mainTableLayout.TabIndex = 1;

            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageOverview);
            this.tabControl.Controls.Add(this.tabPageManagement);
            this.tabControl.Controls.Add(this.tabPageSettings);
            this.tabControl.Controls.Add(this.tabPageAdvanced);
            this.tabControl.Controls.Add(this.tabPageRepair);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(3, 3);
            this.tabControl.Margin = new System.Windows.Forms.Padding(5);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(954, 540);
            this.tabControl.TabIndex = 0;

            // 
            // logRichTextBox
            // 
            this.logRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logRichTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logRichTextBox.Location = new System.Drawing.Point(3, 549);
            this.logRichTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.ReadOnly = true;
            this.logRichTextBox.Size = new System.Drawing.Size(954, 124);
            this.logRichTextBox.TabIndex = 1;
            this.logRichTextBox.Text = "";

            // 选项卡 1：启动项总览
            // 
            // tabPageOverview
            // 
            this.tabPageOverview.Controls.Add(this.overviewPanel);
            this.overviewPanel.Controls.Add(this.overviewTableLayout);
            this.tabPageOverview.Location = new System.Drawing.Point(4, 22);
            this.tabPageOverview.Name = "tabPageOverview";
            this.tabPageOverview.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOverview.Size = new System.Drawing.Size(946, 514);
            this.tabPageOverview.TabIndex = 0;
            this.tabPageOverview.Text = "启动项总览";
            this.tabPageOverview.UseVisualStyleBackColor = true;

            // 
            // overviewPanel
            // 
            this.overviewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.overviewPanel.Location = new System.Drawing.Point(3, 3);
            this.overviewPanel.Name = "overviewPanel";
            this.overviewPanel.Size = new System.Drawing.Size(940, 508);
            this.overviewPanel.TabIndex = 0;

            // 
            // overviewTableLayout
            // 
            this.overviewTableLayout.ColumnCount = 2;
            this.overviewTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.overviewTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.overviewTableLayout.Controls.Add(this.overviewRichTextBox, 0, 0);
            this.overviewTableLayout.Controls.Add(this.overviewRefreshButton, 1, 0);
            this.overviewTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.overviewTableLayout.Location = new System.Drawing.Point(0, 0);
            this.overviewTableLayout.Margin = new System.Windows.Forms.Padding(0);

            this.overviewTableLayout.Name = "overviewTableLayout";
            this.overviewTableLayout.RowCount = 1;
            this.overviewTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.overviewTableLayout.Size = new System.Drawing.Size(940, 508);
            this.overviewTableLayout.TabIndex = 0;

            // 
            // overviewRichTextBox
            // 
            this.overviewRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.overviewRichTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.overviewRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.overviewRichTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.overviewRichTextBox.Name = "overviewRichTextBox";
            this.overviewRichTextBox.ReadOnly = true;

            this.overviewRichTextBox.Size = new System.Drawing.Size(840, 502);
            this.overviewRichTextBox.TabIndex = 0;
            this.overviewRichTextBox.Text = "";

            // 
            // overviewRefreshButton
            // 
            this.overviewRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.overviewRefreshButton.AutoSize = false;
            this.overviewRefreshButton.Location = new System.Drawing.Point(849, 3);
            this.overviewRefreshButton.Margin = new System.Windows.Forms.Padding(5);
            this.overviewRefreshButton.Name = "overviewRefreshButton";
            this.overviewRefreshButton.Size = new System.Drawing.Size(88, 27);
            this.overviewRefreshButton.TabIndex = 1;
            this.overviewRefreshButton.Text = "刷新";
            this.overviewRefreshButton.UseVisualStyleBackColor = true;

            // 选项卡 2：启动项管理
            // 
            // tabPageManagement
            // 
            this.tabPageManagement.Controls.Add(this.managementPanel);
            this.managementPanel.Controls.Add(this.managementTableLayout);
            this.tabPageManagement.Location = new System.Drawing.Point(4, 22);
            this.tabPageManagement.Name = "tabPageManagement";
            this.tabPageManagement.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageManagement.Size = new System.Drawing.Size(946, 514);
            this.tabPageManagement.TabIndex = 1;
            this.tabPageManagement.Text = "启动项管理";
            this.tabPageManagement.UseVisualStyleBackColor = true;

            // 
            // managementPanel
            // 
            this.managementPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.managementPanel.Location = new System.Drawing.Point(3, 3);
            this.managementPanel.Name = "managementPanel";
            this.managementPanel.Size = new System.Drawing.Size(940, 508);
            this.managementPanel.TabIndex = 0;

            // 
            // managementTableLayout
            // 
            this.managementTableLayout.ColumnCount = 1;
            this.managementTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.managementTableLayout.Controls.Add(this.showSystemEntriesCheckBox, 0, 0);
            this.managementTableLayout.Controls.Add(this.managementInnerTableLayout, 0, 1);
            this.managementTableLayout.Controls.Add(this.createGroupBox, 0, 2);
            this.managementTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.managementTableLayout.Location = new System.Drawing.Point(0, 0);
            this.managementTableLayout.Margin = new System.Windows.Forms.Padding(0);

            this.managementTableLayout.Name = "managementTableLayout";
            this.managementTableLayout.RowCount = 3;
            this.managementTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.managementTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.managementTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.managementTableLayout.Size = new System.Drawing.Size(940, 508);
            this.managementTableLayout.TabIndex = 0;

            // 
            // showSystemEntriesCheckBox
            // 
            this.showSystemEntriesCheckBox.AutoSize = true;
            this.showSystemEntriesCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.showSystemEntriesCheckBox.Location = new System.Drawing.Point(3, 3);
            this.showSystemEntriesCheckBox.Margin = new System.Windows.Forms.Padding(5);
            this.showSystemEntriesCheckBox.Name = "showSystemEntriesCheckBox";

            this.showSystemEntriesCheckBox.Size = new System.Drawing.Size(934, 21);
            this.showSystemEntriesCheckBox.TabIndex = 0;
            this.showSystemEntriesCheckBox.Text = "显示系统核心条目（bootmgr、fwbootmgr 等）";
            this.showSystemEntriesCheckBox.UseVisualStyleBackColor = true;

            // 
            // managementInnerTableLayout
            // 
            this.managementInnerTableLayout.ColumnCount = 2;
            this.managementInnerTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89F));
            this.managementInnerTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.managementInnerTableLayout.Controls.Add(this.entriesDataGridView, 0, 0);
            this.managementInnerTableLayout.Controls.Add(this.managementButtonsFlow, 1, 0);

            this.managementInnerTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.managementInnerTableLayout.Location = new System.Drawing.Point(3, 33);
            this.managementInnerTableLayout.Name = "managementInnerTableLayout";
            this.managementInnerTableLayout.RowCount = 1;
            this.managementInnerTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.managementInnerTableLayout.Size = new System.Drawing.Size(934, 352);
            this.managementInnerTableLayout.TabIndex = 1;

            // 
            // entriesDataGridView
            // 
            this.entriesDataGridView.AllowUserToAddRows = false;
            this.entriesDataGridView.AllowUserToDeleteRows = false;
            this.entriesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.entriesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.entriesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entriesDataGridView.Location = new System.Drawing.Point(3, 3);
            this.entriesDataGridView.Margin = new System.Windows.Forms.Padding(5);
            this.entriesDataGridView.MultiSelect = false;
            this.entriesDataGridView.Name = "entriesDataGridView";

            this.entriesDataGridView.ReadOnly = true;
            this.entriesDataGridView.RowHeadersVisible = false;
            this.entriesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.entriesDataGridView.Size = new System.Drawing.Size(694, 346);
            this.entriesDataGridView.TabIndex = 0;

            // 
            // managementButtonsFlow
            // 
            this.managementButtonsFlow.Controls.Add(this.copyEntryButton);
            this.managementButtonsFlow.Controls.Add(this.renameEntryButton);
            this.managementButtonsFlow.Controls.Add(this.deleteEntryButton);
            this.managementButtonsFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.managementButtonsFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.managementButtonsFlow.Location = new System.Drawing.Point(703, 3);
            this.managementButtonsFlow.Name = "managementButtonsFlow";
            this.managementButtonsFlow.Size = new System.Drawing.Size(228, 346);
            this.managementButtonsFlow.TabIndex = 1;

            // 
            // copyEntryButton
            // 
            this.copyEntryButton.AutoSize = true;
            this.copyEntryButton.Location = new System.Drawing.Point(3, 3);
            this.copyEntryButton.Margin = new System.Windows.Forms.Padding(5);
            this.copyEntryButton.Name = "copyEntryButton";
            this.copyEntryButton.Size = new System.Drawing.Size(90, 27);
            this.copyEntryButton.TabIndex = 0;
            this.copyEntryButton.Text = "复制此项";
            this.copyEntryButton.UseVisualStyleBackColor = true;

            // 
            // deleteEntryButton
            // 
            // 
            // renameEntryButton
            // 
            this.renameEntryButton.AutoSize = true;
            this.renameEntryButton.Location = new System.Drawing.Point(3, 36);
            this.renameEntryButton.Margin = new System.Windows.Forms.Padding(5);
            this.renameEntryButton.Name = "renameEntryButton";
            this.renameEntryButton.Size = new System.Drawing.Size(90, 27);
            this.renameEntryButton.TabIndex = 1;
            this.renameEntryButton.Text = "修改名称";
            this.renameEntryButton.UseVisualStyleBackColor = true;

            // 
            // deleteEntryButton
            // 
            this.deleteEntryButton.AutoSize = true;
            this.deleteEntryButton.Location = new System.Drawing.Point(3, 69);
            this.deleteEntryButton.Margin = new System.Windows.Forms.Padding(5);
            this.deleteEntryButton.Name = "deleteEntryButton";
            this.deleteEntryButton.Size = new System.Drawing.Size(90, 27);
            this.deleteEntryButton.TabIndex = 2;
            this.deleteEntryButton.Text = "删除此项";
            this.deleteEntryButton.UseVisualStyleBackColor = true;

            // 
            // createGroupBox
            // 
            this.createGroupBox.Controls.Add(this.createTableLayout);
            this.createGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.createGroupBox.Location = new System.Drawing.Point(3, 391);
            this.createGroupBox.Margin = new System.Windows.Forms.Padding(5);
            this.createGroupBox.Name = "createGroupBox";

            this.createGroupBox.Padding = new System.Windows.Forms.Padding(5);
            this.createGroupBox.Size = new System.Drawing.Size(934, 115);
            this.createGroupBox.TabIndex = 2;
            this.createGroupBox.TabStop = false;
            this.createGroupBox.Text = "创建新项";

            // 
            // createTableLayout
            // 
            this.createTableLayout.ColumnCount = 5;
            this.createTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.createTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.createTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.createTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.createTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.createTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.createTableLayout.Location = new System.Drawing.Point(5, 19);
            this.createTableLayout.Name = "createTableLayout";
            this.createTableLayout.RowCount = 3;
            this.createTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.createTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.createTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.createTableLayout.Size = new System.Drawing.Size(924, 88);
            this.createTableLayout.TabIndex = 0;
            // Row 0: 一键创建
            this.createTableLayout.Controls.Add(new System.Windows.Forms.Label { Text = "名称：", AutoSize = true, Anchor = System.Windows.Forms.AnchorStyles.Left, Margin = new System.Windows.Forms.Padding(5) }, 0, 0);
            this.createTableLayout.Controls.Add(this.createNameTextBox, 1, 0);
            this.createTableLayout.Controls.Add(new System.Windows.Forms.Label { Text = "盘符：", AutoSize = true, Anchor = System.Windows.Forms.AnchorStyles.Left, Margin = new System.Windows.Forms.Padding(5) }, 2, 0);
            this.createTableLayout.Controls.Add(this.createDriveComboBox, 3, 0);
            this.createTableLayout.Controls.Add(this.createButton, 4, 0);
            // Row 1: 分割线
            this.createTableLayout.Controls.Add(this.createSepLabel, 0, 1);
            this.createTableLayout.SetColumnSpan(this.createSepLabel, 5);
            // Row 2: 高级模式
            this.createTableLayout.Controls.Add(new System.Windows.Forms.Label { Text = "类型：", AutoSize = true, Anchor = System.Windows.Forms.AnchorStyles.Left, Margin = new System.Windows.Forms.Padding(5) }, 0, 2);
            this.createTableLayout.Controls.Add(this.createTypeComboBox, 1, 2);
            this.createTableLayout.Controls.Add(this.advancedCreateButton, 4, 2);

            // 
            // createNameTextBox
            // 
            this.createNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.createNameTextBox.Location = new System.Drawing.Point(83, 8);
            this.createNameTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.createNameTextBox.Name = "createNameTextBox";
            this.createNameTextBox.Size = new System.Drawing.Size(252, 21);
            this.createNameTextBox.TabIndex = 1;

            // 
            // createDriveComboBox
            // 
            this.createDriveComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.createDriveComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.createDriveComboBox.FormattingEnabled = true;
            this.createDriveComboBox.Location = new System.Drawing.Point(415, 8);
            this.createDriveComboBox.Margin = new System.Windows.Forms.Padding(5);
            this.createDriveComboBox.Name = "createDriveComboBox";
            this.createDriveComboBox.Size = new System.Drawing.Size(298, 20);
            this.createDriveComboBox.TabIndex = 2;

            // 
            // createButton
            // 
            this.createButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.createButton.AutoSize = false;
            this.createButton.Location = new System.Drawing.Point(721, 3);
            this.createButton.Margin = new System.Windows.Forms.Padding(5);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(155, 27);
            this.createButton.TabIndex = 3;
            this.createButton.Text = "一键创建 Windows 启动项";
            this.createButton.UseVisualStyleBackColor = true;

            // 
            // createSepLabel
            // 
            this.createSepLabel.AutoSize = false;
            this.createSepLabel.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.createSepLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.createSepLabel.Location = new System.Drawing.Point(5, 39);
            this.createSepLabel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.createSepLabel.Name = "createSepLabel";
            this.createSepLabel.Size = new System.Drawing.Size(914, 2);
            this.createSepLabel.TabIndex = 8;
            this.createSepLabel.Text = "";

            // 
            // createTypeComboBox
            // 
            this.createTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.createTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.createTypeComboBox.FormattingEnabled = true;
            this.createTypeComboBox.Location = new System.Drawing.Point(83, 90);
            this.createTypeComboBox.Margin = new System.Windows.Forms.Padding(5);
            this.createTypeComboBox.Name = "createTypeComboBox";
            this.createTypeComboBox.Size = new System.Drawing.Size(252, 20);
            this.createTypeComboBox.TabIndex = 4;

            // 
            // advancedCreateButton
            // 
            this.advancedCreateButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.advancedCreateButton.AutoSize = false;
            this.advancedCreateButton.Location = new System.Drawing.Point(721, 87);
            this.advancedCreateButton.Margin = new System.Windows.Forms.Padding(5);
            this.advancedCreateButton.Name = "advancedCreateButton";
            this.advancedCreateButton.Size = new System.Drawing.Size(155, 27);
            this.advancedCreateButton.TabIndex = 5;
            this.advancedCreateButton.Text = "高级模式创建";
            this.advancedCreateButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.advancedCreateButton.UseVisualStyleBackColor = true;

            // 选项卡 3：常见启动项设置
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.settingsPanel);
            this.settingsPanel.Controls.Add(this.settingsTableLayout);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Size = new System.Drawing.Size(946, 514);
            this.tabPageSettings.TabIndex = 2;
            this.tabPageSettings.Text = "常见启动项设置";
            this.tabPageSettings.UseVisualStyleBackColor = true;

            // 
            // settingsPanel
            // 
            this.settingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPanel.Location = new System.Drawing.Point(3, 3);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(940, 508);
            this.settingsPanel.TabIndex = 0;

            // 
            // settingsTableLayout
            // 
            this.settingsTableLayout.ColumnCount = 2;
            this.settingsTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.settingsTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.settingsTableLayout.Controls.Add(this.defaultEntryGroupBox, 0, 0);
            this.settingsTableLayout.Controls.Add(this.timeoutGroupBox, 0, 1);
            this.settingsTableLayout.Controls.Add(this.displayOrderGroupBox, 1, 0);
            this.settingsTableLayout.SetRowSpan(this.displayOrderGroupBox, 5);
            this.settingsTableLayout.Controls.Add(this.forceMenuGroupBox, 0, 2);
            this.settingsTableLayout.Controls.Add(this.exportGroupBox, 0, 3);
            this.settingsTableLayout.Controls.Add(this.importGroupBox, 0, 4);
            this.settingsTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsTableLayout.Location = new System.Drawing.Point(0, 0);
            this.settingsTableLayout.Margin = new System.Windows.Forms.Padding(0);

            this.settingsTableLayout.Name = "settingsTableLayout";
            this.settingsTableLayout.RowCount = 5;
            this.settingsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.settingsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.settingsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.settingsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.settingsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.settingsTableLayout.Size = new System.Drawing.Size(940, 508);
            this.settingsTableLayout.TabIndex = 0;

            // 
            // defaultEntryGroupBox
            // 
            this.defaultEntryGroupBox.Controls.Add(this.defaultEntryFlow);
            this.defaultEntryGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defaultEntryGroupBox.Location = new System.Drawing.Point(3, 3);
            this.defaultEntryGroupBox.Margin = new System.Windows.Forms.Padding(5);
            this.defaultEntryGroupBox.Name = "defaultEntryGroupBox";
            this.defaultEntryGroupBox.Padding = new System.Windows.Forms.Padding(5);
            this.defaultEntryGroupBox.Size = new System.Drawing.Size(934, 121);
            this.defaultEntryGroupBox.TabIndex = 0;
            this.defaultEntryGroupBox.TabStop = false;
            this.defaultEntryGroupBox.Text = "默认启动项";

            // 
            // defaultEntryFlow
            // 
            this.defaultEntryFlow.ColumnCount = 2;
            this.defaultEntryFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.defaultEntryFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.defaultEntryFlow.Controls.Add(this.defaultEntryComboBox, 0, 0);
            this.defaultEntryFlow.Controls.Add(this.applyDefaultEntryButton, 1, 0);
            this.defaultEntryFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defaultEntryFlow.Location = new System.Drawing.Point(5, 19);
            this.defaultEntryFlow.Margin = new System.Windows.Forms.Padding(0);
            this.defaultEntryFlow.Name = "defaultEntryFlow";
            this.defaultEntryFlow.RowCount = 1;
            this.defaultEntryFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.defaultEntryFlow.Size = new System.Drawing.Size(924, 97);
            this.defaultEntryFlow.TabIndex = 0;

            // 
            // defaultEntryComboBox
            // 
            this.defaultEntryComboBox.Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            this.defaultEntryComboBox.FormattingEnabled = true;
            this.defaultEntryComboBox.Location = new System.Drawing.Point(3, 0);
            this.defaultEntryComboBox.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.defaultEntryComboBox.Name = "defaultEntryComboBox";
            this.defaultEntryComboBox.Size = new System.Drawing.Size(824, 21);
            this.defaultEntryComboBox.TabIndex = 0;


            // 
            // applyDefaultEntryButton
            // 
            this.applyDefaultEntryButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.applyDefaultEntryButton.AutoSize = true;
            this.applyDefaultEntryButton.Location = new System.Drawing.Point(844, 0);
            this.applyDefaultEntryButton.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.applyDefaultEntryButton.Name = "applyDefaultEntryButton";
            this.applyDefaultEntryButton.Size = new System.Drawing.Size(75, 27);
            this.applyDefaultEntryButton.TabIndex = 1;
            this.applyDefaultEntryButton.Text = "应用";
            this.applyDefaultEntryButton.UseVisualStyleBackColor = true;


            // 
            // timeoutGroupBox
            // 
            this.timeoutGroupBox.Controls.Add(this.timeoutFlow);
            this.timeoutGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeoutGroupBox.Location = new System.Drawing.Point(3, 130);
            this.timeoutGroupBox.Margin = new System.Windows.Forms.Padding(5);
            this.timeoutGroupBox.Name = "timeoutGroupBox";
            this.timeoutGroupBox.Padding = new System.Windows.Forms.Padding(5);
            this.timeoutGroupBox.Size = new System.Drawing.Size(934, 121);
            this.timeoutGroupBox.TabIndex = 1;
            this.timeoutGroupBox.TabStop = false;
            this.timeoutGroupBox.Text = "启动菜单超时";

            // 
            // timeoutFlow
            // 
            this.timeoutFlow.ColumnCount = 3;
            this.timeoutFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.timeoutFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.timeoutFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.timeoutFlow.Controls.Add(this.timeoutNumericUpDown, 0, 0);
            this.timeoutFlow.Controls.Add(this.timeoutLabel, 1, 0);
            this.timeoutFlow.Controls.Add(this.applyTimeoutButton, 2, 0);
            this.timeoutFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeoutFlow.Location = new System.Drawing.Point(5, 19);
            this.timeoutFlow.Margin = new System.Windows.Forms.Padding(0);
            this.timeoutFlow.Name = "timeoutFlow";
            this.timeoutFlow.RowCount = 1;
            this.timeoutFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.timeoutFlow.Size = new System.Drawing.Size(924, 97);
            this.timeoutFlow.TabIndex = 0;

            // 
            // timeoutNumericUpDown
            // 
            this.timeoutNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.timeoutNumericUpDown.Location = new System.Drawing.Point(8, 0);
            this.timeoutNumericUpDown.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            this.timeoutNumericUpDown.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.timeoutNumericUpDown.Name = "timeoutNumericUpDown";
            this.timeoutNumericUpDown.Size = new System.Drawing.Size(120, 21);
            this.timeoutNumericUpDown.TabIndex = 0;


            // 
            // timeoutLabel
            // 
            this.timeoutLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.timeoutLabel.AutoSize = true;
            this.timeoutLabel.Location = new System.Drawing.Point(138, 0);
            this.timeoutLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.timeoutLabel.Name = "timeoutLabel";
            this.timeoutLabel.Size = new System.Drawing.Size(17, 12);
            this.timeoutLabel.TabIndex = 2;
            this.timeoutLabel.Text = "秒";
            this.timeoutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;


            // 
            // applyTimeoutButton
            // 
            this.applyTimeoutButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.applyTimeoutButton.AutoSize = false;
            this.applyTimeoutButton.Location = new System.Drawing.Point(844, 0);
            this.applyTimeoutButton.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.applyTimeoutButton.Name = "applyTimeoutButton";
            this.applyTimeoutButton.Size = new System.Drawing.Size(75, 27);
            this.applyTimeoutButton.TabIndex = 1;
            this.applyTimeoutButton.Text = "应用";
            this.applyTimeoutButton.UseVisualStyleBackColor = true;



            // 
            // displayOrderGroupBox
            // 
            this.displayOrderGroupBox.Controls.Add(this.displayOrderTableLayout);
            this.displayOrderGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayOrderGroupBox.Location = new System.Drawing.Point(3, 257);
            this.displayOrderGroupBox.Margin = new System.Windows.Forms.Padding(5);
            this.displayOrderGroupBox.Name = "displayOrderGroupBox";
            this.displayOrderGroupBox.Padding = new System.Windows.Forms.Padding(5);
            this.displayOrderGroupBox.Size = new System.Drawing.Size(934, 121);
            this.displayOrderGroupBox.TabIndex = 2;
            this.displayOrderGroupBox.TabStop = false;
            this.displayOrderGroupBox.Text = "启动项显示顺序";

            // 
            // displayOrderTableLayout
            // 
            this.displayOrderTableLayout.ColumnCount = 2;
            this.displayOrderTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82F));
            this.displayOrderTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.displayOrderTableLayout.Controls.Add(this.displayOrderListBox, 0, 0);
            this.displayOrderTableLayout.Controls.Add(this.displayOrderButtonsFlow, 1, 0);




            this.displayOrderTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayOrderTableLayout.Location = new System.Drawing.Point(5, 19);
            this.displayOrderTableLayout.Name = "displayOrderTableLayout";
            this.displayOrderTableLayout.RowCount = 1;
            this.displayOrderTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.displayOrderTableLayout.Size = new System.Drawing.Size(924, 97);
            this.displayOrderTableLayout.TabIndex = 0;

            // 
            // displayOrderListBox
            // 
            this.displayOrderListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayOrderListBox.FormattingEnabled = true;
            this.displayOrderListBox.Location = new System.Drawing.Point(3, 3);
            this.displayOrderListBox.Margin = new System.Windows.Forms.Padding(5);
            this.displayOrderListBox.Name = "displayOrderListBox";
            this.displayOrderListBox.Size = new System.Drawing.Size(733, 91);
            this.displayOrderListBox.TabIndex = 0;

            // 
            // displayOrderButtonsFlow
            // 
            this.displayOrderButtonsFlow.Controls.Add(this.moveUpButton);
            this.displayOrderButtonsFlow.Controls.Add(this.moveDownButton);
            this.displayOrderButtonsFlow.Controls.Add(this.applyDisplayOrderButton);
            this.displayOrderButtonsFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayOrderButtonsFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.displayOrderButtonsFlow.Location = new System.Drawing.Point(742, 3);
            this.displayOrderButtonsFlow.Name = "displayOrderButtonsFlow";
            this.displayOrderButtonsFlow.Size = new System.Drawing.Size(179, 91);
            this.displayOrderButtonsFlow.TabIndex = 1;

            // 
            // moveUpButton
            // 
            this.moveUpButton.AutoSize = true;
            this.moveUpButton.Location = new System.Drawing.Point(3, 3);
            this.moveUpButton.Margin = new System.Windows.Forms.Padding(5);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(75, 27);
            this.moveUpButton.TabIndex = 0;
            this.moveUpButton.Text = "上移";
            this.moveUpButton.UseVisualStyleBackColor = true;

            // 
            // moveDownButton
            // 
            this.moveDownButton.AutoSize = true;
            this.moveDownButton.Location = new System.Drawing.Point(3, 36);
            this.moveDownButton.Margin = new System.Windows.Forms.Padding(5);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(75, 27);
            this.moveDownButton.TabIndex = 1;
            this.moveDownButton.Text = "下移";
            this.moveDownButton.UseVisualStyleBackColor = true;

            // 
            // applyDisplayOrderButton
            // 
            this.applyDisplayOrderButton.AutoSize = true;
            this.applyDisplayOrderButton.Location = new System.Drawing.Point(3, 69);
            this.applyDisplayOrderButton.Margin = new System.Windows.Forms.Padding(5);
            this.applyDisplayOrderButton.Name = "applyDisplayOrderButton";
            this.applyDisplayOrderButton.Size = new System.Drawing.Size(75, 27);
            this.applyDisplayOrderButton.TabIndex = 2;
            this.applyDisplayOrderButton.Text = "应用";
            this.applyDisplayOrderButton.UseVisualStyleBackColor = true;

            // 
            // forceMenuGroupBox
            // 
            this.forceMenuGroupBox.Controls.Add(this.forceMenuFlow);
            this.forceMenuGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.forceMenuGroupBox.Location = new System.Drawing.Point(3, 384);
            this.forceMenuGroupBox.Margin = new System.Windows.Forms.Padding(5);
            this.forceMenuGroupBox.Name = "forceMenuGroupBox";
            this.forceMenuGroupBox.Padding = new System.Windows.Forms.Padding(5);
            this.forceMenuGroupBox.Size = new System.Drawing.Size(934, 121);
            this.forceMenuGroupBox.TabIndex = 3;
            this.forceMenuGroupBox.TabStop = false;
            this.forceMenuGroupBox.Text = "强制显示启动菜单";

            // 
            // forceMenuFlow
            // 
            this.forceMenuFlow.ColumnCount = 2;
            this.forceMenuFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.forceMenuFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.forceMenuFlow.Controls.Add(this.forceMenuCheckBox, 0, 0);
            this.forceMenuFlow.Controls.Add(this.applyForceMenuButton, 1, 0);
            this.forceMenuFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.forceMenuFlow.Location = new System.Drawing.Point(5, 19);
            this.forceMenuFlow.Margin = new System.Windows.Forms.Padding(0);
            this.forceMenuFlow.Name = "forceMenuFlow";
            this.forceMenuFlow.RowCount = 1;
            this.forceMenuFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.forceMenuFlow.Size = new System.Drawing.Size(924, 97);
            this.forceMenuFlow.TabIndex = 0;

            // 
            // forceMenuCheckBox
            // 
            this.forceMenuCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.forceMenuCheckBox.AutoSize = true;
            this.forceMenuCheckBox.Location = new System.Drawing.Point(8, 0);
            this.forceMenuCheckBox.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.forceMenuCheckBox.Name = "forceMenuCheckBox";
            this.forceMenuCheckBox.Size = new System.Drawing.Size(126, 21);
            this.forceMenuCheckBox.TabIndex = 0;
            this.forceMenuCheckBox.Text = "强制显示启动菜单";
            this.forceMenuCheckBox.UseVisualStyleBackColor = true;


            // 
            // applyForceMenuButton
            // 
            this.applyForceMenuButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.applyForceMenuButton.AutoSize = true;
            this.applyForceMenuButton.Location = new System.Drawing.Point(844, 0);
            this.applyForceMenuButton.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.applyForceMenuButton.Name = "applyForceMenuButton";
            this.applyForceMenuButton.Size = new System.Drawing.Size(75, 27);
            this.applyForceMenuButton.TabIndex = 1;
            this.applyForceMenuButton.Text = "应用";
            this.applyForceMenuButton.UseVisualStyleBackColor = true;


            // 选项卡 4：高级开关
            // 
            // tabPageAdvanced
            // 
            this.tabPageAdvanced.Controls.Add(this.advancedPanel);
            this.advancedPanel.Controls.Add(this.advancedTableLayout);
            this.tabPageAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            this.tabPageAdvanced.Size = new System.Drawing.Size(946, 514);
            this.tabPageAdvanced.TabIndex = 3;
            this.tabPageAdvanced.Text = "高级开关";
            this.tabPageAdvanced.UseVisualStyleBackColor = true;

            // 
            // advancedPanel
            // 
            this.advancedPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advancedPanel.Location = new System.Drawing.Point(3, 3);
            this.advancedPanel.Name = "advancedPanel";
            this.advancedPanel.Size = new System.Drawing.Size(940, 508);
            this.advancedPanel.TabIndex = 0;

            // 
            // advancedTableLayout
            // 
            this.advancedTableLayout.ColumnCount = 1;
            this.advancedTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.advancedTableLayout.Controls.Add(this.targetEntryTableLayout, 0, 0);
            this.advancedTableLayout.Controls.Add(this.advancedSwitchesTableLayout, 0, 1);
            this.advancedTableLayout.Controls.Add(this.advancedPreviewTableLayout, 0, 2);
            this.advancedTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advancedTableLayout.Location = new System.Drawing.Point(0, 0);
            this.advancedTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.advancedTableLayout.Name = "advancedTableLayout";
            this.advancedTableLayout.RowCount = 3;
            this.advancedTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.advancedTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.advancedTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.advancedTableLayout.Size = new System.Drawing.Size(940, 508);
            this.advancedTableLayout.TabIndex = 0;

            // 
            // targetEntryTableLayout
            // 
            this.targetEntryTableLayout.ColumnCount = 2;
            this.targetEntryTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.targetEntryTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.targetEntryTableLayout.Controls.Add(this.targetEntryLabel, 0, 0);
            this.targetEntryTableLayout.Controls.Add(this.targetEntryComboBox, 1, 0);
            this.targetEntryTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.targetEntryTableLayout.Location = new System.Drawing.Point(3, 3);
            this.targetEntryTableLayout.Name = "targetEntryTableLayout";
            this.targetEntryTableLayout.RowCount = 1;
            this.targetEntryTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.targetEntryTableLayout.Size = new System.Drawing.Size(934, 39);
            this.targetEntryTableLayout.TabIndex = 0;

            // 
            // targetEntryLabel
            // 
            this.targetEntryLabel.Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            this.targetEntryLabel.AutoSize = true;
            this.targetEntryLabel.Location = new System.Drawing.Point(3, 13);
            this.targetEntryLabel.Margin = new System.Windows.Forms.Padding(5);
            this.targetEntryLabel.Name = "targetEntryLabel";
            this.targetEntryLabel.Size = new System.Drawing.Size(42, 12);
            this.targetEntryLabel.TabIndex = 0;
            this.targetEntryLabel.Text = "目标条目：";

            // 
            // targetEntryComboBox
            // 
            this.targetEntryComboBox.Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            this.targetEntryComboBox.FormattingEnabled = true;
            this.targetEntryComboBox.Location = new System.Drawing.Point(83, 3);
            this.targetEntryComboBox.Margin = new System.Windows.Forms.Padding(5);
            this.targetEntryComboBox.Name = "targetEntryComboBox";
            this.targetEntryComboBox.Size = new System.Drawing.Size(846, 20);
            this.targetEntryComboBox.TabIndex = 1;

            // 
            // advancedSwitchesTableLayout
            // 
            this.advancedSwitchesTableLayout.ColumnCount = 2;
            this.advancedSwitchesTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.advancedSwitchesTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.advancedSwitchesTableLayout.Controls.Add(this.debugTestsGroupBox, 0, 0);
            this.advancedSwitchesTableLayout.Controls.Add(this.securityDriversGroupBox, 1, 0);
            this.advancedSwitchesTableLayout.Controls.Add(this.displayMemoryGroupBox, 0, 1);
            this.advancedSwitchesTableLayout.Controls.Add(this.virtualizationGroupBox, 1, 1);
            this.advancedSwitchesTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advancedSwitchesTableLayout.AutoSize = true;
            this.advancedSwitchesTableLayout.Location = new System.Drawing.Point(3, 48);
            this.advancedSwitchesTableLayout.Name = "advancedSwitchesTableLayout";
            this.advancedSwitchesTableLayout.RowCount = 2;
            this.advancedSwitchesTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.advancedSwitchesTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.advancedSwitchesTableLayout.Size = new System.Drawing.Size(934, 400);
            this.advancedSwitchesTableLayout.TabIndex = 1;

            // 
            // debugTestsGroupBox
            // 
            this.debugTestsGroupBox.Controls.Add(this.debugTestsFlow);
            this.debugTestsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugTestsGroupBox.Location = new System.Drawing.Point(5, 5);
            this.debugTestsGroupBox.Margin = new System.Windows.Forms.Padding(5);
            this.debugTestsGroupBox.Name = "debugTestsGroupBox";
            this.debugTestsGroupBox.Padding = new System.Windows.Forms.Padding(5);
            this.debugTestsGroupBox.Size = new System.Drawing.Size(457, 120);

            this.debugTestsGroupBox.TabIndex = 0;
            this.debugTestsGroupBox.TabStop = false;
            this.debugTestsGroupBox.Text = "调试与测试";

            // 
            // debugTestsFlow
            // 
this.debugTestsFlow.Controls.Add(this.testsigningCheckBox);
this.debugTestsFlow.Controls.Add(this.debugCheckBox);
this.debugTestsFlow.Controls.Add(this.bootdebugCheckBox);
this.debugTestsFlow.Dock = System.Windows.Forms.DockStyle.Fill;
this.debugTestsFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
this.debugTestsFlow.Location = new System.Drawing.Point(5, 19);
this.debugTestsFlow.Margin = new System.Windows.Forms.Padding(0);
this.debugTestsFlow.Name = "debugTestsFlow";
this.debugTestsFlow.Size = new System.Drawing.Size(447, 166);
this.debugTestsFlow.TabIndex = 0;

            // 
            // testsigningCheckBox
            // 
            this.testsigningCheckBox.AutoSize = true;
            this.testsigningCheckBox.Margin = new System.Windows.Forms.Padding(5);
            this.testsigningCheckBox.Name = "testsigningCheckBox";
            this.testsigningCheckBox.Size = new System.Drawing.Size(162, 21);
            this.testsigningCheckBox.TabIndex = 0;
            this.testsigningCheckBox.Text = "测试签名模式 (testsigning)";
            this.testsigningCheckBox.UseVisualStyleBackColor = true;

            // 
            // debugCheckBox
            // 
            this.debugCheckBox.AutoSize = true;
            this.debugCheckBox.Margin = new System.Windows.Forms.Padding(5);
            this.debugCheckBox.Name = "debugCheckBox";
            this.debugCheckBox.Size = new System.Drawing.Size(126, 21);
            this.debugCheckBox.TabIndex = 1;
            this.debugCheckBox.Text = "内核调试 (debug)";
            this.debugCheckBox.UseVisualStyleBackColor = true;

            // 
            // bootdebugCheckBox
            // 
            this.bootdebugCheckBox.AutoSize = true;
            this.bootdebugCheckBox.Margin = new System.Windows.Forms.Padding(5);
            this.bootdebugCheckBox.Name = "bootdebugCheckBox";
            this.bootdebugCheckBox.Size = new System.Drawing.Size(150, 21);
            this.bootdebugCheckBox.TabIndex = 2;
            this.bootdebugCheckBox.Text = "启动调试 (bootdebug)";
            this.bootdebugCheckBox.UseVisualStyleBackColor = true;

            // 
            // securityDriversGroupBox
            // 
            this.securityDriversGroupBox.Controls.Add(this.securityDriversFlow);
            this.securityDriversGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.securityDriversGroupBox.Location = new System.Drawing.Point(472, 5);
            this.securityDriversGroupBox.Margin = new System.Windows.Forms.Padding(5);
            this.securityDriversGroupBox.Name = "securityDriversGroupBox";
            this.securityDriversGroupBox.Padding = new System.Windows.Forms.Padding(5);
            this.securityDriversGroupBox.Size = new System.Drawing.Size(457, 100);

            this.securityDriversGroupBox.TabIndex = 1;
            this.securityDriversGroupBox.TabStop = false;
            this.securityDriversGroupBox.Text = "安全与驱动";

            // 
            // securityDriversFlow
            // 
this.securityDriversFlow.Controls.Add(this.nointegritychecksCheckBox);
this.securityDriversFlow.Controls.Add(this.disableelamdriversCheckBox);
this.securityDriversFlow.Dock = System.Windows.Forms.DockStyle.Fill;
this.securityDriversFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
this.securityDriversFlow.Location = new System.Drawing.Point(5, 19);
this.securityDriversFlow.Margin = new System.Windows.Forms.Padding(0);
this.securityDriversFlow.Name = "securityDriversFlow";
this.securityDriversFlow.Size = new System.Drawing.Size(447, 166);
this.securityDriversFlow.TabIndex = 0;

            // 
            // nointegritychecksCheckBox
            // 
            this.nointegritychecksCheckBox.AutoSize = true;
            this.nointegritychecksCheckBox.Margin = new System.Windows.Forms.Padding(5);
            this.nointegritychecksCheckBox.Name = "nointegritychecksCheckBox";
            this.nointegritychecksCheckBox.Size = new System.Drawing.Size(210, 21);
            this.nointegritychecksCheckBox.TabIndex = 0;
            this.nointegritychecksCheckBox.Text = "禁用完整性检查 (nointegritychecks)";
            this.nointegritychecksCheckBox.UseVisualStyleBackColor = true;

            // 
            // disableelamdriversCheckBox
            // 
            this.disableelamdriversCheckBox.AutoSize = true;
            this.disableelamdriversCheckBox.Margin = new System.Windows.Forms.Padding(5);
            this.disableelamdriversCheckBox.Name = "disableelamdriversCheckBox";
            this.disableelamdriversCheckBox.Size = new System.Drawing.Size(210, 21);
            this.disableelamdriversCheckBox.TabIndex = 1;
            this.disableelamdriversCheckBox.Text = "禁用 ELAM 驱动 (disableelamdrivers)";
            this.disableelamdriversCheckBox.UseVisualStyleBackColor = true;

            // 
            // displayMemoryGroupBox
            // 
            this.displayMemoryGroupBox.Controls.Add(this.displayMemoryFlow);
            this.displayMemoryGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayMemoryGroupBox.Location = new System.Drawing.Point(5, 205);
            this.displayMemoryGroupBox.Margin = new System.Windows.Forms.Padding(5);
            this.displayMemoryGroupBox.Name = "displayMemoryGroupBox";
            this.displayMemoryGroupBox.Padding = new System.Windows.Forms.Padding(5);
            this.displayMemoryGroupBox.Size = new System.Drawing.Size(457, 120);

            this.displayMemoryGroupBox.TabIndex = 2;
            this.displayMemoryGroupBox.TabStop = false;
            this.displayMemoryGroupBox.Text = "显示与内存";

            // 
            // displayMemoryFlow
            // 
this.displayMemoryFlow.Controls.Add(this.paeCheckBox);
this.displayMemoryFlow.Controls.Add(this.nxCheckBox);
this.displayMemoryFlow.Controls.Add(this.novesaCheckBox);
this.displayMemoryFlow.Dock = System.Windows.Forms.DockStyle.Fill;
this.displayMemoryFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
this.displayMemoryFlow.Location = new System.Drawing.Point(5, 19);
this.displayMemoryFlow.Margin = new System.Windows.Forms.Padding(0);
this.displayMemoryFlow.Name = "displayMemoryFlow";
this.displayMemoryFlow.Size = new System.Drawing.Size(447, 166);
this.displayMemoryFlow.TabIndex = 0;

            // 
            // paeCheckBox
            // 
            this.paeCheckBox.AutoSize = true;
            this.paeCheckBox.Margin = new System.Windows.Forms.Padding(5);
            this.paeCheckBox.Name = "paeCheckBox";
            this.paeCheckBox.Size = new System.Drawing.Size(150, 21);
            this.paeCheckBox.TabIndex = 0;
            this.paeCheckBox.Text = "物理地址扩展 (pae)";
            this.paeCheckBox.UseVisualStyleBackColor = true;

            // 
            // nxCheckBox
            // 
            this.nxCheckBox.AutoSize = true;
            this.nxCheckBox.Margin = new System.Windows.Forms.Padding(5);
            this.nxCheckBox.Name = "nxCheckBox";
            this.nxCheckBox.Size = new System.Drawing.Size(150, 21);
            this.nxCheckBox.TabIndex = 1;
            this.nxCheckBox.Text = "数据执行保护 (nx)";
            this.nxCheckBox.UseVisualStyleBackColor = true;

            // 
            // novesaCheckBox
            // 
            this.novesaCheckBox.AutoSize = true;
            this.novesaCheckBox.Margin = new System.Windows.Forms.Padding(5);
            this.novesaCheckBox.Name = "novesaCheckBox";
            this.novesaCheckBox.Size = new System.Drawing.Size(174, 21);
            this.novesaCheckBox.TabIndex = 2;
            this.novesaCheckBox.Text = "禁用 VESA 显示 (novesa)";
            this.novesaCheckBox.UseVisualStyleBackColor = true;

            // 
            // virtualizationGroupBox
            // 
            this.virtualizationGroupBox.Controls.Add(this.virtualizationFlow);
            this.virtualizationGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.virtualizationGroupBox.Location = new System.Drawing.Point(472, 205);
            this.virtualizationGroupBox.Margin = new System.Windows.Forms.Padding(5);
            this.virtualizationGroupBox.Name = "virtualizationGroupBox";
            this.virtualizationGroupBox.Padding = new System.Windows.Forms.Padding(5);
            this.virtualizationGroupBox.Size = new System.Drawing.Size(457, 100);

            this.virtualizationGroupBox.TabIndex = 3;
            this.virtualizationGroupBox.TabStop = false;
            this.virtualizationGroupBox.Text = "Hyper-V 虚拟化";

            // 
            // virtualizationFlow
            // 
this.virtualizationFlow.Controls.Add(this.hypervisorLabel);
this.virtualizationFlow.Controls.Add(this.hypervisorComboBox);
this.virtualizationFlow.Dock = System.Windows.Forms.DockStyle.Fill;
this.virtualizationFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
this.virtualizationFlow.Location = new System.Drawing.Point(5, 19);
this.virtualizationFlow.Margin = new System.Windows.Forms.Padding(0);
this.virtualizationFlow.Name = "virtualizationFlow";
this.virtualizationFlow.Size = new System.Drawing.Size(447, 166);
this.virtualizationFlow.TabIndex = 0;

            // 
            // hypervisorLabel
            // 
            this.hypervisorLabel.AutoSize = true;
            this.hypervisorLabel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.hypervisorLabel.Name = "hypervisorLabel";
            this.hypervisorLabel.Size = new System.Drawing.Size(113, 12);
            this.hypervisorLabel.TabIndex = 0;
            this.hypervisorLabel.Text = "Hyper-V 启动类型：";

            // 
            // hypervisorComboBox
            // 
            this.hypervisorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.hypervisorComboBox.Margin = new System.Windows.Forms.Padding(5);
            this.hypervisorComboBox.Name = "hypervisorComboBox";
            this.hypervisorComboBox.Size = new System.Drawing.Size(150, 20);
            this.hypervisorComboBox.TabIndex = 1;

            // 
            // advancedPreviewTableLayout
            // 
            this.advancedPreviewTableLayout.ColumnCount = 2;
            this.advancedPreviewTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.advancedPreviewTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.advancedPreviewTableLayout.Controls.Add(this.commandPreviewTextBox, 0, 0);
            this.advancedPreviewTableLayout.Controls.Add(this.applyAdvancedButton, 1, 0);
            this.advancedPreviewTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advancedPreviewTableLayout.Location = new System.Drawing.Point(3, 451);
            this.advancedPreviewTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.advancedPreviewTableLayout.Name = "advancedPreviewTableLayout";
            this.advancedPreviewTableLayout.RowCount = 1;
            this.advancedPreviewTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.advancedPreviewTableLayout.Size = new System.Drawing.Size(934, 54);
            this.advancedPreviewTableLayout.TabIndex = 2;

            // 
            // commandPreviewTextBox
            // 
            this.commandPreviewTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandPreviewTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandPreviewTextBox.Location = new System.Drawing.Point(3, 3);
            this.commandPreviewTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.commandPreviewTextBox.Multiline = true;
            this.commandPreviewTextBox.Name = "commandPreviewTextBox";
            this.commandPreviewTextBox.ReadOnly = true;
            this.commandPreviewTextBox.Size = new System.Drawing.Size(831, 46);
            this.commandPreviewTextBox.TabIndex = 0;
            this.commandPreviewTextBox.Text = "";

            // 
            // applyAdvancedButton
            // 
            this.applyAdvancedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.applyAdvancedButton.AutoSize = true;
            this.applyAdvancedButton.Location = new System.Drawing.Point(844, 3);
            this.applyAdvancedButton.Margin = new System.Windows.Forms.Padding(5);
            this.applyAdvancedButton.Name = "applyAdvancedButton";
            this.applyAdvancedButton.Size = new System.Drawing.Size(75, 27);
            this.applyAdvancedButton.TabIndex = 1;
            this.applyAdvancedButton.Text = "应用";
            this.applyAdvancedButton.UseVisualStyleBackColor = true;



            // 选项卡 5：引导修复
            // 
            // tabPageRepair
            // 
            this.tabPageRepair.Controls.Add(this.repairPanel);
            this.tabPageRepair.Location = new System.Drawing.Point(4, 22);
            this.tabPageRepair.Name = "tabPageRepair";
            this.tabPageRepair.Size = new System.Drawing.Size(946, 514);
            this.tabPageRepair.TabIndex = 4;
            this.tabPageRepair.Text = "引导修复";
            this.tabPageRepair.UseVisualStyleBackColor = true;

            // 
            // repairPanel
            // 
            this.repairPanel.Controls.Add(this.repairTableLayout);
            this.repairPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.repairPanel.Padding = new System.Windows.Forms.Padding(12);
            this.repairPanel.Location = new System.Drawing.Point(3, 3);
            this.repairPanel.Name = "repairPanel";
            this.repairPanel.Size = new System.Drawing.Size(940, 508);
            this.repairPanel.TabIndex = 0;

            // 
            // repairTableLayout
            // 
            this.repairTableLayout.ColumnCount = 1;
            this.repairTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.repairTableLayout.Controls.Add(this.unlockRepairButton, 0, 0);
            this.repairTableLayout.Controls.Add(this.repairConfigGroupBox, 0, 1);
            this.repairTableLayout.Controls.Add(this.repairResultLabel, 0, 2);
            this.repairTableLayout.Controls.Add(this.repairResultTextBox, 0, 3);
            this.repairTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.repairTableLayout.Location = new System.Drawing.Point(0, 0);
            this.repairTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.repairTableLayout.Name = "repairTableLayout";
            this.repairTableLayout.RowCount = 4;
            this.repairTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.repairTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.repairTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.repairTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.repairTableLayout.Size = new System.Drawing.Size(916, 484);
            this.repairTableLayout.TabIndex = 0;

            // 
            // unlockRepairButton
            // 
            this.unlockRepairButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.unlockRepairButton.Location = new System.Drawing.Point(0, 0);
            this.unlockRepairButton.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.unlockRepairButton.Name = "unlockRepairButton";
            this.unlockRepairButton.Size = new System.Drawing.Size(916, 36);
            this.unlockRepairButton.TabIndex = 8;
            this.unlockRepairButton.Text = "⚠  开启实验性功能——引导修复";
            this.unlockRepairButton.UseVisualStyleBackColor = true;

            // 
            // repairConfigGroupBox
            // 
            this.repairConfigGroupBox.Controls.Add(this.repairConfigLayout);
            this.repairConfigGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.repairConfigGroupBox.Location = new System.Drawing.Point(0, 48);
            this.repairConfigGroupBox.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.repairConfigGroupBox.Name = "repairConfigGroupBox";
            this.repairConfigGroupBox.Padding = new System.Windows.Forms.Padding(10);
            this.repairConfigGroupBox.Size = new System.Drawing.Size(916, 202);
            this.repairConfigGroupBox.TabIndex = 5;
            this.repairConfigGroupBox.TabStop = false;
            this.repairConfigGroupBox.Text = "修复参数";

            // 
            // repairConfigLayout
            // 
            this.repairConfigLayout.ColumnCount = 2;
            this.repairConfigLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.repairConfigLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.repairConfigLayout.Controls.Add(this.CreateLabel("Windows 目录："), 0, 0);
            this.repairConfigLayout.Controls.Add(this.windowsDirPanel, 1, 0);
            this.repairConfigLayout.Controls.Add(this.CreateLabel("语言："), 0, 1);
            this.repairConfigLayout.Controls.Add(this.languageComboBox, 1, 1);
            this.repairConfigLayout.Controls.Add(this.CreateLabel("固件类型："), 0, 2);
            this.repairConfigLayout.Controls.Add(this.firmwareComboBox, 1, 2);
            this.repairConfigLayout.Controls.Add(this.overwriteCheckBox, 1, 3);
            this.repairConfigLayout.Controls.Add(this.repairButton, 0, 4);
            this.repairConfigLayout.SetColumnSpan(this.repairButton, 2);
            this.repairConfigLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.repairConfigLayout.Location = new System.Drawing.Point(10, 22);
            this.repairConfigLayout.Margin = new System.Windows.Forms.Padding(0);
            this.repairConfigLayout.Name = "repairConfigLayout";
            this.repairConfigLayout.RowCount = 5;
            this.repairConfigLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.repairConfigLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.repairConfigLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.repairConfigLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.repairConfigLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.repairConfigLayout.Size = new System.Drawing.Size(896, 170);
            this.repairConfigLayout.TabIndex = 0;

            // 
            // windowsDirPanel
            // 
            this.windowsDirPanel.ColumnCount = 2;
            this.windowsDirPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.windowsDirPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.windowsDirPanel.Controls.Add(this.windowsDirTextBox, 0, 0);
            this.windowsDirPanel.Controls.Add(this.windowsDirBrowseButton, 1, 0);
            this.windowsDirPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.windowsDirPanel.Location = new System.Drawing.Point(110, 0);
            this.windowsDirPanel.Margin = new System.Windows.Forms.Padding(0);
            this.windowsDirPanel.Name = "windowsDirPanel";
            this.windowsDirPanel.RowCount = 1;
            this.windowsDirPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.windowsDirPanel.Size = new System.Drawing.Size(786, 34);
            this.windowsDirPanel.TabIndex = 0;

            // 
            // windowsDirTextBox
            // 
            this.windowsDirTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.windowsDirTextBox.Location = new System.Drawing.Point(5, 6);
            this.windowsDirTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.windowsDirTextBox.Name = "windowsDirTextBox";
            this.windowsDirTextBox.Size = new System.Drawing.Size(691, 21);
            this.windowsDirTextBox.TabIndex = 3;

            // 
            // windowsDirBrowseButton
            // 
            this.windowsDirBrowseButton.Location = new System.Drawing.Point(701, 3);
            this.windowsDirBrowseButton.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.windowsDirBrowseButton.Name = "windowsDirBrowseButton";
            this.windowsDirBrowseButton.Size = new System.Drawing.Size(82, 26);
            this.windowsDirBrowseButton.TabIndex = 4;
            this.windowsDirBrowseButton.Text = "浏览...";
            this.windowsDirBrowseButton.UseVisualStyleBackColor = true;

            // 
            // languageComboBox
            // 
            this.languageComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.languageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageComboBox.FormattingEnabled = true;
            this.languageComboBox.Location = new System.Drawing.Point(115, 5);
            this.languageComboBox.Margin = new System.Windows.Forms.Padding(5);
            this.languageComboBox.Name = "languageComboBox";
            this.languageComboBox.Size = new System.Drawing.Size(776, 20);
            this.languageComboBox.TabIndex = 1;

            // 
            // firmwareComboBox
            // 
            this.firmwareComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.firmwareComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.firmwareComboBox.FormattingEnabled = true;
            this.firmwareComboBox.Location = new System.Drawing.Point(115, 73);
            this.firmwareComboBox.Margin = new System.Windows.Forms.Padding(5);
            this.firmwareComboBox.Name = "firmwareComboBox";
            this.firmwareComboBox.Size = new System.Drawing.Size(776, 20);
            this.firmwareComboBox.TabIndex = 0;

            // 
            // overwriteCheckBox
            // 
            this.overwriteCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.overwriteCheckBox.AutoSize = true;
            this.overwriteCheckBox.Location = new System.Drawing.Point(115, 106);
            this.overwriteCheckBox.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.overwriteCheckBox.Name = "overwriteCheckBox";
            this.overwriteCheckBox.Size = new System.Drawing.Size(126, 16);
            this.overwriteCheckBox.TabIndex = 1;
            this.overwriteCheckBox.Text = "保留已有启动项（/addlast）";
            this.overwriteCheckBox.UseVisualStyleBackColor = true;

            // 
            // repairButton
            // 
            this.repairButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.repairButton.Location = new System.Drawing.Point(3, 134);
            this.repairButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.repairButton.Name = "repairButton";
            this.repairButton.Size = new System.Drawing.Size(890, 33);
            this.repairButton.TabIndex = 6;
            this.repairButton.Text = "   执 行 修 复";
            this.repairButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.repairButton.UseVisualStyleBackColor = false;

            // 
            // repairResultLabel
            // 
            this.repairResultLabel.AutoSize = false;
            this.repairResultLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.repairResultLabel.Location = new System.Drawing.Point(0, 258);
            this.repairResultLabel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.repairResultLabel.Name = "repairResultLabel";
            this.repairResultLabel.Size = new System.Drawing.Size(916, 24);
            this.repairResultLabel.TabIndex = 9;
            this.repairResultLabel.Text = "  执行结果：";
            this.repairResultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // repairResultTextBox
            // 
            this.repairResultTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.repairResultTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.repairResultTextBox.Font = new System.Drawing.Font("Consolas", 9F);
            this.repairResultTextBox.Location = new System.Drawing.Point(0, 286);
            this.repairResultTextBox.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.repairResultTextBox.Name = "repairResultTextBox";
            this.repairResultTextBox.ReadOnly = true;
            this.repairResultTextBox.Size = new System.Drawing.Size(916, 198);
            this.repairResultTextBox.TabIndex = 7;
            this.repairResultTextBox.Text = "";

            // 选项卡 3：常见启动项设置 - 导出/导入
            // 
            // exportGroupBox
            // 
            this.exportGroupBox.Controls.Add(this.exportTableLayout);
            this.exportGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exportGroupBox.Location = new System.Drawing.Point(3, 3);
            this.exportGroupBox.Margin = new System.Windows.Forms.Padding(5);
            this.exportGroupBox.Name = "exportGroupBox";
            this.exportGroupBox.Padding = new System.Windows.Forms.Padding(5);
            this.exportGroupBox.Size = new System.Drawing.Size(934, 248);
            this.exportGroupBox.TabIndex = 4;
            this.exportGroupBox.TabStop = false;
            this.exportGroupBox.Text = "导出 BCD 配置";

            // 
            // exportTableLayout
            // 
            this.exportTableLayout.ColumnCount = 3;
            this.exportTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.exportTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.exportTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.exportTableLayout.Controls.Add(this.exportPathTextBox, 0, 0);
            this.exportTableLayout.Controls.Add(this.exportBrowseButton, 1, 0);
            this.exportTableLayout.Controls.Add(this.exportButton, 2, 0);
            this.exportTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exportTableLayout.Location = new System.Drawing.Point(5, 19);
            this.exportTableLayout.Name = "exportTableLayout";
            this.exportTableLayout.RowCount = 1;
            this.exportTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.exportTableLayout.Size = new System.Drawing.Size(924, 224);
            this.exportTableLayout.TabIndex = 0;

            // 
            // exportPathTextBox
            // 
            this.exportPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            this.exportPathTextBox.Location = new System.Drawing.Point(3, 0);
            this.exportPathTextBox.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.exportPathTextBox.Name = "exportPathTextBox";
            this.exportPathTextBox.Size = new System.Drawing.Size(738, 21);
            this.exportPathTextBox.TabIndex = 0;


            // 
            // exportBrowseButton
            // 
            this.exportBrowseButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.exportBrowseButton.AutoSize = true;
            this.exportBrowseButton.Location = new System.Drawing.Point(747, 0);
            this.exportBrowseButton.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.exportBrowseButton.Name = "exportBrowseButton";
            this.exportBrowseButton.Size = new System.Drawing.Size(84, 27);
            this.exportBrowseButton.TabIndex = 1;
            this.exportBrowseButton.Text = "浏览";
            this.exportBrowseButton.UseVisualStyleBackColor = true;


            // 
            // exportButton
            // 
            this.exportButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.exportButton.AutoSize = true;
            this.exportButton.Location = new System.Drawing.Point(837, 0);
            this.exportButton.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(84, 27);
            this.exportButton.TabIndex = 2;
            this.exportButton.Text = "导出";
            this.exportButton.UseVisualStyleBackColor = true;


            // 
            // importGroupBox
            // 
            this.importGroupBox.BackColor = System.Drawing.Color.FromArgb(255, 242, 242);
            this.importGroupBox.Controls.Add(this.importTableLayout);
            this.importGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importGroupBox.ForeColor = System.Drawing.Color.FromArgb(180, 50, 50);
            this.importGroupBox.Location = new System.Drawing.Point(3, 257);
            this.importGroupBox.Margin = new System.Windows.Forms.Padding(5);
            this.importGroupBox.Name = "importGroupBox";
            this.importGroupBox.Padding = new System.Windows.Forms.Padding(5);
            this.importGroupBox.Size = new System.Drawing.Size(934, 248);
            this.importGroupBox.TabIndex = 5;
            this.importGroupBox.TabStop = false;
            this.importGroupBox.Text = "⚠ 导入 BCD 配置（危险操作）";

            // 
            // importTableLayout
            // 
            this.importTableLayout.ColumnCount = 3;
            this.importTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.importTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.importTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.importTableLayout.Controls.Add(this.importPathTextBox, 0, 0);
            this.importTableLayout.Controls.Add(this.importBrowseButton, 1, 0);
            this.importTableLayout.Controls.Add(this.importButton, 2, 0);
            this.importTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importTableLayout.Location = new System.Drawing.Point(5, 19);
            this.importTableLayout.Name = "importTableLayout";
            this.importTableLayout.RowCount = 1;
            this.importTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.importTableLayout.Size = new System.Drawing.Size(924, 224);
            this.importTableLayout.TabIndex = 0;

            // 
            // importPathTextBox
            // 
            this.importPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            this.importPathTextBox.Location = new System.Drawing.Point(3, 0);
            this.importPathTextBox.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.importPathTextBox.Name = "importPathTextBox";
            this.importPathTextBox.Size = new System.Drawing.Size(738, 21);
            this.importPathTextBox.TabIndex = 0;


            // 
            // importBrowseButton
            // 
            this.importBrowseButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.importBrowseButton.AutoSize = true;
            this.importBrowseButton.Location = new System.Drawing.Point(747, 0);
            this.importBrowseButton.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.importBrowseButton.Name = "importBrowseButton";
            this.importBrowseButton.Size = new System.Drawing.Size(84, 27);
            this.importBrowseButton.TabIndex = 1;
            this.importBrowseButton.Text = "浏览";
            this.importBrowseButton.UseVisualStyleBackColor = true;


            // 
            // importButton
            // 
            this.importButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.importButton.AutoSize = true;
            this.importButton.Location = new System.Drawing.Point(837, 0);
            this.importButton.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(84, 27);
            this.importButton.TabIndex = 2;
            this.importButton.Text = "⚠ 导入";
            this.importButton.UseVisualStyleBackColor = true;



            // 主窗体
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(960, 700);
            this.Controls.Add(this.mainTableLayout);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MinimumSize = new System.Drawing.Size(960, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "panbcdedit - BCD 编辑器";

            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.mainTableLayout.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.overviewPanel.ResumeLayout(false);
            this.overviewTableLayout.ResumeLayout(false);
            this.overviewTableLayout.PerformLayout();
            this.managementPanel.ResumeLayout(false);
            this.managementTableLayout.ResumeLayout(false);
            this.managementTableLayout.PerformLayout();
            this.managementInnerTableLayout.ResumeLayout(false);
            this.managementButtonsFlow.ResumeLayout(false);
            this.managementButtonsFlow.PerformLayout();
            this.createGroupBox.ResumeLayout(false);
            this.createTableLayout.ResumeLayout(false);
            this.createTableLayout.PerformLayout();
            this.settingsPanel.ResumeLayout(false);
            this.settingsTableLayout.ResumeLayout(false);
            this.defaultEntryGroupBox.ResumeLayout(false);
            this.defaultEntryFlow.ResumeLayout(false);
            this.defaultEntryFlow.PerformLayout();
            this.timeoutGroupBox.ResumeLayout(false);
            this.timeoutFlow.ResumeLayout(false);
            this.timeoutFlow.PerformLayout();
            this.displayOrderGroupBox.ResumeLayout(false);
            this.displayOrderTableLayout.ResumeLayout(false);
            this.displayOrderButtonsFlow.ResumeLayout(false);
            this.displayOrderButtonsFlow.PerformLayout();
            this.forceMenuGroupBox.ResumeLayout(false);
            this.forceMenuFlow.ResumeLayout(false);
            this.forceMenuFlow.PerformLayout();
            this.advancedTableLayout.ResumeLayout(false);
            this.advancedTableLayout.PerformLayout();
            this.advancedPreviewTableLayout.ResumeLayout(false);
            this.advancedPreviewTableLayout.PerformLayout();
            this.targetEntryTableLayout.ResumeLayout(false);
            this.targetEntryTableLayout.PerformLayout();
            this.advancedSwitchesTableLayout.ResumeLayout(false);
            this.advancedSwitchesTableLayout.PerformLayout();
            this.debugTestsFlow.ResumeLayout(false);
            this.debugTestsFlow.PerformLayout();
            this.securityDriversFlow.ResumeLayout(false);
            this.securityDriversFlow.PerformLayout();
            this.displayMemoryFlow.ResumeLayout(false);
            this.displayMemoryFlow.PerformLayout();
            this.virtualizationFlow.ResumeLayout(false);
            this.virtualizationFlow.PerformLayout();
            this.repairPanel.ResumeLayout(false);
            this.repairTableLayout.ResumeLayout(false);
            this.repairTableLayout.PerformLayout();
            this.repairConfigGroupBox.ResumeLayout(false);
            this.repairConfigLayout.ResumeLayout(false);
            this.repairConfigLayout.PerformLayout();
            this.windowsDirPanel.ResumeLayout(false);
            this.windowsDirPanel.PerformLayout();
            this.exportGroupBox.ResumeLayout(false);
            this.exportTableLayout.ResumeLayout(false);
            this.exportTableLayout.PerformLayout();
            this.importGroupBox.ResumeLayout(false);
            this.importTableLayout.ResumeLayout(false);
            this.importTableLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
