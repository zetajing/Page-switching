using InduLink.Protocols.Ads;

namespace Page_switching;

// 只处理旧PLC的A/B反馈标定。连接由用户在反馈页发起。
internal sealed class LegacyFeedbackClient : IDisposable
{
    private readonly SemaphoreSlim _gate = new(1, 1);
    private AdsClient? _client;
    internal bool IsConnected => _client?.IsConnected == true;

    internal async Task ConnectAsync(CancellationToken token)
    {
        await _gate.WaitAsync(token);
        try
        {
            if (IsConnected) return;
            var options = AxisServiceOptions.FromConfiguration();
            _client?.Dispose();
            _client = new AdsClient(new AdsClientOptions
            {
                DeviceId = "legacy-feedback-plc", AmsNetId = options.AmsNetId, Port = options.AdsPort,
                ConnectTimeoutMilliseconds = options.ConnectTimeoutMilliseconds,
                OperationTimeoutMilliseconds = options.OperationTimeoutMilliseconds, ValidateTargetStateOnConnect = true
            });
            await _client.ConnectAsync(token);
            await _client.ReadAnyAsync<short>("GVL_1.CJZ_A_FK", cancellationToken: token);
            await _client.ReadAnyAsync<short>("GVL_1.CJZ_B_FK", cancellationToken: token);
            OperationJournal.Record("反馈标定", "已连接旧PLC的A/B反馈变量。");
        }
        catch { _client?.Dispose(); _client = null; throw; }
        finally { _gate.Release(); }
    }

    internal async Task<(short SensorA, short SensorB)> ReadAsync(CancellationToken token)
    {
        await _gate.WaitAsync(token);
        try
        {
            var client = _client ?? throw new InvalidOperationException("请先连接反馈PLC。");
            return (await client.ReadAnyAsync<short>("GVL_1.CJZ_A_FK", cancellationToken: token),
                await client.ReadAnyAsync<short>("GVL_1.CJZ_B_FK", cancellationToken: token));
        }
        finally { _gate.Release(); }
    }

    internal async Task WriteCalibrationAsync(LegacyProjectSettings settings, CancellationToken token)
    {
        await _gate.WaitAsync(token);
        try
        {
            var client = _client ?? throw new InvalidOperationException("请先连接反馈PLC。");
            if (await client.ReadAnyAsync<bool>("GVL_1.YunXingZhong", cancellationToken: token))
                throw new InvalidOperationException("PLC正在运行，不能修改反馈标定。");
            await client.WriteAnyAsync("GVL_1.B1_A", settings.SensorASlope, cancellationToken: token);
            await client.WriteAnyAsync("GVL_1.B2_A", settings.SensorAIntercept, cancellationToken: token);
            await client.WriteAnyAsync("GVL_1.B1_B", settings.SensorBSlope, cancellationToken: token);
            await client.WriteAnyAsync("GVL_1.B2_B", settings.SensorBIntercept, cancellationToken: token);
            OperationJournal.Record("反馈标定", "A/B斜率与截距已写入旧PLC。");
        }
        finally { _gate.Release(); }
    }

    public void Dispose() { _client?.Dispose(); _client = null; }
}
