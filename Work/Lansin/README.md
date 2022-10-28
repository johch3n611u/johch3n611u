## 引言

本項目紀錄在職前公司時歷練的技術關鍵字，方便日後查找，

前公司為品牌代操兼物流、自研發系統，同時也有自我品牌之乙方電商公司，

在職時協同四位夥伴工程師，

共同開發與維護多個電子商務平台、品牌前台形象建立與後台 CMS 等相關功能、後台 ERP、SCM、CRM、Workflow 等相關功能，包含幾十個以上的站頁、系統資料庫，超過百支以上的 SQL Server Agent、Winform、Jenkins 排程，近二十個雲與本地 Server，

每日專案開發、維護管理、新技術重構，團隊合作使用 Scrum、OKR、KPI、TRELLO 任務管理、Git / SVN 版本控制。

# 電子商務網站工程師

### 職責劃分

> 1. **專案較複雜的頁面由設計部門完成頁面切版、 Banner 、 EDM 等 UI，** </br>
> 2. **協同數據發展部與需求單位與工程師討論 UX 並完成。** </br>
> 3. **團隊利用 Scrum、OKR、KPI、TRELLO 任務管理、Git / SVN 版本控制。** </br>
> 4. **工程師團隊工作橫以品牌切割從前端、後端至資料庫與架站、排程。** </br>
> 5. **縱為各品牌之間維護需求或功能新增相互支援。** </br>

### 工作技能

> **主 C# / VB / MPA / SSR / ASP.NET 4.7 / MVC 5 / WebForm / WebAPI / Winform ...**
>
> **次 .NET Core 2.1 / Razor / SPA / CSR / HTML5 / Vue.js ...**

**註 :** .Net Core 架構，因從 ASP.NET MVC 轉上去的，所以沒有使用到 DI Container 撰寫 Service 、 Component ，使用公司自製的 Module 類別核心，使用少量的 ADO.NET Entity Framework ， 大部分使用 Dapper ORM 套件，操作 Transact-SQL 查詢語言。

### 達成目標

> * **兩個禮拜內**
>
>   **從只會 C# 與較熟悉 .Net MVC ，到熟悉前端 EasyUI 函式庫、後端 .net Webform 框架、 VB 語言。所架構之前台後台系統。**
>
> * **第三個禮拜至離開前**
>
>   **協助 CRM 平台專案架構，前端 @Razor 、後端 .net MVC 、 MSSQL 、 IIS 等相關網管協定之架設與程式撰寫、 WebAPI 串接。**
>
> * **第二個月末至第四個月末**
>
>   **從不會 Winform 到獨立處理購物中心上架 API 約一萬行的程式，包含十三隻 API 功能，加密與後台 ERP 系統串接。**
>
> * **期間**
>
>   **持續處理公司其他單位需求單日常開發與維護如下列...**

### 工作項目

#### 專案開發

<details>
<summary>MPA SSR CRM 平台 部分專案</summary>

* 使用技術 : C# / .NET MVC 5 / SendGrid / Tableau / Power BI / Analytics.js / Embed.js / Chart.js / Google Chat API / Datatables.js / Sourcetree...
  * 需求: 根據數據分析部需求提供應用。
    * [實作包含]
    * `會員 RFM 歸戶 ( B2B 、 B2C ...) 流程優化`
    * `分類會員 EDM 寄送等相關行銷自動化`
    * `Datatables 商品 & 會員標籤系統`
    * `DB 同步、資料清洗、欄位設計、排程預存整理、資料庫備份`
    * `IIS 架設 DB 建置 Tableau Token 設定`
    * `報表圖形化與產出`
    * `串接 SendGrid 電子報系統`
    * `嵌入 Power BI、Tableau、Google Analytics API 報表顯示與應用`
    * `架設 Tableau Server`
    * `串接 Tableau API 並嵌入應用`

</details>

<details>
<summary>( 雅虎 & Momo SCM ) API 串接 MPA SSR 後台 SCM 管理系統 專案</summary>

* 使用技術 : .NET / Restful API / EasyUi.js / MSSQL / 加密解密編碼 / Winform ...
  * 需求: 在技術文件不齊全的狀況下，與雅虎業務溝通，加密編碼解密串 API ，將電商商品結構資料抓取，比對公司本身商品結構資料，上架精技商品資料，約 13 支 API 與後台介面 CRUD ，排程系統...
    * [實作包含]
    * `Momo & Yahoo API 文件新舊比對`
    * `與 Momo & Yahoo 業務溝通在無法解決問題情況下找其他方案`
    * `Java 元件 / 程式碼解意轉為 .NET`
    * `架設 Java 程式碼環境 Eclipse / 元件建置，擷取正確加密代碼`
    * `Header AES CBC PKCS5Padding / hmacsha512 加密`
    * `Header base64 / UTF_8 / Hex 轉碼`
    * `CookieCollection Stream Request Response 應用`
    * `取商品結構串接 API`
    * `反序列化 JsonObject 轉存 SQL ，約 41 種類 8 百多項目，結構化標籤約 15 萬筆`
    * `後台 ERP 平台，結構畫類別、屬性選單， EasyUi.js 、MSSQL 應用`
    * `Winform 、Web API 排程串接商品類別序列化提報上架 API`
    * `協同同事取 Token 將影音圖片 Stream 提報雅虎 Amazon S3 上傳檔案`

</details>

<details>
<summary>系統發展部 需求單電子化 專案</summary>

* 使用技術 : Webform 架構改良之 EasyUi + API 、 Chart.js 、 AJAX 、JQ
  * 需求: 量化質化系統發展部工作內容，合理安排工作順序、時程管控。
    * [實作包含]
    * `UML 需求單流程架構討論`
    * `MSSQL 資料庫與欄位建置`
    * `EasyUI 清單建置 CRUD`
    * `圖表 Chart.js 建置`

</details>

#### 需求單開發與維護

* MPA SSR 前台 需求單
  <details><summary>動態 Menu 首頁</summary>

   * 需求: [官網前台改版](https://twstore.msi.com/)
     * 使用技術 : MPA / SSR /jQ / AJAX / ASP.NET
       * [實作包含]
       * `TSQL 查詢`
       * `AJAX 拋資料`

  </details>
  <details><summary>商品包裝問券頁面、登入送購物金功能</summary>

   * 需求: [官網前台改版](https://www.sastty.com.tw/)
     * 使用技術 : ASP.NET Page / CSS / Javascript / JQ / TSQL
       * [實作包含]
       * `商品說明書上 QR-Code , 拍了之後連到官網問券`
       * `會員登錄`
       * `填寫問券`
       * `送出問券，送購物金`
       * `手機板頁面`
       * `version 2 。 階層權限 Css 樣式覆蓋、 RWD 手機板畫面`

  </details>
  <details><summary>春聯廣告頁</summary>

   * 需求: [根據行銷企劃部需求提供應用](https://www.aimedia.com.tw/)...
     * 使用技術 : jQ DOM / jQ library EasyUI / MPA / AJaX / ASP.NET ...
       * [實作包含]
       * `前台動態頁面`
       * `後台上稿功能`

  </details>
  <details><summary>抽獎活動登入頁面</summary>

   * 需求: 基本資料填寫，驗證後存庫。
     * 使用技術 : jQ / Js / Webforms
       * [實作包含]
       * `抽獎頁面驗證登入 Session`
       * `前端頁面套版`
       * `資料傳遞至後端處頁面`
       * `資料驗證`
       * `回傳錯誤訊息`
       * `回傳成功訊息並導入明細抽獎列表頁面`
       * `登入抽獎頁面表單`
       * `後端驗證是否重複序號`
       * `有重複錯誤提示`
       * `無重複存入資料庫並轉回抽獎列表頁面`

  </details>
  <details><summary>文章頁面</summary>

   * 需求: [新增行銷露出頁面](https://www.lab101.asia/)。
     * 使用技術 : Primary CSS / EasyUI.js / ASP.NET / Youtube API
       * [實作包含]
       * `後台 EasyUI 行銷頁面類別與文章新增`
       * `前台 rewrite 分類參數進入類別頁`
       * `後台資料前台樣式套版顯示`

  </details>
  <details><summary>產品頁頁面</summary>

   * 需求: 訊息不明顯，購物車功能常駐頁面。
     * 使用技術 : JQ / Primary CSS
       * [實作包含]
       * `後台 EasyUI 行銷頁面類別與文章新增`
       * `前台 rewrite 分類參數進入類別頁`
       * `後台資料前台樣式套版顯示`

  </details>
  <details><summary>金流購物車付款頁面</summary>

   * 需求: 選擇宅配時顯示地址表單，選擇超商付款時顯示超商 API 串接內容。並將資料傳回資料庫。
     * 使用技術 : JQ / webforms
       * [實作包含]
       * `Webforms Repeater`
       * `webforms contentPlaceHolder`
       * `並在多個頁面內容實作 jQ 、 js 互動效果`
       * `資料驗證`
       * `回傳錯誤訊息`
       * `回傳成功訊息並導入資料庫與跳轉頁面`
       * `物流 API 串接`

  </details>

* ERP CMS EasyUI.js AJAX 後台 需求單
  <details><summary>後台抽獎功能清單轉存功能</summary>

  * 需求: 後台顯示前台抽獎頁面登入之內容，並有按鈕可以將清單轉出 Excel 。
     * 使用技術 : EasyUI.js
       * [實作包含]
       * `EasyUI 階層設定`
       * `EasyUI 基本顯示`
       * `EasyUI 分頁`
       * `EasyUI 日期查詢顯示`
       * `EasyUI 匯出清單 Excel`

  </details>
  <details><summary>後台 banner 上稿系統</summary>

  * 需求: 後台顯示前台抽獎頁面登入之內容，並有按鈕可以將清單轉出 Excel 。
     * 使用技術 : MPA /jQ /AJaX / EasyUI.js / ASP.NET
       * [實作包含]
       * `EasyUI 、 Webforms API 混和架構維護`
       * `CISCO VPN 串接`
       * `TSQL 查詢`
       * `AJAX 拋資料`

  </details>
  <details><summary>後台會員權限、預存排程管理、報表統計匯出功能、業務綁定報表功</summary>

  * 需求: 購物車未結帳資料建置，用以數據發展部測試弱 AI 廣告投放演算與行銷部 KPI 報表統計。
     * 使用技術 : TSQL / C# / .NET / EasyUI.js
       * [實作包含]
       * `EasyUI 、 Webforms API 混和架構維護`
       * `KPI 指標研擬`
       * `TSQL 操作`
       * `預存與 Jenkins 管理`
       * `轉 Excel 報表`

  </details>

* Winform 需求單
  <details><summary>FTP API 串接 排程載入資料庫</summary>

  * 需求: 串接 API 載入 FTP 檔案，轉存資料庫，並設定自動排程。
     * 使用技術 : Winform
       * [實作包含]
       * `Windowsform 簡易排版`
       * `Windowsform 後台程式撰寫`
       * `串接 API 資轉存資料庫並顯示`
       * `時間處理函式導入排程`
       * `ShowDoc php Markdown 文件歸檔`

  </details>



