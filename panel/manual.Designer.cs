namespace Page_switching
{
    partial class Manual
    {
        private System.ComponentModel.IContainer components = null!;
        private TableLayoutPanel axis1FeedbackLayout;
        private Label axis1NegativeLimitLamp;
        private Label axis1OriginLamp;
        private Label axis1PositiveLimitLamp;
        private TableLayoutPanel axis2FeedbackLayout;
        private Label axis2NegativeLimitLamp;
        private Label axis2OriginLamp;
        private Label axis2PositiveLimitLamp;
        private TableLayoutPanel axis3FeedbackLayout;
        private Label axis3NegativeLimitLamp;
        private Label axis3OriginLamp;
        private Label axis3PositiveLimitLamp;
        private TableLayoutPanel axis4FeedbackLayout;
        private Label axis4NegativeLimitLamp;
        private Label axis4OriginLamp;
        private Label axis4PositiveLimitLamp;
        private TableLayoutPanel rootLayout;
        private Panel headerPanel;
        private Label titleLabel;
        private Label connectionStateLabel;
        private FlowLayoutPanel globalActionsPanel;
        private Button enableAllButton;
        private Button disableAllButton;
        private Button resetAlarmButton;
        private Button homeAllButton;
        private Button stopAllButton;
        private TableLayoutPanel contentLayout;
        private GroupBox overviewGroup;
        private TableLayoutPanel axisOverviewLayout;
        private Label axisHeaderLabel;
        private Label positionHeaderLabel;
        private Label statusHeaderLabel;
        private Label axis1NameLabel;
        private Label axis2NameLabel;
        private Label axis3NameLabel;
        private Label axis4NameLabel;
        private Label axis1StatusLabel;
        private Label axis2StatusLabel;
        private Label axis3StatusLabel;
        private Label axis4StatusLabel;
        private ServoPositionIndicator axis1PositionIndicator;
        private ServoPositionIndicator axis2PositionIndicator;
        private ServoPositionIndicator axis3PositionIndicator;
        private ServoPositionIndicator axis4PositionIndicator;
        private GroupBox controlGroup;
        private TableLayoutPanel controlLayout;
        private Label selectedAxisCaptionLabel;
        private ComboBox axisSelector;
        private Label selectedStatusCaptionLabel;
        private Label selectedStatusLabel;
        private Label selectedActualCaptionLabel;
        private Label selectedActualLabel;
        private Label selectedSpeedCaptionLabel;
        private Label selectedSpeedLabel;
        private Label jogSpeedCaptionLabel;
        private NumericUpDown jogSpeedInput;
        private Label jogSectionLabel;
        private Button jogNegativeButton;
        private Button jogPositiveButton;
        private Label singleAxisSectionLabel;
        private Button homeSelectedButton;
        private Button stopSelectedButton;
        private Label helperLabel;

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
            axis1FeedbackLayout = new TableLayoutPanel();
            axis1PositionIndicator = new ServoPositionIndicator();
            axis1NegativeLimitLamp = new Label();
            axis1OriginLamp = new Label();
            axis1PositiveLimitLamp = new Label();
            axis2FeedbackLayout = new TableLayoutPanel();
            axis2PositionIndicator = new ServoPositionIndicator();
            axis2NegativeLimitLamp = new Label();
            axis2OriginLamp = new Label();
            axis2PositiveLimitLamp = new Label();
            axis3FeedbackLayout = new TableLayoutPanel();
            axis3PositionIndicator = new ServoPositionIndicator();
            axis3NegativeLimitLamp = new Label();
            axis3OriginLamp = new Label();
            axis3PositiveLimitLamp = new Label();
            axis4FeedbackLayout = new TableLayoutPanel();
            axis4PositionIndicator = new ServoPositionIndicator();
            axis4NegativeLimitLamp = new Label();
            axis4OriginLamp = new Label();
            axis4PositiveLimitLamp = new Label();
            rootLayout = new TableLayoutPanel();
            headerPanel = new Panel();
            connectionStateLabel = new Label();
            titleLabel = new Label();
            globalActionsPanel = new FlowLayoutPanel();
            enableAllButton = new Button();
            disableAllButton = new Button();
            resetAlarmButton = new Button();
            homeAllButton = new Button();
            stopAllButton = new Button();
            contentLayout = new TableLayoutPanel();
            overviewGroup = new GroupBox();
            axisOverviewLayout = new TableLayoutPanel();
            axisHeaderLabel = new Label();
            positionHeaderLabel = new Label();
            statusHeaderLabel = new Label();
            axis1NameLabel = new Label();
            axis1StatusLabel = new Label();
            axis2NameLabel = new Label();
            axis2StatusLabel = new Label();
            axis3NameLabel = new Label();
            axis3StatusLabel = new Label();
            axis4NameLabel = new Label();
            axis4StatusLabel = new Label();
            controlGroup = new GroupBox();
            controlLayout = new TableLayoutPanel();
            selectedAxisCaptionLabel = new Label();
            axisSelector = new ComboBox();
            selectedStatusCaptionLabel = new Label();
            selectedStatusLabel = new Label();
            selectedActualCaptionLabel = new Label();
            selectedActualLabel = new Label();
            selectedSpeedCaptionLabel = new Label();
            selectedSpeedLabel = new Label();
            jogSpeedCaptionLabel = new Label();
            jogSpeedInput = new NumericUpDown();
            jogSectionLabel = new Label();
            jogNegativeButton = new Button();
            jogPositiveButton = new Button();
            singleAxisSectionLabel = new Label();
            homeSelectedButton = new Button();
            stopSelectedButton = new Button();
            helperLabel = new Label();
            axis1FeedbackLayout.SuspendLayout();
            axis2FeedbackLayout.SuspendLayout();
            axis3FeedbackLayout.SuspendLayout();
            axis4FeedbackLayout.SuspendLayout();
            rootLayout.SuspendLayout();
            headerPanel.SuspendLayout();
            globalActionsPanel.SuspendLayout();
            contentLayout.SuspendLayout();
            overviewGroup.SuspendLayout();
            axisOverviewLayout.SuspendLayout();
            controlGroup.SuspendLayout();
            controlLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)jogSpeedInput).BeginInit();
            SuspendLayout();
            // 
            // axis1FeedbackLayout
            // 
            axis1FeedbackLayout.ColumnCount = 3;
            axis1FeedbackLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            axis1FeedbackLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            axis1FeedbackLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            axis1FeedbackLayout.Controls.Add(axis1PositionIndicator, 0, 0);
            axis1FeedbackLayout.Controls.Add(axis1NegativeLimitLamp, 0, 1);
            axis1FeedbackLayout.Controls.Add(axis1OriginLamp, 1, 1);
            axis1FeedbackLayout.Controls.Add(axis1PositiveLimitLamp, 2, 1);
            axis1FeedbackLayout.Dock = DockStyle.Fill;
            axis1FeedbackLayout.Location = new Point(45, 26);
            axis1FeedbackLayout.Margin = new Padding(0);
            axis1FeedbackLayout.Name = "axis1FeedbackLayout";
            axis1FeedbackLayout.RowCount = 2;
            axis1FeedbackLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            axis1FeedbackLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            axis1FeedbackLayout.Size = new Size(269, 95);
            axis1FeedbackLayout.TabIndex = 4;
            // 
            // axis1PositionIndicator
            // 
            axis1PositionIndicator.ActualPosition = null;
            axis1PositionIndicator.BackColor = Color.White;
            axis1FeedbackLayout.SetColumnSpan(axis1PositionIndicator, 3);
            axis1PositionIndicator.Dock = DockStyle.Fill;
            axis1PositionIndicator.ForeColor = Color.FromArgb(31, 41, 55);
            axis1PositionIndicator.HasAlarm = false;
            axis1PositionIndicator.IsConnected = false;
            axis1PositionIndicator.Location = new Point(3, 5);
            axis1PositionIndicator.Margin = new Padding(3, 5, 3, 5);
            axis1PositionIndicator.MaximumPosition = 20D;
            axis1PositionIndicator.MinimumPosition = -20D;
            axis1PositionIndicator.MinimumSize = new Size(140, 56);
            axis1PositionIndicator.Name = "axis1PositionIndicator";
            axis1PositionIndicator.NegativeLimit = false;
            axis1PositionIndicator.PositiveLimit = false;
            axis1PositionIndicator.Size = new Size(263, 61);
            axis1PositionIndicator.TabIndex = 4;
            axis1PositionIndicator.TabStop = false;
            axis1PositionIndicator.UnitText = "°";
            // 
            // axis1NegativeLimitLamp
            // 
            axis1NegativeLimitLamp.BackColor = Color.FromArgb(241, 245, 249);
            axis1NegativeLimitLamp.Dock = DockStyle.Fill;
            axis1NegativeLimitLamp.Font = new Font("Microsoft YaHei UI", 8F);
            axis1NegativeLimitLamp.ForeColor = Color.Gray;
            axis1NegativeLimitLamp.Location = new Point(2, 73);
            axis1NegativeLimitLamp.Margin = new Padding(2);
            axis1NegativeLimitLamp.Name = "axis1NegativeLimitLamp";
            axis1NegativeLimitLamp.Size = new Size(84, 20);
            axis1NegativeLimitLamp.TabIndex = 5;
            axis1NegativeLimitLamp.Text = "● 负限位 --";
            axis1NegativeLimitLamp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis1OriginLamp
            // 
            axis1OriginLamp.BackColor = Color.FromArgb(241, 245, 249);
            axis1OriginLamp.Dock = DockStyle.Fill;
            axis1OriginLamp.Font = new Font("Microsoft YaHei UI", 8F);
            axis1OriginLamp.ForeColor = Color.Gray;
            axis1OriginLamp.Location = new Point(90, 73);
            axis1OriginLamp.Margin = new Padding(2);
            axis1OriginLamp.Name = "axis1OriginLamp";
            axis1OriginLamp.Size = new Size(84, 20);
            axis1OriginLamp.TabIndex = 6;
            axis1OriginLamp.Text = "● 原点 --";
            axis1OriginLamp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis1PositiveLimitLamp
            // 
            axis1PositiveLimitLamp.BackColor = Color.FromArgb(241, 245, 249);
            axis1PositiveLimitLamp.Dock = DockStyle.Fill;
            axis1PositiveLimitLamp.Font = new Font("Microsoft YaHei UI", 8F);
            axis1PositiveLimitLamp.ForeColor = Color.Gray;
            axis1PositiveLimitLamp.Location = new Point(178, 73);
            axis1PositiveLimitLamp.Margin = new Padding(2);
            axis1PositiveLimitLamp.Name = "axis1PositiveLimitLamp";
            axis1PositiveLimitLamp.Size = new Size(89, 20);
            axis1PositiveLimitLamp.TabIndex = 7;
            axis1PositiveLimitLamp.Text = "● 正限位 --";
            axis1PositiveLimitLamp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis2FeedbackLayout
            // 
            axis2FeedbackLayout.ColumnCount = 3;
            axis2FeedbackLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            axis2FeedbackLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            axis2FeedbackLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            axis2FeedbackLayout.Controls.Add(axis2PositionIndicator, 0, 0);
            axis2FeedbackLayout.Controls.Add(axis2NegativeLimitLamp, 0, 1);
            axis2FeedbackLayout.Controls.Add(axis2OriginLamp, 1, 1);
            axis2FeedbackLayout.Controls.Add(axis2PositiveLimitLamp, 2, 1);
            axis2FeedbackLayout.Dock = DockStyle.Fill;
            axis2FeedbackLayout.Location = new Point(45, 121);
            axis2FeedbackLayout.Margin = new Padding(0);
            axis2FeedbackLayout.Name = "axis2FeedbackLayout";
            axis2FeedbackLayout.RowCount = 2;
            axis2FeedbackLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            axis2FeedbackLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            axis2FeedbackLayout.Size = new Size(269, 95);
            axis2FeedbackLayout.TabIndex = 7;
            // 
            // axis2PositionIndicator
            // 
            axis2PositionIndicator.ActualPosition = null;
            axis2PositionIndicator.BackColor = Color.White;
            axis2FeedbackLayout.SetColumnSpan(axis2PositionIndicator, 3);
            axis2PositionIndicator.Dock = DockStyle.Fill;
            axis2PositionIndicator.ForeColor = Color.FromArgb(31, 41, 55);
            axis2PositionIndicator.HasAlarm = false;
            axis2PositionIndicator.IsConnected = false;
            axis2PositionIndicator.Location = new Point(3, 5);
            axis2PositionIndicator.Margin = new Padding(3, 5, 3, 5);
            axis2PositionIndicator.MaximumPosition = 20D;
            axis2PositionIndicator.MinimumPosition = -20D;
            axis2PositionIndicator.MinimumSize = new Size(140, 56);
            axis2PositionIndicator.Name = "axis2PositionIndicator";
            axis2PositionIndicator.NegativeLimit = false;
            axis2PositionIndicator.PositiveLimit = false;
            axis2PositionIndicator.Size = new Size(263, 61);
            axis2PositionIndicator.TabIndex = 7;
            axis2PositionIndicator.TabStop = false;
            axis2PositionIndicator.UnitText = "°";
            // 
            // axis2NegativeLimitLamp
            // 
            axis2NegativeLimitLamp.BackColor = Color.FromArgb(241, 245, 249);
            axis2NegativeLimitLamp.Dock = DockStyle.Fill;
            axis2NegativeLimitLamp.Font = new Font("Microsoft YaHei UI", 8F);
            axis2NegativeLimitLamp.ForeColor = Color.Gray;
            axis2NegativeLimitLamp.Location = new Point(2, 73);
            axis2NegativeLimitLamp.Margin = new Padding(2);
            axis2NegativeLimitLamp.Name = "axis2NegativeLimitLamp";
            axis2NegativeLimitLamp.Size = new Size(84, 20);
            axis2NegativeLimitLamp.TabIndex = 8;
            axis2NegativeLimitLamp.Text = "● 负限位 --";
            axis2NegativeLimitLamp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis2OriginLamp
            // 
            axis2OriginLamp.BackColor = Color.FromArgb(241, 245, 249);
            axis2OriginLamp.Dock = DockStyle.Fill;
            axis2OriginLamp.Font = new Font("Microsoft YaHei UI", 8F);
            axis2OriginLamp.ForeColor = Color.Gray;
            axis2OriginLamp.Location = new Point(90, 73);
            axis2OriginLamp.Margin = new Padding(2);
            axis2OriginLamp.Name = "axis2OriginLamp";
            axis2OriginLamp.Size = new Size(84, 20);
            axis2OriginLamp.TabIndex = 9;
            axis2OriginLamp.Text = "● 原点 --";
            axis2OriginLamp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis2PositiveLimitLamp
            // 
            axis2PositiveLimitLamp.BackColor = Color.FromArgb(241, 245, 249);
            axis2PositiveLimitLamp.Dock = DockStyle.Fill;
            axis2PositiveLimitLamp.Font = new Font("Microsoft YaHei UI", 8F);
            axis2PositiveLimitLamp.ForeColor = Color.Gray;
            axis2PositiveLimitLamp.Location = new Point(178, 73);
            axis2PositiveLimitLamp.Margin = new Padding(2);
            axis2PositiveLimitLamp.Name = "axis2PositiveLimitLamp";
            axis2PositiveLimitLamp.Size = new Size(89, 20);
            axis2PositiveLimitLamp.TabIndex = 10;
            axis2PositiveLimitLamp.Text = "● 正限位 --";
            axis2PositiveLimitLamp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis3FeedbackLayout
            // 
            axis3FeedbackLayout.ColumnCount = 3;
            axis3FeedbackLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            axis3FeedbackLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            axis3FeedbackLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            axis3FeedbackLayout.Controls.Add(axis3PositionIndicator, 0, 0);
            axis3FeedbackLayout.Controls.Add(axis3NegativeLimitLamp, 0, 1);
            axis3FeedbackLayout.Controls.Add(axis3OriginLamp, 1, 1);
            axis3FeedbackLayout.Controls.Add(axis3PositiveLimitLamp, 2, 1);
            axis3FeedbackLayout.Dock = DockStyle.Fill;
            axis3FeedbackLayout.Location = new Point(45, 216);
            axis3FeedbackLayout.Margin = new Padding(0);
            axis3FeedbackLayout.Name = "axis3FeedbackLayout";
            axis3FeedbackLayout.RowCount = 2;
            axis3FeedbackLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            axis3FeedbackLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            axis3FeedbackLayout.Size = new Size(269, 95);
            axis3FeedbackLayout.TabIndex = 10;
            // 
            // axis3PositionIndicator
            // 
            axis3PositionIndicator.ActualPosition = null;
            axis3PositionIndicator.BackColor = Color.White;
            axis3FeedbackLayout.SetColumnSpan(axis3PositionIndicator, 3);
            axis3PositionIndicator.Dock = DockStyle.Fill;
            axis3PositionIndicator.ForeColor = Color.FromArgb(31, 41, 55);
            axis3PositionIndicator.HasAlarm = false;
            axis3PositionIndicator.IsConnected = false;
            axis3PositionIndicator.Location = new Point(3, 5);
            axis3PositionIndicator.Margin = new Padding(3, 5, 3, 5);
            axis3PositionIndicator.MaximumPosition = 20D;
            axis3PositionIndicator.MinimumPosition = -20D;
            axis3PositionIndicator.MinimumSize = new Size(140, 56);
            axis3PositionIndicator.Name = "axis3PositionIndicator";
            axis3PositionIndicator.NegativeLimit = false;
            axis3PositionIndicator.PositiveLimit = false;
            axis3PositionIndicator.Size = new Size(263, 61);
            axis3PositionIndicator.TabIndex = 10;
            axis3PositionIndicator.TabStop = false;
            axis3PositionIndicator.UnitText = "°";
            // 
            // axis3NegativeLimitLamp
            // 
            axis3NegativeLimitLamp.BackColor = Color.FromArgb(241, 245, 249);
            axis3NegativeLimitLamp.Dock = DockStyle.Fill;
            axis3NegativeLimitLamp.Font = new Font("Microsoft YaHei UI", 8F);
            axis3NegativeLimitLamp.ForeColor = Color.Gray;
            axis3NegativeLimitLamp.Location = new Point(2, 73);
            axis3NegativeLimitLamp.Margin = new Padding(2);
            axis3NegativeLimitLamp.Name = "axis3NegativeLimitLamp";
            axis3NegativeLimitLamp.Size = new Size(84, 20);
            axis3NegativeLimitLamp.TabIndex = 11;
            axis3NegativeLimitLamp.Text = "● 负限位 --";
            axis3NegativeLimitLamp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis3OriginLamp
            // 
            axis3OriginLamp.BackColor = Color.FromArgb(241, 245, 249);
            axis3OriginLamp.Dock = DockStyle.Fill;
            axis3OriginLamp.Font = new Font("Microsoft YaHei UI", 8F);
            axis3OriginLamp.ForeColor = Color.Gray;
            axis3OriginLamp.Location = new Point(90, 73);
            axis3OriginLamp.Margin = new Padding(2);
            axis3OriginLamp.Name = "axis3OriginLamp";
            axis3OriginLamp.Size = new Size(84, 20);
            axis3OriginLamp.TabIndex = 12;
            axis3OriginLamp.Text = "● 原点 --";
            axis3OriginLamp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis3PositiveLimitLamp
            // 
            axis3PositiveLimitLamp.BackColor = Color.FromArgb(241, 245, 249);
            axis3PositiveLimitLamp.Dock = DockStyle.Fill;
            axis3PositiveLimitLamp.Font = new Font("Microsoft YaHei UI", 8F);
            axis3PositiveLimitLamp.ForeColor = Color.Gray;
            axis3PositiveLimitLamp.Location = new Point(178, 73);
            axis3PositiveLimitLamp.Margin = new Padding(2);
            axis3PositiveLimitLamp.Name = "axis3PositiveLimitLamp";
            axis3PositiveLimitLamp.Size = new Size(89, 20);
            axis3PositiveLimitLamp.TabIndex = 13;
            axis3PositiveLimitLamp.Text = "● 正限位 --";
            axis3PositiveLimitLamp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis4FeedbackLayout
            // 
            axis4FeedbackLayout.ColumnCount = 3;
            axis4FeedbackLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            axis4FeedbackLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            axis4FeedbackLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            axis4FeedbackLayout.Controls.Add(axis4PositionIndicator, 0, 0);
            axis4FeedbackLayout.Controls.Add(axis4NegativeLimitLamp, 0, 1);
            axis4FeedbackLayout.Controls.Add(axis4OriginLamp, 1, 1);
            axis4FeedbackLayout.Controls.Add(axis4PositiveLimitLamp, 2, 1);
            axis4FeedbackLayout.Dock = DockStyle.Fill;
            axis4FeedbackLayout.Location = new Point(45, 311);
            axis4FeedbackLayout.Margin = new Padding(0);
            axis4FeedbackLayout.Name = "axis4FeedbackLayout";
            axis4FeedbackLayout.RowCount = 2;
            axis4FeedbackLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            axis4FeedbackLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            axis4FeedbackLayout.Size = new Size(269, 97);
            axis4FeedbackLayout.TabIndex = 13;
            // 
            // axis4PositionIndicator
            // 
            axis4PositionIndicator.ActualPosition = null;
            axis4PositionIndicator.BackColor = Color.White;
            axis4FeedbackLayout.SetColumnSpan(axis4PositionIndicator, 3);
            axis4PositionIndicator.Dock = DockStyle.Fill;
            axis4PositionIndicator.ForeColor = Color.FromArgb(31, 41, 55);
            axis4PositionIndicator.HasAlarm = false;
            axis4PositionIndicator.IsConnected = false;
            axis4PositionIndicator.Location = new Point(3, 5);
            axis4PositionIndicator.Margin = new Padding(3, 5, 3, 5);
            axis4PositionIndicator.MaximumPosition = 20D;
            axis4PositionIndicator.MinimumPosition = -20D;
            axis4PositionIndicator.MinimumSize = new Size(140, 56);
            axis4PositionIndicator.Name = "axis4PositionIndicator";
            axis4PositionIndicator.NegativeLimit = false;
            axis4PositionIndicator.PositiveLimit = false;
            axis4PositionIndicator.Size = new Size(263, 63);
            axis4PositionIndicator.TabIndex = 13;
            axis4PositionIndicator.TabStop = false;
            axis4PositionIndicator.UnitText = "°";
            // 
            // axis4NegativeLimitLamp
            // 
            axis4NegativeLimitLamp.BackColor = Color.FromArgb(241, 245, 249);
            axis4NegativeLimitLamp.Dock = DockStyle.Fill;
            axis4NegativeLimitLamp.Font = new Font("Microsoft YaHei UI", 8F);
            axis4NegativeLimitLamp.ForeColor = Color.Gray;
            axis4NegativeLimitLamp.Location = new Point(2, 75);
            axis4NegativeLimitLamp.Margin = new Padding(2);
            axis4NegativeLimitLamp.Name = "axis4NegativeLimitLamp";
            axis4NegativeLimitLamp.Size = new Size(84, 20);
            axis4NegativeLimitLamp.TabIndex = 14;
            axis4NegativeLimitLamp.Text = "● 负限位 --";
            axis4NegativeLimitLamp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis4OriginLamp
            // 
            axis4OriginLamp.BackColor = Color.FromArgb(241, 245, 249);
            axis4OriginLamp.Dock = DockStyle.Fill;
            axis4OriginLamp.Font = new Font("Microsoft YaHei UI", 8F);
            axis4OriginLamp.ForeColor = Color.Gray;
            axis4OriginLamp.Location = new Point(90, 75);
            axis4OriginLamp.Margin = new Padding(2);
            axis4OriginLamp.Name = "axis4OriginLamp";
            axis4OriginLamp.Size = new Size(84, 20);
            axis4OriginLamp.TabIndex = 15;
            axis4OriginLamp.Text = "● 原点 --";
            axis4OriginLamp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis4PositiveLimitLamp
            // 
            axis4PositiveLimitLamp.BackColor = Color.FromArgb(241, 245, 249);
            axis4PositiveLimitLamp.Dock = DockStyle.Fill;
            axis4PositiveLimitLamp.Font = new Font("Microsoft YaHei UI", 8F);
            axis4PositiveLimitLamp.ForeColor = Color.Gray;
            axis4PositiveLimitLamp.Location = new Point(178, 75);
            axis4PositiveLimitLamp.Margin = new Padding(2);
            axis4PositiveLimitLamp.Name = "axis4PositiveLimitLamp";
            axis4PositiveLimitLamp.Size = new Size(89, 20);
            axis4PositiveLimitLamp.TabIndex = 16;
            axis4PositiveLimitLamp.Text = "● 正限位 --";
            axis4PositiveLimitLamp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // rootLayout
            // 
            rootLayout.BackColor = Color.FromArgb(241, 245, 249);
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(headerPanel, 0, 0);
            rootLayout.Controls.Add(globalActionsPanel, 0, 1);
            rootLayout.Controls.Add(contentLayout, 0, 2);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Margin = new Padding(2, 3, 2, 3);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(11, 12, 11, 12);
            rootLayout.RowCount = 3;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.Size = new Size(704, 579);
            rootLayout.TabIndex = 0;
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.White;
            headerPanel.Controls.Add(connectionStateLabel);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Location = new Point(13, 15);
            headerPanel.Margin = new Padding(2, 3, 2, 3);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new Padding(11, 0, 11, 0);
            headerPanel.Size = new Size(678, 42);
            headerPanel.TabIndex = 0;
            // 
            // connectionStateLabel
            // 
            connectionStateLabel.Dock = DockStyle.Right;
            connectionStateLabel.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            connectionStateLabel.ForeColor = Color.FromArgb(5, 150, 105);
            connectionStateLabel.Location = new Point(543, 0);
            connectionStateLabel.Margin = new Padding(2, 0, 2, 0);
            connectionStateLabel.Name = "connectionStateLabel";
            connectionStateLabel.Size = new Size(124, 42);
            connectionStateLabel.TabIndex = 1;
            connectionStateLabel.Text = "ADS 未连接";
            connectionStateLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // titleLabel
            // 
            titleLabel.Dock = DockStyle.Left;
            titleLabel.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            titleLabel.Location = new Point(11, 0);
            titleLabel.Margin = new Padding(2, 0, 2, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(233, 42);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "造波板轴 · 手动控制";
            titleLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // globalActionsPanel
            // 
            globalActionsPanel.Controls.Add(enableAllButton);
            globalActionsPanel.Controls.Add(disableAllButton);
            globalActionsPanel.Controls.Add(resetAlarmButton);
            globalActionsPanel.Controls.Add(homeAllButton);
            globalActionsPanel.Controls.Add(stopAllButton);
            globalActionsPanel.Dock = DockStyle.Fill;
            globalActionsPanel.Location = new Point(13, 63);
            globalActionsPanel.Margin = new Padding(2, 3, 2, 3);
            globalActionsPanel.Name = "globalActionsPanel";
            globalActionsPanel.Padding = new Padding(0, 7, 0, 0);
            globalActionsPanel.Size = new Size(678, 42);
            globalActionsPanel.TabIndex = 1;
            globalActionsPanel.WrapContents = false;
            // 
            // enableAllButton
            // 
            enableAllButton.BackColor = Color.FromArgb(5, 150, 105);
            enableAllButton.FlatAppearance.BorderSize = 0;
            enableAllButton.FlatStyle = FlatStyle.Flat;
            enableAllButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            enableAllButton.ForeColor = Color.White;
            enableAllButton.Location = new Point(0, 7);
            enableAllButton.Margin = new Padding(0, 0, 8, 0);
            enableAllButton.Name = "enableAllButton";
            enableAllButton.Size = new Size(98, 32);
            enableAllButton.TabIndex = 0;
            enableAllButton.Text = "全部使能";
            enableAllButton.UseVisualStyleBackColor = false;
            enableAllButton.Click += EnableAllButton_Click;
            // 
            // disableAllButton
            // 
            disableAllButton.BackColor = Color.FromArgb(180, 83, 9);
            disableAllButton.FlatAppearance.BorderSize = 0;
            disableAllButton.FlatStyle = FlatStyle.Flat;
            disableAllButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            disableAllButton.ForeColor = Color.White;
            disableAllButton.Location = new Point(106, 7);
            disableAllButton.Margin = new Padding(0, 0, 8, 0);
            disableAllButton.Name = "disableAllButton";
            disableAllButton.Size = new Size(98, 32);
            disableAllButton.TabIndex = 1;
            disableAllButton.Text = "取消使能";
            disableAllButton.UseVisualStyleBackColor = false;
            disableAllButton.Click += DisableAllButton_Click;
            // 
            // resetAlarmButton
            // 
            resetAlarmButton.BackColor = Color.FromArgb(37, 99, 235);
            resetAlarmButton.FlatAppearance.BorderSize = 0;
            resetAlarmButton.FlatStyle = FlatStyle.Flat;
            resetAlarmButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            resetAlarmButton.ForeColor = Color.White;
            resetAlarmButton.Location = new Point(212, 7);
            resetAlarmButton.Margin = new Padding(0, 0, 8, 0);
            resetAlarmButton.Name = "resetAlarmButton";
            resetAlarmButton.Size = new Size(98, 32);
            resetAlarmButton.TabIndex = 2;
            resetAlarmButton.Text = "全部复位报警";
            resetAlarmButton.UseVisualStyleBackColor = false;
            resetAlarmButton.Click += ResetAlarmButton_Click;
            // 
            // homeAllButton
            // 
            homeAllButton.BackColor = Color.FromArgb(71, 85, 105);
            homeAllButton.FlatAppearance.BorderSize = 0;
            homeAllButton.FlatStyle = FlatStyle.Flat;
            homeAllButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            homeAllButton.ForeColor = Color.White;
            homeAllButton.Location = new Point(318, 7);
            homeAllButton.Margin = new Padding(0, 0, 8, 0);
            homeAllButton.Name = "homeAllButton";
            homeAllButton.Size = new Size(98, 32);
            homeAllButton.TabIndex = 3;
            homeAllButton.Text = "全部回零";
            homeAllButton.UseVisualStyleBackColor = false;
            homeAllButton.Click += HomeAllButton_Click;
            // 
            // stopAllButton
            // 
            stopAllButton.BackColor = Color.FromArgb(220, 38, 38);
            stopAllButton.FlatAppearance.BorderSize = 0;
            stopAllButton.FlatStyle = FlatStyle.Flat;
            stopAllButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            stopAllButton.ForeColor = Color.White;
            stopAllButton.Location = new Point(424, 7);
            stopAllButton.Margin = new Padding(0, 0, 8, 0);
            stopAllButton.Name = "stopAllButton";
            stopAllButton.Size = new Size(98, 32);
            stopAllButton.TabIndex = 4;
            stopAllButton.Text = "全部停止";
            stopAllButton.UseVisualStyleBackColor = false;
            stopAllButton.Click += StopAllButton_Click;
            // 
            // contentLayout
            // 
            contentLayout.ColumnCount = 2;
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            contentLayout.Controls.Add(overviewGroup, 0, 0);
            contentLayout.Controls.Add(controlGroup, 1, 0);
            contentLayout.Dock = DockStyle.Fill;
            contentLayout.Location = new Point(13, 111);
            contentLayout.Margin = new Padding(2, 3, 2, 3);
            contentLayout.Name = "contentLayout";
            contentLayout.RowCount = 1;
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            contentLayout.Size = new Size(678, 453);
            contentLayout.TabIndex = 2;
            // 
            // overviewGroup
            // 
            overviewGroup.Controls.Add(axisOverviewLayout);
            overviewGroup.Dock = DockStyle.Fill;
            overviewGroup.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            overviewGroup.ForeColor = Color.FromArgb(15, 23, 42);
            overviewGroup.Location = new Point(2, 3);
            overviewGroup.Margin = new Padding(2, 3, 2, 3);
            overviewGroup.Name = "overviewGroup";
            overviewGroup.Padding = new Padding(8, 14, 8, 8);
            overviewGroup.Size = new Size(389, 447);
            overviewGroup.TabIndex = 0;
            overviewGroup.TabStop = false;
            overviewGroup.Text = "四轴位置总览";
            // 
            // axisOverviewLayout
            // 
            axisOverviewLayout.BackColor = Color.White;
            axisOverviewLayout.ColumnCount = 3;
            axisOverviewLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45F));
            axisOverviewLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            axisOverviewLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 59F));
            axisOverviewLayout.Controls.Add(axisHeaderLabel, 0, 0);
            axisOverviewLayout.Controls.Add(positionHeaderLabel, 1, 0);
            axisOverviewLayout.Controls.Add(statusHeaderLabel, 2, 0);
            axisOverviewLayout.Controls.Add(axis1NameLabel, 0, 1);
            axisOverviewLayout.Controls.Add(axis1FeedbackLayout, 1, 1);
            axisOverviewLayout.Controls.Add(axis1StatusLabel, 2, 1);
            axisOverviewLayout.Controls.Add(axis2NameLabel, 0, 2);
            axisOverviewLayout.Controls.Add(axis2FeedbackLayout, 1, 2);
            axisOverviewLayout.Controls.Add(axis2StatusLabel, 2, 2);
            axisOverviewLayout.Controls.Add(axis3NameLabel, 0, 3);
            axisOverviewLayout.Controls.Add(axis3FeedbackLayout, 1, 3);
            axisOverviewLayout.Controls.Add(axis3StatusLabel, 2, 3);
            axisOverviewLayout.Controls.Add(axis4NameLabel, 0, 4);
            axisOverviewLayout.Controls.Add(axis4FeedbackLayout, 1, 4);
            axisOverviewLayout.Controls.Add(axis4StatusLabel, 2, 4);
            axisOverviewLayout.Dock = DockStyle.Fill;
            axisOverviewLayout.Location = new Point(8, 31);
            axisOverviewLayout.Margin = new Padding(2, 3, 2, 3);
            axisOverviewLayout.Name = "axisOverviewLayout";
            axisOverviewLayout.RowCount = 5;
            axisOverviewLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            axisOverviewLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            axisOverviewLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            axisOverviewLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            axisOverviewLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            axisOverviewLayout.Size = new Size(373, 408);
            axisOverviewLayout.TabIndex = 0;
            // 
            // axisHeaderLabel
            // 
            axisHeaderLabel.Dock = DockStyle.Fill;
            axisHeaderLabel.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            axisHeaderLabel.ForeColor = Color.FromArgb(71, 85, 105);
            axisHeaderLabel.Location = new Point(2, 0);
            axisHeaderLabel.Margin = new Padding(2, 0, 2, 0);
            axisHeaderLabel.Name = "axisHeaderLabel";
            axisHeaderLabel.Size = new Size(41, 26);
            axisHeaderLabel.TabIndex = 0;
            axisHeaderLabel.Text = "轴号";
            axisHeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // positionHeaderLabel
            // 
            positionHeaderLabel.Dock = DockStyle.Fill;
            positionHeaderLabel.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            positionHeaderLabel.ForeColor = Color.FromArgb(71, 85, 105);
            positionHeaderLabel.Location = new Point(47, 0);
            positionHeaderLabel.Margin = new Padding(2, 0, 2, 0);
            positionHeaderLabel.Name = "positionHeaderLabel";
            positionHeaderLabel.Size = new Size(265, 26);
            positionHeaderLabel.TabIndex = 1;
            positionHeaderLabel.Text = "当前位置（倍福 PLC 实际位置）";
            positionHeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // statusHeaderLabel
            // 
            statusHeaderLabel.Dock = DockStyle.Fill;
            statusHeaderLabel.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            statusHeaderLabel.ForeColor = Color.FromArgb(71, 85, 105);
            statusHeaderLabel.Location = new Point(316, 0);
            statusHeaderLabel.Margin = new Padding(2, 0, 2, 0);
            statusHeaderLabel.Name = "statusHeaderLabel";
            statusHeaderLabel.Size = new Size(55, 26);
            statusHeaderLabel.TabIndex = 2;
            statusHeaderLabel.Text = "状态";
            statusHeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis1NameLabel
            // 
            axis1NameLabel.Dock = DockStyle.Fill;
            axis1NameLabel.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            axis1NameLabel.ForeColor = Color.FromArgb(15, 23, 42);
            axis1NameLabel.Location = new Point(2, 26);
            axis1NameLabel.Margin = new Padding(2, 0, 2, 0);
            axis1NameLabel.Name = "axis1NameLabel";
            axis1NameLabel.Size = new Size(41, 95);
            axis1NameLabel.TabIndex = 3;
            axis1NameLabel.Text = "轴 1";
            axis1NameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis1StatusLabel
            // 
            axis1StatusLabel.Dock = DockStyle.Fill;
            axis1StatusLabel.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            axis1StatusLabel.ForeColor = Color.Gray;
            axis1StatusLabel.Location = new Point(316, 26);
            axis1StatusLabel.Margin = new Padding(2, 0, 2, 0);
            axis1StatusLabel.Name = "axis1StatusLabel";
            axis1StatusLabel.Size = new Size(55, 95);
            axis1StatusLabel.TabIndex = 5;
            axis1StatusLabel.Text = "未连接";
            axis1StatusLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis2NameLabel
            // 
            axis2NameLabel.Dock = DockStyle.Fill;
            axis2NameLabel.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            axis2NameLabel.ForeColor = Color.FromArgb(15, 23, 42);
            axis2NameLabel.Location = new Point(2, 121);
            axis2NameLabel.Margin = new Padding(2, 0, 2, 0);
            axis2NameLabel.Name = "axis2NameLabel";
            axis2NameLabel.Size = new Size(41, 95);
            axis2NameLabel.TabIndex = 6;
            axis2NameLabel.Text = "轴 2";
            axis2NameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis2StatusLabel
            // 
            axis2StatusLabel.Dock = DockStyle.Fill;
            axis2StatusLabel.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            axis2StatusLabel.ForeColor = Color.Gray;
            axis2StatusLabel.Location = new Point(316, 121);
            axis2StatusLabel.Margin = new Padding(2, 0, 2, 0);
            axis2StatusLabel.Name = "axis2StatusLabel";
            axis2StatusLabel.Size = new Size(55, 95);
            axis2StatusLabel.TabIndex = 8;
            axis2StatusLabel.Text = "未连接";
            axis2StatusLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis3NameLabel
            // 
            axis3NameLabel.Dock = DockStyle.Fill;
            axis3NameLabel.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            axis3NameLabel.ForeColor = Color.FromArgb(15, 23, 42);
            axis3NameLabel.Location = new Point(2, 216);
            axis3NameLabel.Margin = new Padding(2, 0, 2, 0);
            axis3NameLabel.Name = "axis3NameLabel";
            axis3NameLabel.Size = new Size(41, 95);
            axis3NameLabel.TabIndex = 9;
            axis3NameLabel.Text = "轴 3";
            axis3NameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis3StatusLabel
            // 
            axis3StatusLabel.Dock = DockStyle.Fill;
            axis3StatusLabel.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            axis3StatusLabel.ForeColor = Color.Gray;
            axis3StatusLabel.Location = new Point(316, 216);
            axis3StatusLabel.Margin = new Padding(2, 0, 2, 0);
            axis3StatusLabel.Name = "axis3StatusLabel";
            axis3StatusLabel.Size = new Size(55, 95);
            axis3StatusLabel.TabIndex = 11;
            axis3StatusLabel.Text = "未连接";
            axis3StatusLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis4NameLabel
            // 
            axis4NameLabel.Dock = DockStyle.Fill;
            axis4NameLabel.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            axis4NameLabel.ForeColor = Color.FromArgb(15, 23, 42);
            axis4NameLabel.Location = new Point(2, 311);
            axis4NameLabel.Margin = new Padding(2, 0, 2, 0);
            axis4NameLabel.Name = "axis4NameLabel";
            axis4NameLabel.Size = new Size(41, 97);
            axis4NameLabel.TabIndex = 12;
            axis4NameLabel.Text = "轴 4";
            axis4NameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // axis4StatusLabel
            // 
            axis4StatusLabel.Dock = DockStyle.Fill;
            axis4StatusLabel.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            axis4StatusLabel.ForeColor = Color.Gray;
            axis4StatusLabel.Location = new Point(316, 311);
            axis4StatusLabel.Margin = new Padding(2, 0, 2, 0);
            axis4StatusLabel.Name = "axis4StatusLabel";
            axis4StatusLabel.Size = new Size(55, 97);
            axis4StatusLabel.TabIndex = 14;
            axis4StatusLabel.Text = "未连接";
            axis4StatusLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // controlGroup
            // 
            controlGroup.Controls.Add(controlLayout);
            controlGroup.Dock = DockStyle.Fill;
            controlGroup.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            controlGroup.ForeColor = Color.FromArgb(15, 23, 42);
            controlGroup.Location = new Point(395, 3);
            controlGroup.Margin = new Padding(2, 3, 2, 3);
            controlGroup.Name = "controlGroup";
            controlGroup.Padding = new Padding(9, 14, 9, 10);
            controlGroup.Size = new Size(281, 447);
            controlGroup.TabIndex = 1;
            controlGroup.TabStop = false;
            controlGroup.Text = "选定轴手动操作";
            // 
            // controlLayout
            // 
            controlLayout.ColumnCount = 2;
            controlLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            controlLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            controlLayout.Controls.Add(selectedAxisCaptionLabel, 0, 0);
            controlLayout.Controls.Add(axisSelector, 1, 0);
            controlLayout.Controls.Add(selectedStatusCaptionLabel, 0, 1);
            controlLayout.Controls.Add(selectedStatusLabel, 1, 1);
            controlLayout.Controls.Add(selectedActualCaptionLabel, 0, 2);
            controlLayout.Controls.Add(selectedActualLabel, 1, 2);
            controlLayout.Controls.Add(selectedSpeedCaptionLabel, 0, 3);
            controlLayout.Controls.Add(selectedSpeedLabel, 1, 3);
            controlLayout.Controls.Add(jogSpeedCaptionLabel, 0, 4);
            controlLayout.Controls.Add(jogSpeedInput, 1, 4);
            controlLayout.Controls.Add(jogSectionLabel, 0, 5);
            controlLayout.Controls.Add(jogNegativeButton, 0, 6);
            controlLayout.Controls.Add(jogPositiveButton, 1, 6);
            controlLayout.Controls.Add(singleAxisSectionLabel, 0, 7);
            controlLayout.Controls.Add(homeSelectedButton, 0, 8);
            controlLayout.Controls.Add(stopSelectedButton, 1, 8);
            controlLayout.Controls.Add(helperLabel, 0, 9);
            controlLayout.Dock = DockStyle.Fill;
            controlLayout.Location = new Point(9, 31);
            controlLayout.Margin = new Padding(2, 3, 2, 3);
            controlLayout.Name = "controlLayout";
            controlLayout.RowCount = 10;
            controlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            controlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            controlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            controlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            controlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            controlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            controlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            controlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            controlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            controlLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            controlLayout.Size = new Size(263, 406);
            controlLayout.TabIndex = 0;
            // 
            // selectedAxisCaptionLabel
            // 
            selectedAxisCaptionLabel.Dock = DockStyle.Fill;
            selectedAxisCaptionLabel.Font = new Font("Microsoft YaHei UI", 9F);
            selectedAxisCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            selectedAxisCaptionLabel.Location = new Point(2, 0);
            selectedAxisCaptionLabel.Margin = new Padding(2, 0, 2, 0);
            selectedAxisCaptionLabel.Name = "selectedAxisCaptionLabel";
            selectedAxisCaptionLabel.Size = new Size(127, 32);
            selectedAxisCaptionLabel.TabIndex = 0;
            selectedAxisCaptionLabel.Text = "控制轴";
            selectedAxisCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // axisSelector
            // 
            axisSelector.Dock = DockStyle.Fill;
            axisSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            axisSelector.Font = new Font("Microsoft YaHei UI", 10F);
            axisSelector.FormattingEnabled = true;
            axisSelector.Items.AddRange(new object[] { "轴 1", "轴 2", "轴 3", "轴 4" });
            axisSelector.Location = new Point(133, 3);
            axisSelector.Margin = new Padding(2, 3, 2, 3);
            axisSelector.Name = "axisSelector";
            axisSelector.Size = new Size(128, 27);
            axisSelector.TabIndex = 0;
            axisSelector.SelectedIndexChanged += AxisSelector_SelectedIndexChanged;
            // 
            // selectedStatusCaptionLabel
            // 
            selectedStatusCaptionLabel.Dock = DockStyle.Fill;
            selectedStatusCaptionLabel.Font = new Font("Microsoft YaHei UI", 9F);
            selectedStatusCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            selectedStatusCaptionLabel.Location = new Point(2, 32);
            selectedStatusCaptionLabel.Margin = new Padding(2, 0, 2, 0);
            selectedStatusCaptionLabel.Name = "selectedStatusCaptionLabel";
            selectedStatusCaptionLabel.Size = new Size(127, 27);
            selectedStatusCaptionLabel.TabIndex = 1;
            selectedStatusCaptionLabel.Text = "当前状态";
            selectedStatusCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // selectedStatusLabel
            // 
            selectedStatusLabel.Dock = DockStyle.Fill;
            selectedStatusLabel.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            selectedStatusLabel.ForeColor = Color.Gray;
            selectedStatusLabel.Location = new Point(133, 32);
            selectedStatusLabel.Margin = new Padding(2, 0, 2, 0);
            selectedStatusLabel.Name = "selectedStatusLabel";
            selectedStatusLabel.Size = new Size(128, 27);
            selectedStatusLabel.TabIndex = 2;
            selectedStatusLabel.Text = "未连接";
            selectedStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // selectedActualCaptionLabel
            // 
            selectedActualCaptionLabel.Dock = DockStyle.Fill;
            selectedActualCaptionLabel.Font = new Font("Microsoft YaHei UI", 9F);
            selectedActualCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            selectedActualCaptionLabel.Location = new Point(2, 59);
            selectedActualCaptionLabel.Margin = new Padding(2, 0, 2, 0);
            selectedActualCaptionLabel.Name = "selectedActualCaptionLabel";
            selectedActualCaptionLabel.Size = new Size(127, 27);
            selectedActualCaptionLabel.TabIndex = 3;
            selectedActualCaptionLabel.Text = "实际位置";
            selectedActualCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // selectedActualLabel
            // 
            selectedActualLabel.Dock = DockStyle.Fill;
            selectedActualLabel.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            selectedActualLabel.ForeColor = Color.FromArgb(14, 116, 144);
            selectedActualLabel.Location = new Point(133, 59);
            selectedActualLabel.Margin = new Padding(2, 0, 2, 0);
            selectedActualLabel.Name = "selectedActualLabel";
            selectedActualLabel.Size = new Size(128, 27);
            selectedActualLabel.TabIndex = 4;
            selectedActualLabel.Text = "--";
            selectedActualLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // selectedSpeedCaptionLabel
            // 
            selectedSpeedCaptionLabel.Dock = DockStyle.Fill;
            selectedSpeedCaptionLabel.Font = new Font("Microsoft YaHei UI", 9F);
            selectedSpeedCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            selectedSpeedCaptionLabel.Location = new Point(2, 86);
            selectedSpeedCaptionLabel.Margin = new Padding(2, 0, 2, 0);
            selectedSpeedCaptionLabel.Name = "selectedSpeedCaptionLabel";
            selectedSpeedCaptionLabel.Size = new Size(127, 27);
            selectedSpeedCaptionLabel.TabIndex = 7;
            selectedSpeedCaptionLabel.Text = "当前速度";
            selectedSpeedCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // selectedSpeedLabel
            // 
            selectedSpeedLabel.Dock = DockStyle.Fill;
            selectedSpeedLabel.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            selectedSpeedLabel.ForeColor = Color.FromArgb(71, 85, 105);
            selectedSpeedLabel.Location = new Point(133, 86);
            selectedSpeedLabel.Margin = new Padding(2, 0, 2, 0);
            selectedSpeedLabel.Name = "selectedSpeedLabel";
            selectedSpeedLabel.Size = new Size(128, 27);
            selectedSpeedLabel.TabIndex = 8;
            selectedSpeedLabel.Text = "--";
            selectedSpeedLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // jogSpeedCaptionLabel
            // 
            jogSpeedCaptionLabel.Dock = DockStyle.Fill;
            jogSpeedCaptionLabel.Font = new Font("Microsoft YaHei UI", 9F);
            jogSpeedCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            jogSpeedCaptionLabel.Location = new Point(2, 113);
            jogSpeedCaptionLabel.Margin = new Padding(2, 0, 2, 0);
            jogSpeedCaptionLabel.Name = "jogSpeedCaptionLabel";
            jogSpeedCaptionLabel.Size = new Size(127, 36);
            jogSpeedCaptionLabel.TabIndex = 9;
            jogSpeedCaptionLabel.Text = "点动速度";
            jogSpeedCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // jogSpeedInput
            // 
            jogSpeedInput.DecimalPlaces = 1;
            jogSpeedInput.Dock = DockStyle.Fill;
            jogSpeedInput.Font = new Font("Microsoft YaHei UI", 10F);
            jogSpeedInput.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            jogSpeedInput.Location = new Point(133, 116);
            jogSpeedInput.Margin = new Padding(2, 3, 2, 3);
            jogSpeedInput.Maximum = new decimal(new int[] { 180, 0, 0, 0 });
            jogSpeedInput.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            jogSpeedInput.Name = "jogSpeedInput";
            jogSpeedInput.Size = new Size(128, 24);
            jogSpeedInput.TabIndex = 1;
            jogSpeedInput.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // jogSectionLabel
            // 
            controlLayout.SetColumnSpan(jogSectionLabel, 2);
            jogSectionLabel.Dock = DockStyle.Fill;
            jogSectionLabel.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            jogSectionLabel.ForeColor = Color.FromArgb(14, 116, 144);
            jogSectionLabel.Location = new Point(2, 149);
            jogSectionLabel.Margin = new Padding(2, 0, 2, 0);
            jogSectionLabel.Name = "jogSectionLabel";
            jogSectionLabel.Padding = new Padding(0, 6, 0, 0);
            jogSectionLabel.Size = new Size(259, 27);
            jogSectionLabel.TabIndex = 10;
            jogSectionLabel.Text = "点动控制";
            jogSectionLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // jogNegativeButton
            // 
            jogNegativeButton.BackColor = Color.FromArgb(71, 85, 105);
            jogNegativeButton.Dock = DockStyle.Fill;
            jogNegativeButton.FlatAppearance.BorderSize = 0;
            jogNegativeButton.FlatStyle = FlatStyle.Flat;
            jogNegativeButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            jogNegativeButton.ForeColor = Color.White;
            jogNegativeButton.Location = new Point(3, 179);
            jogNegativeButton.Name = "jogNegativeButton";
            jogNegativeButton.Size = new Size(125, 40);
            jogNegativeButton.TabIndex = 11;
            jogNegativeButton.Text = "◀ 负向点动";
            jogNegativeButton.UseVisualStyleBackColor = false;
            jogNegativeButton.MouseDown += JogNegativeButton_MouseDown;
            jogNegativeButton.MouseLeave += JogButton_MouseUp;
            jogNegativeButton.MouseUp += JogButton_MouseUp;
            // 
            // jogPositiveButton
            // 
            jogPositiveButton.BackColor = Color.FromArgb(71, 85, 105);
            jogPositiveButton.Dock = DockStyle.Fill;
            jogPositiveButton.FlatAppearance.BorderSize = 0;
            jogPositiveButton.FlatStyle = FlatStyle.Flat;
            jogPositiveButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            jogPositiveButton.ForeColor = Color.White;
            jogPositiveButton.Location = new Point(134, 179);
            jogPositiveButton.Name = "jogPositiveButton";
            jogPositiveButton.Size = new Size(126, 40);
            jogPositiveButton.TabIndex = 12;
            jogPositiveButton.Text = "正向点动 ▶";
            jogPositiveButton.UseVisualStyleBackColor = false;
            jogPositiveButton.MouseDown += JogPositiveButton_MouseDown;
            jogPositiveButton.MouseLeave += JogButton_MouseUp;
            jogPositiveButton.MouseUp += JogButton_MouseUp;
            // 
            // singleAxisSectionLabel
            // 
            controlLayout.SetColumnSpan(singleAxisSectionLabel, 2);
            singleAxisSectionLabel.Dock = DockStyle.Fill;
            singleAxisSectionLabel.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            singleAxisSectionLabel.ForeColor = Color.FromArgb(14, 116, 144);
            singleAxisSectionLabel.Location = new Point(2, 222);
            singleAxisSectionLabel.Margin = new Padding(2, 0, 2, 0);
            singleAxisSectionLabel.Name = "singleAxisSectionLabel";
            singleAxisSectionLabel.Padding = new Padding(0, 6, 0, 0);
            singleAxisSectionLabel.Size = new Size(259, 27);
            singleAxisSectionLabel.TabIndex = 13;
            singleAxisSectionLabel.Text = "单轴动作";
            singleAxisSectionLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // homeSelectedButton
            // 
            homeSelectedButton.BackColor = Color.FromArgb(71, 85, 105);
            homeSelectedButton.Dock = DockStyle.Fill;
            homeSelectedButton.FlatAppearance.BorderSize = 0;
            homeSelectedButton.FlatStyle = FlatStyle.Flat;
            homeSelectedButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            homeSelectedButton.ForeColor = Color.White;
            homeSelectedButton.Location = new Point(3, 252);
            homeSelectedButton.Name = "homeSelectedButton";
            homeSelectedButton.Size = new Size(125, 40);
            homeSelectedButton.TabIndex = 14;
            homeSelectedButton.Text = "选定轴回零";
            homeSelectedButton.UseVisualStyleBackColor = false;
            homeSelectedButton.Click += HomeSelectedButton_Click;
            // 
            // stopSelectedButton
            // 
            stopSelectedButton.BackColor = Color.FromArgb(220, 38, 38);
            stopSelectedButton.Dock = DockStyle.Fill;
            stopSelectedButton.FlatAppearance.BorderSize = 0;
            stopSelectedButton.FlatStyle = FlatStyle.Flat;
            stopSelectedButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            stopSelectedButton.ForeColor = Color.White;
            stopSelectedButton.Location = new Point(134, 252);
            stopSelectedButton.Name = "stopSelectedButton";
            stopSelectedButton.Size = new Size(126, 40);
            stopSelectedButton.TabIndex = 15;
            stopSelectedButton.Text = "选定轴停止";
            stopSelectedButton.UseVisualStyleBackColor = false;
            stopSelectedButton.Click += StopSelectedButton_Click;
            // 
            // helperLabel
            // 
            controlLayout.SetColumnSpan(helperLabel, 2);
            helperLabel.Dock = DockStyle.Fill;
            helperLabel.Font = new Font("Microsoft YaHei UI", 9F);
            helperLabel.ForeColor = Color.FromArgb(100, 116, 139);
            helperLabel.Location = new Point(2, 295);
            helperLabel.Margin = new Padding(2, 0, 2, 0);
            helperLabel.Name = "helperLabel";
            helperLabel.Padding = new Padding(0, 10, 0, 0);
            helperLabel.Size = new Size(259, 111);
            helperLabel.TabIndex = 10;
            helperLabel.Text = "提示：点动按钮按下运行，松开立即停止。";
            // 
            // Manual
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            Controls.Add(rootLayout);
            Margin = new Padding(2, 3, 2, 3);
            Name = "Manual";
            Size = new Size(704, 579);
            axis1FeedbackLayout.ResumeLayout(false);
            axis2FeedbackLayout.ResumeLayout(false);
            axis3FeedbackLayout.ResumeLayout(false);
            axis4FeedbackLayout.ResumeLayout(false);
            rootLayout.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            globalActionsPanel.ResumeLayout(false);
            contentLayout.ResumeLayout(false);
            overviewGroup.ResumeLayout(false);
            axisOverviewLayout.ResumeLayout(false);
            controlGroup.ResumeLayout(false);
            controlLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)jogSpeedInput).EndInit();
            ResumeLayout(false);
        }

    }
}
