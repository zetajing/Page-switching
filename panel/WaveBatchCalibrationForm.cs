namespace Page_switching.panel;

public partial class WaveBatchCalibrationForm : Form
{
    public WaveBatchCalibrationForm()
    {
        InitializeComponent();
        channels.SetItemChecked(0, true);
        addButton.Click += (_, _) =>
        {
            var index = points.Rows.Add();
            FillRaw(points.Rows[index]);
            OperationJournal.Record("批量标定", "添加共同水位标定点。");
        };
        readButton.Click += (_, _) =>
        {
            if (points.CurrentRow is { IsNewRow: false } row) FillRaw(row);
            OperationJournal.Record("批量标定", "读取最近收到的真实通道原始值。");
        };
        removeButton.Click += (_, _) =>
        {
            foreach (DataGridViewRow row in points.SelectedRows) if (!row.IsNewRow) points.Rows.Remove(row);
            OperationJournal.Record("批量标定", "删除所选标定点。");
        };
        saveButton.Click += (_, _) => Save();
        channels.ItemCheck += (_, e) => OperationJournal.Record("批量标定",
            channels.Items[e.Index] + "：" + (e.NewValue == CheckState.Checked ? "选中" : "取消"));
        points.CellEndEdit += (_, e) => OperationJournal.Record("批量标定",
            $"第{e.RowIndex + 1}行{points.Columns[e.ColumnIndex].HeaderText}：{points.Rows[e.RowIndex].Cells[e.ColumnIndex].Value}");
    }

    private void FillRaw(DataGridViewRow row)
    {
        foreach (var item in channels.CheckedIndices.Cast<int>())
        {
            var raw = WaveDataWorkspace.Shared.GetLatestRaw(item + 1);
            if (raw.HasValue) row.Cells[item + 1].Value = raw.Value;
        }
        status.Text = "已填入收到过的原始值。CH2–6协议待验证，缺少真实值的通道需手工录入；共同水位单位为mm。";
    }

    private void Save()
    {
        try
        {
            points.EndEdit();
            var workspace = WaveDataWorkspace.Shared;
            var profiles = new List<WaveCalibrationProfile>();
            foreach (var item in channels.CheckedIndices.Cast<int>())
            {
                var profile = workspace.GetCalibration(item + 1);
                profile.Points = [];
                foreach (DataGridViewRow row in points.Rows)
                {
                    if (row.IsNewRow) continue;
                    try
                    {
                        profile.Points.Add(new WaveCalibrationPoint(WaveSeriesFile.Number(Convert.ToString(row.Cells[item + 1].Value) ?? ""),
                            WaveSeriesFile.Number(Convert.ToString(row.Cells[0].Value) ?? "")));
                    }
                    catch (Exception ex) { throw new InvalidOperationException($"第{row.Index + 1}行CH{item + 1}：{ex.Message}", ex); }
                }
                profile.Fit();
                profiles.Add(profile);
            }
            workspace.SaveBatchCalibration(profiles);
            status.Text = "批量标定已保存：" + string.Join("、", profiles.Select(x => $"CH{x.Channel} k={x.Slope:0.######} b={x.Intercept:0.######}"));
            OperationJournal.Record("批量标定", status.Text);
        }
        catch (Exception ex) { status.Text = "批量标定失败：" + ex.Message; OperationJournal.Record("批量标定", status.Text); }
    }
}
