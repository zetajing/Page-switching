namespace Page_switching.panel
{
    partial class IrregularWavePage
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pagePanel;
        private TableLayoutPanel irregularLayout;
        private AntdUI.Panel irregularParameterGroup;
        private AntdUI.Label irregularParameterGroupCaption;
        private TableLayoutPanel irregularParameterLayout;
        private AntdUI.Label irregularModeLabel;
        internal AntdUI.Select irregularModeComboBox;
        private AntdUI.Label irregularTheoryLabel;
        internal AntdUI.Select irregularTheoryComboBox;
        private AntdUI.Label irregularSpectrumLabel;
        internal AntdUI.Select irregularSpectrumComboBox;
        private AntdUI.Label irregularSegmentLabel;
        internal AntdUI.Select irregularSegmentComboBox;
        private AntdUI.Label irregularDirectionLabel;
        internal AntdUI.Input irregularDirectionTextBox;
        private AntdUI.Label irregularDepthLabel;
        internal AntdUI.Input irregularDepthTextBox;
        private AntdUI.Label irregularSignificantPeriodLabel;
        internal AntdUI.Input irregularSignificantPeriodTextBox;
        private AntdUI.Label irregularSignificantHeightLabel;
        internal AntdUI.Input irregularSignificantHeightTextBox;
        private AntdUI.Label irregularTimeStepLabel;
        internal AntdUI.Input irregularTimeStepTextBox;
        private AntdUI.Label irregularSampleCountLabel;
        internal AntdUI.Input irregularSampleCountTextBox;
        private AntdUI.Label irregularFrequencyLabel;
        internal AntdUI.Input irregularCharacteristicFrequencyTextBox;
        private AntdUI.Label irregularCharacteristicPeriodLabel;
        internal AntdUI.Input irregularCharacteristicPeriodTextBox;
        private AntdUI.Label irregularPeakFactorLabel;
        internal AntdUI.Input irregularPeakFactorTextBox;
        private AntdUI.Label irregularRandomSeedLabel;
        internal AntdUI.Input irregularRandomSeedTextBox;
        private AntdUI.Label irregularMinimumPeriodLabel;
        internal AntdUI.Input irregularMinimumPeriodTextBox;
        private AntdUI.Label irregularMaximumPeriodLabel;
        internal AntdUI.Input irregularMaximumPeriodTextBox;
        private AntdUI.Label irregularMinimumDifferencePeriodLabel;
        internal AntdUI.Input irregularMinimumDifferencePeriodTextBox;
        private AntdUI.Label irregularMaximumDifferencePeriodLabel;
        internal AntdUI.Input irregularMaximumDifferencePeriodTextBox;
        private AntdUI.Label irregularNegativeDirectionLabel;
        internal AntdUI.Input irregularNegativeDirectionTextBox;
        private AntdUI.Label irregularPositiveDirectionLabel;
        internal AntdUI.Input irregularPositiveDirectionTextBox;
        private AntdUI.Label irregularOutputLabel;
        internal AntdUI.Input irregularOutputTextBox;
        internal AntdUI.Button irregularBrowseOutputButton;
        internal AntdUI.Button irregularGenerateButton;
        internal AntdUI.Label irregularStatusLabel;
        private AntdUI.Panel irregularPreviewGroup;
        private AntdUI.Label irregularPreviewGroupCaption;
        internal global::Page_switching.WaveformPreviewControl irregularPreview;

        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            irregularPreview = new WaveformPreviewControl();
            pagePanel = new Panel();
            irregularLayout = new TableLayoutPanel();
            irregularParameterGroup = new AntdUI.Panel();
            irregularParameterGroupCaption = new AntdUI.Label();
            irregularParameterLayout = new TableLayoutPanel();
            irregularModeLabel = new AntdUI.Label();
            irregularModeComboBox = new AntdUI.Select();
            irregularModeComboBox.WaveSize = 0;
            irregularModeComboBox.Radius = 6;
            irregularModeComboBox.List = true;
            irregularTheoryLabel = new AntdUI.Label();
            irregularTheoryComboBox = new AntdUI.Select();
            irregularTheoryComboBox.WaveSize = 0;
            irregularTheoryComboBox.Radius = 6;
            irregularTheoryComboBox.List = true;
            irregularSpectrumLabel = new AntdUI.Label();
            irregularSpectrumComboBox = new AntdUI.Select();
            irregularSpectrumComboBox.WaveSize = 0;
            irregularSpectrumComboBox.Radius = 6;
            irregularSpectrumComboBox.List = true;
            irregularSegmentLabel = new AntdUI.Label();
            irregularSegmentComboBox = new AntdUI.Select();
            irregularSegmentComboBox.WaveSize = 0;
            irregularSegmentComboBox.Radius = 6;
            irregularSegmentComboBox.List = true;
            irregularDirectionLabel = new AntdUI.Label();
            irregularDirectionTextBox = new AntdUI.Input();
            irregularDirectionTextBox.WaveSize = 0;
            irregularDirectionTextBox.Radius = 6;
            irregularDepthLabel = new AntdUI.Label();
            irregularDepthTextBox = new AntdUI.Input();
            irregularDepthTextBox.WaveSize = 0;
            irregularDepthTextBox.Radius = 6;
            irregularSignificantPeriodLabel = new AntdUI.Label();
            irregularSignificantPeriodTextBox = new AntdUI.Input();
            irregularSignificantPeriodTextBox.WaveSize = 0;
            irregularSignificantPeriodTextBox.Radius = 6;
            irregularSignificantHeightLabel = new AntdUI.Label();
            irregularSignificantHeightTextBox = new AntdUI.Input();
            irregularSignificantHeightTextBox.WaveSize = 0;
            irregularSignificantHeightTextBox.Radius = 6;
            irregularTimeStepLabel = new AntdUI.Label();
            irregularTimeStepTextBox = new AntdUI.Input();
            irregularTimeStepTextBox.WaveSize = 0;
            irregularTimeStepTextBox.Radius = 6;
            irregularSampleCountLabel = new AntdUI.Label();
            irregularSampleCountTextBox = new AntdUI.Input();
            irregularSampleCountTextBox.WaveSize = 0;
            irregularSampleCountTextBox.Radius = 6;
            irregularFrequencyLabel = new AntdUI.Label();
            irregularCharacteristicFrequencyTextBox = new AntdUI.Input();
            irregularCharacteristicFrequencyTextBox.WaveSize = 0;
            irregularCharacteristicFrequencyTextBox.Radius = 6;
            irregularCharacteristicPeriodLabel = new AntdUI.Label();
            irregularCharacteristicPeriodTextBox = new AntdUI.Input();
            irregularCharacteristicPeriodTextBox.WaveSize = 0;
            irregularCharacteristicPeriodTextBox.Radius = 6;
            irregularPeakFactorLabel = new AntdUI.Label();
            irregularPeakFactorTextBox = new AntdUI.Input();
            irregularPeakFactorTextBox.WaveSize = 0;
            irregularPeakFactorTextBox.Radius = 6;
            irregularRandomSeedLabel = new AntdUI.Label();
            irregularRandomSeedTextBox = new AntdUI.Input();
            irregularRandomSeedTextBox.WaveSize = 0;
            irregularRandomSeedTextBox.Radius = 6;
            irregularMinimumPeriodLabel = new AntdUI.Label();
            irregularMinimumPeriodTextBox = new AntdUI.Input();
            irregularMinimumPeriodTextBox.WaveSize = 0;
            irregularMinimumPeriodTextBox.Radius = 6;
            irregularMaximumPeriodLabel = new AntdUI.Label();
            irregularMaximumPeriodTextBox = new AntdUI.Input();
            irregularMaximumPeriodTextBox.WaveSize = 0;
            irregularMaximumPeriodTextBox.Radius = 6;
            irregularMinimumDifferencePeriodLabel = new AntdUI.Label();
            irregularMinimumDifferencePeriodTextBox = new AntdUI.Input();
            irregularMinimumDifferencePeriodTextBox.WaveSize = 0;
            irregularMinimumDifferencePeriodTextBox.Radius = 6;
            irregularMaximumDifferencePeriodLabel = new AntdUI.Label();
            irregularMaximumDifferencePeriodTextBox = new AntdUI.Input();
            irregularMaximumDifferencePeriodTextBox.WaveSize = 0;
            irregularMaximumDifferencePeriodTextBox.Radius = 6;
            irregularNegativeDirectionLabel = new AntdUI.Label();
            irregularNegativeDirectionTextBox = new AntdUI.Input();
            irregularNegativeDirectionTextBox.WaveSize = 0;
            irregularNegativeDirectionTextBox.Radius = 6;
            irregularPositiveDirectionLabel = new AntdUI.Label();
            irregularPositiveDirectionTextBox = new AntdUI.Input();
            irregularPositiveDirectionTextBox.WaveSize = 0;
            irregularPositiveDirectionTextBox.Radius = 6;
            irregularOutputLabel = new AntdUI.Label();
            irregularOutputTextBox = new AntdUI.Input();
            irregularOutputTextBox.WaveSize = 0;
            irregularOutputTextBox.Radius = 6;
            irregularBrowseOutputButton = new AntdUI.Button();
            irregularBrowseOutputButton.Radius = 6;
            irregularBrowseOutputButton.BorderWidth = 1F;
            irregularGenerateButton = new AntdUI.Button();
            irregularGenerateButton.Radius = 6;
            irregularStatusLabel = new AntdUI.Label();
            irregularPreviewGroup = new AntdUI.Panel();
            irregularPreviewGroupCaption = new AntdUI.Label();
            pagePanel.SuspendLayout();
            irregularLayout.SuspendLayout();
            irregularParameterGroup.SuspendLayout();
            irregularParameterLayout.SuspendLayout();
            irregularPreviewGroup.SuspendLayout();
            SuspendLayout();
            //
            // irregularPreview
            //
            irregularPreview.BackColor = Color.White;
            irregularPreview.Dock = DockStyle.Fill;
            irregularPreview.ForeColor = Color.FromArgb(29, 78, 216);
            irregularPreview.Location = new Point(10, 34);
            irregularPreview.Margin = new Padding(4);
            irregularPreview.MinimumSize = new Size(450, 270);
            irregularPreview.Name = "irregularPreview";
            irregularPreview.Size = new Size(582, 540);
            irregularPreview.TabIndex = 0;
            //
            // pagePanel
            //
            pagePanel.Controls.Add(irregularLayout);
            pagePanel.Dock = DockStyle.Fill;
            pagePanel.Location = new Point(0, 0);
            pagePanel.Name = "pagePanel";
            pagePanel.Padding = new Padding(8);
            pagePanel.Size = new Size(1104, 606);
            pagePanel.TabIndex = 0;
            //
            // irregularLayout
            //
            irregularLayout.ColumnCount = 2;
            irregularLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 480F));
            irregularLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            irregularLayout.Controls.Add(irregularParameterGroup, 0, 0);
            irregularLayout.Controls.Add(irregularPreviewGroup, 1, 0);
            irregularLayout.Dock = DockStyle.Fill;
            irregularLayout.Location = new Point(8, 8);
            irregularLayout.MinimumSize = new Size(930, 460);
            irregularLayout.Name = "irregularLayout";
            irregularLayout.RowCount = 1;
            irregularLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            irregularLayout.Size = new Size(1088, 590);
            irregularLayout.TabIndex = 0;
            //
            // irregularParameterGroup
            //
            irregularParameterGroup.BackColor = Color.White;
            irregularParameterGroup.Controls.Add(irregularParameterLayout);
            irregularParameterGroup.Dock = DockStyle.Fill;
            irregularParameterGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            irregularParameterGroup.Location = new Point(3, 3);
            irregularParameterGroup.Name = "irregularParameterGroup";
            irregularParameterGroup.Padding = new Padding(10, 32, 10, 10);
            irregularParameterGroup.Size = new Size(474, 584);
            irregularParameterGroup.TabIndex = 0;
            // AntdUI 面板用独立标题标签保留原分组标题，布局仍可在 Designer 编辑。
            irregularParameterGroupCaption.Text = "不规则波参数";
            irregularParameterGroupCaption.AutoSize = true;
            irregularParameterGroupCaption.Name = "irregularParameterGroupCaption";
            irregularParameterGroupCaption.Location = new Point(10, 2);
            irregularParameterGroupCaption.ForeColor = Color.FromArgb(15, 23, 42);
            irregularParameterGroup.BorderWidth = 1F;
            irregularParameterGroup.BorderColor = Color.FromArgb(203, 213, 225);
            irregularParameterGroup.Radius = 6;
            irregularParameterGroup.Controls.Add(irregularParameterGroupCaption);
            irregularParameterGroupCaption.BringToFront();
            //
            // irregularParameterLayout
            //
            irregularParameterLayout.AutoSize = true;
            irregularParameterLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            irregularParameterLayout.ColumnCount = 4;
            irregularParameterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            irregularParameterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115F));
            irregularParameterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            irregularParameterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            irregularParameterLayout.Controls.Add(irregularModeLabel, 0, 0);
            irregularParameterLayout.Controls.Add(irregularModeComboBox, 1, 0);
            irregularParameterLayout.Controls.Add(irregularTheoryLabel, 2, 0);
            irregularParameterLayout.Controls.Add(irregularTheoryComboBox, 3, 0);
            irregularParameterLayout.Controls.Add(irregularSpectrumLabel, 0, 1);
            irregularParameterLayout.Controls.Add(irregularSpectrumComboBox, 1, 1);
            irregularParameterLayout.Controls.Add(irregularSegmentLabel, 2, 1);
            irregularParameterLayout.Controls.Add(irregularSegmentComboBox, 3, 1);
            irregularParameterLayout.Controls.Add(irregularDirectionLabel, 0, 2);
            irregularParameterLayout.Controls.Add(irregularDirectionTextBox, 1, 2);
            irregularParameterLayout.Controls.Add(irregularDepthLabel, 2, 2);
            irregularParameterLayout.Controls.Add(irregularDepthTextBox, 3, 2);
            irregularParameterLayout.Controls.Add(irregularSignificantPeriodLabel, 0, 3);
            irregularParameterLayout.Controls.Add(irregularSignificantPeriodTextBox, 1, 3);
            irregularParameterLayout.Controls.Add(irregularSignificantHeightLabel, 2, 3);
            irregularParameterLayout.Controls.Add(irregularSignificantHeightTextBox, 3, 3);
            irregularParameterLayout.Controls.Add(irregularTimeStepLabel, 0, 4);
            irregularParameterLayout.Controls.Add(irregularTimeStepTextBox, 1, 4);
            irregularParameterLayout.Controls.Add(irregularSampleCountLabel, 2, 4);
            irregularParameterLayout.Controls.Add(irregularSampleCountTextBox, 3, 4);
            irregularParameterLayout.Controls.Add(irregularFrequencyLabel, 0, 5);
            irregularParameterLayout.Controls.Add(irregularCharacteristicFrequencyTextBox, 1, 5);
            irregularParameterLayout.Controls.Add(irregularCharacteristicPeriodLabel, 2, 5);
            irregularParameterLayout.Controls.Add(irregularCharacteristicPeriodTextBox, 3, 5);
            irregularParameterLayout.Controls.Add(irregularPeakFactorLabel, 0, 6);
            irregularParameterLayout.Controls.Add(irregularPeakFactorTextBox, 1, 6);
            irregularParameterLayout.Controls.Add(irregularRandomSeedLabel, 2, 6);
            irregularParameterLayout.Controls.Add(irregularRandomSeedTextBox, 3, 6);
            irregularParameterLayout.Controls.Add(irregularMinimumPeriodLabel, 0, 7);
            irregularParameterLayout.Controls.Add(irregularMinimumPeriodTextBox, 1, 7);
            irregularParameterLayout.Controls.Add(irregularMaximumPeriodLabel, 2, 7);
            irregularParameterLayout.Controls.Add(irregularMaximumPeriodTextBox, 3, 7);
            irregularParameterLayout.Controls.Add(irregularMinimumDifferencePeriodLabel, 0, 8);
            irregularParameterLayout.Controls.Add(irregularMinimumDifferencePeriodTextBox, 1, 8);
            irregularParameterLayout.Controls.Add(irregularMaximumDifferencePeriodLabel, 2, 8);
            irregularParameterLayout.Controls.Add(irregularMaximumDifferencePeriodTextBox, 3, 8);
            irregularParameterLayout.Controls.Add(irregularNegativeDirectionLabel, 0, 9);
            irregularParameterLayout.Controls.Add(irregularNegativeDirectionTextBox, 1, 9);
            irregularParameterLayout.Controls.Add(irregularPositiveDirectionLabel, 2, 9);
            irregularParameterLayout.Controls.Add(irregularPositiveDirectionTextBox, 3, 9);
            irregularParameterLayout.Controls.Add(irregularOutputLabel, 0, 10);
            irregularParameterLayout.Controls.Add(irregularOutputTextBox, 1, 10);
            irregularParameterLayout.Controls.Add(irregularBrowseOutputButton, 3, 10);
            irregularParameterLayout.Controls.Add(irregularGenerateButton, 0, 11);
            irregularParameterLayout.Controls.Add(irregularStatusLabel, 2, 11);
            irregularParameterLayout.Dock = DockStyle.Top;
            irregularParameterLayout.Location = new Point(10, 32);
            irregularParameterLayout.Name = "irregularParameterLayout";
            irregularParameterLayout.RowCount = 12;
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            irregularParameterLayout.Size = new Size(454, 418);
            irregularParameterLayout.TabIndex = 0;
            //
            // irregularModeLabel
            //
            irregularModeLabel.Dock = DockStyle.Fill;
            irregularModeLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularModeLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularModeLabel.Location = new Point(3, 0);
            irregularModeLabel.Name = "irregularModeLabel";
            irregularModeLabel.Size = new Size(94, 34);
            irregularModeLabel.TabIndex = 0;
            irregularModeLabel.Text = "造波模式";
            irregularModeLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularModeComboBox
            //
            irregularModeComboBox.BackColor = Color.White;
            irregularModeComboBox.Dock = DockStyle.Fill;
            irregularModeComboBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularModeComboBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularModeComboBox.Items.AddRange(new object[] { "单向不规则波", "多向不规则波" });
            irregularModeComboBox.Location = new Point(103, 5);
            irregularModeComboBox.Margin = new Padding(3, 5, 3, 5);
            irregularModeComboBox.Name = "irregularModeComboBox";
            irregularModeComboBox.Size = new Size(109, 25);
            irregularModeComboBox.TabIndex = 1;
            //
            // irregularTheoryLabel
            //
            irregularTheoryLabel.Dock = DockStyle.Fill;
            irregularTheoryLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularTheoryLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularTheoryLabel.Location = new Point(218, 0);
            irregularTheoryLabel.Name = "irregularTheoryLabel";
            irregularTheoryLabel.Size = new Size(94, 34);
            irregularTheoryLabel.TabIndex = 2;
            irregularTheoryLabel.Text = "造波理论";
            irregularTheoryLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularTheoryComboBox
            //
            irregularTheoryComboBox.BackColor = Color.White;
            irregularTheoryComboBox.Dock = DockStyle.Fill;
            irregularTheoryComboBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularTheoryComboBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularTheoryComboBox.Items.AddRange(new object[] { "线性理论", "非线性理论" });
            irregularTheoryComboBox.Location = new Point(318, 5);
            irregularTheoryComboBox.Margin = new Padding(3, 5, 3, 5);
            irregularTheoryComboBox.Name = "irregularTheoryComboBox";
            irregularTheoryComboBox.Size = new Size(133, 25);
            irregularTheoryComboBox.TabIndex = 3;
            //
            // irregularSpectrumLabel
            //
            irregularSpectrumLabel.Dock = DockStyle.Fill;
            irregularSpectrumLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularSpectrumLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularSpectrumLabel.Location = new Point(3, 34);
            irregularSpectrumLabel.Name = "irregularSpectrumLabel";
            irregularSpectrumLabel.Size = new Size(94, 34);
            irregularSpectrumLabel.TabIndex = 4;
            irregularSpectrumLabel.Text = "输入谱";
            irregularSpectrumLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularSpectrumComboBox
            //
            irregularSpectrumComboBox.BackColor = Color.White;
            irregularSpectrumComboBox.Dock = DockStyle.Fill;
            irregularSpectrumComboBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularSpectrumComboBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularSpectrumComboBox.Items.AddRange(new object[] { "JONSWAP", "Scott", "ITTC", "B谱", "Wallops", "P-M", "规范谱", "Darbyshire" });
            irregularSpectrumComboBox.Location = new Point(103, 39);
            irregularSpectrumComboBox.Margin = new Padding(3, 5, 3, 5);
            irregularSpectrumComboBox.Name = "irregularSpectrumComboBox";
            irregularSpectrumComboBox.Size = new Size(109, 25);
            irregularSpectrumComboBox.TabIndex = 5;
            //
            // irregularSegmentLabel
            //
            irregularSegmentLabel.Dock = DockStyle.Fill;
            irregularSegmentLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularSegmentLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularSegmentLabel.Location = new Point(218, 34);
            irregularSegmentLabel.Name = "irregularSegmentLabel";
            irregularSegmentLabel.Size = new Size(94, 34);
            irregularSegmentLabel.TabIndex = 6;
            irregularSegmentLabel.Text = "造波段";
            irregularSegmentLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularSegmentComboBox
            //
            irregularSegmentComboBox.BackColor = Color.White;
            irregularSegmentComboBox.Dock = DockStyle.Fill;
            irregularSegmentComboBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularSegmentComboBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularSegmentComboBox.Items.AddRange(new object[] { "X轴+Y轴", "X轴", "Y轴" });
            irregularSegmentComboBox.Location = new Point(318, 39);
            irregularSegmentComboBox.Margin = new Padding(3, 5, 3, 5);
            irregularSegmentComboBox.Name = "irregularSegmentComboBox";
            irregularSegmentComboBox.Size = new Size(133, 25);
            irregularSegmentComboBox.TabIndex = 7;
            //
            // irregularDirectionLabel
            //
            irregularDirectionLabel.Dock = DockStyle.Fill;
            irregularDirectionLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularDirectionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularDirectionLabel.Location = new Point(3, 68);
            irregularDirectionLabel.Name = "irregularDirectionLabel";
            irregularDirectionLabel.Size = new Size(94, 34);
            irregularDirectionLabel.TabIndex = 8;
            irregularDirectionLabel.Text = "主波向(°)";
            irregularDirectionLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularDirectionTextBox
            //
            irregularDirectionTextBox.BackColor = Color.White;
            irregularDirectionTextBox.Dock = DockStyle.Fill;
            irregularDirectionTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularDirectionTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularDirectionTextBox.Location = new Point(103, 73);
            irregularDirectionTextBox.Margin = new Padding(3, 5, 3, 5);
            irregularDirectionTextBox.Name = "irregularDirectionTextBox";
            irregularDirectionTextBox.Size = new Size(109, 23);
            irregularDirectionTextBox.TabIndex = 9;
            irregularDirectionTextBox.Text = "90";
            //
            // irregularDepthLabel
            //
            irregularDepthLabel.Dock = DockStyle.Fill;
            irregularDepthLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularDepthLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularDepthLabel.Location = new Point(218, 68);
            irregularDepthLabel.Name = "irregularDepthLabel";
            irregularDepthLabel.Size = new Size(94, 34);
            irregularDepthLabel.TabIndex = 10;
            irregularDepthLabel.Text = "水深(m)";
            irregularDepthLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularDepthTextBox
            //
            irregularDepthTextBox.BackColor = Color.White;
            irregularDepthTextBox.Dock = DockStyle.Fill;
            irregularDepthTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularDepthTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularDepthTextBox.Location = new Point(318, 73);
            irregularDepthTextBox.Margin = new Padding(3, 5, 3, 5);
            irregularDepthTextBox.Name = "irregularDepthTextBox";
            irregularDepthTextBox.Size = new Size(133, 23);
            irregularDepthTextBox.TabIndex = 11;
            irregularDepthTextBox.Text = "0.5";
            //
            // irregularSignificantPeriodLabel
            //
            irregularSignificantPeriodLabel.Dock = DockStyle.Fill;
            irregularSignificantPeriodLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularSignificantPeriodLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularSignificantPeriodLabel.Location = new Point(3, 102);
            irregularSignificantPeriodLabel.Name = "irregularSignificantPeriodLabel";
            irregularSignificantPeriodLabel.Size = new Size(94, 34);
            irregularSignificantPeriodLabel.TabIndex = 12;
            irregularSignificantPeriodLabel.Text = "有效周期(s)";
            irregularSignificantPeriodLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularSignificantPeriodTextBox
            //
            irregularSignificantPeriodTextBox.BackColor = Color.White;
            irregularSignificantPeriodTextBox.Dock = DockStyle.Fill;
            irregularSignificantPeriodTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularSignificantPeriodTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularSignificantPeriodTextBox.Location = new Point(103, 107);
            irregularSignificantPeriodTextBox.Margin = new Padding(3, 5, 3, 5);
            irregularSignificantPeriodTextBox.Name = "irregularSignificantPeriodTextBox";
            irregularSignificantPeriodTextBox.Size = new Size(109, 23);
            irregularSignificantPeriodTextBox.TabIndex = 13;
            irregularSignificantPeriodTextBox.Text = "1.5";
            //
            // irregularSignificantHeightLabel
            //
            irregularSignificantHeightLabel.Dock = DockStyle.Fill;
            irregularSignificantHeightLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularSignificantHeightLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularSignificantHeightLabel.Location = new Point(218, 102);
            irregularSignificantHeightLabel.Name = "irregularSignificantHeightLabel";
            irregularSignificantHeightLabel.Size = new Size(94, 34);
            irregularSignificantHeightLabel.TabIndex = 14;
            irregularSignificantHeightLabel.Text = "有效波高(m)";
            irregularSignificantHeightLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularSignificantHeightTextBox
            //
            irregularSignificantHeightTextBox.BackColor = Color.White;
            irregularSignificantHeightTextBox.Dock = DockStyle.Fill;
            irregularSignificantHeightTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularSignificantHeightTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularSignificantHeightTextBox.Location = new Point(318, 107);
            irregularSignificantHeightTextBox.Margin = new Padding(3, 5, 3, 5);
            irregularSignificantHeightTextBox.Name = "irregularSignificantHeightTextBox";
            irregularSignificantHeightTextBox.Size = new Size(133, 23);
            irregularSignificantHeightTextBox.TabIndex = 15;
            irregularSignificantHeightTextBox.Text = "0.1";
            //
            // irregularTimeStepLabel
            //
            irregularTimeStepLabel.Dock = DockStyle.Fill;
            irregularTimeStepLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularTimeStepLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularTimeStepLabel.Location = new Point(3, 136);
            irregularTimeStepLabel.Name = "irregularTimeStepLabel";
            irregularTimeStepLabel.Size = new Size(94, 34);
            irregularTimeStepLabel.TabIndex = 16;
            irregularTimeStepLabel.Text = "步长(s)";
            irregularTimeStepLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularTimeStepTextBox
            //
            irregularTimeStepTextBox.BackColor = Color.White;
            irregularTimeStepTextBox.Dock = DockStyle.Fill;
            irregularTimeStepTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularTimeStepTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularTimeStepTextBox.Location = new Point(103, 141);
            irregularTimeStepTextBox.Margin = new Padding(3, 5, 3, 5);
            irregularTimeStepTextBox.Name = "irregularTimeStepTextBox";
            irregularTimeStepTextBox.Size = new Size(109, 23);
            irregularTimeStepTextBox.TabIndex = 17;
            irregularTimeStepTextBox.Text = "0.02";
            //
            // irregularSampleCountLabel
            //
            irregularSampleCountLabel.Dock = DockStyle.Fill;
            irregularSampleCountLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularSampleCountLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularSampleCountLabel.Location = new Point(218, 136);
            irregularSampleCountLabel.Name = "irregularSampleCountLabel";
            irregularSampleCountLabel.Size = new Size(94, 34);
            irregularSampleCountLabel.TabIndex = 18;
            irregularSampleCountLabel.Text = "时序数";
            irregularSampleCountLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularSampleCountTextBox
            //
            irregularSampleCountTextBox.BackColor = Color.White;
            irregularSampleCountTextBox.Dock = DockStyle.Fill;
            irregularSampleCountTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularSampleCountTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularSampleCountTextBox.Location = new Point(318, 141);
            irregularSampleCountTextBox.Margin = new Padding(3, 5, 3, 5);
            irregularSampleCountTextBox.Name = "irregularSampleCountTextBox";
            irregularSampleCountTextBox.Size = new Size(133, 23);
            irregularSampleCountTextBox.TabIndex = 19;
            irregularSampleCountTextBox.Text = "8192";
            //
            // irregularFrequencyLabel
            //
            irregularFrequencyLabel.Dock = DockStyle.Fill;
            irregularFrequencyLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularFrequencyLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularFrequencyLabel.Location = new Point(3, 170);
            irregularFrequencyLabel.Name = "irregularFrequencyLabel";
            irregularFrequencyLabel.Size = new Size(94, 34);
            irregularFrequencyLabel.TabIndex = 20;
            irregularFrequencyLabel.Text = "特征频率";
            irregularFrequencyLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularCharacteristicFrequencyTextBox
            //
            irregularCharacteristicFrequencyTextBox.BackColor = Color.White;
            irregularCharacteristicFrequencyTextBox.Dock = DockStyle.Fill;
            irregularCharacteristicFrequencyTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularCharacteristicFrequencyTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularCharacteristicFrequencyTextBox.Location = new Point(103, 175);
            irregularCharacteristicFrequencyTextBox.Margin = new Padding(3, 5, 3, 5);
            irregularCharacteristicFrequencyTextBox.Name = "irregularCharacteristicFrequencyTextBox";
            irregularCharacteristicFrequencyTextBox.Size = new Size(109, 23);
            irregularCharacteristicFrequencyTextBox.TabIndex = 21;
            irregularCharacteristicFrequencyTextBox.Text = "0.514";
            //
            // irregularCharacteristicPeriodLabel
            //
            irregularCharacteristicPeriodLabel.Dock = DockStyle.Fill;
            irregularCharacteristicPeriodLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularCharacteristicPeriodLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularCharacteristicPeriodLabel.Location = new Point(218, 170);
            irregularCharacteristicPeriodLabel.Name = "irregularCharacteristicPeriodLabel";
            irregularCharacteristicPeriodLabel.Size = new Size(94, 34);
            irregularCharacteristicPeriodLabel.TabIndex = 22;
            irregularCharacteristicPeriodLabel.Text = "特征周期";
            irregularCharacteristicPeriodLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularCharacteristicPeriodTextBox
            //
            irregularCharacteristicPeriodTextBox.BackColor = Color.White;
            irregularCharacteristicPeriodTextBox.Dock = DockStyle.Fill;
            irregularCharacteristicPeriodTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularCharacteristicPeriodTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularCharacteristicPeriodTextBox.Location = new Point(318, 175);
            irregularCharacteristicPeriodTextBox.Margin = new Padding(3, 5, 3, 5);
            irregularCharacteristicPeriodTextBox.Name = "irregularCharacteristicPeriodTextBox";
            irregularCharacteristicPeriodTextBox.Size = new Size(133, 23);
            irregularCharacteristicPeriodTextBox.TabIndex = 23;
            irregularCharacteristicPeriodTextBox.Text = "3.00";
            //
            // irregularPeakFactorLabel
            //
            irregularPeakFactorLabel.Dock = DockStyle.Fill;
            irregularPeakFactorLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularPeakFactorLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularPeakFactorLabel.Location = new Point(3, 204);
            irregularPeakFactorLabel.Name = "irregularPeakFactorLabel";
            irregularPeakFactorLabel.Size = new Size(94, 34);
            irregularPeakFactorLabel.TabIndex = 24;
            irregularPeakFactorLabel.Text = "谱峰因子";
            irregularPeakFactorLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularPeakFactorTextBox
            //
            irregularPeakFactorTextBox.BackColor = Color.White;
            irregularPeakFactorTextBox.Dock = DockStyle.Fill;
            irregularPeakFactorTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularPeakFactorTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularPeakFactorTextBox.Location = new Point(103, 209);
            irregularPeakFactorTextBox.Margin = new Padding(3, 5, 3, 5);
            irregularPeakFactorTextBox.Name = "irregularPeakFactorTextBox";
            irregularPeakFactorTextBox.Size = new Size(109, 23);
            irregularPeakFactorTextBox.TabIndex = 25;
            irregularPeakFactorTextBox.Text = "3.3";
            //
            // irregularRandomSeedLabel
            //
            irregularRandomSeedLabel.Dock = DockStyle.Fill;
            irregularRandomSeedLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularRandomSeedLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularRandomSeedLabel.Location = new Point(218, 204);
            irregularRandomSeedLabel.Name = "irregularRandomSeedLabel";
            irregularRandomSeedLabel.Size = new Size(94, 34);
            irregularRandomSeedLabel.TabIndex = 26;
            irregularRandomSeedLabel.Text = "随机种子";
            irregularRandomSeedLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularRandomSeedTextBox
            //
            irregularRandomSeedTextBox.BackColor = Color.White;
            irregularRandomSeedTextBox.Dock = DockStyle.Fill;
            irregularRandomSeedTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularRandomSeedTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularRandomSeedTextBox.Location = new Point(318, 209);
            irregularRandomSeedTextBox.Margin = new Padding(3, 5, 3, 5);
            irregularRandomSeedTextBox.Name = "irregularRandomSeedTextBox";
            irregularRandomSeedTextBox.Size = new Size(133, 23);
            irregularRandomSeedTextBox.TabIndex = 27;
            irregularRandomSeedTextBox.Text = "12345";
            //
            // irregularMinimumPeriodLabel
            //
            irregularMinimumPeriodLabel.Dock = DockStyle.Fill;
            irregularMinimumPeriodLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularMinimumPeriodLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularMinimumPeriodLabel.Location = new Point(3, 238);
            irregularMinimumPeriodLabel.Name = "irregularMinimumPeriodLabel";
            irregularMinimumPeriodLabel.Size = new Size(94, 34);
            irregularMinimumPeriodLabel.TabIndex = 28;
            irregularMinimumPeriodLabel.Text = "最小周期";
            irregularMinimumPeriodLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularMinimumPeriodTextBox
            //
            irregularMinimumPeriodTextBox.BackColor = Color.White;
            irregularMinimumPeriodTextBox.Dock = DockStyle.Fill;
            irregularMinimumPeriodTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularMinimumPeriodTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularMinimumPeriodTextBox.Location = new Point(103, 243);
            irregularMinimumPeriodTextBox.Margin = new Padding(3, 5, 3, 5);
            irregularMinimumPeriodTextBox.Name = "irregularMinimumPeriodTextBox";
            irregularMinimumPeriodTextBox.Size = new Size(109, 23);
            irregularMinimumPeriodTextBox.TabIndex = 29;
            irregularMinimumPeriodTextBox.Text = "0.5";
            //
            // irregularMaximumPeriodLabel
            //
            irregularMaximumPeriodLabel.Dock = DockStyle.Fill;
            irregularMaximumPeriodLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularMaximumPeriodLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularMaximumPeriodLabel.Location = new Point(218, 238);
            irregularMaximumPeriodLabel.Name = "irregularMaximumPeriodLabel";
            irregularMaximumPeriodLabel.Size = new Size(94, 34);
            irregularMaximumPeriodLabel.TabIndex = 30;
            irregularMaximumPeriodLabel.Text = "最大周期";
            irregularMaximumPeriodLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularMaximumPeriodTextBox
            //
            irregularMaximumPeriodTextBox.BackColor = Color.White;
            irregularMaximumPeriodTextBox.Dock = DockStyle.Fill;
            irregularMaximumPeriodTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularMaximumPeriodTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularMaximumPeriodTextBox.Location = new Point(318, 243);
            irregularMaximumPeriodTextBox.Margin = new Padding(3, 5, 3, 5);
            irregularMaximumPeriodTextBox.Name = "irregularMaximumPeriodTextBox";
            irregularMaximumPeriodTextBox.Size = new Size(133, 23);
            irregularMaximumPeriodTextBox.TabIndex = 31;
            irregularMaximumPeriodTextBox.Text = "4.0";
            //
            // irregularMinimumDifferencePeriodLabel
            //
            irregularMinimumDifferencePeriodLabel.Dock = DockStyle.Fill;
            irregularMinimumDifferencePeriodLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularMinimumDifferencePeriodLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularMinimumDifferencePeriodLabel.Location = new Point(3, 272);
            irregularMinimumDifferencePeriodLabel.Name = "irregularMinimumDifferencePeriodLabel";
            irregularMinimumDifferencePeriodLabel.Size = new Size(94, 34);
            irregularMinimumDifferencePeriodLabel.TabIndex = 32;
            irregularMinimumDifferencePeriodLabel.Text = "最小差频";
            irregularMinimumDifferencePeriodLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularMinimumDifferencePeriodTextBox
            //
            irregularMinimumDifferencePeriodTextBox.BackColor = Color.White;
            irregularMinimumDifferencePeriodTextBox.Dock = DockStyle.Fill;
            irregularMinimumDifferencePeriodTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularMinimumDifferencePeriodTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularMinimumDifferencePeriodTextBox.Location = new Point(103, 277);
            irregularMinimumDifferencePeriodTextBox.Margin = new Padding(3, 5, 3, 5);
            irregularMinimumDifferencePeriodTextBox.Name = "irregularMinimumDifferencePeriodTextBox";
            irregularMinimumDifferencePeriodTextBox.Size = new Size(109, 23);
            irregularMinimumDifferencePeriodTextBox.TabIndex = 33;
            irregularMinimumDifferencePeriodTextBox.Text = "0.2";
            //
            // irregularMaximumDifferencePeriodLabel
            //
            irregularMaximumDifferencePeriodLabel.Dock = DockStyle.Fill;
            irregularMaximumDifferencePeriodLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularMaximumDifferencePeriodLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularMaximumDifferencePeriodLabel.Location = new Point(218, 272);
            irregularMaximumDifferencePeriodLabel.Name = "irregularMaximumDifferencePeriodLabel";
            irregularMaximumDifferencePeriodLabel.Size = new Size(94, 34);
            irregularMaximumDifferencePeriodLabel.TabIndex = 34;
            irregularMaximumDifferencePeriodLabel.Text = "最大差频";
            irregularMaximumDifferencePeriodLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularMaximumDifferencePeriodTextBox
            //
            irregularMaximumDifferencePeriodTextBox.BackColor = Color.White;
            irregularMaximumDifferencePeriodTextBox.Dock = DockStyle.Fill;
            irregularMaximumDifferencePeriodTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularMaximumDifferencePeriodTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularMaximumDifferencePeriodTextBox.Location = new Point(318, 277);
            irregularMaximumDifferencePeriodTextBox.Margin = new Padding(3, 5, 3, 5);
            irregularMaximumDifferencePeriodTextBox.Name = "irregularMaximumDifferencePeriodTextBox";
            irregularMaximumDifferencePeriodTextBox.Size = new Size(133, 23);
            irregularMaximumDifferencePeriodTextBox.TabIndex = 35;
            irregularMaximumDifferencePeriodTextBox.Text = "10";
            //
            // irregularNegativeDirectionLabel
            //
            irregularNegativeDirectionLabel.Dock = DockStyle.Fill;
            irregularNegativeDirectionLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularNegativeDirectionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularNegativeDirectionLabel.Location = new Point(3, 306);
            irregularNegativeDirectionLabel.Name = "irregularNegativeDirectionLabel";
            irregularNegativeDirectionLabel.Size = new Size(94, 34);
            irregularNegativeDirectionLabel.TabIndex = 36;
            irregularNegativeDirectionLabel.Text = "负向偏角";
            irregularNegativeDirectionLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularNegativeDirectionTextBox
            //
            irregularNegativeDirectionTextBox.BackColor = Color.White;
            irregularNegativeDirectionTextBox.Dock = DockStyle.Fill;
            irregularNegativeDirectionTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularNegativeDirectionTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularNegativeDirectionTextBox.Location = new Point(103, 311);
            irregularNegativeDirectionTextBox.Margin = new Padding(3, 5, 3, 5);
            irregularNegativeDirectionTextBox.Name = "irregularNegativeDirectionTextBox";
            irregularNegativeDirectionTextBox.Size = new Size(109, 23);
            irregularNegativeDirectionTextBox.TabIndex = 37;
            irregularNegativeDirectionTextBox.Text = "-25";
            //
            // irregularPositiveDirectionLabel
            //
            irregularPositiveDirectionLabel.Dock = DockStyle.Fill;
            irregularPositiveDirectionLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularPositiveDirectionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularPositiveDirectionLabel.Location = new Point(218, 306);
            irregularPositiveDirectionLabel.Name = "irregularPositiveDirectionLabel";
            irregularPositiveDirectionLabel.Size = new Size(94, 34);
            irregularPositiveDirectionLabel.TabIndex = 38;
            irregularPositiveDirectionLabel.Text = "正向偏角";
            irregularPositiveDirectionLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularPositiveDirectionTextBox
            //
            irregularPositiveDirectionTextBox.BackColor = Color.White;
            irregularPositiveDirectionTextBox.Dock = DockStyle.Fill;
            irregularPositiveDirectionTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularPositiveDirectionTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularPositiveDirectionTextBox.Location = new Point(318, 311);
            irregularPositiveDirectionTextBox.Margin = new Padding(3, 5, 3, 5);
            irregularPositiveDirectionTextBox.Name = "irregularPositiveDirectionTextBox";
            irregularPositiveDirectionTextBox.Size = new Size(133, 23);
            irregularPositiveDirectionTextBox.TabIndex = 39;
            irregularPositiveDirectionTextBox.Text = "25";
            //
            // irregularOutputLabel
            //
            irregularOutputLabel.Dock = DockStyle.Fill;
            irregularOutputLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularOutputLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularOutputLabel.Location = new Point(3, 340);
            irregularOutputLabel.Name = "irregularOutputLabel";
            irregularOutputLabel.Size = new Size(94, 34);
            irregularOutputLabel.TabIndex = 40;
            irregularOutputLabel.Text = "保存文件";
            irregularOutputLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularOutputTextBox
            //
            irregularOutputTextBox.BackColor = Color.White;
            irregularParameterLayout.SetColumnSpan(irregularOutputTextBox, 2);
            irregularOutputTextBox.Dock = DockStyle.Fill;
            irregularOutputTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularOutputTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularOutputTextBox.Location = new Point(103, 345);
            irregularOutputTextBox.Margin = new Padding(3, 5, 3, 5);
            irregularOutputTextBox.Name = "irregularOutputTextBox";
            irregularOutputTextBox.Size = new Size(209, 23);
            irregularOutputTextBox.TabIndex = 41;
            //
            // irregularBrowseOutputButton
            //
            irregularBrowseOutputButton.BackColor = Color.FromArgb(248, 250, 252);
            irregularBrowseOutputButton.DefaultBack = Color.FromArgb(248, 250, 252);
            irregularBrowseOutputButton.Cursor = Cursors.Hand;
            irregularBrowseOutputButton.Dock = DockStyle.Fill;
            irregularBrowseOutputButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
            irregularBrowseOutputButton.BackHover = Color.FromArgb(226, 232, 240);
            irregularBrowseOutputButton.ForeColor = Color.FromArgb(15, 23, 42);
            irregularBrowseOutputButton.Location = new Point(318, 344);
            irregularBrowseOutputButton.Margin = new Padding(3, 4, 3, 4);
            irregularBrowseOutputButton.Name = "irregularBrowseOutputButton";
            irregularBrowseOutputButton.Size = new Size(133, 26);
            irregularBrowseOutputButton.TabIndex = 42;
            irregularBrowseOutputButton.Text = "浏览";
            irregularBrowseOutputButton.Click += BrowseIrregularOutputButton_Click;
            //
            // irregularGenerateButton
            //
            irregularGenerateButton.BackColor = Color.FromArgb(29, 78, 216);
            irregularGenerateButton.DefaultBack = Color.FromArgb(29, 78, 216);
            irregularParameterLayout.SetColumnSpan(irregularGenerateButton, 2);
            irregularGenerateButton.Cursor = Cursors.Hand;
            irregularGenerateButton.BorderWidth = 0;
            irregularGenerateButton.BackHover = Color.FromArgb(30, 64, 175);
            irregularGenerateButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            irregularGenerateButton.ForeColor = Color.White;
            irregularGenerateButton.Location = new Point(3, 377);
            irregularGenerateButton.Name = "irregularGenerateButton";
            irregularGenerateButton.Size = new Size(160, 34);
            irregularGenerateButton.TabIndex = 43;
            irregularGenerateButton.Text = "计算并保存";
            irregularGenerateButton.Click += GenerateIrregularButton_Click;
            //
            // irregularStatusLabel
            //
            irregularStatusLabel.AutoEllipsis = true;
            irregularParameterLayout.SetColumnSpan(irregularStatusLabel, 2);
            irregularStatusLabel.Dock = DockStyle.Fill;
            irregularStatusLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularStatusLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularStatusLabel.Location = new Point(218, 374);
            irregularStatusLabel.Name = "irregularStatusLabel";
            irregularStatusLabel.Size = new Size(233, 44);
            irregularStatusLabel.TabIndex = 44;
            irregularStatusLabel.Text = "请选择 CSV 保存路径";
            irregularStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // irregularPreviewGroup
            //
            irregularPreviewGroup.BackColor = Color.White;
            irregularPreviewGroup.Controls.Add(irregularPreview);
            irregularPreviewGroup.Dock = DockStyle.Fill;
            irregularPreviewGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            irregularPreviewGroup.Location = new Point(483, 3);
            irregularPreviewGroup.Name = "irregularPreviewGroup";
            irregularPreviewGroup.Padding = new Padding(10, 32, 10, 10);
            irregularPreviewGroup.Size = new Size(602, 584);
            irregularPreviewGroup.TabIndex = 1;
            // AntdUI 面板用独立标题标签保留原分组标题，布局仍可在 Designer 编辑。
            irregularPreviewGroupCaption.Text = "波形预览";
            irregularPreviewGroupCaption.AutoSize = true;
            irregularPreviewGroupCaption.Name = "irregularPreviewGroupCaption";
            irregularPreviewGroupCaption.Location = new Point(10, 2);
            irregularPreviewGroupCaption.ForeColor = Color.FromArgb(15, 23, 42);
            irregularPreviewGroup.BorderWidth = 1F;
            irregularPreviewGroup.BorderColor = Color.FromArgb(203, 213, 225);
            irregularPreviewGroup.Radius = 6;
            irregularPreviewGroup.Controls.Add(irregularPreviewGroupCaption);
            irregularPreviewGroupCaption.BringToFront();
            //
            // IrregularWavePage
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            AutoScrollMinSize = new Size(946, 476);
            BackColor = Color.FromArgb(241, 245, 249);
            Controls.Add(pagePanel);
            Font = new Font("Microsoft YaHei UI", 9F);
            ForeColor = Color.FromArgb(15, 23, 42);
            Name = "IrregularWavePage";
            Size = new Size(1104, 606);
            pagePanel.ResumeLayout(false);
            irregularLayout.ResumeLayout(false);
            irregularParameterGroup.ResumeLayout(false);
            irregularParameterGroup.PerformLayout();
            irregularParameterLayout.ResumeLayout(false);
            irregularParameterLayout.PerformLayout();
            irregularPreviewGroup.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
