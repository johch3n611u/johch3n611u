<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Form_List_Mod_Rec.aspx.vb" Inherits="mng_product_list_Form_List_Mod_Rec" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>異動記錄</title>
    <link rel="stylesheet" type="text/css" href="/css/easyui.css" />
</head>
<body>

<table border="1" class="doc-table">
    <tr>
        <td colspan="5">產品名稱: <font color="red"><%=productname%></font> -- 異動記錄 Top 100</td>
    </tr>
    <tr bgcolor="yellow">
        <td nowrap>編號</td>
        <td nowrap>異動人員</td>
        <td>異動狀態</td>
        <td>程式位置</td>
        <td>異動時間　</td>
    </tr>
<%If _Count = 0 Then%>
    <tr>
        <td colspan="5" align="center"><font color="red">尚無記錄</font></td>
    </tr>
<%End If%>
<asp:Repeater ID="rpt_list_mod_rec" runat="server">
    <ItemTemplate>
    <tr>
        <td align="center"><%#Container.ItemIndex + 1%></td>
        <td><%#Eval("Man")%></td>
        <td><%#Eval("Status")%></td>
        <td><%#Eval("Prg_name")%></td>
        <td><%#Eval("postdate")%></td>
    </tr>
    </ItemTemplate>
</asp:Repeater>
</table>

</body>
</html>