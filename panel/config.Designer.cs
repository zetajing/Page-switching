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
    private Button saveWaveGeneratorButton;
    private TextBox waveGeneratorPathTextBox;
    private Button browseWaveGeneratorButton;
    private Label waveGeneratorStateLabel;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

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
        rootLayout.SuspendLayout();
        routerGroup.SuspendLayout();
        routerLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)routerTcpPortInput).BeginInit();
        SuspendLayout();
        // rootLayout
        rootLayout.BackColor = Color.FromArgb(241, 245, 249);
        waveGeneratorGroup.SuspendLayout();
        waveGeneratorLayout.SuspendLayout();
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(titleLabel, 0, 0);
        rootLayout.Controls.Add(waveGeneratorGroup, 0, 3);
        rootLayout.Controls.Add(subtitleLabel, 0, 1);
        rootLayout.Controls.Add(routerGroup, 0, 2);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(20);
        rootLayout.RowCount = 4;
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 455F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
        // titleLabel
        titleLabel.Dock = DockStyle.Fill;
        titleLabel.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
        titleLabel.Name = "titleLabel";
        titleLabel.Text = "通信配置";
        titleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // subtitleLabel
        subtitleLabel.Dock = DockStyle.Fill;
        subtitleLabel.Font = new Font("Microsoft YaHei UI", 9F);
        subtitleLabel.ForeColor = Color.FromArgb(71, 85, 105);
        subtitleLabel.Name = "subtitleLabel";
        subtitleLabel.Text = "TwinCAT ADS TCP Router 与倍福 PLC 路由参数";
        subtitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // routerGroup
        routerGroup.Controls.Add(routerLayout);
        routerGroup.Dock = DockStyle.Fill;
        routerGroup.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
        routerGroup.ForeColor = Color.FromArgb(15, 23, 42);
        routerGroup.Name = "routerGroup";
        routerGroup.Padding = new Padding(14, 18, 14, 14);
        routerGroup.Text = "TwinCAT.Ads.TcpRouter";
        // routerLayout
        routerLayout.ColumnCount = 2;
        routerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
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
        routerLayout.SetColumnSpan(routerNoticeLabel, 2);
        routerLayout.Dock = DockStyle.Fill;
        routerLayout.Name = "routerLayout";
        routerLayout.RowCount = 10;
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        routerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        // routerEnabledCheckBox
        routerEnabledCheckBox.Dock = DockStyle.Fill;
        routerEnabledCheckBox.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        routerEnabledCheckBox.Name = "routerEnabledCheckBox";
        routerEnabledCheckBox.Text = "启用独立 TCP Router";
        routerEnabledCheckBox.CheckedChanged += RouterEnabledCheckBox_CheckedChanged;
        // routerStateLabel
        routerStateLabel.Dock = DockStyle.Fill;
        routerStateLabel.Font = new Font("Microsoft YaHei UI", 9F);
        routerStateLabel.Name = "routerStateLabel";
        routerStateLabel.TextAlign = ContentAlignment.MiddleLeft;
        // routerNameLabel
        routerNameLabel.Dock = DockStyle.Fill;
        routerNameLabel.Font = new Font("Microsoft YaHei UI", 9F);
        routerNameLabel.ForeColor = Color.FromArgb(71, 85, 105);
        routerNameLabel.Name = "routerNameLabel";
        routerNameLabel.Text = "Router 名称";
        routerNameLabel.TextAlign = ContentAlignment.MiddleLeft;
        // routerNameTextBox
        routerNameTextBox.Dock = DockStyle.Fill;
        routerNameTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        routerNameTextBox.Margin = new Padding(3, 6, 3, 6);
        routerNameTextBox.Name = "routerNameTextBox";
        // localNetIdLabel
        localNetIdLabel.Dock = DockStyle.Fill;
        localNetIdLabel.Font = new Font("Microsoft YaHei UI", 9F);
        localNetIdLabel.ForeColor = Color.FromArgb(71, 85, 105);
        localNetIdLabel.Name = "localNetIdLabel";
        localNetIdLabel.Text = "本机 AMS Net ID";
        localNetIdLabel.TextAlign = ContentAlignment.MiddleLeft;
        // localNetIdTextBox
        localNetIdTextBox.Dock = DockStyle.Fill;
        localNetIdTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        localNetIdTextBox.Margin = new Padding(3, 6, 3, 6);
        localNetIdTextBox.Name = "localNetIdTextBox";
        // routerTcpPortLabel
        routerTcpPortLabel.Dock = DockStyle.Fill;
        routerTcpPortLabel.Font = new Font("Microsoft YaHei UI", 9F);
        routerTcpPortLabel.ForeColor = Color.FromArgb(71, 85, 105);
        routerTcpPortLabel.Name = "routerTcpPortLabel";
        routerTcpPortLabel.Text = "Router TCP 端口";
        routerTcpPortLabel.TextAlign = ContentAlignment.MiddleLeft;
        // routerTcpPortInput
        routerTcpPortInput.Dock = DockStyle.Fill;
        routerTcpPortInput.Font = new Font("Microsoft YaHei UI", 10F);
        routerTcpPortInput.Margin = new Padding(3, 6, 3, 6);
        routerTcpPortInput.Maximum = 65535;
        routerTcpPortInput.Minimum = 1;
        routerTcpPortInput.Name = "routerTcpPortInput";
        routerTcpPortInput.Value = 48898;
        // remoteNameLabel
        remoteNameLabel.Dock = DockStyle.Fill;
        remoteNameLabel.Font = new Font("Microsoft YaHei UI", 9F);
        remoteNameLabel.ForeColor = Color.FromArgb(71, 85, 105);
        remoteNameLabel.Name = "remoteNameLabel";
        remoteNameLabel.Text = "PLC 路由名称";
        remoteNameLabel.TextAlign = ContentAlignment.MiddleLeft;
        // remoteNameTextBox
        remoteNameTextBox.Dock = DockStyle.Fill;
        remoteNameTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        remoteNameTextBox.Margin = new Padding(3, 6, 3, 6);
        remoteNameTextBox.Name = "remoteNameTextBox";
        // remoteAddressLabel
        remoteAddressLabel.Dock = DockStyle.Fill;
        remoteAddressLabel.Font = new Font("Microsoft YaHei UI", 9F);
        remoteAddressLabel.ForeColor = Color.FromArgb(71, 85, 105);
        remoteAddressLabel.Name = "remoteAddressLabel";
        remoteAddressLabel.Text = "PLC IP 地址";
        remoteAddressLabel.TextAlign = ContentAlignment.MiddleLeft;
        // remoteAddressTextBox
        remoteAddressTextBox.Dock = DockStyle.Fill;
        remoteAddressTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        remoteAddressTextBox.Margin = new Padding(3, 6, 3, 6);
        remoteAddressTextBox.Name = "remoteAddressTextBox";
        // remoteNetIdLabel
        remoteNetIdLabel.Dock = DockStyle.Fill;
        remoteNetIdLabel.Font = new Font("Microsoft YaHei UI", 9F);
        remoteNetIdLabel.ForeColor = Color.FromArgb(71, 85, 105);
        remoteNetIdLabel.Name = "remoteNetIdLabel";
        remoteNetIdLabel.Text = "PLC AMS Net ID";
        remoteNetIdLabel.TextAlign = ContentAlignment.MiddleLeft;
        // remoteNetIdTextBox
        remoteNetIdTextBox.Dock = DockStyle.Fill;
        remoteNetIdTextBox.Font = new Font("Microsoft YaHei UI", 10F);
        remoteNetIdTextBox.Margin = new Padding(3, 6, 3, 6);
        remoteNetIdTextBox.Name = "remoteNetIdTextBox";
        // saveRouterButton
        saveRouterButton.Anchor = AnchorStyles.Left;
        saveRouterButton.BackColor = Color.FromArgb(37, 99, 235);
        saveRouterButton.FlatAppearance.BorderSize = 0;
        saveRouterButton.FlatStyle = FlatStyle.Flat;
        saveRouterButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        saveRouterButton.ForeColor = Color.White;
        saveRouterButton.Name = "saveRouterButton";
        saveRouterButton.Size = new Size(160, 38);
        saveRouterButton.Text = "保存 Router 配置";
        saveRouterButton.UseVisualStyleBackColor = false;
        saveRouterButton.Click += SaveRouterButton_Click;
        // saveResultLabel
        saveResultLabel.Dock = DockStyle.Fill;
        saveResultLabel.Font = new Font("Microsoft YaHei UI", 9F);
        saveResultLabel.Name = "saveResultLabel";
        saveResultLabel.TextAlign = ContentAlignment.MiddleLeft;
        // routerNoticeLabel
        routerNoticeLabel.Dock = DockStyle.Fill;
        routerNoticeLabel.Font = new Font("Microsoft YaHei UI", 9F);
        routerNoticeLabel.ForeColor = Color.FromArgb(180, 83, 9);
        routerNoticeLabel.Name = "routerNoticeLabel";
        routerNoticeLabel.Padding = new Padding(0, 10, 0, 0);
        routerNoticeLabel.Text = "提示：系统 TwinCAT Router 已运行时请关闭独立 Router，否则 TCP 48898 端口会冲突。远程 PLC 还需要配置返回本机 AMS Net ID 的路由。";
        // waveGeneratorGroup
        waveGeneratorGroup.Controls.Add(waveGeneratorLayout);
        waveGeneratorGroup.Dock = DockStyle.Fill;
        waveGeneratorGroup.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
        waveGeneratorGroup.ForeColor = Color.FromArgb(15, 23, 42);
        waveGeneratorGroup.Name = "waveGeneratorGroup";
        waveGeneratorGroup.Padding = new Padding(14, 18, 14, 10);
        waveGeneratorGroup.TabStop = false;
        waveGeneratorGroup.Text = "波形生成器";
        // waveGeneratorLayout
        waveGeneratorLayout.ColumnCount = 4;
        waveGeneratorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        waveGeneratorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        waveGeneratorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
        waveGeneratorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
        waveGeneratorLayout.Controls.Add(waveGeneratorPathLabel, 0, 0);
        waveGeneratorLayout.Controls.Add(waveGeneratorPathTextBox, 1, 0);
        waveGeneratorLayout.Controls.Add(browseWaveGeneratorButton, 2, 0);
        waveGeneratorLayout.Controls.Add(saveWaveGeneratorButton, 3, 0);
        waveGeneratorLayout.Controls.Add(waveGeneratorStateLabel, 1, 1);
        waveGeneratorLayout.SetColumnSpan(waveGeneratorStateLabel, 3);
        waveGeneratorLayout.Dock = DockStyle.Fill;
        waveGeneratorLayout.Name = "waveGeneratorLayout";
        waveGeneratorLayout.RowCount = 2;
        waveGeneratorLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        waveGeneratorLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        // waveGeneratorPathLabel
        waveGeneratorPathLabel.Dock = DockStyle.Fill;
        waveGeneratorPathLabel.ForeColor = Color.FromArgb(71, 85, 105);
        waveGeneratorPathLabel.Name = "waveGeneratorPathLabel";
        waveGeneratorPathLabel.Text = "WFast.exe 路径";
        waveGeneratorPathLabel.TextAlign = ContentAlignment.MiddleLeft;
        // waveGeneratorPathTextBox
        waveGeneratorPathTextBox.Dock = DockStyle.Fill;
        waveGeneratorPathTextBox.Name = "waveGeneratorPathTextBox";
        waveGeneratorPathTextBox.TextChanged += WaveGeneratorPathTextBox_TextChanged;
        // browseWaveGeneratorButton
        browseWaveGeneratorButton.Dock = DockStyle.Fill;
        browseWaveGeneratorButton.Name = "browseWaveGeneratorButton";
        browseWaveGeneratorButton.Text = "浏览...";
        browseWaveGeneratorButton.UseVisualStyleBackColor = true;
        browseWaveGeneratorButton.Click += BrowseWaveGeneratorButton_Click;
        // saveWaveGeneratorButton
        saveWaveGeneratorButton.Dock = DockStyle.Fill;
        saveWaveGeneratorButton.Font = new Font("Microsoft YaHei UI", 9F);
        saveWaveGeneratorButton.Name = "saveWaveGeneratorButton";
        saveWaveGeneratorButton.Text = "保存路径";
        saveWaveGeneratorButton.UseVisualStyleBackColor = true;
        saveWaveGeneratorButton.Click += SaveWaveGeneratorButton_Click;
        // waveGeneratorStateLabel
        waveGeneratorStateLabel.Dock = DockStyle.Fill;
        waveGeneratorStateLabel.Font = new Font("Microsoft YaHei UI", 8F);
        waveGeneratorStateLabel.Name = "waveGeneratorStateLabel";
        waveGeneratorStateLabel.TextAlign = ContentAlignment.MiddleLeft;
        // Config
        AutoScaleDimensions = new SizeF(9F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(rootLayout);
        Name = "Config";
        Size = new Size(905, 681);
        rootLayout.ResumeLayout(false);
        routerGroup.ResumeLayout(false);
        routerLayout.ResumeLayout(false);
        routerLayout.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)routerTcpPortInput).EndInit();
        waveGeneratorGroup.ResumeLayout(false);
        waveGeneratorLayout.ResumeLayout(false);
        ResumeLayout(false);
    }
}
