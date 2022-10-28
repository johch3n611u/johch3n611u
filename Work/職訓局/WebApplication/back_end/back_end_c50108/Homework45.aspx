<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Homework45.aspx.cs" Inherits="ASPnet.Homework45" %>
<%-- 一些細節感覺老師教太快沒提到可能要自己去W3C看ASP.NET或看書了。
     所以優先解析這兩個作業然後可以先從W3C ASP先看到某個程度再去補齊W3C HTML CSS JAVASCRIPT或行有餘力同步進行。
     http://www.kangting.tw/2007/10/aspnet-page.html

     AutoEventWireup
     https://blog.csdn.net/ydm19891101/article/details/50546389
     http://www.blueshop.com.tw/board/show.asp?subcde=BRD20080104173749I2K
     https://www.c-sharpcorner.com/article/Asp-Net-page-life-cycle/
     CodeBehind
     https://www.google.com/search?q=CodeBehind&rlz=1C1GCEU_zh-TWTW835TW836&oq=CodeBehind&aqs=chrome..69i57j0l5.1152j0j4&sourceid=chrome&ie=UTF-8
     Inherits
     https://codertw.com/%E5%89%8D%E7%AB%AF%E9%96%8B%E7%99%BC/221370/
     https://baike.baidu.com/item/%E6%B4%BE%E7%94%9F%E7%B1%BB#1
--%>



    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <%-- 在xml檔案中定義命名空間
         http://shaocian.blogspot.com/2013/02/xml-name-space.html
         http://www.w3school.com.cn/tags/tag_prop_xmlns.asp 
         https://zh.wikipedia.org/wiki/%E5%91%BD%E5%90%8D%E7%A9%BA%E9%97%B4       
    --%>

    <head runat="server">
    <%-- http://www.cnblogs.com/volnet/archive/2009/07/25/essensial-of-runat-server-in-aspnet.html 
    --%>

        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <%-- http://www.wibibi.com/info.php?tid=416 
             https://developer.mozilla.org/zh-TW/docs/Web_%E9%96%8B%E7%99%BC/Historical_artifacts_to_avoid 
        --%>

        <title></title>
        <style> 
            
            /* 區別 display : block or inline */
            /* https://www.cnblogs.com/KeithWang/p/3139517.html */
           
            .a1 {background-color:darkgrey;}
            .a2 {background-color:black; border:dashed}  
            .a3 {background-color:dimgray;}
            .a4 {background-color:yellow;}
            .a5 {background-color:red;}
            .a6 {background-color:blue;}
            .a7 {background-color:blueviolet ;}
            .a8 {background-color:chartreuse ;}
            .a9 {background-color:chocolate ;}
            .a0 {background-color:cornflowerblue ;}
            
        </style>
    </head>
    <body class="a1">
        <form id="form1" runat="server" class="a2" >
            <%-- https://www.w3schools.com/tags/tag_form.asp 
                 http://www.w3school.com.cn/tags/tag_form.asp
                 創建表單用於顯示數據或向伺服器傳輸數據。
                 可以包含多個表單元數。<input><textarea><button><select><option><fieldset><label>
                 https://blog.csdn.net/ithomer/article/details/8080912
                 https://www.jianshu.com/p/246ad0659e2a
                 id class name 差別 name 可與後端互動
            --%>

            <%-- 階段性作業四 
                 ASP.net Web Form - Standard Controls表單介面設計與排版
                 	請利用各標準控制項，達到以下要求，完成如圖示之參考畫面。
                 1.	請以TextBox控制項實做帳號、密碼、姓名、身分證字號、生日、E-Mail等欄位，並設定最適合以上欄位的屬性。
                 2.	請以RadioButtonList控制項實做性別欄位，其item自訂。
                 3.	請以DropDownList控制項實做學歷欄位，其item自訂。
                 4.	請以ListBox控制項實做興趣欄位，其item自訂。
                 5.	興趣需用兩個ListBox控制項置於左右，並以四個按鈕分別實做「全選」、「全不選」、「單選一個」、「取消一個」等功能。
                 6.	須有「送出資料」及「重設表單」兩個功能按鈕。
                 7.	上述欄位均須具備，可另自己添增。
            --%>

            <div class="a3">
                <table class="a4">
                    <caption class="a5">會員資料</caption>
                    <tr class="a6">
                        <td class="a7">帳號：</td>
                        <td class="a8">
                            <asp:TextBox ID="txtAccount" runat="server" cssclass="a9" ></asp:TextBox>
                            <%-- 正規用法好像是cssclass但不知為何直接餵class好像也會吃，只是提示字元不會出現。
                                 TextBox 控制項:創建用戶可輸入文本的文本框。屬性參考:http://www.w3school.com.cn/aspnet/control_textbox.asp
                            --%>

                        </td>
                    </tr>
                    <tr class="a0">
                        <td class="a4">密碼：</td>
                        <td class="a5">
                            <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" placeholder="8-12碼" MaxLength="12" class="a6"></asp:TextBox>
                            <%-- TextMode 屬性:定義行為模式(單行、多行、密碼)。
                                 placeholder 屬性:字段內提示字。HTML5新屬性 http://sky940811.pixnet.net/blog/post/217819573-%E3%80%90%E6%95%99%E5%AD%B8%E3%80%91%E4%BD%BF%E7%94%A8html5%E6%96%B0%E5%B1%AC%E6%80%A7placeholder-%E5%9C%A8%3Cinput%3E%E7%9A%84%E6%96%87
                                 MaxLength 屬性:允許最大字符數。
                            --%>

                        </td>
                    </tr>
                    <tr class="a7" >
                        <td class="a8">密碼確認：</td>
                        <td class="a9">
                            <asp:TextBox ID="txtPwd2" runat="server" TextMode="Password" placeholder="請再輸入一次密碼" class="a0"></asp:TextBox>                   
                        </td>
                    </tr>
                    <tr class="a4">
                        <td class="a5">身分證字號：</td>
                        <td >
                            <asp:TextBox ID="txtID" runat="server" placeholder="A123456789" MaxLength="10" class="a6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="a7">
                        <td class="a8">姓名：</td>
                        <td class="a9">
                            <asp:TextBox ID="txtName" runat="server" class="a0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="a4">
                        <td class="a5">生日：</td>
                        <td class="a6">
                            <asp:TextBox ID="txtBirthday" runat="server" placeholder="1990-01-12" class="a7"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="a8">
                        <td class="a9">E-mail：</td>
                        <td class="a0">
                            <asp:TextBox ID="txtEmail" runat="server" placeholder="abc@abc.com" class="a4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="a5">
                        <td class="a6">性別：</td>
                        <td class="a7">
                            <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal" class="a8">
                            <%-- RadioButtonList 控制項:創建單選按鈕組。http://www.w3school.com.cn/aspnet/control_radiobuttonlist.asp
                                 RepeatDirection 屬性:定義選單水平排列或垂直。
                            --%>
                                <asp:ListItem Text="男" Selected="True" class="a9"></asp:ListItem>
                                <%-- ListItem 控制項:創建列表中的項目。http://www.w3school.com.cn/aspnet/control_listitem.asp
                                     Text 屬性:字段內顯示的文本。
                                     Selected 屬性:定義項目是否為預選。
                                --%>
                                <asp:ListItem Text="女" class="a0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="a4">
                        <td class="a5">學歷：</td>
                        <td class="a6">
                            <asp:DropDownList ID="ddlEduLevel" runat="server" class="a7">
                            <%-- DropDownList 控制項:創建下拉式列表。http://www.w3school.com.cn/aspnet/control_dropdownlist.asp
                            --%>

                                <asp:ListItem Text="請選擇" class="a8"></asp:ListItem>
                                <asp:ListItem Text="國小" class="a9"></asp:ListItem>
                                <asp:ListItem Text="國中" class="a0"></asp:ListItem>
                                <asp:ListItem Text="高中" class="a4"></asp:ListItem>
                                <asp:ListItem Text="大學" class="a5"></asp:ListItem>
                                <asp:ListItem Text="研究所以上" class="a6"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="a7">
                        <td class="a8">興趣：</td>
                        <td clas="9">
                            <table class="a0">
                                <tr class="a4">
                                    <td rowspan="4" class="a5">
                                        <asp:ListBox ID="ltbInterest" runat="server" Width="100" Height="150" class="a6">
                                        <%-- ListBox 控制項:創建單或多選的下拉式列表。http://www.w3school.com.cn/aspnet/control_listbox.asp
                                        --%>

                                            <asp:ListItem Text="爬山" class="a7"></asp:ListItem>
                                            <asp:ListItem Text="踏青" class="a8"></asp:ListItem>
                                            <asp:ListItem Text="看雲" class="a9"></asp:ListItem>
                                            <asp:ListItem Text="健行" class="a0"></asp:ListItem>
                                            <asp:ListItem Text="聽音樂" class="a4"></asp:ListItem>
                                            <asp:ListItem Text="上網" class="a5"></asp:ListItem>
                                            <asp:ListItem Text="遊泳" class="a6"></asp:ListItem>
                                            <asp:ListItem Text="賺錢" class="a7"></asp:ListItem>
                                            <asp:ListItem Text="唱歌" class="a8"></asp:ListItem>
                                        </asp:ListBox>
                                    </td>
                                    <td class="a9">
                                        <asp:Button ID="btnAll" runat="server" Text=">>" OnClick="btnAll_Click" CausesValidation="false" class="a0"/>
                                        <%-- Button 控制項:創建提交或命令按鈕。http://www.w3school.com.cn/aspnet/control_button.asp
                                              OnClick 屬性:在教學中顯示為OnClientClick 不知為何OnClick也通...??跟上方Cssclass相同問題，
                                                           當按鈕被點擊時運行伺服器此ID之後端腳本。
                                              CausesValidation 屬性:定義當按鈕被點擊時是否驗證(需傳值到伺服器在後端腳本進行運算)內容。
                                        --%>

                                    </td>
                                    <td rowspan="4" class="a4">
                                        <asp:ListBox ID="ltbInterest2" runat="server" Width="100" Height="150" class="a5"></asp:ListBox>
                                    </td>
                                </tr>
                                <tr class="a6">
                                    <td class="a7">
                                        <asp:Button ID="btnYes" runat="server" Text=">" OnClick="btnYes_Click" CausesValidation="false" class="a8"/>
                                    </td>
                                </tr>
                                <tr class="a9">
                                    <td class="a0">
                                        <asp:Button ID="btnNo" runat="server" Text="<" OnClick="btnNo_Click" CausesValidation="false" class="a4"/>
                                    </td>
                                </tr>
                                <tr class="a5">
                                    <td class="a6">
                                        <asp:Button ID="btnCancel" runat="server" Text="<<" OnClick="btnCancel_Click" CausesValidation="false" class="a7" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="a8">
                        <td colspan="2" class="a9">
                            <asp:Button ID="Button1" runat="server" Text="確定送出" OnClick="Button1_Click" class="a0"/>
                            <input id="Reset1" type="reset" value="重設" class="a4" />
                            <%-- input 標籤:創建可進行輸入數據的字段。https://www.w3schools.com/tags/tag_input.asp / http://www.w3school.com.cn/tags/tag_input.asp
                                 type 屬性:定義input的類型。reset 值:清除表單中的所有數據。http://www.w3school.com.cn/tags/att_input_type.asp
                                 value 屬性:定義input預設值。http://www.w3school.com.cn/tags/att_input_value.asp
                            --%>

                        </td>
                    </tr>
                </table>
            </div>

            <%-- 階段性作業五
                 ASP.net WebForm - Validation Controls綜合應用
                 	承作業四，請利用各控制項與驗證器，完成以下要求之功能。
                 1.	請利用合適的控制項，驗證帳號、密碼、身分證字號、姓名、生日、學歷等欄位是否有填，若未填須提示錯誤。
                 2.	請利用合適的控制項，驗證身分證字號、E-Mail、生日等欄位之格式是否正確，若不正確須提示錯誤。
                 3.	請利用合適的控制項，驗證生日欄位之輸入區間是否介於1912/1/1至填表單當日之間，若不正確須提示錯誤。
                 4.	請利用合適的控制項，驗證兩個密碼欄位是否輸入相同的值，若值不相同須提示錯誤。
                 5.	請利用合適的控制項，驗證興趣欄位是否選擇三個(含)以上，若不足三個須提示錯誤。
                 6.	請利用合適的控制項，驗證身分證字號是否合法，若不合法須提示錯誤。
                 7.	上述所有欄位必須全部通過驗證，才能將表單資料送至(Post)伺服器端，反之不得送出資料。
            --%>

                <div style="background-color:white">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="10" ForeColor="Red" DisplayMode="BulletList" ShowMessageBox="true" />
            <%-- ValidationSummary 驗證摘要控制項:驗證失敗時顯示摘要。http://www.w3school.com.cn/aspnet/control_validationsummary.asp
                 DisplayMode 屬性:如何顯示摘要。BulletList 值:小黑原點符號排序。
                 ShowMessageBox 屬性:是否跳出錯誤提示框。
                                               
            --%>
            <table id="tbMember">
                <caption>會員資料</caption>
                <%-- caption 標籤:定義表格標題，必須緊隨<table>。http://www.w3school.com.cn/tags/tag_caption.asp                               
                --%>

                <%-- HTML5 Input Types added several new input types: color/date/datetime-local/email/month/number/range/search/tel/time/url/week。
                     最大的避免輸入錯誤格式的可能性。https://www.w3schools.com/html/html_form_input_types.asp
                --%>

                <tr>
                    <td>帳號：</td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBox1" Display="Dynamic" runat="server" ErrorMessage="姓名為必填欄位" Text="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>
                        <%-- RequiredFieldValidator 必填字段驗證器控制項:使影響的輸入控制項成為必填字段。http://www.w3school.com.cn/aspnet/control_reqfieldvalidator.asp 。 https://zh.wikipedia.org/zh-tw/%E6%AD%A3%E5%88%99%E8%A1%A8%E8%BE%BE%E5%BC%8F 。
                             ControlToValidate 屬性:需要驗證的控制項之id。
                             Display 屬性:此控制項的顯示方式。Dynamic 值:動態的在驗證失敗時，添加驗證消息位置到頁面。
                             ErrorMessage 屬性:驗證失敗時在ValidationSummary控制項中顯示的文本，如無添加則使用Text 屬性之文本。
                             Text:驗證失敗時顯示的文本。
                        --%>

                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="TextBox1" ValidationExpression="[ABC][A-Za-z0-9]{4}" runat="server" ErrorMessage="(格式有誤)" ForeColor="Red" Font-Size="10pt"></asp:RegularExpressionValidator>
                        <%-- RegularExpressionValidator 正規表達式驗證器控制項:使影響的輸入控制項成為必填字段。http://www.w3school.com.cn/aspnet/control_regularexpvalidator.asp 。 https://zh.wikipedia.org/zh-tw/%E6%AD%A3%E5%88%99%E8%A1%A8%E8%BE%BE%E5%BC%8F 。
                             ValidationExpression 屬性:規定控制項之正規表達式。https://dotblogs.com.tw/wesley0917/2010/12/16/20153
                        --%>

                    </td>
                </tr>
                <tr>
                    <td>密碼：</td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" placeholder="8-12碼" MaxLength="12"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="TextBox2" runat="server" ErrorMessage="密碼為必填欄位" Text="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="TextBox2" ValidationExpression="\S{8,12}" runat="server" ErrorMessage="(密碼不可含有空白)" ForeColor="Red" Font-Size="10pt"></asp:RegularExpressionValidator>
                     </td>
                </tr>
                <tr>
                    <td>密碼確認：</td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" TextMode="Password" placeholder="請再輸入一次密碼"></asp:TextBox>
                      <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="TextBox3" runat="server" ErrorMessage="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>--%>
                          <asp:CompareValidator ID="CompareValidator3" runat="server" Operator="Equal" ControlToCompare="TextBox3"  ControlToValidate="TextBox2" ErrorMessage="(兩次密碼輸入不相同)" ForeColor="Red" Font-Size="10pt"></asp:CompareValidator>
                          <%-- CompareValidator 比較驗證器控制項:用於將輸入控制項的值與其他控制項比較。http://www.w3school.com.cn/aspnet/control_comparevalidator.asp
                               Operator 屬性:要執行的比較類型。Equal 值:相等。
                               ControlToCompare 屬性:要與被驗證控制項比較的輸入控制項id。
                               ControlToValidate 屬性:被驗證控制項id。 
                          --%>

                    </td>
                </tr>
                 <tr>
                    <td>身分證字號：</td>
                    <td>
                        <asp:TextBox ID="TextBox4" runat="server" placeholder="A123456789" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" ControlToValidate="TextBox4" runat="server" ErrorMessage="身分證為必填欄位"  ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" ControlToValidate="TextBox4" ValidationExpression="[A-Za-z](1|2)\d{8}" runat="server" ErrorMessage="(格式有誤)" ForeColor="Red" Font-Size="10pt"></asp:RegularExpressionValidator>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="(不合法)" ForeColor="Red" Font-Size="10pt" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
                        <%-- CustomValidator 自定義驗證器控制項:可對輸入控制項執行自定義驗證方式。http://www.w3school.com.cn/aspnet/control_customvalidator.asp
                             OnServerValidate 屬性:定義被執行驗證之伺服器函式名稱。
                        --%>

                    </td>
                </tr>
                <tr>
                    <td>姓名：</td>
                    <td>
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="TextBox5" runat="server" ErrorMessage="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                    <td>生日：</td>
                    <td>
                         <asp:TextBox ID="TextBox6" runat="server" placeholder="1990-01-12"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="TextBox6" Display="Dynamic" runat="server" ErrorMessage="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="CompareValidator1" Display="Dynamic" runat="server" Operator="DataTypeCheck" Type="Date" ControlToValidate="TextBox6" ErrorMessage="(格式錯誤)" ForeColor="Red" Font-Size="10pt"></asp:CompareValidator>
                         <%-- DataTypeCheck 值:比較數據類型。http://www.w3school.com.cn/aspnet/control_comparevalidator.asp
                              Type 屬性:定義比較值的數據類型。Date 值:日期資料。https://n.sfs.tw/content/index/10309
                              MaximumValue 屬性以直接指定於後端pageload事件發生時:ranForBirthday.MaximumValue = DateTime.Now.ToString("d");
                         --%>

                         <asp:RangeValidator ID="ranForBirthday" runat="server" Type="Date" MinimumValue="1912/1/1" ControlToValidate="TextBox6" ErrorMessage="(超出範圍)" ForeColor="Red" Font-Size="10pt"></asp:RangeValidator>
                         <%-- RangeValidator 範圍驗證器控制項:檢測輸入控制項的值是否介於兩值之間。可以對不同類型資料進行比對。
                              Type 屬性:定義比較值的數據類型。Date 值:日期資料。https://n.sfs.tw/content/index/10309
                              MinimumValue 屬性:規定輸入控制項的值最小範圍。
                        --%>
                    
                    </td>
                </tr>
                <tr>
                    <td>E-mail：</td>
                    <td>
                         <asp:TextBox ID="TextBox7" runat="server" placeholder="abc@abc.com"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="TextBox7" ValidationExpression="([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)" runat="server" ErrorMessage="(格式有誤)" ForeColor="Red" Font-Size="10pt"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>性別：</td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="男" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="女"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>學歷：</td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem Text="請選擇"></asp:ListItem>
                            <asp:ListItem Text="國小"></asp:ListItem>
                            <asp:ListItem Text="國中"></asp:ListItem>
                            <asp:ListItem Text="高中"></asp:ListItem>
                            <asp:ListItem Text="大學"></asp:ListItem>
                            <asp:ListItem Text="研究所以上"></asp:ListItem>
                        </asp:DropDownList>
                         <asp:CompareValidator ID="CompareValidator2" runat="server" Operator="NotEqual" ValueToCompare="請選擇"  ControlToValidate="DropDownList1" ErrorMessage="(請選擇)" ForeColor="Red" Font-Size="10pt"></asp:CompareValidator>
                         <%-- ValueToCompare 屬性:該值與輸入控制項之值進行比對。
                         --%>
                    
                    </td>
                  
                </tr>
                <tr>
                    <td>興趣：</td>
                    <td>
                        <table>
                            <tr>
                                <td rowspan="4">
                                    <asp:ListBox ID="ListBox1" runat="server" Width="100" Height="150">
                                        <asp:ListItem Text="爬山"></asp:ListItem>
                                        <asp:ListItem Text="踏青"></asp:ListItem>
                                        <asp:ListItem Text="看雲"></asp:ListItem>
                                        <asp:ListItem Text="健行"></asp:ListItem>
                                        <asp:ListItem Text="聽音樂"></asp:ListItem>
                                        <asp:ListItem Text="上網"></asp:ListItem>
                                        <asp:ListItem Text="遊泳"></asp:ListItem>
                                        <asp:ListItem Text="賺錢"></asp:ListItem>
                                        <asp:ListItem Text="唱歌"></asp:ListItem>
                                    </asp:ListBox>
                                </td>
                                <td>
                                     <asp:Button ID="Button2" runat="server" Text=">>" OnClick="Button2_Click" CausesValidation="false" />
                       
                                </td>
                                <td rowspan="4">
                                     <asp:ListBox ID="ListBox2" runat="server" Width="100" Height="150">

                                     </asp:ListBox>
                                    <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="(請至少選3種)" ForeColor="Red" Font-Size="10pt" OnServerValidate="CustomValidator2_ServerValidate"></asp:CustomValidator>
                        
                                </td>
                            </tr>
                            <tr>
                                <td>
                                      <asp:Button ID="Button3" runat="server" Text=">" OnClick="Button3_Click" CausesValidation="false" />
                     
                                </td>
                              
                            </tr>
                            <tr>
                                <td>
                                       <asp:Button ID="Button4" runat="server" Text="<" OnClick="Button4_Click" CausesValidation="false" />
                                </td>
                              
                            </tr>
                            <tr>
                                <td >
                                     <asp:Button ID="Button5" runat="server" Text="<<" OnClick="Button5_Click" CausesValidation="false" />
                                    
                                </td>
                              
                            </tr>
                        </table>
                 
                    </td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <asp:Button ID="Button6" runat="server" Text="確定送出" OnClick="Button6_Click" />
                        <input id="Reset1" type="reset" value="重設" />
                    </td>                  
                </tr>      
            </table>
        </div>
    </form>
</body>
</html>