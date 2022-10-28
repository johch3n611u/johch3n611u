良興電子商務網站工程師 : 實際內容請至Readme.md閱讀，協同四位工程師，共同開發與維護部分良興電子商務平台，與14個品牌前台電子商務平台，形象建立與後台CMS等相關功能、後台ERP、SCM、CRM、HCM、FMS、KMS、WMS等相關功能，服務對像包含EPSON、羅技、微星、AIMEDIA、ASO、全國電子...，包含50個以上的站頁，18個系統資料庫，超過100支以上的SQL Server Agent、Winform、Jenkins排程，20個雲與本地Server，每日維護管理、專案開發、新技術重構...，團隊合作使用 Scrum、OKR、KPI、TRELLO 任務管理、Git/SVN 版本控制。

---
20191115 - bug暫不修復 : gitbook同步上github，部分程式碼增加，造成連結失效。 
details summary 標籤消失等 ... 計畫等離職後不在更新gitbook再將此bug修復。 
20200113 - 暫時人工修復不挑戰開源碼。 
---

# eclife_Web_Developer_Products

<details>
  
<summary>縮寫全名</summary>


MPA : Multi Page Application

SPA : Single Page Application

AMP : Accelerated Mobile Pages

RWD : Responsive Web Design

PWA : Progressive Web Application

OPW : One Page Web

SSR : Server Side Render

CSR : Client Side Render

CMS : Content Management System

ERP : Enterprise Resource Planning

SCM : Supply Chain Management

CRM : Customer Relationship Management

RFM : Recency Frequency Monetary

HCM : Human Capital Management System

PLM : Product Lifecycle Management

PDM : Product Data Management

BPM : Business Process Management

MES : Manufacturing Execution System

FMS : Facility Management System

KMS : Knowledge Management System

WMS : Workflow Management System

OKR : Objectives and Key Results

KPI : Key Performance Indicators

Wireframe 線框圖

Mockup 視覺槁

Prototype 原型

</details>

#### 良興電子商務網站工程師

> **公司工作橫縱結構**
>
> **專案較複雜的頁面由設計部門完成頁面切版與Banner等UI，** </br> 
> **協同數據發展部與需求單位與工程師討論UX並完成。** </br> 
> **團隊利用 Scrum、OKR、KPI、TRELLO 任務管理、Git/SVN 版本控制。** </br> 
> **工程師團隊工作橫以品牌切割從前端、後端至資料庫與架站、排程。** </br> 
> **縱為各品牌之間維護需求或功能新增相互支援。** </br>

**負責開發維護**

> **主 C# / VB / MPA / SSR / ASP.NET 4.7 / MVC 5 / WebForm / WebAPI / Winform ...**
>
> **次 .NET Core 2.1 / Razor / SPA / CSR / HTML5 / Vue.js ...**

**註 :** .Net Core 架構，因從 ASP.NET MVC 轉上去的，所以沒有使用到 DI Container 撰寫 Service、Component，使用公司自製的Module類別核心，使用少量的 ADO.NET Entity Framework ， 大部分使用 Dapper ORM 套件，操作 Transact-SQL 查詢語言。 

### 達成目標

> * **兩個禮拜內**
>
>   **從只會C\#與較熟悉.Net MVC，到熟悉前端框架EasyUI、後端框架.net Webform、VB。所架構之前台後台系統。**
>
> * **第三個禮拜至離開前**
>
>   **協助CRM平台專案負責工程師架構前端@Razor、後端.net MVC、MSSQL、IIS等相關網管協定之架設與程式撰寫、WebAPI串接。**
>
> * **第二個月末至第四個月末**
>
>   **從不會Winform到獨立處理精技上架購物中心API商品上架約一萬行的程式，包含十三隻API功能，加密與後台ERP系統串接。**
>
> * **期間**
>
>   **持續處理公司其他單位需求單日常維護與開發。**

### 目錄 <a id="top"></a>

> **專案**
>
> 1. [EClife MPA SSR CRM平台 部分專案](#A)
> 2. [精技電腦\(雅虎&Momo\_SCM\_API\MPA SSR 後台SCM管理系統\) 專案](#B)
> 3. [系統發展部\_需求單電子化 專案](#C)
>
> **日常維護與開發較完整如下 ...**
>
> 1. [Sastty-商品包裝問券頁面 實作](#P15)
> 1. [EClife ERP後台 報表統計匯出 / SQL預存排程 實作](#P14)
> 1. [全國電子 AP程式\_API串接 / 後台ERP管理系統顯示 實作](#P13)
> 1. [MSI MPA首頁Menu改版 實作](#P12)
> 1. [EPSON MPA前台動態Megamenu / 後台banner上稿CMS管理系統 實作](#P11)
> 1. [Sastty、aimedia MPA前台春聯廣告頁 / 後台CMS管理系統 實作](#P10)
> 1. [EClife MSSQL KPI報表重構 實作](#P9)
> 1. [Lab101 MPA文章前台頁面 / 後台CMS管理系統 實作](#P8)
> 1. [Lab101 MPA產品頁需求更改 實作](#7)
> 1. [Lab101 MPA金流購物車付款頁面 互動 實作](#P6)
> 1.. [Sastty MPA抽獎活動登入頁面 實作](#P5)
> 1. [EasyUI ERP後台系統 功能維護](#P4)
> 1. [AP API/FTP 排程API載入資料庫 實作](#P3)
> 1. [Sastty ERP後台抽獎功能清單轉存 實作](#P2)
> 1. [EClife MSSQL KPI報表 實作](#P1)
>
> **其餘較重複請看**[**日記**](https://app.gitbook.com/@johch3n611u/s/-1/)
>
> 1. epson MPA前台menu 後台CMS管理系統 實作
> 2. 全國電子 ERP後台報表修改 實作
> 3. epson MPA前台登入送購物金 實作
> 4. epson recycle MPA前台網站需求 實作
> 5. MPA CRM平台 排程預存整理 料庫備份 iis 架設 db 建置 token 設定
> 6. MPA ERP 全國電子後台 會員權限管理 實作
> 7. MPA ERP ECLIFE後台 業務綁定報表 實作

</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>
</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>

#### EClife CRM平台 部分專案 [▲BackToTop](#top) <a id="A"></a>

使用技術 : C# / .NET MVC5 /SendGrid /Tableau /POWERBI /analytics.js /embed.js /chart.js /googlechat /datatable.js /GitSourceTree ...

> **需求**: 根據數據分析部需求提供應用...。
>
> * [實作包含]
> * `會員RFM歸戶(B2B、B2C...)流程優化`
> * `分類會員EDM寄送等相關行銷自動化`
> * `datatable商品&會員標籤系統`
> * `DB同步、資料清洗、欄位設計、排程預存整理`
> * `iis 架設 db 建置 tableau token 設定`
> * `報表圖形化與產出`
> * `串接 SendGrid電子報系統`
> * `嵌入 POWERBI、tableau、GA API報表顯示與應用`
> * `架設 tableau server`
> * `串接 tableau api 並嵌入應用` 

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/1.jpg)

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/2.jpg)

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/3.jpg)

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/4.jpg)

**tableau 串接架設完後製作教學供企劃使用**

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/5.jpg) 

**sendgrid 串接電子報系統**

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/6.jpg) 

</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>

#### 精技電腦\(雅虎&Momo\_SCM\_WebAPI\_AP\) 專案 [▲BackToTop](#top) <a id="B"></a>

使用技術 : .NET / restful API / easyui / mssql / 加密解密編碼 / Winform ...

> **需求**: 在技術文件不齊全的狀況下，與雅虎業務溝通，加密編碼解密串API，將電商商品結構資料抓取，比對良興本身商品結構資料，上架精技商品資料，約13支API與後台介面crud，排程系統...。
>
> * \[實作包含\]
> * `Momo&Yahoo_API文件新舊比對`
> * `與Momo&Yahoo業務溝通在無法解決問題情況下找其他方案`
> * `java元件/程式碼解意轉為.net`
> * `架設java程式碼環境eclipse/元件建置，擷取正確加密代碼`
> * `header AES CBC PKCS5Padding / hmacsha512 加密`
> * `header base64 / UTF_8 / Hex 轉碼`
> * `CookieCollection stream request response應用`
> * `取商品結構串接api`
> * `反序列化jsonobject轉存sql，約41種類8百多項目，結構化標籤約十五萬筆`
> * `後台ERP平台，結構畫類別、屬性選單，easyui、mssql應用`
> * `ap winform 、webapi排程串接商品類別序列化提報上架api`
> * `協同同事取token將影音圖片stream提報雅虎amazon s3上傳檔案`

#### [▶](https://github.com/johch3n611u/EC_Web-AP_Developer/tree/master/Unitech_API_20191223)

**CSV 上傳上架清單**

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/7.gif)

**操作LOG & API上架**

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/8.gif)

**上架欄位表單**

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/9.gif)

**java元件/程式碼解意轉為.net**

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/10.jpg)

<!-- ![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/20191223picture/GIF%202019-12-23%20%E4%B8%8B%E5%8D%88%2002-10-14.gif) -->

<!-- ![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/20191127/3964.jpg) -->

* 20191101 WinFrom Class 模組化 與 加密request 而後轉至WebAPI後台CMS v1

* 20200107 優化AWS存取圖片速度2-3秒降到無感覺&介面UIUX調整 v2

  ![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/11.jpg)

* 20200110 AWSLoading畫面與連動欄位暫存等UIUX調整與BUG修正 v3

  ![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/12.jpg)

</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>

#### 系統發展部\_需求單電子化 專案 [▲BackToTop](#top) <a id="C"></a>

使用技術 : webform架構改良之easyui + api、chart.js、AJAX、JQ

> **需求**: 量化質化系統發展部工作內容，合理安排工作順序、時程管控。
>
> * [實作包含]
> * `UML需求單流程架構討論`
> * `MSSQL資料庫與欄位建置`
> * `EasyUI清單建置CRUD`
> * `EasyUI表單建置CRUD`
> * `圖表chart.js建置`

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/13.gif) 

<!-- ![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/2020/%E7%B3%BB%E7%B5%B1%E6%94%AF%E6%8F%B4%E9%83%A820191105A.jpg) -->

<!-- ![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/2020/%E7%B3%BB%E7%B5%B1%E6%94%AF%E6%8F%B4%E9%83%A820191105S.jpg) -->

<!-- ![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/2020/%E7%B3%BB%E7%B5%B1%E6%94%AF%E6%8F%B4%E9%83%A820191105.jpg) -->

**20200107 更新需求單部分UIUX優化 v2**

  ![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/14.gif)

**20200107 新增需求單備註多對多留言板 v3**

  ![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/15.jpg)



</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>

#### Sastty-商品包裝問券頁面 實作    [▲BackToTop](#top) <a id="P15"></a>

使用技術 : ASP.NET Page / CSS / Javascript / JQ / TSQL

> **需求**: test
>
> * [(Sastty)官網](https://www.sastty.com.tw/survey/)
> * [實作包含]
> * `商品說明書上QrCode , 拍了之後連到官網問券`
> * `會員登錄`
> * `填寫問券`
> * `送出問券 送 100元購物金`
> * `手機板頁面`
> * `v2 階層權限Css樣式覆蓋、Js手機板畫面增加Br`

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/16.gif)

**RWD手機版**

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/17.gif)

</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>

#### EClife ERP後台 報表統計匯出 / SQL預存排程 實作  [▲BackToTop](#top) <a id="P14"></a>

使用技術 : TSQL\C#.NET

> **需求**: 購物車未結帳資料建置，用以數據發展部測試弱AI廣告投放演算與行銷部KPI報表統計。
>
> * [實作包含]
> * `KPI指標研擬`
> * `TSQL操作`
> * `預存與jenkins管理`
> * `轉excel報表`

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/18.jpg)

<!-- <details>
  <summany>展開查看程式碼</summany>

```
USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[Upd_ShoppingCartUncheckedGoods_Report]    Script Date: 2019/10/30 上午 09:26:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        育誠
-- Create date: 2019-10-29
-- Description:    EC_購物車未結帳商品報表 ShoppingCartUncheckedGoods_Report
-- =============================================
ALTER PROCEDURE [dbo].[Upd_ShoppingCartUncheckedGoods_Report]

AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;


-- 參考:https://dotblogs.com.tw/daniel/2018/01/19/174836 temp table

/** #Temp_Cart_Tracking 購物車狀態暫存表 **/

SELECT CONVERT(CHAR, postdate, 111) AS 'postdate', 
       prodcno, 
       memberno, 
       amount
INTO #Temp_Cart_Tracking
FROM LS3C_V2_2005.dbo.Cart_Tracking
WHERE ISNUMERIC(memberno) = 1;

/** #Temp_mListNO 購物車報表暫存表 **/

SELECT *
INTO #Temp_mListNO
FROM CRM.dbo.mailSend_REC
WHERE mListNO = 'W1D';

/** EC_購物車未結帳商品報表 **/

INSERT INTO ShoppingCartUncheckedGoods_Report (
"編號",
"報表資料產製日期",
"發送日期",
"會員姓名",
"Email",
"手機號碼",
"會員身分",
"是否訂閱電子報",
"接收狀態",
"開啟數",
"點擊數",
"產品CNO",
"良興代碼",
"產品品名",
"金額",
"成本價",
"數量",
"生日"
)
SELECT 
       a.mRID AS '編號',
       CONVERT (VARCHAR,GETDATE(),111) AS '報表資料產製日期', 
       CONVERT (VARCHAR,b.sendDate,111) AS '發送日期', 
       a.memName AS '會員姓名', 
       a.memberEmail AS 'Email', 
       m.Mobil AS '手機號碼', 
       (CASE
            WHEN m.MemberLevel = '0'
            THEN '網路會員'
            WHEN m.MemberLevel = '1'
            THEN '門市會員'
            WHEN m.MemberLevel = '8'
            THEN '金賺會員'
            ELSE m.MemberLevel
        END) AS '會員身分', 
       (CASE
            WHEN m.AcceptEpaper = '0'
            THEN '否'
            WHEN m.AcceptEpaper = '1'
            THEN '是'
            ELSE m.MemberLevel
        END) AS '是否訂閱電子報', 
       (CASE
            WHEN CONVERT(VARCHAR, a.[status]) = '1'
            THEN '成功'
            ELSE '失敗'
        END) AS '接收狀態', 
       a.opened AS '開起數', 
       a.clicked AS '點擊數', 
       ct.prodcno AS '產品CNO', 
       L.ProductNo AS '良興代碼', 
       L.ProductName AS '產品品名', 
       L.SpicalPrice AS '金額', 
       L.Cost AS '成本價', 
       ct.amount AS '數量'
       ,
       --, '先記著' as '是否訂閱電子報' 
       CONVERT (VARCHAR,m.Birthday,111) AS '生日'
       --, '先記著' as '紅利點數'
       --, '先記著' as '購物金'
       --, '先記著' as '最後消費門市'
FROM CRM.dbo.mailSend_result AS a
JOIN #Temp_mListNO AS b 
ON a.mRID = b.ID
JOIN LS3C_V2_2005.dbo.member AS m 
ON a.memberNO = m.cno
LEFT JOIN #Temp_Cart_Tracking AS ct 
ON a.memberNO = ct.memberno
AND ct.postdate = DATEADD(day, -1, b.sendDate)
LEFT JOIN LS3C_V2_2005.dbo.list AS L 
ON L.cno = ct.prodcno
WHERE b.sendDate = CONVERT(VARCHAR, DATEADD(day, -1, GETDATE()), 111) 
--and a.mRID = '12345'
ORDER BY 發送日期,會員姓名;


DROP TABLE #Temp_Cart_Tracking;
DROP TABLE #Temp_mListNO;

END
```
</details> -->

</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>

#### 全國電子 AP程式\_API串接 / 後台CMS管理系統顯示 實作 [▲BackToTop](#top) <a id="P13"></a>

使用技術 : Winform , .net , API , JSON , SSMS , VBA , VB.NET

> **需求**:串接AP全國電子拋回之API並導入後台顯示
>
> * [實作包含]
> * `Winform元件使用`
> * `JSON物件使用`
> * `MS-SQL應用`
> * `後台AJaX&VB.NET應用`

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/19.jpg) 
![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/20.jpg) 
![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/21.jpg)

</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>

#### MSI MPA首頁Menu改版 實作 [▲BackToTop](#top) <a id="P12"></a>

使用技術 : MPA /jQ /AJaX / ASP.NET

> **需求**: 動態Menu改版
>
> * [(MSI)官網](https://twstore.msi.com/)
> * [實作包含]
> * `TSQL 查詢`
> * `AJax 拋資料`

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/22.jpg)

</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>

#### EPSON MPA前台動態Megamenu / 後台banner上稿CMS管理系統 實作 [▲BackToTop](#top) <a id="P11"></a>

使用技術 : MPA /jQ /AJaX / EasyUI / ASP.NET

> **需求**: 動態Menu改版
>
> * [(EPSON)官網](https://myepson.epson.com.tw/)
> * [(回收)官網](https://w3.epson.com.tw/recycle/)
> * \[實作包含\]
> * `CISCO VPN串接`
> * `TSQL 查詢`
> * `AJax 拋資料


![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/23.jpg) 
![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/24.jpg)

</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>


#### Sastty、aimedia MPA前台春聯廣告頁 / 後台CMS管理系統 實作 [▲BackToTop](#top) <a id="P10"></a>

使用技術 : jQ DOM / jQ library EasyUI / MPA / AJaX / ASP.NET ...

> **需求**: 根據行銷企劃部需求提供應用...。
>
> * [(sastty)官網](https://www.sastty.com.tw/)
> * [(aimedia)官網](https://www.aimedia.com.tw/)
> * [實作包含]
> * `前台動態頁面`
> * `後台上稿功能`

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/25.jpg)

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/26.jpg)

</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>

#### EClife MS-SQL KPI報表重構 實作 [▲BackToTop](#top) <a id="P9"></a>

使用技術 : TSQL\C#.NET

> **需求**: 因GA數據沒辦法API載回所以重構TSQL，利於KPI報表呈現。
>
> * [(Lab101)官網](https://www.lab101.asia/News/1909040001/)
> * [實作包含]
> * `KPI指標研擬`
> * `TSQL操作`
> * `轉excel`


<!-- <details>

```
USE LS3C_V2_2005; --使用DB
GO

--迴圈設定
DECLARE @RunNum INT, --執行次數
@NowNum INT, --目前次數
@DatebyLiu DATETIME; --當日時間

SET @RunNum = 1; --執行至幾天前
SET @NowNum = 1; --迴圈初始值

--SET @YorDbyLiu = DateDiff( dd , ListOrder_Main.PostDate ,getdate()) ;
--當時間首購人數

WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT COUNT(*) AS 當時間首購人數, 
               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS 日期
        FROM
        (
            SELECT ListOrder_Main.memberno, 
                   m.Name
            FROM ListOrder_Main
                 LEFT JOIN Member m ON m.cno = ListOrder_Main.MemberNo
            WHERE m.Name IS NOT NULL
                  AND (SerialNo LIKE '0%'
                       OR SerialNo LIKE '5%')
                  AND DATEDIFF(dd, ListOrder_Main.PostDate, GETDATE()) = @DatebyLiu
            GROUP BY ListOrder_Main.memberno, 
                     m.Name, 
                     ListOrder_Main.[Status]
            HAVING COUNT(ListOrder_Main.memberno) = 1
                   AND ListOrder_Main.[Status] IN(1, 2, 3)
            --當時間每筆首購訂單明細
            --ORDER BY ListOrder_Main.memberno;
        ) AS DAYBUYCOUNT;
        SET @NowNum = @NowNum + 1;
    END;


--迴圈設定
--DECLARE @RunNum INT, --執行次數
--@NowNum INT, --目前次數
--@DatebyLiu DATETIME; --當日時間

--SET @RunNum = 74; --執行至幾天前
SET @NowNum = 1; --迴圈初始值
--當時間訂單筆數
WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT COUNT(cno) AS 當時間訂單筆數, 
               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS 日期
        FROM ListOrder_Main
        WHERE Site = 'eclife'
              AND (SerialNo LIKE '0%'
                   OR SerialNo LIKE '5%')
              AND DATEDIFF(dd, ListOrder_Main.PostDate, GETDATE()) = @DatebyLiu;
        SET @NowNum = @NowNum + 1;
    END;


--迴圈設定
--DECLARE @RunNum INT, --執行次數
--@NowNum INT, --目前次數
--@DatebyLiu DATETIME; --當日時間

--SET @RunNum = 74; --執行至幾天前
SET @NowNum = 1; --迴圈初始值
--當時間查詢當下發送總數減掉回購訂單數 = 放入購物車”當日”未結帳人數
WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT(mp.success - mp.orderCount) AS 當時間放入購物車當日未結帳人數, 
              CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS 日期
        FROM CRM.dbo.mailSend_REPORT mp WITH(NOLOCK)
             JOIN CRM.dbo.mailSend_REC mr WITH(NOLOCK) ON mp.mRID = mr.ID
             JOIN CRM.dbo.mailCategory_list mcl WITH(NOLOCK) ON mr.mListNO = mcl.NO
        WHERE mListNO LIKE '%W1D%'
              AND DATEDIFF(dd, sendDate, GETDATE()) = @DatebyLiu;
        SET @NowNum = @NowNum + 1;
    END;


--迴圈設定
--DECLARE @RunNum INT, --執行次數
--@NowNum INT, --目前次數
--@DatebyLiu DATETIME; --當日時間

--SET @RunNum = 74; --執行至幾天前
SET @NowNum = 1; --迴圈初始值
----當時間刷卡失敗筆數
WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT COUNT(cno) AS 當時間刷卡失敗筆數, 
               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS 日期
        FROM ListOrder_Main
        WHERE Site = 'eclife'
              AND DATEDIFF(dd, ListOrder_Main.PostDate, GETDATE()) = @DatebyLiu
              AND STATUS = 8
              AND TradeMode = 0
              AND memberno != '3003490'
              AND memberno != '3007210';
        SET @NowNum = @NowNum + 1;
    END;
```

</details> -->
 

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/27.jpg)

</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>

#### Lab101 MPA文章前台頁面 / 後台CMS管理系統 實作 [▲BackToTop](#top) <a id="P8"></a>

使用技術 : UIUX&CSS&EasyUI&ASP.NET&YoutubeAPI

> **需求**: 新增行銷露出頁面。
>
> * [(Lab101)官網](https://www.lab101.asia/News/1909040001/)
> * [實作包含]
> * `後台EasyUI行銷頁面類別與文章新增`
> * `前台rewrite分類參數進入類別頁`
> * `後台資料前台樣式套版顯示`

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/28.jpg)
![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/29.jpg)
![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/30.jpg)

</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>

#### Lab101 Lab101 MPA產品頁需求更改 實作 [▲BackToTop](#top) <a id="P7"></a>

使用技術 : JQ&CSS&UIUX

> **需求**: 訊息不明顯，購物車功能常駐頁面。
>
> * [(Lab101)官網](https://www.lab101.asia/)
> * [實作包含]
> * `UIUX改善`
> * `JQ購物車功能改善`

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/31.jpg)

</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>

#### Lab101 MPA金流購物車付款頁面 互動 實作 [▲BackToTop](./#top) <a id="P6"></a>

使用技術 :

> **需求**: 選擇宅配時顯示地址表單，選擇超商付款時顯示超商API串接內容。並將資料傳回資料庫。
>
> * [(點我進入官方網站)。](https://www.lab101.asia/)
> * [實作包含]
> * `Webforms repeater`
> * `webforms contentPlaceHolder`
> * `並在多個頁面內容實作 jQ、js互動效果`
> * `資料驗證`
> * `回傳錯誤訊息`
> * `回傳成功訊息並導入資料庫與跳轉頁面`
> * `物流API串接`

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/32.jpg)
![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/33.jpg)

</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>

#### Sastty MPA抽獎活動登入頁面 實作 [▲BackToTop](#top) <a id="P5"></a>

使用技術 : jQ&Js&Webforms

> **需求**: 基本資料填寫，驗證後存庫。
>
> * [(點我進入官方網站)抽獎功能官方網站預計8月底上線。](https://www.sastty.com.tw/)
> * [實作包含]
> * `抽獎頁面驗證登入session`
> * `前端頁面套版`
> * `資料傳遞至後端處頁面`
> * `資料驗證`
> * `回傳錯誤訊息`
> * `回傳成功訊息並導入明細抽獎列表頁面`
> * `登入抽獎頁面表單`
> * `後端驗證是否重複序號`
> * `有重複錯誤提示`
> * `無重複存入資料庫並轉回抽獎列表頁面`

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/34.jpg)
![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/35.jpg)
![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/36.jpg)

</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>

#### EasyUI CMS後台系統 功能維護 [▲BackToTop](#top) <a id="P4"></a>

使用技術 : EasyUI.js

> **需求**: 查詢驗證Bug、新增功能移植。
>
> * [實作包含]
> * `利用EasyUI功能查詢文件位置`
> * `單步偵錯中斷點查詢資訊流傳遞的bug`
> * `檢測出SQLcommand欄位名稱重複造成例外`
> * `接著利用EasyUI功能增加新功能頁面`

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/37.jpg)

</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>

#### AP API/FTP 排程API載入資料庫 實作 [▲BackToTop](#top) <a id="P3"></a>

使用技術 : Windowsform &gt;

> **需求**: 串接API載入FTP檔案，轉存資料庫，並設定自動排程。
>
>   * [實作包含]
>   * `Windowsform簡易排版` 
>   * `Windowsform後台程式撰寫` 
>   * `串接API` 
>   * `資轉存資料庫並顯示` 
>   * `時間處理函式` 
>   * `時間處理函示導入排程` 
>   * `ShowDoc php Markdown文件歸檔`

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/38.jpg)

</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>

#### Sastty CMS後台抽獎功能清單轉存 實作 [▲BackToTop](#top) <a id="P2"></a>

使用技術 : EasyUI.js

> **需求**: 後台顯示前台抽獎頁面登入之內容，並有按鈕可以將清單轉出excel。
>
> * [實作包含]
> * `EasyUI 階層設定`
> * `EasyUI 基本顯示`
> * `EasyUI 分頁`
> * `EasyUI 日期查詢顯示`
> * `EasyUI 匯出清單excel`

![](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/NewIMG/39.jpg)
<!-- ![image](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/20200117/%E6%9C%AA%E5%91%BD%E5%90%8Dssssasdasdadadadadss.png) -->

</br></br></br></br></br></br></br></br></br></br>     
</br></br></br></br></br></br></br></br></br></br>

<!-- #### EClife MSSQL KPI報表 實作 [▲BackToTop](#top) <a id="P1"></a>

使用技術 : TSql&Sqlcompletes

> **需求**: 依據數據發展部與企劃部實作KPI報表TSQL。
>
> * [實作包含]
> * `定義報表欄位內容`
> * `TSQL`
> * `Excel`

![image](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/%E6%93%B7%E5%8F%96.PNG)

<details>
<summany>展開查看程式碼</summany>

```text
USE LS3C_V2_2005
GO

--筆數 = 首購人數
SELECT l.memberno, m.Name
FROM ListOrder_Main l
LEFT JOIN Member m ON m.cno = l.MemberNo
WHERE m.Name IS NOT NULL
AND DateDiff(dd,l.PostDate,getdate())=1
GROUP BY l.memberno, m.Name , l.[Status]
HAVING COUNT(l.memberno) = 1
AND l.[Status] = '2'
ORDER BY l.memberno 
GO

--昨日訂單筆數
select COUNT(cno) as 昨日訂單筆數
from ListOrder_Main
where Site = 'eclife' 
AND ( SerialNo like '0%'  OR SerialNo LIKE '5%' )
AND DateDiff(dd,PostDate,getdate())=1
GO

--查詢當下發送總數減掉回購訂單數 = 放入購物車”當日”未結帳人數
SELECT mr.ID mRID,
mr.mListNO + '-' + mcl.decription mListNO, 
CONVERT(VARCHAR(10),mr.sendDate,111) sendDate,
mp.totalMailCount, mp.success as 發送總數, mp.fail, mp.opened, mp.clicked,
mp.successRate, mp.openedRate, mp.clickedRate,
mp.orderCount as 訂單數, mp.orderTotalMoney, mp.bagRate
FROM CRM.dbo.mailSend_REPORT mp WITH (NOLOCK)
JOIN CRM.dbo.mailSend_REC mr WITH (NOLOCK)
ON mp.mRID=mr.ID
JOIN CRM.dbo.mailCategory_list mcl WITH (NOLOCK)
ON mr.mListNO=mcl.NO
where  mListNO like '%W1D%'
and DateDiff(dd,sendDate,getdate())=1
GO

-- 昨日刷卡失敗筆數
select COUNT(cno) as 昨日刷卡失敗筆數
from ListOrder_Main
where Site = 'eclife' 
and DateDiff(dd,PostDate,getdate())=1
and Status=8
and TradeMode=0
and memberno != '3003490'
and memberno != '3007210'
GO
```

&lt;/pre&gt;&lt;/details&gt; ![image](https://github.com/johch3n611u/EC_Web-AP_Developer/blob/master/img/%E6%93%B7%E5%8F%96S.PNG)
</details> -->


#### test    [▲BackToTop](#top) <a id="test"></a>

使用技術 : test

> **需求**: test
>
> * [(TEST)官網]()
> * [實作包含]
> * `test`

![](https://github.com/johch3n611u/EC_Web-AP_Developer/tree/326317fa91c9494aafef8b1091334a6839aee16e/test)

https://johch3n611u.gitbook.io/-1/
