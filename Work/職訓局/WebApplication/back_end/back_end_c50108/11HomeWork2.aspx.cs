using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnet
{
    public partial class _11HomeWork2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //質數判斷
            int n = 130;
            bool flag = false;

            for (int i = 2; i < n; i++)
            {
                if(n%i==0)
                {
                    flag = true;
                    break;
                }
            }

            if (flag)
                Response.Write(n+"不是質數");
            else
                Response.Write(n + "是質數");

            Response.Write("<hr />");

            //最大公因數
            int x = 5589712, y = 652214587;
            int xx = x, yy = y, z = 0;  //xx永遠當被除數, yy永遠當除數, z放餘數

            while(xx%yy!=0)
            {
                z = xx % yy;
                xx = yy;
                yy = z;
            }
            Response.Write(x+"與"+y+"的最大公因數為"+yy);

            Response.Write("<hr />");

            //迴文判斷
            int k = 12321;
            int r = 0, q = 0, kk = k;
            string strResult = "";

            for(int i=0;i<9;i++)
            {
                r = kk % 10;
                q = kk / 10;
                strResult += r;

                if (q == 0)
                    break;
                else
                    kk = q;
            }


            if (k.ToString()==strResult)
                Response.Write(k + "是迴文");
            else
                Response.Write(k + "不是迴文");
        }
    }
}