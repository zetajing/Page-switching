using System.Collections.Specialized;

namespace Page_switching;

public sealed class MachineMonitorOptions
{
    public string AmsNetId { get; init; } = string.Empty;
    public int AdsPort { get; init; } = 851;
    public int ConnectTimeoutMilliseconds { get; init; } = 10000;
    // 单次 ADS 请求与整轮读取分开计时，四轴监控每轮读取一个结构体。
    public int OperationTimeoutMilliseconds { get; init; } = 1000;
    public int RefreshTimeoutMilliseconds { get; init; } = 3000;
    public int StaleAfterMilliseconds { get; init; } = 3000;
    public int ReconnectIntervalMilliseconds { get; init; } = 3000;
    // 四轴与全局反馈共用一个只读结构体；命令地址仍由 AxisServiceOptions 管理。
    public string SnapshotSymbol { get; init; } = "GVL_HMI.stMonitor";

    // 只复用连接目标；监控超时独立于手动客户端。
    public static MachineMonitorOptions FromConfiguration(AxisServiceOptions axisOptions, NameValueCollection? settings = null)
    {
        ArgumentNullException.ThrowIfNull(axisOptions);
        settings ??= AdsConnectionSettings.GetSettings();
        var refreshTimeout = int.TryParse(settings["AdsMonitorRefreshTimeoutMs"], out var milliseconds) && milliseconds > 0
            ? milliseconds : 3000;
        return new MachineMonitorOptions
        {
            AmsNetId = axisOptions.AmsNetId,
            AdsPort = axisOptions.AdsPort,
            ConnectTimeoutMilliseconds = axisOptions.ConnectTimeoutMilliseconds,
            RefreshTimeoutMilliseconds = refreshTimeout,
            SnapshotSymbol = settings["AdsMonitorSnapshot"] ?? "GVL_HMI.stMonitor"
        };
    }
}
