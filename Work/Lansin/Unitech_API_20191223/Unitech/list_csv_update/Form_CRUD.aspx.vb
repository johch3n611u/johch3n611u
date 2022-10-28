'/////////////////////////////////////////////////////////////////////////////////
' 表單 (新增/修改)
'
' 建檔人員: 阿友
' 建檔日期: 2013-08-28
' 修改記錄: 範例--> 日期 記錄人員 簡述
' 關連程式: 
' 呼叫來源: 
'/////////////////////////////////////////////////////////////////////////////////
Imports EC.Library.Security
Imports System.Data
Partial Class mng_ShoppingCash_SC_UserList_Form_CRUD
    Inherits System.Web.UI.Page


    Public PrgID As String = ""
    Public prglimit As EC.mng.Limit       '讀取程式的權限
    Public _ID As Integer = 0
    Public tb As New Ls3c_v2_2005.ShoppingCash_UserGroup
    Public GroupUseEvtNosStatus As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.CacheControl = "no-cache"             '避免被 Cache 住
        EC.mng.Login.LoginCheck()                      '未登入則導到登入頁
        prglimit = New EC.mng.Limit(ViewState)         '讀取程式的權限

        PrgID = SQLIJ(Request("PrgID"))                '主選單的MENUID,判斷權限使用.
        _ID = RequestNumeric("ID", RequestActMode.None, RequestMode.SQLInjection)

        If Not IsNumeric(_ID) Then _ID = 0




    End Sub

End Class
