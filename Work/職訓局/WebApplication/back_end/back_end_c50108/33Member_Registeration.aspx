<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="33Member_Registeration.aspx.cs" Inherits="ASPnet._33Member_Registeration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
            #tbMember {
                width: 450px;
                height: 500px;
                margin: auto;
                border:3px double;
            }

            #tbMember table {
                width: 100%;
            }

            #tbMember>tbody>tr>td:first-child {
                text-align: right;
            }

            #tbMember>tbody>tr:last-child > td {
                text-align: center;
            }
            #tbMember table>tbody>tr>td {
                text-align: center;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <table id="tbMember">
                <caption>註冊會員</caption>
                <tr>
                    <td>帳號：</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtAccount" runat="server" placeholder="5-10碼"></asp:TextBox><asp:Button ID="Button2" runat="server" Text="檢查帳號可用性" ValidationGroup="abc123" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAccount" Display="Dynamic" runat="server" ErrorMessage="姓名為必填欄位" Text="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtAccount" Display="Dynamic" ValidationExpression="[A-Za-z][A-Za-z0-9]{4,9}" runat="server" ErrorMessage="(格式有誤)" ForeColor="Red" Font-Size="10pt"></asp:RegularExpressionValidator>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="(帳號重複)" ForeColor="Red" Font-Size="10pt" ValidationGroup="abc123" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>密碼：</td>
                    <td>
                        <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" placeholder="8-12碼" MaxLength="12"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtPwd" runat="server" ErrorMessage="密碼為必填欄位" Text="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtPwd" ValidationExpression="\S{8,12}" runat="server" ErrorMessage="(密碼不可含有空白)" ForeColor="Red" Font-Size="10pt"></asp:RegularExpressionValidator>
                     </td>
                </tr>
                <tr>
                    <td>密碼確認：</td>
                    <td>
                        <asp:TextBox ID="txtPwd2" runat="server" TextMode="Password" placeholder="請再輸入一次密碼"></asp:TextBox>
                      <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtPwd2" runat="server" ErrorMessage="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>--%>
                          <asp:CompareValidator ID="CompareValidator3" runat="server" Operator="Equal" ControlToCompare="txtPwd2"  ControlToValidate="txtPwd" ErrorMessage="(兩次密碼輸入不相同)" ForeColor="Red" Font-Size="10pt"></asp:CompareValidator>
                    </td>
                </tr>
             
                <tr>
                    <td>姓名：</td>
                    <td>
                         <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtName" runat="server" ErrorMessage="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                    <td>生日：</td>
                    <td>
                         <asp:TextBox ID="txtBirthday" runat="server" placeholder="1990-01-12"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtBirthday" Display="Dynamic" runat="server" ErrorMessage="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="CompareValidator1" Display="Dynamic" runat="server" Operator="DataTypeCheck" Type="Date" ControlToValidate="txtBirthday" ErrorMessage="(格式錯誤)" ForeColor="Red" Font-Size="10pt"></asp:CompareValidator>
                       
                    </td>
                </tr>
                <tr>
                    <td>E-mail：</td>
                    <td>
                         <asp:TextBox ID="txtEmail" runat="server" placeholder="abc@abc.com"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtEmail" ValidationExpression="([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)" runat="server" ErrorMessage="(格式有誤)" ForeColor="Red" Font-Size="10pt"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>性別：</td>
                    <td>
                        <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal" Width="120">
                            <asp:ListItem Text="男" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="女" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>學歷：</td>
                    <td>
                        <asp:DropDownList ID="ddlEduLevel" runat="server">
                        <asp:ListItem Text="請選擇"></asp:ListItem>
                        </asp:DropDownList>
                         <asp:CompareValidator ID="CompareValidator2" runat="server" Operator="NotEqual" ValueToCompare="請選擇"  ControlToValidate="ddlEduLevel" ErrorMessage="(請選擇)" ForeColor="Red" Font-Size="10pt"></asp:CompareValidator>
                    </td>
                  
                </tr>
                 <tr>
                    <td>照片：</td>
                    <td>
                        <asp:FileUpload ID="fulPhoto" runat="server" /><asp:Label ID="lblPhoto" Font-Size="10pt" ForeColor="Red" runat="server"></asp:Label>
                    </td>
                  
                </tr>
               <tr>
                    <td>備註：</td>
                    <td>
                        <asp:TextBox ID="txtNote" TextMode="MultiLine" runat="server" Width="200" Height="150"></asp:TextBox>
                       </td>
                  
                </tr>
                 <tr>
                    <td colspan="2">
                        <asp:Button ID="Button1" runat="server" Text="加入會員" OnClick="Button1_Click"  />
                        <input id="Reset1" type="reset" value="重設" />
                    </td>                  
                </tr>
                
            </table>
        </div>
    </form>
</body>
</html>
