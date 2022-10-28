<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="14WebForm_MemberForm.aspx.cs" Inherits="ASPnet._14WebForm_MemberForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        #tbMember{
            width:400px;height:500px;margin:auto;
            
        }
       
        #tbMember tr:last-child>td{
            text-align:center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="tbMember">
                <caption>會員資料</caption>
                <tr>
                    <td>帳號：</td>
                    <td>
                        <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>密碼：</td>
                    <td>
                        <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>密碼確認：</td>
                    <td>
                        <asp:TextBox ID="txtPwd2" runat="server" TextMode="Password" placeholder="請再輸入一次密碼"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>姓名：</td>
                    <td>
                         <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>E-mail：</td>
                    <td>
                         <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="ex:abc@abc.com"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>姓別：</td>
                    <td>
                        <asp:RadioButton ID="rdbMale" runat="server" Text="男生" GroupName="gender" />
                        <asp:RadioButton ID="rdbFemale" runat="server" Text="女生" GroupName="gender" Checked="true" />
                        <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="男" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="女"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>學歷：</td>
                    <td>
                        <asp:DropDownList ID="ddlEduLevel" runat="server">
                            <asp:ListItem Text="請選擇"></asp:ListItem>
                            <asp:ListItem Text="國小"></asp:ListItem>
                            <asp:ListItem Text="國中"></asp:ListItem>
                            <asp:ListItem Text="高中"></asp:ListItem>
                            <asp:ListItem Text="大學"></asp:ListItem>
                            <asp:ListItem Text="研究所以上"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                  
                </tr>
                <tr>
                    <td>興趣：</td>
                    <td>
                        <asp:CheckBoxList ID="cblInterest" runat="server">
                            <asp:ListItem Text="爬山"></asp:ListItem>
                            <asp:ListItem Text="健行"></asp:ListItem>
                            <asp:ListItem Text="踏青"></asp:ListItem>
                            <asp:ListItem Text="看雲"></asp:ListItem>
                            <asp:ListItem Text="觀星"></asp:ListItem>
                        </asp:CheckBoxList>
                        <asp:CheckBox ID="ckbInterest1" runat="server" Text="爬山" />
                        <asp:CheckBox ID="ckbInterest2" runat="server" Text="健行" />
                        <asp:CheckBox ID="ckbInterest3" runat="server" Text="踏青" />
                        <asp:CheckBox ID="ckbInterest4" runat="server" Text="看雲" />
                        <asp:CheckBox ID="ckbInterest5" runat="server" Text="觀星" />
                    </td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <asp:Button ID="Button1" runat="server" Text="確定送出" OnClick="Button1_Click" />
                        <input id="Reset1" type="reset" value="重設" />
                    </td>                  
                </tr>
                
            </table>


        </div>
    </form>
</body>
</html>
