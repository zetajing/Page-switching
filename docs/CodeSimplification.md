# 代码阅读与调试入口

## 调试时的写法约定

主窗体的控件类型分派和页面名称使用 `switch` 语句，每个 `case` 中保留直白的判断和局部变量，方便打断点。其他流程优先使用普通 `if/else`、`for`、`foreach` 和有名称的方法。复杂条件拆成局部变量，关键分支使用大括号；配置默认值和信号灯状态保持这种写法。轴命令符号读取使用普通私有方法，不使用捕获局部变量的局部函数。

简单事件可以保留直接调用；事件中有多步处理时使用代码块。通信所需的 `async/await`、取消和锁保持原有执行顺序。现有公开的数据类型和接口保持兼容，不为了改语法而更换数据模型或计算公式。

## 四根轴对象与共享通信服务

`AxisService` 创建并持有四个 `Axis` 对象。`GetAxis(1)` 到 `GetAxis(4)` 按轴号取得同一组对象；`Axes` 集合只读，不允许页面替换轴对象。`Axis` 保存轴号和最近一次的 `Snapshot`，提供使能、取消使能、报警复位、回零、停止、点动入口。

```csharp
Axis axis1 = axisService.GetAxis(1);

// 仍由共享服务整帧读取四轴，完成后分别更新各轴的 Snapshot。
await axisService.ReadSnapshotAsync(token);
AxisSnapshot state = axis1.Snapshot;

// 单轴操作入口清楚可见；连接、权限检查和命令排队仍在 AxisService 中。
await axis1.HomeAsync(token);
await axis1.StopAsync(token);
```

手动页通过 `SelectedAxis` 取得所选对象；点动按下时保存 `_jogAxis`，松开和隐藏页面时清零原轴，不跟随之后的下拉框选择。全部使能、全部回零、全部停止等操作保留共享服务原有的批量流程。轴对象不创建或释放 ADS 客户端；连接和退出统一由共享服务管理。

`Snapshot` 是最近一次读取的缓存，不表示持续实时采样。仍需结合连接状态和 `CanControl` 判断是否可以操作；读取失败和断开连接会替换为不可用反馈。页面显示和日志沿用现有入口，不增加 Designer 控件。

推荐断点顺序：`Manual.SelectedAxis → Axis.HomeAsync/StopAsync/JogAsync → AxisService`；反馈查看 `ReadSnapshotAsync → UpdateAxisSnapshots → Axis.Snapshot`。

## 2026-10-20 可读性整理

本次整理面向上位机代码阅读，保持既有公开接口、页面布局、配置键、ADS 布局和文件格式。自动造波控制、PLC 源码、造波公式和轴点位转换不在本次变更内。

推荐按下面的顺序阅读，先看流程，再进入具体实现：

1. `Program.Main → Mainpage`：构造函数创建共享服务和缓存页面；`ConnectPageEvents` 连接跨页请求，`RegisterPageLogging` 注册一次操作日志，`InitializeStartup` 显示默认页面并登记启动事件。
2. `NavigateTo → ShowPage`：所有页面切换继续通过主窗体；批量标定返回、数据管理打开分析也使用缓存页面，保留输入。
3. `Manual → Axis → AxisService`：页面负责显示和按钮状态，轴对象提供单轴入口，服务负责串行通信。`AxisServiceOptions.cs` 单独保存轴配置与命令符号映射；监控继续使用独立客户端。
4. `TableColumns.Initialize`：表格列的标题、顺序和编辑权限由调用页明确提供；辅助方法只创建已有的 `C0…Cn` 列键、object 列类型和绑定关系，不控制页面业务。
5. `WaveDataModels.cs → WaveDataWorkspace`：先了解标定、样本、会话类型，再看标定保存、采集会话、旧格式导入导出和数据库同步。数据库按建表、保存单任务、更新本地同步状态阅读。

关键断点：手动页的 `RunCommandAsync`、`RunStopCommandAsync`、`StartJogAsync`、`StopActiveJogAsync`；监控的 `RefreshAsync`、`ReadFrameAsync`、`SaveFrame`；采集的 `RecordRawSample`、`EndSession`；分析页的 `AnalyzeAsync`、`ShowAnalysisResult`。停止请求版本用于阻止迟到的旧结果覆盖新的停止提示，不能当作多余字段删除。

关键逻辑使用中文注释；保留 Designer 事件方法和控件名称。配置页按波形程序、Router、数据库分别读取和验证；配置保存位置与生效时机沿用原实现。批量标定 CH2–6 只处理文件或手工数据，不启用未经确认的实采协议。

本次离线验证结果、源探针和截图保存在本地 `artifacts/readability-verification/`，具体通过项见该目录的 `summary.txt`。数据库仅检查 SQL、参数及禁用时的本地行为；没有连接真实数据库，也没有连接或写入真实 PLC。

## 既有监控读取与调试说明

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
