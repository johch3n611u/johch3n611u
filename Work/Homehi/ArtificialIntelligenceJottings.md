
# AI Coding 2026 年 8 月新聞整理

> 承接〈ArtificialIntelligenceJottings〉（AI 程式編輯 / AI 全領域專有名詞時間軸）的 2026-08 最新動向
> 整理日期：2026-08-17 ｜ 語言：繁體中文 ｜ 風格：延續原筆記「編號 + 一句話白話解釋」

---

## 摘要（30 秒版）

2026 年 8 月，AI Coding 有三件大事同時發生，方向一致——**「Agent 外殼（Harness）」與「插件生態」成了新的兵家必爭之地**：

1. **8/6** — AWS、Cursor、GitHub、微軟、OpenAI、Vercel 等六巨頭共同發布開放插件標準 **Agent Plugins 1.0.0**，要統一「AI 智能體的包裝盒」；發明這套玩法的 **Anthropic 反而缺席**。
2. **8/13** — **DeepSeek 發布 Harness v0.1**（MIT 開源），主張「一切皆插件」，把模型、工具、UI、沙箱全部模組化，24 小時內在 GitHub 狂攬 7 萬星，被形容為「用《我的世界》的方式幹掉 Claude Code」。
3. **8 月持續** — **騰訊 WorkBuddy** 以 2097 萬次 PC 月訪問量登頂國內辦公智能體，廣告鋪滿北上廣深地鐵；同時 Claude Code / Codex 仍是海外主力。

**一句話總結：** 2026 下半年，AI 編程的競爭已經從「哪個模型強」升級到「誰的 Agent 外殼＋插件生態能留住開發者」。

---

## 一、2026-08 事件時間軸（重點清單）

| 日期 | 事件 | 意義 |
|---|---|---|
| 8/6 | **Agent Plugins 1.0.0** 開放規範公開 | 六大廠商統一智能體插件「包裝盒」，Anthropic 缺席 |
| 8/6 | 谷歌（Google）發布當天追加為核心維護者 | 開放標準的陣營又 +1 |
| 8/8 起 | 騰訊 WorkBuddy 廣告「包圍北上廣深」 | 騰訊近年少見的飽和式產品投放 |
| 8/13 凌晨 | **DeepSeek V4 Pro** 正式推出（強化 Agent 能力） | DeepSeek 模型層更新 |
| 8/13 | DeepSeek **API 調價**、採用**峰谷分時定價** | 為 Harness 落地做價格配套 |
| 8/13 晚 | **DeepSeek Harness v0.1（DSH）** 開發者預覽版 + MIT 開源 | 首款 Agent 產品，24h 內 7 萬星 |
| 8/14 | 澎湃、36氪、快科技等大量報導 | 「Harness」一詞開始出圈 |
| 8 月上旬 | 傳 OpenAI Codex 月燒 200 美元、Trae 永久免費、WorkBuddy 日活登頂 | 桌面智能體選型被熱議 |

### 承接 7 月的動向（一起看）
- **7/6** — 騰訊混元 **Hy3** 大模型發布，WorkBuddy 首發接入並限時免費，兩天後算力排隊率一度超過 50%。
- **7 月** — **MCP 規格「無狀態化」大改版**，瞄準企業級規模部署。
- **7 月** — 首份 **AI Programmer Index** 出爐：Cursor + Claude Opus 4.7 險勝 Codex 奪冠。
- **7 月** — Claude Code 佔公開 GitHub 提交約 4%，年化營收傳達 25 億美元。

---

## 二、新專有名詞補充（承接原筆記，編號延續 AI 全領域時間軸 76 起）

> 原筆記已涵蓋到「推理模型 + Agent 原生時代（2025～2026）」。以下補上 2026-08 出現 / 爆紅的新名詞，維持「一句話白話解釋」。

76. **Harness（繮繩 / Agent 外殼）— 2026 年成為熱詞**
    原義是馬具、繮繩；在 Agent 語境裡指「模型之外那一整套工程外殼」——讓模型能讀檔案、調工具、管上下文、失敗重試、連續工作幾個小時的系統。官方公式：**Model + Harness = Agent**。

77. **DSH（DeepSeek Harness）— 2026 年 8 月 13 日發布**
    DeepSeek 首款 Agent 產品，開發者預覽版 v0.1，MIT 開源；一行命令 `npx @deepseek-ai/dsh web` 就能在本地瀏覽器拉起一個網頁版 Coding Agent。

78. **一切皆插件（Everything is a Plug-in）— 2026 年 DeepSeek Harness 主張**
    模型、工具、技能、會話、沙箱、儲存、迴圈、調度、UI……所有 Agent 能力都是可替換的插件；甚至「模型本身」和「你看到的網頁界面」也只是預設掛載的插件。

79. **Cordis — 2026 年因 DSH 被熟知**
    從 Koishi（QQ 機器人框架）抽出來的微內核，負責插件載入/卸載與依賴管理，特色是「可逆副作用」——卸載插件時自動回收副作用，做到熱插拔。已在 Koishi 運作四年、有 4000+ 社群插件驗證。

80. **Trajectory（軌跡）— DSH 的隱藏亮點**
    Agent 看到的系統提示、推理過程、工具呼叫結果、子 Agent 調度、每次上下文注入，全部記進一份「只增不改」的會話日誌，可復盤、分叉、回放——等於每個 Agent 都有黑盒子。

81. **Agent Plugins（Agent 插件開放規範）— 2026 年 8 月 6 日公開**
    六巨頭定的一套「智能體插件包裝盒」標準：一個插件就是一個資料夾，根目錄 `plugin.json` 清單 + `skills/` 技能 + `mcp.json` MCP 設定，一份包走天下。

82. **Vibe Working（感覺辦公）— WorkBuddy 帶紅的詞**
    Vibe Coding（感覺編程）的辦公版延伸：用口語描述「你想要什麼感覺」，AI 自動把文件、簡報、程式、郵件做完。

83. **AI Programmer Index — 2026 年 7 月首發**
    首個衡量 AI 程式員能力的指數，以實際程式任務排名各家 Coding Agent（首屆：Cursor + Opus 4.7 險勝 Codex）。

84. **MCP 無狀態化（Stateless MCP）— 2026 年 7 月規格改版**
    MCP 從「每次都要記住上一輪狀態」改成無狀態設計，大幅簡化規模化部署，瞄準企業級場景。

---

## 三、DeepSeek Harness 專章：用《我的世界》的方式幹掉 Claude Code？

### 3.1 它是什麼？

2026 年 8 月 13 日晚間，DeepSeek 宣布 API 調價幾個小時後，甩出首款 Agent 產品 **DeepSeek Harness v0.1 開發者預覽版**，MIT 協議、程式碼全開。

官方給出的公式很乾脆：**Model + Harness = Agent**。模型負責思考推理，Harness 負責實際執行。官方團隊把模型比作「Agent 的靈魂」，Harness 則「給予 Agent 理解環境、使用工具，並在真實場景中持續工作的能力」。

安裝只要一行命令，跑在你自己的機器上、界面開在瀏覽器裡：

```bash
npx @deepseek-ai/dsh web
```

### 3.2 最大賣點：「一切皆插件」

DSH 的核心理念是「一切皆插件」——**模型、工具、技能、會話、沙箱、儲存、迴圈、調度、UI，所有 Agent 能力均由插件組合而成，可自由替換、靈活重組**。

整套框架基於 **Cordis 插件元框架**打造（出自 Koishi 社群，作者 Shigma）。開發者不需修改專案原始碼，就能透過新增 / 替換插件實現任意模組的自訂與擴充。

**四種預設模式**（本質是四份插件配置清單）：

| 模式 | 內容 | 用途 |
|---|---|---|
| **標準模式** | 檔案編輯、Shell、搜尋、技能、規劃、子 Agent、工作流全套 | 通用開發 |
| **PTC 程序化工具調用** | 模型直接寫 TypeScript 程式，把多次工具呼叫合併成一次執行 | 減少來回往返、省 Token |
| **極簡模式** | 只留一個持久 bash + 一個檔案編輯器 | 給模型做基準測試（看裸實力） |
| **創造模式** | 可查看執行時狀態、記憶體內調試 Cordis 插件 | 自訂全新模式 |

### 3.3 生態爆炸：288 個插件倉庫、24 小時不到

發布不到 24 小時，GitHub 上的 `dsh-plugin` 話題已收錄 **288 個插件倉庫**（社群目錄站 `awesome-dsh-plugins` 每日追蹤相容性）。因為內測期已鋪墊一個多月，生態是一夜之間浮出來的。

社群插件充滿「玩心」：
- **dsh-plan-execute** — 雙模型路由：規劃用推理模型、執行切換到便宜模型，省錢。
- **dsh-vision** — 給純文字 DeepSeek 模型「裝眼睛」，橋接任意 OpenAI 相容視覺模型。
- **dsh-qq2006 / dsh-ui-whale / whale-girl** — 換 2006 年 QQ 皮膚、像素鯨魚、鯨魚娘桌寵。
- **dsh-ads / dsh-anti-ads** — 加廣告的插件 vs 擋廣告的插件，生態自帶攻防。
- **dsh-gomoku** — 跟 AI 下五子棋。
- 沙箱也有三種可選（sandbox-micro / sandbox-mxc / sandbox-nono），「Agent 在什麼籠子裡跑程式」也是設定項。
- 還有遠端渠道插件（QQ、微信、飛書、企業微信、Telegram），讓 DSH 變成能被 @ 的機器人。

關鍵洞察：**在 DSH 裡，「用哪個模型」和「模型有什麼能力」都被降級成皮膚級的設定**——跟換主題是同一個操作。這跟 Claude Code 截然不同：後者的模型、能力、上下文壓縮全是 Anthropic 單方面決定的。

### 3.4 戰略意圖：不是「又一個 Claude Code」，是「讓繮繩免費」

36氪的結論很尖銳：**Claude Code 是一個意見極其強烈的產品**——工具集、權限模型、上下文壓縮、子 Agent 調度全是 Anthropic 訂死的，訂閱費買的就是「模型與外殼互相咬合」這套，拆不開、抄不走。Harness 在 Anthropic 手裡是產品、是收入、是壁壘。

而 DSH 從第一天就選了「**把這個品類本身開源掉**」的路：不跟你比誰的繮繩編得好，直接宣布**繮繩這個東西不該收錢**。配合 8/13 同一天的 API 峰谷調價，DeepSeek 的算盤是：**只要 Harness 被拉平成公共品，競爭就被壓回「模型本身的能力與價格」**——而那正是 DeepSeek 的主場。

### 3.5 外界評價與短板

- **Armin Ronacher**（Flask 創始人）：「一些很酷的想法，讓我重新思考產品裡 Harness 重構的方式。」
- 有人用完全相同的任務（Three.js 滑沙遊戲）在 DSH、Reasonix、Codex 中跑，**DeepSeek V4 Flash + DSH 的組合表現最好**。
- **短板**：任務耗時較長、Token 消耗大；使用體驗複雜（Skills 加載機制、CLI 方式都有學習成本），「技術架構走得比產品體驗快」。
- **相容性警告**：官方 README 用全大寫標註 `THERE WILL BE COMPATIBILITY-BREAKING CHANGES`（一定會有破壞相容的變更）；社群還存在「DSH 可行攻擊鏈 demo」倉庫——人人可掛插件、插件能碰 Shell 和檔案系統的架構，攻擊面不言而喻。

> 來源：[36氪 矽星人深度體驗](https://www.36kr.com/p/3938774780263814) ｜ [澎湃新聞：Harness 是什麼？有什麼特別？](https://m.thepaper.cn/detail/33783308) ｜ [快科技（DoNews 轉載）](https://www.donews.com/news/detail/1/6670751.html)

---

## 四、騰訊 WorkBuddy 專章：辦公智能體的「小龍蝦」

### 4.1 它是什麼？

WorkBuddy 是騰訊旗下的**桌面 / 辦公 AI 智能體**，2026 年在國內爆紅。媒體形容它是一隻「小龍蝦」——因為 2026 年初 OpenClaw 在國內快速走紅後，騰訊內部接連冒出 QClaw、WorkBuddy、Marvis 等多款「龍蝦」產品，被外界稱為「百蝦大戰」；直到 7 月騰訊把 QClaw 相關團隊調整到 WorkBuddy 部門，「百蝦大戰」正式收口，資源向 WorkBuddy 集中。

### 4.2 為何爆火？（四大原因）

1. **背靠騰訊生態**：已接入騰訊文件，並與 ima、騰訊會議、企業微信、騰訊樂享等知識/協作產品聯動；大量「開箱即用」本土 Skill（QQ 郵箱、公眾號助手、小紅書助手、飛書、谷歌全家桶）。
2. **低價 + 免費策略**：會員、積分、模型調用結合的收費方式，註冊送積分、邀請獎勵、模型限免。相比成本高的 Codex、帳號風控嚴格的 Claude Code，門檻極低。
3. **多模型可選**：同時接入**混元、DeepSeek、GLM、Kimi**，用戶依任務與成本選模型（Hy3 首發時，主動選 Hy3 的比例超過 60%）。
4. **對話式互動**：把使用門檻降到最低。

### 4.3 數字與定位

- 易觀報告：**2026 Q2 國內辦公智能體平台 PC 端月訪問量 2097 萬次，排名第一**，超過第二名 TRAE 與第三名 QoderWork 的總和。
- 月活 2000 萬級、日活百萬級，是國內用戶規模最大的辦公智能體應用。
- 技術源自騰訊雲 2023 年啟動的 AI 程式工具 **CodeBuddy**；2026 年 1 月 Claude Cowork 發布後，負責人基於既有智能體平台提出構想，一個週末做出初代。
- 騰訊內部有「繼 QQ、微信後第三個戰略級產品」的說法，馬化騰親自參與產品會議。
- 企業端是下一階段重點：生態團隊正在大量接觸企業、推進採購與聯合開發，下半年主打政企、製造業、網際網路可複製方案。

### 4.4 與 Claude Code / Cursor 的定位差異

| | **WorkBuddy** | **Claude Code / Codex** | **Cursor** |
|---|---|---|---|
| 主力場景 | 辦公、文件、知識、日常任務 | 終端機 / 軟體工程 | AI 原生 IDE |
| 目標用戶 | 辦公室上班族、非程式人 | 開發者 | 開發者 |
| 特色 | 本土化 Skill、多模型、免費用戶多 | 工程能力強、外殼與模型咬合 | 專案理解、補全體驗 |
| 收費 | 低價/免費 + 積分 | 訂閱制 | 訂閱制 |

> 來源：[新浪財經/界面新聞：騰訊大力把 WorkBuddy 送上牌桌](https://finance.sina.com.cn/roll/2026-08-08/doc-inimqtzr4218954.shtml)

---

## 五、AI 插件體系 / 插件標準專章

### 5.1 Agent Plugins 1.0.0（8/6 開放規範）

8 月 6 日，一份名為 **Agent Plugins 1.0.0** 的開放規範正式公開，目的是給 AI 智能體的插件定一個**統一的「包裝盒」**——「一次打包，就能在所有相容的智能體客戶端裡通用」。

**共建者**：AWS、Anysphere（Cursor 母公司）、GitHub、微軟、OpenAI、Vercel 六家，**谷歌在發布當天追加為核心維護者**。**唯獨少了開創者 Anthropic。**

**規範內容（一個插件 = 一個資料夾）：**

```
my-plugin/
├── plugin.json          ← 必填：$schema 與 name，其餘靠固定位置
├── skills/              ← Agent Skills（可重用的指令與資源）
│   └── summarize/
│       ├── SKILL.md
│       ├── scripts/
│       └── references/
├── mcp.json             ← MCP 伺服器設定（stdio / Streamable HTTP / 舊版 HTTP+SSE）
└── com.example.client/  ← 各家「私貨」放反向網域名稱目錄（別人掃到就略過）
```

**關鍵設計哲學：**
- **只統一包裝盒，不管運行**：安裝、分發、權限、沙箱、認證、信任驗證、UX 全留給各家客戶端自己處理。
- **只管兩類可攜組件**：skills/ 與 mcp.json；hooks、斜杠命令、custom agents 還是各家地盤。
- **規範仍標註「工作草案」**，離成熟標準還有距離。
- 一句話：**Agent Skills 管指令，MCP 管連工具，Agent Plugins 管把這兩個裝進同一個包裝盒。**

### 5.2 為什麼說「撞臉 Claude」？

這套格式跟 **Claude Code 一直用的插件系統**幾乎一樣：`plugin.json` + `skills/` + `mcp.json` + `commands` + `agents`，Anthropic 是最早跑通「插件 = 技能 + MCP + 清單」打包思路的公司，甚至開放了兩個官方插件市場。

這次的開放標準，基本照著 Anthropic 家格式長，Anthropic 卻沒上桌。微軟 VS Code 預設插件市場裡有 `anthropics/claude-code`，一邊支援新格式、一邊繼續認 `.claude-plugin/plugin.json`；OpenAI 的 Codex 甚至保留 Claude 原來的變數名，只為相容既有 Claude 插件。

**為什麼缺席很關鍵**：底層標準一旦談攏，競爭就會上移到「插件生態誰更大、誰讓開發者第一個想到自己」。Anthropic 向來更願意自己蓋自己的樓，這次它選擇繼續經營從格式到市場、再到分發的完整閉環。

### 5.3 MCP 的演進（承接原筆記 54 條）

- **MCP（2024/11 Anthropic 發布）**：讓任何 AI 都能連上各種軟體、資料庫與工具的通用接頭標準。
- **2026 年 7 月無狀態化改版**：MCP 走向無狀態設計，瞄準企業級規模部署——這是它從「開發者玩具」走向「企業基礎設施」的關鍵一步。
- **MCP vs 插件**：MCP 是「工具連線協定」，插件是「能力打包單位」，兩者互補；Agent Plugins 規範正是把 MCP Server 收進包裝盒的一部分。

> 來源：[智源社區/新智元：六巨頭定 AI 插件新標準](https://hub.baai.ac.cn/view/56983) ｜ [Agent Plugins 官方網站](https://agent-plugins.org/) ｜ [Ars Technica：MCP 無狀態化](https://arstechnica.com/ai/2026/07/with-a-stateless-makeover-new-mcp-spec-targets-enterprise-scale/)

---

## 六、其他 2026-08 值得關注的動向

### 6.1 Ponytail（最懶高級工程師規則集）
GitHub 上爆紅（74k+ 星）的純文字規則集：讓 AI Agent「像房間裡最懶的高級開發者」——先查標準庫、再查專案現有程式碼、再查依賴，最後才寫新程式；實測平均可減少約 54% 的程式碼。作者 repo 已在你的 Process 資料夾內（ponytail-main）。[GitHub](https://github.com/DietrichGebert/ponytail) ｜ [基準修正報導（InfoQ）](https://www.infoq.com/news/2026/08/ponytail-agent-skill-benchmark/)

### 6.2 模型層
- **DeepSeek V4 Pro**（8/13）：增強 Agent 能力；V4 全系列正式版上線。
- **GPT-5.4 / Claude Sonnet 4.6 / Gemini 3.1 Pro**：3 月後的模型大戰持續，模型「月月出新」成常態，選模型比選工具更關鍵。

### 6.3 評測與指數
- **AI Programmer Index（7 月首發）**：Cursor + Opus 4.7 險勝 Codex；評測標準化（如 AI Programmer Index、SWE-bench）越來越受企業重視。
- **AI Programmer Index 報導**：[The BlockBeats](https://en.theblockbeats.news/flash/345472)

---

## 七、對「個人開發者」與「團隊各角色」的啟示

| 角色 | 2026-08 的關鍵啟示 |
|---|---|
| **個人開發者** | ① 可用 `npx @deepseek-ai/dsh web` 體驗 DSH「一切皆插件」；② 用 Ponytail 規則集對抗「AI 愛寫大量程式」；③ Agent Plugins 規範出來後，寫插件可以「打一份包、到處用」 |
| **Tech Lead / 架構師** | Agent 外殼成為新技術棧取捨：選 Claude Code（體驗成熟、閉環）還是 DSH（開源、可拆、生態自由）？建議先跑原型再定案 |
| **Dev / 工程師** | 開始學習「Harness 思維」：模型只是插件之一；Context 管理、工具鏈、可追溯（Trajectory）才是 Agent 工程的核心 |
| **DevOps / SRE** | ① Agent 的沙箱、權限、攻擊面（DSH 的插件能碰 Shell）是新的安全焦點；② MCP 無狀態化讓企業部署更可行 |
| **團隊 / 企業決策** | ① 插件標準化（Agent Plugins / MCP）降低多工具並存成本；② WorkBuddy 代表「辦公入口」之爭，企業可評估其本土化 Skill 與企業端方案；③ 別綁死單一外殼——DeepSeek 正在把 Harness 拉平為公共品 |

---

## 八、關鍵詞與延伸閱讀

**關鍵詞**：Harness、DSH、一切皆插件、Cordis、Trajectory、Agent Plugins、MCP 無狀態化、Vibe Working、AI Programmer Index、Ponytail、WorkBuddy、百蝦大戰

**核心來源**
- [36氪 矽星人：玩了一夜 DeepSeek Harness](https://www.36kr.com/p/3938774780263814)
- [澎湃新聞：DeepSeek 智能體框架開放測試](https://m.thepaper.cn/detail/33783308)
- [新浪財經/界面：騰訊大力把 WorkBuddy 送上牌桌](https://finance.sina.com.cn/roll/2026-08-08/doc-inimqtzr4218954.shtml)
- [智源/新智元：六巨頭定 AI 插件新標準](https://hub.baai.ac.cn/view/56983)
- [Agent Plugins 官方規範](https://agent-plugins.org/)
- [Ars Technica：MCP 無狀態化](https://arstechnica.com/ai/2026/07/with-a-stateless-makeover-new-mcp-spec-targets-enterprise-scale/)
- [Ponytail（GitHub）](https://github.com/DietrichGebert/ponytail)
- [AI Programmer Index（The BlockBeats）](https://en.theblockbeats.news/flash/345472)

**承接原筆記**：本文為〈ArtificialIntelligenceJottings.md〉的 2026-08 補充篇；原筆記兩份時間軸的編號（程式編輯 1–36、全領域 1–75）不變，本文新增名詞自 76 起延續。


















---------------------------------------------------------------------

* 如果不知道如何調用 skill 可以透過 agent teams 調用嗎，

設一個「技能管理員 Agent」, 根據你的問題自動去 skills/ 資料夾找最適合的，然後叫其他 Agent 執行,

=> 多層 Agent 分工架構, 總管 => 研究, 文書, 分析, 審查

* 如果沒有適合的 skill 可以透過自己整合出自己的嗎 ?

把幾個現有 skill 串起來變成你專屬的 SOP

我的專屬工作流：
Step 1 → 先用 problem-framing-canvas 定義問題
Step 2 → 再用 stakeholder-identification 盤利害關係人
Step 3 → 接著用 opportunity-solution-tree 發想方案
Step 4 → 最後用 prd-development 寫成正式 PRD

以後我說「跑新功能流程」就照這個順序執行，
每一步做完先給我看，我確認了再往下走。
主題：【你的功能名稱】

---

### 時間軸濃縮版
> 1956 AI → 1959 ML → 2012 深度學習 → 2017 Transformer → 2018 BERT/GPT-1/預訓練 → 2020 GPT-3/RAG → 2022 ChatGPT/CoT/RLHF → 2023 GPT-4/Claude/多模態/Agent → 2024 MCP/多智能體/DPO → 2025～2026 推理模型 + Agent 原生時代

### 時間軸濃縮版
> 傳統補全 → 2019 Tabnine（第一個 AI 補全）→ 2021 Codex / Copilot（大眾化）→ 2023 Cursor / GPT-4（聊天+多檔案）→ 2024 Devin / Windsurf（Agent 興起）→ 2025 Claude Code / Vibe Coding（感覺編程）→ 2026 全自動 Agent 編程時代

---

# AI 程式編輯相關專有名詞時間軸（VSCode / Codex 體系）
從傳統補全到 2026 年 Agent 編程，按出現時間排序，一句話白話解釋

---

## 一、傳統補全時代（2000～2018）

1. **IntelliSense（智慧感知）— 1990 年代就有，VS 時代普及**
   微軟經典的程式自動補全功能，根據變數型態、函式定義跳出提示，是純靜態比對不是 AI。

2. **Tab Completion（Tab 補全）— 編輯器基礎功能**
   打到一半按 Tab 鍵自動補完剩餘的字，所有程式編輯器都有的基本功能。

3. **Snippet（程式碼片段）— 長期存在**
   預存好的常用程式碼模板，打關鍵字就跳出整段，純手動預設跟 AI 無關。

---

## 二、AI 補全萌芽（2019～2021）

4. **Tabnine — 2019 年推出，最早的 AI 補全工具**
   第一個用深度學習做程式碼補全的插件，支援本地離線執行，主打隱私安全。

5. **Codex — 2021 年 8 月 OpenAI 發布**
   GPT-3 專門拿 GitHub 程式碼再訓練的版本，只會寫程式不會聊天，GitHub Copilot 的底層心臟。

6. **GitHub Copilot — 2021 年 6 月技術預覽，2022 年 6 月正式版**
   微軟跟 OpenAI 聯手做的 VSCode 外掛，寫註解自動生出程式碼，第一個大眾化 AI 程式助手。

7. **Ghost Text（幽靈文字）— Copilot 帶流行的互動模式**
   灰色半透明的預測文字出現在游標後面，按 Tab 就接受，是現在 AI 補全的標準顯示方式。

---

## 三、百家爭鳴：聊天與多檔案理解（2022～2023）

8. **Amazon CodeWhisperer — 2022 年亞馬遜推出**
   AWS 版的 Copilot，免費給個人開發者用，主打跟 AWS 服務整合比較好。

9. **Codeium — 2022 年推出，免費路線代表**
   免費版就有完整補全跟聊天功能的 AI 程式助手，後來發展出 Windsurf 編輯器。

10. **GPT-4 程式能力 — 2023 年 3 月推出**
    第一個能看懂整個檔案架構、推理複雜邏輯的通用大模型，寫程式能力跳級成長。

11. **Cursor — 2023 年推出，AI 原生編輯器**
    直接改裝 VSCode 的程式編輯器，從頭設計就是為了跟 AI 一起寫程式，不是後加外掛。

12. **Inline Chat（行內聊天）— 2023 年成為標配**
    選一段程式碼直接旁邊叫 AI 改，不用切視窗對話，邊寫邊改的互動模式。

13. **CodeLlama — 2023 年 8 月 Meta 開源**
    Llama 家族專門寫程式的開源版本，可以自己抓下來本地跑，不用付 API 錢。

14. **Continue.dev — 2023 年推出，開源 VSCode 插件**
    開源的 AI 程式助手外掛，不綁模型，你想用 GPT、Claude 還是本地模型都能自己接。

15. **Repo-level Understanding（倉庫級理解）— 2023 下半年普及**
    AI 不再只看當前這支檔案，能掃完整個專案所有程式碼來回答跟修改，不會亂用不存在的函式。

16. **StarCoder — 2023 年 HuggingFace 開源**
    社群合作訓練的開源程式模型，主打訓練資料公開透明，商業使用也免費。

---

## 四、Agent 與自主編程崛起（2024 年）

17. **Devin — 2024 年 3 月 Cognition 推出，第一個 AI 軟體工程師**
    號稱能自己開終端、寫程式、除錯、跑測試的全自動 AI 工程師，當時造成話題。

18. **Agentic Coding（智能體編程）— 2024 年概念興起**
    不是補一行兩行，而是 AI 自己規劃步驟、自己改多個檔案、自己除錯，自主完成整個功能。

19. **Windsurf — 2024 年 11 月 Codeium 推出**
    第一個主打「Agent 原生」的 IDE，有 Cascade 功能能自動執行多步驟程式修改。

20. **Cursor Composer / Agent Mode — 2024 年底 Cursor 推出**
    Cursor 的 AI Agent 模式，下一個指令就自動改多支檔案、自己偵錯、自己跑命令。

21. **Test Generation（自動生成測試）— 2024 年普及**
    AI 看你寫的函式自動生出單元測試，包含邊界案例、異常狀況，省很多寫測試的時間。

22. **AI Refactoring（AI 重構）— 2024 年成為標配功能**
    選一大段程式碼叫 AI 重構，自動變數改名、拆分函式、優化效能、統一風格。

---

## 五、Vibe Coding 與全自動時代（2025～2026）

23. **Vibe Coding（感覺編程）— 2025 年 Karpathy 提出並爆紅**
    不用糾結語法細節，用口語描述你想要什麼感覺的功能，AI 自動幫你生出完整程式碼。

24. **Claude Code — 2025 年 5 月 Anthropic 推出**
    Claude 官方的命令列程式 Agent，直接在終端機執行，能掃整個專案、改檔案、跑指令，工程界好評爆高。

25. **Terminal Integration（終端整合）— 2025 年成為高階 AI 編輯器標配**
    AI 可以直接打開你的終端執行指令、看錯誤訊息、自己除錯重跑，不用你複製貼上。

26. **Multi-file Edit（多檔案編輯）— 2025 年標配**
    AI 一次任務自動修改好幾支相關檔案，自動管理 import、保持風格一致，不是只能改單一檔案。

27. **Trae — 2025 年字節跳動推出**
    免費的 AI 原生 IDE，中國開發者圈很紅，主打 Agent 模式跟全專案理解，免費版就很夠用。

28. **Cursor Tab + — 2025 年 Cursor 推出的新一代補全**
    補全從一行變成一整段甚至整個函式，預測更準，幾乎按到底就能寫完一個功能。

29. **ACU（Agent Compute Unit）— Devin 帶出的計量單位**
    AI Agent 完成任務消耗的計算單位，類似「人天」概念，用來算 AI 工程師的工作量跟成本。

30. **OpenHands（舊稱 OpenDevin）— 2024 開源 2025 爆紅**
    開源版的 Devin，大家可以自己架設的 AI 軟體工程師 Agent，社群生態很活躍。

---

## 六、常見概念與模式（不分先後）

31. **Pair Programming AI（AI 結對編程）**
    你跟 AI 一起寫程式，你想架構它幫忙寫細節，像旁邊坐了一個隨問隨答的學長。

32. **Prompt-to-Code（提示詞生成程式碼）**
    用自然語言講你要什麼功能，AI 直接生出完整程式碼，是最基礎的 AI 編程型態。

33. **Code Review AI（AI 程式碼審查）**
    AI 幫你看 Pull Request，抓潛在 bug、效能問題、資安漏洞、風格不統一的地方。

34. **Local LLM Coding（本地大模型編程）**
    模型跑在自己電腦上，不用把程式碼傳到雲端，公司機密專案會要求這種。

35. **Ollama + VSCode — 本地模型常見組合**
    Ollama 負責在後台跑本地模型，VSCode 裝 Continue 之類的插件連上去，完全離線可用。

36. **Copilot Chat vs Inline Completion — 兩大互動模式**
    聊天模式是問問題叫 AI 寫；行內補全是你打一半 AI 自動幫你接下去，兩種是不同使用場景。

---

# AI 全領域專有名詞時間軸（一句話白話版）
從 1950 年到 2026 年 7 月，按出現時間排序
---

## 一、基礎概念時代（1950～2016）

1. **AI（Artificial Intelligence / 人工智慧）— 1956 年正式命名**
   就是讓電腦模仿人類會思考、會判斷的技術，所有下面東西的總稱。

2. **圖靈測試（Turing Test）— 1950 年提出**
   判斷電腦算不算有智慧的考試：人跟機器聊天，如果分不出對方是人還是電腦，就算通過。

3. **ML（Machine Learning / 機器學習）— 1959 年提出**
   不寫死規則，丟一大堆資料給電腦自己學規律，學會後就能預測新東西。

4. **Neural Network（神經網路）— 1980 年代概念成型**
   模擬人腦神經元連接方式的數學模型，一層一層傳遞資訊來學習。

5. **Deep Learning（深度學習）— 2012 年 AlexNet 帶動爆紅**
   很多層神經網路疊起來的技術，資料越多越聰明，現在所有 AI 的底層核心。

6. **Attention Mechanism（注意力機制）— 2014 年提出**
   讓 AI 看文章時自動聚焦重點字詞，像人讀書會畫重點一樣，不用每個字平均用力。

---

## 二、核心架構與基礎組件（2017～2018）

7. **Transformer — 2017 年 Google 論文提出**
   專門處理文字的 AI 骨架，靠注意力機制看懂前後文，現在所有大語言模型的基礎。

8. **Token（令牌）— 伴隨 Transformer 普及**
   AI 眼中的最小文字碎片，不是一個字也不是一個詞，是它認識的積木塊，中文 1 字約 1～2 個 Token。

9. **Tokenizer（分詞器）— 伴隨 Transformer 普及**
   把人類的文字拆成 AI 認識的 Token 碎片的工具，也能把碎片拼回文字。

10. **Embedding（嵌入向量）— 2018 年後普及**
    把文字變成一串數字座標，意思相近的詞數字也靠得近，讓 AI 能數學運算語義。

11. **Context Window（上下文窗口）— 伴隨 LLM 普及**
    AI 一次能看進去的最大 Token 總量，就是它的短期記憶容量，超過就會忘記前面的內容。

12. **BERT — 2018 年 Google 發布**
    雙向理解的預訓練模型，擅長讀懂文章意思，早期用於搜尋、分類、情感分析。

13. **GPT-1 — 2018 年 OpenAI 發布**
    第一個 GPT 系列模型，確立了「預訓練 + 生成式」的路線，只有 1.17 億參數。

14. **Pre-training（預訓練）— 2018 年成為標準範式**
    把網路上幾乎所有文字丟進去瘋狂訓練幾個月，煉出一個什麼都懂一點的通用毛坯模型。

15. **Foundation Model（基礎模型）— 2018 年後概念形成**
    預訓練完成的通用大模型毛坯，後續可以微調成各種專用模型，像一塊可以雕刻的原木。

---

## 三、LLM 萌芽與微調技術（2019～2020）

16. **LLM（Large Language Model / 大型語言模型）— 2019 年開始流行**
    參數量超級大的預訓練語言模型，能聊天、寫東西、推理，本質就是猜下一個字是什麼。

17. **GPT-2 — 2019 年 OpenAI 發布**
    GPT 第二代，15 億參數，當年因為「太危險不敢完整開源」造成話題，開始有湧現能力。

18. **Fine-tuning（微調）— 2019 年成為標準流程**
    拿特定領域的資料再訓練一次已經預訓練好的基礎模型，讓通才變成專才。

19. **SFT（Supervised Fine-Tuning / 監督微調）— 2019 年後普及**
    拿人寫好的標準問答範本教模型，讓它回答的格式和風格符合人類預期。

20. **GPT-3 — 2020 年 5 月 OpenAI 發布**
    1750 億參數，大模型時代真正開啟，首次展現少樣本學習能力，開啟 API 商用模式。

21. **Few-shot Learning（少樣本學習）— 2020 年 GPT-3 帶動**
    不用重新訓練，只要在 Prompt 裡給幾個範例，AI 當場就學會新任務怎麼做。

22. **Zero-shot Learning（零樣本學習）— 2020 年普及**
    連範例都不用給，直接下指令 AI 就能做，靠預訓練時累積的通用知識硬做。

23. **RAG（Retrieval-Augmented Generation / 檢索增強生成）— 2020 年 Meta 提出**
    AI 回答前先去你的資料庫撈真實資料再回答，避免它憑空亂編假內容。

24. **Codex — 2021 年 8 月 OpenAI 發布**
    GPT 家族專門寫程式的版本，用 GitHub 程式碼訓練，GitHub Copilot 的底層就是它。

25. **LoRA（Low-Rank Adaptation）— 2021 年提出**
    省錢版微調技術，不用改動整個大模型，只加一小層參數就能訓練，效果接近全量微調。

---

## 四、Prompt 工程與對齊技術（2022 年）

26. **Prompt（提示詞）— 2022 年 ChatGPT 帶動大眾化**
    你輸入給 AI 的指令、問題、背景資料總稱，講得越清楚 AI 答得越準。

27. **Prompt Engineering（提示詞工程）— 2022 年成為專業技能**
    研究怎麼講話才能讓 AI 聽懂並給出好答案的學問，算是跟 AI 溝通的說話藝術。

28. **System Prompt（系統提示詞）— 2022 年普及**
    偷偷塞給 AI 的人設說明書，規定它的身份、規則、輸出格式，使用者通常看不到。

29. **CoT（Chain of Thought / 思維鏈）— 2022 年 Google 提出**
    叫 AI 一步一步慢慢想、把推理過程講起來，不要直接跳答案，答題準確度會大幅提升。

30. **RLHF（Reinforcement Learning from Human Feedback）— 2022 年 ChatGPT 帶動**
    讓人類幫 AI 的回答打分好壞，再用強化學習訓練它趨向人類喜歡的回答方式。

31. **Reward Model（獎勵模型）— RLHF 的配套產物**
    專門用來評分 AI 回答好壞的模型，代替人類做大量評分，是 RLHF 三階段的中間產物。

32. **PPO（Proximal Policy Optimization）— RLHF 常用算法**
    強化學習的一種訓練算法，用來讓模型慢慢往獎勵高的方向調整，不會改太猛走偏。

33. **Hallucination（幻覺）— 2022 年成為大眾熱詞**
    AI 講得頭頭是道但內容全是編出來的假貨，講得越自信越容易騙到人。

34. **ChatGPT — 2022 年 11 月 OpenAI 發布**
    基於 GPT-3.5 的對話機器人，兩個月破億用戶，正式把 AI 帶進大眾視野。

35. **GPT-3.5 — 2022 年 11 月隨 ChatGPT 推出**
    GPT-3 的升級版，加入 RLHF 對齊，對話體驗大幅提升，是第一個平民化大模型。

36. **Emergent Abilities（湧現能力）— 2022 年研究熱點**
    模型大到一定程度突然解鎖的新能力，像臨界點一樣，小模型做不到的事大模型突然就會了。

---

## 五、百花齊放：模型、多模態、Agent 爆發（2023 年）

37. **GPT-4 — 2023 年 3 月 OpenAI 發布**
    當時最強的閉源大模型，推理能力跳躍式進步，支援多模態看圖，參數量傳聞萬億級。

38. **Claude — 2023 年 3 月 Anthropic 推出**
    Anthropic 公司的對手模型，主打安全、長上下文、程式能力強，是 GPT 最大競爭對手。

39. **LLaMA / Llama — 2023 年 2 月 Meta 開源**
    Meta 開源的大模型系列，證明小模型也能很強，直接引爆開源大模型生態。

40. **Multimodal（多模態）— 2023 年 GPT-4V 後大眾化**
    同一個 AI 同時能讀文字、看圖片、聽聲音、看影片，不像舊版只能吃文字。

41. **DPO（Direct Preference Optimization / 直接偏好優化）— 2023 年提出**
    RLHF 的簡化版，不用訓練獎勵模型也不用強化學習，直接用人類偏好數據微調，省很多事。

42. **LLM Agent（語言模型智慧體）— 2023 年 AutoGPT 帶火**
    把 LLM 當腦袋，讓它自己規劃步驟、自己查資料、自己動手做完一整件事，不用你一步一步下指令。

43. **AutoGPT — 2023 年 3 月爆紅**
    第一個爆紅的 Agent 專案，讓 GPT 自己分解任務、自己執行，當時震驚整個業界。

44. **Workflow（工作流）— 2023 年隨 Agent 興起**
    把 AI 要做的事拆成固定步驟按順序跑，像流水線一樣確保每次都照流程執行。

45. **Tool Use / Function Calling（工具呼叫）— 2023 年成為標配**
    AI 不只用嘴巴回答，還能主動呼叫外部工具，比如計算機、搜尋、讀資料庫、發訊息。

46. **Vector Database（向量資料庫）— 2023 年 RAG 爆紅帶動**
    專門存 Embedding 數字向量的資料庫，能快速搜出語義最接近的內容，是 RAG 的必備零件。

47. **Grounding（接地 / 事實根基）— 2023 年 RAG 相關熱詞**
    讓 AI 的回答有真實資料來源支撐，不是空中閣樓亂編，減少幻覺的核心概念。

---

## 六、多智能體與生態標準（2024 年）

48. **MAS（Multi-Agent System / 多智能體系統）— 2024 年成為研究熱點**
    好幾個 AI Agent 組成團隊，分工合作一起做複雜任務，像公司不同部門各司其職。

49. **Agent Team（智能體團隊）— 2024 年框架普及**
    有固定角色分工的多智能體組合，比如一個負責寫、一個負責審、一個負責查資料。

50. **Subagent（子智能體）— 2024 年架構成熟**
    大 Agent 手下的小幫手，老大把麻煩任務拆開派給不同子 Agent 並行處理，做完再彙整。

51. **Orchestrator（編排者 / 主控 Agent）— 多智能體架構核心**
    多 Agent 系統裡的總指揮，負責分配任務、接收結果、協調其他子 Agent 工作。

52. **MoA（Mixture-of-Agents / 混合智能體）— 2024 年 Berkeley 提出**
    同一個問題丟給好幾個 Agent 同時回答，再由一個總管把所有答案融合成最終版本。

53. **Skills（技能模組）— 2023～2024 年 Agent 框架普及**
    預先寫好的專用工具包，AI 需要時直接呼叫就能用（例如算數學、查天氣、讀檔案），不用每次重新學。

54. **MCP（Model Context Protocol / 模型上下文協定）— 2024 年 11 月 Anthropic 發布**
    Anthropic 做的一套通用接頭標準，讓任何 AI 都能輕鬆安全地連上各種軟體、資料庫和工具。

55. **GRPO（Group Relative Policy Optimization）— 2024 年普及**
    新一代強化學習算法，不用獎勵模型，一群答案互相比較好壞來訓練，省成本效果又好。

56. **QLoRA — 2023 年提出 2024 年普及**
    LoRA 的升級省錢版，把模型量化成 4bit 再微調，一般消費級顯卡也能微調大模型。

57. **Prompt Injection（提示詞注入）— 2024 年成為安全熱詞**
    一種攻擊手法，在輸入內容裡藏惡意指令，騙 AI 忘記原本規則去做壞事。

---

## 七、推理模型與 Agent 生態成熟（2025～2026 年）

58. **o1 / Reasoning Model（推理模型）— 2024 年 9 月 OpenAI 推出 o1**
    專門用來做深度思考的模型，回答前會內部推演很久，數學、編碼、複雜推理能力爆強。

59. **o3-mini — 2025 年 1 月 OpenAI 推出**
    o 系列的平價小版本，可調整推理強度，主打程式、數學、工具使用的性價比。

60. **Claude Opus / Sonnet / Haiku — Anthropic 產品線**
    Claude 的三個定位：Opus 最強最貴、Sonnet 均衡主力、Haiku 最快最便宜，三種尺寸按需選。

61. **GPT-5 系列 — 2025～2026 年陸續推出**
    OpenAI 最新一代旗艦模型，推理、多模態、Agent 工具使用能力全面升級。

62. **Claude 4 / 5 系列 — 2025～2026 年推出**
    Anthropic 最新旗艦，主打超長上下文（1M Token 以上）、超強程式能力和 Agent 原生設計。

63. **Long Context（長上下文）— 2025 年成為標配競技場**
    模型上下文窗口從幾萬擴張到幾十萬甚至一百萬 Token，能一次讀完整本書或整個專案程式碼。

64. **Agentic AI（智能體優先 AI）— 2025～2026 年主流方向**
    新一代模型從設計上就原生支援自動規劃、工具使用、多步執行，不是只會聊天的機器人。

65. **Memory（記憶機制）— 2025 年 Agent 標配功能**
    讓 AI 能記住之前對話過的內容、學過的東西，下次開啟還認得你，不用每次重新介紹自己。

66. **Planning（任務規劃）— Agent 核心能力之一**
    AI 接到複雜任務後，先自己拆解成一步步計畫，再按計畫逐步執行，不會想到哪做到哪。

67. **Reflection（自我反思）— 2025 年高階 Agent 技術**
    AI 做完一件事後自己檢查成果，發現問題就修正重來，像人寫完東西會回頭檢查一樣。

---

## 八、常見現象與通用概念（不分先後）

68. **Alignment（對齊）— 貫穿全程的核心議題**
    讓 AI 的價值觀、行為方式跟人類期望保持一致，不要講髒話、不要亂來、不要幫壞人。

69. **Scaling Law（縮放定律）— 2020 年 OpenAI 提出**
    模型參數越多、訓練資料越多、計算量越大，能力就會可預測地變強，早期大模型瘋狂堆參數的理論基礎。

70. **Parameters（參數）— 模型大小單位**
    模型腦袋裡的連接權重數量，類似神經突觸，通常越多越聰明但也越耗電越貴。

71. **Inference（推理）— 日常使用階段**
    模型訓練好之後，你輸入問題它輸出答案的這個執行過程，就是推理，比訓練便宜很多。

72. **Temperature（溫度參數）— 生成控制參數**
    調整 AI 回答隨機性的轉鈕，數值越高越有創意越亂，越低越保守越固定。

73. **Perplexity（困惑度 / PPL）— 模型好壞指標**
    衡量模型猜下一個字準不準的數值，越低代表模型對文本的預測越準、理解越好。

74. **Closed-source（閉源）vs Open-source（開源）— 模型路線之爭**
    閉源就是廠商不給模型檔案只能用 API（如 GPT、Claude），開源就是模型檔案放出來大家可以自己改自己跑（如 Llama）。

75. **AIGC（AI Generated Content / AI 生成內容）— 2023 年流行詞**
    用 AI 生出來的所有東西總稱，包含文字、圖片、影片、音樂、程式碼等。
