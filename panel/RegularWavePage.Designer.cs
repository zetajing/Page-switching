namespace Page_switching.panel
{
    partial class RegularWavePage
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pagePanel;
        private TableLayoutPanel regularLayout;
        private GroupBox regularParameterGroup;
        private TableLayoutPanel regularParameterLayout;
        private Label regularSegmentLabel;
        internal ComboBox regularSegmentComboBox;
        private Label regularTheoryLabel;
        internal ComboBox regularTheoryComboBox;
        private Label regularDepthLabel;
        internal TextBox regularDepthTextBox;
        private Label regularPeriodLabel;
        internal TextBox regularPeriodTextBox;
        private Label regularHeightLabel;
        internal TextBox regularHeightTextBox;
        private Label regularDirectionLabel;
        internal TextBox regularDirectionTextBox;
        private Label regularTimeStepLabel;
        internal TextBox regularTimeStepTextBox;
        private Label regularSampleCountLabel;
        internal TextBox regularSampleCountTextBox;
        private Label regularFrequencyLabel;
        internal TextBox regularCharacteristicFrequencyTextBox;
        private Label regularCharacteristicPeriodLabel;
        internal TextBox regularCharacteristicPeriodTextBox;
        private Label regularOutputLabel;
        internal TextBox regularOutputTextBox;
        internal Button regularBrowseOutputButton;
        internal Button regularGenerateButton;
        internal Label regularStatusLabel;
        private GroupBox regularPreviewGroup;
        internal WaveformPreviewControl regularPreview;

        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pagePanel = new Panel();
            regularLayout = new TableLayoutPanel();
            regularParameterGroup = new GroupBox();
            regularParameterLayout = new TableLayoutPanel();
            regularSegmentLabel = new Label();
            regularSegmentComboBox = new ComboBox();
            regularTheoryLabel = new Label();
            regularTheoryComboBox = new ComboBox();
            regularDepthLabel = new Label();
            regularDepthTextBox = new TextBox();
            regularPeriodLabel = new Label();
            regularPeriodTextBox = new TextBox();
            regularHeightLabel = new Label();
            regularHeightTextBox = new TextBox();
            regularDirectionLabel = new Label();
            regularDirectionTextBox = new TextBox();
            regularTimeStepLabel = new Label();
            regularTimeStepTextBox = new TextBox();
            regularSampleCountLabel = new Label();
            regularSampleCountTextBox = new TextBox();
            regularFrequencyLabel = new Label();
            regularCharacteristicFrequencyTextBox = new TextBox();
            regularCharacteristicPeriodLabel = new Label();
            regularCharacteristicPeriodTextBox = new TextBox();
            regularOutputLabel = new Label();
            regularOutputTextBox = new TextBox();
            regularBrowseOutputButton = new Button();
            regularGenerateButton = new Button();
            regularStatusLabel = new Label();
            regularPreviewGroup = new GroupBox();
            regularPreview = new WaveformPreviewControl();
            pagePanel.SuspendLayout();
            regularLayout.SuspendLayout();
            regularParameterGroup.SuspendLayout();
            regularParameterLayout.SuspendLayout();
            regularPreviewGroup.SuspendLayout();
            SuspendLayout();
            //
            // pagePanel
            //
            pagePanel.Controls.Add(regularLayout);
            pagePanel.Dock = DockStyle.Fill;
            pagePanel.Location = new Point(0, 0);
            pagePanel.Name = "pagePanel";
            pagePanel.Padding = new Padding(8);
            pagePanel.Size = new Size(1104, 606);
            pagePanel.TabIndex = 0;
            //
            // regularLayout
            //
            regularLayout.ColumnCount = 2;
            regularLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 445F));
            regularLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            regularLayout.Controls.Add(regularParameterGroup, 0, 0);
            regularLayout.Controls.Add(regularPreviewGroup, 1, 0);
            regularLayout.Dock = DockStyle.Fill;
            regularLayout.Location = new Point(8, 8);
            regularLayout.MinimumSize = new Size(900, 400);
            regularLayout.Name = "regularLayout";
            regularLayout.RowCount = 1;
            regularLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            regularLayout.Size = new Size(1088, 590);
            regularLayout.TabIndex = 0;
            //
            // regularParameterGroup
            //
            regularParameterGroup.BackColor = Color.White;
            regularParameterGroup.Controls.Add(regularParameterLayout);
            regularParameterGroup.Dock = DockStyle.Fill;
            regularParameterGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            regularParameterGroup.Location = new Point(3, 3);
            regularParameterGroup.Name = "regularParameterGroup";
            regularParameterGroup.Padding = new Padding(10, 16, 10, 10);
            regularParameterGroup.Size = new Size(439, 584);
            regularParameterGroup.TabIndex = 0;
            regularParameterGroup.TabStop = false;
            regularParameterGroup.Text = "规则波参数";
            //
            // regularParameterLayout
            //
            regularParameterLayout.AutoSize = true;
            regularParameterLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            regularParameterLayout.ColumnCount = 4;
            regularParameterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            regularParameterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            regularParameterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            regularParameterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            regularParameterLayout.Controls.Add(regularSegmentLabel, 0, 0);
            regularParameterLayout.Controls.Add(regularSegmentComboBox, 1, 0);
            regularParameterLayout.Controls.Add(regularTheoryLabel, 2, 0);
            regularParameterLayout.Controls.Add(regularTheoryComboBox, 3, 0);
            regularParameterLayout.Controls.Add(regularDepthLabel, 0, 1);
            regularParameterLayout.Controls.Add(regularDepthTextBox, 1, 1);
            regularParameterLayout.Controls.Add(regularPeriodLabel, 2, 1);
            regularParameterLayout.Controls.Add(regularPeriodTextBox, 3, 1);
            regularParameterLayout.Controls.Add(regularHeightLabel, 0, 2);
            regularParameterLayout.Controls.Add(regularHeightTextBox, 1, 2);
            regularParameterLayout.Controls.Add(regularDirectionLabel, 2, 2);
            regularParameterLayout.Controls.Add(regularDirectionTextBox, 3, 2);
            regularParameterLayout.Controls.Add(regularTimeStepLabel, 0, 3);
            regularParameterLayout.Controls.Add(regularTimeStepTextBox, 1, 3);
            regularParameterLayout.Controls.Add(regularSampleCountLabel, 2, 3);
            regularParameterLayout.Controls.Add(regularSampleCountTextBox, 3, 3);
            regularParameterLayout.Controls.Add(regularFrequencyLabel, 0, 4);
            regularParameterLayout.Controls.Add(regularCharacteristicFrequencyTextBox, 1, 4);
            regularParameterLayout.Controls.Add(regularCharacteristicPeriodLabel, 2, 4);
            regularParameterLayout.Controls.Add(regularCharacteristicPeriodTextBox, 3, 4);
            regularParameterLayout.Controls.Add(regularOutputLabel, 0, 5);
            regularParameterLayout.Controls.Add(regularOutputTextBox, 1, 5);
            regularParameterLayout.Controls.Add(regularBrowseOutputButton, 3, 5);
            regularParameterLayout.Controls.Add(regularGenerateButton, 0, 6);
            regularParameterLayout.Controls.Add(regularStatusLabel, 2, 6);
            regularParameterLayout.Dock = DockStyle.Top;
            regularParameterLayout.Location = new Point(10, 32);
            regularParameterLayout.Name = "regularParameterLayout";
            regularParameterLayout.RowCount = 7;
            regularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            regularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            regularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            regularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            regularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            regularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            regularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            regularParameterLayout.Size = new Size(419, 284);
            regularParameterLayout.TabIndex = 0;
            //
            // regularSegmentLabel
            //
            regularSegmentLabel.Dock = DockStyle.Fill;
            regularSegmentLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            regularSegmentLabel.ForeColor = Color.FromArgb(71, 85, 105);
            regularSegmentLabel.Location = new Point(3, 0);
            regularSegmentLabel.Name = "regularSegmentLabel";
            regularSegmentLabel.Size = new Size(84, 40);
            regularSegmentLabel.TabIndex = 0;
            regularSegmentLabel.Text = "造波段";
            regularSegmentLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // regularSegmentComboBox
            //
            regularSegmentComboBox.BackColor = Color.White;
            regularSegmentComboBox.Dock = DockStyle.Fill;
            regularSegmentComboBox.FlatStyle = FlatStyle.Flat;
            regularSegmentComboBox.Font = new Font("Microsoft YaHei UI", 9F);
            regularSegmentComboBox.ForeColor = Color.FromArgb(15, 23, 42);
            regularSegmentComboBox.Items.AddRange(new object[] { "X轴+Y轴", "X轴", "Y轴" });
            regularSegmentComboBox.Location = new Point(93, 5);
            regularSegmentComboBox.Margin = new Padding(3, 5, 3, 5);
            regularSegmentComboBox.Name = "regularSegmentComboBox";
            regularSegmentComboBox.Size = new Size(104, 25);
            regularSegmentComboBox.TabIndex = 1;
            //
            // regularTheoryLabel
            //
            regularTheoryLabel.Dock = DockStyle.Fill;
            regularTheoryLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            regularTheoryLabel.ForeColor = Color.FromArgb(71, 85, 105);
            regularTheoryLabel.Location = new Point(203, 0);
            regularTheoryLabel.Name = "regularTheoryLabel";
            regularTheoryLabel.Size = new Size(84, 40);
            regularTheoryLabel.TabIndex = 2;
            regularTheoryLabel.Text = "造波理论";
            regularTheoryLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // regularTheoryComboBox
            //
            regularTheoryComboBox.BackColor = Color.White;
            regularTheoryComboBox.Dock = DockStyle.Fill;
            regularTheoryComboBox.FlatStyle = FlatStyle.Flat;
            regularTheoryComboBox.Font = new Font("Microsoft YaHei UI", 9F);
            regularTheoryComboBox.ForeColor = Color.FromArgb(15, 23, 42);
            regularTheoryComboBox.Items.AddRange(new object[] { "线性理论", "二阶Stokes（内置）", "孤立波", "流函数波（旧程序）", "椭圆余弦波（旧程序）" });
            regularTheoryComboBox.Location = new Point(293, 5);
            regularTheoryComboBox.Margin = new Padding(3, 5, 3, 5);
            regularTheoryComboBox.Name = "regularTheoryComboBox";
            regularTheoryComboBox.Size = new Size(123, 25);
            regularTheoryComboBox.TabIndex = 3;
            //
            // regularDepthLabel
            //
            regularDepthLabel.Dock = DockStyle.Fill;
            regularDepthLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            regularDepthLabel.ForeColor = Color.FromArgb(71, 85, 105);
            regularDepthLabel.Location = new Point(3, 40);
            regularDepthLabel.Name = "regularDepthLabel";
            regularDepthLabel.Size = new Size(84, 40);
            regularDepthLabel.TabIndex = 4;
            regularDepthLabel.Text = "水深(m)";
            regularDepthLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // regularDepthTextBox
            //
            regularDepthTextBox.BackColor = Color.White;
            regularDepthTextBox.BorderStyle = BorderStyle.FixedSingle;
            regularDepthTextBox.Dock = DockStyle.Fill;
            regularDepthTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            regularDepthTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            regularDepthTextBox.Location = new Point(93, 45);
            regularDepthTextBox.Margin = new Padding(3, 5, 3, 5);
            regularDepthTextBox.Name = "regularDepthTextBox";
            regularDepthTextBox.Size = new Size(104, 23);
            regularDepthTextBox.TabIndex = 5;
            regularDepthTextBox.Text = "0.5";
            //
            // regularPeriodLabel
            //
            regularPeriodLabel.Dock = DockStyle.Fill;
            regularPeriodLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            regularPeriodLabel.ForeColor = Color.FromArgb(71, 85, 105);
            regularPeriodLabel.Location = new Point(203, 40);
            regularPeriodLabel.Name = "regularPeriodLabel";
            regularPeriodLabel.Size = new Size(84, 40);
            regularPeriodLabel.TabIndex = 6;
            regularPeriodLabel.Text = "周期(s)";
            regularPeriodLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // regularPeriodTextBox
            //
            regularPeriodTextBox.BackColor = Color.White;
            regularPeriodTextBox.BorderStyle = BorderStyle.FixedSingle;
            regularPeriodTextBox.Dock = DockStyle.Fill;
            regularPeriodTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            regularPeriodTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            regularPeriodTextBox.Location = new Point(293, 45);
            regularPeriodTextBox.Margin = new Padding(3, 5, 3, 5);
            regularPeriodTextBox.Name = "regularPeriodTextBox";
            regularPeriodTextBox.Size = new Size(123, 23);
            regularPeriodTextBox.TabIndex = 7;
            regularPeriodTextBox.Text = "1.5";
            //
            // regularHeightLabel
            //
            regularHeightLabel.Dock = DockStyle.Fill;
            regularHeightLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            regularHeightLabel.ForeColor = Color.FromArgb(71, 85, 105);
            regularHeightLabel.Location = new Point(3, 80);
            regularHeightLabel.Name = "regularHeightLabel";
            regularHeightLabel.Size = new Size(84, 40);
            regularHeightLabel.TabIndex = 8;
            regularHeightLabel.Text = "波高(m)";
            regularHeightLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // regularHeightTextBox
            //
            regularHeightTextBox.BackColor = Color.White;
            regularHeightTextBox.BorderStyle = BorderStyle.FixedSingle;
            regularHeightTextBox.Dock = DockStyle.Fill;
            regularHeightTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            regularHeightTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            regularHeightTextBox.Location = new Point(93, 85);
            regularHeightTextBox.Margin = new Padding(3, 5, 3, 5);
            regularHeightTextBox.Name = "regularHeightTextBox";
            regularHeightTextBox.Size = new Size(104, 23);
            regularHeightTextBox.TabIndex = 9;
            regularHeightTextBox.Text = "0.1";
            //
            // regularDirectionLabel
            //
            regularDirectionLabel.Dock = DockStyle.Fill;
            regularDirectionLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            regularDirectionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            regularDirectionLabel.Location = new Point(203, 80);
            regularDirectionLabel.Name = "regularDirectionLabel";
            regularDirectionLabel.Size = new Size(84, 40);
            regularDirectionLabel.TabIndex = 10;
            regularDirectionLabel.Text = "波向(°)";
            regularDirectionLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // regularDirectionTextBox
            //
            regularDirectionTextBox.BackColor = Color.White;
            regularDirectionTextBox.BorderStyle = BorderStyle.FixedSingle;
            regularDirectionTextBox.Dock = DockStyle.Fill;
            regularDirectionTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            regularDirectionTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            regularDirectionTextBox.Location = new Point(293, 85);
            regularDirectionTextBox.Margin = new Padding(3, 5, 3, 5);
            regularDirectionTextBox.Name = "regularDirectionTextBox";
            regularDirectionTextBox.Size = new Size(123, 23);
            regularDirectionTextBox.TabIndex = 11;
            regularDirectionTextBox.Text = "90";
            //
            // regularTimeStepLabel
            //
            regularTimeStepLabel.Dock = DockStyle.Fill;
            regularTimeStepLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            regularTimeStepLabel.ForeColor = Color.FromArgb(71, 85, 105);
            regularTimeStepLabel.Location = new Point(3, 120);
            regularTimeStepLabel.Name = "regularTimeStepLabel";
            regularTimeStepLabel.Size = new Size(84, 40);
            regularTimeStepLabel.TabIndex = 12;
            regularTimeStepLabel.Text = "步长(s)";
            regularTimeStepLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // regularTimeStepTextBox
            //
            regularTimeStepTextBox.BackColor = Color.White;
            regularTimeStepTextBox.BorderStyle = BorderStyle.FixedSingle;
            regularTimeStepTextBox.Dock = DockStyle.Fill;
            regularTimeStepTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            regularTimeStepTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            regularTimeStepTextBox.Location = new Point(93, 125);
            regularTimeStepTextBox.Margin = new Padding(3, 5, 3, 5);
            regularTimeStepTextBox.Name = "regularTimeStepTextBox";
            regularTimeStepTextBox.Size = new Size(104, 23);
            regularTimeStepTextBox.TabIndex = 13;
            regularTimeStepTextBox.Text = "0.02";
            //
            // regularSampleCountLabel
            //
            regularSampleCountLabel.Dock = DockStyle.Fill;
            regularSampleCountLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            regularSampleCountLabel.ForeColor = Color.FromArgb(71, 85, 105);
            regularSampleCountLabel.Location = new Point(203, 120);
            regularSampleCountLabel.Name = "regularSampleCountLabel";
            regularSampleCountLabel.Size = new Size(84, 40);
            regularSampleCountLabel.TabIndex = 14;
            regularSampleCountLabel.Text = "时序数";
            regularSampleCountLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // regularSampleCountTextBox
            //
            regularSampleCountTextBox.BackColor = Color.White;
            regularSampleCountTextBox.BorderStyle = BorderStyle.FixedSingle;
            regularSampleCountTextBox.Dock = DockStyle.Fill;
            regularSampleCountTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            regularSampleCountTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            regularSampleCountTextBox.Location = new Point(293, 125);
            regularSampleCountTextBox.Margin = new Padding(3, 5, 3, 5);
            regularSampleCountTextBox.Name = "regularSampleCountTextBox";
            regularSampleCountTextBox.Size = new Size(123, 23);
            regularSampleCountTextBox.TabIndex = 15;
            regularSampleCountTextBox.Text = "4096";
            //
            // regularFrequencyLabel
            //
            regularFrequencyLabel.Dock = DockStyle.Fill;
            regularFrequencyLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            regularFrequencyLabel.ForeColor = Color.FromArgb(71, 85, 105);
            regularFrequencyLabel.Location = new Point(3, 160);
            regularFrequencyLabel.Name = "regularFrequencyLabel";
            regularFrequencyLabel.Size = new Size(84, 40);
            regularFrequencyLabel.TabIndex = 16;
            regularFrequencyLabel.Text = "特征频率";
            regularFrequencyLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // regularCharacteristicFrequencyTextBox
            //
            regularCharacteristicFrequencyTextBox.BackColor = Color.White;
            regularCharacteristicFrequencyTextBox.BorderStyle = BorderStyle.FixedSingle;
            regularCharacteristicFrequencyTextBox.Dock = DockStyle.Fill;
            regularCharacteristicFrequencyTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            regularCharacteristicFrequencyTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            regularCharacteristicFrequencyTextBox.Location = new Point(93, 165);
            regularCharacteristicFrequencyTextBox.Margin = new Padding(3, 5, 3, 5);
            regularCharacteristicFrequencyTextBox.Name = "regularCharacteristicFrequencyTextBox";
            regularCharacteristicFrequencyTextBox.Size = new Size(104, 23);
            regularCharacteristicFrequencyTextBox.TabIndex = 17;
            regularCharacteristicFrequencyTextBox.Text = "0.514";
            //
            // regularCharacteristicPeriodLabel
            //
            regularCharacteristicPeriodLabel.Dock = DockStyle.Fill;
            regularCharacteristicPeriodLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            regularCharacteristicPeriodLabel.ForeColor = Color.FromArgb(71, 85, 105);
            regularCharacteristicPeriodLabel.Location = new Point(203, 160);
            regularCharacteristicPeriodLabel.Name = "regularCharacteristicPeriodLabel";
            regularCharacteristicPeriodLabel.Size = new Size(84, 40);
            regularCharacteristicPeriodLabel.TabIndex = 18;
            regularCharacteristicPeriodLabel.Text = "特征周期";
            regularCharacteristicPeriodLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // regularCharacteristicPeriodTextBox
            //
            regularCharacteristicPeriodTextBox.BackColor = Color.White;
            regularCharacteristicPeriodTextBox.BorderStyle = BorderStyle.FixedSingle;
            regularCharacteristicPeriodTextBox.Dock = DockStyle.Fill;
            regularCharacteristicPeriodTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            regularCharacteristicPeriodTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            regularCharacteristicPeriodTextBox.Location = new Point(293, 165);
            regularCharacteristicPeriodTextBox.Margin = new Padding(3, 5, 3, 5);
            regularCharacteristicPeriodTextBox.Name = "regularCharacteristicPeriodTextBox";
            regularCharacteristicPeriodTextBox.Size = new Size(123, 23);
            regularCharacteristicPeriodTextBox.TabIndex = 19;
            regularCharacteristicPeriodTextBox.Text = "4.00";
            //
            // regularOutputLabel
            //
            regularOutputLabel.Dock = DockStyle.Fill;
            regularOutputLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            regularOutputLabel.ForeColor = Color.FromArgb(71, 85, 105);
            regularOutputLabel.Location = new Point(3, 200);
            regularOutputLabel.Name = "regularOutputLabel";
            regularOutputLabel.Size = new Size(84, 40);
            regularOutputLabel.TabIndex = 20;
            regularOutputLabel.Text = "保存文件";
            regularOutputLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // regularOutputTextBox
            //
            regularOutputTextBox.BackColor = Color.White;
            regularOutputTextBox.BorderStyle = BorderStyle.FixedSingle;
            regularParameterLayout.SetColumnSpan(regularOutputTextBox, 2);
            regularOutputTextBox.Dock = DockStyle.Fill;
            regularOutputTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            regularOutputTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            regularOutputTextBox.Location = new Point(93, 205);
            regularOutputTextBox.Margin = new Padding(3, 5, 3, 5);
            regularOutputTextBox.Name = "regularOutputTextBox";
            regularOutputTextBox.Size = new Size(194, 23);
            regularOutputTextBox.TabIndex = 21;
            //
            // regularBrowseOutputButton
            //
            regularBrowseOutputButton.BackColor = Color.FromArgb(248, 250, 252);
            regularBrowseOutputButton.Cursor = Cursors.Hand;
            regularBrowseOutputButton.Dock = DockStyle.Fill;
            regularBrowseOutputButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            regularBrowseOutputButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
            regularBrowseOutputButton.FlatStyle = FlatStyle.Flat;
            regularBrowseOutputButton.ForeColor = Color.FromArgb(15, 23, 42);
            regularBrowseOutputButton.Location = new Point(293, 204);
            regularBrowseOutputButton.Margin = new Padding(3, 4, 3, 4);
            regularBrowseOutputButton.Name = "regularBrowseOutputButton";
            regularBrowseOutputButton.Size = new Size(123, 32);
            regularBrowseOutputButton.TabIndex = 22;
            regularBrowseOutputButton.Text = "浏览";
            regularBrowseOutputButton.UseVisualStyleBackColor = false;
            regularBrowseOutputButton.Click += BrowseRegularOutputButton_Click;
            //
            // regularGenerateButton
            //
            regularGenerateButton.BackColor = Color.FromArgb(29, 78, 216);
            regularParameterLayout.SetColumnSpan(regularGenerateButton, 2);
            regularGenerateButton.Cursor = Cursors.Hand;
            regularGenerateButton.FlatAppearance.BorderSize = 0;
            regularGenerateButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 64, 175);
            regularGenerateButton.FlatStyle = FlatStyle.Flat;
            regularGenerateButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            regularGenerateButton.ForeColor = Color.White;
            regularGenerateButton.Location = new Point(3, 243);
            regularGenerateButton.Name = "regularGenerateButton";
            regularGenerateButton.Size = new Size(160, 34);
            regularGenerateButton.TabIndex = 23;
            regularGenerateButton.Text = "计算并保存";
            regularGenerateButton.UseVisualStyleBackColor = false;
            regularGenerateButton.Click += GenerateRegularButton_Click;
            //
            // regularStatusLabel
            //
            regularStatusLabel.AutoEllipsis = true;
            regularParameterLayout.SetColumnSpan(regularStatusLabel, 2);
            regularStatusLabel.Dock = DockStyle.Fill;
            regularStatusLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            regularStatusLabel.ForeColor = Color.FromArgb(71, 85, 105);
            regularStatusLabel.Location = new Point(203, 240);
            regularStatusLabel.Name = "regularStatusLabel";
            regularStatusLabel.Size = new Size(213, 44);
            regularStatusLabel.TabIndex = 24;
            regularStatusLabel.Text = "请选择 CSV 保存路径";
            regularStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // regularPreviewGroup
            //
            regularPreviewGroup.BackColor = Color.White;
            regularPreviewGroup.Controls.Add(regularPreview);
            regularPreviewGroup.Dock = DockStyle.Fill;
            regularPreviewGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            regularPreviewGroup.Location = new Point(448, 3);
            regularPreviewGroup.Name = "regularPreviewGroup";
            regularPreviewGroup.Padding = new Padding(10, 18, 10, 10);
            regularPreviewGroup.Size = new Size(637, 584);
            regularPreviewGroup.TabIndex = 1;
            regularPreviewGroup.TabStop = false;
            regularPreviewGroup.Text = "波形预览";
            //
            // regularPreview
            //
            regularPreview.BackColor = Color.White;
            regularPreview.Dock = DockStyle.Fill;
            regularPreview.ForeColor = Color.FromArgb(29, 78, 216);
            regularPreview.Location = new Point(10, 34);
            regularPreview.MinimumSize = new Size(300, 180);
            regularPreview.Name = "regularPreview";
            regularPreview.Size = new Size(617, 540);
            regularPreview.TabIndex = 0;
            regularPreview.UnitText = "m";
            //
            // RegularWavePage
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            AutoScrollMinSize = new Size(900, 416);
            BackColor = Color.FromArgb(241, 245, 249);
            Controls.Add(pagePanel);
            Font = new Font("Microsoft YaHei UI", 9F);
            ForeColor = Color.FromArgb(15, 23, 42);
            Name = "RegularWavePage";
            Size = new Size(1104, 606);
            pagePanel.ResumeLayout(false);
            regularLayout.ResumeLayout(false);
            regularParameterGroup.ResumeLayout(false);
            regularParameterGroup.PerformLayout();
            regularParameterLayout.ResumeLayout(false);
            regularParameterLayout.PerformLayout();
            regularPreviewGroup.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
