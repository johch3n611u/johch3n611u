# XTrack iServices CI/CD — 記憶體崩潰風險分析報告

> 檢查對象:`F:\Documents\YuChengDocuments\Process\BLProjectsOverview.md`(CI/CD 流程詳解 第 10 節)與 `F:\BLProjects\xtrack-iservice` 各 repo 的 CI/CD Pipeline 設定
> 分析日期:2026-08-14
> 性質:唯讀分析報告(未修改任何設定檔)

---

## 結論

**會,存在多個可能造成記憶體崩潰的風險,其中有 4 個高風險項目。** 但主要**不是**透過「無限制開啟新 container」——目前架構沒有 `--scale`/`replicas`/`docker run` 迴圈,容器數量固定各 1 份,也沒有形成 pipeline 無限迴圈(拓撲是單向 app→infra)。真正的殺手是「容器記憶體限制失效 + JVM 無 heap 上限 + 無限重啟」三個機制疊加,再加上 CI 建置無併行節流。

---

## 四個主要記憶體崩潰機制

### H1. 所有容器記憶體限制是「死設定」⚠️ 最嚴重

記憶體上限全部寫在 `deploy.resources.limits`,但**非 swarm 模式的 `docker compose up` 不會套用 `deploy` 區塊**(該區塊只對 `docker stack deploy`/Swarm 生效)。

- 寫好的限制:`docker-compose.yml:33-36`(auth 256M)、`55-58`(kanban 256M)、`79-82`(mrms 512M)、`99-102`(frontend 64M)、`docker-compose.infra.yml:23-26`(Elasticsearch 1G)
- 實際部署指令是 `docker compose ... up -d`(`xtrack-infra-testing/.gitlab-ci.yml:25-26`),非 swarm、無 `--compatibility`
- **結果:5 個容器實際「完全沒有記憶體上限」**,任一服務洩漏/burst 可吃光整台 host 的 RAM

### H2. 兩個 JVM 服務無 `-Xmx`

auth 與 mrms 的 Dockerfile 都是 `ENTRYPOINT ["java", "-jar", "app.jar"]`(`auth-service-testing/Dockerfile:14`、`mrms-iservice-backend-testing/Dockerfile:14`),在無容器限制下,JVM 預設把 max heap 設為 **host 實體 RAM 的 25%**。兩個服務合計最多吃掉半台記憶體當 heap,再疊 metaspace/threads/JIT/direct buffer。

### H3. OOM 絞殺循環(無限重啟)

`docker-compose.yml:14/40/62/86` 四個服務全部 `restart: unless-stopped`,且**全 repo 沒有任何 healthcheck**。一旦容器被 host OOM killer 殺掉 → 自動重啟 → 再吃記憶體 → 再被殺,Docker 的 restart 退避會成長到約 5 分鐘但**永不停止**,host 反覆進入記憶體壓力狀態。

### H4. CI 建置無併行節流

4 個 app repo 的 `build` job 都**沒有** `only`/`rules`(任何 push/tag/MR 都建)、沒有 `resource_group`/`timeout`/`retry`,例如 `auth-service-testing/.gitlab-ci.yml:9-16`。每個 build 內部是 Maven(1–2GB)+ Node/Vite(1–2GB)工具鏈,走 host docker socket(`DOCKER_HOST: unix:///var/run/docker.sock`)。runner `concurrent ≥ 2` 且多 repo 同時 push 時,runner 主機極可能 OOM。

---

## 完整風險清單

### 高風險

| # | 風險 | 關鍵證據 |
|---|------|---------|
| H1 | 容器記憶體限制失效 → 任一服務可吃光 host RAM | `deploy.resources.limits` vs `docker compose up` |
| H2 | JVM 無 `-Xmx` → 各預設 25% host RAM heap | 兩個 Dockerfile 的 ENTRYPOINT |
| H3 | OOM 絞殺循環:被殺 → 無限重啟 → 再被殺 | `restart: unless-stopped` × 無 healthcheck |
| H4 | CI 併發 build OOM runner | build job 無任何節流欄位 |
| H5 | **tag 變數是「死變數」**:trigger 傳入的 `AUTH_TAG`/`MRMS_TAG`/`BACKEND_TAG`/`FE_TAG` 到 infra pipeline 後**從未被 export 到遠端主機**(`xtrack-infra-testing/.gitlab-ci.yml:17-26` 只 export 5 個 secret),遠端讀 `.env.testing:5-8` 內定 `latest` → 每次 push 都 pull `latest` + 重建容器;且 testing/main 共用同一 `:latest`(最後 push 者覆蓋),**SHA 回滾機制實際上無法運作** | 變數在 trigger 端設、在部署端未被消費 |

### 中風險

| # | 風險 | 說明 |
|---|------|------|
| M1 | 每次 push 都 pull `latest` + `up -d` recreate 該服務 | 高頻 commit 造成容器 churn 與短暫斷線 |
| M2 | 手動 web trigger 且 `SERVICE_NAME` 為空 → 整套 stack 全量 pull + 重建 | 單次高影響,非迴圈 |
| M3 | 無 `pids_limit`/`cpus` | JVM/Python 執行緒爆量、busy-loop 不受限 |
| M4 | Elasticsearch heap 限 512M 但容器無上限 | off-heap(mmap/direct buffer)在索引尖峰可吃光 host RAM |
| M5 | 每次部署 host 累積一張 `:SHA` image,無任何 prune/保留策略 | 磁碟隨時間耗盡 |
| M6 | Filebeat 完全無資源設定,掛載 docker.sock + 全量容器 log | 磁碟/記憶體皆無上限 |
| M7 | Traefik 以 `docker run` 另起(`docker-compose.infra.yml:3`),不在任何 compose 內 | 無可見資源/restart 設定 |

### 低風險

| # | 風險 | 說明 |
|---|------|------|
| L1 | 目前無無限迴圈(拓撲單向、infra `only: [triggers, web]` 自防自觸發) | 日後若 infra 改成會 push tag 回 app 就會閉合成迴圈 |
| L2 | `scripts/nexus-mirror.py` 全量 mirror 時 `all_files` 清單在記憶體累積 | 數百萬筆時數百 MB~GB |
| L3 | `dev-db/manage.ps1` 為每人建 2 個 **database(非 container)** | 有 `drop`/`reset` 清理但需人為執行;是 DB host 磁碟而非 app host RAM |
| L4 | `scripts/main.go` 上傳時整包 buffer(2 workers) | 僅超大套件才有感 |
| L5 | 無 job `timeout`,卡住的 `docker build` 佔住 runner slot 達 GitLab 預設 60 分鐘 | registry/網路異常時 runner 資源枯竭 |

---

## 一句話總結

> 目前 pipeline 是「無測試、無節流、無有效記憶體限制、tag 變數失效、每次 push 重建 latest 並重啟服務」的直通管線。**最現實的崩潰路徑**:任一服務記憶體洩漏/burst → host OOM killer 介入 → `unless-stopped` 無限重啟 → 反覆記憶體絞殺;加上多 repo 同時 push 時 runner 主機建置 OOM。不是「無限開 container」,但效果同樣是資源耗盡。

## 最重要的三條證據(可在 host 上驗證)

1. 記憶體限制全寫在會被忽略的區塊:`docker-compose.yml:33-36` `deploy: resources: limits: memory: 256M`
2. 部署走非 swarm 的 `docker compose up`:`xtrack-infra-testing/.gitlab-ci.yml:25-26`
3. JVM 無 `-Xmx`:`auth-service-testing/Dockerfile:14` / `mrms-iservice-backend-testing/Dockerfile:14`

---

## 建議修復方向(僅供參考,未動任何檔案)

若要在部署環境修復,優先順序為:

1. **讓限制生效**(R1):`deploy.resources.limits` → 改用 `mem_limit`(任何 `docker compose up` 版本都明確生效),並補 `pids_limit`/`cpus`。對象:`xtrack-infra-testing/docker-compose.yml`(4 個服務)+ `docker-compose.infra.yml`(elasticsearch/filebeat)。
   - 建議值:auth 512m、kanban 512m、mrms 1024m、frontend 128m、elasticsearch 1g、filebeat 128m;`pids_limit: 512`、`cpus: 1.0`。
   - **是否需要 rollback**:此變更為純設定、可逆、無資料遷移。回滾方式 = git revert/手動還原 compose 檔 + `docker compose up -d` 重建即可。唯一營運注意事項:若 `mem_limit` 設得比服務目前實際用量低,容器會開始觸頂被 OOM-kill(`docker inspect`/`docker stats` 可見),所以上線時宜先觀察 `docker stats` 再設定最終值。
2. **JVM heap 上限**(R2):Dockerfile 加 `-XX:MaxRAMPercentage=75`,或 compose environment 加 `JAVA_TOOL_OPTIONS: -XX:MaxRAMPercentage=75`(不必重新 build image,最快)。
3. **中斷絞殺循環**(R3):加 `healthcheck` + `start_period`;對易 crash 服務改用 `restart: on-failure:N`。
4. **CI 節流**(R5):build job 加 `rules`(限定 testing/main push 才建)、`timeout`、`retry`、可選 `resource_group`。
5. **修 tag 變數**(R6):在 infra ssh 段 export `$AUTH_TAG`/`$MRMS_TAG`/`$BACKEND_TAG`/`$FE_TAG`,讓 SHA 部署與回滾真正可用;修正 front-end `VITE_MODE` 層級(R8)。
6. **長期清理**(R9-R11):`docker image prune --filter until=720h`、Traefik 納入 compose 管理、dev-db 離職清理。

## 在 host 上的驗證方式

```bash
# 確認限制是否真的生效(非 0 才有生效)
docker inspect --format '{{.HostConfig.Memory}} {{.HostConfig.PidsLimit}}' <container>
docker stats
docker compose --env-file .env.testing config | grep -A3 mem_limit
```

---

*此報告由 Claude Code 分析生成,基於 `F:\BLProjects\xtrack-iservice` 5 個 repo 的 `.gitlab-ci.yml`、`docker-compose.yml`、`docker-compose.infra.yml`、Traefik 設定、部署腳本、4 份 Dockerfile、`.env.testing`、`CICD_HANDBOOK.md`、`dev-db/manage.ps1` 之實際內容。*
