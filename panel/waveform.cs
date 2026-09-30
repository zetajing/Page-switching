using System.Globalization;

namespace Page_switching.panel
{
    public partial class WaveformPage : UserControl
    {
        private readonly WaveformGeneratorService _generatorService = null!;
        private readonly CancellationTokenSource _lifetimeCancellation = new();
        private bool _generationInProgress;
        private bool _disposed;

        // 初始化波形页面、生成服务和下拉选项。
        public WaveformPage()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            _generatorService = new WaveformGeneratorService(WaveformGeneratorOptions.FromConfiguration());
            regularPage.GenerateRequested += GenerateRegularButton_Click;
            regularPage.OutputBrowseRequested += BrowseRegularOutputButton_Click;
            irregularPage.GenerateRequested += GenerateIrregularButton_Click;
            irregularPage.OutputBrowseRequested += BrowseIrregularOutputButton_Click;
        }

        // 选择规则波输出文件路径。
        private void BrowseRegularOutputButton_Click(object? sender, EventArgs e) =>
            SelectOutputPath(regularPage.regularOutputTextBox);

        // 选择不规则波输出文件路径。
        private void BrowseIrregularOutputButton_Click(object? sender, EventArgs e) =>
            SelectOutputPath(irregularPage.irregularOutputTextBox);

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
                if (_generatorService.GenerationModeText == "WFast.exe" && regularPage.regularTheoryComboBox.SelectedIndex >= 3)
                    throw new InvalidOperationException("流函数和椭圆余弦选项请使用旧WP-5-6.exe方案。");
                if (_generatorService.GenerationModeText != "WaveMaker 内置算法" && regularPage.regularTheoryComboBox.SelectedIndex == 1)
                    throw new InvalidOperationException("二阶Stokes选项仅用于WaveMaker内置算法。");
                var outputPath = RequireOutputPath(regularPage.regularOutputTextBox);
                var parameters = new RegularWaveParameters(
                    ParseDouble(regularPage.regularDepthTextBox, "水深", 0),
                    ParseDouble(regularPage.regularPeriodTextBox, "周期", 0),
                    ParseDouble(regularPage.regularHeightTextBox, "波高", 0),
                    ParseDouble(regularPage.regularDirectionTextBox, "波向角", -360, 360),
                    ParseDouble(regularPage.regularTimeStepTextBox, "时间步长", 0),
                    ParseInt(regularPage.regularSampleCountTextBox, "时序总数", 2, 1_000_000),
                    ParseDouble(regularPage.regularCharacteristicFrequencyTextBox, "特征频率", 0),
                    ParseDouble(regularPage.regularCharacteristicPeriodTextBox, "特征周期", 0),
                    regularPage.regularTheoryComboBox.SelectedIndex + 1,
                    GetSideCode(regularPage.regularSegmentComboBox));

                SetGenerationState(true, regularPage.regularStatusLabel);
                regularPage.regularStatusLabel.Text = $"正在使用 {_generatorService.GenerationModeText} 生成规则波……";
                var result = await _generatorService.GenerateRegularAsync(
                    parameters,
                    outputPath,
                    _lifetimeCancellation.Token);
                regularPage.regularPreview.UnitText = result.Unit;
                regularPage.regularPreview.SetSamples(result.Samples, result.SampleInterval ?? parameters.TimeStep);
                regularPage.regularStatusLabel.Text =
                    $"生成成功：{result.Samples.Count:n0} 点，耗时 {result.Elapsed.TotalSeconds:0.##} 秒";
            }
            catch (OperationCanceledException)
            {
                regularPage.regularStatusLabel.ForeColor = UiPalette.Warning;
                regularPage.regularStatusLabel.Text = "规则波生成已取消。";
            }
            catch (Exception ex)
            {
                regularPage.regularStatusLabel.ForeColor = UiPalette.Danger;
                regularPage.regularStatusLabel.Text = "规则波生成失败：" + ex.Message;
            }
            finally
            {
                SetGenerationState(false, regularPage.regularStatusLabel);
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
                var outputPath = RequireOutputPath(irregularPage.irregularOutputTextBox);
                var parameters = new IrregularWaveParameters(
                    ParseDouble(irregularPage.irregularDepthTextBox, "水深", 0),
                    ParseDouble(irregularPage.irregularSignificantPeriodTextBox, "有效周期", 0),
                    ParseDouble(irregularPage.irregularSignificantHeightTextBox, "有效波高", 0),
                    ParseDouble(irregularPage.irregularDirectionTextBox, "主波向", -360, 360),
                    ParseDouble(irregularPage.irregularTimeStepTextBox, "时间步长", 0),
                    ParseInt(irregularPage.irregularSampleCountTextBox, "时序总数", 2, 1_000_000),
                    ParseDouble(irregularPage.irregularCharacteristicFrequencyTextBox, "特征频率", 0),
                    ParseDouble(irregularPage.irregularCharacteristicPeriodTextBox, "特征周期", 0),
                    ParseDouble(irregularPage.irregularPeakFactorTextBox, "谱峰因子", 0),
                    ParseInt(irregularPage.irregularRandomSeedTextBox, "随机种子", int.MinValue, int.MaxValue),
                    ParseDouble(irregularPage.irregularMinimumPeriodTextBox, "最小周期", 0),
                    ParseDouble(irregularPage.irregularMaximumPeriodTextBox, "最大周期", 0),
                    ParseDouble(irregularPage.irregularMinimumDifferencePeriodTextBox, "最小差频周期", 0),
                    ParseDouble(irregularPage.irregularMaximumDifferencePeriodTextBox, "最大差频周期", 0),
                    ParseDouble(irregularPage.irregularNegativeDirectionTextBox, "负向偏角", -360, 0),
                    ParseDouble(irregularPage.irregularPositiveDirectionTextBox, "正向偏角", 0, 360),
                    irregularPage.irregularModeComboBox.SelectedIndex == 0 ? 2 : 3,
                    GetSpectrumCode(irregularPage.irregularSpectrumComboBox),
                    irregularPage.irregularTheoryComboBox.SelectedIndex + 1,
                    GetSideCode(irregularPage.irregularSegmentComboBox));

                if (parameters.MinimumPeriod > parameters.MaximumPeriod)
                {
                    throw new InvalidOperationException("最小周期不能大于最大周期。");
                }

                if (parameters.MinimumDifferencePeriod > parameters.MaximumDifferencePeriod)
                {
                    throw new InvalidOperationException("最小差频周期不能大于最大差频周期。");
                }

                SetGenerationState(true, irregularPage.irregularStatusLabel);
                irregularPage.irregularStatusLabel.Text = $"正在使用 {_generatorService.GenerationModeText} 生成不规则波……";
                var result = await _generatorService.GenerateIrregularAsync(
                    parameters,
                    outputPath,
                    _lifetimeCancellation.Token);
                irregularPage.irregularPreview.UnitText = result.Unit;
                irregularPage.irregularPreview.SetSamples(result.Samples, result.SampleInterval ?? parameters.TimeStep);
                irregularPage.irregularStatusLabel.Text =
                    $"生成成功：{result.Samples.Count:n0} 点，耗时 {result.Elapsed.TotalSeconds:0.##} 秒";
            }
            catch (OperationCanceledException)
            {
                irregularPage.irregularStatusLabel.ForeColor = UiPalette.Warning;
                irregularPage.irregularStatusLabel.Text = "不规则波生成已取消。";
            }
            catch (Exception ex)
            {
                irregularPage.irregularStatusLabel.ForeColor = UiPalette.Danger;
                irregularPage.irregularStatusLabel.Text = "不规则波生成失败：" + ex.Message;
            }
            finally
            {
                SetGenerationState(false, irregularPage.irregularStatusLabel);
            }
        }

        // 生成期间禁用操作按钮并更新状态提示。
        private void SetGenerationState(bool generating, Label statusLabel)
        {
            _generationInProgress = generating;
            regularPage.regularGenerateButton.Enabled = !generating;
            irregularPage.irregularGenerateButton.Enabled = !generating;
            regularPage.regularBrowseOutputButton.Enabled = !generating;
            irregularPage.irregularBrowseOutputButton.Enabled = !generating;
            if (generating)
            {
                statusLabel.ForeColor = UiPalette.Primary;
            }
            else if (!statusLabel.Text.Contains("失败", StringComparison.Ordinal))
            {
                statusLabel.ForeColor = UiPalette.Success;
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
                _generatorService?.Dispose();
                _lifetimeCancellation.Dispose();
                components?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
