"""执行实际轴配置块，检查复用空闲反馈、补齐全零配置和已有参数保留。"""
from copy import deepcopy
from pathlib import Path
from types import SimpleNamespace as NS
import re
import unittest
import xml.etree.ElementTree as ET
from verify_upload_assembly import StBranch

ROOT = Path(__file__).resolve().parents[1]
FIELDS = ("lrMinPosition", "lrMaxPosition", "lrMaxVelocity", "lrMaxAcceleration",
          "lrStopDeceleration", "lrInitVelocity", "lrStartTolerance")
EXPECTED = dict(zip(FIELDS, (-200, 200, 100, 100, 100, 10, 10)))


class AxisConfigFixture:
    """读取真实 ST，直接提供现有全局反馈，不另造一份初始化状态。"""
    def __init__(self, root=None):
        root = ROOT if root is None else root
        path = root / "axis_4/POUs/AxisConfig_轴配置/FB_AxisConfig.TcPOU"
        source = re.sub(r"//[^\n]*", "", ET.parse(path).findtext(".//ST"))
        self.program = StBranch(source.replace("0.000001", "cVelocityTolerance"))
        self.motion = NS(aConfig=[None] + [NS(**dict.fromkeys(FIELDS, 0)) for _ in range(4)],
                         xBusy=False, xUploadBusy=False, xMotionPermit=True,
                         xStart=False, xStop=False, xUpload=True, xAssemble=False, xReset=True,
                         nCommandStation=2, nUploadStation=2, udiHeartbeat=123)
        self.axis_data = NS(**{f"Axis{i}": NS(xBusy=False, xStart=i % 2 == 0, xStop=i % 2 != 0,
                                             jogz=False, jogf=False, gohome=False, reset=False, lrJogSpeed=i)
                              for i in range(1, 5)})
        self.stations = [None] + [NS(aAxis=[None] + [NS(xStart=False, xStop=False, jogz=False,
                                                       jogf=False, gohome=False, reset=False, lrJogSpeed=i)
                                                    for i in range(1, 5)]) for _ in range(3)]
        self.control = NS(nOwner=2, aStation=self.stations, xCanCommand=True,
                          xLocked=False, xFeedbackInvalid=False)
        self.hmi = NS(stMonitor=NS(xAxesIdle=True))
        self.axes = [None] + [NS(Status=NS(StandStill=False, Disabled=True), NcToPlc=NS(ActVelo=0))
                              for _ in range(4)]
        self.values = dict(i=0, GVL_motion=self.motion, GVL_axis=self.axis_data,
                           GVL_control=self.control, GVL_HMI=self.hmi,
                           GVL_sy=NS(axis=self.axes), ABS=abs, cVelocityTolerance=0.000001)

    def scan(self):
        self.program.run(self.values)

    def command_state(self):
        motion = {name: value for name, value in vars(self.motion).items() if name != "aConfig"}
        return deepcopy((motion, vars(self.axis_data), vars(self.control), vars(self.hmi)))


class VirtualConfigChecks(unittest.TestCase):
    def test_all_zero_axes_receive_confirmed_configuration(self):
        fixture = AxisConfigFixture()
        fixture.scan()
        for config in fixture.motion.aConfig[1:]:
            self.assertEqual(vars(config), EXPECTED)

    def test_existing_feedback_gates_block_and_allow_later_retry(self):
        for owner, field, blocked, released in (
            ("hmi", "xAxesIdle", False, True),
            ("control", "xLocked", True, False),
            ("control", "xFeedbackInvalid", True, False),
            ("motion", "xBusy", True, False),
            ("motion", "xUploadBusy", True, False),
            ("motion", "xStart", True, False),
        ):
            with self.subTest(feedback=owner + "." + field):
                fixture = AxisConfigFixture()
                target = fixture.hmi.stMonitor if owner == "hmi" else getattr(fixture, owner)
                setattr(target, field, blocked)
                before = deepcopy(fixture.motion.aConfig)
                commands = fixture.command_state()
                fixture.scan()
                self.assertEqual(fixture.motion.aConfig, before, "被阻断时不能先写部分轴配置")
                self.assertEqual(fixture.command_state(), commands, "配置块不能改命令或已有反馈")
                setattr(target, field, released)
                commands = fixture.command_state()
                fixture.scan()
                self.assertTrue(all(vars(config) == EXPECTED for config in fixture.motion.aConfig[1:]))
                self.assertEqual(fixture.command_state(), commands, "条件恢复后补参数仍不能改命令")

    def test_any_nonzero_field_preserves_partial_configuration(self):
        for field in FIELDS:
            with self.subTest(field=field):
                fixture = AxisConfigFixture()
                setattr(fixture.motion.aConfig[2], field, 123)
                original = deepcopy(fixture.motion.aConfig[2])
                fixture.scan()
                self.assertEqual(fixture.motion.aConfig[2], original,
                                 "只要已有任一非零字段，就不能悄悄重写其余字段")
                for number in (1, 3, 4):
                    self.assertEqual(vars(fixture.motion.aConfig[number]), EXPECTED)

    def test_current_nc_state_blocks_stale_idle_feedback(self):
        # 上扫描空闲仍为 TRUE 时，本扫描速度无效或不再静止，也不能补配置。
        for number in range(1, 5):
            for velocity in (float('nan'), float('inf'), -float('inf'), 0.001, -0.001):
                with self.subTest(axis=number, velocity=velocity):
                    fixture = AxisConfigFixture()
                    fixture.axes[number].NcToPlc.ActVelo = velocity
                    fixture.scan()
                    self.assertTrue(all(not any(vars(config).values()) for config in fixture.motion.aConfig[1:]))
                    fixture.axes[number].NcToPlc.ActVelo = 0
                    fixture.scan()
                    self.assertTrue(all(vars(config) == EXPECTED for config in fixture.motion.aConfig[1:]))
            fixture = AxisConfigFixture()
            fixture.axes[number].Status.Disabled = False
            fixture.scan()
            self.assertTrue(all(not any(vars(config).values()) for config in fixture.motion.aConfig[1:]))
            fixture.axes[number].Status.StandStill = True
            fixture.scan()
            self.assertTrue(all(vars(config) == EXPECTED for config in fixture.motion.aConfig[1:]))

    def test_mixed_configuration_and_full_zero_can_be_filled_again(self):
        fixture = AxisConfigFixture()
        fixture.motion.aConfig[1] = NS(**{name: value / 2 for name, value in EXPECTED.items()})
        fixture.motion.aConfig[3].lrInitVelocity = 7
        existing = deepcopy((fixture.motion.aConfig[1], fixture.motion.aConfig[3]))
        fixture.scan()
        self.assertEqual((fixture.motion.aConfig[1], fixture.motion.aConfig[3]), existing)
        self.assertEqual(vars(fixture.motion.aConfig[2]), EXPECTED)
        self.assertEqual(vars(fixture.motion.aConfig[4]), EXPECTED)
        # 配置内容本身就是标记：正常重复扫描不覆盖，整轴再次全零时允许重新填写。
        fixture.motion.aConfig[4].lrMaxVelocity = 55
        before = deepcopy(fixture.motion.aConfig)
        fixture.scan()
        self.assertEqual(fixture.motion.aConfig, before)
        fixture.motion.aConfig[2] = NS(**dict.fromkeys(FIELDS, 0))
        fixture.scan()
        self.assertEqual(vars(fixture.motion.aConfig[2]), EXPECTED)
        self.assertEqual(fixture.motion.aConfig[4].lrMaxVelocity, 55)
        self.assertEqual((fixture.motion.aConfig[1], fixture.motion.aConfig[3]), existing)

    def test_configuration_never_changes_motion_commands_or_feedback(self):
        fixture = AxisConfigFixture()
        commands = fixture.command_state()
        fixture.scan()
        fixture.scan()
        self.assertEqual(fixture.command_state(), commands)
        self.assertTrue(all(vars(config) == EXPECTED for config in fixture.motion.aConfig[1:]))

    def test_block_uses_existing_feedback_without_private_status_copies(self):
        block = ET.parse(ROOT / "axis_4/POUs/AxisConfig_轴配置/FB_AxisConfig.TcPOU")
        declaration = re.sub(r"//[^\n]*", "", block.findtext(".//Declaration"))
        fields = re.findall(r"^\s*(\w+)\s*:\s*(\w+)\s*;", declaration, re.M)
        self.assertEqual(fields, [("i", "INT")])
        source = re.sub(r"//[^\n]*", "", block.findtext(".//ST"))
        self.assertIn("GVL_sy.axis[1].Status.StandStill", source)
        # 确认复用信号仍由真实仲裁和反馈块发布，而不是另造恒真的允许位。
        authority = ET.parse(ROOT / "axis_4/POUs/ControlAuthority_控制权/FB_ControlAuthority.TcPOU").findtext(".//ST")
        self.assertIn("GVL_control.xLocked := xLocked;", authority)
        for request in ("jogz", "jogf", "gohome"):
            self.assertIn("aAxis[j]." + request, authority)
        feedback = ET.parse(ROOT / "axis_4/POUs/Feedback_反馈/FB_HmiFeedback.TcPOU").findtext(".//ST")
        self.assertIn("GVL_HMI.stMonitor.xAxesIdle := GVL_HMI.stMonitor.xAxesStopped", feedback)
        self.assertIn("Status.Disabled", feedback)
        self.assertIn("Status.IoDataInvalid", feedback)

    def test_configuration_storage_does_not_depend_on_declaration_initializers(self):
        declaration = ET.parse(ROOT / "axis_4/GVLs/GVL_motion.TcGVL").findtext(".//Declaration")
        self.assertRegex(declaration, r"aConfig\s*:\s*ARRAY\[1\.\.4\]\s+OF\s+ST_AxisConfig\s*;")
        self.assertNotIn("stVirtualAxisConfig", declaration)


if __name__ == "__main__":
    unittest.main(verbosity=2)
