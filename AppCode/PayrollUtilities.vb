Imports System.Data.SqlClient
'Imports DiversifiedLogistics.PayrollReportAdminObj

Public Class PayrollUtilities

    Public Function getEmployeeBusiness(ByVal empID As Guid, ByVal sdate As Date, ByVal edate As Date) As Decimal
        Dim tpdal As New TimePuncheDAL
        '        sdate = tpdal.getPayStartDate(sdate)
        '        edate = DateAdd(DateInterval.Day, 14, sdate)
        Dim lDAL As New locaDAL()
        Dim locaOffset As Integer = lDAL.getLocaOffset(empID)
        If locaOffset <> 0 Then
            sdate = DateAdd(DateInterval.Hour, locaOffset, sdate)
            edate = DateAdd(DateInterval.Hour, locaOffset, edate)
        End If
        Dim strSql As String = String.Empty
        strSql = "SELECT CASE WHEN [Amount] > 0 THEN (CASE WHEN dbo.WorkOrder.CheckNumber = ''  " & _
            "THEN dbo.WorkOrder.Amount ELSE dbo.WorkOrder.Amount - " & _
            "(SELECT CheckCharge FROM dbo.Location " & _
            "WHERE ID = dbo.WorkOrder.LocationID) END / " & _
            "(SELECT COUNT(dbo.Unloader.EmployeeID) AS ulCount " & _
            "FROM dbo.Unloader " & _
            "WHERE dbo.Unloader.LoadID = dbo.WorkOrder.ID)) ELSE (0) END AS ulAmount " & _
            "FROM dbo.WorkOrder INNER JOIN " & _
            "dbo.Unloader AS Unloader_1 ON dbo.WorkOrder.ID = Unloader_1.LoadID INNER JOIN " & _
            "dbo.Employee ON Unloader_1.EmployeeID = dbo.Employee.ID INNER JOIN " & _
            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID  " & _
            "AND dbo.WorkOrder.LocationID = dbo.Location.ID "
        If locaOffset <> 0 Then
            strSql &= "WHERE (dbo.WorkOrder.StartTime >= @sdate) AND (dbo.WorkOrder.StartTime < @edate) "
        Else
            strSql &= "WHERE (dbo.WorkOrder.LogDate >= @sdate) AND (dbo.WorkOrder.LogDate < @edate) "
        End If
        strSql &= "AND (dbo.Employee.ID = @empID) "

        Dim dba As New DBAccess
        dba.CommandText = strSql
        dba.AddParameter("@empID", empID)
        dba.AddParameter("@sdate", sdate)
        dba.AddParameter("@edate", edate)
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        Dim dec As Decimal = 0
        For Each rw As DataRow In dt.Rows
            If Not IsDBNull(rw.Item("ulAmount")) Then
                dec += rw.Item("ulAmount")
            Else
                dec = 0
            End If
        Next

        Return Math.Round(dec, 2)

    End Function

    Public Function getAdminReport(ByVal locaID As Guid, ByVal sdate As Date, Optional ByVal edate As Date = Nothing) As List(Of PayrollReportAdminObj)
        'strip the time to 0:0
        sdate = FormatDateTime(sdate, DateFormat.ShortDate)
        edate = FormatDateTime(edate, DateFormat.ShortDate)
        Dim praList As New List(Of PayrollReportAdminObj)   'create empty list
        Dim tpDAL As New TimePuncheDAL()

        'Get list of employees that are working here right now
        'should be list of people that are home based here
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT Employee.ID as empID, Employee.FirstName, Employee.LastName, Employee.Login, Employment.PayRateHourly, Employment.PayRatePercentage,  " & _
            "employment.specialpay, employment.holidaypay " & _
            "FROM Employee INNER JOIN " & _
            "Employment ON Employee.ID = Employment.EmployeeID " & _
            "where employee.HomeLocationID = @locaID and employment.dateofdismiss > @sdate " & _
            "order by PayRatePercentage desc, LastName "
        dba.AddParameter("@locaID", locaID)
        dba.AddParameter("@sdate", sdate)
        Dim dt As DataTable = dba.ExecuteDataSet().Tables(0)
        For Each row As DataRow In dt.Rows
            Dim pra As New PayrollReportAdminObj
            pra.ID = row.Item("empID")
            pra.empName = row.Item("FirstName") & ", " & row.Item("LastName")
            pra.EmpNumber = row.Item("Login")
            pra.PayRateHourly = row.Item("PayRateHourly")
            pra.PayRatePercentage = row.Item("PayRatePercentage")
            pra.AddCompAmount = row.Item("AddCompAmount")
            pra.ulAmount = pra.ulAmount
            pra.GrossPay = 0
            praList.Add(pra) 'adds the pra to the list
        Next
        Return praList  'returns a list of PayrollAdminReportObj
    End Function

    'Public Sub setTime(ByRef pra As PayrollReportAdminObj, ByVal sdate As Date, ByVal edate As Date)
    '    Dim tpDAL As New TimePuncheDAL()
    '    Dim arPay() As Decimal = tpDAL.getEmployeeTime(pra.EmployeeRTDSID, sdate, edate)
    '    '0 = reg hrs wk 1 : 1 = ot hrs wk 1 : 2 reg hrs wk 2 : 3 = ot hrs wk 2 : 4 = hrly hours wk 1 : 5 = hrly hours wk 2
    '    pra.TotalHours = arPay(0) + arPay(2)
    '    pra.OTHours = arPay(1) + arPay(3)
    '    pra.TotalHoursHourly = arPay(4) + arPay(5)

    '    If pra.PercentageRate > 0 Then
    '        pra.LoadPay = Math.Round(pra.Business * (pra.PercentageRate * 0.01), 2)
    '        If pra.TotalHours > 0 Then
    '            pra.HalfTimePay = Math.Round((pra.LoadPay + pra.SpecialPay) / pra.TotalHours / 2, 2)
    '        Else
    '            pra.HalfTimePay = 0
    '        End If
    '        pra.PercentageOTPay = Math.Round(pra.OTHours * pra.HalfTimePay, 2)
    '        pra.GrossPay = Math.Round(pra.LoadPay + pra.HolidayPay + pra.SpecialPay + pra.PercentageOTPay, 2)
    '    End If

    '    If pra.TotalHoursHourly > 0 Then
    '        If pra.PercentageRate = 0 Then
    '            pra.OTPay = Math.Round(pra.OTHours * (pra.HourlyRate * 1.5), 2)
    '            '''' minus ot hours?
    '            pra.RegHoursPay = Math.Round((pra.TotalHoursHourly - pra.OTHours) * pra.HourlyRate, 2)
    '            pra.GrossPay = Math.Round(pra.RegHoursPay + pra.OTPay + pra.HolidayPay + pra.SpecialPay, 2)
    '        Else
    '            pra.OTPay = 0 'Math.Round(pra.OTHours * (pra.HourlyRate * 1.5), 2)
    '            pra.RegHoursPay = Math.Round((pra.TotalHoursHourly - pra.OTHours) * pra.HourlyRate, 2)
    '            pra.GrossPay = Math.Round(pra.RegHoursPay + pra.OTPay + pra.HolidayPay + pra.SpecialPay, 2)
    '        End If
    '    End If

    'End Sub

    Public Function getPayrollReportAdminObjList(ByVal loca As String, ByVal startDate As Date, ByVal endDate As Date) As List(Of PayrollReportAdminObj)
        Dim plist As New List(Of PayrollReportAdminObj)
        Dim dt As DataTable = getPayrollByLocationAndDateRange(loca, startDate, endDate)
        If dt.Rows.Count > 0 Then

            For Each row As DataRow In dt.Rows
                Dim pra As New PayrollReportAdminObj
                pra.ID = row.Item("ID")
                pra.EmpName = row.Item("EmpName")
                
                pra.EmpNumber = row.Item("EmpNumber")
                pra.PayRateHourly = IIf(IsDBNull(row.Item("PayRateHourly")), 0, row.Item("PayRateHourly"))
                pra.PayRatePercentage = IIf(IsDBNull(row.Item("PayRatePercentage")), 0, row.Item("PayRatePercentage"))
                pra.ulAmount = row.Item("ulAmount")
                pra.PercentHours = row.Item("PercentHours")
                pra.AddCompAmount = row.Item("AddCompAmount")
                pra.HourlyHours = row.Item("HourlyHours")
                pra.TotalHours = row.Item("TotalHours")
                pra.OTHours = row.Item("OTHours")
                pra.RegularPay = row.Item("RegularPay")
        '            pra.OTPay = row.item("xx")
                pra.ulAmount = IIf(IsDBNull(row.Item("ulAmount")), 0, row.Item("ulAmount"))
                If pra.TotalHours > 40 Then
                    pra.HalfTimePay = Math.Round((pra.TotalHours - 40) * (pra.RegularPay / pra.TotalHours * 0.5), 2)
                Else
                    pra.HalfTimePay = 0
                End If

                pra.GrossPay = pra.RegularPay + pra.HalfTimePay
                plist.Add(pra) 'adds the pra to the list
            Next

        End If





        Return plist
    End Function




    Public Function getPayrollByLocationAndDateRange(ByVal loca As String, ByVal sDate As Date, ByVal eDate As Date) As DataTable
        Dim dt As New DataTable
        Dim sql As String = "DECLARE @tblPayrollEmployees TABLE (ID uniqueidentifier) INSERT INTO @tblPayrollEmployees SELECT dbo.TimePunche.EmployeeID FROM dbo.Location  " & _
"INNER JOIN dbo.TimePunche ON dbo.Location.ID = dbo.TimePunche.LocationID INNER JOIN dbo.TimeInOut ON dbo.TimePunche.ID = dbo.TimeInOut.TimepuncheID " & _
"INNER JOIN dbo.Employee ON dbo.TimePunche.EmployeeID = dbo.Employee.ID WHERE (dbo.TimePunche.DateWorked >= @startdate)AND (dbo.TimePunche.DateWorked <= @enddate)  " & _
"AND (dbo.Location.Name = @location)GROUP BY dbo.TimePunche.EmployeeID, dbo.Employee.FirstName HAVING (dbo.Employee.FirstName <> 'Truck')ORDER BY dbo.TimePunche.EmployeeID  " & _
"DECLARE @PercentHours TABLE (ID uniqueidentifier, PercentHours Decimal(8,2)) " & _
"INSERT @PercentHours (ID, PercentHours) SELECT dbo.TimePunche.EmployeeID, SUM(dbo.TimeInOut.HoursWorked) AS pHW  " & _
"FROM dbo.Location INNER JOIN dbo.TimePunche ON dbo.Location.ID = dbo.TimePunche.LocationID INNER JOIN dbo.TimeInOut ON dbo.TimePunche.ID = dbo.TimeInOut.TimepuncheID " & _
"WHERE (dbo.Location.Name = @location) AND (dbo.TimePunche.DateWorked >= @startdate) AND (dbo.TimePunche.DateWorked <= @enddate) " & _
"AND (dbo.TimeInOut.IsHourly = 0) GROUP BY dbo.TimePunche.EmployeeID " & _
"DECLARE @HourlyHours TABLE (ID uniqueidentifier, HourlyHours Decimal(8,2)) INSERT @HourlyHours (ID, HourlyHours)   " & _
"SELECT dbo.TimePunche.EmployeeID, SUM(dbo.TimeInOut.HoursWorked) AS hHW FROM dbo.Location   " & _
"INNER JOIN dbo.TimePunche ON dbo.Location.ID = dbo.TimePunche.LocationID INNER JOIN dbo.TimeInOut ON dbo.TimePunche.ID = dbo.TimeInOut.TimepuncheID  " & _
"WHERE (dbo.Location.Name = @location) AND (dbo.TimePunche.DateWorked >= @startdate) AND (dbo.TimePunche.DateWorked <= @enddate) AND (dbo.TimeInOut.IsHourly = 1)  " & _
"GROUP BY dbo.TimePunche.EmployeeID  " & _
"DECLARE @tblEmployeeHours TABLE (ID uniqueidentifier, PercentHours Decimal(8,2), HourlyHours Decimal(8,2) )   " & _
"INSERT INTO @tblEmployeeHours(ID, PercentHours, HourlyHours)  " & _
"SELECT tblPayrollEmployees.ID, CASE WHEN PercentHours.PercentHours >0 THEN PercentHours.PercentHours ELSE 0.00 END AS PercentHours, CASE WHEN HourlyHours.HourlyHours >0  " & _
"THEN HourlyHours.HourlyHours ELSE 0.00 END AS HourlyHours   " & _
"FROM @tblPayrollEmployees AS tblPayrollEmployees LEFT OUTER JOIN @PercentHours AS PercentHours ON tblPayrollEmployees.ID = PercentHours.ID  " & _
"LEFT OUTER JOIN @HourlyHours AS HourlyHours ON tblPayrollEmployees.ID = HourlyHours.ID   " & _
"DECLARE @tblEmployeeRates TABLE (ID uniqueidentifier,EmpName varchar(50),EmpNumber varchar(50),PayType int, PayRateHourly Decimal(6,2), PayRatePercentage Decimal(6,2))        " & _
"INSERT INTO @tblEmployeeRates SELECT dbo.Employee.ID, dbo.Employee.LastName + '  ' + dbo.Employee.FirstName AS EmpName, dbo.Employee.Login AS EmpNum, dbo.Employment.PayType, dbo.Employment.PayRateHourly, dbo.Employment.PayRatePercentage   " & _
"FROM dbo.Employee LEFT OUTER JOIN dbo.Employment ON dbo.Employee.ID = dbo.Employment.EmployeeID WHERE (NOT (dbo.Employee.FirstName + '  ' + dbo.Employee.LastName + '  ' + dbo.Employee.Login LIKE 'Truck' + '%'))   " & _
"AND (dbo.Employment.DateOfEmployment <= @startdate) AND (dbo.Employment.DateOfDismiss >= @enddate) OR (NOT (dbo.Employee.FirstName + '  ' + dbo.Employee.LastName + '  ' + dbo.Employee.Login LIKE 'Truck' + '%'))   " & _
"AND (dbo.Employment.DateOfEmployment >= @startdate) AND (@enddate BETWEEN dbo.Employment.DateOfEmployment AND dbo.Employment.DateOfDismiss)   " & _
"OR (NOT (dbo.Employee.FirstName + '  ' + dbo.Employee.LastName + '  ' + dbo.Employee.Login LIKE 'Truck' + '%')) AND (dbo.Employment.DateOfEmployment <= @startdate)   " & _
"AND (@startdate BETWEEN dbo.Employment.DateOfEmployment AND dbo.Employment.DateOfDismiss)  " & _
"DECLARE @tblLoadInfo TABLE (woid uniqueidentifier,EmployeeId uniqueidentifier,Amount decimal(8,2),CheckNumber varchar(50),CheckCharge decimal(8,2))  " & _
"INSERT INTO @tblLoadInfo SELECT dbo.WorkOrder.ID AS woid, Unloader.EmployeeID, dbo.WorkOrder.Amount, dbo.WorkOrder.CheckNumber, dbo.Location.CheckCharge  " & _
"FROM dbo.WorkOrder INNER JOIN dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID   " & _
"LEFT OUTER JOIN dbo.Unloader AS Unloader ON dbo.WorkOrder.ID = Unloader.LoadID WHERE (dbo.Location.Name = @location)   " & _
"AND (dbo.WorkOrder.DockTime >= @startdate) AND (dbo.WorkOrder.DockTime <= @enddate) AND (NOT (Unloader.EmployeeID IN (SELECT ID FROM dbo.Employee WHERE (FirstName = 'Truck'))))  " & _
"DECLARE @tblUnlCount TABLE (woid uniqueidentifier,empcount int)   " & _
"INSERT INTO @tblUnlCount SELECT dbo.WorkOrder.ID AS woid, COUNT(Unloader.EmployeeID) AS empcount   " & _
"FROM dbo.WorkOrder INNER JOIN dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID   " & _
"LEFT OUTER JOIN dbo.Unloader AS Unloader ON dbo.WorkOrder.ID = Unloader.LoadID WHERE (dbo.WorkOrder.DockTime >= @startdate)   " & _
"AND (dbo.WorkOrder.DockTime <= @enddate) AND (dbo.Location.Name = @location) GROUP BY dbo.WorkOrder.ID  " & _
"DECLARE @tblLoadAmount TABLE (woid uniqueidentifier,EmployeeId uniqueidentifier,Amount decimal(8,2),CheckNumber varchar(50),empcount int,CheckCharge decimal (8,2),ulAmount decimal (8,3))  " & _
"INSERT INTO @tblLoadAmount SELECT tblLoadInfo.woid, EmployeeId, Amount, CheckNumber, empcount, CheckCharge, CASE WHEN CheckNumber = '' THEN Amount/empcount ELSE (Amount-CheckCharge)/empcount END AS ulAmount   " & _
"FROM @tblLoadInfo AS tblLoadInfo LEFT OUTER JOIN @tblUnlCount AS tblUnlCount ON tblUnlCount.woid=tblLoadInfo.woid  " & _
"DECLARE @tblLoadsSum TABLE (EmployeeId uniqueidentifier,ulAmount decimal (8,2))   " & _
"INSERT INTO @tblLoadsSum SELECT  EmployeeId, SUM(ulAmount)FROM @tblLoadAmount GROUP BY EmployeeId  " & _
"DECLARE @tbladditional TABLE (EmployeeID uniqueidentifier,AddCompAmount decimal(8,2))   " & _
"INSERT INTO @tbladditional SELECT dbo.AdditionalComp.EmployeeID, CASE WHEN Credit = 1 THEN AddCompAmount ELSE AddCompAmount - AddCompAmount - AddCompAmount END AS AddCompAmount   " & _
"FROM dbo.AdditionalComp INNER JOIN dbo.AddCompDesc ON dbo.AdditionalComp.AddCompDescriptionID = dbo.AddCompDesc.AddCompDescriptionID   " & _
"WHERE (dbo.AdditionalComp.AddCompStartDate >= @startdate) AND (dbo.AdditionalComp.AddCompEndDate <= @enddate)  " & _
"DECLARE @tbladditionalcomp TABLE (EmployeeID uniqueidentifier,AddCompAmount decimal(8,2))   " & _
"INSERT INTO @tbladditionalcomp SELECT EmployeeID,SUM(AddCompAmount)FROM @tbladditional GROUP BY EmployeeID  " & _
"DECLARE @tblTotals TABLE(ID uniqueidentifier,EmpName varchar(50),EmpNumber varchar(50),ulAmount decimal(8,2), PercentHours decimal(8,2), HourlyHours decimal(8,2),TotalHours decimal(8,2),OThours decimal(8,2), PayType int, PayRateHourly decimal(8,2), PayRatePercentage decimal(8,2),AddCompAmount decimal(8,2),RegularPay decimal(8,2))  " & _
"INSERT INTO @tblTotals SELECT tblEmployeeHours.ID, EmpName, EmpNumber, CASE WHEN ulAmount>0 THEN ulAmount ELSE 0.00 END AS ulAmount, PercentHours, HourlyHours, PercentHours + HourlyHours AS TotalHours,  " & _
"CASE WHEN PercentHours + HourlyHours >40 THEN PercentHours + HourlyHours -40 ELSE 0.00 END AS OThours, PayType, PayRateHourly, PayRatePercentage, CASE WHEN AddCompAmount>0  " & _
"THEN AddCompAmount ELSE 0.00 END AS AddCompAmount, ((HourlyHours + PercentHours) * PayRateHourly)+(case when ulAmount > 0 then ulamount else 0.00 end *(PayRatePercentage*.01))+   " & _
"CASE WHEN AddCompAmount>0 THEN AddCompAmount ELSE 0.00 END AS RegularPay FROM @tblEmployeeHours AS tblEmployeeHours   " & _
"LEFT OUTER JOIN @tblEmployeeRates AS tblEmployeeRates ON tblEmployeeRates.ID = tblEmployeeHours.ID  " & _
"LEFT OUTER JOIN @tblLoadsSum AS tblLoadsSum ON tblLoadsSum.EmployeeId = tblEmployeeHours.ID  " & _
"LEFT OUTER JOIN @tbladditionalcomp AS tbladditionalcomp ON tbladditionalcomp.EmployeeID = tblEmployeeHours.ID ORDER BY PayType, EmpNumber " & _
"SELECT * FROM @tblTotals ORDER BY PayType, EmpNumber "




        '        Dim dba As New DBAccess
        '        dba.CommandText = sql
        '        dba.AddParameter("@startdate", sDate)
        '        dba.AddParameter("@enddate", eDate)
        '        dba.AddParameter("@location", loca)
        sql = "SELECT * from dbo.tblfunc_payrollDateRangeAndLocation(@startdate,@enddate,@location)"
        Dim adapter As New SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings("rtdsConnectionString").ConnectionString)
        adapter.SelectCommand.CommandType = CommandType.Text
        adapter.SelectCommand.CommandText = sql
        Dim param As New SqlParameter("location", loca)
        adapter.SelectCommand.Parameters.Add(param)
        Dim param2 As New SqlParameter("startdate", sDate)
        adapter.SelectCommand.Parameters.Add(param2)
        Dim param3 As New SqlParameter("enddate", eDate)
        adapter.SelectCommand.Parameters.Add(param3)
        adapter.SelectCommand.CommandTimeout = 360

        Try
            Dim getlist As Integer = 0
            getlist = adapter.Fill(dt)
        Catch ex As Exception
            Dim err As String = ex.Message
        End Try

        Return dt
    End Function

    Public Function getNetOtherPayByEmpID(ByVal loca As String, ByVal sDate As Date, ByVal eDate As Date, ByVal empID As Guid) As Decimal
        Dim otherPay As Decimal = 0

        Dim strSQL As String = "DECLARE @tblHours TABLE (ID  uniqueidentifier,PercentHours Decimal(8,2),HourlyHours Decimal(8,2)) " & _
"INSERT INTO @tblHours (ID  ,PercentHours ,HourlyHours ) " & _
"select ID,PercentHours ,HourlyHours  FROM tblfunc_payrolllocationhours(@startdate,@enddate,@Location) " & _
"DECLARE @tblEmpRates TABLE (ID  uniqueidentifier, EmpName varchar(50), EmpNum varchar(50), PayType int, PayRateHourly Decimal(6,2), PayRatePercentage Decimal(6,2)) " & _
"INSERT INTO @tblEmpRates(ID , EmpName, EmpNum, PayType, PayRateHourly, PayRatePercentage) " & _
"SELECT * FROM tblfunc_payrollemployeeinfo(@startdate,@enddate) "
        '"DECLARE @tblLoadsSum TABLE (ID  uniqueidentifier, ulAmount  Decimal(8,2)) " & _
        '"INSERT INTO @tblLoadsSum (ID, ulAmount)      " & _
        '"SELECT ID,SUM(ulAmount)AS ulAmount FROM tblfunc_payrollloads(@startdate,@enddate,@Location)GROUP BY ID " & _
        '"DECLARE @tblAddCompSum TABLE (EmployeeID uniqueidentifier,  AddCompAmount decimal(8,2)) " & _
        '"INSERT INTO @tblAddCompSum (EmployeeID,   AddCompAmount) " & _
        '"SELECT EmployeeID,SUM(AddCompAmount)AS AddCompAmount FROM tblfunc_payrolladdcomp(@startdate,@enddate)GROUP BY EmployeeID " & _
        '"SELECT tblHours.ID,  " & _
        '"EmpName, " & _
        '"EmpNum, " & _
        '"CASE " & _
        '"WHEN ulAmount>0 " & _
        '"THEN ulAmount " & _
        '"ELSE 0.00 " & _
        '"END AS ulAmount, " & _
        '"PercentHours,  " & _
        '"HourlyHours, " & _
        '"PercentHours + HourlyHours AS TotalHours, " & _
        '"CASE " & _
        '"WHEN PercentHours + HourlyHours >40 " & _
        '"THEN PercentHours + HourlyHours -40 " & _
        '"ELSE 0.00 " & _
        '"END AS OThours, " & _
        '"PayType, " & _
        '"PayRateHourly, " & _
        '"PayRatePercentage, " & _
        '"CASE " & _
        '"WHEN AddCompAmount>0 " & _
        '"THEN AddCompAmount " & _
        '"ELSE 0.00 " & _
        '"END AS AddCompAmount, " & _
        '"((HourlyHours + PercentHours) * PayRateHourly)+(case when ulAmount > 0 then ulamount else 0.00 end *(PayRatePercentage*.01))+ CASE WHEN AddCompAmount>0 THEN AddCompAmount ELSE 0.00 END AS RegularPay, " & _
        '"CASE " & _
        '"WHEN PercentHours + HourlyHours > 40 AND PayType = 1 " & _
        '"THEN (PercentHours + HourlyHours - 40.00)*(ulAmount*(PayRatePercentage*.01) + CASE WHEN AddCompAmount>0 THEN AddCompAmount ELSE 0.00 END)/((PercentHours + HourlyHours))*0.5 " & _
        '"WHEN PercentHours + HourlyHours > 40 AND PayType = 2 " & _
        '"THEN ((PercentHours + HourlyHours)- 40) * PayRateHourly " & _
        '"ELSE 0.00 " & _
        '"END AS OTpay " & _
        '"FROM @tblHours AS tblHours " & _
        '"LEFT OUTER JOIN @tblEmpRates AS tblEmpRates ON tblEmpRates.ID = tblHours.ID " & _
        '"LEFT OUTER JOIN @tblLoadsSum AS tblLoadsSum ON tblLoadsSum.ID = tblHours.ID " & _
        '"LEFT OUTER JOIN @tblAddCompSum AS tblAddCompSum ON tblAddCompSum.EmployeeID = tblHours.ID " & _
        '"ORDER BY PayType, EmpNum"

        Dim dba As New DBAccess
        dba.CommandText = strSQL
        dba.AddParameter("@locaID", loca)
        dba.AddParameter("@empID", empID)
        dba.AddParameter("@sDate", sDate)
        dba.AddParameter("@eDate", eDate)
        Dim ds As New DataSet
        Try
            ds = dba.ExecuteDataSet
        Catch ex As Exception
        End Try
        If ds.Tables(1).Rows.Count > 0 Then
            otherPay = ds.Tables(1).Rows(0).Item("OtherAmt")
        End If
        Return otherPay
    End Function



    ' to do   include percentage ot pay





End Class
