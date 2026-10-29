using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using InduLink.Storage;

namespace Page_switching;

// 使用Total的存储接口逐批补传；本地游标仅在DLL确认写入成功后推进。
internal sealed class DatabaseSyncService
{
    internal static DatabaseSyncService Shared { get; } = new();
    private readonly SemaphoreSlim _gate = new(1, 1);
    private readonly CancellationTokenSource _lifetime = new();
    private readonly Func<int, string, IInduLinkHistoryStore> _open;
    private readonly Func<bool> _enabled;
    private readonly Func<string> _connection;
    internal string MonitoringRoot { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PageSwitching", "AxisMonitoring");
    private Task _worker = Task.CompletedTask;
    internal string Status { get; private set; } = "数据库未启用，本地文件继续保存。";
    internal DateTimeOffset? LastSuccess { get; private set; }

    internal DatabaseSyncService(Func<int, string, IInduLinkHistoryStore>? open = null,
        Func<bool>? enabled = null, Func<string>? connection = null)
    {
        _open = open ?? DatabaseStore.Open;
        _enabled = enabled ?? (() => DatabaseStore.Enabled);
        _connection = connection ?? (() => DatabaseStore.ConnectionString);
    }
    internal void Start() { if (_worker.IsCompleted) _worker = Task.Run(LoopAsync); }
    private async Task LoopAsync()
    {
        while (!_lifetime.IsCancellationRequested)
        {
            try { await Task.Delay(5000, _lifetime.Token).ConfigureAwait(false); await SynchronizeAsync(_lifetime.Token).ConfigureAwait(false); }
            catch (OperationCanceledException) when (_lifetime.IsCancellationRequested) { break; }
            catch (Exception ex) { Status = "同步失败，本地记录已保留：" + ex.Message; }
        }
    }
    internal async Task StopAsync()
    {
        _lifetime.Cancel();
        try { await _worker.WaitAsync(TimeSpan.FromSeconds(2)).ConfigureAwait(false); }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine("数据库有界退出：" + ex.Message); }
    }

    internal async Task<int> SynchronizeAsync(CancellationToken token = default)
    {
        using var linked = CancellationTokenSource.CreateLinkedTokenSource(token, _lifetime.Token);
        token = linked.Token;
        await _gate.WaitAsync(token).ConfigureAwait(false);
        var stores = new List<IInduLinkHistoryStore>();
        try
        {
            if (!_enabled()) { Status = "数据库未启用，本地文件继续保存。"; return 0; }
            Status = "正在同步本地归档……";
            string connection = _connection();
            // 游标按目标服务器/数据库隔离，改密码不触发整库重放。
            var target = new System.Data.SqlClient.SqlConnectionStringBuilder(connection);
            string destination = Hash(target.DataSource + "|" + target.InitialCatalog);
            string cursorRoot = Path.Combine(DatabaseArchive.Root, "sync", destination);
            Directory.CreateDirectory(cursorRoot);
            for (int kind = 0; kind < DatabaseStore.Names.Length; kind++)
            {
                var store = _open(kind, connection); stores.Add(store);
                await store.InitializeAsync(token).ConfigureAwait(false);
            }
            int count = 0, failures = 0; string? lastError = null;
            async Task Import(string path, Func<Task> save)
            {
                token.ThrowIfCancellationRequested();
                try { await save().ConfigureAwait(false); }
                catch (OperationCanceledException) when (token.IsCancellationRequested) { throw; }
                catch (Exception ex) { failures++; lastError = Path.GetFileName(path) + "：" + ex.Message; }
            }
            foreach (string path in Files(Path.Combine(DatabaseArchive.Root, "operations"), "*.jsonl"))
            {
                await Import(path, async () =>
                {
                    string marker = Path.Combine(cursorRoot, Hash(path) + ".lines");
                    int offset = File.Exists(marker) ? int.Parse(await File.ReadAllTextAsync(marker, token).ConfigureAwait(false)) : 0;
                    string id = Hash(Environment.MachineName + "|" + Path.GetFullPath(path));
                    // 丢失游标时完整重建该小时，避免旧分批边界留下重复记录。
                    if (offset == 0) await stores[0].DeleteAsync(new HistoryQueryFilter { DeviceId = id }, token).ConfigureAwait(false);
                    while (true)
                    {
                        var items = DatabaseArchive.ReadOperations(path, offset, 200);
                        if (items.Count == 0) break;
                        string batch = "batch/" + offset.ToString("D10") + "/";
                        var records = items.Select(item => DatabaseStore.JsonRecord(id, batch + (item.RunId?.ToString("N") ?? "general"), item.At, item)).ToArray();
                        await DatabaseStore.ReplaceBatchAsync(stores[0], id, batch, records, token, prefix: true).ConfigureAwait(false);
                        offset += items.Count;
                        await WriteMarkerAsync(marker, offset.ToString(), token).ConfigureAwait(false);
                        count += items.Count;
                    }
                });
            }
            foreach (string path in Files(Path.Combine(DatabaseArchive.Root, "waves"), "*.json"))
            {
                await Import(path, () => Once(path, async json =>
                {
                    var data = Read<DatabaseWaveData>(json);
                    await WriteStream(2, data.DataId, DatabaseStore.PointBlocks(data, Path.ChangeExtension(path, ".bin")));
                    await DatabaseStore.ReplaceBatchAsync(stores[1], data.DataId, "metadata",
                        new[] { DatabaseStore.JsonRecord(data.DataId, "metadata", data.CreatedAt, data) }, token).ConfigureAwait(false);
                }));
            }
            foreach (string path in Files(Path.Combine(DatabaseArchive.Root, "runs"), "*.json"))
            {
                await Import(path, () => Once(path, async json =>
                {
                    var run = Read<DatabaseWaveRun>(json); string id = run.RunId.ToString("N");
                    await DatabaseStore.ReplaceBatchAsync(stores[3], id, "metadata",
                        new[] { DatabaseStore.JsonRecord(id, "metadata", run.StartedAt, run) }, token).ConfigureAwait(false);
                }));
            }
            foreach (string path in Files(MonitoringRoot, "summary.json", SearchOption.AllDirectories))
            {
                await Import(path, async () =>
                {
                    string json = await File.ReadAllTextAsync(path, token).ConfigureAwait(false);
                    var report = Read<AxisMonitoringReport>(json);
                    if (report.EndedAt is null) return;
                    await Once(path, async _ =>
                    {
                        string id = report.RunId.ToString("N");
                        await WriteStream(4, id, DatabaseStore.AxisRecords(report.RunId, Path.Combine(Path.GetDirectoryName(path)!, "samples.csv")));
                        await DatabaseStore.ReplaceBatchAsync(stores[7], id, "metadata",
                            new[] { DatabaseStore.JsonRecord(id, "metadata", report.StartedAt, report) }, token).ConfigureAwait(false);
                    });
                });
            }
            foreach (var manifest in WaveDataWorkspace.Shared.ListSessions())
            {
                if (manifest.EndedAt is null) continue;
                if (manifest.SyncStatus == "已同步" && manifest.DatabaseDetailVersion >= 1 && manifest.DatabaseTarget == destination) continue;
                await Import(manifest.CsvPath, async () =>
                {
                    try
                    {
                        string id = manifest.SessionId.ToString("N");
                        await WriteStream(6, id, DatabaseStore.SensorRecords(manifest));
                        await DatabaseStore.ReplaceBatchAsync(stores[5], id, "metadata",
                            new[] { DatabaseStore.JsonRecord(id, "metadata", manifest.StartedAt,
                                new { manifest.SessionId, manifest.RunId, manifest.StartedAt, manifest.EndedAt,
                                    manifest.DeviceAddress, manifest.SampleRateHz, manifest.CalibrationVersion,
                                    manifest.Channels, manifest.SampleCount, manifest.CsvPath, manifest.IsLegacyImport, DataRecorded = true }) }, token).ConfigureAwait(false);
                        WaveDataWorkspace.Shared.MarkDatabaseSync(manifest, null, destination); count++;
                    }
                    catch (Exception ex) { WaveDataWorkspace.Shared.MarkDatabaseSync(manifest, ex.Message, destination); throw; }
                });
            }
            Status = failures == 0 ? $"同步完成：{count}条记录或归档任务；{DateTimeOffset.Now:HH:mm:ss}" : $"已同步{count}项，{failures}项待重试：{lastError}";
            if (failures == 0) LastSuccess = DateTimeOffset.Now;
            return count;

            async Task Once(string path, Func<string, Task> save)
            {
                string json = await File.ReadAllTextAsync(path, token).ConfigureAwait(false);
                string stamp = Hash(json), marker = Path.Combine(cursorRoot, Hash(path) + ".stamp");
                if (File.Exists(marker) && await File.ReadAllTextAsync(marker, token).ConfigureAwait(false) == stamp) return;
                await save(json).ConfigureAwait(false);
                await WriteMarkerAsync(marker, stamp, token).ConfigureAwait(false); count++;
            }
            async Task WriteStream(int kind, string id, IEnumerable<InduLinkDataRecord> records)
            {
                var batch = new List<InduLinkDataRecord>(200); int offset = 0;
                foreach (var record in records)
                {
                    token.ThrowIfCancellationRequested(); batch.Add(record);
                    if (batch.Count == 200) { await Flush(); }
                }
                if (batch.Count > 0) await Flush();
                async Task Flush()
                {
                    string address = "batch/" + offset.ToString("D10");
                    foreach (var record in batch) record.Address = address;
                    await DatabaseStore.ReplaceBatchAsync(stores[kind], id, address, batch, token).ConfigureAwait(false);
                    offset += batch.Count; batch.Clear();
                }
            }
        }
        catch (Exception ex) { Status = "同步失败，本地记录已保留：" + ex.Message; throw; }
        finally
        {
            foreach (var store in stores) store.Dispose();
            _gate.Release();
        }
    }
    private static IEnumerable<string> Files(string path, string pattern, SearchOption option = SearchOption.TopDirectoryOnly) =>
        Directory.Exists(path) ? Directory.EnumerateFiles(path, pattern, option) : Array.Empty<string>();
    private static T Read<T>(string json) => JsonSerializer.Deserialize<T>(json) ?? throw new InvalidDataException("归档内容为空。");
    private static string Hash(string text) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(text)));
    private static async Task WriteMarkerAsync(string path, string text, CancellationToken token)
    {
        string temporary = path + ".tmp";
        try { await File.WriteAllTextAsync(temporary, text, token).ConfigureAwait(false); File.Move(temporary, path, true); }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }
}
