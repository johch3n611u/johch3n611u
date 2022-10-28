using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnet
{
    public partial class _22GridView_BottonField : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ShowOrderList(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Order")
            {
                int index=Convert.ToInt32(e.CommandArgument);

                lblCar.Text += GridView1.Rows[index].Cells[1].Text+" 已經加入購物車<br />";
            }
           
        }
    }
}