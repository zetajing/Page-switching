using System.Numerics;

namespace Page_switching;

internal sealed record WaveAnalysisResult(double[] Frequencies, double[] Measured, double[] Theoretical,
    IReadOnlyDictionary<string, double> Statistics, int UsedSamples, int WaveCount);

internal static class WaveAnalysisService
{
    internal static WaveAnalysisResult Analyze(WaveSeries series, double height, double period, double gamma,
        int spectrum, int smoothPasses, int smoothPoints)
    {
        if (series.Values.Length < 64) throw new InvalidOperationException("频谱分析至少需要64个样本。");
        if (series.Values.Length > 4_194_304) throw new InvalidOperationException("单次分析最多支持4194304个样本。");
        if (height <= 0 || period <= 0 || gamma <= 0) throw new InvalidOperationException("理论波高、周期和谱峰因子必须大于0。");
        var n = 1;
        while (n * 2 <= series.Values.Length) n *= 2;
        var centered = series.Values.Take(n).ToArray();
        var mean = centered.Average();
        if (!double.IsFinite(mean)) throw new InvalidDataException("采样幅值过大，均值计算溢出。");
        for (var i = 0; i < n; i++) centered[i] -= mean;
        var input = new Complex[n];
        double windowEnergy = 0;
        for (var i = 0; i < n; i++)
        {
            var window = 0.5 - 0.5 * Math.Cos(2 * Math.PI * i / (n - 1));
            input[i] = centered[i] * window;
            windowEnergy += window * window;
        }
        Transform(input);
        var frequencies = new double[n / 2 + 1];
        var measured = new double[frequencies.Length];
        var df = 1.0 / (n * series.Interval);
        for (var i = 0; i < measured.Length; i++)
        {
            frequencies[i] = i * df;
            measured[i] = input[i].Magnitude * input[i].Magnitude * series.Interval / windowEnergy;
            if (i > 0 && i < n / 2) measured[i] *= 2;
        }
        measured[0] = 0;
        if (measured.Any(x => !double.IsFinite(x))) throw new InvalidDataException("采样幅值过大，谱密度计算溢出。");
        measured = Smooth(measured, smoothPasses, smoothPoints);
        var parameters = new IrregularWaveParameters(1, period, height, 0, series.Interval, n,
            0, 0, gamma, 0, 0.1, 10, 0, 0, 0, 0, 2, spectrum, 1, 1);
        var density = WaveMakerWaveGenerator.CreateSpectrum(parameters);
        var theoretical = frequencies.Select(density).ToArray();
        var waves = GetWaves(centered, series.Interval);
        var stats = GetStatistics(waves);
        double Moment(int order) => measured.Select((value, i) => value * Math.Pow(frequencies[i], order) * df).Sum();
        var m0 = Moment(0);
        var m1 = Moment(1);
        var m2 = Moment(2);
        var peak = Array.IndexOf(measured, measured.Max());
        stats["Hm0 (m)"] = 4 * Math.Sqrt(m0);
        stats["Tp (s)"] = peak > 0 ? 1 / frequencies[peak] : 0;
        stats["Tm01 (s)"] = m1 > 0 ? m0 / m1 : 0;
        stats["Tm02 (s)"] = m2 > 0 ? Math.Sqrt(m0 / m2) : 0;
        return new WaveAnalysisResult(frequencies, measured, theoretical, stats, n, waves.Count);
    }

    // 基2 FFT，输入长度必须为2的幂。
    internal static void Transform(Complex[] values)
    {
        var n = values.Length;
        for (int i = 1, j = 0; i < n; i++)
        {
            var bit = n >> 1;
            for (; (j & bit) != 0; bit >>= 1) j ^= bit;
            j ^= bit;
            if (i < j) (values[i], values[j]) = (values[j], values[i]);
        }
        for (var length = 2; length <= n; length <<= 1)
        {
            var step = Complex.FromPolarCoordinates(1, -2 * Math.PI / length);
            for (var start = 0; start < n; start += length)
            {
                var factor = Complex.One;
                for (var j = 0; j < length / 2; j++)
                {
                    var a = values[start + j];
                    var b = values[start + j + length / 2] * factor;
                    values[start + j] = a + b;
                    values[start + j + length / 2] = a - b;
                    factor *= step;
                }
            }
        }
    }

    private static double[] Smooth(double[] values, int passes, int points)
    {
        if (passes is < 0 or > 20 || points is < 1 or > 101 || points % 2 == 0)
            throw new InvalidOperationException("光滑次数为0–20，点数为1–101的奇数。");
        for (var pass = 0; pass < passes; pass++)
        {
            var prefix = new double[values.Length + 1];
            for (var i = 0; i < values.Length; i++) prefix[i + 1] = prefix[i] + values[i];
            var next = new double[values.Length];
            for (var i = 1; i < values.Length; i++)
            {
                var left = Math.Max(1, i - points / 2);
                var right = Math.Min(values.Length, i + points / 2 + 1);
                next[i] = (prefix[right] - prefix[left]) / (right - left);
            }
            // 光滑前后保持离散谱的总能量。
            var sum = next.Sum();
            var scale = sum > 0 ? values.Sum() / sum : 1;
            values = next.Select(x => x * scale).ToArray();
        }
        return values;
    }

    private static List<(double Height, double Period)> GetWaves(double[] values, double dt)
    {
        var crossings = new List<(int Index, double Time)>();
        for (var i = 1; i < values.Length; i++)
            if (values[i - 1] <= 0 && values[i] > 0)
                crossings.Add((i, (i - 1 - values[i - 1] / (values[i] - values[i - 1])) * dt));
        var waves = new List<(double, double)>();
        for (var i = 1; i < crossings.Count; i++)
        {
            var segment = values.AsSpan(crossings[i - 1].Index, crossings[i].Index - crossings[i - 1].Index);
            double low = 0, high = 0;
            foreach (var value in segment) { low = Math.Min(low, value); high = Math.Max(high, value); }
            waves.Add((high - low, crossings[i].Time - crossings[i - 1].Time));
        }
        return waves.OrderByDescending(x => x.Item1).ToList();
    }

    private static Dictionary<string, double> GetStatistics(List<(double Height, double Period)> waves)
    {
        var stats = new Dictionary<string, double>();
        foreach (var (name, divisor) in new[] { ("max", 0), ("1/100", 100), ("1/10", 10), ("1/3", 3), ("_ave", 1), ("min", -1) })
        {
            var selected = divisor == -1 ? waves.TakeLast(1).ToArray()
                : waves.Take(divisor == 0 ? 1 : Math.Max(1, (int)Math.Ceiling(waves.Count / (double)divisor))).ToArray();
            stats["H" + name + " (m)"] = selected.Length > 0 ? selected.Average(x => x.Height) : double.NaN;
            stats["TH" + name + " (s)"] = selected.Length > 0 ? selected.Average(x => x.Period) : double.NaN;
        }
        return stats;
    }
}
