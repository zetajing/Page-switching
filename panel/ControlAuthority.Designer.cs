namespace Page_switching
{
    partial class ControlAuthority
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // 使用标准 WinForms Designer 结构：明确控件类型及 this 成员访问，便于设计器读取。
        private void InitializeComponent()
        {
            pagePanel = new Panel();
            rootLayout = new TableLayoutPanel();
            headerPanel = new Panel();
            subtitleLabel = new AntdUI.Label();
            titleLabel = new AntdUI.Label();
            statusGroup = new AntdUI.Panel();
            statusLayout = new TableLayoutPanel();
            connectionCaptionLabel = new AntdUI.Label();
            _connectionValue = new AntdUI.Label();
            stationCaptionLabel = new AntdUI.Label();
            _stationValue = new AntdUI.Label();
            ownerCaptionLabel = new AntdUI.Label();
            _ownerValue = new AntdUI.Label();
            stateCaptionLabel = new AntdUI.Label();
            _stateValue = new AntdUI.Label();
            safetyCaptionLabel = new AntdUI.Label();
            _safetyValue = new AntdUI.Label();
            watchdogCaptionLabel = new AntdUI.Label();
            _watchdogValue = new AntdUI.Label();
            statusGroupCaption = new AntdUI.Label();
            actionGroup = new AntdUI.Panel();
            actionGroupCaption = new AntdUI.Label();
            rulesGroup = new AntdUI.Panel();
            rulesLayout = new TableLayoutPanel();
            priorityLabel = new AntdUI.Label();
            centralCaptionLabel = new AntdUI.Label();
            centralRuleLabel = new AntdUI.Label();
            localCaptionLabel = new AntdUI.Label();
            localRuleLabel = new AntdUI.Label();
            remoteCaptionLabel = new AntdUI.Label();
            remoteRuleLabel = new AntdUI.Label();
            plcResponsibilityLabel = new AntdUI.Label();
            rulesGroupCaption = new AntdUI.Label();
            actionButtons = new FlowLayoutPanel();
            refreshButton = new AntdUI.Button();
            _releaseButton = new AntdUI.Button();
            _requestButton = new AntdUI.Button();
            actionLayout = new TableLayoutPanel();
            pagePanel.SuspendLayout();
            rootLayout.SuspendLayout();
            headerPanel.SuspendLayout();
            statusGroup.SuspendLayout();
            statusLayout.SuspendLayout();
            actionGroup.SuspendLayout();
            rulesGroup.SuspendLayout();
            rulesLayout.SuspendLayout();
            actionButtons.SuspendLayout();
            actionLayout.SuspendLayout();
            SuspendLayout();
            // 
            // pagePanel
            // 
            pagePanel.Controls.Add(rootLayout);
            pagePanel.Dock = DockStyle.Fill;
            pagePanel.Location = new Point(0, 0);
            pagePanel.Margin = new Padding(0);
            pagePanel.MinimumSize = new Size(1100, 850);
            pagePanel.Name = "pagePanel";
            pagePanel.Size = new Size(1380, 925);
            pagePanel.TabIndex = 0;
            // 
            // rootLayout
            // 
            rootLayout.BackColor = Color.FromArgb(241, 245, 249);
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(headerPanel, 0, 0);
            rootLayout.Controls.Add(statusGroup, 0, 1);
            rootLayout.Controls.Add(actionGroup, 0, 2);
            rootLayout.Controls.Add(rulesGroup, 0, 3);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Margin = new Padding(0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(20);
            rootLayout.RowCount = 4;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 190F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 180F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.Size = new Size(1380, 925);
            rootLayout.TabIndex = 0;
            // 
            // headerPanel
            // 
            headerPanel.Controls.Add(subtitleLabel);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Location = new Point(20, 20);
            headerPanel.Margin = new Padding(0);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(1340, 70);
            headerPanel.TabIndex = 0;
            // 
            // subtitleLabel
            // 
            subtitleLabel.Dock = DockStyle.Fill;
            subtitleLabel.ForeColor = Color.FromArgb(71, 85, 105);
            subtitleLabel.Location = new Point(0, 42);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(1340, 28);
            subtitleLabel.TabIndex = 0;
            subtitleLabel.Text = "控制权由 PLC 统一管理 · 上位机提交申请、释放请求并显示授权反馈";
            // 
            // titleLabel
            // 
            titleLabel.Dock = DockStyle.Top;
            titleLabel.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            titleLabel.Location = new Point(0, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(1340, 42);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "控制权申请";
            // 
            // statusGroup
            // 
            statusGroup.BackColor = Color.White;
            statusGroup.BorderColor = Color.FromArgb(203, 213, 225);
            statusGroup.BorderWidth = 1F;
            statusGroup.Controls.Add(statusLayout);
            statusGroup.Controls.Add(statusGroupCaption);
            statusGroup.Dock = DockStyle.Fill;
            statusGroup.Font = new Font("Microsoft YaHei UI", 9.5F, FontStyle.Bold);
            statusGroup.Location = new Point(20, 90);
            statusGroup.Margin = new Padding(0, 0, 0, 12);
            statusGroup.Name = "statusGroup";
            statusGroup.Padding = new Padding(20, 32, 20, 15);
            statusGroup.Size = new Size(1340, 178);
            statusGroup.TabIndex = 1;
            statusGroup.TabStop = false;
            // 
            // statusLayout
            // 
            statusLayout.ColumnCount = 4;
            statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            statusLayout.Controls.Add(connectionCaptionLabel, 0, 0);
            statusLayout.Controls.Add(_connectionValue, 1, 0);
            statusLayout.Controls.Add(stationCaptionLabel, 2, 0);
            statusLayout.Controls.Add(_stationValue, 3, 0);
            statusLayout.Controls.Add(ownerCaptionLabel, 0, 1);
            statusLayout.Controls.Add(_ownerValue, 1, 1);
            statusLayout.Controls.Add(stateCaptionLabel, 2, 1);
            statusLayout.Controls.Add(_stateValue, 3, 1);
            statusLayout.Controls.Add(safetyCaptionLabel, 0, 2);
            statusLayout.Controls.Add(_safetyValue, 1, 2);
            statusLayout.Controls.Add(watchdogCaptionLabel, 2, 2);
            statusLayout.Controls.Add(_watchdogValue, 3, 2);
            statusLayout.Dock = DockStyle.Fill;
            statusLayout.Font = new Font("Microsoft YaHei UI", 9F);
            statusLayout.Location = new Point(22, 34);
            statusLayout.Margin = new Padding(0);
            statusLayout.Name = "statusLayout";
            statusLayout.RowCount = 3;
            statusLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
            statusLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
            statusLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33334F));
            statusLayout.Size = new Size(1296, 127);
            statusLayout.TabIndex = 0;
            // 
            // connectionCaptionLabel
            // 
            connectionCaptionLabel.Dock = DockStyle.Fill;
            connectionCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            connectionCaptionLabel.Location = new Point(3, 3);
            connectionCaptionLabel.Name = "connectionCaptionLabel";
            connectionCaptionLabel.Size = new Size(124, 36);
            connectionCaptionLabel.TabIndex = 0;
            connectionCaptionLabel.Text = "ADS 连接";
            // 
            // _connectionValue
            // 
            _connectionValue.Dock = DockStyle.Fill;
            _connectionValue.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold);
            _connectionValue.ForeColor = Color.FromArgb(154, 52, 18);
            _connectionValue.Location = new Point(133, 3);
            _connectionValue.Name = "_connectionValue";
            _connectionValue.Size = new Size(512, 36);
            _connectionValue.TabIndex = 1;
            _connectionValue.Text = "未连接";
            // 
            // stationCaptionLabel
            // 
            stationCaptionLabel.Dock = DockStyle.Fill;
            stationCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            stationCaptionLabel.Location = new Point(651, 3);
            stationCaptionLabel.Name = "stationCaptionLabel";
            stationCaptionLabel.Size = new Size(124, 36);
            stationCaptionLabel.TabIndex = 2;
            stationCaptionLabel.Text = "本站点";
            // 
            // _stationValue
            // 
            _stationValue.Dock = DockStyle.Fill;
            _stationValue.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold);
            _stationValue.Location = new Point(781, 3);
            _stationValue.Name = "_stationValue";
            _stationValue.Size = new Size(512, 36);
            _stationValue.TabIndex = 3;
            _stationValue.Text = "未配置本站身份";
            // 
            // ownerCaptionLabel
            // 
            ownerCaptionLabel.Dock = DockStyle.Fill;
            ownerCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            ownerCaptionLabel.Location = new Point(3, 45);
            ownerCaptionLabel.Name = "ownerCaptionLabel";
            ownerCaptionLabel.Size = new Size(124, 36);
            ownerCaptionLabel.TabIndex = 4;
            ownerCaptionLabel.Text = "控制端反馈";
            // 
            // _ownerValue
            // 
            _ownerValue.Dock = DockStyle.Fill;
            _ownerValue.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold);
            _ownerValue.Location = new Point(133, 45);
            _ownerValue.Name = "_ownerValue";
            _ownerValue.Size = new Size(512, 36);
            _ownerValue.TabIndex = 5;
            _ownerValue.Text = "等待反馈";
            // 
            // stateCaptionLabel
            // 
            stateCaptionLabel.Dock = DockStyle.Fill;
            stateCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            stateCaptionLabel.Location = new Point(651, 45);
            stateCaptionLabel.Name = "stateCaptionLabel";
            stateCaptionLabel.Size = new Size(124, 36);
            stateCaptionLabel.TabIndex = 6;
            stateCaptionLabel.Text = "运行模式";
            // 
            // _stateValue
            // 
            _stateValue.Dock = DockStyle.Fill;
            _stateValue.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold);
            _stateValue.Location = new Point(781, 45);
            _stateValue.Name = "_stateValue";
            _stateValue.Size = new Size(512, 36);
            _stateValue.TabIndex = 7;
            _stateValue.Text = "等待反馈";
            // 
            // safetyCaptionLabel
            // 
            safetyCaptionLabel.Dock = DockStyle.Fill;
            safetyCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            safetyCaptionLabel.Location = new Point(3, 87);
            safetyCaptionLabel.Name = "safetyCaptionLabel";
            safetyCaptionLabel.Size = new Size(124, 37);
            safetyCaptionLabel.TabIndex = 8;
            safetyCaptionLabel.Text = "反馈质量";
            // 
            // _safetyValue
            // 
            _safetyValue.Dock = DockStyle.Fill;
            _safetyValue.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold);
            _safetyValue.ForeColor = Color.FromArgb(154, 52, 18);
            _safetyValue.Location = new Point(133, 87);
            _safetyValue.Name = "_safetyValue";
            _safetyValue.Size = new Size(512, 37);
            _safetyValue.TabIndex = 9;
            _safetyValue.Text = "监控未连接";
            // 
            // watchdogCaptionLabel
            // 
            watchdogCaptionLabel.Dock = DockStyle.Fill;
            watchdogCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            watchdogCaptionLabel.Location = new Point(651, 87);
            watchdogCaptionLabel.Name = "watchdogCaptionLabel";
            watchdogCaptionLabel.Size = new Size(124, 37);
            watchdogCaptionLabel.TabIndex = 10;
            watchdogCaptionLabel.Text = "PLC 心跳";
            // 
            // _watchdogValue
            // 
            _watchdogValue.Dock = DockStyle.Fill;
            _watchdogValue.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold);
            _watchdogValue.Location = new Point(781, 87);
            _watchdogValue.Name = "_watchdogValue";
            _watchdogValue.Size = new Size(512, 37);
            _watchdogValue.TabIndex = 11;
            _watchdogValue.Text = "等待反馈";
            // 
            // statusGroupCaption
            // 
            statusGroupCaption.AutoSizeMode = AntdUI.TAutoSize.Auto;
            statusGroupCaption.ForeColor = Color.FromArgb(15, 23, 42);
            statusGroupCaption.Location = new Point(10, 2);
            statusGroupCaption.Name = "statusGroupCaption";
            statusGroupCaption.Size = new Size(80, 21);
            statusGroupCaption.TabIndex = 1;
            statusGroupCaption.Text = "控制权状态";
            // 
            // actionGroup
            // 
            actionGroup.BackColor = Color.White;
            actionGroup.BorderColor = Color.FromArgb(203, 213, 225);
            actionGroup.BorderWidth = 1F;
            actionGroup.Controls.Add(actionLayout);
            actionGroup.Controls.Add(actionGroupCaption);
            actionGroup.Dock = DockStyle.Fill;
            actionGroup.Font = new Font("Microsoft YaHei UI", 9.5F, FontStyle.Bold);
            actionGroup.Location = new Point(20, 280);
            actionGroup.Margin = new Padding(0, 0, 0, 12);
            actionGroup.Name = "actionGroup";
            actionGroup.Padding = new Padding(20, 32, 20, 15);
            actionGroup.Size = new Size(1340, 148);
            actionGroup.TabIndex = 2;
            actionGroup.TabStop = false;
            // 
            // actionGroupCaption
            // 
            actionGroupCaption.AutoSizeMode = AntdUI.TAutoSize.Auto;
            actionGroupCaption.ForeColor = Color.FromArgb(15, 23, 42);
            actionGroupCaption.Location = new Point(10, 2);
            actionGroupCaption.Name = "actionGroupCaption";
            actionGroupCaption.Size = new Size(80, 21);
            actionGroupCaption.TabIndex = 1;
            actionGroupCaption.Text = "控制权操作";
            // 
            // rulesGroup
            // 
            rulesGroup.BackColor = Color.White;
            rulesGroup.BorderColor = Color.FromArgb(203, 213, 225);
            rulesGroup.BorderWidth = 1F;
            rulesGroup.Controls.Add(rulesLayout);
            rulesGroup.Controls.Add(rulesGroupCaption);
            rulesGroup.Dock = DockStyle.Fill;
            rulesGroup.Font = new Font("Microsoft YaHei UI", 9.5F, FontStyle.Bold);
            rulesGroup.Location = new Point(20, 440);
            rulesGroup.Margin = new Padding(0);
            rulesGroup.Name = "rulesGroup";
            rulesGroup.Padding = new Padding(20, 32, 20, 15);
            rulesGroup.Size = new Size(1340, 465);
            rulesGroup.TabIndex = 3;
            rulesGroup.TabStop = false;
            // 
            // rulesLayout
            // 
            rulesLayout.ColumnCount = 2;
            rulesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            rulesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rulesLayout.Controls.Add(priorityLabel, 0, 0);
            rulesLayout.Controls.Add(centralCaptionLabel, 0, 1);
            rulesLayout.Controls.Add(centralRuleLabel, 1, 1);
            rulesLayout.Controls.Add(localCaptionLabel, 0, 2);
            rulesLayout.Controls.Add(localRuleLabel, 1, 2);
            rulesLayout.Controls.Add(remoteCaptionLabel, 0, 3);
            rulesLayout.Controls.Add(remoteRuleLabel, 1, 3);
            rulesLayout.Controls.Add(plcResponsibilityLabel, 0, 4);
            rulesLayout.Dock = DockStyle.Fill;
            rulesLayout.Font = new Font("Microsoft YaHei UI", 9F);
            rulesLayout.Location = new Point(22, 34);
            rulesLayout.Margin = new Padding(0);
            rulesLayout.Name = "rulesLayout";
            rulesLayout.RowCount = 5;
            rulesLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            rulesLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
            rulesLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
            rulesLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33334F));
            rulesLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rulesLayout.Size = new Size(1296, 414);
            rulesLayout.TabIndex = 0;
            // 
            // priorityLabel
            // 
            rulesLayout.SetColumnSpan(priorityLabel, 2);
            priorityLabel.Dock = DockStyle.Fill;
            priorityLabel.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Bold);
            priorityLabel.ForeColor = Color.FromArgb(29, 78, 216);
            priorityLabel.Location = new Point(3, 3);
            priorityLabel.Name = "priorityLabel";
            priorityLabel.Size = new Size(1290, 42);
            priorityLabel.TabIndex = 0;
            priorityLabel.Text = "造波控制室操作 ＞ 拖车本地操作 ＞ 远程操作室操作";
            // 
            // centralCaptionLabel
            // 
            centralCaptionLabel.Dock = DockStyle.Fill;
            centralCaptionLabel.Font = new Font("Microsoft YaHei UI", 9.5F, FontStyle.Bold);
            centralCaptionLabel.Location = new Point(3, 51);
            centralCaptionLabel.Name = "centralCaptionLabel";
            centralCaptionLabel.Size = new Size(164, 102);
            centralCaptionLabel.TabIndex = 1;
            centralCaptionLabel.Text = "最高 · 造波控制室";
            // 
            // centralRuleLabel
            // 
            centralRuleLabel.Dock = DockStyle.Fill;
            centralRuleLabel.Location = new Point(173, 51);
            centralRuleLabel.Name = "centralRuleLabel";
            centralRuleLabel.Size = new Size(1120, 102);
            centralRuleLabel.TabIndex = 2;
            centralRuleLabel.Text = "空闲时可申请，优先级最高；运行中不交接。";
            // 
            // localCaptionLabel
            // 
            localCaptionLabel.Dock = DockStyle.Fill;
            localCaptionLabel.Font = new Font("Microsoft YaHei UI", 9.5F, FontStyle.Bold);
            localCaptionLabel.Location = new Point(3, 159);
            localCaptionLabel.Name = "localCaptionLabel";
            localCaptionLabel.Size = new Size(164, 102);
            localCaptionLabel.TabIndex = 3;
            localCaptionLabel.Text = "中间 · 拖车本地";
            // 
            // localRuleLabel
            // 
            localRuleLabel.Dock = DockStyle.Fill;
            localRuleLabel.Location = new Point(173, 159);
            localRuleLabel.Name = "localRuleLabel";
            localRuleLabel.Size = new Size(1120, 102);
            localRuleLabel.TabIndex = 4;
            localRuleLabel.Text = "空闲时可申请，可由造波控制室接管；运行中不交接。";
            // 
            // remoteCaptionLabel
            // 
            remoteCaptionLabel.Dock = DockStyle.Fill;
            remoteCaptionLabel.Font = new Font("Microsoft YaHei UI", 9.5F, FontStyle.Bold);
            remoteCaptionLabel.Location = new Point(3, 267);
            remoteCaptionLabel.Name = "remoteCaptionLabel";
            remoteCaptionLabel.Size = new Size(164, 102);
            remoteCaptionLabel.TabIndex = 5;
            remoteCaptionLabel.Text = "最低 · 远程操作室";
            // 
            // remoteRuleLabel
            // 
            remoteRuleLabel.Dock = DockStyle.Fill;
            remoteRuleLabel.Location = new Point(173, 267);
            remoteRuleLabel.Name = "remoteRuleLabel";
            remoteRuleLabel.Size = new Size(1120, 102);
            remoteRuleLabel.TabIndex = 6;
            remoteRuleLabel.Text = "空闲时可申请，可由更高优先级站接管；运行中不交接。";
            // 
            // plcResponsibilityLabel
            // 
            rulesLayout.SetColumnSpan(plcResponsibilityLabel, 2);
            plcResponsibilityLabel.Dock = DockStyle.Fill;
            plcResponsibilityLabel.ForeColor = Color.FromArgb(100, 116, 139);
            plcResponsibilityLabel.Location = new Point(3, 375);
            plcResponsibilityLabel.Name = "plcResponsibilityLabel";
            plcResponsibilityLabel.Size = new Size(1290, 36);
            plcResponsibilityLabel.TabIndex = 7;
            plcResponsibilityLabel.Text = "持有站空闲时可释放；失联退出后需重新申请。";
            // 
            // rulesGroupCaption
            // 
            rulesGroupCaption.AutoSizeMode = AntdUI.TAutoSize.Auto;
            rulesGroupCaption.ForeColor = Color.FromArgb(15, 23, 42);
            rulesGroupCaption.Location = new Point(10, 2);
            rulesGroupCaption.Name = "rulesGroupCaption";
            rulesGroupCaption.Size = new Size(190, 21);
            rulesGroupCaption.TabIndex = 1;
            rulesGroupCaption.Text = "三站控制优先级与授权规则";
            // 
            // actionButtons
            // 
            actionButtons.Controls.Add(_requestButton);
            actionButtons.Controls.Add(_releaseButton);
            actionButtons.Controls.Add(refreshButton);
            actionButtons.Dock = DockStyle.Fill;
            actionButtons.Location = new Point(0, 0);
            actionButtons.Margin = new Padding(0);
            actionButtons.Name = "actionButtons";
            actionButtons.Size = new Size(1296, 97);
            actionButtons.TabIndex = 0;
            actionButtons.WrapContents = false;
            // 
            // refreshButton
            // 
            refreshButton.BackActive = Color.FromArgb(30, 64, 175);
            refreshButton.BackColor = Color.FromArgb(29, 78, 216);
            refreshButton.BackHover = Color.FromArgb(30, 64, 175);
            refreshButton.Cursor = Cursors.Hand;
            refreshButton.DefaultBack = Color.FromArgb(29, 78, 216);
            refreshButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            refreshButton.ForeColor = Color.White;
            refreshButton.Location = new Point(444, 0);
            refreshButton.Margin = new Padding(0);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(150, 42);
            refreshButton.TabIndex = 2;
            refreshButton.Text = "刷新状态";
            refreshButton.Click += RefreshButton_Click;
            // 
            // _releaseButton
            // 
            _releaseButton.BackColor = Color.FromArgb(248, 250, 252);
            _releaseButton.BorderWidth = 1F;
            _releaseButton.DefaultBack = Color.FromArgb(248, 250, 252);
            _releaseButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
            _releaseButton.Enabled = false;
            _releaseButton.Font = new Font("Microsoft YaHei UI", 9F);
            _releaseButton.ForeColor = Color.FromArgb(100, 116, 139);
            _releaseButton.Location = new Point(222, 0);
            _releaseButton.Margin = new Padding(0, 0, 12, 0);
            _releaseButton.Name = "_releaseButton";
            _releaseButton.Size = new Size(210, 42);
            _releaseButton.TabIndex = 1;
            _releaseButton.Text = "释放控制权";
            _releaseButton.Click += ReleaseButton_Click;
            // 
            // _requestButton
            // 
            _requestButton.BackColor = Color.FromArgb(248, 250, 252);
            _requestButton.BorderWidth = 1F;
            _requestButton.DefaultBack = Color.FromArgb(248, 250, 252);
            _requestButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
            _requestButton.Dock = DockStyle.Fill;
            _requestButton.Enabled = false;
            _requestButton.Font = new Font("Microsoft YaHei UI", 9F);
            _requestButton.ForeColor = Color.FromArgb(100, 116, 139);
            _requestButton.Location = new Point(0, 0);
            _requestButton.Margin = new Padding(0, 0, 12, 0);
            _requestButton.Name = "_requestButton";
            _requestButton.Size = new Size(210, 42);
            _requestButton.TabIndex = 0;
            _requestButton.Text = "申请控制权";
            _requestButton.Click += RequestButton_Click;
            // 
            // actionLayout
            // 
            actionLayout.ColumnCount = 1;
            actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            actionLayout.Controls.Add(actionButtons, 0, 0);
            actionLayout.Dock = DockStyle.Fill;
            actionLayout.Location = new Point(22, 34);
            actionLayout.Margin = new Padding(0);
            actionLayout.Name = "actionLayout";
            actionLayout.RowCount = 1;
            actionLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            actionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            actionLayout.Size = new Size(1296, 97);
            actionLayout.TabIndex = 0;
            // 
            // ControlAuthority
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            BackColor = Color.FromArgb(241, 245, 249);
            Controls.Add(pagePanel);
            Font = new Font("Microsoft YaHei UI", 9F);
            ForeColor = Color.FromArgb(15, 23, 42);
            Margin = new Padding(4);
            Name = "ControlAuthority";
            Size = new Size(1380, 925);
            pagePanel.ResumeLayout(false);
            rootLayout.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            statusGroup.ResumeLayout(false);
            statusGroup.PerformLayout();
            statusLayout.ResumeLayout(false);
            actionGroup.ResumeLayout(false);
            actionGroup.PerformLayout();
            rulesGroup.ResumeLayout(false);
            rulesGroup.PerformLayout();
            rulesLayout.ResumeLayout(false);
            actionButtons.ResumeLayout(false);
            actionLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.ComponentModel.IContainer components = null;
        private Panel pagePanel;
        private TableLayoutPanel rootLayout;
        private Panel headerPanel;
        private AntdUI.Label titleLabel;
        private AntdUI.Label subtitleLabel;
        private AntdUI.Panel statusGroup;
        private AntdUI.Label statusGroupCaption;
        private TableLayoutPanel statusLayout;
        private AntdUI.Label connectionCaptionLabel;
        private AntdUI.Label stationCaptionLabel;
        private AntdUI.Label ownerCaptionLabel;
        private AntdUI.Label stateCaptionLabel;
        private AntdUI.Label safetyCaptionLabel;
        private AntdUI.Label watchdogCaptionLabel;
        private AntdUI.Label _connectionValue;
        private AntdUI.Label _stationValue;
        private AntdUI.Label _ownerValue;
        private AntdUI.Label _stateValue;
        private AntdUI.Label _safetyValue;
        private AntdUI.Label _watchdogValue;
        private AntdUI.Panel actionGroup;
        private AntdUI.Label actionGroupCaption;
        private AntdUI.Panel rulesGroup;
        private AntdUI.Label rulesGroupCaption;
        private TableLayoutPanel rulesLayout;
        private AntdUI.Label priorityLabel;
        private AntdUI.Label centralCaptionLabel;
        private AntdUI.Label centralRuleLabel;
        private AntdUI.Label localCaptionLabel;
        private AntdUI.Label localRuleLabel;
        private AntdUI.Label remoteCaptionLabel;
        private AntdUI.Label remoteRuleLabel;
        private AntdUI.Label plcResponsibilityLabel;
        private TableLayoutPanel actionLayout;
        private FlowLayoutPanel actionButtons;
        private AntdUI.Button _requestButton;
        private AntdUI.Button _releaseButton;
        private AntdUI.Button refreshButton;
    }
}
