# 内部 DLL 放置说明

本项目改为引用 DLL，不再引用或还原 `Total` 的任何项目，也不要求两个仓库按固定目录排列。

将下面五个 DLL 直接放在本文件所在的 `lib` 目录中。请使用相互匹配的 .NET 8 构建产物，建议从同一次 Release 构建中获取。

| 必需文件 | 用途 | 在 Total 工程中的输出位置 |
|---|---|---|
| `InduLink.Abstractions.dll` | ADS 请求、数据类型和质量状态等公共定义 | `InduLink.Abstractions/bin/Release/net8.0/`，ADS 输出目录也包含此文件 |
| `InduLink.Runtime.dll` | ADS 所需运行基础设施 | `InduLink.Runtime/bin/Release/net8.0/`，ADS 输出目录也包含此文件 |
| `InduLink.Protocols.Ads.dll` | 四轴手动控制的 ADS 客户端 | `InduLink.Protocols.Ads/bin/Release/net8.0/` |
| `InduLink.Protocols.Ads.Router.dll` | 可选独立 ADS TCP Router 的封装 | `InduLink.Protocols.Ads.Router/bin/Release/net8.0/` |
| `InduLink.Storage.dll` | 现有日志显示组件 | `InduLink.Storage/bin/Release/net8.0/` |

目录结构应为：

```text
nb/
├─ Page switching.csproj
└─ lib/
   ├─ README.md
   ├─ InduLink.Abstractions.dll
   ├─ InduLink.Runtime.dll
   ├─ InduLink.Protocols.Ads.dll
   ├─ InduLink.Protocols.Ads.Router.dll
   └─ InduLink.Storage.dll
```

五个文件均为构建必需项。即使应用配置关闭独立 Router，主窗体仍引用 Router 类型，因此仍需提供 Router DLL。无需在 Visual Studio 中逐个添加引用，项目已经配置好路径；构建时会把这些 DLL 复制到输出目录。

## 第三方依赖

DLL 引用不会继承原项目的 NuGet 依赖。本项目显式保留以下包，正常还原后自动提供对应的第三方运行文件，无需手工将它们放进 `lib`：

| NuGet 包 | 版本 | 来源 |
|---|---|---|
| `Beckhoff.TwinCAT.Ads` | `7.0.317` | ADS 客户端依赖 |
| `Beckhoff.TwinCAT.Ads.TcpRouter` | `7.0.317` | 独立 Router 依赖 |
| `Newtonsoft.Json` | `13.0.4` | InduLink.Runtime 依赖 |
| `System.Security.Cryptography.ProtectedData` | `8.0.0` | InduLink.Runtime 依赖 |
| `System.Data.SqlClient` | `4.8.6` | 原有数据库和 Storage 依赖 |
| `System.Configuration.ConfigurationManager` | `8.0.0` | 原有应用配置依赖 |

保留原有 `Microsoft.AspNetCore.App` 框架引用，用于配置和日志基础设施。

## 还原与构建

在项目目录运行：

```powershell
dotnet restore "Page switching.sln"
dotnet build "Page switching.sln" -c Release
```

缺少内部 DLL 时，NuGet 还原仍可完成；构建会直接列出缺少的 DLL 文件名及预期目录。补齐文件后重新构建即可。此目录初始仅包含说明文件，DLL 由使用者补充。
