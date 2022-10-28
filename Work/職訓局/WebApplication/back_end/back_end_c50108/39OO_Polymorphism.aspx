<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="39OO_Polymorphism.aspx.cs" Inherits="ASPnet._39OO_Polymorphism" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                <asp:ListItem Text="人"></asp:ListItem>
                <asp:ListItem Text="狗"></asp:ListItem>
                <asp:ListItem Text="貓"></asp:ListItem>
                <asp:ListItem Text="魚"></asp:ListItem>
            </asp:RadioButtonList>
            <asp:Button ID="Button1" runat="server" Text="按我就叫(類別)" OnClick="Button1_Click" />
            
            <hr />
            <asp:Label ID="Label1" runat="server"></asp:Label>

        </div>
    </form>
</body>
</html>
