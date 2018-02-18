Public Class eMailPictures
    Inherits System.Web.UI.Page
    Protected locaid As String
    Protected lastsent As Date

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        locaid = Request("locaid")
        Dim dba As New DBAccess
        dba.CommandText = "Select PicsLastSent FROM Location WHERE ID=@locaid"
        dba.AddParameter("@locaid", locaid)
        lastsent = IIf(IsDBNull(dba.ExecuteScalar), Date.Now, dba.ExecuteScalar)
        If IsDBNull(lastsent) Then
            lblLastMailed.Text = "no record"
        Else
            lblLastMailed.Text = Format(lastsent, "dd MMM yy - hh:m tt")
        End If
        Dim ldal As New locaDAL

        lblLocaName.Text = ldal.getLocationNameByID(locaid)
        Dim utl As New Utilities
        Dim numpics As String = Utilities.countNewPics("7/1/2016", locaid).ToString
        Dim numworkorders As String = Utilities.countWorkOrdersWPics("7/1/2016", locaid).ToString
        lblNumPics.Text = numpics
        lblNumWorkOrders.Text = numworkorders
    End Sub

    Private Sub RadGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim edate As Date = Date.Now
        If IsDate(dpStartDate.SelectedDate) Then
            lastsent = dpStartDate.SelectedDate
        End If
        If IsDate(dpEndDate.SelectedDate) Then
            edate = dpStartDate.SelectedDate
        End If
        Dim sDate As Date = lastsent
        edate = DateAdd(DateInterval.Day, 1, edate)
        Dim dba As New DBAccess()
        '        Dim defaultGridWidth As Integer = 776
        '        Dim defaultMasterGridWidth As Integer = 660

        If Utilities.IsValidGuid(locaid) Then
            '            RadGrid1.MasterTableView.GetColumn("LocationName").Visible = False
            '            RadGrid1.Width = defaultGridWidth - 105
            '            RadGrid1.MasterTableView.Width = defaultMasterGridWidth - 105
        Else
            '            RadGrid1.MasterTableView.GetColumn("LocationName").Visible = True
            '            RadGrid1.Width = defaultGridWidth
            '            RadGrid1.MasterTableView.Width = defaultMasterGridWidth

        End If
        Dim sql As String = "SELECT DISTINCT (SELECT COUNT(WorkOrderID) AS PicCount " & _
            "FROM LoadImages " & _
            "GROUP BY WorkOrderID " & _
            "HAVING (WorkOrderID = WorkOrder.ID)) AS PicCount, Location.Name AS Location, WorkOrder.DockTime, WorkOrder.LogDate, WorkOrder.PurchaseOrder,  " & _
            "Department.Name AS Department, WorkOrder.BadPallets, WorkOrder.Restacks, Vendor.Name AS Vendor, Vendor.Number AS VendorNumber, Carrier.Name AS Carrier,  " & _
            "WorkOrder.TrailerNumber, WorkOrder.ID, LocationDepartment.email, LocationDepartment.emailCC " & _
            "FROM Carrier RIGHT OUTER JOIN " & _
            "WorkOrder INNER JOIN " & _
            "Location ON WorkOrder.LocationID = Location.ID LEFT OUTER JOIN " & _
            "LocationDepartment INNER JOIN " & _
            "Department ON LocationDepartment.DepartmentID = Department.ID ON Location.ID = LocationDepartment.LocationID AND WorkOrder.DepartmentID = Department.ID ON " & _
            "Carrier.ID = WorkOrder.CarrierID LEFT OUTER JOIN " & _
            "Vendor ON WorkOrder.CustomerID = Vendor.ID RIGHT OUTER JOIN " & _
            "LoadImages AS LoadImages_1 ON WorkOrder.ID = LoadImages_1.WorkOrderID "
        If locaid = String.Empty Then
            sql &= "WHERE WorkOrder.DockTime >= @startDate " & _
                "ORDER BY Department ASC, WorkOrder.LogDate DESC "
        Else
            sql &= "WHERE WorkOrder.DockTime >= @startDate AND WorkOrder.DockTime < @endDate AND WorkOrder.LocationID = @locaID " & _
                "ORDER BY Department ASC, WorkOrder.LogDate DESC "
        End If
        dba.CommandText = sql
        dba.AddParameter("@startDate", lastsent)
        dba.AddParameter("@endDate", eDate)
        If Not locaid = String.Empty Then
            dba.AddParameter("@locaID", locaid)
        End If
        Try
            Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
            RadGrid1.DataSource = dt
            lblNumPics.Text = Utilities.countNewPics(lastsent, locaid)
            lblNumWorkOrders.Text = Utilities.countWorkOrdersWPics(lastsent, locaid)
            lnkButton1.Visible = lblNumWorkOrders.Text > "0"
            Linkbutton1.Visible = lnkButton1.Visible
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub RadGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
    '    Dim dba As New DBAccess
    '    If IsDate(dpStartDate.SelectedDate) Then
    '        lastsent = dpStartDate.SelectedDate
    '    End If
    '    dba.CommandText = "SELECT distinct(WorkOrder.ID),WorkOrder.PurchaseOrder,workorder.docktime as timestamp " & _
    '        "FROM WorkOrder Inner JOIN " & _
    '        "LoadImages ON WorkOrder.ID = LoadImages.WorkOrderID " & _
    '        "WHERE (WorkOrder.DockTime > @PicsLastSent) and (WorkOrder.LocationID = @locaid) order by WorkOrder.DockTime desc"
    '    dba.AddParameter("@PicsLastSent", lastsent)
    '    dba.AddParameter("@locaid", locaid)
    '    Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
    '    RadGrid1.DataSource = dt
    '    lblNumPics.Text = Utilities.countNewPics(lastsent, locaid)
    '    lblNumWorkOrders.Text = Utilities.countWorkOrdersWPics(lastsent, locaid)
    'End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        RadGrid1.Rebind()
    End Sub


End Class