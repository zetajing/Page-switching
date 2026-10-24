# 第五项：内部代码整理与调试入口

[返回文档导航](README.md)

2026-10-24 补齐第五项剩余的轨迹块内部整理、上位机会话整理和变量核查。运动流程、参数公式、停止条件、超时、四轴映射、ADS 名称及布局保持兼容。当前 PLC 源码发布号更新为 **2026102401**，监控仍为 **4 / 164 字节**、行为版本 **1**；下载后的在线行为仍需按[验收表](OptimizationAcceptance.md)确认。

## PLC 从一个入口顺着读

`FB_FourAxisTrajectory` 原来约 499 行实现，现在入口只保留八个 Action 的调用和单一阶段分派，变量与 MC 实例继续属于原功能块实例。TwinCAT 中展开该功能块即可进入对应 Action，不新增全局中转变量或独立功能块实例。

| Action | 职责与写入对象 |
| --- | --- |
| A01_RequestsAndTimers | 消费请求沿、核对来源站、准备诊断位置、更新超时实例 |
| A02_ReadAxisStatus | 汇总 NC 状态及 MC 上扫描输出，写轴就绪、忙和退出条件 |
| A03_ResetAndInterlocks | 有效复位、首次联停、人工停止，写故障、表资格和阶段 |
| A04_ReceiveAndAssemble | 状态 0/5：清表、收段、拼表，以及启动请求登记 |
| A05_PrepareAndRun | 状态 10/15/20/30：配置检查、首点定位、开启外部给定、同步取点 |
| A06_StopAndExit | 状态 40/50/60/70：末点保持、减速、关闭、等待退出 |
| A07_CallMotionBlocks | 每扫描调用四轴 Move/Stop/Feed/Enable/Disable；循环后统一处理块错误和退出超时 |
| A08_PublishFeedback | 最后发布接收、拼接、运行阶段和占用反馈 |

入口保留一个 `CASE nState`，一次只选择 A04、A05、A06 中的一个。各 Action 内仍是原来的互斥 CASE 分支；本扫描修改阶段后，不连续执行下一阶段。A07 仍使用更新后的阶段调用 MC 块，与原程序时序相同。状态90保留原来的故障等待复位行为，不执行运动阶段 Action。

实际工程与仓库副本分别保留原代码后迁移；忽略注释和排版展开比较，整理前后均为 **3,131 个相同执行词元**，SHA256 为 `1ce8fc4e4886e9b8f13c5d8d3ca96c15abdccc95923cb7084afc554703a1110e`。测试同时检查分派状态集合、每个 Action 的唯一调用位置和 MC 调用次数，不能以几个阶段顺序调用来替代单一分派。

## 上位机按四种职责阅读

仍是同一个 `AxisService` 的四个分部文件，共用原有连接、锁和状态，没有新建调度框架。

| 文件 | 先打断点的方法 |
| --- | --- |
| Axis/Trajectory/AxisService.WaveSession.cs | RunWaveOperationAsync → ExecuteWaveOperationAsync → BeginWaveSessionAsync / FinishWaveSessionAsync |
| Axis/Trajectory/AxisService.Trajectory.cs | StartWaveCoreAsync → PrepareWaveAxesAsync → ExecuteWaveAsync |
| Axis/Trajectory/AxisService.WaveStop.cs | StopWaveAsync → StopWaveCoreAsync；程序退出由 ShutdownWaveAsync 进入 |
| Axis/Trajectory/AxisService.WaveCommunication.cs | WaveWriteAsync / ReadWaveFrameAsync / WaitWaveFrameAsync；每次读写经过 WaveAccessAsync |

点位下发仍是 `PointUpload/AxisService.PointUpload.cs`：读参数 → 整表预检 → 清表 → 分段下发与确认 → 请求拼表与确认。上传不会自动启动。

从按钮查到 PLC 的三条直接路径：

- 发送：`SendWaveButton_Click → UploadWaveAsync → UploadWaveCoreAsync → WaveWriteAsync / WriteManyAsync → GVL_motion.aUpload / xUpload / xAssemble → MAIN.A04_TrajectoryControl → fbTrajectory.A01 + A04`。
- 开始：`StartWaveButton_Click → StartWaveAsync → RunWaveOperationAsync → ExecuteWaveOperationAsync → BeginWaveSessionAsync → StartWaveCoreAsync → PrepareWaveAxesAsync → ExecuteWaveAsync → PulseWaveAsync("xStart") → GVL_motion.xStart → MAIN.A04_TrajectoryControl → fbTrajectory.A01消费请求 → A04登记启动 → A05准备/取点 → A07调用MC`。
- 停止：`StopWaveButton_Click → StopWaveAsync取消并等待旧任务 → StopWaveCoreAsync → GVL_motion.xStop / 本站aAxis[].xStop → fbTrajectory.A03判断停止 → A06退出 → A07更新MC → A08发布状态 → MAIN.A06_PublishFeedback → 上位机确认停稳并释放请求`。

原来任务中的匿名大段逻辑改成三个具名步骤：登记会话与确认锁/心跳、执行本次操作、失败清理及释放会话。后台任务、取消令牌、异常保留及 finally 顺序不变。新增回归专门验证“等待会话锁时取消”：不得提前清表、写点位、发运动命令；取消清理完成后新的操作仍可正常开始。

## 变量核查与保留理由

全工程 87 份 C# 源码做了编译及 Roslyn 符号核查，包含生成代码共92份输入，编译模型0错误；检查820个私有字段和606个显式局部变量，没有发现无引用或只写不读的候选项。`using` 声明由隐式释放使用，公开数据契约另行保留，不按文本次数删除。PLC 核对22份受管源码、110个内部局部声明，未发现只有声明没有引用的候选项；这项是按 POU 的词法检查，不能当作完整 ST 数据流证明。

以下状态虽然相似，但用途不同，继续保留：

| 变量 | 保留用途 |
| --- | --- |
| nCount / nReceived / nLoaded | 锁存总点数、段确认进度、实际拼入进度，不能用一个计数冒充三种确认 |
| udiSequence / udiAcceptedSequence | 本次表身份与已消费身份，防止旧启动重复执行 |
| aConfig / aSet | 本次运动锁存参数与最后给定；停车需沿最后给定连续减速 |
| nHoldCycles / nStopCycles | 保留末点及至少经过 MC_Stop 调用的扫描时序 |
| nBlockAxis / nBlockSource / udiBlockError | 四轴循环内只收集首个错误，循环后统一切换阶段 |
| _waveActive / _waveStopRequested | 普通任务占用与停止占用不同；停止等待期间仍禁止新任务 |
| _waveFailSafe / _waveNeedsStop | 停车未确认的禁止恢复状态，与本次失败是否需要先停车不同 |
| _waveTask / _waveCancellation | 取消并等待旧任务退出，防止停止后的迟到启动 |
| _waveReadySequence / _waveValidatedLimits | 本次表凭据与其预检参数快照，启动前分别核对 |
| 心跳值 / 最后变化时间 / 已确认变化 | 区分新会话首帧、持续有效反馈和停跳；不能用已连接替代 |

此前已合并按钮/服务条件和单轴/全轴脉冲，删除诊断窗口重复失败缓存。本轮未发现更多可直接删除的内部变量，新增分部文件中移除了搬移后无用的引用，并补齐保留状态的中文用途注释。公开 ADS 字段、反射/序列化配置和 Designer 控件没有据此删除。

## 实际工程与交付边界

实际工程 `C:\Users\75881\Desktop\宁波材料所\PLC\TwinCAT_NB` 已同步本轮两个文件：`FB_FourAxisTrajectory` 的 Action 整理和 `FB_HmiFeedback` 的发布号。两份改动受管源码核对一致。实际 `MC_Direct` 与仓库 `MC_DefaultHoming` 的既有差异保留，用户额外 POU、工程引用和排版未批量覆盖。

本轮源码与离线整理不包含 XAE 编译、PLC 下载或在线运动。PLC 编译下载由用户执行；实际单位、机械转换、安全继电器和 CH2–6 接入继续按原边界处理。

本轮验证：工作目录及独立提交副本 Release 构建均为0警告、0错误；77项PLC/接口离线检查通过（监控89条、模拟ADS服务196条断言）；造波页面128条、手动页面129条、独立提交副本的诊断窗口38条断言通过。实际工程的三个Action结构/展开检查通过，两份修改源码核对一致。检查不进行实际ADS访问。
