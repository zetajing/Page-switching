using System.Globalization;

namespace Page_switching;

public enum MachineMonitorQuality
{
    Disconnected,
    Unavailable,
    Good,
    Partial,
    WaitingHeartbeat,
    Stale,
    HeartbeatStalled
}

// 无效字段保留原因；调用方必须检查可用性，不能把缺少的 BOOL 当成 false。
public sealed record FeedbackField<T>(T? Value, bool IsAvailable, string? ErrorMessage = null)
    where T : struct
{
    public static FeedbackField<T> Unavailable(string message) => new(null, false, message);

    public string ToDisplayText(Func<T, string>? formatter = null)
    {
        if (!IsAvailable || !Value.HasValue) return "--";
        if (formatter is not null) return formatter(Value.Value);
        return Value.Value is IFormattable number
            ? number.ToString(null, CultureInfo.InvariantCulture)
            : Value.Value.ToString() ?? "--";
    }

    internal FeedbackField<T> WithoutAvailability(string reason) =>
        this with { IsAvailable = false, ErrorMessage = reason };
}

public sealed record MachineAxisSnapshot
{
    public int AxisNumber { get; init; }
    public FeedbackField<double> ActualPosition { get; init; } = FeedbackField<double>.Unavailable("未读取");
    public FeedbackField<double> Speed { get; init; } = FeedbackField<double>.Unavailable("未读取");
    public FeedbackField<bool> IsHomed { get; init; } = FeedbackField<bool>.Unavailable("未读取");
    public FeedbackField<bool> HasAlarm { get; init; } = FeedbackField<bool>.Unavailable("未读取");
    public FeedbackField<bool> PositiveLimit { get; init; } = FeedbackField<bool>.Unavailable("未读取");
    public FeedbackField<bool> NegativeLimit { get; init; } = FeedbackField<bool>.Unavailable("未读取");
    public FeedbackField<bool> OriginSignal { get; init; } = FeedbackField<bool>.Unavailable("未读取");

    internal MachineAxisSnapshot WithoutAvailability(string reason) => this with
    {
        ActualPosition = ActualPosition.WithoutAvailability(reason),
        Speed = Speed.WithoutAvailability(reason),
        IsHomed = IsHomed.WithoutAvailability(reason),
        HasAlarm = HasAlarm.WithoutAvailability(reason),
        PositiveLimit = PositiveLimit.WithoutAvailability(reason),
        NegativeLimit = NegativeLimit.WithoutAvailability(reason),
        OriginSignal = OriginSignal.WithoutAvailability(reason)
    };
}

public sealed record MachineMonitorSnapshot
{
    public bool IsConnected { get; init; }
    public MachineMonitorQuality OverallQuality { get; init; } = MachineMonitorQuality.Disconnected;
    public bool IsLive => IsConnected && Heartbeat.IsAvailable &&
        OverallQuality is MachineMonitorQuality.Good or MachineMonitorQuality.Partial;
    public DateTimeOffset? LastSuccessTimestamp { get; init; }
    public DateTimeOffset? HeartbeatLastChangedAt { get; init; }
    public string? ErrorMessage { get; init; }
    public FeedbackField<ushort> ControlOwner { get; init; } = FeedbackField<ushort>.Unavailable("未读取");
    public FeedbackField<ushort> ControlMode { get; init; } = FeedbackField<ushort>.Unavailable("未读取");
    public FeedbackField<ushort> WaveState { get; init; } = FeedbackField<ushort>.Unavailable("未读取");
    // 与现场类型一致：故障码为 UINT，心跳为 UDINT。
    public FeedbackField<ushort> WaveFaultCode { get; init; } = FeedbackField<ushort>.Unavailable("未读取");
    public FeedbackField<uint> Heartbeat { get; init; } = FeedbackField<uint>.Unavailable("未读取");
    public IReadOnlyList<MachineAxisSnapshot> Axes { get; init; } = Array.Empty<MachineAxisSnapshot>();

    public string QualityText => OverallQuality switch
    {
        MachineMonitorQuality.Good => "反馈正常",
        MachineMonitorQuality.Partial => "部分反馈不可用",
        MachineMonitorQuality.WaitingHeartbeat => "等待 PLC 心跳变化",
        MachineMonitorQuality.Stale => "反馈已过期",
        MachineMonitorQuality.HeartbeatStalled => "PLC 心跳停止",
        MachineMonitorQuality.Unavailable => "反馈不可用",
        _ => "监控未连接"
    };

    // 机器状态依赖 PLC 任务心跳；轴读取有自己的质量，不随机器心跳一同失效。
    internal MachineMonitorSnapshot WithoutMachineAvailability(MachineMonitorQuality quality, string reason) => this with
    {
        OverallQuality = quality,
        ErrorMessage = reason,
        ControlOwner = ControlOwner.WithoutAvailability(reason),
        ControlMode = ControlMode.WithoutAvailability(reason),
        WaveState = WaveState.WithoutAvailability(reason),
        WaveFaultCode = WaveFaultCode.WithoutAvailability(reason)
    };

    internal MachineMonitorSnapshot WithoutAvailability(MachineMonitorQuality quality, string reason) =>
        WithoutMachineAvailability(quality, reason) with
    {
        Heartbeat = Heartbeat.WithoutAvailability(reason),
        Axes = Array.AsReadOnly(Axes.Select(axis => axis.WithoutAvailability(reason)).ToArray())
    };
}
