'/////////////////////////////////////////////////////////////////////////////////
' 取 default 主頁資料 (json 格式)
'
' 建檔人員: 阿友
' 建檔日期: 2012-08-01
' 修改記錄: 2015-02-03 葉: 增加市價欄位
'          20191017 育誠:需求單 list.rebuy 回購欄位增加
' 關連程式:
' 呼叫來源: default.aspx
'/////////////////////////////////////////////////////////////////////////////////
Imports System.Data
Imports EC.Library.Security

Partial Class mng_limit_menu_getdata
    Inherits System.Web.UI.Page

    Public TotalCount As Integer = 0
    Public sqla As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.CacheControl = "no-cache"    '避免被 Cache 住 

        Dim selitemcat As String = RequestString("selitemcat", RequestActMode.None, RequestMode.SQLInjection)          '商品類別
        Dim selhouseno As String = RequestString("selhouseno", RequestActMode.None, RequestMode.SQLInjection)          '館別
        Dim sellargeno As String = RequestString("sellargeno", RequestActMode.None, RequestMode.SQLInjection)          '大類
        Dim selmediumno As String = RequestString("selmediumno", RequestActMode.None, RequestMode.SQLInjection)        '中類
        Dim selmediumsubno As String = RequestString("selmediumsubno", RequestActMode.None, RequestMode.SQLInjection)  '小類
        Dim selempno As String = RequestString("selempno", RequestActMode.None, RequestMode.SQLInjection)              '採購
        Dim selOptions As String = SQLIJ(Request("selOptions"))         '搜尋欄位
        Dim keyword As String = SQLIJ(Request("keyword"))               '搜尋字串
        Dim selSort As String = SQLIJ(Request("Sort"))                  '排序欄位
        Dim selOrder As String = SQLIJ(Request("Order"))                '排序方式: asc / desc
        Dim CurrentPage As Integer = SQLIJ(Request("page"))             '跳到N頁
        Dim PageSize As Integer = SQLIJ(Request("rows"))                '每頁筆數

        If String.IsNullOrWhiteSpace(selSort) Then selSort = "MediumSubNo"
        If Not IsNumeric(CurrentPage) Then CurrentPage = 1
        If CurrentPage < 1 Then CurrentPage = 1
        If PageSize <= 1 Then PageSize = 30


        '使用預存跳頁(Paging2005_Complex) ****************************************************************************
        Dim sql_complex As String = ""
        sql_complex = "SELECT" & vbCrLf &
                      "     isNGFree," & vbCrLf &
                      "    '' as isZprd,  --Z-商品" & vbCrLf &
                      "    IsNew = CASE ISNULL(CAST(  OnLine_DT AS VARCHAR(10) ),'')" & vbCrLf &
                      "    WHEN '' THEN '*'" & vbCrLf &
                      "    ELSE '' End," & vbCrLf &
                      "     cno," & vbCrLf &
                      "     HouseNO," & vbCrLf &
                      "    --HouseName," & vbCrLf &
                      "     largeno," & vbCrLf &
                      "    --LargeName," & vbCrLf &
                      "     mediumno," & vbCrLf &
                      "    --mediumname," & vbCrLf &
                      "     MediumSubNO," & vbCrLf &
                      "    --MediumSubName," & vbCrLf &
                      "    --EventID," & vbCrLf &
                      "     num," & vbCrLf &
                      "    --brandID," & vbCrLf &
                      "     Status," & vbCrLf &
                      "    '' AS StatusName," & vbCrLf &
                      "     OnLine," & vbCrLf &
                      "    --OnLine_DT," & vbCrLf &
                      "     ProductName," & vbCrLf &
                      "     v2014_list.ProductNo," & vbCrLf &
                      "     ls3cProductNO," & vbCrLf &
                      "     saleprice," & vbCrLf &
                      "     SpicalPrice," & vbCrLf &
                      "     memSavePrice," & vbCrLf &
                      "    --Role," & vbCrLf &
                      "    --SavePrice_DnDate," & vbCrLf &
                      "    ISNULL(DATALENGTH( gift),0) AS giftLength," & vbCrLf &
                      "    --empno," & vbCrLf &
                      "    --Offer," & vbCrLf &
                      "     vip_memSavePrice," & vbCrLf &
                      "    ISNULL(DATALENGTH( vip_gift),0) AS vip_giftLength," & vbCrLf &
                      "     v2014_list.PostDate," & vbCrLf &
                      "     ModifyDate," & vbCrLf &
                      "     rebuy," & vbCrLf &
                      "Unitech_list.Yahoo_Report," & vbCrLf &
                      "Unitech_list.Momo_Report," & vbCrLf &
                      "     YahooshoppingSCM_API_list.postdate as Yahooposteddate," & vbCrLf &
                      "     MomoshoppingSCM_API_list.postdate as Momoposteddate" & vbCrLf &
                      "FROM v2014_list" & vbCrLf &
                      "RIGHT JOIN Unitech_list" & vbCrLf &
                      "ON v2014_list.ProductNo = Unitech_list.ProductNo" & vbCrLf &
                      "LEFT JOIN YahooshoppingSCM_API_list" & vbCrLf &
                      "ON v2014_list.ProductNo = YahooshoppingSCM_API_list.ProductNo" & vbCrLf &
                      "LEFT JOIN MomoshoppingSCM_API_list" & vbCrLf &
                      "ON v2014_list.ProductNo = MomoshoppingSCM_API_list.ProductNo" & vbCrLf &
                      "Where" & vbCrLf &
                      "     EventID = 0" & vbCrLf &
                      "    AND  gcount = 0" & vbCrLf &
                      "    AND  Large_GroupName = 'general'" & vbCrLf

        Select Case selitemcat
            Case "isngfree"   'NG福利品
                sql_complex = sql_complex & "AND  NG = 2" & vbCrLf
            Case Else   '排除NG商品
                sql_complex = sql_complex & "AND  NG not in (1)" & vbCrLf
        End Select

        '採購
        If Not String.IsNullOrWhiteSpace(selempno) Then
            sql_complex += String.Format("and  empno='{0}'" & vbCrLf, selempno)
        End If

        '館別
        If Not String.IsNullOrWhiteSpace(selhouseno) Then
            sql_complex += String.Format("and  houseno='{0}'" & vbCrLf, selhouseno)
        End If

        '大類
        If Not String.IsNullOrWhiteSpace(sellargeno) Then
            sql_complex += String.Format("and  largeno='{0}'" & vbCrLf, sellargeno)
        End If

        '中類
        If Not String.IsNullOrWhiteSpace(selmediumno) Then
            sql_complex += String.Format("and  mediumno='{0}'" & vbCrLf, selmediumno)
        End If

        '小類
        If Not String.IsNullOrWhiteSpace(selmediumsubno) Then
            sql_complex += String.Format("and  mediumsubno='{0}'" & vbCrLf, selmediumsubno)
        End If

        '搜尋欄位
        If Not String.IsNullOrWhiteSpace(keyword) Then
            sql_complex += "and v2014_list." & selOptions & " like '%" & keyword & "%'" & vbCrLf
        End If

        Dim Orderby As String = selSort & " " & selOrder
        Dim Where As String = ""

        '取出資料
        Dim ds As DataSet = EC.DB.Paging2005.ExecuteDataSet(PageSize, CurrentPage, sql_complex, Where, Orderby, False)
        Dim dt As DataTable = ds.Tables(0)

        TotalCount = ds.Tables("TableRowsOut").Rows(0).Item("Count")           '總筆數


        '輸出資料給 Repeater
        With rptdata
            .DataSource = dt
            .DataBind()
        End With

    End Sub

    Protected Sub rptdata_ItemCreated(sender As Object, e As RepeaterItemEventArgs) Handles rptdata.ItemCreated

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            Dim dt As DataRowView = TryCast(e.Item.DataItem, DataRowView)

            Dim ls3cproductno As String = dt.Row.Item("ls3cproductno").ToString
            Dim ProductNo As String = UCase(dt.Row.Item("ProductNo").ToString)
            Dim ProductName As String = dt.Row.Item("ProductName").ToString
            'Dim MediumSubName As String = dt.Row.Item("MediumSubName").ToString
            Dim Status As Integer = dt.Row.Item("Status")
            Dim StatusName As String = EC.ENUMS.GetEnum_ItemName(GetType(EC.ENUMS.ProdStatus), status)

            ls3cproductno = Replace(ls3cproductno, """", "")
            ProductName = Replace(ProductName, """", "")
            'MediumSubName = Replace(MediumSubName, """", "")
            ProductName = Replace(ProductName, "	", "")

            dt.Row.Item("ls3cproductno") = ls3cproductno
            dt.Row.Item("ProductName") = ProductName
            'dt.Row.Item("MediumSubName") = MediumSubName
            dt.Row.Item("StatusName") = StatusName
            dt.Row.Item("isZprd") = IIf(Left(ProductNo, 1) = "Z", "Y", "N")

        End If


    End Sub

End Class
