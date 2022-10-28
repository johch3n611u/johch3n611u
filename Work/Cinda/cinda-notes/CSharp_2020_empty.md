# 2020_0618

[進階](#abc)

## 1.C#介紹

* 課程介紹
* 什麼C#
* 為何學習C#

## 2.準備用Csharp

* 安裝 VS Community
* 第一個C#程序
* C#程序的基本架構
* Main()方法 <https://stackoverflow.com/questions/4176326/arguments-to-main-in-c>
* 註解 Comments

## 3.變量與運算符

* 什麼是變量？
* C#數據類型

<https://docs.microsoft.com/zh-tw/dotnet/csharp/tour-of-csharp/types-and-variables>

* 泛型 Generics, List`<T>` 泛型(Generic Type)是一個C#語言的功能，它可以讓你在定義Class、Method、Interface時先不用決定型別，到了要實體化的時候再決定其型別

<https://dotblogs.com.tw/chichiBlog/2017/09/11/155041>

<https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/generics/>

<https://ithelp.ithome.com.tw/articles/10194019>

* const 常數變數定義後即無法改變其值，所以宣告時一定要設定初始值。
* 為變量取名
* 初始化變量 - 給變數一個初始值，每個變數都該初始化，若沒有給定初始值，則變數的值為產生此變數前，殘留在記憶體中的資料。
* 赋值符號
* 基本運算符 operators
* 更多赋值運算符
* C# 類型轉換

### 轉型 Casting

<https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/types/casting-and-type-conversions>

1. 隱含轉換
2. 明確轉換
3. 使用者定義的轉換
4. 使用協助程是類別轉換

* 匿名類型 ( var a = new { n="N" };

<https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/anonymous-types>

* Scope 全域變數（Global variable）、區域變數（Local variable）與區塊變數（Block variable）。
* @ 逐字識別項

1. 限定跳脫字串
2. 讓字串連接跨行
3. 在識別字中的用法

```C#
// string url = "D:\\TEMP\\test.txt";
string url = @"D:\TEMP\test.txt";

//string sqlcmd = "SELECT * FROM A as aa" + "INNER JOIN B as bb" + "ON aa.a = bb.b ORDER BY aa.a";
string sqlcmd = @"SELECT * FROM A as aa INNER JOIN B as bb ON aa.a = bb.b ORDER BY aa.a";

// can not !!! int int = 1;
int @int = 1;
```

<https://blog.xuite.net/david670919/twblog/499762471-C%23+%40%E7%AC%A6%E8%99%9F%28%E5%B0%8F%E8%80%81%E9%BC%A0%29%E7%9A%84%E7%94%A8%E8%99%95>

<https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/tokens/verbatim>

* $ 字串插值(C# 引用)

<https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/tokens/interpolated>

主要用法如下，搭配不同參數可以 format

```C#
string name = "Mark";
var date = DateTime.Now;

// Composite formatting:
Console.WriteLine("Hello, {0}! Today is {1}, it's {2:HH:mm} now.", name, date.DayOfWeek, date);
// String interpolation:
Console.WriteLine($"Hello, {name}! Today is {date.DayOfWeek}, it's {date:HH:mm} now.");
// Both calls produce the same output that is similar to:
// Hello, Mark! Today is Wednesday, it's 19:40 now.
```

---

* 參數 Parameter 寫法

<https://dotblogs.com.tw/mis2000lab/2015/11/26/adonet_parameter_sql_injection_20151126>

<https://ithelp.ithome.com.tw/articles/10230068>

---

* 類別別名 using alisas = NamespaceName

<https://stackoverflow.com/questions/505262/c-sharp-namespace-alias-whats-the-point>

---

* XML 註解

<https://docs.microsoft.com/zh-tw/dotnet/csharp/codedoc>

---

* 前置處理氣指示器

`#`開頭的程式碼並不會編譯給機器執行，而是在編譯的過程中給編譯器看的，稱為「前置處理器」。

 #define 宏，假想中的前置處理器會使用語彙基元字串替代原始程式檔中出現的每個識別項。

 <https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/preprocessor-directives/preprocessor-define>

 #undef 移除 (取消定義) 先前使用 #define 建立的名稱。

===

 #if

 #elif

 #else

 #endif

===

 #waning 編譯器級別的警告

 #error 編譯器級別的錯誤

 #region 指定程式碼區塊

 #endregion 指定程式碼區塊

---

* C# 保留關鍵字

<https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/>

* is

<https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/is>

* as

<https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/operators/type-testing-and-cast#as-operator>

<https://docs.microsoft.com/zh-tw/dotnet/csharp/how-to/safely-cast-using-pattern-matching-is-and-as-operators>

* modified

<https://docs.microsoft.com/zh-tw/dotnet/api/system.windows.forms.textboxbase.modified?view=netcore-3.1>

## 4.數組、字符串與鏈表

* 數組
* 數組屬性與方法
* 字符串
* StringBuilder 比 string 還要更有可變動性。

<https://medium.com/@MyCollegeNotebook/stringbuilder-%E7%9A%84%E4%BD%BF%E7%94%A8-b75300871c6f>

* 字符串屬性與与方法I
* string.formats

<https://docs.microsoft.com/zh-tw/dotnet/api/system.string.format?view=netcore-3.1>

* number.formats

<https://docs.microsoft.com/zh-tw/dotnet/standard/base-types/standard-numeric-format-strings>

有各種類別的 format 方法，與標準格式字串，感覺是需要用到時才去查

* 正規、正則或常規 表示式 Regular Expressions

<https://www.cyut.edu.tw/~ckhung/b/re/index.php>

* 字符串屬性與与方法II
* 字符串屬性與与方法III
* 鏈表

list 擁有更好的伸縮性 arrays vs list Like string vs stringbuilder，必須先引用 system.collections.generic

<https://csharp-station.com/c-arrays-vs-lists/>

* 值類型vs引用類型  Value Types and Reference Types

值類型就是現金，要用直接用；引用類型是存摺，要用還得先去銀行取現

值類型是分配在棧（stack）上面，而引用類型分配在堆（heap）上面

stack 靜態記憶體配置 、 heap 動態記憶體配置

C# 中有兩種類型：參考類型和實值類型。 參考類型的變數會儲存期資料 (物件) 的參考，而實值類型的變數則會直接包含其資料。 使用參考類型時，這兩種變數可以參考相同的物件，因此對其中一個變數進行的作業可能會影響另一個變數所參考的物件。 使用實值型別時，每個變數都有自己的資料複本，因此對某一個變數進行的作業，不可能會影響另一個變數 (但 in、ref 及 out 參數變數除外)，請參閱 in、ref 及 out 參數修飾詞)。

> reference type 的變數也會放在 stack 但其值會放在 heap

<https://dotblogs.com.tw/skyline0217/2011/04/22/23327>

<https://kknews.cc/zh-tw/news/2b6n5zz.html>

* <https://nwpie.blogspot.com/2017/05/5-stack-heap.html>

<https://www.cnblogs.com/fly-100/p/4564048.html>

<https://blog.gtwang.org/programming/memory-layout-of-c-program/>

<https://medium.com/@wuufone/%E5%AD%B8%E6%9C%83-swift-%E7%9A%84%E9%97%9C%E9%8D%B5-value-type-vs-reference-type-50d3034596a8>

<https://numbbbbb.gitbooks.io/-the-swift-programming-language-/content/chapter4/05_Value_and_Reference_Types.html>

---

* 欄位 fields

<https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/fields>

* readonly

<https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/readonly>

* Queue & Stack

Queue 先進先出隊列

Stack 後進先出隊列

<https://dotblogs.com.tw/h091237557/2014/05/23/145236>

* Dictionary ( Key / Value )

<http://code2study.blogspot.com/2012/01/c-dictionary.html>

* BitArray 点阵列

<https://www.runoob.com/csharp/csharp-collection.html>

<https://www.runoob.com/csharp/csharp-bitarray.html>

* BitVector32 結構是用一個 32-bit 的整數值來儲存 32 個 boolean 值的結構，相較於 BitArray ，它只能固定在 32 個位元的大小。

<http://vito-note.blogspot.com/2012/01/specialized-collections.html>

* ObservableCollection 類似像 list 但是可以監聽事件

<https://dotblogs.com.tw/wadehuang36/2010/11/03/observablecollection>

<https://www.itread01.com/content/1513748426.html>

### thread 多線程(多執行緒)

<https://dotblogs.com.tw/kinanson/2017/04/25/083020>

### Concurrent - thread safe

collection的操作通常都是非執行緒安全的，例如List。  意思是說當會一個集合進行多執行緒操作的時候，會造成不可預期的情況，例如資料遺漏、索引重複等等...

執行緒安全的collection，可使用在多執行緒的環境底下

* ConcurrentQueue
* ConcurrentStack
* ConcurrentBag
* ConcurrentDictionary

<https://dotblogs.com.tw/mileslin/2016/03/13/150234>

* BlockingCollection - thread safe

<https://dotblogs.com.tw/kinanson/2017/05/17/145521#3>

## 5.開發交互式程序

* 向用戶顯示消息
* 轉義序列
* 接受用戶輸入
* 將字符串轉換为數字
* 把它們放在一起

## 6.做出選擇和決定

* If 語句
* Switch
* For Loop
* While
* Do While
* Break Continue
* Exception Handling try carth
* 特定誤差 Specific Errors

formatexception

DivideByZeroException 特定異常後才接

exception 普通例外

## 7.物件導向編程I

* boxing 與 unboxing

<https://nwpie.blogspot.com/2017/04/5-boxing-unboxing.html>

* 什麼是面向對象編程
* class
* partial 部分類別

<https://dotblogs.com.tw/jackeir/2015/06/16/151580>

<https://dotblogs.com.tw/dc690216/2010/03/13/14008>

<https://ithelp.ithome.com.tw/articles/10184806>

<https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods>

* 字段(修飾符)
* [屬性](https://www.dropbox.com/sh/3keg0m451l7c5m8/AADbv7VF8u_rb8CdYwecVmToa/C%23%20%E5%9F%BA%E7%A4%8E%E5%88%9D%E5%AD%B8%E8%80%85%E8%AA%B2%E7%A8%8B/7.%20%E9%9D%A2%E5%90%91%E5%B0%8D%E8%B1%A1%E7%B7%A8%E7%A8%8B(I)%20Object%20Oriented%20Programming%20(I)?dl=0&preview=3.+%E5%B1%AC%E6%80%A7Properties.mp4&subfolder_nav_tracking=1) get set 訪問器傳入value

訪問器也可以簡寫 public xxx {get;set;}

訪問器也可以有私有屬性 private get set

Properties (get;set;)

<https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/properties>

* set

<https://pydoing.blogspot.com/2012/11/csharp-set-and-get.html>

<https://dotblogs.com.tw/mvp/2018/04/27/171143>

<https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/set>

---

* 方法 可以是 void
* Overloading 重載 方法的重載 (Method Overloading) 相同 method namd 不同參數

* 命名的參數 funcA(x: 30, y:20, z:10)

* 有預設值的參數

### 個數可變的參數

<https://dotblogs.com.tw/LazyCodeStyle/2016/05/30/232127>

<https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/params>

<https://ithelp.ithome.com.tw/articles/10198212>

* kewword
  * params 指定這個參數可以接受可變數目的引數。
  * in 指定這個參數是傳址方式傳遞，但只會由所呼叫的方法讀取。
  * ref 指定這個參數是傳址方式傳遞，且可以由所呼叫的方法讀取或寫入。
  * out 指定這個參數是傳址方式傳遞，且由所呼叫的方法寫入。
  * nullable <https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/nullable-value-types>

---

* ToString() Method
* Constructors 施工人 建構子 構造函數(建構函數)
* 實例化對象 Instantiating an Object
* 構造函數Constructors (II)
* Static <https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/static-constructors>

static 不需要 new 物件 即可使用，適合常態儲存值

初始化時機（對於靜態的只要使用了類，就會初始化，非靜態的則要在創建對象的時候才初始化）

<https://www.twblogs.net/a/5b8ed6952b71771883482178>

* 使用數組和列表做為參數
* 使用 params keyword

method(params string [] xxx) 可以指定在參數數目可變處採用參數的方法參數。

```C#
// object[] arr = new object[3] { 100, 'a', "keywords" };

//UseParams(arr);

UseParams(100, 'a', "keywords");

Console.Read();
```

## 8.物件導向編程II

* system object

<https://docs.microsoft.com/zh-tw/dotnet/api/system.object?view=netcore-3.1>

* 子類继承父類 - 構造
* base.Method(); 呼叫父類型的方法

<https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/base>

* 子類屬性與方法
* 擴究方法 Extension Method (partial)

<http://blog.ctrlxctrlv.net/unity-extension-methods/>

<https://tpu.thinkpower.com.tw/tpu/articleDetails/1184>

<https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/extension-methods>

* this 指的是類別的目前執行個體，也用作擴充方法第一個參數的修飾詞。

<https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/this>

<https://www.itread01.com/content/1520914804.html>

* lambda 運算式

<https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/statements-expressions-operators/lambda-expressions>

* Main()方法
* abstract 抽象類別 interface 介面接口

<https://medium.com/@ad57475747/c-%E9%9B%9C%E8%A8%98-%E4%BB%8B%E9%9D%A2-interface-%E6%8A%BD%E8%B1%A1-abstract-%E8%99%9B%E6%93%AC-virtual-%E4%B9%8B%E6%88%91%E8%A6%8B-dc3c5878bb80>

* sealed 不能被繼承

<https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/sealed>

* 多態性 Polymorphism 多型 // 虛方法 virtual , override

<https://jimmy0222.pixnet.net/blog/post/37271702>

<https://medium.com/@ad57475747/c-%E9%9B%9C%E8%A8%98-%E4%BB%8B%E9%9D%A2-interface-%E6%8A%BD%E8%B1%A1-abstract-%E8%99%9B%E6%93%AC-virtual-%E4%B9%8B%E6%88%91%E8%A6%8B-dc3c5878bb80>

<https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/virtual>

<https://wayne265265.pixnet.net/blog/post/115533452-%E3%80%90%E6%95%99%E5%AD%B8%E3%80%91override-%E8%88%87-overload-%E7%9A%84%E5%B7%AE%E5%88%A5>

* 隱藏方法(Hiding Methods) new

<https://dotblogs.com.tw/skychang/2012/05/10/72114>

<https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/knowing-when-to-use-override-and-new-keywords>

<https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/new-modifier>

* Local Functions 區域函式

<https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/local-functions>

* Tuples 簡單、或者用完即丟的返回值方法

<https://www.huanlintalk.com/2017/04/c-7-tuple-syntax.html>

* try, catch, finally

<https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/try-finally>

使用 finally 區塊，即可清除 try 區塊中所配置的任何資源，以及執行程式碼，即使 try 區塊中發生例外狀況也是一樣。

它是整段程式必執行的部分,通常是用來放清資源的

<https://charleslin74.pixnet.net/blog/post/454942883-%5Bc%23%5D-exception%E7%9A%84%E5%9F%BA%E6%9C%AC%E4%BD%BF%E7%94%A8%E6%96%B9%E6%B3%95-try-catch-finally>

<https://dotblogs.com.tw/yc421206/2011/06/09/27445>

* re-throw exception , throw; vs throw ex

```C#
static string ReadAFile(string fileName) {
    string result = string.Empty;
    try {
        result = File.ReadAllLines(fileName);
    } catch(Exception ex) {
        throw ex; // This will show ReadAFile in the StackTrace
        throw;    // This will show ReadAllLines in the StackTrace
    }
```

<https://stackoverflow.com/questions/178456/what-is-the-proper-way-to-re-throw-an-exception-in-c>

<https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/throw>

## 9.列舉與結構體

* Enum 枚舉 <https://www.runoob.com/cprogramming/c-enum.html>

### IEnumerator, IEnumerable, and Yield

迭代器 類似 foreach

* Yield 自定義迭代器語法時的[語法糖](https://www.google.com/search?q=%E8%AA%9E%E6%B3%95%E7%B3%96&rlz=1C1CHBF_zh-TWTW905TW905&oq=%E8%AA%9E%E6%B3%95%E7%B3%96&aqs=chrome..69i57j0l7.2633j0j4&sourceid=chrome&ie=UTF-8)

<https://dev.twsiyuan.com/2016/03/csharp-ienumerable-ienumerator-and-yield-return.html>

<https://hant-kb.kutu66.com/python/post_1497973>

* Struct 自訂資料型別, 所以定義結 構體後, 就能把它當成一種新的資料型別。

<https://kopu.chat/2017/05/30/c-%E8%AA%9E%E8%A8%80%EF%BC%9A%E7%B5%90%E6%A7%8B%EF%BC%88struct%EF%BC%89%E8%87%AA%E8%A8%82%E4%B8%8D%E5%90%8C%E8%B3%87%E6%96%99%E5%9E%8B%E6%85%8B%E7%B6%81%E4%B8%80%E8%B5%B7/>

Object and Struct (copy by value)

類似 class 但不支持繼承，但可執行介面，值類型非引用類型

<https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/struct>

## 10.LINQ 語言整合查詢

LINQ, query, filter, group, join, select

<https://wellsb.com/csharp/beginners/linq-syntax-filter-list/>

<https://docs.microsoft.com/zh-tw/dotnet/csharp/linq/write-linq-queries>

1. 查詢語法
2. 方法語法
3. 合併使用查詢語法與方法語法

* linq i
* linq ii

system.linq

```C#
int[] xx = {xx,xx}
var xxx =
from num in xx
where (num%2==0)
orderby num ascending
select num;
```

要選擇多過一個屬性 必須用 select new { cust.name,cust,balance}

* Concat 合併兩個或多個陣列 List。

<https://docs.microsoft.com/zh-tw/dotnet/csharp/linq/write-linq-queries#example---method-syntax>

<https://developer.mozilla.org/zh-TW/docs/Web/JavaScript/Reference/Global_Objects/Array/concat>

### LINQ to DataSet

<https://docs.microsoft.com/zh-tw/dotnet/framework/data/adonet/sorting-with-dataview-linq-to-dataset>

* linq zip 將指定的函式套用至兩個序列的對應項目，產生結果的序列。

<https://docs.microsoft.com/zh-tw/dotnet/api/system.linq.enumerable.zip?view=netcore-3.1>

* linq skip 跳過來源序列中的前 n 筆項目，再把剩下的資料全部回傳 / take 取出來源序列中前 n 筆資料

<https://ithelp.ithome.com.tw/articles/10104729>

### ADO 資料庫操作

* Connection String
* Connection Pools 連接池
* 要知道 SqlConnection, SqlCommand 等傳統方法
* Transaction 資料庫交易, IsolationalLevel 指定交易層級, Rollback 從暫止狀態回復交易。

<https://ithelp.ithome.com.tw/articles/10194749>

<https://docs.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqlconnection.begintransaction?view=dotnet-plat-ext-3.1>

<https://docs.microsoft.com/zh-tw/dotnet/api/system.transactions.isolationlevel?view=netcore-3.1>

<https://docs.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqltransaction.rollback?view=dotnet-plat-ext-3.1>

## 11.文件處理

* 讀取文本文件

system.io

streamReader

dispose() 釋放記憶體

```C#
string path = "/Users/limjunqu/Documents/myFile2.txt"
if (File.Exists(path)){
  using (StreamReader sr = new StreamReader(path)) {

    // Dispose()

    while (sr.EndOfStream != true){
      Console.WriteLine(sr.ReadLine());
    }

    sr.Close();
  }
}else{
    // do something else
    }
```

* 處理錯誤

File.Exists() 查詢文件是否存在

* 寫入文本文件

steamWriter

* Zip

<https://docs.microsoft.com/zh-tw/dotnet/api/system.io.compression.zipfile?view=netcore-3.1>

* C# 檔案操作大全

  * FileSystemInfo 代表目前目錄的檔案與子目錄。, FIleInfo 檔案資訊, File 檔案, DirectoryInfo 取得目錄下的子目錄，和檔案及目錄的基本屬性。, Directory 目錄 , DriveInfo 取得磁碟相關資訊。
  * Path
  * Stream, StreamReader 實作以特定的編碼方式自位元組資料流讀取字元的 TextReader。, StreamWriter 實作以特定的編碼方式將字元寫入位元組資料流的 TextWriter。

<https://www.itread01.com/p/623567.html>

<https://einboch.pixnet.net/blog/post/266428691>

* Stream 提供位元組序列的一般檢視。 這是抽象類別。

<https://docs.microsoft.com/zh-tw/dotnet/api/system.io.stream?view=netcore-3.1>

## 12.簡單工資軟件項目

---

---

---

---

---

---

---

## 13. csharp 各版本功能 <a id="abc"></a>

<https://zhuanlan.zhihu.com/p/109853373>

## C# 2.0版 - 2005

### 泛型

Java中的泛型不支持值类型，且会运行时类型擦除，这一点.NET更优秀。

```c#
// Declare the generic class.
public class GenericList<T>
{
    public void Add(T input) { }
}
class TestGenericList
{
    private class ExampleClass { }
    static void Main()
    {
        // Declare a list of type int.
        GenericList<int> list1 = new GenericList<int>();
        list1.Add(1);

        // Declare a list of type string.
        GenericList<string> list2 = new GenericList<string>();
        list2.Add("");

        // Declare a list of type ExampleClass.
        GenericList<ExampleClass> list3 = new GenericList<ExampleClass>();
        list3.Add(new ExampleClass());
    }
}
```

### 部分类型

拆分一个类、一个结构、一个接口或一个方法的定义到两个或更多的文件中是可能的。 每个源文件包含类型或方法定义的一部分，编译应用程序时将把所有部分组合起来。

```c#
public partial class Employee
{
    public void DoWork()
    {
    }
}

public partial class Employee
{
    public void GoToLunch()
    {
    }
}
```

### 匿名方法

```c#
Func<int, int, int> sum = delegate (int a, int b) { return a + b; };
Console.WriteLine(sum(3, 4));  // output: 7
```

### 可以为null的值类型

```c#
double? pi = 3.14;
char? letter = 'a';

int m2 = 10;
int? m = m2;

bool? flag = null;

// An array of a nullable type:
int?[] arr = new int?[10];
```

### 迭代器

```c#
static void Main()
{
    foreach (int number in SomeNumbers())
    {
        Console.Write(number.ToString() + " ");
    }
    // Output: 3 5 8
    Console.ReadKey();
}

public static System.Collections.IEnumerable SomeNumbers()
{
    yield return 3;
    yield return 5;
    yield return 8;
}
```

### 协变和逆变

在 C# 中，协变和逆变能够实现数组类型、委托类型和泛型类型参数的隐式引用转换。 协变保留分配兼容性，逆变则与之相反。

```c#
// Assignment compatibility.
string str = "test";
// An object of a more derived type is assigned to an object of a less derived type.
object obj = str;

// Covariance.
IEnumerable<string> strings = new List<string>();
// An object that is instantiated with a more derived type argument
// is assigned to an object instantiated with a less derived type argument.
// Assignment compatibility is preserved.
IEnumerable<object> objects = strings;

// Contravariance.
// Assume that the following method is in the class:
// static void SetObject(object o) { }
Action<object> actObject = SetObject;
// An object that is instantiated with a less derived type argument
// is assigned to an object instantiated with a more derived type argument.
// Assignment compatibility is reversed.
Action<string> actString = actObject;
```

## C# 3.0版 - 2007

### 自动实现的属性

```C#
// This class is mutable. Its data can be modified from
// outside the class.
class Customer
{
    // Auto-implemented properties for trivial get and set
    public double TotalPurchases { get; set; }
    public string Name { get; set; }
    public int CustomerID { get; set; }

    // Constructor
    public Customer(double purchases, string name, int ID)
    {
        TotalPurchases = purchases;
        Name = name;
        CustomerID = ID;
    }

    // Methods
    public string GetContactInfo() { return "ContactInfo"; }
    public string GetTransactionHistory() { return "History"; }

    // .. Additional methods, events, etc.
}

class Program
{
    static void Main()
    {
        // Intialize a new object.
        Customer cust1 = new Customer(4987.63, "Northwind", 90108);

        // Modify a property.
        cust1.TotalPurchases += 499.99;
    }
}
```

### 匿名类型

```C#
var v = new { Amount = 108, Message = "Hello" };

// Rest the mouse pointer over v.Amount and v.Message in the following
// statement to verify that their inferred types are int and n .
Console.WriteLine(v.Amount + v.Message);
```

### 查询表达式（LINQ）

LINQ允许你可以像写SQL一样写C#代码，像这样：

```C#
from p in persons
where p.Age > 18 && p.IsBeatiful
select new
{
    p.WeChatId,
    p.PhoneNumber
}
```

LINQ的意义在于让C#做出了重大调整，本章中说到的lambda表达式、扩展方法、表达式树、匿名类型、自动属性等，都是LINQ的必要组成部分。

由于用扩展方法的形式也能得到一致的结果，而且还能让代码风格更加一致，所以我平时用LINQ语法较少：

```C#
// 与上文代码相同，但改成了扩展方法风格：
persons
    .Where(x => x.Age > 18 && x.IsBeatiful)
    .Select(x => new
    {
        x.WeChatId,
        x.PhoneNumber,
    });
```

### Lambda表达式

```C#
Func<int, int> square = x => x * x;
Console.WriteLine(square(5));
// Output:
// 25
```

#### 表达式树

这个是LINQ的基础之一，它的作用是将代码像数据一样，保存在内存中；然后稍后对这些"代码数据"进行重新解释/执行。

Entity Framework就是一个经典场景，它先将表达式树保存起来，然后执行时，将其翻译为SQL发给数据库执行。

注意：表达式树并不能表示所有的代码，C# 3.0之后的语法，包含??、?.、async await、可选参数等，都无法放到表达式树中。据说官方准备更新它，但迟迟没有进展。

### 扩展方法

扩展方法使你能够向现有类型"添加"方法，而无需创建新的派生类型、重新编译或以其他方式修改原始类型。

```C#
static void Main()
{
    Console.WriteLine ("Perth".IsCapitalized());
    // Equivalent to:
    Console.WriteLine (StringHelper.IsCapitalized ("Perth"));

    // Interfaces can be extended, too:
    Console.WriteLine ("Seattle".First());   // S
}

public static class StringHelper
{
    public static bool IsCapitalized (this string s)
    {
        if (string.IsNullOrEmpty(s)) return false;
        return char.IsUpper (s[0]);
    }

    public static T First<T> (this IEnumerable<T> sequence)
    {
        foreach (T element in sequence)
            return element;

        throw new InvalidOperationException ("No elements!");
    }
}
```

### var

```C#
var i = 10; // Implicitly typed.
int i = 10; // Explicitly typed.
```

### 分部方法

```C#
namespace PM
{
    partial class A
    {
        partial void OnSomethingHappened(string s);
    }

    // This part can be in a separate file.
    partial class A
    {
        // Comment out this method and the program
        // will still compile.
        partial void OnSomethingHappened(String s)
        {
            Console.WriteLine("Something happened: {0}", s);
        }
    }
}
```

### 对象和集合初始值设定项

```C#
public class Cat
{
    // Auto-implemented properties.
    public int Age { get; set; }
    public string Name { get; set; }

    public Cat()
    {
    }

    public Cat(string name)
    {
        this.Name = name;
    }
}
```

## C# 4.0版 - 2010

### dynamic

这个是特性使得CLR不得不进行一次修改。有了这个，C#也能像js、php、python等弱类型语言一样写代码了。

```C#
dynamic a = 3;
a = 3.14;
a = "Hello World";
a = new[] { 1, 2, 3, 4, 5 };
a = new Func<int>(() => 3);
a = new StringBuilder();
Console.WriteLine(a.GetType().Name); // StringBuilder
```

注意dynamic可以表示任何东西，包含数组、委托等等。滥用dynamic容易让程序变得很难维护。

### 命名参数/可选参数

```C#
PrintOrderDetails(productName: "Red Mug", sellerName: "Gift Shop", orderNum: 31);
public void ExampleMethod(int required, string optionalstr = "default string",
    int optionalint = 10)
```

### 泛型中的协变和逆变

```C#
IEnumerable<Derived> d = new List<Derived>();
IEnumerable<Base> b = d;
Action<Base> b = (target) => { Console.WriteLine(target.GetType().Name); };
Action<Derived> d = b;
d(new Derived());
```

类型等效、内置互操作类型
这个主要是为了和COM进行交互。之前需要引用一些COM类型相关的程序集，现在可以直接引用COM。 具体可以参见：https://docs.microsoft.com/zh-cn/dotnet/framework/interop/type-equivalence-and-embedded-interop-types

## C# 5.0版 - 2012

### async/await

```C#
private DamageResult CalculateDamageDone()
{
    // Code omitted:
    //
    // Does an expensive calculation and returns
    // the result of that calculation.
}

calculateButton.Clicked += async (o, e) =>
{
    // This line will yield control to the UI while CalculateDamageDone()
    // performs its work.  The UI thread is free to perform other work.
    var damageResult = await Task.Run(() => CalculateDamageDone());
    DisplayDamage(damageResult);
};
```

async/await的本质是状态机，像IEnumerable<T>一样。以前游戏引擎Unity只支持C# 3.0，因此当时它用状态机发Http请求是用的IEnumerable<T>。

async/await有两个好处，一是可以避免UI线程卡顿，二是提高系统吞吐率，最终提高性能。

### 调用方信息

```c#
public void DoProcessing()
{
    TraceMessage("Something happened.");
}

public void TraceMessage(string message,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
{
    System.Diagnostics.Trace.WriteLine("message: " + message);
    System.Diagnostics.Trace.WriteLine("member name: " + memberName);
    System.Diagnostics.Trace.WriteLine("source file path: " + sourceFilePath);
    System.Diagnostics.Trace.WriteLine("source line number: " + sourceLineNumber);
}

// Sample Output:
//  message: Something happened.
//  member name: DoProcessing
//  source file path: c:\Visual Studio Projects\CallerInfoCS\CallerInfoCS\Form1.cs
//  source line number: 31
```

注意这个是编译期生成的，因此比StackTrace更能保证性能。

## C# 6.0版 - 2015

### 静态导入

终于可以不用写静态类名了。

```C#
using static System.Math;
using static System.Console;

WriteLine(Sin(3.14)); // 0.00159265291648683
```

### 异常筛选器

在try-catch时，可以按指定的条件进行catch，其它条件不catch。

```C#
public static async Task<string> MakeRequest()
{
    WebRequestHandler webRequestHandler = new WebRequestHandler();
    webRequestHandler.AllowAutoRedirect = false;
    using (HttpClient client = new HttpClient(webRequestHandler))
    {
        var stringTask = client.GetStringAsync("https://docs.microsoft.com/en-us/dotnet/about/");
        try
        {
            var responseText = await stringTask;
            return responseText;
        }
        catch (System.Net.Http.HttpRequestException e) when (e.Message.Contains("301"))
        {
            return "Site Moved";
        }
    }
}
```

### 自动初始化表达式

```C#
public ICollection<double> Grades { get; } = new List<double>();
```

### Expression-bodied 函数成员

```C#
public override string ToString() => $"{LastName}, {FirstName}";
```

### Null传播器

```C#
var first = person?.FirstName;
```

### 字符串内插

```C#
public string GetGradePointPercentage() =>
    $"Name: {LastName}, {FirstName}. G.P.A: {Grades.Average():F2}";
```

### nameof表达式

有时字符串值和某个变量名称一致，尤其是在做参数验证时。这里nameof就能在编译期，自动从变量名生成一个字符串。

```C#
if (IsNullOrWhiteSpace(lastName))
    throw new ArgumentException(message: "Cannot be blank", paramName: nameof(lastName));
```

### 索引初始值设定项

使集合初始化更容易的另一个功能是对 Add 方法使用扩展方法 。 添加此功能的目的是进行 Visual Basic 的奇偶校验。 如果自定义集合类的方法具有通过语义方式添加新项的名称，则此功能非常有用。

## C# 7.0版本 - 2017

### out变量

```C#
if (int.TryParse(input, out int result))
    Console.WriteLine(result);
else
    Console.WriteLine("Could not parse input");
```

### 元组和解构

```C#
(string Alpha, string Beta) namedLetters = ("a", "b");
Console.WriteLine($"{namedLetters.Alpha}, {namedLetters.Beta}");
```

如上代码所示，解构可以将元组拆分为多个变量。

### 模式匹配

现在可以在匹配一个类型时，自动转换为这个类型的变量，如果转换失败，这个变量就赋值为默认值（null或0）。

极简版：

```C#
if (input is int count)
    sum += count;
```

switch/case版：

```C#
public static int SumPositiveNumbers(IEnumerable<object> sequence)
{
    int sum = 0;
    foreach (var i in sequence)
    {
        switch (i)
        {
            case 0:
                break;
            case IEnumerable<int> childSequence:
            {
                foreach(var item in childSequence)
                    sum += (item > 0) ? item : 0;
                break;
            }
            case int n when n > 0:
                sum += n;
                break;
            case null:
                throw new NullReferenceException("Null found in sequence");
            default:
                throw new InvalidOperationException("Unrecognized type");
        }
    }
    return sum;
}
```

### 本地函数

这个主要是方便，javascript就能这样写。

比lambda的好处在于，这个可以定义在后面，而lambda必须定义在前面。

```C#
public static IEnumerable<char> AlphabetSubset3(char start, char end)
{
    if (start < 'a' || start > 'z')
        throw new ArgumentOutOfRangeException(paramName: nameof(start), message: "start must be a letter");
    if (end < 'a' || end > 'z')
        throw new ArgumentOutOfRangeException(paramName: nameof(end), message: "end must be a letter");

    if (end <= start)
        throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");

    return alphabetSubsetImplementation();

    IEnumerable<char> alphabetSubsetImplementation()
    {
        for (var c = start; c < end; c++)
            yield return c;
    }
}
```

### 更多的expression-bodied成员

该功能可以让一些函数写成表达式的形式，非常的方便。

```C#
// Expression-bodied constructor
public ExpressionMembersExample(string label) => this.Label = label;

// Expression-bodied finalizer
~ExpressionMembersExample() => Console.Error.WriteLine("Finalized!");

private string label;

// Expression-bodied get / set accessors.
public string Label
{
    get => label;
    set => this.label = value ?? "Default label";
}
```

### Ref 局部变量和返回结果

此功能允许使用并返回对变量的引用的算法，这些变量在其他位置定义。 一个示例是使用大型矩阵并查找具有某些特征的单个位置。
这个功能主要是为了提高值类型的性能，让它真正发挥其作用。C++就有类似的功能。

```C#
public static ref int Find(int[,] matrix, Func<int, bool> predicate)
{
    for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
            if (predicate(matrix[i, j]))
                return ref matrix[i, j];
    throw new InvalidOperationException("Not found");
}
ref var item = ref MatrixSearch.Find(matrix, (val) => val == 42);
Console.WriteLine(item);
item = 24;
Console.WriteLine(matrix[4, 2]);
```

### 弃元

通常，在进行元组解构或使用out参数调用方法时，必须定义一个其值无关紧要且你不打算使用的变量。 为处理此情况，C#增添了对弃元的支持 。 弃元是一个名为_的只写变量，可向单个变量赋予要放弃的所有值。 弃元类似于未赋值的变量；不可在代码中使用弃元（赋值语句除外）。

```C#
using System;
using System.Collections.Generic;

public class Example
{
    public static void Main()
    {
        var (_, _, _, pop1, _, pop2) = QueryCityDataForYears("New York City", 1960, 2010);

        Console.WriteLine($"Population change, 1960 to 2010: {pop2 - pop1:N0}");
    }

    private static (string, double, int, int, int, int) QueryCityDataForYears(string name, int year1, int year2)
    {
        int population1 = 0, population2 = 0;
        double area = 0;

        if (name == "New York City")
        {
            area = 468.48;
            if (year1 == 1960)
            {
                population1 = 7781984;
            }
            if (year2 == 2010)
            {
                population2 = 8175133;
            }
            return (name, area, year1, population1, year2, population2);
        }

        return ("", 0, 0, 0, 0, 0);
    }
}
// The example displays the following output:
//      Population change, 1960 to 2010: 393,149
```

### 二进制文本和数字分隔符

这个用于使数字和二进制更可读。

```C#
// 二进制文本：
public const int Sixteen =   0b0001_0000;
public const int ThirtyTwo = 0b0010_0000;
public const int SixtyFour = 0b0100_0000;
public const int OneHundredTwentyEight = 0b1000_0000;

// 数字分隔符：
public const long BillionsAndBillions = 100_000_000_000;
public const double AvogadroConstant = 6.022_140_857_747_474e23;
public const decimal GoldenRatio = 1.618_033_988_749_894_848_204_586_834_365_638_117_720_309_179M;
```

### throw表达式

throw之前必须是一个语句，因此有时不得不写更多的代码来完成所需功能。但7.0提供了throw表达式来使代码更简洁，阅读更轻松。

```C#
void Main()
{
    // You can now throw expressions in expressions clauses.
    // This is useful in conditional expressions:

    string result = new Random().Next(2) == 0 ? "Good" : throw new Exception ("Bad");
    result.Dump();

    Foo().Dump();
}

public string Foo() => throw new NotImplementedException();
```

## C# 8.0 版 - 2019

### Readonly 成员

```C#
public readonly override string ToString() =>
    $"({X}, {Y}) is {Distance} from the origin";
```

### 默认接口方法

接口中也能定义方法了，这个新功能经常受到争论。但想想，有时是先定义接口，而实现接口需要实现很多相关、但又繁琐的功能，如ASP.NET Core中的ILogger，谁用谁知道，特别多需要实现的方法，但又都差不多。因此所以这个功能其实很有必要。

```C#
void Main()
{
    ILogger foo = new Logger();
    foo.Log (new Exception ("test"));
}

class Logger : ILogger
{
    public void Log (string message) => Console.WriteLine (message);
}

interface ILogger
{
    void Log (string message);

    // Adding a new member to an interface need not break implementors:
    public void Log (Exception ex) => Log (ExceptionHeader + ex.Message);

    // The static modifier (and other modifiers) are now allowed:
    static string ExceptionHeader = "Exception: ";
}
```

## 模式匹配增强

这个是为简化代码、函数式编程而生的，我个人非常喜欢。

### 属性模式

```C#
public static decimal ComputeSalesTax(Address location, decimal salePrice) =>
    location switch
    {
        { State: "WA" } => salePrice * 0.06M,
        { State: "MN" } => salePrice * 0.75M,
        { State: "MI" } => salePrice * 0.05M,
        // other cases removed for brevity...
        _ => 0M
    };
```

### Tuple模式

```C#
public static string RockPaperScissors(string first, string second)
    => (first, second) switch
    {
        ("rock", "paper") => "rock is covered by paper. Paper wins.",
        ("rock", "scissors") => "rock breaks scissors. Rock wins.",
        ("paper", "rock") => "paper covers rock. Paper wins.",
        ("paper", "scissors") => "paper is cut by scissors. Scissors wins.",
        ("scissors", "rock") => "scissors is broken by rock. Rock wins.",
        ("scissors", "paper") => "scissors cuts paper. Scissors wins.",
        (_, _) => "tie"
    };
```

### 位置模式

```C#
static Quadrant GetQuadrant(Point point) => point switch
{
    (0, 0) => Quadrant.Origin,
    var (x, y) when x > 0 && y > 0 => Quadrant.One,
    var (x, y) when x < 0 && y > 0 => Quadrant.Two,
    var (x, y) when x < 0 && y < 0 => Quadrant.Three,
    var (x, y) when x > 0 && y < 0 => Quadrant.Four,
    var (_, _) => Quadrant.OnBorder,
    _ => Quadrant.Unknown
};
```

### switch表达式

这个功能能使代码从大量的if/else或switch/case变成“一行代码”，符合函数式编程的思想，非常好用！

```C#
public static RGBColor FromRainbow(Rainbow colorBand) =>
    colorBand switch
    {
        Rainbow.Red    => new RGBColor(0xFF, 0x00, 0x00),
        Rainbow.Orange => new RGBColor(0xFF, 0x7F, 0x00),
        Rainbow.Yellow => new RGBColor(0xFF, 0xFF, 0x00),
        Rainbow.Green  => new RGBColor(0x00, 0xFF, 0x00),
        Rainbow.Blue   => new RGBColor(0x00, 0x00, 0xFF),
        Rainbow.Indigo => new RGBColor(0x4B, 0x00, 0x82),
        Rainbow.Violet => new RGBColor(0x94, 0x00, 0xD3),
        _              => throw new ArgumentException(message: "invalid enum value", paramName: nameof(colorBand)),
    };
```

### using声明

```C#
static int WriteLinesToFile(IEnumerable<string> lines)
{
    using var file = new System.IO.StreamWriter("WriteLines2.txt");
    // Notice how we declare skippedLines after the using statement.
    int skippedLines = 0;
    foreach (string line in lines)
    {
        if (!line.Contains("Second"))
        {
            file.WriteLine(line);
        }
        else
        {
            skippedLines++;
        }
    }
    // Notice how skippedLines is in scope here.
    return skippedLines;
    // file is disposed here
}
```

### 静态本地函数

相比非静态本地函数，静态本地函数没有闭包，因此生成的代码更少，性能也更容易控制。

```C#
int M()
{
    int y = 5;
    int x = 7;
    return Add(x, y);

    static int Add(int left, int right) => left + right;
}
```

### 异步流

这个功能和IEnumerable<T>、Task<T>对应，一个经典的表格如下：

其中，这个问号?终于有了答案，它就叫异步流——IAsyncEnumerable<T>：

```C#
public static async System.Collections.Generic.IAsyncEnumerable<int> GenerateSequence()
{
    for (int i = 0; i < 20; i++)
    {
        await Task.Delay(100);
        yield return i;
    }
}
```

不像IEnumerable<T>，IAsyncEnumerable<T>系统还没有内置扩展方法，因此可能没有IEnumerable<T>方便，但是可以通过安装NuGet包f来实现和IEnumerable<T>一样（或者更爽）的效果。

### 索引和范围

和Python中的切片器一样，只是-用^代替了。

```C#
var words = new string[]
{
                // index from start    index from end
    "The",      // 0                   ^9
    "quick",    // 1                   ^8
    "brown",    // 2                   ^7
    "fox",      // 3                   ^6
    "jumped",   // 4                   ^5
    "over",     // 5                   ^4
    "the",      // 6                   ^3
    "lazy",     // 7                   ^2
    "dog"       // 8                   ^1
};              // 9 (or words.Length) ^0

var quickBrownFox = words[1..4];
var lazyDog = words[^2..^0];
var allWords = words[..]; // contains "The" through "dog".
var firstPhrase = words[..4]; // contains "The" through "fox"
var lastPhrase = words[6..]; // contains "the", "lazy" and "dog"
```

### Null合并赋值

```C#
List<int> numbers = null;
int? i = null;

numbers ??= new List<int>();
numbers.Add(i ??= 17);
numbers.Add(i ??= 20);

Console.WriteLine(string.Join(" ", numbers));  // output: 17 17
Console.WriteLine(i);  // output: 17
```

### 非托管构造类型

与任何非托管类型一样，可以创建指向此类型的变量的指针，或针对此类型的实例在堆栈上分配内存块

```C#
Span<Coords<int>> coordinates = stackalloc[]
{
    new Coords<int> { X = 0, Y = 0 },
    new Coords<int> { X = 0, Y = 3 },
    new Coords<int> { X = 4, Y = 0 }
};
```

### 嵌套表达式中的 stackalloc

```C#
Span<int> numbers = stackalloc[] { 1, 2, 3, 4, 5, 6 };
var ind = numbers.IndexOfAny(stackalloc[] { 2, 4, 6 ,8 });
Console.WriteLine(ind);  // output: 1
```

### 附录/总结

这么多功能，你印象最深刻的是哪个呢？

参考资料：C#发展历史 - C#指南 | Microsoft Docs https://docs.microsoft.com/zh-cn/dotnet/csharp/whats-new/csharp-version-history
本文内容和代码由肖鹏整理，有修改；转载已获得肖鹏本人授权。肖鹏是我公司从Java转.NET的同事。原文链接为：https://akiyax.github.io/new-features-in-csharp/。
喜欢的朋友请关注我的微信公众号：【DotNet骚操作】

http://weixin.qq.com/r/vy4oMAPEKF5XrUdT93ut (二维码自动识别)
