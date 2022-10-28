<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="24CommandField.aspx.cs" Inherits="ASPnet._24CommandField" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:MySystemConnectionString1 %>" 
                SelectCommand="SELECT * FROM [Members]"
                 UpdateCommand="update Members set Name=@Name,  Email=@Email, EduLevel=@EduLevel, Notes=@Notes where Account=@Account"
                DeleteCommand="delete from Members where Account=@Account"
                ></asp:SqlDataSource>

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Account" 
                DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="Account" HeaderText="Account" ReadOnly="True" SortExpression="Account" />
                    <asp:BoundField DataField="Pswd" HeaderText="Pswd" SortExpression="Pswd" ReadOnly="true" DataFormatString="******" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Birthday" HeaderText="Birthday" SortExpression="Birthday" ReadOnly="true" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:CheckBoxField DataField="Gender" HeaderText="Gender" SortExpression="Gender" ReadOnly="true" />
                    <asp:BoundField DataField="EduLevel" HeaderText="EduLevel" SortExpression="EduLevel" />
                    <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
                    <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" ButtonType="Button" />

                </Columns>
            </asp:GridView>
     

        </div>
    </form>
    
</body>
</html>
