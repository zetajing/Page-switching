namespace Page_switching
{
    internal static class Program
    {
        [STAThread]
        // 初始化 WinForms 运行环境并启动主窗体。
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Mainpage());
        }
    }
}
