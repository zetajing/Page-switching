using InduLink.Abstractions;
using InduLink.Protocols.Ads;

namespace Page_switching;

// 造波 ADS 读写、脉冲和反馈等待；复用 AxisService 的同一客户端及串行锁。
public sealed partial class AxisService
{
    private uint? _waveLastPlcHeartbeat; // 上次实际反馈心跳；null 表示新会话尚未读到首帧。
    private long _waveHeartbeatTick; // 心跳最后变化时刻，用单调时钟判断失效。
    private bool _waveHeartbeatConfirmed; // 新会话必须看到心跳变化，不能直接采信第一帧。

    private Task SetWaveAxisCommandsAsync(bool? enable, bool stop, CancellationToken token) => WaveAccessAsync(async (client, ct) =>
    {
        var writes = new List<WriteRequest>();
        for (int i = 1; i <= 4; i++)
        {
            string prefix = $"{StationPrefix}.aAxis[{i}].";
            foreach (var field in new[] { "jogz", "jogf", "gohome", "reset" })
                writes.Add(new(client.DeviceId, prefix + field, DataType.Bool, false));
            if (enable.HasValue) writes.Add(new(client.DeviceId, prefix + "xStart", DataType.Bool, enable.Value));
            writes.Add(new(client.DeviceId, prefix + "xStop", DataType.Bool, stop));
        }
        await client.WriteManyAsync(writes, ct);
    }, token);

    private async Task PulseWaveAsync(string field, CancellationToken token)
    {
        await WaveWriteAsync("GVL_motion." + field, DataType.Bool, false, token);
        await Task.Delay(20, token);
        await WaveWriteAsync("GVL_motion." + field, DataType.Bool, true, token);
        try { await Task.Delay(50, token); }
        finally { await WaveWriteAsync("GVL_motion." + field, DataType.Bool, false, CancellationToken.None); }
    }

    private async Task WaveWriteAsync(string symbol, DataType type, object value, CancellationToken token)
    {
        try
        {
            await WaveAccessAsync((client, ct) => client.WriteAsync(new WriteRequest(client.DeviceId, symbol, type, value), ct), token);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            // 保留 InnerException 和具体符号，不能把底层 ADS 错误换成通用“发送失败”。
            throw new InvalidOperationException($"写入 {symbol} 失败：{AdsDiagnostics.DescribeException(ex)}", ex);
        }
    }

    private async Task WaveAccessAsync(Func<AdsClient, CancellationToken, Task> action, CancellationToken token)
    {
        await _operationGate.WaitAsync(token);
        try
        {
            token.ThrowIfCancellationRequested();
            RequireControlAuthority();
            await action(GetConnectedClient() ?? throw new InvalidOperationException("ADS 未连接。"), token);
        }
        finally { _operationGate.Release(); }
    }

    private async Task<PlcMachineMonitor> ReadWaveFrameAsync(CancellationToken token)
    {
        PlcMachineMonitor frame = default;
        await WaveAccessAsync(async (client, ct) =>
        {
            try
            {
                frame = await client.ReadAnyAsync<PlcMachineMonitor>(_options.SnapshotSymbol, cancellationToken: ct);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new InvalidOperationException($"读取 {_options.SnapshotSymbol} 失败：{AdsDiagnostics.DescribeException(ex)}", ex);
            }
        }, token);
        frame.ValidateLayout();
        if (frame.ControlOwner != ControlStationCode) throw new InvalidOperationException("PLC 控制端已改变，禁止继续发送或启动。");
        var now = Environment.TickCount64;
        if (!_waveLastPlcHeartbeat.HasValue) _waveHeartbeatTick = now;
        else if (_waveLastPlcHeartbeat.Value != frame.Heartbeat)
        {
            _waveHeartbeatConfirmed = true;
            _waveHeartbeatTick = now;
        }
        _waveLastPlcHeartbeat = frame.Heartbeat;
        if (now - _waveHeartbeatTick > 1500) throw new TimeoutException("PLC 心跳停止，造波反馈不可用。");
        return frame;
    }

    private async Task<PlcMachineMonitor> WaitWaveFrameAsync(Func<PlcMachineMonitor, bool> condition,
        int milliseconds, string operation, CancellationToken token, bool allowError = false)
    {
        long until = Environment.TickCount64 + milliseconds;
        PlcMachineMonitor frame;
        do
        {
            token.ThrowIfCancellationRequested();
            frame = await ReadWaveFrameAsync(token);
            try
            {
                if (!allowError && frame.Error)
                    throw new InvalidOperationException(PlcWaveDiagnostics.DescribeFault(frame.TrajectoryErrorId));
                if (condition(frame)) return frame;
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                // 记录失败当时的反馈；后续安全停止可能改变 PLC 状态，不能事后再取值替代。
                throw new InvalidOperationException($"{operation}失败：{ex.Message}；{PlcWaveDiagnostics.DescribeFrame(frame)}", ex);
            }
            await Task.Delay(100, token);
        } while (Environment.TickCount64 < until);
        throw new TimeoutException($"{operation}超时（{milliseconds / 1000.0:g} 秒），不能视为已完成；{PlcWaveDiagnostics.DescribeFrame(frame)}");
    }
}
