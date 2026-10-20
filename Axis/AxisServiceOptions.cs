using System.Collections.Specialized;

namespace Page_switching;

// 手动轴命令的符号映射及启动配置，和服务实现分开阅读。
public sealed class AxisSymbolMap
{
    public string EnableCommand { get; init; } = string.Empty;
    public string ResetAlarmCommand { get; init; } = string.Empty;
    public string HomeCommand { get; init; } = string.Empty;
    public string StopCommand { get; init; } = string.Empty;
    public string JogSpeedCommand { get; init; } = string.Empty;
    public string JogPositiveCommand { get; init; } = string.Empty;
    public string JogNegativeCommand { get; init; } = string.Empty;
}

public sealed class AxisServiceOptions
{
    public string AmsNetId { get; init; } = string.Empty;
    public int AdsPort { get; init; } = 851;
    public int ConnectTimeoutMilliseconds { get; init; } = 10000;
    public int OperationTimeoutMilliseconds { get; init; } = 5000;
    public int RefreshIntervalMilliseconds { get; init; } = 100;
    public ushort ControlStationCode { get; init; } // 0 未配置；1 中心；2 本地；3 远程。
    public string SnapshotSymbol { get; init; } = "GVL_HMI.stMonitor"; // 与机器监控共用反馈结构体地址。
    public string Unit { get; init; } = "°";
    public double MinimumPosition { get; init; } = -20;
    public double MaximumPosition { get; init; } = 20;
    public IReadOnlyList<AxisSymbolMap> AxisSymbols { get; init; } = Array.Empty<AxisSymbolMap>();

    // 合并用户保存的 ADS 连接参数；四根轴的变量地址仍来自 App.config。
    public static AxisServiceOptions FromConfiguration(NameValueCollection? settings = null)
    {
        settings ??= AdsConnectionSettings.GetSettings();
        var stationCode = ControlStationIdentity.GetPlcCode(ControlStationIdentity.Parse(settings["ControlStationIdentity"]));
        // 反馈统一读取监控结构体；命令必须进入本站邮箱，未配置身份时禁止写命令。
        string Command(string key) => stationCode == 0 ? "" : (settings[key] ?? "")
            .Replace("{station}", stationCode.ToString(System.Globalization.CultureInfo.InvariantCulture));
        var symbols = new AxisSymbolMap[4];
        for (var axisNumber = 1; axisNumber <= 4; axisNumber++)
        {
            symbols[axisNumber - 1] = new AxisSymbolMap
            {
                EnableCommand = Command($"AdsAxis{axisNumber}EnableCommand"),
                ResetAlarmCommand = Command($"AdsAxis{axisNumber}ResetAlarmCommand"),
                HomeCommand = Command($"AdsAxis{axisNumber}HomeCommand"),
                StopCommand = Command($"AdsAxis{axisNumber}StopCommand"),
                JogSpeedCommand = Command($"AdsAxis{axisNumber}JogSpeedCommand"),
                JogPositiveCommand = Command($"AdsAxis{axisNumber}JogPositiveCommand"),
                JogNegativeCommand = Command($"AdsAxis{axisNumber}JogNegativeCommand")
            };
        }

        return new AxisServiceOptions
        {
            ControlStationCode = stationCode,
            SnapshotSymbol = settings["AdsMonitorSnapshot"] ?? "GVL_HMI.stMonitor",
            AmsNetId = settings["AdsAmsNetId"] ?? "",
            AdsPort = Convert.ToInt32(settings["AdsPort"] ?? "851"),
            ConnectTimeoutMilliseconds = Convert.ToInt32(settings["AdsConnectTimeoutMs"] ?? "10000"),
            OperationTimeoutMilliseconds = Convert.ToInt32(settings["AdsOperationTimeoutMs"] ?? "5000"),
            RefreshIntervalMilliseconds = Convert.ToInt32(settings["AdsRefreshIntervalMs"] ?? "100"),
            Unit = settings["AxisUnit"] ?? "°",
            MinimumPosition = Convert.ToDouble(settings["AxisMinimum"] ?? "-20",
                System.Globalization.CultureInfo.InvariantCulture),
            MaximumPosition = Convert.ToDouble(settings["AxisMaximum"] ?? "20",
                System.Globalization.CultureInfo.InvariantCulture),
            AxisSymbols = symbols
        };
    }
}
