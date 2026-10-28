namespace Page_switching
{
    internal static class Program
    {
        // 临时屏蔽登录页；需要恢复账号验证时，将此开关改为 true。
        private static readonly bool LoginEnabled = false;

        [STAThread]
        // 根据登录开关确定软件使用者，再创建主窗体和启动通信。
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // 页面使用统一浅色调，弹出菜单和日期选择器也采用同一主题。
            AntdUI.Config.Mode = AntdUI.TMode.Light;
            LoginUser user;
            if (LoginEnabled)
            {
                using var login = new LoginForm();
                if (login.ShowDialog() != DialogResult.OK || login.User is null) return;
                user = login.User;
            }
            else
            {
                // 临时身份只开放软件页面，不写入账号库，也不授予 PLC 控制权。
                user = new LoginUser("临时免登录", UserRole.Administrator);
            }
            Application.Run(new Mainpage(user));
        }
    }
}
