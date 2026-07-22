# PostgreSQL 備份驗證與維運操作手冊（已脫敏無敏感資訊）PostgreSQL Backup Validation and Operations Manual (Desensitized – No Sensitive Information).md

## PostgreSQL 資料庫備份腳本（Shell Script） .sh 執行檔

Linux 伺服器或一般工作站的習慣維運文件專區

/data01/backup/docs/ 或 /opt/scripts/docs/

~/documents/projects/xtrack/ 或 ~/Desktop/

如果你們有 Git，通常放在專案儲存庫的 /docs/ 子目錄下。

<REMOTE_DB_HOST> 和 <SSH_USER> 換成真實的 IP 與帳號

ssh -i ~/.ssh/id_rsa root@<REMOTE_DB_HOST> "echo OK" 確認不用輸入密碼就能連線。若失敗，排程也會卡住。

```code
#!/bin/bash
# =============================================================================
# pg_backup_78.sh - PostgreSQL Backup Script for XTrack Platform
#
#   Source : <REMOTE_DB_HOST>  (PostgreSQL 18.1)   # 解释：源数据库服务器地址（已脱敏）
#   Target : <LOCAL_BACKUP_HOST>  (this host - DevOps Hub)   # 解释：备份目标主机（注释，不参与执行）
#
# Workflow:
#   Phase 0 - Pre-flight checks (local/remote disk, SSH, DB connectivity)   # 解释：预检阶段
#   Phase 1 - pg_dump on <REMOTE_DB_HOST> via SSH, SCP back to <LOCAL_BACKUP_HOST>   # 解释：备份阶段
#   Phase 2 - Cleanup old backups (retention)   # 解释：清理旧备份
#   Phase 3 - Push summary metrics to Pushgateway   # 解释：推送监控指标
#
# Author : Kai
# =============================================================================

# Note: NOT using `set -e` so a single DB failure doesn't abort the whole run.   # 解释：不使用 -e，避免单库失败导致整个脚本退出
set -uo pipefail   # 解释：启用 -u（未定义变量报错）和 pipefail（管道中任一失败则整体失败），但不启用 -e

# -------------------------------------------------------------------
# Configuration
# -------------------------------------------------------------------
REMOTE_HOST="<REMOTE_DB_HOST>"   # 解释：远程数据库服务器地址（已脱敏）
SSH_USER="<SSH_USER>"   # 解释：SSH 登录用户名（已脱敏）
SSH_KEY="${HOME}/.ssh/id_rsa"   # 解释：SSH 私钥文件路径

LOCAL_BACKUP_DIR="/data01/backup/postgresql/78"   # 解释：本地备份存放目录
LOCAL_LOG_DIR="/data01/backup/logs"   # 解释：本地日志存放目录
REMOTE_TMP_DIR="/tmp/pg_backup"   # 解释：远程临时目录，用于暂存 dump 文件

RETENTION_DAYS=14   # 解释：备份文件保留天数
DISK_THRESHOLD_GB=5   # 解释：磁盘可用空间告警阈值（GB）

PUSHGATEWAY_URL="http://localhost:9091"   # 解释：Prometheus Pushgateway 地址
JOB_NAME="pg_backup_78"   # 解释：Pushgateway 作业名称
INSTANCE="<REMOTE_DB_HOST>"   # 解释：指标中的实例标签（源主机）

# Databases to back up. pg_dump runs as the 'postgres' superuser via peer auth,
# so no password file is needed. Add new DBs to this list.   # 解释：数据库列表，备份时使用 postgres 系统用户通过 peer 认证，无需密码文件
DATABASES=(
    "planr"   # 解释：示例数据库 planr
    "postgres"   # 解释：示例数据库 postgres（系统库）
)

# -------------------------------------------------------------------
# Initialization
# -------------------------------------------------------------------
TIMESTAMP="$(date +%Y%m%d_%H%M%S)"   # 解释：生成时间戳，格式如 20260721_120000
LOG_FILE="${LOCAL_LOG_DIR}/backup_pg78_${TIMESTAMP}.log"   # 解释：本次运行的日志文件路径
START_TIME=$(date +%s)   # 解释：记录脚本开始时间（秒数）

mkdir -p "${LOCAL_BACKUP_DIR}" "${LOCAL_LOG_DIR}"   # 解释：创建本地备份和日志目录（若不存在）
exec > >(tee -a "${LOG_FILE}") 2>&1   # 解释：将所有标准输出和错误同时输出到终端并追加到日志文件

log()  { echo "[$(date '+%Y-%m-%d %H:%M:%S')] $*"; }   # 解释：定义日志函数，输出带时间戳的信息
fail() { log "ERROR: $*"; }   # 解释：定义错误函数，输出 ERROR 前缀

SSH_OPTS="-i ${SSH_KEY} -o ConnectTimeout=10 -o BatchMode=yes -o StrictHostKeyChecking=accept-new"   # 解释：SSH 连接选项（密钥、超时、批处理、主机密钥检查）
SSH="ssh ${SSH_OPTS} ${SSH_USER}@${REMOTE_HOST}"   # 解释：构建完整的 SSH 命令
SCP_OPTS="${SSH_OPTS}"   # 解释：SCP 复用相同的 SSH 选项

SUCCESS_COUNT=0   # 解释：成功备份的数据库计数器
FAIL_COUNT=0   # 解释：失败备份的数据库计数器
TOTAL_COUNT=${#DATABASES[@]}   # 解释：需要备份的数据库总数

push_metrics() {
    # $1 = grouping path suffix (e.g. "" or "/database/planr")   # 解释：函数参数为指标分组路径后缀
    # stdin = metrics text   # 解释：通过标准输入传递指标文本
    local path_suffix="$1"   # 解释：将参数赋值给局部变量
    curl -s --max-time 10 --data-binary @- \
        "${PUSHGATEWAY_URL}/metrics/job/${JOB_NAME}/instance/${INSTANCE}${path_suffix}" \
        > /dev/null || log "WARN: pushgateway push failed"   # 解释：使用 curl 推送指标，超时10秒，失败则记录警告
}

log "=========================================="   # 解释：输出分隔线
log "PostgreSQL backup started"   # 解释：输出开始信息
log "  Source     : ${REMOTE_HOST}"   # 解释：显示源主机
log "  Databases  : ${TOTAL_COUNT} (${DATABASES[*]})"   # 解释：显示数据库总数及列表
log "  Retention  : ${RETENTION_DAYS} days"   # 解释：显示保留天数
log "=========================================="

# -------------------------------------------------------------------
# Phase 0 - Pre-flight Checks
# -------------------------------------------------------------------
log ""   # 解释：输出空行
log "[Phase 0] Pre-flight checks"   # 解释：标识进入预检阶段

# Local disk space
LOCAL_FREE_GB=$(df -BG "${LOCAL_BACKUP_DIR}" | awk 'NR==2 {gsub("G",""); print $4}')   # 解释：获取本地备份目录所在分区的可用空间（GB），去掉单位
if [ "${LOCAL_FREE_GB}" -lt "${DISK_THRESHOLD_GB}" ]; then   # 解释：判断可用空间是否低于阈值
    fail "Local disk below threshold (${LOCAL_FREE_GB}GB < ${DISK_THRESHOLD_GB}GB)"   # 解释：若低于则记录错误
    exit 1   # 解释：退出脚本，返回1
fi
log "  Local disk OK         : ${LOCAL_FREE_GB}GB free"   # 解释：本地磁盘检查通过

# SSH connectivity
if ! ${SSH} "echo OK" > /dev/null 2>&1; then   # 解释：尝试通过 SSH 执行 echo OK，丢弃输出
    fail "SSH to ${REMOTE_HOST} failed (check key & network)"   # 解释：若失败则记录错误
    exit 1   # 解释：退出
fi
log "  SSH connectivity OK"   # 解释：SSH 连通性检查通过

# Remote disk space (in /tmp where dumps land before SCP)
REMOTE_FREE_GB=$(${SSH} "df -BG /tmp | awk 'NR==2 {gsub(\"G\",\"\"); print \$4}'")   # 解释：在远程主机上检查 /tmp 分区可用空间
if [ "${REMOTE_FREE_GB}" -lt "${DISK_THRESHOLD_GB}" ]; then   # 解释：判断远程空间是否足够
    fail "Remote /tmp below threshold (${REMOTE_FREE_GB}GB)"   # 解释：不足则报错
    exit 1
fi
log "  Remote disk OK        : ${REMOTE_FREE_GB}GB free in /tmp"   # 解释：远程磁盘检查通过

# PostgreSQL connectivity (as postgres OS user, peer auth)
if ! ${SSH} "su - postgres -c 'psql -tAc \"SELECT 1\"'" > /dev/null 2>&1; then   # 解释：通过 SSH 以 postgres 用户执行 psql 测试查询
    fail "PostgreSQL connectivity test failed on ${REMOTE_HOST}"   # 解释：连接失败则报错
    exit 1
fi
log "  PostgreSQL OK"   # 解释：PostgreSQL 连接检查通过

# Ensure remote tmp dir exists & owned by postgres
${SSH} "mkdir -p ${REMOTE_TMP_DIR} && chown postgres:postgres ${REMOTE_TMP_DIR} && chmod 700 ${REMOTE_TMP_DIR}"   # 解释：在远程创建临时目录，设置所有者为 postgres，权限700

# -------------------------------------------------------------------
# Phase 1 - Database Dump
# -------------------------------------------------------------------
log ""   # 解释：空行
log "[Phase 1] Database dump"   # 解释：进入备份阶段

for DB in "${DATABASES[@]}"; do   # 解释：遍历所有数据库
    DUMP_FILE="${DB}_${TIMESTAMP}.dump"   # 解释：生成 dump 文件名，包含数据库名和时间戳
    REMOTE_PATH="${REMOTE_TMP_DIR}/${DUMP_FILE}"   # 解释：远程完整路径
    LOCAL_DB_DIR="${LOCAL_BACKUP_DIR}/${DB}"   # 解释：本地数据库子目录
    LOCAL_PATH="${LOCAL_DB_DIR}/${DUMP_FILE}"   # 解释：本地完整路径

    mkdir -p "${LOCAL_DB_DIR}"   # 解释：创建本地数据库子目录

    log "  -> ${DB}"   # 解释：输出当前处理的数据库名
    DUMP_START=$(date +%s)   # 解释：记录该数据库备份开始时间
    DB_STATUS=0   # 解释：初始化该数据库备份状态（0=失败，1=成功）
    FILE_SIZE=0   # 解释：初始化文件大小

    # pg_dump on <REMOTE_DB_HOST>
    #   -Fc      custom format (compressed, supports parallel restore, selective)   # 解释：自定义格式（压缩）
    #   -Z 6     compression level 6 (default; tune if CPU-bound)   # 解释：压缩级别6
    #   --no-owner --no-privileges  recommended for cross-environment restore   # 解释：不保存所有者与权限信息，便于跨环境恢复
    if ${SSH} "su - postgres -c 'pg_dump -Fc -Z 6 --no-owner --no-privileges -d ${DB} -f ${REMOTE_PATH}'"; then   # 解释：在远程以 postgres 用户执行 pg_dump，将输出保存到远程临时文件

        # Transfer back to <LOCAL_BACKUP_HOST>
        if scp ${SCP_OPTS} "${SSH_USER}@${REMOTE_HOST}:${REMOTE_PATH}" "${LOCAL_PATH}"; then   # 解释：使用 scp 将远程 dump 文件拷贝到本地
            FILE_SIZE=$(stat -c %s "${LOCAL_PATH}")   # 解释：获取本地文件大小（字节）
            FILE_SIZE_HUMAN=$(numfmt --to=iec --suffix=B "${FILE_SIZE}")   # 解释：将字节数转换为人类可读格式（如 1.2GB）
            DURATION=$(( $(date +%s) - DUMP_START ))   # 解释：计算该数据库备份耗时（秒）
            log "     SUCCESS  size=${FILE_SIZE_HUMAN}  duration=${DURATION}s"   # 解释：输出成功信息
            SUCCESS_COUNT=$(( SUCCESS_COUNT + 1 ))   # 解释：成功计数器加1
            DB_STATUS=1   # 解释：标记该数据库备份成功
        else
            fail "     SCP failed for ${DB}"   # 解释：若 SCP 失败则记录错误
            FAIL_COUNT=$(( FAIL_COUNT + 1 ))   # 解释：失败计数器加1
            DURATION=$(( $(date +%s) - DUMP_START ))   # 解释：计算耗时
        fi

        # Always remove the remote temp file
        ${SSH} "rm -f ${REMOTE_PATH}" || true   # 解释：删除远程临时文件（无论是否成功，|| true 忽略错误）
    else
        fail "     pg_dump failed for ${DB}"   # 解释：若 pg_dump 本身失败则记录错误
        FAIL_COUNT=$(( FAIL_COUNT + 1 ))   # 解释：失败计数器加1
        DURATION=$(( $(date +%s) - DUMP_START ))   # 解释：计算耗时
        ${SSH} "rm -f ${REMOTE_PATH}" || true   # 解释：同样尝试删除远程临时文件
    fi

    # Per-database metrics
    push_metrics "/database/${DB}" <<EOF   # 解释：调用 push_metrics 函数，分组路径为 /database/数据库名，并通过 here-doc 传递指标
pg_backup_status{database="${DB}"} ${DB_STATUS}   # 解释：指标：备份状态（1成功，0失败）
pg_backup_duration_seconds{database="${DB}"} ${DURATION}   # 解释：指标：备份耗时（秒）
pg_backup_file_size_bytes{database="${DB}"} ${FILE_SIZE}   # 解释：指标：文件大小（字节）
pg_backup_timestamp_seconds{database="${DB}"} $(date +%s)   # 解释：指标：本次备份的时间戳
EOF   # 解释：here-doc 结束
done   # 解释：数据库循环结束

# -------------------------------------------------------------------
# Phase 2 - Cleanup (Retention)
# -------------------------------------------------------------------
log ""   # 解释：空行
log "[Phase 2] Cleanup files older than ${RETENTION_DAYS} days"   # 解释：进入清理阶段

DELETED_DUMPS=$(find "${LOCAL_BACKUP_DIR}" -type f -name "*.dump" -mtime +${RETENTION_DAYS} -print -delete | wc -l)   # 解释：在本地备份目录查找超过保留天数的 .dump 文件，打印并删除，统计删除数量
DELETED_LOGS=$(find "${LOCAL_LOG_DIR}"   -type f -name "backup_pg78_*.log" -mtime +${RETENTION_DAYS} -print -delete | wc -l)   # 解释：在日志目录查找超过保留天数的日志文件，打印并删除，统计数量
log "  Deleted ${DELETED_DUMPS} old dumps, ${DELETED_LOGS} old logs"   # 解释：输出清理结果

# -------------------------------------------------------------------
# Phase 3 - Summary Metrics Push
# -------------------------------------------------------------------
log ""   # 解释：空行
log "[Phase 3] Push summary metrics"   # 解释：进入汇总指标推送阶段

TOTAL_DURATION=$(( $(date +%s) - START_TIME ))   # 解释：计算脚本总运行时长（秒）

push_metrics "" <<EOF   # 解释：调用 push_metrics，无路径后缀，推送全局汇总指标
pg_backup_total ${TOTAL_COUNT}   # 解释：指标：总数据库数
pg_backup_success_total ${SUCCESS_COUNT}   # 解释：指标：成功数
pg_backup_fail_total ${FAIL_COUNT}   # 解释：指标：失败数
pg_backup_run_duration_seconds ${TOTAL_DURATION}   # 解释：指标：本次总运行时长
pg_backup_last_run_timestamp_seconds $(date +%s)   # 解释：指标：本次运行结束时间戳
EOF   # 解释：here-doc 结束

log ""   # 解释：空行
log "=========================================="   # 解释：分隔线
log "Summary"   # 解释：输出汇总
log "  Total    : ${TOTAL_COUNT}"   # 解释：显示总数
log "  Success  : ${SUCCESS_COUNT}"   # 解释：显示成功数
log "  Failed   : ${FAIL_COUNT}"   # 解释：显示失败数
log "  Duration : ${TOTAL_DURATION}s"   # 解释：显示总耗时
log "=========================================="   # 解释：分隔线

[ "${FAIL_COUNT}" -gt 0 ] && exit 1 || exit 0   # 解释：若有任何数据库备份失败，则脚本返回 1，否则返回 0（供外部监控）
```

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

## 7. Docker 容器化 PG 擴充操作
## 7.1 備份檔傳入容器

```bash
docker cp /tmp/xxx.dump [容器名]:/tmp/
```

## 7.2 容器內驗證備份清單

```bash
docker exec -u postgres [容器名] pg_restore -l /tmp/xxx.dump
```

## 7.3 容器內隔離還原測試

```bash
# 建立測試庫
docker exec -u postgres [容器名] createdb test_planr_bak
# 還原備份
docker exec -u postgres [容器名] pg_restore -d test_planr_bak /tmp/xxx.dump
# 登入驗證
docker exec -u postgres -it [容器名] psql -d test_planr_bak
```

## 7.4 推薦隔離驗證方式（不動生產容器）

開啟臨時同版本 PG 容器，掛載備份目錄單獨測試，用完自動銷毀：

```bash
docker run --rm --name pg-temp-test -v /data01/backup/postgresql:/backup -e POSTGRES_PASSWORD=test postgres:18
```

## 7.5 容器重啟指令

```bash
docker restart [容器名]
```

## 8. 常見報錯與標準解決方案

|報錯資訊|根本原因|標準解法|
|-----|-----|-----|
|postgres is not in the sudoers file|當前使用者已是 postgres，不需要 sudo	移除 sudo -u postgres，直接執行指令|
|ERROR: schema "planr_schema" already exists|測試庫已還原過，物件重複|方案 1：dropdb 重建；方案 2：加 --clean --if-exists|
|pg_stat_user_tables 所有 n_live_tup = 0|PG 重啟後統計資訊未刷新，非真無數據|執行 analyze; 刷新統計後重新匯出行數|
|pg_restore: command not found|當前伺服器未安裝 PostgreSQL 客戶端|將備份檔傳送至有 PG 工具的 DB 主機驗證|
|FATAL: role "root" does not exist|PG 不允許 root 登入|切換 postgres 使用者執行 psql|
|pg_restore 執行後無任何輸出|預設靜默模式，無錯誤即執行成功|進庫查表確認還原結果|

## 9. 通用維運注意事項

1. PostgreSQL 完整重啟會強制中斷所有業務連線，必須協調業務低峰執行；僅修改配置優先使用 reload。
2. 備份驗證統一使用獨立測試庫，全程隔離正式數據，零風險。
3. 備份僅代表快照數據，正式庫後續新增 / 刪除數據會造成行數差異，屬正常現象。
4. 自訂編譯 / 解壓安裝的 PostgreSQL 無 systemd 管理，系統重啟後需手動拉起資料庫。
5. 大備份檔傳輸優先使用 SFTP 圖形工具，支援斷點續傳，避免 scp 中斷後重下。
6. 驗證完畢務必清理測試庫與臨時 dump/sql/txt 檔，釋放伺服器儲存空間。
