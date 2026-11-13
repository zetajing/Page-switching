"""链接真实监控源码，用不含网络连接的 ADS 替身验证重连与整帧数据质量。"""
from pathlib import Path
import subprocess
import tempfile
import unittest
from xml.sax.saxutils import escape

ROOT = Path(__file__).resolve().parents[1]
SOURCES = ["Axis/Feedback/MachineMonitorService.cs", "Axis/Feedback/MachineMonitorOptions.cs",
           "Axis/Feedback/MachineMonitorSnapshot.cs", "Axis/Feedback/PlcMachineMonitor.cs",
           "Axis/Feedback/AxisSnapshot.cs", "Infrastructure/AdsDiagnostics.cs"]


class AdsMonitorChecks(unittest.TestCase):
    def test_monitor_reconnect_and_feedback_quality(self):
        with tempfile.TemporaryDirectory(prefix="AdsMonitor_") as directory:
            folder = Path(directory)
            links = "".join(f'<Compile Include="{escape(str(ROOT / name))}" />' for name in SOURCES)
            (folder / "Probe.csproj").write_text(
                '<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup>'
                '<OutputType>Exe</OutputType><TargetFramework>net8.0</TargetFramework>'
                '<ImplicitUsings>enable</ImplicitUsings><Nullable>enable</Nullable>'
                '<TreatWarningsAsErrors>true</TreatWarningsAsErrors></PropertyGroup><ItemGroup>'
                + links + '</ItemGroup></Project>', encoding="utf-8")
            (folder / "Program.cs").write_text(
                (ROOT / "tests/ads_monitor_checks.cs.txt").read_text(encoding="utf-8"), encoding="utf-8")
            result = subprocess.run(
                ["dotnet", "run", "--project", str(folder / "Probe.csproj"), "-c", "Release", "-v", "quiet"],
                capture_output=True, text=True, encoding="utf-8", timeout=120)
            self.assertEqual(result.returncode, 0, result.stdout + result.stderr)
            print(result.stdout.strip())


if __name__ == "__main__":
    unittest.main(verbosity=2)
