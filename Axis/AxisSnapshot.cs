namespace Page_switching;

public sealed class AxisSnapshot
{
    // 创建一根轴的状态数据，默认显示为未连接。
    public AxisSnapshot(int axisNumber)
    {
        AxisNumber = axisNumber;
        StatusText = "未连接";
    }

    public int AxisNumber { get; }
    public double? ActualPosition { get; internal set; }
    public double? Speed { get; internal set; }
    public bool IsEnabled { get; internal set; }
    public bool IsHomed { get; internal set; }
    public bool HasAlarm { get; internal set; }
    public bool PositiveLimit { get; internal set; }
    public bool NegativeLimit { get; internal set; }
    public bool PositiveLimitAvailable { get; internal set; }
    public bool NegativeLimitAvailable { get; internal set; }
    public bool? OriginSignal { get; internal set; }
    public string StatusText { get; internal set; }
}

