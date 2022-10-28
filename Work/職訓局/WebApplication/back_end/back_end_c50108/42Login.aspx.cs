using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace ASPnet
{
    public partial class _42Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MySystemConnectionString1"].ConnectionString);
            SqlCommand Cmd = new SqlCommand("select * from members where Account=@account and pswd=hashbytes('sha2_256',@pswd)", Conn);
            Cmd.Parameters.AddWithValue("@account", txtAccount.Text);
            Cmd.Parameters.AddWithValue("@pswd", txtPswd.Text);
            SqlDataReader rd;
            Conn.Open();
            rd = Cmd.ExecuteReader();
            if(rd.Read())
            {
                Session["id"] = rd["Account"];
                Session["name"] = rd["Name"];
                Response.Redirect("41MainPage.aspx");
            }
            else
            {
                Response.Write("<script>alert('帳密錯誤!!')</script>");
            }

            Conn.Close();

        }
    }
}