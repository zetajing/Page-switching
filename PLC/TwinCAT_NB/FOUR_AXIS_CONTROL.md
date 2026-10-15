# 四轴基础控制与整条位置表执行

当前实现参考 `PLC-2M-20241113（60分钟）\PLC-2M-20241113（60分钟）\TwinCAT Project1\setpositionGen` 的位置表、首点定位及 2 ms 外部给定流程。参考工程未修改。PC 先将四轴位置点的所有段下发到独立接收区，全部段确认后单独请求 PLC 统一拼接，完整表拼好后才允许启动。

## 代码分工

- `FB_AxisControl`：每轴一个实例，保留 MC_Power、MC_Reset、MC_Jog、MC_Home、MC_Stop 和状态反馈。轨迹占用时通过 xGroupBusy 禁止手动命令抢占。
- `FB_FourAxisTrajectory`：分段拼表、整表检查、四轴首点定位、外部给定激活、同步取点、联停和退出。
- `F_TrajectoryPoint`：根据相邻位置点计算本点速度、加速度，校验与执行共用同一函数。
- `MAIN`：统一刷新四轴状态，调用轨迹块和四个基础轴块。

## 与参考工程的对应关系

| 参考工程 | 当前四轴实现 |
| --- | --- |
| `TAble_zz1..21` 分段接收 | `GVL_motion.aUpload[段号,轴号,段内点号]`，21 个独立四轴接收段 |
| `YunSuan()` 拼入 `GVL.Table` | 全部段确认后，`xAssemble` 请求统一拼接，每扫描最多复制 1024 点/轴 |
| `yusuanwancheng` | `GVL_motion.xDataReady`，所有段拼接完后由 PLC 置 TRUE；下载完成不会自动置 TRUE |
| `move_init()` 移到首点 | 四个 MC_MoveAbsolute 全部完成并检查到位偏差 |
| `Table[t]` 每 2 ms 送点 | 四轴共用 `nSendPoint`，每 2 ms 各取自己的位置点 |
| 固定 Velocity=0、Acceleration=2000 | 相邻位置点差分得到速度和加速度，先整表校验再执行 |

没有移植旧项目的主动吸收递推、水位采集、驱动器状态位地址或自定义回零寻边过程。基础回零采用 MC_Home，按当前设备的 NC 回零配置和原点输入执行。

## 容量、周期和内存

- 完整位置表：四轴各 2100000 点，`LREAL`，每轴点号 `0..2099999`。
- 分段接收区：21 段，每段四轴各 100000 点，`LREAL`。`aUpload[段号,轴号,段内点号]` 下标分别为 `0..20`、`1..4`、`0..99999`。
- 每段按轴排列，不按四轴逐帧交错；所有已确认段保持不变，接收期间不复制到执行表。第 1 段对应完整表 `0..99999`，第 2 段对应 `100000..199999`，依此类推。
- 固定间隔 2 ms，无额外插值。最大首末点时间差为 `(2100000-1)×0.002 = 4199.998 秒`，约 70 分钟；精确 60 分钟需要 1800001 点。
- 完整表与独立接收区各占 67.2 MB，合计 **134.4 MB（128.17 MiB）**，不含其它运行时开销。相对原来的单段复用方案增加 64 MB，以保留整条待拼接轨迹；目标 PLC 的可用内存和长时间运行能力仍需验证。
- PLC 与 NC SAF 周期均为 2 ms。PLC 优先级 7，NC SAF 优先级 4、NC SVB 优先级 8。
- 拼表按段、按轴调用有界 `MEMCPY`，每扫描最多复制 1024 点/轴，即四轴合计 32 KiB；批次不会跨过段尾。2100000 点需 2058 次拼接扫描，按 2 ms 名义约 4.116 秒，不含请求/反馈及调度开销。
- 整表数值校验仍为每扫描 128 点/轴，最大表名义约 32.814 秒。复制和校验都需实测 2 ms 任务时间，不能将名义扫描次数当作实时性能证明。

## 基础轴接口

`GVL_axis.Axis1..Axis4` 沿用以下命令和反馈：

| 字段 | 方向 | 含义 |
| --- | --- | --- |
| `xStart` | 上位机写 | 保持型使能请求；撤销后先停车再撤使能 |
| `xStop` | 上位机写 | 保持停止，松开后等待 MC_Stop 解锁 |
| `jogz` / `jogf` | 上位机写 | 正反点动，松开减速；同时按下不运行 |
| `lrJogSpeed` | 上位机写 | 正数，不能超过最大速度 |
| `gohome` | 上位机写 | 上升沿启动 NC 标准回零 |
| `reset` | 上位机写 | 上升沿复位该轴，须静止、轨迹已退出且停止已释放 |
| `lrActualPosition` / `lrSpeed` | PLC 写 | NC 实际位置、速度 |
| `xMotorRun` / `xHomed` | PLC 写 | 正在运动、已回零 |
| `xPositiveLimit` / `xNegativeLimit` / `xOriginSignal` | PLC 写 | 限位和原点反馈 |
| `xEnabled` / `xBusy` | PLC 写 | 使能成功、控制忙 |
| `xError` / `udiErrorId` | PLC 写 | 轴或功能块错误及原始错误码 |

轨迹过程中发出点动、回零、复位等手动命令会触发整组停车。点动在轨迹占用或联停后必须先释放按钮，才能重新按下运行。

ST_AxisAds 末尾增加了反馈字段。上位机按符号分别访问；如整结构传输，必须同步更新布局、对齐和大小，不应把旧反馈值整包写回。

## 首次设备配置

1. 在 XAE 引用管理器中确认 Tc2_MC2 正常解析，重新生成项目。
2. 保存的当前工程没有实际 I/O 设备和映射记录。将四个 `GVL_sy.axis[i].PlcToNc` 与 NC 对应轴 `FromPlc` 关联，将 NC `ToPlc` 与 `NcToPlc` 关联。
3. 按设备设置轴单位、比例、方向、软限位、跟随误差、目标位置监控、NC 标准回零方式和速度。
4. 将 `GVL_motion.axPositiveLimit`、`axNegativeLimit`、`axHomeSensor` 接到实际输入处理逻辑，统一 TRUE 表示触发。它们当前是普通 BOOL，尚未映射物理输入。
5. 设置 `GVL_motion.aConfig[1..4]`：

| 参数 | 含义 |
| --- | --- |
| `lrMinPosition` / `lrMaxPosition` | 允许位置范围，最小值小于最大值 |
| `lrMaxVelocity` | 最大速度，必须为正 |
| `lrMaxAcceleration` | 最大加速度绝对值，必须为正 |
| `lrStopDeceleration` | 停车减速度，正数且不大于最大加速度 |
| `lrStartTolerance` | 首点定位完成后与实际/设定位置的允许偏差，正数 |
| `lrInitVelocity` | 自动定位到轨迹首点的速度，正数且不超过最大速度 |

位置、速度、加速度均跟随 NC 单位，不预设毫米或角度。配置仅在所有轴静止、轨迹空闲时修改。默认全零，运动许可默认 FALSE，不会自动开机运动。

6. 将 `xMotionPermit` 接入真实运行联锁。急停、驱动器 STO 等设备安全回路独立配置。
7. 先逐轴低速确认方向、限位、原点和回零，再联调轨迹。四轴须已使能、回零、静止且手动命令已释放。

## 上位机上传协议

本版替换上一版“复用单段接收区、边收边拼”的协议。接收数组改为三维，并增加 `xAssemble`、`nReceivedCount` 和 `udiReceivedSequence`，PC 必须同步更新下发地址和握手。反馈字段均由 PLC 写，PC 只读。

1. 确认 `xBusy=FALSE`、`xUploadBusy=FALSE`。有故障先复位，再将 xClear 给一个上升沿，确认 nReceivedCount=0、nLoadedCount=0、udiReceivedSequence=0、xDataReady=FALSE。
2. 设置新的非零 `udiUploadSequence`，与 udiAcceptedSequence 不同；设置整条轨迹总点数 `nPointCount` 和 `nSampleTimeUs=2000`。整条轨迹所有上传段共用这些元数据。
3. 按 `段号=nUploadOffset/100000` 写该段四轴的全部位置数据：`aUpload[段号,1..4,0..本段点数-1]`。非末段必须 100000 点/轴；末段只写剩余点。四轴写入均成功后，设置 nUploadOffset、nUploadCount，给 xUpload 一个上升沿。
4. 等待 `udiReceivedSequence=本次轨迹序号`、`nReceivedCount=nUploadOffset+nUploadCount` 且无故障，再写下一独立段。`xUploadBusy=FALSE` 不表示段请求已确认；已确认段禁止覆盖。xUpload 回 FALSE 并至少保持一个 PLC 扫描后才能提交下一段。
5. 下一段 nUploadOffset 必须等于当前 nReceivedCount。不能漏段、重复段或超出总点数；总点数、采样周期和整表序号必须保持一致。下载期间 nLoadedCount 始终为 0，xDataReady 为 FALSE。
6. 全部段确认后，PC 单独给 `xAssemble` 一个上升沿，通知 PLC 下发完成。PLC 进入状态 5，xUploadBusy=TRUE，统一拼接到内部四轴连续表。等待 `nLoadedCount=nPointCount`、`xDataReady=TRUE`、`xUploadBusy=FALSE` 且无故障，才算拼表完成。拼接期间不得写任何接收段或改变整表元数据；完成后也不会自动启动运动。
7. 开始独立的心跳通信循环，每 100~200 ms 改变 `udiHeartbeat`。校验和运动期间超过 1 秒不变化会联停；上传阶段不要求心跳。
8. 四轴就绪、xStop=FALSE 时，给 xStart 一个上升沿。PLC 先校验整表，再自动将四轴移到各自首点；全部完成后才激活四轴外部给定并推进共同点号。
9. 监视 xBusy、xRunning、xDone、xError、udiErrorId、nState、nCurrentPoint。xRunning 只表示正在推进位置表，不包括首点定位和停止阶段。
10. 正常结束保持末点，不自动返回零位。xDone 保持到清表、复位或下次启动。再次执行需清表并使用新序号重新上传，避免旧启动沿重放。

PC 使用 ADS 原始块写入时，先解析 aUpload 的实际 IndexGroup/IndexOffset。段号 s、轴号 a、段内起点 k 对应相对字节偏移 `((s*4+(a-1))*100000+k)*8`，本次长度为 `点数*8` 字节；每次写入限制在当前轴当前段内。不能把四轴交错排列后直接写入，也不能把单个 LREAL 元素的符号句柄当作任意长度数组句柄。重新下载或在线变更 PLC 后重新解析符号地址。

例如总点数 100003：PC 写段 0 的四轴各 100000 点并等待接收确认，再写段 1 的四轴各 3 点并等待接收确认；此时 nReceivedCount=100003、nLoadedCount=0、xDataReady=FALSE。PC 再发 xAssemble，PLC 才将两段统一拼为完整表。

拼表过程中元数据变化或新的 Upload/Clear/Assemble 请求会报 1001；内存复制失败报 1012，不发布完整表就绪。故障复位后可用原序号重新 xAssemble，从第 0 点重拼。下载时漏段/错段可复位后从 nReceivedCount 指示的下一段继续；换轨迹必须先清表。接收确认是 PC 声明本段四轴已写完，不是内容校验或原子事务，PC 必须等待所有 ADS 写入成功，PLC 不能检测绕过协议直接覆盖数组的行为。所有请求位均须回 FALSE 并保持至少一个 PLC 扫描，再产生下次上升沿。

## 轨迹规划、停止和状态

上位机规划平滑位置表。首两点必须相等、末两点必须相等，且首尾速度平滑趋于零；仅重复端点不能使任意突变曲线变平滑。PLC 内部使用：

- `v[k]=(p[k+1]-p[k-1])/(2×0.002)`
- `a[k]=(p[k+1]-2p[k]+p[k-1])/0.002²`
- 首末点 v、a 为零。位置量化噪声会被二阶差分放大，超加速度的表会被拒绝。

启动检查位置范围、差分速度、差分加速度、相邻点位置增量和停车空间。停车边界估计为 `p+v×|v|/(2d)+v×0.002`，不包含驱动通信延迟、实际跟随误差和额外安全余量，不能代替设备软硬限位。

首点定位最多等待 60 秒。任一轴异常、限位、撤使能、撤许可、手动命令冲突、停止或心跳超时均联停。定位阶段用 MC_Stop；轨迹阶段从最后送出的速度按配置减速度停车，再退出外部给定并确认静止。减速未做 jerk 平滑，各轴停稳时刻、距离可能不同，不保持四轴路径比例。实际故障轴由 NC/驱动器保护决定停机行为，普通 PLC 联停不能代替安全停机。

| nState | 含义 |
| --- | --- |
| 0 | 空闲/等待下一段 |
| 5 | 全部段下发后统一分周期拼表，仅 xUploadBusy 为 TRUE，不占用基础轴控制 |
| 10 | 整表校验，xBusy=TRUE |
| 15 | 四轴自动定位到各自轨迹首点 |
| 20 | 等待外部给定全部激活，5 秒超时 |
| 30 | 每 2 ms 同步取点 |
| 40 | 保持静止末点 |
| 50 | 联停减速 |
| 60 | 等待外部给定退出 |
| 70 | 等待实际静止和功能块释放 |
| 90 | 故障保持，等待复位 |

| udiErrorId | 含义 |
| --- | --- |
| 1001 | 上传元数据、段偏移、数量、采样周期或序号错误，或整表未到齐 |
| 1002 | 轴参数无效 |
| 1003 | 非有限数据、位置/差分速度/加速度、首尾或停车空间不合格 |
| 1004 | 四轴未就绪或首点定位偏差过大 |
| 1005 | 相邻位置点跳变超限 |
| 1006 | 心跳超时 |
| 1007 | 联锁、命令冲突或外部给定意外退出（正常人工停止独立处理） |
| 1008 | 外部给定激活超时 |
| 1009 | 外部给定退出或等待静止超时，保持占用 |
| 1010 | 某轴错误或 NC 反馈 I/O 无效 |
| 1011 | 首点定位超过 60 秒 |
| 1012 | 拼表的内存复制返回字节数异常，完整表不就绪 |
| 其它 | MoveAbsolute、Stop、外部给定 Enable/Disable 的原始错误码 |

关闭外部给定失败时保持占用。xReset 新上升沿使 Disable 请求重试，确认退出和静止后再给新复位沿清组故障。各轴错误仍需各轴 reset。

## 当前验证边界

离线检查脚本使用 Python 标准库运行。`tests/verify_trajectory.py` 检查项目 XML、文件引用、ST 控制块结构、数组边界、位置差分、平滑位置表及 1000 组停车数值；`tests/verify_upload_assembly.py` 直接解释实际 ST 的状态 0/5，检查整表完成请求、所有容量边界、四轴复制范围、错段/漏段/序号变化、提前启动、复制失败和重拼。内存复制由受检模拟器替代；测试不执行完整 PLC/NC 状态机，也不验证 ADS 数据传输或实机运动。

本次 27 项离线检查通过。已检测到本机安装的 XAE，并在临时工程副本尝试离线自动化编译，但构建调用返回 `HRESULT E_FAIL`，未取得 TwinCAT 编译通过的结果。未做 PLC 下载、在线激活、实机运动、60 分钟连续运行或 2 ms 实时性能测试。

参考项目只读取；位置表修改前备份：`C:\Users\75881\AppData\Local\Temp\TwinCAT_NB_before_position_table_20261015_094405`。

## switch 配套接口

本工程补充 `GVL_HMI` 与 `FB_HmiFeedback`，MAIN 在四个基础轴块之后发布五项机器状态和扩展轨迹诊断。`udiWaveFaultCode` 为 UINT，心跳为 UDINT；Owner/Mode 由 `FB_ControlAuthority` 仲裁后经 `GVL_pc` 发布，运行中拒绝控制权交接。

上位机使用 `GVL_axis.Axis1..4`，使能反馈读 `xEnabled`，正向点动写 `jogz`，负向点动写 `jogf`。本次只对接现有手动控制和只读监控，自动造波页面保持原行为。

独立 PLC 编辑目录没有 Git 仓库；可审查源码副本同步到 `switch/PLC/TwinCAT_NB`，与上位机配置一同版本管理。后续外部目录的修改需同步副本。详见 switch 仓库的 `docs/TwinCAT_NB_Integration.md`。

## 三站控制权与命令入口

三站编码 1 造波控制室、2 拖车本地、3 远程操作室。采用纯软件申请：三站均可提交申请，PLC 根据心跳、运行锁和站号优先级仲裁，不依赖现场选择/授权开关。运行锁涵盖轨迹、点动、回原、减速、运动块 Busy 和尚未消费的运动命令；运行中拒绝高优先级接管和当前端释放，被拒绝请求不排队。

本说明前文 `GVL_axis.Axis1..4` 的命令字段现在为内部执行值。上位机实际写入 `GVL_control.aStation[本站号].aAxis[轴号]`，反馈统一读取 GVL_HMI.stMonitor。PLC 每扫描只接纳授权站邮箱，交接清空旧命令，新站重新发动作。轨迹接口保留，轨迹启动同样需要有效控制权，自动页面已新增发送、自动准备、启动和停止入口。

失联时撤命令并停车，停稳前不交接；停稳后清除旧 Owner，恢复连接不自动重放动作。细节见 switch 的 `docs/ControlAuthorityRules.md`。

## 整块监控接口

`GVL_HMI.stMonitor` 为 `ST_MachineMonitor`，版本 3 / 148 字节，按 1 字节对齐；嵌套四个 `ST_AxisMonitor`（每轴 22 字节）。FB_HmiFeedback 在四轴块执行后集中发布，全局心跳保持约 500 ms 递增。上位机通过 AdsMonitorSnapshot 配置一次读取，失败时整帧不可用，布局版本不符时提示更新 PLC。旧五项独立监控变量和 GVL_pc 兼容层已删除，保留完整故障码及轨迹诊断；控制命令仍进入授权站邮箱。整块读不保证跨任务原子快照，未部署前不能报告实测提速。

## 造波页会话

上位机已接入导入、分段发送、拼表确认、自动使能/必要回原、启动和停止。nCommandStation 必须等于有效 Owner，nUploadStation 为 PLC 锁存反馈；交接/失联使旧表失效。aStation[].xWaveSession 锁住发送和自动准备间隙，失联后忽略会话保持位。原始请求沿先消费再检查授权，避免未授权保持位在以后取得权限时自动执行。

正常人工停止发布 xStopped 和页面状态 5，实际故障仍保持 Error。新增轴 xStopped 区分 MC_Stop.Done 与 Busy 释放；监控 xAxesStopped 先确认停稳，再撤停止保持位并等待 xAxesIdle。新监控结构体版本 3 / 148 字节，详细两端流程见 switch/docs/WaveOperation.md。未做在线下载或实机运动验证。
