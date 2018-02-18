Imports System.Collections.Generic
'Imports DiversifiedLogistics.Utilities


Public Class ClearNoShowScheduledLoads
    Inherits System.Web.UI.Page

    Public Enum StatusFlags
        Undefined = 0          ' initial value for the record just created
        CheckedIn = 1           ' bit 0 - data entered but no employee assigned to the load
        Assigned = 2            ' bit 1 - Employee assigned to the load
        Printed = 4             ' bit 2 - receipt printed
        AddDataChanged = 8      ' bit 3 - other data pages touched
        Complete = 64           ' bit 6 - Load has values entered for both bad pallets and restacks  
        Finished = 128          ' bit 7 - the load is was finished and not to be shown on any PDA lists
    End Enum


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadLocations()
            RadGrid1.Visible = False
        End If
    End Sub



    Protected Sub LoadLocations()
        cbLocation.Items.Clear()
        cbLocation.Text = ""
        Dim dba As New DBAccess
        Dim dt As New DataTable
        Dim edal As New empDAL
        Dim usr As MembershipUser = Membership.GetUser(User.Identity.Name)
        Dim emp As New Employee
        Dim uid As String = usr.ProviderUserKey.ToString
        Dim rtdsid As String
        rtdsid = Utilities.getRTDSidByUserID(uid)
        If rtdsid <> "00000000-0000-0000-0000-000000000000" Then
            'if this is an hourly or percentage employee populate Employee object
            Dim rtdsIDtoGUID As New Guid(rtdsid)
            Dim empdal As New empDAL
            emp = empdal.GetEmployeeByID(rtdsIDtoGUID)
        End If



        'get import list
        dba.CommandText = "Select DISTINCT(Location.Name), WorkOrder.LocationID " & _
            "FROM WorkOrder INNER JOIN " & _
            "Location ON WorkOrder.LocationID = Location.ID " & _
            "WHERE (LoadTypeID = '00000000-0000-0000-0000-000000000000')  " & _
            "AND (LoadDescriptionID = '00000000-0000-0000-0000-000000000000') " & _
            "AND (CarrierID = '00000000-0000-0000-0000-000000000000')  " & _
            "AND (Amount = 0)  " & _
            "AND (TruckNumber = '' ) " & _
            "AND (TrailerNUmber = '')  order by Location.Name"
        '            "WHERE WorkOrder.StartTime = '1/1/1900 12:00:00 AM' and WorkOrder.Status=9 order by Location.Name"
        dt = dba.ExecuteDataSet.Tables(0) '.Name LocationID

        'place import list in dictionary
        Dim importlist As New Dictionary(Of Guid, String)
        For Each row As DataRow In dt.Rows
            importlist.Add(row.Item("LocationID"), row.Item("Name"))
        Next

        'get allowed list for multi loca managers
        Dim ldal As New locaDAL
        dt = ldal.getUserLocaList(usr.ProviderUserKey)  'locaID, LocationName
        'place localist in Dictionary
        Dim allowedlist As New Dictionary(Of Guid, String)
        For Each row As DataRow In dt.Rows
            allowedlist.Add(row.Item("locaID"), row.Item("LocationName"))
        Next

        'if allowed list is empty, get employee location
        If allowedlist.Count < 1 Then 'this is not a manager w/ multiple access
            Try
                If allowedlist.Count < 1 Then 'this is not a manager w/ multiple access
                    allowedlist.Add(emp.LocationID, emp.LocationName)
                End If

            Catch ex As Exception
            End Try
        End If
        cbLocation.Items.Clear()
        For Each Pair In importlist
            If allowedlist.ContainsKey(Pair.Key) Then
                Dim cbitem As New Telerik.Web.UI.RadComboBoxItem
                cbitem.Value = Pair.Key.ToString
                cbitem.Text = Pair.Value
                cbLocation.Items.Add(cbitem)
            End If
        Next

        If User.IsInRole("SysOp") Or User.IsInRole("Administrator") Then
            cbLocation.Items.Clear()
            For Each Pair In importlist
                Dim cbitem As New Telerik.Web.UI.RadComboBoxItem
                cbitem.Value = Pair.Key.ToString
                cbitem.Text = Pair.Value
                cbLocation.Items.Add(cbitem)
            Next
        End If

        'No soup for you
        If cbLocation.Items.Count < 1 Then
            cbLocation.Visible = False
            cbDepartment.Visible = False
            lblcbDepartment.Visible = False
            lblcbLocation.Text = "No records for your location(s)"
            lblcbLocation.Style.Item("Color") = "red"
            lblcbLocation.Style.Item("font-weight") = "Bold"
            lblcbLocation.Style.Item("font-size") = "1.2em"
        End If


        cbLocation.ClearSelection()
        cbDepartment.Items.Clear()
        cbDepartment.Text = ""
        Dim tlocation As New Telerik.Web.UI.RadComboBox


        'Dim cbLocations As Telerik.Web.UI.ComboBox = FindControl(cbLocation)

        'ldal.setLocaCombo(puser, cbLocations)
        'puser.ProviderUserKey


        'For Each item As Telerik.Web.UI.RadComboBoxItem In tlocation.Items
        '    item.Value
        '    item.Text

        'Next()

    End Sub

    Private Sub RadGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim locaID As String = cbLocation.SelectedValue
        Dim deptID As String = cbDepartment.SelectedValue
        Dim dba As New DBAccess
        Dim dt As DataTable = New DataTable
        If Utilities.IsValidGuid(cbLocation.SelectedValue) Then
            dba.CommandText = "SELECT ID, Status, LogDate, LoadNumber, PurchaseOrder, DoorNumber, CreatedBy " & _
                "FROM WorkOrder WHERE WorkOrder.StartTime = '1/1/1900 12:00:00 AM' AND LocationID = @LocationID AND DepartmentID=@DepartmentID AND ((CreatedBy LIKE 'Imported:%') OR (CreatedBy LIKE 'Excel_%')) "
            dba.AddParameter("@LocationID", locaID)
            dba.AddParameter("@DepartmentID", deptID)
            dt = dba.ExecuteDataSet.Tables(0)
            RadGrid1.DataSource = dt
        End If
        btnCloseEm.Text = "Close " & dt.Rows.Count.ToString() & " loads displayed at left"

    End Sub

    Private Sub btnCloseEm_Click(sender As Object, e As System.EventArgs) Handles btnCloseEm.Click
        Dim locaID As String = cbLocation.SelectedValue
        Dim deptID As String = cbDepartment.SelectedValue
        Dim dba As New DBAccess
        If Utilities.IsValidGuid(cbLocation.SelectedValue) Then
            'too much info .. all i need is id to create list of wo
            dba.CommandText = "SELECT ID, Status, LogDate, LoadNumber, PurchaseOrder, DoorNumber, CreatedBy " & _
                "FROM WorkOrder WHERE WorkOrder.StartTime = '1/1/1900 12:00:00 AM' AND LocationID = @LocationID AND DepartmentID=@DepartmentID AND ((CreatedBy LIKE 'Imported:%') OR (CreatedBy LIKE 'Excel_%'))"
            dba.AddParameter("@LocationID", locaID)
            dba.AddParameter("@DepartmentID", deptID)
            Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
            If dt.Rows.Count > 0 Then
                Dim wolist As List(Of WorkOrder) = New List(Of WorkOrder)
                Dim woDAL As New WorkOrderDAL
                For Each rw As DataRow In dt.Rows
                    Dim wo As WorkOrder = woDAL.GetLoadByID(rw.Item("ID").ToString())
                    wolist.Add(wo)
                Next
                For Each wo As WorkOrder In wolist
                    wo.Status = 73
                    wo.Restacks = 0
                    wo.BadPallets = 0
                    Dim elst As New List(Of String)
                    wo.Employee = elst
                    woDAL.UpdateWorkOrder(wo)
                Next
                lblBillsLilCounter.Text = wolist.Count.ToString()
                btnCloseEm.Visible = False
                pnlEndOfDayInstructions.Visible = True
            End If
        End If
    End Sub

    Private Sub cbDepartment_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbDepartment.SelectedIndexChanged
        RadGrid1.Visible = True
        btnCloseEm.Visible = True
        RadGrid1.Rebind()
    End Sub

    Private Sub cbLocation_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocation.SelectedIndexChanged
        RadGrid1.Visible = False
        btnCloseEm.Visible = False
        cbDepartment.Items.Clear()
        cbDepartment.Text = ""
        pnlEndOfDayInstructions.Visible = False
        lbldun.Visible = False
        pnlDONE.Visible = False
        Dim dba As New DBAccess()
        Dim loca As String = cbLocation.SelectedValue
        dba.CommandText = "SELECT distinct(WorkOrder.DepartmentID), Department.Name " & _
            "FROM WorkOrder INNER JOIN " & _
            "Department ON WorkOrder.DepartmentID = Department.ID " & _
            "WHERE WorkOrder.StartTime = '1/1/1900 12:00:00 AM' AND LocationID = @LocationID AND ((CreatedBy LIKE 'Imported:%') OR (CreatedBy LIKE 'Excel_%'))"
        dba.AddParameter("@LocationID", loca)
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        cbDepartment.DataSource = dt
        cbDepartment.DataTextField = "Name"
        cbDepartment.DataValueField = "DepartmentID"
        cbDepartment.DataBind()
        cbDepartment.ClearSelection()
        cbDepartment.Text = ""
    End Sub

    Private Sub btnDeleteLoads_Click(sender As Object, e As System.EventArgs) Handles btnDeleteLoads.Click
        Dim locaID As String = cbLocation.SelectedValue
        Dim deptID As String = cbDepartment.SelectedValue
        Dim dba As New DBAccess
        'too much info .. all i need is id to create list of wo
        dba.CommandText = "SELECT ID, Status, LogDate, LoadNumber, PurchaseOrder, DoorNumber, CreatedBy " & _
            "FROM WorkOrder WHERE WorkOrder.StartTime = '1/1/1900 12:00:00 AM' AND LocationID = @LocationID AND DepartmentID=@DepartmentID AND ((CreatedBy LIKE 'Imported:%') OR (CreatedBy LIKE 'Excel_%'))"
        dba.AddParameter("@LocationID", locaID)
        dba.AddParameter("@DepartmentID", deptID)
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        Dim sb As String = String.Empty
        If dt.Rows.Count > 0 Then
            Dim ldal As New locaDAL()
            Dim wolist As List(Of WorkOrder) = New List(Of WorkOrder)
            Dim woDAL As New WorkOrderDAL
            For Each rw As DataRow In dt.Rows
                Dim wo As WorkOrder = woDAL.GetLoadByID(rw.Item("ID").ToString())
                wolist.Add(wo)
            Next
            sb = "<table border='1' cellpadding='7' style='text-align:left;border-collapse:collapse;'><tr style=""font-size:14px;""><th>LocationID</th><th>LogDate</th><th>PurchaseOrder</th><th>LoadNumber</th><th>Department</th><th>VendorName</th><th>Pieces</th><th>Comments</th></tr>"
            Dim ttlPcs As Integer = 0
            For Each wo As WorkOrder In wolist

                woDAL.MoveToNoShowWorkOrder(wo)
                ttlPcs += wo.Pieces
                sb &= "<tr style=""font-size:12px;""><td>" & ldal.getLocationNameByID(wo.LocationID.ToString) & "</td><td>" & wo.LogDate & "</td><td>" & wo.PurchaseOrder & "</td><td>" & wo.LoadNumber & "</td><td>" & wo.Department & "</td><td>" & wo.VendorName & "</td><td>" & wo.Pieces & "</td><td>" & wo.Comments & "</td></tr>"
            Next
            sb &= "</table>" & vbCrLf
            Dim ssb As String = sb
            sb = "<br/>Total No Show Loads: " & wolist.Count & vbCrLf & "<br/>Total Pieces: " & ttlPcs.ToString & "<br />" & ssb

        End If
        btnCloseEm.Visible = False
        pnlEndOfDayInstructions.Visible = False
        pnlDONE.Visible = True
        lbldun.Text = sb
        RadGrid1.Visible = False
        LoadLocations()
        lbldun.Visible = True

        Dim em As New rwMailer
        em.To = "WWalklett@Div-Log.com"
        em.isBodyHtml = True
        em.Body = sb
        rwMailer.SendMail(em)

    End Sub

End Class
