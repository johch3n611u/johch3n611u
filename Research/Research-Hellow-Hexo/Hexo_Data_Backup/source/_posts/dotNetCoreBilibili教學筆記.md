---
title: dotNetCoreBilibili教學筆記
date: 2020-05-23 05:54:27
tags: StudyProject
categories: StudyProject
---

參考 : <https://www.bilibili.com/video/BV15J411v7v4?from=search&seid=17120593444138397234>

## Asp.Net Core 3.1跨平台实战(.Net Core/跨平台/.Net开发/微服务/IOC/AOP/.Net Core开发/大数据高并发/设计模式)

Eleven 老師 直面大數據高併發

從單機到集群負載均衡

數據庫讀寫分離，分庫分表表分區

特定問題特定解決

拆分走向分布式 - 微服務

### .Net Core31跨平台实战1，环境搭建到项目开发到运行部署

### <span class='red'>重點</span>

* .NET Core 3.0 後全部要求 VS2019 新版本已包含 SDK

* Level => .NET FRAMEWORK / .NET Core / XAMARIN => .NET 5

* .NET Core 包含 ASP.NET Core / UWP

* ASP.NET Core 與 ASP.MVC 差異 ? 框架設計思想進步 全家桶 VS 極簡設計 ? 內置 IOC

* Session 直接 Configure 引入不副寫

* 引入 Nuget Log4net.aspnetcore / NLog 成熟第三方組件 依賴項 複寫 LogService

* 為何不用 Using 或 import ? 因為是在 ASP.NET Core 原來的 Log 基礎上去擴展 Log4Net

* Internet <- HTTP -> IIS (w3wp.exe) ASP.NET Core Module (http://contoso.com) / nginx / Apache (監聽轉發 反向代理)
<- HTTP -> ( ASP.NET Core app ) ( http://localhost:port 監聽 Kestrel (dotnet.exe) <- 解析 HttpContext -> Application code )

* IIS 以前不是做反向代理用的 所以需要 ASP.NETCore Module 轉發用 (一般用 Nginx)

* 因為 Kestrel 高效 但有些地方有缺陷，真實環境應該是一個 nginx 搭配多個 Kestrel 的集群式環境。

* IIS 指向剛剛編譯的資料夾為何不能直接啟動需要發佈 ? 因為差一句命令，發佈的版本內會多一個腳本檔案叫 Web.config 並執行剛剛的 dotnet command

<details>
<summary>完整內容</summary>

.NET5 整合跨平台 2019-12-03 - LTS 3.1

#### 環境配置

* Release information

v3.1.0

* Release notes

* Released

2019-12-03

* Build apps - SDK

3.1.100

* Visual Studio support

VS 2019 v16.4

* Included in

Visual Studio 16.4.0

* Included runtimes

.NET Core Runtime 3.1.0

ASP.NET Core Runtime 3.1.0

Desktop RUNTIME 3.1.0

* Language support

C# 8.0

F# 4.7

* OS / Installers / Binaries

Linux / Package manager instructions / ARM32 | ARM64 | x64 Alpine | x64 | RHEL 6 x64

macOS / x64 / x64

Windows / x64 | x86 / ARM32 | x64 | x86

All / dontnet-install scripts

* Run apps Runtime ASP.NET Core 3.1.0

enables you to run existing web/server app. on windows, we recommended installing the hosting bundle which includes the .net core runtime and IIS support.

IIS runtime support ( Module v2 ) 13.1.19320.0

* Desktop Runtime 3.1.0

#### .NET Core 3.0 後全部要求 VS2019 新版本已包含 SDK

#### 源碼位置 <https://github.com/dotnet/aspnetcore/tree/v3.1.0>

#### 資料結構與生命週期

輸出類型 控制台應用程序 ? 為何會變一個網站 ?

常規 MVC 與 .NET Core MVC 對比 ?

傳值方式 ViewData ViewBag TempData Session

標記區域 #region #endregion

ADO.NET - ActiveX Data Objects

COM - Component Object Model

@model 實體 EF - Entity Framework

腳本啟動 ? 快一些 ?

-------------------------------------

Level => .NET FRAMEWORK / .NET Core / XAMARIN

-------------------------------------

.NET FRAMEWORK 包含 ASP.NET / WPF / WindowsForms

.NET Core 包含 ASP.NET Core / UWP

XAMARIN 包含 iOS / OSX / Android

-------------------------------------

.NET STANDARD LIBRARY

COMMON INFRASTRUCTURE

Build Tools / Languages / Runtime components

-------------------------------------

* Session 直接 Configure 引入不副寫

HttpContext.Session

ISession interface { Clear / CommitAsync / LoadAsync / Remove / Set / TryGetValue }

// 最小化抽象設計 : 通過擴展方法完成易用性擴展

SessionExtensions static 擴展方法 { Get / GetInt32 / GetString / SetInt32 / SetString }

// SessionExtensions.SetString(HttpContext.Session)

// 擴展方法給這實例增加了一種實例方法，就像擴展這個類別

// 但其實是語法糖編譯後還是長得像上面那樣

// 便捷功能可以透過擴展方法來提供

base.HttpContext.Session.SetString("123456",JSON序列化);

-------------------------------------

#### .NET Core 最小化 所以連 Session 都沒有，必須在 ConfigureServices 添加 Session 服務實例，且在 Configure 中間件使用 Session 實例

全家桶 ASP.NET 無所不包但很大包 vs 自選式 ASP.NETCore 簡約設計但並不簡單且高效

框架設計思想進步 ? 內置 IOC

ILogger 日誌 ? IOC ? this._logger.LogInformation("123456");

```C#
public class HomeController : Controller
{
   private readonly ILogger<HomeController> _logger;

   public HomeController(ILogger<HomeController> logger)
   {
       _logger = logger;
   }

}
```

依賴注入 - 控制反轉 ( 擴展第三方 )

AOP ( Aspect-oriented programming )

* 內置日誌及擴展

Program

Startup

-------------------------------------

引入 依賴項 Nuget 複寫

Log4net.aspnetcore / NLog 成熟第三方組件

Microsoft.Extensions.Logging.Log4Net.AspNetCore

log4net.Config ?

#### 為何不用 Using 或 import ? 因為是在 ASP.NET Core 原來的 Log 基礎上去擴展 Log4Net

-------------------------------------

VS 啟動 IIS Express ?

Properties / launchSettings.json

commandName IISExpress / Project -> 控制台程序 -> Web 程序

dotnet Run .exe ?

佈署運行 ?

基於 IIS 佈署，以前可以直接 IIS 指定代碼文件路徑，現在會失敗

2.1 之前 需要 .NET CLR 無託管代碼

模塊 AspNetCoreModuleV2

必須發佈

-------------------------------------

#### 輸出類型 控制台應用程序 ? 為何會變一個網站 ?

#### 全新託管方式基於命令行

腳本託管 ? 弊端 ? 樣式路徑需更改 需要 Startup.cs -> app.UseStaticFiles {  }

首先先至代碼資料夾再開啟 cmd

dotnet Zhaoxi.AspBetCore31.Practica1Demo.dll --urls="http://*:5177"

-------------------------------------

#### 瀏覽器發請求 為何 控制台應用程序 會響應呢 ?

Http -> 協議 -> 請求響應模型 -> 傳遞文本

瀏覽器(應用程序)訪問 -> 發封包 -> 至 server port ? 那如何訪問到 M-V-C 呢 ?

有個中間人 監聽 server port 是否有請求，解析為 HttpContext 然後交給 M-V-C 程序 以往是 IIS 做這個工作

現在 控制台應用程序時負責的為 Kestrel 高效的 HttpServer 以包形式提供，自身並不能單獨運行

封裝 libuv 調用，作 I/O 底層，屏蔽各種系統底層實現差異，

#### 有了 Kestrel 才能實現跨平台。

Internet <- HTTP -> ( ASP.NET Core app ) (  監聽 Kestrel <- 解析 HttpContext -> Application code )

#### 理解 Kestrel 原碼運作

* host -> 主機

Program.cs -> Main 入口 ->

調適

hostBuilder = CreateHostBuilder(args);

host = hostBuilder.Build();

host.run();

// 準備一個 web 服務器並且運行

// asp.net core 控制台本身就是一個 web server

Host.CreateDefaultBuilder(args)

// 創建默認 Builder 完成各種配置

```C#
.ConfigureWebHostDefaults( // 指定一個 web 服務器 Kestrel ，如何監聽如何響應
    webBuilder=>{ // 客製化配置

        webBuilder.UseStartup<Startup>(); // 與 MVC 流程串起來

        }
);
```

1.0 時的套路，並無 ConfigureWebHostDefaults 而是以下

```C#
webBuilder => {
    webBuilder.UseKestrel( // 使用 Kestrel
       o =>{
              o.Listen(IPAddress.Loopback, 12344); // 監聽 12344 port
       }
    )
    .Configure(app=> app.Run(async context => await context.Response.WriteAsync("HelloWorld")))
    // 全部都響應 HelloWorld
    .UseIIS()//iis可用
    .UseIISIntegration();
}
```

#### 原碼運作 F12

Microsoft.Extensions.Hosting

Host.cs

CreateDefaultBuilder // 完成一系列基礎配置

builder.UseContentRoot(Directory.GetCurrentDirectory());

// 靜態文件參考( 獲取當前目錄 ) 所以需要將靜態文件拷貝過去

Environment 開發環境

config.AddCommandLine(args);

// 監聽命令行參數

config.AddJsonFile("appsettings.json", optional:true, reloadOnChange: true)

// 指定設定檔

.ConfigureLogging()

.UseDefaultServiceProvider()

// IOC 依賴注入 控制反轉

return builder;

-------------------------------------

雖然 CreateDefaultBuilder 內有 ConfigureLogging 了，

但你在寫一次就是覆蓋它，很多擴展看源碼就知道如何擴展它了。

-------------------------------------

CronfigureWebHostDefaults()

GenericHostBuilderExtensions.cs

WebHost.ConfigureWebDefaults(webHostBuilder);

WebHost.cs

ConfigureWebDefaults()

builder.UseKestrel()

options.Configure(builderContext.Configuration.GetSection("Kestrel"));

上傳文件 -> 服務器限制大小 -> appsettings.json

AllowedHosts 配置文件

.UseIIS() // 託管模式

.UseIISIntegration() // 非託管模式

-------------------------------------

Internet <- HTTP -> ( ASP.NET Core app ) (  監聽 Kestrel <- 解析 HttpContext -> Application code )

完整生態、生產環境中直接這樣用，獨力完成 http 請求響應，不綁定託管 IIS 跨平台。且基於 CLR Core

Kestrel 基於 Linux I/O libuv。 ASP.NET Core 內置的。

-------------------------------------

Internet <- HTTP -> IIS (w3wp.exe) ASP.NET Core Module (http://contoso.com) / nginx / Apache (監聽轉發)
<- HTTP -> ( ASP.NET Core app ) ( http://localhost:port 監聽 Kestrel (dotnet.exe) <- 解析 HttpContext -> Application code )

.NetCore2.2 + ASP.NETCore Module 支持進程內託管模型反向代理 Web.config

IIS 以前不是做反向代理用的 所以需要 ASP.NETCore Module 轉發用 (一般用 Nginx)

因為 Kestrel 高效 但有些地方有缺陷，真實環境應該是一個 nginx 搭配多個 Kestrel 的集群式環境。

-------------------------------------

IIS 指向剛剛編譯的資料夾為何不能直接啟動需要發佈 ? 因為差一句命令，發佈的版本內會多一個腳本檔案叫

Web.config

handlers name=aspNetCore path=* verb=* modules=AspNetCoreModuleV2

// 任何位置任何請求都交由 AspNetCoreModuleV2 處理

aspNetCore processPath=dotnot arguments=.\xxxx.xxxx.xxxx.dll

// 指定 dotnet 命令 啟動 dll 檔案

-------------------------------------

</details>

### Asp.Net Core 中间件源码解读，新旧管道模型对比解读

### Asp.Net Core 内置IOC容器解读，生命周期理解，扩展autofac，整合AOP

### Asp.Net Core 解读AOP和扩展定制Filter

### 20200209Core31AOPAdvanced

### 20200212微服务架构之全组件解析

### 20200213微服务架构之集群和服务注册发现

### 20200214微服务架构之集群和服务注册发现

### 20200215微服务架构之网关Gateway

### P1020200216微服务OcelotPollyCAP

### P1120200217DIPFactory

### P1220200218CustomIOC

### P1320200219CustomIOCAdvanced

### P1420200223CustomIOCLifeTime

### P1520200225CustomIOCAOP

### P1620200229大数据高并发之集群覆盖均衡

### P1720200301大数据高并发之读写分离

### P1820200302大数据高并发之专项突破

### P1920200303大数据高并发之分布式异步队列

### P2020200307大数据高并发之分布式&微服务

### P2120200308GOF设计模式之结构型

### P2220200309GOF设计模式之行为型

### P2320200310GOF设计模式之创建型

### P2420200315CacheDay1

### P2520200316CacheDay2

### P2620200317CacheDay3

### P2720200108论编程思想的变迁

### P2820200104分布式异步队列

### P2920200105分布式系统构建之分布式事务

### P3020200322DotNet5Practical

### P3120200323DotNet5Practical2

### P3220200324DotNet5Practical3

### P3320200325DotNet5Practical4