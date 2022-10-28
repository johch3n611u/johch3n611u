<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="20GridView_Search.aspx.cs" Inherits="ASPnet._20GridView_Search" %>

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
                SelectCommand="SELECT * FROM [Members] where Name like '%'+ @name + '%'">
                <SelectParameters>
                    <asp:ControlParameter Name="name" ControlID="txtName" Type="String" />
                </SelectParameters>

            </asp:SqlDataSource>
            請輸入姓名:<asp:TextBox ID="txtName" runat="server"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="確定" />
            <hr />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Account" 
                DataSourceID="SqlDataSource1" EmptyDataText="查無資料!!">
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

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            <br />
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MySystemConnectionString1 %>" 
                SelectCommand="SELECT * FROM [Members]" FilterExpression="Name like '%{0}%'">
              <%--  <SelectParameters>
                    <asp:ControlParameter Name="name" ControlID="txtName2" Type="String" />
                </SelectParameters>--%>
                <FilterParameters>
                      <asp:ControlParameter Name="name" ControlID="txtName2" Type="String" />

                </FilterParameters>
            </asp:SqlDataSource>
            請輸入姓名:<asp:TextBox ID="txtName2" runat="server"></asp:TextBox><asp:Button ID="Button2" runat="server" Text="確定" />
            <hr />
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="Account" 
                DataSourceID="SqlDataSource2" EmptyDataText="查無資料!!">
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
