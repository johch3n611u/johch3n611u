<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="40GGetAuthImg.aspx.cs" Inherits="ASPnet._40GGetAuthImg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            帳號:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <img src="40GetAuthImg.ashx" />
            <asp:Button ID="Button2" runat="server" Text="重新產生" />

            <br />
            驗證碼:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
 
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </div>
       
    </form>
</body>
</html>
