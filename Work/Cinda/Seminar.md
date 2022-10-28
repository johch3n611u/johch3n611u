# 研討會 & 日常

## 目錄

* [Topshelf & Quartz.Net](#1)
* [Dapper ORM](#2)
* [CAPTCHA 圖靈測試](#3)
* [SASS / LESS](#4)
* [CSharp 非同步程式](#5)
* [Angular pipe regx](#6)
* [SSRS SQL Server Reporting Services](#7)
* [Navicat Premium](#8)
* [SQL 效能調校](#9)
* [正規表示式 Regx](#10)
* [.Net Core ViewComponent](#11)
* [Flurl C# Library](#12)
* [Robots.txt 封鎖 / SEO 優化搜尋](#13)
* [Elasticsearch 搜尋引擎](#14)
* [XXXXXXXXXXXXXX](#15)
* [XXXXXXXXXXXXXX](#16)
* [XXXXXXXXXXXXXX](#17)
* [XXXXXXXXXXXXXX](#18)
* [XXXXXXXXXXXXXX](#19)
* [XXXXXXXXXXXXXX](#20)
* [XXXXXXXXXXXXXX](#21)
* [XXXXXXXXXXXXXX](#22)
* [XXXXXXXXXXXXXX](#23)
* [XXXXXXXXXXXXXX](#24)
* [XXXXXXXXXXXXXX](#25)
* [XXXXXXXXXXXXXX](#26)
* [XXXXXXXXXXXXXX](#27)
* [XXXXXXXXXXXXXX](#28)
* [XXXXXXXXXXXXXX](#29)
* [XXXXXXXXXXXXXX](#30)
* [XXXXXXXXXXXXXX](#31)
* [XXXXXXXXXXXXXX](#32)
* [XXXXXXXXXXXXXX](#33)
* [XXXXXXXXXXXXXX](#34)
* [XXXXXXXXXXXXXX](#35)
* [XXXXXXXXXXXXXX](#36)
* [XXXXXXXXXXXXXX](#37)
* [XXXXXXXXXXXXXX](#38)
* [XXXXXXXXXXXXXX](#39)
* [XXXXXXXXXXXXXX](#40)
* [XXXXXXXXXXXXXX](#41)
* [XXXXXXXXXXXXXX](#42)
* [XXXXXXXXXXXXXX](#43)
* [XXXXXXXXXXXXXX](#44)
* [XXXXXXXXXXXXXX](#45)
* [XXXXXXXXXXXXXX](#46)
* [XXXXXXXXXXXXXX](#47)
* [XXXXXXXXXXXXXX](#48)
* [XXXXXXXXXXXXXX](#49)
* [XXXXXXXXXXXXXX](#50)
* [XXXXXXXXXXXXXX](#51)
* [XXXXXXXXXXXXXX](#52)
* [XXXXXXXXXXXXXX](#53)
* [XXXXXXXXXXXXXX](#54)
* [XXXXXXXXXXXXXX](#55)
* [XXXXXXXXXXXXXX](#56)
* [XXXXXXXXXXXXXX](#57)
* [XXXXXXXXXXXXXX](#58)
* [XXXXXXXXXXXXXX](#59)
* [XXXXXXXXXXXXXX](#60)
* [XXXXXXXXXXXXXX](#61)
* [XXXXXXXXXXXXXX](#62)
* [XXXXXXXXXXXXXX](#63)
* [XXXXXXXXXXXXXX](#64)
* [XXXXXXXXXXXXXX](#65)
* [XXXXXXXXXXXXXX](#66)
* [XXXXXXXXXXXXXX](#67)
* [XXXXXXXXXXXXXX](#68)
* [XXXXXXXXXXXXXX](#69)
* [XXXXXXXXXXXXXX](#70)
* [XXXXXXXXXXXXXX](#71)

## Topshelf & Quartz.Net <a id="1"></a>

<https://dotblogs.com.tw/wasichris/2017/01/14/165637>

利用服務跑排程，一個排程一個服務(Service)，要利用容器才能啟用多個服務 ? (名稱不能一樣)

job / trigger / schedule (取代 Timer) - 有支援 .new core

補充 : 直接手動執行而不是透過服務執行排程

## Dapper ORM <a id="2"></a>

| 比較       | EF x Dapper 混用 | EF      | Dapper  |
| ---------- | ---------------- | ------- | ------- |
| 操作性     | :cupid:          | :cupid: | -       |
| 效能       | :cupid:          | -       | :cupid: |
| 查詢精準度 | :cupid:          | -       | :cupid: |
| 可讀性     | :cupid:          | :cupid: | -       |

> Dapper 用於查詢

搭配 AutoMapping 等 map 才會好用， Dapper 的罩門是關聯

<https://github.com/shps951023/Trace-Dapper.NET-Source-Code>

---

### 20201028 補充

主要用於查詢

跟一般使用一樣需要連線

指定型別或動態接值

跟 EF 搭配可以直接用實體接值

支援參數化 / 自動對應

Muti data read

沒有延遲查詢功能

slapper automapper 套件可以解決聯集表 join 問題

## CAPTCHA 圖靈測試 <a id="3"></a>

Google reCAPTCHA v2 , v3 費用 0

## SASS / LESS <a id="4"></a>

LESS 可由 js 轉為 css ， SASS 是由 Ruby 編寫需要編譯為 css ，但具有較多程式特性 e.g. 繼承...

<http://programmermagazine.github.io/201409/htm/focus2.html>

## CSharp 非同步程式 <a id="5"></a>

i/o、調用API、存取資料表 有效 (產能增加)

CPU (看硬體效能)無效 (效能不變)

Task Like void

await async 等待器消耗 100mb

非必要時少掛 async 關鍵字

> 調用時要排序不然跟同步沒啥兩樣

Task 很多寫法

危險性 Dead lock / 資料亂跳異常

Current 不為 null / 會卡執行序

> nodejs 非同步庫比 csharp 好懂 ?

不會起新執行序 ( 不是真正意義上的多工 )

configre await 死鎖原因及解法

asp.core 沒這問題

## Angular pipe regx <a id="6"></a>

ng 寶哥 套件 又見 可以快速建立裝飾器

## SSRS SQL Server Reporting Services <a id="7"></a>

伺服器端的報表產生系統 Gui 給工程師下 sql 做報表

似乎要錢

<http://www.blueshop.com.tw/board/FUM20041006161839LRJ/BRD20050809154645F57.html>

<https://ithelp.ithome.com.tw/articles/10181489>

資料集

還是需要工程師，會有一點學習成本，但是上手後產報表速度似乎比寫得快 ? ( 待參考 )

IIF nothing ( VB use )

Blob 二進位大型物件

XML

---

權限設定是個問題不知道有沒有，會造成任何人從 url 都可進入報表畫面。

可以克制 css 但是比較硬難改，直接編輯 xml，效能問題，但報表轉格式 download 方便，維護方便。report 方便

## Navicat Premium <a id="8"></a>

以單一的 GUI 資料庫管理套件，同時連線到不同類型的資料庫

## SQL 效能調校 <a id="9"></a>

* 查詢成本 執行計畫
* IN < EXISTS
* INSERT INTO (Table存在) < SELECT INTO (Table不存在) (通常直接備份時使用)
* Order by (無 index 時慢)，但叢集索引太多時 inster 會變慢，只是查詢變快 ( key 只是讓值不重複 )
* Count(*) > Count(col)
* < and >= 優於 between
* C# SQLBalkCopy 吃系統轉檔不做過多邏輯判斷，進什麼吃什麼

需補使用情境

## 正規表示式 Regx <a id="10"></a>

Regx DTO 標籤 應用

正向反向

Ctrl + F 也可吃 / 取代也可吃

檢查條件 / 限制輸入條件 JS C# 都有 Object Method

需補正則背後原理 & 效能關係

## .Net Core ViewComponent <a id="11"></a>

類似現代前端框架的 Component 概念，但後端渲染 SSR

可以 JS 呼 Controller 再回傳 VC

其實就是 .Net Core 因應沒有 MVC 的 Partial View 所做的升級版

主要應用可以資料來源同一個 Model 但不同套版

## Flurl C# Library <a id="12"></a>

nuget 安裝

應用 URL組裝 參數化系統設計

Get Post Async Task Await

Exception 應用

Header Download

Oauth

Get String Byte Stream ...

## Robots.txt 封鎖 / SEO 優化搜尋 <a id="13"></a>

#### 案例

高鐵還沒釋出被爬蟲爬到，因為測試站沒有 robots.txt

福華旅行社開發完後，SEO 無起色，使用 sitemap.xml 嘗試

---

#### 1 .robots.txt

檔名須小寫 root 目錄要讀的到 e.g. Angular 要放在靜態目錄之下

不允許...，啥檔案 like gitnone (內容寫法)

Google search console / robots tool

#### 2 .http 回應標頭 meta ( IIS 內設定 )

與 robots.txt 相比權重較大

如何看有無成功 ? 瀏覽器 dev tool Network X-Robot-tag

#### 3 .網址 Google Search Console 移除六個月

半天成效

#### 4 sitemap.xml 讓瀏覽器搜入提高 SEO 算分

sitemap 產生器，丟 root 目錄

GSC 丟 siemap

連線偵測，要求 google 搜入

#### 預防與總結

robot.txt 容易改動，多人協作或客戶有機會異動檔案時不建議

header meta 相對較保險，只有 MIS 有機會去異動

或者靠較嚴謹的流程管控，如申請測試站一律掛 header meta

- 補充 h1 container 容易被 bot 搜到 (HTML結構優化)

## Elasticsearch 搜尋引擎 <a id="14"></a>

分散式搜尋系統 / Java / 容易擴充 / 反向搜尋(分池計分)

etg 正向搜尋：計算每個分池 / 精確大小寫

專為英文做的 JAVAHOME ?

內建訪問服務

中（每字）英（空白間隔）

分析器　輔助避免中文過多分池　ＩＫＡ　同義池

自定義分析　／　ＭＡＰＰＩＮＧ　查詢使用

資料類型．．．　超過則跳過ＥＳ直接搜尋

#### 重點

1. 搜尋規則 (類似正則／全文檢索／模糊／精確)
2. 分池（分析套件）大小寫／繁簡體
3. 資料源（查詢結果或其他／備份ｓｎａｐｓｈｏｔ快照　？
4. 結果（分頁／Ｇｒｏｕｐｂｙ）

#### 總結

中鼎（斷池）醫學專有名詞　／　廢話就分池　／　簡單就利用　Ｇｏｏｇｌｅ搜尋ＡＰＩ

ｐａｒｓａｌｅ　主打公司加值應用／ＳＱＬ　Ｌｉｋｅ　人才庫

目前ｂｕｇ　中英代碼搜尋不到　ＴＣ１２３４５

### 20201014 補充 elasticsearch 進階應用 (感覺還是要實際遇到問題解決才懂) java 開源搜尋引擎

聚合 / 分池 / 關聯 / reindex / 分群搜尋 / kinana 管理頁面套件 要錢 可以驗證登入 / 全文檢索 / 雙引號會爆

## Visual Studio Spell Checker (拼字檢查套件) <a id="15"></a>

* [Visual Studio Vession](https://marketplace.visualstudio.com/items?itemName=EWoodruff.VisualStudioSpellCheckerVS2017andLater)

* [Visual Studio Code Vession](https://marketplace.visualstudio.com/items?itemName=streetsidesoftware.code-spell-checker)

工具 => Spell Checker => Edit Global Configuration => add => [\u4e00-\u9fa5] => 重開即可排除中文

<https://blog.poychang.net/visual-studio-spell-checker/>

## C# RestSharp (Http的组件) <a id="16"></a>

一般後端調用 API 都是使用 System.net.http => HttpClient

<https://dotblogs.com.tw/justforgood/2018/08/24/100629>

但 C# RestSharp 函式庫簡化了使用方式。

RestSharp 是一个轻量的，不依赖任何第三方的组件或者类库的Http的组件。

<https://zhuanlan.zhihu.com/p/29338111>

<https://github.com/restsharp/RestSharp>

<https://www.nuget.org/packages/RestClient/>

## Nuget (開源軟體包管理系統) <a id="17"></a>

NuGet是一個自由開源軟體包管理系統。用於Microsoft開發平台。

藉由類似 cdnjs 的方式，載入線上 packge 所以要確認網址一定要能解析正確，nuget 管理介面不能放錯誤網址否則會影響其他套件載入。

<https://stackoverflow.com/questions/10240029/how-do-i-install-a-nuget-package-nupkg-file-locally>

<https://dotblogs.com.tw/OldNick/2017/03/24/162927>

## NLOG (Log工具) <a id="18"></a>

C#中有許多好用的Log工具(如NLog、Log4net、Serilog)，

<https://ithelp.ithome.com.tw/articles/10207693>

<https://dotblogs.com.tw/stanley14/2017/02/15/nlog>

## 網站規劃 & UIUX & 設計規範 <<a id="19"></a>>

<http://homepage.ntu.edu.tw/~huangsl/dw8/dw8-Pre-procedure.pdf>

<https://ctmaxs.com/%E7%B6%B2%E9%A0%81%E8%A8%AD%E8%A8%88/%E7%B6%B2%E7%AB%99%E8%A8%AD%E8%A8%88%E8%88%87%E9%96%8B%E7%99%BC%E6%B5%81%E7%A8%8B/#i>

<https://www.ctkpro.com/blog/web-design-progress-and-schedule/>

<http://www.tbsa.tw/ezfiles/tbsa/download/attach/5/%BD%B2%A9%FA%AD%F5-%BA%F4%AF%B8%A5%F8%B9%BA%BBP%A4%B6%AD%B1%B3%5D%ADp.pdf>

<https://otoitsuki.info/%E7%B6%B2%E7%AB%99%E4%BC%81%E5%8A%83%E6%B5%81%E7%A8%8B-8e631cce494d>

<https://www.google.com/search?rlz=1C1CHBF_zh-TWTW905TW905&ei=RfhRX9KcKcKTr7wPt9eW-Aw&q=%E9%96%8B%E7%99%BC%E9%9A%8E%E6%AE%B5%E7%B6%B2%E7%AB%99%E4%BC%81%E5%8A%83%E6%B5%81%E7%A8%8B&oq=%E9%96%8B%E7%99%BC%E9%9A%8E%E6%AE%B5%E7%B6%B2%E7%AB%99%E4%BC%81%E5%8A%83%E6%B5%81%E7%A8%8B&gs_lcp=CgZwc3ktYWIQAzoECAAQQzoGCAAQBRAeUOkLWOkLYNsVaABwAHgAgAHXAYgBoQOSAQMyLTKYAQCgAQKgAQGqAQdnd3Mtd2l6wAEB&sclient=psy-ab&ved=0ahUKEwiS09SUiM_rAhXCyYsBHberBc8Q4dUDCA0&uact=5>

<https://ithelp.ithome.com.tw/users/20121038/ironman/3574>

<https://ithelp.ithome.com.tw/articles/10233696>

## JS 事件循環 <a id="20"></a>

<https://www.google.com/search?q=Event+Queue&source=lmns&bih=915&biw=1920&rlz=1C1CHBF_zh-TWTW905TW905&hl=zh-TW&sa=X&ved=2ahUKEwi_-KSB0YDsAhVWAqYKHbhYBZQQ_AUoAHoECAEQAA>

## IIS & SQL資料庫 部屬 <a id="21"></a>

<https://dotblogs.com.tw/mis2000lab/2017/07/12/IIS_WindowsAuth_ASPnet_SQL>

<https://blog.xuite.net/tolarku/blog/536261609-%5BIIS%5D+VS.NET+%E5%B0%88%E6%A1%88%E7%99%BC%E8%A1%8C%E8%B3%87%E6%96%99%E5%BA%AB%E9%8C%AF%E8%AA%A4+-+IIS+APPPOOL%5C.NET+v4.5+%E7%9A%84%E7%99%BB%E5%85%A5%E5%A4%B1%E6%95%97>

<https://dotblogs.com.tw/aken1215/2016/08/24/155102>

<http://lhzyaminabe.blogspot.com/2017/08/mvciis75mvc.html>

<https://dotblogs.com.tw/mis2000lab/2017/07/12/IIS_WindowsAuth_ASPnet_SQL>

## Nuget Case Converter 快速轉換命名方式 <a id="22"></a>

<https://marketplace.visualstudio.com/items?itemName=munyabe.CaseConverter>

## Visual Studio 視窗配置 <a id="23"></a>

<https://www.google.com/search?rlz=1C1CHBF_zh-TWTW905TW905&ei=TxVDX6GmEMuImAWNt72oCw&q=visual+studio+%E8%A6%96%E7%AA%97%E9%85%8D%E7%BD%AE+%E8%B7%91%E6%8E%89+&oq=visual+studio+%E8%A6%96%E7%AA%97%E9%85%8D%E7%BD%AE+%E8%B7%91%E6%8E%89+&gs_lcp=CgZwc3ktYWIQAzIFCAAQzQIyBQgAEM0COggIABCwAxDNAjoCCAA6BAgAEB5QhSpYjTdgtzhoAXAAeACAAUKIAa4BkgEBM5gBAKABAaABAqoBB2d3cy13aXrAAQE&sclient=psy-ab&ved=0ahUKEwjhiaPf1bLrAhVLBKYKHY1bD7UQ4dUDCAw&uact=5>

<https://kevintsengtw.blogspot.com/2015/02/visual-studio-part1.html>

## EF Codefirst <a id="24"></a>

<https://dotblogs.com.tw/supershowwei/2016/04/11/000015>

<https://programmium.wordpress.com/2017/07/17/ef-code-first-auto-increment-key/>

## JS Array & List <a id="25"></a>

<https://ithelp.ithome.com.tw/questions/10191125>

## LINQ <a id="26"></a>

<https://stackoverflow.com/questions/21000287/date-difference-logic-in-linq>

<https://dotblogs.com.tw/chhuang/2008/05/01/3772>

<https://blog.xuite.net/f8789/DCLoveEP/23587655-LINQ+-+DISTINCT%E7%9A%84%E4%BD%BF%E7%94%A8>

<https://support.microsoft.com/zh-tw/help/2588635>

<http://coding.anyun.tw/2012/03/05/linq-to-entity-datatime-diff/>

<https://yangxinde.pixnet.net/blog/post/31357272>

<https://dotblogs.com.tw/dc690216/2009/09/13/10601>

<https://stackoverflow.com/questions/1042152/how-can-i-convert-iqueryablestring-to-a-string-array>

<https://dotblogs.com.tw/dc690216/2009/09/13/10601>

<https://stackoverflow.com/questions/7325278/group-by-in-linq>

## SQL Server 設計師模式關閉 <a id="27"></a>

<https://mitblog.pixnet.net/blog/post/39977377?m=off/>

## SQL <a id="28"></a>

<https://www.fooish.com/sql/left-outer-join.html>

<https://stackoverflow.com/questions/1748794/is-there-an-arraylist-in-javascript>

<https://www.1keydata.com/tw/sql/sqlorderby.html>

## CSharp Foreach index <a id="29"></a>

<http://jengting.blogspot.com/2014/08/foreach-index.html>

## CORS 預檢請求 <a id="30"></a>

預檢請求 就是為甚麼每次看network都會有兩條一樣的 但只有一條有response

<https://developer.mozilla.org/zh-TW/docs/Web/HTTP/CORS>

## Visual Studio 主題 <a id="31"></a>

<https://dotblogs.com.tw/junior78469/2013/11/24/131070>

<https://studiostyl.es/>

推薦下載

<https://drive.google.com/file/d/1fBGLVGSlYKN3WRAkaod5nRGJnrh1VbJ_/view?usp=sharing>

## Visual Studio Code Lens display Git info <a id="32"></a>

Enterprise、Professional 似乎要 企業版與專業版才有 ...

<https://stackoverflow.com/questions/55610991/is-there-any-codelens-add-on-available-that-shows-the-git-history-of-the-given-m>

## SQL USE DBNAME <a id="33"></a>

<https://stackoverflow.com/questions/10461861/use-database-command-on-sql-plus-oracle-11gr1>

## EF DBFirst 使用 DataAnnotations 屬性 與繼承 EF 實體 <a id="34"></a>

<https://dotblogs.com.tw/chentingw/2016/11/28/235523>

## HashSet 內容不重複的 Set <a id="35"></a>

<https://www.google.com/search?q=HashSet&rlz=1C1CHBF_zh-TWTW905TW905&oq=HashSet&aqs=chrome..69i57j0l7.549j0j7&sourceid=chrome&ie=UTF-8>

## Angular TypeScript 非同步 <a id="36"></a>

```TypeScript
getDataUsingSubscribe() {
	this.httpClient.get<Employee>(this.url).subscribe(data => {
	this.subscribeResult = data;
	console.log('Subscribe executed.')
	});
	console.log('I will not wait until subscribe is executed..');
	}

改成下面

async getAsyncData() {
	this.asyncResult = await this.httpClient.get<Employee>(this.url).toPromise();
	console.log('No issues, I will wait until promise is resolved..');
	}
```

<https://medium.com/media/5283ff33e9bd30397e3cf21f953daac1>

<https://medium.com/@balramchavan/using-async-await-feature-in-angular-587dd56fdc77>

## Exception.StackTrace 比較有用最好從最內層丟到最外 <a id="37"></a>

```json
{
    "PageSize": 0,
    "PageNum": 0,
    "TotalPages": 0,
    "TotalItems": 0,
    "Entries": [],
    "HasPreviousPage": false,
    "HasNextPage": false,
    "StackTrace": "   於 System.ThrowHelper.ThrowInvalidOperationException(ExceptionResource resource)\r\n   於 System.Nullable`1.get_Value()\r\n   於 Mxic.ITC.Portal.Service.BatchService.PAM115() 於 C:\\Users\\rognp\\Desktop\\旺宏_sourcecode\\MxicAppSample\\Mxic.App.Sample\\Mxic.ITC.Portal.Service\\Batch\\BatchService.cs: 行 1305",
    "StatusCode": 2
}
```

## LINQ GroupBy <a id="38"></a>

<https://dotblogs.com.tw/noncoder/2019/03/25/Lambda-GroupBy-Sum>

## Nuget Case Converter 套件 快速重構命名方法 <a id="39"></a>

<https://marketplace.visualstudio.com/items?itemName=munyabe.CaseConverter>

## AWS IPV6 應用 <a id="40"></a>

USECASE 高鐵 登入驗證 IP 需前後相同否則前端登入踢出

Azure 不提供

VPC 虛擬網路

安全性群組 (防火牆)

VM(很多設定)

站台架設 DNS IIS 防火牆設定

IPV6 > 4 預設

網卡同時包含 4 & 6

Client 只能選一個用

Site 吃 2 種 但要設定

---

jsonp - cors

json - cors 標頭

xmlhttp... / fetch 都有同源政策

src att 無視 cors ?

cdn 跨網域

## Csharp Docx 套件 <a id="41"></a>

Word 套印

修改內容 / 表格化

Templt 套版

pdf 通常 html 轉

## Line API (Bot / Notify / Login) <a id="42"></a>

註冊通知取得使用者資料選群組

類似一個人要把那個人的帳號加入，必須要把這個機器人的 token 存入 db 已再次使用

驗證 code 取客戶資料，透過 httpclient 由後端與 line api 溝通

平台已經有一些原本要寫 code 的功能但是要是官方付費帳戶

限制 ? bot 要錢 發送量 ?

收費 ?

應用情境 ?

line 註冊 平台上的帳號與賴綁定，做一些別的應用

bot 一問一答互動 多個框或特定字串

---

登入 另一個套件 類似 google 驗證 api

可以組 flex 發送

推波 push 要錢

其他有免費或使用扣打要去官網查

---

notify 與 push 差別在於 notify 是群發 ， Push 是私聊

使用情竟 客戶申請官方帳號有養人要讓特定的人進平台

官方帳號才有管理 crm 客戶管理其實有 line gui 管理後台

其餘要用 line develop

同個 providers 才會通，可以有管理員幫設定不用直接給權限 可以設定管理員

人工回應或機器人

要注意是綁定對話框

## UnitTest <a id="42"></a>

<https://docs.google.com/presentation/d/1IEZ3Ow6EekNYDxqzI9toHEgPiagYbZTiffCKjdfXha0/edit#slide=id.ga4518fb1b2_2_7>

<https://github.com/johch3n611u/Experience-of-Cinda-Company/tree/master/Meeting.SoftwareTesting>

## oracle 臨時表應用 <a id="43"></a>

<https://freetoad.pixnet.net/blog/post/25444612>

<http://www.aspphp.online/shujuku/oraclesjk/Oraclejc/201701/93025.html>

<https://www.itread01.com/content/1549304681.html>

<https://byron0920.pixnet.net/blog/post/85759990>

## String.TrimEnd 方法 去除尾數的 char <a id="44"></a>

<https://docs.microsoft.com/zh-tw/dotnet/api/system.string.trimend?view=netcore-3.1>

取得尾數 <https://blog.xuite.net/i20254/Work/111369008>

<https://blog.csdn.net/xushaozhang/article/details/77146091>

## Angular 元件改 style <a id="45"></a>

```css
::ng-deep .closeDateDropdown .ui-dropdown-label {
    color: blue;
    font-weight:bold;
 }
```

```html
<p-dropdown
styleClass="closeDateDropdown"
[disabled]="this.items.Status == 'ThreeOfOneWork' || this.items.Status === 'Closed' || this.items.Status === 'Invalid'"
appendTo="body" [options]="this.DetailCloseDate" required [(ngModel)]="items.CloseDate"
[disabled]="item.Disabled==false">
</p-dropdown>
```

## Primeng 惱人 table 跑板問題

在 p-table 掛 `[style]="{ overflow: 'auto',width:'100%' }"`

然後在 css 內覆蓋此屬性

```
::ng-deep .ui-table-scrollable-view{
    width: 2600px !important;
}
```


## linq order by <a id="46"></a>

<https://ithelp.ithome.com.tw/articles/10104089>

## oracle 坑 <a id="47"></a>

搭配 Ef 如果有新增 ef， db也要新增不然 會錯，如果同時也在 transation 會 lock db 直接卡死，此時如無 session 或相關權限 直接等於放棄急救...

<https://www.twblogs.net/a/5b8c48482b7177188331e0d9>

ORA-00054

## EF 版本更新錯誤 <a id="48"></a>

<https://www.coder.work/article/2615671>

## C# 字串處理 <a id="49"></a>

string.IsNullOrEmpty(ele.AccountDisableFormPortalNo)

string.empty <https://jasper-it.blogspot.com/2014/08/c-string-stringempty-is-more-efficient.html>

split <https://codertw.com/%E7%A8%8B%E5%BC%8F%E8%AA%9E%E8%A8%80/641792/>

## Swiper 輪播套件 <a id="50"></a>

<https://hackmd.io/@chupai/BkohH4KzL>

## css-background-patterns 背景產生器 <a id="51"></a>

<https://free.com.tw/css-background-patterns/>

## Google obfuscator Tool 混淆 <a id="52"></a>

tool.css-js.com

UglifyJS

WebWoker js ? 線呈

web api (pwa)

## HAP (HTML Agility Pack) 爬蟲 解析器 nuget 套件 <a id="53"></a>

Node Select XML 結構

Xpath 瀏覽器右鍵 COYP 可以取

或 CSS Selector nuget 套件

## XX.AA ?? Empty 安全措施 <a id="54"></a>

string.isNullorEmpty and Withespase

## Transtain 交易 <a id="55"></a>

ACID, 樂觀悲觀 Lock

鎖定模式 不當鎖定影響 隔離層級

鎖定粒度 鎖定範例

nolock(共用鎖定) tablelock ... 撈的到 補SQL

### 情境

開始

一堆邏輯

儲存時鎖死

存完釋放

不是一開始鎖


新隔離層級 SNAPSHOT TEMP 暫存模式

## og.title <a id="56"></a>

## Linq Queryable <a id="57"></a>

才能用如以下語法

```C#
data.Where(x =>
EntityFunctions.DiffMonths(x.SIGN_FORM_MAIN.CREATE_DATE, DateTime.Now) <= 3
&&
EntityFunctions.DiffMonths(x.SIGN_FORM_MAIN.CREATE_DATE, DateTime.Now) >= -3);
```

## .NET API 屬性路由 <a id="58"></a>

EnabledAnonymous

<https://www.google.com/search?q=.NET+API+Attributes&rlz=1C1CHBF_zh-TWTW905TW905&oq=.NET+API+Attributes&aqs=chrome..69i57.1436j0j1&sourceid=chrome&ie=UTF-8>

## Oracle Update Date <a id="59"></a>

> UPDATE PAMV2.PAM_IF_RESIGN SET ACCOUNT_CLOSE_DATE = DATE '2020-12-13';

<https://stackoverflow.com/questions/13497130/updating-a-date-in-oracle-sql-table/13497380>

## CSharp new 用法 <a id="60"></a>

```c#
public string Approved(HighPermissionForm Data, SignFormMain Sign)
        {
            foreach (var data in Data.Details)
            {
                var account = new Account();
                account.RefType = (byte)EnumAccountRefType.PAMHighPermission;
                account.FunctionType = (byte)EnumAccountFunctionType.HighPermission;

                if (Data.ActionType == (byte)EnumAccountActionType.New)
                {
                    account.Group = data.Group;
                    account.EmpNo = data.EmpNo;
                    account.EmpName = data.EmpName;
                    account.DeptNo = data.DeptNo;
                    account.ManageType = data.ManageType;
                    account.UpdaterEmpNo = Sign.ApplicanterEmpNO;
                    account.Attachment = Data.UploadFile;
                    new HighPermissionRepository().Add(account);
                }
                else
                {
                    account.Id = (decimal)data.DelRefId;
                    account.LastRefSignFormId = Sign.SignFromID;
                    new HighPermissionRepository().Remove(account);
                }
            }
            return "";
        }
```

## CSharp AutofacResolverHelper.Current.Container.ResolveNamed <a id="61"></a>

```C#
public class PageQuery<T>
    {
        public OrderBy OrderBy { get; set; }
        public int PageSize { get; set; } = 9999999;
        public int PageNum { get; set; } = 1;
        public T QueryObject { get; set; }
        public string Sort { get; set; } = "ID";
        public SortDirection SortDirection { get; set; } = SortDirection.Desc;
        public LazyLoadEvent LoadEvent { get; set; }
    }
    public class OrderBy
    {
        public string Field { get; set; }
        public int Type { get; set; }
    }
    public enum SortDirection
    {
        Asc = 1,
        Desc = 2
    }
--------------------------------------------------------------------------------------------------
public class AccountShift : Profile
    {
        public AccountShift()
        {
            CreateMap<ACCOUNT, Account>()
                        .ForMember(s => s.Id, opt => opt.MapFrom(s => s.ID))
                        .ForMember(s => s.FunctionType, opt => opt.MapFrom(s => s.FUNCTION_TYPE))
                        .ForMember(s => s.AccountType, opt => opt.MapFrom(s => s.ACCOUNT_TYPE))
                        .ForMember(s => s.IsCross, opt => opt.MapFrom(s => s.ISCROSS))
                        ;

            CreateMap<Account, ACCOUNT>()
                .ForMember(s => s.ID, opt => opt.MapFrom(s => s.Id))
                .ForMember(s => s.FUNCTION_TYPE, opt => opt.MapFrom(s => s.FunctionType))
                .ForMember(s => s.ACCOUNT_TYPE, opt => opt.MapFrom(s => s.AccountType))
                .ForMember(s => s.ISCROSS, opt => opt.MapFrom(s => s.IsCross))
                ;
        }
    }
}

--------------------------------------------------------------------------------------------------
                    var ACCOUNT = Entities.ACCOUNT.FirstOrDefault(x => x.ID == Item.AccountId);

                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile<AccountShift>();
                    });
                    var mapper = config.CreateMapper();
                    var Account = mapper.Map<Account>(ACCOUNT);
                    AutofacResolverHelper.Current.Container.ResolveNamed<IAccessRepository>(
                         ((EnumAccountFunctionType)Account.FunctionType).ToString()).Remove(Account);
```

## CSharp Debug 時不執行 [Conditional("DEBUG")] <a id="62"></a>

<https://dotblogs.com.tw/joysdw12/2014/03/14/asp-net-debug-release-if-else-conditional>

<https://stackoverflow.com/questions/6927263/enable-code-on-release-compilation>

## Oracle CSharp 抓最大 Id <a id="63"></a>

> var autoincrementId = Entities.ACCOUNT.Select(x => x.ID).DefaultIfEmpty(0).Max() + 1;

排除 table 是空的撈不到東西的狀況

## LINQ Not in <a id="64"></a>

<https://dotblogs.com.tw/dc690216/2009/09/13/10601>

## .NET 快取 <a id="65"></a>

<https://www.google.com/search?q=.net+%E5%BF%AB%E5%8F%96&oq=.net+%E5%BF%AB%E5%8F%96&aqs=chrome..69i57j69i65.8615j0j1&sourceid=chrome&ie=UTF-8>

前端 / 後端 快取

.NET Core 2 以上 內建 ResponseCache

<https://ithelp.ithome.com.tw/articles/10203293>

使用方式 Attr 掛在接口上 有參數可以調整

ConfigureService 要設置

有限制 e.g. get/put no post

舊的 outputCatch

## TypeScript <a id="66"></a>

型別 / 包 ES5,6

Tools - CSharp2TS / Eslint / TSlint

## 驗證模式 <a id="67"></a>

### SSO - 單一登入 Single sign-on

一種設計方式/平台，能夠整合各平台帳號，類似單一入口

### AD系統 - Active Directory

LDAP / 是微軟Windows Server中，負責架構中大型網路環境的集中式目錄管理服務（Directory Services）

### Windows 驗證 / IIS 開啟 WINDOWS 驗證

不用再打一次帳號密碼，默認吃 WINDOWS OS 開啟時輸入的帳密且如果都設定好，可以間接串 AD

BOWERS 網域 要綁 間接串 AD

### OAuth - 委任授權

資料 token 丟來丟去

### OIDC - OpenID連線

類似上述只是多丟了一些東西

## JWT - JSON Web Token <a id="68"></a>

<https://yami.io/jwt/>

JSON Web Token (JWT) 是由 Auth0 所提構出的一個新 Token 想法，這並不是一套軟體、也不是一個技術

## SQL 查詢技巧 UNION <a id="69"></a>

<https://www.itread01.com/content/1546941990.html>

## SYSTEM 系統 <a id="70"></a>

ERP：Enterprise resource planning

POS：Point of Sale

CRM：Customer Relationship Management

BI：Business Intelligence

eCOM：支援購物車、整合線上金流，可成立訂單，服務消費者做線上銷售的品牌官網。

app：

OMS：Order Management System

CDP：Customer Data Platform

<https://www.inside.com.tw/article/21346-8-info-systems-of-retail>

## IT學習歷程 <a id="71"></a>

一开始是增删改查。。。。

日子久了你就发现你的代码越来越复杂。。。。就会涉及到业务拆分。架构设计。

常用的静态资源比如图片，js文件占用带宽怎么办。。。。静态资源服务器。

文件的上传下载怎么提高效率。。。。。。。。。FastDFS。

消息推送的实时性怎么保证。。。。。。建立长连接吧netty，websockt。

用户开始越来越多了，一台服务器不够要多台。。。就会涉及到负载均衡。。。。

多台服务器下他们中间会有通信问题。。。。这就涉及到RPC远程调用。。。。。

特别是支付和认证这块。。。会产生对方接口调用过慢，网络等影响。就需要异步。。。

同时使用人数过多，不能让服务器爆炸吧。。。。。很多地方就要用到消息队列。。。

数据库数据量过大影响效率怎么办。。。。建立索引，分表分库。

常用信息访问过多占用资源怎么办。。。。。。NOSQL缓存吧。。。

IM下的点对点传输，多用户下的关系指数增长。。。。。。

以上是常见的场景应用。。。。背后涉及到的东西各有深度。。。。

设计模式，CAP，架构模式，SOA，服务治理，WebService,通讯协议，文件编码类型。。。。。

书到用时方恨少，你觉得没啥是因为你没用到。。。

## Yahoo奇摩新聞: 什麼是行銷自動化漏斗？所有企業都需要行銷自動化嗎？. <a id="72"></a>

<https://tw.news.yahoo.com/%E4%BB%80%E9%BA%BC%E6%98%AF%E8%A1%8C%E9%8A%B7%E8%87%AA%E5%8B%95%E5%8C%96%E6%BC%8F%E6%96%97-%E6%89%80%E6%9C%89%E4%BC%81%E6%A5%AD%E9%83%BD%E9%9C%80%E8%A6%81%E8%A1%8C%E9%8A%B7%E8%87%AA%E5%8B%95%E5%8C%96%E5%97%8E-080922115.html>

## [C#] 目前沒有執行ID 為{0} 的處理序。BUG

<https://dotblogs.com.tw/onebin/2017/11/21/150815>

## Active Directory: LDAP Syntax Filters

> (&(cn=*0)(givenName=J*))

<https://social.technet.microsoft.com/wiki/contents/articles/5392.active-directory-ldap-syntax-filters.aspx>

## JQ 選擇器返回物件 n.fn.init [0]

<https://stackoverflow.com/questions/34494873/why-is-my-jquery-selector-returning-a-n-fn-init0-and-what-is-it>

## PWA

<https://blog.techbridge.cc/2018/10/13/pwa-in-action/>

## i18n 土法練功

### AG 能吃 html Attributes innerHTML 不知道為啥 <div [innerHTML]="'MailOut.Content'|translate"> </div>

<https://stackoverflow.com/questions/44073674/why-innerhtml-property-from-javascript-cant-be-an-html-attribute>

## 由前端控顯示版本

```JAVASCRIPT
this.signFooterService.GetSignFooter(pProject, localStorage.getItem('l')).subscribe(res => {
            this.footerDESC = res;
        });
```

```c#
// pStrServiceCode => {"pStrServiceCode":"AA01_001","pStrLang":"zh-TW"}
public string GetSignFooter(string pStrServiceCode)
        {
            Entities = new Entities(ConnectionString.DefaultConnectionName);

            clsFooterDESC objDESC = JsonConvert.DeserializeObject<clsFooterDESC>(pStrServiceCode);
            PORTAL_SYSTEM_SERVICES objService = Entities.PORTAL_SYSTEM_SERVICES.FirstOrDefault(x => x.SERVICE_CODE == objDESC.pStrServiceCode);
            if (objDESC.pStrLang == "zh-TW")
                return objService.APPLICATION_DESCRIPTION?.Replace("\n","<br>") + "@" + objService.INFO_SECURITY_DESCRIPTION?.Replace("\n", "<br>");
            else
                return objService.APPLICATION_DESCRIPTION_EN?.Replace("\n", "<br>") + "@" + objService.INFO_SECURITY_DESCRIPTION_EN?.Replace("\n", "<br>");
        }
```

##

/// <summary>
/// 複製物件
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="obj"></param>
/// <returns></returns>
public static T Clone<T>(this T obj)
{
    var inst = obj.GetType().GetMethod("MemberwiseClone", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
    return (T)inst?.Invoke(obj, null);
}

## CSharp Excel 匯入

<https://stackoverflow.com/questions/11832930/html-input-file-accept-attribute-file-type-csv>

<https://primefaces.org/primeng/showcase/#/fileupload>

<https://ithelp.ithome.com.tw/articles/10218430>

[CSharp Excel 匯入範例 方式為先上傳到某個資料夾，再藉由名字去那個資料夾取 stream](/assets/csharpexcel.md)

下載則是特定資料夾才開放路徑去下載

## .NET Mail

<https://docs.microsoft.com/zh-tw/dotnet/api/system.web.mail.mailmessage.priority?view=netframework-4.8>

## AG AOT Error predefined keyword "import"

不能用 import 來取名組件，資料夾會變成跟 src 類似的權重的感覺，網路上查不到資料

<https://matthung0807.blogspot.com/2019/07/angular-7-can-not-determine-module.html>

![alt](/assets/agbug.png)

## PostMan

<https://identity.getpostman.com/>

用 PostMan 做 SPA 測試報告 Test

Interceptor Chrome 側錄 filter 重複執行 排程 搶票

Team Upgrade

## 直接 console call api

隨意一支 api 右鍵 copy fetch 即可再調用

## Visual Studio

Alt Shift 可以達到類似 VSCode 連續選的功能

## .NET Catch

```csharp
public PageQueryResult<Model.PORTAL_SYSTEM_SERVICES> GetPortalSystemServices(string serviceCode)
        {
            var apiUri = base.ITCPORTALAPIUri + "GetPortalSystemServices";
            var response = new PageQueryResult<Model.PORTAL_SYSTEM_SERVICES>();
            try
            {
                if (_cache.Contains("GetPortalSystemServices"))
                    return _cache.Get("GetPortalSystemServices") as PageQueryResult<Model.PORTAL_SYSTEM_SERVICES>;

                var jsonResult = RestSharpHelper.PostJson(apiUri, null, JsonConvert.SerializeObject(serviceCode));
                response = JsonConvert.DeserializeObject<PageQueryResult<Model.PORTAL_SYSTEM_SERVICES>>(jsonResult);
                _cache.Add("GetPortalSystemServices", response, DateTimeOffset.Now.AddMinutes(10));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                throw ex;
            }
            return response;
        }
```

## Csharp CopyPropertiesTo

<https://dev-felix72.blogspot.com/2017/12/be-caution-about-modified-entity-state-after-automapping-when-doing-ef-updating.html>

## Q: AutoMap 後異動 EF 寫不進 DB

參考: <https://dev-felix72.blogspot.com/2017/12/be-caution-about-modified-entity-state-after-automapping-when-doing-ef-updating.html>

A: 經過 AutoMapper 異動後，EntityState 已被移除追蹤， EF 再怎麼儲存異動都沒有任何作用，所以需要重新比對資料

```Csharp
// _user Mapper 完的 ORM
var entry = context.Entry(_user);
if (entry.State == EntityState.Detached)
{
  var set = context.Set<User>();
  User attachedEntity = set.Find(_user.UserId);

  if (attachedEntity != null)
  {
    var attachedEntry = context.Entry(attachedEntity);
    attachedEntry.CurrentValues.SetValues(_user);
  }
  else
  {
    entry.State = EntityState.Modified;
  }
}
// 經過以上判斷與調整實體狀態後就可以使用 Mapper 後的 ORM
var User = Entities.USER.FirstOrDefault();
User = _user
Entities.SaveChange();
```

## CSharp => Lambda 表示式

<https://zhidao.baidu.com/question/283430770.html#:~:text=1%E3%80%81c%23%E4%B8%AD%EF%BC%88%3D%3E,%E4%BE%A7%E7%9A%84lambda%20%E4%BD%93%E5%88%86%E7%A6%BB%E3%80%82>

```CSharp
delegate int Method(int a, int b);

int Add(int a, int b)
{
   return a + b;
}

Method m += Add;
Console.WriteLine(m(2, 3));

---

Method m += (a ,b) => a + b;
```

## EF => 印出自動生成的 SQL

<https://www.google.com/search?q=EF+%E5%8D%B0+SQL&rlz=1C1CHBF_zh-TWTW905TW905&oq=EF+%E5%8D%B0+SQL&aqs=chrome..69i57.3183j0j7&sourceid=chrome&ie=UTF-8>

DBContent.Database.Log = s => System.Diagnostics.Debug.WriteLine(s)

## oracle developer 垃圾 聚合鍵 bug

必須先停用才能刪除才能新增

![alt](/assets/oracledeveloper.png)

## BOM window.onbeforeunload

<https://blog.darkthread.net/blog/onbeforeunload-custom-message/>

        // window.onbeforeunload = function (event) {
        //     var event = event || window.event;
        //     if (event) {
        //         event.returnValue = '確定?';
        //     }
        //     return '確定?';
        // }

## VS CodeMap 視覺化程式碼之間的相依性

<https://docs.microsoft.com/zh-tw/visualstudio/modeling/map-dependencies-across-your-solutions?view=vs-2019>

## DbEntityValidationException

```c#
public partial class SomethingSomethingEntities
{
    public override int SaveChanges()
    {
        try
        {
            return base.SaveChanges();
        }
        catch (DbEntityValidationException ex)
        {
            // Retrieve the error messages as a list of strings.
            var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
    
            // Join the list to a single string.
            var fullErrorMessage = string.Join("; ", errorMessages);
    
            // Combine the original exception message with the new one.
            var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
    
            // Throw a new DbEntityValidationException with the improved exception message.
            throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
        }
    }
}
```

<https://edwardkuo.imas.tw/paper/2016/08/26/Net/2016/2016-08-27/>

<https://dotblogs.com.tw/wasichris/2015/01/24/148255>

<https://stackoverflow.com/questions/34456001/i-cannot-find-system-data-entity-validation-in-entityframework-5>

<https://shunnien.github.io/2015/11/26/trouble-shooting-DbEntityValidation/>

## Blazor

16:49 ㄩ\ㄔㄥ/ 架構圖

16:50 ㄩ\ㄔㄥ/ Webaxxx 與 js 比較

16:58 ㄩ\ㄔㄥ/ Webaxxx Spa

16:58 ㄩ\ㄔㄥ/ 優點

16:59 ㄩ\ㄔㄥ/ App 會有問題

16:59 ㄩ\ㄔㄥ/ 部署 後的 bin 可以知道到底是啥黑科技

17:04 ㄩ\ㄔㄥ/ 跟 mvc razor viewcomponent 之類的比較

17:06 ㄩ\ㄔㄥ/ 同時跟 js 庫使用？？區塊有分

17:09 ㄩ\ㄔㄥ/ 發佈後會有個沙盒。低階跑不動？？

## Autofac

<https://docs.google.com/presentation/d/e/2PACX-1vQmd3DIboFhjpUrlmpx3tW3QVG5qGh7CDIEVY0PaewAgBs2mr5NWyYK2NF1_a4vQnOXrO5AdBoHHGkF/pub?start=false&loop=false&delayms=3000>

<https://docs.google.com/document/d/1NHeQj-NoBNPtTMOdkeX1FcT8oN48JY__sRpqT5rpxTw/edit>

## JS ... 淺拷貝深拷貝


物件裡面有物件無法使用 ...

可用以下解決方法但只適用純資料，若遇 Function、Set、Map..等型態，失效。

```JS
let a = {o:{v:1}}
let b = JSON.parse(JSON.stringify(a));
```

來自 <https://kanboo.github.io/2018/01/27/JS-ShallowCopy-DeepCopy/> 

## 錯誤處理 ( 待完善 )

```c#
	    var response = new PageQueryResult<string>();

            try
            {
                PAMapplyFormService Service = new PAMapplyFormService();
                response = Service.UCPAM113(UCPAM113_List);

            }
            catch (DbException dbex)
            {
                _logger.Error("⭐⭐⭐PAM113⭐⭐⭐");
                throwException(dbex);
                response.StatusCode = (long)EnumStatusCode.Exception;
                _logger.Error(dbex.Message);
                _logger.Error(dbex.StackTrace);
                response.Message = dbex.Message + dbex.StackTrace;
            }
            catch (Exception ex)
            {
                _logger.Error("⭐⭐⭐PAM113⭐⭐⭐");
                throwException(ex);
                response.StatusCode = (long)EnumStatusCode.Exception;
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                response.Message = ex.Message + ex.StackTrace;
            }
```

## System.IO

1. 判斷資料夾是否存在，並新增資料夾

```c#
if (System.IO.Directory.Exists(資料夾路徑))
{
    //資料夾存在
}
else
{
    //新增資料夾
    System.IO.Directory.CreateDirectory(@"D:\temp\");
}
```

2. 判斷檔案是否存在

```c#
if (System.IO.File.Exists(檔案路徑))
{
    //檔案存在
}
```

3. 建立壓縮檔案

```c#
    /// <summary>
    /// 參考 https://dotblogs.com.tw/gelis/2016/09/04/161341
    /// </summary>
    public class ZipHelper
    {
        /// <summary>
        /// 壓縮包
        /// </summary>
        public class ZipOptions
        {
            /// <summary>
            /// 新壓縮檔路徑
            /// </summary>
            public string NewZipPath { get; set; } = "";
            /// <summary>
            /// 新壓縮檔名
            /// </summary>
            public string NewFileName { get; set; } = "";
            public List<string> FilePath { get; set; }
        }

        private static Logger _Logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// ZipArchive 成功回傳路徑壓縮檔案 / 失敗壓 log 回傳 "Fail" / 
        /// <para>如 ZipOptions NewZipPath、NewFileName 為空則預設第一筆壓縮位置與檔名寫入</para>
        /// </summary>
        /// <param name="_ZipOptions"></param>
        /// <returns></returns>
        public string CreateZipArchiveByFilePath(ZipOptions _ZipOptions)
        {
            string distinationFile = "Fail";

            try
            {
                string workPath = _ZipOptions.NewZipPath == "" ? Path.GetDirectoryName(_ZipOptions.FilePath[0]): _ZipOptions.NewZipPath; // 待壓縮檔案目錄
                Console.WriteLine("workPath：" + workPath);
                _Logger.Debug("workPath：" + workPath);
                string targetZipFileName = _ZipOptions.NewFileName == "" ? string.Format("{0}{1}", Path.GetFileNameWithoutExtension(_ZipOptions.FilePath[0]), ".zip") : _ZipOptions.NewFileName + ".zip"; // 新壓縮檔案名稱
                Console.WriteLine("targetZipFileName：" + targetZipFileName);
                _Logger.Debug("targetZipFileName：" + targetZipFileName);
                distinationFile = Path.Combine(workPath, targetZipFileName); // 新壓縮檔案路徑
                Console.WriteLine("distinationFile：" + distinationFile);
                _Logger.Debug("distinationFile：" + distinationFile);

                // 壓縮用 Stream
                using (var fileStream = new FileStream(distinationFile, FileMode.CreateNew))
                {
                    // 設定 zip archive
                    using (var archive = new System.IO.Compression.ZipArchive(fileStream, System.IO.Compression.ZipArchiveMode.Create, true))
                    {
                        // 壓縮幾個檔案就必須做幾次底下的重複動作
                        foreach (var FilePath in _ZipOptions.FilePath)
                        {
                            // Read 檔案 Stream
                            FileStream f = new FileStream(FilePath, FileMode.Open, FileAccess.Read);

                            try
                            {
                                // Stream 轉為 Bytes
                                byte[] ItemBytes = BinaryReadToEnd(f);

                                string createEntryFileName = Path.GetFileName(FilePath); // 新壓縮檔案內容名稱 ( 打開 zip 能看到的檔案 )
                                Console.WriteLine("createEntryFileName：" + createEntryFileName);
                                _Logger.Debug("createEntryFileName：" + createEntryFileName);

                                // 建立 zip archive ( 打開 zip 能看到的檔案 )
                                var zipArchiveEntry = archive.CreateEntry(createEntryFileName, System.IO.Compression.CompressionLevel.Fastest);
                                BinaryReader bs = new BinaryReader(f);

                                // 開啟 zip Stream
                                using (var zipStream = zipArchiveEntry.Open())
                                {
                                    // 寫入 Bytes
                                    zipStream.Write(ItemBytes, 0, ItemBytes.Length);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.Write("Inner ZipHelper Error");
                                Console.Write(ex.Message);
                                Console.Write(ex.StackTrace);
                                distinationFile = "Fail";
                                _Logger.Error("Inner ZipHelper Error");
                                _Logger.Error(ex);
                                _Logger.Error(ex.Message);
                                _Logger.Error(ex.StackTrace);
                            }
                            finally
                            {
                                f.Close();
                                Console.Write("Inner FileStream Close");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("ZipHelper Error");
                Console.Write(ex.Message);
                Console.Write(ex.StackTrace);
                distinationFile = "Fail";
                _Logger.Error("ZipHelper Error");
                _Logger.Error(ex);
                _Logger.Error(ex.Message);
                _Logger.Error(ex.StackTrace);
            }
            finally
            {
                Console.Write("FileStream Close");
            }

            return distinationFile;
        }

        /// <summary>
        /// Stream 轉為 Bytes
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public byte[] BinaryReadToEnd(Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
    }
```

### 複製檔案 c3
 
<https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/file-system/how-to-copy-delete-and-move-files-and-folders>

## MVVM

如果用 mapping 替换 controller，那你就得到了 MVVM , model-view-view-model。

但 MVVM 之所以可以普及，是因为先进的框架和方法论以及计算性能的提高提供了标准化的映射管线，再加上目前 99% 的 app 无论是 web 还是原生应用程序都只是个调用后台 REST API 的外壳，说穿了就是请求增删改查，那么标准化的 mapping 就可以满足需要，自然可以踢开 controller。

但那些需要在本地执行复杂逻辑的 app 就不是这么回事了，你做个游戏现在还是得 MVC. 实际上，经典的本地胖客户端催生了 MVC，因为那时候所有的业务逻辑都是跑在本地的，仅仅有 model-view 的同步和映射远远不够，逻辑必须放在 controller 里面。

那么现在呢？前端就是 MVVM，而后端都微服务化了，而且也鼓励无状态化所以也不用建 model 来描述状态了，基本上可以看作一个简单输入输出的函数，所以从某种意义上讲 model 和 view 放在了前端，controller 给放到后端了。

## Class Format JSON Log Beautiful

```C#
WaitFormatClass Data = new WaitFormatClass();
var JsonLog = JsonConvert.SerializeObject(WaitFormatClass, Formatting.Indented);
_Logger.Debug(JsonLog);
```

## operation not permitted, rename

真的不能改名，關閉 vscode setting 內的 Editor Rename : Enable Preview

## PrimeNG 開發相關紀錄

1. Router 開發模式
2. Router ( 無 / 有 ) 參數路由
3. 相關元件應用
4. 測試用假資料製作

[相關程式碼參考](https://github.com/johch3n611u/Experience-of-Cinda-Company/tree/master/assets/self-account-check)

## EF 坑

撈出來有 PK 的資料不可以直接 = New 要拆包一項項 = 不然會複製到類似 PK 的索引的感覺，Add 或 Save 會報錯

## Csharp task 應用 & 前端

[DemoCode](https://github.com/johch3n611u/Experience-of-Cinda-Company/tree/master/assets/self-account-check)

## SQL SERVER 設計模式預設欄位增加描述

http://adolph.com.tw/archives/62/sql-server-management-studio-table%E8%A8%AD%E8%A8%88%E6%99%82%E4%B8%80%E4%BD%B5%E9%A1%AF%E7%A4%BA%E6%8F%8F%E8%BF%B0%E6%AC%84%E4%BD%8D/

## Nginx 反向代理防卡 IP 

https://github.com/johch3n611u/Experience-of-Cinda-Company/blob/master/assets/%E5%8F%8D%E5%90%91%E4%BB%A3%E7%90%86config.txt

## AG 要引入外部 url 要用 DomSanitizer 类

https://blog.csdn.net/xjtarzan/article/details/103635010

1.在需要使用外部url链接的ts文件中，引入DomSanitizer类
import { DomSanitizer } from '@angular/platform-browser'; 
 
export class safeHtml {  
  safeUrl: any;
  constructor(private sanitizer: DomSanitizer) {}

  //2.在需要使用转换后的url地方加上
  getSafeUrl(url){
      this.safeUrl = this.sanitizer.bypassSecurityTrustResourceUrl(url); 
  }

2. html页面中 (这里以iframe标签为例)
<iframe [src]="safeUrl" style="width: 100%; height: 100%; border: none"></iframe>

## Jenkins 文件

1. https://web.devdon.com/archives/10
2. https://www.jianshu.com/p/629fefe5468a
3. https://zeckli.github.io/zh/2017/10/01/resolve-gitlab-permission-denied-zh.html
4. 需要用 SSH 手動拉下來的專案才有辦法吃到 openSSH ... ?

## FineReport

### v10.0

#### 移機設定

1. X-Frame-Options' to 'sameorigin' 錯誤
   打開報表數據平台介面，列表 `管理系統 > 安全管理 > 安全防護 > 關閉 Security Headers`

#### 資料串接

##### 數據庫連結

[文檔參考](https://help.fanruan.com/finereport/doc-view-2880.html)

`伺服器 > 定義資料連結 > 新增`

##### 資料集

資料集類似 View 表，分為報表專用的 "區域" 範本資料集與 "全域" 共用的伺服器資料集，新增方式只要從 FR 設計器的左下角新增即可。

#### 報表搬移

本地 FR 設計器左上角選定檔案後，可點選上方檢視位置，將 cpt、frm 報表複製至伺服器的相同位置，並確定伺服器上的 FR 設計器，列表 `伺服器 > 定義資料連結 > DB` 的暱稱是否相同於本地端，否則須修改報表內資料集撈取的 SQL。

也有如 [串接遠程設計](https://help.finereport.com/finereport8.0/doc-view-133.html) 的方法

#### 報表設計

[詳細報表功能](https://www.finereport.com/tw/knowledge/finereport/3-report-forms.html)

報表以副檔名分為 frm、cpt，以格式與功能分為 [普通報表] [聚合報表] [決策報表]，詳細請看上述網址。

#### 單點訪問

[原理](https://www.codenong.com/cs107068327/)

官方免费版具有全部系统功能，但是只有2个并发，也就是2个以内用户可以访问，第三个用户访问就会提示未注册,无法访问，实现原理就是在服务器上做特殊处理，让所有访问该服务器的用户都从图中A点访问，仅仅是对服务器进行部署，并非修改了FR或者BI软件本身，所以在FR服务器看来只是一个用户在访问，永远不会提示超出并发数

[Nginx 安裝](https://www.cnblogs.com/taiyonghai/p/9402734.html)

[Nginx 設定](https://linuxize.com/post/nginx-reverse-proxy/)

proxy_set_header X-Forwarded-For 127.0.0.1;

##### 台灣區費用

個人 IP 買斷 9 萬，軟體買斷 200 萬

## Csharp 抓離自己最近獲最遠時間最快方法

1. 排序後撈第一筆...

## .net core 吃 js 傳值 可能會被 js 偷偷轉型導致後端撈不到須注意

只要基底用物件包住內容參數大小寫一樣就能抓到，如果抓不到參數可套用 dynamic 型別先確定傳了啥上來

```c#
 public async Task<ResponseBase<ProductSaveDataResponse>> SaveData([FromBody] dynamic Args)
```

## 小技巧 angular 當 radio 需要 number 時 value 補中括弧即可

```js
<input [value]="2" type="radio" id="cShape2" name="cShape" [(ngModel)]="activity.cShape">
<label for="cShape2">方形</label>
```

## .net core ef 坑

//第一次需先安裝SDK // bug 需要指定版本 --version 5.0.5 

dotnet tool install --global dotnet-ef --version 5.0.5

//需要進到專案資料夾而不是方案

dotnet ef dbcontext scaffold "Server=104.42.40.253;Database=dbPXMart;Trusted_Connection=False;user id=id;password=pp;" Microsoft.EntityFrameworkCore.SqlServer --output-dir EF/path -f -v

## csharp 上傳檔案

https://blog.johnwu.cc/article/ironman-day23-asp-net-core-upload-download-files.html

```c#
   /// <summary>
        /// 上傳檔案
        /// </summary>
        /// <param name="configPath"></param>
        /// <param name="isShowFolder"></param>
        /// <returns></returns>
        //public List<string> uploadFile(string configPath, bool isShowFolder)
        //{
        //    var result = new List<string>();

        //    var basePath = System.Web.HttpContext.Current.Server.MapPath("~/" + configPath);
        //    var files = HttpContext.Current.Request.Files;

        //    if (files.Count > 0)
        //    {
        //        if (!System.IO.Directory.Exists(basePath)) { System.IO.Directory.CreateDirectory(basePath); }

        //        foreach (string fileName in files.AllKeys)
        //        {
        //            var file = files[fileName];
        //            var filePath = basePath + file.FileName;
        //            var newFileName = file.FileName;

        //            newFileName = string.Format("{0}_{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), file.FileName);
        //            filePath = basePath + newFileName;

        //            file.SaveAs(filePath);
        //            if (isShowFolder)
        //                result.Add(configPath + newFileName);
        //            else
        //                result.Add(newFileName);
        //        }
        //    }

        //    return result;
        //}
	
	
```

## iis 圖片上傳也必須要設定虛擬目錄才能讓外部讀的到上傳檔案的資料夾

## NLOG 分資料夾要注意參數

<https://ithelp.ithome.com.tw/articles/10206339>

## Ediot Line Number 純 CSS 解決方法

<https://jsfiddle.net/vaakash/5TF5h/>

```
textarea{
    background: url(http://i.imgur.com/2cOaJ.png);
background-attachment: local;
background-repeat: no-repeat;
padding-left: 35px;
padding-top: 10px;
    border-color:#ccc;
}
```

## .net core 如果上傳查詢，可能部分欄位會 null ，導致整包不吃所以最好裡面宣告時要多個 ?

// TS type Date 的 "" 會讓 .NetCore 整包資料吃不到須轉為 null

## 情境 後端在別的 host 但資料源在同一台電腦，其它前台訪問這個後端會 Cors
	
此時可以在其他前台的 host 架虛擬目錄連到那個資料源，直接開個門即可
	
不用到原開口開跨域白名單之類的

## 要找主機位置直接 ping domain 就可以知道 ip 位置 如果是 dns 就要再問，不是的話開 ip 那台伺服器的 iis 或 apha 找 domain 就能找到程式放置位置 

## Webform 要更版要更新 aspx cs bin 內的 pdb dll

## 用 GetValue 方法遍歷寫 log 時要注意排除 EF 關聯，不然會報錯 
	
## CSharp 模淑搜尋 result.Where(x => Args.cLink.Contains(x.cLink) || x.cLink.Contains(Args.cLink))

## 接別人的 api 不知道型別可以全都先用 string 接回來再轉
	
## 送審機制 - 利用 jsondata 反解
	
## 安卓 webview 跟 ios 不一樣 / 上傳 相機 各種反應都需要對接確定 ( 如果外包的話 )

## 研討會時隔快一年 1. 爬蟲應用 2. Azure 雲基礎 <a id="2"></a>

* keypoint
  * db 會設定時區，會影響 linq 轉為 sql getDate 時 +8 +0 問題
  * 雲 db、vm 會可以看很細的報表易於除錯與效能調教
  * 公司也是用 nas 私有雲
  * 如果某些節日需要大流量，就可以開高運算或流量，這個是公有雲的好處
  * 私有你就會被長期資源或短期資源影響，長期沒用那麼多流量浪費，不升級某些時候又跑不動
  * 公有雲很容易外洩，一外洩資安風險也超級高
  * 感覺接人的案子也是先放對方或我方主機，然後再考慮公有雲
  * azure 24小時 360 天都可以上去設定 很危險
  * 所以建議混和雲
  * 建議客戶某個簡單的 range 雲，在客戶需求與壓力測試下，在調整看是要哪種等級的雲，使用的人/一台可以多少人
  * gcp aws azuer 都有提供 線上估價單可以直接跟客戶報價
  * https://cloud.google.com/products/calculator/
  * https://calculator.s3.amazonaws.com/index.html?lng=zh_CN#
  * https://azure.microsoft.com/zh-tw/pricing/tco/calculator/
  * https://tw.etsoutdoors.com/715090-azure-dtu-calculation-BEOASO

## 為了解一個奇怪的 nb date pick 套件的時間問題搞半天

this.Date 是空的 null 但 nb date pick 沒吃 '' 吃 null 就會自動 new Date(null) == 1970 XX XX 

偏偏這欄位又不是必填或有預設值

而 new Date(this.Date) object 也不等於 new Date(null) object 只好用以下智障方法 ...

this.Date = this.Date.toString() == new Date(null).toString() ? '' :  this.Date;

## jose-jwt 不同站台，版本也要一樣，不然加解密還是會錯，但搞不懂的是都指定解密機制了，只是版本錯演算法就錯 ? 還是有啥底層我不知道 ...

## 指定時間格式序列化，轉JSON再轉回來比對時會錯誤

https://stackoverflow.com/questions/38276174/newtonsoft-json-customize-date-serialization
	
```
 // 時間欄位處理 2022-06-15T11:51:23.4881436+08:00 <=> 2022-06-15T11:51:23.487
                foreach (PropertyInfo propertyInfo in Entrie.Entity.GetType().GetProperties())
                {
                    if (propertyInfo.PropertyType == typeof(DateTime))
                    {
                        var Value = ((DateTime)propertyInfo.GetValue(Entrie.Entity, null));
                        object Time = Value.AddHours(-8);
                        Entrie.Entity.GetType().GetProperty(propertyInfo.Name).SetValue(Entrie.Entity, Time);
                    }
                }
```

## Vue Root 在 VueConfig 設定，也是不熟搞半天 

## 舊版 IIS 與 舊版 Http Methods 原生/自定義 ( get post pull put patch delete etc... ) 互有衝突會造成 405 搞半天
	
## 資料防竄改
	
Table Name		tblDataTamperProof			表格名稱		資料防篡改設定		第三張表的設定檔	
Description		資料防篡改排程設定檔								
PKey	欄位名稱		格式	可Null?		欄位說明				
v	cID		int	N		審核ID				
	cTable		varchar(40)	Y		原表				
	cCompareTable		varchar(40)	Y		比較表				
	cColumn		varchar(255)	Y		加密用column				
	cIsEnable		bit	N		啟停用				
	cAutoRecovery		bit	N		自動還原				
	cCreateDT		datetime	N		建立日				
	cUpdateDT		datetime	N		更新日				
										
Table Name		tblFileTamperProof			表格名稱		檔案防篡改設定		拆出來的排程	
Description		檔案防篡改排程設定檔								
PKey	欄位名稱		格式	可Null?		欄位說明				
v	cID		int	N		審核ID				
	cPath		varchar(40)	Y		目錄				
	cComparePath		varchar(255)	Y		比較目錄				
	cIsEnable		bit	N		啟停用				
	cAutoRecovery		bit	N		自動還原				
	cCreateDT		datetime	N		建立日				
	cUpdateDT		datetime	N		更新日				
										
Table Name		tblDataCompare			表格名稱		資料防篡改紀錄檢索			
Description		紀錄資料的Hash值以及要還原的物件資料								
PKey	欄位名稱		格式	可Null?		欄位說明			over write savechange	盡量包成 dll
v	cID		int	N		流水號				
	cTableName		varchar(40)	N		資料表名				
	cPKeyName		varchar(255)	N		PKey欄位名稱				
	cPkeyValue		bit	N		PKey值				
	cObjectJson		nvarchar(MAX)	N		資料物件Json				
	cHash		varchar(255)	N		Hash值			MD5	
	cCreateDT		datetime	N		建立日				
	cUpdateDT		datetime	N		更新日				
						api 名稱				
Table Name		tblCompareReport			表格名稱		防篡改比較表			
Description		紀錄比較過後的報告								
PKey	欄位名稱		格式	可Null?		欄位說明				
v	cID		int	N		流水號				
	cDataCompareId		int	Y		資料防竄改檢索表流水號				
	cFilePathName		nvarchar(300)	Y		防竄改檔案名稱路徑				
	cIsAutoOverWritten		nvarchar(100)	N		狀況				
	cCreateDT		datetime	N		建立日				
	cUpdateDT		datetime	N		更新日				
										
1	快照功能	當資料結構異動時，更新整張比較表，保持後續比對正常				SnapshotDataCompare			根據 table 去異動	
2	更新功能	單筆資料透過程式異動時，呼叫此功能更新比較表內對應的資料行				DataCompareByConfig \ FileCompareByConfig			單筆	
3	還原功能	比對資料不相同時，進行該筆資料的還原複寫				DataOverwrite \ FileOverwrite			通知確定後才單筆還原		

## SQL 效能調校

設計準則 語法技巧 工具 索引

越短越好、不會變動
	
主鍵索引可以提升搜尋效能，沒 PK 時會被自動建立叢集索引，一定會有除非資料不會被刪
	
非叢集索引數量與使用率須注意
	
WHER JOIN ORDER SELECT 索引順序

![image](https://user-images.githubusercontent.com/46659635/177952568-a358d3dd-feb8-4160-9b54-846c6336728b.png)

![image](https://user-images.githubusercontent.com/46659635/177952958-62bd4541-5107-47c8-832a-1a18c32d5748.png)

![image](https://user-images.githubusercontent.com/46659635/177953332-501db7b3-4e4a-42d3-84cb-04882dbc00e7.png)
	
https://www.google.com/search?q=TABLE+SCAN&rlz=1C1CHBF_zh-TWTW905TW905&oq=TABLE+SCAN&aqs=chrome..69i57.8917j0j7&sourceid=chrome&ie=UTF-8

盡量避免 Table scan
	
二元樹
	
![image](https://user-images.githubusercontent.com/46659635/177953882-3453140b-f9c7-4586-b709-317155488700.png)

![image](https://user-images.githubusercontent.com/46659635/177954323-05476f7d-de71-4c62-91cc-2bec6813aa94.png)

![image](https://user-images.githubusercontent.com/46659635/177955028-f5a3bf45-a864-4057-a794-be0b8b31ae50.png)

![image](https://user-images.githubusercontent.com/46659635/177958325-341fcd34-bc5f-4311-a634-8bd47e51da5b.png)

https://ithelp.ithome.com.tw/articles/10193063

![image](https://user-images.githubusercontent.com/46659635/177958398-b460559b-f3d8-4bfb-ab15-6921b6bccc51.png)

叢集有排序的在 DB 內只能有一個

![image](https://user-images.githubusercontent.com/46659635/177958498-10e6da14-a5af-46ac-a0b6-371d289a38fd.png)

![image](https://user-images.githubusercontent.com/46659635/177958930-e1193634-e7cf-4f7f-b3f9-8ba59c87e73b.png)

![image](https://user-images.githubusercontent.com/46659635/177958953-8b3a72ce-61df-4d5e-a45f-0862fa6dada3.png)

![image](https://user-images.githubusercontent.com/46659635/177959312-26576877-3c14-4cdb-bab1-3f22f1a5cf2e.png)
	
![image](https://user-images.githubusercontent.com/46659635/177960355-6860ae44-8a0d-4ff4-be02-2fe3607bd712.png)

# Appservice Paas

不像 VM 沒有實體或虛擬的機器，部屬需要新的方式例如擷取設定檔案內的訊息用 FTP 發布，或 AZURE 下載設定檔用 VS 發布工具
	
![image](https://user-images.githubusercontent.com/46659635/177962400-fbd44d95-90ec-4197-b5c8-9d9d02f2e54c.png)
	
![image](https://user-images.githubusercontent.com/46659635/177962322-d8480d9a-1bc3-4069-b210-caa6e8aa585f.png)

機器等級或數量
	
![image](https://user-images.githubusercontent.com/46659635/177964605-0a0c06c6-42b3-4432-abcd-eceadb22a21a.png)
	
![image](https://user-images.githubusercontent.com/46659635/177964662-51816b74-3864-4c96-b884-6859a3a1408b.png)

	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
