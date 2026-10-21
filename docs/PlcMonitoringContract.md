# PLC 只读监控接口

[返回文档导航](README.md)

2026-10-15 起，监控改为整块读取 `GVL_HMI.stMonitor`，替代原来的 33 次逐字段读取。该结构只包含反馈，不包含任何控制命令；三站命令、控制权与轨迹接口不受影响。手动页面也通过现有 DLL 整体读取同一个结构体。

## 通信布局

PLC `ST_MachineMonitor` 和 C# `PlcMachineMonitor` 均明确采用 1 字节对齐。版本 3 固定 148 字节，按以下顺序排列：

| 字段 | PLC 类型 | 字节偏移 |
| --- | --- | --- |
| nVersion | UINT，固定 3 | 0 |
| nByteSize | UINT，SIZEOF(stMonitor)，固定 148 | 2 |
| nControlOwner | UINT | 4 |
| nControlMode | UINT | 6 |
| nWaveState | UINT | 8 |
| uiWaveFaultCode | UINT | 10 |
| udiHeartbeat | UDINT | 12 |
| Axis1 / Axis2 / Axis3 / Axis4 | ST_AxisMonitor，各 22 字节 | 16 / 38 / 60 / 82 |

每轴依次为：`lrActualPosition`、`lrSpeed`（各 LREAL / double，8 字节），`xHomed`、`xError`、`xPositiveLimit`、`xNegativeLimit`、`xOriginSignal`、`xEnabled`（各 BOOL，1 字节）。C# BOOL 显式使用 `MarshalAs(UnmanagedType.I1)`，禁止依赖默认 BOOL 尺寸。修改结构必须同步两端并升级版本，不能直接复用轴内部的混合命令结构。

控制端编码 0 未分配、1 造波控制室、2 拖车本地、3 远程操作室；模式 0 未选择、1 手动、2 自动；轨迹显示状态 0 待机、1 准备、2 运行、4 停止中、6 完成、7 故障。未知值保留数值提示。故障码保持 UINT，完整源故障超出 UINT 时显示 65535，完整码仍保留在GVL_HMI.udiDetailedFaultCode。轴方向及机械单位需现场确认，不在监控中做角度或丝杆转换。

尾部追加 44 字节轨迹及准备反馈，版本 3 的完整字段顺序见 [造波操作](WaveOperation.md)。包括接收/拼接/执行计数和序号、所属站、完整错误、准备条件及停车完成标志。

## 发布和读取

`FB_HmiFeedback` 在 MAIN 四个基础轴块之后，直接填充 stMonitor 并使心跳每约 500 ms 递增；待机和故障时心跳也持续运行。旧的五项独立监控变量及 GVL_pc 兼容层已删除；控制端、模式、状态、故障码和心跳直接写入 stMonitor。完整故障码和轨迹诊断仍保留在 GVL_HMI，手动页面不再单独读取 GVL_axis.Axis1..4。

App.config 用 `AdsMonitorSnapshot=GVL_HMI.stMonitor` 指定整块地址，原有五个 AdsMonitor 字段地址配置已移除。四轴 32 项成员反馈地址已移除；命令地址仍保留。监控和手动服务统一使用 `InduLink.Protocols.Ads.AdsClient` 及 DLL 的 `ReadAnyAsync<PlcMachineMonitor>`；句柄创建、复用和释放由 DLL 负责。监控连接时由 DLL 探测 PLC 状态，原有心跳、过期、整轮超时和错误显示继续由监控服务处理。两个服务保留独立连接，各自按需读取，不共用缓存，保持现有控制、轨迹上传和心跳调度。页面隐藏时仍读取这个 148 字节结构，只不生成四轴显示快照。500 ms 调度周期保持现状，不排队积累刷新任务。

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

离线运行 `python -m unittest discover -s PLC/TwinCAT_NB/tests -p "verify_*.py" -v`。新增检查验证 PLC 字段顺序、22/148 字节封送、四轴偏移、UINT 高位、BOOL、布局拒绝和数值失效。版本 1 / 100 字节已不兼容，两端必须一起更新到版本 3。仍需现场确认 ADS SDK 和 DLL 对实际 PLC 的整块读取，以及断线、心跳停滞和恢复。

性能验收在同一 PLC、网络和调度周期下进行：先预热句柄，再分别采集旧 33 字段和新结构体读取耗时，比较平均值、P95 和失败数。新结构体未部署前无法测得实际优化比例，不报告推测的倍数。

2026-10-15 本次只读检查 `192.168.0.55.1.1:851`：新符号返回 DeviceSymbolNotFound，实际服务反馈为 Unavailable / IsLive=false，控制端和心跳均不可用。未写入 PLC、未下载，因此本次未测得新结构体的实际读取耗时。
