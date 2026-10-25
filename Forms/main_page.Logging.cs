using System.Diagnostics;
using Page_switching.panel;

namespace Page_switching
{
    // 主窗体的操作日志部分；与 main_page.cs 是同一个 Mainpage 实例。
    public partial class Mainpage
    {
        private static readonly HashSet<string> ResultLabels = new HashSet<string>
        {
            "regularStatusLabel", "irregularStatusLabel",
            "result", "status", "_hintLabel", "_connectionStatusLabel"
        };

        // 先接入日志通知，后续服务和页面初始化产生的记录也能被主窗体接收。
        private void InitializeOperationJournal()
        {
            if (!_recordOperations) return;
            operationPathLabel.Text = "日志保存位置：" + OperationJournal.DirectoryPath;
            OperationJournal.EntryAdded += AddOperationEntry;
            OperationJournal.Record("系统", "程序启动");
        }

        // 仅在页面创建后注册一次，切页时复用原页面和事件。
        private void RegisterPageLogging()
        {
            if (!_recordOperations) return;
            foreach (var page in _pages)
                TrackActions(page, page);
        }

        // 日志内容只写入文件；新记录通知仅用于同步配置后的保存目录。
        private void AddOperationEntry(string entry)
        {
            if (_closing || IsDisposed || Disposing) return;
            if (InvokeRequired)
            {
                PostToUi(() => AddOperationEntry(entry));
                return;
            }

            operationPathLabel.Text = "日志保存位置：" + OperationJournal.DirectoryPath;
        }

        private void OpenOperationFolderButton_Click(object? sender, EventArgs e)
        {
            try
            {
                var directory = OperationJournal.DirectoryPath;
                Directory.CreateDirectory(directory);
                Process.Start(new ProcessStartInfo(directory) { UseShellExecute = true });
                OperationJournal.Record("操作记录", "已打开日志目录");
            }
            catch (Exception ex)
            {
                OperationJournal.Record("操作记录", "打开目录失败：" + ex.Message);
            }
        }

        // 按控件类型分派日志注册，每个 case 内保留独立步骤，方便打断点。
        private static void TrackActions(UserControl page, Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                string pageName = GetPageName(page);
                switch (control)
                {
                    case AntdUI.Button button:
                        if (button.Name == "jogNegativeButton" || button.Name == "jogPositiveButton"
                            || button.Name == "clearLogButton" || button.Name == "resetViewButton")
                        {
                            break;
                        }
                        button.Click += (_, _) => RecordButtonClick(pageName, button);
                        break;

                    case AntdUI.Checkbox checkBox:
                        if (checkBox.Name == "autoScaleCheckBox" || page is WaveBatchCalibrationPage)
                        {
                            break;
                        }
                        checkBox.CheckedChanged += (_, _) => RecordCheckboxChange(pageName, checkBox);
                        break;

                    case AntdUI.Select comboBox:
                        // 只记录用户正在操作的下拉框，程序加载默认值时不额外记日志。
                        comboBox.SelectedIndexChanged += (_, _) => RecordSelectionChange(pageName, comboBox);
                        break;

                    case AntdUI.Radio radioButton:
                        radioButton.CheckedChanged += (_, _) => RecordRadioChange(pageName, radioButton);
                        break;

                    case AntdUI.Tabs tabs:
                        tabs.SelectedIndexChanged += (_, _) => RecordTabChange(pageName, tabs);
                        break;

                    case AntdUI.Table table:
                        if (page is WaveBatchCalibrationPage)
                        {
                            break;
                        }
                        table.CellEditComplete += (_, e) => RecordTableEdit(pageName, table, e.Record, e.ColumnIndex);
                        break;

                    case AntdUI.Label label:
                        if (ShouldTrackResultLabel(page, label))
                        {
                            label.TextChanged += (_, _) => RecordResultChange(pageName, label);
                        }
                        break;
                }

                TrackActions(page, control);
            }
        }

        // 已主动记录结果的页面不再从标签变化重复推断，其他页面保留原有日志。
        private static bool ShouldTrackResultLabel(UserControl page, AntdUI.Label label)
        {
            if (!ResultLabels.Contains(label.Name))
            {
                return false;
            }
            if (page is WaveAnalysisPage || page is SignalCorrectionPage || page is WaveBatchCalibrationPage)
            {
                return false;
            }
            if (page is WaveformPage && label.Name == "status")
            {
                return false;
            }
            return true;
        }

        // 绑定只保留一次转发并捕获所属页面；具名方法处理日志，隐藏页也沿用原页面名称。
        private static void RecordButtonClick(string pageName, AntdUI.Button button)
        {
            string text = (button.Text ?? "").Replace("\r", " ").Replace("\n", " ").Trim();
            OperationJournal.Record(pageName, "点击" + text);
        }

        private static void RecordCheckboxChange(string pageName, AntdUI.Checkbox checkBox)
        {
            string text = (checkBox.Text ?? "").Trim();
            string state = checkBox.Checked ? "选中" : "取消";
            OperationJournal.Record(pageName, text + "：" + state);
        }

        // 仅记录用户操作，程序加载选项时不额外产生选择日志。
        private static void RecordSelectionChange(string pageName, AntdUI.Select comboBox)
        {
            if (comboBox.HasFocus || comboBox.Expand)
            {
                OperationJournal.Record(pageName, "选择" + comboBox.Text);
            }
        }

        private static void RecordRadioChange(string pageName, AntdUI.Radio radioButton)
        {
            if (radioButton.Checked)
            {
                OperationJournal.Record(pageName, "选择" + radioButton.Text);
            }
        }

        private static void RecordTabChange(string pageName, AntdUI.Tabs tabs)
        {
            OperationJournal.Record(pageName, "切换到" + tabs.SelectedTab?.Text);
        }

        // 保留原来的 DataRow、行号和列标题，方便在写日志前检查实际修改值。
        private static void RecordTableEdit(string pageName, AntdUI.Table table, object? record, int columnIndex)
        {
            System.Data.DataRow? row = record as System.Data.DataRow;
            if (row != null)
            {
                int rowNumber = row.Table.Rows.IndexOf(row) + 1;
                string title = table.Columns[columnIndex].Title;
                OperationJournal.Record(pageName, $"第{rowNumber}行{title}：{row[columnIndex]}");
            }
        }

        private static void RecordResultChange(string pageName, AntdUI.Label label)
        {
            OperationJournal.Record(pageName, label.Text ?? "");
        }
    }
}
