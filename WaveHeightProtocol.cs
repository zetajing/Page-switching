using System.Text;

namespace Page_switching;

/// <summary>Device framing contract. Further channels require confirmed device evidence.</summary>
public interface IWaveHeightProtocol
{
    IReadOnlyCollection<int> SupportedChannels { get; }
    int FrameLength { get; }
    ReadOnlyMemory<byte> FrameHeader { get; }
    IReadOnlyList<byte[]> StartCommands(IReadOnlyCollection<int> channels, int sampleRateHz);
    bool TryDecodeFrame(ReadOnlySpan<byte> frame, out int channel, out int rawCount);
}

public sealed class LegacyChannelOneProtocol : IWaveHeightProtocol
{
    private static readonly byte[] Header = [0xAA, 0x55, 0x55, 0xAA];
    public IReadOnlyCollection<int> SupportedChannels => [1];
    public int FrameLength => 15;
    public ReadOnlyMemory<byte> FrameHeader => Header;

    public IReadOnlyList<byte[]> StartCommands(IReadOnlyCollection<int> channels, int sampleRateHz)
    {
        if (channels.Count != 1 || !channels.Contains(1))
            throw new NotSupportedException("尚无通道 2–6 的设备协议，不能启动真实采集。");
        if (sampleRateHz is < 2 or > 50)
            throw new ArgumentOutOfRangeException(nameof(sampleRateHz));
        return
        [
            Encoding.ASCII.GetBytes("{\"ch\":1,\"type\":2}"),
            Encoding.ASCII.GetBytes($"{{\"cmd\":\"start\",\"Hz\":{sampleRateHz},\"enable\":\"00000001\"}}")
        ];
    }

    public bool TryDecodeFrame(ReadOnlySpan<byte> frame, out int channel, out int rawCount)
    {
        channel = 0;
        rawCount = 0;
        if (frame.Length != FrameLength || !frame[..Header.Length].SequenceEqual(Header))
            return false;
        // Only the channel-one value offset is established by the legacy source.
        channel = 1;
        rawCount = (frame[13] << 8) | frame[14];
        return true;
    }
}
