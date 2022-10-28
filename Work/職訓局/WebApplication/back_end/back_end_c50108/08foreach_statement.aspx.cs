using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnet
{
    public partial class _08foreach_statement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] arrRainbow = { "紅", "橙", "黃", "綠", "藍", "靛", "紫" };
            string[] arrColor = { "red", "orange", "yellow", "green", "blue", "indigo", "violet" };

            int i= 0;
            foreach ( string a in arrRainbow)
            {
                //Response.Write(a);
                Response.Write("<span style='color:" + arrColor[i] + "'>" + a + "</span>");
                i++;
            }
        }
    }
}