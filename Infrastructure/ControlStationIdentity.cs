namespace Page_switching;

// 软件本站身份由 GetPlcCode 显式映射到新 PLC 三站协议；身份本身不是授权状态。
internal enum ControlStation
{
    Unconfigured,
    WaveControlRoom,
    TowingLocal,
    RemoteControlRoom
}

internal static class ControlStationIdentity
{
    internal const string SettingKey = "ControlStationIdentity";
    internal static ControlStation Current => Parse(AdsConnectionSettings.Read(SettingKey));

    // 未配置或未知值不推定为任何控制端，必须由操作人员明确选择。
    internal static ControlStation Parse(string? value) => value?.Trim() switch
    {
        "WaveControlRoom" => ControlStation.WaveControlRoom,
        "TowingLocal" => ControlStation.TowingLocal,
        "RemoteControlRoom" => ControlStation.RemoteControlRoom,
        _ => ControlStation.Unconfigured
    };

    internal static string ToSettingValue(ControlStation station) => station switch
    {
        ControlStation.Unconfigured => "",
        ControlStation.WaveControlRoom => "WaveControlRoom",
        ControlStation.TowingLocal => "TowingLocal",
        ControlStation.RemoteControlRoom => "RemoteControlRoom",
        _ => throw new ArgumentOutOfRangeException(nameof(station))
    };

    // 与新 PLC GVL_control.aStation[1..3] 的独立站点邮箱一致。
    internal static ushort GetPlcCode(ControlStation station) => station switch
    {
        ControlStation.WaveControlRoom => 1,
        ControlStation.TowingLocal => 2,
        ControlStation.RemoteControlRoom => 3,
        _ => 0
    };

    internal static string Format(ControlStation station) => station switch
    {
        ControlStation.WaveControlRoom => "造波控制室",
        ControlStation.TowingLocal => "拖车本地",
        ControlStation.RemoteControlRoom => "远程操作室",
        _ => "未配置本站身份"
    };
}
