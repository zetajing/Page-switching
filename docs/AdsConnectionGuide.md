# ADS 连接配置与现场排查

[返回文档导航](README.md) · [监控接口与数据质量](PlcMonitoringContract.md) · [通信代码检查与待解决项](AdsCommunicationReview.md)

本文按首次部署、确认通信、故障排查的顺序说明当前程序。源码核对日期：2026-11-20。三台电脑可以连接同一个 PLC，但每台电脑的本机身份和路由需要分别配置。

## 1. 确认地址和实际配置文件

连接前从目标 TwinCAT/XAE 核对以下信息，不从示例地址推算：

| 信息 | 用途 |
| --- | --- |
| PLC 的可达 IP | 网卡通信和 Router 路由的网络地址 |
| PLC 的 AMS Net ID | ADS 目标身份，填写到 `AdsAmsNetId` |
| PLC Runtime 的 ADS 端口 | 填写到 `AdsPort`；Runtime 1 通常为 `851`，以目标工程为准 |
| 各电脑的 IP 和本机 AMS Net ID | PLC 返回该电脑的路由；各电脑的 Net ID 必须唯一，并与 PLC 区分 |

AMS Net ID 的前四段可以与当前 IP 不同。`851` 是 PLC Runtime 的 ADS 端口；`48898` 通常是 Router 的 TCP 端口；`127.0.0.1` 是本机回环地址。

程序读取运行目录中的 `Page switching.dll.config`，启动日志的“ADS 配置文件”给出实际完整路径。项目根目录的 `App.config` 用于生成输出配置，修改它不会立即改变已经运行的程序。

“系统配置”中的“保存 ADS 配置”写入实际运行配置，**重启程序后生效**；通信服务使用本次启动取得的配置快照。发布更新时保留各电脑自己的配置文件，运行目录需要有写入权限。

旧配置只有 `AdsTcpRouterRemoteNetId`、没有 `AdsAmsNetId` 时，程序沿用旧目标；两项都有时以 `AdsAmsNetId` 为准。保存时合并旧项，后续只维护 `AdsAmsNetId`。

## 2. 选择 Router 模式并填写参数

所有电脑都先在“PLC 连接目标”填写同一个 PLC AMS Net ID。页面只显示 PLC ADS 端口；需要更改 `AdsPort` 时，编辑上一步确认的运行配置并重启。

| 设置 | 系统 TwinCAT Router | 程序独立 TCP Router |
| --- | --- | --- |
| 适用电脑 | 已安装并运行 TwinCAT Router | 没有 TwinCAT Router 的电脑 |
| “独立 TCP Router”开关 | 关闭 | 开启 |
| PLC 目标 AMS Net ID | `AdsAmsNetId` | 同一个 `AdsAmsNetId`，也用于创建 PLC 路由 |
| 本机 AMS Net ID | 由系统 Router 管理 | `AdsTcpRouterLocalNetId`，本电脑专用 |
| PLC IP | 在系统 Router 的目标路由中配置 | `AdsTcpRouterRemoteAddress` |
| 本机 Router TCP 端口 | 由系统 Router 管理 | `AdsTcpRouterTcpPort`，通常 `48898` |
| 本机回环地址和端口 | 由系统 Router 管理 | 页面保存为 `127.0.0.1`，端口跟随上面的 TCP 端口 |

独立模式还使用 `AdsTcpRouterName` 标识本机 Router，使用 `AdsTcpRouterRemoteName` 标识 PLC 路由。`AdsTcpRouterLoopbackIp`、`AdsTcpRouterLoopbackPort` 指向本机独立 Router，不能填 PLC IP 或把 PLC ADS 端口 `851` 复制到这里。

页面保存会修正回环 IP 并使回环端口跟随 TCP 端口，**不会把用户填写的 TCP 端口自动改成 `48898`**。手工编辑配置或启用独立模式前，仍要逐项核对。

独立 Router 关闭时，上述独立参数不参与系统 Router 连接。同机已有进程占用监听端口时，独立 Router 可能启动失败；程序会停止此次客户端启动，不会自动改连另一个 Router。

## 3. 配置两端路由并重启

1. 上位机侧登记 `PLC AMS Net ID → PLC 可达 IP`。系统模式在 TwinCAT Router 配置；独立模式由程序按上述参数创建。
2. PLC 侧登记 `各上位机本机 AMS Net ID → 该电脑在 PLC 网络中可达的 IP`。三台分别登记，先核对已有路由，再补缺项。
3. 保存运行配置并重启。日志应显示目标、实际加载的 SDK 版本、Router 模式及客户端连接结果。

若 PLC 就运行在本机系统 Router 下，可先用本地 ADS 读取验证；远端电脑仍需配置路由。各电脑使用相同的 TCP 端口号没有冲突，因为监听发生在不同电脑上；本机 AMS Net ID 和操作站身份则不能重复。

程序先启动 Router，再分别连接监控和手动客户端。两客户端使用各自分配的本机 ADS 端口，访问同一个 PLC Runtime 端口。退出时先取消并释放客户端，最后停止 Router。

## 4. 先确认监控，再确认操作权限

主窗体分别显示手动 ADS、监控 ADS 的连接状态。只读确认应同时满足：目标正确、`GVL_HMI.stMonitor` 可读、布局为 `4 / 164`、心跳持续变化、监控质量正常。“ADS 已连接”本身不能证明反馈实时有效，详见[监控接口](PlcMonitoringContract.md)。

需要操作的电脑在运行配置中设置 `ControlStationIdentity`，一个站点身份只分配给一台电脑。随后在控制权页申请，由 PLC 返回授权。只监视的电脑可留空。配置值与站号见 [三站控制权](ControlAuthorityRules.md)。

三台电脑先分别完成只读确认，再同时读取同一个 PLC，核对各自心跳和质量。当前文档和离线测试不代表三台实机已完成并发联调。

## 5. 按失败阶段排查

| 现象或日志阶段 | 检查顺序 |
| --- | --- |
| Router 启动失败 | 模式是否正确 → 本机端口是否占用 → 本机 Net ID、TCP 和回环参数 |
| “连接”超时、`TargetMachineNotFound`、`TCPSendError` | 网卡是否已连接 → PLC IP 是否可达 → 目标 Net ID、两端路由及 Runtime 端口 |
| “读取变量 GVL_HMI.stMonitor”失败 | 在线符号是否存在 → 是否下载配套程序 → 原始错误属于协议还是通信问题 |
| 布局不匹配 | 核对 PLC 和上位机是否同时更新到 `4 / 164`，不要只反复重启连接 |
| ADS 已连接但等待心跳、心跳停止 | PLC 任务是否运行并发布反馈，可读内存是否一直冻结 |
| 监控恢复，手动仍失败 | 分别查看两个客户端，按下一节确认恢复范围 |

远端 TCP 检查使用 PLC 的真实 IP，将下面占位文字替换后执行：

```powershell
Test-NetConnection -ComputerName 'PLC实际IP' -Port 48898
```

Ping 通不等于 ADS TCP 可用；TCP 可用也不等于路由、Runtime 或符号正确。反向 TCP 探测失败本身不能直接判定读取响应被防火墙阻断，响应可能沿客户端已建立的连接返回。保留完整日志中的失败阶段和底层错误后再定位。

项目同时引用 ADS 与 Router SDK `7.0.389`，部署应包含完整输出目录；启动日志记录实际文件版本。调试监控可在 `MachineMonitorService.RefreshAsync` 的 `ReadAnyAsync<PlcMachineMonitor>` 处查看 `symbol`、`frame` 和异常。

## 6. 明确连接恢复的范围

| 客户端 | 当前行为 |
| --- | --- |
| 手动客户端首次连接 | 每次失败后等待 3 秒重试，同一原因日志去重；首次成功即结束启动重试；关闭窗口取消连接或等待 |
| 独立监控客户端 | 通信失败后等待完整的 3 秒再重建；普通缺符号或类型错误不反复重连；旧句柄失效按错误类别进入重建流程；恢复后重新确认心跳 |
| 手动客户端已连接后的运行期 | 尚无统一的断线或旧句柄恢复入口；独立监控恢复不会替它恢复连接 |

ADS 写入超时可能意味着结果不确定，不能认定 PLC 没收到请求。程序不会自动重放手动命令、控制权申请或恢复运动。手动反馈目前也没有独立监控服务的心跳新鲜度判定。遇到“监控正常但手动失败”应保留两侧日志，按现场流程确认设备状态后恢复服务，不连续重复发送操作。

手动读写与控制权维护共用串行锁。默认操作超时为 5000 ms，长请求可能影响心跳送达。循环每次等待 250 ms，之后仍须等待通信锁。详细证据和待解决项见 [2026-11-13 通信代码检查](AdsCommunicationReview.md)。

## 7. 历史示例与验证边界

以下是旧虚拟 PLC 环境，**仅用于理解配置关系，不是当前现场地址**：

| 设置 | 73 电脑 | 113 电脑 |
| --- | --- | --- |
| 电脑 IP | `192.168.1.73` | `192.168.1.113` |
| Router 模式 | 系统 Router，运行虚拟 PLC | 程序独立 Router |
| 本机 AMS Net ID | `192.168.0.55.1.1` | `192.168.1.113.1.1` |
| PLC 目标 | `192.168.0.55.1.1:851` | `192.168.0.55.1.1:851` |
| 独立 Router 的 PLC IP | 不使用此项 | `192.168.1.73` |
| 独立 Router TCP / 回环 | 不使用此项 | `48898` / `127.0.0.1:48898` |

2026-10-08 记录在 73 的 `C:\ProgramData\Beckhoff\TwinCAT\3.1\Runtimes\UmRT_Default\3.1\Target\StaticRoutes.xml` 中读到 `192.168.1.113.1.1 → 192.168.1.113`。该路径属于当时的 UmRT 环境，不能代替当前系统或实物 CX 的路由核对。

同日记录包括：两个客户端并发读取虚拟 PLC 心跳、手动只读四轴反馈、专用端口的独立 Router 本机客户端登记、端口冲突和关闭清理检查。当时监控仍为逐字段读取，“33 项读取”不是当前整帧接口的性能数据，也未完成 113 到实际 PLC 的端到端验收。

现场仍需确认 PLC IP、AMS Net ID、路由、在线程序和三机持续运行。SDK 升级、本地编译或离线测试通过都不能单独证明现场超时根因已经消除。

相关官方资料：[ADS 设备寻址](https://infosys.beckhoff.com/content/1033/tc3_ads_intro/116159883.html)、[独立 Router 配置](https://infosys.beckhoff.com/content/1033/tc3_ads.net/9409008395.html)、[Static Routes](https://infosys.beckhoff.com/content/1033/tc3_system/5210273931.html)、[Router 7.0.389 包说明](https://www.nuget.org/packages/Beckhoff.TwinCAT.Ads.TcpRouter/7.0.389)。
