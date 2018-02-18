Imports Telerik.Web.UI

Public Class LoadsSummary
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
        End If
    End Sub

    Private Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim date1 As Date = dpStartDate.SelectedDate
        Dim date2 As Date = dpEndDate.SelectedDate
        Dim loca As String = cbLocations.SelectedItem.Text
        Dim rootDir As String = Server.MapPath("~/")
        Dim logoBG As String = "url(" & Utilities.getLogobg(New Guid(cbLocations.SelectedValue), rootDir) & ")"
        theBody.Style.Item("BACKGROUND-IMAGE") = logoBG
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT dbo.CompanyInformation.Name AS [CompanyName], dbo.CompanyInformation.AddressOne AS Address,  " & _
            "dbo.CompanyInformation.City + ', ' + dbo.CompanyInformation.State + '. ' + dbo.CompanyInformation.Zip AS CityState,  " & _
            "dbo.ParentCompany.Name + ' Logistics ' AS [CompanyHeader], dbo.WorkOrder.LogDate AS Date, dbo.Location.Name AS Location,  " & _
            "dbo.LoadType.Name AS [LoadType], dbo.Department.Name AS Department, dbo.WorkOrder.PurchaseOrder AS [PONum], dbo.Carrier.Name AS Carrier,  " & _
            "dbo.WorkOrder.TrailerNumber AS Trailer, dbo.WorkOrder.Pieces, dbo.WorkOrder.PalletsUnloaded, dbo.Vendor.Name AS Customer,  " & _
            "dbo.WorkOrder.Weight, dbo.WorkOrder.PalletsReceived, dbo.WorkOrder.Amount " & _
            "FROM dbo.ParentCompany RIGHT OUTER JOIN " & _
            "dbo.Location ON dbo.ParentCompany.ID = dbo.Location.ParentCompanyID RIGHT OUTER JOIN " & _
            "dbo.WorkOrder LEFT OUTER JOIN " & _
            "dbo.Vendor ON dbo.WorkOrder.CustomerID = dbo.Vendor.ID LEFT OUTER JOIN " & _
            "dbo.Carrier ON dbo.WorkOrder.CarrierID = dbo.Carrier.ID LEFT OUTER JOIN " & _
            "dbo.Department ON dbo.WorkOrder.DepartmentID = dbo.Department.ID LEFT OUTER JOIN " & _
            "dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID ON dbo.Location.ID = dbo.WorkOrder.LocationID CROSS JOIN " & _
            "dbo.CompanyInformation " & _
            "WHERE (WorkOrder.LogDate >= @startdate) AND (WorkOrder.LogDate <= DATEADD(n, 1439, @enddate)) AND (dbo.Location.Name = @Location) "

        dba.AddParameter("@startdate", date1)
        dba.AddParameter("@enddate", date2)
        dba.AddParameter("@Location", loca)
        Dim dt As New DataTable
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception

        End Try
        RadGrid1.DataSource = dt
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

End Class