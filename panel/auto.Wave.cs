namespace Page_switching;

public partial class Auto
{
    private AxisService? _waveService;
    private WaveTrajectoryFile? _waveSignal;
    private PlcMachineMonitor? _waveFrame;
    private bool _waveImporting;
    private bool _waveStopping;
    private readonly System.Windows.Forms.Timer _waveUiTimer = new() { Interval = 200 };

    public Auto(AxisService service) : this()
    {
        _waveService = service;
        _waveUiTimer.Tick += (_, _) => UpdateWaveControls();
        _waveUiTimer.Start();
        Disposed += (_, _) => _waveUiTimer.Dispose(); // 页面释放不停止运动，主窗体负责有序停车。
        UpdateWaveControls();
    }

    private async void ImportWaveButton_Click(object? sender, EventArgs e)
    {
        if (_waveService is null || _waveService.WaveOperationActive || _waveImporting) return;
        using var dialog = new OpenFileDialog
        {
            Title = "导入旧格式的最终 PLC 点位文件",
            Filter = "造波信号 (*.csv;*.txt;*.dat)|*.csv;*.txt;*.dat|所有文件 (*.*)|*.*"
        };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        _waveImporting = true;
        UpdateWaveControls();
        try
        {
            var elapsed = System.Diagnostics.Stopwatch.StartNew();
            var signal = await Task.Run(() => WaveTrajectoryFile.Read(dialog.FileName));
            if (IsDisposed || Disposing) return;
            _waveService.ForgetWaveSignal();
            _waveSignal = signal; // 全部解析成功后才替换，失败保留原文件。
            waveFileLabel.Text = $"文件：{signal.FileName}\r\n{signal.Positions.Length:N0} 点 · 2 ms · 预计 {signal.DurationSeconds:F3} 秒";
            AddLog("导入造波信号：" + signal.FileName + $"，{signal.Positions.Length} 点，四轴共用。");
            AddLog($"文件读取及解析耗时：{elapsed.Elapsed.TotalMilliseconds:F1} ms。");
        }
        catch (Exception ex) { AddLog("导入失败，保留原信号：" + ex.Message); }
        finally { _waveImporting = false; if (!IsDisposed) UpdateWaveControls(); }
    }

    private async void StartWaveButton_Click(object? sender, EventArgs e)
    {
        if (_waveService is null) return;
        // 人工确认只代表提交启动请求，不代表PLC已允许运动；取消不进入启动流程。
        var result = MessageBox.Show(this,
            "确认开始造波吗？\r\n\r\n确认后将按现有流程自动使能、必要时回原，并请求启动造波。\r\n最终是否开始造波，仍由PLC检查运行条件和运动联锁。",
            "开始造波确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        if (result != DialogResult.OK)
        {
            AddLog("已取消开始造波，未发送启动请求。");
            return;
        }
        if (IsDisposed || Disposing) return;
        // 确认后仍由原服务读取最新PLC反馈并检查，不能沿用弹窗前的条件直接启动。
        await RunWaveUiAsync("开始造波（自动使能及必要回原）", () => _waveService.StartWaveAsync());
    }

    private async void StopWaveButton_Click(object? sender, EventArgs e)
    {
        if (_waveService is null || _waveStopping) return;
        _waveStopping = true;
        try { await RunWaveUiAsync("停止造波", () => _waveService.StopWaveAsync()); }
        finally { _waveStopping = false; if (!IsDisposed) UpdateWaveControls(); }
    }

    private async Task RunWaveUiAsync(string operation, Func<Task> action)
    {
        try
        {
            AddLog(operation + "请求。");
            await action();
            AddLog(operation + "：" + _waveService!.WaveStatus.Stage);
        }
        catch (OperationCanceledException) { AddLog(operation + "已取消；以 PLC 停止反馈为准。"); }
        catch (Exception ex) { AddLog(operation + "失败：" + ex.Message); }
        finally { if (!IsDisposed) UpdateWaveControls(); }
    }

    private void UpdateWaveControls()
    {
        if (IsDisposed || _waveService is null) return;
        runtimeMonitoringSummary.ApplyReport(_waveService.RuntimeMonitoringReport);
        bool active = _waveService.WaveOperationActive || _waveStopping;
        bool authorized = _waveService.IsConnected && _waveService.HasControlAuthority;
        var status = _waveService.WaveStatus;
        // 上传只要求有效控制权和空闲轴；使能、回原及运动许可在开始阶段检查。
        string? sendBlockReason = GetWaveSendBlockReason(authorized, active);
        string? startBlockReason = GetWaveStartBlockReason(authorized, active, status.Ready);
        importWaveButton.Enabled = !active && !_waveImporting && _waveFrame is not { TrajectoryBusy: true };
        sendWaveButton.Enabled = sendBlockReason is null;
        startWaveButton.Enabled = startBlockReason is null;
        stopWaveButton.Enabled = authorized && !_waveStopping;
        // 空闲时只提示下一步。开始条件仅在已经发送后才有展示意义。
        string? nextStep = _waveSignal is null ? "请先导入点位文件。"
            : status.Ready ? startBlockReason : sendBlockReason ?? "请发送造波信号。";
        UpdateWaveProgress(status, _waveFrame, active, nextStep);
    }

    private string? GetWaveSendBlockReason(bool authorized, bool active)
    {
        if (!authorized) return "本站未取得有效控制权。";
        if (active) return "正在执行造波操作或停止，请等待结束。";
        if (_waveImporting) return "正在导入文件。";
        if (_waveSignal is null) return "请先导入点位文件。";
        if (_waveFrame is not { } frame) return "等待有效 PLC 反馈。";
        return WaveCommandChecks.ForUpload(frame).FirstOrDefault();
    }

    // 两处状态文字共用阶段；执行点数、百分比和进度条均取同一帧 PLC 反馈。
    private void UpdateWaveProgress(WaveOperationStatus status, PlcMachineMonitor? frame, bool active, string? nextStep)
    {
        string stage = status.Stage;
        string? detail = null;
        double? fraction = null;
        bool plcBusy = frame is { UploadBusy: true } or { TrajectoryBusy: true } or { Running: true } or { WaveState: 3 or 4 };
        bool plcFault = frame is { Error: true } or { WaveFaultCode: > 0 };

        if (_waveImporting) stage = "导入中";
        else if (frame is not { } feedback) stage = status.Error is null ? "等待 PLC 反馈" : status.Stage;
        else if (_waveStopping || feedback.WaveState == 4) stage = "停止中";
        else if (plcFault) stage = "造波故障";
        else if (feedback.Running)
        {
            stage = "造波中";
            if (feedback.CurrentPoint < feedback.PointCount)
            {
                // PLC 点号从 0 开始，界面按本次已经取到的第几个点显示。
                uint current = feedback.CurrentPoint + 1;
                fraction = current / (double)feedback.PointCount;
                detail = $"轨迹进度 {fraction:P1} · {current:N0} / {feedback.PointCount:N0} 点";
            }
        }
        else if (feedback.WaveState == 3) stage = "已暂停";
        else if (feedback.TrajectoryBusy) stage = "准备中";
        else if (feedback.UploadBusy)
        {
            stage = "拼接中";
            if (feedback.PointCount > 0 && feedback.LoadedCount <= feedback.PointCount)
            {
                fraction = feedback.LoadedCount / (double)feedback.PointCount;
                detail = $"拼接进度 {fraction:P1} · {feedback.LoadedCount:N0} / {feedback.PointCount:N0} 点";
            }
        }
        else if (active && status.IsSending)
        {
            stage = "发送中";
            fraction = status.Fraction;
            detail = $"发送进度 {fraction:P1}";
        }
        else if (active && !status.Active) stage = "处理中";
        else if (status.Ready) stage = startWaveButton.Enabled ? "待开始" : "等待就绪";
        else if (_waveSignal is null && status.Error is null && !active && feedback.WaveState is 5 or 6)
            stage = FormatWaveState(feedback.WaveState);

        // 原始失败和诊断字段由操作日志及运行诊断保留，进度区只提示当前情况。
        if (frame is { Error: true } fault) detail = PlcWaveDiagnostics.DescribeFault(fault.TrajectoryErrorId);
        else if (frame is { WaveFaultCode: > 0 } axisFault) detail = $"PLC 故障码 {axisFault.WaveFaultCode}，请查看运行诊断。";
        else if (!plcBusy && status.Error is { Length: > 0 } error) detail = error.Split('\n', 2)[0].TrimEnd('\r');
        else if (!active && !plcBusy && !_waveImporting) detail = nextStep;

        waveStageLabel.Text = _runStateLabel.Text = stage;
        _runStateLabel.ForeColor = plcFault || (!plcBusy && status.Error is not null) ? UiPalette.Danger
            : frame is null ? UiPalette.Muted : active || plcBusy ? UiPalette.Primary : UiPalette.Text;
        waveStageLabel.AccessibleDescription = status.Error;
        waveProgressBar.Value = (int)Math.Clamp(Math.Round((fraction ?? 0) * waveProgressBar.Maximum), 0, waveProgressBar.Maximum);
        waveProgressBar.Visible = fraction.HasValue;
        waveDetailLabel.Text = detail ?? "";
    }

    // 任一条件不满足仍禁用开始，页面只展示首个原因。
    private string? GetWaveStartBlockReason(bool authorized, bool active, bool ready)
    {
        if (!authorized) return "本站未取得有效控制权。";
        if (active) return "造波操作尚未结束。";
        if (_waveImporting) return "正在导入文件。";
        if (!ready) return "请先发送造波信号。";
        if (_waveFrame is not { } frame) return "等待有效 PLC 反馈。";
        return _waveService!.GetWaveStartBlockReasons(frame).FirstOrDefault();
    }
}
