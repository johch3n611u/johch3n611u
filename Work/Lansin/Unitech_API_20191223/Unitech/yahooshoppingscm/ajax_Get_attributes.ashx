<%@ WebHandler Language="VB" Class="ajax_Get_category" %>

Imports System
Imports System.Web
Imports System.Web.UI

Public Class ajax_Get_category : Implements IHttpHandler
    Public attrclass As String = HttpContext.Current.Request("attrclass")
    Public catItemId As String = HttpContext.Current.Request("catItemId")
    Public level1 As String = HttpContext.Current.Request("level1")
    Public level2 As String = HttpContext.Current.Request("level2")


    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        '小類觸發屬性 AJAX
        If catItemId <> "" Or catItemId <> Nothing Then

            Select Case attrclass
                Case "level1"
                    '查屬性
                    Dim attrbrandsql = "SELECT * FROM YahooshoppingSCM_API_struDataAttrClusters WITH (NOLOCK) WHERE catItemId='{0}' AND Attrrequired = 'True'"
                    attrbrandsql = String.Format(attrbrandsql, catItemId)
                    Dim dt = EC.DB.ExecuteDataTable(attrbrandsql)

                    '轉為選項
                    Dim htmltext = "<option value=''>未選擇</option>"

                    For num As Integer = 0 To dt.Rows.Count - 1 Step 1
                        htmltext += "<option value='" + dt.Rows.Item(num).Item("Attrname") + "'>" + dt.Rows.Item(num).Item("Attrname") + "</option>"
                    Next

                    context.Response.ContentType = "text/plain"
                    context.Response.Write(htmltext)

                Case "level2"
                    '查屬性
                    Dim attrbrandsql = "SELECT * FROM YahooshoppingSCM_API_struDataAttrClusters WITH (NOLOCK) WHERE catItemId='{0}' AND Attrrequired = 'True' AND Attrname <> '{1}'"
                    attrbrandsql = String.Format(attrbrandsql, catItemId, level1)
                    Dim dt = EC.DB.ExecuteDataTable(attrbrandsql)

                    '轉為選項
                    Dim htmltext = "<option value=''>未選擇</option>"
                    For num As Integer = 0 To dt.Rows.Count - 1 Step 1
                        htmltext += "<option value='" + dt.Rows.Item(num).Item("Attrname") + "'>" + dt.Rows.Item(num).Item("Attrname") + "</option>"
                    Next

                    context.Response.ContentType = "text/plain"
                    context.Response.Write(htmltext)


                Case "required"

                    '查必填屬性
                    '必填非自定義屬性
                    Dim attrbrandsql = "SELECT * FROM YahooshoppingSCM_API_struDataAttrClusters WITH (NOLOCK) WHERE catItemId='{0}' AND Attrrequired = 'True' AND Attrtype <> 'text'"
                    attrbrandsql = String.Format(attrbrandsql, catItemId)
                    Dim dt = EC.DB.ExecuteDataTable(attrbrandsql)

                    Dim attrrequireds = "<div id='requiredattr1' name='requiredattr1'>"
                    For num1 As Integer = 0 To dt.Rows.Count - 1 Step 1

                        Dim dtstring = dt.Rows.Item(num1).Item("Attrname").ToString
                        attrrequireds += "<input style='border: 0' id='attrname{1}' name='attrname{2}' readonly='readonly' value='{0}' /> : <select id='attrnoncustom{1}' name='attrnoncustom{2}' required='required'><option value=''>未選擇</option>"

                        attrrequireds = String.Format(attrrequireds, dtstring, num1, num1)

                        '轉為目標格式
                        Dim attrs = dt.Rows.Item(num1).Item("Attrvalues").ToString
                        Dim clear() As Char = {"[", "]"}
                        attrs = attrs.Trim(clear)
                        attrs = attrs.Replace("""", Nothing)
                        Dim requireds() = attrs.Split(",")

                        '轉為選擇與選項
                        For num2 As Integer = 0 To requireds.Count - 1 Step 1
                            attrrequireds += "<option value='" + requireds(num2) + "'>" + requireds(num2) + "</option>"
                        Next
                        attrrequireds += "</select></br>"
                    Next
                    attrrequireds += "</div>"

                    '必填自定義屬性

                    Dim attrbrandsql2 = "SELECT * FROM YahooshoppingSCM_API_struDataAttrClusters WITH (NOLOCK) WHERE catItemId='{0}' AND Attrrequired = 'True' AND Attrtype = 'text'"
                    attrbrandsql2 = String.Format(attrbrandsql2, catItemId)
                    Dim dt2 = EC.DB.ExecuteDataTable(attrbrandsql2)

                    attrrequireds += "</br></br>"
                    attrrequireds += "<div id='requiredattr2' name='requiredattr2'>"
                    For num1 As Integer = 0 To dt2.Rows.Count - 1 Step 1

                        Dim dtstring = dt2.Rows.Item(num1).Item("Attrname").ToString
                        attrrequireds += "<input style='border: 0' id='textattrname{1}' name='textattrname{2}' readonly='readonly' value='{0}' required='required'/> : <input id='attrcustom{1}' name='attrcustom{2}' required='required' />"

                        attrrequireds = String.Format(attrrequireds, dtstring, num1, num1)

                        attrrequireds += "</br>"
                    Next
                    attrrequireds += "</div>"

                    context.Response.ContentType = "text/plain"
                    context.Response.Write(attrrequireds)


                 '屬性ID
                Case "struDataAttrClusterId"

                    Dim struDataAttrClusterIdsql = "SELECT struDataAttrClusterId FROM YahooshoppingSCM_API_struDataAttrClusters WITH (NOLOCK) WHERE catItemId='{0}'"
                    struDataAttrClusterIdsql = String.Format(struDataAttrClusterIdsql, catItemId)
                    Dim dt2 = EC.DB.ExecuteDataTable(struDataAttrClusterIdsql)

                    context.Response.ContentType = "text/plain"
                    context.Response.Write(dt2.Select().FirstOrDefault.Item("struDataAttrClusterId").ToString())


            End Select

        End If

    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class