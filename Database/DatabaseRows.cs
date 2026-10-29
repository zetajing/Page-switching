using System.Globalization;

namespace Page_switching;

// 顺序读取文件，最多由写库方缓存一批，不把整场数据放入DataTable。
internal static class DatabaseRows
{
    internal static IEnumerable<object[]> AxisSamples(Guid monitorId, string path)
    {
        using var reader = new StreamReader(path);
        bool header = true; long index = 0;
        foreach (var fields in Csv(reader))
        {
            if (header) { header = false; if (fields.Length != 16 || fields[0] != "ReadCompletedAt") throw new InvalidDataException("轴采样CSV表头不符。"); continue; }
            if (fields.Length != 16) throw new InvalidDataException("轴采样CSV字段数不符。");
            var row = new object[18];
            row[0] = monitorId; row[1] = index++;
            row[2] = DateTimeOffset.Parse(fields[0], CultureInfo.InvariantCulture);
            row[3] = double.Parse(fields[1], CultureInfo.InvariantCulture);
            row[4] = Integer(fields[2]); row[5] = Integer(fields[3]); row[6] = SmallInteger(fields[4]);
            row[7] = Boolean(fields[5]); row[8] = Boolean(fields[6]); row[9] = fields[7];
            for (int i = 0; i < 8; i++) row[10 + i] = Number(fields[8 + i]);
            yield return row;
        }
    }

    internal static IEnumerable<object[]> SensorSamples(WaveCaptureManifest manifest)
    {
        using var reader = new StreamReader(manifest.CsvPath);
        bool header = true; long index = 0;
        foreach (var fields in Csv(reader))
        {
            if (header) { header = false; if (fields.Length != 5 || fields[0] != "Timestamp") throw new InvalidDataException("传感器CSV表头不符。"); continue; }
            if (fields.Length != 5) throw new InvalidDataException("传感器CSV字段数不符。");
            yield return new object[] { manifest.SessionId, index++, DateTimeOffset.Parse(fields[0], CultureInfo.InvariantCulture),
                int.Parse(fields[1], CultureInfo.InvariantCulture), SmallInteger(fields[2]), Number(fields[3]), fields[4] };
        }
        if (index != manifest.SampleCount) throw new InvalidDataException("传感器样本数与任务记录不一致，未标记同步成功。");
    }

    // 支持引号、逗号及错误信息中的换行；不能用Split(',')读取轴监测CSV。
    internal static IEnumerable<string[]> Csv(TextReader reader)
    {
        var fields = new List<string>(); var field = new System.Text.StringBuilder(); bool quoted = false, hasValue = false;
        while (true)
        {
            int read = reader.Read();
            if (read < 0)
            {
                if (quoted) throw new InvalidDataException("CSV引号未闭合。");
                if (hasValue) { fields.Add(field.ToString()); yield return fields.ToArray(); }
                yield break;
            }
            char c = (char)read; hasValue = true;
            if (c == '"')
            {
                if (quoted && reader.Peek() == '"') { reader.Read(); field.Append('"'); }
                else quoted = !quoted;
            }
            else if (c == ',' && !quoted) { fields.Add(field.ToString()); field.Clear(); }
            else if ((c == '\r' || c == '\n') && !quoted)
            {
                if (c == '\r' && reader.Peek() == '\n') reader.Read();
                fields.Add(field.ToString()); yield return fields.ToArray(); fields.Clear(); field.Clear(); hasValue = false;
            }
            else field.Append(c);
        }
    }
    private static object Integer(string value) => value.Length == 0 ? DBNull.Value : long.Parse(value, CultureInfo.InvariantCulture);
    private static object SmallInteger(string value) => value.Length == 0 ? DBNull.Value : int.Parse(value, CultureInfo.InvariantCulture);
    private static object Boolean(string value) => value.Length == 0 ? DBNull.Value : bool.Parse(value);
    private static object Number(string value)
    {
        if (value.Length == 0) return DBNull.Value;
        double number = double.Parse(value, CultureInfo.InvariantCulture);
        return double.IsFinite(number) ? number : DBNull.Value;
    }
}
