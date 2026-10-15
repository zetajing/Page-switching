# PLC 只读监控接口

2026-10-15 起，监控改为整块读取 `GVL_HMI.stMonitor`，替代原来的 33 次逐字段读取。该结构只包含反馈，不包含任何控制命令；三站命令、控制权与轨迹接口不受影响。手动页面仍使用原有批量轴反馈读取路径。

## 通信布局

PLC `ST_MachineMonitor` 和 C# `PlcMachineMonitor` 均明确采用 1 字节对齐。版本 1 固定 100 字节，按以下顺序排列：

| 字段 | PLC 类型 | 字节偏移 |
| --- | --- | --- |
| nVersion | UINT，固定 1 | 0 |
| nByteSize | UINT，SIZEOF(stMonitor)，固定 100 | 2 |
| nControlOwner | UINT | 4 |
| nControlMode | UINT | 6 |
| nWaveState | UINT | 8 |
| uiWaveFaultCode | UINT | 10 |
| udiHeartbeat | UDINT | 12 |
| Axis1 / Axis2 / Axis3 / Axis4 | ST_AxisMonitor，各 21 字节 | 16 / 37 / 58 / 79 |

每轴依次为：`lrActualPosition`、`lrSpeed`（各 LREAL / double，8 字节），`xHomed`、`xError`、`xPositiveLimit`、`xNegativeLimit`、`xOriginSignal`（各 BOOL，1 字节）。C# BOOL 显式使用 `MarshalAs(UnmanagedType.I1)`，禁止依赖默认 BOOL 尺寸。修改结构必须同步两端并升级版本，不能直接复用轴内部的混合命令结构。

控制端编码 0 未分配、1 造波控制室、2 拖车本地、3 远程操作室；模式 0 未选择、1 手动、2 自动；轨迹显示状态 0 待机、1 准备、2 运行、4 停止中、6 完成、7 故障。未知值保留数值提示。故障码保持 UINT，完整源故障超出 UINT 时显示 65535，完整码仍保留在旧诊断符号。轴方向及机械单位需现场确认，不在监控中做角度或丝杆转换。

## 发布和读取

`FB_HmiFeedback` 在 MAIN 四个基础轴块之后，更新旧 GVL_HMI 状态及每约 500 ms 递增的心跳，再填充 stMonitor；待机和故障时心跳也持续运行。原有单字段反馈符号保留用于诊断及手动页面。

App.config 用 `AdsMonitorSnapshot=GVL_HMI.stMonitor` 指定整块地址，原有五个 AdsMonitor 字段地址配置已移除。四轴成员地址仍供手动页面使用。监控使用独立只读 TwinCAT ADS 客户端，连接目标来自现有配置，首次建立句柄后复用；后续每轮只执行一次结构体读取。页面隐藏时仍读取这个 100 字节结构，只不生成四轴显示快照。500 ms 调度周期保持现状，不排队积累刷新任务。

一块读取减少请求往返和字段采样间隔，不承诺跨任务原子性；如将来需要严格同一扫描快照，需另外实现发布缓冲/一致性协议。它不改变 PLC 2 ms 轨迹取点周期。

## 质量和异常

- 整块读取失败时，全部本轮反馈无效，页面显示 `--` 或不可用，不显示默认零/false；不自动回退到逐字段读。
- 布局版本/字节数不符时明确提示更新配套 PLC，不连续重连。未部署符号、类型和 Marshalling 错误同样按协议错误显示。
- 已下载/在线修改导致句柄失效时丢弃句柄，下轮重新获取；网络错误沿用重连退避，恢复后重新确认心跳。
- 单个位置/速度为 NaN 或无穷时，该数值不可用，其余字段保留有效质量。
- 首次有效读取需观察心跳变化后确认全局状态；3 秒不变化显示心跳停止，3 秒没有有效帧显示过期。历史数据不能视为当前有效状态。
- 每次 ADS 请求超时默认 1000 ms，整轮默认 3000 ms，由 AdsMonitorRefreshTimeoutMs 配置；连接默认 10000 ms。超时日志保留具体读取步骤。

## 部署及验证

先在设备停稳时用 XAE 编译、下载配套 PLC，再重启更新后的上位机。当前在线 PLC 未更新时会显示结构体符号不存在，不能将编译成功当作已部署。

离线运行 `python -m unittest discover -s PLC/TwinCAT_NB/tests -p "verify_*.py" -v`。新增检查验证 PLC 字段顺序、21/100 字节封送、四轴偏移、UINT 高位、BOOL、布局拒绝和数值失效。仍需现场确认 ADS SDK 对实际 PLC 的整块读取，以及断线、心跳停滞和恢复。

性能验收在同一 PLC、网络和调度周期下进行：先预热句柄，再分别采集旧 33 字段和新结构体读取耗时，比较平均值、P95 和失败数。新结构体未部署前无法测得实际优化比例，不报告推测的倍数。

2026-10-15 本次只读检查 `192.168.0.55.1.1:851`：新符号返回 DeviceSymbolNotFound，实际服务反馈为 Unavailable / IsLive=false，控制端和心跳均不可用。未写入 PLC、未下载，因此本次未测得新结构体的实际读取耗时。

以下为旧接口历史验证记录，不能当作新结构体已在线验证的证据。

## 上位机交付验证（2026-10-05）

- Debug、Release 编译均为 0 警告、0 错误。
- 使用实际监控和页面源码、模拟 transport 的离线检查共 59 组、376 条断言通过，覆盖全部枚举、未知值、部分字段失效、首帧心跳等待、停滞、回绕、重启、断线恢复、读取超时、不重入、关闭及日志去重。主窗体测试使用手动服务和 Router 替身，不显示真实主程序，也不连接 PLC。
- 布局检查共 9 组、1185 条断言通过：在 96/144 DPI 下检查 900×650、1104×606、1600×900 的逻辑尺寸，并检查 150% 下小物理窗口的滚动、放大恢复和页面重新挂载。四轴表固定列、行高及表头从 Designer 逻辑尺寸按 DPI 缩放，实际测量表头和单元格文字，避免文字省略。
- 桌面工程的 Visual Studio 自动页 Designer 成功加载，三个状态卡片、四轴表格和消息区保持可编辑。模拟结果和编译成功不代表五个新符号已经部署到 PLC。

## 现场读取修复（2026-10-08）

- PLC 在线变量表确认前三个状态及 `udiWaveFaultCode` 为 UINT，只有 `udiHeartbeat` 为 UDINT。故障码沿用现场符号名，但按实际 2 字节类型读取，不能根据 `udi` 前缀判断为 UDINT。
- 对 `192.168.0.55.1.1:851` 只读验证：错误地把故障码读为 UInt32 时，Sum Command 路径导致五项一起报告 Marshalling 大小不匹配；逐项读取时仅故障码失败。按 UInt16 读取故障码后，两种路径均能读取全部五个变量。
- 修改后的实际监控服务连续读取五轮，共 33 项均返回正常质量；首帧等待心跳变化，后续状态正常，心跳持续递增。每轮读取耗时约 35–166 ms，低于现有 1000 ms 预算。
- 验证时控制端、模式、造波状态分别为 `1、1、1`，故障码为 `0`，四轴位置和速度均为 `0`。实际自动页控件确认显示“上位机、手动、准备中、故障码：0”和完整轴反馈。此记录只确认读取与显示，不代表四轴物理含义或自动造波控制已经验证。
- 离线回归与实际页面检查共 32 条断言通过，覆盖 UINT 故障码上界、UDINT 心跳范围、首帧等待、单字段读取失败/类型错误隔离、Marshalling 错误不触发重连及读取恢复。Release 编译为 0 警告、0 错误。

## 读取代码简化与断点位置（2026-10-08）

- 主窗体定时器直接等待 `RefreshMonitorAsync`，读取完成后在 UI 线程更新自动页；不再套用 `Task.Run` 和界面回调。关闭窗体只等待实际读取任务，避免等待已经停止处理消息的 UI。
- `MachineMonitorService` 移除 transport 接口、工厂、请求列表和批量结果解析。`ReadSnapshotAsync` 逐行声明每个符号的读取类型，`ReadPlcAsync<T>` 直接调用 TwinCAT SDK；实际读取位于本项目源码，便于打断点。
- 在 `ReadPlcAsync<T>` 的 `ReadAnyAsync<T>` 调用及下一行打断点，观察 `symbol`、`typeof(T)`、`result.Value`、`result.ErrorCode`。只查看某个变量时，可设置条件 `symbol == "GVL_HMI.nControlOwner"`。
- Debug 编译关闭优化，并生成 portable PDB；已核对 DLL/PDB 标识、当前源码校验值和 PLC 读取行的断点映射。调试应从 Visual Studio 启动最新 Debug 构建。
- 窗体页面统一释放一次，保留超时、心跳检查、失败重连、单字段错误隔离和关闭取消。现场 33 项连续读取正常，验证时造波状态为 `2`，心跳递增；关闭期间无需 UI 回调也能完成清理。
- 离线、现场读取、页面显示和三种页面的关闭检查共 28 条断言通过。Debug 与 Release 编译均为 0 警告、0 错误；读取行的 DLL/PDB/源码一致性检查通过。

## TwinCAT_NB 配套实现

配套 PLC 新增 `GVL_HMI` 与 `FB_HmiFeedback`，在 MAIN 四个基础轴块调用之后发布本扫描反馈。手动控制默认使用 `GVL_axis.Axis1..Axis4`；使能反馈为 `xEnabled=MC_Power.Status`，运动反馈 `xMotorRun` 不再被当作使能。NC 正向点动为 `jogz`，负向为 `jogf`。详细对照和部署步骤见 [TwinCAT_NB 对接说明](TwinCAT_NB_Integration.md)。

PLC 心跳由约 500 ms 的自复位 TON 事件递增，不依赖上位机写入。`nControlOwner`、`nControlMode` 来自 `FB_ControlAuthority` 经 `GVL_pc` 发布的真实结果；运行中不交接，不以页面或 ADS 连接推断授权。

本实现不发布暂停状态 3，也不虚构已停止状态 5。轨迹内部 40/50/60/70 显示停止中 4，退出后若仍有组错误则显示故障 7。组状态 30 显示运行 2，5/10/15/20 显示准备 1，正常完成显示 6，其余无错误时待机 0。只读状态适配不会启动自动造波。

`udiWaveFaultCode` 仍为 UINT。源错误码大于 65535 时，兼容字段显示 65535，完整值保存在新增 UDINT `GVL_HMI.udiDetailedFaultCode`；`nFaultAxis` 表示首个故障轴。已生成同名新源码并不表示现场 PLC 已部署或通过在线验证。
