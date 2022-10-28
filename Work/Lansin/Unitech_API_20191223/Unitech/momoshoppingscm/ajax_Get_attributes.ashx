<%@ WebHandler Language="VB" Class="ajax_Get_category" %>

Imports System
Imports System.Web
Imports System.Web.UI

Public Class ajax_Get_category : Implements IHttpHandler
    Public mainEcCategoryCode As String = HttpContext.Current.Request("mainEcCategoryCode") + ""

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        '新選項 
        Dim attrindexsql_New As String = "SELECT * FROM MomoshoppingSCM_API_AttrIndex WITH(NOLOCK) WHERE mainEcCategoryCode='{0}'"
        attrindexsql_New = String.Format(attrindexsql_New, mainEcCategoryCode)
        Dim attrdt_New = EC.DB.ExecuteDataTable(attrindexsql_New)

        Dim tag = ""

        If attrdt_New.Rows.Count > 0 Then
            For num As Integer = 0 To attrdt_New.Rows.Count - 1 Step 1
                '部分連動選項可複選但此處沒實作，複選選項可至資料表表MomoshoppingSCM_API_AttrIndex欄位CHECK_YN查詢
                tag +=
                   "<input style ='display:none;' id='indexNo_{0}' name='indexNo_{0}' value='{1}' required='required'/>" +
                   "<span>{2}</span> : <select id='select_{0}' name='select_{0}' required='required'><option value='' selected='selected'>未選擇</option>"

                Dim indexNo = attrdt_New.Rows.Item(num).Item("indexNo")
                Dim attrName = attrdt_New.Rows.Item(num).Item("attrName")
                tag = String.Format(tag, num, indexNo, attrName)

                Dim chosenItemNos() = Split((attrdt_New.Rows.Item(num).Item("chosenItemNo").ToString), ",")
                Dim chosenItemNames() = Split((attrdt_New.Rows.Item(num).Item("chosenItemName").ToString), ",")

                For num2 As Integer = 0 To chosenItemNos.Count - 1 Step 1

                    Dim selecthtml = "<option value='{0}'>{1}</option>"
                    Dim chosenItemNo = chosenItemNos(num2)
                    Dim chosenItemName = chosenItemNames(num2)

                    selecthtml = String.Format(selecthtml, chosenItemNo, chosenItemName)
                    tag += selecthtml

                Next

                tag += "</select>" + "</br>"
            Next

        End If

        context.Response.ContentType = "text/plain"
        context.Response.Write(tag)

    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class