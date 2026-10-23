"""只比较仓库受管 PLC 源码；不复制、不删除、不覆盖实际工程的额外文件。"""
from pathlib import Path
import argparse
from datetime import datetime, timezone
import difflib
import hashlib
import json
import re
import subprocess
import sys
import xml.etree.ElementTree as ET

ROOT = Path(__file__).resolve().parents[1]

def code(path):
    root = ET.parse(path).getroot()
    parts = []
    for item in root.iter():
        if item.tag in ('POU', 'Action', 'Method', 'DUT', 'GVL'):
            parts.append(item.tag + ' ' + str(item.get('Name')))
        if item.tag in ('Declaration', 'ST'):
            source = '\n'.join(line.rstrip() for line in (item.text or '').strip().splitlines())
            parts.append(item.tag + '\n' + source)
    # Task 配置没有 ST，比较其完整 XML；对象 GUID 不作为程序版本依据。
    return '\n'.join(parts) if parts else ET.tostring(root, encoding='unicode')

def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--actual', type=Path, required=True, help='实际 TwinCAT_NB 工程目录')
    parser.add_argument('--only', nargs='+', help='可选：仅比较指定受管文件，路径相对于 axis_4')
    parser.add_argument('--report', type=Path, help='可选：保存源码哈希和差异清单；不编译、不下载')
    args = parser.parse_args()
    actual = args.actual.resolve()
    if not (actual / 'axis_4/axis_4.plcproj').is_file():
        parser.error('实际目录下没有 axis_4/axis_4.plcproj')
    project = ET.parse(ROOT / 'axis_4/axis_4.plcproj')
    files = [node.get('Include').replace('\\', '/') for node in project.findall('.//{*}Compile')]
    managed_files = files.copy()
    if args.only:
        for name in args.only:
            if name not in files: parser.error('不属于仓库受管源码：' + name)
        files = args.only
    differences = 0
    records = []
    for name in files:
        local = ROOT / 'axis_4' / name
        target = actual / 'axis_4' / name
        record = {'file': name, 'repository_sha256': hashlib.sha256(local.read_bytes()).hexdigest(),
                  'actual_sha256': hashlib.sha256(target.read_bytes()).hexdigest() if target.is_file() else None,
                  'equivalent_ignoring_format': False}
        records.append(record)
        if not target.is_file():
            print('MISSING ' + name)
            differences += 1
            continue
        left, right = code(local), code(target)
        # 按词比较忽略代码排版，字符串和整行注释保留，防止把注释边界当成空白忽略。
        tokens = lambda text: re.findall(r"//[^\n]*|\(\*.*?\*\)|'(?:''|[^'])*'|\w+|:=|<>|<=|>=|[^\s]", text, re.S)
        record['equivalent_ignoring_format'] = tokens(left) == tokens(right)
        if not record['equivalent_ignoring_format']:
            differences += 1
            print('DIFFERENT ' + name)
            print('\n'.join(difflib.unified_diff(left.splitlines(), right.splitlines(), fromfile='repository', tofile='actual', n=2)))
    if args.report:
        # 记录的是保存到磁盘的源码。哈希、文件时间均不能代替 XAE 构建或在线程序证据。
        repository = subprocess.check_output(['git', '-C', str(ROOT), 'rev-parse', '--show-toplevel'], text=True, encoding='utf-8').strip()
        commit = subprocess.check_output(['git', '-C', repository, 'rev-parse', 'HEAD'], text=True, encoding='utf-8').strip()
        dirty = bool(subprocess.check_output(['git', '-C', repository, 'status', '--porcelain'], text=True, encoding='utf-8').strip())
        actual_project = ET.parse(actual / 'axis_4/axis_4.plcproj')
        extra = [n.get('Include').replace('\\', '/') for n in actual_project.findall('.//{*}Compile')
                 if n.get('Include').replace('\\', '/') not in managed_files]
        report = {'recorded_at_utc': datetime.now(timezone.utc).isoformat(), 'git_commit': commit,
                  'working_tree_was_dirty': dirty, 'repository_plc': str(ROOT), 'actual_plc': str(actual),
                  'differences': differences, 'managed_sources': records, 'extra_actual_references': extra,
                  'verification_scope': 'Saved source comparison only. No XAE compile, download or online verification.'}
        output = args.report.resolve()
        # 禁止把报告写到工程源码上；报告应保存为单独 JSON 文件。
        if output.suffix.lower() != '.json': parser.error('--report 必须是独立的 .json 文件')
        output.parent.mkdir(parents=True, exist_ok=True)
        output.write_text(json.dumps(report, ensure_ascii=False, indent=2) + '\n', encoding='utf-8')
        print('Source report: ' + str(output))
    print(f'Compared {len(files)} managed PLC sources; differences={differences}. Extra actual files are untouched.')
    return 1 if differences else 0

if __name__ == '__main__': sys.exit(main())
