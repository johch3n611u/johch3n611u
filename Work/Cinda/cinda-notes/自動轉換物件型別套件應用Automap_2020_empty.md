# 參考

<https://www.itread01.com/content/1545214335.html>

是一個讓 Data Transfer Object 資料傳輸物件與 Model 之間能夠自動完成物件與物件之間轉化的開源庫

---

```C#
public class DTO
{
    public string userName {set; get;}
    public string age {set; get;}
    public string job {set; get;}
}
public class Model
{
    public string userName {set; get;}
    public string age {set; get;}
    public string job {set; get;}
}
```

DTO 為直接對映資料庫中的資料，Model 為互動資料

## 通常的做法為：DTO 與 Model 之間可以這樣轉化，因為的物件的屬性都為string 型別

```C#
DTO.userName = Model.userName;
DTO.age = Model.age ;
DTO.job = Model.job ;
```

## 使用 AutoMapper 轉化：首先需要先為 DTO 與 Model 之間定義一個對映關係

```C#
Mapper.CreateMap<DTO, Model>();
DTO dtoData = GetdtoDataFromDB();
Model modelData = Mapper.Map<DTO, Model>(dtoData );
```

DTO 物件就被 AutoMapper 自動轉化成了Model物件，所以 modelData 中的 userName、age、job 的值即為 GetdtoDataFromDB（） 方法取出來的值。

> 需注意子物件對映

---

以上為舊版寫法

<https://dotblogs.com.tw/yc421206/2016/07/15/automapper_version_5_MapperConfiguration>

```C#
// Old
Mapper.Initialize(x => x.AddProfile<AS400MapSqlProfile>());←可以繼續用
Mapper.AddProfile<SqlMapperProfile>();←被拔掉
// or

Mapper.Initialize(config =>
{
    config.AddProfile<AS400MapSqlProfile>();
    config.AddProfile<SqlMapperProfile>();
});

// New
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<AS400MapSqlProfile>();
    cfg.AddProfile<SqlMapperProfile>();
});
```

---

## 變數前面加底線

<http://www.blueshop.com.tw/board/FUM20041006161839LRJ/BRD200607181003030C1.html>

前面加底線，通常是表示 private 的變數
