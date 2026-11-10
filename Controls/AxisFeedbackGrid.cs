using System.Data;

namespace Page_switching;

// 四轴反馈直接绑定 AntdUI 表格；不提供编辑入口，设计器也能显示四条占位行。
public sealed class AxisFeedbackGrid : AntdUI.Table
{
    // 数据由控件自己创建，禁止 Designer 把占位行写入二进制资源。
    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public DataTable FeedbackRows { get; } = new();

    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public new object? DataSource
    {
        get => base.DataSource;
        set => base.DataSource = value;
    }

    public AxisFeedbackGrid()
    {
        var titles = new[] { "轴", "位置反馈", "速度反馈", "回零", "报警", "负限位", "正限位", "原点" };
        // 按表格宽度分配，预留少量空间给边距和像素取整，避免出现横向滚动条。
        var widths = new[] { "7%", "19%", "19%", "10%", "10%", "11%", "11%", "11%" };
        for (var index = 0; index < titles.Length; index++)
        {
            var key = "C" + index;
            FeedbackRows.Columns.Add(key, typeof(object));
            var column = new AntdUI.Column(key, titles[index])
            {
                ReadOnly = true,
                Width = widths[index],
                Align = index is 1 or 2 ? AntdUI.ColumnAlign.Right : AntdUI.ColumnAlign.Center
            };
            Columns.Add(column);
        }
        AutoSizeColumnsMode = AntdUI.ColumnsMode.Fill;
        DataSource = FeedbackRows;
        ShowTip = true;
        RowHeight = 28;
        RowHeightHeader = 28;
        Gap = 4;
        InitializeRows();
    }

    public void InitializeRows()
    {
        if (IsDisposed || FeedbackRows.Rows.Count != 0) return;
        for (var axis = 1; axis <= 4; axis++) FeedbackRows.Rows.Add(axis, "--", "--", "--", "--", "--", "--", "--");
        Refresh();
    }
}
