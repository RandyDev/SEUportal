Imports Telerik.Web.UI

Public Class FreightIssuePictures
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager") Or User.IsInRole("Client")
            dpEndDate.SelectedDate = FormatDateTime(Date.Now(), DateFormat.ShortDate)
            dpStartDate.SelectedDate = FormatDateTime(DateAdd(DateInterval.Day, -14, Date.Now), DateFormat.ShortDate)
            RadGrid1.Visible = False
        End If
        lblEditLoadImages.Visible = Not User.IsInRole("Client")
    End Sub

    Private Sub ReloadForms(ByVal woid As String)
        Dim rw As DataRow
        Dim dba As New DBAccess()
        pnlDetail.Visible = True
        dba.CommandText = "SELECT Location.Name AS Location, WorkOrder.LogDate, WorkOrder.PurchaseOrder, Department.Name AS Department, WorkOrder.BadPallets, WorkOrder.Restacks, " & _
            "Vendor.Name AS Vendor, Vendor.Number AS VendorNumber, Carrier.Name AS Carrier, WorkOrder.TrailerNumber, WorkOrder.ID " & _
            "FROM WorkOrder INNER JOIN " & _
            "Location ON WorkOrder.LocationID = Location.ID LEFT OUTER JOIN " & _
            "Carrier ON WorkOrder.CarrierID = Carrier.ID LEFT OUTER JOIN " & _
            "Department ON WorkOrder.DepartmentID = Department.ID LEFT OUTER JOIN " & _
            "Vendor ON WorkOrder.CustomerID = Vendor.ID " & _
            "WHERE WorkOrder.ID = @woid"
        dba.AddParameter("@woid", woid)
        Try
            rw = dba.ExecuteDataSet.Tables(0).Rows(0)
            lblLocation.Text = rw("Location")

            lblDate.Text = FormatDateTime(rw("LogDate"), DateFormat.ShortDate).ToString
            lblPurchaseOrder.Text = rw("PurchaseOrder")

            lblDepartment.Text = rw("Department")
            lblBadPallets.Text = rw("BadPallets")
            lblRestacks.Text = rw("Restacks")
            lblVendorNumber.Text = rw("VendorNumber")
            lblVendor.Text = rw("Vendor")
            lblCarrier.Text = rw("Carrier")
            lblTrailerNumber.Text = rw("TrailerNumber")
            lblEditLoadImages.Text = "<span style=""font-size:11px;color:blue"" onmouseover=""this.style.cursor='pointer'"" onclick=""openLoadImages('" & woid & "');"">Manage Pictures</span>"
            lblViewPhotoReport.Text = "<span style=""font-size:11px;color:blue"" onmouseover=""this.style.cursor='pointer'"" onclick=""openPDFwin('" & woid & "');"">View Photo Report</span>"

        Catch ex As Exception

        End Try
    End Sub


    Private Sub RadGrid1_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If e.CommandName = "RowClick" Then
            Dim woid As Guid = RadGrid1.SelectedValue
            ReloadForms(woid.ToString)
            'For Each dataItem As GridDataItem In RadGrid1.MasterTableView.Items
            '    TryCast(dataItem.FindControl("CheckBox1"), CheckBox).Checked = headerCheckBox.Checked
            '    dataItem.Selected = headerCheckBox.Checked
            'Next
        End If
    End Sub

    Private Sub RadGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim sDate As Date = dpStartDate.SelectedDate
        Dim eDate As Date = dpEndDate.SelectedDate
        eDate = DateAdd(DateInterval.Day, 1, eDate)
        Dim dba As New DBAccess()
        Dim locaID As String = String.Empty
        '        Dim defaultGridWidth As Integer = 776
        '        Dim defaultMasterGridWidth As Integer = 660

        If cbLocations.SelectedIndex <> -1 Then
            locaID = cbLocations.SelectedValue
            RadGrid1.MasterTableView.GetColumn("LocationName").Visible = False
            '            RadGrid1.Width = defaultGridWidth - 105
            '            RadGrid1.MasterTableView.Width = defaultMasterGridWidth - 105
        Else
            RadGrid1.MasterTableView.GetColumn("LocationName").Visible = True
            '            RadGrid1.Width = defaultGridWidth
            '            RadGrid1.MasterTableView.Width = defaultMasterGridWidth

        End If
        Dim sql As String = "SELECT DISTINCT (SELECT COUNT(WorkOrderID) AS PicCount " & _
            "FROM LoadImages " & _
            "GROUP BY WorkOrderID " & _
            "HAVING (WorkOrderID = WorkOrder.ID)) AS PicCount, Location.Name AS Location, WorkOrder.LogDate, WorkOrder.PurchaseOrder, Department.Name AS Department, WorkOrder.BadPallets, WorkOrder.Restacks, " & _
            "Vendor.Name AS Vendor, Vendor.Number AS VendorNumber, Carrier.Name AS Carrier, WorkOrder.TrailerNumber, WorkOrder.ID " & _
            "FROM WorkOrder INNER JOIN " & _
            "Location ON WorkOrder.LocationID = Location.ID LEFT OUTER JOIN " & _
            "Carrier ON WorkOrder.CarrierID = Carrier.ID LEFT OUTER JOIN " & _
            "Department ON WorkOrder.DepartmentID = Department.ID LEFT OUTER JOIN " & _
            "Vendor ON WorkOrder.CustomerID = Vendor.ID RIGHT OUTER JOIN " & _
            "LoadImages AS LoadImages_1 ON WorkOrder.ID = LoadImages_1.WorkOrderID "
        If locaID = String.Empty Then
            sql &= "WHERE WorkOrder.LogDate >= @startDate AND WorkOrder.LogDate < @endDate " & _
                "ORDER BY Location.Name ASC, WorkOrder.LogDate DESC "
        Else
            sql &= "WHERE WorkOrder.LogDate >= @startDate AND WorkOrder.LogDate < @endDate AND WorkOrder.LocationID = @locaID " & _
                "ORDER BY WorkOrder.LogDate DESC "
        End If
        dba.CommandText = sql
        dba.AddParameter("@startDate", sDate)
        dba.AddParameter("@endDate", eDate)
        If Not locaID = String.Empty Then
            dba.AddParameter("@locaID", locaID)
        End If
        Try
            Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
            RadGrid1.DataSource = dt
        Catch ex As Exception

        End Try
    End Sub



    Protected Sub ToggleRowSelection(ByVal sender As Object, ByVal e As EventArgs)
        '        CType(CType(sender, CheckBox).NamingContainer, GridItem).Selected = CType(sender, CheckBox).Checked
    End Sub
    Private Sub btnShowRecords_Click(sender As Object, e As System.EventArgs) Handles btnShowRecords.Click
        RadGrid1.Visible = True
        lnkButton1.Visible = True
        pnlDetail.Visible = False
        RadGrid1.Rebind()
    End Sub
    Private Sub RadGrid1_ItemCreated(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles RadGrid1.ItemCreated
        'If TypeOf e.Item Is GridDataItem Then
        '    AddHandler e.Item.PreRender, AddressOf RadGrid1_ItemPreRender
        'End If
    End Sub

    Protected Sub RadGrid1_ItemPreRender(ByVal sender As Object, ByVal e As EventArgs)
        '       CType(CType(sender, GridDataItem)("CheckBoxTemplateColumn").FindControl("CheckBox1"), CheckBox).Checked = CType(sender, GridDataItem).Selected
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles RadGrid1.PreRender
        '        Literal1.Text = String.Format("<h3 class=""qsfSubtitle"">Selected rows count is: {0}</h3>", RadGrid1.SelectedItems.Count)
    End Sub

    Protected Sub RadGrid1_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles RadGrid1.PreRender
        '        Dim headerItem As GridHeaderItem = CType(RadGrid1.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
        '        CType(headerItem.FindControl("headerChkbox"), CheckBox).Checked = RadGrid1.SelectedItems.Count = RadGrid1.Items.Count
    End Sub

    Private Sub cbLocations_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        pnlDetail.Visible = False
    End Sub
End Class