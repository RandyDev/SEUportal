Imports Telerik.Web.UI

Public Class _Default

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '            If Request.IsAuthenticated Then
            Dim usrDAL As New userDAL()
            Dim usr As String = HttpContext.Current.User.Identity.Name
            Dim cuser As ssUser = usrDAL.getUserByName(usr)
            lbluserName.Text = "William" & " " & "Tell"
            Utilities.setSessionVars(cuser)

            '            lblUserName.Text = cuser.FirstName & " " & cuser.LastName
            RadMenu1.DataSourceID = "XmlDataSource0"

            'Else
            '    Response.Redirect(FormsAuthentication.LoginUrl)
            'End If
        End If

        If Not Request.IsAuthenticated Then
            lnkLogOut.Visible = "False"
        End If

    End Sub

    Protected Sub RadMenu1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadMenu1.DataBound
        If Not IsPostBack Then
            For Each myItem As RadMenuItem In RadMenu1.Items
                If Not myItem.Text = "Home" Then assignTarget(myItem)
            Next
            If Roles.IsUserInRole("Client") Then
                cPane.ContentUrl = "~/ClientSvcs/Default.aspx"
            Else
                cPane.ContentUrl = "~/homepg.aspx"
            End If
        End If
    End Sub

    Private Sub assignTarget(ByVal item As RadMenuItem)
        item.Target = cPane.ClientID
        If (item.Items.Count <> 0) Then
            For Each myItem As RadMenuItem In item.Items
                assignTarget(myItem)
            Next
        End If

    End Sub

    Private Sub LoginStatus2_LoggedOut(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginStatus2.LoggedOut
        FormsAuthentication.SignOut()
        Session.Abandon()
        Dim cookie1 = New HttpCookie(FormsAuthentication.FormsCookieName, "")
        cookie1.Expires = Date.Now.AddYears(-1)
        Response.Cookies.Add(cookie1)
        Dim cookie2 As HttpCookie = New HttpCookie("ASP.NET_SessionId", "")
        cookie2.Expires = DateTime.Now.AddYears(-1)
        Response.Cookies.Add(cookie2)
        FormsAuthentication.RedirectToLoginPage()
    End Sub

    Private Sub RadMenu1_ItemClick(sender As Object, e As Telerik.Web.UI.RadMenuEventArgs) Handles RadMenu1.ItemClick
        If e.Item.Text = "Home" Then
            e.Item.Target = "blank"
        End If


    End Sub
End Class