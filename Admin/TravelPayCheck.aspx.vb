Imports Telerik.Web.UI


Public Class TravelPayCheck
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            ' populate Locations Combo Box (cbLocations on aspx page)
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)

            If User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager") Then
                cbLocations.Enabled = True

            End If

            LoadPayWeekSelections()


        End If


    End Sub

#Region "Grids"

    Private Sub RadGrid1_EditCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.EditCommand
        Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
        If (TypeOf e.Item Is GridEditFormItem AndAlso e.Item.IsInEditMode) Then
            Dim a As String = String.Empty
            a = "3234"

        End If

    End Sub
    Private Sub RadGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim sdate As Date = Date.Now
        Dim tpdal As New TimePuncheDAL()
        sdate = tpdal.getPayStartDate(sdate)
        Dim lid As String = cbLocations.SelectedValue
        If lid = "" Then lid = "00000000-0000-0000-0000-000000000000"
        Dim locaID As Guid = New Guid(lid)
        Dim dba As New DBAccess()
        Dim sqlString As String = "SELECT T.rtdsEmployeeID, e.FirstName, e.LastName, e.Login, Location.Name AS HomeLocaName, Location_1.Name AS TravelLocaName,  " & _
        "T.startdate, T.returndate, T.travelID, T.homeLocation as HomeLocaID, T.travelLocation as TravelLocaID " & _
        "FROM Travelers AS T INNER JOIN " & _
        "Employee AS e ON T.rtdsEmployeeID = e.ID INNER JOIN " & _
        "Location ON T.homeLocation = Location.ID INNER JOIN " & _
        "Location AS Location_1 ON T.travelLocation = Location_1.ID " & _
        "WHERE ((T.returndate > @sdate) OR (T.returndate = '1990-01-01') AND (T.loadMoney = 0))    "
        If User.IsInRole("Manager") Or cbLocations.SelectedIndex > -1 Then
            sqlString &= "AND T.travelLocation = @locaID "
        End If
        sqlString &= "ORDER BY Location_1.Name, T.returndate, e.LastName "
        dba.CommandText = sqlString
        dba.AddParameter("@sdate", sdate)
        If User.IsInRole("Manager") Or cbLocations.SelectedIndex > -1 Then
            dba.AddParameter("@locaID", locaID)
        End If

        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        Dim col As New DataColumn

        'go get week one
        col = New DataColumn("Week1AddCompID", System.Type.GetType("System.Guid"))
        dt.Columns.Add(col)

        col = New DataColumn("Week1AddCompAmount", System.Type.GetType("System.Decimal"))
        dt.Columns.Add(col)

        col = New DataColumn("Week1AddCompComments", System.Type.GetType("System.String"))
        dt.Columns.Add(col)

        col = New DataColumn("Week1UserID", System.Type.GetType("System.Guid"))
        dt.Columns.Add(col)

        col = New DataColumn("Week1UserName", System.Type.GetType("System.String"))
        dt.Columns.Add(col)

        col = New DataColumn("Week1TimeStamp", System.Type.GetType("System.DateTime"))
        dt.Columns.Add(col)


        'go get week 2
        col = New DataColumn("Week2AddCompID", System.Type.GetType("System.Guid"))
        dt.Columns.Add(col)

        col = New DataColumn("Week2AddCompAmount", System.Type.GetType("System.Decimal"))
        dt.Columns.Add(col)

        col = New DataColumn("Week2AddCompComments", System.Type.GetType("System.String"))
        dt.Columns.Add(col)

        col = New DataColumn("Week2UserID", System.Type.GetType("System.Guid"))
        dt.Columns.Add(col)

        col = New DataColumn("Week2UserName", System.Type.GetType("System.String"))
        dt.Columns.Add(col)

        col = New DataColumn("Week2TimeStamp", System.Type.GetType("System.DateTime"))
        dt.Columns.Add(col)

        Dim PayPeriodWeek1 As Date = IIf(IsDate(rbLst.SelectedValue), rbLst.SelectedValue, sdate)
        Dim pp1End As Date = DateAdd(DateInterval.Day, 6, PayPeriodWeek1)
        Dim PayPeriodWeek2 As Date = DateAdd(DateInterval.Day, 7, PayPeriodWeek1)
        Dim pp2End As Date = DateAdd(DateInterval.Day, 13, PayPeriodWeek1)
        Dim addCompdt As New DataTable
        For Each row As DataRow In dt.Rows
            Dim empID As String = row.Item("rtdsEmployeeID").ToString
            'go get week one amount and comments
            dba.CommandText = "SELECT AC.AddCompID, AC.EmployeeID, AC.AddCompAmount, ACD.CompDesc, AC.AddCompStartDate,  " & _
                "AC.AddCompEndDate, AC.AddCompComments, AC.userID, AC.TimeStamp " & _
                "FROM AdditionalComp AS AC INNER JOIN " & _
                "AddCompDesc AS ACD ON AC.AddCompDescriptionID = ACD.AddCompDescriptionID " & _
                "WHERE EmployeeID = @empID AND AddCompStartDate = @sdate and AddCompEndDate = @eDate "
            'week one
            dba.AddParameter("@empID", empID)
            dba.AddParameter("@sdate", PayPeriodWeek1)
            dba.AddParameter("@eDate", pp1End)
            addCompdt = New DataTable
            addCompdt = dba.ExecuteDataSet.Tables(0)
            If addCompdt.Rows.Count > 0 Then
                row.Item("Week1AddCompID") = IIf(Not IsDBNull(addCompdt.Rows(0).Item("AddCompID")), addCompdt.Rows(0).Item("AddCompID"), "00000000-0000-0000-0000-000000000000")
                row.Item("Week1AddCompAmount") = IIf(Not IsDBNull(addCompdt.Rows(0).Item("AddCompAmount")), addCompdt.Rows(0).Item("AddCompAmount"), 0)
                row.Item("Week1AddCompComments") = IIf(Not IsDBNull(addCompdt.Rows(0).Item("AddCompComments")), addCompdt.Rows(0).Item("AddCompComments"), "")
                row.Item("Week1UserID") = IIf(Not IsDBNull(addCompdt.Rows(0).Item("UserID")), addCompdt.Rows(0).Item("UserID"), "00000000-0000-0000-0000-000000000000")
                row.Item("Week1TimeStamp") = IIf(Not IsDBNull(addCompdt.Rows(0).Item("TimeStamp")), addCompdt.Rows(0).Item("TimeStamp"), DBNull.Value)
            Else
                row.Item("Week1AddCompID") = "00000000-0000-0000-0000-000000000000"
                row.Item("Week1AddCompAmount") = 0
                row.Item("Week1AddCompComments") = ""
                Dim udal As New userDAL
                Dim usr As ssUser = udal.getUserByName(User.Identity.Name)
                row.Item("Week1UserName") = usr.FirstName & " " & usr.LastName
                row.Item("Week1UserID") = usr.userID ' "00000000-0000-0000-0000-000000000000"
                row.Item("Week1TimeStamp") = Date.Now() ' DBNull.Value
            End If


            'go get week two amount and comments
            dba.CommandText = "SELECT AC.AddCompID, AC.EmployeeID, AC.AddCompAmount, ACD.CompDesc, AC.AddCompStartDate,  " & _
                "AC.AddCompEndDate, AC.AddCompComments, AC.userID, AC.TimeStamp " & _
                "FROM AdditionalComp AS AC INNER JOIN " & _
                "AddCompDesc AS ACD ON AC.AddCompDescriptionID = ACD.AddCompDescriptionID " & _
                "WHERE EmployeeID = @empID AND AddCompStartDate = @sdate and AddCompEndDate <= @eDate "
            'week two
            dba.AddParameter("@empID", empID)
            dba.AddParameter("@sdate", PayPeriodWeek2)
            dba.AddParameter("@eDate", pp2End)
            addCompdt = New DataTable
            addCompdt = dba.ExecuteDataSet.Tables(0)

            If addCompdt.Rows.Count > 0 Then
                row.Item("Week2AddCompID") = IIf(Not IsDBNull(addCompdt.Rows(0).Item("AddCompID")), addCompdt.Rows(0).Item("AddCompID"), "00000000-0000-0000-0000-000000000000")
                row.Item("Week2AddCompAmount") = IIf(Not IsDBNull(addCompdt.Rows(0).Item("AddCompAmount")), addCompdt.Rows(0).Item("AddCompAmount"), 0)
                row.Item("Week2AddCompComments") = IIf(Not IsDBNull(addCompdt.Rows(0).Item("AddCompComments")), addCompdt.Rows(0).Item("AddCompComments"), "")
                row.Item("Week2UserID") = IIf(Not IsDBNull(addCompdt.Rows(0).Item("UserID")), addCompdt.Rows(0).Item("UserID"), "00000000-0000-0000-0000-000000000000")


                row.Item("Week2TimeStamp") = IIf(Not IsDBNull(addCompdt.Rows(0).Item("TimeStamp")), addCompdt.Rows(0).Item("TimeStamp"), DBNull.Value)

            Else
                row.Item("Week2AddCompID") = "00000000-0000-0000-0000-000000000000"
                row.Item("Week2AddCompAmount") = 0
                row.Item("Week2AddCompComments") = ""
                Dim udal As New userDAL
                Dim usr As ssUser = udal.getUserByName(User.Identity.Name)
                row.Item("Week2UserName") = usr.FirstName & " " & usr.LastName
                row.Item("Week2UserID") = usr.userID ' "00000000-0000-0000-0000-000000000000"
                row.Item("Week2TimeStamp") = Date.Now() ' DBNull.Value
            End If
        Next

        RadGrid1.DataSource = dt

    End Sub

    Private Sub RadGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound

        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim retDate As DateTime = IIf(IsDBNull(DirectCast(e.Item.DataItem, DataRowView)("returndate")), "12/31/9999", DirectCast(e.Item.DataItem, DataRowView)("returndate"))
            retDate = FormatDateTime(retDate, DateFormat.ShortDate)
            Dim returndateLabel As Label = e.Item.FindControl("lblRetDate")
            If retDate = "12/31/9999" Or retDate = "1/1/1990" Then
                returndateLabel.Text = " - - - "
            Else
                returndateLabel.Text = Format(retDate, "dd MMM yyyy")
            End If
        End If
    End Sub



    Private Sub RadGrid1_UpdateCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.UpdateCommand
        Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
        If (TypeOf e.Item Is GridEditableItem AndAlso e.Item.IsInEditMode) Then
            Dim ppStartDate As Date = rbLst.SelectedValue
            Dim ppEndWeek1 As Date = DateAdd(DateInterval.Day, 6, ppStartDate)
            Dim editFormItem As GridEditFormItem = CType(e.Item, GridEditFormItem)
            Dim parentItem As GridDataItem = editFormItem.ParentItem
            Dim travelID As Guid = New Guid(editedItem.GetDataKeyValue("travelID").ToString)
            Dim acList As New List(Of additionalComp)
            Dim addComp As New additionalComp
            Dim tpdal As New TimePuncheDAL
            addComp.AddCompID = Nothing
            addComp.EmployeeID = New Guid(editedItem.GetDataKeyValue("rtdsEmployeeID").ToString)

            Dim startAssignment As Date = tpdal.getPayStartDate(parentItem.Item("startdate").Text)
            ' earn money week one?
            '            If startAssignment >= ppStartDate And startAssignment <= ppEndWeek1 Then
            'create additionalComp for week one
            addComp.AddCompStartDate = ppStartDate
            addComp.AddCompEndDate = ppEndWeek1
            'IIf(IsDate(parentItem.Item("returndate").Text), parentItem.Item("returndate").Text, "12/31/9999")
            Dim dba As New DBAccess()
            dba.CommandText = "Select AddCompDescriptionID from addcompdesc where CompDesc = 'TravelPay'"
            addComp.AddCompDescriptionID = dba.ExecuteScalar
            Dim numWeek1AddCompAmount As RadNumericTextBox = CType(e.Item.FindControl("numWeek1AddCompAmount"), RadNumericTextBox)
            addComp.AddCompAmount = numWeek1AddCompAmount.Value
            Dim txtWeek1AddCompComments As RadTextBox = CType(e.Item.FindControl("txtWeek1AddCompComments"), RadTextBox)
            addComp.AddCompComments = txtWeek1AddCompComments.Text
            Dim udal As New userDAL
            Dim usr As ssUser = udal.getUserByName(User.Identity.Name)
            addComp.userID = usr.userID
            addComp.TimeStamp = Date.Now
            acList.Add(addComp)
            '        End If

            'week 2
            Dim addComp2 As New additionalComp

            addComp2.AddCompID = Nothing
            addComp2.EmployeeID = New Guid(editedItem.GetDataKeyValue("rtdsEmployeeID").ToString)

            'same as week one check
            addComp2.AddCompDescriptionID = addComp.AddCompDescriptionID
            addComp2.userID = usr.userID
            addComp2.TimeStamp = Date.Now


            ' Check that start of week 2 does not exceed return date
            addComp2.AddCompStartDate = DateAdd(DateInterval.Day, 7, ppStartDate)
            addComp2.AddCompEndDate = DateAdd(DateInterval.Day, 7, ppEndWeek1)
            '            addComp.AddCompEndDate = IIf(IsDate(parentItem.Item("returndate").Text), parentItem.Item("returndate").Text, "12/31/9999")

            Dim numWeek2AddCompAmount As RadNumericTextBox = CType(e.Item.FindControl("numWeek2AddCompAmount"), RadNumericTextBox)
            addComp2.AddCompAmount = numWeek2AddCompAmount.Value
            Dim txtWeek2AddCompComments As RadTextBox = CType(e.Item.FindControl("txtWeek2AddCompComments"), RadTextBox)
            addComp2.AddCompComments = txtWeek2AddCompComments.Text

            acList.Add(addComp2)


            'save it again w/ new amounts
            Dim a As String = String.Empty


        End If
    End Sub

    Private Sub RadGrid2_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid2.NeedDataSource
        Dim holdal As New DivLogHolidays
        RadGrid2.DataSource = holdal.GetHoliday(Date.Now())

    End Sub

    Private Sub RadGrid2_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid2.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim ho As New Holiday
            ho = DirectCast(e.Item.DataItem, Holiday)
            Dim hodateLabel As Label = e.Item.FindControl("lblObservedOnDate")
            If ho.hdate = ho.hdateObserve Then
                hodateLabel.Text = " - - - "
            Else
                hodateLabel.Text = Format(ho.hdate, "dd MMM yyyy")
            End If
        End If
    End Sub


    Private Sub RadGrid3_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid3.NeedDataSource
        Dim empDAL As New empDAL()

        Dim locaID As String = IIf(cbLocations.SelectedValue = "", "00000000-0000-0000-0000-000000000000", cbLocations.SelectedValue)
        Dim dt As DataTable = empDAL.getSupervisorsByLocation(New Guid(locaID))

        Dim col As New DataColumn

        'go get week one
        col = New DataColumn("Week1")
        dt.Columns.Add(col)

        'go get week 2
        col = New DataColumn("Week2")
        dt.Columns.Add(col)



        RadGrid3.DataSource = dt
    End Sub




#End Region

#Region "buttons"


#End Region

#Region "populate controls"

    Protected Sub LoadPayWeekSelections()
        Dim tpdal As New TimePuncheDAL

        Dim csdate1 As Date = tpdal.getPayStartDate(Date.Now()) 'sdate is start of pay period
        Dim cedate1 As Date = DateAdd(DateInterval.Day, 6, csdate1)
        Dim csdate2 As Date = DateAdd(DateInterval.Day, 7, csdate1)
        Dim cedate2 As Date = DateAdd(DateInterval.Day, 6, csdate2)

        Dim sdateIZdayzFromNow As Integer = DateDiff(DateInterval.Day, csdate1, Date.Now()) 'number of days since sdate
        If sdateIZdayzFromNow < 7 Then  'we are in week 1 of current pay period
            ' do something
        Else 'we are in week 2 of current pay period
            ' do something
        End If

        Dim li As New ListItem
        Dim mo As String = MonthName(Month(csdate1), True)
        Dim da As String = Day(csdate1).ToString
        da = Utilities.getIntegerSuperScript(da)


        If Date.Now() < DateAdd(DateInterval.Day, 5, csdate1) Then 'allow prev pay period
            Dim psdate1 As Date = DateAdd(DateInterval.Day, -14, csdate1) 'sdate - 14 is start of prev pay period
            Dim pedate1 As Date = DateAdd(DateInterval.Day, 6, psdate1)
            Dim psdate2 As Date = DateAdd(DateInterval.Day, 7, psdate1)
            Dim pedate2 As Date = DateAdd(DateInterval.Day, 6, psdate2)

            li = New ListItem
            li.Text = Day(psdate1).ToString & " " & MonthName(Month(psdate1), True) & "<font size='1'> thru </font>" & Day(pedate2).ToString & " " & MonthName(Month(pedate2), True) & " <font size='1'>(Previous Pay Period)</font>"
            '            li.Text = MonthName(Month(psdate1), True) & " " & Day(psdate1).ToString & Utilities.getIntegerSuperScript(Day(psdate1).ToString) & "<font size='1'> thru </font>" & MonthName(Month(pedate2), True) & " " & Day(pedate2).ToString & Utilities.getIntegerSuperScript(Day(pedate2).ToString) & " <font size='1'>(Previous Pay Period)</font>"
            li.Value = psdate1.ToShortDateString
            rbLst.Items.Add(li)


        End If

        li = New ListItem
        li.Text = Day(csdate1).ToString & " " & MonthName(Month(csdate1), True) & "<font size='1'> thru </font>" & Day(cedate2).ToString & " " & MonthName(Month(cedate2), True) & " <font size='1'>(Current Pay Period)</font>"
        '        li.Text = MonthName(Month(csdate1), True) & " " & Day(csdate1).ToString & Utilities.getIntegerSuperScript(Day(csdate1).ToString) & "<font size='1'> thru </font>" & MonthName(Month(cedate1), True) & " " & Day(cedate2).ToString & Utilities.getIntegerSuperScript(Day(cedate2).ToString) & " <font size='1'>(Current Pay Period)</font>"
        li.Value = csdate1.ToShortDateString
        rbLst.Items.Add(li)


        Dim sday As String = WeekdayName(Weekday(tpdal.getPayStartDate(Date.Now)))
        Dim eday As String = WeekdayName(Weekday(DateAdd(DateInterval.Day, 6, tpdal.getPayStartDate(Date.Now))))

        ' lblPayWeek.Text = sday & " thru " & eday
        rbLst.SelectedIndex = 0

    End Sub

#End Region

#Region "Control Events"
    Private Sub cbLocations_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        RadGrid1.Rebind()
        RadGrid2.Rebind()
        RadGrid3.Rebind()

    End Sub

    Private Sub rbLst_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rbLst.SelectedIndexChanged
        If cbLocations.SelectedIndex > -1 Then
            '            LoadPayTypes()
            Dim dba As New DBAccess()
            dba.CommandText = "Select "
        End If
    End Sub

#End Region





End Class
