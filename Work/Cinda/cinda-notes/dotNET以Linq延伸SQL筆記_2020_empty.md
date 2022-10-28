# dotNET以Linq延伸SQL

## Left Join

```C#
var queryable =
 (
from t3 in Entities.SIGN_FORM_MAIN.AsNoTracking()
join t2 in Entities.PAM_AF_DISABLED.AsNoTracking() on  t3.SIGN_FORM_ID equals t2.SIGN_FORM_ID
join t1 in Entities.PAM_AF_DISABLED_DETAIL.AsNoTracking() on t2.ID equals t1.AF_DISABLED_ID into add from t1 in add.DefaultIfEmpty()
join t4 in Entities.FUNCTION_TYPE.AsNoTracking() on t1.SERVICE_NAME equals t4.ID into ft from t4 in ft.DefaultIfEmpty()
orderby t2.SIGN_FORM_ID descending
select new { t1, t2, t3, t4 }
)
.AsQueryable();
```

## EntityFunctions

```C#
var IF008_EMP_NO =
(from t1 in Repository.Entities.PAM_IF_RESIGN.AsNoTracking()
where EntityFunctions.DiffDays(DateTime.Now, t1.ACCOUNT_CLOSE_DATE) <= 7 &&
EntityFunctions.DiffDays(DateTime.Now, t1.ACCOUNT_CLOSE_DATE) > 0
group t1 by new
{
EMP_NO = t1.EMP_NO,
 } into t1
select new { t1 });
```

## Lamda

```C#
var data =
 Entities.PERSON_PERMISSION
 .Where(x => x.PERSON_ORG_CHANGE_ID ==
(int)i.Id)
.Select(x => x.ACCOUNT.FUNCTION_TYPE)
.ToList();
```

## groupby

<https://yangxinde.pixnet.net/blog/post/31357272>

## not in

<https://dotblogs.com.tw/dc690216/2009/09/13/10601>

```C#
(new int?[] { 1, 2 }).Contains(p.CategoryID)

(new int?[] { 9001, 9002, 9003, 9004, 9005, 9006, 9007, 9008, 9009, 2, 3, 4, 5, 7, 9 }).Contains(x.t2.FUNCTION_TYPE)
```

## IQueryable to String Array 做 not in 比對

```C#
/// 錯誤
string[] IF008_EMP_NO = (from t1 in Repository.Entities.PAM_IF_RESIGN.AsNoTracking()
                                where EntityFunctions.DiffDays(DateTime.Now, t1.ACCOUNT_CLOSE_DATE) <= 7 &&
                                       EntityFunctions.DiffDays(DateTime.Now, t1.ACCOUNT_CLOSE_DATE) > 0
                                group t1 by new
                                {
                                    EMP_NO = t1.EMP_NO,

                                } into t1
                                select new { t1 }).ToArray(); // 7 天內 PAM_IF_RESIGN 新增資料且 IF008 Distinct 確保唯一值 DBSET

// 用了鳥方法成功...

var IF008_EMP_NO = (from t1 in Repository.Entities.PAM_IF_RESIGN.AsNoTracking()
                                where EntityFunctions.DiffDays(DateTime.Now, t1.ACCOUNT_CLOSE_DATE) <= 7 &&
                                       EntityFunctions.DiffDays(DateTime.Now, t1.ACCOUNT_CLOSE_DATE) > 0
                                group t1 by t1.EMP_NO into EMP_NO
                                select new { EMP_NO } ).ToList(); // 7 天內 PAM_IF_RESIGN 新增資料且 IF008 Distinct 確保唯一值 DBSET

            string[] Items = new string[IF008_EMP_NO.Count];
            for (int i = 0; i < IF008_EMP_NO.Count; i++) { Items[i] = IF008_EMP_NO[i].EMP_NO.Key; }


            var SevenDays_List = (from t2 in Repository.Entities.ACCOUNT.AsNoTracking()
                                  where Items.Contains(t2.EMP_NO)
                                  select new { t2 }).ToList(); // 7 天內 Account 新增資料 Group Emp_No 只取一筆

```

<https://stackoverflow.com/questions/2176011/iqueryable-to-string-array/2176044>

## Linq Lambda groupby

```C#
List<Order> OrderList = viewModel.GroupBy(x => new {
                SoldTime = x.SoldTime.GetValueOrDefault().Date,
                GoodsName = x.GoodsName
            }).Select(x => new Order
            {
                GoodsName = x.Key.GoodsName,
                SoldTime = x.Key.SoldTime,
                Quantity = x.Sum(y => y.Quantity)
            }).ToList();
```

<https://dotblogs.com.tw/noncoder/2019/03/25/Lambda-GroupBy-Sum>

## LINQ 多表連接

<https://blog.csdn.net/lixiaoer757/article/details/80598966>

## DBSet DbContext / DataTable DataRow DataSet DataView / List Array Object / Class

DataTable 動態型別，資料量大會爆

DTO (Class/Model)

<https://whitecat2.pixnet.net/blog/post/62010016-%5B%E7%A8%8B%E5%BC%8F%E7%AD%86%E8%A8%98%5D%5Bc%23%5D%E7%94%A8datatable%E8%88%87dataset%E8%AE%80%E5%8F%96%E8%B3%87%E6%96%99%E8%A1%A8>

<https://codertw.com/%E5%89%8D%E7%AB%AF%E9%96%8B%E7%99%BC/221520/>

<https://ithelp.ithome.com.tw/articles/10196856>

## 搞懂 DbSet與上述 相關函式 Where add remove find...

<https://docs.microsoft.com/zh-tw/dotnet/framework/data/adonet/sql/linq/how-to-display-linq-to-sql-commands>

<https://docs.microsoft.com/zh-tw/dotnet/framework/data/adonet/sql/linq/how-to-directly-execute-sql-queries>

軍規資料庫是不下 pk 都要手動 inner join 的

<https://www.google.com/search?q=%E8%BB%8D%E8%A6%8F%E8%B3%87%E6%96%99%E5%BA%AB&rlz=1C1CHBF_zh-TWTW905TW905&oq=%E8%BB%8D%E8%A6%8F%E8%B3%87%E6%96%99%E5%BA%AB&aqs=chrome..69i57.542j0j7&sourceid=chrome&ie=UTF-8>

## SQL Like LINQ Contains

<https://kevintsengtw.blogspot.com/2014/04/linq-contains-arraylist.html>

## [SQL]將多筆資料合併為一筆顯示(FOR XML PATH)

<https://dotblogs.com.tw/kevinya/2012/06/01/72553>

## Database first / Model first / Code first

### Code first

<https://dotblogs.com.tw/supershowwei/2016/04/11/000015>

## DTO

## EF.Database.BeginTransaction

<https://docs.microsoft.com/zh-tw/ef/ef6/saving/transactions>
<https://programmium.wordpress.com/2017/07/17/ef-code-first-auto-increment-key/>

## 資料繫結 DataSet vs DTO (Data transfer object)

<https://www.google.com/search?q=DataSet+vs+DTO&rlz=1C1CHBF_zh-TWTW905TW905&oq=DataSet&aqs=chrome.1.69i59l3j0l2j69i60l3.641j0j7&sourceid=chrome&ie=UTF-8>

<https://dotblogs.com.tw/yc421206/2011/03/07/21706>

<https://whitecat2.pixnet.net/blog/post/62010016-%5B%E7%A8%8B%E5%BC%8F%E7%AD%86%E8%A8%98%5D%5Bc%23%5D%E7%94%A8datatable%E8%88%87dataset%E8%AE%80%E5%8F%96%E8%B3%87%E6%96%99%E8%A1%A8>

---

<https://ithelp.ithome.com.tw/articles/10186055>
