Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class BackhaulInbound
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            LogDate.SelectedDate = Date.Now()
            LogDate.MaxDate = Date.Now()
            '            LogDate.MinDate = DateAdd(DateInterval.Day, -6, Date.Now)
            tpDockTime.SelectedDate = Date.Now()
            tpStartTime.SelectedDate = tpDockTime.SelectedDate
            tpEndTime.SelectedDate = tpDockTime.SelectedDate

            ' set location combobox
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)

            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")

            'If cbLocations.Enabled = False Then
            If cbLocations.SelectedIndex > -1 Then
                pnlDynamicStuff.Visible = True
                populateAvailableLoaders()
            End If
            getDepartments()
            getLoadTypes()
            getDescriptions()
            lbAvailableUnloaders.Enabled = False
            lbUnloaderList.Enabled = False
            lbAvailableUnloaders.ButtonSettings.ShowTransferAll = False
            '            populateAvailableLoaders()

            txtVendorNumber.Attributes("onkeyup") = "decOnly(this);"
            '            SetInitialRow()
            cbIsRounded.Attributes.Add("onclick", "javascript:gimmeTotal();")
            If User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager") Then
                lblVendorNumber.Text = "<span onclick=""openVendor();"" onmouseover=""this.style.cursor='pointer';"" style=""color:blue;font-size:11px;"">Vendor List</span>"
            Else
                lblVendorNumber.Text = "Vendor Number"
            End If
            Dim elst As New List(Of WorkOrder)
            RadGrid1.DataSource = elst
            RadGrid1.DataBind()
            lblRowCount.Text = recordCount(elst.Count)
        End If
        '        spSelectCarrier.Visible = cbLocations.SelectedIndex > -1
    End Sub

    Protected Sub getLocations()
        Dim dt As New DataTable()
        Dim ldal As New locaDAL()
        dt = ldal.getLocations()
        cbLocations.DataSource = dt
        cbLocations.DataTextField = "LocationName"
        cbLocations.DataValueField = "locaID"
        cbLocations.DataBind()
        cbLocations.ClearSelection()
    End Sub

    Protected Sub getDepartments()
        Dim rdba As New DBAccess()
        Dim locadal As New locaDAL
        Dim dt As DataTable = locadal.GetDepartmentsByLocationID(cbLocations.SelectedValue.ToString)
        cbDepartment.DataSource = dt
        cbDepartment.DataValueField = "ID"
        cbDepartment.DataTextField = "Name"
        cbDepartment.DataBind()
        cbDepartment.ClearSelection()
    End Sub

    Protected Sub getLoadTypes()
        Dim rdba As New DBAccess()
        rdba.CommandText = "SELECT ID, Name FROM LoadType WHERE ((Name='Backhaul') OR (Name='Inbound')) ORDER BY Name"
        Dim ds As DataSet = rdba.ExecuteDataSet
        cbLoadType.DataSource = ds.Tables(0)
        cbLoadType.DataValueField = "ID"
        cbLoadType.DataTextField = "Name"
        cbLoadType.DataBind()
        cbLoadType.ClearSelection()
    End Sub

    Protected Sub getDescriptions()
        Dim rdba As New DBAccess()
        Dim ldal As New locaDAL
        Dim dt As DataTable = ldal.GetLoadDescriptionsByLocationID(cbLocations.SelectedValue.ToString)
        cbLoadDescription.DataSource = dt
        cbLoadDescription.DataValueField = "ID"
        cbLoadDescription.DataTextField = "Name"
        cbLoadDescription.DataBind()
        cbLoadDescription.ClearSelection()
    End Sub

    Protected Sub populateAvailableLoaders()
        '        If cbLocations.Text <> "Select Location" Then
        If cbLocations.SelectedIndex > -1 Then
            Dim dba As New DBAccess()
            Dim locaID As Guid = New Guid(cbLocations.SelectedValue)
            Dim edal As New empDAL()
            Dim dt As DataTable = edal.getUnloadersByLocation(locaID)
            lbAvailableUnloaders.DataSource = dt
            lbAvailableUnloaders.DataValueField = "ID"
            lbAvailableUnloaders.DataTextField = "Name"
            lbAvailableUnloaders.DataBind()
            '            Dim uloaders As RadListBoxItemCollection = lbUnloaderList.Items
            '            For Each item As RadListBoxItem In uloaders
            '            Dim itemToRemove As RadListBoxItem = lbAvailableUnloaders.FindItemByText(item.Text)
            '            lbAvailableUnloaders.Items.Remove(itemToRemove)
            '            Next
        Else
            lbAvailableUnloaders.Items.Clear()
        End If
        lbUnloaderList.Items.Clear()
    End Sub

    Protected Sub cbLocations_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        pnlDynamicStuff.Visible = True
        populateAvailableLoaders()
        spSelectCarrier.Visible = cbLocations.SelectedIndex > -1
        getDepartments()

    End Sub

    Protected Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        Dim arg As String = e.Argument
        Dim args() As String
        If arg.Contains("Carrier") Then
            args = Split(e.Argument, ":")
            txtCarrierIDv.Text = args(2)
            htxtCarrierName.Text = args(1)
        ElseIf arg.Contains("VendorLookup") Then
            args = Split(arg, ":")
            Dim locaID As Guid = New Guid(cbLocations.SelectedValue)
            Dim vnum As String = args(1).ToUpper
            If vnum.Length > 2 Then
                Dim dba As New DBAccess()
                dba.CommandText = "SELECT Vendor.ID AS VendorID, Vendor.Number, Vendor.Name AS VendorName " & _
                    "FROM Location INNER JOIN " & _
                    "Vendor ON Location.ParentCompanyID = Vendor.ParentCompanyID " & _
                    "WHERE (Location.ID = @locaID) AND (Vendor.Number = @vNum) "
                dba.AddParameter("@locaID", locaID)
                dba.AddParameter("@vNum", vnum)
                Dim reader As SqlDataReader = dba.ExecuteReader
                If reader.HasRows Then
                    reader.Read()
                    lblVendorName.Text = reader.Item(2)
                    reader.Close()
                    lbAvailableUnloaders.Enabled = True
                    lbUnloaderList.Enabled = True
                    lbUnloaderList.EmptyMessage = "Select Unloaders"
                    lbAvailableUnloaders.ButtonSettings.ShowTransferAll = True

                Else
                    lblVendorName.Text = "not found"
                End If
            Else
                If vnum.Length = 0 Then
                    lblVendorName.Text = ""
                Else
                    lblVendorName.Text = "not found"

                End If
            End If

        ElseIf arg.Contains("NewVendor") Or arg.Contains("UpdateVendor") Or arg.Contains("SelectVendor") Then
            args = Split(arg, ":")
            Dim tbVendorNumber As RadTextBox = CType(FindControl("txtVendorNumber"), RadTextBox)
            tbVendorNumber.Text = args(2)
            lblVendorName.Text = args(2) & " - " & args(3)
            lbAvailableUnloaders.Enabled = True
            lbUnloaderList.Enabled = True
            lbUnloaderList.EmptyMessage = "Select Unloaders"
            lbAvailableUnloaders.ButtonSettings.ShowTransferAll = True
        End If
    End Sub

    Private Sub tpDockTime_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles tpDockTime.SelectedDateChanged
        tpStartTime.MinDate = tpDockTime.SelectedDate
        tpEndTime.MinDate = tpDockTime.SelectedDate
        tpStartTime.SelectedDate = tpDockTime.SelectedDate
        tpEndTime.SelectedDate = tpDockTime.SelectedDate

    End Sub

    Private Sub tpStartTime_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles tpStartTime.SelectedDateChanged
        tpEndTime.MinDate = tpStartTime.SelectedDate
        tpEndTime.SelectedDate = tpStartTime.SelectedDate
    End Sub

    Private Sub ButtonAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAdd.Click
        Dim wo As New WorkOrder()
        wo.Status = 192
        wo.LogDate = FormatDateTime(LogDate.SelectedDate, DateFormat.ShortDate)
        wo.LogNumber = -1
        wo.LocationID = New Guid(cbLocations.SelectedValue)
        Dim wodal As New WorkOrderDAL()
        wo.LoadNumber = wodal.nextLoadNumber(LogDate.SelectedDate)
        If cbDepartment.SelectedIndex > -1 Then
            wo.DepartmentID = New Guid(cbDepartment.SelectedValue)
        End If
        If cbLoadType.SelectedIndex > -1 Then
            wo.LoadTypeID = New Guid(cbLoadType.SelectedValue)
        End If
        wo.VendorNumber = txtVendorNumber.Text.Trim()
        wo.CustomerID = wodal.getCustomerID(wo.LocationID, wo.VendorNumber)
        wo.ReceiptNumber = -1
        wo.PurchaseOrder = txtPONumber.Text.Trim()
        wo.Amount = IIf(txtAmount.Value Is Nothing, 0, txtAmount.Value)
        '        wo.IsCash = False
        If cbLoadDescription.SelectedIndex > -1 Then
            wo.LoadDescriptionID = New Guid(cbLoadDescription.SelectedValue)
        End If
        If txtCarrierIDv.Text.Length = 36 Then
            wo.CarrierID = New Guid(txtCarrierIDv.Text)
        End If
        Dim vTruckNumber As String = txtTruckNumber.Text.Trim()
        wo.TruckNumber = IIf(vTruckNumber = "", "DROP", vTruckNumber)
        wo.TrailerNumber = txtTrailerNumber.Text.Trim()
        wo.AppointmentTime = "1/1/1900"
        wo.GateTime = "1/1/1900"
        wo.DockTime = wo.LogDate & " " & FormatDateTime(tpDockTime.SelectedDate, DateFormat.ShortTime)
        wo.StartTime = wo.LogDate & " " & FormatDateTime(tpStartTime.SelectedDate, DateFormat.ShortTime)
        wo.CompTime = wo.LogDate & " " & FormatDateTime(tpEndTime.SelectedDate, DateFormat.ShortTime)
        If Format(wo.StartTime, "Short Date") = Format(wo.CompTime, "Short Date") Then
            If wo.StartTime > wo.CompTime Then wo.CompTime = DateAdd(DateInterval.Day, 1, wo.CompTime)
        End If

        wo.TTLTime = DateDiff(DateInterval.Minute, wo.StartTime, wo.CompTime)
        wo.PalletsUnloaded = IIf(txtPalletsUnloaded.Value Is Nothing, 0, txtPalletsUnloaded.Value)
        wo.DoorNumber = txtDoorNumber.Text.Trim()
        wo.Pieces = IIf(txtPieces.Value Is Nothing, 0, txtPieces.Value)
        wo.Weight = IIf(txtWeight.Value Is Nothing, 0, txtWeight.Value)
        wo.Comments = txtComments.Text.Trim()
        wo.Restacks = IIf(txtRestacks.Value Is Nothing, 0, txtRestacks.Value)
        wo.PalletsReceived = IIf(txtPalletsReceived.Value Is Nothing, 0, txtPalletsReceived.Value)
        wo.BadPallets = IIf(txtBadPallets.Value Is Nothing, 0, txtBadPallets.Value)
        wo.NumberOfItems = IIf(txtNumberItems.Value Is Nothing, 0, txtNumberItems.Value)
        wo.CheckNumber = ""
        wo.BOL = ""
        wo.ID = Guid.NewGuid()
        Dim emlst As List(Of String) = New List(Of String)
        Dim unlcount As Integer = lbUnloaderList.Items.Count

        For Each it As RadListBoxItem In lbUnloaderList.Items
            emlst.Add(it.Value)
        Next
        wo.Employee = emlst
        wodal.AddWorkOrder(wo)
        Dim lst As List(Of String) = New List(Of String)
        If Not ViewState("BatchList") Is Nothing Then
            lst = ViewState("BatchList")    'read into lst
            Dim li As ListItem = New ListItem()
            li.Text = wo.ID.ToString
            lst.Add(wo.ID.ToString)
            ViewState("BatchList") = lst
        Else
            lst.Add(wo.ID.ToString)
            ViewState("BatchList") = lst
            LockStaticForm()
        End If
        Dim wdal As New WorkOrderDAL()
        Dim elst As New List(Of WorkOrder)
        For Each s As String In lst
            Dim wxo As WorkOrder = wdal.GetLoadByID(s)
            elst.Add(wxo)
        Next
        RadGrid1.DataSource = elst
        RadGrid1.DataBind()
        btnNewBatch.Visible = True
        lblRowCount.Text = recordCount(elst.Count)
        clearDynamicForm()

    End Sub

    Protected Sub LockStaticForm()
        LogDate.Enabled = False
        cbLocations.Enabled = False
        cbDepartment.Enabled = False
        cbLoadType.Enabled = False
        cbLoadDescription.Enabled = False
        txtDoorNumber.Enabled = False
        lblCarrierNamev.Text = htxtCarrierName.Text
        spSelectCarrier.Visible = False
        txtTruckNumber.Enabled = False
        txtTrailerNumber.Enabled = False
        tpDockTime.Enabled = False
        tpStartTime.Enabled = False
        tpEndTime.Enabled = False
    End Sub

    Protected Sub clearStaticForm()
        LogDate.SelectedDate = Date.Now()
        '        cbLocations.SelectedValue = ""
        cbDepartment.ClearSelection()
        cbLoadType.ClearSelection()
        cbLoadDescription.ClearSelection()
        txtDoorNumber.Text = ""
        'cbCarrier.ClearSelection()
        txtTruckNumber.Text = ""
        txtTrailerNumber.Text = ""
        tpDockTime.SelectedDate = Date.Now()
        tpStartTime.SelectedDate = Date.Now()
        tpEndTime.SelectedDate = Date.Now()
    End Sub

    Protected Sub clearDynamicForm()
        txtPONumber.Text = ""
        txtVendorNumber.Text = ""
        lblVendorName.Text = ""
        txtPalletsUnloaded.Text = ""
        txtWeight.Text = ""
        txtPieces.Text = ""
        txtPalletsReceived.Text = ""
        txtRestacks.Text = ""
        txtBadPallets.Text = ""
        txtNumberItems.Text = ""
        txtAmount.Text = ""
        txtComments.Text = ""
        txtPONumber.Focus()
    End Sub

    Private Sub btnNewBatch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewBatch.Click
        Response.Redirect("BackhaulInbound.aspx")
    End Sub

    Private Sub RadGrid1_DeleteCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.DeleteCommand
        Dim woid As String = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()

        Dim lst As List(Of String) = New List(Of String)

        lst = ViewState("BatchList")    'read into lst
        lst.Remove(woid)
        ViewState("BatchList") = lst
        Dim dba As New DBAccess()
        dba.CommandText = "DELETE FROM WorkOrder WHERE ID = @woid"
        dba.AddParameter("@woid", woid)
        dba.ExecuteNonQuery()
        dba.CommandText = "DELETE FROM Unloader WHERE LoadID = @woid"
        dba.AddParameter("@woid", woid)
        dba.ExecuteNonQuery()

        Dim wdal As New WorkOrderDAL()
        Dim elst As New List(Of WorkOrder)
        For Each s As String In lst
            Dim wxo As WorkOrder = wdal.GetLoadByID(s)
            elst.Add(wxo)
        Next
        RadGrid1.DataSource = elst
        RadGrid1.DataBind()
        lblRowCount.Text = recordCount(elst.Count)
    End Sub

    Public Function recordCount(ByVal recs As Integer) As String
        lblRowCount.Visible = recs > 0
        If recs = 1 Then
            recordCount = recs & " record"
        Else
            recordCount = recs & " records"
        End If
        Return recordCount
    End Function


End Class
