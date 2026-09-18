using System.ComponentModel;
using LogHelper;

namespace Page_switching
{
    public partial class Auto : UserControl
    {
        // 初始化自动页面控件。
        public Auto()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                AddLog("自动运行页面已就绪");
            }
        }

        // 向自动运行日志区域追加一条带时间的消息。
        public void AddLog(string message)
        {
            if (IsDisposed)
            {
                return;
            }

            if (InvokeRequired)
            {
                BeginInvoke(() => AddLog(message));
                return;
            }

            _logList.Items.Add($"{DateTime.Now:HH:mm:ss}  {message}");
            try
            {
                LogDisplayHelper.ShowMsg($"[自动运行] {message}");
            }
            catch
            {
                // 本地日志组件异常时不影响自动页面显示。
            }

            if (_logList.Items.Count > 500)
            {
                _logList.Items.RemoveAt(0);
            }

            _logList.TopIndex = Math.Max(0, _logList.Items.Count - 1);
        }

        // 更新自动运行页面顶部的当前状态。
        public void SetRunState(string state, Color? color = null)
        {
            _runStateLabel.Text = state;
            _runStateLabel.ForeColor = color ?? Color.FromArgb(5, 150, 105);
            AddLog("状态：" + state);
        }

        // 清空自动运行页面中的内存日志列表。
        private void ClearLogButton_Click(object? sender, EventArgs e)
        {
            _logList.Items.Clear();
        }
    }
}
