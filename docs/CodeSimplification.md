# 监控读取和调试入口

自动页的 MachineMonitorService 和手动页的 AxisService 统一使用现有 InduLink ADS DLL。两条路径均按 AdsMonitorSnapshot 配置，通过 DLL ReadAnyAsync<PlcMachineMonitor> 整块读取，校验版本 3 / 148 字节后转换成页面快照。

- MachineMonitorService.ReadFrameAsync：调用 DLL ReadAnyAsync<PlcMachineMonitor>；应用不再直接创建、缓存或释放官方 SDK 句柄。
- AxisService.ReadAdsSnapshotsAsync：调用 DLL ReadAnyAsync<PlcMachineMonitor>，DLL 维护句柄；ToAxisSnapshots 转换四轴反馈。
- PlcMachineMonitor.ValidateLayout：检查版本、字节数，禁止将旧布局解释成新数据。
- PlcAxisMonitor：实际双精度数值、1 字节 BOOL、使能反馈转换；非有限数值显示不可用。

两端布局为 1 字节对齐，每轴 22 字节，四轴起始偏移为 16/38/60/82。自动页显示/隐藏只影响是否生成轴表格数据。32 项旧轴反馈配置和旧五项独立监控配置均已删除，命令仍进入授权站邮箱。

调试时在两个 ReadAnyAsync 调用、布局校验及页面 UpdateAxisRows/UpdateAxisOverview 打断点。观察实际错误、结构体 Version/Size、轴顺序及 Enabled，不能以默认零值或连接成功代替有效反馈。

监控单次请求默认 1000 ms，整轮默认 3000 ms；手动页保留自己的刷新间隔和操作超时。真实超时和反馈过期处理保持现有机制，不承诺现场运动停止时延。UI Designer、控制权和运动命令写入流程未调整。

两个服务保留独立客户端，避免本次封装迁移改变控制、轨迹上传和心跳的串行调度。监控连接开启 DLL 的 ValidateTargetStateOnConnect，句柄及请求超时由 DLL 管理；通信失败按现有间隔重连，失效句柄通过重建客户端恢复，不存在的符号保留原始错误并避免无效重连。

2026-10-15 验证：Release 构建 0 警告、0 错误；实际只读服务连续读取 PLC 四轮，首轮等待心跳，后续质量正常、四轴数据完整。28 项在线及服务检查覆盖 DLL 单句柄缓存、过期/停跳、忙时跳过、取消、缺失符号、失效句柄恢复和重复退出；未向 PLC 写入控制、心跳或轨迹变量。现有 37 项离线测试通过，包含监控帧 82 条断言及造波服务 49 条模拟 ADS 断言。
