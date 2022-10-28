<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="25DataList-ItemTeamlate.aspx.cs" Inherits="ASPnet._25DataList_ItemTeamlate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        #container{
            width:1247px;
            margin:auto;
        }
        #left{
            float:left;
        }
        #right{
            width:1063px;
            float:right;
        }
        #bottom {
            clear:both;
        }


        .line_through{
            text-decoration:line-through;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">

            <div id="top">
                <img src="images/top.jpg" />
            </div>
            <div id="left">
                <img src="images/left.jpg" />
            </div>
            <div id="right">

              <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" RepeatColumns="3" CellSpacing="20" RepeatDirection="Horizontal">
                <FooterTemplate>
                    This is Footer
                </FooterTemplate>
                  
                  <ItemTemplate>
                    <div style="text-align:center">
                        <%# Eval("Product_id") %>
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

                  <HeaderTemplate>
                      This is Header
                  </HeaderTemplate>

                  <SeparatorTemplate>
                      **********************
                  </SeparatorTemplate>
                
                  <AlternatingItemTemplate>
                       <div style="text-align:center;background-color:#ffd800">
                        <%# Eval("Product_id") %>
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

                  </AlternatingItemTemplate>

                  
            </asp:DataList>

            </div>
            <div id="bottom">
                <img src="images/bottom.jpg" />
            </div>


            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:MySystemConnectionString1 %>" 
                SelectCommand="SELECT * FROM [Products]"></asp:SqlDataSource>

           

          
        </div>
    </form>
</body>
</html>
