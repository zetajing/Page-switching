namespace Page_switching.panel;

partial class Config
{
    private Panel pagePanel;

    private System.ComponentModel.IContainer components = null!;
    private TableLayoutPanel rootLayout;
    private Label titleLabel;
    private Label subtitleLabel;
    private GroupBox operationLogGroup;
    private TableLayoutPanel operationLogLayout;
    private Label operationLogDirectoryLabel;
    private TextBox operationLogDirectoryTextBox;
    private Button browseOperationLogDirectoryButton;
    private Button saveOperationLogDirectoryButton;
    private Label operationLogStateLabel;
    private GroupBox routerGroup;
    private TableLayoutPanel routerLayout;
    private CheckBox routerEnabledCheckBox;
    private Label routerStateLabel;
    private Label routerNameLabel;
    private TextBox routerNameTextBox;
    private Label localNetIdLabel;
    private TextBox localNetIdTextBox;
    private Label routerTcpPortLabel;
    private NumericUpDown routerTcpPortInput;
    private Label remoteNameLabel;
    private TextBox remoteNameTextBox;
    private Label remoteAddressLabel;
    private TextBox remoteAddressTextBox;
    private Label remoteNetIdLabel;
    private TextBox remoteNetIdTextBox;
    private Button saveRouterButton;
    private Label saveResultLabel;
    private Label routerNoticeLabel;
    private GroupBox waveGeneratorGroup;
    private TableLayoutPanel waveGeneratorLayout;
    private Label waveGeneratorPathLabel;
    private TextBox waveGeneratorPathTextBox;
    private Button browseWaveGeneratorButton;
    private Button saveWaveGeneratorButton;
    private Label waveGeneratorStateLabel;
    private Label waveGeneratorModeLabel;
    private ComboBox waveGeneratorModeComboBox;
    private Label waveProgramDirectoryLabel;
    private TextBox waveProgramDirectoryTextBox;
    private Button browseWaveProgramDirectoryButton;
    private Button saveWaveProgramDirectoryButton;
    private GroupBox _databaseGroup;
    private TableLayoutPanel _databaseLayout;
    private CheckBox _databaseEnabledCheckBox;
    private TextBox _databaseConnectionTextBox;
    private Label _databaseUserNameLabel;
    private TextBox _databaseUserNameTextBox;
    private Label _databasePasswordLabel;
    private TextBox _databasePasswordTextBox;
    private Button _saveDatabaseButton;
    private Label _databaseStateLabel;

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
        titleLabel = new Label();
        subtitleLabel = new Label();
        operationLogGroup = new GroupBox();
        operationLogLayout = new TableLayoutPanel();
        operationLogDirectoryLabel = new Label();
        operationLogDirectoryTextBox = new TextBox();
        browseOperationLogDirectoryButton = new Button();
        saveOperationLogDirectoryButton = new Button();
        operationLogStateLabel = new Label();
        routerGroup = new GroupBox();
        routerLayout = new TableLayoutPanel();
        routerEnabledCheckBox = new CheckBox();
        routerStateLabel = new Label();
        routerNameLabel = new Label();
        routerNameTextBox = new TextBox();
        localNetIdLabel = new Label();
        localNetIdTextBox = new TextBox();
        routerTcpPortLabel = new Label();
        routerTcpPortInput = new NumericUpDown();
        remoteNameLabel = new Label();
        remoteNameTextBox = new TextBox();
        remoteAddressLabel = new Label();
        remoteAddressTextBox = new TextBox();
        remoteNetIdLabel = new Label();
        remoteNetIdTextBox = new TextBox();
        saveRouterButton = new Button();
        saveResultLabel = new Label();
        routerNoticeLabel = new Label();
        waveGeneratorGroup = new GroupBox();
        waveGeneratorLayout = new TableLayoutPanel();
        waveGeneratorPathLabel = new Label();
        waveGeneratorPathTextBox = new TextBox();
        browseWaveGeneratorButton = new Button();
        saveWaveGeneratorButton = new Button();
        waveGeneratorStateLabel = new Label();
        waveGeneratorModeLabel = new Label();
        waveGeneratorModeComboBox = new ComboBox();
        waveProgramDirectoryLabel = new Label();
        waveProgramDirectoryTextBox = new TextBox();
        browseWaveProgramDirectoryButton = new Button();
        saveWaveProgramDirectoryButton = new Button();
        _databaseGroup = new GroupBox();
        _databaseLayout = new TableLayoutPanel();
        _databaseEnabledCheckBox = new CheckBox();
        _databaseConnectionTextBox = new TextBox();
        _databaseUserNameLabel = new Label();
        _databaseUserNameTextBox = new TextBox();
        _databasePasswordLabel = new Label();
        _databasePasswordTextBox = new TextBox();
        _saveDatabaseButton = new Button();
        _databaseStateLabel = new Label();
        operationLogGroup.SuspendLayout();
        operationLogLayout.SuspendLayout();
        pagePanel.SuspendLayout();
        rootLayout.SuspendLayout();
        routerGroup.SuspendLayout();
        routerLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)routerTcpPortInput).BeginInit();
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
        pagePanel.Margin = new Padding(4, 4, 4, 4);
        pagePanel.Name = "pagePanel";
        pagePanel.Size = new Size(1231, 758);
        pagePanel.TabIndex = 0;
        // 
        // rootLayout
        // 
        rootLayout.BackColor = Color.FromArgb(241, 245, 249);
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(titleLabel, 0, 0);
        rootLayout.Controls.Add(subtitleLabel, 0, 1);
        rootLayout.Controls.Add(operationLogGroup, 0, 2);
        rootLayout.Controls.Add(routerGroup, 0, 3);
        rootLayout.Controls.Add(waveGeneratorGroup, 0, 4);
        rootLayout.Controls.Add(_databaseGroup, 0, 5);
        // 内容按分组高度展开，由外层滚动面板确保所有配置入口可达。
        rootLayout.AutoSize = true;
        rootLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        rootLayout.Dock = DockStyle.Top;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Margin = new Padding(2, 4, 2, 4);
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(20, 20, 20, 20);
        rootLayout.RowCount = 6;
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 145F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 365F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 215F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 195F));
        rootLayout.Size = new Size(1231, 758);
        rootLayout.TabIndex = 0;
        // 
        // titleLabel
        // 
        titleLabel.Dock = DockStyle.Fill;
        titleLabel.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
        titleLabel.Location = new Point(22, 20);
        titleLabel.Margin = new Padding(2, 0, 2, 0);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(1187, 52);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "系统配置";
        titleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // subtitleLabel
        // 
        subtitleLabel.Dock = DockStyle.Fill;
        subtitleLabel.Font = new Font("Microsoft YaHei UI", 9F);
        subtitleLabel.ForeColor = Color.FromArgb(71, 85, 105);
        subtitleLabel.Location = new Point(22, 72);
        subtitleLabel.Margin = new Padding(2, 0, 2, 0);
        subtitleLabel.Name = "subtitleLabel";
        subtitleLabel.Size = new Size(1187, 32);
        subtitleLabel.TabIndex = 1;
        subtitleLabel.Text = "日志保存位置、手动控制 ADS 路由、波形程序和数据库连接";
        subtitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // operationLogGroup
        //
        operationLogGroup.BackColor = Color.White;
        operationLogGroup.Controls.Add(operationLogLayout);
        operationLogGroup.Dock = DockStyle.Fill;
        operationLogGroup.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
        operationLogGroup.ForeColor = Color.FromArgb(15, 23, 42);
        operationLogGroup.Margin = new Padding(2, 4, 2, 4);
        operationLogGroup.Name = "operationLogGroup";
        operationLogGroup.Padding = new Padding(14, 10, 14, 10);
        operationLogGroup.Size = new Size(1187, 137);
        operationLogGroup.TabIndex = 2;
        operationLogGroup.TabStop = false;
        operationLogGroup.Text = "操作日志";
        //
        // operationLogLayout
        //
        operationLogLayout.ColumnCount = 4;
        operationLogLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 175F));
        operationLogLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        operationLogLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        operationLogLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
        operationLogLayout.Controls.Add(operationLogDirectoryLabel, 0, 0);
        operationLogLayout.Controls.Add(operationLogDirectoryTextBox, 1, 0);
        operationLogLayout.Controls.Add(browseOperationLogDirectoryButton, 2, 0);
        operationLogLayout.Controls.Add(saveOperationLogDirectoryButton, 3, 0);
        operationLogLayout.Controls.Add(operationLogStateLabel, 0, 1);
        operationLogLayout.SetColumnSpan(operationLogStateLabel, 4);
        operationLogLayout.Dock = DockStyle.Fill;
        operationLogLayout.Name = "operationLogLayout";
        operationLogLayout.RowCount = 2;
        operationLogLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        operationLogLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        operationLogLayout.Size = new Size(1159, 86);
        //
        // operationLogDirectoryLabel
        //
        operationLogDirectoryLabel.Dock = DockStyle.Fill;
        operationLogDirectoryLabel.Font = new Font("Microsoft YaHei UI", 9F);
        operationLogDirectoryLabel.Margin = new Padding(2, 0, 2, 0);
        operationLogDirectoryLabel.Name = "operationLogDirectoryLabel";
        operationLogDirectoryLabel.Text = "日志保存位置";
        operationLogDirectoryLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // operationLogDirectoryTextBox
        //
        operationLogDirectoryTextBox.BorderStyle = BorderStyle.FixedSingle;
        operationLogDirectoryTextBox.Dock = DockStyle.Fill;
        operationLogDirectoryTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        operationLogDirectoryTextBox.Margin = new Padding(2, 4, 2, 4);
        operationLogDirectoryTextBox.Name = "operationLogDirectoryTextBox";
        operationLogDirectoryTextBox.PlaceholderText = "留空使用默认日志目录";
        operationLogDirectoryTextBox.TabIndex = 0;
        //
        // browseOperationLogDirectoryButton
        //
        browseOperationLogDirectoryButton.BackColor = Color.White;
        browseOperationLogDirectoryButton.Cursor = Cursors.Hand;
        browseOperationLogDirectoryButton.Dock = DockStyle.Fill;
        browseOperationLogDirectoryButton.FlatStyle = FlatStyle.Flat;
        browseOperationLogDirectoryButton.Font = new Font("Microsoft YaHei UI", 9F);
        browseOperationLogDirectoryButton.Margin = new Padding(2, 4, 2, 4);
        browseOperationLogDirectoryButton.Name = "browseOperationLogDirectoryButton";
        browseOperationLogDirectoryButton.TabIndex = 1;
        browseOperationLogDirectoryButton.Text = "选择目录";
        browseOperationLogDirectoryButton.UseVisualStyleBackColor = false;
        browseOperationLogDirectoryButton.Click += BrowseOperationLogDirectoryButton_Click;
        //
        // saveOperationLogDirectoryButton
        //
        saveOperationLogDirectoryButton.BackColor = Color.FromArgb(29, 78, 216);
        saveOperationLogDirectoryButton.Cursor = Cursors.Hand;
        saveOperationLogDirectoryButton.Dock = DockStyle.Fill;
        saveOperationLogDirectoryButton.FlatAppearance.BorderSize = 0;
        saveOperationLogDirectoryButton.FlatStyle = FlatStyle.Flat;
        saveOperationLogDirectoryButton.Font = new Font("Microsoft YaHei UI", 9F);
        saveOperationLogDirectoryButton.ForeColor = Color.White;
        saveOperationLogDirectoryButton.Margin = new Padding(2, 4, 2, 4);
        saveOperationLogDirectoryButton.Name = "saveOperationLogDirectoryButton";
        saveOperationLogDirectoryButton.TabIndex = 2;
        saveOperationLogDirectoryButton.Text = "保存日志目录";
        saveOperationLogDirectoryButton.UseVisualStyleBackColor = false;
        saveOperationLogDirectoryButton.Click += SaveOperationLogDirectoryButton_Click;
        //
        // operationLogStateLabel
        //
        operationLogStateLabel.Dock = DockStyle.Fill;
        operationLogStateLabel.Font = new Font("Microsoft YaHei UI", 8F);
        operationLogStateLabel.ForeColor = Color.FromArgb(100, 116, 139);
        operationLogStateLabel.Margin = new Padding(2, 0, 2, 0);
        operationLogStateLabel.Name = "operationLogStateLabel";
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
        routerGroup.Location = new Point(22, 108);
        routerGroup.Margin = new Padding(2, 4, 2, 4);
        routerGroup.Name = "routerGroup";
        routerGroup.Padding = new Padding(20, 15, 20, 15);
        routerGroup.Size = new Size(1187, 357);
        routerGroup.TabIndex = 2;
        routerGroup.TabStop = false;
        routerGroup.Text = "手动控制 ADS 路由";
        // 
        // routerLayout
        // 
        routerLayout.ColumnCount = 4;
        routerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 162F));
        routerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        routerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 162F));
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
        routerLayout.Location = new Point(20, 37);
        routerLayout.Margin = new Padding(2, 4, 2, 4);
        routerLayout.Name = "routerLayout";
        routerLayout.RowCount = 6;
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        routerLayout.Size = new Size(1147, 305);
        routerLayout.TabIndex = 0;
        // 
        // routerEnabledCheckBox
        // 
        routerLayout.SetColumnSpan(routerEnabledCheckBox, 2);
        routerEnabledCheckBox.Dock = DockStyle.Fill;
        routerEnabledCheckBox.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        routerEnabledCheckBox.Location = new Point(2, 4);
        routerEnabledCheckBox.Margin = new Padding(2, 4, 2, 4);
        routerEnabledCheckBox.Name = "routerEnabledCheckBox";
        routerEnabledCheckBox.Size = new Size(569, 32);
        routerEnabledCheckBox.TabIndex = 0;
        routerEnabledCheckBox.Text = "启用独立 TCP Router";
        routerEnabledCheckBox.CheckedChanged += RouterEnabledCheckBox_CheckedChanged;
        // 
        // routerStateLabel
        // 
        routerLayout.SetColumnSpan(routerStateLabel, 2);
        routerStateLabel.Dock = DockStyle.Fill;
        routerStateLabel.Font = new Font("Microsoft YaHei UI", 9F);
        routerStateLabel.Location = new Point(575, 0);
        routerStateLabel.Margin = new Padding(2, 0, 2, 0);
        routerStateLabel.Name = "routerStateLabel";
        routerStateLabel.Size = new Size(570, 40);
        routerStateLabel.TabIndex = 1;
        routerStateLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // routerNameLabel
        // 
        routerNameLabel.Dock = DockStyle.Fill;
        routerNameLabel.Font = new Font("Microsoft YaHei UI", 9F);
        routerNameLabel.ForeColor = Color.FromArgb(71, 85, 105);
        routerNameLabel.Location = new Point(2, 40);
        routerNameLabel.Margin = new Padding(2, 0, 2, 0);
        routerNameLabel.Name = "routerNameLabel";
        routerNameLabel.Size = new Size(158, 48);
        routerNameLabel.TabIndex = 2;
        routerNameLabel.Text = "Router 名称";
        routerNameLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // routerNameTextBox
        // 
        routerNameTextBox.BackColor = Color.White;
        routerNameTextBox.BorderStyle = BorderStyle.FixedSingle;
        routerNameTextBox.Dock = DockStyle.Fill;
        routerNameTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        routerNameTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        routerNameTextBox.Location = new Point(164, 46);
        routerNameTextBox.Margin = new Padding(2, 6, 2, 6);
        routerNameTextBox.Name = "routerNameTextBox";
        routerNameTextBox.Size = new Size(407, 29);
        routerNameTextBox.TabIndex = 3;
        // 
        // localNetIdLabel
        // 
        localNetIdLabel.Dock = DockStyle.Fill;
        localNetIdLabel.Font = new Font("Microsoft YaHei UI", 9F);
        localNetIdLabel.ForeColor = Color.FromArgb(71, 85, 105);
        localNetIdLabel.Location = new Point(2, 88);
        localNetIdLabel.Margin = new Padding(2, 0, 2, 0);
        localNetIdLabel.Name = "localNetIdLabel";
        localNetIdLabel.Size = new Size(158, 48);
        localNetIdLabel.TabIndex = 4;
        localNetIdLabel.Text = "本机 AMS Net ID";
        localNetIdLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // localNetIdTextBox
        // 
        localNetIdTextBox.BackColor = Color.White;
        localNetIdTextBox.BorderStyle = BorderStyle.FixedSingle;
        localNetIdTextBox.Dock = DockStyle.Fill;
        localNetIdTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        localNetIdTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        localNetIdTextBox.Location = new Point(164, 94);
        localNetIdTextBox.Margin = new Padding(2, 6, 2, 6);
        localNetIdTextBox.Name = "localNetIdTextBox";
        localNetIdTextBox.Size = new Size(407, 29);
        localNetIdTextBox.TabIndex = 5;
        // 
        // routerTcpPortLabel
        // 
        routerTcpPortLabel.Dock = DockStyle.Fill;
        routerTcpPortLabel.Font = new Font("Microsoft YaHei UI", 9F);
        routerTcpPortLabel.ForeColor = Color.FromArgb(71, 85, 105);
        routerTcpPortLabel.Location = new Point(2, 136);
        routerTcpPortLabel.Margin = new Padding(2, 0, 2, 0);
        routerTcpPortLabel.Name = "routerTcpPortLabel";
        routerTcpPortLabel.Size = new Size(158, 48);
        routerTcpPortLabel.TabIndex = 6;
        routerTcpPortLabel.Text = "Router TCP 端口";
        routerTcpPortLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // routerTcpPortInput
        // 
        routerTcpPortInput.BackColor = Color.White;
        routerTcpPortInput.BorderStyle = BorderStyle.FixedSingle;
        routerTcpPortInput.Dock = DockStyle.Fill;
        routerTcpPortInput.Font = new Font("Microsoft YaHei UI", 10F);
        routerTcpPortInput.ForeColor = Color.FromArgb(15, 23, 42);
        routerTcpPortInput.Location = new Point(164, 142);
        routerTcpPortInput.Margin = new Padding(2, 6, 2, 6);
        routerTcpPortInput.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
        routerTcpPortInput.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        routerTcpPortInput.Name = "routerTcpPortInput";
        routerTcpPortInput.Size = new Size(407, 29);
        routerTcpPortInput.TabIndex = 7;
        routerTcpPortInput.Value = new decimal(new int[] { 48898, 0, 0, 0 });
        // 
        // remoteNameLabel
        // 
        remoteNameLabel.Dock = DockStyle.Fill;
        remoteNameLabel.Font = new Font("Microsoft YaHei UI", 9F);
        remoteNameLabel.ForeColor = Color.FromArgb(71, 85, 105);
        remoteNameLabel.Location = new Point(575, 40);
        remoteNameLabel.Margin = new Padding(2, 0, 2, 0);
        remoteNameLabel.Name = "remoteNameLabel";
        remoteNameLabel.Size = new Size(158, 48);
        remoteNameLabel.TabIndex = 8;
        remoteNameLabel.Text = "PLC 路由名称";
        remoteNameLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // remoteNameTextBox
        // 
        remoteNameTextBox.BackColor = Color.White;
        remoteNameTextBox.BorderStyle = BorderStyle.FixedSingle;
        remoteNameTextBox.Dock = DockStyle.Fill;
        remoteNameTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        remoteNameTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        remoteNameTextBox.Location = new Point(737, 46);
        remoteNameTextBox.Margin = new Padding(2, 6, 2, 6);
        remoteNameTextBox.Name = "remoteNameTextBox";
        remoteNameTextBox.Size = new Size(408, 29);
        remoteNameTextBox.TabIndex = 9;
        // 
        // remoteAddressLabel
        // 
        remoteAddressLabel.Dock = DockStyle.Fill;
        remoteAddressLabel.Font = new Font("Microsoft YaHei UI", 9F);
        remoteAddressLabel.ForeColor = Color.FromArgb(71, 85, 105);
        remoteAddressLabel.Location = new Point(575, 88);
        remoteAddressLabel.Margin = new Padding(2, 0, 2, 0);
        remoteAddressLabel.Name = "remoteAddressLabel";
        remoteAddressLabel.Size = new Size(158, 48);
        remoteAddressLabel.TabIndex = 10;
        remoteAddressLabel.Text = "PLC IP 地址";
        remoteAddressLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // remoteAddressTextBox
        // 
        remoteAddressTextBox.BackColor = Color.White;
        remoteAddressTextBox.BorderStyle = BorderStyle.FixedSingle;
        remoteAddressTextBox.Dock = DockStyle.Fill;
        remoteAddressTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        remoteAddressTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        remoteAddressTextBox.Location = new Point(737, 94);
        remoteAddressTextBox.Margin = new Padding(2, 6, 2, 6);
        remoteAddressTextBox.Name = "remoteAddressTextBox";
        remoteAddressTextBox.Size = new Size(408, 29);
        remoteAddressTextBox.TabIndex = 11;
        // 
        // remoteNetIdLabel
        // 
        remoteNetIdLabel.Dock = DockStyle.Fill;
        remoteNetIdLabel.Font = new Font("Microsoft YaHei UI", 9F);
        remoteNetIdLabel.ForeColor = Color.FromArgb(71, 85, 105);
        remoteNetIdLabel.Location = new Point(575, 136);
        remoteNetIdLabel.Margin = new Padding(2, 0, 2, 0);
        remoteNetIdLabel.Name = "remoteNetIdLabel";
        remoteNetIdLabel.Size = new Size(158, 48);
        remoteNetIdLabel.TabIndex = 12;
        remoteNetIdLabel.Text = "PLC AMS Net ID";
        remoteNetIdLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // remoteNetIdTextBox
        // 
        remoteNetIdTextBox.BackColor = Color.White;
        remoteNetIdTextBox.BorderStyle = BorderStyle.FixedSingle;
        remoteNetIdTextBox.Dock = DockStyle.Fill;
        remoteNetIdTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        remoteNetIdTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        remoteNetIdTextBox.Location = new Point(737, 142);
        remoteNetIdTextBox.Margin = new Padding(2, 6, 2, 6);
        remoteNetIdTextBox.Name = "remoteNetIdTextBox";
        remoteNetIdTextBox.Size = new Size(408, 29);
        remoteNetIdTextBox.TabIndex = 13;
        // 
        // saveRouterButton
        // 
        saveRouterButton.Anchor = AnchorStyles.Left;
        saveRouterButton.BackColor = Color.FromArgb(29, 78, 216);
        saveRouterButton.Cursor = Cursors.Hand;
        saveRouterButton.FlatAppearance.BorderSize = 0;
        saveRouterButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 64, 175);
        saveRouterButton.FlatStyle = FlatStyle.Flat;
        saveRouterButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        saveRouterButton.ForeColor = Color.White;
        saveRouterButton.Location = new Point(164, 189);
        saveRouterButton.Margin = new Padding(2, 4, 2, 4);
        saveRouterButton.Name = "saveRouterButton";
        saveRouterButton.Size = new Size(175, 42);
        saveRouterButton.TabIndex = 14;
        saveRouterButton.Text = "保存 Router 配置";
        saveRouterButton.UseVisualStyleBackColor = false;
        saveRouterButton.Click += SaveRouterButton_Click;
        // 
        // saveResultLabel
        // 
        routerLayout.SetColumnSpan(saveResultLabel, 2);
        saveResultLabel.Dock = DockStyle.Fill;
        saveResultLabel.Font = new Font("Microsoft YaHei UI", 9F);
        saveResultLabel.Location = new Point(575, 184);
        saveResultLabel.Margin = new Padding(2, 0, 2, 0);
        saveResultLabel.Name = "saveResultLabel";
        saveResultLabel.Size = new Size(570, 52);
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
        routerNoticeLabel.Location = new Point(2, 236);
        routerNoticeLabel.Margin = new Padding(2, 0, 2, 0);
        routerNoticeLabel.Name = "routerNoticeLabel";
        routerNoticeLabel.Padding = new Padding(0, 4, 0, 0);
        routerNoticeLabel.Size = new Size(1143, 69);
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
        waveGeneratorGroup.Location = new Point(22, 473);
        waveGeneratorGroup.Margin = new Padding(2, 4, 2, 4);
        waveGeneratorGroup.Name = "waveGeneratorGroup";
        waveGeneratorGroup.Padding = new Padding(14, 19, 14, 10);
        waveGeneratorGroup.Size = new Size(1187, 207);
        waveGeneratorGroup.TabIndex = 3;
        waveGeneratorGroup.TabStop = false;
        waveGeneratorGroup.Text = "波形生成器";
        // 
        // waveGeneratorLayout
        // 
        waveGeneratorLayout.ColumnCount = 5;
        waveGeneratorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 175F));
        waveGeneratorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        waveGeneratorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        waveGeneratorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
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
        waveGeneratorLayout.Location = new Point(14, 41);
        waveGeneratorLayout.Margin = new Padding(2, 4, 2, 4);
        waveGeneratorLayout.Name = "waveGeneratorLayout";
        waveGeneratorLayout.RowCount = 4;
        waveGeneratorLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        waveGeneratorLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        waveGeneratorLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        waveGeneratorLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        waveGeneratorLayout.Size = new Size(1159, 156);
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
        waveGeneratorPathLabel.Size = new Size(171, 42);
        waveGeneratorPathLabel.TabIndex = 0;
        waveGeneratorPathLabel.Text = "WFast.exe 路径";
        waveGeneratorPathLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // waveGeneratorPathTextBox
        // 
        waveGeneratorPathTextBox.BackColor = Color.White;
        waveGeneratorPathTextBox.BorderStyle = BorderStyle.FixedSingle;
        waveGeneratorPathTextBox.Dock = DockStyle.Fill;
        waveGeneratorPathTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        waveGeneratorPathTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        waveGeneratorPathTextBox.Location = new Point(177, 4);
        waveGeneratorPathTextBox.Margin = new Padding(2, 4, 2, 4);
        waveGeneratorPathTextBox.Name = "waveGeneratorPathTextBox";
        waveGeneratorPathTextBox.Size = new Size(728, 27);
        waveGeneratorPathTextBox.TabIndex = 1;
        waveGeneratorPathTextBox.TextChanged += WaveGeneratorPathTextBox_TextChanged;
        // 
        // browseWaveGeneratorButton
        // 
        browseWaveGeneratorButton.BackColor = Color.FromArgb(248, 250, 252);
        browseWaveGeneratorButton.Cursor = Cursors.Hand;
        browseWaveGeneratorButton.Dock = DockStyle.Fill;
        browseWaveGeneratorButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
        browseWaveGeneratorButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
        browseWaveGeneratorButton.FlatStyle = FlatStyle.Flat;
        browseWaveGeneratorButton.Font = new Font("Microsoft YaHei UI", 9F);
        browseWaveGeneratorButton.ForeColor = Color.FromArgb(15, 23, 42);
        browseWaveGeneratorButton.Location = new Point(909, 4);
        browseWaveGeneratorButton.Margin = new Padding(2, 4, 2, 4);
        browseWaveGeneratorButton.Name = "browseWaveGeneratorButton";
        browseWaveGeneratorButton.Size = new Size(108, 34);
        browseWaveGeneratorButton.TabIndex = 2;
        browseWaveGeneratorButton.Text = "浏览...";
        browseWaveGeneratorButton.UseVisualStyleBackColor = false;
        browseWaveGeneratorButton.Click += BrowseWaveGeneratorButton_Click;
        // 
        // saveWaveGeneratorButton
        // 
        saveWaveGeneratorButton.BackColor = Color.FromArgb(29, 78, 216);
        saveWaveGeneratorButton.Cursor = Cursors.Hand;
        saveWaveGeneratorButton.Dock = DockStyle.Fill;
        saveWaveGeneratorButton.FlatAppearance.BorderSize = 0;
        saveWaveGeneratorButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 64, 175);
        saveWaveGeneratorButton.FlatStyle = FlatStyle.Flat;
        saveWaveGeneratorButton.Font = new Font("Microsoft YaHei UI", 9F);
        saveWaveGeneratorButton.ForeColor = Color.White;
        saveWaveGeneratorButton.Location = new Point(1021, 4);
        saveWaveGeneratorButton.Margin = new Padding(2, 4, 2, 4);
        saveWaveGeneratorButton.Name = "saveWaveGeneratorButton";
        saveWaveGeneratorButton.Size = new Size(136, 34);
        saveWaveGeneratorButton.TabIndex = 3;
        saveWaveGeneratorButton.Text = "保存方案";
        saveWaveGeneratorButton.UseVisualStyleBackColor = false;
        saveWaveGeneratorButton.Click += SaveWaveGeneratorButton_Click;
        // 
        // waveGeneratorStateLabel
        // 
        waveGeneratorLayout.SetColumnSpan(waveGeneratorStateLabel, 2);
        waveGeneratorStateLabel.Dock = DockStyle.Fill;
        waveGeneratorStateLabel.Font = new Font("Microsoft YaHei UI", 8F);
        waveGeneratorStateLabel.Location = new Point(177, 42);
        waveGeneratorStateLabel.Margin = new Padding(2, 0, 2, 0);
        waveGeneratorStateLabel.Name = "waveGeneratorStateLabel";
        waveGeneratorStateLabel.Size = new Size(840, 30);
        waveGeneratorStateLabel.TabIndex = 3;
        waveGeneratorStateLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // waveGeneratorModeLabel
        // 
        waveGeneratorModeLabel.Dock = DockStyle.Fill;
        waveGeneratorModeLabel.Font = new Font("Microsoft YaHei UI", 9F);
        waveGeneratorModeLabel.ForeColor = Color.FromArgb(71, 85, 105);
        waveGeneratorModeLabel.Location = new Point(2, 72);
        waveGeneratorModeLabel.Margin = new Padding(2, 0, 2, 0);
        waveGeneratorModeLabel.Name = "waveGeneratorModeLabel";
        waveGeneratorModeLabel.Size = new Size(171, 42);
        waveGeneratorModeLabel.TabIndex = 4;
        waveGeneratorModeLabel.Text = "生成方案";
        waveGeneratorModeLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // waveGeneratorModeComboBox
        // 
        waveGeneratorModeComboBox.BackColor = Color.White;
        waveGeneratorLayout.SetColumnSpan(waveGeneratorModeComboBox, 3);
        waveGeneratorModeComboBox.Dock = DockStyle.Left;
        waveGeneratorModeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        waveGeneratorModeComboBox.FlatStyle = FlatStyle.Flat;
        waveGeneratorModeComboBox.Font = new Font("Microsoft YaHei UI", 9F);
        waveGeneratorModeComboBox.ForeColor = Color.FromArgb(15, 23, 42);
        waveGeneratorModeComboBox.FormattingEnabled = true;
        waveGeneratorModeComboBox.Location = new Point(177, 76);
        waveGeneratorModeComboBox.Margin = new Padding(2, 4, 2, 4);
        waveGeneratorModeComboBox.Name = "waveGeneratorModeComboBox";
        waveGeneratorModeComboBox.Size = new Size(349, 28);
        waveGeneratorModeComboBox.TabIndex = 5;
        waveGeneratorModeComboBox.SelectedIndexChanged += WaveGeneratorModeComboBox_SelectedIndexChanged;
        // 
        // waveProgramDirectoryLabel
        // 
        waveProgramDirectoryLabel.Dock = DockStyle.Fill;
        waveProgramDirectoryLabel.Font = new Font("Microsoft YaHei UI", 9F);
        waveProgramDirectoryLabel.ForeColor = Color.FromArgb(71, 85, 105);
        waveProgramDirectoryLabel.Location = new Point(4, 114);
        waveProgramDirectoryLabel.Margin = new Padding(4, 0, 4, 0);
        waveProgramDirectoryLabel.Name = "waveProgramDirectoryLabel";
        waveProgramDirectoryLabel.Size = new Size(167, 42);
        waveProgramDirectoryLabel.TabIndex = 6;
        waveProgramDirectoryLabel.Text = "波形程序目录";
        waveProgramDirectoryLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // waveProgramDirectoryTextBox
        // 
        waveProgramDirectoryTextBox.BackColor = Color.White;
        waveProgramDirectoryTextBox.BorderStyle = BorderStyle.FixedSingle;
        waveProgramDirectoryTextBox.Dock = DockStyle.Fill;
        waveProgramDirectoryTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        waveProgramDirectoryTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        waveProgramDirectoryTextBox.Location = new Point(179, 118);
        waveProgramDirectoryTextBox.Margin = new Padding(4, 4, 4, 4);
        waveProgramDirectoryTextBox.Name = "waveProgramDirectoryTextBox";
        waveProgramDirectoryTextBox.Size = new Size(724, 27);
        waveProgramDirectoryTextBox.TabIndex = 6;
        // 
        // browseWaveProgramDirectoryButton
        // 
        browseWaveProgramDirectoryButton.BackColor = Color.FromArgb(248, 250, 252);
        browseWaveProgramDirectoryButton.Dock = DockStyle.Fill;
        browseWaveProgramDirectoryButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
        browseWaveProgramDirectoryButton.FlatStyle = FlatStyle.Flat;
        browseWaveProgramDirectoryButton.Font = new Font("Microsoft YaHei UI", 9F);
        browseWaveProgramDirectoryButton.Location = new Point(911, 118);
        browseWaveProgramDirectoryButton.Margin = new Padding(4, 4, 4, 4);
        browseWaveProgramDirectoryButton.Name = "browseWaveProgramDirectoryButton";
        browseWaveProgramDirectoryButton.Size = new Size(104, 34);
        browseWaveProgramDirectoryButton.TabIndex = 7;
        browseWaveProgramDirectoryButton.Text = "浏览目录";
        browseWaveProgramDirectoryButton.UseVisualStyleBackColor = false;
        browseWaveProgramDirectoryButton.Click += BrowseWaveProgramDirectoryButton_Click;
        // 
        // saveWaveProgramDirectoryButton
        // 
        saveWaveProgramDirectoryButton.BackColor = Color.FromArgb(29, 78, 216);
        saveWaveProgramDirectoryButton.Dock = DockStyle.Fill;
        saveWaveProgramDirectoryButton.FlatAppearance.BorderSize = 0;
        saveWaveProgramDirectoryButton.FlatStyle = FlatStyle.Flat;
        saveWaveProgramDirectoryButton.Font = new Font("Microsoft YaHei UI", 9F);
        saveWaveProgramDirectoryButton.ForeColor = Color.White;
        saveWaveProgramDirectoryButton.Location = new Point(1023, 118);
        saveWaveProgramDirectoryButton.Margin = new Padding(4, 4, 4, 4);
        saveWaveProgramDirectoryButton.Name = "saveWaveProgramDirectoryButton";
        saveWaveProgramDirectoryButton.Size = new Size(132, 34);
        saveWaveProgramDirectoryButton.TabIndex = 8;
        saveWaveProgramDirectoryButton.Text = "保存目录";
        saveWaveProgramDirectoryButton.UseVisualStyleBackColor = false;
        saveWaveProgramDirectoryButton.Click += SaveWaveProgramDirectoryButton_Click;
        // 
        // _databaseGroup
        // 
        _databaseGroup.BackColor = Color.White;
        _databaseGroup.Controls.Add(_databaseLayout);
        _databaseGroup.Dock = DockStyle.Fill;
        _databaseGroup.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
        _databaseGroup.ForeColor = Color.FromArgb(15, 23, 42);
        _databaseGroup.Location = new Point(22, 688);
        _databaseGroup.Margin = new Padding(2, 4, 2, 4);
        _databaseGroup.Name = "_databaseGroup";
        _databaseGroup.Padding = new Padding(14, 19, 14, 10);
        _databaseGroup.Size = new Size(1187, 187);
        _databaseGroup.TabIndex = 4;
        _databaseGroup.TabStop = false;
        _databaseGroup.Text = "SQL Server 任务索引";
        // 
        // _databaseLayout
        // 
        _databaseLayout.ColumnCount = 5;
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 141F));
        _databaseLayout.Controls.Add(_databaseEnabledCheckBox, 0, 0);
        _databaseLayout.Controls.Add(_databaseConnectionTextBox, 1, 0);
        _databaseLayout.Controls.Add(_databaseUserNameLabel, 0, 1);
        _databaseLayout.Controls.Add(_databaseUserNameTextBox, 1, 1);
        _databaseLayout.Controls.Add(_databasePasswordLabel, 2, 1);
        _databaseLayout.Controls.Add(_databasePasswordTextBox, 3, 1);
        _databaseLayout.Controls.Add(_saveDatabaseButton, 4, 0);
        _databaseLayout.Controls.Add(_databaseStateLabel, 0, 2);
        _databaseLayout.Dock = DockStyle.Fill;
        _databaseLayout.Location = new Point(14, 41);
        _databaseLayout.Margin = new Padding(2, 4, 2, 4);
        _databaseLayout.Name = "_databaseLayout";
        _databaseLayout.RowCount = 3;
        _databaseLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        _databaseLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        _databaseLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _databaseLayout.Size = new Size(1159, 136);
        _databaseLayout.TabIndex = 0;
        // 
        // _databaseEnabledCheckBox
        // 
        _databaseEnabledCheckBox.Dock = DockStyle.Fill;
        _databaseEnabledCheckBox.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        _databaseEnabledCheckBox.Location = new Point(2, 4);
        _databaseEnabledCheckBox.Margin = new Padding(2, 4, 2, 4);
        _databaseEnabledCheckBox.Name = "_databaseEnabledCheckBox";
        _databaseEnabledCheckBox.Size = new Size(156, 37);
        _databaseEnabledCheckBox.TabIndex = 0;
        _databaseEnabledCheckBox.Text = "启用数据库";
        _databaseEnabledCheckBox.CheckedChanged += DatabaseEnabledCheckBox_CheckedChanged;
        // 
        // _databaseConnectionTextBox
        // 
        _databaseConnectionTextBox.BackColor = Color.White;
        _databaseConnectionTextBox.BorderStyle = BorderStyle.FixedSingle;
        _databaseLayout.SetColumnSpan(_databaseConnectionTextBox, 3);
        _databaseConnectionTextBox.Dock = DockStyle.Fill;
        _databaseConnectionTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        _databaseConnectionTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        _databaseConnectionTextBox.Location = new Point(162, 4);
        _databaseConnectionTextBox.Margin = new Padding(2, 4, 2, 4);
        _databaseConnectionTextBox.Name = "_databaseConnectionTextBox";
        _databaseConnectionTextBox.PlaceholderText = "SQL Server 连接字符串";
        _databaseConnectionTextBox.Size = new Size(854, 27);
        _databaseConnectionTextBox.TabIndex = 1;
        // 
        // _databaseUserNameLabel
        // 
        _databaseUserNameLabel.Dock = DockStyle.Fill;
        _databaseUserNameLabel.Font = new Font("Microsoft YaHei UI", 9F);
        _databaseUserNameLabel.ForeColor = Color.FromArgb(71, 85, 105);
        _databaseUserNameLabel.Location = new Point(2, 45);
        _databaseUserNameLabel.Margin = new Padding(2, 0, 2, 0);
        _databaseUserNameLabel.Name = "_databaseUserNameLabel";
        _databaseUserNameLabel.Size = new Size(156, 45);
        _databaseUserNameLabel.TabIndex = 2;
        _databaseUserNameLabel.Text = "账号";
        _databaseUserNameLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _databaseUserNameTextBox
        // 
        _databaseUserNameTextBox.BackColor = Color.White;
        _databaseUserNameTextBox.BorderStyle = BorderStyle.FixedSingle;
        _databaseUserNameTextBox.Dock = DockStyle.Fill;
        _databaseUserNameTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        _databaseUserNameTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        _databaseUserNameTextBox.Location = new Point(162, 49);
        _databaseUserNameTextBox.Margin = new Padding(2, 4, 2, 4);
        _databaseUserNameTextBox.Name = "_databaseUserNameTextBox";
        _databaseUserNameTextBox.PlaceholderText = "SQL Server 账号";
        _databaseUserNameTextBox.Size = new Size(370, 27);
        _databaseUserNameTextBox.TabIndex = 3;
        // 
        // _databasePasswordLabel
        // 
        _databasePasswordLabel.Dock = DockStyle.Fill;
        _databasePasswordLabel.Font = new Font("Microsoft YaHei UI", 9F);
        _databasePasswordLabel.ForeColor = Color.FromArgb(71, 85, 105);
        _databasePasswordLabel.Location = new Point(536, 45);
        _databasePasswordLabel.Margin = new Padding(2, 0, 2, 0);
        _databasePasswordLabel.Name = "_databasePasswordLabel";
        _databasePasswordLabel.Size = new Size(106, 45);
        _databasePasswordLabel.TabIndex = 4;
        _databasePasswordLabel.Text = "密码";
        _databasePasswordLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _databasePasswordTextBox
        // 
        _databasePasswordTextBox.BackColor = Color.White;
        _databasePasswordTextBox.BorderStyle = BorderStyle.FixedSingle;
        _databasePasswordTextBox.Dock = DockStyle.Fill;
        _databasePasswordTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        _databasePasswordTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        _databasePasswordTextBox.Location = new Point(646, 49);
        _databasePasswordTextBox.Margin = new Padding(2, 4, 2, 4);
        _databasePasswordTextBox.Name = "_databasePasswordTextBox";
        _databasePasswordTextBox.PlaceholderText = "SQL Server 密码";
        _databasePasswordTextBox.Size = new Size(370, 27);
        _databasePasswordTextBox.TabIndex = 5;
        _databasePasswordTextBox.UseSystemPasswordChar = true;
        // 
        // _saveDatabaseButton
        // 
        _saveDatabaseButton.BackColor = Color.FromArgb(29, 78, 216);
        _saveDatabaseButton.Cursor = Cursors.Hand;
        _saveDatabaseButton.Dock = DockStyle.Fill;
        _saveDatabaseButton.FlatAppearance.BorderSize = 0;
        _saveDatabaseButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 64, 175);
        _saveDatabaseButton.FlatStyle = FlatStyle.Flat;
        _saveDatabaseButton.Font = new Font("Microsoft YaHei UI", 9F);
        _saveDatabaseButton.ForeColor = Color.White;
        _saveDatabaseButton.Location = new Point(1020, 4);
        _saveDatabaseButton.Margin = new Padding(2, 4, 2, 4);
        _saveDatabaseButton.Name = "_saveDatabaseButton";
        _saveDatabaseButton.Size = new Size(137, 37);
        _saveDatabaseButton.TabIndex = 2;
        _saveDatabaseButton.Text = "保存数据库";
        _saveDatabaseButton.UseVisualStyleBackColor = false;
        _saveDatabaseButton.Click += SaveDatabaseButton_Click;
        // 
        // _databaseStateLabel
        // 
        _databaseLayout.SetColumnSpan(_databaseStateLabel, 5);
        _databaseStateLabel.Dock = DockStyle.Fill;
        _databaseStateLabel.Font = new Font("Microsoft YaHei UI", 8F);
        _databaseStateLabel.ForeColor = Color.FromArgb(100, 116, 139);
        _databaseStateLabel.Location = new Point(2, 90);
        _databaseStateLabel.Margin = new Padding(2, 0, 2, 0);
        _databaseStateLabel.Name = "_databaseStateLabel";
        _databaseStateLabel.Size = new Size(1155, 46);
        _databaseStateLabel.TabIndex = 3;
        _databaseStateLabel.Text = "数据库日志未启用。";
        _databaseStateLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // Config
        // 
        AutoScaleDimensions = new SizeF(120F, 120F);
        AutoScaleMode = AutoScaleMode.Dpi;
        AutoScroll = true;
        AutoScrollMinSize = new Size(860, 720);
        BackColor = Color.FromArgb(241, 245, 249);
        Controls.Add(pagePanel);
        Font = new Font("Microsoft YaHei UI", 9F);
        ForeColor = Color.FromArgb(15, 23, 42);
        Margin = new Padding(2, 4, 2, 4);
        Name = "Config";
        Size = new Size(1231, 758);
        operationLogGroup.ResumeLayout(false);
        operationLogLayout.ResumeLayout(false);
        operationLogLayout.PerformLayout();
        pagePanel.ResumeLayout(false);
        pagePanel.PerformLayout();
        rootLayout.ResumeLayout(false);
        routerGroup.ResumeLayout(false);
        routerLayout.ResumeLayout(false);
        routerLayout.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)routerTcpPortInput).EndInit();
        waveGeneratorGroup.ResumeLayout(false);
        waveGeneratorLayout.ResumeLayout(false);
        waveGeneratorLayout.PerformLayout();
        _databaseGroup.ResumeLayout(false);
        _databaseLayout.ResumeLayout(false);
        _databaseLayout.PerformLayout();
        ResumeLayout(false);
    }
}
