using System.Data;

namespace Page_switching;

internal static class TableColumns
{
    // 数据表和界面使用相同列键；保留 object 类型，兼容各页已有的字符串和数值行。
    internal static void Initialize(AntdUI.Table table, DataTable rows, bool editable, params string[] titles)
    {
        for (var index = 0; index < titles.Length; index++)
        {
            var key = "C" + index;
            rows.Columns.Add(key, typeof(object));
            table.Columns.Add(new AntdUI.Column(key, titles[index])
            {
                Editable = editable,
                ReadOnly = !editable
            });
        }

        table.DataSource = rows;
    }
}
