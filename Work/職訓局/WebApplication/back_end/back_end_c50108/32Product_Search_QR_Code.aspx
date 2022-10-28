<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="32Product_Search_QR_Code.aspx.cs" Inherits="ASPnet._32Product_Search_QR_Code" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="Button" />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                ConnectionString="<%$ ConnectionStrings:MySystemConnectionString1 %>" 
                SelectCommand="SELECT * FROM [Products] where Product_ID=@Product_ID">
                <SelectParameters>
                    <asp:ControlParameter Name="Product_ID" ControlID="TextBox1" Type="String" />
                </SelectParameters>

            </asp:SqlDataSource>
            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1"></asp:GridView>
        </div>
    </form>
</body>
</html>
