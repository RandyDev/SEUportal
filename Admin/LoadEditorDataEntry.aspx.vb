Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class LoadEditorDataEntry
    Inherits System.Web.UI.Page
    Dim surNull As Date = CDate("1/1/1900 12:00 AM")
    <Flags()> _
    Enum LoadStatus     'track load status via bitwise operations
        is_done = finished
        todo = Undefined
        Undefined = 0
        CheckedIn = 1
        Assigned = 2
        Printed = 4
        AddDataChanged = 8
        Complete = 64
        finished = 128
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblChangesSaved.Visible = False
        If Not Page.IsPostBack Then
            lbldpStartDate.Text = Format(Date.Now, "dd-MMM-yyyy  ddd")
            ' populate Locations Combo Box (cbLocations on aspx page)
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
            txtVendorNumber.Attributes("onkeyup") = "vendorLookup(this);"
            RadGrid1.Rebind()
            pnlWOedit.Visible = False
            pnlWOinfo.Visible = False
            If cbLocations.Items.Count > 1 Then
                lblEmptyMessage.Text = "<<<--- Select a Location"
            End If
            lblEmptyMessage.Visible = True
        End If
    End Sub 'Page_Load

    Protected Sub ReloadForms(ByVal woid As String)
        Dim surNull As Date = "1/1/1900 12:00"
        lblChangesSaved.Visible = False
        editlblCheckNumber.Visible = True
        editlblCheckNumber.Text = "Check/Transaction #"
        lblEmptyMessage.Visible = False
        '        Dim woid As String = RadGrid1.SelectedValue.ToString
        Dim wodal As New WorkOrderDAL()
        Dim edal As New empDAL()
        Dim wo As WorkOrder = wodal.GetLoadByID(woid)
        If wo.Status > 73 Then 'Not Printed
            pnlWOinfo.Visible = True
            pnlWOedit.Visible = False
            txt74comments.Visible = True
            lblComments.Visible = False
        Else
            pnlWOinfo.Visible = False
            pnlWOedit.Visible = True
        End If
        If wo.Status = 74 Then btnPrintWO.Text = "Save/Print Receipt"
        If wo.Status = 76 Then btnPrintWO.Text = "Re-Print Receipt"
        If wo.Status = 78 Then btnPrintWO.Text = "Re-Print Receipt"
        If wo.Status > 78 Then
            btnPrintWO.Text = "LoadComplete"
            btnPrintWO.Enabled = False

        End If
        '****************************
        'HEADER***********************************
        '****************************
        lblCreatedBy.Visible = True
        'lblCreatedBy.Text = wo.CreatedBy
        If Not wo.CreatedBy Is Nothing Then
            lblCreatedBy.Text = IIf(wo.CreatedBy.Contains(":"), wo.CreatedBy, "rtdsHandHeld")
        End If
        '****************************
        'SECTION ONE***********************************
        '****************************

        lblDateWorked.Text = Format(wo.LogDate, "M/d/yyyy")
        lblLoadNumber.Text = wo.LoadNumber
        lblDoorNumber.Text = wo.DoorNumber
        If Utilities.IsValidGuid(wo.CarrierID.ToString) Then
            cbCarrier.SelectedValue = wo.CarrierID.ToString
            cbCarrier.Text = wo.CarrierName
            lblCarrierName.Text = wo.CarrierName
        Else
            cbCarrier.SelectedIndex = -1
        End If
        lblTruckNumber.Text = wo.TruckNumber
        lblTrailerNumber.Text = wo.TrailerNumber
        lblPurchaseOrder.Text = wo.PurchaseOrder
        lblDepartment.Text = wo.Department
        '*******EDIT ONE***********
        lbldpDateWorked.Text = wo.LogDate.ToShortDateString
        txtLoadNumber.Text = wo.LoadNumber
        txtLoadNumber.EmptyMessage = wo.LoadNumber
        txtDoorNumber.Text = wo.DoorNumber
        txtTruckNumber.Text = wo.TruckNumber
        txtTrailerNumber.Text = wo.TrailerNumber
        txtPurchaseOrder.Text = wo.PurchaseOrder
        Dim rdba As New DBAccess()
        Dim ldal As New locaDAL
        loadDepartments()
        cbDepartment.SelectedValue = wo.DepartmentID.ToString
        '****************************
        'SECTION TWO***********************************
        '****************************
        lblVendorNumber.Text = wo.VendorNumber
        lblVendorName.Text = wo.VendorName
        lblPieces.Text = wo.Pieces
        lblPalletsUnloaded.Text = wo.PalletsUnloaded
        lblLoadDescription.Text = wo.LoadDescription
        lblUnloadersV.Text = IIf(edal.GetUnloadersByWOIDString(woid) = "<center>- - -</center>", "None Selected", edal.GetUnloadersByWOIDString(woid))
        lblEditUnloaders.Text = "<span style=""font-size:11px;color:blue"" onmouseover=""this.style.cursor='pointer'"" onclick=""openUnloaders('" & wo.ID.ToString & "');"">Edit Unloaders</span>"
        lblEditUnloaders.Visible = wo.DockTime > surNull
        '*******EDIT TWO***********
        txtVendorNumber.Text = wo.VendorNumber
        txtVendorNumber.EmptyMessage = "---"
        txtVendorName.Text = wo.VendorNumber & " - " & wo.VendorName
        vid.Text = wo.LocationID.ToString
        If wo.PalletsUnloaded > 0 Then
            txtPalletsUnloaded.Text = wo.PalletsUnloaded
        Else
            txtPalletsUnloaded.Text = Nothing
            txtPalletsUnloaded.EmptyMessage = "---"
        End If
        If wo.Pieces > 0 Then
            txtPieces.Text = wo.Pieces
        Else
            txtPieces.Text = Nothing
            txtPieces.EmptyMessage = "---"
        End If
        Dim dt As DataTable = ldal.GetLoadDescriptionsByLocationID(cbLocations.SelectedValue.ToString)
        cbLoadDescription.DataSource = dt
        cbLoadDescription.DataValueField = "ID"
        cbLoadDescription.DataTextField = "Name"
        cbLoadDescription.DataBind()
        cbLoadDescription.ClearSelection()
        cbLoadDescription.Text = ""
        cbLoadDescription.SelectedValue = wo.LoadDescriptionID.ToString
        txtUnloaderIDlist.Text = "nc"
        If edal.GetUnloadersByWOID(wo.ID.ToString).Rows.Count > 0 Then
            txtUnloaders.ForeColor = Drawing.Color.Black
        End If
        txtUnloaders.Text = IIf(edal.GetUnloadersByWOIDString(woid) = "<center>- - -</center>", "None Selected", edal.GetUnloadersByWOIDString(woid))
        '****************************
        'SECTION THREE***********************************
        '****************************
        lblPalletsReceived.Text = wo.PalletsReceived
        lblAppTime.Text = IIf(DatePart(DateInterval.Second, wo.AppointmentTime) > 0, Format(wo.AppointmentTime, "hh:mm:ss tt"), "---")
        lblGateTime.Text = IIf(wo.GateTime > surNull, Format(wo.GateTime, "hh:mm:ss tt"), "---")
        lblArrivalTime.Text = IIf(wo.DockTime > surNull, Format(wo.DockTime, "hh:mm:ss tt"), "---")
        lblStartTime.Text = IIf(wo.StartTime > surNull, Format(wo.StartTime, "hh:mm:ss tt"), "---")
        lbltxtStartTime.Text = IIf(wo.StartTime > surNull, Format(wo.StartTime, "hh:mm:ss tt"), "---")
        txtStartTime.Text = IIf(wo.StartTime > surNull, Format(wo.StartTime, "hh:mm:ss tt"), "---")
        lblCompTime.Text = IIf(wo.CompTime > surNull, Format(wo.CompTime, "hh:mm:ss tt"), "---")
        lbltxtCompTime.Text = IIf(wo.CompTime > surNull, Format(wo.CompTime, "hh:mm:ss tt"), "---")
        txtCompTime.Text = IIf(wo.CompTime > surNull, Format(wo.CompTime, "hh:mm:ss tt"), "---")
        'Total time ******************************
        Dim dif As Integer = 0
        If wo.StartTime > surNull Then
            If wo.CompTime > wo.StartTime Then
                lblEditTotalTimeLabel.Text = "Total Time"
                dif = DateDiff(DateInterval.Minute, wo.StartTime, wo.CompTime)
            Else
                lblEditTotalTimeLabel.Text = "in progress"
                dif = DateDiff(DateInterval.Minute, wo.StartTime, Date.Now)
            End If
            If dif > -1 Then
                If dif > 120 Then
                    Dim difhrs As Integer = dif / 60
                    Dim difmins As Integer = dif Mod 60
                    lblTotalTime.Text = difhrs.ToString & " <font size=""1"">hrs</font>" & " " & difmins.ToString & " <font size=""1"">min</font>"
                    lblEditTotalTime.Text = lblTotalTime.Text
                    lblTotalTime.ForeColor = Drawing.Color.Red
                    lblEditTotalTime.ForeColor = Drawing.Color.Red
                    lblEditTotalTimeLabel.ForeColor = Drawing.Color.Red
                ElseIf dif >= 60 Then
                    Dim difhrs As Integer = dif / 60
                    Dim difmins As Integer = dif Mod 60
                    lblTotalTime.ForeColor = Drawing.Color.OrangeRed
                    lblEditTotalTimeLabel.ForeColor = Drawing.Color.Black
                    lblEditTotalTime.ForeColor = Drawing.Color.OrangeRed
                    lblTotalTime.Text = difhrs.ToString & " <font size=""1"">hr</font>" & " " & difmins.ToString & " <font size=""1"">min</font>"
                    lblEditTotalTime.Text = lblTotalTime.Text
                Else
                    lblTotalTime.Text = dif.ToString & " <font size=""1"">min</font>"
                    lblEditTotalTime.Text = lblTotalTime.Text
                    lblTotalTime.ForeColor = Drawing.Color.Blue
                    lblEditTotalTimeLabel.ForeColor = Drawing.Color.Black
                    lblEditTotalTime.ForeColor = Drawing.Color.Blue
                    lblEditTotalTime.Text = lblTotalTime.Text
                End If
            End If
        Else
            lblTotalTime.Text = "- - -"
            lblEditTotalTime.Text = lblTotalTime.Text
            lblTotalTime.ForeColor = Drawing.Color.Black
            lblEditTotalTimeLabel.ForeColor = Drawing.Color.Black
            lblEditTotalTime.ForeColor = Drawing.Color.Black
        End If
        lblBadPallets.Text = wo.BadPallets
        lblWeight.Text = wo.Weight
        lblRestacks.Text = wo.Restacks
        lblTotalItems.Text = wo.NumberOfItems
        lblLoadType.Text = wo.LoadType
        '*******EDIT THREE***********
        If wo.PalletsReceived > 0 Then
            txtPalletsReceived.Text = wo.PalletsReceived
        Else
            txtPalletsReceived.Text = Nothing
            txtPalletsReceived.EmptyMessage = "---"
        End If
        If wo.PalletsReceived < 1 Then txtPalletsReceived.Text = Nothing
        txtPalletsReceived.EmptyMessage = "---"

        If wo.AppointmentTime > surNull Then
            txtAppTime.SelectedDate = wo.AppointmentTime
        Else
            txtAppTime.Clear()
            txtAppTime.ToolTip = "Optional Appointment Time"
        End If
        If wo.GateTime > surNull Then
            lbltxtGateTime.Text = wo.GateTime.ToShortTimeString
        Else
            lbltxtGateTime.Text = "n/a"
        End If
        If wo.DockTime > surNull Then
            lbltxtArrivalTime.Text = wo.DockTime.ToShortTimeString
        Else
            lbltxtArrivalTime.Text = "---"
        End If
        If wo.StartTime > surNull Then
            lbltxtStartTime.Text = wo.StartTime.ToShortTimeString
        Else
            lbltxtStartTime.Text = "---"
        End If
        If wo.CompTime > surNull Then
            lbltxtCompTime.Text = wo.CompTime.ToShortTimeString
        Else
            lbltxtCompTime.Text = "---"
        End If
        txtBadPallets.Text = wo.BadPallets.ToString
        If wo.BadPallets = -1 Then
            txtBadPallets.Text = ""
            txtBadPallets.EmptyMessage = "---"
            txtBadPallets.Enabled = wo.StartTime > surNull
        End If
        txtWeight.Text = wo.Weight
        If wo.Weight = -1 Then
            txtWeight.Text = String.Empty
            txtWeight.EmptyMessage = "---"
        End If
        txtRestacks.Text = wo.Restacks.ToString
        If wo.Restacks = -1 Then
            txtRestacks.Text = ""
            txtRestacks.EmptyMessage = "---"
            txtRestacks.Enabled = wo.StartTime > surNull
        End If
        txtTotalItems.Text = wo.NumberOfItems
        If wo.NumberOfItems = -1 Then
            txtTotalItems.Text = String.Empty
            txtTotalItems.EmptyMessage = "---"
        End If
        rdba.CommandText = "SELECT ID, Name FROM LoadType ORDER BY Name"
        Dim ds As DataSet = rdba.ExecuteDataSet
        cbLoadType.DataSource = ds.Tables(0)
        cbLoadType.DataValueField = "ID"
        cbLoadType.DataTextField = "Name"
        cbLoadType.DataBind()
        cbLoadType.ClearSelection()
        cbLoadType.SelectedIndex = -1
        cbLoadType.Text = ""
        cbLoadType.SelectedValue = wo.LoadTypeID.ToString
        setstuff()
        '****************************
        'SECTION FOUR***********************************
        '****************************


        If lblLoadType.Text = "Credit Card" Then
            rbgroup74.SelectedIndex = 2
            lblINFOCheckNumber.Text = "Transaction Number"
            rbgroup74.Items(0).Enabled = False
            rbgroup74.Items(1).Enabled = False
            rbgroup74.Items(3).Enabled = False
        Else
            rbgroup74.Items(2).Enabled = False 'disable credit card button
            If wo.CheckNumber > "" Or wo.PaymentType = "Check" Then
                rbgroup.SelectedIndex = 1
                rbgroup74.SelectedIndex = 1
                editlblCheckNumber.Visible = True
                lblINFOCheckNumber.Text = "Check Number"
                txt74CheckNumber.Text = wo.CheckNumber
                txt74CheckNumber.Visible = True
            End If
            If wo.SplitPaymentAmount > 0 Or wo.PaymentType = "Split" Then
                rbgroup.SelectedIndex = 3
                rbgroup74.SelectedIndex = 3
                lblINFOSplitAmount.Visible = True
                txt74SplitAmount.Visible = True
                txt74SplitAmount.Text = wo.SplitPaymentAmount.ToString
                txt74CheckNumber.Visible = True
                editlblCheckNumber.Visible = True
                lblINFOCheckNumber.Visible = True
                txt74CheckNumber.Text = wo.CheckNumber
            End If
        End If
        If wo.SplitPaymentAmount > 0 Or wo.PaymentType = "Split" Then
            rbgroup74.SelectedIndex = 3
            txt74SplitAmount.Visible = True
            lblINFOSplitAmount.Visible = True
        Else
            txt74SplitAmount.Visible = False
            lblINFOSplitAmount.Visible = False
        End If
        '        rbgroup74.Visible = wo.Status < 78
        ' lblchecknumber.Text = wo.CheckNumber
        txtCheckNumber.Text = wo.CheckNumber
        txt74CheckNumber.Text = wo.CheckNumber
        txxtAmount.Value = FormatCurrency(wo.Amount, 2)
        txt74Amount.Value = FormatCurrency(wo.Amount, 2)
        lblAmount.Text = FormatCurrency(wo.Amount, 2)
        txtSplitAmount.Value = FormatCurrency(wo.SplitPaymentAmount, 2)
        txt74SplitAmount.Value = FormatCurrency(wo.SplitPaymentAmount, 2)
        lblSplitAmount.Text = FormatCurrency(wo.SplitPaymentAmount, 2)
        lblBOL.Text = wo.BOL
        lblComments.Text = wo.Comments
        txtComments.Text = wo.Comments
        txt74comments.Text = wo.Comments
        '*******EDIT FOUR***********
        If cbLoadType.SelectedIndex > -1 Then
            Select Case cbLoadType.SelectedItem.Text
                Case "Cash"
                    wo.LoadTypeID = New Guid(cbLoadType.SelectedValue)
                    wo.PaymentType = "Cash"
                    wo.IsCash = True
                    rbgroup.SelectedIndex = 0
                    rbgroup.Items(0).Enabled = True
                    rbgroup.Items(1).Enabled = True
                    rbgroup.Items(2).Enabled = False
                    rbgroup.Items(3).Enabled = True
                    rbgroup74.SelectedIndex = 0
                    rbgroup74.Items(0).Enabled = True
                    rbgroup74.Items(1).Enabled = True
                    rbgroup74.Items(2).Enabled = False
                    rbgroup74.Items(3).Enabled = True
                Case "Credit Card"
                    wo.PaymentType = "Card"
                    rbgroup.SelectedIndex = 2
                    rbgroup.Items(0).Enabled = False
                    rbgroup.Items(1).Enabled = False
                    rbgroup.Items(2).Enabled = True
                    rbgroup.Items(3).Enabled = False
                    rbgroup74.SelectedIndex = 2
                    rbgroup74.Items(0).Enabled = False
                    rbgroup74.Items(1).Enabled = False
                    rbgroup74.Items(2).Enabled = True
                    rbgroup74.Items(3).Enabled = False
            End Select
        End If

        '        rbgroup.Enabled = cbLoadType.SelectedIndex > -1
        txxtAmount.Enabled = cbLoadType.SelectedIndex > -1
        txxtAmount.Text = wo.Amount.ToString
        txt74Amount.Text = txxtAmount.Text
        If wo.Amount = 0 Then
            txxtAmount.Text = ""
            txxtAmount.EmptyMessage = "---"
            txt74Amount.Text = ""
            txt74Amount.EmptyMessage = "---"
        End If
        txtSplitAmount.Enabled = cbLoadType.SelectedIndex > -1
        txtSplitAmount.Text = IIf(wo.SplitPaymentAmount < 1, 0, wo.SplitPaymentAmount)
        If wo.SplitPaymentAmount = 0 Then
            txtSplitAmount.Text = ""
            txtSplitAmount.EmptyMessage = "---"
            txt74SplitAmount.Text = ""
            txt74SplitAmount.EmptyMessage = "---"
        Else
            txtSplitAmount.Visible = True
            lblAddCash.Visible = True
            rbgroup.SelectedIndex = 3
            rbgroup74.SelectedIndex = 3
        End If
        txtCheckNumber.Enabled = cbLoadType.SelectedIndex > -1
        txtCheckNumber.Text = wo.CheckNumber
        txtCheckNumber.EmptyMessage = "---"
        If wo.CheckNumber > "" Then
            txtCheckNumber.Visible = True
        End If
        If wodal.isLoadLocked(woid) And wo.LoadType = "Cash" Then
            txxtAmount.Enabled = False
            cbLoadType.Enabled = False
            txtSplitAmount.Enabled = False
            'no let everyone in
        End If
        If User.IsInRole("Administrator") Or User.IsInRole("SysOp") Then
            txxtAmount.Enabled = True
            cbLoadType.Enabled = True
            txtSplitAmount.Enabled = True
        End If
        txtBOL.Text = wo.BOL
        txtComments.Text = wo.Comments
        If wo.Status = 74 Then
            txt74CheckNumber.Visible = True
            lblAmount.Visible = False
            txt74Amount.Visible = True
            txt74SplitAmount.Visible = True
            lblSplitAmount.Visible = Not txt74SplitAmount.Visible
            txt74comments.Visible = True
            lblComments.Visible = False
        End If
        If wo.Status >= 76 Then
            If wo.PaymentType = "Check" Or wo.PaymentType = "Split" Then
                txt74CheckNumber.Visible = True
            End If

            lblAmount.Visible = True
            txt74Amount.Visible = False
            lblSplitAmount.Visible = Not txt74SplitAmount.Visible
            txt74SplitAmount.Visible = wo.SplitPaymentAmount > 0 Or rbgroup.SelectedIndex = 3
            lblComments.Visible = False
            txt74comments.Visible = True
        End If
        '****************************
        'BUTTONS**********************************
        '****************************
        '        btnclickit.Visible = Not wo.isClosed And (User.IsInRole("SysOp") Or User.IsInRole("Administrator") Or User.IsInRole("Manager"))
        lblLoadImages.Text = "<span style=""font-size:11px;color:blue"" onmouseover=""this.style.cursor='pointer'"" onclick=""openLoadImages('" & wo.ID.ToString & "');"">Manage Pictures</span>"
        lblEditLoadImages.Text = lblLoadImages.Text
        btnPrintWO.CommandArgument = wo.ID.ToString
        '*******EDIT BUTTONS***********
        btnClearForm.Text = "Add New"
        btnClearForm.Visible = True
        btnSaveChanges.Text = "Save Changes"
        btnSaveChanges.CommandName = "Update"
        btnSaveChanges.CommandArgument = wo.ID.ToString
        '        btnPrintWO.OnClientClick = "OpenWinDupReceipts('" & wo.ID.ToString & "')"
        btnCancel.Text = "Cancel - NO SAVE"
    End Sub         ' ************* ReloadForms(woid as string) 

    Private Sub clearForm()
        clearerrors()
        'SECTION Header***********************************
        lblerr.Visible = False
        lblEmptyMessage.Visible = False
        lbldpDateWorked.Text = Format(Date.Now, "ddd dd-MMM-yyyy")
        lblCreatedBy.Visible = False
        'SECTION ONE***********************************
        txtLoadNumber.Text = ""
        Dim wdal As New WorkOrderDAL
        txtLoadNumber.EmptyMessage = wdal.nextLoadNumber(Date.Now)
        txtDoorNumber.Text = ""
        txtDoorNumber.EmptyMessage = "---"
        Dim ldal As New locaDAL
        lblCarrierName.Text = "---"
        cbCarrier.ClearSelection()
        cbCarrier.SelectedValue = ""
        cbCarrier.Text = Nothing
        txtTruckNumber.Text = ""
        txtTruckNumber.Text = Nothing
        txtTruckNumber.EmptyMessage = "---"
        txtTrailerNumber.Text = ""
        txtTrailerNumber.Text = Nothing
        txtTrailerNumber.EmptyMessage = "---"
        txtPurchaseOrder.Text = ""
        txtPurchaseOrder.Text = Nothing
        txtPurchaseOrder.EmptyMessage = "---"
        loadDepartments()
        cbDepartment.ClearSelection()
        cbDepartment.SelectedValue = Nothing
        'SECTION TWO***********************************
        txtVendorName.Text = "---"
        txtVendorNumber.Text = ""
        txtVendorNumber.Text = Nothing

        txtVendorNumber.EmptyMessage = "---"
        txtPieces.Text = Nothing
        txtPieces.EmptyMessage = "---"
        txtPalletsReceived.Text = Nothing
        txtPalletsReceived.EmptyMessage = "---"
        Dim dt As DataTable = ldal.GetLoadDescriptionsByLocationID(cbLocations.SelectedValue.ToString)
        cbLoadDescription.DataSource = dt
        cbLoadDescription.DataValueField = "ID"
        cbLoadDescription.DataTextField = "Name"
        cbLoadDescription.DataBind()
        cbLoadDescription.ClearSelection()
        cbLoadDescription.Text = ""
        lblEditUnloaders.Text = "<span style=""font-size:11px;color:blue"" onmouseover=""this.style.cursor='pointer'"" onclick=""openUnloaders('" & cbLocations.SelectedValue.ToString & "&gtype=locaID" & "');""><u>Select Unloaders</u></span>"
        txtUnloaders.Text = "None Selected"
        txtUnloaderIDlist.Text = ""
        lblEditUnloaders.Visible = False
        'SECTION THREE***********************************
        txtPalletsUnloaded.Text = Nothing
        txtPalletsUnloaded.EmptyMessage = "---"
        txtAppTime.Clear()
        lbltxtGateTime.Text = "---"
        lbltxtArrivalTime.Text = "---"
        lblArrivalTime.Text = "---"
        lbltxtStartTime.Text = "---"
        txtCompTime.Text = ""
        lbltxtCompTime.Text = "---"
        lblTotalTime.Text = "---"
        lblEditTotalTimeLabel.Text = "Total Time"
        lblEditTotalTime.Text = "---"
        txtBadPallets.Text = Nothing
        txtBadPallets.EmptyMessage = "---"
        txtBadPallets.Enabled = False
        txtWeight.Text = Nothing
        txtWeight.EmptyMessage = "---"
        txtRestacks.Text = Nothing
        txtRestacks.EmptyMessage = "--"
        txtRestacks.Enabled = False
        txtTotalItems.Text = Nothing
        txtTotalItems.EmptyMessage = "--"
        Dim rdba As New DBAccess
        rdba.CommandText = "SELECT ID, Name FROM LoadType ORDER BY Name"
        Dim ds As DataSet = rdba.ExecuteDataSet
        cbLoadType.DataSource = ds.Tables(0)
        cbLoadType.DataValueField = "ID"
        cbLoadType.DataTextField = "Name"
        cbLoadType.DataBind()
        cbLoadType.ClearSelection()
        cbLoadType.SelectedIndex = -1
        cbLoadType.Text = ""
        'SECTION FOUR***********************************
        rbgroup.Enabled = cbLoadType.SelectedIndex > -1
        txxtAmount.Enabled = cbLoadType.SelectedIndex > -1
        txtSplitAmount.Enabled = cbLoadType.SelectedIndex > -1
        txtCheckNumber.Enabled = cbLoadType.SelectedIndex > -1
        rbgroup.Enabled = cbLoadType.SelectedIndex > -1
        rbgroup.SelectedIndex = -1
        rbgroup.SelectedIndex = -1
        txxtAmount.Text = 0
        txtCheckNumber.Text = ""
        txtSplitAmount.Value = 0
        txtBOL.Text = ""
        txtComments.Text = ""
        'SECTION Footer***********************************
        lblChangesSaved.Visible = False
        vid.Text = cbLocations.SelectedValue.ToString
        '        btnCancel
    End Sub     'clearForm()

    Private Sub clearerrors()
        Dim retstr As Boolean = False
        lblerr.Visible = False
        errDoor.Visible = False
        errCarrier.Visible = False
        errTruck.Visible = False
        errTrailer.Visible = False
        errPurchaseOrder.Visible = False
        errDepartment.Visible = False
        lblerr.Visible = False
        lblerr.Visible = False
        errVendorNumber.Visible = False
        errPieces.Visible = False
        errPalletsUnloaded.Visible = False
        errLoadDescription.Visible = False
        errunloaders.Visible = False
        errPalletsReceived.Visible = False
        errBadPallets.Visible = False
        errWeight.Visible = False
        errRestacks.Visible = False
        errTotalItems.Visible = False
        errLoadType.Visible = False
        errAmount.Visible = False
        errCheckNumber.Visible = False
        errSplitAmount.Visible = False
    End Sub

#Region "Grid"
    Private Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim loadList As New List(Of WorkOrder)
        Dim wodal As New WorkOrderDAL()
        Dim ldal As New locaDAL
        Dim sDate As Date = Date.Now.ToShortDateString()
        Dim edate As Date = sDate
        Dim dt As New DataTable()
        Dim offset As Integer = ldal.getLocaBDOffset(New Guid(cbLocations.SelectedValue.ToString))
        sDate = DateAdd(DateInterval.Hour, offset, sDate)
        dt = wodal.GetEditLoads(cbLocations.SelectedValue, sDate, edate)
        RadGrid1.DataSource = dt
        RadGrid1.SelectedIndexes.Clear()
    End Sub

    Private Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            'set txt to 1/1/1900 or database startime
            Dim txt As DateTime = IIf(IsDBNull(DirectCast(e.Item.DataItem, DataRowView)("StartTime")), CDate("1/1/1900 12:00 AM"), DirectCast(e.Item.DataItem, DataRowView)("StartTime"))
            Dim lbl As Label = e.Item.FindControl("lblStartTime")
            Dim clbl As Label = e.Item.FindControl("lblCompTime")
            Dim dif As Integer = 0
            If txt > "1/1/1900" Then
                lbl.Text = Format(txt, "hh:mm tt")
                Dim ctxt As DateTime = IIf(IsDBNull(DirectCast(e.Item.DataItem, DataRowView)("CompTime")), CDate("1/1/1900 12:00 AM"), DirectCast(e.Item.DataItem, DataRowView)("CompTime"))
                If ctxt > txt Then
                    dif = DateDiff(DateInterval.Minute, txt, ctxt)
                Else
                    dif = DateDiff(DateInterval.Minute, txt, Date.Now)
                End If

                If dif > 0 Then
                    If dif > 120 Then
                        clbl.Text = "<font color=""red"">" & dif.ToString & " <font size=""1"">mins</font></font>"
                        'ElseIf dif = 0 Then
                        '    clbl.Text = "<font color=""blue"">" & dif.ToString & " <font size=""1"">mins</font></font>"
                    Else
                        clbl.Text = dif.ToString & " <font size=""1"">mins</font>"
                    End If
                End If
            Else
                clbl.Text = "- - -"
                lbl.Text = "- - -"
            End If
        End If
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
        'Dim headerItem As GridHeaderItem = TryCast(RadGrid1.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
        'Dim img As Image = New Image()
        'img.ImageUrl = "~/images/camera.jpg"
        'headerItem.BackColor = Drawing.Color.Yellow
        'headerItem("PicMe").Controls.AddAt(1, img)

    End Sub

    Private Sub RadGrid1_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        clearerrors()
        If e.CommandName = "RowClick" Then
            Dim woid As Guid = RadGrid1.SelectedValue
            setSelectedWOid(woid)
            clearForm() '<-- was missing this method, so for those loads where the user moved from one load to another by simply clicking another in the list, potential overwrite
            ReloadForms(woid.ToString) '<-- now that all form controls are properly reset we can reload form with newly selected item.
        End If
    End Sub

    Private Sub setSelectedWOid(ByVal woid As Guid)
        Dim selectedItems As ArrayList
        If Session("selectedItems") Is Nothing Then
            selectedItems = New ArrayList
        Else
            selectedItems = CType(Session("selectedItems"), ArrayList)
        End If
        selectedItems.Add(woid)
        Session("selectedItems") = selectedItems
    End Sub

#End Region

#Region "Buttons"
    Private Function BuildWorkOrder(Optional ByVal wo As WorkOrder = Nothing) As WorkOrder
        Dim ldal As New locaDAL
        Dim wdal As New WorkOrderDAL 'create a new WorkOrder data access layer for access to it's methods
        Dim ut As New Utilities()
        If wo Is Nothing Then 'This is an INSERTCovenantTransportAndDomesticViolence.com
            wo = New WorkOrder 'create a NEW workorder, some properties are prepopulated 
            wo.LocationID = New Guid(cbLocations.SelectedValue)
            wo.CreatedBy = ut.CreatedByToText(HttpContext.Current.Session("userID"))
        End If
        'if we get here, a wo was passed, so we will 'update' from the ui
        '        wo.LogDate = Nothing 'Date.Now

        '********** Section One ***************
        wo.LogNumber = -1
        If IsNumeric(txtLoadNumber.Text) Then
            wo.LoadNumber = txtLoadNumber.Text
        Else
            wo.LoadNumber = txtLoadNumber.EmptyMessage
        End If
        wo.DoorNumber = txtDoorNumber.Text
        If Utilities.IsValidGuid(cbCarrier.SelectedValue.ToString) Then
            wo.CarrierID = New Guid(cbCarrier.SelectedValue.ToString)
        End If
        wo.TruckNumber = IIf(txtTruckNumber.Text.Trim.Length > 0, txtTruckNumber.Text, String.Empty)
        wo.TrailerNumber = txtTrailerNumber.Text
        wo.PurchaseOrder = txtPurchaseOrder.Text
        If cbDepartment.SelectedIndex > -1 Then
            wo.DepartmentID = New Guid(cbDepartment.SelectedValue)
        End If
        'set by NEW wo.LoadNumber = Nothing 'Format(_LogDate, "MMddHHmmss")
        '********** Section Two ***************
        wo.VendorNumber = txtVendorNumber.Text
        wo.CustomerID = wdal.getCustomerID(wo.LocationID, wo.VendorNumber)
        wo.PalletsUnloaded = IIf(txtPalletsUnloaded.Value Is Nothing, -1, txtPalletsUnloaded.Value)
        wo.Pieces = IIf(txtPieces.Value Is Nothing, -1, txtPieces.Value)
        If cbLoadDescription.SelectedIndex > -1 Then
            wo.LoadDescriptionID = New Guid(cbLoadDescription.SelectedValue)
        End If
        Dim edal As New empDAL
        If edal.GetUnloadersByWOIDString(wo.ID.ToString) <> "<center>- - -</center>" Then
            If txtStartTime.Text = String.Empty Or txtStartTime.Text = "---" Then
                wo.StartTime = surNull
            Else
                Dim astr As String = Format(wo.LogDate, "Short Date") & " " & Format(txtStartTime.Text, "Long Time")
                wo.StartTime = ldal.locaTime(CDate(astr), cbLocations.SelectedValue)

            End If
        End If
        '********** Section Three ***************
        wo.PalletsReceived = IIf(txtPalletsReceived.Value Is Nothing, 0, txtPalletsReceived.Value)
        If txtAppTime.SelectedDate Is Nothing Then
            wo.AppointmentTime = surNull
        Else
            If wo.AppointmentTime > surNull Then
                'cannot change appointment time
            Else
                Dim astr As String = Format(wo.LogDate, "Short Date") & " " & Format(txtAppTime.SelectedDate, "Long Time")
                wo.AppointmentTime = CDate(astr)
            End If
        End If

        If wo.DockTime > surNull Then
            'cannot change Arrival Time
        Else
            Dim astr As String = Format(wo.LogDate, "Short Date") & " " & Format(lbltxtArrivalTime.Text, "Long Time")
            wo.DockTime = astr
        End If

        If txtCompTime.Text = String.Empty Or txtCompTime.Text = "---" Then
            wo.CompTime = surNull
        Else
            Dim astr As String = Format(wo.LogDate, "Short Date") & " " & Format(lbltxtCompTime.Text, "Long Time")
            wo.CompTime = astr
        End If
        wo.TTLTime = 0
        wo.BadPallets = IIf(txtBadPallets.Value Is Nothing, -1, txtBadPallets.Value)
        wo.Weight = IIf(txtWeight.Value Is Nothing, -1, txtWeight.Value)
        wo.Restacks = IIf(txtRestacks.Value Is Nothing, -1, txtRestacks.Value)
        wo.NumberOfItems = IIf(txtTotalItems.Value Is Nothing, -1, txtTotalItems.Value)

        If cbLoadType.SelectedIndex > -1 Then

            Select Case cbLoadType.SelectedItem.Text
                Case "Cash"
                    wo.LoadTypeID = New Guid(cbLoadType.SelectedValue)
                    wo.PaymentType = "Cash"
                    wo.IsCash = True
                    rbgroup.SelectedIndex = 0
                    rbgroup.Items(0).Enabled = True
                    rbgroup.Items(1).Enabled = True
                    rbgroup.Items(2).Enabled = False
                    rbgroup.Items(3).Enabled = True
                    rbgroup74.SelectedIndex = 0
                    rbgroup74.Items(0).Enabled = True
                    rbgroup74.Items(1).Enabled = True
                    rbgroup74.Items(2).Enabled = False
                    rbgroup74.Items(3).Enabled = True
                Case "Credit Card"
                    wo.PaymentType = "Card"
                    rbgroup.SelectedIndex = 2
                    rbgroup.Items(0).Enabled = False
                    rbgroup.Items(1).Enabled = False
                    rbgroup.Items(2).Enabled = True
                    rbgroup.Items(3).Enabled = False
                    wo.PaymentType = "Card"
                    rbgroup74.SelectedIndex = 2
                    rbgroup74.Items(0).Enabled = False
                    rbgroup74.Items(1).Enabled = False
                    rbgroup74.Items(2).Enabled = True
                    rbgroup74.Items(3).Enabled = False
                Case Else
                    wo.LoadTypeID = New Guid(cbLoadType.SelectedValue)
                    rbgroup.Items(0).Enabled = True
                    rbgroup.Items(1).Enabled = True
                    rbgroup.Items(2).Enabled = False
                    rbgroup.Items(3).Enabled = True
                    rbgroup74.Items(0).Enabled = True
                    rbgroup74.Items(1).Enabled = True
                    rbgroup74.Items(2).Enabled = False
                    rbgroup74.Items(3).Enabled = True
            End Select
        End If
        '********** Section Four ***************
        If rbgroup.SelectedIndex > -1 Then
            wo.PaymentType = rbgroup.SelectedItem.Text
            wo.PaymentType = rbgroup74.SelectedItem.Text
        Else
            wo.PaymentType = ""
        End If
        wo.Amount = IIf(txxtAmount.Value Is Nothing, 0, txxtAmount.Value)
        wo.CheckNumber = txtCheckNumber.Text.Trim()
        wo.SplitPaymentAmount = IIf(txtSplitAmount.Value Is Nothing, 0, txtSplitAmount.Value)
        '                wo.ID = Guid.NewGuid()
        wo.BOL = txtBOL.Text
        wo.Comments = txtComments.Text
        'nochange        wo.ID = Nothing 'Guid.NewGuid
        wo.isClosed = Nothing
        '        wo.Employee = Nothing
        setStatus(wo)
        Return wo
    End Function 'BuildWorkOrder

    Private Sub btnSaveChanges_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnSaveChanges.Command
        Dim action As String = e.CommandName
        If action = "Update" Then
            If IsNumeric(txtBadPallets.Text) And IsNumeric(txtRestacks.Text) Then
                Dim one As Integer = validate1()
                Dim two As Integer = validate2()
                Dim Three As Integer = validate3()
                Dim Four As Integer = validate4()
                Dim count As Integer = one + two + Three + Four
                If count > 0 Then
                    lblerr.Visible = True
                    Exit Sub
                End If
            End If 'check that both have value
        End If ' is update

        If validate1() > 0 Then ' minimum is page 1 info
            lblerr.Visible = True
            Exit Sub
        Else
            Dim wdal As New WorkOrderDAL 'create a new WorkOrder data access layer for access to it's methods
            Dim wo As WorkOrder = Nothing 'spin up WorkOrder Object
            'decide if this is an update or new record

            'if it's an update, load what we have in the database into the work order (wo)
            'then update it with current data from UI
            If action = "Update" Then
                wo = wdal.GetLoadByID(RadGrid1.SelectedValue.ToString)
                wo = BuildWorkOrder(wo)
            Else 'create a NEW WorkOder from the UI
                wo = BuildWorkOrder()
            End If
            '**********page 1
            '**********page 2
            Dim hasunloaders As Boolean = Not wo.Employee Is Nothing
            '**********page 3

            If IsNumeric(txtBadPallets.Text) And Not IsNumeric(txtRestacks.Text) Then
                RadWindowManager1.RadAlert("If you enter BadPallets you MUST also enter Restacks (0 is valid)", 280, 150, "Restacks REQUIRED!", "callBackFn", "~/images/dialog_warning.png")
                txtRestacks.Focus()
                errRestacks.Visible = True
                lblerr.Visible = True
                Exit Sub
            ElseIf Not IsNumeric(txtBadPallets.Text) And IsNumeric(txtRestacks.Text) Then
                RadWindowManager1.RadAlert("If you enter Restacks you MUST also enter BadPallets (0 is valid)", 280, 150, "BadPallets REQUIRED!", "callBackFn", "~/images/dialog_warning.png")
                txtBadPallets.Focus()
                errBadPallets.Visible = True
                lblerr.Visible = True
                Exit Sub
            End If


            Dim dtime As Date = wo.DockTime
            If Format(wo.DockTime, "Short Date") = Format(wo.StartTime, "Short Date") Then
                If wo.StartTime < wo.DockTime Then wo.StartTime = DateAdd(DateInterval.Day, 1, wo.StartTime)
            End If

            If Format(wo.StartTime, "Short Date") = Format(wo.CompTime, "Short Date") Then
                If wo.CompTime < wo.StartTime Then wo.CompTime = DateAdd(DateInterval.Day, 1, wo.CompTime)
            End If

            Dim updateMsg As String = String.Empty
            Select Case action
                Case "Insert"
                    If wo.BadPallets > -1 And wo.Restacks > -1 Then 'should never hit this .. already checked??
                        Dim one As Integer = validate1()
                        Dim two As Integer = validate2()
                        Dim Three As Integer = validate3()
                        Dim Four As Integer = validate4()
                        Dim count As Integer = one + two + Three + Four
                        If count > 0 Then
                            txtBadPallets.Text = String.Empty
                            txtRestacks.Text = String.Empty
                            lblerr.Visible = True
                            Exit Sub
                        End If
                    End If
                    updateMsg = wdal.AddWorkOrder(wo, , False)
                    setSelectedWOid(wo.ID)
                    btnSaveChanges.Text = "Add NEW"
                    btnSaveChanges.CommandName = "INSERT"
                    btnSaveChanges.CommandArgument = wo.ID.ToString
                    clearForm()
                    pnlWOedit.Visible = False
                    pnlWOinfo.Visible = False
                    '                    setSelectedWOid(wo.ID)
                Case "Update"
                    If txtBadPallets.Text > "" And txtRestacks.Text > "" Then
                        Dim one As Integer = validate1()
                        Dim two As Integer = validate2()
                        Dim Three As Integer = validate3()
                        Dim Four As Integer = validate4()
                        Dim count As Integer = one + two + Three + Four
                        If count > 0 Then
                            txtBadPallets.Text = String.Empty
                            txtRestacks.Text = String.Empty
                            lblerr.Visible = True
                            Exit Sub
                        End If
                    End If
                    updateMsg = wdal.UpdateWorkOrder(wo, , False)
            End Select
            lblChangesSaved.Visible = True
            setSelectedWOid(wo.ID)
            clearForm() 'changed 9/6/1016
            ReloadForms(wo.ID.ToString)
            btnClearForm.Text = "Add New"
            '            ReloadForms(wo.ID.ToString)
            RadGrid1.Rebind()
        End If
    End Sub 'btnSaveChanges_Command 686

    Private Sub btnClearForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearForm.Click
        If cbLocations.SelectedIndex > -1 Then
            RadGrid1.SelectedIndexes.Clear()
            clearForm()
            '            btnClearForm.Visible = False
            pnlWOedit.Visible = True
            pnlWOinfo.Visible = False
            btnClearForm.Text = "Clear Form"
            btnSaveChanges.Text = "ADD / SAVE This Load"
            btnSaveChanges.CommandName = "Insert"
            btnSaveChanges.CommandArgument = Nothing
            lblEditLoadImages.Text = Nothing
            btnCancel.Text = "Cancel - NO SAVE"
            lblEmptyMessage.Text = "<br /><br /><br /><<<----  Select a Load from left"
            lblEmptyMessage.ForeColor = Drawing.Color.Black
            lblCreatedBy.Visible = False
        Else
            lblEmptyMessage.Visible = True
            pnlWOedit.Visible = False
            pnlWOinfo.Visible = False
            lblEmptyMessage.Text = "<<<--- Select a Location!"
            lblEmptyMessage.ForeColor = Drawing.Color.Red
            lblCreatedBy.Visible = False
        End If
    End Sub

    Private Sub btnPrintWO_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnPrintWO.Command
        Dim rdba As New DBAccess()
        Dim utl As New Utilities()
        Dim workOrderID As String = e.CommandArgument
        Dim wodal As New WorkOrderDAL
        Dim wo As WorkOrder = wodal.GetLoadByID(workOrderID)
        If wo.Status >= 76 Then wo.Status = 78
        wo.CheckNumber = txt74CheckNumber.Text
        If wo.CheckNumber > "" Then wo.PaymentType = "Check"
        wo.Comments = txt74comments.Text.Trim()

        If wo.Status = 74 Then
            wo.Status = 76

            If wo.Status <= 78 Then wo.CheckNumber = txt74CheckNumber.Text

            Dim count As Integer = validate4()
            If count > 0 Then
                lblerr.Visible = True
                Exit Sub
            Else
                wo.Amount = txt74Amount.Value
            End If
            wo.SplitPaymentAmount = IIf(txt74SplitAmount.Value Is Nothing, 0, txt74SplitAmount.Value)
            wo.Comments = txt74comments.Text.Trim()
            btnPrintWO.Text = "SAVE/Print Receipt"
        End If
        If wo.Status = 74 Then btnPrintWO.Text = "SAVE/Print Receipt"
        If wo.Status = 76 Then btnPrintWO.Text = "Re-Print Receipt"
        If validate74() > 0 Then Exit Sub
        wodal.UpdateWorkOrder(wo, , False)

        btnclickit.Visible = True 'False
        RadGrid1.Rebind()
        Session("selectedItems") = Nothing
        pnlWOedit.Visible = False
        pnlWOinfo.Visible = False
        btnClearForm.Visible = True
        btnClearForm.Text = "Add New"
        clearForm() 'changed 9/6/1016
        ReloadForms(wo.ID.ToString)
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "OpenWinDupReceipts('" & workOrderID & "')", True)
    End Sub 'btnPrintWO_Command 35

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        clearForm()
        btnClearForm.Visible = True
        If btnCancel.Text = "Cancel - NO SAVE" Then
            btnClearForm.Text = "Add New"
            btnSaveChanges.Text = "Save Changes"
            btnCancel.Text = "Cancel - NO SAVE"
            pnlWOedit.Visible = False
            pnlWOinfo.Visible = False
            lblEmptyMessage.Visible = True
            lblCreatedBy.Visible = False
        Else
            pnlWOedit.Visible = True
            pnlWOinfo.Visible = False
        End If
    End Sub 'btnCancel_Click

    Protected Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        Dim arg As String = e.Argument
        Dim sarg() As String
        If arg.Contains("Unloader") Then
            sarg = Split(arg, "|")
            Dim wdal As New WorkOrderDAL()
            If RadGrid1.SelectedValue Is Nothing Then
                lblerr.Visible = validate1() > 0
                txtUnloaders.Text = "None Selected"
                txtUnloaderIDlist.Text = String.Empty
                txtUnloaderNamelist.Text = String.Empty
                RadWindowManager1.RadAlert("You MUST save this load before assigning unloaders!<br />", 280, 150, "Cheater Alert", "callBackFn", "~/images/dialog_warning.png")
                Exit Sub
            Else
                Dim ldal As New locaDAL
                Dim updatedWorkOrder As WorkOrder = wdal.GetLoadByID(RadGrid1.SelectedValue.ToString)
                BuildWorkOrder(updatedWorkOrder)
                wdal.UpdateWorkOrder(updatedWorkOrder, , False)
                Dim emp As String = String.Empty
                Dim elst As List(Of String) = New List(Of String)
                If sarg(0) = "Unloader:None Selected" Then
                    txtUnloaderIDlist.Text = "listcleared"
                Else
                    txtUnloaderIDlist.Text = sarg(1)
                    Dim unamelist() As String = Split(sarg(0), ":")
                    txtUnloaderNamelist.Text = unamelist(1)

                    Dim ulList() As String = Split(sarg(1), ":")
                    Dim i As Integer = ulList.Length
                    For x = 0 To i - 1
                        emp = ulList(x)
                        elst.Add(emp)
                    Next
                    If txtStartTime.Text = "---" Or txtStartTime.Text = String.Empty Then
                        txtStartTime.Text = Format(ldal.locaTime(Date.Now, cbLocations.SelectedValue), "hh:mm:ss tt")
                        lbltxtStartTime.Text = txtStartTime.Text
                        updatedWorkOrder.StartTime = updatedWorkOrder.LogDate.ToShortDateString & " " & txtStartTime.Text
                    End If
                End If
                updatedWorkOrder.Employee = elst
                If updatedWorkOrder.BadPallets > -1 And updatedWorkOrder.Restacks > -1 Then
                    Dim one As Integer = validate1()
                    If one > 0 Then
                        lblerr.Visible = True
                        Exit Sub
                    End If
                End If
                updatedWorkOrder.Status = setStatus(updatedWorkOrder)

                wdal.UpdateWorkOrder(updatedWorkOrder, , False)
                clearForm() 'changed 9/6/1016
                ReloadForms(updatedWorkOrder.ID.ToString)
                setSelectedWOid(updatedWorkOrder.ID)
                RadGrid1.Rebind()
            End If
            '************VENDOR
        ElseIf arg.Contains("VendorLookup") Then
            sarg = Split(arg, ":")
            Dim locaID As Guid = New Guid(cbLocations.SelectedValue)
            Dim vnum As String = sarg(1).ToUpper
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
                    txtVendorName.Text = "<font size=""2"" color=""blue"">" & reader.Item(1) & " - " & reader.Item(2) & "</font>"
                    reader.Close()
                Else
                    txtVendorName.Text = "not found"
                End If
            Else
                If vnum.Length = 0 Then
                    txtVendorName.Text = ""
                End If
            End If
        ElseIf arg.Contains("NewVendor") Or arg.Contains("UpdateVendor") Or arg.Contains("SelectVendor") Then
            sarg = Split(arg, ":")
            Dim tbVendorNumber As RadTextBox = CType(FindControl("txtVendorNumber"), RadTextBox)
            tbVendorNumber.Text = sarg(2)
            txtVendorName.Text = "<font size=""2"" color=""blue"">" & sarg(2) & " - " & sarg(3) & "</font>"
        End If

    End Sub 'RadAjaxManager1_AjaxRequest

#End Region

    Public Function setStatus(ByRef wo As WorkOrder) As Integer
        Dim status As LoadStatus = LoadStatus.Undefined
        Dim ldal As New locaDAL
        If wo.Status < 76 Then
            If (validate1() = 0) Or (txtVendorName.Text > "" And txtVendorNumber.Text > "") Then
                status = LoadStatus.CheckedIn
            End If
            If (validate1() = 0) And (txtPieces.Text > "" Or txtPalletsReceived.Text > "" Or cbLoadDescription.SelectedIndex > 1) Then
                status = LoadStatus.CheckedIn Or LoadStatus.AddDataChanged
            End If

            If Not wo.Employee Is Nothing Then
                If wo.Employee.Count > 0 Then
                    status = LoadStatus.AddDataChanged Or LoadStatus.Assigned
                Else
                    '                wo.StartTime = "1/1/1900 12:00 AM"
                End If
            End If

            If wo.BadPallets > -1 And wo.Restacks > -1 Then
                If wo.CompTime = "1/1/1900 12:00 AM" Then
                    wo.CompTime = ldal.locaTime(Date.Now(), cbLocations.SelectedValue)
                End If
                status = LoadStatus.AddDataChanged Or LoadStatus.Assigned Or LoadStatus.Complete
                If rbgroup.SelectedIndex = -1 And cbLoadType.Text <> "Invoice" Then
                    status = LoadStatus.AddDataChanged Or LoadStatus.Assigned Or LoadStatus.Complete Or LoadStatus.Printed
                End If
            Else
                Dim dat As Date = "1/1/1900 12:00 AM"
                wo.CompTime = dat.ToLongDateString
            End If
            wo.Status = status
        End If

        Return status
    End Function 'set status


    Private Sub cbLocations_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        RadGrid1.Rebind()
        btnClearForm.Text = "Add New"
        pnlWOedit.Visible = False
        pnlWOinfo.Visible = False
        lblCreatedBy.Visible = False
        lblEmptyMessage.Text = "<br /><br /><br /><<<----  Select a Load from left"
        lblEmptyMessage.ForeColor = Drawing.Color.Black
        lblEmptyMessage.Visible = True
        lblCreatedBy.Visible = False
        RadGrid1.Rebind()

    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        If cbLocations.SelectedIndex > -1 Then
            RadGrid1.Rebind()
            btnClearForm.Text = "Add New"
            pnlWOedit.Visible = False
            pnlWOinfo.Visible = False
            lblEmptyMessage.Visible = True
            lblCreatedBy.Visible = False
        Else
            lblEmptyMessage.Visible = True
            pnlWOedit.Visible = False
            pnlWOinfo.Visible = False
            lblEmptyMessage.Text = "<<<--- Select a Location!"
            lblEmptyMessage.ForeColor = Drawing.Color.Red
            lblCreatedBy.Visible = False
        End If
    End Sub

    Dim ischanged As Boolean = False

    Private Sub setstuff()
        If cbLoadType.SelectedIndex > -1 Then
            txxtAmount.Enabled = cbLoadType.SelectedIndex > -1
            txtSplitAmount.Enabled = cbLoadType.SelectedIndex > -1
            txtCheckNumber.Enabled = cbLoadType.SelectedIndex > -1
            rbgroup.Enabled = cbLoadType.SelectedIndex > -1

            ischanged = True
            Select Case cbLoadType.SelectedValue.ToString
                Case "e9af1c92-31b1-4849-b52b-93a9b618abc9" 'Driver Load
                    rbgroup.SelectedIndex = -1
                    rbgroup.Enabled = False
                    '                   ' lblchecknumber.Enabled = False
                    editlblCheckNumber.Text = "Check/Transaction Number"
                    editlblCheckNumber.Visible = False
                    txtCheckNumber.Text = Nothing
                    txtCheckNumber.Visible = False
                    ' lblchecknumber.Visible = False
                    lblAmount.Visible = True
                    txtCheckNumber.Enabled = False
                    lblAddCash.Visible = False
                    txtSplitAmount.Visible = False
                    txxtAmount.Value = 0
                    txxtAmount.Enabled = False
                    lblAmount.Enabled = False
                    txtSplitAmount.Text = Nothing
                Case "6144c1a1-3657-4d91-a50a-f107c3a41847", _
                "0369f50a-52ca-4c97-8323-650adc182e04", _
                "c150f229-91aa-433c-8180-3d5a7d4b52f4", _
                "55acef39-f005-4a6b-8ae9-80c2df9dcbb6", _
                "f9d8fade-56db-4b46-b183-d7cce538270f" 'invoice - backhaul - inbound - 3rdparty - Manufacturers
                    rbgroup.SelectedIndex = -1
                    rbgroup.Enabled = False
                    txtSplitAmount.Text = Nothing
                    txxtAmount.Enabled = True
                    lblAmount.Enabled = True
                    editlblCheckNumber.Text = "Check/Transaction Number"
                    editlblCheckNumber.Enabled = False
                    editlblCheckNumber.Visible = False
                    txtCheckNumber.Text = Nothing
                    txtCheckNumber.Enabled = False
                    txtCheckNumber.Visible = False
                    lblAddCash.Visible = False
                    txtSplitAmount.Visible = False
                    txtSplitAmount.Text = Nothing
                Case "fe3fabc8-5335-46f5-8be6-df3d5975d08c" 'Creditcard
                    rbgroup.SelectedIndex = 2
                    rbgroup.Enabled = False
                    lblAmount.Enabled = True
                    editlblCheckNumber.Text = "Transaction Number"
                    editlblCheckNumber.Visible = True
                    txtCheckNumber.Visible = True
                    txxtAmount.Enabled = True
                    editlblCheckNumber.Enabled = True
                    txtCheckNumber.Enabled = True
                    lblAddCash.Visible = False
                    txtSplitAmount.Visible = False
                    txtSplitAmount.Text = Nothing
                Case "d62da4a5-fd15-4460-b62f-baa83ace65fd" 'Cash
                    rbgroup.SelectedIndex = 0
                    rbgroup.Enabled = True
                    rbgroup.Items(2).Enabled = False
                    lblAmount.Enabled = True
                    txxtAmount.Text = Nothing
                    txxtAmount.Enabled = True
                    editlblCheckNumber.Text = "Check/Transaction Number"
                    editlblCheckNumber.Visible = False
                    txtCheckNumber.Text = Nothing
                    txtCheckNumber.Visible = False
                    lblAddCash.Visible = False
                    txtSplitAmount.Visible = False
                    txxtAmount.Visible = True
                    txxtAmount.Text = Nothing
                Case Else 'same as cash  
                    rbgroup.SelectedIndex = 0
                    rbgroup.Enabled = True
                    rbgroup.Items(2).Enabled = False
                    lblAmount.Enabled = True
                    txxtAmount.Text = Nothing
                    txxtAmount.Enabled = True
                    editlblCheckNumber.Text = "Check/Transaction Number"
                    editlblCheckNumber.Visible = False
                    txtCheckNumber.Text = Nothing
                    txtCheckNumber.Visible = False
                    lblAddCash.Visible = False
                    txtSplitAmount.Visible = False
                    txxtAmount.Visible = True
                    txxtAmount.Text = Nothing
            End Select
        End If

    End Sub 'setstuff


    Private Sub cbLoadType_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cbLoadType.SelectedIndexChanged
        rbgroup.Enabled = True

        setstuff()
    End Sub

    Private Sub rbgroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbgroup.SelectedIndexChanged
        errAmount.Visible = False
        errCheckNumber.Visible = False
        errSplitAmount.Visible = False
        Select Case rbgroup.SelectedItem.Text
            Case "Cash"
                txtSplitAmount.Visible = False
                lblAddCash.Visible = False
                editlblCheckNumber.Visible = False
                txtCheckNumber.Text = Nothing
                txtCheckNumber.Visible = False
                errCheckNumber.Visible = False
                errSplitAmount.Visible = False
                txtSplitAmount.Text = Nothing
                txtCheckNumber = Nothing
            Case "Check"
                txtSplitAmount.Visible = False
                lblAddCash.Visible = False
                editlblCheckNumber.Text = "Check Number"
                editlblCheckNumber.Visible = True
                txtCheckNumber.Visible = True
                errSplitAmount.Visible = False
                txtSplitAmount.Text = Nothing
            Case "Card"
                txtSplitAmount.Visible = False
                lblAddCash.Visible = False
                editlblCheckNumber.Text = "Transaction Number"
                editlblCheckNumber.Visible = True
                txtCheckNumber.Visible = True
                errSplitAmount.Visible = False
                txtSplitAmount.Text = Nothing
            Case "Split"
                lblAddCash.Visible = True
                txtSplitAmount.Visible = True
                editlblCheckNumber.Text = "Check Number"
                txtCheckNumber.Visible = True
                editlblCheckNumber.Visible = True
        End Select
    End Sub 'rbgroup_SelectedIndexChanged
    Private Sub rbgroup74_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbgroup74.SelectedIndexChanged

        Select Case rbgroup74.SelectedItem.Text
            Case "Cash"
                txt74SplitAmount.Visible = False
                lblINFOSplitAmount.Visible = False
                lblINFOCheckNumber.Visible = False
                txt74CheckNumber.Visible = False
                errCheckNumber.Visible = False
                errSplitAmount.Visible = False
                '                txtSplitAmount.Text = Nothing
                '                txtCheckNumber = Nothing
            Case "Check"
                txt74SplitAmount.Visible = False
                lblINFOSplitAmount.Visible = False
                lblINFOCheckNumber.Text = "Check Number"
                lblINFOCheckNumber.Visible = True
                txt74CheckNumber.Visible = True
                errSplitAmount.Visible = False
                '                txtSplitAmount.Text = Nothing
            Case "Card"
                txt74SplitAmount.Visible = False
                lblINFOSplitAmount.Visible = False
                lblINFOCheckNumber.Text = "Transaction Number"
                lblINFOCheckNumber.Visible = True
                txt74CheckNumber.Visible = True
                '                errSplitAmount.Visible = False
                '                txtSplitAmount.Text = Nothing
            Case "Split"
                lblINFOSplitAmount.Visible = True
                txt74SplitAmount.Visible = True
                lblINFOCheckNumber.Text = "Check Number"
                txt74CheckNumber.Visible = True
                lblINFOCheckNumber.Visible = True
        End Select
    End Sub 'rbgroup74_SelectedIndexChanged

    Private Sub txtDoorNumber_TextChanged(sender As Object, e As EventArgs) Handles txtDoorNumber.TextChanged
        Dim ldal As New locaDAL
        If Not IsDate(lbltxtArrivalTime.Text) Then
            If lblArrivalTime.Text = "---" Or lblArrivalTime.Text = String.Empty Then
                lbltxtArrivalTime.Text = Format(ldal.locaTime(Date.Now, cbLocations.SelectedValue), "hh:mm:ss tt")

            End If
        End If
    End Sub 'txtDoorNumber_TextChanged

    Private Sub txtBadPallets_TextChanged(sender As Object, e As EventArgs) Handles txtBadPallets.TextChanged, txtRestacks.TextChanged
        Dim ldal As New locaDAL
        If IsNumeric(txtBadPallets.Text) And IsNumeric(txtRestacks.Text) Then
            If txtStartTime.Text = "---" Or txtStartTime.Text = String.Empty Then
                RadWindowManager1.RadAlert("You can NOT enter Bad Pallets or Restacks<br />before assigning uloaders!<br />", 280, 150, "Cheater Alert", "callBackFn", "~/images/dialog_warning.png")
                errunloaders.Visible = True
                lblerr.Visible = True
                txtBadPallets.Text = String.Empty
                txtRestacks.Text = String.Empty
            Else
                lbltxtCompTime.Text = Format(ldal.locaTime(Date.Now, cbLocations.SelectedValue), "hh:mm:ss tt")
                txtCompTime.Text = Format(ldal.locaTime(Date.Now, cbLocations.SelectedValue), "hh:mm:ss tt")
            End If
        End If
    End Sub 'txtBadPallets_TextChanged(sender As Object, e As EventArgs) Handles txtBadPallets.TextChanged, txtRestacks.TextChanged
    Protected Sub loadDepartments()
        Dim ldal As New locaDAL
        Dim dt As DataTable = ldal.GetDepartmentsByLocationID(cbLocations.SelectedValue.ToString)
        cbDepartment.DataSource = dt
        cbDepartment.DataValueField = "ID"
        cbDepartment.DataTextField = "Name"
        cbDepartment.DataBind()
        cbDepartment.ClearSelection()
        cbDepartment.Text = ""
    End Sub

#Region "Validation"

    Protected Function validate1() As Integer
        Dim count As Integer = 0
        clearerrors()
        If txtDoorNumber.Text = String.Empty Then
            errDoor.Visible = True
            count += 1
        End If


        If Not Utilities.IsValidGuid(cbCarrier.SelectedValue.ToString) Then
            errCarrier.Visible = True
            count += 1
        End If
        If txtTruckNumber.Text = String.Empty Then
            errTruck.Visible = True
            count += 1
        End If
        If txtTrailerNumber.Text = String.Empty Then
            errTrailer.Visible = True
            count += 1
        End If
        If txtPurchaseOrder.Text = String.Empty Then
            errPurchaseOrder.Visible = True
            count += 1
        End If
        If cbDepartment.SelectedIndex = -1 Then
            errDepartment.Visible = True
            count += 1
        End If
        If count > 0 Then
        End If
        Return count
    End Function

    Protected Function validate2() As Integer

        Dim count As Integer = validate1()

        errVendorNumber.Visible = False
        If txtVendorNumber.Text = "" Then
            errVendorNumber.Visible = True
            count += 1
        End If

        errPalletsUnloaded.Visible = False
        If errPalletsUnloaded.Text = "" Then
            errPalletsUnloaded.Visible = True
            count += 1
        End If

        errPieces.Visible = False
        If txtPieces.Text = "" Then
            errPieces.Visible = True
            count += 1
        End If
        Return count
    End Function

    Protected Function validate3()
        Dim count As Integer = validate1()

        errPalletsReceived.Visible = False
        If txtPalletsReceived.Text = "" Then
            errPalletsReceived.Visible = True
            count += 1
        End If

        errBadPallets.Visible = False
        If txtBadPallets.Text = "" Then
            errBadPallets.Visible = True
            count += 1
        End If

        errWeight.Visible = False
        If txtWeight.Text = "" Then
            errWeight.Visible = True
            count += 1
        End If

        errRestacks.Visible = False
        If txtRestacks.Text = "" Then
            errRestacks.Visible = True
            count += 1
        End If

        errTotalItems.Visible = False
        If txtTotalItems.Text = "" Then
            errTotalItems.Visible = True
            count += 1
        End If

        errLoadType.Visible = False
        If cbLoadType.SelectedIndex < 0 Then
            errLoadType.Visible = True
            count += 1
        End If


        Return count
    End Function

    Private Function validate4() As Integer
        Dim count As Integer = 0
        ' 6144c1a1-3657-4d91-a50a-f107c3a41847 'Invoice
        ' e9af1c92-31b1-4849-b52b-93a9b618abc9 'Driver Load
        ' 0369f50a-52ca-4c97-8323-650adc182e04 'Backhaul
        ' c150f229-91aa-433c-8180-3d5a7d4b52f4 'Inbound
        ' 55acef39-f005-4a6b-8ae9-80c2df9dcbb6 '3rd Party
        ' f9d8fade-56db-4b46-b183-d7cce538270f 'Manufacturers
        ' d62da4a5-fd15-4460-b62f-baa83ace65fd 'Cash
        ' fe3fabc8-5335-46f5-8be6-df3d5975d08c 'Creditcard
        '    'only if Driver Load is amount of zero ok        
        errAmount.Visible = False
        If cbLoadType.SelectedValue.ToString <> "e9af1c92-31b1-4849-b52b-93a9b618abc9" Then     'Driver Load
            If txxtAmount.Text = "0" Or txxtAmount.Text = "" Then
                errAmount.Visible = True
                count += 1
            End If
        End If

        errCheckNumber.Visible = False
        If (rbgroup.Items(1).Selected Or rbgroup.Items(3).Selected) And txtCheckNumber.Text = "" Then
            errCheckNumber.Visible = True
            count += 1
        End If

        errSplitAmount.Visible = False
        If rbgroup.Items(3).Selected And txtSplitAmount.Text = "" Then
            errSplitAmount.Visible = True
            count += 1
        End If



        Return count
    End Function 'page4validate

    Private Function validate74() As Integer
        Dim count As Integer = 0
        ' 6144c1a1-3657-4d91-a50a-f107c3a41847 'Invoice
        ' e9af1c92-31b1-4849-b52b-93a9b618abc9 'Driver Load
        ' 0369f50a-52ca-4c97-8323-650adc182e04 'Backhaul
        ' c150f229-91aa-433c-8180-3d5a7d4b52f4 'Inbound
        ' 55acef39-f005-4a6b-8ae9-80c2df9dcbb6 '3rd Party
        ' f9d8fade-56db-4b46-b183-d7cce538270f 'Manufacturers
        ' d62da4a5-fd15-4460-b62f-baa83ace65fd 'Cash
        ' fe3fabc8-5335-46f5-8be6-df3d5975d08c 'Creditcard
        '    'only if Driver Load is amount of zero ok        
        errAmount.Visible = False
        If cbLoadType.SelectedValue.ToString <> "e9af1c92-31b1-4849-b52b-93a9b618abc9" Then     'Driver Load
            If txt74Amount.Text = "0" Or txt74Amount.Text = "" Then
                errAmount.Visible = True
                count += 1
            End If
        End If

        lblINFOCheckNumber.ForeColor = Drawing.Color.Black
        If (rbgroup74.Items(1).Selected Or rbgroup74.Items(3).Selected) And txt74CheckNumber.Text = "" Then
            lblINFOCheckNumber.ForeColor = Drawing.Color.Red
            count += 1
        End If

        lblINFOSplitAmount.ForeColor = Drawing.Color.Black
        If rbgroup74.Items(3).Selected And txt74SplitAmount.Text = "" Then
            lblINFOSplitAmount.ForeColor = Drawing.Color.Red
            count += 1
        End If
        lblerr.Visible = count > 0
        Return count
    End Function 'page4validate

#End Region 'Validation

End Class

'Dim status As LoadStatus = LoadStatus.Undefined
'        If wo.Status < 76 Then

'Dim strpg1 As String = page1validate()
'            If (strpg1 = "") Or (strpg1 = "" And txtVendor.Text > "" And txtVendorNumber.Text > "") Then
'                status = LoadStatus.CheckedIn
'            End If
'            If page1validate() = "" And AddDataChanged() Then
'                status = LoadStatus.CheckedIn Or LoadStatus.AddDataChanged
'            End If

'            If Not wo.Unloaders Is Nothing Then
'                If wo.Unloaders.Count > 0 Then
'                    status = LoadStatus.AddDataChanged Or LoadStatus.Assigned
'                    status = LoadStatus.AddDataChanged Or LoadStatus.Assigned
'                Else
''                wo.StartTime = "1/1/1900 12:00 AM"
'                End If
'            End If

'            If wo.BadPallets > -1 And wo.Restacks > -1 Then
'                If wo.CompTime = "1/1/1900 12:00 AM" Then
'                    wo.CompTime = Date.Now()
'                End If
'                status = LoadStatus.AddDataChanged Or LoadStatus.Assigned Or LoadStatus.Complete
'                If Not rbCash.Checked And Not rbCheck.Checked And Not rbCard.Checked And Not rbsplit.Checked And cbLoadType.Text <> "Invoice" Then
'                    status = LoadStatus.AddDataChanged Or LoadStatus.Assigned Or LoadStatus.Complete Or LoadStatus.Printed
'                End If
'            Else
'Dim dat As Date = "1/1/1900 12:00 AM"
'                wo.CompTime = dat.ToLongDateString
'            End If
'            wo.Status = status
'        End If
