using System.Data;

namespace Page_switching;

// 四轴反馈直接绑定 AntdUI 表格；不提供编辑入口，设计器也能显示四条占位行。
public sealed class AxisFeedbackGrid : AntdUI.Table
{
    public DataTable FeedbackRows { get; } = new();

    public AxisFeedbackGrid()
    {
        var titles = new[] { "轴", "位置反馈", "速度反馈", "回零", "报警", "负限位", "正限位", "原点" };
        for (var index = 0; index < titles.Length; index++)
        {
            var key = "C" + index;
            FeedbackRows.Columns.Add(key, typeof(object));
            var column = new AntdUI.Column(key, titles[index])
            {
                ReadOnly = true,
                Width = index == 0 ? "40" : index <= 2 ? "20%" : "70",
                Align = index <= 2 ? AntdUI.ColumnAlign.Right : AntdUI.ColumnAlign.Center
            };
            if (index == 0) column.Width = "40";
            else if (index > 2) column.Width = "70";
            Columns.Add(column);
        }
        // 数值列填满剩余宽度，固定信号列不再挤出横向滚动条。
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
