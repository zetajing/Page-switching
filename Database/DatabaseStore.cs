using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.Json;
using InduLink.Abstractions;
using InduLink.Storage;

namespace Page_switching;

// 业务只负责归档与记录转换，建表、参数化SQL、事务写入和历史查询由Total的DLL执行。
internal static class DatabaseStore
{
    internal static readonly string[] Names = { "操作日志", "造波文件", "目标点位", "造波运行", "轴实际反馈", "传感器任务", "传感器数据", "超限报告" };
    private static readonly string[] Tables = { "WaveOperationHistory", "WaveFileHistory", "WavePointBlocks", "WaveRunHistory", "WaveAxisHistory", "WaveSensorSessionHistory", "WaveSensorHistory", "WaveMonitorReports" };
    internal static bool Enabled => bool.TryParse(ConfigurationManager.AppSettings["DatabaseEnabled"], out bool enabled) && enabled;
    internal static string ConnectionString => Normalize(ConfigurationManager.AppSettings["DatabaseConnectionString"] ?? "");
    internal static string Normalize(string text)
    {
        var builder = new SqlConnectionStringBuilder(text) { ConnectTimeout = 3 };
        if (string.IsNullOrWhiteSpace(builder.DataSource) || string.IsNullOrWhiteSpace(builder.InitialCatalog))
            throw new InvalidOperationException("请填写SQL Server服务器和数据库名称。");
        return builder.ConnectionString;
    }
    internal static IInduLinkHistoryStore Open(int kind, string connectionString)
    {
        if (kind < 0 || kind >= Tables.Length) throw new ArgumentOutOfRangeException(nameof(kind));
        return new SqlServerInduLinkDataStore(new SqlServerDataStoreOptions
        { ConnectionString = Normalize(connectionString), TableName = "dbo." + Tables[kind], CommandTimeoutSeconds = 10 });
    }
    internal static async Task InitializeAsync(string connectionString, CancellationToken token)
    {
        for (int kind = 0; kind < Tables.Length; kind++)
        {
            using var store = Open(kind, connectionString);
            await store.InitializeAsync(token).ConfigureAwait(false);
        }
    }

    internal static InduLinkDataRecord JsonRecord(string id, string address, DateTimeOffset at, object value,
        QualityStatus quality = QualityStatus.Good, ProtocolKind protocol = ProtocolKind.TwinCatAds) => new()
    {
        DeviceId = id, Address = address, Timestamp = at, Protocol = protocol, DataType = DataType.String,
        ValueText = JsonSerializer.Serialize(value), RawData = null!, Quality = quality, ErrorMessage = null!
    };

    // 同一批用固定地址标识。失败后本地记录不确认；下次先清理未确认批次再重放。
    // DLL的Delete和Write是两个事务，重试期间该批可能暂缺，查询须同时查看同步状态。
    internal static async Task ReplaceBatchAsync(IInduLinkDataStore store, string id, string batch,
        IReadOnlyCollection<InduLinkDataRecord> records, CancellationToken token, bool prefix = false)
    {
        await store.DeleteAsync(new HistoryQueryFilter { DeviceId = id, Address = batch, AddressMatchMode = prefix ? HistoryAddressMatchMode.Contains : HistoryAddressMatchMode.Exact }, token).ConfigureAwait(false);
        if (records.Count > 0) await store.WriteAsync(records, token).ConfigureAwait(false);
    }

    internal static IEnumerable<InduLinkDataRecord> PointBlocks(DatabaseWaveData data, string binary)
    {
        using var stream = File.OpenRead(binary);
        if (stream.Length != (long)data.PointCount * sizeof(double)) throw new InvalidDataException("点位归档长度不符。");
        int offset = 0;
        while (offset < data.PointCount)
        {
            int count = Math.Min(1000, data.PointCount - offset);
            var bytes = new byte[count * sizeof(double)]; stream.ReadExactly(bytes);
            var record = JsonRecord(data.DataId, "points/" + offset.ToString("D8", CultureInfo.InvariantCulture), data.CreatedAt,
                new { FirstPoint = offset, Count = count, data.SampleTime });
            record.RawData = bytes;
            yield return record;
            offset += count;
        }
    }

    internal static IEnumerable<InduLinkDataRecord> AxisRecords(Guid id, string csv)
    {
        foreach (object[] row in DatabaseRows.AxisSamples(id, csv))
        {
            var values = new Dictionary<string, object?>();
            string[] names = { "MonitorRunId", "SampleIndex", "ReadAt", "ReadMilliseconds", "Heartbeat", "Sequence", "Owner", "Running", "NcInvalid", "Outcome", "Axis1Position", "Axis1Speed", "Axis2Position", "Axis2Speed", "Axis3Position", "Axis3Speed", "Axis4Position", "Axis4Speed" };
            for (int i = 0; i < names.Length; i++) values[names[i]] = row[i] is DBNull ? null : row[i];
            string outcome = (string)row[9];
            yield return JsonRecord(id.ToString("N"), "axis/" + row[1], (DateTimeOffset)row[2], values,
                outcome == "有效完整样本" ? QualityStatus.Good : QualityStatus.Bad);
        }
    }
    internal static IEnumerable<InduLinkDataRecord> SensorRecords(WaveCaptureManifest manifest)
    {
        foreach (object[] row in DatabaseRows.SensorSamples(manifest))
            yield return JsonRecord(manifest.SessionId.ToString("N"), "sensor/" + row[1], (DateTimeOffset)row[2],
                new { manifest.SessionId, SampleIndex = row[1], Timestamp = row[2], Channel = row[3], RawCount = row[4] is DBNull ? null : row[4],
                    CalibratedValue = row[5] is DBNull ? null : row[5], CalibrationVersion = row[6] }, protocol: ProtocolKind.TcpSocket);
    }

    internal static async Task<DataTable> QueryAsync(int kind, DateTimeOffset from, DateTimeOffset to, string taskId, CancellationToken token)
    {
        if (to <= from) throw new InvalidOperationException("结束时间须晚于开始时间。");
        if (!Enabled) throw new InvalidOperationException("数据库未启用，请先由管理员保存数据库配置。");
        using var store = Open(kind, ConnectionString);
        string id = Guid.TryParse(taskId, out var guid) ? guid.ToString("N") : taskId.Trim();
        var records = await store.QueryAsync(new HistoryQueryFilter
        { FromTime = from, ToTime = to, DeviceId = kind == 0 ? "" : id,
            Address = kind == 0 ? id : "", AddressMatchMode = HistoryAddressMatchMode.Contains,
            MaxRows = kind == 2 ? 1 : 500 }, token).ConfigureAwait(false);
        var table = new DataTable();
        if (kind == 2)
        {
            table.Columns.Add("点位编号"); table.Columns.Add("点号", typeof(int)); table.Columns.Add("时间秒", typeof(double)); table.Columns.Add("目标位置", typeof(double));
            foreach (var record in records)
            {
                using var json = JsonDocument.Parse(record.ValueText);
                int first = json.RootElement.GetProperty("FirstPoint").GetInt32();
                int count = json.RootElement.GetProperty("Count").GetInt32();
                double period = json.RootElement.GetProperty("SampleTime").GetDouble();
                if (record.RawData is null || record.RawData.Length != count * sizeof(double)) throw new InvalidDataException("数据库点位块长度不符。");
                for (int i = 0; i < count; i++) table.Rows.Add(record.DeviceId, first + i, (first + i) * period, BitConverter.ToDouble(record.RawData, i * sizeof(double)));
            }
        }
        else
        {
            table.Columns.Add("时间", typeof(DateTimeOffset)); table.Columns.Add("任务编号"); table.Columns.Add("记录地址"); table.Columns.Add("质量"); table.Columns.Add("内容JSON");
            foreach (var record in records) table.Rows.Add(record.Timestamp, record.DeviceId, record.Address, record.Quality.ToString(), record.ValueText);
        }
        return table;
    }
}
