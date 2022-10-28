using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace ASPnet
{
    public partial class _30ADOnet_SqlDataAdapter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MySystemConnectionString1"].ConnectionString );
            SqlDataAdapter da = new SqlDataAdapter("select * from products2",Conn);

            DataSet ds = new DataSet();
            da.Fill(ds, "products");

            GridView gv = new GridView();
            gv.ID = "test";
            gv.DataSource = ds;
            //gv.AutoGenerateColumns = true;
            


            //修改dataset裡的某筆資料
            ds.Tables["products"].Rows[7]["Product_Name"] = "111111Chef Topf薔薇系列不沾鍋 - 28公分炒鍋+28公分";
            ds.Tables["products"].Rows[7]["Product_price"] = 123456;

            //新增一筆資料進dataset
            DataRow dr = ds.Tables["products"].NewRow();
            dr[0] = "P011";
            dr[1] = "uuuuuuuuu";
            dr[2] = "P011.jpg";
            dr[3] = 123;
            dr[4] = 100;
            dr[5] = "sssssss";
            dr[6] = 1;

            ds.Tables["products"].Rows.Add(dr);

            //刪除DataSet裡的資料
            ds.Tables["products"].Rows[5].Delete();


            //gv.DataBind();

            //form1.Controls.Add(gv);

            try
            {
                SqlCommandBuilder myUpdate = new SqlCommandBuilder(da);
                da.Update(ds, "products");
            }
            catch(Exception ex)
            {
                Response.Write("沒有成功,原因"+ex.Message);
            }

        }
    }
}