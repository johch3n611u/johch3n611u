using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace _03Model_ADOnet.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbStudentConnectionString"].ConnectionString);
        SqlCommand Cmd = new SqlCommand();

        //05-2-8 建立一般方法querySql()-可傳入SQL字串並傳回DataTable物件
        private DataTable querySql(string sql)
        {
            SqlDataAdapter adp = new SqlDataAdapter(sql, Conn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            return  ds.Tables[0];
        }

        //05-2-7 建立一般方法executeSql()-可傳入SQL字串來輯編資料表
        private void executeSql(string sql)
        {
            Conn.Open();
            Cmd.Connection = Conn;
            Cmd.CommandText = sql;
            Cmd.ExecuteNonQuery();
            Conn.Close();

           
        }
        //05-2-9 建立Index() Action回傳DataTable資料給View
        public ActionResult Index()
        {
            DataTable dt = querySql("Select * from tStudent");
            return View(dt);
        }

        //05-2-10 建立GET與POST Create Action
        public ActionResult Create()
        {
         
            return View();
        }

        [HttpPost]
        public ActionResult Create(string fStuId, string fName, string fEmail, string fScore)
        {
            string sql = "insert into tStudent values(@fStuId,@fName,@fEmail,@fScore)";
            Cmd.Parameters.AddWithValue("@fStuId", fStuId);
            Cmd.Parameters.AddWithValue("@fName", fName);
            Cmd.Parameters.AddWithValue("@fEmail", fEmail);
            Cmd.Parameters.AddWithValue("@fScore", fScore);

            executeSql(sql);

            return RedirectToAction("Index");
        }

        //05-2-11 建立POST Delete Action
        public ActionResult Delete(string id)
        {
            string sql = "delete from tstudent where fStuId=@fStuId";
            Cmd.Parameters.AddWithValue("@fStuId", id);

            executeSql(sql);

            return RedirectToAction("Index");
        }

    }
}

//05-2 ADO.net使用練習
//05-2-1 將dbStudent.mdb資料庫放入App_Data資料夾
//05-2-2 在Controllers資料夾上按右鍵,加入,控制器,選擇 MVC5Controller-Empty
//05-2-3 指定控制器名稱為HomeController,並開啟HomeController
//05-2-4 using System.Data、System.Data.SqlClient、System.Configuration
//05-2-5 加入連線設定在Web.config檔裡
//05-2-6 設定Connection與SqlCommand物件
//05-2-7 建立一般方法executeSql()-可傳入SQL字串來輯編資料表
//05-2-8 建立一般方法querySql()-可傳入SQL字串並傳回DataTable物件
//05-2-9 建立Index() Action回傳DataTable資料給View
//05-2-10 建立GET與POST Create Action
//05-2-11 建立POST Delete Action
//05-2-12 在public ActionResult Index()上按右鍵,新增檢視,建立Index View
//05-2-13 進行下列設定:
//        View name:Index
//        Template:Empty (Without model)
//        勾選Use a layout pages
//        按下Add
//05-2-14 撰寫Home/Index View的內容
