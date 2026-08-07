# XTrack iServices — BL-Projects 專案總覽 / Project Overview

> **XTrack iServices**（又稱 **BL-IService Platform**）是一個部署於氣隙網路（Air-Gapped）環境的企業內部微服務平台，隸屬於 **NPI/Foxconn**。平台提供 SSO 認證、會議室管理（MRMS）、看板（Kanban）等企業應用功能。
>
> This workspace contains the complete set of repositories for the XTrack iServices platform — an internal enterprise microservice platform serving NPI/Foxconn, deployed in an air-gapped corporate network.

---

## 目錄 / Table of Contents

1. [專案總覽 / Project Overview](#1-專案總覽--project-overview)
2. [架構總覽 / Architecture Overview](#2-架構總覽--architecture-overview)
3. [auth-service-testing — 認證服務](#3-auth-service-testing--認證服務)
4. [mrms-iservice-backend-testing — 會議室管理系統](#4-mrms-iservice-backend-testing--會議室管理系統)
5. [kanban-iservice-backend-testing — 看板系統](#5-kanban-iservice-backend-testing--看板系統)
6. [front-end-testing — 前端門戶](#6-front-end-testing--前端門戶)
7. [xtrack-infra-testing — 基礎設施](#7-xtrack-infra-testing--基礎設施)
8. [learning-resource-main — 學習資源](#8-learning-resource-main--學習資源)
9. [Nexus / GitLab / Docker 三者關係](#9-nexus--gitlab--docker-三者關係)
10. [CI/CD 流程詳解 / CI/CD Pipeline](#10-cicd-流程詳解--cicd-pipeline)
11. [資料庫遷移總表 / Database Migrations](#11-資料庫遷移總表--database-migrations)
12. [技術棧總表 / Technology Stack](#12-技術棧總表--technology-stack)
13. [程式碼規模估算 / Code Size](#13-程式碼規模估算--code-size)
14. [文件覆蓋 / Documentation](#14-文件覆蓋--documentation)
15. [總結 / Summary](#15-總結--summary)

---

## 1. 專案總覽 / Project Overview

`F:\BL-Projects` 並非單一 monorepo，而是 **6 個各自獨立版本控制的 GitLab 專案**，共同組成 XTrack iServices 平台。每個子專案有獨立的 CI/CD pipeline，透過 Docker Compose（位於 `xtrack-infra-testing`）在 Traefik 反向代理後統一調度。

`F:\BL-Projects` is **not a monorepo** — it contains 6 independently version-controlled GitLab projects that together form the XTrack iServices platform. Each project has its own CI/CD pipeline, orchestrated via Docker Compose behind a Traefik reverse proxy.

### 子專案一覽表 / Project Inventory

| # | 目錄 / Directory | 類型 / Type | 語言 / Language | 框架 / Framework | 端口 / Port |
|---|-------------------|-------------|-----------------|------------------|-------------|
| 1 | `auth-service-testing` | 後端服務 / Backend | Java 21 | Spring Boot 3.5 | 3000 |
| 2 | `mrms-iservice-backend-testing` | 後端服務 / Backend | Java 21 | Spring Boot 3.5 + JPA + Flyway | 8081 |
| 3 | `kanban-iservice-backend-testing` | 後端服務 / Backend | Python 3.12 | FastAPI + SQLAlchemy Async | 8081 / 8082 |
| 4 | `front-end-testing` | 前端 / Frontend (Monorepo) | TypeScript | React 18 + Vite + Ant Design 5 | 5173 (dev) |
| 5 | `xtrack-infra-testing` | 基礎設施 / Infrastructure | YAML / Go / PowerShell | Docker Compose + Traefik | — |
| 6 | `learning-resource-main` | 文件 / Documentation | Markdown / XLSX | N/A | — |

### 網路環境 / Network Environment

- **內部 Nexus**（依賴鏡像倉庫）：`10.226.122.79:8081`
- **內部 GitLab**（原始碼 + CI/CD + Container Registry）：`10.226.122.79:8443`（Git）、`:5050`（Registry）
- **目標部署主機**：`10.226.122.80`
- **Oracle UPS 資料庫**：`10.226.122.23:1526`
- **PostgreSQL 資料庫**：`10.226.122.79:5432`
- **Xylon 郵件服務**：`10.226.122.50`

---

## 2. 架構總覽 / Architecture Overview

```
                         ┌──────────────────────────────────────┐
                         │         Traefik (反向代理)             │
                         │   - 路由分發                           │
                         │   - ForwardAuth → auth-service        │
                         │   - SSL 終端                           │
                         └──────┬──────────┬──────────┬──────────┘
                                │          │          │
                    ┌───────────┼──────────┼──────────┼───────────┐
                    ▼           ▼          ▼          ▼           │
              ┌──────────┐ ┌────────┐ ┌────────┐ ┌────────┐      │
              │  auth    │ │ kanban │ │  mrms  │ │frontend│      │
              │ service  │ │backend │ │backend │ │(nginx) │      │
              │ (Java)   │ │(Python)│ │ (Java) │ │(React) │      │
              │ :3000    │ │ :8081  │ │ :8081  │ │ :80    │      │
              └────┬─────┘ └───┬────┘ └───┬────┘ └────────┘      │
                   │           │          │                        │
                   ▼           ▼          ▼                        │
              ┌────────┐ ┌──────────────────┐                     │
              │ Oracle │ │   PostgreSQL     │                     │
              │  UPS   │ │ (mrms + kanban) │                     │
              └────────┘ └──────────────────┘                     │
                                                                  │
              ┌─────────────────────────────────────────┐         │
              │         Elasticsearch + Filebeat         │         │
              │           (日誌採集與儲存)                 │         │
              └─────────────────────────────────────────┘         │
                                                                  │
              內部 Nexus (10.226.122.79:8081) ← 依賴鏡像          │
              內部 GitLab Registry (10.226.122.79:5050) ← 容器鏡像 │
              ───────────────────────────────────────────          │
              目標部署主機: 10.226.122.80                         │
              ────────────────────────────────────────────────────┘
```

---

## 3. auth-service-testing — 認證服務

### 定位 / Purpose

OUAC SSO 統一認證 + JWT 令牌管理 + UPS 使用者查詢服務。

Handles OUAC SSO authentication, JWT token issuance/verification, and user/group lookups against the corporate Oracle UPS database. Serves as the Traefik ForwardAuth provider.

### 技術棧 / Tech Stack

| 技術 / Technology | 用途 / Purpose |
|-------------------|---------------|
| Spring Boot 3.5.11 (web, validation, actuator, jdbc) | 應用框架 / Framework |
| Java 21 + Maven | 語言與構建 / Language & Build |
| Oracle JDBC (`ojdbc11`) | 唯讀連線 Oracle UPS |
| JJWT 0.12.6 (HS256) | JWT 簽名/驗證 |
| Caffeine | JWT 黑名單 + IP 白名單 TTL 快取 |
| Springdoc OpenAPI 2.8.6 | Swagger 文件 |
| Lombok | 樣板程式碼 |

### 目錄結構 / Directory Structure

```
src/main/java/com/npi/xtrack/auth/
├── AuthServiceApplication.java        ← 入口點 / Entry point
├── presentation/                      ← 表示層
│   ├── controller/
│   │   ├── AuthController.java        POST /auth/callback, GET /auth/verify, GET /auth/me, POST /auth/logout
│   │   ├── UserController.java        GET /auth/users/search, GET /auth/users/{empNo}
│   │   ├── GroupController.java       GET /auth/groups, GET /auth/groups/{groupId}
│   │   ├── HealthController.java      GET /health
│   │   └── MailController.java        POST /auth/mail/send
│   ├── dto/request/                   CallbackRequest, SendMailRequest
│   ├── dto/response/                  ApiResponse, AuthUserDto, AuthUserSearchDto, AuthGroupDto
│   ├── support/CookieHelper.java
│   └── advice/GlobalExceptionHandler.java
├── application/                       ← 用例層 / Use-case layer
│   ├── AuthCallbackUseCase.java       解密 OUAC → 查 UPS → 簽 JWT → 設定 Cookie
│   └── VerifyTokenUseCase.java        Traefik ForwardAuth：驗證 token → 回傳 X-User-* 頭
├── domain/                            ← 領域層
│   ├── UpsUser.java                   唯讀 record（來自 Oracle UPS）
│   └── UpsGroup.java                  唯讀 record（來自 Oracle UPS）
└── infrastructure/                    ← 基礎設施層
    ├── config/                        AuthProperties, MailProperties
    ├── oracle/                        UpsUserRepository, UpsGroupRepository (JDBC)
    ├── sso/OuacDecoder.java           AES/CBC/PKCS5 解密 OUAC payload
    ├── jwt/JwtService.java            JJWT + Caffeine JTI 黑名單
    ├── ip/IpValidationService.java    RestClient + Caffeine IP 白名單快取
    └── mail/XylonMailService.java     內部 Xylon 郵件微服務客戶端
```

### 核心認證流程 / Core Auth Flow

```
使用者瀏覽器 → OUAC SSO 登入
     ↓
OUAC 回呼 → auth-service 解密 AES payload
     ↓
查詢 Oracle UPS 取得使用者/組織資訊
     ↓
簽發 JWT（HS256），寫入 HttpOnly Secure Cookie
     ↓
後續請求 → Traefik ForwardAuth → /auth/verify
     ↓
驗證通過 → 注入 X-User-Id, X-User-Name 等 HTTP 頭
     ↓
後端服務從 X-User-* 頭讀取使用者上下文
```

### 歷史備註 / History Note

此服務於 **2026 年 5 月從 Python FastAPI 重寫為 Java**（分支 `java-rewrite`），保留 Python 測試用例以確保 AES 解密結果逐位元組相容。

### 測試覆蓋 / Tests

| 測試類 / Test Class | 內容 / Content |
|---------------------|---------------|
| `OuacDecoderTest` | 11 個 AES 奇偶校驗用例 |
| `JwtServiceTest` | JWT 簽名/驗證 |
| `AuthControllerVerifyHeadersTest` | ForwardAuth 回應頭驗證 |

---

## 4. mrms-iservice-backend-testing — 會議室管理系統

### 定位 / Purpose

Meeting Room Management System（MRMS）— 完整的會議室預訂與管理 REST API（**三個後端中程式碼量最大**，約 157 個 Java 源文件）。

Full CRUD for rooms, meeting orders, devices, managers, locked times, and notices.

### 技術棧 / Tech Stack

| 技術 / Technology | 用途 / Purpose |
|-------------------|---------------|
| Spring Boot 3.5.11 (web, data-jpa, validation, actuator, mail) | 應用框架 |
| Java 21 + Maven | 語言與構建 |
| Hibernate 6 + Spring Data JPA | ORM 映射 |
| PostgreSQL | 主資料庫（`iservice_mrms`，schema `mrms`） |
| Flyway (+ flyway-database-postgresql) | 資料庫遷移（15 個） |
| Apache POI 5.3.0 | Excel 報表匯出 |
| Oracle JDBC (`ojdbc11`) | 舊資料遷移（暫時） |
| Springdoc OpenAPI 2.8.6 | Swagger 文件 |
| Lombok | 樣板程式碼 |

### 目錄結構 / Directory Structure

```
src/main/java/com/npi/xtrack/mrms/
├── MrmsIserviceBackendApplication.java   ← 入口點 / Entry point
├── domain/                               ← 領域層（11 個 JPA 實體）
│   ├── entity/          MeetingRoom, MeetingOrder, Device, Manager,
│   │                    LockedMeetingTime, Notice 等 11 個
│   ├── valueobject/     AbnormalType, ManagerPermission, MeetingPurpose,
│   │                    OrderStatus, TimeSlot, WebexType
│   ├── repository/      7 個 Repository 介面
│   └── exception/       13 個領域異常類
├── application/                           ← 用例層（35 個用例，按聚合根分目錄）
│   ├── room/            CreateRoom, UpdateRoom, DeleteRoom, ListRooms,
│   │                    GetRoomInfo, GetRoomAvailability
│   ├── order/           CreateOrder, UpdateOrder, CancelOrder, ReassignOrder,
│   │                    AdjustDate, AbnormalHandle, ListMyOrders, QueryOrders, GetOrderDetail
│   ├── device/          ListDevices
│   ├── manager/         AddManager, RemoveManager, UpdateManager, ListManagers,
│   │                    GetMyManager, ManagerAuthz
│   ├── lock/            CreateLockedTime, DeleteLockedTime, ListLockedTimes
│   ├── notice/          CreateNotice, UpdateNotice, DeleteNotice,
│   │                    ListNotices, GetNoticeById
│   └── dashboard/       GetBookingOverview, GetCancelledOrders,
│                        GetTodayStats, GetWeekOverview
├── infrastructure/                        ← 基礎設施層
│   ├── persistence/     JpaRepository 介面 + Adapter 類
│   ├── config/          AppProperties, AsyncConfig, OpenApiConfig, WebConfig
│   ├── client/          AuthServiceClient（REST 客戶端呼叫 auth-service）
│   └── email/           EmailService, NoOpEmailService, XylonEmailService
├── presentation/                          ← 表示層
│   ├── controller/      9 個 Controller：Dashboard, Device, Group, Lock,
│   │                    Manager, Notice, Order, Room, User
│   ├── dto/request/     23 個 Request DTO
│   ├── dto/response/    7 個 Response DTO
│   ├── filter/TraefikAuthFilter.java      讀取 X-User-* 頭，建構 AuthContext
│   └── advice/GlobalExceptionHandler.java
├── shared/                                ← 共享模組
│   ├── AuthContext.java           userId, loginName, empNo, userName, email, dept, roles
│   ├── AuthContextHolder.java     ThreadLocal holder
│   └── AuditableEntity.java       JPA 審計基底類
└── migration/OracleExplorer.java           Oracle → PostgreSQL 資料遷移工具
```

### 資料庫遷移 / Database Migrations

15 個 Flyway 遷移檔（`src/main/resources/db/migration/`）：V1~V15，覆蓋完整 schema 演進 — 初始建表 → 會議室 → 設備 → 權限組 → 管理員 → 鎖定時間 → 訂單 → 通知 → 郵件日誌 → 索引 → 權限欄位 → 預訂人欄位。（詳見[第 11 節](#11-資料庫遷移總表--database-migrations)）

### 本地開發 / Local Dev

- `TraefikAuthFilter` 在本地開發時可**回退呼叫 auth-service `/auth/me`**（當沒有 Traefik 時）
- `local` Spring profile 將資料庫從 `10.226.122.79` 切到 `localhost`

### 測試覆蓋 / Tests

僅 1 個測試類：`MrmsIserviceBackendApplicationTests.java`

---

## 5. kanban-iservice-backend-testing — 看板系統

### 定位 / Purpose

完整的看板/專案管理 API，支援組織、專案、看板、卡片、衝刺、工時追蹤、評論通知等（**三個後端中功能最豐富**）。

Full Kanban board / project management API: organizations, projects, boards, columns, cards, sprints, time tracking, comments, and notifications.

### 技術棧 / Tech Stack

| 技術 / Technology | 用途 / Purpose |
|-------------------|---------------|
| Python 3.12（`>=3.12,<3.14`） | 語言 |
| FastAPI >= 0.133.1 | Web 框架 |
| SQLAlchemy 2.0 (async) | 非同步 ORM |
| asyncpg >= 0.31.0 | PostgreSQL 非同步驅動 |
| Alembic >= 1.18.4 | 資料庫遷移（18 個） |
| Pydantic v2 + pydantic-settings | 設定管理 + 資料校驗 |
| Uvicorn >= 0.41.0 | ASGI 伺服器 |
| Sentry SDK 2.53.0 | 錯誤監控 |
| pytest / httpx / pytest-asyncio | 測試（dev） |

### 目錄結構 / Directory Structure

```
app/
├── main.py                          ← FastAPI 入口點，註冊 14 個 Router
├── domain/                          ← 領域層
│   ├── entities/                    16 個實體：Board, Card, Column, Sprint, Label,
│   │                                Subtask, Comment, Member, Organization, Task,
│   │                                TimeEntry, Notification, Project, RoleAssignment,
│   │                                TaskComment, TimeTracking
│   ├── value_objects.py             Priority, SprintStatus, OrgRole 列舉
│   ├── repositories.py              9 個 Protocol 介面
│   ├── unit_of_work.py              UnitOfWork Protocol
│   └── exceptions.py
├── application/                     ← 用例層（~73 個檔案，一個用例一個檔案）
│   ├── board/                       10 個用例（CRUD + 成員/標籤管理）
│   ├── card/                        5 個用例（CRUD + 移動）
│   ├── column/                      4 個用例（CRUD + 排序）
│   ├── comment/                     5 個用例 + 通知
│   ├── sprint/                      6 個用例（CRUD + 啟停/關閉）
│   ├── subtask/                     3 個用例
│   ├── task/                        5 個用例
│   ├── project/                     5 個用例
│   ├── organization/                6 個用例
│   ├── member/                      1 個用例（ensure_member）
│   ├── timetracking/                15 個用例（CRUD + 計時器 + 審批 + 儀表板 + 統計）
│   └── services/                    TaskAssignmentNotifier, TaskNotificationService
├── infrastructure/                  ← 基礎設施層
│   ├── config.py                    Pydantic Settings（DB URL, CORS, auth 服務 URL）
│   ├── database.py                  AsyncEngine + AsyncSession
│   ├── models/                      17 個 SQLAlchemy ORM Model
│   ├── repositories/                14 個 Repository 實作
│   ├── unit_of_work.py              SqlAlchemyUnitOfWork
│   └── external/                    mail_client.py, user_client.py
└── presentation/                    ← 表示層
    ├── dependencies.py              FastAPI Depends() DI 注入
    ├── exception_handlers.py
    ├── routers/                     14 個 Router：boards, cards, columns, comments,
    │                                subtasks, sprints, organizations, projects, tasks,
    │                                role_assignments, department_members, notification,
    │                                task_comments, timetracking
    └── schemas/                     16 個 Pydantic Schema（camelCase 別名適配前端）
```

### 其他配置 / Other Config

- `alembic/versions/` — 18 個 Alembic 遷移版本
- `alembic.ini` + `alembic/env.py` — Alembic 設定
- `pyproject.toml` + `requirements.txt` — Python 專案定義
- 完全非同步（SQLAlchemy async + asyncpg）
- 透過 `root_path` 實現 Ingress-agnostic 路由

### 測試覆蓋 / Tests

無 pytest 測試。僅有手動測試腳本 `test.py`（httpx 發送 POST 到 `localhost:8081/api/kanban-iservice/projects`）。

---

## 6. front-end-testing — 前端門戶

### 定位 / Purpose

XTrack iServices Portal — 統一的單頁應用（SPA），整合所有業務模組。使用 pnpm workspace monorepo 架構。

The single frontend SPA for all modules — dashboard, kanban, and meeting-room.

### 技術棧 / Tech Stack

| 技術 / Technology | 用途 / Purpose |
|-------------------|---------------|
| React 18 + React Router DOM 7 | UI 框架 + 路由 |
| Vite 6 + pnpm 9.15.4 | 構建工具 + 套件管理（Monorepo） |
| Ant Design 5（含 Pro Components） | 元件庫 |
| TanStack React Query | 服務端狀態管理 |
| Zustand | 用戶端狀態管理 |
| Axios | HTTP 客戶端 |
| react-i18next / i18next | 國際化（zh-TW / en-US） |
| @dnd-kit | 拖放（Drag & Drop） |
| MSW (Mock Service Worker) | 本地 API Mock |
| VitePWA 插件 | 離線快取支援 |
| dayjs | 日期工具 |
| TypeScript 5.7, ESLint, Prettier, Vitest | 程式碼規範與測試 |

### Monorepo 結構 / Monorepo Structure

```
front-end-testing/
├── apps/
│   └── portal/                        ← 唯一部署應用 / The only deployed app
│       └── src/
│           ├── main.tsx               ← 入口：檢查 ConfigMap → 啟動 MSW → 渲染 App
│           ├── App.tsx                ← QueryClient + ConfigProvider + RouterProvider
│           ├── routes.tsx             ← 公開路由 + AuthGuard 保護路由
│           ├── layouts/               ← MainLayout（Header+Sidebar+Tab）, AuthLayout
│           ├── guards/                ← AuthGuard, PermissionGuard
│           ├── stores/                ← useAuthStore, useAppStore, useTabStore (Zustand)
│           ├── hooks/useUrlTabSync.ts
│           ├── systems/registry.tsx   ← 功能旗標控制的懶載入模組元件
│           ├── config/site-config.ts
│           └── modules/               ← 業務模組（完全隔離）
│               ├── dashboard-iservice/
│               │   ├── pages/DashboardPage.tsx
│               │   └── i18n/en.ts, zh-TW.ts
│               ├── kanban-iservice/        ← 看板模組（~80 個檔案）
│               │   ├── KanbanSystem.tsx
│               │   ├── api/kanban.api.ts       ← HTTP 適配器（Repository）
│               │   ├── components/              ← ~20 元件
│               │   ├── hooks/queries/           ← 16 個查詢 Hook
│               │   ├── hooks/mutations/         ← ~30 個變更 Hook
│               │   ├── stores/                  ← 模組級 Zustand 狀態
│               │   ├── domain/types.ts, rules.ts ← 前端領域邏輯
│               │   └── i18n/en.ts, zh-TW.ts
│               └── meeting-room-iservice/  ← 會議室模組
│                   ├── MeetingRoomSystem.tsx
│                   ├── api/mrms.api.ts
│                   ├── components/         ← RoomCard, BookingFormModal, TimeSlotGrid,
│                   │   │                     WeekAvailabilityTable, admin/
│                   ├── hooks/queries/ + mutations/
│                   ├── stores/             ← useBookingStore, useMrmsTabStore
│                   ├── types/index.ts
│                   └── i18n/en.ts, zh-TW.ts
├── packages/
│   ├── api-client/                   ← 共享 API 層
│   │   └── src/
│   │       ├── index.ts              ← 匯出 httpClient + 型別
│   │       ├── http-client.ts        ← Axios 實例（JWT Cookie 攔截器）
│   │       └── types.ts              ← ApiErrorResponse, PaginatedResponse
│   ├── ui-kit/                       ← 共享 UI 元件庫
│   │   └── src/
│   │       ├── index.ts              ← 匯出 PageHeader, StatusTag, DataTable, theme, hooks
│   │       ├── components/           ← DataTable, PageHeader, StatusTag
│   │       ├── hooks/                ← useModal, usePagination
│   │       └── theme/token.ts        ← Ant Design 主題令牌（亮色 + 暗色）
│   └── utils/                        ← 純工具函式庫
│       └── src/index.ts              ← formatDate, formatDateTime, fromNow, hasPermission,
│                                     hasAnyPermission, storage, formatNumber, formatCurrency,
│                                     isFeatureEnabled, useFeature, ensureArray
├── docs/                             ← 11 份文件（詳見第 14 節）
├── deploy/nginx.conf                 ← Nginx SPA 配置
├── .github/CODEOWNERS
├── package.json                      ← pnpm workspace root
├── pnpm-workspace.yaml
├── pnpm-lock.yaml
├── tsconfig.json + tsconfig.base.json
├── eslint.config.js
└── vite.config.ts
```

### 模組隔離原則 / Module Isolation

每個業務模組擁有自己的路由、API 層、Hooks、Store、型別定義和 i18n 檔案，**模組之間禁止交叉匯入**。功能旗標（`kanban`、`meetingRoom`）控制哪些模組渲染。

### Vite 開發代理 / Dev Proxy

Dev Server 端口 5173，`/api` 和 `/auth` 代理到測試環境 Traefik（`10.226.122.80`）或本地，支援 Cookie 域重寫（本地開發）。

### 測試覆蓋 / Tests

目前前端無測試檔案，但已配置 Vitest、ESLint、Prettier。

---

## 7. xtrack-infra-testing — 基礎設施

### 定位 / Purpose

DevOps 基礎設施即程式碼，統一編排所有服務的部署。此 repo **不是可執行的服務**，而是透過 CI/CD 將配置 SCP 到應用主機並觸發重新部署。

Infrastructure-as-code for the platform — Docker Compose stacks, Traefik routing, CI/CD pipelines, and helper scripts.

### 核心組件 / Core Components

```
xtrack-infra-testing/
├── docker-compose.yml               ← 4 個應用服務：auth, kanban, mrms, frontend
│                                     （按 sha 標籤從 10.226.122.79:5050 拉取）
│     ├── auth-service             :3000
│     ├── kanban-iservice-backend  :8081  /api/kanban-iservice
│     ├── mrms-iservice-backend    :8081  /api/mrms-iservice（512M 記憶體）
│     └── frontend                 :80    nginx（64M 記憶體）
├── docker-compose.infra.yml        ← 基礎設施：Elasticsearch 8.12.0 + Filebeat（容器日誌採集）
├── traefik/
│   ├── config/traefik.yml          ← Traefik 靜態配置
│   └── dynamic/middleware.yml      ← 動態中間件（ForwardAuth → auth-service /auth/verify）
├── filebeat/filebeat.yml           ← 從 /var/lib/docker/containers 採集日誌 → Elasticsearch
├── site-config/default/config.js   ← 前端執行時站點配置（K8s ConfigMap）
├── java/
│   ├── maven-settings.xml          ← 離線 Maven 設定（走內部 Nexus 代理）
│   └── setup-dev.ps1               ← JDK 21 + Maven 開發環境一鍵安裝
├── dev-db/
│   ├── manage.ps1 / manage.sh      ← 按工程師編號建立/重置/刪除開發資料庫
├── scripts/
│   ├── main.go                     ← Go CLI：同步 PyPI/npm/Maven 套件到內部 Nexus
│   ├── nexus-sync.exe              ← 編譯後的 Go 可執行檔
│   ├── nexus-sync.py               ← Python 版同步工具
│   ├── oracle-client.ps1           ← Oracle Instant Client 安裝腳本
│   ├── setup-pg-dev.ps1            ← 本地 PostgreSQL 開發資料庫建立
│   └── vscode-ext.ps1              ← 大量安裝 VS Code 擴充（離線）
├── .env.testing                    ← 測試環境變數（鏡像標籤、Oracle DSN、OUAC 設定）
└── docs/
    ├── CICD_HANDBOOK.md            ← CI/CD 管道手冊
    └── ONBOARDING.md               ← 新工程師入職指南
```

### 輔助工具 / Helper Tools

| 工具 / Tool | 說明 / Description |
|-------------|-------------------|
| `dev-db/manage.ps1` | 為每個工程師建立獨立開發資料庫（`iservice_mrms_dev_<empno>`、`iservice_kanban_dev_<empno>`） |
| `nexus-sync.exe` | 從外網下載 Python/npm/Maven 套件並同步到內部 Nexus 鏡像 |
| `setup-pg-dev.ps1` | 本地 PostgreSQL 開發資料庫 |
| `oracle-client.ps1` | 從 Nexus 安裝 Oracle Instant Client |
| `vscode-ext.ps1` | 離線安裝 VS Code 擴充 |

---

## 8. learning-resource-main — 學習資源

### 定位 / Purpose

新團隊成員入職學習材料（**純文件，無程式碼**）。

### 內容 / Content

| 檔案 / File | 內容 / Content |
|-------------|---------------|
| `README.md` | 索引（預設 GitLab 範本） |
| `java-developer-learning-roadmap.md` | Java 開發者學習路線 |
| `python-developer-learning-roadmap.md` | Python 開發者學習路線 |
| `infra-developer-learning-roadmap.md` | 基礎設施開發者學習路線 |
| `xtrack-tech-stack.xlsx` | 平台技術棧電子表格 |

---

## 9. Nexus / GitLab / Docker 三者關係

### 三者角色總覽 / Roles at a Glance

```
┌─────────────────────────────────────────────────────────────────┐
│                    內部網路 (10.226.122.x)                       │
│                                                                 │
│   ┌──────────────────┐                                          │
│   │     GitLab        │  ← 原始碼託管 + CI/CD Pipeline           │
│   │  10.226.122.79    │                                          │
│   │  :8443 (Git)      │                                          │
│   │  :5050 (Registry)  │←──────────┐                            │
│   └────────┬─────────┘            │                            │
│            │                      │                            │
│    1. git push / merge            │                            │
│            │                      │                            │
│            ▼                      │                            │
│   ┌──────────────────┐   ┌────────┴───────────┐                │
│   │   GitLab CI/CD    │   │  GitLab Container   │                │
│   │   .gitlab-ci.yml  │   │  Registry           │                │
│   │                   │   │  :5050/xtrack-      │                │
│   │  - mvnw 構建      │──▶│  iservice/<service> │                │
│   │  - pnpm 構建      │   │  :<sha-tag>         │                │
│   │  - pip install    │   └────────────────────┘                │
│   │  - docker build   │                                         │
│   │  - docker push ───┘                                         │
│   └────────┬─────────┘                                          │
│            │                                                    │
│    2. 觸發下游 Pipeline                                          │
│            │                                                    │
│            ▼                                                    │
│   ┌──────────────────┐                                          │
│   │    Nexus          │  ← 依賴鏡像 / 套件代理                   │
│   │  10.226.122.79    │                                          │
│   │  :8081            │                                          │
│   │                   │                                          │
│   │  - Maven proxy    │──▶ mvnw 從這裡拉 jar 包                 │
│   │  - PyPI proxy     │──▶ pip 從這裡拉 Python 包               │
│   │  - npm proxy      │──▶ pnpm 從這裡拉 npm 包                 │
│   └──────────────────┘                                          │
│                                                                 │
│   ┌──────────────────┐                                          │
│   │   應用主機         │                                          │
│   │  10.226.122.80    │                                          │
│   │                   │                                          │
│   │  docker compose   │  ← 從 GitLab Registry pull 鏡像         │
│   │  pull && up -d    │  ← Traefik 路由 + ForwardAuth           │
│   └──────────────────┘                                          │
└─────────────────────────────────────────────────────────────────┘
```

### Nexus — 開發階段的依賴來源 / Dependency Source

| 代理 / Proxy | 用途 / Purpose |
|-------------|---------------|
| Maven proxy | Java 專案 `mvnw` 從這裡拉 jar 包（非 Maven Central） |
| PyPI proxy | kanban `pip install` 從這裡拉 Python 包 |
| npm proxy | 前端 `pnpm install` 從這裡拉 npm 包 |
| 離線橋樑 | `nexus-sync.exe` 從外網下載套件後同步到 Nexus |

> **一句話總結**：Nexus = 內部 App Store，所有套件依賴的統一入口。

### GitLab — 原始碼 + CI/CD + 容器倉庫（三位一體）

| 角色 / Role | 端口 / Port | 說明 / Description |
|-------------|-------------|-------------------|
| Git 原始碼管理 | `:8443` | 6 個專案各自獨立的 Git repo |
| CI/CD Pipeline | `.gitlab-ci.yml` | 每個專案有獨立 pipeline：build → docker push → 觸發下游 |
| Container Registry | `:5050` | 儲存所有 Docker 鏡像（`xtrack-iservice/<service>:<sha>`） |

> **一句話總結**：GitLab = 程式碼倉庫 + 自動化流水線 + Docker 鏡像倉庫。

### Docker — 執行階段的載體 / Runtime Carrier

| 階段 / Stage | 說明 / Description |
|-------------|-------------------|
| 構建 (docker build) | 多階段 Dockerfile（maven + temurin-21 → JRE 鏡像 / node + pnpm → nginx 鏡像） |
| 儲存 (docker push) | 鏡像推送到 GitLab Container Registry |
| 部署 (docker compose pull && up) | 應用主機從 Registry 拉取指定 sha 標籤並啟動 |
| 運行 | 4 個服務容器 + Traefik + Elasticsearch/Filebeat |

### 端口 8443 vs 5050 的區別 / Port 8443 vs 5050

| | `:8443` Git 原始碼倉庫 | `:5050` Container Registry |
|---|---|---|
| **存什麼** | Java/Python/TS **原始碼**、設定檔、Dockerfile | 編譯打包後的**完整執行環境**（OS + JDK + jar + 設定） |
| **操作方式** | `git push` / `git pull` / `git clone` | `docker push` / `docker pull` |
| **版本控制** | Git — 完整歷史、diff、blame | 無歷史 — 只有 tag（`testing`、`main`、`<sha>`） |
| **儲存單位** | Git object（blob、tree、commit） | Docker image layer（二進位 blob） |
| **使用者** | 開發人員 | 部署系統 / docker compose |
| **可讀性** | 文字檔、可 diff、人類可讀 | 二進位、不可 diff、人類不可讀 |

> **一句話總結**：`:8443` 存的是「食譜」（原始碼），`:5050` 存的是「做好的菜」（Docker 鏡像）。**原始碼經過 CI 編譯打包後變成鏡像，從 8443 推到 5050，就像食譜做好後變成成品出餐。** 它們是同一條流水線上的**輸入端和輸出端**，不是「另一種儲存方式」。

### 完整開發→部署串聯 / Full Dev-to-Deploy Flow

```
工程師寫代碼
    │
    ├─ mvnw/pnpm/pip ──────▶ Nexus (:8081) ── 拉依賴
    │
    ▼
git push 到 GitLab (:8443)
    │
    ▼
GitLab CI 觸發
    ├─ mvnw/pnpm/pip ──────▶ Nexus (:8081) ── 拉依賴（CI 環境也走 Nexus）
    ├─ docker build
    └─ docker push ────────▶ GitLab Registry (:5050) ── 儲存鏡像
    │
    ▼
觸發 xtrack-infra Pipeline
    ├─ scp docker-compose.yml → 10.226.122.80
    └─ ssh docker compose pull ── 從 GitLab Registry (:5050) 拉鏡像
         docker compose up -d ── 啟動容器
```

> **最終總結**：**Nexus** 是套件原料倉庫（餵給構建過程），**GitLab** 同時是代碼倉庫、CI 引擎和成品倉庫（存 Docker 鏡像），**Docker** 將成品打包並在目標主機上運行。三者串成一條完整鏈：**Nexus 提供依賴 → GitLab CI 構建鏡像 → Docker 在目標機部署運行**。

---

## 10. CI/CD 流程詳解 / CI/CD Pipeline

### 兩段式 Pipeline / Two-Stage Pipeline

```
第一段（各服務 repo）：                           第二段（xtrack-infra repo）：
┌─────────────────────────────┐               ┌────────────────────────────────┐
│ build + deploy 兩個 stage   │               │ 僅在 triggers / web 事件執行     │
│                             │               │                                │
│ 1. build: 構建 Docker 鏡像   │               │ 1. scp docker-compose.yml、    │
│    - mvnw package /          │               │    .env.testing、配置 →        │
│    - pnpm build /            │  觸發下游       │    10.226.122.80              │
│    - pip install             │──────────────▶│ 2. ssh docker compose pull     │
│ 2. deploy: docker push 到    │               │ 3. ssh docker compose up -d   │
│    GitLab Registry           │               │                                │
│    + 觸發 xtrack-infra       │               │                                │
└─────────────────────────────┘               └────────────────────────────────┘
```

### 各專案 CI 差異 / Per-Project CI Differences

| 專案 / Project | 觸發分支 / Branches | 特殊參數 / Special Args |
|----------------|--------------------|------------------------|
| `auth-service-testing` | testing, main | 觸發 infra pipeline |
| `mrms-iservice-backend-testing` | testing, main | 傳遞 `MRMS_TAG` 變數 |
| `kanban-iservice-backend-testing` | testing, main | 傳遞 `BACKEND_TAG` 變數 |
| `front-end-testing` | testing, main | `VITE_MODE`（testing=testing, main=production） |
| `xtrack-infra-testing` | 僅 triggers / web | 最終部署（scp + compose pull/up） |

---

## 11. 資料庫遷移總表 / Database Migrations

### MRMS — 15 個 Flyway 遷移

位於 `mrms-iservice-backend-testing/src/main/resources/db/migration/`：

| 版本 / Version | 內容 / Content |
|---------------|---------------|
| V1 | 初始 schema |
| V2 | meeting_rooms 會議室 |
| V3 | devices 設備 |
| V4 | applicable_group_ids 適用群組 |
| V5 | managers 管理員 |
| V6 | locked_meeting_times 鎖定時間 |
| V7 | meeting_orders 會議訂單 |
| V8 | used_devices 使用設備 |
| V9 | notices 通知 |
| V10 | email_logs 郵件日誌 |
| V11 | indexes 索引 |
| V12 | manager_permission 權限欄位 |
| V13 | days_of_week（鎖定時間每週日期） |
| V14 | booker_fields（訂單預訂人欄位） |
| V15 | legacy_mt_id 可為空 |

### Kanban — 18 個 Alembic 遷移

位於 `kanban-iservice-backend-testing/alembic/versions/`（部分代表性）：

| 遷移 / Migration | 內容 / Content |
|-----------------|---------------|
| `de95de61e827_initial_schema` | 初始 schema |
| `ff61dcb95243_create_projects_table` | projects 表 |
| `b979318616be_create_tasks_table` | tasks 表 |
| `55626825175e_create_role_assignments_table` | role_assignments 表 |
| `fba4e3ebc9e3_create_role_assignments_table` | role_assignments（接續） |
| `679481ccb99b_create_tables_notification_and_comment*` | notification + comment 表 |
| `53639de4be09_create_table_task_comments_for_task*` | task_comments 表 |
| `4767f329ce68_create_department_member_table` | department_member 表 |
| `a1b2c3d4e5f6_add_member_fields_and_audit` | member 欄位 + 審計 |
| `2df06d761756_create_tables_time_entries_and_risk*` | time_entries 表 |
| `3e0b5dea01f5_add_department_to_tasks` | tasks 加部門 |
| `4529fca33ba7_taking_task_title_values_from_tasks*` | 任務標題調整 |
| `a6085025a733_add_approved_and_approved_at_columns_to*` | 審批欄位 |
| `7fe40455d820_add_updated_at_to_projects` | projects 加 updated_at |
| `38f5ac42875a_add_updated_at_to_projects` | projects 加 updated_at（接續） |
| `b8c51de6c6a5_add_updated_at_defaults` | updated_at 預設值 |
| `daa549c7df9c_add_unique_constraint_to_department*` | department 唯一約束 |

> **備註**：auth-service **無遷移** — 連線現有的唯讀 Oracle UPS 資料庫，使用 Spring Boot + Oracle JDBC。

---

## 12. 技術棧總表 / Technology Stack

| 層級 / Layer | 技術 / Technology | 用途 / Purpose |
|-------------|-------------------|---------------|
| **後端 (Java)** | Spring Boot 3.5, Java 21, Maven | auth-service, mrms-backend |
| **後端 (Python)** | FastAPI, Python 3.12, Uvicorn | kanban-backend |
| **ORM** | Spring Data JPA / Hibernate 6, SQLAlchemy 2.0 Async | 資料存取 |
| **資料庫** | PostgreSQL, Oracle | 主儲存 (Postgres), 舊使用者系統 (Oracle) |
| **遷移** | Flyway (Java), Alembic (Python) | 資料庫版本管理 |
| **認證** | JJWT 0.12, Caffeine, OUAC SSO | JWT + 黑名單 + SSO |
| **前端** | React 18, TypeScript, Vite, pnpm | SPA 門戶 |
| **UI** | Ant Design 5, @dnd-kit | 元件庫 + 拖放 |
| **狀態** | TanStack Query, Zustand | 服務端 + 用戶端狀態 |
| **國際化** | react-i18next | zh-TW / en-US |
| **反向代理** | Traefik | 路由 + ForwardAuth |
| **容器化** | Docker, Docker Compose | 所有服務容器化部署 |
| **CI/CD** | GitLab CI | 構建 → 推送鏡像 → 觸發下游 Pipeline |
| **監控** | Elasticsearch 8.12 + Filebeat, Sentry | 日誌採集 + 錯誤監控 |
| **私有倉庫** | Nexus, GitLab Container Registry | 依賴鏡像 + 容器鏡像 |
| **工具** | Go, PowerShell, Bash | 基礎設施腳本 |

---

## 13. 程式碼規模估算 / Code Size

| 專案 / Project | 源文件數（約）/ Files | 語言 / Language |
|----------------|:-------------------:|----------------|
| auth-service-testing | ~25 | Java |
| mrms-iservice-backend-testing | ~157 | Java |
| kanban-iservice-backend-testing | ~160+ | Python |
| front-end-testing | ~150+ | TypeScript / TSX |
| xtrack-infra-testing | ~20 | YAML / Go / PowerShell |
| learning-resource-main | 5 | Markdown / XLSX |
| **總計 / Total** | **~500+** | |

---

## 14. 文件覆蓋 / Documentation

### 各專案文件清單 / Per-Project Docs

| 專案 / Project | 文件 / Docs | 主要內容 / Content |
|---------------|-------------|-------------------|
| auth-service | 2 | README + `docs/CUTOVER.md` 遷移指南 |
| mrms-backend | 2 | `docs/ENTITY_WORKFLOW.md` + `docs/SPEC.md` |
| kanban-backend | 2 | `ARCHITECTURE.md` + `docs/MIGRATION_WORKFLOW.md` |
| **front-end** | **11** | 見下方清單 |
| xtrack-infra | 2 | `docs/CICD_HANDBOOK.md` + `docs/ONBOARDING.md` |
| learning-resource | 5 | 三條學習路線 + 技術棧表 |

### 前端 11 份文件 / Frontend's 11 Docs

| 檔案 / File | 內容 / Content |
|-------------|---------------|
| `architecture.md` | 架構概覽 |
| `monorepo-structure.md` | Monorepo 結構說明 |
| `coding-standards.md` | 編碼規範 |
| `routing.md` | 路由文件 |
| `state-management.md` | 狀態管理 |
| `api-client.md` | API 客戶端 |
| `design-system.md` | 設計系統 |
| `deployment.md` | 部署流程 |
| `ui-guidelines.md` | UI 指南 |
| `kanban-iserverc.md` | 看板功能文件 |
| `frontend-architecture-walkthrough.md` | 架構演練 |

---

## 15. 總結 / Summary

### 平台特點 / Strengths

1. **統一的架構理念** — 前後端均遵循 DDD + Clean Architecture，分層清晰，職責單一
2. **模組化隔離** — 前端模組獨立，後端按聚合根組織用例，互不耦合
3. **氣隙網路適配** — 完善的內部鏡像/代理方案，支撐離線環境開發與部署
4. **成熟的 CI/CD** — GitLab CI 驅動的自動化建構與部署 Pipeline
5. **文件健全** — 前端擁有 11 份架構/規範文件，基礎設施有完整入職指南
6. **技術多樣性** — Java + Python 後端共存，根據業務需求選擇合適技術棧

### 待改進領域 / Areas for Improvement

- **測試覆蓋率整體偏低** — auth-service 有 3 個測試，mrms 僅 1 個，kanban 和前端無自動化測試
- kanban 後端 Python 程式碼量較大但無測試保護
- 前端尚未編寫任何測試檔案

---

*此文件由 Claude Code 分析生成 / Generated by Claude Code analysis — 2026-08-07*
