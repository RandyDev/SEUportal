Imports System.Data
'Imports System.Data.SqlClient

Public Class TravelerDAL

    Public Function getTraveler(ByVal tid As Guid) As Traveler
        Dim trv As Traveler = New Traveler
        Dim dt As DataTable = New DataTable()
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT T.travelID, T.rtdsEmployeeID, T.homeLocation, T.travelLocation, T.startDate, T.returnDate,  " & _
            "T.loadMoney, (E.FirstName + ' ' + E.LastName) As EmployeeName, hL.Name AS homeLocaName, tL.Name AS travelLocaName, " & _
            "T.salaryWeek, T.perDiemWeek " & _
            "FROM Travelers T INNER JOIN " & _
            "Employee E ON T.rtdsEmployeeID = E.ID INNER JOIN " & _
            "Location hL ON T.homeLocation = hL.ID INNER JOIN " & _
            "Location tL ON T.travelLocation = tL.ID WHERE travelID = @travelID"
        dba.AddParameter("@travelID", tid)
        Try
            dt = dba.ExecuteDataSet.Tables(0)
            If dt.Rows.Count > 0 Then
                Dim rw As DataRow = dt.Rows(0)
                trv.travelID = tid
                trv.rtdsEmployeeID = rw.Item("rtdsEmployeeID")
                trv.homeLocation = rw.Item("homeLocation")
                trv.travelLocation = rw.Item("travelLocation")
                trv.startDate = rw.Item("startDate")
                trv.returnDate = rw.Item("returnDate")
                trv.loadMoney = rw.Item("loadMoney")
                trv.EmployeeName = rw.Item("EmployeeName")
                trv.homeLocaName = rw.Item("homeLocaName")
                trv.travelLocaName = rw.Item("travelLocaName")
                trv.salaryWeek = rw.Item("salaryWeek")
                trv.perDiemWeek = rw.Item("perDiemWeek")
            End If

        Catch ex As Exception

        End Try
        Return trv
    End Function

    Public Function getActiveTravelers(Optional ByVal locaID As Guid? = Nothing, Optional ByVal isTAD As Boolean = True) As DataTable ' List(Of Traveler)
        '        Dim tList As List(Of Traveler) = New List(Of Traveler)
        Dim dt As DataTable = New DataTable()
        Dim dba As New DBAccess()
        Dim sql As String = "SELECT T.travelID, T.rtdsEmployeeID, T.homeLocation, T.travelLocation, T.startDate, T.returnDate,  " & _
            "T.loadMoney, (E.FirstName + ' ' + E.LastName) As EmployeeName, hL.Name AS homeLocaName, tL.Name AS travelLocaName, T.salaryWeek, T.perDiemWeek " & _
            "FROM Travelers T INNER JOIN " & _
            "Employee E ON T.rtdsEmployeeID = E.ID INNER JOIN " & _
            "Location hL ON T.homeLocation = hL.ID INNER JOIN " & _
            "Location tL ON T.travelLocation = tL.ID WHERE ((DATEPART(hh,startDate) + DATEPART(n, startDate) + DATEPART(s, startDate)) > 0) " & _
            "AND ((DATEPART(hh,returnDate) + DATEPART(n, returnDate) + DATEPART(s, returnDate)) = 0)"
        If Not isTAD Then
            sql &= "AND loadMoney = 1 "
        End If
        If Not locaID Is Nothing Then
            sql &= "AND homeLocation = @homeLocation  ORDER BY startDate, homeLocaName"
            dba.CommandText = sql
            dba.AddParameter("@homeLocation", locaID)
        End If
        dba.CommandText = sql & " ORDER BY startDate, homeLocaName"
        Try
            dt = dba.ExecuteDataSet.Tables(0)

        Catch ex As Exception

        End Try

        Return dt
        '        Return tList
    End Function

    Public Function getVisitingTravelers(ByVal locaID As Guid) As DataTable ' List(Of Traveler)
        '        Dim tList As List(Of Traveler) = New List(Of Traveler)
        Dim dt As DataTable = New DataTable()
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT T.travelID, T.rtdsEmployeeID, T.homeLocation, T.travelLocation, T.startDate, T.returnDate,  " & _
            "T.loadMoney, (E.FirstName + ' ' + E.LastName) As EmployeeName, hL.Name AS homeLocaName, tL.Name AS travelLocaName, T.salaryWeek, T.perDiemWeek " & _
            "FROM Travelers T INNER JOIN " & _
            "Employee E ON T.rtdsEmployeeID = E.ID INNER JOIN " & _
            "Location hL ON T.homeLocation = hL.ID INNER JOIN " & _
            "Location tL ON T.travelLocation = tL.ID " & _
            "WHERE ((DATEPART(hh,startDate) + DATEPART(n, startDate) + DATEPART(s, startDate)) > 0) " & _
            "AND ((DATEPART(hh,returnDate) + DATEPART(n, returnDate) + DATEPART(s, returnDate)) = 0) " & _
            "AND travelLocation = @travelLocation"
        dba.AddParameter("@travelLocation", locaID)
        Try
            dt = dba.ExecuteDataSet.Tables(0)

        Catch ex As Exception

        End Try

        Return dt
        '        Return tList
    End Function

    Public Function getTravelHistory(Optional ByVal locaID As Guid? = Nothing) As DataTable
        Dim dt As DataTable = New DataTable()
        Dim dba As New DBAccess()
        Dim sql As String = "SELECT T.travelID, T.rtdsEmployeeID, T.homeLocation, T.travelLocation, T.startDate, T.returnDate,  " & _
            "T.loadMoney, (E.FirstName + ' ' + E.LastName) As EmployeeName, hL.Name AS homeLocaName, tL.Name AS travelLocaName, " & _
            "T.salaryWeek, T.perDiemWeek " & _
            "FROM Travelers T INNER JOIN " & _
            "Employee E ON T.rtdsEmployeeID = E.ID INNER JOIN " & _
            "Location hL ON T.homeLocation = hL.ID INNER JOIN " & _
            "Location tL ON T.travelLocation = tL.ID " & _
            "WHERE ((DATEPART(hh,startDate) + DATEPART(n, startDate) + DATEPART(s, startDate)) > 0) " & _
            "AND ((DATEPART(hh,returnDate) + DATEPART(n, returnDate) + DATEPART(s, returnDate)) > 0) "
        If Not locaID Is Nothing Then
            sql &= "AND homeLocation = @homeLocation ORDER BY returnDate DESC, homeLocaName"
            dba.CommandText = sql
            dba.AddParameter("@homeLocation", locaID)
        Else
            dba.CommandText = sql & " ORDER BY returnDate DESC, homeLocaName"
        End If
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception

        End Try

        Return dt
        '        Return tList
    End Function

    Public Function getOpenTravelerByEmployeeID(ByVal empID As Guid) As Traveler
        Dim trv As New Traveler
        Dim dba As New DBAccess()
        Dim sql As String = "SELECT T.travelID, T.rtdsEmployeeID, T.homeLocation, T.travelLocation, T.startDate, T.returnDate,  " & _
            "T.loadMoney, (E.FirstName + ' ' + E.LastName) As EmployeeName, hL.Name AS homeLocaName, tL.Name AS travelLocaName, T.salaryWeek, T.perDiemWeek " & _
            "FROM Travelers T INNER JOIN " & _
            "Employee E ON T.rtdsEmployeeID = E.ID INNER JOIN " & _
            "Location hL ON T.homeLocation = hL.ID INNER JOIN " & _
            "Location tL ON T.travelLocation = tL.ID WHERE ((DATEPART(hh,startDate) + DATEPART(n, startDate) + DATEPART(s, startDate)) > 0) " & _
            "AND ((DATEPART(hh,returnDate) + DATEPART(n, returnDate) + DATEPART(s, returnDate)) = 0) " & _
            "AND rtdsEmployeeID = @empID"
        dba.CommandText = sql
        dba.AddParameter("@empID", empID)
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        If dt.Rows.Count > 0 Then
            Dim rw As DataRow = dt.Rows(0)
            trv.travelID = rw.Item("travelID")
            trv.rtdsEmployeeID = rw.Item("rtdsEmployeeID")
            trv.homeLocation = rw.Item("homeLocation")
            trv.travelLocation = rw.Item("travelLocation")
            trv.startDate = rw.Item("startDate")
            trv.returnDate = rw.Item("returnDate")
            trv.loadMoney = rw.Item("loadMoney")
            trv.EmployeeName = rw.Item("EmployeeName")
            trv.homeLocaName = rw.Item("homeLocaName")
            trv.travelLocaName = rw.Item("travelLocaName")
            trv.salaryWeek = rw.Item("salaryWeek")
            trv.perDiemWeek = rw.Item("perDiemWeek")
        End If
        Return trv
    End Function


    Public Function getPendingTravelers(Optional ByVal locaID As Guid? = Nothing) As DataTable ' List(Of Traveler)
        Dim dt As DataTable = New DataTable()
        Dim dba As New DBAccess()
        Dim sql As String = "SELECT T.travelID, T.rtdsEmployeeID, T.homeLocation, T.travelLocation, T.startDate, T.returnDate,  " & _
            "T.loadMoney, (E.FirstName + ' ' + E.LastName) As EmployeeName, hL.Name AS homeLocaName, tL.Name AS travelLocaName, T.salaryWeek, T.perDiemWeek " & _
            "FROM Travelers T INNER JOIN " & _
            "Employee E ON T.rtdsEmployeeID = E.ID INNER JOIN " & _
            "Location hL ON T.homeLocation = hL.ID INNER JOIN " & _
            "Location tL ON T.travelLocation = tL.ID  " & _
            "WHERE ((DATEPART(hh,startDate) + DATEPART(n, startDate) + DATEPART(s, startDate)) = 0) " 'because the time is all zeros
        If Not locaID Is Nothing Then
            sql &= "AND homeLocation = @homeLocation ORDER BY startDate, homeLocaName"
            dba.CommandText = sql
            dba.AddParameter("@homeLocation", locaID)
        Else
            dba.CommandText = sql & " ORDER BY startDate"
        End If
        Try
            dt = dba.ExecuteDataSet.Tables(0)

        Catch ex As Exception

        End Try

        Return dt
    End Function






#Region "CRUD"

    Public Function insertTraveler(ByVal trv As Traveler) As String
        Dim retString As String = String.Empty
        Dim dba As New DBAccess()
        dba.CommandText = "INSERT INTO dbo.Travelers (travelID, rtdsEmployeeID, homeLocation, travelLocation, startDate, returnDate, loadMoney, salaryWeek, perDiemWeek) " & _
            "VALUES (@travelID, @rtdsEmployeeID, @homeLocation, @travelLocation, @startDate, @returnDate, @loadMoney, @salaryWeek, @perDiemWeek)"
        dba.AddParameter("@travelID", trv.travelID)
        dba.AddParameter("@rtdsEmployeeID", trv.rtdsEmployeeID)
        dba.AddParameter("@homeLocation", trv.homeLocation)
        dba.AddParameter("@travelLocation", trv.travelLocation)
        dba.AddParameter("@startDate", trv.startDate)
        dba.AddParameter("@returnDate", trv.returnDate)
        dba.AddParameter("@loadMoney", trv.loadMoney)
        dba.AddParameter("@salaryWeek", trv.salaryWeek)
        dba.AddParameter("@perDiemWeek", trv.perDiemWeek)
        Try
            dba.ExecuteNonQuery()
        Catch ex As Exception
            retString = ex.Message
        End Try
        Return retString    'should be empty    ... if not, contains error message
    End Function

    Public Function updateTravelerDate(ByVal trv As Traveler) As String
        Dim retString As String = String.Empty
        Dim dba As New DBAccess()
        dba.CommandText = "UPDATE Travelers SET startDate = @startDate, returnDate = @returnDate, loadMoney = @loadMoney, salaryWeek = @salaryWeek, perDiemWeek = @perDiemWeek WHERE travelID = @travelID"
        dba.AddParameter("@startDate", trv.startDate)
        dba.AddParameter("@returnDate", trv.returnDate)
        dba.AddParameter("@loadMoney", trv.loadMoney)
        dba.AddParameter("@salaryWeek", trv.salaryWeek)
        dba.AddParameter("@perDiemWeek", trv.perDiemWeek)

        dba.AddParameter("@travelID", trv.travelID)
        Try
            Dim recs As Integer = dba.ExecuteNonQuery
        Catch ex As Exception
            retString = ex.Message
        End Try

        Return retString     'should be empty    ... if not, contains error message
    End Function

    Public Function beginTravelAssignment(ByVal travelID As Guid) As String
        Dim retString As String = String.Empty
        Dim trv As Traveler = getTraveler(travelID)
        Dim dba As New DBAccess()
        dba.CommandText = "UPDATE Employee SET LocationID = @locaID WHERE ID = @empID"
        dba.AddParameter("@locaID", trv.travelLocation)
        dba.AddParameter("@empID", trv.rtdsEmployeeID)
        Try
            Dim i As Integer = dba.ExecuteNonQuery
        Catch ex As Exception
            retString = ex.Message
        End Try

        'spin up updated employee object
        Dim edal As New empDAL()
        Dim emp As Employee = edal.GetEmployeeByID(trv.rtdsEmployeeID)
        'write it to rep log
        Dim rlDAL As New AuditRepLogDAL()
        rlDAL.doEmployeeRepLog(emp)

        ' update Travelers table
        Dim vstartDate As Date = DateAdd(DateInterval.Minute, 1, trv.startDate)
        dba.CommandText = "UPDATE dbo.Travelers SET startDate = @startDate WHERE travelID = @travelID"
        dba.AddParameter("@travelID", travelID)
        dba.AddParameter("@startDate", vstartDate)
        Try
            Dim i As Integer = dba.ExecuteNonQuery
        Catch ex As Exception
            retString = ex.Message
        End Try
        Return retString     'should be empty    ... if not, contains error message
    End Function

    Public Function endTravelAssignment(ByVal travelID As Guid) As String
        Dim retString As String = String.Empty
        Dim trv As Traveler = getTraveler(travelID)
        Dim dba As New DBAccess()
        dba.CommandText = "UPDATE Employee SET LocationID = @locaID WHERE ID = @empID"
        dba.AddParameter("@empID", trv.rtdsEmployeeID)
        dba.AddParameter("@locaID", trv.homeLocation)
        Try
            Dim i As Integer = dba.ExecuteNonQuery
        Catch ex As Exception
            retString = ex.Message
        End Try

        'spin up updated employee object
        Dim edal As New empDAL()
        Dim emp As Employee = edal.GetEmployeeByID(trv.rtdsEmployeeID)
        'write it to rep log
        Dim arLogDAL As New AuditRepLogDAL()
        arLogDAL.doEmployeeRepLog(emp)

        ' update Travelers table

        If trv.returnDate = "1/1/1990" Then
            Dim dnow As DateTime = Date.Now
            If dnow < trv.startDate Then
                'add one day and one minute
                trv.returnDate = DateAdd(DateInterval.Minute, 1, DateAdd(DateInterval.Day, 1, trv.startDate))
            Else
                trv.returnDate = dnow
            End If
        Else
            trv.returnDate = DateAdd(DateInterval.Minute, 1, trv.returnDate)

        End If
        dba.CommandText = "UPDATE dbo.Travelers SET returnDate = @returnDate WHERE travelID = @travelID"
        dba.AddParameter("@travelID", travelID)
        dba.AddParameter("@returnDate", trv.returnDate)
        Try
            Dim i As Integer = dba.ExecuteNonQuery
        Catch ex As Exception
            retString = ex.Message
        End Try
        Return retString     'should be empty    ... if not, contains error message
    End Function

    Public Function deleteTraveler(ByVal travelID As Guid) As String
        Dim retString As String = String.Empty
        Dim trv As Traveler = getTraveler(travelID)

        Dim dba As New DBAccess()
        dba.CommandText = "SELECT LocationID FROM Employee WHERE ID=@eid"
        dba.AddParameter("@eid", trv.rtdsEmployeeID)
        Dim currlocaID As Guid = dba.ExecuteScalar
        If Not trv.homeLocation = currlocaID Then
            'move em home
            dba.CommandText = "UPDATE Employee SET LocationID = @locaID WHERE ID=@eid"
            dba.AddParameter("@locaID", trv.homeLocation)
            dba.AddParameter("@eid", trv.rtdsEmployeeID)
            dba.ExecuteNonQuery()
            'spin up updated employee object
            Dim edal As New empDAL()
            Dim emp As Employee = edal.GetEmployeeByID(trv.rtdsEmployeeID)
            'write it to rep log
            Dim rlDAL As New AuditRepLogDAL()
            rlDAL.doEmployeeRepLog(emp)
        End If
        dba.CommandText = "DELETE FROM dbo.Travelers WHERE travelID = @travelID"
        dba.AddParameter("@travelID", travelID)
        Try
            dba.ExecuteNonQuery()
        Catch ex As Exception
            retString = ex.Message
        End Try

        Return retString    'should be empty    ... if not, contains error message
    End Function

#End Region



End Class
