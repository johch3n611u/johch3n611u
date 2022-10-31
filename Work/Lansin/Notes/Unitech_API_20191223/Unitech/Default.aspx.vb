'/////////////////////////////////////////////////////////////////////////////////
' 產品管理
'
' 建檔人員: 阿友
' 建檔日期: 2012-07-02
' 修改記錄: 2012-07-23 友: 
' 關連程式:
' 呼叫來源:
'/////////////////////////////////////////////////////////////////////////////////
Imports EC.Library.Security
Imports System.Data

Partial Class mng_Product_Default
    Inherits System.Web.UI.Page

    Public prgname As String = "產品"
    Public prglimit As EC.mng.Limit       '讀取程式的權限
    Public prgid As String = ""
    Public houseno As String = ""
    Public largeno As String = ""
    Public mediumno As String = ""
    Public mediumsubno As String = ""
    Public _currentPath As String = EC.Library.Site.GetCurrentPath   'ex: /mng/Product/Category/LargeNo/

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Response.CacheControl = "no-cache"             '避免被 Cache 住
        EC.mng.Login.LoginCheck()                      '未登入則導到登入頁
        prglimit = New EC.mng.Limit(ViewState)         '讀取程式的權限
        prgid = EC.mng.Limit.GetPrgID(ViewState)

        '自訂要讀取的某程式的權限
        'Dim prglimit2 As New EC.mng.Limit(ViewState, 12, "/mng/limit/menu/default.aspx", 321, 2)

        '參數值
        houseno = EC.Library.Security.RequestString("houseno", RequestActMode.None, RequestMode.SQLInjection)          '館別
        largeno = EC.Library.Security.RequestString("largeno", RequestActMode.None, RequestMode.SQLInjection)          '大類
        mediumno = EC.Library.Security.RequestString("mediumno", RequestActMode.None, RequestMode.SQLInjection)        '中類
        mediumsubno = EC.Library.Security.RequestString("mediumsubno", RequestActMode.None, RequestMode.SQLInjection)  '小類


    End Sub

End Class
