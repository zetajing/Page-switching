using InduLink.Abstractions;
using InduLink.Protocols.Ads;

namespace Page_switching;

// 手动控制：只提交使能、复位、回零、停止和点动命令；执行结果由反馈读取。
public sealed partial class AxisService
{
    public bool CanControlAll => !WaveOperationActive && IsConnected && HasControlAuthority && _options.AxisSymbols.All(map =>
            !string.IsNullOrWhiteSpace(map.EnableCommand) &&
            !string.IsNullOrWhiteSpace(map.ResetAlarmCommand) &&
            !string.IsNullOrWhiteSpace(map.HomeCommand) &&
            !string.IsNullOrWhiteSpace(map.StopCommand));

    // 判断指定轴是否已连接，并且已经配置回零、停止和点动变量。
    public bool CanControlAxis(int axisNumber)
    {
        ValidateAxisNumber(axisNumber);
        var map = _options.AxisSymbols[axisNumber - 1];
        return !WaveOperationActive && IsConnected && HasControlAuthority &&
            !string.IsNullOrWhiteSpace(map.HomeCommand) &&
            !string.IsNullOrWhiteSpace(map.StopCommand) &&
            !string.IsNullOrWhiteSpace(map.JogSpeedCommand) &&
            !string.IsNullOrWhiteSpace(map.JogPositiveCommand) &&
            !string.IsNullOrWhiteSpace(map.JogNegativeCommand);
    }

    // 将四根轴的 EnableCommand 全部写为 true。
    public Task EnableAllAsync(CancellationToken cancellationToken) =>
        WriteAllAsync(
            _options.AxisSymbols.Select(axis => axis.EnableCommand).ToArray(), true, cancellationToken);

    // 将四根轴的 EnableCommand 全部写为 false。
    public Task DisableAllAsync(CancellationToken cancellationToken) =>
        WriteAllAsync(
            _options.AxisSymbols.Select(axis => axis.EnableCommand).ToArray(), false, cancellationToken);

    // 单轴使能沿用批量写入的门锁、控制权和造波互锁，只提交本轴符号。
    public Task SetAxisEnabledAsync(int axisNumber, bool enabled, CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        ValidateAxisNumber(axisNumber);
        string symbol = _options.AxisSymbols[axisNumber - 1].EnableCommand;
        if (string.IsNullOrWhiteSpace(symbol))
        {
            throw new InvalidOperationException($"轴 {axisNumber} 尚未配置 ADS 控制符号。");
        }
        return WriteAllAsync(new string[] { symbol }, enabled, cancellationToken);
    }

    // 将一组布尔变量批量写成指定值。
    private async Task WriteAllAsync(
        IReadOnlyList<string> symbols,
        bool value,
        CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        await _operationGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var client = GetConnectedClient() ?? throw new InvalidOperationException("ADS 尚未连接。");
            RequireControlAuthority();
            if (WaveOperationActive) throw new InvalidOperationException("造波会话进行中，请先停止造波再手动操作。");
            await client.WriteManyAsync(CreateWriteRequests(client, symbols, value), cancellationToken)
                .ConfigureAwait(false);
        }
        finally
        {
            _operationGate.Release();
        }
    }

    // 将四根轴的 ResetAlarmCommand 置位 50 毫秒后复位。
    public Task ResetAlarmsAsync(CancellationToken cancellationToken) =>
        PulseAllAsync(
            _options.AxisSymbols.Select(axis => axis.ResetAlarmCommand).ToArray(), cancellationToken);

    // 本轴报警复位复用原有 50 ms 脉冲，取消时仍尝试清零。
    public Task ResetAxisAlarmAsync(int axisNumber, CancellationToken cancellationToken)
    {
        ValidateAxisNumber(axisNumber);
        string symbol = _options.AxisSymbols[axisNumber - 1].ResetAlarmCommand;
        return PulseAxisAsync(axisNumber, symbol, cancellationToken);
    }

    // 将四根轴的 HomeCommand 置位 50 毫秒后复位。
    public Task HomeAllAsync(CancellationToken cancellationToken) =>
        PulseAllAsync(
            _options.AxisSymbols.Select(axis => axis.HomeCommand).ToArray(), cancellationToken);

    // 将四根轴的 StopCommand 置位 50 毫秒后复位。
    public Task StopAllAsync(CancellationToken cancellationToken) =>
        PulseAllAsync(
            _options.AxisSymbols.Select(axis => axis.StopCommand).ToArray(), cancellationToken);

    // 触发指定轴的 HomeCommand，使该轴开始回零。
    public Task HomeAxisAsync(int axisNumber, CancellationToken cancellationToken)
    {
        ValidateAxisNumber(axisNumber);
        return PulseAxisAsync(axisNumber, _options.AxisSymbols[axisNumber - 1].HomeCommand, cancellationToken);
    }

    // 触发指定轴的 StopCommand，使该轴停止运动。
    public Task StopAxisAsync(int axisNumber, CancellationToken cancellationToken)
    {
        ValidateAxisNumber(axisNumber);
        return PulseAxisAsync(axisNumber, _options.AxisSymbols[axisNumber - 1].StopCommand, cancellationToken);
    }

    // 写入点动速度和方向变量；按下时写 true，松开时写 false。
    public async Task JogAsync(
        int axisNumber,
        bool positive,
        bool start,
        double speed,
        CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        ValidateAxisNumber(axisNumber);

        var map = _options.AxisSymbols[axisNumber - 1];
        var commandSymbol = positive ? map.JogPositiveCommand : map.JogNegativeCommand;
        if (string.IsNullOrWhiteSpace(commandSymbol) || string.IsNullOrWhiteSpace(map.JogSpeedCommand))
        {
            throw new InvalidOperationException($"轴 {axisNumber} 尚未完整配置 ADS 点动速度和方向符号。");
        }

        await _operationGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var client = GetConnectedClient() ?? throw new InvalidOperationException("ADS 尚未连接。");
            if (start)
            {
                RequireControlAuthority();
                if (WaveOperationActive) throw new InvalidOperationException("造波会话进行中，禁止点动。");
                await client.WriteAsync(
                    new WriteRequest(client.DeviceId, map.JogSpeedCommand, DataType.Double, Math.Abs(speed)),
                    cancellationToken).ConfigureAwait(false);
            }

            await client.WriteAsync(
                new WriteRequest(client.DeviceId, commandSymbol, DataType.Bool, start),
                cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            _operationGate.Release();
        }
    }

    // 将四根轴的命令变量置位 50 毫秒，再全部复位。
    private async Task PulseAllAsync(
        IReadOnlyList<string> symbols,
        CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        await _operationGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var client = GetConnectedClient() ?? throw new InvalidOperationException("ADS 尚未连接。");
            RequireControlAuthority();
            if (WaveOperationActive) throw new InvalidOperationException("造波会话进行中，请先停止造波再手动操作。");
            var setRequests = CreateWriteRequests(client, symbols, true);
            var resetRequests = CreateWriteRequests(client, symbols, false);
            try
            {
                await client.WriteManyAsync(setRequests, cancellationToken).ConfigureAwait(false);
                await Task.Delay(50, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                // 置位可能部分成功；即使取消，也按原轴顺序尝试复位，超时由 ADS 客户端控制。
                await client.WriteManyAsync(resetRequests, CancellationToken.None).ConfigureAwait(false);
            }
        }
        finally
        {
            _operationGate.Release();
        }
    }

    // 将指定轴的命令变量置位 50 毫秒，再复位。
    private async Task PulseAxisAsync(
        int axisNumber,
        string symbol,
        CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        ValidateAxisNumber(axisNumber);
        await _operationGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var client = GetConnectedClient() ?? throw new InvalidOperationException("ADS 尚未连接。");
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new InvalidOperationException($"轴 {axisNumber} 尚未配置 ADS 控制符号。");
            }

            RequireControlAuthority();
            if (WaveOperationActive) throw new InvalidOperationException("造波会话进行中，请先停止造波再手动操作。");
            try
            {
                await client.WriteAsync(
                    new WriteRequest(client.DeviceId, symbol, DataType.Bool, true), cancellationToken)
                    .ConfigureAwait(false);
                await Task.Delay(50, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                // 页面取消不应跳过复位；断线时仍会向调用方报告写入失败。
                await client.WriteAsync(
                    new WriteRequest(client.DeviceId, symbol, DataType.Bool, false), CancellationToken.None)
                    .ConfigureAwait(false);
            }
        }
        finally
        {
            _operationGate.Release();
        }
    }

    // 把变量地址和值转换成 ADS 批量写请求。
    private static IReadOnlyCollection<WriteRequest> CreateWriteRequests(
        AdsClient client,
        IReadOnlyList<string> symbols,
        bool value)
    {
        var requests = new List<WriteRequest>(symbols.Count);
        for (var index = 0; index < symbols.Count; index++)
        {
            var symbol = symbols[index];
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new InvalidOperationException($"轴 {index + 1} 尚未配置 ADS 控制符号。");
            }

            requests.Add(new WriteRequest(client.DeviceId, symbol, DataType.Bool, value));
        }

        return requests;
    }
}
