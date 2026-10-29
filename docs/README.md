# 项目文档导航

更新日期：2026-10-24。按用途归类现有文档，文件名和设备图片路径保持不变。

## 建议阅读顺序

1. 接手项目：先读[整个项目代码讲解](整个项目代码讲解.md)，再读[代码阅读与调试入口](CodeSimplification.md)。
2. 操作造波页：先读[造波页操作与通信](WaveOperation.md)，再核对[三站控制权与运行锁](ControlAuthorityRules.md)。
3. 连接或部署 PLC：依次读 [ADS 连接](AdsConnectionGuide.md)、[TwinCAT 对接](TwinCAT_NB_Integration.md)和[PLC 只读监控接口](PlcMonitoringContract.md)。
4. 调整造波计算或机械转换：先读[造波计算讨论记录](WaveGenerationNotes.md)，区分设备资料、理论示例及待确认参数。
5. 本轮交付及在线验收：见[优化交付与虚拟轴验收](OptimizationAcceptance.md)，包含版本记录、固定点位、逐项验收表和性能基线。
6. 查找本轮精简后的代码：见[第五项整理记录](Step5CodeMap.md)，包含轨迹 Action、上位机四条入口和变量核查结果。

## 开发与界面

| 文档 | 内容与用途 |
| --- | --- |
| [整个项目代码讲解](整个项目代码讲解.md) | 项目架构、业务调用链、源码索引与维护边界 |
| [代码阅读与调试入口](CodeSimplification.md) | 写法约定、四轴对象、共享通信服务与调试入口 |
| [AntdUI 全页面迁移](AntdUiMigration.md) | 全页面控件迁移范围、数据行为与验证 |
| [页面布局与验证](PageLayout.md) | 导航、手动、控制权、诊断和监测配置布局；多尺寸与缩放检查 |

## 操作、通信与设备

| 文档 | 内容与用途 |
| --- | --- |
| [造波页操作与通信](WaveOperation.md) | 导入、发送、开始、停止，四轴共用最终点位及 2 ms 周期 |
| [三站控制权与运行锁](ControlAuthorityRules.md) | 站点优先级、申请与应答、运行期间禁止交接 |
| [运行诊断与轴参数](RuntimeDiagnostics.md) | 首次故障、只读参数、复制诊断、配套版本和源码核对 |
| [运行实测超限统计](AxisRuntimeMonitoring.md) | 独立100 ms只读采样、四轴位置/速度阈值、去重占比及CSV/JSON报告 |
| [PLC 只读监控接口](PlcMonitoringContract.md) | 版本 4 / 164 字节反馈布局、数据质量、异常与部署验证 |
| [TwinCAT 对接](TwinCAT_NB_Integration.md) | 配套 PLC 源码、手动映射、轨迹下发与接口演进 |
| [ADS 连接](AdsConnectionGuide.md) | 73 / 113 两台电脑的 Router 配置、回程路由与超时排查 |
| [造波计算讨论记录](WaveGenerationNotes.md) | 摇板设备资料、计算思路、机械转换边界与待确认参数 |

## 历史与阶段记录

以下文档保留当时的改动与验证背景，其中的“未接入”“待确认”等描述需结合后续专题文档阅读。

| 文档 | 记录范围 | 后续阅读 |
| --- | --- | --- |
| [旧工程功能覆盖](旧工程功能覆盖.md) | 旧功能迁移、计算程序、文件格式、单位及阶段性缺口 | [造波操作](WaveOperation.md)、[控制权](ControlAuthorityRules.md)、[监控接口](PlcMonitoringContract.md) |
| [程序检查记录（2026-09-30）](ProgramReview-20260930.md) | 当次缺陷修复、检查结果与待验收事项 | 按问题查看对应专题文档及当前源码 |
| [AntdUI 与 ScottPlot 试用记录](UiTrial.md) | 初期控件试用、图表行为与 Designer 检查 | [全页面迁移](AntdUiMigration.md) |

## 设备原始资料

- [设备简介与设计指标](assets/wavemaker/equipment-specification.png)
- [主要部件布局](assets/wavemaker/drive-structure.png)

图片的解释和使用边界见[造波计算讨论记录](WaveGenerationNotes.md)。设计指标不能当作现场实测结果，理论示例不能直接用作 PLC 点位参数。

## 阅读与维护约定

- 操作流程以 `WaveOperation.md` 为入口，控制权规则查 `ControlAuthorityRules.md`，反馈布局查 `PlcMonitoringContract.md`。
- `TwinCAT_NB_Integration.md` 包含接口演进过程，部署时需一并阅读文末的整块监控和造波操作更新。
- 文档描述不代表现场 PLC 已部署或设备已验收；实际行为需核对当前源码、配置及 PLC 版本。
- 修改接口时同步更新对应专题文档；新增文档时补充本索引。历史检查结果保留原日期和验证边界。
