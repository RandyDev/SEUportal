Imports Telerik.Web.UI

Public Class homepg
    Inherits System.Web.UI.Page

    Private Sub homepg_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Dim usrdal As New userDAL
        Dim usr As ssUser = usrdal.getUserByName(User.Identity.Name)
        lblFirstLast.Text = usr.FirstName & " " & usr.LastName
        
        If lblFirstLast.Text = "William Walklett" Then
            MoveImports()
        End If
        If User.IsInRole("Clerk") And (User.IsInRole("Supervisor") Or User.IsInRole("Employee")) Then
            'you need to be clocked in
            Dim utl As New Utilities
            If utl.isOnClock(usr.rtdsEmployeeID.ToString) Then
                btnLoadEditor.RenderMode = RenderMode.Lightweight
                btnLoadEditor.Visible = True
            End If
        ElseIf User.IsInRole("Clerk") Then
            btnLoadEditor.Visible = True
        End If
        If User.IsInRole("Manager") Then
            tblCertReport.Visible = True
            Dim edal As New empDAL()
            Dim loca As String = String.Empty
            Dim ldal As New locaDAL
            Dim mUser As MembershipUser = Membership.GetUser()
            Dim locadt As DataTable = ldal.getUserLocaList(mUser.ProviderUserKey)
            'Create linkbuttons
            Dim wlbl As Label
            Dim elbl As Label
            Dim localbl As Label
            Dim lb As LinkButton
            Dim row As TableRow
            Dim cell As TableCell
            row = New TableRow
            cell = New TableCell
            cell.ColumnSpan = 4
            wlbl = New Label
            wlbl.Text = "Employee Equipment Certifications"
            wlbl.CssClass = "CertHeaderText"
            cell.CssClass = "CertHeaderCell"
            cell.Controls.Add(wlbl)
            row.Cells.Add(cell)
            tblCertReport.Rows.Add(row)
            For Each rw As DataRow In locadt.Rows()
                Dim locaName As String = rw.Item("LocationName")
                Dim dtCertWarnings As DataTable = edal.CertificationWarnings(locaName)
                Dim warnCount As String = dtCertWarnings.Rows(0).Item("warning").ToString()
                Dim expCount As String = dtCertWarnings.Rows(0).Item("expired").ToString()
                If warnCount = "0" And expCount = "0" Then
                    tblCertReport.Visible = False
                    Exit For
                End If
                wlbl = New Label
                wlbl.Text = "Expiring: " & warnCount
                elbl = New Label
                elbl.Text = "Expired Certifcates: " & expCount
                localbl = New Label
                localbl.Text = locaName
                lb = New LinkButton
                lb.CommandName = "lbShowMe"
                lb.CommandArgument = locaName
                lb.Text = "Show List"
                AddHandler lb.Command, AddressOf showCertList
                row = New TableRow

                cell = New TableCell
                localbl.CssClass = "loca"
                cell.Controls.Add(localbl)
                row.Cells.Add(cell)

                cell = New TableCell
                elbl.CssClass = "expired"
                cell.Controls.Add(elbl)
                row.Cells.Add(cell)

                cell = New TableCell
                wlbl.CssClass = "expiring"
                cell.Controls.Add(wlbl)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.CssClass = "lnkbtn"
                cell.Controls.Add(lb)
                row.Cells.Add(cell)

                tblCertReport.Rows.Add(row)
            Next


            'lblCertCount.Text += "<br/>" & Session("CertLocationName") & ": Warning: " & warnCount & " &nbsp; Expired: " & expCount
            'lblCertCount.Visible = True


        End If


    End Sub

    Private Sub showCertList(ByVal sender As System.Object, ByVal e As CommandEventArgs)
        Dim lb As LinkButton = CType(sender, LinkButton)
        Dim arg As String = e.CommandArgument
        Session("CertLocationName") = arg
        gridCertifications.Visible = True
        gridCertifications.Rebind()

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim usrdal As New userDAL
        Dim usr As ssUser = usrdal.getUserByName(User.Identity.Name)

        If usr.PasswordQuestion = "na" Then
            '   dont think I need to test for Nothing any longer
            'if LastPasswordChangedDate < LastLoginDate  then this is reset
            pnlUpdateProfile.Visible = True
            lblName.Text = usr.FirstName
        ElseIf usr.CreationDate = usr.LastPasswordChangedDate Then
            lblChangePassword.Visible = True
            lblChangePassword.ForeColor = Drawing.Color.Firebrick
            lblChangePassword.Text = "It appears you are using a default password.<br />Please click Tools - Edit Profile and reset your password."
        End If
        Dim tpdal As New TimePuncheDAL()
        Dim startDate As Date = tpdal.getPayStartDate(Date.Now())
        Dim endDate As Date = DateAdd(DateInterval.Day, 13, startDate)
        lblPayPeriod.Text = "Current Pay-Period: <br />" & Format(startDate, "ddd: dd MMM yy") & "<font size='1'><em> thru </em></font>" & Format(endDate, "ddd: dd MMM yy")

        If User.IsInRole("Manager") Then

        End If


    End Sub

    Public Sub MoveImports()
        Dim dba As New DBAccess()
        Dim dt As New DataTable()
        '        Dim loca As Guid
        Dim locaName As String = String.Empty


        'dba.CommandText = "Select DISTINCT(Location.Name), WorkOrder.LocationID " & _
        '    "FROM WorkOrder INNER JOIN " & _
        '    "Location ON WorkOrder.LocationID = Location.ID " & _
        '            "WHERE WorkOrder.StartTime > '4/1/2012 12:00:00 AM' order by Location.Name"

        ''                    "WHERE WorkOrder.StartTime = '1/1/1900 12:00:00 AM' and WorkOrder.Status=9 order by Location.Name"
        'dt = dba.ExecuteDataSet.Tables(0)
        'Dim btn As RadButton
        'If dt.Rows.Count > 0 Then
        '    Dim table As Table
        '    Dim tablerow As TableRow
        '    Dim tablecell As TableCell
        '    table = New Table()
        '    Dim i As Integer = 1
        '    For Each lrow As DataRow In dt.Rows
        '        loca = lrow.Item("LocationID")
        '        locaName = lrow.Item("Name")
        '        '                dba.CommandText = "SELECT distinct(WorkOrder.DepartmentID), Department.Name " & _
        '        '            "FROM WorkOrder INNER JOIN " & _
        '        '            "Department ON WorkOrder.DepartmentID = Department.ID " & _
        '        '            "WHERE WorkOrder.StartTime = '1/1/1900 12:00:00 AM' AND LocationID = @LocationID AND (CreatedBy LIKE 'Imported:%')"
        '        dba.CommandText = "Select distinct(WorkOrder.DepartmentID), Department.Name From Workorder inner join department on workorder.departmentid = department.id order by department.name"
        '        dba.AddParameter("@LocationID", loca)
        '        Dim dtdepartments As DataTable = dba.ExecuteDataSet.Tables(0)
        '        For Each row As DataRow In dtdepartments.Rows()
        '            If i < 2 Then

        '                '                    tablerow = New TableRow
        '                '                    tablecell = New TableCell
        '                'Dim lbl As New Label
        '                'lbl.Text = locaName
        '                ''                    tablecell.Controls.Add(lbl)
        '                ''                    tablerow.Cells.Add(tablecell)
        '                'form1.Controls.Add(lbl)
        '                ''                    tablecell = New TableCell
        '                'lbl = New Label
        '                'lbl.Text = row.Item("Name")
        '                ''                    tablecell.Controls.Add(lbl)
        '                ''                    tablerow.Cells.Add(tablecell)
        '                'form1.Controls.Add(lbl)

        '                ''                    tablecell = New TableCell

        '                btn = New RadButton
        '                Dim hcBreak As HtmlControl = New HtmlGenericControl("br")
        '                btn.Text = "Clear Loads for: " & locaName & " : " & row.Item("Name")
        '                btn.ID = locaName & ":" & row.Item("Name") & ":" & i.ToString
        '                AddHandler btn.Click, AddressOf btn_Click
        '                btn.CommandArgument = loca.ToString() & ":" & row.Item("DepartmentID").ToString()
        '                btn.CommandName = locaName & ":" & row.Item("Name")
        '                form1.Controls.Add(btn)
        '                form1.Controls.Add(hcBreak)
        '                'tablecell.Controls.Add(btn)
        '                '                    tablerow.Cells.Add(tablecell)

        '                '                    table.Rows.Add(tablerow)
        '                i = i + 1
        '            End If
        '       Next
        '    Next
        '    '            form1.Controls.Add(table)
        '    For i = 1 To 5
        '        btn = New RadButton
        '        Dim hcBreak As HtmlControl = New HtmlGenericControl("br")
        '        btn.Text = "Mash Me - " & i.ToString
        '        btn.ID = "radButton" & i.ToString
        '        AddHandler btn.Click, AddressOf btn_Click
        '        btn.CommandArgument = "button" & i.ToString
        '        btn.CommandName = "ThisIsTheDeleteCommand" & i.ToString
        '        form1.Controls.Add(btn)
        '        form1.Controls.Add(hcBreak)
        '    Next
        'End If

        dba.CommandText = "SELECT Count(ID) FROM dbo.WorkOrder  " & _
            "WHERE (CarrierID = '00000000-0000-0000-0000-000000000000') AND  " & _
            "(LoadTypeID = '00000000-0000-0000-0000-000000000000') AND  " & _
            "(LoadDescriptionID = '00000000-0000-0000-0000-000000000000') AND  " & _
            "(Amount = 0) AND  " & _
            "(TruckNumber = '') AND  " & _
            "(TrailerNUmber = '') AND  " & _
            "(LogDate > '1/1/2012') AND  " & _
            "((Status = 137) OR (Status = 192))"
        Dim countem As Integer = dba.ExecuteScalar
        btnMoveImports.Visible = countem > 0


    End Sub

    Private Sub btn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As RadButton
        btn = CType(sender, RadButton)
        Dim str As String = btn.Text
        Dim cmd As String = btn.CommandName
        Dim arg As String = btn.CommandArgument
    End Sub

    Private Sub btnSubmitProfile_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnSubmitProfile.Command
        Dim udal As New userDAL
        Dim usr As ssUser = udal.getUserByName(User.Identity.Name)
        usr.PasswordQuestion = RadComboBox1.Text
        usr.PasswordAnswer = txt_Response.Text.Trim()
        '        udal.UpdatecUser(usr)
        udal.UpdateSecurityQuestion(usr)
        pnlUpdateProfile.Visible = False
        If usr.CreationDate = usr.LastPasswordChangedDate Then
            lblChangePassword.Visible = True
            lblChangePassword.ForeColor = Drawing.Color.Firebrick
            lblChangePassword.Text = "It appears you are using a default password.<br />Please click Tools - Edit Profile and reset your password."
        End If
    End Sub

    Private Sub gridCertifications_ColumnCreated(sender As Object, e As Telerik.Web.UI.GridColumnCreatedEventArgs) Handles gridCertifications.ColumnCreated
        If TypeOf (e.Column) Is GridGroupSplitterColumn Then
            e.Column.Visible = False
        End If
    End Sub

    Private Sub gridCertifications_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gridCertifications.ItemCreated
        If TypeOf (e.Item) Is GridGroupHeaderItem Then
            e.Item.Cells(0).Controls.Clear()
            e.Item.Cells(0).Visible = False
        End If
    End Sub


    Private Sub gridCertifications_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gridCertifications.ItemDataBound
        If (TypeOf (e.Item) Is GridDataItem) Then
            'Get the instance of the right type
            Dim databoundItem As GridDataItem = e.Item

            If IsDate(databoundItem("Issued").Text) Then
                Dim idate As Date
                idate = databoundItem("Issued").Text
                Dim exDate As Date = DateAdd(DateInterval.Year, 2, idate)
                Dim warnDate = DateAdd(DateInterval.Month, -2, exDate)
                databoundItem("Issued").ForeColor = Drawing.Color.Black
                If warnDate <= Date.Now Then databoundItem("Issued").ForeColor = Drawing.Color.Orange
                If exDate <= Date.Now Then databoundItem("Issued").ForeColor = Drawing.Color.Red
            End If
        End If
    End Sub

    Private Sub gridCertifications_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gridCertifications.NeedDataSource
        Dim loca As String = String.Empty
        loca = Session("CertLocationName")
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT dbo.Location.Name AS Location, dbo.Employment.JobTitle, dbo.Employee.Login AS [Emp Number], dbo.Employee.FirstName,  " & _
            "dbo.Employee.LastName, dbo.CertificationType.Name AS Certification, CONVERT(varchar(10), dbo.Certification.Date, 101) AS Issued, " & _
            " dbo.Employee.LastName + ', ' + dbo.Employee.FirstName AS eName " & _
            "FROM dbo.CertificationType INNER JOIN " & _
            "dbo.Certification ON dbo.CertificationType.ID = dbo.Certification.TypeID RIGHT OUTER JOIN " & _
            "dbo.Employee LEFT OUTER JOIN " & _
            "dbo.Employment ON dbo.Employee.ID = dbo.Employment.EmployeeID ON dbo.Certification.EmployeeID = dbo.Employee.ID LEFT OUTER JOIN " & _
            "dbo.Location ON dbo.Employee.LocationID = dbo.Location.ID " & _
            "WHERE (dbo.Employment.DateOfDismiss > { fn NOW() }) AND  " & _
            "DATEADD(MONTH,22,dbo.Certification.Date) < GETDATE() AND " & _
            "(NOT (dbo.Employment.JobTitle = 'administrator') AND  " & _
            "NOT (dbo.Employment.JobTitle = 'Manager')) AND  " & _
            "(dbo.Location.Name IN (@location)) "
        dba.AddParameter("@location", loca)
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        gridCertifications.DataSource = dt
        gridCertifications.GroupingSettings.ShowUnGroupButton = False



    End Sub

    Private Sub btnLoadEditor_Click(sender As Object, e As EventArgs) Handles btnLoadEditor.Click
        Response.Redirect("~/admin/LoadEditorDataEntry.aspx")
    End Sub

End Class


