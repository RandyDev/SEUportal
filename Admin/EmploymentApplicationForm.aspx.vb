Imports System
Imports WebSupergoo.ABCpdf10

Public Class EmploymentApplicationForm
    Inherits System.Web.UI.Page

    Public Font1, Font2, Font3, Font4, Font5, Font6 As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request("appid") Is Nothing Then
            CreatePDF()
            'CreatePDF("6399913e-8508-4a37-bad2-37dcf94f188d")
        Else
            CreatePDF(Request("appid"))
        End If
    End Sub

    Public docFile As String = Nothing
    Public docSubject As String = Nothing

    Private Sub CreatePDF(Optional ByVal eid As String = "0")
        Dim app As New EmploymentApplicationObject()

        If Utilities.IsValidGuid(eid) Then
            Dim edal As New empDAL()
            app = edal.getJobApp(eid)
        End If

        docFile = "EmploymentApplication_" & app.LastName & "_" & app.FirstName & ".pdf"   'dups.Item(0).woID.ToString & ".pdf"
        docSubject = "AppID: " & app.EmploymentApplicationID.ToString()

        Dim theDoc As New Doc()
        theDoc.Color.String = "0 0 0"
        Dim SansFont As Integer = theDoc.AddFont("Arial")
        Dim SerrifFont As Integer = theDoc.AddFont("Times New Roman")
        '' ********** embed some fonts **********
        ' ********** fonts
        Font1 = theDoc.EmbedFont("Verdana")
        Font2 = theDoc.EmbedFont("Verdana Bold")
        Font3 = theDoc.EmbedFont("Arial")
        Font4 = theDoc.EmbedFont("Arial Bold")
        Font5 = theDoc.EmbedFont("Times-Roman")
        Font6 = theDoc.EmbedFont("Times-Bold")

        ' ************************************************************************************************
        ' ****   Import Supporting Documents
        ' ************************************************************************************************
        Dim importDoc As String = "EMPLOYMENTAPPLICATION.xps"
        Dim fext As String = Right(importDoc, 3)

        If fext = "pdf" Or fext = "xps" Then
            '            theDoc.Read(Server.MapPath(importDoc))
        ElseIf fext = "doc" Or fext = "ocx" Then
            '            Dim svcs As New PdfEnterpriseServices.PdfUtilities
            '            theDoc.Read(svcs.DocToPdf(Server.MapPath(importDoc)))
        End If

        Dim supDocumentCount As Integer = theDoc.PageCount

        Dim aid As String = Request("appID")
        Dim dba As New DBAccess
        dba.CommandText = "SELECT eac.companyABR " & _
            "FROM employmentApplicationCompanies eac INNER JOIN " & _
            "employmentApplicationLocations eal ON eac.companyID = eal.companyID RIGHT OUTER JOIN " & _
            "EmploymentApplication ea ON eal.companyLocationID = ea.LocationID " & _
            "WHERE (ea.EmploymentApplicationID = @EmploymentApplicationID) "
        dba.AddParameter("@EmploymentApplicationID", aid)
        Dim co As String = dba.ExecuteScalar.ToString
        If co Is Nothing Then co = "SEU"
        Dim logo As XImage = New XImage()
        Select Case co
            Case "FCP"
                logo.SetFile(Server.MapPath("../images/FCP.jpg"))
            Case "SEU"
                logo.SetFile(Server.MapPath("../images/seu.jpg"))
            Case "TCE"
                logo.SetFile(Server.MapPath("../images/TripleCenterprise.jpg"))
            Case Else
                logo.SetFile(Server.MapPath("../images/TripleCenterprise.jpg"))


        End Select

        Dim theWaterMark As XImage = New XImage()
        theWaterMark.SetFile(Server.MapPath("../images/SouthEastUnloadingWaterMark.png"))

        '        theDoc.AddGrid()
        'If pgNumber > 0 Then theDoc.AddPage(pgNumber + 1)
        'theDoc.PageNumber = pgNumber + 1
        theDoc.Page = theDoc.AddPage()
        theDoc.PageNumber = 3
        '        theDoc.Page = 0
        theDoc.Color.String = "0 0 0"
        theDoc.Width = 1
        ' add logo at top of page, kinda centered
        theDoc.Rect.Position(225, 720)
        theDoc.Rect.Right = 390
        theDoc.Rect.Top = 760
        theDoc.AddImageObject(logo, False)

        theDoc.Rect.Position(30, 677)
        theDoc.Rect.Right = 580
        '        theDoc.FrameRect()

        theDoc.TextStyle.HPos = 0.5
        Dim lblHeaderText As String = "<font pid=" & Font5 & " >APPLICATION FOR EMPLOYMENT</font>"
        '''''' FontSize
        theDoc.FontSize = 14
        theDoc.AddHtml(lblHeaderText)

        '' separator line --- the 'date this duplicatte reciept was issued line is just above this line
        'theDoc.AddLine(30, 690, 580, 690)

        ' '''''' FontSize


        theDoc.TextStyle.HPos = 0
        theDoc.TextStyle.VPos = 1
        theDoc.FontSize = 10
        theDoc.TextStyle.Bold = True
        theDoc.Rect.Position(30, 680)
        theDoc.AddText("PERSONAL INFORMATION")
        theDoc.TextStyle.Bold = False

        theDoc.Rect.Position(30, 590)
        theDoc.Rect.Top = 680
        theDoc.Rect.Right = 580
        theDoc.FrameRect()
        theDoc.AddLine(30, 650, 580, 650)
        theDoc.AddLine(30, 620, 580, 620)
        theDoc.FontSize = 8
        theDoc.Rect.Position(35, 670)
        theDoc.AddText("NAME (last, first, middle int.)")
        theDoc.AddLine(300, 590, 300, 680)
        theDoc.Rect.Position(305, 670)
        theDoc.AddText("REFERRED BY")

        theDoc.Rect.Position(35, 640)
        theDoc.AddText("ADDRESS")
        theDoc.Rect.Position(305, 640)
        theDoc.AddText("CITY")
        theDoc.AddLine(395, 620, 395, 650)
        theDoc.Rect.Position(400, 640)
        theDoc.AddText("STATE")
        theDoc.AddLine(495, 620, 495, 650)
        theDoc.Rect.Position(500, 640)
        theDoc.AddText("ZIP")
        theDoc.Rect.Position(35, 610)
        theDoc.AddText("PRIMARY PHONE")
        theDoc.Rect.Position(305, 610)
        theDoc.AddText("ALTERNATE PHONE")

        theDoc.Rect.Position(30, 565)
        theDoc.FontSize = 10
        theDoc.TextStyle.Bold = True
        theDoc.AddText("EMPLOYMENT DESIRED")
        theDoc.TextStyle.Bold = False
        theDoc.Rect.Position(30, 475)
        theDoc.Rect.Top = 565
        theDoc.Rect.Right = 580
        theDoc.FrameRect()
        theDoc.AddLine(30, 535, 580, 535)
        theDoc.AddLine(30, 505, 580, 505)
        theDoc.FontSize = 8
        theDoc.Rect.Position(35, 555)
        theDoc.AddText("POSITION")
        theDoc.AddLine(200, 475, 200, 565)
        theDoc.Rect.Position(205, 555)
        theDoc.AddText("DATE YOU CAN START")
        theDoc.AddLine(400, 535, 400, 565)
        theDoc.Rect.Position(405, 555)
        theDoc.AddText("SALARY DESIRED")
        theDoc.Rect.Position(35, 525)
        theDoc.AddText("ARE YOU CURRENTLY EMPLOYED?")
        theDoc.Rect.Position(205, 525)
        theDoc.AddText("IF SO, MAY WE CONTACT YOUR PRESENT EMPLOYER?")
        theDoc.Rect.Position(35, 495)
        theDoc.AddText("EVER APPLIED TO SEU BEFORE?")
        theDoc.Rect.Position(205, 495)
        theDoc.AddText("WHERE?")
        theDoc.AddLine(400, 475, 400, 505)
        theDoc.Rect.Position(405, 495)
        theDoc.AddText("WHEN?")

        theDoc.Rect.Position(30, 450)
        theDoc.FontSize = 10
        theDoc.TextStyle.Bold = True
        theDoc.AddText("EDUCATION")
        theDoc.TextStyle.Bold = False

        theDoc.FontSize = 8
        theDoc.Rect.Position(30, 340)
        theDoc.Rect.Top = 450
        theDoc.FrameRect()
        theDoc.AddLine(30, 430, 580, 430)
        theDoc.AddLine(30, 400, 580, 400)
        theDoc.AddLine(30, 370, 580, 370)

        theDoc.AddLine(275, 340, 275, 430)
        theDoc.AddLine(330, 340, 330, 430)
        theDoc.AddLine(385, 340, 385, 430)

        theDoc.Rect.Position(110, 430)
        theDoc.AddText("NAME AND LOCATION OF SCHOOL")
        theDoc.Rect.Position(290, 440)
        theDoc.AddText("YEARS")
        theDoc.Rect.Position(282, 430)
        theDoc.AddText("ATTENDED")
        theDoc.Rect.Position(333, 440)
        theDoc.AddText("GRADUATED")
        theDoc.Rect.Position(342, 430)
        theDoc.AddText("YES / NO")
        theDoc.Rect.Position(440, 430)
        theDoc.AddText("SUBJECTS STUDIED")
        theDoc.AddLine(75, 340, 75, 430)

        theDoc.TextStyle.VPos = 0.5
        theDoc.TextStyle.HPos = 0.5

        theDoc.FontSize = 6
        theDoc.Rect.Position(30, 410)
        theDoc.Rect.Width = 45
        theDoc.Rect.Height = 15
        theDoc.AddText("HIGHSCHOOL")
        theDoc.Rect.Position(30, 400)
        theDoc.AddText("EQUIVALENT")

        theDoc.FontSize = 8
        theDoc.Rect.Position(30, 370)
        theDoc.Rect.Height = 30
        theDoc.AddText("COLLEGE")

        theDoc.FontSize = 7
        theDoc.Rect.Position(30, 355)
        theDoc.Rect.Height = 15
        theDoc.AddText("TRADE /")
        theDoc.Rect.Position(30, 345)
        theDoc.AddText("BUSINESS")

        theDoc.Rect.Right = 580
        theDoc.TextStyle.HPos = 0
        theDoc.TextStyle.VPos = 1
        theDoc.Rect.Height = 30

        theDoc.FontSize = 10
        theDoc.TextStyle.Bold = True
        theDoc.Rect.Position(30, 315)
        theDoc.AddText("GENERAL")
        theDoc.TextStyle.Bold = False
        theDoc.Rect.Position(30, 255)
        theDoc.Rect.Top = 315
        theDoc.FrameRect()
        theDoc.AddLine(30, 285, 580, 285)

        theDoc.FontSize = 7
        theDoc.Rect.Position(35, 305)
        theDoc.AddText("Subjects of special study, research work, special training or skills")
        theDoc.FontSize = 8
        theDoc.Rect.Position(35, 275)
        theDoc.AddText("US MILITARY")
        theDoc.AddLine(100, 255, 100, 285)
        theDoc.Rect.Position(105, 275)
        theDoc.AddText("DATES OF SERVICE")
        theDoc.AddLine(300, 255, 300, 285)
        theDoc.Rect.Position(305, 275)
        theDoc.AddText("RANK")


        theDoc.FontSize = 10
        theDoc.TextStyle.Bold = True
        theDoc.Rect.Position(30, 230)
        theDoc.AddText("FORMER EMPLOYMENT")
        theDoc.TextStyle.Bold = False
        theDoc.FontSize = 7
        theDoc.TextStyle.Italic = True
        theDoc.Rect.Position(147, 230)
        theDoc.AddText("(Most current first)")
        theDoc.TextStyle.Italic = False
        theDoc.Rect.Position(300, 230)
        theDoc.AddText("LAST 4 EMPLOYERS OR LAST 10 YEARS OF EMPLOYMENT")
        theDoc.Rect.Position(30, 90)
        theDoc.Rect.Top = 230
        theDoc.FrameRect()
        theDoc.AddLine(30, 210, 580, 210)
        theDoc.AddLine(30, 180, 580, 180)
        theDoc.AddLine(30, 150, 580, 150)
        theDoc.AddLine(30, 120, 580, 120)
        theDoc.FontSize = 8
        theDoc.Rect.Position(35, 215)
        theDoc.AddText("MONTH, YEAR")
        theDoc.AddLine(95, 90, 95, 210)
        theDoc.Rect.Position(100, 215)
        theDoc.AddText("NAME AND ADDRESS OF EMPLOYER")
        theDoc.AddLine(245, 90, 245, 210)
        theDoc.Rect.Position(265, 215)
        theDoc.AddText("PHONE")
        theDoc.AddLine(315, 90, 315, 210)
        theDoc.Rect.Position(330, 215)
        theDoc.AddText("POSITION")
        theDoc.AddLine(380, 90, 380, 210)
        theDoc.Rect.Position(390, 215)
        theDoc.AddText("SALARY")
        theDoc.AddLine(430, 90, 430, 210)
        theDoc.Rect.Position(450, 215)
        theDoc.AddText("REASON FOR LEAVING")

        theDoc.FontSize = 6
        theDoc.Rect.Position(32, 203)
        theDoc.AddText("TO")
        theDoc.AddLine(30, 195, 95, 195)
        theDoc.Rect.Position(32, 188)
        theDoc.AddText("FROM")

        theDoc.Rect.Position(32, 173)
        theDoc.AddText("TO")
        theDoc.AddLine(30, 165, 95, 165)
        theDoc.Rect.Position(32, 158)
        theDoc.AddText("FROM")

        theDoc.Rect.Position(32, 143)
        theDoc.AddText("TO")
        theDoc.AddLine(30, 135, 95, 135)
        theDoc.Rect.Position(32, 128)
        theDoc.AddText("FROM")

        theDoc.Rect.Position(32, 113)
        theDoc.AddText("TO")
        theDoc.AddLine(30, 105, 95, 105)
        theDoc.Rect.Position(32, 98)
        theDoc.AddText("FROM")

        theDoc.Page = theDoc.AddPage()
        theDoc.TextStyle.HPos = 0
        theDoc.TextStyle.VPos = 1


        theDoc.FontSize = 9
        theDoc.TextStyle.Bold = True
        theDoc.Rect.Position(30, 750)
        theDoc.AddText("REFERENCES")
        theDoc.Rect.Position(170, 750)
        theDoc.TextStyle.Italic = True
        theDoc.TextStyle.Underline = True
        theDoc.AddText("You must have known these persons for a minimum of one year")
        theDoc.TextStyle.Italic = False
        theDoc.TextStyle.Underline = False
        theDoc.TextStyle.Bold = False
        theDoc.Rect.Position(30, 640)
        theDoc.Rect.Top = 750
        theDoc.Rect.Right = 580
        theDoc.FrameRect()
        theDoc.AddLine(30, 730, 580, 730)
        theDoc.AddLine(30, 700, 580, 700)
        theDoc.AddLine(30, 670, 580, 670)

        theDoc.AddLine(45, 640, 45, 750)
        theDoc.AddLine(170, 640, 170, 750)
        theDoc.AddLine(540, 640, 540, 750)
        theDoc.Rect.Position(60, 735)
        theDoc.AddText("NAME")
        theDoc.Rect.Position(190, 735)
        theDoc.AddText("BEST WAY TO REACH REFERENCE: EMAIL / CELL PHONE / HOME PHONE / ETC")
        theDoc.Rect.Position(545, 740)
        theDoc.AddText("YEARS")
        theDoc.Rect.Position(543, 730)
        theDoc.AddText("KNOWN")
        theDoc.TextStyle.Bold = True
        theDoc.Rect.Position(33, 710)
        theDoc.AddText("1")
        theDoc.Rect.Position(33, 680)
        theDoc.AddText("2")
        theDoc.Rect.Position(33, 650)
        theDoc.AddText("3")
        theDoc.TextStyle.Bold = False


        Dim ack1 As String = "Southeast Unloading, LLC is an equal opportunity employer. Southeast Unloading, LLC does not discriminate " & _
            "in employment on account of race, color, religion, national origin, citizenship status, ancestry, age, sex (including " & _
            "sexual harassment), sexual orientation, marital status, physical or mental disability, military status or " & _
            "unfavorable discharge from military service."

        Dim ack2 As String = "I understand that neither the completion of this application nor any other part of my consideration for " & _
            "employment establishes any obligation for Southeast Unloading, LLC to hire me.  If I am hired, I understand " & _
            "that both Southeast Unloading, LLC and/or myself can terminate my employment at any time and for any reason, " & _
            "with or without cause and without prior notice.  I understand that no representative of Southeast Unloading, LLC " & _
            "has the authority to make any assurance to the contrary."

        Dim ack3 As String = "I attest with my signature below that I have given to Southeast Unloading, LLC true and complete information on  " & _
            "this application. No requested information has been concealed.  I authorize Southeast Unloading, LLC to contact " & _
            "references provided for employment reference checks.  If any information I have provided is untrue, or if I have " & _
            "concealed material information, I understand that this will constitute cause for the denial of employment or " & _
            "immediate dismissal."
        theDoc.Rect.Position(30, 330)
        theDoc.Rect.Top = 625
        theDoc.FrameRect()
        theDoc.TextStyle.HPos = 0.5
        theDoc.Rect.Position(30, 610)
        theDoc.Rect.Right = 580
        theDoc.TextStyle.Underline = True
        theDoc.FontSize = 9
        theDoc.TextStyle.Bold = True
        theDoc.AddText("Please read before signing")
        theDoc.TextStyle.HPos = 0
        theDoc.TextStyle.Underline = False
        theDoc.TextStyle.Bold = False
        theDoc.FontSize = 8

        theDoc.TextStyle.VPos = 0
        theDoc.Rect.Position(35, 550)
        theDoc.Rect.Top = 595
        theDoc.Rect.Right = 575
        theDoc.AddText(ack1)

        theDoc.Rect.Position(35, 470)
        theDoc.Rect.Top = 565
        theDoc.Rect.Right = 575
        theDoc.AddText(ack2)

        theDoc.Rect.Position(35, 435)
        theDoc.Rect.Top = 530
        theDoc.Rect.Right = 575
        theDoc.AddText(ack3)
        theDoc.TextStyle.VPos = 1
        theDoc.Rect.Position(35, 470)
        theDoc.AddText("Date: _____________________________")
        theDoc.Rect.Position(300, 470)
        theDoc.AddText("Signature: ______________________________________________")

        'theDoc.Rect.Position(30, 160)
        'theDoc.Rect.Top = 445
        'theDoc.Rect.Right = 585
        'theDoc.FrameRect()
        theDoc.AddLine(30, 415, 580, 415)
        theDoc.AddLine(30, 385, 580, 385)
        theDoc.AddLine(30, 355, 580, 355)
        theDoc.AddLine(30, 325, 580, 325)

        theDoc.AddLine(140, 295, 140, 325)
        theDoc.AddLine(250, 295, 250, 325)
        theDoc.AddLine(360, 295, 360, 325)
        theDoc.AddLine(470, 295, 470, 325)
        theDoc.FontSize = 8
        theDoc.Rect.Position(35, 435)
        theDoc.TextStyle.Bold = True
        theDoc.AddText("REMARKS:")
        theDoc.TextStyle.Bold = False
        theDoc.Rect.Position(35, 315)
        theDoc.AddText("HIRED")
        theDoc.Rect.Position(145, 315)
        theDoc.AddText("DEPARTMENT")
        theDoc.Rect.Position(255, 315)
        theDoc.AddText("POSITION")
        theDoc.Rect.Position(365, 315)
        theDoc.AddText("WILL REPORT ON")
        theDoc.Rect.Position(475, 315)
        theDoc.AddText("SALARY/WAGE")
        theDoc.Rect.Position(35, 260)
        theDoc.AddText("APPROVED BY: __________________________________________")
        theDoc.Rect.Position(300, 260)
        theDoc.AddText("DATE: ___________________________________")
        theDoc.Rect.Position(145, 251)
        theDoc.AddText("Operations Manager")

        ''************** applicant responses
        If Utilities.IsValidGuid(eid) Then
            doApplicantResponse(theDoc, app)
        End If




        Dim thecount As Integer = theDoc.PageCount
        Dim divcount As Integer = thecount = supDocumentCount
        Dim mapstr As String = String.Empty
        mapstr = "3 4 1 2"
        '        theDoc.RemapPages(mapstr)

        ' ***************** Do Page Footers and Water marks
        Dim gstring As String = Guid.NewGuid().ToString
        theDoc.TextStyle.VPos = 1

        For i = 1 To theDoc.PageCount
            theDoc.PageNumber = i
            '            theDoc.AddGrid()
            theDoc.TextStyle.HPos = 0.5
            theDoc.FontSize = 8
            theDoc.Rect.Position(200, 15)
            theDoc.Rect.Right = 400
            theDoc.AddText("Page " & i.ToString)
            theDoc.TextStyle.HPos = 1
            theDoc.FontSize = 5
            theDoc.Rect.Position(400, 15)
            theDoc.Rect.Right = 600
            theDoc.AddText("FormID: " & gstring)
        Next




        '        Dim filename = "cdrApplication_" & DatePart(DateInterval.Second, Date.Now) & ".pdf"
        '        theDoc.Save(Server.MapPath("../pdf/" & filename))
        '        theDoc.Clear()
        '        Dim thufile As String = "../pdf/" & filename
        '        Dim guid As New Guid

        '        If theDoc.GetInfo(-1, "/Info") = "" Then
        Dim theID As Integer = theDoc.AddObject("<< >>")
        theDoc.SetInfo(-1, "/Info:Ref", theID.ToString())
        '        End If
        theDoc.SetInfo(-1, "/Info*/Title:Text", "Southeast Unloading Employment Application")
        theDoc.SetInfo(-1, "/Info*/Author:Text", "Diversified Logistics - Southeast Unloading")
        theDoc.SetInfo(-1, "/Info*/Subject:Text", "Employment Application")
        theDoc.SetInfo(theDoc.Root, "/Metadata:Del", "")

        Dim thuData() As Byte = theDoc.GetData()

        '        Response.Clear()
        Response.ContentType = "application/pdf"
        If Request("dld") = "yes" Then
            Response.AddHeader("Content-Disposition", "attachment; filename=" & docFile)
        Else
            Response.AddHeader("content-disposition", "inline; filename=" & docFile)
        End If
        Response.AddHeader("content-length", thuData.Length.ToString())
        Response.BinaryWrite(thuData)
        Response.End()

    End Sub

    Protected Sub doApplicantResponse(ByRef theDoc As Doc, ByVal app As EmploymentApplicationObject)
        theDoc.Page = 3
        theDoc.PageNumber = 3
        theDoc.Font = Font3
        theDoc.TextStyle.VPos = 1
        theDoc.FontSize = 11
        theDoc.Color.String = "0 0 0"
        theDoc.Rect.Position(60, 655)
        theDoc.Rect.Height = 30
        theDoc.AddText(app.LastName & ", " & app.FirstName & " " & app.MiddleInitial)

        theDoc.Rect.Position(330, 655)
        theDoc.AddText(app.Referredby)

        theDoc.Rect.Position(60, 625)
        theDoc.AddText(app.StreetAddress)
        theDoc.Rect.Position(330, 625)
        theDoc.AddText(app.City)
        theDoc.Rect.Position(425, 625)
        theDoc.AddText(app.State)
        theDoc.Rect.Position(525, 625)
        theDoc.AddText(app.Zip)
        theDoc.Rect.Position(60, 595)
        theDoc.AddText(app.PrimaryPhone)
        theDoc.Rect.Position(330, 595)
        theDoc.AddText(IIf(app.AltPhone.Length > 6, app.AltPhone, "n/a"))

        'employment
        theDoc.Rect.Position(60, 540)
        theDoc.AddText(app.DesiredPosition)
        theDoc.Rect.Position(230, 540)
        theDoc.AddText(IIf(app.DesiredStartDate > "1/1/1900", app.DesiredStartDate, "n/a"))
        theDoc.Rect.Position(430, 540)
        theDoc.AddText(app.DesiredSalary)

        theDoc.Rect.Position(60, 510)
        theDoc.AddText(IIf(app.CurrentlyEmployed, "Yes", "No"))
        theDoc.Rect.Position(230, 510)
        theDoc.AddText(IIf(app.AskCurrentEmployer, "Yes", "DO NOT CALL"))

        theDoc.Rect.Position(60, 480)
        theDoc.AddText(IIf(app.AppliedBefore, "Yes", "No"))
        theDoc.Rect.Position(230, 480)
        theDoc.AddText(app.AppliedBeforeLocation)
        theDoc.Rect.Position(430, 480)
        theDoc.AddText(IIf(app.AppliedBeforeDate > "1/1/1900", app.AppliedBeforeDate, "n/a"))

        theDoc.Rect.Height = 15
        theDoc.Rect.Position(90, 415)
        theDoc.AddText(app.School1)
        theDoc.Rect.Position(90, 405)
        theDoc.AddText(app.School1Location)
        theDoc.Rect.Height = 30
        theDoc.Rect.Position(295, 410)
        theDoc.AddText(app.School1YearsAttended)
        theDoc.Rect.Position(345, 410)
        theDoc.AddText(IIf(app.School1Graduated, "Yes", "No"))
        theDoc.Rect.Width = 185
        theDoc.TextStyle.VPos = 0.5
        theDoc.Rect.Position(390, 400)
        theDoc.AddText(app.School1SubjectsStudied)
        theDoc.TextStyle.VPos = 1
        theDoc.Rect.Right = 580

        theDoc.Rect.Height = 15
        theDoc.Rect.Position(90, 385)
        theDoc.AddText(app.School2)
        theDoc.Rect.Position(90, 375)
        theDoc.AddText(app.School2Location)
        theDoc.Rect.Position(295, 380)
        theDoc.AddText(app.School2YearsAttended)
        theDoc.Rect.Position(345, 380)
        theDoc.AddText(IIf(app.School2Graduated, "Yes", "No"))
        theDoc.Rect.Width = 185
        theDoc.TextStyle.VPos = 0.5
        theDoc.Rect.Height = 30
        theDoc.Rect.Position(390, 370)
        theDoc.AddText(app.School2SubjectsStudied)
        theDoc.TextStyle.VPos = 1
        theDoc.Rect.Right = 580

        theDoc.Rect.Height = 15
        theDoc.Rect.Position(90, 355)
        theDoc.AddText(app.School3)
        theDoc.Rect.Position(90, 345)
        theDoc.AddText(app.School3Location)
        theDoc.Rect.Height = 30
        theDoc.Rect.Position(295, 350)
        theDoc.AddText(app.School3YearsAttended)
        theDoc.Rect.Position(345, 350)
        theDoc.AddText(IIf(app.School3Graduated, "Yes", "No"))
        theDoc.Rect.Position(390, 340)
        theDoc.Rect.Width = 185
        theDoc.TextStyle.VPos = 0.5
        theDoc.AddText(app.School3SubjectsStudied)

        theDoc.TextStyle.VPos = 0.5
        theDoc.FontSize = 8
        theDoc.Rect.Position(60, 285)
        theDoc.Rect.Width = 520
        theDoc.Rect.Height = 20
        theDoc.AddText(app.SpecialSkills)
        theDoc.TextStyle.VPos = 1

        theDoc.FontSize = 9
        theDoc.Rect.Position(60, 260)
        If app.MilitaryBranch.Length > 0 Then
            theDoc.AddText(IIf(app.MilitaryBranch.Length > 0, app.MilitaryBranch, "n/a"))
            theDoc.Rect.Position(130, 260)
            theDoc.AddText(IIf(app.MilitaryServiceFromDate > "1/1/1900", app.MilitaryServiceFromDate, "n/a"))
            theDoc.Rect.Position(200, 260)
            theDoc.AddText(IIf(app.MilitaryServiceToDate > "1/1/1900", app.MilitaryServiceToDate, "n/a"))
            theDoc.Rect.Position(330, 260)
            theDoc.AddText(app.MilitaryRank)
        End If


        If app.PE1.Length > 0 Then
            theDoc.TextStyle.VPos = 1
            theDoc.Rect.Height = 15
            theDoc.Rect.Position(45, 195)
            theDoc.AddText(app.pe1ToDate)
            theDoc.Rect.Position(45, 180)
            theDoc.AddText(app.pe1FromDate)

            theDoc.Rect.Height = 30
            theDoc.FontSize = IIf(app.PE1.Length > 24, 7, 8)
            theDoc.Rect.Position(100, 195)
            theDoc.AddText(app.PE1)
            theDoc.Rect.Position(100, 185)
            theDoc.AddText(app.PE1Location)

            theDoc.TextStyle.VPos = 0.5

            theDoc.Rect.Position(248, 180)
            theDoc.Width = 65
            theDoc.AddText(app.PE1phone)

            theDoc.FontSize = 7
            theDoc.Rect.Height = 30
            theDoc.Rect.Position(320, 180)
            theDoc.Rect.Width = 55
            theDoc.AddText(app.PE1position)

            theDoc.Rect.Position(385, 180)
            theDoc.Rect.Width = 40
            theDoc.AddText(app.PE1salary)

            theDoc.Rect.Position(435, 180)
            theDoc.Rect.Width = 140
            theDoc.AddText(app.PE1reasonForLeaving)

            theDoc.FontSize = 8
        End If

        If app.PE2.Length > 0 Then
            theDoc.TextStyle.VPos = 1
            theDoc.Rect.Height = 15
            theDoc.Rect.Position(45, 165)
            theDoc.AddText(app.pe2ToDate)
            theDoc.Rect.Position(45, 150)
            theDoc.AddText(app.pe2FromDate)

            theDoc.Rect.Height = 30
            theDoc.FontSize = IIf(app.PE2.Length > 24, 7, 8)
            theDoc.Rect.Position(100, 165)
            theDoc.AddText(app.PE2)
            theDoc.Rect.Position(100, 155)
            theDoc.AddText(app.PE2Location)

            theDoc.TextStyle.VPos = 0.5

            theDoc.Rect.Position(248, 150)
            theDoc.Width = 65
            theDoc.AddText(app.PE2phone)

            theDoc.FontSize = 7
            theDoc.Rect.Height = 30
            theDoc.Rect.Position(320, 150)
            theDoc.Rect.Width = 55
            theDoc.AddText(app.PE2position)

            theDoc.Rect.Position(385, 150)
            theDoc.Rect.Width = 40
            theDoc.AddText(app.PE2salary)

            theDoc.Rect.Position(435, 150)
            theDoc.Rect.Width = 140
            theDoc.AddText(app.PE2reasonForLeaving)

            theDoc.FontSize = 8
        End If

        If app.PE3.Length > 0 Then
            theDoc.TextStyle.VPos = 1
            theDoc.Rect.Height = 15
            theDoc.Rect.Position(45, 135)
            theDoc.AddText(app.pe3ToDate)
            theDoc.Rect.Position(45, 120)
            theDoc.AddText(app.pe3FromDate)

            theDoc.Rect.Height = 30
            theDoc.FontSize = IIf(app.PE3.Length > 24, 7, 8)
            theDoc.Rect.Position(100, 135)
            theDoc.AddText(app.PE3)
            theDoc.Rect.Position(100, 125)
            theDoc.AddText(app.PE3Location)

            theDoc.TextStyle.VPos = 0.5

            theDoc.Rect.Position(248, 120)
            theDoc.Width = 65
            theDoc.AddText(app.PE3phone)

            theDoc.FontSize = 7
            theDoc.Rect.Height = 30
            theDoc.Rect.Position(320, 120)
            theDoc.Rect.Width = 55
            theDoc.AddText(app.PE3position)

            theDoc.Rect.Position(385, 120)
            theDoc.Rect.Width = 40
            theDoc.AddText(app.PE3salary)

            theDoc.Rect.Position(435, 120)
            theDoc.Rect.Width = 140
            theDoc.AddText(app.PE3reasonForLeaving)

            theDoc.FontSize = 8
        End If

        If app.PE4.Length > 0 Then
            theDoc.TextStyle.VPos = 1
            theDoc.Rect.Height = 15
            theDoc.Rect.Position(45, 105)
            theDoc.AddText(app.pe4ToDate)
            theDoc.Rect.Position(45, 90)
            theDoc.AddText(app.pe4FromDate)

            theDoc.Rect.Height = 30
            theDoc.FontSize = IIf(app.PE4.Length > 24, 7, 8)
            theDoc.Rect.Position(100, 105)
            theDoc.AddText(app.PE4)
            theDoc.Rect.Position(100, 95)
            theDoc.AddText(app.PE4Location)

            theDoc.TextStyle.VPos = 0.5

            theDoc.Rect.Position(248, 90)
            theDoc.Width = 65
            theDoc.AddText(app.PE4phone)

            theDoc.FontSize = 7
            theDoc.Rect.Height = 30
            theDoc.Rect.Position(320, 90)
            theDoc.Rect.Width = 55
            theDoc.AddText(app.PE4position)

            theDoc.Rect.Position(385, 90)
            theDoc.Rect.Width = 40
            theDoc.AddText(app.PE4salary)

            theDoc.Rect.Position(435, 90)
            theDoc.Rect.Width = 140
            theDoc.AddText(app.PE4reasonForLeaving)
        End If
        theDoc.PageNumber = theDoc.PageNumber + 1
        theDoc.FontSize = 8
        theDoc.TextStyle.VPos = 1
        theDoc.Rect.Position(60, 710)
        theDoc.AddText(app.Reference1)
        theDoc.Rect.Position(190, 710)
        theDoc.AddText(app.Reference1Contact)
        theDoc.Rect.Position(555, 710)
        theDoc.AddText(app.Reference1YrsKnown)
        theDoc.Rect.Position(60, 680)
        theDoc.AddText(app.Reference2)
        theDoc.Rect.Position(190, 680)
        theDoc.AddText(app.Reference2Contact)
        theDoc.Rect.Position(555, 680)
        theDoc.AddText(app.Reference2YrsKnown)
        theDoc.Rect.Position(60, 650)
        theDoc.AddText(app.Reference3)
        theDoc.Rect.Position(190, 650)
        theDoc.AddText(app.Reference3Contact)
        theDoc.Rect.Position(555, 650)
        theDoc.AddText(app.Reference3YrsKnown)

        theDoc.Rect.Position(70, 472)
        theDoc.AddText(app.TimeStamp)
        theDoc.Rect.Position(350, 472)
        theDoc.AddText("IP Address: " & app.ApplicantIP)






    End Sub

End Class
