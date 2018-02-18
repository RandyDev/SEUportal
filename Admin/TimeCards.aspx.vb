Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.IO
Imports System.Drawing

Public Class TimeCards
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ' populate Locations Combo Box (cbLocations on aspx page)
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)
            Session("locaid") = cbLocations.SelectedValue.ToString

            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")

            'or
            '            cbLocations.Enabled = "You are in Fernandina"
        End If
        imgMugShot.Height = "100"
        RadGrid1.Visible = cbLocations.SelectedIndex > -1

        lblSelectLocation.Visible = Not RadGrid1.Visible
        lblSelectEmployee.Visible = RadGrid1.Visible
        'txt_Comments.Attributes.Add("onfocus", "toggleSaveBtnOn();")
        '        lbtnSaveComments.Visible = False

    End Sub

    Protected Sub fillcbLocations()
        Dim dt As New DataTable()
        Dim ldal As New locaDAL()
        dt = ldal.getLocations()
        cbLocations.DataSource = dt
        cbLocations.DataTextField = "LocationName"
        cbLocations.DataValueField = "locaID"
        cbLocations.DataBind()
        cbLocations.ClearSelection()
    End Sub

    Private Sub RadGrid1_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If e.CommandName = "RowClick" Then
            Dim empid As Guid = RadGrid1.SelectedValue
            Dim selectedItems As ArrayList
            If Session("selectedItems") Is Nothing Then
                selectedItems = New ArrayList
            Else
                selectedItems = CType(Session("selectedItems"), ArrayList)
            End If
            selectedItems.Add(empid)
            Session("selectedItems") = selectedItems
            ReloadForms(empid)
        End If
    End Sub

    Private Sub ReloadForms(ByVal empid As Guid)
        lblSelectEmployee.Visible = False
        lblSelectLocation.Visible = False
        lblttlcptime.Text = "no records found"
        lblttlpptime.Text = "no records found"
        lblCurpp.Text = ""
        lblPrepp.Text = ""
        pnlWOedit.Visible = True
        Dim empdal As New empDAL()
        Dim emp As Employee = empdal.GetEmployeeByID(empid)
        lblAddNewLinkButton.Text = "<span ID=""addNew"" runat=""server"" onmouseover=""this.style.cursor='hand';"" onclick=""openNewTimePunche('" & empid.ToString & "','" & cbLocations.SelectedValue.ToString() & "')"" >add New TimeCard</span> "
        '        lblAddComp.Text = "<span ID=""addNew"" runat=""server"" onmouseover=""this.style.cursor='hand';"" onclick=""openNewCompensation('" & empid.ToString & "','" & cbLocations.SelectedValue.ToString() & "')"" >add Compensation</span> "
        lblEmpName.Text = emp.rtdsFirstName & " " & emp.rtdsLastName
        txt_Comments.Text = emp.Comments
        lbtnSaveComments.CommandArgument = emp.ID.ToString
        '        txt_Certification.Text = emp.Certification
        If emp.PhotoJpegData.Count < 1 Then
            Dim data As Byte() = Nothing
            Dim sPath As String = Server.MapPath("~")
            sPath &= "/images/noimage.png"
            Dim fInfo As New FileInfo(sPath)
            Dim numBytes As Long = fInfo.Length
            Dim fStream As New FileStream(sPath, FileMode.Open, FileAccess.Read)
            Dim br As New BinaryReader(fStream)
            data = br.ReadBytes(CInt(numBytes))
            imgMugShot.DataValue = data
            imgMugShot.SavedImageName = "No Image"
        Else
            imgMugShot.DataValue = emp.PhotoJpegData
            imgMugShot.SavedImageName = emp.rtdsFirstName & "'s MugShot"
        End If
        txt_Comments.Text = emp.Comments
        emp.empCertifications = empdal.getEmployeeCertList(emp.ID)
        lblCerts.Text = ""
        If emp.empCertifications.Count > 0 Then
            For Each crt As Certification In emp.empCertifications
                Dim clr As String = "Black"
                If Date.Now > DateAdd(DateInterval.Year, 2, crt.certDate) Then 'over 2
                    clr = "Red"
                ElseIf Date.Now > DateAdd(DateInterval.Month, 22, crt.certDate) Then 'over 22 months
                    clr = "Orange"
                End If
                lblCerts.Text &= "<span style=""color:" & clr & ";"">" & Format(crt.certDate, "dd MMM yy") & "</span> : " & crt.certName & "<br />"
            Next
        Else
            lblCerts.Text = "no certs<br />"
        End If


        displayCurrPayPeriod(emp.ID)
        Dim tpdal As New TimePuncheDAL
        displayPrevPayPeriod(emp.ID, DateAdd(DateInterval.Day, -1, tpdal.getPayStartDate(Date.Now)).ToShortDateString)
        gridAddComp.Rebind()
    End Sub

    Public Sub displayCurrPayPeriod(ByVal empid As Guid)
        Dim tpDAL As New TimePuncheDAL()
        Dim ppDate As Date = FormatDateTime(Date.Now(), DateFormat.ShortDate)
        Dim curTimeCard As List(Of TimePunche) = tpDAL.getTimePunchesByEmpIDandPayPeriod(empid, ppDate)
        Dim pps As Date = tpDAL.getPayStartDate(ppDate)
        Dim ttlpptime As String = String.Empty
        lblcwk.Text = Format(pps, "MMM dd ") & " <font style='font-weight:normal;' size='1'>thru</font> " & Format(DateAdd(DateInterval.Day, 13, pps), "MMM dd")
        If curTimeCard.Count > 0 Then
            Dim ctbl As String = BuildTimePunche(curTimeCard, ttlpptime)
            lblttlcptime.Text = ttlpptime
            lblCurpp.Text = ctbl
        End If
    End Sub

    Public Sub displayPrevPayPeriod(ByVal empid As Guid, ByVal sDate As Date)
        Dim tpDAL As New TimePuncheDAL()
        Dim pvDate As Date = tpDAL.getPayStartDate(sDate)
        Dim prevTimeCard As List(Of TimePunche) = tpDAL.getTimePunchesByEmpIDandPayPeriod(empid, pvDate)
        Dim ttlpptime As String = String.Empty
        lblpwk.Text = Format(pvDate, "MMM dd") & " <font style='font-weight:normal;' size='1'>thru</font> " & Format(DateAdd(DateInterval.Day, 13, pvDate), "MMM dd")
        If prevTimeCard.Count > 0 Then
            Dim ctbl As String = BuildTimePunche(prevTimeCard, ttlpptime)
            lblttlpptime.Text = ttlpptime
            lblPrepp.Text = ctbl
        End If
    End Sub

    Public Function BuildTimePunche(ByVal tpList As List(Of TimePunche), ByRef ttlpptime As String) As String
        Dim dp As String = String.Empty
        Dim ttlMins1 As Integer = 0
        Dim ttlMins2 As Integer = 0
        Dim lastDayWorked As Date = Nothing
        Dim tpdal As New TimePuncheDAL
        For Each tp As TimePunche In tpList

            'This filters out timepunches that have an empty tio list ????
            If Not tp.tpList Is Nothing Then
                'set color based on isclosed flag
                Dim isclosed As String = IIf(tp.IsClosed, "#aaaaaa", "orange")

                'Begin td cell for a single time punch (tp)
                If tp.LocationID.ToString = cbLocations.SelectedValue.ToString Or Not Utilities.IsValidGuid(tp.LocationID.ToString) Or User.IsInRole("Administrator") Or User.IsInRole("SysOp") Then
                    dp &= "<td valign=""top"" style=""width:75px;padding-left:5px;"">" & "<span title=""Click to edit this TimeCard"" onmouseover=""this.style.cursor='pointer'"" onclick=""openTimePunche('" & tp.ID.ToString & "')"">" & Format(tp.DateWorked, "ddd dd MMM") & " <br><span style='font-size:12px;color:" & isclosed & ";'>" & tp.DepartmentName & "</span></span><br />"
                Else
                    dp &= "<td valign=""top"" style=""width:75px;padding-left:5px;"">" & "<span title=""Contact HR or IT for edit"" >" & Format(tp.DateWorked, "ddd dd MMM") & " <br><span style='font-size:12px;color:" & isclosed & ";'>" & tp.DepartmentName & "</span></span><br />"
                End If
                Dim surrogate As Date = "1/1/1900"

                For Each tio As TimeInOut In tp.tpList
                    Dim tioHours As String = String.Empty
                    Dim ishrColor As String = String.Empty
                    If tio.isHourly Then
                        ishrColor = "#A54242"
                    Else
                        ishrColor = "Black"
                    End If
                    dp &= "<span style=""color:" & ishrColor & ";"">" & Format(tio.TimeIn, "hh:mm tt") & "</span> <br>"
                    Dim dd As Integer = 0
                    Dim sTimeIn As DateTime = FormatDateTime(tio.TimeIn, DateFormat.ShortDate) & " " & FormatDateTime(tio.TimeIn, DateFormat.ShortTime)
                    Dim sTimeOut As DateTime = FormatDateTime(tio.TimeOut, DateFormat.ShortDate) & " " & FormatDateTime(tio.TimeOut, DateFormat.ShortTime)
                    If sTimeOut > surrogate Then
                        dp &= "<span style=""color:" & ishrColor & ";"">" & Format(sTimeOut, "hh:mm tt") & "</span>"
                    Else
                        dp &= "<span style=""color:blue;font-size:11px;"" onmouseover=""this.style.cursor='pointer'"" onclick=""openTimePunche('" & tp.ID.ToString & "')""> clock out </span>"
                        sTimeOut = Date.Now()
                    End If
                    dd = DateDiff(DateInterval.Minute, sTimeIn, sTimeOut)

                    If tp.DateWorked < DateAdd(DateInterval.Day, 7, tpdal.getPayStartDate(tp.DateWorked)) Then
                        'week one
                        ttlMins1 += dd
                    Else
                        'week2
                        ttlMins2 += dd
                    End If


                    Dim numModMins As Integer = dd Mod 60
                    Dim numHours As Integer = (dd - numModMins) / 60
                    Dim ddmins As String = ""
                    If numModMins < 10 Then
                        ddmins = "0" & numModMins.ToString
                    Else
                        ddmins = numModMins.ToString
                    End If
                    If ddmins.Length = 1 Then ddmins = "0" & ddmins
                    Dim numHoursColor As String = "#000000"
                    Select Case numHours
                        Case Is > 5
                            numHoursColor = "Red"
                        Case Is > 4
                            numHoursColor = "Orange"
                    End Select
                    dp &= "<span style=""font-size:10px;""><center><span style=""color:" & numHoursColor & ";"">[</span>" & numHours.ToString & ":" & ddmins & "<span style=""color:" & numHoursColor & ";"">]</span></center></span>"

                Next
                If tp.LocationID.ToString <> cbLocations.SelectedValue.ToString Then
                    dp &= tp.LocationName
                End If
                dp &= "</td>"
                ' End td cell for single time punche

                lastDayWorked = tp.DateWorked

            End If
        Next

        If ttlMins2 > 0 Then    'we just added time to week 2 of pay period
            ttlpptime = "This Week: "
            'subtract mod mins from total mins to get integer hours worked
            Dim ttlhrs2 As String = CType((ttlMins2 - ttlMins2 Mod 60) / 60, Integer).ToString.Trim()
            'store mod mins to string
            Dim ttlmodmins2 As String = (ttlMins2 Mod 60).ToString.Trim()
            If ttlmodmins2.Length = 1 Then ttlmodmins2 = "0" & ttlmodmins2
            ttlpptime &= ttlhrs2 & "hr&nbsp;" & ttlmodmins2 & "min  - "
        End If


        'subtract mod mins from total week one mins to get integer hours worked
        Dim ttlhrs1 As String = CType((ttlMins1 - ttlMins1 Mod 60) / 60, Integer).ToString.Trim()
        'store mod mins to string
        Dim ttlmodmins1 As String = (ttlMins1 Mod 60).ToString.Trim()
        If ttlmodmins1.Length = 1 Then ttlmodmins1 = "0" & ttlmodmins1


        'check to see if 'today' is in this week or last week
        If Date.Now < DateAdd(DateInterval.Day, 7, tpdal.getPayStartDate(lastDayWorked)) Then
            ttlpptime = "This Week: "
        Else
            ttlpptime &= "Last Week: "
        End If
        ttlpptime &= ttlhrs1 & "hr&nbsp;" & ttlmodmins1 & "min "

        If lastDayWorked < tpdal.getPayStartDate(Date.Now()) Then
            Dim ttlmins As Integer = ttlMins1 + ttlMins2
            Dim ttlhrs As String = CType((ttlmins - ttlmins Mod 60) / 60, Integer).ToString.Trim()
            Dim ttlmodmins As String = (ttlmins Mod 60).ToString.Trim()
            If ttlmodmins.Length = 1 Then ttlmodmins = "0" & ttlmodmins
            ttlpptime = ttlhrs & "hr&nbsp;" & ttlmodmins & "min "
        End If


                Return dp
    End Function

    Private Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim dba As New DBAccess()
        Dim datenow As Date = FormatDateTime(Date.Now(), DateFormat.ShortDate)
        dba.CommandText = "SELECT E.ID AS empID, E.LastName + ', ' + E.FirstName + '  (' + E.Login + ')' AS EmpName " & _
            "FROM Employee AS E INNER JOIN " & _
            "Employment ON E.ID = Employment.EmployeeID " & _
            "WHERE (E.LocationID = @locaID) AND (Employment.PayType <> '3') " & _
            "AND (Employment.DateOfDismiss > @datenow) " & _
            "ORDER BY E.LastName "
        dba.AddParameter("@locaID", cbLocations.SelectedValue)
        dba.AddParameter("@datenow", datenow)
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        dt.Columns.Add("Department", Type.GetType("System.String"))
        For Each rw As DataRow In dt.Rows
            Dim tpdal As New TimePuncheDAL()
            Dim empid As Guid = dt.Rows(0).Item(0)
            Dim tpl As List(Of TimePunche) = tpdal.getMostRecentTimePunchByEmpID(rw("empID"))
            If tpl.Count > 0 Then
                Dim tp As TimePunche = tpl(0)

                If tp.tpList Is Nothing Then
                    rw.Item("Department") = "<font color=""#cccccc"">" & tp.DepartmentName & "</font>"
                Else
                    If tp.tpList(tp.tpList.Count - 1).TimeOut = "1/1/1900" Then
                        Dim isHrly As String = String.Empty
                        If tp.tpList(tp.tpList.Count - 1).isHourly Then isHrly = "*"
                        rw.Item("Department") = isHrly & "<font color=""Green"">" & tp.DepartmentName & "</font>"
                    ElseIf Not tp.IsClosed Then
                    rw.Item("Department") = "<font color=""Orange"">" & tp.DepartmentName & "</font>"
                    Else
                    rw.Item("Department") = "<font color=""#cccccc"">" & tp.DepartmentName & "</font>"
                End If
                End If
            End If
        Next

        RadGrid1.DataSource = dt

        Dim em As New Employee
    End Sub

    Protected Sub RadGrid1_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles RadGrid1.PreRender
        If Not (Session("selectedItems") Is Nothing) Then
            Dim selectedItems As ArrayList = CType(Session("selectedItems"), ArrayList)
            Dim stackIndex As Int16
            For stackIndex = 0 To selectedItems.Count - 1
                Dim curItem As String = selectedItems(stackIndex).ToString
                For Each item As GridItem In RadGrid1.MasterTableView.Items
                    If TypeOf item Is GridDataItem Then
                        Dim dataItem As GridDataItem = CType(item, GridDataItem)
                        If curItem.Equals(dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("empID").ToString()) Then
                            dataItem.Selected = True
                        End If
                    End If
                Next
            Next
        End If
    End Sub
    Protected Sub cbLocations_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        If cbLocations.SelectedIndex < 0 Then
            RadGrid1.Visible = False
            lblSelectLocation.Visible = True
            lblSelectEmployee.Visible = False
        Else
            Session("locaid") = cbLocations.SelectedValue.ToString
            RadGrid1.Rebind()
            lblSelectLocation.Visible = False
            lblSelectEmployee.Visible = True
        End If
        pnlWOedit.Visible = False
    End Sub

    Private Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        Dim arg As String = e.Argument
        Dim sarg() As String = Split(arg, ":")
        If arg.Contains("TimePunche") Then

            Dim empid As Guid = RadGrid1.SelectedValue
            Dim selectedItems As ArrayList
            ' the grid is NOT multi select so I'm just turnin' the array back to a single item (currently selected employee)
            '            If Session("selectedItems") Is Nothing Then
            selectedItems = New ArrayList
            '        Else
            '            selectedItems = CType(Session("selectedItems"), ArrayList)
            '        End If
            selectedItems.Add(empid)
            Session("selectedItems") = selectedItems
            ReloadForms(empid)
            RadGrid1.Rebind()

        ElseIf arg.Contains("Compensation") Then
            'Dim empid As Guid = RadGrid1.SelectedValue
            'Dim selectedItems As ArrayList
            'If Session("selectedItems") Is Nothing Then
            '    selectedItems = New ArrayList
            'Else
            '    selectedItems = CType(Session("selectedItems"), ArrayList)
            'End If
            'selectedItems.Add(empid)
            'Session("selectedItems") = selectedItems


        Else
            ReloadForms(RadGrid1.SelectedValue)
            RadGrid1.Rebind()
        End If

    End Sub

    Private Sub lbtnSaveComments_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnSaveComments.Click
        Dim txtcomments As String = txt_Comments.Text.Trim
        Dim edal As New empDAL()

        Dim msg As String = edal.updateEmployeeComments(RadGrid1.SelectedValue, txtcomments)

        If msg.Length > 1 Then
            'DISPLAY ERROR MESSAGE
        Else
            'OK
        End If
        'lbtnSaveComments.CssClass = "hideme"
        '        lbtnSaveComments.Visible = False
    End Sub



    Private Sub gridAddComp_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gridAddComp.NeedDataSource
        Dim selectedItems As ArrayList = CType(Session("selectedItems"), ArrayList)
        Dim empID As Guid = New Guid(selectedItems(selectedItems.Count - 1).ToString)
        Dim tpdal As New TimePuncheDAL()
        Dim startDate As Date = tpdal.getPayStartDate(Date.Now())
        Dim endDate As Date = DateAdd(DateInterval.Day, 13, startDate)

        Dim dba As New DBAccess
        dba.CommandText = "SELECT AdditionalComp.AddCompID, AdditionalComp.EmployeeID,  " & _
            "AdditionalComp.AddCompStartDate, AdditionalComp.AddCompEndDate,  " & _
            "AdditionalComp.AddCompAmount, AdditionalComp.AddCompComments,  " & _
            "AdditionalComp.userID, AdditionalComp.TimeStamp,  " & _
            "AddCompDesc.CompDesc, AddCompDesc.Credit,  " & _
            "AddCompDesc.InActive FROM AdditionalComp INNER JOIN  " & _
            "AddCompDesc ON AdditionalComp.AddCompDescriptionID = AddCompDesc.AddCompDescriptionID  " & _
            "WHERE AdditionalComp.EmployeeID = @empID  " & _
            "and AddCompStartDate >= @startdate  " & _
            "and AddCompEndDate <= @enddate "
        dba.AddParameter("@empID", empID)
        dba.AddParameter("@startdate", startDate)
        dba.AddParameter("@enddate", endDate)

        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        gridAddComp.DataSource = dt
    End Sub
End Class