using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//00-2-1 using MVC1.Models
using MVC1.Models;

namespace MVC1
{
    public partial class Index2 : System.Web.UI.Page
    {
        //00-2-2 使用Entity建立DB物件
        dbProductEntities db = new dbProductEntities();

        void loadData()
        {
            GridView1.DataSource = db.tProduct.ToList();
            GridView1.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        { 
            //00-2-3 將讀出的資料餵給GridView
            if(!IsPostBack)
                loadData();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string fId = e.CommandArgument.ToString();

            if(e.CommandName=="Edit")
            {
                Response.Redirect("Edit.aspx?fId="+ fId);
            }
            else if(e.CommandName == "KillMe")
            {
                //取得要刪除的產品
                var product = db.tProduct.Where(m => m.fId == fId).FirstOrDefault();
                string fileName = product.fImg;//取得要刪除的產品的圖檔
                //刪除指定圖檔
                System.IO.File.Delete(Server.MapPath("images/" + fileName));

                db.tProduct.Remove(product);//刪除指定產品
                db.SaveChanges();//回寫結果
                loadData(); //呼叫loadData()餵目前資料給GridView
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}


//00-1 利用Entity Framework建立Model(DB First)
//00-1-1 建立dbProduct.mdb資料庫Model
//       在Models上按右鍵,選擇加入,新增項目,資料,ADO.NET實體資料模型,名稱輸入"ProductModel",按新增
//       來自資料庫的EF Designer
//       連接dbProduct.mdf資料庫,連線名稱不修改,按下一步按鈕
//       選擇Entity Framework 6.x, 按下一步按鈕
//       資料表勾選"tProuct", 按完成鈕
//       若跳出詢問方法按確定鈕
//00-1-2 在專案上按右鍵,建置

//00-2 撰寫Index2.aspx的後置程式碼
//00-2-1 using MVC1.Models
//00-2-2 使用Entity建立DB物件
//00-2-3 將讀出的資料餵給GridView
//00-2-4 撰寫GridView裡的按鈕被按下時所要做的事

//00-3 撰寫Create.aspx的後置程式碼
//00-3-1 using MVC1.Models
//00-3-2 撰寫"儲存"鈕被按下時要做的事
//00-3-3 使用Entity建立DB物件
//00-3-4 使用Entity Framework新增資料

//00-4 撰寫Edit.aspx的後置程式碼
//00-4-1 using MVC1.Models
//00-4-2 使用Entity建立DB物件
//00-4-3 讀出要修改的產品資料並放入表單欄位中
//00-4-4 撰寫"儲存"鈕被按下時要做的事
//00-4-5 使用Entity Framework更新資料