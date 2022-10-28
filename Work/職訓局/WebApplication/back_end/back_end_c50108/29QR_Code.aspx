<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="29QR_Code.aspx.cs" Inherits="ASPnet._29QR_Code" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Image ID="Image1" runat="server" />
            <hr />
            <asp:Button ID="Button1" runat="server" Text="取得商品QR-Code" OnClick="Button1_Click" />
   
        </div>
    </form>
</body>
</html>
