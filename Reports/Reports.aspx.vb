'Imports Telerik.Web.UI
Imports Telerik.Reporting
'Imports SEUreports

Partial Class Reports
    Inherits System.Web.UI.Page
#Region "Set Parameters"
    Private Sub SetControls()
        Dim dtToday As DateTime = FormatDateTime(Date.Now(), DateFormat.ShortDate)
        Dim lnkString As String = String.Empty

        Select Case Request.QueryString("report")
            ' add report to user menus w/ this link: ~/Reports/Reports.aspx?report=ReportName
            '       WHERE ReportName is the report name with NO SPACES
            ' ************* do this for EACH report *************
            Case "POsearch"
                '***** set endDate label and date picker date
                lblEndDate.Visible = False   'endDate label visibility
                '                lblEndDate.Text = "Custom text for this label"
                'dpEndDate.SelectedDate = dtToday    'set value for EndDate
                dpEndDate.Visible = False    'endDate date picker visibility
                '----->>>>> StartDate <<<<<-----
                '*****set startDate label and date picker date
                lblStartDate.Visible = False 'startDate label visibility
                '   lblStartDate.Text = "Custom text for this label"
                'dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -7, dtToday)   'example next line
                '   **example: 7 days ago use:   DateAdd(DateInterval.Day, -7, dtToday)
                dpStartDate.Visible = False  'startDate date picker visibility
                '----->>>>> Location(s) <<<<<-----
                '*****set locations label and comboBox visibility
                lblLocation.Visible = True  'location label visibility
                '   lblLocation.Text = "Custom text for this label"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                '*****set departments label and comboBox visibility
                lblDepartment.Visible = False
                '   lblDepartment.Text = "Custom text for this label"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                '*****set vendor label and textbox visibility
                lblVendorNumber.Visible = True  'vendorNumber visibility
                lblVendorNumber.Text = "PO Number<br />"
                txtVendorNumber.Visible = True  'vendorNumber TextBox visibility
                '----->>>>> Report Name <<<<<-----
                '*****show report name to user 
                lblReportName.Text = "PO Search"
                lbSwitch.Visible = False
            Case "VendorReport"    'must match ReportName
                '----->>>>> EndDate <<<<<-----
                '***** set endDate label and date picker date
                lblEndDate.Visible = True   'endDate label visibility
                '                lblEndDate.Text = "Custom text for this label"
                dpEndDate.SelectedDate = dtToday    'set value for EndDate
                dpEndDate.Visible = True    'endDate date picker visibility
                '----->>>>> StartDate <<<<<-----
                '*****set startDate label and date picker date
                lblStartDate.Visible = True 'startDate label visibility
                '   lblStartDate.Text = "Custom text for this label"
                dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -6, dtToday)   'example next line
                '   **example: 7 days ago use:   DateAdd(DateInterval.Day, -7, dtToday)
                dpStartDate.Visible = True  'startDate date picker visibility
                '----->>>>> Location(s) <<<<<-----
                '*****set locations label and comboBox visibility
                lblLocation.Visible = True  'location label visibility
                '   lblLocation.Text = "Custom text for this label"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                '*****set departments label and comboBox visibility
                lblDepartment.Visible = False
                '   lblDepartment.Text = "Custom text for this label"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                '*****set vendor label and textbox visibility
                lblVendorNumber.Visible = True  'vendorNumber visibility
                '   lblVendorNumber.Text = "Custom text for this label"
                txtVendorNumber.Visible = True  'vendorNumber TextBox visibility
                '----->>>>> Report Name <<<<<-----
                '*****show report name to user 
                lblReportName.Text = "Vendor Report"
                lbSwitch.Visible = False
            Case "AdditionalCosts"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = dtToday
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -6, dtToday)
                dpStartDate.Visible = True
                '----->>>>> Location(s) <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = False
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = False
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = False
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "Additional Costs Report"
                lbSwitch.CommandName = "~/ClientSvcs/AdditionalCost.aspx"
            Case "AverageCost"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = dtToday
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -6, dtToday)
                dpStartDate.Visible = True
                '----->>>>> Location(s) <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = False
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = True
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = True
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "Average Cost Report"
                lbSwitch.CommandName = "~/ClientSvcs/AverageCost.aspx"
            Case "BadPalletReport"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = dtToday
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -6, dtToday)
                dpStartDate.Visible = True
                '----->>>>> Location(s) <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = False
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = True
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = True
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "Bad Pallet Report"
                lbSwitch.CommandName = "~/ClientSvcs/BadPallets.aspx"
            Case "LoadsDetailed"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = dtToday
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -6, dtToday)
                dpStartDate.Visible = True
                '----->>>>> Location(s) <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = False
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = True
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = True
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "Loads Detailed"
                lbSwitch.CommandName = "~/ClientSvcs/LoadsDetails.aspx"
            Case "VendorSummary"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = dtToday
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -6, dtToday)
                dpStartDate.Visible = True
                '----->>>>> Location(s) <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = False
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = True
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = True
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "Vendor Summary Report"
                lbSwitch.Visible = False
            Case "BusinessSummary"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = dtToday
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -6, dtToday)
                dpStartDate.Visible = True
                '----->>>>> Location <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = True
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = True
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = False
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = False
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "Business Summary"
                lbSwitch.CommandName = "~/ClientSvcs/BusinessSummary.aspx"
            Case "BusinessSummaryNet"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = dtToday
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -6, dtToday)
                dpStartDate.Visible = True
                '----->>>>> Location <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = True
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = True
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = False
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = False
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "Business Summary Net"
                lbSwitch.CommandName = "~/ClientSvcs/BusinessSummaryNet.aspx"
                lbSwitch.Visible = False
            Case "BusinessSummaryCashBreakdown"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = dtToday
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -1, dtToday)
                dpStartDate.Visible = True
                '----->>>>> Location <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = False
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = False
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = False
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = ""
                lbSwitch.CommandName = ""
            Case "DailyWeekly"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = dtToday
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -6, dtToday)
                dpStartDate.Visible = True
                '----->>>>> Location <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = False
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = False
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = False
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "Daily Weekly Report"
                lbSwitch.Visible = False
            Case "DriverWorked"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = dtToday
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -6, dtToday)
                dpStartDate.Visible = True
                '----->>>>> Location <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = False
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = False
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = False
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "Driver Worked Loads"
                lbSwitch.Visible = False
            Case "LoadsOverTwoHours"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = DateAdd(DateInterval.Day, -1, dtToday)
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = dpEndDate.SelectedDate
                dpStartDate.Visible = True
                '----->>>>> Location <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = False
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = False
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = False
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "Loads Over 2 Hours"
                'lbSwitch.CommandName = "~/ClientSvcs/LoadsOverTwo.aspx"
                lbSwitch.Visible = False
            Case "LoadsLessThanTenMin"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = DateAdd(DateInterval.Day, -1, dtToday)
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = dpEndDate.SelectedDate
                dpStartDate.Visible = True
                '----->>>>> Location <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = False
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = False
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = False
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "Loads Less Than Ten Mins."
                lbSwitch.CommandName = ""
                lbSwitch.Visible = False
            Case "OnTimeDelivery"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = dtToday
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -6, dtToday)
                dpStartDate.Visible = True
                '----->>>>> Location <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = True
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = True
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = False
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = False
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "On Time Delivery"
                lbSwitch.CommandName = "~/ClientSvcs/OnTimeDelivery.aspx"
                lbSwitch.Visible = False
            Case "LoadsOverTwoHoursLate"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = DateAdd(DateInterval.Day, -1, dtToday)
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = dpEndDate.SelectedDate
                dpStartDate.Visible = True
                '----->>>>> Location <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = False
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = False
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = False
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "Loads Over 2 Hours Late"
                lbSwitch.CommandName = "~/ClientSvcs/LoadsOverTwoHoursLate.aspx"
                lbSwitch.Visible = False
            Case "RestackPallets"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = dtToday
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -6, dtToday)
                dpStartDate.Visible = True
                '----->>>>> Location(s) <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = False
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = True
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = True
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "Restack Pallets"
                lbSwitch.CommandName = "~/ClientSvcs/RestackPallets.aspx"
            Case "TravelTime"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = dtToday
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -6, dtToday)
                dpStartDate.Visible = True
                '----->>>>> Location(s) <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = False
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = False
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = False
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "Travel Time "
                lbSwitch.Visible = False
                lbSwitch.CommandName = ""
            Case "ForkliftCerts"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = False
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = dtToday
                dpEndDate.Visible = False
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = False
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -6, dtToday)
                dpStartDate.Visible = False
                '----->>>>> Location(s) <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = False
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = False
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = False
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "Forklift Certifications"
                lbSwitch.Visible = False
                lbSwitch.CommandName = ""
            Case "ImportNoShows"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = dtToday
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -6, dtToday)
                dpStartDate.Visible = True
                '----->>>>> Location(s) <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = False
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = False
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = False
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "Import No Shows "
                lbSwitch.Visible = False
                lbSwitch.CommandName = ""
            Case "PalletsPiecesGraph"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = dtToday
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -1, dtToday)
                dpStartDate.Visible = True
                '----->>>>> Location(s) <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = False
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = False
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = False
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "PalletsPiecesGraph"
                lbSwitch.Visible = False
                lbSwitch.CommandName = ""
            Case "AuditTimeCards"
                '----->>>>> EndDate <<<<<-----
                lblEndDate.Visible = True
                '   lblEndDate.Text = "xx"
                dpEndDate.SelectedDate = dtToday
                dpEndDate.Visible = True
                '----->>>>> StartDate <<<<<-----
                lblStartDate.Visible = True
                '   lblStartDate.Text = "xx"
                dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -6, dtToday)
                dpStartDate.Visible = True
                '----->>>>> Location(s) <<<<<-----
                lblLocation.Visible = True
                '   lblLocation.Text = "xx"
                cbLocations.Visible = True
                '----->>>>> Department(s) <<<<<-----
                lblDepartment.Visible = False
                '   lblDepartment.Text = "xx"
                cbDepartment.Visible = False
                '----->>>>> Vendor Number <<<<<-----
                lblVendorNumber.Visible = False
                '   lblVendorNumber.Text = "xx"
                txtVendorNumber.Visible = False
                '----->>>>> Report Name <<<<<-----
                lblReportName.Text = "Time Card Audit"
                lbSwitch.Visible = False
                lbSwitch.CommandName = ""
        End Select
        If lblDepartment.Visible Then
            cbLocations.AutoPostBack = True
            If cbLocations.SelectedIndex > -1 Then
                Dim ldal As New locaDAL
                cbDepartment.DataSource = ldal.GetDepartmentsByLocationID(cbLocations.SelectedValue.ToString)
                cbDepartment.DataTextField = "Name"
                cbDepartment.DataValueField = "ID"
                cbDepartment.DataBind()
                cbDepartment.ClearSelection()
                cbDepartment.EmptyMessage = "Select Department"
            Else
                cbDepartment.EmptyMessage = "No Location Selected"
            End If
        End If
        checkQueryString()
    End Sub
#End Region

#Region "Button Click, Run Report"
    Protected Sub btnRunReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRunReport.Click
        '        Dim report As SEUreports.VendorReport '<<< just to init an SEUreports telerik report
        Dim errStr As String = ValidateParams()
        If errStr.Length > 0 Then
            ReportViewer1.Visible = False
            lblInstructions.Visible = True
            lblReportName.Visible = True
            lblInstructions.Text = "<br><br><br><center>Please select parameters<br /><font color='red'>" & errStr & "</font>"
            Exit Sub
        End If
        Dim ldal As New locaDAL()
        Dim vLocaName As String = String.Empty
        Dim vlocaID As Guid = Nothing
        Dim rootDir As String = Server.MapPath("~/")
        If cbLocations.Visible Then
            vLocaName = cbLocations.SelectedItem.Text.Trim()
            vlocaID = New Guid(cbLocations.SelectedValue.ToString())
        End If
        Dim sdt As DateTime = Date.Now
        Dim edt As DateTime = Date.Now
        Dim sbdOffSet As Integer = 0

        If dpStartDate.Visible And dpEndDate.Visible Then

            sdt = dpStartDate.SelectedDate
            edt = dpStartDate.SelectedDate
            edt = DateAdd(DateInterval.Day, 1, edt) 'add one day to the selected endDate
            edt = DateAdd(DateInterval.Second, -1, edt) 'subtract one second
            sbdOffSet = ldal.getLocaBDOffset(vlocaID)
            If sbdOffSet <> 0 Then
                sdt = DateAdd(DateInterval.Hour, sbdOffSet, sdt)
                edt = DateAdd(DateInterval.Hour, sbdOffSet, edt)
            End If
        End If
        '        sbdOffSet = 0
        Dim userDal As New userDAL
        Dim usr As ssUser = userDal.getUserByName(User.Identity.Name)
        Select Case Request.QueryString("report")

            Case "POsearch"  'PO Search
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.POsearch()
                report.Parameters.Add(New Telerik.Reporting.Parameter("POnumber", IIf(txtVendorNumber.Text Is Nothing, " ", txtVendorNumber.Text)))
                report.Parameters.Add(New Telerik.Reporting.Parameter("location", vLocaName))
                '               report.Parameters.Add(New Telerik.Reporting.Parameter("startdate", sdt))
                '               report.Parameters.Add(New Telerik.Reporting.Parameter("enddate", edt))
                '               If cbDepartment.SelectedIndex = -1 Then
                '                report.Parameters.Add(New Telerik.Reporting.Parameter("department", " "))
                '                Else
                '                report.Parameters.Add(New Telerik.Reporting.Parameter("department", cbDepartment.SelectedItem.Text))
                '                End If
                '                report.Parameters.Add(New Telerik.Reporting.Parameter("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName))
                report.Parameters.Add(New Telerik.Reporting.Parameter("imgPath", Utilities.getLogo(vlocaID, rootDir)))
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "VendorReport"  'Vendor Report
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.VendorReport

                Dim r As Report = TryCast(report.ReportDocument, Report)
                ' r.DataSource = getVendorReportDataSource(sdt, edt, cbLocations.SelectedItem.Text, sbdOffSet <> 0)
                report.Parameters.Add("VendorNumber", IIf(txtVendorNumber.Text Is Nothing, " ", txtVendorNumber.Text))
                report.Parameters.Add("location", vLocaName)
                report.Parameters.Add("startdate", sdt)
                report.Parameters.Add("enddate", edt)
                '   If cbDepartment.SelectedIndex = -1 Then
                '      report.Parameters.Add("department", " ")
                '   Else
                '   report.Parameters.Add("department", cbDepartment.SelectedItem.Text)
                '   End If
                report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                Dim img As String = Utilities.getLogo(vlocaID, rootDir)
                report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "AdditionalCosts"  'Additional Costs Report
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.AdditionalCosts
                If sbdOffSet <> 0 Then
                    Dim r As Report = TryCast(report.ReportDocument, Report)
                    r.DataSource = getAdditionalCostDataSource(sdt, edt, cbLocations.SelectedItem.Text)
                    report.Parameters.Add("location", vLocaName & " (includes offset " & sbdOffSet.ToString & " hrs)")
                    report.Parameters.Add(New Telerik.Reporting.Parameter("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName))
                    report.Parameters.Add("startdate", sdt)
                    report.Parameters.Add("enddate", edt)
                    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                Else
                    report.Parameters.Add("location", vLocaName)
                    report.Parameters.Add(New Telerik.Reporting.Parameter("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName))
                    report.Parameters.Add("startdate", sdt)
                    report.Parameters.Add("enddate", edt)
                    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                End If
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "BadPalletReport"  'Bad Pallet Report
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.BadPalletReport
                Dim r As Report = TryCast(report.ReportDocument, Report)
                r.DataSource = getBadPalletReportDataSource(sdt, edt, cbLocations.SelectedItem.Text, sbdOffSet <> 0)
                report.Parameters.Add("VendorNumber", IIf(txtVendorNumber.Text Is Nothing, " ", txtVendorNumber.Text))
                report.Parameters.Add("location", vLocaName)
                report.Parameters.Add("startdate", sdt)
                report.Parameters.Add("enddate", edt)
                report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "LoadsDetailed"  'Loads Detailed Report
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.LoadsDetailed
                Dim r As Report = TryCast(report.ReportDocument, Report)
                r.DataSource = getLoadsDetailedDataSource(sdt, edt, cbLocations.SelectedItem.Text, sbdOffSet <> 0)
                report.Parameters.Add("VendorNumber", IIf(txtVendorNumber.Text Is Nothing, " ", txtVendorNumber.Text))
                report.Parameters.Add("location", vLocaName)
                report.Parameters.Add("startdate", sdt)
                report.Parameters.Add("enddate", edt)
                report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "AverageCost"  'Average Cost Report
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.AverageCost
                Dim r As Report = TryCast(report.ReportDocument, Report)
                r.DataSource = getAverageCostDataSource(sdt, edt, cbLocations.SelectedItem.Text, sbdOffSet <> 0)
                report.Parameters.Add("VendorNumber", IIf(txtVendorNumber.Text Is Nothing, " ", txtVendorNumber.Text))
                report.Parameters.Add("location", vLocaName)
                report.Parameters.Add("startdate", sdt)
                report.Parameters.Add("enddate", edt)
                report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "VendorSummary"
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.VendorSummary
                If sbdOffSet <> 0 Then
                    Dim r As Report = TryCast(report.ReportDocument, Report)
                    r.DataSource = getVendorSummaryDataSource(sdt, edt, cbLocations.SelectedItem.Text)
                    report.Parameters.Add("VendorNumber", IIf(txtVendorNumber.Text Is Nothing, " ", txtVendorNumber.Text))
                    report.Parameters.Add("location", vLocaName & " (includes offset " & sbdOffSet.ToString & " hrs)")
                    report.Parameters.Add("startdate", sdt)
                    report.Parameters.Add("enddate", edt)
                    report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                Else
                    report.Parameters.Add("VendorNumber", IIf(txtVendorNumber.Text Is Nothing, " ", txtVendorNumber.Text))
                    report.Parameters.Add("location", vLocaName)
                    report.Parameters.Add("startdate", sdt)
                    report.Parameters.Add("enddate", edt)
                    report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                End If
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "BusinessSummary"
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.BusinessSummary
                Dim r As Report = TryCast(report.ReportDocument, Report)
                If sbdOffSet <> 0 Then
                    r.DataSource = getBSdataSource(sdt, edt, cbLocations.SelectedItem.Text, True)
                    report.Parameters.Add("startdate", sdt)
                    report.Parameters.Add("enddate", edt)
                    report.Parameters.Add("location", vLocaName & " (includes offset " & sbdOffSet.ToString & " hrs) ")
                    report.Parameters.Add("txtStart", Format(sdt, "ddd dd MMM yy  h:mm tt"))
                    report.Parameters.Add("txtEnd", Format(edt, "ddd dd MMM yy  h:mm tt"))
                    report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                Else
                    r.DataSource = getBSdataSource(sdt, edt, cbLocations.SelectedItem.Text, False)
                    report.Parameters.Add("startdate", sdt)
                    report.Parameters.Add("enddate", edt)
                    report.Parameters.Add("location", vLocaName) 'DO NOT EDIT
                    report.Parameters.Add("txtStart", Format(sdt, "ddd dd MMM yy"))
                    report.Parameters.Add("txtEnd", Format(edt, "ddd dd MMM yy"))
                    report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                End If
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "BusinessSummaryNet"
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.BusinessSummaryNet
                Dim r As Report = TryCast(report.ReportDocument, Report)
                If sbdOffSet <> 0 Then
                    r.DataSource = getBSdataSourceNet(sdt, edt, cbLocations.SelectedItem.Text, True)
                    report.Parameters.Add("startdate", sdt)
                    report.Parameters.Add("enddate", edt)
                    report.Parameters.Add("location", vLocaName & " (includes offset " & sbdOffSet.ToString & " hrs) ")
                    report.Parameters.Add("txtStart", Format(sdt, "ddd dd MMM yy  h:mm tt"))
                    report.Parameters.Add("txtEnd", Format(edt, "ddd dd MMM yy  h:mm tt"))
                    report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                Else
                    r.DataSource = getBSdataSourceNet(sdt, edt, cbLocations.SelectedItem.Text, False)
                    report.Parameters.Add("startdate", sdt)
                    report.Parameters.Add("enddate", edt)
                    report.Parameters.Add("location", vLocaName) 'DO NOT EDIT
                    report.Parameters.Add("txtStart", Format(sdt, "ddd dd MMM yy"))
                    report.Parameters.Add("txtEnd", Format(edt, "ddd dd MMM yy"))
                    report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                End If
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "BusinessSummaryCashBreakdown"
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.BusinessSummaryCashBreakdown
                Dim r As Report = TryCast(report.ReportDocument, Report)
                If sbdOffSet <> 0 Then
                    r.DataSource = getBSCashBreakdowndataSource(sdt, edt, cbLocations.SelectedItem.Text, True)
                    report.Parameters.Add("startdate", sdt)
                    report.Parameters.Add("enddate", edt)
                    report.Parameters.Add("location", vLocaName & " (includes offset " & sbdOffSet.ToString & " hrs) ")
                    report.Parameters.Add("txtStart", Format(sdt, "ddd dd MMM yy  h:mm tt"))
                    report.Parameters.Add("txtEnd", Format(edt, "ddd dd MMM yy  h:mm tt"))
                    report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                Else
                    r.DataSource = getBSCashBreakdowndataSource(sdt, edt, cbLocations.SelectedItem.Text, False)
                    report.Parameters.Add("startdate", sdt)
                    report.Parameters.Add("enddate", edt)
                    report.Parameters.Add("location", vLocaName) 'DO NOT EDIT
                    report.Parameters.Add("txtStart", Format(sdt, "ddd dd MMM yy"))
                    report.Parameters.Add("txtEnd", Format(edt, "ddd dd MMM yy"))
                    report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                End If
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "DailyWeekly"
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.DailyWeeklyReport
                Dim r As Report = TryCast(report.ReportDocument, Report)
                If sbdOffSet <> 0 Then
                    r.DataSource = getDailyWeeklydataSource(sdt, edt, cbLocations.SelectedItem.Text, True)
                    report.Parameters.Add("startdate", sdt)
                    report.Parameters.Add("enddate", edt)
                    report.Parameters.Add("location", vLocaName & " (includes offset " & sbdOffSet.ToString & " hrs) ")
                    '                    report.Parameters.Add("txtStart", Format(sdt, "ddd dd MMM yy  h:mm tt"))
                    '                    report.Parameters.Add("txtEnd", Format(edt, "ddd dd MMM yy  h:mm tt"))
                    '                    report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                Else
                    r.DataSource = getDailyWeeklydataSource(sdt, edt, cbLocations.SelectedItem.Text, False)
                    report.Parameters.Add("startdate", sdt)
                    report.Parameters.Add("enddate", edt)
                    report.Parameters.Add("location", vLocaName) 'DO NOT EDIT
                    '                   report.Parameters.Add("txtStart", Format(sdt, "ddd dd MMM yy"))
                    '                   report.Parameters.Add("txtEnd", Format(edt, "ddd dd MMM yy"))
                    '                   report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                End If
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

                'Case "DriverWorked"
                '    Dim report As New Telerik.Reporting.InstanceReportSource
                '    report.ReportDocument = New SEUreports.DriverWorked
                '    '                Dim r As Report = TryCast(report.ReportDocument, Report)
                '    '                r.DataSource = getDriverWorkedDataSource(sdt, edt, cbLocations.SelectedItem.Text, sbdOffSet <> 0)
                '    report.Parameters.Add("location", vLocaName)
                '    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                '    report.Parameters.Add("startdate", sdt)
                '    report.Parameters.Add("enddate", edt)
                '    ReportViewer1.ReportSource = report
                '    ReportViewer1.Visible = True

            Case "LoadsOverTwoHours"
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.LoadsOverTwoHours
                Dim r As Report = TryCast(report.ReportDocument, Report)
                If sbdOffSet <> 0 Then
                    r.DataSource = getLoadsOverTwoHoursDataSource(sdt, edt, cbLocations.SelectedItem.Text, True)
                    report.Parameters.Add("startdate", sdt)
                    report.Parameters.Add("enddate", edt)
                    report.Parameters.Add("location", vLocaName & " (includes offset " & sbdOffSet.ToString & " hrs) ")
                    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                Else
                    r.DataSource = getLoadsOverTwoHoursDataSource(sdt, edt, cbLocations.SelectedItem.Text, False)
                    report.Parameters.Add("startdate", sdt)
                    report.Parameters.Add("enddate", edt)
                    report.Parameters.Add("location", vLocaName)
                    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                End If
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "LoadsLessThanTenMin"
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.LoadsLessThanTenMin
                Dim r As Report = TryCast(report.ReportDocument, Report)
                r.DataSource = getLoadsLessThanTenMinDataSource(sdt, edt, cbLocations.SelectedItem.Text, sbdOffSet <> 0)
                report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                report.Parameters.Add("location", vLocaName)
                report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                report.Parameters.Add("startdate", sdt)
                report.Parameters.Add("enddate", edt)
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "OnTimeDelivery"
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.OnTimeDelivery
                Dim r As Report = TryCast(report.ReportDocument, Report)
                If sbdOffSet <> 0 Then
                    r.DataSource = getOnTimeDeliveryDataSource(sdt, edt, cbLocations.SelectedItem.Text, True)
                    report.Parameters.Add("startdate", sdt)
                    report.Parameters.Add("enddate", edt)
                    report.Parameters.Add("location", vLocaName & " (includes offset " & sbdOffSet.ToString & " hrs) ")
                    report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                    If cbDepartment.SelectedIndex = -1 Then
                        report.Parameters.Add("department", " ")
                    Else
                        report.Parameters.Add("department", cbDepartment.SelectedItem.Text)
                    End If
                Else
                    r.DataSource = getOnTimeDeliveryDataSource(sdt, edt, cbLocations.SelectedItem.Text, False)
                    report.Parameters.Add("startdate", sdt)
                    report.Parameters.Add("enddate", edt)
                    report.Parameters.Add("location", vLocaName) 'DO NOT EDIT
                    report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                    If cbDepartment.SelectedIndex = -1 Then
                        report.Parameters.Add("department", " ")
                    Else
                        report.Parameters.Add("department", cbDepartment.SelectedItem.Text)
                    End If
                End If
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "LoadsOverTwoHoursLate"
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.LoadsOverTwoHoursLate
                Dim r As Report = TryCast(report.ReportDocument, Report)
                If sbdOffSet <> 0 Then
                    r.DataSource = getLoadsOver2HoursLateDataSource(sdt, edt, cbLocations.SelectedItem.Text, True)
                    report.Parameters.Add("startdate", sdt)
                    report.Parameters.Add("enddate", edt)
                    report.Parameters.Add("location", vLocaName & " (includes offset " & sbdOffSet.ToString & " hrs) ")
                    '                    report.Parameters.Add("txtStart", Format(sdt, "ddd dd MMM yy  h:mm tt"))
                    '                    report.Parameters.Add("txtEnd", Format(edt, "ddd dd MMM yy  h:mm tt"))
                    report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                Else
                    r.DataSource = getLoadsOver2HoursLateDataSource(sdt, edt, cbLocations.SelectedItem.Text, False)
                    report.Parameters.Add("startdate", sdt)
                    report.Parameters.Add("enddate", edt)
                    report.Parameters.Add("location", vLocaName) 'DO NOT EDIT
                    '                   report.Parameters.Add("txtStart", Format(sdt, "ddd dd MMM yy"))
                    '                   report.Parameters.Add("txtEnd", Format(edt, "ddd dd MMM yy"))
                    report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                    report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                End If
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "RestackPallets"  'Restack Pallet Report
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.RestackReport
                Dim r As Report = TryCast(report.ReportDocument, Report)
                r.DataSource = getRestackPalletReportDataSource(sdt, edt, cbLocations.SelectedItem.Text, sbdOffSet <> 0)
                report.Parameters.Add("VendorNumber", IIf(txtVendorNumber.Text Is Nothing, " ", txtVendorNumber.Text))
                report.Parameters.Add("location", vLocaName)
                report.Parameters.Add("startdate", sdt)
                report.Parameters.Add("enddate", edt)
                report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "TravelTime"
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.TravelTime
                ' report.Parameters.Add("sbdOffSet", sbdOffSet)
                report.Parameters.Add("location", vLocaName)
                ' report.Parameters.Add("txtLocation", vLocaName & " (includes offset " & sbdOffSet.ToString & " hrs) ")
                report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                report.Parameters.Add("startdate", sdt)
                report.Parameters.Add("enddate", edt)
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "ImportNoShows"
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.ImportNoShows
                report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                report.Parameters.Add("location", vLocaName)
                report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                report.Parameters.Add("startdate", sdt)
                report.Parameters.Add("enddate", edt)
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "PalletsPiecesGraph"
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.PalletsPiecesGraph
                'report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                report.Parameters.Add("location", vLocaName)
                report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                report.Parameters.Add("startdate", sdt)
                report.Parameters.Add("enddate", edt)
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "AuditTimeCards"
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.AuditTimeCards
                report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                report.Parameters.Add("location", vLocaName)
                ' report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                report.Parameters.Add("startdate", sdt)
                report.Parameters.Add("enddate", edt)
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case "ForkliftCerts"
                Dim report As New Telerik.Reporting.InstanceReportSource
                report.ReportDocument = New SEUreports.ForkliftCerts
                ' report.Parameters.Add("createdby", "Report CreatedBy: " & usr.FirstName & " " & usr.LastName)
                report.Parameters.Add("location", vLocaName)
                ' report.Parameters.Add("imgPath", Utilities.getLogo(vlocaID, rootDir))
                ' report.Parameters.Add("startdate", sdt)
                ' report.Parameters.Add("enddate", edt)
                ReportViewer1.ReportSource = report
                ReportViewer1.Visible = True

            Case Else
                ReportViewer1.Visible = False
        End Select
        lblReportName.Visible = Not ReportViewer1.Visible
        lblInstructions.Visible = Not ReportViewer1.Visible
    End Sub
#End Region

#Region "DataSources"


    Private Function getAdditionalCostDataSource(ByVal sDate As DateTime, ByVal eDate As DateTime, ByVal locaName As String) As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        Dim sqlString As String = "SELECT dbo.Location.Name AS location, dbo.WorkOrder.dockTime AS LogDate, dbo.Department.Name AS Department, dbo.Vendor.Name AS CustomerID, " & _
            "dbo.WorkOrder.PurchaseOrder, dbo.WorkOrder.BadPallets, dbo.unf_GetLocationBadPalletCosts(@location, dbo.WorkOrder.LoadTypeID)  " & _
            "* dbo.WorkOrder.BadPallets AS BadCost, dbo.Carrier.Name AS Carrier, dbo.WorkOrder.Restacks, dbo.unf_GetLocationRestkPalletCosts(@location,  " & _
            "dbo.WorkOrder.LoadTypeID) * dbo.WorkOrder.Restacks AS ReskCost, dbo.LoadType.Name AS Type, dbo.unf_GetLocationBadPalletCosts(@location,  " & _
            "dbo.WorkOrder.LoadTypeID) * dbo.WorkOrder.BadPallets + dbo.unf_GetLocationRestkPalletCosts(@location, dbo.WorkOrder.LoadTypeID)  " & _
            "* dbo.WorkOrder.Restacks AS Amount " & _
            "FROM dbo.WorkOrder INNER JOIN " & _
            "dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID INNER JOIN " & _
            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID INNER JOIN " & _
            "dbo.Department ON dbo.WorkOrder.DepartmentID = dbo.Department.ID INNER JOIN " & _
            "dbo.Vendor ON dbo.WorkOrder.CustomerID = dbo.Vendor.ID INNER JOIN " & _
            "dbo.Carrier ON dbo.WorkOrder.CarrierID = dbo.Carrier.ID " & _
            "WHERE (dbo.WorkOrder.docktime <= @enddate) OR " & _
            "(dbo.WorkOrder.docktime <= @enddate) " & _
            "GROUP BY dbo.WorkOrder.LoadNumber, dbo.WorkOrder.BadPallets, dbo.WorkOrder.Restacks, dbo.unf_GetLocationBadPalletCosts(@location,  " & _
            "dbo.WorkOrder.LoadTypeID) * dbo.WorkOrder.BadPallets, dbo.LoadType.Name, dbo.unf_GetLocationRestkPalletCosts(@location,  " & _
            "dbo.WorkOrder.LoadTypeID) * dbo.WorkOrder.Restacks, dbo.WorkOrder.dockTime, dbo.Location.Name, dbo.Department.Name, dbo.Vendor.Name,  " & _
            "dbo.WorkOrder.PurchaseOrder, dbo.unf_GetLocationBadPalletCosts(@location, dbo.WorkOrder.LoadTypeID)  " & _
            "* dbo.WorkOrder.BadPallets + dbo.unf_GetLocationRestkPalletCosts(@location, dbo.WorkOrder.LoadTypeID) * dbo.WorkOrder.Restacks,  " & _
            "dbo.Carrier.Name " & _
            "HAVING (dbo.WorkOrder.BadPallets > 0) AND (dbo.WorkOrder.docktime >= @startdate) AND (dbo.Location.Name = @location) OR " & _
            "(dbo.WorkOrder.Restacks > 0) AND (dbo.WorkOrder.docktime >= @startdate) AND (dbo.Location.Name = @location) " & _
            "ORDER BY dbo.WorkOrder.LoadNumber "
        dba.CommandText = sqlString
        dba.AddParameter("@startdate", sDate)
        dba.AddParameter("@enddate", eDate)
        dba.AddParameter("@location", locaName)
        dt = dba.ExecuteDataSet.Tables(0)
        Return dt

    End Function

    Private Function getBSdataSource(ByVal sDate As DateTime, ByVal eDate As DateTime, ByVal locaName As String, ByVal isOffset As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sql As String
        sql = "SELECT dbo.LoadType.Name AS [Load Type], dbo.Department.Name AS Department, COUNT(dbo.LoadType.Name) AS Loads, SUM(dbo.WorkOrder.PalletsUnloaded)  " & _
            "AS [Pallets Unloaded], SUM(dbo.WorkOrder.PalletsReceived) AS [Pallets Received], SUM(dbo.WorkOrder.Pieces) AS Pieces, SUM(dbo.WorkOrder.Amount)  " & _
            "AS Amount " & _
            "FROM dbo.WorkOrder INNER JOIN " & _
            "dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID INNER JOIN " & _
            "dbo.Department ON dbo.WorkOrder.DepartmentID = dbo.Department.ID INNER JOIN " & _
            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID AND dbo.Location.Name = @location "
        If isOffset Then
            sql &= "WHERE (dbo.WorkOrder.DockTime >= @startdate) AND (dbo.WorkOrder.DockTime <= @enddate) "
        Else
            sql &= "WHERE (dbo.WorkOrder.LogDate >= @startdate) AND (dbo.WorkOrder.LogDate <= @enddate) "
        End If
        sql &= "GROUP BY dbo.Department.Name, dbo.LoadType.Name "
        If cbDepartment.SelectedIndex > -1 Then
            sql &= "HAVING (dbo.Department.Name = @department)"
        End If

        Dim dba As New DBAccess()
        dba.CommandText = sql
        dba.AddParameter("@location", locaName)
        dba.AddParameter("@startdate", sDate)
        dba.AddParameter("@enddate", eDate)
        If cbDepartment.SelectedIndex > -1 Then
            dba.AddParameter("@department", cbDepartment.SelectedItem.Text())
        End If
        Try
            dt = dba.ExecuteDataSet.Tables(0)

        Catch ex As Exception
            Dim retString As String = ex.Message
        End Try


        Return dt
    End Function

    Private Function getBSdataSourceNet(ByVal sDate As DateTime, ByVal eDate As DateTime, ByVal locaName As String, ByVal isOffset As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sql As String
        sql = "SELECT dbo.LoadType.Name AS LoadType, dbo.Department.Name AS Department, COUNT(dbo.WorkOrder.LoadNumber) AS POcount, SUM(dbo.WorkOrder.PalletsUnloaded) AS [Pallets Unloaded], " & _
        "SUM(dbo.WorkOrder.PalletsReceived) AS [Pallets Received], SUM(dbo.WorkOrder.Pieces) AS Pieces, SUM(CASE WHEN [CheckNumber] = '' THEN (0.00)ELSE dbo.unf_GetLocationCheckCosts(@location) END) AS [Ck Charges], " & _
        "SUM(CASE WHEN dbo.LoadType.Name = 'Cash' OR dbo.LoadType.Name = 'Invoice' THEN dbo.unf_GetLocationOtherCosts(@location) ELSE (0.00) END) AS OtherCosts, SUM(CASE WHEN dbo.LoadType.Name = 'Invoice' " & _
        "THEN WorkOrder.Amount - dbo.unf_GetLocationOtherCosts(@location) WHEN dbo.LoadType.Name = 'Cash' AND [CheckNumber] = '' THEN WorkOrder.Amount - dbo.unf_GetLocationOtherCosts(@location) " & _
        "WHEN dbo.LoadType.Name = 'Cash' AND [CheckNumber] > '' THEN (WorkOrder.Amount - dbo.unf_GetLocationOtherCosts(@location))- dbo.unf_GetLocationCheckCosts(@location) ELSE dbo.WorkOrder.Amount " & _
        "END) AS Amount FROM dbo.WorkOrder INNER JOIN dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID INNER JOIN  dbo.Department ON dbo.WorkOrder.DepartmentID = dbo.Department.ID INNER JOIN " & _
        "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID AND dbo.Location.Name = @location "

        If isOffset Then

            sql &= "WHERE (dbo.WorkOrder.DockTime >= @startdate) AND (dbo.WorkOrder.DockTime <= @enddate) "

        Else

            sql &= "WHERE (dbo.WorkOrder.LogDate >= @startdate) AND (dbo.WorkOrder.LogDate <= @enddate) "

        End If

        sql &= "GROUP BY dbo.Department.Name, dbo.LoadType.Name "

        If cbDepartment.SelectedIndex > -1 Then
            sql &= "HAVING (dbo.Department.Name = @department)"
        End If

        Dim dba As New DBAccess()
        dba.CommandText = sql
        dba.AddParameter("@location", locaName)
        dba.AddParameter("@startdate", sDate)
        dba.AddParameter("@enddate", eDate)
        If cbDepartment.SelectedIndex > -1 Then
            dba.AddParameter("@department", cbDepartment.SelectedItem.Text())
        End If
        Try
            dt = dba.ExecuteDataSet.Tables(0)

        Catch ex As Exception
            Dim retString As String = ex.Message
        End Try

        Return dt
    End Function

    Private Function getBSCashBreakdowndataSource(ByVal sDate As DateTime, ByVal eDate As DateTime, ByVal locaName As String, ByVal isOffset As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sql As String
        sql = "SELECT dbo.LoadType.Name AS LoadType, COUNT(dbo.WorkOrder.ID) AS POCount, SUM(dbo.WorkOrder.Pieces) AS Pieces, " & _
            "SUM(dbo.WorkOrder.PalletsUnloaded) AS PalUnld, SUM(dbo.WorkOrder.PalletsReceived) AS PalRecv, " & _
            "SUM(dbo.WorkOrder.Amount) AS Amount, SUM(dbo.WorkOrder.SplitPaymentAmount) AS Split, CASE WHEN dbo.WorkOrder.CheckNumber > '1' THEN 'Check' WHEN dbo.LoadType.Name ='Cash' " & _
            "THEN 'Cash' ELSE '- - - -' END AS CkCash " & _
            "FROM  dbo.WorkOrder INNER JOIN dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID INNER JOIN " & _
            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID AND dbo.Location.Name = @location "
        If isOffset Then
            sql &= "WHERE (dbo.WorkOrder.DockTime >= @startdate) AND (dbo.WorkOrder.DockTime <= @enddate) "
        Else
            sql &= "WHERE (dbo.WorkOrder.LogDate >= @startdate) AND (dbo.WorkOrder.LogDate <= @enddate) "
        End If
        sql &= "GROUP BY dbo.LoadType.Name, " & _
               "CASE WHEN dbo.WorkOrder.CheckNumber > '1' THEN 'Check' WHEN dbo.LoadType.Name = 'Cash' THEN 'Cash' ELSE '- - - -' END " & _
               "ORDER BY LoadType "

        If cbDepartment.SelectedIndex > -1 Then
            sql &= "HAVING (dbo.Department.Name = @department)"
        End If

        Dim dba As New DBAccess()
        dba.CommandText = sql
        dba.AddParameter("@location", locaName)
        dba.AddParameter("@startdate", sDate)
        dba.AddParameter("@enddate", eDate)
        If cbDepartment.SelectedIndex > -1 Then
            dba.AddParameter("@department", cbDepartment.SelectedItem.Text())
        End If
        Try
            dt = dba.ExecuteDataSet.Tables(0)

        Catch ex As Exception
            Dim retString As String = ex.Message
        End Try
        Return dt

    End Function

    Private Function getDailyWeeklydataSource(ByVal sDate As DateTime, ByVal eDate As DateTime, ByVal locaName As String, ByVal isOffset As Boolean) As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        Dim sqlString As String = "SELECT Location_1.Name,  " & _
            "CONVERT(varchar(10), dbo.WorkOrder.LogDate, 110) AS WorkDate,  " & _
            "COUNT(DISTINCT dbo.WorkOrder.PurchaseOrder) AS NumOfPOs,  " & _
            "COUNT(DISTINCT CONVERT(varchar(10), dbo.WorkOrder.LogDate, 110)+ dbo.LoadType.Name + dbo.WorkOrder.DoorNumber + dbo.WorkOrder.TrailerNumber) AS NumOfLoads,  " & _
            "SUM(dbo.WorkOrder.PalletsUnloaded)AS PalUnld,  " & _
            "SUM(dbo.WorkOrder.Pieces) AS Pieces,  " & _
            "SUM(dbo.WorkOrder.PalletsReceived) AS PalRecd,  " & _
            "SUM(dbo.WorkOrder.BadPallets) AS Bad,  " & _
            "SUM(dbo.WorkOrder.Restacks) AS Resk, " & _
            "(SELECT SUM(dbo.TimeInOut.HoursWorked) AS HOURS " & _
            "FROM dbo.Employee INNER JOIN " & _
            "dbo.TimePunche ON dbo.Employee.ID = dbo.TimePunche.EmployeeID INNER JOIN " & _
            "dbo.TimeInOut ON dbo.TimePunche.ID = dbo.TimeInOut.TimepuncheID LEFT OUTER JOIN " & _
            "dbo.Location ON dbo.Employee.LocationID = dbo.Location.ID LEFT OUTER JOIN " & _
            "dbo.Employment ON dbo.Employee.ID = dbo.Employment.EmployeeID " & _
            "WHERE (dbo.Location.Name = @location) AND (dbo.TimePunche.DateWorked = CONVERT(varchar(10), dbo.WorkOrder.LogDate, 110)) AND  " & _
            "(dbo.Employment.JobTitle LIKE 'Unloader%')) AS Hours " & _
            "FROM dbo.WorkOrder INNER JOIN " & _
            "dbo.Location AS Location_1 ON dbo.WorkOrder.LocationID = Location_1.ID INNER JOIN " & _
            "dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID " & _
            "WHERE (Location_1.Name = @location)  "
        If isOffset Then
            sqlString &= "AND (dbo.WorkOrder.DockTime >= @startdate) AND (dbo.WorkOrder.DockTime <= @enddate) "
        Else
            sqlString &= "AND (dbo.WorkOrder.LogDate >= @startdate) AND (dbo.WorkOrder.LogDate <= @enddate) "
        End If
        sqlString &= "GROUP BY CONVERT(varchar(10), dbo.WorkOrder.LogDate, 110), Location_1.Name " & _
            "ORDER BY WorkDate "
        dba.CommandText = sqlString
        dba.AddParameter("@location", locaName)
        dba.AddParameter("@startdate", sDate)
        dba.AddParameter("@enddate", eDate)

        dt = dba.ExecuteDataSet.Tables(0)
        Return dt

    End Function

    Private Function getDriverWorkedDataSource(ByVal sDate As DateTime, ByVal eDate As DateTime, ByVal locaName As String, ByVal isOffset As Boolean) As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        Dim sqlString As String = "SELECT dbo.Location.Name, dbo.WorkOrder.LogDate, dbo.Department.Name AS Dept, dbo.WorkOrder.PurchaseOrder AS PO,  " & _
        "dbo.Vendor.Name AS CustIDCode, dbo.WorkOrder.VendorNumber, dbo.Carrier.Name AS Carrier, dbo.WorkOrder.AppointmentTime, dbo.WorkOrder.DockTime, dbo.WorkOrder.Pieces " & _
        "FROM dbo.WorkOrder INNER JOIN " & _
        "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID INNER JOIN " & _
        "dbo.Department ON dbo.WorkOrder.DepartmentID = dbo.Department.ID INNER JOIN " & _
        "dbo.Unloader ON dbo.WorkOrder.ID = dbo.Unloader.LoadID INNER JOIN " & _
        "dbo.Employee ON dbo.Unloader.EmployeeID = dbo.Employee.ID INNER JOIN " & _
        "dbo.Vendor ON dbo.WorkOrder.CustomerID = dbo.Vendor.ID INNER JOIN " & _
        "dbo.Carrier ON dbo.WorkOrder.CarrierID = dbo.Carrier.ID " & _
        "WHERE(dbo.Employee.FirstName = 'Truck') AND (dbo.Employee.LastName = 'Driver') AND (dbo.Location.Name = @location) AND  " & _
        "(dbo.WorkOrder.LogDate >= @startdate) AND (dbo.WorkOrder.LogDate <= @enddate) " & _
        "ORDER BY dbo.WorkOrder.LogDate DESC "

        dba.CommandText = sqlString
        dba.AddParameter("@location", locaName)
        dba.AddParameter("@startdate", sDate)
        dba.AddParameter("@enddate", eDate)

        dt = dba.ExecuteDataSet.Tables(0)
        Return dt

    End Function

    Private Function getVendorSummaryDataSource(ByVal sDate As DateTime, ByVal eDate As DateTime, ByVal locaName As String) As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        Dim sqlString As String = "SELECT COUNT(dbo.WorkOrder.PurchaseOrder) AS Loads, " & _
            "dbo.Vendor.Name AS Vendor, " & _
            "dbo.WorkOrder.VendorNumber,  " & _
            "SUM(dbo.WorkOrder.PalletsUnloaded) AS [Pallets Unloaded], " & _
            "SUM(dbo.WorkOrder.PalletsReceived) AS [Pallets Received],  " & _
            "CONVERT(DECIMAL(5, 2), AVG(dbo.WorkOrder.Amount)) AS Avrg,  " & _
            "CASE WHEN (AVG(DATEDIFF(mi, dbo.WorkOrder.StartTime, dbo.WorkOrder.CompTime)) / 60) < 10  " & _
            "THEN '0' + CAST((AVG(DATEDIFF(mi,dbo.WorkOrder.StartTime, dbo.WorkOrder.CompTime)) / 60) AS varchar) " & _
            "ELSE CAST((AVG(DATEDIFF(mi, dbo.WorkOrder.StartTime, dbo.WorkOrder.CompTime)) / 60) AS varchar)  " & _
            "END + ':' +  " & _
            "CASE WHEN (AVG(DATEDIFF(mi, dbo.WorkOrder.StartTime, dbo.WorkOrder.CompTime)) % 60) < 10  " & _
            "THEN '0' + CAST((AVG(DATEDIFF(mi,dbo.WorkOrder.StartTime, dbo.WorkOrder.CompTime)) % 60) AS varchar) " & _
            "ELSE CAST((AVG(DATEDIFF(mi, dbo.WorkOrder.StartTime, dbo.WorkOrder.CompTime)) % 60) AS varchar) END AS avgt " & _
            "FROM dbo.WorkOrder INNER JOIN " & _
            "dbo.Department ON dbo.WorkOrder.DepartmentID = dbo.Department.ID INNER JOIN " & _
            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID AND dbo.Location.Name = @location INNER JOIN " & _
            "dbo.Vendor ON dbo.WorkOrder.CustomerID = dbo.Vendor.ID " & _
            "WHERE (dbo.WorkOrder.DockTime >= @startdate) AND (dbo.WorkOrder.DockTime <= @enddate) AND (dbo.Location.Name = (@location)) " & _
            "GROUP BY dbo.Vendor.Name, dbo.WorkOrder.VendorNumber "
        If txtVendorNumber.Text.Trim() > "" Then
            sqlString &= "HAVING (dbo.WorkOrder.VendorNumber IN (@VendorNumber)) "
        End If
        sqlString &= "ORDER BY Vendor "
        dba.CommandText = sqlString
        dba.AddParameter("@VendorNumber", txtVendorNumber.Text.Trim())
        dba.AddParameter("@location", locaName)
        dba.AddParameter("@startdate", sDate)
        dba.AddParameter("@enddate", eDate)

        dt = dba.ExecuteDataSet.Tables(0)
        Return dt
    End Function

    Private Function getLoadsOverTwoHoursDataSource(ByVal sDate As DateTime, ByVal eDate As DateTime, ByVal locaName As String, ByVal isOffset As Boolean) As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        Dim sqlString As String = "SELECT dbo.WorkOrder.LogDate, dbo.Location.Name AS Location, " & _
            "dbo.WorkOrder.PurchaseOrder, dbo.Vendor.Name AS [Customer ID], dbo.WorkOrder.VendorNumber,  " & _
            "dbo.WorkOrder.AppointmentTime, dbo.WorkOrder.DockTime, dbo.WorkOrder.StartTime, " & _
            "dbo.WorkOrder.CompTime, " & _
            "CONVERT(Varchar(5),dbo.WorkOrder.CompTime - dbo.WorkOrder.DockTime, 108) AS [Hrs from Dock], " & _
            "CONVERT(Varchar(5),dbo.WorkOrder.CompTime - dbo.WorkOrder.StartTime, 108) AS [Hrs from Assigned], " & _
            "dbo.WorkOrder.Comments,  " & _
            "CASE WHEN Convert (Varchar(6),dbo.WorkOrder.DockTime,108) > Convert (varchar(6),dbo.WorkOrder.StartTime,108) THEN ('Times are wrong') " & _
            "WHEN Convert (Varchar(6),dbo.WorkOrder.DockTime,108) > Convert (varchar(6),dbo.WorkOrder.CompTime,108) THEN ('Times are wrong') " & _
            "WHEN Convert (Varchar(6),dbo.WorkOrder.StartTime,108) > Convert (Varchar(6),dbo.WorkOrder.CompTime,108) THEN ('Times are wrong') " & _
            "ELSE ('') END AS Issue, " & _
            "dbo.WorkOrder.DoorNumber " & _
            "FROM dbo.WorkOrder INNER JOIN " & _
            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID INNER JOIN " & _
            "dbo.Vendor ON dbo.WorkOrder.CustomerID = dbo.Vendor.ID " & _
            "WHERE (dbo.Location.Name = @Location) "
        If isOffset Then
            sqlString &= "AND (dbo.WorkOrder.DockTime >= @startdate) AND (dbo.WorkOrder.DockTime <= @enddate) "
        Else
            sqlString &= "AND (dbo.WorkOrder.LogDate >= @startdate) AND (dbo.WorkOrder.LogDate <= @enddate) "
        End If
        sqlString &= "AND (dbo.WorkOrder.DoorNumber <> N'Drop') AND  " & _
            "(dbo.WorkOrder.CompTime - dbo.WorkOrder.DockTime > CONVERT(DATETIME, '1900-01-01 01:59:00', 102))  " & _
            "ORDER BY dbo.WorkOrder.LogDate, [Customer ID] "
        dba.CommandText = sqlString
        dba.AddParameter("@location", locaName)
        dba.AddParameter("@startdate", sDate)
        dba.AddParameter("@enddate", eDate)

        dt = dba.ExecuteDataSet.Tables(0)
        Return dt
    End Function

    Private Function getLoadsLessThanTenMinDataSource(ByVal sDate As DateTime, ByVal eDate As DateTime, ByVal locaName As String, ByVal isOffset As Boolean) As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        Dim sqlString As String = "SELECT CONVERT(varchar(10), dbo.WorkOrder.LogDate, 110) AS Date,  dbo.Location.Name AS Location, dbo.LoadType.Name AS LoadType,  " & _
        "dbo.WorkOrder.PurchaseOrder AS PO, dbo.WorkOrder.Pieces, dbo.WorkOrder.PalletsUnloaded AS Unloaded, " & _
        "dbo.WorkOrder.PalletsReceived AS Recd, CONVERT(varchar(5), dbo.WorkOrder.DockTime, 108) AS Dock,  " & _
        "CONVERT(varchar(5), dbo.WorkOrder.CompTime, 108) AS Completed,  " & _
        "CASE  WHEN CONVERT(varchar(10), DATEDIFF(n, dbo.WorkOrder.DockTime, dbo.WorkOrder.CompTime), 1)< 0  " & _
        "THEN 'Load not closed'  " & _
        "ELSE CONVERT(varchar(10), DATEDIFF(n, dbo.WorkOrder.DockTime, dbo.WorkOrder.CompTime), 1)  " & _
        "END AS Min,  " & _
        "CASE  WHEN CONVERT(varchar(16), dbo.VerifiedWorkOrders.timeStamp, 109) IS NULL  " & _
        "THEN ''  " & _
        "ELSE CONVERT(varchar(10),dbo.VerifiedWorkOrders.timeStamp, 110) + '  ' + CONVERT(varchar(5), dbo.VerifiedWorkOrders.timeStamp, 108)  " & _
        "END AS ReviewDate,  " & _
        "CASE WHEN dbo.aspnet_Users.UserName IS NULL  " & _
        "THEN 'Not Reviewed'  " & _
        "ELSE dbo.aspnet_Users.UserName  " & _
        "END AS Reviewer, Comments FROM " & _
        "dbo.LoadType INNER JOIN " & _
        "dbo.WorkOrder INNER JOIN " & _
        "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID ON dbo.LoadType.ID = dbo.WorkOrder.LoadTypeID LEFT OUTER JOIN " & _
        "dbo.aspnet_Users INNER JOIN " & _
        "dbo.VerifiedWorkOrders ON dbo.aspnet_Users.UserId = dbo.VerifiedWorkOrders.userID ON  " & _
        "dbo.WorkOrder.ID = dbo.VerifiedWorkOrders.workOrderID " & _
        "WHERE (dbo.Location.Name = @location) AND (DATEDIFF(n, dbo.WorkOrder.DockTime, dbo.WorkOrder.CompTime) < 11) "
        If isOffset Then
            sqlString &= "AND (dbo.WorkOrder.DockTime >= @startdate) AND (dbo.WorkOrder.DockTime <= @enddate) "
        Else
            sqlString &= "AND (dbo.WorkOrder.LogDate >= @startdate) AND (dbo.WorkOrder.LogDate <= @enddate) "
        End If

        sqlString &= "ORDER BY Date DESC "
        dba.CommandText = sqlString
        dba.AddParameter("@location", locaName)
        dba.AddParameter("@startdate", sDate)
        dba.AddParameter("@enddate", eDate)
        dt = dba.ExecuteDataSet.Tables(0)
        Return dt
    End Function

    Private Function getOnTimeDeliveryDataSource(ByVal sDate As DateTime, ByVal eDate As DateTime, ByVal locaName As String, ByVal isOffset As Boolean) As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        Dim sqlString As String = "SELECT dbo.WorkOrder.LogDate, dbo.Location.Name AS Location, dbo.Department.Name AS Department, " & _
        "dbo.Vendor.Name AS Vendor, dbo.Carrier.Name AS Carrier, dbo.WorkOrder.PurchaseOrder AS [PO Numer], " & _
        "CONVERT(varchar(5), dbo.WorkOrder.DockTime, 108) AS [Dock Time], " & _
        "CONVERT(varchar(5), dbo.WorkOrder.AppointmentTime, 108) AS [Appointment Time], " & _
        "(CASE WHEN DockTime > DATEADD(MINUTE, 15,AppointmentTime)THEN 'NO'ELSE 'YES'END) AS [On Time] " & _
        "FROM  dbo.WorkOrder INNER JOIN " & _
        "dbo.Location ON dbo.Location.ID = dbo.WorkOrder.LocationID INNER JOIN " & _
        "dbo.Department ON dbo.Department.ID = dbo.WorkOrder.DepartmentID INNER JOIN " & _
        "dbo.Vendor ON dbo.Vendor.ID = dbo.WorkOrder.CustomerID INNER JOIN " & _
        "dbo.Carrier ON dbo.WorkOrder.CarrierID = dbo.Carrier.ID " & _
        "WHERE(dbo.Location.Name = @Location) "
        If isOffset Then
            sqlString &= "AND (dbo.WorkOrder.DockTime >= @startdate) AND (dbo.WorkOrder.DockTime <= @enddate) "
        Else
            sqlString &= "AND (dbo.WorkOrder.LogDate >= @startdate) AND (dbo.WorkOrder.LogDate <= @enddate) "
        End If
        sqlString &= "AND (dbo.WorkOrder.TruckNumber <> N'Drop') "
        If cbDepartment.SelectedIndex > -1 Then
            sqlString &= "AND Department.Name = @department "
        End If

        sqlString &= "ORDER BY dbo.WorkOrder.LogDate, Vendor "
        dba.CommandText = sqlString
        dba.AddParameter("@location", locaName)
        dba.AddParameter("@startdate", sDate)
        dba.AddParameter("@enddate", eDate)
        If cbDepartment.SelectedIndex > -1 Then
            dba.AddParameter("@department", cbDepartment.SelectedItem.Text())
        End If
        dt = dba.ExecuteDataSet.Tables(0)
        Return dt
    End Function

    Private Function getLoadsOver2HoursLateDataSource(ByVal sDate As DateTime, ByVal eDate As DateTime, ByVal locaName As String, ByVal isOffset As Boolean) As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        Dim sqlString As String = "SELECT dbo.WorkOrder.LogDate, dbo.Location.Name AS Location, dbo.Department.Name AS Department, dbo.Vendor.Name AS Vendor, " & _
        "dbo.Carrier.Name AS Carrier, dbo.WorkOrder.PurchaseOrder AS [PO Numer], CONVERT(varchar(5), dbo.WorkOrder.AppointmentTime, 108) " & _
        "AS [Appointment Time], CONVERT(varchar(5), dbo.WorkOrder.DockTime, 108) AS [Dock Time] " & _
        "FROM dbo.WorkOrder INNER JOIN " & _
        "dbo.Location ON dbo.Location.ID = dbo.WorkOrder.LocationID INNER JOIN " & _
        "dbo.Department ON dbo.Department.ID = dbo.WorkOrder.DepartmentID INNER JOIN " & _
        "dbo.Vendor ON dbo.Vendor.ID = dbo.WorkOrder.CustomerID INNER JOIN " & _
        "dbo.Carrier ON dbo.WorkOrder.CarrierID = dbo.Carrier.ID " & _
        "WHERE (dbo.Location.Name = @Location) "
        If isOffset Then
            sqlString &= "AND (dbo.WorkOrder.DockTime >= @startdate) AND (dbo.WorkOrder.DockTime <= @enddate) "
        Else
            sqlString &= "AND (dbo.WorkOrder.LogDate >= @startdate) AND (dbo.WorkOrder.LogDate <= @enddate) "
        End If
        sqlString &= "AND (dbo.WorkOrder.TruckNumber <> N'Drop') "
        sqlString &= "AND (dbo.WorkOrder.DockTime - dbo.WorkOrder.AppointmentTime > CONVERT(DATETIME, '1900-01-01 01:59:00', 102)) "
        sqlString &= "AND (dbo.WorkOrder.AppointmentTime > CONVERT(DATETIME, '1900-01-02 01:59:00', 102)) "

        sqlString &= "ORDER BY dbo.WorkOrder.LogDate, [Appointment Time] "
        dba.CommandText = sqlString
        dba.AddParameter("@location", locaName)
        dba.AddParameter("@startdate", sDate)
        dba.AddParameter("@enddate", eDate)
        
        dt = dba.ExecuteDataSet.Tables(0)
        Return dt
    End Function

    Private Function getRestackPalletReportDataSource(ByVal sDate As DateTime, ByVal eDate As DateTime, ByVal locaName As String, ByVal isOffset As Boolean) As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        Dim sqlString As String = String.Empty
        sqlString = "SELECT dbo.Location.Name AS Location, dbo.WorkOrder.LogDate AS Date, dbo.Department.Name AS Department,  " & _
                "dbo.Vendor.Name AS [Customer Id], dbo.WorkOrder.PurchaseOrder AS [PO #], dbo.WorkOrder.Restacks, dbo.Carrier.Name AS Carrier " & _
                "FROM dbo.WorkOrder INNER JOIN " & _
                "dbo.Vendor ON dbo.WorkOrder.CustomerID = dbo.Vendor.ID INNER JOIN " & _
                "dbo.Carrier ON dbo.WorkOrder.CarrierID = dbo.Carrier.ID INNER JOIN " & _
                "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID INNER JOIN " & _
                "dbo.Department ON dbo.WorkOrder.DepartmentID = dbo.Department.ID " & _
                "WHERE (dbo.Location.Name IN (@Location))  "
        If isOffset Then
            sqlString &= "AND (dbo.WorkOrder.DockTime >= @startdate) AND (dbo.WorkOrder.DockTime <= @enddate) "
        Else
            sqlString &= "AND (dbo.WorkOrder.LogDate >= @startdate) AND (dbo.WorkOrder.LogDate <= @enddate) "
        End If
        If txtVendorNumber.Text.Trim() > "" Then
            sqlString &= "AND (dbo.WorkOrder.VendorNumber = @VendorNumber) "
        End If
        sqlString &= "AND (dbo.WorkOrder.Restacks > 0) ORDER BY Date, [Customer Id] "

        dba.CommandText = sqlString
        dba.AddParameter("@location", locaName)
        dba.AddParameter("@startdate", sDate)
        dba.AddParameter("@enddate", eDate)
        If txtVendorNumber.Text.Trim() > "" Then
            dba.AddParameter("@VendorNumber", txtVendorNumber.Text.Trim())
        End If
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception
            Dim err As String = ex.Message
        End Try

        Return dt

    End Function

    Private Function getAverageCostDataSource(ByVal sDate As DateTime, ByVal eDate As DateTime, ByVal locaName As String, ByVal isOffset As Boolean) As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        Dim sqlString As String = String.Empty
        If isOffset Then
            sqlString = "SELECT dbo.Location.Name AS Location, " & _
                "dbo.Vendor.Name AS Customer, " & _
                "dbo.WorkOrder.VendorNumber, " & _
                "COUNT(dbo.WorkOrder.LoadNumber) AS LoadCount,  " & _
                "AVG(dbo.WorkOrder.Amount) AS AvgCost " & _
                "FROM dbo.WorkOrder INNER JOIN " & _
                "dbo.Vendor ON dbo.Vendor.ID = dbo.WorkOrder.CustomerID INNER JOIN " & _
                "dbo.Location ON dbo.Location.ID = dbo.WorkOrder.LocationID " & _
                "WHERE (dbo.Location.Name IN (@Location)) AND (dbo.WorkOrder.DockTime >= @startdate) AND (dbo.WorkOrder.DockTime <= @enddate) " & _
                "GROUP BY dbo.Vendor.Name, dbo.Location.Name, dbo.WorkOrder.VendorNumber "
            If txtVendorNumber.Text.Trim() > "" Then
                sqlString &= "HAVING (dbo.WorkOrder.VendorNumber = @VendorNumber) "
            End If

        Else
            sqlString = "SELECT dbo.Location.Name AS Location, " & _
                "dbo.Vendor.Name AS Customer, " & _
                "dbo.WorkOrder.VendorNumber, " & _
                "COUNT(dbo.WorkOrder.LoadNumber) AS LoadCount,  " & _
                "AVG(dbo.WorkOrder.Amount) AS AvgCost " & _
                "FROM dbo.WorkOrder INNER JOIN " & _
                "dbo.Vendor ON dbo.Vendor.ID = dbo.WorkOrder.CustomerID INNER JOIN " & _
                "dbo.Location ON dbo.Location.ID = dbo.WorkOrder.LocationID " & _
                "WHERE (dbo.Location.Name IN (@Location)) AND (dbo.WorkOrder.LogDate >= @startdate) AND (dbo.WorkOrder.LogDate <= @enddate) " & _
                "GROUP BY dbo.Vendor.Name, dbo.Location.Name, dbo.WorkOrder.VendorNumber "
            If txtVendorNumber.Text.Trim() > "" Then
                sqlString &= "HAVING (dbo.WorkOrder.VendorNumber = @VendorNumber) "
            End If
        End If
        dba.CommandText = sqlString
        dba.AddParameter("@location", locaName)
        dba.AddParameter("@startdate", sDate)
        dba.AddParameter("@enddate", eDate)
        If cbDepartment.SelectedIndex > -1 Then
            dba.AddParameter("@department", cbDepartment.SelectedItem.Text)
        End If
        If txtVendorNumber.Text.Trim() > "" Then
            dba.AddParameter("@VendorNumber", txtVendorNumber.Text.Trim())
        End If
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception
            Dim err As String = ex.Message
        End Try

        Return dt

    End Function

    Private Function getBadPalletReportDataSource(ByVal sDate As DateTime, ByVal eDate As DateTime, ByVal locaName As String, ByVal isOffset As Boolean) As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        Dim sqlString As String = String.Empty
        If isOffset Then
            sqlString = "SELECT dbo.Location.Name AS Location, dbo.WorkOrder.LogDate AS Date, dbo.Department.Name AS Department,  " & _
                "dbo.Vendor.Name AS [Customer Id], dbo.WorkOrder.PurchaseOrder AS [PO #], dbo.WorkOrder.BadPallets AS [Bad Pallets],  " & _
                "dbo.Carrier.Name AS Carrier " & _
                "FROM dbo.WorkOrder INNER JOIN " & _
                "dbo.Vendor ON dbo.WorkOrder.CustomerID = dbo.Vendor.ID INNER JOIN " & _
                "dbo.Carrier ON dbo.WorkOrder.CarrierID = dbo.Carrier.ID INNER JOIN " & _
                "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID INNER JOIN " & _
                "dbo.Department ON dbo.WorkOrder.DepartmentID = dbo.Department.ID " & _
                "WHERE (dbo.Location.Name IN (@Location)) AND (dbo.WorkOrder.DockTime <= @enddate) AND (dbo.WorkOrder.BadPallets > 0) AND  (dbo.WorkOrder.DockTime >= @startdate) "
            If txtVendorNumber.Text.Trim() > "" Then
                sqlString &= "AND (dbo.WorkOrder.VendorNumber = @VendorNumber) "
            End If
            sqlString &= "ORDER BY Date, [Customer Id] "
        Else
            sqlString = "SELECT dbo.Location.Name AS Location, dbo.WorkOrder.LogDate AS Date, dbo.Department.Name AS Department,  " & _
                "dbo.Vendor.Name AS [Customer Id], dbo.WorkOrder.PurchaseOrder AS [PO #], dbo.WorkOrder.BadPallets AS [Bad Pallets],  " & _
                "dbo.Carrier.Name AS Carrier " & _
                "FROM dbo.WorkOrder INNER JOIN " & _
                "dbo.Vendor ON dbo.WorkOrder.CustomerID = dbo.Vendor.ID INNER JOIN " & _
                "dbo.Carrier ON dbo.WorkOrder.CarrierID = dbo.Carrier.ID INNER JOIN " & _
                "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID INNER JOIN " & _
                "dbo.Department ON dbo.WorkOrder.DepartmentID = dbo.Department.ID " & _
                "WHERE (dbo.Location.Name IN (@Location)) AND (dbo.WorkOrder.LogDate <= @enddate) AND (dbo.WorkOrder.BadPallets > 0) AND  (dbo.WorkOrder.LogDate >= @startdate) "
            If txtVendorNumber.Text.Trim() > "" Then
                sqlString &= "AND (dbo.WorkOrder.VendorNumber = @VendorNumber) "
            End If
            sqlString &= "ORDER BY Date, [Customer Id] "
        End If
        dba.CommandText = sqlString
        dba.AddParameter("@location", locaName)
        dba.AddParameter("@startdate", sDate)
        dba.AddParameter("@enddate", eDate)
        If cbDepartment.SelectedIndex > -1 Then
            dba.AddParameter("@department", cbDepartment.SelectedItem.Text)
        End If
        If txtVendorNumber.Text.Trim() > "" Then
            dba.AddParameter("@VendorNumber", txtVendorNumber.Text.Trim())
        End If
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception
            Dim err As String = ex.Message
        End Try

        Return dt

    End Function

    Private Function getLoadsDetailedDataSource(ByVal sDate As DateTime, ByVal eDate As DateTime, ByVal locaName As String, ByVal isOffset As Boolean) As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        Dim sqlString As String = String.Empty
        If isOffset Then
            sqlString = "SELECT dbo.WorkOrder.LogDate AS Date, dbo.Location.Name AS Location, dbo.Department.Name AS Dept,  " & _
                "dbo.LoadType.Name AS LoadType, dbo.Vendor.Name AS CustomerName, dbo.WorkOrder.VendorNumber,  " & _
                "CASE WHEN [ReceiptNumber] > - 1 THEN [ReceiptNumber] ELSE (0) END AS Rect, dbo.WorkOrder.PurchaseOrder, dbo.WorkOrder.Amount,  " & _
                "dbo.Description.Name AS LoadDesc, dbo.Carrier.Name AS Carrier, dbo.WorkOrder.TruckNumber AS Truck, dbo.WorkOrder.TrailerNumber AS Trailer,  " & _
                "CASE WHEN [AppointmentTime] < '1900-01-02 00:00:00.000' THEN '- - -' ELSE CONVERT(varchar(5), AppointmentTime, 108) END AS Appt,  " & _
                "CASE WHEN [GateTime] < '1900-01-02 00:00:00.000' THEN '- - -' ELSE CONVERT(varchar(5), GateTime, 108) END AS Gate, dbo.WorkOrder.DockTime,  " & _
                "dbo.WorkOrder.StartTime, dbo.WorkOrder.CompTime, dbo.WorkOrder.PalletsUnloaded AS PallUnld, dbo.WorkOrder.DoorNumber AS Door,  " & _
                "dbo.WorkOrder.Pieces, dbo.WorkOrder.Weight, dbo.WorkOrder.Restacks, dbo.WorkOrder.PalletsReceived AS PalRecd,  " & _
                "dbo.WorkOrder.BadPallets AS BadPal, dbo.WorkOrder.NumberOfItems AS Items, dbo.WorkOrder.CheckNumber AS [Check], dbo.WorkOrder.BOL,  " & _
                "CASE WHEN [CreatedBy] IS NOT NULL THEN [CreatedBy] ELSE 'Handheld Load' END AS [Created By], dbo.WorkOrder.Comments,  " & _
                "dbo.ufn_UnloaderList(dbo.WorkOrder.ID) AS Unloaders " & _
                "FROM dbo.WorkOrder INNER JOIN " & _
                "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID INNER JOIN " & _
                "dbo.Department ON dbo.WorkOrder.DepartmentID = dbo.Department.ID INNER JOIN " & _
                "dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID INNER JOIN " & _
                "dbo.Vendor ON dbo.WorkOrder.CustomerID = dbo.Vendor.ID INNER JOIN " & _
                "dbo.Description ON dbo.WorkOrder.LoadDescriptionID = dbo.Description.ID INNER JOIN " & _
                "dbo.Carrier ON dbo.WorkOrder.CarrierID = dbo.Carrier.ID " & _
                "WHERE (dbo.Location.Name IN (@Location)) AND (dbo.WorkOrder.DockTime >= @startdate) AND (dbo.WorkOrder.DockTime <= @enddate) "
            If txtVendorNumber.Text.Trim() > "" Then
                sqlString &= "AND (dbo.WorkOrder.VendorNumber = @VendorNumber) "
            End If
            sqlString &= "ORDER BY Date DESC "

        Else
            sqlString = "SELECT dbo.WorkOrder.LogDate AS Date, dbo.Location.Name AS Location, dbo.Department.Name AS Dept,  " & _
                "dbo.LoadType.Name AS LoadType, dbo.Vendor.Name AS CustomerName, dbo.WorkOrder.VendorNumber,  " & _
                "CASE WHEN [ReceiptNumber] > - 1 THEN [ReceiptNumber] ELSE (0) END AS Rect, dbo.WorkOrder.PurchaseOrder, dbo.WorkOrder.Amount,  " & _
                "dbo.Description.Name AS LoadDesc, dbo.Carrier.Name AS Carrier, dbo.WorkOrder.TruckNumber AS Truck, dbo.WorkOrder.TrailerNumber AS Trailer,  " & _
                "CASE WHEN [AppointmentTime] < '1900-01-02 00:00:00.000' THEN '- - -' ELSE CONVERT(varchar(5), AppointmentTime, 108) END AS Appt,  " & _
                "CASE WHEN [GateTime] < '1900-01-02 00:00:00.000' THEN '- - -' ELSE CONVERT(varchar(5), GateTime, 108) END AS Gate, dbo.WorkOrder.DockTime,  " & _
                "dbo.WorkOrder.StartTime, dbo.WorkOrder.CompTime, dbo.WorkOrder.PalletsUnloaded AS PallUnld, dbo.WorkOrder.DoorNumber AS Door,  " & _
                "dbo.WorkOrder.Pieces, dbo.WorkOrder.Weight, dbo.WorkOrder.Restacks, dbo.WorkOrder.PalletsReceived AS PalRecd,  " & _
                "dbo.WorkOrder.BadPallets AS BadPal, dbo.WorkOrder.NumberOfItems AS Items, dbo.WorkOrder.CheckNumber AS [Check], dbo.WorkOrder.BOL,  " & _
                "CASE WHEN [CreatedBy] IS NOT NULL THEN [CreatedBy] ELSE 'Handheld Load' END AS [Created By], dbo.WorkOrder.Comments,  " & _
                "dbo.ufn_UnloaderList(dbo.WorkOrder.ID) AS Unloaders " & _
                "FROM dbo.WorkOrder INNER JOIN " & _
                "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID INNER JOIN " & _
                "dbo.Department ON dbo.WorkOrder.DepartmentID = dbo.Department.ID INNER JOIN " & _
                "dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID INNER JOIN " & _
                "dbo.Vendor ON dbo.WorkOrder.CustomerID = dbo.Vendor.ID INNER JOIN " & _
                "dbo.Description ON dbo.WorkOrder.LoadDescriptionID = dbo.Description.ID INNER JOIN " & _
                "dbo.Carrier ON dbo.WorkOrder.CarrierID = dbo.Carrier.ID " & _
                "WHERE (dbo.Location.Name IN (@Location)) AND (dbo.WorkOrder.LogDate >= @startdate) AND (dbo.WorkOrder.LogDate <= @enddate) "
            If txtVendorNumber.Text.Trim() > "" Then
                sqlString &= "AND (dbo.WorkOrder.VendorNumber = @VendorNumber) "
            End If
            sqlString &= "ORDER BY Date DESC "
        End If
        dba.CommandText = sqlString
        dba.AddParameter("@location", locaName)
        dba.AddParameter("@startdate", sDate)
        dba.AddParameter("@enddate", eDate)
        If cbDepartment.SelectedIndex > -1 Then
            dba.AddParameter("@department", cbDepartment.SelectedItem.Text)
        End If
        If txtVendorNumber.Text.Trim() > "" Then
            dba.AddParameter("@VendorNumber", txtVendorNumber.Text.Trim())
        End If
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception
            Dim err As String = ex.Message
        End Try

        Return dt

    End Function

   ' Private Function getVendorReportDataSource(ByVal sDate As DateTime, ByVal eDate As DateTime, ByVal locaName As String, ByVal isOffset As Boolean) As DataTable
    '     Dim dt As New DataTable
    ' Dim dba As New DBAccess()
    '    Dim sqlString As String = String.Empty
    '    If isOffset Then
    '        sqlString = "SELECT dbo.WorkOrder.LogDate, dbo.Location.Name AS Location, dbo.Department.Name AS Department, dbo.Vendor.Name AS Vendor,  " & _
    '            "dbo.WorkOrder.VendorNumber, dbo.WorkOrder.PalletsUnloaded, dbo.WorkOrder.PalletsReceived, dbo.WorkOrder.StartTime,  " & _
    '            "dbo.WorkOrder.CompTime, dbo.WorkOrder.Pieces, dbo.WorkOrder.Weight, dbo.WorkOrder.LogDate AS startdate, dbo.WorkOrder.LogDate AS enddate,  " & _
    '            "dbo.WorkOrder.PurchaseOrder, dbo.WorkOrder.Amount, dbo.WorkOrder.Restacks, dbo.WorkOrder.BadPallets, dbo.Carrier.Name,  " & _
    '            "dbo.WorkOrder.TrailerNumber, dbo.LoadType.Name AS Type " & _
    '            "FROM dbo.WorkOrder INNER JOIN " & _
    '            "dbo.Carrier ON dbo.WorkOrder.CarrierID = dbo.Carrier.ID INNER JOIN " & _
    '            "dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID LEFT OUTER JOIN " & _
    '            "dbo.Department ON dbo.WorkOrder.DepartmentID = dbo.Department.ID LEFT OUTER JOIN " & _
    '            "dbo.Vendor ON dbo.WorkOrder.CustomerID = dbo.Vendor.ID LEFT OUTER JOIN " & _
    '            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID " & _
    '            "WHERE (dbo.WorkOrder.DockTime >= @startdate) AND (dbo.WorkOrder.DockTime <= @enddate) AND (dbo.Location.Name = (@location)) ORDER BY CustomerID,LogDate "
    '        If cbDepartment.SelectedIndex > -1 Then
    '            sqlString &= "AND (dbo.Department.Name = (@department)) "
    '        End If
    '        If txtVendorNumber.Text.Trim() > "" Then
    '            sqlString &= "AND (dbo.WorkOrder.VendorNumber IN (@VendorNumber)) "
    '        End If
    '    Else
    '        sqlString = "SELECT dbo.WorkOrder.LogDate, dbo.Location.Name AS Location, dbo.Department.Name AS Department, dbo.Vendor.Name AS Vendor,  " & _
    '            "dbo.WorkOrder.VendorNumber, dbo.WorkOrder.PalletsUnloaded, dbo.WorkOrder.PalletsReceived, dbo.WorkOrder.StartTime,  " & _
    '            "dbo.WorkOrder.CompTime, dbo.WorkOrder.Pieces, dbo.WorkOrder.Weight, dbo.WorkOrder.LogDate AS startdate, dbo.WorkOrder.LogDate AS enddate,  " & _
    '            "dbo.WorkOrder.PurchaseOrder, dbo.WorkOrder.Amount, dbo.WorkOrder.Restacks, dbo.WorkOrder.BadPallets, dbo.Carrier.Name,  " & _
    '            "dbo.WorkOrder.TrailerNumber, dbo.LoadType.Name AS Type " & _
    '            "FROM dbo.WorkOrder INNER JOIN " & _
    '            "dbo.Carrier ON dbo.WorkOrder.CarrierID = dbo.Carrier.ID INNER JOIN " & _
    '            "dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID LEFT OUTER JOIN " & _
    '            "dbo.Department ON dbo.WorkOrder.DepartmentID = dbo.Department.ID LEFT OUTER JOIN " & _
    '            "dbo.Vendor ON dbo.WorkOrder.CustomerID = dbo.Vendor.ID LEFT OUTER JOIN " & _
    '            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID " & _
    '            "WHERE (dbo.WorkOrder.LogDate >= @startdate) AND (dbo.WorkOrder.LogDate <= @enddate) AND (dbo.Location.Name = (@location)) ORDER BY CustomerID,LogDate "
    '        If cbDepartment.SelectedIndex > -1 Then
    '            sqlString &= "AND (dbo.Department.Name = (@department)) "
    '        End If
    '        If txtVendorNumber.Text.Trim() > "" Then
    '            sqlString &= "AND (dbo.WorkOrder.VendorNumber IN (@VendorNumber)) "
    '        End If
    '    End If
    '    dba.CommandText = sqlString
    '    dba.AddParameter("@location", locaName)
    '    dba.AddParameter("@startdate", sDate)
    '    dba.AddParameter("@enddate", eDate)
    '    If cbDepartment.SelectedIndex > -1 Then
    '        dba.AddParameter("@department", cbDepartment.SelectedItem.Text)
    '    End If
    '    If txtVendorNumber.Text.Trim() > "" Then
    '        dba.AddParameter("@VendorNumber", txtVendorNumber.Text.Trim())
    '    End If
    '    Try
    '        dt = dba.ExecuteDataSet.Tables(0)
    '    Catch ex As Exception
    '        Dim err As String = ex.Message
    '    End Try
    '    Return dt
    'End Function

#End Region

#Region "Parameter Validation"
    Protected Function ValidateParams() As String
        Dim retString As String = String.Empty
        'cbLocations
        If cbLocations.Visible Then
            If cbLocations.SelectedIndex = -1 Then
                retString &= "Please select Location<br />"
            End If
        End If
        'cbLocationsMS
        'If cbLocationsMS.Visible Then
        '    Dim checkLocations As String = GetSelectedLocas()
        '    If checkLocations.Length = 0 Then
        '        retString &= "Please select Location(s)<br />"
        '    End If
        'End If
        'dpStartDate
        If dpStartDate.Visible Then
            If dpStartDate.SelectedDate Is Nothing Then
                retString &= "Please enter or select Start Date<br />"
            End If
        End If
        'dpEndDate
        If dpEndDate.Visible Then
            If dpEndDate.SelectedDate Is Nothing Then
                retString &= "Please enter or select End Date<br />"
            Else
                If dpEndDate.SelectedDate < dpStartDate.SelectedDate Then
                    retString &= "End Date must be equal to or greater than Start Date<br />"
                End If
            End If
        End If
        'cbDepartment
        'If cbDepartment.Visible Then
        '    If cbDepartment.SelectedIndex = -1 Then
        '        retString &= "Please select Department<br />"
        '    End If
        'End If
        'cbDepartmentMS
        'If cbDepartmentMS.Visible Then
        '    Dim checkLocations As String = GetSelectedDepts()
        '    If checkLocations.Length = 0 Then
        '        retString &= "Please select Department(s)<br />"
        '    End If
        'End If
        'txtVendorNumber NO Validation
        'If txtVendorNumber.Visible Then
        '    retString &= "Please select Location<br />"
        'End If
        Return retString
    End Function
#End Region

    Private Sub cbLocations_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        ReportViewer1.Visible = False
        lblInstructions.Visible = True
        lblReportName.Visible = True
        If cbDepartment.Visible Then
            cbDepartment.Items.Clear()
            cbDepartment.Text = ""
            Dim ldal As New locaDAL
            Dim dt As DataTable = ldal.GetDepartmentsByLocationID(cbLocations.SelectedValue.ToString)
            cbDepartment.DataSource = dt
            cbDepartment.DataTextField = "Name"
            cbDepartment.DataValueField = "ID"
            cbDepartment.DataBind()
            cbDepartment.ClearSelection()
            cbDepartment.EmptyMessage = "Select Department"
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ' set location combobox
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager") Or User.IsInRole("Client")
            'set viewer visibility to false on initial load
            ReportViewer1.Visible = False
            lblInstructions.Visible = True
            SetControls()
        End If
        'always turn of the reports parameter area
        ReportViewer1.ParametersAreaVisible = False
    End Sub

    Private Sub checkQueryString()
        If Request.QueryString("startDate") > "" Then
            If dpStartDate.Visible Then
                dpStartDate.SelectedDate = Request.QueryString("startDate")
            End If

        End If
        If Request.QueryString("endDate") > "" Then
            If dpEndDate.Visible Then
                dpEndDate.SelectedDate = Request.QueryString("endDate")
            End If
        End If
        If Request.QueryString("locaID") > "" Then
            If cbLocations.Visible Then
                '                cbLocations.SelectedItem.Text = Request.QueryString("locaName")
                cbLocations.SelectedValue = Request.QueryString("locaID")
            End If
        End If
        If Request.QueryString("DepartmentName") > "" Then
            If cbDepartment.Visible Then
                cbDepartment.SelectedItem.Text = Request.QueryString("DepartmentName")
                cbDepartment.SelectedValue = Request.QueryString("DepartmentID")
            End If
        End If
        If Request.QueryString("VendorNumber") > "" Then
            If txtVendorNumber.Visible Then
                txtVendorNumber.Text = Request.QueryString("VendorNumber")
            End If
        End If
        If Request.QueryString("startDate") > "" And Request.QueryString("endDate") > "" And Request.QueryString("locaID") > "" Then
            lblInstructions.Visible = False
            lblReportName.Visible = False
            ReportViewer1.Visible = True
            btnRunReport_Click(btnRunReport, Nothing)
        End If
    End Sub

    Private Sub lbSwitch_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles lbSwitch.Command
        Dim lnkString As String = String.Empty
        If dpEndDate.Visible Then
            lnkString = "endDate=" & dpEndDate.SelectedDate & "&"
        End If
        If dpStartDate.Visible Then
            lnkString &= "startDate=" & dpStartDate.SelectedDate & "&"
        End If
        If cbLocations.Visible Then
            If cbLocations.SelectedIndex > -1 Then
                lnkString &= "locaID=" & cbLocations.SelectedValue & "&"
                lnkString &= "locaName=" & cbLocations.SelectedItem.Text & "&"
            Else
                lnkString &= "locaID=&"
                lnkString &= "locaName=&"
            End If
        End If
        If cbDepartment.Visible Then
            If cbDepartment.SelectedIndex > -1 Then
                lnkString &= "locaID=" & cbLocations.SelectedValue & "&"
                lnkString &= "locaName=" & cbLocations.SelectedItem.Text & "&"
            Else
                lnkString &= "DepartmentID=&"
                lnkString &= "DepartmentName=&"
            End If
        Else
            lnkString &= "DepartmentID=&"
            lnkString &= "DepartmentName=&"
        End If
        If txtVendorNumber.Text > "" Then
            lnkString &= "VendorNumber=" & txtVendorNumber.Text.Trim() & "&"
        End If
        Response.Redirect(e.CommandName & "?" & lnkString)
    End Sub


    '        dba.AddParameter("@startdate", sdt)
    '        dba.AddParameter("@enddate", edt)
    '        dba.AddParameter("@location", vLocaName)
    '        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
    '        RadGrid1.DataSource = dt
    '        RadGrid1.DataBind()
    '    End Sub


End Class
