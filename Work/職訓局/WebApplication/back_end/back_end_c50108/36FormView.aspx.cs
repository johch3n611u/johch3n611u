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
    public partial class _36FormView : System.Web.UI.Page
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MySystemConnectionString1"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (FormView1.CurrentMode == FormViewMode.Insert)
            {
                SqlCommand Cmd = new SqlCommand("select dbo.fnCheckMemberAccount(@account)", Conn);
                string account = ((TextBox)FormView1.FindControl("txtAccount")).Text;
                Cmd.Parameters.AddWithValue("@account", account);

                SqlDataReader rd;
                Conn.Open();
                rd = Cmd.ExecuteReader();

                rd.Read();
                if (rd[0].ToString() == "0")
                    args.IsValid = true;
                else
                    args.IsValid = false;

                Conn.Close();
            }
        }

        protected void FormView1_ModeChanged(object sender, EventArgs e)
        {
            //if (FormView1.CurrentMode == FormViewMode.Insert)
            //{
            //    FormView1.DataBind();
            //    SqlCommand Cmd = new SqlCommand("select * from Edu order by EduLevel_Code desc", Conn);
            //    DropDownList EduLevel = (DropDownList)FormView1.FindControl("ddlEduLevel");
            //    SqlDataReader rd;
            //    Conn.Open();
            //    rd = Cmd.ExecuteReader();
            //    ListItem item;
            //    while (rd.Read())
            //    {
            //        item = new ListItem(rd["EduLevel"].ToString(), rd["EduLevel_Code"].ToString());
            //        EduLevel.Items.Add(item);
            //    }

            //    Conn.Close();
            //    //ddlEduLevel.DataBind();

            //}
        }

        
    }
}