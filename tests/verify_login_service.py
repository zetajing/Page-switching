"""链接实际账号服务源码，在临时目录验证持久化、登录和管理权限。"""
from pathlib import Path
import subprocess
import tempfile
import unittest
from xml.sax.saxutils import escape

ROOT = Path(__file__).resolve().parents[1]


class LoginServiceChecks(unittest.TestCase):
    def test_actual_login_service(self):
        with tempfile.TemporaryDirectory(prefix="PageSwitching_Login_") as directory:
            folder = Path(directory)
            links = "".join(
                f'<Compile Include="{escape(str(ROOT / "Infrastructure" / name))}" />'
                for name in ("LoginService.cs", "LoginUser.cs")
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
                (ROOT / "tests/login_service_checks.cs.txt").read_text(encoding="utf-8"),
                encoding="utf-8",
            )
            result = subprocess.run(
                ["dotnet", "run", "--project", str(folder / "Probe.csproj"), "-c", "Release", "-v", "quiet"],
                capture_output=True, text=True, encoding="utf-8", timeout=120,
            )
            self.assertEqual(result.returncode, 0, result.stdout + result.stderr)
            print(result.stdout.strip())


if __name__ == "__main__":
    unittest.main(verbosity=2)
