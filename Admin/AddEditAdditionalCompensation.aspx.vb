Imports Telerik.Web.UI
Imports System.IO

Public Class AddEditAdditionalCompensation
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session("selectedItems") = Nothing
            ' populate Locations Combo Box (cbLocations on aspx page)
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            Dim isFern As Boolean = User.IsInRole("Administrator") Or User.IsInRole("SysOp")
            ldal.setLocaCombo(puser, cbLocations, isFern)
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
            LoadPayWeekSelections()
        End If
        imgMugShot.Height = "100"
        RadGrid1.Visible = cbLocations.SelectedIndex > -1
        pnlTitle.Visible = Not RadGrid1.Visible
        lblSelectLocation.Visible = Not RadGrid1.Visible
        lblSelectEmployee.Visible = (Not pnlWOedit.Visible) And RadGrid1.Visible
        'txt_Comments.Attributes.Add("onfocus", "toggleSaveBtnOn();")
        '        lbtnSaveComments.Visible = False

    End Sub

#Region "Grids"

    Private Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim locaID As Guid = New Guid(cbLocations.SelectedValue)
        Dim sDate As Date = rbLst.SelectedValue ' "11/1/2012"
        Dim ds As DataSet = getOtherPayDataSet(locaID, sDate)
        RadGrid1.DataSource = ds.Tables(0)

    End Sub

    Protected Function getOtherPayDataSet(ByVal locaID As Guid, ByVal sDate As Date) As DataSet
        Dim ds As New DataSet
        Dim sqlStr As String = String.Empty
        Dim eDate As Date = DateAdd(DateInterval.Day, 13, sDate)
        sqlStr = "DECLARE @tblEmpTable TABLE (ID  Uniqueidentifier, Emp  VarChar(50), CurrentLoca  Uniqueidentifier, HomeLoca  Uniqueidentifier, JobTitle  VarChar(50)) " & _
            "INSERT INTO @tblEmpTable (ID, Emp, CurrentLoca, HomeLoca, JobTitle) " & _
            "SELECT     dbo.Employee.ID, dbo.Employee.FirstName + ' ' + dbo.Employee.LastName + ' :  ' + dbo.Employee.Login AS Emp, " & _
            "dbo.Employee.LocationID AS CurrentLoca, dbo.Employee.HomeLocationID AS HomeLoca, dbo.Employment.JobTitle " & _
            "FROM dbo.Employee INNER JOIN " & _
            "dbo.Location ON dbo.Employee.LocationID = dbo.Location.ID INNER JOIN " & _
            "dbo.Employment ON dbo.Employee.ID = dbo.Employment.EmployeeID " & _
            "WHERE (dbo.Location.ID = @locaID) AND (dbo.Employment.DateOfDismiss > CONVERT(DATETIME, '9999-12-30 00:00:00', 102)) "
        '-------------------------------------------------------------------------------------------------Emps
        sqlStr &= "DECLARE @tbAddComp TABLE (ID  Uniqueidentifier, AddCompStartDate  Datetime, AddCompEndDate  Datetime, AddCompAmount  Money, " & _
            "AddCompComments  Varchar(MAX), CompDesc  VarChar(50), Credit  Bit, OAPlus  Money, OAMinus  Money, OtherAmt  Money) " & _
            "INSERT INTO @tbAddComp (ID, AddCompStartDate, AddCompEndDate, AddCompAmount, AddCompComments, CompDesc, Credit, OAPlus, OAMinus, OtherAmt) " & _
            "SELECT    dbo.Employee.ID, dbo.AdditionalComp.AddCompStartDate, dbo.AdditionalComp.AddCompEndDate, dbo.AdditionalComp.AddCompAmount,  " & _
            "dbo.AdditionalComp.AddCompComments, dbo.AddCompDesc.CompDesc, dbo.AddCompDesc.Credit, " & _
            "CASE " & _
            "WHEN Credit = 1  " & _
            "THEN AddCompAmount " & _
            "ELSE 0 " & _
            "END AS OAPlus,  " & _
            "CASE " & _
            "WHEN Credit=0 " & _
            "THEN AddCompAmount - AddCompAmount*2 " & _
            "ELSE 0 " & _
            "END AS OAMinus,           " & _
            "CASE " & _
            "WHEN Credit = 0 " & _
            "THEN AddCompAmount - AddCompAmount*2 " & _
            "ELSE AddCompAmount " & _
            "END AS OtherAmt " & _
            "FROM dbo.AddCompDesc INNER JOIN " & _
            "dbo.AdditionalComp ON dbo.AddCompDesc.AddCompDescriptionID = dbo.AdditionalComp.AddCompDescriptionID INNER JOIN " & _
            "dbo.Employee ON dbo.AdditionalComp.EmployeeID = dbo.Employee.ID " & _
            "WHERE (dbo.AdditionalComp.AddCompStartDate >= @sDate) AND (dbo.AdditionalComp.AddCompEndDate <= @eDate) "

        sqlStr &= "select tblEmpTable.ID, Emp, JobTitle, CurrentLoca, HomeLoca,  " & _
            "CASE " & _
            "WHEN SUM(OAPlus) IS NULL " & _
            "THEN 0 " & _
            "ELSE SUM(OAPlus) " & _
            "END AS OAPlus, " & _
            "CASE " & _
            "WHEN SUM(OAMinus) IS NULL " & _
            "THEN 0 " & _
            "ELSE SUM(OAMinus) " & _
            "END AS OAMinus " & _
            "FROM @tblEmpTable AS tblEmpTable " & _
            "LEFT OUTER JOIN @tbAddComp AS tbAddComp " & _
            "ON tblEmpTable.ID = tbAddComp.ID " & _
            "GROUP BY tblEmpTable.ID,Emp,JobTitle,CurrentLoca, HomeLoca " & _
            "Order By Emp ASC "
        '------------------------------------------------------------------------------------------------------SUMS
        Dim dba As New DBAccess
        dba.CommandText = sqlStr
        dba.AddParameter("@locaID", locaID)
        dba.AddParameter("@sDate", sDate)
        dba.AddParameter("@eDate", eDate)
        Try
            ds = dba.ExecuteDataSet

        Catch ex As Exception

        End Try

        Return ds

    End Function

    Private Sub RadGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim dataItem As GridDataItem = e.Item
            Dim jobTitle As String = DirectCast(e.Item.DataItem, DataRowView)("JobTitle")
            Dim HomeLoca As Guid = IIf(IsDBNull(DirectCast(e.Item.DataItem, DataRowView)("HomeLoca")), New Guid("00000000-0000-0000-0000-000000000000"), DirectCast(e.Item.DataItem, DataRowView)("HomeLoca"))
            Dim CurrentLoca As Guid = DirectCast(e.Item.DataItem, DataRowView)("CurrentLoca")
            If jobTitle = "Unloader Supervisor" Then
                dataItem.ForeColor = Drawing.Color.Blue
            ElseIf HomeLoca <> CurrentLoca Then
                dataItem.ForeColor = Drawing.Color.Red
            Else
                dataItem.ForeColor = Drawing.Color.Black
            End If
            Dim OAPlus As Decimal = DirectCast(e.Item.DataItem, DataRowView)("OAPlus")
            Dim OAMinus As Decimal = DirectCast(e.Item.DataItem, DataRowView)("OAMinus")
            Dim lblPlus As Label = e.Item.FindControl("lblOAPlus")
            Dim lblMinus As Label = e.Item.FindControl("lblOAMinus")
            lblPlus.Text = IIf(OAPlus = 0, " --- ", FormatCurrency(OAPlus, 0))
            lblMinus.Text = IIf(OAMinus = 0, " --- ", FormatCurrency(OAMinus, 0))


        End If


        '        If TypeOf e.Item Is GridDataItem Then

        '    Dim dataItem As GridDataItem = e.Item

        '    Dim jobTitle As String = dataItem.Item("JobTitle").Text
        '    If jobTitle = "Unloader Supervisor" Then
        '        dataItem.BackColor = Drawing.Color.Blue
        '        dataItem.ForeColor = Drawing.Color.White
        '    End If

        'End If


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
                        If curItem.Equals(dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("ID").ToString()) Then
                            dataItem.Selected = True
                        End If
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub RadGrid2_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid2.NeedDataSource
        '        Dim locaID As Guid = New Guid(cbLocations.SelectedValue)
        Dim sDate As Date = rbLst.SelectedValue ' "11/1/2012"
        Dim eDate As Date = DateAdd(DateInterval.Day, 13, sDate)
        Dim tpdal As New TimePuncheDAL
        Dim dn As Date = Date.Now()
        dn = FormatDateTime(tpdal.getPayStartDate(dn), DateFormat.ShortDate)
        lblCurPrevPayPeriod.Text = IIf(sDate = dn, "<span class='currentColor'>CURRENT</span>", "<span class='previousColor'>PREVIOUS</span>")

        If Session("selectedItems") IsNot Nothing Then

            Dim selectedItems As ArrayList = CType(Session("selectedItems"), ArrayList)
            Dim empID As Guid = New Guid(selectedItems(selectedItems.Count - 1).ToString)

            Dim dba As New DBAccess
            dba.CommandText = "SELECT AdditionalComp.AddCompID, AdditionalComp.EmployeeID, AdditionalComp.AddCompStartDate, " & _
                "AdditionalComp.AddCompEndDate, AdditionalComp.AddCompAmount, AdditionalComp.AddCompComments, AdditionalComp.userID, " & _
                "AdditionalComp.TimeStamp, AddCompDesc.CompDesc, AdditionalComp.AddCompDescriptionID, AddCompDesc.Credit, AddCompDesc.InActive " & _
                "FROM AdditionalComp INNER JOIN " & _
                "AddCompDesc ON AdditionalComp.AddCompDescriptionID = AddCompDesc.AddCompDescriptionID " & _
                "WHERE AdditionalComp.EmployeeID = @empID AND AddCompStartDate >= @sDate AND AddCompEndDate <= @eDate"
            dba.AddParameter("@empID", empID)
            dba.AddParameter("@sDate", sDate)
            dba.AddParameter("@eDate", eDate)

            Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
            RadGrid2.DataSource = dt
        End If

    End Sub

    Private Sub RadGrid2_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid2.ItemDataBound

        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim dataItem As GridDataItem = e.Item

            '            Dim jobTitle As String = DirectCast(e.Item.DataItem, DataRowView)("JobTitle")
            '            Dim HomeLoca As Guid = IIf(IsDBNull(DirectCast(e.Item.DataItem, DataRowView)("HomeLoca")), New Guid("00000000-0000-0000-0000-000000000000"), DirectCast(e.Item.DataItem, DataRowView)("HomeLoca"))
            '            Dim CurrentLoca As Guid = DirectCast(e.Item.DataItem, DataRowView)("CurrentLoca")
            'If jobTitle = "Unloader Supervisor" Then
            '    dataItem.ForeColor = Drawing.Color.Blue
            'ElseIf HomeLoca <> CurrentLoca Then
            '    dataItem.ForeColor = Drawing.Color.Red
            'Else
            '    dataItem.ForeColor = Drawing.Color.Black
            'End If
            Dim Credit As Boolean = DirectCast(e.Item.DataItem, DataRowView)("Credit")
            Dim OAmount As Decimal = DirectCast(e.Item.DataItem, DataRowView)("AddCompAmount")
            '            Dim OAMinus As Decimal = DirectCast(e.Item.DataItem, DataRowView)("OAMinus")
            Dim lblAddCompAmount As Label = e.Item.FindControl("lblAddCompAmount")
            '            Dim lblMinus As Label = e.Item.FindControl("lblOAMinus")
            If Not Credit Then OAmount = OAmount * -1

            lblAddCompAmount.Text = IIf(OAmount = 0, " --- ", FormatCurrency(OAmount, 2))
            '            lblMinus.Text = IIf(OAMinus = 0, " --- ", FormatCurrency(OAMinus, 0))

        End If
        If e.Item.IsInEditMode Then
            Dim sddp As RadDatePicker = e.Item.FindControl("dpAddCompStartDate")
            Dim eddp As RadDatePicker = e.Item.FindControl("dpAddCompEndDate")
            sddp.MinDate = rbLst.SelectedValue
            sddp.MaxDate = DateAdd(DateInterval.Day, 13, sddp.MinDate)
            eddp.MinDate = rbLst.SelectedValue
            eddp.MaxDate = DateAdd(DateInterval.Day, 13, sddp.MinDate)

        End If

    End Sub

    Private Sub RadGrid2_PreRender(sender As Object, e As System.EventArgs) Handles RadGrid2.PreRender
        If Not (User.IsInRole("SysOp") Or User.IsInRole("Administrator")) Then
            RadGrid2.MasterTableView.Columns.FindByUniqueName("EditCommandColumn").Visible = True
            RadGrid2.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
        Else
            RadGrid2.MasterTableView.Columns.FindByUniqueName("EditCommandColumn").Visible = True
            RadGrid2.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = True
        End If

        ' uncomment next three lines to prevent deleting last remaining record in table
        '        If RadGrid2.MasterTableView.Items.Count = 1 Then
        '        RadGrid2.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
        '        End If
    End Sub



#End Region

#Region "Grid Events"

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
            RadGrid2.MasterTableView.ClearEditItems()
            ReloadForms(empid)
        End If
    End Sub

    Private Sub RadGrid2_DeleteCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid2.DeleteCommand
        Dim itm As GridDataItem = DirectCast(e.Item, GridDataItem)
        Dim delitem As Guid = itm.OwnerTableView.DataKeyValues(itm.ItemIndex)("AddCompID")
        Dim dba As New DBAccess()
        dba.CommandText = "DELETE FROM AdditionalComp WHERE AddCompID = @AddCompID"
        dba.AddParameter("@AddCompID", delitem)
        dba.ExecuteNonQuery()
        RadGrid1.Rebind()

    End Sub

    Private Sub RadGrid2_EditCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid2.EditCommand

    End Sub

    Private Sub RadGrid2_UpdateCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid2.UpdateCommand
        Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
        Dim ac As New additionalComp

        If TypeOf e.Item Is GridEditableItem AndAlso e.Item.IsInEditMode Then

            Dim acID As String = editedItem.GetDataKeyValue("AddCompID").ToString
            ac.AddCompID = New Guid(acID)
            ac.EmployeeID = New Guid(editedItem.GetDataKeyValue("EmployeeID").ToString)
            Dim dpsDate As RadDatePicker = CType(editedItem.FindControl("dpAddCompStartDate"), RadDatePicker)
            ac.AddCompStartDate = FormatDateTime(dpsDate.SelectedDate, DateFormat.ShortDate)
            Dim dpedate As RadDatePicker = CType(editedItem.FindControl("dpAddCompEndDate"), RadDatePicker)
            ac.AddCompEndDate = FormatDateTime(dpedate.SelectedDate, DateFormat.ShortDate)
            Dim compDesc As RadComboBox = CType(editedItem.FindControl("cbAddCompDescription"), RadComboBox)
            ac.AddCompDescriptionID = New Guid(compDesc.SelectedValue.ToString)
            Dim compAmount As RadNumericTextBox = CType(editedItem.FindControl("numAddCompAmount"), RadNumericTextBox)
            ac.AddCompAmount = compAmount.Value
            Dim compComments As RadTextBox = CType(editedItem.FindControl("txtAddCompComments"), RadTextBox)
            ac.AddCompComments = compComments.Text
            Dim userdal As New userDAL
            Dim ssu As ssUser = userdal.getUserByName(HttpContext.Current.User.Identity.Name)
            ac.userID = ssu.userID
            ac.TimeStamp = Date.Now()
            Dim dba As New DBAccess()
            dba.CommandText = "UPDATE AdditionalComp SET AddCompStartDate=@AddCompStartDate, AddCompEndDate=@AddCompEndDate, AddCompDescriptionID=@AddCompDescriptionID, AddCompAmount=@AddCompAmount, AddCompComments=@AddCompComments, " & _
                "userID=@userID, TimeStamp=@TimeStamp WHERE AddCompID=@AddCompID "
            dba.AddParameter("@AddCompStartDate", ac.AddCompStartDate)
            dba.AddParameter("@AddCompEndDate", ac.AddCompEndDate)
            dba.AddParameter("@AddCompDescriptionID", ac.AddCompDescriptionID)
            dba.AddParameter("@AddCompAmount", ac.AddCompAmount)
            dba.AddParameter("@AddCompComments", ac.AddCompComments)
            dba.AddParameter("@userID", ac.userID)
            dba.AddParameter("@TimeStamp", ac.TimeStamp)
            dba.AddParameter("@AddCompID", ac.AddCompID)
            Try
                dba.ExecuteNonQuery()
                RadGrid1.Rebind()

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub RadGrid2_InsertCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid2.InsertCommand

        Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
        Dim ac As New additionalComp

        If TypeOf e.Item Is GridEditableItem Then

            ac.AddCompID = Guid.NewGuid()
            ac.EmployeeID = New Guid(RadGrid1.SelectedValue.ToString)
            Dim dpsDate As RadDatePicker = CType(editedItem.FindControl("dpAddCompStartDate"), RadDatePicker)
            ac.AddCompStartDate = FormatDateTime(dpsDate.SelectedDate, DateFormat.ShortDate)
            Dim dpedate As RadDatePicker = CType(editedItem.FindControl("dpAddCompEndDate"), RadDatePicker)
            If dpedate.SelectedDate Is Nothing Then
                ac.AddCompEndDate = ac.AddCompStartDate
            Else
                ac.AddCompEndDate = FormatDateTime(dpedate.SelectedDate, DateFormat.ShortDate)
            End If
            Dim compDesc As RadComboBox = CType(editedItem.FindControl("cbAddCompDescription"), RadComboBox)
            ac.AddCompDescriptionID = New Guid(compDesc.SelectedValue.ToString)
            Dim compAmount As RadNumericTextBox = CType(editedItem.FindControl("numAddCompAmount"), RadNumericTextBox)
            ac.AddCompAmount = compAmount.Value
            Dim compComments As RadTextBox = CType(editedItem.FindControl("txtAddCompComments"), RadTextBox)
            ac.AddCompComments = IIf(compComments.Text Is Nothing, "", compComments.Text)
            Dim userdal As New userDAL
            Dim ssu As ssUser = userdal.getUserByName(HttpContext.Current.User.Identity.Name)
            ac.userID = ssu.userID
            ac.TimeStamp = Date.Now()

            Dim dba As New DBAccess
            dba.CommandText = "INSERT INTO AdditionalComp (AddCompID, EmployeeID, AddCompStartDate, AddCompEndDate, AddCompDescriptionID, AddCompAmount, AddCompComments, " & _
                "userID, TimeStamp) VALUES (@AddCompID, @EmployeeID, @AddCompStartDate, @AddCompEndDate, @AddCompDescriptionID, @AddCompAmount, @AddCompComments, @userID, @TimeStamp)"
            dba.AddParameter("@AddCompID", ac.AddCompID)
            dba.AddParameter("@EmployeeID", ac.EmployeeID)
            dba.AddParameter("@AddCompStartDate", ac.AddCompStartDate)
            dba.AddParameter("@AddCompEndDate", ac.AddCompEndDate)
            dba.AddParameter("@AddCompDescriptionID", ac.AddCompDescriptionID)
            dba.AddParameter("@AddCompAmount", ac.AddCompAmount)
            dba.AddParameter("@AddCompComments", ac.AddCompComments)
            dba.AddParameter("@userID", ac.userID)
            dba.AddParameter("@TimeStamp", ac.TimeStamp)
            Try
                dba.ExecuteNonQuery()
                RadGrid1.Rebind()
            Catch ex As Exception
            End Try
        End If
    End Sub

#End Region

#Region "Controls"

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

    Protected Sub LoadPayWeekSelections()
        Dim tpdal As New TimePuncheDAL
        Dim csdate1 As Date = tpdal.getPayStartDate(Date.Now()) 'sdate is start of pay period
        Dim cedate1 As Date = DateAdd(DateInterval.Day, 6, csdate1)
        Dim csdate2 As Date = DateAdd(DateInterval.Day, 7, csdate1)
        Dim cedate2 As Date = DateAdd(DateInterval.Day, 6, csdate2)
        ' abbrev month and day of month w/ superscript
        Dim mo As String = MonthName(Month(csdate1), True)
        Dim da As String = Day(csdate1).ToString
        da = Utilities.getIntegerSuperScript(da)

        'spin up list item
        Dim li As New ListItem
        ' how far are we into the pay period
        Dim sdateIZdayzFromNow As Integer = DateDiff(DateInterval.Day, csdate1, Date.Now()) 'number of days since sdate
        If sdateIZdayzFromNow < 15 Then  'we are in week 1 of current pay period
            ' ***** allow previous week if we are less than 5 days into pay period
            If sdateIZdayzFromNow < 15 Then  'we are four days or less into current pay period
                Dim psdate1 As Date = DateAdd(DateInterval.Day, -14, csdate1) 'sdate - 14 is start of prev pay period
                Dim pedate1 As Date = DateAdd(DateInterval.Day, 6, psdate1)
                Dim psdate2 As Date = DateAdd(DateInterval.Day, 7, psdate1)
                Dim pedate2 As Date = DateAdd(DateInterval.Day, 6, psdate2)
                '                lblPPSelect.Visible = True
                li = New ListItem
                li.Text = Format(psdate1, "MMM dd, yyyy") & " <font size='1' color='gray'> thru </font> " & Format(pedate2, "MMM dd, yyyy") & " &nbsp; &nbsp; &nbsp; &nbsp; "
                '            li.Text = MonthName(Month(psdate1), True) & " " & Day(psdate1).ToString & Utilities.getIntegerSuperScript(Day(psdate1).ToString) & "<font size='1'> thru </font>" & MonthName(Month(pedate2), True) & " " & Day(pedate2).ToString & Utilities.getIntegerSuperScript(Day(pedate2).ToString) & " <font size='1'>(Previous Pay Period)</font>"
                li.Value = psdate1.ToShortDateString
                rbLst.Items.Add(li)
            End If
        Else 'we are in week 2 of current pay period
            ' do something

        End If

        li = New ListItem
        li.Text = Format(csdate1, "MMM dd, yyyy") & " <font size='1' color='gray'> thru </font> " & Format(cedate2, "MMM dd, yyyy") & " <font size='1'><--(Current Pay Period)</font>"
        '        li.Text = MonthName(Month(csdate1), True) & " " & Day(csdate1).ToString & Utilities.getIntegerSuperScript(Day(csdate1).ToString) & "<font size='1'> thru </font>" & MonthName(Month(cedate1), True) & " " & Day(cedate2).ToString & Utilities.getIntegerSuperScript(Day(cedate2).ToString) & " <font size='1'>(Current Pay Period)</font>"
        li.Value = csdate1.ToShortDateString
        rbLst.Items.Add(li)
        Dim sday As String = WeekdayName(Weekday(tpdal.getPayStartDate(Date.Now)))
        Dim eday As String = WeekdayName(Weekday(DateAdd(DateInterval.Day, 6, tpdal.getPayStartDate(Date.Now))))

        ' lblPayWeek.Text = sday & " thru " & eday
        rbLst.SelectedIndex = rbLst.Items.Count - 1
        setHelpDates()
        Dim sdate As Date = rbLst.SelectedValue
        sdate = DateAdd(DateInterval.Day, 13, sdate)
        Dim edate As Date = DateAdd(DateInterval.Day, 5, sdate)

        lbldowend.Text = WeekdayName(Weekday(sdate))
        lbldowend5.Text = WeekdayName(Weekday(edate))

        For Each item As ListItem In rbLst.Items
            item.Attributes.CssStyle("font-size") = "16px"
            If item.Selected Then
                item.Attributes.CssStyle("color") = "Black"
            Else
                item.Attributes.CssStyle("color") = "Gray"
            End If

        Next
    End Sub
#End Region

#Region "Control Events"

    Protected Sub cbLocations_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        If cbLocations.SelectedIndex < 0 Then
            RadGrid1.Visible = False
            lblSelectLocation.Visible = True
            lblSelectEmployee.Visible = False
        Else
            RadGrid1.Rebind()
            lblSelectLocation.Visible = False
            lblSelectEmployee.Visible = True
        End If
        pnlWOedit.Visible = False
        pnlTitle.Visible = True

    End Sub

#End Region

#Region "Form"

    Private Sub ReloadForms(ByVal empid As Guid)
        pnlWOedit.Visible = True
        pnlTitle.Visible = False
        RadGrid2.Visible = True
        RadGrid2.Rebind()

        lblSelectEmployee.Visible = False

        Dim empdal As New empDAL()
        Dim emp As Employee = empdal.GetEmployeeByID(empid)
        lblEmpName.Text = emp.rtdsFirstName & " " & emp.rtdsLastName
        txt_Comments.Text = emp.Comments
        '        txt_Certification.Text = emp.Certification

        If emp.PhotoJpegData Is Nothing Then
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
        ElseIf emp.PhotoJpegData.Count < 1 Then
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
            imgMugShot.SavedImageName = emp.rtdsFirstName & "'s MugShot®"
        End If
        imgMugShot.Width = Unit.Pixel(75)

    End Sub

    Public Sub displayCurrPayPeriod(ByVal empid As Guid)
        Dim tpDAL As New TimePuncheDAL()
        Dim ppDate As Date = FormatDateTime(Date.Now(), DateFormat.ShortDate)
        Dim curTimeCard As List(Of TimePunche) = tpDAL.getTimePunchesByEmpIDandPayPeriod(empid, ppDate)
        Dim pps As Date = tpDAL.getPayStartDate(ppDate)
        Dim ttlpptime As String = String.Empty
        '        lblcwk.Text = Format(pps, "MMM dd ") & " <font style='font-weight:normal;' size='1'>thru</font> " & Format(DateAdd(DateInterval.Day, 13, pps), "MMM dd")
        If curTimeCard.Count > 0 Then
            'Dim ctbl As String = BuildTimePunche(curTimeCard, ttlpptime)
            'lblttlcptime.Text = ttlpptime
            'lblCurpp.Text = ctbl
        End If
    End Sub

    Public Sub displayPrevPayPeriod(ByVal empid As Guid, ByVal sDate As Date)
        Dim tpDAL As New TimePuncheDAL()
        Dim pvDate As Date = tpDAL.getPayStartDate(sDate)
        Dim prevTimeCard As List(Of TimePunche) = tpDAL.getTimePunchesByEmpIDandPayPeriod(empid, pvDate)
        Dim ttlpptime As String = String.Empty
        '        lblpwk.Text = Format(pvDate, "MMM dd") & " <font style='font-weight:normal;' size='1'>thru</font> " & Format(DateAdd(DateInterval.Day, 13, pvDate), "MMM dd")
        If prevTimeCard.Count > 0 Then
            '            Dim ctbl As String = BuildTimePunche(prevTimeCard, ttlpptime)
            '            lblttlpptime.Text = ttlpptime
            '            lblPrepp.Text = ctbl
        End If
    End Sub

#End Region


    Private Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
    End Sub

    Private Sub rbLst_PreRender(sender As Object, e As System.EventArgs) Handles rbLst.PreRender
        For Each item As ListItem In rbLst.Items
            item.Attributes.CssStyle("font-size") = "16px"
            If item.Selected Then
                item.Attributes.CssStyle("color") = "Black"
            Else
                item.Attributes.CssStyle("color") = "Gray"
            End If

        Next
    End Sub

    Private Sub rbLst_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rbLst.SelectedIndexChanged
        If rbLst.Items.Count > 1 Then
            For Each item As ListItem In rbLst.Items
                item.Attributes.CssStyle("font-size") = "16px"
                If item.Selected Then
                    item.Attributes.CssStyle("color") = "Black"
                Else
                    item.Attributes.CssStyle("color") = "Gray"
                End If
            Next
        End If
        RadGrid2.MasterTableView.ClearEditItems()
        RadGrid1.Rebind()
        RadGrid2.Rebind()
        setHelpDates()
    End Sub
    Protected Sub setHelpDates()
        Dim sdate As Date = rbLst.SelectedValue
        Dim sdate1 As String = Format(sdate, "MMM d")
        Dim edate1 As String = Format(DateAdd(DateInterval.Day, 6, sdate), "MMM d")
        Dim sdate2 As String = Format(DateAdd(DateInterval.Day, 7, sdate), "MMM d")
        Dim edate2 As String = Format(DateAdd(DateInterval.Day, 13, sdate), "MMM d")
        lblspay1.Text = sdate1
        lblspay1a.Text = sdate1
        lblspay2.Text = sdate2
        lblspay2a.Text = sdate2

        lblepay1.Text = edate1
        lblepay1a.Text = edate1
        lblepay2.Text = edate2
        lblepay2a.Text = edate2
    End Sub


End Class
'Dim dba As New DBAccess()
'Dim datenow As Date = FormatDateTime(Date.Now(), DateFormat.ShortDate)
'dba.CommandText = "SELECT E.ID AS empID, E.LastName + ', ' + E.FirstName + '  (' + E.Login + ')' AS EmpName, Employment.JobTitle, " & _
'    "e.LocationID, e.HomeLocationID, Select other stuff as amount where empid=empid , select other stuff as negamount " & _
'    "FROM Employee AS E INNER JOIN " & _
'    "Employment ON E.ID = Employment.EmployeeID " & _
'    "WHERE (E.LocationID = @locaID) AND (Employment.PayType <> '3') " & _
'    "AND (Employment.DateOfDismiss > @datenow) " & _
'    "ORDER BY E.LastName "
'dba.AddParameter("@locaID", cbLocations.SelectedValue)
'dba.AddParameter("@datenow", datenow)

'Dim tpdal As New TimePuncheDAL
'Dim selectedPayPeriod As Date = "11/1/2012"
'selectedPayPeriod = FormatDateTime(Date.Now, DateFormat.ShortDate)
'Dim sdate As Date = tpdal.getPayStartDate(selectedPayPeriod)
'Dim edate As Date = DateAdd(DateInterval.Day, 13, sdate)

'Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
'dt.Columns.Add("+Amount+", Type.GetType("System.String"))
'dt.Columns.Add("-Amount-", Type.GetType("System.String"))
'For Each rw As DataRow In dt.Rows
'    Dim empid As Guid = dt.Rows(0).Item(0)
'    dba.CommandText = "SELECT SUM(AdditionalComp.AddCompAmount) AS AddCompAmount " & _
'        "FROM AdditionalComp INNER JOIN " & _
'        "AddCompDesc ON AdditionalComp.AddCompDescriptionID = AddCompDesc.AddCompDescriptionID " & _
'        "WHERE (AdditionalComp.EmployeeID =  @empID) AND (AdditionalComp.AddCompStartDate >= @sDate) " & _
'        "AND (AdditionalComp.AddCompEndDate <= @eDate) AND (AddCompDesc.Credit = 1)"
'    dba.AddParameter("@empID", rw.Item("empID"))
'    dba.AddParameter("@sDate", sdate)
'    dba.AddParameter("@eDate", edate)
'    Dim dtr As DataTable = dba.ExecuteDataSet.Tables(0)
'    Dim txtPlus As String = String.Empty
'    If dtr.Rows.Count > 0 Then
'        Dim dtrRow As DataRow = dtr.Rows(0)
'        txtPlus = IIf(IsDBNull(dtrRow.Item(0)), " --- ", dtrRow.Item(0).ToString)
'    Else
'        txtPlus = " --- "
'    End If
'    rw.Item("+Amount+") = txtPlus

'    Dim txtMinus As String = String.Empty
'    dba.CommandText = "SELECT SUM(AdditionalComp.AddCompAmount) AS AddCompAmount " & _
'        "FROM AdditionalComp INNER JOIN " & _
'        "AddCompDesc ON AdditionalComp.AddCompDescriptionID = AddCompDesc.AddCompDescriptionID " & _
'        "WHERE (AdditionalComp.EmployeeID =  @empID) AND (AdditionalComp.AddCompStartDate >= @sDate) " & _
'        "AND (AdditionalComp.AddCompEndDate <= @eDate) AND (AddCompDesc.Credit = 0)"
'    dba.AddParameter("@empID", rw.Item("empID"))
'    dba.AddParameter("@sDate", sdate)
'    dba.AddParameter("@eDate", edate)
'    dtr = dba.ExecuteDataSet.Tables(0)
'    If dtr.Rows.Count > 0 Then
'        Dim dtrRow As DataRow = dtr.Rows(0)
'        txtMinus = IIf(IsDBNull(dtrRow.Item(0)), " --- ", dtrRow.Item(0).ToString)
'    Else
'        txtMinus = " --- "
'    End If
'    rw.Item("-Amount-") = txtMinus
'Next

'RadGrid1.DataSource = dt

'Dim em As New Employee








'    Dim arg As String = e.Argument
'    Dim sarg() As String = Split(arg, ":")
'    'If arg.Contains("TimePunche") Then

'    '    Dim empid As Guid = RadGrid1.SelectedValue
'    '    Dim selectedItems As ArrayList
'    '    ' the grid is NOT multi select so I'm just turnin' the array back to a single item (currently selected employee)
'    '    '            If Session("selectedItems") Is Nothing Then
'    '    selectedItems = New ArrayList
'    '    '        Else
'    '    '            selectedItems = CType(Session("selectedItems"), ArrayList)
'    '    '        End If
'    '    selectedItems.Add(empid)
'    '    Session("selectedItems") = selectedItems
'    '    ReloadForms(empid)
'    '    RadGrid1.Rebind()

'    'ElseIf arg.Contains("Compensation") Then
'    '    'Dim empid As Guid = RadGrid1.SelectedValue
'    '    'Dim selectedItems As ArrayList
'    '    'If Session("selectedItems") Is Nothing Then
'    '    '    selectedItems = New ArrayList
'    '    'Else
'    '    '    selectedItems = CType(Session("selectedItems"), ArrayList)
'    '    'End If
'    '    'selectedItems.Add(empid)
'    '    'Session("selectedItems") = selectedItems


'    'Else
'    '    ReloadForms(RadGrid1.SelectedValue)
'    'End If














'Dim sqlStr As String = String.Empty
'sqlStr = "DECLARE @tblEmpTable TABLE (ID  uniqueidentifier,Emp  VarChar(50),CurrentLoca  uniqueidentifier,HomeLoca  uniqueidentifier,JobTitle  VarChar(50)) " & _
'    "INSERT INTO @tblEmpTable (ID, Emp, CurrentLoca, HomeLoca,JobTitle) " & _
'    "SELECT dbo.Employee.ID,  " & _
'    "dbo.Employee.FirstName + ' ' + dbo.Employee.LastName + ' :  ' + dbo.Employee.Login AS Emp,  " & _
'    "dbo.Employee.LocationID AS CurrentLoca, dbo.Employee.HomeLocationID AS HomeLoca, dbo.Employment.JobTitle " & _
'    "FROM dbo.Employee INNER JOIN " & _
'    "dbo.Location ON dbo.Employee.LocationID = dbo.Location.ID INNER JOIN " & _
'    "dbo.Employment ON dbo.Employee.ID = dbo.Employment.EmployeeID " & _
'    "WHERE (dbo.Location.ID = @locaID) AND (dbo.Employment.DateOfDismiss > CONVERT(DATETIME, '9999-12-30 00:00:00', 102)) " & _
'    "DECLARE @tbAddComp TABLE (ID  uniqueidentifier,AddCompAmount  Money,AddCompComments  varchar(MAX),CompDesc  VarChar(50),Credit  Bit, OtherAmt  Money) " & _
'    "INSERT INTO @tbAddComp (ID, AddCompAmount, AddCompComments, CompDesc,Credit,OtherAmt) " & _
'    "SELECT dbo.Employee.ID,  " & _
'    "dbo.AdditionalComp.AddCompAmount,  " & _
'    "dbo.AdditionalComp.AddCompComments,  " & _
'    "dbo.AddCompDesc.CompDesc,  " & _
'    "dbo.AddCompDesc.Credit, " & _
'    "CASE " & _
'    "WHEN Credit = 0 " & _
'    "THEN AddCompAmount - AddCompAmount*2 " & _
'    "ELSE AddCompAmount " & _
'    "END AS OtherAmt " & _
'    "FROM dbo.AddCompDesc INNER JOIN " & _
'    "dbo.AdditionalComp ON dbo.AddCompDesc.AddCompDescriptionID = dbo.AdditionalComp.AddCompDescriptionID INNER JOIN " & _
'    "dbo.Employee ON dbo.AdditionalComp.EmployeeID = dbo.Employee.ID " & _
'    "WHERE (dbo.AdditionalComp.AddCompStartDate >= @startdate) AND (dbo.AdditionalComp.AddCompEndDate <= @enddate) " & _
'    "select tblEmpTable.ID, Emp, CurrentLoca, HomeLoca,JobTitle, AddCompAmount, AddCompComments, CompDesc,Credit, OtherAmt " & _
'    "from @tblEmpTable AS tblEmpTable " & _
'    "LEFT OUTER JOIN @tbAddComp AS tbAddComp " & _
'    "ON tblEmpTable.ID = tbAddComp.ID "
'dba.CommandText = sqlStr















'----------------------------------------------------------------------------------------------- Additional Comps	
'sqlStr &= "SELECT tblEmpTable.ID, Emp,  " & _
'    "CASE " & _
'    "WHEN AddCompStartDate IS NULL " & _
'    "THEN '' " & _
'    "ELSE AddCompStartDate " & _
'    "END AS AddCompStartDate,  " & _
'    "CASE " & _
'    "WHEN AddCompEndDate IS NULL " & _
'    "THEN '' " & _
'    "ELSE AddCompEndDate " & _
'    "END AS AddCompEndDate ,  " & _
'    "CurrentLoca, HomeLoca, JobTitle,  " & _
'    "CASE " & _
'    "WHEN AddCompAmount IS NULL  " & _
'    "THEN 0 " & _
'    "ELSE AddCompAmount " & _
'    "END AS AddCompAmount, " & _
'    "CASE " & _
'    "WHEN AddCompComments IS NULL " & _
'    "THEN '' " & _
'    "ELSE AddCompComments " & _
'    "END AS AddCompComments,  " & _
'    "CASE " & _
'    "WHEN CompDesc IS NULL " & _
'    "THEN '' " & _
'    "ELSE CompDesc " & _
'    "END AS CompDesc, " & _
'    "CASE " & _
'    "WHEN Credit IS NULL " & _
'    "THEN 0 " & _
'    "ELSE Credit " & _
'    "END AS Credit,  " & _
'    "CASE   " & _
'    "WHEN Credit IS NULL " & _
'    "THEN 0 " & _
'    "ELSE OAPlus " & _
'    "END AS OAPlus,  " & _
'    "CASE   " & _
'    "WHEN Credit IS NULL " & _
'    "THEN 0 " & _
'    "ELSE OAMinus " & _
'    "END AS OAMinus, " & _
'    "CASE " & _
'    "WHEN OtherAmt IS NULL " & _
'    "THEN 0 " & _
'    "ELSE OtherAmt " & _
'    "END AS OtherAmt " & _
'    "FROM @tblEmpTable AS tblEmpTable " & _
'    "LEFT OUTER JOIN @tbAddComp AS tbAddComp " & _
'    "ON tblEmpTable.ID = tbAddComp.ID "
'---------------------------------------------------------------------------------------------- DETAILS     








'----------------------------------------------------------------------------------------------------------WIP
'sqlStr &= "select tblEmpTable.ID,  " & _
'    "CASE " & _
'    "WHEN SUM(OtherAmt) IS NULL " & _
'    "THEN 0 " & _
'    "ELSE SUM(OtherAmt) " & _
'    "END AS OtherAmt " & _
'    "FROM @tblEmpTable AS tblEmpTable " & _
'    "LEFT OUTER JOIN @tbAddComp AS tbAddComp " & _
'    "ON tblEmpTable.ID = tbAddComp.ID " & _
'    "GROUP BY tblEmpTable.ID "





'Private Sub gridAddComp_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gridAddComp.NeedDataSource
'    Dim selectedItems As ArrayList = CType(Session("selectedItems"), ArrayList)
'    Dim empID As Guid = New Guid(selectedItems(selectedItems.Count - 1).ToString)
'    Dim dba As New DBAccess
'    dba.CommandText = "SELECT AdditionalComp.AddCompID, AdditionalComp.EmployeeID, AdditionalComp.AddCompStartDate, AdditionalComp.AddCompEndDate, AdditionalComp.AddCompAmount, AdditionalComp.AddCompComments, AdditionalComp.userID, AdditionalComp.TimeStamp, AddCompDesc.CompDesc, AddCompDesc.Credit, AddCompDesc.InActive FROM AdditionalComp INNER JOIN AddCompDesc ON AdditionalComp.AddCompDescriptionID = AddCompDesc.AddCompDescriptionID WHERE AdditionalComp.EmployeeID = @empID "
'    dba.AddParameter("@empID", empID)
'    Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
'    gridAddComp.DataSource = dt
'End Sub