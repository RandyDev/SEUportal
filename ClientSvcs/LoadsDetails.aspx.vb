﻿Imports Telerik.Web.UI

Public Class LoadsDetails
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
            If Request.QueryString("startDate") > "" Then dpStartDate.SelectedDate = Request.QueryString("startDate")
            If Request.QueryString("endDate") > "" Then dpEndDate.SelectedDate = Request.QueryString("endDate")
            If Request.QueryString("locaID") > "" Then cbLocations.SelectedValue = Request.QueryString("locaID")
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
            dba.CommandText = "SELECT WorkOrder.LogDate AS Date, Location.Name AS Location, Department.Name AS Dept,  " & _
    "LoadType.Name AS [LoadType], Vendor.Name AS [CustomerName], WorkOrder.VendorNumber AS [VendorNum],  " & _
    "CASE WHEN [ReceiptNumber] > - 1 THEN [ReceiptNumber] ELSE (0) END AS Rect, WorkOrder.PurchaseOrder AS PO,  " & _
    "WorkOrder.Amount AS Amount, Description.Name AS [LoadDesc], Carrier.Name AS Carrier, WorkOrder.TruckNumber AS Truck,  " & _
    "WorkOrder.TrailerNumber AS Trailer, WorkOrder.AppointmentTime AS [ApptTime], WorkOrder.GateTime AS Gate, WorkOrder.DockTime AS Dock, " & _
    "WorkOrder.StartTime AS Start, WorkOrder.CompTime AS Comp, WorkOrder.PalletsUnloaded AS [PallUnld], WorkOrder.DoorNumber AS Door,  " & _
    "WorkOrder.Pieces, WorkOrder.Weight, WorkOrder.Restacks, WorkOrder.PalletsReceived AS [PalRecd], WorkOrder.BadPallets AS [BadPal],  " & _
    "WorkOrder.NumberOfItems AS Items, WorkOrder.CheckNumber AS [Check], WorkOrder.BOL, WorkOrder.CreatedBy AS [CreatedBy],  " & _
    "WorkOrder.Comments, dbo.ufn_UnloaderList(WorkOrder.ID) AS Unloaders " & _
    "FROM WorkOrder INNER JOIN " & _
    "Location ON WorkOrder.LocationID = Location.ID INNER JOIN " & _
    "Department ON WorkOrder.DepartmentID = Department.ID INNER JOIN " & _
    "LoadType ON WorkOrder.LoadTypeID = LoadType.ID INNER JOIN " & _
    "Vendor ON WorkOrder.CustomerID = Vendor.ID INNER JOIN " & _
    "Description ON WorkOrder.LoadDescriptionID = Description.ID INNER JOIN " & _
    "Carrier ON WorkOrder.CarrierID = Carrier.ID " & _
    "WHERE (WorkOrder.LogDate >= @FromLogDate) AND (Location.Name = @Location) AND (WorkOrder.LogDate <= @ToLogDate) " & _
    "ORDER BY Date DESC "

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

    Private Sub lbReportView_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles lbReportView.Command
        Dim argStr As String = String.Empty
        argStr = "~/Reports/Reports.aspx?"
        argStr &= "report=LoadsDetailed&"
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

    Private Sub RadGrid1_PdfExporting(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPdfExportingArgs) Handles RadGrid1.PdfExporting

    End Sub

#End Region

End Class