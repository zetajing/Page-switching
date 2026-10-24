"""核对轨迹 Action 分派和整理前的执行语句；不替代 XAE 编译。"""
import hashlib
from pathlib import Path
import re
import tempfile
import unittest
import xml.etree.ElementTree as ET
from plc_source import pou_source

ROOT = Path(__file__).resolve().parents[1]
PATH = ROOT / 'axis_4/POUs/Trajectory_点位与轨迹/FB_FourAxisTrajectory.TcPOU'


class TrajectoryActionChecks(unittest.TestCase):
    def test_expanded_statements_equal_pre_refactor_baseline(self):
        # 基线来自 e21965f 保存的 499 行 ST：只允许搬移与加注释，不能改运行语句。
        source = re.sub(r'//[^\n]*|\(\*.*?\*\)', '', pou_source(PATH), flags=re.S)
        tokens = re.findall(r"'(?:''|[^'])*'|\w+|:=|<>|<=|>=|[^\s]", source)
        self.assertEqual(len(tokens), 3131)
        self.assertEqual(hashlib.sha256(' '.join(tokens).encode()).hexdigest(),
                         '1ce8fc4e4886e9b8f13c5d8d3ca96c15abdccc95923cb7084afc554703a1110e')

    def test_one_phase_dispatch_and_fixed_motion_call_order(self):
        pou = ET.parse(PATH).find('POU')
        main = re.sub(r'//[^\n]*', '', pou.findtext('./Implementation/ST'))
        actions = [node.get('Name') for node in pou.findall('Action')]
        self.assertEqual(len(actions), 8)
        for name in actions: self.assertEqual(main.count(name + '();'), 1)
        self.assertEqual(main.count('CASE nState OF'), 1)
        self.assertIn('0, 5: A04_ReceiveAndAssemble();', main)
        self.assertIn('10, 15, 20, 30: A05_PrepareAndRun();', main)
        self.assertIn('40, 50, 60, 70: A06_StopAndExit();', main)
        self.assertLess(main.index('A03_ResetAndInterlocks();'), main.index('CASE nState OF'))
        self.assertLess(main.index('END_CASE'), main.index('A07_CallMotionBlocks();'))
        self.assertLess(main.index('A07_CallMotionBlocks();'), main.index('A08_PublishFeedback();'))
        expanded = pou_source(PATH)
        for call in ['aMoveInit[i](', 'aStopInit[i](', 'MC_ExtSetPointGenFeed(', 'aEnable[i](', 'aDisable[i](']:
            self.assertEqual(expanded.count(call), 1, call)

    def test_invalid_action_graph_and_dispatch_are_rejected(self):
        with tempfile.TemporaryDirectory() as directory:
            path = Path(directory) / 'Invalid.TcPOU'
            for entry, body in [('A01_Test();', 'A01_Test();'), ('A02_Missing();', ''),
                                ('CASE nState OF\n0, 5: A01_Test();\nEND_CASE', 'CASE nState OF\n0: x := 1;\nEND_CASE')]:
                root = ET.Element('TcPlcObject')
                pou = ET.SubElement(root, 'POU')
                ET.SubElement(ET.SubElement(pou, 'Implementation'), 'ST').text = entry
                action = ET.SubElement(pou, 'Action', Name='A01_Test')
                ET.SubElement(ET.SubElement(action, 'Implementation'), 'ST').text = body
                ET.ElementTree(root).write(path, encoding='utf-8')
                with self.assertRaises(ValueError): pou_source(path)


if __name__ == '__main__': unittest.main(verbosity=2)
