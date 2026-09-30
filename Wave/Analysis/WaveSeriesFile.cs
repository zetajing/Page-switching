using System.Globalization;
using System.Text;

namespace Page_switching;

internal sealed record WaveSeries(double[] Values, double Interval, int Channel);

// 分析统一使用米。旧文件没有单位字段，由导入界面明确选择。
internal static class WaveSeriesFile
{
    internal static double Number(string value)
    {
        if ((!double.TryParse(value.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var result) &&
             !double.TryParse(value.Trim(), NumberStyles.Float, CultureInfo.CurrentCulture, out result)) ||
            !double.IsFinite(result))
            throw new InvalidDataException("文件包含无效数值：" + value);
        return result;
    }

    internal static string Format(double value) => value.ToString("R", CultureInfo.InvariantCulture);
    internal static string[] Fields(string line) => line.Trim().Split(['\t', ','], StringSplitOptions.TrimEntries);

    internal static int Integer(string value)
    {
        var number = Number(value);
        if (number != Math.Truncate(number) || number < int.MinValue || number > int.MaxValue)
            throw new InvalidDataException("文件中的通道号或点数必须为整数：" + value);
        return (int)number;
    }

    internal static WaveSeries Read(string path, int channel, bool legacyMillimetres, double csvSampleRate = 0)
    {
        var lines = File.ReadAllLines(path).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        if (lines.Length < 3) throw new InvalidDataException("文件缺少采样数据。");
        var header = Fields(lines[0]);
        if (header.Contains("Timestamp") && header.Contains("CalibratedValue"))
        {
            var manifestPath = Path.Combine(Path.GetDirectoryName(Path.GetFullPath(path))!, "session.json");
            if (!File.Exists(manifestPath)) manifestPath = path + ".session.json";
            var session = File.Exists(manifestPath)
                ? System.Text.Json.JsonSerializer.Deserialize<WaveCaptureManifest>(File.ReadAllText(manifestPath)) : null;
            var scale = session?.IsLegacyImport == true ? (legacyMillimetres ? 0.001 : 1) : 0.001;
            var values = new List<double>();
            var times = new List<double>();
            DateTimeOffset? first = null;
            foreach (var line in lines.Skip(1))
            {
                var row = Fields(line);
                if (row.Length < 5) throw new InvalidDataException("采集记录列数不完整。");
                var sampleChannel = Integer(row[1]);
                if (sampleChannel is < 1 or > 6) throw new InvalidDataException("采集记录通道号必须为1–6。");
                if (sampleChannel != channel) continue;
                if (string.IsNullOrEmpty(row[3]))
                    throw new InvalidDataException("所选通道有未标定的原始计数，不能当作真实波高分析。");
                var time = DateTimeOffset.Parse(row[0], CultureInfo.InvariantCulture);
                first ??= time;
                times.Add((time - first.Value).TotalSeconds);
                values.Add(Number(row[3]) * scale);
            }
            if (session is not null && session.SampleRateHz > 0) csvSampleRate = session.SampleRateHz;
            if (csvSampleRate > 0)
            {
                if (values.Count < 3) throw new InvalidDataException("所选通道样本不足。");
                return new WaveSeries(values.ToArray(), 1 / csvSampleRate, channel);
            }
            return Create(values, times, channel);
        }
        if (header.Length >= 2 && header[0] == "TimeSeconds" && header[1] == "ElevationMetres")
        {
            if (channel != 1) throw new InvalidDataException("这个单列波形文件只有通道1。");
            var rows = lines.Skip(1).Select(Fields).ToArray();
            return Create(rows.Select(x => Number(x[1])).ToList(), rows.Select(x => Number(x[0])).ToList(), 1);
        }
        // 旧采集文件：通道数、样本数、间隔；下一行是通道号；后面是水位。
        if (header.Length < 3) throw new InvalidDataException("无法识别文件格式。");
        var count = Integer(header[0]);
        var interval = Number(header[2]);
        if (count is < 1 or > 6 || interval <= 0)
            throw new InvalidDataException("旧文件的通道数或采样间隔无效。");
        var channels = Fields(lines[1]).Take(count).Select(Integer).ToArray();
        if (channels.Length != count || channels.Any(x => x is < 1 or > 6) || channels.Distinct().Count() != count)
            throw new InvalidDataException("旧文件的通道号应为不重复的1–6，且与声明的通道数一致。");
        var column = Array.IndexOf(channels, channel);
        if (column < 0) throw new InvalidDataException($"文件没有通道 {channel}。");
        var samples = lines.Skip(2).Select(line =>
        {
            var row = Fields(line);
            if (row.Length < count) throw new InvalidDataException("旧采集文件缺少通道列。");
            return Number(row[column]) * (legacyMillimetres ? 0.001 : 1);
        }).ToArray();
        if (samples.Length != Integer(header[1]))
            throw new InvalidDataException("旧文件声明的样本数与实际行数不一致。");
        return new WaveSeries(samples, interval, channel);
    }

    private static WaveSeries Create(List<double> values, List<double> times, int channel)
    {
        if (values.Count < 3) throw new InvalidDataException("所选通道至少需要三个有效样本。");
        var intervals = times.Zip(times.Skip(1), (a, b) => b - a).ToArray();
        if (intervals.Any(x => x <= 0)) throw new InvalidDataException("采样时间必须严格递增。");
        var interval = intervals.Order().ElementAt(intervals.Length / 2);
        // TCP 收包时间可能成批到达；不能把不均匀时间序列直接交给 FFT。
        if (intervals.Any(x => Math.Abs(x - interval) > interval * 0.2))
            throw new InvalidDataException("采样时间不均匀，请使用会话采样频率导出等间隔序列后分析。");
        return new WaveSeries(values.ToArray(), interval, channel);
    }

    internal static void WriteLegacy(string path, IReadOnlyList<WaveSeries> channels)
    {
        if (channels.Count == 0 || channels.Any(x => x.Values.Length != channels[0].Values.Length ||
                Math.Abs(x.Interval - channels[0].Interval) > 1e-9))
            throw new InvalidOperationException("各通道必须有相同的样本数与采样间隔。");
        using var writer = new StreamWriter(path, false, new UTF8Encoding(false));
        writer.WriteLine($"{channels.Count}\t{channels[0].Values.Length}\t{Format(channels[0].Interval)}");
        writer.WriteLine(string.Join("\t", channels.Select(x => x.Channel)));
        for (var i = 0; i < channels[0].Values.Length; i++)
            writer.WriteLine(string.Join("\t", channels.Select(x => Format(x.Values[i]))));
    }
}
