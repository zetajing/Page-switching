# 三站控制权、交接和失联

[文档导航](README.md)

上位机在启动时读取运行配置的 `ControlStationIdentity`。每个站身份对应一个 PLC 邮箱，操作站独占自己的身份。实际持有者由 PLC 仲裁，操作时查 PLC 公布的授权和运行锁。

| PLC 站号 | 配置值 | 站点 |
| --- | --- | --- |
| 0 | 空值 | 未配置，允许只读监视 |
| 1 | `WaveControlRoom` | 造波控制室，最高优先级 |
| 2 | `TowingLocal` | 拖车本地 |
| 3 | `RemoteControlRoom` | 远程操作室 |

## 申请与释放

- 设备空闲且无人持有时，在线站可申请。已有持有者时，空闲状态下才允许优先级更高的在线站接管。
- 运动、轴块忙、轨迹占用、未消费的运动请求或有效造波会话都会锁住交接。运行中拒绝接管和主动释放。
- 持有者重复申请可确认已有授权。拒绝立即应答，设备回到空闲后须重新申请。
- I/O 无效分别公布为 `xFeedbackInvalid`。无效反馈且仍有轴使能时，交接锁保持有效。基础轴块独立检查新使能和运动条件。

## 请求和应答

站点邮箱位于 `GVL_control.aStation[1..3]`。上位机先写 `nAction`（1 申请、2 释放），最后提交非零 `udiRequestSequence`。PLC 返回 `udiAckSequence` 和 `nResult`。上位机等待相同序号及实际 Owner/Granted 确认，5 秒无确认则报告结果不确定。

| 结果码 | 含义 |
| --- | --- |
| 1 | 成功，持有者重复申请只确认现有授权 |
| 2 | 运行或交接锁有效 |
| 3 | 旧版现场授权拒绝。新版 PLC 保留兼容提示 |
| 4 | 本站心跳未建立或超时 |
| 5 | 本站未持有控制权，不能释放 |
| 6 | 当前持有者优先级更高 |
| 7 | 无效站号或操作 |

PLC 发布每站 `xOnline`、`xGranted` 和全局 `nOwner`、`xLocked`。兼容字段 `xCanRelease` 由当前持有者、交接锁和失联状态共同决定。持有者保存在 PLC 仲裁块私有状态中。

## 心跳和命令清理

上位机连接后，按约 250 ms 的间隔尝试更新本站 `udiHeartbeat`。它与普通 ADS 请求共用串行锁，实际送达间隔取决于通信是否繁忙。PLC 连续 3 秒未观察到心跳变化时将站点判为离线。

持有站离线时，PLC 撤销命令许可，忽略旧保持请求，保留失联状态直到退出条件允许清除持有者。心跳恢复后通过新申请取得授权。交接时清所有站点的旧命令。当前扫描的命令门依据 `MAIN` 给出的站号，读取本站各轴邮箱或清除轴内部请求，同时处理独立的失联停车输入。

造波会话使用 `xWaveSession` 锁住交接。轨迹运行另有 `GVL_motion.udiHeartbeat`，由 PLC 独立检查。`GVL_HMI.stMonitor.udiHeartbeat` 则由 PLC 产生，供上位机确认画面反馈仍在更新。共享锁调度限制见 [ADS 通讯检查](AdsCommunicationReview.md) 和 [监控契约](PlcMonitoringContract.md)。

## 代码与验证入口

PLC 仲裁与判定见 [FB_ControlAuthority](../PLC/TwinCAT_NB/axis_4/POUs/ControlAuthority_控制权/FB_ControlAuthority.TcPOU) 和 [F_ControlDecision](../PLC/TwinCAT_NB/axis_4/POUs/ControlAuthority_控制权/F_ControlDecision.TcPOU)。上位机申请、释放和心跳见 [AxisService.Authority.cs](../Axis/ControlAuthority/AxisService.Authority.cs)。

离线回归执行上述 ST 的优先级、运行锁、连续失联生命周期、交接清除和三站四轴命令映射。真实站点联调需核对各电脑身份、PLC 在线程序和设备反馈。[TwinCAT 对接](TwinCAT_NB_Integration.md)
