using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ASPnet
{
    /// <summary>
    /// _36GetMemberPhoto 的摘要描述
    /// </summary>
    public class _36GetMemberPhoto : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MySystemConnectionString1"].ConnectionString);
            SqlCommand Cmd = new SqlCommand("select Photo from members where account=@account", Conn);
            Cmd.Parameters.AddWithValue("@account", context.Request.QueryString["account"]);

            SqlDataReader rd;
            Conn.Open();
            rd = Cmd.ExecuteReader();
            if (rd.Read())
            {
                context.Response.BinaryWrite((byte[])rd["Photo"]);
            }

            Conn.Close();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}