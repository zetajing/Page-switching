using System.ComponentModel;
using System.Globalization;

namespace Page_switching
{
    public partial class Auto : UserControl
    {
        private string? _lastMonitorStateKey;
        private readonly List<string> _messages = new();

        private float _minimumPageHeight;

        // 设计器也会触发布局事件；编辑位置和行高时不执行运行时滚动逻辑。
        private bool IsDesignSurface => LicenseManager.UsageMode == LicenseUsageMode.Designtime
            || DesignMode || Site?.DesignMode == true;

        public Auto()
        {
            InitializeComponent();
            // InitializeComponent 已应用当前 DPI，先还原为逻辑基线，避免再次放大。
            var scale = DeviceDpi / 96F;
            _minimumPageHeight = pagePanel.MinimumSize.Height / scale;
            axisGrid.InitializeRows();
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                AddLog("自动运行页面已就绪");
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (IsDesignSurface)
            {
                base.OnLayout(e);
                return;
            }
            UpdateScrollCanvas();
            // Dock.Fill 子项不会自动扩展滚动范围，使用已随 DPI 缩放的最小画布。
            if (pagePanel != null && AutoScrollMinSize != pagePanel.MinimumSize)
                AutoScrollMinSize = pagePanel.MinimumSize;
            base.OnLayout(e);
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

            _messages.Add($"{DateTime.Now:HH:mm:ss}  {message}");
            // 同一条结果只写入统一操作日志，主窗体订阅后同步显示。
            OperationJournal.Record("自动运行", message);
            if (_messages.Count > 500) _messages.RemoveAt(0);
            _logList.Text = string.Join(Environment.NewLine, _messages);
            _logList.ClearUndo();
            _logList.ScrollToEnd();
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

            _waveFrame = snapshot.IsLive ? snapshot.Frame : null;
            UpdateWaveControls();
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

        // 固定四行只更新单元格；表格布局在 Designer，列定义在 AxisFeedbackGrid。
        private void UpdateAxisRows(MachineMonitorSnapshot snapshot)
        {
            for (var index = 0; index < 4; index++)
            {
                var axis = snapshot.Axes.FirstOrDefault(value => value.AxisNumber == index + 1)
                    ?? new MachineAxisSnapshot { AxisNumber = index + 1 };
                var row = axisGrid.FeedbackRows.Rows[index];
                row[1] = NumberCell(axis.ActualPosition);
                row[2] = NumberCell(axis.Speed);
                row[3] = BoolCell(axis.IsHomed, "已回零", "未回零", UiPalette.Success, UiPalette.Warning);
                row[4] = BoolCell(axis.HasAlarm, "报警", "正常", UiPalette.Danger, UiPalette.Text);
                row[5] = BoolCell(axis.NegativeLimit, "触发", "未触发", UiPalette.Warning, UiPalette.Text);
                row[6] = BoolCell(axis.PositiveLimit, "触发", "未触发", UiPalette.Warning, UiPalette.Text);
                row[7] = BoolCell(axis.OriginSignal, "触发", "未触发", UiPalette.Primary, UiPalette.Text);
            }

            axisGrid.Refresh();
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
            1 => "造波控制室",
            2 => "拖车本地",
            3 => "远程操作室",
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

        private static void ApplyEnumField(FeedbackField<ushort> field, AntdUI.Label valueLabel, AntdUI.Label detailLabel,
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

        // 单元格保留无效状态及原始原因；超长文字由 AntdUI ShowTip 展示。
        private static AntdUI.CellText NumberCell(FeedbackField<double> field) => new(
            HasValue(field) ? field.ToDisplayText(value => value.ToString("0.###", CultureInfo.CurrentCulture))
                : "--：" + (field.ErrorMessage ?? "等待反馈"))
        { Fore = HasValue(field) ? UiPalette.Text : UnavailableColor(FormatUnavailableFeedback(field.ErrorMessage)) };

        private static AntdUI.CellText BoolCell(FeedbackField<bool> field,
            string trueText, string falseText, Color trueColor, Color falseColor) => new(
            HasValue(field) ? (field.Value == true ? trueText : falseText) : "--：" + (field.ErrorMessage ?? "等待反馈"))
            {
                Fore = !HasValue(field) ? UnavailableColor(FormatUnavailableFeedback(field.ErrorMessage))
            : field.Value == true ? trueColor : falseColor
            };

        private void UpdateMessageSummary()
        {
            messageCountLabel.Text = $"消息：{_messages.Count} 条";
            var latest = _messages.Count == 0 ? null : _messages[^1]?.ToString();
            // 摘要只占一行，完整消息仍保留在列表和文件日志中。
            messageSummaryLabel.Text = latest is null ? "暂无运行消息"
                : "最新：" + latest.Replace("\r", " ").Replace("\n", " ").Replace("\t", " ");
            messageToolTip.SetToolTip(messageSummaryLabel, latest);
        }

        private void UpdateScrollCanvas()
        {
            if (IsDesignSurface || _minimumPageHeight <= 0 || logGroup is null) return;
            // 左侧进度区域与右侧反馈/消息由 Designer 分栏，最小画布保证缩小时可滚动。
            var scale = DeviceDpi / 96F;
            var contentHeight = rootLayout.Padding.Vertical + pagePanel.Padding.Vertical
                + rootLayout.RowStyles.Cast<RowStyle>().Where(row => row.SizeType == SizeType.Absolute)
                    .Sum(row => row.Height)
                + rightLayout.RowStyles.Cast<RowStyle>().Where(row => row.SizeType == SizeType.Absolute)
                    .Sum(row => row.Height)
                + (float)Math.Round(12 * scale);
            var minimumSize = new Size(pagePanel.MinimumSize.Width,
                (int)Math.Ceiling(Math.Max(_minimumPageHeight * scale, contentHeight)));
            if (pagePanel.MinimumSize != minimumSize) pagePanel.MinimumSize = minimumSize;
        }

        private void ClearLogButton_Click(object? sender, EventArgs e)
        {
            _messages.Clear();
            _logList.Clear();
            UpdateMessageSummary();
            // 第三方按钮不依赖主窗体对原生 AntdUI.Button 的监听，操作仍记录一次。
            OperationJournal.Record("自动运行", "点击 清空本页消息");
        }

        // 发送事件保留在设计器对应的代码文件中，只维护这一份实现。
        private async void SendWaveButton_Click(object? sender, EventArgs e)
        {
            if (_waveSignal is null || _waveService is null) return;
            await RunWaveUiAsync("发送造波信号", () => _waveService.UploadWaveAsync(_waveSignal));
        }
    }
}
