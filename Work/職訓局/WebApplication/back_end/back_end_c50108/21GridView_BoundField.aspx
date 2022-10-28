<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="21GridView_BoundField.aspx.cs" Inherits="ASPnet._21GridView_BoundField" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MySystemConnectionString1 %>" SelectCommand="SELECT * FROM [Products]"></asp:SqlDataSource>
            <div class="table-responsive">
            <asp:GridView GridLines="None" CssClass="table table-hover" ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="Product_ID" HeaderText="Product_ID" SortExpression="Product_ID" />
                    <asp:BoundField DataField="Product_Name" HeaderText="Product_Name" SortExpression="Product_Name" />
                  <%--  <asp:BoundField DataField="Product_Img" HeaderText="Product_Img" SortExpression="Product_Img" />--%>
                    <asp:ImageField DataImageUrlField="Product_Img" DataImageUrlFormatString="~\ProductsImg\s{0}"></asp:ImageField>
                    <asp:BoundField DataField="Product_Price" DataFormatString="{0:C0}" HeaderText="Product_Price" SortExpression="Product_Price" />
                    <asp:BoundField DataField="Product_price2" DataFormatString="{0:C0}" HeaderText="Product_price2" SortExpression="Product_price2" />
                    <%--<asp:BoundField DataField="Product_Intro" HeaderText="Product_Intro" SortExpression="Product_Intro" />--%>
                    <asp:TemplateField HeaderText="Product_Intro" SortExpression="Product_Intro">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Product_Intro") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Product_Intro").ToString().Replace("\n","<br>") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CheckBoxField DataField="Product_Status" HeaderText="Product_Status" SortExpression="Product_Status" />

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image runat="server" ID="Image1"></asp:Image>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
