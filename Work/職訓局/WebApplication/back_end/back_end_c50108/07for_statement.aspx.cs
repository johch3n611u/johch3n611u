using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnet
{
    public partial class for_statement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            for(int i=1;i<=6;i++)
            {
                Response.Write("<h"+i+">Welcome My Homepage!!</h"+i+">");
            }

            ////////////////////////////////////////////////////////////////
            Response.Write("<hr>");

            //用for回圈做1加到1000,輸出其結果

            int a = 0;
            for (int i =1 ; i <= 1000; i++)
            {
                a += i; //a=a+i
            }
            Response.Write("1加到1000的結果為："+a);

            ////////////////////////////////////////////////////////////////
            Response.Write("<hr>");

            //用for回圈顯示出下列圖形
            //*
            //**
            //***
            //****
            //*****

            //巢狀
            //for(int i=1;i<=5;i++)
            //{
            //    for (int j = 1; j <= i; j++)
            //        Response.Write("*");

            //    Response.Write("<br />");
            //}

            //用一個回圈完成
            string result = "";
            for (int i = 1; i <= 10; i++)
            {
                result += "*";  //result=result+"*"
                Response.Write(result);
                Response.Write("<br />");
            }
            ////////////////////////////////////////////////////////////////
            Response.Write("<hr>");

            //陣列

            int[] arrNumber = new int[5];

            arrNumber[0] = 6;
            arrNumber[1] = 7;
            arrNumber[2] = 8;
            arrNumber[3] = 9;
            arrNumber[4] = 10;

            //Response.Write(arrNumber[2]);  //輸出8
            
            //for(int i=0;i<arrNumber.Length;i++)
            //{
            //    Response.Write(arrNumber[i]);
            //}

            Response.Write("<hr>");


            string[] arrRainbow = { "紅", "橙", "黃", "綠", "藍", "靛", "紫" };
            string[] arrColor = { "red", "orange", "yellow", "green", "blue", "indigo", "violet" };

            for (int i = 0; i < arrRainbow.Length; i++)
            {
                Response.Write("<span style='color:"+ arrColor[i] + "'>"+ arrRainbow[i] + "</span>");
            }

        }
    }
}