Imports Telerik.Web.UI
Public Class MyWork
    Inherits System.Web.UI.Page
    Public endPayPeriod As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim usrdal As New userDAL
        Dim usr As ssUser = usrdal.getUserByName(User.Identity.Name)
        Dim tpdal As New TimePuncheDAL()
        Dim startDate As Date = tpdal.getPayStartDate(Date.Now())
        Dim endDate As Date = DateAdd(DateInterval.Day, 13, startDate)
        lblPayPeriod.Text = "<font size='2'>" & Format(startDate, "ddd: dd MMM yy") & "<font size='1'><em> thru </em></font>" & Format(endDate, "ddd: dd MMM yy") & "</font>"
        'for the help file
        endPayPeriod = Format(endDate, "dddd, MMMM dd") & Utilities.getIntegerSuperScript(Day(endDate))
        If Utilities.IsValidGuid(usr.rtdsEmployeeID.ToString) Then 'we have a div-log employee
            Dim empdal As New empDAL()
            Dim emp As Employee = empdal.GetEmployeeByID(usr.rtdsEmployeeID)
            If emp.Employment.PayType = 1 Or emp.Employment.PayType = 2 Then
                displayCurrPayPeriod(usr.rtdsEmployeeID)
                lblShowTimeSheet.Text = "<span class=""lilBlueButton"" onmouseover=""this.style.cursor='pointer'"" onclick=""toggleTimeSheet();"">toggle Time</span>"
            End If

            If emp.Employment.PayRatePercentage > 0 Then
                lblShowBusiness.Text = "<span class=""lilBlueButton"" onmouseover=""this.style.cursor='pointer'"" onclick=""toggleBusiness();"">toggle Business</span>"
            End If

        End If
        divTimeSheet.Style.Item("display") = "inline"
        divBusiness.Style.Item("display") = "none"
        '        divTempBusiness.Style.Item("display") = "none"
    End Sub
    Private Function buildTimeSheet(ByVal empID As Guid) As String
        Dim resp As String = String.Empty


        Return resp
    End Function

    Public Sub displayCurrPayPeriod(ByVal empid As Guid)
        Dim tpDAL As New TimePuncheDAL()
        Dim ppDate As Date = FormatDateTime(Date.Now(), DateFormat.ShortDate)
        Dim curTimeCard As List(Of TimePunche) = tpDAL.getTimePunchesByEmpIDandPayPeriod(empid, ppDate)
        Dim pps As Date = tpDAL.getPayStartDate(ppDate)
        Dim ttlpptime As String = String.Empty
        lblcwk.Text = Format(pps, "MMM dd ") & " <font style='font-weight:normal;' size='1'>thru</font> " & Format(DateAdd(DateInterval.Day, 13, pps), "MMM dd")
        If curTimeCard.Count > 0 Then
            Dim ctbl As String = BuildTimePunche(curTimeCard, ttlpptime)
            lblttlcptime.Text = ttlpptime
            lblCurpp.Text = ctbl
        End If
    End Sub

    Public Function BuildTimePunche(ByVal tpList As List(Of TimePunche), ByRef ttlpptime As String) As String
        Dim dp As String = String.Empty
        Dim ttlMins1 As Integer = 0
        Dim ttlMins2 As Integer = 0
        For Each tp As TimePunche In tpList
            Dim isclosed As String = IIf(tp.IsClosed, "#aaaaaa", "orange")
            dp &= "<td valign=""top"" style=""width:75px;padding-left:5px;"">" & Format(tp.DateWorked, "ddd dd MMM") & " <br><span style='font-size:12px;color:" & isclosed & ";'>" & tp.DepartmentName & "</span><br />"
            Dim surrogate As Date = "1/1/1900"
            If Not tp.tpList Is Nothing Then
                Dim tpdal As New TimePuncheDAL
                For Each tio As TimeInOut In tp.tpList
                    Dim tioHours As String = String.Empty
                    dp &= Format(tio.TimeIn, "hh:mm tt") & " <br>"
                    Dim dd As Integer = 0
                    Dim sTimeIn As DateTime = FormatDateTime(tio.TimeIn, DateFormat.ShortDate) & " " & FormatDateTime(tio.TimeIn, DateFormat.ShortTime)
                    Dim sTimeOut As DateTime = FormatDateTime(tio.TimeOut, DateFormat.ShortDate) & " " & FormatDateTime(tio.TimeOut, DateFormat.ShortTime)
                    If sTimeOut > surrogate Then
                        dp &= Format(sTimeOut, "hh:mm tt")
                    Else
                        dp &= "<span style=""color:red;font-size:11px;""> clocked in </span>"
                        sTimeOut = Date.Now()
                    End If
                    dd = DateDiff(DateInterval.Minute, sTimeIn, sTimeOut)

                    If tp.DateWorked < DateAdd(DateInterval.Day, 7, tpdal.getPayStartDate(tp.DateWorked)) Then
                        'week one
                        ttlMins1 += dd
                    Else
                        'week2
                        ttlMins2 += dd
                    End If


                    Dim numModMins As Integer = dd Mod 60
                    Dim numHours As Integer = (dd - numModMins) / 60
                    Dim ddmins As String = ""
                    If numModMins < 10 Then
                        ddmins = "0" & numModMins.ToString
                    Else
                        ddmins = numModMins.ToString
                    End If
                    If ddmins.Length = 1 Then ddmins = "0" & ddmins
                    Dim numHoursColor As String = "#000000"
                    Select Case numHours
                        Case Is > 5
                            numHoursColor = "Red"
                        Case Is > 4
                            numHoursColor = "Orange"
                    End Select
                    dp &= "<span style=""font-size:10px;""><center><span style=""color:" & numHoursColor & ";"">[</span>" & numHours.ToString & ":" & ddmins & "<span style=""color:" & numHoursColor & ";"">]</span></center></span>"

                Next
            End If
            dp &= "</td>"

            If Not tp.tpList Is Nothing Then
                Dim tpdal As New TimePuncheDAL

                If ttlMins2 > 0 Then
                    ttlpptime = "This Week: "
                    Dim ttlhrs2 As String = CType((ttlMins2 - ttlMins2 Mod 60) / 60, Integer).ToString.Trim()
                    Dim ttlmodmins2 As String = (ttlMins2 Mod 60).ToString.Trim()
                    If ttlmodmins2.Length = 1 Then ttlmodmins2 = "0" & ttlmodmins2
                    ttlpptime &= ttlhrs2 & "hr&nbsp;" & ttlmodmins2 & "min  - "
                End If

                Dim ttlhrs1 As String = CType((ttlMins1 - ttlMins1 Mod 60) / 60, Integer).ToString.Trim()
                Dim ttlmodmins1 As String = (ttlMins1 Mod 60).ToString.Trim()
                If ttlmodmins1.Length = 1 Then ttlmodmins1 = "0" & ttlmodmins1

                If Date.Now < DateAdd(DateInterval.Day, 7, tpdal.getPayStartDate(tp.DateWorked)) Then
                    ttlpptime = "This Week: "
                Else
                    ttlpptime &= "Last Week: "
                End If
                ttlpptime &= ttlhrs1 & "hr&nbsp;" & ttlmodmins1 & "min "




                If tp.DateWorked < tpdal.getPayStartDate(Date.Now()) Then
                    Dim ttlmins As Integer = ttlMins1 + ttlMins2
                    Dim ttlhrs As String = CType((ttlmins - ttlmins Mod 60) / 60, Integer).ToString.Trim()
                    Dim ttlmodmins As String = (ttlmins Mod 60).ToString.Trim()
                    If ttlmodmins.Length = 1 Then ttlmodmins = "0" & ttlmodmins
                    ttlpptime = ttlhrs & "hr&nbsp;" & ttlmodmins & "min "
                End If


            End If
        Next
        Return dp
    End Function

    Private Sub RadGrid1_CustomAggregate(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCustomAggregateEventArgs) Handles RadGrid1.CustomAggregate
        Dim ttlBus As Double = 0
        Dim payPercent As Double = 0
        For Each index As GridDataItem In RadGrid1.MasterTableView.Items
            Dim Amount As Double = 0
            Dim lunl As Integer = 0
            Amount = DirectCast(index.GetDataKeyValue("Amount"), Double)
            lunl = DirectCast(index.GetDataKeyValue("ulCount"), Integer)
            payPercent = DirectCast(index.GetDataKeyValue("PayRatePercentage"), Double)
            ttlBus = ttlBus + (Amount / lunl)
        Next
        e.Result = "Total: " & FormatCurrency(ttlBus, 2).ToString & "<br />Gross Payout: " & FormatCurrency(ttlBus * (payPercent / 100)).ToString
    End Sub


    Private Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim tpdal As New TimePuncheDAL
        Dim usr As MembershipUser = Membership.GetUser()
        Dim rtdsID As String = Utilities.getRTDSidByUserID(usr.ProviderUserKey.ToString)
        Dim edal As New empDAL
        Dim emp As Employee = edal.GetEmployeeByID(New Guid(rtdsID))

        If Not emp.Employment Is Nothing Then
            If Not emp.Employment.PayRatePercentage > 0 Then
                RadGrid1.Visible = False
            Else
                RadGrid1.Visible = True

                Dim currentPayPeriodStartDate As Date = tpdal.getPayStartDate(FormatDateTime(Date.Now(), DateFormat.ShortDate))
                '            Dim curdate As Date = DateAdd(DateInterval.Day, -32, Date.Now)
                '            Dim currentPayPeriodStartDate As Date = tpdal.getPayStartDate(FormatDateTime(curdate, DateFormat.ShortDate))

                Dim currentPayPeriodEndDate As Date = DateAdd(DateInterval.Day, 14, currentPayPeriodStartDate)

                Dim previousPayPeriodStartDate As Date = tpdal.getPayStartDate(DateAdd(DateInterval.Day, -1, currentPayPeriodStartDate))
                Dim previousPayPeriodEndDate As Date = DateAdd(DateInterval.Day, 14, previousPayPeriodStartDate)

                Dim ldal As New locaDAL()
                Dim offset As Integer = ldal.getLocaBDOffset(emp.LocationID)
                If offset <> 0 Then
                    currentPayPeriodStartDate = DateAdd(DateInterval.Hour, offset, currentPayPeriodStartDate)
                    currentPayPeriodEndDate = DateAdd(DateInterval.Hour, offset, currentPayPeriodEndDate)
                End If
                Dim dba As New DBAccess()

                'dba.CommandText = "SELECT WO.LogDate, WO.DoorNumber, WO.PurchaseOrder, " & _
                '    "(SELECT COUNT(EmployeeID) AS ulCount " & _
                '    "FROM Unloader AS Un " & _
                '    "WHERE (LoadID = WO.ID)) AS ulCount, WO.Amount, WO.LoadNumber, E.ID, Employment.PayRatePercentage " & _
                '    "FROM Employee AS E INNER JOIN " & _
                '    "Unloader AS U ON E.ID = U.EmployeeID INNER JOIN " & _
                '    "Employment ON E.ID = Employment.EmployeeID LEFT OUTER JOIN " & _
                '    "WorkOrder AS WO ON U.LoadID = WO.ID " & _
                '    "WHERE (U.EmployeeID = @empID) AND (WO.LogDate >= @startDate) AND (WO.LogDate <= @endDate) " & _
                '    "ORDER BY WO.LogDate, WO.DoorNumber "

                Dim strSQL As String = String.Empty
                strSQL = "SELECT dbo.Employee.ID AS empID, dbo.WorkOrder.LogDate, dbo.WorkOrder.PurchaseOrder, dbo.WorkOrder.DoorNumber, " & _
                    "(SELECT COUNT(EmployeeID) AS Count " & _
                    "FROM   dbo.Unloader " & _
                    "WHERE (LoadID = dbo.WorkOrder.ID)) AS ulCount,  " & _
                    "CASE WHEN dbo.WorkOrder.CheckNumber > '' THEN dbo.WorkOrder.Amount - " & _
                    "(SELECT CheckCharge " & _
                    "FROM   dbo.Location " & _
                    "WHERE dbo.workorder.LocationID = dbo.location.ID) - " & _
                    "(SELECT AdministrativeFee " & _
                    "FROM   dbo.Location " & _
                    "WHERE dbo.workorder.LocationID = dbo.location.ID) - " & _
                    "(SELECT CustomerFee " & _
                    "FROM   dbo.Location " & _
                    "WHERE dbo.workorder.LocationID = dbo.location.ID) WHEN dbo.WorkOrder.CheckNumber = '' AND dbo.workorder.loadtypeid = 'D62DA4A5-FD15-4460-B62F-BAA83ACE65FD' OR " & _
                    "dbo.WorkOrder.CheckNumber = '' AND dbo.workorder.loadtypeid = '6144C1A1-3657-4D91-A50A-F107C3A41847' THEN dbo.WorkOrder.Amount - " & _
                    "(SELECT AdministrativeFee " & _
                    "FROM   dbo.Location " & _
                    "WHERE dbo.workorder.LocationID = dbo.location.ID) - " & _
                    "(SELECT CustomerFee " & _
                    "FROM   dbo.Location " & _
                    "WHERE dbo.workorder.LocationID = dbo.location.ID) ELSE dbo.WorkOrder.Amount END AS Amount,  " & _
                    "CASE WHEN [Amount] > 0 THEN (CASE WHEN dbo.WorkOrder.CheckNumber = '' THEN dbo.WorkOrder.Amount ELSE dbo.WorkOrder.Amount - " & _
                    "(SELECT CheckCharge " & _
                    "FROM   dbo.Location " & _
                    "WHERE ID = dbo.WorkOrder.LocationID) END / " & _
                    "(SELECT COUNT(dbo.Unloader.EmployeeID) AS ulCount " & _
                    "FROM   dbo.Unloader " & _
                    "WHERE dbo.Unloader.LoadID = dbo.WorkOrder.ID)) ELSE (0) END AS ulAmount, dbo.WorkOrder.ID, dbo.Employment.PayRatePercentage  " & _
                    "FROM  dbo.WorkOrder INNER JOIN " & _
                    "dbo.Unloader AS Unloader_1 ON dbo.WorkOrder.ID = Unloader_1.LoadID INNER JOIN " & _
                    "dbo.Employee ON Unloader_1.EmployeeID = dbo.Employee.ID INNER JOIN " & _
                    "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID AND dbo.WorkOrder.LocationID = dbo.Location.ID INNER JOIN " & _
                    "dbo.Employment ON dbo.Employee.ID = dbo.Employment.EmployeeID AND dbo.WorkOrder.StartTime >= @startdate AND  " & _
                    "dbo.WorkOrder.StartTime < @enddate AND dbo.Employee.ID = @empID " & _
                    "WHERE (dbo.Employment.DateOfEmployment <= @startdate) AND (dbo.Employment.DateOfDismiss >= @startdate) ORDER BY LogDate"
                dba.CommandText = strSQL
                dba.AddParameter("@empID", rtdsID)
                dba.AddParameter("@startDate", currentPayPeriodStartDate)
                dba.AddParameter("@endDate", currentPayPeriodEndDate)
                Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
                RadGrid1.DataSource = dt

            End If

        Else
            RadGrid1.Visible = False
        End If

    End Sub



End Class