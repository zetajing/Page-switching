namespace Page_switching.panel
{
    partial class Wave_Height_Meter
    {
        private Panel pagePanel;

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pagePanel = new Panel();
            rootLayout = new TableLayoutPanel();
            headerPanel = new Panel();
            titleLabel = new Label();
            subtitleLabel = new Label();
            connectionCard = new Panel();
            connectionLayout = new TableLayoutPanel();
            connectionSettings = new FlowLayoutPanel();
            ipField = new Panel();
            ipCaption = new Label();
            _ipAddressInput = new TextBox();
            portField = new Panel();
            portCaption = new Label();
            _portInput = new NumericUpDown();
            sampleRateField = new Panel();
            sampleRateCaption = new Label();
            _sampleRateInput = new NumericUpDown();
            sampleLimitField = new Panel();
            sampleLimitCaption = new Label();
            _sampleLimitInput = new NumericUpDown();
            _connectButton = new Button();
            _disconnectButton = new Button();
            _saveSettingsButton = new Button();
            connectionStatusPanel = new Panel();
            connectionStatusCaption = new Label();
            _connectionStatusLabel = new Label();
            channelStatusPanel = new FlowLayoutPanel();
            channel1Selector = new CheckBox();
            channel2Selector = new CheckBox();
            channel3Selector = new CheckBox();
            channel4Selector = new CheckBox();
            channel5Selector = new CheckBox();
            channel6Selector = new CheckBox();
            readoutLayout = new TableLayoutPanel();
            latestValueCard = new Panel();
            latestValueCaption = new Label();
            _latestValueLabel = new Label();
            latestValueDetail = new Label();
            sampleCountCard = new Panel();
            sampleCountCaption = new Label();
            _sampleCountLabel = new Label();
            sampleCountDetail = new Label();
            sampleRateCard = new Panel();
            sampleRateCardCaption = new Label();
            _sampleRateLabel = new Label();
            sampleRateDetail = new Label();
            chartCard = new Panel();
            chartLayout = new TableLayoutPanel();
            chartHeader = new TableLayoutPanel();
            chartTitleLabel = new Label();
            chartActions = new FlowLayoutPanel();
            _clearButton = new Button();
            _exportButton = new Button();
            _chartStatusLabel = new Label();
            channelTabs = new TabControl();
            channelOneTab = new TabPage();
            _waveformPreview = new WaveformPreviewControl();
            channel2Tab = new TabPage();
            channel2Notice = new Label();
            channel3Tab = new TabPage();
            channel3Notice = new Label();
            channel4Tab = new TabPage();
            channel4Notice = new Label();
            channel5Tab = new TabPage();
            channel5Notice = new Label();
            channel6Tab = new TabPage();
            channel6Notice = new Label();
            pagePanel.SuspendLayout();
            rootLayout.SuspendLayout();
            headerPanel.SuspendLayout();
            connectionCard.SuspendLayout();
            connectionLayout.SuspendLayout();
            connectionSettings.SuspendLayout();
            ipField.SuspendLayout();
            portField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_portInput).BeginInit();
            sampleRateField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_sampleRateInput).BeginInit();
            sampleLimitField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_sampleLimitInput).BeginInit();
            connectionStatusPanel.SuspendLayout();
            channelStatusPanel.SuspendLayout();
            readoutLayout.SuspendLayout();
            latestValueCard.SuspendLayout();
            sampleCountCard.SuspendLayout();
            sampleRateCard.SuspendLayout();
            chartCard.SuspendLayout();
            chartLayout.SuspendLayout();
            chartHeader.SuspendLayout();
            chartActions.SuspendLayout();
            channelTabs.SuspendLayout();
            channelOneTab.SuspendLayout();
            channel2Tab.SuspendLayout();
            channel3Tab.SuspendLayout();
            channel4Tab.SuspendLayout();
            channel5Tab.SuspendLayout();
            channel6Tab.SuspendLayout();
            SuspendLayout();
            //
            // pagePanel
            //
            pagePanel.Controls.Add(rootLayout);
            pagePanel.Dock = DockStyle.Fill;
            pagePanel.Location = new Point(0, 0);
            pagePanel.Name = "pagePanel";
            pagePanel.Size = new Size(1104, 606);
            pagePanel.TabIndex = 0;
            //
            // rootLayout
            //
            rootLayout.BackColor = Color.FromArgb(241, 245, 249);
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(headerPanel, 0, 0);
            rootLayout.Controls.Add(connectionCard, 0, 1);
            rootLayout.Controls.Add(channelStatusPanel, 0, 2);
            rootLayout.Controls.Add(readoutLayout, 0, 3);
            rootLayout.Controls.Add(chartCard, 0, 4);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(16);
            rootLayout.RowCount = 5;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 112F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.Size = new Size(1104, 606);
            rootLayout.TabIndex = 0;
            //
            // headerPanel
            //
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Controls.Add(subtitleLabel);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Location = new Point(16, 16);
            headerPanel.Margin = new Padding(0, 0, 0, 8);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(1072, 42);
            headerPanel.TabIndex = 0;
            //
            // titleLabel
            //
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            titleLabel.Location = new Point(0, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(123, 30);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "浪高仪监测";
            //
            // subtitleLabel
            //
            subtitleLabel.AutoSize = true;
            subtitleLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            subtitleLabel.ForeColor = Color.FromArgb(100, 116, 139);
            subtitleLabel.Location = new Point(0, 30);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(187, 17);
            subtitleLabel.TabIndex = 1;
            subtitleLabel.Text = "TCP 采集 · 通道 1 · 仪器原始计数";
            //
            // connectionCard
            //
            connectionCard.BackColor = Color.White;
            connectionCard.Controls.Add(connectionLayout);
            connectionCard.Dock = DockStyle.Fill;
            connectionCard.Location = new Point(16, 70);
            connectionCard.Margin = new Padding(0, 4, 0, 4);
            connectionCard.Name = "connectionCard";
            connectionCard.Padding = new Padding(14, 10, 14, 10);
            connectionCard.Size = new Size(1072, 104);
            connectionCard.TabIndex = 1;
            //
            // connectionLayout
            //
            connectionLayout.ColumnCount = 1;
            connectionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            connectionLayout.Controls.Add(connectionSettings, 0, 0);
            connectionLayout.Controls.Add(connectionStatusPanel, 0, 1);
            connectionLayout.Dock = DockStyle.Fill;
            connectionLayout.Location = new Point(14, 10);
            connectionLayout.Margin = new Padding(0);
            connectionLayout.Name = "connectionLayout";
            connectionLayout.RowCount = 2;
            connectionLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            connectionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            connectionLayout.Size = new Size(1044, 84);
            connectionLayout.TabIndex = 0;
            //
            // connectionSettings
            //
            connectionSettings.AutoScroll = true;
            connectionSettings.Controls.Add(ipField);
            connectionSettings.Controls.Add(portField);
            connectionSettings.Controls.Add(sampleRateField);
            connectionSettings.Controls.Add(sampleLimitField);
            connectionSettings.Controls.Add(_connectButton);
            connectionSettings.Controls.Add(_disconnectButton);
            connectionSettings.Controls.Add(_saveSettingsButton);
            connectionSettings.Dock = DockStyle.Fill;
            connectionSettings.Location = new Point(0, 0);
            connectionSettings.Margin = new Padding(0);
            connectionSettings.Name = "connectionSettings";
            connectionSettings.Size = new Size(1044, 56);
            connectionSettings.TabIndex = 0;
            connectionSettings.WrapContents = false;
            //
            // ipField
            //
            ipField.Controls.Add(ipCaption);
            ipField.Controls.Add(_ipAddressInput);
            ipField.Location = new Point(0, 0);
            ipField.Margin = new Padding(0, 0, 8, 0);
            ipField.Name = "ipField";
            ipField.Size = new Size(170, 54);
            ipField.TabIndex = 0;
            //
            // ipCaption
            //
            ipCaption.AutoSize = true;
            ipCaption.Font = new Font("Microsoft YaHei UI", 8F);
            ipCaption.ForeColor = Color.FromArgb(71, 85, 105);
            ipCaption.Location = new Point(0, 0);
            ipCaption.Name = "ipCaption";
            ipCaption.Size = new Size(42, 16);
            ipCaption.TabIndex = 0;
            ipCaption.Text = "设备 IP";
            //
            // _ipAddressInput
            //
            _ipAddressInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _ipAddressInput.BackColor = Color.White;
            _ipAddressInput.BorderStyle = BorderStyle.FixedSingle;
            _ipAddressInput.Font = new Font("Microsoft YaHei UI", 9F);
            _ipAddressInput.ForeColor = Color.FromArgb(15, 23, 42);
            _ipAddressInput.Location = new Point(0, 23);
            _ipAddressInput.Name = "_ipAddressInput";
            _ipAddressInput.Size = new Size(166, 23);
            _ipAddressInput.TabIndex = 0;
            _ipAddressInput.Text = "192.168.0.7";
            //
            // portField
            //
            portField.Controls.Add(portCaption);
            portField.Controls.Add(_portInput);
            portField.Location = new Point(178, 0);
            portField.Margin = new Padding(0, 0, 8, 0);
            portField.Name = "portField";
            portField.Size = new Size(80, 54);
            portField.TabIndex = 1;
            //
            // portCaption
            //
            portCaption.AutoSize = true;
            portCaption.Font = new Font("Microsoft YaHei UI", 8F);
            portCaption.ForeColor = Color.FromArgb(71, 85, 105);
            portCaption.Location = new Point(0, 0);
            portCaption.Name = "portCaption";
            portCaption.Size = new Size(29, 16);
            portCaption.TabIndex = 0;
            portCaption.Text = "端口";
            //
            // _portInput
            //
            _portInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _portInput.BackColor = Color.White;
            _portInput.BorderStyle = BorderStyle.FixedSingle;
            _portInput.Font = new Font("Microsoft YaHei UI", 9F);
            _portInput.ForeColor = Color.FromArgb(15, 23, 42);
            _portInput.Location = new Point(0, 23);
            _portInput.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            _portInput.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            _portInput.Name = "_portInput";
            _portInput.Size = new Size(76, 23);
            _portInput.TabIndex = 1;
            _portInput.Value = new decimal(new int[] { 502, 0, 0, 0 });
            //
            // sampleRateField
            //
            sampleRateField.Controls.Add(sampleRateCaption);
            sampleRateField.Controls.Add(_sampleRateInput);
            sampleRateField.Location = new Point(266, 0);
            sampleRateField.Margin = new Padding(0, 0, 8, 0);
            sampleRateField.Name = "sampleRateField";
            sampleRateField.Size = new Size(110, 54);
            sampleRateField.TabIndex = 2;
            //
            // sampleRateCaption
            //
            sampleRateCaption.AutoSize = true;
            sampleRateCaption.Font = new Font("Microsoft YaHei UI", 8F);
            sampleRateCaption.ForeColor = Color.FromArgb(71, 85, 105);
            sampleRateCaption.Location = new Point(0, 0);
            sampleRateCaption.Name = "sampleRateCaption";
            sampleRateCaption.Size = new Size(76, 16);
            sampleRateCaption.TabIndex = 0;
            sampleRateCaption.Text = "采样频率 (Hz)";
            //
            // _sampleRateInput
            //
            _sampleRateInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _sampleRateInput.BackColor = Color.White;
            _sampleRateInput.BorderStyle = BorderStyle.FixedSingle;
            _sampleRateInput.Font = new Font("Microsoft YaHei UI", 9F);
            _sampleRateInput.ForeColor = Color.FromArgb(15, 23, 42);
            _sampleRateInput.Location = new Point(0, 23);
            _sampleRateInput.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            _sampleRateInput.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            _sampleRateInput.Name = "_sampleRateInput";
            _sampleRateInput.Size = new Size(106, 23);
            _sampleRateInput.TabIndex = 2;
            _sampleRateInput.Value = new decimal(new int[] { 50, 0, 0, 0 });
            //
            // sampleLimitField
            //
            sampleLimitField.Controls.Add(sampleLimitCaption);
            sampleLimitField.Controls.Add(_sampleLimitInput);
            sampleLimitField.Location = new Point(384, 0);
            sampleLimitField.Margin = new Padding(0, 0, 8, 0);
            sampleLimitField.Name = "sampleLimitField";
            sampleLimitField.Size = new Size(95, 54);
            sampleLimitField.TabIndex = 3;
            //
            // sampleLimitCaption
            //
            sampleLimitCaption.AutoSize = true;
            sampleLimitCaption.Font = new Font("Microsoft YaHei UI", 8F);
            sampleLimitCaption.Location = new Point(0, 0);
            sampleLimitCaption.Name = "sampleLimitCaption";
            sampleLimitCaption.Size = new Size(51, 16);
            sampleLimitCaption.TabIndex = 0;
            sampleLimitCaption.Text = "采样点数";
            //
            // _sampleLimitInput
            //
            _sampleLimitInput.BackColor = Color.White;
            _sampleLimitInput.BorderStyle = BorderStyle.FixedSingle;
            _sampleLimitInput.ForeColor = Color.FromArgb(15, 23, 42);
            _sampleLimitInput.Location = new Point(0, 23);
            _sampleLimitInput.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            _sampleLimitInput.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            _sampleLimitInput.Name = "_sampleLimitInput";
            _sampleLimitInput.Size = new Size(91, 23);
            _sampleLimitInput.TabIndex = 1;
            _sampleLimitInput.Value = new decimal(new int[] { 4096, 0, 0, 0 });
            //
            // _connectButton
            //
            _connectButton.BackColor = Color.FromArgb(29, 78, 216);
            _connectButton.Cursor = Cursors.Hand;
            _connectButton.FlatAppearance.BorderSize = 0;
            _connectButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 64, 175);
            _connectButton.FlatStyle = FlatStyle.Flat;
            _connectButton.Font = new Font("Microsoft YaHei UI", 8.5F, FontStyle.Bold);
            _connectButton.ForeColor = Color.White;
            _connectButton.Location = new Point(487, 20);
            _connectButton.Margin = new Padding(0, 20, 6, 0);
            _connectButton.Name = "_connectButton";
            _connectButton.Size = new Size(104, 34);
            _connectButton.TabIndex = 3;
            _connectButton.Text = "连接并采集";
            _connectButton.UseVisualStyleBackColor = false;
            _connectButton.Click += ConnectButton_Click;
            //
            // _disconnectButton
            //
            _disconnectButton.BackColor = Color.FromArgb(248, 250, 252);
            _disconnectButton.Cursor = Cursors.Hand;
            _disconnectButton.Enabled = false;
            _disconnectButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            _disconnectButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
            _disconnectButton.FlatStyle = FlatStyle.Flat;
            _disconnectButton.Font = new Font("Microsoft YaHei UI", 8.5F, FontStyle.Bold);
            _disconnectButton.ForeColor = Color.FromArgb(15, 23, 42);
            _disconnectButton.Location = new Point(597, 20);
            _disconnectButton.Margin = new Padding(0, 20, 6, 0);
            _disconnectButton.Name = "_disconnectButton";
            _disconnectButton.Size = new Size(100, 34);
            _disconnectButton.TabIndex = 4;
            _disconnectButton.Text = "停止并断开";
            _disconnectButton.UseVisualStyleBackColor = false;
            _disconnectButton.Click += DisconnectButton_Click;
            //
            // _saveSettingsButton
            //
            _saveSettingsButton.BackColor = Color.FromArgb(248, 250, 252);
            _saveSettingsButton.Cursor = Cursors.Hand;
            _saveSettingsButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            _saveSettingsButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
            _saveSettingsButton.FlatStyle = FlatStyle.Flat;
            _saveSettingsButton.Font = new Font("Microsoft YaHei UI", 8.5F, FontStyle.Bold);
            _saveSettingsButton.ForeColor = Color.FromArgb(15, 23, 42);
            _saveSettingsButton.Location = new Point(703, 20);
            _saveSettingsButton.Margin = new Padding(0, 20, 6, 0);
            _saveSettingsButton.Name = "_saveSettingsButton";
            _saveSettingsButton.Size = new Size(88, 34);
            _saveSettingsButton.TabIndex = 5;
            _saveSettingsButton.Text = "保存参数";
            _saveSettingsButton.UseVisualStyleBackColor = false;
            _saveSettingsButton.Click += SaveSettingsButton_Click;
            //
            // connectionStatusPanel
            //
            connectionStatusPanel.Controls.Add(connectionStatusCaption);
            connectionStatusPanel.Controls.Add(_connectionStatusLabel);
            connectionStatusPanel.Dock = DockStyle.Fill;
            connectionStatusPanel.Location = new Point(0, 56);
            connectionStatusPanel.Margin = new Padding(0);
            connectionStatusPanel.Name = "connectionStatusPanel";
            connectionStatusPanel.Size = new Size(1044, 28);
            connectionStatusPanel.TabIndex = 1;
            //
            // connectionStatusCaption
            //
            connectionStatusCaption.AutoSize = true;
            connectionStatusCaption.Font = new Font("Microsoft YaHei UI", 8.5F);
            connectionStatusCaption.ForeColor = Color.FromArgb(71, 85, 105);
            connectionStatusCaption.Location = new Point(0, 5);
            connectionStatusCaption.Name = "connectionStatusCaption";
            connectionStatusCaption.Size = new Size(56, 17);
            connectionStatusCaption.TabIndex = 0;
            connectionStatusCaption.Text = "连接状态";
            //
            // _connectionStatusLabel
            //
            _connectionStatusLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _connectionStatusLabel.AutoEllipsis = true;
            _connectionStatusLabel.Font = new Font("Microsoft YaHei UI", 9F);
            _connectionStatusLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _connectionStatusLabel.Location = new Point(78, 4);
            _connectionStatusLabel.Name = "_connectionStatusLabel";
            _connectionStatusLabel.Size = new Size(1624, 24);
            _connectionStatusLabel.TabIndex = 1;
            _connectionStatusLabel.Text = "设备未连接";
            //
            // channelStatusPanel
            //
            channelStatusPanel.AutoScroll = true;
            channelStatusPanel.Controls.Add(channel1Selector);
            channelStatusPanel.Controls.Add(channel2Selector);
            channelStatusPanel.Controls.Add(channel3Selector);
            channelStatusPanel.Controls.Add(channel4Selector);
            channelStatusPanel.Controls.Add(channel5Selector);
            channelStatusPanel.Controls.Add(channel6Selector);
            channelStatusPanel.Dock = DockStyle.Fill;
            channelStatusPanel.Location = new Point(16, 182);
            channelStatusPanel.Margin = new Padding(0, 4, 0, 4);
            channelStatusPanel.Name = "channelStatusPanel";
            channelStatusPanel.Size = new Size(1072, 56);
            channelStatusPanel.TabIndex = 2;
            channelStatusPanel.WrapContents = false;
            //
            // channel1Selector
            //
            channel1Selector.Appearance = Appearance.Button;
            channel1Selector.BackColor = Color.White;
            channel1Selector.Checked = true;
            channel1Selector.CheckState = CheckState.Checked;
            channel1Selector.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            channel1Selector.FlatAppearance.CheckedBackColor = Color.FromArgb(219, 234, 254);
            channel1Selector.FlatStyle = FlatStyle.Flat;
            channel1Selector.Font = new Font("Microsoft YaHei UI", 9F);
            channel1Selector.ForeColor = Color.FromArgb(15, 23, 42);
            channel1Selector.Location = new Point(0, 0);
            channel1Selector.Margin = new Padding(0, 0, 8, 0);
            channel1Selector.Name = "channel1Selector";
            channel1Selector.Size = new Size(130, 56);
            channel1Selector.TabIndex = 0;
            channel1Selector.Text = "CH1\r\n等待采集";
            channel1Selector.TextAlign = ContentAlignment.MiddleCenter;
            channel1Selector.UseVisualStyleBackColor = false;
            //
            // channel2Selector
            //
            channel2Selector.Appearance = Appearance.Button;
            channel2Selector.BackColor = Color.White;
            channel2Selector.Enabled = false;
            channel2Selector.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            channel2Selector.FlatAppearance.CheckedBackColor = Color.FromArgb(219, 234, 254);
            channel2Selector.FlatStyle = FlatStyle.Flat;
            channel2Selector.Font = new Font("Microsoft YaHei UI", 9F);
            channel2Selector.ForeColor = Color.FromArgb(100, 116, 139);
            channel2Selector.Location = new Point(138, 0);
            channel2Selector.Margin = new Padding(0, 0, 8, 0);
            channel2Selector.Name = "channel2Selector";
            channel2Selector.Size = new Size(130, 56);
            channel2Selector.TabIndex = 1;
            channel2Selector.Text = "CH2\r\n待协议验证";
            channel2Selector.TextAlign = ContentAlignment.MiddleCenter;
            channel2Selector.UseVisualStyleBackColor = false;
            //
            // channel3Selector
            //
            channel3Selector.Appearance = Appearance.Button;
            channel3Selector.BackColor = Color.White;
            channel3Selector.Enabled = false;
            channel3Selector.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            channel3Selector.FlatAppearance.CheckedBackColor = Color.FromArgb(219, 234, 254);
            channel3Selector.FlatStyle = FlatStyle.Flat;
            channel3Selector.Font = new Font("Microsoft YaHei UI", 9F);
            channel3Selector.ForeColor = Color.FromArgb(100, 116, 139);
            channel3Selector.Location = new Point(276, 0);
            channel3Selector.Margin = new Padding(0, 0, 8, 0);
            channel3Selector.Name = "channel3Selector";
            channel3Selector.Size = new Size(130, 56);
            channel3Selector.TabIndex = 2;
            channel3Selector.Text = "CH3\r\n待协议验证";
            channel3Selector.TextAlign = ContentAlignment.MiddleCenter;
            channel3Selector.UseVisualStyleBackColor = false;
            //
            // channel4Selector
            //
            channel4Selector.Appearance = Appearance.Button;
            channel4Selector.BackColor = Color.White;
            channel4Selector.Enabled = false;
            channel4Selector.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            channel4Selector.FlatAppearance.CheckedBackColor = Color.FromArgb(219, 234, 254);
            channel4Selector.FlatStyle = FlatStyle.Flat;
            channel4Selector.Font = new Font("Microsoft YaHei UI", 9F);
            channel4Selector.ForeColor = Color.FromArgb(100, 116, 139);
            channel4Selector.Location = new Point(414, 0);
            channel4Selector.Margin = new Padding(0, 0, 8, 0);
            channel4Selector.Name = "channel4Selector";
            channel4Selector.Size = new Size(130, 56);
            channel4Selector.TabIndex = 3;
            channel4Selector.Text = "CH4\r\n待协议验证";
            channel4Selector.TextAlign = ContentAlignment.MiddleCenter;
            channel4Selector.UseVisualStyleBackColor = false;
            //
            // channel5Selector
            //
            channel5Selector.Appearance = Appearance.Button;
            channel5Selector.BackColor = Color.White;
            channel5Selector.Enabled = false;
            channel5Selector.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            channel5Selector.FlatAppearance.CheckedBackColor = Color.FromArgb(219, 234, 254);
            channel5Selector.FlatStyle = FlatStyle.Flat;
            channel5Selector.Font = new Font("Microsoft YaHei UI", 9F);
            channel5Selector.ForeColor = Color.FromArgb(100, 116, 139);
            channel5Selector.Location = new Point(552, 0);
            channel5Selector.Margin = new Padding(0, 0, 8, 0);
            channel5Selector.Name = "channel5Selector";
            channel5Selector.Size = new Size(130, 56);
            channel5Selector.TabIndex = 4;
            channel5Selector.Text = "CH5\r\n待协议验证";
            channel5Selector.TextAlign = ContentAlignment.MiddleCenter;
            channel5Selector.UseVisualStyleBackColor = false;
            //
            // channel6Selector
            //
            channel6Selector.Appearance = Appearance.Button;
            channel6Selector.BackColor = Color.White;
            channel6Selector.Enabled = false;
            channel6Selector.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            channel6Selector.FlatAppearance.CheckedBackColor = Color.FromArgb(219, 234, 254);
            channel6Selector.FlatStyle = FlatStyle.Flat;
            channel6Selector.Font = new Font("Microsoft YaHei UI", 9F);
            channel6Selector.ForeColor = Color.FromArgb(100, 116, 139);
            channel6Selector.Location = new Point(690, 0);
            channel6Selector.Margin = new Padding(0, 0, 8, 0);
            channel6Selector.Name = "channel6Selector";
            channel6Selector.Size = new Size(130, 56);
            channel6Selector.TabIndex = 5;
            channel6Selector.Text = "CH6\r\n待协议验证";
            channel6Selector.TextAlign = ContentAlignment.MiddleCenter;
            channel6Selector.UseVisualStyleBackColor = false;
            //
            // readoutLayout
            //
            readoutLayout.ColumnCount = 3;
            readoutLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            readoutLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            readoutLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.334F));
            readoutLayout.Controls.Add(latestValueCard, 0, 0);
            readoutLayout.Controls.Add(sampleCountCard, 1, 0);
            readoutLayout.Controls.Add(sampleRateCard, 2, 0);
            readoutLayout.Dock = DockStyle.Fill;
            readoutLayout.Location = new Point(16, 246);
            readoutLayout.Margin = new Padding(0, 4, 0, 4);
            readoutLayout.Name = "readoutLayout";
            readoutLayout.RowCount = 1;
            readoutLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            readoutLayout.Size = new Size(1072, 88);
            readoutLayout.TabIndex = 3;
            //
            // latestValueCard
            //
            latestValueCard.BackColor = Color.White;
            latestValueCard.Controls.Add(latestValueCaption);
            latestValueCard.Controls.Add(_latestValueLabel);
            latestValueCard.Controls.Add(latestValueDetail);
            latestValueCard.Dock = DockStyle.Fill;
            latestValueCard.Location = new Point(4, 4);
            latestValueCard.Margin = new Padding(4);
            latestValueCard.Name = "latestValueCard";
            latestValueCard.Padding = new Padding(16, 10, 12, 8);
            latestValueCard.Size = new Size(349, 80);
            latestValueCard.TabIndex = 0;
            //
            // latestValueCaption
            //
            latestValueCaption.AutoSize = true;
            latestValueCaption.Font = new Font("Microsoft YaHei UI", 8.5F);
            latestValueCaption.ForeColor = Color.FromArgb(100, 116, 139);
            latestValueCaption.Location = new Point(12, 6);
            latestValueCaption.Name = "latestValueCaption";
            latestValueCaption.Size = new Size(68, 17);
            latestValueCaption.TabIndex = 0;
            latestValueCaption.Text = "当前原始值";
            //
            // _latestValueLabel
            //
            _latestValueLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _latestValueLabel.AutoEllipsis = true;
            _latestValueLabel.Font = new Font("Microsoft YaHei UI", 21F, FontStyle.Bold);
            _latestValueLabel.ForeColor = Color.FromArgb(15, 23, 42);
            _latestValueLabel.Location = new Point(12, 25);
            _latestValueLabel.Name = "_latestValueLabel";
            _latestValueLabel.Size = new Size(429, 34);
            _latestValueLabel.TabIndex = 1;
            _latestValueLabel.Text = "--";
            _latestValueLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // latestValueDetail
            //
            latestValueDetail.AutoSize = true;
            latestValueDetail.Font = new Font("Microsoft YaHei UI", 8F);
            latestValueDetail.ForeColor = Color.FromArgb(100, 116, 139);
            latestValueDetail.Location = new Point(12, 62);
            latestValueDetail.Name = "latestValueDetail";
            latestValueDetail.Size = new Size(88, 16);
            latestValueDetail.TabIndex = 2;
            latestValueDetail.Text = "16 位无符号计数";
            //
            // sampleCountCard
            //
            sampleCountCard.BackColor = Color.White;
            sampleCountCard.Controls.Add(sampleCountCaption);
            sampleCountCard.Controls.Add(_sampleCountLabel);
            sampleCountCard.Controls.Add(sampleCountDetail);
            sampleCountCard.Dock = DockStyle.Fill;
            sampleCountCard.Location = new Point(361, 4);
            sampleCountCard.Margin = new Padding(4);
            sampleCountCard.Name = "sampleCountCard";
            sampleCountCard.Padding = new Padding(16, 10, 12, 8);
            sampleCountCard.Size = new Size(349, 80);
            sampleCountCard.TabIndex = 1;
            //
            // sampleCountCaption
            //
            sampleCountCaption.AutoSize = true;
            sampleCountCaption.Font = new Font("Microsoft YaHei UI", 8.5F);
            sampleCountCaption.ForeColor = Color.FromArgb(100, 116, 139);
            sampleCountCaption.Location = new Point(12, 6);
            sampleCountCaption.Name = "sampleCountCaption";
            sampleCountCaption.Size = new Size(80, 17);
            sampleCountCaption.TabIndex = 0;
            sampleCountCaption.Text = "本次连接收到";
            //
            // _sampleCountLabel
            //
            _sampleCountLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _sampleCountLabel.AutoEllipsis = true;
            _sampleCountLabel.Font = new Font("Microsoft YaHei UI", 21F, FontStyle.Bold);
            _sampleCountLabel.ForeColor = Color.FromArgb(15, 23, 42);
            _sampleCountLabel.Location = new Point(12, 25);
            _sampleCountLabel.Name = "_sampleCountLabel";
            _sampleCountLabel.Size = new Size(429, 34);
            _sampleCountLabel.TabIndex = 1;
            _sampleCountLabel.Text = "0";
            _sampleCountLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // sampleCountDetail
            //
            sampleCountDetail.AutoSize = true;
            sampleCountDetail.Font = new Font("Microsoft YaHei UI", 8F);
            sampleCountDetail.ForeColor = Color.FromArgb(100, 116, 139);
            sampleCountDetail.Location = new Point(12, 62);
            sampleCountDetail.Name = "sampleCountDetail";
            sampleCountDetail.Size = new Size(40, 16);
            sampleCountDetail.TabIndex = 2;
            sampleCountDetail.Text = "样本数";
            //
            // sampleRateCard
            //
            sampleRateCard.BackColor = Color.White;
            sampleRateCard.Controls.Add(sampleRateCardCaption);
            sampleRateCard.Controls.Add(_sampleRateLabel);
            sampleRateCard.Controls.Add(sampleRateDetail);
            sampleRateCard.Dock = DockStyle.Fill;
            sampleRateCard.Location = new Point(718, 4);
            sampleRateCard.Margin = new Padding(4);
            sampleRateCard.Name = "sampleRateCard";
            sampleRateCard.Padding = new Padding(16, 10, 12, 8);
            sampleRateCard.Size = new Size(350, 80);
            sampleRateCard.TabIndex = 2;
            //
            // sampleRateCardCaption
            //
            sampleRateCardCaption.AutoSize = true;
            sampleRateCardCaption.Font = new Font("Microsoft YaHei UI", 8.5F);
            sampleRateCardCaption.ForeColor = Color.FromArgb(100, 116, 139);
            sampleRateCardCaption.Location = new Point(12, 6);
            sampleRateCardCaption.Name = "sampleRateCardCaption";
            sampleRateCardCaption.Size = new Size(56, 17);
            sampleRateCardCaption.TabIndex = 0;
            sampleRateCardCaption.Text = "采集通道";
            //
            // _sampleRateLabel
            //
            _sampleRateLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _sampleRateLabel.AutoEllipsis = true;
            _sampleRateLabel.Font = new Font("Microsoft YaHei UI", 21F, FontStyle.Bold);
            _sampleRateLabel.ForeColor = Color.FromArgb(15, 23, 42);
            _sampleRateLabel.Location = new Point(12, 25);
            _sampleRateLabel.Name = "_sampleRateLabel";
            _sampleRateLabel.Size = new Size(430, 34);
            _sampleRateLabel.TabIndex = 1;
            _sampleRateLabel.Text = "CH1 · 50 Hz";
            _sampleRateLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // sampleRateDetail
            //
            sampleRateDetail.AutoSize = true;
            sampleRateDetail.Font = new Font("Microsoft YaHei UI", 8F);
            sampleRateDetail.ForeColor = Color.FromArgb(100, 116, 139);
            sampleRateDetail.Location = new Point(12, 62);
            sampleRateDetail.Name = "sampleRateDetail";
            sampleRateDetail.Size = new Size(60, 16);
            sampleRateDetail.TabIndex = 2;
            sampleRateDetail.Text = "设备通道 1";
            //
            // chartCard
            //
            chartCard.BackColor = Color.White;
            chartCard.Controls.Add(chartLayout);
            chartCard.Dock = DockStyle.Fill;
            chartCard.Location = new Point(16, 342);
            chartCard.Margin = new Padding(0, 4, 0, 4);
            chartCard.Name = "chartCard";
            chartCard.Padding = new Padding(14, 10, 14, 10);
            chartCard.Size = new Size(1072, 244);
            chartCard.TabIndex = 4;
            //
            // chartLayout
            //
            chartLayout.ColumnCount = 1;
            chartLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            chartLayout.Controls.Add(chartHeader, 0, 0);
            chartLayout.Controls.Add(channelTabs, 0, 1);
            chartLayout.Dock = DockStyle.Fill;
            chartLayout.Location = new Point(14, 10);
            chartLayout.Margin = new Padding(0);
            chartLayout.Name = "chartLayout";
            chartLayout.RowCount = 2;
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            chartLayout.Size = new Size(1044, 224);
            chartLayout.TabIndex = 0;
            //
            // chartHeader
            //
            chartHeader.ColumnCount = 2;
            chartHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            chartHeader.ColumnStyles.Add(new ColumnStyle());
            chartHeader.Controls.Add(chartTitleLabel, 0, 0);
            chartHeader.Controls.Add(chartActions, 1, 0);
            chartHeader.Dock = DockStyle.Fill;
            chartHeader.Location = new Point(0, 0);
            chartHeader.Margin = new Padding(0);
            chartHeader.Name = "chartHeader";
            chartHeader.RowCount = 1;
            chartHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            chartHeader.Size = new Size(1044, 38);
            chartHeader.TabIndex = 0;
            //
            // chartTitleLabel
            //
            chartTitleLabel.Dock = DockStyle.Fill;
            chartTitleLabel.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            chartTitleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            chartTitleLabel.Location = new Point(3, 0);
            chartTitleLabel.Name = "chartTitleLabel";
            chartTitleLabel.Size = new Size(574, 38);
            chartTitleLabel.TabIndex = 0;
            chartTitleLabel.Text = "实时波形";
            chartTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // chartActions
            //
            chartActions.AutoSize = true;
            chartActions.Controls.Add(_clearButton);
            chartActions.Controls.Add(_exportButton);
            chartActions.Controls.Add(_chartStatusLabel);
            chartActions.Dock = DockStyle.Fill;
            chartActions.FlowDirection = FlowDirection.RightToLeft;
            chartActions.Location = new Point(580, 0);
            chartActions.Margin = new Padding(0);
            chartActions.Name = "chartActions";
            chartActions.Size = new Size(464, 38);
            chartActions.TabIndex = 1;
            chartActions.WrapContents = false;
            //
            // _clearButton
            //
            _clearButton.BackColor = Color.FromArgb(248, 250, 252);
            _clearButton.Cursor = Cursors.Hand;
            _clearButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            _clearButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
            _clearButton.FlatStyle = FlatStyle.Flat;
            _clearButton.Font = new Font("Microsoft YaHei UI", 8.5F, FontStyle.Bold);
            _clearButton.ForeColor = Color.FromArgb(15, 23, 42);
            _clearButton.Location = new Point(360, 0);
            _clearButton.Margin = new Padding(0, 0, 10, 0);
            _clearButton.Name = "_clearButton";
            _clearButton.Size = new Size(94, 38);
            _clearButton.TabIndex = 7;
            _clearButton.Text = "清空预览";
            _clearButton.UseVisualStyleBackColor = false;
            _clearButton.Click += ClearButton_Click;
            //
            // _exportButton
            //
            _exportButton.BackColor = Color.FromArgb(248, 250, 252);
            _exportButton.Cursor = Cursors.Hand;
            _exportButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            _exportButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
            _exportButton.FlatStyle = FlatStyle.Flat;
            _exportButton.Font = new Font("Microsoft YaHei UI", 8.5F, FontStyle.Bold);
            _exportButton.ForeColor = Color.FromArgb(15, 23, 42);
            _exportButton.Location = new Point(256, 0);
            _exportButton.Margin = new Padding(0, 0, 10, 0);
            _exportButton.Name = "_exportButton";
            _exportButton.Size = new Size(94, 38);
            _exportButton.TabIndex = 6;
            _exportButton.Text = "导出 CSV";
            _exportButton.UseVisualStyleBackColor = false;
            _exportButton.Click += ExportButton_Click;
            //
            // _chartStatusLabel
            //
            _chartStatusLabel.AutoEllipsis = true;
            _chartStatusLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            _chartStatusLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _chartStatusLabel.Location = new Point(3, 0);
            _chartStatusLabel.Name = "_chartStatusLabel";
            _chartStatusLabel.Size = new Size(250, 38);
            _chartStatusLabel.TabIndex = 8;
            _chartStatusLabel.Text = "显示最近 1,000 个原始采样点";
            _chartStatusLabel.TextAlign = ContentAlignment.MiddleRight;
            //
            // channelTabs
            //
            channelTabs.Controls.Add(channelOneTab);
            channelTabs.Controls.Add(channel2Tab);
            channelTabs.Controls.Add(channel3Tab);
            channelTabs.Controls.Add(channel4Tab);
            channelTabs.Controls.Add(channel5Tab);
            channelTabs.Controls.Add(channel6Tab);
            channelTabs.Dock = DockStyle.Fill;
            channelTabs.ItemSize = new Size(60, 26);
            channelTabs.Location = new Point(3, 41);
            channelTabs.Name = "channelTabs";
            channelTabs.SelectedIndex = 0;
            channelTabs.Size = new Size(1038, 180);
            channelTabs.TabIndex = 1;
            //
            // channelOneTab
            //
            channelOneTab.Controls.Add(_waveformPreview);
            channelOneTab.Location = new Point(4, 30);
            channelOneTab.Name = "channelOneTab";
            channelOneTab.Size = new Size(1030, 146);
            channelOneTab.TabIndex = 0;
            channelOneTab.Text = "CH1";
            //
            // _waveformPreview
            //
            _waveformPreview.BackColor = Color.White;
            _waveformPreview.Dock = DockStyle.Fill;
            _waveformPreview.ForeColor = Color.FromArgb(29, 78, 216);
            _waveformPreview.Location = new Point(0, 0);
            _waveformPreview.Margin = new Padding(0);
            _waveformPreview.MinimumSize = new Size(300, 150);
            _waveformPreview.Name = "_waveformPreview";
            _waveformPreview.Size = new Size(1030, 150);
            _waveformPreview.TabIndex = 0;
            _waveformPreview.UnitText = "count";
            //
            // channel2Tab
            //
            channel2Tab.Controls.Add(channel2Notice);
            channel2Tab.Location = new Point(4, 30);
            channel2Tab.Name = "channel2Tab";
            channel2Tab.Size = new Size(1030, 146);
            channel2Tab.TabIndex = 1;
            channel2Tab.Text = "CH2";
            //
            // channel2Notice
            //
            channel2Notice.Dock = DockStyle.Fill;
            channel2Notice.Font = new Font("Microsoft YaHei UI", 11F);
            channel2Notice.ForeColor = Color.FromArgb(100, 116, 139);
            channel2Notice.Location = new Point(0, 0);
            channel2Notice.Name = "channel2Notice";
            channel2Notice.Size = new Size(1030, 146);
            channel2Notice.TabIndex = 0;
            channel2Notice.Text = "CH2 的设备帧协议待确认，暂无独立波形。";
            channel2Notice.TextAlign = ContentAlignment.MiddleCenter;
            //
            // channel3Tab
            //
            channel3Tab.Controls.Add(channel3Notice);
            channel3Tab.Location = new Point(4, 30);
            channel3Tab.Name = "channel3Tab";
            channel3Tab.Size = new Size(1030, 146);
            channel3Tab.TabIndex = 2;
            channel3Tab.Text = "CH3";
            //
            // channel3Notice
            //
            channel3Notice.Dock = DockStyle.Fill;
            channel3Notice.Font = new Font("Microsoft YaHei UI", 11F);
            channel3Notice.ForeColor = Color.FromArgb(100, 116, 139);
            channel3Notice.Location = new Point(0, 0);
            channel3Notice.Name = "channel3Notice";
            channel3Notice.Size = new Size(1030, 146);
            channel3Notice.TabIndex = 0;
            channel3Notice.Text = "CH3 的设备帧协议待确认，暂无独立波形。";
            channel3Notice.TextAlign = ContentAlignment.MiddleCenter;
            //
            // channel4Tab
            //
            channel4Tab.Controls.Add(channel4Notice);
            channel4Tab.Location = new Point(4, 30);
            channel4Tab.Name = "channel4Tab";
            channel4Tab.Size = new Size(1030, 146);
            channel4Tab.TabIndex = 3;
            channel4Tab.Text = "CH4";
            //
            // channel4Notice
            //
            channel4Notice.Dock = DockStyle.Fill;
            channel4Notice.Font = new Font("Microsoft YaHei UI", 11F);
            channel4Notice.ForeColor = Color.FromArgb(100, 116, 139);
            channel4Notice.Location = new Point(0, 0);
            channel4Notice.Name = "channel4Notice";
            channel4Notice.Size = new Size(1030, 146);
            channel4Notice.TabIndex = 0;
            channel4Notice.Text = "CH4 的设备帧协议待确认，暂无独立波形。";
            channel4Notice.TextAlign = ContentAlignment.MiddleCenter;
            //
            // channel5Tab
            //
            channel5Tab.Controls.Add(channel5Notice);
            channel5Tab.Location = new Point(4, 30);
            channel5Tab.Name = "channel5Tab";
            channel5Tab.Size = new Size(1030, 146);
            channel5Tab.TabIndex = 4;
            channel5Tab.Text = "CH5";
            //
            // channel5Notice
            //
            channel5Notice.Dock = DockStyle.Fill;
            channel5Notice.Font = new Font("Microsoft YaHei UI", 11F);
            channel5Notice.ForeColor = Color.FromArgb(100, 116, 139);
            channel5Notice.Location = new Point(0, 0);
            channel5Notice.Name = "channel5Notice";
            channel5Notice.Size = new Size(1030, 146);
            channel5Notice.TabIndex = 0;
            channel5Notice.Text = "CH5 的设备帧协议待确认，暂无独立波形。";
            channel5Notice.TextAlign = ContentAlignment.MiddleCenter;
            //
            // channel6Tab
            //
            channel6Tab.Controls.Add(channel6Notice);
            channel6Tab.Location = new Point(4, 30);
            channel6Tab.Name = "channel6Tab";
            channel6Tab.Size = new Size(1030, 146);
            channel6Tab.TabIndex = 5;
            channel6Tab.Text = "CH6";
            //
            // channel6Notice
            //
            channel6Notice.Dock = DockStyle.Fill;
            channel6Notice.Font = new Font("Microsoft YaHei UI", 11F);
            channel6Notice.ForeColor = Color.FromArgb(100, 116, 139);
            channel6Notice.Location = new Point(0, 0);
            channel6Notice.Name = "channel6Notice";
            channel6Notice.Size = new Size(1030, 146);
            channel6Notice.TabIndex = 0;
            channel6Notice.Text = "CH6 的设备帧协议待确认，暂无独立波形。";
            channel6Notice.TextAlign = ContentAlignment.MiddleCenter;
            //
            // Wave_Height_Meter
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            AutoScrollMinSize = new Size(860, 600);
            BackColor = Color.FromArgb(241, 245, 249);
            Controls.Add(pagePanel);
            Font = new Font("Microsoft YaHei UI", 9F);
            ForeColor = Color.FromArgb(15, 23, 42);
            Name = "Wave_Height_Meter";
            Size = new Size(1104, 606);
            pagePanel.ResumeLayout(false);
            rootLayout.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            connectionCard.ResumeLayout(false);
            connectionLayout.ResumeLayout(false);
            connectionSettings.ResumeLayout(false);
            ipField.ResumeLayout(false);
            ipField.PerformLayout();
            portField.ResumeLayout(false);
            portField.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_portInput).EndInit();
            sampleRateField.ResumeLayout(false);
            sampleRateField.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_sampleRateInput).EndInit();
            sampleLimitField.ResumeLayout(false);
            sampleLimitField.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_sampleLimitInput).EndInit();
            connectionStatusPanel.ResumeLayout(false);
            connectionStatusPanel.PerformLayout();
            channelStatusPanel.ResumeLayout(false);
            readoutLayout.ResumeLayout(false);
            latestValueCard.ResumeLayout(false);
            latestValueCard.PerformLayout();
            sampleCountCard.ResumeLayout(false);
            sampleCountCard.PerformLayout();
            sampleRateCard.ResumeLayout(false);
            sampleRateCard.PerformLayout();
            chartCard.ResumeLayout(false);
            chartLayout.ResumeLayout(false);
            chartHeader.ResumeLayout(false);
            chartHeader.PerformLayout();
            chartActions.ResumeLayout(false);
            channelTabs.ResumeLayout(false);
            channelOneTab.ResumeLayout(false);
            channel2Tab.ResumeLayout(false);
            channel3Tab.ResumeLayout(false);
            channel4Tab.ResumeLayout(false);
            channel5Tab.ResumeLayout(false);
            channel6Tab.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TableLayoutPanel rootLayout;
        private Panel headerPanel;
        private Label titleLabel;
        private Label subtitleLabel;
        private Panel connectionCard;
        private TableLayoutPanel connectionLayout;
        private FlowLayoutPanel connectionSettings;
        private Panel ipField;
        private Label ipCaption;
        private TextBox _ipAddressInput;
        private Panel portField;
        private Label portCaption;
        private NumericUpDown _portInput;
        private Panel sampleRateField;
        private Label sampleRateCaption;
        private NumericUpDown _sampleRateInput;
        private Panel sampleLimitField;
        private Label sampleLimitCaption;
        private NumericUpDown _sampleLimitInput;
        private Button _connectButton;
        private Button _disconnectButton;
        private Button _saveSettingsButton;
        private Panel connectionStatusPanel;
        private Label connectionStatusCaption;
        private Label _connectionStatusLabel;
        private FlowLayoutPanel channelStatusPanel;
        private CheckBox channel1Selector;
        private CheckBox channel2Selector;
        private CheckBox channel3Selector;
        private CheckBox channel4Selector;
        private CheckBox channel5Selector;
        private CheckBox channel6Selector;
        private TableLayoutPanel readoutLayout;
        private Panel latestValueCard;
        private Label latestValueCaption;
        private Label _latestValueLabel;
        private Label latestValueDetail;
        private Panel sampleCountCard;
        private Label sampleCountCaption;
        private Label _sampleCountLabel;
        private Label sampleCountDetail;
        private Panel sampleRateCard;
        private Label sampleRateCardCaption;
        private Label _sampleRateLabel;
        private Label sampleRateDetail;
        private Panel chartCard;
        private TableLayoutPanel chartLayout;
        private TableLayoutPanel chartHeader;
        private Label chartTitleLabel;
        private FlowLayoutPanel chartActions;
        private Button _clearButton;
        private Button _exportButton;
        private Label _chartStatusLabel;
        private WaveformPreviewControl _waveformPreview;
        private TabControl channelTabs;
        private TabPage channelOneTab;
        private TabPage channel2Tab;
        private Label channel2Notice;
        private TabPage channel3Tab;
        private Label channel3Notice;
        private TabPage channel4Tab;
        private Label channel4Notice;
        private TabPage channel5Tab;
        private Label channel5Notice;
        private TabPage channel6Tab;
        private Label channel6Notice;
    }
}
