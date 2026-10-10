namespace Page_switching
{
    partial class Auto
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pagePanel;
        private TableLayoutPanel rootLayout;
        private Panel headerPanel;
        private Label titleLabel;
        private Label subtitleLabel;
        private TableLayoutPanel cardsLayout;
        private Panel controlCard;
        private TableLayoutPanel controlCardLayout;
        private Label controlCaptionLabel;
        private Label controlValueLabel;
        private Label controlDetailLabel;
        private Panel modeCard;
        private TableLayoutPanel modeCardLayout;
        private Label modeCaptionLabel;
        private Label modeValueLabel;
        private Label modeDetailLabel;
        private Panel waveCard;
        private TableLayoutPanel waveCardLayout;
        private Label waveCaptionLabel;
        private Label _runStateLabel;
        private Label faultCodeLabel;
        private TableLayoutPanel monitorInfoLayout;
        private Label connectionLabel;
        private Label feedbackLabel;
        private Label heartbeatLabel;
        private GroupBox axisGroup;
        private AxisFeedbackGrid axisGrid;
        private DataGridViewTextBoxColumn axisNumberColumn;
        private DataGridViewTextBoxColumn positionColumn;
        private DataGridViewTextBoxColumn speedColumn;
        private DataGridViewTextBoxColumn homedColumn;
        private DataGridViewTextBoxColumn alarmColumn;
        private DataGridViewTextBoxColumn negativeLimitColumn;
        private DataGridViewTextBoxColumn positiveLimitColumn;
        private DataGridViewTextBoxColumn originColumn;
        private GroupBox logGroup;
        private TableLayoutPanel logLayout;
        private TableLayoutPanel messageHeader;
        private Label messageCountLabel;
        private Label messageSummaryLabel;
        private Button messageToggleButton;
        private ToolTip messageToolTip;
        private ListBox _logList;
        private Button clearLogButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            components = new System.ComponentModel.Container();
            pagePanel = new Panel();
            rootLayout = new TableLayoutPanel();
            headerPanel = new Panel();
            subtitleLabel = new Label();
            titleLabel = new Label();
            cardsLayout = new TableLayoutPanel();
            controlCard = new Panel();
            controlCardLayout = new TableLayoutPanel();
            controlCaptionLabel = new Label();
            controlValueLabel = new Label();
            controlDetailLabel = new Label();
            modeCard = new Panel();
            modeCardLayout = new TableLayoutPanel();
            modeCaptionLabel = new Label();
            modeValueLabel = new Label();
            modeDetailLabel = new Label();
            waveCard = new Panel();
            waveCardLayout = new TableLayoutPanel();
            waveCaptionLabel = new Label();
            _runStateLabel = new Label();
            faultCodeLabel = new Label();
            monitorInfoLayout = new TableLayoutPanel();
            connectionLabel = new Label();
            feedbackLabel = new Label();
            heartbeatLabel = new Label();
            axisGroup = new GroupBox();
            axisGrid = new AxisFeedbackGrid();
            axisNumberColumn = new DataGridViewTextBoxColumn();
            positionColumn = new DataGridViewTextBoxColumn();
            speedColumn = new DataGridViewTextBoxColumn();
            homedColumn = new DataGridViewTextBoxColumn();
            alarmColumn = new DataGridViewTextBoxColumn();
            negativeLimitColumn = new DataGridViewTextBoxColumn();
            positiveLimitColumn = new DataGridViewTextBoxColumn();
            originColumn = new DataGridViewTextBoxColumn();
            logGroup = new GroupBox();
            logLayout = new TableLayoutPanel();
            messageHeader = new TableLayoutPanel();
            messageCountLabel = new Label();
            messageSummaryLabel = new Label();
            messageToggleButton = new Button();
            messageToolTip = new ToolTip(components);
            _logList = new ListBox();
            clearLogButton = new Button();
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
            axisGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)axisGrid).BeginInit();
            logGroup.SuspendLayout();
            logLayout.SuspendLayout();
            messageHeader.SuspendLayout();
            SuspendLayout();
            //
            // pagePanel
            //
            pagePanel.Controls.Add(logGroup);
            pagePanel.Controls.Add(rootLayout);
            pagePanel.Dock = DockStyle.Fill;
            pagePanel.Location = new Point(0, 0);
            pagePanel.MinimumSize = new Size(900, 480);
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
            rootLayout.Controls.Add(cardsLayout, 0, 1);
            rootLayout.Controls.Add(monitorInfoLayout, 0, 2);
            rootLayout.Controls.Add(axisGroup, 0, 3);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Margin = new Padding(0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(16);
            rootLayout.RowCount = 6;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 88F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 190F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.Size = new Size(1104, 606);
            rootLayout.TabIndex = 0;
            //
            // headerPanel
            //
            headerPanel.Controls.Add(subtitleLabel);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Location = new Point(16, 16);
            headerPanel.Margin = new Padding(0);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(1072, 56);
            headerPanel.TabIndex = 0;
            //
            // subtitleLabel
            //
            subtitleLabel.Dock = DockStyle.Fill;
            subtitleLabel.ForeColor = Color.FromArgb(71, 85, 105);
            subtitleLabel.Location = new Point(0, 34);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(1072, 22);
            subtitleLabel.TabIndex = 0;
            subtitleLabel.Text = "控制端、运行模式与造波反馈 · 只读监控";
            subtitleLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // titleLabel
            //
            titleLabel.Dock = DockStyle.Top;
            titleLabel.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            titleLabel.Location = new Point(0, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(1072, 34);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "自动运行";
            titleLabel.TextAlign = ContentAlignment.MiddleLeft;
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
            cardsLayout.Location = new Point(16, 72);
            cardsLayout.Margin = new Padding(0);
            cardsLayout.Name = "cardsLayout";
            cardsLayout.RowCount = 1;
            cardsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            cardsLayout.Size = new Size(1072, 88);
            cardsLayout.TabIndex = 1;
            //
            // controlCard
            //
            controlCard.BackColor = Color.White;
            controlCard.BorderStyle = BorderStyle.FixedSingle;
            controlCard.Controls.Add(controlCardLayout);
            controlCard.Dock = DockStyle.Fill;
            controlCard.Location = new Point(4, 0);
            controlCard.Margin = new Padding(4, 0, 4, 8);
            controlCard.Name = "controlCard";
            controlCard.Padding = new Padding(12, 5, 12, 5);
            controlCard.Size = new Size(349, 80);
            controlCard.TabIndex = 0;
            //
            // controlCardLayout
            //
            controlCardLayout.ColumnCount = 1;
            controlCardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            controlCardLayout.Controls.Add(controlCaptionLabel, 0, 0);
            controlCardLayout.Controls.Add(controlValueLabel, 0, 1);
            controlCardLayout.Controls.Add(controlDetailLabel, 0, 2);
            controlCardLayout.Dock = DockStyle.Fill;
            controlCardLayout.Location = new Point(12, 5);
            controlCardLayout.Margin = new Padding(0);
            controlCardLayout.Name = "controlCardLayout";
            controlCardLayout.RowCount = 3;
            controlCardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            controlCardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            controlCardLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            controlCardLayout.Size = new Size(323, 68);
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
            controlCaptionLabel.Size = new Size(323, 20);
            controlCaptionLabel.TabIndex = 0;
            controlCaptionLabel.Text = "当前控制端";
            controlCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // controlValueLabel
            //
            controlValueLabel.AutoEllipsis = true;
            controlValueLabel.Dock = DockStyle.Fill;
            controlValueLabel.Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Bold);
            controlValueLabel.ForeColor = Color.FromArgb(100, 116, 139);
            controlValueLabel.Location = new Point(0, 20);
            controlValueLabel.Margin = new Padding(0);
            controlValueLabel.Name = "controlValueLabel";
            controlValueLabel.Size = new Size(323, 28);
            controlValueLabel.TabIndex = 1;
            controlValueLabel.Text = "等待反馈";
            controlValueLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // controlDetailLabel
            //
            controlDetailLabel.AutoEllipsis = true;
            controlDetailLabel.Dock = DockStyle.Fill;
            controlDetailLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            controlDetailLabel.ForeColor = Color.FromArgb(100, 116, 139);
            controlDetailLabel.Location = new Point(0, 48);
            controlDetailLabel.Margin = new Padding(0);
            controlDetailLabel.Name = "controlDetailLabel";
            controlDetailLabel.Size = new Size(323, 20);
            controlDetailLabel.TabIndex = 2;
            controlDetailLabel.Text = "等待有效反馈";
            controlDetailLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // modeCard
            //
            modeCard.BackColor = Color.White;
            modeCard.BorderStyle = BorderStyle.FixedSingle;
            modeCard.Controls.Add(modeCardLayout);
            modeCard.Dock = DockStyle.Fill;
            modeCard.Location = new Point(361, 0);
            modeCard.Margin = new Padding(4, 0, 4, 8);
            modeCard.Name = "modeCard";
            modeCard.Padding = new Padding(12, 5, 12, 5);
            modeCard.Size = new Size(349, 80);
            modeCard.TabIndex = 1;
            //
            // modeCardLayout
            //
            modeCardLayout.ColumnCount = 1;
            modeCardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            modeCardLayout.Controls.Add(modeCaptionLabel, 0, 0);
            modeCardLayout.Controls.Add(modeValueLabel, 0, 1);
            modeCardLayout.Controls.Add(modeDetailLabel, 0, 2);
            modeCardLayout.Dock = DockStyle.Fill;
            modeCardLayout.Location = new Point(12, 5);
            modeCardLayout.Margin = new Padding(0);
            modeCardLayout.Name = "modeCardLayout";
            modeCardLayout.RowCount = 3;
            modeCardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            modeCardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            modeCardLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            modeCardLayout.Size = new Size(323, 68);
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
            modeCaptionLabel.Size = new Size(323, 20);
            modeCaptionLabel.TabIndex = 0;
            modeCaptionLabel.Text = "运行模式";
            modeCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // modeValueLabel
            //
            modeValueLabel.AutoEllipsis = true;
            modeValueLabel.Dock = DockStyle.Fill;
            modeValueLabel.Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Bold);
            modeValueLabel.ForeColor = Color.FromArgb(100, 116, 139);
            modeValueLabel.Location = new Point(0, 20);
            modeValueLabel.Margin = new Padding(0);
            modeValueLabel.Name = "modeValueLabel";
            modeValueLabel.Size = new Size(323, 28);
            modeValueLabel.TabIndex = 1;
            modeValueLabel.Text = "等待反馈";
            modeValueLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // modeDetailLabel
            //
            modeDetailLabel.AutoEllipsis = true;
            modeDetailLabel.Dock = DockStyle.Fill;
            modeDetailLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            modeDetailLabel.ForeColor = Color.FromArgb(100, 116, 139);
            modeDetailLabel.Location = new Point(0, 48);
            modeDetailLabel.Margin = new Padding(0);
            modeDetailLabel.Name = "modeDetailLabel";
            modeDetailLabel.Size = new Size(323, 20);
            modeDetailLabel.TabIndex = 2;
            modeDetailLabel.Text = "等待有效反馈";
            modeDetailLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // waveCard
            //
            waveCard.BackColor = Color.White;
            waveCard.BorderStyle = BorderStyle.FixedSingle;
            waveCard.Controls.Add(waveCardLayout);
            waveCard.Dock = DockStyle.Fill;
            waveCard.Location = new Point(718, 0);
            waveCard.Margin = new Padding(4, 0, 4, 8);
            waveCard.Name = "waveCard";
            waveCard.Padding = new Padding(12, 5, 12, 5);
            waveCard.Size = new Size(350, 80);
            waveCard.TabIndex = 2;
            //
            // waveCardLayout
            //
            waveCardLayout.ColumnCount = 1;
            waveCardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            waveCardLayout.Controls.Add(waveCaptionLabel, 0, 0);
            waveCardLayout.Controls.Add(_runStateLabel, 0, 1);
            waveCardLayout.Controls.Add(faultCodeLabel, 0, 2);
            waveCardLayout.Dock = DockStyle.Fill;
            waveCardLayout.Location = new Point(12, 5);
            waveCardLayout.Margin = new Padding(0);
            waveCardLayout.Name = "waveCardLayout";
            waveCardLayout.RowCount = 3;
            waveCardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            waveCardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            waveCardLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            waveCardLayout.Size = new Size(324, 68);
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
            waveCaptionLabel.Size = new Size(324, 20);
            waveCaptionLabel.TabIndex = 0;
            waveCaptionLabel.Text = "造波状态";
            waveCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // _runStateLabel
            //
            _runStateLabel.AutoEllipsis = true;
            _runStateLabel.Dock = DockStyle.Fill;
            _runStateLabel.Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Bold);
            _runStateLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _runStateLabel.Location = new Point(0, 20);
            _runStateLabel.Margin = new Padding(0);
            _runStateLabel.Name = "_runStateLabel";
            _runStateLabel.Size = new Size(324, 28);
            _runStateLabel.TabIndex = 1;
            _runStateLabel.Text = "等待反馈";
            _runStateLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // faultCodeLabel
            //
            faultCodeLabel.AutoEllipsis = true;
            faultCodeLabel.Dock = DockStyle.Fill;
            faultCodeLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            faultCodeLabel.ForeColor = Color.FromArgb(100, 116, 139);
            faultCodeLabel.Location = new Point(0, 48);
            faultCodeLabel.Margin = new Padding(0);
            faultCodeLabel.Name = "faultCodeLabel";
            faultCodeLabel.Size = new Size(324, 20);
            faultCodeLabel.TabIndex = 2;
            faultCodeLabel.Text = "故障码：--";
            faultCodeLabel.TextAlign = ContentAlignment.MiddleLeft;
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
            monitorInfoLayout.Location = new Point(16, 160);
            monitorInfoLayout.Margin = new Padding(0);
            monitorInfoLayout.Name = "monitorInfoLayout";
            monitorInfoLayout.RowCount = 1;
            monitorInfoLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            monitorInfoLayout.Size = new Size(1072, 32);
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
            connectionLabel.Size = new Size(385, 32);
            connectionLabel.TabIndex = 0;
            connectionLabel.Text = "● ADS 未连接";
            connectionLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // feedbackLabel
            //
            feedbackLabel.AutoEllipsis = true;
            feedbackLabel.Dock = DockStyle.Fill;
            feedbackLabel.ForeColor = Color.FromArgb(100, 116, 139);
            feedbackLabel.Location = new Point(385, 0);
            feedbackLabel.Margin = new Padding(0);
            feedbackLabel.Name = "feedbackLabel";
            feedbackLabel.Size = new Size(375, 32);
            feedbackLabel.TabIndex = 1;
            feedbackLabel.Text = "反馈时间：--";
            feedbackLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // heartbeatLabel
            //
            heartbeatLabel.AutoEllipsis = true;
            heartbeatLabel.Dock = DockStyle.Fill;
            heartbeatLabel.ForeColor = Color.FromArgb(100, 116, 139);
            heartbeatLabel.Location = new Point(760, 0);
            heartbeatLabel.Margin = new Padding(0);
            heartbeatLabel.Name = "heartbeatLabel";
            heartbeatLabel.Size = new Size(312, 32);
            heartbeatLabel.TabIndex = 2;
            heartbeatLabel.Text = "心跳：--";
            heartbeatLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // axisGroup
            //
            axisGroup.BackColor = Color.White;
            axisGroup.Controls.Add(axisGrid);
            axisGroup.Dock = DockStyle.Fill;
            axisGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            axisGroup.ForeColor = Color.FromArgb(15, 23, 42);
            axisGroup.Location = new Point(16, 192);
            axisGroup.Margin = new Padding(0);
            axisGroup.Name = "axisGroup";
            axisGroup.Padding = new Padding(14, 18, 14, 12);
            axisGroup.Size = new Size(1072, 190);
            axisGroup.TabIndex = 3;
            axisGroup.TabStop = false;
            axisGroup.Text = "四轴反馈";
            //
            // axisGrid
            //
            axisGrid.AllowUserToAddRows = false;
            axisGrid.AllowUserToDeleteRows = false;
            axisGrid.AllowUserToResizeColumns = false;
            axisGrid.AllowUserToResizeRows = false;
            axisGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            axisGrid.BackgroundColor = Color.White;
            axisGrid.BorderStyle = BorderStyle.None;
            axisGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            axisGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.Font = new Font("Microsoft YaHei UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(71, 85, 105);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(71, 85, 105);
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            axisGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            axisGrid.ColumnHeadersHeight = 28;
            axisGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            axisGrid.Columns.AddRange(new DataGridViewColumn[] { axisNumberColumn, positionColumn, speedColumn, homedColumn, alarmColumn, negativeLimitColumn, positiveLimitColumn, originColumn });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Microsoft YaHei UI", 9F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(100, 116, 139);
            dataGridViewCellStyle4.Padding = new Padding(4, 0, 4, 0);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            axisGrid.DefaultCellStyle = dataGridViewCellStyle4;
            axisGrid.Dock = DockStyle.Fill;
            axisGrid.EnableHeadersVisualStyles = false;
            axisGrid.Font = new Font("Microsoft YaHei UI", 9F);
            axisGrid.GridColor = Color.FromArgb(226, 232, 240);
            axisGrid.Location = new Point(14, 34);
            axisGrid.MultiSelect = false;
            axisGrid.Name = "axisGrid";
            axisGrid.ReadOnly = true;
            axisGrid.RowHeadersVisible = false;
            axisGrid.RowTemplate.Height = 28;
            axisGrid.ScrollBars = ScrollBars.None;
            axisGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            axisGrid.Size = new Size(1044, 144);
            axisGrid.TabIndex = 0;
            axisGrid.TabStop = false;
            //
            // axisNumberColumn
            //
            axisNumberColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            axisNumberColumn.HeaderText = "轴";
            axisNumberColumn.MinimumWidth = 48;
            axisNumberColumn.Name = "axisNumberColumn";
            axisNumberColumn.ReadOnly = true;
            axisNumberColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            axisNumberColumn.Width = 48;
            //
            // positionColumn
            //
            positionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            positionColumn.DefaultCellStyle = dataGridViewCellStyle2;
            positionColumn.HeaderText = "位置反馈";
            positionColumn.MinimumWidth = 120;
            positionColumn.Name = "positionColumn";
            positionColumn.ReadOnly = true;
            positionColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            //
            // speedColumn
            //
            speedColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            speedColumn.DefaultCellStyle = dataGridViewCellStyle3;
            speedColumn.HeaderText = "速度反馈";
            speedColumn.MinimumWidth = 120;
            speedColumn.Name = "speedColumn";
            speedColumn.ReadOnly = true;
            speedColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            //
            // homedColumn
            //
            homedColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            homedColumn.HeaderText = "回零";
            homedColumn.MinimumWidth = 64;
            homedColumn.Name = "homedColumn";
            homedColumn.ReadOnly = true;
            homedColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            homedColumn.Width = 64;
            //
            // alarmColumn
            //
            alarmColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            alarmColumn.HeaderText = "报警";
            alarmColumn.MinimumWidth = 64;
            alarmColumn.Name = "alarmColumn";
            alarmColumn.ReadOnly = true;
            alarmColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            alarmColumn.Width = 64;
            //
            // negativeLimitColumn
            //
            negativeLimitColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            negativeLimitColumn.HeaderText = "负限位";
            negativeLimitColumn.MinimumWidth = 74;
            negativeLimitColumn.Name = "negativeLimitColumn";
            negativeLimitColumn.ReadOnly = true;
            negativeLimitColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            negativeLimitColumn.Width = 74;
            //
            // positiveLimitColumn
            //
            positiveLimitColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            positiveLimitColumn.HeaderText = "正限位";
            positiveLimitColumn.MinimumWidth = 74;
            positiveLimitColumn.Name = "positiveLimitColumn";
            positiveLimitColumn.ReadOnly = true;
            positiveLimitColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            positiveLimitColumn.Width = 74;
            //
            // originColumn
            //
            originColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            originColumn.HeaderText = "原点";
            originColumn.MinimumWidth = 64;
            originColumn.Name = "originColumn";
            originColumn.ReadOnly = true;
            originColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            originColumn.Width = 64;
            //
            // logGroup
            //
            logGroup.BackColor = Color.White;
            logGroup.Controls.Add(logLayout);
            logGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            logGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            logGroup.ForeColor = Color.FromArgb(15, 23, 42);
            logGroup.Location = new Point(16, 386);
            logGroup.Margin = new Padding(0, 4, 0, 0);
            logGroup.Name = "logGroup";
            logGroup.Padding = new Padding(14, 18, 14, 12);
            logGroup.Size = new Size(1072, 78);
            logGroup.TabIndex = 4;
            logGroup.TabStop = false;
            logGroup.Text = "运行消息";
            //
            // logLayout
            //
            logLayout.ColumnCount = 1;
            logLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            logLayout.Controls.Add(messageHeader, 0, 0);
            logLayout.Controls.Add(_logList, 0, 1);
            logLayout.Controls.Add(clearLogButton, 0, 2);
            logLayout.Dock = DockStyle.Fill;
            logLayout.Location = new Point(14, 34);
            logLayout.Margin = new Padding(0);
            logLayout.Name = "logLayout";
            logLayout.RowCount = 3;
            logLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            logLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 0F));
            logLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 0F));
            logLayout.Size = new Size(1044, 32);
            logLayout.TabIndex = 0;
            //
            // messageHeader
            //
            messageHeader.ColumnCount = 3;
            messageHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            messageHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            messageHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 88F));
            messageHeader.Controls.Add(messageCountLabel, 0, 0);
            messageHeader.Controls.Add(messageSummaryLabel, 1, 0);
            messageHeader.Controls.Add(messageToggleButton, 2, 0);
            messageHeader.Dock = DockStyle.Fill;
            messageHeader.Margin = new Padding(0);
            messageHeader.Name = "messageHeader";
            messageHeader.RowCount = 1;
            messageHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            messageHeader.Size = new Size(1044, 32);
            messageHeader.TabIndex = 0;
            //
            // messageCountLabel
            //
            messageCountLabel.Dock = DockStyle.Fill;
            messageCountLabel.Font = new Font("Microsoft YaHei UI", 9F);
            messageCountLabel.ForeColor = Color.FromArgb(71, 85, 105);
            messageCountLabel.Margin = new Padding(0);
            messageCountLabel.Name = "messageCountLabel";
            messageCountLabel.Text = "消息：0 条";
            messageCountLabel.TextAlign = ContentAlignment.MiddleLeft;
            messageCountLabel.TabIndex = 0;
            //
            // messageSummaryLabel
            //
            messageSummaryLabel.AutoEllipsis = true;
            messageSummaryLabel.Dock = DockStyle.Fill;
            messageSummaryLabel.Font = new Font("Microsoft YaHei UI", 9F);
            messageSummaryLabel.ForeColor = Color.FromArgb(71, 85, 105);
            messageSummaryLabel.Margin = new Padding(0, 0, 12, 0);
            messageSummaryLabel.Name = "messageSummaryLabel";
            messageSummaryLabel.Text = "暂无运行消息";
            messageSummaryLabel.TextAlign = ContentAlignment.MiddleLeft;
            messageSummaryLabel.TabIndex = 1;
            //
            // messageToggleButton
            //
            messageToggleButton.BackColor = Color.FromArgb(248, 250, 252);
            messageToggleButton.Cursor = Cursors.Hand;
            messageToggleButton.Dock = DockStyle.Fill;
            messageToggleButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            messageToggleButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
            messageToggleButton.FlatStyle = FlatStyle.Flat;
            messageToggleButton.Font = new Font("Microsoft YaHei UI", 9F);
            messageToggleButton.ForeColor = Color.FromArgb(15, 23, 42);
            messageToggleButton.Margin = new Padding(0);
            messageToggleButton.Name = "messageToggleButton";
            messageToggleButton.Text = "展开 ▼";
            messageToggleButton.TabIndex = 2;
            messageToggleButton.UseVisualStyleBackColor = false;
            messageToggleButton.Click += MessageToggleButton_Click;
            //
            // _logList
            //
            _logList.BackColor = Color.White;
            _logList.BorderStyle = BorderStyle.None;
            _logList.Dock = DockStyle.Fill;
            _logList.Font = new Font("Microsoft YaHei UI", 9F);
            _logList.IntegralHeight = false;
            _logList.ItemHeight = 17;
            _logList.Location = new Point(3, 3);
            _logList.Name = "_logList";
            _logList.Size = new Size(1038, 114);
            _logList.TabIndex = 1;
            _logList.Visible = false;
            //
            // clearLogButton
            //
            clearLogButton.Anchor = AnchorStyles.Left;
            clearLogButton.AutoSize = true;
            clearLogButton.BackColor = Color.FromArgb(248, 250, 252);
            clearLogButton.Cursor = Cursors.Hand;
            clearLogButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            clearLogButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
            clearLogButton.FlatStyle = FlatStyle.Flat;
            clearLogButton.Font = new Font("Microsoft YaHei UI", 9F);
            clearLogButton.ForeColor = Color.FromArgb(15, 23, 42);
            clearLogButton.Location = new Point(3, 123);
            clearLogButton.MinimumSize = new Size(114, 32);
            clearLogButton.Name = "clearLogButton";
            clearLogButton.Size = new Size(114, 32);
            clearLogButton.TabIndex = 2;
            clearLogButton.Visible = false;
            clearLogButton.Text = "清空本页消息";
            clearLogButton.UseVisualStyleBackColor = false;
            clearLogButton.Click += ClearLogButton_Click;
            //
            // Auto
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            AutoScrollMinSize = new Size(900, 480);
            BackColor = Color.FromArgb(241, 245, 249);
            Controls.Add(pagePanel);
            Font = new Font("Microsoft YaHei UI", 9F);
            ForeColor = Color.FromArgb(15, 23, 42);
            Name = "Auto";
            Size = new Size(1104, 606);
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
            axisGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)axisGrid).EndInit();
            logGroup.ResumeLayout(false);
            logLayout.ResumeLayout(false);
            logLayout.PerformLayout();
            messageHeader.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
