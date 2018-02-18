Imports WebSupergoo.ABCpdf10

Public Class LoadImagesToPDF
    Inherits System.Web.UI.Page
    Dim DBA As New DBAccess()
    Dim db As String
    Dim page1L, page1R, page2l, page2R As Array
    Dim cl0, cl1, cl2, cl3, cpage, ccol As Integer
    ' *********** define some objects ***************
    Dim theDoc As New Doc()
    Dim ht, Font1, Font2, Font3, Font4 As String
    Dim currtcat, currscat, currsscat, currssscat, currlevel, cname, cnametitle As String
    Dim a, b, c, d, theID As Integer
    Dim isblank As Boolean
    Dim thudate As Date = Nothing
    Dim ds As DataSet
    Dim dt As DataTable
    Dim rw, trw As DataRow
    Dim styp1, styp2, styp3, stp As String
    Dim dict As New Dictionary(Of String, String)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim woid As String = Request.QueryString("woid").Trim.ToString
        If Utilities.IsValidGuid(woid) Then
            Dim dba As New DBAccess
            dba.CommandText = "SELECT UP.FirstName + ' ' + UP.LastName AS Uploader, LI.ImageID, LI.ImageName, LI.ImageData " & _
                "FROM LoadImages LI INNER JOIN " & _
                "UserProfile UP ON LI.UserID = UP.userID " & _
                "WHERE LI.WorkOrderID=@woid "
            dba.AddParameter("@woid", woid)
            Dim dt As DataTable = New DataTable
            dt = dba.ExecuteDataSet.Tables(0)
            If dt.Rows.Count > 0 Then
                Dim woDAL As New WorkOrderDAL
                Dim workOrder As WorkOrder = woDAL.GetLoadByID(woid)
                CreatePDF(workOrder, dt, False)





            Else

                Response.Write("<br /><br /><br /><br /><br /><center>This page not directly accessable</center>")
                Response.End()

            End If
        End If


    End Sub

    Public docFile As String = Nothing
    Public docSubject As String = Nothing

    Private Sub CreatePDF(ByVal woid As WorkOrder, ByVal dt As DataTable, Optional ByVal isFav As Boolean = False)
        '   FirstName + ' ' + LastName AS Uploader, ImageID, ImageName, ImageData 
        If Not isFav Then
            'Dim compDAL As New Companies()
            'Dim contract As Contract = compDAL.getContract(compID)
            docFile = "WorkOrderPictures.pdf" 'dups.Item(0).woID.ToString & ".pdf"
            docSubject = "Receipt Number: "  '& dups.Item(0).ReceiptNumber
        Else
            'Dim compDAL As New Companies()
            'Dim contract As Contract = compDAL.getContract(compID)
            docFile = "DuplicateReceipt.pdf" 'dups.Item(0).woID.ToString & ".pdf"
            docSubject = "Receipt Number: "  '& dups.Item(0).ReceiptNumber
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

        Dim logo As XImage = New XImage()
        logo.SetFile(Server.MapPath("../images/SoutheastUnloading.jpg"))
        Dim theWaterMark As XImage = New XImage()
        theWaterMark.SetFile(Server.MapPath("../images/SouthEastUnloadingWaterMark.png"))
        Dim limage As XImage = New XImage()
        


        limage.SetData(dt.Rows(0)("ImageData"))
        '       theDoc.Rect.Inset(20, 20)
        'images are 384 x 512
        'images to fit w 10px pic 10px pic 10px
        'need to be 290 wide by 217.5 tall
        theDoc.Rect.Position(10, 450)
        theDoc.Rect.Height = 217
        theDoc.Rect.Right = 300
        theDoc.AddImageObject(limage, False)
        limage.Clear()
        Dim pgNumber As Integer = 0
        theDoc.AddGrid()
        If pgNumber > 0 Then theDoc.AddPage(pgNumber + 1)
        theDoc.PageNumber = pgNumber + 1
        theDoc.Color.String = "0 0 0"
        'left side of header, left of logo
        Dim lblHeaderText As String = "<font pid=" & Font3 & " >DUPLICATE &nbsp; RECEIPT</font>"
        theDoc.TextStyle.HPos = 0
        theDoc.TextStyle.VPos = 1
        theDoc.Rect.Position(38, 748)
        theDoc.Rect.Right = 250
        theDoc.Rect.Top = 765
        '''''' FontSize
        theDoc.FontSize = 17
        theID = theDoc.AddHtml(lblHeaderText)

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
        theDoc.Rect.Position(233, 702)
        theDoc.Rect.Right = 396
        theDoc.Rect.Top = 739
        theDoc.AddImageObject(logo, False)

        'phone, taxid, date to right of logo
        Dim lblPhone As String = "<font pid=" & Font3 & ">Phone: (904) 491-6800</font>"
        Dim lblTaxID As String = "<font pid=" & Font3 & ">Tax ID: 593746670</font>"
        Dim lblRdate As String = "<font pid=" & Font3 & ">* " & Date.Now.ToShortDateString & "</font"
        Dim lblDupNote As String = "<font pid=" & Font3 & ">* Date this duplicate receipt was issued</font>"
        theDoc.TextStyle.HPos = 1
        theDoc.Rect.Position(460, 740)
        theDoc.Rect.Right = 560
        theID = theDoc.AddHtml(lblPhone)

        theDoc.Rect.Position(460, 725)
        theDoc.Rect.Right = 560
        theID = theDoc.AddHtml(lblTaxID)

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

        Dim theCount As Integer = theDoc.PageCount
        For i = 1 To theCount

            theDoc.PageNumber = i
            '            theDoc.Layer = theDoc.LayerCount + 1
            theDoc.Rect.String = "53 170 557 314"
            If i = 1 Then
                theID = theDoc.AddImageObject(theWaterMark, True)
            Else
                theDoc.AddImageCopy(theID)
            End If

            Dim varVersion As String = "<font pid=" & Font1 & ">SEU Duplicate Receipt Generator v1.3</font>"
            Dim varValidationTerms As String = "<font pid=" & Font1 & ">Duplicate Receipt valid only if validation code above matches our records</font>"
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
        theDoc.SetInfo(-1, "/Info*/Title:Text", "Southeast Unloading WorkOrder Pictures")
        theDoc.SetInfo(-1, "/Info*/Author:Text", "Diversified Logistics - Southeast Unloading")
        theDoc.SetInfo(-1, "/Info*/Subject:Text", "NOTICE: (valid only if each validation code matches our records")
        theDoc.SetInfo(theDoc.Root, "/Metadata:Del", "")

        Dim thuData() As Byte = theDoc.GetData()

        '        Response.Clear()
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "inline; filename=SEU_Div-Log.PDF")
        Response.AddHeader("content-length", thuData.Length.ToString())
        Response.BinaryWrite(thuData)
        Response.End()



    End Sub



End Class