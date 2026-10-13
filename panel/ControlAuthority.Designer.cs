namespace Page_switching;

public sealed partial class ControlAuthority
{
    private System.ComponentModel.IContainer components = null!;
    private TableLayoutPanel rootLayout = null!;
    private Label titleLabel = null!;
    private Label subtitleLabel = null!;
    private GroupBox statusGroup = null!;
    private TableLayoutPanel statusLayout = null!;
    private Label connectionCaptionLabel = null!;
    private Label stationCaptionLabel = null!;
    private Label ownerCaptionLabel = null!;
    private Label stateCaptionLabel = null!;
    private Label safetyCaptionLabel = null!;
    private Label watchdogCaptionLabel = null!;
    private Label _connectionValue = null!;
    private Label _stationValue = null!;
    private Label _ownerValue = null!;
    private Label _stateValue = null!;
    private Label _safetyValue = null!;
    private Label _watchdogValue = null!;
    private GroupBox actionGroup = null!;
    private TableLayoutPanel actionLayout = null!;
    private Button _requestButton = null!;
    private Button _releaseButton = null!;
    private Label _hintLabel = null!;
    private Button refreshButton = null!;
    private GroupBox rulesGroup = null!;
    private TableLayoutPanel rulesLayout = null!;
    private Label priorityLabel = null!;
    private Label centralRuleLabel = null!;
    private Label localRuleLabel = null!;
    private Label remoteRuleLabel = null!;

    // 释放 Designer 创建的控件资源。
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    // 创建控制权页面的标题、状态区域和操作区域。
    private void InitializeComponent()
    {
        rootLayout = new TableLayoutPanel();
        titleLabel = new Label();
        subtitleLabel = new Label();
        statusGroup = new GroupBox();
        statusLayout = new TableLayoutPanel();
        connectionCaptionLabel = new Label();
        _connectionValue = new Label();
        stationCaptionLabel = new Label();
        _stationValue = new Label();
        ownerCaptionLabel = new Label();
        _ownerValue = new Label();
        stateCaptionLabel = new Label();
        _stateValue = new Label();
        safetyCaptionLabel = new Label();
        _safetyValue = new Label();
        watchdogCaptionLabel = new Label();
        _watchdogValue = new Label();
        actionGroup = new GroupBox();
        actionLayout = new TableLayoutPanel();
        _requestButton = new Button();
        _releaseButton = new Button();
        _hintLabel = new Label();
        refreshButton = new Button();
        rulesGroup = new GroupBox();
        rulesLayout = new TableLayoutPanel();
        priorityLabel = new Label();
        centralRuleLabel = new Label();
        localRuleLabel = new Label();
        remoteRuleLabel = new Label();
        rootLayout.SuspendLayout();
        statusGroup.SuspendLayout();
        statusLayout.SuspendLayout();
        actionGroup.SuspendLayout();
        actionLayout.SuspendLayout();
        rulesGroup.SuspendLayout();
        rulesLayout.SuspendLayout();
        SuspendLayout();
        //
        // rootLayout
        //
        rootLayout.BackColor = Color.FromArgb(241, 245, 249);
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(titleLabel, 0, 0);
        rootLayout.Controls.Add(subtitleLabel, 0, 1);
        rootLayout.Controls.Add(statusGroup, 0, 2);
        rootLayout.Controls.Add(actionGroup, 0, 3);
        rootLayout.Controls.Add(rulesGroup, 0, 4);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Name = "rootLayout";
        rootLayout.AutoScroll = false;
        rootLayout.Padding = new Padding(16);
        rootLayout.RowCount = 5;

        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 180F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 170F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
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
        titleLabel.Size = new Size(1164, 44);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "控制权申请";
        titleLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // subtitleLabel
        //
        subtitleLabel.Dock = DockStyle.Fill;
        subtitleLabel.Font = new Font("Microsoft YaHei UI", 9F);
        subtitleLabel.ForeColor = Color.FromArgb(71, 85, 105);
        subtitleLabel.Location = new Point(23, 64);
        subtitleLabel.Name = "subtitleLabel";
        subtitleLabel.Size = new Size(1164, 38);
        subtitleLabel.TabIndex = 1;
        subtitleLabel.Text = "造波控制室 ＞ 拖车本地 ＞ 远程操作室；控制权切换以 PLC 实际授权反馈为准";
        subtitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // statusGroup
        //
        statusGroup.BackColor = Color.White;
        statusGroup.Controls.Add(statusLayout);
        statusGroup.Dock = DockStyle.Fill;
        statusGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        statusGroup.ForeColor = Color.FromArgb(15, 23, 42);
        statusGroup.Location = new Point(23, 105);
        statusGroup.Name = "statusGroup";
        statusGroup.Padding = new Padding(14, 18, 14, 10);
        statusGroup.Size = new Size(1164, 204);
        statusGroup.TabIndex = 2;
        statusGroup.TabStop = false;
        statusGroup.Text = "控制权状态";
        //
        // statusLayout
        //
        statusLayout.ColumnCount = 4;
        statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
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
        statusLayout.Location = new Point(14, 38);
        statusLayout.Name = "statusLayout";
        statusLayout.RowCount = 3;
        statusLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.333F));
        statusLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.333F));
        statusLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.334F));
        statusLayout.Size = new Size(1136, 156);
        statusLayout.TabIndex = 0;
        //
        // connectionCaptionLabel
        //
        connectionCaptionLabel.Location = new Point(3, 0);
        connectionCaptionLabel.Name = "connectionCaptionLabel";
        connectionCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        connectionCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
        connectionCaptionLabel.Font = new Font("Microsoft YaHei UI", 9F);
        connectionCaptionLabel.Dock = DockStyle.Fill;
        connectionCaptionLabel.Size = new Size(100, 23);
        connectionCaptionLabel.TabIndex = 0;
        connectionCaptionLabel.Text = "ADS 连接";
        //
        // _connectionValue
        //
        _connectionValue.Location = new Point(113, 0);
        _connectionValue.Name = "_connectionValue";
        _connectionValue.ForeColor = Color.FromArgb(185, 28, 28);
        _connectionValue.TextAlign = ContentAlignment.MiddleLeft;
        _connectionValue.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        _connectionValue.Dock = DockStyle.Fill;
        _connectionValue.Size = new Size(100, 23);
        _connectionValue.TabIndex = 1;
        _connectionValue.Text = "未连接";
        //
        // stationCaptionLabel
        //
        stationCaptionLabel.Location = new Point(571, 0);
        stationCaptionLabel.Name = "stationCaptionLabel";
        stationCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        stationCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
        stationCaptionLabel.Font = new Font("Microsoft YaHei UI", 9F);
        stationCaptionLabel.Dock = DockStyle.Fill;
        stationCaptionLabel.Size = new Size(100, 23);
        stationCaptionLabel.TabIndex = 2;
        stationCaptionLabel.Text = "本站点";
        //
        // _stationValue
        //
        _stationValue.Location = new Point(681, 0);
        _stationValue.Name = "_stationValue";
        _stationValue.TextAlign = ContentAlignment.MiddleLeft;
        _stationValue.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        _stationValue.Dock = DockStyle.Fill;
        _stationValue.Size = new Size(100, 23);
        _stationValue.TabIndex = 3;
        _stationValue.Text = "待确认本站所属站点";
        //
        // ownerCaptionLabel
        //
        ownerCaptionLabel.Location = new Point(3, 51);
        ownerCaptionLabel.Name = "ownerCaptionLabel";
        ownerCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        ownerCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
        ownerCaptionLabel.Font = new Font("Microsoft YaHei UI", 9F);
        ownerCaptionLabel.Dock = DockStyle.Fill;
        ownerCaptionLabel.Size = new Size(100, 23);
        ownerCaptionLabel.TabIndex = 4;
        ownerCaptionLabel.Text = "控制端反馈";
        //
        // _ownerValue
        //
        _ownerValue.Location = new Point(113, 51);
        _ownerValue.Name = "_ownerValue";
        _ownerValue.TextAlign = ContentAlignment.MiddleLeft;
        _ownerValue.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        _ownerValue.Dock = DockStyle.Fill;
        _ownerValue.Size = new Size(100, 23);
        _ownerValue.TabIndex = 5;
        _ownerValue.Text = "等待反馈";
        //
        // stateCaptionLabel
        //
        stateCaptionLabel.Location = new Point(571, 51);
        stateCaptionLabel.Name = "stateCaptionLabel";
        stateCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        stateCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
        stateCaptionLabel.Font = new Font("Microsoft YaHei UI", 9F);
        stateCaptionLabel.Dock = DockStyle.Fill;
        stateCaptionLabel.Size = new Size(100, 23);
        stateCaptionLabel.TabIndex = 6;
        stateCaptionLabel.Text = "运行模式";
        //
        // _stateValue
        //
        _stateValue.Location = new Point(681, 51);
        _stateValue.Name = "_stateValue";
        _stateValue.TextAlign = ContentAlignment.MiddleLeft;
        _stateValue.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        _stateValue.Dock = DockStyle.Fill;
        _stateValue.Size = new Size(100, 23);
        _stateValue.TabIndex = 7;
        _stateValue.Text = "等待反馈";
        //
        // safetyCaptionLabel
        //
        safetyCaptionLabel.Location = new Point(3, 102);
        safetyCaptionLabel.Name = "safetyCaptionLabel";
        safetyCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        safetyCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
        safetyCaptionLabel.Font = new Font("Microsoft YaHei UI", 9F);
        safetyCaptionLabel.Dock = DockStyle.Fill;
        safetyCaptionLabel.Size = new Size(100, 23);
        safetyCaptionLabel.TabIndex = 8;
        safetyCaptionLabel.Text = "反馈质量";
        //
        // _safetyValue
        //
        _safetyValue.Location = new Point(113, 102);
        _safetyValue.Name = "_safetyValue";
        _safetyValue.TextAlign = ContentAlignment.MiddleLeft;
        _safetyValue.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        _safetyValue.Dock = DockStyle.Fill;
        _safetyValue.Size = new Size(100, 23);
        _safetyValue.TabIndex = 9;
        _safetyValue.Text = "监控未连接";
        //
        // watchdogCaptionLabel
        //
        watchdogCaptionLabel.Location = new Point(571, 102);
        watchdogCaptionLabel.Name = "watchdogCaptionLabel";
        watchdogCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        watchdogCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
        watchdogCaptionLabel.Font = new Font("Microsoft YaHei UI", 9F);
        watchdogCaptionLabel.Dock = DockStyle.Fill;
        watchdogCaptionLabel.Size = new Size(100, 23);
        watchdogCaptionLabel.TabIndex = 10;
        watchdogCaptionLabel.Text = "PLC 心跳";
        //
        // _watchdogValue
        //
        _watchdogValue.Location = new Point(681, 102);
        _watchdogValue.Name = "_watchdogValue";
        _watchdogValue.TextAlign = ContentAlignment.MiddleLeft;
        _watchdogValue.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        _watchdogValue.Dock = DockStyle.Fill;
        _watchdogValue.Size = new Size(100, 23);
        _watchdogValue.TabIndex = 11;
        _watchdogValue.Text = "等待反馈";
        //
        // actionGroup
        //
        actionGroup.BackColor = Color.White;
        actionGroup.Controls.Add(actionLayout);
        actionGroup.Dock = DockStyle.Fill;
        actionGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        actionGroup.ForeColor = Color.FromArgb(15, 23, 42);
        actionGroup.Location = new Point(23, 315);
        actionGroup.Name = "actionGroup";
        actionGroup.Padding = new Padding(14, 18, 14, 12);
        actionGroup.Size = new Size(1164, 458);
        actionGroup.TabIndex = 3;
        actionGroup.TabStop = false;
        actionGroup.Text = "控制权操作";
        //
        // actionLayout
        //
        actionLayout.ColumnCount = 2;
        actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 210F));
        actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        actionLayout.Controls.Add(_requestButton, 0, 0);
        actionLayout.Controls.Add(_releaseButton, 0, 1);
        actionLayout.Controls.Add(_hintLabel, 1, 0);
        actionLayout.Controls.Add(refreshButton, 1, 2);
        actionLayout.Dock = DockStyle.Fill;
        actionLayout.Location = new Point(14, 38);
        actionLayout.Name = "actionLayout";
        actionLayout.RowCount = 3;
        actionLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        actionLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        actionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        actionLayout.Size = new Size(1136, 408);
        actionLayout.TabIndex = 0;
        //
        // _requestButton
        //
        _requestButton.Location = new Point(3, 3);
        _requestButton.Name = "_requestButton";
        _requestButton.UseVisualStyleBackColor = false;
        _requestButton.Font = new Font("Microsoft YaHei UI", 9F);
        _requestButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
        _requestButton.FlatAppearance.BorderSize = 1;
        _requestButton.FlatStyle = FlatStyle.Flat;
        _requestButton.ForeColor = Color.FromArgb(100, 116, 139);
        _requestButton.BackColor = Color.FromArgb(248, 250, 252);
        _requestButton.Enabled = false;
        _requestButton.Dock = DockStyle.Fill;
        _requestButton.Cursor = Cursors.Hand;
        _requestButton.Size = new Size(164, 46);
        _requestButton.TabIndex = 0;
        _requestButton.Text = "申请控制权（待接入）";
        //
        // _releaseButton
        //
        _releaseButton.Location = new Point(3, 55);
        _releaseButton.Name = "_releaseButton";
        _releaseButton.UseVisualStyleBackColor = false;
        _releaseButton.Font = new Font("Microsoft YaHei UI", 9F);
        _releaseButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
        _releaseButton.FlatAppearance.BorderSize = 1;
        _releaseButton.FlatStyle = FlatStyle.Flat;
        _releaseButton.ForeColor = Color.FromArgb(100, 116, 139);
        _releaseButton.BackColor = Color.FromArgb(248, 250, 252);
        _releaseButton.Enabled = false;
        _releaseButton.Dock = DockStyle.Fill;
        _releaseButton.Cursor = Cursors.Hand;
        _releaseButton.Size = new Size(164, 46);
        _releaseButton.TabIndex = 1;
        _releaseButton.Text = "释放控制权（待接入）";
        //
        // _hintLabel
        //
        _hintLabel.Dock = DockStyle.Fill;
        _hintLabel.Font = new Font("Microsoft YaHei UI", 9F);
        _hintLabel.ForeColor = Color.FromArgb(154, 52, 18);
        _hintLabel.Location = new Point(173, 0);
        _hintLabel.Name = "_hintLabel";
        actionLayout.SetRowSpan(_hintLabel, 2);
        _hintLabel.Size = new Size(960, 104);
        _hintLabel.TabIndex = 2;
        _hintLabel.Text = "申请、释放接口待接入。本站身份、三站编码和授权反馈尚未确认；现有控制端反馈沿用原定义。";
        _hintLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // refreshButton
        //
        refreshButton.Name = "refreshButton";
        refreshButton.Text = "刷新状态";
        refreshButton.Size = new Size(130, 30);
        refreshButton.BackColor = Color.White;
        refreshButton.FlatStyle = FlatStyle.Flat;
        refreshButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
        refreshButton.Click += RefreshButton_Click;
        //
        // rulesGroup
        //
        rulesGroup.Name = "rulesGroup";
        rulesGroup.Text = "三站控制优先级与授权规则";
        rulesGroup.Dock = DockStyle.Fill;
        rulesGroup.BackColor = Color.White;
        rulesGroup.Padding = new Padding(14, 18, 14, 10);
        rulesGroup.Controls.Add(rulesLayout);
        rulesLayout.Name = "rulesLayout";
        rulesLayout.Dock = DockStyle.Fill;
        rulesLayout.ColumnCount = 1;
        rulesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rulesLayout.RowCount = 4;
        rulesLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        rulesLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.333F));
        rulesLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.333F));
        rulesLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.334F));
        rulesLayout.Controls.Add(priorityLabel, 0, 0);
        rulesLayout.Controls.Add(centralRuleLabel, 0, 1);
        rulesLayout.Controls.Add(localRuleLabel, 0, 2);
        rulesLayout.Controls.Add(remoteRuleLabel, 0, 3);
        priorityLabel.Name = "priorityLabel";
        priorityLabel.Dock = DockStyle.Fill;
        priorityLabel.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Bold);
        priorityLabel.ForeColor = Color.FromArgb(29, 78, 216);
        priorityLabel.Text = "控制优先级：造波控制室操作 ＞ 拖车本地操作 ＞ 远程操作室操作";
        priorityLabel.TextAlign = ContentAlignment.MiddleLeft;
        centralRuleLabel.Name = "centralRuleLabel";
        centralRuleLabel.Dock = DockStyle.Fill;
        centralRuleLabel.Text = "最高优先级 · 造波控制室\r\n中心控制室处于主控模式时，仅中心控制室指令有效；自动锁定、屏蔽拖车本地和远程操作室全部操作权限。";
        centralRuleLabel.TextAlign = ContentAlignment.MiddleLeft;
        localRuleLabel.Name = "localRuleLabel";
        localRuleLabel.Dock = DockStyle.Fill;
        localRuleLabel.Text = "中间优先级 · 拖车本地\r\n中心控制室解除主控并开放本地授权后，本地操作方可生效，用于检修、调试及局部试验；此时远程操作仍锁定。";
        localRuleLabel.TextAlign = ContentAlignment.MiddleLeft;
        remoteRuleLabel.Name = "remoteRuleLabel";
        remoteRuleLabel.Dock = DockStyle.Fill;
        remoteRuleLabel.Text = "最低优先级 · 远程操作室\r\n中心控制室与拖车本地均解除主控，并开启远程授权模式后，远程方可操作；任意高优先级站点介入主控，远程权限立即失效并锁定。";
        remoteRuleLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // ControlAuthority
        //
        Font = new Font("Microsoft YaHei UI", 9F);
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        AutoScroll = true;
        AutoScrollMinSize = new Size(860, 780);
        BackColor = Color.FromArgb(241, 245, 249);
        ForeColor = Color.FromArgb(15, 23, 42);
        Controls.Add(rootLayout);
        Name = "ControlAuthority";
        Size = new Size(1104, 796);
        rootLayout.ResumeLayout(false);
        statusGroup.ResumeLayout(false);
        statusLayout.ResumeLayout(false);
        actionGroup.ResumeLayout(false);
        actionLayout.ResumeLayout(false);
        rulesGroup.ResumeLayout(false);
        rulesLayout.ResumeLayout(false);
        ResumeLayout(false);
    }

}
