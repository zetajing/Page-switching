namespace Page_switching
{
    public partial class Auto : UserControl
    {
        private readonly AxisService _axisService;
        private readonly bool _ownsAxisService;
        private readonly System.Windows.Forms.Timer _refreshTimer;
        private readonly CancellationTokenSource _lifetimeCancellation = new();
        private bool _refreshInProgress;

        public Auto()
            : this(new AxisService(AxisServiceOptions.FromConfiguration()), true)
        {
        }

        public Auto(AxisService axisService)
            : this(axisService, false)
        {
        }

        private Auto(AxisService axisService, bool ownsAxisService)
        {
            _axisService = axisService ?? throw new ArgumentNullException(nameof(axisService));
            _ownsAxisService = ownsAxisService;
            InitializeComponent();

            _refreshTimer = new System.Windows.Forms.Timer
            {
                Interval = _axisService.RefreshIntervalMilliseconds
            };
            _refreshTimer.Tick += RefreshTimer_Tick;
            VisibleChanged += Auto_VisibilityChanged;
            ParentChanged += Auto_VisibilityChanged;
            Disposed += Auto_Disposed;
            UpdateConnectionLabel();
        }

        private void Auto_VisibilityChanged(object? sender, EventArgs e)
        {
            if (!Visible || Parent is null || IsDisposed)
            {
                _refreshTimer.Stop();
                return;
            }

            _refreshTimer.Start();
            _ = RefreshValuesAsync();
        }

        private async void RefreshTimer_Tick(object? sender, EventArgs e)
        {
            await RefreshValuesAsync();
        }

        private async Task RefreshValuesAsync()
        {
            if (_refreshInProgress || IsDisposed || _lifetimeCancellation.IsCancellationRequested)
            {
                return;
            }

            _refreshInProgress = true;
            try
            {
                var snapshots = await _axisService.ReadSnapshotAsync(_lifetimeCancellation.Token);
                if (IsDisposed || _lifetimeCancellation.IsCancellationRequested)
                {
                    return;
                }

                var axis = snapshots.Count > 0 ? snapshots[0] : null;
                SetValue(axis_Location, FormatValue(axis?.ActualPosition, _axisService.Unit));
                SetValue(axis_Speed, FormatValue(axis?.Speed, _axisService.Unit + "/s"));
                UpdateConnectionLabel();
            }
            catch (OperationCanceledException)
            {
                // 页面关闭或切换时取消刷新。
            }
            catch
            {
                SetValue(axis_Location, "读取失败");
                SetValue(axis_Speed, "读取失败");
                UpdateConnectionLabel();
            }
            finally
            {
                _refreshInProgress = false;
            }
        }

        private void UpdateConnectionLabel()
        {
            label2.Text = "轴 1 · " + _axisService.ConnectionStateText;
            label2.ForeColor = _axisService.IsSimulation
                ? Color.FromArgb(37, 99, 235)
                : _axisService.IsConnected
                    ? Color.FromArgb(5, 150, 105)
                    : Color.FromArgb(220, 38, 38);
        }

        private static void SetValue(TextBox textBox, string value)
        {
            if (!textBox.IsDisposed && textBox.Text != value)
            {
                textBox.Text = value;
            }
        }

        private static string FormatValue(double? value, string unit) =>
            value.HasValue ? $"{value.Value:0.00} {unit}" : "--";

        private void Auto_Disposed(object? sender, EventArgs e)
        {
            _refreshTimer.Stop();
            _refreshTimer.Dispose();
            _lifetimeCancellation.Cancel();
            _lifetimeCancellation.Dispose();
            if (_ownsAxisService)
            {
                _axisService.Dispose();
            }
        }
    }
}
