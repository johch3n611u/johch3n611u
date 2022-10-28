using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnet
{
    public partial class _10HomeWork1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //5.寫一顯示1~100整數中，不是5的倍數的程式
            for (int i = 1; i <= 100; i++)
            {
                if (i % 5 != 0)
                    Response.Write(i + " ");
            }

            Response.Write("<hr />");
            //6.計算1~1000中除了3倍數外所有數的總合。
            int sum = 0;
            for (int i = 1; i <= 1000; i++)
            {
                if (i % 3 != 0)
                    sum += i;  //sum=sum+i

            }
            Response.Write(sum);
            Response.Write("<hr />");

            //8.請利用回圈寫一九九乘法表
            for (int i = 2; i <= 9; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    Response.Write(i + "*" + j + "=" + i * j + "<br />");

                }
                Response.Write("<br />");
            }
        }

    }
}