namespace Page_switching;

// 四轴反馈表：列和样式由 Designer 编辑，固定四行在设计器与运行时都可见。
public sealed class AxisFeedbackGrid : DataGridView
{
    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        InitializeRows();
    }

    protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
    {
        base.OnColumnAdded(e);
        // Designer 可能先创建控件再添加列；列变更结束后补齐四行。
        if (IsHandleCreated && Columns.Count == 8)
            BeginInvoke((Action)InitializeRows);
    }

    // 运行时也可直接初始化，不依赖界面消息循环；重复调用不会增加行数。
    public void InitializeRows()
    {
        if (IsDisposed || Columns.Count != 8 || Rows.Count != 0) return;
        Rows.Add(1, "--", "--", "--", "--", "--", "--", "--");
        Rows.Add(2, "--", "--", "--", "--", "--", "--", "--");
        Rows.Add(3, "--", "--", "--", "--", "--", "--", "--");
        Rows.Add(4, "--", "--", "--", "--", "--", "--", "--");
        ClearSelection();
    }
}
