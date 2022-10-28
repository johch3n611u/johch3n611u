<%@ Page Language="C#" AutoEventWireup="true" CodeFile="27GridView_Pagination.aspx.cs" Inherits="_27GridView_Pagination" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <style>
       #GridView1 > tbody > tr:first-child{
           background-color:#ce0059;
           color:white;
       }
         #GridView1 > tbody > tr:last-child{
           background-color:#ffffff;
           color:cadetblue;
       }
       .PageActive{
           background-color:#ce0059;
            color:white;
       }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MySystemConnectionString %>" SelectCommand="SELECT * FROM [Products2]"></asp:SqlDataSource>
            <asp:GridView CssClass="table table-danger" ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="Product_ID" DataSourceID="SqlDataSource1" PageSize="1">
                <Columns>
                    <asp:BoundField DataField="Product_ID" HeaderText="Product_ID" ReadOnly="True" SortExpression="Product_ID" />
                    <asp:BoundField DataField="Product_Name" HeaderText="Product_Name" SortExpression="Product_Name" />
                    <asp:BoundField DataField="Product_Img" HeaderText="Product_Img" SortExpression="Product_Img" />
                    <asp:BoundField DataField="Product_Price" HeaderText="Product_Price" SortExpression="Product_Price" />
                    <asp:BoundField DataField="Product_price2" HeaderText="Product_price2" SortExpression="Product_price2" />
                    <asp:BoundField DataField="Product_Intro" HeaderText="Product_Intro" SortExpression="Product_Intro" />
                    <asp:CheckBoxField DataField="Product_Status" HeaderText="Product_Status" SortExpression="Product_Status" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <script src="Scripts/jquery-3.0.0.min.js"></script>

    <script src="Scripts/bootstrap.bundle.min.js"></script>
    <script>
        $('#GridView1 > tbody > tr:last-child>td>table').addClass('nav justify-content-center');

        $('#GridView1 > tbody > tr:last-child>td>table td').addClass('page-item');
        $('#GridView1 > tbody > tr:last-child>td>table td>a').addClass('page-link');
         $('#GridView1 > tbody > tr:last-child>td>table td>span').addClass('page-link PageActive');
    </script>

</body>
</html>
