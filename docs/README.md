# switch 项目文档

仓库包含四轴造波上位机和配套 TwinCAT PLC 源码。本页按工作任务导航。PLC 连接配置和在线程序以现场工程及实际运行目录为准。

## 先按要做的事查

| 任务 | 文档 |
| --- | --- |
| 第一次接手，了解模块和数据怎样流动 | [项目架构与源码入口](整个项目代码讲解.md) |
| 找上位机或 PLC 断点，检查一条命令或一次反馈 | [上位机功能分工](上位机功能分工.md) · [调试入口](CodeSimplification.md) · [Axis 服务目录](../Axis/README.md) |
| 连接一台或多台上位机，排查连接和超时 | [ADS 连接与排查](AdsConnectionGuide.md) |
| 对接、编译和验收 PLC | [TwinCAT 对接](TwinCAT_NB_Integration.md) · [监控结构和字节布局](PlcMonitoringContract.md) |
| 理解站点身份、申请、交接和失联 | [三站控制权](ControlAuthorityRules.md) |
| 操作造波文件、发送、开始或停止 | [造波页操作](WaveOperation.md) |
| 查轴参数、错误码或实测超限 | [运行诊断](RuntimeDiagnostics.md) · [运行实测统计](AxisRuntimeMonitoring.md) |
| 管理登录账号、配置 SQL Server | [登录与账号权限](登录与账号权限.md) · [数据库保存与补传](DatabasePersistence.md) |
| 调整页面布局、控件或主题 | [页面布局及验证](PageLayout.md) · [AntdUI 页面改造](AntdUiMigration.md) |
| 讨论摇板模型、丝杆换算和生成算法 | [造波计算记录](WaveGenerationNotes.md) |

## 从源码到现场

在仓库根目录可运行以下构建和离线检查。上位机引用文件见 [lib 说明](../lib/README.md)。

```powershell
dotnet build 'Page switching.csproj' -c Release
python -m unittest discover -s PLC/TwinCAT_NB/tests -p 'verify_*.py' -v
python tests/verify_ads_connection.py
python tests/verify_ads_monitor.py
```

PLC 离线检查覆盖仓库结构、扫描分支和接口契约。现场验收应在 XAE 核对实际工程、PLC 在线版本、轴配置与现场接线，再按部署方式验证 ADS、反馈和手动操作。虚拟轴的调试许可和参数只用于相应测试。[TwinCAT 对接](TwinCAT_NB_Integration.md) · [虚拟轴验收记录](OptimizationAcceptance.md)

## 设备资料

- [设备简介与设计指标原图](assets/wavemaker/equipment-specification.png)
- [主要部件布局原图](assets/wavemaker/drive-structure.png)

设计指标、理论示例与待确认的铰链、传动几何和 NC 轴单位分别记录在 [造波计算记录](WaveGenerationNotes.md)。

## 历史改动与验证记录

历史记录的数值和结论对应当时的源码及验证环境。当前配置、操作方法和接口查上面的专题文档。

| 记录 | 内容 |
| --- | --- |
| [2026-11-13 ADS 通讯代码检查](AdsCommunicationReview.md) | 首次连接与监控恢复修复，运行期限制 |
| [优化交付与虚拟轴验收](OptimizationAcceptance.md) | 固定点位、性能基线和验收步骤 |
| [第五项代码整理](Step5CodeMap.md) | 轨迹 Action、调用图和旧变量核查 |
| [旧工程功能覆盖](旧工程功能覆盖.md) | 已迁移的功能和当时的缺口 |
| [2026-09-30 程序检查](ProgramReview-20260930.md) | 当时的检查项和处理记录 |
| [AntdUI 与 ScottPlot 试用](UiTrial.md) | 初期界面和绘图验证 |
