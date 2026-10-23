"""链接真实手动页面，用离线服务检查按钮反馈、并发拦截和停止请求。"""
from pathlib import Path
import argparse
import os
import subprocess
import tempfile
import unittest
from xml.sax.saxutils import escape

ROOT = Path(__file__).resolve().parents[1]
IMAGES = None


class ManualUiChecks(unittest.TestCase):
    def test_actual_manual_page(self):
        packages = Path(os.environ.get("NUGET_PACKAGES", Path.home() / ".nuget/packages"))
        antdui = packages / "antdui/2.4.12/lib/net8.0-windows7.0/AntdUI.dll"
        self.assertTrue(antdui.is_file(), f"缺少本地 AntdUI 2.4.12：{antdui}")
        sources = ["panel/manual.cs", "panel/manual.Designer.cs", "Controls/ServoPositionIndicator.cs",
                   "Controls/UiPalette.cs", "Axis/Feedback/AxisSnapshot.cs", "Infrastructure/AdsDiagnostics.cs"]
        with tempfile.TemporaryDirectory(prefix="PageSwitching_ManualUI_") as directory:
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
                (ROOT / "tests/manual_ui_checks.cs.txt").read_text(encoding="utf-8"), encoding="utf-8")
            command = ["dotnet", "run", "--project", str(folder / "Probe.csproj"), "-c", "Release", "-v", "quiet"]
            if IMAGES is not None:
                command += ["--", str(IMAGES)]
            result = subprocess.run(command, capture_output=True, text=True, encoding="utf-8", timeout=120)
            self.assertEqual(result.returncode, 0, result.stdout + result.stderr)
            print(result.stdout.strip())


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("--images", type=Path, help="可选：保存离线 UI 回归图片的目录。")
    options = parser.parse_args()
    IMAGES = options.images.resolve() if options.images else None
    unittest.main(argv=[__file__], verbosity=2)
