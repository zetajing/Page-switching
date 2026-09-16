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
        components = new System.ComponentModel.Container();
        rootLayout = new TableLayoutPanel();
        titleLabel = new Label();
        subtitleLabel = new Label();
        waveformTabs = new TabControl();
        regularTab = new TabPage();
        irregularTab = new TabPage();
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
        rootLayout.Padding = new Padding(18);
        rootLayout.RowCount = 3;
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        // 
        // titleLabel
        // 
        titleLabel.Dock = DockStyle.Fill;
        titleLabel.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
        titleLabel.Text = "波形生成";
        titleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // subtitleLabel
        // 
        subtitleLabel.Dock = DockStyle.Fill;
        subtitleLabel.Font = new Font("Microsoft YaHei UI", 9F);
        subtitleLabel.ForeColor = Color.FromArgb(71, 85, 105);
        subtitleLabel.Text = "规则波和不规则波参数设置、WFast 生成与曲线预览";
        subtitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // waveformTabs
        // 
        waveformTabs.Controls.Add(regularTab);
        waveformTabs.Controls.Add(irregularTab);
        waveformTabs.Dock = DockStyle.Fill;
        waveformTabs.Font = new Font("Microsoft YaHei UI", 9F);
        waveformTabs.SelectedIndex = 0;
        // 
        // regularTab
        // 
        regularTab.Controls.Add(regularLayout);
        regularTab.Padding = new Padding(8);
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
        regularLayout.RowCount = 1;
        regularLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        // 
        // regularParameterGroup
        // 
        regularParameterGroup.Controls.Add(regularParameterLayout);
        regularParameterGroup.Dock = DockStyle.Fill;
        regularParameterGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        regularParameterGroup.Padding = new Padding(10, 16, 10, 10);
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
        regularParameterLayout.Dock = DockStyle.Top;
        regularParameterLayout.RowCount = 7;
        for (var i = 0; i < 7; i++)
        {
            regularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        }
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
        regularParameterLayout.SetColumnSpan(regularOutputTextBox, 2);
        regularParameterLayout.Controls.Add(regularBrowseOutputButton, 3, 5);
        regularParameterLayout.Controls.Add(regularGenerateButton, 0, 6);
        regularParameterLayout.SetColumnSpan(regularGenerateButton, 2);
        regularParameterLayout.Controls.Add(regularStatusLabel, 2, 6);
        regularParameterLayout.SetColumnSpan(regularStatusLabel, 2);
        // 
        // regular controls
        // 
        regularSegmentLabel.Text = "造波段";
        regularTheoryLabel.Text = "造波理论";
        regularDepthLabel.Text = "水深(m)";
        regularPeriodLabel.Text = "周期(s)";
        regularHeightLabel.Text = "波高(m)";
        regularDirectionLabel.Text = "波向(°)";
        regularTimeStepLabel.Text = "步长(s)";
        regularSampleCountLabel.Text = "时序数";
        regularFrequencyLabel.Text = "特征频率";
        regularCharacteristicPeriodLabel.Text = "特征周期";
        regularOutputLabel.Text = "保存文件";
        regularGenerateButton.Text = "计算并保存";
        regularBrowseOutputButton.Text = "浏览";
        regularStatusLabel.Text = "请选择 CSV 保存路径";
        ConfigureParameterLabel(regularSegmentLabel);
        ConfigureParameterLabel(regularTheoryLabel);
        ConfigureParameterLabel(regularDepthLabel);
        ConfigureParameterLabel(regularPeriodLabel);
        ConfigureParameterLabel(regularHeightLabel);
        ConfigureParameterLabel(regularDirectionLabel);
        ConfigureParameterLabel(regularTimeStepLabel);
        ConfigureParameterLabel(regularSampleCountLabel);
        ConfigureParameterLabel(regularFrequencyLabel);
        ConfigureParameterLabel(regularCharacteristicPeriodLabel);
        ConfigureParameterLabel(regularOutputLabel);
        ConfigureInput(regularSegmentComboBox);
        ConfigureInput(regularTheoryComboBox);
        ConfigureInput(regularDepthTextBox);
        ConfigureInput(regularPeriodTextBox);
        ConfigureInput(regularHeightTextBox);
        ConfigureInput(regularDirectionTextBox);
        ConfigureInput(regularTimeStepTextBox);
        ConfigureInput(regularSampleCountTextBox);
        ConfigureInput(regularCharacteristicFrequencyTextBox);
        ConfigureInput(regularCharacteristicPeriodTextBox);
        ConfigureInput(regularOutputTextBox);
        ConfigureStatus(regularStatusLabel);
        ConfigureActionButton(regularBrowseOutputButton, false);
        ConfigureActionButton(regularGenerateButton, true);
        regularBrowseOutputButton.Click += BrowseRegularOutputButton_Click;
        regularGenerateButton.Click += GenerateRegularButton_Click;
        // 
        // regularPreviewGroup
        // 
        regularPreviewGroup.Controls.Add(regularPreview);
        regularPreviewGroup.Dock = DockStyle.Fill;
        regularPreviewGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        regularPreviewGroup.Padding = new Padding(10, 18, 10, 10);
        regularPreviewGroup.Text = "波形预览";
        regularPreview.Dock = DockStyle.Fill;
        regularPreview.UnitText = "m";
        // 
        // irregularTab
        // 
        irregularTab.Controls.Add(irregularLayout);
        irregularTab.Padding = new Padding(8);
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
        irregularLayout.RowCount = 1;
        irregularLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        // 
        // irregularParameterGroup
        // 
        irregularParameterGroup.Controls.Add(irregularParameterLayout);
        irregularParameterGroup.Dock = DockStyle.Fill;
        irregularParameterGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        irregularParameterGroup.Padding = new Padding(10, 16, 10, 10);
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
        irregularParameterLayout.Dock = DockStyle.Top;
        irregularParameterLayout.RowCount = 12;
        for (var i = 0; i < 12; i++)
        {
            irregularParameterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        }
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
        irregularParameterLayout.SetColumnSpan(irregularOutputTextBox, 2);
        irregularParameterLayout.Controls.Add(irregularBrowseOutputButton, 3, 10);
        irregularParameterLayout.Controls.Add(irregularGenerateButton, 0, 11);
        irregularParameterLayout.SetColumnSpan(irregularGenerateButton, 2);
        irregularParameterLayout.Controls.Add(irregularStatusLabel, 2, 11);
        irregularParameterLayout.SetColumnSpan(irregularStatusLabel, 2);
        // 
        // irregular controls
        // 
        irregularModeLabel.Text = "造波模式";
        irregularTheoryLabel.Text = "造波理论";
        irregularSpectrumLabel.Text = "输入谱";
        irregularSegmentLabel.Text = "造波段";
        irregularDirectionLabel.Text = "主波向(°)";
        irregularDepthLabel.Text = "水深(m)";
        irregularSignificantPeriodLabel.Text = "有效周期(s)";
        irregularSignificantHeightLabel.Text = "有效波高(m)";
        irregularTimeStepLabel.Text = "步长(s)";
        irregularSampleCountLabel.Text = "时序数";
        irregularFrequencyLabel.Text = "特征频率";
        irregularCharacteristicPeriodLabel.Text = "特征周期";
        irregularPeakFactorLabel.Text = "谱峰因子";
        irregularRandomSeedLabel.Text = "随机种子";
        irregularMinimumPeriodLabel.Text = "最小周期";
        irregularMaximumPeriodLabel.Text = "最大周期";
        irregularMinimumDifferencePeriodLabel.Text = "最小差频";
        irregularMaximumDifferencePeriodLabel.Text = "最大差频";
        irregularNegativeDirectionLabel.Text = "负向偏角";
        irregularPositiveDirectionLabel.Text = "正向偏角";
        irregularOutputLabel.Text = "保存文件";
        irregularGenerateButton.Text = "计算并保存";
        irregularBrowseOutputButton.Text = "浏览";
        irregularStatusLabel.Text = "请选择 CSV 保存路径";
        ConfigureParameterLabel(irregularModeLabel);
        ConfigureParameterLabel(irregularTheoryLabel);
        ConfigureParameterLabel(irregularSpectrumLabel);
        ConfigureParameterLabel(irregularSegmentLabel);
        ConfigureParameterLabel(irregularDirectionLabel);
        ConfigureParameterLabel(irregularDepthLabel);
        ConfigureParameterLabel(irregularSignificantPeriodLabel);
        ConfigureParameterLabel(irregularSignificantHeightLabel);
        ConfigureParameterLabel(irregularTimeStepLabel);
        ConfigureParameterLabel(irregularSampleCountLabel);
        ConfigureParameterLabel(irregularFrequencyLabel);
        ConfigureParameterLabel(irregularCharacteristicPeriodLabel);
        ConfigureParameterLabel(irregularPeakFactorLabel);
        ConfigureParameterLabel(irregularRandomSeedLabel);
        ConfigureParameterLabel(irregularMinimumPeriodLabel);
        ConfigureParameterLabel(irregularMaximumPeriodLabel);
        ConfigureParameterLabel(irregularMinimumDifferencePeriodLabel);
        ConfigureParameterLabel(irregularMaximumDifferencePeriodLabel);
        ConfigureParameterLabel(irregularNegativeDirectionLabel);
        ConfigureParameterLabel(irregularPositiveDirectionLabel);
        ConfigureParameterLabel(irregularOutputLabel);
        ConfigureInput(irregularModeComboBox);
        ConfigureInput(irregularTheoryComboBox);
        ConfigureInput(irregularSpectrumComboBox);
        ConfigureInput(irregularSegmentComboBox);
        ConfigureInput(irregularDirectionTextBox);
        ConfigureInput(irregularDepthTextBox);
        ConfigureInput(irregularSignificantPeriodTextBox);
        ConfigureInput(irregularSignificantHeightTextBox);
        ConfigureInput(irregularTimeStepTextBox);
        ConfigureInput(irregularSampleCountTextBox);
        ConfigureInput(irregularCharacteristicFrequencyTextBox);
        ConfigureInput(irregularCharacteristicPeriodTextBox);
        ConfigureInput(irregularPeakFactorTextBox);
        ConfigureInput(irregularRandomSeedTextBox);
        ConfigureInput(irregularMinimumPeriodTextBox);
        ConfigureInput(irregularMaximumPeriodTextBox);
        ConfigureInput(irregularMinimumDifferencePeriodTextBox);
        ConfigureInput(irregularMaximumDifferencePeriodTextBox);
        ConfigureInput(irregularNegativeDirectionTextBox);
        ConfigureInput(irregularPositiveDirectionTextBox);
        ConfigureInput(irregularOutputTextBox);
        ConfigureStatus(irregularStatusLabel);
        ConfigureActionButton(irregularBrowseOutputButton, false);
        ConfigureActionButton(irregularGenerateButton, true);
        irregularBrowseOutputButton.Click += BrowseIrregularOutputButton_Click;
        irregularGenerateButton.Click += GenerateIrregularButton_Click;
        // 
        // irregularPreviewGroup
        // 
        irregularPreviewGroup.Controls.Add(irregularPreview);
        irregularPreviewGroup.Dock = DockStyle.Fill;
        irregularPreviewGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        irregularPreviewGroup.Padding = new Padding(10, 18, 10, 10);
        irregularPreviewGroup.Text = "波形预览";
        irregularPreview.Dock = DockStyle.Fill;
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
        regularParameterLayout.ResumeLayout(false);
        regularParameterLayout.PerformLayout();
        regularPreviewGroup.ResumeLayout(false);
        irregularTab.ResumeLayout(false);
        irregularLayout.ResumeLayout(false);
        irregularParameterGroup.ResumeLayout(false);
        irregularParameterLayout.ResumeLayout(false);
        irregularParameterLayout.PerformLayout();
        irregularPreviewGroup.ResumeLayout(false);
        ResumeLayout(false);
    }

    // 统一设置参数标签的字体、颜色和对齐方式。
    private static void ConfigureParameterLabel(Label label)
    {
        label.Dock = DockStyle.Fill;
        label.Font = new Font("Microsoft YaHei UI", 8.5F);
        label.ForeColor = Color.FromArgb(71, 85, 105);
        label.TextAlign = ContentAlignment.MiddleLeft;
        label.AutoEllipsis = true;
    }

    // 统一设置输入控件的尺寸、字体和边距。
    private static void ConfigureInput(Control control)
    {
        control.Dock = DockStyle.Fill;
        control.Margin = new Padding(3, 5, 3, 5);
        control.Font = new Font("Microsoft YaHei UI", 9F);
    }

    // 统一设置状态标签的显示样式。
    private static void ConfigureStatus(Label label)
    {
        label.Dock = DockStyle.Fill;
        label.Font = new Font("Microsoft YaHei UI", 8.5F);
        label.ForeColor = Color.FromArgb(71, 85, 105);
        label.TextAlign = ContentAlignment.MiddleLeft;
        label.AutoEllipsis = true;
    }

    // 根据主次操作统一设置按钮样式。
    private static void ConfigureActionButton(Button button, bool primary)
    {
        button.Dock = primary ? DockStyle.None : DockStyle.Fill;
        button.Anchor = primary ? AnchorStyles.Left | AnchorStyles.Top : AnchorStyles.Left | AnchorStyles.Right;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.Margin = primary ? new Padding(3, 6, 3, 6) : new Padding(3, 4, 3, 4);
        if (primary)
        {
            button.Size = new Size(174, 46);
        }
        button.Font = new Font("Microsoft YaHei UI", 9F, primary ? FontStyle.Bold : FontStyle.Regular);
        button.BackColor = primary ? Color.FromArgb(37, 99, 235) : Color.FromArgb(226, 232, 240);
        button.ForeColor = primary ? Color.White : Color.FromArgb(15, 23, 42);
        button.UseVisualStyleBackColor = false;
    }
}
