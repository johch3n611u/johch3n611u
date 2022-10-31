'/////////////////////////////////////////////////////////////////////////////////
' 呼叫商品Momo購物中心上架API
'
' 建檔人員: 育誠
' 建檔日期: 2019-11-27
' 修改記錄: 範例--> 日期 記錄人員 簡述
' 關連程式:
' 呼叫來源: 
'/////////////////////////////////////////////////////////////////////////////////
Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports AmazonUploadFile
Imports EC.Library.Security
Imports Newtonsoft.Json

Partial Class call_MomoAPI
    Inherits System.Web.UI.Page

    Public prglimit As EC.mng.Limit                    '讀取程式的權限

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Response.CacheControl = "no-cache"             '避免被 Cache 住
        'EC.mng.Login.LoginCheck()                      '未登入則導到登入頁
        'prglimit = EC.mng.Limit.CheckLimit(ViewState)  '檢查使用者權限
        Dim ProductNo = RequestString("ProductNo", RequestActMode.None, RequestMode.XSS) & ""
        Dim REQUEST = RequestString("RequestString", RequestActMode.None, RequestMode.XSS) & ""
        REQUEST = "POST_proposals"
        Select Case REQUEST
            'Case "GET_categories"
            '    YahooShoppingSCM_API.GET_categories_API()
            'Case "GET_struDataAttrClusters"
            '    YahooShoppingSCM_API.GET_struDataAttrClusters_API()
            Case "POST_proposals"
                Response.Write(MomoSCM_API.POST_proposals_API(ProductNo, "verifyReportGoods")) 'POST_proposals_API(ProductNo) 'POST_proposals_API 'POST_AttrIndex_API 'POST_webbrand_API
        End Select
    End Sub
End Class
