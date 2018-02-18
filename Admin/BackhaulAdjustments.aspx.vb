Imports OfficeOpenXml
Imports System.Collections.Generic
'Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports Telerik.Web.UI
'Imports OfficeOpenXml.Packaging
Imports System.IO

Public Class BackhaulAdjustments
    Inherits System.Web.UI.Page

    Dim filename As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
        Dim ldal As New locaDAL
        If Not IsPostBack Then
            divul.Visible = cbLocations.SelectedIndex > -1
            ldal.setLocaCombo(puser, cbLocations)
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
        End If
    End Sub

    Protected Sub btnImport_Click1(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim path As String = Server.MapPath("~\upload\")
        Dim xfile As Stream = OpenFile(path & filename)
        Dim package As New ExcelPackage(xfile)
        xfile.Close()
        '        Dim ArrayOutput As String(,)
        Dim i As Integer
        For i = 1 To package.Workbook.Worksheets.Count
            ConvertExcelToArray(package, i)
        Next
        FileIO.FileSystem.DeleteFile(path + filename)
    End Sub

    Public Sub ConvertExcelToArray(ByVal package As ExcelPackage, ByVal sheetnum As Integer)
        ' Get the first sheet of the excel file
        Dim workSheet As ExcelWorksheet = package.Workbook.Worksheets.Item(sheetnum)
        Dim arrayoutput As String(,)
        If Not workSheet.Dimension Is Nothing Then
            'Set the array limit 
            arrayoutput = New String(workSheet.Dimension.[End].Row - 1, workSheet.Dimension.[End].Column - 1) {}
            Dim startrow As Integer = numstartRow.Value - 1
            Dim utls As New Utilities
            Dim startcol As Integer = utls.getLetterValue(txtstartcolumn.Text) - 1
            Dim wo As WorkOrder = New WorkOrder
            Dim woList As New List(Of WorkOrder)
            Dim locaID As Guid = New Guid(cbLocations.SelectedValue.ToString)
            ' Iterate each cell and assign into array
            For rownumber As Integer = 1 To workSheet.Dimension.[End].Row
                For colNumber As Integer = 1 To workSheet.Dimension.[End].Column
                    arrayoutput(rownumber - 1, colNumber - 1) = workSheet.Cells(rownumber, colNumber).Text
                Next
            Next
            'grab workorder fields
            Dim wodal As New WorkOrderDAL
            For i = startrow To workSheet.Dimension.[End].Row
                If arrayoutput(i, startcol) = String.Empty Then Exit For
                wo = New WorkOrder
                wo.ID = Guid.NewGuid
                Dim varPurchaseOrder As String = arrayoutput(i, startcol)
                varPurchaseOrder = Right(varPurchaseOrder, varPurchaseOrder.Length - 5)
                wo.PurchaseOrder = varPurchaseOrder.Trim()

                Dim varVendorName As String = arrayoutput(i, startcol + 2)
                Dim nvcp As Integer = InStrRev(varVendorName, ")") '13
                Dim nvop As Integer = InStrRev(varVendorName, "(") '10
                Dim nv1op As Integer = InStr(varVendorName, "(") '6

                If nvcp > 0 And nv1op > 0 Then
                    varVendorName = Left(varVendorName, nv1op - 1)
                Else
                    wo.VendorName = varVendorName.Trim()
                End If

                Dim varVendorNumber As String = arrayoutput(i, startcol + 2)
                If nvcp > 0 And nv1op > 0 Then
                    varVendorNumber = Left(varVendorNumber, nvcp - 1)
                    varVendorNumber = Right(varVendorNumber, varVendorNumber.Length - nvop)
                    wo.VendorNumber = varVendorNumber
                Else
                    wo.VendorNumber = "0"
                End If

                Dim varPieces = arrayoutput(i, startcol + 5)
                wo.Pieces = varPieces

                Dim varAmount = arrayoutput(i, startcol + 7)
                wo.Amount = varAmount
                ' TO DO
                wo.LogDate = RadDatePicker1.SelectedDate
                wo.LoadNumber = Nothing '''''''''''''
                Dim mo As String = Month(Date.Now)
                Dim da As String = Day(Date.Now)
                Dim min As String = Minute(Date.Now)
                Dim ms As String = Left(i.ToString, 4)
                Dim LoadNumber As String = mo & da & min & ms
                wo.Department = "General"
                wo.DepartmentID = New Guid("27D94AF9-4E89-476B-B310-7D2C48165D7D")
                wo.LoadType = "Backhaul"
                wo.LoadTypeID = New Guid("0369F50A-52CA-4C97-8323-650ADC182E04")
                If wo.VendorNumber = "" Then
                    wo.CustomerID = Utilities.zeroGuid
                Else
                    wo.CustomerID = wodal.getCustomerID(wo.LocationID, wo.VendorNumber)
                End If
                wo.LoadDescription = "BreakDown"
                wo.LoadDescriptionID = New Guid("C0C43203-7372-418E-B793-3121CD93FBE3")
                wo.CarrierID = New Guid("820D004D-B4D8-46D7-917B-7BBAC8D7D492")
                wo.CarrierName = "CHENEY BROTHERS"
                wo.TruckNumber = "DROP"
                wo.TrailerNumber = "DOCK"
                wo.AppointmentTime = "1/1/1900"
                wo.PalletsUnloaded = -1
                wo.NumberOfItems = -1
                wo.BOL = ""
                wo.Comments = ""
                wo.Comments = ""
                wo.PalletsReceived = -1
                wo.Weight = wo.Pieces * 3

                'from default dbvalues txt values
                wo.LocationID = New Guid(cbLocations.SelectedValue)
                wo.Status = 128
                wo.LogNumber = 0
                wo.ReceiptNumber = -1
                wo.IsCash = False
                wo.GateTime = "1/1/1900"
                wo.DockTime = RadDatePicker1.SelectedDate
                wo.DockTime = DateAdd(DateInterval.Hour, 13, wo.DockTime)
                wo.StartTime = RadDatePicker1.SelectedDate
                wo.StartTime = DateAdd(DateInterval.Hour, 13, wo.StartTime)
                wo.CompTime = RadDatePicker1.SelectedDate
                wo.CompTime = DateAdd(DateInterval.Hour, 14, wo.CompTime)
                wo.DoorNumber = "DOCK"
                wo.Restacks = 0
                wo.BadPallets = 0
                wo.CheckNumber = ""
                woList.Add(wo)
            Next
            Dim updatewoList As New List(Of WorkOrder)
            Dim insertwoList As New List(Of WorkOrder)
            For Each lwo As WorkOrder In woList
                Dim dt As DataTable = wodal.doesBackhaulExistByDatePoLocation(lwo.LogDate, lwo.PurchaseOrder, lwo.LocationID) 'updateme
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        Dim val As String = dt.Rows(0).Item(0).ToString
                        Dim exwo As WorkOrder = wodal.GetLoadByID(val)
                        exwo.Amount = lwo.Amount
                        exwo.Pieces = lwo.Pieces
                        updatewoList.Add(exwo)
                    Else ' insertme
                        insertwoList.Add(lwo)
                    End If
                End If
            Next
            For Each iwo As WorkOrder In insertwoList
                wodal.AddWorkOrder(iwo)
            Next
            For Each uwo As WorkOrder In updatewoList
                wodal.UpdateWorkOrder(wo, False)
            Next

            lblresult.Text &= "<tr><td style='color:Green'>--Processed </td><td style='color:Green'> " & woList.Count & " workorders</td></tr>"
            lblresult.Text &= "<tr><td style='color:Green'>Updated</td><td style='color:Green'>" & updatewoList.Count & "</td</tr>"
            Dim lst As String = String.Empty
            For Each nwo As WorkOrder In insertwoList
                lst &= nwo.PurchaseOrder & "<br />"
            Next
            lblresult.Text &= "<tr><td>INSERTED</td><td>" & insertwoList.Count & " workorders </td</tr>"
            If insertwoList.Count > 0 Then
                lblresult.Text &= "<tr><td valign='top'>PO Numbers</td><td>" & lst & "</td></tr>"
            End If
        Else
            lblresult.Text &= "<tr><td style='color:red'colspan=2>--Empty Sheet </td></tr>"
        End If
        lblresult.Text &= "</table"

    End Sub

    Private Sub AsyncUpload1_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles AsyncUpload1.FileUploaded
        If cbLocations.SelectedIndex = -1 Then
            lblerrLocation.Visible = True
            e.File.SaveAs(Nothing)
            lblresult.Text = "Select Location First, then Try Again"
            Exit Sub
        Else
            lblerrLocation.Visible = False
        End If

        lblresult.Text = "<table><tr><td class='padresult'>uploaded filename:</td><td class='padresult'> " & e.File.FileName & "</td></tr>"
        Dim path As String = Server.MapPath("~\upload\")
        Try
            filename = Guid.NewGuid.ToString & e.File.GetExtension
            e.File.SaveAs(path + filename)
        Catch ex As Exception
            lblresult.Text = ex.Message
        End Try
    End Sub

    Private Sub cbLocations_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        If RadDatePicker1.SelectedDate > DateAdd(DateInterval.Day, -90, Date.Now) Then
            divul.Visible = True
            lblerrLocation.Visible = False
        Else
            lblerrLocation.Text = "Select Date"
            lblerrLocation.Visible = True
        End If
    End Sub

    Private Sub RadDatePicker1_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs) Handles RadDatePicker1.SelectedDateChanged
        If cbLocations.Text > "" Then
            divul.Visible = True
            lblerrLocation.Visible = False
        Else
            lblerrLocation.Text = "Select Location"
            lblerrLocation.Visible = True
        End If
    End Sub
End Class
