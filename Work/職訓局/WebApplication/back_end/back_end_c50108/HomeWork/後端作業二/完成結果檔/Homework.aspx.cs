using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnet
{
    public partial class Homework : System.Web.UI.Page
    {
        protected void Page_Load3(object sender, EventArgs e)
        {

            // variable type 變數型別

            //string 字串
            //bool 布林值

            //byte 8bit 正整數
            //short 16bit 整數
            //int 32bit 整數
            //long 64bit 整數

            //float 32bit 浮點數
            //double 64bit 浮點數

            //more...

            //C#2010:https://www.tutorialspoint.com/csharp/csharp_data_types.htm

            //1-1.宣告變數a為整數，值為42，宣告變數b為浮點數，值2.5，將兩值分別做加、減、乘、除及取餘數之運算，並輸出其結果。

            Response.Write("<style>table{border: 3px solid; width:800px; height:50px;line-height:50px;} tr{border: 1px solid;}td{border: 1px solid;} h4{text-align:center;}</style>");
            Response.Write("<h3>1-1.宣告變數a為整數，值為42，宣告變數b為浮點數，值2.5，將兩值分別做加、減、乘、除及取餘數之運算，並輸出其結果。</h3>");
            float floA = 42f, floB = 2.5f, floC, floD, floE, floF, floG;  //因為涉及整數與浮點數運算固直接宣告a為浮點數,位數運算不超過32bit所以選用float。 // 因為不知道運算式是否可以寫在輸出所以給了一堆變數
            {
                floC = floA + floB; floD = floA - floB; floE = floA * floB; floF = floA / floB; floG = floA % floA;
                Response.Write("<table><tr><td><h4>a+b=" + floC + "</h4></td><td><h4>a-b=" + floD + "</h4></td><td><h4>a*b=" + floE + "</h4></td><td><h4>a/b=" + floF + "</h4></td><td><h4>a%b=" + floG + "</h4></td></tr></table>");
            }
            Response.Write("<hr/>");

            //1-2.撰寫一個將攝氏溫度轉換為華氏溫度的程式，攝氏溫度的值直接在程式中給定即可。(華氏＝攝氏*9/5+32)。
            Response.Write("<h3>1-2.撰寫一個將攝氏溫度轉換為華氏溫度的程式，攝氏溫度的值直接在程式中給定即可。(華氏＝攝氏*9/5+32)。</h3>");


            float floCelsius, floFahrenheit = 1;
            floCelsius = 33.5f;
            floFahrenheit = (floCelsius * 9 / 5 + 32);
            Response.Write("攝氏" + floCelsius + "度等於華氏" + floFahrenheit + "度");
            Response.Write("<hr/>");


            //1-3.設有兩個變數X與Y，其值為任何整數，試寫在不另宣告其他變數的情況下，交換X與Y的值的程式。 (例X = 3, Y = 5, 執行完您的程式後X = 5, Y = 3
            Response.Write("<h3>1-3.設有兩個變數X與Y，其值為任何整數，試寫在不另宣告其他變數的情況下，交換X與Y的值的程式。 (例X = 3, Y = 5, 執行完您的程式後X = 5, Y = 3</h3>");

            long lonX = 3, lonY = 5;
            Response.Write("X=" + lonX + ",Y=" + lonY);
            lonX = lonX + lonY;
            lonY = lonX - lonY;
            lonX = lonX - lonY;
            Response.Write("經過邏輯演算X=" + lonX + ",Y=" + lonY);
            Response.Write("<hr/>");


            //1-4.請利用switch敘述句，分別試寫判斷成績等第之程式。90分以上為優等，80~89為甲等，70~79為乙等，60~69為丙等，其餘為丁等(不可另外搭配if 敘述句)。
            Response.Write("<h3>1-4.請利用switch敘述句，分別試寫判斷成績等第之程式。90分以上為優等，80~89為甲等，70~79為乙等，60~69為丙等，其餘為丁等(不可另外搭配if 敘述句)。</h3>");

            int intScore = 85;
            Response.Write("分數為" + intScore + "。");
            int intLevel = intScore / 10;
            switch (intLevel)
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

            Response.Write("<hr/>");

            //1-5.寫一顯示1~100整數中，不是5的倍數的程式。
            Response.Write("<h3>1-5.寫一顯示1~100整數中，不是5的倍數的程式。</h3>");
            int intOnehundrednumbers;

            //for ( 計數器初始值 ; 計數器結束值 ; 計數器增量值 ) 
            //if( 條件運算式 ) { 條件成立時執行 } else { 不成立時執行 }
            Response.Write("1~100中");
            for (intOnehundrednumbers = 1; intOnehundrednumbers < 101; intOnehundrednumbers++) { if (intOnehundrednumbers % 5 != 0) { Response.Write(intOnehundrednumbers + "、"); }; };
            Response.Write("都不是5的倍數。");
            Response.Write("<hr/>");

            //1-6.計算1~1000中除了3倍數外所有數的總合。
            Response.Write("<h3>1-6.計算1~1000中除了3倍數外所有數的總合。</h3>");

            int intOnethousandnumbers, intTriplepile = 0;
            for (intOnethousandnumbers = 1; intOnethousandnumbers < 1001; intOnethousandnumbers++) { if (intOnethousandnumbers % 3 != 0) { intTriplepile = intTriplepile + intOnethousandnumbers; }; };
            Response.Write("1~1000除了除了3倍數外所有數的總合為" + intTriplepile);
            Response.Write("<hr/>");

            //1-7.請利用回圈顯示出下方圖形。(不可以使用巢狀回圈)
            //    *
            //    **
            //    ***
            //    ****
            //    *****
            Response.Write("<h3>1-7.請利用回圈顯示出下方圖形。(不可以使用巢狀回圈)</h3>");




            string intH = "";
            for (int intI = 1; intI < 6; intI++)
            {
                intH += "*";  //intH=intH+"*"
                Response.Write(intH);
                Response.Write("<br />");
            }
            Response.Write("<hr/>");

            //1-8.請利用回圈寫一九九乘法表。
            Response.Write("<h3>1-8.請利用回圈寫一九九乘法表。</h3>");

            Response.Write("<table>");
            for (int intJ = 1, intK, intL; intJ < 10; intJ++) { Response.Write("<tr>"); for (intK = 1; intK < 10; intK++) { Response.Write("<td>"); intL = intJ * intK; Response.Write(intJ + "*" + intK + "=" + intL); Response.Write("</td>"); } Response.Write("</tr>"); };
            Response.Write("</table>");
            Response.Write("<hr/>");

            //2-1.質數判斷(必須用回圈)請給定一個整數變數值，判斷其是否為質數，若是，請在螢幕顯示「○○是質數」，若不是，請在螢幕顯示「○○不是質數」。如例變數值為13，即顯示「13是質數」。(ps.質數的定義為除了1與本身之外，沒有其他的因數存在)
            // while (條件){條件成立時重複時執行至條件不成立};
            Response.Write("<h3>2-1.質數判斷(必須用回圈)請給定一個整數變數值，判斷其是否為質數，若是，請在螢幕顯示「○○是質數」，若不是，請在螢幕顯示「○○不是質數」。如例變數值為13，即顯示「13是質數」。(ps.質數的定義為除了1與本身之外，沒有其他的因數存在)</h3>");


            int intM = 13, intN = 14, intDiscriminate, intDiscriminateresult = 1; //帶入兩常數M,N測試，intDiscrimirate為一個不大於M,N常數之變數用以當被除數，inDiscriminateresule為判別變數。

            for (intDiscriminate = 2; intDiscriminate < intM; intDiscriminate++) //被除數變數起始大於2到小於常數，可測試是否除了1與本身沒有其他因數。
            {
                if (intM % intDiscriminate == 0)  //A%B顯示值為A/B之餘數。A==B為判別式A值為B值。此行邏輯為當常數與被除數之餘數為0時，常數被除數整除，常數就會擁有除了1與本身之其他因數。
                {
                    intDiscriminateresult = intDiscriminateresult + 1; //此行為判別式，當常數擁有除了1與本身之外因數，則判別變數+1。
                }
                if (intDiscriminateresult > 2) break; //原判別變數值為1，當判別式成立時判別常數+1，但判別迴圈會繼續做到大於判別常數才結束， 若超過2就不會是質數了，所以不需要繼續計算，則跳出檢查迴圈。
            }

            if (intDiscriminateresult == 2)
            { Response.Write(intM + "是質數。"); }
            else
            { Response.Write(intM + "不是質數。"); }

            ///////

            for (intDiscriminate = 2; intDiscriminate <= intN; intDiscriminate++)
            {
                if (intN % intDiscriminate == 0)
                {
                    intDiscriminateresult = intDiscriminateresult + 1;
                }
                if (intDiscriminateresult > 2) break;
            }

            if (intDiscriminateresult == 2)
            { Response.Write(intN + "是質數。"); }
            else
            { Response.Write(intN + "不是質數。"); }

            Response.Write("<hr/>");

            //2-2.求最大公因數(必須用回圈)請給定兩個整數變數值，求其兩數之最大公因數，並在螢幕顯示「○○與○○之最大公因數為○○」。如例變數值為12及18，即顯示「 12及18 之最大公因數為6」（ps.最大公因數的定義為某幾個整數所共同擁有的最大因數）
            //greatest common divisor，某幾個整數所共同擁有的最大因數，為最大公因數(gcd)。least common multiple，某幾個數的公倍數中最小的公倍數，為最小公倍數(lcm)。最大公因數與最小公倍數關係為(a*b)=gcd(a.b)*lcm(a.b)。 
            //Euclidean algorithm，輾轉相除法，又稱歐幾里得算法。基於如下原理：兩個整數的最大公因數等於其中較小的數和兩數的差的最大公因數。
            //ex.(18與12)之最大公因數求法:兩數的差為6，兩數較小的為12，(18與12)的最大公因數為(12與6)的最大公因數，(12與6)的差為6，兩數較小的為6，(18與12)、(12與6)的最大公因數為(6與6)的最大公因數為6。
            //邏輯18/12=1...6、12/6=2...0，gcd(18.12)=6。
            Response.Write("<h3>2-2.求最大公因數(必須用回圈)請給定兩個整數變數值，求其兩數之最大公因數，並在螢幕顯示「○○與○○之最大公因數為○○」。如例變數值為12及18，即顯示「 12及18 之最大公因數為6」（ps.最大公因數的定義為某幾個整數所共同擁有的最大因數）</h3>");


            int intO = 88888, intP = 22, intQ, intR, intS = 0;  // intO , intP 為示範變數初始值，intQ 被除數變數 , intR 除數變數為輾轉相除法需求變數容器，intQ為兩常數相除之餘數變數。

            intQ = intO; intR = intP;


            while (intQ % intR != 0)            //如果滿足餘數變數不等於0時，重複執行以下運算。
            {
                intS = intQ % intR;          //邏輯18/12=1...6、12/6=2...0，gcd(18.12)=6。

                intQ = intR;                 //當餘數為0則求出gcd

                intR = intS;

            };


            Response.Write("intS" + intS + " = intQ" + intQ + " % intR" + intR + "<br/>");

            Response.Write("intQ" + intQ + " = intR" + intR + "<br/>");

            Response.Write("intR" + intR + " = intS" + intS + "<br/>");



            Response.Write("gcd(" + intO + "." + intP + ")=" + intR);
            Response.Write("<hr />");



            //2-3.迴文判斷(必須用回圈)請給定一個九位數以內的整數變數值，判斷其是否為迴文，若是，請在螢幕顯示「○○○○是迴文」，若不是，請在螢幕顯示「○○不是迴文」。如例變數值為12321，即顯示「12321是迴文」。(ps.迴文的定義為一個數字，由左唸至右及由右唸至左時，皆一模一樣)
            Response.Write("<h3>2-3.迴文判斷(必須用回圈)請給定一個九位數以內的整數變數值，判斷其是否為迴文，若是，請在螢幕顯示「○○○○是迴文」，若不是，請在螢幕顯示「○○不是迴文」。如例變數值為12321，即顯示「12321是迴文」。(ps.迴文的定義為一個數字，由左唸至右及由右唸至左時，皆一模一樣)</h3>");


            int intT = 12321, intU, intV, intW;

            intU = intT;

            Response.Write("桌上放著數字" + intT + "。<br/>");


            string strBox = ""; // 字串盒子

            for (int i = 1; i < 10; i++) //題目限制為9位數內所以做九次。
            {
                intV = intU % 10; // 取個位數值。

                Response.Write("第" + i + "次運算拿取" + intV + "放入盒子。");

                strBox += intV; // 將數字存於字串盒子避免運算。X+=Y 為將Y的值加X的值結果存回X。

                Response.Write("strBox盒子裡面有" + strBox + "。");

                intW = intU / 10; // 將整數變數浮點向右移一位。C#運算並不會保留除後的小數。

                Response.Write("剩下" + intW + "。");

                Response.Write("<br/>");

                if (intW == 0) //當intW變數為0時停止運算。
                    break;
                else
                    intU = intW; //將intW裡的變數轉回去intU並進行下一次運算。            
            }

            if (intT.ToString() == strBox) //將intT轉為字串，並查看是否等於strBox裡的字串。
                Response.Write("人工運算結果" + intT + "是迴文");//如果是的話判斷為迴文。
            else
                Response.Write("人工運算結果" + intT + "不是迴文");


            Response.Write("<hr />");



            //3-1.試寫一撲克牌發牌程式，將52張牌發給四玩家，每家共13張，並利用poker_img資料夾裡的素材來顯示撲克牌。(ps.每次發牌均需為不同結果)
            Response.Write("<h3>3-1.試寫一撲克牌發牌程式，將52張牌發給四玩家，每家共13張，並利用poker_img資料夾裡的素材來顯示撲克牌。(ps.每次發牌均需為不同結果)</h3>");
            Response.Write("<hr />");
            Response.Write("<h3>1.創建牌組</h3>");
            string[] strPoker = new string[52]; //陣列牌盒內容為html:img串所以用string宣告，new運算子用以建立陣列並初始化其預設值。https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/arrays/single-dimensional-arrays
            for (int i = 0; i <= 51; i++) { strPoker[i] = (i + 1).ToString(); } //ToString運算子用以數字轉字串，利用for迴圈將值丟入陣列，用以下列邏輯運算叫出牌gif檔案。
            for (int i = 0; i < strPoker.Length; i++) { Response.Write("<img src='poker_img/" + strPoker[i] + ".gif'/>"); }//Length運算子用以取得strPoker的總數量。顯示出所有圖檔。
                                                                                                                           //Response.Write("<img src='poker_img/" + poker[i] + ".gif' />");
                                                                                                                           //xx錯誤寫法詳請參考下鏈結{ Response.Write("<img src='../D:/Desk/Little-donkey/MyWebApplication/ASPnet/HomeWork/後端作業三/poker_img/1.gif'/>"); }
                                                                                                                           //http://www.blueshop.com.tw/board/FUM20041006161839LRJ/BRD20091213033633MWJ.html
            Response.Write("<hr />");

            //////////////////////////////////////////// 洗牌
            Response.Write("<h3>2.切牌洗牌</h3>");
            Random ranShufflecards = new Random(Guid.NewGuid().GetHashCode()); //宣告一亂數物件，new運算子初始化。http://ksjolin.pixnet.net/blog/post/150115680-%E3%80%90c%23%E3%80%91%E4%BA%82%E6%95%B8%E7%94%A2%E7%94%9F%E6%B3%95
            int intCut = 0; //用於放置亂數
            string strPorkerbox = ""; //用於放置牌圖字串
            for (int intTimes = 0; intTimes < strPoker.Length; intTimes++) //洗52次
            {
                int intRealtimes = intTimes + 1;

                intCut = ranShufflecards.Next(52); //Next傳回一個int數值，其範圍介於 0-2,147,483,6747之間。(52)為0<x<52之間的亂數，並放置於intCut。

                Response.Write("第" + intRealtimes + "次洗牌，intCut=" + intCut + "、ranShufflecards.Next(52)=" + ranShufflecards.Next(52) + "、");

                strPorkerbox = strPoker[intTimes]; //將inTimes計數器指定之strPoker牌字串，放置於strPorkerbox。

                Response.Write("strPorkerbox=" + strPorkerbox + "、strPoker[intTimes]=" + strPoker[intTimes] + "、");

                strPoker[intTimes] = strPoker[intCut]; //將亂數指定之strPoke牌字串，放置於inTimes計數器指定之strPoker牌字串。

                Response.Write("strPoker[intTimes]=" + strPoker[intTimes] + "、strPoker[intCut]=" + strPoker[intCut] + "、");

                strPoker[intCut] = strPorkerbox; //將inTimes計數器指定之strPoker牌字串，轉移到intCut指定之strPoker牌字串。

                Response.Write("strPoker[intCut]=" + strPoker[intCut] + "、strPorkerbox =" + strPorkerbox + "。");

                Response.Write("<br/>");
            }
            Response.Write("<hr />");
            //////////////////////////////////////////// 發牌

            Response.Write("<h3>3.莊家發牌</h3>");
            string strPlay1 = "", strPlay2 = "", strPlay3 = "", strPlay4 = "";
            for (int i = 0; i < strPoker.Length; i++)
            {
                switch (i % 4)
                {
                    case 0:
                        strPlay1 += "<img src='poker_img/" + strPoker[i] + ".gif'/>";
                        break;
                    case 1:
                        strPlay2 += "<img src='poker_img/" + strPoker[i] + ".gif'/>";
                        break;
                    case 2:
                        strPlay3 += "<img src='poker_img/" + strPoker[i] + ".gif'/>";
                        break;
                    case 3:
                        strPlay4 += "<img src='poker_img/" + strPoker[i] + ".gif'/>";
                        break;
                }
            }


            Response.Write("<h1>p1</h1>:" + strPlay1 + "<br/>");
            Response.Write("<h1>p2</h1>:" + strPlay2 + "<br/>");
            Response.Write("<h1>p3</h1>:" + strPlay3 + "<br/>");
            Response.Write("<h1>p4</h1>:" + strPlay4 + "<br/>");
            Response.Write("<hr />");
        }












    }
}


