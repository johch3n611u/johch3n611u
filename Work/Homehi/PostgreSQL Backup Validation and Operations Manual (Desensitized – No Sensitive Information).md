# PostgreSQL 備份驗證與維運操作手冊（已脫敏無敏感資訊）

## 目錄

1. 環境架構總覽
2. 基礎傳輸操作（MobaXterm + PowerShell）
3. 備份檔檢查流程
4. 隔離還原驗證標準流程（不影響正式庫）
5. 數據一致性三層比對方法
6. PostgreSQL 伺服器維運操作
7. Docker 容器化 PG 擴充操作
8. 常見報錯與標準解決方案
9. 伺服器資源狀態解讀

## 1. 環境架構總覽

### 伺服器角色

* DB 主機：存放正式 PostgreSQL 18.1 資料庫，具備完整 psql / pg_restore 工具
* 備份儲存主機：僅存放 .dump 備份檔，無 PostgreSQL 客戶端工具
* 業務庫名：planr，專屬 Schema：planr_schema
* 備份格式：pg_dump custom 壓縮格式 .dump，備份時不匯出權限、所有者資訊

### 路徑規劃（通用化脫敏）

* 備份儲存機備份根目錄：/data01/backup/postgresql/[DB主機標識]/
* DB 主機 PG 安裝路徑：/postgresql/pgsql-18.01/bin/
* DB 主機數據目錄：/pgdata01/pgdata-18.01
* 本地臨時存放目錄：/tmp/
* 備份檔命名規則：[庫名]_[日期時間戳].dump

### 備份機制

每日凌晨自動執行全庫邏輯備份，透過 SSH 傳輸至備份儲存機留存，保留多日備份快照。

## 2. 基礎傳輸操作（MobaXterm + PowerShell）

---powershell
### 密碼認證
scp -r root@<備份機IP>:/data01/backup/postgresql <本機本地路徑>

### SSH 金鑰免密下載
scp -r -P 2222 root@<備份機IP>:/data01/backup/postgresql <本機本地路徑>
---

> 備註：大資料夾建議使用 MobaXterm 左側 SFTP 圖形介面拖曳，支援斷點續傳。

## 2.2 備份機傳輸 dump 至 DB 主機 /tmp

---bash
scp root@<備份機IP>:/data01/backup/postgresql/[DB主機標識]/planr/*.dump /tmp/
---































備份檔命名規則：[庫名]_[日期時間戳].dump
備份機制
每日凌晨自動執行全庫邏輯備份，透過 SSH 傳輸至備份儲存機留存，保留多日備份快照。
