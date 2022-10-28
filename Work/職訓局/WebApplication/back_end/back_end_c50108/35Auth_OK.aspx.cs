using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnet
{
    public partial class _35Auth_OK : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string account = Request.QueryString["account"].ToString();


            SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MySystemConnectionString1"].ConnectionString);
            SqlCommand Cmd = new SqlCommand("select * from members where account=@account", Conn);
            Cmd.Parameters.AddWithValue("@account", account);

            SqlDataReader rd;
            Conn.Open();
            rd = Cmd.ExecuteReader();
            if(rd.Read())
            {
                rd.Close();
                Cmd.Dispose();
                SqlCommand Cmd2 = new SqlCommand("update members set IsAuth=1 where account=@account", Conn);
                Cmd2.Parameters.AddWithValue("@account", account);
                Cmd2.ExecuteNonQuery();
                Response.Write("<script>alert('恭禧您完成會員認證') </script>");
            }

            Conn.Close();

        }
    }
}