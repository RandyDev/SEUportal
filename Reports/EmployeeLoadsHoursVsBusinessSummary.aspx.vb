Imports Telerik.Web.UI

Public Class EmployeeLoadsHoursVsBusinessSummary
    Inherits System.Web.UI.Page

    Dim ttlbus As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Server.ScriptTimeout = 4800
        Dim scriptman As ScriptManager = ScriptManager.GetCurrent(Me)
        scriptman.AsyncPostBackTimeout = 4800
        If Not Page.IsPostBack Then
            Dim tpdal As New TimePuncheDAL
            dpStartDate.SelectedDate = Date.Now().ToShortDateString
            dpEndDate.SelectedDate = Date.Now().ToShortDateString
            ' populate Locations Combo Box (cbLocations on aspx page)
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
            RadGrid1.Visible = False
            pnlDeposit.Visible = False
            pnlTroubleShooter.Visible = False
        End If
    End Sub

    Private Sub RadGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadGrid1.Init
        'Dim menu As GridFilterMenu = RadGrid1.FilterMenu
        'Dim i As Integer = 0
        'While i < menu.Items.Count
        '    If menu.Items(i).Text = "NoFilter" Or _
        '       menu.Items(i).Text = "Contains" Then
        '        i = i + 1
        '    Else
        '        menu.Items.RemoveAt(i)
        '    End If
        'End While
    End Sub

    Private Sub RadGrid1_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If e.CommandName = "RowClick" Then
        End If
    End Sub

    Private Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If TypeOf e.Item Is GridGroupHeaderItem Then
            Dim item As GridGroupHeaderItem = DirectCast(e.Item, GridGroupHeaderItem)
            Dim groupDataRow As DataRowView = DirectCast(e.Item.DataItem, DataRowView)
            item.DataCell.Text = groupDataRow("Employee").ToString() & "<div style=""display:inline;float:right;"">" & FormatCurrency(groupDataRow("ulAmount"), 2).ToString() & "</div>"
            '            For Each column As DataColumn In groupDataRow.DataView.Table.Columns
            '            item.Cells(1).Text = groupDataRow("Employee").ToString()
            '            item.Cells(item.Cells.Count() - 1).Text = FormatCurrency(groupDataRow("ulAmount"), 2).ToString()
            '            Next
            '            item.Cells(item.Cells.Count() - 1).Text = "<table width=""100%"" cellpadding=""0"" cellspacing=""0""><tr><td>" & groupDataRow("Employee").ToString() & "</td>" & FormatCurrency(groupDataRow("ulAmount"), 2).ToString() & "<td align=""right""></td></tr></table>"

        End If
    End Sub

    Private Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim dt As New DataTable
        Dim ldal As New locaDAL
        Dim locaID As Guid = New Guid(cbLocations.SelectedValue.ToString)
        Dim location As String = cbLocations.SelectedItem.Text.Trim()
        Dim startdate As DateTime = FormatDateTime(dpStartDate.SelectedDate, DateFormat.ShortDate)
        Dim enddate As DateTime = FormatDateTime(dpEndDate.SelectedDate, DateFormat.ShortDate)
        enddate = DateAdd(DateInterval.Day, 1, enddate)

        Dim offset As Integer = ldal.getLocaBDOffset(locaID)
        If offset <> 0 Then
            startdate = DateAdd(DateInterval.Hour, offset, startdate)
            enddate = DateAdd(DateInterval.Hour, offset, enddate)
        End If

        Dim dba As New DBAccess

        Dim sqlStr As String = "SELECT dbo.WorkOrder.ID AS woid, CASE WHEN dbo.WorkOrder.CheckNumber > '' THEN dbo.WorkOrder.Amount - " & _
            "(SELECT CheckCharge " & _
            "FROM   dbo.Location " & _
            "WHERE Name = @location) - " & _
            "(SELECT AdministrativeFee " & _
            "FROM   dbo.Location " & _
            "WHERE Name = @location) - " & _
            "(SELECT CustomerFee " & _
            "FROM   dbo.Location " & _
            "WHERE Name = @location) WHEN dbo.WorkOrder.CheckNumber = '' AND dbo.workorder.loadtypeid = 'D62DA4A5-FD15-4460-B62F-BAA83ACE65FD' OR " & _
            "dbo.WorkOrder.CheckNumber = '' AND dbo.workorder.loadtypeid = '6144C1A1-3657-4D91-A50A-F107C3A41847' THEN dbo.WorkOrder.Amount - " & _
            "(SELECT AdministrativeFee " & _
            "FROM   dbo.Location " & _
            "WHERE Name = @location) - " & _
            "(SELECT CustomerFee " & _
            "FROM   dbo.Location " & _
            "WHERE Name = @location) ELSE dbo.WorkOrder.Amount END AS Amount, " & _
            "(SELECT COUNT(EmployeeID) AS Count " & _
            "FROM   dbo.Unloader " & _
            "WHERE (LoadID = dbo.WorkOrder.ID)) AS ulCount,  " & _
            "CASE WHEN [Amount] > 0 THEN CASE WHEN dbo.WorkOrder.CheckNumber > '' THEN dbo.WorkOrder.Amount - " & _
            "(SELECT CheckCharge " & _
            "FROM   dbo.Location " & _
            "WHERE Name = @location) - " & _
            "(SELECT AdministrativeFee " & _
            "FROM   dbo.Location " & _
            "WHERE Name = @location) - " & _
            "(SELECT CustomerFee " & _
            "FROM   dbo.Location " & _
            "WHERE Name = @location) WHEN dbo.WorkOrder.CheckNumber = '' AND dbo.workorder.loadtypeid = 'D62DA4A5-FD15-4460-B62F-BAA83ACE65FD' OR " & _
            "dbo.WorkOrder.CheckNumber = '' AND dbo.workorder.loadtypeid = '6144C1A1-3657-4D91-A50A-F107C3A41847' THEN dbo.WorkOrder.Amount - " & _
            "(SELECT AdministrativeFee " & _
            "FROM   dbo.Location " & _
            "WHERE Name = @location) - " & _
            "(SELECT CustomerFee " & _
            "FROM   dbo.Location " & _
            "WHERE Name = @location) ELSE dbo.WorkOrder.Amount END / " & _
            "(SELECT COUNT(dbo.Unloader.EmployeeID) AS Count " & _
            "FROM   dbo.Unloader " & _
            "WHERE dbo.Unloader.LoadID = dbo.WorkOrder.ID) ELSE (0) END AS ulAmount,  " & _
            "dbo.Employee.LastName + ', ' + dbo.Employee.FirstName + ' - ' + dbo.Employee.Login AS Employee, CONVERT(varchar(10), dbo.WorkOrder.LogDate, 110)  " & _
            "+ ':: ' + dbo.WorkOrder.PurchaseOrder AS DatePo, dbo.Location.Name " & _
            "FROM  dbo.WorkOrder INNER JOIN " & _
            "dbo.Unloader AS Unloader_1 ON dbo.WorkOrder.ID = Unloader_1.LoadID INNER JOIN " & _
            "dbo.Employee ON Unloader_1.EmployeeID = dbo.Employee.ID INNER JOIN " & _
            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID "

        If offset <> 0 Then
            sqlStr &= "WHERE (dbo.WorkOrder.StartTime >= @startdate) AND (dbo.WorkOrder.StartTime < @enddate) "
        Else
            sqlStr &= "WHERE (dbo.WorkOrder.LogDate >= @startdate) AND (dbo.WorkOrder.LogDate < @enddate) "
        End If
        sqlStr &= "AND (dbo.Location.Name = @location) ORDER BY DatePo "

        dba.CommandText = sqlStr
            dba.AddParameter("@startdate", startdate)
            dba.AddParameter("@enddate", enddate)
        dba.AddParameter("@location", location)
            dt = dba.ExecuteDataSet.Tables(0)

            Dim ttlUnloaderBusiness As Decimal = 0
            If dt.Rows.Count > 0 Then
                ttlUnloaderBusiness = dt.Compute("Sum(ulAmount)", "")
            End If
        If Utilities.IsValidGuid(locaID.ToString) Then
            sqlStr = String.Empty
            sqlStr = "select sum(amount) from workorder " & _
                "WHERE (WorkOrder.LocationID = @locaID) AND "
            If offset <> 0 Then
                sqlStr &= "(WorkOrder.DockTime >= @startdate) AND (WorkOrder.DockTime < @enddate) "
            Else
                sqlStr &= "(WorkOrder.LogDate >= @startdate) AND (WorkOrder.LogDate < @enddate) "
            End If
            dba.CommandText = sqlStr
            dba.AddParameter("@startdate", startdate)
            dba.AddParameter("@enddate", enddate)
            dba.AddParameter("@locaID", locaID)
            Dim ttlLocaBusiness As Decimal
            Try
                ttlLocaBusiness = dba.ExecuteScalar

            Catch ex As Exception

            End Try
            sqlStr = "SELECT COUNT(ID) from workorder where (workorder.locationID = @locaID) and "
            If offset <> 0 Then
                sqlStr &= "(WorkOrder.DockTime >= @startdate) AND (WorkOrder.DockTime < @enddate) "
            Else
                sqlStr &= "(WorkOrder.LogDate >= @startdate) AND (WorkOrder.LogDate < @enddate) "
            End If

            sqlStr = "SELECT COUNT(dbo.WorkOrder.CheckNumber) AS Expr1 " & _
                "FROM dbo.WorkOrder INNER JOIN " & _
                "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID " & _
                "WHERE (WorkOrder.LocationID = @locaID) AND LEN(dbo.WorkOrder.Checknumber) > 2 AND "
            If offset <> 0 Then
                sqlStr &= "(WorkOrder.DockTime >= @startdate) AND (WorkOrder.DockTime < @enddate) "
            Else
                sqlStr &= "(WorkOrder.LogDate >= @startdate) AND (WorkOrder.LogDate < @enddate) "
            End If
            dba.CommandText = sqlStr
            dba.AddParameter("@startdate", startdate)
            dba.AddParameter("@enddate", enddate)
            dba.AddParameter("@locaID", locaID)
            Dim checkCount As Integer = 0
            Try
                checkCount = dba.ExecuteScalar
            Catch ex As Exception
                Dim msg As String = ex.Message
            End Try
            lblNumChecks.Text = checkCount.ToString
            sqlStr = "SELECT COUNT(dbo.WorkOrder.ID) " & _
                "FROM dbo.WorkOrder INNER JOIN " & _
                "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID " & _
                "WHERE (WorkOrder.LocationID = @locaID) AND (dbo.WorkOrder.LoadTypeid=@cash) AND "
            If offset <> 0 Then
                sqlStr &= "(WorkOrder.DockTime >= @startdate) AND (WorkOrder.DockTime < @enddate) "
            Else
                sqlStr &= "(WorkOrder.LogDate >= @startdate) AND (WorkOrder.LogDate < @enddate) "
            End If
            dba.CommandText = sqlStr
            dba.AddParameter("@startdate", startdate)
            dba.AddParameter("@enddate", enddate)
            dba.AddParameter("@locaID", locaID)
            dba.AddParameter("@cash", "D62DA4A5-FD15-4460-B62F-BAA83ACE65FD")
            Dim cashCount As Integer = 0
            Try
                cashCount = dba.ExecuteScalar
            Catch ex As Exception
                Dim msg As String = ex.Message
            End Try
            sqlStr = "SELECT COUNT(dbo.WorkOrder.ID) " & _
                "FROM dbo.WorkOrder INNER JOIN " & _
                "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID " & _
                "WHERE (WorkOrder.LocationID = @locaID) AND (dbo.WorkOrder.LoadTypeid=@invoice) AND "
            If offset <> 0 Then
                sqlStr &= "(WorkOrder.DockTime >= @startdate) AND (WorkOrder.DockTime < @enddate) "
            Else
                sqlStr &= "(WorkOrder.LogDate >= @startdate) AND (WorkOrder.LogDate < @enddate) "
            End If
            dba.CommandText = sqlStr
            dba.AddParameter("@startdate", startdate)
            dba.AddParameter("@enddate", enddate)
            dba.AddParameter("@locaID", locaID)
            dba.AddParameter("@invoice", "6144C1A1-3657-4D91-A50A-F107C3A41847")

            Dim invoiceCount As Integer = 0
            Try
                invoiceCount = dba.ExecuteScalar
            Catch ex As Exception
                Dim msg As String = ex.Message
            End Try


            dba.CommandText = "Select CheckCharge from Location WHERE ID=@ID"
            dba.AddParameter("@ID", locaID)
            Dim checkCharge As Decimal = 0
            Try
                checkCharge = dba.ExecuteScalar
            Catch ex As Exception
            End Try

            dba.CommandText = "Select AdministrativeFee from Location WHERE ID=@ID"
            dba.AddParameter("@ID", locaID)
            Dim administrativefee As Decimal = 0
            Try
                administrativefee = dba.ExecuteScalar
            Catch ex As Exception

            End Try
            dba.CommandText = "Select CustomerFee from Location WHERE ID=@ID"
            dba.AddParameter("@ID", locaID)
            Dim CustomerFee As Decimal = 0
            Try
                CustomerFee = dba.ExecuteScalar
            Catch ex As Exception
            End Try


            lblCheckTotal.Text = FormatCurrency(checkCount * checkCharge)
            Dim adminFeeTotal As Decimal = (cashCount + invoiceCount) * administrativefee
            Dim customerFeeTotal As Decimal = (cashCount + invoiceCount) * CustomerFee
            Dim feetotal As Decimal = adminFeeTotal + customerFeeTotal
            lblAdministrationFee.Text = FormatCurrency(adminFeeTotal)
            lblCustomerFeeTotal.Text = FormatCurrency(customerFeeTotal)


            Dim adjustedLocaBusiness As Decimal = ttlLocaBusiness - (checkCount * checkCharge) - feetotal
            lblCashTotal.Text = FormatCurrency(adjustedLocaBusiness + ((checkCharge * checkCount) + feetotal))
            lblttlbusloca.Visible = True
            lblttlbusunl.Visible = True
            lblttlbusdif.Visible = True
            lblttlbusunl.Text = FormatCurrency(ttlUnloaderBusiness, 2) & " <-- Total Unloader Business"
            lblchecksfees.Text = FormatCurrency(((checkCharge * checkCount) + feetotal))
            lbllocabusiness.Text = lblttlbusloca.Text
            If Not IsNumeric(ttlLocaBusiness) Then ttlLocaBusiness = 0

            lblttlbusloca.Text = FormatCurrency(adjustedLocaBusiness, 2) & " <-- Total Location Business"
            lbllocabusiness.Text = FormatCurrency(adjustedLocaBusiness, 2)

            lblttlbusdif.Text = FormatCurrency(adjustedLocaBusiness - ttlUnloaderBusiness, 2) & " <-- Net Difference"
            '            lblttlbusloca.Text = "n/a <-- Total Location Business"
            '            lblttlbusdif.Text = "n/a <-- Net Difference"

            Dim dif As Decimal = adjustedLocaBusiness - ttlUnloaderBusiness
            If dif > 1 Or dif < -1 Then
                lblttlbusdif.Style.Item("color") = "Red"
            Else
                lblttlbusdif.Style.Item("color") = "Black"
            End If
        End If

        RadGrid1.DataSource = dt
        TroubleShootMe()

    End Sub


    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        RadGrid1.Visible = True
        RadGrid1.Rebind()

        pnlDeposit.Visible = True ' dpEndDate.SelectedDate = dpStartDate.SelectedDate

        If pnlDeposit.Visible Then
            Dim thisdate As Date = dpStartDate.SelectedDate
            lblDeposit.Text = "Checks & Fees"
        End If
    End Sub

    Private Sub dpStartDate_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dpStartDate.SelectedDateChanged
        dpEndDate.SelectedDate = dpStartDate.SelectedDate
        pnlDeposit.Visible = False
        lblttlbusloca.Visible = False
        lblttlbusunl.Visible = False
        lblttlbusdif.Visible = False
        pnlTroubleShooter.Visible = False
    End Sub

    Private Sub dpEndDate_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dpEndDate.SelectedDateChanged
        pnlDeposit.Visible = False
        lblttlbusloca.Visible = False
        lblttlbusunl.Visible = False
        lblttlbusdif.Visible = False
        pnlTroubleShooter.Visible = False
    End Sub

    Private Sub cbLocations_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        RadGrid1.Visible = False
        pnlDeposit.Visible = False
        lblttlbusdif.Visible = False
        lblttlbusloca.Visible = False
        lblttlbusunl.Visible = False
        pnlTroubleShooter.Visible = False

    End Sub


    Private Sub lbtnTroubleShoot_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles lbtnTroubleShoot.Command
        TroubleShootMe()

    End Sub

    Private Sub TroubleShootMe()
        pnlTroubleShooter.Visible = True
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim ldal As New locaDAL()
        Dim startDate As DateTime = FormatDateTime(dpStartDate.SelectedDate, DateFormat.ShortDate)
        Dim endDate As DateTime = FormatDateTime(dpEndDate.SelectedDate, DateFormat.ShortDate)
        endDate = DateAdd(DateInterval.Second, 86399, endDate)
        Dim locaID As Guid = New Guid(cbLocations.SelectedValue.ToString)
        Dim locationName As String = cbLocations.SelectedItem.Text
        Dim businessDayOffset As Integer = ldal.getLocaBDOffset(locaID)
        Dim dba As New DBAccess()
        ' no unloaders
        Dim strSQL As String = "SELECT dbo.WorkOrder.ID AS WorkOrder " & _
            "FROM dbo.Employee INNER JOIN " & _
            "dbo.Unloader ON dbo.Employee.ID = dbo.Unloader.EmployeeID RIGHT OUTER JOIN " & _
            "dbo.WorkOrder INNER JOIN " & _
            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID ON dbo.Unloader.LoadID = dbo.WorkOrder.ID " & _
            "WHERE (dbo.Location.ID = @locationID) "
        If businessDayOffset <> 0 Then
            strSQL &= "AND (dbo.WorkOrder.DockTime >= @startdate) " & _
                "AND (dbo.WorkOrder.DockTime < @enddate) "
        Else
            strSQL &= "AND (dbo.WorkOrder.LogDate >= @startdate) " & _
                "AND (dbo.WorkOrder.LogDate < @enddate) "
        End If
        strSQL &= "GROUP BY dbo.WorkOrder.ID, dbo.Unloader.EmployeeID, dbo.Employee.FirstName, dbo.Employee.LastName " & _
            "HAVING (dbo.Unloader.EmployeeID IS NULL) "
        dba.CommandText = strSQL
        dba.AddParameter("@startdate", startDate)
        dba.AddParameter("@enddate", endDate)
        dba.AddParameter("@locationid", locaID)
        ds = New DataSet

        ds = dba.ExecuteDataSet
        Dim retStr As String = "" 'String.Empty
        If ds.Tables.Count > 0 Then
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                For Each rw As DataRow In dt.Rows
                    retStr &= "<a href=""javascript:OpenWorkOrderTS('" & rw.Item("WorkOrder").ToString & "&edit=editUnloader')"" >view</a><br />"
                Next
            Else
                retStr = "none found - <font color='green'>OK</font>"
            End If

        End If
        lblNoUnLoaders.Text = retStr

        ' duplicate unloaders
        dt = New DataTable
        strSQL = "SELECT dbo.WorkOrder.PurchaseOrder, dbo.workorder.ID, " & _
            "COUNT (dbo.WorkOrder.ID) AS Lo2xEm2x " & _
            "FROM dbo.WorkOrder INNER JOIN " & _
            "dbo.Unloader ON dbo.WorkOrder.ID = dbo.Unloader.LoadID INNER JOIN " & _
            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID " & _
            "WHERE (dbo.WorkOrder.LogDate >= @startdate) AND (dbo.WorkOrder.LogDate < @enddate) AND (dbo.Location.ID = @locationID) " & _
            "GROUP BY dbo.WorkOrder.PurchaseOrder, dbo.Unloader.EmployeeID, dbo.WorkOrder.ID " & _
            "HAVING (COUNT(dbo.WorkOrder.ID) > 1) "
        dba.CommandText = strSQL
        dba.AddParameter("@startdate", startDate)
        dba.AddParameter("@enddate", endDate)
        dba.AddParameter("@locationid", locaID)
        dt = dba.ExecuteDataSet.Tables(0)
        retStr = ""
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                retStr &= "<a href=""javascript:OpenWorkOrderTS('" & rw.Item("ID").ToString & "&edit=editUnloader')"">view</a><br />"
            Next
        Else
            retStr = "none found - <font color='green'>OK</font>"
        End If
        lblDupeUnLoaders.Text = retStr

        ' missing LoadType
        dt = New DataTable
        strSQL = "SELECT dbo.WorkOrder.ID, dbo.WorkOrder.PurchaseOrder " & _
            "FROM dbo.WorkOrder INNER JOIN " & _
            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID " & _
            "WHERE (dbo.WorkOrder.LogDate >= @startdate) AND (dbo.WorkOrder.LogDate < @enddate) AND (dbo.Location.ID = @locationid) " & _
            "GROUP BY dbo.WorkOrder.LoadTypeID, dbo.WorkOrder.PurchaseOrder, dbo.WorkOrder.ID " & _
            "HAVING (dbo.WorkOrder.LoadTypeID = '00000000-0000-0000-0000-000000000000') " & _
            "ORDER BY dbo.WorkOrder.LoadTypeID "
        dba.CommandText = strSQL
        dba.AddParameter("@startdate", startDate)
        dba.AddParameter("@enddate", endDate)
        dba.AddParameter("@locationid", locaID)
        dt = dba.ExecuteDataSet.Tables(0)
        retStr = ""
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                retStr &= "<a href=""javascript:OpenWorkOrderTS('" & rw.Item("ID").ToString & "&edit=editLoadType')"">view</a><br />"
            Next
        Else
            retStr = "none found - <font color='green'>OK</font>"
        End If
        lblMissingLoadTypes.Text = retStr


        ' Duplicate Purchase Orders
        dt = New DataTable
        strSQL = "SELECT dbo.WorkOrder.ID, dbo.WorkOrder.PurchaseOrder AS PO, dbo.WorkOrder.VendorNumber, dbo.WorkOrder.Amount " & _
            "FROM dbo.WorkOrder INNER JOIN " & _
            "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID " & _
            "WHERE (dbo.WorkOrder.LogDate >= @startdate) AND (dbo.WorkOrder.LogDate < @enddate) " & _
            "GROUP BY dbo.WorkOrder.ID, dbo.Location.Name, dbo.WorkOrder.PurchaseOrder, dbo.WorkOrder.VendorNumber, dbo.WorkOrder.Amount " & _
            "HAVING (COUNT(dbo.WorkOrder.PurchaseOrder) > 1) AND (dbo.Location.Name = @location) " & _
            "ORDER BY PO "
        dba.CommandText = strSQL
        dba.AddParameter("@startdate", startDate)
        dba.AddParameter("@enddate", endDate)
        dba.AddParameter("@location", locationName)
        dt = dba.ExecuteDataSet.Tables(0)
        retStr = ""
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                retStr &= "<a href=""javascript:OpenWorkOrderTS('" & rw.Item("ID").ToString & "&edit=editDupe')"">view" & rw.Item("PO").ToString & "</a><br />"
            Next
        Else
            retStr = "none found - <font color='green'>OK</font>"
        End If
        lblDupePOs.Text = retStr
    End Sub

End Class

'Dim sqlStr As String = "SELECT dbo.WorkOrder.ID AS woid, " & _
'    "dbo.WorkOrder.Amount,  " & _
'    "(SELECT     COUNT(dbo.Unloader.EmployeeID) AS Count " & _
'    "FROM dbo.Unloader  " & _
'    "WHERE dbo.Unloader.LoadID = dbo.WorkOrder.ID) AS ulCount,  " & _
'    "CASE WHEN [Amount] > 0 THEN ([Amount] / (SELECT     COUNT(dbo.Unloader.EmployeeID) AS Count " & _
'    "FROM dbo.Unloader  " & _
'    "WHERE dbo.Unloader.LoadID = dbo.WorkOrder.ID)) ELSE (0) END AS ulAmount,  " & _
'    "dbo.Employee.LastName + ', ' + dbo.Employee.FirstName + ' - ' + dbo.Employee.Login AS Employee, " & _
'    "CONVERT(varchar(10),dbo.WorkOrder.LogDate, 110) + '  ::  ' + dbo.WorkOrder.PurchaseOrder AS DatePo, " & _
'    "dbo.Location.Name " & _
'    "FROM dbo.WorkOrder INNER JOIN " & _
'    "dbo.Unloader ON dbo.WorkOrder.ID = dbo.Unloader.LoadID INNER JOIN " & _
'    "dbo.Employee ON dbo.Unloader.EmployeeID = dbo.Employee.ID INNER JOIN " & _
'    "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID "
'If offset <> 0 Then
'    sqlStr &= "WHERE (dbo.WorkOrder.DockTime >= @startdate) AND (dbo.WorkOrder.DockTime <= @enddate) AND  "
'Else
'    sqlStr &= "WHERE (dbo.WorkOrder.LogDate >= @startdate) AND (dbo.WorkOrder.LogDate <= @enddate) AND  "
'End If
'sqlStr &= "(dbo.WorkOrder.LocationID = @locationID) " & _
'    "GROUP BY dbo.WorkOrder.ID, dbo.WorkOrder.Amount, dbo.Employee.LastName + ', ' + dbo.Employee.FirstName + ' - ' + dbo.Employee.Login,  " & _
'    "CONVERT(varchar(10), dbo.WorkOrder.LogDate, 110) + '  ::  ' + dbo.WorkOrder.PurchaseOrder, dbo.Location.Name " & _
'    "ORDER BY DatePo "