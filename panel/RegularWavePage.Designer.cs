namespace Page_switching.panel
{
    partial class RegularWavePage
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pagePanel;
        private TableLayoutPanel regularLayout;
        private AntdUI.Panel regularParameterGroup;
        private AntdUI.Label regularParameterGroupCaption;
        private TableLayoutPanel regularParameterLayout;
        private AntdUI.Label regularSegmentLabel;
        internal AntdUI.Select regularSegmentComboBox;
        private AntdUI.Label regularTheoryLabel;
        internal AntdUI.Select regularTheoryComboBox;
        private AntdUI.Label regularDepthLabel;
        internal AntdUI.Input regularDepthTextBox;
        private AntdUI.Label regularPeriodLabel;
        internal AntdUI.Input regularPeriodTextBox;
        private AntdUI.Label regularHeightLabel;
        internal AntdUI.Input regularHeightTextBox;
        private AntdUI.Label regularDirectionLabel;
        internal AntdUI.Input regularDirectionTextBox;
        private AntdUI.Label regularTimeStepLabel;
        internal AntdUI.Input regularTimeStepTextBox;
        private AntdUI.Label regularSampleCountLabel;
        internal AntdUI.Input regularSampleCountTextBox;
        private AntdUI.Label regularFrequencyLabel;
        internal AntdUI.Input regularCharacteristicFrequencyTextBox;
        private AntdUI.Label regularCharacteristicPeriodLabel;
        internal AntdUI.Input regularCharacteristicPeriodTextBox;
        private AntdUI.Label regularOutputLabel;
        internal AntdUI.Input regularOutputTextBox;
        internal AntdUI.Button regularBrowseOutputButton;
        internal AntdUI.Button regularGenerateButton;
        internal AntdUI.Label regularStatusLabel;
        private AntdUI.Panel regularPreviewGroup;
        private AntdUI.Label regularPreviewGroupCaption;
        internal global::Page_switching.WaveformPreviewControl regularPreview;

        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            regularPreview = new WaveformPreviewControl();
            pagePanel = new Panel();
            regularLayout = new TableLayoutPanel();
            regularParameterGroup = new AntdUI.Panel();
            regularParameterGroupCaption = new AntdUI.Label();
            regularParameterLayout = new TableLayoutPanel();
            regularSegmentLabel = new AntdUI.Label();
            regularSegmentComboBox = new AntdUI.Select();
            regularSegmentComboBox.WaveSize = 0;
            regularSegmentComboBox.Radius = 6;
            regularSegmentComboBox.List = true;
            regularTheoryLabel = new AntdUI.Label();
            regularTheoryComboBox = new AntdUI.Select();
            regularTheoryComboBox.WaveSize = 0;
            regularTheoryComboBox.Radius = 6;
            regularTheoryComboBox.List = true;
            regularDepthLabel = new AntdUI.Label();
            regularDepthTextBox = new AntdUI.Input();
            regularDepthTextBox.WaveSize = 0;
            regularDepthTextBox.Radius = 6;
            regularPeriodLabel = new AntdUI.Label();
            regularPeriodTextBox = new AntdUI.Input();
            regularPeriodTextBox.WaveSize = 0;
            regularPeriodTextBox.Radius = 6;
            regularHeightLabel = new AntdUI.Label();
            regularHeightTextBox = new AntdUI.Input();
            regularHeightTextBox.WaveSize = 0;
            regularHeightTextBox.Radius = 6;
            regularDirectionLabel = new AntdUI.Label();
            regularDirectionTextBox = new AntdUI.Input();
            regularDirectionTextBox.WaveSize = 0;
            regularDirectionTextBox.Radius = 6;
            regularTimeStepLabel = new AntdUI.Label();
            regularTimeStepTextBox = new AntdUI.Input();
            regularTimeStepTextBox.WaveSize = 0;
            regularTimeStepTextBox.Radius = 6;
            regularSampleCountLabel = new AntdUI.Label();
            regularSampleCountTextBox = new AntdUI.Input();
            regularSampleCountTextBox.WaveSize = 0;
            regularSampleCountTextBox.Radius = 6;
            regularFrequencyLabel = new AntdUI.Label();
            regularCharacteristicFrequencyTextBox = new AntdUI.Input();
            regularCharacteristicFrequencyTextBox.WaveSize = 0;
            regularCharacteristicFrequencyTextBox.Radius = 6;
            regularCharacteristicPeriodLabel = new AntdUI.Label();
            regularCharacteristicPeriodTextBox = new AntdUI.Input();
            regularCharacteristicPeriodTextBox.WaveSize = 0;
            regularCharacteristicPeriodTextBox.Radius = 6;
            regularOutputLabel = new AntdUI.Label();
            regularOutputTextBox = new AntdUI.Input();
            regularOutputTextBox.WaveSize = 0;
            regularOutputTextBox.Radius = 6;
            regularBrowseOutputButton = new AntdUI.Button();
            regularBrowseOutputButton.Radius = 6;
            regularBrowseOutputButton.BorderWidth = 1F;
            regularGenerateButton = new AntdUI.Button();
            regularGenerateButton.Radius = 6;
            regularStatusLabel = new AntdUI.Label();
            regularPreviewGroup = new AntdUI.Panel();
            regularPreviewGroupCaption = new AntdUI.Label();
            pagePanel.SuspendLayout();
            regularLayout.SuspendLayout();
            regularParameterGroup.SuspendLayout();
            regularParameterLayout.SuspendLayout();
            regularPreviewGroup.SuspendLayout();
            SuspendLayout();
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
            regularParameterGroup.Padding = new Padding(10, 32, 10, 10);
            regularParameterGroup.Size = new Size(439, 584);
            regularParameterGroup.TabIndex = 0;
            // AntdUI 面板用独立标题标签保留原分组标题，布局仍可在 Designer 编辑。
            regularParameterGroupCaption.Text = "规则波参数";
            regularParameterGroupCaption.AutoSize = true;
            regularParameterGroupCaption.Name = "regularParameterGroupCaption";
            regularParameterGroupCaption.Location = new Point(10, 2);
            regularParameterGroupCaption.ForeColor = Color.FromArgb(15, 23, 42);
            regularParameterGroup.BorderWidth = 1F;
            regularParameterGroup.BorderColor = Color.FromArgb(203, 213, 225);
            regularParameterGroup.Radius = 6;
            regularParameterGroup.Controls.Add(regularParameterGroupCaption);
            regularParameterGroupCaption.BringToFront();
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
            regularBrowseOutputButton.DefaultBack = Color.FromArgb(248, 250, 252);
            regularBrowseOutputButton.Cursor = Cursors.Hand;
            regularBrowseOutputButton.Dock = DockStyle.Fill;
            regularBrowseOutputButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
            regularBrowseOutputButton.BackHover = Color.FromArgb(226, 232, 240);
            regularBrowseOutputButton.ForeColor = Color.FromArgb(15, 23, 42);
            regularBrowseOutputButton.Location = new Point(293, 204);
            regularBrowseOutputButton.Margin = new Padding(3, 4, 3, 4);
            regularBrowseOutputButton.Name = "regularBrowseOutputButton";
            regularBrowseOutputButton.Size = new Size(123, 32);
            regularBrowseOutputButton.TabIndex = 22;
            regularBrowseOutputButton.Text = "浏览";
            regularBrowseOutputButton.Click += BrowseRegularOutputButton_Click;
            //
            // regularGenerateButton
            //
            regularGenerateButton.BackColor = Color.FromArgb(29, 78, 216);
            regularGenerateButton.DefaultBack = Color.FromArgb(29, 78, 216);
            regularParameterLayout.SetColumnSpan(regularGenerateButton, 2);
            regularGenerateButton.Cursor = Cursors.Hand;
            regularGenerateButton.BorderWidth = 0;
            regularGenerateButton.BackHover = Color.FromArgb(30, 64, 175);
            regularGenerateButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            regularGenerateButton.ForeColor = Color.White;
            regularGenerateButton.Location = new Point(3, 243);
            regularGenerateButton.Name = "regularGenerateButton";
            regularGenerateButton.Size = new Size(160, 34);
            regularGenerateButton.TabIndex = 23;
            regularGenerateButton.Text = "计算并保存";
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
            regularPreviewGroup.Padding = new Padding(10, 32, 10, 10);
            regularPreviewGroup.Size = new Size(637, 584);
            regularPreviewGroup.TabIndex = 1;
            // AntdUI 面板用独立标题标签保留原分组标题，布局仍可在 Designer 编辑。
            regularPreviewGroupCaption.Text = "波形预览";
            regularPreviewGroupCaption.AutoSize = true;
            regularPreviewGroupCaption.Name = "regularPreviewGroupCaption";
            regularPreviewGroupCaption.Location = new Point(10, 2);
            regularPreviewGroupCaption.ForeColor = Color.FromArgb(15, 23, 42);
            regularPreviewGroup.BorderWidth = 1F;
            regularPreviewGroup.BorderColor = Color.FromArgb(203, 213, 225);
            regularPreviewGroup.Radius = 6;
            regularPreviewGroup.Controls.Add(regularPreviewGroupCaption);
            regularPreviewGroupCaption.BringToFront();
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
