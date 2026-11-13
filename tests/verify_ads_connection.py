"""链接实际轴连接源码，用离线 ADS 替身检查首次连接重试与客户端清理。"""
from pathlib import Path
import subprocess
import tempfile
import unittest
from xml.sax.saxutils import escape

ROOT = Path(__file__).resolve().parents[1]


class AdsConnectionChecks(unittest.TestCase):
    def test_actual_connection_lifecycle(self):
        with tempfile.TemporaryDirectory(prefix="PageSwitching_AdsConnection_") as directory:
            folder = Path(directory)
            links = "".join(
                f'<Compile Include="{escape(str(ROOT / name))}" />'
                for name in ("Axis/AxisService.cs", "Infrastructure/AdsDiagnostics.cs")
            )
            (folder / "Probe.csproj").write_text(
                '<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup>'
                '<OutputType>Exe</OutputType><TargetFramework>net8.0</TargetFramework>'
                '<ImplicitUsings>enable</ImplicitUsings><Nullable>enable</Nullable>'
                '<TreatWarningsAsErrors>true</TreatWarningsAsErrors>'
                '</PropertyGroup><ItemGroup>' + links + '</ItemGroup></Project>',
                encoding="utf-8",
            )
            (folder / "Program.cs").write_text(
                (ROOT / "tests/ads_connection_checks.cs.txt").read_text(encoding="utf-8"),
                encoding="utf-8",
            )
            result = subprocess.run(
                ["dotnet", "run", "--project", str(folder / "Probe.csproj"), "-c", "Release", "-v", "quiet"],
                capture_output=True, text=True, encoding="utf-8", timeout=90,
            )
            self.assertEqual(result.returncode, 0, result.stdout + result.stderr)
            print(result.stdout.strip())


if __name__ == "__main__":
    unittest.main(verbosity=2)
