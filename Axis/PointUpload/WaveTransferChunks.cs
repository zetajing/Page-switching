namespace Page_switching;

public readonly record struct WaveTransferChunk(int Segment, int Axis, int Point, int Offset, int Count,
    int SegmentCount, long Completed, bool EndOfSegment);

// 逻辑段与实际 ADS 小批次分开，保证最后一批不跨段、不串轴。
public static class WaveTransferChunks
{
    public static IEnumerable<WaveTransferChunk> Create(int count)
    {
        if (count is < 4 or > WaveTrajectoryFile.MaximumPoints) throw new ArgumentOutOfRangeException(nameof(count));
        long completed = 0;
        for (int offset = 0; offset < count; offset += 100000)
        {
            int segmentCount = Math.Min(100000, count - offset);
            for (int axis = 1; axis <= 4; axis++)
                for (int point = 0; point < segmentCount; point += 1024)
                {
                    int size = Math.Min(1024, segmentCount - point);
                    completed += size;
                    yield return new(offset / 100000, axis, point, offset + point, size, segmentCount,
                        completed, axis == 4 && point + size == segmentCount);
                }
        }
    }
}
