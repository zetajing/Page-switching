namespace Page_switching.panel
{

    public partial class WaveBatchCalibrationPage : UserControl
    {
        private readonly System.Data.DataTable pointsRows = new();
        private readonly AntdUI.Checkbox[] _channelChecks;
        // 这里只选择文件标定通道，不启用未确认的设备采集协议。
        private IEnumerable<int> SelectedChannels => Enumerable.Range(0, 6).Where(index => _channelChecks[index].Checked);

        public WaveBatchCalibrationPage()
        {
            InitializeComponent();
            InitializeTables();
            _channelChecks = [channel1Check, channel2Check, channel3Check, channel4Check, channel5Check, channel6Check];
            // 设计器只加载布局，运行时才绑定标定操作。
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            addButton.Click += (_, _) => AddPoint();
            readButton.Click += (_, _) => ReadSelectedPoint();
            removeButton.Click += (_, _) => RemoveSelectedPoints();
            saveButton.Click += (_, _) => Save();
            foreach (var check in _channelChecks)
                check.CheckedChanged += (_, _) => OperationJournal.Record("批量标定",
                    check.Text + "：" + (check.Checked ? "选中" : "取消"));
            points.CellEditComplete += (_, e) => OperationJournal.Record("批量标定",
                $"{points.Columns[e.ColumnIndex].Title}：{e.Record}");
        }

        private void InitializeTables()
        {
            TableColumns.Initialize(points, pointsRows, true,
                "共同水位(mm)", "CH1原始值", "CH2原始值", "CH3原始值", "CH4原始值", "CH5原始值", "CH6原始值");
            points.EditMode = AntdUI.TEditMode.DoubleClick;
            points.MultipleRows = true;
        }

        internal event EventHandler? BackRequested;
        // 保存成功后通知主窗体，避免单通道页面显示旧系数。
        internal event EventHandler? CalibrationSaved;

        private void BackButton_Click(object? sender, EventArgs e) =>
            BackRequested?.Invoke(this, e);

        // 新行仍保留七列：共同水位加六个文件标定通道。
        private void AddPoint()
        {
            var row = pointsRows.Rows.Add(new object[7]);
            FillRaw(row);
            OperationJournal.Record("批量标定", "添加共同水位标定点。");
        }

        private void ReadSelectedPoint()
        {
            var row = points.SelectedsReal().OfType<System.Data.DataRow>().FirstOrDefault();
            if (row is not null) FillRaw(row);
            OperationJournal.Record("批量标定", "读取最近收到的真实通道原始值。");
        }

        private void RemoveSelectedPoints()
        {
            // 先取出选择结果，再删除行，避免遍历过程中修改同一集合。
            foreach (var row in points.SelectedsReal().OfType<System.Data.DataRow>().ToArray())
                pointsRows.Rows.Remove(row);
            points.Refresh();
            OperationJournal.Record("批量标定", "删除所选标定点。");
        }

        private void FillRaw(System.Data.DataRow row)
        {
            foreach (var item in SelectedChannels)
            {
                var raw = WaveDataWorkspace.Shared.GetLatestRaw(item + 1);
                if (raw.HasValue) row[item + 1] = raw.Value;
            }
            points.Refresh();
            status.Text = "已填入收到过的原始值。CH2–6协议待验证，缺少真实值的通道需手工录入；共同水位单位为mm。";
        }

        private void Save()
        {
            try
            {
                points.EditModeClose();
                var workspace = WaveDataWorkspace.Shared;
                var profiles = new List<WaveCalibrationProfile>();
                foreach (var item in SelectedChannels)
                {
                    var profile = workspace.GetCalibration(item + 1);
                    profile.Points = [];
                    foreach (System.Data.DataRow row in pointsRows.Rows)
                    {

                        try
                        {
                            profile.Points.Add(new WaveCalibrationPoint(WaveSeriesFile.Number(Convert.ToString(row[item + 1]) ?? ""),
                                WaveSeriesFile.Number(Convert.ToString(row[0]) ?? "")));
                        }
                        catch (Exception ex) { throw new InvalidOperationException($"第{pointsRows.Rows.IndexOf(row) + 1}行CH{item + 1}：{ex.Message}", ex); }
                    }
                    profile.Fit();
                    profiles.Add(profile);
                }
                workspace.SaveBatchCalibration(profiles);
                status.Text = "批量标定已保存：" + string.Join("、", profiles.Select(x => $"CH{x.Channel} k={x.Slope:0.######} b={x.Intercept:0.######}"));
                OperationJournal.Record("批量标定", status.Text);
                CalibrationSaved?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex) { status.Text = "批量标定失败：" + ex.Message; OperationJournal.Record("批量标定", status.Text); }
        }
    }
}
