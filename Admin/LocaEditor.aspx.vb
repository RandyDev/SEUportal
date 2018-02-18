Imports Telerik.Web.UI

Public Class LocaEditor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            cbParentCompany.Text = ""
            cbParentCompany.ClearSelection()
            chkInActive.Style.Item("color") = "#cccccc"
            chkInActive.Text = "Close Location"
            loadLocations()
            '            lbtnAddLocation.Enabled = True
            '            lbtnAddLocation.ToolTip = "future use"
            pnlLocaEdit.Visible = False
            lblCopy.Text = "<span style=""font-size:18px; font-weight:bold; color:#cfcfcf;""><center>Location & Pricing Manager</center></span>"
            RadTabStrip1.Tabs(0).Selected = True
            radmultipage1.SelectedIndex = 0
            RadTabStrip1.Visible = False
            radmultipage1.Visible = False
        Else
            lblCopy.Text = String.Empty
            lblCopy.Style.Item("display") = "none"
        End If
        txtZip.Attributes("onkeyup") = "decOnly(this);"
    End Sub

    Protected Sub loadLocations()
        Dim ldal As New locaDAL
        Dim locaList As DataTable = ldal.getAllLocations()
        cbLocations.DataSource = locaList
        cbLocations.DataTextField = "LocationName"
        cbLocations.DataValueField = "locaID"
        cbLocations.DataBind()
        cbLocations.ClearSelection()
    End Sub

    Private Sub cbLocations_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        lblsubmitBenefitsresponse.Visible = False
        If cbLocations.SelectedIndex > -1 Then
            loadLocation(New Guid(cbLocations.SelectedValue.ToString))
            RadTabStrip1.Visible = True
            radmultipage1.Visible = True
            RadGridPriceList.Rebind()
            pnlLocaPriceEdit.Visible = True
            lbtnSaveChanges.Text = "Save Changes"
            lbtnSaveChanges.CommandName = "Update"
            lbtnSaveChanges.CommandArgument = cbLocations.SelectedValue.ToString()
            Dim ldal As New locaDAL
            Dim dt As New DataTable()

            dt = ldal.GetJobTitlesbyLocationID(cbLocations.SelectedValue)
            '            pnlJobtitles.Visible = True
            JobTitleList.DataSource = dt
            JobTitleList.DataValueField = "JobTitleID"
            JobTitleList.DataTextField = "JobTitle"
            JobTitleList.DataBind()
            JobTitleList.Visible = True

            dt = ldal.GetJobTitles()
            lbAvailableJobTitles.DataSource = dt
            lbAvailableJobTitles.DataValueField = "JobTitleID"
            lbAvailableJobTitles.DataTextField = "JobTitle"
            lbAvailableJobTitles.DataBind()

            Dim JobTitles As RadListBoxItemCollection = JobTitleList.Items
            For Each item As RadListBoxItem In JobTitles
                Dim itemToRemove As RadListBoxItem = lbAvailableJobTitles.FindItemByText(item.Text)
                lbAvailableJobTitles.Items.Remove(itemToRemove)
            Next


            dt = ldal.GetLoadDescriptionsByLocationID(cbLocations.SelectedValue)
            LoadDescriptionsList.DataSource = dt
            LoadDescriptionsList.DataValueField = "ID"
            LoadDescriptionsList.DataTextField = "Name"
            LoadDescriptionsList.DataBind()

            dt = ldal.GetLoadDescriptions
            lbAvailableLoadDescriptions.DataSource = dt
            lbAvailableLoadDescriptions.DataValueField = "ID"
            lbAvailableLoadDescriptions.DataTextField = "Name"
            lbAvailableLoadDescriptions.DataBind()

            Dim loadDescriptions As RadListBoxItemCollection = LoadDescriptionsList.Items
            For Each item As RadListBoxItem In loadDescriptions
                Dim itemToRemove As RadListBoxItem = lbAvailableLoadDescriptions.FindItemByText(item.Text)
                lbAvailableLoadDescriptions.Items.Remove(itemToRemove)
            Next

            dt = ldal.GetDepartmentsByLocationID(cbLocations.SelectedValue)
            DepartmentList.DataSource = dt
            DepartmentList.DataTextField = "Name"
            DepartmentList.DataValueField = "ID"
            DepartmentList.DataBind()

            dt = ldal.GetDepartments()
            lbAvailableDepartments.DataSource = dt
            lbAvailableDepartments.DataTextField = "Name"
            lbAvailableDepartments.DataValueField = "ID"
            lbAvailableDepartments.DataBind()
            Dim departments As RadListBoxItemCollection = DepartmentList.Items
            For Each item As RadListBoxItem In departments
                Dim itemToRemove As RadListBoxItem = lbAvailableDepartments.FindItemByText(item.Text)
                lbAvailableDepartments.Items.Remove(itemToRemove)
            Next

            dt = ldal.GetJobDescriptionsByLocationID(cbLocations.SelectedValue)
            '            pnlJobtitles.Visible = True
            JobDescriptionList.DataSource = dt
            JobDescriptionList.DataValueField = "JobDescriptionID"
            JobDescriptionList.DataTextField = "JobDescription"
            JobDescriptionList.DataBind()
            JobDescriptionList.Visible = True

            dt = ldal.GetJobDescriptions()
            lbAvailableJobDescriptions.DataSource = dt
            lbAvailableJobDescriptions.DataValueField = "JobDescriptionID"
            lbAvailableJobDescriptions.DataTextField = "JobDescription"
            lbAvailableJobDescriptions.DataBind()
            Dim JobDescriptions As RadListBoxItemCollection = JobDescriptionList.Items
            For Each item As RadListBoxItem In JobDescriptions
                Dim itemToRemove As RadListBoxItem = lbAvailableJobDescriptions.FindItemByText(item.Text)
                lbAvailableJobDescriptions.Items.Remove(itemToRemove)
            Next

            RadGrid1.Rebind()
            RadGrid2.Rebind()

        End If
    End Sub

    Protected Sub cbDescription_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs)
        Dim ldal As New locaDAL()

        Dim dt As DataTable = ldal.GetLoadDescriptions()
        Dim combobox As Telerik.Web.UI.RadComboBox = DirectCast(sender, Telerik.Web.UI.RadComboBox)
        combobox.Items.Clear()
        For Each row As DataRow In dt.Rows
            Dim item As New RadComboBoxItem
            item.Text = row("Name").ToString()
            item.Value = row("ID").ToString
            combobox.Items.Add(item)
        Next


    End Sub

    Protected Sub OnSelectedIndexChangedHandler(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs)
        Session("SupplierID") = e.Value
    End Sub

#Region "Location"

    Private Sub loadLocation(ByVal locaID As Guid)
        ClearForm()
        pnlLocaEdit.Visible = True
        pnlLocaPriceEdit.Visible = True
        lblResponse.Text = ""
        lblResponse.Visible = False
        lblCopy.Style.Item("display") = "none"
        Dim ldal As New locaDAL
        Dim loca As Location = ldal.getLocationByID(locaID)
        txtLocationName.Text = loca.Name
        txtLoginPrefix.Text = loca.loginPrefix
        rblistEnableSickLeave.SelectedIndex = IIf(loca.EnableSickLeave, 0, 1)
        Dim tab As RadTab = RadTabStrip1.FindTabByText("Benefit Configuration")
        tab.Visible = loca.EnableSickLeave
        Dim tab2 As RadTab = RadTabStrip1.FindTabByText("JobTitles/LoadDescriptions/Departments")
        tab2.Selected = Not loca.EnableSickLeave
        RadPageView1.Selected = Not loca.EnableSickLeave

        cbParentCompany.SelectedValue = loca.ParentCompanyID.ToString
        chkInActive.Checked = loca.InActive
        chkPrintTimeStamp.Checked = loca.hhPrintTimeStamp
        chkInActive.Style.Item("color") = IIf(chkInActive.Checked, "Red", "#cccccc")
        chkInActive.Text = IIf(chkInActive.Checked, "Location CLOSED", "Close Location")
        cbTimeZones.SelectedValue = loca.TimezoneOffset
        txtBeginDatOffset.Value = loca.BeginDayOffset
        txtCheckCharge.Value = loca.CheckCharge
        txtDividend.Value = loca.Dividend * 100
        txtZip.Text = loca.locazip
        numAdministrativeFee.Value = loca.AdministrativeFee
        numCustomerFee.Value = loca.CustomerFee
        If Not loca.locaCity Is Nothing Then
            If loca.locaCity.Length > 0 Then
                lblCityState.Text = loca.locaCity & ", " & loca.locaState
            End If
        End If
        lblLocaID.Text = loca.ID.ToString
        LoadBenefits(loca.ID.ToString)
    End Sub


    Private Sub chkInActive_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkInActive.CheckedChanged
        chkInActive.Style.Item("color") = IIf(chkInActive.Checked, "Red", "#cccccc")
        '        chkInActive.Text = IIf(chkInActive.Checked, "CLOSE Location", "Close Location")

    End Sub

    Private Sub ClearForm()
        txtLocationName.Text = ""
        txtZip.Text = ""
        txtLoginPrefix.Text = ""
        lblCityState.Text = ""
        txtCheckCharge.Value = 0
        chkInActive.Checked = False
        chkPrintTimeStamp.Checked = False
        cbParentCompany.ClearSelection()
        cbTimeZones.ClearSelection()
        txtBeginDatOffset.Value = 0
        lblResponse.Text = ""
        lblLocaID.Text = ""
        txtCheckCharge.Text = ""
        txtDividend.Text = ""
        lbtnSaveChanges.Text = "Add New Location"
        lbtnSaveChanges.CommandName = "NewLocation"
        lbtnSaveChanges.CommandArgument = ""
        dpStartBenefitsDate.Clear()
        numMaxHours.Value = 0
        numMaxPerAnnum.Value = 0
        numMinHours.Value = 0
        numEligibleDays.Value = 0
        numAdministrativeFee.Value = 0
        numCustomerFee.Value = 0
    End Sub

    Private Sub lbtnSaveChanges_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles lbtnSaveChanges.Command
        Dim ldal As New locaDAL
        If cbParentCompany.SelectedIndex = -1 Then
            lblResponse.Visible = True
            lblResponse.Text = "Parent Company Required"
            Exit Sub
        End If

        Dim loca As New Location
        loca.Name = txtLocationName.Text.Trim
        loca.ParentCompanyID = New Guid(cbParentCompany.SelectedValue.ToString)
        loca.TimezoneOffset = cbTimeZones.SelectedValue
        loca.BeginDayOffset = txtBeginDatOffset.Value
        loca.InActive = chkInActive.Checked
        loca.hhPrintTimeStamp = chkPrintTimeStamp.Checked
        loca.CheckCharge = txtCheckCharge.Value
        loca.Dividend = txtDividend.Value * 0.01
        loca.EnableSickLeave = rblistEnableSickLeave.SelectedValue = "Yes"
        loca.AdministrativeFee = numAdministrativeFee.Value
        loca.CustomerFee = numCustomerFee.Value

        If txtZip.Text.Length = 5 Then
            If lblCityState.Text > "" And lblCityState.Text <> "Zip not found" Then
                loca.locazip = txtZip.Text
                Dim ar As String() = Split(lblCityState.Text, ", ")
                loca.locaCity = ar(0)
                loca.locaState = ar(1)
            Else
                loca.locazip = ""
                loca.locaCity = ""
                loca.locaState = ""
            End If
        Else
            loca.locazip = ""
            loca.locaCity = ""
            loca.locaState = ""
        End If
        loca.loginPrefix = txtLoginPrefix.Text.Trim()

        Dim resp As String = String.Empty
        If e.CommandName = "Update" Then
            loca.ID = New Guid(e.CommandArgument.ToString)
            resp = ldal.updateLocation(loca)
        ElseIf e.CommandName = "NewLocation" Then
            loca.ID = Guid.NewGuid()
            resp = ldal.insertLocation(loca)
        End If


        If resp Is Nothing Then
            If e.CommandName = "Update" Then
                lblResponse.Text = "Changes Saved"
            ElseIf e.CommandName = "NewLocation" Then
                lblResponse.Text = "NEW Location Saved"
                lbtnSaveChanges.Text = "Save Changes"
                lbtnSaveChanges.CommandName = "Update"
                lbtnSaveChanges.CommandArgument = loca.ID.ToString
                lblLocaID.Text = loca.ID.ToString
            End If

        Else
            lblResponse.Text = resp

        End If

        lblResponse.Visible = True

        loadLocations()
        cbLocations.Text = loca.Name
        cbLocations.SelectedValue = loca.ID.ToString
        If e.CommandName = "NewLocation" Then
            pnlLocaPriceEdit.Visible = True
            RadGridPriceList.Rebind()
        End If

    End Sub

#End Region

#Region "Price List"

    Private Sub RadGridPriceList_DeleteCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridPriceList.DeleteCommand
        If TypeOf e.Item Is GridDataItem Then
            Dim itm As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim delitem As Guid = itm.OwnerTableView.DataKeyValues(itm.ItemIndex)("PriceID")
            Dim ldal As New locaDAL()
            Dim delMsg As String = ldal.deletePriceList(delitem)
        End If
    End Sub

    Private Sub RadGridPriceList_InsertCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridPriceList.InsertCommand
        Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
        Dim ldal As New locaDAL
        Dim pl As New PriceList
        Dim plTable As DataTable = ldal.getLocaPriceListTable(New Guid(cbLocations.SelectedValue.ToString))
        Dim newRow As DataRow = plTable.NewRow
        newRow("PriceID") = Guid.NewGuid()


        ' locate priceID to be sure it exists and cancel edit if not
        '        Dim changedRows As DataRow() = plTable.Select("PriceID = " & itemID.ToString)
        '        If Not (changedRows.Length = 1) Then
        '        RadGridPriceList.Controls.Add(New LiteralControl("Unable to locate price line."))
        '        e.Canceled = True
        '        Return
        '        End If

        Dim newValues As Hashtable = New Hashtable
        e.Item.OwnerTableView.ExtractValuesFromItem(newValues, editedItem)

        '        Dim changedRow As DataRow = changedRows(0)

        newRow.BeginEdit()
        Try
            For Each entry As DictionaryEntry In newValues
                newRow(CType(entry.Key, String)) = IIf(entry.Value Is Nothing, DBNull.Value, entry.Value)
            Next
            newRow.EndEdit()
            ' store row to pl and send to DAL
            pl.PriceID = Guid.NewGuid()
            pl.LocationID = New Guid(cbLocations.SelectedValue.ToString)
            pl.DepartmentID = newRow.Item("DepartmentID")
            pl.LoadtypeID = newRow.Item("LoadtypeID")
            pl.LoadDescriptionID = newRow.Item("LoadDescriptionID")
            pl.RatePerCase = IIf(IsDBNull(newRow.Item("RatePerCase")), Nothing, newRow.Item("RatePerCase"))
            pl.RatePerPallet = IIf(IsDBNull(newRow.Item("RatePerPallet")), Nothing, newRow.Item("RatePerPallet"))
            pl.PerPalletLow = IIf(IsDBNull(newRow.Item("PerPalletLow")), Nothing, newRow.Item("PerPalletLow"))
            pl.PerPalletHigh = IIf(IsDBNull(newRow.Item("PerPalletHigh")), Nothing, newRow.Item("PerPalletHigh"))
            pl.RatePerLoad = IIf(IsDBNull(newRow.Item("RatePerLoad")), Nothing, newRow.Item("RatePerLoad"))
            pl.RateBadPallet = IIf(IsDBNull(newRow.Item("RateBadPallet")), Nothing, newRow.Item("RateBadPallet"))
            pl.RateRestack = IIf(IsDBNull(newRow.Item("RateRestack")), Nothing, newRow.Item("RateRestack"))
            pl.PriceMax = IIf(IsDBNull(newRow.Item("PriceMax")), Nothing, newRow.Item("PriceMax"))
            pl.RateDoubleStack = IIf(IsDBNull(newRow.Item("RateDoubleStack")), Nothing, newRow.Item("RateDoubleStack"))
            pl.RatePinWheeled = IIf(IsDBNull(newRow.Item("RatePinWheeled")), Nothing, newRow.Item("RatePinWheeled"))

            Dim resp As String = ldal.insertPriceList(pl)



        Catch ex As Exception
            newRow.CancelEdit()
            RadGridPriceList.Controls.Add(New LiteralControl("Unable to update PriceList. Reason: " & ex.Message))
            e.Canceled = True
        End Try


        RadGridPriceList.Controls.Add(New LiteralControl("Price List Item " & newRow("PriceID").ToString & " saved"))


    End Sub

    Private Sub RadGridPriceList_UpdateCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridPriceList.UpdateCommand

        Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
        Dim ldal As New locaDAL
        Dim pl As New PriceList
        Dim plTable As DataTable = ldal.getLocaPriceListTable(New Guid(cbLocations.SelectedValue.ToString))
        Dim itemID As Guid = editedItem.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("PriceID")

        ' locate priceID to be sure it exists and cancel edit if not
        '        Dim changedRows As DataRow() = plTable.Select("PriceID = " & itemID.ToString)
        '        If Not (changedRows.Length = 1) Then
        '        RadGridPriceList.Controls.Add(New LiteralControl("Unable to locate price line."))
        '        e.Canceled = True
        '        Return
        '        End If

        Dim newValues As Hashtable = New Hashtable
        e.Item.OwnerTableView.ExtractValuesFromItem(newValues, editedItem)

        '        Dim changedRow As DataRow = changedRows(0)

        Dim changedrow As DataRow = plTable.NewRow
        changedrow.BeginEdit()
        Try
            For Each entry As DictionaryEntry In newValues
                changedrow(CType(entry.Key, String)) = entry.Value
            Next
            changedrow.EndEdit()
            ' store row to pl and send to DAL
            pl.PriceID = itemID
            pl.LocationID = New Guid(cbLocations.SelectedValue.ToString)
            pl.DepartmentID = changedrow.Item("DepartmentID")
            pl.LoadtypeID = changedrow.Item("LoadtypeID")
            pl.LoadDescriptionID = changedrow.Item("LoadDescriptionID")
            pl.RatePerCase = changedrow.Item("RatePerCase")
            pl.RatePerPallet = changedrow.Item("RatePerPallet")
            pl.PerPalletLow = changedrow.Item("PerPalletLow")
            pl.PerPalletHigh = changedrow.Item("PerPalletHigh")
            pl.RatePerLoad = changedrow.Item("RatePerLoad")
            pl.RateBadPallet = changedrow.Item("RateBadPallet")
            pl.RateRestack = changedrow.Item("RateRestack")
            pl.PriceMax = changedrow.Item("PriceMax")
            pl.RateDoubleStack = changedrow.Item("RateDoubleStack")
            pl.RatePinWheeled = changedrow.Item("RatePinWheeled")

            Dim resp As String = ldal.updatePriceList(pl)



        Catch ex As Exception
            changedrow.CancelEdit()
            RadGridPriceList.Controls.Add(New LiteralControl("Unable to update PriceList. Reason: " & ex.Message))
            e.Canceled = True
        End Try


        RadGridPriceList.Controls.Add(New LiteralControl("Price List Item " & changedrow("PriceID") & " updated"))


        'Dim newRow As DataRow = plTable.NewRow
        'Dim allValues As DataRow() = plTable.Select("", "PriceID", DataViewRowState.CurrentRows)

        'If allValues.Length > 0 Then
        '    '   newRow("PriceID"
        'End If


        'If (TypeOf e.Item Is GridDataItem AndAlso e.Item.IsInEditMode) Then
        '    Dim editFormItem As GridEditFormItem = CType(e.Item, GridEditFormItem)
        '    Dim parentItem As GridDataItem = CType(e.Item, GridDataItem)
        '    Dim pl As New PriceList
        '    pl.PriceID = editedItem.GetDataKeyValue("PriceID")
        '    pl.LocationID = New Guid(cbLocations.SelectedValue.ToString)
        '    Dim deptid As RadComboBox = CType(editFormItem.FindControl("cbDepartment"), RadComboBox)
        '    pl.DepartmentID = New Guid(deptid.SelectedValue)
        '    Dim loadtype As RadComboBox = CType(editFormItem.FindControl("cbLoadType"), RadComboBox)
        '    pl.LoadtypeID = New Guid(loadtype.SelectedValue.ToString)
        '    Dim desc As RadComboBox = CType(editFormItem.FindControl("cbDepartment"), RadComboBox)
        '    pl.LoadDescriptionID = New Guid(desc.SelectedValue.ToString)

        '    Dim RatePerCase As RadNumericTextBox = CType(editFormItem.FindControl("num_RatePerCase"), RadNumericTextBox)
        '    pl.RatePerCase = RatePerCase.Value

        '    Dim RatePerPallet As RadNumericTextBox = CType(editFormItem.FindControl("num_RatePerPallet"), RadNumericTextBox)
        '    pl.RatePerPallet = RatePerPallet.Value

        '    Dim PerPalletLow As RadNumericTextBox = CType(editFormItem.FindControl("num_PerPalletLow"), RadNumericTextBox)
        '    pl.PerPalletLow = PerPalletLow.Value

        '    Dim PerPalletHigh As RadNumericTextBox = CType(editFormItem.FindControl("num_PerPalletHigh"), RadNumericTextBox)
        '    pl.PerPalletHigh = PerPalletHigh.Value

        '    Dim RatePerLoad As RadNumericTextBox = CType(editFormItem.FindControl("num_RatePerLoad"), RadNumericTextBox)
        '    pl.RatePerLoad = RatePerLoad.Value

        '    Dim RateBadPallet As RadNumericTextBox = CType(editFormItem.FindControl("num_RateBadPallet"), RadNumericTextBox)
        '    pl.RateBadPallet = RateBadPallet.Value

        '    Dim RateRestack As RadNumericTextBox = CType(editFormItem.FindControl("num_RateRestack"), RadNumericTextBox)
        '    pl.RateRestack = RateRestack.Value

        '    Dim PriceMax As RadNumericTextBox = CType(editFormItem.FindControl("num_PriceMax"), RadNumericTextBox)
        '    pl.PriceMax = PriceMax.Value

        '    Dim RateDoubleStack As RadNumericTextBox = CType(editFormItem.FindControl("num_RateDoubleStack"), RadNumericTextBox)
        '    pl.RateDoubleStack = RateDoubleStack.Value

        '    Dim RatePinWheeled As RadNumericTextBox = CType(editFormItem.FindControl("num_RatePinWheeled"), RadNumericTextBox)
        '    pl.RatePinWheeled = RatePinWheeled.Value

        '    Dim RateCheck As RadNumericTextBox = CType(editFormItem.FindControl("num_RateCheck"), RadNumericTextBox)
        '    pl.RateCheck = RateCheck.Value






        '        End If

    End Sub

    Private Sub RadGridPriceList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridPriceList.NeedDataSource
        If cbLocations.SelectedIndex <> -1 Then
            If Utilities.IsValidGuid(cbLocations.SelectedValue.ToString) Then
                Dim locaID As Guid = New Guid(cbLocations.SelectedValue.ToString)
                Dim ldal As New locaDAL
                Dim dt As DataTable = ldal.getLocaPriceListTable(locaID)
                RadGridPriceList.DataSource = dt
            End If
        End If
    End Sub

#End Region

    Private Sub RadAjaxManager1_AjaxRequest(sender As Object, e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        Dim arg As String = e.Argument
        Dim sarg() As String
        If arg.Contains("NewParent") Or arg.Contains("UpdateParent") Then
            sarg = Split(arg, ":")
            cbParentCompany.DataBind()
            cbParentCompany.Text = sarg(2)
            cbParentCompany.SelectedValue = sarg(1)
            lblResponse.Text = "Parent Saved"
            lblResponse.Visible = True
            '            cbParentCompany.Text = sarg(1)
        ElseIf arg.Contains("ZipCodeLookup") Then
            sarg = Split(arg, ":")
            Dim utl As New Utilities
            Dim zipinf() As String = utl.ZipCityState(sarg(1))
            If zipinf(0) <> "ZIP not found" Then
                lblCityState.Text = zipinf(0) & ", " & zipinf(1)
            Else
                lblCityState.Text = zipinf(0)
            End If
        End If
    End Sub

    Protected Sub lbtnAddLocation_Click(sender As Object, e As EventArgs) Handles lbtnAddLocation.Click
        lblCopy.Visible = False
        pnlLocaEdit.Visible = True
        ClearForm()
        pnlLocaPriceEdit.Visible = False
        cbLocations.ClearSelection()
    End Sub

    Private Sub RadGridPriceList_Init(sender As Object, e As System.EventArgs) Handles RadGridPriceList.Init
        Dim menu As GridFilterMenu = RadGridPriceList.FilterMenu
        Dim i As Integer = 0
        While i < menu.Items.Count
            If menu.Items(i).Text = "NoFilter" Or _
               menu.Items(i).Text = "Contains" Then
                i = i + 1
            Else
                menu.Items.RemoveAt(i)
            End If
        End While
    End Sub
    Private Sub btnSaveJobTitles_Click(sender As Object, e As EventArgs) Handles btnSaveJobTitles.Click
        lblsubmitBenefitsresponse.Visible = False
        Dim locaid As Guid = New Guid(cbLocations.SelectedValue)
        Dim dba As New DBAccess()
        Dim actionString As String = String.Empty
        Dim jobList As String = String.Empty
        Dim ldal As New locaDAL
        Dim jtl As Telerik.Web.UI.RadListBoxItemCollection
        jtl = JobTitleList.Items
        Dim dblb As New Telerik.Web.UI.RadListBox
        Dim dbJobDescriptionList As New Telerik.Web.UI.RadListBoxItemCollection(dblb)

        dba.CommandText = "DELETE FROM LocationJobTitle WHERE LocationID = @locaid"
        dba.AddParameter("@locaid", locaid)
        dba.ExecuteNonQuery()

        For Each itm As Telerik.Web.UI.RadListBoxItem In jtl
            dba.CommandText = "INSERT INTO LocationJobTitle (LocationID, JobTitleID) VALUES (@LocationID, @JobTitleID)"
            dba.AddParameter("@LocationID", locaid)
            dba.AddParameter("@JobTitleID", New Guid(itm.Value))
            dba.ExecuteNonQuery()
        Next

        Dim dt As DataTable = ldal.GetJobTitlesbyLocationID(cbLocations.SelectedValue)
        '            pnlJobtitles.Visible = True
        JobTitleList.DataSource = dt
        JobTitleList.DataValueField = "JobTitleID"
        JobTitleList.DataTextField = "JobTitle"
        JobTitleList.DataBind()
        JobTitleList.Visible = True
        '        RadAjaxManager1.ResponseScripts.Add("returnArg('" & actionString & "');")
    End Sub

#Region "Buttons"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        lblsubmitBenefitsresponse.Visible = False
        Dim locaid As Guid = New Guid(cbLocations.SelectedValue)
        Dim descList As String = String.Empty
        Dim jtl As Telerik.Web.UI.RadListBoxItemCollection
        jtl = LoadDescriptionsList.Items
        Dim dba As New DBAccess()
        dba.CommandText = "DELETE FROM LocationDescription WHERE LocationID = @locaid"
        dba.AddParameter("@locaid", locaid)
        dba.ExecuteNonQuery()

        For Each itm As Telerik.Web.UI.RadListBoxItem In jtl
            dba.CommandText = "INSERT INTO LocationDescription (ID,LocationID, DescriptionID) VALUES (@ID, @LocationID, @DescriptionID)"
            dba.AddParameter("@ID", Guid.NewGuid())
            dba.AddParameter("@LocationID", locaid)
            dba.AddParameter("@DescriptionID", New Guid(itm.Value))
            dba.ExecuteNonQuery()
        Next

        Dim ldal As New locaDAL
        Dim dt As DataTable = ldal.GetLoadDescriptionsByLocationID(cbLocations.SelectedValue)
        '            pnlJobtitles.Visible = True
        LoadDescriptionsList.DataSource = dt
        LoadDescriptionsList.DataValueField = "ID"
        LoadDescriptionsList.DataTextField = "Name"
        LoadDescriptionsList.DataBind()
        LoadDescriptionsList.Visible = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        lblsubmitBenefitsresponse.Visible = False
        Dim locaid As Guid = New Guid(cbLocations.SelectedValue)
        Dim dba As New DBAccess
        Dim ldal As New locaDAL
        Dim dept As String = String.Empty
        Dim vdepartmentList As Telerik.Web.UI.RadListBoxItemCollection
        vdepartmentList = DepartmentList.Items 'list of selected Departments from ui
        Dim dblb As New Telerik.Web.UI.RadListBox 'empty listbox
        Dim dbDepartmentList As New Telerik.Web.UI.RadListBoxItemCollection(dblb) 'empty collection for empty listbox
        Dim dt As DataTable = ldal.GetDepartmentsByLocationID(cbLocations.SelectedValue, True)
        'ID, Name
        For Each row As DataRow In dt.Rows
            'create database jobdescription item
            Dim dbjditem As New RadListBoxItem
            dbjditem.Text = row.Item("Name")
            dbjditem.Value = row.Item("ID").ToString
            dblb.Items.Add(dbjditem) 'list of items from db

            Dim vDepartment As String = row.Item("Name")
            Dim jdlitem As RadListBoxItem = DepartmentList.FindItemByText(vDepartment)

            If Not vdepartmentList.Contains(jdlitem) Then
                dba.CommandText = "DELETE FROM LocationDepartment WHERE LocationID = @locaid and DepartmentID = @DepartmentID"
                dba.AddParameter("@locaid", locaid)
                dba.AddParameter("@DepartmentID", row.Item("ID"))
                Dim suc As Integer = dba.ExecuteNonQuery()
                suc = suc
            End If
        Next

        For Each itm As RadListBoxItem In vdepartmentList
            Dim sitem As RadListBoxItem = dblb.FindItemByText(itm.Text)
            If sitem Is Nothing Then
                dba.CommandText = "INSERT INTO LocationDepartment (id, LocationID, DepartmentID) " & _
                    "VALUES (@id,@LocationID,@DepartmentID)"
                dba.AddParameter("@id", Guid.NewGuid().ToString)
                dba.AddParameter("@LocationID", cbLocations.SelectedValue)
                dba.AddParameter("@DepartmentID", itm.Value)
                Dim suc As Integer = dba.ExecuteNonQuery
                suc = suc
            End If
        Next
        dt = ldal.GetDepartmentsByLocationID(cbLocations.SelectedValue, True)
        '            pnlJobtitles.Visible = True
        DepartmentList.DataSource = dt
        DepartmentList.DataValueField = "ID"
        DepartmentList.DataTextField = "Name"
        DepartmentList.DataBind()
        DepartmentList.Visible = True

        dt = ldal.GetDepartments()
        lbAvailableDepartments.DataSource = dt
        lbAvailableDepartments.DataValueField = "ID"
        lbAvailableDepartments.DataTextField = "Name"
        lbAvailableDepartments.DataBind()
        Dim departments As RadListBoxItemCollection = DepartmentList.Items
        For Each item As RadListBoxItem In departments
            Dim itemToRemove As RadListBoxItem = lbAvailableDepartments.FindItemByText(item.Text)
            lbAvailableDepartments.Items.Remove(itemToRemove)
        Next
        RadGrid2.Rebind()
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        lblsubmitBenefitsresponse.Visible = False


        'dbJobDescriptionList ' list of selected Job Descriptions from database
        'dbJobDescriptions() 'all jobdescritions in database()

        'JobDescriptionList 'list of selected Job Descriptions from ui
        'lbAvailableJobDescriptions ' list of Excluded job descriptions from ui

        'look for each item in jobdescription list
        'if not not f

        Dim locaid As Guid = New Guid(cbLocations.SelectedValue)
        Dim dba As New DBAccess()
        Dim ldal As New locaDAL
        Dim vJobDescriptionList As Telerik.Web.UI.RadListBoxItemCollection
        vJobDescriptionList = JobDescriptionList.Items 'list of selected Job Descriptions from ui
        Dim dblb As New Telerik.Web.UI.RadListBox 'empty listbox
        Dim dbJobDescriptionList As New Telerik.Web.UI.RadListBoxItemCollection(dblb) 'empty collection for empty listbox

        Dim dt As DataTable = ldal.GetJobDescriptionsByLocationID(cbLocations.SelectedValue, True)
        'LocationID, JobDescriptionID, JobDescription OR JobDescriptionID,JobDescription
        For Each row As DataRow In dt.Rows
            'create database jobdescription item
            Dim dbjditem As New RadListBoxItem
            dbjditem.Text = row.Item("jobDescription")
            dbjditem.Value = row.Item("JobDescriptionID").ToString
            dblb.Items.Add(dbjditem) 'list of items from db

            Dim vJobDescription As String = row.Item("JobDescription")
            Dim jdlitem As RadListBoxItem = JobDescriptionList.FindItemByText(vJobDescription)

            If Not vJobDescriptionList.Contains(jdlitem) Then
                dba.CommandText = "DELETE FROM LocationJobDescriptions WHERE LocationID = @locaid and JobDescriptionID = @JobDescriptionID"
                dba.AddParameter("@locaid", locaid)
                dba.AddParameter("@JobDescriptionID", row.Item("JobDescriptionID"))
                Dim suc As Integer = dba.ExecuteNonQuery()
            End If
        Next

        For Each itm As RadListBoxItem In vJobDescriptionList
            Dim sitem As RadListBoxItem = dblb.FindItemByText(itm.Text)

            If sitem Is Nothing Then
                dba.CommandText = "INSERT INTO LocationJobDescriptions (id, LocationID, JobDescriptionID, CustBillingRate) " & _
                    "VALUES (@id,@LocationID,@JobDescriptionID,@CustBillingRate)"
                dba.AddParameter("@id", Guid.NewGuid().ToString)
                dba.AddParameter("@LocationID", cbLocations.SelectedValue)
                dba.AddParameter("@JobDescriptionID", itm.Value)
                dba.AddParameter("@CustBillingRate", 0)
                Dim suc As Integer = dba.ExecuteNonQuery
            End If
        Next
        dt = ldal.GetJobDescriptionsByLocationID(cbLocations.SelectedValue)
        '            pnlJobtitles.Visible = True
        JobDescriptionList.DataSource = dt
        JobDescriptionList.DataValueField = "JobDescriptionID"
        JobDescriptionList.DataTextField = "JobDescription"
        JobDescriptionList.DataBind()
        JobDescriptionList.Visible = True

        dt = ldal.GetJobDescriptions()
        lbAvailableJobDescriptions.DataSource = dt
        lbAvailableJobDescriptions.DataValueField = "JobDescriptionID"
        lbAvailableJobDescriptions.DataTextField = "JobDescription"
        lbAvailableJobDescriptions.DataBind()
        Dim JobDescriptions As RadListBoxItemCollection = JobDescriptionList.Items
        For Each item As RadListBoxItem In JobDescriptions
            Dim itemToRemove As RadListBoxItem = lbAvailableJobDescriptions.FindItemByText(item.Text)
            lbAvailableJobDescriptions.Items.Remove(itemToRemove)
        Next

        RadGrid1.Rebind()
    End Sub

    Private Sub JobDescriptionList_Deleting(sender As Object, e As RadListBoxDeletingEventArgs) Handles JobDescriptionList.Deleting
        If JobDescriptionList.Items.Count = 1 Then
            e.Cancel = True
        End If
    End Sub
#End Region

#Region "RadGrid1"

    Private Sub RadGrid1_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim dataItem As GridDataItem = e.Item
            Dim sOAmount As DataRowView = DirectCast(e.Item.DataItem, DataRowView)
            Dim lblamount As Label = e.Item.FindControl("lblCustBillingRate")
            If Not IsDBNull(sOAmount.Item("CustBillingRate")) Then
                Dim OAmount As Decimal = sOAmount.Item("CustBillingRate")
                lblamount.Text = IIf(OAmount = 0, "---", FormatCurrency(OAmount, 2))
            Else
                lblamount.Text = FormatCurrency(0, 2)
            End If
        End If
    End Sub

    Private Sub RadGrid1_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        If cbLocations.SelectedValue > "" Then
            Dim locaid As String = cbLocations.SelectedValue.ToString
            Dim dba As New DBAccess
            dba.CommandText = "SELECT LocationJobDescriptions.ID, JobDescriptions.JobDescription, LocationJobDescriptions.CustBillingRate " & _
                "FROM LocationJobDescriptions INNER JOIN " & _
                "JobDescriptions ON LocationJobDescriptions.JobDescriptionID = JobDescriptions.ID INNER JOIN " & _
                "Location ON LocationJobDescriptions.LocationID = Location.id " & _
                "WHERE(Location.ID = @locaid) AND (JobDescriptions.IsHourly = 'True') AND (JobDescriptions.IsActive = 'True')"
            dba.AddParameter("@locaid", locaid)
            Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
            RadGrid1.DataSource = dt
        End If
    End Sub

    Private Sub RadGrid1_UpdateCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid1.UpdateCommand
        Dim dt As DataTable = New DataTable
        Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
        Dim datakey As String = editedItem.GetDataKeyValue("ID").ToString
        Dim txtamount As RadNumericTextBox = CType(e.Item.FindControl("numCustBillingRate"), RadNumericTextBox)
        Dim newamount As Decimal = txtamount.Value
        Dim dba As New DBAccess
        dba.CommandText = "UPDATE LocationJobDescriptions SET CustBillingRate=@newamount where id=@datakey"
        dba.AddParameter("@newamount", newamount)
        dba.AddParameter("@datakey", datakey)
        dba.ExecuteNonQuery()
    End Sub
#End Region

    Private Sub RadGrid2_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles RadGrid2.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim dataItem As GridDataItem = e.Item
            Dim drv As DataRowView = DirectCast(e.Item.DataItem, DataRowView)
            Dim lblemail As Label = e.Item.FindControl("lblemail")
            If Not IsDBNull(drv.Item("email")) Then
                Dim email As String = drv.Item("email")
                lblemail.Text = IIf(email = "", "---", email)
            Else
                lblemail.Text = "---"
            End If
            Dim lblemailCC As Label = e.Item.FindControl("lblemailCC")
            If Not IsDBNull(drv.Item("emailCC")) Then
                Dim emailCC As String = drv.Item("emailCC")
                lblemailCC.Text = IIf(emailCC = "", "---", emailCC)
            Else
                lblemailCC.Text = "---"
            End If
        End If
    End Sub



    Private Sub RadGrid2_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGrid2.NeedDataSource
        Dim ldal As New locaDAL
        Dim dt As DataTable
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT LocationDepartment.LocationID, LocationDepartment.DepartmentID, Department.Name AS DeptName, " & _
            "LocationDepartment.email, LocationDepartment.emailCC, Location.hasPics " & _
            "FROM LocationDepartment INNER JOIN " & _
            "Location ON LocationDepartment.LocationID = Location.ID INNER JOIN " & _
            "Department ON LocationDepartment.DepartmentID = Department.ID " & _
            "WHERE (LocationDepartment.LocationID = @locaid)"
        dba.AddParameter("@locaid", cbLocations.SelectedValue)
        dt = dba.ExecuteDataSet.Tables(0)
        RadGrid2.DataSource = dt
    End Sub

    Private Sub RadGrid2_UpdateCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid2.UpdateCommand

        Dim dt As DataTable = New DataTable
        Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
        Dim locaid As String = editedItem.GetDataKeyValue("LocationID").ToString
        Dim Deptid As String = editedItem.GetDataKeyValue("DepartmentID").ToString
        Dim txtemail As RadTextBox = CType(e.Item.FindControl("txtemail"), RadTextBox)
        Dim email As String = txtemail.Text
        Dim txtemailCC As RadTextBox = CType(e.Item.FindControl("txtemailCC"), RadTextBox)
        Dim emailCC As String = txtemailCC.Text
        If email.Trim = "" And emailCC.Trim > "" Then
            email = emailCC.Trim
            emailCC = ""
        End If

        Dim dba As New DBAccess
        dba.CommandText = "UPDATE LocationDepartment set email=@email, emailCC=@emailCC where LocationID=@locaid and DepartmentID=@DeptID"
        dba.AddParameter("@email", email)
        dba.AddParameter("@emailCC", emailCC)
        dba.AddParameter("@locaid", locaid)
        dba.AddParameter("@DeptID", Deptid)
        Dim i As Integer = dba.ExecuteNonQuery()
    End Sub

    Protected Sub rblistEnableSickLeave_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblistEnableSickLeave.SelectedIndexChanged
        Dim enablesickLeave As Boolean = rblistEnableSickLeave.SelectedValue = "Yes"
        Dim tab As RadTab = RadTabStrip1.FindTabByText("Benefit Configuration")
        tab.Visible = enablesickLeave
        Dim tab2 As RadTab = RadTabStrip1.FindTabByText("JobTitles/LoadDescriptions/Departments")
        tab2.Selected = Not enablesickLeave
        RadPageView1.Selected = Not enablesickLeave
        RadPageView4.Selected = enablesickLeave

    End Sub

    Protected Sub LoadBenefits(ByVal locaid As String)
        Dim dba As New DBAccess
        dba.CommandText = "Select LocationID,sickLeaveStart,sickLeaveMaxAccrued,sickLeaveMinPerUse,sickLeaveEligibility," & _
            "sickLeaveMaxUseAnnum,sickLeaveAccrualRate from BenefitsConfiguration WHERE LocationID = @locaid order by sickLeaveStart Desc"
        dba.AddParameter("@locaid", cbLocations.SelectedValue)
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        If Not dt.Rows.Count = 0 Then
            Dim row As DataRow = dt.Rows(0)
            If Not IsDBNull(row.Item("sickLeaveStart")) Then
                dpStartBenefitsDate.SelectedDate = row.Item("sickLeaveStart")
                If dpStartBenefitsDate.SelectedDate = "1/1/1900" Then dpStartBenefitsDate.Clear()
            Else
                dpStartBenefitsDate.Clear()
            End If
            numMaxHours.Value = row.Item("sickLeaveMaxAccrued").ToString
            numEligibleDays.Value = row.Item("sickLeaveEligibility").ToString
            numMaxPerAnnum.Value = row.Item("sickLeaveMaxUseAnnum").ToString
            numMinHours.Value = row.Item("sickLeaveMinPerUse").ToString

        End If

    End Sub

    Protected Sub btnSubmitBenefits_Click(sender As Object, e As EventArgs) Handles btnSubmitBenefits.Click
        Dim locaid As String = cbLocations.SelectedValue
        Dim sickLeaveStart As Date
        If Not dpStartBenefitsDate.SelectedDate Is Nothing Then
            sickLeaveStart = dpStartBenefitsDate.SelectedDate
        Else
            sickLeaveStart = "1/1/1900"
        End If
        Dim sickLeaveMaxAccrued As Integer = numMaxHours.Value
        Dim sickLeaveEligibility As Integer = numEligibleDays.Value
        Dim sickLeaveMaxUseAnnum = numMaxPerAnnum.Value
        Dim sickLeaveMinPerUse As Integer = numMinHours.Value
        Dim sickLeaveAccrualRate As Integer = numAccrualRate.Value
        Dim dba As New DBAccess
        dba.CommandText = "select COUNT(*) from BenefitsConfiguration where LocationID=@locaid"
        dba.AddParameter("@locaid", locaid)
        If dba.ExecuteScalar = 1 Then
            dba.CommandText = "UPDATE BenefitsConfiguration SET sickLeaveStart=@sickLeaveStart, sickLeaveMaxAccrued=@sickLeaveMaxAccrued, sickLeaveEligibility=@sickLeaveEligibility, sickLeaveMaxUseAnnum=@sickLeaveMaxUseAnnum, sickLeaveMinPerUse=@sickLeaveMinPerUse WHERE LocationID = @locaid"
            dba.AddParameter("@locaid", locaid)
            dba.AddParameter("@sickLeaveStart", sickLeaveStart)
            dba.AddParameter("@sickLeaveMaxAccrued", sickLeaveMaxAccrued)
            dba.AddParameter("@sickLeaveMinPerUse", sickLeaveMinPerUse)
            dba.AddParameter("@sickLeaveEligibility", sickLeaveEligibility)
            dba.AddParameter("@sickLeaveMaxUseAnnum", sickLeaveMaxUseAnnum)
            dba.ExecuteNonQuery()
        Else
            dba.CommandText = "INSERT INTO BenefitsConfiguration (LocationID,sickLeaveStart,sickLeaveMaxAccrued,sickLeaveMinPerUse,sickLeaveEligibility,sickLeaveMaxUseAnnum, sickLeaveAccrualRate) " & _
                "VALUES (@locaid,@sickLeaveStart,@sickLeaveMaxAccrued,@sickLeaveMinPerUse,@sickLeaveEligibility,@sickLeaveMaxUseAnnum,@sickLeaveAccrualRate)"
            dba.AddParameter("@locaid", locaid)
            dba.AddParameter("@sickLeaveStart", sickLeaveStart)
            dba.AddParameter("@sickLeaveMaxAccrued", sickLeaveMaxAccrued)
            dba.AddParameter("@sickLeaveMinPerUse", sickLeaveMinPerUse)
            dba.AddParameter("@sickLeaveEligibility", sickLeaveEligibility)
            dba.AddParameter("@sickLeaveMaxUseAnnum", sickLeaveMaxUseAnnum)
            dba.AddParameter("@sickLeaveAccrualRate", sickLeaveAccrualRate)
            dba.ExecuteNonQuery()
        End If
        'dba.CommandText = "UPDATE Location Set EnableBenefits=@EnableBenefits WHERE id=@locaid"
        'dba.AddParameter("@EnableBenefits", True)
        'dba.AddParameter("@locaid", locaid)
        'dba.ExecuteNonQuery()
        'lblsubmitBenefitsresponse.Text = "Config Saved"
        'lblsubmitBenefitsresponse.Visible = True
    End Sub

    Protected Sub lbtnSaveChanges_Click(sender As Object, e As EventArgs) Handles lbtnSaveChanges.Click

    End Sub
End Class