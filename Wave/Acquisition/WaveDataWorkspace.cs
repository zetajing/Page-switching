using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace Page_switching;

public sealed record WaveCalibrationPoint(double RawCount, double WaterLevel);

public sealed class WaveCalibrationProfile
{
    public int Channel { get; set; }
    public double ZeroRawCount { get; set; }
    public double Slope { get; set; } = 1;
    public double Intercept { get; set; }
    public string Version { get; set; } = Guid.NewGuid().ToString("N");
    public List<WaveCalibrationPoint> Points { get; set; } = [];

    public double Convert(int rawCount) => (rawCount - ZeroRawCount) * Slope + Intercept;

    public void Fit()
    {
        if (Points.Count < 2)
            throw new InvalidOperationException("至少需要两个标定点。");
        var meanX = Points.Average(p => p.RawCount - ZeroRawCount);
        var meanY = Points.Average(p => p.WaterLevel);
        var denominator = Points.Sum(p => Math.Pow(p.RawCount - ZeroRawCount - meanX, 2));
        if (denominator < 1e-12)
            throw new InvalidOperationException("标定点的原始值不能全部相同。");
        Slope = Points.Sum(p => (p.RawCount - ZeroRawCount - meanX) * (p.WaterLevel - meanY)) / denominator;
        Intercept = meanY - Slope * meanX;
        Version = Guid.NewGuid().ToString("N");
    }
}

public sealed record WaveSample(
    Guid SessionId, int Channel, DateTimeOffset Timestamp, int? RawCount,
    double? CalibratedValue, string CalibrationVersion);

public sealed class WaveCaptureManifest
{
    public Guid SessionId { get; set; }
    public string DeviceAddress { get; set; } = "";
    public DateTimeOffset StartedAt { get; set; }
    public DateTimeOffset? EndedAt { get; set; }
    public int SampleRateHz { get; set; }
    public int[] Channels { get; set; } = [];
    public long SampleCount { get; set; }
    public string CalibrationVersion { get; set; } = "";
    public string CsvPath { get; set; } = "";
    public string SyncStatus { get; set; } = "待同步";
    public string? SyncError { get; set; }
    public bool IsLegacyImport { get; set; }
}

/// <summary>Shared local source of truth for capture, calibration, and session browsing.</summary>
public sealed class WaveDataWorkspace : IDisposable
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };
    private readonly object _gate = new();
    private readonly SemaphoreSlim _syncGate = new(1, 1);
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
                SessionId = id,
                DeviceAddress = deviceAddress,
                StartedAt = DateTimeOffset.Now,
                SampleRateHz = sampleRateHz,
                Channels = [1],
                CalibrationVersion = _profiles.TryGetValue(1, out var profile) ? profile.Version : "",
                CsvPath = Path.Combine(folder, "samples.csv")
            };
            _activeWriter = new StreamWriter(manifest.CsvPath, false, new UTF8Encoding(true));
            _activeWriter.WriteLine("Timestamp,Channel,RawCount,CalibratedValue,CalibrationVersion");
            _activeWriter.Flush();
            _activeManifest = manifest;
            SaveManifest(manifest);
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
            _activeWriter?.Flush();
            _activeWriter?.Dispose();
            _activeWriter = null;
            manifest.EndedAt = DateTimeOffset.Now;
            SaveManifest(manifest);
            _activeManifest = null;
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
        var id = Guid.NewGuid();
        var folder = Path.Combine(RootDirectory, "sessions", id.ToString("N"));
        Directory.CreateDirectory(folder);
        var manifest = new WaveCaptureManifest
        {
            SessionId = id, DeviceAddress = "旧文件导入", StartedAt = DateTimeOffset.Now,
            Channels = channels, CsvPath = Path.Combine(folder, "samples.csv"), IsLegacyImport = true
        };
        var interval = double.TryParse(header[2], NumberStyles.Float, CultureInfo.CurrentCulture, out var parsed)
            ? parsed : 0;
        if (interval > 0)
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
                    if (!double.TryParse(values[col], NumberStyles.Float, CultureInfo.CurrentCulture, out var value))
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
                // The old parser expects the coefficient row at the same index for every COM pair.
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

    public async Task<int> SynchronizeSessionsAsync(CancellationToken cancellationToken = default)
    {
        await _syncGate.WaitAsync(cancellationToken);
        try { return await SynchronizeSessionsCoreAsync(cancellationToken); }
        finally { _syncGate.Release(); }
    }

    private async Task<int> SynchronizeSessionsCoreAsync(CancellationToken cancellationToken)
    {
        if (!bool.TryParse(ConfigurationManager.AppSettings["DatabaseEnabled"], out var enabled) || !enabled)
            return 0;
        var connectionString = ConfigurationManager.AppSettings["DatabaseConnectionString"];
        if (string.IsNullOrWhiteSpace(connectionString)) throw new InvalidOperationException("数据库连接字符串未配置。");
        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        const string schema = """
            IF OBJECT_ID(N'dbo.WaveCaptureSessions', N'U') IS NULL
            CREATE TABLE dbo.WaveCaptureSessions (
                SessionId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                DeviceAddress NVARCHAR(128) NOT NULL,
                StartedAt DATETIMEOFFSET NOT NULL,
                EndedAt DATETIMEOFFSET NULL,
                SampleRateHz INT NOT NULL,
                Channels NVARCHAR(64) NOT NULL,
                SampleCount BIGINT NOT NULL,
                CalibrationVersion NVARCHAR(64) NOT NULL,
                CsvPath NVARCHAR(1024) NOT NULL,
                IsLegacyImport BIT NOT NULL
            );
            """;
        using (var create = new SqlCommand(schema, connection))
            await create.ExecuteNonQueryAsync(cancellationToken);
        var synced = 0;
        foreach (var manifest in ListSessions().Where(x => x.EndedAt.HasValue && x.SyncStatus != "已同步"))
        {
            try
            {
                const string sql = """
                    UPDATE dbo.WaveCaptureSessions SET
                      DeviceAddress=@DeviceAddress, StartedAt=@StartedAt, EndedAt=@EndedAt,
                      SampleRateHz=@SampleRateHz, Channels=@Channels, SampleCount=@SampleCount,
                      CalibrationVersion=@CalibrationVersion, CsvPath=@CsvPath, IsLegacyImport=@IsLegacyImport
                    WHERE SessionId=@SessionId;
                    IF @@ROWCOUNT=0
                    INSERT INTO dbo.WaveCaptureSessions
                      (SessionId, DeviceAddress, StartedAt, EndedAt, SampleRateHz, Channels,
                       SampleCount, CalibrationVersion, CsvPath, IsLegacyImport)
                    VALUES (@SessionId, @DeviceAddress, @StartedAt, @EndedAt, @SampleRateHz,
                            @Channels, @SampleCount, @CalibrationVersion, @CsvPath, @IsLegacyImport);
                    """;
                using var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@SessionId", SqlDbType.UniqueIdentifier).Value = manifest.SessionId;
                command.Parameters.Add("@DeviceAddress", SqlDbType.NVarChar, 128).Value = manifest.DeviceAddress;
                command.Parameters.Add("@StartedAt", SqlDbType.DateTimeOffset).Value = manifest.StartedAt;
                command.Parameters.Add("@EndedAt", SqlDbType.DateTimeOffset).Value = manifest.EndedAt!.Value;
                command.Parameters.Add("@SampleRateHz", SqlDbType.Int).Value = manifest.SampleRateHz;
                command.Parameters.Add("@Channels", SqlDbType.NVarChar, 64).Value = string.Join(",", manifest.Channels);
                command.Parameters.Add("@SampleCount", SqlDbType.BigInt).Value = manifest.SampleCount;
                command.Parameters.Add("@CalibrationVersion", SqlDbType.NVarChar, 64).Value = manifest.CalibrationVersion;
                command.Parameters.Add("@CsvPath", SqlDbType.NVarChar, 1024).Value = manifest.CsvPath;
                command.Parameters.Add("@IsLegacyImport", SqlDbType.Bit).Value = manifest.IsLegacyImport;
                await command.ExecuteNonQueryAsync(cancellationToken);
                manifest.SyncStatus = "已同步";
                manifest.SyncError = null;
                SaveManifest(manifest);
                synced++;
            }
            catch (Exception ex)
            {
                manifest.SyncStatus = "待同步";
                manifest.SyncError = ex.Message;
                SaveManifest(manifest);
            }
        }
        return synced;
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
