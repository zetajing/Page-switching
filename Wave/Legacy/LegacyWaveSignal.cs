namespace Page_switching;

internal sealed class LegacyWaveSignal
{
    internal required double[] Displacement { get; init; }
    internal double[]? Absorption { get; init; }
    internal required double Interval { get; init; }
    internal double Depth { get; init; }
    internal double Period { get; init; }
    internal double Height { get; init; }
    internal int WaveType { get; init; }
    internal int Spectrum { get; init; }
    internal double[] Gains { get; init; } = [];

    // 旧信号首行为表头，随后8行元信息，再是PLC位移(mm)。
    // ElevationMetres 是水面高程，不能直接当成造波板位移下发。
    internal static LegacyWaveSignal Read(string path)
    {
        var lines = File.ReadAllLines(path).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        if (lines.Length < 11 || lines[0].Contains("ElevationMetres", StringComparison.OrdinalIgnoreCase))
            throw new InvalidDataException("请选择旧格式的造波板位移文件；水面高程CSV不能直接控制电机。");
        var rows = lines.Skip(1).Select(WaveSeriesFile.Fields).ToArray();
        double At(int index) => WaveSeriesFile.Number(rows[index][0]);
        var count = WaveSeriesFile.Integer(rows[0][0]);
        var dt = At(1);
        if (count is < 2 or > 1_100_000 || dt <= 0 || dt > 10 || rows.Length != count + 8)
            throw new InvalidDataException("造波文件点数、时间步长或数据行数无效，应有8行元信息及声明数量的位移行。");
        var absorption = rows.Skip(8).All(x => x.Length > 1 && !string.IsNullOrWhiteSpace(x[1]));
        return new LegacyWaveSignal
        {
            Displacement = rows.Skip(8).Select(x => WaveSeriesFile.Number(x[0])).ToArray(),
            Absorption = absorption ? rows.Skip(8).Select(x => WaveSeriesFile.Number(x[1])).ToArray() : null,
            Interval = dt, Depth = At(2), Period = At(3), Height = At(4),
            WaveType = WaveSeriesFile.Integer(rows[5][0]), Spectrum = WaveSeriesFile.Integer(rows[6][0]),
            Gains = absorption ? rows.Take(3).Select(x => WaveSeriesFile.Number(x[1])).ToArray() : []
        };
    }

}
