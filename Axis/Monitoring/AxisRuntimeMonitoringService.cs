using System.Globalization;
using System.Text;
using System.Text.Json;

namespace Page_switching;

// 独立观测服务：一个后台循环、一个客户端、逐行落盘；不执行任何运动命令。
internal sealed class AxisRuntimeMonitoringService
{
    private readonly object _gate = new();
    private readonly Func<IAxisMonitoringSource> _source;
    private readonly IAxisMonitoringClock _clock;
    private readonly Func<(AxisMonitoringProfile? Profile, string? Error)> _load;
    private readonly string _directory;
    private readonly Action<string> _log;
    private Task _worker = Task.CompletedTask;
    private Run? _current;

    private sealed class Run(AxisMonitoringCounter counter)
    {
        internal readonly AxisMonitoringCounter Counter = counter;
        internal readonly CancellationTokenSource Cancellation = new();
        internal string? RequestedReason;
    }

    internal AxisRuntimeMonitoringService(AxisServiceOptions options) : this(
        () => new AdsAxisMonitoringSource(options), new AxisMonitoringClock(), () => AxisMonitoringSettings.Load(),
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PageSwitching", "AxisMonitoring"),
        message => OperationJournal.Record("运行超限统计", message)) { }

    // 时钟和只读来源可替换，离线验证不连接真实 ADS。
    internal AxisRuntimeMonitoringService(Func<IAxisMonitoringSource> source, IAxisMonitoringClock clock,
        Func<(AxisMonitoringProfile? Profile, string? Error)> load, string directory, Action<string> log)
    {
        _source = source; _clock = clock; _load = load; _directory = directory; _log = log;
    }

    internal AxisMonitoringReport? Latest { get { lock (_gate) return _current?.Counter.Snapshot(); } }

    internal void Start(uint sequence, ushort station, uint heartbeat)
    {
        lock (_gate)
        {
            var loaded = _load();
            // 上一采集/写盘尚未退出时，不并发启动下一份采样，也不阻断运动。
            bool busy = !_worker.IsCompleted;
            if (busy) loaded = (null, "上一采样任务尚未结束，未统计。");
            var run = new Run(new(sequence, station, heartbeat, _clock.UtcNow, loaded.Profile, loaded.Error));
            _current = run;
            if (busy) { run.Cancellation.Dispose(); _ = Task.Run(() => Log(run.Counter.Snapshot().Format())); return; }
            _worker = Task.Run(() => SampleAsync(run));
        }
    }

    // 停止按钮只先登记原因；继续观察到离开 Running，不能把停车前的有效执行样本提前丢掉。
    internal void NoteEnding(string reason)
    {
        lock (_gate) if (_current is { } run && !run.Counter.Finished) run.RequestedReason = reason;
    }

    internal void Complete(string reason, bool immediately = false)
    {
        lock (_gate)
        {
            if (_current is not { } run || run.Counter.Finished) return;
            run.RequestedReason ??= reason;
            // 正常收尾留一次只读观察机会，避免运动线程先结束就把完整报告误标缺测。
            // 不等待这个任务；软件关闭则立即取消，并在 StopAsync 中执行有界清理。
            try { if (immediately) run.Cancellation.Cancel(); else run.Cancellation.CancelAfter(600); }
            catch (Exception ex) { run.Counter.NoteIncomplete("取消采样失败：" + ex.Message); }
        }
    }

    internal async Task StopAsync()
    {
        Task worker;
        lock (_gate) { Complete("软件退出/统计停止", immediately: true); worker = _worker; }
        try { await worker.WaitAsync(TimeSpan.FromSeconds(2)).ConfigureAwait(false); }
        catch (TimeoutException)
        {
            lock (_gate) _current?.Counter.NoteIncomplete("统计清理超过2秒，记录可能未完成。");
            Log("统计清理未确认；不阻塞运动服务释放，原始记录可能不完整。");
        }
        catch (Exception ex) { Log("统计清理失败：" + ex.Message); }
    }

    private async Task SampleAsync(Run run)
    {
        StreamWriter? writer = null;
        IAxisMonitoringSource? source = null;
        var token = run.Cancellation.Token;
        long next = _clock.Milliseconds;
        long retryAt = next;
        string? path = null;
        string? lastReadError = null;
        try
        {
            try
            {
                AxisMonitoringReport initial;
                lock (_gate) initial = run.Counter.Snapshot();
                path = Path.Combine(_directory, initial.StartedAt.ToString("yyyyMMdd-HHmmss", CultureInfo.InvariantCulture) + "-" + initial.RunId.ToString("N"));
                Directory.CreateDirectory(path);
                lock (_gate) run.Counter.DirectoryPath = path;
                // 写入 UTF-8 BOM，供 Excel 直接打开时正确识别 Outcome 中的中文。
                writer = new StreamWriter(Path.Combine(path, "samples.csv"), false, new UTF8Encoding(true));
                await writer.WriteLineAsync("ReadCompletedAt,ReadMilliseconds,Heartbeat,AcceptedSequence,ControlOwner,Running,FeedbackInvalid,Outcome,Axis1Position,Axis1Speed,Axis2Position,Axis2Speed,Axis3Position,Axis3Speed,Axis4Position,Axis4Speed").ConfigureAwait(false);
                // 先保存锁存阈值，异常退出时仍能识别未结束的任务；最终汇总再原子替换。
                await SaveSummaryAsync(path, initial).ConfigureAwait(false);
            }
            catch (Exception ex) { StorageError(run, ex); }
            while (true)
            {
                lock (_gate) if (run.Counter.Finished) break;
                token.ThrowIfCancellationRequested();
                long due = Math.Max(next, retryAt);
                long wait = due - _clock.Milliseconds;
                if (wait > 0) await _clock.DelayAsync((int)Math.Min(wait, int.MaxValue), token).ConfigureAwait(false);
                token.ThrowIfCancellationRequested();
                long started = _clock.Milliseconds;
                AxisMonitoringReading? reading = null;
                string outcome;
                DateTimeOffset completed;
                double elapsed;
                var transitions = new List<string>();
                try
                {
                    source ??= _source();
                    reading = await source.ReadAsync(token).ConfigureAwait(false);
                    token.ThrowIfCancellationRequested(); // 取消后的迟到读取不能继续计数。
                    completed = _clock.UtcNow; elapsed = _clock.Milliseconds - started;
                    lock (_gate) outcome = run.Counter.Observe(reading.Frame, reading.FeedbackInvalid, completed, elapsed, transitions, run.RequestedReason);
                    if (lastReadError is not null) { Log("只读采样恢复；之前缺测仍保留在本次报告。"); lastReadError = null; }
                }
                catch (OperationCanceledException) when (token.IsCancellationRequested) { throw; }
                catch (Exception ex)
                {
                    completed = _clock.UtcNow; elapsed = _clock.Milliseconds - started;
                    outcome = "读取失败：" + ex.Message;
                    lock (_gate) { run.Counter.Fail(outcome); run.Counter.NoteReadTime(elapsed); }
                    if (lastReadError != ex.Message) { Log(outcome); lastReadError = ex.Message; }
                    source?.Dispose(); source = null;
                    retryAt = _clock.Milliseconds + 1000;
                }
                foreach (string transition in transitions) Log(transition);
                if (writer is not null)
                {
                    try { await writer.WriteLineAsync(CsvRow(completed, elapsed, reading, outcome)).ConfigureAwait(false); }
                    catch (Exception ex)
                    {
                        StorageError(run, ex);
                        try { writer.Dispose(); } catch (Exception disposeError) { StorageError(run, disposeError); }
                        writer = null;
                    }
                }
                lock (_gate) if (run.Counter.Finished) break;
                // 每次只安排未来的采样机会。迟到不重放缓存，也不突发补读。
                next += 100;
                long now = _clock.Milliseconds;
                if (next <= now)
                {
                    long skipped = (now - next) / 100 + 1;
                    lock (_gate) run.Counter.Skip(skipped);
                    next += skipped * 100;
                }
            }
        }
        catch (OperationCanceledException) when (token.IsCancellationRequested) { }
        catch (Exception ex) { lock (_gate) run.Counter.Fail("采样任务失败：" + ex.Message); Log("采样任务失败：" + ex.Message); }
        finally
        {
            try { source?.Dispose(); } catch (Exception ex) { Log("采样连接释放失败：" + ex.Message); }
            try { if (writer is not null) await writer.DisposeAsync().ConfigureAwait(false); }
            catch (Exception ex) { StorageError(run, ex); }
            AxisMonitoringReport final;
            lock (_gate)
            {
                run.Counter.Finish(_clock.UtcNow, run.RequestedReason ?? "采样任务结束", true);
                final = run.Counter.Snapshot();
            }
            if (path is not null)
            {
                try { await SaveSummaryAsync(path, final).ConfigureAwait(false); }
                catch (Exception ex) { StorageError(run, ex); lock (_gate) final = run.Counter.Snapshot(); }
            }
            Log(final.Format());
            run.Cancellation.Dispose();
        }
    }

    private void StorageError(Run run, Exception error)
    {
        lock (_gate) run.Counter.NoteStorageFailure(error.Message);
        Log("运行统计记录保存失败，不改变运动状态：" + error.Message);
    }

    private void Log(string message)
    {
        try { _log(message); }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine("运行监测日志失败：" + ex); }
    }

    private static async Task SaveSummaryAsync(string path, AxisMonitoringReport report)
    {
        string target = Path.Combine(path, "summary.json"), temporary = target + ".tmp";
        try
        {
            await File.WriteAllTextAsync(temporary, JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true })).ConfigureAwait(false);
            File.Move(temporary, target, overwrite: true);
        }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }

    private static string CsvRow(DateTimeOffset at, double elapsed, AxisMonitoringReading? reading, string outcome)
    {
        var fields = new List<string> { at.ToString("O"), elapsed.ToString("G17", CultureInfo.InvariantCulture) };
        if (reading is { } value)
        {
            var frame = value.Frame;
            fields.AddRange(new[] { frame.Heartbeat.ToString(CultureInfo.InvariantCulture), frame.AcceptedSequence.ToString(CultureInfo.InvariantCulture),
                frame.ControlOwner.ToString(CultureInfo.InvariantCulture), frame.Running.ToString(), value.FeedbackInvalid.ToString(), outcome });
            foreach (var axis in new[] { frame.Axis1, frame.Axis2, frame.Axis3, frame.Axis4 })
            {
                fields.Add(Number(axis.ActualPosition)); fields.Add(Number(axis.Speed));
            }
        }
        else { fields.AddRange(new[] { "", "", "", "", "", outcome }); for (int i = 0; i < 8; i++) fields.Add(""); }
        return string.Join(",", fields.Select(field => "\"" + field.Replace("\"", "\"\"") + "\""));
    }
    // CSV保留NaN/Infinity文本便于定位轴和字段；计数器已将它们排除，不能写成0。
    private static string Number(double value) => value.ToString("G17", CultureInfo.InvariantCulture);
}
