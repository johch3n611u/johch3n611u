using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnet
{
    public partial class _40GGetAuthImg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(TextBox2.Text== Session["Number"].ToString())
            {
                Label1.Text = "驗證碼正確";

            }
            else
                Label1.Text = "驗證碼不正確";
        }

     
    }
}