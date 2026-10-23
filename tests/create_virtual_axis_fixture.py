"""生成固定虚拟轴流程验收文件；仅含 PLC 原始点位，不代表水面高程或机械角度。"""
from pathlib import Path
import argparse
import hashlib
import json


def create_fixture(output):
    # 20.006 秒内 0→1→0→-1→0，首尾各保留两点静止。仅供已确认的虚拟轴使用。
    points = [0.0, 0.0]
    for start, end in [(0, 1), (1, 0), (0, -1), (-1, 0)]:
        points.extend(start + (end - start) * step / 2500 for step in range(1, 2501))
    points.extend([0.0, 0.0])
    metadata = ['VirtualAxisOnly_NoMechanicalConversion', str(len(points)), '0.002', '1', '1', '1', '0', '0', '0']
    output = output.resolve()
    output.parent.mkdir(parents=True, exist_ok=True)
    output.write_text('\n'.join(metadata + [format(point, '.17g') for point in points]) + '\n', encoding='utf-8', newline='\n')
    record = {'file': output.name, 'sha256': hashlib.sha256(output.read_bytes()).hexdigest(),
              'points': len(points), 'sample_time_seconds': 0.002, 'duration_seconds': (len(points) - 1) * 0.002,
              'min_position_raw': min(points), 'max_position_raw': max(points),
              'scope': 'Virtual axes only. No ADS writes. No mechanical conversion or real-machine validation.'}
    output.with_suffix('.json').write_text(json.dumps(record, ensure_ascii=False, indent=2) + '\n', encoding='utf-8')
    return record


if __name__ == '__main__':
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--output', type=Path, default=Path(__file__).resolve().parents[1] / 'artifacts/roadmap-verification/virtual-axis-smoke.csv')
    args = parser.parse_args()
    print(json.dumps(create_fixture(args.output), ensure_ascii=False, indent=2))
