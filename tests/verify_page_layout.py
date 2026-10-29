"""渲染主导航和常用页面，检查小窗口及缩放布局；不启动通信。"""
from pathlib import Path
import argparse
import subprocess
import tempfile

ROOT = Path(__file__).resolve().parents[1]

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument('assembly', type=Path)
    parser.add_argument('images', type=Path)
    parser.add_argument('--baseline', action='store_true')
    args = parser.parse_args()
    with tempfile.TemporaryDirectory(prefix='SwitchLayout_') as directory:
        folder = Path(directory)
        (folder / 'Probe.csproj').write_text('<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup>'
            '<OutputType>Exe</OutputType><TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>'
            '<UseWindowsForms>true</UseWindowsForms><ImplicitUsings>enable</ImplicitUsings>'
            '<Nullable>enable</Nullable></PropertyGroup><ItemGroup>'
            '<FrameworkReference Include="Microsoft.AspNetCore.App" /></ItemGroup></Project>', encoding='utf-8')
        (folder / 'Program.cs').write_text((ROOT / 'tests/page_layout_checks.cs.txt').read_text(encoding='utf-8'), encoding='utf-8')
        command = ['dotnet', 'run', '--project', str(folder / 'Probe.csproj'), '-v', 'quiet', '--',
                   str(args.assembly.resolve()), str(args.images.resolve())]
        if args.baseline:
            command.append('baseline')
        result = subprocess.run(command, capture_output=True, text=True, encoding='utf-8', timeout=120)
        print(result.stdout, end='')
        print(result.stderr, end='')
        raise SystemExit(result.returncode)
