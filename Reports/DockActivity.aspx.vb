'Imports System.Data.SqlClient
'Imports DiversifiedLogistics
'Imports Telerik.Web.UI

Partial Public Class DockActivity
    Inherits System.Web.UI.Page

    Protected cntWorking As Integer = 0
    Protected cntWarning As Integer = 0
    Protected cntLate As Integer = 0
    Protected cntcLate As Integer = 0
    Protected cntComplete As Integer = 0
    Protected cntTotal As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' set location combobox
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager") Or User.IsInRole("Client")
            lblHideCompletedStatus.Style.Item("Color") = "Red"
            If User.IsInRole("Client") Or User.IsInRole("Vendor") Or User.IsInRole("Carrier") Then
                chkHideCompleted.Checked = False
                lblHideCompletedStatus.Style.Item("Color") = "#3F682A"
                lblHideCompletedStatus.Text = "yes"
                chkHideCompleted.ToolTip = "Check this box to hide 'completed'` loads"
            End If
        End If
    End Sub

    Protected Sub fillcbLocations()
        Dim dt As New DataTable()
        Dim ldal As New locaDAL()
        dt = ldal.getLocations()
        cbLocations.DataSource = dt
        cbLocations.DataTextField = "LocationName"
        cbLocations.DataValueField = "locaID"
        cbLocations.DataBind()
        cbLocations.ClearSelection()
    End Sub


    Protected Sub cbLocations_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        '        RadGrid1.Rebind()
        UpdateActiveList(cbLocations.SelectedValue)
    End Sub

    'Private Sub RadGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
    '    Dim wdal As New WorkOrderDAL()
    '    If cbLocations.SelectedIndex > -1 Then
    '        Dim dt As DataTable = wdal.GetActiveWorkOrders2(New Guid(cbLocations.SelectedValue), chkHideCompleted.Checked)
    '        RadGrid1.DataSource = dt
    '        Panel1.Visible = True
    '    End If
    'End Sub

    '
    Protected Sub UpdateActiveList(ByVal cblocaID As String)
        Dim wdal As New WorkOrderDAL()
        If cbLocations.SelectedIndex > -1 Then
            Dim dt As DataTable = wdal.GetActiveWorkOrders(New Guid(cblocaID), chkHideCompleted.Checked)
            Panel1.Visible = True
            LoadList.Text = String.Empty
            Dim locadal As New locaDAL
            ' ********************** This static offset (for server location) needs to be maintained
            ' ********************** This static offset (for server location) needs to be maintained
            Dim offset As Integer = locadal.getLocaTimeZoneOffset(New Guid(cbLocations.SelectedValue)) + 6
            ' ********************** This static offset (for server location) needs to be maintained
            ' ********************** This static offset (for server location) needs to be maintained
            Dim varTimeNow As DateTime = DateAdd(DateInterval.Hour, offset, Date.Now())
            lblTimeNow.Text = "Last Refresh:&nbsp;" & Format(varTimeNow, "h:mm tt")
            For Each rw As DataRow In dt.Rows
                cntTotal += 1
                LoadList.Text &= CreateLoadRow(rw)
            Next
            lblWorking.Text = cntWorking.ToString
            lblWarning.Text = cntWarning.ToString
            lblTwoPlus.Text = cntLate.ToString

            lblTwoPlusComplete.Text = cntcLate.ToString
            lblComplete.Text = (cntComplete - cntcLate)
            lblTotalLoads.Text = cntTotal.ToString


            lblTwoPlusComplete.Text = IIf(cntcLate > 0, cntcLate.ToString, "--")
            lblComplete.Text = IIf(cntComplete > 0, (cntComplete - cntcLate).ToString, "--")
            lblTotalLoads.Text = IIf(chkHideCompleted.Checked, "--", cntTotal.ToString)
        End If

    End Sub

    Protected Function CreateLoadRow(ByVal rw As DataRow) As String

        Dim txtAppointmentTime As String = "<center>- - -</center>"
        Dim txtDockTime As String = "<center>- - -</center>"
        Dim txtStartTime As String = "<center>- - -</center>"
        Dim txtCompTime As String = "<center>- - -</center>"

        If Not IsDBNull(rw.Item("AppointmentTime")) Then txtAppointmentTime = Format(rw.Item("AppointmentTime"), "hh:mm tt")
        If Not IsDBNull(rw.Item("DockTime")) Then txtDockTime = Format(rw.Item("DockTime"), "hh:mm tt")

        Dim varCompTime As DateTime = IIf(IsDBNull(rw.Item("CompTime")), "1/1/1900", rw.Item("CompTime"))

        Dim isOpen As Boolean = True
        Dim rwStyle As String = "color:#000000;" 'String.Empty
        Dim diff As Long

        Dim locadal As New locaDAL
        Dim offset As Integer = locadal.getLocaTimeZoneOffset(New Guid(cbLocations.SelectedValue)) + 5
        Dim varTimeNow As DateTime = DateAdd(DateInterval.Hour, offset, Date.Now())
        lblTimeNow.Text = "Last Refresh:&nbsp;" & Format(varTimeNow, "h:mm tt")

        ' look at comptime to determine status
        'look at year diff
        If Not IsDBNull(rw.Item("StartTime")) Then
            txtStartTime = Format(rw.Item("StartTime"), "hh:mm tt")
            diff = DateDiff(DateInterval.Year, varCompTime, varTimeNow)
            If diff = 0 Or diff = 1 Then
                rwStyle = "background-color:#BCCCB4;"
                isOpen = False
                cntComplete += 1       'add 1 to completed
                diff = DateDiff(DateInterval.Minute, rw.Item("StartTime"), varCompTime)
                If diff > 120 Then
                    rwStyle &= "color:#FF0000;"
                    cntcLate += 1       'add 1 to completed Late
                End If
            End If
            If isOpen Then
                diff = DateDiff(DateInterval.Minute, rw.Item("StartTime"), varTimeNow)
                cntWorking += 1
                If diff > 119 Then
                    rwStyle = "color:#FF0000;"
                    cntLate += 1
                ElseIf diff > 89 Then
                    rwStyle = "background-color:#FFAA00;"
                    cntWarning += 1
                End If
            End If
        End If

        If varCompTime <> "1/1/1900" Then
            txtCompTime = Format(rw.Item("CompTime"), "hh:mm tt")
        End If



        Dim edal As New empDAL
        Dim unloaders As String = edal.GetUnloadersByWOIDString(rw.Item("ID").ToString)
        If unloaders = "<center>- - -</center>" And txtDockTime <> "<center>- - -</center>" Then rwStyle = "color:blue;"
        Dim str As String = "<tr style=" & rwStyle & ";"">" & _
        "<td>" & rw.Item("DoorNum") & "</td>" & _
        "<td>" & rw.Item("Vendor") & "</td>" & _
        "<td>" & rw.Item("PurchaseOrder") & "</td>" & _
        "<td>" & rw.Item("Carrier") & "</td>" & _
        "<td>" & rw.Item("TrailerNumber") & "</td>" & _
        "<td>" & txtAppointmentTime & "</td>" & _
        "<td>" & txtDockTime & "</td>" & _
        "<td>" & txtStartTime & "</td>" & _
        "<td>" & txtCompTime & "</td>" & _
        "<td>" & rw.Item("Department") & "</td>" & _
        "<td>" & rw.Item("LoadType") & "</td>" & _
        "<td>" & unloaders & "</td>" & _
        "</tr>"

        Return str

    End Function

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRefresh.Click
        UpdateActiveList(cbLocations.SelectedValue)
        '        RadGrid1.Rebind()

    End Sub

    Private Sub thuTime_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles thuTime.Tick
        UpdateActiveList(cbLocations.SelectedValue)
        '        RadGrid1.Rebind()

    End Sub


    Protected Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonList1.SelectedIndexChanged
        Select Case RadioButtonList1.SelectedValue
            Case "1"
                thuTime.Interval = "60000"
            Case "5"
                thuTime.Interval = "300000"
            Case "10"
                thuTime.Interval = "600000"
            Case "15"
                thuTime.Interval = "900000"
        End Select
        UpdateActiveList(cbLocations.SelectedValue)
        '        RadGrid1.Rebind()
    End Sub

    Private Sub chkHideCompleted_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkHideCompleted.CheckedChanged
        If cbLocations.SelectedIndex > -1 Then
            '            RadGrid1.Rebind()
            UpdateActiveList(cbLocations.SelectedValue)
        End If
        lblHideCompletedStatus.Style.Item("Color") = IIf(chkHideCompleted.Checked, "Red", "3F682A")
        lblHideCompletedStatus.Text = IIf(chkHideCompleted.Checked, "NO", "yes")
        chkHideCompleted.ToolTip = IIf(chkHideCompleted.Checked, "UNcheck this box to show ALL loads", "Check this box to hide 'completed' loads")
    End Sub


End Class