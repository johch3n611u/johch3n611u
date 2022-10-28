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
    public partial class _31ADOnet_SqlDataReader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MySystemConnectionString1"].ConnectionString);
            SqlCommand Cmd = new SqlCommand("select * from products",Conn);

            SqlDataReader rd;
            Conn.Open();
            rd=Cmd.ExecuteReader();
            while (rd.Read())
            {
                Response.Write(rd["product_id"] +"-"+rd["product_name"]+"<br>");
            }

            Conn.Close();

        }
    }
}