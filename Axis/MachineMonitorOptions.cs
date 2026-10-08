using System.Collections.Specialized;

namespace Page_switching;

public sealed class MachineMonitorOptions
{
    public string AmsNetId { get; init; } = string.Empty;
    public int AdsPort { get; init; } = 851;
    public int ConnectTimeoutMilliseconds { get; init; } = 10000;
    // 单次 ADS 请求与整轮读取分开计时，四轴监控共有 33 个变量。
    public int OperationTimeoutMilliseconds { get; init; } = 1000;
    public int RefreshTimeoutMilliseconds { get; init; } = 3000;
    public int StaleAfterMilliseconds { get; init; } = 3000;
    public int ReconnectIntervalMilliseconds { get; init; } = 3000;
    public string ControlOwnerSymbol { get; init; } = "GVL_HMI.nControlOwner";
    public string ControlModeSymbol { get; init; } = "GVL_HMI.nControlMode";
    public string WaveStateSymbol { get; init; } = "GVL_HMI.nWaveState";
    public string WaveFaultCodeSymbol { get; init; } = "GVL_HMI.udiWaveFaultCode";
    public string HeartbeatSymbol { get; init; } = "GVL_HMI.udiHeartbeat";
    public IReadOnlyList<AxisSymbolMap> AxisSymbols { get; init; } = Array.Empty<AxisSymbolMap>();

    // 只复用连接目标和既有轴反馈地址；监控超时独立于手动客户端。
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
            AxisSymbols = Array.AsReadOnly(axisOptions.AxisSymbols.ToArray()),
            ControlOwnerSymbol = settings["AdsMonitorControlOwner"] ?? "GVL_HMI.nControlOwner",
            ControlModeSymbol = settings["AdsMonitorControlMode"] ?? "GVL_HMI.nControlMode",
            WaveStateSymbol = settings["AdsMonitorWaveState"] ?? "GVL_HMI.nWaveState",
            WaveFaultCodeSymbol = settings["AdsMonitorWaveFaultCode"] ?? "GVL_HMI.udiWaveFaultCode",
            HeartbeatSymbol = settings["AdsMonitorHeartbeat"] ?? "GVL_HMI.udiHeartbeat"
        };
    }
}
