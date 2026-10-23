"""只测本机文件解析和整表预检；使用零点位测试文件，不连接 PLC。"""
from pathlib import Path
import argparse
import hashlib
import json
import subprocess
import tempfile
from xml.sax.saxutils import escape

ROOT = Path(__file__).resolve().parents[1]


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--output', type=Path, default=ROOT / 'artifacts/roadmap-verification/pipeline-baseline.json')
    args = parser.parse_args()
    sources = ['Wave/Legacy/WaveTrajectoryFile.cs', 'Wave/Legacy/WavePointValidator.cs']
    with tempfile.TemporaryDirectory(prefix='WavePipelineBenchmark_') as directory:
        folder = Path(directory)
        links = ''.join(f'<Compile Include="{escape(str(ROOT / name))}" />' for name in sources)
        (folder / 'Probe.csproj').write_text('<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup>'
            '<OutputType>Exe</OutputType><TargetFramework>net8.0</TargetFramework><ImplicitUsings>enable</ImplicitUsings>'
            '<Nullable>enable</Nullable><TreatWarningsAsErrors>true</TreatWarningsAsErrors>'
            '</PropertyGroup><ItemGroup>' + links + '</ItemGroup></Project>', encoding='utf-8')
        (folder / 'Program.cs').write_text((ROOT / 'tests/wave_pipeline_benchmark.cs.txt').read_text(encoding='utf-8'), encoding='utf-8')
        result = subprocess.run(['dotnet', 'run', '--project', str(folder / 'Probe.csproj'), '-c', 'Release', '-v', 'quiet', '--', str(folder)],
            capture_output=True, text=True, encoding='utf-8', timeout=120)
        if result.returncode:
            raise RuntimeError(result.stdout + result.stderr)
        report = json.loads((folder / 'result.json').read_text(encoding='utf-8'))
        report['source_sha256'] = {name: hashlib.sha256((ROOT / name).read_bytes()).hexdigest() for name in sources}
        report['git_head_at_measurement'] = subprocess.check_output(['git', 'rev-parse', 'HEAD'], cwd=ROOT, text=True).strip()
        report['working_tree_was_dirty'] = bool(subprocess.check_output(['git', 'status', '--porcelain'], cwd=ROOT, text=True).strip())
        output = args.output.resolve()
        output.parent.mkdir(parents=True, exist_ok=True)
        output.write_text(json.dumps(report, ensure_ascii=False, indent=2) + '\n', encoding='utf-8')
        print(result.stdout.strip())
        print('Report: ' + str(output))


if __name__ == '__main__':
    main()
