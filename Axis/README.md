# Axis 服务目录与 F12 调试入口

`AxisService.cs` 是共享配置、ADS 控制连接和生命周期入口。下面各目录中的 `partial AxisService` 都属于这个类，共用同一个实例中的字段、连接、锁和运行状态。

正常启动的对象关系：

```text
Mainpage._axisService（创建一次）
  ├─ Auto._waveService
  ├─ Manual._axisService
  ├─ ControlAuthority._axisService
  ├─ MachineDiagnosticsPage._axisService
  └─ Axis[1..4]._axisService
```

主窗体负责关闭共享服务；页面切换复用服务和轴对象。手动页的无参构造保留给 Designer，正常运行由 `Mainpage` 传入服务。不要在页面切换或 `Axis` 操作中再建控制服务。

| 功能目录 | 主要代码 | F12 跟踪入口 |
| --- | --- | --- |
| 根目录 | [AxisService.cs](AxisService.cs)、[AxisServiceOptions.cs](AxisServiceOptions.cs) | 构造函数、`GetAxis`、`ConnectAsync` |
| 手动控制 | [ManualControl/Axis.cs](ManualControl/Axis.cs)、[AxisService.Manual.cs](ManualControl/AxisService.Manual.cs) | `Axis.HomeAsync → HomeAxisAsync → PulseAxisAsync → PulseAllAsync` |
| 反馈与诊断 | [AxisService.Feedback.cs](Feedback/AxisService.Feedback.cs)、[AxisService.Diagnostics.cs](Feedback/AxisService.Diagnostics.cs) | `ReadSnapshotAsync`、`ReadDiagnosticsAsync` |
| 控制权 | [AxisService.Authority.cs](ControlAuthority/AxisService.Authority.cs) | `ChangeControlAuthorityAsync`、`RequireControlAuthority` |
| 点位下发 | [AxisService.PointUpload.cs](PointUpload/AxisService.PointUpload.cs) | `UploadWaveAsync → UploadWaveCoreAsync` |
| 轨迹会话 | [AxisService.Trajectory.cs](Trajectory/AxisService.Trajectory.cs)、[AxisService.WaveSession.cs](Trajectory/AxisService.WaveSession.cs) | `StartWaveAsync → RunWaveOperationAsync → StartWaveCoreAsync` |
| 轨迹通信与停止 | [AxisService.WaveCommunication.cs](Trajectory/AxisService.WaveCommunication.cs)、[AxisService.WaveStop.cs](Trajectory/AxisService.WaveStop.cs) | `WaveAccessAsync`、`WaitWaveFrameAsync`、`StopWaveAsync` |
| 运行统计 | [AxisService.Monitoring.cs](Monitoring/AxisService.Monitoring.cs)、[AxisRuntimeMonitoringService.cs](Monitoring/AxisRuntimeMonitoringService.cs) | `StartRuntimeMonitoring → Start → SampleAsync` |

`MachineMonitorService` 和运行统计使用独立的只读客户端；它们与共享控制服务职责不同。调试时先确认自己在看哪条读取链。`Axis.Snapshot` 是最近一次读取结果，不能单靠缓存数值判断 PLC 当前状态。

页面事件和日志断点说明见 [代码阅读与调试入口](../docs/CodeSimplification.md)。
