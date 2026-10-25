namespace Page_switching;

// 同一 AxisService 的统计挂接入口；统计服务使用独立只读采样，不另建运动控制服务。
public sealed partial class AxisService
{
    private AxisRuntimeMonitoringService? _runtimeMonitoring;
    internal AxisMonitoringReport? RuntimeMonitoringReport => _runtimeMonitoring?.Latest;

    private void StartRuntimeMonitoring(uint sequence, uint heartbeat)
    {
        try
        {
            _runtimeMonitoring ??= new AxisRuntimeMonitoringService(_options);
            _runtimeMonitoring.Start(sequence, ControlStationCode, heartbeat);
        }
        catch (Exception ex)
        {
            try { OperationJournal.Record("运行超限统计", "统计准备失败，不改变运动流程：" + ex.Message); }
            catch (Exception logError) { System.Diagnostics.Debug.WriteLine(logError); }
        }
    }

    // 统计任务独立结束，调用方不等待磁盘或采样网络请求，不能拖住停车。
    private void EndRuntimeMonitoring(string reason)
    {
        try { _runtimeMonitoring?.Complete(reason); }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine("统计结束失败：" + ex); }
    }

    internal Task ShutdownRuntimeMonitoringAsync() => _runtimeMonitoring?.StopAsync() ?? Task.CompletedTask;
}
