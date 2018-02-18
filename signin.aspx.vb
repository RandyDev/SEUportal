'Imports System.Data
'Imports DiversifiedLogistics.ssUser
'Imports System.Web
Imports DiversifiedLogistics.Utilities
'Imports System.IO
'Imports System.Security.Cryptography


Public Class signin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserName.Focus()
        If Not Page.IsPostBack Then
            If Request.IsAuthenticated AndAlso Not String.IsNullOrEmpty(Request.QueryString("ReturnUrl")) Then
                ' This is an unauthorized, authenticated request...
                Response.Redirect("~/UnauthorizedNoMaster.aspx")
            End If
        End If
    End Sub

    Protected Sub LoginButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginButton.Click
        Dim udal As New userDAL()

        Dim usr As ssUser = udal.getUserByName(UserName.Text)
        If Not usr.myRoles Is Nothing Then
            If usr.myRoles.Contains("Carrier") Or usr.myRoles.Contains("Client") Or usr.myRoles.Contains("Vendor") Then

                If usr.LastLoginDate < Date.Now.AddDays(-30) And usr.LastActivityDate < Date.Now.AddDays(-30) Then
                    usr.IsApproved = False
                    udal.UpdateUser(usr)
                    FailureText.Text = "This account has been disabled due to:<br />... no activity 30+ days ...<br />Please call 904-491-6800 and request to be reactivated."
                    FailureText.Visible = True
                    FormsAuthentication.SignOut()
                    Session.Abandon()
                    Exit Sub
                End If
            End If
        End If

        If Membership.ValidateUser(UserName.Text, Password.Text) Then
            ' Log the user into the site
            FormsAuthentication.RedirectFromLoginPage(UserName.Text, RememberMe.Checked)
        End If
        ' If we reach here, the user's credentials were invalid
        FailureText.Text = LoginError(UserName.Text)
        FailureText.Visible = True
    End Sub

    Protected Function LoginError(ByVal usr As String) As String
        '     Determine why the user could not login...
        LoginError = "Unable to log you in with provided credentials.<br />If you feel this is in error, please contact the Administrator<br />Administrator may ask for the following information:<br /><table align='center'><tr><td align='right'>Your IP Address:</td><td align='left' style='padding-left:7px;'>" & Request.ServerVariables("REMOTE_ADDR") & "</td></tr><tr><td align='right'>Date/Time: </td><td align='left' style='padding-left:7px;'>" & Date.Now.ToString() & "</td></tr></table>"
        '     Does there exist a User account for this user?
        Dim usrInfo As MembershipUser = Membership.GetUser(usr)
        If usrInfo IsNot Nothing Then
            '     Is this user locked out?
            If usrInfo.IsLockedOut Then
                LoginError = "Your account has been locked out because of too many invalid login attempts.<br />Please call 904-491-6800 to have your account unlocked."
            ElseIf Not usrInfo.IsApproved Then
                If usrInfo.LastActivityDate < Date.Now.AddDays(-30) Then
                    LoginError = "This account has been disabled due to:<br />... no activity 30+ days ...<br />Please call 904-491-6800 and request to be reactivated."
                Else
                    LoginError = "Your account has not yet been approved. <br />You cannot login until an administrator has approved your account."
                End If
            End If
        End If
    End Function

    Private Sub lbtnForgotPassword_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles lbtnForgotPassword.Command
        Dim unam As String = UserName.Text.Trim()
        If e.CommandName = "forgotPassword" Then
            RememberMe.Visible = False
            LoginButton.Visible = False
            lblHeader.Text = "Retrieve Password"
            Dim usrdal As New userDAL()

            Dim uname As ssUser = usrdal.getUserByName(unam)
            If uname.userName IsNot Nothing Then    ' you are a user
                'have you set your passwordquestion?
                ' are you a client or employee
                If uname.myRoles.Contains("Carrier") Or uname.myRoles.Contains("Client") Or uname.myRoles.Contains("Vendor") Then
                    lblHeader.Text = "Please Log In"
                    LoginButton.Visible = True
                    FailureText.Text = "We can send your password to: " & uname.eMail
                    btnSendNewPassword.Visible = True
                    btnSendNewPassword.CommandArgument = uname.eMail
                    btnSendNewPassword.CommandName = unam
                    btnNoPass.Visible = True

                Else

                    If uname.PasswordQuestion <> "na" Then ' you have selected/set a security/password question
                        lblQuestion.Text = uname.PasswordQuestion
                        lblQuestion.Visible = True
                        lblUserName.Text = "Response: "
                        UserName.Text = ""

                        rpInstructions.Visible = True
                        '                        Dim musr As MembershipUser = Membership.GetUser(UserName.Text)
                        '                        Dim tpass As String = musr.GetPassword()
                        Password.Enabled = False
                        Password.Visible = False
                        PasswordLabel.Visible = False
                        lbtnForgotPassword.CommandArgument = unam
                        lbtnForgotPassword.CommandName = "ChangePassword"
                        lbtnForgotPassword.Text = " Show my password "
                        btnSendNewPassword.Text = "eMail my password"
                        btnSendNewPassword.Visible = True
                        btnSendNewPassword.CommandArgument = uname.eMail
                        btnSendNewPassword.CommandName = unam


                    Else 'you have not set up password question
                        FailureText.Text = "<font color=""red"">You have not yet set your security question<br />Try the default password or please contact your Supervisor/Manager </font>"
                        FailureText.Visible = True
                        lbtnForgotPassword.Visible = False
                        lblHeader.Text = "Please Log In"
                        lblUserName.Text = "Login ID: "
                        RememberMe.Visible = True
                        LoginButton.Visible = True
                        Password.Focus()
                    End If
                End If
            Else 'you are not a user
                FailureText.Text = "<font color=""red"">Login ID not valid</font>"
                FailureText.Visible = True
                lblQuestion.Text = ""
                lbtnForgotPassword.Text = "forgot password"
                lbtnForgotPassword.CommandName = "forgotPassword"
                rpInstructions.Visible = False
                LoginButton.Visible = True
                lblHeader.Text = "Please Log In"
                btnNoPass.Visible = False
                btnSendNewPassword.Visible = False
            End If

        ElseIf e.CommandName = "ChangePassword" Then
            Dim ans As String = unam
            Dim udal As New userDAL
            Dim usr As ssUser = udal.getUserByName(e.CommandArgument)
            lblHeader.Text = "Please Log In"
            lblUserName.Text = "Login ID: "
            UserName.Text = usr.userName
            RememberMe.Visible = True
            LoginButton.Visible = True
            rpInstructions.Visible = False
            If usr.PasswordAnswer = ans Then
                Dim musr As MembershipUser = Membership.GetUser(usr.userName)
                Dim tpass As String = musr.GetPassword() 'musr.ResetPassword()
                PasswordLabel.Visible = True
                Password.Visible = True
                Password.Enabled = True
                lblQuestion.Visible = False
                btnSendNewPassword.Visible = False
                '                musr.ChangePassword(tpass, Password.Text.Trim())
                RememberMe.Enabled = True
                LoginButton.Visible = True
                FailureText.Text = "Here ya go: " & tpass 'tpass & " Your password has been changed<br />Please use your NEW password to log in."
                lbtnForgotPassword.Visible = False
                lbtnForgotPassword.Visible = True
                lbtnForgotPassword.Text = "forgot password"
                lbtnForgotPassword.CommandName = "forgotPassword"
                'change it

            Else
                Dim musr As MembershipUser = Membership.GetUser(usr.userName)
                '                musr.IsApproved = False
                lbtnForgotPassword.Visible = False
                lbtnForgotPassword.CommandArgument = Nothing
                FailureText.Text = "Your response was incorrect.<br />You will have to use your existing password. <br />We may be able to eMail it or you may contact a Manager to have it changed.<br />Your IP address: " & Request.ServerVariables("REMOTE_ADDR")
                Password.Visible = True
                PasswordLabel.Visible = True
                lblQuestion.Visible = False
                'lock it
            End If
            '            Password.Focus()

        End If
    End Sub

    Private Sub btnSendNewPassword_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnSendNewPassword.Command
        If isValidEmail(e.CommandArgument) Then
            FailureText.Text = "Do you have access to:<br />" & e.CommandArgument
            btnSendNewPassword.CommandArgument = Replace(btnSendNewPassword.CommandArgument, ".", "~")
            btnNoPass.Visible = True
            btnSendNewPassword.Visible = True
            btnSendNewPassword.Attributes.Add("OnClientClick", "this.disabled='true';this.value='Processing...';return true;")
        Else
            Dim eml As String = Replace(btnSendNewPassword.CommandArgument, "~", ".")
            Dim udal As New userDAL
            Dim ssuser As ssUser = udal.getUserByName(e.CommandName)
            Dim usr As MembershipUser = Membership.GetUser(e.CommandName)
            Dim tempPW As String = usr.GetPassword()

            '            Dim tempPW As String = usr.ResetPassword()
            '            strResponse = usr.UserName & "welcome"


            Dim msg As New rwMailer()
            msg.isBodyHtml = True
            msg.To = eml
            msg.Subject = "SEU.Div-Log Password Retrieval"
            msg.Body = "Hello " & ssuser.FirstName & " -<br /><br />Your password is..: " & tempPW & "<br />"
            msg.Body &= "Please guard it carefully.<br />If you forget or loose it then "
            msg.Body &= "we will, of course, <br />send it again ... but still ... ;-)<br /><br />"
            msg.Body &= "Regards - <br />SEU Support Team<br /><br />"
            '            msg.BCC = "wwalklett@Div-Log.com"
            rwMailer.SendMail(msg)
            '            If ret = "Message Sent" Then
            FailureText.Text = "Your password is being sent to you now.  <br />Please check your eMail."
            '            Else
            '            FailureText.Text = ret

            '        End If
            resetLogin()
            btnNoPass.Visible = False
            btnSendNewPassword.Visible = False
        End If
    End Sub
    Private Sub resetLogin()
        lblHeader.Text = "Please Log In"
        lblQuestion.Visible = False
        lblUserName.Text = "Login ID:"
        PasswordLabel.Visible = True
        Password.Visible = True
        lbtnForgotPassword.Visible = True
        lbtnForgotPassword.Text = "forgot password"
        lbtnForgotPassword.CommandName = "forgotPassword"
        lbtnForgotPassword.CommandArgument = Nothing
        LoginButton.Visible = True
        rpInstructions.Visible = False
    End Sub


    Private Sub btnNoPass_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNoPass.Click
        FailureText.Text = "Please call 904-491-6800 between 9am and 5pm <br />and request a new password<br />SEU Employees, contact your manager"
        btnNoPass.Visible = False
        btnSendNewPassword.Visible = False
        Password.Visible = True
        PasswordLabel.Visible = True
        lblHeader.Text = "Please Log In"
        lblUserName.Text = "Login ID: "
        lblQuestion.Visible = False
        rpInstructions.Visible = False
        LoginButton.Visible = True
    End Sub


End Class
