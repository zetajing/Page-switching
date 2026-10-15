"""离线检查项目配置、位置差分和缓冲边界；不替代 TwinCAT 编译或实机测试。"""

from array import array
from pathlib import Path
import math
import random
import re
import unittest
import xml.etree.ElementTree as ET


ROOT = Path(__file__).resolve().parents[1]
PLC = ROOT / "axis_4"


def st_source(name):
    return ET.parse(PLC / "POUs" / name).findtext(".//ST")


class TrajectoryChecks(unittest.TestCase):
    def test_project_files_and_task(self):
        # 校验工程实际引用的所有文件，避免新增程序漏加进项目。
        ns = {"p": "http://schemas.microsoft.com/developer/msbuild/2003"}
        project = ET.parse(PLC / "axis_4.plcproj")
        includes = [node.attrib["Include"].replace("\\", "/")
                    for node in project.findall(".//p:Compile", ns)]
        self.assertEqual(len(includes), len(set(includes)))
        for name in includes:
            self.assertTrue((PLC / name).is_file(), name)
        for path in PLC.rglob("*"):
            if path.suffix in (".TcPOU", ".TcDUT", ".TcGVL", ".TcTTO"):
                ET.parse(path)
                self.assertIn(path.relative_to(PLC).as_posix(), includes)
        task = ET.parse(PLC / "PlcTask.TcTTO")
        system = ET.parse(ROOT / "TwinCAT_NB.tsproj")
        self.assertEqual(task.findtext(".//CycleTime"), "2000")
        # 一个工程可有多个系统任务，必须检查 PLC 上下文实际绑定的任务。
        # 本工程系统任务 OTCID 的低 16 位是任务 Id 左移 4 位，如 Id=6 对应 #x02010060。
        bound_otcid = system.findtext(".//Contexts/Context/ManualConfig/OTCID")
        bound_id = (int(bound_otcid.removeprefix("#x"), 16) & 0xFFFF) >> 4
        bound_task = system.find(f".//System/Tasks/Task[@Id='{bound_id}']")
        self.assertIsNotNone(bound_task, f"PLC 绑定的系统任务不存在：{bound_otcid}")
        self.assertEqual(bound_task.get("CycleTime"), "20000")
        self.assertEqual(bound_task.get("Priority"), "7")
        self.assertEqual(system.find(".//SafTask").get("CycleTime"), "20000")
        self.assertEqual(system.findtext(".//Contexts/Context/CycleTime"), "2000000")
        self.assertEqual(task.findtext(".//Priority"), "7")
        self.assertEqual(system.findtext(".//Contexts/Context/Priority"), "7")
        self.assertEqual(system.find(".//TaskPouOid").get("Prio"), "7")

    def test_control_block_nesting(self):
        # 只检查控制结构配对，不将此结果当作 ST 编译成功。
        closing = {"END_IF": "IF", "END_FOR": "FOR", "END_CASE": "CASE"}
        for path in (PLC / "POUs").glob("*.TcPOU"):
            source = st_source(path.name)
            source = re.sub(r"\(\*.*?\*\)", "", source, flags=re.S)
            source = re.sub(r"//[^\n]*", "", source)
            stack = []
            for token in re.findall(r"\b(?:IF|FOR|CASE|END_IF|END_FOR|END_CASE)\b", source):
                if token in closing:
                    self.assertTrue(stack, str(path))
                    self.assertEqual(stack.pop(), closing[token], str(path))
                else:
                    stack.append(token)
            self.assertFalse(stack, str(path))

    def test_capacity_and_chunk_boundaries(self):
        source = (PLC / "POUs" / "FB_FourAxisTrajectory.TcPOU").read_text(encoding="utf-8")
        motion = (PLC / "GVLs" / "GVL_motion.TcGVL").read_text(encoding="utf-8")
        self.assertIn("ARRAY[1..4, 0..2099999] OF LREAL", source)
        self.assertIn("cMaxPoints : UDINT := 2100000", source)
        self.assertIn("ARRAY[0..20, 1..4, 0..99999] OF LREAL", motion)
        self.assertIn("cUploadPoints : UDINT := 100000", source)
        self.assertIn("nPointCount : UDINT", motion)
        self.assertIn("nCurrentPoint : UDINT", motion)
        self.assertEqual((1_800_001 - 1) * .002, 3600)
        # 检查整段、短末段以及 60 分钟和最大容量的所有首末索引。
        for count in (4, 99999, 100000, 100001, 1800001, 2100000):
            offset = 0
            while offset < count:
                size = min(100000, count - offset)
                self.assertLessEqual(offset + size, count)
                self.assertLess(offset + size - 1, 2100000)
                self.assertLess(size - 1, 100000)
                offset += size
            self.assertEqual(offset, count)
        # 四轴独立编码，核对段边界拼接不会串轴或漏点。
        count = 100003
        table = [array("d", [0]) * count for _ in range(4)]
        for offset in (0, 100000):
            size = min(100000, count - offset)
            for axis in range(4):
                block = array("d", (axis * 1000000 + offset + k for k in range(size)))
                table[axis][offset:offset + size] = block
        for axis in range(4):
            for index in (0, 99999, 100000, 100002):
                self.assertEqual(table[axis][index], axis * 1000000 + index)

    def test_actual_st_difference_expressions(self):
        # 直接执行 ST 文件中的算术表达式，检查常值、线性和二次位置表。
        source = st_source("F_TrajectoryPoint.TcPOU")
        expressions = {}
        for field in ("lrVelocity", "lrAcceleration"):
            expressions[field] = re.findall(r"F_TrajectoryPoint\." + field + r" := ([^;]+);", source)[-1]
        for time in (0., .1, 1., 100.):
            for a, b, c in ((0., 0., 10.), (0., -3., 2.), (2., 3., -1.)):
                p = lambda t: a * t * t + b * t + c
                values = {"lrPrevious": p(time - .002), "lrCurrent": p(time),
                          "lrNext": p(time + .002), "cDt": .002}
                v = eval(expressions["lrVelocity"], {"__builtins__": {}}, values)
                acc = eval(expressions["lrAcceleration"], {"__builtins__": {}}, values)
                self.assertAlmostEqual(v, 2 * a * time + b, delta=1e-7)
                self.assertAlmostEqual(acc, 2 * a, delta=2e-6)

    def test_smooth_position_only_packet(self):
        # 上位机只提供平滑位置，并重复首末点；检查差分后的速度/加速度界限。
        dt = .002
        data = [10 * (k / 1000) ** 3 - 15 * (k / 1000) ** 4 + 6 * (k / 1000) ** 5
                for k in range(1001)]
        data = [data[0]] + data + [data[-1]]
        self.assertEqual(data[0], data[1])
        self.assertEqual(data[-2], data[-1])
        for k in range(1, len(data) - 1):
            v = (data[k + 1] - data[k - 1]) / (2 * dt)
            acc = (data[k + 1] - 2 * data[k] + data[k - 1]) / dt ** 2
            self.assertLessEqual(abs(v), 1.)
            self.assertLessEqual(abs(acc), 2.)
            self.assertTrue(math.isfinite(acc))

    def test_sampled_stopping_distance(self):
        # 对正反方向和不同减速度，独立核对连续制动距离加一个采样周期的余量。
        rng = random.Random(421)
        for _ in range(1000):
            velocity = rng.uniform(-100, 100)
            deceleration = rng.uniform(10, 500)
            sign = 1 if velocity >= 0 else -1
            position = 0.
            remaining = abs(velocity)
            while remaining > 0:
                next_speed = max(0, remaining - deceleration * .002)
                position += sign * (remaining + next_speed) * .5 * .002
                remaining = next_speed
            bound = velocity ** 2 / (2 * deceleration) + abs(velocity) * .002
            self.assertLessEqual(abs(position), bound + 1e-9)


if __name__ == "__main__":
    unittest.main(verbosity=2)
