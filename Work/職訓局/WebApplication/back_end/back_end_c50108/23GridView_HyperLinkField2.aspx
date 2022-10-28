<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="23GridView_HyperLinkField2.aspx.cs" Inherits="ASPnet._23GridView_HyperLinkField2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MySystemConnectionString1 %>" 
                SelectCommand="SELECT * FROM [Members] where Account=@Account">
                <SelectParameters>
                    <asp:QueryStringParameter Name="Account" Type="String" QueryStringField="id" />
                </SelectParameters>

            </asp:SqlDataSource>
            
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Account" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="Account" HeaderText="Account" ReadOnly="True" SortExpression="Account" />
                    <asp:BoundField DataField="Pswd" HeaderText="Pswd" SortExpression="Pswd" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Birthday" HeaderText="Birthday" SortExpression="Birthday" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:CheckBoxField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                    <asp:BoundField DataField="EduLevel" HeaderText="EduLevel" SortExpression="EduLevel" />
                    <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
