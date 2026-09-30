using System.ComponentModel;
using System.Text.Json;

namespace Page_switching;

internal sealed class LegacyProjectSettings
{
    internal static string DirectoryPath => Path.Combine(Environment.GetFolderPath(
        Environment.SpecialFolder.LocalApplicationData), "PageSwitching", "Legacy");
    private static string FilePath => Path.Combine(DirectoryPath, "equipment.json");
    [Category("旧工程"), DisplayName("计算程序目录")]
    public string ProgramDirectory { get; set; } = "";
    [Category("水池布局"), DisplayName("平面形式")]
    public string PlanType { get; set; } = "水槽造波机";
    [Category("水池布局"), DisplayName("X边长(m)")]
    public double LengthX { get; set; } = 6;
    [Category("水池布局"), DisplayName("Y边长(m)")]
    public double LengthY { get; set; } = 2;
    [Category("水池布局"), DisplayName("板宽(m)")]
    public double BoardWidth { get; set; } = 2;
    [Category("水池布局"), DisplayName("圆半径(m)")]
    public double Radius { get; set; } = 1;
    [Category("机械参数"), DisplayName("垂向形式")]
    public string VerticalType { get; set; } = "推板式";
    [Category("机械参数"), DisplayName("减速比")]
    public double GearRatio { get; set; } = 1;
    [Category("机械参数"), DisplayName("轮径(m)")]
    public double WheelDiameter { get; set; } = 0.1;
    [Category("机械参数"), DisplayName("臂半径(m)")]
    public double ArmRadius { get; set; } = 1;
    [Category("机械参数"), DisplayName("轴高(m)")]
    public double AxisHeight { get; set; } = 0;
    [Category("反馈传感器"), DisplayName("A斜率")]
    public double SensorASlope { get; set; } = 1;
    [Category("反馈传感器"), DisplayName("A截距")]
    public double SensorAIntercept { get; set; }
    [Category("反馈传感器"), DisplayName("B斜率")]
    public double SensorBSlope { get; set; } = 1;
    [Category("反馈传感器"), DisplayName("B截距")]
    public double SensorBIntercept { get; set; }
    [Browsable(false)]
    public List<WaveCalibrationPoint> SensorAPoints { get; set; } = [];
    [Browsable(false)]
    public List<WaveCalibrationPoint> SensorBPoints { get; set; } = [];
    [Category("信号生成"), DisplayName("生成吸收式信号(0.002s)")]
    public bool GenerateAbsorptionSignal { get; set; }

    internal static LegacyProjectSettings Load()
    {
        if (File.Exists(FilePath))
            return JsonSerializer.Deserialize<LegacyProjectSettings>(File.ReadAllText(FilePath))
                ?? throw new InvalidDataException("设备配置为空。");
        var old = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
            "宁波材料所", "上位机程序20241111_开展到210w", "MYAPP_TJU_20240609_5_中断型_1AXIS_2m",
            "MYAPP_TJU_20240609_5_中断型_1AXIS_2m", "bin", "Debug");
        return new LegacyProjectSettings { ProgramDirectory = Directory.Exists(old) ? old : "" };
    }

    internal void Save()
    {
        var numbers = new[] { LengthX, LengthY, BoardWidth, Radius, GearRatio, WheelDiameter,
            ArmRadius, AxisHeight, SensorASlope, SensorAIntercept, SensorBSlope, SensorBIntercept };
        if (numbers.Any(x => !double.IsFinite(x)) || LengthX <= 0 || LengthY <= 0 || BoardWidth <= 0 ||
            GearRatio <= 0 || WheelDiameter <= 0 || ArmRadius <= 0 || Radius <= 0)
            throw new InvalidOperationException("尺寸、减速比必须为有效的正数。");
        Directory.CreateDirectory(DirectoryPath);
        var temporary = FilePath + ".tmp";
        File.WriteAllText(temporary, JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true }));
        File.Move(temporary, FilePath, true);
    }
}
