"""核对 PLC 布局和发布来源，并在临时目录执行实际 C# 封送/快照转换。"""
from pathlib import Path
import re
import subprocess
import tempfile
import unittest
import xml.etree.ElementTree as ET
from xml.sax.saxutils import escape

ROOT = Path(__file__).resolve().parents[1]
SWITCH = next((p for p in ROOT.parents if (p / "App.config").is_file()), ROOT.parents[1] / "switch")

class MonitorFrameChecks(unittest.TestCase):
    def test_plc_layout_and_publication(self):
        def fields(name):
            text = ET.parse(ROOT / f"axis_4/DUTs/{name}.TcDUT").findtext(".//Declaration")
            self.assertIn("'pack_mode' := '1'", text)
            return re.findall(r"^\s*(\w+)\s*:\s*(\w+)\s*;", text, re.M)
        axis = fields("ST_AxisMonitor")
        self.assertEqual(axis, [("lrActualPosition", "LREAL"), ("lrSpeed", "LREAL"), ("xHomed", "BOOL"),
                               ("xError", "BOOL"), ("xPositiveLimit", "BOOL"), ("xNegativeLimit", "BOOL"), ("xOriginSignal", "BOOL"), ("xEnabled", "BOOL")])
        frame = fields("ST_MachineMonitor")
        self.assertEqual(frame, [("nVersion", "UINT"), ("nByteSize", "UINT"), ("nControlOwner", "UINT"),
            ("nControlMode", "UINT"), ("nWaveState", "UINT"), ("uiWaveFaultCode", "UINT"), ("udiHeartbeat", "UDINT")]
            + [(f"Axis{i}", "ST_AxisMonitor") for i in range(1, 5)]
            + [('nTrajectoryState', 'UINT'), ('nReceivedCount', 'UDINT'), ('nLoadedCount', 'UDINT'), ('nPointCount', 'UDINT'), ('nCurrentPoint', 'UDINT'), ('udiReceivedSequence', 'UDINT'), ('udiAcceptedSequence', 'UDINT'), ('udiErrorId', 'UDINT'), ('nUploadStation', 'UINT'), ('xUploadBusy', 'BOOL'), ('xDataReady', 'BOOL'), ('xTrajectoryBusy', 'BOOL'), ('xRunning', 'BOOL'), ('xDone', 'BOOL'), ('xError', 'BOOL'), ('xStopped', 'BOOL'), ('xMotionPermit', 'BOOL'), ('xConfigValid', 'BOOL'), ('xAxesIdle', 'BOOL'), ('xAxisFault', 'BOOL'), ('xAxesStopped', 'BOOL')])
        source = ET.parse(ROOT / "axis_4/POUs/FB_HmiFeedback.TcPOU").findtext(".//ST")
        for i in range(1, 5):
            for member, _ in axis:
                self.assertIn(f"GVL_HMI.stMonitor.Axis{i}.{member} := GVL_axis.Axis{i}.{member};", source)
        self.assertIn("GVL_HMI.stMonitor.nVersion := 3;", source)
        self.assertIn("nByteSize := SIZEOF(GVL_HMI.stMonitor)", source)
        project = ET.parse(ROOT / "axis_4/axis_4.plcproj")
        includes = [n.get("Include") for n in project.findall(".//{*}Compile")]
        for name in ("ST_AxisMonitor", "ST_MachineMonitor"):
            self.assertIn(f"DUTs\\{name}.TcDUT", includes)

    def test_actual_csharp_marshalling_and_failure_quality(self):
        with tempfile.TemporaryDirectory(prefix="TwinCAT_MonitorLayout_") as directory:
            folder = Path(directory)
            links = "".join(f'<Compile Include="{escape(str(SWITCH / "Axis" / name))}" />'
                            for name in ("PlcMachineMonitor.cs", "MachineMonitorSnapshot.cs", "AxisSnapshot.cs"))
            (folder / "Probe.csproj").write_text('<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup>'
                '<OutputType>Exe</OutputType><TargetFramework>net8.0</TargetFramework>'
                '<ImplicitUsings>enable</ImplicitUsings><Nullable>enable</Nullable>'
                '</PropertyGroup><ItemGroup>' + links + '</ItemGroup></Project>', encoding="utf-8")
            (folder / "Program.cs").write_text((ROOT / "tests/monitor_frame_checks.cs.txt").read_text(encoding="utf-8"), encoding="utf-8")
            result = subprocess.run(["dotnet", "run", "--project", str(folder / "Probe.csproj"), "-c", "Release", "-v", "quiet"],
                                    capture_output=True, text=True, timeout=90)
            self.assertEqual(result.returncode, 0, result.stdout + result.stderr)
            print(result.stdout.strip())

if __name__ == "__main__":
    unittest.main(verbosity=2)
