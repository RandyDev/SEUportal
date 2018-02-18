Imports Telerik.Web.UI

Public Class ViewAudit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim edate As Date = DatePart(DateInterval.Month, Date.Now) & "/" & DatePart(DateInterval.Day, Date.Now) & "/" & DatePart(DateInterval.Year, Date.Now)
            Dim sdate As Date = DateAdd(DateInterval.Day, -7, edate)
            RadDatePicker1.SelectedDate = sdate
            RadDatePicker2.SelectedDate = edate
            RadGrid1.Visible = False
            btnDeleteSelected.Visible = False
        Else
            RadGrid1.Visible = True
            btnDeleteSelected.Visible = True
        End If
    End Sub
    Private Sub OpenWorkOrder()
        Dim a As String = String.Empty

    End Sub

    Private Sub RadGrid1_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If e.CommandName = "RowClick" Then
            Dim woid As String = RadGrid1.SelectedValue.ToString
            '           ReloadForms(woid)
        End If
    End Sub

    Private Sub btnShowRange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShowRange.Click
        RadGrid1.Rebind()
    End Sub

    Private Sub btnDeleteSelected_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteSelected.Click
        Dim nbr As Integer = RadGrid1.SelectedItems.Count
        Dim dba As New DBAccess()
        For Each gi As GridDataItem In RadGrid1.SelectedItems
            Dim id As String = gi("AuditID").Text
            Dim rid As String = gi("PK").Text
            dba.CommandText = "DELETE FROM Audit WHERE AuditID = @auditID"
            dba.AddParameter("@auditID", id)
            dba.ExecuteNonQuery()
        Next
        RadGrid1.Rebind()
    End Sub


    Private Function getds() As DataTable
        Dim sdate As Date = RadDatePicker1.SelectedDate
        Dim edate As Date = RadDatePicker2.SelectedDate
        edate = DateAdd(DateInterval.Day, 1, edate)
        Dim dba As New DBAccess()
        Dim strSelect As String = "SELECT * FROM [Audit] WHERE UpdateDate >= @sdate AND UpdateDate < @edate "
        If txtFilter.Text.Trim() > "" Then
            Dim strFilter As String = String.Format("AND ((TableName LIKE '{0}%') OR (FieldName LIKE '{0}%')) ", txtFilter.Text.Trim())
            strSelect &= strFilter
        End If
        strSelect &= "ORDER BY [TableName], [FieldName], [UpdateDate] DESC"
        dba.CommandText = strSelect
        dba.AddParameter("@sdate", sdate)
        dba.AddParameter("@edate", edate)
        Dim ds As DataSet = dba.ExecuteDataSet
        Dim dt As DataTable = ds.Tables(0)
        Return dt
    End Function

    Private Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim sdate As Date = RadDatePicker1.SelectedDate
        Dim edate As Date = RadDatePicker2.SelectedDate
        edate = DateAdd(DateInterval.Day, 1, edate)
        Dim dba As New DBAccess()
        Dim strSelect As String = "SELECT * FROM [Audit] WHERE UpdateDate >= @sdate AND UpdateDate < @edate "
        If txtFilter.Text.Trim() > "" Then
            Dim strFilter As String = String.Format("AND ((TableName LIKE '{0}%') OR (FieldName LIKE '{0}%')) ", txtFilter.Text.Trim())
            strSelect &= strFilter
        End If
        strSelect &= "ORDER BY [TableName], [FieldName], [UpdateDate] DESC"
        dba.CommandText = strSelect
        dba.AddParameter("@sdate", sdate)
        dba.AddParameter("@edate", edate)
        Dim ds As DataSet = dba.ExecuteDataSet
        Dim dt As DataTable = ds.Tables(0)
        RadGrid1.DataSource = dt
    End Sub
End Class
