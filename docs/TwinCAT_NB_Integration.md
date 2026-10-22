# switch 与 TwinCAT_NB 手动控制、监控对接

[返回文档导航](README.md)

> 文档范围：本文包含分阶段的接口说明。反馈已改为版本 3 / 148 字节整块读取，造波页操作也已接入，详见文末更新及[PLC 只读监控接口](PlcMonitoringContract.md)、[造波页操作与通信](WaveOperation.md)；前文逐轴反馈映射保留作历史对照。

基础对接范围是四轴手动控制和机器只读监控。轨迹接收协议现已改为“PC 全部下发后，PLC 统一拼接”，详见下文；未接入自动造波页面，也未改变造波计算和机械点位转换。

## 配套源码与版本

- PLC 编辑目录：`C:\Users\75881\Desktop\宁波材料所\PLC\TwinCAT_NB`。
- 本仓库内可审查的配套 PLC 源码：`PLC/TwinCAT_NB`，包括解决方案、系统工程、PLC 源码、说明和离线检查。
- 同步副本不含 `.vs`、`_Boot`、`_CompileInfo`、`_Libraries`、TMC 等生成内容。XAE 需要安装对应 TwinCAT 版本及 Tc2_MC2 库。
- 本次工作目录与仓库交付副本逐文件核对源码内容一致，交付副本仅规范缩进、换行和行尾空白。后续在外部目录编辑 PLC 后，需要同步源码再提交，避免用旧副本联调。

## 手动 ADS 契约

`N` 为 1..4，反馈使用 `GVL_axis.AxisN`；命令使用 `GVL_control.aStation[{station}].aAxis[N]`，站号由启动时的本站身份解析为 1/2/3，未配置时禁止命令写入。

| 上位机配置项后缀 | PLC 成员 | 类型 | 含义 |
| --- | --- | --- | --- |
| ActualPosition | lrActualPosition | LREAL / Double | NC 实际位置 |
| Speed | lrSpeed | LREAL / Double | NC 实际速度 |
| Enabled | xEnabled | BOOL | MC_Power.Status，使能成功 |
| Homed | xHomed | BOOL | NC 回原完成 |
| Alarm | xError | BOOL | 轴或 MC 功能块错误 |
| PositiveLimit / NegativeLimit | xPositiveLimit / xNegativeLimit | BOOL | 正负限位反馈 |
| OriginSignal | xOriginSignal | BOOL | 原点开关反馈 |
| EnableCommand | xStart | BOOL | 保持型使能请求 |
| ResetAlarmCommand | reset | BOOL | 上升沿复位 |
| HomeCommand | gohome | BOOL | 上升沿标准回原 |
| StopCommand | xStop | BOOL | 停止；现有上位机置位 50 ms 后复位 |
| JogSpeedCommand | lrJogSpeed | LREAL / Double | 点动速度，须为正且不超过最大速度 |
| JogPositiveCommand | jogz | BOOL | NC 正向保持点动 |
| JogNegativeCommand | jogf | BOOL | NC 负向保持点动 |

原默认 `IN1.AxisN` 与本 PLC 源码不一致，现已改为 `GVL_axis.AxisN`。`xMotorRun` 表示正在运动，不能用于“使能完成”，否则静止的已使能轴会被错误显示成未使能。旧配置中正反点动地址也已与当前 PLC 定义统一。

当前 App.config 的 `AxisUnit=°` 和 ±20 显示范围保留既有设置，这不证明 PLC 的实际轴单位就是板角度。现场必须确认 NC 单位、机械传动、轴顺序和正负方向，不能直接把水面高程、板角度或丝杆位移互相代用。

基础轴块没有通过收到停止请求来伪造停止反馈。MC_Stop 继续执行减速，即使上位机的 50 ms 脉冲已撤销；点动互锁会等待功能块释放。轨迹占用时，任何轴停止请求会触发组停车流程。

## 机器监控

新增 `GVL_HMI` 和 `FB_HmiFeedback`。MAIN 在所有基础轴块执行后调用反馈块，发布本扫描的结果。该块不修改轴命令，不启动轨迹。

| ADS 地址 | PLC 类型 | 来源 |
| --- | --- | --- |
| GVL_HMI.stMonitor.nControlOwner | UINT | FB_ControlAuthority 的实际仲裁结果 |
| GVL_HMI.stMonitor.nControlMode | UINT | 无控制端 0、已授权基础控制 1、轨迹占用 2 |
| GVL_HMI.stMonitor.nWaveState | UINT | 从实际轨迹流程和轴错误适配到页面状态编码 |
| GVL_HMI.stMonitor.uiWaveFaultCode | UINT | 当前组/轴错误，供上位机 UInt16 读取 |
| GVL_HMI.stMonitor.udiHeartbeat | UDINT | PLC 约每 500 ms 递增，待机和故障时仍更新 |

控制权由 `FB_ControlAuthority` 仲裁并发布，默认无持有者。运行中禁止申请接管和释放，包含高优先级站；停机后重新申请。各站命令分邮箱，PLC 只复制授权站命令到内部轴数据。采用纯软件申请，不依赖现场主控选择或授权开关；本站身份和 ADS 已连接都不能代替 PLC 的申请应答。完整协议、失联处理和结果码见 [控制权规则](ControlAuthorityRules.md)。

扩展只读字段：`udiDetailedFaultCode`、`nFaultAxis`、`nTrajectoryState`、`nCurrentPoint`、`nLoadedCount`、`nPointCount`、`xDataReady`、`xTrajectoryBusy`。源错误超过 65535 时，兼容 UINT 显示 65535，完整 UDINT 保存在详细字段中。

内部轨迹状态 30 对应页面运行 2；5/10/15/20 对应准备 1；40/50/60/70 对应停止中 4；退出后有错误为故障 7；正常完成为 6；其余为待机 0。未实现的暂停 3、已停止 5 不使用常量模拟。

## 轨迹下发与统一拼接

配套 PLC 参考旧工程的独立分段接收方式：`GVL_motion.aUpload[0..20,1..4,0..99999]`，下标为段号、轴号、段内点号。每段最多 100000 个 LREAL 点/轴，非末段必须写满，末段只写剩余点。

PC 写完一段四轴数据后，用 nUploadOffset、nUploadCount 和 xUpload 上升沿登记，等待 `udiReceivedSequence=本次序号`、`nReceivedCount=本段末偏移` 的 PLC 确认，再下发下一独立段。所有段确认前只接收，不拼表；已确认段禁止覆盖。

全部下发后，PC 单独给 xAssemble 上升沿，PLC 在状态 5 分扫描将所有段拼为四轴连续表；每扫描每轴最多复制 1024 点。只有 `nLoadedCount=nPointCount`、`xDataReady=TRUE`、`xUploadBusy=FALSE` 且无故障，才表示拼接完成，后续 xStart 仍是独立请求。

本版新增 xAssemble、nReceivedCount、udiReceivedSequence，并改变 aUpload 数组布局；旧的单段复用发送端必须同步升级。接收区加执行表共 134.4 MB，部署前检查目标 PLC 的内存和 2 ms 任务时间。完整步骤、短末段例子和重试条件见 [四轴轨迹说明](../PLC/TwinCAT_NB/FOUR_AXIS_CONTROL.md#上位机上传协议)。

## 部署前后验证

1. 在 XAE 编译配套 PLC 源码。确认四轴 PLC↔NC 映射、传感器、急停/STO 和运行联锁。
2. 配置 GVL_motion.aConfig 的真实行程、动态参数及回原方式。基础使能检查授权、运行许可及有效 I/O，不要求整组运动参数填写完；点动/回原仍要求参数有效。默认运行许可为 FALSE。
3. 核对上位机目标 AMS Net ID、端口和回程路由。App.config 地址变化不代表已向现场 PLC 部署；用户保存的连接/Router 配置仍优先使用。
4. 先只读核对符号、类型和五项全局反馈，观察心跳持续变化。四项 UINT、心跳 UDINT 不可按变量名前缀猜测。
5. 使能后不点动，确认 Enabled 仍为 TRUE、位置和速度正常；再逐轴低速验证正负方向、松键停车、停止、回原和复位。
6. 验证轨迹占用时手动命令互锁、故障码发布和解除后的清零。自动造波界面保持本次之前的行为。

基础轴块将位置范围与运动参数分开校验：有效的位置范围继续限制正负方向，未配置范围时仍保留硬限位，运动命令则被完整参数校验阻止。参数无效且轴静止时不持续触发 MC_Stop；运动过程中参数失效仍请求停车。停止使用有效的配置减速度；整组参数无效时传 0，按 [MC_Stop 官方规则](https://infosys.beckhoff.com/content/1033/tcplclib_tc2_mc2/70108555.html) 沿用最近运动命令的减速度。

离线检查：在配套 PLC 目录运行 `python -m unittest discover -s tests -p 'verify_*.py' -v`。其中 `verify_upload_assembly.py` 直接解释实际 ST 状态 0/5，检查接收、统一拼接和异常恢复；内存复制使用范围受检的模拟器。外部目录可用环境变量 `SWITCH_PROJECT` 指定本仓库。契约检查覆盖实际 App.config 中的 28 个轴命令地址和一个监控结构体地址、PLC 类型、反馈调用顺序及只读边界；它不执行完整 PLC/NC 状态机或验证实际 ADS 数据传输。

上位机 Release 编译通过不等于实机对接完成；本次没有下载 PLC、写现场轴命令或验证 2 ms 实时性能。控制权规则检查脚本为 `tests/verify_control_authority.py`。

## 整块监控读取（2026-10-15）

只读监控每轮从 `GVL_HMI.stMonitor` 读取一次，Config 键为 `AdsMonitorSnapshot`；版本 3 / 148 字节，PLC 和 C# 均按 1 字节对齐。旧独立监控变量及 GVL_pc 兼容层已删除；手动页统一读取该结构体；32 项旧轴反馈地址已删除，控制命令地址保留。必须先部署新增 DUT、GVL 和反馈发布逻辑，完整布局及验收见 [监控契约](PlcMonitoringContract.md)。

## 造波操作接入

四按钮、自动准备、会话锁和正常停止已接入，使用最终 PLC 点位、不做算法换算。所有新增协议和验证边界见 [造波操作](WaveOperation.md)。需配套部署版本 3 / 148 字节监控结构体。
