Imports WebSupergoo.ABCpdf10
'Imports System.Net.Mail
Imports System.Data
Imports System.Data.SqlClient
'Imports System.IO


Public Class seuDuplicateReceipts
    Inherits System.Web.UI.Page

    Protected conn As New SqlConnection()
    Protected cmd As New SqlCommand()
    Protected inWin As Boolean = False
    Dim isdupe As Boolean = True
    Dim lstrdupe As String = "duplicate"
    Dim ustrDupe As String = "Duplicate"
    Dim uustrDUPE As String = "DUPLICATE &nbsp; "
    Dim lstrreceipt As String = "receipt"
    Dim ustrReceipt As String = "Receipt"
    Dim uustrRECEIPT As String = "RECEIPT"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim st As String = Request.QueryString("woid")
        Dim ref As String = Request("HTTP_REFERER")
        If Right(ref, 24) = "LoadEditorDataEntry.aspx" Then
            lstrdupe = ""
            ustrdupe = ""
            uustrdupe = ""
            isdupe = False
        End If
        If Not st Is Nothing And Not st = String.Empty Then
            Dim woidString As String() = Split(Request.QueryString("woid"), ":")
            Dim woid As String = String.Empty
            Dim dupList As New List(Of DuplicateReceipt)
            For i As Integer = 0 To woidString.Length - 1
                '' this strips the appended symbol (comment switch)
                If isdupe Then
                    woid = Right(woidString(i), Len(woidString(i)) - 1)
                Else
                    woid = woidString(0)
                End If
                Dim gwoid As Guid = New Guid(woid)
                '' this picks up the appended symbol telling me if the po was primary(=) or found in comments (+)
                Dim vShowComments As Boolean = Left(woidString(i), 1) = ">"
                Dim dup As DuplicateReceipt = New DuplicateReceipt
                Dim dba As New DBAccess()
                dba.CommandText = "SELECT WorkOrder.ID AS woID, CONVERT(varchar(10), WorkOrder.LogDate, 110) AS LogDate, WorkOrder.DoorNumber, Location.Name AS Location, Department.Name AS Department,  " & _
                    "WorkOrder.ReceiptNumber, Carrier.Name AS Carrier, WorkOrder.TruckNumber, WorkOrder.TrailerNumber, WorkOrder.BadPallets, WorkOrder.CompTime,  " & _
                    "WorkOrder.Amount, WorkOrder.CheckNumber,WorkOrder.PaymentType, WorkOrder.PurchaseOrder, WorkOrder.Restacks, Description.Name AS LoadDescription, WorkOrder.Comments,  " & _
                    "LoadType.Name AS LoadType " & _
                    "FROM WorkOrder INNER JOIN " & _
                    "Location ON WorkOrder.LocationID = Location.ID INNER JOIN " & _
                    "Department ON WorkOrder.DepartmentID = Department.ID INNER JOIN " & _
                    "Carrier ON WorkOrder.CarrierID = Carrier.ID INNER JOIN " & _
                    "Description ON WorkOrder.LoadDescriptionID = Description.ID INNER JOIN " & _
                    "LoadType ON WorkOrder.LoadTypeID = LoadType.ID " & _
                    "WHERE WorkOrder.ID = @woid"
                dba.AddParameter("@woid", gwoid)
                Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
                If dt.Rows.Count > 0 Then
                    Dim row As DataRow = dt.Rows(0)
                    If Not IsDBNull(row.Item("woID")) Then dup.woID = row.Item("woID")
                    If Not IsDBNull(row.Item("LogDate")) Then dup.LogDate = row.Item("LogDate")
                    If Not IsDBNull(row.Item("Location")) Then dup.Location = row.Item("Location")
                    If Not IsDBNull(row.Item("Department")) Then dup.Department = row.Item("Department")
                    If Not IsDBNull(row.Item("ReceiptNumber")) Then dup.ReceiptNumber = row.Item("ReceiptNumber")
                    If Not IsDBNull(row.Item("DoorNumber")) Then dup.DoorNumber = row.Item("DoorNumber")
                    If Not IsDBNull(row.Item("LoadDescription")) Then dup.LoadDescription = row.Item("LoadDescription")
                    If Not IsDBNull(row.Item("PurchaseOrder")) Then dup.PurchaseOrder = row.Item("PurchaseOrder")
                    If Not IsDBNull(row.Item("Carrier")) Then dup.Carrier = row.Item("Carrier")
                    If Not IsDBNull(row.Item("TruckNumber")) Then dup.TruckNumber = row.Item("TruckNumber")
                    If Not IsDBNull(row.Item("TrailerNumber")) Then dup.TrailerNumber = row.Item("TrailerNumber")
                    If Not IsDBNull(row.Item("BadPallets")) Then dup.BadPallets = row.Item("BadPallets")
                    If Not IsDBNull(row.Item("Restacks")) Then dup.Restacks = row.Item("Restacks")
                    If Not IsDBNull(row.Item("CompTime")) Then dup.CompTime = row.Item("CompTime")
                    If Not IsDBNull(row.Item("Amount")) Then dup.Amount = row.Item("Amount")
                    If Not IsDBNull(row.Item("Comments")) Then dup.Comments = row.Item("Comments")
                    If Not IsDBNull(row.Item("LoadType")) Then dup.LoadType = row.Item("LoadType")
                    If Not IsDBNull(row.Item("CheckNumber")) Then dup.CheckNumber = row.Item("CheckNumber")
                    If Not IsDBNull(row.Item("PaymentType")) Then dup.PaymentType = row.Item("PaymentType")
                    If vShowComments Then dup.Comments = "SHOWME" & dup.Comments
                End If

                dupList.Add(dup)
                'ElseIf woidString.Length > 0 Then
                '    Dim dupList As New List(Of DuplicateReceipt)
                '    Dim dup As DuplicateReceipt = New DuplicateReceipt
                '    dupList.Add(dup)
                '    doPDF(dupList, True)
            Next
            doPDF(dupList)
        Else
            Response.Write("<br><br><br><br><br><center>This page is not directly accessable<br />Selected Purchase Orders que is empty</center>")
            Response.End()
        End If

    End Sub

    Public docFile As String = Nothing
    Public docSubject As String = Nothing

    Sub doPDF(ByVal dups As List(Of DuplicateReceipt), Optional ByVal isFav As Boolean = False)
        '************************************************************************************

        Dim dup As DuplicateReceipt = dups.Item(0)
        If Not isFav Then
            'Dim compDAL As New Companies()
            'Dim contract As Contract = compDAL.getContract(compID)
            docFile = ustrdupe & "Receipt.pdf" 'dups.Item(0).woID.ToString & ".pdf"
            docSubject = "Receipt Number: " & dups.Item(0).ReceiptNumber
            'Else
            '    'Dim compDAL As New Companies()
            '    'Dim contract As Contract = compDAL.getContract(compID)
            '    docFile = ustrdupe & "Receipt.pdf" 'dups.Item(0).woID.ToString & ".pdf"
            '    docSubject = "Receipt Number: " & dups.Item(0).ReceiptNumber
        End If



        '************************************************************************************
        '************************************ Create Document ****************************************
        '************************************************************************************

        Dim theDoc As New Doc()
        theDoc.Color.String = "0 0 0"
        '		theDoc.Color.Alpha = 150
        Dim theID As Integer
        Dim SansFont As Integer = theDoc.AddFont("Arial")
        Dim SerrifFont As Integer = theDoc.AddFont("Times New Roman")
        '' ********** embed some fonts **********
        Dim Font1, Font2, Font3, Font4 As String   ' ********** fonts
        Font1 = theDoc.EmbedFont("Verdana")
        Font2 = theDoc.EmbedFont("Verdana Bold")
        Font3 = theDoc.EmbedFont("Arial")
        Font4 = theDoc.EmbedFont("Arial Bold")
        '' ********** load images used in document **********
        Dim logo As XImage = New XImage()
        logo.SetFile(Server.MapPath("../images/SoutheastUnloading.jpg"))
        Dim theWaterMark As XImage = New XImage()
        theWaterMark.SetFile(Server.MapPath("../images/SouthEastUnloadingWaterMark.png"))
        Dim pgNumber As Integer = 0
        For Each dup In dups
            If dup.LoadType = "Invoice" Then
                lstrreceipt = "invoice"
                ustrReceipt = "Invoice"
                uustrRECEIPT = "INVOICE"
            Else
                lstrreceipt = "receipt"
                ustrReceipt = "Receipt"
                uustrRECEIPT = "RECEIPT"

            End If
            If pgNumber > 0 Then theDoc.AddPage(pgNumber + 1)
            theDoc.PageNumber = pgNumber + 1
            theDoc.Color.String = "0 0 0"
            'left side of header, left of logo

            Dim lblHeaderText As String = "<font pid=" & Font3 & " > " & uustrDUPE & " " & uustrRECEIPT & "</font>"
            theDoc.TextStyle.HPos = 0
            theDoc.TextStyle.VPos = 1
            theDoc.Rect.Position(38, 748)
            theDoc.Rect.Right = 250
            theDoc.Rect.Top = 765
            '''''' FontSize
            theDoc.FontSize = 17
            theID = theDoc.AddHtml(lblHeaderText)

            If dup.LoadType = "Invoice" Then
                Dim lblNotABill As String = "<font pid=" & Font3 & ">This is NOT a Bill, do not Pay from this document</font>"
                theDoc.Rect.Position(38, 748)
                theDoc.Rect.Right = 560
                theDoc.Rect.Top = 765
                '''''' HPos
                theDoc.TextStyle.HPos = 0.5
                '''''' FontSize
                theDoc.FontSize = 9
                theDoc.AddHtml(lblNotABill)
                theDoc.TextStyle.HPos = 0

            End If


            Dim CompanyName As String = "<font pid=" & Font4 & " >Southeast Unloading</font>"
            theDoc.Rect.Position(38, 728)
            theDoc.Rect.Right = 200
            theDoc.Rect.Top = 750
            '''''' FontSize
            theDoc.FontSize = 13
            theID = theDoc.AddHtml(CompanyName)

            Dim CompanyAddress As String = "<font pid=" & Font3 & ">1864 South 14<sup>th</sup> Street<br />Fernandina Beach, Florida &nbsp;32043"
            theDoc.Rect.Position(38, 710)
            theDoc.Rect.Right = 200
            theDoc.Rect.Top = 728
            '''''' FontSize
            theDoc.FontSize = 9
            theID = theDoc.AddHtml(CompanyAddress)

            ' add logo at top of page, kinda centered
            theDoc.Rect.Position(219, 702)
            theDoc.Rect.Right = 396
            theDoc.Rect.Top = 739
            theDoc.AddImageObject(logo, False)

            'phone, taxid, date to right of logo
            Dim lblPhone As String = "<font pid=" & Font3 & ">Phone: (904) 491-6800</font>"
            Dim lblTaxID As String = "<font pid=" & Font3 & ">Tax ID: 593746670</font>"
            Dim lblRdate As String = "<font pid=" & Font3 & ">* " & Date.Now.ToShortDateString & "</font"
            Dim lblDupNote As String = "<font pid=" & Font3 & ">* Date this " & lstrdupe & " " & lstrreceipt & "  was issued</font>"
            theDoc.TextStyle.HPos = 1
            theDoc.Rect.Position(460, 740)
            theDoc.Rect.Right = 560
            theID = theDoc.AddHtml(lblPhone)

            theDoc.Rect.Position(460, 725)
            theDoc.Rect.Right = 560
            theID = theDoc.AddHtml(lblTaxID)
            If isdupe Then
                theDoc.Rect.Position(460, 710)
                theDoc.Rect.Right = 560
                theID = theDoc.AddHtml(lblRdate)
            End If

            theDoc.Rect.Position(400, 693)
            theDoc.Rect.Right = 560
            '''''' FontSize
            If isdupe Then
                theDoc.FontSize = 5
                theDoc.AddHtml(lblDupNote)
            End If
            '''''' FontSize

            ' separator line --- the 'date this duplicatte reciept was issued line is just above this line
            theDoc.AddLine(30, 690, 570, 690)



            Dim lblDate As String = "<font pid=" & Font4 & ">Date:</font>"
            Dim lblDoorNumber As String = "<font pid=" & Font4 & ">Door #:</font>"
            Dim lblLocation As String = "<font pid=" & Font4 & ">Location:</font>"
            Dim lblDepartment As String = "<font pid=" & Font4 & ">Department:</font>"
            Dim lblRestacks As String = "<font pid=" & Font4 & ">Restacks:</font>"
            Dim varDoorNumber As String = "<font pid=" & Font3 & ">" & dup.DoorNumber & "</font>"
            Dim varDate As String = "<font pid=" & Font3 & ">" & FormatDateTime(dup.LogDate, DateFormat.ShortDate) & "</font>"
            Dim varLocation As String = "<font pid=" & Font3 & ">" & dup.Location & "</font>"
            Dim varDepartment As String = "<font pid=" & Font3 & ">" & dup.Department & "</font>"
            Dim varRestacks As String = "<font pid=" & Font3 & ">" & dup.Restacks & "</font>"
            Dim varLoadDesc As String = "<font pid=" & Font3 & ">" & dup.LoadDescription & "</font>"
            Dim lblLoadDesc As String = "<font pid=" & Font4 & ">Load Description:</font>"
            Dim lblCarrier As String = "<font pid=" & Font4 & ">Carrier:</font>"
            Dim lblTruck As String = "<font pid=" & Font4 & ">Truck #:</font>"
            Dim lblTrailer As String = "<font pid=" & Font4 & ">Trailer #:</font>"
            Dim lblBadPallets As String = "<font pid=" & Font4 & ">Bad Pallets:</font>"
            Dim varCarrier As String = "<font pid=" & Font3 & ">" & dup.Carrier & "</font>"
            Dim varTruck As String = "<font pid=" & Font3 & ">" & dup.TruckNumber & "</font>"
            Dim varTrailer As String = "<font pid=" & Font3 & ">" & dup.TrailerNumber & "</font>"
            Dim varBadPallets As String = "<font pid=" & Font3 & ">" & dup.BadPallets & "</font>"

            Dim lblEndTime As String = "<font pid=" & Font4 & ">End Time:</font>"

            If dup.PaymentType = "Check" Then
                Dim lblCheckNumber As String = "<font pid=" & Font4 & ">Check #:</font>"
            ElseIf dup.PaymentType = "Card" Then
                Dim lblCheckNumber As String = "<font pid=" & Font4 & ">Trans #:</font>"
            End If

            Dim lblAmount As String = "<font pid=" & Font4 & ">Amount:</font>"

            Dim lblPOnum As String = "<font pid=" & Font4 & ">Purchase Order:</font>"
            Dim lblReceipt As String = "<font pid=" & Font4 & ">" & ustrReceipt & ":</font>"

            Dim varCompTime As String = "<font pid=" & Font3 & ">" & FormatDateTime(dup.CompTime, DateFormat.ShortTime) & "</font>"
            Dim varCheckNumber As String = "<font pid=" & Font3 & ">" & dup.CheckNumber & "</font>"
            Dim varAmount As String = "<font pid=" & Font3 & ">" & FormatCurrency(dup.Amount, 2) & "</font>"
            Dim varPurchaseOrdr As String = "<font pid=" & Font3 & ">" & dup.PurchaseOrder & "</font>"
            If dup.ReceiptNumber = "-1" Then dup.ReceiptNumber = "n/a"
            Dim varReceipt As String = "<font pid=" & Font3 & ">" & dup.ReceiptNumber & "</font>"
            theDoc.TextStyle.HPos = 0

            '''''' FontSize
            'column 1 labels
            theDoc.FontSize = 12
            theDoc.Rect.Position(30, 660)
            theDoc.Rect.Left = 30
            theID = theDoc.AddHtml(lblDate)
            theDoc.Rect.Position(30, 632)
            theDoc.Rect.Left = 30
            theID = theDoc.AddHtml(lblLocation)
            theDoc.Rect.Position(25, 604)
            theDoc.Rect.Left = 30
            theID = theDoc.AddHtml(lblDepartment)

            theDoc.Rect.Position(30, 576)
            theDoc.Rect.Left = 30
            theID = theDoc.AddHtml(lblReceipt)
            theDoc.Rect.Position(30, 548)
            theDoc.Rect.Left = 30
            theID = theDoc.AddHtml(lblDoorNumber)


            theDoc.Rect.Position(35, 515)
            theDoc.Rect.Left = 30
            theID = theDoc.AddHtml(lblLoadDesc)


            'already set in load desc above            theDoc.TextStyle.HPos = 0
            '''''' FontSize
            'column 1 data
            theDoc.FontSize = 11
            theDoc.Rect.Position(110, 660)
            theDoc.AddHtml(varDate)
            theDoc.Rect.Position(110, 632)
            theDoc.AddHtml(varLocation)
            theDoc.Rect.Position(110, 604)
            theDoc.AddHtml(varDepartment)
            theDoc.Rect.Position(110, 576)
            theID = theDoc.AddHtml(varReceipt)
            theDoc.Rect.Position(110, 548)
            theID = theDoc.AddHtml(varDoorNumber)

            theDoc.Rect.Position(110, 576)
            theDoc.AddHtml(varRestacks)
            theDoc.Rect.Position(140, 515)
            theDoc.AddHtml(varLoadDesc)


            theDoc.TextStyle.HPos = 0
            '''''' FontSize
            'column 2 labels
            theDoc.FontSize = 12
            theDoc.Rect.Position(240, 660)
            theID = theDoc.AddHtml(lblCarrier)
            theDoc.Rect.Position(240, 632)
            theID = theDoc.AddHtml(lblTruck)
            theDoc.Rect.Position(240, 604)
            theID = theDoc.AddHtml(lblTrailer)
            theDoc.Rect.Position(240, 576)
            theID = theDoc.AddHtml(lblBadPallets)
            theDoc.Rect.Position(240, 548)
            theID = theDoc.AddHtml(lblRestacks)


            '''''' FontSize
            theDoc.FontSize = 11
            theDoc.TextStyle.HPos = 0
            theDoc.Rect.Position(320, 660)
            theDoc.AddHtml(varCarrier)
            theDoc.Rect.Position(320, 632)
            theDoc.AddHtml(varTruck)
            theDoc.Rect.Position(320, 604)
            theDoc.AddHtml(varTrailer)
            theDoc.Rect.Position(320, 576)
            theDoc.AddHtml(varBadPallets)
            theDoc.Rect.Position(320, 548)
            theDoc.AddHtml(varRestacks)


            '''''' FontSize
            'column 3 labels
            theDoc.FontSize = 12
            Dim ldal As New locaDAL
            If ldal.PrintReceipt(dup.woID) Then
                theDoc.Rect.Position(410, 632)
                theID = theDoc.AddHtml(lblEndTime)
                theDoc.Rect.Position(510, 632)
                theDoc.AddHtml(varCompTime)
            End If
            theDoc.Rect.Position(410, 604)
            theID = theDoc.AddHtml(lblAmount)

            theDoc.Rect.Position(410, 576)
            theID = theDoc.AddHtml(lblPOnum)


            theDoc.TextStyle.HPos = 0
            '''''' FontSize
            'column 3 data
            theDoc.FontSize = 11
            theDoc.Rect.Position(510, 604)
            theDoc.AddHtml(varAmount)
            theDoc.Rect.Position(510, 576)
            theDoc.AddHtml(varPurchaseOrdr)


            '            Dim lblSEUrep As String = "<font pid=" & Font3 & ">SEU Representative:</font>"
            Dim lblVerificationKey As String = "<font pid=" & Font3 & ">validation code: " & dup.woID.ToString & "</font>"
            theDoc.TextStyle.HPos = 0
            '''''' FontSize
            theDoc.FontSize = 6
            '            theDoc.Rect.Position(300, 535)
            '            theDoc.AddHtml(lblSEUrep)
            theDoc.Rect.Position(400, 475)
            theDoc.AddHtml(lblVerificationKey)
            theDoc.AddLine(400, 474, 553, 474)
            If Not dup.Comments Is Nothing Then

                If dup.Comments.Contains("SHOWME") Then
                    Dim lblComments As String = "<font pid=" & Font4 & ">** More info: (including other purchase orders that may be covered by this " & lstrreceipt & ")</font>"
                    theDoc.TextStyle.HPos = 0

                    theDoc.FontSize = 9
                    Dim lblasterisk As String = "<font pid=" & Font4 & ">**</font>"
                    theDoc.Rect.Position(377, 602)

                    theDoc.AddHtml(lblasterisk)

                    theDoc.FontSize = 8
                    theDoc.Rect.Position(35, 520)
                    theDoc.Rect.Right = 550
                    theDoc.AddHtml(lblComments)
                    Dim varComments As String = "<font pid=" & Font3 & ">" & Right(dup.Comments, Len(dup.Comments) - 6) & "</font>"
                    theDoc.Rect.Position(55, 508)
                    theDoc.Rect.Right = 550
                    theDoc.AddHtml(varComments)
                End If
                theDoc.AddLine(30, 495, 570, 495)
            Else
                theDoc.AddLine(30, 528, 570, 528)
            End If

            Dim lblRemainderBlank As String = "<font pid=" & Font4 & ">this area intentionally blank</font>"
            theDoc.Rect.Position(40, 415)
            theDoc.Rect.Right = 560
            theDoc.Rect.Height = 485
            '''''' HPos
            theDoc.TextStyle.HPos = 0.5
            '''''' FontSize
            theDoc.FontSize = 24
            theDoc.Color.String = "210 210 210"
            theDoc.AddHtml(lblRemainderBlank)
            theDoc.Color.String = "0 0 0"
            '            theDoc.AddGrid()
            pgNumber += 1

        Next
        '        theDoc.AddGrid()


        '************************************************************************************
        '************************************ Page 1 ****************************************
        '************************************************************************************





        '************************************************************************************
        '************************ Count Pages, Add to Fax Cover Page ************************
        '************************************************************************************
        Dim theCount As Integer = 0
        theCount = theDoc.PageCount
        theDoc.Page = 1

        ' ***************** S A M P L E
        Dim i As Integer

        'theDoc.TextStyle.HPos = 0.5
        'theDoc.TextStyle.VPos = 0.5
        'theDoc.Color.String = "255 0 0"
        'theDoc.Color.Alpha = 45
        'theDoc.FontSize = 125
        'theDoc.Font = SerrifFont
        For i = 1 To theCount

            theDoc.PageNumber = i
            '            theDoc.Layer = theDoc.LayerCount + 1
            theDoc.Rect.String = "53 170 557 314"
            If i = 1 Then
                theID = theDoc.AddImageObject(theWaterMark, True)
            Else
                theDoc.AddImageCopy(theID)
            End If

            Dim varVersion As String = "<font pid=" & Font1 & ">SEU " & ustrDupe & " " & ustrReceipt & " Generator v2.0</font>"
            Dim varValidationTerms As String = "<font pid=" & Font1 & ">" & ustrDupe & " " & ustrReceipt & " valid only if validation code above matches our records</font>"
            theDoc.Color.String = "0 0 0"
            theDoc.TextStyle.HPos = 0
            theDoc.Rect.Position(25, 15)
            theDoc.FontSize = 6
            theDoc.AddHtml(varVersion)
            theDoc.TextStyle.HPos = 1
            theDoc.Rect.Position(250, 15)
            theDoc.Rect.Right = 580
            theDoc.FontSize = 6
            theDoc.AddHtml(varValidationTerms)

        Next

        For i = 1 To theCount
            theDoc.Page = i
            '        theDoc.Color.Alpha = 255
            '        theDoc.Rect.String = "-200 5 800 790"
            '        theDoc.Transform.Reset()
            '        theDoc.Transform.Rotate(54, 302, 396)
            '        theDoc.AddText("File Copy")
            '        theDoc.Transform.Reset()
        Next



        '        Dim filename = "cdrApplication_" & DatePart(DateInterval.Second, Date.Now) & ".pdf"
        '        theDoc.Save(Server.MapPath("../pdf/" & filename))
        '        theDoc.Clear()
        '        Dim thufile As String = "../pdf/" & filename
        '        Dim guid As New Guid

        If theDoc.GetInfo(-1, "/Info") = "" Then
            theDoc.SetInfo(-1, "/Info:Ref", theDoc.AddObject("<< >>").ToString())
        End If
        theDoc.SetInfo(-1, "/Info*/Title:Text", "Southeast Unloading" & ustrDupe & " " & ustrReceipt)
        theDoc.SetInfo(-1, "/Info*/Author:Text", "Diversified Logistics - Southeast Unloading")
        theDoc.SetInfo(-1, "/Info*/Subject:Text", "NOTICE: " & ustrDupe & " Receipt(s)/Ivoice(s) valid only if each validation code matches our records")
        theDoc.SetInfo(theDoc.Root, "/Metadata:Del", "")


        'If Request("eml").Length > 1 And Request("eml") <> "eMail Address (optional)" Then
        '    If Utilities.isValidEmail(Request("eml")) Then

        '        Dim pdf As Stream = New MemoryStream()
        '        theDoc.Save(pdf)
        '        pdf.Position = 0
        '        Dim msg As MailMessage = New MailMessage()
        '        msg.To.Add(Request("eml"))
        '        msg.From = New MailAddress("Diversified Logistics <no-reply@Div-Log.com>")
        '        msg.Subject = "Southeast Unloading Duplicate Receipt(s)"
        '        msg.Body = "Your requested receipt(s) are attached as a pdf document"
        '        Dim dt As Date = Date.Now
        '        Dim tm As String = dt.Year.ToString & dt.Month.ToString & dt.Day.ToString & dt.Second.ToString
        '        Dim fn As String = "seuDuplicateReceipt" & tm & ".pdf"
        '        msg.Attachments.Add(New Attachment(pdf, fn, "application/pdf"))
        '        Dim smtp As SmtpClient = New SmtpClient("mail.randydev.com")
        '        smtp.Credentials = New System.Net.NetworkCredential("system@realwebs.com", "systempw")
        '        smtp.Send(msg)
        '    Else
        '        Dim errString As String = "Invalid eMail Address"
        '        Me.Page.ClientScript.RegisterStartupScript(Me.GetType(), "ex", "alert('" + errString + "');", True)
        '        Exit Sub
        '    End If
        'End If

        Dim thuData() As Byte = theDoc.GetData()

        '        Response.Clear()
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "inline; filename=SEU_Div-Log.PDF")
        Response.AddHeader("content-length", thuData.Length.ToString())
        Response.BinaryWrite(thuData)
        Response.End()
    End Sub

End Class