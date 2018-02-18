Imports WebSupergoo.ABCpdf10
Imports System.Net.Mail
Imports System
'Imports System.Collections
Imports System.Collections.Generic
'Imports System.Configuration
Imports System.Data
'Imports System.Data.SqlClient
'Imports System.Drawing
'Imports System.Drawing.Drawing2D
'Imports System.Drawing.Imaging
Imports System.IO

Public Class seuFreightIssues
    Inherits System.Web.UI.Page

    Dim page1L, page1R, page2L, page2R As Array
    Dim cl0, cl1, cl2, cl3, cpage, ccol, pgNumber As Integer
    Dim theID As Integer = 0
    ' ********** define some objects **********
    Dim theDoc As New Doc()
    Dim logo As XImage = New XImage()
    Dim theWaterMark As XImage = New XImage()
    Public docFile As String = Nothing
    Public docSubject As String = Nothing
    '' ********** embed some fonts **********
    Dim SansFont As Integer = theDoc.AddFont("Arial")
    Dim SerrifFont As Integer = theDoc.AddFont("Times New Roman")
    Dim Font1, Font2, Font3, Font4 As String   ' ********** fonts
    Dim varPurchaseOrder As String
    Dim lblPOnum As String
    Dim locaID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim st As String = Request.QueryString("woid")
        Dim email As String = Request("email")

        If Not st Is Nothing Then
            If Right(st, 1) = ":" Then st = Left(st, st.Length - 1)
            '            Dim woidString As String() = Split(Request.QueryString("woid"), ":")
            Dim woidString As String() = Split(st, ":")
            Dim wodal As New WorkOrderDAL
            locaID = wodal.getLocationIDbyWorkOrderID(woidString(0))
            Dim FreightIssueList As New List(Of FreightIssue)
            For i As Integer = 0 To woidString.Length - 1
                '' this strips the appended symbol (comment switch)
                Dim woid As String = woidString(i)
                '' this picks up the appended symbol telling me if the po was primary(=) or found in comments (+)
                Dim vShowComments As Boolean = False
                Dim dup As DuplicateReceipt = New DuplicateReceipt
                Dim FreightIssue As New FreightIssue
                Dim dba As New DBAccess()
                dba.CommandText = "SELECT WorkOrder.ID AS woID, CONVERT(varchar(10), WorkOrder.LogDate, 110) AS LogDate, WorkOrder.LoadNumber, Location.Name AS Location,Location.ID as locaid,   " & _
                    "Department.Name AS Department, Carrier.Name AS Carrier, WorkOrder.TrailerNumber, WorkOrder.BadPallets, WorkOrder.PurchaseOrder, WorkOrder.Restacks,  " & _
                    "WorkOrder.Comments, Vendor.Name AS VendorName, Vendor.Number AS VendorNumber, LocationDepartment.email, LocationDepartment.emailCC  " & _
                    "FROM WorkOrder INNER JOIN " & _
                    "Location ON WorkOrder.LocationID = Location.ID INNER JOIN " & _
                    "Department ON WorkOrder.DepartmentID = Department.ID INNER JOIN " & _
                    "Carrier ON WorkOrder.CarrierID = Carrier.ID INNER JOIN " & _
                    "Vendor ON WorkOrder.CustomerID = Vendor.ID INNER JOIN " & _
                    "LocationDepartment ON Department.ID = LocationDepartment.DepartmentID AND Location.ID = LocationDepartment.LocationID " & _
                    "WHERE WorkOrder.ID = @woid"
                dba.AddParameter("@woid", woid)
                Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
                If dt.Rows.Count > 0 Then
                    Dim row As DataRow = dt.Rows(0)
                    FreightIssue.woid = New Guid(woid)
                    If Not IsDBNull(row.Item("LogDate")) Then FreightIssue.LogDate = row.Item("LogDate")
                    If Not IsDBNull(row.Item("LoadNumber")) Then FreightIssue.LoadNumber = row.Item("LoadNumber")
                    If Not IsDBNull(row.Item("Location")) Then FreightIssue.Location = row.Item("Location")
                    If Not IsDBNull(row.Item("locaid")) Then FreightIssue.locaid = row.Item("locaid")
                    If Not IsDBNull(row.Item("Department")) Then FreightIssue.Department = row.Item("Department")
                    If Not IsDBNull(row.Item("email")) Then FreightIssue.email = row.Item("email")
                    If Not IsDBNull(row.Item("emailCC")) Then FreightIssue.emailCC = row.Item("emailCC")

                    If Not IsDBNull(row.Item("PurchaseOrder")) Then FreightIssue.PurchaseOrder = row.Item("PurchaseOrder")
                    If Not IsDBNull(row.Item("Carrier")) Then FreightIssue.Carrier = row.Item("Carrier")
                    If Not IsDBNull(row.Item("TrailerNumber")) Then FreightIssue.TrailerNumber = row.Item("TrailerNumber")
                    If Not IsDBNull(row.Item("VendorName")) Then FreightIssue.VendorName = row.Item("VendorName")
                    If Not IsDBNull(row.Item("VendorNumber")) Then FreightIssue.VendorNumber = row.Item("VendorNumber")
                    If Not IsDBNull(row.Item("BadPallets")) Then FreightIssue.BadPallets = row.Item("BadPallets")
                    If Not IsDBNull(row.Item("Restacks")) Then FreightIssue.Restacks = row.Item("Restacks")
                    If Not IsDBNull(row.Item("Comments")) Then FreightIssue.Comments = row.Item("Comments")
                    '                    vShowComments = FreightIssue.LoadNumber > ""
                    vShowComments = True
                    FreightIssue.Comments = "Load/CMV #:" & FreightIssue.LoadNumber & "<br />"
                    If vShowComments Then FreightIssue.Comments = "SHOWME" & FreightIssue.Comments
                End If
                FreightIssueList.Add(FreightIssue)

                'ElseIf woidString.Length > 0 Then
                '    Dim dupList As New List(Of DuplicateReceipt)
                '    Dim dup As DuplicateReceipt = New DuplicateReceipt
                '    dupList.Add(dup)
                '    doPDF(dupList, True)
            Next
            doPDF(FreightIssueList)
        Else
            Response.Write("<br><br><br><br><br><center>This page is not directly accessable.</center>")
            Response.End()
        End If

    End Sub

    Protected Sub doPDF(ByVal FreightIssueList As List(Of FreightIssue))
        FreightIssueList = FreightIssueList.OrderBy(Function(x) x.Department).ToList
        Dim FreightIssue As FreightIssue = FreightIssueList.Item(0)
        'Dim compDAL As New Companies()
        'Dim contract As Contract = compDAL.getContract(compID)
        docFile = "FreightIssuesPhotoReport.pdf" 'dups.Item(0).woID.ToString & ".pdf"
        docSubject = "Purchase Order #: " & FreightIssueList.Item(0).PurchaseOrder
        '************************************************************************************
        '************************************ Create Document ****************************************
        '************************************************************************************
        theDoc.Color.String = "0 0 0"
        Font1 = theDoc.EmbedFont("Verdana")
        Font2 = theDoc.EmbedFont("Verdana Bold")
        Font3 = theDoc.EmbedFont("Arial")
        Font4 = theDoc.EmbedFont("Arial Bold")

        pgNumber = 0

        For Each FreightIssue In FreightIssueList
            DoHeader(theDoc, FreightIssue)
            doPOinfo(theDoc, FreightIssue)
            DoPhotos(theDoc, FreightIssue)

        Next
        DoFooter(theDoc, FreightIssue)

        If Request("email") = "true" Then
            'check for email
            eMailIt(theDoc, FreightIssueList)
        Else

            SendIt(theDoc)
        End If

    End Sub

    Sub DoHeader(ByRef theDoc As Doc, ByRef FreightIssue As FreightIssue)

        theDoc.AddPage(theDoc.PageCount + 1)
        theDoc.PageNumber = theDoc.PageCount

        '' ********** load images used in document **********
        logo.SetFile(Server.MapPath("../images/SoutheastUnloading.jpg"))

        'left side of header, left of logo
        Dim lblHeaderText As String = "<font pid=" & Font3 & " >Freight Issue<font size='-1'>(s)</font> Photo Report</font>"
        theDoc.TextStyle.HPos = 0
        theDoc.TextStyle.VPos = 1
        theDoc.Rect.Position(38, 748)
        theDoc.Rect.Right = 600
        theDoc.Rect.Top = 765
        '''''' FontSize
        theDoc.FontSize = 17
        theID = theDoc.AddHtml(lblHeaderText)

        Dim CompanyName As String = "<font pid=" & Font4 & " >Southeast Unloading</font>"
        theDoc.Rect.Position(38, 724)
        theDoc.Rect.Right = 200
        theDoc.Rect.Top = 750
        '''''' FontSize
        theDoc.FontSize = 13
        theID = theDoc.AddHtml(CompanyName)

        Dim CompanyAddress As String = "<font pid=" & Font3 & ">1864 South 14<sup>th</sup> Street<br />Fernandina Beach, Florida &nbsp;32043"
        theDoc.Rect.Position(38, 705)
        theDoc.Rect.Right = 200
        '''''' FontSize
        theDoc.FontSize = 9
        theID = theDoc.AddHtml(CompanyAddress)

        ' add logo at top of page, kinda centered
        theDoc.Rect.Position(233, 696)
        theDoc.Rect.Right = 396
        theDoc.Rect.Top = 739
        theDoc.AddImageObject(logo, False)

        'phone, taxid, date to right of logo
        Dim lblPhone As String = "<font pid=" & Font3 & ">Phone: (904) 491-6800</font>"
        Dim lblRdate As String = "<font pid=" & Font3 & ">* " & Date.Now.ToShortDateString & "</font"
        Dim lblDupNote As String = "<font pid=" & Font3 & ">* Date this document was issued</font>"
        theDoc.TextStyle.HPos = 1

        theDoc.Rect.Position(460, 725)
        theDoc.Rect.Right = 560
        theID = theDoc.AddHtml(lblPhone)

        theDoc.Rect.Position(460, 710)
        theDoc.Rect.Right = 560
        theID = theDoc.AddHtml(lblRdate)

        theDoc.Rect.Position(400, 693)
        theDoc.Rect.Right = 560
        '''''' FontSize
        theDoc.FontSize = 5
        theDoc.AddHtml(lblDupNote)
        '''''' FontSize

        ' separator line --- the 'date this duplicatte reciept was issued line is just above this line
        theDoc.AddLine(30, 690, 570, 690)
        Dim varValidationTerms As String = "<font pid=" & Font1 & ">" & FreightIssue.woid.ToString & "</font>"
        theDoc.TextStyle.HPos = 1
        theDoc.Rect.Position(250, 15)
        theDoc.Rect.Right = 580
        theDoc.FontSize = 6
        theDoc.AddHtml(varValidationTerms)
        theDoc.TextStyle.HPos = 0




    End Sub

    Sub doPOinfo(ByRef theDoc As Doc, ByVal FreightIssue As FreightIssue)
        'separate departments here
        theDoc.Color.String = "0 0 0"
        Dim lblDate As String = "<font pid=" & Font4 & ">Date:</font>"
        Dim lblLocation As String = "<font pid=" & Font4 & ">Location:</font>"
        Dim lblDepartment As String = "<font pid=" & Font4 & ">Department:</font>"
        Dim lblRestacks As String = "<font pid=" & Font4 & ">Restacks:</font>"
        Dim lblCarrier As String = "<font pid=" & Font4 & ">Carrier:</font>"
        Dim lblTrailer As String = "<font pid=" & Font4 & ">Trailer #:</font>"
        Dim lblVendorName As String = "<font pid=" & Font4 & ">Vendor:</font>"
        Dim lblVendorNumber As String = "<font pid=" & Font4 & ">Vendor #:</font>"
        Dim lblBadPallets As String = "<font pid=" & Font4 & ">Bad Pallets:</font>"
        lblPOnum = "<font pid=" & Font4 & ">Purchase Order:</font>"

        Dim varDate As String = "<font pid=" & Font3 & ">" & FormatDateTime(FreightIssue.LogDate, DateFormat.ShortDate) & "</font>"
        Dim varLocation As String = "<font pid=" & Font3 & ">" & FreightIssue.Location & "</font>"
        Dim varDepartment As String = "<font pid=" & Font3 & ">" & FreightIssue.Department & "</font>"
        Dim varRestacks As String = "<font pid=" & Font3 & ">" & FreightIssue.Restacks & "</font>"
        Dim varCarrier As String = "<font pid=" & Font3 & ">" & FreightIssue.Carrier & "</font>"
        Dim varTrailer As String = "<font pid=" & Font3 & ">" & FreightIssue.TrailerNumber & "</font>"
        Dim varVendorName As String = "<font pid=" & Font3 & ">" & FreightIssue.VendorName & "</font>"
        Dim varVendorNumber As String = "<font pid=" & Font3 & ">" & FreightIssue.VendorNumber & "</font>"
        Dim varBadPallets As String = "<font pid=" & Font3 & ">" & FreightIssue.BadPallets & "</font>"
        varPurchaseOrder = "<font pid=" & Font3 & ">" & FreightIssue.PurchaseOrder & "</font>"

        '''''' FontSize
        theDoc.FontSize = 12
        theDoc.TextStyle.HPos = 0
        theDoc.Rect.Right = 600
        theDoc.Rect.Position(70, 660)
        theID = theDoc.AddHtml(lblDate)

        theDoc.Rect.Position(210, 660)
        theID = theDoc.AddHtml(lblPOnum)

        theDoc.Rect.Position(395, 660)
        theID = theDoc.AddHtml(lblLocation)

        theDoc.Rect.Position(395, 640)
        theID = theDoc.AddHtml(lblDepartment)

        '''''' FontSize
        theDoc.TextStyle.HPos = 0
        theDoc.FontSize = 11
        theDoc.Rect.Position(105, 660)
        theDoc.AddHtml(varDate)

        theDoc.Rect.Position(310, 660)
        theDoc.AddHtml(varPurchaseOrder)

        theDoc.Rect.Position(455, 660)
        theDoc.AddHtml(varLocation)

        theDoc.Rect.Position(470, 640)
        theDoc.AddHtml(varDepartment)

        theDoc.FontSize = 12
        theDoc.TextStyle.HPos = 0
        theDoc.Rect.Position(70, 640)
        theID = theDoc.AddHtml(lblRestacks)

        theDoc.TextStyle.HPos = 0
        theDoc.Rect.Position(210, 640)
        theID = theDoc.AddHtml(lblBadPallets)

        theDoc.FontSize = 11
        theDoc.Rect.Position(130, 640)
        theDoc.AddHtml(varRestacks)

        theDoc.Rect.Position(287, 640)
        theDoc.AddHtml(varBadPallets)

        theDoc.FontSize = 12
        theDoc.Rect.Position(70, 620)
        theID = theDoc.AddHtml(lblVendorNumber)

        theDoc.Rect.Position(205, 620)
        theDoc.Rect.Right = 610
        theID = theDoc.AddHtml(lblVendorName)

        theDoc.FontSize = 12
        theDoc.Rect.Position(70, 600)
        theID = theDoc.AddHtml(lblTrailer)

        theDoc.Rect.Position(205, 600)
        theID = theDoc.AddHtml(lblCarrier)

        theDoc.FontSize = 11
        theDoc.Rect.Position(127, 620)
        theDoc.AddHtml(varVendorNumber)

        theDoc.Rect.Position(255, 620)
        theDoc.AddHtml(varVendorName)

        theDoc.Rect.Position(127, 600)
        theDoc.AddHtml(varTrailer)

        theDoc.Rect.Position(255, 600)
        theDoc.AddHtml(varCarrier)


        Dim lblVerificationKey As String = "<font pid=" & Font3 & ">validation code: " & FreightIssue.woid.ToString & "</font>"
        theDoc.TextStyle.HPos = 0
        '''''' FontSize
        theDoc.FontSize = 6
        theDoc.Rect.Position(368, 585)
        theDoc.Rect.Right = 560
        theDoc.TextStyle.HPos = 1
        theDoc.AddHtml(lblVerificationKey)
        theDoc.AddLine(403, 585, 562, 585)
        If FreightIssue.Comments.Contains("SHOWME") Then
            Dim lblComments As String = "<font pid=" & Font4 & ">COMMENTS </font>"
            theDoc.TextStyle.HPos = 0

            ' put asterisk in front of Purchase Order Label
            theDoc.FontSize = 9
            Dim lblasterisk As String = "<font pid=" & Font4 & ">**</font>"
            theDoc.Rect.Position(377, 602)
            '            theDoc.AddHtml(lblasterisk)

            theDoc.FontSize = 8
            theDoc.Rect.Position(35, 580)
            theDoc.Rect.Right = 550
            theDoc.AddHtml(lblComments)
            Dim varComments As String = "<font pid=" & Font3 & ">" & Right(FreightIssue.Comments, Len(FreightIssue.Comments) - 6) & "</font>"
            theDoc.Rect.Position(55, 570)
            theDoc.Rect.Right = 550
            theDoc.AddHtml(varComments)
            theDoc.AddLine(30, 530, 580, 530)
            cl1 = 568 ' bottom position
        Else
            theDoc.AddLine(30, 568, 580, 568)
            cl1 = 568 ' bottom position
        End If
    End Sub

    Sub DoPhotos(ByRef theDoc As Doc, ByVal FreightIssue As FreightIssue)

        Dim dba As New DBAccess()
        dba.CommandText = "SELECT ImageData, ImageName, UserID, ImageID FROM LoadImages WHERE WorkOrderID = @woid"
        dba.AddParameter("@woid", FreightIssue.woid)
        Dim dti As DataTable = dba.ExecuteDataSet.Tables(0)

        ' ********** define column boundries **********
        page1L = Split("36 30 307 550")
        page1R = Split("307 30 576 550")
        'page 2 and beyond ''''''''' left bottom right top
        page2L = Split("36 30 307 770")
        page2R = Split("307 30 576 770")
        ' ********** define current location variables **********
        ' ********** init some counters and variables **********

        Dim imageWidth As Integer = 270
        Dim imageHeight As Integer = imageWidth * 0.75

        ' place first photo
        cl0 = page1L(0)
        cl1 = cl1 - imageHeight - 50 ' this is the margin between header bottom line and top of first photo
        cl2 = cl0 + imageWidth
        cl3 = cl1 + imageHeight

        Dim lcol As Integer = 36
        Dim rcol As Integer = 307

        theDoc.Rect.String = cl0.ToString & " " & cl1.ToString & " " & cl2.ToString & " " & cl3.ToString
        Dim picCount As Integer = dti.Rows.Count
        Dim counter As Integer = 1
        For Each r As DataRow In dti.Rows()
            Dim LoadImg As XImage = New XImage()
            Dim theData As Byte() = r.Item("ImageData")
            LoadImg.SetData(theData)

            Dim col As Integer = IIf((counter Mod 2) = 0, page1R(0), page1L(0))

            theDoc.TextStyle.HPos = 0
            theDoc.Rect.Position(col + 5, cl1 - 10)
            theDoc.AddText("Photograph # " & counter & " of " & picCount)
            theDoc.Rect.Position(col, cl1)
            theDoc.AddImageObject(LoadImg)

            If counter Mod 2 = 0 Then
                cl1 = cl1 - imageHeight - 35
            End If
            counter = counter + 1
            If picCount >= counter Then

                If counter = 5 Or counter = 11 Or counter = 17 Or counter = 23 Or counter = 29 Or counter = 35 Then
                    theDoc.AddPage(theDoc.PageCount + 1)
                    theDoc.PageNumber = theDoc.PageCount
                    Dim varValidationTerms As String = "<font pid=" & Font1 & ">" & FreightIssue.woid.ToString & "</font>"
                    theDoc.TextStyle.HPos = 1
                    theDoc.Rect.Position(250, 15)
                    theDoc.Rect.Right = 580
                    theDoc.FontSize = 6
                    theDoc.AddHtml(varValidationTerms)
                    theDoc.TextStyle.HPos = 0
                    Dim lblHeaderText As String = "<font pid=" & Font3 & " >Freight Issue<font size='-1'>(s)</font> Photo Report</font>&nbsp;<font size='3'>(continued)</font>"
                    theDoc.FontSize = 17
                    theDoc.Rect.Position(38, 748)
                    theDoc.Rect.Width = 500
                    theID = theDoc.AddHtml(lblHeaderText)
                    theDoc.Rect.Position(400, 748)
                    theDoc.Rect.Right = page2R(2)
                    theDoc.TextStyle.HPos = 1
                    theDoc.FontSize = 12
                    theDoc.AddHtml(lblPOnum & " " & varPurchaseOrder)


                    theDoc.TextStyle.HPos = 0
                    theDoc.FontSize = 6
                    theDoc.Rect.Width = imageWidth
                    '                    ccol = 1
                    cl1 = page2L(3) - imageHeight - 50

                End If
            End If


        Next



        theDoc.Color.String = "0 0 0"
        '        theDoc.AddGrid()
        pgNumber += 1



        '************************************************************************************
    End Sub

    Sub DoFooter(ByRef theDoc As Doc, ByVal FreightIssue As FreightIssue)
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
            Dim varVersion As String = "<font pid=" & Font1 & ">SEU Freight Issue Photo Report Generator v2.0</font>"
            theDoc.Color.String = "0 0 0"
            theDoc.TextStyle.HPos = 0
            theDoc.Rect.Position(25, 15)
            theDoc.FontSize = 6
            theDoc.AddHtml(varVersion)


        Next
    End Sub


    Sub SendIt(ByRef theDoc As Doc)
        If theDoc.GetInfo(-1, "/Info") = "" Then
            theDoc.SetInfo(-1, "/Info:Ref", theDoc.AddObject("<< >>").ToString())
        End If
        theDoc.SetInfo(-1, "/Info*/Title:Text", "Southeast Unloading Freight Issues Photo Report")
        theDoc.SetInfo(-1, "/Info*/Author:Text", "Diversified Logistics - Southeast Unloading")
        theDoc.SetInfo(-1, "/Info*/Subject:Text", "NOTICE: each validation code must match our records")
        theDoc.SetInfo(theDoc.Root, "/Metadata:Del", "")
        Dim thuData() As Byte = theDoc.GetData()
        '        Response.Clear()
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "inline; filename=SEU_FreightIssuesPhotoReport.PDF")
        Response.AddHeader("content-length", thuData.Length.ToString())
        Response.BinaryWrite(thuData)
        Response.End()
    End Sub

    Sub eMailIt(ByRef theDoc As Doc, ByVal freightIssueList As List(Of FreightIssue))
        Dim pdf As Stream = New MemoryStream
        theDoc.Save(pdf)
        theDoc.Clear()
        pdf.Position = 0

        Dim msg As New MailMessage
        If freightIssueList(0).email Is Nothing Then
            Response.Write("No eMail address on file!<br /> Send FAILED")
            Exit Sub
        End If
        msg.To.Add(freightIssueList(0).email)
        msg.IsBodyHtml = True
        If Not freightIssueList(0).emailCC Is Nothing Then
            If freightIssueList(0).emailCC > "" Then msg.CC.Add(freightIssueList(0).emailCC)
        End If
        '        msg.CC.Add("randy@realwebs.com")
        msg.From = New MailAddress("Diversified Logistics <no-reply@Div-Log.com>")
        msg.Subject = "SEU - Freight Issue(s) Photo Report"
        msg.Attachments.Add(New Attachment(pdf, "SEU_FreightIssuesPhotoReport_" & freightIssueList(0).LoadNumber & ".PDF", "application/pdf"))
        Dim body As String = "Please see the attached Freight Issues Photo Report - <br />" & _
                    "<table><tr><td>Load/CMV #(s):</td><td>  "
        For Each f As FreightIssue In freightIssueList
            body &= f.LoadNumber & "</td></tr><tr><td></td><td>"
        Next
        body &= "</td></tr></table><br />SEU Location: " & freightIssueList(0).Location

        msg.Body = body
        Dim smtp As SmtpClient = New SmtpClient("mail.div-log.com")
        smtp.Port = 25
        smtp.Credentials = New System.Net.NetworkCredential("donotreply@div-log.com", "2n@f1s#")
        Try
            smtp.Send(msg)
            Response.Write("email Sent")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try


        Dim dba As New DBAccess()
        dba.CommandText = "UPDATE Location Set HasPics=0, PicsLastSent= '" & Date.Now & "'"
        dba.ExecuteNonQuery()

        Response.End()
    End Sub




End Class