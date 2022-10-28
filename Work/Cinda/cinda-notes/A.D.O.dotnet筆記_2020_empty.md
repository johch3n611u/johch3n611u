# 參考

<https://zh.wikipedia.org/wiki/Microsoft_Data_Access_Components>

<https://zh.wikipedia.org/wiki/ADO.NET>

<https://docs.microsoft.com/zh-tw/dotnet/framework/data/adonet/ado-net-overview>

---

## [Wiki ADO.NET](https://zh.wikipedia.org/wiki/ADO.NET#%E7%99%BC%E5%B1%95%E7%B7%A3%E8%B5%B7)

### IDbConnection

負責與資料庫的連線管理，包含連線字串（connection string），連線的開關，資料庫交易的啟始與連線錯誤的處理，所有的ADO.NET資料提供者都要實作此介面。

* Open()/Close()：開啟與關閉資料庫連線。

* BeginTransaction()：啟動資料庫交易，並回傳一個IDbTransaction物件，以控制交易的結果。

* Connection String <https://docs.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring?view=dotnet-plat-ext-3.1>

---

### IDbCommand

負責執行資料庫指令（在大多數的案例中都是SQL指令），並傳回由資料庫中擷取的結果集，或是執行不回傳結果集的資料庫指令。

* ExecuteNonQuery()：執行不回傳結果集的資料庫指令，像是INSERT、UPDATE與DELETE指令，返回值為該命令所影響的行數。 對於其他所有類型的語句，返回值 為 -1。

* ExecuteScalar()：執行指令並回傳第一列第一行中的值（object類型）。當沒有資料時，ExcuteScalar方法返回System.DBNull。

* ExecuteReader()：執行指令並回傳IDataReader物件，以讀取資料集中的資料。

* BeginExecuteNonQuery：開始執行非同步查詢

* EndExecuteNonQuery: 結束執行非同步查詢

---

### IDataParameter

負責裝載資料庫指令所需要的參數資料，在使用參數化查詢時會經常使用。 對於不同的資料來源來說，預留位置不同。SQLServer資料來源用@parametername格式來命名參數，OleDb以及Odbc資料來源均用問號（?）來標識參數位置，而Oracle則以:parmname格式使用命名參數。

---

### IDbTransaction

負責裝載資料庫交易所需的控制物件，以執行交易的認可（commit）或復原（rollback）的工作。

* Commit()：認可資料庫交易。
* Rollback()：復原資料庫交易。

---

### Entities.SaveChanges() VS tran.Commit()

多次存資料，避免一次異常造成其餘正常多存一次則，先 EF SC 在 TS Cmt ，錯誤則 TS Rb ...

<https://www.cnblogs.com/OpenCoder/p/9799586.html>

<https://stackoverflow.com/questions/50844828/ef-db-savechanges-vs-dbtransaction-commit>

---

### IDbDataAdapter

負責將來自於IDbCommand執行取得的結果集，裝載到離線型資料集（DataSet）或是離線型資料表（DataTable）中。

* Fill()：將資料填入離線型資料物件。
* Update()：將變更過的離線型資料物件中的資料寫回資料庫。

---

### IDataReader

建立一個只可向前讀取游標（forward-only）的資料讀取器工具，以逐列讀取方式存取資料，IDbDataAdapter內部也是由它來讀取資料。

* Read()：第一次呼叫Read()方法取得第一行資料，並將游標指向下一行資料。當再次呼叫該方法時候，將讀取下一行資料。當檢測到不再有資料行時，Read()方法將返回false。

---

### IDataRecord

在IDataReader讀取資料後實際裝載資料列的物件，提供方法來讀取資料行中的資料，以及轉換成.NET Framework原生型別的工具。

* GetOrdinal()：取得指定資料行的欄位索引值。
* IsDBNull()：判斷指定欄位的資料是否為NULL值。

---

### 總覽

對每種Data Provider，ADO.NET要實現下述物件結構：

* Connection 物件提供與資料來源的連接。
* Command物件使您能夠存取用於返回資料、修改資料、執行儲存程序以及傳送或檢索參數資訊的資料庫命令。
* DataReader 物件從資料來源中提供快速的，唯讀的資料流。
* DataAdapter 物件提供連接 DataSet 物件和資料來源的橋樑。* DataAdapter 使用 Command 物件在資料來源中執行 SQL 命令，以便將資料載入到 DataSet 中，並使對 DataSet 中資料的更改與資料來源保持一致。
* Parameter 物件用於參數化查詢。例如，對於SQL Server資料來源：

```C#
strSQL = "SELECT * FROM users WHERE Name = @Name and Password = @Password";
SqlParamter[] paras = new SqlParamter[]{//参数数组
     new SqlParamter("@Name",SqlDBType.Varchar,50)
     new SqlParamter("@Password",SqlDBType.Varchar,50)};
paras[0].value = userName;//绑定用户名
paras[1].value = password;//绑定用户密码
```

* ConnectionStringBuilder：提供一種用於建立和管理由 Connection 物件使用的連接字串的內容的簡單方法。 所有 ConnectionStringBuilder 物件的基礎類別均為 DbConnectionStringBuilder 類。

<https://docs.microsoft.com/zh-tw/dotnet/framework/data/adonet/connection-string-builders>

```C#
private static void BuildConnectionString(string dataSource,
    string userName, string userPassword)
{
    // Retrieve the partial connection string named databaseConnection
    // from the application's app.config or web.config file.
    ConnectionStringSettings settings =
        ConfigurationManager.ConnectionStrings["partialConnectString"];

    if (null != settings)
    {
        // Retrieve the partial connection string.
        string connectString = settings.ConnectionString;
        Console.WriteLine("Original: {0}", connectString);

        // Create a new SqlConnectionStringBuilder based on the
        // partial connection string retrieved from the config file.
        SqlConnectionStringBuilder builder =
            new SqlConnectionStringBuilder(connectString);

        // Supply the additional values.
        builder.DataSource = dataSource;
        builder.UserID = userName;
        builder.Password = userPassword;
        Console.WriteLine("Modified: {0}", builder.ConnectionString);
    }
}
```

* CommandBuilder ：自動生成 DataAdapter 的命令內容或從儲存程序中衍生參數資訊，並填充 Command 物件的 Parameters 集合。 所有 CommandBuilder 物件的基礎類別均為 DbCommandBuilder 類。

<https://iveswind.pixnet.net/blog/post/4239049>

---

### Connection Pool

就是為了要節省開啟、關閉資料庫連線時所造成大量的資源與時間耗損所設計出來的機制。

<http://peggg327.blogspot.com/2014/11/connection-pool.html>

<http://godleon.blogspot.com/2008/11/adonet-connection-pooling_7793.html>

<https://docs.microsoft.com/zh-tw/dotnet/framework/data/adonet/sql-server-connection-pooling>

<https://www.huanlintalk.com/2012/05/net-connection-pool.html>

---

### 離線資料模型

離線資料模型是微軟為了改良ADO在網路應用程式中的缺陷所設計的，同時它也是COM+中，IMDB技術的設計概念的實作品，但它並沒有完整的IMDB功能，像是交易處理（transaction processing），但它仍不失為一個能在離線狀態下處理資料的好幫手，它也可以透過連線資料來源物件，支援將離線資料存回資料庫的能力。離線資料模型由下列物件組成

System.Data.Entity

System.Data

<https://docs.microsoft.com/zh-tw/dotnet/api/system.data.dataset?view=netcore-3.1>

### DataSet

離線型資料模型的核心之一，可將它當成一個離線型的資料庫，它可以內含許多個DataTable，並且利用關聯與限制方式來設定資料的完整性，它本身也提供了可以和XML互動作業的支援。

* ReadXml()/WriteXml()：以DataSet的結構讀寫XML。
* ReadXmlSchema()/WriteXmlSchema()：以DataSet的結構讀寫XML Schema。
* GetXml()/GetXmlSchema()：取得DataSet內容的XML或XML Schema。
* Merge()：合併兩個DataSet。
* Load()：自IDataReader載入資料到DataSet。
* AcceptChanges()：將修改過的資料列的修改旗標改為Unchanged。
* GetChanges()：將修改過的資料列以DataRow陣列方式傳回。
* RejectChanges()：復原所有資料的修改。

### DataTable

離線型資料模型的核心之一，可將它當成一個離線型的資料表，是儲存資料的收納器。

* Copy()：將DataTable複製出一個副本，包含結構與資料。
* Merge()：將兩個DataTable合併。
* Select()：以指定的特殊查詢語法，傳回符合條件的DataRow陣列。
* Compute()：以指定的彙總語法，傳回彙總的結果。
* GetErrors()：傳回有錯誤的DataRow陣列。
* HasErrors：判斷DataTable中的DataRow有沒有含有錯誤的DataRow。
* DataRow，表示表格中的資料列，與資料欄組合成資料儲存的單元。
* IsNull()：判斷指定的欄位是否為NULL值。
* ItemArray：將DataRow中的資料轉換成陣列。
* DataColumn，表示表格中的欄位。
* DataView，展示資料的輔助元件，類似於資料庫中的檢視表，並可設定過濾條件與排序條件。
* Filter：設定DataView的過濾條件。
* Sort：設定DataView的排序條件。
* ToTable()：將套用過濾與排序後的內容轉換為DataTable物件。
* DataRelation，可在DataTable之間設定欄位間的關聯。
* Constraint，設定欄位的條件約束，例如ForeignKeyConstraint為外部鍵限制，而UniqueConstraint則確保了欄位中的值都是唯一的。

DataSet和DataTable除了資料庫的處理以外，也經常被用來管理應用程式中的資料，並且由於它可以儲存在XML中的特性，也讓它可以用來儲存需要儲存的應用程式資訊。DataSet中可包含DataRelation物件，用於建構表之間的約束關係。

<https://www.cnblogs.com/tangge/p/4528102.html>