# 參考

## [Wiki EF](https://zh.wikipedia.org/zh-tw/Entity_Framework)

Entity Framework (又稱ADO.NET Entity Framework) 是微軟以 ADO.NET 為基礎所發展出來的物件關聯對應 (`O/R Mapping`) 解決方案，以 `Entity Data Model` (EDM) 為主，將資料邏輯層切分為三塊，分別為 `Conceptual Schema`, `Mapping Schema` 與 `Storage Schema` (概念模式，映射架構與存儲模式)三層，其上還有 `Entity Client`，`Object Context` 以及 `LINQ` 可以使用。

物件關聯對應 (Object-Relational Mapping) 讓應用程式可以用完全物件化的方法連接與存取資料庫，抽象化資料結構的方式，將每個資料庫物件都轉換成應用程式物件 (entity)，而資料欄位都轉換為屬性 (property)，關聯則轉換為結合屬性 (association)，讓資料庫的 E/R 模型完全的轉成物件模型，如此讓程式設計師能用最熟悉的程式語言來呼叫存取。而在抽象化的結構之下，則是高度整合與對應結構的`概念層`、`對應層`和`儲存層`，以及支援 Entity Framework 的資料提供者 (provider)，讓資料存取的工作得以順利與完整的進行。

---

UML的關聯(Association), 聚合(Aggregation), 組合(Composition)，三者描述物件的附屬[也就是依賴]關係: 關聯<聚合<組合, 依賴關係是逐漸加強的

<https://codertw.com/%E7%A8%8B%E5%BC%8F%E8%AA%9E%E8%A8%80/623388/>

<https://kilean.pixnet.net/blog/post/159895314-association%2C-aggregation%2C-composition-%E8%AA%AA%E6%98%8E>

---

* 概念層：負責向上的物件與屬性顯露與存取。
* 對應層：將上方的概念層和底下的儲存層的資料結構對應在一起。
* 儲存層：依不同資料庫與資料結構，而顯露出實體的資料結構體，和 Provider 一起，負責實際對資料庫的存取和 SQL 的產生。

![alt](https://upload.wikimedia.org/wikipedia/commons/thumb/1/16/ADO_NET_Entity_Framework_Architecture.png/480px-ADO_NET_Entity_Framework_Architecture.png)

## 查詢

Entity Framework 支援針對概念模型進行物件查詢。 這些查詢可以使用 `Entity SQL` 、Language-Integrated Query (`LINQ`) 和`物件查詢產生器`方法來撰寫。

### Entity SQL

它的物件模型和 ADO.NET 的其他用戶端非常相似，一樣有 Connection, Command, DataReader 等物件，但最大的差異就是，它有自己的 SQL 指令 (Entity SQL)，可以用 SQL 的方式存取 EDM，簡單的說，就是把 EDM 當成一個實體資料庫。

<https://docs.microsoft.com/zh-tw/dotnet/framework/data/adonet/ef/language-reference/entity-sql-language>

```C#
EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();

// Set the provider name.
entityBuilder.Provider = providerName;

// Set the provider-specific connection string.
entityBuilder.ProviderConnectionString = providerString;

// Set the Metadata location.
entityBuilder.Metadata =  @"res://*/AdventureWorksModel.csdl|
                            res://*/AdventureWorksModel.ssdl|
                            res://*/AdventureWorksModel.msl";

Console.WriteLine(entityBuilder.ToString());

using (EntityConnection conn = new EntityConnection(entityBuilder.ToString()))
{
    conn.Open();
    Console.WriteLine("Just testing the connection.");
    conn.Close();
}
```

<https://ithelp.ithome.com.tw/articles/10186363>

## 總結 類似於 SQL 但只有簡單查詢 ?

似乎對於 Entity SQL 都沒有較深的應用方式，全都在 MSDN 內...

Entity SQL 中無法使用下列 Transact-sql 功能。

### DML

Entity SQL 目前不提供 DML 語句（insert、update、delete）的支援。

### DDL

Entity SQL 在目前版本中不提供 DDL 的支援。

### 命令式程式設計

Entity SQL 不會提供命令式程式設計的支援，與 Transact-sql 不同。 請改用程式語言。

### 群組函式

Entity SQL 尚未提供群組函式的支援（例如 CUBE、ROLLUP 和 GROUPING_SET）。

### 分析函式

Entity SQL 尚未提供分析函數的支援。

### 內建函式，運算子

Entity SQL 支援 Transact-sql 內建函數和運算子的子集。 主要存放區提供者可能會支援這些運算子和函式。 Entity SQL 會使用在提供者資訊清單中宣告的存放區特有函式。 此外，Entity Framework 可讓您宣告內建和使用者定義的現有存放區函式，以供 Entity SQL 使用。

### 提示

Entity SQL 不提供查詢提示的機制。

### 批次處理查詢結果

Entity SQL 不支援批次處理查詢結果。 例如，下列是有效的 Transact-sql （以批次傳送）：

```SQL
SELECT * FROM products;
SELECT * FROM catagories;
```

不過，不支援對等的 Entity SQL：

```SQL
SELECT value p FROM Products AS p;
SELECT value c FROM Categories AS c;
```

Entity SQL 只支援每個命令產生一個結果的查詢語句。

---

## LINQ to Entities

實作 `IEnumerable<T>` 泛型介面或 `IQueryable<T>` 泛型介面的資料來源可以透過 LINQ 進行查詢。 實作泛型 `IQueryable<T>` 介面之泛型 `ObjectQuery<T>` 類別的執行個體會當做 LINQ to Entities 查詢的資料來源。 `ObjectQuery<T>` 泛型類別表示傳回零個或多個具型別物件之集合的查詢。 使用 C# 的 var 關鍵字 (在 Visual Basic 中為 Dim)，您也可以讓編譯器推斷實體類型。

```C#
using (AdventureWorksEntities AWEntities = new AdventureWorksEntities())
{
    ObjectQuery<Product> products = AWEntities.Products;

    // LINQ Query syntax:
    IOrderedQueryable<Product> query =
        from product in products
        orderby product.Name, product.ListPrice descending
        select product;

    // LINQ Method syntax:
    IOrderedQueryable<Product> query = products
        .OrderBy(product => product.Name)
        .ThenByDescending(product => product.ListPrice);
}
```

<https://docs.microsoft.com/zh-tw/dotnet/csharp/linq/>

---

## 查詢產生器方法

`ObjectQuery 類別支援對概念模型進行 LINQ to Entities 和 Entity SQL 查詢`。 ObjectQuery 也會實作一組查詢產生器方法，這些方法可用來循序建構與 Entity SQL 相等的查詢命令。由於 ObjectQuery 會實作 IQueryable 和 IEnumerable，所以將 ObjectQuery 所實作的查詢產生器方法結合 LINQ 特定的標準查詢運算子方法 (如 First 或 Count) 是可行的。 LINQ 運算子並不會傳回 ObjectQuery，與查詢產生器方法不同。

```C#
// Get the contacts with the specified name.
ObjectQuery<Contact> contactQuery = context.Contact
    .Where("it.LastName = @ln AND it.FirstName = @fn",
    new ObjectParameter("ln", lastName),
    new ObjectParameter("fn", firstName));
```

---

<https://docs.microsoft.com/zh-tw/samples/browse/?redirectedfrom=MSDN-samples>
