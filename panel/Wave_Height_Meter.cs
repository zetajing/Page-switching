using System.Configuration;
using System.Globalization;
using InduLink.Storage;
using Page_switching;

namespace Page_switching.panel
{
    public partial class Wave_Height_Meter : UserControl
    {
        private const int PreviewCapacity = 1000;
        private readonly List<double> _previewSamples = new(PreviewCapacity);
        private readonly CancellationTokenSource _lifetimeCancellation = new();
        private WaveDataWorkspace? _workspace;
        private WaveCaptureManifest? _lastSession;

        private WaveHeightMeterClient? _client;
        private bool _connecting;
        private bool _disposed;
        private bool _autoStopping;
        private long _totalSamplesReceived;

        public Wave_Height_Meter()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                _workspace = WaveDataWorkspace.Shared;
                LoadSettings();
                UpdateConnectionControls();
            }

            Disposed += (_, _) =>
            {
                _disposed = true;
                _lifetimeCancellation.Cancel();
                _client?.Dispose();
                _client = null;
                // 结束文件失败也继续释放页面资源，错误写入主窗体操作日志。
                CompleteSession();
                _lifetimeCancellation.Dispose();
            };
        }
        private void LoadSettings()
        {
            _ipAddressInput.Text = ReadSetting("WaveHeightMeterIp", "192.168.0.7");
            SetNumericValue(_portInput, ReadIntegerSetting("WaveHeightMeterPort", 502));
            SetNumericValue(_sampleRateInput, ReadIntegerSetting("WaveHeightMeterSampleRateHz", 50));
            _sampleRateLabel.Text = $"CH1 · {_sampleRateInput.Value:0} Hz";
            SetNumericValue(_sampleLimitInput, ReadIntegerSetting("WaveHeightMeterSampleCount", 4096));
        }

        private async void ConnectButton_Click(object? sender, EventArgs e)
        {
            if (_connecting || _client?.IsConnected == true)
            {
                return;
            }
            if (!channel1Selector.Checked)
            {
                _connectionStatusLabel.Text = "请先选择已验证的 CH1 通道。";
                return;
            }

            _connecting = true;
            _autoStopping = false;
            _totalSamplesReceived = 0;
            _sampleCountLabel.Text = "0";
            _latestValueLabel.Text = "--";
            channel1Selector.Text = "CH1\r\n等待采集";
            _previewSamples.Clear();
            _waveformPreview.ClearSamples();
            UpdateConnectionControls();
            _connectionStatusLabel.Text = "正在连接并启动采集…";
            _connectionStatusLabel.ForeColor = UiPalette.Primary;

            var client = new WaveHeightMeterClient();
            _client?.Dispose();
            _client = client;
            client.SampleReceived += Client_SampleReceived;
            client.ConnectionLost += Client_ConnectionLost;
            client.ProtocolDiagnostic += (_, message) =>
            {
                System.Diagnostics.Debug.WriteLine("浪高仪协议：" + message);
                try { LogDisplayHelper.ShowMsg("[浪高仪协议] " + message); }
                catch { /* Diagnostics must not interrupt the TCP receive loop. */ }
            };

            try
            {
                _lastSession = _workspace?.StartSession(
                    _ipAddressInput.Text.Trim() + ":" + _portInput.Value,
                    decimal.ToInt32(_sampleRateInput.Value), [1]);
                await client.ConnectAsync(
                    _ipAddressInput.Text.Trim(),
                    decimal.ToInt32(_portInput.Value),
                    decimal.ToInt32(_sampleRateInput.Value),
                    [1],
                    _lifetimeCancellation.Token);

                if (!IsDisposed)
                {
                    _connectionStatusLabel.Text = "设备已连接，正在接收数据";
                    _connectionStatusLabel.ForeColor = UiPalette.Success;
                    _sampleRateLabel.Text = $"CH1 · {_sampleRateInput.Value:0} Hz";
                }
            }
            catch (OperationCanceledException) when (_disposed)
            {
                // The page is closing while the socket connection is being established.
            }
            catch (Exception ex)
            {
                CompleteSession();
                client.Dispose();
                if (ReferenceEquals(_client, client))
                {
                    _client = null;
                }

                if (!IsDisposed)
                {
                    _connectionStatusLabel.Text = "连接失败：" + ex.Message;
                    _connectionStatusLabel.ForeColor = UiPalette.Danger;
                }
            }
            finally
            {
                _connecting = false;
                if (!IsDisposed)
                {
                    UpdateConnectionControls();
                }
            }
        }

        private async void DisconnectButton_Click(object? sender, EventArgs e)
        {
            var client = _client;
            if (client is null)
            {
                return;
            }

            _disconnectButton.Enabled = false;
            _connectionStatusLabel.Text = "正在停止采集并断开…";
            try
            {
                await client.DisconnectAsync();
                if (ReferenceEquals(_client, client))
                {
                    _client = null;
                }

                client.Dispose();
                CompleteSession();
                if (!IsDisposed)
                {
                    _connectionStatusLabel.Text = "设备已断开";
                    _connectionStatusLabel.ForeColor = UiPalette.Muted;
                }
            }
            catch (Exception ex)
            {
                CompleteSession();
                if (!IsDisposed)
                {
                    _connectionStatusLabel.Text = "断开时发生错误：" + ex.Message;
                    _connectionStatusLabel.ForeColor = UiPalette.Danger;
                }
            }
            finally
            {
                if (!IsDisposed)
                {
                    UpdateConnectionControls();
                }
            }
        }

        private void SaveSettingsButton_Click(object? sender, EventArgs e)
        {
            try
            {
                var ipAddress = _ipAddressInput.Text.Trim();
                if (!System.Net.IPAddress.TryParse(ipAddress, out _))
                {
                    throw new InvalidOperationException("请输入有效的 IPv4 或 IPv6 地址。");
                }

                var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configuration.AppSettings.Settings;
                SetSetting(settings, "WaveHeightMeterIp", ipAddress);
                SetSetting(settings, "WaveHeightMeterPort", decimal.ToInt32(_portInput.Value).ToString(CultureInfo.InvariantCulture));
                SetSetting(settings, "WaveHeightMeterSampleRateHz", decimal.ToInt32(_sampleRateInput.Value).ToString(CultureInfo.InvariantCulture));
                SetSetting(settings, "WaveHeightMeterSampleCount", decimal.ToInt32(_sampleLimitInput.Value).ToString(CultureInfo.InvariantCulture));
                configuration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                _connectionStatusLabel.Text = "浪高仪连接参数已保存";
                _connectionStatusLabel.ForeColor = UiPalette.Success;
            }
            catch (Exception ex)
            {
                _connectionStatusLabel.Text = "保存参数失败：" + ex.Message;
                _connectionStatusLabel.ForeColor = UiPalette.Danger;
            }
        }

        private void Client_SampleReceived(object? sender, WaveHeightSampleEventArgs e) =>
            PostToUi(() =>
            {
                if (ReferenceEquals(sender, _client) && !_autoStopping && _workspace?.ActiveSession is not null)
                    DisplaySample(e);
            });

        private void Client_ConnectionLost(object? sender, WaveHeightConnectionLostEventArgs e)
        {
            PostToUi(() =>
            {
                if (!ReferenceEquals(sender, _client))
                {
                    return;
                }

                _connectionStatusLabel.Text = e.Error is null
                    ? "设备连接已关闭"
                    : "连接中断：" + e.Error.Message;
                _connectionStatusLabel.ForeColor = UiPalette.Danger;
                CompleteSession();
                UpdateConnectionControls();
            });
        }

        private void DisplaySample(WaveHeightSampleEventArgs sample)
        {
            if (IsDisposed || _disposed)
            {
                return;
            }

            if (sample.Channel != 1)
            {
                System.Diagnostics.Debug.WriteLine($"忽略未验证通道 CH{sample.Channel} 的数据。");
                return;
            }
            WaveSample recorded;
            try
            {
                recorded = _workspace!.RecordRawSample(sample.Channel, sample.Timestamp, sample.RawCount);
            }
            catch (Exception ex)
            {
                _connectionStatusLabel.Text = "本地记录失败：" + ex.Message;
                _connectionStatusLabel.ForeColor = UiPalette.Danger;
                _ = DisconnectForStorageFailureAsync();
                return;
            }
            _totalSamplesReceived++;
            _latestValueLabel.Text = sample.RawCount.ToString("N0", CultureInfo.CurrentCulture);
            latestValueDetail.Text = recorded.CalibratedValue is { } level
                ? $"标定水位 {level:0.####} mm"
                : "未标定 · 16 位无符号计数";
            channel1Selector.Text = $"CH1\r\n{sample.RawCount:N0}";
            _sampleCountLabel.Text = _totalSamplesReceived.ToString("N0", CultureInfo.CurrentCulture);

            _previewSamples.Add(recorded.CalibratedValue ?? sample.RawCount);
            if (_previewSamples.Count > PreviewCapacity)
            {
                _previewSamples.RemoveAt(0);
            }

            var sampleRate = 1.0 / decimal.ToDouble(_sampleRateInput.Value);
            _waveformPreview.UnitText = recorded.CalibratedValue.HasValue ? "mm" : "count";
            _waveformPreview.SetSamples(_previewSamples, sampleRate);
            _chartStatusLabel.Text = $"累计收到 {_totalSamplesReceived:N0} 点 · 显示最近 {PreviewCapacity:N0} 点";
            if (!_autoStopping && _totalSamplesReceived >= _sampleLimitInput.Value)
            {
                _autoStopping = true;
                DisconnectButton_Click(this, EventArgs.Empty);
            }
        }

        private void ClearButton_Click(object? sender, EventArgs e)
        {
            _previewSamples.Clear();
            _latestValueLabel.Text = "--";
            _chartStatusLabel.Text = "预览已清空；采集记录保留在本地文件";
            _waveformPreview.ClearSamples();
        }

        private void ExportButton_Click(object? sender, EventArgs e)
        {
            var manifest = _workspace?.ActiveSession ?? _lastSession;
            if (manifest is null || manifest.SampleCount == 0 || !File.Exists(manifest.CsvPath))
            {
                _connectionStatusLabel.Text = "当前没有可导出的采样数据";
                _connectionStatusLabel.ForeColor = UiPalette.Warning;
                return;
            }

            using var dialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "csv",
                FileName = $"WaveHeight_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                Filter = "CSV 文件 (*.csv)|*.csv|所有文件 (*.*)|*.*",
                OverwritePrompt = true,
                Title = "导出浪高仪原始数据"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                // 将文件刷新放在异常处理内，磁盘错误以状态提示呈现。
                _workspace?.FlushActiveSession();
                File.Copy(manifest.CsvPath, dialog.FileName, true);
                _connectionStatusLabel.Text = $"已导出 {manifest.SampleCount:N0} 个采样点";
                _connectionStatusLabel.ForeColor = UiPalette.Success;
            }
            catch (Exception ex)
            {
                _connectionStatusLabel.Text = "导出失败：" + ex.Message;
                _connectionStatusLabel.ForeColor = UiPalette.Danger;
            }
        }

        private async Task DisconnectForStorageFailureAsync()
        {
            if (_autoStopping) return;
            _autoStopping = true;
            var client = _client;
            if (client is not null)
            {
                try { await client.DisconnectAsync(); }
                catch { /* The storage error is already visible to the operator. */ }
            }
            CompleteSession();
            if (!IsDisposed) UpdateConnectionControls();
        }

        private void CompleteSession()
        {
            try
            {
                var completed = _workspace?.EndSession();
                if (completed is null) return;
                _lastSession = completed;
                if (!_disposed) _ = SynchronizeSessionAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("结束采集文件失败：" + ex);
                OperationJournal.Record("浪高监测", "结束本地记录失败：" + ex.Message);
                if (!IsDisposed)
                    _connectionStatusLabel.Text = "结束本地记录失败：" + ex.Message;
            }
        }

        private async Task SynchronizeSessionAsync()
        {
            try { if (_workspace is not null) await _workspace.SynchronizeSessionsAsync(_lifetimeCancellation.Token); }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("采集记录待同步：" + ex.Message);
            }
        }

        private void PostToUi(Action action)
        {
            if (_disposed || IsDisposed)
            {
                return;
            }

            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(action);
                }
                else
                {
                    action();
                }
            }
            catch (InvalidOperationException)
            {
                // The form handle may be closing while the socket worker posts an update.
            }
        }

        private void UpdateConnectionControls()
        {
            var connected = _client?.IsConnected == true;
            _connectButton.Enabled = !_connecting && !connected;
            _disconnectButton.Enabled = connected && !_connecting;
            _ipAddressInput.Enabled = !_connecting && !connected;
            _portInput.Enabled = !_connecting && !connected;
            _sampleRateInput.Enabled = !_connecting && !connected;
            _sampleLimitInput.Enabled = !_connecting && !connected;
            _saveSettingsButton.Enabled = !_connecting && !connected;
            channel1Selector.Enabled = !_connecting && !connected;
        }

        private static void SetNumericValue(NumericUpDown input, int value)
        {
            input.Value = Math.Clamp(value, decimal.ToInt32(input.Minimum), decimal.ToInt32(input.Maximum));
        }

        private static string ReadSetting(string key, string fallback) =>
            ConfigurationManager.AppSettings[key]?.Trim() is { Length: > 0 } value ? value : fallback;

        private static int ReadIntegerSetting(string key, int fallback) =>
            int.TryParse(ConfigurationManager.AppSettings[key], NumberStyles.Integer,
                CultureInfo.InvariantCulture, out var value) ? value : fallback;

        private static void SetSetting(KeyValueConfigurationCollection settings, string key, string value)
        {
            if (settings[key] is null)
            {
                settings.Add(key, value);
            }
            else
            {
                settings[key]!.Value = value;
            }
        }
    }
}
