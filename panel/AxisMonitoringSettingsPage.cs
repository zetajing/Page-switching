using System.Globalization;

namespace Page_switching.panel;

// 独立分析阈值；管理员保存，运行中保存的版本仅供下一场使用。
public partial class AxisMonitoringSettingsPage : UserControl
{
    private readonly LoginUser? _user;
    public AxisMonitoringSettingsPage() : this(null) { }
    internal AxisMonitoringSettingsPage(LoginUser? user)
    {
        _user = user;
        InitializeComponent();
        thresholdsGrid.ReadOnly = user?.IsAdministrator != true;
        saveButton.Enabled = user?.IsAdministrator == true;
        var loaded = AxisMonitoringSettings.Load();
        for (int axis = 0; axis < 4; axis++)
        {
            var threshold = loaded.Profile?.Axes[axis];
            thresholdsGrid.Rows.Add(axis + 1, Number(threshold?.MinPosition), Number(threshold?.MaxPosition), Number(threshold?.MaxSpeed));
        }
        stateLabel.Text = loaded.Error ?? "当前监测阈值版本：" + loaded.Profile!.Version;
    }
    private static string Number(double? value) => value?.ToString("G17", CultureInfo.InvariantCulture) ?? "";

    // 返回系统配置仍由主窗体切换，不创建额外窗口。
    internal event EventHandler? BackRequested;
    private void BackButton_Click(object? sender, EventArgs e) => BackRequested?.Invoke(this, e);
    private void SaveButton_Click(object? sender, EventArgs e)
    {
        try
        {
            thresholdsGrid.EndEdit();
            var axes = new AxisMonitoringThreshold[4];
            for (int axis = 0; axis < 4; axis++) axes[axis] = new(Read(axis, 1), Read(axis, 2), Read(axis, 3));
            var profile = AxisMonitoringSettings.Save(axes, _user);
            stateLabel.ForeColor = UiPalette.Success;
            stateLabel.Text = "已保存，下次运行生效。版本：" + profile.Version + "；PLC运动参数未修改。";
        }
        catch (Exception ex)
        {
            stateLabel.ForeColor = UiPalette.Danger; stateLabel.Text = "保存失败：" + ex.Message;
            OperationJournal.Record("运行监测配置", stateLabel.Text);
        }
    }
    private double Read(int axis, int column)
    {
        string text = thresholdsGrid.Rows[axis].Cells[column].Value?.ToString() ?? "";
        if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double value) || !double.IsFinite(value))
            throw new InvalidDataException($"轴 {axis + 1} {thresholdsGrid.Columns[column].HeaderText}须填写有限数值。");
        return value;
    }
}
