using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnet
{
    public partial class _12HomeWork3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //弄一副牌出來
            string[] poker = new string[52];
            for (int i = 0; i <= 51; i++)
                poker[i] = (i + 1).ToString();

            //測試
            for (int i = 0; i < poker.Length; i++)
                Response.Write("<img src='poker_img/" + poker[i] + ".gif' />");

            Response.Write("<hr />");
            ////////////////////////////////////////////
            //洗牌

            Random r = new Random();
            int t = 0;
            string tmp = "";
            for(int i=0;i<poker.Length;i++)
            {
                t = r.Next(52);
                tmp = poker[i];
                poker[i] = poker[t];
                poker[t] = tmp;
            }

            //測試
            for (int i = 0; i < poker.Length; i++)
                Response.Write("<img src='poker_img/" + poker[i] + ".gif' />");

            ////////////////////////////////////////////
            //發牌

            string p1 = "", p2 = "", p3 = "", p4 = "";
            for (int i = 0; i < poker.Length; i++)
            {
                switch (i%4)
                {
                    case 0:
                        p1 += "<img src='poker_img/" + poker[i] + ".gif' />";
                        break;
                    case 1:
                        p2 += "<img src='poker_img/" + poker[i] + ".gif' />";
                        break;
                    case 2:
                        p3 += "<img src='poker_img/" + poker[i] + ".gif' />";
                        break;
                    case 3:
                        p4 += "<img src='poker_img/" + poker[i] + ".gif' />";
                        break;
                }
            }
            Response.Write("<hr />");

            Response.Write("p1:"+p1+"p2:"+p2+"p3:"+p3+"p4:"+p4);
        }
    }
}