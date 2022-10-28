using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//00-3-1 using MVC1.Models
using MVC1.Models;

namespace MVC1
{
    public partial class Create : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        //按下[儲存]按鈕
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            //00-3-2 撰寫"儲存"鈕被按下時要做的事
            try
            {
                string fileName = "";
                //圖片上傳儲存
                if (FileUpload1.HasFile)
                {
                    FileUpload1.SaveAs(Server.MapPath("images/" + FileUpload1.FileName));
                    fileName = FileUpload1.FileName;
                }

                //新增資料進資料庫
                //00-3-3 使用Entity建立DB物件
                dbProductEntities db = new dbProductEntities();

                //00-3-4 使用Entity Framework新增資料
                tProduct product = new tProduct();
                product.fId = txtId.Text;
                product.fName = txtName.Text;
                product.fPrice = Convert.ToDecimal(txtPrice.Text);
                product.fImg = fileName;
                db.tProduct.Add(product);
                db.SaveChanges();

                Response.Redirect("Index2.aspx");

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}