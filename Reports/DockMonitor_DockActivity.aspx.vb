Imports Telerik.Web.UI

Public Class DockMonitor_DockActivity
    Inherits System.Web.UI.Page

    Protected cntWorking As Integer = 0
    Protected cntWarning As Integer = 0
    Protected cntLate As Integer = 0
    Protected cntcLate As Integer = 0
    Protected cntComplete As Integer = 0
    Protected cntTotal As Integer = 0

    Protected locaID As String = String.Empty ' "CFE1F703-F8D3-4F23-B30C-192672B13BCC"
    Protected hideCompleted As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("vState") = 1
            If Request.QueryString("locaID") Is Nothing Then
                Dim ldal As New locaDAL()
                Dim dt As DataTable = ldal.getLocations()
                cbLocation.DataSource = dt
                cbLocation.DataTextField = "LocationName"
                cbLocation.DataValueField = "locaID"
                cbLocation.DataBind()
                cbLocation.AutoPostBack = True
                cbLocation.Visible = True
                cbLocation.ClearSelection()
                cbLocation.EmptyMessage = "Select Location"
                RadAjaxPanel1.Visible = False
                ' use the dropdown above or ... we can just end it 
                '                Response.Write("This page not directly accessable")
                '                Response.End()
            Else
                locaID = Request.QueryString("locaID")
                Dim root As String = Server.MapPath("\")
                imgClientLogo.ImageUrl = Utilities.getLogo(New Guid(locaID), root)
                If Utilities.IsValidGuid(locaID) Then
                    'we have valid guid for locationID in the queryString
                    Dim ldal As New locaDAL()
                    Dim lnam As String = ldal.getLocationNameByID(locaID)
                    loadTicker()
                    lblLocaName.Text = lnam
                    lblDate.Text = Date.Now().ToString("dddd - dd MMM yyyy")
                    lblGridFooter.Text = Date.Now.ToString("hh:mm tt")
                    RadAjaxPanel1.Visible = True
                    RadGrid1.Rebind()
                    RadAjaxManager1.ResponseScripts.Add("refreshTimer = setTimeout(""InitiateAjaxRequest('argMe')"", 60000);")
                End If
            End If
        End If
    End Sub

    Private Sub loadTicker()
        Dim ticketItems As New List(Of TickerItem)
        Dim ti As TickerItem = New TickerItem
        Dim dtnow As Date = FormatDateTime(Date.Now(), DateFormat.ShortDate)
        Dim woDAL As New WorkOrderDAL
        Dim protable As DataTable = woDAL.getWorkOrderProductivityByDateRangeAndLocation(New Guid(locaID), DateAdd(DateInterval.Day, -14, dtnow), dtnow)
        'WorkDate NumOfPOs NumOfLoads PalUnld Pieces PalRecd Bad Resk Hours
        Dim rw As DataRow = protable.Rows(0)
        ti = New TickerItem()
        ti.Name = "Pallets per man hour: " & CInt((rw("PalRecd") / rw("Hours"))).ToString & " &nbsp; Cases per man hour: " & CInt((rw("Pieces") / rw("Hours"))).ToString & "<br><< Today >>"
        ticketItems.Add(ti)
        Dim pals, pcs, ttlhrs As Integer
        For i = 0 To 6
            rw = protable.Rows(i)
            pals += rw("PalRecd")
            pcs += rw("Pieces")
            ttlhrs += rw("Hours")
        Next
        ti = New TickerItem()
        ti.Name = "Pallets per man hour: " & CInt((pals / ttlhrs)).ToString & " &nbsp; Cases per man hour: " & CInt((pcs / ttlhrs)).ToString & "<br>(Average past 7 days)"
        ticketItems.Add(ti)
        Dim decOverTwo As Decimal = woDAL.getPercentLoadsOverTwo(New Guid(locaID), DateAdd(DateInterval.Day, -14, dtnow), dtnow)
        ti = New TickerItem()
        ti.Name = "Percentage of loads over 2 hours: &nbsp;" & decOverTwo.ToString("0%") & "<br />(Average past two weeks)"
        ticketItems.Add(ti)
        Dim decUnderTwo As Decimal = 1 - decOverTwo
        ti = New TickerItem()
        ti.Name = "Percentage of loads under 2 hours: &nbsp;" & decUnderTwo.ToString("0%") & "<br />(Average past two weeks)"
        ticketItems.Add(ti)
        Radticker1.DataSource = ticketItems
        Radticker1.DataBind()
    End Sub

    Private Sub RadGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
    End Sub

    Private Sub RadGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim locaID As String = Me.locaID   ' "CFE1F703-F8D3-4F23-B30C-192672B13BCC" 'Request.QueryString("locaID")
        Dim hideCompleted As Boolean = Me.hideCompleted
        lblGridTitle.Text = IIf(hideCompleted, "Active Loads", "All Loads")
        Dim locadal As New locaDAL
        ' ********************** This static offset (for server location) needs to be maintained
        ' ********************** This static offset (for server location) needs to be maintained
        Dim offset As Integer = locadal.getLocaTimeZoneOffset(New Guid(locaID)) + 6
        ' ********************** This static offset (for server location) needs to be maintained
        ' ********************** This static offset (for server location) needs to be maintained
        Dim varTimeNow As DateTime = DateAdd(DateInterval.Hour, offset, Date.Now())
        lblGridFooter.Text = varTimeNow.ToString("hh:mm tt")
        Dim wdal As New WorkOrderDAL()
        Dim dt As New DataTable
        If Utilities.IsValidGuid(locaID) Then
            dt = wdal.GetActiveWorkOrders(New Guid(locaID), hideCompleted)
            RadGrid1.DataSource = dt
        End If
        If dt.Rows.Count > 25 Then
            RadAjaxManager1.ResponseScripts.Add("pageScroll();")
        Else
            RadAjaxManager1.ResponseScripts.Add("clearTimeout(scrollTimer);")
        End If
    End Sub

    Private Sub RadAjaxManager1_AjaxRequest(sender As Object, e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        Dim arg As String = e.Argument
        Dim pg As String = String.Empty
        Dim ldal As New locaDAL()
        locaID = cbLocation.SelectedValue.ToString
        Select Case Session("vState")
            Case 1
                Session("vState") = 2
                hideCompleted = True 'False
                loadTicker()
                '               UpdateActiveList()
                RadGrid1.Rebind()
                RadAjaxManager1.ResponseScripts.Add("refreshTimer = setTimeout(""InitiateAjaxRequest('argMe', 'argit')"", 15000);")
            Case 2
                Dim rootDir As String = Server.MapPath("~/")
                Dim logo As String = Utilities.getLogo(New Guid(locaID), rootDir)
                '                lblHelloWorld.Text = logo
                Session("vState") = 1
                hideCompleted = False
                RadGrid1.Rebind()
                RadAjaxManager1.ResponseScripts.Add("refreshTimer = setTimeout(""InitiateAjaxRequest('argMe', 'argit')"", 15000);")
                'Case 3
                '    Dim rootDir As String = Server.MapPath("~/")
                '    Dim logo As String = Utilities.getLogo(New Guid(locaID), rootDir)
                '    '                lblHelloWorld.Text = logo
                '    Session("vState") = 1
                '    hideCompleted = True
                '    RadGrid1.Rebind()
                '    RadAjaxManager1.ResponseScripts.Add("refreshTimer = setTimeout(""InitiateAjaxRequest('argMe', 'argit')"", 5000);")
        End Select
    End Sub

    Private Sub cbLocation_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocation.SelectedIndexChanged
        locaID = cbLocation.SelectedValue.ToString
        Dim root As String = Server.MapPath("\")
        imgClientLogo.ImageUrl = Utilities.getLogo(New Guid(locaID), root)
        'we have valid guid for locationID in the queryString
        Dim ldal As New locaDAL()
        Dim lnam As String = ldal.getLocationNameByID(locaID)
        loadTicker()
        lblLocaName.Text = lnam
        lblDate.Text = Date.Now().ToString("dddd - dd MMM yyyy")
        RadAjaxPanel1.Visible = True
        cbLocation.Visible = False
        RadGrid1.Visible = True
        RadGrid1.Rebind()
        RadAjaxManager1.ResponseScripts.Add("refreshTimer = setTimeout(""InitiateAjaxRequest('argMe', 'argit')"", 15000);")
    End Sub

    Private Sub RadGrid1_PreRender(sender As Object, e As System.EventArgs) Handles RadGrid1.PreRender
        For Each dataItem As GridDataItem In RadGrid1.MasterTableView.Items
            If dataItem.ItemType = GridItemType.Item Or dataItem.ItemType = GridItemType.AlternatingItem Then
                Dim txtDockTime As String = "<center>- - -</center>"
                Dim txtStartTime As String = "<center>- - -</center>"
                Dim txtCompTime As String = "<center>- - -</center>"
                Dim dTime As Date = dataItem("DockTime").Text
                Dim sTime As Date = dataItem("StartTime").Text
                Dim cTime As Date = dataItem("CompTime").Text
                dataItem.ForeColor = Drawing.Color.Black
                dataItem.BackColor = Drawing.Color.White
                If Not IsDBNull(dataItem("DockTime")) Then txtDockTime = Format(dTime, "hh:mm tt")
                Dim varCompTime As DateTime = IIf(IsDBNull(dataItem("CompTime")), "1/1/1900", cTime)
                Dim isOpen As Boolean = True
                Dim rwStyle As String = "color:#000000;" 'String.Empty
                Dim diff As Long
                Dim locadal As New locaDAL
                Dim offset As Integer = locadal.getLocaTimeZoneOffset(New Guid(locaID)) + 5
                Dim varTimeNow As DateTime = DateAdd(DateInterval.Hour, offset, Date.Now())
                ' look at comptime to determine status
                'look at year diff
                If Not IsDBNull(dataItem("StartTime")) Then
                    txtStartTime = Format(sTime, "hh:mm tt")
                    diff = DateDiff(DateInterval.Year, varCompTime, varTimeNow)
                    If diff = 0 Or diff = 1 Then
                        '                        rwStyle = "background-color:#BCCCB4;"
                        dataItem.BackColor = Drawing.Color.FromArgb(188, 204, 180) 'bcccb4
                        isOpen = False
                        cntComplete += 1       'add 1 to completed
                        diff = DateDiff(DateInterval.Minute, sTime, varCompTime)
                        If diff > 120 Then
                            '                            rwStyle &= "color:#FF0000;"
                            dataItem.ForeColor = Drawing.Color.FromArgb(255, 0, 0) 'ff0000
                            cntcLate += 1       'add 1 to completed Late
                        End If
                    End If
                    If isOpen Then
                        diff = DateDiff(DateInterval.Minute, sTime, varTimeNow)
                        cntWorking += 1
                        If diff > 119 Then
                            dataItem.ForeColor = Drawing.Color.FromArgb(255, 0, 0) 'ff0000
                            '                            rwStyle = "color:#FF0000;"
                            cntLate += 1
                        ElseIf diff > 89 Then
                            '                            rwStyle = "background-color:#FFAA00;"
                            dataItem.BackColor = Drawing.Color.FromArgb(255, 170, 0) 'ffaa00
                            cntWarning += 1
                        End If
                    End If
                End If
                If varCompTime <> "1/1/1900" Then
                    txtCompTime = Format(cTime, "hh:mm tt")
                End If
                dataItem.Item("StartTime").Text = txtStartTime
                dataItem("DockTime").Text = txtDockTime
                dataItem("CompTime").Text = txtCompTime
            End If
        Next
    End Sub
End Class