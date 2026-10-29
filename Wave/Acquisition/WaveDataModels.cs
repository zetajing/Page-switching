namespace Page_switching;

// 标定、采样和会话的持久化数据类型；属性名对应已有 JSON/CSV 格式。
public sealed record WaveCalibrationPoint(double RawCount, double WaterLevel);

public sealed class WaveCalibrationProfile
{
    public int Channel { get; set; }
    public double ZeroRawCount { get; set; }
    public double Slope { get; set; } = 1;
    public double Intercept { get; set; }
    public string Version { get; set; } = Guid.NewGuid().ToString("N");
    public List<WaveCalibrationPoint> Points { get; set; } = [];

    // 先扣除零点，再应用已保存的线性系数；保持现有标定单位。
    public double Convert(int rawCount) => (rawCount - ZeroRawCount) * Slope + Intercept;

    public void Fit()
    {
        // 使用中心化最小二乘拟合，将扣除零点后的原始值换算成标定水位。
        if (Points.Count < 2)
            throw new InvalidOperationException("至少需要两个标定点。");
        var meanX = Points.Average(p => p.RawCount - ZeroRawCount);
        var meanY = Points.Average(p => p.WaterLevel);
        var denominator = Points.Sum(p => Math.Pow(p.RawCount - ZeroRawCount - meanX, 2));
        if (denominator < 1e-12)
            throw new InvalidOperationException("标定点的原始值不能全部相同。");
        Slope = Points.Sum(p => (p.RawCount - ZeroRawCount - meanX) * (p.WaterLevel - meanY)) / denominator;
        Intercept = meanY - Slope * meanX;
        Version = Guid.NewGuid().ToString("N");
    }
}

public sealed record WaveSample(
    Guid SessionId, int Channel, DateTimeOffset Timestamp, int? RawCount,
    double? CalibratedValue, string CalibrationVersion);

public sealed class WaveCaptureManifest
{
    public Guid? RunId { get; set; } // 开始采集时的本机造波任务；独立采集时为空。
    public int DatabaseDetailVersion { get; set; } // 旧版只同步索引为0，本版逐点数据成功后为1。
    public string DatabaseTarget { get; set; } = "";
    public Guid SessionId { get; set; }
    public string DeviceAddress { get; set; } = "";
    public DateTimeOffset StartedAt { get; set; }
    public DateTimeOffset? EndedAt { get; set; }
    public int SampleRateHz { get; set; }
    public int[] Channels { get; set; } = [];
    public long SampleCount { get; set; }
    public string CalibrationVersion { get; set; } = "";
    public string CsvPath { get; set; } = "";
    public string SyncStatus { get; set; } = "待同步";
    public string? SyncError { get; set; }
    public bool IsLegacyImport { get; set; }
}
