"""链接真实服务源码，使用离线 ADS 替身验证上传、准备、取消和停止。"""
from pathlib import Path
import subprocess
import tempfile
import unittest
from xml.sax.saxutils import escape
ROOT = Path(__file__).resolve().parents[1]
SWITCH = next(p for p in ROOT.parents if (p / "App.config").is_file())
class WaveServiceChecks(unittest.TestCase):
    def test_real_service_with_fake_ads(self):
        sources = ["Axis/AxisService.cs", "Axis/AxisService.Authority.cs", "Axis/AxisService.Trajectory.cs",
                   "Axis/Axis.cs", "Axis/AxisServiceOptions.cs",
                   "Axis/AxisSnapshot.cs", "Axis/PlcMachineMonitor.cs", "Axis/MachineMonitorSnapshot.cs",
                   "Wave/Legacy/WaveTrajectoryFile.cs"]
        with tempfile.TemporaryDirectory(prefix="TwinCAT_WaveService_") as directory:
            folder = Path(directory)
            links = "".join(f'<Compile Include="{escape(str(SWITCH / name))}" />' for name in sources)
            (folder / "Probe.csproj").write_text('<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup>'
                '<OutputType>Exe</OutputType><TargetFramework>net8.0</TargetFramework><ImplicitUsings>enable</ImplicitUsings>'
                '<Nullable>enable</Nullable></PropertyGroup><ItemGroup>' + links + '</ItemGroup></Project>', encoding="utf-8")
            (folder / "Program.cs").write_text((ROOT / "tests/wave_service_checks.cs.txt").read_text(encoding="utf-8"), encoding="utf-8")
            result = subprocess.run(["dotnet", "run", "--project", str(folder / "Probe.csproj"), "-c", "Release", "-v", "quiet"],
                                    capture_output=True, text=True, encoding="utf-8", timeout=120)
            self.assertEqual(result.returncode, 0, result.stdout + result.stderr)
            print(result.stdout.strip())
if __name__ == "__main__": unittest.main(verbosity=2)
