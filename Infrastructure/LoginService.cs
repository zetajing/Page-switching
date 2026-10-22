using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Page_switching;

// 本机账号和角色只管理软件访问；PLC 控制权仍由原有站点协议决定。
internal sealed class LoginService
{
    private const int Iterations = 600000;
    private static readonly object AccountLock = new();
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
    private readonly string _filePath;

    internal LoginService(string? filePath = null)
    {
        _filePath = Path.GetFullPath(filePath ?? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "PageSwitching", "users.json"));
    }

    // 只有文件确实不存在才进入首次注册；损坏或读取失败必须报告错误。
    internal bool HasAccount
    {
        get
        {
            lock (AccountLock) return ReadStore() is not null;
        }
    }

    // 空库的第一个账号固定为管理员，已有账号库不允许重新注册覆盖。
    internal LoginUser CreateAccount(string userName, string password)
    {
        lock (AccountLock)
        {
            if (ReadStore() is not null)
                throw new InvalidOperationException("已经创建账号，请登录后由管理员管理账号。");
            userName = ValidateUserName(userName);
            ValidatePassword(password);
            var account = CreateAccountData(userName, password, UserRole.Administrator);
            var store = new AccountStore { Version = 1, Users = new List<AccountData> { account } };
            SaveStore(store, firstAccount: true);
            return new LoginUser(account.UserName, account.Role);
        }
    }

    internal LoginUser? ValidateLogin(string userName, string password)
    {
        lock (AccountLock)
        {
            var store = ReadStore();
            if (store is null || password is null || password.Length is < 8 or > 128)
                return null;
            if (userName is null || userName.Any(char.IsControl)) return null;
            var account = store.Users.Find(user => string.Equals(
                user.UserName, userName.Trim(), StringComparison.OrdinalIgnoreCase));
            if (account is null) return null;
            byte[] actual = HashPassword(password, Convert.FromBase64String(account.Salt));
            byte[] expected = Convert.FromBase64String(account.PasswordHash);
            if (!CryptographicOperations.FixedTimeEquals(actual, expected)) return null;
            return new LoginUser(account.UserName, account.Role);
        }
    }

    // 只返回账号和角色，不向页面暴露盐值或密码哈希。
    internal IReadOnlyList<LoginUser> GetUsers(LoginUser actor)
    {
        lock (AccountLock)
        {
            var store = RequireAdministrator(actor);
            return store.Users.Select(user => new LoginUser(user.UserName, user.Role)).ToArray();
        }
    }

    internal void AddUser(LoginUser actor, string userName, string password, UserRole role)
    {
        lock (AccountLock)
        {
            var store = RequireAdministrator(actor);
            userName = ValidateUserName(userName);
            ValidatePassword(password);
            if (role is not (UserRole.Administrator or UserRole.Operator))
                throw new ArgumentException("账号角色不正确。", nameof(role));
            if (store.Users.Any(user => string.Equals(user.UserName, userName, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("该账号已存在，请使用其他账号名。");
            store.Users.Add(CreateAccountData(userName, password, role));
            SaveStore(store);
        }
    }

    internal void ResetPassword(LoginUser actor, string userName, string password)
    {
        lock (AccountLock)
        {
            var store = RequireAdministrator(actor);
            userName = ValidateUserName(userName);
            ValidatePassword(password);
            var account = store.Users.Find(user => string.Equals(user.UserName, userName, StringComparison.OrdinalIgnoreCase))
                ?? throw new InvalidOperationException("账号不存在。");
            var replacement = CreateAccountData(account.UserName, password, account.Role);
            account.Salt = replacement.Salt;
            account.PasswordHash = replacement.PasswordHash;
            SaveStore(store);
        }
    }

    internal void DeleteUser(LoginUser actor, string userName)
    {
        lock (AccountLock)
        {
            var store = RequireAdministrator(actor);
            userName = ValidateUserName(userName);
            var account = store.Users.Find(user => string.Equals(user.UserName, userName, StringComparison.OrdinalIgnoreCase))
                ?? throw new InvalidOperationException("账号不存在。");
            if (account.Role == UserRole.Administrator && store.Users.Count(user => user.Role == UserRole.Administrator) == 1)
                throw new InvalidOperationException("不能删除最后一个管理员。");
            if (string.Equals(account.UserName, actor.UserName, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("不能删除当前登录账号。");
            store.Users.Remove(account);
            SaveStore(store);
        }
    }

    // 管理操作每次重读账号库，不能仅凭调用者携带的角色放行。
    private AccountStore RequireAdministrator(LoginUser actor)
    {
        var store = ReadStore() ?? throw new UnauthorizedAccessException("请先创建管理员账号。");
        var current = actor is null ? null : store.Users.Find(user => string.Equals(
            user.UserName, actor.UserName, StringComparison.OrdinalIgnoreCase));
        if (current is null || current.Role != UserRole.Administrator)
            throw new UnauthorizedAccessException("只有管理员可以管理账号。");
        return store;
    }

    private AccountStore? ReadStore()
    {
        try
        {
            using var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var store = JsonSerializer.Deserialize<AccountStore>(stream, JsonOptions);
            if (store is null || store.Version != 1 || store.Users is null || store.Users.Count == 0)
                throw new InvalidDataException("账号库版本或内容不正确。");
            var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var user in store.Users)
            {
                if (user is null || ValidateUserName(user.UserName) != user.UserName || !names.Add(user.UserName)
                    || user.Role is not (UserRole.Administrator or UserRole.Operator)
                    || Convert.FromBase64String(user.Salt).Length != 16
                    || Convert.FromBase64String(user.PasswordHash).Length != 32)
                    throw new InvalidDataException("账号记录格式不正确。");
            }
            if (!store.Users.Any(user => user.Role == UserRole.Administrator))
                throw new InvalidDataException("账号库缺少管理员。");
            return store;
        }
        catch (FileNotFoundException) { return null; }
        catch (DirectoryNotFoundException) { return null; }
        catch (Exception ex) when (ex is JsonException or FormatException or InvalidDataException or ArgumentException)
        {
            throw new InvalidDataException("本机账号资料损坏，请恢复账号资料后重试。", ex);
        }
    }

    // 先完整写入同目录临时文件，再替换旧文件，避免中途退出留下半份账号库。
    private void SaveStore(AccountStore store, bool firstAccount = false)
    {
        string directory = Path.GetDirectoryName(_filePath)!;
        Directory.CreateDirectory(directory);
        string temporaryPath = Path.Combine(directory, ".users-" + Guid.NewGuid().ToString("N") + ".tmp");
        try
        {
            using (var stream = new FileStream(temporaryPath, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                JsonSerializer.Serialize(stream, store, JsonOptions);
                stream.Flush(flushToDisk: true);
            }
            if (firstAccount)
                File.Move(temporaryPath, _filePath, overwrite: false);
            else
                File.Replace(temporaryPath, _filePath, destinationBackupFileName: null);
        }
        finally
        {
            // 保存失败也清理临时文件；保留原始异常，便于页面提示真正原因。
            try { File.Delete(temporaryPath); }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
        }
    }

    private static string ValidateUserName(string userName)
    {
        if (userName is null || userName.Any(char.IsControl))
            throw new ArgumentException("账号请填写 1～32 个字符，不能包含控制字符。");
        userName = userName.Trim();
        if (userName.Length is < 1 or > 32)
            throw new ArgumentException("账号请填写 1～32 个字符。");
        return userName;
    }

    private static void ValidatePassword(string password)
    {
        if (password is null || password.Length is < 8 or > 128)
            throw new ArgumentException("密码请填写 8～128 个字符。");
    }

    private static AccountData CreateAccountData(string userName, string password, UserRole role)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        return new AccountData
        {
            UserName = userName,
            Role = role,
            Salt = Convert.ToBase64String(salt),
            PasswordHash = Convert.ToBase64String(HashPassword(password, salt))
        };
    }

    // 固定使用 PBKDF2-SHA256；账号库只保存随机盐和哈希。
    private static byte[] HashPassword(string password, byte[] salt) =>
        Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, 32);

    private sealed class AccountStore
    {
        [JsonRequired] public int Version { get; set; }
        [JsonRequired] public List<AccountData> Users { get; set; } = new();
    }

    private sealed class AccountData
    {
        [JsonRequired] public string UserName { get; set; } = string.Empty;
        [JsonRequired] public UserRole Role { get; set; }
        [JsonRequired] public string Salt { get; set; } = string.Empty;
        [JsonRequired] public string PasswordHash { get; set; } = string.Empty;
    }
}
