namespace Page_switching
{
    public partial class Manual : UserControl
    {
        private readonly AxisService _axisService;
        private readonly bool _ownsAxisService;
        private readonly System.Windows.Forms.Timer _refreshTimer;
        private readonly CancellationTokenSource _lifetimeCancellation = new();
        private readonly ServoPositionIndicator[] _positionIndicators;
        private readonly AntdUI.Label[] _statusLabels;
        private readonly AntdUI.Label[] _negativeLimitLamps;
        private readonly AntdUI.Label[] _originLamps;
        private readonly AntdUI.Label[] _positiveLimitLamps;
        private bool _refreshInProgress;
        private bool _commandInProgress;
        // 停止可以在普通命令等待时继续受理；计数拦截普通命令，版本防止旧结果覆盖新提示。
        private int _stopRequestsInProgress;
        private long _stopRequestVersion;
        private bool _jogActive;
        private bool _jogPositive;
        private Axis? _jogAxis;
        private long _jogRequestVersion;

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
            // 连接栏空间有限；读取错误显示省略号，悬停可查看完整变量和原因。
            connectionStateLabel.AutoEllipsis = true;
            connectionStateLabel.ShowTooltip = true;

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
                // 手动页只显示数值；显示范围和反馈数值仍沿用原配置。
                indicator.UnitText = string.Empty;
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

        protected override void OnLayout(LayoutEventArgs e)
        {
            // 最小画布已随 Designer 的 DPI 缩放，缩小窗口后可滚动到全部停止按钮。
            if (pagePanel != null && AutoScrollMinSize != pagePanel.MinimumSize)
                AutoScrollMinSize = pagePanel.MinimumSize;
            base.OnLayout(e);
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
            // 停止请求等待 ADS 时不再追加刷新，保留服务现有的串行读写顺序。
            if (_refreshInProgress || _stopRequestsInProgress > 0 || IsDisposed || _lifetimeCancellation.IsCancellationRequested)
            {
                return;
            }

            _refreshInProgress = true;
            var stopRequestVersion = _stopRequestVersion;
            try
            {
                var snapshots = await _axisService.ReadSnapshotAsync(_lifetimeCancellation.Token);
                if (IsDisposed || _lifetimeCancellation.IsCancellationRequested)
                {
                    return;
                }

                UpdateAxisOverview(snapshots);
                UpdateConnectionState();
            }
            catch (OperationCanceledException)
            {
                // 页面关闭或切换时取消刷新属于正常生命周期。
            }
            catch (Exception ex)
            {
                if (IsDisposed)
                {
                    return;
                }
                var message = "刷新失败：" + ex.Message;
                if (CanUpdateCommandStatus(stopRequestVersion) && helperLabel.Text != message)
                    ShowCommandResult(message);
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
                    string.Empty,
                    _axisService.MinimumPosition,
                    _axisService.MaximumPosition,
                    _axisService.IsConnected);
                _statusLabels[index].Text = snapshot.StatusText;
                _statusLabels[index].ForeColor = GetStatusColor(snapshot);
                var connected = _axisService.IsConnected;
                UpdateSignalLamp(_negativeLimitLamps[index], "负限位",
                    connected && snapshot.NegativeLimitAvailable ? snapshot.NegativeLimit : null, UiPalette.Warning);
                UpdateSignalLamp(_originLamps[index], "原点",
                    connected ? snapshot.OriginSignal : null, UiPalette.Success);
                UpdateSignalLamp(_positiveLimitLamps[index], "正限位",
                    connected && snapshot.PositiveLimitAvailable ? snapshot.PositiveLimit : null, UiPalette.Warning);
            }
        }

        // 根据布尔信号更新一个状态灯的文字和颜色。
        private static void UpdateSignalLamp(AntdUI.Label lamp, string caption, bool? active, Color activeColor)
        {
            // 无效、触发和未触发分别赋值，便于检查文字、颜色和实际信号。
            if (!active.HasValue)
            {
                lamp.Text = "○ " + caption + " --";
                lamp.BackColor = UiPalette.Canvas;
                lamp.ForeColor = UiPalette.Muted;
                lamp.AccessibleName = caption + "：无有效数据";
            }
            else if (active.Value)
            {
                lamp.Text = "● " + caption;
                lamp.BackColor = activeColor;
                lamp.ForeColor = Color.White;
                lamp.AccessibleName = caption + "：触发";
            }
            else
            {
                lamp.Text = "○ " + caption;
                lamp.BackColor = UiPalette.Canvas;
                lamp.ForeColor = UiPalette.SecondaryText;
                lamp.AccessibleName = caption + "：未触发";
            }
        }

        // 显示当前选中轴的状态、位置、速度和可用控制按钮。
        private void UpdateSelectedAxisDetails()
        {
            Axis axis = SelectedAxis;
            AxisSnapshot snapshot = axis.Snapshot;
            selectedStatusLabel.Text = snapshot.StatusText;
            selectedStatusLabel.ForeColor = GetStatusColor(snapshot);
            selectedActualLabel.Text = FormatValue(snapshot.ActualPosition);
            selectedSpeedLabel.Text = FormatValue(snapshot.Speed);

            bool canControlAxis = axis.CanControl;
            SetCommandButtonEnabled(jogNegativeButton, canControlAxis);
            SetCommandButtonEnabled(jogPositiveButton, canControlAxis);
            SetCommandButtonEnabled(homeSelectedButton, canControlAxis);
            SetCommandButtonEnabled(stopSelectedButton, canControlAxis);
        }

        // 更新 ADS 连接文字、颜色和所有控制按钮状态。
        private void UpdateConnectionState()
        {
            connectionStateLabel.Text = _axisService.ConnectionStateText;
            connectionStateLabel.ForeColor = _axisService.IsConnected && string.IsNullOrWhiteSpace(_axisService.LastError)
                ? UiPalette.Success
                : UiPalette.Danger;

            var canControlAll = _axisService.CanControlAll;
            SetCommandButtonEnabled(enableAllButton, canControlAll);
            SetCommandButtonEnabled(disableAllButton, canControlAll);
            SetCommandButtonEnabled(resetAlarmButton, canControlAll);
            SetCommandButtonEnabled(homeAllButton, canControlAll);
            SetCommandButtonEnabled(stopAllButton, canControlAll);
            UpdateSelectedAxisDetails();
        }

        // 按钮只跟随服务的操作许可；命令忙状态在事件入口检查，避免整排按钮闪动。
        private static void SetCommandButtonEnabled(AntdUI.Button button, bool canControl)
        {
            if (button.Enabled != canControl)
            {
                button.Enabled = canControl;
            }
        }

        // 切换轴选择后刷新所选轴的详细信息。
        private void AxisSelector_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateSelectedAxisDetails();
        }

        // 点击后将四根轴的使能变量全部写为 true。
        // ADS 写入成功只代表请求已提交，实际使能状态以各轴 PLC 反馈为准。
        private async void EnableAllButton_Click(object? sender, EventArgs e)
        {
            await RunCommandAsync(
                enableAllButton, token => _axisService.EnableAllAsync(token), "全部轴使能请求已发送，请查看各轴实际状态");
        }

        // 点击后将四根轴的使能变量全部写为 false。
        private async void DisableAllButton_Click(object? sender, EventArgs e)
        {
            await RunCommandAsync(
                disableAllButton, token => _axisService.DisableAllAsync(token), "全部轴取消使能请求已发送，请查看各轴实际状态");
        }

        // 点击后触发四根轴的报警复位变量。
        private async void ResetAlarmButton_Click(object? sender, EventArgs e)
        {
            await RunCommandAsync(
                resetAlarmButton, token => _axisService.ResetAlarmsAsync(token), "报警复位请求已发送");
        }

        // 点击后触发四根轴的回零变量。
        private async void HomeAllButton_Click(object? sender, EventArgs e)
        {
            await RunCommandAsync(
                homeAllButton, token => _axisService.HomeAllAsync(token), "全部轴回零请求已发送");
        }

        // 点击后触发四根轴的停止变量。
        private async void StopAllButton_Click(object? sender, EventArgs e)
        {
            await RunStopCommandAsync(
                token => _axisService.StopAllAsync(token), "已请求全部轴停止，正在发送命令……", "全部轴停止命令已发送");
        }

        // 点击后触发当前所选轴的回零变量。
        private async void HomeSelectedButton_Click(object? sender, EventArgs e)
        {
            Axis axis = SelectedAxis;
            await RunCommandAsync(
                homeSelectedButton, token => axis.HomeAsync(token),
                $"轴 {axis.AxisNumber} 回零请求已发送");
        }

        // 点击后触发当前所选轴的停止变量。
        private async void StopSelectedButton_Click(object? sender, EventArgs e)
        {
            Axis axis = SelectedAxis;
            await RunStopCommandAsync(
                token => axis.StopAsync(token),
                $"已请求轴 {axis.AxisNumber} 停止，正在发送命令……", $"轴 {axis.AxisNumber} 停止命令已发送");
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
            if (_jogActive || _commandInProgress || _stopRequestsInProgress > 0 || IsDisposed)
            {
                return;
            }

            // 保存按下时的轴对象，后续切换选择框也只清零这根轴。
            Axis axis = SelectedAxis;
            _jogAxis = axis;
            _jogPositive = positive;
            _jogActive = true;
            // 保存点动开始时的停止序号；旧点动结果不能覆盖之后的停止提示。
            long stopRequestVersion = _stopRequestVersion;
            _jogRequestVersion = stopRequestVersion;
            OperationJournal.Record("手动控制",
                $"请求轴 {axis.AxisNumber} {(positive ? "正向" : "负向")}点动，速度 {jogSpeedInput.Value:0.0}");
            try
            {
                await axis.JogAsync(
                    positive,
                    true,
                    decimal.ToDouble(jogSpeedInput.Value),
                    _lifetimeCancellation.Token);
                if (CanUpdateCommandStatus(stopRequestVersion))
                {
                    ShowCommandResult($"轴 {axis.AxisNumber} {(positive ? "正向" : "负向")}点动请求已发送");
                }
            }
            catch (OperationCanceledException)
            {
                // 页面释放时取消请求，异常不能逃出鼠标事件。
                _jogActive = false;
            }
            catch (Exception ex)
            {
                _jogActive = false;
                if (CanUpdateCommandStatus(stopRequestVersion))
                {
                    ShowCommandResult("点动失败：" + ex.Message);
                }
            }
        }

        // 清除当前轴的点动方向信号。
        private async Task StopActiveJogAsync()
        {
            if (!_jogActive || _jogAxis == null)
            {
                return;
            }

            Axis axis = _jogAxis;
            var positive = _jogPositive;
            _jogActive = false;
            _jogAxis = null;
            // 松开或隐藏页面后的清零仍属于原点动，不能覆盖之后接受的停止请求。
            var stopRequestVersion = _jogRequestVersion;
            try
            {
                await axis.JogAsync(
                    positive,
                    false,
                    decimal.ToDouble(jogSpeedInput.Value),
                    _lifetimeCancellation.Token);
                if (CanUpdateCommandStatus(stopRequestVersion))
                {
                    ShowCommandResult($"轴 {axis.AxisNumber} 停止点动请求已发送");
                }
            }
            catch (OperationCanceledException)
            {
                // 页面释放时取消请求，异常不能逃出切换事件。
            }
            catch (Exception ex)
            {
                if (CanUpdateCommandStatus(stopRequestVersion))
                {
                    ShowCommandResult("停止点动失败：" + ex.Message);
                }
            }
        }

        // 显示命令提交结果，再刷新 PLC 实际状态；提交成功不等于动作已经完成。
        private async Task RunCommandAsync(
            AntdUI.Button button,
            Func<CancellationToken, Task> action,
            string successMessage)
        {
            if (_commandInProgress || _stopRequestsInProgress > 0 || IsDisposed)
            {
                return;
            }

            _commandInProgress = true;
            var stopRequestVersion = _stopRequestVersion;
            // 仅当前按钮显示发送中；停止按钮不进入 Loading，仍可追加停止请求。
            button.Loading = true;
            UpdateConnectionState();
            try
            {
                await action(_lifetimeCancellation.Token);
                if (IsDisposed) return;
                if (CanUpdateCommandStatus(stopRequestVersion))
                {
                    ShowCommandResult(successMessage);
                    await RefreshValuesAsync();
                }
            }
            catch (OperationCanceledException)
            {
                // 页面关闭时取消命令。
            }
            catch (Exception ex)
            {
                AdsDiagnostics.RecordException("手动命令", ex);
                if (CanUpdateCommandStatus(stopRequestVersion))
                {
                    ShowCommandResult("操作失败：" + ex.Message);
                }
            }
            finally
            {
                _commandInProgress = false;
                if (!IsDisposed)
                {
                    button.Loading = false;
                    UpdateConnectionState();
                }
            }
        }

        // 停止不受普通命令忙状态拦截；每次请求均交给轴服务排队，允许选轴停止后追加全停。
        private async Task RunStopCommandAsync(
            Func<CancellationToken, Task> action,
            string requestMessage,
            string successMessage)
        {
            if (IsDisposed)
            {
                return;
            }

            _stopRequestsInProgress++;
            // 每次停止先递增序号，再保存本次序号，避免旧任务覆盖新的停止结果。
            _stopRequestVersion++;
            long stopRequestVersion = _stopRequestVersion;
            UpdateConnectionState();
            ShowCommandResult(requestMessage);
            var commandSent = false;
            try
            {
                await action(_lifetimeCancellation.Token);
                commandSent = true;
                if (CanUpdateCommandStatus(stopRequestVersion))
                {
                    ShowCommandResult(successMessage);
                }
                else
                {
                    OperationJournal.Record("手动控制", successMessage);
                }
            }
            catch (OperationCanceledException)
            {
                if (CanUpdateCommandStatus(stopRequestVersion))
                {
                    ShowCommandResult("停止命令已取消");
                }
                else
                {
                    OperationJournal.Record("手动控制", "停止命令已取消：" + requestMessage);
                }
            }
            catch (Exception ex)
            {
                AdsDiagnostics.RecordException("手动停止", ex);
                var message = "停止命令发送失败：" + ex.Message + "；" + requestMessage;
                if (CanUpdateCommandStatus(stopRequestVersion))
                {
                    ShowCommandResult(message);
                }
                else
                {
                    OperationJournal.Record("手动控制", message);
                }
            }
            finally
            {
                _stopRequestsInProgress--;
                if (!IsDisposed)
                {
                    UpdateConnectionState();
                }
            }

            if (commandSent && CanUpdateCommandStatus(stopRequestVersion))
            {
                await RefreshValuesAsync();
            }
        }

        // 结果明确记录一次，不再由主窗体监听标签文字变化来推断。
        private void ShowCommandResult(string message)
        {
            helperLabel.Text = message;
            OperationJournal.Record("手动控制", message);
        }

        // 停止请求之后完成的旧命令、点动或刷新，不能覆盖最新停止提示。
        private bool CanUpdateCommandStatus(long stopRequestVersion) =>
            !IsDisposed && stopRequestVersion == _stopRequestVersion;

        // 下拉框只负责选择对象，单轴命令不再反复传入轴号。
        private Axis SelectedAxis
        {
            get
            {
                int axisNumber = Math.Max(0, axisSelector.SelectedIndex) + 1;
                return _axisService.GetAxis(axisNumber);
            }
        }

        // 手动页不附加单位文字；没有有效反馈时仍显示“--”。
        private static string FormatValue(double? value) =>
            value.HasValue ? $"{value.Value:0.00}" : "--";

        // 根据报警、限位和运行状态选择状态文字颜色。
        private static Color GetStatusColor(AxisSnapshot snapshot)
        {
            if (snapshot.HasAlarm)
            {
                return UiPalette.Danger;
            }
            if (snapshot.PositiveLimit || snapshot.NegativeLimit)
            {
                return UiPalette.Warning;
            }
            if (snapshot.StatusText == "就绪" || snapshot.StatusText == "运行中")
            {
                return UiPalette.Success;
            }
            return UiPalette.Muted;
        }

        // 页面释放时停止定时器、取消任务并按需释放轴服务。
        private void Manual_Disposed(object? sender, EventArgs e)
        {
            // Designer 或父窗体重复 Dispose 时，不能再次取消已释放的任务对象。
            Disposed -= Manual_Disposed;
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
