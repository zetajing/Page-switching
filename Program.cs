namespace Page_switching
{
    internal static class Program
    {
        [STAThread]
        // 初始化 WinForms 运行环境并启动主窗体。
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // 页面使用统一浅色调，弹出菜单和日期选择器也采用同一主题。
            AntdUI.Config.Mode = AntdUI.TMode.Light;
            Application.Run(new Mainpage());
        }
    }
}
