using System.Reflection;
using System.Text;
using InduLink.Abstractions;
using InduLink.Protocols.Ads;

namespace Page_switching;

// 一次只读诊断；参数与状态分次读取，不宣称跨 ADS 请求原子一致。
internal sealed record MachineDiagnosticsSnapshot(PlcMachineMonitor Frame,
    AxisTrajectoryLimits[]? Limits, DateTimeOffset ReadAt, string? ParameterError)
{
    internal static string SoftwareVersion => typeof(AxisService).Assembly
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "未知";

    internal string Format(string? operationError)
    {
        var text = new StringBuilder();
        text.AppendLine($"读取时间：{ReadAt:yyyy-MM-dd HH:mm:ss zzz}（手动读取快照）");
        text.AppendLine($"上位机版本：{SoftwareVersion}");
        text.AppendLine($"PLC 程序发布号：{Frame.ProgramVersion}；监控布局：{Frame.Version}/{Frame.Size}");
        text.AppendLine($"接口行为版本：PLC={Frame.BehaviorVersion}，上位机要求={PlcMachineMonitor.ExpectedBehaviorVersion}（上位机整表预检）");
        text.AppendLine(PlcWaveDiagnostics.DescribeFrame(Frame));
        var axes = new[] { Frame.Axis1, Frame.Axis2, Frame.Axis3, Frame.Axis4 };
        for (int axis = 0; axis < axes.Length; axis++)
        {
            var state = axes[axis];
            text.AppendLine(FormattableString.Invariant($"轴 {axis + 1}：实际位置={state.ActualPosition:G17}，实际速度={state.Speed:G17}，使能={state.Enabled}，回原={state.Homed}，报警={state.Alarm}，正限位={state.PositiveLimit}，负限位={state.NegativeLimit}"));
        }
        text.AppendLine("轴单位：PLC/NC 原始单位，尚未确认与角度或丝杆位移的换算。");
        text.AppendLine("参数来源：GVL_motion.aConfig[1..4]；由 FB_AxisConfig 填写，本窗口只读。");
        if (ParameterError is not null) text.AppendLine("参数读取失败：" + ParameterError);
        if (!string.IsNullOrWhiteSpace(operationError)) text.AppendLine("最近一次造波操作失败记录：" + operationError);
        text.AppendLine("静态快照不能确认心跳持续变化；运行状态以主页面新鲜反馈为准。");
        return text.ToString();
    }
}

public sealed partial class AxisService
{
    // 查看诊断不要求控制权，只读真实反馈和参数；与现有读写共用串行门闩。
    internal async Task<MachineDiagnosticsSnapshot> ReadDiagnosticsAsync(CancellationToken token)
    {
        ThrowIfDisposed();
        await _operationGate.WaitAsync(token).ConfigureAwait(false);
        try
        {
            token.ThrowIfCancellationRequested();
            var client = GetConnectedClient() ?? throw new InvalidOperationException("控制 ADS 未连接，请等待主页面连接成功。");
            var frame = await client.ReadAnyAsync<PlcMachineMonitor>(
                _options.SnapshotSymbol, cancellationToken: token).ConfigureAwait(false);
            frame.ValidateLayout();
            AxisTrajectoryLimits[]? limits = null;
            string? parameterError = null;
            try { limits = await ReadAxisLimitsAsync(client, token).ConfigureAwait(false); }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                parameterError = AdsDiagnostics.DescribeException(ex);
            }
            return new(frame, limits, DateTimeOffset.Now, parameterError);
        }
        finally { _operationGate.Release(); }
    }

    // 诊断与点位预检共用同一读参方法，不在上位机另设默认限值。
    private static async Task<AxisTrajectoryLimits[]> ReadAxisLimitsAsync(AdsClient client, CancellationToken token)
    {
        string[] fields = ["lrMinPosition", "lrMaxPosition", "lrMaxVelocity", "lrMaxAcceleration",
            "lrStopDeceleration", "lrStartTolerance", "lrInitVelocity"];
        var requests = new List<ReadRequest>();
        for (int axis = 1; axis <= 4; axis++)
            foreach (string field in fields)
                requests.Add(new(client.DeviceId, $"GVL_motion.aConfig[{axis}].{field}", DataType.Double));
        try
        {
            var values = (await client.ReadManyAsync(requests, token).ConfigureAwait(false)).Values;
            if (values.Count != requests.Count)
                throw new InvalidDataException($"轴参数读取数量不符：收到 {values.Count}，需要 {requests.Count}。");
            for (int index = 0; index < values.Count; index++)
                if (values[index].Quality != QualityStatus.Good || values[index].Value is not double value || !double.IsFinite(value))
                    throw new InvalidDataException($"轴参数 {requests[index].Address} 反馈无效。");
            var limits = new AxisTrajectoryLimits[4];
            for (int axis = 0; axis < 4; axis++)
            {
                int offset = axis * fields.Length;
                limits[axis] = new((double)values[offset].Value!, (double)values[offset + 1].Value!,
                    (double)values[offset + 2].Value!, (double)values[offset + 3].Value!,
                    (double)values[offset + 4].Value!, (double)values[offset + 5].Value!, (double)values[offset + 6].Value!);
            }
            return limits;
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            throw new InvalidOperationException($"读取 GVL_motion.aConfig[1..4] 失败：{AdsDiagnostics.DescribeException(ex)}", ex);
        }
    }
}
