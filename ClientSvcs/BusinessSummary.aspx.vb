Imports Telerik.Web.UI

Public Class BusinessSummary
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' set location combobox
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)

            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager") Or User.IsInRole("Client")
            dpEndDate.SelectedDate = FormatDateTime(Date.Now(), DateFormat.ShortDate)
            dpStartDate.SelectedDate = FormatDateTime(DateAdd(DateInterval.Day, -14, Date.Now), DateFormat.ShortDate)
            If Request.QueryString("locaID") > "" Then cbLocations.SelectedValue = Request.QueryString("locaID")
            '            If Request.QueryString("locaName") > "" Then cbLocations.SelectedItem.Text = Request.QueryString("locaName")
            If Request.QueryString("endDate") > "" Then dpEndDate.SelectedDate = Request.QueryString("endDate")
            If Request.QueryString("startDate") > "" Then dpStartDate.SelectedDate = Request.QueryString("startDate")

        End If
    End Sub

    Private Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        If cbLocations.SelectedIndex > -1 Then
            Dim ldal As New locaDAL
            Dim loca As String = cbLocations.SelectedItem.Text
            Dim locaID As Guid = New Guid(cbLocations.SelectedValue.ToString)
            Dim sdate As DateTime = FormatDateTime(dpStartDate.SelectedDate, DateFormat.ShortDate)
            Dim edate As DateTime = FormatDateTime(dpEndDate.SelectedDate, DateFormat.ShortDate)
            edate = DateAdd(DateInterval.Day, 1, edate)
            edate = DateAdd(DateInterval.Second, -1, edate)
            Dim offset As Integer = ldal.getLocaBDOffset(locaID)
            sdate = DateAdd(DateInterval.Hour, offset, sdate)
            edate = DateAdd(DateInterval.Hour, offset, edate)
            Dim rootDir As String = Server.MapPath("~/")
            Dim logoBG As String = "url(" & Utilities.getLogobg(New Guid(cbLocations.SelectedValue), rootDir) & ")"
            theBody.Style.Item("BACKGROUND-IMAGE") = logoBG
            Dim dba As New DBAccess()
            Dim sqlStr As String = "SELECT dbo.LoadType.Name AS [LoadType], dbo.Department.Name AS Department, COUNT(dbo.LoadType.Name) AS Loads,  " & _
                "SUM(dbo.WorkOrder.PalletsUnloaded) AS [PalletsUnloaded], SUM(dbo.WorkOrder.PalletsReceived) AS [PalletsReceived],  " & _
                "SUM(dbo.WorkOrder.Pieces) AS Pieces, CONVERT(FLOAT, SUM(dbo.WorkOrder.Weight)) AS Weight, " & _
                "SUM(dbo.WorkOrder.Amount) AS Amount, AVG(dbo.WorkOrder.Amount) AS Avrg " & _
                "FROM dbo.WorkOrder INNER JOIN " & _
                "dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID INNER JOIN " & _
                "dbo.Department ON dbo.WorkOrder.DepartmentID = dbo.Department.ID INNER JOIN " & _
                "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID AND dbo.Location.Name = @Location "
            If offset <> 0 Then
                sqlStr &= "WHERE (dbo.WorkOrder.DockTime >= @startDate) AND (dbo.WorkOrder.DockTime <= @endDate) AND  "
            Else
                sqlStr &= "WHERE (dbo.WorkOrder.LogDate >= @startDate) AND (dbo.WorkOrder.LogDate <= @endDate) AND  "
            End If
            sqlStr &= "(dbo.Department.Name <> 'BJ Wholesale') " & _
                "GROUP BY dbo.Department.Name, dbo.LoadType.Name "
            dba.CommandText = sqlStr
            dba.AddParameter("@startDate", sdate)
            dba.AddParameter("@endDate", edate)
            dba.AddParameter("@Location", loca)
            Dim dt As New DataTable
            Try
                dt = dba.ExecuteDataSet.Tables(0)
            Catch ex As Exception

            End Try
            RadGrid1.DataSource = dt
        End If
    End Sub

    Private Sub btnShowRecords_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnShowRecords.Command
        RadGrid1.Rebind()
    End Sub

    Protected Sub RadAjaxManager1_AjaxSettingCreating(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxSettingCreatingEventArgs) Handles RadAjaxManager1.AjaxSettingCreating
        If (e.Initiator.ID.StartsWith("mngBtn")) Then
            e.UpdatePanel.RenderMode = UpdatePanelRenderMode.Inline
        End If
    End Sub

    Private Sub lbReportView_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles lbReportView.Command
        Dim argStr As String = String.Empty
        argStr = "~/Reports/Reports.aspx?"
        argStr &= "report=BusinessSummary&"
        If Not dpStartDate.SelectedDate Is Nothing Then
            argStr &= "startDate=" & dpStartDate.SelectedDate & "&"
        End If
        If Not dpEndDate.SelectedDate Is Nothing Then
            argStr &= "endDate=" & dpEndDate.SelectedDate & "&"
        End If
        If cbLocations.SelectedIndex > -1 Then
            argStr &= "locaID=" & cbLocations.SelectedValue & "&"
            argStr &= "locaName=" & cbLocations.SelectedItem.Text & "&"
        Else
            argStr &= "locaID=""""&"
            argStr &= "locaName=""""&"
        End If
        Response.Redirect(argStr)
    End Sub


#Region "Exporting"

    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        RadGrid1.ExportSettings.ExportOnlyData = True
        RadGrid1.ExportSettings.IgnorePaging = True
        RadGrid1.ExportSettings.OpenInNewWindow = True
        RadGrid1.MasterTableView.ExportToExcel()
    End Sub

    Protected Sub btnWord_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        RadGrid1.ExportSettings.ExportOnlyData = True
        RadGrid1.ExportSettings.IgnorePaging = True
        RadGrid1.ExportSettings.OpenInNewWindow = True
        RadGrid1.MasterTableView.ExportToWord()
    End Sub
    Protected Sub btnCSV_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        RadGrid1.ExportSettings.ExportOnlyData = True
        RadGrid1.ExportSettings.IgnorePaging = True
        RadGrid1.ExportSettings.OpenInNewWindow = True
        RadGrid1.MasterTableView.ExportToCSV()
    End Sub
    Protected Sub btnPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        RadGrid1.ExportSettings.ExportOnlyData = False
        RadGrid1.ExportSettings.IgnorePaging = True
        RadGrid1.ExportSettings.OpenInNewWindow = True
        RadGrid1.MasterTableView.ExportToPdf()
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

    Private Sub RadGrid1_PdfExporting(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPdfExportingArgs) Handles RadGrid1.PdfExporting

    End Sub

#End Region

End Class