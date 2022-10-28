using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//00-4-1 using MVC1.Models
using MVC1.Models;

namespace MVC1
{
    public partial class Edit : System.Web.UI.Page
    {
        //00-4-2 使用Entity建立DB物件
        dbProductEntities db = new dbProductEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            //00-4-3 讀出要修改的產品資料並放入表單欄位中
            if (!IsPostBack)
            {
                //由url取得產品代號
                string fId = Request.QueryString["fId"].ToString();

                //Lambda
                var product = db.tProduct.Where(m => m.fId == fId).FirstOrDefault();
                txtId.Text = product.fId;
                txtName.Text = product.fName;
                txtPrice.Text = product.fPrice.ToString();
                lblShowImg.Text = "<img src='images/" + product.fImg + "' width='200' />";
            }
        }

        //按 [儲存] 鈕時執行
        protected void btnSave_Click(object sender, EventArgs e)
        {

            //dbProductEntities db = new dbProductEntities();
            string fileName, fId;
            fId = txtId.Text;
            var product = db.tProduct.Where(m => m.fId == fId).FirstOrDefault();
            fileName = product.fImg;

            //圖片上傳儲存
            if (FileUpload1.HasFile)
            {
                FileUpload1.SaveAs(Server.MapPath("images/" + FileUpload1.FileName));
                fileName = FileUpload1.FileName;
            }

            //00-3-4 使用Entity Framework新增資料
            product.fName = txtName.Text;
            product.fPrice = Convert.ToDecimal(txtPrice.Text);
            product.fImg = fileName;//如果使用者沒上傳就用原來的檔名
            db.SaveChanges();//回寫結果

            Response.Redirect("Index2.aspx");



        }
    }
}