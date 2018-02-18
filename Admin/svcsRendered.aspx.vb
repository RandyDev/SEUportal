Imports Telerik.Web.UI
'Imports System.IO
'Imports System.Drawing

Public Class svcsRendered
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
            If cbLocations.Items.Count > 1 Then
                RadGrid1.Visible = False
            End If
            RadDatePickerFrom.SelectedDate = DateAdd(DateInterval.Day, -7, Date.Now)
            RadDatePickerTo.SelectedDate = Date.Now
        End If
    End Sub

    Private Sub empdatasource()
        EmployeeDataSource.SelectCommand = "SELECT distinct(E.ID), E.LastName, E.LastName + ', ' + E.FirstName + '  (' + E.Login + ')' AS EmpName " & _
            "FROM Employee AS E INNER JOIN " & _
            "Employment ON E.ID = Employment.EmployeeID " & _
            "WHERE ((E.LocationID = @locaID) " & _
            "AND (Employment.DateOfDismiss > " & Date.Now & ")) " & _
            "AND (Employment.PayType=1 OR Employment.PayType=2) " & _
            "AND (E.FirstName <> 'Truck' OR E.LastName <> 'Driver') " & _
            "ORDER BY E.LastName, E.ID "
        EmployeeDataSource.SelectParameters.Clear()
        Dim param0 As Parameter = New Parameter("datenow", DbType.Date, Date.Now.ToShortDateString)
        EmployeeDataSource.SelectParameters.Add(param0)
        Dim param1 As Parameter = New Parameter("locaID", DbType.Guid, cbLocations.SelectedValue)
        EmployeeDataSource.SelectParameters.Add(param1)
    End Sub

    Private Sub RadGrid1_DeleteCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid1.DeleteCommand
        Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
        Dim datakey As String = editedItem.GetDataKeyValue("ID").ToString
        Dim dba As New DBAccess
        dba.CommandText = "DELETE FROM ServicesRendered where id = @id"
        dba.AddParameter("@id", datakey)
        dba.ExecuteNonQuery()

    End Sub

    Private Sub RadGrid1_ItemUpdated(sender As Object, e As GridUpdatedEventArgs) Handles RadGrid1.ItemUpdated
        Dim udd As String = "updated"
lblcustbillingrate.Visible=false
    End Sub

    Private Sub RadGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim strLocaID As String = cbLocations.SelectedValue
        Dim fromdate As Date = RadDatePickerFrom.SelectedDate
        Dim todate As Date = RadDatePickerTo.SelectedDate

        Dim dba As New DBAccess

        Dim strSQL As String = "SELECT ServicesRendered.ID, JobDescriptions.JobDescription, ServicesRendered.jobDescriptionID, Location.Name AS locationName, ServicesRendered.locationID,  " & _
            "ServicesRendered.jobDate,Employee.FirstName + ' ' + Employee.LastName + ' (' + Employee.Login + ')' AS employeeName, Employee.FirstName, Employee.LastName, Employee.Login, ServicesRendered.employeeID, ServicesRendered.amount,  " & _
            "ServicesRendered.createdByID, ServicesRendered.timeStamp, ServicesRendered.isCurrent " & _
            "FROM ServicesRendered INNER JOIN " & _
            "JobDescriptions ON ServicesRendered.jobDescriptionID = JobDescriptions.ID INNER JOIN " & _
            "Location ON ServicesRendered.locationID = Location.ID INNER JOIN " & _
            "Employee ON ServicesRendered.employeeID = Employee.ID  " & _
            "WHERE jobdate >= @fromdate and jobdate <= @todate AND isCurrent = 1 "
        If Utilities.IsValidGuid(strLocaID) Then
            strSQL &= "AND ServicesRendered.locationID = @LocaID ORDER BY jobDate DESC"
            dba.CommandText = strSQL
            dba.AddParameter("@LocaID", strLocaID)
            dba.AddParameter("@fromdate", fromdate)
            dba.AddParameter("@todate", todate)
        Else
            strSQL &= "ORDER BY jobDate DESC"
            dba.CommandText = strSQL
            dba.AddParameter("@fromdate", fromdate)
            dba.AddParameter("@todate", todate)
        End If
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        Dim srList As New List(Of SvcsRenderedobj)
        If Not dt Is Nothing Then

            If dt.Rows.Count > 0 Then
                Dim sr As SvcsRenderedobj
                For Each row As DataRow In dt.Rows
                    sr = New SvcsRenderedobj
                    sr.ID = row.Item("ID")
                    sr.jobDescription = row.Item("JobDescription")
                    sr.jobDescriptionID = row.Item("JobDescriptionID")
                    sr.location = row.Item("locationName")
                    sr.locationID = row.Item("locationID")
                    sr.jobDate = row.Item("jobDate")
                    sr.employeeName = row.Item("FirstName") & " " & row.Item("LastName") & " (" & row.Item("Login") & ")"
                    sr.employeeID = row.Item("employeeID")
                    sr.amount = row.Item("amount")
                    sr.CreatedByID = row.Item("createdByID")
                    sr.timeStamp = row.Item("timeStamp")
                    sr.isCurrent = row.Item("isCurrent")
                    sr.CreatedBy = ""
                    srList.Add(sr)
                Next
            End If
        End If
        RadGrid1.DataSource = srList
    End Sub

    Private Sub RadGrid1_InsertCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.InsertCommand

        Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
        Dim s As New SvcsRenderedobj

        If TypeOf e.Item Is GridEditableItem Then
            s.ID = Guid.NewGuid
            Dim cbjobdescription As RadComboBox = e.Item.FindControl("cbJobDescription")
            s.jobDescriptionID = New Guid(cbjobdescription.SelectedValue)
            s.locationID = New Guid(cbLocations.SelectedValue)
            Dim cbemp As RadComboBox = e.Item.FindControl("cbEmployee")
            s.employeeID = New Guid(cbemp.SelectedValue)
            Dim dpjobdate As RadDatePicker = e.Item.FindControl("dpJobDate")
            s.jobDate = dpjobdate.SelectedDate
            Dim createdbylabel As Label = e.Item.FindControl("lblCreatedBy")
            s.CreatedByID = HttpContext.Current.Session("userID")
            Dim timestamplabel As Label = e.Item.FindControl("lblTimeStamp")
            s.timeStamp = timestamplabel.Text
            Dim txtamount As RadNumericTextBox = CType(editedItem.FindControl("numAmount"), RadNumericTextBox)
            s.amount = txtamount.Value

            Dim itm As GridDataItem = TryCast(e.Item, GridDataItem)
            s.CreatedIP = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
            s.isCurrent = True
            Dim dba As New DBAccess
            dba.CommandText = "INSERT INTO ServicesRendered (ID, jobDescriptionID, locationID, jobDate, employeeID, amount, createdbyID, createdIP, timeStamp, isCurrent) VALUES (@ID, @jobDescriptionID, @locationID, @jobDate, @employeeID, @amount, @createdbyID, @createdIP, @timeStamp, @isCurrent)"
            dba.AddParameter("@ID", s.ID)
            dba.AddParameter("@jobDescriptionID", s.jobDescriptionID)
            dba.AddParameter("@locationID", s.locationID)
            dba.AddParameter("@jobDate", s.jobDate)
            dba.AddParameter("@employeeID", s.employeeID)
            dba.AddParameter("@amount", s.amount)
            dba.AddParameter("@createdByID", s.CreatedByID)
            dba.AddParameter("@createdIP", s.CreatedIP)
            dba.AddParameter("@timeStamp", s.timeStamp)
            dba.AddParameter("@isCurrent", s.isCurrent)
            dba.ExecuteNonQuery()
            RadGrid1.Rebind()
            Dim a As String = s.location
        End If
    End Sub

    Private function getrate(ByVal jobid As String, ByVal locaid As string) as decimal
        Dim vrate as decimal = 0
dim dba as new dbaccess
dba.commandtext="Select CustBillingRate from LocationJobDescriptions WHERE LocationID=@LocationID and JobDescriptionID=@JobDescriptionID"
dba.addparameter("@LocationID",locaid)
dba.addparameter("@JobDescriptionID",jobid)
vrate = IIf(IsDBNull(dba.executeScalar()),0, dba.ExecuteScalar)


        return vrate
    end function

    Private Sub RadGrid1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand

        Dim c As String = e.CommandName
        If c = "Cancel" Then
            RadGrid1.MasterTableView.IsItemInserted = False
lblcustbillingrate.Visible=false
            Exit Sub
        End If

    End Sub

    Private Sub RadGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        Dim utls As New Utilities
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim dataItem As GridDataItem = e.Item
            Dim sOAmount As SvcsRenderedobj = e.Item.DataItem
            Dim OAmount As Decimal = sOAmount.amount
            Dim lblamount As Label = e.Item.FindControl("lblAmount")
            lblamount.Text = IIf(OAmount = 0, "---", FormatCurrency(OAmount, 2))
        End If

        If e.Item.IsInEditMode Then
            Dim item As GridEditableItem = DirectCast(e.Item, GridEditableItem)
            If Not (TypeOf e.Item Is IGridInsertItem) Then 'NOT in insert mode
                Dim cbjobdesc As RadComboBox = DirectCast(item.FindControl("cbJobDescription"), RadComboBox)
                Dim cbemp As RadComboBox = DirectCast(item.FindControl("cbemployee"), RadComboBox)
                Dim idx As Integer = cbemp.SelectedIndex
                Dim eid As String = cbemp.SelectedValue
                Dim eidx As String = cbemp.SelectedItem.Value
                Dim nm As String = cbemp.SelectedItem.Text


                Dim numamnt As RadNumericTextBox = DirectCast(item.FindControl("numAmount"), RadNumericTextBox)

                Dim selectedItem As New RadComboBoxItem()
                selectedItem.Attributes.Add("jobDescription", cbjobdesc.SelectedValue.ToString)
                cbjobdesc.Items.Add(selectedItem)
                selectedItem.DataBind()
                Dim selectedemp As New RadComboBoxItem()
                selectedemp.Attributes.Add("ID", cbemp.SelectedValue.ToString)
                cbemp.SelectedIndex = idx

            End If
        End If

        If e.Item.ItemType = Telerik.Web.UI.GridItemType.AlternatingItem Or e.Item.ItemType = Telerik.Web.UI.GridItemType.Item Then
            Dim createdbylabel As Label = e.Item.FindControl("lblCreatedBy")
            Dim tmpcbl As String = utls.CreatedByToText(HttpContext.Current.Session("userID"))
            createdbylabel.Text = tmpcbl
        End If

        If (TypeOf e.Item Is GridDataInsertItem AndAlso e.Item.IsInEditMode) Then
            Dim createdbylabel As Label = e.Item.FindControl("lblCreatedBy")
            Dim tmpcbl As String = utls.CreatedByToText(HttpContext.Current.Session("userID"))
            createdbylabel.Text = tmpcbl

            Dim timestamplabel As Label = e.Item.FindControl("lblTimeStamp")
            timestamplabel.Text = Date.Now()

            Dim dp As RadDatePicker = e.Item.FindControl("dpJobDate")
            dp.SelectedDate = Date.Now
            Dim cbemp As RadComboBox = e.Item.FindControl("cbemployee")
            cbemp.SelectedIndex = -1
            cbemp.EmptyMessage = "Select Employee"

            Dim cbjobdescription As RadComboBox = e.Item.FindControl("cbJobDescription")
            cbjobdescription.EmptyMessage = "Select Job Description"
            cbjobdescription.SelectedIndex = -1


        ElseIf (TypeOf e.Item Is GridEditableItem AndAlso e.Item.IsInEditMode) Then
            Dim xstr As String = "editableitem in edit mode"
            Dim cbjd As RadComboBox = e.Item.FindControl("cbJobDescription")
            Dim cbemp As RadComboBox = e.Item.FindControl("cbemployee")

dim jdid as string = cbjd.selectedvalue
dim lid as string = cblocations.selectedvalue
dim jn as string = cbjd.SelectedItem.text
        lblcustbillingrate.Text = "Customer Billing Rate for <b><u>" & jn & "</b></u> is: <b>" & FormatCurrency(getrate(jdid, lid), 2) &"</b>"
        lblcustbillingrate.Visible=true

            Dim itmid As String = ""
            'Dim srID As String = editedItem.GetDataKeyValue("AddCompID").ToString
            'sr = New SvcsRenderedobj
            'sr.ID = row.Item("ID")
            'sr.jobDescription = row.Item("JobDescription")
            'sr.jobDescriptionID = row.Item("JobDescriptionID")
            'sr.location = row.Item("locationName")
            'sr.locationID = row.Item("locationID")
            'sr.jobDate = row.Item("jobDate")
            'sr.employeeName = row.Item("FirstName") & " " & row.Item("LastName") & " (" & row.Item("Login") & ")"
            'sr.employeeID = row.Item("employeeID")
            'sr.amount = row.Item("amount")
            'sr.CreatedByID = row.Item("createdByID")
            'sr.timeStamp = row.Item("timeStamp")
            'sr.isCurrent = row.Item("isCurrent")
            'sr.CreatedBy = ""


        End If

        If e.Item.IsInEditMode Then
            Dim createdbylabel As Label = e.Item.FindControl("lblCreatedBy")
            Dim tmpcbl As String = utls.CreatedByToText(HttpContext.Current.Session("userID"))
            createdbylabel.Text = tmpcbl

            Dim timestamplabel As Label = e.Item.FindControl("lblTimeStamp")
            timestamplabel.Text = Date.Now()

            Dim dp As Telerik.Web.UI.RadDatePicker = e.Item.FindControl("dpJobDate")
            dp.SelectedDate = Date.Now
            
            dim djd as radcombobox = e.item.findcontrol("cbJobDescription")

        End If

    End Sub

    Private Sub cbLocations_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        RadGrid1.Rebind
    End Sub

    Protected Sub RadAjaxManager1_AjaxRequest(sender As Object, e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest

    End Sub

    Private Sub RadGrid1_UpdateCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid1.UpdateCommand
        Dim s As New SvcsRenderedobj
        Dim uc As String = "update command"
        Dim dt As DataTable = New DataTable
        Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
        Dim datakey As String = editedItem.GetDataKeyValue("ID").ToString
        Dim olds As New SvcsRenderedobj
        Dim dba As New DBAccess
        dba.CommandText = "Select * from servicesrendered where id=@id"
        dba.AddParameter("@id", datakey)
        dt = dba.ExecuteDataSet.Tables(0)
        Dim row As DataRow = dt.Rows(0)

        olds.jobDescriptionID = row("jobdescriptionid")
        olds.jobDate = row("jobDate")
        olds.employeeID = row("employeeid")
        olds.amount = row("amount")


        Dim cbjobdescription As RadComboBox = e.Item.FindControl("cbJobDescription")
        s.jobDescriptionID = New Guid(cbjobdescription.SelectedValue)
        s.locationID = New Guid(cbLocations.SelectedValue)
        Dim cbemp As RadComboBox = e.Item.FindControl("cbEmployee")
        s.employeeID = New Guid(cbemp.SelectedValue)
        Dim dpjobdate As RadDatePicker = e.Item.FindControl("dpJobDate")
        s.jobDate = dpjobdate.SelectedDate
        Dim createdbylabel As Label = e.Item.FindControl("lblCreatedBy")
        s.CreatedByID = HttpContext.Current.Session("userID")
        Dim timestamplabel As Label = e.Item.FindControl("lblTimeStamp")
        s.timeStamp = timestamplabel.Text
        Dim txtamount As RadNumericTextBox = CType(e.Item.FindControl("numAmount"), RadNumericTextBox)
        s.amount = txtamount.Value

        Dim itm As GridDataItem = TryCast(e.Item, GridDataItem)
        s.CreatedIP = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
        s.isCurrent = True
        Dim ralog As New AuditRepLogDAL
        If olds.amount <> s.amount Then ralog.UpdateAudit("ServicesRendered", olds.amount.ToString, s.amount.ToString, "Amount", s.ID)
        If olds.jobDescriptionID <> s.jobDescriptionID Then ralog.UpdateAudit("ServicesRendered", olds.jobDescriptionID.ToString, s.jobDescriptionID.ToString, "jobDescriptionID", s.ID)
        If olds.jobDate <> s.jobDate Then ralog.UpdateAudit("ServicesRendered", olds.jobDate.ToString, s.jobDate.ToString, "jobDate", s.ID)
        If olds.employeeID <> s.employeeID Then ralog.UpdateAudit("ServicesRendered", olds.employeeID.ToString, s.employeeID.ToString, "employeeID", s.ID)
        dba.CommandText = "UPDATE ServicesRendered SET jobDescriptionID=@jobDescriptionID, locationID=@locationID, jobDate=@jobDate, employeeID=@employeeID, amount=@amount, createdbyID=@createdbyID, createdIP=@createdIP, timeStamp=@timeStamp, isCurrent=@isCurrent WHERE ID = @id"
        dba.AddParameter("@ID", datakey)
        dba.AddParameter("@jobDescriptionID", s.jobDescriptionID)
        dba.AddParameter("@locationID", s.locationID)
        dba.AddParameter("@jobDate", s.jobDate)
        dba.AddParameter("@employeeID", s.employeeID)
        dba.AddParameter("@amount", s.amount)
        dba.AddParameter("@createdByID", s.CreatedByID)
        dba.AddParameter("@createdIP", s.CreatedIP)
        dba.AddParameter("@timeStamp", s.timeStamp)
        dba.AddParameter("@isCurrent", s.isCurrent)
        dba.ExecuteNonQuery()
        RadGrid1.Rebind()
        Dim a As String = s.location
        lblcustbillingrate.Visible=false
    End Sub

    Private Sub RadGrid1_PreRender(sender As Object, e As EventArgs) Handles RadGrid1.PreRender
        If Not (User.IsInRole("SysOp") Or User.IsInRole("Administrator")) Then
            RadGrid1.MasterTableView.Columns.FindByUniqueName("EditCommandColumn").Visible = True
            RadGrid1.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False '
        Else
            RadGrid1.MasterTableView.Columns.FindByUniqueName("EditCommandColumn").Visible = True
            RadGrid1.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = True
        End If
    End Sub

    Private Sub btnShowRecords_Click(sender As Object, e As EventArgs) Handles btnShowRecords.Click
        RadGrid1.Rebind()
        RadGrid1.Visible = True
        empdatasource()
    End Sub

    Protected Sub cbJobDescription_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Dim jid As String = e.Value
        Dim jidt As String = e.Text
        Dim locaid As String = cbLocations.SelectedValue
        Dim loca As String = cbLocations.Text
        lblcustbillingrate.Text = "Customer Billing Rate for <b><u>" & jidt & "</b></u> is: <b>" & FormatCurrency(getrate(jid, locaid), 2) &"</b>"
        lblcustbillingrate.Visible=true

    End Sub
End Class

