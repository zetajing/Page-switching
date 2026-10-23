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
            var signal = await Task.Run(() => WaveTrajectoryFile.Read(dialog.FileName));
            if (IsDisposed || Disposing) return;
            _waveService.ForgetWaveSignal();
            _waveSignal = signal; // 全部解析成功后才替换，失败保留原文件。
            waveFileLabel.Text = $"文件：{signal.FileName}\r\n点数：{signal.Positions.Length:N0}　周期：2 ms\r\n预计时长：{signal.DurationSeconds:F3} 秒\r\n按最终 PLC 点位发送，四轴共用；不使用主动吸收列。";
            AddLog("导入造波信号：" + signal.FileName + $"，{signal.Positions.Length} 点，四轴共用。");
        }
        catch (Exception ex) { AddLog("导入失败，保留原信号：" + ex.Message); }
        finally { _waveImporting = false; if (!IsDisposed) UpdateWaveControls(); }
    }

    private async void StartWaveButton_Click(object? sender, EventArgs e)
    {
        if (_waveService is null) return;
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
        bool active = _waveService.WaveOperationActive || _waveStopping;
        bool authorized = _waveService.IsConnected && _waveService.HasControlAuthority;
        // 上传只要求有效控制权和空闲轴；使能、回原及运动许可在开始阶段检查。
        string? sendBlockReason = GetWaveSendBlockReason(authorized);
        importWaveButton.Enabled = !active && !_waveImporting && _waveFrame is not { TrajectoryBusy: true };
        sendWaveButton.Enabled = sendBlockReason is null && !active && !_waveImporting && _waveSignal is not null;
        // 启用条件与提示共用同一份判断，避免按钮变灰却看不到缺少哪项条件。
        var startBlockReasons = GetWaveStartBlockReasons(authorized, active);
        startWaveButton.Enabled = startBlockReasons.Count == 0;
        stopWaveButton.Enabled = authorized && !_waveStopping;
        var status = _waveService.WaveStatus;
        waveStageLabel.Text = _waveImporting ? "正在导入文件…" : status.Stage;
        waveProgressBar.Value = (int)Math.Clamp(status.Fraction * 1000, 0, 1000);
        waveStageLabel.AccessibleDescription = status.Error;
        string detail = BuildWaveDetail(active, sendBlockReason, startBlockReasons);
        // 完整文字只更新一次；相同反馈不反复清除、追加，避免定时刷新闪动。
        if (waveDetailLabel.Text != detail) waveDetailLabel.Text = detail;
    }

    private string? GetWaveSendBlockReason(bool authorized)
    {
        if (!authorized) return "本站未取得有效控制权。";
        if (_waveFrame is not { } frame) return "等待有效 PLC 反馈。";
        if (frame.UploadBusy) return "PLC 正在拼接轨迹。";
        if (frame.TrajectoryBusy) return "PLC 轨迹任务尚未退出。";
        if (!frame.AxesIdle) return "PLC 未确认四轴空闲，请检查轴忙状态和 NC 反馈。";
        return null;
    }

    // 只组装本次反馈文字，与按钮启用判断共用原因；不产生通信和命令。
    private string BuildWaveDetail(bool active, string? sendBlockReason, List<string> startBlockReasons)
    {
        string detail;
        if (_waveFrame is { } feedback)
            detail = $"PLC 接收：{feedback.ReceivedCount:N0} / {feedback.PointCount:N0}\r\n"
                + $"PLC 拼接：{feedback.LoadedCount:N0} / {feedback.PointCount:N0}\r\n"
                + $"执行点号：{feedback.CurrentPoint:N0}　轨迹状态：{feedback.TrajectoryState}（{PlcWaveDiagnostics.DescribeState(feedback.TrajectoryState)}）\r\n"
                + (feedback.Error ? PlcWaveDiagnostics.DescribeFault(feedback.TrajectoryErrorId) : "发送完成不会自动开始运动。")
                + (!active && _waveSignal is not null && sendBlockReason is not null
                    ? "\r\n暂不能发送：" + sendBlockReason : "");
        else detail = "等待有效 PLC 反馈；请确认配套结构体版本已部署。";
        if (_waveService?.WaveStatus.Error is { Length: > 0 } error)
            detail += "\r\n最近操作失败：" + error;
        if (startBlockReasons.Count > 0)
            detail += "\r\n暂不能开始：\r\n" + string.Join("\r\n", startBlockReasons);
        return detail;
    }

    // 保留原有启动门槛；这里只说明缺项，不发送命令、不自动复位或放宽 PLC 联锁。
    private List<string> GetWaveStartBlockReasons(bool authorized, bool active)
    {
        var reasons = new List<string>();
        if (!authorized) reasons.Add("• 本站未取得有效控制权。");
        if (active) reasons.Add("• 正在执行造波操作或停止，请等待结束。");
        if (_waveImporting) reasons.Add("• 正在导入文件。");
        if (_waveService?.WaveStatus.Ready != true)
            reasons.Add("• 本次尚未完成轨迹发送；重启或更换文件后需重新发送。");
        if (_waveFrame is { } frame)
        {
            if (!frame.DataReady) reasons.Add("• PLC 点位表未拼接就绪。");
            if (!frame.MotionPermit) reasons.Add("• PLC 运动许可未满足。");
            if (!frame.ConfigValid) reasons.Add("• 四轴全局参数无效，请检查 GVL_motion.aConfig[1..4]。");
            if (frame.AxisFault) reasons.Add("• 存在轴故障或 NC 反馈无效。");
            if (!frame.AxesIdle) reasons.Add("• PLC 未确认四轴空闲。");
            if (frame.UploadStation != _waveService?.ControlStationCode)
                reasons.Add("• PLC 点位表不属于本站，请由本站重新发送。");
        }
        else reasons.Add("• 等待有效 PLC 反馈。");
        return reasons;
    }
}
