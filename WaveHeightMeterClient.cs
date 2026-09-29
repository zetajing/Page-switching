using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Page_switching;

public sealed class WaveHeightSampleEventArgs(DateTimeOffset timestamp, int channel, int rawCount) : EventArgs
{
    public DateTimeOffset Timestamp { get; } = timestamp;
    public int Channel { get; } = channel;
    public int RawCount { get; } = rawCount;
}

public sealed class WaveHeightConnectionLostEventArgs(Exception? error) : EventArgs
{
    public Exception? Error { get; } = error;
}

/// <summary>Receives the legacy wave height meter's TCP binary frames.</summary>
public sealed class WaveHeightMeterClient : IDisposable
{
    private readonly IWaveHeightProtocol _protocol;
    private readonly object _sync = new();
    private readonly SemaphoreSlim _lifecycleGate = new(1, 1);
    private Socket? _socket;
    private Task? _receiveTask;
    private int _connected;
    private int _disposed;
    private int _diagnosticCount;

    public bool IsConnected => Volatile.Read(ref _connected) != 0;

    public event EventHandler<WaveHeightSampleEventArgs>? SampleReceived;
    public event EventHandler<WaveHeightConnectionLostEventArgs>? ConnectionLost;
    public event EventHandler<string>? ProtocolDiagnostic;

    public WaveHeightMeterClient(IWaveHeightProtocol? protocol = null)
    {
        _protocol = protocol ?? new LegacyChannelOneProtocol();
    }

    public async Task ConnectAsync(
        string ipAddress,
        int port,
        int sampleRateHz,
        IReadOnlyCollection<int> channels,
        CancellationToken cancellationToken)
    {
        ObjectDisposedException.ThrowIf(Volatile.Read(ref _disposed) != 0, this);
        if (!IPAddress.TryParse(ipAddress, out var address))
        {
            throw new InvalidOperationException("请输入有效的 IPv4 或 IPv6 地址。");
        }

        if (port is < 1 or > 65535)
        {
            throw new InvalidOperationException("设备端口必须在 1 到 65535 之间。");
        }

        var startCommands = _protocol.StartCommands(channels, sampleRateHz);

        await _lifecycleGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            ObjectDisposedException.ThrowIf(Volatile.Read(ref _disposed) != 0, this);
            await DisconnectCoreAsync(cancellationToken).ConfigureAwait(false);

            var socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
            {
                NoDelay = true
            };

            using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeout.CancelAfter(TimeSpan.FromSeconds(5));

            try
            {
                await socket.ConnectAsync(new IPEndPoint(address, port), timeout.Token).ConfigureAwait(false);
                foreach (var command in startCommands)
                    await SendAllAsync(socket, command, timeout.Token).ConfigureAwait(false);

                ObjectDisposedException.ThrowIf(Volatile.Read(ref _disposed) != 0, this);
                lock (_sync)
                {
                    ObjectDisposedException.ThrowIf(Volatile.Read(ref _disposed) != 0, this);
                    _socket = socket;
                    Volatile.Write(ref _connected, 1);
                    _receiveTask = Task.Run(() => ReceiveLoopAsync(socket));
                }
            }
            catch
            {
                socket.Dispose();
                throw;
            }
        }
        finally
        {
            _lifecycleGate.Release();
        }
    }

    /// <summary>Sends the legacy stop command, then closes the TCP connection.</summary>
    public async Task DisconnectAsync(CancellationToken cancellationToken = default)
    {
        await _lifecycleGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            await DisconnectCoreAsync(cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            _lifecycleGate.Release();
        }
    }

    private async Task DisconnectCoreAsync(CancellationToken cancellationToken)
    {
        Socket? socket;
        Task? receiveTask;
        lock (_sync)
        {
            socket = _socket;
            _socket = null;
            receiveTask = _receiveTask;
            _receiveTask = null;
            Volatile.Write(ref _connected, 0);
        }

        if (socket is null)
        {
            return;
        }

        try
        {
            using var stopTimeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            stopTimeout.CancelAfter(TimeSpan.FromMilliseconds(500));
            await SendAllAsync(socket, Encoding.ASCII.GetBytes("{\"cmd\":\"stop\"}"), stopTimeout.Token).ConfigureAwait(false);
        }
        catch
        {
            // Always close the socket, even when the device no longer accepts commands.
        }

        CloseSocket(socket);
        if (receiveTask is not null)
        {
            try
            {
                await receiveTask.ConfigureAwait(false);
            }
            catch
            {
                // The receive loop reports transport errors through ConnectionLost.
            }
        }
    }

    private async Task ReceiveLoopAsync(Socket socket)
    {
        Exception? failure = null;
        var receiveBuffer = new byte[4096];
        var pending = new List<byte>(4096);

        try
        {
            while (true)
            {
                var bytesRead = await socket.ReceiveAsync(
                    receiveBuffer.AsMemory(),
                    SocketFlags.None,
                    CancellationToken.None).ConfigureAwait(false);

                if (bytesRead == 0)
                {
                    throw new IOException("浪高仪已关闭 TCP 连接。");
                }

                for (var index = 0; index < bytesRead; index++)
                {
                    pending.Add(receiveBuffer[index]);
                }

                ExtractFrames(pending);
            }
        }
        catch (Exception ex)
        {
            failure = ex;
        }
        finally
        {
            var notify = false;
            lock (_sync)
            {
                if (ReferenceEquals(_socket, socket))
                {
                    _socket = null;
                    _receiveTask = null;
                    Volatile.Write(ref _connected, 0);
                    notify = true;
                }
            }

            CloseSocket(socket);
            if (notify)
            {
                RaiseConnectionLost(failure);
            }
        }
    }

    private void ExtractFrames(List<byte> pending)
    {
        var frameHeader = _protocol.FrameHeader.Span;
        while (true)
        {
            var headerIndex = FindHeader(pending, frameHeader);
            if (headerIndex < 0)
            {
                // Keep a possible partial header at the end of the current TCP chunk.
                if (pending.Count > frameHeader.Length - 1)
                {
                    var discarded = pending.Count - (frameHeader.Length - 1);
                    pending.RemoveRange(0, discarded);
                    if (discarded > 0) RaiseDiagnostic($"忽略 {discarded} 个未识别字节。");
                }

                return;
            }

            if (headerIndex > 0)
            {
                pending.RemoveRange(0, headerIndex);
                RaiseDiagnostic($"跳过 {headerIndex} 个帧前字节。");
            }

            if (pending.Count < _protocol.FrameLength)
            {
                return;
            }

            var frame = pending.GetRange(0, _protocol.FrameLength).ToArray();
            pending.RemoveRange(0, _protocol.FrameLength);
            if (_protocol.TryDecodeFrame(frame, out var channel, out var rawCount) &&
                _protocol.SupportedChannels.Contains(channel))
                RaiseSampleReceived(new WaveHeightSampleEventArgs(DateTimeOffset.Now, channel, rawCount));
            else
                RaiseDiagnostic("忽略通道未知或格式错误的设备帧。");
        }
    }

    private static int FindHeader(IReadOnlyList<byte> bytes, ReadOnlySpan<byte> header)
    {
        for (var index = 0; index <= bytes.Count - header.Length; index++)
        {
            var matches = true;
            for (var headerIndex = 0; headerIndex < header.Length; headerIndex++)
            {
                if (bytes[index + headerIndex] != header[headerIndex])
                {
                    matches = false;
                    break;
                }
            }

            if (matches)
            {
                return index;
            }
        }

        return -1;
    }

    private static async Task SendAllAsync(Socket socket, byte[] payload, CancellationToken cancellationToken)
    {
        var offset = 0;
        while (offset < payload.Length)
        {
            var sent = await socket.SendAsync(
                payload.AsMemory(offset),
                SocketFlags.None,
                cancellationToken).ConfigureAwait(false);

            if (sent <= 0)
            {
                throw new IOException("无法向浪高仪发送命令。");
            }

            offset += sent;
        }
    }

    private void RaiseDiagnostic(string message)
    {
        if (Interlocked.Increment(ref _diagnosticCount) > 20) return;
        try { ProtocolDiagnostic?.Invoke(this, message); }
        catch { /* Diagnostic observers must not stop acquisition. */ }
    }

    private static void CloseSocket(Socket socket)
    {
        try
        {
            socket.Shutdown(SocketShutdown.Both);
        }
        catch
        {
            // Ignore already closed or disconnected sockets.
        }

        socket.Dispose();
    }

    private void RaiseSampleReceived(WaveHeightSampleEventArgs args) =>
        NotifySubscribers(SampleReceived, args);

    private void RaiseConnectionLost(Exception? error) =>
        NotifySubscribers(ConnectionLost, new WaveHeightConnectionLostEventArgs(error));

    private void NotifySubscribers<T>(EventHandler<T>? handlers, T args)
    {
        if (handlers is null)
        {
            return;
        }

        foreach (EventHandler<T> handler in handlers.GetInvocationList())
        {
            try
            {
                handler(this, args);
            }
            catch
            {
                // A subscriber must not terminate the socket receive loop.
            }
        }
    }

    public void Dispose()
    {
        if (Interlocked.Exchange(ref _disposed, 1) != 0)
        {
            return;
        }

        Socket? socket;
        lock (_sync)
        {
            socket = _socket;
            _socket = null;
            _receiveTask = null;
            Volatile.Write(ref _connected, 0);
        }

        if (socket is null)
        {
            return;
        }

        try
        {
            socket.SendTimeout = 250;
            var stopCommand = Encoding.ASCII.GetBytes("{\"cmd\":\"stop\"}");
            socket.Send(stopCommand, SocketFlags.None);
        }
        catch
        {
            // Shutdown must not block closing the application.
        }

        CloseSocket(socket);
    }
}
