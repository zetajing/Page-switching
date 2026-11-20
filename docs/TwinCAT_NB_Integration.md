# TwinCAT PLC 对接与验收

[文档导航](README.md)

仓库配套工程位于 [PLC/TwinCAT_NB](../PLC/TwinCAT_NB)。本机另有独立编辑工程 `C:\Users\75881\Desktop\宁波材料所\PLC\TwinCAT_NB`。打开 XAE 后先确认当前解决方案引用哪一份。独立工程与仓库副本可能有现场差异，例如以往确认的 `MC_Direct` 与仓库的 `MC_DefaultHoming`。合并源码时逐文件核对，回原方式、设备参数和 I/O 接线按实际工程复核。

## 从命令邮箱到运动块

| 环节 | 数据来源与职责 |
| --- | --- |
| 上位机 | 从运行配置解析本站身份，向 `GVL_control.aStation[站号].aAxis[轴号]` 提交请求 |
| `FB_ControlAuthority` | 按本站心跳、运动锁和优先级处理请求，公布 `nOwner`、`xCanCommand` 和失联状态 |
| `MAIN.A03_ManualCommands` | 只在允许命令且本扫描未交接时传入实际站号，其余情况传入 0 |
| `FB_AxisCommandGate` | 读取对应轴的命令一次，转入该轴内部数据。站号无效时清请求，保留独立的失联停车输入 |
| `FB_AxisControl` | 调用轴的使能、点动、回原、停车和复位块，再更新实际反馈 |
| `FB_HmiFeedback` | 四个基础轴块执行后发布 `GVL_HMI.stMonitor` |

PLC 任务先读取 NC 状态，再执行仲裁、轴配置、手动命令门、现有轨迹块、基础轴块和反馈发布。命令门仍先于轨迹联锁检查，PLC 取数和执行顺序可在 [MAIN.TcPOU](../PLC/TwinCAT_NB/axis_4/POUs/MAIN.TcPOU) 查看。

## ADS 手动命令和反馈

| 邮箱字段 | 类型 | 上位机动作 |
| --- | --- | --- |
| `xStart` | BOOL | 使能请求保持位 |
| `xStop` | BOOL | 手动停车请求 |
| `reset`、`gohome` | BOOL | 复位和回原请求，当前上位机使用 50 ms 脉冲 |
| `jogz`、`jogf` | BOOL | 正反点动，按下置位、松开清除 |
| `lrJogSpeed` | LREAL | 点动速度幅值，先于方向请求写入 |

发送命令后，实际状态从 `GVL_HMI.stMonitor` 读取。例如使能取 `MC_Power.Status`，位置和速度取 NC 实际值。C# 要求布局 **版本 4 / 164 字节**。现有点位下发与启动额外校验行为版本 1。字段布局和操作流程分别见 [PLC 只读监控接口](PlcMonitoringContract.md) 和 [造波文件、下发和启停](WaveOperation.md)。

## 编译前核对

- 使用只读脚本比较实际工程与 Git 副本。需要时把示例路径换成现场工程路径。
- 确认 XAE、PLC Runtime、NC、Tc2_MC2 及相关引用，检查四轴 NC 引用与轴号一一对应。运行端口以实际工程为准。
- 仓库配置块含虚拟轴调试参数，`MAIN` 也含调试许可。实机的轴单位、限位、原点、运行许可、停机和抱闸条件应按现场工程确认。
- 当前发布号为源码中的 `2026102401`。核对 XAE 保存文件、程序发布号及实际在线实例，避免用发布号代替源码比对。

```powershell
python PLC/TwinCAT_NB/tests/check_source_copy.py --actual 'C:\Users\75881\Desktop\宁波材料所\PLC\TwinCAT_NB'
python -m unittest discover -s PLC/TwinCAT_NB/tests -p 'verify_*.py' -v
```

## 现场验证顺序

1. 在 XAE 编译配套工程，按现场流程部署并核对实际在线版本。
2. 先连通 ADS，确认 PLC 目标、运行端口和符号。查看心跳、整体质量、四轴实际位置与速度、使能、回原、报警、限位和原点。路由见 [ADS 连接与排查](AdsConnectionGuide.md)。
3. 按测试条件在配套虚拟轴或现场验证三站交接、失联、停止和复位反馈。每次检查实际轴行为及 PLC 应答，操作条件见 [三站控制权](ControlAuthorityRules.md)。
4. 记录 XAE 编译、在线读写、停稳和现场验收证据。离线测试只覆盖源码分支、接口与模拟通信。固定样例和已有验证见 [虚拟轴验收记录](OptimizationAcceptance.md)。
