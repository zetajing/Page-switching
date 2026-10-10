using System.ComponentModel;
using System.Globalization;

namespace Page_switching
{
    public partial class Auto : UserControl
    {
        private string? _lastMonitorStateKey;
        private (DataGridViewColumn Column, int Width, int MinimumWidth)[]? _axisColumnMetrics;
        private int _axisHeaderHeight;
        private int _axisRowHeight;
        private bool _messagesExpanded;
        private float _collapsedMessageHeight;
        private int _collapsedPageHeight;

        public Auto()
        {
            InitializeComponent();
            // InitializeComponent 已应用当前 DPI，先还原为逻辑基线，避免再次放大。
            var scale = DeviceDpi / 96F;
            _collapsedMessageHeight = rootLayout.RowStyles[4].Height / scale;
            _collapsedPageHeight = (int)Math.Round(pagePanel.MinimumSize.Height / scale);
            // 保存 Designer 的逻辑尺寸，每次 DPI 改变都从基线计算，避免重复放大。
            _axisColumnMetrics = axisGrid.Columns.Cast<DataGridViewColumn>()
                .Select(column => (column, column.Width, column.MinimumWidth)).ToArray();
            _axisHeaderHeight = axisGrid.ColumnHeadersHeight;
            _axisRowHeight = axisGrid.RowTemplate.Height;
            // 表格初始化时已创建四条占位行，运行时只更新反馈值。
            axisGrid.InitializeRows();
            axisGrid.ClearSelection();
            UpdateAxisGridDpi();
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                AddLog("自动运行页面已就绪");
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            UpdateMessageLayout();
            // Dock.Fill 子项不会自动扩展滚动范围，使用已随 DPI 缩放的最小画布。
            if (pagePanel != null && AutoScrollMinSize != pagePanel.MinimumSize)
                AutoScrollMinSize = pagePanel.MinimumSize;
            UpdateAxisGridDpi();
            base.OnLayout(e);
        }

        private void UpdateAxisGridDpi()
        {
            if (_axisColumnMetrics is null) return;
            var scale = DeviceDpi / 96.0;
            foreach (var metric in _axisColumnMetrics)
            {
                var minimum = (int)Math.Round(metric.MinimumWidth * scale);
                if (metric.Column.MinimumWidth != minimum) metric.Column.MinimumWidth = minimum;
                if (metric.Column.AutoSizeMode != DataGridViewAutoSizeColumnMode.Fill)
                {
                    var width = (int)Math.Round(metric.Width * scale);
                    if (metric.Column.Width != width) metric.Column.Width = width;
                }
            }
            var headerHeight = (int)Math.Round(_axisHeaderHeight * scale);
            if (axisGrid.ColumnHeadersHeight != headerHeight) axisGrid.ColumnHeadersHeight = headerHeight;
            var rowHeight = (int)Math.Round(_axisRowHeight * scale);
            axisGrid.RowTemplate.Height = rowHeight;
            foreach (DataGridViewRow row in axisGrid.Rows)
                if (row.Height != rowHeight) row.Height = rowHeight;
        }

        public void AddLog(string message)
        {
            if (IsDisposed || Disposing) return;
            if (InvokeRequired)
            {
                try { BeginInvoke(() => AddLog(message)); }
                catch (InvalidOperationException) when (IsDisposed || Disposing || !IsHandleCreated) { }
                return;
            }

            _logList.Items.Add($"{DateTime.Now:HH:mm:ss}  {message}");
            // 同一条结果只写入统一操作日志，主窗体订阅后同步显示。
            OperationJournal.Record("自动运行", message);
            if (_logList.Items.Count > 500) _logList.Items.RemoveAt(0);
            _logList.TopIndex = Math.Max(0, _logList.Items.Count - 1);
            UpdateMessageSummary();
        }

        // 只显示快照，不申请控制权、不切换模式，也不下发造波或轴命令。
        internal void ApplyMonitorSnapshot(MachineMonitorSnapshot snapshot)
        {
            ArgumentNullException.ThrowIfNull(snapshot);
            if (IsDisposed || Disposing) return;
            if (InvokeRequired)
            {
                try { BeginInvoke(() => ApplyMonitorSnapshot(snapshot)); }
                catch (InvalidOperationException) when (IsDisposed || Disposing || !IsHandleCreated) { }
                return;
            }

            UpdateStatusCards(snapshot);
            UpdateConnectionFeedback(snapshot);
            UpdateAxisRows(snapshot);
            LogMonitorStateChange(snapshot);
        }

        // 三个状态卡片及故障码只使用当前快照，读取失败时保留明确的无效状态。
        private void UpdateStatusCards(MachineMonitorSnapshot snapshot)
        {
            ApplyEnumField(snapshot.ControlOwner, controlValueLabel, controlDetailLabel,
                FormatControlOwner, value => value <= 3, value => value == 0 ? UiPalette.Warning : UiPalette.Primary);
            ApplyEnumField(snapshot.ControlMode, modeValueLabel, modeDetailLabel,
                FormatControlMode, value => value <= 2,
                value => value == 0 ? UiPalette.Warning : value == 2 ? UiPalette.Primary : UiPalette.Text);
            ApplyWaveState(snapshot.WaveState);
            var faultText = FormatMonitorField(snapshot.WaveFaultCode);
            var hasCurrentFault = HasValue(snapshot.WaveFaultCode) && snapshot.WaveFaultCode.Value != 0;
            faultCodeLabel.Text = "故障码：" + faultText + (hasCurrentFault ? "（当前故障）" : "");
            faultCodeLabel.ForeColor = hasCurrentFault ? UiPalette.Danger
                : HasValue(snapshot.WaveFaultCode) ? UiPalette.Muted : UnavailableColor(faultText);
            // 故障码独立于运行枚举：保留 PLC 状态文字，同时突出当前故障。
            if (hasCurrentFault) _runStateLabel.ForeColor = UiPalette.Danger;
            faultCodeLabel.AccessibleDescription = snapshot.WaveFaultCode.ErrorMessage;
        }

        // ADS 已连接与 PLC 反馈有效分别显示，心跳停滞不能显示为正常。
        private void UpdateConnectionFeedback(MachineMonitorSnapshot snapshot)
        {
            connectionLabel.Text = snapshot.IsConnected
                ? "● ADS 已连接 · " + snapshot.QualityText
                : "● ADS 未连接";
            connectionLabel.ForeColor = snapshot.OverallQuality is MachineMonitorQuality.Stale or MachineMonitorQuality.HeartbeatStalled ||
                !snapshot.IsConnected && snapshot.LastSuccessTimestamp.HasValue
                ? UiPalette.Warning
                : snapshot.IsLive && snapshot.OverallQuality == MachineMonitorQuality.Good ? UiPalette.Success
                : snapshot.OverallQuality == MachineMonitorQuality.Partial ? UiPalette.Warning
                : UnavailableColor(FormatUnavailableFeedback(snapshot.ErrorMessage));
            connectionLabel.AccessibleDescription = snapshot.ErrorMessage;
            feedbackLabel.Text = "最近有效反馈：" +
                (snapshot.LastSuccessTimestamp?.ToLocalTime().ToString("HH:mm:ss", CultureInfo.CurrentCulture) ?? "--");
            feedbackLabel.ForeColor = snapshot.IsLive ? UiPalette.SecondaryText : UiPalette.Muted;
            var heartbeatText = FormatMonitorField(snapshot.Heartbeat);
            heartbeatLabel.Text = "心跳：" + heartbeatText + (HasValue(snapshot.Heartbeat)
                ? snapshot.OverallQuality switch
                {
                    MachineMonitorQuality.HeartbeatStalled => "（停滞）",
                    MachineMonitorQuality.WaitingHeartbeat => "（等待变化）",
                    _ => ""
                } : "");
            heartbeatLabel.ForeColor = snapshot.OverallQuality == MachineMonitorQuality.HeartbeatStalled
                ? UiPalette.Warning : snapshot.OverallQuality == MachineMonitorQuality.WaitingHeartbeat ? UiPalette.Muted
                : HasValue(snapshot.Heartbeat) ? UiPalette.SecondaryText : UnavailableColor(heartbeatText);
            heartbeatLabel.AccessibleDescription = snapshot.Heartbeat.ErrorMessage;
        }

        // 固定四行只更新单元格；列、布局和样式继续由 Designer 编辑。
        private void UpdateAxisRows(MachineMonitorSnapshot snapshot)
        {
            for (var index = 0; index < 4; index++)
            {
                var axis = snapshot.Axes.FirstOrDefault(value => value.AxisNumber == index + 1)
                    ?? new MachineAxisSnapshot { AxisNumber = index + 1 };
                var row = axisGrid.Rows[index];
                ApplyNumberCell(row.Cells[1], axis.ActualPosition);
                ApplyNumberCell(row.Cells[2], axis.Speed);
                ApplyBoolCell(row.Cells[3], axis.IsHomed, "已回零", "未回零", UiPalette.Success, UiPalette.Warning);
                ApplyBoolCell(row.Cells[4], axis.HasAlarm, "报警", "正常", UiPalette.Danger, UiPalette.Text);
                ApplyBoolCell(row.Cells[5], axis.NegativeLimit, "触发", "未触发", UiPalette.Warning, UiPalette.Text);
                ApplyBoolCell(row.Cells[6], axis.PositiveLimit, "触发", "未触发", UiPalette.Warning, UiPalette.Text);
                ApplyBoolCell(row.Cells[7], axis.OriginSignal, "触发", "未触发", UiPalette.Primary, UiPalette.Text);
            }
        }

        private void LogMonitorStateChange(MachineMonitorSnapshot snapshot)
        {
            // 首次快照作为基线；时间、心跳计数、位置和速度变化不产生周期性日志。
            var stateKey = string.Join("|", snapshot.IsConnected, snapshot.OverallQuality,
                snapshot.ControlOwner.ToDisplayText(), snapshot.ControlMode.ToDisplayText(),
                snapshot.WaveState.ToDisplayText(), snapshot.WaveFaultCode.ToDisplayText());
            if (_lastMonitorStateKey != null && _lastMonitorStateKey != stateKey)
                AddLog($"监控状态：{(snapshot.IsConnected ? "ADS 已连接" : "ADS 未连接")}；" +
                    $"控制端：{FormatMonitorField(snapshot.ControlOwner, FormatControlOwner)}；" +
                    $"模式：{FormatMonitorField(snapshot.ControlMode, FormatControlMode)}；" +
                    $"造波：{FormatMonitorField(snapshot.WaveState, FormatWaveState)}；" +
                    $"故障码：{FormatMonitorField(snapshot.WaveFaultCode)}；{snapshot.QualityText}");
            _lastMonitorStateKey = stateKey;
        }

        internal static string FormatControlOwner(ushort value) => value switch
        {
            0 => "未分配",
            1 => "上位机",
            2 => "现场操作台",
            3 => "其他控制端",
            _ => $"未知({value})"
        };

        internal static string FormatControlMode(ushort value) => value switch
        {
            0 => "未选择",
            1 => "手动",
            2 => "自动",
            _ => $"未知({value})"
        };

        private static string FormatWaveState(ushort value) => value switch
        {
            0 => "待机",
            1 => "准备中",
            2 => "造波中",
            3 => "已暂停",
            4 => "停止中",
            5 => "已停止",
            6 => "已完成",
            7 => "故障",
            _ => $"未知({value})"
        };

        // 主窗体和本页共用无效反馈的短文；原始原因保留在详情或提示中。
        internal static string FormatMonitorField<T>(FeedbackField<T> field,
            Func<T, string>? formatter = null) where T : struct =>
            HasValue(field) ? field.ToDisplayText(formatter) : FormatUnavailableFeedback(field.ErrorMessage);

        internal static string FormatUnavailableFeedback(string? reason)
        {
            if (string.IsNullOrWhiteSpace(reason) || reason.Contains("未读取", StringComparison.Ordinal))
                return "等待反馈";
            if (reason.Contains("未配置", StringComparison.Ordinal) || reason.Contains("未接入", StringComparison.Ordinal) ||
                reason.Contains("DeviceSymbolNotFound", StringComparison.OrdinalIgnoreCase))
                return "未接入";
            if (reason.Contains("过期", StringComparison.Ordinal) ||
                reason.Contains("未收到有效反馈", StringComparison.Ordinal) && reason.Contains("超过", StringComparison.Ordinal))
                return "反馈过期";
            if (reason.Contains("心跳", StringComparison.Ordinal))
            {
                if (reason.Contains("等待", StringComparison.Ordinal) || reason.Contains("首次", StringComparison.Ordinal) ||
                    reason.Contains("尚未", StringComparison.Ordinal))
                    return "等待心跳";
                if (reason.Contains("未变化", StringComparison.Ordinal) || reason.Contains("停止", StringComparison.Ordinal) ||
                    reason.Contains("停滞", StringComparison.Ordinal))
                    return "心跳停滞";
            }
            if (reason.Contains("未连接", StringComparison.Ordinal) || reason.Contains("断开", StringComparison.Ordinal))
                return "未连接";
            if (reason.Contains("已停止", StringComparison.Ordinal))
                return "监控已停止";
            return "读取失败";
        }

        private static Color UnavailableColor(string text) =>
            text is "未接入" or "等待反馈" or "等待心跳" or "未连接" ? UiPalette.Muted : UiPalette.Warning;

        private static bool HasValue<T>(FeedbackField<T> field) where T : struct =>
            field.IsAvailable && field.Value.HasValue;

        private static void ApplyEnumField(FeedbackField<ushort> field, Label valueLabel, Label detailLabel,
            Func<ushort, string> format, Func<ushort, bool> isKnown, Func<ushort, Color> color)
        {
            valueLabel.Text = FormatMonitorField(field, format);
            if (!HasValue(field))
            {
                valueLabel.ForeColor = UnavailableColor(valueLabel.Text);
                detailLabel.Text = field.ErrorMessage ?? "等待有效反馈";
                detailLabel.ForeColor = valueLabel.ForeColor;
                return;
            }

            var value = field.Value!.Value;
            var known = isKnown(value);
            valueLabel.ForeColor = known ? color(value) : UiPalette.Warning;
            detailLabel.Text = known ? "PLC 反馈" : "未知状态值：" + value;
            detailLabel.ForeColor = known ? UiPalette.Muted : UiPalette.Warning;
        }

        private void ApplyWaveState(FeedbackField<ushort> field)
        {
            _runStateLabel.Text = FormatMonitorField(field, FormatWaveState);
            _runStateLabel.AccessibleDescription = field.ErrorMessage;
            _runStateLabel.ForeColor = !HasValue(field) ? UnavailableColor(_runStateLabel.Text) : field.Value switch
            {
                1 or 2 => UiPalette.Primary,
                3 or 4 => UiPalette.Warning,
                6 => UiPalette.Success,
                7 => UiPalette.Danger,
                0 or 5 => UiPalette.Text,
                _ => UiPalette.Warning
            };
        }

        private static void ApplyNumberCell(DataGridViewCell cell, FeedbackField<double> field)
        {
            cell.Value = field.ToDisplayText(value => value.ToString("0.###", CultureInfo.CurrentCulture));
            cell.Style.ForeColor = HasValue(field) ? UiPalette.Text
                : UnavailableColor(FormatUnavailableFeedback(field.ErrorMessage));
            cell.Style.SelectionForeColor = cell.Style.ForeColor;
            cell.ToolTipText = field.ErrorMessage ?? "";
        }

        private static void ApplyBoolCell(DataGridViewCell cell, FeedbackField<bool> field,
            string trueText, string falseText, Color trueColor, Color falseColor)
        {
            cell.Value = field.ToDisplayText(value => value ? trueText : falseText);
            cell.Style.ForeColor = !HasValue(field) ? UnavailableColor(FormatUnavailableFeedback(field.ErrorMessage))
                : field.Value == true ? trueColor : falseColor;
            cell.Style.SelectionForeColor = cell.Style.ForeColor;
            cell.ToolTipText = field.ErrorMessage ?? "";
        }

        private void UpdateMessageSummary()
        {
            messageCountLabel.Text = $"消息：{_logList.Items.Count} 条";
            var latest = _logList.Items.Count == 0 ? null : _logList.Items[^1]?.ToString();
            // 摘要只占一行，完整消息仍保留在列表和文件日志中。
            messageSummaryLabel.Text = latest is null ? "暂无运行消息"
                : "最新：" + latest.Replace("\r", " ").Replace("\n", " ").Replace("\t", " ");
            messageToolTip.SetToolTip(messageSummaryLabel, latest);
        }

        private void MessageToggleButton_Click(object? sender, EventArgs e)
        {
            _messagesExpanded = !_messagesExpanded;
            SuspendLayout();
            logLayout.SuspendLayout();
            _logList.Visible = _messagesExpanded;
            clearLogButton.Visible = _messagesExpanded;
            messageToggleButton.Text = _messagesExpanded ? "收起 ▲" : "展开 ▼";
            UpdateMessageLayout();
            logLayout.ResumeLayout(true);
            ResumeLayout(true);
            // 隐藏的列表首次显示会重建滚动位置，展开后重新定位到最新记录。
            if (_messagesExpanded) _logList.TopIndex = Math.Max(0, _logList.Items.Count - 1);
        }

        private void UpdateMessageLayout()
        {
            if (_collapsedMessageHeight <= 0 || logLayout is null) return;
            // 只改变展示高度；折叠期间消息记录、500 条上限和文件日志继续执行。
            var scale = DeviceDpi / 96F;
            var extraHeight = _messagesExpanded ? 180 + 38 : 0;
            var groupHeight = (float)Math.Round((_collapsedMessageHeight + extraHeight) * scale);
            if (rootLayout.RowStyles[4].Height != groupHeight)
                rootLayout.RowStyles[4].Height = groupHeight;
            logLayout.RowStyles[1].Height = _messagesExpanded ? (float)Math.Round(180 * scale) : 0;
            logLayout.RowStyles[2].Height = _messagesExpanded ? (float)Math.Round(38 * scale) : 0;
            var minimumSize = new Size(pagePanel.MinimumSize.Width,
                (int)Math.Round((_collapsedPageHeight + extraHeight) * scale));
            if (pagePanel.MinimumSize != minimumSize) pagePanel.MinimumSize = minimumSize;
        }

        private void ClearLogButton_Click(object? sender, EventArgs e)
        {
            _logList.Items.Clear();
            UpdateMessageSummary();
        }
    }
}
