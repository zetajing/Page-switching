# PLC 只读监控接口

[返回文档导航](README.md) · [ADS 配置与排查](AdsConnectionGuide.md) · [运行诊断与轴参数](RuntimeDiagnostics.md)

本文说明 `GVL_HMI.stMonitor` 的发布、读取、布局和数据质量，按 2026-11-20 源码核对。该结构是 PLC 发布给上位机的反馈接口，不用于回写控制命令。连接和路由问题先查[ADS 连接配置](AdsConnectionGuide.md)。

## 1. 确认配套版本

| 项目 | 当前约定 |
| --- | --- |
| 配置键与默认符号 | `AdsMonitorSnapshot=GVL_HMI.stMonitor` |
| PLC / C# 类型 | `ST_MachineMonitor` / `PlcMachineMonitor` |
| 对齐 | PLC `pack_mode=1`，C# `StructLayout(Pack=1)` |
| 布局版本 / 总长 | `4 / 164` 字节 |
| 每轴结构 | `ST_AxisMonitor` / `PlcAxisMonitor`，22 字节 |
| PLC 发布块中的程序版本 | `2026102401`，在线值取决于实际下载程序 |
| 接口行为版本 | `1`，现有点位下发和启动另行校验 |

`AdsMonitorSnapshot` 保存在实际运行配置 `Page switching.dll.config`；项目 `App.config` 是生成该配置的源文件。旧的五个独立机器监控地址和四轴逐成员反馈地址已移除，命令地址仍单独保留。

监控和手动反馈检查布局版本及字节数，不符合就拒绝解释。现有点位下发和启动另外要求行为版本 1；不匹配时不能通过清表、下发或启动检查，既有停车退出流程不因此改道。版本号说明接口约定，不能证明现场已下载，也不能证明其他 ADS 客户端执行了上位机预检。

## 2. PLC 发布与上位机读取

`MAIN` 在四个基础轴块执行后调用 `FB_HmiFeedback`，从本扫描的轴状态和全局反馈直接填充 `stMonitor`。反馈心跳约每 500 ms 加一，`UDINT` 回绕到 0；待机和故障时也继续更新，上位机只读。

两条读取路径使用同一结构定义，但连接、缓存和质量处理各自独立：

| 路径 | 读取与用途 |
| --- | --- |
| `MachineMonitorService.RefreshAsync` | 直接调用 DLL 的 `ReadAnyAsync<PlcMachineMonitor>`，将整帧转换成监控快照；负责重连、心跳和过期质量 |
| `AxisService.ReadSnapshotAsync` | 通过手动客户端读取同一帧，校验布局后更新四轴手动反馈；目前不执行独立监控服务的心跳新鲜度判定 |

主窗体每 500 ms 尝试刷新独立监控，读取未完成时跳过、不排队。页面隐藏后仍读取完整的 164 字节，只在不需要轴表格时省略四轴显示快照。手动反馈按手动页面自己的刷新设置读取，不能把两者当成同一缓存。

两者都使用 `InduLink.Protocols.Ads.AdsClient`，句柄创建、复用和释放由 DLL 管理。整帧失败时不回退到逐字段读。2026-11-20 的整理已将监控读取集中到 `RefreshAsync`，不再经过错误列表和整帧 `FeedbackField` 包装层。

整块读取减少请求往返和字段之间的采样间隔，但没有声明跨任务原子性。严格同一扫描一致性需要另行设计发布缓冲或序号校验；现有接口不能据此作出保证，也没有改变 PLC 轨迹周期。

## 3. 判断质量、心跳和错误

以下质量规则属于独立 `MachineMonitorService`：

| 情况 | 对外结果 |
| --- | --- |
| 首次连接或重建连接后的第一帧 | 建立心跳基线，等待变化；机器状态和四轴反馈不可用 |
| 布局有效且已确认心跳变化 | 正常反馈，按有效字段显示状态 |
| 单个位置或速度为 NaN / 无穷 | 该数值不可用，其余字段保留；包含轴列表时质量为“部分反馈不可用” |
| 持续读到帧，但心跳 3 秒不变 | 心跳停滞，机器和四轴字段一同失效；保留心跳计数用于显示原因 |
| 3 秒没有有效帧 | 反馈过期，整帧不可用 |
| 断线或整帧读取失败 | 本轮数据不可用，不把缺失值显示成零或把缺失 BOOL 当作 false |

可用性判断使用单调计时，界面另保留最近成功读取和心跳变化时间。`IsConnected` 表示客户端连接状态；`IsLive` 还要求心跳有效、质量为正常或部分可用。即使继续成功读到 PLC 内存，也不能把冻结值当作实时反馈。

错误处理按以下类别进行：

- 布局不符、普通符号不存在、数据尺寸/类型或封送错误：显示协议问题，不靠反复重连解决。
- `ReadAny` 已缓存句柄找不到：下轮重建客户端，重新获取句柄。
- `DeviceSymbolVersionInvalid` 或通信错误：记录失败，等待完整重连间隔后重建；每次实际失败重新计时。
- 调用方取消或程序退出：取消读取并清理，不记成普通通信故障；超时则记录上限和当前“连接”或“读取变量”步骤。

独立监控恢复后重新确认心跳，**不会替手动客户端恢复连接或控制权**。手动客户端只有首次连接重试，运行中断线和旧句柄恢复仍未统一处理，见[连接恢复范围](AdsConnectionGuide.md#6-明确连接恢复的范围)。

| 时间参数 | 默认值 | 来源 |
| --- | ---: | --- |
| 连接超时 | 10000 ms | 复用 `AdsConnectTimeoutMs` |
| 监控单次 ADS 请求超时 | 1000 ms | `MachineMonitorOptions.OperationTimeoutMilliseconds` 默认值 |
| 整轮监控读取超时 | 3000 ms | `AdsMonitorRefreshTimeoutMs`，无效值回到默认值 |
| 反馈过期 / 心跳停滞 | 3000 ms | `MachineMonitorOptions.StaleAfterMilliseconds` 默认值 |
| 通信失败后的重连等待 | 3000 ms | `MachineMonitorOptions.ReconnectIntervalMilliseconds` 默认值 |

`AdsOperationTimeoutMs` 默认 5000 ms 属于手动客户端，当前配置加载方法不会把它复制给监控客户端。

## 4. 核对字段布局

偏移从结构起点计，单位为字节。PLC 字段顺序必须与 [C# 结构](../Axis/Feedback/PlcMachineMonitor.cs)一致；修改时同步两端并升级布局版本。BOOL 明确按 1 字节封送，不能依赖 C# 默认 BOOL 大小，也不能直接改用含命令的轴内部结构。

### 机器与四轴反馈：0–103

| PLC 字段 | 类型 | 偏移 |
| --- | --- | ---: |
| `nVersion` | UINT | 0 |
| `nByteSize` | UINT | 2 |
| `nControlOwner` | UINT | 4 |
| `nControlMode` | UINT | 6 |
| `nWaveState` | UINT | 8 |
| `uiWaveFaultCode` | UINT | 10 |
| `udiHeartbeat` | UDINT | 12 |
| `Axis1` / `Axis2` / `Axis3` / `Axis4` | 4 × ST_AxisMonitor | 16 / 38 / 60 / 82 |

每轴内部依次为：`lrActualPosition`（偏移 0，LREAL）、`lrSpeed`（偏移 8，LREAL），然后是 `xHomed`、`xError`、`xPositiveLimit`、`xNegativeLimit`、`xOriginSignal`、`xEnabled`（偏移 16–21，各 BOOL）。`xEnabled` 是实际使能反馈，不是请求。

控制端编码：`0` 未分配、`1` 造波控制室、`2` 拖车本地、`3` 远程操作室。模式：`0` 未分配、`1` 手动、`2` 轨迹占用。页面状态：`0` 待机、`1` 准备、`2` 运行、`4` 停止中、`5` 已停止、`6` 完成、`7` 故障；它与内部轨迹状态不同，未知值保留数值提示。

`uiWaveFaultCode` 为 UINT，完整源故障超过 65535 时显示 65535，完整值另在 `GVL_HMI.udiDetailedFaultCode`。轴位置和速度保留 PLC/NC 原始单位；方向、丝杆几何和机械换算尚未确认，不在监控接口中擅自转换为角度或水面高程。

### 轨迹、准备及停车反馈：104–147

| PLC 字段 | 类型 | 偏移 |
| --- | --- | ---: |
| `nTrajectoryState` | UINT | 104 |
| `nReceivedCount` / `nLoadedCount` / `nPointCount` / `nCurrentPoint` | 4 × UDINT | 106 / 110 / 114 / 118 |
| `udiReceivedSequence` / `udiAcceptedSequence` | 2 × UDINT | 122 / 126 |
| `udiErrorId` | UDINT | 130 |
| `nUploadStation` | UINT | 134 |
| `xUploadBusy` / `xDataReady` / `xTrajectoryBusy` | 3 × BOOL | 136 / 137 / 138 |
| `xRunning` / `xDone` / `xError` / `xStopped` | 4 × BOOL | 139 / 140 / 141 / 142 |
| `xMotionPermit` / `xConfigValid` | 2 × BOOL | 143 / 144 |
| `xAxesIdle` / `xAxisFault` / `xAxesStopped` | 3 × BOOL | 145 / 146 / 147 |

接收、拼表、执行点数各自独立；`nCurrentPoint` 是从 0 开始的给定点号，不表示机械轴实际到位。`xAxesStopped` 表示实际停车完成，`xAxesIdle` 还要求相关异步块退出，不能互换。业务含义见 [PLC 类型定义](../PLC/TwinCAT_NB/axis_4/DUTs/ST_MachineMonitor.TcDUT)和[现有造波协议](WaveOperation.md)。

### 程序与首次故障诊断：148–163

| PLC 字段 | 类型 | 偏移 |
| --- | --- | ---: |
| `udiProgramVersion` | UDINT | 148 |
| `nBehaviorVersion` | UINT | 152 |
| `nFaultAxis` | UINT | 154 |
| `nFaultSource` | UINT | 156 |
| `nFaultState` | UINT | 158 |
| `udiCleanupErrorId` | UDINT | 160 |

尾部 `nFaultAxis`、`nFaultSource`、`nFaultState` 对应轨迹首次故障；0 号轴可表示组条件，1–4 表示轴号。首次故障保持到有效组复位，停车/退出错误单独放在 `udiCleanupErrorId`。当前阶段看 `nTrajectoryState`，故障发生阶段看 `nFaultState`。不要与 `GVL_HMI.nFaultAxis` 的当前汇总故障来源混淆，详见[运行诊断](RuntimeDiagnostics.md)。

## 5. 部署与验证

1. 核对两端源码、164 字节布局和实际目标。旧版本 `1 / 100`、`3 / 148` 不能直接按版本 4 解释。
2. 由现场流程完成配套 PLC 的 XAE 编译、下载和在线确认，再更新并重启上位机。源文件已修改、C# 编译成功都不代表 PLC 已部署。
3. 先只读检查布局、心跳变化、质量及四轴原始反馈，再按现场条件验证断线、PLC 任务停滞和恢复。本文不要求发送运动命令。

离线检查入口：

```powershell
python PLC/TwinCAT_NB/tests/verify_monitor_frame.py
python tests/verify_ads_monitor.py
```

前者检查 PLC 布局、发布源码和 C# 封送；后者直接链接真实监控服务，以无网络 ADS 替身验证恢复、质量、超时和取消。2026-11-20 监控整理后该服务回归为 35 项断言通过。这些检查不能替代实际 ADS SDK、PLC 在线程序、网络和三机联调验收。

评估整帧性能时，应在同一 PLC、网络和调度条件下预热句柄后采样，比较平均耗时、P95 和失败数。2026-10-15 从旧 33 次逐字段读取迁移时，对 `192.168.0.55.1.1:851` 的只读检查曾返回 `DeviceSymbolNotFound`；当时未下载 PLC、未测得整帧实际耗时。这是有日期的历史结果，不代表今天目标仍缺符号，也不能据此报告当前性能提升倍数。
