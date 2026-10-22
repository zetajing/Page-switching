"""逐扫描执行完整 FB_ControlAuthority ST；TON 使用确定性时钟，不连接 NC 或 ADS。"""

from pathlib import Path
from types import SimpleNamespace as NS
import re
import unittest
import xml.etree.ElementTree as ET
from verify_control_authority import DecisionProgram
from verify_upload_assembly import StBranch

ROOT = Path(__file__).resolve().parents[1]


class ScanTimer:
    def __init__(self):
        self.elapsed = 0
        self.Q = False

    def __call__(self, IN, PT):
        self.elapsed = self.elapsed + 250 if IN else 0
        self.Q = self.elapsed >= PT


def empty_commands():
    return {i: NS(xStart=False, xStop=False, jogz=False, jogf=False,
                  gohome=False, reset=False, lrJogSpeed=0) for i in range(1, 5)}


class AuthorityFixture:
    def __init__(self, invalid=True):
        source = ET.parse(ROOT / 'axis_4/POUs/ControlAuthority_控制权/FB_ControlAuthority.TcPOU').findtext('.//ST')
        source = re.sub(r'//[^\n]*', '', source).replace('T#3S', 'cTimeout')
        self.program = StBranch(source)
        decision = DecisionProgram()
        self.stations = {i: NS(udiHeartbeat=0, udiRequestSequence=0, udiAckSequence=0,
                               nAction=0, nResult=0, xOnline=False, xGranted=False,
                               xWaveSession=False, aAxis=empty_commands()) for i in range(1, 4)}
        self.control = NS(aStation=self.stations)
        self.axis = NS(**{f'Axis{i}': NS(xEnabled=False) for i in range(1, 5)})
        self.motion = NS(nCommandStation=0, xStart=False, xUpload=False, xAssemble=False,
                         xClear=False, xReset=False, xStop=False)
        self.values = dict(nOwner=0, nOldOwner=0, xOwnerLost=False, xResetCommands=False,
                           xLocked=False, xMotionBusy=False,
                           xFeedbackInvalid=invalid, axSeenHeartbeat={i: False for i in range(1, 4)},
                           aLastHeartbeat={i: 0 for i in range(1, 4)},
                           aTimeout={i: ScanTimer() for i in range(1, 4)}, aEmpty=empty_commands(),
                           GVL_control=self.control, GVL_axis=self.axis, GVL_motion=self.motion,
                           cTimeout=3000, INT_TO_UINT=int,
                           F_ControlDecision=lambda nOwner, nStation, nAction, xLocked, xOnline:
                               decision.execute(nOwner, nStation, nAction, xLocked, xOnline))

    def scan(self, beats=(2,)):
        for station in beats:
            self.stations[station].udiHeartbeat += 1
        self.program.run(self.values)

    def request(self, station=2, action=1, beats=None):
        mailbox = self.stations[station]
        mailbox.nAction = action
        mailbox.udiRequestSequence += 1
        self.scan((station,) if beats is None else beats)
        assert mailbox.udiAckSequence == mailbox.udiRequestSequence
        return mailbox.nResult

    def expire(self):
        for _ in range(13):
            self.scan(beats=())


class AuthorityLifecycleChecks(unittest.TestCase):
    def assert_cleared(self, fixture):
        self.assertEqual(fixture.control.nOwner, 0)
        self.assertFalse(fixture.control.xCanCommand)
        self.assertFalse(fixture.control.xStopOwner)
        self.assertFalse(fixture.control.xCanRelease)
        for station in fixture.stations.values():
            self.assertFalse(station.xGranted)
            self.assertFalse(station.xWaveSession)
            for command in station.aAxis.values():
                self.assertFalse(command.xStart or command.jogz or command.jogf or command.gohome)

    def test_faulted_idle_owner_disconnect_reconnect_reapply_release(self):
        f = AuthorityFixture()
        # 连续三次关闭、重开上位机，覆盖旧心跳/应答序号和失联锁存的完整生命周期。
        for _ in range(3):
            self.assertEqual(f.request(), 1)
            self.assertTrue(f.control.xCanCommand)
            f.stations[2].aAxis[1].xStart = True
            f.stations[2].aAxis[4].jogz = True
            f.stations[2].xWaveSession = True
            f.expire()
            self.assert_cleared(f)
            f.scan()
            self.assertTrue(f.stations[2].xOnline)
            self.assertFalse(f.control.xLocked)
            self.assertEqual(f.control.nOwner, 0, '恢复心跳不会自动恢复旧授权')
            self.assertEqual(f.request(), 1)
            self.assertTrue(f.stations[2].xGranted)
            f.stations[2].aAxis[2].xStart = True
            self.assertTrue(f.control.xCanRelease)
            self.assertEqual(f.request(action=2), 1)
            self.assert_cleared(f)

    def test_moving_or_enabled_faulted_axis_keeps_lost_owner_frozen(self):
        f = AuthorityFixture()
        self.assertEqual(f.request(), 1)
        f.values['xMotionBusy'] = True
        f.expire()
        self.assertEqual(f.control.nOwner, 2)
        self.assertTrue(f.control.xStopOwner)
        self.assertFalse(f.control.xCanCommand or f.control.xCanRelease)
        f.scan()  # 心跳恢复，但尚未完成退出，不能恢复权限。
        self.assertFalse(f.stations[2].xGranted)
        f.values['xMotionBusy'] = False
        f.axis.Axis4.xEnabled = True
        f.scan()
        self.assertEqual(f.control.nOwner, 2)
        self.assertTrue(f.control.xStopOwner)
        f.axis.Axis4.xEnabled = False
        f.scan()
        self.assert_cleared(f)
        self.assertEqual(f.request(), 1)
        self.assertTrue(f.control.xCanCommand)

    def test_other_station_can_apply_after_lost_idle_owner_is_revoked(self):
        f = AuthorityFixture()
        self.assertEqual(f.request(), 1)
        f.expire()
        self.assert_cleared(f)
        self.assertEqual(f.request(station=1), 1)
        self.assertEqual(f.control.nOwner, 1)
        self.assertTrue(f.stations[1].xGranted)
        self.assertFalse(f.stations[2].xGranted)

    def test_idle_release_and_new_request_complete_in_one_scan(self):
        f = AuthorityFixture()
        self.assertEqual(f.request(), 1)
        f.stations[1].nAction = 1
        f.stations[1].udiRequestSequence = 1
        self.assertEqual(f.request(action=2, beats=(1, 2)), 1)
        self.assertEqual(f.control.nOwner, 1)
        self.assertTrue(f.stations[1].xGranted)
        self.assertFalse(f.stations[2].xGranted)
        self.assertTrue(f.control.xOwnerChanged)
        self.assertEqual((f.stations[1].udiAckSequence, f.stations[1].nResult), (1, 1))

    def test_busy_request_is_acknowledged_and_never_replayed(self):
        f = AuthorityFixture()
        self.assertEqual(f.request(), 1)
        f.values['xMotionBusy'] = True
        self.assertEqual(f.request(station=1, beats=(1, 2)), 2)
        f.values['xMotionBusy'] = False
        f.scan(beats=(1, 2))
        self.assertEqual(f.control.nOwner, 2, '被拒绝的申请不能在空闲后自动重放')
        self.assertEqual(f.request(station=1), 1)
        self.assertEqual(f.control.nOwner, 1)

    def test_io_fault_uses_same_idle_priority_and_release_rules(self):
        f = AuthorityFixture()
        self.assertEqual(f.request(station=3), 1)
        self.assertFalse(f.control.xLocked)
        self.assertTrue(f.control.xCanRelease)
        self.assertEqual(f.request(station=1, beats=(1, 3)), 1)
        self.assertEqual(f.request(station=2, beats=(1, 2)), 6)
        self.assertEqual(f.request(station=1, action=2), 1)
        self.assert_cleared(f)

    def test_faulted_enabled_axis_blocks_first_acquisition(self):
        f = AuthorityFixture()
        f.axis.Axis4.xEnabled = True
        self.assertEqual(f.request(), 2)
        self.assertEqual(f.control.nOwner, 0)
        f.axis.Axis4.xEnabled = False
        self.assertEqual(f.request(), 1)

    def test_invalid_feedback_multiple_new_requests_choose_one_station(self):
        f = AuthorityFixture()
        for mailbox in f.stations.values():
            mailbox.nAction = mailbox.udiRequestSequence = 1
        f.scan(beats=(1, 2, 3))
        self.assertEqual(f.control.nOwner, 1)
        self.assertEqual([f.stations[i].nResult for i in (1, 2, 3)], [1, 6, 6])
        self.assertTrue(f.control.xCanCommand)

    def test_valid_feedback_keeps_existing_idle_disconnect_behavior(self):
        f = AuthorityFixture(invalid=False)
        self.assertEqual(f.request(), 1)
        f.axis.Axis1.xEnabled = True
        f.expire()
        self.assert_cleared(f)


if __name__ == '__main__':
    unittest.main(verbosity=2)
