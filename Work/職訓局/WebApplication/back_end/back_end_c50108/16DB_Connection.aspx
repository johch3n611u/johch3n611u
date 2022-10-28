<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="16DB_Connection.aspx.cs" Inherits="ASPnet._16DB_Connection" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>


            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="Account" DataSourceID="SqlDataSource1" EmptyDataText="沒有資料錄可顯示。">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="Account" HeaderText="Account" ReadOnly="True" SortExpression="Account" />
                    <asp:BoundField DataField="Pswd" HeaderText="Pswd" SortExpression="Pswd" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Birthday" HeaderText="Birthday" SortExpression="Birthday" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:CheckBoxField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                    <asp:BoundField DataField="EduLevel" HeaderText="EduLevel" SortExpression="EduLevel" />
                    <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>


            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MySystemConnectionString1 %>" DeleteCommand="DELETE FROM [Members] WHERE [Account] = @Account" InsertCommand="INSERT INTO [Members] ([Account], [Pswd], [Name], [Birthday], [Email], [Gender], [EduLevel], [Notes]) VALUES (@Account, @Pswd, @Name, @Birthday, @Email, @Gender, @EduLevel, @Notes)" ProviderName="<%$ ConnectionStrings:MySystemConnectionString1.ProviderName %>" SelectCommand="SELECT [Account], [Pswd], [Name], [Birthday], [Email], [Gender], [EduLevel], [Notes] FROM [Members]" UpdateCommand="UPDATE [Members] SET [Pswd] = @Pswd, [Name] = @Name, [Birthday] = @Birthday, [Email] = @Email, [Gender] = @Gender, [EduLevel] = @EduLevel, [Notes] = @Notes WHERE [Account] = @Account">
                <DeleteParameters>
                    <asp:Parameter Name="Account" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Account" Type="String" />
                    <asp:Parameter Name="Pswd" Type="String" />
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Birthday" Type="DateTime" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="Gender" Type="Boolean" />
                    <asp:Parameter Name="EduLevel" Type="String" />
                    <asp:Parameter Name="Notes" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Pswd" Type="String" />
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Birthday" Type="DateTime" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="Gender" Type="Boolean" />
                    <asp:Parameter Name="EduLevel" Type="String" />
                    <asp:Parameter Name="Notes" Type="String" />
                    <asp:Parameter Name="Account" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>


        </div>
    </form>
</body>
</html>
