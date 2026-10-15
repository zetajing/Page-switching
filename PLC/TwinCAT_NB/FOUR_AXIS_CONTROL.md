# 四轴基础控制与整条位置表执行

当前实现参考 `PLC-2M-20241113（60分钟）\PLC-2M-20241113（60分钟）\TwinCAT Project1\setpositionGen` 的位置表、首点定位及 2 ms 外部给定流程。参考工程未修改。上位机只下发四轴位置点，完整位置表全部到齐后才允许启动。

## 代码分工

- `FB_AxisControl`：每轴一个实例，保留 MC_Power、MC_Reset、MC_Jog、MC_Home、MC_Stop 和状态反馈。轨迹占用时通过 xGroupBusy 禁止手动命令抢占。
- `FB_FourAxisTrajectory`：分段拼表、整表检查、四轴首点定位、外部给定激活、同步取点、联停和退出。
- `F_TrajectoryPoint`：根据相邻位置点计算本点速度、加速度，校验与执行共用同一函数。
- `MAIN`：统一刷新四轴状态，调用轨迹块和四个基础轴块。

## 与参考工程的对应关系

| 参考工程 | 当前四轴实现 |
| --- | --- |
| `TAble_zz1..21` 分段接收 | `GVL_motion.aUpload` 统一的四轴分段接收区 |
| `YunSuan()` 拼入 `GVL.Table` | 每次扫描最多复制 128 点/轴，按连续偏移拼入块内部的完整位置表 |
| `yusuanwancheng` | `GVL_motion.xDataReady`，全部段到齐后由 PLC 置 TRUE |
| `move_init()` 移到首点 | 四个 MC_MoveAbsolute 全部完成并检查到位偏差 |
| `Table[t]` 每 2 ms 送点 | 四轴共用 `nSendPoint`，每 2 ms 各取自己的位置点 |
| 固定 Velocity=0、Acceleration=2000 | 相邻位置点差分得到速度和加速度，先整表校验再执行 |

没有移植旧项目的主动吸收递推、水位采集、驱动器状态位地址或自定义回零寻边过程。基础回零采用 MC_Home，按当前设备的 NC 回零配置和原点输入执行。

## 容量、周期和内存

- 完整位置表：四轴各 2100000 点，`LREAL`，每轴点号 `0..2099999`。
- 分段接收区：四轴各 100000 点，`LREAL`，段内点号 `0..99999`。
- `aUpload[轴号, 段内点号]`：第一维轴号 `1..4`，第二维点号。通过 ADS 符号分别写各轴数组片段；整段二进制按轴排列，不按四轴逐帧交错排列。
- 固定间隔 2 ms，无额外插值。最大首末点时间差为 `(2100000-1)×0.002 = 4199.998 秒`，约 70 分钟；精确 60 分钟需要 1800001 点。
- 完整表与上传区约占 **70.4 MB（67.14 MiB）**，不含其它运行时开销。容量是声明值，未在目标 PLC 验证内存、编译或长时间运行能力。
- PLC 与 NC SAF 周期均为 2 ms。PLC 优先级 7，NC SAF 优先级 4、NC SVB 优先级 8。
- 拼表和校验每扫描最多处理 128 点/轴。100000 点一段名义复制约 1.564 秒；2100000 点整表校验约 32.814 秒，均未计调度、通信和执行开销，需实测 2 ms 任务时间。

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

新接口代替上一版的“每帧位置/速度/加速度结构体”。xDataReady、nLoadedCount 和 xUploadBusy 都由 PLC 写，上位机只读。

1. 确认 `xBusy=FALSE`、`xUploadBusy=FALSE`。有故障先复位，再将 xClear 给一个上升沿，确认 nLoadedCount=0、xDataReady=FALSE。
2. 设置新的非零 `udiUploadSequence`，与 udiAcceptedSequence 不同；设置整条轨迹总点数 `nPointCount` 和 `nSampleTimeUs=2000`。整条轨迹所有上传段共用这些元数据。
3. 准备第一段四轴位置数据：写入 `aUpload[1..4,0..段点数-1]`，设置 `nUploadOffset=0`、`nUploadCount=段点数`，给 xUpload 一个上升沿。
4. 等待 `nLoadedCount = nUploadOffset+nUploadCount` 且 xUploadBusy=FALSE，再允许覆盖上传区。不能只看到 xUploadBusy=FALSE 就立即覆盖，因为提交请求可能还没被 PLC 扫描到。
5. 下一段 nUploadOffset 必须等于当前 nLoadedCount，重复以上操作。每段最多 100000 点/轴，末段可不足。不能漏段、重复段或超出总点数。
6. `nLoadedCount=nPointCount` 后，PLC 设置 xDataReady=TRUE，表示整条位置表完整到齐。运行中不再接收上传，不是流式双缓冲续传。
7. 开始独立的心跳通信循环，每 100~200 ms 改变 `udiHeartbeat`。校验和运动期间超过 1 秒不变化会联停；上传阶段不要求心跳。
8. 四轴就绪、xStop=FALSE 时，给 xStart 一个上升沿。PLC 先校验整表，再自动将四轴移到各自首点；全部完成后才激活四轴外部给定并推进共同点号。
9. 监视 xBusy、xRunning、xDone、xError、udiErrorId、nState、nCurrentPoint。xRunning 只表示正在推进位置表，不包括首点定位和停止阶段。
10. 正常结束保持末点，不自动返回零位。xDone 保持到清表、复位或下次启动。再次执行需清表并使用新序号重新上传，避免旧启动沿重放。

上传区在段复制期间必须保持不变；元数据改动会拒绝本段，已完成段的 nLoadedCount 不推进，复位后可从相同偏移重传。PLC 不提供未按协议乱写数据时的原子事务保证。xUpload/xClear/xStart/xReset 均为上升沿信号，需回 FALSE 后才能再次触发。

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
| 5 | 分周期拼接当前上传段，仅 xUploadBusy 为 TRUE，不占用基础轴控制 |
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
| 1007 | 联锁、停止、命令冲突或外部给定意外退出 |
| 1008 | 外部给定激活超时 |
| 1009 | 外部给定退出或等待静止超时，保持占用 |
| 1010 | 某轴错误或 NC 反馈 I/O 无效 |
| 1011 | 首点定位超过 60 秒 |
| 其它 | MoveAbsolute、Stop、外部给定 Enable/Disable 的原始错误码 |

关闭外部给定失败时保持占用。xReset 新上升沿使 Disable 请求重试，确认退出和静止后再给新复位沿清组故障。各轴错误仍需各轴 reset。

## 当前验证边界

离线检查脚本为 `tests/verify_trajectory.py`，使用 Python 标准库运行。6 项检查涵盖项目 XML、编译文件引用、ST 控制块结构、数组边界/分段拼接、位置差分和平滑参考位置表，以及 1000 组停车数值检查。它不执行完整的 PLC 状态机，也不验证 ADS 通信。

当前环境没有可用 TwinCAT XAE 编译器，未做 TwinCAT 编译、下载、在线激活、实机运动、60 分钟连续运行或 2 ms 实时性能测试。

参考项目只读取；位置表修改前备份：`C:\Users\75881\AppData\Local\Temp\TwinCAT_NB_before_position_table_20261015_094405`。

## switch 配套接口

本工程补充 `GVL_HMI` 与 `FB_HmiFeedback`，MAIN 在四个基础轴块之后发布五项机器状态和扩展轨迹诊断。`udiWaveFaultCode` 为 UINT，心跳为 UDINT；Owner/Mode 由 `FB_ControlAuthority` 仲裁后经 `GVL_pc` 发布，运行中拒绝控制权交接。

上位机使用 `GVL_axis.Axis1..4`，使能反馈读 `xEnabled`，正向点动写 `jogz`，负向点动写 `jogf`。本次只对接现有手动控制和只读监控，自动造波页面保持原行为。

独立 PLC 编辑目录没有 Git 仓库；可审查源码副本同步到 `switch/PLC/TwinCAT_NB`，与上位机配置一同版本管理。后续外部目录的修改需同步副本。详见 switch 仓库的 `docs/TwinCAT_NB_Integration.md`。

## 三站控制权与命令入口

三站编码 1 造波控制室、2 拖车本地、3 远程操作室。`GVL_control` 中现场选择/授权默认 FALSE，需接入实际开关。运行锁涵盖轨迹、点动、回原、减速、运动块 Busy 和尚未消费的运动命令；运行中拒绝高优先级接管和当前端释放，被拒绝请求不排队。

本说明前文 `GVL_axis.Axis1..4` 的命令字段现在为内部执行值。上位机实际写入 `GVL_control.aStation[本站号].aAxis[轴号]`，反馈仍读取 GVL_axis。PLC 每扫描只接纳授权站邮箱，交接清空旧命令，新站重新发动作。轨迹接口保留，轨迹启动同样需要有效控制权，自动页面未新增运行入口。

失联时撤命令并停车，停稳前不交接；停稳后清除旧 Owner，恢复连接不自动重放动作。细节见 switch 的 `docs/ControlAuthorityRules.md`。
