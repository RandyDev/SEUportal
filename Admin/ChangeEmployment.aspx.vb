'Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class ChangeEmployment
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then

            Dim empID As String = Request("empID")
            Dim edal As New empDAL
            Dim emp As New Employee
            emp = edal.GetEmployeeByID(New Guid(empID))
            Dim emplmntList As New List(Of Employment)
            emplmntList = edal.GetEmploymentList(emp.ID)
            emp.Employment = emplmntList(0)
            '********************************************************************************************************
            dp_DateOfEmployment.MinDate = emp.Employment.DateOfEmployment
            dp_DateOfEmployment.SelectedDate = emp.Employment.DateOfEmployment
            '********************************************************************************************************

            Page.Title = emp.rtdsFirstName & " " & emp.rtdsLastName
            dp_DateOfEmployment.Visible = False
            cbPayWeekStartDatesLoad()

            cb_JobTitle.Text = emp.Employment.JobTitle

            num_PayRateHourly.Value = emp.Employment.PayRateHourly
            num_PayRatePercentage.Value = emp.Employment.PayRatePercentage

            num_PayRateHourly.Enabled = True
            num_PayRateHourly.Value = emp.Employment.PayRateHourly
            num_PayRateHourly.EmptyMessage = "$0.00"

            If emp.Employment.PayType = 1 Then  'Percentage
                cb_PayType.Text = "Percentage payment"
                cb_PayType.SelectedValue = 1

                num_PayRatePercentage.Enabled = True
                num_PayRatePercentage.Value = emp.Employment.PayRatePercentage
                num_PayRatePercentage.EmptyMessage = "00.00 %"

            Else
                cb_PayType.Text = "Hourly payment"
                cb_PayType.SelectedValue = 2                '                emp.Employment.PayType = 2  'Hourly

                num_PayRatePercentage.Enabled = False
                num_PayRatePercentage.Value = Nothing
                num_PayRatePercentage.EmptyMessage = " --- "

            End If
            cbLoadJobTitles()
        End If


    End Sub

    Private Sub cbPayWeekStartDates_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbPayWeekStartDates.SelectedIndexChanged
        Dim sdate As Date = cbPayWeekStartDates.SelectedValue
        dp_DateOfEmployment.SelectedDate = sdate

        If e.Text = "Other" Then
            dp_DateOfEmployment.Visible = True
        Else
            dp_DateOfEmployment.Visible = False
        End If

    End Sub
    Private Sub cbLoadJobTitles()
        Dim dba As New DBAccess()
        Dim edal As New empDAL
        Dim empID As Guid = New Guid(Request("empID").ToString)
        Dim emp As Employee = edal.GetEmployeeByID(empID)
        Dim locaid As Guid = emp.LocationID
        Dim ldal As New locaDAL
        Dim dt As DataTable = ldal.GetJobTitlesByLocationID(locaid.tostring)
        cb_JobTitle.DataSource = dt
        cb_JobTitle.DataTextField = "JobTitle"
        cb_JobTitle.DataValueField = "JobTitle"
        cb_JobTitle.DataBind()
        cb_JobTitle.ClearSelection()
    End Sub
    Private Sub cbPayWeekStartDatesLoad()
        Dim tpdal As New TimePuncheDAL
        Dim sdate As Date = tpdal.getPayStartDate(Date.Now())
        Dim today As Date = Date.Now()
        Dim sdate2 As Date = DateAdd(DateInterval.Day, 14, sdate)
        Dim itm As RadComboBoxItem
        Dim minDate As Date = dp_DateOfEmployment.MinDate   'dateofhire on previous employment record

        itm = New RadComboBoxItem
        itm.Text = Format(sdate, "MMM dd, yyyy") & " : THIS pay period"
        itm.Value = sdate
        lblHelpThisPayPeriodStart.Text = itm.Text 'label in help file
        If sdate > minDate Then
            cbPayWeekStartDates.Items.Add(itm)
        End If

        itm = New RadComboBoxItem
        itm.Text = Format(sdate2, "MMM dd, yyyy") & " : NEXT pay period"
        itm.Value = sdate2
        lblHelpNextPayPeriodStart.Text = itm.Text 'label in help file
        If sdate2 > minDate Then
            cbPayWeekStartDates.Items.Add(itm)
        End If

        ' Only allow previous week for the first four 4 days of new pay week
        Dim ddif As Integer = DateDiff(DateInterval.Day, sdate, Date.Now())
        itm = New RadComboBoxItem
        Dim sdatePrev As Date = DateAdd(DateInterval.Day, -14, sdate)
        itm.Text = Format(sdatePrev, "MMM dd, yyyy") & " : PREVIOUS pay period"
        itm.Value = sdatePrev
        lblHelpPreviousPayPeriodStart.Text = itm.Text 'label in help file
        If ddif < 5 And sdatePrev > minDate Then
            cbPayWeekStartDates.Items.Add(itm)
        End If

        'default to Next pay period

        'If DateAdd(DateInterval.Day, 14, sdate2) < minDate Then
        '    cbPayWeekStartDates.Visible = False
        '    dp_DateOfEmployment.Visible = True
        '    lbtnCalendarPayWeek.Visible = False

        'End If

        If cbPayWeekStartDates.Items.Count > 1 Then
            If cbPayWeekStartDates.SelectedValue >= dp_DateOfEmployment.MinDate Then
                cbPayWeekStartDates.SelectedIndex = 1
                dp_DateOfEmployment.SelectedDate = cbPayWeekStartDates.SelectedValue
            End If
        ElseIf cbPayWeekStartDates.Items.Count = 1 Then
            cbPayWeekStartDates.SelectedIndex = 0
            dp_DateOfEmployment.SelectedDate = cbPayWeekStartDates.SelectedValue
        ElseIf cbPayWeekStartDates.Items.Count = 0 Then
            cbPayWeekStartDates.Visible = False
            dp_DateOfEmployment.Visible = True
            lbtnCalendarPayWeek.Visible = False

        End If
        '        itm = New RadComboBoxItem
        '        itm.Text = "Other"
        '        cbPayWeekStartDates.Items.Add(itm)

    End Sub

    Private Sub lbtnCalendarPayWeek_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles lbtnCalendarPayWeek.Command
        If e.CommandName = "Calendar" Then
            lbtnCalendarPayWeek.Text = "show Pay Periods"
            lbtnCalendarPayWeek.CommandName = "PayPeriod"
            dp_DateOfEmployment.Visible = True
            cbPayWeekStartDates.Visible = False

        Else
            lbtnCalendarPayWeek.Text = "show Calendar"
            lbtnCalendarPayWeek.CommandName = "Calendar"
            dp_DateOfEmployment.Visible = False
            cbPayWeekStartDates.Visible = True
        End If
    End Sub

    Protected Sub cb_PayType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cb_PayType.SelectedIndexChanged
        Select Case cb_PayType.SelectedIndex
            Case 0 'Hourly
                num_PayRateHourly.Enabled = True
                num_PayRateHourly.EmptyMessage = "$0.00"
                num_PayRatePercentage.Enabled = False
                num_PayRatePercentage.Value = Nothing
                num_PayRatePercentage.EmptyMessage = " - - - "
                'num_SalaryPay.Enabled = False
                'num_SalaryPay.Value = Nothing
                'num_SalaryPay.EmptyMessage = " - - - "
            Case 1 'Percentage
                num_PayRateHourly.Enabled = True
                num_PayRateHourly.EmptyMessage = "$0.00"
                num_PayRatePercentage.Enabled = True
                num_PayRatePercentage.EmptyMessage = "00.00 %"

                'num_SalaryPay.Enabled = False
                'num_SalaryPay.Value = Nothing
                'num_SalaryPay.EmptyMessage = " - - - "

                'Case 2 'Other
                '    num_PayRateHourly.Enabled = False
                '    num_PayRateHourly.Value = Nothing
                '    num_PayRateHourly.EmptyMessage = " - - - "

                '    num_PayRatePercentage.Enabled = False
                '    num_PayRatePercentage.EmptyMessage = " - - - "
                '    num_PayRatePercentage.Value = Nothing


                '    num_SalaryPay.Enabled = True
                '    num_SalaryPay.EmptyMessage = "$ Weekly"

        End Select
    End Sub

    Private Function validateChanges() As String
        Dim retStr As String = String.Empty
        'cb_JobTitle
        If cb_JobTitle.Text.Trim().Length < 3 Then
            retStr &= "Please select Job Title<br />"
        End If
        'cb_PayType
        If cb_PayType.SelectedIndex < 0 Then
            retStr &= "Please select Pay Type<br />"
        End If

        'cbPayWeekStartDates
        'dp_DateOfEmployment

        Select Case cb_PayType.SelectedValue
            Case 1  'Percentage
                If num_PayRatePercentage.Value = 0 Or num_PayRatePercentage.Value Is Nothing Then
                    retStr &= "You must set a percentage amount. <br />"
                End If
            Case 2  'Hourly
                If num_PayRateHourly.Value = 0 Then
                    retStr &= "You must set an hourly amount. <br />"
                End If
                'num_PayRateHourly
        End Select


        Return retStr
    End Function

    Private Sub btnSaveChanges_Click(sender As Object, e As System.EventArgs) Handles btnSaveChanges.Click

        ' might want to validate form here
        Dim retErrors As String = validateChanges()

        If retErrors.Length > 0 Then
            lblError.Visible = True
            lblError.Text = retErrors

        Else
            lblError.Visible = False

            Dim empID As String = Request("empID")
            Dim edal As New empDAL
            Dim emp As New Employee
            emp = edal.GetEmployeeByID(New Guid(empID))

            Dim emplmntList As New List(Of Employment)
            emplmntList = edal.GetEmploymentList(emp.ID)


            emp.Employment = emplmntList(0)


            'set this employment to stop one prior to startdate
            Dim disdate As Date = dp_DateOfEmployment.SelectedDate
            emp.Employment.DateOfDismiss = DateAdd(DateInterval.Day, -1, disdate)
            '            edal.updateEmployment(emp)

            emp.Employment.ID = Guid.NewGuid()
            emp.Employment.DateOfEmployment = dp_DateOfEmployment.SelectedDate
            emp.Employment.DateOfDismiss = Utilities.getDefaultDateOfDismiss


            emp.Employment.JobTitle = cb_JobTitle.Text
            emp.Employment.PayType = cb_PayType.SelectedValue
            emp.Employment.PayRateHourly = IIf(num_PayRateHourly.Value Is Nothing, 0, num_PayRateHourly.Value)
            emp.Employment.PayRatePercentage = IIf(num_PayRatePercentage.Value Is Nothing, 0, num_PayRatePercentage.Value)

            edal.insertPendingEmployment(emp)


            RadAjaxManager1.ResponseScripts.Add("setReturnArg('Employment:change:" & empID & "');")
        End If 'validator

    End Sub

End Class