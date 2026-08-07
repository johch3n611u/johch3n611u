# F:\ 專案完整技術文件 — Xylon/Xtrack 系統

> **縮寫慣例**：本文中所有英文縮寫在首次出現時會標註全稱 (Full Name)，後續使用縮寫。範例：CDP (Common Data Platform，通用資料平台)

> **最後更新：** 2026-07-31 | **去重規則：** 重複專案以 pom.xml 最新修改日期為準 | **專案數：** 24 個

---

## 目錄

1. [專案總覽](#1-專案總覽)
2. [系統整體架構](#2-系統整體架構)
3. [Xtrack .NET ERP**(Enterprise Resource Planning)** 平台](#3-xtrack-net-erp-平台)
4. [CDP**(Common Data Platform)** 核心平台](#4-cdp-核心平台)
5. [cdp-ups**(Unified Permission & Storage)**— 統一權限與檔案系統](#5-cdp-ups)
    - [5.5 新平台接入：權限體系與必要串接 (EISP / UPS)](#55-新平台接入權限體系與必要串接-eisp--ups)
    - [5.5.1 後端菜單權限管控](#551-後端菜單權限管控)
    - [5.5.2 後端功能權限管控 (@CdpRbac)](#552-後端功能權限管控-cdprbac-註解)
    - [5.5.3 前端菜單/按鈕權限](#553-前端菜單按鈕權限)
    - [5.5.4 後端注入 AuthenticationFacade](#554-後端注入-authenticationfacade-取得目前使用者)
    - [5.5.5 新站台權限管控 SOP 重點](#555-新站台權限管控-sop-重點對照上方範例)
6. [cdp-job — 分散式任務排程](#6-cdp-job)
7. [cdp-mqs**(Message Queue & Notification Center)** — 訊息佇列與通知中心](#7-cdp-mqs)
8. [cdp-cofa-cd**(Collaborative Failure Analysis System)** — FA**(Fail Analysis)** 最終組裝線 MES](#8-cdp-cofa-cd)
9. [cdp-smt-cofa — SMT**(Surface Mount Technology)** 表面貼裝線 MES**(Manufacturing Execution System)**](#9-cdp-smt-cofa)
10. [cdp-bpo**(Business Process Outsourcing)** — 業務流程外包管理](#10-cdp-bpo)
11. [cdp-planr — 排程規劃系統](#11-cdp-planr)
12. [cdp-vision-ct**(Computed Tomography)** — 影像檢測系統](#12-cdp-vision-ct)
13. [DocSecure — 文件安全管理](#13-docsecure)
14. [cdp-radar — 雷達同步系統](#14-cdp-radar)
15. [hrm-web — 人力資源管理](#15-hrm-web)
16. [其他專案](#16-其他專案)
17. [開發環境與規範](#17-開發環境與規範)
    - [17.6 如何透過 Maven 產生可執行 Debug JAR](#176-如何透過-maven-產生可執行-debug-jar-類似-fxylon-server)
18. [cdp-malloc — 物料分配與 DRP 系統](#18-cdp-malloc)
    - [18.2 與 Matrix (dsc-matrix) 及 cdp-mpc 專案的關係](#182-與-matrix-dsc-matrix-及-cdp-mpc-專案的關係)
    - [18.9 cdp-malloc Local Debug 最少啟動服務](#189-cdp-malloc-local-debug-最少啟動服務)
19. [STS Workspace 遷移問題記錄](#19-sts-workspace-遷移問題記錄2026-07-31)
20. [dsc-matrix — Matrix 矩陣配置平台](#20-dsc-matrix)

---

## 1. 專案總覽

### 去重結果

| Repo | 版本路徑 | 選擇 | 原因 |
|---|---|---|---|
| cdp | `JavaWorkspace\cdp` | ✅ 選此 | pom.xml 最新 (vs codespace 舊版) |
| cdp-cofa-fa | `JavaWorkspace\cdp-cofa-cd` | ✅ 選此 | pom.xml 最新 (vs cofa-pro, codespace 舊版) |
| cdp-ups | `JavaWorkspace\cdp-ups` | ✅ 選此 | pom.xml 最新 (vs codespace, backup 舊版) |

### 最終不重複 24 個專案

| # | 專案 | 路徑 | 類型 | 摘要 |
|---|---|---|---|---|
| 1 | **APS** | `Xtrack-workspace\aps` | .NET WinForm | APS 排程桌面應用 |
| 2 | **SWS-APS** | `Xtrack-workspace\sws-aps` | .NET Solution | SWS 版 APS，含排班引擎 |
| 3 | **XtrackOAuth2Bridge** | `Xtrack-workspace\xtrackoauth2bridge` | .NET Library | OAuth2/JWT 驗證橋接層 |
| 4 | **XtrackSDBWS** | `Xtrack-workspace\xtracksdbws` | .NET | 統一 DB Web Service |
| 5 | **XtrackWEB** | `Xtrack-workspace\xtrackweb` | ASP.NET WebForms | 企業入口網站 |
| 6 | **XtrackWS** | `Xtrack-workspace\xtrackws` | ASP.NET ASMX | 核心 WebService 層 |
| 7 | **XtrackWSLogin** | `Xtrack-workspace\xtrackwslogin` | ASP.NET ASMX | 登入驗證獨立服務 |
| 8 | **cdp** | `JavaWorkspace\cdp` | Spring Cloud 微服務 | 基礎設施平台 (Eureka/网关/认证/监控) |
| 9 | **cdp-framework** | `JavaWorkspace\cdp-framework` | Spring Boot Starter | 共用框架層 |
| 10 | **cdp-ups** | `JavaWorkspace\cdp-ups` | Spring Cloud 微服務 | 統一權限與檔案系統 |
| 11 | **cdp-job** | `JavaWorkspace\cdp-job` | Spring Cloud 微服務 | 分散式任務排程平台 |
| 12 | **cdp-mqs** | `JavaWorkspace\cdp-mqs` | Spring Cloud 微服務 | 訊息佇列與通知中心 |
| 13 | **cdp-cofa-cd** (cdp-cofa-fa) | `JavaWorkspace\cdp-cofa-cd` | Spring Cloud 微服務 | FA 最終組裝線 MES |
| 14 | **cdp-smt-cofa** | `JavaWorkspace\cdp-smt-cofa` | Spring Cloud 微服務 | SMT 表面貼裝線 MES |
| 15 | **cdp-bpo** | `codespace\cdp-bpo` | Spring Cloud 微服務 | 業務流程外包管理 |
| 16 | **cdp-planr** | `codespace\cdp-planr` | Spring Cloud 微服務 | 排程規劃系統 |
| 17 | **cdp-vision-ct** | `codespace\cdp-vision-ct` | Spring Cloud 微服務 | 影像 CT 檢測系統 |
| 18 | **DocSecure** | `codespace\DocSecure` | Spring Boot 單體 | 文件安全管理 |
| 19 | **cdp-radar** | `radarsync-web-master` | Spring Cloud 微服務 | 雷達同步系統 |
| 20 | **hrm-web** | `hrm-web\hrm-web` | Spring Boot 單體 | 人力資源管理 |
| 21 | **xylon-sample** | `codespace\xylon-sample` | Spring Boot Demo | 示範專案集合 |
| 22 | **Xylon-Open-Ecosystem** | `91-SVN` | SVN 歸檔 | Dev_Manual + MinIO + Radar 備份 |
| 23 | **cdp-malloc** | `JavaWorkspace\cdp-malloc` | Spring Cloud 微服務 | 物料分配與 DRP 系統 |
| 24 | **dsc-matrix** | `JavaWorkspace\matrix` | Spring Cloud 微服務 | Matrix 矩陣配置平台 (前身 data-service-center) |
| 📁 | **TestApi** | `TestApi\` | 容器 | Demop + radar 測試 |
| 📁 | **project** | `project\` | 容器 | demo + test 練習 |
| ⚙️ | **xylon-dev-config** | `JavaWorkspace\xylon-dev-config` | SVN 配置中心 | trunk/tags/branches 共用配置 |

---

## 1.5 Xylon / CDP / Xtrack 三者關係

> **一句話：Xylon 是母平台，CDP 是 Java 後端微服務平台，Xtrack 是 .NET 前端品牌。**

```
┌───────────────────────────────────────────────────────────┐
│                    Xylon 平台 (母品牌)                      │
│                   Foxconn iDPBG 事業群                      │
│                                                           │
│  ┌─────────────────────────┐  ┌─────────────────────────┐ │
│  │     Xtrack (.NET)       │  │     CDP (Java)          │ │
│  │     (前端 / WebService)  │  │     (後端微服務平台)      │ │
│  │                         │  │                         │ │
│  │  XtrackWEB  企業入口網站  │  │  cdp       基礎設施平台   │ │
│  │  XtrackWS   15+ ASMX API│  │  cdp-ups   權限/檔案系統  │ │
│  │  APS        排程桌面應用  │  │  cdp-cofa-*  MES 製造   │ │
│  │  SWS-APS    排班桌面應用  │  │  cdp-mqs   訊息佇列     │ │
│  │  XtrackOAuth2Bridge ←───┼──┼─► cdp-job   任務排程     │ │
│  │  XtrackWSLogin          │  │  cdp-bpo/planr/...     │ │
│  │                         │  │  DocSecure 文件安全     │ │
│  │  技術: .NET 4.0, C#     │  │  hrm-web   人資管理     │ │
│  │  DB: XTRACK157/226      │  │                         │ │
│  │  歷史: 較早的系統 (~2012) │  │  技術: Java 8, Spring   │ │
│  │                         │  │  Boot 2.1.6, Spring    │ │
│  │                         │  │  Cloud Greenwich       │ │
│  │                         │  │  歷史: 較新的架構        │ │
│  └─────────────────────────┘  └─────────────────────────┘ │
│                                                           │
│  共用基礎設施: Oracle DB / RabbitMQ / Redis / OAuth2       │
│  橋接層: XtrackOAuth2Bridge (.NET → Java JWT 認證)        │
│  配置中心: xylon-dev-config (SVN)                         │
└───────────────────────────────────────────────────────────┘
```

### 名詞解釋

| 名稱 | 全稱/含義 | 說明 |
|---|---|---|
| **Xylon** | 平台母品牌 | Foxconn iDPBG-RD-SW 團隊的整體系統名稱 |
| **CDP** | Common Data Platform (推測) | Java Spring Cloud 微服務後端，所有 package 皆為 `com.foxconn.cdp.*` |
| **Xtrack** | — | .NET/C# 前端品牌，舊版 ERP/WebService 系統，歷史較久 |

### 程式碼證據

1. **Xylon 是母品牌**
   - `F:\Xylon Server\` 內所有 JAR 都以 `cdp-*` 命名 (cdp-cas, cdp-config, cdp-eureka...)
   - `xylon-dev-config` 是共用配置中心，管理所有 cdp-* 專案的配置
   - `xylon-sample` 示範專案內的 package 為 `com.foxconn.cdp.*`
   - SVN 備份資料夾名為 `Xylon-Open-Ecosystem`
   - XtrackWS Assembly 版權宣告為 `iDPBG-RD-SW`

2. **CDP = Java 後端平台**
   - 所有 Java 專案名稱均為 `cdp-*`
   - 所有 Java package 均為 `com.foxconn.cdp.*`
   - cdp-cofa-cd 內的子模組命名為 `cdp-cofa-fa-*`，表示 CDP 是技術平台名

3. **Xtrack = .NET 前端品牌**
   - C# 專案 Assembly 名均為 `Xtrack*` (XtrackWEB, XtrackWS, XtrackWSLogin...)
   - .NET 端的 Oracle DB 主機命名為 `XTRACK157` / `XTRACK226`
   - `XtrackOAuth2Bridge` 專門橋接 Xtrack (.NET) ↔ CDP (Java)

4. **兩者透過 OAuth2 Bridge 連接**
   - `XtrackOAuth2Bridge` (.NET Library) 在 XtrackWS 的 `JobConfiguration.cs` 中被引用
   - 用於讓舊版 .NET 應用取得 JWT Token 後呼叫 Java 端 API

### 架構演進推測

```
Phase 1 (~2012):  Xtrack .NET 單體
                  ├── XtrackWEB (ASP.NET WebForms)
                  ├── XtrackWS  (ASMX SOAP WebService)
                  └── Oracle XTRACK157/226

Phase 2 (~2018):  引入 Java 後端
                  ├── CDP Spring Cloud 微服務平台
                  ├── XtrackOAuth2Bridge 橋接 .NET ↔ Java
                  └── .NET + Java 共用 Oracle

Phase 3 (現在):    Java 為主力，.NET 逐步淡化
                  ├── 15+ Java 微服務 (cdp-*)
                  ├── .NET 維護模式 (XtrackWEB/XtrackWS)
                  └── DocSecure 獨立於 Eureka 之外
```

---

## 2. 系統整體架構

### 2.1 兩大平台

整個系統分為兩大平台，透過 **OAuth2 Bridge** 連接：

```
┌─────────────────────────────────┐     ┌──────────────────────────────────────┐
│     Xtrack .NET 平台 (C#)        │     │       CDP Java 平台 (Spring Cloud)     │
│                                  │     │                                        │
│  XtrackWEB (ASP.NET WebForms)   │     │   Zuul Gateway (API 网关)              │
│  XtrackWS  (ASMX WebService)    │◄───►│   Eureka (服务注册中心)                 │
│  XtrackWSLogin (登入驗證)        │OAuth2│   Config Server (集中配置)              │
│  APS / SWS-APS (桌面應用)        │Bridge│   CAS/SSO (统一认证)                   │
│  XtrackSDBWS (DB WebService)    │     │   Monitor + Hystrix + Zipkin (监控)    │
│                                  │     │                                        │
│  技術: .NET 4.0, Oracle         │     │  业务系统:                               │
│  DB: XTRACK157/226              │     │  ├─ cdp-ups (权限/文件)                 │
│                                  │     │  ├─ cdp-job (任务调度)                 │
│                                  │     │  ├─ cdp-mqs (消息队列)                 │
│                                  │     │  ├─ cdp-cofa-cd (FA组装)              │
│                                  │     │  ├─ cdp-smt-cofa (SMT贴装)            │
│                                  │     │  ├─ cdp-bpo (BPO外包)                 │
│                                  │     │  ├─ cdp-planr (排程规划)              │
│                                  │     │  ├─ cdp-vision-ct (影像检测)           │
│                                  │     │  ├─ cdp-radar (雷达同步)              │
│                                  │     │  ├─ DocSecure (文档安全)              │
│                                  │     │  └─ hrm-web (人资管理)                │
│                                  │     │                                        │
│                                  │     │  技術: Java 8, Spring Boot 2.1.6      │
│                                  │     │  DB: Oracle, ES, Redis, MinIO         │
└─────────────────────────────────┘     └──────────────────────────────────────┘
```

### 2.2 整體通訊架構圖

```
                      ┌──────────────┐
                      │  外部用戶端    │
                      └──────┬───────┘
                             │ HTTPS
                             ▼
                    ┌─────────────────┐
                    │   Zuul Gateway   │ ← OAuth2/SSO 認證 (Redis Session)
                    │    (入口網關)     │ ← IP 白名單過濾
                    └────────┬────────┘
                             │
                    ┌────────▼────────┐
                    │  CAS / SSO      │ ← 統一認證中心
                    └────────┬────────┘
                             │
              ┌──────────────┼──────────────┐
              │              │              │
              ▼              ▼              ▼
     ┌────────────┐  ┌────────────┐  ┌────────────┐
     │  cdp-ups   │  │  cdp-mqs   │  │ cdp-cofa-* │  ... (各業務微服務)
     └─────┬──────┘  └─────┬──────┘  └─────┬──────┘
           │               │               │
           └───────┬───────┴───────┬───────┘
                   │               │
          ┌────────▼────┐  ┌───────▼───────┐
          │   Oracle    │  │   RabbitMQ    │ ← 非同步訊息
          │  (主要 DB)   │  │  (訊息代理)    │
          └─────────────┘  └───────┬───────┘
                                   │
                          ┌────────▼────────┐
                          │  Elasticsearch  │ ← 日誌搜尋
                          │  Redis (快取)    │ ← Session/快取
                          │  MinIO (物件儲存) │ ← 文件/檔案
                          │  Zipkin/ELK     │ ← 鏈路追蹤/日誌
                          └─────────────────┘
```

### 2.3 通訊機制總表

| 通訊類型 | 機制 | 用途 |
|---|---|---|
| **同步 REST** | OpenFeign + OkHttp (透過 Eureka 解析) | 服務間同步呼叫 |
| **API 網關** | Zuul Gateway | 統一入口、路由、認證 |
| **服務發現** | Eureka | 所有微服務註冊與發現 |
| **非同步訊息** | RabbitMQ (AMQP + Stream Binder + Bus) | 業務事件、配置刷新、指標聚合 |
| **即時推送** | WebSocket (cdp-mqs) | 瀏覽器即時通知 |
| **分散式排程** | XXL-JOB pattern (cdp-job) | Admin → Executor HTTP 排程 |
| **配置管理** | Spring Cloud Config + SVN + RabbitMQ Bus | 集中配置與動態刷新 |
| **分散式追蹤** | Zipkin + Sleuth (經 RabbitMQ 傳輸) | 呼叫鏈追蹤 |
| **斷路器** | Hystrix + Turbine (AMQP 聚合) | 服務保護 |
| **SOAP/XML** | ASP.NET ASMX (.NET 端) | Xtrack 舊系統對外 API |
| **OAuth2 Bridge** | XtrackOAuth2Bridge (.NET Library) | .NET ↔ Java 跨平台認證 |

---

## 3. Xtrack .NET ERP 平台

### 3.1 XtrackWEB — 企業入口網站

| 項目 | 內容 |
|---|---|
| **架構模式** | ASP.NET WebForms 單體應用 (分層: WebForms → FrameWork → DataBaseAccess) |
| **技術棧** | C#, .NET Framework 4.0, Visual Studio 2012, Oracle |
| **資料庫** | Oracle XTRACK157/226，30+ 個 Schema (IPS, LAS, AMS, RADAR, NEUTRON, HERA...) |

**主要功能頁面 (.aspx)**：
- `Agreement.aspx` — 協議簽署
- `ApplyRight.aspx` — 權限申請
- `Register.aspx` / `RegisterResult.aspx` — 註冊
- `OKRSignContent.aspx` — OKR 簽核
- `CoWorkspaceSignContent.aspx` — 協作空間簽核
- `SelectGroup.aspx` — 群組選擇
- `Import.aspx` — 資料匯入
- `JumpPage.aspx` — 頁面跳轉

**代碼結構**：
```
Web/
├── FrameWork.web/          # WebForms 表現層 (.aspx)
├── FrameWork/              # 業務邏輯層
│   ├── Components/
│   │   └── cls_Base.cs     # 抽象基底類 (審計欄位: 建立人/時間/修改人/時間)
│   ├── Config/
│   │   └── XtrackConfiguration.cs
│   └── CacheOnline.cs      # 線上快取
├── DataBaseAccess/         # 資料存取層
│   ├── DBConnectionString.cs  # 集中式連線字串工廠 (30+ Schema)
│   ├── DBType.cs
│   └── SYSNAME.cs          # 子系統枚舉
```

**資料流**：Browser HTTP → WebForms `__doPostBack` → Code-Behind → `DBConnectionString.getConnString(SYSNAME)` → Oracle

---

### 3.2 XtrackWS — 核心 WebService 層

| 項目 | 內容 |
|---|---|
| **架構模式** | SOA (Service-Oriented Architecture)，ASP.NET ASMX Web 服務 |
| **技術棧** | C#, ASP.NET ASMX (SOAP/XML), .NET Framework, Oracle |
| **認證** | OAuth2 (透過 XtrackOAuth2Bridge) + IP 白名單過濾 (`IPFilterHttpModule`) |

**服務端點清單 (.asmx)**：

| 端點 | 子系統 | 功能 |
|---|---|---|
| `NPITS.asmx` | NPITS | 來料檢驗 |
| `EQMS.asmx` | EQMS | 設備品質管理 |
| `IBQS.asmx` | IBQS | 進料品質 |
| `IPS.asmx` | IPS | 進料檢驗 |
| `IT.asmx` | IT | IT 服務 |
| `MailService.asmx` | MailService | 郵件發送 |
| `MS.asmx` | MS | 維護系統 |
| `NEWXSIGN.asmx` | NEWXSIGN | 新版簽核系統 |
| `NMITS.asmx` | NMITS | 新物料檢驗 |
| `NPFAS.asmx` | NPFAS | 物料替代 |
| `OAICS.asmx` | OAICS | OA 整合 |
| `PTS.asmx` | PTS | 生產追蹤 |
| `RADAR.asmx` | RADAR | 雷達資料 |
| `SQAS.asmx` | SQAS | 供應商品質 |
| `SUPS.asmx` | SUPS | 供應商管理 |
| `TCMS.asmx` | TCMS | 測試校正 |
| `UPS/XUPS.asmx` | UPS | 統一權限 (含 OAuth2) |
| `XSIGN.asmx` | XSIGN | 簽核系統 |
| `Validation.asmx` | Validation | 資料驗證 |

---

### 3.3 XtrackOAuth2Bridge — OAuth2 驗證橋接層

| 項目 | 內容 |
|---|---|
| **架構模式** | .NET 類別庫 (DLL)，為舊版 .NET 應用提供 OAuth2 能力 |
| **技術棧** | C#, .NET Framework 4.0, jose-jwt 2.4.0, Newtonsoft.Json 10.0 |

**核心能力** (`OAuth2Util.cs`, 640 行)：
- **密碼授權模式** (`GetTokenObject`) — 使用者名稱/密碼交換 Access Token
- **授權碼模式** (`GetTokenObjectAuthorizationCodeMode`) — 重定向流程
- **Token 刷新** — 支援 Refresh Token
- **Token 驗證** (`CheckAccessToken`) — 向 OAuth2 Provider 驗證 Bearer Token
- **API 呼叫** (`AccessOtherApi`) — 帶 JWT Bearer Token 的 HTTP 請求 (POST/GET/PUT/DELETE)

**資料流**：.NET 端建立 OAuth2Server/Client Config → `GetTokenObject()` HTTP POST → 接收 JWT → `AccessOtherApi()` 攜帶 Bearer Token → 存取 Java 端 API

---

### 3.4 APS / SWS-APS — 排程桌面應用

| 項目 | 內容 |
|---|---|
| **架構模式** | WinForm 桌面應用 (C# .NET Solution) |
| **技術棧** | C#, .NET Framework, Oracle |
| **用戶端** | 工廠現場 Windows PC |

**APS**：WinForm 應用 (Form1.cs, MainForm.cs, LogIn.cs)，含 NPFAS 物料替代匯出模組。

**SWS-APS**：APS 的 SWS 擴展版，含：
- `Core/Components/JobComponent/` — Job 排程引擎 (SWSJobEntityPersoner, SWSEntityDirector)
- `Core/Components/Entity/Emp/` — IWX 員工/部門 API 串接
- `SWSAPSpro/` — 主專案
- `TestSWSAPS/` — 測試專案

---

### 3.5 XtrackSDBWS / XtrackWSLogin

| 專案 | 用途 |
|---|---|
| **XtrackSDBWS** | 統一資料庫 Web Service，集中管理跨子系統的 DB 存取 |
| **XtrackWSLogin** | 獨立登入驗證 WebService，含 IP 過濾 (`AccessableIPSet`) + OAuth2 (`JobConfiguration`) |

---

## 4. CDP 核心平台

### 4.1 cdp — 微服務基礎設施

| 項目 | 內容 |
|---|---|
| **架構模式** | Spring Cloud Netflix 微服務全家桶 |
| **版本** | 2.0.0-RELEASE |
| **技術棧** | Java 8, Spring Boot 2.1.6, Spring Cloud Greenwich.RELEASE |

**服務清單 (20 個可構建模組)**：

| # | 模組 | 角色 | 關鍵依賴 |
|---|---|---|---|
| 1 | `cdp-eureka` | 🔵 服務註冊中心 | Eureka Server, Spring Security |
| 2 | `cdp-zuul-gateway` | 🟢 API 入口網關 | Zuul, OpenFeign/OkHttp, Hystrix, RabbitMQ, OAuth2, Redis, Zipkin |
| 3 | `cdp-gateway` | 🟡 備用網關 | Spring Cloud Gateway (遷移路徑) |
| 4 | `cdp-config` | ⚙️ 集中配置伺服器 | Config Server, SVN Kit, RabbitMQ Bus |
| 5 | `cdp-cas` | 🔐 CAS 認證服務 | CAS Protocol, Eureka Client |
| 6 | `cdp-common` | 📦 共用工具庫 | 被所有模組依賴 |
| 7 | `cdp-aop` | 📐 橫切關注點 | Spring AOP (日誌/審計) |
| 8 | `cdp-sso-token` | 🔑 SSO JWT Token 管理 | java-jwt, spring-security-jwt |
| 9 | `cdp-sso-client` | 🔑 SSO 客戶端庫 | 供下游服務使用 |
| 10 | `cdp-rbac-common-implement` | 🛡️ RBAC 共通實作 | 權限模型 |
| 11 | `cdp-rbac-aop-external` | 🛡️ RBAC 宣告式權限 | `@CdpRbac` 註解, AOP |
| 12 | `cdp-consumer` | 📨 訊息消費者 | RabbitMQ |
| 13 | `cdp-monitor` | 📊 Spring Boot Admin | 健康聚合, Jolokia JMX |
| 14 | `cdp-elasticsearch` | 🔍 ES 整合 | Elasticsearch 6.3.1 (Transport Client) |
| 15 | `cdp-report` | 📈 報表引擎 | POI 4.0.1, Guava |
| 16 | `cdp-up-download` | 📁 檔案上傳/下載 | Commons FileUpload |
| 17 | `cdp-turbine-stream` | 📊 Hystrix 指標聚合 | Turbine + RabbitMQ |
| 18 | `cdp-hystrix-dashboard` | 📊 熔斷器儀表板 | Hystrix Dashboard |
| 19 | `cdp-zipkin-elk` | 🔎 分散式追蹤 | Zipkin + Sleuth + ELK/Logstash |
| 20 | `cdp-file-sharding-transfer` | 📁 檔案分片傳輸 | Netty 4.1.101 |

**請求進入 → 處理 → 儲存的完整路徑**：

```
外部請求
  │
  ▼
cdp-zuul-gateway (OAuth2/SSO 認證 → Redis Session 查詢)
  │
  ├─→ Eureka 服務發現 → 路由到目標微服務
  │
  ▼
目標業務微服務 (如 cdp-cofa-cd-web)
  │
  ├─→ cdp-rbac-aop-external (@CdpRbac 權限檢查)
  ├─→ cdp-sso-token (JWT Token 驗證)
  │
  ▼
Service Layer → Mapper Layer → Oracle (MyBatis)
  │
  ├─→ RabbitMQ (非同步事件: 通知/日誌/資料同步)
  │     └─→ cdp-consumer / cdp-mqs-consumer → Oracle
  │
  ├─→ Elasticsearch (日誌/搜尋索引)
  │
  └─→ Zipkin/Sleuth (呼叫鏈 → RabbitMQ → ELK)
```

**核心依賴關係圖**：
```
cdp-eureka  ◄── (所有微服務註冊)
cdp-config  ◄── (所有微服務拉取配置, SVN 後端, RabbitMQ Bus 刷新)
cdp-zuul-gateway ──► cdp-cas ──► cdp-sso-token
       │                  │
       ▼                  ▼
cdp-rbac-common-implement  cdp-sso-client
       │
       ▼
cdp-rbac-aop-external (@CdpRbac 註解)
```

---

### 4.2 cdp-framework — 共用框架層

| 項目 | 內容 |
|---|---|
| **架構模式** | Spring Boot Starter (不是獨立服務，是共用 Library) |
| **版本** | 2.0.0-RELEASE |
| **模組** | 1 個：`cdp-framework-web` |

**cdp-framework-web 預裝的依賴**：Eureka Client, OpenFeign+OkHttp, Hystrix+Turbine, RabbitMQ (AMQP+Stream+Bus), OAuth2+Redis, Zipkin/Sleuth, Thymeleaf+SpringSecurity5, Swagger 2.9.2, Jasypt, Config Client+Retry。

> 任何業務微服務引入 `cdp-framework-web` 即自動獲得完整的微服務基礎能力。

---

## 5. cdp-ups — 統一權限與檔案系統

| 項目 | 內容 |
|---|---|
| **架構模式** | Spring Cloud 微服務 (分層 + 內外網關分離) |
| **GroupId** | `com.foxconn.ups` (與其他 `com.foxconn.cdp` 不同) |
| **版本** | 2.0.0-RELEASE |
| **特殊依賴** | Elasticsearch 6.3.1 (Transport+REST+Sniffer), Feign-form 3.8.0 |

**服務清單 (12 個模組)**：

| 模組 | 角色 | 說明 |
|---|---|---|
| `cdp-ups-web` | 🌐 主 Web UI | REST API + Thymeleaf UI，含 Elasticsearch 整合 |
| `cdp-ups-core-ws` | 🔵 核心 API | 內部核心 WebService |
| `cdp-ups-internal-ws` | 🔒 內部 API | 內部服務 API 網關 |
| `cdp-ups-external-ws` | 🌍 外部 API | 對外 API 網關 |
| `cdp-ups-service` | 💼 業務邏輯 | ACL 區域管理、應用管理、IP 規則 |
| `cdp-ups-mapper` | 🗄️ 資料存取 | MyBatis Oracle Mapper |
| `cdp-ups-consumer` | 📨 訊息消費 | RabbitMQ 非同步訊息處理 |
| `cdp-ups-job-executor` | ⏰ 排程任務 | Quartz Job |
| `cdp-ups-job-service` | ⏰ 排程業務邏輯 | Job 特定邏輯 |
| `cdp-ups-report-ws` | 📈 報表服務 | 報表 WebService |
| `cdp-ups-echart-ws` | 📊 EChart 圖表 | 視覺化圖表服務 |

**服務間依賴關係**：
```
cdp-ups-web ──► cdp-ups-service ──► cdp-ups-mapper ──► Oracle
     │                │
     ├─► cdp-elasticsearch ──► Elasticsearch 6.3.1
     ├─► cdp-report (POI 報表)
     ├─► cdp-rbac-common-implement (權限)
     └─► cdp-aop (日誌/審計)

cdp-ups-consumer ──► RabbitMQ ──► cdp-ups-service
cdp-ups-core-ws ──► cdp-ups-service
cdp-ups-internal-ws ──► cdp-ups-service
cdp-ups-external-ws ──► cdp-ups-service
```

**通訊方式**：同步 REST (Feign+OkHttp, Eureka) + 非同步 RabbitMQ + Elasticsearch 查詢 (Feign-form 支援多檔案上傳)

---

## 5.5 新平台接入：權限體系與必要串接 (EISP / UPS)

### EISP 是什麼？

**EISP (Enterprise Information Security Platform，企業資訊安全平台)** 是組織部門資料的**上游提供者**。它不是一個本地專案，而是外部系統，負責：

- 提供**官方部門樹結構**：部門名稱、部門代碼 (`EISP Code`)、父部門代碼、部門層級 (1-12)
- 提供**部門主管工號**、**廠區** (`siteName`) 等元資料
- UPS 中的 `Group` 表有一個 `code` 欄位，註釋為 `"部门代号，来自EISP，默认为空"` (`Group.java` 第 42 行)
- **有 EISP 部門代碼的部門禁止手動刪除**："配置部門代碼的部門禁止刪除，數據同步與EISP" (`messages.properties`)

### UPS (Unified Permission System) 權限體系整體架構

```
┌──────────────────────────────────────────────────────────────────────┐
│                       上游資料來源 (外部)                               │
│                                                                       │
│  ┌──────────────────┐      ┌──────────────────────────────┐          │
│  │  EISP            │      │  XUPS (Xtrack 端 SOAP)        │          │
│  │  企業資訊安全平台   │      │  端點: UPSForXUPS.ASMX       │          │
│  │                   │      │  命名空間: tempuri.org        │          │
│  │  提供: 部門樹       │      │                              │          │
│  │  格式: Excel / DB  │      │  提供: User, Role, App,       │          │
│  │                   │      │       Project 等 11 個實體     │          │
│  └────────┬──────────┘      └─────────────┬────────────────┘          │
│           │  Excel 批量匯入                │ SOAP (Axis2)               │
│           │  GroupImportController         │ 11 個 SyncXxxWithXUPS      │
│           ▼                               ▼                           │
│  ┌─────────────────────────────────────────────────────────────────┐ │
│  │                   cdp-ups (UPS 統一權限系統，Java)                  │ │
│  │                                                                   │ │
│  │  核心資料:                                                         │ │
│  │  ├── Group (部門) ─── code 欄位來自 EISP, dataSource 標記來源       │ │
│  │  ├── User (使用者) ─── 含 EMP_NO (工號)、角色關聯                   │ │
│  │  ├── Role (角色) ─── 含 role_code                                  │ │
│  │  ├── Application / SubApplication (應用/子應用註冊)                 │ │
│  │  ├── Project / Milestone (專案/里程碑)                              │ │
│  │  ├── ACL (Area / IP / Rule) ─── 區域訪問控制                        │ │
│  │  └── OAuthClientDetails ─── OAuth2 客戶端註冊                       │ │
│  └─────────────┬───────────────────────────────────────────────────┘ │
│                │                                                      │
│   ┌────────────┼────────────┬──────────────────┐                     │
│   ▼            ▼            ▼                  ▼                     │
│ ┌──────┐ ┌──────────┐ ┌───────────┐ ┌──────────────────┐            │
│ │ CAS/ │ │RabbitMQ  │ │ Internal  │ │ OAuth2           │            │
│ │ SSO  │ │廣播通知    │ │ WS API    │ │ Client 管理       │            │
│ │ 認證  │ │          │ │           │ │                  │            │
│ │      │ │GROUP_    │ │查詢部門樹  │ │client_id/secret  │            │
│ │loadBy│ │BROADCAST │ │查詢使用者  │ │授權類型/scope    │            │
│ │User- │ │ROUTING   │ │查詢角色   │ │                  │            │
│ │Name- │ │KEY       │ │           │ │                  │            │
│ │ToCas │ │          │ │           │ │                  │            │
│ └──────┘ └──────────┘ └───────────┘ └──────────────────┘            │
│   ▲                                                                  │
│   │                        下游消費端 (所有業務微服務)                   │
│   ├── cdp-cofa-cd (FA MES)                                           │
│   ├── cdp-smt-cofa (SMT MES)                                         │
│   ├── cdp-bpo (BPO)                                                  │
│   ├── cdp-planr (排程規劃)                                            │
│   ├── cdp-vision-ct (影像檢測)                                        │
│   ├── cdp-radar (雷達同步)                                            │
│   ├── DocSecure (文件安全)                                            │
│   └── hrm-web (人資) ─── 及任何新平台                                  │
└──────────────────────────────────────────────────────────────────────┘
```

### 11 個 SOAP 資料同步實體

UPS 從 XUPS (Xtrack 端) 透過 Axis2 SOAP 同步以下 11 個實體。每個實體都有對應的 `Sync*ServiceImpl` (位於 `cdp-ups-job-service`) 與 `*JobHandler` (位於 `cdp-ups-job-executor`)。同步參數由**單一** `SyncParameterController` (`/sync-param`) 管理，而非每個實體一個 Controller。

| # | 實體 | SOAP 方法 | 用途 |
|---|---|---|---|
| 1 | **Group** | `SyncGroupWithXUPS` | 部門同步 (來源 EISP，含 EISP Code、父部門代碼) |
| 2 | **User** | `SyncUserWithXUPS` | 使用者同步 (含工號、部門關聯) |
| 3 | **Application** | `SyncApplicationWithXUPS` | 應用系統註冊 (每個平台註冊一個 Application) |
| 4 | **SubApplication** | `SyncSubApplicationWithXUPS` | 子應用/模組註冊 |
| 5 | **Project** | `SyncProjectWithXUPS` | 專案註冊 |
| 6 | **Milestone** | `SyncMilestoneWithXUPS` | 里程碑註冊 |
| 7 | **ApplicationProject** | `SyncApplicationProjectWithXUPS` | 應用-專案關聯 |
| 8 | **Role** | `SyncRoleWithXUPS` | 角色定義 (含 role_code) |
| 9 | **RoleApplication** | `SyncRoleApplicationWithXUPS` | 角色-應用關聯 (角色對哪些應用有權限) |
| 10 | **RoleSubApplication** | `SyncRoleSubApplicationWithXUPS` | 角色-子應用關聯 |
| 11 | **RoleUserProject** | `SyncRoleUserProjectWithXUPS` | 角色-使用者-專案三方關聯 |

**同步模式統一為**：定時任務觸發 → JobHandler 分頁查詢 SOAP → 解析 JSON → 對比 DB 既有資料 → 新增/更新 → 清除 Redis 快取 → 記錄同步狀態

**SOAP 端點外部化**：SOAP 端點 URL、方法名、命名空間等值**不在本 repo**，由 Spring Cloud Config Server (`cdp-config`) 注入 `ups.ups-sync-data-uri` / `ups.ups-sync-data-code` 配置（`UpsConfig` + `SOAPURI` 綁定類別）。JobHandler 透過 `upsConfig.getUpsSyncDataUri().getGroup().getMethod()` 取得方法名，端點 URL 優先取 DB `SyncParameter.url`，否則用 `defaultSetting.getEndPointUrl()`。

### 新平台接入必須完成的對接清單

| # | 對接項目 | 必須 | 說明 |
|---|---|---|---|
| 1 | **OAuth2 Client 註冊** | ✅ 必須 | 在 UPS Web 管理介面/API (`/oauth-client` CRUD) 註冊 `clientId`、`clientSecret`、`authorizedGrantTypes`、`scopes`（對應 `OAuthClientDetailsController`） |
| 2 | **CAS/SSO 認證串接** | ✅ 必須 | 新平台 SSO 需指向 CAS 伺服器 (`cdp-cas`)，CAS 會呼叫 UPS 的 `GET /user/loadByUserNameForCas/{userName}` 取得使用者資訊與角色 (含 `roleList`) |
| 3 | **Application 註冊** | ✅ 必須 | 在 UPS 中註冊一個新的 Application 記錄 (對應 `SyncApplicationWithXUPS`) |
| 4 | **Role 角色定義** | ✅ 必須 | 在 UPS 中定義角色並關聯到 Application，才能使用 `@CdpRbac` 做權限檢查 |
| 5 | **cdp-sso-token 依賴** | ✅ 必須 | pom.xml 引入 `cdp-sso-token` (JWT Token 驗證) |
| 6 | **cdp-rbac-common-implement 依賴** | ✅ 必須 | pom.xml 引入 RBAC 權限檢查基礎實作 |
| 7 | **cdp-rbac-aop-external 依賴** | ✅ 必須 | pom.xml 引入 `@CdpRbac` 註解式權限控制 |
| 8 | **Eureka Client 註冊** | ✅ 必須 | 新微服務需註冊到 `cdp-eureka`，配置 `spring.application.name` |
| 9 | **cdp-framework-web 依賴** | 強烈建議 | 引入即自動獲得：Eureka Client + Feign/OkHttp + Hystrix + Zipkin + OAuth2 + Redis + Swagger + Config Client |
| 10 | **cdp-common 依賴** | 強烈建議 | 共用工具庫 |
| 11 | **xylon-dev-config 配置** | 建議 | 在 `F:\JavaWorkspace\xylon-dev-config` SVN 中新增 `application-{site}-{env}.yml` |
| 12 | **RabbitMQ Bus 監聽** | 建議 | 監聽 `ups.group.object.broadcast.message` 路由鍵，接收部門變更即時通知 (UPS.GROUP_BROADCAST) |
| 13 | **Spring Cloud Config Client** | 建議 | 從 `cdp-config` (SVN 後端) 拉取集中配置 |
| 14 | **cdp-mqs 依賴** | 可選 | 如需 WebSocket 即時推送或訊息通知功能 |
| 15 | **cdp-aop 依賴** | 可選 | 統一審計日誌 |

### 新平台完整接入 SOP (Standard Operating Procedure)

```
步驟 1: 基礎設施準備
  ├── 在 cdp-eureka 確認新服務名稱可用
  ├── 在 xylon-dev-config SVN 建立 {app}-{site}-{env}.yml 配置
  └── 確認 Oracle 連線權限、Redis、RabbitMQ 可達

步驟 2: 平台註冊 (在 UPS 管理後台操作)
  ├── 註冊 Application (應用系統)
  ├── 註冊 SubApplication (子模組，如 web/consumer/job-executor)
  ├── 註冊 Role (角色，如 ADMIN / USER / VIEWER)
  ├── 建立 Role-Application 關聯
  └── 建立 Role-SubApplication 關聯

步驟 3: Maven 依賴配置 (pom.xml)
  ├── 繼承 cdp-framework (或手動引入 cdp-framework-web)
  ├── 引入 cdp-sso-token
  ├── 引入 cdp-rbac-common-implement
  ├── 引入 cdp-rbac-aop-external
  └── 引入 cdp-common

步驟 4: Spring Boot 配置
  ├── @SpringBootApplication + @ComponentScan + @MapperScan
  ├── @EnableEurekaClient (或自動)
  ├── @EnableFeignClients
  ├── @EnableCircuitBreaker (Hystrix)
  └── application.yml: eureka.client.serviceUrl, spring.cloud.config, spring.redis, spring.rabbitmq

步驟 5: 安全配置
  ├── 配置 Spring Security + OAuth2 Resource Server
  ├── 在需要權限的 Controller 方法加上 @CdpRbac 註解 (框架註解，token 如 ups_app_*)
  └── 配置 Redis Token Store 連線

步驟 6: OAuth2 Client 註冊 (透過 UPS Web/API)
  ├── 呼叫 UPS 的 /oauth-client/add API (OAuthClientDetailsController)
  ├── 設定 client_id, client_secret, authorized_grant_types, scopes
  ├── 系統 client 受保護 (CDPDefaultConfig.getSystemOauthClientIds())
  └── 實際 Token 核發/驗證由 cdp-cas-server 負責，非 UPS 直接核發

步驟 7: 使用者/權限同步 (如新平台有自己的使用者來源)
  ├── 參考現有 SyncXxxServiceImpl 模式 (cdp-ups-job-service)
  ├── 透過 /sync-param API 建立 SyncParameter (sync_code, URL, sync_enable)
  ├── 在 cdp-ups-job-executor 建立 JobHandler (@JobHandler 註解)
  ├── SOAP 端點值由 Config Server 注入 (ups.ups-sync-data-uri)
  └── 排定 Cron 定時同步

步驟 8: 驗證
  ├── Eureka Dashboard 確認服務已註冊
  ├── Hystrix Dashboard 確認熔斷器正常
  ├── Zipkin 確認呼叫鏈追蹤正常
  └── 測試 SSO 登入 + RBAC 權限檢查
```

### 現有同步模式的程式碼參考

**SOAP 同步 JobHandler 模式** (以 Group 為例，位於 `cdp-ups-job-executor`)：

```java
@JobHandler(value = "upsSyncGroupFromUPS2XUPSJobHandler")
@Component
public class GroupJobHandler extends IJobHandler {
    // 1. 從 DB 取得 SyncParameter (sync_code, URL, lastSyncAt)
    SyncParameter sp = jobSyncParameterService.getSyncParameterByCode(syncCode);
    
    // 2. 構建 SOAP 請求 (Axis2RPCParameter: URL, namespace, method)
    // 3. 呼叫 syncGroupService.sync() → SOAP → JSON → 對比 DB → update/insert
    // 4. 記錄同步狀態 (成功/失敗/時間戳)
}
```

**RabbitMQ 廣播模式** (部門變更通知，位於 `cdp-ups-job-executor`)：

```java
// 查詢 SYS_GROUP 變更日誌 → 轉換為 GroupForSyncDTO →
//   CdpMessageInfo 設定 routingKey=UPS.GROUP_BROADCAST → messageServiceFeign.send()
// → 所有監聽此 routing key 的微服務收到通知
```

### 實際權限管控範例（其他專案程式碼）

> 新站台除了註冊 Application/Role（見上方 SOP），實際「菜單/功能權限管控」可分四層。以下為 cdp-ups、cdp-malloc、cdp-cofa-cd 的真實範例程式碼。

```
權限管控完整流程：
登入 (cdp-cas) → 平台寫入 #cdp-user# + #cdp-all-app-user-setting#
    → 菜單樹: GET /module/menu/list/level/{userId}/{appId}/{projectId} (cdp-ups)
        → ModuleMapper SQL (SYS_USERROLE → SYS_ROLEMODULE → SYS_MODULE)
        → ModuleServiceImpl 合併 + IP 區域過濾
    → 功能權限: @CdpRbac(acToken="ups_*") 於 controller method
    → 前端按鈕: 呼叫 /account-role/{projectId} role map → v-if="someRole"
    → 當前使用者: AuthenticationFacade.getCDPUserDetails() → UserDetailsImpl
```

#### 5.5.1 後端菜單權限管控（cdp-ups internal-ws）

**端點**：`F:\JavaWorkspace\cdp-ups\cdp-ups-internal-ws\...\controller\ModuleController.java`

```java
// 回傳目前使用者有權限看到的菜單樹（登入平台外殼呼叫）
@GetMapping("/menu/list/level/{userId}/{appId}/{projectId}")
public R<List<Module>> listModulesByUAPIdForUI(@PathVariable Long userId,
                                               @PathVariable Long appId,
                                               @PathVariable Long projectId) {
    List<Module> listModule = moduleService.listModulesByUAPIdForUI(userId, appId, projectId);
    return new R<>(listModule);
}
```

**Service 層**：`F:\JavaWorkspace\cdp-ups\cdp-ups-service\...\service\impl\ModuleServiceImpl.java`

```java
// 合併父子菜單 + IP 區域等級過濾（非權限範圍的菜單隱藏）
@Override
public List<Module> listModulesByUAPIdForUI(Long userId, Long appId, Long projectId, String ip) {
    IPControlLevel ipControlLevelFromCache = ipControlLevelService.getIpControlLevelFromCache(ip);
    List<Module> moduleList = moduleMapper.listModulesByUAPId(userId, appId, projectId);
    moduleList.removeIf(module -> {
        if (ignoreIpLevelControlMenu.contains(module.getApplication().getApplicationId())) {
            return false;
        } else if (null != module.getAllowedAreas() && module.getAllowedAreas() > 0) {
            if (ipControlLevelFromCache != null && ipControlLevelFromCache.getIpLevel() != null) {
                return (module.getAllowedAreas() & ipLevelEnum.getValue()) == 0; // IP 不符 → 隱藏菜單
            }
        }
        return false;
    });
    return mergeModuleList(moduleList);
}
```

**SQL 層**：`F:\JavaWorkspace\cdp-ups\cdp-ups-mapper\...\mapper\ModuleMapper.xml` — 核心 RBAC join

```sql
-- 決定使用者可見菜單：使用者角色 → 角色-模組 → (可選)專案
AND EXISTS (
    SELECT 1
    FROM SYS_ROLEMODULE SRM_1_1
    JOIN SYS_MODULE SM_1_1 ON SM_1_1.FMODULEID = SRM_1_1.FMODULEID
    JOIN SYS_USERROLE SUR_1_1 ON SUR_1_1.FROLEID = SRM_1_1.FROLEID
    JOIN SYS_USERROLEPROJECT SURP_1 ON SURP_1.FUSERROLEID = SUR_1_1.FUSERROLEID
    WHERE SUR_1_1.FUSERID = #{userId}
          AND SM_1_1.FAPPLICATIONID = #{appId}
          AND (SM.FMODULEID = SM_1_1.FPARENTID OR SM.FMODULEID = SM_1_1.FMODULEID)
          AND SM_1_1.FDFLAG = 'N' AND SM_1_1.FISCLOSE = 'N' AND SM_1_1.FISSHOW = 'Y'
          AND SURP_1.FPROJECTID = #{projectId}  -- 專案型應用才限制
)
```

#### 5.5.2 後端功能權限管控（@CdpRbac 註解）

> 每個 Controller action 以 `@CdpRbac(acToken = "...")` 宣告所需權限 token。token 對應 `SYS_URI` 表的記錄，AOP 攔截器在使用者呼叫前檢查其角色是否含該 token。

**範例 1**：`F:\JavaWorkspace\cdp-ups\cdp-ups-web\...\controller\ModuleController.java`

```java
@GetMapping("/load/{id}")
@CdpRbac(acToken = "ups_module_load")
public R<Module> load(@PathVariable Long id) {
    Module module = moduleService.load(id);
    return new R<>(module);
}

@GetMapping("/tree/listModuleVos/{applicationId}")
@CdpRbac(acToken = "ups_module_list")
public R<List<ModuleVo>> listModuleVoByApplicationId(@PathVariable Long applicationId) { ... }
```

**範例 2**：`F:\JavaWorkspace\cdp-ups\cdp-ups-web\...\controller\UriController.java`

```java
@GetMapping("/load/{id}")
@CdpRbac(acToken = "ups_uri_load")
public R<Uri> load(@PathVariable Long id) { ... }

@PostMapping("/add")
@CdpRbac(acToken = "ups_uri_add")
public R<Boolean> add(@RequestBody Uri uri) { ... }
```

**Token 對應機制**：`@CdpRbac` 的 `acToken`（如 `ups_module_load`、`ups_uri_add`）即為 `SYS_URI` 表的功能代碼。新站台需在 UPS 註冊 URI + 綁定到 Role，才能使用。

#### 5.5.3 前端菜單/按鈕權限（cdp-malloc-ui）

**讀取目前使用者設定**：`F:\JavaWorkspace\cdp-malloc\cdp-malloc-ui\src\api\utils.js`

```js
// 登入後平台寫入 sessionStorage：使用者資訊 + 授權 app/project/milestone
export const getLoginUser = () => {
    let user = JSON.parse(window.sessionStorage.getItem('#cdp-user#'));
    if (user) { return user; }
    else { return { id: 198319, loginName: "F1241683", nameCN: "Kasa", nameEN: "Kasa" }; }
};
export const getProjectAndMilestone = () => {
    let setting = JSON.parse(window.sessionStorage.getItem('#cdp-all-app-user-setting#'));
    if (setting) {
        for (var i = 0; i < setting.length; ++i) {
            if (setting[i].applicationName == "Malloc") {
                return { projectId: setting[i].projectId, milestoneId: setting[i].milestoneId };
            }
        }
    }
    ...defaults...
};
```

**頁面載入時抓 role flag**：`F:\JavaWorkspace\cdp-malloc\cdp-malloc-ui\src\pages\confirm-receipt\App.vue`

```js
created() {
    let projectAndMilestone = getProjectAndMilestone();
    loadData("/malloc/confirm-receipt/account-role/" + projectAndMilestone.projectId)
        .then((res) => {
            let role = res.data;
            if (role && role.malloc_inventory_account_role) { this.inventoryAccountRole = true; }
            if (role && role.malloc_unit_account_role) { this.unitAccountRole = true; }
        });
},
data() { return { inventoryAccountRole: false, unitAccountRole: false, ... } }
```

**按鈕依權限顯示**：`F:\JavaWorkspace\cdp-malloc\cdp-malloc-ui\src\pages\confirm-receipt\ListView.vue`

```html
<!-- 權限 flag 為 true 才顯示按鈕 -->
<vxe-button
    v-if="unitAccountRole && row.issueMaterialStatus == 2"
    type="text" status="primary" @click="accountConfirm(row)">
    {{$t('confirmReceipt.props.PeripheryFinancialVerification')}}
</vxe-button>
<vxe-button
    v-if="inventoryAccountRole && row.issueMaterialStatus == 3"
    type="text" status="warning" @click="transferOrder(row)" style="color: #FF9900">
    {{$t('confirmReceipt.props.Generatetransferorder')}}
</vxe-button>
```

**另一種前端權限檢查**（cdp-cofa-cd BI Yield UI）：`...cdp-cofa-fa-bi-app-yield-ui\src\pages\index\requestData.js`

```js
// 先檢查權限 boolean，通過才載入搜尋條件資料
getListData("/cofa-fa-bi-app-yield/query/user-role/" + userInfo.userNO + "/33", {})
    .then((userRoleRes) => {
        if (userRoleRes.code == 200) {
            this.havePermission = userRoleRes.data;
            if (this.havePermission) {
                getListData("/cofa-fa-bi-app-yield/query/get-search-condition/" + userInfo.userNO, {}).then(...);
            } else {
                Notify("没权限，请先在Xylon上申请权限");
            }
        }
    });
```

#### 5.5.4 後端注入 AuthenticationFacade 取得目前使用者

**共用基底類**：`F:\JavaWorkspace\cdp-cofa-cd\cdp-cofa-fa-web\...\controller\base\BaseController.java`

```java
public class BaseController {
    @Autowired
    protected AuthenticationFacade auth;   // 注入權限門面
    @Value("${cdp.default.op.user.id:1001}")
    protected Long defaultUserId;           // 系統呼叫 fallback
    @Value("${cdp.default.op.user.name:ADMIN}")
    protected String defaultUserName;
}
```

**取得目前使用者（含 fallback）**：`F:\JavaWorkspace\cdp-cofa-cd\cdp-cofa-fa-web\...\controller\AppIssueCategoryController.java`

```java
if (auth != null) {
    if (auth.getCDPUserDetails() instanceof UserDetailsImpl) {
        UserDetailsImpl userDetail = (UserDetailsImpl) (auth.getCDPUserDetails());
        dto.setCreatorId(userDetail.getUserId());    // 審計欄位：建立人
        dto.setCreator(userDetail.getNameCN());
    } else {
        dto.setCreatorId(defaultUserId);             // 非登入狀態 fallback
        dto.setCreator(defaultNameCN);
    }
}
```

**直接 cast + 功能權限檢查**：`F:\JavaWorkspace\cdp-malloc\cdp-malloc-server\...\controller\ShortageHandlingController.java`

```java
// 取得目前使用者
if (auth.getCDPUserDetails() != null) {
    UserDetailsImpl userDetail = (UserDetailsImpl) (auth.getCDPUserDetails());
    userId = userDetail.getUserId();
}
// 以多個功能 token 檢查權限 → 回傳 Map<acToken, Boolean> 給前端
UserRBACValidationResult permission = permissionUtil.getPermission(projectId, new String[]{
    "malloc_shortage_handling_agree_button",
    "malloc_shortage_handling_cancel_button"
}, AreaProjectAcl.VIEW_AC + AreaProjectAcl.UPLOAD_AC + AreaProjectAcl.DOWN_AC, userId, request);
```

#### 5.5.5 新站台權限管控 SOP 重點（對照上方範例）

| 層 | 作法 | 對應範例 |
|---|---|---|
| **菜單** | 在 UPS 註冊 Module + 綁定 Role → `GET /menu/list/level` 回傳菜單樹 | §5.5.1 |
| **功能** | 在 UPS 註冊 URI/acToken → controller method 加 `@CdpRbac(acToken)` | §5.5.2 |
| **前端按鈕** | 提供 `/account-role/{projectId}` role map API → 前端 `v-if` | §5.5.3 |
| **取得使用者** | 注入 `AuthenticationFacade` → `getCDPUserDetails()` cast `UserDetailsImpl` | §5.5.4 |
| **專案限定** | role map / ModuleMapper SQL 帶 projectId 過濾 | §5.5.1 + §5.5.3 |

> ⚠️ **重點**：菜單與功能權限的**基礎資料都在 UPS**（`SYS_MODULE`、`SYS_URI`、`SYS_ROLEMODULE`）。新站台必須先在 UPS 註冊這些資源並綁定到 Role，後端才能用 `@CdpRbac`、前端才能拿到 role map。`AuthenticationFacade` 與 `UserDetailsImpl` 來自 `cdp-common`/`cdp-aop` 依賴（見 SOP 步驟 3）。

---



## 6. cdp-job — 分散式任務排程平台

| 項目 | 內容 |
|---|---|
| **架構模式** | XXL-JOB 模式的 Admin-Executor 分散式任務系統 |
| **版本** | 2.0.0-RELEASE |
| **特殊技術** | Quartz 2.3.1, Groovy 2.5.6, Commons Exec, Freemarker (Admin UI) |

**服務清單 (8 個模組)**：

| 模組 | 角色 | 說明 |
|---|---|---|
| `cdp-job-admin` | 🖥️ 排程管理後台 | Web UI (Freemarker), Cron 表達式管理, 任務分發 |
| `cdp-job-executor` | ⚡ 任務執行節點 | Undertow (替代 Tomcat), 執行 Admin 分配的任務 |
| `cdp-job-core` | 🧠 核心引擎 | Quartz 排程, Groovy 腳本執行, 共用邏輯 |
| `cdp-job-service` | 💼 業務邏輯 | 任務業務層 |
| `cdp-job-mapper` | 🗄️ 資料存取 | MyBatis Oracle |
| `cdp-job-pojo` | 📦 資料物件 | DTO/Entity |
| `cdp-job-external-ws` | 🌍 外部 API | 對外 WebService |
| `cdp-job-api-demo` | 📘 API 範例 | LoginController, ApiController 示範 |

**Admin ↔ Executor 通訊架構**：
```
cdp-job-admin (Web UI, Eureka Client)
     │
     │ HTTP 排程 (透過 Feign/OkHttp)
     ▼
cdp-job-executor (任務執行節點, Undertow)
     │
     ├─► CommandJobHandler (命令列任務)
     ├─► HttpJobHandler (HTTP 任務)
     └─► DemoJobHandler (示範任務)
     │
     ▼
Oracle (MyBatis): 任務定義、執行日誌
Redis: Session
```

**依賴鏈**：`cdp-job-admin` → `cdp-job-mapper` → `cdp-job-pojo` + `cdp-job-core` + `cdp-job-service` + CDP 平台 (`cdp-rbac-common-implement`, `cdp-sso-token`)

---

## 7. cdp-mqs — 訊息佇列與通知中心

| 項目 | 內容 |
|---|---|
| **架構模式** | Spring Cloud 微服務，專用的訊息傳遞平台 |
| **版本** | 1.0.0-RELEASE |
| **核心能力** | RabbitMQ 訊息收發、WebSocket 即時推送、使用者待辦/通知 |

**服務清單 (14 個模組)**：

| 模組 | 角色 | 說明 |
|---|---|---|
| `cdp-mqs-web` | 🌐 訊息管理 Web UI | Thymeleaf + REST API, Spring Security 5 |
| `cdp-mqs-consumer` | 📨 訊息消費者 | RabbitMQ Consumer, 獨立部署 |
| `cdp-mqs-websocket` | 🔌 即時推送 | WebSocket 瀏覽器推送 |
| `cdp-mqs-internal-ws` | 🔒 內部 API | MessageController, MessageGroupController |
| `cdp-mqs-user-todo-ws` | 📋 使用者待辦 | 待辦事項 WebService |
| `cdp-mqs-user-queue-filter-ws` | 🔍 佇列過濾 | 使用者訊息過濾 |
| `cdp-mqs-forward-service` | 🔄 訊息轉發 | 跨服務訊息路由 |
| `cdp-mqs-rabbitmq-sender-service` | 📤 訊息發送 | RabbitMQ 生產者抽象層 |
| `cdp-mqs-report-consumer-template` | 📈 報表消費模板 | 報表專用消費模板 |
| `cdp-mqs-job-executor` | ⏰ 排程任務 | MQS 相關 Job |
| `cdp-mqs-service` | 💼 業務邏輯 | 訊息業務層 |
| `cdp-mqs-mapper` | 🗄️ 資料存取 | MyBatis Oracle |
| `cdp-mqs-pojo` | 📦 資料物件 | DTO/Entity |
| `cdp-mqs-common` | 📦 共用工具 | MQS 內部共用 |

**訊息流架構**：
```
業務事件
  │
  ▼
cdp-mqs-web (REST API, OAuth2/SSO + Redis Session)
  │
  ▼
cdp-mqs-rabbitmq-sender-service (訊息生產者)
  │
  ▼
RabbitMQ Broker
  │
  ├─► cdp-mqs-consumer (消費 → MyBatis → Oracle)
  │     └─► cdp-mqs-websocket → Browser 即時推送
  │
  ├─► cdp-mqs-forward-service (轉發至其他服務)
  │
  └─► cdp-mqs-report-consumer-template (報表消費)
  │
  ▼
Zipkin/Sleuth → ELK (呼叫鏈追蹤)
```

---

## 8. cdp-cofa-cd (cdp-cofa-fa) — FA 最終組裝線 MES

| 項目 | 內容 |
|---|---|
| **架構模式** | Spring Cloud 微服務 (分層: common→pojo→mapper→service→web/consumer) |
| **版本** | 1.0.0-RELEASE |
| **子模組數** | 28 個 (全系統最大) |

**完整服務清單**：

| 層 | 模組 | 說明 |
|---|---|---|
| **基礎** | `cdp-cofa-fa-common` | 共用工具/常數 |
| | `cdp-cofa-fa-pojo` | DTO/Entity |
| **資料** | `cdp-cofa-fa-mapper` | MyBatis (主 DB) |
| | `cdp-cofa-fa-bobcat-mapper` | MyBatis (Bobcat DB) |
| | `cdp-cofa-fa-xbobcat-mapper` | MyBatis (XBobcat DB) |
| | `cdp-cofa-fa-radar-mapper` | MyBatis (Radar DB) |
| | `cdp-cofa-fa-data-sync-mapper` | MyBatis (DataSync DB) |
| **業務** | `cdp-cofa-fa-service` | FA 業務邏輯 |
| | `cdp-cofa-fa-bobcat-service` | Bobcat 整合服務 |
| | `cdp-cofa-fa-radar-service` | Radar 分析服務 |
| | `cdp-cofa-fa-data-sync-service` | 資料同步邏輯 |
| **運行** | `cdp-cofa-fa-web` | 🌐 REST API + Thymeleaf UI (COFAFAWebApp) |
| | `cdp-cofa-fa-consumer` | 📨 RabbitMQ Consumer (COFAConsumerApplication) |
| **排程** | `cdp-cofa-fa-job-executor` | Quartz 定時任務 |
| | `cdp-cofa-fa-data-sync-job-executor` | 資料同步排程 |
| **內部** | `cdp-cofa-fa-data-sync-internal-ws` | 資料同步內部 API |
| **報表** | `cdp-cofa-fa-report-ws` | 通用報表 |
| | `cdp-cofa-fa-yield-report-ws` | BI Yield 良率報表 |
| | `cdp-cofa-fa-unit-tracking-report-ws` | 單位追蹤報表 |
| | `cdp-cofa-fa-wip-report-ws` | WIP 在製品報表 |
| | `cdp-cofa-fa-common-data-report-ws` | 共通資料報表 |
| **專業** | `cdp-cofa-fa-material-web` | 物料管理 Web |
| | `cdp-cofa-fa-material-ui` | 物料管理 UI |
| | `cdp-cofa-fa-test-rule-web` | 測試規則 Web |
| | `cdp-cofa-fa-test-rule-ui` | 測試規則 UI |
| | `cdp-cofa-fa-test-rule-external-ws` | 測試規則外部 API |
| | `cdp-cofa-fa-radar-web` | Radar 視覺化 Web |
| | `cdp-cofa-fa-radar-ui` | Radar 視覺化 UI |
| | `cdp-cofa-fa-dashboard-web` | Dashboard Web |
| | `cdp-cofa-fa-dashboard-ui` | Dashboard UI |
| | `cdp-cofa-fa-f1-app-api` | F1 App API |
| | `cdp-cofa-fa-wiki-sync-ws` | Wiki 同步 WebService |
| | `cdp-cofa-fa-bi-app-yield-*` | BI Yield App (外部+IWX+UI+Web, 4 模組) |

**服務間依賴關係**：
```
cdp-cofa-fa-web (入口)
  ├─► cdp-cofa-fa-service
  │     ├─► cdp-cofa-fa-mapper → Oracle (主 DB)
  │     ├─► cdp-cofa-fa-bobcat-mapper → Bobcat DB
  │     ├─► cdp-cofa-fa-xbobcat-mapper → XBobcat DB
  │     └─► cdp-cofa-fa-radar-mapper → Radar DB
  ├─► cdp-cofa-fa-data-sync-service → cdp-cofa-fa-data-sync-mapper
  ├─► cdp-sso-token (CDP 平台 SSO)
  ├─► cdp-rbac-common-implement (CDP 平台 RBAC)
  └─► cdp-mqs-rabbitmq-sender-service (非同步訊息)

cdp-cofa-fa-consumer (RabbitMQ Consumer)
  ├─► cdp-cofa-fa-service → Mapper → Oracle
  └─► cdp-mqs-service

cdp-cofa-fa-data-sync-job-executor (定時同步)
  └─► cdp-cofa-fa-data-sync-internal-ws → data-sync-service → Oracle
```

**資料流向**：
```
外部請求 → cdp-cofa-fa-web (REST Controller) → service → mapper → Oracle
非同步訊息 → cdp-cofa-fa-consumer → service → mapper → Oracle
定時任務 → cdp-cofa-fa-data-sync-job-executor → data-sync-internal-ws → Oracle
```

---

## 9. cdp-smt-cofa — SMT 表面貼裝線 MES

| 項目 | 內容 |
|---|---|
| **架構模式** | 與 cdp-cofa-fa 完全相同的分層微服務架構，Domain 為 SMT |
| **版本** | 1.0.0-RELEASE |
| **子模組數** | 27 個 |

**與 FA 的差異**：
- 無 `material-ui/web`、`dashboard-ui/web`、`f1-app-api`、`wiki-sync-ws`、`bi-app-yield-*`
- 新增 `cdp-smt-cof-xmlb-pojo`、`cdp-smt-cofa-xmlb-mapper` (XMLB 格式資料對接)
- 新增 `cdp-smt-cofa-test-station-report-ws`、`cdp-smt-cofa-test-summary-report-ws`
- 無 ClassFinal JAR 加密

**服務間依賴與通訊方式與 FA 完全一致**，唯 Domain 從 `com.foxconn.cdp.cofa.fa` 改為 `com.foxconn.cdp.smt.cofa`。

---

## 10. cdp-bpo — 業務流程外包管理

| 項目 | 內容 |
|---|---|
| **架構模式** | Spring Cloud 微服務 (9 模組，比 COFA 精簡) |
| **版本** | 1.0.0-RELEASE |
| **特殊依賴** | Netty 4.1.22 (直接網路 I/O), cdp-das-pojo (資料分析服務整合) |

**服務清單**：

| 模組 | 角色 |
|---|---|
| `cdp-bpo-web` | 🌐 REST API + Thymeleaf UI (Attachment, Audit, BdAudit, AutoSummary Controllers) |
| `cdp-bpo-consumer` | 📨 RabbitMQ Consumer |
| `cdp-bpo-external-ws` | 🌍 外部 API 網關 |
| `cdp-bpo-job-executor` | ⏰ 排程任務 (含 SSO Token 取得) |
| `cdp-bpo-service` | 💼 業務邏輯 |
| `cdp-bpo-mapper` | 🗄️ MyBatis |
| `cdp-bpo-pojo` | 📦 DTO/Entity |
| `cdp-bpo-common` | 📦 共用工具 |
| `cdp-bpo-report` | 📈 報表 |

**通訊方式**：同步 REST (Feign+OkHttp, Eureka) + 非同步 RabbitMQ (cdp-mqs-rabbitmq-sender-service)

---

## 11. cdp-planr — 排程規劃系統

| 項目 | 內容 |
|---|---|
| **架構模式** | Spring Cloud 微服務 (7 模組，全系統最精簡) |
| **版本** | 1.0.0-RELEASE |
| **特殊依賴** | **PostgreSQL 42.7.3** (唯一同時支援 Oracle + PostgreSQL 的專案) |

**服務清單**：

| 模組 | 角色 |
|---|---|
| `cdp-planr-web` | 🌐 REST API + Thymeleaf UI (Category, AbnormalMessage Controllers) |
| `cdp-planr-consumer` | 📨 RabbitMQ Consumer |
| `cdp-planr-ui` | 🎨 Vue.js 前端 (含 package.json, babel.config.js) |
| `cdp-planr-service` | 💼 業務邏輯 |
| `cdp-planr-mapper` | 🗄️ MyBatis (Oracle + PostgreSQL) |
| `cdp-planr-pojo` | 📦 DTO/Entity |
| `cdp-planr-common` | 📦 共用工具 |
| `cdp-planr-report-ws` | 📈 報表 WebService |

---

## 12. cdp-vision-ct — 影像 CT 檢測系統

| 項目 | 內容 |
|---|---|
| **架構模式** | Spring Cloud 微服務 |
| **版本** | **3.0.0-RELEASE** (全系統最高版本) |
| **GroupId** | `com.foxconn.vision.ct` (獨立 GroupId) |
| **特殊依賴** | Caffeine Cache 2.9.3 (JVM 本地快取), cdp-das-pojo |
| **Docker Registry** | 10.244.170.49:8084 (與其他不同) |

**服務清單 (8 個活躍模組 + 4 個已停用)**：

| 狀態 | 模組 | 角色 |
|---|---|---|
| ✅ | `cdp-vision-ct-web` | 🌐 REST API |
| ✅ | `cdp-vision-ct-consumer` | 📨 RabbitMQ Consumer |
| ✅ | `cdp-vision-ct-job-executor` | ⏰ Quartz 排程 |
| ✅ | `cdp-vision-ct-service` | 💼 業務邏輯 |
| ✅ | `cdp-vision-ct-mapper` | 🗄️ MyBatis |
| ✅ | `cdp-vision-ct-pojo` | 📦 DTO/Entity |
| ✅ | `cdp-vision-ct-common` | 📦 共用工具 |
| ✅ | `cdp-vision-ct-report-ws` | 📈 報表 |
| ❌ | `cdp-vision-ct-radar-*` | Radar (4 模組，已註解) |
| ❌ | `cdp-vision-ct-up-download` | 檔案上傳 (已註解) |

---

## 13. DocSecure — 文件安全管理系統

| 項目 | 內容 |
|---|---|
| **架構模式** | Spring Boot 單體 (7 模組分層)，非 Eureka 微服務 |
| **GitLab** | `ma/docsecure` (獨立 Group) |
| **特殊依賴** | **MyBatis-Plus 3.5.2** (其他都用 MyBatis), **MinIO 8.0.3** (S3 物件儲存), **XXL-Job 1.4.0** |

**服務清單**：

| 模組 | 角色 | 說明 |
|---|---|---|
| `cdp-docsecure-web` | 🌐 REST API | OAuth2 保護 |
| `cdp-docsecure-ui` | 🎨 前端 UI | Thymeleaf/靜態資源 |
| `cdp-docsecure-service` | 💼 業務邏輯 | 文件加密/權限 |
| `cdp-docsecure-mapper` | 🗄️ 資料存取 | MyBatis-Plus (含 Generator) |
| `cdp-docsecure-pojo` | 📦 資料物件 | Lombok |
| `cdp-docsecure-common` | 📦 共用工具 | |
| `cdp-docsecure-job-executor` | ⏰ 排程任務 | XXL-Job: 文件處理/權限同步 |

**資料流向**：
```
使用者 (OAuth2 認證)
  │
  ▼
cdp-docsecure-web (REST API)
  │
  ├─► cdp-docsecure-service
  │     ├─► cdp-docsecure-mapper (MyBatis-Plus) → Oracle (Metadata)
  │     └─► MinIO (文件儲存, S3 API)
  │
  └─► RabbitMQ (cdp-mqs) → cdp-mail (通知)

cdp-docsecure-job-executor (XXL-Job)
  └─► 文件過期檢查 / 權限同步 / 存取日誌輪替
```

---

## 14. cdp-radar — 雷達同步系統

| 項目 | 內容 |
|---|---|
| **架構模式** | Spring Cloud 微服務 (5 模組) |
| **入口點** | `COFAFARadarWebApp` |

**服務清單**：

| 模組 | 角色 |
|---|---|
| `cdp-radar-web` | 🌐 REST API + Thymeleaf UI |
| `cdp-radar-service` | 💼 業務邏輯 |
| `cdp-radar-mapper` | 🗄️ MyBatis |
| `cdp-radar-pojo` | 📦 DTO/Entity |
| `cdp-radar-common` | 📦 共用工具 |

**依賴**：Eureka Client, OpenFeign+OkHttp, Hystrix+Turbine, Zipkin/Sleuth, OAuth2+Redis, Config Client+Bus AMQP, CDP 平台 (cdp-sso-token, cdp-rbac-common-implement, cdp-report, cdp-aop)。

---

## 15. hrm-web — 人力資源管理系統

| 項目 | 內容 |
|---|---|
| **架構模式** | Spring Boot 單體 (7 模組分層) |
| **版本** | SNAPSHOT (活躍開發中) |

**服務清單**：

| 模組 | 角色 |
|---|---|
| `cdp-hrm-web` | 🌐 REST API (CdpHrmWebApp) |
| `cdp-hrm-ui` | 🎨 前端 UI |
| `cdp-hrm-service` | 💼 HRM 業務邏輯 |
| `cdp-hrm-mapper` | 🗄️ MyBatis (EmployeeMapper: CRUD) |
| `cdp-hrm-pojo` | 📦 DTO/Entity (Employee) |
| `cdp-hrm-common` | 📦 共用工具 |
| `cdp-hrm` | 📦 父模組 |

---

## 16. 其他專案

### xylon-dev-config
- **路徑**: `F:\JavaWorkspace\xylon-dev-config`
- **結構**: SVN trunk/tags/branches
- **內容**: 所有 COFA/CDP 微服務的共用配置檔案 (application-{site}-{env}.yml)
- **站點**: Alaska, TN, KA, CD (多站點部署)

### xylon-sample
- **路徑**: `F:\codespace\xylon-sample`
- **內容**:
  - `cdp-code-demo` — 標準 CRUD Demo (common/mapper/pojo/service/internal-ws/job-executor)
  - `cdp-code-example` — 程式碼範例
  - `xylon-demo-job-executor` — Job Executor Demo

### Xylon-Open-Ecosystem
- **路徑**: `F:\91-SVN`
- **內容**: Dev_Manual (技術棧/maven/jar/工具), MinIO 部署資料, radarsync-web 備份, hrm-web 備份, maven resp 備份

### TestApi / project
- **路徑**: `F:\TestApi\` (`Demop` + `radar` 測試專案)
- **路徑**: `F:\project\` (`demo` + `test` 練習專案)

---

## 17. 開發環境與規範

### 17.1 統一技術棧 (CDP Java 平台)

| 層 | 技術 | 版本 |
|---|---|---|
| **語言** | Java | 1.8 |
| **框架** | Spring Boot | 2.1.6.RELEASE |
| **微服務** | Spring Cloud | Greenwich.RELEASE |
| **服務發現** | Eureka | Greenwich |
| **API 網關** | Zuul (主) / Spring Cloud Gateway (遷移中) | Greenwich |
| **服務間呼叫** | OpenFeign + OkHttp | 4.2.2 |
| **斷路器** | Hystrix + Dashboard + Turbine | Greenwich |
| **訊息代理** | RabbitMQ (AMQP + Stream + Bus) | — |
| **分散式追蹤** | Zipkin + Sleuth | Greenwich |
| **ORM** | MyBatis 3.5.2 | (DocSecure 用 MyBatis-Plus 3.5.2) |
| **連線池** | Druid | 1.1.12 |
| **資料庫** | Oracle (ojdbc7 12.1.0.2) | (cdp-planr 也支援 PostgreSQL) |
| **快取** | Redis (Lettuce + Commons Pool2) | — |
| **搜尋引擎** | Elasticsearch | 6.3.1 |
| **物件儲存** | MinIO (DocSecure) | 8.0.3 |
| **認證** | Spring Security + OAuth2 + JWT + CAS | — |
| **加密** | Jasypt | 2.1.1 |
| **JAR 加密** | ClassFinal | 1.2.1 |
| **排程** | Quartz 2.3.1 / XXL-Job 1.4.0 | — |
| **模板引擎** | Thymeleaf (+ SpringSecurity5 + Layout) | — |
| **API 文件** | Swagger | 2.9.2 |
| **報表** | Apache POI 4.0.1/4.1.2 + EasyExcel 3.3.2 | — |
| **物件映射** | MapStruct | 1.4.2 |
| **JSON** | Fastjson 1.2.59 + Fastjson2 2.0.27 | — |
| **工具庫** | Hutool | 5.5.4 |
| **日誌** | Logstash Logback Encoder | 5.1/6.1 |
| **建置** | Maven + Docker (Spotify plugin) | — |
| **監控** | Spring Boot Admin 2.1.6 + Jolokia JMX | — |
| **SOAP** | Axis2 | 1.6.2/1.7.8 |

### 17.2 統一技術棧 (Xtrack .NET 平台)

| 層 | 技術 | 版本 |
|---|---|---|
| **語言** | C# | — |
| **框架** | .NET Framework | 4.0 |
| **Web** | ASP.NET WebForms + ASMX WebService | — |
| **IDE** | Visual Studio | 2012 |
| **資料庫** | Oracle (XTRACK157/226) | — |
| **認證** | OAuth2 Bridge (jose-jwt 2.4.0, Newtonsoft.Json 10.0) | — |

### 17.3 模組命名規範

所有 CDP Java 專案遵循統一的分層模組命名：

```
{project}/
├── {project}-common/     # 共用工具/常數
├── {project}-pojo/       # DTO/Entity
├── {project}-mapper/     # MyBatis 資料存取層
├── {project}-service/    # 業務邏輯層
├── {project}-web/        # REST API + UI (Spring Boot 入口)
├── {project}-consumer/   # RabbitMQ 非同步消費者
├── {project}-job-executor/  # 定時任務執行器
├── {project}-internal-ws/   # 內部 WebService
├── {project}-external-ws/   # 外部 WebService
├── {project}-report-ws/     # 報表 WebService
└── pom.xml               # Maven Parent POM
```

| 專案 | cdp-malloc | 說明 |
|---|---|---|---|
| **非標準模組名稱** | `server` 取代 `web` | 入口模組名為 `cdp-malloc-server` 而非 `cdp-malloc-web` |
| **獨立 GroupId** | `com.foxconn.malloc` | 與主線 `com.foxconn.cdp` 不同 |
| **獨立 Oracle Schema** | `MALLOC` | 使用獨立 Schema 而非共用 |
| **cdp-das-pojo** | ✅ 依賴 | Data Analysis Service 資料分析服務整合 |

### 17.4 跨系統依賴矩陣

| 依賴 | cdp | cdp-ups | cdp-job | cdp-mqs | cofa-cd | smt-cofa | bpo | planr | vision-ct | radar | DocSecure | hrm | malloc | matrix |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| cdp-common | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | | | ✅ | ✅ |
| cdp-sso-token | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | | | ✅ | ✅ |
| cdp-rbac | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | | | ✅ | ✅ |
| cdp-mqs | | | | | ✅ | ✅ | ✅ | ✅ | ✅ | | | | ✅ | ✅ |
| cdp-job-core | | | | | ✅ | ✅ | | | | | ✅ | | | ✅ |
| cdp-mail-pojo | | ✅ | ✅ | ✅ | ✅ | ✅ | | | | | | | ✅ | ✅ |
| cdp-ups-pojo | ✅ | | ✅ | ✅ | ✅ | ✅ | | | | | | | ✅ | ✅ |
| cdp-report | | ✅ | | | ✅ | ✅ | | | | | ✅ | | ✅ | |
| cdp-aop | ✅ | ✅ | | | ✅ | ✅ | | | | | | | ✅ | ✅ |
| cdp-framework | | | | | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | | | | |
| cdp-das-pojo | | | | | | | | | | | | | ✅ | ✅ |
| cdp-mpc-service | | | | | ✅ | ✅ | | ✅ | ✅ | | | | ✅ | ✅ |
| Eureka Client | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | | | ✅ | ✅ |
| RabbitMQ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | | ✅ | ✅ |

### 17.5 技術債 & 歷史背景

1. **Hystrix → Resilience4j**：全系統使用 Hystrix (已停止維護)，Spring Cloud Greenwich 是最後一個支援 Hystrix 的版本，需規劃遷移至 Resilience4j
2. **Zuul → Spring Cloud Gateway**：`cdp-gateway` 模組已存在但未完全取代 Zuul，顯示遷移進行中
3. **Spring Boot 2.1.6 → 2.7+/3.x**：2.1.6 已 EOL，需升級
4. **Java 8 → 17+**：Java 8 已停止公開更新
5. **Elasticsearch 6.3.1**：Transport Client 在 7.x 已移除，需遷移至 REST High Level Client
6. **ASP.NET WebForms / ASMX**：Xtrack 平台技術老舊 (.NET 4.0/VS2012)，需評估遷移至 .NET Core/gRPC
7. **OAuth2 Bridge 過渡期**：XtrackOAuth2Bridge 的存在顯示 .NET 舊系統正在過渡到基於 JWT 的現代認證
8. **多 Nexus 倉庫**：`xtrack-releases` (10.176.109.94) vs `dsc-releases` vs `10.244.170.49:8084`，需統一依賴管理
9. **MyBatis vs MyBatis-Plus**：DocSecure 獨立使用 MyBatis-Plus，其餘使用標準 MyBatis
10. **DocSecure 獨立部署**：DocSecure 作為獨立 Group (`ma/docsecure`) 且不使用 Eureka，與其他系統整合度較低

### 17.6 如何透過 Maven 產生可執行 Debug JAR (類似 F:\Xylon Server)

> 參考範例：`F:\Xylon Server\` 內的所有 `*-RELEASE.jar` 都是透過 `spring-boot-maven-plugin` repackage 產出的 Spring Boot 可執行 JAR。

#### 方法一：STS 內建 Maven Build（最常用）

1. 在 STS **Package Explorer** 中，對目標模組（例：`cdp-malloc-server`）**右鍵**
2. 選擇 **Run As → Maven build...**
3. Goals 欄位輸入：
   ```
   clean package -DskipTests -Dclassfinal.skip=true
   ```
4. 點擊 **Run**

| 參數 | 用途 |
|---|---|
| `clean` | 清除上次 `target/` 編譯目錄 |
| `package` | 編譯 + 打包成 JAR |
| `-DskipTests` | 跳過單元測試執行 |
| `-Dclassfinal.skip=true` | 🆕 **跳過 ClassFinal JAR 加密** (Debug 必加！否則 class 被加密無法 debug) |

#### 方法二：STS Boot Dashboard（快速，不產 JAR）

1. 對 Main Class（如 `MallocWebApplication.java`）**右鍵**
2. 選擇 **Run As → Spring Boot App**
3. 設定啟動參數：**Run As → Run Configurations → Arguments → VM arguments**：
   ```
   -Dspring.profiles.active=dev-local
   -Dclassfinal.skip=true
   ```

#### 方法三：Command Line Maven

```bash
cd /f/JavaWorkspace/cdp-malloc

# 編譯所有模組
mvn clean package -DskipTests -Dclassfinal.skip=true

# 只編譯單一模組
cd cdp-malloc-server
mvn clean package -DskipTests -Dclassfinal.skip=true
```

#### JAR 產出位置

```
{模組}\target\{模組名}-{版本}.jar

例：F:\JavaWorkspace\cdp-malloc\cdp-malloc-server\target\cdp-malloc-server-1.0.0-RELEASE.jar
```

#### 與 F:\Xylon Server 的 JAR 格式對比

| | `F:\Xylon Server` JAR | Maven Build JAR |
|---|---|---|
| 打包方式 | `spring-boot-maven-plugin` repackage | ✅ 相同 |
| 內部結構 | `BOOT-INF/classes/` + `BOOT-INF/lib/` | ✅ 相同 |
| ClassFinal 加密 | ✅ 有加密 | 加 `-Dclassfinal.skip=true` 則無 |
| 執行方式 | `java -jar xxx.jar` | ✅ 相同 |
| `bootstrap.yml` 內嵌 | ✅ | ✅ (但建議從 Config Server 拉) |

> ⚠️ **注意**：每個需獨立運行的微服務模組的 `pom.xml` 都必須包含 `spring-boot-maven-plugin` 和 `docker-maven-plugin`（參考 `F:\JavaWorkspace\cdp\cdp-cas\pom.xml` 的 `<build>` 段落）。父 POM 中已定義 plugin 版本，子模組只需宣告即可。

---

## 18. cdp-malloc — 物料分配與 DRP 系統

### 18.1 基本資訊

| 項目 | 內容 |
|---|---|
| **專案名稱** | cdp-malloc (Material Allocation) |
| **路徑** | `F:\JavaWorkspace\cdp-malloc` |
| **架構模式** | Spring Cloud 微服務 (10 模組分層) |
| **版本** | 1.0.0-RELEASE |
| **GroupId** | `com.foxconn.malloc` (獨立 GroupId) |
| **GitLab** | `xylon/cdp-malloc` (10.176.109.182) |
| **作者** | Kasa (2024-02-29 起) |
| **Docker Registry** | 10.176.109.63:5000 |
| **技術棧** | Java 8, Spring Boot 2.1.6, Spring Cloud Greenwich.RELEASE, Oracle Schema `MALLOC` |

### 18.2 與 Matrix (dsc-matrix) 及 cdp-mpc 專案的關係

> **Matrix 相關有三層：`cdp-mpc` (解析 library)、`dsc-matrix` (矩陣平台專案)、`cdp-malloc` (物料分配平台)。cdp-malloc 透過 `cdp-mpc-service` 依賴 Matrix 解析能力，但本地同時缺少 `dsc-matrix` 和 `cdp-mpc` 兩個專案。**

#### 三者關係圖

```
cdp-mpc (Matrix Parse Component，矩陣解析 Library)
  │  GroupId: com.foxconn.mpc
  │  ArtifactId: cdp-mpc-service
  │  用途: 提供 Matrix 解析的共用程式庫（package: com.foxconn.cdp.mpc.service）
  │
  ├──► dsc-matrix (矩陣平台，消費 cdp-mpc)
  │     路徑: F:\JavaWorkspace\matrix\dsc-matrix\
  │     GroupId: com.foxconn / ArtifactId: dsc-matrix
  │     用途: 矩陣表配置檢查、HSG/PAM 子配置、Key Parts 管理、InterChange 緩衝規則
  │
  └──► cdp-malloc (物料分配，消費 cdp-mpc)
        路徑: F:\JavaWorkspace\cdp-malloc\
        用途: DRP 日報解析、物料分配
        ⚠️ 有 @ComponentScan 但缺 pom 依賴
```

#### 修正：Matrix ≠ cdp-mpc

- **cdp-mpc** (`com.foxconn.mpc:cdp-mpc-service`) 是 Matrix 解析的**共用 Library**，`dsc-matrix` 和 `cdp-malloc` 都依賴它
- **dsc-matrix** (`com.foxconn:dsc-matrix`) 是完整的 Matrix 平台專案（9 模組），包含矩陣表 Web UI、API、解析引擎、報表
- **cdp-malloc** 只需要 `cdp-mpc-service` library 就能做 DRP 矩陣解析，不需要整個 dsc-matrix 專案

#### 依賴情況對比

| 專案 | 有 `cdp-mpc-service` pom 依賴 | GroupId | 本地有原始碼 |
|---|---|---|---|
| cdp-cofa-cd (FA) | ✅ `<dependencyManagement>` | `com.foxconn.mpc` | ❌ |
| cdp-smt-cofa (SMT) | ✅ `<dependencyManagement>` | `com.foxconn.mpc` | ❌ |
| cdp-planr | ✅ `<dependencyManagement>` | `com.foxconn.mpc` | ❌ |
| cdp-vision-ct | ✅ `<dependencyManagement>` | `com.foxconn.mpc` | ❌ |
| **dsc-matrix** | ✅ (內部模組依賴) | `com.foxconn.mpc` | ✅ `F:\JavaWorkspace\matrix\` |
| **cdp-malloc** | ❌ **完全沒有宣告** | — | ❌ |

#### 🔴 500 錯誤根本原因

`MallocConsumerApplication.java` 第 30 行的 `@ComponentScan` 掃描了 Matrix 相關 package：

```java
@ComponentScan(basePackages = {
    "com.foxconn.cdp.malloc",
    "com.foxconn.cdp.aop",
    "com.foxconn.cdp.common.config",
    "com.foxconn.cdp.sso",
    "com.foxconn.cdp.mqs.service",
    "com.foxconn.cdp.mpc.service",  // ← 掃描 Matrix 解析相關包！
    "com.foxconn.cdp.mqs.rabbitmq.sender.service",
})
```

**問題鏈**：
1. `cdp-malloc-consumer` 的 `@ComponentScan` 要掃描 `com.foxconn.cdp.mpc.service`
2. 但 `cdp-malloc` 的 `pom.xml` **沒有宣告** `cdp-mpc-service` 為依賴
3. 執行時期找不到 Matrix 解析相關的 class → `NoClassDefFoundError` / `BeanCreationException`
4. 任何依賴 Matrix 解析的頁面 (DRP 解析、Daily DRP、物料分配查詢等) → HTTP 500

#### 修復方案

**步驟 1**：在 `F:\JavaWorkspace\cdp-malloc\pom.xml` 的 `<properties>` 和 `<dependencyManagement>` 中新增：

```xml
<!-- mpc 版本信息(Matrix 解析相关业务) -->
<cdp.mpc.version>1.0.0-RELEASE</cdp.mpc.version>

<dependency>
    <groupId>com.foxconn.mpc</groupId>
    <artifactId>cdp-mpc-service</artifactId>
    <version>${cdp.mpc.version}</version>
</dependency>
```

**步驟 2**：在 `cdp-malloc-consumer/pom.xml` 中引入 `cdp-mpc-service` 依賴。

**步驟 3**：從 GitLab (`10.176.109.182`，路徑為 `xylon/cdp-mpc` 或 `mpc/cdp-mpc`) clone cdp-mpc 原始碼。或直接使用 `dsc-matrix`（內含 `cdp-mpc-service` 的完整依賴，路徑 `F:\JavaWorkspace\matrix\`）。

> 💡 **補充**：`F:\JavaWorkspace\matrix\dsc-matrix\` 是 Matrix 平台本體（詳見 §20），它和 cdp-malloc 共同依賴 `cdp-mpc-service`。但 cdp-malloc 只需要 cdp-mpc 的 library jar，不需要匯入整個 dsc-matrix 專案。

### 18.3 服務清單 (9 個可構建模組)

| 層 | 模組 | 角色 |
|---|---|---|
| **基礎** | `cdp-malloc-common` | 共用工具/常數 |
| | `cdp-malloc-pojo` | DTO/Entity (Component, MaterialAllocation, DRP, Email 等) |
| **資料** | `cdp-malloc-mapper` | MyBatis Oracle Mapper (Schema: `MALLOC`) |
| | `cdp-malloc-data-sync-mapper` | MyBatis Oracle Mapper (資料同步) |
| **業務** | `cdp-malloc-service` | 物料分配業務邏輯 + Feign Client (郵件/使用者/UPS Token) |
| **運行** | `cdp-malloc-server` | 🌐 REST API + Thymeleaf UI (`MallocWebApplication`) |
| | `cdp-malloc-consumer` | 📨 RabbitMQ Consumer (`MallocConsumerApplication`) |
| **排程** | `cdp-malloc-job-executor` | ⏰ 定時任務 (含 TestController) |
| **報表** | `cdp-malloc-report-ws` | 📈 報表 WebService |
| **前端** | `cdp-malloc-ui` | 🎨 前端 UI 資源 |

**非標準模組名稱**：此專案使用 `cdp-malloc-server` 而非其他專案的 `cdp-*-web`，且路徑為 `com.foxconn.cdp.malloc.web`。

### 18.4 資料庫結構 (Oracle Schema: MALLOC，14 張表)

| 表名 | 說明 |
|---|---|
| `DICT` | 字典類型表 (類型代碼/名稱/描述/啟用) |
| `DICT_ITEM` | 字典條目表 (鍵值對) |
| `CDP_USER` | UPS 使用者表 (使用者 ID/姓名) |
| `CDP_PROJECT` | 專案表 |
| `CDP_MILESTONE` | 里程碑表 |
| `BUILD_MAPPER` | 建置對映表 |
| `ATTACHMENT` | 附件表 |
| `EMAIL_TEMPLATE` | 郵件模板表 |
| `DEPARTMENT` | 部門表 |
| `DEPARTMENT_USER` | 部門-使用者關聯表 |
| `REQUIREMENT_COLLECT_RECORD` | 需求收集記錄表 |
| `REQUIREMENT_REFINE` | 需求細化表 |
| **`MATERIAL_ALLOCATION`** | 🔑 **物料分配表** (核心業務表) |
| `COLUMN_VAL_RECORD_RELATION_EPM` | EPM 欄位值關聯記錄表 |

### 18.5 核心業務 Controller

| Controller | 用途 |
|---|---|
| `MaterialAllocationController` | 🔑 物料分配管理 (核心) |
| `RequirementCollectionController` | 需求收集 |
| `InitiateCollectionController` | 發起收集 |
| `PurposeRequirementController` | 用途需求管理 |
| `ConfirmIssuanceController` | 確認發放 |
| `ConfirmReceiptController` | 確認收貨 |
| `ComponentController` | 元件管理 |
| `SubComponentController` | 子元件管理 |
| `DrpDashDrpController` | DRP 日報追蹤 |
| `DepartmentConfigurationController` | 部門配置 |
| `DepartmentStaffConfigurationController` | 部門人員配置 |
| `DictController` | 字典管理 |
| `EmailController` | 郵件管理 |
| `FileController` | 檔案管理 |
| `UserController` | 使用者管理 |

### 18.6 依賴與通訊

**CDP 平台依賴** (與其他微服務一致)：
- `cdp-sso-token` (SSO Token 驗證)
- `cdp-common` (共用工具)
- `cdp-aop` (審計日誌)
- `cdp-rbac-common-implement` (RBAC 權限)
- `cdp-report` (報表)
- `cdp-job-core` (排程)
- `cdp-mail-pojo` (郵件)

**UPS/MQS/DAS 整合**：
- `cdp-ups-pojo` (UPS 使用者/部門資料)
- `cdp-mqs-pojo` + `cdp-mqs-rabbitmq-sender-service` (RabbitMQ 訊息發送)
- `cdp-das-pojo` (Data Analysis Service，資料分析服務)

**Feign Client (服務間同步呼叫)**：
- `SendEmailServiceFeign` — 呼叫郵件服務發送通知
- `UserServiceFeign` — 呼叫 UPS 查詢使用者
- `UserTokenValidationServiceFeign` — Token 驗證

**RabbitMQ Routing Key** (`MallocMessageRoutingKey`)：自定義訊息路由，用於物料分配相關事件廣播

**通訊方式**：同步 REST (Feign+OkHttp, Eureka) + 非同步 RabbitMQ (cdp-mqs) + 郵件通知 (cdp-mail)

### 18.7 資料流向

```
使用者 (SSO/OAuth2 認證)
  │
  ▼
cdp-malloc-server (REST API, Thymeleaf UI)
  │
  ├─► cdp-malloc-service
  │     ├─► cdp-malloc-mapper → Oracle (Schema: MALLOC)
  │     │     └─► MATERIAL_ALLOCATION (核心表)
  │     ├─► Feign → cdp-ups (查詢使用者/部門)
  │     ├─► Feign → cdp-mail (發送郵件通知)
  │     └─► RabbitMQ (cdp-mqs) 廣播物料分配事件
  │
  ▼
cdp-malloc-consumer (RabbitMQ 非同步處理)
  │
  ▼
cdp-malloc-job-executor (定時任務：資料同步/報表生成)
```

### 18.8 業務摘要

**Malloc = Material Allocation (物料分配系統)**。這是一個供應鏈/物料管理系統，核心功能為：

1. **物料分配管理** — 物料需求收集 → 細化 → 分配 (MaterialAllocation)
2. **元件/子元件管理** — Component/SubComponent CRUD
3. **DRP (Distribution Resource Planning)** — 日報追蹤與解析
4. **發放/收貨確認** — ConfirmIssuance / ConfirmReceipt 流程
5. **部門與人員配置** — 部門-使用者關聯管理
6. **郵件通知** — EmailTemplate + Feign 呼叫 cdp-mail
7. **字典管理** — Dict/DictItem 動態配置

此專案與 CDP 生態系統完全整合 (Eureka/RBAC/SSO/MQS/UPS)，遵循相同的分層架構規範，唯獨 `server` 取代 `web` 的命名方式與其他專案不同。

### 18.9 cdp-malloc Local Debug 最少啟動服務

#### 啟動順序與層級

| 層級 | # | 服務 | Main Class | 必須 | 預設 Port |
|---|---|---|---|---|---|
| **基礎設施** | 1 | **cdp-eureka** | `EurekaApplication` | ✅ 必須 | 8761 |
| | 2 | **cdp-config** | `ConfigApplication` | ✅ 必須 | 50010 |
| **可選基礎** | 3 | cdp-cas | `CasApplication` | ⚠️ 登入才需要 | — |
| | 4 | cdp-zuul-gateway | `ZuulGatewayApplication` | ⚠️ 經網關才需要 | — |
| **malloc 自身** | 5 | **cdp-malloc-server** | `MallocWebApplication` | ✅ 核心 | 看 application.yml |
| | 6 | cdp-malloc-consumer | `MallocConsumerApplication` | ⚠️ 訊息消費 | — |
| | 7 | cdp-malloc-job-executor | — | ❌ 可選 | — |
| | 8 | cdp-malloc-report-ws | — | ❌ 可選 | — |

#### 外部依賴服務 (Feign 呼叫對象，不啟動只會觸發 Hystrix fallback)

| 服務 | Feign Client | 用途 |
|---|---|---|
| cdp-ups-internal-ws | `UserServiceFeign` | 查詢使用者/部門資訊 |
| cdp-mqs-internal-ws | `SendEmailServiceFeign` | 發送郵件通知 |
| cdp-ups-core-ws | `UserTokenValidationServiceFeign` | Token 驗證 |

#### 最小啟動組合

```
僅開發物料 CRUD 頁面（不用登入、不用 DRP）：
  cdp-eureka + cdp-config + cdp-malloc-server          (3 個)

完整 Local Debug（含登入驗證）：
  cdp-eureka + cdp-config + cdp-cas + cdp-zuul-gateway
  + cdp-malloc-server + cdp-malloc-consumer            (6 個)
```

> 💡 **提示**：`cdp-malloc-consumer` 啟動前建議先修復 Matrix 依賴 (參見 §18.2)，否則 `@ComponentScan("com.foxconn.cdp.mpc.service")` 會在執行時期拋出 `NoClassDefFoundError`。

---

## 19. STS Workspace 遷移問題記錄（2026-07-31）

### 20.1 基本資訊

| 項目 | 內容 |
|---|---|
| **專案名稱** | dsc-matrix (Data Service Center - Matrix) |
| **路徑** | `F:\JavaWorkspace\matrix\dsc-matrix` |
| **架構模式** | Spring Cloud 微服務 (9 模組分層，4 活躍 + 5 開發用) |
| **版本** | 1.0.0-RELEASE |
| **GroupId** | `com.foxconn` / ArtifactId: `dsc-matrix` |
| **技術棧** | Java 8, Spring Boot 2.1.6, Spring Cloud Greenwich.RELEASE, Oracle |
| **起源** | 從 `data-service-center` 重構而來（因原專案過於臃腫） |
| **Docker Registry** | 10.244.170.49:8084 / 10.175.94.71:5000 |

### 20.2 服務清單

#### Active 模組 (Docker 部署用，4 個)

| 模組 | 獨立服務 | Main Class | Package | 說明 |
|---|---|---|---|---|
| `dsc-matrix-web` | ✅ | `MatrixWebApplication` | `com.foxconn.config.inspect` | 🌐 矩陣配置 Web UI + REST，17 個 Controller |
| `dsc-matrix-api` | ✅ | `MatrixApiApplication` | `com.foxconn.dsc.matrix.api` | 🔵 REST API 微服務 (CofaController, CosmeticController) |
| `dsc-matrix-parse` | ✅ | `MatrixParseApplication` | `com.foxconn.dsc.matrix.parse` | ⚙️ 矩陣解析引擎 (含 @EnableScheduling 定時任務) |
| `dsc-matrix-report-ws` | ✅ | `MatrixReportWSApplication` | `com.foxconn.dsc.matrix.report.ws` | 📈 報表 WebService (Hystrix + Zipkin + EasyExcel) |

#### 開發用模組 (本地開發，5 個，在根 pom 中被註解)

| 模組 | 說明 |
|---|---|
| `dsc-matrix-pojo` | 📦 DTO/Entity |
| `dsc-matrix-common` | 📦 共用工具 |
| `dsc-matrix-mapper` | 🗄️ MyBatis Oracle Mapper |
| `dsc-matrix-service` | 💼 業務邏輯層 |
| `dsc-matrix-base-api` | 🔵 API 層 (含獨立 Main Class: `DscMatrixBaseApiApplication`) |

> **本地開發 vs Docker 部署切換**：根 pom 註解寫明「前面的项目必须注释，以下项目才可以正常往 docker 推送」。本地開發時使用 `bk-dir/pom.xml` (全 8 模組)，Docker 部署時用根 `pom.xml` (4 模組)。

### 20.3 核心 Controller 清單 (dsc-matrix-web，17 個)

| Controller | 用途 |
|---|---|
| `MatrixTableController` | 🔑 矩陣表配置管理 (核心) |
| `MatrixPreviewController` | 🔑 矩陣預覽 |
| `MatrixTableViewController` | Thymeleaf 矩陣表頁面 |
| `SubConfigQueryController` | 子配置查詢 |
| `SubConfigQueryViewController` | 子配置 Thymeleaf 頁面 |
| `PamSubConfigController` | PAM 子配置 CRUD |
| `HsgSubConfigController` | HSG 子配置管理 |
| `KeyPartsController` | Key Parts 關鍵零件管理 |
| `SideAndLocationController` | 側面/線體/位置管理 |
| `SideLocalViewController` | 側面/位置 Thymeleaf 頁面 |
| `LineAndDependencyController` | 線體依賴關係管理 |
| `InterChangeController` | InterChange 互換/緩衝邏輯 |
| `InterChangeViewController` | InterChange Thymeleaf 頁面 |
| `BufferRuleController` | 緩衝規則管理 |
| `ConfigurationViewController` | 配置 Thymeleaf 頁面 |
| `ExportExcelController` | Excel 匯出 |
| `ConfigInspectExceptionHandler` | 全域例外處理 |

### 20.4 與 cdp-mpc (Matrix Parse Component) 的關係

> **dsc-matrix ≠ cdp-mpc**。cdp-mpc (`com.foxconn.mpc:cdp-mpc-service`) 是 Matrix 解析的共用 Library，被 dsc-matrix 和 cdp-malloc 共同依賴。

```
cdp-mpc (Matrix Parse Component, 外部 Library)
  │  GroupId: com.foxconn.mpc
  │  ArtifactId: cdp-mpc-service
  │  用途: 矩陣解析的共用套件
  │
  ├──► dsc-matrix (本專案，完整 Matrix 平台)
  │     「mpc 版本信息(Matrix 解析相关业务)」 ← pom.xml 第 97 行
  │
  └──► cdp-malloc (物料分配平台)  
        @ComponentScan("com.foxconn.cdp.mpc.service") ← 依賴但缺 pom 宣告
```

### 20.5 依賴與通訊

**CDP 平台依賴**：`cdp-sso-token`, `cdp-common`, `cdp-aop`, `cdp-rbac-common-implement`, `cdp-rbac-aop-external`, `cdp-report`, `cdp-job-core`, `cdp-ups-pojo`, `cdp-mail-pojo`, `cdp-mqs-*`, `cdp-das-pojo`

**MPC 依賴**：`com.foxconn.mpc:cdp-mpc-service:1.0.0-RELEASE` (矩陣解析 Library)

**通訊方式**：同步 REST (OpenFeign+OkHttp, Eureka) + 非同步 RabbitMQ (cdp-mqs) + SOAP (Axis2)

**特點**：`dsc-matrix-web` 使用獨立的 package `com.foxconn.config.inspect`，與其他模組的 `com.foxconn.dsc.matrix.*` 不同。

### 20.6 與其他專案的關係

| 關聯專案 | 關係 |
|---|---|
| **cdp-mpc** (Matrix Parse Component) | 📦 依賴此 Library (矩陣解析共用套件) |
| **cdp-malloc** | 🔗 共同依賴 cdp-mpc；cdp-malloc 做 DRP 物料分配矩陣解析 |
| **cdp-cofa-cd / cdp-smt-cofa** | 🔗 共同依賴 cdp-mpc (FA/SMT 製程使用矩陣解析) |
| **cdp-planr / cdp-vision-ct** | 🔗 共同依賴 cdp-mpc |
| **data-service-center** | 📜 前身（因過於臃腫而重構） |

### 20.7 備註

- **Solon 框架**：根 pom 定義了 `<solon.version>3.4.0</solon.version>`，但原始碼中未找到任何 Solon 引用（`@Solon`、`org.noear.solon` 均無），推測是實驗性或已放棄的遷移計畫
- **Swagger**：dsc-matrix-web 整合了 Swagger2 + Swagger2Markup，可生成 API 文件
- **DevTools**：dsc-matrix-web 引入了 `spring-boot-devtools`，支援熱重載

---

## 20. dsc-matrix — Matrix 矩陣配置平台

> **遷移方向：** 舊 `E:\TeckDocuments` → 新 `F:\JavaWorkspace`

### 19.1 環境資訊

| 項目 | 路徑 |
|---|---|
| **STS 版本** | `D:\tool\STS\4.10.0.RELEASE` |
| **JDK 1.8** | `C:\Program Files (x86)\Java\jdk1.8.0_171` |
| **Maven 3.6.3** | `D:\tool\apache-maven-3.6.3` |
| **Maven Local Repo** | `D:\maven-resp` |
| **Maven Settings** | `D:\tool\apache-maven-3.6.3\conf\settings.xml` |
| **Nexus (內網)** | `http://10.176.109.52:8088/nexus/content/groups/public` |
| **舊 Workspace Metadata** | `E:\TeckDocuments\.metadata` |

### 19.2 問題 1：`SysCustomizeToken` 和 `AppModuleInfo` 無法解析（待處理）

**錯誤訊息：**
```
The import com.foxconn.cdp.ups.pojo.SysCustomizeToken cannot be resolved
The import com.foxconn.cdp.ups.pojo.AppModuleInfo cannot be resolved
```

**根因：** `cdp-ups/cdp-ups-pojo` 模組中缺少這兩個 POJO 類別檔案，但 `cdp-cas` 等多個模組均有引用。

**受影響檔案（5 個）：**
- `cdp-cas/.../feign/UserServiceFeign.java`
- `cdp-cas/.../feign/fallback/UserServiceFeignFallbackImpl.java`
- `cdp-cas/.../provider/AbstractCustomXylonAuthTokenProvider.java`
- `cdp-cas/.../provider/CustomXylonAuthTokenProvider.java`
- `cdp-cas/.../filter/XylonCustomTokenCallbackLoginFilter.java`

**狀態：** 待補建兩個 POJO 類別。`SysCustomizeToken` 用途為 Xylon 自定義 Token 認證配置（含 `aesKey`、`menuWhiteList`、`requestFromMenu`），`AppModuleInfo` 用途為 app/module ID 查詢（含 `appName`、`moduleName`、`appId`、`moduleId`）。

### 19.3 問題 2：JDK 1.8 在新 Workspace 中未註冊（✅ 已修復）

**根因：** 新的 STS workspace metadata 中只有 Java 15 JRE，缺少 JDK 1.8 定義，且 `defaultVM` 指向了錯誤的 Java 15。

**修復操作：**

**a) 修復 `org.eclipse.jdt.launching.prefs`**
檔案：`F:\JavaWorkspace\.metadata\.plugins\org.eclipse.core.runtime\.settings\org.eclipse.jdt.launching.prefs`
- 添加了 `jdk1.8.0_171` VM 定義（`C:\Program Files (x86)\Java\jdk1.8.0_171`）
- 將 `defaultVM` 從 Java 15 JRE 改為 `jdk1.8.0_171`

**b) 修復 `org.eclipse.jdt.core.prefs`**
檔案：`F:\JavaWorkspace\.metadata\.plugins\org.eclipse.core.runtime\.settings\org.eclipse.jdt.core.prefs`
補回 5 個 classpath 變數：
```
ECLIPSE_HOME   = D:/tool/STS/4.10.0.RELEASE/
JRE_LIB        = C:/Program Files (x86)/Java/jdk1.8.0_171/jre/lib/rt.jar
JRE_SRC        = C:/Program Files (x86)/Java/jdk1.8.0_171/src.zip
JRE_SRCROOT    = (empty)
JUNIT_HOME     = D:/tool/STS/4.10.0.RELEASE/plugins/org.junit_4.13.0.v20200204-1500.jar
```

### 19.4 問題 3：Maven Parent POM 無法下載（✅ 已修復）

**錯誤訊息：**
```
Non-resolvable parent POM for com.foxconn:cdp:2.0.0-RELEASE: Failure to transfer
org.springframework.boot:spring-boot-starter-parent:pom:2.1.6.RELEASE from ...
```

**根因：** 之前網路不通時的 Maven 下載失敗被快取在本地 repo 中（`.lastUpdated` 檔案），阻擋了後續重試。

**Maven 配置：**
- 使用 Nexus mirror (`mirrorOf *`)，所有請求經由 `http://10.176.109.52:8088/nexus/content/groups/public`
- Maven Central 無法直連（公司網路需要 proxy，但 `settings.xml` 中 proxy 設定是註解掉的）

**修復操作：**
1. 手動刪除失敗快取目錄：`D:/maven-resp/org/springframework/boot/spring-boot-starter-parent/2.1.6.RELEASE`
2. STS 中 Maven → Update Project → 勾選 `Force Update of Snapshots/Releases`

### 19.5 問題 4：Boot Dashboard 不顯示 Launch 設定（已診斷，待處理）

**現象：** Launch 設定檔案都存在，但 Boot Dashboard 中看不到任何應用。

**根因：** 所有模組的 `.project` 檔案缺少 Spring Nature：
```xml
<nature>org.springframework.ide.eclipse.core.springnature</nature>
```
雖然 `buildSpec` 中有 `springbootbuilder`，但 Boot Dashboard 是以 **project nature** 來判斷是否為 Spring Boot 專案。

**受影響範圍：** 共 88 個 `.project` 檔案缺少該 nature。

**修復方案：** 關閉 STS → 對每個缺少 springnature 的 `.project` 在 `<natures>` 區塊中插入該行 → 重啟 STS。

### 19.6 問題 5：Workspace 專案匯入數量對比

| | 舊 (E:\) | 新 (F:\) |
|---|---|---|
| **總數** | 85 | 80 |

**舊有、新無（5 個，不影響功能）：**
- `cdp`（根 parent POM）、`cdp-cofa-cd`、`cdp-job`、`cdp-mqs`（父 POM）、`cdp-password-encoder`

**新有、舊無（整合進來的模組）：**
- `cdp-malloc-*` 系列（新模組）
- `cdp-ups-*` 系列（獨立 workspace → 已整合）、`cdp-ups-echart-ws`

**結論：** 數量差異屬正常模組重組，不影響功能。

### 19.7 舊 STS 啟動設定備份（dev-local 環境）

從 `E:\TeckDocuments\.metadata\.plugins\org.eclipse.debug.core\.launches\` 擷取：

| 服務 | Main Class | Program Args | VM Args |
|---|---|---|---|
| **cdp-cas** | `com.foxconn.cdp.cas.CasApplication` | *(無)* | *(無)* |
| **cdp-eureka** | `com.foxconn.cdp.eureka.EurekaApplication` | *(無)* | *(無)* |
| **cdp-config** | `com.foxconn.cdp.config.ConfigApplication` | *(無)* | *(無)* |
| **cdp-monitor** | `com.foxconn.cdp.monitor.MonitorApplication` | *(無)* | *(無)* |
| **cdp-zuul-gateway** | `com.foxconn.cdp.gateway.ZuulGatewayApplication` | `--spring.profiles.active=dev-local` | `-Xmx256M -Xms128M` |
| **cdp-framework-web** | `com.foxconn.cdp.framework.FWWebApplication` | `--spring.profiles.active=dev-local` | `-Xmx256M -Xms128M` |
| **cdp-cofa-fa-web** | `com.foxconn.cdp.cofa.fa.web.COFAFAWebApp` | *(無)* | `-Xmx256M -Xms128M` |
| **cdp-cofa-fa-web (rbac)** | 同上 | `--spring.profiles.active=dev-local-with-rbac` | `-Xmx256M -Xms128M` |

**建議啟動順序：** Eureka → Config → Monitor → Zuul Gateway → CAS → 其他業務服務

---

> 📊 **文件統計**：24 個不重複專案 | CDP Java 平台: 17 個 | Xtrack .NET 平台: 7 個 | 總服務/模組數: ~170+ 個
