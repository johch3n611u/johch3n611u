using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnet
{
    public partial class Homework45 : System.Web.UI.Page
    {    
        protected void btnAll_Click(object sender, EventArgs e)
        {
            for(int i=0;i<ltbInterest.Items.Count;i++)
            {
                ltbInterest2.Items.Add(ltbInterest.Items[i].Text);
            }
            ltbInterest.Items.Clear();
            //發生click事件時，執行迴圈次數為0到ltbInterest控制項的item數，每次遞增1。迴圈內容為，增加位置[i]的ltbInterest控制項的值至ltbInterest2控制項中，迴圈完後清空ltbInterest控制項。
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ltbInterest2.Items.Count; i++)
            {
                ltbInterest.Items.Add(ltbInterest2.Items[i].Text);
            }
            ltbInterest2.Items.Clear();
            //發生click事件時，執行迴圈次數為0到ltbInterest2控制項的Items數，每次遞增1。迴圈內容為，增加位置[i]的ltbInterest2控制項的值至ltbInterest控制項中，迴圈完後清空ltbInterest2控制項。
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ltbInterest.Items.Count; i++)
            {
                if (ltbInterest.Items[i].Selected)
                {
                    ltbInterest2.Items.Add(ltbInterest.Items[i].Text);
                    ltbInterest.Items.RemoveAt(i);
                }
            }
            //發生click事件時，執行迴圈次數為0到ltbInterest控制項的items數，每次遞增1。迴圈內容為如果滑鼠選擇的ltbInterest控制項Items位置為當次迴圈次數時，ltbInterest控制項增加當次迴圈次數位置之值至ltbInterest2控制項中後，ltbInterest移除當次迴圈次數位置之值。
        }
        protected void btnNo_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ltbInterest2.Items.Count; i++)
            {
                if (ltbInterest2.Items[i].Selected)
                {
                    ltbInterest.Items.Add(ltbInterest2.Items[i].Text);
                    ltbInterest2.Items.RemoveAt(i);
                }
            }
            //發生click事件時，執行迴圈次數為0到ltbInterest2控制項的items數，每次遞增1。迴圈內容為如果滑鼠選擇的ltbInterest2控制項Items位置為當次迴圈次數時，ltbInterest2控制項增加當次迴圈次數位置之值至ltbInterest控制項中後，ltbInterest2移除當次迴圈次數位置之值。
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                Response.Write("表單已成功送到伺服器");
            }
            //this 關鍵字:指的是類別的目前執行個體，在此表示Homework45 page頁面。
            //https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/this 。 https://zhuanlan.zhihu.com/p/23804247
            //IsValid 關鍵字:取得布林值指出網頁驗證是否成功。
            //https://docs.microsoft.com/zh-tw/dotnet/api/system.web.ui.page.isvalid?view=netframework-4.7.2
        }

        //階段性作業五
        //ASP.net WebForm - Validation Controls綜合應用
        //承作業四，請利用各控制項與驗證器，完成以下要求之功能。
        //1.請利用合適的控制項，驗證帳號、密碼、身分證字號、姓名、生日、學歷等欄位是否有填，若未填須提示錯誤。
        //2.請利用合適的控制項，驗證身分證字號、E-Mail、生日等欄位之格式是否正確，若不正確須提示錯誤。
        //3.請利用合適的控制項，驗證生日欄位之輸入區間是否介於1912/1/1至填表單當日之間，若不正確須提示錯誤。
        //4.請利用合適的控制項，驗證兩個密碼欄位是否輸入相同的值，若值不相同須提示錯誤。
        //5.請利用合適的控制項，驗證興趣欄位是否選擇三個(含) 以上，若不足三個須提示錯誤。
        //6.請利用合適的控制項，驗證身分證字號是否合法，若不合法須提示錯誤。
        //7.上述所有欄位必須全部通過驗證，才能將表單資料送至(Post) 伺服器端，反之不得送出資料。

        protected void Page_Load(object sender, EventArgs e)
        {
            ranForBirthday.MaximumValue = DateTime.Now.ToString("d");
            //在pageload事件時讀取當時DateTime資料並轉為字串代號d，並指定於forbirthday的maximumvalue屬性的值。
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBox1.Items.Count; i++) 
            {
                ListBox2.Items.Add(ListBox1.Items[i].Text);
            }
            ListBox1.Items.Clear();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBox2.Items.Count; i++)
            {
                ListBox1.Items.Add(ListBox2.Items[i].Text);
            }
            ListBox2.Items.Clear();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBox1.Items.Count; i++)
            {
                if (ListBox1.Items[i].Selected)
                {
                    ListBox2.Items.Add(ListBox1.Items[i].Text);
                    ListBox1.Items.RemoveAt(i);
                }
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBox2.Items.Count; i++)
            {
                if (ListBox2.Items[i].Selected)
                {
                    ListBox1.Items.Add(ListBox2.Items[i].Text);
                    ListBox2.Items.RemoveAt(i);
                }
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string id = TextBox4.Text; 
            //指定textbox4的text字串值至變數id中

            string[] eng = {"A", "B", "C", "D", "E", "F", "G", "H", "J", "K",
                "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "X", "Y", "W",
                "Z", "I", "O" };
            //身分證規則-> https://zh.wikipedia.org/wiki/%E4%B8%AD%E8%8F%AF%E6%B0%91%E5%9C%8B%E5%9C%8B%E6%B0%91%E8%BA%AB%E5%88%86%E8%AD%89
            //指定A - Z個變數至eng陣列中

            int intEng = 0;
            //指定一個intEng整數變數為0

            for (int i = 0; i < eng.Length; i++)
            {
                if (eng[i] == id.Substring(0, 1).ToUpper())
                {
                    intEng = i + 10;
                    break;
                }
            }
            //設置一迴圈，目的:為將身分證規則字母比對輸入控制項字母後並將相對應數字傳入inteng中。
            //Substring 方法:擷取指定位置之內容字串(從此位置開始,取n位)。 https://docs.microsoft.com/zh-tw/dotnet/api/system.string.substring?view=netframework-4.7.2
            //ToUpper 方法:轉換為大寫。

            int n1 = intEng / 10;
            //目的:利用int關鍵字特性除法只會留下整數，除與十->inteng十位數。
            int n2 = intEng % 10;
            //目的:利用%運算子特性，取除與十之餘數->取inteng個位數。

            int[] a = new int[9];
            //宣告一個a陣列，初始化 擁有0-9十個位址。

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = Convert.ToInt16(id.Substring(i + 1, 1));
            }
            //設置一迴圈，目的:分別塞入身分證剩餘9個數字。
            //Convert 類別:將資料類型轉換為其他資料。 https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/types/how-to-convert-a-string-to-a-number 。 https://docs.microsoft.com/zh-tw/dotnet/api/system.convert?view=netframework-4.7.2
            //ToInt16 方法:轉換為16位元帶正負號整數。 https://docs.microsoft.com/zh-tw/dotnet/api/system.convert.toint16?view=netframework-4.7.2

            int sum = 0;
            for (int i = 0; i < 8; i++)
            {
                sum += a[i] * (8 - i);
            }
            //驗證規則為A123456789，A=10 => 1 0 2 3 4 5 6 7 8 9 => 共十一碼 ， 而驗證代碼為 1 9 8 7 6 5 4 3 2 1 1 => 共十一碼 。 第一碼身分字號與第一碼驗證代碼相乘依序至第十一碼，並將結果相加除與十，若為整數則驗證該組號碼有效。
            //設置一迴圈，目的:扣除身分證代碼第一與第二碼與第十一碼，將身分證代碼與驗證代碼，從第三碼相乘至第十碼。
            //x += y 等於 x = x+y

            int n = 0;
            n = n1 + n2 * 9 + sum + a[8];
            //將運算後的數值相加，指定為變數n。           

            if (n % 10 == 0)
                args.IsValid = true;
            else
                args.IsValid = false;
            //目的:當驗證通過時指定
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                Response.Write("表單已成功送到伺服器");
            }
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ListBox2.Items.Count < 3)
                args.IsValid = false;
            else
                args.IsValid = true;
        }
        //C#  物件導向程式設計「事件的型別引數 (EventArgs) 」的用法。
        //(object source, ServerValidateEventArgs args)
        //1.第一個為 object 型別參數 sender  (代表觸發此事件的來源物件)
        //2.第二個為 EventArgs 型別參數 e    (包含與事件相關的一些資訊)
        //https://dotblogs.com.tw/wesley0917/2013/04/20/101975
        //https://ithelp.ithome.com.tw/articles/10198771?sc=pt
        //args 事件處理用參數 ??? 看不太懂待補 
        //http://rexmen.pixnet.net/blog/post/26432036-asp.net%E8%87%AA%E5%AE%9A%E7%BE%A9%E7%9A%84%E9%A9%97%E8%AD%89%E9%A0%85customvalidator 。 
        //http://welkingunther.pixnet.net/blog/post/27959816-%28asp.net%29%E9%A9%97%E8%AD%89%E8%BC%B8%E5%85%A5%E6%8E%A7%E5%88%B6%E9%A0%85%E4%B9%8B%E7%9B%B8%E9%97%9C%E5%BF%83%E5%BE%97 。
        //https://blog.csdn.net/david_520042/article/details/6026213 。
        //結論:必須趕快看一下w3c.asp的內容 https://www.w3schools.com/asp/default.asp
    }
}