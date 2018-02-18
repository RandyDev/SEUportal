Imports Telerik.Web.UI

Public Class FreightIssues
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            dpStartDate.SelectedDate = DateAdd(DateInterval.Day, -14, Date.Now())
            dpEndDate.SelectedDate = Date.Now()
            ' populate Locations Combo Box (cbLocations on aspx page)
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
            lblCopy.Text = "<br /><br /><br /><br /><br /><span style=""font-size:24px; font-weight:bold; color:#cfcfcf;""><center>Freight Issues Summary Report</center></span>"
            RadGrid1.Visible = False
        End If
    End Sub

    Private Sub RadGrid1_GroupsChanging(sender As Object, e As Telerik.Web.UI.GridGroupsChangingEventArgs) Handles RadGrid1.GroupsChanging
        If (e.Action = GridGroupsChangingAction.Group) Then
            RadGrid1.MasterTableView.GetColumn(e.Expression.GroupByFields(0).FieldName).Visible = False
        ElseIf (e.Action = GridGroupsChangingAction.Ungroup) Then
            RadGrid1.MasterTableView.GetColumnSafe(e.Expression.GroupByFields(0).FieldName).Visible = True
        End If
    End Sub

    Private Sub RadGrid1_Init(sender As Object, e As System.EventArgs) Handles RadGrid1.Init
        Dim menu As GridFilterMenu = RadGrid1.FilterMenu
        Dim i As Integer = 0
        While i < menu.Items.Count
            If menu.Items(i).Text = "NoFilter" Or _
               menu.Items(i).Text = "StartsWith" Or _
               menu.Items(i).Text = "Contains" Or _
               menu.Items(i).Text = "EqualTo" Then
                i = i + 1
            Else
                menu.Items.RemoveAt(i)
            End If
        End While
        'NoFilter, Contains, DoesNotContain, StartsWith, EndsWith, EqualTo, NotEqualTo, GreaterThan, LessThan, 
        'GreaterThanOrEqualTo, LessThanOrEqualTo, Between, NotBetween, NotBetween, IsEmpth, NotIsEmpty, IsNull, NotIsNull

    End Sub

    Private Sub RadGrid1_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemCreated
        If TypeOf e.Item Is GridFilteringItem Then
            Dim filteringItem As GridFilteringItem = CType(e.Item, GridFilteringItem)
            'set dimensions for the filter textbox   
            Dim box As TextBox = CType(filteringItem("VendorNumber").Controls(0), TextBox)
            box.Width = Unit.Pixel(45)
            Dim box2 As TextBox = CType(filteringItem("PurchaseOrder").Controls(0), TextBox)
            box2.Width = Unit.Pixel(45)
            Dim box3 As TextBox = CType(filteringItem("Vendor").Controls(0), TextBox)
            box3.Width = Unit.Pixel(110)
            Dim box4 As TextBox = CType(filteringItem("Carrier").Controls(0), TextBox)
            box4.Width = Unit.Pixel(110)
            Dim box5 As TextBox = CType(filteringItem("Comments").Controls(0), TextBox)
            box5.Width = Unit.Pixel(150)
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
            RadGrid1.MasterTableView.GetColumn("Location").Visible = False
            '            RadGrid1.Width = defaultGridWidth - 105
            '            RadGrid1.MasterTableView.Width = defaultMasterGridWidth - 105
            'Else
            '    RadGrid1.MasterTableView.GetColumn("Location").Visible = True
            '            RadGrid1.Width = defaultGridWidth
            '            RadGrid1.MasterTableView.Width = defaultMasterGridWidth

        End If
        Dim sql As String = "SELECT DISTINCT (SELECT COUNT(WorkOrderID) AS PicCount " & _
            "FROM LoadImages " & _
            "GROUP BY WorkOrderID " & _
            "HAVING (WorkOrderID = WorkOrder.ID)) AS PicCount, Location.Name AS Location, WorkOrder.LogDate, WorkOrder.PurchaseOrder, Department.Name AS Department, WorkOrder.BadPallets, WorkOrder.Restacks, " & _
            "Vendor.Name AS Vendor, Vendor.Number AS VendorNumber, Carrier.Name AS Carrier, WorkOrder.TrailerNumber, WorkOrder.ID, WorkOrder.Comments " & _
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
            mngBtnExcelz.Visible = dt.Rows.Count > 0
        Catch ex As Exception

        End Try
        lblCopy.Visible = False
    End Sub




    'Protected Sub RadGrid1_GroupsChanging(ByVal source As Object, ByVal e As Telerik.WebControls.GridGroupsChangingEventArgs) Handles RadGrid1.GroupsChanging
    '    If (e.Action = GridGroupsChangingAction.Group) Then
    '        RadGrid1.MasterTableView.GetColumnSafe(e.Expression.GroupByFields(0).FieldName).Visible = False
    '    ElseIf (e.Action = GridGroupsChangingAction.Ungroup) Then
    '        RadGrid1.MasterTableView.GetColumnSafe(e.Expression.GroupByFields(0).FieldName).Visible = True
    '    End If
    'End Sub


    Private Sub RadButton1_Click(sender As Object, e As System.EventArgs) Handles RadButton1.Click
        If cbLocations.SelectedIndex = -1 Then
            RadGrid1.Visible = True
            RadGrid1.Rebind()
        Else
            cbLocations.ClearSelection()
            cbLocations.Text = ""
            cbLocations.SelectedIndex = -1
            RadGrid1.Rebind()
            RadGrid1.MasterTableView.GetColumn("Location").Visible = True
        End If
    End Sub

    Private Sub cbLocations_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        RadGrid1.Visible = True
        RadGrid1.Rebind()
    End Sub

    Private Sub RadAjaxManager1_AjaxSettingCreated(sender As Object, e As Telerik.Web.UI.AjaxSettingCreatedEventArgs) Handles RadAjaxManager1.AjaxSettingCreated
        If (e.Initiator.ID.StartsWith("mngBtn")) Then
            e.UpdatePanel.RenderMode = UpdatePanelRenderMode.Inline
        End If
    End Sub

#Region "Exporting"
    '<ExportSettings
    '   HideStructureColumns="true"
    '   ExportOnlyData="true"
    '   IgnorePaging="true"
    '   OpenInNewWindow="true">
    '</ExportSettings>
    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        RadGrid1.ExportSettings.HideStructureColumns = True
        RadGrid1.ExportSettings.ExportOnlyData = True
        RadGrid1.ExportSettings.IgnorePaging = True
        RadGrid1.ExportSettings.OpenInNewWindow = True
        RadGrid1.ExportSettings.FileName = "FreightIssuesSummary_" & IIf(cbLocations.SelectedIndex = -1, "AllLocations_", cbLocations.Text & "_") & Format(dpStartDate.SelectedDate, "ddMMMyy") & "_" & Format(dpEndDate.SelectedDate, "ddMMMyy")
        RadGrid1.MasterTableView.ExportToExcel()

    End Sub

    Protected Sub RadGrid1_GridExporting(ByVal source As Object, ByVal e As GridExportingArgs)
        Select Case e.ExportType
            Case ExportType.Excel
                'do something with the e.ExportOutput value
                Exit Select
            Case ExportType.ExcelML
                'do something with the e.ExportOutput value
                Exit Select
            Case ExportType.Word
                'do something with the e.ExportOutput value
                Exit Select
            Case ExportType.Csv
                'do something with the e.ExportOutput value
                Exit Select
            Case ExportType.Pdf
                'you can't change the output here - use the PdfExporting event instead
                Exit Select
        End Select
    End Sub

#End Region

End Class

