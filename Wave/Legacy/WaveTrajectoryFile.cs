using System.Globalization;

namespace Page_switching;

// 专用于执行的旧文件导入器；数据已是最终 PLC 点位，不做单位或主动吸收换算。
public sealed record WaveTrajectoryFile(string FileName, double[] Positions)
{
    public const int MaximumPoints = 2_100_000;
    public const double SampleTime = 0.002;
    public int SourcePointCount { get; init; } = Positions.Length;
    public double SourceSampleTime { get; init; } = SampleTime;
    public double DurationSeconds => (Positions.Length - 1) * SampleTime;

    public static WaveTrajectoryFile Read(string path)
    {
        using var reader = new StreamReader(path, detectEncodingFromByteOrderMarks: true);
        string? Next()
        {
            string? line;
            do { line = reader.ReadLine(); } while (line is not null && string.IsNullOrWhiteSpace(line));
            return line;
        }
        static string First(string line) => line.Split([',', ';', '\t'], StringSplitOptions.TrimEntries)[0];
        static double Number(string text)
        {
            if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var number) || !double.IsFinite(number))
                throw new InvalidDataException("造波文件包含无效数值。");
            return number;
        }
        var header = Next() ?? throw new InvalidDataException("造波文件为空。");
        if (header.Contains("ElevationMetres", StringComparison.OrdinalIgnoreCase))
            throw new InvalidDataException("水面高程文件不能作为最终 PLC 点位发送。");
        var metadata = new double[8];
        for (int i = 0; i < metadata.Length; i++)
            metadata[i] = Number(First(Next() ?? throw new InvalidDataException("缺少旧格式的 8 行元信息。")));
        var count = metadata[0];
        if (count != Math.Truncate(count) || count < 4 || count > MaximumPoints)
            throw new InvalidDataException("轨迹点数必须为 4～2,100,000。");
        int subdivisions;
        if (Math.Abs(metadata[1] - SampleTime) <= 1e-9)
            subdivisions = 1;
        else if (Math.Abs(metadata[1] - 0.02) <= 1e-9)
            subdivisions = 10;
        else
            throw new InvalidDataException("轨迹采样周期仅支持 0.002 秒或 0.02 秒；20 ms 点位会自动线性补点为 2 ms。");

        // N 个原点只有 N-1 个时间间隔，保留首末点后时长不变；先核对 PLC 容量再分配数组。
        int outputCount = ((int)count - 1) * subdivisions + 1;
        if (outputCount > MaximumPoints)
            throw new InvalidDataException($"线性补点后共有 {outputCount:N0} 点，超过 2,100,000 点；20 ms 文件最多支持 210,000 个原始点。");
        var positions = new double[(int)count];
        for (int i = 0; i < positions.Length; i++)
            positions[i] = Number(First(Next() ?? throw new InvalidDataException($"点位不足：声明 {count} 点，实际只有 {i} 点。")));
        if (Next() is not null) throw new InvalidDataException("点位行数超过文件声明数量。");

        var output = positions;
        if (subdivisions == 10)
        {
            output = new double[outputCount];
            for (int point = 0; point < positions.Length - 1; point++)
            {
                int offset = point * subdivisions;
                // 原点直接复制，两个原点之间按直线等分；最后一段也只读取已有的右端点。
                output[offset] = positions[point];
                for (int step = 1; step < subdivisions; step++)
                    output[offset + step] = positions[point]
                        + (positions[point + 1] - positions[point]) * (step / (double)subdivisions);
            }
            output[^1] = positions[^1];
        }
        return new WaveTrajectoryFile(Path.GetFileName(path), output)
        {
            SourcePointCount = positions.Length,
            SourceSampleTime = subdivisions * SampleTime
        };
    }
}
