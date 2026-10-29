using System.Globalization;
using System.Text;
using System.Text.Json;

namespace Page_switching;

// 统一管理标定、采集文件和任务索引；各页面共用这一份工作区。
public sealed class WaveDataWorkspace : IDisposable
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };
    private readonly object _gate = new();
    private readonly Dictionary<int, WaveCalibrationProfile> _profiles = new();
    private readonly int?[] _latestRaw = new int?[7];
    private StreamWriter? _activeWriter;
    private WaveCaptureManifest? _activeManifest;

    public static WaveDataWorkspace Shared { get; } = new();
    public string RootDirectory { get; }
    public string? CalibrationLoadError { get; private set; }
    public event EventHandler<WaveSample>? SampleRecorded;
    public WaveCaptureManifest? ActiveSession
    {
        get { lock (_gate) return _activeManifest; }
    }

    private WaveDataWorkspace()
    {
        RootDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "PageSwitching", "WaveHeight");
        Directory.CreateDirectory(RootDirectory);
        var path = Path.Combine(RootDirectory, "calibrations.json");
        if (File.Exists(path))
        {
            try
            {
                foreach (var profile in JsonSerializer.Deserialize<List<WaveCalibrationProfile>>(File.ReadAllText(path)) ?? [])
                {
                    if (profile.Channel is >= 1 and <= 6)
                        _profiles[profile.Channel] = profile;
                }
            }
            catch (Exception ex)
            {
                CalibrationLoadError = "标定配置读取失败，原文件已保留：" + ex.Message;
            }
        }
    }

    // 标定管理：返回副本，保存成功后才替换工作区内的配置。
    public WaveCalibrationProfile GetCalibration(int channel)
    {
        ValidateChannel(channel);
        lock (_gate)
        {
            return _profiles.TryGetValue(channel, out var profile)
                ? CloneProfile(profile)
                : new WaveCalibrationProfile { Channel = channel };
        }
    }

    public int? GetLatestRaw(int channel)
    {
        ValidateChannel(channel);
        lock (_gate) return _latestRaw[channel];
    }

    public void SaveCalibration(WaveCalibrationProfile profile)
    {
        ValidateChannel(profile.Channel);
        if (profile.Points.Count < 2 || !double.IsFinite(profile.Slope) || !double.IsFinite(profile.Intercept))
            throw new InvalidOperationException("请先完成有效的标定计算。");
        lock (_gate)
            SaveProfiles([profile]);
    }

    internal void SaveBatchCalibration(IReadOnlyList<WaveCalibrationProfile> profiles)
    {
        if (profiles.Count == 0 || profiles.Select(x => x.Channel).Distinct().Count() != profiles.Count)
            throw new InvalidOperationException("请至少选择一个不同的通道。");
        foreach (var profile in profiles)
        {
            ValidateChannel(profile.Channel);
            if (profile.Points.Count < 2 || !double.IsFinite(profile.Slope) || !double.IsFinite(profile.Intercept))
                throw new InvalidOperationException($"CH{profile.Channel}标定系数无效。");
        }
        lock (_gate) SaveProfiles(profiles);
    }

    // 采集会话：写入和结束都受同一把锁保护，避免释放正在使用的文件。
    public WaveCaptureManifest StartSession(string deviceAddress, int sampleRateHz, IReadOnlyCollection<int> channels)
    {
        if (channels.Count != 1 || !channels.Contains(1))
            throw new NotSupportedException("通道 2–6 的设备协议尚未确认，当前只允许通道 1 实采。");
        lock (_gate)
        {
            if (_activeManifest is not null) throw new InvalidOperationException("已有采集任务正在运行。");
            var id = Guid.NewGuid();
            var folder = Path.Combine(RootDirectory, "sessions", id.ToString("N"));
            Directory.CreateDirectory(folder);
            var manifest = new WaveCaptureManifest
            {
                RunId = DatabaseArchive.CurrentRunId,
                SessionId = id,
                DeviceAddress = deviceAddress,
                StartedAt = DateTimeOffset.Now,
                SampleRateHz = sampleRateHz,
                Channels = [1],
                CalibrationVersion = _profiles.TryGetValue(1, out var profile) ? profile.Version : "",
                CsvPath = Path.Combine(folder, "samples.csv")
            };
            // 文件和会话信息都保存成功后，才发布活动状态。
            var writer = new StreamWriter(manifest.CsvPath, false, new UTF8Encoding(true));
            try
            {
                writer.WriteLine("Timestamp,Channel,RawCount,CalibratedValue,CalibrationVersion");
                writer.Flush();
                SaveManifest(manifest);
            }
            catch
            {
                writer.Dispose();
                throw;
            }
            _activeWriter = writer;
            _activeManifest = manifest;
            return manifest;
        }
    }

    public WaveSample RecordRawSample(int channel, DateTimeOffset timestamp, int rawCount)
    {
        ValidateChannel(channel);
        WaveSample sample;
        lock (_gate)
        {
            _latestRaw[channel] = rawCount;
            _profiles.TryGetValue(channel, out var profile);
            var manifest = _activeManifest;
            sample = new WaveSample(manifest?.SessionId ?? Guid.Empty, channel, timestamp, rawCount,
                profile?.Convert(rawCount), profile?.Version ?? "");
            if (manifest is not null && manifest.Channels.Contains(channel))
            {
                _activeWriter!.WriteLine(SerializeSample(sample));
                manifest.SampleCount++;
                if (manifest.SampleCount % 50 == 0)
                {
                    _activeWriter.Flush();
                    SaveManifest(manifest);
                }
            }
        }
        if (SampleRecorded is { } handlers)
        {
            foreach (EventHandler<WaveSample> handler in handlers.GetInvocationList())
            {
                try { handler(this, sample); }
                catch (Exception ex) { System.Diagnostics.Debug.WriteLine("采集页面刷新失败：" + ex); }
            }
        }
        return sample;
    }

    public WaveCaptureManifest? EndSession()
    {
        lock (_gate)
        {
            var manifest = _activeManifest;
            if (manifest is null) return null;
            try
            {
                // Dispose 会刷新剩余样本；写入失败仍应释放本次会话。
                _activeWriter?.Dispose();
                manifest.EndedAt = DateTimeOffset.Now;
                SaveManifest(manifest);
            }
            finally
            {
                _activeWriter = null;
                _activeManifest = null;
            }
            return manifest;
        }
    }

    public void FlushActiveSession()
    {
        lock (_gate)
        {
            _activeWriter?.Flush();
            if (_activeManifest is not null) SaveManifest(_activeManifest);
        }
    }

    public IReadOnlyList<WaveCaptureManifest> ListSessions() =>
        Directory.Exists(Path.Combine(RootDirectory, "sessions"))
            ? Directory.EnumerateFiles(Path.Combine(RootDirectory, "sessions"), "session.json",
                    SearchOption.AllDirectories)
                .Select(path =>
                {
                    try { return JsonSerializer.Deserialize<WaveCaptureManifest>(File.ReadAllText(path)); }
                    catch { return null; }
                })
                .Where(x => x is not null).Cast<WaveCaptureManifest>()
                .OrderByDescending(x => x.StartedAt).ToList()
            : [];

    public IReadOnlyList<WaveSample> ReadSamples(WaveCaptureManifest manifest, int? channel = null, int maxRows = 100000)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxRows);
        return EnumerateSamples(manifest, channel).Take(maxRows).ToList();
    }

    public IReadOnlyList<WaveSample> ReadRecentSamples(WaveCaptureManifest manifest, int? channel = null, int maxRows = 1000)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxRows);
        var recent = new Queue<WaveSample>(maxRows);
        foreach (var sample in EnumerateSamples(manifest, channel))
        {
            if (recent.Count == maxRows) recent.Dequeue();
            recent.Enqueue(sample);
        }
        return recent.ToArray();
    }

    // 旧格式导入导出：保留原始单位和通道，不补造缺失的实采数据。
    public WaveCaptureManifest ImportLegacyCapture(string path)
    {
        var lines = File.ReadAllLines(path);
        if (lines.Length < 3) throw new InvalidDataException("旧采集文件缺少通道或采样数据。");
        var header = lines[0].Split('\t', StringSplitOptions.RemoveEmptyEntries);
        if (header.Length < 3 || !int.TryParse(header[0], out var count) || count is < 1 or > 6)
            throw new InvalidDataException("无法识别旧采集文件的通道数。");
        var channelFields = lines[1].Split('\t', StringSplitOptions.RemoveEmptyEntries);
        if (channelFields.Length < count) throw new InvalidDataException("旧采集文件的通道列不完整。");
        var channels = channelFields.Take(count).Select(x => int.Parse(x, CultureInfo.InvariantCulture)).ToArray();
        if (channels.Any(x => x is < 1 or > 6) || channels.Distinct().Count() != channels.Length)
            throw new InvalidDataException("旧采集文件的通道号无效。");
        // 与分析页面采用同一规则，不把损坏文件导入成正常采集记录。
        if (!int.TryParse(header[1], out var declaredRows) || declaredRows <= 0 || declaredRows != lines.Length - 2)
            throw new InvalidDataException("旧文件声明的样本数与实际行数不一致。");
        if (!TryLegacyDouble(header[2], out var interval) || !double.IsFinite(interval) || interval <= 0 ||
            !double.IsFinite(1.0 / interval) || 1.0 / interval > int.MaxValue)
            throw new InvalidDataException("旧采集文件的采样间隔无效。");
        var id = Guid.NewGuid();
        var folder = Path.Combine(RootDirectory, "sessions", id.ToString("N"));
        Directory.CreateDirectory(folder);
        var manifest = new WaveCaptureManifest
        {
            SessionId = id, DeviceAddress = "旧文件导入", StartedAt = DateTimeOffset.Now,
            Channels = channels, CsvPath = Path.Combine(folder, "samples.csv"), IsLegacyImport = true
        };
        manifest.SampleRateHz = (int)Math.Round(1.0 / interval);
        using (var writer = new StreamWriter(manifest.CsvPath, false, new UTF8Encoding(true)))
        {
            writer.WriteLine("Timestamp,Channel,RawCount,CalibratedValue,CalibrationVersion");
            for (var row = 2; row < lines.Length; row++)
            {
                var values = lines[row].Split('\t', StringSplitOptions.RemoveEmptyEntries);
                if (values.Length < count) throw new InvalidDataException($"旧采集文件第 {row + 1} 行列数不足。");
                for (var col = 0; col < count; col++)
                {
                    if (!TryLegacyDouble(values[col], out var value) || !double.IsFinite(value))
                        throw new InvalidDataException($"旧采集文件第 {row + 1} 行数值无效。");
                    var timestamp = manifest.StartedAt.AddSeconds(Math.Max(0, row - 2) * interval);
                    writer.WriteLine(SerializeSample(new WaveSample(id, channels[col], timestamp, null, value, "")));
                    manifest.SampleCount++;
                }
            }
        }
        manifest.EndedAt = DateTimeOffset.Now;
        SaveManifest(manifest);
        return manifest;
    }

    public void ImportLegacyCalibration(string path)
    {
        var rows = File.ReadAllLines(path).Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Split(',')).ToList();
        if (rows.Count < 3) throw new InvalidDataException("旧标定文件至少需要表头、标定点和系数行。");
        var imported = new List<WaveCalibrationProfile>();
        for (var col = 0; col < rows[0].Length; col += 2)
        {
            if (!rows[0][col].Trim().StartsWith("COM", StringComparison.OrdinalIgnoreCase)) continue;
            if (!int.TryParse(rows[0][col].Trim()[3..], out var channel) || channel is < 1 or > 6)
                continue;
            var profile = new WaveCalibrationProfile { Channel = channel };
            for (var row = 1; row < rows.Count - 1; row++)
            {
                if (rows[row].Length <= col + 1) continue;
                if (TryLegacyDouble(rows[row][col], out var raw) && TryLegacyDouble(rows[row][col + 1], out var level))
                    profile.Points.Add(new WaveCalibrationPoint(raw, level));
            }
            var last = rows[^1];
            if (last.Length <= col + 1 || !TryLegacyDouble(last[col], out var slope) ||
                !TryLegacyDouble(last[col + 1], out var intercept))
                throw new InvalidDataException($"COM{channel} 缺少有效的末行系数。");
            profile.Slope = slope;
            profile.Intercept = intercept;
            if (profile.Points.Count < 2) throw new InvalidDataException($"COM{channel} 标定点不足。");
            imported.Add(profile);
        }
        if (imported.Count == 0) throw new InvalidDataException("没有找到 COM1–COM6 标定列。");
        lock (_gate)
            SaveProfiles(imported);
    }

    public void ExportLegacyCalibration(string path)
    {
        List<WaveCalibrationProfile> profiles;
        lock (_gate) profiles = _profiles.Values.OrderBy(x => x.Channel).Select(CloneProfile).ToList();
        if (profiles.Count == 0) throw new InvalidOperationException("没有可导出的标定数据。");
        using var writer = new StreamWriter(path, false, new UTF8Encoding(false));
        writer.WriteLine(string.Join(",", profiles.SelectMany(p => new[] { $"COM{p.Channel}", "" })));
        for (var row = 0; row < profiles.Max(p => p.Points.Count); row++)
            writer.WriteLine(string.Join(",", profiles.SelectMany(p =>
            {
                // 各 COM 列对的系数必须位于同一行，点数不足时沿用最后一个标定点。
                var point = p.Points[Math.Min(row, p.Points.Count - 1)];
                return new[] { Format(point.RawCount), Format(point.WaterLevel) };
            })));
        writer.WriteLine(string.Join(",", profiles.SelectMany(p => new[]
        {
            Format(p.Slope), Format(p.Intercept - p.ZeroRawCount * p.Slope)
        })));
    }

    internal void ExportLegacyCapture(WaveCaptureManifest manifest, string path, int? channel)
    {
        if (manifest.SampleRateHz <= 0) throw new InvalidOperationException("任务没有有效的采样频率，不能导出旧格式。");
        var channels = channel.HasValue ? new[] { channel.Value } : manifest.Channels;
        if (channels.Length == 0 || channels.Any(x => !manifest.Channels.Contains(x)))
            throw new InvalidOperationException("所选通道不属于此任务。");
        var counts = channels.ToDictionary(x => x, _ => 0L);
        foreach (var sample in EnumerateSamples(manifest, channel))
        {
            if (!counts.ContainsKey(sample.Channel)) continue;
            if (!sample.CalibratedValue.HasValue || !double.IsFinite(sample.CalibratedValue.Value))
                throw new InvalidOperationException("旧波浪文件只能保存有效的标定值，不能把原始计数当成波高。");
            counts[sample.Channel]++;
        }
        var count = counts.Values.First();
        if (count == 0 || counts.Values.Any(x => x != count))
            throw new InvalidOperationException("所选通道样本为空或数量不一致，不能组合成旧格式。");
        var destination = Path.GetFullPath(path);
        if (string.Equals(destination, Path.GetFullPath(manifest.CsvPath), StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("旧格式导出不能覆盖原始采集文件。");
        var temporary = destination + "." + Guid.NewGuid().ToString("N") + ".tmp";
        var readers = channels.Select(x => EnumerateSamples(manifest, x).GetEnumerator()).ToArray();
        try
        {
            using (var writer = new StreamWriter(temporary, false, new UTF8Encoding(false)))
            {
                writer.WriteLine($"{channels.Length}\t{count}\t{Format(1.0 / manifest.SampleRateHz)}");
                writer.WriteLine(string.Join("\t", channels));
                for (long row = 0; row < count; row++)
                {
                    var values = new string[readers.Length];
                    for (var col = 0; col < readers.Length; col++)
                    {
                        if (!readers[col].MoveNext()) throw new InvalidDataException("采集文件在导出过程中发生变化。");
                        values[col] = Format(readers[col].Current.CalibratedValue!.Value);
                    }
                    writer.WriteLine(string.Join("\t", values));
                }
            }
            File.Move(temporary, destination, true);
        }
        finally
        {
            foreach (var reader in readers) reader.Dispose();
            if (File.Exists(temporary)) File.Delete(temporary);
        }
    }

    // 手动同步与后台补传共用一个入口；SQL Server实现来自Total的DLL。
    public Task<int> SynchronizeSessionsAsync(CancellationToken cancellationToken = default) =>
        DatabaseSyncService.Shared.SynchronizeAsync(cancellationToken);

    internal void MarkDatabaseSync(WaveCaptureManifest manifest, string? error, string destination)
    {
        lock (_gate)
        {
            manifest.SyncStatus = error is null ? "已同步" : "待同步";
            manifest.SyncError = error;
            manifest.DatabaseDetailVersion = error is null ? 1 : 0;
            manifest.DatabaseTarget = destination;
            SaveManifest(manifest);
        }
    }

    public void Dispose() => EndSession();

    private void SaveProfiles(IEnumerable<WaveCalibrationProfile> profiles)
    {
        if (_activeManifest is not null)
            throw new InvalidOperationException("请先结束采集，再修改标定参数。");

        var merged = _profiles.ToDictionary(x => x.Key, x => CloneProfile(x.Value));
        foreach (var profile in profiles)
            merged[profile.Channel] = CloneProfile(profile);

        var path = Path.Combine(RootDirectory, "calibrations.json");
        BackupDamagedCalibration(path);
        WriteJsonAtomically(path, merged.Values.OrderBy(p => p.Channel).ToList());
        CalibrationLoadError = null;
        _profiles.Clear();
        foreach (var item in merged)
            _profiles[item.Key] = item.Value;
    }

    private static IEnumerable<WaveSample> EnumerateSamples(WaveCaptureManifest manifest, int? channel)
    {
        if (!File.Exists(manifest.CsvPath))
            throw new FileNotFoundException("采集文件不存在。", manifest.CsvPath);

        foreach (var line in File.ReadLines(manifest.CsvPath).Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var sample = ParseSample(manifest.SessionId, line);
            if (!channel.HasValue || channel.Value == sample.Channel)
                yield return sample;
        }
    }

    private static WaveCalibrationProfile CloneProfile(WaveCalibrationProfile profile) => new()
    {
        Channel = profile.Channel, ZeroRawCount = profile.ZeroRawCount,
        Slope = profile.Slope, Intercept = profile.Intercept, Version = profile.Version,
        Points = [.. profile.Points]
    };

    private static string SerializeSample(WaveSample sample) =>
        string.Join(",", sample.Timestamp.ToString("O", CultureInfo.InvariantCulture),
            sample.Channel.ToString(CultureInfo.InvariantCulture),
            sample.RawCount?.ToString(CultureInfo.InvariantCulture) ?? "",
            sample.CalibratedValue?.ToString("R", CultureInfo.InvariantCulture) ?? "",
            sample.CalibrationVersion);

    private static WaveSample ParseSample(Guid sessionId, string line)
    {
        var fields = line.Split(',');
        if (fields.Length < 5 || !int.TryParse(fields[1], out var channel))
            throw new InvalidDataException("采集文件包含无效的数据行。");
        return new WaveSample(sessionId, channel,
            DateTimeOffset.Parse(fields[0], CultureInfo.InvariantCulture),
            int.TryParse(fields[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out var raw) ? raw : null,
            double.TryParse(fields[3], NumberStyles.Float, CultureInfo.InvariantCulture, out var calibrated)
                ? calibrated : null, fields[4]);
    }

    private static bool TryLegacyDouble(string value, out double parsed) =>
        double.TryParse(value.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out parsed) ||
        double.TryParse(value.Trim(), NumberStyles.Float, CultureInfo.CurrentCulture, out parsed);

    private static string Format(double value) => value.ToString("R", CultureInfo.InvariantCulture);
    private static void ValidateChannel(int channel)
    {
        if (channel is < 1 or > 6) throw new ArgumentOutOfRangeException(nameof(channel));
    }

    private static void SaveManifest(WaveCaptureManifest manifest) =>
        WriteJsonAtomically(Path.Combine(Path.GetDirectoryName(manifest.CsvPath)!, "session.json"), manifest);

    private void BackupDamagedCalibration(string path)
    {
        if (CalibrationLoadError is not null && File.Exists(path))
            File.Copy(path, path + ".damaged." + DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture));
    }

    private static void WriteJsonAtomically<T>(string path, T value)
    {
        var temp = path + ".tmp";
        File.WriteAllText(temp, JsonSerializer.Serialize(value, JsonOptions), new UTF8Encoding(false));
        File.Move(temp, path, true);
    }
}
