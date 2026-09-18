namespace Page_switching.panel;

partial class Config
{
    private System.ComponentModel.IContainer components = null!;
    private TableLayoutPanel rootLayout;
    private Label titleLabel;
    private Label subtitleLabel;
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
    private GroupBox _databaseGroup;
    private TableLayoutPanel _databaseLayout;
    private CheckBox _databaseEnabledCheckBox;
    private TextBox _databaseConnectionTextBox;
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
        rootLayout = new TableLayoutPanel();
        titleLabel = new Label();
        subtitleLabel = new Label();
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
        _databaseGroup = new GroupBox();
        _databaseLayout = new TableLayoutPanel();
        _databaseEnabledCheckBox = new CheckBox();
        _databaseConnectionTextBox = new TextBox();
        _saveDatabaseButton = new Button();
        _databaseStateLabel = new Label();
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
        // rootLayout
        // 
        rootLayout.BackColor = Color.FromArgb(241, 245, 249);
        rootLayout.AutoScroll = true;
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(titleLabel, 0, 0);
        rootLayout.Controls.Add(subtitleLabel, 0, 1);
        rootLayout.Controls.Add(routerGroup, 0, 2);
        rootLayout.Controls.Add(waveGeneratorGroup, 0, 3);
        rootLayout.Controls.Add(_databaseGroup, 0, 4);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(16);
        rootLayout.RowCount = 5;
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 420F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 144F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 114F));
        rootLayout.Size = new Size(1210, 796);
        rootLayout.TabIndex = 0;
        // 
        // titleLabel
        // 
        titleLabel.Dock = DockStyle.Fill;
        titleLabel.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
        titleLabel.Location = new Point(23, 20);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(859, 44);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "通信配置";
        titleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // subtitleLabel
        // 
        subtitleLabel.Dock = DockStyle.Fill;
        subtitleLabel.Font = new Font("Microsoft YaHei UI", 9F);
        subtitleLabel.ForeColor = Color.FromArgb(71, 85, 105);
        subtitleLabel.Location = new Point(23, 64);
        subtitleLabel.Name = "subtitleLabel";
        subtitleLabel.Size = new Size(859, 38);
        subtitleLabel.TabIndex = 1;
        subtitleLabel.Text = "TwinCAT ADS TCP Router 与倍福 PLC 路由参数";
        subtitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // routerGroup
        // 
        routerGroup.Controls.Add(routerLayout);
        routerGroup.Dock = DockStyle.Fill;
        routerGroup.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
        routerGroup.ForeColor = Color.FromArgb(15, 23, 42);
        routerGroup.Location = new Point(23, 105);
        routerGroup.Name = "routerGroup";
        routerGroup.Padding = new Padding(14, 18, 14, 14);
        routerGroup.Size = new Size(859, 449);
        routerGroup.TabIndex = 2;
        routerGroup.TabStop = false;
        routerGroup.Text = "TwinCAT.Ads.TcpRouter";
        // 
        // routerLayout
        // 
        routerLayout.ColumnCount = 3;
        routerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
        routerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 650F));
        routerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        routerLayout.Controls.Add(routerEnabledCheckBox, 0, 0);
        routerLayout.Controls.Add(routerStateLabel, 1, 0);
        routerLayout.Controls.Add(routerNameLabel, 0, 1);
        routerLayout.Controls.Add(routerNameTextBox, 1, 1);
        routerLayout.Controls.Add(localNetIdLabel, 0, 2);
        routerLayout.Controls.Add(localNetIdTextBox, 1, 2);
        routerLayout.Controls.Add(routerTcpPortLabel, 0, 3);
        routerLayout.Controls.Add(routerTcpPortInput, 1, 3);
        routerLayout.Controls.Add(remoteNameLabel, 0, 4);
        routerLayout.Controls.Add(remoteNameTextBox, 1, 4);
        routerLayout.Controls.Add(remoteAddressLabel, 0, 5);
        routerLayout.Controls.Add(remoteAddressTextBox, 1, 5);
        routerLayout.Controls.Add(remoteNetIdLabel, 0, 6);
        routerLayout.Controls.Add(remoteNetIdTextBox, 1, 6);
        routerLayout.Controls.Add(saveRouterButton, 1, 7);
        routerLayout.Controls.Add(saveResultLabel, 1, 8);
        routerLayout.Controls.Add(routerNoticeLabel, 0, 9);
        routerLayout.Dock = DockStyle.Fill;
        routerLayout.Location = new Point(14, 40);
        routerLayout.Name = "routerLayout";
        routerLayout.RowCount = 10;
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        routerLayout.Size = new Size(831, 395);
        routerLayout.TabIndex = 0;
        // 
        // routerEnabledCheckBox
        // 
        routerEnabledCheckBox.Dock = DockStyle.Fill;
        routerEnabledCheckBox.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        routerEnabledCheckBox.Location = new Point(3, 3);
        routerEnabledCheckBox.Name = "routerEnabledCheckBox";
        routerEnabledCheckBox.Size = new Size(184, 36);
        routerEnabledCheckBox.TabIndex = 0;
        routerEnabledCheckBox.Text = "启用独立 TCP Router";
        routerEnabledCheckBox.CheckedChanged += RouterEnabledCheckBox_CheckedChanged;
        // 
        // routerStateLabel
        // 
        routerStateLabel.Dock = DockStyle.Fill;
        routerStateLabel.Font = new Font("Microsoft YaHei UI", 9F);
        routerStateLabel.Location = new Point(193, 0);
        routerStateLabel.Name = "routerStateLabel";
        routerStateLabel.Size = new Size(635, 42);
        routerStateLabel.TabIndex = 1;
        routerStateLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // routerNameLabel
        // 
        routerNameLabel.Dock = DockStyle.Fill;
        routerNameLabel.Font = new Font("Microsoft YaHei UI", 9F);
        routerNameLabel.ForeColor = Color.FromArgb(71, 85, 105);
        routerNameLabel.Location = new Point(3, 42);
        routerNameLabel.Name = "routerNameLabel";
        routerNameLabel.Size = new Size(184, 42);
        routerNameLabel.TabIndex = 2;
        routerNameLabel.Text = "Router 名称";
        routerNameLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // routerNameTextBox
        // 
        routerNameTextBox.Dock = DockStyle.Fill;
        routerNameTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        routerNameTextBox.Location = new Point(193, 48);
        routerNameTextBox.Margin = new Padding(3, 6, 3, 6);
        routerNameTextBox.Name = "routerNameTextBox";
        routerNameTextBox.Size = new Size(635, 29);
        routerNameTextBox.TabIndex = 3;
        // 
        // localNetIdLabel
        // 
        localNetIdLabel.Dock = DockStyle.Fill;
        localNetIdLabel.Font = new Font("Microsoft YaHei UI", 9F);
        localNetIdLabel.ForeColor = Color.FromArgb(71, 85, 105);
        localNetIdLabel.Location = new Point(3, 84);
        localNetIdLabel.Name = "localNetIdLabel";
        localNetIdLabel.Size = new Size(184, 42);
        localNetIdLabel.TabIndex = 4;
        localNetIdLabel.Text = "本机 AMS Net ID";
        localNetIdLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // localNetIdTextBox
        // 
        localNetIdTextBox.Dock = DockStyle.Fill;
        localNetIdTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        localNetIdTextBox.Location = new Point(193, 90);
        localNetIdTextBox.Margin = new Padding(3, 6, 3, 6);
        localNetIdTextBox.Name = "localNetIdTextBox";
        localNetIdTextBox.Size = new Size(635, 29);
        localNetIdTextBox.TabIndex = 5;
        // 
        // routerTcpPortLabel
        // 
        routerTcpPortLabel.Dock = DockStyle.Fill;
        routerTcpPortLabel.Font = new Font("Microsoft YaHei UI", 9F);
        routerTcpPortLabel.ForeColor = Color.FromArgb(71, 85, 105);
        routerTcpPortLabel.Location = new Point(3, 126);
        routerTcpPortLabel.Name = "routerTcpPortLabel";
        routerTcpPortLabel.Size = new Size(184, 42);
        routerTcpPortLabel.TabIndex = 6;
        routerTcpPortLabel.Text = "Router TCP 端口";
        routerTcpPortLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // routerTcpPortInput
        // 
        routerTcpPortInput.Dock = DockStyle.Fill;
        routerTcpPortInput.Font = new Font("Microsoft YaHei UI", 10F);
        routerTcpPortInput.Location = new Point(193, 132);
        routerTcpPortInput.Margin = new Padding(3, 6, 3, 6);
        routerTcpPortInput.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
        routerTcpPortInput.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        routerTcpPortInput.Name = "routerTcpPortInput";
        routerTcpPortInput.Size = new Size(635, 29);
        routerTcpPortInput.TabIndex = 7;
        routerTcpPortInput.Value = new decimal(new int[] { 48898, 0, 0, 0 });
        // 
        // remoteNameLabel
        // 
        remoteNameLabel.Dock = DockStyle.Fill;
        remoteNameLabel.Font = new Font("Microsoft YaHei UI", 9F);
        remoteNameLabel.ForeColor = Color.FromArgb(71, 85, 105);
        remoteNameLabel.Location = new Point(3, 168);
        remoteNameLabel.Name = "remoteNameLabel";
        remoteNameLabel.Size = new Size(184, 42);
        remoteNameLabel.TabIndex = 8;
        remoteNameLabel.Text = "PLC 路由名称";
        remoteNameLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // remoteNameTextBox
        // 
        remoteNameTextBox.Dock = DockStyle.Fill;
        remoteNameTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        remoteNameTextBox.Location = new Point(193, 174);
        remoteNameTextBox.Margin = new Padding(3, 6, 3, 6);
        remoteNameTextBox.Name = "remoteNameTextBox";
        remoteNameTextBox.Size = new Size(635, 29);
        remoteNameTextBox.TabIndex = 9;
        // 
        // remoteAddressLabel
        // 
        remoteAddressLabel.Dock = DockStyle.Fill;
        remoteAddressLabel.Font = new Font("Microsoft YaHei UI", 9F);
        remoteAddressLabel.ForeColor = Color.FromArgb(71, 85, 105);
        remoteAddressLabel.Location = new Point(3, 210);
        remoteAddressLabel.Name = "remoteAddressLabel";
        remoteAddressLabel.Size = new Size(184, 42);
        remoteAddressLabel.TabIndex = 10;
        remoteAddressLabel.Text = "PLC IP 地址";
        remoteAddressLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // remoteAddressTextBox
        // 
        remoteAddressTextBox.Dock = DockStyle.Fill;
        remoteAddressTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        remoteAddressTextBox.Location = new Point(193, 216);
        remoteAddressTextBox.Margin = new Padding(3, 6, 3, 6);
        remoteAddressTextBox.Name = "remoteAddressTextBox";
        remoteAddressTextBox.Size = new Size(635, 29);
        remoteAddressTextBox.TabIndex = 11;
        // 
        // remoteNetIdLabel
        // 
        remoteNetIdLabel.Dock = DockStyle.Fill;
        remoteNetIdLabel.Font = new Font("Microsoft YaHei UI", 9F);
        remoteNetIdLabel.ForeColor = Color.FromArgb(71, 85, 105);
        remoteNetIdLabel.Location = new Point(3, 252);
        remoteNetIdLabel.Name = "remoteNetIdLabel";
        remoteNetIdLabel.Size = new Size(184, 42);
        remoteNetIdLabel.TabIndex = 12;
        remoteNetIdLabel.Text = "PLC AMS Net ID";
        remoteNetIdLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // remoteNetIdTextBox
        // 
        remoteNetIdTextBox.Dock = DockStyle.Fill;
        remoteNetIdTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        remoteNetIdTextBox.Location = new Point(193, 258);
        remoteNetIdTextBox.Margin = new Padding(3, 6, 3, 6);
        remoteNetIdTextBox.Name = "remoteNetIdTextBox";
        remoteNetIdTextBox.Size = new Size(635, 29);
        remoteNetIdTextBox.TabIndex = 13;
        // 
        // saveRouterButton
        // 
        saveRouterButton.Anchor = AnchorStyles.Left;
        saveRouterButton.BackColor = Color.FromArgb(37, 99, 235);
        saveRouterButton.FlatAppearance.BorderSize = 0;
        saveRouterButton.FlatStyle = FlatStyle.Flat;
        saveRouterButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        saveRouterButton.ForeColor = Color.White;
        saveRouterButton.Location = new Point(193, 301);
        saveRouterButton.Name = "saveRouterButton";
        saveRouterButton.Size = new Size(160, 38);
        saveRouterButton.TabIndex = 14;
        saveRouterButton.Text = "保存 Router 配置";
        saveRouterButton.UseVisualStyleBackColor = false;
        saveRouterButton.Click += SaveRouterButton_Click;
        // 
        // saveResultLabel
        // 
        saveResultLabel.Dock = DockStyle.Fill;
        saveResultLabel.Font = new Font("Microsoft YaHei UI", 9F);
        saveResultLabel.Location = new Point(193, 346);
        saveResultLabel.Name = "saveResultLabel";
        saveResultLabel.Size = new Size(635, 38);
        saveResultLabel.TabIndex = 15;
        saveResultLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // routerNoticeLabel
        // 
        routerLayout.SetColumnSpan(routerNoticeLabel, 3);
        routerNoticeLabel.Dock = DockStyle.Fill;
        routerNoticeLabel.Font = new Font("Microsoft YaHei UI", 9F);
        routerNoticeLabel.ForeColor = Color.FromArgb(180, 83, 9);
        routerNoticeLabel.Location = new Point(3, 384);
        routerNoticeLabel.Name = "routerNoticeLabel";
        routerNoticeLabel.Padding = new Padding(0, 10, 0, 0);
        routerNoticeLabel.Size = new Size(825, 11);
        routerNoticeLabel.TabIndex = 16;
        routerNoticeLabel.Text = "提示：系统 TwinCAT Router 已运行时请关闭独立 Router，否则 TCP 48898 端口会冲突。远程 PLC 还需要配置返回本机 AMS Net ID 的路由。";
        // 
        // waveGeneratorGroup
        // 
        waveGeneratorGroup.Controls.Add(waveGeneratorLayout);
        waveGeneratorGroup.Dock = DockStyle.Fill;
        waveGeneratorGroup.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
        waveGeneratorGroup.ForeColor = Color.FromArgb(15, 23, 42);
        waveGeneratorGroup.Location = new Point(23, 560);
        waveGeneratorGroup.Name = "waveGeneratorGroup";
        waveGeneratorGroup.Padding = new Padding(14, 18, 14, 10);
        waveGeneratorGroup.Size = new Size(859, 128);
        waveGeneratorGroup.TabIndex = 3;
        waveGeneratorGroup.TabStop = false;
        waveGeneratorGroup.Text = "波形生成器";
        // 
        // waveGeneratorLayout
        // 
        waveGeneratorLayout.ColumnCount = 5;
        waveGeneratorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
        waveGeneratorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 650F));
        waveGeneratorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
        waveGeneratorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
        waveGeneratorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        waveGeneratorLayout.Controls.Add(waveGeneratorPathLabel, 0, 0);
        waveGeneratorLayout.Controls.Add(waveGeneratorPathTextBox, 1, 0);
        waveGeneratorLayout.Controls.Add(browseWaveGeneratorButton, 2, 0);
        waveGeneratorLayout.Controls.Add(saveWaveGeneratorButton, 3, 0);
        waveGeneratorLayout.Controls.Add(waveGeneratorStateLabel, 1, 1);
        waveGeneratorLayout.Controls.Add(waveGeneratorModeLabel, 0, 2);
        waveGeneratorLayout.Controls.Add(waveGeneratorModeComboBox, 1, 2);
        waveGeneratorLayout.SetColumnSpan(waveGeneratorModeComboBox, 3);
        waveGeneratorLayout.Dock = DockStyle.Fill;
        waveGeneratorLayout.Location = new Point(14, 40);
        waveGeneratorLayout.Name = "waveGeneratorLayout";
        waveGeneratorLayout.RowCount = 3;
        waveGeneratorLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        waveGeneratorLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        waveGeneratorLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        waveGeneratorLayout.Size = new Size(831, 108);
        waveGeneratorLayout.TabIndex = 0;
        // 
        // waveGeneratorPathLabel
        // 
        waveGeneratorPathLabel.Dock = DockStyle.Fill;
        waveGeneratorPathLabel.Font = new Font("Microsoft YaHei UI", 9F);
        waveGeneratorPathLabel.ForeColor = Color.FromArgb(71, 85, 105);
        waveGeneratorPathLabel.Location = new Point(3, 0);
        waveGeneratorPathLabel.Name = "waveGeneratorPathLabel";
        waveGeneratorPathLabel.Size = new Size(114, 34);
        waveGeneratorPathLabel.TabIndex = 0;
        waveGeneratorPathLabel.Text = "WFast.exe 路径";
        waveGeneratorPathLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // waveGeneratorPathTextBox
        // 
        waveGeneratorPathTextBox.Dock = DockStyle.Fill;
        waveGeneratorPathTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        waveGeneratorPathTextBox.Location = new Point(123, 3);
        waveGeneratorPathTextBox.Name = "waveGeneratorPathTextBox";
        waveGeneratorPathTextBox.Size = new Size(545, 27);
        waveGeneratorPathTextBox.TabIndex = 1;
        waveGeneratorPathTextBox.TextChanged += WaveGeneratorPathTextBox_TextChanged;
        // 
        // browseWaveGeneratorButton
        // 
        browseWaveGeneratorButton.Dock = DockStyle.Fill;
        browseWaveGeneratorButton.Font = new Font("Microsoft YaHei UI", 9F);
        browseWaveGeneratorButton.Location = new Point(674, 3);
        browseWaveGeneratorButton.Name = "browseWaveGeneratorButton";
        browseWaveGeneratorButton.Size = new Size(74, 28);
        browseWaveGeneratorButton.TabIndex = 2;
        browseWaveGeneratorButton.Text = "浏览...";
        browseWaveGeneratorButton.UseVisualStyleBackColor = true;
        browseWaveGeneratorButton.Click += BrowseWaveGeneratorButton_Click;
        // 
        // saveWaveGeneratorButton
        // 
        saveWaveGeneratorButton.Dock = DockStyle.Fill;
        saveWaveGeneratorButton.Font = new Font("Microsoft YaHei UI", 9F);
        saveWaveGeneratorButton.Location = new Point(754, 3);
        saveWaveGeneratorButton.Name = "saveWaveGeneratorButton";
        saveWaveGeneratorButton.Size = new Size(74, 28);
        saveWaveGeneratorButton.TabIndex = 3;
        saveWaveGeneratorButton.Text = "保存设置";
        saveWaveGeneratorButton.UseVisualStyleBackColor = true;
        saveWaveGeneratorButton.Click += SaveWaveGeneratorButton_Click;
        // 
        // waveGeneratorStateLabel
        // 
        waveGeneratorLayout.SetColumnSpan(waveGeneratorStateLabel, 2);
        waveGeneratorStateLabel.Dock = DockStyle.Fill;
        waveGeneratorStateLabel.Font = new Font("Microsoft YaHei UI", 8F);
        waveGeneratorStateLabel.Location = new Point(123, 34);
        waveGeneratorStateLabel.Name = "waveGeneratorStateLabel";
        waveGeneratorStateLabel.Size = new Size(625, 20);
        waveGeneratorStateLabel.TabIndex = 3;
        waveGeneratorStateLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // waveGeneratorModeLabel
        //
        waveGeneratorModeLabel.Dock = DockStyle.Fill;
        waveGeneratorModeLabel.Font = new Font("Microsoft YaHei UI", 9F);
        waveGeneratorModeLabel.ForeColor = Color.FromArgb(71, 85, 105);
        waveGeneratorModeLabel.Location = new Point(3, 54);
        waveGeneratorModeLabel.Name = "waveGeneratorModeLabel";
        waveGeneratorModeLabel.Size = new Size(114, 34);
        waveGeneratorModeLabel.TabIndex = 4;
        waveGeneratorModeLabel.Text = "生成方案";
        waveGeneratorModeLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // waveGeneratorModeComboBox
        //
        waveGeneratorModeComboBox.Dock = DockStyle.Fill;
        waveGeneratorModeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        waveGeneratorModeComboBox.Font = new Font("Microsoft YaHei UI", 9F);
        waveGeneratorModeComboBox.FormattingEnabled = true;
        waveGeneratorModeComboBox.Location = new Point(123, 57);
        waveGeneratorModeComboBox.Name = "waveGeneratorModeComboBox";
        waveGeneratorModeComboBox.Size = new Size(705, 28);
        waveGeneratorModeComboBox.TabIndex = 5;
        waveGeneratorModeComboBox.SelectedIndexChanged += WaveGeneratorModeComboBox_SelectedIndexChanged;
        // _databaseGroup
        // 
        _databaseGroup.Controls.Add(_databaseLayout);
        _databaseGroup.Dock = DockStyle.Fill;
        _databaseGroup.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
        _databaseGroup.ForeColor = Color.FromArgb(15, 23, 42);
        _databaseGroup.Padding = new Padding(14, 18, 14, 10);
        _databaseGroup.TabStop = false;
        _databaseGroup.Text = "数据库日志";
        // 
        // _databaseLayout
        // 
        _databaseLayout.ColumnCount = 4;
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 650F));
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _databaseLayout.Controls.Add(_databaseEnabledCheckBox, 0, 0);
        _databaseLayout.Controls.Add(_databaseConnectionTextBox, 1, 0);
        _databaseLayout.Controls.Add(_saveDatabaseButton, 2, 0);
        _databaseLayout.Controls.Add(_databaseStateLabel, 1, 1);
        _databaseLayout.Dock = DockStyle.Fill;
        _databaseLayout.RowCount = 2;
        _databaseLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        _databaseLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
        _databaseLayout.SetColumnSpan(_databaseStateLabel, 2);
        // 
        // _databaseEnabledCheckBox
        // 
        _databaseEnabledCheckBox.Dock = DockStyle.Fill;
        _databaseEnabledCheckBox.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        _databaseEnabledCheckBox.Text = "启用 SQL 日志";
        _databaseEnabledCheckBox.CheckedChanged += DatabaseEnabledCheckBox_CheckedChanged;
        // 
        // _databaseConnectionTextBox
        // 
        _databaseConnectionTextBox.Dock = DockStyle.Fill;
        _databaseConnectionTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        _databaseConnectionTextBox.PlaceholderText = "SQL Server 连接字符串";
        // 
        // _saveDatabaseButton
        // 
        _saveDatabaseButton.BackColor = Color.FromArgb(226, 232, 240);
        _saveDatabaseButton.Dock = DockStyle.Fill;
        _saveDatabaseButton.FlatAppearance.BorderSize = 0;
        _saveDatabaseButton.FlatStyle = FlatStyle.Flat;
        _saveDatabaseButton.Font = new Font("Microsoft YaHei UI", 9F);
        _saveDatabaseButton.ForeColor = Color.FromArgb(15, 23, 42);
        _saveDatabaseButton.Text = "保存数据库";
        _saveDatabaseButton.Click += SaveDatabaseButton_Click;
        // 
        // _databaseStateLabel
        // 
        _databaseStateLabel.Dock = DockStyle.Fill;
        _databaseStateLabel.Font = new Font("Microsoft YaHei UI", 8F);
        _databaseStateLabel.ForeColor = Color.FromArgb(100, 116, 139);
        _databaseStateLabel.Text = "数据库日志未启用。";
        _databaseStateLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 

        // 
        // Config
        // 
        AutoScaleDimensions = new SizeF(9F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(rootLayout);
        Name = "Config";
        Size = new Size(1210, 796);
        rootLayout.ResumeLayout(false);
        routerGroup.ResumeLayout(false);
        routerLayout.ResumeLayout(false);
        routerLayout.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)routerTcpPortInput).EndInit();
        waveGeneratorGroup.ResumeLayout(false);
        waveGeneratorLayout.ResumeLayout(false);
        waveGeneratorLayout.PerformLayout();
        _databaseLayout.ResumeLayout(false);
        _databaseGroup.ResumeLayout(false);
        ResumeLayout(false);
    }
}
