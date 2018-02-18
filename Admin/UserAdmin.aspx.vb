'Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class UserAdmin
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            '            BindGrid()
            BindRolesToList()
            clearForm()
            loadTitles()
            Session("uaRoleList") = Nothing
            RadGrid1.Visible = False
        End If
    End Sub

    Private Sub loadTitles()
        Dim dba As New DBAccess()
        dba.CommandText = "select distinct jobtitle from employment order by jobtitle"
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        cbTitle.DataSource = dt
        cbTitle.DataTextField = "jobtitle"
        cbTitle.DataBind()

    End Sub

    Private Function validateForm() As String
        Dim strResponse As String = Nothing
        Dim errList As New List(Of String)
        If txtFname.Text.Trim() = String.Empty Then
            errList.Add("First name is required")
            lblerrtxtFname.Style.Item("display") = "inline"
        End If
        If txtLname.Text.Trim() = String.Empty Then
            errList.Add("Last name is required")
            lblerrtxtLname.Style.Item("display") = "inline"
        End If
        If txtEmail.Text.Trim() = String.Empty Then
            errList.Add("eMail Address required")
            lblerrtxtEmail.Style.Item("display") = "inline"
        Else
            Dim myRegex As New Regex("\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
            If Not myRegex.IsMatch(txtEmail.Text.Trim()) Then
                errList.Add("eMail Address Invalid")
                lblerrtxtEmail.Style.Item("display") = "inline"
            End If
        End If
        If txtUserName.Text.Trim() = String.Empty Then
            errList.Add("LoginID is required")
            lblerrtxtUserName.Style.Item("display") = "inline"
        End If
        If errList.Count > 0 Then

            For Each itm In errList
                strResponse &= itm & "<br />"
            Next
            strResponse = Left(strResponse, Len(strResponse) - 6)
        End If
        Return strResponse
    End Function

    Private Sub ClearValidators()
        lblerrtxtFname.Style.Item("display") = "none"
        lblerrtxtLname.Style.Item("display") = "none"
        lblerrtxtUserName.Style.Item("display") = "none"
        lblerrtxtEmail.Style.Item("display") = "none"
        lblResponse.Text = Nothing
    End Sub

    Protected Sub clearForm()
        ClearValidators()
        lblEditType.Text = "Add New"
        txtEmail.Text = ""
        txtFname.Text = ""
        txtLname.Text = ""
        txtUserName.Text = ""
        lbLocationList.Items.Clear()
        lbAvailableLocations.Items.Clear()
        loadUserLocationSelectList()
        txtUserName.Visible = True
        lblUserName.Visible = False
        lblUserName.Text = ""
        txtPassword.Text = ""
        txtPassword.Visible = True
        lbtnResetPassword.Visible = False
        lbtnResetPassword.ToolTip = Nothing
        txtEmail.Text = ""
        txtPhone.Text = ""
        txtCellText.Text = ""
        chkIsApproved.Checked = True
        btnSubmit.Text = "Add User"
        btnSubmit.CommandName = "Insert"
        btnSubmit.CommandArgument = Nothing
        lblResponse.Visible = False
        For Each ri As RepeaterItem In UsersRoleList.Items
            Dim RoleCheckBox As CheckBox = CType(ri.FindControl("RoleCheckBox"), CheckBox)
            RoleCheckBox.Checked = (RoleCheckBox.Text = "Administrator")
            If (RoleCheckBox.Text = "Supervisor") Or (RoleCheckBox.Text = "Employee") Then
                RoleCheckBox.Enabled = False
            End If
        Next
        pnlLocations.Visible = False
        btnDelete.Visible = False
        '        RadGrid1.Rebind()
    End Sub

    Private Sub RadGrid1_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If e.CommandName = "RowClick" Then
            clearForm()
            lblResponse.Text = String.Empty
            lblResponse.Visible = False
            Dim sas As String = RadGrid1.SelectedValue
            Dim udal As New userDAL()
            Dim cuser As ssUser = udal.getUserByName(sas)
            Dim usrInfo As MembershipUser = Membership.GetUser(sas)
            'UNlock on load  :)
            If usrInfo.IsLockedOut Then
                usrInfo.UnlockUser()
                lblResponse.Text = "Lock-Out has been cleared"
                lblResponse.Visible = True
            End If
            txtFname.Text = cuser.FirstName
            txtLname.Text = cuser.LastName
            cbTitle.Text = cuser.Title

            txtEmail.Text = cuser.eMail
            txtPhone.Text = cuser.Phone
            txtCellText.Text = cuser.cellText

            txtUserName.Text = sas
            txtUserName.Visible = False
            lblUserName.Visible = True
            lblUserName.Text = sas

            txtPassword.Visible = False
            lbtnResetPassword.CommandArgument = cuser.userID.ToString
            lbtnResetPassword.Visible = True
            lbtnResetPassword.Enabled = True
            lbtnResetPassword.Text = "Reset Password"
            lbtnResetPassword.ToolTip = "Click here to reset password AND security question." & vbCrLf & "Password will be reset to: " & cuser.userName & "welcome"
            chkIsApproved.Checked = cuser.IsApproved

            btnSubmit.Text = "Save Changes"
            btnSubmit.CommandName = "Update"
            btnSubmit.CommandArgument = cuser.userID.ToString
            lblEditType.Text = "Edit"

            BindRolesToList()
            CheckRolesForSelectedUser(sas)

            lbLocationList.Items.Clear()
            lbAvailableLocations.Items.Clear()
            loadUserLocaList(cuser)
            pnlLocations.Visible = cuser.myRoles.Count = 1 And cuser.myRoles(0) = "Manager"

            btnDelete.Visible = Not Roles.IsUserInRole(sas, "SysOp")

        End If
    End Sub


    Public Sub chkLocaList(ByVal sender As Object, ByVal e As System.EventArgs)
        ' get selected roles, default to 'Recruiter' if empty
        Dim rlc As CheckBox = CType(sender, CheckBox)
        Dim rlstr As String = String.Empty
        Select Case rlc.Checked
            Case True
                Select Case rlc.Text
                    Case "SysOp"
                        rlstr = "Administrator,Manager,Supervisor,Employee"
                    Case "Administrator"
                        rlstr = "SysOp,Manager,Supervisor,Employee"
                    Case "Manager"
                        rlstr = "SysOp,Administrator,Employee"
                    Case "Supervisor"
                        rlstr = "SysOp,Administrator"
                    Case "Clerk"
                        rlstr = ""
                    Case "Employee"
                        rlstr = "SysOp,Administrator,Manager"
                End Select
        End Select
        Dim unselectUsersRoles() As String = Split(rlstr, ",")
        ' Loop through the Repeater's Items and check or uncheck the checkbox as needed 
        For Each ri As RepeaterItem In UsersRoleList.Items
            ' Programmatically reference the CheckBox 
            Dim RoleCheckBox As CheckBox = CType(ri.FindControl("RoleCheckBox"), CheckBox)
            ' See if RoleCheckBox.Text is in selectedUsersRoles 
            If Linq.Enumerable.Contains(Of String)(unselectUsersRoles, RoleCheckBox.Text) Then
                RoleCheckBox.Checked = False
            End If
        Next
        Dim rlst As New List(Of String)
        For Each ri As RepeaterItem In UsersRoleList.Items
            Dim RoleCheckBox As CheckBox = CType(ri.FindControl("RoleCheckBox"), CheckBox)
            If RoleCheckBox.Checked = True Then
                Dim rl As String = RoleCheckBox.Text
                rlst.Add(rl)
            End If
        Next
        pnlLocations.Visible = rlst.Count = 1 And rlst.Contains("Manager")
    End Sub

    Private Sub loadUserLocaList(ByVal usr As ssUser)
        If usr.myRoles.Count > 0 And usr.myRoles.Contains("Manager") Then 'populate the location selectors.
            ' get current list
            Dim dba As New DBAccess()
            dba.CommandText = "SELECT UserLocations.locationID as ID, Location.Name " & _
                "FROM UserLocations INNER JOIN " & _
                "Location ON UserLocations.locationID = Location.ID " & _
                "WHERE (UserLocations.userID = @userID)"
            dba.AddParameter("@userID", usr.userID)
            Dim userLocationListDataTable As New DataTable
            userLocationListDataTable = dba.ExecuteDataSet.Tables(0)
            lbLocationList.Items.Clear()
            If userLocationListDataTable.Rows.Count > 0 Then
                lbLocationList.DataSource = userLocationListDataTable
                lbLocationList.DataTextField = "Name"
                lbLocationList.DataValueField = "ID"
                lbLocationList.DataBind()
            End If
        End If
        loadUserLocationSelectList()
    End Sub

    Private Sub loadUserLocationSelectList()
        Dim dba As New DBAccess()
        dba.CommandText = "Select ID, Name FROM Location ORDER BY Name"
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        lbAvailableLocations.Items.Clear()
        lbAvailableLocations.DataSource = dt
        lbAvailableLocations.DataValueField = "ID"
        lbAvailableLocations.DataTextField = "Name"
        lbAvailableLocations.DataBind()
        Dim assignedLocations As RadListBoxItemCollection = lbLocationList.Items
        For Each loca As RadListBoxItem In assignedLocations
            Dim itemToRemove As RadListBoxItem = lbAvailableLocations.FindItemByText(loca.Text)
            lbAvailableLocations.Items.Remove(itemToRemove)
        Next
    End Sub

    Private Sub lbtnResetPassword_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles lbtnResetPassword.Command
        If e.CommandName = "ResetPassword" Then
            Dim udal As New userDAL()
            lbtnResetPassword.Text = (udal.ResetPasswordandSecurity(e.CommandArgument))
            lbtnResetPassword.Enabled = False
            lbtnResetPassword.ToolTip = "Password and Security Question have been reset." & vbCrLf & "User will be prompted to reset security question at next login."
        End If
    End Sub



    'Sub RebindGrid(ByVal sender As Object, ByVal e As EventArgs)
    '    '        BindGrid()
    'End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        clearForm()
        RadGrid1.Rebind()
    End Sub

    Private Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim udal As New userDAL()
        Dim gusers As New List(Of ssUser)
        If Session("uaRoleList") Is Nothing Then Session("uaRoleList") = "Show All"
        Dim selRole As String = Session("uaRoleList")

        '        If selRole = "SysOp" Or selRole = "Administrator" Or selRole = "Manager" Or selRole = "Supervisor" Or selRole = "Clerk" Or selRole = "Employee" Then
        If selRole = "Show All" Then
            gusers = udal.getUsers()
        Else
            gusers = udal.getUsersByRole(selRole)
            RadGrid1.Visible = True
        End If
        RadGrid1.DataSource = gusers
    End Sub

    Sub BindGrid()
        'allUsers
        RadGrid1.DataBind()
        '      Dim userTable As New StringBuilder
    End Sub

#Region "Roles"

    Public Function RemoveElementFromArray(ByVal objArray As System.Array, ByVal objElement As Object, ByVal objType As System.Type) As Array
        Dim objArrayList As New ArrayList(objArray)
        objArrayList.Remove(objElement)
        Return objArrayList.ToArray(objType)
    End Function

    Private Sub BindRolesToList()
        ' Get all of the roles 
        'Dim roleNames() As String = Roles.GetAllRoles()
        'Dim rls() As String = RemoveElementFromArray(roleNames, "Client", GetType(String))
        'rls = RemoveElementFromArray(rls, "Carrier", GetType(String))
        'rls = RemoveElementFromArray(rls, "Vendor", GetType(String))
        Dim rls() As String = Split("SysOp Administrator Manager Supervisor Clerk Employee")
        UsersRoleList.DataSource = rls
        UsersRoleList.DataBind()
    End Sub

    Private Sub CheckRolesForSelectedUser(ByVal user As String)
        ' Determine what roles the selected user belongs to 

        Dim selectedUsersRoles() As String = Roles.GetRolesForUser(user)

        ' Loop through the Repeater's Items and check or uncheck the checkbox as needed 
        For Each ri As RepeaterItem In UsersRoleList.Items
            ' Programmatically reference the CheckBox 
            Dim RoleCheckBox As CheckBox = CType(ri.FindControl("RoleCheckBox"), CheckBox)
            ' See if RoleCheckBox.Text is in selectedUsersRoles 
            If Linq.Enumerable.Contains(Of String)(selectedUsersRoles, RoleCheckBox.Text) Then
                RoleCheckBox.Checked = True
            Else
                RoleCheckBox.Checked = False
            End If
        Next
    End Sub

    Private Sub PreSelectRole(ByVal roles As String)

        Dim selectedUsersRoles() As String = Split(roles, ",")
        For Each ri As RepeaterItem In UsersRoleList.Items
            ' Programmatically reference the CheckBox 
            Dim RoleCheckBox As CheckBox = CType(ri.FindControl("RoleCheckBox"), CheckBox)
            ' See if RoleCheckBox.Text is in selectedUsersRoles 
            If Linq.Enumerable.Contains(Of String)(selectedUsersRoles, RoleCheckBox.Text) Then
                RoleCheckBox.Checked = True
            Else
                RoleCheckBox.Checked = False
            End If
        Next


    End Sub

#End Region

    'Protected Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles RadGrid1.ItemDataBound
    '    'Is it a GridDataItem
    '    If (TypeOf (e.Item) Is GridDataItem) Then
    '        'Get the instance of the right type
    '        Dim databoundItem As GridDataItem = e.Item
    '        Dim LockedOut As String = databoundItem("IsLockedOut").Text
    '        If LockedOut = "True" Then
    '            databoundItem("IsLockedOut").Text = "YES"
    '        Else
    '            databoundItem("IsLockedOut").Text = ""
    '        End If
    '        Dim IsOnline As String = databoundItem("IsOnline").Text
    '        If IsOnline = "True" Then
    '            databoundItem("IsOnline").Text = "YES"
    '        Else
    '            databoundItem("IsOnline").Text = ""
    '        End If

    '    End If
    'End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim udal As New userDAL()
        Dim strResponse As String = udal.deleteUser(txtUserName.Text)
        If strResponse.Length < 1 Then
            clearForm()
            RadGrid1.DataBind()
        Else
            lblResponse.Text = strResponse
            lblResponse.Visible = True
        End If
    End Sub

    Private Sub btnSubmit_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnSubmit.Command
        ClearValidators()
        Dim strResponse As String = validateForm()
        If strResponse Is Nothing Then

            Select Case e.CommandName
                Case "Insert"
                    Dim nUser As New ssUser
                    nUser.FirstName = txtFname.Text.Trim
                    nUser.MI = ""
                    nUser.LastName = txtLname.Text
                    nUser.Title = cbTitle.Text.Trim()
                    nUser.eMail = txtEmail.Text.Trim()
                    nUser.Phone = txtPhone.Text.Trim()
                    nUser.cellText = txtCellText.Text.Trim()
                    nUser.userName = txtUserName.Text.Trim()
                    nUser.Password = IIf(txtPassword.Text = String.Empty, nUser.userName & "welcome", txtPassword.Text.Trim())
                    nUser.IsApproved = chkIsApproved.Checked
                    ' get selected roles, default to 'Recruiter' if empty
                    Dim rlst As New List(Of String)
                    For Each ri As RepeaterItem In UsersRoleList.Items
                        Dim RoleCheckBox As CheckBox = CType(ri.FindControl("RoleCheckBox"), CheckBox)
                        If RoleCheckBox.Checked = True Then
                            Dim rl As String = RoleCheckBox.Text
                            rlst.Add(rl)
                        End If
                    Next
                    If rlst.Count < 1 Then
                        strResponse = "Select Role for this user"
                        lblResponse.Text = strResponse
                        lblResponse.Visible = True
                        Exit Sub
                    End If
                    nUser.myRoles = rlst
                    Dim udal As New userDAL

                    strResponse = udal.addUser(nUser)
                    If strResponse = "The user account was successfully created!" Then
                        btnSubmit.Text = "Save Changes"
                        btnSubmit.CommandName = "Update"
                        btnSubmit.CommandArgument = nUser.userID.ToString
                        '                        RadGrid1.Rebind()
                        txtUserName.Visible = False
                        lblUserName.Visible = True
                        lblUserName.Text = nUser.userName
                        txtPassword.Visible = False
                        lbtnResetPassword.Visible = True
                        lbtnResetPassword.Text = nUser.Password
                        lbtnResetPassword.Enabled = False
                    End If

                Case "Update"
                    Dim udal As New userDAL
                    Dim updatedUser As ssUser = udal.getUserByID(New Guid(e.CommandArgument.ToString))
                    updatedUser.userName = txtUserName.Text

                    updatedUser.eMail = txtEmail.Text.Trim()

                    updatedUser.IsApproved = chkIsApproved.Checked
                    ' get selected roles,  throw error if empty
                    Dim rlst As New List(Of String)
                    For Each ri As RepeaterItem In UsersRoleList.Items
                        Dim RoleCheckBox As CheckBox = CType(ri.FindControl("RoleCheckBox"), CheckBox)
                        If RoleCheckBox.Checked = True Then
                            Dim rl As String = RoleCheckBox.Text
                            rlst.Add(rl)
                        End If
                    Next
                    If rlst.Count < 1 Then
                        strResponse = "Select Role for this user"
                        lblResponse.Text = strResponse
                        lblResponse.Visible = True
                        Exit Sub
                    End If
                    updatedUser.myRoles = rlst

                    updatedUser.FirstName = txtFname.Text.Trim()
                    updatedUser.LastName = txtLname.Text.Trim()
                    updatedUser.Title = cbTitle.Text.Trim()
                    updatedUser.Phone = txtPhone.Text.Trim()
                    updatedUser.cellText = txtCellText.Text.Trim()
                    strResponse = udal.UpdateUser(updatedUser)
                    If Not strResponse = String.Empty Then
                        lblResponse.Text = strResponse
                        lblResponse.Visible = True
                        Exit Sub
                    End If
                    ' go ahead and delete all user/location entries
                    Dim dba As New DBAccess()
                    dba.CommandText = "DELETE FROM UserLocations WHERE userID=@userID"
                    dba.AddParameter("@userID", updatedUser.userID)
                    dba.ExecuteNonQuery()
                    ' if they are only a site manager, store  the user/locations 
                    If updatedUser.myRoles.Contains("Manager") Then 'populate the location selectors.
                        For Each it As RadListBoxItem In lbLocationList.Items
                            dba.CommandText = "INSERT INTO UserLocations (userID, locationID) VALUES (@userID, @locationID)"
                            dba.AddParameter("@userID", updatedUser.userID)
                            dba.AddParameter("@locationID", New Guid(it.Value))
                            Dim l As String = it.Text

                            dba.ExecuteNonQuery()
                        Next
                    End If

                    strResponse = "Changes Saved"
                    '                    RadGrid1.Rebind()

            End Select
            lblResponse.Text = strResponse
            lblResponse.Visible = True

        Else
            lblResponse.Text = strResponse
            lblResponse.Visible = True
        End If

    End Sub

    Private Sub rbSelectRole_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbSelectRole.SelectedIndexChanged
        Dim sele As String = e.ToString
        Dim sel As String = rbSelectRole.SelectedItem.Text
        Session("uaRoleList") = sel
        RadGrid1.Rebind()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        RadGrid1.Rebind()

    End Sub
End Class