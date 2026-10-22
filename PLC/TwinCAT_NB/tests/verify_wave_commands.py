"""执行真实 ST 的权限门控、失联失效和正常停止分支，未模拟 NC。"""
import unittest
from types import SimpleNamespace as NS
from verify_upload_assembly import ROOT, SOURCE, StBranch, AssemblyFixture

class WaveCommandChecks(unittest.TestCase):
    def test_idle_accepts_disabled_axes_but_requires_valid_stopped_feedback(self):
        import re
        source = (ROOT / "axis_4/POUs/Feedback_反馈/FB_HmiFeedback.TcPOU").read_text(encoding="utf-8")
        assignments = re.findall(r"GVL_HMI\.stMonitor\.xAxesIdle :=.*?;", source, re.S)
        self.assertEqual(len(assignments), 2)

        def idle(**changes):
            monitor = NS(xAxesStopped=changes.get("stopped", True))
            statuses = {i: NS(Status=NS(StandStill=False, Disabled=True, Homing=False, Stopping=False,
                                       ExtSetPointGenEnabled=False, IoDataInvalid=False)) for i in range(1, 5)}
            axis_data = NS(**{f"Axis{i}": NS(xBusy=False) for i in range(1, 5)})
            # 每次只改变其中一轴，确保四轴都参与检查。
            for field, value in changes.items():
                if field == "busy": axis_data.Axis4.xBusy = value
                elif field != "stopped": setattr(statuses[4].Status, field, value)
            values = {"GVL_HMI": NS(stMonitor=monitor), "GVL_HMI.stMonitor": monitor,
                      "GVL_axis": axis_data, "GVL_sy": NS(axis=statuses)}
            StBranch(assignments[0]).run(values)
            for i in range(1, 5):
                values["i"] = i
                StBranch(assignments[1]).run(values)
            return monitor.xAxesIdle

        self.assertTrue(idle(), "未使能但已停稳时允许上传及自动准备")
        self.assertTrue(idle(Disabled=False, StandStill=True), "混合使能状态仍允许空闲")
        for changes in ({"stopped": False}, {"busy": True}, {"Disabled": False},
                        {"Homing": True}, {"Stopping": True},
                        {"ExtSetPointGenEnabled": True}, {"IoDataInvalid": True}):
            with self.subTest(changes=changes): self.assertFalse(idle(**changes))

    def test_stop_confirmation_requires_valid_nc_feedback(self):
        source = (ROOT / "axis_4/POUs/ManualControl_手动控制/FB_AxisControl.TcPOU").read_text(encoding="utf-8")
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
        for path in (ROOT / "axis_4/POUs").rglob("*.TcPOU"):
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
