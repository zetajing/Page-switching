namespace Page_switching.panel;

partial class WaveformPage
{
    private System.ComponentModel.IContainer components = null!;
    private TableLayoutPanel rootLayout;
    private Label titleLabel;
    private Label subtitleLabel;
    private TabControl waveformTabs;
    private TabPage regularTab;
    private TabPage irregularTab;
    private TableLayoutPanel regularLayout;
    private GroupBox regularParameterGroup;
    private TableLayoutPanel regularParameterLayout;
    private Label regularSegmentLabel;
    private ComboBox regularSegmentComboBox;
    private Label regularTheoryLabel;
    private ComboBox regularTheoryComboBox;
    private Label regularDepthLabel;
    private TextBox regularDepthTextBox;
    private Label regularPeriodLabel;
    private TextBox regularPeriodTextBox;
    private Label regularHeightLabel;
    private TextBox regularHeightTextBox;
    private Label regularDirectionLabel;
    private TextBox regularDirectionTextBox;
    private Label regularTimeStepLabel;
    private TextBox regularTimeStepTextBox;
    private Label regularSampleCountLabel;
    private TextBox regularSampleCountTextBox;
    private Label regularFrequencyLabel;
    private TextBox regularCharacteristicFrequencyTextBox;
    private Label regularCharacteristicPeriodLabel;
    private TextBox regularCharacteristicPeriodTextBox;
    private Label regularOutputLabel;
    private TextBox regularOutputTextBox;
    private Button regularBrowseOutputButton;
    private Button regularGenerateButton;
    private Label regularStatusLabel;
    private GroupBox regularPreviewGroup;
    private WaveformPreviewControl regularPreview;
    private TableLayoutPanel irregularLayout;
    private GroupBox irregularParameterGroup;
    private TableLayoutPanel irregularParameterLayout;
    private Label irregularModeLabel;
    private ComboBox irregularModeComboBox;
    private Label irregularTheoryLabel;
    private ComboBox irregularTheoryComboBox;
    private Label irregularSpectrumLabel;
    private ComboBox irregularSpectrumComboBox;
    private Label irregularSegmentLabel;
    private ComboBox irregularSegmentComboBox;
    private Label irregularDirectionLabel;
    private TextBox irregularDirectionTextBox;
    private Label irregularDepthLabel;
    private TextBox irregularDepthTextBox;
    private Label irregularSignificantPeriodLabel;
    private TextBox irregularSignificantPeriodTextBox;
    private Label irregularSignificantHeightLabel;
    private TextBox irregularSignificantHeightTextBox;
    private Label irregularTimeStepLabel;
    private TextBox irregularTimeStepTextBox;
    private Label irregularSampleCountLabel;
    private TextBox irregularSampleCountTextBox;
    private Label irregularFrequencyLabel;
    private TextBox irregularCharacteristicFrequencyTextBox;
    private Label irregularCharacteristicPeriodLabel;
    private TextBox irregularCharacteristicPeriodTextBox;
    private Label irregularPeakFactorLabel;
    private TextBox irregularPeakFactorTextBox;
    private Label irregularRandomSeedLabel;
    private TextBox irregularRandomSeedTextBox;
    private Label irregularMinimumPeriodLabel;
    private TextBox irregularMinimumPeriodTextBox;
    private Label irregularMaximumPeriodLabel;
    private TextBox irregularMaximumPeriodTextBox;
    private Label irregularMinimumDifferencePeriodLabel;
    private TextBox irregularMinimumDifferencePeriodTextBox;
    private Label irregularMaximumDifferencePeriodLabel;
    private TextBox irregularMaximumDifferencePeriodTextBox;
    private Label irregularNegativeDirectionLabel;
    private TextBox irregularNegativeDirectionTextBox;
    private Label irregularPositiveDirectionLabel;
    private TextBox irregularPositiveDirectionTextBox;
    private Label irregularOutputLabel;
    private TextBox irregularOutputTextBox;
    private Button irregularBrowseOutputButton;
    private Button irregularGenerateButton;
    private Label irregularStatusLabel;
    private GroupBox irregularPreviewGroup;
    private WaveformPreviewControl irregularPreview;

    // 创建设计器中的波形页面控件并绑定事件。
    private void InitializeComponent()
    {
        rootLayout = new TableLayoutPanel();
        titleLabel = new Label();
        subtitleLabel = new Label();
        waveformTabs = new TabControl();
        regularTab = new TabPage();
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
        irregularTab = new TabPage();
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
        rootLayout.SuspendLayout();
        waveformTabs.SuspendLayout();
        regularTab.SuspendLayout();
        regularLayout.SuspendLayout();
        regularParameterGroup.SuspendLayout();
        regularParameterLayout.SuspendLayout();
        regularPreviewGroup.SuspendLayout();
        irregularTab.SuspendLayout();
        irregularLayout.SuspendLayout();
        irregularParameterGroup.SuspendLayout();
        irregularParameterLayout.SuspendLayout();
        irregularPreviewGroup.SuspendLayout();
        SuspendLayout();
        // 
        // rootLayout
        // 
        rootLayout.BackColor = Color.FromArgb(241, 245, 249);
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(titleLabel, 0, 0);
        rootLayout.Controls.Add(subtitleLabel, 0, 1);
        rootLayout.Controls.Add(waveformTabs, 0, 2);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(18);
        rootLayout.RowCount = 3;
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        rootLayout.Size = new Size(905, 681);
        rootLayout.TabIndex = 0;
        // 
        // titleLabel
        // 
        titleLabel.Dock = DockStyle.Fill;
        titleLabel.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
        titleLabel.Location = new Point(21, 18);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(863, 42);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "波形生成";
        titleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // subtitleLabel
        // 
        subtitleLabel.Dock = DockStyle.Fill;
        subtitleLabel.Font = new Font("Microsoft YaHei UI", 9F);
        subtitleLabel.ForeColor = Color.FromArgb(71, 85, 105);
        subtitleLabel.Location = new Point(21, 60);
        subtitleLabel.Name = "subtitleLabel";
        subtitleLabel.Size = new Size(863, 32);
        subtitleLabel.TabIndex = 1;
        subtitleLabel.Text = "规则波和不规则波参数设置、外部 WFast / 内置 WaveMaker 生成与曲线预览";
        subtitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // waveformTabs
        // 
        waveformTabs.Controls.Add(regularTab);
        waveformTabs.Controls.Add(irregularTab);
        waveformTabs.Dock = DockStyle.Fill;
        waveformTabs.Font = new Font("Microsoft YaHei UI", 9F);
        waveformTabs.Location = new Point(21, 95);
        waveformTabs.Name = "waveformTabs";
        waveformTabs.SelectedIndex = 0;
        waveformTabs.Size = new Size(863, 565);
        waveformTabs.TabIndex = 2;
        // 
        // regularTab
        // 
        regularTab.Controls.Add(regularLayout);
        regularTab.Location = new Point(4, 29);
        regularTab.Name = "regularTab";
        regularTab.Padding = new Padding(8);
        regularTab.Size = new Size(855, 532);
        regularTab.TabIndex = 0;
        regularTab.Text = "规则波";
        // 
        // regularLayout
        // 
        regularLayout.ColumnCount = 2;
        regularLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 405F));
        regularLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        regularLayout.Controls.Add(regularParameterGroup, 0, 0);
        regularLayout.Controls.Add(regularPreviewGroup, 1, 0);
        regularLayout.Dock = DockStyle.Fill;
        regularLayout.Location = new Point(8, 8);
        regularLayout.Name = "regularLayout";
        regularLayout.RowCount = 1;
        regularLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        regularLayout.Size = new Size(839, 516);
        regularLayout.TabIndex = 0;
        // 
        // regularParameterGroup
        // 
        regularParameterGroup.Controls.Add(regularParameterLayout);
        regularParameterGroup.Dock = DockStyle.Fill;
        regularParameterGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        regularParameterGroup.Location = new Point(3, 3);
        regularParameterGroup.Name = "regularParameterGroup";
        regularParameterGroup.Padding = new Padding(10, 16, 10, 10);
        regularParameterGroup.Size = new Size(399, 510);
        regularParameterGroup.TabIndex = 0;
        regularParameterGroup.TabStop = false;
        regularParameterGroup.Text = "规则波参数";
        // 
        // regularParameterLayout
        // 
        regularParameterLayout.AutoSize = true;
        regularParameterLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        regularParameterLayout.ColumnCount = 4;
        regularParameterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 84F));
        regularParameterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 106F));
        regularParameterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 84F));
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
        regularParameterLayout.Location = new Point(10, 36);
        regularParameterLayout.Name = "regularParameterLayout";
        regularParameterLayout.RowCount = 7;
        regularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        regularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        regularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        regularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        regularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        regularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        regularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        regularParameterLayout.Size = new Size(379, 280);
        regularParameterLayout.TabIndex = 0;
        // 
        // regularSegmentLabel
        // 
        regularSegmentLabel.Location = new Point(3, 0);
        regularSegmentLabel.Name = "regularSegmentLabel";
        regularSegmentLabel.Size = new Size(78, 23);
        regularSegmentLabel.TabIndex = 0;
        regularSegmentLabel.Text = "造波段";
        // 
        // regularSegmentComboBox
        // 
        regularSegmentComboBox.Location = new Point(87, 3);
        regularSegmentComboBox.Name = "regularSegmentComboBox";
        regularSegmentComboBox.Size = new Size(100, 27);
        regularSegmentComboBox.TabIndex = 1;
        // 
        // regularTheoryLabel
        // 
        regularTheoryLabel.Location = new Point(193, 0);
        regularTheoryLabel.Name = "regularTheoryLabel";
        regularTheoryLabel.Size = new Size(78, 23);
        regularTheoryLabel.TabIndex = 2;
        regularTheoryLabel.Text = "造波理论";
        // 
        // regularTheoryComboBox
        // 
        regularTheoryComboBox.Location = new Point(277, 3);
        regularTheoryComboBox.Name = "regularTheoryComboBox";
        regularTheoryComboBox.Size = new Size(99, 27);
        regularTheoryComboBox.TabIndex = 3;
        // 
        // regularDepthLabel
        // 
        regularDepthLabel.Location = new Point(3, 40);
        regularDepthLabel.Name = "regularDepthLabel";
        regularDepthLabel.Size = new Size(78, 23);
        regularDepthLabel.TabIndex = 4;
        regularDepthLabel.Text = "水深(m)";
        // 
        // regularDepthTextBox
        // 
        regularDepthTextBox.Location = new Point(87, 43);
        regularDepthTextBox.Name = "regularDepthTextBox";
        regularDepthTextBox.Size = new Size(100, 27);
        regularDepthTextBox.TabIndex = 5;
        // 
        // regularPeriodLabel
        // 
        regularPeriodLabel.Location = new Point(193, 40);
        regularPeriodLabel.Name = "regularPeriodLabel";
        regularPeriodLabel.Size = new Size(78, 23);
        regularPeriodLabel.TabIndex = 6;
        regularPeriodLabel.Text = "周期(s)";
        // 
        // regularPeriodTextBox
        // 
        regularPeriodTextBox.Location = new Point(277, 43);
        regularPeriodTextBox.Name = "regularPeriodTextBox";
        regularPeriodTextBox.Size = new Size(99, 27);
        regularPeriodTextBox.TabIndex = 7;
        // 
        // regularHeightLabel
        // 
        regularHeightLabel.Location = new Point(3, 80);
        regularHeightLabel.Name = "regularHeightLabel";
        regularHeightLabel.Size = new Size(78, 23);
        regularHeightLabel.TabIndex = 8;
        regularHeightLabel.Text = "波高(m)";
        // 
        // regularHeightTextBox
        // 
        regularHeightTextBox.Location = new Point(87, 83);
        regularHeightTextBox.Name = "regularHeightTextBox";
        regularHeightTextBox.Size = new Size(100, 27);
        regularHeightTextBox.TabIndex = 9;
        // 
        // regularDirectionLabel
        // 
        regularDirectionLabel.Location = new Point(193, 80);
        regularDirectionLabel.Name = "regularDirectionLabel";
        regularDirectionLabel.Size = new Size(78, 23);
        regularDirectionLabel.TabIndex = 10;
        regularDirectionLabel.Text = "波向(°)";
        // 
        // regularDirectionTextBox
        // 
        regularDirectionTextBox.Location = new Point(277, 83);
        regularDirectionTextBox.Name = "regularDirectionTextBox";
        regularDirectionTextBox.Size = new Size(99, 27);
        regularDirectionTextBox.TabIndex = 11;
        // 
        // regularTimeStepLabel
        // 
        regularTimeStepLabel.Location = new Point(3, 120);
        regularTimeStepLabel.Name = "regularTimeStepLabel";
        regularTimeStepLabel.Size = new Size(78, 23);
        regularTimeStepLabel.TabIndex = 12;
        regularTimeStepLabel.Text = "步长(s)";
        // 
        // regularTimeStepTextBox
        // 
        regularTimeStepTextBox.Location = new Point(87, 123);
        regularTimeStepTextBox.Name = "regularTimeStepTextBox";
        regularTimeStepTextBox.Size = new Size(100, 27);
        regularTimeStepTextBox.TabIndex = 13;
        // 
        // regularSampleCountLabel
        // 
        regularSampleCountLabel.Location = new Point(193, 120);
        regularSampleCountLabel.Name = "regularSampleCountLabel";
        regularSampleCountLabel.Size = new Size(78, 23);
        regularSampleCountLabel.TabIndex = 14;
        regularSampleCountLabel.Text = "时序数";
        // 
        // regularSampleCountTextBox
        // 
        regularSampleCountTextBox.Location = new Point(277, 123);
        regularSampleCountTextBox.Name = "regularSampleCountTextBox";
        regularSampleCountTextBox.Size = new Size(99, 27);
        regularSampleCountTextBox.TabIndex = 15;
        // 
        // regularFrequencyLabel
        // 
        regularFrequencyLabel.Location = new Point(3, 160);
        regularFrequencyLabel.Name = "regularFrequencyLabel";
        regularFrequencyLabel.Size = new Size(78, 23);
        regularFrequencyLabel.TabIndex = 16;
        regularFrequencyLabel.Text = "特征频率";
        // 
        // regularCharacteristicFrequencyTextBox
        // 
        regularCharacteristicFrequencyTextBox.Location = new Point(87, 163);
        regularCharacteristicFrequencyTextBox.Name = "regularCharacteristicFrequencyTextBox";
        regularCharacteristicFrequencyTextBox.Size = new Size(100, 27);
        regularCharacteristicFrequencyTextBox.TabIndex = 17;
        // 
        // regularCharacteristicPeriodLabel
        // 
        regularCharacteristicPeriodLabel.Location = new Point(193, 160);
        regularCharacteristicPeriodLabel.Name = "regularCharacteristicPeriodLabel";
        regularCharacteristicPeriodLabel.Size = new Size(78, 23);
        regularCharacteristicPeriodLabel.TabIndex = 18;
        regularCharacteristicPeriodLabel.Text = "特征周期";
        // 
        // regularCharacteristicPeriodTextBox
        // 
        regularCharacteristicPeriodTextBox.Location = new Point(277, 163);
        regularCharacteristicPeriodTextBox.Name = "regularCharacteristicPeriodTextBox";
        regularCharacteristicPeriodTextBox.Size = new Size(99, 27);
        regularCharacteristicPeriodTextBox.TabIndex = 19;
        // 
        // regularOutputLabel
        // 
        regularOutputLabel.Location = new Point(3, 200);
        regularOutputLabel.Name = "regularOutputLabel";
        regularOutputLabel.Size = new Size(78, 23);
        regularOutputLabel.TabIndex = 20;
        regularOutputLabel.Text = "保存文件";
        // 
        // regularOutputTextBox
        // 
        regularParameterLayout.SetColumnSpan(regularOutputTextBox, 2);
        regularOutputTextBox.Location = new Point(87, 203);
        regularOutputTextBox.Name = "regularOutputTextBox";
        regularOutputTextBox.Size = new Size(100, 27);
        regularOutputTextBox.TabIndex = 21;
        // 
        // regularBrowseOutputButton
        // 
        regularBrowseOutputButton.Location = new Point(277, 203);
        regularBrowseOutputButton.Name = "regularBrowseOutputButton";
        regularBrowseOutputButton.Size = new Size(75, 23);
        regularBrowseOutputButton.TabIndex = 22;
        regularBrowseOutputButton.Text = "浏览";
        regularBrowseOutputButton.Click += BrowseRegularOutputButton_Click;
        // 
        // regularGenerateButton
        // 
        regularParameterLayout.SetColumnSpan(regularGenerateButton, 2);
        regularGenerateButton.Location = new Point(3, 243);
        regularGenerateButton.Name = "regularGenerateButton";
        regularGenerateButton.Size = new Size(75, 23);
        regularGenerateButton.TabIndex = 23;
        regularGenerateButton.Text = "计算并保存";
        regularGenerateButton.Click += GenerateRegularButton_Click;
        // 
        // regularStatusLabel
        // 
        regularParameterLayout.SetColumnSpan(regularStatusLabel, 2);
        regularStatusLabel.Location = new Point(193, 240);
        regularStatusLabel.Name = "regularStatusLabel";
        regularStatusLabel.Size = new Size(100, 23);
        regularStatusLabel.TabIndex = 24;
        regularStatusLabel.Text = "请选择 CSV 保存路径";
        // 
        // regularPreviewGroup
        // 
        regularPreviewGroup.Controls.Add(regularPreview);
        regularPreviewGroup.Dock = DockStyle.Fill;
        regularPreviewGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        regularPreviewGroup.Location = new Point(408, 3);
        regularPreviewGroup.Name = "regularPreviewGroup";
        regularPreviewGroup.Padding = new Padding(10, 18, 10, 10);
        regularPreviewGroup.Size = new Size(428, 510);
        regularPreviewGroup.TabIndex = 1;
        regularPreviewGroup.TabStop = false;
        regularPreviewGroup.Text = "波形预览";
        // 
        // regularPreview
        // 
        regularPreview.BackColor = Color.White;
        regularPreview.Dock = DockStyle.Fill;
        regularPreview.ForeColor = Color.FromArgb(30, 64, 175);
        regularPreview.Location = new Point(10, 38);
        regularPreview.MinimumSize = new Size(360, 260);
        regularPreview.Name = "regularPreview";
        regularPreview.Size = new Size(408, 462);
        regularPreview.TabIndex = 0;
        regularPreview.UnitText = "m";
        // 
        // irregularTab
        // 
        irregularTab.Controls.Add(irregularLayout);
        irregularTab.Location = new Point(4, 29);
        irregularTab.Name = "irregularTab";
        irregularTab.Padding = new Padding(8);
        irregularTab.Size = new Size(192, 67);
        irregularTab.TabIndex = 1;
        irregularTab.Text = "不规则波";
        // 
        // irregularLayout
        // 
        irregularLayout.ColumnCount = 2;
        irregularLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 450F));
        irregularLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        irregularLayout.Controls.Add(irregularParameterGroup, 0, 0);
        irregularLayout.Controls.Add(irregularPreviewGroup, 1, 0);
        irregularLayout.Dock = DockStyle.Fill;
        irregularLayout.Location = new Point(8, 8);
        irregularLayout.Name = "irregularLayout";
        irregularLayout.RowCount = 1;
        irregularLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        irregularLayout.Size = new Size(176, 51);
        irregularLayout.TabIndex = 0;
        // 
        // irregularParameterGroup
        // 
        irregularParameterGroup.Controls.Add(irregularParameterLayout);
        irregularParameterGroup.Dock = DockStyle.Fill;
        irregularParameterGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        irregularParameterGroup.Location = new Point(3, 3);
        irregularParameterGroup.Name = "irregularParameterGroup";
        irregularParameterGroup.Padding = new Padding(10, 16, 10, 10);
        irregularParameterGroup.Size = new Size(444, 45);
        irregularParameterGroup.TabIndex = 0;
        irregularParameterGroup.TabStop = false;
        irregularParameterGroup.Text = "不规则波参数";
        // 
        // irregularParameterLayout
        // 
        irregularParameterLayout.AutoSize = true;
        irregularParameterLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        irregularParameterLayout.ColumnCount = 4;
        irregularParameterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 94F));
        irregularParameterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        irregularParameterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 94F));
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
        irregularParameterLayout.Location = new Point(10, 36);
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
        irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        irregularParameterLayout.Size = new Size(424, 408);
        irregularParameterLayout.TabIndex = 0;
        // 
        // irregularModeLabel
        // 
        irregularModeLabel.Location = new Point(3, 0);
        irregularModeLabel.Name = "irregularModeLabel";
        irregularModeLabel.Size = new Size(88, 23);
        irregularModeLabel.TabIndex = 0;
        irregularModeLabel.Text = "造波模式";
        // 
        // irregularModeComboBox
        // 
        irregularModeComboBox.Location = new Point(97, 3);
        irregularModeComboBox.Name = "irregularModeComboBox";
        irregularModeComboBox.Size = new Size(104, 27);
        irregularModeComboBox.TabIndex = 1;
        // 
        // irregularTheoryLabel
        // 
        irregularTheoryLabel.Location = new Point(207, 0);
        irregularTheoryLabel.Name = "irregularTheoryLabel";
        irregularTheoryLabel.Size = new Size(88, 23);
        irregularTheoryLabel.TabIndex = 2;
        irregularTheoryLabel.Text = "造波理论";
        // 
        // irregularTheoryComboBox
        // 
        irregularTheoryComboBox.Location = new Point(301, 3);
        irregularTheoryComboBox.Name = "irregularTheoryComboBox";
        irregularTheoryComboBox.Size = new Size(120, 27);
        irregularTheoryComboBox.TabIndex = 3;
        // 
        // irregularSpectrumLabel
        // 
        irregularSpectrumLabel.Location = new Point(3, 34);
        irregularSpectrumLabel.Name = "irregularSpectrumLabel";
        irregularSpectrumLabel.Size = new Size(88, 23);
        irregularSpectrumLabel.TabIndex = 4;
        irregularSpectrumLabel.Text = "输入谱";
        // 
        // irregularSpectrumComboBox
        // 
        irregularSpectrumComboBox.Location = new Point(97, 37);
        irregularSpectrumComboBox.Name = "irregularSpectrumComboBox";
        irregularSpectrumComboBox.Size = new Size(104, 27);
        irregularSpectrumComboBox.TabIndex = 5;
        // 
        // irregularSegmentLabel
        // 
        irregularSegmentLabel.Location = new Point(207, 34);
        irregularSegmentLabel.Name = "irregularSegmentLabel";
        irregularSegmentLabel.Size = new Size(88, 23);
        irregularSegmentLabel.TabIndex = 6;
        irregularSegmentLabel.Text = "造波段";
        // 
        // irregularSegmentComboBox
        // 
        irregularSegmentComboBox.Location = new Point(301, 37);
        irregularSegmentComboBox.Name = "irregularSegmentComboBox";
        irregularSegmentComboBox.Size = new Size(120, 27);
        irregularSegmentComboBox.TabIndex = 7;
        // 
        // irregularDirectionLabel
        // 
        irregularDirectionLabel.Location = new Point(3, 68);
        irregularDirectionLabel.Name = "irregularDirectionLabel";
        irregularDirectionLabel.Size = new Size(88, 23);
        irregularDirectionLabel.TabIndex = 8;
        irregularDirectionLabel.Text = "主波向(°)";
        // 
        // irregularDirectionTextBox
        // 
        irregularDirectionTextBox.Location = new Point(97, 71);
        irregularDirectionTextBox.Name = "irregularDirectionTextBox";
        irregularDirectionTextBox.Size = new Size(100, 27);
        irregularDirectionTextBox.TabIndex = 9;
        // 
        // irregularDepthLabel
        // 
        irregularDepthLabel.Location = new Point(207, 68);
        irregularDepthLabel.Name = "irregularDepthLabel";
        irregularDepthLabel.Size = new Size(88, 23);
        irregularDepthLabel.TabIndex = 10;
        irregularDepthLabel.Text = "水深(m)";
        // 
        // irregularDepthTextBox
        // 
        irregularDepthTextBox.Location = new Point(301, 71);
        irregularDepthTextBox.Name = "irregularDepthTextBox";
        irregularDepthTextBox.Size = new Size(100, 27);
        irregularDepthTextBox.TabIndex = 11;
        // 
        // irregularSignificantPeriodLabel
        // 
        irregularSignificantPeriodLabel.Location = new Point(3, 102);
        irregularSignificantPeriodLabel.Name = "irregularSignificantPeriodLabel";
        irregularSignificantPeriodLabel.Size = new Size(88, 23);
        irregularSignificantPeriodLabel.TabIndex = 12;
        irregularSignificantPeriodLabel.Text = "有效周期(s)";
        // 
        // irregularSignificantPeriodTextBox
        // 
        irregularSignificantPeriodTextBox.Location = new Point(97, 105);
        irregularSignificantPeriodTextBox.Name = "irregularSignificantPeriodTextBox";
        irregularSignificantPeriodTextBox.Size = new Size(100, 27);
        irregularSignificantPeriodTextBox.TabIndex = 13;
        // 
        // irregularSignificantHeightLabel
        // 
        irregularSignificantHeightLabel.Location = new Point(207, 102);
        irregularSignificantHeightLabel.Name = "irregularSignificantHeightLabel";
        irregularSignificantHeightLabel.Size = new Size(88, 23);
        irregularSignificantHeightLabel.TabIndex = 14;
        irregularSignificantHeightLabel.Text = "有效波高(m)";
        // 
        // irregularSignificantHeightTextBox
        // 
        irregularSignificantHeightTextBox.Location = new Point(301, 105);
        irregularSignificantHeightTextBox.Name = "irregularSignificantHeightTextBox";
        irregularSignificantHeightTextBox.Size = new Size(100, 27);
        irregularSignificantHeightTextBox.TabIndex = 15;
        // 
        // irregularTimeStepLabel
        // 
        irregularTimeStepLabel.Location = new Point(3, 136);
        irregularTimeStepLabel.Name = "irregularTimeStepLabel";
        irregularTimeStepLabel.Size = new Size(88, 23);
        irregularTimeStepLabel.TabIndex = 16;
        irregularTimeStepLabel.Text = "步长(s)";
        // 
        // irregularTimeStepTextBox
        // 
        irregularTimeStepTextBox.Location = new Point(97, 139);
        irregularTimeStepTextBox.Name = "irregularTimeStepTextBox";
        irregularTimeStepTextBox.Size = new Size(100, 27);
        irregularTimeStepTextBox.TabIndex = 17;
        // 
        // irregularSampleCountLabel
        // 
        irregularSampleCountLabel.Location = new Point(207, 136);
        irregularSampleCountLabel.Name = "irregularSampleCountLabel";
        irregularSampleCountLabel.Size = new Size(88, 23);
        irregularSampleCountLabel.TabIndex = 18;
        irregularSampleCountLabel.Text = "时序数";
        // 
        // irregularSampleCountTextBox
        // 
        irregularSampleCountTextBox.Location = new Point(301, 139);
        irregularSampleCountTextBox.Name = "irregularSampleCountTextBox";
        irregularSampleCountTextBox.Size = new Size(100, 27);
        irregularSampleCountTextBox.TabIndex = 19;
        // 
        // irregularFrequencyLabel
        // 
        irregularFrequencyLabel.Location = new Point(3, 170);
        irregularFrequencyLabel.Name = "irregularFrequencyLabel";
        irregularFrequencyLabel.Size = new Size(88, 23);
        irregularFrequencyLabel.TabIndex = 20;
        irregularFrequencyLabel.Text = "特征频率";
        // 
        // irregularCharacteristicFrequencyTextBox
        // 
        irregularCharacteristicFrequencyTextBox.Location = new Point(97, 173);
        irregularCharacteristicFrequencyTextBox.Name = "irregularCharacteristicFrequencyTextBox";
        irregularCharacteristicFrequencyTextBox.Size = new Size(100, 27);
        irregularCharacteristicFrequencyTextBox.TabIndex = 21;
        // 
        // irregularCharacteristicPeriodLabel
        // 
        irregularCharacteristicPeriodLabel.Location = new Point(207, 170);
        irregularCharacteristicPeriodLabel.Name = "irregularCharacteristicPeriodLabel";
        irregularCharacteristicPeriodLabel.Size = new Size(88, 23);
        irregularCharacteristicPeriodLabel.TabIndex = 22;
        irregularCharacteristicPeriodLabel.Text = "特征周期";
        // 
        // irregularCharacteristicPeriodTextBox
        // 
        irregularCharacteristicPeriodTextBox.Location = new Point(301, 173);
        irregularCharacteristicPeriodTextBox.Name = "irregularCharacteristicPeriodTextBox";
        irregularCharacteristicPeriodTextBox.Size = new Size(100, 27);
        irregularCharacteristicPeriodTextBox.TabIndex = 23;
        // 
        // irregularPeakFactorLabel
        // 
        irregularPeakFactorLabel.Location = new Point(3, 204);
        irregularPeakFactorLabel.Name = "irregularPeakFactorLabel";
        irregularPeakFactorLabel.Size = new Size(88, 23);
        irregularPeakFactorLabel.TabIndex = 24;
        irregularPeakFactorLabel.Text = "谱峰因子";
        // 
        // irregularPeakFactorTextBox
        // 
        irregularPeakFactorTextBox.Location = new Point(97, 207);
        irregularPeakFactorTextBox.Name = "irregularPeakFactorTextBox";
        irregularPeakFactorTextBox.Size = new Size(100, 27);
        irregularPeakFactorTextBox.TabIndex = 25;
        // 
        // irregularRandomSeedLabel
        // 
        irregularRandomSeedLabel.Location = new Point(207, 204);
        irregularRandomSeedLabel.Name = "irregularRandomSeedLabel";
        irregularRandomSeedLabel.Size = new Size(88, 23);
        irregularRandomSeedLabel.TabIndex = 26;
        irregularRandomSeedLabel.Text = "随机种子";
        // 
        // irregularRandomSeedTextBox
        // 
        irregularRandomSeedTextBox.Location = new Point(301, 207);
        irregularRandomSeedTextBox.Name = "irregularRandomSeedTextBox";
        irregularRandomSeedTextBox.Size = new Size(100, 27);
        irregularRandomSeedTextBox.TabIndex = 27;
        // 
        // irregularMinimumPeriodLabel
        // 
        irregularMinimumPeriodLabel.Location = new Point(3, 238);
        irregularMinimumPeriodLabel.Name = "irregularMinimumPeriodLabel";
        irregularMinimumPeriodLabel.Size = new Size(88, 23);
        irregularMinimumPeriodLabel.TabIndex = 28;
        irregularMinimumPeriodLabel.Text = "最小周期";
        // 
        // irregularMinimumPeriodTextBox
        // 
        irregularMinimumPeriodTextBox.Location = new Point(97, 241);
        irregularMinimumPeriodTextBox.Name = "irregularMinimumPeriodTextBox";
        irregularMinimumPeriodTextBox.Size = new Size(100, 27);
        irregularMinimumPeriodTextBox.TabIndex = 29;
        // 
        // irregularMaximumPeriodLabel
        // 
        irregularMaximumPeriodLabel.Location = new Point(207, 238);
        irregularMaximumPeriodLabel.Name = "irregularMaximumPeriodLabel";
        irregularMaximumPeriodLabel.Size = new Size(88, 23);
        irregularMaximumPeriodLabel.TabIndex = 30;
        irregularMaximumPeriodLabel.Text = "最大周期";
        // 
        // irregularMaximumPeriodTextBox
        // 
        irregularMaximumPeriodTextBox.Location = new Point(301, 241);
        irregularMaximumPeriodTextBox.Name = "irregularMaximumPeriodTextBox";
        irregularMaximumPeriodTextBox.Size = new Size(100, 27);
        irregularMaximumPeriodTextBox.TabIndex = 31;
        // 
        // irregularMinimumDifferencePeriodLabel
        // 
        irregularMinimumDifferencePeriodLabel.Location = new Point(3, 272);
        irregularMinimumDifferencePeriodLabel.Name = "irregularMinimumDifferencePeriodLabel";
        irregularMinimumDifferencePeriodLabel.Size = new Size(88, 23);
        irregularMinimumDifferencePeriodLabel.TabIndex = 32;
        irregularMinimumDifferencePeriodLabel.Text = "最小差频";
        // 
        // irregularMinimumDifferencePeriodTextBox
        // 
        irregularMinimumDifferencePeriodTextBox.Location = new Point(97, 275);
        irregularMinimumDifferencePeriodTextBox.Name = "irregularMinimumDifferencePeriodTextBox";
        irregularMinimumDifferencePeriodTextBox.Size = new Size(100, 27);
        irregularMinimumDifferencePeriodTextBox.TabIndex = 33;
        // 
        // irregularMaximumDifferencePeriodLabel
        // 
        irregularMaximumDifferencePeriodLabel.Location = new Point(207, 272);
        irregularMaximumDifferencePeriodLabel.Name = "irregularMaximumDifferencePeriodLabel";
        irregularMaximumDifferencePeriodLabel.Size = new Size(88, 23);
        irregularMaximumDifferencePeriodLabel.TabIndex = 34;
        irregularMaximumDifferencePeriodLabel.Text = "最大差频";
        // 
        // irregularMaximumDifferencePeriodTextBox
        // 
        irregularMaximumDifferencePeriodTextBox.Location = new Point(301, 275);
        irregularMaximumDifferencePeriodTextBox.Name = "irregularMaximumDifferencePeriodTextBox";
        irregularMaximumDifferencePeriodTextBox.Size = new Size(100, 27);
        irregularMaximumDifferencePeriodTextBox.TabIndex = 35;
        // 
        // irregularNegativeDirectionLabel
        // 
        irregularNegativeDirectionLabel.Location = new Point(3, 306);
        irregularNegativeDirectionLabel.Name = "irregularNegativeDirectionLabel";
        irregularNegativeDirectionLabel.Size = new Size(88, 23);
        irregularNegativeDirectionLabel.TabIndex = 36;
        irregularNegativeDirectionLabel.Text = "负向偏角";
        // 
        // irregularNegativeDirectionTextBox
        // 
        irregularNegativeDirectionTextBox.Location = new Point(97, 309);
        irregularNegativeDirectionTextBox.Name = "irregularNegativeDirectionTextBox";
        irregularNegativeDirectionTextBox.Size = new Size(100, 27);
        irregularNegativeDirectionTextBox.TabIndex = 37;
        // 
        // irregularPositiveDirectionLabel
        // 
        irregularPositiveDirectionLabel.Location = new Point(207, 306);
        irregularPositiveDirectionLabel.Name = "irregularPositiveDirectionLabel";
        irregularPositiveDirectionLabel.Size = new Size(88, 23);
        irregularPositiveDirectionLabel.TabIndex = 38;
        irregularPositiveDirectionLabel.Text = "正向偏角";
        // 
        // irregularPositiveDirectionTextBox
        // 
        irregularPositiveDirectionTextBox.Location = new Point(301, 309);
        irregularPositiveDirectionTextBox.Name = "irregularPositiveDirectionTextBox";
        irregularPositiveDirectionTextBox.Size = new Size(100, 27);
        irregularPositiveDirectionTextBox.TabIndex = 39;
        // 
        // irregularOutputLabel
        // 
        irregularOutputLabel.Location = new Point(3, 340);
        irregularOutputLabel.Name = "irregularOutputLabel";
        irregularOutputLabel.Size = new Size(88, 23);
        irregularOutputLabel.TabIndex = 40;
        irregularOutputLabel.Text = "保存文件";
        // 
        // irregularOutputTextBox
        // 
        irregularParameterLayout.SetColumnSpan(irregularOutputTextBox, 2);
        irregularOutputTextBox.Location = new Point(97, 343);
        irregularOutputTextBox.Name = "irregularOutputTextBox";
        irregularOutputTextBox.Size = new Size(100, 27);
        irregularOutputTextBox.TabIndex = 41;
        // 
        // irregularBrowseOutputButton
        // 
        irregularBrowseOutputButton.Location = new Point(301, 343);
        irregularBrowseOutputButton.Name = "irregularBrowseOutputButton";
        irregularBrowseOutputButton.Size = new Size(75, 23);
        irregularBrowseOutputButton.TabIndex = 42;
        irregularBrowseOutputButton.Text = "浏览";
        irregularBrowseOutputButton.Click += BrowseIrregularOutputButton_Click;
        // 
        // irregularGenerateButton
        // 
        irregularParameterLayout.SetColumnSpan(irregularGenerateButton, 2);
        irregularGenerateButton.Location = new Point(3, 377);
        irregularGenerateButton.Name = "irregularGenerateButton";
        irregularGenerateButton.Size = new Size(75, 23);
        irregularGenerateButton.TabIndex = 43;
        irregularGenerateButton.Text = "计算并保存";
        irregularGenerateButton.Click += GenerateIrregularButton_Click;
        // 
        // irregularStatusLabel
        // 
        irregularParameterLayout.SetColumnSpan(irregularStatusLabel, 2);
        irregularStatusLabel.Location = new Point(207, 374);
        irregularStatusLabel.Name = "irregularStatusLabel";
        irregularStatusLabel.Size = new Size(100, 23);
        irregularStatusLabel.TabIndex = 44;
        irregularStatusLabel.Text = "请选择 CSV 保存路径";
        // 
        // irregularPreviewGroup
        // 
        irregularPreviewGroup.Controls.Add(irregularPreview);
        irregularPreviewGroup.Dock = DockStyle.Fill;
        irregularPreviewGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        irregularPreviewGroup.Location = new Point(453, 3);
        irregularPreviewGroup.Name = "irregularPreviewGroup";
        irregularPreviewGroup.Padding = new Padding(10, 18, 10, 10);
        irregularPreviewGroup.Size = new Size(1, 45);
        irregularPreviewGroup.TabIndex = 1;
        irregularPreviewGroup.TabStop = false;
        irregularPreviewGroup.Text = "波形预览";
        // 
        // irregularPreview
        // 
        irregularPreview.BackColor = Color.White;
        irregularPreview.Dock = DockStyle.Fill;
        irregularPreview.ForeColor = Color.FromArgb(30, 64, 175);
        irregularPreview.Location = new Point(10, 38);
        irregularPreview.MinimumSize = new Size(360, 260);
        irregularPreview.Name = "irregularPreview";
        irregularPreview.Size = new Size(360, 260);
        irregularPreview.TabIndex = 0;
        irregularPreview.UnitText = "m";
        // 
        // WaveformPage
        // 
        AutoScaleDimensions = new SizeF(9F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(rootLayout);
        Name = "WaveformPage";
        Size = new Size(905, 681);
        rootLayout.ResumeLayout(false);
        waveformTabs.ResumeLayout(false);
        regularTab.ResumeLayout(false);
        regularLayout.ResumeLayout(false);
        regularParameterGroup.ResumeLayout(false);
        regularParameterGroup.PerformLayout();
        regularParameterLayout.ResumeLayout(false);
        regularParameterLayout.PerformLayout();
        regularPreviewGroup.ResumeLayout(false);
        irregularTab.ResumeLayout(false);
        irregularLayout.ResumeLayout(false);
        irregularParameterGroup.ResumeLayout(false);
        irregularParameterGroup.PerformLayout();
        irregularParameterLayout.ResumeLayout(false);
        irregularParameterLayout.PerformLayout();
        irregularPreviewGroup.ResumeLayout(false);
        ResumeLayout(false);
    }

}
