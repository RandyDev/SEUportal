Public Class DockMonitorBanners
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session("selectedItems") = Nothing
            ' populate Locations Combo Box (cbLocations on aspx page)
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            Dim showFern As Boolean = User.IsInRole("Administrator") Or User.IsInRole("SysOp")
            ldal.setLocaCombo(puser, cbLocations, showFern)
            checkEnabled.Checked = True
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
        End If
    End Sub
    Private Function chkpage() As String
        Dim retStr As String = String.Empty
        If cbLocations.SelectedIndex < 0 Then
            retStr = " Select Location"
        End If
        Return retStr
    End Function

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim err As String = chkpage()
        If err > String.Empty Then
            lblerr.Text = err
            lblerr.Visible = True
            Exit Sub
        End If
        If btnSave.Text = "Save Changes" Then
            Dim bid As String = gridBanners.SelectedValue.ToString
            Dim locaid As String = cbLocations.SelectedValue
            Dim locaName As String = cbLocations.SelectedItem.Text
            Dim sortOrder As String = cbSort.SelectedItem.Text
            Dim enabled As Boolean = checkEnabled.Checked
            Dim banner As String = RadEditor1.Content
            Dim dba As New DBAccess
            dba.CommandText = "UPDATE DockMonitorBanners SET SortOrder = @sortOrder, enabled=@enabled, banner = @banner WHERE BannerID = @bid"
            dba.AddParameter("bid", bid)
            dba.AddParameter("enabled", enabled)
            dba.AddParameter("banner", banner)
            dba.AddParameter("sortOrder", sortOrder)
            dba.ExecuteNonQuery()
            gridBanners.Rebind()

            btnSave.Text = "Save New Banner"
            RadEditor1.Content = String.Empty
            cbSort.SelectedIndex = -1
        Else

            If RadEditor1.Content > "" Then
                Dim locaid As Guid = New Guid(cbLocations.SelectedValue)

                Dim locaName As String = cbLocations.Text
                Dim sortOrder As String = cbSort.Text
                Dim enabled As Boolean = checkEnabled.Checked
                Dim banner As String = RadEditor1.Content
                Dim dba As New DBAccess
                dba.CommandText = "INSERT INTO DockMonitorBanners (BannerID, LocationID, LocationName, SortOrder, Enabled, Banner) " & _
                    "VALUES (@BannerID,@LocationID,@LocationName,@SortOrder,@Enabled,@Banner)"
                dba.AddParameter("BannerID", Guid.NewGuid)
                dba.AddParameter("LocationID", locaid)
                dba.AddParameter("LocationName", locaName)
                dba.AddParameter("SortOrder", sortOrder)
                dba.AddParameter("Enabled", enabled)
                dba.AddParameter("Banner", banner)
                dba.ExecuteNonQuery()
                gridBanners.Rebind()
                RadEditor1.Content = String.Empty
                cbSort.SelectedIndex = -1
            End If
        End If
    End Sub

    Private Sub GridBanners_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles gridBanners.ItemCommand
        If e.CommandName = "RowClick" Then
            Dim bannerID As Guid = gridBanners.SelectedValue
            Dim dba As New DBAccess
            dba.CommandText = "SELECT BannerID, LocationID, LocationName, SortOrder, Enabled, Banner FROM DockMonitorBanners WHERE BannerID = @bannerID"
            dba.AddParameter("@bannerID", bannerID)
            Dim dt As DataTable
            dt = dba.ExecuteDataSet.Tables(0)
            cbSort.SelectedItem.Text = dt.Rows(0).Item("SortOrder")
            checkEnabled.Checked = dt.Rows(0).Item("Enabled")
            RadEditor1.Content = dt.Rows(0).Item("Banner")
            btnSave.Text = "Save Changes"

        End If
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        cbSort.SelectedIndex = -1
        checkEnabled.Checked = True
        RadEditor1.Content = String.Empty
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim bannerID As Guid = gridBanners.SelectedValue
        Dim dba As New DBAccess
        dba.CommandText = "DELETE FROM DockMonitorBanners WHERE BannerID = @BannerID"
        dba.AddParameter("@BannerID", bannerID)
        dba.ExecuteNonQuery()
        gridBanners.Rebind()
        cbSort.SelectedIndex = -1
        checkEnabled.Checked = True
        RadEditor1.Content = String.Empty
    End Sub
End Class