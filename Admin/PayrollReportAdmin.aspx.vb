Imports Telerik.Web.UI
'Imports DiversifiedLogistics.PayrollUtilities


Public Class PayrollReportAdmin
    Inherits System.Web.UI.Page

    Private isConfigured As Boolean = False
 
    Private timeOut As Integer


    Private Sub PayrollReportAdmin(sender As Object, e As System.EventArgs) Handles Me.Init
        timeOut = Server.ScriptTimeout
        Server.ScriptTimeout = 3600
        RadScriptManager1.AsyncPostBackTimeout = 3600
    End Sub

    Private Sub PayrollSummary_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        '        Server.ScriptTimeout = timeOut
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            dpStartDate.SelectedDate = Date.Now()
            '            dpEndDate.SelectedDate = Date.Now()
            ' populate Locations Combo Box (cbLocations on aspx page)
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
            lblPageTitle.Text = "Weekly Payroll Summary Report"
            RadGrid1.Visible = False
            pnlTitle.Visible = True
            Dim tpdal As New TimePuncheDAL
            Dim sday As String = WeekdayName(Weekday(tpdal.getPayStartDate(Date.Now)))
            Dim eday As String = WeekdayName(Weekday(DateAdd(DateInterval.Day, 6, tpdal.getPayStartDate(Date.Now))))
            lblPayWeek.Text = sday & " thru " & eday
        End If
    End Sub

    Private Sub cbLocations_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        If RadGrid1.Visible Then
            Dim dt As New DataTable
            RadGrid1.DataSource = dt
            RadGrid1.DataBind()
        End If
        '        loadRadGrid()
        '        RadGrid1.Rebind()
    End Sub



    Private Sub RadGrid1_ItemDataBound1(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        'Is it a GridDataItem
        If (TypeOf (e.Item) Is GridDataItem) Then
            'Get the instance of the right type
            Dim databoundItem As GridDataItem = e.Item
            '            Dim LockedOut As String = databoundItem("IsLockedOut").Text
            If databoundItem("HourlyRate").Text = "$0.00" Then databoundItem("HourlyRate").Text = "-"
            If databoundItem("PercentageRate").Text = "0" Then databoundItem("PercentageRate").Text = "-"
            If databoundItem("Business").Text = "$0.00" Then databoundItem("Business").Text = "-"
            If databoundItem("RegHoursPay").Text = "$0.00" Then databoundItem("RegHoursPay").Text = "-"

            If databoundItem("specialpay").Text = "$0.00" Then databoundItem("specialpay").Text = "-"

            If databoundItem("holidaypay").Text = "$0.00" Then databoundItem("holidaypay").Text = "-"
            If databoundItem("TotalHours").Text = "0" Then databoundItem("TotalHours").Text = "-"
            If databoundItem("OTHours").Text = "0" Then databoundItem("OTHours").Text = "-"
            If databoundItem("LoadPay").Text = "$0.00" Then databoundItem("LoadPay").Text = "-"
            If databoundItem("OTPay").Text = "$0.00" Then databoundItem("OTPay").Text = "-"
            If databoundItem("LoadPay").Text = "$0.00" Then databoundItem("LoadPay").Text = "-"
            If databoundItem("HalfTimePay").Text = "$0.00" Then databoundItem("HalfTimePay").Text = "-"
            If databoundItem("PercentageOTPay").Text = "$0.00" Then databoundItem("PercentageOTPay").Text = "-"
            If databoundItem("GrossPay").Text = "$0.00" Then databoundItem("GrossPay").Text = "-"

        End If

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


    Private Sub RadGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        'retrieve location ID from location selector
        Dim loca As String = cbLocations.SelectedItem.Text
        Dim dayz As Integer = 7

        Dim plst As New List(Of PayrollReportAdminObj)
        Dim praDAL As New PayrollUtilities
        Dim tpdal As New TimePuncheDAL
        Dim startDate As Date = Nothing
        'get selected date
        Dim selectedDate As Date = dpStartDate.SelectedDate
        'determine start of payperiod
        Dim payPeriodStartDate As Date = tpdal.getPayStartDate(selectedDate)
        Dim dd As Long = DateDiff(DateInterval.Day, selectedDate, payPeriodStartDate)
        Dim ddd As Long = DateDiff(DateInterval.Day, payPeriodStartDate, selectedDate)
        If DateDiff(DateInterval.Day, payPeriodStartDate, selectedDate) > dayz - 1 Then
            startDate = DateAdd(DateInterval.Day, dayz, payPeriodStartDate) 'set startdate to beginning of week 2
        Else
            startDate = payPeriodStartDate
        End If
        'the end date represents the stop point. Search shall match be < endDate
        Dim endDate As Date = DateAdd(DateInterval.Day, dayz, startDate)
        'go fetch list of payrollReportAdimObj
        plst = praDAL.getPayrollReportAdminObjList(loca, startDate, endDate)
        pnlTitle.Visible = False
        RadGrid1.Visible = True
        RadGrid1.DataSource = plst

                Dim lblsdate As String = Format(startDate, "MMM dd")
                'this is dayz -1 so we can display: startDate thru endDate
                Dim lbledate As String = Format(DateAdd(DateInterval.Day, dayz - 1, startDate), "MMM dd")
                If plst.Count > 0 Then
                    lblspayPeriod.Visible = True
                    lblspayPeriod.Text = "Showing " & lblsdate & Utilities.getIntegerSuperScript(Day(startDate)) & " thru " & lbledate & Utilities.getIntegerSuperScript(Day(DateAdd(DateInterval.Day, dayz - 1, startDate)))
                End If
        RadGrid1.ExportSettings.FileName = "WeeklyPayrollReport" & Format(startDate, "yymmdd")
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        RadGrid1.Visible = True
        RadGrid1.Rebind()
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




End Class