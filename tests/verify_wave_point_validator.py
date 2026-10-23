"""链接真实点位预检器；只检查最终轴点位，不含 ADS、PLC 或波形生成。"""
from pathlib import Path
import argparse
import subprocess
import tempfile
import unittest
from xml.sax.saxutils import escape

ROOT = Path(__file__).resolve().parents[1]
LOCAL_FILE = None
VIRTUAL_FIXTURE = None


class WavePointValidatorChecks(unittest.TestCase):
    def test_actual_validator(self):
        with tempfile.TemporaryDirectory(prefix="PageSwitching_PointValidator_") as directory:
            folder = Path(directory)
            links = "".join('<Compile Include="' + escape(str(ROOT / name)) + '" />' for name in
                            ["Wave/Legacy/WavePointValidator.cs", "Wave/Legacy/WaveTrajectoryFile.cs"])
            (folder / "Probe.csproj").write_text(
                '<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup>'
                '<OutputType>Exe</OutputType><TargetFramework>net8.0</TargetFramework>'
                '<ImplicitUsings>enable</ImplicitUsings><Nullable>enable</Nullable>'
                '<TreatWarningsAsErrors>true</TreatWarningsAsErrors>'
                '</PropertyGroup><ItemGroup>' + links +
                '</ItemGroup></Project>', encoding="utf-8")
            (folder / "Program.cs").write_text(
                (ROOT / "tests/wave_point_validator_checks.cs.txt").read_text(encoding="utf-8"), encoding="utf-8")
            command = ["dotnet", "run", "--project", str(folder / "Probe.csproj"), "-c", "Release", "-v", "quiet"]
            if LOCAL_FILE is not None:
                command += ["--", str(LOCAL_FILE)]
            elif VIRTUAL_FIXTURE is not None:
                command += ["--", "--fixture", str(VIRTUAL_FIXTURE)]
            result = subprocess.run(command,
                capture_output=True, text=True, encoding="utf-8", timeout=120)
            self.assertEqual(result.returncode, 0, result.stdout + result.stderr)
            print(result.stdout.strip())


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("--file", type=Path, help="可选：只读检查本机已确认的 163831 点调试文件，不作为 CI 依赖。")
    parser.add_argument("--fixture", type=Path, help="可选：只读验证 create_virtual_axis_fixture.py 生成的固定验收文件。")
    options = parser.parse_args()
    LOCAL_FILE = options.file.resolve() if options.file else None
    VIRTUAL_FIXTURE = options.fixture.resolve() if options.fixture else None
    if LOCAL_FILE and VIRTUAL_FIXTURE: parser.error('--file 和 --fixture 只能选择一项')
    unittest.main(argv=[__file__], verbosity=2)
