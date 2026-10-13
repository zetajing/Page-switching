namespace Page_switching;

// 这些值只表示软件本站身份，不是尚未定义的 PLC Owner 编码或授权状态。
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

    internal static string Format(ControlStation station) => station switch
    {
        ControlStation.WaveControlRoom => "造波控制室",
        ControlStation.TowingLocal => "拖车本地",
        ControlStation.RemoteControlRoom => "远程操作室",
        _ => "未配置本站身份"
    };
}
