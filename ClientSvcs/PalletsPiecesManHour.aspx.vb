Imports Telerik.Web.UI

Public Class PalletsPiecesManHour
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
        dba.CommandText = "declare @NumOfPOs varchar(50) " & _
            "declare @NumOfLoads integer " & _
            "declare @PalUnld integer " & _
            "declare @Pieces integer " & _
            "declare @PalRecd integer " & _
            "declare @Bad integer " & _
            "declare @Resk integer " & _
            "declare @ttlHours integer " & _
            "declare @PPH integer " & _
            "declare @CPH integer " & _
            "declare @NOL integer " & _
            "set @NumOfPOs = (SELECT COUNT(dbo.WorkOrder.LoadNumber) AS NumOfPOs " & _
            "FROM dbo.WorkOrder INNER JOIN " & _
            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID INNER JOIN " & _
            "dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID " & _
            "WHERE (dbo.Location.Name = @Location) AND (dbo.WorkOrder.LogDate >= @FromLogDate) AND (dbo.WorkOrder.LogDate <= DATEADD(n, 1439, @ToLogDate))) " & _
            "set @NumOfLoads = (SELECT COUNT(DISTINCT Convert(varchar (10),dbo.WorkOrder.LogDate,110) + dbo.LoadType.Name + dbo.WorkOrder.DoorNumber + dbo.WorkOrder.TrailerNumber) AS NumOfLoads " & _
            "FROM dbo.WorkOrder INNER JOIN " & _
            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID INNER JOIN " & _
            "dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID " & _
            "WHERE (dbo.Location.Name = @Location) AND(dbo.WorkOrder.LogDate >= @FromLogDate) AND (dbo.WorkOrder.LogDate <= DATEADD(n, 1439, @ToLogDate))) " & _
            "set @PalUnld = (SELECT SUM(dbo.WorkOrder.PalletsUnloaded) AS PalUnld " & _
            "FROM dbo.WorkOrder INNER JOIN " & _
            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID INNER JOIN " & _
            "dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID " & _
            "WHERE (dbo.Location.Name = @Location) AND (dbo.WorkOrder.LogDate >= @FromLogDate) AND (dbo.WorkOrder.LogDate <= DATEADD(n, 1439, @ToLogDate))) " & _
            "set @Pieces = (SELECT SUM(dbo.WorkOrder.Pieces) AS Pieces " & _
            "FROM dbo.WorkOrder INNER JOIN " & _
            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID INNER JOIN " & _
            "dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID " & _
            "WHERE (dbo.Location.Name = @Location) AND (dbo.WorkOrder.LogDate >= @FromLogDate) AND (dbo.WorkOrder.LogDate <= DATEADD(n, 1439, @ToLogDate))) " & _
            "set @PalRecd = (SELECT SUM(dbo.WorkOrder.PalletsReceived) AS PalRecd " & _
            "FROM dbo.WorkOrder INNER JOIN " & _
            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID INNER JOIN " & _
            "dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID " & _
            "WHERE (dbo.Location.Name = @Location) AND (dbo.WorkOrder.LogDate >= @FromLogDate) AND (dbo.WorkOrder.LogDate <= DATEADD(n, 1439, @ToLogDate))) " & _
            "set @Bad = (SELECT SUM(dbo.WorkOrder.BadPallets) AS Bad " & _
            "FROM dbo.WorkOrder INNER JOIN " & _
            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID INNER JOIN " & _
            "dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID " & _
            "WHERE (dbo.Location.Name = @Location) AND (dbo.WorkOrder.LogDate >= @FromLogDate) AND (dbo.WorkOrder.LogDate <= DATEADD(n, 1439, @ToLogDate))) " & _
            "set @Resk = (SELECT SUM(dbo.WorkOrder.Restacks) AS Resk " & _
            "FROM dbo.WorkOrder INNER JOIN " & _
            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID INNER JOIN " & _
            "dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID " & _
            "WHERE (dbo.Location.Name = @Location) AND (dbo.WorkOrder.LogDate >= @FromLogDate) AND (dbo.WorkOrder.LogDate <= DATEADD(n, 1439, @ToLogDate))) " & _
            "set @ttlHours = (SELECT SUM(dbo.TimeInOut.HoursWorked) AS Hours " & _
            "FROM dbo.Employee INNER JOIN " & _
            "dbo.TimePunche ON dbo.Employee.ID = dbo.TimePunche.EmployeeID INNER JOIN " & _
            "dbo.TimeInOut ON dbo.TimePunche.ID = dbo.TimeInOut.TimepuncheID LEFT OUTER JOIN " & _
            "dbo.Location ON dbo.Employee.LocationID = dbo.Location.ID LEFT OUTER JOIN " & _
            "dbo.Employment ON dbo.Employee.ID = dbo.Employment.EmployeeID " & _
            "WHERE (dbo.Location.Name = @Location) AND (dbo.TimePunche.DateWorked >= @FromLogDate) AND (dbo.TimePunche.DateWorked <= DATEADD(n, 1439, @ToLogDate)) AND (dbo.Employment.JobTitle LIKE 'Unloader%')) " & _
            "set @PPH = (@PalRecd/@ttlHours) " & _
            "set @CPH = (@Pieces/@ttlHours) " & _
            "select @Location as Location,@FromLogDate as Logdate,@ToLogDate as Logdate1, @NumOfPOs as NumOfPOs, @NumOfLoads as NumOfLoads, @PalUnld as PalUnld, " & _
            "@Pieces as Pieces, @PalRecd as PalRecd, @Bad as Bad, @Resk as Resk, @ttlHours as 'TotalHours',@PPH as pph, @CPH as cph "

        dba.AddParameter("@FromLogDate", date1)
        dba.AddParameter("@ToLogDate", date2)
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