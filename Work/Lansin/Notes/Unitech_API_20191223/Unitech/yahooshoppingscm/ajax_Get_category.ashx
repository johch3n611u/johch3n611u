<%@ WebHandler Language="VB" Class="ajax_Get_category" %>

Imports System
Imports System.Web
Imports System.Web.UI

Public Class ajax_Get_category : Implements IHttpHandler
    Public subStationId As String = HttpContext.Current.Request("subStationId")
    Public categoryId As String = HttpContext.Current.Request("categoryId")

    Public yahooCategorycats As String
    Public yahooCategorycatitems As String

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        '大類觸發中類 AJAX
        If subStationId <> "" Or subStationId <> Nothing Then

            '用於結構化雅虎商品類別 

            Dim APICategorydtSql = String.Format("SELECT categoryId,categoryIdname FROM YahooshoppingSCM_API_Category WITH(NOLOCK) WHERE subId='{0}' GROUP BY categoryId,categoryIdname", subStationId)
            Dim APICategorydt = EC.DB.ExecuteDataTable(APICategorydtSql)

            yahooCategorycats += "<option value='中類'>未選擇</option>"

            For num As Integer = 0 To APICategorydt.Rows.Count - 1 Step 1
                Dim html = "<option value='{0}'>{1}</option>"
                html = String.Format(html, APICategorydt.Rows.Item(num).Item(0), APICategorydt.Rows.Item(num).Item(1))
                yahooCategorycats += html
            Next

            context.Response.ContentType = "text/plain"
            context.Response.Write(yahooCategorycats)

        End If

        '中類觸發小類 AJAX
        If categoryId <> "" Or categoryId <> Nothing Then

            '用於結構化雅虎商品類別 

            yahooCategorycatitems += "<option value=''>未選擇</option>"

            Dim APICategorydtSql = String.Format("SELECT catItemId,catItemIdname FROM YahooshoppingSCM_API_Category WITH(NOLOCK) WHERE categoryId='{0}' GROUP BY catItemId,catItemIdname", categoryId)
            Dim APICategorydt = EC.DB.ExecuteDataTable(APICategorydtSql)

            For num As Integer = 0 To APICategorydt.Rows.Count - 1 Step 1
                Dim html = "<option value='{0}'>{1}</option>"
                html = String.Format(html, APICategorydt.Rows.Item(num).Item(0), APICategorydt.Rows.Item(num).Item(1))
                yahooCategorycatitems += html
            Next

            context.Response.ContentType = "text/plain"
            context.Response.Write(yahooCategorycatitems)

        End If

    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class