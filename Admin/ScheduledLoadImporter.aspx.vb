Imports OfficeOpenXml
Imports System.Collections.Generic
'Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports Telerik.Web.UI
'Imports DocumentFormat.OpenXml.Spreadsheet
'Imports DocumentFormat.OpenXml.Packaging
Imports System.IO

Public Class ScheduledLoadImporter
    Inherits System.Web.UI.Page
    Protected filename As String
    Protected vhasDate As Boolean = False
    Protected vImportdate As Date
    Protected vImportType As Integer = 0
    Protected vConfigID As String = String.Empty
    Protected vStartRow As Integer = 1
    Protected vConfigName As String = String.Empty
    Protected configlst As List(Of importConfigDataItem)
    Dim utl As New Utilities
    Dim wodal As New WorkOrderDAL
    Private timeOut As Integer

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        timeOut = Server.ScriptTimeout
        Server.ScriptTimeout = 3600
        RadScriptManager1.AsyncPostBackTimeout = 3600
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' populate Locations Combo Box (cbLocations on aspx page)
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
            cbImportName.Enabled = False
            RadDatePicker2.Visible = False
        End If
    End Sub

    Private Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        Server.ScriptTimeout = timeOut
    End Sub

    Private Sub cbLocations_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        Dim ldal As New locaDAL
        If cbLocations.SelectedIndex > -1 Then
            cbImportName.Items.Clear()
            cbImportName.Text = String.Empty
            If Utilities.IsValidGuid(cbLocations.SelectedValue) Then
                Dim dba As New DBAccess
                dba.CommandText = "SELECT ConfigID,ConfigName FROM dbo.ImportConfig WHERE LocationID=@LocationID"
                dba.AddParameter("@LocationID", cbLocations.SelectedValue.ToString)
                Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
                If dt.Rows.Count > 0 Then
                    cbImportName.Enabled = True
                    cbImportName.DataSource = dt
                    cbImportName.DataValueField = "ConfigID"
                    cbImportName.DataTextField = "ConfigName"
                    cbImportName.DataBind()
                    cbImportName.Visible = True
                    cbImportName.EmptyMessage = "Select Configuration"
                    cbImportName.SelectedIndex = -1
                Else
                    cbImportName.EmptyMessage = "None Defined"
                    cbImportName.Visible = True
                    divupload.Visible = False
                End If
                If dt.Rows.Count = 1 Then
                    cbImportName.Enabled = True
                    cbImportName.DataSource = dt
                    cbImportName.DataValueField = "ConfigID"
                    cbImportName.DataTextField = "ConfigName"
                    cbImportName.DataBind()
                    cbImportName.Visible = True
                    cbImportName.SelectedIndex = 0
                    cbImportName.Visible = True
                    divupload.Visible = True
                    getimportConfigInfo()
                End If
            End If
        End If
    End Sub

    Private Sub cbImportName_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbImportName.SelectedIndexChanged
        If cbImportName.Items.Count > 0 Then
        End If
        getimportConfigInfo()
        divupload.Visible = vhasDate
    End Sub

    Protected Sub btnImport_Click1(sender As Object, e As EventArgs) Handles btnImport.Click
        getimportConfigInfo()

        If Not vhasDate Then
            If RadDatePicker2.Visible And RadDatePicker2.SelectedDate Is Nothing Then
                lblresult.Text = "<span color='red'>You MUST select a Date!</span>"
                Exit Sub
            Else
                Dim logdate As Date = RadDatePicker2.SelectedDate
                lblresult.Text = String.Empty
            End If
        End If

        '        vImportdate = RadDatePicker2.SelectedDate

        If cbImportName.SelectedIndex = -1 Then 'no configname selected
            Exit Sub
        End If
        Dim locaid As String = cbLocations.SelectedValue
        '        configlst = GetImportData(vConfigID)        ' is this redundant? 163 'create a list to hold
        'now we know where/if to look on the spreadsheet for each field
        Dim path As String = Server.MapPath("~\upload\")
        Dim xfile As Stream = OpenFile(path & filename) '**********open the workbook
        Dim package As New ExcelPackage(xfile)          '**********store the workbook in object[variable]
        xfile.Close()                                   '**********close the workbook
        Dim i As Integer
        For i = 1 To package.Workbook.Worksheets.Count  'each sheet in the workbook
            Try
                ConvertExcelToArray(package, i)
            Catch ex As Exception
                Dim strerr As String = ex.Message
                lblresult.Text = strerr
                lblresult.ForeColor = System.Drawing.Color.Red
            End Try
        Next
        FileIO.FileSystem.DeleteFile(path + filename)   'should be done .. delete the upload
    End Sub

    Public Sub ConvertExcelToArray(ByVal package As ExcelPackage, ByVal sheetnum As Integer)
        Dim ldal As New locaDAL
        'extract the next sheet from the workbook and store to object
        'create empty workorder
        Dim wo As WorkOrder
        Dim workSheet As ExcelWorksheet = package.Workbook.Worksheets.Item(sheetnum)
        Dim arrayoutput As String(,)
        Dim utl As New Utilities
        Dim wodal As New WorkOrderDAL
        Dim lst As New List(Of importConfigDataItem) '' is this redundant? 114
        If Not workSheet.Dimension Is Nothing Then 'there is data on the worksheet
            'Set the array limit zero based array
            arrayoutput = New String(workSheet.Dimension.[End].Row - 1, workSheet.Dimension.[End].Column - 1) {}
            Dim startrow As Integer = vStartRow - 1 'FROM CONFIG -1 for zero based array
            Dim utls As New Utilities
            Dim startcol As Integer = 0 'always assume 0 for determining empty sheet ...
            'create list to hold workorders
            Dim woList As New List(Of WorkOrder)
            'get selected location from dropdown
            Dim locaID As Guid = New Guid(cbLocations.SelectedValue.ToString)
            For rownumber As Integer = 1 To workSheet.Dimension.[End].Row 'look at each row
                For colNumber As Integer = 1 To workSheet.Dimension.[End].Column 'and in each column of each row
                    arrayoutput(rownumber - 1, colNumber - 1) = workSheet.Cells(rownumber, colNumber).Text 'store the content in that array slot
                Next 'go to next column
            Next 'go to next row
            '**************sheet has been stored to array
            'grab workorder fields
            'try
            For i = startrow To workSheet.Dimension.[End].Row - 1
                If arrayoutput(i, startcol) = String.Empty Then
                    lblresult.Text = "Starting cell is empty"
                    Continue For
                    'Exit For 'first col empty move to next row
                End If
                'something before exit
                'if insert
                wo = New WorkOrder 'with default values
                wo.ID = Guid.NewGuid 'reset the wo id

                lst = GetImportData(vConfigID) 'same as configlst @ 109

                'search the config data for each field in the workorder
                Dim m As importConfigDataItem
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "LogDate")
                If Not m Is Nothing Then 'there is data
                    If m.columnLetter > "" Then 'there is column selected
                        Dim vlogdate As String = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                        'get data from array

                        wo.LogDate = vlogdate 'store it to workorder
                    ElseIf m.DefaultValue > "" And IsDate(m.DefaultValue) Then 'if default has value and it is a date
                        wo.LogDate = m.DefaultValue 'store it to workorder
                    End If
                End If
                If RadDatePicker2.Visible And Not RadDatePicker2.SelectedDate Is Nothing Then
                    wo.LogDate = RadDatePicker2.SelectedDate
                End If

                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Status")
                If Not m Is Nothing Then 'there is data
                    If m.columnLetter > "" Then 'there is data
                        wo.Status = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                    ElseIf m.DefaultValue > "" Then
                        wo.Status = CType(m.DefaultValue, Integer)
                    Else 'no columnLetter

                    End If

                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "LogNumber")
                If Not m Is Nothing Then
                    If m.DefaultValue > "" Then
                        wo.LogNumber = CType(m.DefaultValue, Integer)
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "LoadNumber")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        Dim vloadNumber As String = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                        wo.LoadNumber = vloadNumber 'IIf(IsNumeric(vloadNumber), CType(vloadNumber, Integer), -1)
                    ElseIf m.DefaultValue > "Calculated" Then
                        'comes calculated in NEW workorder
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Department")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        wo.Department = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                        wo.DepartmentID = ldal.getDepartmenIDByName(wo.Department)
                    ElseIf Utilities.IsValidGuid(m.DefaultValue) Then
                        wo.DepartmentID = New Guid(m.DefaultValue)
                    Else
                        wo.DepartmentID = Utilities.zeroGuid
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "LoadType")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        wo.LoadType = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                        wo.LoadTypeID = ldal.getLoadTypeIDByName(wo.LoadType)
                    ElseIf m.DefaultValue > "" Then
                        wo.LoadTypeID = New Guid(m.DefaultValue)
                    Else
                        wo.DepartmentID = Utilities.zeroGuid
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Vendor")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        'for ocala
                        wo.VendorName = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                        If Left(wo.VendorName, 3) = "DOT" Then
                            wo.VendorName = "DOT FOODS INC"
                        End If
                        If wo.VendorName > "" Then
                            If wo.VendorName.Contains("(") Then 'has a vendor number
                                wo.VendorName = Left(wo.VendorName, wo.VendorName.Length - InStr(wo.VendorName, "("))
                            End If
                            wo.VendorNumber = ldal.getVendorNumberByLocaIDAndName(locaID.ToString, wo.VendorName)
                        End If
                    Else
                        wo.VendorNumber = String.Empty
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "VendorNumber")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        wo.VendorNumber = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                        If IsNumeric(wo.VendorNumber) Then
                            wo.VendorName = ldal.getVendorNameByLocaIDAndNumber(locaID.ToString, wo.VendorNumber)
                        End If
                    End If

                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "CustomerID")
                If Not m Is Nothing Then
                    wo.CustomerID = Utilities.zeroGuid
                End If
                If wo.VendorNumber Is Nothing Then
                    wo.CustomerID = Utilities.zeroGuid
                Else
                    wo.CustomerID = wodal.getCustomerID(locaID, wo.VendorNumber)
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "PurchaseOrder")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        wo.PurchaseOrder = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                    ElseIf m.DefaultValue > "" Then
                        wo.PurchaseOrder = m.DefaultValue
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Amount")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        Dim vamount As String = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                        If IsNumeric(vamount) Then
                            wo.Amount = CType(vamount, Decimal)
                        End If
                    End If
                    '                    wo.Amount = m.canUpdate
                    '                    wo.Amount = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                    '                    wo.Amount = String.Empty
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "IsCash")
                If Not m Is Nothing Then
                    wo.IsCash = wo.LoadType = "Cash"
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "LoadDescription")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        Dim vloadDescription As String = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                        wo.LoadDescription = vloadDescription
                        wo.LoadDescriptionID = ldal.GetLoadDescriptionIDByName(locaID, wo.LoadDescription)
                    ElseIf m.DefaultValue > "" Then
                        wo.LoadDescriptionID = New Guid(m.DefaultValue)
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Carrier")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        Dim vcarrier As String = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                        wo.CarrierID = Utilities.zeroGuid 'm.columnLetter
                        'to do
                        'lookup carrier
                    ElseIf m.DefaultValue > "" Then
                        wo.CarrierID = New Guid(m.DefaultValue)
                    Else
                        wo.CarrierID = Utilities.zeroGuid 'm.columnLetter
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "TruckNumber")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        wo.TruckNumber = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                    ElseIf m.DefaultValue > "" Then
                        wo.TruckNumber = m.DefaultValue
                    Else
                        wo.TruckNumber = String.Empty 'm.columnLetter
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "TrailerNumber")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        wo.TrailerNumber = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                    ElseIf m.DefaultValue > "" Then
                        wo.TrailerNumber = m.DefaultValue
                    Else
                        wo.TrailerNumber = String.Empty 'm.columnLetter
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "AppointmentTime")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        Dim vappointmenttime As String = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                        If IsDate(vappointmenttime) Then
                            DatePart(DateInterval.Hour, CDate(vappointmenttime))
                            DatePart(DateInterval.Minute, CDate(vappointmenttime))
                        End If
                        wo.AppointmentTime = DateAdd(DateInterval.Hour, DatePart(DateInterval.Hour, CDate(vappointmenttime)), wo.LogDate)
                        wo.AppointmentTime = DateAdd(DateInterval.Minute, DatePart(DateInterval.Minute, CDate(vappointmenttime)), wo.AppointmentTime)
                    Else
                        If m.DefaultValue.Length = 5 Then
                            Dim strappointmenttime As String = wo.LogDate.ToShortDateString & " " & m.DefaultValue
                            wo.AppointmentTime = CDate(strappointmenttime)
                        Else
                            wo.AppointmentTime = m.DefaultValue
                        End If
                    End If
                End If

                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "GateTime")
                If Not m Is Nothing Then
                    If m.DefaultValue.Length = 5 Then
                        Dim strGateTime As String = wo.LogDate.ToShortDateString & " " & m.DefaultValue
                        wo.GateTime = CDate(strGateTime)
                    Else
                        wo.GateTime = m.DefaultValue
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "DockTime")
                If Not m Is Nothing Then
                    If m.DefaultValue.Length = 5 Then
                        Dim strDockTime As String = wo.LogDate.ToShortDateString & " " & m.DefaultValue
                        wo.DockTime = CDate(strDockTime)
                    Else
                        wo.DockTime = m.DefaultValue
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "StartTime")
                If Not m Is Nothing Then
                    If m.DefaultValue.Length = 5 Then
                        Dim strStartTime As String = wo.LogDate.ToShortDateString & " " & m.DefaultValue
                        wo.StartTime = CDate(strStartTime)
                    Else
                        wo.StartTime = m.DefaultValue
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "CompTime")
                If Not m Is Nothing Then
                    If m.DefaultValue.Length = 5 Then
                        Dim strCompTime As String = wo.LogDate.ToShortDateString & " " & m.DefaultValue
                        wo.CompTime = CDate(strCompTime)
                    Else
                        wo.CompTime = m.DefaultValue
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "PalletsUnloaded")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        Dim vPalletsUnloaded As String = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                        If IsNumeric(vPalletsUnloaded) Then
                            wo.PalletsUnloaded = CType(vPalletsUnloaded, Integer)
                        End If
                    ElseIf m.DefaultValue > "" Then
                        If IsNumeric(m.DefaultValue) Then
                            wo.PalletsUnloaded = CType(m.DefaultValue, Integer)
                        End If
                    Else
                        '-1 set by NEW
                    End If
                End If

                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "DoorNumber")
                If Not m Is Nothing Then
                    If m.DefaultValue > "" Then
                        wo.DoorNumber = m.DefaultValue

                    Else
                        wo.DoorNumber = String.Empty
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Pieces")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        Dim vpieces As String = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                        If IsNumeric(vpieces) Then
                            wo.Pieces = CType(vpieces, Integer)
                        ElseIf m.DefaultValue > "" Then
                            If IsNumeric(m.DefaultValue) Then
                                wo.Pieces = CType(m.DefaultValue, Integer)
                            End If
                        End If
                        '                    wo.Amount = m.canUpdate
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Weight")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        Dim vWeight As String = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                        If IsNumeric(vWeight) Then
                            wo.Weight = CType(vWeight, Integer)
                        ElseIf m.DefaultValue > "" Then
                            If IsNumeric(m.DefaultValue) Then
                                wo.Weight = CType(m.DefaultValue, Integer)
                            End If
                        End If
                    Else
                        If IsNumeric(m.DefaultValue) Then
                            wo.Weight = CType(m.DefaultValue, Integer)
                        End If
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Restacks")
                If Not m Is Nothing Then
                    If m.DefaultValue > "" Then
                        If IsNumeric(m.DefaultValue) Then
                            wo.Restacks = CType(m.DefaultValue, Integer)
                        End If
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "PalletsReceived")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        'get from array
                        Dim vPalletsReceived As String = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                        If IsNumeric(vPalletsReceived) Then
                            wo.PalletsReceived = CType(vPalletsReceived, Integer)
                        Else
                            If IsNumeric(m.DefaultValue) Then
                                wo.PalletsReceived = CType(m.DefaultValue, Integer)
                            End If
                        End If
                    Else 'no columnLetter
                        If IsNumeric(m.DefaultValue) Then
                            wo.PalletsReceived = CType(m.DefaultValue, Integer)
                        End If
                    End If
                Else
                    If IsNumeric(m.DefaultValue) Then
                        wo.PalletsReceived = CType(m.DefaultValue, Integer)
                    End If
                End If
                'wo.PalletsReceived = m.canUpdate
                'wo.PalletsReceived = m.columnLetter
                'wo.PalletsReceived = String.Empty
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "BadPallets")
                If Not m Is Nothing Then
                    If m.DefaultValue > "" Then
                        If IsNumeric(m.DefaultValue) Then
                            wo.BadPallets = CType(m.DefaultValue, Integer)
                        End If
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "NumberOfItems")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        Dim vNumberOfItems As String = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                        If IsNumeric(vNumberOfItems) Then
                            wo.NumberOfItems = CType(vNumberOfItems, Integer)
                        Else
                            If IsNumeric(m.DefaultValue) Then
                                wo.NumberOfItems = CType(m.DefaultValue, Integer)
                            End If
                        End If
                    Else 'no columnLetter
                        If IsNumeric(m.DefaultValue) Then
                            wo.NumberOfItems = CType(m.DefaultValue, Integer)
                        End If
                    End If

                End If
                'wo.NumberOfItems = m.canUpdate
                'wo.NumberOfItems = m.columnLetter
                'wo.NumberOfItems = String.Empty
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "CheckNumber")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        wo.CheckNumber = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                    End If
                    wo.CheckNumber = String.Empty
                Else
                    wo.CheckNumber = String.Empty
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "BOL")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        wo.BOL = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                    ElseIf m.DefaultValue > "" Then
                        wo.BOL = m.DefaultValue
                    Else
                        wo.BOL = String.Empty
                    End If
                End If
                m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Comments")
                If Not m Is Nothing Then
                    If m.columnLetter > "" Then
                        wo.Comments = arrayoutput(i, utl.getLetterValue(m.columnLetter))
                    ElseIf m.DefaultValue > "" Then
                        wo.Comments = m.DefaultValue
                    Else
                        wo.Comments = String.Empty
                    End If
                End If
                wo.LocationID = New Guid(cbLocations.SelectedValue)
                Dim proc As Integer = woList.Count
                Dim ut As New Utilities
                If wo.CreatedBy = "" Then wo.CreatedBy = "Excel_" & ut.CreatedByToText(HttpContext.Current.Session("userID"))
                Try
                    woList.Add(wo) 'woList contains all workorders on the sheet
                Catch ex As Exception
                    Dim strerr As String = ex.Message
                    lblresult.Text = strerr
                    lblresult.ForeColor = System.Drawing.Color.Red
                End Try

            Next
            ' now 'woList contains all workorders on the sheet
            'create two empty lists to separate inserts from updates
            Dim updatewoList As New List(Of WorkOrder)
            Dim insertwoList As New List(Of WorkOrder)
            'separate the updates from the inserts
            For Each lwo As WorkOrder In woList
                ' check to see if  workorder already exist
                Dim dt As DataTable = wodal.doesExistByDatePoLocation(lwo.LogDate, lwo.PurchaseOrder, lwo.LocationID) 'updateme
                If Not dt Is Nothing Then 'we have datatable
                    If dt.Rows.Count > 0 Then 'the datatable has rows
                        'the val of the found workorderID
                        Dim val As String = dt.Rows(0).Item(0).ToString
                        'if the workorder exists, go get it
                        Dim exwo As WorkOrder = wodal.GetLoadByID(val) 'existing workorder exwo
                        'go thru each field and update the canUpdate fields
                        Dim m As importConfigDataItem
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "LogDate")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.LogDate = lwo.LogDate
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Status")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.Status = lwo.Status
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "LogNumber")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.LogNumber = lwo.LogNumber
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "LoadNumber")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.LoadNumber = lwo.LoadNumber
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Department")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.Department = lwo.Department
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "LoadType")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.LoadType = lwo.LoadType
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Vendor")
                        'If Not m Is Nothing Then
                        '    If m.canUpdate Then
                        '        exwo.Vendor = wo.Vendor
                        '    End If
                        'End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "VendorNumber")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.VendorNumber = lwo.VendorNumber
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "CustomerID")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.CustomerID = lwo.CustomerID
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Amount")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.Amount = lwo.Amount
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "IsCash")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.IsCash = lwo.IsCash
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "LoadDescription")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.LoadDescription = lwo.LoadDescription
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Carrier")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.CarrierID = lwo.CarrierID
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "TruckNumber")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.TruckNumber = lwo.TruckNumber
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "TrailerNumber")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.TrailerNumber = lwo.TrailerNumber
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "AppointmentTime")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.AppointmentTime = lwo.AppointmentTime
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "GateTime")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.GateTime = lwo.GateTime
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "DockTime")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.DockTime = lwo.DockTime
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "StartTime")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.StartTime = lwo.StartTime
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "CompTime")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.CompTime = lwo.CompTime
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "PalletsUnloaded")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.PalletsUnloaded = lwo.PalletsUnloaded
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "DoorNumber")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.DoorNumber = lwo.DoorNumber
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Pieces")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.Pieces = lwo.Pieces
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Restacks")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.Restacks = lwo.Restacks
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "PalletsReceived")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.PalletsReceived = lwo.PalletsReceived
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "BadPallets")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.BadPallets = lwo.BadPallets
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "NumberOfItems")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.NumberOfItems = lwo.NumberOfItems
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "CheckNumber")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.CheckNumber = lwo.CheckNumber
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "BOL")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.BOL = lwo.BOL
                            End If
                        End If
                        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Comments")
                        If Not m Is Nothing Then
                            If m.canUpdate Then
                                exwo.Comments = lwo.Comments
                            End If
                        End If
                        updatewoList.Add(exwo)  'then add this found/updated wo to list to be update

                    Else                        'this wo was not found in db, add to insert List
                        Try
                            insertwoList.Add(lwo)
                        Catch ex As Exception
                            Dim strerr As String = ex.Message
                            lblresult.Text = strerr
                            lblresult.ForeColor = System.Drawing.Color.Red
                        End Try
                    End If 'we found wo in existing
                End If 'the dt looking for the existing was not nothing

            Next        'go to  next workorder
            '

            If vImportType <> 2 Then
                For Each iwo As WorkOrder In insertwoList
                    Try
                        Dim didadd As String = wodal.AddWorkOrder(iwo)
                    Catch ex As Exception
                        Dim strerr As String = ex.Message
                        lblresult.Text = strerr
                        lblresult.ForeColor = System.Drawing.Color.Red
                    End Try
                Next
            End If
            If vImportType > 1 Then
                For Each uwo As WorkOrder In updatewoList
                    Try
                        wodal.UpdateWorkOrder(uwo, False)
                    Catch ex As Exception
                        Dim strerr As String = ex.Message
                        lblresult.Text = strerr
                        lblresult.ForeColor = System.Drawing.Color.Red
                    End Try
                Next
            End If
            Dim skipped As String = String.Empty
            lblresult.Text &= "<table style='border:1px solid black;'><tr><td style='color:black'> " & workSheet.Name & " </td><td style='color:black'> " & woList.Count & " workorders processed</td></tr>"
            If vImportType = 2 Then skipped = "... ingnored, Update Only"
            lblresult.Text &= "<tr><td style='color:Blue'>INSERTED</td><td style='color:Blue'>" & insertwoList.Count & skipped & "</td</tr>"
            skipped = String.Empty
            If vImportType = 1 Then skipped = "... ingnored, Import Only"
            lblresult.Text &= "<tr><td style='color:Green'>Updated</td><td style='color:Green'>" & updatewoList.Count & skipped & "</td</tr>"
        Else
            lblresult.Text &= "<tr><td style='color:red'colspan=2>--Empty Sheet </td></tr>"
        End If
        lblresult.Text &= "</table>"

    End Sub

    Private Sub getimportConfigInfo()
        If cbImportName.SelectedIndex > -1 Then
            Dim dba As New DBAccess
            dba.CommandText = "Select ConfigName,hasDate,StartRow,ImportType from importConfig WHERE ConfigID=@ConfigID"
            dba.AddParameter("@ConfigID", cbImportName.SelectedValue)
            Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
            If Not dt Is Nothing Then
                For Each dtRow As DataRow In dt.Rows
                    vConfigID = cbImportName.SelectedValue
                    vConfigName = dtRow.Item("ConfigName")
                    vhasDate = dtRow.Item("hasDate")
                    vStartRow = dtRow.Item("StartRow")
                    vImportType = dtRow.Item("ImportType")
                Next
                RadDatePicker2.Visible = Not vhasDate
            End If
        End If
    End Sub

    Private Function GetImportData(ByVal cfgid As String) As List(Of importConfigDataItem)
        Dim retlist As New List(Of importConfigDataItem)
        Dim icdi As importConfigDataItem = New importConfigDataItem
        Dim dba As New DBAccess
        dba.CommandText = "SELECT * FROM ImportConfigData WHERE ConfigID=@ConfigID"
        dba.AddParameter("@ConfigID", cfgid)
        Dim dt As DataTable = New DataTable
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception
            Dim exmsg As String = ex.Message
            Dim strerr As String = ex.Message
            lblresult.Text = strerr
            lblresult.ForeColor = System.Drawing.Color.Red
        End Try
        For Each row In dt.Rows
            icdi = New importConfigDataItem
            icdi.FieldName = row.item("FieldName")
            icdi.canUpdate = row.item("canUpdate")
            icdi.columnLetter = row.item("columnLetter")
            icdi.DefaultValue = row.item("DefaultValue")
            retlist.Add(icdi)
        Next
        Return retlist
    End Function

    Private Sub AsyncUpload1_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles AsyncUpload1.FileUploaded

        lblresult.Text = "<table><tr><td class='padresult'>uploaded filename:</td><td class='padresult'> " & e.File.FileName & "</td></tr>"
        Dim path As String = Server.MapPath("~\upload\")
        Try
            filename = Guid.NewGuid.ToString & e.File.GetExtension
            e.File.SaveAs(path + filename)
            lblresult.Text = filename & "<br/>"

        Catch ex As Exception
            Dim strerr As String = ex.Message
            lblresult.Text = strerr
            lblresult.ForeColor = System.Drawing.Color.Red
        End Try
    End Sub

    Private Sub RadDatePicker2_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs) Handles RadDatePicker2.SelectedDateChanged
        If IsDate(RadDatePicker2.SelectedDate) Then
            divupload.Visible = True
        End If
    End Sub


End Class
