# 启动、PLC 读取和页面显示

本次整理保持现有手动控制和只读监控。自动造波计算、点位转换和 CH2–6 协议不在本次修改范围。

## 调用顺序

- 主窗体 `StartServicesAsync`：启动 Router → 允许监控刷新 → 连接手动客户端。
- `AdsTcpRouterRuntime.StartAsync`：记录启动配置；关闭独立 Router 时使用系统 Router，启用时等待独立 Router 就绪。
- `RefreshMonitorAsync`：等待一次读取完成，再将快照交给自动页。
- `MachineMonitorService.ReadSnapshotAsync`：五项全局变量依次读取到局部变量，四轴反馈使用固定循环。
- `ReadPlcAsync<T>`：获取变量句柄 → `ReadAnyAsync<T>` → 在 `finally` 中释放句柄。
- 自动页 `ApplyMonitorSnapshot`：依次更新状态卡片、连接与心跳、四轴表格、状态变化日志。

## 断点位置

在 `Axis/MachineMonitorService.cs` 中搜索以下方法：

| 位置 | 查看内容 |
| --- | --- |
| `ReadSnapshotAsync` 的五项局部变量 | 控制端、模式、造波状态、故障码、心跳的 `Value` / `IsAvailable` / `ErrorMessage` |
| 四轴循环 | `index`、当前 `axis` 的变量名，以及位置、速度和各个 BOOL 反馈 |
| `ReadPlcAsync<T>` 的 `ReadAnyAsync<T>` 调用 | `symbol`、`typeof(T)`、`result.Value`、`result.ErrorCode` |
| 自动页 `UpdateStatusCards` / `UpdateAxisRows` | 读取成功后是否已正确更新控件 |

状态与故障码使用 `ushort`（PLC `UINT`），心跳使用 `uint`（`UDINT`），位置与速度使用 `double`（`LREAL`），状态信号使用 `bool`（`BOOL`）。按实际 PLC 变量表核对类型。

调试时使用当前源码生成的 Debug 程序与 PDB。长时间停在断点会使原有超时或反馈过期检查生效，继续执行后等待下一轮反馈。

## Designer、配置和日志

表格的八列、布局和样式保留在 `auto.Designer.cs`。`AxisFeedbackGrid` 只负责补齐四条占位行，运行时更新单元格，重复初始化不增加行数。

启动时合并一次程序默认配置和用户保存的 ADS 参数，Router、手动客户端、监控客户端使用这份配置。系统配置页保存后需要重启；两台电脑仍各自保存自己的本机身份。具体地址见 [两台电脑 ADS 连接说明](AdsConnectionGuide.md)。

自动页写入统一 `OperationJournal`。手动页通过 `ShowCommandResult`、配置页通过 `ShowSaveResult` 明确显示和记录结果；主窗体不再监听这两页的结果标签。已主动记录结果的分析、修正、批量标定和自定义谱页面也避免重复捕获。尚未迁移的旧页面保留原有标签日志。

退出时先停止界面刷新，取消并等待启动和读取任务，释放两个 ADS 客户端，最后停止独立 Router。停止命令仍单独受理，保留取消、超时、心跳、重连间隔和句柄释放。
