using System.Runtime.InteropServices;

namespace Page_switching;

// 与 PLC ST_AxisMonitor 的 pack_mode=1 对应，BOOL 必须按 1 字节封送。
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct PlcAxisMonitor
{
    public double ActualPosition;
    public double Speed;
    [MarshalAs(UnmanagedType.I1)] public bool Homed;
    [MarshalAs(UnmanagedType.I1)] public bool Alarm;
    [MarshalAs(UnmanagedType.I1)] public bool PositiveLimit;
    [MarshalAs(UnmanagedType.I1)] public bool NegativeLimit;
    [MarshalAs(UnmanagedType.I1)] public bool OriginSignal;
    [MarshalAs(UnmanagedType.I1)] public bool Enabled;

    internal MachineAxisSnapshot ToSnapshot(int number) => new()
    {
        AxisNumber = number,
        ActualPosition = Number(ActualPosition),
        Speed = Number(Speed),
        IsHomed = new(Homed, true),
        HasAlarm = new(Alarm, true),
        PositiveLimit = new(PositiveLimit, true),
        NegativeLimit = new(NegativeLimit, true),
        OriginSignal = new(OriginSignal, true)
    };

    // 手动页保留现有状态显示语义，无效数值显示为空，不伪造零位置。
    internal AxisSnapshot ToAxisSnapshot(int number) => new(number)
    {
        ActualPosition = double.IsFinite(ActualPosition) ? ActualPosition : null,
        Speed = double.IsFinite(Speed) ? Speed : null,
        IsEnabled = Enabled,
        IsHomed = Homed,
        HasAlarm = Alarm,
        PositiveLimit = PositiveLimit,
        NegativeLimit = NegativeLimit,
        PositiveLimitAvailable = true,
        NegativeLimitAvailable = true,
        OriginSignal = OriginSignal,
        StatusText = Alarm ? "报警" : PositiveLimit || NegativeLimit ? "限位"
            : !double.IsFinite(ActualPosition) || !double.IsFinite(Speed) ? "反馈数值无效"
            : !Enabled ? "未使能" : !Homed ? "未回零" : "就绪"
    };

    // 单个非有限数值失效，其余有效反馈仍可显示。
    private static FeedbackField<double> Number(double value) => double.IsFinite(value)
        ? new(value, true) : FeedbackField<double>.Unavailable("反馈数值无效");
}

// 与 PLC ST_MachineMonitor 完全同序，固定 164 字节；嵌套轴也是 Pack=1。
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct PlcMachineMonitor
{
    public const ushort LayoutVersion = 4;
    public const ushort ByteSize = 164;
    public const ushort ExpectedBehaviorVersion = 1; // 上位机整表预检，PLC 保留运动联锁。
    public ushort Version;
    public ushort Size;
    public ushort ControlOwner;
    public ushort ControlMode;
    public ushort WaveState;
    public ushort WaveFaultCode;
    public uint Heartbeat;
    public PlcAxisMonitor Axis1;
    public PlcAxisMonitor Axis2;
    public PlcAxisMonitor Axis3;
    public PlcAxisMonitor Axis4;

    public ushort TrajectoryState;
    public uint ReceivedCount;
    public uint LoadedCount;
    public uint PointCount;
    public uint CurrentPoint;
    public uint ReceivedSequence;
    public uint AcceptedSequence;
    public uint TrajectoryErrorId;
    public ushort UploadStation;
    [MarshalAs(UnmanagedType.I1)] public bool UploadBusy;
    [MarshalAs(UnmanagedType.I1)] public bool DataReady;
    [MarshalAs(UnmanagedType.I1)] public bool TrajectoryBusy;
    [MarshalAs(UnmanagedType.I1)] public bool Running;
    [MarshalAs(UnmanagedType.I1)] public bool Done;
    [MarshalAs(UnmanagedType.I1)] public bool Error;
    [MarshalAs(UnmanagedType.I1)] public bool Stopped;
    [MarshalAs(UnmanagedType.I1)] public bool MotionPermit;
    [MarshalAs(UnmanagedType.I1)] public bool ConfigValid;
    [MarshalAs(UnmanagedType.I1)] public bool AxesIdle;
    [MarshalAs(UnmanagedType.I1)] public bool AxisFault;
    [MarshalAs(UnmanagedType.I1)] public bool AxesStopped;

    // 追加字段对应 PLC 布局版本4；首次故障与当前停车阶段分别显示。
    public uint ProgramVersion;
    public ushort BehaviorVersion;
    public ushort FaultAxis;
    public ushort FaultSource;
    public ushort FaultState;
    public uint CleanupErrorId;

    // 读取状态只检查布局；下发和启动另外检查行为版本，停车仍可进入原退出流程。
    internal readonly void ValidateWaveContract()
    {
        ValidateLayout();
        if (BehaviorVersion != ExpectedBehaviorVersion)
            throw new InvalidOperationException($"点位接口版本不匹配：PLC={BehaviorVersion}，要求={ExpectedBehaviorVersion}（上位机整表预检）；请更新配套程序后重新下发。");
    }

    // 两个页面共用同一布局验证；旧版帧不能被解释成新版轴数据。
    internal readonly void ValidateLayout()
    {
        if (Version != LayoutVersion || Size != ByteSize)
            throw new InvalidOperationException($"监控结构体布局不匹配：版本={Version}，字节数={Size}，要求 {LayoutVersion}/{ByteSize}；请更新配套 PLC。");
    }

    internal IReadOnlyList<AxisSnapshot> ToAxisSnapshots()
    {
        ValidateLayout();
        return new[] { Axis1.ToAxisSnapshot(1), Axis2.ToAxisSnapshot(2), Axis3.ToAxisSnapshot(3), Axis4.ToAxisSnapshot(4) };
    }

    internal MachineMonitorSnapshot ToSnapshot(bool includeAxes)
    {
        ValidateLayout();
        var axes = includeAxes ? new[] { Axis1.ToSnapshot(1), Axis2.ToSnapshot(2), Axis3.ToSnapshot(3), Axis4.ToSnapshot(4) }
            : Array.Empty<MachineAxisSnapshot>();
        var invalidNumber = axes.Any(axis => !axis.ActualPosition.IsAvailable || !axis.Speed.IsAvailable);
        return new MachineMonitorSnapshot
        {
            IsConnected = true,
            Frame = this,
            ControlOwner = new(ControlOwner, true),
            ControlMode = new(ControlMode, true),
            WaveState = new(WaveState, true),
            WaveFaultCode = new(WaveFaultCode, true),
            Heartbeat = new(Heartbeat, true),
            Axes = Array.AsReadOnly(axes),
            OverallQuality = invalidNumber ? MachineMonitorQuality.Partial : MachineMonitorQuality.Good,
            ErrorMessage = invalidNumber ? "轴位置或速度包含非有限数值" : null
        };
    }
}
