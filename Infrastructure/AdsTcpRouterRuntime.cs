using InduLink.Protocols.Ads.Router;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net;
using TwinCAT.Ads;
using TwinCAT.Ams;

namespace Page_switching;

internal static class AdsTcpRouterRuntime
{
    public static bool IsEnabled =>
        bool.TryParse(Read("AdsTcpRouterEnabled"), out var enabled) && enabled;

    // 使用用户保存的 Router 参数，未保存的参数沿用 App.config。
    public static AdsTcpRouterHost Create()
    {
        var values = new Dictionary<string, string?>
        {
            ["AmsRouter:Name"] = Read("AdsTcpRouterName"),
            ["AmsRouter:NetId"] = Read("AdsTcpRouterLocalNetId"),
            ["AmsRouter:TcpPort"] = Read("AdsTcpRouterTcpPort"),
            ["AmsRouter:ChannelPortType"] = "Loopback",
            ["AmsRouter:LoopbackIP"] = Read("AdsTcpRouterLoopbackIp"),
            ["AmsRouter:LoopbackPort"] = Read("AdsTcpRouterLoopbackPort"),
            ["AmsRouter:RemoteConnections:0:Name"] = Read("AdsTcpRouterRemoteName"),
            ["AmsRouter:RemoteConnections:0:Address"] = Read("AdsTcpRouterRemoteAddress"),
            ["AmsRouter:RemoteConnections:0:NetId"] = Read("AdsTcpRouterRemoteNetId"),
            ["AmsRouter:RemoteConnections:0:Type"] = "TCP_IP"
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(values)
            .Build();
        // 必须在两个 ADS 客户端创建前指定回环地址，确保它们使用本程序的 Router。
        AmsConfiguration.RouterLoopbackEndPoint = new IPEndPoint(
            IPAddress.Parse(Read("AdsTcpRouterLoopbackIp")), int.Parse(Read("AdsTcpRouterLoopbackPort")));
        AmsConfiguration.ChannelPortType = ChannelPortType.Loopback;
        return new AdsTcpRouterHost(configuration, NullLoggerFactory.Instance);
    }

    // 读取指定 Router 配置项并去除首尾空格。
    private static string Read(string key) =>
        AdsConnectionSettings.Read(key)?.Trim() ?? string.Empty;
}
