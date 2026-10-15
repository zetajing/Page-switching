using System.Globalization;

namespace Page_switching;

// 专用于执行的旧文件导入器；数据已是最终 PLC 点位，不做单位或主动吸收换算。
public sealed record WaveTrajectoryFile(string FileName, double[] Positions)
{
    public const int MaximumPoints = 2_100_000;
    public const double SampleTime = 0.002;
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
        if (Math.Abs(metadata[1] - SampleTime) > 1e-9)
            throw new InvalidDataException("轨迹采样周期必须为 0.002 秒，不自动重采样。");
        var positions = new double[(int)count];
        for (int i = 0; i < positions.Length; i++)
            positions[i] = Number(First(Next() ?? throw new InvalidDataException($"点位不足：声明 {count} 点，实际只有 {i} 点。")));
        if (Next() is not null) throw new InvalidDataException("点位行数超过文件声明数量。");
        return new WaveTrajectoryFile(Path.GetFileName(path), positions);
    }
}
