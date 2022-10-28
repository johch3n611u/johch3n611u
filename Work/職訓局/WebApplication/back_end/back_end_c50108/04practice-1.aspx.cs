using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnet
{
    public partial class _04practice_1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            /********************************************************
             * 宣告變數a為整數，值為42，宣告變數b為浮點數，值2.5，  *
             *將兩值分別做加、減、乘、除及取餘數之運算，並輸出其結果*
             ********************************************************/
            int a = 42;
            float b = 2.5f;
            Response.Write(a + "+" + b + "=" + (a + b));
            Response.Write("<br />");
            Response.Write(a + "-" + b + "=" + (a - b));
            Response.Write("<br />");
            Response.Write(a + "*" + b + "=" + a * b);
            Response.Write("<br />");
            Response.Write(a + "/" + b + "=" + a / b);
            Response.Write("<br />");
            Response.Write(a + "%" + b + "=" + a % b);
            Response.Write("<hr />");

            /*****************************************
             * 撰寫一個將攝氏溫度轉換為華氏溫度的程式*
             * 攝氏溫度的值直接在程式中給定即可      *
             *(華氏＝攝氏 * 9 / 5 + 32)              *
             *****************************************/
            float C = 33.5f;
            Response.Write("攝氏" + C + "度等於華氏" + (C * 9 / 5 + 32) + "度");
            Response.Write("<hr />");

            /***********************************
            * 設有兩個變數X與Y，其值為任何整數 *
            * 試寫交換X與Y的值的程式           *
            *(例X=3,Y=5,執行完您的程式後X=5,Y=3*
            ************************************/

            //int X = 969, Y = 524;

            //int tmp = 0;
            //tmp = Y;
            //Y = X;
            //X = tmp;
            //Response.Write("X=" + X + ", Y=" + Y);


            /************************************
            * 設有兩個變數X與Y，其值為任何整數 *
            * 試寫在不另宣告其他變數的情況下   *
            * 交換X與Y的值的程式               *
            *(例X=3,Y=5,執行完您的程式後X=5,Y=3*
            ************************************/

            int X = 578798797, Y = 38789678;
            X = X ^ Y;  //X=8
            Y = X ^ Y;  //Y=8-3=5
            X = X ^ Y;  //X=8-5=3
        
            Response.Write("X=" + X + ", Y=" + Y);

        }
    }
}