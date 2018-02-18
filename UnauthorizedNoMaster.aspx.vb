Partial Class UnauthorizedNoMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ip As String = Request.ServerVariables("REMOTE_ADDR")
        Dim dt As String = Date.Now.ToString
        lblIP.Text = ip
        lblTime.Text = dt

    End Sub

End Class