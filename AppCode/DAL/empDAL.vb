'Imports DiversifiedLogistics.Employee
Imports System.Data
Imports System.Data.SqlClient
'Imports System.Web
'Imports DiversifiedLogistics.Utilities
'Imports Telerik.Web.UI
'Imports System.IO
'Imports System.Drawing
Public Class empDAL
#Region "Get Methods"
    Public Function GetEmployeeByID(ByVal rtdsID As Guid) As Employee
        Dim emp As New Employee
        Dim gstr As String = rtdsID.ToString
        Dim sql As String = "SELECT E.FirstName, E.LastName, E.Comments, E.Certification, E.LocationID, E.HomeLocationID, E.PhotoJpegData, E.Login, E.Password, E.AccessRightsMask, E.PayrollEmpNum, E.ID,  " & _
            "Location.Name AS LocationName " & _
            "FROM Employee AS E INNER JOIN " & _
            "Location ON E.LocationID = Location.ID " & _
            "WHERE (E.ID = @eid) "
        Dim adapter As New SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings("rtdsConnectionString").ConnectionString)
        Dim param As New SqlParameter("eid", rtdsID)
        adapter.SelectCommand.Parameters.Add(param)
        Dim dt As New DataTable()
        adapter.Fill(dt)
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                emp.rtdsFirstName = rw.Item("FirstName")
                emp.rtdsLastName = rw.Item("LastName")
                emp.Comments = IIf(IsDBNull(rw.Item("Comments")), "", rw.Item("Comments"))
                emp.Certification = IIf(IsDBNull(rw.Item("Certification")), "", rw.Item("Certification"))
                emp.PhotoJpegData = IIf(IsDBNull(rw.Item("PhotoJpegData")), Nothing, rw.Item("PhotoJpegData"))
                emp.LocationID = rw.Item("LocationID")
                emp.LocationName = rw.Item("LocationName")
                emp.HomeLocationID = IIf(IsDBNull(rw.Item("HomeLocationID")), Nothing, rw.Item("HomeLocationID"))
                emp.Login = rw.Item("Login")
                emp.rtdsPassword = rw.Item("Password")
                emp.AccessRightsMask = rw.Item("AccessRightsMask")
                emp.ID = rw.Item("ID")
                emp.PayrollEmpNum = IIf(IsDBNull(rw.Item("PayrollEmpNum")), Nothing, rw.Item("PayrollEmpNum"))
                emp.Employment = getCurrentEmployment(emp.ID)
                Dim usrdal As New userDAL()
                emp.ssUser = usrdal.getUserByName(emp.Login)
            Next
        End If
        Return emp

    End Function

    Public Function getEmployees() As DataTable
        Dim dt As New DataTable()
        Dim sql As String = "SELECT ID, (LastName + ', ' + FirstName) AS Name FROM Employee ORDER BY LastName"
        Dim adapter As New SqlDataAdapter(Sql, ConfigurationManager.ConnectionStrings("rtdsConnectionString").ConnectionString)
        adapter.Fill(dt)
        Return dt   'ID, Name
    End Function

    Public Function getEmployeesByLocation(ByVal locaID As Guid) As DataTable
        Dim dt As New DataTable()
        Dim sql As String = "SELECT ID, (LastName + ', ' + FirstName) AS Name FROM Employee WHERE LocationID = @locaID ORDER BY LastName"
        Dim adapter As New SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings("rtdsConnectionString").ConnectionString)
        Dim param As New SqlParameter("locaID", locaID)
        adapter.SelectCommand.Parameters.Add(param)
        adapter.Fill(dt)
        Return dt   'ID, Name
    End Function

    Public Function getWorkersByLocation(ByVal locaID As Guid) As DataTable
        'as determined by PayType = 1(percentag) or 2(hourly)
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        Dim datenow As Date = FormatDateTime(Date.Now(), DateFormat.ShortDate)
        dba.CommandText = "SELECT distinct(E.ID), E.LastName,  (E.LastName + ', ' + E.FirstName) AS Name, E.Login " & _
            "FROM Employee AS E INNER JOIN " & _
            "Employment ON E.ID = Employment.EmployeeID " & _
            "WHERE ((E.LocationID = @locaID) " & _
            "AND (Employment.DateOfDismiss > @datenow)) " & _
            "AND (Employment.PayType=1 OR Employment.PayType=2) " & _
            "AND (E.FirstName <> 'Truck' OR E.LastName <> 'Driver') " & _
            "ORDER BY E.LastName"
        dba.AddParameter("@locaID", locaID)
        dba.AddParameter("@datenow", datenow)
        Try
            dt = dba.ExecuteDataSet.Tables(0)
            For Each rw As DataRow In dt.Rows




            Next
        Catch ex As Exception

        End Try
        Return dt   'returns ID , EmpName
    End Function


    Public Function getUnloadersByLocation(ByVal locaID As Guid) As DataTable
        Dim dt As New DataTable()
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT Employee.ID, Employee.FirstName + ' ' + Employee.LastName AS Name " & _
            "FROM Employee INNER JOIN " & _
            "Employment ON Employee.ID = Employment.EmployeeID " & _
            "WHERE (Employee.LocationID = @locaID) AND (Employment.DateOfDismiss > @today) " & _
            "ORDER BY Employee.LastName "
        dba.AddParameter("@locaID", locaID)
        dba.AddParameter("@joTitle", "Manager")
        dba.AddParameter("@today", Date.Now)
        Dim ds As DataSet = dba.ExecuteDataSet
        dt = ds.Tables(0)
        Return dt   'ID, Name
    End Function

    Public Function GetUnloadersByWOID(ByVal woid As String) As DataTable
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT E.ID, E.FirstName + ' ' + E.LastName AS Name " & _
            "FROM WorkOrder WO INNER JOIN " & _
            "Unloader U ON WO.ID = U.LoadID INNER JOIN " & _
            "Employee E ON U.EmployeeID = E.ID " & _
            "WHERE (WO.ID = @woID) "
        dba.AddParameter("@woID", woid)
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        Return dt
    End Function

    Public Function GetUnloadersByWOIDString(ByVal loadID As String) As String
        Dim ulstring As String = String.Empty
        Dim rdba As New DBAccess()
        rdba.CommandText = "SELECT E.FirstName, E.LastName " & _
            "FROM WorkOrder WO INNER JOIN " & _
            "Unloader U ON WO.ID = U.LoadID INNER JOIN " & _
            "Employee E ON U.EmployeeID = E.ID " & _
            "WHERE (WO.ID = @woID) "
        rdba.AddParameter("@woID", loadID)
        Dim reader As SqlDataReader = rdba.ExecuteReader
        Do While reader.Read()
            ulstring &= reader(0) & " " & reader(1) & " - "
        Loop
        reader.Close()
        If ulstring.Length > 3 Then
            ulstring = Left(ulstring, Len(ulstring) - 3)
        Else
            ulstring = "<center>- - -</center>"
        End If
        Return ulstring
    End Function

    Public Function getEmployeeCertList(ByVal empID As Guid) As List(Of Certification)
        Dim certList As New List(Of Certification)
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT C.TypeID, CertificationType.Name AS certName, C.Date AS certDate, C.ID AS certID " & _
            "FROM Certification C INNER JOIN " & _
            "CertificationType ON C.TypeID = CertificationType.ID " & _
            "WHERE C.EmployeeID = @empID ORDER BY certName"
        dba.AddParameter("@empID", empID)
        Dim ds As DataSet = dba.ExecuteDataSet
        Dim dt As DataTable = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                Dim cert As New Certification
                cert.TypeID = rw.Item("TypeID")
                cert.certDate = rw.Item("certDate")
                cert.EmployeeID = empID
                cert.certName = rw.Item("certName")
                cert.ID = rw.Item("certID")
                certList.Add(cert)
            Next
        End If
        Return certList
    End Function

    Public Function getSupervisorsByLocation(ByVal locaID As Guid, Optional ByVal sdate As Date = Nothing) As DataTable
        Dim tpDAL As New TimePuncheDAL()

        If sdate = Nothing Then
            sdate = tpDAL.getPayStartDate(Date.Now())
        Else
            sdate = tpDAL.getPayStartDate(sdate)
        End If
        Dim edate As Date = DateAdd(DateInterval.Day, 14, sdate)
        Dim dt As New DataTable
        Dim dba As New DBAccess()

        dba.CommandText = "SELECT empl.EmployeeID, em.FirstName + ' ' + em.LastName as EmployeeName, em.Login  " & _
            "FROM Employment AS empl INNER JOIN " & _
            "Employee as Em ON empl.EmployeeID = Em.ID " & _
            "WHERE (empl.JobTitle = 'Unloader Supervisor') AND (empl.DateOfEmployment < @payperiodEndDate) AND (empl.DateOfDismiss > @payperiodStartDate)   " & _
            "AND em.LocationID = @LocationID ORDER BY empl.DateOfEmployment "
        dba.AddParameter("@payperiodStartDate", sdate)
        dba.AddParameter("@payperiodEndDate", edate)
        dba.AddParameter("@LocationID", locaID)
        dt = dba.ExecuteDataSet.Tables(0)
        Return dt       ' EmployeeID, EmployeeName, Login
    End Function



    Public Function getCurrentEmployment(ByVal empID As Guid, Optional thuDate As Object = Nothing) As Employment
        Dim sdate As Date = Nothing
        Dim tpdal As New TimePuncheDAL
        If IsDate(thuDate) Then
            sdate = tpdal.getPayStartDate(FormatDateTime(thuDate, DateFormat.ShortDate))
        Else
            sdate = tpdal.getPayStartDate(FormatDateTime(Date.Now(), DateFormat.ShortDate))
        End If
        Dim edate As Date = DateAdd(DateInterval.Day, 14, sdate)
        Dim empl As New Employment()
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT TOP 1 EmployeeID, DateOfEmployment, DateOfDismiss, JobTitle, PayType, PayRateHourly, PayRatePercentage, SpecialPay, HolidayPay, SalaryPay, ID from Employment " & _
            "WHERE EmployeeID=@empID AND DateOfEmployment < @payperiodEndDate And DateOfDismiss > @payperiodStartDate ORDER BY DateOfEmployment ASC"
        dba.AddParameter("@empID", empID)
        dba.AddParameter("@payperiodStartDate", sdate)
        dba.AddParameter("@payperiodEndDate", edate)
        Try
            Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
            If dt.Rows.Count > 0 Then
                Dim rw As DataRow = dt.Rows(0)
                empl.EmployeeID = empID
                empl.DateOfEmployment = IIf(IsDBNull(rw.Item("DateOfEmployment")), Nothing, rw.Item("DateOfEmployment"))
                empl.DateOfDismiss = IIf(IsDBNull(rw.Item("DateOfDismiss")), Nothing, rw.Item("DateOfDismiss"))
                empl.JobTitle = IIf(IsDBNull(rw.Item("JobTitle")), "", rw.Item("JobTitle"))
                empl.PayType = IIf(IsDBNull(rw.Item("PayType")), Nothing, rw.Item("PayType"))
                empl.PayRateHourly = IIf(IsDBNull(rw.Item("PayRateHourly")), Nothing, rw.Item("PayRateHourly"))
                empl.PayRatePercentage = IIf(IsDBNull(rw.Item("PayRatePercentage")), Nothing, rw.Item("PayRatePercentage"))

                empl.SpecialPay = IIf(IsDBNull(rw.Item("SpecialPay")), Nothing, rw.Item("SpecialPay"))
                empl.HolidayPay = IIf(IsDBNull(rw.Item("HolidayPay")), Nothing, rw.Item("HolidayPay"))
                empl.SalaryPay = IIf(IsDBNull(rw.Item("SalaryPay")), Nothing, rw.Item("SalaryPay"))

                empl.ID = rw.Item("ID")
            End If
        Catch ex As Exception

        End Try

        Return empl
    End Function

    Public Function getEmploymentByID(ByVal vemploymentID As Guid) As Employment
        Dim empl As New Employment()
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT EmployeeID, DateOfEmployment, DateOfDismiss, JobTitle, PayType, PayRateHourly, PayRatePercentage, SpecialPay, HolidayPay, SalaryPay, ID from Employment " & _
            "WHERE ID=@emplID"
        dba.AddParameter("@emplID", vemploymentID)
        Try
            Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
            If dt.Rows.Count > 0 Then
                Dim rw As DataRow = dt.Rows(0)
                empl.EmployeeID = IIf(IsDBNull(rw.Item("EmployeeID")), Nothing, rw.Item("EmployeeID"))
                empl.DateOfEmployment = IIf(IsDBNull(rw.Item("DateOfEmployment")), Nothing, rw.Item("DateOfEmployment"))
                empl.DateOfDismiss = IIf(IsDBNull(rw.Item("DateOfDismiss")), Nothing, rw.Item("DateOfDismiss"))
                empl.JobTitle = IIf(IsDBNull(rw.Item("JobTitle")), "", rw.Item("JobTitle"))
                empl.PayType = IIf(IsDBNull(rw.Item("PayType")), Nothing, rw.Item("PayType"))
                empl.PayRateHourly = IIf(IsDBNull(rw.Item("PayRateHourly")), Nothing, rw.Item("PayRateHourly"))
                empl.PayRatePercentage = IIf(IsDBNull(rw.Item("PayRatePercentage")), Nothing, rw.Item("PayRatePercentage"))
                empl.SpecialPay = IIf(IsDBNull(rw.Item("SpecialPay")), Nothing, rw.Item("SpecialPay"))
                empl.HolidayPay = IIf(IsDBNull(rw.Item("HolidayPay")), Nothing, rw.Item("HolidayPay"))
                empl.SalaryPay = IIf(IsDBNull(rw.Item("SalaryPay")), Nothing, rw.Item("SalaryPay"))
                empl.ID = rw.Item("ID")
            End If
        Catch ex As Exception

        End Try
        Return empl
    End Function

    Public Function GetEmploymentList(ByVal empID As Guid) As List(Of Employment)
        Dim empList As New List(Of Employment)
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT [EmployeeID], [DateOfEmployment], [DateOfDismiss], [JobTitle], [PayType], [PayRateHourly], " & _
            "[PayRatePercentage], [ID] FROM [Employment] " & _
            "WHERE ([EmployeeID] = @EmployeeID) order by DateOfEmployment DESC"
        dba.AddParameter("@EmployeeID", empID)
        Try
            Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
            If dt.Rows.Count > 0 Then
                For Each rw As DataRow In dt.Rows
                    Dim empl As New Employment()
                    empl.EmployeeID = empID
                    empl.DateOfEmployment = IIf(IsDBNull(rw.Item("DateOfEmployment")), Nothing, rw.Item("DateOfEmployment"))
                    empl.DateOfDismiss = IIf(IsDBNull(rw.Item("DateOfDismiss")), Nothing, rw.Item("DateOfDismiss"))
                    empl.JobTitle = IIf(IsDBNull(rw.Item("JobTitle")), "", rw.Item("JobTitle"))
                    empl.PayType = IIf(IsDBNull(rw.Item("PayType")), Nothing, rw.Item("PayType"))
                    empl.PayRateHourly = IIf(IsDBNull(rw.Item("PayRateHourly")), Nothing, rw.Item("PayRateHourly"))
                    empl.PayRatePercentage = IIf(IsDBNull(rw.Item("PayRatePercentage")), Nothing, rw.Item("PayRatePercentage"))
                    'empl.SpecialPay = IIf(Not IsDBNull(rw.Item("SpecialPay")), "", rw.Item("SpecialPay"))
                    'empl.HolidayPay = IIf(Not IsDBNull(rw.Item("HolidayPay")), "", rw.Item("HolidayPay"))
                    'empl.SalaryPay = IIf(Not IsDBNull(rw.Item("SalaryPay")), "", rw.Item("SalaryPay"))
                    empl.ID = rw.Item("ID")
                    empList.Add(empl)
                Next
            End If
        Catch ex As Exception

        End Try

        Return empList
    End Function

    Public Function getEmployeeName(ByVal rtdsID As Guid) As String
        Dim retString As String = String.Empty
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT Employee.FirstName + ' ' + Employee.LastName AS Name FROM Employee WHERE ID=@eid"
        dba.AddParameter("@eid", rtdsID)
        Return dba.ExecuteScalar
    End Function


#End Region

    Public Function getSickLeaveBalance(ByVal empid As Guid, ByVal locaid As String) As Double
        Dim retval As Double
        retval = getSickLeave(empid, locaid) - getSickLeaveUsed(empid)
        Return retval
    End Function

    Public Function getSickLeaveUsed(ByVal empid As Guid) As Double
        Dim retval As Double
        Dim dba As New DBAccess
        dba.CommandText = "Select SUM(hoursUsed) from SickLeaveUsed WHERE employeeID = @employeeID"
        dba.AddParameter("@employeeID", empid)
        retval = IIf(IsDBNull(dba.ExecuteScalar), 0, dba.ExecuteScalar)
        Return retval
    End Function

    Public Function getSickLeave(ByVal empid As Guid, ByVal locaid As String) As Double
        Dim retval As Double
        Dim dba As New DBAccess
        dba.CommandText = "SELECT sickLeaveStart,sickLeaveMaxAccrued,sickLeaveMinPerUse,sickLeaveEligibility,sickleaveMaxUseAnnum,sickLeaveAccrualRate from BenefitsConfiguration where LocationID=@locaID order by sickLeaveStart DESC"
        dba.AddParameter("@locaID", locaid)
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        Dim row As DataRow = dt.Rows(0)
        Dim vsickLeaveStart As Date = row.Item(0)
        Dim vsickLeaveMaxAccrued As Integer = row.Item(1)
        Dim sickLeaveMinPerUse As Integer = row.Item(2)
        Dim sickLeaveEligibilitya As Integer = row.Item(3)
        Dim sickleaveMaxUseAnnum As Integer = row.Item(4)
        Dim sickLeaveAccrualRate As Integer = row.Item(4)



        dba.CommandText = "SELECT SUM(TimeInOut.HoursWorked) AS HoursWorked " & _
            "FROM TimeInOut INNER JOIN " & _
            "TimePunche ON TimeInOut.TimepuncheID = TimePunche.ID INNER JOIN " & _
            "Employee ON TimePunche.EmployeeID = Employee.ID " & _
            "WHERE (TimePunche.DateWorked > @startDate) AND (TimePunche.DateWorked < @endDate) " & _
            "and employee.id = @empID "
        dba.AddParameter("@startDate", DateAdd(DateInterval.Day, -365, Date.Now))
        dba.AddParameter("@endDate", Date.Now.ToShortDateString)
        dba.AddParameter("@empID", empid)
        Try
            retval = dba.ExecuteScalar
            If retval > sickLeaveAccrualRate Then
                If retval / sickLeaveAccrualRate > vsickLeaveMaxAccrued Then
                    retval = vsickLeaveMaxAccrued
                Else
                    retval = retval / sickLeaveAccrualRate
                End If
            Else
                retval = 0
            End If
        Catch ex As Exception
            retval = 0
        End Try
        Return retval
    End Function



#Region "Update Methods"

    Public Function updateEmployee(ByVal emp As Employee) As String
        Dim errMsg As String = String.Empty
        Dim dba As New DBAccess()
        ' setup
        dba.CommandText = "UPDATE Employee SET FirstName = @FirstName, PayrollEmpNum = @PayrollEmpNum, LastName = @LastName, Comments = @Comments, Password=@Password, LocationID = @LocationID, HomeLocationID = @HomeLocationID,  Locked = @Locked, AccessRightsMask = @AccessRightsMask, TermDate=@TermDate WHERE ID = @ID"
        dba.AddParameter("@FirstName", emp.rtdsFirstName)
        dba.AddParameter("@LastName", emp.rtdsLastName)
        dba.AddParameter("@Comments", emp.Comments)
        dba.AddParameter("@LocationID", emp.LocationID)
        dba.AddParameter("@HomeLocationID", emp.HomeLocationID)
        dba.AddParameter("@Locked", emp.Locked)
        dba.AddParameter("@Password", emp.rtdsPassword)
        dba.AddParameter("@AccessRightsMask", emp.AccessRightsMask)
        dba.AddParameter("@ID", emp.ID)
        dba.AddParameter("@PayrollEmpNum", emp.PayrollEmpNum)
        dba.AddParameter("@TermDate", emp.TermDate)
        Try
            dba.ExecuteNonQuery()
        Catch ex As Exception
            errMsg = ex.Message
        End Try
        Dim ardal As New AuditRepLogDAL()
        Return errMsg
    End Function

    Public Function updateEmployment(ByVal emp As Employee) As String
        Dim errMsg As String = String.Empty
        Dim dba As New DBAccess()
        ' setup
        dba.CommandText = "UPDATE Employment SET EmployeeID=@EmployeeID, DateOfEmployment=@DateOfEmployment, DateOfDismiss=@DateOfDismiss, JobTitle=@JobTitle, PayType=@PayType, PayRateHourly=@PayRateHourly, PayRatePercentage=@PayRatePercentage, SpecialPay=@SpecialPay, HolidayPay=@HolidayPay, SalaryPay=@SalaryPay WHERE ID=@ID"
        dba.AddParameter("@EmployeeID", emp.ID)
        dba.AddParameter("@DateOfEmployment", emp.Employment.DateOfEmployment)
        dba.AddParameter("@DateOfDismiss", emp.Employment.DateOfDismiss)
        dba.AddParameter("@JobTitle", emp.Employment.JobTitle)
        dba.AddParameter("@PayType", emp.Employment.PayType)
        dba.AddParameter("@PayRateHourly", emp.Employment.PayRateHourly)
        dba.AddParameter("@PayRatePercentage", emp.Employment.PayRatePercentage)
        dba.AddParameter("@SpecialPay", emp.Employment.SpecialPay)
        dba.AddParameter("@HolidayPay", emp.Employment.HolidayPay)
        dba.AddParameter("@SalaryPay", emp.Employment.SalaryPay)
        dba.AddParameter("@ID", emp.Employment.ID)
        Try
            dba.ExecuteNonQuery()
        Catch ex As Exception
            errMsg = ex.Message
        End Try
        ' if the DateOfDismiss is less than the surrogate null then delete the Div-Log Membership user
        Dim snDate As Date = "12/31/9999"
        'If emp.Employment.DateOfDismiss < snDate Then
        '    Dim udal As New userDAL()
        '    errMsg &= udal.deleteUser(emp.ssUser.userName)
        'End If
        Dim ardal As New AuditRepLogDAL()
        ardal.doEmploymentRepLog(emp)
        Return errMsg
    End Function

    Public Function updateEmployeeComments(ByVal empID As Guid, ByVal strComments As String) As String
        Dim errMsg As String = String.Empty
        Dim dba As New DBAccess()
        dba.CommandText = "UPDATE Employee SET Comments = @Comments WHERE ID = @ID"
        dba.AddParameter("@Comments", strComments)
        dba.AddParameter("@ID", empID)
        Try
            dba.ExecuteNonQuery()
        Catch ex As Exception
            errMsg = ex.Message
        End Try
        Return errMsg
    End Function

    Public Function updateEmployeeCertification(ByVal certID As Guid, ByVal certDate As Date) As String
        Dim errMsg As String = String.Empty
        Dim dba As New DBAccess()
        dba.CommandText = "UPDATE Certification SET [Date] = @certDate WHERE ID = @ID"
        dba.AddParameter("@certDate", certDate)
        dba.AddParameter("@ID", certID)
        Try
            dba.ExecuteNonQuery()
        Catch ex As Exception
            errMsg = ex.Message
        End Try
        Return errMsg
    End Function

    Public Function insertEmployeeCertification(ByVal empID As Guid, ByVal certID As Guid, ByVal certDate As Date) As String
        Dim errMsg As String = String.Empty
        Dim dba As New DBAccess()
        ' setup
        Dim sql As String = String.Empty
        Dim adapter As New SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings("rtdsConnectionString").ConnectionString)

        Dim param As New SqlParameter("eid", empID)
        adapter.SelectCommand.Parameters.Add(param)
        param.ParameterName = "xx"
        param.Value = "xx"



        Dim dt As New DataTable()
        adapter.Fill(dt)

        Try
            dba.ExecuteNonQuery()
        Catch ex As Exception
            errMsg = ex.Message
        End Try
        Return errMsg

    End Function

    Public Function getNextEmpID(ByVal locaID As Guid) As String
        Dim nxtID As String = String.Empty
        Dim nxtNum As Integer = 101
        Dim locaDAL As New locaDAL
        Dim uDAL As New userDAL
        Dim locaPrefix As String = locaDAL.getLoginPrefixByLocationID(locaID.ToString())
        Dim dba As New DBAccess()
        Dim prev As String = String.Empty
        If locaPrefix = "" Then
            dba.CommandText = "SELECT TOP 50 [Login] FROM Employee WHERE HomeLocationID = '" & locaID.ToString() & "' ORDER BY Login Desc"
            Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
            For Each rw As DataRow In dt.Rows
                If IsNumeric(rw.Item(0)) Then
                    prev = rw.Item(0)
                    nxtID = (prev + 1).ToString
                    Exit For
                End If
            Next
        Else
            dba.CommandText = "SELECT TOP 1 [Login] FROM Employee WHERE HomeLocationID = '" & locaID.ToString() & "' AND Login LIKE '" & locaPrefix & "%' ORDER BY Login Desc"
            Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
            If dt.Rows.Count > 0 Then
                prev = dt.Rows(0).Item(0)
                nxtNum = Right(prev, prev.Length - locaPrefix.Length)
                If IsNumeric(nxtNum) Then
                    nxtNum = nxtNum + 1
                End If

            End If
            nxtID = locaPrefix & nxtNum
            Dim nxtIDexist As String = uDAL.checkDupeUserName(nxtID)
            If nxtIDexist <> String.Empty Then
                While nxtIDexist <> String.Empty
                    nxtNum += 1
                    nxtID = locaPrefix & nxtNum
                    nxtIDexist = uDAL.checkDupeUserName(nxtID)
                End While
            End If
        End If
        Return nxtID
    End Function

    Public Function insertEmployee(ByVal emp As Employee) As String
        Dim errMsg As String = String.Empty
        Dim dba As New DBAccess()
        dba.CommandText = "INSERT INTO Employee (FirstName,LastName,Comments,PhotoJpegData,LocationID,HomeLocationID,Login,Password,Locked,AccessRightsMask,ID,PayrollEmpNum,rowguid,TermDate) " & _
            "VALUES (@FirstName,@LastName,@Comments,@PhotoJpegData,@LocationID,@HomeLocationID,@Login,@Password,@Locked,@AccessRightsMask,@ID,@PayrollEmpNum,@rowguid,@TermDate)"
        dba.AddParameter("@FirstName", emp.rtdsFirstName)
        dba.AddParameter("@LastName", emp.rtdsLastName)
        dba.AddParameter("@Comments", emp.Comments)
        dba.AddParameter("@PhotoJpegData", emp.PhotoJpegData)
        dba.AddParameter("@LocationID", emp.LocationID)
        dba.AddParameter("@HomeLocationID", emp.HomeLocationID)
        dba.AddParameter("@Login", emp.Login)
        dba.AddParameter("@Password", emp.rtdsPassword)
        dba.AddParameter("@Locked", emp.Locked)
        dba.AddParameter("@AccessRightsMask", emp.AccessRightsMask)
        dba.AddParameter("@ID", emp.ID)
        dba.AddParameter("@PayrollEmpNum", emp.PayrollEmpNum)
        dba.AddParameter("@rowguid", emp.rowguid)
        dba.AddParameter("@TermDate", emp.TermDate)

        ' setup
        Try
            dba.ExecuteNonQuery()
        Catch ex As Exception
            errMsg = ex.Message
        End Try
        'add to audit table and Replication Log
        Dim arDAL As New AuditRepLogDAL()
        arDAL.UpdateAudit("Employee", "n/a", emp.rtdsFirstName & " " & emp.rtdsLastName, "New Employee", emp.ID)
        arDAL.doEmployeeRepLog(emp)
        Return errMsg
    End Function

    Public Function insertEmployment(ByVal emp As Employee) As String
        Dim errMsg As String = String.Empty
        Dim dba As New DBAccess()
        Dim udal As New userDAL()
        Dim usr As String = udal.addUser(emp.ssUser)
        ' did membership svcs suceed
        dba.CommandText = "INSERT INTO Employment (EmployeeID,DateOfEmployment,DateOfDismiss,JobTitle,PayType,PayRateHourly,PayRatePercentage,SpecialPay,HolidayPay,SalaryPay,ID) " & _
            "VALUES (@EmployeeID,@DateOfEmployment,@DateOfDismiss,@JobTitle,@PayType,@PayRateHourly,@PayRatePercentage,@SpecialPay,@HolidayPay,@SalaryPay,@ID)"
        dba.AddParameter("@EmployeeID", emp.ID)
        dba.AddParameter("@DateOfEmployment", emp.Employment.DateOfEmployment)
        dba.AddParameter("@DateOfDismiss", emp.Employment.DateOfDismiss)
        dba.AddParameter("@JobTitle", emp.Employment.JobTitle)
        dba.AddParameter("@PayType", emp.Employment.PayType)
        dba.AddParameter("@PayRateHourly", emp.Employment.PayRateHourly)
        dba.AddParameter("@PayRatePercentage", emp.Employment.PayRatePercentage)
        dba.AddParameter("@SpecialPay", emp.Employment.SpecialPay)
        dba.AddParameter("@HolidayPay", emp.Employment.HolidayPay)
        dba.AddParameter("@SalaryPay", emp.Employment.SalaryPay)
        dba.AddParameter("@ID", emp.Employment.ID)
        Try
            dba.ExecuteNonQuery()
        Catch ex As Exception
            errMsg = ex.Message
        End Try
        Dim arDAL As New AuditRepLogDAL()
        'add to audit table
        arDAL.UpdateAudit("Employment", "n/a", emp.Employment.JobTitle & ":" & emp.Employment.PayType, "New Employee", emp.ID)
        'add to Replica Log
        arDAL.doEmploymentRepLog(emp)
        Return errMsg
    End Function
    Public Function insertPendingEmployment(ByVal emp As Employee) As String
        Dim errMsg As String = String.Empty
        Dim dba As New DBAccess()
        Dim udal As New userDAL()
        Dim usr As String = udal.addUser(emp.ssUser)
        ' did membership svcs suceed
        dba.CommandText = "INSERT INTO PendingEmployment (EmployeeID,DateOfEmployment,DateOfDismiss,JobTitle,PayType,PayRateHourly,PayRatePercentage,SpecialPay,HolidayPay,SalaryPay,ID) " & _
            "VALUES (@EmployeeID,@DateOfEmployment,@DateOfDismiss,@JobTitle,@PayType,@PayRateHourly,@PayRatePercentage,@SpecialPay,@HolidayPay,@SalaryPay,@ID)"
        dba.AddParameter("@EmployeeID", emp.ID)
        dba.AddParameter("@DateOfEmployment", emp.Employment.DateOfEmployment)
        dba.AddParameter("@DateOfDismiss", emp.Employment.DateOfDismiss)
        dba.AddParameter("@JobTitle", emp.Employment.JobTitle)
        dba.AddParameter("@PayType", emp.Employment.PayType)
        dba.AddParameter("@PayRateHourly", emp.Employment.PayRateHourly)
        dba.AddParameter("@PayRatePercentage", emp.Employment.PayRatePercentage)
        dba.AddParameter("@SpecialPay", emp.Employment.SpecialPay)
        dba.AddParameter("@HolidayPay", emp.Employment.HolidayPay)
        dba.AddParameter("@SalaryPay", emp.Employment.SalaryPay)
        dba.AddParameter("@ID", emp.Employment.ID)
        Try
            dba.ExecuteNonQuery()
        Catch ex As Exception
            errMsg = ex.Message
        End Try
        Return errMsg
    End Function
#End Region

    Public Function getJobApp(ByVal apid As String) As EmploymentApplicationObject
        Dim ja As New EmploymentApplicationObject
        Dim dba As New DBAccess("rtds")
        Dim dt As New DataTable()
        Dim sql As String = String.Empty
        sql = "SELECT EmploymentApplicationID, LocationID, FirstName, MiddleInitial, LastName, Referredby, StreetAddress, Zip, City, State, " & _
           "PrimaryPhone, AltPhone, Email, DesiredPosition, DesiredStartDate, DesiredSalary, CurrentlyEmployed, AskCurrentEmployer, " & _
           "AppliedBefore, AppliedBeforeLocation, AppliedBeforeDate, EducationLevel, School1, School1Location, School1YearsAttended, " & _
           "School1Graduated, School1SubjectsStudied, School2, School2Location, School2YearsAttended, School2Graduated, School2SubjectsStudied, " & _
           "School3, School3Location, School3YearsAttended, School3Graduated, School3SubjectsStudied, SpecialSkills, MilitaryBranch, " & _
           "MilitaryServiceFromDate, MilitaryServiceToDate, MilitaryRank, pe1FromDate, pe1ToDate, PE1, PE1Location, PE1phone, PE1salary, " & _
           "PE1position, PE1reasonForLeaving, pe2FromDate, pe2ToDate, PE2, PE2Location, PE2phone, PE2salary, PE2position, PE2reasonForLeaving, " & _
           "pe3FromDate, pe3ToDate, PE3, PE3Location, PE3phone, PE3salary, PE3position, PE3reasonForLeaving, pe4FromDate, pe4ToDate, PE4, " & _
           "PE4Location, PE4phone, PE4salary, PE4position, PE4reasonForLeaving, Reference1, Reference1YrsKnown, Reference1Contact, Reference2, " & _
           "Reference2YrsKnown, Reference2Contact, Reference3, Reference3YrsKnown, Reference3Contact, " & _
           "Rating, TimeStamp, ApplicantIP FROM EmploymentApplication " & _
           "WHERE EmploymentApplicationID = @EmploymentApplicationID"
        dba.CommandText = sql
        dba.AddParameter("@EmploymentApplicationID", apid)
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception
            Dim err As String = ex.Message
        End Try
        If dt.Rows.Count > 0 Then
            Dim rw As DataRow = dt.Rows(0)
            ja.EmploymentApplicationID = rw.Item("EmploymentApplicationID")
            ja.LocationID = rw.Item("LocationID")
            ja.FirstName = rw.Item("FirstName")
            ja.MiddleInitial = rw.Item("MiddleInitial")
            ja.LastName = rw.Item("LastName")
            ja.Referredby = rw.Item("Referredby")
            ja.StreetAddress = rw.Item("StreetAddress")
            ja.Zip = rw.Item("Zip")
            ja.City = rw.Item("City")
            ja.State = rw.Item("State")
            ja.PrimaryPhone = rw.Item("PrimaryPhone")
            ja.AltPhone = rw.Item("AltPhone")
            ja.Email = rw.Item("Email")
            ja.DesiredPosition = rw.Item("DesiredPosition")
            ja.DesiredStartDate = rw.Item("DesiredStartDate")
            ja.DesiredSalary = rw.Item("DesiredSalary")
            ja.CurrentlyEmployed = rw.Item("CurrentlyEmployed")
            ja.AskCurrentEmployer = rw.Item("AskCurrentEmployer")
            ja.AppliedBefore = rw.Item("AppliedBefore")
            ja.AppliedBeforeLocation = rw.Item("AppliedBeforeLocation")
            ja.AppliedBeforeDate = rw.Item("AppliedBeforeDate")
            ja.EducationLevel = rw.Item("EducationLevel")
            ja.School1 = rw.Item("School1")
            ja.School1Location = rw.Item("School1Location")
            ja.School1YearsAttended = rw.Item("School1YearsAttended")
            ja.School1Graduated = rw.Item("School1Graduated")
            ja.School1SubjectsStudied = rw.Item("School1SubjectsStudied")
            ja.School2 = rw.Item("School2")
            ja.School2Location = rw.Item("School2Location")
            ja.School2YearsAttended = rw.Item("School2YearsAttended")
            ja.School2Graduated = rw.Item("School2Graduated")
            ja.School2SubjectsStudied = rw.Item("School2SubjectsStudied")
            ja.School3 = rw.Item("School3")
            ja.School3Location = rw.Item("School3Location")
            ja.School3YearsAttended = rw.Item("School3YearsAttended")
            ja.School3Graduated = rw.Item("School3Graduated")
            ja.School3SubjectsStudied = rw.Item("School3SubjectsStudied")
            ja.SpecialSkills = rw.Item("SpecialSkills")
            ja.MilitaryBranch = rw.Item("MilitaryBranch")
            ja.MilitaryServiceFromDate = rw.Item("MilitaryServiceFromDate")
            ja.MilitaryServiceToDate = rw.Item("MilitaryServiceToDate")
            ja.MilitaryRank = rw.Item("MilitaryRank")
            ja.pe1FromDate = rw.Item("pe1FromDate")
            ja.pe1ToDate = rw.Item("pe1ToDate")
            ja.PE1 = rw.Item("PE1")
            ja.PE1Location = rw.Item("PE1Location")
            ja.PE1phone = rw.Item("PE1phone")
            ja.PE1salary = rw.Item("PE1salary")
            ja.PE1position = rw.Item("PE1position")
            ja.PE1reasonForLeaving = rw.Item("PE1reasonForLeaving")
            ja.pe2FromDate = rw.Item("pe2FromDate")
            ja.pe2ToDate = rw.Item("pe2ToDate")
            ja.PE2 = rw.Item("PE2")
            ja.PE2Location = rw.Item("PE2Location")
            ja.PE2phone = rw.Item("PE2phone")
            ja.PE2salary = rw.Item("PE2salary")
            ja.PE2position = rw.Item("PE2position")
            ja.PE2reasonForLeaving = rw.Item("PE2reasonForLeaving")
            ja.pe3FromDate = rw.Item("pe3FromDate")
            ja.pe3ToDate = rw.Item("pe3ToDate")
            ja.PE3 = rw.Item("PE3")
            ja.PE3Location = rw.Item("PE3Location")
            ja.PE3phone = rw.Item("PE3phone")
            ja.PE3salary = rw.Item("PE3salary")
            ja.PE3position = rw.Item("PE3position")
            ja.PE3reasonForLeaving = rw.Item("PE3reasonForLeaving")
            ja.pe4FromDate = rw.Item("pe4FromDate")
            ja.pe4ToDate = rw.Item("pe4ToDate")
            ja.PE4 = rw.Item("PE4")
            ja.PE4Location = rw.Item("PE4Location")
            ja.PE4phone = rw.Item("PE4phone")
            ja.PE4salary = rw.Item("PE4salary")
            ja.PE4position = rw.Item("PE4position")
            ja.PE4reasonForLeaving = rw.Item("PE4reasonForLeaving")
            ja.Reference1 = rw.Item("Reference1")
            ja.Reference1YrsKnown = rw.Item("Reference1YrsKnown")
            ja.Reference1Contact = rw.Item("Reference1Contact")
            ja.Reference2 = rw.Item("Reference2")
            ja.Reference2YrsKnown = rw.Item("Reference2YrsKnown")
            ja.Reference2Contact = rw.Item("Reference2Contact")
            ja.Reference3 = rw.Item("Reference3")
            ja.Reference3YrsKnown = rw.Item("Reference3YrsKnown")
            ja.Reference3Contact = rw.Item("Reference3Contact")
            ja.Rating = rw.Item("Rating")
            ja.TimeStamp = rw.Item("TimeStamp")

            ja.ApplicantIP = rw.Item("ApplicantIP")
            ja.Rating = rw.Item("Rating")
            ja.TimeStamp = rw.Item("TimeStamp")
        End If

        Return ja
    End Function


    Public Function CertificationWarnings(ByVal locaName As String) As DataTable
        Dim dba As New DBAccess()
        dba.CommandText = "select (SELECT COUNT(CertificationType.ID) " & _
            "FROM Certification INNER JOIN " & _
            "CertificationType ON Certification.TypeID = CertificationType.ID INNER JOIN " & _
            "Employee ON Certification.EmployeeID = Employee.ID INNER JOIN " & _
            "Location ON Employee.LocationID = Location.ID INNER JOIN " & _
            "Employment ON Employee.ID = Employment.EmployeeID " & _
            "WHERE (Location.Name = @locaName) AND Employment.DateOfDismiss > {fn NOW()} " & _
            "and DATEADD(MONTH,24,[Date]) < GETDATE()) as expired, " & _
            "(SELECT COUNT(CertificationType.ID) " & _
            "FROM Certification INNER JOIN " & _
            "CertificationType ON Certification.TypeID = CertificationType.ID INNER JOIN " & _
            "Employee ON Certification.EmployeeID = Employee.ID INNER JOIN " & _
            "Location ON Employee.LocationID = Location.ID INNER JOIN " & _
            "Employment ON Employee.ID = Employment.EmployeeID " & _
            "WHERE (Location.Name = @locaName) AND Employment.DateOfDismiss > {fn NOW()} " & _
            "and DATEADD(MONTH,22,[Date]) < GETDATE()) - " & _
            "(SELECT COUNT(CertificationType.ID) " & _
            "FROM Certification INNER JOIN " & _
            "CertificationType ON Certification.TypeID = CertificationType.ID INNER JOIN " & _
            "Employee ON Certification.EmployeeID = Employee.ID INNER JOIN " & _
            "Location ON Employee.LocationID = Location.ID INNER JOIN " & _
            "Employment ON Employee.ID = Employment.EmployeeID " & _
            "WHERE (Location.Name = @locaName) AND Employment.DateOfDismiss > {fn NOW()} " & _
            "and DATEADD(MONTH,24,[Date]) < GETDATE()) as warning"
        dba.AddParameter("@locaName", locaName)
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        Return dt
    End Function



    Public Function getHRemp(ByVal emp As Employee) As HR_Employee
        Dim hrEmp As New HR_Employee()
        Dim dba As New DBAccess("divlogHR")
        dba.CommandText = "SELECT EmpTableID, EmployeeFirstName, EmployeeLastName, EmploeeRace, EmployeeGender, EmployeeDOB, EmployeeSSnumber, EmployeeNumber, EmployeePhone, EmployeeCell,  " & _
            "EmployeeAddress, EmployeeCity, EmployeeState, EmployeeZip, EmpNotificationName, EmpNotificationNumber, EmpNotificationAddress, EmployeeTerminationDate,  " & _
            "SeperationReason, LastEdited, LastEditUser, EmployeeSiteCompany " & _
            "FROM Employees WHERE EmployeeNumber=@EmployeeNumber "
        dba.AddParameter("@EmployeeLastName", emp.rtdsLastName)
        dba.AddParameter("@EmployeeNumber", emp.Login)
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        If dt.Rows.Count > 0 Then
            Dim row As DataRow = dt.Rows(0)
            hrEmp.EmpTableID = row.Item("EmpTableID")
            hrEmp.EmployeeFirstName = IIf(IsDBNull(row.Item("EmployeeFirstName")), Nothing, row.Item("EmployeeFirstName"))
            hrEmp.EmployeeLastName = IIf(IsDBNull(row.Item("EmployeeLastName")), Nothing, row.Item("EmployeeLastName"))
            hrEmp.EmploeeRace = IIf(IsDBNull(row.Item("EmploeeRace")), Nothing, row.Item("EmploeeRace"))
            hrEmp.EmployeeGender = IIf(IsDBNull(row.Item("EmployeeGender")), Nothing, row.Item("EmployeeGender"))
            hrEmp.EmployeeDOB = IIf(IsDBNull(row.Item("EmployeeDOB")), Nothing, row.Item("EmployeeDOB"))
            hrEmp.EmployeeSSnumber = IIf(IsDBNull(row.Item("EmployeeSSnumber")), Nothing, row.Item("EmployeeSSnumber"))
            hrEmp.EmployeeNumber = IIf(IsDBNull(row.Item("EmployeeNumber")), Nothing, row.Item("EmployeeNumber"))
            hrEmp.EmployeePhone = IIf(IsDBNull(row.Item("EmployeePhone")), Nothing, row.Item("EmployeePhone"))
            hrEmp.EmployeeCell = IIf(IsDBNull(row.Item("EmployeeCell")), Nothing, row.Item("EmployeeCell"))
            hrEmp.EmployeeAddress = IIf(IsDBNull(row.Item("EmployeeAddress")), Nothing, row.Item("EmployeeAddress"))
            hrEmp.EmployeeCity = IIf(IsDBNull(row.Item("EmployeeCity")), Nothing, row.Item("EmployeeCity"))
            hrEmp.EmployeeState = IIf(IsDBNull(row.Item("EmployeeState")), Nothing, row.Item("EmployeeState"))
            hrEmp.EmployeeZip = IIf(IsDBNull(row.Item("EmployeeZip")), Nothing, row.Item("EmployeeZip"))
            hrEmp.EmpNotificationName = IIf(IsDBNull(row.Item("EmpNotificationName")), Nothing, row.Item("EmpNotificationName"))
            hrEmp.EmpNotificationNumber = IIf(IsDBNull(row.Item("EmpNotificationNumber")), Nothing, row.Item("EmpNotificationNumber"))
            hrEmp.EmpNotificationAddress = IIf(IsDBNull(row.Item("EmpNotificationAddress")), Nothing, row.Item("EmpNotificationAddress"))
            hrEmp.EmployeeTerminationDate = IIf(IsDBNull(row.Item("EmployeeTerminationDate")), Nothing, row.Item("EmployeeTerminationDate"))
            hrEmp.SeperationReason = IIf(IsDBNull(row.Item("SeperationReason")), Nothing, row.Item("SeperationReason"))
            hrEmp.LastEdited = IIf(IsDBNull(row.Item("LastEdited")), Nothing, row.Item("LastEdited"))
            hrEmp.LastEditUser = IIf(IsDBNull(row.Item("LastEditUser")), Nothing, row.Item("LastEditUser"))
            hrEmp.EmployeeSiteCompany = IIf(IsDBNull(row.Item("EmployeeSiteCompany")), Nothing, row.Item("EmployeeSiteCompany"))
        End If

        Return hrEmp
    End Function


    Public Function updateDivLogHR(ByVal hrEmp As HR_Employee) As String
        Dim strRet As String = String.Empty
        Dim dba As New DBAccess("divlogHR")
        dba.CommandText = "UPDATE Employees SET EmployeeFirstName=@EmployeeFirstName, EmployeeLastName=@EmployeeLastName, " & _
            "EmploeeRace=@EmploeeRace, EmployeeGender=@EmployeeGender, EmployeeDOB=@EmployeeDOB, EmployeeSSnumber=@EmployeeSSnumber, " & _
            "EmployeeNumber=@EmployeeNumber, EmployeePhone=@EmployeePhone, EmployeeCell=@EmployeeCell, EmployeeAddress=@EmployeeAddress, " & _
            "EmployeeCity=@EmployeeCity, EmployeeState=@EmployeeState, EmployeeZip=@EmployeeZip, EmpNotificationName=@EmpNotificationName, " & _
            "EmpNotificationNumber=@EmpNotificationNumber, EmpNotificationAddress=@EmpNotificationAddress, EmployeeTerminationDate=@EmployeeTerminationDate, " & _
            "SeperationReason=@SeperationReason, LastEdited=@LastEdited, LastEditUser=@LastEditUser, EmployeeSiteCompany=@EmployeeSiteCompany " & _
            "WHERE EmpTableID = @EmpTableID"
        dba.AddParameter("@EmpTableID", hrEmp.EmpTableID)
        dba.AddParameter("@EmployeeFirstName", hrEmp.EmployeeFirstName)
        dba.AddParameter("@EmployeeLastName", hrEmp.EmployeeLastName)
        hrEmp.EmploeeRace = IIf(hrEmp.EmploeeRace Is Nothing, "", hrEmp.EmploeeRace)
        dba.AddParameter("@EmploeeRace", hrEmp.EmploeeRace)
        dba.AddParameter("@EmployeeGender", hrEmp.EmployeeGender)
        dba.AddParameter("@EmployeeDOB", hrEmp.EmployeeDOB)
        dba.AddParameter("@EmployeeSSnumber", hrEmp.EmployeeSSnumber)
        dba.AddParameter("@EmployeeNumber", hrEmp.EmployeeNumber)
        dba.AddParameter("@EmployeePhone", hrEmp.EmployeePhone)
        dba.AddParameter("@EmployeeCell", hrEmp.EmployeeCell)
        dba.AddParameter("@EmployeeAddress", hrEmp.EmployeeAddress)
        dba.AddParameter("@EmployeeCity", hrEmp.EmployeeCity)
        dba.AddParameter("@EmployeeState", hrEmp.EmployeeState)
        dba.AddParameter("@EmployeeZip", hrEmp.EmployeeZip)
        dba.AddParameter("@EmpNotificationName", hrEmp.EmpNotificationName)
        dba.AddParameter("@EmpNotificationNumber", hrEmp.EmpNotificationNumber)
        dba.AddParameter("@EmpNotificationAddress", hrEmp.EmpNotificationAddress)
        dba.AddParameter("@EmployeeTerminationDate", hrEmp.EmployeeTerminationDate)
        dba.AddParameter("@SeperationReason", hrEmp.SeperationReason)
        dba.AddParameter("@LastEdited", hrEmp.LastEdited)
        dba.AddParameter("@LastEditUser", hrEmp.LastEditUser)
        dba.AddParameter("@EmployeeSiteCompany", hrEmp.EmployeeSiteCompany)

        dba.AddParameter("@EmployeeLocation", hrEmp.EmployeeLocation)
        dba.AddParameter("@EmployeeJobTitle", hrEmp.EmployeeJobTitle)
        dba.AddParameter("@EmployeePayType", hrEmp.EmployeePayType)
        Try
            Dim i As Integer = dba.ExecuteNonQuery
            If i > 0 Then
                strRet = "Personnel Changes Saved"
            Else
                strRet = "unknown error saving personnel record"
            End If

        Catch ex As Exception
            strRet = ex.Message
        End Try
        Return strRet
    End Function


    Public Function insertDivLogHR(ByVal hrEmp As HR_Employee) As String
        Dim strRet As String = String.Empty
        Dim dba As New DBAccess("divlogHR")
        dba.CommandText = "INSERT INTO Employees (EmployeeFirstName, EmployeeLastName, EmploeeRace, EmployeeGender, EmployeeDOB, EmployeeSSnumber, EmployeeNumber, EmployeePhone, EmployeeCell, " & _
            "EmployeeAddress, EmployeeCity, EmployeeState, EmployeeZip, EmpNotificationName, EmpNotificationNumber, " & _
            "EmpNotificationAddress, EmployeeTerminationDate, SeperationReason, LastEdited, LastEditUser, EmployeeSiteCompany, " & _
            "EmployeeLocation, EmployeeJobTitle, EmployeePayType) " & _
            "VALUES (@EmployeeFirstName, @EmployeeLastName, @EmploeeRace, @EmployeeGender, @EmployeeDOB, @EmployeeSSnumber, @EmployeeNumber, @EmployeePhone, @EmployeeCell, " & _
            "@EmployeeAddress, @EmployeeCity, @EmployeeState, @EmployeeZip, @EmpNotificationName, @EmpNotificationNumber, " & _
            "@EmpNotificationAddress, @EmployeeTerminationDate, @SeperationReason, @LastEdited, @LastEditUser, @EmployeeSiteCompany, " & _
            "@EmployeeLocation, @EmployeeJobTitle, @EmployeePayType)"
        dba.AddParameter("@EmployeeFirstName", hrEmp.EmployeeFirstName)
        dba.AddParameter("@EmployeeLastName", hrEmp.EmployeeLastName)
        hrEmp.EmploeeRace = IIf(hrEmp.EmploeeRace Is Nothing, "", hrEmp.EmploeeRace)
        dba.AddParameter("@EmploeeRace", hrEmp.EmploeeRace)
        dba.AddParameter("@EmployeeGender", hrEmp.EmployeeGender)
        dba.AddParameter("@EmployeeDOB", hrEmp.EmployeeDOB)
        dba.AddParameter("@EmployeeSSnumber", hrEmp.EmployeeSSnumber)
        dba.AddParameter("@EmployeeNumber", hrEmp.EmployeeNumber)
        dba.AddParameter("@EmployeePhone", hrEmp.EmployeePhone)
        dba.AddParameter("@EmployeeCell", hrEmp.EmployeeCell)
        dba.AddParameter("@EmployeeAddress", hrEmp.EmployeeAddress)
        dba.AddParameter("@EmployeeCity", hrEmp.EmployeeCity)
        dba.AddParameter("@EmployeeState", hrEmp.EmployeeState)
        dba.AddParameter("@EmployeeZip", hrEmp.EmployeeZip)
        dba.AddParameter("@EmpNotificationName", hrEmp.EmpNotificationName)
        dba.AddParameter("@EmpNotificationNumber", hrEmp.EmpNotificationNumber)
        dba.AddParameter("@EmpNotificationAddress", hrEmp.EmpNotificationAddress)
        dba.AddParameter("@EmployeeTerminationDate", hrEmp.EmployeeTerminationDate)
        dba.AddParameter("@SeperationReason", hrEmp.SeperationReason)
        dba.AddParameter("@LastEdited", hrEmp.LastEdited)
        dba.AddParameter("@LastEditUser", hrEmp.LastEditUser)
        dba.AddParameter("@EmployeeSiteCompany", hrEmp.EmployeeSiteCompany)
        dba.AddParameter("@EmployeeLocation", hrEmp.EmployeeLocation)
        dba.AddParameter("@EmployeeJobTitle", hrEmp.EmployeeJobTitle)
        dba.AddParameter("@EmployeePayType", hrEmp.EmployeePayType)
        Try
            Dim i As Integer = dba.ExecuteNonQuery()
            If i > 0 Then
                strRet = "New HR Record Saved"
            Else
                strRet = "unknown error saving new personnel record"
            End If
        Catch ex As Exception
            strRet = ex.Message
        End Try
        Return strRet
    End Function

    Public Function getJobTitleNumber(ByVal jobTitle As String) As Integer
        Dim retInt As Integer = 14
        Dim dba As New DBAccess("divlogHR")
        dba.CommandText = "Select PositionID FROM JobTitles WHERE Position = @position"
        dba.AddParameter("@position", jobTitle)
        Dim pid As Integer = dba.ExecuteScalar
        If pid > 0 Then retInt = pid
        Return retInt
    End Function

    Public Function getFirstNameByeid(ByVal eid As String) As String
        Dim retstr As String = String.Empty
        Dim dba As New DBAccess
        dba.CommandText = "SELECT firstname from employee where id = @eid"
        dba.AddParameter("@eid", eid)
        Try
            retstr = dba.ExecuteScalar

        Catch ex As Exception
            retstr = "--"
        End Try
        Return retstr
    End Function

    Public Function isOnClock(ByVal eid As String) As Boolean
        Dim dba As New DBAccess
        dba.CommandText = "SELECT e.id,e.FirstName, e.LastName " & _
        "FROM TimePunche INNER JOIN " & _
        "Employee e ON TimePunche.EmployeeID = e.ID INNER JOIN " & _
        "TimeInOut Tio ON TimePunche.ID = Tio.TimepuncheID " & _
        "WHERE ((Tio.TimeOut = '1/1/1900') OR " & _
        "(Tio.TimeOut IS NULL)) AND (e.id = @eid)"
        dba.AddParameter("@eid", eid)
        Dim dtemponclock As New DataTable
        dtemponclock = dba.ExecuteDataSet.Tables(0)
        Dim empName As String = String.Empty
        If dtemponclock.Rows.Count > 0 Then empName = dtemponclock.Rows(0).Item("FirstName") & dtemponclock.Rows(0).Item("LastName")
        isOnClock = dtemponclock.Rows.Count > 0
        Return isOnClock
    End Function

    Public Function isoutforday(ByVal eid As String) As Boolean
        Dim retbool As Boolean = True
        Dim dba As New DBAccess
        dba.CommandText = "SELECT e.id,e.FirstName, e.LastName " & _
        "FROM TimePunche INNER JOIN " & _
        "Employee e ON TimePunche.EmployeeID = e.ID INNER JOIN " & _
        "TimeInOut Tio ON TimePunche.ID = Tio.TimepuncheID " & _
        "WHERE (Tio.TimeOut > '1/1/1900') AND (e.id = @eid) and timepunche.isclosed = 1"
        dba.AddParameter("@eid", eid)
        Dim dtemponclock As New DataTable
        dtemponclock = dba.ExecuteDataSet.Tables(0)
        Dim empName As String = String.Empty
        If dtemponclock.Rows.Count > 0 Then empName = dtemponclock.Rows(0).Item("FirstName") & dtemponclock.Rows(0).Item("LastName")
        retbool = dtemponclock.Rows.Count > 0
        Return retbool
    End Function

    Public Function isOnBreak(ByVal eid As String) As Boolean
        Dim retbool As Boolean = True
        Dim dba As New DBAccess
        dba.CommandText = "SELECT e.id,e.FirstName, e.LastName " & _
        "FROM TimePunche INNER JOIN " & _
        "Employee e ON TimePunche.EmployeeID = e.ID INNER JOIN " & _
        "TimeInOut Tio ON TimePunche.ID = Tio.TimepuncheID " & _
        "WHERE (Tio.TimeOut > '1/1/1900') AND (e.id = @eid) and timepunche.isclosed = 0"
        dba.AddParameter("@eid", eid)
        Dim dtemponclock As New DataTable
        dtemponclock = dba.ExecuteDataSet.Tables(0)
        Dim empName As String = String.Empty
        If dtemponclock.Rows.Count > 0 Then empName = dtemponclock.Rows(0).Item("FirstName") & dtemponclock.Rows(0).Item("LastName")
        retbool = dtemponclock.Rows.Count > 0
        Return retbool
    End Function



    Public Function isUnloader(ByVal eid As String) As Boolean

    End Function

    Public Function isOnLoad(ByVal eid As String) As Boolean
        Dim numloads As Integer = 0
        numloads = countUnloaderLoads(eid)
        isOnLoad = numloads > 0
        Return isOnLoad
    End Function


    Public Function getOpenLoads() As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess
        dba.CommandText = "SELECT id from workorder where status < 74"
        Return dt
    End Function

    Public Function getOpenLoaders() As DataTable
        Dim dt As DataTable = New DataTable

        Return dt

    End Function

    Public Function countUnloaderLoads(ByVal eid As String) As Integer
        Dim retstr As Integer = 0
        Dim dba As New DBAccess
        dba.CommandText = "SELECT COUNT(WorkOrder.ID) AS Expr1 " & _
            "FROM WorkOrder INNER JOIN " & _
            "Unloader ON WorkOrder.ID = Unloader.LoadID " & _
            "WHERE (WorkOrder.Status < 74) " & _
            "AND (Unloader.EmployeeID = @eid)"
        dba.AddParameter("@eid", eid)
        retstr = dba.ExecuteScalar
        Return retstr
    End Function
End Class
