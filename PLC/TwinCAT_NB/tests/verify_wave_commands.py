"""执行真实 ST 的权限门控、失联失效和正常停止分支，未模拟 NC。"""
import unittest
from types import SimpleNamespace as NS
from verify_upload_assembly import ROOT, SOURCE, StBranch, AssemblyFixture

class WaveCommandChecks(unittest.TestCase):
    def test_stop_confirmation_requires_valid_nc_feedback(self):
        source = (ROOT / "axis_4/POUs/FB_AxisControl.TcPOU").read_text(encoding="utf-8")
        stopped = source[source.index("Data.xStopped :="):source.index("Data.xBusy :=")]
        self.assertIn("NOT Axis.Status.IoDataInvalid", stopped)
        self.assertIn("ABS(Axis.NcToPlc.ActVelo)", stopped)
        self.assertIn("fbStop.Done", stopped)

    def test_unowned_request_edges_are_consumed(self):
        guard = SOURCE[SOURCE.index("xUploadRequest :="):SOURCE.index("IF GVL_control.xOwnerChanged")]
        names = ("Upload", "Assemble", "Clear", "Start", "Reset")
        # 模拟原始 CLK 跨扫描：未授权沿、授权时仍保持、释放、再次触发。
        previous = False
        for clock, allowed, expected in ((True, False, False), (True, True, False),
                                          (False, True, False), (True, True, True)):
            edge = clock and not previous
            previous = clock
            values = dict(xCommandAllowed=allowed)
            for name in names: values["rt" + name] = NS(Q=edge)
            StBranch(guard).run(values)
            for name in names:
                self.assertEqual(values["x" + name + "Request"], expected)
                self.assertEqual(values["rt" + name].Q, edge, "禁止修改 R_TRIG 输出")

    def test_rising_edge_outputs_are_never_written(self):
        import re
        for path in (ROOT / "axis_4/POUs").glob("*.TcPOU"):
            source = path.read_text(encoding="utf-8")
            self.assertFalse(re.search(r"\brt\w+\.Q\s*:=", source), str(path))

    def test_owner_change_invalidates_idle_or_assembling_table(self):
        end = SOURCE.index("tmHeartbeat(")
        guard = SOURCE[SOURCE.index("IF GVL_control.xOwnerChanged"):end]
        for state in (0, 5, 30):
            f = AssemblyFixture()
            f.values.update(nState=state, nCount=100003, nReceived=100003, nLoaded=100003, udiSequence=7,
                            GVL_control=NS(xOwnerChanged=True, xCanCommand=True))
            f.motion.xDataReady = True
            f.motion.nUploadStation = 1
            StBranch(guard).run(f.values)
            self.assertFalse(f.motion.xDataReady)
            self.assertEqual(f.motion.nUploadStation, 0)
            self.assertEqual(f.values["nState"], 30 if state == 30 else 0)
            if state != 30: self.assertEqual(f.values["nReceived"], 0)

    def test_normal_stop_invalidates_table_without_fault(self):
        fragment = SOURCE[SOURCE.index("IF xStopCommand AND NOT"):SOURCE.index("CASE nState OF")]
        for state in (0, 5, 10, 15, 20, 30, 40, 50, 60, 70):
            f = AssemblyFixture()
            f.values.update(nState=state, xStopCommand=True, xAllStopped=True, xAllDisabled=True,
                            xMovesIdle=True, xNormalStop=False, nStopCycles=9)
            f.motion.xStopped = False
            f.motion.xDataReady = True
            StBranch(fragment).run(f.values)
            self.assertFalse(f.motion.xError)
            self.assertFalse(f.motion.xDataReady)
            self.assertTrue(f.values["xNormalStop"])
            expected = 0 if state in (0, 5, 10) else 50 if state in (15, 20, 30) else state
            self.assertEqual(f.values["nState"], expected)

    def test_real_fault_not_relabelled_as_normal_stop(self):
        fragment = SOURCE[SOURCE.index("IF xStopCommand AND NOT"):SOURCE.index("CASE nState OF")]
        f = AssemblyFixture()
        f.values.update(nState=30, xStopCommand=True, xNormalStop=False)
        f.motion.xError = True
        f.motion.udiErrorId = 1006
        StBranch(fragment).run(f.values)
        self.assertFalse(f.values["xNormalStop"])
        self.assertEqual(f.motion.udiErrorId, 1006)

if __name__ == "__main__": unittest.main(verbosity=2)
