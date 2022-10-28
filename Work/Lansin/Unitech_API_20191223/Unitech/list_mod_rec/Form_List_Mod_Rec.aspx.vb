'/////////////////////////////////////////////////////////////////////////////////
' 商品異動記錄列表
'
' 建檔人員: 阿友
' 建檔日期: 2012-08-28
' 修改記錄: 範例--> 日期 記錄人員 簡述
' 關連程式:
' 呼叫來源:
'/////////////////////////////////////////////////////////////////////////////////
Imports System.Data
Imports EC.Library.Security

Partial Class mng_product_list_Form_List_Mod_Rec
    Inherits System.Web.UI.Page

    Public cno As Integer = 0
    Public list_NewCNO As Integer = 0
    Public productname As String = ""      '產品名稱
    Public _Count As Integer = 0           '資料總筆數
    Public _TopN As Integer = 100          '顯示筆數

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Response.CacheControl = "no-cache"    '避免被 Cache 住 
        cno = RequestNumeric("CNO", RequestActMode.None, RequestMode.SQLInjection)                        '一般品的 CNO      (list.cno)
        list_NewCNO = RequestNumeric("list_NewCNO", RequestActMode.None, RequestMode.SQLInjection)        '未處理新品的 CNO  (list_new.id)

        Dim ISAdministrator_Or_ISDeveloper As Boolean = EC.mng.Func.User_ISAdministrator_Or_ISDeveloper()

        '管理者/程式人員 才可看異動記錄
        'If ISAdministrator_Or_ISDeveloper Then

        Dim dt As DataTable

        If cno > 0 Then  '取 一般品 的Log
            Dim tb As Ls3c_v2_2005.list = Ls3c_v2_2005.list.Load(cno)   '取產品資料
            productname = tb.ProductName
            dt = Ls3c_v2_2005.List_MOD_REC.ExecuteDataTable(cno, _TopN)
        Else  '取 未處理新品 的Log
            Dim tb As Ls3c_v2_2005.list_new = Ls3c_v2_2005.list_new.Load(list_NewCNO)   '取產品資料
            productname = tb.PName
            dt = Ls3c_v2_2005.List_MOD_REC.ExecuteDataTable(tb.Prod, _TopN)
        End If

        _Count = dt.Rows.Count
        With rpt_list_mod_rec
            .DataSource = dt
            .DataBind()
        End With

        'End If

    End Sub

End Class
