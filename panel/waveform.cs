using System.Globalization;

namespace Page_switching.panel;

public partial class WaveformPage : UserControl
{
    private readonly WaveformGeneratorService _generatorService;
    private readonly CancellationTokenSource _lifetimeCancellation = new();
    private bool _generationInProgress;

    public WaveformPage()
    {
        InitializeComponent();
        _generatorService = new WaveformGeneratorService(WaveformGeneratorOptions.FromConfiguration());
        InitializeWaveformChoices();
    }

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

    private void BrowseRegularOutputButton_Click(object? sender, EventArgs e) =>
        SelectOutputPath(regularOutputTextBox);

    private void BrowseIrregularOutputButton_Click(object? sender, EventArgs e) =>
        SelectOutputPath(irregularOutputTextBox);

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
            regularStatusLabel.Text = "正在调用 WFast.exe 生成规则波……";
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
            regularStatusLabel.Text = "规则波生成已取消。";
        }
        catch (Exception ex)
        {
            regularStatusLabel.Text = "规则波生成失败：" + ex.Message;
        }
        finally
        {
            SetGenerationState(false, regularGenerateButton, regularStatusLabel);
        }
    }

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
            irregularStatusLabel.Text = "正在调用 WFast.exe 生成不规则波……";
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
            irregularStatusLabel.Text = "不规则波生成已取消。";
        }
        catch (Exception ex)
        {
            irregularStatusLabel.Text = "不规则波生成失败：" + ex.Message;
        }
        finally
        {
            SetGenerationState(false, irregularGenerateButton, irregularStatusLabel);
        }
    }

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

    private static string RequireOutputPath(TextBox textBox)
    {
        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            throw new InvalidOperationException("请先选择造波信号保存路径。");
        }

        return Path.GetFullPath(textBox.Text.Trim());
    }

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

    private static int ParseInt(TextBox textBox, string caption, int minimum, int maximum)
    {
        if (!int.TryParse(textBox.Text.Trim(), NumberStyles.Integer,
                CultureInfo.InvariantCulture, out var value) || value < minimum || value > maximum)
        {
            throw new InvalidOperationException($"{caption}必须是 {minimum} 到 {maximum} 之间的整数。");
        }

        return value;
    }

    private static int GetSideCode(ComboBox comboBox) => comboBox.SelectedIndex switch
    {
        1 => 2,
        2 => 3,
        _ => 1
    };

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

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _lifetimeCancellation.Cancel();
            _generatorService.Dispose();
            _lifetimeCancellation.Dispose();
            components?.Dispose();
        }

        base.Dispose(disposing);
    }
}
