"""执行真实 ST 的收段/拼表分支；模拟内存复制，不连接 ADS、NC 或实际 PLC。"""

from pathlib import Path
from types import SimpleNamespace
import re
import unittest
import xml.etree.ElementTree as ET


ROOT = Path(__file__).resolve().parents[1]
POU = ET.parse(ROOT / "axis_4/POUs/FB_FourAxisTrajectory.TcPOU")
SOURCE = re.sub(r"//[^\n]*", "", POU.findtext(".//ST"))


class StBranch:
    """仅支持本次分支使用的 IF、FOR 和赋值；不支持的语法直接使测试失败。"""

    def __init__(self, source):
        self.tokens = re.findall(r"\w+(?:\.\w+)*|:=|<>|<=|>=|[();,\[\]<>=+*/\-]", source)
        self.index = 0
        self.statements = self.block(set())

    def take(self, expected=None):
        token = self.tokens[self.index]
        self.index += 1
        if expected is not None:
            assert token == expected, (token, expected)
        return token

    def until(self, end):
        result = []
        while self.tokens[self.index] != end:
            result.append(self.take())
        self.take(end)
        return " ".join(result)

    def block(self, stops):
        statements = []
        while self.index < len(self.tokens) and self.tokens[self.index] not in stops:
            token = self.take()
            if token == ";":
                continue
            if token == "IF":
                branches = []
                while True:
                    condition = self.until("THEN")
                    body = self.block({"ELSIF", "ELSE", "END_IF"})
                    branches.append((condition, body))
                    stop = self.take()
                    if stop == "ELSIF":
                        continue
                    alternative = []
                    if stop == "ELSE":
                        alternative = self.block({"END_IF"})
                        self.take("END_IF")
                    statements.append(("if", branches, alternative))
                    break
            elif token == "FOR":
                variable = self.take()
                self.take(":=")
                start, end = self.until("TO"), self.until("DO")
                body = self.block({"END_FOR"})
                self.take("END_FOR")
                statements.append(("for", variable, start, end, body))
            else:
                target = token
                self.take(":=")
                statements.append(("set", target, self.until(";")))
        return statements

    @staticmethod
    def value(expression, values):
        # ADR 作为受检的数组地址描述，MEMCPY 模拟器记录复制范围，无须分配 134 MB。
        expression = re.sub(r"ADR\s*\(\s*(\w+(?:\.\w+)*)\s*\[([^]]+)\]\s*\)",
                            r'ADR("\1", (\2))', expression)
        expression = expression.replace(":=", "=").replace("<>", "!=")
        # 函数命名参数保留单等号，其余单等号转换为比较。
        expression = re.sub(r"(?<![!<>=])=(?!=)", "==", expression)
        expression = re.sub(r"\b(destAddr|srcAddr|n)\s*==", r"\1=", expression)
        for before, after in (("AND", "and"), ("OR", "or"), ("NOT", "not"),
                              ("TRUE", "True"), ("FALSE", "False"), ("MOD", "%")):
            expression = re.sub(r"\b" + before + r"\b", after, expression)
        expression = expression.replace("/", "//")  # 本次分支只有 UDINT 整数除法。
        return eval(expression, {"__builtins__": {}}, values)

    def run(self, values):
        def execute(statements):
            for statement in statements:
                if statement[0] == "if":
                    for condition, body in statement[1]:
                        if self.value(condition, values):
                            execute(body)
                            break
                    else:
                        execute(statement[2])
                elif statement[0] == "for":
                    _, variable, start, end, body = statement
                    for index in range(self.value(start, values), self.value(end, values) + 1):
                        values[variable] = index
                        execute(body)
                else:
                    _, target, expression = statement
                    value = self.value(expression, values)
                    if "." in target:
                        parent, member = target.rsplit(".", 1)
                        setattr(values[parent], member, value)
                    else:
                        values[target] = value
        execute(self.statements)


class AssemblyFixture:
    def __init__(self, count=100003):
        self.copies = []
        self.fail_axis = None
        self.motion = SimpleNamespace(nPointCount=count, nSampleTimeUs=2000, udiUploadSequence=7,
                                      udiAcceptedSequence=0, nUploadOffset=0, nUploadCount=0,
                                      xDataReady=False, xDone=False, xError=False, udiErrorId=0,
                                      nCurrentPoint=0, aConfig=[])
        constants = dict(re.findall(r"\b(\w+)\s*:\s*(?:U?D?INT)\s*:=\s*(\d+)",
                                    POU.findtext(".//Declaration")))
        self.values = dict(nCount=0, nReceived=0, nLoaded=0, udiSequence=0, nState=0,
                           xAllDisabled=True, xMovesIdle=True, xAxesReady=True,
                           xAbort=False, xAxisFault=False, tmHeartbeat=SimpleNamespace(Q=False),
                           GVL_motion=self.motion, MIN=min, SIZEOF=lambda _: 8,
                           ADR=lambda name, indices: (name, indices), MEMCPY=self.copy,
                           aPosition={(1, 0): 0.0})
        self.motion.xStop = False
        self.values.update({name: int(value) for name, value in constants.items()})
        self.branches = {state: StBranch(re.search(r"\n" + str(state) + r":(.*?)(?=\n\d+:)",
                                                 SOURCE, re.S)[1]) for state in (0, 5)}
        self.feedback = StBranch(SOURCE[SOURCE.index("GVL_motion.nReceivedCount :="):])

    def copy(self, destAddr, srcAddr, n):
        assert destAddr[0] == "aPosition" and srcAddr[0] == "GVL_motion.aUpload"
        axis, offset = destAddr[1]
        segment, source_axis, point = srcAddr[1]
        assert axis == source_axis and 1 <= axis <= 4 and 0 <= segment <= 20
        assert n > 0 and n % 8 == 0 and n <= self.values["cCopyPointsPerScan"] * 8
        size = n // 8
        assert 0 <= point < 100000 and point + size <= 100000
        assert offset == segment * 100000 + point
        assert 0 <= offset < self.motion.nPointCount and offset + size <= self.motion.nPointCount
        if axis == self.fail_axis:
            return 0
        self.copies.append((axis, offset, size))
        return n

    def scan(self, request=None):
        for name in ("Upload", "Assemble", "Clear", "Start"):
            self.values["rt" + name] = SimpleNamespace(Q=request == name)
        self.branches[self.values["nState"]].run(self.values)
        self.feedback.run(self.values)

    def upload(self, offset, size):
        self.motion.nUploadOffset, self.motion.nUploadCount = offset, size
        self.scan("Upload")

    def upload_all(self):
        for offset in range(0, self.motion.nPointCount, 100000):
            self.upload(offset, min(100000, self.motion.nPointCount - offset))

    def assemble(self):
        self.scan("Assemble")
        while self.values["nState"] == 5:
            self.scan()


class UploadAssemblyChecks(unittest.TestCase):
    def test_no_copy_until_all_segments_and_completion_request(self):
        f = AssemblyFixture()
        f.upload(0, 100000)
        f.scan()
        self.assertEqual((f.values["nReceived"], f.values["nLoaded"], len(f.copies)), (100000, 0, 0))
        f.upload(100000, 3)
        f.scan()
        self.assertFalse(f.motion.xDataReady)
        self.assertEqual((f.values["nReceived"], f.values["nLoaded"], len(f.copies)), (100003, 0, 0))
        self.assertEqual((f.motion.nReceivedCount, f.motion.udiReceivedSequence), (100003, 7))
        f.assemble()
        self.assertTrue(f.motion.xDataReady)
        self.assertEqual(f.motion.nLoadedCount, 100003)
        self.assertFalse(f.motion.xUploadBusy)

    def test_capacity_tail_and_four_axis_copy_ranges(self):
        for count in (4, 99999, 100000, 100001, 1800001, 2100000):
            with self.subTest(count=count):
                f = AssemblyFixture(count)
                f.upload_all()
                f.assemble()
                self.assertTrue(f.motion.xDataReady)
                for axis in range(1, 5):
                    expected_offset = 0
                    for copied_axis, offset, size in f.copies:
                        if copied_axis == axis:
                            self.assertEqual(offset, expected_offset)
                            expected_offset += size
                    self.assertEqual(expected_offset, count)

    def test_invalid_segment_and_metadata_never_advance(self):
        cases = ((0, 99999, {}), (1, 100000, {}), (0, 0, {}), (0, 100001, {}),
                 (0, 100000, {"nPointCount": 2100001}), (0, 100000, {"nPointCount": 3}),
                 (0, 100000, {"nSampleTimeUs": 1000}), (0, 100000, {"udiUploadSequence": 0}),
                 (0, 100000, {"udiAcceptedSequence": 7}))
        for offset, size, changes in cases:
            with self.subTest(offset=offset, size=size, changes=changes):
                f = AssemblyFixture()
                for field, value in changes.items():
                    setattr(f.motion, field, value)
                f.upload(offset, size)
                self.assertEqual((f.values["nReceived"], f.values["nLoaded"]), (0, 0))
                self.assertEqual(f.motion.udiErrorId, 1001)

    def test_duplicate_missing_segment_and_early_start(self):
        for request in ("Assemble", "Start", "Upload"):
            f = AssemblyFixture()
            f.upload(0, 100000)
            f.scan(request)
            self.assertTrue(f.motion.xError)
            self.assertFalse(f.motion.xDataReady)
            self.assertFalse(f.copies)

    def test_sequence_or_total_change_between_segments(self):
        for field, value in (("udiUploadSequence", 8), ("nPointCount", 100004)):
            f = AssemblyFixture()
            f.upload(0, 100000)
            setattr(f.motion, field, value)
            f.upload(100000, f.motion.nPointCount - 100000)
            self.assertEqual(f.values["nReceived"], 100000)
            self.assertTrue(f.motion.xError)

    def test_requests_during_assembly_and_start_before_assembly(self):
        for request in ("Upload", "Clear", "Assemble"):
            f = AssemblyFixture()
            f.upload_all()
            f.scan("Assemble")
            self.assertTrue(f.motion.xUploadBusy)
            f.scan(request)
            self.assertTrue(f.motion.xError)
            self.assertFalse(f.motion.xDataReady)
            self.assertFalse(f.copies)
        f = AssemblyFixture()
        f.upload_all()
        f.scan("Start")
        self.assertEqual(f.motion.udiErrorId, 1001)
        self.assertFalse(f.copies)

    def test_copy_failure_does_not_publish_partial_four_axis_batch(self):
        f = AssemblyFixture()
        f.upload_all()
        f.scan("Assemble")
        f.fail_axis = 3
        f.scan()
        self.assertEqual(f.values["nLoaded"], 0)
        self.assertEqual(f.motion.udiErrorId, 1012)
        self.assertFalse(f.motion.xDataReady)

    def test_assembly_metadata_change_and_recovery(self):
        f = AssemblyFixture()
        f.upload_all()
        f.scan("Assemble")
        f.scan()
        self.assertGreater(f.values["nLoaded"], 0)
        f.motion.udiUploadSequence = 8
        f.scan()
        self.assertTrue(f.motion.xError)
        self.assertFalse(f.motion.xDataReady)
        # 此处仅模拟外部复位完成，重拼必须重新从 0 开始。
        f.values["nState"], f.motion.xError, f.motion.udiErrorId = 0, False, 0
        f.motion.udiUploadSequence = 7
        f.scan("Assemble")
        self.assertEqual(f.values["nLoaded"], 0)
        while f.values["nState"] == 5:
            f.scan()
        self.assertTrue(f.motion.xDataReady)

    def test_clear_invalidates_old_table_and_start_requires_ready(self):
        f = AssemblyFixture()
        f.upload_all()
        f.assemble()
        f.scan("Start")
        self.assertEqual(f.values["nState"], 10)
        f.values["nState"] = 0
        f.scan("Clear")
        self.assertEqual((f.values["nCount"], f.values["nReceived"], f.values["nLoaded"],
                          f.values["udiSequence"]), (0, 0, 0, 0))
        self.assertFalse(f.motion.xDataReady)
        f.scan("Start")
        self.assertEqual(f.motion.udiErrorId, 1001)


if __name__ == "__main__":
    unittest.main(verbosity=2)
