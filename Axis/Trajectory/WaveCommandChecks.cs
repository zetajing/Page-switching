namespace Page_switching;

// 页面解释按钮状态，服务在执行前重读 PLC 后再检查；两处使用同一份条件。
// 本类只读反馈，不通信、不写命令，也不代替 PLC 运动联锁。
internal static class WaveCommandChecks
{
    internal static List<string> ForUpload(PlcMachineMonitor frame)
    {
        var reasons = new List<string>();
        try { frame.ValidateWaveContract(); }
        catch (InvalidOperationException ex) { reasons.Add(ex.Message); return reasons; }
        if (frame.UploadBusy) reasons.Add("PLC 正在拼接轨迹。");
        if (frame.TrajectoryBusy) reasons.Add("PLC 轨迹任务尚未退出。");
        if (!frame.AxesIdle) reasons.Add("PLC 未确认四轴空闲，请检查轴忙状态和 NC 反馈。");
        return reasons;
    }

    internal static List<string> ForStart(PlcMachineMonitor frame, ushort station, uint readySequence)
    {
        var reasons = ForUpload(frame);
        // 布局或行为版本不符时，不继续解释帧内的轴和点位字段。
        if (frame.Version != PlcMachineMonitor.LayoutVersion || frame.Size != PlcMachineMonitor.ByteSize
            || frame.BehaviorVersion != PlcMachineMonitor.ExpectedBehaviorVersion) return reasons;
        if (readySequence == 0 || !frame.DataReady || frame.ReceivedSequence != readySequence)
            reasons.Add("本次点位表未拼接就绪或接收序号不符，请由本站重新发送。");
        else if (frame.AcceptedSequence == readySequence)
            reasons.Add("本次点位表已启动过，再次运行请重新发送。");
        if (frame.UploadStation != station) reasons.Add("PLC 点位表不属于本站，请由本站重新发送。");
        if (frame.Error) reasons.Add(PlcWaveDiagnostics.DescribeFault(frame.TrajectoryErrorId)
            + " " + PlcWaveDiagnostics.DescribeFirstFault(frame));
        reasons.AddRange(ForPreparation(frame));
        return reasons;
    }

    // 回原过程中轴可以运动，因此这里不要求空闲、已使能或已回原。
    internal static List<string> ForPreparation(PlcMachineMonitor frame)
    {
        var reasons = new List<string>();
        if (!frame.MotionPermit) reasons.Add("PLC 运动许可未满足。");
        if (!frame.ConfigValid) reasons.Add("四轴全局参数无效，请检查 GVL_motion.aConfig[1..4]。");
        var axes = new[] { frame.Axis1, frame.Axis2, frame.Axis3, frame.Axis4 };
        bool hasAlarm = false;
        for (int i = 0; i < axes.Length; i++)
        {
            if (!double.IsFinite(axes[i].ActualPosition)) reasons.Add($"轴 {i + 1} 位置反馈无效。");
            if (!double.IsFinite(axes[i].Speed)) reasons.Add($"轴 {i + 1} 速度反馈无效。");
            if (axes[i].Alarm)
            {
                hasAlarm = true;
                reasons.Add($"轴 {i + 1} 报警，请先处理故障。");
            }
        }
        if (frame.AxisFault && !hasAlarm) reasons.Add("PLC 汇总轴故障或 NC 反馈无效；具体轴请查看运行诊断或 TwinCAT。");
        return reasons;
    }

    internal static void ThrowIfBlocked(List<string> reasons)
    {
        if (reasons.Count > 0) throw new InvalidOperationException(string.Join(Environment.NewLine, reasons));
    }
}
