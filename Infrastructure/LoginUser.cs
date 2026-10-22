namespace Page_switching;

// 软件账号角色与 PLC 控制权分别管理，管理员也必须经过 PLC 授权才能操作设备。
internal enum UserRole
{
    Administrator,
    Operator
}

internal sealed record LoginUser(string UserName, UserRole Role)
{
    internal bool IsAdministrator => Role == UserRole.Administrator;
}
