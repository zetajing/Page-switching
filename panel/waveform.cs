using System.Globalization;

namespace Page_switching.panel;

public partial class WaveformPage : UserControl
{
    private readonly WaveformGeneratorService _generatorService;
    private readonly CancellationTokenSource _lifetimeCancellation = new();
    private bool _generationInProgress;
    private bool _disposed;

    // 初始化波形页面、生成服务和下拉选项。
    public WaveformPage()
    {
        InitializeComponent();
        ApplyStyles();
        _generatorService = new WaveformGeneratorService(WaveformGeneratorOptions.FromConfiguration());
        InitializeWaveformChoices();
    }

    // 在设计器初始化完成后统一设置标签、输入框和按钮样式。
    private void ApplyStyles()
    {
        foreach (var label in new[]
        {
            regularSegmentLabel, regularTheoryLabel, regularDepthLabel, regularPeriodLabel,
            regularHeightLabel, regularDirectionLabel, regularTimeStepLabel, regularSampleCountLabel,
            regularFrequencyLabel, regularCharacteristicPeriodLabel, regularOutputLabel,
            irregularModeLabel, irregularTheoryLabel, irregularSpectrumLabel, irregularSegmentLabel,
            irregularDirectionLabel, irregularDepthLabel, irregularSignificantPeriodLabel,
            irregularSignificantHeightLabel, irregularTimeStepLabel, irregularSampleCountLabel,
            irregularFrequencyLabel, irregularCharacteristicPeriodLabel, irregularPeakFactorLabel,
            irregularRandomSeedLabel, irregularMinimumPeriodLabel, irregularMaximumPeriodLabel,
            irregularMinimumDifferencePeriodLabel, irregularMaximumDifferencePeriodLabel,
            irregularNegativeDirectionLabel, irregularPositiveDirectionLabel, irregularOutputLabel
        })
        {
            ConfigureParameterLabel(label);
        }

        foreach (var input in new Control[]
        {
            regularSegmentComboBox, regularTheoryComboBox, regularDepthTextBox, regularPeriodTextBox,
            regularHeightTextBox, regularDirectionTextBox, regularTimeStepTextBox,
            regularSampleCountTextBox, regularCharacteristicFrequencyTextBox,
            regularCharacteristicPeriodTextBox, regularOutputTextBox,
            irregularModeComboBox, irregularTheoryComboBox, irregularSpectrumComboBox,
            irregularSegmentComboBox, irregularDirectionTextBox, irregularDepthTextBox,
            irregularSignificantPeriodTextBox, irregularSignificantHeightTextBox,
            irregularTimeStepTextBox, irregularSampleCountTextBox,
            irregularCharacteristicFrequencyTextBox, irregularCharacteristicPeriodTextBox,
            irregularPeakFactorTextBox, irregularRandomSeedTextBox, irregularMinimumPeriodTextBox,
            irregularMaximumPeriodTextBox, irregularMinimumDifferencePeriodTextBox,
            irregularMaximumDifferencePeriodTextBox, irregularNegativeDirectionTextBox,
            irregularPositiveDirectionTextBox, irregularOutputTextBox
        })
        {
            ConfigureInput(input);
        }

        ConfigureStatus(regularStatusLabel);
        ConfigureStatus(irregularStatusLabel);
        ConfigureActionButton(regularBrowseOutputButton, false);
        ConfigureActionButton(regularGenerateButton, true);
        ConfigureActionButton(irregularBrowseOutputButton, false);
        ConfigureActionButton(irregularGenerateButton, true);
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

    // 填充规则波和不规则波所需的方向、频谱等选项。
    private void InitializeWaveformChoices()
    {
        regularSegmentComboBox.Items.AddRange(["X轴+Y轴", "X轴", "Y轴"]);
        regularTheoryComboBox.Items.AddRange(["线性理论", "椭圆余弦波", "孤立波"]);
        irregularModeComboBox.Items.AddRange(["单向不规则波", "多向不规则波"]);
        irregularSpectrumComboBox.Items.AddRange([
            "JONSWAP", "Scott", "ITTC", "B谱", "Wallops", "P-M", "规范谱", "Darbyshire"]);
        irregularTheoryComboBox.Items.AddRange(["线性理论", "非线性理论"]);
        irregularSegmentComboBox.Items.AddRange(["X轴+Y轴", "X轴", "Y轴"]);

        regularSegmentComboBox.SelectedIndex = 0;
        regularTheoryComboBox.SelectedIndex = 0;
        irregularModeComboBox.SelectedIndex = 0;
        irregularSpectrumComboBox.SelectedIndex = 0;
        irregularTheoryComboBox.SelectedIndex = 0;
        irregularSegmentComboBox.SelectedIndex = 0;

        regularDepthTextBox.Text = "0.5";
        regularPeriodTextBox.Text = "1.5";
        regularHeightTextBox.Text = "0.1";
        regularDirectionTextBox.Text = "90";
        regularTimeStepTextBox.Text = "0.02";
        regularSampleCountTextBox.Text = "4096";
        regularCharacteristicFrequencyTextBox.Text = "0.514";
        regularCharacteristicPeriodTextBox.Text = "4.00";

        irregularDirectionTextBox.Text = "90";
        irregularDepthTextBox.Text = "0.5";
        irregularSignificantPeriodTextBox.Text = "1.5";
        irregularSignificantHeightTextBox.Text = "0.1";
        irregularTimeStepTextBox.Text = "0.02";
        irregularSampleCountTextBox.Text = "8192";
        irregularCharacteristicFrequencyTextBox.Text = "0.514";
        irregularCharacteristicPeriodTextBox.Text = "3.00";
        irregularPeakFactorTextBox.Text = "3.3";
        irregularRandomSeedTextBox.Text = "12345";
        irregularMinimumPeriodTextBox.Text = "0.5";
        irregularMaximumPeriodTextBox.Text = "4.0";
        irregularMinimumDifferencePeriodTextBox.Text = "0.2";
        irregularMaximumDifferencePeriodTextBox.Text = "10";
        irregularNegativeDirectionTextBox.Text = "-25";
        irregularPositiveDirectionTextBox.Text = "25";
    }

    // 选择规则波输出文件路径。
    private void BrowseRegularOutputButton_Click(object? sender, EventArgs e) =>
        SelectOutputPath(regularOutputTextBox);

    // 选择不规则波输出文件路径。
    private void BrowseIrregularOutputButton_Click(object? sender, EventArgs e) =>
        SelectOutputPath(irregularOutputTextBox);

    // 打开保存对话框，并把选择的路径写入指定文本框。
    private static void SelectOutputPath(TextBox target)
    {
        using var dialog = new SaveFileDialog
        {
            Filter = "造波信号文件 (*.csv)|*.csv|所有文件 (*.*)|*.*",
            DefaultExt = "csv",
            AddExtension = true,
            OverwritePrompt = true,
            Title = "选择造波信号保存路径"
        };

        if (!string.IsNullOrWhiteSpace(target.Text))
        {
            dialog.FileName = Path.GetFileName(target.Text.Trim());
            dialog.InitialDirectory = Path.GetDirectoryName(Path.GetFullPath(target.Text.Trim()));
        }

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            target.Text = dialog.FileName;
        }
    }

    // 校验规则波参数、调用生成程序并显示预览。
    private async void GenerateRegularButton_Click(object? sender, EventArgs e)
    {
        if (_generationInProgress)
        {
            return;
        }

        try
        {
            var outputPath = RequireOutputPath(regularOutputTextBox);
            var parameters = new RegularWaveParameters(
                ParseDouble(regularDepthTextBox, "水深", 0),
                ParseDouble(regularPeriodTextBox, "周期", 0),
                ParseDouble(regularHeightTextBox, "波高", 0),
                ParseDouble(regularDirectionTextBox, "波向角", -360, 360),
                ParseDouble(regularTimeStepTextBox, "时间步长", 0),
                ParseInt(regularSampleCountTextBox, "时序总数", 2, 1_000_000),
                ParseDouble(regularCharacteristicFrequencyTextBox, "特征频率", 0),
                ParseDouble(regularCharacteristicPeriodTextBox, "特征周期", 0),
                regularTheoryComboBox.SelectedIndex + 1,
                GetSideCode(regularSegmentComboBox));

            SetGenerationState(true, regularGenerateButton, regularStatusLabel);
            regularStatusLabel.Text = $"正在使用 {_generatorService.GenerationModeText} 生成规则波……";
            var result = await _generatorService.GenerateRegularAsync(
                parameters,
                outputPath,
                _lifetimeCancellation.Token);
            regularPreview.SetSamples(result.Samples, parameters.TimeStep);
            regularStatusLabel.Text =
                $"生成成功：{result.Samples.Count:n0} 点，耗时 {result.Elapsed.TotalSeconds:0.##} 秒";
        }
        catch (OperationCanceledException)
        {
            regularStatusLabel.ForeColor = Color.FromArgb(180, 83, 9);
            regularStatusLabel.Text = "规则波生成已取消。";
        }
        catch (Exception ex)
        {
            regularStatusLabel.ForeColor = Color.FromArgb(220, 38, 38);
            regularStatusLabel.Text = "规则波生成失败：" + ex.Message;
        }
        finally
        {
            SetGenerationState(false, regularGenerateButton, regularStatusLabel);
        }
    }

    // 校验不规则波参数、调用生成程序并显示预览。
    private async void GenerateIrregularButton_Click(object? sender, EventArgs e)
    {
        if (_generationInProgress)
        {
            return;
        }

        try
        {
            var outputPath = RequireOutputPath(irregularOutputTextBox);
            var parameters = new IrregularWaveParameters(
                ParseDouble(irregularDepthTextBox, "水深", 0),
                ParseDouble(irregularSignificantPeriodTextBox, "有效周期", 0),
                ParseDouble(irregularSignificantHeightTextBox, "有效波高", 0),
                ParseDouble(irregularDirectionTextBox, "主波向", -360, 360),
                ParseDouble(irregularTimeStepTextBox, "时间步长", 0),
                ParseInt(irregularSampleCountTextBox, "时序总数", 2, 1_000_000),
                ParseDouble(irregularCharacteristicFrequencyTextBox, "特征频率", 0),
                ParseDouble(irregularCharacteristicPeriodTextBox, "特征周期", 0),
                ParseDouble(irregularPeakFactorTextBox, "谱峰因子", 0),
                ParseInt(irregularRandomSeedTextBox, "随机种子", int.MinValue, int.MaxValue),
                ParseDouble(irregularMinimumPeriodTextBox, "最小周期", 0),
                ParseDouble(irregularMaximumPeriodTextBox, "最大周期", 0),
                ParseDouble(irregularMinimumDifferencePeriodTextBox, "最小差频周期", 0),
                ParseDouble(irregularMaximumDifferencePeriodTextBox, "最大差频周期", 0),
                ParseDouble(irregularNegativeDirectionTextBox, "负向偏角", -360, 0),
                ParseDouble(irregularPositiveDirectionTextBox, "正向偏角", 0, 360),
                irregularModeComboBox.SelectedIndex == 0 ? 2 : 3,
                GetSpectrumCode(irregularSpectrumComboBox),
                irregularTheoryComboBox.SelectedIndex + 1,
                GetSideCode(irregularSegmentComboBox));

            if (parameters.MinimumPeriod > parameters.MaximumPeriod)
            {
                throw new InvalidOperationException("最小周期不能大于最大周期。");
            }

            if (parameters.MinimumDifferencePeriod > parameters.MaximumDifferencePeriod)
            {
                throw new InvalidOperationException("最小差频周期不能大于最大差频周期。");
            }

            SetGenerationState(true, irregularGenerateButton, irregularStatusLabel);
            irregularStatusLabel.Text = $"正在使用 {_generatorService.GenerationModeText} 生成不规则波……";
            var result = await _generatorService.GenerateIrregularAsync(
                parameters,
                outputPath,
                _lifetimeCancellation.Token);
            irregularPreview.SetSamples(result.Samples, parameters.TimeStep);
            irregularStatusLabel.Text =
                $"生成成功：{result.Samples.Count:n0} 点，耗时 {result.Elapsed.TotalSeconds:0.##} 秒";
        }
        catch (OperationCanceledException)
        {
            irregularStatusLabel.ForeColor = Color.FromArgb(180, 83, 9);
            irregularStatusLabel.Text = "不规则波生成已取消。";
        }
        catch (Exception ex)
        {
            irregularStatusLabel.ForeColor = Color.FromArgb(220, 38, 38);
            irregularStatusLabel.Text = "不规则波生成失败：" + ex.Message;
        }
        finally
        {
            SetGenerationState(false, irregularGenerateButton, irregularStatusLabel);
        }
    }

    // 生成期间禁用操作按钮并更新状态提示。
    private void SetGenerationState(bool generating, Button button, Label statusLabel)
    {
        _generationInProgress = generating;
        button.Enabled = !generating;
        regularGenerateButton.Enabled = !generating;
        irregularGenerateButton.Enabled = !generating;
        regularBrowseOutputButton.Enabled = !generating;
        irregularBrowseOutputButton.Enabled = !generating;
        if (generating)
        {
            statusLabel.ForeColor = Color.FromArgb(37, 99, 235);
        }
        else if (!statusLabel.Text.Contains("失败", StringComparison.Ordinal))
        {
            statusLabel.ForeColor = Color.FromArgb(5, 150, 105);
        }
    }

    // 获取必填输出路径，空值时提示用户。
    private static string RequireOutputPath(TextBox textBox)
    {
        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            throw new InvalidOperationException("请先选择造波信号保存路径。");
        }

        return Path.GetFullPath(textBox.Text.Trim());
    }

    // 读取并校验一个浮点参数的范围。
    private static double ParseDouble(TextBox textBox, string caption, double minimum, double? maximum = null)
    {
        if (!double.TryParse(textBox.Text.Trim(), NumberStyles.Float,
                CultureInfo.InvariantCulture, out var value) || !double.IsFinite(value))
        {
            throw new InvalidOperationException($"{caption}必须是有效数字。");
        }

        if (value < minimum || maximum.HasValue && value > maximum.Value)
        {
            var range = maximum.HasValue ? $"，范围为 {minimum} 到 {maximum.Value}" : $"，必须大于等于 {minimum}";
            throw new InvalidOperationException($"{caption}数值无效{range}。");
        }

        return value;
    }

    // 读取并校验一个整数参数的范围。
    private static int ParseInt(TextBox textBox, string caption, int minimum, int maximum)
    {
        if (!int.TryParse(textBox.Text.Trim(), NumberStyles.Integer,
                CultureInfo.InvariantCulture, out var value) || value < minimum || value > maximum)
        {
            throw new InvalidOperationException($"{caption}必须是 {minimum} 到 {maximum} 之间的整数。");
        }

        return value;
    }

    // 将界面中的造波方向转换为外部程序需要的数字代码。
    private static int GetSideCode(ComboBox comboBox) => comboBox.SelectedIndex switch
    {
        1 => 2,
        2 => 3,
        _ => 1
    };

    // 将频谱名称转换为外部程序需要的数字代码。
    private static int GetSpectrumCode(ComboBox comboBox) => comboBox.SelectedItem?.ToString() switch
    {
        "Scott" => 2,
        "ITTC" => 9,
        "B谱" => 4,
        "Wallops" => 8,
        "P-M" => 6,
        "规范谱" => 3,
        "Darbyshire" => 7,
        _ => 1
    };

    // 页面释放时取消并释放波形生成服务。
    protected override void Dispose(bool disposing)
    {
        if (disposing && !_disposed)
        {
            _disposed = true;
            _lifetimeCancellation.Cancel();
            _generatorService.Dispose();
            _lifetimeCancellation.Dispose();
            components?.Dispose();
        }

        base.Dispose(disposing);
    }
}
