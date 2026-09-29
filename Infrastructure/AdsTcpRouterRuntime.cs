using System.Configuration;
using InduLink.Protocols.Ads.Router;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;

namespace Page_switching;

internal static class AdsTcpRouterRuntime
{
    public static bool IsEnabled =>
        bool.TryParse(Read("AdsTcpRouterEnabled"), out var enabled) && enabled;

    // 从 App.config 读取 Router 参数并创建 ADS TCP Router。
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
        return new AdsTcpRouterHost(configuration, NullLoggerFactory.Instance);
    }

    // 读取指定 Router 配置项并去除首尾空格。
    private static string Read(string key) =>
        System.Configuration.ConfigurationManager.AppSettings[key]?.Trim() ?? string.Empty;
}
