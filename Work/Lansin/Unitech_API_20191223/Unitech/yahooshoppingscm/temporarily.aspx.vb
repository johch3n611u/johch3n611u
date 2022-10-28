Partial Class temporarily
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim data = YahooSCM_API.Get_temporarily_PostManData
        Dim timestamp = data(0)
        Dim signature = data(1)
        Response.Write("timestamp = " + timestamp + "</br>signature = " + signature)
    End Sub

End Class
