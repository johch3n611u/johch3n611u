<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="26DataList-SelectedItemTemplate.aspx.cs" Inherits="ASPnet._26DataList_SelectedItemTemplate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" 
                RepeatColumns="3" CellSpacing="20" RepeatDirection="Horizontal"
                 OnItemCommand="DataList1_ItemCommand"
                >
     
                  
                  <ItemTemplate>
                    <div style="text-align:center">
                       
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Select"> <%# Eval("Product_id") %></asp:LinkButton>
                        <br />

                        <img src='ProductsImg/s<%# Eval("Product_Img") %>' />
                        <br />
                        <asp:Label ID="Label2" runat="server" Font-Names="微軟正黑體" Font-Bold="true" Font-Size="14pt" Text='<%# Eval("Product_Name") %>' />
                        <br />
                        原價：
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Product_Price","{0:C0}") %>' ForeColor="#999999" CssClass="line_through" />
                        <br />
                        特價：
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Product_Price2","{0:C0}") %>' ForeColor="#ff0066" Font-Names="Arial Black" Font-Size="18pt" />
                    </div>
                </ItemTemplate>

                <SelectedItemTemplate>
                    <%# Eval("Product_Name") %>
                    <br />
                    <%# Eval("Product_Intro").ToString().Replace("\n","<br>") %>
                </SelectedItemTemplate>
         
                  
            </asp:DataList>
             <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:MySystemConnectionString1 %>" 
                SelectCommand="SELECT * FROM [Products]"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
