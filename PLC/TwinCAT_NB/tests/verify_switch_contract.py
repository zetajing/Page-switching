"""核对 switch App.config 与 PLC 源码的 ADS 契约，不连接或写入实际 PLC。"""

from pathlib import Path
import os
import re
import unittest
import xml.etree.ElementTree as ET
from plc_source import main_source


ROOT = Path(__file__).resolve().parents[1]
# 仓库交付副本位于 switch/PLC 内；独立 PLC 工作目录则使用同级 switch 工程。
DEFAULT_SWITCH = next((p for p in ROOT.parents if (p / "App.config").is_file()), ROOT.parents[1] / "switch")
SWITCH = Path(os.environ.get("SWITCH_PROJECT", str(DEFAULT_SWITCH)))


def declaration(path):
    return ET.parse(path).findtext(".//Declaration")


def declared_fields(path):
    source = re.sub(r"//[^\n]*", "", declaration(path))
    return dict(re.findall(r"\b(\w+)\s*:\s*(\w+)\s*(?:;|:=)", source))


class SwitchContractChecks(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        if not (SWITCH / "App.config").is_file():
            raise unittest.SkipTest("请通过 SWITCH_PROJECT 指定 switch 源码目录")
        cls.settings = {n.get("key"): n.get("value") for n in
                        ET.parse(SWITCH / "App.config").findall(".//appSettings/add")}

    def test_all_four_axis_symbols_and_types(self):
        fields = declared_fields(ROOT / "axis_4/DUTs/ST_AxisAds.TcDUT")
        commands = declared_fields(ROOT / "axis_4/DUTs/ST_AxisCommand.TcDUT")
        globals_ = declared_fields(ROOT / "axis_4/GVLs/GVL_axis.TcGVL")
        # 只保留四轴命令地址；所有旧反馈配置必须移除，反馈改读 stMonitor。
        expected = {
            "ActualPosition": ("lrActualPosition", "LREAL"), "Speed": ("lrSpeed", "LREAL"),
            "Enabled": ("xEnabled", "BOOL"), "Homed": ("xHomed", "BOOL"),
            "Alarm": ("xError", "BOOL"), "PositiveLimit": ("xPositiveLimit", "BOOL"),
            "NegativeLimit": ("xNegativeLimit", "BOOL"), "OriginSignal": ("xOriginSignal", "BOOL"),
            "EnableCommand": ("xStart", "BOOL"), "ResetAlarmCommand": ("reset", "BOOL"),
            "HomeCommand": ("gohome", "BOOL"), "StopCommand": ("xStop", "BOOL"),
            "JogSpeedCommand": ("lrJogSpeed", "LREAL"), "JogPositiveCommand": ("jogz", "BOOL"),
            "JogNegativeCommand": ("jogf", "BOOL"),
        }
        for axis in range(1, 5):
            self.assertEqual(globals_[f"Axis{axis}"], "ST_AxisAds")
            for key, (member, plc_type) in expected.items():
                if key.endswith("Command"):
                    self.assertEqual(self.settings[f"AdsAxis{axis}{key}"],
                                     f"GVL_control.aStation[{{station}}].aAxis[{axis}].{member}")
                    self.assertEqual(commands[member], plc_type)
                else:
                    self.assertNotIn(f"AdsAxis{axis}{key}", self.settings)
                    self.assertEqual(fields[member], plc_type)
        # xEnabled 必须来自 Power 状态，不能用运动位代替。
        axis_source = ET.parse(ROOT / "axis_4/POUs/ManualControl_手动控制/FB_AxisControl.TcPOU").findtext(".//ST")
        self.assertIn("Data.xEnabled := fbPower.Status;", axis_source)
        self.assertIn("Data.xMotorRun := Axis.Status.Moving;", axis_source)

    def test_monitor_symbols_and_types(self):
        fields = declared_fields(ROOT / "axis_4/GVLs/GVL_HMI.TcGVL")
        self.assertNotIn("nControlOwner", fields)
        self.assertNotIn("nControlMode", fields)
        self.assertNotIn("nWaveState", fields)
        self.assertNotIn("udiWaveFaultCode", fields)
        self.assertNotIn("udiHeartbeat", fields)
        self.assertFalse((ROOT / "axis_4/GVLs/GVL_pc.TcGVL").exists())
        self.assertEqual(fields["udiDetailedFaultCode"], "UDINT")
        self.assertEqual(self.settings["AdsMonitorSnapshot"], "GVL_HMI.stMonitor")
        self.assertEqual(fields["stMonitor"], "ST_MachineMonitor")

    def test_feedback_runs_after_axis_blocks(self):
        source = main_source(ROOT)
        # 检查真实调用顺序和次数，防止整理 Action 时漏调、重调或提前发布反馈。
        calls = ["ReadStatus()", "fbAxisConfig();", "fbAuthority(", "aCommandGate[1](", "aCommandGate[4](",
                 "fbTrajectory(", "fbAxis1(", "fbAxis2(", "fbAxis3(", "fbAxis4(", "fbHmiFeedback();"]
        positions = [source.index(call) for call in calls]
        self.assertEqual(positions, sorted(positions))
        for call in calls:
            self.assertEqual(source.count(call), 1, call)
        self.assertGreater(source.index("fbHmiFeedback();"), source.index("fbAxis4("))
        project = ET.parse(ROOT / "axis_4/axis_4.plcproj")
        includes = {n.get("Include") for n in project.findall(
            ".//p:Compile", {"p": "http://schemas.microsoft.com/developer/msbuild/2003"})}
        self.assertIn("GVLs\\GVL_HMI.TcGVL", includes)
        self.assertIn("POUs\\Feedback_反馈\\FB_HmiFeedback.TcPOU", includes)

    def test_feedback_never_writes_axis_commands(self):
        source = ET.parse(ROOT / "axis_4/POUs/Feedback_反馈/FB_HmiFeedback.TcPOU").findtext(".//ST")
        source = re.sub(r"//[^\n]*", "", source)
        self.assertFalse(re.search(r"GVL_(axis|motion)\.\w+(?:\.\w+)*\s*:=", source))
        self.assertNotIn("GVL_pc.", source)
        self.assertIn("GVL_HMI.stMonitor.nControlOwner := GVL_control.nOwner;", source)
        self.assertIn("GVL_HMI.stMonitor.nControlMode := 2;", source)
        self.assertIn("PT := T#500MS", source)
        self.assertIn("GVL_HMI.stMonitor.udiHeartbeat := 0;", source)
        self.assertIn("GVL_HMI.stMonitor.uiWaveFaultCode := UINT#65535;", source)


if __name__ == "__main__":
    unittest.main(verbosity=2)
