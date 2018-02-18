Imports System
Imports System.IO
Imports System.Net
'Imports System.Net.Mail
'Imports System.Net.Mime
'Imports System.Threading
'Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Web

Public Class Utilities
    Public Function getLetterValue(ByVal letter As String) As Integer
        Select Case letter
            Case "A" : Return 0
            Case "B" : Return 1
            Case "C" : Return 2
            Case "D" : Return 3
            Case "E" : Return 4
            Case "F" : Return 5
            Case "G" : Return 6
            Case "H" : Return 7
            Case "I" : Return 8
            Case "J" : Return 9
            Case "K" : Return 10
            Case "L" : Return 11
            Case "M" : Return 12
            Case "N" : Return 13
            Case "O" : Return 14
            Case "P" : Return 15
            Case "Q" : Return 16
            Case "R" : Return 17
            Case "S" : Return 18
            Case "T" : Return 19
            Case "U" : Return 20
            Case "V" : Return 21
            Case "W" : Return 22
            Case "X" : Return 23
            Case "Y" : Return 24
            Case "Z" : Return 25
            Case "AA" : Return 26
            Case "BB" : Return 27
            Case "CC" : Return 28
            Case "DD" : Return 29
            Case "EE" : Return 30
            Case "FF" : Return 31
            Case "GG" : Return 32
            Case "HH" : Return 33
            Case "II" : Return 34
            Case "JJ" : Return 35
            Case "KK" : Return 36
            Case "LL" : Return 37
            Case "MM" : Return 38
            Case "NN" : Return 39
            Case "OO" : Return 40
            Case "PP" : Return 41
            Case "QQ" : Return 42
            Case "RR" : Return 43
            Case "SS" : Return 44
            Case "TT" : Return 45
            Case "UU" : Return 46
            Case "VV" : Return 47
            Case "WW" : Return 48
            Case "XX" : Return 49
            Case "YY" : Return 50
            Case "ZZ" : Return 51
        End Select
    End Function

    Public Shared Function IsValidGuid(ByVal value As String) As Boolean
        If value Is Nothing Or value = "00000000-0000-0000-0000-000000000000" Then
            Return False
        End If
        Return Regex.IsMatch(value, "^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.CultureInvariant)
    End Function

    Public Shared Function isValidEmail(ByVal eMail As String) As Boolean
        Return Regex.IsMatch(eMail, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
    End Function

    Public Shared Function zeroGuid() As Guid
        Return New Guid("00000000-0000-0000-0000-000000000000")
    End Function

    Public Function ZipCityState(ByVal zip As String) As String()
        Dim ZipInfo(3) As String
        Dim sql As String = "Select CityName, StateName, StateAbbr FROM ZipCodes WHERE ZipCode=@zipcode AND CityType='D'"
        Dim adapter As New SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings("zipCodeConnectionString").ConnectionString)
        Dim param As New SqlParameter("zipcode", zip)
        adapter.SelectCommand.Parameters.Add(param)
        Dim dt As New DataTable()
        adapter.Fill(dt)
        If dt.Rows.Count > 0 Then
            Dim rw As DataRow = dt.Rows(0)
            ZipInfo(0) = rw.Item(0)
            ZipInfo(1) = rw.Item(2)
            ZipInfo(2) = rw.Item(1)
        Else
            ZipInfo(0) = "ZIP not found"
        End If
        Return ZipInfo
    End Function

    Public Function isHourly(ByVal JobDescriptionid As Guid) As Boolean
        Dim retval As Boolean
        Dim dba As New DBAccess()
        dba.CommandText = "Select IsHourly from JobDescriptions WHERE ID=@ID"
        dba.AddParameter("@ID", JobDescriptionid)
        retval = dba.ExecuteScalar
        Return retval
    End Function
    Public Function isHourly(ByVal JobDescriptionid As String) As Boolean
        Dim retval As Boolean
        Dim dba As New DBAccess()
        dba.CommandText = "Select IsHourly from JobDescriptions WHERE ID=@ID"
        dba.AddParameter("@ID", JobDescriptionid)
        retval = dba.ExecuteScalar
        Return retval
    End Function

    Public Shared Function dwm(ByVal laston As Date) As String
        dwm = "Hello"
        Dim today As Date = Date.Now
        Dim days As Integer = CType(DateDiff(DateInterval.Day, laston, today), Integer)
        Select Case days
            Case 0
                dwm = "a little while ago."
            Case 1
                dwm = "yesterday"
            Case 2
                dwm = "a couple of days ago"
            Case Is < 7
                dwm = "a few days ago"
            Case Is < 10
                dwm = "about a week ago"
            Case Is < 15
                dwm = "a couple weeks ago"
            Case Is < 31
                dwm = "a few weeks ago"
            Case Is < 61
                dwm = "last month"
            Case Is < 90
                dwm = "a couple months ago"
            Case Is < 365
                dwm = "a few months ago"
            Case Else
                dwm = "over a year ago"
        End Select

        Return dwm
    End Function

    Public Shared Sub setSessionVars(ByVal cuser As ssUser)
        HttpContext.Current.Session("userID") = "8943897A-B2E0-401A-913B-3ED4B5BDC39F"
        HttpContext.Current.Session("userName") = "William"
        HttpContext.Current.Session("eMail") = "Randy@RandyDev.com"
        HttpContext.Current.Session("FirstName") = "William"
        HttpContext.Current.Session("MI") = "b"
        HttpContext.Current.Session("LastName") = "Tell"
        HttpContext.Current.Session("Title") = "Developer"
        HttpContext.Current.Session("Phone") = "615.681.7423"
        'HttpContext.Current.Session("Comment") = cuser.Comment
        'HttpContext.Current.Session("CreationDate") = cuser.CreationDate
        'HttpContext.Current.Session("Password") = cuser.Password
        'HttpContext.Current.Session("IsApproved") = cuser.IsApproved
        'HttpContext.Current.Session("IsLockedOut") = cuser.IsLockedOut
        'HttpContext.Current.Session("IsOnline") = cuser.IsOnline
        'HttpContext.Current.Session("LastActivityDate") = cuser.LastActivityDate
        'HttpContext.Current.Session("LastLockoutDate") = cuser.LastLockoutDate
        'HttpContext.Current.Session("LastLoginDate") = cuser.LastLoginDate
        'HttpContext.Current.Session("LastPasswordChangedDate") = cuser.LastPasswordChangedDate
        'HttpContext.Current.Session("PasswordQuestion") = cuser.PasswordQuestion
        'HttpContext.Current.Session("myRoles") = cuser.myRoles
    End Sub

    Public Function calcAccessRightsMask(ByVal admin As Boolean, ByVal web As Boolean, ByVal pda As Boolean) As String
        Dim arm As String = String.Empty
        If Not admin And web And Not pda Then
            arm = "1"
        ElseIf admin And Not web And Not pda Then
            arm = "2"
        ElseIf admin And web And Not pda Then
            arm = "3"
        ElseIf Not admin And Not web And pda Then
            arm = "4"
        ElseIf Not admin And web And pda Then
            arm = "5"
        ElseIf admin And Not web And pda Then
            arm = "6"
        ElseIf admin And web And pda Then
            arm = "7"
        Else
            arm = "0"
        End If
        Return arm
    End Function

    Public Function convertAccessRightsMask(ByVal arm As String) As ArrayList
        Dim arms As New ArrayList
        Select Case arm
            Case 1
                arms.Add(False)
                arms.Add(True)
                arms.Add(False)
            Case 2
                arms.Add(True)
                arms.Add(False)
                arms.Add(False)
            Case 3
                arms.Add(True)
                arms.Add(True)
                arms.Add(False)
            Case 4
                arms.Add(False)
                arms.Add(False)
                arms.Add(True)
            Case 5
                arms.Add(False)
                arms.Add(True)
                arms.Add(True)
            Case 6
                arms.Add(True)
                arms.Add(False)
                arms.Add(True)
            Case 7
                arms.Add(True)
                arms.Add(True)
                arms.Add(True)
            Case Else
                arms.Add(False)
                arms.Add(False)
                arms.Add(False)
        End Select
        Return arms
    End Function
    Public Function isOnClock(ByVal eid As String) As Boolean
        Dim a As New DBAccess
        a.CommandText = "SELECT e.id,e.FirstName, e.LastName " & _
        "FROM TimePunche INNER JOIN " & _
        "Employee e ON TimePunche.EmployeeID = e.ID INNER JOIN " & _
        "TimeInOut Tio ON TimePunche.ID = Tio.TimepuncheID " & _
        "WHERE ((Tio.TimeOut = '1/1/1900') OR " & _
        "(Tio.TimeOut IS NULL)) AND (e.id = '" & eid & "')"
        Dim dtemponclock As New DataTable
        dtemponclock = a.ExecuteDataSet.Tables(0)
        Dim empName As String = String.Empty
        If dtemponclock.Rows.Count > 0 Then empName = dtemponclock.Rows(0).Item("FirstName") & dtemponclock.Rows(0).Item("LastName")
        isOnClock = dtemponclock.Rows.Count > 0
        Return isOnClock
    End Function

    Public Shared Function getRTDSidByUserID(ByVal uid As String) As String
        Dim str As Guid
        Dim dba As New DBAccess()
        dba.CommandText = "Select rtdsEmployeeID from UserProfile WHERE userID = @userID"
        dba.AddParameter("@userID", uid)
        str = dba.ExecuteScalar
        Return str.ToString
    End Function

    Public Shared Function getRTDSuserNameByUserID(ByVal uid As String) As String
        Dim str As String
        Dim dba As New DBAccess()
        dba.CommandText = "Select FirstName + ' ' + LastName as Name FROM Employee WHERE ID = @userID"
        dba.AddParameter("@userID", uid)
        str = dba.ExecuteScalar
        Return str
    End Function

    Public Function CreatedByToText(ByVal cbGUID As Guid) As String
        Dim empdal As New empDAL()
        Dim rtdsUserID As String = Utilities.getRTDSidByUserID(HttpContext.Current.Session("userID").ToString)
        Dim emp As Employee = empdal.GetEmployeeByID(New Guid(rtdsUserID))
        Dim nm As String = emp.rtdsFirstName & " " & emp.rtdsLastName
        If nm.Length < 5 Then nm = HttpContext.Current.User.Identity.Name
        nm &= " : " & HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
        Return nm
    End Function

    Public Shared Function getLogo(ByVal locaID As Guid, ByVal rootDir As String) As String
        Dim retString As String = String.Empty
        Dim imgDir As String = "Images/"
        Dim cnam As String = String.Empty
        Dim ldal As New locaDAL
        cnam = Replace(ldal.getParentNameByLocationID(locaID.ToString).Trim(), " ", "")
        If File.Exists(rootDir + imgDir + cnam + ".png") Then
            retString = "http://SEU.Div-Log.com/" + imgDir + cnam + ".png"
        ElseIf File.Exists(rootDir + imgDir + cnam + ".jpg") Then
            retString = "http://SEU.Div-Log.com/" + imgDir + cnam + ".jpg"
        Else
            retString = "http://SEU.Div-Log.com/" + imgDir + "spacer.gif"
        End If
        Return retString
    End Function

    Public Shared Function getLogobg(ByVal locaID As Guid, ByVal rootDir As String) As String
        Dim retString As String = String.Empty
        Dim imgDir As String = "Images\"
        Dim cnam As String = String.Empty
        Dim ldal As New locaDAL
        cnam = Replace(ldal.getParentNameByLocationID(locaID.ToString).Trim(), " ", "") + "bg"
        If File.Exists(rootDir + imgDir + cnam + ".png") Then
            retString = "http://SEU.Div-Log.com/images/" + cnam + ".png"
        ElseIf File.Exists(rootDir + imgDir + cnam + ".jpg") Then
            retString = "http://SEU.Div-Log.com/images/" + cnam + ".jpg"
        Else
            retString = "http://SEU.Div-Log.com/images/" + "spacer.gif"
        End If
        Return retString
    End Function

    Public Shared Function getDefaultDateOfDismiss() As Date
        Dim dt As Date = "12/31/9999"
        Return dt
    End Function

    Public Shared Function getUserSkillSheet() As Boolean

        Return False

    End Function

    Public Shared Function getIntegerSuperScript(ByVal str As Integer) As String
        Dim retStr As String = String.Empty
        If str > 3 And str < 21 Then
            retStr = "<sup>th</sup>"
        ElseIf Right(str.ToString, 1) = "1" Then
            retStr = "<sup>st</sup>"
        ElseIf Right(str.ToString, 1) = "2" Then
            retStr = "<sup>nd</sup>"
        ElseIf Right(str.ToString, 1) = "3" Then
            retStr = "<sup>rd</sup>"
        Else
            retStr = "<sup>th</sup>"
        End If
        Return retStr
    End Function

    Public Shared Function countNewPics(ByVal PicsLastSent As Date, ByVal locaid As String) As Integer
        Dim retval As Integer = 0
        Dim dba As New DBAccess
        dba.CommandText = "SELECT count(WorkOrder.ID) " & _
            "FROM WorkOrder Inner JOIN " & _
            "LoadImages ON WorkOrder.ID = LoadImages.WorkOrderID " & _
            "WHERE (WorkOrder.DockTime > @PicsLastSent) and (WorkOrder.LocationID = @locaid)"
        dba.AddParameter("@PicsLastSent", PicsLastSent)
        dba.AddParameter("@locaid", locaid)
        Try
            retval = dba.ExecuteScalar
        Catch ex As Exception
            Dim er As String = ex.Message
        End Try
        Return retval
    End Function

    Public Shared Function countWorkOrdersWPics(ByVal PicsLastSent As Date, ByVal locaid As String) As Integer
        Dim retval As Integer = 0
        Dim dba As New DBAccess
        dba.CommandText = "SELECT count(Distinct(WorkOrder.ID)) " & _
            "FROM WorkOrder Inner JOIN " & _
            "LoadImages ON WorkOrder.ID = LoadImages.WorkOrderID " & _
            "WHERE (WorkOrder.DockTime > @PicsLastSent) and (WorkOrder.LocationID = @locaid)"
        dba.AddParameter("@PicsLastSent", PicsLastSent)
        dba.AddParameter("@locaid", locaid)
        Try
            retval = dba.ExecuteScalar
        Catch ex As Exception
            Dim er As String = ex.Message
        End Try
        Return retval
    End Function

    Public Shared Function haspics(ByVal locaid As String) As Boolean
        Dim retboo As Boolean = False
        Dim dba As New DBAccess
        dba.CommandText = "Select hasPics from location where ID = @locaid"
        dba.AddParameter("@locaid", locaid)
        retboo = dba.ExecuteScalar
        Return retboo
    End Function
End Class
