using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Page_switching;

internal sealed record DatabaseOperation(Guid Id, DateTimeOffset At, string UserName, ushort Station,
    Guid? RunId, string Page, string Message);
internal sealed record DatabaseWaveData(string DataId, string FileName, int PointCount, double SampleTime, DateTimeOffset CreatedAt);
internal sealed record DatabaseWaveRun(Guid RunId, string? DataId, Guid? MonitorRunId, string UserName, ushort Station,
    uint Sequence, DateTimeOffset StartedAt, DateTimeOffset? EndedAt, string Status, string? Error);

// 本地归档只负责文件，不访问SQL；数据库离线时仍保留可重放的数据。
internal static class DatabaseArchive
{
    private static readonly object Gate = new();
    private static volatile bool _active;
    private static string _user = "未登录";
    private static ushort _station;
    private static Guid? _runId;
    internal static string Root { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PageSwitching", "DatabaseArchive");
    internal static string? LastError { get; private set; }

    internal static void Start(string user, ushort station)
    {
        lock (Gate) { _user = user; _station = station; _active = true; }
    }

    internal static void RecordOperation(DateTimeOffset at, string page, string message)
    {
        // 数据库自身错误只留在原操作日志中，防止失败日志反复产生入库任务。
        if (page == "数据库") return;
        lock (Gate)
        {
            if (!_active) return;
            try
            {
                string folder = Path.Combine(Root, "operations"); Directory.CreateDirectory(folder);
                var item = new DatabaseOperation(Guid.NewGuid(), at.ToUniversalTime(), _user, _station, _runId, page, message);
                File.AppendAllText(Path.Combine(folder, at.ToUniversalTime().ToString("yyyyMMdd-HH", CultureInfo.InvariantCulture) + ".jsonl"),
                    JsonSerializer.Serialize(item) + "\n", new UTF8Encoding(false));
            }
            catch (Exception ex) { ReportError(ex); }
        }
    }

    // 保存已经解析/预检的点位，而不是再读取可能已被修改的原文件。
    internal static async Task<string?> SaveWaveDataAsync(double[] points, string name, CancellationToken token)
    {
        if (!_active) return null;
        string? temporary = null;
        try
        {
            string folder = Path.Combine(Root, "waves"); Directory.CreateDirectory(folder);
            temporary = Path.Combine(folder, Guid.NewGuid().ToString("N") + ".tmp");
            await using (var stream = new FileStream(temporary, FileMode.CreateNew, FileAccess.Write, FileShare.None, 65536, true))
            {
                var buffer = new byte[65536];
                for (int offset = 0; offset < points.Length;)
                {
                    int count = Math.Min(buffer.Length / sizeof(double), points.Length - offset);
                    Buffer.BlockCopy(points, offset * sizeof(double), buffer, 0, count * sizeof(double));
                    await stream.WriteAsync(buffer.AsMemory(0, count * sizeof(double)), token).ConfigureAwait(false);
                    offset += count;
                }
            }
            string id;
            await using (var stream = File.OpenRead(temporary))
                id = Convert.ToHexString(await SHA256.HashDataAsync(stream, token).ConfigureAwait(false));
            // 各站可连同一数据库：本机来源也进入编号，避免两台电脑同时重放同一批点位。
            string source = Environment.MachineName + "|" + Path.GetFullPath(Root);
            id = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(source + "|" + id)));
            string target = Path.Combine(folder, id + ".bin");
            if (!File.Exists(target)) File.Move(temporary, target);
            else File.Delete(temporary);
            temporary = null;
            if (File.Exists(Path.Combine(folder, id + ".json"))) return id;
            await SaveJsonAsync(Path.Combine(folder, id + ".json"),
                new DatabaseWaveData(id, name, points.Length, WaveTrajectoryFile.SampleTime, DateTimeOffset.UtcNow), token).ConfigureAwait(false);
            return id;
        }
        catch (OperationCanceledException) when (token.IsCancellationRequested) { throw; }
        catch (Exception ex) { ReportError(ex); return null; }
        finally { if (temporary is not null) { try { File.Delete(temporary); } catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { System.Diagnostics.Debug.WriteLine(ex); } } }
    }

    internal static DatabaseWaveRun? BeginRun(string? dataId, Guid? monitorId, ushort station, uint sequence)
    {
        lock (Gate)
        {
            if (!_active) return null;
            var run = new DatabaseWaveRun(Guid.NewGuid(), dataId, monitorId, _user, station, sequence,
                DateTimeOffset.UtcNow, null, "等待PLC执行", null);
            try
            {
                SaveJsonAsync(RunPath(run.RunId), run, CancellationToken.None).GetAwaiter().GetResult();
                _runId = run.RunId;
                return run;
            }
            catch (Exception ex) { ReportError(ex); return null; }
        }
    }

    internal static void EndRun(DatabaseWaveRun? run, string status, string? error)
    {
        if (run is null) return;
        lock (Gate)
        {
            try { SaveJsonAsync(RunPath(run.RunId), run with { EndedAt = DateTimeOffset.UtcNow, Status = status, Error = error }, CancellationToken.None).GetAwaiter().GetResult(); }
            catch (Exception ex) { ReportError(ex); }
            finally { if (_runId == run.RunId) _runId = null; }
        }
    }

    internal static DatabaseWaveRun? AttachMonitor(DatabaseWaveRun? run, Guid monitorId)
    {
        if (run is null) return null;
        var linked = run with { MonitorRunId = monitorId };
        try { SaveJsonAsync(RunPath(run.RunId), linked, CancellationToken.None).GetAwaiter().GetResult(); }
        catch (Exception ex) { ReportError(ex); }
        return linked;
    }

    internal static Guid? CurrentRunId { get { lock (Gate) return _runId; } }
    internal static IReadOnlyList<DatabaseOperation> ReadOperations(string path, int offset, int count)
    {
        lock (Gate)
        {
            return File.ReadLines(path).Skip(offset).Take(count)
                .Select(line => JsonSerializer.Deserialize<DatabaseOperation>(line) ?? throw new InvalidDataException("日志归档为空。")).ToArray();
        }
    }
    private static string RunPath(Guid id) => Path.Combine(Root, "runs", id.ToString("N") + ".json");
    internal static async Task SaveJsonAsync<T>(string path, T value, CancellationToken token)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        string temporary = path + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            await File.WriteAllTextAsync(temporary, JsonSerializer.Serialize(value), token).ConfigureAwait(false);
            File.Move(temporary, path, true);
        }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }
    private static void ReportError(Exception ex)
    {
        LastError = "本地数据库归档失败：" + ex.Message;
        OperationJournal.Record("数据库", LastError);
    }
}
