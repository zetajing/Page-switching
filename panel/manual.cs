namespace Page_switching
{
    public partial class Manual : UserControl
    {
        private readonly AxisService _axisService;
        private readonly bool _ownsAxisService;
        private readonly System.Windows.Forms.Timer _refreshTimer;
        private readonly CancellationTokenSource _lifetimeCancellation = new();
        private readonly ServoPositionIndicator[] _positionIndicators;
        private readonly Label[] _statusLabels;
        private readonly Label[] _negativeLimitLamps;
        private readonly Label[] _originLamps;
        private readonly Label[] _positiveLimitLamps;
        private IReadOnlyList<AxisSnapshot> _lastSnapshots = Array.Empty<AxisSnapshot>();
        private bool _refreshInProgress;
        private bool _commandInProgress;
        private Button? _activeCommandButton;
        private bool _jogActive;
        private bool _jogPositive;
        private int _jogAxisNumber;

        // 供设计器使用；自行创建并管理轴服务。
        public Manual()
            : this(new AxisService(AxisServiceOptions.FromConfiguration()), true)
        {
        }

        // 使用主窗体传入的共享轴服务创建手动页面。
        public Manual(AxisService axisService)
            : this(axisService, false)
        {
        }

        // 初始化手动页面、轴指示器和定时刷新器。
        private Manual(AxisService axisService, bool ownsAxisService)
        {
            _axisService = axisService ?? throw new ArgumentNullException(nameof(axisService));
            _ownsAxisService = ownsAxisService;

            InitializeComponent();
            Size = new Size(1210, 796);

            _positionIndicators =
            [
                axis1PositionIndicator,
                axis2PositionIndicator,
                axis3PositionIndicator,
                axis4PositionIndicator
            ];
            _statusLabels =
            [
                axis1StatusLabel,
                axis2StatusLabel,
                axis3StatusLabel,
                axis4StatusLabel
            ];

            foreach (var indicator in _positionIndicators)
            {
                indicator.MinimumPosition = _axisService.MinimumPosition;
                indicator.MaximumPosition = _axisService.MaximumPosition;
                indicator.UnitText = _axisService.Unit;
            }

            _negativeLimitLamps = [axis1NegativeLimitLamp, axis2NegativeLimitLamp, axis3NegativeLimitLamp, axis4NegativeLimitLamp];
            _originLamps = [axis1OriginLamp, axis2OriginLamp, axis3OriginLamp, axis4OriginLamp];
            _positiveLimitLamps = [axis1PositiveLimitLamp, axis2PositiveLimitLamp, axis3PositiveLimitLamp, axis4PositiveLimitLamp];

            axisSelector.SelectedIndex = 0;
            _refreshTimer = new System.Windows.Forms.Timer
            {
                Interval = _axisService.RefreshIntervalMilliseconds
            };
            _refreshTimer.Tick += RefreshTimer_Tick;
            VisibleChanged += Manual_VisibilityChanged;
            ParentChanged += Manual_VisibilityChanged;
            Disposed += Manual_Disposed;
            UpdateConnectionState();
        }

        // 页面显示时开始刷新，隐藏时停止刷新和正在进行的点动。
        private void Manual_VisibilityChanged(object? sender, EventArgs e)
        {
            if (!Visible || Parent is null || IsDisposed)
            {
                _refreshTimer.Stop();
                _ = StopActiveJogAsync();
                return;
            }

            _refreshTimer.Start();
            _ = RefreshValuesAsync();
        }

        // 定时器到期后读取一次四轴状态。
        private async void RefreshTimer_Tick(object? sender, EventArgs e)
        {
            await RefreshValuesAsync();
        }

        // 从轴服务读取最新状态并更新手动页面。
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

                _lastSnapshots = snapshots;
                UpdateAxisOverview(snapshots);
                UpdateSelectedAxisDetails();
                UpdateConnectionState();
            }
            catch (OperationCanceledException)
            {
                // 页面关闭或切换时取消刷新属于正常生命周期。
            }
            catch (Exception ex)
            {
                helperLabel.Text = "刷新失败：" + ex.Message;
                UpdateAxisOverview(Array.Empty<AxisSnapshot>());
                UpdateConnectionState();
            }
            finally
            {
                _refreshInProgress = false;
            }
        }

        // 更新四根轴的位置指示、状态文字和限位信号灯。
        private void UpdateAxisOverview(IReadOnlyList<AxisSnapshot> snapshots)
        {
            for (var index = 0; index < _positionIndicators.Length; index++)
            {
                var snapshot = index < snapshots.Count
                    ? snapshots[index]
                    : new AxisSnapshot(index + 1);
                _positionIndicators[index].Update(
                    snapshot,
                    _axisService.Unit,
                    _axisService.MinimumPosition,
                    _axisService.MaximumPosition,
                    _axisService.IsConnected);
                _statusLabels[index].Text = snapshot.StatusText;
                _statusLabels[index].ForeColor = GetStatusColor(snapshot);
                var connected = _axisService.IsConnected;
                UpdateSignalLamp(_negativeLimitLamps[index], "负限位",
                    connected && snapshot.NegativeLimitAvailable ? snapshot.NegativeLimit : null, Color.FromArgb(234, 88, 12));
                UpdateSignalLamp(_originLamps[index], "原点",
                    connected ? snapshot.OriginSignal : null, Color.FromArgb(5, 150, 105));
                UpdateSignalLamp(_positiveLimitLamps[index], "正限位",
                    connected && snapshot.PositiveLimitAvailable ? snapshot.PositiveLimit : null, Color.FromArgb(234, 88, 12));
            }
        }

        // 根据布尔信号更新一个状态灯的文字和颜色。
        private static void UpdateSignalLamp(Label lamp, string caption, bool? active, Color activeColor)
        {
            lamp.Text = $"{(active == true ? "●" : "○")} {caption}{(active.HasValue ? string.Empty : " --")}";
            lamp.BackColor = active == true ? activeColor : Color.FromArgb(241, 245, 249);
            lamp.ForeColor = active == true ? Color.White : active.HasValue ? Color.FromArgb(71, 85, 105) : Color.Gray;
            lamp.AccessibleName = $"{caption}：{(active.HasValue ? active.Value ? "触发" : "未触发" : "无有效数据")}";
        }

        // 显示当前选中轴的状态、位置、速度和可用控制按钮。
        private void UpdateSelectedAxisDetails()
        {
            var selectedIndex = Math.Max(0, axisSelector.SelectedIndex);
            if (selectedIndex >= _lastSnapshots.Count)
            {
                selectedStatusLabel.Text = "未连接";
                selectedActualLabel.Text = "--";
                selectedSpeedLabel.Text = "--";
            }
            else
            {
                var snapshot = _lastSnapshots[selectedIndex];
                selectedStatusLabel.Text = snapshot.StatusText;
                selectedStatusLabel.ForeColor = GetStatusColor(snapshot);
                selectedActualLabel.Text = FormatValue(snapshot.ActualPosition, _axisService.Unit);
                selectedSpeedLabel.Text = FormatValue(snapshot.Speed, _axisService.Unit + "/s");
            }

            var canControlAxis = _axisService.CanControlAxis(selectedIndex + 1);
            SetCommandButtonEnabled(jogNegativeButton, canControlAxis);
            SetCommandButtonEnabled(jogPositiveButton, canControlAxis);
            SetCommandButtonEnabled(homeSelectedButton, canControlAxis);
            SetCommandButtonEnabled(stopSelectedButton, canControlAxis);
        }

        // 更新 ADS 连接文字、颜色和所有控制按钮状态。
        private void UpdateConnectionState()
        {
            connectionStateLabel.Text = _axisService.ConnectionStateText;
            connectionStateLabel.ForeColor = _axisService.IsConnected
                ? Color.FromArgb(5, 150, 105)
                : Color.FromArgb(220, 38, 38);

            var canControlAll = _axisService.CanControlAll;
            SetCommandButtonEnabled(enableAllButton, canControlAll);
            SetCommandButtonEnabled(disableAllButton, canControlAll);
            SetCommandButtonEnabled(resetAlarmButton, canControlAll);
            SetCommandButtonEnabled(homeAllButton, canControlAll);
            SetCommandButtonEnabled(stopAllButton, canControlAll);
            UpdateSelectedAxisDetails();
        }

        // 根据连接状态和当前执行命令设置按钮是否可用。
        private void SetCommandButtonEnabled(Button button, bool canControl)
        {
            var enabled = canControl && !ReferenceEquals(button, _activeCommandButton);
            if (button.Enabled != enabled)
            {
                button.Enabled = enabled;
            }
        }

        // 切换轴选择后刷新所选轴的详细信息。
        private void AxisSelector_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateSelectedAxisDetails();
        }

        // 点击后将四根轴的使能变量全部写为 true。
        private async void EnableAllButton_Click(object? sender, EventArgs e)
        {
            await RunCommandAsync(sender as Button,
                token => _axisService.EnableAllAsync(token), "全部轴已使能");
        }

        // 点击后将四根轴的使能变量全部写为 false。
        private async void DisableAllButton_Click(object? sender, EventArgs e)
        {
            await RunCommandAsync(sender as Button,
                token => _axisService.DisableAllAsync(token), "全部轴已取消使能");
        }

        // 点击后触发四根轴的报警复位变量。
        private async void ResetAlarmButton_Click(object? sender, EventArgs e)
        {
            await RunCommandAsync(sender as Button,
                token => _axisService.ResetAlarmsAsync(token), "报警复位命令已执行");
        }

        // 点击后触发四根轴的回零变量。
        private async void HomeAllButton_Click(object? sender, EventArgs e)
        {
            await RunCommandAsync(sender as Button,
                token => _axisService.HomeAllAsync(token), "全部轴开始回零");
        }

        // 点击后触发四根轴的停止变量。
        private async void StopAllButton_Click(object? sender, EventArgs e)
        {
            await RunCommandAsync(sender as Button,
                token => _axisService.StopAllAsync(token), "全部轴已停止");
        }

        // 点击后触发当前所选轴的回零变量。
        private async void HomeSelectedButton_Click(object? sender, EventArgs e)
        {
            var axisNumber = SelectedAxisNumber;
            await RunCommandAsync(
                sender as Button,
                token => _axisService.HomeAxisAsync(axisNumber, token),
                $"轴 {axisNumber} 开始回零");
        }

        // 点击后触发当前所选轴的停止变量。
        private async void StopSelectedButton_Click(object? sender, EventArgs e)
        {
            var axisNumber = SelectedAxisNumber;
            await RunCommandAsync(
                sender as Button,
                token => _axisService.StopAxisAsync(axisNumber, token),
                $"轴 {axisNumber} 已停止");
        }

        // 按下负向点动按钮时启动负向点动。
        private async void JogNegativeButton_MouseDown(object? sender, MouseEventArgs e)
        {
            await StartJogAsync(false);
        }

        // 按下正向点动按钮时启动正向点动。
        private async void JogPositiveButton_MouseDown(object? sender, MouseEventArgs e)
        {
            await StartJogAsync(true);
        }

        // 松开点动按钮时立即停止当前点动。
        private async void JogButton_MouseUp(object? sender, EventArgs e)
        {
            await StopActiveJogAsync();
        }

        // 写入点动速度和方向启动信号。
        private async Task StartJogAsync(bool positive)
        {
            if (_jogActive || _commandInProgress)
            {
                return;
            }

            _jogAxisNumber = SelectedAxisNumber;
            _jogPositive = positive;
            _jogActive = true;
            try
            {
                await _axisService.JogAsync(
                    _jogAxisNumber,
                    positive,
                    true,
                    decimal.ToDouble(jogSpeedInput.Value),
                    _lifetimeCancellation.Token);
                helperLabel.Text = $"轴 {_jogAxisNumber} 正在{(positive ? "正向" : "负向")}点动";
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _jogActive = false;
                helperLabel.Text = "点动失败：" + ex.Message;
            }
        }

        // 清除当前轴的点动方向信号。
        private async Task StopActiveJogAsync()
        {
            if (!_jogActive)
            {
                return;
            }

            var axisNumber = _jogAxisNumber;
            var positive = _jogPositive;
            _jogActive = false;
            try
            {
                await _axisService.JogAsync(
                    axisNumber,
                    positive,
                    false,
                    decimal.ToDouble(jogSpeedInput.Value),
                    _lifetimeCancellation.Token);
                if (!IsDisposed)
                {
                    helperLabel.Text = $"轴 {axisNumber} 点动已停止";
                }
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                if (!IsDisposed)
                {
                    helperLabel.Text = "停止点动失败：" + ex.Message;
                }
            }
        }

        // 统一执行按钮命令、显示结果并在成功后刷新轴状态。
        private async Task RunCommandAsync(
            Button? sourceButton,
            Func<CancellationToken, Task> action,
            string successMessage)
        {
            if (_commandInProgress)
            {
                return;
            }

            _commandInProgress = true;
            _activeCommandButton = sourceButton;
            UpdateConnectionState();
            try
            {
                await action(_lifetimeCancellation.Token);
                helperLabel.Text = successMessage;
                await RefreshValuesAsync();
            }
            catch (OperationCanceledException)
            {
                // 页面关闭时取消命令。
            }
            catch (Exception ex)
            {
                helperLabel.Text = "操作失败：" + ex.Message;
            }
            finally
            {
                _commandInProgress = false;
                _activeCommandButton = null;
                if (!IsDisposed)
                {
                    UpdateConnectionState();
                }
            }
        }

        private int SelectedAxisNumber => Math.Max(0, axisSelector.SelectedIndex) + 1;

        // 将可空数值格式化为带单位的界面文字。
        private static string FormatValue(double? value, string unit) =>
            value.HasValue ? $"{value.Value:0.00} {unit}" : "--";

        // 根据报警、限位和运行状态选择状态文字颜色。
        private static Color GetStatusColor(AxisSnapshot snapshot)
        {
            if (snapshot.HasAlarm) return Color.FromArgb(220, 38, 38);
            if (snapshot.PositiveLimit || snapshot.NegativeLimit) return Color.FromArgb(234, 88, 12);
            return snapshot.StatusText is "就绪" or "运行中"
                ? Color.FromArgb(5, 150, 105)
                : Color.Gray;
        }

        // 页面释放时停止定时器、取消任务并按需释放轴服务。
        private void Manual_Disposed(object? sender, EventArgs e)
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
