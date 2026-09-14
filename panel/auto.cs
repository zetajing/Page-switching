using System.Configuration;
using System.Globalization;
using System.Windows.Forms;
using InduLink.Abstractions;
using InduLink.Protocols.S7;

namespace Page_switching
{
    public partial class Auto : UserControl
    {
        private Func<SiemensS7Client?> _getClient = static () => null;
        private readonly System.Windows.Forms.Timer _refreshTimer;
        private readonly string _positionAddress;
        private readonly string _speedAddress;
        private readonly string _targetPositionAddress;
        private bool _refreshInProgress;

        public Auto()
        {
            InitializeComponent();

            _positionAddress = "DB1.DBD8";
            _speedAddress = "DB1.DBD12";
            _targetPositionAddress = "DB1.DBD16";

            var interval = Convert.ToInt32(ConfigurationManager.AppSettings["AutoRefreshIntervalMs"]);


            _refreshTimer = new System.Windows.Forms.Timer { Interval = interval };
            _refreshTimer.Tick += RefreshTimer_Tick;
            VisibleChanged += Auto_VisibleChanged;
            ParentChanged += Auto_ParentChanged;
            Disposed += Auto_Disposed;
        }

        public Auto(Func<SiemensS7Client?> getClient)
            : this()
        {
            _getClient = getClient ?? throw new ArgumentNullException(nameof(getClient));
        }

        private void Auto_VisibleChanged(object? sender, EventArgs e)
        {
            UpdateRefreshTimerState();
        }

        private void Auto_ParentChanged(object? sender, EventArgs e)
        {
            UpdateRefreshTimerState();
        }

        private void UpdateRefreshTimerState()
        {
            if (!Visible || Parent is null)
            {
                _refreshTimer.Stop();
                return;
            }

            _refreshTimer.Start();
            _ = RefreshValuesAsync();
        }

        private async void RefreshTimer_Tick(object? sender, EventArgs e)
        {
            await RefreshValuesAsync();
        }

        private async Task RefreshValuesAsync()
        {
            if (_refreshInProgress || IsDisposed)
            {
                return;
            }

            var client = _getClient();
            if (client is null || !client.IsConnected)
            {
                SetValue(axis_Location, "--");
                SetValue(axis_Speed, "--");
                SetValue(axis_TargetPosition, "--");
                return;
            }

            _refreshInProgress = true;

            try
            {
                var result = await client.ReadManyAsync(
                    new[]
                    {
                        new ReadRequest(client.DeviceId, _positionAddress, DataType.Float),
                        new ReadRequest(client.DeviceId, _speedAddress, DataType.Float),
                        new ReadRequest(client.DeviceId, _targetPositionAddress, DataType.Float),
                    },
                    CancellationToken.None);

                if (IsDisposed)
                {
                    return;
                }

                SetValue(axis_Location, FormatValue(result.Values[0].Value));
                SetValue(axis_Speed, FormatValue(result.Values[1].Value));
                SetValue(axis_TargetPosition, FormatValue(result.Values[2].Value));
            }
            catch (Exception)
            {
                if (!IsDisposed)
                {
                    SetValue(axis_Location, "读取失败");
                    SetValue(axis_Speed, "读取失败");
                    SetValue(axis_TargetPosition, "读取失败");
                }
            }
            finally
            {
                _refreshInProgress = false;
            }
        }

        private static void SetValue(TextBox textBox, string value)
        {
            if (!textBox.IsDisposed && textBox.Text != value)
            {
                textBox.Text = value;
            }
        }

        private static string FormatValue(object? value)
        {
            return value switch
            {
                null => "--",
                IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture) ?? "--",
                _ => value.ToString() ?? "--"
            };
        }

        private void Auto_Disposed(object? sender, EventArgs e)
        {
            _refreshTimer.Stop();
            _refreshTimer.Dispose();
        }
    }
}
