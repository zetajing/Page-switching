# 调试入口与代码取舍

[文档导航](README.md) · [上位机功能分工](上位机功能分工.md)

从能复现问题的页面事件或 PLC 扫描环节进入。先核对数据来源，再跟一次完整的读、写或发布过程。

| 现象 | 第一处断点 | 观察值 |
| --- | --- | --- |
| 上位机启动后连不上 PLC | [Mainpage.StartServicesAsync](../Forms/main_page.cs)、[AdsTcpRouterRuntime.StartAsync](../Infrastructure/AdsTcpRouterRuntime.cs) | 实际配置文件、运行端口、Router 模式、异常阶段 |
| 手动未连接，监控已恢复 | [AxisService.ConnectAtStartupAsync](../Axis/AxisService.cs)、[GetConnectedClient](../Axis/AxisService.cs) | 首次连接结果、实际客户端状态 |
| 手动页四轴反馈缺失 | [AxisService.ReadSnapshotAsync](../Axis/Feedback/AxisService.Feedback.cs) 的整帧读取 | PLC 符号、布局、底层异常、快照是否失效 |
| 机器监控等待或过期 | [MachineMonitorService.RefreshAsync](../Axis/Feedback/MachineMonitorService.cs) 的整帧读取、`LatestSnapshot` | 读取阶段、心跳是否变化、最近有效时间、质量 |
| 请求未得到控制权 | [AxisService.ChangeControlAuthorityAsync](../Axis/ControlAuthority/AxisService.Authority.cs)、[PLC 仲裁块](../PLC/TwinCAT_NB/axis_4/POUs/ControlAuthority_控制权/FB_ControlAuthority.TcPOU) | 站号、心跳、运行锁、请求及应答序号 |
| PLC 收到命令但轴未动作 | [MAIN.A03_ManualCommands](../PLC/TwinCAT_NB/axis_4/POUs/MAIN.TcPOU)、[FB_AxisControl](../PLC/TwinCAT_NB/axis_4/POUs/ManualControl_手动控制/FB_AxisControl.TcPOU) | 授权站号、该轴邮箱、实际使能、运动条件和 MC 错误 |
| 页面上看不到本扫描实际状态 | [FB_HmiFeedback](../PLC/TwinCAT_NB/axis_4/POUs/Feedback_反馈/FB_HmiFeedback.TcPOU) | 发布顺序、`stMonitor` 布局和四轴反馈 |
| 点位发送或停稳失败 | [点位下发入口](../Axis/PointUpload/AxisService.PointUpload.cs)、[停止入口](../Axis/Trajectory/AxisService.WaveStop.cs) | 参数版本、上传段和序号、实际停稳及退出结果 |
| 浪高仪已收到字节却没有样本 | [WaveHeightMeterClient](../Wave/Acquisition/WaveHeightMeterClient.cs)、[帧协议](../Wave/Acquisition/WaveHeightProtocol.cs) | 帧头、数据累计、帧长、CH1 原始计数 |
| 曲线、分析或文件有问题 | [采集工作区](../Wave/Acquisition/WaveDataWorkspace.cs)、[分析输入](../Wave/Analysis/WaveSeriesFile.cs) | 实际输入路径、单位、采样率、通道及标定版本 |

## 当前直接数据路径

- PLC 从授权站对应轴读取一份命令，在命令门中提取速度和方向。交接扫描执行清理，失联停车有单独输入。
- 上位机的手动和独立监控各在自己的刷新方法里直接调用 `ReadAnyAsync<PlcMachineMonitor>`。句柄由 DLL 管理，页面只消费转换后的反馈。
- 连接状态直接取自控制客户端。轴快照、运行会话、心跳和请求序号有各自生命周期，保留这些状态用于确认真正的设备反馈。

修改时优先用具名事件、清晰的 `if/else` 和普通循环。单次调用只搬运参数的方法可并回调用点。多处共用的权限、锁、异常和释放逻辑集中在现有入口。PLC 展开变量时保留扫描顺序、取值时刻和外部符号，轴动作与反馈通过现有离线测试核对。界面布局继续保存在 Designer。

通信现场诊断见 [ADS 连接与排查](AdsConnectionGuide.md)，PLC 现场验收见 [TwinCAT 对接](TwinCAT_NB_Integration.md)。
