# AntdUI 与 ScottPlot 试用

这版沿用 .NET 8 WinForms、Designer 页面布局和统一页面切换。新增依赖为 AntdUI 2.4.12、ScottPlot.WinForms 5.1.59，使用 Windows 10 2004（19041）及以上的平台目标，以匹配现代 SkiaSharp WinForms 依赖。

静态控件的创建、布局、外观属性与本次新增按钮事件绑定位于 `.Designer.cs`；普通 `.cs` 保留数据更新、曲线配置和业务订阅。工程对 `Forms`、`panel`、`Controls` 中的每个 Designer 和资源文件明确填写同名父 `.cs`，不通过文件名表达式推算关联，也不重复 Remove / Include 页面。

控制权页曾出现命令行 `DependentUpon` 正确、VS 仍将 Designer 当独立文件的情况。此次刷新当前 VS 项目的控制权文件关联后，实际项目对象确认 `ControlAuthority.cs` 下有 Designer 和 resx 两个子文件，两者的 `IsDependentFile` 均为 `true`。旧的空白设计标签需要关闭后，从 `ControlAuthority.cs` 重新打开设计器；其他页面有未保存编辑时，不应为此关闭整个解决方案。

## 已接入的页面

- 自动运行：三个状态卡片及“清空本页消息”按钮使用 AntdUI。PLC 快照、四轴表格和无效反馈显示沿用原有逻辑。
- 规则波、不规则波、浪高监测、数据管理：原有 `WaveformPreviewControl` 内嵌 ScottPlot，调用方继续使用 `SetSamples`、`ClearSamples` 和 `UnitText`。
- 曲线工具栏：“自动跟随”及“复位视图”使用 AntdUI；操作通过现有 `OperationJournal` 记录。

本版是上述控件的试用，主窗体导航、分析与信号修正的双曲线控件继续沿用现有实现。

## 曲线使用

“自动跟随”默认开启，每次收到数据后自动显示完整范围。需要查看局部细节时关闭自动跟随，再使用 ScottPlot 的滚轮缩放与鼠标拖动；新数据仍更新曲线，保留当前视图。“复位视图”重新显示完整数据范围，重新开启自动跟随也会立即适配当前数据。

横轴使用调用方提供的真实采样间隔，单位为秒；纵轴使用原来的 `m`、`mm` 或 `count`，不进行物理换算。Signal 保存全部采样值，显示长数据时由 ScottPlot 优化绘制，不再按固定步长丢弃尖峰。单个采样值显示为点，空数据仍显示“暂无波形数据”。

## 验证范围

验证包含采样时间轴、单位、窄尖峰、百万点显示、单点与常量、缩放范围保持、清空和复位、自动页快照、四轴反馈、按钮单次日志、11 个页面切换和原有默认选项。使用 .NET DesignSurface 检查自动页、控制权页和曲线预览加载，并做显式 125% / 150% 布局缩放检查。控制权页额外检查根面板、标题、状态控件，以及运行时申请和释放按钮仍禁用，共 47 项检查通过。

当前 VS 的文件父子关系已经实测。窗口捕获工具两次超时，未完成实际 VS 设计画布的保存重开验证，不能以 DesignSurface 检查替代这一结果。

预览图使用合成波形与测试反馈，不代表设备现场状态。DesignSurface 和显式缩放检查不替代真实 Visual Studio 保存重开及多显示器 DPI 验证。试用后可在当前 Visual Studio 打开 `auto.cs` 和 `WaveformPreviewControl.cs` 的设计器，检查布局和控件属性。

## 上游说明

- [AntdUI](https://github.com/AntdUI/AntdUI)：Apache-2.0。
- [ScottPlot](https://github.com/ScottPlot/ScottPlot)：MIT。
- [ScottPlot WinForms 使用说明](https://scottplot.net/quickstart/winforms/)。

这次只调整界面显示，没有修改造波算法、轴点位转换、采集协议或 PLC 控制命令。
