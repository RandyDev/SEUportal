Imports Telerik.Web.UI
Public Class DailyWeekly
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
            'dpStartDate.SelectedDate = FormatDateTime(DateAdd(DateInterval.Day, -1, Date.Now), DateFormat.ShortDate)
            Dim mo As Integer = Month(Date.Now)
            Dim yr As Integer = Year(Date.Now)
            'Dim sdt As Date = "1/" & mo.ToString & "/" & yr.ToString
            dpStartDate.SelectedDate = FormatDateTime(DateAdd(DateInterval.Day, -1, Date.Now), DateFormat.ShortDate)
            ' Dim nm As Date = DateAdd(DateInterval.Day, 1, sdt)
            '            dpEndDate.SelectedDate = DateAdd(DateInterval.Day, -1, nm)
            If Request.QueryString("startDate") > "" Then dpStartDate.SelectedDate = Request.QueryString("startDate")
            If Request.QueryString("endDate") > "" Then dpEndDate.SelectedDate = Request.QueryString("endDate")
            If Request.QueryString("locaID") > "" Then cbLocations.SelectedValue = Request.QueryString("locaID")
            RadGrid1.Visible = False

        End If

    End Sub


    Private Sub btnShowRecords_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnShowRecords.Command
        RadGrid1.Visible = True
        RadGrid1.Rebind()
    End Sub

    Protected Sub RadAjaxManager1_AjaxSettingCreating(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxSettingCreatingEventArgs) Handles RadAjaxManager1.AjaxSettingCreating

    End Sub

#Region "Exporting"
    Public Sub ConfigureExport()
        RadGrid1.ExportSettings.ExportOnlyData = True
        RadGrid1.MasterTableView.UseAllDataFields = True
        RadGrid1.ExportSettings.IgnorePaging = True
        RadGrid1.ExportSettings.FileName = "DailyWeeklyReport"
        RadGrid1.ExportSettings.OpenInNewWindow = False
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

    Private Sub RadGrid1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If e.CommandName = Telerik.Web.UI.RadGrid.ExportToExcelCommandName OrElse e.CommandName = Telerik.Web.UI.RadGrid.ExportToWordCommandName OrElse e.CommandName = Telerik.Web.UI.RadGrid.ExportToCsvCommandName Then
            ConfigureExport()
        End If
        If e.CommandName = Telerik.Web.UI.RadGrid.ExportToExcelCommandName Then
            RadGrid1.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML
        End If
    End Sub

    Private Sub RadGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If TypeOf e.Item Is GridFooterItem Then
            Try
                Dim footerItem As GridFooterItem = CType(e.Item, GridFooterItem)
                Dim ttlHours As Decimal = CType(footerItem("HoursWorked").Text, Decimal)
                Dim ttlPcs As Integer = CType(footerItem("Pieces").Text, Integer)
                Dim ttlPal As Integer = CType(footerItem("PalRecd").Text, Integer) ' or PalUnld

                Dim palHour As Decimal = Math.Round(ttlPal / ttlHours, 2)
                Dim pcsHour As Decimal = Math.Round(ttlPcs / ttlHours, 2)
                footerItem("PALph").Text = palHour.ToString
                footerItem("PIEph").Text = pcsHour.ToString

            Catch ex As Exception

            End Try

        End If

    End Sub


    Private Sub RadGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        If cbLocations.SelectedIndex > -1 Then
            Dim startdate As Date = dpStartDate.SelectedDate
            Dim enddate As Date = dpEndDate.SelectedDate
            Dim location As String = cbLocations.SelectedItem.Text
            Dim dba As New DBAccess()
            dba.CommandText = "DECLARE @tblHours TABLE (EmployeeID uniqueidentifier NULL,HoursWorked Decimal(8,2) NULL,DateWorked DateTime NULL,IsHourly Bit NULL) " & _
                "INSERT INTO @tblHours (EmployeeID,DateWorked,HoursWorked,IsHourly) " & _
                "SELECT   dbo.TimePunche.EmployeeID, dbo.TimePunche.DateWorked, SUM(dbo.TimeInOut.HoursWorked), dbo.TimeInOut.IsHourly " & _
                "FROM     dbo.TimePunche INNER JOIN dbo.TimeInOut ON dbo.TimePunche.ID = dbo.TimeInOut.TimepuncheID INNER JOIN dbo.Location ON dbo.TimePunche.LocationID = dbo.Location.ID " & _
                "WHERE   (dbo.Location.Name = @location) AND (dbo.TimePunche.DateWorked <= DATEADD(n, 1439, @enddate)) " & _
                "GROUP BY dbo.TimePunche.EmployeeID, dbo.TimeInOut.IsHourly, dbo.TimePunche.DateWorked " & _
                "HAVING  (dbo.TimePunche.DateWorked >= @startdate) " & _
                "ORDER BY dbo.TimePunche.EmployeeID " & _
                "DECLARE @EmpInfo  TABLE (EmployeeID uniqueidentifier NULL,JobTitle varchar(50) NULL,HomeLocation varchar(50)NULL,FirstName varchar(50)NULL, LastName varchar(50)NULL) " & _
                "INSERT INTO @EmpInfo (EmployeeID,JobTitle,HomeLocation,FirstName,LastName) " & _
                "SELECT   dbo.TimePunche.EmployeeID, dbo.Employment.JobTitle, Location_1.Name AS HomeLocation, dbo.Employee.FirstName, dbo.Employee.LastName " & _
                "FROM     dbo.Employment INNER JOIN " & _
                "dbo.Employee ON dbo.Employment.EmployeeID = dbo.Employee.ID INNER JOIN " & _
                "dbo.Location AS Location_1 ON dbo.Employee.HomeLocationID = Location_1.ID RIGHT OUTER JOIN " & _
                "dbo.TimePunche INNER JOIN " & _
                "dbo.Location ON dbo.TimePunche.LocationID = dbo.Location.ID ON dbo.Employee.ID = dbo.TimePunche.EmployeeID " & _
"WHERE (dbo.Employment.DateOfEmployment <= @startdate) AND (dbo.Employment.DateOfDismiss >= @enddate) AND  " & _
"(NOT (dbo.Employee.FirstName + '  ' + dbo.Employee.LastName + '  ' + dbo.Employee.Login LIKE 'Truck' + '%')) OR " & _
"(dbo.Employment.DateOfEmployment >= @startdate) AND  " & _
"(NOT (dbo.Employee.FirstName + '  ' + dbo.Employee.LastName + '  ' + dbo.Employee.Login LIKE 'Truck' + '%')) AND (@enddate BETWEEN  " & _
"dbo.Employment.DateOfEmployment AND dbo.Employment.DateOfDismiss) OR " & _
"(dbo.Employment.DateOfEmployment <= @startdate) AND  " & _
"(NOT (dbo.Employee.FirstName + '  ' + dbo.Employee.LastName + '  ' + dbo.Employee.Login LIKE 'Truck' + '%')) AND (@startdate BETWEEN  " & _
"dbo.Employment.DateOfEmployment AND dbo.Employment.DateOfDismiss) " & _
                "GROUP BY dbo.TimePunche.EmployeeID, dbo.Employment.JobTitle, Location_1.Name, dbo.Employee.FirstName, dbo.Employee.LastName " & _
                "HAVING      (dbo.Employee.FirstName <> 'Truck') AND (dbo.Employment.JobTitle LIKE 'Unload' + '%') " & _
                "ORDER BY dbo.TimePunche.EmployeeID               " & _
                "DECLARE @CombInfo TABLE (EmployeeID uniqueidentifier NULL, HoursWorked Decimal(8,2)NULL,DateWorked DateTime NULL, IsHourly Bit NULL, JobTitle varchar(50)NULL,HomeLocation varchar(50)NULL, FirstName varchar(50) NULL, LastName varchar(50)NULL) " & _
                "INSERT INTO @CombInfo (EmployeeID,DateWorked,HoursWorked,IsHourly,JobTitle,HomeLocation,FirstName,LastName) " & _
                "SELECT tblHours.EmployeeID, DateWorked,HoursWorked, IsHourly, JobTitle, HomeLocation, FirstName, LastName " & _
                "FROM  @tblHours AS tblHours RIGHT OUTER JOIN @EmpInfo AS EmpInfo ON (tblHours.EmployeeID = EmpInfo.EmployeeID) " & _
                "DECLARE @dateTime TABLE (DateWorked DateTime NULL,HoursWorked Decimal(8,2) NULL) " & _
                "INSERT INTO @dateTime(DateWorked, HoursWorked)" & _
                "SELECT DateWorked,SUM(HoursWorked)AS HoursWorked FROM @CombInfo WHERE JobTitle LIKE 'Unload' + '%'GROUP BY DateWorked ORDER BY DateWorked " & _
                "DECLARE @tblLoads TABLE (Location varchar(50)NULL,WorkDate DateTime NULL,NumOfPOs BigInt NULL,NumOfLoads BigInt NULL,PalUnld BigInt NULL,Pieces BigInt NULL,PalRecd BigInt NULL,Bad BigInt NULL,Resk BigInt NULL) " & _
                "INSERT INTO @tblLoads(Location,WorkDate,NumOfPOs,NumOfLoads,PalUnld,Pieces,PalRecd,Bad,Resk)" & _
                "SELECT dbo.Location.Name AS Location, CONVERT(varchar(10), dbo.WorkOrder.LogDate, 110) AS WorkDate, COUNT(DISTINCT dbo.WorkOrder.PurchaseOrder) AS NumOfPOs, COUNT(DISTINCT CONVERT(varchar(10), dbo.WorkOrder.LogDate, 110) + dbo.LoadType.Name + dbo.WorkOrder.DoorNumber + dbo.WorkOrder.TrailerNumber) AS NumOfLoads,  " & _
                "SUM(dbo.WorkOrder.PalletsUnloaded) AS PalUnld, SUM(dbo.WorkOrder.Pieces) AS Pieces, SUM(dbo.WorkOrder.PalletsReceived) AS PalRecd, SUM(dbo.WorkOrder.BadPallets) AS Bad, SUM(dbo.WorkOrder.Restacks) AS Resk " & _
                "FROM     dbo.Location INNER JOIN dbo.WorkOrder ON dbo.Location.ID = dbo.WorkOrder.LocationID INNER JOIN dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID " & _
                "WHERE   (dbo.Location.Name = @location) AND (dbo.WorkOrder.DockTime >= @startdate) AND (dbo.WorkOrder.DockTime <= DATEADD(n, 1439, @enddate)) " & _
                "GROUP BY dbo.Location.Name, CONVERT(varchar(10), dbo.WorkOrder.LogDate, 110) " & _
                "SELECT Location,WorkDate,NumOfPOs,NumOfLoads,PalUnld,Pieces,PalRecd,Bad,Resk,HoursWorked, CONVERT(DECIMAL(10,2),(PalRecd/HoursWorked)) AS PALph, CONVERT(DECIMAL(10,2),(Pieces/HoursWorked)) AS PIEph " & _
                "FROM @tblLoads AS tblLoads INNER JOIN @dateTime AS dateTime ON (tblLoads.WorkDate = dateTime.DateWorked) ORDER BY WorkDate"
            dba.AddParameter("@startdate", startdate)
            dba.AddParameter("@enddate", enddate)
            dba.AddParameter("@location", location)
            Dim dt As New DataTable
            Try
                dt = dba.ExecuteDataSet.Tables(0)
            Catch ex As Exception

            End Try

            RadGrid1.DataSource = dt

        End If
    End Sub


#End Region

End Class