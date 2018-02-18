Imports Telerik.Web.UI
'Imports Telerik.Web.UI.GridExcelBuilder

Public Class PayrollSummary
    Inherits System.Web.UI.Page

    Private isConfigured As Boolean = False
    Private timeOut As Integer


    Private Sub PayrollSummary_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        timeOut = Server.ScriptTimeout
        Server.ScriptTimeout = 3600
        RadScriptManager1.AsyncPostBackTimeout = 3600
    End Sub

    Private Sub PayrollSummary_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        '        Server.ScriptTimeout = timeOut
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        RadScriptManager1.AsyncPostBackTimeout = 3600
        If Not Page.IsPostBack Then
            dpStartDate.SelectedDate = Date.Now()
            '            dpEndDate.SelectedDate = Date.Now()
            ' populate Locations Combo Box (cbLocations on aspx page)
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
            lblPageTitle.Text = "Payroll Summary Report"
            RadGrid1.Visible = False
            pnlTitle.Visible = True
            Dim tpdal As New TimePuncheDAL
            Dim sday As String = WeekdayName(Weekday(tpdal.getPayStartDate(Date.Now)))
            Dim eday As String = WeekdayName(Weekday(DateAdd(DateInterval.Day, 6, tpdal.getPayStartDate(Date.Now))))
            lblPayWeek.Text = sday & " thru " & eday
        End If
    End Sub

    Private Sub cbLocations_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        '       loadRadGrid()
        '        RadGrid1.Rebind()
    End Sub

    'Private Sub loadRadGrid()
    '    Dim locaID As Guid = New Guid(cbLocations.SelectedValue)
    '    Dim plst As New List(Of PayrollReportAdminObj)
    '    Dim praDAL As New PayrollUtilities
    '    Dim sdate As Date = dpStartDate.SelectedDate
    '    '        Dim edate As Date = dpEndDate.SelectedDate
    '    Dim a As String = ""
    '    RadGrid1.DataSource = plst
    'End Sub

    Dim plst1 As New List(Of PayrollReportAdminObj)
    Dim plst2 As New List(Of PayrollReportAdminObj)
    Dim plst3 As New List(Of PayrollReportAdminObj)


    Private Sub loadGrids()
        'retrieve location ID from location selector
        Dim loca As String = String.Empty

        If cbLocations.SelectedIndex = -1 Then
            lblerrloca.Visible = True
            Exit Sub
        Else
            loca = cbLocations.SelectedItem.Text.Trim()
        End If

        Dim dayz As Integer = 7

        Dim praDAL As New PayrollUtilities
        Dim tpdal As New TimePuncheDAL
        Dim startDate As Date = Nothing
        'get selected date
        Dim selectedDate As Date = dpStartDate.SelectedDate
        'determine start of payperiod
        startDate = tpdal.getPayStartDate(selectedDate)
        'the end date represents the stop point. Search shall match be < endDate
        Dim endDate As Date = DateAdd(DateInterval.Day, dayz, startDate)
        Dim startDate2 As Date = DateAdd(DateInterval.Day, 7, startDate)
        Dim enddate2 As Date = DateAdd(DateInterval.Day, 7, endDate)
        'go fetch list of payrollReportAdimObj
        pnlTitle.Visible = False

        plst1 = praDAL.getPayrollReportAdminObjList(loca, startDate, endDate)
        RadGrid1.DataSource = plst1
        RadGrid1.DataBind()
        RadGrid1.Visible = True

        'go fetch list of payrollReportAdimObj week 2
        If FormatDateTime(Date.Now, DateFormat.ShortDate) >= startDate2 Then
            plst2 = praDAL.getPayrollReportAdminObjList(loca, startDate2, endDate2)
        End If
        RadGrid2.DataSource = plst2
        RadGrid2.DataBind()
        RadGrid2.Visible = True

        'retrieve location ID from location selector
        'go fetch list of payrollReportAdimObj



        '************************* TO DO **************************************
        'Correct to run for all PayrollReportAdminObj in both lists
        '************************* TO DO **************************************
        For Each pr1 As PayrollReportAdminObj In plst1
            Dim eid As Guid = pr1.ID
            Dim pr2 As New PayrollReportAdminObj()
            Dim plist3 As New List(Of PayrollReportAdminObj)


            pr2 = plst2.Find(Function(p As PayrollReportAdminObj) p.ID = eid) ' find 1 in two
            If Not pr2 Is Nothing Then
                pr2.PercentHours += pr1.PercentHours
                pr2.ulAmount += pr1.ulAmount
                pr2.AddCompAmount += pr1.AddCompAmount
                pr2.HourlyHours += pr1.HourlyHours
                pr2.TotalHours += pr1.TotalHours
                pr2.OTHours += pr1.OTHours
                pr2.ulAmount += pr1.ulAmount
                pr2.HalfTimePay += pr1.HalfTimePay
                pr2.GrossPay += pr1.GrossPay
                pr2.RegularPay += pr1.RegularPay
                plst3.Add(pr2)
            ElseIf pr2 Is Nothing Then
                plst3.Add(pr1)
            Else
                plst3.Add(pr2)
            End If
        Next
        For Each pr2 As PayrollReportAdminObj In plst2
            Dim pr1 As New PayrollReportAdminObj()
            Dim eid As Guid = pr2.ID
            pr1 = plst1.Find(Function(p As PayrollReportAdminObj) p.ID = eid)
            If pr1 Is Nothing Then plst3.Add(pr2)
        Next
        RadGrid3.DataSource = plst3
        RadGrid3.DataBind()
        RadGrid3.Visible = True
        panelTabs.Visible = True
        '        btnSubmit.Enabled = True

        Dim tab1EndDate As Date = DateAdd(DateInterval.Day, -1, endDate)
        RadTabStrip1.Tabs(0).Text = Format(startDate, "MMM dd") & " thru " & Format(tab1EndDate, "MMM dd")
        Dim tab2EndDate As Date = DateAdd(DateInterval.Day, -1, enddate2)
        RadTabStrip1.Tabs(1).Text = Format(startDate2, "MMM dd") & " thru " & Format(tab2EndDate, "MMM dd")
        RadTabStrip1.Tabs(2).Text = Format(startDate, "MMM d, yyyy") & " thru " & Format(tab2EndDate, "MMM d, yyyy")


        '        Dim lblsdate As String = Format(startDate, "MMM dd")
        'this is dayz -1 so we can display: startDate thru endDate
        '        Dim lbledate As String = Format(DateAdd(DateInterval.Day, dayz - 1, startDate), "MMM dd")
        RadGrid1.ExportSettings.FileName = "WeeklyPayrollReport" & Format(startDate, "yymmdd")
        RadGrid4.DataBind()
        Dim dba As New DBAccess()
        Dim dt As DataTable = RadGrid4DataSource(startDate, enddate2)
        RadGrid4.DataSource = dt
        RadGrid4.Visible = True
        RadGrid4.DataBind()

    End Sub

    Private Sub RadGrid1_ItemDataBound1(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        'Is it a GridDataItem
        If (TypeOf (e.Item) Is GridDataItem) Then
            'Get the instance of the right type
            Dim databoundItem As GridDataItem = e.Item
            If databoundItem("PayRateHourly").Text = "$0.00" Then databoundItem("PayRateHourly").Text = "-"
            If databoundItem("PayRatePercentage").Text = "0" Then databoundItem("PayRatePercentage").Text = "-"
            If databoundItem("ulAmount").Text = "$0.00" Then databoundItem("ulAmount").Text = "-"
            If databoundItem("RegularPay").Text = "$0.00" Then databoundItem("RegularPay").Text = "-"
            If databoundItem("TotalHours").Text = "0" Then databoundItem("TotalHours").Text = "-"
            If databoundItem("OTHours").Text = "0" Then databoundItem("OTHours").Text = "-"
            If databoundItem("HalfTimePay").Text = "$0.00" Then databoundItem("HalfTimePay").Text = "-"
            If databoundItem("AddCompAmount").Text = "$0.00" Then databoundItem("AddCompAmount").Text = "-"
            If databoundItem("GrossPay").Text = "$0.00" Then databoundItem("GrossPay").Text = "-"

        End If

    End Sub
    Private Sub RadGrid2_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid2.ItemDataBound
        'Is it a GridDataItem
        If (TypeOf (e.Item) Is GridDataItem) Then
            'Get the instance of the right type
            Dim databoundItem As GridDataItem = e.Item
            If databoundItem("PayRateHourly").Text = "$0.00" Then databoundItem("PayRateHourly").Text = "-"
            If databoundItem("PayRatePercentage").Text = "0" Then databoundItem("PayRatePercentage").Text = "-"
            If databoundItem("ulAmount").Text = "$0.00" Then databoundItem("ulAmount").Text = "-"
            If databoundItem("TotalHours").Text = "0" Then databoundItem("TotalHours").Text = "-"
            If databoundItem("OTHours").Text = "0" Then databoundItem("OTHours").Text = "-"
            If databoundItem("HalfTimePay").Text = "$0.00" Then databoundItem("HalfTimePay").Text = "-"
            '            If databoundItem("OTPay").Text = "$0.00" Then databoundItem("OTPay").Text = "-"
            If databoundItem("HalfTimePay").Text = "$0.00" Then databoundItem("HalfTimePay").Text = "-"
            If databoundItem("AddCompAmount").Text = "$0.00" Then databoundItem("AddCompAmount").Text = "-"
            If databoundItem("GrossPay").Text = "$0.00" Then databoundItem("GrossPay").Text = "-"
        End If
    End Sub
    Private Sub RadGrid3_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid3.ItemDataBound
        'Is it a GridDataItem
        If (TypeOf (e.Item) Is GridDataItem) Then
            'Get the instance of the right type
            Dim databoundItem As GridDataItem = e.Item
            If databoundItem("PayRateHourly").Text = "$0.00" Then databoundItem("PayRateHourly").Text = "-"
            If databoundItem("PayRatePercentage").Text = "0" Then databoundItem("PayRatePercentage").Text = "-"
            If databoundItem("ulAmount").Text = "$0.00" Then databoundItem("ulAmount").Text = "-"
            If databoundItem("TotalHours").Text = "$0.00" Then databoundItem("TotalHours").Text = "-"
            If databoundItem("OTHours").Text = "$0.00" Then databoundItem("OTHours").Text = "-"
            If databoundItem("HalfTimePay").Text = "$0.00" Then databoundItem("HalfTimePay").Text = "-"
            If databoundItem("TotalHours").Text = "0" Then databoundItem("TotalHours").Text = "-"
            If databoundItem("OTHours").Text = "0" Then databoundItem("OTHours").Text = "-"
            '            If databoundItem("OTPay").Text = "$0.00" Then databoundItem("OTPay").Text = "-"
            '            If databoundItem("LoadPay").Text = "$0.00" Then databoundItem("LoadPay").Text = "-"
            If databoundItem("HalfTimePay").Text = "$0.00" Then databoundItem("HalfTimePay").Text = "-"
            If databoundItem("AddCompAmount").Text = "$0.00" Then databoundItem("AddCompAmount").Text = "-"
            If databoundItem("GrossPay").Text = "$0.00" Then databoundItem("GrossPay").Text = "-"
        End If
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        loadGrids()
    End Sub


    Private Sub RadGrid1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If e.CommandName = RadGrid.ExportToExcelCommandName OrElse e.CommandName = RadGrid.ExportToWordCommandName OrElse e.CommandName = RadGrid.ExportToCsvCommandName Then
            ConfigureExport()
        End If
    End Sub

#Region "Exporting"

    'Protected Sub RadGrid1_ExcelMLExportRowCreated(ByVal source As Object, ByVal e As GridExportExcelMLRowCreatedArgs)
    '    If e.RowType = GridExportExcelMLRowType.DataRow Then
    '        'Add custom styles to the desired cells
    '        Dim cell As CellElement = e.Row.Cells.GetCellByName("UnitPrice")
    '        cell.StyleValue = IIf(cell.StyleValue = "itemStyle", "priceItemStyle", "alternatingPriceItemStyle")

    '        cell = e.Row.Cells.GetCellByName("ExtendedPrice")
    '        cell.StyleValue = IIf(cell.StyleValue = "itemStyle", "priceItemStyle", "alternatingPriceItemStyle")

    '        cell = e.Row.Cells.GetCellByName("Discount")
    '        cell.StyleValue = IIf(cell.StyleValue = "itemStyle", "percentItemStyle", "alternatingPercentItemStyle")

    '        If Not isConfigured Then
    '            'Set Worksheet name
    '            e.Worksheet.Name = "Weekly Payroll Summary"

    '            'Set Column widths
    '            For Each column As ColumnElement In e.Worksheet.Table.Columns
    '                If e.Worksheet.Table.Columns.IndexOf(column) = 2 Then
    '                    column.Width = Unit.Point(180)
    '                Else
    '                    'set width 180 to ProductName column
    '                    column.Width = Unit.Point(80)
    '                    'set width 80 to the rest of the columns
    '                End If
    '            Next

    '            'Set Page options
    '            Dim pageSetup As PageSetupElement = e.Worksheet.WorksheetOptions.PageSetup
    '            pageSetup.PageLayoutElement.IsCenteredVertical = True
    '            pageSetup.PageLayoutElement.IsCenteredHorizontal = True
    '            pageSetup.PageMarginsElement.Left = 0.5
    '            pageSetup.PageMarginsElement.Top = 0.5
    '            pageSetup.PageMarginsElement.Right = 0.5
    '            pageSetup.PageMarginsElement.Bottom = 0.5
    '            pageSetup.PageLayoutElement.PageOrientation = PageOrientationType.Landscape

    '            'Freeze panes
    '            e.Worksheet.WorksheetOptions.AllowFreezePanes = True
    '            e.Worksheet.WorksheetOptions.LeftColumnRightPaneNumber = 1
    '            e.Worksheet.WorksheetOptions.TopRowBottomPaneNumber = 1
    '            e.Worksheet.WorksheetOptions.SplitHorizontalOffset = 1
    '            e.Worksheet.WorksheetOptions.SplitVerticalOffest = 1

    '            e.Worksheet.WorksheetOptions.ActivePane = 2
    '            isConfigured = True
    '        End If
    '    End If
    'End Sub

    'Protected Sub RadGrid1_ExcelMLExportStylesCreated(ByVal source As Object, ByVal e As GridExportExcelMLStyleCreatedArgs)
    '    'Add currency and percent styles
    '    Dim priceStyle As New StyleElement("priceItemStyle")
    '    priceStyle.NumberFormat.FormatType = NumberFormatType.Currency
    '    priceStyle.FontStyle.Color = System.Drawing.Color.Red
    '    e.Styles.Add(priceStyle)

    '    Dim alternatingPriceStyle As New StyleElement("alternatingPriceItemStyle")
    '    alternatingPriceStyle.NumberFormat.FormatType = NumberFormatType.Currency
    '    alternatingPriceStyle.FontStyle.Color = System.Drawing.Color.Red
    '    e.Styles.Add(alternatingPriceStyle)

    '    Dim percentStyle As New StyleElement("percentItemStyle")
    '    percentStyle.NumberFormat.FormatType = NumberFormatType.Percent
    '    percentStyle.FontStyle.Italic = True
    '    e.Styles.Add(percentStyle)

    '    Dim alternatingPercentStyle As New StyleElement("alternatingPercentItemStyle")
    '    alternatingPercentStyle.NumberFormat.FormatType = NumberFormatType.Percent
    '    alternatingPercentStyle.FontStyle.Italic = True
    '    e.Styles.Add(alternatingPercentStyle)

    '    'Apply background colors 
    '    For Each style As StyleElement In e.Styles
    '        If style.Id = "headerStyle" Then
    '            style.InteriorStyle.Pattern = InteriorPatternType.Solid
    '            style.InteriorStyle.Color = System.Drawing.Color.Gray
    '        End If
    '        If style.Id = "alternatingItemStyle" OrElse style.Id = "alternatingPriceItemStyle" OrElse style.Id = "alternatingPercentItemStyle" OrElse style.Id = "alternatingDateItemStyle" Then
    '            style.InteriorStyle.Pattern = InteriorPatternType.Solid
    '            style.InteriorStyle.Color = System.Drawing.Color.LightGray
    '        End If
    '        If style.Id.Contains("itemStyle") OrElse style.Id = "priceItemStyle" OrElse style.Id = "percentItemStyle" OrElse style.Id = "dateItemStyle" Then
    '            style.InteriorStyle.Pattern = InteriorPatternType.Solid
    '            style.InteriorStyle.Color = System.Drawing.Color.White
    '        End If
    '    Next

    '    Dim empList As New List(Of DivLogEmployee)

    '    Dim newHRemp As DivLogEmployee = New DivLogEmployee

    '    newHRemp.FirstName = "  False"
    '    newHRemp.DOB = "1/1/1111"

    '    empList.Add(newHRemp)

    '    For Each emp As DivLogEmployee In empList
    '        Dim efn As String = emp.FirstName
    '        Dim eln As String = emp.LastName
    '        Dim ehicn As String = emp.Health.InsCompanyName
    '        Dim ehse As String = emp.Health.somethingelse



    '    Next

    'End Sub


    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        RadGrid1.Page.Response.ClearHeaders()
        RadGrid1.Page.Response.Cache.SetCacheability(HttpCacheability.[Private])
        RadGrid1.ExportSettings.ExportOnlyData = True
        RadGrid1.ExportSettings.IgnorePaging = False
        RadGrid1.ExportSettings.OpenInNewWindow = True
        RadGrid1.MasterTableView.ExportToExcel()
    End Sub

    Public Sub ConfigureExport()
        RadGrid1.ExportSettings.ExportOnlyData = True
        RadGrid1.ExportSettings.IgnorePaging = True
        RadGrid1.ExportSettings.OpenInNewWindow = True
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


    Private Function RadGrid4DataSource(ByVal startdate As Date, ByVal enddate2 As Date) As DataTable
        Dim dba As New DBAccess

        dba.CommandText = "DECLARE @tblPayrollEmployees TABLE (ID uniqueidentifier) " & _
"INSERT INTO @tblPayrollEmployees (ID) " & _
"SELECT dbo.TimePunche.EmployeeID   FROM     dbo.Location INNER JOIN dbo.TimePunche ON dbo.Location.ID = dbo.TimePunche.LocationID INNER JOIN dbo.TimeInOut  ON dbo.TimePunche.ID = dbo.TimeInOut.TimepuncheID " & _
"WHERE (dbo.TimePunche.DateWorked >= @startdate1)   AND     (dbo.TimePunche.DateWorked < @enddate1)   AND     (dbo.Location.Name = @location)GROUP BY   dbo.TimePunche.EmployeeID ORDER BY   dbo.TimePunche.EmployeeID " & _
"DECLARE @tblEmployeeRates TABLE (ID uniqueidentifier,EmpName varchar(50),EmpNumber varchar(50))       " & _
"INSERT INTO @tblEmployeeRates " & _
"SELECT dbo.Employee.ID, dbo.Employee.LastName + '  ' + dbo.Employee.FirstName AS EmpName, dbo.Employee.Login AS EmpNum FROM   dbo.Employee LEFT OUTER JOIN dbo.Employment ON dbo.Employee.ID = dbo.Employment.EmployeeID " & _
"WHERE (NOT (dbo.Employee.FirstName + '  ' + dbo.Employee.LastName + '  ' + dbo.Employee.Login LIKE 'Truck' + '%')) AND (dbo.Employment.DateOfEmployment <= @startdate1) AND (dbo.Employment.DateOfDismiss >= @enddate1) OR " & _
"(NOT (dbo.Employee.FirstName + '  ' + dbo.Employee.LastName + '  ' + dbo.Employee.Login LIKE 'Truck' + '%')) AND (dbo.Employment.DateOfEmployment >= @startdate1) AND (@enddate1 BETWEEN dbo.Employment.DateOfEmployment AND dbo.Employment.DateOfDismiss) " & _
"ORDER BY dbo.Employee.ID " & _
"DECLARE @tbladditionalcomp TABLE (EmployeeID uniqueidentifier,AddCompAmount decimal(8,2),CompDesc varchar(50),AddCompComments varchar(MAX),AddCompStartDate datetime,AddCompEndDate datetime) " & _
"INSERT INTO @tbladditionalcomp " & _
"SELECT TOP (100) PERCENT dbo.AdditionalComp.EmployeeID,CASE WHEN Credit = 1 THEN AddCompAmount ELSE AddCompAmount - AddCompAmount - AddCompAmount END AS Amount, dbo.AddCompDesc.CompDesc, dbo.AdditionalComp.AddCompComments, dbo.AdditionalComp.AddCompStartDate,  " & _
"dbo.AdditionalComp.AddCompEndDate FROM dbo.AdditionalComp INNER JOIN dbo.AddCompDesc ON dbo.AdditionalComp.AddCompDescriptionID = dbo.AddCompDesc.AddCompDescriptionID " & _
"WHERE (dbo.AdditionalComp.AddCompStartDate >= @startdate1) AND (dbo.AdditionalComp.AddCompEndDate < @enddate1) ORDER BY dbo.AdditionalComp.EmployeeID, dbo.AddCompDesc.CompDesc " & _
"SELECT " & _
"EmpName,  " & _
"EmpNumber, " & _
"CASE " & _
"WHEN AddCompAmount>0 " & _
"THEN AddCompAmount " & _
"ELSE 0.00 " & _
"END AS AddCompAmount, " & _
"CASE " & _
"WHEN tbladditionalcomp.CompDesc IS NULL " & _
"THEN ' - - - ' " & _
"ELSE CompDesc " & _
"END AS CompDescription, " & _
"AddCompComments, " & _
"AddCompStartDate, " & _
"AddCompEndDate " & _
"FROM @tblPayrollEmployees AS tblPayrollEmployees  " & _
"LEFT OUTER JOIN @tblEmployeeRates AS tblEmployeeRates ON tblEmployeeRates.ID = tblPayrollEmployees.ID " & _
"LEFT OUTER JOIN @tbladditionalcomp AS tbladditionalcomp ON tbladditionalcomp.EmployeeID = tblPayrollEmployees.ID " & _
"WHERE AddCompAmount <> 0 " & _
"ORDER BY  EmpNumber "
        dba.AddParameter("@startdate1", startDate)
        dba.AddParameter("@enddate1", enddate2)
        dba.AddParameter("@location", cbLocations.SelectedItem.Text)
        Dim dt As New DataTable
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception

        End Try
        Return dt
    End Function
End Class