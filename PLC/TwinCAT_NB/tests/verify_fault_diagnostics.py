"""执行实际 ST 故障分支，验证首因保持；不模拟 NC 或替代 XAE 编译。"""
from pathlib import Path
from types import SimpleNamespace as NS
import random
import re
import unittest
import xml.etree.ElementTree as ET
from verify_upload_assembly import StBranch

ROOT = Path(__file__).resolve().parents[1]
RAW = ET.parse(ROOT / 'axis_4/POUs/Trajectory_点位与轨迹/FB_FourAxisTrajectory.TcPOU').findtext('.//ST')

def branch(start, end):
    code = RAW[RAW.index(start):RAW.index(end, RAW.index(start))]
    return StBranch(re.sub(r'//[^\n]*', '', code))

def fixture(state=30, fault=False):
    motion = NS(xError=fault, udiErrorId=1006 if fault else 0, nFaultAxis=0,
                nFaultSource=7 if fault else 0, nFaultState=30, udiCleanupErrorId=0)
    axis_data = NS(**{f'Axis{i}': NS(xError=False) for i in range(1, 5)})
    axes = [None] + [NS(Status=NS(Error=False, IoDataInvalid=False)) for _ in range(4)]
    block = lambda: [None] + [NS(Error=False, ErrorID=0) for _ in range(4)]
    return dict(GVL_motion=motion, GVL_axis=axis_data, Axes=axes, nState=state,
                xAxisFault=False, xAbort=False, nAbortReason=0, xAllEnabled=True,
                tmHeartbeat=NS(Q=False), tmExit=NS(Q=False), INT_TO_UINT=int,
                aMoveInit=block(), aEnable=block(), aStopInit=block(), aDisable=block(),
                nBlockAxis=0, nBlockSource=0, udiBlockError=0)

class FaultDiagnosticChecks(unittest.TestCase):
    def test_interlock_reason_and_axis_fault_are_preserved(self):
        guard = branch('IF (nState >= 10)', '// 人工停止')
        for reason in range(1, 7):
            values = fixture()
            values.update(xAbort=True, nAbortReason=reason)
            guard.run(values)
            self.assertEqual((values['GVL_motion'].udiErrorId, values['GVL_motion'].nFaultSource), (1007, reason))
            self.assertEqual(values['nState'], 50)
            values.update(xAbort=True, nAbortReason=2)
            guard.run(values)
            self.assertEqual(values['GVL_motion'].nFaultSource, reason)
        for axis in range(1, 5):
            values = fixture()
            values['xAxisFault'] = True
            values['Axes'][axis].Status.IoDataInvalid = True
            guard.run(values)
            self.assertEqual(values['GVL_motion'].nFaultAxis, axis)
            self.assertEqual(values['GVL_motion'].nFaultSource, 8)

    def test_first_axis_wins_without_changing_common_stage_mid_loop(self):
        collect = branch('IF nBlockAxis = 0 THEN', '\nEND_FOR')
        finish = branch('IF nBlockAxis <> 0 THEN', '// 退出超时')
        for state, name, source in [(15, 'aMoveInit', 10), (20, 'aEnable', 11)]:
            for first_axis in range(1, 5):
                values = fixture(state)
                for axis in range(first_axis, 5):
                    values[name][axis] = NS(Error=True, ErrorID=0x4000 + axis)
                for axis in range(1, 5):
                    values['i'] = axis
                    collect.run(values)
                    self.assertEqual(values['nState'], state)
                finish.run(values)
                motion = values['GVL_motion']
                self.assertEqual((motion.udiErrorId, motion.nFaultAxis, motion.nFaultSource, motion.nFaultState),
                                 (0x4000 + first_axis, first_axis, source, state))
                self.assertEqual(values['nState'], 50)

    def test_mc_error_flag_with_zero_error_id_still_stops(self):
        collect = branch('IF nBlockAxis = 0 THEN', '\nEND_FOR')
        finish = branch('IF nBlockAxis <> 0 THEN', '// 退出超时')
        values = fixture(15)
        values['aMoveInit'][1] = NS(Error=True, ErrorID=0)
        values['i'] = 1
        collect.run(values)
        finish.run(values)
        self.assertEqual(values['nState'], 50)
        self.assertTrue(values['GVL_motion'].xError)
        self.assertEqual(values['GVL_motion'].nFaultAxis, 1)

    def test_cleanup_failure_and_timeout_do_not_overwrite_original(self):
        collect = branch('IF nBlockAxis = 0 THEN', '\nEND_FOR')
        timeout = branch('IF tmExit.Q AND', '// 6. 已接收')
        for state, name, source in [(50, 'aStopInit', 12), (60, 'aDisable', 13)]:
            for original in (False, True):
                values = fixture(state, original)
                values[name][2] = NS(Error=True, ErrorID=0x6002)
                values[name][4] = NS(Error=True, ErrorID=0x6004)
                for axis in range(1, 5):
                    values['i'] = axis
                    collect.run(values)
                motion = values['GVL_motion']
                self.assertEqual(motion.udiCleanupErrorId, 0x6002)
                self.assertEqual(motion.udiErrorId, 1006 if original else 0x6002)
                self.assertEqual(motion.nFaultSource, 7 if original else source)
                values.update(nState=60, tmExit=NS(Q=True))
                timeout.run(values)
                self.assertEqual(motion.udiCleanupErrorId, 0x6002)
                self.assertEqual(motion.udiErrorId, 1006 if original else 0x6002)
        values = fixture(70)
        values['tmExit'].Q = True
        timeout.run(values)
        self.assertEqual((values['GVL_motion'].udiErrorId, values['GVL_motion'].nFaultSource), (1009, 14))
        self.assertEqual(values['nState'], 70, '退出超时仍占用轴，不能强制清忙')

    def test_diagnostic_preparation_cannot_clear_latched_failure(self):
        prepare = branch('IF NOT GVL_motion.xError THEN', '// 1. 上位机操作')
        values = fixture(60, True)
        prepare.run(values)
        self.assertEqual(values['GVL_motion'].nFaultState, 30)
        self.assertEqual(values['GVL_motion'].udiErrorId, 1006)
        values['GVL_motion'].xError = False
        prepare.run(values)
        self.assertEqual(values['GVL_motion'].nFaultState, 60)
        self.assertEqual(values['GVL_motion'].nFaultSource, 0)

    def test_main_reason_matches_unchanged_interlock_conditions(self):
        tree = ET.parse(ROOT / 'axis_4/POUs/MAIN.TcPOU')
        code = tree.findtext(".//Action[@Name='A04_TrajectoryControl']/Implementation/ST")
        code = re.sub(r'//[^\n]*', '', code[:code.index('fbTrajectory(')])
        program = StBranch(code)
        rng = random.Random(1023)
        for _ in range(500):
            chance = lambda: rng.random() < .08
            data = NS(**{f'Axis{i}': NS(xEnabled=not chance(), xStart=not chance(),
                      xError=chance(), xStop=chance(), jogz=chance(), jogf=chance(), gohome=chance(), reset=chance())
                      for i in range(1, 5)})
            axes = [None] + [NS(Status=NS(Homed=True, StandStill=True, Stopping=False,
                      Error=False, IoDataInvalid=False, SoftLimitMinExceeded=chance(), SoftLimitMaxExceeded=chance()))
                      for _ in range(4)]
            motion = NS(xMotionPermit=not chance(), xBusy=chance(), xStop=chance(), nCommandStation=1,
                        axPositiveLimit=[None]+[chance() for _ in range(4)],
                        axNegativeLimit=[None]+[chance() for _ in range(4)])
            values = dict(GVL_axis=data, GVL_motion=motion, GVL_sy=NS(axis=axes),
                          GVL_control=NS(xCanCommand=not chance(), nOwner=1))
            program.run(values)
            self.assertEqual(values['xAbort'], values['nAbortReason'] != 0)

if __name__ == '__main__': unittest.main(verbosity=2)
