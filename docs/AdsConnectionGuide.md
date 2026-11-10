# 三台电脑连接同一台 PLC

[返回文档导航](README.md)

三台电脑运行同一套上位机。安装了 TwinCAT 的电脑使用系统 TwinCAT Router，另外两台分别启用程序自带的独立 Router。三台电脑各自直连 PLC 的 ADS Router，可同时读取 PLC 数据。

## 每台电脑的配置

在上位机「系统配置」的「PLC 连接目标」中填写目标 AMS Net ID。无 TwinCAT 的电脑还需在下方的「独立 TCP Router」区域勾选并填写参数。点「保存 ADS 配置」会更新运行目录中的 `Page switching.dll.config`，重启后应用。启动日志会显示该文件的完整路径。

项目里的 `App.config` 用于生成发布配置。现场手工编辑时，打开正在使用的 `Page switching.dll.config`。PLC 的 AMS Net ID 统一填写 `AdsAmsNetId`，独立 Router 的 PLC 路由也用这个值。旧配置只有 `AdsTcpRouterRemoteNetId` 时，程序会沿用该值。`AdsTcpRouterLocalNetId` 填独立 Router 所在电脑自己的 AMS Net ID。

从旧版本升级时，先启动各电脑实际使用的那份程序。程序会把当前 Windows 用户的旧 `AdsConnectionSettings.json` 导入运行配置，保存成功后清理旧文件。后续覆盖发布文件时，保留各电脑自己的 `Page switching.dll.config`。

| 设置 | 已安装 TwinCAT 的电脑 | 无 TwinCAT 的电脑 B | 无 TwinCAT 的电脑 C |
| --- | --- | --- | --- |
| 独立 Router | 关闭 | 开启 | 开启 |
| 本机 AMS Net ID | TwinCAT 系统 Router 已配置的 Net ID | B 专用 Net ID | C 专用 Net ID |
| PLC AMS Net ID | 三台均填 PLC 实际的 AMS Net ID | 同左 | 同左 |
| PLC ADS 端口 | `851`（PLC Runtime 1） | `851` | `851` |
| PLC IP 地址 | 在 TwinCAT 路由里配置 | 填 PLC 在该网段可达的 IP | 填 PLC 在该网段可达的 IP |
| Router TCP 端口 | 由系统 Router 管理 | `48898` | `48898` |
| 本机回环地址及端口 | 由系统 Router 管理 | 保存时自动使用 `127.0.0.1:48898` | 保存时自动使用 `127.0.0.1:48898` |

AMS Net ID 是 ADS 节点身份。B、C 可以用各自实际 IP 加 `.1.1` 来命名，但必须彼此不同，也要与系统 Router 和 PLC 的 Net ID 区分。两台电脑使用同一个 TCP 端口号没有冲突，因为各自在本机监听。`851` 用于寻址 PLC Runtime，标准 ADS TCP 通信使用 `48898`。参考 [Beckhoff ADS 设备寻址](https://infosys.beckhoff.com/content/1033/tc3_ads_intro/116159883.html)和[独立 Router 配置](https://infosys.beckhoff.com/content/1033/tc3_ads.net/9409008395.html)。

## 在 PLC 侧配置路由

- 在实际运行 PLC 的 TwinCAT Router 中，分别添加 `B 的 AMS Net ID → B 的实际 IP` 和 `C 的 AMS Net ID → C 的实际 IP`。
- 如果 PLC 是独立 CX 或另一台电脑，给已安装 TwinCAT 的上位机与 PLC 也分别配置指向对方的静态路由。如果 PLC 就运行在该上位机的系统 Router 下，本机 ADS 客户端使用本地路由即可。
- 配置前从目标 TwinCAT 核对 PLC 的 AMS Net ID 和实际可达 IP。AMS Net ID 的前四段与 IP 不同时，分别按各自实际值填写。参考 [Beckhoff Static Routes](https://infosys.beckhoff.com/content/1033/tc3_system/5210273931.html)。

## 三站控制权和联调

ADS 联通后，三台电脑可同时监视。需要操作的电脑按现场分工在运行配置中设置 `ControlStationIdentity`，可用值为 `WaveControlRoom`、`TowingLocal`、`RemoteControlRoom`。同一个身份只分配给一台电脑，因为同站会共用 PLC 中的请求、心跳和手动命令邮箱。实际控制权由 PLC 仲裁，同一时刻只有一个站持有。只看监控的电脑可以保留未配置身份。

先从三台电脑分别检查到 PLC IP 的 TCP `48898`，再同时打开上位机，确认三台都能持续读取同一个 PLC 的 `GVL_HMI.stMonitor`，看到变化的反馈心跳和正常的监控质量。若 PLC 在系统 Router 所在电脑上，该电脑以本地 ADS 读取验证。当前上位机监控结构要求布局版本 `4`、`164` 字节；端口连通后若提示符号或结构不匹配，要核对在线 PLC 是否为配套程序。

## 73 与 113 的虚拟 PLC 示例

以下为原虚拟 PLC 环境的配置示例。连接独立 CX 时，使用 CX 当前的 ADS 地址和可达 IP。

| 设置 | 73 电脑 | 113 电脑 |
| --- | --- | --- |
| 电脑 IP | `192.168.1.73` | `192.168.1.113` |
| 独立 Router | 关闭，使用系统 TwinCAT Router | 开启 |
| 本机 Router AMS Net ID | 虚拟 PLC 当前为 `192.168.0.55.1.1` | `192.168.1.113.1.1` |
| PLC 目标 AMS Net ID | `192.168.0.55.1.1` | `192.168.0.55.1.1` |
| PLC ADS 端口 | `851` | `851` |
| 独立 Router 的 PLC 路由 IP | 不使用此项 | `192.168.1.73` |
| 独立 Router TCP / 回环端口 | 不使用此项 | `48898` |
| 独立 Router 回环地址 | 不使用此项 | `127.0.0.1` |

AMS Net ID 是 Router 身份，不要求前四段等于当前 IP。73 的 PLC 已确认为 `192.168.0.55.1.1`，不能仅因 IP 是 `192.168.1.73` 就修改 PLC 目标。

113 的程序先启动独立 Router，再连接手动和监控客户端。两个客户端共用这个 Router，本机 ADS 端口由 Router 自动分配；`851` 是 PLC 的目标端口。

73 的 Router 需要识别 113 的身份：`192.168.1.113.1.1 → 192.168.1.113`。2026-10-08 在 73 的以下文件中已读到这条配置，没有修改它：

`C:\ProgramData\Beckhoff\TwinCAT\3.1\Runtimes\UmRT_Default\3.1\Target\StaticRoutes.xml`

程序的 ADS 设置在系统配置页保存，重启后生效。每台电脑的设置保存在各自运行目录的 `Page switching.dll.config`。

## 读取超时的检查

1. 确认 113 上的程序正在运行，日志出现“独立 ADS TCP Router 已启动”。
2. 确认启动日志的 ADS SDK 和 Router SDK 均为 `7.0.389.0`；更新程序需要包含全部输出文件。当前控制和监控均通过 InduLink DLL 连接；查看监控连接日志的目标地址，113 的本机 Router 身份由配置及启动日志核对。
3. 查看超时的具体步骤。监控日志保留“连接”或“读取变量 变量名”，DLL 异常保留底层 PLC 状态探测、获取句柄及读取失败原因。可在 `MachineMonitorService.ReadFrameAsync` 的 DLL ReadAnyAsync 调用处打断点。
4. Router 运行时检查两台电脑之间的 TCP `48898` 通信。Ping 通不能证明 ADS TCP 通信正常；若端口不通，再核对程序监听和防火墙的放行配置。单独从 73 连接 113 失败不能直接证明读取超时由防火墙造成，读取响应也可能沿 113 主动建立的 TCP 连接返回。
5. 核对 PLC 变量的符号名和类型。当前故障码按 `UINT / ushort` 读取，心跳按 `UDINT / uint` 读取。

项目将 ADS 与 Router 依赖同步到 `7.0.389`。Beckhoff 的发布说明包含远端并发连接和 Router 重启后的连接恢复修复；此版本更新不能单独证明 113 的现场超时根因。参考 [Router 发布说明](https://www.nuget.org/packages/Beckhoff.TwinCAT.Ads.TcpRouter/7.0.389) 与 [Router 配置说明](https://infosys.beckhoff.com/content/1033/tc3_ads.net/9409008395.html)。

## 验证范围

2026-10-08 更新后的程序完成以下验证：

- Debug、Release 编译均为 0 警告、0 错误；输出 ADS 与 Router DLL 的文件版本均为 `7.0.389.0`。
- 73 上两个独立客户端并发读取五轮 PLC 心跳，本机 ADS 端口不同，读取均成功。
- 实际监控服务连续五轮读取 33 项反馈，心跳变化后质量正常；离线、页面显示、单字段失效及关闭检查共 28 条断言通过。
- 手动客户端仍使用现有 InduLink DLL，实际连接并读取四轴反馈成功，没有向 PLC 写入控制命令。
- 使用专用端口和测试 Net ID 启动实际 Router 工厂，两个客户端均取得测试 Router 的本机身份及不同 ADS 端口。该检查只登记本机客户端，不向真实 PLC 发送远端读取。
- 独立 Router 与系统端口冲突时，手动与监控客户端均未连接，关闭清理完成；缩短只读超时后，日志保留具体变量和读取步骤。
- Debug DLL、PDB 和当前读取源码一致，`ReadAnyAsync<T>` 行有断点映射。

以上未替代 113 的现场联调。113 上的独立 Router 需要使用更新后的程序再做两台电脑的端到端验证。
