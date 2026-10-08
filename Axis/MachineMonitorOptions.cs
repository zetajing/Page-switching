using System.Configuration;

namespace Page_switching;

public sealed class MachineMonitorOptions
{
    public string AmsNetId { get; init; } = string.Empty;
    public int AdsPort { get; init; } = 851;
    public int ConnectTimeoutMilliseconds { get; init; } = 10000;
    public int OperationTimeoutMilliseconds { get; init; } = 1000;
    public int StaleAfterMilliseconds { get; init; } = 3000;
    public int ReconnectIntervalMilliseconds { get; init; } = 3000;
    public string ControlOwnerSymbol { get; init; } = "GVL_HMI.nControlOwner";
    public string ControlModeSymbol { get; init; } = "GVL_HMI.nControlMode";
    public string WaveStateSymbol { get; init; } = "GVL_HMI.nWaveState";
    public string WaveFaultCodeSymbol { get; init; } = "GVL_HMI.udiWaveFaultCode";
    public string HeartbeatSymbol { get; init; } = "GVL_HMI.udiHeartbeat";
    public IReadOnlyList<AxisSymbolMap> AxisSymbols { get; init; } = Array.Empty<AxisSymbolMap>();

    // 只复用连接目标和既有轴反馈地址；监控超时独立于手动客户端。
    public static MachineMonitorOptions FromConfiguration(AxisServiceOptions axisOptions)
    {
        ArgumentNullException.ThrowIfNull(axisOptions);
        var settings = ConfigurationManager.AppSettings;
        return new MachineMonitorOptions
        {
            AmsNetId = axisOptions.AmsNetId,
            AdsPort = axisOptions.AdsPort,
            ConnectTimeoutMilliseconds = axisOptions.ConnectTimeoutMilliseconds,
            AxisSymbols = Array.AsReadOnly(axisOptions.AxisSymbols.ToArray()),
            ControlOwnerSymbol = settings["AdsMonitorControlOwner"] ?? "GVL_HMI.nControlOwner",
            ControlModeSymbol = settings["AdsMonitorControlMode"] ?? "GVL_HMI.nControlMode",
            WaveStateSymbol = settings["AdsMonitorWaveState"] ?? "GVL_HMI.nWaveState",
            WaveFaultCodeSymbol = settings["AdsMonitorWaveFaultCode"] ?? "GVL_HMI.udiWaveFaultCode",
            HeartbeatSymbol = settings["AdsMonitorHeartbeat"] ?? "GVL_HMI.udiHeartbeat"
        };
    }
}
