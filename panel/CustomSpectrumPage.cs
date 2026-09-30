namespace Page_switching.panel
{
    public partial class CustomSpectrumPage : UserControl
    {
        private readonly CancellationTokenSource _lifetime = new();
        private bool _busy;

        public CustomSpectrumPage()
        {
            InitializeComponent();
            // 设计预览和运行时均保留原默认选项，不依赖设计器序列化。
            theoryInput.SelectedIndex = 0;
            sideInput.SelectedIndex = 0;
            modeInput.SelectedIndex = 0;
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            spectrumBrowse.Click += (_, _) => Browse(false);
            outputBrowse.Click += (_, _) => Browse(true);
            previewButton.Click += (_, _) => Preview();
            runButton.Click += async (_, _) => await RunAsync();
            Disposed += (_, _) => { _lifetime.Cancel(); _lifetime.Dispose(); };
        }

        private void Browse(bool save)
        {
            using FileDialog dialog = save ? new SaveFileDialog { Filter = "造波信号 (*.csv)|*.csv", DefaultExt = "csv", AddExtension = true }
                : new OpenFileDialog { Filter = "自定义波谱 (*.dat)|*.dat|所有文件 (*.*)|*.*" };
            if (dialog.ShowDialog(this) != DialogResult.OK) return;
            if (save) outputPath.Text = dialog.FileName;
            else { spectrumPath.Text = dialog.FileName; Preview(); }
        }

        private (double[] Frequency, double[] Density) ReadSpectrum()
        {
            var rows = File.ReadAllLines(spectrumPath.Text).Where(x => !string.IsNullOrWhiteSpace(x)).Skip(1)
                .Select(WaveSeriesFile.Fields).ToArray();
            if (rows.Length < 2 || rows.Any(x => x.Length < 2)) throw new InvalidDataException("波谱需有表头及至少两行频率/谱密度。");
            var f = rows.Select(x => WaveSeriesFile.Number(x[0])).ToArray();
            var s = rows.Select(x => WaveSeriesFile.Number(x[1])).ToArray();
            if (f.Any(x => x < 0) || s.Any(x => x < 0) || f.Zip(f.Skip(1), (a, b) => b <= a).Any(x => x))
                throw new InvalidDataException("频率应非负且严格递增，谱密度应非负。");
            return (f, s);
        }

        private void Preview()
        {
            try
            {
                var spectrum = ReadSpectrum();
                plot.HorizontalCaption = "Frequency (Hz)";
                plot.VerticalCaption = "S(f) (m²·s)";
                plot.FirstCaption = "输入谱";
                plot.SetSeries(spectrum.Frequency, spectrum.Density, []);
                status.Text = $"已读取 {spectrum.Frequency.Length} 个频率点。";
                OperationJournal.Record("自定义谱", spectrumPath.Text + "；" + status.Text);
            }
            catch (Exception ex) { status.Text = "波谱读取失败：" + ex.Message; OperationJournal.Record("自定义谱", status.Text); }
        }

        private async Task RunAsync()
        {
            if (_busy) return;
            _busy = true;
            options.Enabled = false;
            try
            {
                ReadSpectrum();
                var source = Path.GetFullPath(spectrumPath.Text);
                var destination = Path.GetFullPath(outputPath.Text);
                if (string.Equals(source, destination, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("输出路径不能覆盖输入谱。");
                string N(decimal value) => value.ToString(System.Globalization.CultureInfo.InvariantCulture);
                var prefix = new[] { N(depthInput.Value), "90", "0", "0", absorbInput.Checked ? "0.002" : N(stepInput.Value),
                    N(countInput.Value), N(angleInput.Value), N(frequencyInput.Value), N(periodInput.Value),
                    modeInput.SelectedIndex == 0 ? "2" : "3", N(seedInput.Value), (theoryInput.SelectedIndex + 1).ToString(),
                    (sideInput.SelectedIndex + 1).ToString() };
                status.Text = "正在调用WP-7.exe……";
                await LegacyProgramRunner.RunAsync("WP-7.exe", "BasicParameters_user.dat", work =>
                {
                    var input = Path.Combine(work, "InputSpectra.dat");
                    File.Copy(source, input);
                    return [.. prefix, Path.Combine(work, "output.csv"), input];
                }, destination, _lifetime.Token);
                if (IsDisposed) return;
                var result = LegacyWaveSignal.Read(destination);
                plot.HorizontalCaption = "Time (s)";
                plot.VerticalCaption = "造波板位移(mm)";
                plot.FirstCaption = "生成信号";
                plot.SetSeries(Enumerable.Range(0, result.Displacement.Length).Select(i => i * result.Interval).ToArray(),
                    result.Displacement, []);
                status.Text = $"生成完成：{result.Displacement.Length:N0}点；{destination}";
                OperationJournal.Record("自定义谱", status.Text);
            }
            catch (Exception ex) { if (!IsDisposed) status.Text = "生成失败：" + ex.Message; OperationJournal.Record("自定义谱", ex.Message); }
            finally { _busy = false; if (!IsDisposed) options.Enabled = true; }
        }
    }
}
