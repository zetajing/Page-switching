"""核对源码报告与只读边界，使用临时工程，不修改实际 PLC。"""
from contextlib import redirect_stdout
import io
import json
from pathlib import Path
import shutil
import sys
import tempfile
import unittest
from unittest.mock import patch
import xml.etree.ElementTree as ET
import check_source_copy


class SourceReportChecks(unittest.TestCase):
    def test_saved_source_report_preserves_input_and_scoped_references(self):
        with tempfile.TemporaryDirectory(prefix='PLC源码核对_') as directory:
            actual = Path(directory) / 'actual'
            source = actual / 'axis_4/GVLs/GVL_motion.TcGVL'
            source.parent.mkdir(parents=True)
            shutil.copyfile(check_source_copy.ROOT / 'axis_4/axis_4.plcproj', actual / 'axis_4/axis_4.plcproj')
            shutil.copyfile(check_source_copy.ROOT / 'axis_4/GVLs/GVL_motion.TcGVL', source)
            output = Path(directory) / 'source-report.json'
            arguments = ['check_source_copy', '--actual', str(actual), '--only', 'GVLs/GVL_motion.TcGVL', '--report', str(output)]
            with patch.object(sys, 'argv', arguments), redirect_stdout(io.StringIO()):
                self.assertEqual(check_source_copy.main(), 0)
            report = json.loads(output.read_text(encoding='utf-8'))
            self.assertEqual(len(report['managed_sources']), 1)
            self.assertEqual(report['extra_actual_references'], [], '受管但未参与 --only 的文件不应被误报为额外源码')
            self.assertTrue(report['managed_sources'][0]['equivalent_ignoring_format'])
            self.assertEqual(len(report['git_commit']), 40)
            self.assertIn('No XAE compile', report['verification_scope'])
            tree = ET.parse(source)
            declaration = tree.find('.//Declaration')
            declaration.text += '\n// 测试已有差异，核对工具不得覆盖。'
            tree.write(source, encoding='utf-8')
            before = source.read_bytes()
            with patch.object(sys, 'argv', arguments), redirect_stdout(io.StringIO()):
                self.assertEqual(check_source_copy.main(), 1)
            report = json.loads(output.read_text(encoding='utf-8'))
            self.assertEqual(report['differences'], 1)
            self.assertNotEqual(report['managed_sources'][0]['repository_sha256'], report['managed_sources'][0]['actual_sha256'])
            self.assertEqual(source.read_bytes(), before, '核对只能读，不能覆盖实际源码')


if __name__ == '__main__':
    unittest.main(verbosity=2)
