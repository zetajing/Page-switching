"""执行真实 F_ControlDecision 的 IF/赋值子集，检查运行锁；不模拟 NC 或 ADS。"""

from pathlib import Path
import re
import unittest
import xml.etree.ElementTree as ET


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

    def execute(self, owner, station, action, locked=False, allowed=True, online=True):
        values = dict(nOwner=owner, nStation=station, nAction=action,
                      xLocked=locked, xAllowed=allowed, xOnline=online)

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

    def test_stopped_uses_priority_and_explicit_authorization(self):
        self.assertEqual(self.decision.execute(3, 1, 1), 1)
        self.assertEqual(self.decision.execute(2, 3, 1), 6)
        self.assertEqual(self.decision.execute(0, 3, 1, allowed=False), 3)
        self.assertEqual(self.decision.execute(0, 3, 1, online=False), 4)
        self.assertEqual(self.decision.execute(2, 2, 2), 1)
        self.assertEqual(self.decision.execute(2, 1, 2), 5)

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
        self.assertIn("IF NOT xLocked AND (xOwnerLost OR NOT axAllowed[nOwner])", authority)
        self.assertIn("udiRequestSequence <> GVL_control.aStation[i].udiAckSequence", authority)
        self.assertIn("GVL_control.aStation[i].aAxis := aEmpty", authority)
        self.assertIn("OR GVL_control.aStation[nOwner].aAxis[j].gohome", authority)

    def test_idle_revocation_clears_loss_latch_before_new_requests(self):
        source = ET.parse(ROOT / "axis_4/POUs/FB_ControlAuthority.TcPOU").findtext(".//ST")
        revoke = source.index("IF NOT xLocked AND (xOwnerLost OR NOT axAllowed[nOwner])")
        dispatch = source.index("// 同一扫描先处理释放")
        # 清失联锁存后才处理新请求，防止新持有者继承上一站的失联状态。
        self.assertIn("xOwnerLost := FALSE;", source[revoke:dispatch])
        self.assertIn("xResetCommands := TRUE;", source[revoke:dispatch])
        # 同扫描重新授予原站也清命令，避免旧点动/回原保持位重放。
        self.assertIn("(nOwner <> nOldOwner) OR xResetCommands", source)


if __name__ == "__main__":
    unittest.main(verbosity=2)
