雖然跟自身目標比較無相關...

但強制要考那就讀吧...

https://www.tutorialspoint.com/compile_csharp_online.php

## CSharp

* http://tpcg.io/vqeAXRr1
* Action<T>,Func<T> 
* Anonymous Methods 
* Lambda expressions 
* Events 

```
using System.IO;
using System;

class Program
{
    // 參考文章
    // https://ithelp.ithome.com.tw/articles/10205614
    // https://me1237guy.pixnet.net/blog/post/65140795-c%23-delegate-and-event-%E5%A7%94%E8%A8%97%E8%88%87%E4%BA%8B%E4%BB%B6
    // https://codertw.com/%E5%89%8D%E7%AB%AF%E9%96%8B%E7%99%BC/206252/
    // https://iter01.com/137001.html
    
    // delegate 委託
    public delegate void GreetingDelegate(string Name);
    public delegate int AnonymousMethods  (int x, int y);
    public delegate void EventMethod();
    static event EventMethod DelegateEvent;
    
    static void Main()
    {
       Program.Say("育誠",SayCN);
       Program.Say("Yucheng",SayEN);
       
       // 預設委託
       // Func<T1,T2,T3,T4,TResult> 有反傳值的委託，具有多型可丟最多4個參數
       Func<string> SaySomeThing = Program.GetName;
       Console.WriteLine(SaySomeThing());
       
       // Action<T> 無法傳值的委託
       Action<string,GreetingDelegate> SaySomeThing2 = Program.Say;
       SaySomeThing2("育誠",SayCN);
       
       // 匿名函式
       AnonymousMethods AM = delegate (int x, int y) {
                return x + y;
       };
       Console.WriteLine(AM(1,1));
       
       // 匿名 Lambda表達式
       AM = (int x, int y) => { return x * y; };
       Console.WriteLine(AM(2,2));
       
       // Event
       Program.DelegateEvent += new EventMethod(WriteName);
       Program.DelegateEvent += new EventMethod(WriteName);
       Program.DelegateEvent(); // 事件觸發
    }

    static void SayCN(string Name)
    {
        Console.WriteLine("你好,"+Name);
    }
    
    static void SayEN(string Name)
    {
        Console.WriteLine("Hollow,"+Name);
    }
    
    static void Say(string Name,GreetingDelegate Greeting)
    {
        Greeting(Name);
    }
        
    static string GetName()
    {
        return "育誠";
    }  
    
    static void WriteName()
    {
        Console.WriteLine("育誠");
    }  
            
}
```

* Stack, Heap memory https://nwpie.blogspot.com/2017/05/5-stack-heap.html
* Garbage Collection https://derjohng.doitwell.tw/533/%E9%9B%BB%E8%85%A6%E9%A1%9E%E5%88%A5/%E7%A8%8B%E5%BC%8F%E7%AD%86%E8%A8%98/c-garbage-collection-%E7%AD%86%E8%A8%98/
* using ( …) { … } 

---
    
* Parallel.For, Parallel.Foreach
    
```
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// 參考 
// https://dotblogs.com.tw/asdtey/2010/05/08/parallelforforeach
// https://dotblogs.com.tw/JesperLai/2018/04/05/232204
// Parallel 平行運算

class Program
{
    static void Main()
    {
        List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        
        for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine("【第" + i.ToString() + "回合】");
                Single(list);
                Multi(list);
                Console.WriteLine();
            }

    }
    
    private static void Single(List<int> list)
    {
            Console.Write("單執行緒");
            list.ForEach(i => Console.Write(i + ", "));
            Console.WriteLine();
            Console.WriteLine("-完成-");
    }

    private static void Multi(List<int> list)
    {
            Console.Write("平行處理");
            Parallel.ForEach(list, q =>
            {
                Console.Write(q + ", ");
            });
            Console.WriteLine();
            Console.WriteLine("-完成-");
    }
}
```
    
* Task https://dotblogs.com.tw/JesperLai/2018/04/06/013332
* Lock https://dotblogs.com.tw/JesperLai/2018/04/06/010817
* Interlocked http://noteofisabella.blogspot.com/2019/03/c-threadlock.html
* Monitor https://dotblogs.com.tw/noncoder/2018/06/30/lock-Monitor
* Mutex http://noteofisabella.blogspot.com/2019/03/c-threadmutexsemaphore.html
* Semaphore https://dotblogs.com.tw/JesperLai/2018/04/06/011954

---
    
* HttpClient https://blog.darkthread.net/blog/httpclient-sigleton/
  => HttpClientFactory
* EntityFramework https://medium.com/sally-thinking/%E7%A8%8B%E5%BC%8F%E5%AD%B8%E7%BF%92%E4%B9%8B%E8%B7%AF-day28-c-ado-net-entity-framework-%E7%89%A9%E4%BB%B6%E9%97%9C%E8%81%AF%E5%B0%8D%E6%87%89-4b27943af679
    
---
    
* async, await https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/concepts/async/
    
---
    
* Custom Attributes https://dotblogs.com.tw/johnny/2015/07/31/csharp-custom-attributes
* dynamic https://iter01.com/154116.html
* ExpandoObject https://dotblogs.com.tw/Leo_CodeSpace/2020/01/05/115025
    
---
    
## SQL
    
* OFFSET 
* FETCH NEXT 
    
```
SELECT OrderID, CustomerID, EmployeeID, OrderDate
FROM dbo.Orders
ORDER BY OrderDate DESC
 OFFSET 0 ROWS -- 跳過幾行
 FETCH FIRST 50 ROWS ONLY; -- 抓取幾行
GO
```
    
* CTE ( Common Table Expression ) 暫存表
  * https://dotblogs.com.tw/dc690216/2010/02/02/13440
  * https://dotblogs.com.tw/wasichris/2016/11/03/151251
* MERGE https://shunnien.github.io/2018/04/19/sql-merge-into/
* OUTPUT 
* View(檢視表) 
* CREATE VIEW 
* ALTER VIEW 
* DROP VIEW 
* WITH CHECK OPTION 
* Index(索引) 
* 叢集索引(Clustered Indexes) link 
* 非叢集索引(Nonclustered Indexes) 
* 複合索引 
* PRIMARY KEY欄位的索引 
* UNIQUE 欄位的索引 
* 如何建立索引 
* 如何檢視執行計劃 
* 篩選索引(Filtered Index) 
* 資料行索引(Columnstore Index) 
* BEGIN/END 
* WHILE, BREAK, CONTINU, GOTO 
* IF/ELSE 
* WAITFOR 
* IIF , CHOOSE 
* 預存程序(Stored Procedures, SP) 
* 建立SP 
* 執行SP 
* SP參數 
* Sequence的建立及操作 
* 自訂函數 User-defined Functions, UDF 
* 純量值函數 
* 資料表值函數 
* CURSOR 指標 
* Trigger 
* Transaction 概念 
* commit, rollback 
* Lock, Dead Lock 
