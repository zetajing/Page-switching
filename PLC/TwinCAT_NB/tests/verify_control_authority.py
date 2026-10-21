"""执行真实 F_ControlDecision 的 IF/赋值子集，检查运行锁；不模拟 NC 或 ADS。"""

from pathlib import Path
import re
import unittest
import xml.etree.ElementTree as ET
from types import SimpleNamespace as NS
from verify_upload_assembly import StBranch


ROOT = Path(__file__).resolve().parents[1]


class DecisionProgram:
    def __init__(self):
        source = ET.parse(ROOT / "axis_4/POUs/F_ControlDecision.TcPOU").findtext(".//ST")
        source = re.sub(r"//[^\n]*", "", source)
        self.tokens = re.findall(r"\w+|:=|<>|<=|>=|[();<>=+\-]", source)
        self.index = 0
        self.program = self.block(set())

    def take(self, expected=None):
        token = self.tokens[self.index]
        self.index += 1
        if expected is not None:
            assert token == expected, (token, expected)
        return token

    def expression(self, end):
        result = []
        while self.tokens[self.index] != end:
            result.append(self.take())
        return result

    def block(self, stops):
        result = []
        while self.index < len(self.tokens) and self.tokens[self.index] not in stops:
            name = self.take()
            if name == "IF":
                branches = []
                while True:
                    condition = self.expression("THEN")
                    self.take("THEN")
                    body = self.block({"ELSIF", "ELSE", "END_IF"})
                    branches.append((condition, body))
                    terminator = self.take()
                    if terminator == "ELSIF":
                        continue
                    alternative = []
                    if terminator == "ELSE":
                        alternative = self.block({"END_IF"})
                        self.take("END_IF")
                    result.append(("if", branches, alternative))
                    break
            else:
                self.take(":=")
                expression = self.expression(";")
                self.take(";")
                result.append(("set", name, expression))
        return result

    @staticmethod
    def value(tokens, values):
        operators = {"AND": "and", "OR": "or", "NOT": "not", "=": "==", "<>": "!="}
        text = " ".join(operators.get(token, token) for token in tokens)
        return eval(text, {"__builtins__": {}}, values)

    def execute(self, owner, station, action, locked=False, online=True):
        values = dict(nOwner=owner, nStation=station, nAction=action,
                      xLocked=locked, xOnline=online)

        def run(statements):
            for kind, arg, body in statements:
                if kind == "set":
                    values[arg] = self.value(body, values)
                else:
                    for condition, branch in arg:
                        if self.value(condition, values):
                            run(branch)
                            break
                    else:
                        run(body)

        run(self.program)
        return values["F_ControlDecision"]


class AuthorityChecks(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.decision = DecisionProgram()

    def test_running_rejects_all_handoffs_including_higher_priority(self):
        for owner in (1, 2, 3):
            for station in (1, 2, 3):
                expected = 1 if station == owner else 2
                self.assertEqual(self.decision.execute(owner, station, 1, locked=True), expected)

    def test_running_rejects_release(self):
        for owner in (1, 2, 3):
            for station in (1, 2, 3):
                self.assertEqual(self.decision.execute(owner, station, 2, locked=True), 2)

    def test_stopped_uses_priority_without_field_switches(self):
        self.assertEqual(self.decision.execute(3, 1, 1), 1)
        self.assertEqual(self.decision.execute(2, 3, 1), 6)
        # 无持有者时三个在线站均能申请；已有持有者时完整检查优先级矩阵。
        for station in (1, 2, 3):
            self.assertEqual(self.decision.execute(0, station, 1), 1)
            for owner in (1, 2, 3):
                self.assertEqual(self.decision.execute(owner, station, 1), 1 if station <= owner else 6)
        self.assertEqual(self.decision.execute(0, 3, 1, online=False), 4)
        self.assertEqual(self.decision.execute(2, 2, 2), 1)
        self.assertEqual(self.decision.execute(2, 1, 2), 5)

    def test_offline_stations_cannot_request_or_release(self):
        for owner in (0, 1, 2, 3):
            for station in (1, 2, 3):
                for action in (1, 2):
                    self.assertEqual(self.decision.execute(owner, station, action, online=False), 4)

    def test_no_field_authorization_dependency(self):
        sources = "\n".join(path.read_text(encoding="utf-8") for path in (ROOT / "axis_4").rglob("*.Tc*"))
        for removed in ("xCenterMaster", "xLocalMaster", "xAllowLocal", "xAllowRemote", "xAllowed", "axAllowed"):
            self.assertNotIn(removed, sources)

    def test_invalid_actions_never_grant(self):
        for station in (0, 4):
            self.assertEqual(self.decision.execute(0, station, 1), 7)
        for action in (0, 3):
            self.assertEqual(self.decision.execute(0, 1, action), 7)

    def test_lock_sources_and_no_command_bypass(self):
        main = ET.parse(ROOT / "axis_4/POUs/MAIN.TcPOU").findtext(".//ST")
        for condition in ("Status.Moving", "Status.Homing", "Status.Stopping",
                          "Status.ExtSetPointGenEnabled", "NcToPlc.ActVelo", "Axis1.xBusy"):
            self.assertIn(condition, main)
        self.assertLess(main.index("fbAuthority("), main.index("aCommandGate[1]("))
        self.assertLess(main.index("aCommandGate[4]("), main.index("fbAxis1("))
        authority = ET.parse(ROOT / "axis_4/POUs/FB_ControlAuthority.TcPOU").findtext(".//ST")
        self.assertIn("IF NOT xLocked AND xOwnerLost", authority)
        self.assertIn("udiRequestSequence <> GVL_control.aStation[i].udiAckSequence", authority)
        self.assertIn("GVL_control.aStation[i].aAxis := aEmpty", authority)
        self.assertIn("OR GVL_control.aStation[nOwner].aAxis[j].gohome", authority)

    def test_idle_revocation_clears_loss_latch_before_new_requests(self):
        source = ET.parse(ROOT / "axis_4/POUs/FB_ControlAuthority.TcPOU").findtext(".//ST")
        revoke = source.index("IF NOT xLocked AND xOwnerLost")
        dispatch = source.index("// 同一扫描先处理释放")
        # 清失联锁存后才处理新请求，防止新持有者继承上一站的失联状态。
        self.assertIn("xOwnerLost := FALSE;", source[revoke:dispatch])
        self.assertIn("xResetCommands := TRUE;", source[revoke:dispatch])
        # 同扫描重新授予原站也清命令，避免旧点动/回原保持位重放。
        self.assertIn("(nOwner <> nOldOwner) OR xResetCommands", source)


class AxisInterlockChecks(unittest.TestCase):
    """执行基础轴真实条件，确认控制权精简没有绕过运动和复位联锁。"""

    def test_each_nc_motion_condition_blocks_recovery(self):
        source = ET.parse(ROOT / "axis_4/POUs/MAIN.TcPOU").findtext(".//ST")
        assignment = re.search(r"xControlBusy := xControlBusy.*?;", source, re.S)[0]
        # 测试解释器不解析小数 token；保留真实比较式，将阈值绑定为同值常量。
        program = StBranch(assignment.replace("0.000001", "cVelocityTolerance"))
        for field in (None, "Moving", "Homing", "Stopping", "ExtSetPointGenEnabled", "velocity"):
            status = NS(Moving=False, Homing=False, Stopping=False,
                        ExtSetPointGenEnabled=False, IoDataInvalid=True)
            if field and field != "velocity": setattr(status, field, True)
            axis = NS(Status=status, NcToPlc=NS(ActVelo=0.1 if field == "velocity" else 0))
            for index in (1, 2, 3, 4):
                values = dict(xControlBusy=False, GVL_sy=NS(axis={index: axis}), i=index,
                              ABS=abs, cVelocityTolerance=0.000001)
                program.run(values)
                self.assertEqual(values["xControlBusy"], field is not None)

    def test_invalid_io_blocks_new_power_but_not_reset(self):
        source = ET.parse(ROOT / "axis_4/POUs/FB_AxisControl.TcPOU").findtext(".//ST")
        enable = re.search(r"fbPower\(Axis := Axis, Enable := (.*?),\s*Enable_Positive", source, re.S)[1]
        reset = re.search(r"fbReset\(Axis := Axis, Execute := (.*?)\);", source, re.S)[1]
        manual = re.search(r"xManual := .*?;", source, re.S)[0]
        program = StBranch("xEnable := " + enable + "; xReset := " + reset + "; " + manual)
        for invalid in (False, True):
            values = dict(Data=NS(xStart=True, reset=True, xStop=False), xPermit=True,
                          xConfigValid=True, xHoldPower=False, Axis=NS(Status=NS(
                              IoDataInvalid=invalid, Moving=False, Error=False, ExtSetPointGenEnabled=False)),
                          rtReset=NS(Q=True), xGroupBusy=False, xStopRequest=False,
                          fbPower=NS(Status=True), fbStop=NS(Busy=False), fbReset=NS(Busy=False))
            program.run(values)
            self.assertEqual(values["xEnable"], not invalid)
            self.assertTrue(values["xReset"], "反馈故障不能再次堵住已授权的复位入口")
            self.assertFalse(values["xManual"], "复位同扫描不能启动手动运动")
            values["Data"].reset = False
            program.run(values)
            self.assertEqual(values["xManual"], not invalid)


if __name__ == "__main__":
    unittest.main(verbosity=2)
