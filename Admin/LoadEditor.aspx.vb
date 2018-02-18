Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class LoadEditor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            dpStartDate.SelectedDate = Date.Now()
            dpEndDate.SelectedDate = Date.Now()
            ' populate Locations Combo Box (cbLocations on aspx page)
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
            If cbLocations.SelectedIndex > -1 Then
                radbut1.Visible = Utilities.haspics(cbLocations.SelectedValue)
                radbut1.Attributes.Add("onclick", "openPicEmailer('" & cbLocations.SelectedValue & "');return false;")
            End If
            lblEditVendorList.Visible = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
            txtVendorNumber.Attributes("onkeyup") = "vendorLookup(this);"
        End If
        'If HttpContext.Current.User.Identity.Name = "Bill" Then
        '    Image1.ImageUrl = "~/images/rogersFish.gif"
        'Else
        Image1.ImageUrl = "~/images/rogersFish.gif"
        '       End If
    End Sub

    Protected Sub ReloadForms(ByVal woid As String)
        lblEmptyMessage.Visible = False
        pnlWOinfo.Visible = True
        btnDeleteLoad.Visible = User.IsInRole("Administrator") Or User.IsInRole("SysOp")
        pnlWOedit.Visible = False
        '        Dim woid As String = RadGrid1.SelectedValue.ToString
        Dim wodal As New WorkOrderDAL()
        Dim edal As New empDAL()
        Dim wo As WorkOrder = wodal.GetLoadByID(woid)
        lblCreatedBy.Visible = True
        lblCreatedBy.Text = wo.CreatedBy
        'lblCreatedBy.Text = IIf(wo.CreatedBy.Contains(":"), wo.CreatedBy, "rtdsHandHeld")
        lblLoadNumber.Text = wo.LoadNumber
        lblDateWorked.Text = Format(wo.LogDate, "M/d/yyyy")
        lblDoorNumber.Text = wo.DoorNumber
        lblAmount.Text = FormatCurrency(wo.Amount, 2)
        LblSplitAmount.Text = FormatCurrency(wo.SplitPaymentAmount, 2)

        lblDepartment.Text = wo.Department
        If wo.CarrierName.ToUpper.IndexOf("SEE COMMENTS") <> -1 Then
            lblCarrierName.Text = "<span class=""ColorMeRed"">" & wo.CarrierName & "</span>"
        Else
            lblCarrierName.Text = wo.CarrierName
        End If
        If wo.CarrierName.ToUpper.IndexOf("SEE COMMENTS") <> -1 Then
            lblCarrierNamev.Text = "<span class=""ColorMeRed"">" & wo.CarrierName & "</span>"
            txtCarrierIDv.Text = String.Empty
        Else
            lblCarrierNamev.Text = wo.CarrierName
            txtCarrierIDv.Text = wo.CarrierID.ToString
        End If
        lblTruckNumber.Text = wo.TruckNumber
        lblTrailerNumber.Text = wo.TrailerNumber
        lblPurchaseOrder.Text = wo.PurchaseOrder

        lblVendorNumber.Text = wo.VendorNumber
        lblVendorName.Text = wo.VendorName

        lblUnloadersV.Text = edal.GetUnloadersByWOIDString(woid)
        lblEditUnloaders.Text = "<span style=""font-size:11px;color:blue"" onmouseover=""this.style.cursor='pointer'"" onclick=""openUnloaders('" & wo.ID.ToString & "');"">Change</span>"
        lblLoadImages.Text = "<span style=""font-size:11px;color:blue"" onmouseover=""this.style.cursor='pointer'"" onclick=""openLoadImages('" & wo.ID.ToString & "');"">Manage Pictures</span>"
        lblEditLoadImages.Text = lblLoadImages.Text

        lblPieces.Text = wo.Pieces
        lblPalletsReceived.Text = wo.PalletsReceived
        If wo.PalletsReceived = -1 Then lblPalletsReceived.Text = "---"
        lblLoadDescription.Text = wo.LoadDescription
        lblPalletsUnloaded.Text = wo.PalletsUnloaded
        If wo.PalletsUnloaded = -1 Then lblPalletsUnloaded.Text = "---"
        Dim surNull As Date = "1/1/1900 12:00"
        lblAppTime.Text = IIf(wo.AppointmentTime > surNull, Format(wo.AppointmentTime, "hh:mm:ss tt"), "---")
        lblGateTime.Text = IIf(wo.GateTime > surNull, Format(wo.GateTime, "hh:mm:ss tt"), "---")
        lblArrivalTime.Text = IIf(wo.DockTime > surNull, Format(wo.DockTime, "hh:mm:ss tt"), "---")
        lblStartTime.Text = IIf(wo.StartTime > surNull, Format(wo.StartTime, "hh:mm:ss tt"), "---")
        lblCompTime.Text = IIf(wo.CompTime > surNull, Format(wo.CompTime, "hh:mm:ss tt"), "---")
        Dim totaltime As Integer = DateDiff(DateInterval.Minute, wo.StartTime, wo.CompTime)
        If totaltime > 120 Then
            lblTotalTime.Text = "<span style=""color:red;"">" & totaltime & " minutes</span>"
        ElseIf totaltime < 0 Then
            lblTotalTime.Text = "in progress"
        Else
            lblTotalTime.Text = totaltime & " minutes</span>"
        End If
        lblBadPallets.Text = wo.BadPallets
        If wo.BadPallets = -1 Then lblBadPallets.Text = "---"
        lblWeight.Text = wo.Weight
        If wo.Weight = -1 Then lblWeight.Text = "---"
        lblRestacks.Text = wo.Restacks
        If wo.Restacks = -1 Then lblRestacks.Text = "---"
        lblTotalItems.Text = wo.NumberOfItems
        If wo.NumberOfItems = -1 Then lblTotalItems.Text = "---"
        lblLoadType.Text = wo.LoadType
        lblCheckNumber.Text = wo.CheckNumber
        lblBOL.Text = wo.BOL
        lblComments.Text = wo.Comments
        btnclickit.Visible = Not wo.isClosed And (User.IsInRole("SysOp") Or User.IsInRole("Administrator") Or User.IsInRole("Manager"))
        btnCloseWO.CommandArgument = wo.ID.ToString
        If User.IsInRole("Supervisor") Then
            btnEdit.Visible = Not wo.isClosed Or (User.IsInRole("SysOp") Or User.IsInRole("Administrator") Or User.IsInRole("Manager"))
        End If

        dpDateWorked.SelectedDate = wo.LogDate
        txtLoadNumber.Text = wo.LoadNumber
        txtDoorNumber.Text = wo.DoorNumber
        txxtAmount.Text = wo.Amount
        Dim rdba As New DBAccess()
        Dim ldal As New locaDAL
        Dim dt As DataTable = ldal.GetDepartmentsByLocationID(cbLocations.SelectedValue.ToString)
        cbDepartment.DataSource = dt
        cbDepartment.DataValueField = "ID"
        cbDepartment.DataTextField = "Name"
        cbDepartment.DataBind()
        cbDepartment.SelectedValue = wo.DepartmentID.ToString
        txtTruckNumber.Text = wo.TruckNumber
        txtTrailerNumber.Text = wo.TrailerNumber
        txtPurchaseOrder.Text = wo.PurchaseOrder
        txtVendorNumber.Text = wo.VendorNumber
        txtVendorName.Text = wo.VendorNumber & " - " & wo.VendorName
        vid.Text = wo.LocationID.ToString
        txtPieces.Text = wo.Pieces
        txtPalletsReceived.Text = wo.PalletsReceived
        txtPalletsUnloaded.Text = wo.PalletsUnloaded
        dt = ldal.GetLoadDescriptionsByLocationID(cbLocations.SelectedValue.ToString)
        cbLoadDescription.DataSource = dt
        cbLoadDescription.DataValueField = "ID"
        cbLoadDescription.DataTextField = "Name"
        cbLoadDescription.DataBind()
        cbLoadDescription.ClearSelection()
        cbLoadDescription.Text = ""
        cbLoadDescription.SelectedValue = wo.LoadDescriptionID.ToString
        txtUnloaderIDlist.Text = "nc"
        txtUnloaders.Text = edal.GetUnloadersByWOIDString(woid)

        If wo.AppointmentTime > surNull Then
            txtAppTime.SelectedDate = wo.AppointmentTime
        Else
            txtAppTime.Clear()
        End If
        If wo.GateTime > surNull Then
            txtGateTime.SelectedDate = wo.GateTime
        Else
            txtGateTime.Clear()
        End If
        If wo.DockTime > surNull Then
            txtArrivalTime.SelectedDate = wo.DockTime
        Else
            txtArrivalTime.Clear()
        End If
        If wo.StartTime > surNull Then
            txtStartTime.SelectedDate = wo.StartTime
        Else
            txtStartTime.Clear()
        End If
        If wo.CompTime > surNull Then
            txtCompTime.SelectedDate = wo.CompTime
        Else
            txtCompTime.Clear()
        End If
        txtBadPallets.Text = wo.BadPallets
        txtTotalItems.Text = wo.NumberOfItems
        txtWeight.Text = wo.Weight
        txtRestacks.Text = wo.Restacks
        rdba.CommandText = "SELECT ID, Name FROM LoadType ORDER BY Name"
        Dim ds As New DataSet
        ds = rdba.ExecuteDataSet
        txtLoadType.DataSource = ds.Tables(0)
        txtLoadType.DataValueField = "ID"
        txtLoadType.DataTextField = "Name"
        txtLoadType.DataBind()
        txtLoadType.ClearSelection()
        txtLoadType.SelectedValue = wo.LoadTypeID.ToString
        lblIsCash.Text = IIf(wo.LoadTypeID.ToString = "d62da4a5-fd15-4460-b62f-baa83ace65fd", "<span style=""color:green;"">YES</span>", "No")
        txtCheckNumber.Text = wo.CheckNumber
        txtBOL.Text = wo.BOL
        txtComments.Text = wo.Comments
        txtSplitAmount.Text = IIf(wo.SplitPaymentAmount < 1, 0, wo.SplitPaymentAmount)
        If wodal.isLoadLocked(woid) And wo.LoadType = "Cash" Then
            txxtAmount.Enabled = False
            txtLoadType.Enabled = False
            txtSplitAmount.Enabled = False
            'no let everyone in
        End If
        If User.IsInRole("Administrator") Or User.IsInRole("SysOp") Then
            txxtAmount.Enabled = True
            txtLoadType.Enabled = True
            txtSplitAmount.Enabled = True
        End If

        btnClearForm.Text = "Add New"
        btnSaveChanges.Text = "Save Changes"
        btnSaveChanges.CommandName = "Update"
        btnSaveChanges.CommandArgument = wo.ID.ToString

        btnCancel.Text = "Return to View Mode"
        lblChangeSelectCarrier.Text = "Change"

    End Sub         ' ************* ReloadForms(woid as string) 

    Private Sub clearForm()
        lblEmptyMessage.Visible = False
        txtLoadNumber.Text = ""
        dpDateWorked.SelectedDate = FormatDateTime(Date.Now, DateFormat.ShortDate)
        txtDoorNumber.Text = ""
        txxtAmount.Text = ""
        Dim ldal As New locaDAL

        Dim dt As DataTable = ldal.GetDepartmentsByLocationID(cbLocations.SelectedValue.ToString)
        cbDepartment.DataSource = dt
        cbDepartment.DataValueField = "ID"
        cbDepartment.DataTextField = "Name"
        cbDepartment.DataBind()
        cbDepartment.ClearSelection()
        cbDepartment.Text = ""
        lblChangeSelectCarrier.Text = "Select"
        lblCarrierNamev.Text = ""
        txtCarrierIDv.Text = ""
        txtTruckNumber.Text = ""
        txtTrailerNumber.Text = ""
        txtPurchaseOrder.Text = ""
        txtVendorName.Text = ""
        txtVendorNumber.Text = ""
        txtPieces.Text = ""
        txtPalletsReceived.Text = ""
        Dim rdba As New DBAccess()
        rdba.CommandText = "SELECT ID, Name FROM Description WHERE InActive=0 ORDER BY Name"
        Dim ds As DataSet = rdba.ExecuteDataSet
        cbLoadDescription.DataSource = ds.Tables(0)
        cbLoadDescription.DataValueField = "ID"
        cbLoadDescription.DataTextField = "Name"
        cbLoadDescription.DataBind()
        cbLoadDescription.ClearSelection()
        cbLoadDescription.Text = ""
        lblEditUnloaders.Text = "<span style=""font-size:11px;color:blue"" onmouseover=""this.style.cursor='pointer'"" onclick=""openUnloaders('" & cbLocations.SelectedValue.ToString & "&gtype=locaID" & "');"">Select</span>"
        txtUnloaders.Text = ""
        txtUnloaderIDlist.Text = ""
        txtPalletsUnloaded.Text = ""
        txtAppTime.Clear()
        txtGateTime.Clear()
        txtArrivalTime.Clear()
        txtStartTime.Clear()
        txtCompTime.Clear()
        txtBadPallets.Text = ""
        txtWeight.Text = ""
        txtRestacks.Text = ""
        txtTotalItems.Text = ""
        rdba.CommandText = "SELECT ID, Name FROM LoadType ORDER BY Name"
        ds = rdba.ExecuteDataSet
        txtLoadType.DataSource = ds.Tables(0)
        txtLoadType.DataValueField = "ID"
        txtLoadType.DataTextField = "Name"
        txtLoadType.DataBind()
        txtLoadType.ClearSelection()
        txtLoadType.Text = ""
        txtCheckNumber.Text = ""
        txtBOL.Text = ""
        txtComments.Text = ""
        vid.Text = cbLocations.SelectedValue.ToString
        lblChangesSaved.Visible = False
        '        btnCancel

    End Sub     ' *********** clearForm()

#Region "Grid"
    Private Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim loadList As New List(Of WorkOrder)
        Dim wodal As New WorkOrderDAL()
        Dim sDate As Date = dpStartDate.SelectedDate
        Dim edate As Date = dpEndDate.SelectedDate
        Dim dt As New DataTable()
        If txtFilter.Text.Trim.Length > 0 Then
            dt = wodal.GetLoadsToBeVerified(cbLocations.SelectedValue, sDate, edate, txtFilter.Text.Trim())
        Else
            dt = wodal.GetLoadsToBeVerified(cbLocations.SelectedValue, sDate, edate)
        End If
        RadGrid1.DataSource = dt
    End Sub

    Private Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim txt As DateTime = IIf(IsDBNull(DirectCast(e.Item.DataItem, DataRowView)("StartTime")), "1/1/1900", DirectCast(e.Item.DataItem, DataRowView)("StartTime"))
            Dim lbl As Label = e.Item.FindControl("lblStartTime")
            If txt <> "1/1/1900" Then
                lbl.Text = Format(txt, "hh:mm tt")
            Else
                lbl.Text = "- - -"
            End If

            Dim ctxt As DateTime = IIf(IsDBNull(DirectCast(e.Item.DataItem, DataRowView)("CompTime")), "1/1/1900", DirectCast(e.Item.DataItem, DataRowView)("CompTime"))
            Dim clbl As Label = e.Item.FindControl("lblCompTime")
            If ctxt <> "1/1/1900" Then
                Dim dif As Integer = DateDiff(DateInterval.Minute, txt, ctxt)
                If dif > -1 Then

                    If dif > 120 Then
                        clbl.Text = "<font color=""red"">" & dif.ToString & " <font size=""1"">mins</font></font>"
                        'ElseIf dif = 0 Then
                        '    clbl.Text = "<font color=""blue"">" & dif.ToString & " <font size=""1"">mins</font></font>"
                    Else
                        clbl.Text = dif.ToString & " <font size=""1"">mins</font>"
                    End If
                Else
                    clbl.Text = "- - -"
                End If
            Else
                clbl.Text = "- - -"
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
        If e.CommandName = "RowClick" Then
            Dim woid As Guid = RadGrid1.SelectedValue
            setSelectedWOid(woid)
            ReloadForms(woid.ToString)
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
    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        RadGrid1.Rebind()
        radbut1.Visible = Utilities.haspics(cbLocations.SelectedValue)
        radbut1.Attributes.Add("onclick", "openPicEmailer('" & cbLocations.SelectedItem.Value & "');return false;")
        radbut1.Attributes.Add("onmouseover", "this.style.pointer='pointer';")
        pnlWOedit.Visible = False
        pnlWOinfo.Visible = False
        lblEmptyMessage.Visible = True
    End Sub

    Private Sub btnClearForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearForm.Click
        If cbLocations.SelectedIndex > -1 Then
            clearForm()
            pnlWOedit.Visible = True
            pnlWOinfo.Visible = False
            btnClearForm.Text = "Clear Form"
            btnSaveChanges.Text = "ADD / SAVE This Load"
            btnSaveChanges.CommandName = "Insert"
            btnSaveChanges.CommandArgument = Nothing
            btnCancel.Text = "Cancel"
            lblEmptyMessage.Text = "<<<----  Select a Load from left"
        Else
            lblEmptyMessage.Visible = True
            pnlWOedit.Visible = False
            pnlWOinfo.Visible = False
            lblEmptyMessage.Text = "<<<... ummm ... and the location we'll be using today is ...??"
        End If
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
        pnlWOedit.Visible = True
        pnlWOinfo.Visible = False

    End Sub

    Private Sub btnCloseWO_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnCloseWO.Command
        Dim rdba As New DBAccess()
        Dim utl As New Utilities()
        Dim workOrderID As String = e.CommandArgument
        Dim wodal As New WorkOrderDAL
        Dim wo As WorkOrder = wodal.GetLoadByID(workOrderID)
        wo.Status = 128
        wodal.UpdateWorkOrder(wo)
        Dim user As MembershipUser = Membership.GetUser(HttpContext.Current.User.Identity.Name)
        Dim userID As String = user.ProviderUserKey.ToString
        If Not wodal.isLoadLocked(workOrderID) Then
            rdba.CommandText = "INSERT INTO VerifiedWorkOrders (workOrderID, userID, timeStamp) VALUES (@workOrderID, @userID, @timeStamp)"
            rdba.AddParameter("@workOrderID", workOrderID)
            rdba.AddParameter("@userID", userID)
            rdba.AddParameter("@timeStamp", Date.Now())
            rdba.ExecuteNonQuery()
        End If
        btnclickit.Visible = False
        RadGrid1.Rebind()
    End Sub

    Private Sub btnSaveChanges_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnSaveChanges.Command
        Dim wdal As New WorkOrderDAL 'create a new WorkOrder data access layer for access to it's methods
        Dim wo As New WorkOrder 'create a new empty workorder 
        'decide if this is an update or new record
        Dim action As String = e.CommandName

        'if it's an update, load what we have in the database into the work order (wo)
        If action = "Update" Then
            wo = wdal.GetLoadByID(RadGrid1.SelectedValue.ToString)
            BuildWorkOrder(wo)
        Else
            BuildWorkOrder(wo)
        End If

        If action = "Insert" Then 'if this is a new record, populate the fields not on the form
            wo.Status = 192
            wo.LogNumber = -1
            wo.LoadNumber = wdal.nextLoadNumber(wo.LogDate)
            wo.LocationID = New Guid(cbLocations.SelectedValue)
            If txtLoadType.Text = "Cash" Then
                wo.ReceiptNumber = wo.LoadNumber  ''''''''''''''''''' NEVER ON Backhaul/Inbound default to -1 
            Else
                wo.ReceiptNumber = -1 ''''''''''''''''''' NEVER ON Backhaul/Inbound default to -1 
            End If
            If Year(wo.StartTime) = Year(wo.CompTime) Then
                wo.TTLTime = DateDiff(DateInterval.Minute, wo.StartTime, wo.CompTime)
            Else
                wo.TTLTime = 0
            End If
            wo.ID = Guid.NewGuid()
            Dim ut As New Utilities()
            wo.CreatedBy = HttpContext.Current.Session("userID").ToString
        End If

        wo.CustomerID = wdal.getCustomerID(wo.LocationID, wo.VendorNumber) ' Nothing ''''''''''''''''''''' what is this?

        Dim updateMsg As String
        Select Case action
            Case "Insert"
                updateMsg = wdal.AddWorkOrder(wo)
                btnSaveChanges.Text = "Save Changes"
                btnSaveChanges.CommandName = "Update"
                btnSaveChanges.CommandArgument = wo.ID.ToString
                setSelectedWOid(wo.ID)
            Case "Update"
                updateMsg = wdal.UpdateWorkOrder(wo)
            Case Else

        End Select
        '        lblChangesSaved.Visible = True
        ReloadForms(wo.ID.ToString)
        RadGrid1.Rebind()
    End Sub
    Protected Sub BuildWorkOrder(ByRef wo As WorkOrder)
        Dim wdal As New WorkOrderDAL
        If IsNumeric(txtLoadNumber.Text) Then
            wo.LoadNumber = txtLoadNumber.Text.Trim
        Else
            wo.LoadNumber = wdal.nextLoadNumber(Date.Now)
        End If

        wo.LogDate = dpDateWorked.SelectedDate + " " + Date.Now.ToLongTimeString
        wo.DoorNumber = txtDoorNumber.Text
        wo.Amount = IIf(txxtAmount.Value Is Nothing, 0, txxtAmount.Value)
        wo.SplitPaymentAmount = IIf(txtSplitAmount.Value Is Nothing, 0, txtSplitAmount.Value)

        '        wo.IsCash = txtLoadType.Text = "Cash"

        If cbDepartment.SelectedIndex > -1 Then
            wo.DepartmentID = New Guid(cbDepartment.SelectedValue)
        End If
        Dim str As String = txtCarrierIDv.Text
        If txtCarrierIDv.Text.Length = 36 Then
            wo.CarrierID = New Guid(txtCarrierIDv.Text)
        End If
        wo.TruckNumber = IIf(txtTruckNumber.Text.Trim.Length > 0, txtTruckNumber.Text, String.Empty)
        wo.TrailerNumber = txtTrailerNumber.Text
        wo.PurchaseOrder = txtPurchaseOrder.Text
        wo.VendorNumber = txtVendorNumber.Text
        wo.Pieces = IIf(txtPieces.Value Is Nothing, 0, txtPieces.Value)
        wo.PalletsReceived = IIf(txtPalletsReceived.Value Is Nothing, 0, txtPalletsReceived.Value)
        wo.PalletsUnloaded = IIf(txtPalletsUnloaded.Value Is Nothing, 0, txtPalletsUnloaded.Value)
        If cbLoadDescription.SelectedIndex > -1 Then
            wo.LoadDescriptionID = New Guid(cbLoadDescription.SelectedValue)
        End If
        Dim elst As List(Of String) = New List(Of String)
        Select Case txtUnloaderIDlist.Text
            Case "nc"

            Case "listcleared"
                elst.Add("listcleared")
            Case Else
                If Len(txtUnloaderIDlist.Text) > 25 Then
                    Dim ulList() As String = Split(txtUnloaderIDlist.Text, ":")
                    Dim i As Integer = ulList.Length
                    For x = 0 To i - 1
                        Dim emp As String = String.Empty
                        emp = ulList(x)
                        elst.Add(emp)
                    Next
                Else

                End If
        End Select
        wo.Employee = elst
        Dim surNull As Date = "1/1/1900 12:00 AM"
        If txtAppTime.SelectedDate Is Nothing Then
            wo.AppointmentTime = surNull
        Else
            Dim astr As String = Format(wo.LogDate, "Short Date") & " " & Format(txtAppTime.SelectedDate, "Long Time")
            wo.AppointmentTime = CDate(astr)
        End If

        If txtGateTime.SelectedDate Is Nothing Then
            wo.GateTime = surNull
        Else
            Dim astr As String = Format(wo.LogDate, "Short Date") & " " & Format(txtGateTime.SelectedDate, "Long Time")
            wo.GateTime = CDate(astr)
        End If

        If txtArrivalTime.SelectedDate Is Nothing Then
            wo.DockTime = surNull
        Else
            Dim astr As String = Format(wo.LogDate, "Short Date") & " " & Format(txtArrivalTime.SelectedDate, "Long Time")
            wo.DockTime = CDate(astr)
        End If

        If txtStartTime Is Nothing Then
            wo.StartTime = surNull
        Else
            Dim astr As String = Format(wo.LogDate, "Short Date") & " " & Format(txtArrivalTime.SelectedDate, "Long Time")
            wo.StartTime = CDate(astr)

        End If

        Dim dtime As Date = wo.DockTime

        If txtStartTime.SelectedDate Is Nothing Then
            wo.StartTime = surNull
        Else

            'If DatePart(DateInterval.Hour, sdt) > DatePart(DateInterval.Hour, wo.DockTime) Then

            '    'starttime dateportion= docktime dateportion

            'Else

            '    ' startime dateportion = docktime dateportion + 1

            'End If

            Dim astr As String = Format(wo.DockTime, "Short Date") & " " & Format(txtStartTime.SelectedDate, "Long Time")
            wo.StartTime = CDate(astr)
        End If
        If Format(wo.DockTime, "Short Date") = Format(wo.StartTime, "Short Date") Then
            If wo.StartTime < wo.DockTime Then wo.StartTime = DateAdd(DateInterval.Day, 1, wo.StartTime)
        End If

        If txtCompTime.SelectedDate Is Nothing Then
            wo.CompTime = surNull
        Else
            Dim astr As String = Format(wo.StartTime, "Short Date") & " " & Format(txtCompTime.SelectedDate, "Long Time")
            wo.CompTime = CDate(astr)
        End If

        If Format(wo.StartTime, "Short Date") = Format(wo.CompTime, "Short Date") Then
            If wo.CompTime < wo.StartTime Then wo.CompTime = DateAdd(DateInterval.Day, 1, wo.CompTime)
        End If


        wo.BadPallets = IIf(txtBadPallets.Value Is Nothing, 0, txtBadPallets.Value)
        wo.NumberOfItems = IIf(txtTotalItems.Value Is Nothing, 0, txtTotalItems.Value)
        wo.Weight = IIf(txtWeight.Value Is Nothing, 0, txtWeight.Value)
        wo.Restacks = IIf(txtRestacks.Value Is Nothing, 0, txtRestacks.Value)
        If txtLoadType.SelectedIndex > -1 Then
            wo.LoadTypeID = New Guid(txtLoadType.SelectedValue)
            wo.IsCash = txtLoadType.Text = "Cash"
        End If
        wo.CheckNumber = txtCheckNumber.Text.Trim()
        wo.BOL = txtBOL.Text
        wo.Comments = txtComments.Text
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        If btnCancel.Text = "Cancel" Then
            btnClearForm.Text = "Add New"
            btnSaveChanges.Text = "Save Changes"
            btnCancel.Text = "Return to View Mode"
            lblChangeSelectCarrier.Text = "Change"
            pnlWOedit.Visible = False
            pnlWOinfo.Visible = False
            lblEmptyMessage.Visible = True
        Else
            pnlWOedit.Visible = False
            pnlWOinfo.Visible = True
        End If
    End Sub

    Private Sub btnDeleteLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteLoad.Click
        Dim woid As String = RadGrid1.SelectedValue.ToString
        Dim dba As New DBAccess()
        dba.CommandText = "DELETE FROM WorkOrder WHERE ID = @woid"
        dba.AddParameter("@woid", woid)
        dba.ExecuteNonQuery()
        dba.CommandText = "DELETE FROM Unloader WHERE LoadID = @woid"
        dba.AddParameter("@woid", woid)
        dba.ExecuteNonQuery()
        dba.CommandText = "DELETE FROM LoadImages WHERE WorkOrderID = @woid"
        dba.AddParameter("@woid", woid)
        dba.ExecuteNonQuery()
        dba.CommandText = "DELETE FROM VerifiedWorkOrders WHERE WorkOrderID = @woid"
        dba.AddParameter("@woid", woid)
        dba.ExecuteNonQuery()
        RadGrid1.Rebind()
        pnlWOedit.Visible = False
        pnlWOinfo.Visible = False
        lblEmptyMessage.Visible = True
        lblCreatedBy.Text = ""
    End Sub
#End Region

    Protected Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        Dim arg As String = e.Argument
        Dim sarg() As String
        If arg.Contains("Carrier") Then
            sarg = Split(arg, ":")
            txtCarrierIDv.Text = sarg(2)
            btnSaveChanges.Enabled = True
            'ElseIf Left(sarg, 6) = "Vendor" Then
            '    arg = Split(e.Argument, ":")
            '    txtVendorNumber.Text = arg(2)
        ElseIf arg.Contains("Unloader") Then
            sarg = Split(arg, "|")
            Dim wdal As New WorkOrderDAL()
            Dim newWorkOrder As WorkOrder
            Dim doAudit As Boolean = True
            Dim utl As New Utilities
            If Utilities.IsValidGuid(RadGrid1.SelectedValue.ToString) Then
                newWorkOrder = wdal.GetLoadByID(RadGrid1.SelectedValue.ToString)
            Else
                newWorkOrder = New WorkOrder
                newWorkOrder.LocationID = New Guid(cbLocations.SelectedValue.ToString)
                BuildWorkOrder(newWorkOrder)
                doAudit = False
            End If
            Dim emp As String = String.Empty
            Dim elst As List(Of String) = New List(Of String)
                If sarg(0) = "Unloader:none listed" Then
                    txtUnloaderIDlist.Text = "listcleared"
                Else
                    txtUnloaderIDlist.Text = sarg(1)
                    Dim ulList() As String = Split(sarg(1), ":")
                    Dim i As Integer = ulList.Length
                    For x = 0 To i - 1
                        emp = ulList(x)
                        elst.Add(emp)
                    Next
                End If
                newWorkOrder.Employee = elst
            If doAudit Then
                wdal.UpdateWorkOrder(newWorkOrder)
            Else
                wdal.AddWorkOrder(newWorkOrder, doAudit)
                RadGrid1.Rebind()
                clearForm()
                setSelectedWOid(newWorkOrder.ID)
                btnClearForm.Text = "Add New"
                btnSaveChanges.Text = "Save Changes"
                btnSaveChanges.CommandArgument = "Update"
                btnEdit.Visible = True
                ReloadForms(newWorkOrder.ID.ToString)
            End If


        ElseIf arg.Contains("VendorLookup") Then
                sarg = Split(arg, ":")
                Dim locaID As Guid = New Guid(cbLocations.SelectedValue)
                Dim vnum As String = sarg(1).ToUpper
                If vnum.Length > 2 Then
                    Dim dba As New DBAccess()
                    dba.CommandText = "SELECT Vendor.ID AS VendorID, Vendor.Number, Vendor.Name AS VendorName " &
                    "FROM Location INNER JOIN " &
                    "Vendor ON Location.ParentCompanyID = Vendor.ParentCompanyID " &
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
                    Else
                        txtVendorName.Text = "not found"
                    End If
                End If
            ElseIf arg.Contains("NewVendor") Or arg.Contains("UpdateVendor") Or arg.Contains("SelectVendor") Then
                sarg = Split(arg, ":")
            Dim tbVendorNumber As RadTextBox = CType(FindControl("txtVendorNumber"), RadTextBox)
            tbVendorNumber.Text = sarg(2)
            txtVendorName.Text = "<font size=""2"" color=""blue"">" & sarg(2) & " - " & sarg(3) & "</font>"
        End If

            End Sub

    Private Sub cbLocations_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        Dim utl As New Utilities
        radbut1.Visible = Utilities.haspics(cbLocations.SelectedValue)
        radbut1.Attributes.Add("onclick", "openPicEmailer('" & cbLocations.SelectedItem.Value & "');return false;")
        radbut1.Attributes.Add("onmouseover", "this.style.pointer='pointer';")
    End Sub
End Class

'dpLogDate.SelectedDate = wo.LogDate
'txtDoorNumber.Text = wo.DoorNumber
'txtAmount.Text = wo.Amount
'chkIsCash.Checked = wo.IsCash
'txtTruckNumber.Text = wo.TruckNumber
'txtTrailerNumber.Text = wo.TrailerNumber
'txtPurchaseOrder.Text = wo.PurchaseOrder
'txtVendorNumber.Text = wo.VendorNumber
'txtVendorName.Text = wo.VendorName
'txtPieces.Text = wo.Pieces
'txtPalletsReceived.Text = wo.PalletsReceived
'txtPalletsUnloaded.Text = wo.PalletsUnloaded
''            Dim tp As Telerik.Web.UI.RadTimePicker = FindControl("tpGateTime")
''            tp.SelectedDate = wo.GateTime
''            txtStartTime.Text = wo.StartTime
''            txtCompTime.Text = wo.CompTime
'txtBadPallets.Text = wo.BadPallets
'txtWeight.Text = wo.Weight
'txtRestacks.Text = wo.Restacks
'txtCheckNumber.Text = wo.CheckNumber
'txtBOL.Text = wo.BOL
'txtComments.Text = wo.Comments
