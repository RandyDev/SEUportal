Imports Telerik.Web.UI
Imports System.IO

Public Class Employees
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session("selectedItems") = Nothing
            ' populate Locations Combo Box (cbLocations on aspx page)
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            Dim showFern As Boolean = User.IsInRole("Administrator") Or User.IsInRole("SysOp")
            ldal.setLocaCombo(puser, cbLocations, showFern)

            ldal.setLocaCombo(puser, cbHomeLocation, showFern)
            ldal.setLocaCombo(puser, cbCurrentLocation, showFern)

            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
            cbHomeLocation.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp")
            ' control what access rights user may assign based on users access rights
            Dim rtdsID As String = Utilities.getRTDSidByUserID(puser.ProviderUserKey.ToString)
            Dim emp As New Employee
            If rtdsID <> "00000000-0000-0000-0000-000000000000" Then
                Dim rtdsIDtoGUID As New Guid(rtdsID)
                Dim empdal As New empDAL
                emp = empdal.GetEmployeeByID(rtdsIDtoGUID)
            End If
            cbARMadministrator.Visible = (emp.AccessRightsMask = 3) Or (emp.AccessRightsMask = 7)
            cbARMweb.Visible = emp.AccessRightsMask <> 4

            BindRolesToList()
            dp_DateOfEmployment.Visible = False
            cbPayWeekStartDatesLoad()
            dp_DateOfEmployment.SelectedDate = Date.Now()
        End If
        '        imgMugShot.Width = Unit.Pixel(75)
        '        imgMugShot.Height = Unit.Pixel(100)
        RadGrid1.Visible = cbLocations.SelectedIndex > -1
        pnlTitle.Visible = Not RadGrid1.Visible
        lblPageTitle.Text = Page.Title
        lblSelectLocation.Visible = Not RadGrid1.Visible
        lblSelectEmployee.Visible = RadGrid1.Visible
        lbtnAddNew.Visible = RadGrid1.Visible And Not pnlWOedit.Visible
        '        lbtnChangeLocation.Visible = False
        txt_rtdsPassword.Attributes("onkeyup") = "decOnly(this);"
        txtpayrollempnum.Attributes("onkeyup") = "decOnly(this);"
        txt_rtdsPassword.Attributes("onfocus") = "this.select();"

    End Sub

#Region "Grids"

    Private Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim empdal As New empDAL()
        Dim dt As DataTable = empdal.getWorkersByLocation(New Guid(cbLocations.SelectedValue))
        RadGrid1.DataSource = dt
    End Sub

    Private Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)

        End If
    End Sub

    Protected Sub RadGrid1_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles RadGrid1.PreRender
        If Not (Session("selectedItems") Is Nothing) Then
            Dim selectedItems As ArrayList = CType(Session("selectedItems"), ArrayList)
            Dim stackIndex As Int16
            For stackIndex = 0 To selectedItems.Count - 1
                Dim curItem As String = selectedItems(stackIndex).ToString
                For Each item As GridItem In RadGrid1.MasterTableView.Items
                    If TypeOf item Is GridDataItem Then
                        Dim dataItem As GridDataItem = CType(item, GridDataItem)
                        If curItem.Equals(dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("ID").ToString()) Then
                            dataItem.Selected = True
                        End If
                    End If
                Next
            Next
            ' while it works ... is this the place to rebind grid 2 ? 
            RadGrid2.Visible = True
            RadGrid2.Rebind()
        End If
    End Sub

    Private Sub RadGrid1_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If e.CommandName = "RowClick" Then
            RadGrid2.MasterTableView.ClearEditItems()
            Dim empid As Guid = RadGrid1.SelectedValue
            Dim selectedItems As ArrayList
            If Session("selectedItems") Is Nothing Then
                selectedItems = New ArrayList
            Else
                selectedItems = CType(Session("selectedItems"), ArrayList)
            End If
            selectedItems.Add(empid)
            Session("selectedItems") = selectedItems
            ReloadForms(empid)
            lbtnAddEmployment.Visible = True
            fldLocation.Visible = True
            fsAccess.Visible = True
            btnChangePhoto.Visible = True
            btnSaveChanges.Visible = True
            btnCancel.Visible = True
        End If
    End Sub

    Private Sub RadGrid2_CancelCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid2.CancelCommand
        lbtnAddEmployment.Visible = True
        fldLocation.Visible = True
        fsAccess.Visible = True
        btnChangePhoto.Visible = True
        btnSaveChanges.Visible = True
        btnCancel.Visible = True
    End Sub

    Private Sub RadGrid2_EditCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid2.EditCommand
        lbtnAddEmployment.Visible = False
        fldLocation.Visible = False
        fsAccess.Visible = False
        btnChangePhoto.Visible = False
        btnSaveChanges.Visible = False
        btnCancel.Visible = False
    End Sub

    Private Sub RadGrid2_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid2.NeedDataSource
        Dim selectGuid As Guid
        If RadGrid1.SelectedItems.Count > 0 Then
            selectGuid = RadGrid1.SelectedValue
        Else
            Dim curItem As String = Utilities.zeroGuid.ToString
            If Not (Session("selectedItems") Is Nothing) Then
                Dim selectedItems As ArrayList = CType(Session("selectedItems"), ArrayList)
                Dim stackIndex As Int16
                For stackIndex = 0 To selectedItems.Count - 1
                    curItem = selectedItems(stackIndex).ToString
                Next
            End If
            selectGuid = New Guid(curItem)
        End If
        Dim dba As New DBAccess()
        Dim sqlStr As String = String.Empty
        Dim edal As New empDAL
        sqlStr = "SELECT [EmployeeID], [DateOfEmployment], [DateOfDismiss], [JobTitle], [PayType], [PayRateHourly], " & _
                "[PayRatePercentage], [ID], [SpecialPay], [HolidayPay], [SalaryPay] FROM [Employment] " & _
                "WHERE ([EmployeeID] = @EmployeeID) order by DateOfEmployment DESC"
        dba.CommandText = sqlStr
        dba.AddParameter("@EmployeeID", selectGuid)
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        Dim dt2 As New DataTable

        If dt.Rows.Count > 3 Then
            lbtnShowAllEmployment.Visible = True

            If lbtnShowAllEmployment.Text <> "Show All" Then
                RadGrid2.DataSource = dt
            Else
                dt2 = dt.Clone()
                For i As Integer = 0 To 2
                    Dim rw As DataRow = Nothing
                    dt2.ImportRow(dt.Rows(i))
                Next
                RadGrid2.DataSource = dt2
            End If


        Else 'less than 3
            RadGrid2.DataSource = dt
            lbtnShowAllEmployment.Visible = False
        End If

    End Sub

    Private Sub RadGrid2_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid2.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            ' change paytype to text
            Dim payType As Integer = IIf(IsDBNull(DirectCast(e.Item.DataItem, DataRowView)("PayType")), 0, DirectCast(e.Item.DataItem, DataRowView)("PayType"))
            Dim lbl As Label = e.Item.FindControl("lblPayType")
            Select Case payType
                Case 1
                    lbl.Text = "Percent"
                Case 2
                    lbl.Text = "Hourly"
                Case 3
                    lbl.Text = "Other"
                Case Else
                    lbl.Text = "<font color='red'>UNKNOWN</font>"
            End Select
            Dim dateOfEmployment As DateTime = DirectCast(e.Item.DataItem, DataRowView)("DateOfEmployment")
            Dim DateOfEmploymentLabel As Label = e.Item.FindControl("lblDateOfEmployment")
            DateOfEmploymentLabel.Text = Format(dateOfEmployment, "MMM dd, yyyy")
            'format the date of dismiss column
            Dim dateOfDismiss As DateTime = IIf(IsDBNull(DirectCast(e.Item.DataItem, DataRowView)("DateOfDismiss")), "12/31/9999", DirectCast(e.Item.DataItem, DataRowView)("DateOfDismiss"))
            Dim DateOfDismissLabel As Label = e.Item.FindControl("lblDateOfDismiss")
            If FormatDateTime(dateOfDismiss, DateFormat.ShortDate) = "12/31/9999" Then
                DateOfDismissLabel.Text = " - - - "
            Else
                DateOfDismissLabel.Text = Format(dateOfDismiss, "MMM dd, yyyy")
            End If

            Dim jt As String = DirectCast(e.Item.DataItem, DataRowView)("JobTitle")
            Dim JobTitleLabel As Label = e.Item.FindControl("lblJobTitle")
            JobTitleLabel.Text = jt
        End If
        If e.Item.IsInEditMode Then
            Dim dd As DateTime = DirectCast(e.Item.DataItem, DataRowView)("DateOfDismiss")
            Dim dddp As RadDatePicker = e.Item.FindControl("griddpDismissDate")
            If dd < "12/31/9999" Then
                dddp.SelectedDate = dd
            End If
            Dim de As DateTime = DirectCast(e.Item.DataItem, DataRowView)("DateOfEmployment")
            Dim dedp As RadDatePicker = e.Item.FindControl("griddpEmploymentDate")
            dedp.SelectedDate = de

            Dim jt As String = DirectCast(e.Item.DataItem, DataRowView)("JobTitle")
            Dim cbJobTitle As RadComboBox = e.Item.FindControl("gridcbJobTitle")
            Dim cbjtitem As RadComboBoxItem = cbJobTitle.Items.FindItemByText(jt)
            If Not cbjtitem Is Nothing Then
                cbjtitem.Selected = True
            End If

            Dim pt As Integer = DirectCast(e.Item.DataItem, DataRowView)("PayType")
            Dim cbpaytype As RadComboBox = e.Item.FindControl("gridcbPayType")
            Dim cbptitem As RadComboBoxItem = cbpaytype.Items.FindItemByValue(pt)
            cbptitem.Selected = True

        End If

    End Sub

    Private Sub RadGrid2_UpdateCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid2.UpdateCommand
        Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
        If TypeOf e.Item Is GridEditableItem AndAlso e.Item.IsInEditMode Then
            Dim edal As New empDAL
            'spin up new employment
            Dim empl As New Employment
            'populate existing
            Dim emplid As String = editedItem.GetDataKeyValue("ID").ToString
            empl = edal.getEmploymentByID(New Guid(emplid))
            Dim sdate As RadDatePicker = CType(editedItem.FindControl("griddpEmploymentDate"), RadDatePicker)
            empl.DateOfEmployment = sdate.SelectedDate
            Dim edate As RadDatePicker = CType(editedItem.FindControl("griddpDismissDate"), RadDatePicker)
            empl.DateOfDismiss = IIf(edate.SelectedDate Is Nothing, "12/31/9999", edate.SelectedDate)
            Dim cbgJobTitle As RadComboBox = CType(editedItem.FindControl("gridcbJobTitle"), RadComboBox)
            empl.JobTitle = cbgJobTitle.Text
            Dim cbPayType As RadComboBox = CType(editedItem.FindControl("gridcbPayType"), RadComboBox)
            empl.PayType = cbPayType.SelectedValue
            Dim hourly As RadNumericTextBox = CType(editedItem.FindControl("num_PayRateHourly"), RadNumericTextBox)
            empl.PayRateHourly = hourly.Value
            Dim percent As RadNumericTextBox = CType(editedItem.FindControl("num_PayRatePercentage"), RadNumericTextBox)
            empl.PayRatePercentage = percent.Value
            Dim emp As New Employee
            emp.ID = empl.EmployeeID
            Dim newEmployment As New Employment
            emp.Employment = newEmployment
            emp.Employment = empl
            edal.updateEmployment(emp)
        End If
        lbtnAddEmployment.Visible = True
        fldLocation.Visible = True
        fsAccess.Visible = True
        btnChangePhoto.Visible = True
        btnSaveChanges.Visible = True
        btnCancel.Visible = True
    End Sub

    Private Sub RadGrid2_DeleteCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid2.DeleteCommand
        Dim itm As GridDataItem = DirectCast(e.Item, GridDataItem)
        Dim delitem As Guid = itm.OwnerTableView.DataKeyValues(itm.ItemIndex)("ID")
        Dim dba As New DBAccess()
        dba.CommandText = "DELETE FROM Employment WHERE ID = @ID"
        dba.AddParameter("@ID", delitem)
        dba.ExecuteNonQuery()
    End Sub

#End Region

#Region "Buttons"

    Private Sub lbtnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnAddNew.Click
        If RadGrid1.SelectedItems.Count > 0 Then
            Dim selectedItems As ArrayList
            selectedItems = New ArrayList
            selectedItems.Add(Utilities.zeroGuid())
            Session("selectedItems") = selectedItems
            RadGrid1.Rebind()
        End If
        RadGrid2.Visible = False
        '        RadGrid2.Rebind()
        ClearForms()
        pnlAddEmployment.Visible = True
        lbtnAddEmployment.Visible = False
        lbtnShowAllEmployment.Visible = False
        cbPayWeekStartDates.Visible = False
        dp_DateOfEmployment.Visible = True
        dp_DateOfEmployment.SelectedDate = Date.Now()
        lbtnCalendarPayWeek.Text = "show Pay Periods"
        lbtnCalendarPayWeek.CommandName = "PayPeriod"
        cb_PayType.ClearSelection()
        cb_PayType.Text = Nothing
        cb_JobTitle.ClearSelection()
        cb_JobTitle.Text = Nothing
        lblHelpEmployment.Visible = False
        lbtnChangeName.Enabled = False
        lblEmailAddress.Visible = False
        txt_eMail.Visible = False
        fsseuAccess.Visible = False
        cbdivlogIsApproved.Visible = False

        cbCurrentLocation.SelectedValue = cbLocations.SelectedValue
        cbHomeLocation.SelectedValue = cbLocations.SelectedValue
        cbCurrentLocation.Enabled = False
        cbHomeLocation.Enabled = False

        fldLocation.Visible = False

    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Session("selectedItems") = Nothing
        pnlWOedit.Visible = False
        pnlTitle.Visible = True
        lblSelectEmployee.Visible = True
        lbtnAddNew.Visible = True
        pnlAddEmployment.Visible = False
        RadGrid2.MasterTableView.ClearEditItems()
        RadGrid1.Rebind()
        RadGrid2.Rebind()
        lbtnAddEmployment.Visible = True
        lbtnShowAllEmployment.Visible = True
        cbPayWeekStartDates.Visible = True
        dp_DateOfEmployment.Visible = False

    End Sub

    Private Sub btnChangePhoto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangePhoto.Click
        btnChangePhoto.Visible = False
        AsyncUpload1.Visible = True
        btnImageCancel.Visible = True
    End Sub

    Private Sub btnImageCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImageCancel.Click
        btnChangePhoto.Visible = True
        AsyncUpload1.Visible = False
        btnImageCancel.Visible = False
    End Sub

    Private Sub lbtnResetPassword_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles lbtnResetPassword.Command
        If e.CommandName = "ResetPassword" Then
            Dim udal As New userDAL()
            lbtnResetPassword.Text = (udal.ResetPasswordandSecurity(e.CommandArgument))
            lbtnResetPassword.Enabled = False
        End If
    End Sub

    Private Sub btnSaveChanges_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnSaveChanges.Command
        'is change employment form open
        'is the name forms open
        'if true  test visibility of dpStartDate


        'Private Sub btnSaveChanges_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnSaveChanges.Command
        Dim edal As New empDAL()
        Dim emp As New Employee()   'Employee Object
        Dim emplmnt As New Employment() 'Employment Object
        emp.Employment = emplmnt    'Add Employment Object to Employee Object
        Dim suser As New ssUser()   'site user object
        emp.ssUser = suser  'add site user to employee object
        clearValidators()
        lblSelectEmployee.Visible = False

        Dim strErrMsg = validateEmpForm()

        If Len(strErrMsg) > 1 Then
            errMsg.Text = strErrMsg
            errMsg.Visible = True
        Else    'made it past form validator ... process form
            Dim udal As New userDAL
            If e.CommandName = "Update" Or e.CommandName = "Insert" Then
                If e.CommandName = "Update" Then
                    ' check homelocation, currentlocation, comments, pin, isauthorized, email, role
                    emp = edal.GetEmployeeByID(New Guid(e.CommandArgument.ToString))

                    'If Not dp_DateOfDismiss.SelectedDate Is Nothing Then
                    '    btnSaveChanges.CommandName = "PutEmBack"
                    'End If

                ElseIf e.CommandName = "Insert" Then   'both employee.id and employment.id need to be generated
                    'new guids
                    emp.ID = Guid.NewGuid()
                    emp.Employment.ID = Guid.NewGuid()
                    'set PhotoJpegData to default noimage file
                    Dim data As Byte() = Nothing
                    Dim sPath As String = Server.MapPath("~")
                    sPath &= "/images/noimage.png"
                    Dim fInfo As New FileInfo(sPath)
                    Dim numBytes As Long = fInfo.Length
                    Dim fStream As New FileStream(sPath, FileMode.Open, FileAccess.Read)
                    Dim br As New BinaryReader(fStream)
                    data = br.ReadBytes(CInt(numBytes))
                    emp.PhotoJpegData = data
                    'set the grid to select this new employee after postback
                    Dim selectedItems As ArrayList
                    If Session("selectedItems") Is Nothing Then
                        selectedItems = New ArrayList
                    Else
                        selectedItems = CType(Session("selectedItems"), ArrayList)
                    End If
                    selectedItems.Add(emp.ID)
                    Session("selectedItems") = selectedItems
                    '********************************************
                    'USER NAME
                    '********************************************
                    '********************************************

                    emp.Login = txtUserName.Text.Trim()
                    emp.ssUser.Password = emp.Login & "welcome"
                    emp.ssUser.rtdsEmployeeID = emp.ID
                    emp.PayrollEmpNum = txtpayrollempnum.Text

                End If
                emp.rtdsFirstName = txt_rtdsFirstName.Text.Trim()
                emp.rtdsLastName = txt_rtdsLastName.Text.Trim()
                emp.Comments = txt_Comments.Text.Trim()

                emp.HomeLocationID = New Guid(cbHomeLocation.SelectedValue)

                ' ********** handle moves
                If e.CommandName = "Update" Then
                    ' if a move is indicated
                    Dim newLocaID As Guid = New Guid(cbCurrentLocation.SelectedValue)
                    If emp.LocationID <> newLocaID Then 'we are requesting a move
                        Dim trDAL As New TravelerDAL
                        Dim travelRec As New Traveler
                        'is he already traveling?
                        travelRec = trDAL.getOpenTravelerByEmployeeID(emp.ID)
                        If Utilities.IsValidGuid(travelRec.travelID.ToString) Then 'yes he is
                            'is he requesting to go home?
                            If emp.HomeLocationID = newLocaID Then 'going home, move him and update travel record
                                travelRec.returnDate = Date.Now()
                                trDAL.updateTravelerDate(travelRec)
                                emp.LocationID = newLocaID
                            Else 'trying to travel from travel location
                                cbCurrentLocation.SelectedValue = emp.LocationID.ToString
                                'stop the transfer

                            End If
                        Else ' not already traveling ... move him and insert travel record
                            emp.LocationID = newLocaID
                            travelRec.travelID = Guid.NewGuid()
                            travelRec.rtdsEmployeeID = emp.ID
                            travelRec.homeLocation = emp.HomeLocationID
                            travelRec.travelLocation = newLocaID
                            travelRec.startDate = Date.Now()
                            travelRec.returnDate = "1/1/1990"
                            travelRec.loadMoney = True
                            trDAL.insertTraveler(travelRec)
                        End If

                    End If
                    'move em
                Else ' not an update
                    emp.LocationID = New Guid(cbCurrentLocation.SelectedValue)
                End If

                emp.Certification = Nothing
                '        emp.empCertifications
                '        emp.PhotoJpegData
                emp.rtdsPassword = txt_rtdsPassword.Text.Trim   'PIN Number
                '            emp.Locked = cbrtdsAccountLocked.Checked

                'set the proper access rights for handheld
                Dim utl As New Utilities
                emp.AccessRightsMask = utl.calcAccessRightsMask(cbARMadministrator.Checked, cbARMweb.Checked, cbARMpda.Checked)


                If e.CommandName = "Insert" And pnlAddEmployment.Visible = True Then

                    emp.Employment.EmployeeID = emp.ID
                    emp.Employment.DateOfEmployment = dp_DateOfEmployment.SelectedDate
                    emp.Employment.DateOfDismiss = "12/31/9999"
                    emp.Employment.JobTitle = cb_JobTitle.Text.Trim()
                    emp.Employment.PayType = CInt(cb_PayType.SelectedValue)
                    emp.Employment.PayRateHourly = IIf(num_PayRateHourly.Value Is Nothing, 0, num_PayRateHourly.Value)
                    emp.Employment.PayRatePercentage = IIf(num_PayRatePercentage.Value Is Nothing, 0, num_PayRatePercentage.Value)

                    'No longer used
                    '                emp.Employment.SpecialPay = IIf(num_SpecialPay.Value Is Nothing, 0, num_SpecialPay.Value)
                    '                emp.Employment.HolidayPay = IIf(num_HolidayPay.Value Is Nothing, 0, num_HolidayPay.Value)
                    '                emp.Employment.SalaryPay = IIf(num_SalaryPay.Value Is Nothing, 0, num_SalaryPay.Value)
                End If

                '            emp.ssUser.Comment = "UNUSED"

                emp.ssUser.userName = emp.Login
                emp.ssUser.eMail = IIf(txt_eMail.Text = "...pending...", emp.Login & "@Div-Log.com", txt_eMail.Text.Trim())

                emp.ssUser.IsApproved = cbdivlogIsApproved.Checked

                emp.ssUser.FirstName = emp.rtdsFirstName
                emp.ssUser.LastName = emp.rtdsLastName
                '            emp.ssUser.MI = ""
                emp.ssUser.Title = emp.Employment.JobTitle
                Dim rlst As New List(Of String)
                For Each ri As RepeaterItem In UsersRoleList.Items
                    Dim RoleCheckBox As CheckBox = CType(ri.FindControl("RoleCheckBox"), CheckBox)
                    If RoleCheckBox.Checked = True Then
                        Dim rl As String = RoleCheckBox.Text
                        rlst.Add(rl)
                    End If
                Next
                If rlst.Count < 1 Then
                    rlst.Add("Employee")
                End If
                emp.ssUser.myRoles = rlst
            End If
            emp.ssUser.cellText = String.Empty

            Select Case e.CommandName
                Case "Update"

                    strErrMsg &= udal.UpdateUser(emp.ssUser)
                    'does ssUser exist?
                    ' yes .. it is auto created if doesn't exist when the employee record loads
                    strErrMsg &= edal.updateEmployee(emp)
                    '                    strErrMsg &= edal.updateEmployment(emp)
                Case "Insert"
                    'check for dup username
                    Dim tstr As String = udal.checkDupeUserName(emp.ssUser.userName)
                    If Not tstr = String.Empty Then
                        errMsg.Text = tstr
                        errMsg.Visible = True
                        strErrMsg = tstr
                        Exit Sub
                    End If
                    'check for dup email address
                    tstr = udal.checkDupeEmail(emp.ssUser.eMail)
                    If Not tstr = String.Empty Then
                        errMsg.Text = tstr
                        errMsg.Visible = True
                        strErrMsg = tstr
                        Exit Sub
                    End If
                    strErrMsg &= udal.addUser(emp.ssUser)
                    If strErrMsg = "The user account was successfully created!" Then
                        strErrMsg &= edal.insertEmployee(emp)
                        strErrMsg &= edal.insertEmployment(emp)
                    End If
                Case Else
                    strErrMsg = "not an update OR insert"

            End Select
        End If

        If Len(strErrMsg) > 3 And strErrMsg <> "The user account was successfully created!" Then
            errMsg.Text = strErrMsg
            errMsg.Visible = True
            If strErrMsg.Contains("eMail Address already in use") Then lblErrEmail.Visible = True
        Else
            clearValidators()
            If e.CommandName = "Insert" Then    'it's a new guy, reload to show photo,comments,certs
                ReloadForms(emp.ID)
            End If
            'If Not dp_DateOfDismiss.SelectedDate Is Nothing Then
            '    ClearForms()
            '    pnlWOedit.Visible = False
            '    lblSelectEmployee.Visible = True
            'Else
            lblEmpName.Text = emp.rtdsFirstName & " " & emp.rtdsLastName
            txt_rtdsFirstName.Visible = False
            txt_rtdsLastName.Visible = False
            errMsg.Text = "Changes Saved"
            errMsg.Visible = True
            '        End If
            RadGrid1.Rebind()
        End If
        btnSaveChanges.Visible = True

    End Sub

    Private Sub lbtnChangeName_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnChangeName.Click
        txt_rtdsFirstName.Visible = True
        txt_rtdsLastName.Visible = True
    End Sub

    Private Sub lbtnCalendarPayWeek_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles lbtnCalendarPayWeek.Command
        If e.CommandName = "Calendar" Then
            lbtnCalendarPayWeek.Text = "show Pay Periods"
            lbtnCalendarPayWeek.CommandName = "PayPeriod"
            dp_DateOfEmployment.Visible = True
            cbPayWeekStartDates.Visible = False

        Else
            lbtnCalendarPayWeek.Text = "show Calendar"
            lbtnCalendarPayWeek.CommandName = "Calendar"
            dp_DateOfEmployment.Visible = False
            cbPayWeekStartDates.Visible = True
        End If
    End Sub

    Private Sub btnUnlockUser_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnUnlockUser.Command
        Dim usrInfo As MembershipUser = Membership.GetUser(e.CommandArgument.ToString)
        If usrInfo.IsLockedOut Then usrInfo.UnlockUser()
        btnUnlockUser.Visible = False
    End Sub

#End Region

#Region "Combo Boxes"

    Protected Sub cbLocations_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        If cbLocations.SelectedIndex < 0 Then
            RadGrid1.Visible = False
            lblSelectLocation.Visible = True
            lblSelectEmployee.Visible = False
            lbtnAddNew.Visible = False
        Else
            RadGrid1.Rebind()
            lbtnAddNew.Visible = True
        End If
        pnlWOedit.Visible = False
        pnlTitle.Visible = True
    End Sub

    Private Sub cbPayWeekStartDates_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbPayWeekStartDates.SelectedIndexChanged
        Dim sdate As Date = cbPayWeekStartDates.SelectedValue
        dp_DateOfEmployment.SelectedDate = sdate
        If e.Text = "Other" Then
            dp_DateOfEmployment.Visible = True
        Else
            dp_DateOfEmployment.Visible = False
        End If

    End Sub

    Protected Sub cb_PayType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cb_PayType.SelectedIndexChanged
        Select Case cb_PayType.SelectedIndex
            Case 0 'Hourly
                num_PayRateHourly.Enabled = True
                num_PayRateHourly.EmptyMessage = "$0.00"
                num_PayRatePercentage.Enabled = False
                num_PayRatePercentage.Value = Nothing
                num_PayRatePercentage.EmptyMessage = " - - - "
                'num_SalaryPay.Enabled = False
                'num_SalaryPay.Value = Nothing
                'num_SalaryPay.EmptyMessage = " - - - "
            Case 1 'Percentage
                num_PayRateHourly.Enabled = True
                num_PayRateHourly.EmptyMessage = "$0.00"
                num_PayRatePercentage.Enabled = True
                num_PayRatePercentage.EmptyMessage = "00.00 %"

                'num_SalaryPay.Enabled = False
                'num_SalaryPay.Value = Nothing
                'num_SalaryPay.EmptyMessage = " - - - "

                'Case 2 'Other
                '    num_PayRateHourly.Enabled = False
                '    num_PayRateHourly.Value = Nothing
                '    num_PayRateHourly.EmptyMessage = " - - - "

                '    num_PayRatePercentage.Enabled = False
                '    num_PayRatePercentage.EmptyMessage = " - - - "
                '    num_PayRatePercentage.Value = Nothing


                '    num_SalaryPay.Enabled = True
                '    num_SalaryPay.EmptyMessage = "$ Weekly"

        End Select
    End Sub

    Private Sub cbPayWeekStartDatesLoad()
        Dim tpdal As New TimePuncheDAL
        Dim sdate As Date = tpdal.getPayStartDate(Date.Now())
        Dim itmDate As Date = sdate
        Dim itm As RadComboBoxItem

        itm = New RadComboBoxItem
        itm.Text = Format(sdate, "MMM dd, yyyy") & " : THIS pay period"
        itm.Value = sdate
        cbPayWeekStartDates.Items.Add(itm)

        itm = New RadComboBoxItem
        itmDate = DateAdd(DateInterval.Day, 14, sdate)
        itm.Text = Format(itmDate, "MMM dd, yyyy") & " : NEXT pay period"
        itm.Value = itmDate
        cbPayWeekStartDates.Items.Add(itm)

        ' Only allow previous week for the first four 4 days of new pay week
        Dim ddif As Integer = DateDiff(DateInterval.Day, sdate, Date.Now())
        itm = New RadComboBoxItem
        itmDate = DateAdd(DateInterval.Day, -14, sdate)
        itm.Text = Format(itmDate, "MMM dd, yyyy") & " : PREVIOUS pay period"
        itm.Value = itmDate

        If DateDiff(DateInterval.Day, sdate, Date.Now()) < 5 Then
            cbPayWeekStartDates.Items.Add(itm)
        End If

        cbPayWeekStartDates.SelectedIndex = 0

    End Sub

#End Region

#Region "Form Methods"
    Private Sub ReloadForms(ByVal empid As Guid)
        clearValidators()
        fldLocation.Visible = True
        pnlAddEmployment.Visible = False
        cbPayWeekStartDates.Visible = True
        dp_DateOfEmployment.Visible = False
        lbtnCalendarPayWeek.Text = "show Calendar"
        lbtnCalendarPayWeek.CommandName = "Calendar"
        lblHelpEmployment.Visible = True
        lblEmailAddress.Visible = True
        txt_eMail.Visible = True
        fsseuAccess.Visible = True
        cbdivlogIsApproved.Visible = True
        '        btnSaveChanges.Text = "Save Changes"
        '        btnSaveChanges.CommandName = "Update"
        '        btnSaveChanges.CommandArgument = empid.ToString

        lbtnAddEmployment.Visible = True
        lbtnShowAllEmployment.Text = "Show All"

        lbtnAddNew.Visible = False
        lbtnChangeName.Enabled = True
        btnSaveChanges.Text = "Save Changes"
        btnSaveChanges.CommandName = "Update"
        btnSaveChanges.CommandArgument = empid.ToString
        btnSaveChanges.Visible = True
        lblSelectEmployee.Visible = False
        pnlWOedit.Visible = True
        pnlTitle.Visible = False
        imgMugShot.Visible = True
        fsComments.Visible = True
        fsCertifications.Visible = True
        txt_rtdsFirstName.Visible = False
        txt_rtdsLastName.Visible = False
        '        lbtnEmploymentHistory.Visible = True
        btnChangePhoto.Visible = True
        AsyncUpload1.Visible = False
        errMsg.Text = Nothing
        errMsg.Visible = False
        '        lbtnChangeLocation.Visible = False
        Dim empdal As New empDAL()
        Dim emp As Employee = empdal.GetEmployeeByID(empid)
        lblEmpName.Text = emp.rtdsFirstName & " " & emp.rtdsLastName
        txt_rtdsFirstName.Text = emp.rtdsFirstName
        txt_rtdsLastName.Text = emp.rtdsLastName
        txt_Comments.Text = emp.Comments
        '        txt_Certification.Text = emp.Certification

        If emp.PhotoJpegData Is Nothing Then
            Dim data As Byte() = Nothing
            Dim sPath As String = Server.MapPath("~")
            sPath &= "/images/noimage.png"
            Dim fInfo As New FileInfo(sPath)
            Dim numBytes As Long = fInfo.Length
            Dim fStream As New FileStream(sPath, FileMode.Open, FileAccess.Read)
            Dim br As New BinaryReader(fStream)
            data = br.ReadBytes(CInt(numBytes))
            imgMugShot.DataValue = data
            imgMugShot.SavedImageName = "No Image"
        ElseIf emp.PhotoJpegData.Count < 1 Then
            Dim data As Byte() = Nothing
            Dim sPath As String = Server.MapPath("~")
            sPath &= "/images/noimage.png"
            Dim fInfo As New FileInfo(sPath)
            Dim numBytes As Long = fInfo.Length
            Dim fStream As New FileStream(sPath, FileMode.Open, FileAccess.Read)
            Dim br As New BinaryReader(fStream)
            data = br.ReadBytes(CInt(numBytes))
            imgMugShot.DataValue = data
            imgMugShot.SavedImageName = "No Image"
        Else
            imgMugShot.DataValue = emp.PhotoJpegData
            imgMugShot.SavedImageName = emp.rtdsFirstName & "'s MugShot®"
        End If
        imgMugShot.Width = Unit.Pixel(75)
        imgMugShot.Height = Unit.Pixel(100)

        Dim ldal As New locaDAL
        cbCurrentLocation.Text = ldal.getLocationNameByID(emp.LocationID.ToString)
        cbCurrentLocation.SelectedValue = emp.LocationID.ToString
        If Utilities.IsValidGuid(emp.HomeLocationID.ToString()) Then
            cbHomeLocation.Text = ldal.getLocationNameByID(emp.HomeLocationID.ToString)
            cbHomeLocation.SelectedValue = emp.HomeLocationID.ToString
            cbHomeLocation.Enabled = True
        Else
            cbHomeLocation.Text = String.Empty
            cbHomeLocation.ClearSelection()
        End If
        cbCurrentLocation.Enabled = User.IsInRole("SysOp") Or User.IsInRole("Administrator") Or User.IsInRole("Manager")

        '        lblUserName.Text = emp.Login
        '        txtUserName.EmptyMessage = emp.Login
        txtUserName.Text = emp.Login
        '       lblUserName.Visible = True
        '        txtUserName.Visible = False
        'pin
        txt_rtdsPassword.Text = emp.rtdsPassword
        'disabled >>>>>>>>>>>>
        cbrtdsAccountLocked.Checked = emp.Locked
        Dim utl As New Utilities
        Dim arms As ArrayList = utl.convertAccessRightsMask(emp.AccessRightsMask)
        cbARMadministrator.Checked = arms(0)
        cbARMweb.Checked = arms(1)
        cbARMpda.Checked = arms(2)
        '<<<<<<<<<<<<<<< disabled
        Dim eDAL As New empDAL()
        emp.empCertifications = eDAL.getEmployeeCertList(emp.ID)
        lblCerts.Text = ""
        If emp.empCertifications.Count > 0 Then
            For Each crt As Certification In emp.empCertifications
                Dim clr As String = "Black"
                If Date.Now > DateAdd(DateInterval.Year, 2, crt.certDate) Then 'over 2
                    clr = "Red"
                ElseIf Date.Now > DateAdd(DateInterval.Month, 22, crt.certDate) Then 'over 22 months
                    clr = "Orange"
                End If
                lblCerts.Text &= "<span style=""color:" & clr & ";"">" & Format(crt.certDate, "dd MMM yy") & "</span> : " & crt.certName & "<br />"
            Next
        Else
            lblCerts.Text = "no certs<br />"
        End If
        If User.IsInRole("Manager") Or User.IsInRole("Administrator") Or User.IsInRole("SysOp") Then
            lbtnEditCerts.OnClientClick = "openCertEditor('" & emp.ID.ToString & "');"
            lbtnEditCerts.Visible = True
            lbtnAddEmployment.OnClientClick = "openEmploymentEditor('" & emp.ID.ToString & "');"
            lbtnAddEmployment.Visible = True
            lbtnShowAllEmployment.Visible = True
        Else
            lbtnEditCerts.OnClientClick = Nothing
            lbtnEditCerts.Visible = False
            lbtnAddEmployment.OnClientClick = Nothing
            lbtnAddEmployment.Visible = False
            lbtnShowAllEmployment.Visible = False
        End If

        '        dp_DateOfEmployment.MinDate = "1/1/1900"
        '        dp_DateOfEmployment.SelectedDate = emp.Employment.DateOfEmployment
        '        If emp.Employment.DateOfDismiss < Date.Now() Then
        '        dp_DateOfDismiss.SelectedDate = emp.Employment.DateOfDismiss
        '        Else
        '        dp_DateOfDismiss.Clear()
        '        End If
        '        cb_JobTitle.Text = emp.Employment.JobTitle
        '        cb_PayType.SelectedValue = emp.Employment.PayType
        '        num_SpecialPay.Value = emp.Employment.SpecialPay
        '        If num_SpecialPay.Value = 0 Then num_SpecialPay.Value = Nothing
        '        num_HolidayPay.Value = emp.Employment.HolidayPay
        '        If num_HolidayPay.Value = 0 Then num_HolidayPay.Value = Nothing
        '        num_PayRateHourly.Value = emp.Employment.PayRateHourly
        '        If num_PayRateHourly.Value = 0 Then num_PayRateHourly.Value = Nothing
        '        num_PayRatePercentage.Value = emp.Employment.PayRatePercentage
        '        If num_PayRatePercentage.Value = 0 Then num_PayRatePercentage.Value = Nothing
        '        num_SalaryPay.Value = emp.Employment.SalaryPay
        '        If num_SalaryPay.Value = 0 Then num_SalaryPay.Value = Nothing

        Dim hasSEUAccount As Boolean = (emp.ssUser.userID.ToString > "00000000-0000-0000-0000-000000000000")

        If Not hasSEUAccount Then   'run create an account for em

            Dim SEUcreated As String = createSEUaccount(emp)
            lbtnResetPassword.CommandArgument = Nothing
            lbtnResetPassword.Enabled = False
            lbtnResetPassword.Text = emp.Login & "welcome"

        Else
            If emp.ssUser.CreationDate = emp.ssUser.LastPasswordChangedDate Then
                lbtnResetPassword.CommandArgument = Nothing
                lbtnResetPassword.Enabled = False
                lbtnResetPassword.Text = emp.ssUser.userName & "welcome"
            Else
                lbtnResetPassword.CommandArgument = emp.ssUser.userID.ToString
                lbtnResetPassword.Enabled = True
                lbtnResetPassword.Text = "Reset Password"

            End If

        End If

        txt_eMail.Text = emp.ssUser.eMail
        txt_eMail.Enabled = True
        cbdivlogIsApproved.Checked = emp.ssUser.IsApproved
        btnUnlockUser.Visible = emp.ssUser.IsLockedOut
        btnUnlockUser.CommandArgument = emp.ssUser.userName
        CheckRolesForSelectedUser(emp.ssUser.userName)

        Dim a As String = String.Empty
    End Sub

    Private Sub ClearForms()
        clearValidators()
        '        RadGrid2.Visible = True
        Session("selectedItems") = Nothing
        pnlWOedit.Visible = True
        pnlTitle.Visible = False
        lblSelectEmployee.Visible = False
        lbtnAddNew.Visible = False
        lblSelectLocation.Visible = False
        lblEmpName.Text = "New Hire -> &nbsp;"
        txt_rtdsFirstName.Visible = True
        txt_rtdsLastName.Visible = True
        '        lbtnEmploymentHistory.Visible = False
        cbHomeLocation.ClearSelection()
        cbCurrentLocation.ClearSelection()
        AsyncUpload1.Visible = False
        btnChangePhoto.Visible = False
        '        btnSaveChanges.Text = "Add New Employee"
        '        btnSaveChanges.CommandName = "Insert"
        '        btnSaveChanges.CommandArgument = Nothing
        btnSaveChanges.Text = "Add New Employee"
        btnSaveChanges.CommandName = "Insert"
        btnSaveChanges.CommandArgument = Nothing
        '        lbtnChangeLocation.Visible = False
        Dim edal As New empDAL
        Dim locaID As Guid = New Guid(cbLocations.SelectedValue)
        '        lblUserName.Text = edal.getNextEmpID(locaID)
        txtUserName.Text = edal.getNextEmpID(locaID)
        If txtUserName.Text = "" Then
            txtUserName.ReadOnly = False
            lbtnResetPassword.Text = "...pending..." ' lblUserName.Text & "welcome"
        Else
            lbtnResetPassword.Text = txtUserName.Text & "welcome"
        End If
        '        txt_UserName.Visible = True
        '        lblUserName.Text = Nothing

        '        lblUserName.Visible = True

        lbtnResetPassword.Enabled = False
        txt_rtdsPassword.Text = String.Empty

        Dim data As Byte() = Nothing
        Dim sPath As String = Server.MapPath("~")
        sPath &= "/images/noimage.png"
        Dim fInfo As New FileInfo(sPath)
        Dim numBytes As Long = fInfo.Length
        Dim fStream As New FileStream(sPath, FileMode.Open, FileAccess.Read)
        Dim br As New BinaryReader(fStream)
        data = br.ReadBytes(CInt(numBytes))
        imgMugShot.DataValue = data
        imgMugShot.SavedImageName = "No Image"
        txt_rtdsFirstName.Text = Nothing
        txt_rtdsLastName.Text = Nothing
        txt_Comments.Text = Nothing
        '        txt_Certification.Text = emp.Certification
        imgMugShot.SavedImageName = "No Image"
        imgMugShot.Visible = False
        fsComments.Visible = False
        fsCertifications.Visible = False
        '            lblLocation.Text = cbLocations.SelectedItem.Text

        cbrtdsAccountLocked.Checked = False
        cbARMadministrator.Checked = False
        cbARMweb.Checked = False
        cbARMpda.Checked = True
        lblCerts.Text = ""
        lblCerts.Text = "no certs<br />"
        dp_DateOfEmployment.SelectedDate = Date.Now()
        '            dp_DateOfDismiss.SelectedDate = Nothing
        '            cb_JobTitle.Text = ""
        '            cb_PayType.ClearSelection()
        '            cb_PayType.Text = ""
        '            num_SpecialPay.Value = Nothing
        '            num_HolidayPay.Value = Nothing
        '            num_PayRateHourly.Value = Nothing
        '            num_PayRatePercentage.Value = Nothing
        '            num_SalaryPay.Value = Nothing
        cbdivlogIsApproved.Checked = True
        txt_eMail.Text = "...pending..."
        txt_eMail.Enabled = False
        For Each ri As RepeaterItem In UsersRoleList.Items
            Dim RoleCheckBox As CheckBox = CType(ri.FindControl("RoleCheckBox"), CheckBox)
            RoleCheckBox.Checked = (RoleCheckBox.Text = "Employee")
        Next

        Dim a As String = String.Empty
    End Sub

    Private Function validateEmpForm() As String
        Dim strErrMsg As String = String.Empty
        Dim errList As New List(Of String)
        Dim udal As New userDAL()

        If txt_rtdsFirstName.Text.Trim.Length < 1 Then
            errList.Add("First name is required")
            lblErrFname.Visible = True
        End If
        If txt_rtdsLastName.Text.Trim.Length < 1 Then
            errList.Add("Last name is required")
            lblErrLname.Visible = True
        End If

        ' ************** NEW HIRE ONLY ***************
        If pnlAddEmployment.Visible Then
            If cb_JobTitle.Text.Trim() = String.Empty Then
                errList.Add("Select Job Title")
                lblErrJobTitle.Visible = True
            End If

            If dp_DateOfEmployment.Visible Then
                If dp_DateOfEmployment.SelectedDate Is Nothing Then
                    errList.Add("Start Date is required")
                    lblErrStartDate.Visible = True
                End If
            Else

            End If

            If cb_PayType.SelectedIndex = -1 Then
                errList.Add("Select Primary Pay Type")
                lblErrPayType.Visible = True
            End If

            Select Case cb_PayType.SelectedIndex
                Case 0
                    If num_PayRateHourly.Value = 0 Or num_PayRateHourly.Value Is Nothing Then
                        errList.Add("Pay Rates - Hourly Rate required")
                        lblErrPayRates.Visible = True
                    End If
                Case 1
                    If (num_PayRatePercentage.Value = 0 Or num_PayRatePercentage.Value Is Nothing) Then
                        errList.Add("Pay Rates - Percent Rate required")
                        lblErrPayRates.Visible = True
                    End If
                    'Case 2
                    '    If num_SalaryPay.Value = 0 Or num_SalaryPay.Value Is Nothing Then
                    '        errList.Add("Pay Rates - Other Rate required")
                    '        lblErrPayRates.Visible = True
                    '    End If
            End Select
        End If
        '  *************** END NEW HIRE ONLY
        If txt_rtdsPassword.Text = String.Empty Then    'PIN NUMBER
            errList.Add("PIN Number is required <font size='1'>(last 4 digits of SSN)</font>")
            lblErrPIN.Visible = True
        Else
            If txt_rtdsPassword.Text.Length <> 4 Then
                errList.Add("PIN Number must be 4 digits")
                lblErrPIN.Visible = True
            End If
        End If

        '********************************************   NOW AUTO GENERATED
        'USER NAME
        '********************************************
        '********************************************
        'Dim strUserName As String = "" ' IIf(txt_UserName.Text.Trim() = String.Empty, lblUserName.Text, txt_UserName.Text.Trim())
        'If strUserName = String.Empty Then
        '    errList.Add("Employee LoginID required")
        '    lblErrUserName.Visible = True
        'Else
        '    If Len(strUserName) < 4 Then
        '        errList.Add("Employee LoginID must be minimum 4 characters")
        '        lblErrUserName.Visible = True

        '    End If
        'End If

        If txt_eMail.Text <> "...pending..." Then
            If Len(txt_eMail.Text.Trim()) < 1 Then
                errList.Add("eMail Address required")
                lblErrEmail.Visible = True
            Else
                '                Dim myRegex As New Regex("\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
                '                If Not myRegex.IsMatch(txt_eMail.Text.Trim()) Then
                If Not Utilities.isValidEmail(txt_eMail.Text.Trim()) Then
                    errList.Add("eMail Address Invalid")
                    lblErrEmail.Visible = True
                End If
            End If
        End If

        If errList.Count > 0 Then
            For Each itm In errList
                strErrMsg &= itm & "<br />"
            Next
            strErrMsg = Left(strErrMsg, Len(strErrMsg) - 6)
        End If
        Return strErrMsg
    End Function

    Private Sub clearValidators()
        lblErrFname.Visible = False
        lblErrLname.Visible = False
        lblErrJobTitle.Visible = False
        lblErrPayType.Visible = False
        lblErrStartDate.Visible = False
        '        lblErrEndDate.Visible = False
        lblErrPayRates.Visible = False
        lblErrPIN.Visible = False
        lblErrUserName.Visible = False
        lblErrEmail.Visible = False
        errMsg.Text = ""
        errMsg.Visible = False
    End Sub

#End Region

#Region "DB Methods"
    Private Function createSEUaccount(ByRef emp As Employee) As String
        Dim errStr As String = String.Empty
        '        emp.ssUser.userID = Nothing
        emp.ssUser.userName = emp.Login
        emp.ssUser.eMail = emp.Login & "@Div-Log.com"
        '        emp.ssUser.CreationDate = ""
        emp.ssUser.Password = emp.Login & "welcome"
        '        emp.rtdsPassword = txt_rtdsPassword.Text.Trim
        '       emp.Login = txt_rtdsPassword.Text.Trim()
        emp.ssUser.IsApproved = True
        '        emp.ssUser.IsOnline = ""
        '        emp.ssUser.LastActivityDate = ""
        '        emp.ssUser.LastLockoutDate = ""
        '        emp.ssUser.LastLoginDate = ""
        '        emp.ssUser.LastPasswordChangedDate = ""
        '        emp.ssUser.PasswordQuestion = "na"
        '        emp.ssUser.Comment = ""
        Dim rlst As New List(Of String)
        rlst.Add("Employee")
        emp.ssUser.myRoles = rlst
        emp.ssUser.FirstName = emp.rtdsFirstName
        emp.ssUser.MI = ""
        emp.ssUser.LastName = emp.rtdsLastName
        emp.ssUser.Title = emp.Employment.JobTitle
        emp.ssUser.Phone = ""
        emp.ssUser.rtdsEmployeeID = emp.ID
        Dim usrDal As New userDAL
        Return usrDal.addUser(emp.ssUser)
    End Function

    Private Sub AsyncUpload1_FileUploaded(ByVal sender As Object, ByVal e As Telerik.Web.UI.FileUploadedEventArgs) Handles AsyncUpload1.FileUploaded
        Dim imageData As Byte()
        Using stream As Stream = e.File.InputStream
            imageData = New Byte(stream.Length - 1) {}
            stream.Read(imageData, 0, CInt(stream.Length))
            imgMugShot.DataValue = imageData
        End Using
        Dim empid As Guid = RadGrid1.SelectedValue
        Dim dba As New DBAccess()
        dba.CommandText = "UPDATE Employee SET PhotoJpegData = @picData WHERE ID = @empID"
        dba.AddParameter("@picData", imageData)
        dba.AddParameter("@empID", empid)
        Dim err As String = String.Empty
        Try
            dba.ExecuteNonQuery()
        Catch ex As Exception
            err = ex.Message
        End Try
        If err IsNot Nothing Then
            errMsg.Text = err
            errMsg.Visible = True
        End If
        btnChangePhoto.Visible = True
        AsyncUpload1.Visible = False
        btnImageCancel.Visible = False
    End Sub

#End Region

    Public Function RemoveElementFromArray(ByVal objArray As System.Array, ByVal objElement As Object, ByVal objType As System.Type) As Array
        Dim objArrayList As New ArrayList(objArray)
        objArrayList.Remove(objElement)
        Return objArrayList.ToArray(objType)
    End Function

    Private Sub BindRolesToList()
        Dim rls() As String = Split("Employee Supervisor Manager")
        UsersRoleList.DataSource = rls
        UsersRoleList.DataBind()
    End Sub

    Public Function RoleOrBetter(ByVal roleName As String) As Boolean
        Dim ok As Boolean = False
        Select Case roleName
            Case "SysOp"
                ok = User.IsInRole("SysOp")
            Case "Administrator"
                ok = User.IsInRole("Administrator") Or User.IsInRole("SysOp")
            Case "Manager"
                ok = User.IsInRole("Manager") Or User.IsInRole("Administrator") Or User.IsInRole("SysOp")
            Case "Supervisor"
                ok = User.IsInRole("Manager") Or User.IsInRole("Administrator") Or User.IsInRole("SysOp")
            Case "Employee"
                ok = User.IsInRole("Manager") Or User.IsInRole("Administrator") Or User.IsInRole("SysOp")
        End Select
        '        ok = User.IsInRole("Vendor")
        '        ok = User.IsInRole("Carrier")
        '        ok = User.IsInRole("Client")
        Return ok
    End Function

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

    Private Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        Dim arg As String = e.Argument
        If arg.Contains("CertEdit") Then
            Dim eid As String = btnSaveChanges.CommandArgument
            Dim eDAL As New empDAL()
            Dim emp As Employee = eDAL.GetEmployeeByID(New Guid(eid))
            emp.empCertifications = eDAL.getEmployeeCertList(emp.ID)
            lblCerts.Text = ""
            If emp.empCertifications.Count > 0 Then
                For Each crt As Certification In emp.empCertifications
                    Dim clr As String = "Black"
                    If Date.Now > DateAdd(DateInterval.Year, 2, crt.certDate) Then 'over 2
                        clr = "Red"
                    ElseIf Date.Now > DateAdd(DateInterval.Year, 1, crt.certDate) Then 'over 1
                        clr = "Orange"
                    End If
                    lblCerts.Text &= "<span style=""color:" & clr & ";"">" & Format(crt.certDate, "dd MMM yy") & "</span> : " & crt.certName & "<br />"
                Next
            Else
                lblCerts.Text = "no certs<br />"
            End If
            If User.IsInRole("Manager") Or User.IsInRole("Administrator") Or User.IsInRole("SysOp") Then
                lbtnEditCerts.OnClientClick = "openCertEditor('" & emp.ID.ToString & "');"
                lbtnEditCerts.Visible = True
            Else
                lbtnEditCerts.OnClientClick = Nothing
                lbtnEditCerts.Visible = False
            End If
        ElseIf arg.Contains("Employment") Then

            'do something?


        End If

    End Sub

    Private Sub lbtnShowAllEmployment_Click(sender As Object, e As System.EventArgs) Handles lbtnShowAllEmployment.Click

        If lbtnShowAllEmployment.Text = "Show All" Then
            lbtnShowAllEmployment.Text = "Recent 3"
        Else
            lbtnShowAllEmployment.Text = "Show All"
        End If
        RadGrid2.Rebind()
    End Sub


    Private Sub RadGrid2_PreRender(sender As Object, e As System.EventArgs) Handles RadGrid2.PreRender
        If (User.IsInRole("SysOp") Or User.IsInRole("Administrator")) Then
            RadGrid2.MasterTableView.Columns.FindByUniqueName("EditCommandColumn").Visible = True
            RadGrid2.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = True
        ElseIf User.IsInRole("Manager") Then
            RadGrid2.MasterTableView.Columns.FindByUniqueName("EditCommandColumn").Visible = True
            RadGrid2.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
        Else
            RadGrid2.MasterTableView.Columns.FindByUniqueName("EditCommandColumn").Visible = False
            RadGrid2.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
        End If

        If RadGrid2.MasterTableView.Items.Count = 1 Then
            RadGrid2.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
        End If

    End Sub

End Class

