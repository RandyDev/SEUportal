Public Class ScheduledLoadImporterSetup
    Inherits System.Web.UI.Page
    Public ConfigID As Guid

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' populate Locations Combo Box (cbLocations on aspx page)
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
            divsetup.Visible = False
            If cbLocations.SelectedIndex > -1 Then
                cbDepartmentIDDefault.DataSource = ldal.GetDepartmentsByLocationID(cbLocations.SelectedValue.ToString)
                cbDepartmentIDDefault.DataTextField = "Name"
                cbDepartmentIDDefault.DataValueField = "ID"
                cbDepartmentIDDefault.DataBind()
            End If
            btnRemoveConfig.Visible = False
        End If
    End Sub

#Region "Buttons"
    Protected Sub btnRemoveConfig_Click(sender As Object, e As EventArgs) Handles btnRemoveConfig.Click
        Dim locaid As String = cbLocations.SelectedValue
        Dim configname As String = cbImportName.SelectedItem.Text
        Dim dba As New DBAccess
        dba.CommandText = "Select ConfigID FROM ImportConfig WHERE LocationID=@LocationID AND ConfigName = @ConfigName"
        dba.AddParameter("@LocationID", locaid)
        dba.AddParameter("@ConfigName", configname)
        Dim vConfigID As Guid = dba.ExecuteScalar
        dba.CommandText = "Delete from importconfig where configID= @ConfigID"
        dba.AddParameter("@configID", vConfigID)
        dba.ExecuteNonQuery()
        dba.CommandText = "Delete from importconfigdata where configID= @ConfigID"
        dba.AddParameter("@configID", vConfigID)
        Try
            dba.ExecuteNonQuery()
        Catch ex As Exception
            Dim exmsg As String = ex.Message
            Dim h As String = "abc"
        End Try
        populatecbImportName()
        cbImportName.ClearSelection()
        clearForm()
        btnSave.CommandArgument = "Insert"
        btnSave.Text = "Save NEW Configuration"
    End Sub

    Private Sub checkChecks(ByVal bool As Boolean)
        chkLogDate.Checked = bool
        chkLoadNumber.Checked = bool
        chkDepartment.Checked = bool
        chkLoadType.Checked = bool
        chkVendor.Checked = bool
        chkVendorNumber.Checked = bool
        chkPurchaseOrder.Checked = bool
        chkAmount.Checked = bool
        chkCheckNumber.Checked = bool
        chkSplitPaymentAmount.Checked = bool
        chkReceiptNumber.Checked = bool
        chkLoadDescription.Checked = bool
        chkCarrier.Checked = bool
        chkTruckNumber.Checked = bool
        chkTrailerNumber.Checked = bool
        chkAppointmentTime.Checked = bool
        chkGateTime.Checked = bool
        chkDockTime.Checked = bool
        chkStartTime.Checked = bool
        chkCompTime.Checked = bool
        chkPalletsUnloaded.Checked = bool
        chkDoorNumber.Checked = bool
        chkPieces.Checked = bool
        chkWeight.Checked = bool
        chkRestacks.Checked = bool
        chkPalletsReceived.Checked = bool
        chkBadPallets.Checked = bool
        chkNumberOfItems.Checked = bool
        chkBOL.Checked = bool
        chkComments.Checked = bool

    End Sub

    Private Sub enableChecks(ByVal bool As Boolean)
        chkLogDate.Visible = bool
        chkLoadNumber.Enabled = bool
        chkDepartment.Enabled = bool
        chkLoadType.Enabled = bool
        chkVendor.Enabled = bool
        chkVendorNumber.Enabled = bool
        chkPurchaseOrder.Enabled = bool
        chkAmount.Enabled = bool
        chkCheckNumber.Enabled = bool
        chkSplitPaymentAmount.Enabled = bool
        chkReceiptNumber.Enabled = bool
        chkLoadDescription.Enabled = bool
        chkCarrier.Enabled = bool
        chkTruckNumber.Enabled = bool
        chkTrailerNumber.Enabled = bool
        chkAppointmentTime.Enabled = bool
        chkGateTime.Enabled = bool
        chkDockTime.Enabled = bool
        chkStartTime.Enabled = bool
        chkCompTime.Enabled = bool
        chkPalletsUnloaded.Enabled = bool
        chkDoorNumber.Enabled = bool
        chkPieces.Enabled = bool
        chkWeight.Enabled = bool
        chkRestacks.Enabled = bool
        chkPalletsReceived.Enabled = bool
        chkBadPallets.Enabled = bool
        chkNumberOfItems.Enabled = bool
        chkBOL.Enabled = bool
        chkComments.Enabled = bool

    End Sub

    Private Sub btnNEWConfig_Click(sender As Object, e As EventArgs) Handles btnNEWConfig.Click
        divsetup.Visible = True
        txtImportName.Text = String.Empty
        numFirstRow.Value = 1
        rb1.Checked = False
        rb2.Checked = False
        rb3.Checked = False
        cbImportName.ClearSelection()
        clearForm()
        btnRemoveConfig.Visible = False
        btnSave.CommandArgument = "Insert"
        btnSave.Text = "Save NEW Configuration"
    End Sub

    Private Sub rb1_CheckedChanged(sender As Object, e As EventArgs) Handles rb1.CheckedChanged, rb2.CheckedChanged, rb3.CheckedChanged
        If rb2.Checked Or rb3.Checked Then
            enableChecks(True)
        Else
            enableChecks(False)
            checkChecks(False)
        End If

        setlogdatecontrols()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim LocationID As String = cbLocations.SelectedValue

        Select Case btnSave.CommandArgument
            Case "Insert"
                ConfigID = Guid.NewGuid
            Case "Update"
                ConfigID = New Guid(cbImportName.SelectedValue)
        End Select
        Dim StartRow As Integer = 0
        StartRow = numFirstRow.Value
        Dim vImportType As Integer = 0
        If rb1.Checked Then vImportType = 1
        If rb2.Checked Then vImportType = 2
        If rb3.Checked Then vImportType = 3
        Dim ConfigName As String = txtImportName.Text
        Dim hasdate As Boolean = rblistHasDate.SelectedValue
        Dim dba As New DBAccess
        Select Case btnSave.CommandArgument
            Case "Insert"
                dba.CommandText = "INSERT INTO ImportConfig (ConfigID, LocationID,ConfigName,StartRow,ImportType,hasDate) VALUES(@ConfigID,@LocationID,@ConfigName,@StartRow,@ImportType,@hasDate)"
                dba.AddParameter("@ConfigID", ConfigID)
                dba.AddParameter("@LocationID", LocationID)
                Select Case vImportType
                    Case 1
                        dba.AddParameter("@ConfigName", ConfigName & " - INSERT")

                    Case 2
                        dba.AddParameter("@ConfigName", ConfigName & " - UPDATE ")
                    Case 3
                        dba.AddParameter("@ConfigName", ConfigName & " - INSERT / UPDATE ")
                    Case Else
                        Exit Sub
                End Select
                dba.AddParameter("@StartRow", StartRow)
                dba.AddParameter("@ImportType", vImportType)
                dba.AddParameter("@hasDate", hasdate)
                Try
                    dba.ExecuteNonQuery()
                Catch ex As Exception
                    Dim exmsg As String = ex.Message
                    Dim h As String = "abc"
                End Try
                Dim StarRow As Integer = numFirstRow.Value

            Case "Update"
                dba.CommandText = "UPDATE ImportConfig SET LocationID=@LocationID,ConfigName=@ConfigName,StartRow=@StartRow,ImportType=@ImportType, hasDate=@hasDate Where ConfigID=@ConfigID"
                dba.AddParameter("@ConfigID", ConfigID)
                dba.AddParameter("@LocationID", LocationID)
                Select Case vImportType
                    Case 1
                        dba.AddParameter("@ConfigName", ConfigName & " - INSERT")
                    Case 2
                        dba.AddParameter("@ConfigName", ConfigName & " - UPDATE ")
                    Case 3
                        dba.AddParameter("@ConfigName", ConfigName & " - INSERT / UPDATE ")
                    Case Else
                        Exit Sub
                End Select
                dba.AddParameter("@StartRow", StartRow)
                dba.AddParameter("@ImportType", vImportType)
                dba.AddParameter("@hasDate", hasdate)
                Try
                    dba.ExecuteNonQuery()
                Catch ex As Exception
                    Dim exmsg As String = ex.Message
                    Dim h As String = "abc"
                End Try
        End Select
        Dim importlist As List(Of importConfigDataItem) = readForm()
        Select Case btnSave.CommandArgument
            Case "Insert"
                For Each icfg As importConfigDataItem In importlist
                    dba.CommandText = "INSERT INTO ImportConfigData (ConfigID,FieldName,canUpdate,ColumnLetter,DefaultValue) VALUES (@ConfigID,@FieldName,@canUpdate,@ColumnLetter,@DefaultValue)"
                    dba.AddParameter("@ConfigID", icfg.ConfigID)
                    dba.AddParameter("@FieldName", icfg.FieldName)
                    dba.AddParameter("@canUpdate", icfg.canUpdate)
                    dba.AddParameter("@ColumnLetter", icfg.columnLetter)
                    dba.AddParameter("@DefaultValue", icfg.DefaultValue)
                    Try
                        dba.ExecuteNonQuery()
                    Catch ex As Exception
                        Dim exmsg As String = ex.Message
                        Dim h As String = "abc"
                    End Try
                Next
            Case "Update"
                For Each icfg As importConfigDataItem In importlist
                    dba.CommandText = "UPDATE ImportConfigData SET FieldName=@FieldName,canUpdate=@canUpdate,ColumnLetter=@ColumnLetter,DefaultValue=@DefaultValue where ConfigID=@ConfigID and FieldName LIKE @FieldName"
                    dba.AddParameter("@ConfigID", icfg.ConfigID)
                    dba.AddParameter("@FieldName", icfg.FieldName)
                    dba.AddParameter("@canUpdate", icfg.canUpdate)
                    dba.AddParameter("@ColumnLetter", icfg.columnLetter)
                    dba.AddParameter("@DefaultValue", icfg.DefaultValue)
                    Try
                        dba.ExecuteNonQuery()
                    Catch ex As Exception
                        Dim exmsg As String = ex.Message
                        Dim h As String = "abc"
                    End Try
                Next

        End Select
        populatecbImportName()
        cbImportName.SelectedValue = ConfigID.ToString
        btnRemoveConfig.Visible = True
        btnSave.CommandArgument = "Update"
        btnSave.Text = "Update Configuration"
    End Sub

    Protected Sub setlogdatecontrols()
        Select Case rblistHasDate.SelectedIndex
            Case 0 'YES
                If rb1.Checked Then 'INS
                    chkLogDate.Visible = False
                    chkLogDate.Checked = False
                    chkLogDate.Checked = False
                    txtLogDateColumn.Visible = True

                Else 'UPD
                    chkLogDate.Visible = True
                    txtLogDateColumn.Visible = True
                End If

            Case 1
                If rb1.Checked Then 'INS
                    chkLogDate.Visible = False
                    chkLogDate.Checked = False
                    txtLogDateColumn.Visible = False
                    txtLogDateColumn.Text = String.Empty

                Else 'UPD
                    chkLogDate.Visible = True
                    txtLogDateColumn.Visible = False
                    txtLogDateColumn.Text = String.Empty
                End If


        End Select


    End Sub



#End Region

#Region "ComboBoxes"
    Private Sub cbLocations_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        Dim ldal As New locaDAL
        If cbLocations.SelectedIndex > -1 Then
            If Utilities.IsValidGuid(cbLocations.SelectedValue) Then

                cbDepartmentIDDefault.DataSource = ldal.GetDepartmentsByLocationID(cbLocations.SelectedValue.ToString)
                cbDepartmentIDDefault.DataTextField = "Name"
                cbDepartmentIDDefault.DataValueField = "ID"
                cbDepartmentIDDefault.DataBind()

                cbLoadTypeIDDefault.DataSource = ldal.getLoadTypesByLocationID()
                cbLoadTypeIDDefault.DataTextField = "Name"
                cbLoadTypeIDDefault.DataValueField = "ID"
                cbLoadTypeIDDefault.DataBind()
                cbLoadDescriptionID.DataSource = ldal.GetLoadDescriptionsByLocationID(cbLocations.SelectedValue.ToString)
                cbLoadDescriptionID.DataTextField = "Name"
                cbLoadDescriptionID.DataValueField = "ID"
                cbLoadDescriptionID.DataBind()
                populatecbImportName()
            End If
        End If
    End Sub

    Protected Sub populatecbImportName()
        Dim dba As New DBAccess
        cbImportName.Items.Clear()
        dba.CommandText = "SELECT ConfigID, ConfigName FROM ImportConfig WHERE LocationID=@LocationID"
        dba.AddParameter("@LocationID", cbLocations.SelectedValue.ToString)
        Dim dt As DataTable = New DataTable
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception
            Dim exmsg As String = ex.Message
            Dim h As String = "abc"
        End Try
        If dt.Rows.Count > 0 Then
            cbImportName.Enabled = True
            cbImportName.DataSource = dt
            cbImportName.DataValueField = "ConfigID"
            cbImportName.DataTextField = "ConfigName"
            cbImportName.DataBind()
            cbImportName.Visible = True
            cbImportName.EmptyMessage = "Select Configuration"
            cbImportName.SelectedIndex = -1
            cbImportName.Text = String.Empty
            btnNEWConfig.Visible = True
            btnRemoveConfig.Visible = False
        Else
            txtImportName.Text = String.Empty
            cbImportName.EmptyMessage = "None Defined"
            divsetup.Visible = True
            clearForm()
            btnSave.CommandArgument = "Insert"
            btnSave.Text = "Save NEW Configuration"
            cbImportName.ClearSelection()
            cbImportName.Text = String.Empty
            cbImportName.Visible = True
            btnNEWConfig.Visible = False
            btnRemoveConfig.Visible = False
        End If
    End Sub

    Protected Sub cbImportName_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbImportName.SelectedIndexChanged
        If cbImportName.SelectedIndex > -1 Then
            btnRemoveConfig.Visible = True
            divsetup.Visible = True
            Dim cfgid As String = cbImportName.SelectedValue
            populateform(cfgid)
            btnSave.CommandArgument = "Update"
            btnSave.Text = "Update Configuration"
        End If
    End Sub
#End Region

#Region "clear/populate form"
    Private Sub populateform(ByVal cfgid As String)
        clearForm()
        btnSave.CommandArgument = "Insert"
        btnSave.Text = "Save NEW Configuration"
        txtImportName.Text = Left(cbImportName.Text, InStr(cbImportName.Text, " - ") - 1)
        Dim radiotype As String = Right(cbImportName.Text, cbImportName.Text.Length - InStrRev(cbImportName.Text, " - ") - 2).Trim
        rb1.Checked = IIf(radiotype = "INSERT", True, False)
        rb2.Checked = IIf(radiotype = "UPDATE", True, False)
        rb3.Checked = IIf(radiotype = "INSERT / UPDATE", True, False)
        If rb2.Checked Or rb3.Checked Then
            enableChecks(True)
        Else
            enableChecks(False)
            checkChecks(False)
        End If
        'get values for edit
        Dim dba As New DBAccess
        dba.CommandText = "Select StartRow,hasDate FROM ImportConfig WHERE ConfigID = @ConfigID"
        dba.AddParameter("@ConfigID", cfgid)
        Dim strow As Integer = 1
        Dim dt As DataTable = New DataTable
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception
            Dim exmsg As String = ex.Message
            Dim h As String = "abc"
        End Try

        Dim vfirstRow As Integer = dt.Rows(0).Item("StartRow")
        numFirstRow.Value = vfirstRow
        Dim hd As Boolean = dt.Rows(0).Item("hasDate")
        rblistHasDate.SelectedIndex = (IIf(hd, 0, 1))
        Dim lst As New List(Of importConfigDataItem)
        lst = GetImportData(cfgid)
        Dim m As importConfigDataItem
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "LogDate")
        If Not m Is Nothing Then
            chkLogDate.Checked() = m.canUpdate
            txtLogDateColumn.Text = m.columnLetter
        End If
        setlogdatecontrols()
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Status")
        If Not m Is Nothing Then
            txtStatusColumn.Text = m.columnLetter
            txtStatusDefault.Text = m.DefaultValue

        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "LoadNumber")
        If Not m Is Nothing Then
            chkLoadNumber.Checked = m.canUpdate
            txtLoadNumberColumn.Text = m.columnLetter
            txtLoadNumberDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Department")
        If Not m Is Nothing Then
            chkDepartment.Checked = m.canUpdate
            txtDepartmentIDColumn.Text = m.columnLetter
            cbDepartmentIDDefault.SelectedValue = m.DefaultValue.ToString
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "LoadType")
        If Not m Is Nothing Then
            chkLoadType.Checked = m.canUpdate
            txtLoadTypeIDColumn.Text = m.columnLetter
            cbLoadTypeIDDefault.SelectedValue = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Vendor")
        If Not m Is Nothing Then
            chkVendor.Checked = m.canUpdate
            txtVendorColumn.Text = m.columnLetter
            txtVendorDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "VendorNumber")
        If Not m Is Nothing Then
            chkVendorNumber.Checked = m.canUpdate
            txtVendorNumberColumn.Text = m.columnLetter
            txtVendorNumberDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "PurchaseOrder")
        If Not m Is Nothing Then
            chkPurchaseOrder.Checked = m.canUpdate
            txtPurchaseOrderColumn.Text = m.columnLetter
            txtPurchaseOrderDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Amount")
        If Not m Is Nothing Then
            chkAmount.Checked = m.canUpdate
            txtAmountColumn.Text = m.columnLetter
            txtAmountDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "CheckNumber")
        If Not m Is Nothing Then
            chkCheckNumber.Checked = m.canUpdate
            txtCheckNumberColumn.Text = m.columnLetter
            txtCheckNumberDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "SplitPaymentAmount")
        If Not m Is Nothing Then
            chkSplitPaymentAmount.Checked = m.canUpdate
            txtSplitPaymentAmountColumn.Text = m.columnLetter
            txtSplitPaymentAmountDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "ReceiptNumber")
        If Not m Is Nothing Then
            chkReceiptNumber.Checked = m.canUpdate
            txtReceiptNumberColumn.Text = m.columnLetter
            txtReceiptNumberDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "LoadDescription")
        If Not m Is Nothing Then
            chkLoadDescription.Checked = m.canUpdate
            txtLoadDescriptionIDColumn.Text = m.columnLetter
            cbLoadDescriptionID.SelectedValue = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Carrier")
        If Not m Is Nothing Then
            chkCarrier.Checked = m.canUpdate
            txtCarrierIDColumn.Text = m.columnLetter
            cbCarrierOD.SelectedValue = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "TruckNumber")
        If Not m Is Nothing Then
            chkTruckNumber.Checked = m.canUpdate
            txtTruckNumberColumn.Text = m.columnLetter
            txtTruckNumberDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "TrailerNumber")
        If Not m Is Nothing Then
            chkTrailerNumber.Checked = m.canUpdate
            txtTrailerNumberColumn.Text = m.columnLetter
            txtTrailerNumberDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "AppointmentTime")
        If Not m Is Nothing Then
            chkAppointmentTime.Checked = m.canUpdate
            txtAppointmentTimeColumn.Text = m.columnLetter
            txtAppointmentTimeDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "GateTime")
        If Not m Is Nothing Then
            chkGateTime.Checked = m.canUpdate
            txtGateTimeColumn.Text = m.columnLetter
            txtGateTimeDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "DockTime")
        If Not m Is Nothing Then
            chkDockTime.Checked = m.canUpdate
            txtDockTimeColumn.Text = m.columnLetter
            txtDockTimeDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "StartTime")
        If Not m Is Nothing Then
            chkStartTime.Checked = m.canUpdate
            txtStartTimeColumn.Text = m.columnLetter
            txtStartTimeDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "CompTime")
        If Not m Is Nothing Then
            chkCompTime.Checked = m.canUpdate
            txtCompTimeColumn.Text = m.columnLetter
            txtCompTimeDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "PalletsUnloaded")
        If Not m Is Nothing Then
            chkPalletsUnloaded.Checked = m.canUpdate
            txtPalletsUnloadedColumn.Text = m.columnLetter
            txtPalletsUnloadedDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "DoorNumber")
        If Not m Is Nothing Then
            chkDoorNumber.Checked = m.canUpdate
            txtDoorNumberColumn.Text = m.columnLetter
            txtDoorNumberDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Pieces")
        If Not m Is Nothing Then
            chkPieces.Checked = m.canUpdate
            txtPiecesColumn.Text = m.columnLetter
            txtPiecesDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Weight")
        If Not m Is Nothing Then
            chkWeight.Checked = m.canUpdate
            txtWeightColumn.Text = m.columnLetter
            txtWeightDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Restacks")
        If Not m Is Nothing Then
            chkRestacks.Checked = m.canUpdate
            txtRestacksColumn.Text = m.columnLetter
            txtRestacksDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "PalletsReceived")
        If Not m Is Nothing Then
            chkPalletsReceived.Checked = m.canUpdate
            txtPalletsReceivedColumn.Text = m.columnLetter
            txtPalletsReceivedDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "BadPallets")
        If Not m Is Nothing Then
            chkBadPallets.Checked = m.canUpdate
            txtBadPalletsColumn.Text = m.columnLetter
            txtBadPalletsDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "NumberOfItems")
        If Not m Is Nothing Then
            chkNumberOfItems.Checked = m.canUpdate
            txtNumberOfItemsColumn.Text = m.columnLetter
            txtNumberOfItemsDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "BOL")
        If Not m Is Nothing Then
            chkBOL.Checked = m.canUpdate
            txtBOLColumn.Text = m.columnLetter
            txtBOLDefault.Text = m.DefaultValue
        End If
        m = lst.Find(Function(n As importConfigDataItem) n.FieldName = "Comments")
        If Not m Is Nothing Then
            chkComments.Checked = m.canUpdate
            txtCommentsColumn.Text = m.columnLetter
            txtCommentsDefault.Text = m.DefaultValue
        End If
    End Sub

    Private Sub clearForm()
        txtImportName.Text = String.Empty
        numFirstRow.Value = 1
        rblistHasDate.SelectedIndex = 1
        rb1.Checked = True
        rb2.Checked = False
        rb3.Checked = False
        checkChecks(False)
        enableChecks(False)

        setlogdatecontrols()
        txtLogDateColumn.Text = String.Empty
        txtStatusColumn.Text = String.Empty
        txtStatusDefault.Text = String.Empty
        txtLoadNumberColumn.Text = String.Empty
        txtLoadNumberDefault.Text = String.Empty
        txtDepartmentIDColumn.Text = String.Empty
        cbDepartmentIDDefault.ClearSelection()
        txtLoadTypeIDColumn.Text = String.Empty
        cbLoadTypeIDDefault.ClearSelection()
        txtVendorColumn.Text = String.Empty
        txtVendorDefault.Text = String.Empty
        txtVendorNumberColumn.Text = String.Empty
        txtVendorNumberDefault.Text = String.Empty
        txtPurchaseOrderColumn.Text = String.Empty
        txtPurchaseOrderDefault.Text = String.Empty
        txtAmountColumn.Text = String.Empty
        txtAmountDefault.Text = String.Empty
        txtCheckNumberColumn.Text = String.Empty
        txtCheckNumberDefault.Text = String.Empty
        txtSplitPaymentAmountColumn.Text = String.Empty
        txtSplitPaymentAmountDefault.Text = String.Empty
        txtReceiptNumberColumn.Text = String.Empty
        txtReceiptNumberDefault.Text = String.Empty
        txtLoadNumberColumn.Text = String.Empty
        txtLoadNumberDefault.Text = String.Empty
        txtLoadDescriptionIDColumn.Text = String.Empty
        txtLoadDescriptionIDColumn.Text = String.Empty
        cbLoadDescriptionID.ClearSelection()
        txtCarrierIDColumn.Text = String.Empty
        cbCarrierOD.Text = ""
        txtTruckNumberColumn.Text = String.Empty
        txtTruckNumberDefault.Text = String.Empty
        txtTrailerNumberColumn.Text = String.Empty
        txtTrailerNumberDefault.Text = String.Empty
        txtAppointmentTimeColumn.Text = String.Empty
        txtAppointmentTimeDefault.Text = String.Empty
        txtGateTimeColumn.Text = String.Empty
        txtGateTimeDefault.Text = String.Empty
        txtDockTimeColumn.Text = String.Empty
        txtDockTimeDefault.Text = String.Empty
        txtStartTimeColumn.Text = String.Empty
        txtStartTimeDefault.Text = String.Empty
        txtCompTimeColumn.Text = String.Empty
        txtCompTimeDefault.Text = String.Empty
        txtCommentsColumn.Text = String.Empty
        txtCommentsColumn.Text = String.Empty
        txtPalletsUnloadedColumn.Text = String.Empty
        txtPalletsUnloadedDefault.Text = String.Empty
        txtDoorNumberColumn.Text = String.Empty
        txtDoorNumberDefault.Text = String.Empty
        txtWeightColumn.Text = String.Empty
        txtWeightDefault.Text = String.Empty
        txtPiecesColumn.Text = String.Empty
        txtPiecesDefault.Text = String.Empty
        txtLoadNumberColumn.Text = String.Empty
        txtLoadNumberDefault.Text = String.Empty
        txtRestacksColumn.Text = String.Empty
        txtRestacksDefault.Text = String.Empty
        txtPalletsReceivedColumn.Text = String.Empty
        txtPalletsReceivedDefault.Text = String.Empty
        txtLoadNumberColumn.Text = String.Empty
        txtLoadNumberDefault.Text = String.Empty
        txtBadPalletsColumn.Text = String.Empty
        txtBadPalletsDefault.Text = String.Empty
        txtNumberOfItemsColumn.Text = String.Empty
        txtNumberOfItemsDefault.Text = String.Empty
        txtLoadNumberColumn.Text = String.Empty
        txtLoadNumberDefault.Text = String.Empty
        txtBOLColumn.Text = String.Empty
        txtBOLDefault.Text = String.Empty
        txtCommentsColumn.Text = String.Empty
        txtCommentsDefault.Text = String.Empty
    End Sub
#End Region

#Region "get data"
    Protected Function readForm() As List(Of importConfigDataItem)
        Dim importlist As New List(Of importConfigDataItem)
        Dim ic As New importConfigDataItem
        '***************************************read form and create list
        ic = New importConfigDataItem '*************************************LogDate**************
        ic.ConfigID = ConfigID
        ic.FieldName = "LogDate"
        ic.canUpdate = chkLogDate.Checked
        ic.columnLetter = txtLogDateColumn.Text.Trim
        ic.DefaultValue = String.Empty
        importlist.Add(ic)
        ic = New importConfigDataItem '*************************************xStatus**************
        ic.ConfigID = ConfigID
        ic.FieldName = "Status"
        ic.columnLetter = txtStatusColumn.Text
        If txtStatusDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtStatusDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtStatusDefault.Text
        End If
        ic.DefaultValue = txtStatusDefault.Text
        importlist.Add(ic)
        ic = New importConfigDataItem       '************************************LoadNumber***************
        ic.ConfigID = ConfigID
        ic.FieldName = "LoadNumber"
        ic.canUpdate = chkLoadNumber.Checked
        ic.columnLetter = txtLoadNumberColumn.Text
        If txtLoadNumberDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtLoadNumberDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtLoadNumberDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '************************************Department***************
        ic.ConfigID = ConfigID
        ic.FieldName = "Department"
        ic.canUpdate = chkDepartment.Checked
        ic.columnLetter = txtDepartmentIDColumn.Text
        ic.DefaultValue = cbDepartmentIDDefault.SelectedValue
        If ic.DefaultValue = "" Then ic.DefaultValue = Utilities.zeroGuid.ToString
        importlist.Add(ic)
        ic = New importConfigDataItem        '**************************************LoadType*************
        ic.ConfigID = ConfigID
        ic.FieldName = "LoadType"
        ic.canUpdate = chkLoadType.Checked
        ic.columnLetter = txtLoadTypeIDColumn.Text
        ic.DefaultValue = cbLoadTypeIDDefault.SelectedValue
        If ic.DefaultValue = "" Then ic.DefaultValue = Utilities.zeroGuid.ToString
        importlist.Add(ic)
        ic = New importConfigDataItem        '**************************************Vendor*************
        ic.ConfigID = ConfigID
        ic.FieldName = "Vendor"
        ic.canUpdate = chkVendor.Checked
        ic.columnLetter = txtVendorColumn.Text
        If txtVendorNumberDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtVendorDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtVendorDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '**************************************VendorNumber*************
        ic.ConfigID = ConfigID
        ic.FieldName = "VendorNumber"
        ic.canUpdate = chkVendorNumber.Checked
        ic.columnLetter = txtVendorNumberColumn.Text
        If txtVendorNumberDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtVendorNumberDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtVendorNumberDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '**************************************PurchaseOrder*************
        ic.ConfigID = ConfigID
        ic.FieldName = "PurchaseOrder"
        ic.canUpdate = chkPurchaseOrder.Checked
        ic.columnLetter = txtPurchaseOrderColumn.Text
        If txtPurchaseOrderDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtPurchaseOrderDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtPurchaseOrderDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '**************************************Amount up*************
        ic.ConfigID = ConfigID
        ic.FieldName = "Amount"
        ic.canUpdate = chkAmount.Checked
        ic.columnLetter = txtAmountColumn.Text
        If txtAmountDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtAmountDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtAmountDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '**************************************CheckNumber*************
        ic.ConfigID = ConfigID
        ic.FieldName = "CheckNumber"
        ic.canUpdate = chkCheckNumber.Checked
        ic.columnLetter = txtCheckNumberColumn.Text
        If txtCheckNumberDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtCheckNumberDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtCheckNumberDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '**************************************SplitPaymentAmount *************
        ic.ConfigID = ConfigID
        ic.FieldName = "SplitPaymentAmount"
        ic.canUpdate = chkSplitPaymentAmount.Checked
        ic.columnLetter = txtSplitPaymentAmountColumn.Text
        If txtSplitPaymentAmountDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtSplitPaymentAmountDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtSplitPaymentAmountDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '**************************************ReceiptNumber*************
        ic.ConfigID = ConfigID
        ic.FieldName = "ReceiptNumber"
        ic.canUpdate = chkReceiptNumber.Checked
        ic.columnLetter = txtReceiptNumberColumn.Text
        If txtReceiptNumberDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtReceiptNumberDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtReceiptNumberDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '***********XXX*************************LoadDescription***************
        ic.ConfigID = ConfigID
        ic.FieldName = "LoadDescription"
        ic.canUpdate = chkLoadDescription.Checked
        ic.columnLetter = txtLoadDescriptionIDColumn.Text
        ic.DefaultValue = cbLoadDescriptionID.SelectedValue
        importlist.Add(ic)
        ic = New importConfigDataItem        '**************************************Carrier*************
        ic.ConfigID = ConfigID
        ic.FieldName = "Carrier"
        ic.canUpdate = chkCarrier.Checked
        ic.columnLetter = txtCarrierIDColumn.Text
        ic.DefaultValue = cbCarrierOD.SelectedValue
        If ic.DefaultValue = "" Then ic.DefaultValue = Utilities.zeroGuid.ToString
        importlist.Add(ic)
        ic = New importConfigDataItem        '*************************************TruckNumber**************
        ic.ConfigID = ConfigID
        ic.FieldName = "TruckNumber"
        ic.canUpdate = chkTruckNumber.Checked
        ic.columnLetter = txtTruckNumberColumn.Text
        If txtTruckNumberDefault.Text.Trim() = "" Then
            If txtTruckNumberDefault.EmptyMessage = "Null" Then ic.DefaultValue = String.Empty
        Else
            ic.DefaultValue = txtTruckNumberDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '**************************************TrailerNumber*************
        ic.ConfigID = ConfigID
        ic.FieldName = "TrailerNumber"
        ic.canUpdate = chkTrailerNumber.Checked
        ic.columnLetter = txtTrailerNumberColumn.Text
        If txtTrailerNumberDefault.Text.Trim() = "" Then
            If txtTrailerNumberDefault.EmptyMessage = "Null" Then ic.DefaultValue = String.Empty
        Else
            ic.DefaultValue = txtTrailerNumberDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '**************************************AppointmentTime*************
        ic.ConfigID = ConfigID
        ic.FieldName = "AppointmentTime"
        ic.canUpdate = chkAppointmentTime.Checked
        ic.columnLetter = txtAppointmentTimeColumn.Text
        If txtAppointmentTimeDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtAppointmentTimeDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtAppointmentTimeDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '**************************************xGateTime*************
        ic.ConfigID = ConfigID
        ic.FieldName = "GateTime"
        ic.canUpdate = chkGateTime.Checked
        ic.columnLetter = txtGateTimeColumn.Text
        If txtGateTimeDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtGateTimeDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtGateTimeDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '**************************************xDockTime*************
        ic.ConfigID = ConfigID
        ic.FieldName = "DockTime"
        ic.canUpdate = chkDockTime.Checked
        ic.columnLetter = txtDockTimeColumn.Text
        If txtDockTimeDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtDockTimeDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtDockTimeDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '*************************************xStartTime**************
        ic.ConfigID = ConfigID
        ic.FieldName = "StartTime"
        ic.canUpdate = chkStartTime.Checked
        ic.columnLetter = txtStartTimeColumn.Text
        If txtStartTimeDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtStartTimeDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtStartTimeDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem         '*************************************xCompTime**************
        ic.ConfigID = ConfigID
        ic.FieldName = "CompTime"
        ic.canUpdate = chkCompTime.Checked
        ic.columnLetter = txtCompTimeColumn.Text
        If txtCompTimeDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtCompTimeDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtCompTimeDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '************************************PalletsUnloaded*************** 
        ic.ConfigID = ConfigID
        ic.FieldName = "PalletsUnloaded"
        ic.canUpdate = chkPalletsUnloaded.Checked
        ic.columnLetter = txtPalletsUnloadedColumn.Text
        If txtPalletsUnloadedDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtPalletsUnloadedDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtPalletsUnloadedDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '************************************xDoorNumber***************
        ic.ConfigID = ConfigID
        ic.FieldName = "DoorNumber"
        ic.canUpdate = chkDoorNumber.Checked
        ic.columnLetter = txtDoorNumberColumn.Text
        If txtDoorNumberDefault.Text.Trim() = "" Then
            If txtDoorNumberDefault.EmptyMessage = "Null" Then ic.DefaultValue = String.Empty
        Else
            ic.DefaultValue = txtDoorNumberDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '**************************************Pieces up*************
        ic.ConfigID = ConfigID
        ic.FieldName = "Pieces"
        ic.canUpdate = chkPieces.Checked
        ic.columnLetter = txtPiecesColumn.Text
        If txtPiecesDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtPiecesDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtPiecesDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '**************************************Weight*************
        ic.ConfigID = ConfigID
        ic.FieldName = "Weight"
        ic.canUpdate = chkWeight.Checked
        ic.columnLetter = txtWeightColumn.Text
        If txtWeightDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtWeightDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtWeightDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '****************************************xRestacks***********
        ic.ConfigID = ConfigID
        ic.FieldName = "Restacks"
        ic.canUpdate = chkRestacks.Checked
        ic.columnLetter = txtRestacksColumn.Text
        If txtRestacksDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtRestacksDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtRestacksDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '****************************************PalletsReceived up***********
        ic.ConfigID = ConfigID
        ic.canUpdate = chkPalletsReceived.Checked
        ic.FieldName = "PalletsReceived"
        ic.canUpdate = chkPalletsReceived.Checked
        ic.columnLetter = txtPalletsReceivedColumn.Text
        If txtPalletsReceivedDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtPalletsReceivedDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtPalletsReceivedDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '***************************************xBadPallets************
        ic.ConfigID = ConfigID
        ic.FieldName = "BadPallets"
        ic.canUpdate = chkBadPallets.Checked
        ic.columnLetter = txtBadPalletsColumn.Text
        If txtBadPalletsDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtBadPalletsDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtBadPalletsDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '*******************************************NumberOfItems up********
        ic.ConfigID = ConfigID
        ic.FieldName = "NumberOfItems"
        ic.canUpdate = chkNumberOfItems.Checked
        ic.columnLetter = txtNumberOfItemsColumn.Text
        If txtNumberOfItemsDefault.Text.Trim() = "" Then
            ic.DefaultValue = txtNumberOfItemsDefault.EmptyMessage.ToString
        Else
            ic.DefaultValue = txtNumberOfItemsDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '*******************************************BOL********
        ic.ConfigID = ConfigID
        ic.FieldName = "BOL"
        ic.canUpdate = chkBOL.Checked
        ic.columnLetter = txtBOLColumn.Text
        If txtBOLDefault.Text.Trim() = "" Then
            If txtBOLDefault.EmptyMessage = "Null" Then ic.DefaultValue = String.Empty
        Else
            ic.DefaultValue = txtBOLDefault.Text
        End If
        importlist.Add(ic)
        ic = New importConfigDataItem        '******************************************Comments*********
        ic.ConfigID = ConfigID
        ic.FieldName = "Comments"
        ic.canUpdate = chkComments.Checked
        ic.columnLetter = txtCommentsColumn.Text
        If txtCommentsDefault.Text.Trim() = "" Then
            If txtCommentsDefault.EmptyMessage = "Null" Then ic.DefaultValue = String.Empty
        Else
            ic.DefaultValue = txtCommentsDefault.Text
        End If
        importlist.Add(ic)
        Return importlist
    End Function

    Private Function GetImportData(ByVal cfgid As String) As List(Of importConfigDataItem)
        Dim retlist As New List(Of importConfigDataItem)
        Dim icdi As importConfigDataItem = New importConfigDataItem
        Dim dba As New DBAccess
        dba.CommandText = "SELECT * FROM ImportConfigData WHERE ConfigID=@ConfigID"
        dba.AddParameter("@ConfigID", cfgid)
        Dim dt As DataTable = New DataTable
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception
            Dim exmsg As String = ex.Message
            Dim h As String = "abc"
        End Try
        For Each row In dt.Rows
            icdi = New importConfigDataItem
            icdi.FieldName = row.item("FieldName")
            icdi.canUpdate = row.item("canUpdate")
            icdi.columnLetter = row.item("columnLetter")
            icdi.DefaultValue = row.item("DefaultValue")
            retlist.Add(icdi)
        Next
        Return retlist
    End Function

    Private Function fromsheet(ByVal rownum As Integer, ByVal colnum As Integer) As String
        Dim retstr As String = String.Empty
        'go to spreadsheet array for data
        Return retstr
    End Function
#End Region

    Public Sub New()

    End Sub

    Private Sub rblistHasDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblistHasDate.SelectedIndexChanged
        setlogdatecontrols()


    End Sub



End Class


'Private Shared Function GetData(ByVal text As String) As DataTable
'    Dim dba As New DBAccess
'    dba.CommandText = "SELECT * from Carrier WHERE Name LIKE @text + '%'"
'    dba.AddParameter("@text", text)
'    Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
'    Return dt
'End Function
