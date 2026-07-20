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

## 2.1 Windows PowerShell 下載遠端備份資料夾

``` powershell
### 密碼認證
scp -r root@<備份機IP>:/data01/backup/postgresql <本機本地路徑>

### SSH 金鑰免密下載
scp -r -i "C:\Users\本機使用者\.ssh\id_rsa" root@<備份機IP>:/data01/backup/postgresql <本機本地路徑>

### 非 22 自訂連接埠
scp -r -P 2222 root@<備份機IP>:/data01/backup/postgresql <本機本地路徑>
```

> 備註：大資料夾建議使用 MobaXterm 左側 SFTP 圖形介面拖曳，支援斷點續傳。

## 2.2 備份機傳輸 dump 至 DB 主機 /tmp

``` bash
scp root@<備份機IP>:/data01/backup/postgresql/[DB主機標識]/planr/*.dump /tmp/
```

## 3. 備份檔檢查流程

## 3.1 檢查備份檔完整性（無需連資料庫）

```bash
su - postgres -c "/postgresql/pgsql-18.01/bin/pg_restore -l /tmp/xxx.dump"
```

輸出完整 TOC 清單（Schema、表、索引、約束、數據區塊）= 備份檔完好；無輸出 / 報錯代表檔案損毀。

## 3.2 確認備份檔存在

```bash
# 列出當日備份
find /data01/backup/postgresql -name "*.dump" -mtime -1
# 檢視備份清單
ls -lh /tmp/*.dump
```

## 4. 隔離還原驗證標準流程（核心：完全不碰正式庫）

前置規則
永遠新建獨立測試庫 test_planr_bak，禁止直接還原至正式 planr 庫。

* 步驟 1：重建乾淨測試庫（解決物件已存在報錯）

```bash
# 刪除舊測試庫
su - postgres -c "/postgresql/pgsql-18.01/bin/dropdb test_planr_bak"
# 建立全新空白庫
su - postgres -c "/postgresql/pgsql-18.01/bin/createdb test_planr_bak"
```

* 步驟 2：還原備份至測試庫

```bash
su - postgres -c "/postgresql/pgsql-18.01/bin/pg_restore -d test_planr_bak /tmp/xxx.dump"
## 備選：不刪庫直接覆蓋還原
su - postgres -c "/postgresql/pgsql-18.01/bin/pg_restore --clean --if-exists -d test_planr_bak /tmp/xxx.dump"
```

* 步驟 3：登入測試庫確認表結構

```bash
su - postgres -c "/postgresql/pgsql-18.01/bin/psql -d test_planr_bak -c \"\dt planr_schema.*\""
```

* 步驟 4：測試完清理資源

```bash
# 刪測試庫
su - postgres -c "/postgresql/pgsql-18.01/bin/dropdb test_planr_bak"
# 清理臨時檔
rm -f /tmp/*.dump /tmp/*.sql /tmp/*.txt
```

## 5. 數據一致性三層比對方法
 
> 備份為過去時間點快照，正式庫後續有新數據寫入，少量行數差異屬正常；若完全一致代表備份時段後無業務變更。

* 方法一：批量匯出表行數快速比對（推薦日常驗證）

1. 刷新正式庫統計資訊（重啟 PG 後統計值會歸零）

```bash
su - postgres -c "/postgresql/pgsql-18.01/bin/psql -d planr -c \"analyze;\""
```

2. 匯出正式庫行數

```bash
su - postgres -c "/postgresql/pgsql-18.01/bin/psql -d planr -c "
SELECT relname, n_live_tup
FROM pg_stat_user_tables
WHERE schemaname='planr_schema'
ORDER BY relname;
"" > /tmp/prod_rows.txt
```

3. 匯出還原測試庫行數

```bash
su - postgres -c "/postgresql/pgsql-18.01/bin/psql -d test_planr_bak -c "
SELECT relname, n_live_tup
FROM pg_stat_user_tables
WHERE schemaname='planr_schema'
ORDER BY relname;
"" > /tmp/bak_rows.txt
```

4. 差異比對

```bash
diff /tmp/prod_rows.txt /tmp/bak_rows.txt
```

- 無輸出：所有表行數完全匹配
- 有輸出：列出行數不同的表

* 方法二：核心表哈希精準校驗（驗證內容一字不差）

```bash
# 正式庫哈希
su - postgres -c "/postgresql/pgsql-18.01/bin/psql -d planr -c \"
SELECT md5(string_agg(concat_ws('||', id_, code_, name_, delete_flag, created_on) ORDER BY id_)) AS table_hash
FROM planr_schema.cdp_project;
\""

# 測試庫哈希
su - postgres -c "/postgresql/pgsql-18.01/bin/psql -d test_planr_bak -c \"
SELECT md5(string_agg(concat_ws('||', id_, code_, name_, delete_flag, created_on) ORDER BY id_)) AS table_hash
FROM planr_schema.cdp_project;
\""
```

> 兩邊哈希值相同 = 該表備份快照數據完全一致。

* 方法三：全庫純文字 SQL 完整比對（災難演練使用）

```bash
# 匯出正式庫純文字備份
su - postgres -c "/postgresql/pgsql-18.01/bin/pg_dump -Fp -n planr_schema planr" > /tmp/prod_full.sql
# 匯出還原測試庫
su - postgres -c "/postgresql/pgsql-18.01/bin/pg_dump -Fp -n planr_schema test_planr_bak" > /tmp/bak_full.sql
# 逐行比對
diff /tmp/prod_full.sql /tmp/bak_full.sql
```

> 正常無風險差異說明
> 1. \restrict / \unrestrict 後隨機字串：pg_dump 每次動態生成安全令牌，不影響數據
> 2. SCHEMA 權限 GRANT 語句：備份參數 --no-privileges 未匯出權限，屬預期差異
> 除以上兩類差異外，剩餘結構、數據、索引、約束完全一致即備份有效。

## 6. PostgreSQL 伺服器維運操作

## 6.1 使用者規則

* PG 預設管理使用者：系統帳號 postgres
* 禁止使用 root 直接連線 PG，會報 role root does not exist
* postgres 使用者無 sudo 權限，操作時直接 su - postgres -c [指令]，不可加 sudo

## 6.2 重啟 PostgreSQL（自訂安裝、無 systemd 服務）

完整重啟（中斷所有連線，業務低峰執行）

```bash
su - postgres -c "/postgresql/pgsql-18.01/bin/pg_ctl restart -D /pgdata01/pgdata-18.01"
```

僅重載設定（不斷連線，修改 conf 優先使用）

```bash
su - postgres -c "/postgresql/pgsql-18.01/bin/pg_ctl reload -D /pgdata01/pgdata-18.01"
```

手動啟動（系統重啟後 PG 不會自動拉起）

```bash
su - postgres -c "/postgresql/pgsql-18.01/bin/pg_ctl start -D /pgdata01/pgdata-18.01"
```

## 6.3 重啟後驗證服務正常

```bash
# 確認 PG 行程存在
ps aux | grep postgres | grep -v grep
# 測試本地連線
su - postgres -c "/postgresql/pgsql-18.01/bin/psql -c 'select version();'"
# 確認5432埠監聽
netstat -tlnp | grep 5432
```

## 6.4 記憶體資源解讀範例

```bash
free -h
```

常見健康狀態：實體記憶體充足、Swap 完全未使用、可用記憶體剩餘量大，無記憶體瓶頸。
優化建議：業務量增長後調大 shared_buffers、work_mem 提升查詢快取效率。

















備份檔命名規則：[庫名]_[日期時間戳].dump
備份機制
每日凌晨自動執行全庫邏輯備份，透過 SSH 傳輸至備份儲存機留存，保留多日備份快照。
