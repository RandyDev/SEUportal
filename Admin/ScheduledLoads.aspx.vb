Imports System.IO
'Imports Telerik.Web.UI

Public Class ScheduledLoads
    Inherits System.Web.UI.Page
    Public counter As Integer
    Private timeOut As Integer
    Public rowcounter As Integer = 0
    Public myLocaLoadList As List(Of locaFolder)

    Private Sub ScheduledLoads_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        timeOut = Server.ScriptTimeout
        Server.ScriptTimeout = 3600
        RadScriptManager1.AsyncPostBackTimeout = 3600

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            myLocaLoadList = New List(Of locaFolder)
            myLocaLoadList = getScheduledLoadList()
            Session("loadlist") = Nothing
            Session("loadlist") = myLocaLoadList

            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
        Else
            myLocaLoadList = Session("loadlist")
        End If

        If Not Session("loadlist") Is Nothing Then
            RenderTable(myLocaLoadList)
        End If


    End Sub

    Protected Function getScheduledLoadList() As List(Of locaFolder)
        Dim sldDirName As String = Server.MapPath("~/ScheduledLoadData/")
        Dim locaFolderList As List(Of locaFolder) = New List(Of locaFolder)
        Dim fID As Integer = 0
        Dim locaName As String = String.Empty
        Dim doBut As Boolean = True
        If cbLocations IsNot Nothing Then
            If cbLocations.SelectedIndex > -1 Then
                locaName = cbLocations.SelectedItem.Text
            Else
                locaName = ""
            End If
        End If

        For Each Dir As String In Directory.GetDirectories(sldDirName)
            Dim folderName As String = Right(Dir, Dir.Length - InStrRev(Dir, "\"))

            If (locaName > "" And folderName = locaName) Or cbLocations.SelectedIndex = -1 Then


                If folderName <> "backup" Then
                    Dim vlocaFolder As locaFolder = New locaFolder()
                    fID += 1
                    vlocaFolder.ID = fID
                    vlocaFolder.locaName = folderName
                    ' go get location
                    Dim ldal As New locaDAL
                    Dim loca As Location = ldal.getLocationByName(folderName)
                    If Utilities.IsValidGuid(loca.ID.ToString) Then    'This IS a valid location folder
                        Dim di As DirectoryInfo = New DirectoryInfo(sldDirName & folderName)
                        Dim files As FileSystemInfo() = di.GetFileSystemInfos()
                        counter = 0
                        If files.Length > 0 Then    'there are files in the folder
                            Dim locaFileList As List(Of locaFile) = New List(Of locaFile)
                            'sort by date last written
                            Dim orderedFiles = files.OrderBy(Function(f) f.LastWriteTime)
                            'get the last one
                            Dim fl As FileSystemInfo = orderedFiles(orderedFiles.Count - 1)
                            'Dim haveMostRecent As Boolean = False
                            'step backwards from bottom of file list newest first
                            For ii As Integer = orderedFiles.Count - 1 To 0 Step -1
                                Dim lfile As locaFile = New locaFile()
                                lfile.fName = orderedFiles(ii).Name
                                lfile.fPath = orderedFiles(ii).FullName
                                lfile.fDate = orderedFiles(ii).LastWriteTime
                                lfile.isDone = False
                                If orderedFiles(ii).Extension.ToLower = ".txt" Then 'And Not haveMostRecent Then

                                    Dim arImportedLine As String = String.Empty
                                    Dim srImport As New StreamReader(orderedFiles(ii).FullName)
                                    arImportedLine = srImport.ReadLine
                                    arImportedLine = srImport.ReadLine
                                    srImport.Close()
                                    Dim arImportedItem As String() = Split(arImportedLine, vbTab)
                                    If IsDate(arImportedItem(0)) Then   'first position in second line is a date
                                        lfile.fLoadDate = FormatDateTime(arImportedItem(0), DateFormat.ShortDate)

                                    Else    'first position in second line is not a date
                                        lfile.fileErrMsg = "Unable to parse/read this text file"
                                    End If  'first position in second line is a date
                                Else 'not a text file
                                    lfile.fileErrMsg = "Not a load file (Must be '.txt' file)"
                                End If  'is this a .txt file

                                locaFileList.Add(lfile)   'add this file to the list of files
                            Next    'file in this folder

                            vlocaFolder.locaFiles = locaFileList

                        Else 'there are no files in folder
                            vlocaFolder.locaErrMsg = "nothing to do - no files found"
                        End If      'there are files in the folder
                    Else    'this is not a valid location folder
                        vlocaFolder.locaErrMsg = "Folder name does not match a location"
                    End If  'this is a valid location folder
                    '                slFolder.locaFiles.Add(lfile)
                    '                LocaList.LocaList.Add(slFolder)
                    locaFolderList.Add(vlocaFolder)
                End If      'folderName <> "backup"
            Else
            End If
        Next            'folder in sldDirName folder (ftp site)
        Return locaFolderList
    End Function

    Protected Sub RenderTable(ByVal loadList As List(Of locaFolder))
        '    Dim sldDirName As String = Server.MapPath("~/ScheduledLoadData/")
        ph1.Controls.Clear()
        '        Dim table As Table
        Dim tblScheduledLoads As Table
        Dim row As TableRow
        Dim cell As TableCell

        Dim hrow As TableHeaderRow
        Dim hcell As TableHeaderCell
        Dim btn As Button
        '        tblScheduledLoads.Rows.Clear()
        Dim i As Integer = 0

        '        table = New Table
        tblScheduledLoads = New Table
        tblScheduledLoads.CellPadding = 5
        tblScheduledLoads.ID = "tblScheduledLoads"
        '    '        table = CType(Me.FindControl("tblScheduledLoads"), Table)
        tblScheduledLoads.EnableViewState = True
        tblScheduledLoads.BorderStyle = BorderStyle.Solid
        tblScheduledLoads.BorderWidth = 1
        tblScheduledLoads.Style("border-collapse") = "collapse"
        tblScheduledLoads.Style("text-align") = "left"
        tblScheduledLoads.Attributes.Add("border", "1")
        rowcounter = 0
        hrow = New TableHeaderRow
        hrow.Style("text-align") = "center"
        hrow.Style("font-weight") = "bold"
        hcell = New TableHeaderCell
        hcell.Text = "Location"
        hrow.Cells.Add(hcell)
        hcell = New TableHeaderCell
        hcell.Text = "Filename"
        hrow.Cells.Add(hcell)
        hcell = New TableHeaderCell
        hcell.Text = "Date"
        hrow.Cells.Add(hcell)
        hcell = New TableHeaderCell
        hcell.Text = "Proc File"
        hrow.Cells.Add(hcell)

        tblScheduledLoads.Rows.Add(hrow)

        For Each vlocaFolder As locaFolder In loadList
            Dim vlocaName As String = vlocaFolder.locaName
            If vlocaFolder.locaErrMsg Is Nothing Then
                For Each vlocaFile As locaFile In vlocaFolder.locaFiles
                    If vlocaFile.fileErrMsg Is Nothing Or vlocaFile.isDone Then         'is it a load file
                        row = New TableRow

                        cell = New TableCell
                        cell.Text = vlocaFolder.locaName
                        row.Cells.Add(cell)

                        cell = New TableCell
                        'cell.Style("background-color") = "#FED329"
                        cell.Text = vlocaFile.fName
                        row.Cells.Add(cell)

                        cell = New TableCell
                        '                        cell.Style("background-color") = "#FED329"
                        cell.Font.Size = 9
                        cell.Text = "<control>TimeStamp:</control> " & vlocaFile.fDate.ToString & "<br/><control>Load Date</control>: " & vlocaFile.fLoadDate
                        row.Cells.Add(cell)

                        cell = New TableCell
                        If vlocaFile.fLoadDate = FormatDateTime(Date.Now(), DateFormat.ShortDate) Then
                            If Not vlocaFile.isDone Then
                                btn = New Button()
                                btn.CommandArgument = vlocaFile.fPath
                                btn.CommandName = vlocaFolder.locaName & ":" & rowcounter.ToString
                                btn.Text = "Process this file"
                                AddHandler btn.Command, AddressOf btn_Command
                                cell.Controls.Add(btn)
                            Else
                                cell.ForeColor = Drawing.Color.Green
                                cell.Text = "Import Complete"
                            End If
                        ElseIf vlocaFile.fLoadDate > FormatDateTime(Date.Now(), DateFormat.ShortDate) Then
                            cell.Text = "not yet due"
                        ElseIf vlocaFile.fLoadDate < FormatDateTime(Date.Now(), DateFormat.ShortDate) Then
                            cell.Text = "file past due (old file)"
                        End If

                        row.Cells.Add(cell)

                        tblScheduledLoads.Rows.Add(row)
                        rowcounter += 1
                    Else    'display vlocaFile.fileErrMsg
                        row = New TableRow
                        cell = New TableCell
                        cell.Text = vlocaFolder.locaName
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.Text = vlocaFile.fName
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.Style("border-color") = "black"
                        cell.Style("color") = "Red"
                        cell.ColumnSpan = 2
                        cell.Text = vlocaFile.fileErrMsg
                        row.Cells.Add(cell)
                        tblScheduledLoads.Rows.Add(row)
                        rowcounter += 1
                    End If

                Next


            Else ' display locaErrMsg
                row = New TableRow
                cell = New TableCell
                cell.Text = vlocaFolder.locaName
                row.Cells.Add(cell)
                cell = New TableCell
                cell.Style("border-color") = "black"
                Select Case vlocaFolder.locaErrMsg
                    Case "nothing to do - no files found"
                        cell.Style("color") = "Gray"
                    Case "Folder name does not match a location"
                        cell.Style("color") = "Purple"
                    Case Else
                        cell.Style("color") = "Red"
                End Select
                cell.ColumnSpan = 3
                cell.Text = vlocaFolder.locaErrMsg
                row.Cells.Add(cell)
                tblScheduledLoads.Rows.Add(row)
                rowcounter += 1

            End If 'locaErrMsg > ""

        Next

        If loadList.Count = 0 Then
            If cbLocations IsNot Nothing And cbLocations.SelectedIndex > -1 Then

                row = New TableRow
                cell = New TableCell
                cell.Text = cbLocations.SelectedItem.Text
                row.Cells.Add(cell)
                cell = New TableCell
                cell.ForeColor = Drawing.Color.MediumPurple
                cell.ColumnSpan = 3
                cell.Text = "The FTP folder for this location does not exist - nothing to do"
                row.Cells.Add(cell)
                tblScheduledLoads.Rows.Add(row)

            End If
        End If

        ph1.Controls.Add(tblScheduledLoads)

    End Sub

    'clear extra files in scheduled load folders
    Public Function clearFile(ByVal fileToMove As String) As String
        Dim retStr As String = String.Empty
        Dim sldDirName As String = Server.MapPath("~/ScheduledLoadData/")
        Dim fileToMoveName As String = Right(fileToMove, fileToMove.Length - InStrRev(fileToMove, "\"))
        Dim newFileToMove = sldDirName & Date.Now.ToString("yyMMdd_HH_mm_") & fileToMoveName
        File.Copy(fileToMove, newFileToMove, True)
        File.Delete(fileToMove)
        Return retStr
    End Function

    'Protected Sub getEmail()
    '    Dim popConn As New POP3
    '    Dim mailMess As New EmailMessage
    '    popConn.POPConnect("mail.realwebs.com", "seu@randydev.com", "seupassword")
    '    Dim intMessCnt As Integer = popConn.GetMailStat()
    '    For i = 1 To intMessCnt
    '        'load the entire content of the mail into a string
    '        Dim strMailContent = popConn.GetMailMessage(i)
    '        'call the functions to get the various parts out of the email 
    '        Dim strFrom = mailMess.ParseEmail(strMailContent, "From:")
    '        Dim strSubject = mailMess.ParseEmail(strMailContent, "Subject:")
    '        Dim strToo = mailMess.ParseEmail(strMailContent, "To:")
    '        Dim strBody = mailMess.ParseBody()
    '    Next i
    'End Sub

    Private Sub btn_Click(sender As Object, e As System.EventArgs)
        Dim a As String = String.Empty
    End Sub




    Private Sub btn_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs)
        myLocaLoadList = New List(Of locaFolder)
        myLocaLoadList = CType(Session("loadlist"), List(Of locaFolder))
        Dim sldDirName As String = Server.MapPath("~/ScheduledLoadData/")
        Dim ofilename As String = e.CommandArgument
        Dim folder As String = e.CommandName
        folder = Left(folder, InStr(folder, ":") - 1)

        '        Dim c As Integer = Right(e.CommandName, Len(e.CommandName) - InStr(e.CommandName, ":"))
        For Each vlocaFolder As locaFolder In myLocaLoadList
            If vlocaFolder.locaName = folder Then
                For Each vlocaFile As locaFile In vlocaFolder.locaFiles
                    If vlocaFile.fPath = ofilename Then
                        vlocaFile.isDone = True

                        '                        Dim tbl As Table = CType(ph1.FindControl("tblScheduledLoads"), Table)
                        '                        Dim row As TableRow = tbl.Rows(c + 1)
                        '                        Dim cell As TableCell = row.Cells(3)
                        '                        cell.Controls.Clear()
                        '                        cell.ForeColor = Drawing.Color.Green
                        '                        cell.Text = "Import dobee Complete"
                    End If

                Next

            End If
        Next


        Dim nfilename As String = sldDirName & "backup\" & folder & "XXXX.bak"
        '        File.Copy(ofilename, nfilename, True)
        '        File.Delete(ofilename)


        Session("loadlist") = Nothing
        Session("loadlist") = myLocaLoadList
        RenderTable(myLocaLoadList)
    End Sub



    'Private Sub btnGO_Click(sender As Object, e As System.EventArgs)
    '    lblResults.Text = Nothing
    '    Dim sldDirName As String = Server.MapPath("~/ScheduledLoadData/")
    '    lblResults.Text = "<table cellpadding=""5"" style=""border-collapse:collapse;text-align:left"" border=1><tr><th>Location</th><th>Filename</th><th>Date</th></tr>"
    '    For Each Dir As String In Directory.GetDirectories(sldDirName)
    '        Dim folderName As String = Right(Dir, Dir.Length - InStrRev(Dir, "\"))
    '        If folderName <> "backup" Then

    '            lblResults.Text &= "<tr>"
    '            ' go get location
    '            Dim ldal As New locaDAL
    '            Dim loca As Location = ldal.getLocationByName(folderName)

    '            If Utilities.IsValidGuid(loca.ID.ToString) Then
    '                'we have a live one (existing location)
    '                Dim di As DirectoryInfo = New DirectoryInfo(sldDirName & folderName)
    '                Dim files As FileSystemInfo() = di.GetFileSystemInfos()
    '                If files.Length > 0 Then
    '                    'sort by date last written
    '                    Dim orderedFiles = files.OrderBy(Function(f) f.LastWriteTime)
    '                    'get the last one
    '                    Dim fl As FileSystemInfo = orderedFiles(orderedFiles.Count - 1)
    '                    Dim ofilename As String = String.Empty
    '                    Dim nfilename As String = String.Empty
    '                    For ii As Integer = orderedFiles.Count - 1 To 0 Step -1
    '                        If orderedFiles(ii).Extension = ".txt" Then
    '                            ofilename = orderedFiles(ii).FullName
    '                            nfilename = sldDirName & "backup\" & folderName & Date.Now.ToString("_yyMMdd_HH_mm") & ".bak"
    '                            Try

    '                                OpenScheduledLoadCVSfile(ofilename, loca.Name)                                'proccess this file

    '                                File.Copy(ofilename, nfilename, True)
    '                                File.Delete(ofilename)


    '                                lblResults.Text &= "<td>" & folderName & " </td><td style=""background-color:00FF00;""> " & orderedFiles(ii).Name & " </td><td style=""background-color:#00FF00;""> Import Completed </td>"
    '                            Catch ex As Exception
    '                                lblResults.Text &= "<td>" & folderName & " </td><td> " & orderedFiles(ii).Name & " </td><td style=""background-color:#E76868;""> ooophf!" & "</td>"
    '                            End Try
    '                            Exit For
    '                        End If
    '                        If ii = 0 Then lblResults.Text &= "<td>" & folderName & " </td><td style=""background-color:#EEEEEE;"" colspan='2'> Nothing to do </td>"
    '                    Next
    '                Else
    '                    lblResults.Text &= "<td>" & folderName & " </td><td style=""background-color:#EEEEEE;"" colspan='2'> Nothing to do </td>"
    '                End If
    '            Else
    '                lblResults.Text &= "<td>" & folderName & " </td><td colspan='2' style=""background-color:#EEEEEE;""> Not a location </td>"
    '            End If
    '            lblResults.Text &= "</tr>"
    '        End If
    '    Next
    '    lblResults.Text &= "</table>"
    'End Sub


    Protected Function OpenScheduledLoadCVSfile(ByVal fileName As String, ByVal locaName As String) As List(Of ScheduledLoad)
        Dim slList As List(Of ScheduledLoad) = New List(Of ScheduledLoad)
        Dim arImportedLine(0) As String 'each index holds line from import file
        Dim ftpFileName As String = fileName
        ' *** open the file
        Dim srImport As New StreamReader(ftpFileName)
        Dim lineCount As Integer = 0
        Dim comments As String = String.Empty
        Dim woList As List(Of WorkOrder) = New List(Of WorkOrder)

        Do While srImport.Peek <> -1
            ReDim Preserve arImportedLine(lineCount)
            arImportedLine(lineCount) = srImport.ReadLine

            ' remove quotes or make other adjustments
            '            arImportedLine(lineCount) = Replace(arImportedLine(lineCount), """", "")

            ' create array of line being processed
            Dim arImportedItem As String() = Split(arImportedLine(lineCount), vbTab)
            Dim sl As New ScheduledLoad

            Select Case locaName    'locationID
                Case "Richmond-PFG" '138B291D-BC13-4EDB-99DE-B7491DE908D0
                    If IsDate(arImportedItem(0)) Then
                        Dim dt As Date = FormatDateTime(arImportedItem(0), DateFormat.ShortDate)
                        ' time field inserts colon :
                        Dim tm As String = Left(arImportedItem(1), Len(arImportedItem(1)) - 2) & ":" & Right(arImportedItem(1), 2)
                        'create appointment time
                        Dim ats As String = Month(dt).ToString & "/" & Day(dt).ToString & "/" & Year(dt).ToString & " " & tm

                        sl.ApptDate = ats   'uses arImportedItem 0 and 1

                        sl.LoadNumber = arImportedItem(2).Trim()
                        sl.PONumber = arImportedItem(3).Trim()
                        sl.VendorNumber = arImportedItem(4).Trim()
                        ' remove any leading zeros from vendor number 
                        If Left(sl.VendorNumber, 1) = "0" Then
                            If IsNumeric(sl.VendorNumber) Then
                                Dim vn As Integer = CType(sl.VendorNumber, Integer)
                                sl.VendorNumber = vn.ToString
                            End If
                        End If
                        sl.VendorName = arImportedItem(5).Trim()
                        'strip commas from vendor name
                        If sl.VendorName.Length > 3 Then sl.VendorName = Replace(sl.VendorName, ",", "")
                        '                sl.Dry = arImportedItem(6)
                        '                sl.Ref = arImportedItem(7)
                        '                sl.Frz = arImportedItem(8)
                        sl.Total = arImportedItem(9)                'Pieces
                        '                sl.Notes = arImportedItem(10).Trim()
                        '                sl.Type = arImportedItem(11).Trim()
                        slList.Add(sl)
                        lineCount += 1
                    End If
                Case Else
                    ' Bessemer" 'BB07D9A1-EC65-412B-89AD-FA5CC892DC8E
                    ' Temple-PGF" '64699684-0537-4697-8e82-803cce66cea1
                    If IsDate(arImportedItem(0)) Then
                        Dim dtm As DateTime = arImportedItem(0)
                        Dim dtt As Date = arImportedItem(0)
                        Dim dt As Date = FormatDateTime(arImportedItem(0), DateFormat.ShortDate)

                        ' time field inserts colon :
                        Dim tm As String = FormatDateTime(arImportedItem(0), DateFormat.ShortTime).ToString
                        '                        Dim tm As String = Left(arImportedItem(1), Len(arImportedItem(1)) - 2) & ":" & Right(arImportedItem(1), 2)

                        'create appointment time
                        Dim ats As String = Month(dt).ToString & "/" & Day(dt).ToString & "/" & Year(dt).ToString & " " & tm
                        sl.ApptDate = ats
                        If arImportedItem(1).Trim().Length > 0 Then
                            sl.LoadNumber = arImportedItem(1).Trim()
                        Else    ' Temple-PGF" '64699684-0537-4697-8e82-803cce66cea1
                            sl.LoadNumber = arImportedItem(2).Trim() ' same as PONumber
                        End If
                        sl.PONumber = arImportedItem(2).Trim()
                        sl.VendorNumber = arImportedItem(3).Trim()
                        ' remove any leading zeros from vendor number 
                        If Left(sl.VendorNumber, 1) = "0" Then
                            If IsNumeric(sl.VendorNumber) Then
                                Dim vn As Integer = CType(sl.VendorNumber, Integer)
                                sl.VendorNumber = vn.ToString
                            End If
                        End If
                        sl.VendorName = arImportedItem(4).Trim()
                        'strip commas from vendor name
                        If sl.VendorName.Length > 3 Then sl.VendorName = Replace(sl.VendorName, ",", "")
                        '                sl.Dry = arImportedItem(6)
                        '                sl.Ref = arImportedItem(7)
                        '                sl.Frz = arImportedItem(8)
                        sl.Total = arImportedItem(5)                'Pieces
                        sl.DepartmentName = arImportedItem(6).Trim()                'Department
                        '                        If sl.DepartmentName = "GROCERY" Then
                        slList.Add(sl)
                        lineCount += 1
                        '                    End If
                    End If

            End Select
        Loop
        ' close the stream reader
        srImport.Close()
        Dim b As Integer = lineCount
        ' sort slList
        slList.Sort(Function(p1, p2) p1.LoadNumber.CompareTo(p2.LoadNumber))
        woList = doWOlist(slList, fileName)
        Return slList
    End Function

    Public Function doWOlist(ByVal slList As List(Of ScheduledLoad), ByVal fileName As String) As List(Of WorkOrder)
        Dim woList As New List(Of WorkOrder)
        Dim wo As New WorkOrder
        Dim counter As Integer = 0
        fileName = Left(fileName, InStrRev(fileName, "\") - 1)
        Dim dba As New DBAccess()
        Dim locaName As String = Right(fileName, fileName.Length - InStrRev(fileName, "\"))

        '        Dim dirname As String = 
        '        Dim locaName As String = Left(fileName, fileName.Length - 4)
        For Each sld As ScheduledLoad In slList

            If wo.LoadNumber <> sld.LoadNumber Then
                If counter > 0 Then
                    woList.Add(wo)
                End If
                counter = 1
                wo = New WorkOrder
                wo.Status = 9
                ' get date components
                wo.LogDate = FormatDateTime(sld.ApptDate, DateFormat.ShortDate)
                '   for testing, use this instead of the one above
                '   wo.LogDate = FormatDateTime(Date.Now(), DateFormat.ShortDate)
                wo.LogNumber = -1
                wo.LoadNumber = sld.LoadNumber

                Dim ldal As New locaDAL()
                Dim loca As New Location()
                loca = ldal.getLocationByName(locaName)
                wo.LocationID = loca.ID
                Dim dt As New DataTable()

                If sld.DepartmentName IsNot Nothing Then
                    dba.CommandText = "SELECT ID FROM Department WHERE Name=@departmentName"
                    dba.AddParameter("@departmentName", sld.DepartmentName.ToUpper)
                    dt = dba.ExecuteDataSet.Tables(0)
                    If dt.Rows.Count > 0 Then 'we have this department
                        wo.DepartmentID = dt.Rows(0).Item("ID")
                        sld.DepartmentID = wo.DepartmentID
                        wo.Department = sld.DepartmentName.ToUpper()
                    End If
                Else
                    wo.DepartmentID = New Guid("72FBC9B2-21BF-4CE0-BB21-5B4180985D75")  'Grocery
                End If

                wo.LoadTypeID = New Guid("00000000-0000-0000-0000-000000000000")
                dba.CommandText = "SELECT ID, Name FROM Vendor WHERE Number = @VendorNumber AND ParentCompanyID = @ParentCompanyID"
                dba.AddParameter("@VendorNumber", sld.VendorNumber)
                dba.AddParameter("@ParentCompanyID", loca.ParentCompanyID)
                dt = dba.ExecuteDataSet.Tables(0)
                If dt.Rows.Count > 0 Then
                    wo.CustomerID = dt.Rows(0).Item("ID")
                    wo.VendorName = dt.Rows(0).Item("Name")
                Else
                    Dim newCustomerID As Guid = Guid.NewGuid
                    dba.CommandText = "INSERT INTO Vendor (ParentCompanyID, Number, Name, ID) VALUES (@ParentCompanyID, @Number, @Name, @ID)"
                    dba.AddParameter("@ParentCompanyID", loca.ParentCompanyID)
                    dba.AddParameter("@Number", sld.VendorNumber)
                    dba.AddParameter("@Name", sld.VendorName.ToString.ToUpper())
                    dba.AddParameter("@ID", newCustomerID)
                    Try
                        dba.ExecuteNonQuery()
                    Catch ex As Exception

                    End Try
                    wo.CustomerID = newCustomerID
                    wo.VendorName = sld.VendorName

                End If
                wo.VendorNumber = sld.VendorNumber 'VendorNumber
                wo.ReceiptNumber = wo.LoadNumber
                wo.PurchaseOrder = sld.PONumber
                wo.Amount = 0
                wo.IsCash = False
                wo.LoadDescriptionID = New Guid("00000000-0000-0000-0000-000000000000")
                wo.CarrierID = New Guid("00000000-0000-0000-0000-000000000000")
                wo.TruckNumber = ""
                wo.TrailerNumber = ""
                wo.AppointmentTime = sld.ApptDate
                '   for testing, use this instead of the one above
                '   wo.AppointmentTime = Date.Now
                wo.GateTime = "1900-01-01 00:00:00.000"
                wo.DockTime = "1900-01-01 00:00:00.000"
                wo.StartTime = "1900-01-01 00:00:00.000"
                wo.CompTime = "1900-01-01 00:00:00.000"
                wo.TTLTime = -1
                wo.PalletsUnloaded = -1
                wo.DoorNumber = ""
                wo.Pieces = sld.Total         'Pieces
                wo.Weight = -1
                wo.Comments = ""
                wo.Restacks = -1
                wo.PalletsReceived = -1
                wo.BadPallets = -1
                wo.NumberOfItems = -1
                wo.CheckNumber = ""
                wo.BOL = ""
                wo.ID = Guid.NewGuid()
                wo.CreatedBy = "Imported: " & Date.Now().ToString("yyMMdd_HH:mm")
                wo.Employee = New List(Of String)

            Else
                If counter = 1 Then
                    wo.Comments &= wo.PurchaseOrder & " : " & sld.PONumber & " : "
                    wo.PurchaseOrder = wo.LoadNumber
                    counter = 2
                Else
                    wo.Comments &= sld.PONumber & " : "
                End If

                wo.Pieces += sld.Total         'Pieces
            End If

        Next

        woList.Add(wo)

        Dim wodal As New WorkOrderDAL
        For Each wo In woList
            wodal.AddWorkOrder(wo, True)
        Next
        Return woList

    End Function



    Private Sub ScheduledLoads_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender

    End Sub

    Private Sub cbLocations_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        Dim loadlist As New List(Of locaFolder)
        myLocaLoadList = getScheduledLoadList()
        Session("loadlist") = myLocaLoadList
        RenderTable(myLocaLoadList)
    End Sub
End Class
#Region "Objects"

Public Class ScheduledLoad
    '    Implements IComparer(Of ScheduledLoad)
    Private _ApptDate As Date
    Private _LoadNumber As String
    Private _PONumber As String
    Private _VendorNumber As String
    Private _VendorName As String
    Private _Dry As Integer
    Private _Ref As Integer
    Private _Frz As Integer
    '    Private _Other As String
    Private _Total As String
    Private _Notes As String
    Private _Type As String
    Private _DepartmentID As Guid
    Private _DepartmentName As String


    'Public Function Compare(ByVal x As ScheduledLoad, ByVal y As ScheduledLoad) As Integer _
    '    Implements System.Collections.Generic.IComparer(Of ScheduledLoad).Compare
    '    Return String.Compare(x.LoadNumber, y.LoadNumber)
    'End Function

    Public Property ApptDate() As Date
        Get
            Return _ApptDate
        End Get
        Set(ByVal value As Date)
            _ApptDate = value
        End Set
    End Property

    Public Property LoadNumber() As String
        Get
            Return _LoadNumber
        End Get
        Set(ByVal value As String)
            _LoadNumber = value
        End Set
    End Property

    Public Property PONumber() As String
        Get
            Return _PONumber
        End Get
        Set(ByVal value As String)
            _PONumber = value
        End Set
    End Property

    Public Property VendorNumber() As String
        Get
            Return _VendorNumber
        End Get
        Set(ByVal value As String)
            _VendorNumber = value
        End Set
    End Property

    Public Property VendorName() As String
        Get
            Return _VendorName
        End Get
        Set(ByVal value As String)
            _VendorName = value
        End Set
    End Property

    Public Property Dry() As Integer
        Get
            Return _Dry
        End Get
        Set(ByVal value As Integer)
            _Dry = value
        End Set
    End Property

    Public Property Ref() As Integer
        Get
            Return _Ref
        End Get
        Set(ByVal value As Integer)
            _Ref = value
        End Set
    End Property

    Public Property Frz() As Integer
        Get
            Return _Frz
        End Get
        Set(ByVal value As Integer)
            _Frz = value
        End Set
    End Property

    'Public Property Other() As String
    '    Get
    '        Return _Other
    '    End Get
    '    Set(ByVal value As String)
    '        _Other = value
    '    End Set
    'End Property

    Public Property Total() As Integer
        Get
            Return _Total
        End Get
        Set(ByVal value As Integer)
            _Total = value
        End Set
    End Property

    Public Property Notes() As String
        Get
            Return _Notes
        End Get
        Set(ByVal value As String)
            _Notes = value
        End Set
    End Property

    Public Property Type() As String
        Get
            Return _Type
        End Get
        Set(ByVal value As String)
            _Type = value
        End Set
    End Property

    Public Property DepartmentID() As Guid
        Get
            Return _DepartmentID
        End Get
        Set(ByVal value As Guid)
            _DepartmentID = value
        End Set
    End Property

    Public Property DepartmentName() As String
        Get
            Return _DepartmentName
        End Get
        Set(ByVal value As String)
            _DepartmentName = value
        End Set
    End Property


End Class

<Serializable()>
Public Class locaFolder
    Private _ID As Integer
    Private _locaName As String
    Private _locaFiles As List(Of locaFile)
    Private _locaErrMsg As String

    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property

    Public Property locaName() As String
        Get
            Return _locaName
        End Get
        Set(ByVal value As String)
            _locaName = value
        End Set
    End Property

    Public Property locaFiles() As List(Of locaFile)
        Get
            Return _locaFiles
        End Get
        Set(ByVal value As List(Of locaFile))
            _locaFiles = value
        End Set
    End Property

    Public Property locaErrMsg() As String
        Get
            Return _locaErrMsg
        End Get
        Set(ByVal value As String)
            _locaErrMsg = value
        End Set
    End Property
End Class

<Serializable()>
Public Class locaFile
    Private _fName As String
    Private _fPath As String
    Private _fDate As Date
    Private _fLoadDate As Date
    Private _fileErrMsg As String
    Private _isDone As Boolean


    Public Property fName() As String
        Get
            Return _fName
        End Get
        Set(ByVal value As String)
            _fName = value
        End Set
    End Property

    Public Property fPath() As String
        Get
            Return _fPath
        End Get
        Set(ByVal value As String)
            _fPath = value
        End Set
    End Property
    Public Property fDate() As Date
        Get
            Return _fDate
        End Get
        Set(ByVal value As Date)
            _fDate = value
        End Set
    End Property
    Public Property fLoadDate() As Date
        Get
            Return _fLoadDate
        End Get
        Set(ByVal value As Date)
            _fLoadDate = value
        End Set
    End Property
    Public Property fileErrMsg() As String
        Get
            Return _fileErrMsg
        End Get
        Set(ByVal value As String)
            _fileErrMsg = value
        End Set
    End Property

    Public Property isDone() As Boolean
        Get
            Return _isDone
        End Get
        Set(ByVal value As Boolean)
            _isDone = value
        End Set
    End Property


End Class

#End Region
