<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="27DataList-Edit.aspx.cs" Inherits="ASPnet._27DataList_Edit" %>

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
                  OnUpdateCommand="DataList1_UpdateCommand"
                >
     
                  
                  <ItemTemplate>
                    <div style="text-align:center">
                        <asp:Button ID="Button1" runat="server" Text="編輯資料" CommandName="Edit" />
                        <%--<asp:LinkButton ID="LinkButton1" runat="server" CommandName="Select"> <%# Eval("Product_id") %></asp:LinkButton>--%>
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

            <EditItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Update">更新</asp:LinkButton>
                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Cancel">取消</asp:LinkButton>
                <br />
                <asp:Label ID="lblProduct_Status" runat="server" Text='<%# Eval("Product_Status") %>'></asp:Label>
           
                    Product_ID:
                    <asp:Label ID="Product_IDLabel" runat="server" Text='<%# Eval("Product_ID") %>' />
                    <br />
                    Product_Name:
                  
                <asp:TextBox ID="txtProduct_Name" runat="server" Text='<%# Eval("Product_Name") %>' Width="200"></asp:TextBox>
                    <br />
                    Product_Img:
                    <img src='ProductsImg/s<%# Eval("Product_Img") %>' /><br />
                    <asp:FileUpload ID="fulProductsImg" runat="server" />
                    <br />
                    Product_Price:
                 
                <asp:TextBox ID="txtProduct_Price" runat="server" Text='<%# Eval("Product_Price","{0:0}") %>' Width="100"></asp:TextBox>
                    <br />
                    Product_price2:
          
                <asp:TextBox ID="txtProduct_Price2" runat="server" Text='<%# Eval("Product_Price2","{0:0}") %>' Width="100"></asp:TextBox>
                    <br />
                    Product_Intro:
              
                <asp:TextBox ID="txtProduct_Intro" runat="server" TextMode="MultiLine" Text='<%# Eval("Product_Intro") %>' Width="200" Height="100"></asp:TextBox>
                    <br />
                    Product_Status:
              
                <asp:RadioButtonList ID="rblProduct_Status" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="上架" Value="1"></asp:ListItem>
                    <asp:ListItem Text="下架" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
                    <br />

            </EditItemTemplate>
                  
            </asp:DataList>
             <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:MySystemConnectionString1 %>" 
                SelectCommand="SELECT * FROM [Products]"
                  UpdateCommand="update Products set Product_Name=@Product_Name, Product_Price=@Product_Price, 
                 Product_Price2=@Product_Price2, Product_Intro=@Product_Intro, Product_Status=@Product_Status where Product_ID=@Product_ID"
                 >
                    <UpdateParameters>
                        <asp:Parameter Name="Product_Name" Type="String" />
                        <asp:Parameter Name="Product_Price" Type="Int32" />
                        <asp:Parameter Name="Product_Price2" Type="Int32" />
                        <asp:Parameter Name="Product_Intro" Type="String" />
                        <asp:Parameter Name="Product_Status" Type="Int16" />
                        <asp:Parameter Name="Product_ID" Type="String" />
                    </UpdateParameters>



             </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
