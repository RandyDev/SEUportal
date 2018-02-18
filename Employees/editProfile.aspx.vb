'Imports Telerik.Web.UI

Partial Class editProfile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim usrdal As New userDAL()
        Dim usr As ssUser = usrdal.getUserByName(User.Identity.Name)
        If Not IsPostBack Then
            lblQuestion.Text = usr.PasswordQuestion
            Dim edal As New empDAL()
            txtEmail.Text = usr.eMail
            txtPhone.Text = usr.Phone
            btnUpdateProfile.CommandArgument = usr.userID.ToString
            btnChangePassword.CommandArgument = usr.userID.ToString
            If User.IsInRole("Supervisor") Or User.IsInRole("Employee") Then
                If usr.eMail = usr.userName & "@Div-Log.com" Then txtEmail.Text = String.Empty
            Else '
                txtEmail.Enabled = False
                txtPhone.Enabled = False
                btnUpdateProfile.Visible = False
            End If
            loadCellCarrier()
        End If
    End Sub

    Private Sub loadCellCarrier()
        Dim dba As New DBAccess()
        dba.CommandText = "Select CarrierID, CarrierName FROM CellCarrierSMS ORDER BY CarrierName"
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        cbCellCarrier.DataSource = dt
        cbCellCarrier.DataValueField = "CarrierID"
        cbCellCarrier.DataTextField = "CarrierName"
        cbCellCarrier.DataBind()
    End Sub

    Private Sub btnChangePassword_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnChangePassword.Command
        Select Case e.CommandName
            Case "ChangePassword"
                Dim usrdal As New userDAL()
                Dim usr As ssUser = usrdal.getUserByID(New Guid(e.CommandArgument.ToString))
                Dim op As String = txtOldpass.Text.Trim()
                Dim a As String = usr.PasswordAnswer
                Dim np As String = txtNewpass.Text.Trim()
                Dim suc As Boolean = False
                Dim mUser As MembershipUser = Membership.GetUser(usr.userName)
                If txtAnswer.Text.Trim().Length > 0 And txtAnswer.Text.Trim() = usr.PasswordAnswer Then
                    Dim resetPwd As String = mUser.ResetPassword()
                    suc = mUser.ChangePassword(resetPwd, np)
                ElseIf txtOldpass.Text.Trim.Length > 3 Then
                    suc = mUser.ChangePassword(op, np)
                End If
                errMsg.Text = IIf(suc, "Your password has been changed.<br />Use your new password next time you log on.", "Unable to change password.<br />Try again or see a Manager")
        End Select
    End Sub



    Private Sub btnUpdateProfile_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnUpdateProfile.Command
        Dim strResponse As String = String.Empty
        Dim udal As New userDAL
        Dim usr As ssUser = udal.getUserByName(HttpContext.Current.User.Identity.Name)
        If txtEmail.Text.Length > 0 Then

            Dim email As String = txtEmail.Text.Trim()
            If Utilities.isValidEmail(email) Then
                strResponse = udal.UpdateEmail(usr.userID, txtEmail.Text.Trim())
            Else
                strResponse = "Invalid eMail Address"
            End If
        End If

        If txtPhone.Text.Length > 0 Then
            If txtPhone.Text.Length = 10 Then
                Dim phoneResponse As String = udal.UpdatePhone(usr.userID, txtPhone.TextWithLiterals)
                If phoneResponse.Length > 0 Then
                    If strResponse.Length > 0 Then strResponse &= "<br />"
                    strResponse &= phoneResponse
                End If
            Else
                If strResponse.Length > 0 Then strResponse &= "<br />"
                strResponse &= "Phone number incomplete"
            End If
        Else 'delete phone number
            Dim nophone As String = String.Empty
            strResponse = udal.UpdatePhone(usr.userID, nophone)
        End If

        errProfileMsg.Text = IIf(strResponse.Length > 0, strResponse, "Changes Saved")
        errProfileMsg.Visible = True

    End Sub


End Class