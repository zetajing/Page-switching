"""只比较仓库受管 PLC 源码；不复制、不删除、不覆盖实际工程的额外文件。"""
from pathlib import Path
import argparse
import difflib
import re
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
    args = parser.parse_args()
    actual = args.actual.resolve()
    if not (actual / 'axis_4/axis_4.plcproj').is_file():
        parser.error('实际目录下没有 axis_4/axis_4.plcproj')
    project = ET.parse(ROOT / 'axis_4/axis_4.plcproj')
    files = [node.get('Include').replace('\\', '/') for node in project.findall('.//{*}Compile')]
    if args.only:
        for name in args.only:
            if name not in files: parser.error('不属于仓库受管源码：' + name)
        files = args.only
    differences = 0
    for name in files:
        local = ROOT / 'axis_4' / name
        target = actual / 'axis_4' / name
        if not target.is_file():
            print('MISSING ' + name)
            differences += 1
            continue
        left, right = code(local), code(target)
        # 按词比较忽略代码排版，字符串和整行注释保留，防止把注释边界当成空白忽略。
        tokens = lambda text: re.findall(r"//[^\n]*|\(\*.*?\*\)|'(?:''|[^'])*'|\w+|:=|<>|<=|>=|[^\s]", text, re.S)
        if tokens(left) != tokens(right):
            differences += 1
            print('DIFFERENT ' + name)
            print('\n'.join(difflib.unified_diff(left.splitlines(), right.splitlines(), fromfile='repository', tofile='actual', n=2)))
    print(f'Compared {len(files)} managed PLC sources; differences={differences}. Extra actual files are untouched.')
    return 1 if differences else 0

if __name__ == '__main__': sys.exit(main())
