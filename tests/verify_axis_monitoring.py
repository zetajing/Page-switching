"""真实采样/统计/配置代码，受控时钟及只读 ADS 替身；不连接真实 PLC。"""
from pathlib import Path
import subprocess
import tempfile
import unittest
from xml.sax.saxutils import escape

ROOT = Path(__file__).resolve().parents[1]
SOURCES = ['Axis/Monitoring/AxisMonitoringSettings.cs', 'Axis/Monitoring/AxisMonitoringReport.cs',
           'Axis/Monitoring/AxisMonitoringSource.cs', 'Axis/Monitoring/AxisRuntimeMonitoringService.cs',
           'Axis/AxisServiceOptions.cs', 'Axis/Feedback/PlcMachineMonitor.cs', 'Axis/Feedback/AxisSnapshot.cs',
           'Axis/Feedback/MachineMonitorSnapshot.cs', 'Infrastructure/LoginUser.cs']


class AxisMonitoringChecks(unittest.TestCase):
    def test_actual_monitoring_with_controlled_clock_and_fake_ads(self):
        with tempfile.TemporaryDirectory(prefix='AxisMonitoring_') as directory:
            folder = Path(directory)
            links = ''.join(f'<Compile Include="{escape(str(ROOT / name))}" />' for name in SOURCES)
            (folder / 'Probe.csproj').write_text('<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup>'
                '<OutputType>Exe</OutputType><TargetFramework>net8.0</TargetFramework><ImplicitUsings>enable</ImplicitUsings>'
                '<Nullable>enable</Nullable><TreatWarningsAsErrors>true</TreatWarningsAsErrors></PropertyGroup><ItemGroup>'
                + links + '</ItemGroup></Project>', encoding='utf-8')
            (folder / 'Program.cs').write_text((ROOT / 'tests/axis_monitoring_checks.cs.txt').read_text(encoding='utf-8'), encoding='utf-8')
            result = subprocess.run(['dotnet', 'run', '--project', str(folder / 'Probe.csproj'), '-c', 'Release', '-v', 'quiet', '--', str(folder)],
                capture_output=True, text=True, encoding='utf-8', timeout=120)
            self.assertEqual(result.returncode, 0, result.stdout + result.stderr)
            print(result.stdout.strip())


if __name__ == '__main__': unittest.main(verbosity=2)
