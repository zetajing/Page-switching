using System.Globalization;
using System.Text;

namespace Page_switching;

internal sealed record AxisMonitoringCount(long Valid = 0, long Exceeded = 0)
{
    public double? Percent => Valid == 0 ? null : Exceeded * 100.0 / Valid;
    internal string Text => Percent is { } value
        ? $"{value.ToString("0.######", CultureInfo.InvariantCulture)}%（{Exceeded}/{Valid}）" : "未取得有效样本（0/0）";
}

internal sealed record AxisMonitoringAxisReport(int Axis, AxisMonitoringCount Position, AxisMonitoringCount Speed, AxisMonitoringCount Any);
internal sealed record AxisMonitoringReport(
    Guid RunId, uint Sequence, ushort Station, string Status, string? EndReason,
    DateTimeOffset StartedAt, DateTimeOffset? EndedAt, DateTimeOffset? FirstSampleAt, DateTimeOffset? LastSampleAt,
    AxisMonitoringProfile? Thresholds, AxisMonitoringAxisReport[] Axes, AxisMonitoringCount Any,
    long ReadFailures, long DuplicateFrames, long NcInvalidFrames, long InvalidValues, long SkippedSlots,
    double MaxReadMilliseconds, bool Incomplete, string? Error, string? DirectoryPath)
{
    internal string Format()
    {
        var text = new StringBuilder();
        text.AppendLine($"运行实测超限统计：{Status}；轨迹序号 {Sequence}，站 {Station}");
        text.AppendLine("整体（四轴位置/速度完整样本，任一项超限去重）：" + Any.Text);
        foreach (var axis in Axes)
            text.AppendLine($"轴 {axis.Axis}：位置 {axis.Position.Text}；速度 {axis.Speed.Text}；任一项 {axis.Any.Text}");
        text.AppendLine("实际角加速度：未评估；输出扭矩：未评估（未接入，不按0计入整体）。");
        text.AppendLine("目标100 ms瞬时读取，仅统计本软件本次轨迹 Running 阶段；位置/速度均为 PLC/NC 原始单位。");
        text.AppendLine("该比例为有效采样点占比，不能证明采样间隔内没有短峰，也不代表已限制/修正生成时历。");
        text.AppendLine($"读取失败 {ReadFailures}，重复帧 {DuplicateFrames}，NC无效 {NcInvalidFrames}，无效数值 {InvalidValues}，跳过过期采样机会 {SkippedSlots}；最长读取 {MaxReadMilliseconds:F1} ms。");
        text.AppendLine($"采样任务开始：{StartedAt:O}；结束：{EndedAt?.ToString("O") ?? "--"}");
        text.AppendLine($"首个/末个有效样本：{FirstSampleAt?.ToString("O") ?? "--"} / {LastSampleAt?.ToString("O") ?? "--"}");
        if (Incomplete) text.AppendLine("统计不完整：存在缺测、无效数据或未确认完整起止范围；不能作为完整时历的超限时间比例。");
        if (EndReason is not null) text.AppendLine("结束原因：" + EndReason + "；实际停车状态仍以 PLC 反馈为准。");
        if (Error is not null) text.AppendLine("监测说明：" + Error);
        if (Thresholds is { } profile)
        {
            text.AppendLine($"监测阈值版本：{profile.Version}，保存时间：{profile.SavedAt:O}（运行开始锁存，非PLC运动限值）");
            for (int i = 0; i < profile.Axes.Length; i++)
            {
                var axis = profile.Axes[i];
                text.AppendLine(FormattableString.Invariant($"轴 {i + 1} 阈值：位置 [{axis.MinPosition:G17}, {axis.MaxPosition:G17}]，速度绝对值 <= {axis.MaxSpeed:G17}"));
            }
        }
        if (DirectoryPath is not null) text.AppendLine("记录目录：" + DirectoryPath);
        return text.ToString().TrimEnd();
    }
}

// 计数器只在服务锁内使用；Snapshot 返回独立结果，页面不能改写后台计数。
internal sealed class AxisMonitoringCounter
{
    private readonly Guid _id = Guid.NewGuid();
    private readonly uint _sequence;
    private readonly ushort _station;
    private readonly DateTimeOffset _started;
    private readonly AxisMonitoringProfile? _profile;
    private readonly long[,] _valid = new long[4, 3];
    private readonly long[,] _exceeded = new long[4, 3];
    private readonly bool?[,] _lastExceeded = new bool?[4, 2];
    private uint _lastHeartbeat;
    private bool _accepted, _seenRunning;
    private long _globalValid, _globalExceeded;
    private long _failures, _duplicates, _ncInvalid, _invalidValues, _skipped;
    private double _maxRead;
    private bool _incomplete;
    private string _status = "等待轨迹执行", _error;
    private string? _endReason;
    private DateTimeOffset? _ended, _first, _last;
    internal bool Finished => _ended.HasValue;
    internal string? DirectoryPath { get; set; }

    internal AxisMonitoringCounter(uint sequence, ushort station, uint heartbeat, DateTimeOffset at,
        AxisMonitoringProfile? profile, string? error = null)
    {
        _sequence = sequence;
        _station = station;
        _lastHeartbeat = heartbeat;
        _started = at;
        _profile = profile?.Copy();
        _error = error ?? "";
        if (_profile is null)
        {
            _status = "未统计";
            _ended = at;
            _endReason = error ?? "监测阈值未配置";
        }
    }

    internal void Fail(string reason)
    {
        _failures++;
        _incomplete = true;
        _error = reason;
    }
    internal void Skip(long count)
    {
        _skipped += count;
        if (count > 0)
        {
            _incomplete = true;
        }
    }
    internal void NoteIncomplete(string reason)
    {
        _error = reason;
        _incomplete = true;
    }
    internal void NoteStorageFailure(string reason) => NoteIncomplete("记录保存失败：" + reason);
    internal void NoteReadTime(double milliseconds) => _maxRead = Math.Max(_maxRead, milliseconds);

    internal string Observe(PlcMachineMonitor frame, bool ncInvalid, DateTimeOffset at, double milliseconds,
        List<string> transitions, string? requestedReason)
    {
        NoteReadTime(milliseconds);
        // 先检查本次运行归属和帧有效性，排除的帧不能进入下面的超限计数。
        if (Finished)
        {
            return "已结束";
        }
        if (frame.ControlOwner != _station)
        {
            Finish(at, "所属站改变", true);
            return "所属站改变";
        }
        if (_accepted && frame.AcceptedSequence != _sequence)
        {
            Finish(at, "轨迹序号改变", true);
            return "轨迹序号改变";
        }
        if (frame.Heartbeat == _lastHeartbeat)
        {
            _duplicates++;
            _incomplete = true;
            return "重复心跳，未计数";
        }
        _lastHeartbeat = frame.Heartbeat;
        if (frame.AcceptedSequence != _sequence)
        {
            return "非本次轨迹，未计数";
        }
        _accepted = true;
        if (!frame.Running)
        {
            if (_seenRunning)
            {
                // 结束原因沿用原优先级：故障、请求原因、完成、停止、其他退出。
                string endReason;
                if (frame.Error)
                {
                    endReason = $"故障退出（{frame.TrajectoryErrorId}）";
                }
                else if (requestedReason is not null)
                {
                    endReason = requestedReason;
                }
                else if (frame.Done)
                {
                    endReason = "轨迹正常完成";
                }
                else if (frame.Stopped)
                {
                    endReason = "PLC已停止轨迹";
                }
                else
                {
                    endReason = $"离开轨迹执行阶段（PLC状态{frame.TrajectoryState}）";
                }
                Finish(at, endReason, false);
            }
            return "非执行阶段，未计数";
        }
        if (!_seenRunning && frame.CurrentPoint > 50)
        {
            _incomplete = true;
        }
        _seenRunning = true;
        _status = "正在统计";
        if (ncInvalid)
        {
            _ncInvalid++;
            _incomplete = true;
            return "NC反馈无效，未计数";
        }
        var axes = new[] { frame.Axis1, frame.Axis2, frame.Axis3, frame.Axis4 };
        bool complete = true;
        bool any = false;
        bool hasValid = false;
        for (int i = 0; i < 4; i++)
        {
            var threshold = _profile!.Axes[i];
            bool positionValid = double.IsFinite(axes[i].ActualPosition);
            bool speedValid = double.IsFinite(axes[i].Speed);
            bool positionExceeded = positionValid && (axes[i].ActualPosition < threshold.MinPosition || axes[i].ActualPosition > threshold.MaxPosition);
            bool speedExceeded = speedValid && Math.Abs(axes[i].Speed) > threshold.MaxSpeed;
            Update(i, 0, positionValid, positionExceeded, transitions);
            Update(i, 1, speedValid, speedExceeded, transitions);
            if (positionValid && speedValid)
            {
                _valid[i, 2]++;
                if (positionExceeded || speedExceeded)
                {
                    _exceeded[i, 2]++;
                }
            }
            // 这三个标记汇总四轴结果，后面的轴不能清除前面已经记录的情况。
            if (!positionValid || !speedValid)
            {
                complete = false;
            }
            if (positionExceeded || speedExceeded)
            {
                any = true;
            }
            if (positionValid || speedValid)
            {
                hasValid = true;
            }
        }
        if (complete)
        {
            _globalValid++;
            if (any)
            {
                _globalExceeded++;
            }
        }
        if (hasValid)
        {
            if (_first is null)
            {
                _first = at;
            }
            _last = at;
        }
        if (complete)
        {
            return "有效完整样本";
        }
        return "部分数值无效";
    }

    private void Update(int axis, int metric, bool valid, bool exceeded, List<string> transitions)
    {
        if (!valid)
        {
            _invalidValues++;
            _incomplete = true;
            return;
        }
        _valid[axis, metric]++;
        if (exceeded)
        {
            _exceeded[axis, metric]++;
        }

        string metricName = "速度";
        if (metric == 0)
        {
            metricName = "位置";
        }
        // 只在进入超限或恢复时记录，持续超限不会每100 ms重复刷日志。
        if (exceeded && _lastExceeded[axis, metric] != true)
        {
            transitions.Add($"轴 {axis + 1} {metricName}监测超限（只记录，不发送运动命令）。");
        }
        else if (!exceeded && _lastExceeded[axis, metric] == true)
        {
            transitions.Add($"轴 {axis + 1} {metricName}监测恢复（只记录，不发送运动命令）。");
        }
        _lastExceeded[axis, metric] = exceeded;
    }

    internal void Finish(DateTimeOffset at, string reason, bool incomplete)
    {
        if (Finished)
        {
            return;
        }
        _ended = at;
        _endReason = reason;
        _status = "已结束";
        if (incomplete || !_seenRunning || _globalValid == 0)
        {
            _incomplete = true;
        }
    }

    internal AxisMonitoringReport Snapshot()
    {
        var axes = new AxisMonitoringAxisReport[4];
        for (int i = 0; i < 4; i++) axes[i] = new(i + 1,
            new(_valid[i, 0], _exceeded[i, 0]), new(_valid[i, 1], _exceeded[i, 1]), new(_valid[i, 2], _exceeded[i, 2]));
        return new(_id, _sequence, _station, _status, _endReason, _started, _ended, _first, _last, _profile?.Copy(), axes,
            new(_globalValid, _globalExceeded), _failures, _duplicates, _ncInvalid, _invalidValues, _skipped,
            _maxRead, _incomplete, string.IsNullOrEmpty(_error) ? null : _error, DirectoryPath);
    }
}
