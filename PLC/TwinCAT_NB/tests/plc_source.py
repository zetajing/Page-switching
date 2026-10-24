"""按实际调用位置展开本地 Action，供联锁检查读取真实执行代码。"""

import re
import xml.etree.ElementTree as ET


def pou_source(path):
    pou = ET.parse(path).find('POU')
    actions = {action.get('Name'): action.findtext('./Implementation/ST')
               for action in pou.findall('Action')}
    source = pou.findtext('./Implementation/ST')

    def expand_action(name, chain):
        if name not in actions: raise ValueError('引用了不存在的 Action：' + name)
        if name in chain: raise ValueError('Action 存在循环调用：' + name)
        return expand(actions[name], chain + [name])

    def expand(text, chain):
        # 阶段分组只分派一个 Action；其内部 CASE 必须与外部状态集合完全对应。
        # 展开后还原成单一 CASE，不能把几个阶段 Action 顺序调用当成等价代码。
        def stage(match):
            body = expand_action(match.group(2), chain)
            nested = re.search(r'\bCASE\s+nState\s+OF\b(.*)\bEND_CASE\b\s*;?\s*$', body, re.S)
            if nested is None: raise ValueError('阶段 Action 必须只包含 nState CASE：' + match.group(2))
            labels = re.findall(r'^[ \t]*(\d+(?:[ \t]*,[ \t]*\d+)*)[ \t]*:', nested.group(1), re.M)
            actual = [int(n) for label in labels for n in label.split(',')]
            expected = [int(n) for n in match.group(1).split(',')]
            if len(actual) != len(set(actual)) or set(actual) != set(expected):
                raise ValueError('阶段分派与内部 CASE 不对应：' + match.group(2))
            return nested.group(1)

        text = re.sub(r'^[ \t]*(\d+(?:[ \t]*,[ \t]*\d+)*)[ \t]*:[ \t]*(A\d\d_\w+)\(\);[ \t]*$', stage, text, flags=re.M)
        return re.sub(r'^[ \t]*(A\d\d_\w+)\(\);[ \t]*$',
                      lambda match: expand_action(match.group(1), chain), text, flags=re.M)

    return expand(source, [])


def main_source(root):
    return pou_source(root / 'axis_4/POUs/MAIN.TcPOU')
