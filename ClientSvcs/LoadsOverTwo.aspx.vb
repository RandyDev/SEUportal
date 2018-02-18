Imports Telerik.Web.UI

Public Class LoadsOverTwo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim uid As String = puser.ProviderUserKey.ToString
            Dim rtdsID As String
            rtdsID = Utilities.getRTDSidByUserID(uid)
            Dim emp As New Employee
            If rtdsID <> "00000000-0000-0000-0000-000000000000" Then
                Dim rtdsIDtoGUID As New Guid(rtdsID)
                Dim empdal As New empDAL
                emp = empdal.GetEmployeeByID(rtdsIDtoGUID)
            End If
            Dim udal As New userDAL()
            Dim ldal As New locaDAL()
            emp.ssUser = udal.getUserByName(puser.UserName)
            Dim dt As New DataTable
            If emp.ssUser.myRoles.Count = 1 And (emp.ssUser.myRoles(0) = "Manager" Or emp.ssUser.myRoles(0) = "Client" Or emp.ssUser.myRoles(0) = "Guest") Then
                dt = ldal.getUserLocaList(emp.ssUser.userID)
                emp.LocationID = dt.Rows(0).Item("locaID")
            Else
                dt = ldal.getLocations
            End If
            cbLocations.DataSource = dt
            cbLocations.DataTextField = "LocationName"
            cbLocations.DataValueField = "locaID"
            cbLocations.DataBind()
            cbLocations.ClearSelection()
            cbLocations.SelectedValue = emp.LocationID.ToString
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager") Or User.IsInRole("Client")
            dpEndDate.SelectedDate = FormatDateTime(Date.Now(), DateFormat.ShortDate)
            dpStartDate.SelectedDate = FormatDateTime(DateAdd(DateInterval.Day, -14, Date.Now), DateFormat.ShortDate)
            If Request.QueryString("locaID") > "" Then cbLocations.SelectedValue = Request.QueryString("locaID")
            If Request.QueryString("locaName") > "" Then cbLocations.SelectedItem.Text = Request.QueryString("locaName")
            If Request.QueryString("endDate") > "" Then dpEndDate.SelectedDate = Request.QueryString("endDate")
            If Request.QueryString("startDate") > "" Then dpStartDate.SelectedDate = Request.QueryString("startDate")
        End If
    End Sub

    Private Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim date1 As Date = dpStartDate.SelectedDate
        Dim date2 As Date = dpEndDate.SelectedDate
        If cbLocations.SelectedIndex > -1 Then
            Dim loca As String = cbLocations.SelectedItem.Text
            Dim rootDir As String = Server.MapPath("~/")
            Dim logoBG As String = "url(" & Utilities.getLogobg(New Guid(cbLocations.SelectedValue), rootDir) & ")"
            theBody.Style.Item("BACKGROUND-IMAGE") = logoBG
            Dim dba As New DBAccess()
            dba.CommandText = "SELECT dbo.WorkOrder.LogDate [Date], Location.Name AS Location, Vendor.Name AS [CustomerID], WorkOrder.VendorNumber,  " & _
                "WorkOrder.AppointmentTime AS AppointmentTime, WorkOrder.DockTime AS DockTime,  " & _
                "WorkOrder.StartTime AS StartTime, WorkOrder.CompTime AS CompTime,  " & _
                "WorkOrder.CompTime - WorkOrder.DockTime AS [HrsfromDock],  " & _
                "WorkOrder.CompTime - WorkOrder.StartTime AS [HrsfromAssigned], WorkOrder.Comments, WorkOrder.DoorNumber  " & _
                "FROM WorkOrder INNER JOIN  " & _
                "Location ON WorkOrder.LocationID = Location.ID INNER JOIN  " & _
                "Vendor ON WorkOrder.CustomerID = Vendor.ID  " & _
                "WHERE (WorkOrder.LogDate >= @FromLogDate) AND (WorkOrder.LogDate <= DATEADD(n, 1439, @ToLogDate)) AND (Location.Name = @Location) AND  " & _
                "(WorkOrder.TruckNumber <> 'Drop') AND (WorkOrder.CompTime - WorkOrder.DockTime > CONVERT(DATETIME, '1900-01-01 01:59:00', 102)) AND  " & _
                "(WorkOrder.DoorNumber <> N'Drop')  " & _
                "ORDER BY Date, [CustomerID] "
            dba.AddParameter("@FromLogDate", date1)
            dba.AddParameter("@ToLogDate", date2)
            dba.AddParameter("@Location", loca)
            Dim dt As New DataTable
            Try
                dt = dba.ExecuteDataSet.Tables(0)
            Catch ex As Exception

            End Try
            RadGrid1.DataSource = dt
        End If

    End Sub

    Private Sub btnShowRecords_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnShowRecords.Command
        RadGrid1.Rebind()
    End Sub

    Protected Sub RadAjaxManager1_AjaxSettingCreating(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxSettingCreatingEventArgs) Handles RadAjaxManager1.AjaxSettingCreating
        If (e.Initiator.ID.StartsWith("mngBtn")) Then
            e.UpdatePanel.RenderMode = UpdatePanelRenderMode.Inline
        End If
    End Sub

#Region "Exporting"

    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        RadGrid1.ExportSettings.ExportOnlyData = True
        RadGrid1.ExportSettings.IgnorePaging = True
        RadGrid1.ExportSettings.OpenInNewWindow = True
        RadGrid1.MasterTableView.ExportToExcel()
    End Sub

    Protected Sub btnWord_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        RadGrid1.ExportSettings.ExportOnlyData = True
        RadGrid1.ExportSettings.IgnorePaging = True
        RadGrid1.ExportSettings.OpenInNewWindow = True
        RadGrid1.MasterTableView.ExportToWord()
    End Sub
    Protected Sub btnCSV_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        RadGrid1.ExportSettings.ExportOnlyData = True
        RadGrid1.ExportSettings.IgnorePaging = True
        RadGrid1.ExportSettings.OpenInNewWindow = True
        RadGrid1.MasterTableView.ExportToCSV()
    End Sub
    Protected Sub btnPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        RadGrid1.ExportSettings.ExportOnlyData = True
        RadGrid1.ExportSettings.IgnorePaging = True
        RadGrid1.ExportSettings.OpenInNewWindow = True
        RadGrid1.MasterTableView.ExportToPdf()
    End Sub

    Protected Sub RadGrid1_GridExporting(ByVal source As Object, ByVal e As GridExportingArgs)
        Select Case e.ExportType
            Case ExportType.Excel
                'do something with the e.ExportOutput value
                Exit Select
            Case ExportType.ExcelML
                'do something with the e.ExportOutput value
                Exit Select
            Case ExportType.Word
                'do something with the e.ExportOutput value
                Exit Select
            Case ExportType.Csv
                'do something with the e.ExportOutput value
                Exit Select
            Case ExportType.Pdf
                'you can't change the output here - use the PdfExporting event instead
                Exit Select
        End Select
    End Sub

    Private Sub RadGrid1_PdfExporting(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPdfExportingArgs) Handles RadGrid1.PdfExporting

    End Sub

#End Region

    Private Sub lbReportView_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles lbReportView.Command
        Dim argStr As String = String.Empty
        argStr = "~/Reports/Reports.aspx?"
        argStr &= "report=LoadsOverTwoHours&"
        If Not dpStartDate.SelectedDate Is Nothing Then
            argStr &= "startDate=" & dpStartDate.SelectedDate & "&"
        End If
        If Not dpEndDate.SelectedDate Is Nothing Then
            argStr &= "endDate=" & dpEndDate.SelectedDate & "&"
        End If
        If cbLocations.SelectedIndex > -1 Then
            argStr &= "locaID=" & cbLocations.SelectedValue & "&"
            argStr &= "locaName=" & cbLocations.SelectedItem.Text & "&"
        Else
            argStr &= "locaID=""""&"
            argStr &= "locaName=""""&"
        End If
        Response.Redirect(argStr)
    End Sub
End Class