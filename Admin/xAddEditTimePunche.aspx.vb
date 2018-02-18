Imports Telerik.Web.UI

Public Class AddEditTimePunche
    Inherits System.Web.UI.Page

    Private tp As TimePunche
    Private emp As Employee
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then 'originates from timecards
            Dim tpID As String = String.Empty
            If Request.QueryString("tpID") > "" Then
                tpID = Request.QueryString("tpID")
                Session("tpID") = tpID
                Dim tpdal As New TimePuncheDAL
                'Get the TimePunche Record
                tp = tpdal.getTimePuncheByID(tpID)
                'Get the Employee for this TimePunche
                Dim edal As New empDAL
                emp = edal.GetEmployeeByID(tp.EmployeeID)
                lblDateWorked.Text = Format(tp.DateWorked, "ddd") & "&nbsp;" & Format(tp.DateWorked, "MMM") & "&nbsp;" & Format(tp.DateWorked, "dd")
                dpDateWorked.SelectedDate = tp.DateWorked
                chkIsClosed.Checked = tp.IsClosed
                chkIsClosed.Style.Item("color") = IIf(tp.IsClosed, "Black", "Orange")
                gridTIO.Enabled = Not chkIsClosed.Checked
                lbtpAction.Visible = gridTIO.Enabled
                populateDepartments(emp.LocationID.ToString)
                cbDepartment.SelectedValue = tp.DepartmentID.ToString
                cbDepartment.Text = tp.DepartmentName
                cbDepartment.Enabled = cbDepartment.SelectedIndex = -1
                cbDepartment.Enabled = gridTIO.Enabled
                cbDepartment.Text = tp.DepartmentName
                lblempName.Text = emp.rtdsFirstName & " " & emp.rtdsLastName
                'find jobdesc and ishourly  from tio
                'Dim tio As TimeInOut = New TimeInOut
                'If tp.tpList.Count > 0 Then
                '    tio = tp.tpList.Item(tp.tpList.Count - 1) ' go to last one
                '    '                    If tio.isHourly = False Then tio.JobDescriptionID = New Guid("A2A2210A-29C3-485E-8C86-31CDB59F39EC")
                '    cbJobDescription.SelectedValue = tio.JobDescriptionID.ToString
                'End If
                'If tio.JobDescriptionID = Utilities.zeroGuid Then
                '    cbJobDescription.Enabled = True
                '    cbDepartment.Enabled = True
                'Else
                '    cbJobDescription.Enabled = False
                '    cbDepartment.Enabled = False
                'End If
                'If chkIsClosed.Checked Then
                '    cbDepartment.Enabled = False
                'End If
                'where is he
                lbtpAction.CommandArgument = tpID
                chkIsClosed.AutoPostBack = True
            ElseIf Request.QueryString("empID") > "" Then   'Arrived with Empid for NEW Car

                Dim edal As New empDAL
                emp = edal.GetEmployeeByID(New Guid(Request.QueryString("empID")))
                lblempName.Text = emp.rtdsFirstName & " " & emp.rtdsLastName


                ''''''''                cbNewPayType.SelectedValue = IIf(emp.Employment.PayType = 2, "1", "0")

                dpDateWorked.SelectedDate = Date.Now()
                dpDateWorked.Visible = True
                lblDateWorked.Visible = False
                chkIsClosed.Enabled = False
                chkIsClosed.Style.Item("color") = IIf(chkIsClosed.Checked, "Black", "Orange")
                populateDepartments(emp.LocationID.ToString)
                'populateJobDescriptions(emp.LocationID.ToString)
                cbDepartment.ClearSelection()

                gridTIO.Visible = False
                lbtpAction.Visible = False
            End If
        Else 'is a postback
            Dim apostbackstring As String = "isPostback->>" & Request.QueryString.ToString
            Dim ba As String = String.Empty

        End If 'originated from timecards
    End Sub
    Private Sub gridTIO_ItemCreated(sender As Object, e As GridItemEventArgs) Handles gridTIO.ItemCreated

        If TypeOf e.Item Is GridEditFormInsertItem And gridTIO.MasterTableView.IsItemInserted Then
            Dim editform As GridEditFormInsertItem = e.Item
            Dim timepcker As RadTimePicker = editform.FindControl("tpTimeIn")

            timepcker.SelectedTime = DateTime.Now.TimeOfDay
        End If
    End Sub

    Private Sub gridTIO_ItemInserted(sender As Object, e As GridInsertedEventArgs) Handles gridTIO.ItemInserted
        If TypeOf e.Item Is GridEditFormInsertItem And gridTIO.MasterTableView.IsItemInserted Then
            Dim editform As GridEditFormInsertItem = e.Item
            Dim timepcker As RadTimePicker = editform.FindControl("tpTimeIn")

            timepcker.SelectedTime = DateTime.Now.TimeOfDay
        End If
    End Sub

    Private Sub gridTIO_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gridTIO.ItemDataBound
        If (TypeOf e.Item Is GridEditableItem AndAlso e.Item.IsInEditMode) Then
            Dim item As GridEditableItem = e.Item
            'access/modify the edit item template settings here
            Dim eDate As RadTimePicker = CType(item.FindControl("tpTimeOut"), RadTimePicker)
            If Not eDate.SelectedDate Is Nothing Then
                If FormatDateTime(eDate.SelectedDate, DateFormat.ShortDate) = "1/1/1900" Then
                    eDate.Clear()
                End If
            End If
            Dim jobdesc As RadComboBox = CType(item.FindControl("cbJobDescription"), RadComboBox)

        End If

        '    Dim boolIsHourly As Boolean = False

        '    Dim vartpid As String = String.Empty
        '    vartpid = Request("tpid")



        '    If TypeOf item Is GridEditFormInsertItem Then
        '        Dim tp As New TimePunche
        '        If Not vartpid = String.Empty Then
        '            Dim tpdal As New TimePuncheDAL()
        '            tp = tpdal.getTimePuncheByID(vartpid)
        '            Dim edal As New empDAL()
        '            Dim employment As Employment = edal.getCurrentEmployment(tp.EmployeeID)
        '            boolIsHourly = (employment.PayType = 2)
        '        End If
        '        'Dim sDate As RadTimePicker = CType(item.FindControl("tpTimeIn"), RadTimePicker)
        '        'sDate.SelectedDate = Date.Now()

        '    ElseIf TypeOf item Is GridEditFormItem Then
        '        boolIsHourly = item.GetDataKeyValue("IsHourly")

        '    End If

        '    Dim mitem As RadComboBoxItem = izHrly.FindItemByValue(IIf(boolIsHourly, "1", "0"))
        '    mitem.Selected = True

        '    'ElseIf (TypeOf e.Item Is GridDataItem AndAlso Not e.Item.IsInEditMode AndAlso Page.IsPostBack) Then
        '    '    Dim item As GridDataItem = e.Item
        '    '    Dim label As Label = item.FindControl("Label1")

        '    '    'update the label value

        '    '    label.Text = Session("updatedValue")
        '        End If
    End Sub
    Private Sub gridTIO_ItemCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles gridTIO.ItemCommand
        'If cbJobDescription.SelectedIndex = -1 Then
        '    lblErrorMsg.Text = "You Must first select a Job Description"
        '    lblErrorMsg.Visible = True
        '    e.Canceled = True
        '    Exit Sub
        'End If
        Select Case e.CommandName
            Case RadGrid.InitInsertCommandName
                Dim tpDAL As New TimePuncheDAL
                Dim tpid As String = Request.QueryString("tpID")
                Dim tp As TimePunche = tpDAL.getTimePuncheByID(tpid)
                If Not tp.tpList Is Nothing Then
                    If tp.tpList(tp.tpList.Count - 1).TimeOut.ToShortDateString = "1/1/1900" Then
                        lblErrorMsg.Text = "Must first Clock Out on previous record."
                        lblErrorMsg.Visible = True
                        e.Canceled = True
                    End If
                End If
            Case RadGrid.PerformInsertCommandName
                lblErrorMsg.Visible = False
                Dim a As String = "1"
            Case RadGrid.EditCommandName
                lblErrorMsg.Visible = False
                Dim a As String = "1"
            Case RadGrid.DeleteCommandName
                lblErrorMsg.Visible = False
                Dim tpDAL As New TimePuncheDAL
                Dim tpid As String = Request.QueryString("tpID")
                Dim tp As TimePunche = tpDAL.getTimePuncheByID(tpid)
                If Not tp.tpList Is Nothing Then
                    If tp.tpList.Count = 1 Then
                        lblErrorMsg.Text = "Cannot delete sole entry.  Delete TimeCard instead. ^^^"
                        lblErrorMsg.Visible = True
                        e.Canceled = True
                    End If
                End If

            Case RadGrid.UpdateCommandName
                lblErrorMsg.Visible = False
                Dim a As String = "1"

        End Select
    End Sub
    Private Sub gridTIO_DeleteCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles gridTIO.DeleteCommand
        If TypeOf e.Item Is GridDataItem Then
            Dim itm As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim delitem As Guid = itm.OwnerTableView.DataKeyValues(itm.ItemIndex)("ID")
            Dim tpDAL As New TimePuncheDAL()
            Dim delMsg As String = tpDAL.deleteTIO(delitem)
        End If
        RadAjaxManager1.ResponseScripts.Add("setReturnArg('TimePunche:delete');")

    End Sub



    Private Sub gridTIO_InsertCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles gridTIO.InsertCommand
        Dim utl As New Utilities
        Dim tio As New TimeInOut
        Dim timeout As Date
        Dim tpdal As New TimePuncheDAL
        Dim item As GridEditFormInsertItem = e.Item
        Dim ti As String = item("TimeIn").ToString
        If (TypeOf e.Item Is GridEditFormItem AndAlso e.Item.IsInEditMode) Then
            Dim editFormItem As GridEditFormItem = CType(e.Item, GridEditFormItem)
            Dim parentItem As GridDataItem = editFormItem.ParentItem
            tio = New TimeInOut
            Dim sDate As RadTimePicker = CType(editFormItem.FindControl("tpTimeIn"), RadTimePicker)
            If Not sDate.SelectedDate Is Nothing Then
                tio.TimeIn = sDate.SelectedDate
                'get timepunche 
                Dim tpid As String = Session("TimePuncheID")
                If tpid = "" Then
                    tpid = Request("")
                End If
                tpdal = New TimePuncheDAL
                Dim etp As TimePunche = tpdal.getTimePuncheByID(tpid)


                'if this is first tio, set sdate date to timepunche dateworked
                'else get last tio set s date to timeout date
                If Not etp.tpList Is Nothing Then 'this is not first tio for timecard
                    Dim prevtio As TimeInOut = etp.tpList.Item(etp.tpList.Count - 1)
                    tio.JobDescriptionID = prevtio.JobDescriptionID
                    'get TimeOut for last punch and set next timein date to that date
                    Dim nxtTimeInDate As Date = etp.tpList(etp.tpList.Count - 1).TimeOut
                    tio.TimeIn = nxtTimeInDate.ToShortDateString & " " & tio.TimeIn.ToShortTimeString
                Else
                    tio.TimeIn = etp.DateWorked.ToShortDateString & " " & tio.TimeIn.ToShortTimeString
                End If

                Dim eDate As RadTimePicker = CType(editFormItem.FindControl("tpTimeOut"), RadTimePicker)
                If eDate.SelectedDate Is Nothing Then
                    timeout = "1/1/1900"
                Else
                    timeout = eDate.SelectedDate
                    timeout = tio.TimeIn.ToShortDateString & " " & timeout.ToShortTimeString
                    If timeout < tio.TimeIn Then timeout = DateAdd(DateInterval.Day, 1, timeout)
                End If
                tio.TimeOut = timeout
                Dim jobdesc As RadComboBox = CType(editFormItem.FindControl("cbJobDescription"), RadComboBox)
                tio.JobDescriptionID = New Guid(jobdesc.SelectedValue.ToString)
                tio.isHourly = utl.isHourly(tio.JobDescriptionID)
                tio.TimepuncheID = etp.ID
                Dim insertErr As String = tpdal.insertTIO(tio)
            End If
            RadAjaxManager1.ResponseScripts.Add("setReturnArg('TimePunche:insert');")
        End If
    End Sub

    Private Sub gridTIO_UpdateCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles gridTIO.UpdateCommand

        Dim utl As New Utilities
        Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
        Dim tio As New TimeInOut
        Dim timeout As Date
        Dim updateStr As String = String.Empty
        Dim tpdal As New TimePuncheDAL
        If (TypeOf e.Item Is GridEditFormItem AndAlso
         e.Item.IsInEditMode) Then
            Dim editFormItem As GridEditFormItem = CType(e.Item, GridEditFormItem)
            Dim parentItem As GridDataItem = editFormItem.ParentItem
            tio.ID = editedItem.GetDataKeyValue("ID")
            tio = tpdal.getTimeInOutByID(tio.ID)
            '           tio.JobDescriptionID = New Guid(cbJobDescription.SelectedValue)
            tio.isHourly = utl.isHourly(tio.JobDescriptionID)
            Dim sDate As RadTimePicker = CType(editFormItem.FindControl("tpTimeIn"), RadTimePicker)
            tio.TimeIn = sDate.SelectedDate

            'get timepunche 

            Dim tpid As String = Request.QueryString("tpID")


            Dim etp As TimePunche = tpdal.getTimePuncheByID(tpid)
            'if this is first tio, set sdate date to timepunche dateworked
            'else get last tio set s date to timeout date
            'get TimeOut for last punch and set next timein date to that date
            Dim tpcount As Integer = etp.tpList.Count
            Dim eDate As RadTimePicker = CType(editFormItem.FindControl("tpTimeOut"), RadTimePicker)
            For i As Integer = 0 To etp.tpList.Count - 1
                'If etp.tpList(i).ID = tio.ID Then 'find this timeinout
                'If i = 0 Then ' the first timepunche
                tio = etp.tpList(i)
                '                tio.JobDescriptionID = New Guid(cbJobDescription.SelectedValue)
                tio.isHourly = utl.isHourly(tio.JobDescriptionID)
                tio.TimeIn = etp.DateWorked.ToShortDateString & " " & tio.TimeIn.ToShortTimeString
                If eDate.SelectedDate Is Nothing Then
                    timeout = "1/1/1900"
                Else
                    timeout = eDate.SelectedDate
                    timeout = tio.TimeIn.ToShortDateString & " " & timeout.ToShortTimeString
                End If
                updateStr = tpdal.updateTIO(tio)
                '    Else
                '        tio.TimeIn = etp.tpList(i - 1).TimeOut.ToShortDateString & " " & tio.TimeIn.ToShortTimeString
                '        If eDate.SelectedDate Is Nothing Then
                '            timeout = "1/1/1900"
                '        Else
                '            timeout = eDate.SelectedDate
                '            timeout = tio.TimeIn.ToShortDateString & " " & timeout.ToShortTimeString
                '        End If
                '        updateStr = tpdal.updateTIO(tio)
                '    End If
                ''End If
            Next

            If timeout < tio.TimeIn Then timeout = DateAdd(DateInterval.Day, 1, timeout)
        End If
        tio.TimeOut = timeout
        updateStr = tpdal.updateTIO(tio)

        RadAjaxManager1.ResponseScripts.Add("setReturnArg('TimePunche:update');")

    End Sub


    Private Sub cbDepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs) Handles cbDepartment.SelectedIndexChanged
        '        lbtnSaveTimeCard.Visible = cbDepartment.SelectedIndex > -1
        lblErrorMsg.Visible = False
        Dim isTPid As Boolean = Not Request.QueryString("tpID") Is Nothing
        If isTPid Then '
            'Dim tp As New TimePunche
            'tp.ID = New Guid(Request.QueryString("tpID"))
            'tp.DepartmentID = New Guid(cbDepartment.SelectedValue)
            'Dim tpdal As New TimePuncheDAL()
            'Dim udstr As String = tpdal.updateTimePuncheDepartment(tp)
            ' if udstr has a value then sumpm went wrong
        Else 'get ready for NEW timepunche record
            lbtpAction.CommandName = "Insert"
            lbtpAction.CommandArgument = Request.QueryString("empID")
            lbtpAction.OnClientClick = Nothing
            lbtpAction.Text = "Create New TimeCard"
            lbtpAction.Visible = True
        End If

        RadAjaxManager1.ResponseScripts.Add("setReturnArg('TimePunche:updateDepartment');")
    End Sub

    Private Sub lbtpAction_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles lbtpAction.Command
        '        Dim tpToDelete As Guid = New Guid(Request.QueryString("tpID"))
        Dim arg As String = String.Empty
        Dim tpdal As New TimePuncheDAL()
        If e.CommandName.Contains("Delete") Then
            arg = e.CommandArgument
            Dim delErr As String = tpdal.deleteTimePunche(New Guid(arg))
            RadAjaxManager1.ResponseScripts.Add("setReturnArg('TimePunche:Delete');Close();")
        ElseIf e.CommandName.Contains("Insert") Then
            arg = e.CommandArgument
            Dim tp As New TimePunche
            tp.EmployeeID = New Guid(arg)
            tp.DepartmentID = New Guid(cbDepartment.SelectedValue.ToString)
            tp.DepartmentName = cbDepartment.SelectedItem.Text
            tp.DateWorked = FormatDateTime(dpDateWorked.SelectedDate, DateFormat.ShortDate)
            'If cbJobDescription.Text.ToUpper.Contains("UNLOAD") Then
            '    tp.DepartmentID = New Guid("27D94AF9-4E89-476B-B310-7D2C48165D7D")
            'Else
            '    tp.DepartmentID = New Guid(cbDepartment.SelectedValue)
            'End If

            Dim loca As String = Request.QueryString("locaID")

            If Utilities.IsValidGuid(loca) Then
                Dim ldal As New locaDAL
                tp.LocationID = New Guid(loca)
                tp.LocationName = ldal.getLocationNameByID(loca)
            End If
            '            Dim JobDescriptionID As Guid = New Guid(cbJobDescription.SelectedValue)
            Dim utl As New Utilities
            Dim errStr As String = tpdal.CreateTimeCard(tp)
            Session("tpid") = tp.ID.ToString
            'open gridTIO in INSERT mode
            gridTIO.Visible = True
            gridTIO.MasterTableView.IsItemInserted = True
            Session("TimePuncheID") = tp.ID.ToString
            Session("locaid") = loca
        End If
    End Sub

    Private Sub chkIsClosed_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkIsClosed.CheckedChanged
        Dim isTPid As Boolean = Not Request.QueryString("tpID") Is Nothing
        Dim tpid As String = Nothing
        If isTPid Then
            cbDepartment.Enabled = False

            tpid = Request.QueryString("tpID")
            Dim tpDAL As New TimePuncheDAL
            If chkIsClosed.Checked Then
                Dim tp As TimePunche = tpDAL.getTimePuncheByID(tpid)
                If tp.tpList Is Nothing Then
                    lblErrorMsg.Text = "Cannot close TimeCard w/ no punches"
                    chkIsClosed.Checked = False
                    lblErrorMsg.Visible = True
                Else
                    ' OR is less than TimeIn might be better????
                    If tp.tpList(tp.tpList.Count - 1).TimeOut.ToShortDateString = "1/1/1900" Then
                        lblErrorMsg.Text = "Must be Clocked Out to close TimeCard"
                        chkIsClosed.Checked = False
                    Else
                        tpDAL.updateTimePuncheIsClosed(tp.ID, chkIsClosed.Checked)
                        gridTIO.Enabled = Not chkIsClosed.Checked
                        lbtpAction.Visible = gridTIO.Enabled
                        lblErrorMsg.Visible = False
                        RadAjaxManager1.ResponseScripts.Add("setReturnArg('TimePunche:updateIsClosed');")
                    End If

                End If
                cbDepartment.Enabled = False

            Else
                If cbDepartment.SelectedIndex = -1 Then
                    cbDepartment.Enabled = True
                Else
                    cbDepartment.Enabled = False

                End If
                tpDAL.updateTimePuncheIsClosed(New Guid(tpid), chkIsClosed.Checked)
                gridTIO.Enabled = Not chkIsClosed.Checked
                lbtpAction.Visible = gridTIO.Enabled
                lblErrorMsg.Visible = False
                RadAjaxManager1.ResponseScripts.Add("setReturnArg('TimePunche:updateIsClosed');")

            End If
        Else
            cbDepartment.Enabled = True

        End If
        chkIsClosed.Style.Item("color") = IIf(chkIsClosed.Checked, "Black", "Orange")
    End Sub

    Private Sub populateJobDescriptions(ByVal locaid As String)
        Dim ldal As New locaDAL
        Dim dt As New DataTable
        dt = ldal.GetJobDescriptionsByLocationID(locaid)
        '    cbJobDescription.DataSource = dt
        '    cbJobDescription.DataTextField = "JobDescription"
        '    cbJobDescription.DataValueField = "ID"
        '    cbJobDescription.DataBind()
    End Sub

    Private Sub populateDepartments(ByVal locaid As String)
        Dim ldal As New locaDAL
        Dim dt As New DataTable
        dt = ldal.GetDepartmentsByLocationID(locaid)
        cbDepartment.DataSource = dt
        cbDepartment.DataValueField = "ID"
        cbDepartment.DataTextField = "Name"
        cbDepartment.DataBind()
    End Sub

    Private Sub gridTIO_EditCommand(sender As Object, e As GridCommandEventArgs) Handles gridTIO.EditCommand

        '    For Each item As GridItem In gridTIO.MasterTableView.Items

        '    '    If TypeOf item Is GridEditableItem Then
        '    '        Dim editableitem As GridEditableItem = item
        '    '        editableitem.Edit = True

        '    '    End If
        '    'Next

    End Sub
End Class

' OnClientClick="if (!confirm('OK, you wanted a confirm dialog when you \nmash the \'Delete This Load Record\' button.\n                          This is it.\nAre you sure you want to delete this load record?')) return false;"