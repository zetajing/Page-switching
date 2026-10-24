using InduLink.Abstractions;
using InduLink.Protocols.Ads;

namespace Page_switching;

internal sealed record AxisMonitoringReading(PlcMachineMonitor Frame, bool FeedbackInvalid);
internal interface IAxisMonitoringSource : IDisposable
{
    Task<AxisMonitoringReading> ReadAsync(CancellationToken token);
}
internal interface IAxisMonitoringClock
{
    long Milliseconds { get; }
    DateTimeOffset UtcNow { get; }
    Task DelayAsync(int milliseconds, CancellationToken token);
}
internal sealed class AxisMonitoringClock : IAxisMonitoringClock
{
    public long Milliseconds => Environment.TickCount64;
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    public Task DelayAsync(int milliseconds, CancellationToken token) => Task.Delay(milliseconds, token);
}

// 只有读取接口；独立客户端不持有运动命令锁，也不申请控制权或写心跳。
internal sealed class AdsAxisMonitoringSource(AxisServiceOptions options) : IAxisMonitoringSource
{
    private AdsClient? _client;
    public async Task<AxisMonitoringReading> ReadAsync(CancellationToken token)
    {
        try
        {
            _client ??= new AdsClient(new AdsClientOptions
            {
                DeviceId = "axis-runtime-monitor", AmsNetId = options.AmsNetId, Port = options.AdsPort,
                ConnectTimeoutMilliseconds = 1000, OperationTimeoutMilliseconds = 500,
                ValidateTargetStateOnConnect = true
            });
            if (!_client.IsConnected)
            {
                using var connect = CancellationTokenSource.CreateLinkedTokenSource(token);
                connect.CancelAfter(1000);
                await _client.ConnectAsync(connect.Token).ConfigureAwait(false);
            }
            using var read = CancellationTokenSource.CreateLinkedTokenSource(token);
            read.CancelAfter(500);
            var frame = await _client.ReadAnyAsync<PlcMachineMonitor>(options.SnapshotSymbol, cancellationToken: read.Token).ConfigureAwait(false);
            frame.ValidateLayout();
            // 两次 ADS 请求不是原子快照；记录的是上位机读取完成时刻，不伪称 PLC 采样时刻。
            var quality = await _client.ReadManyAsync(new[]
            {
                new ReadRequest(_client.DeviceId, "GVL_control.xFeedbackInvalid", DataType.Bool)
            }, read.Token).ConfigureAwait(false);
            read.Token.ThrowIfCancellationRequested();
            if (quality.Values.Count != 1 || quality.Values[0].Quality != QualityStatus.Good || quality.Values[0].Value is not bool invalid)
                throw new InvalidDataException("GVL_control.xFeedbackInvalid 反馈无效。");
            return new(frame, invalid);
        }
        catch
        {
            _client?.Dispose(); _client = null;
            throw;
        }
    }
    public void Dispose() { _client?.Dispose(); _client = null; }
}
