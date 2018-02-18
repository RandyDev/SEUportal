Public Class AuditRepLogDAL

    Public Sub UpdateAudit(ByVal tableName As String, ByVal oldValue As String, ByVal newValue As String, ByVal FieldName As String, ByVal PK As Guid)
        Dim udal As New userDAL()
        Dim usr As ssUser = udal.getUserByName(HttpContext.Current.User.Identity.Name)
        Dim nm As String = usr.FirstName & " " & usr.LastName
        If nm.Length < 5 Then nm = HttpContext.Current.User.Identity.Name
        nm &= " : " & HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
        Dim dba As New DBAccess()
        dba.CommandText = "INSERT INTO Audit (TableName, PK, FieldName, OldValue, NewValue, UpdateDate, UserName) VALUES (@TableName, @PK, @FieldName, @OldValue, @NewValue, @UpdateDate, @UserName)"
        dba.AddParameter("@TableName", tableName)
        dba.AddParameter("@PK", PK)
        dba.AddParameter("@FieldName", FieldName)
        dba.AddParameter("@OldValue", oldValue)
        dba.AddParameter("@NewValue", newValue)
        dba.AddParameter("@UpdateDate", Date.Now())
        dba.AddParameter("@UserName", nm)
        Dim i As Integer = dba.ExecuteNonQuery
    End Sub

    Public Function InsertRepLogEntry(ByVal rle As ReplicaLogEntry) As String
        Dim strResponse As String = String.Empty
        '    Dim dba As New DBAccess()
        '    ' if this is a subsequent change to a record, set the Actual field to false for previous entries

        '    dba.CommandText = "UPDATE ReplicaLog SET Actual=@actual WHERE ObjectID=@ObjectID"
        '    dba.AddParameter("@ObjectID", rle.ObjectID)
        '    dba.AddParameter("@Actual", False)
        '    Try
        '        dba.ExecuteNonQuery()
        '    Catch ex As Exception
        '    End Try
        '    ' this is the RTDS userID  the ACTUAL user is in audit table??
        '    rle.AutorID = New Guid("9e2dd366-f2ff-8064-156d-44d4d6b7cd8a")
        '    dba.CommandText = "INSERT INTO ReplicaLog (ID, Created, AutorID, ObjectID, ObjectName, ObjectValue, ObjectOperation, Actual, PotentialConflict) " & _
        '        "VALUES (@ID, @Created, @AutorID, @ObjectID, @ObjectName, @ObjectValue, @ObjectOperation, @Actual, @PotentialConflict)"
        '    dba.AddParameter("@ID", Guid.NewGuid())
        '    '        dba.AddParameter("@SeqNO", rle.SeqNO)
        '    dba.AddParameter("@Created", Date.Now())
        '    dba.AddParameter("@AutorID", rle.AutorID)
        '    dba.AddParameter("@ObjectID", rle.ObjectID)
        '    dba.AddParameter("@ObjectName", rle.ObjectName)
        '    dba.AddParameter("@ObjectValue", rle.ObjectValue)
        '    dba.AddParameter("@ObjectOperation", rle.ObjectOperation)
        '    dba.AddParameter("@Actual", True)
        '    dba.AddParameter("@PotentialConflict", rle.PotentialConflict)
        '    Try
        '        dba.ExecuteNonQuery()
        '    Catch ex As Exception
        '        strResponse = ex.Message
        '    End Try
        Return strResponse
    End Function

#Region "Build Replog entries"

    Public Sub DoEmployeeRepLog(ByVal emp As Employee, Optional ByVal operation As Integer = 0)
        Dim repLog As New ReplicaLogEntry
        repLog.ObjectID = emp.ID
        repLog.ObjectName = "Employee"
        Dim sb As New StringBuilder
        sb.Append("<" & repLog.ObjectName & ">")
        sb.Append("<FirstName>" & emp.rtdsFirstName & "</FirstName>")
        sb.Append("<LastName>" & emp.rtdsLastName & "</LastName>")
        sb.Append("<Comments>" & emp.Comments & "</Comments>")
        sb.Append("<PhotoJpegData>" & emp.PhotoJpegData.ToString & "</PhotoJpegData>")
        sb.Append("<LocationID>" & emp.LocationID.ToString & "</LocationID>")
        sb.Append("<Login>" & emp.Login & "</Login>")
        sb.Append("<Password>" & emp.rtdsPassword & "</Password>")
        sb.Append("<Locked>" & emp.Locked.ToString.ToLower & "</Locked>")
        sb.Append("<AccessRightsMask>" & emp.AccessRightsMask & "</AccessRightsMask>")
        sb.Append("<ID>" & emp.ID.ToString & "</ID>")
        sb.Append("</" & repLog.ObjectName & ">")
        repLog.ObjectValue = sb.ToString
        repLog.ObjectOperation = operation
        repLog.PotentialConflict = False
        '        Dim rlResponse As String = InsertRepLogEntry(repLog)
    End Sub

    Public Sub doEmploymentRepLog(ByVal emp As Employee, Optional ByVal operation As Integer = 0)
        Dim repLog As New ReplicaLogEntry
        repLog.ObjectID = emp.Employment.ID
        repLog.ObjectName = "Employment"
        Dim sb As New StringBuilder
        sb.Append("<" & repLog.ObjectName & ">")
        sb.Append("<EmployeeID>" & emp.ID.ToString & "</EmployeeID>")
        sb.Append("<DateOfEmployment>" & cnvDate(emp.Employment.DateOfEmployment) & "</DateOfEmployment>")
        sb.Append("<DateOfDismiss>" & cnvDate(emp.Employment.DateOfDismiss) & "</DateOfDismiss>")
        sb.Append("<JobTitle>" & emp.Employment.JobTitle & "</JobTitle>")
        sb.Append("<PayType>" & emp.Employment.PayType & "</PayType>")
        sb.Append("<PayRateHourly>" & emp.Employment.PayRateHourly & "</PayRateHourly>")
        sb.Append("<PayRatePercentage>" & emp.Employment.PayRatePercentage & "</PayRatePercentage>")
        sb.Append("<SpecialPay>" & emp.Employment.SpecialPay & "</SpecialPay>")
        sb.Append("<HolidayPay>" & emp.Employment.HolidayPay & "</HolidayPay>")
        sb.Append("<SalaryPay>" & emp.Employment.SalaryPay & "</SalaryPay>")
        sb.Append("<ID>" & emp.Employment.ID.ToString & "</ID>")
        sb.Append("</" & repLog.ObjectName & ">")
        repLog.ObjectValue = sb.ToString
        repLog.ObjectOperation = operation
        repLog.PotentialConflict = False

        '      Dim rlResponse As String = InsertRepLogEntry(repLog)
    End Sub

    Public Function cnvDate(ByVal dt As Date) As String
        Dim yr As String = DatePart(DateInterval.Year, dt).ToString
        Dim mo As String = DatePart(DateInterval.Month, dt).ToString
        If mo.Length = 1 Then mo = "0" & mo
        Dim da As String = DatePart(DateInterval.Day, dt).ToString
        If da.Length = 1 Then da = "0" & da
        Dim hr As String = DatePart(DateInterval.Hour, dt).ToString
        If hr.Length = 1 Then hr = "0" & hr
        Dim min As String = DatePart(DateInterval.Minute, dt).ToString
        If min.Length = 1 Then min = "0" & min
        Dim sec As String = DatePart(DateInterval.Second, dt).ToString
        If sec.Length = 1 Then sec = "0" & sec
        Return yr.ToString & "-" & mo.ToString & "-" & da.ToString & "T" & hr.ToString & ":" & min.ToString & ":" & sec.ToString
    End Function


    Public Sub doWorkOrderRepLog(ByVal wo As WorkOrder, Optional ByVal operation As Integer = 0, Optional writeComments As Boolean = False)

        If FormatDateTime(Date.Now(), DateFormat.ShortDate) = FormatDateTime(wo.LogDate, DateFormat.ShortDate) Then
            Dim repLog As New ReplicaLogEntry
            repLog.ObjectID = wo.ID
            repLog.ObjectName = "WorkOrder"
            Dim sb As New StringBuilder
            sb.Append("<" & repLog.ObjectName & ">")
            sb.Append("<Status>" & wo.Status & "</Status>")
            sb.Append("<LogDate>" & cnvDate(wo.LogDate) & "</LogDate>")
            sb.Append("<LogNumber>" & wo.LogNumber & "</LogNumber>")
            sb.Append("<LoadNumber>" & wo.LoadNumber & "</LoadNumber>")
            sb.Append("<LocationID>" & wo.LocationID.ToString & "</LocationID>")
            sb.Append("<DepartmentID>" & wo.DepartmentID.ToString & "</DepartmentID>")
            sb.Append("<LoadTypeID>" & wo.LoadTypeID.ToString & "</LoadTypeID>")
            sb.Append("<CustomerID>" & wo.CustomerID.ToString & "</CustomerID>")
            sb.Append("<VendorNumber>" & wo.VendorNumber & "</VendorNumber>")
            sb.Append("<ReceiptNumber>" & wo.ReceiptNumber & "</ReceiptNumber>")
            sb.Append("<PurchaseOrder>" & wo.PurchaseOrder & "</PurchaseOrder>")
            sb.Append("<Amount>" & wo.Amount & "</Amount>")
            sb.Append("<IsCash>" & wo.IsCash.ToString.ToLower & "</IsCash>")
            sb.Append("<LoadDescriptionID>" & wo.LoadDescriptionID.ToString & "</LoadDescriptionID>")
            sb.Append("<CarrierID>" & wo.CarrierID.ToString & "</CarrierID>")
            sb.Append("<TruckNumber>" & wo.TrailerNumber & "</TruckNumber>")
            sb.Append("<TrailerNumber>" & wo.TrailerNumber & "</TrailerNumber>")
            sb.Append("<AppointmentTime>" & cnvDate(wo.AppointmentTime) & "</AppointmentTime>")
            sb.Append("<GateTime>" & cnvDate(wo.GateTime) & "</GateTime>")
            sb.Append("<DockTime>" & cnvDate(wo.DockTime) & "</DockTime>")
            sb.Append("<StartTime>" & cnvDate(wo.StartTime) & "</StartTime>")
            sb.Append("<CompTime>" & cnvDate(wo.CompTime) & "</CompTime>")
            '        sb.Append("<TTLTime>" & wo.xx & "</TTLTime>")
            sb.Append("<PalletsUnloaded>" & wo.PalletsUnloaded & "</PalletsUnloaded>")
            sb.Append("<DoorNumber>" & wo.DoorNumber & "</DoorNumber>")
            sb.Append("<Pieces>" & wo.Pieces & "</Pieces>")
            sb.Append("<Weight>" & wo.Weight & "</Weight>")
            ' ************ comments commented out until issue with semi-colon is resolved *******************
            If writeComments Then
                '        Dim vComment As String = Replace(wo.Comments, ",", "")
                '        vComment = Replace(vComment, ";", "")
                '        sb.Append("<Comments>" & vComment & "</Comments>")
                sb.Append("<Comments>" & wo.Comments & "</Comments>")
            Else
                sb.Append("<Comments></Comments>")
            End If
            sb.Append("<Restacks>" & wo.Restacks & "</Restacks>")
            sb.Append("<PalletsReceived>" & wo.PalletsReceived & "</PalletsReceived>")
            sb.Append("<BadPallets>" & wo.BadPallets & "</BadPallets>")
            sb.Append("<NumberOfItems>" & wo.NumberOfItems & "</NumberOfItems>")
            sb.Append("<CheckNumber>" & Replace(wo.CheckNumber, "&", ",") & "</CheckNumber>")
            sb.Append("<BOL>" & wo.BOL & "</BOL>")
            sb.Append("<ID>" & wo.ID.ToString & "</ID>")
            '        sb.Append("<CreatedBy>" & wo.xx & "</CreatedBy>")
            sb.Append("</" & repLog.ObjectName & ">")
            repLog.ObjectValue = sb.ToString
            repLog.ObjectOperation = operation
            repLog.PotentialConflict = False

            '           Dim rlResponse As String = InsertRepLogEntry(repLog)
        End If
    End Sub

    Public Sub doTimePuncheRepLog(ByVal tp As TimePunche, Optional ByVal operation As Integer = 0)
        If FormatDateTime(Date.Now(), DateFormat.ShortDate) = FormatDateTime(tp.DateWorked, DateFormat.ShortDate) Then

            Dim repLog As New ReplicaLogEntry
            repLog.ObjectID = tp.ID
            repLog.ObjectName = "TimePunche"
            Dim sb As New StringBuilder
            sb.Append("<" & repLog.ObjectName & ">")

            sb.Append("<EmployeeID>" & tp.EmployeeID.ToString & "</EmployeeID>")
            sb.Append("<DepartmentID>" & tp.DepartmentID.ToString & "</DepartmentID>")
            sb.Append("<DateWorked>" & cnvDate(tp.DateWorked) & "</DateWorked>")
            sb.Append("<IsClosed>" & tp.IsClosed.ToString.ToLower & "</IsClosed>")
            sb.Append("<ID>" & tp.ID.ToString & "</ID>")

            sb.Append("</" & repLog.ObjectName & ">")
            repLog.ObjectValue = sb.ToString
            repLog.ObjectOperation = operation
            repLog.PotentialConflict = False
            '            Dim rlResponse As String = InsertRepLogEntry(repLog)
        End If

    End Sub

    Public Sub doTimeInOutRepLog(ByVal tio As TimeInOut, Optional ByVal operation As Integer = 0)
        Dim repLog As New ReplicaLogEntry
        repLog.ObjectID = tio.ID
        repLog.ObjectName = "TimeInOut"
        Dim sb As New StringBuilder
        sb.Append("<" & repLog.ObjectName & ">")
        sb.Append("<TimepuncheID>" & tio.TimepuncheID.ToString & "</TimepuncheID>")
        sb.Append("<TimeIn>" & cnvDate(tio.TimeIn) & "</TimeIn>")
        sb.Append("<TimeOut>" & cnvDate(tio.TimeOut) & "</TimeOut>")
        '        sb.Append("<HoursWorked>" & tio.ID.ToString & "</HoursWorked>")
        '        sb.Append("<Hours>" & tio.ID.ToString & "</Hours>")
        sb.Append("<ID>" & tio.ID.ToString & "</ID>")
        sb.Append("</" & repLog.ObjectName & ">")
        repLog.ObjectValue = sb.ToString
        repLog.ObjectOperation = operation
        repLog.PotentialConflict = False
        '        Dim rlResponse As String = InsertRepLogEntry(repLog)
    End Sub

    Public Sub doUnloaderRepLog(ByVal LoadID As Guid, ByVal empID As Guid, ByVal id As Guid, Optional ByVal operation As Integer = 0)
        Dim repLog As New ReplicaLogEntry
        repLog.ObjectID = id
        repLog.ObjectName = "Unloader"
        Dim sb As New StringBuilder
        sb.Append("<" & repLog.ObjectName & ">")
        sb.Append("<LoadID>" & LoadID.ToString & "</LoadID>")
        sb.Append("<EmployeeID>" & empID.ToString & "</EmployeeID>")
        sb.Append("<ID>" & id.ToString & "</ID>")
        sb.Append("</" & repLog.ObjectName & ">")
        repLog.ObjectValue = sb.ToString
        repLog.ObjectOperation = operation
        repLog.PotentialConflict = False
        '        Dim rlResponse As String = InsertRepLogEntry(repLog)
    End Sub

#End Region

End Class
