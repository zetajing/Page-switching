namespace Page_switching.panel
{
    public partial class SignalCorrectionPage : UserControl
    {
        private readonly CancellationTokenSource _lifetime = new();
        private bool _busy;

        public SignalCorrectionPage()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            originalBrowse.Click += (_, _) => Select(originalPath, false);
            measuredBrowse.Click += (_, _) => Select(measuredPath, false);
            outputBrowse.Click += (_, _) => Select(outputPath, true);
            runButton.Click += async (_, _) => await RunAsync();
            previewButton.Click += (_, _) => Preview();
            Disposed += (_, _) => { _lifetime.Cancel(); _lifetime.Dispose(); };
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            // 最小画布会随 DPI 缩放，同步滚动范围，保证小窗口仍能访问全部输入和按钮。
            if (pagePanel != null && AutoScrollMinSize != pagePanel.MinimumSize)
                AutoScrollMinSize = pagePanel.MinimumSize;
            base.OnLayout(e);
        }

        private void Select(TextBox target, bool save)
        {
            using FileDialog dialog = save
                ? new SaveFileDialog { Filter = "造波信号 (*.csv)|*.csv", DefaultExt = "csv", AddExtension = true }
                : new OpenFileDialog { Filter = "波浪文件 (*.csv;*.txt;*.dat)|*.csv;*.txt;*.dat" };
            if (dialog.ShowDialog(this) == DialogResult.OK) target.Text = dialog.FileName;
        }

        private async Task RunAsync()
        {
            if (_busy) return;
            _busy = true;
            options.Enabled = false;
            try
            {
                var original = Path.GetFullPath(originalPath.Text);
                var destination = Path.GetFullPath(outputPath.Text);
                var measured = Path.GetFullPath(measuredPath.Text);
                if (string.Equals(original, destination, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(measured, destination, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("输出文件必须与原信号和实测文件不同。");
                LegacyWaveSignal.Read(original);
                var millimetres = unitInput.SelectedIndex == 0;
                var series = WaveSeriesFile.Read(measured, (int)channelInput.Value, millimetres, (double)rateInput.Value);
                if (series.Values.Length < 64 || (series.Values.Length & (series.Values.Length - 1)) != 0)
                    throw new InvalidOperationException("旧修正程序要求至少64点且样本数为2的幂。");
                if ((int)pointsInput.Value % 2 == 0) throw new InvalidOperationException("光滑点数必须为奇数。");
                var passes = passesInput.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
                var points = pointsInput.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
                status.Text = "正在调用WC-12.exe修正……";
                await LegacyProgramRunner.RunAsync("WC-12.exe", "SigCorParamaters.dat", work =>
                {
                    var signal = Path.Combine(work, "original.csv");
                    var capture = Path.Combine(work, "measured.txt");
                    File.Copy(original, signal);
                    var input = new WaveSeries(series.Values.Select(x => x * (millimetres ? 1000 : 1)).ToArray(), series.Interval, 1);
                    WaveSeriesFile.WriteLegacy(capture, [input]);
                    // 只传所选测点，重新编号为1，避免通道号与列号混用。
                    return [passes, points, "1", "1", signal, capture, Path.Combine(work, "output.csv")];
                }, destination, _lifetime.Token);
                if (IsDisposed) return;
                Preview();
                status.Text = "修正结果已保存：" + destination + "；原信号已保留。";
                OperationJournal.Record("信号修正", $"依据CH{series.Channel}，旧程序单位{unitInput.Text}；" + status.Text);
            }
            catch (Exception ex) { if (!IsDisposed) status.Text = "修正失败：" + ex.Message; OperationJournal.Record("信号修正", ex.Message); }
            finally { _busy = false; if (!IsDisposed) options.Enabled = true; }
        }

        private void Preview()
        {
            try
            {
                var original = LegacyWaveSignal.Read(originalPath.Text);
                var corrected = LegacyWaveSignal.Read(outputPath.Text);
                if (original.Displacement.Length != corrected.Displacement.Length || Math.Abs(original.Interval - corrected.Interval) > 1e-9)
                    throw new InvalidDataException("新旧信号点数或时间步长不同，不能按同一时间轴对比。");
                plot.SetSeries(Enumerable.Range(0, original.Displacement.Length).Select(i => i * original.Interval).ToArray(),
                    original.Displacement, corrected.Displacement);
                status.Text = "新旧造波板位移对比已显示。";
                OperationJournal.Record("信号修正", status.Text);
            }
            catch (Exception ex) { status.Text = "预览失败：" + ex.Message; OperationJournal.Record("信号修正", status.Text); }
        }
    }
}
