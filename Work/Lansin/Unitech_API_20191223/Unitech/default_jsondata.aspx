<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default_jsondata.aspx.vb" Inherits="mng_limit_menu_getdata" %>
<%'= sqla%>
{"total":<%=TotalCount%>,"rows":[
<asp:repeater id="rptdata" runat="server">
<itemtemplate>{
    "id": <%# Eval("cno")%>,
    "cno": <%# Eval("cno")%>,
    "online": "<%# Eval("online")%>",
    "houseno": "<%# Eval("HouseNO")%>" ,
    "largeno": "<%#Eval("largeno")%>" ,
    "mediumno": "<%#Eval("mediumno")%>" ,
    "mediumsubno": "<%#Eval("mediumsubno")%>" ,
    "isnew": "<%#Eval("IsNew")%>",
    "iszprd": "<%#Eval("isZprd")%>",
    "productname": "<%# Eval("productname")%>" ,
    "productno": "<%# Eval("ProductNo")%>" ,
    "ls3cproductno": "<%# Eval("ls3cProductNO")%>" ,
    "num": "<%# Eval("num")%>",
    "status": "<%# Eval("Status")%>",
    "statusname": "<%# Eval("statusname")%>",
    "spicalprice": "<%#Eval("SpicalPrice")%>",
    "memsaveprice": "<%#Eval("memSavePrice")%>",
    "vip_memsaveprice": "<%#Eval("vip_memsaveprice")%>",
    "giftlength": "<%#Eval("giftLength")%>",
    "vip_giftLength": "<%#Eval("vip_giftLength")%>",
    "saleprice": "<%#Eval("saleprice")%>",
    "isngfree": "<%#Eval("isNGFree")%>",
    "rebuy": "<%#Eval("rebuy")%>",
    "Yahoo_Report": "<%#Eval("Yahoo_Report")%>",
    "Momo_Report": "<%#Eval("Momo_Report")%>",
    "Yahooposteddate": "<%#Eval("Yahooposteddate")%>",
    "Momoposteddate": "<%#Eval("Momoposteddate")%>"
}<%# IIf(Container.ItemIndex + 1 < TryCast(TryCast(Container.Parent, Repeater).DataSource, System.Data.DataTable).Rows.Count, ",", "")%>
</itemtemplate>
</asp:repeater>]}
    <%--"housename": "<%# Eval("HouseName")%>" ,--%>
    <%--"largename": "<%#Eval("largename")%>" ,--%>
    <%--"mediumname": "<%#Eval("mediumname")%>" ,--%>
    <%--"mediumsubname": "<%# Eval("mediumsubname")%>",--%>
    <%--"eventid": "<%# Eval("EventID")%>",--%>
    <%--"online_dt": "<%#Eval("OnLine_DT")%>",--%>
    <%--"saveprice_dndate": "<%#Eval("SavePrice_DnDate")%>",--%>
    <%--"role": "<%#Eval("Role")%>",--%>