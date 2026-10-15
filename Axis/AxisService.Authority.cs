using InduLink.Abstractions;
using InduLink.Protocols.Ads;

namespace Page_switching;

public sealed record ControlAuthorityStatus(
    bool Available, bool Granted, bool Locked, ushort Owner,
    uint AckSequence, ushort Result, long ReadAt, string? Error)
{
    public bool IsFresh => Available && Environment.TickCount64 - ReadAt < 1500;
    public static ControlAuthorityStatus Unavailable(string? error) => new(false, false, true, 0, 0, 0, 0, error);
}

public sealed partial class AxisService
{
    private CancellationTokenSource _authorityCancellation = new();
    private Task _authorityTask = Task.CompletedTask;
    private readonly SemaphoreSlim _authorityRequestGate = new(1, 1);
    private volatile ControlAuthorityStatus _authorityStatus = ControlAuthorityStatus.Unavailable("等待控制权反馈");
    private uint _authorityHeartbeat;
    private uint? _lastAuthoritySequence;

    public ushort ControlStationCode => _options.ControlStationCode;
    public ControlAuthorityStatus AuthorityStatus => _authorityStatus;
    public bool HasControlAuthority => ControlStationCode is >= 1 and <= 3
        && AuthorityStatus.IsFresh && AuthorityStatus.Granted && AuthorityStatus.Owner == ControlStationCode;
    private string StationPrefix => $"GVL_control.aStation[{ControlStationCode}]";

    // 连接成功后始终维护本站心跳，不随手动页面隐藏而停止；未配置身份只允许监控。
    private void StartAuthorityLoop()
    {
        if (ControlStationCode is < 1 or > 3 || !_authorityTask.IsCompleted) return;
        if (_authorityCancellation.IsCancellationRequested)
        {
            _authorityCancellation.Dispose();
            _authorityCancellation = new CancellationTokenSource();
        }
        _authorityTask = RunAuthorityLoopAsync(_authorityCancellation.Token);
    }

    private async Task StopAuthorityLoopAsync()
    {
        _authorityCancellation.Cancel();
        try { await _authorityTask.ConfigureAwait(false); }
        catch (OperationCanceledException) { }
        _authorityStatus = ControlAuthorityStatus.Unavailable("ADS 已断开");
    }

    private async Task RunAuthorityLoopAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(250, token).ConfigureAwait(false);
                await _operationGate.WaitAsync(token).ConfigureAwait(false);
                try
                {
                    var client = GetConnectedClient() ?? throw new InvalidOperationException("ADS 未连接");
                    _authorityHeartbeat = unchecked(_authorityHeartbeat + 1);
                    await client.WriteAsync(new WriteRequest(client.DeviceId, StationPrefix + ".udiHeartbeat",
                        DataType.UInt32, _authorityHeartbeat), token).ConfigureAwait(false);
                    var result = await client.ReadManyAsync(
                    [
                        new ReadRequest(client.DeviceId, "GVL_control.nOwner", DataType.UInt16),
                        new ReadRequest(client.DeviceId, "GVL_control.xLocked", DataType.Bool),
                        new ReadRequest(client.DeviceId, StationPrefix + ".xGranted", DataType.Bool),
                        new ReadRequest(client.DeviceId, StationPrefix + ".udiAckSequence", DataType.UInt32),
                        new ReadRequest(client.DeviceId, StationPrefix + ".nResult", DataType.UInt16)
                    ], token).ConfigureAwait(false);
                    var values = result.Values;
                    if (values.Count != 5 || values.Any(v => v.Quality != QualityStatus.Good || v.Value is null))
                        throw new InvalidOperationException("PLC 控制权反馈读取失败，请核对配套 PLC 是否部署。");
                    _authorityStatus = new ControlAuthorityStatus(true, (bool)values[2].Value!,
                        (bool)values[1].Value!, (ushort)values[0].Value!, (uint)values[3].Value!,
                        (ushort)values[4].Value!, Environment.TickCount64, null);
                }
                finally { _operationGate.Release(); }
            }
            catch (OperationCanceledException) when (token.IsCancellationRequested) { break; }
            catch (Exception ex)
            {
                // 失败立即撤销本地操作许可；PLC 独立检测心跳并停车，应用不自行切换 Owner。
                _authorityStatus = ControlAuthorityStatus.Unavailable(ex.Message);
            }
        }
    }

    private void RequireControlAuthority()
    {
        if (!HasControlAuthority)
            throw new InvalidOperationException("本站尚未取得有效 PLC 控制权，请先申请；连接成功不等于已授权。");
    }

    // 请求与应答使用同一非零序号；PLC 拒绝运行中交接，不将请求排队到停机后执行。
    public async Task ChangeControlAuthorityAsync(bool release, CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        if (ControlStationCode is < 1 or > 3)
            throw new InvalidOperationException("请先设置本站身份并重启程序。");
        using var linked = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _authorityCancellation.Token);
        var token = linked.Token;
        await _authorityRequestGate.WaitAsync(token).ConfigureAwait(false);
        try
        {
            var initial = AuthorityStatus;
            if (!initial.IsFresh) throw new InvalidOperationException(initial.Error ?? "控制权反馈不可用");
            // 此检查只改善提示，真正的运行锁仍由 PLC 在受理请求时检查。
            if (initial.Locked) throw new InvalidOperationException("设备运行或运动命令尚未退出，禁止控制权交接。");
            var sequence = unchecked((_lastAuthoritySequence ?? initial.AckSequence) + 1);
            if (sequence == 0) sequence = 1;
            _lastAuthoritySequence = sequence;
            await _operationGate.WaitAsync(token).ConfigureAwait(false);
            try
            {
                var client = GetConnectedClient() ?? throw new InvalidOperationException("ADS 未连接");
                // 最后提交序号，PLC 不会在 action 尚未写完整时提前受理。
                await client.WriteAsync(new WriteRequest(client.DeviceId, StationPrefix + ".nAction",
                    DataType.UInt16, (ushort)(release ? 2 : 1)), token).ConfigureAwait(false);
                await client.WriteAsync(new WriteRequest(client.DeviceId, StationPrefix + ".udiRequestSequence",
                    DataType.UInt32, sequence), token).ConfigureAwait(false);
            }
            finally { _operationGate.Release(); }

            var started = Environment.TickCount64;
            while (Environment.TickCount64 - started < 5000)
            {
                token.ThrowIfCancellationRequested();
                var status = AuthorityStatus;
                if (status.IsFresh && status.AckSequence == sequence)
                {
                    if (status.Result != 1) throw new InvalidOperationException(DescribeAuthorityResult(status.Result));
                    if (release ? status.Owner != ControlStationCode : status.Granted && status.Owner == ControlStationCode)
                        return;
                }
                // 等待时不占用 ADS 门锁，心跳和状态读取可以继续执行。
                await Task.Delay(50, token).ConfigureAwait(false);
            }
            throw new TimeoutException("未在 5 秒内收到同序号的 PLC 授权确认，请以实际反馈为准，勿视为交接成功。");
        }
        finally { _authorityRequestGate.Release(); }
    }

    public static string DescribeAuthorityResult(ushort result) => result switch
    {
        1 => "操作成功",
        2 => "设备运行或运动命令尚未退出，PLC 拒绝控制权交接。",
        3 => "本站未满足现场主控选择或授权条件。",
        4 => "本站心跳未建立或已超时。",
        5 => "本站不是当前控制权持有者，不能释放。",
        6 => "当前由更高优先级站点持有控制权。",
        _ => "PLC 拒绝无效的控制权操作。"
    };
}
