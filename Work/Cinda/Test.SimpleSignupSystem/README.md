
# 活動報名系統

![alt](/Test.SimpleSignupSystem/assets/img/tempgif.gif)

## 20200830 錯誤處理補充

<https://dotblogs.com.tw/joysdw12/2013/05/20/104561>

<https://dotblogs.com.tw/b7076476/2012/10/25/78869>

<https://dotblogs.com.tw/jinx/2014/04/10/144683>

## really code first

<https://dotblogs.com.tw/supershowwei/2016/04/11/000015>

前一個專案使用 code first by database ，此次專案要真正的使用 code first，並且搭配相對應的標籤與使用 cmd 。

---

## 使用技術

* MVC5 / Code first / Razor / JS / SSMS

## 功能

1. 開啟一個Asp.net MVC專案 :heavy_check_mark:
2. 寫一個新達2020/9/25活動報名系統(新增、修改、刪除) :heavy_check_mark:
   * 需要欄位(手機、姓名、email 、活動項目(可多選)( AM10:00 ~ AM11:00排球、AM11:00 ~ PM12:00羽球、PM15:00 ~ PM16:00自行車 …) :heavy_check_mark:
   * 活動項目，須從資料庫來 :heavy_check_mark:
   * 所有欄位都必填(需檢查) :heavy_check_mark:
   * 相同的手機，只能報名一次 :heavy_check_mark:
   * 報名完成，需要顯示 報名詳細資訊 :heavy_check_mark:
   * 列表 所有活動名稱、報名人數 及 詳細頁 報名人員、姓名、手機、報名時間 :heavy_check_mark:
   * 刪除需有確認提示視窗 :heavy_check_mark:
   * 列表根據三個欄位(姓名/手機(模糊查詢)、活動項目(含全部)(下拉) 進行查詢 :heavy_check_mark:
   * 查詢出來的列表，需有會 手機、姓名、email 、活動項目(用逗點區隔),報名時間  欄位 :heavy_check_mark:
   * 相關資料表 :heavy_check_mark:
     * tblSignup報名資訊
       * cMobile,varchar(10)，手機 ,PK
       * cName,nvarchar(20)，姓名
       * cEmail,nvarchar(50)，Email
       * cCreateDT,datetime,報名時間(新增當下)
     * tblSignupItem 報名項目
       * cMobile varchar(10), 手機，,PK
       * cItemID,int, 項目ID，PK
     * tblActiveItem 活動項目
       * cItemID,int,pk，項目ID
       * cItemName, 活動項目名稱
       * cActiveDt,活動時間
   * 參考畫面 根據上面提示，自行發想
3. 與資料庫聯繫，需使用EF codefirst :heavy_check_mark:
4. 建立專案，須將專案加入git版控 :heavy_check_mark:
5. 須將網站，部屬至本機端IIS上 :heavy_check_mark:

## 重點

如果求快可以把 畫面跟資料處理都寫在同一頁 DTO 也寫在同一頁。

## 步驟

1. 首先在專案內想建構 DAL 的地方。
2. 右鍵新增項目 -> 資料 -> ADO.NET 實體資料模型。
3. 選擇空的 Code First 模型。
   * 此時 IDE 會幫忙添加以下資料
     * packages.config
     * SimplesSignupSystem.csproj
     * Web.config ★★ (連線資訊 connectionStrings) 等確定實體後才來更改連線方式 SQL Service 登入 ★★
     * SignupDB.cs ★★ (DbContext 設定檔) 處理連線資訊與關聯實體 DTO ★★
4. 編輯 DTO 資料表 Class 此處分為實體用途與其餘用途使用的 DTO。
   > [EF 中,當某一個屬性視為 primary key 時，如果該屬性類別為 int，則生成資料庫時會自動變成自加序號，那如果不是 int 而是 Guid，那就必須你自己給值，或自己設定為自加序號。](https://charleslin74.pixnet.net/blog/post/458313893-%5Bc%23%5D-entity-framework%E4%B8%AD%E7%9A%84databasegenerated%E5%B1%AC%E6%80%A7)
   * PK 屬性 Guid 自加序號 [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
   * PK 屬性 int 不自加序號 [DatabaseGenerated(DatabaseGeneratedOption.None)]
   * 此標籤代表 參數是透過計算而來 DB 不會實際儲存此值 [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
5. [建立完 DTO 與 DBContext 後，重點來了 ★★ 執行 db first cmd ★★。](https://dotblogs.com.tw/supershowwei/2016/04/11/000015)
   * 套件管理器主控台輸入指令
     * `Enable-Migrations`。 此指令會幫忙驗證模型的結構
     * `Add-Migration Initial` 此指令產生實際更新資料庫結構的程式碼 ( 記錄著上下版本的差異 )
     * `Update-Database -Verbose` 此指令使用 SQL Service 真的建立資料表
     * `-Verbose` 目的是要把詳細訊息顯示出來。 ( 但為求確保不會另用此方式真的建立而是用以下方式 )
     * `Update-Database -Script -Verbose` 此指令不會直接對資料庫操作而是產生更新資料庫結構的指令碼。
6. 建立測試資料 [Initializer DropCreateDatabaseIfModelChanges](https://dotblogs.com.tw/wasichris/2014/08/23/146339) 此類別建立後 `Update-Database -Script -Verbose` 指令也會同時產生初始資料所需 SQL
   * DropCreateDatabaseIfModelChanges
   * Web.config entityFramework ( 這裡必須確保新增 configSections 避免報錯 )
7. 但事實上似乎必須藉由系統建立資料庫才會真的把初始化資料寫入，因為產生的 Script 是不包含這段的。
8. 連線字串補 `AttachDbFilename=|DataDirectory|\CodeFirstDb.mdf` 資料庫將被建置於App_Data資料夾中。
9. 使用 SSMS 發現 DB 缺少了一些地方，剛好測試異動後要怎更新資料結構。
    * 更新完 DTO 後，`Enable-Migrations -Force` 如有多個則再加上 `–ContextTypeName DbContext`
    * `Update-Database` 更新實體即可，`Add-Migration AddAddress` 能夠為此次異動記錄著上下版本的差異
    * Configuration.cs `AutomaticMigrationsEnabled = true;` 即可不用 `Add-Migration AddAddress`
    * 連線字串這裡很多坑... 而且不知道為什麼有些 死雞馬 並沒有真的增加上去例如遞增值，最後變成要刪除 mdf 在重製才成功。
10. 接著就是修改 連線字串 不從 mdf 直接連而是 SQL Server 帳密連線的設定。
    * 在更改前先記錄 dbfirst 產生第一次 資料庫結構時的連線字串內容 first dbfirst
    * 設定詳細步驟請參考 大涼奶專案內的 SQL Server 管理.md
11. 資料必須全砍掉然後重頭下一次指令，並且重啟db，不然會卡。
12. [補傳遞驗證/提示資料 Code。](https://social.msdn.microsoft.com/Forums/zh-TW/0b84b707-342b-4c9e-b576-399d77d079a5/mvc229142030923559object-3900622411366812556321040routevalues?forum=236)
13. [理解 LINQ Group & Select。](https://stackoverflow.com/questions/10637760/linq-group-by-and-select-collection)
14. [MVC 資料容器  ViewData、ViewBag、ViewModel、TempData。](https://dotblogs.com.tw/jasonyah/2013/04/18/explain-viewbag-viewdata)

---

* first dbfirst

```XML
  <connectionStrings>
    <add name="SignupDB" connectionString="data source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CodeFirstDb.mdf;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
  </connectionStrings>
```

* Web.config entityFramework

```XML
<entityFramework>
    <contexts>
      <context type="SimpleSignupSystem.DAL.SignupDB, SimpleSignupSystem">
        <databaseInitializer type="SimpleSignupSystem.DAL.DefaultInitializer, SimpleSignupSystem" />
      </context>
    </contexts>
    <defaultConnectionFactory type="System.Data.Entity.Infrastructure.LocalDbConnectionFactory, EntityFramework">
      <parameters>
        <parameter value="v11.0" />
      </parameters>
    </defaultConnectionFactory>
    <providers>
      <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
    </providers>
</entityFramework>
```

* packages.config

```XML
<package id="EntityFramework" version="6.2.0" targetFramework="net472" />
<package id="EntityFramework.zh-Hant" version="6.2.0" targetFramework="net472" />
```

* SimplesSignupSystem.csproj

```XML
<Reference Include="EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, processorArchitecture=MSIL">
  <HintPath>..\packages\EntityFramework.6.2.0\lib\net45\EntityFramework.dll</HintPath>
</Reference>
<Reference Include="EntityFramework.SqlServer, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, processorArchitecture=MSIL">
  <HintPath>..\packages\EntityFramework.6.2.0\lib\net45\EntityFramework.SqlServer.dll</HintPath>
</Reference>
```

* Web.config

```XML
<configSections>
    <section name="entityFramework" type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false" />
</configSections>

<configuration>
   <connectionStrings>
      <add name="SignupDB" connectionString="data source=(LocalDb)\MSSQLLocalDB;initial catalog=SimpleSignupSystem.DAL.SignupDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
   </connectionStrings>
</configuration>
```

## 參考

<https://dotblogs.com.tw/supershowwei/2016/04/11/000015>

<https://charleslin74.pixnet.net/blog/post/458313893-%5Bc%23%5D-entity-framework%E4%B8%AD%E7%9A%84databasegenerated%E5%B1%AC%E6%80%A7>

<https://www.google.com/search?q=DTO&oq=DTO&aqs=chrome.0.69i59j0l7.2231j0j4&sourceid=chrome&ie=UTF-8>

<https://stackoverflow.com/questions/31903342/error-2019-member-mapping-specified-is-not-valid-using-entity-framework-code-fi>

<https://hant-kb.kutu66.com/wpf/post_1510868>

<https://www.google.com/search?q=HashSet&oq=HashSet&aqs=chrome..69i57j0l7.111j0j4&sourceid=chrome&ie=UTF-8>

<https://www.cnblogs.com/firstdream/archive/2012/04/13/2445582.html>

<https://dotblogs.com.tw/wasichris/2014/08/23/146339>

<https://entityframework.net/zh-CN/knowledge-base/40572912/>

<https://stackoverflow.com/questions/40572912/the-type-initializer-for-system-data-entity-migrations-dbmigrationsconfiguratio>

<https://dotblogs.com.tw/skychang/2013/05/29/105057>

<https://social.msdn.microsoft.com/Forums/zh-TW/0b84b707-342b-4c9e-b576-399d77d079a5/mvc229142030923559object-3900622411366812556321040routevalues?forum=236>
