namespace Page_switching.panel;

partial class Config
{
    private Panel pagePanel;

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
        pagePanel.Controls.Add(rootLayout);
        pagePanel.Dock = DockStyle.Fill;
        pagePanel.Location = new Point(0, 0);
        pagePanel.Name = "pagePanel";
        pagePanel.Size = new Size(985, 720);
        pagePanel.TabIndex = 0;
        //
        // rootLayout
        //
        rootLayout.BackColor = Color.FromArgb(241, 245, 249);
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(titleLabel, 0, 0);
        rootLayout.Controls.Add(subtitleLabel, 0, 1);
        rootLayout.Controls.Add(routerGroup, 0, 2);
        rootLayout.Controls.Add(waveGeneratorGroup, 0, 3);
        rootLayout.Controls.Add(_databaseGroup, 0, 4);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Margin = new Padding(2, 3, 2, 3);
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(16);
        rootLayout.RowCount = 5;
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 292F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 172F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 156F));
        rootLayout.Size = new Size(985, 720);
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
        titleLabel.Size = new Size(949, 42);
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
        subtitleLabel.Size = new Size(949, 26);
        subtitleLabel.TabIndex = 1;
        subtitleLabel.Text = "手动控制 ADS 路由、波形程序和数据库连接";
        subtitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // routerGroup
        //
        routerGroup.BackColor = Color.White;
        routerGroup.Controls.Add(routerLayout);
        routerGroup.Dock = DockStyle.Fill;
        routerGroup.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
        routerGroup.ForeColor = Color.FromArgb(15, 23, 42);
        routerGroup.Location = new Point(18, 87);
        routerGroup.Margin = new Padding(2, 3, 2, 3);
        routerGroup.Name = "routerGroup";
        routerGroup.Padding = new Padding(16, 12, 16, 12);
        routerGroup.Size = new Size(949, 286);
        routerGroup.TabIndex = 2;
        routerGroup.TabStop = false;
        routerGroup.Text = "手动控制 ADS 路由";
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
        routerLayout.Size = new Size(917, 245);
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
        routerEnabledCheckBox.Size = new Size(454, 26);
        routerEnabledCheckBox.TabIndex = 0;
        routerEnabledCheckBox.Text = "启用独立 TCP Router";
        routerEnabledCheckBox.CheckedChanged += RouterEnabledCheckBox_CheckedChanged;
        //
        // routerStateLabel
        //
        routerLayout.SetColumnSpan(routerStateLabel, 2);
        routerStateLabel.Dock = DockStyle.Fill;
        routerStateLabel.Font = new Font("Microsoft YaHei UI", 9F);
        routerStateLabel.Location = new Point(460, 0);
        routerStateLabel.Margin = new Padding(2, 0, 2, 0);
        routerStateLabel.Name = "routerStateLabel";
        routerStateLabel.Size = new Size(455, 32);
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
        routerNameTextBox.BorderStyle = BorderStyle.FixedSingle;
        routerNameTextBox.Dock = DockStyle.Fill;
        routerNameTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        routerNameTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        routerNameTextBox.Location = new Point(132, 37);
        routerNameTextBox.Margin = new Padding(2, 5, 2, 5);
        routerNameTextBox.Name = "routerNameTextBox";
        routerNameTextBox.Size = new Size(324, 24);
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
        localNetIdTextBox.BorderStyle = BorderStyle.FixedSingle;
        localNetIdTextBox.Dock = DockStyle.Fill;
        localNetIdTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        localNetIdTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        localNetIdTextBox.Location = new Point(132, 75);
        localNetIdTextBox.Margin = new Padding(2, 5, 2, 5);
        localNetIdTextBox.Name = "localNetIdTextBox";
        localNetIdTextBox.Size = new Size(324, 24);
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
        routerTcpPortInput.BorderStyle = BorderStyle.FixedSingle;
        routerTcpPortInput.Dock = DockStyle.Fill;
        routerTcpPortInput.Font = new Font("Microsoft YaHei UI", 10F);
        routerTcpPortInput.ForeColor = Color.FromArgb(15, 23, 42);
        routerTcpPortInput.Location = new Point(132, 113);
        routerTcpPortInput.Margin = new Padding(2, 5, 2, 5);
        routerTcpPortInput.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
        routerTcpPortInput.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        routerTcpPortInput.Name = "routerTcpPortInput";
        routerTcpPortInput.Size = new Size(324, 24);
        routerTcpPortInput.TabIndex = 7;
        routerTcpPortInput.Value = new decimal(new int[] { 48898, 0, 0, 0 });
        //
        // remoteNameLabel
        //
        remoteNameLabel.Dock = DockStyle.Fill;
        remoteNameLabel.Font = new Font("Microsoft YaHei UI", 9F);
        remoteNameLabel.ForeColor = Color.FromArgb(71, 85, 105);
        remoteNameLabel.Location = new Point(460, 32);
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
        remoteNameTextBox.BorderStyle = BorderStyle.FixedSingle;
        remoteNameTextBox.Dock = DockStyle.Fill;
        remoteNameTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        remoteNameTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        remoteNameTextBox.Location = new Point(590, 37);
        remoteNameTextBox.Margin = new Padding(2, 5, 2, 5);
        remoteNameTextBox.Name = "remoteNameTextBox";
        remoteNameTextBox.Size = new Size(325, 24);
        remoteNameTextBox.TabIndex = 9;
        //
        // remoteAddressLabel
        //
        remoteAddressLabel.Dock = DockStyle.Fill;
        remoteAddressLabel.Font = new Font("Microsoft YaHei UI", 9F);
        remoteAddressLabel.ForeColor = Color.FromArgb(71, 85, 105);
        remoteAddressLabel.Location = new Point(460, 70);
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
        remoteAddressTextBox.BorderStyle = BorderStyle.FixedSingle;
        remoteAddressTextBox.Dock = DockStyle.Fill;
        remoteAddressTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        remoteAddressTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        remoteAddressTextBox.Location = new Point(590, 75);
        remoteAddressTextBox.Margin = new Padding(2, 5, 2, 5);
        remoteAddressTextBox.Name = "remoteAddressTextBox";
        remoteAddressTextBox.Size = new Size(325, 24);
        remoteAddressTextBox.TabIndex = 11;
        //
        // remoteNetIdLabel
        //
        remoteNetIdLabel.Dock = DockStyle.Fill;
        remoteNetIdLabel.Font = new Font("Microsoft YaHei UI", 9F);
        remoteNetIdLabel.ForeColor = Color.FromArgb(71, 85, 105);
        remoteNetIdLabel.Location = new Point(460, 108);
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
        remoteNetIdTextBox.BorderStyle = BorderStyle.FixedSingle;
        remoteNetIdTextBox.Dock = DockStyle.Fill;
        remoteNetIdTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        remoteNetIdTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        remoteNetIdTextBox.Location = new Point(590, 113);
        remoteNetIdTextBox.Margin = new Padding(2, 5, 2, 5);
        remoteNetIdTextBox.Name = "remoteNetIdTextBox";
        remoteNetIdTextBox.Size = new Size(325, 24);
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
        saveRouterButton.Location = new Point(132, 150);
        saveRouterButton.Margin = new Padding(2, 3, 2, 3);
        saveRouterButton.Name = "saveRouterButton";
        saveRouterButton.Size = new Size(140, 34);
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
        saveResultLabel.Location = new Point(460, 146);
        saveResultLabel.Margin = new Padding(2, 0, 2, 0);
        saveResultLabel.Name = "saveResultLabel";
        saveResultLabel.Size = new Size(455, 42);
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
        routerNoticeLabel.Size = new Size(913, 57);
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
        waveGeneratorGroup.Location = new Point(18, 379);
        waveGeneratorGroup.Margin = new Padding(2, 3, 2, 3);
        waveGeneratorGroup.Name = "waveGeneratorGroup";
        waveGeneratorGroup.Padding = new Padding(11, 15, 11, 8);
        waveGeneratorGroup.Size = new Size(949, 166);
        waveGeneratorGroup.TabIndex = 3;
        waveGeneratorGroup.TabStop = false;
        waveGeneratorGroup.Text = "波形生成器";
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
        waveGeneratorLayout.Size = new Size(927, 126);
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
        waveGeneratorPathTextBox.BorderStyle = BorderStyle.FixedSingle;
        waveGeneratorPathTextBox.Dock = DockStyle.Fill;
        waveGeneratorPathTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        waveGeneratorPathTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        waveGeneratorPathTextBox.Location = new Point(142, 3);
        waveGeneratorPathTextBox.Margin = new Padding(2, 3, 2, 3);
        waveGeneratorPathTextBox.Name = "waveGeneratorPathTextBox";
        waveGeneratorPathTextBox.Size = new Size(581, 23);
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
        browseWaveGeneratorButton.Location = new Point(727, 3);
        browseWaveGeneratorButton.Margin = new Padding(2, 3, 2, 3);
        browseWaveGeneratorButton.Name = "browseWaveGeneratorButton";
        browseWaveGeneratorButton.Size = new Size(86, 28);
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
        saveWaveGeneratorButton.Location = new Point(817, 3);
        saveWaveGeneratorButton.Margin = new Padding(2, 3, 2, 3);
        saveWaveGeneratorButton.Name = "saveWaveGeneratorButton";
        saveWaveGeneratorButton.Size = new Size(108, 28);
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
        waveGeneratorStateLabel.Location = new Point(142, 34);
        waveGeneratorStateLabel.Margin = new Padding(2, 0, 2, 0);
        waveGeneratorStateLabel.Name = "waveGeneratorStateLabel";
        waveGeneratorStateLabel.Size = new Size(671, 24);
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
        waveGeneratorModeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        waveGeneratorModeComboBox.FlatStyle = FlatStyle.Flat;
        waveGeneratorModeComboBox.Font = new Font("Microsoft YaHei UI", 9F);
        waveGeneratorModeComboBox.ForeColor = Color.FromArgb(15, 23, 42);
        waveGeneratorModeComboBox.FormattingEnabled = true;
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
        waveProgramDirectoryTextBox.BorderStyle = BorderStyle.FixedSingle;
        waveProgramDirectoryTextBox.Dock = DockStyle.Fill;
        waveProgramDirectoryTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        waveProgramDirectoryTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        waveProgramDirectoryTextBox.Location = new Point(143, 95);
        waveProgramDirectoryTextBox.Name = "waveProgramDirectoryTextBox";
        waveProgramDirectoryTextBox.Size = new Size(579, 23);
        waveProgramDirectoryTextBox.TabIndex = 6;
        //
        // browseWaveProgramDirectoryButton
        //
        browseWaveProgramDirectoryButton.BackColor = Color.FromArgb(248, 250, 252);
        browseWaveProgramDirectoryButton.Dock = DockStyle.Fill;
        browseWaveProgramDirectoryButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
        browseWaveProgramDirectoryButton.FlatStyle = FlatStyle.Flat;
        browseWaveProgramDirectoryButton.Font = new Font("Microsoft YaHei UI", 9F);
        browseWaveProgramDirectoryButton.Location = new Point(728, 95);
        browseWaveProgramDirectoryButton.Name = "browseWaveProgramDirectoryButton";
        browseWaveProgramDirectoryButton.Size = new Size(84, 28);
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
        saveWaveProgramDirectoryButton.Location = new Point(818, 95);
        saveWaveProgramDirectoryButton.Name = "saveWaveProgramDirectoryButton";
        saveWaveProgramDirectoryButton.Size = new Size(106, 28);
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
        _databaseGroup.Location = new Point(18, 551);
        _databaseGroup.Margin = new Padding(2, 3, 2, 3);
        _databaseGroup.Name = "_databaseGroup";
        _databaseGroup.Padding = new Padding(11, 15, 11, 8);
        _databaseGroup.Size = new Size(949, 150);
        _databaseGroup.TabIndex = 4;
        _databaseGroup.TabStop = false;
        _databaseGroup.Text = "SQL Server 任务索引";
        //
        // _databaseLayout
        //
        _databaseLayout.ColumnCount = 5;
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 128F));
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 88F));
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
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
        _databaseLayout.Size = new Size(927, 110);
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
        _databaseConnectionTextBox.BorderStyle = BorderStyle.FixedSingle;
        _databaseLayout.SetColumnSpan(_databaseConnectionTextBox, 3);
        _databaseConnectionTextBox.Dock = DockStyle.Fill;
        _databaseConnectionTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        _databaseConnectionTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        _databaseConnectionTextBox.Location = new Point(130, 3);
        _databaseConnectionTextBox.Margin = new Padding(2, 3, 2, 3);
        _databaseConnectionTextBox.Name = "_databaseConnectionTextBox";
        _databaseConnectionTextBox.PlaceholderText = "SQL Server 连接字符串";
        _databaseConnectionTextBox.Size = new Size(682, 23);
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
        _databaseUserNameTextBox.BorderStyle = BorderStyle.FixedSingle;
        _databaseUserNameTextBox.Dock = DockStyle.Fill;
        _databaseUserNameTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        _databaseUserNameTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        _databaseUserNameTextBox.Location = new Point(130, 39);
        _databaseUserNameTextBox.Margin = new Padding(2, 3, 2, 3);
        _databaseUserNameTextBox.Name = "_databaseUserNameTextBox";
        _databaseUserNameTextBox.PlaceholderText = "SQL Server 账号";
        _databaseUserNameTextBox.Size = new Size(295, 23);
        _databaseUserNameTextBox.TabIndex = 3;
        //
        // _databasePasswordLabel
        //
        _databasePasswordLabel.Dock = DockStyle.Fill;
        _databasePasswordLabel.Font = new Font("Microsoft YaHei UI", 9F);
        _databasePasswordLabel.ForeColor = Color.FromArgb(71, 85, 105);
        _databasePasswordLabel.Location = new Point(429, 36);
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
        _databasePasswordTextBox.BorderStyle = BorderStyle.FixedSingle;
        _databasePasswordTextBox.Dock = DockStyle.Fill;
        _databasePasswordTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        _databasePasswordTextBox.ForeColor = Color.FromArgb(15, 23, 42);
        _databasePasswordTextBox.Location = new Point(517, 39);
        _databasePasswordTextBox.Margin = new Padding(2, 3, 2, 3);
        _databasePasswordTextBox.Name = "_databasePasswordTextBox";
        _databasePasswordTextBox.PlaceholderText = "SQL Server 密码";
        _databasePasswordTextBox.Size = new Size(295, 23);
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
        _saveDatabaseButton.Location = new Point(816, 3);
        _saveDatabaseButton.Margin = new Padding(2, 3, 2, 3);
        _saveDatabaseButton.Name = "_saveDatabaseButton";
        _saveDatabaseButton.Size = new Size(109, 30);
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
        _databaseStateLabel.Location = new Point(2, 72);
        _databaseStateLabel.Margin = new Padding(2, 0, 2, 0);
        _databaseStateLabel.Name = "_databaseStateLabel";
        _databaseStateLabel.Size = new Size(923, 38);
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
        Size = new Size(985, 606);
        pagePanel.ResumeLayout(false);
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
