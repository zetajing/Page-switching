# AntdUI 与 ScottPlot 试用记录

[返回文档导航](README.md)

当前全面迁移与验证见 [AntdUI 全页面迁移](AntdUiMigration.md)。以下保留初期试用记录。

初期版本沿用 .NET 8 WinForms、Designer 页面布局和统一页面切换。新增依赖为 AntdUI 2.4.12、ScottPlot.WinForms 5.1.59，使用 Windows 10 2004（19041）及以上的平台目标，以匹配现代 SkiaSharp WinForms 依赖。

静态控件的创建、布局、外观属性与本次新增按钮事件绑定位于 `.Designer.cs`；普通 `.cs` 保留数据更新、曲线配置和业务订阅。工程对 `Forms`、`panel`、`Controls` 中的每个 Designer 和资源文件明确填写同名父 `.cs`，不通过文件名表达式推算关联，也不重复 Remove / Include 页面。

控制权页曾出现命令行 `DependentUpon` 正确、VS 仍将 Designer 当独立文件的情况。此次刷新当前 VS 项目的控制权文件关联后，实际项目对象确认 `ControlAuthority.cs` 下有 Designer 和 resx 两个子文件，两者的 `IsDependentFile` 均为 `true`。旧的空白设计标签需要关闭后，从 `ControlAuthority.cs` 重新打开设计器；其他页面有未保存编辑时，不应为此关闭整个解决方案。

## 已接入的页面

- 自动运行：三个状态卡片及“清空本页消息”按钮使用 AntdUI。PLC 快照、四轴表格和无效反馈显示沿用原有逻辑。
- 规则波、不规则波、浪高监测、数据管理：原有 `WaveformPreviewControl` 内嵌 ScottPlot，调用方继续使用 `SetSamples`、`ClearSamples` 和 `UnitText`。
- 曲线工具栏：“自动跟随”及“复位视图”使用 AntdUI；操作通过现有 `OperationJournal` 记录。

本版是上述控件的试用，主窗体导航、分析与信号修正的双曲线控件继续沿用现有实现。

主窗体随后接入 AntdUI：10 个导航按钮、日志目录按钮、7 个标题及状态标签，以及顶部、状态和日志路径面板均在 `main_page.Designer.cs` 中创建。滚动导航容器与 `panelswitch` 保留原 WinForms 布局；导航仍经过 `NavigateTo → ShowPage`，选中、悬停和按下样式使用 AntdUI 对应属性。没有新增设备连接或修改控制命令。

## 曲线使用

“自动跟随”默认开启，每次收到数据后自动显示完整范围。需要查看局部细节时关闭自动跟随，再使用 ScottPlot 的滚轮缩放与鼠标拖动；新数据仍更新曲线，保留当前视图。“复位视图”重新显示完整数据范围，重新开启自动跟随也会立即适配当前数据。

横轴使用调用方提供的真实采样间隔，单位为秒；纵轴使用原来的 `m`、`mm` 或 `count`，不进行物理换算。Signal 保存全部采样值，显示长数据时由 ScottPlot 优化绘制，不再按固定步长丢弃尖峰。单个采样值显示为点，空数据仍显示“暂无波形数据”。

## 验证范围

验证包含采样时间轴、单位、窄尖峰、百万点显示、单点与常量、缩放范围保持、清空和复位、自动页快照、四轴反馈、按钮单次日志、11 个页面切换和原有默认选项。使用 .NET DesignSurface 检查自动页、控制权页和曲线预览加载，并做显式 125% / 150% 布局缩放检查。控制权页额外检查根面板、标题、状态控件，以及运行时申请和释放按钮仍禁用，共 47 项检查通过。

当前 VS 的文件父子关系已经实测。窗口捕获工具两次超时，未完成实际 VS 设计画布的保存重开验证，不能以 DesignSurface 检查替代这一结果。

审核剩余 Designer 修改时，先备份并保存 VS 中未保存的相关页面，再检查最终文件。检查扩展至 66 项：11 个页面的 Designer 控件均有父控件、全部相关下拉默认选项、信号修正数值默认值、手动轴和点动速度、配置中的本站身份，以及 CH1 曲线和 CH2–6 禁用状态。发现信号修正页的单位默认选项被 Designer 省略，已在构造函数恢复原来的 `mm`；只恢复默认选项，没有调整修正算法或单位换算。

主窗体替换后共 102 项检查通过，包括逐个触发 10 个导航按钮的真实 Click 事件、目标页面、唯一选中样式、单次操作日志、两种窗口尺寸和主窗体 DesignSurface 加载。主窗体预览采用独立画布承载原布局，未触发 `Shown`，没有连接 PLC 或浪高仪；预览不替代实际 VS 设计器保存重开验证。

预览图使用合成波形与测试反馈，不代表设备现场状态。DesignSurface 和显式缩放检查不替代真实 Visual Studio 保存重开及多显示器 DPI 验证。试用后可在当前 Visual Studio 打开 `auto.cs` 和 `WaveformPreviewControl.cs` 的设计器，检查布局和控件属性。

## 上游说明

- [AntdUI](https://github.com/AntdUI/AntdUI)：Apache-2.0。
- [ScottPlot](https://github.com/ScottPlot/ScottPlot)：MIT。
- [ScottPlot WinForms 使用说明](https://scottplot.net/quickstart/winforms/)。

这次只调整界面显示，没有修改造波算法、轴点位转换、采集协议或 PLC 控制命令。
