<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="13WebForm_StandardControls.aspx.cs" Inherits="ASPnet._13WebForm_StandardControls" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Button ID="Button1" BackColor="#ff0066" ForeColor="White" runat="server" Text="按鈕" OnClick="Button1_Click"  />

            <asp:Button ID="Button2" runat="server" Text="12345" Font-Size="36" BackColor="#6BE9D6" OnClick="Button2_Click" />
        
        
            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine"></asp:TextBox>

<%--            <asp:RadioButton ID="RadioButton1" runat="server" />男
            <asp:RadioButton ID="RadioButton2" runat="server" />女--%>

            <asp:RadioButton ID="RadioButton3" runat="server" Text="男" GroupName="gender" />
            <asp:RadioButton ID="RadioButton4" runat="server" Text="女" GroupName="gender" />
            
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                 <asp:ListItem Text="男"></asp:ListItem>
                 <asp:ListItem Text="女"></asp:ListItem>
            </asp:RadioButtonList>



            <asp:CheckBox ID="CheckBox1" runat="server" Text="踏青" />
            <asp:CheckBox ID="CheckBox2" runat="server" Text="觀星" />
            <asp:CheckBox ID="CheckBox3" runat="server" Text="看雲" />
            <asp:CheckBox ID="CheckBox4" runat="server" Text="健行" />
            <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
                 <asp:ListItem Text="踏青"></asp:ListItem>
                 <asp:ListItem Text="觀星"></asp:ListItem>
                 <asp:ListItem Text="看雲"></asp:ListItem>
                 <asp:ListItem Text="健行"></asp:ListItem>
            </asp:CheckBoxList>


            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem Text="博士"></asp:ListItem>
                 <asp:ListItem Text="碩士"></asp:ListItem>
                 <asp:ListItem Text="學士"></asp:ListItem>
                 <asp:ListItem Text="其他"></asp:ListItem>
            </asp:DropDownList>

            <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple">
                 <asp:ListItem Text="踏青"></asp:ListItem>
                 <asp:ListItem Text="觀星"></asp:ListItem>
                 <asp:ListItem Text="看雲"></asp:ListItem>
                 <asp:ListItem Text="健行"></asp:ListItem>
            </asp:ListBox>

        </div>
    </form>
</body>
</html>
