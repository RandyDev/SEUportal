Public Class EditClientProfile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim udal As New userDAL
            Dim usr As ssUser = udal.getUserByName(User.Identity.Name)
            txtFname.Text = usr.FirstName
            txtMi.Text = usr.MI
            txtLastName.Text = usr.LastName
            txtTitle.Text = usr.Title
            txtPhone.Text = usr.Phone
            txteMail.Text = usr.eMail
        End If
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If Utilities.isValidEmail(txteMail.Text.Trim()) Then
            Dim udal As New userDAL
            Dim usr As ssUser = udal.getUserByName(User.Identity.Name)
            usr.FirstName = txtFname.Text.Trim()
            usr.MI = txtMi.Text.Trim()
            usr.LastName = txtLastName.Text.Trim()
            usr.Title = txtTitle.Text.Trim()
            usr.Phone = txtPhone.Text.Trim()
            usr.eMail = txteMail.Text.Trim()

            Dim retString As String = udal.UpdateUser(usr)

            If retString > "" Then
                lblResult.Text = retString
                Exit Sub
            Else
                lblResult.Text = "Profile Information has been saved.<br/>"
            End If

            If txtOldPassword.Text.Trim().Length > 2 And txtPassword.Text.Trim().Length() > 2 Then
                Dim musr As MembershipUser = Membership.GetUser(usr.userName)
                If musr.ChangePassword(txtOldPassword.Text.Trim(), txtPassword.Text.Trim()) Then
                    lblResult.Text &= "New Password Saved!<br />Use new password next time you log in."
                Else
                    lblResult.Text = "Old password is incorrect"
                End If
            End If
        End If

    End Sub

End Class