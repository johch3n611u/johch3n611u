'/////////////////////////////////////////////////////////////////////////////////
' 商品管理 - 匯出excel
'
' 建檔人員: 小葉
' 建檔日期: 2015-04-29
' 修改記錄: 2018-12-06 Gary  Excel匯出功能:新增"特價"欄位
' 關連程式:
' 呼叫來源: 
'/////////////////////////////////////////////////////////////////////////////////
Imports System.Data
Imports EC.Library.Security
Imports System.IO
Partial Class mng_Product_list_Default_Excel
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Response.CacheControl = "no-cache"    '避免被 Cache 住 

        Dim selitemcat As String = RequestString("selitemcat", RequestActMode.None, RequestMode.SQLInjection)          '商品類別
        Dim selhouseno As String = RequestString("selhouseno", RequestActMode.None, RequestMode.SQLInjection)          '館別
        Dim sellargeno As String = RequestString("sellargeno", RequestActMode.None, RequestMode.SQLInjection)          '大類
        Dim selmediumno As String = RequestString("selmediumno", RequestActMode.None, RequestMode.SQLInjection)        '中類
        Dim selmediumsubno As String = RequestString("selmediumsubno", RequestActMode.None, RequestMode.SQLInjection)  '小類
        Dim selempno As String = RequestString("selempno", RequestActMode.None, RequestMode.SQLInjection)              '採購
        Dim selOptions As String = SQLIJ(Request("selOptions"))         '搜尋欄位
        Dim keyword As String = SQLIJ(Request("keyword"))               '搜尋字串
        Dim selSort As String = "ModifyDate"                  '排序欄位
        Dim selOrder As String = "desc"                       '排序方式: asc / desc

        Dim sql_complex As String = ""
        sql_complex = "SELECT TOP 3000 " & vbCrLf &
                      "productno,ls3cProductNO,productname,spicalprice,memSavePrice," & vbCrLf &
                      "CASE [ONLINE] WHEN 0 THEN '下架' ELSE '上架' END [ONLINE]," & vbCrLf &
                      " (SELECT top 1 xAdmin_Manager.AdminName " & vbCrLf &
                      " FROM xAdmin_Manager WHERE xAdmin_Manager.EMPNO = v2014_list.EMPNO) EMPNO, " & vbCrLf &
                      " (SELECT top 1 barCode FROM list WHERE list.cno = v2014_list.cno) barCode " & vbCrLf &
                      "FROM v2014_list" & vbCrLf &
                      "Where" & vbCrLf &
                      "    EventID = 0" & vbCrLf &
                      "    AND gcount = 0" & vbCrLf &
                      "    AND Large_GroupName = 'general'" & vbCrLf

        Select Case selitemcat
            Case "isngfree"   'NG福利品
                sql_complex = sql_complex & "AND NG = 2" & vbCrLf
            Case Else   '排除NG商品
                sql_complex = sql_complex & "AND NG not in (1)" & vbCrLf
        End Select

        '採購
        If Not String.IsNullOrWhiteSpace(selempno) Then
            sql_complex += String.Format("and empno='{0}'" & vbCrLf, selempno)
        End If

        '館別
        If Not String.IsNullOrWhiteSpace(selhouseno) Then
            sql_complex += String.Format("and houseno='{0}'" & vbCrLf, selhouseno)
        End If

        '大類
        If Not String.IsNullOrWhiteSpace(sellargeno) Then
            sql_complex += String.Format("and largeno='{0}'" & vbCrLf, sellargeno)
        End If

        '中類
        If Not String.IsNullOrWhiteSpace(selmediumno) Then
            sql_complex += String.Format("and mediumno='{0}'" & vbCrLf, selmediumno)
        End If

        '小類
        If Not String.IsNullOrWhiteSpace(selmediumsubno) Then
            sql_complex += String.Format("and mediumsubno='{0}'" & vbCrLf, selmediumsubno)
        End If

        '搜尋欄位
        If Not String.IsNullOrWhiteSpace(keyword) Then
            sql_complex += "and " & selOptions & " like '%" & keyword & "%'" & vbCrLf
        End If

        sql_complex += "Order by " & selSort & " " & selOrder

        Dim dt As DataTable = EC.DB.ExecuteDataTable(sql_complex)

        '調整順序，列排序從0開始
        dt.Columns("productno").SetOrdinal(0)
        dt.Columns("ls3cProductNO").SetOrdinal(1)
        dt.Columns("productname").SetOrdinal(2)
        dt.Columns("spicalprice").SetOrdinal(3)
        dt.Columns("ONLINE").SetOrdinal(4)
        dt.Columns("EMPNO").SetOrdinal(5)
        dt.Columns("barCode").SetOrdinal(6)
        dt.Columns("memSavePrice").SetOrdinal(7)

        '修改列標題名稱
        dt.Columns("productno").ColumnName = "良興代碼"
        dt.Columns("ls3cProductNO").ColumnName = "良興品名"
        dt.Columns("productname").ColumnName = "商品名稱"
        dt.Columns("spicalprice").ColumnName = "網路價"
        dt.Columns("ONLINE").ColumnName = "上下架"
        dt.Columns("EMPNO").ColumnName = "採購員"
        dt.Columns("barCode").ColumnName = "商品條碼"
        dt.Columns("memSavePrice").ColumnName = "特價"
        Dim strm As MemoryStream = EC.NPOI.RenderDataTableToExcel(dt)

        Dim FileName As String = "產品管理-" & Today & ".xls"

        Response.ContentType = "application/vnd.ms-excel"
        Response.CacheControl = "no-cache"    '避免被 Cache 住
        Response.Buffer = True
        Response.Expires = -1
        Response.AddHeader("Pragma", "no-cache")
        Response.AddHeader("Content-Disposition", "attachment; filename=" & FileName)
        Response.BinaryWrite(strm.ToArray)
        Response.End()
    End Sub
End Class
