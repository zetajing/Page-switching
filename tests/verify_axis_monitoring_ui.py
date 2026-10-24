"""真实配置窗体/造波页离线检查与截图，不连接 PLC、不写本机监测配置。"""
from pathlib import Path
import argparse
import subprocess
import tempfile

ROOT = Path(__file__).resolve().parents[1]

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument('assembly', type=Path)
    parser.add_argument('images', type=Path)
    args = parser.parse_args()
    with tempfile.TemporaryDirectory(prefix='AxisMonitoring_UI_') as directory:
        folder = Path(directory)
        (folder / 'Probe.csproj').write_text('<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup>'
            '<OutputType>Exe</OutputType><TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>'
            '<UseWindowsForms>true</UseWindowsForms><ImplicitUsings>enable</ImplicitUsings>'
            '<Nullable>enable</Nullable></PropertyGroup><ItemGroup>'
            '<FrameworkReference Include="Microsoft.AspNetCore.App" /></ItemGroup></Project>', encoding='utf-8')
        (folder / 'Program.cs').write_text((ROOT / 'tests/axis_monitoring_ui_checks.cs.txt').read_text(encoding='utf-8'), encoding='utf-8')
        result = subprocess.run(['dotnet', 'run', '--project', str(folder / 'Probe.csproj'), '-v', 'quiet', '--',
            str(args.assembly.resolve()), str(args.images.resolve())], capture_output=True, text=True, encoding='utf-8', timeout=120)
        print(result.stdout, end='')
        print(result.stderr, end='')
        raise SystemExit(result.returncode)
