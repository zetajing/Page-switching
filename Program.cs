namespace Page_switching
{
    internal static class Program
    {
        [STAThread]
        // 先登录，验证通过后才创建主窗体和启动通信。
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // 页面使用统一浅色调，弹出菜单和日期选择器也采用同一主题。
            AntdUI.Config.Mode = AntdUI.TMode.Light;
            LoginUser user;
            using (var login = new LoginForm())
            {
                if (login.ShowDialog() != DialogResult.OK || login.User is null) return;
                user = login.User;
            }
            Application.Run(new Mainpage(user));
        }
    }
}
