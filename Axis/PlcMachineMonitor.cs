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

    // 单个非有限数值失效，其余有效反馈仍可显示。
    private static FeedbackField<double> Number(double value) => double.IsFinite(value)
        ? new(value, true) : FeedbackField<double>.Unavailable("反馈数值无效");
}

// 与 PLC ST_MachineMonitor 完全同序，固定 100 字节；嵌套轴也是 Pack=1。
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct PlcMachineMonitor
{
    public const ushort LayoutVersion = 1;
    public const ushort ByteSize = 100;
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

    internal MachineMonitorSnapshot ToSnapshot(bool includeAxes)
    {
        if (Version != LayoutVersion || Size != ByteSize)
            throw new InvalidOperationException($"监控结构体布局不匹配：版本={Version}，字节数={Size}，要求 {LayoutVersion}/{ByteSize}；请更新配套 PLC。");
        var axes = includeAxes ? new[] { Axis1.ToSnapshot(1), Axis2.ToSnapshot(2), Axis3.ToSnapshot(3), Axis4.ToSnapshot(4) }
            : Array.Empty<MachineAxisSnapshot>();
        var invalidNumber = axes.Any(axis => !axis.ActualPosition.IsAvailable || !axis.Speed.IsAvailable);
        return new MachineMonitorSnapshot
        {
            IsConnected = true,
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
