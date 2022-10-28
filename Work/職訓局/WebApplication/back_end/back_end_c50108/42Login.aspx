<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="42Login.aspx.cs" Inherits="ASPnet._42Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="form-group">
                <label class="col-form-label" for="txtAccount">帳號:</label>
                <asp:TextBox ID="txtAccount" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="col-form-label" for="txtPswd">密碼:</label>
                <asp:TextBox ID="txtPswd" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>

            <asp:Button ID="Button1" runat="server" Text="登入" CssClass="btn btn-primary" OnClick="Button1_Click" />
        </div>


        

    </form>
</body>
</html>
