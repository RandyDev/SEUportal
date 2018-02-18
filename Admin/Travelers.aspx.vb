Imports Telerik.Web.UI
Imports System.Data.SqlTypes

Public Class Travelers
    Inherits System.Web.UI.Page

    Public Shared ReadOnly Null As SqlDateTime

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' populate Locations Combo Box (cbLocations on aspx page)
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
            If Utilities.IsValidGuid(cbLocations.SelectedValue) Then
                createTravelers(New Guid(cbLocations.SelectedValue.ToString))
            Else
                '            locaval = "1"
            End If
            Dim dt As DataTable = ldal.getLocations()
            cbTravelLocations.DataSource = dt
            cbTravelLocations.DataTextField = "LocationName"
            cbTravelLocations.DataValueField = "locaID"
            cbTravelLocations.DataBind()
            lbTravelPool.Height = 40
            dpStartDate.SelectedDate = FormatDateTime(Date.Now(), DateFormat.ShortDate)
        End If
    End Sub

    Private Sub cbLocations_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        Session("locaid") = cbLocations.SelectedValue.ToString
        If Utilities.IsValidGuid(cbLocations.SelectedValue) Then
            createTravelers(New Guid(cbLocations.SelectedValue.ToString))
        Else
            '            locaval = "1"
        End If

    End Sub

    Private Sub createTravelers(ByVal locaID As Guid)
        Dim dt As New DataTable()
        Dim edal As New empDAL()
        dt = edal.getWorkersByLocation(New Guid(cbLocations.SelectedValue.ToString))
        lbTravelPool.DataSource = dt
        lbTravelPool.Height = (dt.Rows.Count * 22) + 5
        lbTravelPool.DataValueField = "ID"
        lbTravelPool.DataTextField = "Name"
        lbTravelPool.DataBind()
    End Sub

    Private Function validateTravelerList() As String
        Dim retString As String = String.Empty
        If lbTravelTeam.Items.Count < 1 Then
            retString = "Please select Traveler(s)<br />"
        End If
        If cbTravelLocations.SelectedIndex < 0 Then
            retString &= "Please select Destination<br />"
        End If
        If dpStartDate.SelectedDate Is Nothing Then
            retString &= "Please select Start Date<br />"
        End If
        If Not dpReturnDate.SelectedDate Is Nothing Then
            If dpReturnDate.SelectedDate < dpStartDate.SelectedDate Then
                retString &= "Time travel not yet possible!<br />Return Date is before Start Date<br />Select new date or leave empty"
            End If
        End If

        Return retString
    End Function

    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim retStr As String = validateTravelerList()
        If retStr.Length > 1 Then
            lblErrMessage.Text = retStr
            Exit Sub
        Else
            lblErrMessage.Text = ""
        End If
        Dim ulc As RadListBoxItemCollection
        ulc = lbTravelTeam.Items
        Dim dba As New DBAccess()
        For Each it As RadListBoxItem In ulc
            Dim trav As New Traveler()
            trav.travelID = Guid.NewGuid()
            trav.rtdsEmployeeID = New Guid(it.Value.ToString())
            'get each employee's current home location
            dba.CommandText = "SELECT LocationID FROM Employee WHERE ID=@eid"
            dba.AddParameter("@eid", trav.rtdsEmployeeID)
            trav.homeLocation = New Guid(dba.ExecuteScalar.ToString())
            trav.startDate = FormatDateTime(dpStartDate.SelectedDate, DateFormat.ShortDate)
            If dpReturnDate.SelectedDate Is Nothing Then
                trav.returnDate = FormatDateTime("12/31/9999", DateFormat.ShortDate)
            Else
                trav.returnDate = FormatDateTime(dpReturnDate.SelectedDate, DateFormat.ShortDate)

            End If
            If cbTravelLocations.SelectedValue.ToString().Length > 30 Then
                trav.travelLocation = New Guid(cbTravelLocations.SelectedValue.ToString())
            Else
                trav.travelLocation = New Guid("00000000-0000-0000-0000-000000000000")
            End If
            trav.loadMoney = (rblTravTad.SelectedValue = "TAD")
            Dim trDAL As New TravelerDAL()
            Dim insErr As String = trDAL.insertTraveler(trav)
        Next
        createTravelers(New Guid(cbLocations.SelectedValue.ToString))
        lbTravelTeam.Items.Clear()
        cbTravelLocations.ClearSelection()
        dpStartDate.Clear()
        dpReturnDate.Clear()
        RadGrid1.Rebind()

    End Sub

    Private Sub RadGrid1_DeleteCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.DeleteCommand
        If TypeOf e.Item Is GridDataItem Then
            Dim itm As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim delitem As Guid = itm.OwnerTableView.DataKeyValues(itm.ItemIndex)("travelID")
            Dim tdal As New TravelerDAL
            Dim delMsg As String = tdal.deleteTraveler(delitem)
            RadGrid1.Rebind()
        End If
    End Sub

    Private Sub RadGrid1_UpdateCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.UpdateCommand
        Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)

        If (TypeOf e.Item Is GridEditFormItem AndAlso e.Item.IsInEditMode) Then
            Dim editFormItem As GridEditFormItem = CType(e.Item, GridEditFormItem)
            Dim parentItem As GridDataItem = editFormItem.ParentItem
            Dim trv As New Traveler
            trv.travelID = editedItem.GetDataKeyValue("travelID")
            Dim dpsDate As RadDatePicker = CType(editFormItem.FindControl("dpStartDate"), RadDatePicker)
            trv.startDate = dpsDate.SelectedDate
            Dim dprDate As RadDatePicker = CType(editFormItem.FindControl("dpReturnDate"), RadDatePicker)
            If Not dprDate.SelectedDate Is Nothing Then
                trv.returnDate = dprDate.SelectedDate
            Else
                trv.returnDate = FormatDateTime("12/31/9999", DateFormat.ShortDate)
            End If
            Dim numsalaryWeek As RadNumericTextBox = CType(editFormItem.FindControl("numsalaryWeek"), RadNumericTextBox)
            If Not numsalaryWeek.Value Is Nothing Then
                trv.salaryWeek = numsalaryWeek.Value
            Else
                trv.salaryWeek = 0
            End If
            Dim numperDiemWeek As RadNumericTextBox = CType(editFormItem.FindControl("numperDiemWeek"), RadNumericTextBox)
            If Not numperDiemWeek.Value Is Nothing Then
                trv.perDiemWeek = numperDiemWeek.Value
            Else
                trv.perDiemWeek = 0
            End If

            Dim sdate As Date = FormatDateTime(trv.startDate, DateFormat.ShortDate)
            Dim rdate As Date = FormatDateTime(trv.returnDate, DateFormat.ShortDate)
            Dim tdal As New TravelerDAL
            Dim err As String = tdal.updateTravelerDate(trv)
        End If


    End Sub

    Private Sub RadGrid1_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        Select Case e.CommandName
            Case "BeginAssignment"
                If (TypeOf e.Item Is GridDataItem AndAlso Not e.Item.IsInEditMode AndAlso Page.IsPostBack) Then
                    Dim item As GridDataItem = e.Item
                    Dim tvid As Guid = New Guid(item.GetDataKeyValue("travelID").ToString)
                    Dim tdal As New TravelerDAL
                    Dim errMsg As String = tdal.beginTravelAssignment(tvid)
                    RadGrid1.Rebind()
                    RadGrid2.Rebind()

                End If
            Case Else
                Dim b As String = String.Empty
        End Select
        'ElseIf (TypeOf e.Item Is GridDataItem AndAlso Not e.Item.IsInEditMode AndAlso Page.IsPostBack) Then
        '    Dim item As GridDataItem = e.Item
        '    Dim label As Label = item.FindControl("Label1")

        '    'update the label value

        '    label.Text = Session("updatedValue")    

    End Sub

    Private Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If (TypeOf e.Item Is GridEditableItem AndAlso e.Item.IsInEditMode) Then
            Dim item As GridEditableItem = e.Item
            'access/modify the edit item template settings here
            Dim eDate As RadDatePicker = CType(item.FindControl("dpreturnDate"), RadDatePicker)
            If Not eDate.SelectedDate Is Nothing Then
                If FormatDateTime(eDate.SelectedDate, DateFormat.ShortDate) = "1/1/1990" Or FormatDateTime(eDate.SelectedDate, DateFormat.ShortDate) = "12/31/9999" Then
                    eDate.Clear()
                End If
            End If
            'ElseIf (TypeOf e.Item Is GridDataItem AndAlso Not e.Item.IsInEditMode AndAlso Page.IsPostBack) Then
            '    Dim item As GridDataItem = e.Item
            '    Dim label As Label = item.FindControl("Label1")

            '    'update the label value

            '    label.Text = Session("updatedValue")
        End If

    End Sub

    Private Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim trvDAL As New TravelerDAL()
        Dim dt As DataTable = New DataTable()
        dt = trvDAL.getPendingTravelers()
        RadGrid1.DataSource = dt

    End Sub

    Private Sub RadGrid2_DeleteCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid2.DeleteCommand
        If TypeOf e.Item Is GridDataItem Then
            Dim itm As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim delitem As Guid = itm.OwnerTableView.DataKeyValues(itm.ItemIndex)("travelID")
            Dim tdal As New TravelerDAL
            Dim delMsg As String = tdal.deleteTraveler(delitem)
            RadGrid2.Rebind()
        End If
    End Sub

    Private Sub RadGrid2_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid2.ItemCommand
        Select Case e.CommandName
            Case "EndAssignment"
                If (TypeOf e.Item Is GridDataItem AndAlso Not e.Item.IsInEditMode AndAlso Page.IsPostBack) Then
                    Dim item As GridDataItem = e.Item
                    Dim tid As Guid = New Guid(item.GetDataKeyValue("travelID").ToString)
                    Dim tdal As New TravelerDAL
                    Dim errMsg As String = tdal.endTravelAssignment(tid)
                    RadGrid2.Rebind()
                    RadGrid3.Rebind()
                End If
            Case Else
                Dim b As String = String.Empty
        End Select
        'ElseIf (TypeOf e.Item Is GridDataItem AndAlso Not e.Item.IsInEditMode AndAlso Page.IsPostBack) Then
        '    Dim item As GridDataItem = e.Item
        '    Dim label As Label = item.FindControl("Label1")

        '    'update the label value

        '    label.Text = Session("updatedValue")    

    End Sub

    Private Sub RadGrid2_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid2.ItemDataBound
        If (TypeOf e.Item Is GridEditableItem AndAlso e.Item.IsInEditMode) Then
            Dim item As GridEditableItem = e.Item
            'access/modify the edit item template settings here
            Dim eDate As RadDatePicker = CType(item.FindControl("dpreturnDate"), RadDatePicker)
            If Not eDate.SelectedDate Is Nothing Then
                If FormatDateTime(eDate.SelectedDate, DateFormat.ShortDate) = "1/1/1990" Or FormatDateTime(eDate.SelectedDate, DateFormat.ShortDate) = "12/31/9999" Then
                    eDate.Clear()
                End If
            End If
            'ElseIf (TypeOf e.Item Is GridDataItem AndAlso Not e.Item.IsInEditMode AndAlso Page.IsPostBack) Then
            '    Dim item As GridDataItem = e.Item
            '    Dim label As Label = item.FindControl("Label1")

            '    'update the label value

            '    label.Text = Session("updatedValue")
        End If

    End Sub

    Private Sub RadGrid2_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid2.NeedDataSource
        Dim trvDAL As New TravelerDAL()
        Dim dt As DataTable = New DataTable()
        dt = trvDAL.getActiveTravelers()
        RadGrid2.DataSource = dt
    End Sub

    Private Sub RadGrid2_UpdateCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid2.UpdateCommand
        Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
        If (TypeOf e.Item Is GridEditFormItem AndAlso e.Item.IsInEditMode) Then
            Dim editFormItem As GridEditFormItem = CType(e.Item, GridEditFormItem)
            Dim parentItem As GridDataItem = editFormItem.ParentItem
            Dim tdal As New TravelerDAL()
            Dim trv As Traveler = tdal.getTraveler(New Guid(editedItem.GetDataKeyValue("travelID").ToString()))

            Dim dpsDate As RadDatePicker = CType(editFormItem.FindControl("dpStartDate"), RadDatePicker)
            Dim sdate As Date = FormatDateTime(dpsDate.SelectedDate, DateFormat.ShortDate)
            trv.startDate = DateAdd(DateInterval.Minute, 1, sdate)

            Dim dprDate As RadDatePicker = CType(editFormItem.FindControl("dpReturnDate"), RadDatePicker)
            If Not dprDate.SelectedDate Is Nothing Then
                trv.returnDate = FormatDateTime(dprDate.SelectedDate, DateFormat.ShortDate)
            Else
                trv.returnDate = FormatDateTime("12/31/9999", DateFormat.ShortDate)
            End If
            Dim numsalaryWeek As RadNumericTextBox = CType(editFormItem.FindControl("numsalaryWeek"), RadNumericTextBox)
            If Not numsalaryWeek.Value Is Nothing Then
                trv.salaryWeek = numsalaryWeek.Value
            Else
                trv.salaryWeek = 0
            End If
            Dim numperDiemWeek As RadNumericTextBox = CType(editFormItem.FindControl("numperDiemWeek"), RadNumericTextBox)
            If Not numperDiemWeek.Value Is Nothing Then
                trv.perDiemWeek = numperDiemWeek.Value
            Else
                trv.perDiemWeek = 0
            End If
            Dim err As String = tdal.updateTravelerDate(trv)
        End If

    End Sub

    Private Sub RadGrid3_DeleteCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid3.DeleteCommand
        If TypeOf e.Item Is GridDataItem Then
            Dim itm As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim delitem As Guid = itm.OwnerTableView.DataKeyValues(itm.ItemIndex)("travelID")
            Dim tdal As New TravelerDAL
            Dim delMsg As String = tdal.deleteTraveler(delitem)
            RadGrid3.Rebind()
        End If
    End Sub

    Private Sub RadGrid3_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid3.ItemCommand

    End Sub

    Private Sub RadGrid3_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid3.ItemDataBound
        If (TypeOf e.Item Is GridEditableItem AndAlso e.Item.IsInEditMode) Then
            Dim item As GridEditableItem = e.Item
            'access/modify the edit item template settings here
            Dim eDate As RadDatePicker = CType(item.FindControl("dpreturnDate"), RadDatePicker)
            If Not eDate.SelectedDate Is Nothing Then
                If FormatDateTime(eDate.SelectedDate, DateFormat.ShortDate) = "1/1/1990" Or FormatDateTime(eDate.SelectedDate, DateFormat.ShortDate) = "12/31/9999" Then
                    eDate.Clear()
                End If
            End If
            'ElseIf (TypeOf e.Item Is GridDataItem AndAlso Not e.Item.IsInEditMode AndAlso Page.IsPostBack) Then
            '    Dim item As GridDataItem = e.Item
            '    Dim label As Label = item.FindControl("Label1")

            '    'update the label value

            '    label.Text = Session("updatedValue")
        End If

    End Sub

    Private Sub RadGrid3_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid3.NeedDataSource
        Dim trvDAL As New TravelerDAL()
        Dim dt As DataTable = New DataTable()
        dt = trvDAL.getTravelHistory()
        RadGrid3.DataSource = dt
    End Sub


End Class