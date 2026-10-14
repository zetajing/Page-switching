namespace Page_switching.panel;

partial class Config
{
    private Panel pagePanel;
    private AntdUI.Panel controlStationGroup;
        private AntdUI.Label controlStationGroupCaption;
    private TableLayoutPanel controlStationLayout;
    private AntdUI.Label controlStationCaptionLabel;
    private AntdUI.Select controlStationComboBox;
    private AntdUI.Button saveControlStationButton;
    private AntdUI.Label controlStationStateLabel;

    private System.ComponentModel.IContainer components = null!;
    private TableLayoutPanel rootLayout;
    private AntdUI.Label titleLabel;
    private AntdUI.Label subtitleLabel;
    private AntdUI.Panel operationLogGroup;
        private AntdUI.Label operationLogGroupCaption;
    private TableLayoutPanel operationLogLayout;
    private AntdUI.Label operationLogDirectoryLabel;
    private AntdUI.Input operationLogDirectoryTextBox;
    private AntdUI.Button browseOperationLogDirectoryButton;
    private AntdUI.Button saveOperationLogDirectoryButton;
    private AntdUI.Label operationLogStateLabel;
    private AntdUI.Panel routerGroup;
        private AntdUI.Label routerGroupCaption;
    private TableLayoutPanel routerLayout;
    private AntdUI.Checkbox routerEnabledCheckBox;
    private AntdUI.Label routerStateLabel;
    private AntdUI.Label routerNameLabel;
    private AntdUI.Input routerNameTextBox;
    private AntdUI.Label localNetIdLabel;
    private AntdUI.Input localNetIdTextBox;
    private AntdUI.Label routerTcpPortLabel;
    private AntdUI.InputNumber routerTcpPortInput;
    private AntdUI.Label remoteNameLabel;
    private AntdUI.Input remoteNameTextBox;
    private AntdUI.Label remoteAddressLabel;
    private AntdUI.Input remoteAddressTextBox;
    private AntdUI.Label remoteNetIdLabel;
    private AntdUI.Input remoteNetIdTextBox;
    private AntdUI.Button saveRouterButton;
    private AntdUI.Label saveResultLabel;
    private AntdUI.Label routerNoticeLabel;
    private AntdUI.Panel waveGeneratorGroup;
        private AntdUI.Label waveGeneratorGroupCaption;
    private TableLayoutPanel waveGeneratorLayout;
    private AntdUI.Label waveGeneratorPathLabel;
    private AntdUI.Input waveGeneratorPathTextBox;
    private AntdUI.Button browseWaveGeneratorButton;
    private AntdUI.Button saveWaveGeneratorButton;
    private AntdUI.Label waveGeneratorStateLabel;
    private AntdUI.Label waveGeneratorModeLabel;
    private AntdUI.Select waveGeneratorModeComboBox;
    private AntdUI.Label waveProgramDirectoryLabel;
    private AntdUI.Input waveProgramDirectoryTextBox;
    private AntdUI.Button browseWaveProgramDirectoryButton;
    private AntdUI.Button saveWaveProgramDirectoryButton;
    private AntdUI.Panel _databaseGroup;
        private AntdUI.Label _databaseGroupCaption;
    private TableLayoutPanel _databaseLayout;
    private AntdUI.Checkbox _databaseEnabledCheckBox;
    private AntdUI.Input _databaseConnectionTextBox;
    private AntdUI.Label _databaseUserNameLabel;
    private AntdUI.Input _databaseUserNameTextBox;
    private AntdUI.Label _databasePasswordLabel;
    private AntdUI.Input _databasePasswordTextBox;
    private AntdUI.Button _saveDatabaseButton;
    private AntdUI.Label _databaseStateLabel;

    // 释放设计器创建的配置页面组件。
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    // 创建设计器中的配置页面控件并绑定事件。
    private void InitializeComponent()
    {
        pagePanel = new Panel();
        rootLayout = new TableLayoutPanel();
        titleLabel = new AntdUI.Label();
        subtitleLabel = new AntdUI.Label();
        controlStationGroup = new AntdUI.Panel();
            controlStationGroupCaption = new AntdUI.Label();
        controlStationLayout = new TableLayoutPanel();
        controlStationCaptionLabel = new AntdUI.Label();
        controlStationComboBox = new AntdUI.Select();
            controlStationComboBox.WaveSize = 0;
            controlStationComboBox.Radius = 6;
            controlStationComboBox.List = true;
        saveControlStationButton = new AntdUI.Button();
            saveControlStationButton.Radius = 6;
        controlStationStateLabel = new AntdUI.Label();
        operationLogGroup = new AntdUI.Panel();
            operationLogGroupCaption = new AntdUI.Label();
        operationLogLayout = new TableLayoutPanel();
        operationLogDirectoryLabel = new AntdUI.Label();
        operationLogDirectoryTextBox = new AntdUI.Input();
            operationLogDirectoryTextBox.WaveSize = 0;
            operationLogDirectoryTextBox.Radius = 6;
        browseOperationLogDirectoryButton = new AntdUI.Button();
            browseOperationLogDirectoryButton.Radius = 6;
            browseOperationLogDirectoryButton.BorderWidth = 1F;
        saveOperationLogDirectoryButton = new AntdUI.Button();
            saveOperationLogDirectoryButton.Radius = 6;
        operationLogStateLabel = new AntdUI.Label();
        routerGroup = new AntdUI.Panel();
            routerGroupCaption = new AntdUI.Label();
        routerLayout = new TableLayoutPanel();
        routerEnabledCheckBox = new AntdUI.Checkbox();
        routerStateLabel = new AntdUI.Label();
        routerNameLabel = new AntdUI.Label();
        routerNameTextBox = new AntdUI.Input();
            routerNameTextBox.WaveSize = 0;
            routerNameTextBox.Radius = 6;
        localNetIdLabel = new AntdUI.Label();
        localNetIdTextBox = new AntdUI.Input();
            localNetIdTextBox.WaveSize = 0;
            localNetIdTextBox.Radius = 6;
        routerTcpPortLabel = new AntdUI.Label();
        routerTcpPortInput = new AntdUI.InputNumber();
            routerTcpPortInput.WaveSize = 0;
            routerTcpPortInput.Radius = 6;
        remoteNameLabel = new AntdUI.Label();
        remoteNameTextBox = new AntdUI.Input();
            remoteNameTextBox.WaveSize = 0;
            remoteNameTextBox.Radius = 6;
        remoteAddressLabel = new AntdUI.Label();
        remoteAddressTextBox = new AntdUI.Input();
            remoteAddressTextBox.WaveSize = 0;
            remoteAddressTextBox.Radius = 6;
        remoteNetIdLabel = new AntdUI.Label();
        remoteNetIdTextBox = new AntdUI.Input();
            remoteNetIdTextBox.WaveSize = 0;
            remoteNetIdTextBox.Radius = 6;
        saveRouterButton = new AntdUI.Button();
            saveRouterButton.Radius = 6;
        saveResultLabel = new AntdUI.Label();
        routerNoticeLabel = new AntdUI.Label();
        waveGeneratorGroup = new AntdUI.Panel();
            waveGeneratorGroupCaption = new AntdUI.Label();
        waveGeneratorLayout = new TableLayoutPanel();
        waveGeneratorPathLabel = new AntdUI.Label();
        waveGeneratorPathTextBox = new AntdUI.Input();
            waveGeneratorPathTextBox.WaveSize = 0;
            waveGeneratorPathTextBox.Radius = 6;
        browseWaveGeneratorButton = new AntdUI.Button();
            browseWaveGeneratorButton.Radius = 6;
            browseWaveGeneratorButton.BorderWidth = 1F;
        saveWaveGeneratorButton = new AntdUI.Button();
            saveWaveGeneratorButton.Radius = 6;
        waveGeneratorStateLabel = new AntdUI.Label();
        waveGeneratorModeLabel = new AntdUI.Label();
        waveGeneratorModeComboBox = new AntdUI.Select();
            waveGeneratorModeComboBox.WaveSize = 0;
            waveGeneratorModeComboBox.Radius = 6;
            waveGeneratorModeComboBox.List = true;
        waveProgramDirectoryLabel = new AntdUI.Label();
        waveProgramDirectoryTextBox = new AntdUI.Input();
            waveProgramDirectoryTextBox.WaveSize = 0;
            waveProgramDirectoryTextBox.Radius = 6;
        browseWaveProgramDirectoryButton = new AntdUI.Button();
            browseWaveProgramDirectoryButton.Radius = 6;
            browseWaveProgramDirectoryButton.BorderWidth = 1F;
        saveWaveProgramDirectoryButton = new AntdUI.Button();
            saveWaveProgramDirectoryButton.Radius = 6;
        _databaseGroup = new AntdUI.Panel();
            _databaseGroupCaption = new AntdUI.Label();
        _databaseLayout = new TableLayoutPanel();
        _databaseEnabledCheckBox = new AntdUI.Checkbox();
        _databaseConnectionTextBox = new AntdUI.Input();
            _databaseConnectionTextBox.WaveSize = 0;
            _databaseConnectionTextBox.Radius = 6;
        _databaseUserNameLabel = new AntdUI.Label();
        _databaseUserNameTextBox = new AntdUI.Input();
            _databaseUserNameTextBox.WaveSize = 0;
            _databaseUserNameTextBox.Radius = 6;
        _databasePasswordLabel = new AntdUI.Label();
        _databasePasswordTextBox = new AntdUI.Input();
            _databasePasswordTextBox.WaveSize = 0;
            _databasePasswordTextBox.Radius = 6;
        _saveDatabaseButton = new AntdUI.Button();
            _saveDatabaseButton.Radius = 6;
        _databaseStateLabel = new AntdUI.Label();
        pagePanel.SuspendLayout();
        rootLayout.SuspendLayout();
        controlStationGroup.SuspendLayout();
        controlStationLayout.SuspendLayout();
        operationLogGroup.SuspendLayout();
        operationLogLayout.SuspendLayout();
        routerGroup.SuspendLayout();
        routerLayout.SuspendLayout();
        waveGeneratorGroup.SuspendLayout();
        waveGeneratorLayout.SuspendLayout();
        _databaseGroup.SuspendLayout();
        _databaseLayout.SuspendLayout();
        SuspendLayout();
        //
        // pagePanel
        //
        pagePanel.AutoScroll = true;
        pagePanel.Controls.Add(rootLayout);
        pagePanel.Dock = DockStyle.Fill;
        pagePanel.Location = new Point(0, 0);
        pagePanel.Name = "pagePanel";
        pagePanel.Size = new Size(860, 720);
        pagePanel.TabIndex = 0;
        //
        // rootLayout
        //
        rootLayout.AutoSize = true;
        rootLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        rootLayout.BackColor = Color.FromArgb(241, 245, 249);
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(titleLabel, 0, 0);
        rootLayout.Controls.Add(subtitleLabel, 0, 1);
        rootLayout.Controls.Add(controlStationGroup, 0, 2);
        rootLayout.Controls.Add(operationLogGroup, 0, 3);
        rootLayout.Controls.Add(routerGroup, 0, 4);
        rootLayout.Controls.Add(waveGeneratorGroup, 0, 5);
        rootLayout.Controls.Add(_databaseGroup, 0, 6);
        rootLayout.Dock = DockStyle.Top;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Margin = new Padding(2, 3, 2, 3);
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(16);
        rootLayout.RowCount = 7;
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 116F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 292F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 172F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 156F));
        rootLayout.Size = new Size(843, 956);
        rootLayout.TabIndex = 0;
        //
        // titleLabel
        //
        titleLabel.Dock = DockStyle.Fill;
        titleLabel.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
        titleLabel.Location = new Point(18, 16);
        titleLabel.Margin = new Padding(2, 0, 2, 0);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(807, 42);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "系统配置";
        titleLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // subtitleLabel
        //
        subtitleLabel.Dock = DockStyle.Fill;
        subtitleLabel.Font = new Font("Microsoft YaHei UI", 9F);
        subtitleLabel.ForeColor = Color.FromArgb(71, 85, 105);
        subtitleLabel.Location = new Point(18, 58);
        subtitleLabel.Margin = new Padding(2, 0, 2, 0);
        subtitleLabel.Name = "subtitleLabel";
        subtitleLabel.Size = new Size(807, 26);
        subtitleLabel.TabIndex = 1;
        subtitleLabel.Text = "本站身份、日志保存位置、手动控制 ADS 路由、波形程序和数据库连接";
        subtitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // controlStationGroup
        //
        controlStationGroup.BackColor = Color.White;
        controlStationGroup.Controls.Add(controlStationLayout);
        controlStationGroup.Dock = DockStyle.Fill;
        controlStationGroup.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
        controlStationGroup.ForeColor = Color.FromArgb(15, 23, 42);
        controlStationGroup.Location = new Point(18, 87);
        controlStationGroup.Margin = new Padding(2, 3, 2, 3);
        controlStationGroup.Name = "controlStationGroup";
        controlStationGroup.Padding = new Padding(11, 32, 11, 8);
        controlStationGroup.Size = new Size(807, 114);
        controlStationGroup.TabIndex = 2;
        // AntdUI 面板用独立标题标签保留原分组标题，布局仍可在 Designer 编辑。
            controlStationGroupCaption.Text = "本站身份";
            controlStationGroupCaption.AutoSize = true;
            controlStationGroupCaption.Name = "controlStationGroupCaption";
            controlStationGroupCaption.Location = new Point(10, 2);
            controlStationGroupCaption.ForeColor = Color.FromArgb(15, 23, 42);
            controlStationGroup.BorderWidth = 1F;
            controlStationGroup.BorderColor = Color.FromArgb(203, 213, 225);
            controlStationGroup.Radius = 6;
            controlStationGroup.Controls.Add(controlStationGroupCaption);
            controlStationGroupCaption.BringToFront();
        //
        // controlStationLayout
        //
        controlStationLayout.ColumnCount = 3;
        controlStationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
        controlStationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        controlStationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 128F));
        controlStationLayout.Controls.Add(controlStationCaptionLabel, 0, 0);
        controlStationLayout.Controls.Add(controlStationComboBox, 1, 0);
        controlStationLayout.Controls.Add(saveControlStationButton, 2, 0);
        controlStationLayout.Controls.Add(controlStationStateLabel, 0, 1);
        controlStationLayout.Dock = DockStyle.Fill;
        controlStationLayout.Location = new Point(11, 32);
        controlStationLayout.Margin = new Padding(0);
        controlStationLayout.Name = "controlStationLayout";
        controlStationLayout.RowCount = 2;
        controlStationLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        controlStationLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        controlStationLayout.Size = new Size(785, 74);
        controlStationLayout.TabIndex = 0;
        //
        // controlStationCaptionLabel
        //
        controlStationCaptionLabel.Dock = DockStyle.Fill;
        controlStationCaptionLabel.Font = new Font("Microsoft YaHei UI", 9F);
        controlStationCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
        controlStationCaptionLabel.Location = new Point(2, 0);
        controlStationCaptionLabel.Margin = new Padding(2, 0, 2, 0);
        controlStationCaptionLabel.Name = "controlStationCaptionLabel";
        controlStationCaptionLabel.Size = new Size(136, 36);
        controlStationCaptionLabel.TabIndex = 0;
        controlStationCaptionLabel.Text = "本机所属控制端";
        controlStationCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // controlStationComboBox
        //
        controlStationComboBox.Dock = DockStyle.Fill;
        controlStationComboBox.Font = new Font("Microsoft YaHei UI", 9F);
        controlStationComboBox.Items.AddRange(new object[] { "未配置", "造波控制室", "拖车本地", "远程操作室" });
        controlStationComboBox.Location = new Point(142, 5);
        controlStationComboBox.Margin = new Padding(2, 5, 10, 5);
        controlStationComboBox.Name = "controlStationComboBox";
        controlStationComboBox.Size = new Size(505, 25);
        controlStationComboBox.TabIndex = 0;
        //
        // saveControlStationButton
        //
        saveControlStationButton.BackColor = Color.FromArgb(29, 78, 216);
            saveControlStationButton.DefaultBack = Color.FromArgb(29, 78, 216);
        saveControlStationButton.Cursor = Cursors.Hand;
        saveControlStationButton.Dock = DockStyle.Fill;
        saveControlStationButton.BorderWidth = 0;
        saveControlStationButton.Font = new Font("Microsoft YaHei UI", 9F);
        saveControlStationButton.ForeColor = Color.White;
        saveControlStationButton.Location = new Point(659, 3);
        saveControlStationButton.Margin = new Padding(2, 3, 2, 3);
        saveControlStationButton.Name = "saveControlStationButton";
        saveControlStationButton.Size = new Size(124, 30);
        saveControlStationButton.TabIndex = 1;
        saveControlStationButton.Text = "保存本站身份";
        saveControlStationButton.Click += SaveControlStationButton_Click;
        //
        // controlStationStateLabel
        //
        controlStationLayout.SetColumnSpan(controlStationStateLabel, 3);
        controlStationStateLabel.Dock = DockStyle.Fill;
        controlStationStateLabel.Font = new Font("Microsoft YaHei UI", 8F);
        controlStationStateLabel.ForeColor = Color.FromArgb(100, 116, 139);
        controlStationStateLabel.Location = new Point(2, 36);
        controlStationStateLabel.Margin = new Padding(2, 0, 2, 0);
        controlStationStateLabel.Name = "controlStationStateLabel";
        controlStationStateLabel.Size = new Size(781, 38);
        controlStationStateLabel.TabIndex = 2;
        controlStationStateLabel.Text = "选择本机所属站点后保存；本站身份不代表取得控制权，控制权由 PLC 统一管理。";
        controlStationStateLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // operationLogGroup
        //
        operationLogGroup.BackColor = Color.White;
        operationLogGroup.Controls.Add(operationLogLayout);
        operationLogGroup.Dock = DockStyle.Fill;
        operationLogGroup.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
        operationLogGroup.ForeColor = Color.FromArgb(15, 23, 42);
        operationLogGroup.Location = new Point(18, 207);
        operationLogGroup.Margin = new Padding(2, 3, 2, 3);
        operationLogGroup.Name = "operationLogGroup";
        operationLogGroup.Padding = new Padding(11, 32, 11, 8);
        operationLogGroup.Size = new Size(807, 110);
        operationLogGroup.TabIndex = 2;
        // AntdUI 面板用独立标题标签保留原分组标题，布局仍可在 Designer 编辑。
            operationLogGroupCaption.Text = "操作日志";
            operationLogGroupCaption.AutoSize = true;
            operationLogGroupCaption.Name = "operationLogGroupCaption";
            operationLogGroupCaption.Location = new Point(10, 2);
            operationLogGroupCaption.ForeColor = Color.FromArgb(15, 23, 42);
            operationLogGroup.BorderWidth = 1F;
            operationLogGroup.BorderColor = Color.FromArgb(203, 213, 225);
            operationLogGroup.Radius = 6;
            operationLogGroup.Controls.Add(operationLogGroupCaption);
            operationLogGroupCaption.BringToFront();
        //
        // operationLogLayout
        //
        operationLogLayout.ColumnCount = 4;
        operationLogLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
        operationLogLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        operationLogLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
        operationLogLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        operationLogLayout.Controls.Add(operationLogDirectoryLabel, 0, 0);
        operationLogLayout.Controls.Add(operationLogDirectoryTextBox, 1, 0);
        operationLogLayout.Controls.Add(browseOperationLogDirectoryButton, 2, 0);
        operationLogLayout.Controls.Add(saveOperationLogDirectoryButton, 3, 0);
        operationLogLayout.Controls.Add(operationLogStateLabel, 0, 1);
        operationLogLayout.Dock = DockStyle.Fill;
        operationLogLayout.Location = new Point(11, 25);
        operationLogLayout.Margin = new Padding(2);
        operationLogLayout.Name = "operationLogLayout";
        operationLogLayout.RowCount = 2;
        operationLogLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        operationLogLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        operationLogLayout.Size = new Size(785, 77);
        operationLogLayout.TabIndex = 0;
        //
        // operationLogDirectoryLabel
        //
        operationLogDirectoryLabel.Dock = DockStyle.Fill;
        operationLogDirectoryLabel.Font = new Font("Microsoft YaHei UI", 9F);
        operationLogDirectoryLabel.Location = new Point(2, 0);
        operationLogDirectoryLabel.Margin = new Padding(2, 0, 2, 0);
        operationLogDirectoryLabel.Name = "operationLogDirectoryLabel";
        operationLogDirectoryLabel.Size = new Size(136, 36);
        operationLogDirectoryLabel.TabIndex = 0;
        operationLogDirectoryLabel.Text = "日志保存位置";
        operationLogDirectoryLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // operationLogDirectoryTextBox
        //
        operationLogDirectoryTextBox.Dock = DockStyle.Fill;
        operationLogDirectoryTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        operationLogDirectoryTextBox.Location = new Point(142, 3);
        operationLogDirectoryTextBox.Margin = new Padding(2, 3, 2, 3);
        operationLogDirectoryTextBox.Name = "operationLogDirectoryTextBox";
        operationLogDirectoryTextBox.PlaceholderText = "留空使用默认日志目录";
        operationLogDirectoryTextBox.Size = new Size(439, 23);
        operationLogDirectoryTextBox.TabIndex = 0;
        //
        // browseOperationLogDirectoryButton
        //
        browseOperationLogDirectoryButton.BackColor = Color.White;
            browseOperationLogDirectoryButton.DefaultBack = Color.White;
        browseOperationLogDirectoryButton.Cursor = Cursors.Hand;
        browseOperationLogDirectoryButton.Dock = DockStyle.Fill;
        browseOperationLogDirectoryButton.Font = new Font("Microsoft YaHei UI", 9F);
        browseOperationLogDirectoryButton.Location = new Point(585, 3);
        browseOperationLogDirectoryButton.Margin = new Padding(2, 3, 2, 3);
        browseOperationLogDirectoryButton.Name = "browseOperationLogDirectoryButton";
        browseOperationLogDirectoryButton.Size = new Size(86, 30);
        browseOperationLogDirectoryButton.TabIndex = 1;
        browseOperationLogDirectoryButton.Text = "选择目录";
        browseOperationLogDirectoryButton.Click += BrowseOperationLogDirectoryButton_Click;
        //
        // saveOperationLogDirectoryButton
        //
        saveOperationLogDirectoryButton.BackColor = Color.FromArgb(29, 78, 216);
            saveOperationLogDirectoryButton.DefaultBack = Color.FromArgb(29, 78, 216);
        saveOperationLogDirectoryButton.Cursor = Cursors.Hand;
        saveOperationLogDirectoryButton.Dock = DockStyle.Fill;
        saveOperationLogDirectoryButton.BorderWidth = 0;
        saveOperationLogDirectoryButton.Font = new Font("Microsoft YaHei UI", 9F);
        saveOperationLogDirectoryButton.ForeColor = Color.White;
        saveOperationLogDirectoryButton.Location = new Point(675, 3);
        saveOperationLogDirectoryButton.Margin = new Padding(2, 3, 2, 3);
        saveOperationLogDirectoryButton.Name = "saveOperationLogDirectoryButton";
        saveOperationLogDirectoryButton.Size = new Size(108, 30);
        saveOperationLogDirectoryButton.TabIndex = 2;
        saveOperationLogDirectoryButton.Text = "保存日志目录";
        saveOperationLogDirectoryButton.Click += SaveOperationLogDirectoryButton_Click;
        //
        // operationLogStateLabel
        //
        operationLogLayout.SetColumnSpan(operationLogStateLabel, 4);
        operationLogStateLabel.Dock = DockStyle.Fill;
        operationLogStateLabel.Font = new Font("Microsoft YaHei UI", 8F);
        operationLogStateLabel.ForeColor = Color.FromArgb(100, 116, 139);
        operationLogStateLabel.Location = new Point(2, 36);
        operationLogStateLabel.Margin = new Padding(2, 0, 2, 0);
        operationLogStateLabel.Name = "operationLogStateLabel";
        operationLogStateLabel.Size = new Size(781, 41);
        operationLogStateLabel.TabIndex = 3;
        operationLogStateLabel.Text = "日志按小时保存。保存目录立即生效，已有日志保留在原目录；留空恢复默认位置。";
        operationLogStateLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // routerGroup
        //
        routerGroup.BackColor = Color.White;
        routerGroup.Controls.Add(routerLayout);
        routerGroup.Dock = DockStyle.Fill;
        routerGroup.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
        routerGroup.ForeColor = Color.FromArgb(15, 23, 42);
        routerGroup.Location = new Point(18, 323);
        routerGroup.Margin = new Padding(2, 3, 2, 3);
        routerGroup.Name = "routerGroup";
        routerGroup.Padding = new Padding(16, 32, 16, 12);
        routerGroup.Size = new Size(807, 286);
        routerGroup.TabIndex = 2;
        // AntdUI 面板用独立标题标签保留原分组标题，布局仍可在 Designer 编辑。
            routerGroupCaption.Text = "手动控制 ADS 路由";
            routerGroupCaption.AutoSize = true;
            routerGroupCaption.Name = "routerGroupCaption";
            routerGroupCaption.Location = new Point(10, 2);
            routerGroupCaption.ForeColor = Color.FromArgb(15, 23, 42);
            routerGroup.BorderWidth = 1F;
            routerGroup.BorderColor = Color.FromArgb(203, 213, 225);
            routerGroup.Radius = 6;
            routerGroup.Controls.Add(routerGroupCaption);
            routerGroupCaption.BringToFront();
        //
        // routerLayout
        //
        routerLayout.ColumnCount = 4;
        routerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
        routerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        routerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
        routerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        routerLayout.Controls.Add(routerEnabledCheckBox, 0, 0);
        routerLayout.Controls.Add(routerStateLabel, 2, 0);
        routerLayout.Controls.Add(routerNameLabel, 0, 1);
        routerLayout.Controls.Add(routerNameTextBox, 1, 1);
        routerLayout.Controls.Add(localNetIdLabel, 0, 2);
        routerLayout.Controls.Add(localNetIdTextBox, 1, 2);
        routerLayout.Controls.Add(routerTcpPortLabel, 0, 3);
        routerLayout.Controls.Add(routerTcpPortInput, 1, 3);
        routerLayout.Controls.Add(remoteNameLabel, 2, 1);
        routerLayout.Controls.Add(remoteNameTextBox, 3, 1);
        routerLayout.Controls.Add(remoteAddressLabel, 2, 2);
        routerLayout.Controls.Add(remoteAddressTextBox, 3, 2);
        routerLayout.Controls.Add(remoteNetIdLabel, 2, 3);
        routerLayout.Controls.Add(remoteNetIdTextBox, 3, 3);
        routerLayout.Controls.Add(saveRouterButton, 1, 4);
        routerLayout.Controls.Add(saveResultLabel, 2, 4);
        routerLayout.Controls.Add(routerNoticeLabel, 0, 5);
        routerLayout.Dock = DockStyle.Fill;
        routerLayout.Location = new Point(16, 29);
        routerLayout.Margin = new Padding(2, 3, 2, 3);
        routerLayout.Name = "routerLayout";
        routerLayout.RowCount = 6;
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        routerLayout.Size = new Size(775, 245);
        routerLayout.TabIndex = 0;
        //
        // routerEnabledCheckBox
        //
        routerLayout.SetColumnSpan(routerEnabledCheckBox, 2);
        routerEnabledCheckBox.Dock = DockStyle.Fill;
        routerEnabledCheckBox.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        routerEnabledCheckBox.Location = new Point(2, 3);
        routerEnabledCheckBox.Margin = new Padding(2, 3, 2, 3);
        routerEnabledCheckBox.Name = "routerEnabledCheckBox";
        routerEnabledCheckBox.Size = new Size(383, 26);
        routerEnabledCheckBox.TabIndex = 0;
        routerEnabledCheckBox.Text = "启用独立 TCP Router";
        routerEnabledCheckBox.CheckedChanged += RouterEnabledCheckBox_CheckedChanged;
        //
        // routerStateLabel
        //
        routerLayout.SetColumnSpan(routerStateLabel, 2);
        routerStateLabel.Dock = DockStyle.Fill;
        routerStateLabel.Font = new Font("Microsoft YaHei UI", 9F);
        routerStateLabel.Location = new Point(389, 0);
        routerStateLabel.Margin = new Padding(2, 0, 2, 0);
        routerStateLabel.Name = "routerStateLabel";
        routerStateLabel.Size = new Size(384, 32);
        routerStateLabel.TabIndex = 1;
        routerStateLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // routerNameLabel
        //
        routerNameLabel.Dock = DockStyle.Fill;
        routerNameLabel.Font = new Font("Microsoft YaHei UI", 9F);
        routerNameLabel.ForeColor = Color.FromArgb(71, 85, 105);
        routerNameLabel.Location = new Point(2, 32);
        routerNameLabel.Margin = new Padding(2, 0, 2, 0);
        routerNameLabel.Name = "routerNameLabel";
        routerNameLabel.Size = new Size(126, 38);
        routerNameLabel.TabIndex = 2;
        routerNameLabel.Text = "Router 名称";
        routerNameLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // routerNameTextBox
        //
        routerNameTextBox.BackColor = Color.White;
        routerNameTextBox.Dock = DockStyle.Fill;
        routerNameTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        routerNameTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        routerNameTextBox.Location = new Point(132, 37);
        routerNameTextBox.Margin = new Padding(2, 5, 2, 5);
        routerNameTextBox.Name = "routerNameTextBox";
        routerNameTextBox.Size = new Size(253, 24);
        routerNameTextBox.TabIndex = 3;
        //
        // localNetIdLabel
        //
        localNetIdLabel.Dock = DockStyle.Fill;
        localNetIdLabel.Font = new Font("Microsoft YaHei UI", 9F);
        localNetIdLabel.ForeColor = Color.FromArgb(71, 85, 105);
        localNetIdLabel.Location = new Point(2, 70);
        localNetIdLabel.Margin = new Padding(2, 0, 2, 0);
        localNetIdLabel.Name = "localNetIdLabel";
        localNetIdLabel.Size = new Size(126, 38);
        localNetIdLabel.TabIndex = 4;
        localNetIdLabel.Text = "本机 AMS Net ID";
        localNetIdLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // localNetIdTextBox
        //
        localNetIdTextBox.BackColor = Color.White;
        localNetIdTextBox.Dock = DockStyle.Fill;
        localNetIdTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        localNetIdTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        localNetIdTextBox.Location = new Point(132, 75);
        localNetIdTextBox.Margin = new Padding(2, 5, 2, 5);
        localNetIdTextBox.Name = "localNetIdTextBox";
        localNetIdTextBox.Size = new Size(253, 24);
        localNetIdTextBox.TabIndex = 5;
        //
        // routerTcpPortLabel
        //
        routerTcpPortLabel.Dock = DockStyle.Fill;
        routerTcpPortLabel.Font = new Font("Microsoft YaHei UI", 9F);
        routerTcpPortLabel.ForeColor = Color.FromArgb(71, 85, 105);
        routerTcpPortLabel.Location = new Point(2, 108);
        routerTcpPortLabel.Margin = new Padding(2, 0, 2, 0);
        routerTcpPortLabel.Name = "routerTcpPortLabel";
        routerTcpPortLabel.Size = new Size(126, 38);
        routerTcpPortLabel.TabIndex = 6;
        routerTcpPortLabel.Text = "Router TCP 端口";
        routerTcpPortLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // routerTcpPortInput
        //
        routerTcpPortInput.BackColor = Color.White;
        routerTcpPortInput.Dock = DockStyle.Fill;
        routerTcpPortInput.Font = new Font("Microsoft YaHei UI", 10F);
        routerTcpPortInput.ForeColor = Color.FromArgb(15, 23, 42);
        routerTcpPortInput.Location = new Point(132, 113);
        routerTcpPortInput.Margin = new Padding(2, 5, 2, 5);
        routerTcpPortInput.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
        routerTcpPortInput.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        routerTcpPortInput.Name = "routerTcpPortInput";
        routerTcpPortInput.Size = new Size(253, 24);
        routerTcpPortInput.TabIndex = 7;
        routerTcpPortInput.Value = new decimal(new int[] { 48898, 0, 0, 0 });
        //
        // remoteNameLabel
        //
        remoteNameLabel.Dock = DockStyle.Fill;
        remoteNameLabel.Font = new Font("Microsoft YaHei UI", 9F);
        remoteNameLabel.ForeColor = Color.FromArgb(71, 85, 105);
        remoteNameLabel.Location = new Point(389, 32);
        remoteNameLabel.Margin = new Padding(2, 0, 2, 0);
        remoteNameLabel.Name = "remoteNameLabel";
        remoteNameLabel.Size = new Size(126, 38);
        remoteNameLabel.TabIndex = 8;
        remoteNameLabel.Text = "PLC 路由名称";
        remoteNameLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // remoteNameTextBox
        //
        remoteNameTextBox.BackColor = Color.White;
        remoteNameTextBox.Dock = DockStyle.Fill;
        remoteNameTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        remoteNameTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        remoteNameTextBox.Location = new Point(519, 37);
        remoteNameTextBox.Margin = new Padding(2, 5, 2, 5);
        remoteNameTextBox.Name = "remoteNameTextBox";
        remoteNameTextBox.Size = new Size(254, 24);
        remoteNameTextBox.TabIndex = 9;
        //
        // remoteAddressLabel
        //
        remoteAddressLabel.Dock = DockStyle.Fill;
        remoteAddressLabel.Font = new Font("Microsoft YaHei UI", 9F);
        remoteAddressLabel.ForeColor = Color.FromArgb(71, 85, 105);
        remoteAddressLabel.Location = new Point(389, 70);
        remoteAddressLabel.Margin = new Padding(2, 0, 2, 0);
        remoteAddressLabel.Name = "remoteAddressLabel";
        remoteAddressLabel.Size = new Size(126, 38);
        remoteAddressLabel.TabIndex = 10;
        remoteAddressLabel.Text = "PLC IP 地址";
        remoteAddressLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // remoteAddressTextBox
        //
        remoteAddressTextBox.BackColor = Color.White;
        remoteAddressTextBox.Dock = DockStyle.Fill;
        remoteAddressTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        remoteAddressTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        remoteAddressTextBox.Location = new Point(519, 75);
        remoteAddressTextBox.Margin = new Padding(2, 5, 2, 5);
        remoteAddressTextBox.Name = "remoteAddressTextBox";
        remoteAddressTextBox.Size = new Size(254, 24);
        remoteAddressTextBox.TabIndex = 11;
        //
        // remoteNetIdLabel
        //
        remoteNetIdLabel.Dock = DockStyle.Fill;
        remoteNetIdLabel.Font = new Font("Microsoft YaHei UI", 9F);
        remoteNetIdLabel.ForeColor = Color.FromArgb(71, 85, 105);
        remoteNetIdLabel.Location = new Point(389, 108);
        remoteNetIdLabel.Margin = new Padding(2, 0, 2, 0);
        remoteNetIdLabel.Name = "remoteNetIdLabel";
        remoteNetIdLabel.Size = new Size(126, 38);
        remoteNetIdLabel.TabIndex = 12;
        remoteNetIdLabel.Text = "PLC AMS Net ID";
        remoteNetIdLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // remoteNetIdTextBox
        //
        remoteNetIdTextBox.BackColor = Color.White;
        remoteNetIdTextBox.Dock = DockStyle.Fill;
        remoteNetIdTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        remoteNetIdTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        remoteNetIdTextBox.Location = new Point(519, 113);
        remoteNetIdTextBox.Margin = new Padding(2, 5, 2, 5);
        remoteNetIdTextBox.Name = "remoteNetIdTextBox";
        remoteNetIdTextBox.Size = new Size(254, 24);
        remoteNetIdTextBox.TabIndex = 13;
        //
        // saveRouterButton
        //
        saveRouterButton.Anchor = AnchorStyles.Left;
        saveRouterButton.BackColor = Color.FromArgb(29, 78, 216);
            saveRouterButton.DefaultBack = Color.FromArgb(29, 78, 216);
        saveRouterButton.Cursor = Cursors.Hand;
        saveRouterButton.BorderWidth = 0;
        saveRouterButton.BackHover = Color.FromArgb(30, 64, 175);
        saveRouterButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        saveRouterButton.ForeColor = Color.White;
        saveRouterButton.Location = new Point(132, 150);
        saveRouterButton.Margin = new Padding(2, 3, 2, 3);
        saveRouterButton.Name = "saveRouterButton";
        saveRouterButton.Size = new Size(140, 34);
        saveRouterButton.TabIndex = 14;
        saveRouterButton.Text = "保存 Router 配置";
        saveRouterButton.Click += SaveRouterButton_Click;
        //
        // saveResultLabel
        //
        routerLayout.SetColumnSpan(saveResultLabel, 2);
        saveResultLabel.Dock = DockStyle.Fill;
        saveResultLabel.Font = new Font("Microsoft YaHei UI", 9F);
        saveResultLabel.Location = new Point(389, 146);
        saveResultLabel.Margin = new Padding(2, 0, 2, 0);
        saveResultLabel.Name = "saveResultLabel";
        saveResultLabel.Size = new Size(384, 42);
        saveResultLabel.TabIndex = 15;
        saveResultLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // routerNoticeLabel
        //
        routerNoticeLabel.AutoEllipsis = true;
        routerLayout.SetColumnSpan(routerNoticeLabel, 4);
        routerNoticeLabel.Dock = DockStyle.Fill;
        routerNoticeLabel.Font = new Font("Microsoft YaHei UI", 9F);
        routerNoticeLabel.ForeColor = Color.FromArgb(100, 116, 139);
        routerNoticeLabel.Location = new Point(2, 188);
        routerNoticeLabel.Margin = new Padding(2, 0, 2, 0);
        routerNoticeLabel.Name = "routerNoticeLabel";
        routerNoticeLabel.Padding = new Padding(0, 3, 0, 0);
        routerNoticeLabel.Size = new Size(771, 57);
        routerNoticeLabel.TabIndex = 16;
        routerNoticeLabel.Text = "系统 TwinCAT Router 运行时应关闭独立 Router；PLC 需配置到本机 AMS Net ID 的返回路由。";
        //
        // waveGeneratorGroup
        //
        waveGeneratorGroup.BackColor = Color.White;
        waveGeneratorGroup.Controls.Add(waveGeneratorLayout);
        waveGeneratorGroup.Dock = DockStyle.Fill;
        waveGeneratorGroup.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
        waveGeneratorGroup.ForeColor = Color.FromArgb(15, 23, 42);
        waveGeneratorGroup.Location = new Point(18, 615);
        waveGeneratorGroup.Margin = new Padding(2, 3, 2, 3);
        waveGeneratorGroup.Name = "waveGeneratorGroup";
        waveGeneratorGroup.Padding = new Padding(11, 32, 11, 8);
        waveGeneratorGroup.Size = new Size(807, 166);
        waveGeneratorGroup.TabIndex = 3;
        // AntdUI 面板用独立标题标签保留原分组标题，布局仍可在 Designer 编辑。
            waveGeneratorGroupCaption.Text = "波形生成器";
            waveGeneratorGroupCaption.AutoSize = true;
            waveGeneratorGroupCaption.Name = "waveGeneratorGroupCaption";
            waveGeneratorGroupCaption.Location = new Point(10, 2);
            waveGeneratorGroupCaption.ForeColor = Color.FromArgb(15, 23, 42);
            waveGeneratorGroup.BorderWidth = 1F;
            waveGeneratorGroup.BorderColor = Color.FromArgb(203, 213, 225);
            waveGeneratorGroup.Radius = 6;
            waveGeneratorGroup.Controls.Add(waveGeneratorGroupCaption);
            waveGeneratorGroupCaption.BringToFront();
        //
        // waveGeneratorLayout
        //
        waveGeneratorLayout.ColumnCount = 5;
        waveGeneratorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
        waveGeneratorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        waveGeneratorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
        waveGeneratorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        waveGeneratorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 0F));
        waveGeneratorLayout.Controls.Add(waveGeneratorPathLabel, 0, 0);
        waveGeneratorLayout.Controls.Add(waveGeneratorPathTextBox, 1, 0);
        waveGeneratorLayout.Controls.Add(browseWaveGeneratorButton, 2, 0);
        waveGeneratorLayout.Controls.Add(saveWaveGeneratorButton, 3, 0);
        waveGeneratorLayout.Controls.Add(waveGeneratorStateLabel, 1, 1);
        waveGeneratorLayout.Controls.Add(waveGeneratorModeLabel, 0, 2);
        waveGeneratorLayout.Controls.Add(waveGeneratorModeComboBox, 1, 2);
        waveGeneratorLayout.Controls.Add(waveProgramDirectoryLabel, 0, 3);
        waveGeneratorLayout.Controls.Add(waveProgramDirectoryTextBox, 1, 3);
        waveGeneratorLayout.Controls.Add(browseWaveProgramDirectoryButton, 2, 3);
        waveGeneratorLayout.Controls.Add(saveWaveProgramDirectoryButton, 3, 3);
        waveGeneratorLayout.Dock = DockStyle.Fill;
        waveGeneratorLayout.Location = new Point(11, 32);
        waveGeneratorLayout.Margin = new Padding(2, 3, 2, 3);
        waveGeneratorLayout.Name = "waveGeneratorLayout";
        waveGeneratorLayout.RowCount = 4;
        waveGeneratorLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        waveGeneratorLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
        waveGeneratorLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        waveGeneratorLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        waveGeneratorLayout.Size = new Size(785, 126);
        waveGeneratorLayout.TabIndex = 0;
        //
        // waveGeneratorPathLabel
        //
        waveGeneratorPathLabel.Dock = DockStyle.Fill;
        waveGeneratorPathLabel.Font = new Font("Microsoft YaHei UI", 9F);
        waveGeneratorPathLabel.ForeColor = Color.FromArgb(71, 85, 105);
        waveGeneratorPathLabel.Location = new Point(2, 0);
        waveGeneratorPathLabel.Margin = new Padding(2, 0, 2, 0);
        waveGeneratorPathLabel.Name = "waveGeneratorPathLabel";
        waveGeneratorPathLabel.Size = new Size(136, 34);
        waveGeneratorPathLabel.TabIndex = 0;
        waveGeneratorPathLabel.Text = "WFast.exe 路径";
        waveGeneratorPathLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // waveGeneratorPathTextBox
        //
        waveGeneratorPathTextBox.BackColor = Color.White;
        waveGeneratorPathTextBox.Dock = DockStyle.Fill;
        waveGeneratorPathTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        waveGeneratorPathTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        waveGeneratorPathTextBox.Location = new Point(142, 3);
        waveGeneratorPathTextBox.Margin = new Padding(2, 3, 2, 3);
        waveGeneratorPathTextBox.Name = "waveGeneratorPathTextBox";
        waveGeneratorPathTextBox.Size = new Size(439, 23);
        waveGeneratorPathTextBox.TabIndex = 1;
        waveGeneratorPathTextBox.TextChanged += WaveGeneratorPathTextBox_TextChanged;
        //
        // browseWaveGeneratorButton
        //
        browseWaveGeneratorButton.BackColor = Color.FromArgb(248, 250, 252);
            browseWaveGeneratorButton.DefaultBack = Color.FromArgb(248, 250, 252);
        browseWaveGeneratorButton.Cursor = Cursors.Hand;
        browseWaveGeneratorButton.Dock = DockStyle.Fill;
        browseWaveGeneratorButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
        browseWaveGeneratorButton.BackHover = Color.FromArgb(226, 232, 240);
        browseWaveGeneratorButton.Font = new Font("Microsoft YaHei UI", 9F);
        browseWaveGeneratorButton.ForeColor = Color.FromArgb(15, 23, 42);
        browseWaveGeneratorButton.Location = new Point(585, 3);
        browseWaveGeneratorButton.Margin = new Padding(2, 3, 2, 3);
        browseWaveGeneratorButton.Name = "browseWaveGeneratorButton";
        browseWaveGeneratorButton.Size = new Size(86, 28);
        browseWaveGeneratorButton.TabIndex = 2;
        browseWaveGeneratorButton.Text = "浏览...";
        browseWaveGeneratorButton.Click += BrowseWaveGeneratorButton_Click;
        //
        // saveWaveGeneratorButton
        //
        saveWaveGeneratorButton.BackColor = Color.FromArgb(29, 78, 216);
            saveWaveGeneratorButton.DefaultBack = Color.FromArgb(29, 78, 216);
        saveWaveGeneratorButton.Cursor = Cursors.Hand;
        saveWaveGeneratorButton.Dock = DockStyle.Fill;
        saveWaveGeneratorButton.BorderWidth = 0;
        saveWaveGeneratorButton.BackHover = Color.FromArgb(30, 64, 175);
        saveWaveGeneratorButton.Font = new Font("Microsoft YaHei UI", 9F);
        saveWaveGeneratorButton.ForeColor = Color.White;
        saveWaveGeneratorButton.Location = new Point(675, 3);
        saveWaveGeneratorButton.Margin = new Padding(2, 3, 2, 3);
        saveWaveGeneratorButton.Name = "saveWaveGeneratorButton";
        saveWaveGeneratorButton.Size = new Size(108, 28);
        saveWaveGeneratorButton.TabIndex = 3;
        saveWaveGeneratorButton.Text = "保存方案";
        saveWaveGeneratorButton.Click += SaveWaveGeneratorButton_Click;
        //
        // waveGeneratorStateLabel
        //
        waveGeneratorLayout.SetColumnSpan(waveGeneratorStateLabel, 2);
        waveGeneratorStateLabel.Dock = DockStyle.Fill;
        waveGeneratorStateLabel.Font = new Font("Microsoft YaHei UI", 8F);
        waveGeneratorStateLabel.Location = new Point(142, 34);
        waveGeneratorStateLabel.Margin = new Padding(2, 0, 2, 0);
        waveGeneratorStateLabel.Name = "waveGeneratorStateLabel";
        waveGeneratorStateLabel.Size = new Size(529, 24);
        waveGeneratorStateLabel.TabIndex = 3;
        waveGeneratorStateLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // waveGeneratorModeLabel
        //
        waveGeneratorModeLabel.Dock = DockStyle.Fill;
        waveGeneratorModeLabel.Font = new Font("Microsoft YaHei UI", 9F);
        waveGeneratorModeLabel.ForeColor = Color.FromArgb(71, 85, 105);
        waveGeneratorModeLabel.Location = new Point(2, 58);
        waveGeneratorModeLabel.Margin = new Padding(2, 0, 2, 0);
        waveGeneratorModeLabel.Name = "waveGeneratorModeLabel";
        waveGeneratorModeLabel.Size = new Size(136, 34);
        waveGeneratorModeLabel.TabIndex = 4;
        waveGeneratorModeLabel.Text = "生成方案";
        waveGeneratorModeLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // waveGeneratorModeComboBox
        //
        waveGeneratorModeComboBox.BackColor = Color.White;
        waveGeneratorLayout.SetColumnSpan(waveGeneratorModeComboBox, 3);
        waveGeneratorModeComboBox.Dock = DockStyle.Left;
        waveGeneratorModeComboBox.Font = new Font("Microsoft YaHei UI", 9F);
        waveGeneratorModeComboBox.ForeColor = Color.FromArgb(15, 23, 42);
        waveGeneratorModeComboBox.Location = new Point(142, 61);
        waveGeneratorModeComboBox.Margin = new Padding(2, 3, 2, 3);
        waveGeneratorModeComboBox.Name = "waveGeneratorModeComboBox";
        waveGeneratorModeComboBox.Size = new Size(280, 25);
        waveGeneratorModeComboBox.TabIndex = 5;
        waveGeneratorModeComboBox.SelectedIndexChanged += WaveGeneratorModeComboBox_SelectedIndexChanged;
        //
        // waveProgramDirectoryLabel
        //
        waveProgramDirectoryLabel.Dock = DockStyle.Fill;
        waveProgramDirectoryLabel.Font = new Font("Microsoft YaHei UI", 9F);
        waveProgramDirectoryLabel.ForeColor = Color.FromArgb(71, 85, 105);
        waveProgramDirectoryLabel.Location = new Point(3, 92);
        waveProgramDirectoryLabel.Name = "waveProgramDirectoryLabel";
        waveProgramDirectoryLabel.Size = new Size(134, 34);
        waveProgramDirectoryLabel.TabIndex = 6;
        waveProgramDirectoryLabel.Text = "波形程序目录";
        waveProgramDirectoryLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // waveProgramDirectoryTextBox
        //
        waveProgramDirectoryTextBox.BackColor = Color.White;
        waveProgramDirectoryTextBox.Dock = DockStyle.Fill;
        waveProgramDirectoryTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        waveProgramDirectoryTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        waveProgramDirectoryTextBox.Location = new Point(143, 95);
        waveProgramDirectoryTextBox.Name = "waveProgramDirectoryTextBox";
        waveProgramDirectoryTextBox.Size = new Size(437, 23);
        waveProgramDirectoryTextBox.TabIndex = 6;
        //
        // browseWaveProgramDirectoryButton
        //
        browseWaveProgramDirectoryButton.BackColor = Color.FromArgb(248, 250, 252);
            browseWaveProgramDirectoryButton.DefaultBack = Color.FromArgb(248, 250, 252);
        browseWaveProgramDirectoryButton.Dock = DockStyle.Fill;
        browseWaveProgramDirectoryButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
        browseWaveProgramDirectoryButton.Font = new Font("Microsoft YaHei UI", 9F);
        browseWaveProgramDirectoryButton.Location = new Point(586, 95);
        browseWaveProgramDirectoryButton.Name = "browseWaveProgramDirectoryButton";
        browseWaveProgramDirectoryButton.Size = new Size(84, 28);
        browseWaveProgramDirectoryButton.TabIndex = 7;
        browseWaveProgramDirectoryButton.Text = "浏览目录";
        browseWaveProgramDirectoryButton.Click += BrowseWaveProgramDirectoryButton_Click;
        //
        // saveWaveProgramDirectoryButton
        //
        saveWaveProgramDirectoryButton.BackColor = Color.FromArgb(29, 78, 216);
            saveWaveProgramDirectoryButton.DefaultBack = Color.FromArgb(29, 78, 216);
        saveWaveProgramDirectoryButton.Dock = DockStyle.Fill;
        saveWaveProgramDirectoryButton.BorderWidth = 0;
        saveWaveProgramDirectoryButton.Font = new Font("Microsoft YaHei UI", 9F);
        saveWaveProgramDirectoryButton.ForeColor = Color.White;
        saveWaveProgramDirectoryButton.Location = new Point(676, 95);
        saveWaveProgramDirectoryButton.Name = "saveWaveProgramDirectoryButton";
        saveWaveProgramDirectoryButton.Size = new Size(106, 28);
        saveWaveProgramDirectoryButton.TabIndex = 8;
        saveWaveProgramDirectoryButton.Text = "保存目录";
        saveWaveProgramDirectoryButton.Click += SaveWaveProgramDirectoryButton_Click;
        //
        // _databaseGroup
        //
        _databaseGroup.BackColor = Color.White;
        _databaseGroup.Controls.Add(_databaseLayout);
        _databaseGroup.Dock = DockStyle.Fill;
        _databaseGroup.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
        _databaseGroup.ForeColor = Color.FromArgb(15, 23, 42);
        _databaseGroup.Location = new Point(18, 787);
        _databaseGroup.Margin = new Padding(2, 3, 2, 3);
        _databaseGroup.Name = "_databaseGroup";
        _databaseGroup.Padding = new Padding(11, 32, 11, 8);
        _databaseGroup.Size = new Size(807, 150);
        _databaseGroup.TabIndex = 4;
        // AntdUI 面板用独立标题标签保留原分组标题，布局仍可在 Designer 编辑。
            _databaseGroupCaption.Text = "SQL Server 任务索引";
            _databaseGroupCaption.AutoSize = true;
            _databaseGroupCaption.Name = "_databaseGroupCaption";
            _databaseGroupCaption.Location = new Point(10, 2);
            _databaseGroupCaption.ForeColor = Color.FromArgb(15, 23, 42);
            _databaseGroup.BorderWidth = 1F;
            _databaseGroup.BorderColor = Color.FromArgb(203, 213, 225);
            _databaseGroup.Radius = 6;
            _databaseGroup.Controls.Add(_databaseGroupCaption);
            _databaseGroupCaption.BringToFront();
        //
        // _databaseLayout
        //
        _databaseLayout.ColumnCount = 5;
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 128F));
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 88F));
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 113F));
        _databaseLayout.Controls.Add(_databaseEnabledCheckBox, 0, 0);
        _databaseLayout.Controls.Add(_databaseConnectionTextBox, 1, 0);
        _databaseLayout.Controls.Add(_databaseUserNameLabel, 0, 1);
        _databaseLayout.Controls.Add(_databaseUserNameTextBox, 1, 1);
        _databaseLayout.Controls.Add(_databasePasswordLabel, 2, 1);
        _databaseLayout.Controls.Add(_databasePasswordTextBox, 3, 1);
        _databaseLayout.Controls.Add(_saveDatabaseButton, 4, 0);
        _databaseLayout.Controls.Add(_databaseStateLabel, 0, 2);
        _databaseLayout.Dock = DockStyle.Fill;
        _databaseLayout.Location = new Point(11, 32);
        _databaseLayout.Margin = new Padding(2, 3, 2, 3);
        _databaseLayout.Name = "_databaseLayout";
        _databaseLayout.RowCount = 3;
        _databaseLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        _databaseLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        _databaseLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _databaseLayout.Size = new Size(785, 110);
        _databaseLayout.TabIndex = 0;
        //
        // _databaseEnabledCheckBox
        //
        _databaseEnabledCheckBox.Dock = DockStyle.Fill;
        _databaseEnabledCheckBox.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        _databaseEnabledCheckBox.Location = new Point(2, 3);
        _databaseEnabledCheckBox.Margin = new Padding(2, 3, 2, 3);
        _databaseEnabledCheckBox.Name = "_databaseEnabledCheckBox";
        _databaseEnabledCheckBox.Size = new Size(124, 30);
        _databaseEnabledCheckBox.TabIndex = 0;
        _databaseEnabledCheckBox.Text = "启用数据库";
        _databaseEnabledCheckBox.CheckedChanged += DatabaseEnabledCheckBox_CheckedChanged;
        //
        // _databaseConnectionTextBox
        //
        _databaseConnectionTextBox.BackColor = Color.White;
        _databaseLayout.SetColumnSpan(_databaseConnectionTextBox, 3);
        _databaseConnectionTextBox.Dock = DockStyle.Fill;
        _databaseConnectionTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        _databaseConnectionTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        _databaseConnectionTextBox.Location = new Point(130, 3);
        _databaseConnectionTextBox.Margin = new Padding(2, 3, 2, 3);
        _databaseConnectionTextBox.Name = "_databaseConnectionTextBox";
        _databaseConnectionTextBox.PlaceholderText = "SQL Server 连接字符串";
        _databaseConnectionTextBox.Size = new Size(540, 23);
        _databaseConnectionTextBox.TabIndex = 1;
        //
        // _databaseUserNameLabel
        //
        _databaseUserNameLabel.Dock = DockStyle.Fill;
        _databaseUserNameLabel.Font = new Font("Microsoft YaHei UI", 9F);
        _databaseUserNameLabel.ForeColor = Color.FromArgb(71, 85, 105);
        _databaseUserNameLabel.Location = new Point(2, 36);
        _databaseUserNameLabel.Margin = new Padding(2, 0, 2, 0);
        _databaseUserNameLabel.Name = "_databaseUserNameLabel";
        _databaseUserNameLabel.Size = new Size(124, 36);
        _databaseUserNameLabel.TabIndex = 2;
        _databaseUserNameLabel.Text = "账号";
        _databaseUserNameLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _databaseUserNameTextBox
        //
        _databaseUserNameTextBox.BackColor = Color.White;
        _databaseUserNameTextBox.Dock = DockStyle.Fill;
        _databaseUserNameTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        _databaseUserNameTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        _databaseUserNameTextBox.Location = new Point(130, 39);
        _databaseUserNameTextBox.Margin = new Padding(2, 3, 2, 3);
        _databaseUserNameTextBox.Name = "_databaseUserNameTextBox";
        _databaseUserNameTextBox.PlaceholderText = "SQL Server 账号";
        _databaseUserNameTextBox.Size = new Size(224, 23);
        _databaseUserNameTextBox.TabIndex = 3;
        //
        // _databasePasswordLabel
        //
        _databasePasswordLabel.Dock = DockStyle.Fill;
        _databasePasswordLabel.Font = new Font("Microsoft YaHei UI", 9F);
        _databasePasswordLabel.ForeColor = Color.FromArgb(71, 85, 105);
        _databasePasswordLabel.Location = new Point(358, 36);
        _databasePasswordLabel.Margin = new Padding(2, 0, 2, 0);
        _databasePasswordLabel.Name = "_databasePasswordLabel";
        _databasePasswordLabel.Size = new Size(84, 36);
        _databasePasswordLabel.TabIndex = 4;
        _databasePasswordLabel.Text = "密码";
        _databasePasswordLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _databasePasswordTextBox
        //
        _databasePasswordTextBox.BackColor = Color.White;
        _databasePasswordTextBox.Dock = DockStyle.Fill;
        _databasePasswordTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        _databasePasswordTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        _databasePasswordTextBox.Location = new Point(446, 39);
        _databasePasswordTextBox.Margin = new Padding(2, 3, 2, 3);
        _databasePasswordTextBox.Name = "_databasePasswordTextBox";
        _databasePasswordTextBox.PlaceholderText = "SQL Server 密码";
        _databasePasswordTextBox.Size = new Size(224, 23);
        _databasePasswordTextBox.TabIndex = 5;
        _databasePasswordTextBox.UseSystemPasswordChar = true;
        //
        // _saveDatabaseButton
        //
        _saveDatabaseButton.BackColor = Color.FromArgb(29, 78, 216);
            _saveDatabaseButton.DefaultBack = Color.FromArgb(29, 78, 216);
        _saveDatabaseButton.Cursor = Cursors.Hand;
        _saveDatabaseButton.Dock = DockStyle.Fill;
        _saveDatabaseButton.BorderWidth = 0;
        _saveDatabaseButton.BackHover = Color.FromArgb(30, 64, 175);
        _saveDatabaseButton.Font = new Font("Microsoft YaHei UI", 9F);
        _saveDatabaseButton.ForeColor = Color.White;
        _saveDatabaseButton.Location = new Point(674, 3);
        _saveDatabaseButton.Margin = new Padding(2, 3, 2, 3);
        _saveDatabaseButton.Name = "_saveDatabaseButton";
        _saveDatabaseButton.Size = new Size(109, 30);
        _saveDatabaseButton.TabIndex = 2;
        _saveDatabaseButton.Text = "保存数据库";
        _saveDatabaseButton.Click += SaveDatabaseButton_Click;
        //
        // _databaseStateLabel
        //
        _databaseLayout.SetColumnSpan(_databaseStateLabel, 5);
        _databaseStateLabel.Dock = DockStyle.Fill;
        _databaseStateLabel.Font = new Font("Microsoft YaHei UI", 8F);
        _databaseStateLabel.ForeColor = Color.FromArgb(100, 116, 139);
        _databaseStateLabel.Location = new Point(2, 72);
        _databaseStateLabel.Margin = new Padding(2, 0, 2, 0);
        _databaseStateLabel.Name = "_databaseStateLabel";
        _databaseStateLabel.Size = new Size(781, 38);
        _databaseStateLabel.TabIndex = 3;
        _databaseStateLabel.Text = "数据库日志未启用。";
        _databaseStateLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // Config
        //
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        AutoScroll = true;
        AutoScrollMinSize = new Size(860, 720);
        BackColor = Color.FromArgb(241, 245, 249);
        Controls.Add(pagePanel);
        Font = new Font("Microsoft YaHei UI", 9F);
        ForeColor = Color.FromArgb(15, 23, 42);
        Margin = new Padding(2, 3, 2, 3);
        Name = "Config";
        Size = new Size(866, 606);
        pagePanel.ResumeLayout(false);
        pagePanel.PerformLayout();
        rootLayout.ResumeLayout(false);
        controlStationGroup.ResumeLayout(false);
        controlStationLayout.ResumeLayout(false);
        operationLogGroup.ResumeLayout(false);
        operationLogLayout.ResumeLayout(false);
        operationLogLayout.PerformLayout();
        routerGroup.ResumeLayout(false);
        routerLayout.ResumeLayout(false);
        routerLayout.PerformLayout();
        waveGeneratorGroup.ResumeLayout(false);
        waveGeneratorLayout.ResumeLayout(false);
        waveGeneratorLayout.PerformLayout();
        _databaseGroup.ResumeLayout(false);
        _databaseLayout.ResumeLayout(false);
        _databaseLayout.PerformLayout();
        ResumeLayout(false);
    }
}
