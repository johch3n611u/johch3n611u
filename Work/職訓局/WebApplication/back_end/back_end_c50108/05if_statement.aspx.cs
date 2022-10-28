using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnet
{
    public partial class _05if_statement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int a = 4;

            if(a>=10)
            {
                Response.Write("a的值大於等於10");
            }
            else
                Response.Write("a的值小於10");
            ////////////////////////////////////////
            Response.Write("<hr>");

            
            int age = 16;
            //20歲以下免票, 20歲以上全票
            //if(  age>=20 )
            //    Response.Write("全票");
            //else
            //    Response.Write("免票");

            //0-6歲免票, 7-20歲半票, 21歲以上全票

            //if (age >= 0)
            //{
            //    if (age > 20)
            //        Response.Write("全票");

            //    if (age >= 7 && age <= 20)
            //        Response.Write("半票");

            //    if (age <= 6)
            //        Response.Write("免票");
            //}
            //else
            //    Response.Write("年齡輸入錯誤");

            if (age >= 0)
            {
                //搜尋式
                if (age > 20)
                    Response.Write("全票");
                else if (age <=6)
                    Response.Write("免票");
                else
                    Response.Write("半票");
            }
            else
                Response.Write("年齡輸入錯誤");

            ///////////////////////////////////////////////////
            Response.Write("<hr>");

            //判斷分數等第
            //90以上為優等
            //80-89以上為甲等
            //70-79以上為乙等
            //60-69以上為丙等
            //60以下為丁等

            int score = 85;
            //寫法1-各自獨立
            //if (score >= 90 && score <= 100)
            //    Response.Write("優等");
            //if (score >= 80 && score < 90)
            //    Response.Write("甲等");
            //if (score >= 70 && score < 80)
            //    Response.Write("乙等");
            //if (score >= 60 && score < 70)
            //    Response.Write("丙等");
            //if (score < 60)
            //    Response.Write("丁等");

            //寫法2-搜尋式
            if (score >= 90)
                Response.Write("優等");
            else if (score >= 80)
                Response.Write("甲等");
            else if (score >= 70)
                Response.Write("乙等");
            else if (score >= 60)
                Response.Write("丙等");
            else
                Response.Write("丁等");
        }
    }
}