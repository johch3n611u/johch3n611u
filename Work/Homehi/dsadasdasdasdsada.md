# 上傳到 Air-Gapped Nexus 的 SOP

> 適用對象:Nexus **push host** 操作者(團隊內少數同時有「外網出站」與「能到 Nexus」權限的人)。工程師端機器沒有外網,所有套件/檔案都從內部 Nexus 取用;這份文件描述**缺了什麼、怎麼補進 Nexus**。

---

## 0. 為什麼會有這份 SOP

內部網路的工程師機器無法連到公網(`registry.npmmirror.com`、Aliyun PyPI、Maven Central、VS Code Marketplace、oracle.com 全部連不到)。唯一的套件來源是內部 Nexus:

- **Nexus 套件/檔案:** `http://10.226.122.79:8081`
- **Nexus Docker registry:** `10.226.122.79:5050`(Docker image 上傳是 CI 的事,見 `CICD_HANDBOOK.md`,不在本 SOP 範圍)

「把東西弄進 Nexus」的標準做法,是**在 push host 上先從公網下載,再上傳到 Nexus**。push host 是一台同時具備:
1. 出站到公網的能力(通常需經公司 proxy,見 §2)
2. 連到 `10.226.122.79` 的能力

本 repo(`xtrack-infra`)的 `scripts/` 就是這套橋接工具的來源。所有指令皆對照既有工具實作,不要自行改寫流程。

---

## 1. 前置需求(第一次在 push host 上執行前)

| 項目 | 需求 |
|---|---|
| push host | 出站到公網 + 能連 `10.226.122.79`(Nexus、Docker registry 同 IP) |
| 工具 | `scripts/nexus-sync.exe`(或自己 `go build`)、`scripts/vscode-ext.ps1`、`scripts/oracle-client.ps1`、`curl`(Windows 10+ 內建)、`npm`、`pip` |
| Nexus 管理憑證 | admin 帳號(見 §10 安全與維運) |
| `NO_PROXY` | push host 上建議設 `NO_PROXY=10.226.122.79,10.226.122.80`;工具內建 bypass,但 `curl` 等手動指令需要 |
| PowerShell 執行原則 | `.ps1` 第一次跑報錯時先執行 `Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy RemoteSigned -Force` |

本 repo 檢查位置:`F:\BLProjects\xtrack-iservice\xtrack-infra-testing\scripts\`(README 範例以 `D:\Codespace\xtrack-infra\scripts` 為準)。

### Nexus hosted repositories 一覽

| Repository | 格式 | 用途 | 由誰上傳 |
|---|---|---|---|
| `maven-hosted-xtrack` | Maven2 | Java 依賴(`maven-settings.xml` 的 mirror target) | `nexus-sync.exe maven` 或 `curl -T` |
| `pypi-hosted-xtrack` | PyPI | Python wheels(**只鏡像 cp312**) | `nexus-sync.exe pypi` |
| `npm-hosted-xtrack` | npm | Node 套件 | `nexus-sync.exe npm` |
| `vscode-ext-xtrack` | raw | `.vsix` 檔 | `vscode-ext.ps1 upload` |
| `dev-tools-xtrack` | raw | Oracle Instant Client zip(`/oracle/` 下) | `oracle-client.ps1 upload` |

---

## 2. 通用原則

1. **永遠先 `--dry-run`**。工具會列出「缺哪些」而不上傳,先看過再動手。
2. **冪等、可重跑**。Nexus 回 400/409 = 「已存在」,工具視為成功。重跑不會炸掉。
3. **`--force` 才先刪後傳**,且工具保證「**先下載成功、再刪舊的**」,避免刪了卻傳不上去。只用於修 checksum 不符等狀況。
4. **Python 依賴一律 `==` 精確鎖定**。工具只吃 `requirements.txt` 的 `name==version`(見 §5)。
5. **Nexus 上沒有 `-SNAPSHOT` 依賴**,也不需要任何 `-SNAPSHOT`(Maven 同步自動跳過)。
6. 上傳失敗的套件會列在結尾 summary,依樣重跑即可(已傳的會被判定為「已存在」跳過)。

---

## 3. 情境 A — npm 套件同步

**前置**:目標專案要有 `pnpm-lock.yaml`(工具的套件來源;另外會併讀 `node_modules/.pnpm` 捕捉 lock 沒收錄的 phantom peer deps)。CI/部署目標是 **Linux x64**,非 linux-x64 的平台子套件(`@esbuild/linux-arm`、`@rollup/rollup-win32-`…等)工具會自動略過。

```powershell
# 補齊某個專案缺的 npm 套件
.\nexus-sync.exe npm --project D:\Codespace\front-end-react

# 先看缺哪些(不下載、不上傳)
.\nexus-sync.exe npm --project D:\Codespace\front-end-react --dry-run

# 重新同步全部(先刪後傳,修 checksum 不一致)
.\nexus-sync.exe npm --project D:\Codespace\front-end-react --force
```

- 工具從 `https://registry.npmmirror.com` 用 `npm pack` 拉原始 tarball。
- **不要**從本地 `node_modules` 打包上傳 —— `npm pack` 會重新壓縮導致 checksum 不一致(`nexus-sync.py` 註解明示)。

---

## 4. 情境 B — PyPI 套件同步

**前置**:`requirements.txt` 必須是 `name==version` 精確鎖定格式(工具只吃這種)。如果不是,先在專案裡產生:

```powershell
# 用 uv 導出 frozen requirements(在能跑該專案的環境)
uv export --frozen --no-dev -o requirements.txt
```

**同步整個專案的依賴:**

```powershell
.\nexus-sync.exe pypi --project D:\Codespace\kanban-iservice-backend
```

**臨時補缺的套件**(例如某個傳遞依賴不在 requirements.txt):用 `--packages`,工具會自動帶上它的傳遞依賴:

```powershell
.\nexus-sync.exe pypi --packages "SecretStorage==3.3.3,jeepney==0.8.0"
.\nexus-sync.exe pypi --packages "websockets==16.0" --force
```

- 工具用 `pip download` 從 `https://mirrors.aliyun.com/pypi/simple/` 拉 wheel,**只拉 `cp312`**(`--python-version 3.12 --abi cp312`),並同時抓 Linux manylinux、Windows win_amd64 與 pure-python「any」三種,讓 CI 的 Linux 建置與 Windows 開發機都能用。
- 因此 Nexus 上 C-extension 套件**只有 cp312 wheel**。工程師端必須用 Python 3.12(見 ONBOARDING 疑難排解)。

---

## 5. 情境 C — Maven 依賴同步

**前置**:本機(`~/.m2/repository`)已經透過一次 `mvn` build 把缺的依賴抓下來。工具**掃整個 `~/.m2/repository`**(不是讀專案 pom);`--project` 參數是必須填但**目前未使用其值**(只要求非空,不檢查路徑)。第一次跑會一次同步幾千個檔案,會比較久。

```powershell
.\nexus-sync.exe maven --project D:\Codespace\mrms-iservice-backend
.\nexus-sync.exe maven --project D:\Codespace\mrms-iservice-backend --dry-run
```

**工具行為**(對照 `main.go` `syncMaven` / `uploadMavenFile`):
- 掃描 `~/.m2/repository` 下所有 `.jar`、`.pom`、`.module`、`maven-metadata.xml`,用 **PUT** 保留 Maven 佈局路徑(`org/springframework/...`)上傳。
- **自動跳過** `-SNAPSHOT/` 目錄,以及 `.sha1`、`.md5`、`.lastUpdated`、`_remote.repositories` 等 metadata/checksum 檔。
- 已存在(HEAD 200)→ 跳過;`--force` → 全部重傳。

**工程師端消費設定**(不用 push host 管,順帶列出):`java/setup-dev.ps1` 會把 `java/maven-settings.xml` 放到 `~/.m2/settings.xml`,把**所有** Maven 請求 mirror 到 `http://10.226.122.79:8081/repository/maven-hosted-xtrack/`,並設 `checksumPolicy=ignore`。

---

## 6. 情境 D — raw 檔案上傳(raw repositories)

### 6.1 VSCode extensions( `.vsix` )

**前置**:編輯 `scripts/vscode-ext.ps1` 的 `$extensions` 陣列增刪套件。

```powershell
# 在 push host 上,先從公網 marketplace 拉 .vsix 到本機 cache(需 HTTPS_PROXY)
.\vscode-ext.ps1 download

# 再 PUT 進 Nexus raw repo vscode-ext-xtrack
.\vscode-ext.ps1 upload
```

告訴工程師端重新跑 `.\vscode-ext.ps1 install`(從 Nexus 拉 + `code --install-extension`)即可。

> 注意:`download` 用 Python 走 `HTTPS_PROXY`(內嵌帳密的 proxy 網址才可用);它會偵測 marketplace 回傳的 gzip 並解開,避免 `.vsix` 變成一顆 .gz 導致安裝失敗。

### 6.2 Oracle Instant Client( zip )

**前置**:在 push host 上先準備 zip(優先用手上現有檔案,免去從 oracle.com 下載):

```powershell
# 方式一:用 push host 現有的 zip
.\oracle-client.ps1 download -SourcePath C:\oracle\instantclient-basiclite-19.25-windows-x64.zip

# 方式二:直接從 oracle.com 拉(需 HTTPS_PROXY;可能被 license 頁擋)
.\oracle-client.ps1 download

# PUT 進 Nexus raw repo dev-tools-xtrack/oracle/
.\oracle-client.ps1 upload
```

工程師端 `.\oracle-client.ps1 install` 會從 Nexus 拉、解壓到 `%USERPROFILE%\.oracle\instantclient_19_25\` 並寫進 User `PATH`。

### 6.3 一般檔案 / 自建 artifact(泛用 PUT)

任何 raw repo 都接受直接 PUT,URL 格式為:

```
PUT http://10.226.122.79:8081/repository/<repo>/<path>
```

**範例 —— 手動補一個自建 Maven artifact**(放進 `maven-hosted-xtrack`,路徑要符合 Maven 的 GAV 佈局 `groupId 的點→斜線 / artifactId / version /`):

```powershell
curl -u admin:<password> -T mylib-1.0.0.jar `
  http://10.226.122.79:8081/repository/maven-hosted-xtrack/com/example/mylib/1.0.0/mylib-1.0.0.jar
```

建議依對照工具做法同時傳 `.pom`,並設對應 Content-Type(`.jar`→`application/java-archive`、`.pom`/`.xml`→`application/xml`、`.module`→`application/json`),否則 Maven 消費端可能因缺 POM/checksum 而解析失敗。

**其它 raw repo 範例**:

```powershell
# 進 vscode-ext-xtrack(對照 vscode-ext.ps1 的 PUT URL)
curl -u admin:<password> -T myext-1.0.0.vsix `
  http://10.226.122.79:8081/repository/vscode-ext-xtrack/myext-1.0.0.vsix

# 進 dev-tools-xtrack/oracle/(對照 oracle-client.ps1 的 PUT URL)
curl -u admin:<password> -T some-tool.zip `
  http://10.226.122.79:8081/repository/dev-tools-xtrack/oracle/some-tool.zip
```

---

## 7. 上傳後驗證

1. **再跑一次 `--dry-run`**,看到 `All packages already in Nexus!`(npm/pypi)/ `All artifacts already in Nexus!`(maven)即代表齊了。
2. **Nexus UI Browse**:`http://10.226.122.79:8081` → 左側 Browse → 選對應 repo,應看到剛上傳的 component/asset。
3. **消費端實測**(對照 ONBOARDING):
   - npm:`pnpm install`(registry 指向 `npm-hosted-xtrack`)
   - pip:`pip install -r requirements.txt`(index-url 指向 `pypi-hosted-xtrack/simple`)
   - maven:`./mvnw compile`(mirror 到 `maven-hosted-xtrack`)
4. **raw 檔案**:直接對照 repo 的 install 腳本實測(如 `vscode-ext.ps1 install`、`oracle-client.ps1 install`)。

---

## 8. 疑難排解

| 症狀 | 可能原因 | 處理 |
|---|---|---|
| `407 Proxy Authentication Required` | 公司 proxy 拒絕(工具內建 bypass,但 `curl` 等手動指令會碰到) | `$env:NO_PROXY = "10.226.122.79,10.226.122.80"`,或工具沒內建時改用工具 |
| checksum 不一致 / 想重傳 | 舊版本在 Nexus 上 checksum 壞掉 | `--force` 重傳(工具會先下載成功、再刪舊的) |
| 顯示某套件「已存在」直接跳過 | Nexus 回 400/409 視為成功 | 正常,冪等設計 |
| `pypi`:`No matching distribution found for <pkg>` | 該版本不是 cp312 / 只有 source | 確認版本在 Nexus 鏡像範圍;Nexus 只鏡像 cp312 wheel |
| npm tarball checksum 不符 | 用了本地打包而非 registry tarball | 用工具(從 `registry.npmmirror.com` 拉),不要自己 pack |
| `maven` 一次跑超久 | 第一次同步整個 `.m2/repository` | 正常;`--dry-run` 先看規模;已傳的會自動跳過,可分段重跑 |
| `connect to host` / 上傳 401 | push host 連不到 Nexus / 帳密失效 | 確認可達 `10.226.122.79:8081`;帳密見 §10 輪替 |
| `.ps1` 被擋:running scripts disabled | PowerShell 執行原則 | `Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy RemoteSigned -Force` |
| `.vsix` 安裝報「End of central directory record signature not found」 | 上傳了 gzip 假裝的 .vsix | 用 `vscode-ext.ps1 download`(會解 gzip),不要手動抓 |

---

## 9. 安全與維運

- Nexus admin 帳密以**明文**寫在本 repo 的 `scripts/main.go`、`scripts/vscode-ext.ps1`、`scripts/oracle-client.ps1` 常數中。這是私有網段、內部管理用,但:
  - **只允許少數 push host 持有**;工程師端只消費,不該拿 admin 憑證。
  - 不要在新文件/公開場合散佈密碼。
- **密碼輪替 SOP**(對照 README「Nexus credentials」):
  1. 改 `scripts/main.go` 的 `NexusUser` / `NexusPass` 常數。
  2. 同步改 `scripts/vscode-ext.ps1`、`scripts/oracle-client.ps1` 內對應常數。
  3. 重新 `go build`(Go 1.25.5),**同時提交 repo root 與 `scripts/` 兩個 `nexus-sync.exe`**。
  4. Nexus 端改密碼。
- 輪替期間,已存在的套件不受影響;工程師端消費無需知道 admin 密碼。

---

## 10. 相關文件

| 文件 | 內容 |
|---|---|
| [`../README.md`](../README.md) | repo 總覽 + nexus-sync 基本用法 |
| [`ONBOARDING.md`](ONBOARDING.md) | 工程師端從 Nexus 消費的完整設定(consumption 面) |
| [`CICD_HANDBOOK.md`](CICD_HANDBOOK.md) | Docker image 建置/推送/部署(Docker registry 面) |
| [`../scripts/main.go`](../scripts/main.go) | `nexus-sync` 三個 subcommand 的完整實作與 flag |
| [`../java/maven-settings.xml`](../java/maven-settings.xml) | Maven 消費端 mirror 設定 |
