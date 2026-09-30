using System.Configuration;

namespace Page_switching;

// 只保存波形计算程序的目录，不包含PLC或机械参数。
internal static class WaveProgramSettings
{
    internal static string WorkDirectory => Path.Combine(Environment.GetFolderPath(
        Environment.SpecialFolder.LocalApplicationData), "PageSwitching", "WavePrograms");

    internal static string ProgramDirectory
    {
        get
        {
            var configured = ConfigurationManager.AppSettings["WaveProgramDirectory"]?.Trim();
            if (!string.IsNullOrEmpty(configured)) return configured;
            var source = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                "宁波材料所", "上位机程序20241111_开展到210w", "MYAPP_TJU_20240609_5_中断型_1AXIS_2m",
                "MYAPP_TJU_20240609_5_中断型_1AXIS_2m", "bin", "Debug");
            return Directory.Exists(source) ? source : "";
        }
    }
}
