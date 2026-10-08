using InduLink.Protocols.Ads.Router;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using System.Net;
using TwinCAT.Ads;
using TwinCAT.Ams;

namespace Page_switching;

// 管理本程序的 Router；主窗体只负责启动顺序和退出顺序。
internal sealed class AdsTcpRouterRuntime : IAsyncDisposable
{
    private readonly NameValueCollection _settings;
    private readonly Action<string> _log;
    private readonly CancellationTokenSource _lifetime = new();
    private AdsTcpRouterHost? _host;
    private Task _runTask = Task.CompletedTask;
    private Task? _stopTask;

    internal AdsTcpRouterRuntime(NameValueCollection settings, Action<string> log)
    {
        _settings = settings;
        _log = log;
    }

    internal bool IsEnabled =>
        bool.TryParse(Read("AdsTcpRouterEnabled"), out var enabled) && enabled;

    // 系统 Router 无需创建；独立 Router 就绪后才允许两个 ADS 客户端连接。
    internal async Task StartAsync(CancellationToken cancellationToken)
    {
        LogConfiguration();
        if (!IsEnabled)
        {
            _log("独立 ADS TCP Router 未启用，使用系统 TwinCAT Router。");
            return;
        }

        try
        {
            var host = _host = CreateHost();
            host.StatusChanged += (_, _) => _log("ADS TCP Router 状态：" + host.Status);
            // StartAsync 表示整个运行期；启动流程只等待 IsRunning。
            // Router 使用独立取消源，退出时先释放客户端，最后再停止 Router。
            _runTask = host.StartAsync(_lifetime.Token);
            _ = ObserveRunTaskAsync();
            while (!host.IsRunning)
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (_runTask.IsCompleted)
                {
                    await _runTask.ConfigureAwait(false);
                    throw new InvalidOperationException("Router 在进入运行状态前已停止。");
                }
                await Task.Delay(50, cancellationToken).ConfigureAwait(false);
            }
            cancellationToken.ThrowIfCancellationRequested();
            _log("独立 ADS TCP Router 已启动");
        }
        catch (Exception ex)
        {
            _log("ADS TCP Router 启动失败：" + AdsDiagnostics.DescribeException(ex));
            await DisposeAsync().ConfigureAwait(false);
            // 启动失败不能改连其他本机 Router，交给主窗体停止本次连接。
            throw;
        }
    }

    // 运行期异常也要显示；此任务不等待 UI 回调，关闭时不会依赖消息循环。
    private async Task ObserveRunTaskAsync()
    {
        try { await _runTask.ConfigureAwait(false); }
        catch (OperationCanceledException) when (_lifetime.IsCancellationRequested) { }
        catch (Exception ex) { _log("ADS TCP Router 运行异常：" + AdsDiagnostics.DescribeException(ex)); }
    }

    private void LogConfiguration()
    {
        _log("ADS 配置文件：" + ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath);
        var adsVersion = FileVersionInfo.GetVersionInfo(typeof(TwinCAT.Ads.AdsClient).Assembly.Location).FileVersion;
        var routerVersion = FileVersionInfo.GetVersionInfo(typeof(TwinCAT.Ads.TcpRouter.AmsTcpIpRouter).Assembly.Location).FileVersion;
        _log($"ADS SDK={adsVersion}，Router SDK={routerVersion}");
        if (File.Exists(AdsConnectionSettings.FilePath)) _log("ADS 用户配置：" + AdsConnectionSettings.FilePath);
        if (AdsConnectionSettings.LoadError is { } error) _log("ADS 用户配置读取失败，采用程序默认配置：" + error);
        _log($"ADS 连接目标：AMS Net ID={Read("AdsAmsNetId")}，ADS 端口={Read("AdsPort")}，连接超时={Read("AdsConnectTimeoutMs")} ms");
        if (!IsEnabled) return;
        _log($"ADS Router 本机：AMS Net ID={Read("AdsTcpRouterLocalNetId")}，TCP 端口={Read("AdsTcpRouterTcpPort")}，回环={Read("AdsTcpRouterLoopbackIp")}:{Read("AdsTcpRouterLoopbackPort")}");
        _log($"ADS Router PLC 路由：IP={Read("AdsTcpRouterRemoteAddress")}，AMS Net ID={Read("AdsTcpRouterRemoteNetId")}");
    }

    // 使用启动时的同一份配置，不在连接过程中重新读取已保存的设置。
    private AdsTcpRouterHost CreateHost()
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

    private string Read(string key) => _settings[key]?.Trim() ?? string.Empty;

    // 主窗体在两个客户端退出后调用；启动失败后的重复清理也安全。
    public ValueTask DisposeAsync() => new(_stopTask ??= StopAsync());

    private async Task StopAsync()
    {
        _lifetime.Cancel();
        try { await _runTask.ConfigureAwait(false); }
        catch (OperationCanceledException) { }
        catch (Exception ex) { Debug.WriteLine("Router 退出：" + ex); }
        finally
        {
            _host?.Dispose();
            _host = null;
            _lifetime.Dispose();
        }
    }
}
