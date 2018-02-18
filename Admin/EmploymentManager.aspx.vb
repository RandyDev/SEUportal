Imports Telerik.Web.UI
Public Class EmploymentManager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ' populate Locations Combo Box (cbLocations on aspx page)
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            Dim showFern As Boolean = User.IsInRole("Administrator") Or User.IsInRole("SysOp")
            ldal.setLocaCombo(puser, cbLocations, showFern)
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
        End If

    End Sub

    Private Sub RadGrid1_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid1.ItemCommand
        Dim vCommandName As String = e.CommandName
        If vCommandName = "Edit" Then
            Dim delitem As Guid = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID")
            Dim sdelitem As String = delitem.ToString
            Dim empid As String = sdelitem
            RadGrid1.MasterTableView.ClearEditItems()
            RadAjaxManager1.ResponseScripts.Add("openEmploymentEditor('" & empid & "');")
        End If

        If vCommandName = "RowClick" Then
            Dim empid As String = RadGrid1.SelectedValue.ToString
            RadAjaxManager1.ResponseScripts.Add("openEmploymentEditor('" & empid & "');")
        End If
        If vCommandName = "Cancel" Then
            RadGrid1.MasterTableView.IsItemInserted = False
            Exit Sub
        End If
    End Sub


    Private Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim empdal As New empDAL()
        Dim dt As DataTable = empdal.getWorkersByLocation(New Guid(cbLocations.SelectedValue))
        RadGrid1.DataSource = dt
    End Sub


    Private Sub RadGrid2_DeleteCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid2.DeleteCommand
        Dim itm As GridDataItem = DirectCast(e.Item, GridDataItem)
        Dim delitem As Guid = itm.OwnerTableView.DataKeyValues(itm.ItemIndex)("ID")
        Dim dba As New DBAccess()
        dba.CommandText = "DELETE FROM PendingEmployment WHERE ID = @ID"
        dba.AddParameter("@ID", delitem)
        dba.ExecuteNonQuery()
    End Sub

    Private Sub RadGrid2_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid2.ItemCommand
        Dim vcommand As String = e.CommandName
        If vcommand = "Commit" Then
            Dim edal As New empDAL
            Dim cid As Guid = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID")
            Dim dba As New DBAccess
            dba.CommandText = "SELECT EmployeeID, DateOfEmployment, DateOfDismiss, JobTitle, PayType, " & _
                "PayRateHourly, PayRatePercentage, SpecialPay, HolidayPay, SalaryPay, ID " & _
                "FROM PendingEmployment " & _
                "WHERE ID = @id"
            dba.AddParameter("@id", cid)
            Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
            Dim cemployment As New Employment
            cemployment.EmployeeID = dt.Rows(0).Item("EmployeeID")
            cemployment.DateOfEmployment = dt.Rows(0).Item("DateOfEmployment")
            cemployment.DateOfDismiss = dt.Rows(0).Item("DateOfDismiss")
            cemployment.JobTitle = dt.Rows(0).Item("JobTitle")
            cemployment.PayType = dt.Rows(0).Item("PayType")
            cemployment.PayRateHourly = dt.Rows(0).Item("PayRateHourly")
            cemployment.PayType = dt.Rows(0).Item("PayType")
            cemployment.PayRateHourly = dt.Rows(0).Item("PayRateHourly")
            cemployment.PayRatePercentage = dt.Rows(0).Item("PayRatePercentage")
            cemployment.SalaryPay = dt.Rows(0).Item("SalaryPay")
            cemployment.ID = dt.Rows(0).Item("ID")

            Dim emp As New Employee
            emp = edal.GetEmployeeByID(cemployment.EmployeeID)
            Dim emplmntList As New List(Of Employment)
            emplmntList = edal.GetEmploymentList(emp.ID)
            emp.Employment = emplmntList(0)
            'set this employment to stop one prior to startdate
            Dim disdate As Date = cemployment.DateOfEmployment
            emp.Employment.DateOfDismiss = DateAdd(DateInterval.Day, -1, disdate)
            edal.updateEmployment(emp)
            emp.Employment = cemployment
            edal.insertEmployment(emp)
            dba.CommandText = "DELETE FROM PendingEmployment WHERE ID = @id"
            dba.AddParameter("@id", cemployment.ID)
            dba.ExecuteNonQuery()
            RadGrid2.Rebind()

        End If
    End Sub

    Private Sub RadGrid2_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles RadGrid2.ItemDataBound
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

            Dim empid As String = DirectCast(e.Item.DataItem, DataRowView)("EmployeeID").ToString
            Dim dba As New DBAccess
            dba.CommandText = "Select LocationID FROM Employee WHERE ID = @empid"
            dba.AddParameter("@empid", empid)
            Dim locaid As String = dba.ExecuteScalar.ToString
            dba.CommandText = "SELECT JobTitle.JobTitleID, JobTitle.JobTitle " & _
                "FROM JobTitle INNER JOIN " & _
                "LocationJobTitle ON JobTitle.JobTitleID = LocationJobTitle.JobTitleID " & _
                "WHERE (LocationJobTitle.LocationID = @locaid) ORDER BY JobTitle"
            dba.AddParameter("@locaid", locaid)
            Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
            Dim cbJobTitle As RadComboBox = e.Item.FindControl("gridcbJobTitle")
            cbJobTitle.DataSource = dt
            cbJobTitle.DataTextField = "JobTitle"
            cbJobTitle.DataValueField = "JobTitleID"
            cbJobTitle.DataBind()
            Dim jt As String = DirectCast(e.Item.DataItem, DataRowView)("JobTitle")
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
    Protected Sub cbLocations_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        RadGrid1.Rebind()
    End Sub


    Private Sub RadAjaxManager1_AjaxRequest(sender As Object, e As AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        RadGrid2.Rebind()
    End Sub

    Private Sub RadGrid2_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGrid2.NeedDataSource
        Dim dba As New DBAccess
        dba.CommandText = "SELECT PendingEmployment.ID, PendingEmployment.EmployeeID, Location.Name AS LocaName, " & _
            "Employee.LastName + ' ' + Employee.FirstName + ' (' + Employee.Login + ')' AS EmployeeName, " & _
            "PendingEmployment.PayType, PendingEmployment.PayRateHourly, PendingEmployment.PayRatePercentage, " & _
            "PendingEmployment.JobTitle, PendingEmployment.DateOfEmployment, PendingEmployment.DateOfDismiss FROM Employee " & _
            "INNER JOIN Location ON Employee.LocationID = Location.ID INNER JOIN " & _
            "PendingEmployment ON Employee.ID = PendingEmployment.EmployeeID " & _
            "WHERE (PendingEmployment.DateOfDismiss = '12/31/9999') " & _
            "ORDER BY PendingEmployment.DateOfEmployment ASC"
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        RadGrid2.DataSource = dt
    End Sub
End Class