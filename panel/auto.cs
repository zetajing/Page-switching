using System.ComponentModel;
using System.Globalization;

namespace Page_switching
{
    public partial class Auto : UserControl
    {
        private string? _lastMonitorStateKey;
        private (DataGridViewColumn Column, int Width, int MinimumWidth, float FillWeight)[]? _axisColumnMetrics;
        private int _axisHeaderHeight;
        private int _axisRowHeight;
        private bool _messagesExpanded;
        private readonly ToolTip _feedbackTips;
        private readonly List<Image> _cardImages = new();
        private int _iconDpi;

        public Auto()
        {
            InitializeComponent();
            components ??= new Container();
            _feedbackTips = new ToolTip(components) { AutoPopDelay = 30000, InitialDelay = 400, ReshowDelay = 100 };
            axisGrid.CellPainting += AxisGrid_CellPainting;
            Disposed += (_, _) => { foreach (var image in _cardImages) image.Dispose(); };
            // 保存 Designer 的逻辑尺寸，每次 DPI 改变都从基线计算，避免重复放大。
            _axisColumnMetrics = axisGrid.Columns.Cast<DataGridViewColumn>()
                .Select(column => (column, column.Width, column.MinimumWidth, column.FillWeight)).ToArray();
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
            if (pagePanel is not null)
            {
                var scale = DeviceDpi / 96d;
                var minimum = new Size((int)Math.Round(760 * scale),
                    (int)Math.Round((_messagesExpanded ? 800 : 620) * scale));
                if (pagePanel.MinimumSize != minimum) pagePanel.MinimumSize = minimum;
                var messageHeight = _messagesExpanded ? (int)Math.Round(180 * scale) : 0;
                if (rootLayout.RowStyles.Count == 6 && rootLayout.RowStyles[5].Height != messageHeight)
                    rootLayout.RowStyles[5].Height = messageHeight;
                UpdateCardIcons();
            }
            // Dock.Fill 子项不会自动扩展滚动范围，使用已随 DPI 缩放的最小画布。
            if (pagePanel != null && AutoScrollMinSize != pagePanel.MinimumSize)
                AutoScrollMinSize = pagePanel.MinimumSize;
            UpdateAxisGridDpi();
            base.OnLayout(e);
        }

        // 图标从 DPI 基准重建，标题和详情仍是 Designer 中可编辑的 Label。
        private void UpdateCardIcons()
        {
            if (_cardImages is null || _iconDpi == DeviceDpi || controlCaptionLabel is null) return;
            foreach (var image in _cardImages) image.Dispose();
            _cardImages.Clear();
            foreach (var (label, icon, color) in new[]
            {
                (controlCaptionLabel, UiIcon.Owner, UiPalette.WorkPrimary),
                (modeCaptionLabel, UiIcon.Mode, UiPalette.Success),
                (waveCaptionLabel, UiIcon.State, UiPalette.Warning)
            })
            {
                var image = UiIcons.Create(icon, color, (int)Math.Round(20 * DeviceDpi / 96d));
                label.Image = image;
                label.ImageAlign = ContentAlignment.MiddleRight;
                label.Padding = Padding.Empty;
                _cardImages.Add(image);
            }
            _iconDpi = DeviceDpi;
        }

        private void MessageToggleButton_Click(object? sender, EventArgs e)
        {
            _messagesExpanded = !_messagesExpanded;
            logGroup.Visible = _messagesExpanded;
            UpdateMessageSummary();
            PerformLayout();
            if (_messagesExpanded && _logList.Items.Count > 0)
                _logList.TopIndex = _logList.Items.Count - 1;
        }

        private void UpdateMessageSummary()
        {
            messageToggleButton.Text = $"{(_messagesExpanded ? "收起" : "展开")}运行消息 ({_logList.Items.Count})";
            var latest = _logList.Items.Count == 0 ? "暂无运行消息" : _logList.Items[^1]?.ToString() ?? "";
            messageSummaryLabel.Text = latest.Length > 120 ? latest[..120] + "…" : latest;
            _feedbackTips.SetToolTip(messageSummaryLabel, latest);
            clearLogButton.Enabled = _logList.Items.Count > 0;
        }

        // 状态标签只负责绘制；原始单元格文本、选择和错误提示继续使用现有数据。
        private void AxisGrid_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 3 || e.CellStyle is null) return;
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.SelectionBackground);
            var scale = DeviceDpi / 96f;
            var text = e.FormattedValue?.ToString() ?? "--";
            var color = e.CellStyle.ForeColor;
            if (color == UiPalette.Text) color = UiPalette.WorkMuted;
            var font = e.CellStyle.Font ?? axisGrid.Font;
            var width = Math.Min(e.CellBounds.Width - (int)(12 * scale), TextRenderer.MeasureText(text, font).Width + (int)(16 * scale));
            var height = (int)(26 * scale);
            var bounds = new Rectangle(e.CellBounds.X + (e.CellBounds.Width - width) / 2,
                e.CellBounds.Y + (e.CellBounds.Height - height) / 2, Math.Max(1, width), height);
            using var shape = UiIcons.RoundedRectangle(bounds, 5 * scale);
            using var brush = new SolidBrush(Color.FromArgb(18, color));
            e.Graphics!.FillPath(brush, shape);
            TextRenderer.DrawText(e.Graphics, text, font, bounds, color,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            e.Handled = true;
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
            // 原生表格在设置最小列宽时会调整填充权重；恢复 Designer 基准保证两列比例稳定。
            foreach (var metric in _axisColumnMetrics)
                if (metric.Column.AutoSizeMode == DataGridViewAutoSizeColumnMode.Fill && metric.Column.FillWeight != metric.FillWeight)
                    metric.Column.FillWeight = metric.FillWeight;
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
            _feedbackTips.SetToolTip(controlValueLabel, snapshot.ControlOwner.ErrorMessage ?? "PLC 控制端反馈");
            _feedbackTips.SetToolTip(controlDetailLabel, snapshot.ControlOwner.ErrorMessage ?? "PLC 控制端反馈");
            _feedbackTips.SetToolTip(modeValueLabel, snapshot.ControlMode.ErrorMessage ?? "PLC 运行模式反馈");
            _feedbackTips.SetToolTip(modeDetailLabel, snapshot.ControlMode.ErrorMessage ?? "PLC 运行模式反馈");
            _feedbackTips.SetToolTip(_runStateLabel, snapshot.WaveState.ErrorMessage ?? "PLC 造波状态反馈");
            _feedbackTips.SetToolTip(faultCodeLabel, snapshot.WaveFaultCode.ErrorMessage ?? "PLC 故障码反馈");
            _feedbackTips.SetToolTip(connectionLabel, snapshot.ErrorMessage ?? snapshot.QualityText);
            _feedbackTips.SetToolTip(heartbeatLabel, snapshot.Heartbeat.ErrorMessage ?? "PLC 心跳反馈");
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
                detailLabel.Text = FormatUnavailableFeedback(field.ErrorMessage) + " · 悬停查看详情";
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

        private void ClearLogButton_Click(object? sender, EventArgs e)
        {
            _logList.Items.Clear();
            UpdateMessageSummary();
        }
    }
}
