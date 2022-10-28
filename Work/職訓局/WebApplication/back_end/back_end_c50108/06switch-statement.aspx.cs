using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnet
{
    public partial class _06switch_statement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int a = 3;  

            switch (a)
            {
                case 1:
                    Response.Write("a的值為1");
                    break;
                case 2:
                    Response.Write("a的值為2");
                    break;
                case 3:
                    Response.Write("a的值為3");
                    break;
                case 4:
                    Response.Write("a的值為4");
                    break;
                case 5:
                    Response.Write("a的值為5");
                    break;
            }
            ///////////////////////////////////
            Response.Write("<hr>");
            string Color = "黃1";

            switch (Color)
            {
                case "黃":
                    Response.Write("黃色");
                    break;
                case "綠":
                    Response.Write("綠色");
                    break;
                case "紅":
                    Response.Write("紅色");
                    break;
                default:
                    Response.Write("這不是黃綠紅");
                    break;

            }

            ///////////////////////////////////
            Response.Write("<hr>");


            //判斷分數等第(只能用switch不可以加任何if)
            //90以上為優等
            //80-89以上為甲等
            //70-79以上為乙等
            //60-69以上為丙等
            //60以下為丁等

            int score = 85;
            switch (score/10)
            {
                case 10:
                case 9:
                    Response.Write("優等");
                    break;
                case 8:
                    Response.Write("甲等");
                    break;
                case 7:
                    Response.Write("乙等");
                    break;
                case 6:
                    Response.Write("丙等");
                    break;
                default:
                    Response.Write("丁等");
                    break;
            }
        }
    }
}