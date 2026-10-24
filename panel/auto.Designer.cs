namespace Page_switching
{
    partial class Auto
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pagePanel;
        private TableLayoutPanel rootLayout;
        private Panel headerPanel;
        private AntdUI.Label titleLabel;
        private TableLayoutPanel cardsLayout;
        private AntdUI.Panel controlCard;
        private TableLayoutPanel controlCardLayout;
        private AntdUI.Label controlCaptionLabel;
        private AntdUI.Label controlValueLabel;
        private AntdUI.Label controlDetailLabel;
        private AntdUI.Panel modeCard;
        private TableLayoutPanel modeCardLayout;
        private AntdUI.Label modeCaptionLabel;
        private AntdUI.Label modeValueLabel;
        private AntdUI.Label modeDetailLabel;
        private AntdUI.Panel waveCard;
        private TableLayoutPanel waveCardLayout;
        private AntdUI.Label waveCaptionLabel;
        private AntdUI.Label _runStateLabel;
        private AntdUI.Label faultCodeLabel;
        private TableLayoutPanel monitorInfoLayout;
        private AntdUI.Label connectionLabel;
        private AntdUI.Label feedbackLabel;
        private AntdUI.Label heartbeatLabel;
        private TableLayoutPanel bodyLayout;
        private AntdUI.Panel progressGroup;
        private AntdUI.Label progressGroupCaption;
        private TableLayoutPanel waveLayout;
        private TableLayoutPanel waveButtonsLayout;
        private AntdUI.Button importWaveButton;
        private AntdUI.Button sendWaveButton;
        private AntdUI.Button startWaveButton;
        private AntdUI.Button stopWaveButton;
        private Label waveFileLabel;
        private Label waveStageLabel;
        private Label waveDetailLabel;
        private AxisMonitoringSummaryControl runtimeMonitoringSummary;
        private ProgressBar waveProgressBar;
        private TableLayoutPanel rightLayout;
        private AntdUI.Panel axisGroup;
        private AntdUI.Label axisGroupCaption;
        private global::Page_switching.AxisFeedbackGrid axisGrid;
        private AntdUI.Panel logGroup;
        private AntdUI.Label logGroupCaption;
        private TableLayoutPanel logLayout;
        private TableLayoutPanel messageHeader;
        private AntdUI.Label messageCountLabel;
        private AntdUI.Label messageSummaryLabel;
        private ToolTip messageToolTip;
        private AntdUI.Input _logList;
        private AntdUI.Button clearLogButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            axisGrid = new AxisFeedbackGrid();
            pagePanel = new Panel();
            rootLayout = new TableLayoutPanel();
            headerPanel = new Panel();
            titleLabel = new AntdUI.Label();
            cardsLayout = new TableLayoutPanel();
            controlCard = new AntdUI.Panel();
            controlCardLayout = new TableLayoutPanel();
            controlCaptionLabel = new AntdUI.Label();
            controlValueLabel = new AntdUI.Label();
            controlDetailLabel = new AntdUI.Label();
            modeCard = new AntdUI.Panel();
            modeCardLayout = new TableLayoutPanel();
            modeCaptionLabel = new AntdUI.Label();
            modeValueLabel = new AntdUI.Label();
            modeDetailLabel = new AntdUI.Label();
            waveCard = new AntdUI.Panel();
            waveCardLayout = new TableLayoutPanel();
            waveCaptionLabel = new AntdUI.Label();
            _runStateLabel = new AntdUI.Label();
            faultCodeLabel = new AntdUI.Label();
            monitorInfoLayout = new TableLayoutPanel();
            connectionLabel = new AntdUI.Label();
            feedbackLabel = new AntdUI.Label();
            heartbeatLabel = new AntdUI.Label();
            bodyLayout = new TableLayoutPanel();
            progressGroup = new AntdUI.Panel();
            waveLayout = new TableLayoutPanel();
            waveButtonsLayout = new TableLayoutPanel();
            importWaveButton = new AntdUI.Button();
            sendWaveButton = new AntdUI.Button();
            startWaveButton = new AntdUI.Button();
            stopWaveButton = new AntdUI.Button();
            waveFileLabel = new Label();
            waveStageLabel = new Label();
            waveProgressBar = new ProgressBar();
            waveDetailLabel = new Label();
            runtimeMonitoringSummary = new AxisMonitoringSummaryControl();
            runtimeMonitoringSummary.Dock = DockStyle.Fill;
            progressGroupCaption = new AntdUI.Label();
            rightLayout = new TableLayoutPanel();
            axisGroup = new AntdUI.Panel();
            axisGroupCaption = new AntdUI.Label();
            logGroup = new AntdUI.Panel();
            logLayout = new TableLayoutPanel();
            messageHeader = new TableLayoutPanel();
            messageCountLabel = new AntdUI.Label();
            messageSummaryLabel = new AntdUI.Label();
            _logList = new AntdUI.Input();
            clearLogButton = new AntdUI.Button();
            logGroupCaption = new AntdUI.Label();
            messageToolTip = new ToolTip(components);
            pagePanel.SuspendLayout();
            rootLayout.SuspendLayout();
            headerPanel.SuspendLayout();
            cardsLayout.SuspendLayout();
            controlCard.SuspendLayout();
            controlCardLayout.SuspendLayout();
            modeCard.SuspendLayout();
            modeCardLayout.SuspendLayout();
            waveCard.SuspendLayout();
            waveCardLayout.SuspendLayout();
            monitorInfoLayout.SuspendLayout();
            bodyLayout.SuspendLayout();
            progressGroup.SuspendLayout();
            waveLayout.SuspendLayout();
            waveButtonsLayout.SuspendLayout();
            rightLayout.SuspendLayout();
            axisGroup.SuspendLayout();
            logGroup.SuspendLayout();
            logLayout.SuspendLayout();
            messageHeader.SuspendLayout();
            SuspendLayout();
            // 
            // axisGrid
            // 
            axisGrid.AutoSizeColumnsMode = AntdUI.ColumnsMode.Fill;
            axisGrid.Dock = DockStyle.Fill;
            axisGrid.Font = new Font("Microsoft YaHei UI", 9F);
            axisGrid.Gap = 4;
            axisGrid.Gaps = new Size(4, 4);
            axisGrid.Location = new Point(20, 42);
            axisGrid.Margin = new Padding(5, 5, 5, 5);
            axisGrid.Name = "axisGrid";
            axisGrid.RowHeight = 28;
            axisGrid.RowHeightHeader = 28;
            axisGrid.Size = new Size(810, 241);
            axisGrid.TabIndex = 0;
            // 
            // pagePanel
            // 
            pagePanel.Controls.Add(rootLayout);
            pagePanel.Dock = DockStyle.Fill;
            pagePanel.Location = new Point(0, 0);
            pagePanel.Margin = new Padding(4, 4, 4, 4);
            pagePanel.MinimumSize = new Size(1255, 1100);
            pagePanel.Name = "pagePanel";
            pagePanel.Size = new Size(1359, 905);
            pagePanel.TabIndex = 0;
            // 
            // rootLayout
            // 
            rootLayout.BackColor = Color.FromArgb(241, 245, 249);
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(headerPanel, 0, 0);
            rootLayout.Controls.Add(cardsLayout, 0, 1);
            rootLayout.Controls.Add(monitorInfoLayout, 0, 2);
            rootLayout.Controls.Add(bodyLayout, 0, 3);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Margin = new Padding(0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(20, 20, 20, 20);
            rootLayout.RowCount = 4;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 110F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.Size = new Size(1359, 905);
            rootLayout.TabIndex = 0;
            // 
            // headerPanel
            // 
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Location = new Point(20, 20);
            headerPanel.Margin = new Padding(0);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(1319, 70);
            headerPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.Dock = DockStyle.Top;
            titleLabel.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            titleLabel.Location = new Point(0, 0);
            titleLabel.Margin = new Padding(4, 4, 4, 4);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(1319, 42);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "自动运行";
            // 
            // cardsLayout
            // 
            cardsLayout.ColumnCount = 3;
            cardsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            cardsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            cardsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33334F));
            cardsLayout.Controls.Add(controlCard, 0, 0);
            cardsLayout.Controls.Add(modeCard, 1, 0);
            cardsLayout.Controls.Add(waveCard, 2, 0);
            cardsLayout.Dock = DockStyle.Fill;
            cardsLayout.Location = new Point(20, 90);
            cardsLayout.Margin = new Padding(0);
            cardsLayout.Name = "cardsLayout";
            cardsLayout.RowCount = 1;
            cardsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            cardsLayout.Size = new Size(1319, 110);
            cardsLayout.TabIndex = 1;
            // 
            // controlCard
            // 
            controlCard.Back = Color.White;
            controlCard.BorderColor = Color.FromArgb(203, 213, 225);
            controlCard.BorderWidth = 1F;
            controlCard.Controls.Add(controlCardLayout);
            controlCard.Dock = DockStyle.Fill;
            controlCard.Location = new Point(5, 0);
            controlCard.Margin = new Padding(5, 0, 5, 10);
            controlCard.Name = "controlCard";
            controlCard.Padding = new Padding(15, 6, 15, 6);
            controlCard.Radius = 10;
            controlCard.Size = new Size(429, 100);
            controlCard.TabIndex = 0;
            // 
            // controlCardLayout
            // 
            controlCardLayout.BackColor = Color.White;
            controlCardLayout.ColumnCount = 1;
            controlCardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            controlCardLayout.Controls.Add(controlCaptionLabel, 0, 0);
            controlCardLayout.Controls.Add(controlValueLabel, 0, 1);
            controlCardLayout.Controls.Add(controlDetailLabel, 0, 2);
            controlCardLayout.Dock = DockStyle.Fill;
            controlCardLayout.Location = new Point(17, 8);
            controlCardLayout.Margin = new Padding(0);
            controlCardLayout.Name = "controlCardLayout";
            controlCardLayout.RowCount = 3;
            controlCardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            controlCardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            controlCardLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            controlCardLayout.Size = new Size(395, 84);
            controlCardLayout.TabIndex = 0;
            // 
            // controlCaptionLabel
            // 
            controlCaptionLabel.AutoEllipsis = true;
            controlCaptionLabel.Dock = DockStyle.Fill;
            controlCaptionLabel.Font = new Font("Microsoft YaHei UI", 9F);
            controlCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            controlCaptionLabel.Location = new Point(0, 0);
            controlCaptionLabel.Margin = new Padding(0);
            controlCaptionLabel.Name = "controlCaptionLabel";
            controlCaptionLabel.Size = new Size(395, 25);
            controlCaptionLabel.TabIndex = 0;
            controlCaptionLabel.Text = "当前控制端";
            // 
            // controlValueLabel
            // 
            controlValueLabel.AutoEllipsis = true;
            controlValueLabel.Dock = DockStyle.Fill;
            controlValueLabel.Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Bold);
            controlValueLabel.ForeColor = Color.FromArgb(100, 116, 139);
            controlValueLabel.Location = new Point(0, 25);
            controlValueLabel.Margin = new Padding(0);
            controlValueLabel.Name = "controlValueLabel";
            controlValueLabel.Size = new Size(395, 35);
            controlValueLabel.TabIndex = 1;
            controlValueLabel.Text = "等待反馈";
            // 
            // controlDetailLabel
            // 
            controlDetailLabel.AutoEllipsis = true;
            controlDetailLabel.Dock = DockStyle.Fill;
            controlDetailLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            controlDetailLabel.ForeColor = Color.FromArgb(100, 116, 139);
            controlDetailLabel.Location = new Point(0, 60);
            controlDetailLabel.Margin = new Padding(0);
            controlDetailLabel.Name = "controlDetailLabel";
            controlDetailLabel.Size = new Size(395, 24);
            controlDetailLabel.TabIndex = 2;
            controlDetailLabel.Text = "等待有效反馈";
            // 
            // modeCard
            // 
            modeCard.Back = Color.White;
            modeCard.BorderColor = Color.FromArgb(203, 213, 225);
            modeCard.BorderWidth = 1F;
            modeCard.Controls.Add(modeCardLayout);
            modeCard.Dock = DockStyle.Fill;
            modeCard.Location = new Point(444, 0);
            modeCard.Margin = new Padding(5, 0, 5, 10);
            modeCard.Name = "modeCard";
            modeCard.Padding = new Padding(15, 6, 15, 6);
            modeCard.Radius = 10;
            modeCard.Size = new Size(429, 100);
            modeCard.TabIndex = 1;
            // 
            // modeCardLayout
            // 
            modeCardLayout.BackColor = Color.White;
            modeCardLayout.ColumnCount = 1;
            modeCardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            modeCardLayout.Controls.Add(modeCaptionLabel, 0, 0);
            modeCardLayout.Controls.Add(modeValueLabel, 0, 1);
            modeCardLayout.Controls.Add(modeDetailLabel, 0, 2);
            modeCardLayout.Dock = DockStyle.Fill;
            modeCardLayout.Location = new Point(17, 8);
            modeCardLayout.Margin = new Padding(0);
            modeCardLayout.Name = "modeCardLayout";
            modeCardLayout.RowCount = 3;
            modeCardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            modeCardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            modeCardLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            modeCardLayout.Size = new Size(395, 84);
            modeCardLayout.TabIndex = 0;
            // 
            // modeCaptionLabel
            // 
            modeCaptionLabel.AutoEllipsis = true;
            modeCaptionLabel.Dock = DockStyle.Fill;
            modeCaptionLabel.Font = new Font("Microsoft YaHei UI", 9F);
            modeCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            modeCaptionLabel.Location = new Point(0, 0);
            modeCaptionLabel.Margin = new Padding(0);
            modeCaptionLabel.Name = "modeCaptionLabel";
            modeCaptionLabel.Size = new Size(395, 25);
            modeCaptionLabel.TabIndex = 0;
            modeCaptionLabel.Text = "运行模式";
            // 
            // modeValueLabel
            // 
            modeValueLabel.AutoEllipsis = true;
            modeValueLabel.Dock = DockStyle.Fill;
            modeValueLabel.Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Bold);
            modeValueLabel.ForeColor = Color.FromArgb(100, 116, 139);
            modeValueLabel.Location = new Point(0, 25);
            modeValueLabel.Margin = new Padding(0);
            modeValueLabel.Name = "modeValueLabel";
            modeValueLabel.Size = new Size(395, 35);
            modeValueLabel.TabIndex = 1;
            modeValueLabel.Text = "等待反馈";
            // 
            // modeDetailLabel
            // 
            modeDetailLabel.AutoEllipsis = true;
            modeDetailLabel.Dock = DockStyle.Fill;
            modeDetailLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            modeDetailLabel.ForeColor = Color.FromArgb(100, 116, 139);
            modeDetailLabel.Location = new Point(0, 60);
            modeDetailLabel.Margin = new Padding(0);
            modeDetailLabel.Name = "modeDetailLabel";
            modeDetailLabel.Size = new Size(395, 24);
            modeDetailLabel.TabIndex = 2;
            modeDetailLabel.Text = "等待有效反馈";
            // 
            // waveCard
            // 
            waveCard.Back = Color.White;
            waveCard.BorderColor = Color.FromArgb(203, 213, 225);
            waveCard.BorderWidth = 1F;
            waveCard.Controls.Add(waveCardLayout);
            waveCard.Dock = DockStyle.Fill;
            waveCard.Location = new Point(883, 0);
            waveCard.Margin = new Padding(5, 0, 5, 10);
            waveCard.Name = "waveCard";
            waveCard.Padding = new Padding(15, 6, 15, 6);
            waveCard.Radius = 10;
            waveCard.Size = new Size(431, 100);
            waveCard.TabIndex = 2;
            // 
            // waveCardLayout
            // 
            waveCardLayout.BackColor = Color.White;
            waveCardLayout.ColumnCount = 1;
            waveCardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            waveCardLayout.Controls.Add(waveCaptionLabel, 0, 0);
            waveCardLayout.Controls.Add(_runStateLabel, 0, 1);
            waveCardLayout.Controls.Add(faultCodeLabel, 0, 2);
            waveCardLayout.Dock = DockStyle.Fill;
            waveCardLayout.Location = new Point(17, 8);
            waveCardLayout.Margin = new Padding(0);
            waveCardLayout.Name = "waveCardLayout";
            waveCardLayout.RowCount = 3;
            waveCardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            waveCardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            waveCardLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            waveCardLayout.Size = new Size(397, 84);
            waveCardLayout.TabIndex = 0;
            // 
            // waveCaptionLabel
            // 
            waveCaptionLabel.AutoEllipsis = true;
            waveCaptionLabel.Dock = DockStyle.Fill;
            waveCaptionLabel.Font = new Font("Microsoft YaHei UI", 9F);
            waveCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            waveCaptionLabel.Location = new Point(0, 0);
            waveCaptionLabel.Margin = new Padding(0);
            waveCaptionLabel.Name = "waveCaptionLabel";
            waveCaptionLabel.Size = new Size(397, 25);
            waveCaptionLabel.TabIndex = 0;
            waveCaptionLabel.Text = "造波状态";
            // 
            // _runStateLabel
            // 
            _runStateLabel.AutoEllipsis = true;
            _runStateLabel.Dock = DockStyle.Fill;
            _runStateLabel.Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Bold);
            _runStateLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _runStateLabel.Location = new Point(0, 25);
            _runStateLabel.Margin = new Padding(0);
            _runStateLabel.Name = "_runStateLabel";
            _runStateLabel.Size = new Size(397, 35);
            _runStateLabel.TabIndex = 1;
            _runStateLabel.Text = "等待反馈";
            // 
            // faultCodeLabel
            // 
            faultCodeLabel.AutoEllipsis = true;
            faultCodeLabel.Dock = DockStyle.Fill;
            faultCodeLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            faultCodeLabel.ForeColor = Color.FromArgb(100, 116, 139);
            faultCodeLabel.Location = new Point(0, 60);
            faultCodeLabel.Margin = new Padding(0);
            faultCodeLabel.Name = "faultCodeLabel";
            faultCodeLabel.Size = new Size(397, 24);
            faultCodeLabel.TabIndex = 2;
            faultCodeLabel.Text = "故障码：--";
            // 
            // monitorInfoLayout
            // 
            monitorInfoLayout.ColumnCount = 3;
            monitorInfoLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));
            monitorInfoLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            monitorInfoLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 29F));
            monitorInfoLayout.Controls.Add(connectionLabel, 0, 0);
            monitorInfoLayout.Controls.Add(feedbackLabel, 1, 0);
            monitorInfoLayout.Controls.Add(heartbeatLabel, 2, 0);
            monitorInfoLayout.Dock = DockStyle.Fill;
            monitorInfoLayout.Location = new Point(20, 200);
            monitorInfoLayout.Margin = new Padding(0);
            monitorInfoLayout.Name = "monitorInfoLayout";
            monitorInfoLayout.RowCount = 1;
            monitorInfoLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            monitorInfoLayout.Size = new Size(1319, 40);
            monitorInfoLayout.TabIndex = 2;
            // 
            // connectionLabel
            // 
            connectionLabel.AutoEllipsis = true;
            connectionLabel.Dock = DockStyle.Fill;
            connectionLabel.ForeColor = Color.FromArgb(154, 52, 18);
            connectionLabel.Location = new Point(0, 0);
            connectionLabel.Margin = new Padding(0);
            connectionLabel.Name = "connectionLabel";
            connectionLabel.Size = new Size(474, 40);
            connectionLabel.TabIndex = 0;
            connectionLabel.Text = "● ADS 未连接";
            // 
            // feedbackLabel
            // 
            feedbackLabel.AutoEllipsis = true;
            feedbackLabel.Dock = DockStyle.Fill;
            feedbackLabel.ForeColor = Color.FromArgb(100, 116, 139);
            feedbackLabel.Location = new Point(474, 0);
            feedbackLabel.Margin = new Padding(0);
            feedbackLabel.Name = "feedbackLabel";
            feedbackLabel.Size = new Size(461, 40);
            feedbackLabel.TabIndex = 1;
            feedbackLabel.Text = "反馈时间：--";
            // 
            // heartbeatLabel
            // 
            heartbeatLabel.AutoEllipsis = true;
            heartbeatLabel.Dock = DockStyle.Fill;
            heartbeatLabel.ForeColor = Color.FromArgb(100, 116, 139);
            heartbeatLabel.Location = new Point(935, 0);
            heartbeatLabel.Margin = new Padding(0);
            heartbeatLabel.Name = "heartbeatLabel";
            heartbeatLabel.Size = new Size(384, 40);
            heartbeatLabel.TabIndex = 2;
            heartbeatLabel.Text = "心跳：--";
            // 
            // bodyLayout
            // 
            bodyLayout.ColumnCount = 2;
            bodyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            bodyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 850F));
            bodyLayout.Controls.Add(progressGroup, 0, 0);
            bodyLayout.Controls.Add(rightLayout, 1, 0);
            bodyLayout.Dock = DockStyle.Fill;
            bodyLayout.Location = new Point(20, 240);
            bodyLayout.Margin = new Padding(0);
            bodyLayout.Name = "bodyLayout";
            bodyLayout.RowCount = 1;
            bodyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            bodyLayout.Size = new Size(1319, 645);
            bodyLayout.TabIndex = 3;
            // 
            // progressGroup
            // 
            progressGroup.BackColor = Color.White;
            progressGroup.BorderColor = Color.FromArgb(203, 213, 225);
            progressGroup.BorderWidth = 1F;
            progressGroup.Controls.Add(waveLayout);
            progressGroup.Controls.Add(progressGroupCaption);
            progressGroup.Dock = DockStyle.Fill;
            progressGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            progressGroup.ForeColor = Color.FromArgb(15, 23, 42);
            progressGroup.Location = new Point(0, 0);
            progressGroup.Margin = new Padding(0, 0, 15, 0);
            progressGroup.Name = "progressGroup";
            progressGroup.Padding = new Padding(18, 40, 18, 15);
            progressGroup.Size = new Size(454, 645);
            progressGroup.TabIndex = 0;
            // 
            // waveLayout
            // 
            waveLayout.ColumnCount = 1;
            waveLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            waveLayout.Controls.Add(waveButtonsLayout, 0, 0);
            waveLayout.Controls.Add(waveFileLabel, 0, 1);
            waveLayout.Controls.Add(waveStageLabel, 0, 2);
            waveLayout.Controls.Add(waveProgressBar, 0, 3);
            waveLayout.Controls.Add(waveDetailLabel, 0, 4);
            waveLayout.Controls.Add(runtimeMonitoringSummary, 0, 5);
            waveLayout.Dock = DockStyle.Fill;
            waveLayout.Location = new Point(20, 42);
            waveLayout.Margin = new Padding(4, 4, 4, 4);
            waveLayout.Name = "waveLayout";
            waveLayout.RowCount = 6;
            waveLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            waveLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F));
            waveLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            waveLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            waveLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            waveLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 195F));
            waveLayout.Size = new Size(414, 586);
            waveLayout.TabIndex = 0;
            // 
            // waveButtonsLayout
            // 
            waveButtonsLayout.ColumnCount = 2;
            waveButtonsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            waveButtonsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            waveButtonsLayout.Controls.Add(importWaveButton, 0, 0);
            waveButtonsLayout.Controls.Add(sendWaveButton, 1, 0);
            waveButtonsLayout.Controls.Add(startWaveButton, 0, 1);
            waveButtonsLayout.Controls.Add(stopWaveButton, 1, 1);
            waveButtonsLayout.Dock = DockStyle.Fill;
            waveButtonsLayout.Location = new Point(4, 4);
            waveButtonsLayout.Margin = new Padding(4, 4, 4, 4);
            waveButtonsLayout.Name = "waveButtonsLayout";
            waveButtonsLayout.RowCount = 2;
            waveButtonsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            waveButtonsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            waveButtonsLayout.Size = new Size(406, 112);
            waveButtonsLayout.TabIndex = 0;
            // 
            // importWaveButton
            // 
            importWaveButton.Dock = DockStyle.Fill;
            importWaveButton.Enabled = false;
            importWaveButton.Location = new Point(5, 5);
            importWaveButton.Margin = new Padding(5, 5, 5, 5);
            importWaveButton.Name = "importWaveButton";
            importWaveButton.Size = new Size(193, 46);
            importWaveButton.TabIndex = 0;
            importWaveButton.Text = "导入造波信号";
            importWaveButton.Type = AntdUI.TTypeMini.Primary;
            importWaveButton.Click += ImportWaveButton_Click;
            // 
            // sendWaveButton
            // 
            sendWaveButton.Dock = DockStyle.Fill;
            sendWaveButton.Enabled = false;
            sendWaveButton.Location = new Point(208, 5);
            sendWaveButton.Margin = new Padding(5, 5, 5, 5);
            sendWaveButton.Name = "sendWaveButton";
            sendWaveButton.Size = new Size(193, 46);
            sendWaveButton.TabIndex = 1;
            sendWaveButton.Text = "发送造波信号";
            sendWaveButton.Type = AntdUI.TTypeMini.Primary;
            sendWaveButton.Click += SendWaveButton_Click;
            // 
            // startWaveButton
            // 
            startWaveButton.Dock = DockStyle.Fill;
            startWaveButton.Enabled = false;
            startWaveButton.Location = new Point(5, 61);
            startWaveButton.Margin = new Padding(5, 5, 5, 5);
            startWaveButton.Name = "startWaveButton";
            startWaveButton.Size = new Size(193, 46);
            startWaveButton.TabIndex = 2;
            startWaveButton.Text = "开始造波";
            startWaveButton.Type = AntdUI.TTypeMini.Success;
            startWaveButton.Click += StartWaveButton_Click;
            // 
            // stopWaveButton
            // 
            stopWaveButton.Dock = DockStyle.Fill;
            stopWaveButton.Enabled = false;
            stopWaveButton.Location = new Point(208, 61);
            stopWaveButton.Margin = new Padding(5, 5, 5, 5);
            stopWaveButton.Name = "stopWaveButton";
            stopWaveButton.Size = new Size(193, 46);
            stopWaveButton.TabIndex = 3;
            stopWaveButton.Text = "停止造波";
            stopWaveButton.Type = AntdUI.TTypeMini.Error;
            stopWaveButton.Click += StopWaveButton_Click;
            // 
            // waveFileLabel
            // 
            waveFileLabel.Dock = DockStyle.Fill;
            waveFileLabel.Font = new Font("Microsoft YaHei UI", 9F);
            waveFileLabel.ForeColor = Color.FromArgb(71, 85, 105);
            waveFileLabel.Location = new Point(5, 125);
            waveFileLabel.Margin = new Padding(5, 5, 5, 5);
            waveFileLabel.Name = "waveFileLabel";
            waveFileLabel.Size = new Size(404, 130);
            waveFileLabel.TabIndex = 1;
            waveFileLabel.Text = "尚未导入造波信号。\r\n仅支持旧格式最终 PLC 点位，四轴共用，周期 2 ms。";
            waveFileLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // waveStageLabel
            // 
            waveStageLabel.Dock = DockStyle.Fill;
            waveStageLabel.Font = new Font("Microsoft YaHei UI", 9F);
            waveStageLabel.ForeColor = Color.FromArgb(71, 85, 105);
            waveStageLabel.Location = new Point(5, 265);
            waveStageLabel.Margin = new Padding(5, 5, 5, 5);
            waveStageLabel.Name = "waveStageLabel";
            waveStageLabel.Size = new Size(404, 40);
            waveStageLabel.TabIndex = 2;
            waveStageLabel.Text = "未导入";
            waveStageLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // waveProgressBar
            // 
            waveProgressBar.Dock = DockStyle.Fill;
            waveProgressBar.Location = new Point(5, 315);
            waveProgressBar.Margin = new Padding(5, 5, 5, 5);
            waveProgressBar.Maximum = 1000;
            waveProgressBar.Name = "waveProgressBar";
            waveProgressBar.Size = new Size(404, 25);
            waveProgressBar.Style = ProgressBarStyle.Continuous;
            waveProgressBar.TabIndex = 3;
            // 
            // waveDetailLabel
            // 
            waveDetailLabel.Dock = DockStyle.Fill;
            waveDetailLabel.Font = new Font("Microsoft YaHei UI", 9F);
            waveDetailLabel.ForeColor = Color.FromArgb(71, 85, 105);
            waveDetailLabel.Location = new Point(5, 350);
            waveDetailLabel.Margin = new Padding(5, 5, 5, 5);
            waveDetailLabel.Name = "waveDetailLabel";
            waveDetailLabel.Size = new Size(404, 231);
            waveDetailLabel.TabIndex = 4;
            waveDetailLabel.Text = "等待有效 PLC 反馈。";
            // 
            // progressGroupCaption
            // 
            progressGroupCaption.AutoSizeMode = AntdUI.TAutoSize.Auto;
            progressGroupCaption.ForeColor = Color.FromArgb(15, 23, 42);
            progressGroupCaption.Location = new Point(12, 2);
            progressGroupCaption.Margin = new Padding(4, 4, 4, 4);
            progressGroupCaption.Name = "progressGroupCaption";
            progressGroupCaption.Size = new Size(60, 20);
            progressGroupCaption.TabIndex = 1;
            progressGroupCaption.Text = "造波进度";
            // 
            // rightLayout
            // 
            rightLayout.ColumnCount = 1;
            rightLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rightLayout.Controls.Add(axisGroup, 0, 0);
            rightLayout.Controls.Add(logGroup, 0, 2);
            rightLayout.Dock = DockStyle.Fill;
            rightLayout.Location = new Point(469, 0);
            rightLayout.Margin = new Padding(0);
            rightLayout.Name = "rightLayout";
            rightLayout.RowCount = 3;
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 300F));
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 370F));
            rightLayout.Size = new Size(850, 645);
            rightLayout.TabIndex = 1;
            // 
            // axisGroup
            // 
            axisGroup.BackColor = Color.White;
            axisGroup.BorderColor = Color.FromArgb(203, 213, 225);
            axisGroup.BorderWidth = 1F;
            axisGroup.Controls.Add(axisGrid);
            axisGroup.Controls.Add(axisGroupCaption);
            axisGroup.Dock = DockStyle.Fill;
            axisGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            axisGroup.ForeColor = Color.FromArgb(15, 23, 42);
            axisGroup.Location = new Point(0, 0);
            axisGroup.Margin = new Padding(0);
            axisGroup.Name = "axisGroup";
            axisGroup.Padding = new Padding(18, 40, 18, 15);
            axisGroup.Size = new Size(850, 300);
            axisGroup.TabIndex = 3;
            // 
            // axisGroupCaption
            // 
            axisGroupCaption.AutoSizeMode = AntdUI.TAutoSize.Auto;
            axisGroupCaption.ForeColor = Color.FromArgb(15, 23, 42);
            axisGroupCaption.Location = new Point(12, 2);
            axisGroupCaption.Margin = new Padding(4, 4, 4, 4);
            axisGroupCaption.Name = "axisGroupCaption";
            axisGroupCaption.Size = new Size(60, 20);
            axisGroupCaption.TabIndex = 1;
            axisGroupCaption.Text = "四轴反馈";
            // 
            // logGroup
            // 
            logGroup.BackColor = Color.White;
            logGroup.BorderColor = Color.FromArgb(203, 213, 225);
            logGroup.BorderWidth = 1F;
            logGroup.Controls.Add(logLayout);
            logGroup.Controls.Add(logGroupCaption);
            logGroup.Dock = DockStyle.Fill;
            logGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            logGroup.ForeColor = Color.FromArgb(15, 23, 42);
            logGroup.Location = new Point(0, 275);
            logGroup.Margin = new Padding(0);
            logGroup.Name = "logGroup";
            logGroup.Padding = new Padding(18, 40, 18, 15);
            logGroup.Size = new Size(850, 370);
            logGroup.TabIndex = 4;
            // 
            // logLayout
            // 
            logLayout.ColumnCount = 1;
            logLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            logLayout.Controls.Add(messageHeader, 0, 0);
            logLayout.Controls.Add(_logList, 0, 1);
            logLayout.Controls.Add(clearLogButton, 0, 2);
            logLayout.Dock = DockStyle.Fill;
            logLayout.Location = new Point(20, 42);
            logLayout.Margin = new Padding(0);
            logLayout.Name = "logLayout";
            logLayout.RowCount = 3;
            logLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            logLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            logLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            logLayout.Size = new Size(810, 311);
            logLayout.TabIndex = 0;
            // 
            // messageHeader
            // 
            messageHeader.ColumnCount = 2;
            messageHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            messageHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            messageHeader.Controls.Add(messageCountLabel, 0, 0);
            messageHeader.Controls.Add(messageSummaryLabel, 1, 0);
            messageHeader.Dock = DockStyle.Fill;
            messageHeader.Location = new Point(0, 0);
            messageHeader.Margin = new Padding(0);
            messageHeader.Name = "messageHeader";
            messageHeader.RowCount = 1;
            messageHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            messageHeader.Size = new Size(810, 40);
            messageHeader.TabIndex = 0;
            // 
            // messageCountLabel
            // 
            messageCountLabel.Dock = DockStyle.Fill;
            messageCountLabel.Font = new Font("Microsoft YaHei UI", 9F);
            messageCountLabel.ForeColor = Color.FromArgb(71, 85, 105);
            messageCountLabel.Location = new Point(0, 0);
            messageCountLabel.Margin = new Padding(0);
            messageCountLabel.Name = "messageCountLabel";
            messageCountLabel.Size = new Size(150, 40);
            messageCountLabel.TabIndex = 0;
            messageCountLabel.Text = "消息：0 条";
            // 
            // messageSummaryLabel
            // 
            messageSummaryLabel.AutoEllipsis = true;
            messageSummaryLabel.Dock = DockStyle.Fill;
            messageSummaryLabel.Font = new Font("Microsoft YaHei UI", 9F);
            messageSummaryLabel.ForeColor = Color.FromArgb(71, 85, 105);
            messageSummaryLabel.Location = new Point(150, 0);
            messageSummaryLabel.Margin = new Padding(0, 0, 15, 0);
            messageSummaryLabel.Name = "messageSummaryLabel";
            messageSummaryLabel.Size = new Size(645, 40);
            messageSummaryLabel.TabIndex = 1;
            messageSummaryLabel.Text = "暂无运行消息";
            // 
            // _logList
            // 
            _logList.BackColor = Color.White;
            _logList.Dock = DockStyle.Fill;
            _logList.Font = new Font("Microsoft YaHei UI", 9F);
            _logList.Location = new Point(4, 44);
            _logList.Margin = new Padding(4, 4, 4, 4);
            _logList.Multiline = true;
            _logList.Name = "_logList";
            _logList.ReadOnly = true;
            _logList.Size = new Size(802, 215);
            _logList.TabIndex = 1;
            _logList.WaveSize = 0;
            _logList.WordWrap = false;
            // 
            // clearLogButton
            // 
            clearLogButton.Anchor = AnchorStyles.Left;
            clearLogButton.AutoSizeMode = AntdUI.TAutoSize.Auto;
            clearLogButton.BackColor = Color.FromArgb(248, 250, 252);
            clearLogButton.BorderWidth = 1F;
            clearLogButton.Cursor = Cursors.Hand;
            clearLogButton.DefaultBack = Color.FromArgb(248, 250, 252);
            clearLogButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
            clearLogButton.Font = new Font("Microsoft YaHei UI", 9F);
            clearLogButton.ForeColor = Color.FromArgb(15, 23, 42);
            clearLogButton.Location = new Point(4, 267);
            clearLogButton.Margin = new Padding(4, 4, 4, 4);
            clearLogButton.MinimumSize = new Size(142, 40);
            clearLogButton.Name = "clearLogButton";
            clearLogButton.Size = new Size(142, 45);
            clearLogButton.TabIndex = 2;
            clearLogButton.Text = "清空本页消息";
            clearLogButton.Click += ClearLogButton_Click;
            // 
            // logGroupCaption
            // 
            logGroupCaption.AutoSizeMode = AntdUI.TAutoSize.Auto;
            logGroupCaption.ForeColor = Color.FromArgb(15, 23, 42);
            logGroupCaption.Location = new Point(12, 2);
            logGroupCaption.Margin = new Padding(4, 4, 4, 4);
            logGroupCaption.Name = "logGroupCaption";
            logGroupCaption.Size = new Size(60, 20);
            logGroupCaption.TabIndex = 1;
            logGroupCaption.Text = "运行消息";
            // 
            // Auto
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            AutoScrollMinSize = new Size(1255, 905);
            BackColor = Color.FromArgb(241, 245, 249);
            Controls.Add(pagePanel);
            Font = new Font("Microsoft YaHei UI", 9F);
            ForeColor = Color.FromArgb(15, 23, 42);
            Margin = new Padding(4, 4, 4, 4);
            Name = "Auto";
            Size = new Size(1380, 758);
            pagePanel.ResumeLayout(false);
            rootLayout.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            cardsLayout.ResumeLayout(false);
            controlCard.ResumeLayout(false);
            controlCardLayout.ResumeLayout(false);
            modeCard.ResumeLayout(false);
            modeCardLayout.ResumeLayout(false);
            waveCard.ResumeLayout(false);
            waveCardLayout.ResumeLayout(false);
            monitorInfoLayout.ResumeLayout(false);
            bodyLayout.ResumeLayout(false);
            progressGroup.ResumeLayout(false);
            progressGroup.PerformLayout();
            waveLayout.ResumeLayout(false);
            waveButtonsLayout.ResumeLayout(false);
            rightLayout.ResumeLayout(false);
            axisGroup.ResumeLayout(false);
            axisGroup.PerformLayout();
            logGroup.ResumeLayout(false);
            logGroup.PerformLayout();
            logLayout.ResumeLayout(false);
            logLayout.PerformLayout();
            messageHeader.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
