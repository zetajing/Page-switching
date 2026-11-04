"""按实际调用位置展开本地 Action，供联锁检查读取真实执行代码。"""

import re
import xml.etree.ElementTree as ET


def resolve_state_constants(pou, source):
    # 用源码声明的数值还原状态引用，让执行器和原有数字基线都能核对状态行为。
    states = dict(re.findall(r'\b(cState\w+)\s*:\s*UINT\s*:=\s*(\d+)\s*;',
                             pou.findtext('Declaration', default='')))
    return re.sub(r'\bcState\w+\b', lambda match: states[match[0]], source)


def expand_axis_checks(path, source):
    # 离线执行器只支持表达式；从实际函数读取校验规则，避免测试维护第二份规则。
    folder = path.parent.parent / 'AxisConfig_轴配置'

    def inline(match):
        name, argument = match.groups()
        code = ET.parse(folder / (name + '.TcPOU')).findtext('.//Implementation/ST')
        code = re.sub(r'//[^\n]*', '', code).strip()
        expression = re.fullmatch(name + r'\s*:=\s*(.*);', code, re.S)[1]
        expression = expand_axis_checks(path, expression)
        return re.sub(r'\bConfig\.', lambda _: argument + '.', expression)

    return re.sub(r'\b(F_Axis(?:Position|Config)Valid)\((Config|aConfig\[i\])\)', inline, source)


def pou_source(path):
    pou = ET.parse(path).find('POU')
    actions = {action.get('Name'): resolve_state_constants(pou, action.findtext('./Implementation/ST'))
               for action in pou.findall('Action')}
    source = resolve_state_constants(pou, pou.findtext('./Implementation/ST'))

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

    return expand_axis_checks(path, expand(source, []))


def main_source(root):
    return pou_source(root / 'axis_4/POUs/MAIN.TcPOU')
