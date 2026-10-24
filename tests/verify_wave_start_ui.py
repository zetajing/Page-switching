"""链接真实造波页面逻辑，检查开始门槛和中文原因，不连接或写入 PLC。"""
from pathlib import Path
import os
import subprocess
import tempfile
import unittest
from xml.sax.saxutils import escape

ROOT = Path(__file__).resolve().parents[1]


class WaveStartUiChecks(unittest.TestCase):
    def test_actual_start_conditions_and_reasons(self):
        packages = Path(os.environ.get("NUGET_PACKAGES", Path.home() / ".nuget/packages"))
        antdui = packages / "antdui/2.4.12/lib/net8.0-windows7.0/AntdUI.dll"
        self.assertTrue(antdui.is_file(), f"缺少本地 AntdUI 2.4.12：{antdui}")
        sources = ["panel/auto.Wave.cs", "Axis/Feedback/PlcMachineMonitor.cs", "Axis/Feedback/PlcWaveDiagnostics.cs",
                   "Axis/Feedback/MachineMonitorSnapshot.cs", "Axis/Feedback/AxisSnapshot.cs",
                   "Wave/Legacy/WaveTrajectoryFile.cs", "Axis/Trajectory/WaveCommandChecks.cs",
                   "Controls/AxisMonitoringSummaryControl.cs", "Controls/AxisMonitoringSummaryControl.Designer.cs",
                   "Axis/Monitoring/AxisMonitoringSettings.cs", "Axis/Monitoring/AxisMonitoringReport.cs", "Infrastructure/LoginUser.cs"]
        with tempfile.TemporaryDirectory(prefix="PageSwitching_WaveStartUI_") as directory:
            folder = Path(directory)
            links = "".join(f'<Compile Include="{escape(str(ROOT / name))}" />' for name in sources)
            (folder / "Probe.csproj").write_text(
                '<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup>'
                '<OutputType>Exe</OutputType><TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>'
                '<UseWindowsForms>true</UseWindowsForms><ImplicitUsings>enable</ImplicitUsings>'
                '<Nullable>enable</Nullable><TreatWarningsAsErrors>true</TreatWarningsAsErrors>'
                '</PropertyGroup><ItemGroup>' + links +
                '<Reference Include="AntdUI"><HintPath>' + escape(str(antdui)) + '</HintPath></Reference>'
                '</ItemGroup></Project>', encoding="utf-8")
            (folder / "Program.cs").write_text(
                (ROOT / "tests/wave_start_ui_checks.cs.txt").read_text(encoding="utf-8"), encoding="utf-8")
            result = subprocess.run(
                ["dotnet", "run", "--project", str(folder / "Probe.csproj"), "-c", "Release", "-v", "quiet"],
                capture_output=True, text=True, encoding="utf-8", timeout=90)
            self.assertEqual(result.returncode, 0, result.stdout + result.stderr)
            print(result.stdout.strip())


if __name__ == "__main__":
    unittest.main(verbosity=2)
