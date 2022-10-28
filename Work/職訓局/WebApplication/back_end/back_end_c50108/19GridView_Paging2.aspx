<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="19GridView_Paging2.aspx.cs" Inherits="ASPnet._19GridView_Paging2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        .btn{
            font-family:Webdings;
            text-decoration:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged" AutoPostBack="true"></asp:TextBox>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:MySystemConnectionString1 %>" 
                SelectCommand="SELECT * FROM [Members]"></asp:SqlDataSource>

            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" PageSize="2" AllowPaging="true"
                 OnDataBound="GridView1_DataBound"
                >
                <PagerTemplate>
                    <table style="width:100%">
                        <tr>
                            <td>
                               
                                <asp:LinkButton ID="lkbPrev" runat="server" CssClass="btn" OnClick="PageChange_Click">3</asp:LinkButton>
                                <asp:LinkButton ID="lkbNext" runat="server" CssClass="btn" OnClick="PageChange_Click">4</asp:LinkButton>
                                
                                <asp:DropDownList ID="ddlPager" runat="server" OnSelectedIndexChanged="ddlPager_SelectedIndexChanged" AutoPostBack="true">
                          
                                </asp:DropDownList>
                            </td>
                            <td style="text-align:right">
                                <asp:Label ID="lblInfo" runat="server"></asp:Label>  
                            </td>
                        </tr>
                    </table>

                </PagerTemplate>
            </asp:GridView>

            

        </div>
    </form>
</body>
</html>
