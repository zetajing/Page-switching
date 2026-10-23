"""执行实际轴配置块，检查旧全零配置的安全补齐和只执行一次边界。"""
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
    """读取真实 ST，只有 NC 状态和全局内存使用离线替身。"""
    def __init__(self, root=None):
        root = ROOT if root is None else root
        path = root / "axis_4/POUs/AxisConfig_轴配置/FB_AxisConfig.TcPOU"
        source = ET.parse(path).findtext(".//ST")
        source = re.sub(r"//[^\n]*", "", source)
        # 解释器不解析实数字面量；绑定同值常量，保留真实比较及组合条件。
        source = source.replace("0.000001", "cVelocityTolerance")
        self.program = StBranch(source)
        self.motion = NS(aConfig=[None] + [NS(**dict.fromkeys(FIELDS, 0)) for _ in range(4)],
                         xBusy=False, xUploadBusy=False, xMotionPermit=True,
                         xStart=False, xStop=False, xUpload=True, xAssemble=False, xReset=True,
                         nCommandStation=2, nUploadStation=2, udiHeartbeat=123)
        self.axes = [None] + [NS(Status=NS(IoDataInvalid=False, Disabled=True, StandStill=False,
                                           Moving=False, Homing=False, Stopping=False,
                                           ExtSetPointGenEnabled=False), NcToPlc=NS(ActVelo=0))
                              for _ in range(4)]
        self.axis_data = NS(**{f"Axis{i}": NS(xBusy=False, xStart=i % 2 == 0, xStop=i % 2 != 0,
                                             jogz=False, jogf=False, gohome=False, reset=False, lrJogSpeed=i)
                              for i in range(1, 5)})
        self.stations = [None] + [NS(aAxis=[None] + [NS(xStart=False, xStop=False, jogz=False,
                                                       jogf=False, gohome=False, reset=False, lrJogSpeed=i)
                                                    for i in range(1, 5)]) for _ in range(3)]
        self.control = NS(nOwner=2, aStation=self.stations, xCanCommand=True)
        self.values = dict(xInitialized=False, xAxesIdle=False, i=0, GVL_motion=self.motion,
                           GVL_sy=NS(axis=self.axes), GVL_axis=self.axis_data, GVL_control=self.control,
                           cVelocityTolerance=0.000001, ABS=abs)

    def scan(self):
        self.program.run(self.values)

    def command_state(self):
        motion = {name: value for name, value in vars(self.motion).items() if name != "aConfig"}
        return deepcopy((motion, vars(self.axis_data), vars(self.control)))


class VirtualConfigChecks(unittest.TestCase):
    def test_all_zero_disabled_axes_receive_confirmed_configuration(self):
        fixture = AxisConfigFixture()
        fixture.scan()
        self.assertTrue(fixture.values["xInitialized"])
        for config in fixture.motion.aConfig[1:]:
            self.assertEqual(vars(config), EXPECTED)

    def test_standstill_and_disabled_are_both_accepted(self):
        for disabled, standstill in ((True, False), (False, True)):
            with self.subTest(disabled=disabled, standstill=standstill):
                fixture = AxisConfigFixture()
                for axis in fixture.axes[1:]:
                    axis.Status.Disabled, axis.Status.StandStill = disabled, standstill
                fixture.scan()
                self.assertTrue(fixture.values["xInitialized"])
                self.assertTrue(all(vars(config) == EXPECTED for config in fixture.motion.aConfig[1:]))

    def assert_blocked_then_retry(self, fixture, target, field, blocked_value, safe_value):
        setattr(target, field, blocked_value)
        before = deepcopy(fixture.motion.aConfig)
        commands = fixture.command_state()
        fixture.scan()
        self.assertFalse(fixture.values["xInitialized"], "被阻断时不能提前记为已初始化")
        self.assertEqual(fixture.motion.aConfig, before, "被阻断时不能先写部分轴配置")
        self.assertEqual(fixture.command_state(), commands, "配置块不能改运动命令或占用状态")
        setattr(target, field, safe_value)
        released_commands = fixture.command_state()
        fixture.scan()
        self.assertTrue(fixture.values["xInitialized"], "状态安全后应在后续扫描重试")
        self.assertTrue(all(vars(config) == EXPECTED for config in fixture.motion.aConfig[1:]))
        self.assertEqual(fixture.command_state(), released_commands, "重试补配置也不能写回任何命令位")

    def test_pending_trajectory_start_blocks_until_released(self):
        fixture = AxisConfigFixture()
        self.assert_blocked_then_retry(fixture, fixture.motion, "xStart", True, False)

    def test_current_owner_motion_requests_block_until_released(self):
        # 每个持有站、每根轴、每个点动/回原请求分别检查，防止漏站或漏轴。
        for owner in range(1, 4):
            for number in range(1, 5):
                for field in ("jogz", "jogf", "gohome"):
                    with self.subTest(owner=owner, axis=number, request=field):
                        fixture = AxisConfigFixture()
                        fixture.control.nOwner = owner
                        mailbox = fixture.stations[owner].aAxis[number]
                        self.assert_blocked_then_retry(fixture, mailbox, field, True, False)

    def test_non_owner_requests_do_not_block_or_get_cleared(self):
        # 无持有者时不下标访问站0；未授权站的旧请求由仲裁管理，配置块不清命令。
        for owner in (0, 1, 2, 3):
            with self.subTest(owner=owner):
                fixture = AxisConfigFixture()
                fixture.control.nOwner = owner
                other = 2 if owner == 1 else 1
                mailbox = fixture.stations[other].aAxis[1]
                mailbox.jogz = mailbox.jogf = mailbox.gohome = True
                commands = fixture.command_state()
                fixture.scan()
                self.assertTrue(fixture.values["xInitialized"])
                self.assertTrue(all(vars(config) == EXPECTED for config in fixture.motion.aConfig[1:]))
                self.assertEqual(fixture.command_state(), commands)

    def test_group_upload_and_each_basic_axis_busy_block_initialization(self):
        for field in ("xBusy", "xUploadBusy"):
            with self.subTest(group=field):
                fixture = AxisConfigFixture()
                self.assert_blocked_then_retry(fixture, fixture.motion, field, True, False)
        for number in range(1, 5):
            with self.subTest(axis_busy=number):
                fixture = AxisConfigFixture()
                self.assert_blocked_then_retry(fixture, getattr(fixture.axis_data, f"Axis{number}"),
                                              "xBusy", True, False)

    def test_each_nc_invalid_or_active_state_blocks_and_can_retry(self):
        for number in range(1, 5):
            for field in ("IoDataInvalid", "Moving", "Homing", "Stopping", "ExtSetPointGenEnabled"):
                with self.subTest(axis=number, field=field):
                    fixture = AxisConfigFixture()
                    self.assert_blocked_then_retry(fixture, fixture.axes[number].Status, field, True, False)
            with self.subTest(axis=number, field="neither_disabled_nor_standstill"):
                fixture = AxisConfigFixture()
                self.assert_blocked_then_retry(fixture, fixture.axes[number].Status, "Disabled", False, True)

    def test_nonzero_and_invalid_velocities_block_initialization(self):
        for number in range(1, 5):
            for velocity in (0.001, -0.001, float("nan"), float("inf"), -float("inf")):
                with self.subTest(axis=number, velocity=velocity):
                    fixture = AxisConfigFixture()
                    self.assert_blocked_then_retry(fixture, fixture.axes[number].NcToPlc,
                                                  "ActVelo", velocity, 0)
        # 正负阈值均允许；阈值稍外侧仍须拒绝。
        for velocity, accepted in ((0.000001, True), (-0.000001, True),
                                   (0.00000101, False), (-0.00000101, False)):
            with self.subTest(boundary=velocity):
                fixture = AxisConfigFixture()
                fixture.axes[1].NcToPlc.ActVelo = velocity
                fixture.scan()
                self.assertEqual(fixture.values["xInitialized"], accepted)

    def test_any_nonzero_field_preserves_partial_configuration(self):
        for field in FIELDS:
            with self.subTest(field=field):
                fixture = AxisConfigFixture()
                setattr(fixture.motion.aConfig[2], field, 123)
                original = deepcopy(fixture.motion.aConfig[2])
                fixture.scan()
                self.assertTrue(fixture.values["xInitialized"])
                self.assertEqual(fixture.motion.aConfig[2], original,
                                 "只要已有任一非零字段，就不能悄悄重写其余字段")
                for number in (1, 3, 4):
                    self.assertEqual(vars(fixture.motion.aConfig[number]), EXPECTED)

    def test_mixed_configuration_and_once_only_behavior(self):
        fixture = AxisConfigFixture()
        fixture.motion.aConfig[1] = NS(**{name: value / 2 for name, value in EXPECTED.items()})
        fixture.motion.aConfig[3].lrInitVelocity = 7
        existing = deepcopy((fixture.motion.aConfig[1], fixture.motion.aConfig[3]))
        commands = fixture.command_state()
        fixture.scan()
        self.assertEqual((fixture.motion.aConfig[1], fixture.motion.aConfig[3]), existing)
        self.assertEqual(vars(fixture.motion.aConfig[2]), EXPECTED)
        self.assertEqual(vars(fixture.motion.aConfig[4]), EXPECTED)
        self.assertEqual(fixture.command_state(), commands)
        # 一次补齐后，即使用户清零或修改参数，后续扫描也不能再次覆盖。
        fixture.motion.aConfig[2] = NS(**dict.fromkeys(FIELDS, 0))
        fixture.motion.aConfig[4].lrMaxVelocity = 55
        after_edit = deepcopy(fixture.motion.aConfig)
        fixture.scan()
        self.assertEqual(fixture.motion.aConfig, after_edit)
        self.assertEqual(fixture.command_state(), commands)

    def test_configuration_storage_does_not_depend_on_declaration_initializers(self):
        declaration = ET.parse(ROOT / "axis_4/GVLs/GVL_motion.TcGVL").findtext(".//Declaration")
        self.assertRegex(declaration, r"aConfig\s*:\s*ARRAY\[1\.\.4\]\s+OF\s+ST_AxisConfig\s*;")
        self.assertNotIn("stVirtualAxisConfig", declaration)


if __name__ == "__main__":
    unittest.main(verbosity=2)
