using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _04Model_EF.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //04-1-3 建立一般方法ShowAryDesc()-整數陣列遞減排序
        public string ShowArrayDesc()
        {
            int[] score = { 78, 99, 20, 90, 66, 100, 75, 45, 26 };

            //SQL DML
            //Select * from score order by s desc
            //Linq查詢運算式
            var result = from s in score
                         orderby s descending
                         select s;


            //Linq擴充方法
            //用Lambda表示法撰寫
            //var result = score.OrderByDescending(s => s);  //大到小(遞減)
            //var result = score.OrderBy(s => s);  //小到大(遞增)


            string show ="";
            foreach(var s in result)
            {
                show += s + ",";
            }

            return show+ "總和:"+result.Sum();//使用Linq的Sum方法進行加總
        }

        //04-1-9 在HomeController中建立一般方法LoginMember()-整數陣列遞增排序
        public string LoginMember(string uid, string pwd)
        {
            Member[] members = new Member[] 
            {
                new Member{ UId="tom",Pwd="123",Name="湯姆"},
                new Member{ UId="jobs",Pwd="456",Name="賈伯斯"},
                new Member{ UId="mary",Pwd="789",Name="瑪莉"}
            };

            //SQL DML
            //Select * from members where UId=uid and Pwd=pwd
            //Linq查詢運算式
            //var result = (from m in members
            //              where m.UId == uid && m.Pwd == pwd
            //              select m).FirstOrDefault();

            string show = "";
            //Linq擴充方法
            //用Lambda表示法撰寫
            var result = members.Where(m => m.UId == uid && m.Pwd == pwd).FirstOrDefault();

            if (result == null)
                show = "帳號或密碼有誤!!";
            else
                show = result.Name + "歡迎光臨";

            return show;



        }



    }
}

//04-1 Linq練習
//04-1-1 在Controllers資料夾上按右鍵,加入,控制器,選擇 MVC5Controller-Empty
//04-1-2 指定控制器名稱為HomeController,並開啟HomeController
//04-1-3 建立一般方法ShowAryDesc()-整數陣列遞減排序
//04-1-4 執行並測試 http://localhost:53468/Home/ShowAryDesc (port可能不同)
//04-1-7 在Controllers資料夾上按右鍵,選擇加入,新增項目,程式碼,選擇類別,名稱鍵入Member.cs
//04-1-8 在Member class中輸入下列欄位
//04-1-9 在HomeController中建立一般方法LoginMember()-整數陣列遞增排序
//04-1-10 執行並測試 http://localhost:53468/Home/LoginMember?uid=tom&pwd=123 (port可能不同)

//04-2 Entity FrameWork
//04-2-1 建立NorthWind.mdb資料庫Model
//       在Models上按右鍵,選擇加入,新增項目,資料,ADO.NET實體資料模型
//       來自資料庫的EF Designer
//       連接NorthWind.mdf資料庫,連線名稱不修改,按下一步按鈕
//       選擇Entity Framework 6.x, 按下一步按鈕
//       資料表"全選", 按完成鈕
//       若跳出詢問方法按確定鈕
//04-2-3 在專案上按右鍵,建置
//04-2-4 在Controllers資料夾上按右鍵,加入,控制器,選擇 MVC5Controller-Empty
//04-2-5 指定控制器名稱為LinqController,並開啟LinqController
//04-2-6 using _04EF.Models
//04-2-7 於LinqController建立DB物件
//04-2-8 建立一般方法ShowEmployee()-查詢所有員工記錄
//04-2-9 執行並測試 http://localhost:53468/Linq/ShowEmployee (port可能不同)
//04-2-10 建立一般方法Index() Action-查詢所有員工記錄
//04-2-11 進行下列設定:建立View
//        View name:Index
//        Template:List
//        Model class:員工(_04Ef.Models)
//        Data context class:NorthwindEntities(_04Ef.Models)
//        勾選Use a layout pages
//        按下Add
//04-2-12執行並測試 http://localhost:53468/Linq/Index (port可能不同)

//04-3 在Model建立驗證功能
//04-3-1 建立dbStudent.mdb資料庫Model
//       在Models上按右鍵,選擇加入,新增項目,資料,ADO.NET實體資料模型
//       來自資料庫的EF Designer
//       連接dbStudent.mdf資料庫,連線名稱不修改,按下一步按鈕
//       選擇Entity Framework 6.x, 按下一步按鈕
//       資料表"全選", 按完成鈕
//       若跳出詢問方法按確定鈕
//04-3-2 在專案上按右鍵,建置
//04-3-3 展開dbStudentModel.edmx, 再展開dbStudentModel.tt, 找到tStudent.cs並開啟此檔
//04-3-4 using System.ComponentModel 及 System.ComponentModel.DataAnnotations
//04-3-5 在原程式中加入驗證功能標籤 
//04-3-6 在Controllers資料夾上按右鍵,加入,控制器,選擇 MVC5Controller-Empty
//04-3-7 指定控制器名稱為VilidationController,並開啟VilidationController
//04-3-8 using _04EF.Models
//04-3-9 於VilidationController建立DB物件,並撰寫Index、Create、Delete的Action
//04-3-10 在public ActionResult Index()上按右鍵,新增檢視,建立Index View
//04-3-11 進行下列設定:
//        View name:Index
//        Template:List
//        Model class:tStudent(_04Ef.Models)
//        Data context class:dbStudentEntities(_04Ef.Models)
//        勾選Use a layout pages
//        按下Add
//04-3-12 修改英文文字為中文與表格中的內容
//04-3-13 執行並測試
//04-3-14 在public ActionResult Create()上按右鍵,新增檢視,建立Create View
//04-3-15 進行下列設定:
//        View name:Create
//        Template:Empty(without model)
//        Model class:tStudent(_04Ef.Models)
//        Data context class:dbStudentEntities(_04Ef.Models)
//        勾選Use a layout pages
//        按下Add
//04-3-17 執行並測試