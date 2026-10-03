namespace Page_switching.panel
{
    partial class IrregularWavePage
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pagePanel;
        private TableLayoutPanel irregularLayout;
        private GroupBox irregularParameterGroup;
        private TableLayoutPanel irregularParameterLayout;
        private Label irregularModeLabel;
        internal ComboBox irregularModeComboBox;
        private Label irregularTheoryLabel;
        internal ComboBox irregularTheoryComboBox;
        private Label irregularSpectrumLabel;
        internal ComboBox irregularSpectrumComboBox;
        private Label irregularSegmentLabel;
        internal ComboBox irregularSegmentComboBox;
        private Label irregularDirectionLabel;
        internal TextBox irregularDirectionTextBox;
        private Label irregularDepthLabel;
        internal TextBox irregularDepthTextBox;
        private Label irregularSignificantPeriodLabel;
        internal TextBox irregularSignificantPeriodTextBox;
        private Label irregularSignificantHeightLabel;
        internal TextBox irregularSignificantHeightTextBox;
        private Label irregularTimeStepLabel;
        internal TextBox irregularTimeStepTextBox;
        private Label irregularSampleCountLabel;
        internal TextBox irregularSampleCountTextBox;
        private Label irregularFrequencyLabel;
        internal TextBox irregularCharacteristicFrequencyTextBox;
        private Label irregularCharacteristicPeriodLabel;
        internal TextBox irregularCharacteristicPeriodTextBox;
        private Label irregularPeakFactorLabel;
        internal TextBox irregularPeakFactorTextBox;
        private Label irregularRandomSeedLabel;
        internal TextBox irregularRandomSeedTextBox;
        private Label irregularMinimumPeriodLabel;
        internal TextBox irregularMinimumPeriodTextBox;
        private Label irregularMaximumPeriodLabel;
        internal TextBox irregularMaximumPeriodTextBox;
        private Label irregularMinimumDifferencePeriodLabel;
        internal TextBox irregularMinimumDifferencePeriodTextBox;
        private Label irregularMaximumDifferencePeriodLabel;
        internal TextBox irregularMaximumDifferencePeriodTextBox;
        private Label irregularNegativeDirectionLabel;
        internal TextBox irregularNegativeDirectionTextBox;
        private Label irregularPositiveDirectionLabel;
        internal TextBox irregularPositiveDirectionTextBox;
        private Label irregularOutputLabel;
        internal TextBox irregularOutputTextBox;
        internal Button irregularBrowseOutputButton;
        internal Button irregularGenerateButton;
        internal Label irregularStatusLabel;
        private GroupBox irregularPreviewGroup;
        internal WaveformPreviewControl irregularPreview;

        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pagePanel = new Panel();
            irregularLayout = new TableLayoutPanel();
            irregularParameterGroup = new GroupBox();
            irregularParameterLayout = new TableLayoutPanel();
            irregularModeLabel = new Label();
            irregularModeComboBox = new ComboBox();
            irregularTheoryLabel = new Label();
            irregularTheoryComboBox = new ComboBox();
            irregularSpectrumLabel = new Label();
            irregularSpectrumComboBox = new ComboBox();
            irregularSegmentLabel = new Label();
            irregularSegmentComboBox = new ComboBox();
            irregularDirectionLabel = new Label();
            irregularDirectionTextBox = new TextBox();
            irregularDepthLabel = new Label();
            irregularDepthTextBox = new TextBox();
            irregularSignificantPeriodLabel = new Label();
            irregularSignificantPeriodTextBox = new TextBox();
            irregularSignificantHeightLabel = new Label();
            irregularSignificantHeightTextBox = new TextBox();
            irregularTimeStepLabel = new Label();
            irregularTimeStepTextBox = new TextBox();
            irregularSampleCountLabel = new Label();
            irregularSampleCountTextBox = new TextBox();
            irregularFrequencyLabel = new Label();
            irregularCharacteristicFrequencyTextBox = new TextBox();
            irregularCharacteristicPeriodLabel = new Label();
            irregularCharacteristicPeriodTextBox = new TextBox();
            irregularPeakFactorLabel = new Label();
            irregularPeakFactorTextBox = new TextBox();
            irregularRandomSeedLabel = new Label();
            irregularRandomSeedTextBox = new TextBox();
            irregularMinimumPeriodLabel = new Label();
            irregularMinimumPeriodTextBox = new TextBox();
            irregularMaximumPeriodLabel = new Label();
            irregularMaximumPeriodTextBox = new TextBox();
            irregularMinimumDifferencePeriodLabel = new Label();
            irregularMinimumDifferencePeriodTextBox = new TextBox();
            irregularMaximumDifferencePeriodLabel = new Label();
            irregularMaximumDifferencePeriodTextBox = new TextBox();
            irregularNegativeDirectionLabel = new Label();
            irregularNegativeDirectionTextBox = new TextBox();
            irregularPositiveDirectionLabel = new Label();
            irregularPositiveDirectionTextBox = new TextBox();
            irregularOutputLabel = new Label();
            irregularOutputTextBox = new TextBox();
            irregularBrowseOutputButton = new Button();
            irregularGenerateButton = new Button();
            irregularStatusLabel = new Label();
            irregularPreviewGroup = new GroupBox();
            irregularPreview = new WaveformPreviewControl();
            pagePanel.SuspendLayout();
            irregularLayout.SuspendLayout();
            irregularParameterGroup.SuspendLayout();
            irregularParameterLayout.SuspendLayout();
            irregularPreviewGroup.SuspendLayout();
            SuspendLayout();
            // 
            // pagePanel
            // 
            pagePanel.Controls.Add(irregularLayout);
            pagePanel.Dock = DockStyle.Fill;
            pagePanel.Location = new Point(0, 0);
            pagePanel.Margin = new Padding(4, 4, 4, 4);
            pagePanel.Name = "pagePanel";
            pagePanel.Padding = new Padding(12, 12, 12, 12);
            pagePanel.Size = new Size(1656, 909);
            pagePanel.TabIndex = 0;
            // 
            // irregularLayout
            // 
            irregularLayout.ColumnCount = 2;
            irregularLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 720F));
            irregularLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            irregularLayout.Controls.Add(irregularParameterGroup, 0, 0);
            irregularLayout.Controls.Add(irregularPreviewGroup, 1, 0);
            irregularLayout.Dock = DockStyle.Fill;
            irregularLayout.Location = new Point(12, 12);
            irregularLayout.Margin = new Padding(4, 4, 4, 4);
            irregularLayout.MinimumSize = new Size(1395, 690);
            irregularLayout.Name = "irregularLayout";
            irregularLayout.RowCount = 1;
            irregularLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            irregularLayout.Size = new Size(1632, 885);
            irregularLayout.TabIndex = 0;
            // 
            // irregularParameterGroup
            // 
            irregularParameterGroup.BackColor = Color.White;
            irregularParameterGroup.Controls.Add(irregularParameterLayout);
            irregularParameterGroup.Dock = DockStyle.Fill;
            irregularParameterGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            irregularParameterGroup.Location = new Point(4, 4);
            irregularParameterGroup.Margin = new Padding(4, 4, 4, 4);
            irregularParameterGroup.Name = "irregularParameterGroup";
            irregularParameterGroup.Padding = new Padding(15, 24, 15, 15);
            irregularParameterGroup.Size = new Size(712, 877);
            irregularParameterGroup.TabIndex = 0;
            irregularParameterGroup.TabStop = false;
            irregularParameterGroup.Text = "不规则波参数";
            // 
            // irregularParameterLayout
            // 
            irregularParameterLayout.AutoSize = true;
            irregularParameterLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            irregularParameterLayout.ColumnCount = 4;
            irregularParameterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            irregularParameterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 172F));
            irregularParameterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
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
            irregularParameterLayout.Location = new Point(15, 47);
            irregularParameterLayout.Margin = new Padding(4, 4, 4, 4);
            irregularParameterLayout.Name = "irregularParameterLayout";
            irregularParameterLayout.RowCount = 12;
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 66F));
            irregularParameterLayout.Size = new Size(682, 627);
            irregularParameterLayout.TabIndex = 0;
            // 
            // irregularModeLabel
            // 
            irregularModeLabel.Dock = DockStyle.Fill;
            irregularModeLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularModeLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularModeLabel.Location = new Point(4, 0);
            irregularModeLabel.Margin = new Padding(4, 0, 4, 0);
            irregularModeLabel.Name = "irregularModeLabel";
            irregularModeLabel.Size = new Size(142, 51);
            irregularModeLabel.TabIndex = 0;
            irregularModeLabel.Text = "造波模式";
            irregularModeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularModeComboBox
            // 
            irregularModeComboBox.BackColor = Color.White;
            irregularModeComboBox.Dock = DockStyle.Fill;
            irregularModeComboBox.FlatStyle = FlatStyle.Flat;
            irregularModeComboBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularModeComboBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularModeComboBox.Items.AddRange(new object[] { "单向不规则波", "多向不规则波" });
            irregularModeComboBox.Location = new Point(154, 8);
            irregularModeComboBox.Margin = new Padding(4, 8, 4, 8);
            irregularModeComboBox.Name = "irregularModeComboBox";
            irregularModeComboBox.Size = new Size(164, 32);
            irregularModeComboBox.TabIndex = 1;
            // 
            // irregularTheoryLabel
            // 
            irregularTheoryLabel.Dock = DockStyle.Fill;
            irregularTheoryLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularTheoryLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularTheoryLabel.Location = new Point(326, 0);
            irregularTheoryLabel.Margin = new Padding(4, 0, 4, 0);
            irregularTheoryLabel.Name = "irregularTheoryLabel";
            irregularTheoryLabel.Size = new Size(142, 51);
            irregularTheoryLabel.TabIndex = 2;
            irregularTheoryLabel.Text = "造波理论";
            irregularTheoryLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularTheoryComboBox
            // 
            irregularTheoryComboBox.BackColor = Color.White;
            irregularTheoryComboBox.Dock = DockStyle.Fill;
            irregularTheoryComboBox.FlatStyle = FlatStyle.Flat;
            irregularTheoryComboBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularTheoryComboBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularTheoryComboBox.Items.AddRange(new object[] { "线性理论", "非线性理论" });
            irregularTheoryComboBox.Location = new Point(476, 8);
            irregularTheoryComboBox.Margin = new Padding(4, 8, 4, 8);
            irregularTheoryComboBox.Name = "irregularTheoryComboBox";
            irregularTheoryComboBox.Size = new Size(202, 32);
            irregularTheoryComboBox.TabIndex = 3;
            // 
            // irregularSpectrumLabel
            // 
            irregularSpectrumLabel.Dock = DockStyle.Fill;
            irregularSpectrumLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularSpectrumLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularSpectrumLabel.Location = new Point(4, 51);
            irregularSpectrumLabel.Margin = new Padding(4, 0, 4, 0);
            irregularSpectrumLabel.Name = "irregularSpectrumLabel";
            irregularSpectrumLabel.Size = new Size(142, 51);
            irregularSpectrumLabel.TabIndex = 4;
            irregularSpectrumLabel.Text = "输入谱";
            irregularSpectrumLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularSpectrumComboBox
            // 
            irregularSpectrumComboBox.BackColor = Color.White;
            irregularSpectrumComboBox.Dock = DockStyle.Fill;
            irregularSpectrumComboBox.FlatStyle = FlatStyle.Flat;
            irregularSpectrumComboBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularSpectrumComboBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularSpectrumComboBox.Items.AddRange(new object[] { "JONSWAP", "Scott", "ITTC", "B谱", "Wallops", "P-M", "规范谱", "Darbyshire" });
            irregularSpectrumComboBox.Location = new Point(154, 59);
            irregularSpectrumComboBox.Margin = new Padding(4, 8, 4, 8);
            irregularSpectrumComboBox.Name = "irregularSpectrumComboBox";
            irregularSpectrumComboBox.Size = new Size(164, 32);
            irregularSpectrumComboBox.TabIndex = 5;
            // 
            // irregularSegmentLabel
            // 
            irregularSegmentLabel.Dock = DockStyle.Fill;
            irregularSegmentLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularSegmentLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularSegmentLabel.Location = new Point(326, 51);
            irregularSegmentLabel.Margin = new Padding(4, 0, 4, 0);
            irregularSegmentLabel.Name = "irregularSegmentLabel";
            irregularSegmentLabel.Size = new Size(142, 51);
            irregularSegmentLabel.TabIndex = 6;
            irregularSegmentLabel.Text = "造波段";
            irregularSegmentLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularSegmentComboBox
            // 
            irregularSegmentComboBox.BackColor = Color.White;
            irregularSegmentComboBox.Dock = DockStyle.Fill;
            irregularSegmentComboBox.FlatStyle = FlatStyle.Flat;
            irregularSegmentComboBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularSegmentComboBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularSegmentComboBox.Items.AddRange(new object[] { "X轴+Y轴", "X轴", "Y轴" });
            irregularSegmentComboBox.Location = new Point(476, 59);
            irregularSegmentComboBox.Margin = new Padding(4, 8, 4, 8);
            irregularSegmentComboBox.Name = "irregularSegmentComboBox";
            irregularSegmentComboBox.Size = new Size(202, 32);
            irregularSegmentComboBox.TabIndex = 7;
            // 
            // irregularDirectionLabel
            // 
            irregularDirectionLabel.Dock = DockStyle.Fill;
            irregularDirectionLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularDirectionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularDirectionLabel.Location = new Point(4, 102);
            irregularDirectionLabel.Margin = new Padding(4, 0, 4, 0);
            irregularDirectionLabel.Name = "irregularDirectionLabel";
            irregularDirectionLabel.Size = new Size(142, 51);
            irregularDirectionLabel.TabIndex = 8;
            irregularDirectionLabel.Text = "主波向(°)";
            irregularDirectionLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularDirectionTextBox
            // 
            irregularDirectionTextBox.BackColor = Color.White;
            irregularDirectionTextBox.BorderStyle = BorderStyle.FixedSingle;
            irregularDirectionTextBox.Dock = DockStyle.Fill;
            irregularDirectionTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularDirectionTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularDirectionTextBox.Location = new Point(154, 110);
            irregularDirectionTextBox.Margin = new Padding(4, 8, 4, 8);
            irregularDirectionTextBox.Name = "irregularDirectionTextBox";
            irregularDirectionTextBox.Size = new Size(164, 30);
            irregularDirectionTextBox.TabIndex = 9;
            irregularDirectionTextBox.Text = "90";
            // 
            // irregularDepthLabel
            // 
            irregularDepthLabel.Dock = DockStyle.Fill;
            irregularDepthLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularDepthLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularDepthLabel.Location = new Point(326, 102);
            irregularDepthLabel.Margin = new Padding(4, 0, 4, 0);
            irregularDepthLabel.Name = "irregularDepthLabel";
            irregularDepthLabel.Size = new Size(142, 51);
            irregularDepthLabel.TabIndex = 10;
            irregularDepthLabel.Text = "水深(m)";
            irregularDepthLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularDepthTextBox
            // 
            irregularDepthTextBox.BackColor = Color.White;
            irregularDepthTextBox.BorderStyle = BorderStyle.FixedSingle;
            irregularDepthTextBox.Dock = DockStyle.Fill;
            irregularDepthTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularDepthTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularDepthTextBox.Location = new Point(476, 110);
            irregularDepthTextBox.Margin = new Padding(4, 8, 4, 8);
            irregularDepthTextBox.Name = "irregularDepthTextBox";
            irregularDepthTextBox.Size = new Size(202, 30);
            irregularDepthTextBox.TabIndex = 11;
            irregularDepthTextBox.Text = "0.5";
            // 
            // irregularSignificantPeriodLabel
            // 
            irregularSignificantPeriodLabel.Dock = DockStyle.Fill;
            irregularSignificantPeriodLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularSignificantPeriodLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularSignificantPeriodLabel.Location = new Point(4, 153);
            irregularSignificantPeriodLabel.Margin = new Padding(4, 0, 4, 0);
            irregularSignificantPeriodLabel.Name = "irregularSignificantPeriodLabel";
            irregularSignificantPeriodLabel.Size = new Size(142, 51);
            irregularSignificantPeriodLabel.TabIndex = 12;
            irregularSignificantPeriodLabel.Text = "有效周期(s)";
            irregularSignificantPeriodLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularSignificantPeriodTextBox
            // 
            irregularSignificantPeriodTextBox.BackColor = Color.White;
            irregularSignificantPeriodTextBox.BorderStyle = BorderStyle.FixedSingle;
            irregularSignificantPeriodTextBox.Dock = DockStyle.Fill;
            irregularSignificantPeriodTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularSignificantPeriodTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularSignificantPeriodTextBox.Location = new Point(154, 161);
            irregularSignificantPeriodTextBox.Margin = new Padding(4, 8, 4, 8);
            irregularSignificantPeriodTextBox.Name = "irregularSignificantPeriodTextBox";
            irregularSignificantPeriodTextBox.Size = new Size(164, 30);
            irregularSignificantPeriodTextBox.TabIndex = 13;
            irregularSignificantPeriodTextBox.Text = "1.5";
            // 
            // irregularSignificantHeightLabel
            // 
            irregularSignificantHeightLabel.Dock = DockStyle.Fill;
            irregularSignificantHeightLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularSignificantHeightLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularSignificantHeightLabel.Location = new Point(326, 153);
            irregularSignificantHeightLabel.Margin = new Padding(4, 0, 4, 0);
            irregularSignificantHeightLabel.Name = "irregularSignificantHeightLabel";
            irregularSignificantHeightLabel.Size = new Size(142, 51);
            irregularSignificantHeightLabel.TabIndex = 14;
            irregularSignificantHeightLabel.Text = "有效波高(m)";
            irregularSignificantHeightLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularSignificantHeightTextBox
            // 
            irregularSignificantHeightTextBox.BackColor = Color.White;
            irregularSignificantHeightTextBox.BorderStyle = BorderStyle.FixedSingle;
            irregularSignificantHeightTextBox.Dock = DockStyle.Fill;
            irregularSignificantHeightTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularSignificantHeightTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularSignificantHeightTextBox.Location = new Point(476, 161);
            irregularSignificantHeightTextBox.Margin = new Padding(4, 8, 4, 8);
            irregularSignificantHeightTextBox.Name = "irregularSignificantHeightTextBox";
            irregularSignificantHeightTextBox.Size = new Size(202, 30);
            irregularSignificantHeightTextBox.TabIndex = 15;
            irregularSignificantHeightTextBox.Text = "0.1";
            // 
            // irregularTimeStepLabel
            // 
            irregularTimeStepLabel.Dock = DockStyle.Fill;
            irregularTimeStepLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularTimeStepLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularTimeStepLabel.Location = new Point(4, 204);
            irregularTimeStepLabel.Margin = new Padding(4, 0, 4, 0);
            irregularTimeStepLabel.Name = "irregularTimeStepLabel";
            irregularTimeStepLabel.Size = new Size(142, 51);
            irregularTimeStepLabel.TabIndex = 16;
            irregularTimeStepLabel.Text = "步长(s)";
            irregularTimeStepLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularTimeStepTextBox
            // 
            irregularTimeStepTextBox.BackColor = Color.White;
            irregularTimeStepTextBox.BorderStyle = BorderStyle.FixedSingle;
            irregularTimeStepTextBox.Dock = DockStyle.Fill;
            irregularTimeStepTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularTimeStepTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularTimeStepTextBox.Location = new Point(154, 212);
            irregularTimeStepTextBox.Margin = new Padding(4, 8, 4, 8);
            irregularTimeStepTextBox.Name = "irregularTimeStepTextBox";
            irregularTimeStepTextBox.Size = new Size(164, 30);
            irregularTimeStepTextBox.TabIndex = 17;
            irregularTimeStepTextBox.Text = "0.02";
            // 
            // irregularSampleCountLabel
            // 
            irregularSampleCountLabel.Dock = DockStyle.Fill;
            irregularSampleCountLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularSampleCountLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularSampleCountLabel.Location = new Point(326, 204);
            irregularSampleCountLabel.Margin = new Padding(4, 0, 4, 0);
            irregularSampleCountLabel.Name = "irregularSampleCountLabel";
            irregularSampleCountLabel.Size = new Size(142, 51);
            irregularSampleCountLabel.TabIndex = 18;
            irregularSampleCountLabel.Text = "时序数";
            irregularSampleCountLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularSampleCountTextBox
            // 
            irregularSampleCountTextBox.BackColor = Color.White;
            irregularSampleCountTextBox.BorderStyle = BorderStyle.FixedSingle;
            irregularSampleCountTextBox.Dock = DockStyle.Fill;
            irregularSampleCountTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularSampleCountTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularSampleCountTextBox.Location = new Point(476, 212);
            irregularSampleCountTextBox.Margin = new Padding(4, 8, 4, 8);
            irregularSampleCountTextBox.Name = "irregularSampleCountTextBox";
            irregularSampleCountTextBox.Size = new Size(202, 30);
            irregularSampleCountTextBox.TabIndex = 19;
            irregularSampleCountTextBox.Text = "8192";
            // 
            // irregularFrequencyLabel
            // 
            irregularFrequencyLabel.Dock = DockStyle.Fill;
            irregularFrequencyLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularFrequencyLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularFrequencyLabel.Location = new Point(4, 255);
            irregularFrequencyLabel.Margin = new Padding(4, 0, 4, 0);
            irregularFrequencyLabel.Name = "irregularFrequencyLabel";
            irregularFrequencyLabel.Size = new Size(142, 51);
            irregularFrequencyLabel.TabIndex = 20;
            irregularFrequencyLabel.Text = "特征频率";
            irregularFrequencyLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularCharacteristicFrequencyTextBox
            // 
            irregularCharacteristicFrequencyTextBox.BackColor = Color.White;
            irregularCharacteristicFrequencyTextBox.BorderStyle = BorderStyle.FixedSingle;
            irregularCharacteristicFrequencyTextBox.Dock = DockStyle.Fill;
            irregularCharacteristicFrequencyTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularCharacteristicFrequencyTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularCharacteristicFrequencyTextBox.Location = new Point(154, 263);
            irregularCharacteristicFrequencyTextBox.Margin = new Padding(4, 8, 4, 8);
            irregularCharacteristicFrequencyTextBox.Name = "irregularCharacteristicFrequencyTextBox";
            irregularCharacteristicFrequencyTextBox.Size = new Size(164, 30);
            irregularCharacteristicFrequencyTextBox.TabIndex = 21;
            irregularCharacteristicFrequencyTextBox.Text = "0.514";
            // 
            // irregularCharacteristicPeriodLabel
            // 
            irregularCharacteristicPeriodLabel.Dock = DockStyle.Fill;
            irregularCharacteristicPeriodLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularCharacteristicPeriodLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularCharacteristicPeriodLabel.Location = new Point(326, 255);
            irregularCharacteristicPeriodLabel.Margin = new Padding(4, 0, 4, 0);
            irregularCharacteristicPeriodLabel.Name = "irregularCharacteristicPeriodLabel";
            irregularCharacteristicPeriodLabel.Size = new Size(142, 51);
            irregularCharacteristicPeriodLabel.TabIndex = 22;
            irregularCharacteristicPeriodLabel.Text = "特征周期";
            irregularCharacteristicPeriodLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularCharacteristicPeriodTextBox
            // 
            irregularCharacteristicPeriodTextBox.BackColor = Color.White;
            irregularCharacteristicPeriodTextBox.BorderStyle = BorderStyle.FixedSingle;
            irregularCharacteristicPeriodTextBox.Dock = DockStyle.Fill;
            irregularCharacteristicPeriodTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularCharacteristicPeriodTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularCharacteristicPeriodTextBox.Location = new Point(476, 263);
            irregularCharacteristicPeriodTextBox.Margin = new Padding(4, 8, 4, 8);
            irregularCharacteristicPeriodTextBox.Name = "irregularCharacteristicPeriodTextBox";
            irregularCharacteristicPeriodTextBox.Size = new Size(202, 30);
            irregularCharacteristicPeriodTextBox.TabIndex = 23;
            irregularCharacteristicPeriodTextBox.Text = "3.00";
            // 
            // irregularPeakFactorLabel
            // 
            irregularPeakFactorLabel.Dock = DockStyle.Fill;
            irregularPeakFactorLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularPeakFactorLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularPeakFactorLabel.Location = new Point(4, 306);
            irregularPeakFactorLabel.Margin = new Padding(4, 0, 4, 0);
            irregularPeakFactorLabel.Name = "irregularPeakFactorLabel";
            irregularPeakFactorLabel.Size = new Size(142, 51);
            irregularPeakFactorLabel.TabIndex = 24;
            irregularPeakFactorLabel.Text = "谱峰因子";
            irregularPeakFactorLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularPeakFactorTextBox
            // 
            irregularPeakFactorTextBox.BackColor = Color.White;
            irregularPeakFactorTextBox.BorderStyle = BorderStyle.FixedSingle;
            irregularPeakFactorTextBox.Dock = DockStyle.Fill;
            irregularPeakFactorTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularPeakFactorTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularPeakFactorTextBox.Location = new Point(154, 314);
            irregularPeakFactorTextBox.Margin = new Padding(4, 8, 4, 8);
            irregularPeakFactorTextBox.Name = "irregularPeakFactorTextBox";
            irregularPeakFactorTextBox.Size = new Size(164, 30);
            irregularPeakFactorTextBox.TabIndex = 25;
            irregularPeakFactorTextBox.Text = "3.3";
            // 
            // irregularRandomSeedLabel
            // 
            irregularRandomSeedLabel.Dock = DockStyle.Fill;
            irregularRandomSeedLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularRandomSeedLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularRandomSeedLabel.Location = new Point(326, 306);
            irregularRandomSeedLabel.Margin = new Padding(4, 0, 4, 0);
            irregularRandomSeedLabel.Name = "irregularRandomSeedLabel";
            irregularRandomSeedLabel.Size = new Size(142, 51);
            irregularRandomSeedLabel.TabIndex = 26;
            irregularRandomSeedLabel.Text = "随机种子";
            irregularRandomSeedLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularRandomSeedTextBox
            // 
            irregularRandomSeedTextBox.BackColor = Color.White;
            irregularRandomSeedTextBox.BorderStyle = BorderStyle.FixedSingle;
            irregularRandomSeedTextBox.Dock = DockStyle.Fill;
            irregularRandomSeedTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularRandomSeedTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularRandomSeedTextBox.Location = new Point(476, 314);
            irregularRandomSeedTextBox.Margin = new Padding(4, 8, 4, 8);
            irregularRandomSeedTextBox.Name = "irregularRandomSeedTextBox";
            irregularRandomSeedTextBox.Size = new Size(202, 30);
            irregularRandomSeedTextBox.TabIndex = 27;
            irregularRandomSeedTextBox.Text = "12345";
            // 
            // irregularMinimumPeriodLabel
            // 
            irregularMinimumPeriodLabel.Dock = DockStyle.Fill;
            irregularMinimumPeriodLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularMinimumPeriodLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularMinimumPeriodLabel.Location = new Point(4, 357);
            irregularMinimumPeriodLabel.Margin = new Padding(4, 0, 4, 0);
            irregularMinimumPeriodLabel.Name = "irregularMinimumPeriodLabel";
            irregularMinimumPeriodLabel.Size = new Size(142, 51);
            irregularMinimumPeriodLabel.TabIndex = 28;
            irregularMinimumPeriodLabel.Text = "最小周期";
            irregularMinimumPeriodLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularMinimumPeriodTextBox
            // 
            irregularMinimumPeriodTextBox.BackColor = Color.White;
            irregularMinimumPeriodTextBox.BorderStyle = BorderStyle.FixedSingle;
            irregularMinimumPeriodTextBox.Dock = DockStyle.Fill;
            irregularMinimumPeriodTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularMinimumPeriodTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularMinimumPeriodTextBox.Location = new Point(154, 365);
            irregularMinimumPeriodTextBox.Margin = new Padding(4, 8, 4, 8);
            irregularMinimumPeriodTextBox.Name = "irregularMinimumPeriodTextBox";
            irregularMinimumPeriodTextBox.Size = new Size(164, 30);
            irregularMinimumPeriodTextBox.TabIndex = 29;
            irregularMinimumPeriodTextBox.Text = "0.5";
            // 
            // irregularMaximumPeriodLabel
            // 
            irregularMaximumPeriodLabel.Dock = DockStyle.Fill;
            irregularMaximumPeriodLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularMaximumPeriodLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularMaximumPeriodLabel.Location = new Point(326, 357);
            irregularMaximumPeriodLabel.Margin = new Padding(4, 0, 4, 0);
            irregularMaximumPeriodLabel.Name = "irregularMaximumPeriodLabel";
            irregularMaximumPeriodLabel.Size = new Size(142, 51);
            irregularMaximumPeriodLabel.TabIndex = 30;
            irregularMaximumPeriodLabel.Text = "最大周期";
            irregularMaximumPeriodLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularMaximumPeriodTextBox
            // 
            irregularMaximumPeriodTextBox.BackColor = Color.White;
            irregularMaximumPeriodTextBox.BorderStyle = BorderStyle.FixedSingle;
            irregularMaximumPeriodTextBox.Dock = DockStyle.Fill;
            irregularMaximumPeriodTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularMaximumPeriodTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularMaximumPeriodTextBox.Location = new Point(476, 365);
            irregularMaximumPeriodTextBox.Margin = new Padding(4, 8, 4, 8);
            irregularMaximumPeriodTextBox.Name = "irregularMaximumPeriodTextBox";
            irregularMaximumPeriodTextBox.Size = new Size(202, 30);
            irregularMaximumPeriodTextBox.TabIndex = 31;
            irregularMaximumPeriodTextBox.Text = "4.0";
            // 
            // irregularMinimumDifferencePeriodLabel
            // 
            irregularMinimumDifferencePeriodLabel.Dock = DockStyle.Fill;
            irregularMinimumDifferencePeriodLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularMinimumDifferencePeriodLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularMinimumDifferencePeriodLabel.Location = new Point(4, 408);
            irregularMinimumDifferencePeriodLabel.Margin = new Padding(4, 0, 4, 0);
            irregularMinimumDifferencePeriodLabel.Name = "irregularMinimumDifferencePeriodLabel";
            irregularMinimumDifferencePeriodLabel.Size = new Size(142, 51);
            irregularMinimumDifferencePeriodLabel.TabIndex = 32;
            irregularMinimumDifferencePeriodLabel.Text = "最小差频";
            irregularMinimumDifferencePeriodLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularMinimumDifferencePeriodTextBox
            // 
            irregularMinimumDifferencePeriodTextBox.BackColor = Color.White;
            irregularMinimumDifferencePeriodTextBox.BorderStyle = BorderStyle.FixedSingle;
            irregularMinimumDifferencePeriodTextBox.Dock = DockStyle.Fill;
            irregularMinimumDifferencePeriodTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularMinimumDifferencePeriodTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularMinimumDifferencePeriodTextBox.Location = new Point(154, 416);
            irregularMinimumDifferencePeriodTextBox.Margin = new Padding(4, 8, 4, 8);
            irregularMinimumDifferencePeriodTextBox.Name = "irregularMinimumDifferencePeriodTextBox";
            irregularMinimumDifferencePeriodTextBox.Size = new Size(164, 30);
            irregularMinimumDifferencePeriodTextBox.TabIndex = 33;
            irregularMinimumDifferencePeriodTextBox.Text = "0.2";
            // 
            // irregularMaximumDifferencePeriodLabel
            // 
            irregularMaximumDifferencePeriodLabel.Dock = DockStyle.Fill;
            irregularMaximumDifferencePeriodLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularMaximumDifferencePeriodLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularMaximumDifferencePeriodLabel.Location = new Point(326, 408);
            irregularMaximumDifferencePeriodLabel.Margin = new Padding(4, 0, 4, 0);
            irregularMaximumDifferencePeriodLabel.Name = "irregularMaximumDifferencePeriodLabel";
            irregularMaximumDifferencePeriodLabel.Size = new Size(142, 51);
            irregularMaximumDifferencePeriodLabel.TabIndex = 34;
            irregularMaximumDifferencePeriodLabel.Text = "最大差频";
            irregularMaximumDifferencePeriodLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularMaximumDifferencePeriodTextBox
            // 
            irregularMaximumDifferencePeriodTextBox.BackColor = Color.White;
            irregularMaximumDifferencePeriodTextBox.BorderStyle = BorderStyle.FixedSingle;
            irregularMaximumDifferencePeriodTextBox.Dock = DockStyle.Fill;
            irregularMaximumDifferencePeriodTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularMaximumDifferencePeriodTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularMaximumDifferencePeriodTextBox.Location = new Point(476, 416);
            irregularMaximumDifferencePeriodTextBox.Margin = new Padding(4, 8, 4, 8);
            irregularMaximumDifferencePeriodTextBox.Name = "irregularMaximumDifferencePeriodTextBox";
            irregularMaximumDifferencePeriodTextBox.Size = new Size(202, 30);
            irregularMaximumDifferencePeriodTextBox.TabIndex = 35;
            irregularMaximumDifferencePeriodTextBox.Text = "10";
            // 
            // irregularNegativeDirectionLabel
            // 
            irregularNegativeDirectionLabel.Dock = DockStyle.Fill;
            irregularNegativeDirectionLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularNegativeDirectionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularNegativeDirectionLabel.Location = new Point(4, 459);
            irregularNegativeDirectionLabel.Margin = new Padding(4, 0, 4, 0);
            irregularNegativeDirectionLabel.Name = "irregularNegativeDirectionLabel";
            irregularNegativeDirectionLabel.Size = new Size(142, 51);
            irregularNegativeDirectionLabel.TabIndex = 36;
            irregularNegativeDirectionLabel.Text = "负向偏角";
            irregularNegativeDirectionLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularNegativeDirectionTextBox
            // 
            irregularNegativeDirectionTextBox.BackColor = Color.White;
            irregularNegativeDirectionTextBox.BorderStyle = BorderStyle.FixedSingle;
            irregularNegativeDirectionTextBox.Dock = DockStyle.Fill;
            irregularNegativeDirectionTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularNegativeDirectionTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularNegativeDirectionTextBox.Location = new Point(154, 467);
            irregularNegativeDirectionTextBox.Margin = new Padding(4, 8, 4, 8);
            irregularNegativeDirectionTextBox.Name = "irregularNegativeDirectionTextBox";
            irregularNegativeDirectionTextBox.Size = new Size(164, 30);
            irregularNegativeDirectionTextBox.TabIndex = 37;
            irregularNegativeDirectionTextBox.Text = "-25";
            // 
            // irregularPositiveDirectionLabel
            // 
            irregularPositiveDirectionLabel.Dock = DockStyle.Fill;
            irregularPositiveDirectionLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularPositiveDirectionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularPositiveDirectionLabel.Location = new Point(326, 459);
            irregularPositiveDirectionLabel.Margin = new Padding(4, 0, 4, 0);
            irregularPositiveDirectionLabel.Name = "irregularPositiveDirectionLabel";
            irregularPositiveDirectionLabel.Size = new Size(142, 51);
            irregularPositiveDirectionLabel.TabIndex = 38;
            irregularPositiveDirectionLabel.Text = "正向偏角";
            irregularPositiveDirectionLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularPositiveDirectionTextBox
            // 
            irregularPositiveDirectionTextBox.BackColor = Color.White;
            irregularPositiveDirectionTextBox.BorderStyle = BorderStyle.FixedSingle;
            irregularPositiveDirectionTextBox.Dock = DockStyle.Fill;
            irregularPositiveDirectionTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularPositiveDirectionTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularPositiveDirectionTextBox.Location = new Point(476, 467);
            irregularPositiveDirectionTextBox.Margin = new Padding(4, 8, 4, 8);
            irregularPositiveDirectionTextBox.Name = "irregularPositiveDirectionTextBox";
            irregularPositiveDirectionTextBox.Size = new Size(202, 30);
            irregularPositiveDirectionTextBox.TabIndex = 39;
            irregularPositiveDirectionTextBox.Text = "25";
            // 
            // irregularOutputLabel
            // 
            irregularOutputLabel.Dock = DockStyle.Fill;
            irregularOutputLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularOutputLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularOutputLabel.Location = new Point(4, 510);
            irregularOutputLabel.Margin = new Padding(4, 0, 4, 0);
            irregularOutputLabel.Name = "irregularOutputLabel";
            irregularOutputLabel.Size = new Size(142, 51);
            irregularOutputLabel.TabIndex = 40;
            irregularOutputLabel.Text = "保存文件";
            irregularOutputLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // irregularOutputTextBox
            // 
            irregularOutputTextBox.BackColor = Color.White;
            irregularOutputTextBox.BorderStyle = BorderStyle.FixedSingle;
            irregularParameterLayout.SetColumnSpan(irregularOutputTextBox, 2);
            irregularOutputTextBox.Dock = DockStyle.Fill;
            irregularOutputTextBox.Font = new Font("Microsoft YaHei UI", 9F);
            irregularOutputTextBox.ForeColor = Color.FromArgb(15, 23, 42);
            irregularOutputTextBox.Location = new Point(154, 518);
            irregularOutputTextBox.Margin = new Padding(4, 8, 4, 8);
            irregularOutputTextBox.Name = "irregularOutputTextBox";
            irregularOutputTextBox.Size = new Size(314, 30);
            irregularOutputTextBox.TabIndex = 41;
            // 
            // irregularBrowseOutputButton
            // 
            irregularBrowseOutputButton.BackColor = Color.FromArgb(248, 250, 252);
            irregularBrowseOutputButton.Cursor = Cursors.Hand;
            irregularBrowseOutputButton.Dock = DockStyle.Fill;
            irregularBrowseOutputButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            irregularBrowseOutputButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
            irregularBrowseOutputButton.FlatStyle = FlatStyle.Flat;
            irregularBrowseOutputButton.ForeColor = Color.FromArgb(15, 23, 42);
            irregularBrowseOutputButton.Location = new Point(476, 516);
            irregularBrowseOutputButton.Margin = new Padding(4, 6, 4, 6);
            irregularBrowseOutputButton.Name = "irregularBrowseOutputButton";
            irregularBrowseOutputButton.Size = new Size(202, 39);
            irregularBrowseOutputButton.TabIndex = 42;
            irregularBrowseOutputButton.Text = "浏览";
            irregularBrowseOutputButton.UseVisualStyleBackColor = false;
            irregularBrowseOutputButton.Click += BrowseIrregularOutputButton_Click;
            // 
            // irregularGenerateButton
            // 
            irregularGenerateButton.BackColor = Color.FromArgb(29, 78, 216);
            irregularParameterLayout.SetColumnSpan(irregularGenerateButton, 2);
            irregularGenerateButton.Cursor = Cursors.Hand;
            irregularGenerateButton.FlatAppearance.BorderSize = 0;
            irregularGenerateButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 64, 175);
            irregularGenerateButton.FlatStyle = FlatStyle.Flat;
            irregularGenerateButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            irregularGenerateButton.ForeColor = Color.White;
            irregularGenerateButton.Location = new Point(4, 565);
            irregularGenerateButton.Margin = new Padding(4, 4, 4, 4);
            irregularGenerateButton.Name = "irregularGenerateButton";
            irregularGenerateButton.Size = new Size(240, 51);
            irregularGenerateButton.TabIndex = 43;
            irregularGenerateButton.Text = "计算并保存";
            irregularGenerateButton.UseVisualStyleBackColor = false;
            irregularGenerateButton.Click += GenerateIrregularButton_Click;
            // 
            // irregularStatusLabel
            // 
            irregularStatusLabel.AutoEllipsis = true;
            irregularParameterLayout.SetColumnSpan(irregularStatusLabel, 2);
            irregularStatusLabel.Dock = DockStyle.Fill;
            irregularStatusLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            irregularStatusLabel.ForeColor = Color.FromArgb(71, 85, 105);
            irregularStatusLabel.Location = new Point(326, 561);
            irregularStatusLabel.Margin = new Padding(4, 0, 4, 0);
            irregularStatusLabel.Name = "irregularStatusLabel";
            irregularStatusLabel.Size = new Size(352, 66);
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
            irregularPreviewGroup.Location = new Point(724, 4);
            irregularPreviewGroup.Margin = new Padding(4, 4, 4, 4);
            irregularPreviewGroup.Name = "irregularPreviewGroup";
            irregularPreviewGroup.Padding = new Padding(15, 27, 15, 15);
            irregularPreviewGroup.Size = new Size(904, 877);
            irregularPreviewGroup.TabIndex = 1;
            irregularPreviewGroup.TabStop = false;
            irregularPreviewGroup.Text = "波形预览";
            // 
            // irregularPreview
            // 
            irregularPreview.BackColor = Color.White;
            irregularPreview.Dock = DockStyle.Fill;
            irregularPreview.ForeColor = Color.FromArgb(29, 78, 216);
            irregularPreview.Location = new Point(15, 50);
            irregularPreview.Margin = new Padding(4, 4, 4, 4);
            irregularPreview.MinimumSize = new Size(450, 270);
            irregularPreview.Name = "irregularPreview";
            irregularPreview.Size = new Size(874, 812);
            irregularPreview.TabIndex = 0;
            irregularPreview.UnitText = "m";
            // 
            // IrregularWavePage
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            AutoScrollMinSize = new Size(946, 476);
            BackColor = Color.FromArgb(241, 245, 249);
            Controls.Add(pagePanel);
            Font = new Font("Microsoft YaHei UI", 9F);
            ForeColor = Color.FromArgb(15, 23, 42);
            Margin = new Padding(4, 4, 4, 4);
            Name = "IrregularWavePage";
            Size = new Size(1656, 909);
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
