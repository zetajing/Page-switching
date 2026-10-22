"""按 MAIN 实际调用顺序展开 Action，供已有联锁检查读取真实执行代码。"""

import re
import xml.etree.ElementTree as ET


def main_source(root):
    pou = ET.parse(root / 'axis_4/POUs/MAIN.TcPOU').find('POU')
    actions = {action.get('Name'): action.findtext('./Implementation/ST')
               for action in pou.findall('Action')}
    source = pou.findtext('./Implementation/ST')

    # 只展开独占一行的本地 Action 调用，保留调用前后的原有语句顺序。
    def expand(match):
        name = match.group(1)
        if name not in actions:
            raise ValueError('MAIN 引用了不存在的 Action：' + name)
        return actions[name]

    return re.sub(r'^\s*(A\d\d_\w+)\(\);[ \t]*$', expand, source, flags=re.M)
