using System.Text;

namespace Page_switching.panel
{
    public partial class WaveAnalysisPage : UserControl
    {
        private WaveAnalysisResult? _result;
        private bool _busy;
        private readonly CancellationTokenSource _lifetime = new();
        private string _metadata = "{}";

        public WaveAnalysisPage()
        {
            InitializeComponent();
            // 设计器只创建控件，文件读取和分析事件留给运行时。
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            browseButton.Click += (_, _) => Browse();
            analyzeButton.Click += async (_, _) => await AnalyzeAsync();
            exportSpectrumButton.Click += (_, _) => Export(false);
            exportElementsButton.Click += (_, _) => Export(true);
            importResultsButton.Click += (_, _) => ImportLegacyResults();
            Disposed += (_, _) => { _lifetime.Cancel(); _lifetime.Dispose(); };
            inputPath.TextChanged += (_, _) => ClearResult();
            foreach (var number in new[] { channelInput, rateInput, heightInput, periodInput, gammaInput, passesInput, pointsInput, depthInput })
                number.ValueChanged += (_, _) => ClearResult();
            foreach (var picker in new[] { unitInput, spectrumInput, methodInput })
                picker.SelectedIndexChanged += (_, _) => ClearResult();
        }

        private void Browse()
        {
            using var dialog = new OpenFileDialog { Filter = "采集文件 (*.csv;*.txt;*.dat)|*.csv;*.txt;*.dat" };
            if (dialog.ShowDialog(this) == DialogResult.OK) inputPath.Text = dialog.FileName;
        }

        private async Task AnalyzeAsync()
        {
            if (_busy) return;
            _result = null;
            elements.Rows.Clear();
            plot.SetSeries([], [], []);
            _busy = true;
            analyzeButton.Enabled = false;
            options.Enabled = false;
            exportSpectrumButton.Enabled = exportElementsButton.Enabled = false;
            try
            {
                var path = inputPath.Text;
                var channel = (int)channelInput.Value;
                var mm = unitInput.SelectedIndex == 0;
                var hs = (double)heightInput.Value;
                var period = (double)periodInput.Value;
                var gamma = (double)gammaInput.Value;
                var passes = (int)passesInput.Value;
                var points = (int)pointsInput.Value;
                var index = spectrumInput.SelectedIndex;
                var code = new[] { 1, 6, 4, 9, 0, 0, 0, 0 }[index];
                var rate = (double)rateInput.Value;
                var depth = (double)depthInput.Value;
                var legacy = methodInput.SelectedIndex == 1;
                var metadata = System.Text.Json.JsonSerializer.Serialize(new { Source = path, Channel = channel,
                    LegacyUnit = unitInput.Text, CsvSampleRate = rate, Height = hs, Period = period, Depth = depth,
                    Gamma = gamma, Spectrum = spectrumInput.Text, Passes = passes, Points = points,
                    Method = legacy ? "Legacy-WaveTJU-6m-11" : "MeanRemoved-Hann-OneSidedFFT-UpCrossing" });
                if (!legacy && code == 0) throw new InvalidOperationException("内置分析支持JONSWAP、P-M、B谱、ITTC；其他谱请选择旧分析程序，并提供对应EXE。");
                status.Text = "正在读取和计算……";
                var series = await Task.Run(() => WaveSeriesFile.Read(path, channel, mm, rate));
                var result = legacy
                    ? await LegacyWaveAnalysis.AnalyzeAsync(series, hs, period, depth, gamma,
                        new[] { 1, 2, 4, 6, 3, 7, 8, 9 }[index], passes, points, _lifetime.Token)
                    : await Task.Run(() => WaveAnalysisService.Analyze(series, hs, period, gamma, code, passes, points));
                if (IsDisposed) return;
                _result = result;
                var information = System.Text.Json.Nodes.JsonNode.Parse(metadata)!.AsObject();
                information["SampleIntervalSeconds"] = series.Interval;
                information["UsedSamples"] = result.UsedSamples;
                information["WaveCount"] = result.WaveCount;
                _metadata = information.ToJsonString();
                foreach (var item in result.Statistics)
                    elements.Rows.Add(item.Key, double.IsFinite(item.Value) ? item.Value.ToString("0.######") : "无完整波");
                plot.SetSeries(result.Frequencies, result.Theoretical, result.Measured);
                plot.VerticalCaption = legacy ? "S(f)（旧文件单位）" : "S(f) (m²·s)";
                status.Text = legacy ? "旧程序结果已读取；波高保留旧程序单位，须用实测样例确认。"
                    : $"CH{channel}：使用前 {result.UsedSamples:N0} 点，{result.WaveCount} 个完整上跨零波；去均值、Hann窗、单边FFT谱。理论谱为深水模型，水深只记录，未做浅水修正；旧EXE等价性待验收。";
                OperationJournal.Record("波浪分析", path + "；" + status.Text);
                exportSpectrumButton.Enabled = exportElementsButton.Enabled = true;
            }
            catch (Exception ex) { if (!IsDisposed) status.Text = "分析失败：" + ex.Message; OperationJournal.Record("波浪分析", ex.Message); }
            finally { _busy = false; if (!IsDisposed) { analyzeButton.Enabled = true; options.Enabled = true; } }
        }

        private void Export(bool statistics)
        {
            if (_result is null) return;
            using var dialog = new SaveFileDialog
            {
                Filter = "分析文件 (*.dat)|*.dat|CSV文件 (*.csv)|*.csv", DefaultExt = "dat", AddExtension = true,
                FileName = statistics ? "WaveParameters.dat" : "Spectra.dat"
            };
            if (dialog.ShowDialog(this) != DialogResult.OK) return;
            var temporary = dialog.FileName + "." + Guid.NewGuid().ToString("N") + ".tmp";
            try
            {
                if (!string.IsNullOrWhiteSpace(inputPath.Text) && string.Equals(Path.GetFullPath(inputPath.Text),
                        Path.GetFullPath(dialog.FileName), StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("分析输出不能覆盖原采集文件。");
                using var writer = new StreamWriter(temporary, false, new UTF8Encoding(false));
                var separator = Path.GetExtension(dialog.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase) ? "," : "\t";
                if (statistics)
                {
                    if (separator == ",")
                    {
                        writer.WriteLine("Element,Value");
                        foreach (var item in _result.Statistics) writer.WriteLine(item.Key + "," + WaveSeriesFile.Format(item.Value));
                    }
                    else
                    {
                        var names = new[] { "max", "1/100", "1/10", "1/3", "_ave", "min" };
                        double Value(string prefix) => _result.Statistics.First(x => x.Key.StartsWith(prefix + " (", StringComparison.Ordinal)).Value;
                        if (names.Any(x => !double.IsFinite(Value("H" + x))))
                            throw new InvalidOperationException("没有完整波，不能导出旧格式波浪要素；可导出CSV查看频谱统计。");
                        writer.WriteLine(_result.Statistics.Keys.Any(x => x.Contains("旧单位")) ? "Wave Height (legacy unit)" : "Wave Height (m)");
                        foreach (var name in names) writer.WriteLine(WaveSeriesFile.Format(Value("H" + name)));
                        writer.WriteLine("Wave Period (s)");
                        foreach (var name in names) writer.WriteLine(WaveSeriesFile.Format(Value("TH" + name)));
                    }
                }
                else
                {
                    writer.WriteLine(string.Join(separator, "Frequency", "Theoretical", "Measured"));
                    for (var i = 0; i < _result.Frequencies.Length; i++)
                        writer.WriteLine(string.Join(separator, new[] { _result.Frequencies[i], _result.Theoretical[i], _result.Measured[i] }.Select(WaveSeriesFile.Format)));
                }
                writer.Flush();
                writer.Dispose();
                File.Move(temporary, dialog.FileName, true);
                File.WriteAllText(dialog.FileName + ".json", _metadata);
                status.Text = "已导出：" + dialog.FileName;
                OperationJournal.Record("波浪分析", status.Text);
            }
            catch (Exception ex) { status.Text = "导出失败：" + ex.Message; OperationJournal.Record("波浪分析", status.Text); }
            finally { if (File.Exists(temporary)) File.Delete(temporary); }
        }

        private void ImportLegacyResults()
        {
            if (_busy) return;
            using var spectrum = new OpenFileDialog { Filter = "旧频谱结果 (*.dat)|*.dat", Title = "选择旧频谱结果" };
            if (spectrum.ShowDialog(this) != DialogResult.OK) return;
            using var elementsFile = new OpenFileDialog { Filter = "旧波浪要素 (*.dat)|*.dat", Title = "选择旧波浪要素结果" };
            if (elementsFile.ShowDialog(this) != DialogResult.OK) return;
            try
            {
                var result = LegacyWaveAnalysis.Read(spectrum.FileName, elementsFile.FileName);
                _result = result;
                _metadata = System.Text.Json.JsonSerializer.Serialize(new { Method = "ImportedLegacyResult", Spectrum = spectrum.FileName, Elements = elementsFile.FileName });
                elements.Rows.Clear();
                foreach (var item in result.Statistics) elements.Rows.Add(item.Key, item.Value.ToString("0.######"));
                plot.SetSeries(result.Frequencies, result.Theoretical, result.Measured);
                plot.VerticalCaption = "S(f)（旧文件单位）";
                exportSpectrumButton.Enabled = exportElementsButton.Enabled = true;
                status.Text = "已导入旧结果；波高保留旧程序单位，未重新计算。";
                OperationJournal.Record("波浪分析", status.Text + spectrum.FileName);
            }
            catch (Exception ex) { status.Text = "旧结果导入失败：" + ex.Message; OperationJournal.Record("波浪分析", status.Text); }
        }

        internal void OpenSession(WaveCaptureManifest manifest, int? channel)
        {
            inputPath.Text = manifest.CsvPath;
            channelInput.Value = channel ?? manifest.Channels.FirstOrDefault(1);
            rateInput.Value = Math.Clamp(manifest.SampleRateHz, 1, 1000);
            ClearResult();
        }

        private void ClearResult()
        {
            if (_busy) return;
            _result = null;
            elements.Rows.Clear();
            plot.SetSeries([], [], []);
            exportSpectrumButton.Enabled = exportElementsButton.Enabled = false;
        }
    }
}
