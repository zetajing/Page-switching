using System.Globalization;
using System.Windows.Forms;
using InduLink.Abstractions;
using InduLink.Protocols.S7;

namespace Page_switching
{
    public partial class Manual : UserControl
    {
        private Func<SiemensS7Client?> _getClient = static () => null;

        public Manual()
        {
            InitializeComponent();
        }

        public Manual(Func<SiemensS7Client?> getClient)
            : this()
        {
            _getClient = getClient ?? throw new ArgumentNullException(nameof(getClient));
        }

        private async void ManualReadButton_Click(object? sender, EventArgs e)
        {
            await RunAsync(async () =>
            {
                var client = GetConnectedClient();
                var address = GetAddress();
                var dataType = GetDataType();
                var result = await client.ReadAsync(
                    new ReadRequest(client.DeviceId, address, dataType),
                    CancellationToken.None);

                AppendLog($"读取 {result.Address} = {FormatValue(result.Value)}，质量={result.Quality}");
            });
        }

        private async void ManualWriteButton_Click(object? sender, EventArgs e)
        {
            await RunAsync(async () =>
            {
                var client = GetConnectedClient();
                var address = GetAddress();
                var dataType = GetDataType();
                var value = ConvertValue(writeValueTextBox.Text.Trim(), dataType);

                await client.WriteAsync(
                    new WriteRequest(client.DeviceId, address, dataType, value),
                    CancellationToken.None);

                AppendLog($"写入 {address} = {FormatValue(value)} 成功");
            });
        }

        private SiemensS7Client GetConnectedClient()
        {
            var client = _getClient();
            if (client is null || !client.IsConnected)
            {
                throw new InvalidOperationException("请等待主页面连接 S7 PLC 完成。");
            }

            return client;
        }

        private string GetAddress()
        {
            var address = s7ipaddress.Text.Trim();
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new InvalidOperationException("请输入 S7 变量地址，例如 MX0.0。");
            }

            return address;
        }

        private DataType GetDataType()
        {
            if (!Enum.TryParse<DataType>(dataTypeComboBox.Text, out var dataType))
            {
                throw new InvalidOperationException("请选择有效的数据类型。");
            }

            return dataType;
        }

        private static object ConvertValue(string text, DataType dataType)
        {
            return dataType switch
            {
                DataType.Bool => ParseBool(text),
                DataType.Int16 => short.Parse(text, CultureInfo.InvariantCulture),
                DataType.UInt16 => ushort.Parse(text, CultureInfo.InvariantCulture),
                DataType.Int32 => int.Parse(text, CultureInfo.InvariantCulture),
                DataType.UInt32 => uint.Parse(text, CultureInfo.InvariantCulture),
                DataType.Float => float.Parse(text, CultureInfo.InvariantCulture),
                DataType.Double => double.Parse(text, CultureInfo.InvariantCulture),
                DataType.Byte => byte.Parse(text, CultureInfo.InvariantCulture),
                _ => throw new NotSupportedException("当前界面暂不支持该数据类型的写入。")
            };
        }

        private static bool ParseBool(string text)
        {
            if (bool.TryParse(text, out var result))
            {
                return result;
            }

            return text switch
            {
                "1" => true,
                "0" => false,
                _ => throw new FormatException("Bool 请输入 true、false、1 或 0。")
            };
        }

        private static string FormatValue(object? value)
        {
            return value switch
            {
                null => "(null)",
                byte[] bytes => Convert.ToHexString(bytes),
                IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture) ?? string.Empty,
                _ => value.ToString() ?? string.Empty
            };
        }

        private async Task RunAsync(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                AppendLog("错误：" + ex.Message);
            }
        }

        private void AppendLog(string message)
        {
            if (display_log.IsDisposed)
            {
                return;
            }

            display_log.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
        }
    }
}
