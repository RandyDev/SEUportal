Imports Telerik.Web.UI


Public Class JobTitleList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub RadGrid1_InsertCommand1(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.InsertCommand

    End Sub
    Protected Sub RadGrid1_ItemInserted(ByVal source As Object, ByVal e As Telerik.Web.UI.GridInsertedEventArgs) Handles RadGrid1.ItemInserted
        Dim actionString As String = String.Empty
        Dim item As Telerik.Web.UI.GridEditableItem = TryCast(e.Item, GridEditableItem)
        Dim values As New Hashtable()
        item.ExtractValues(values)
        Dim JobTitle As String = values.Item("JoTitle")
        Dim JobTitleID As Guid = Guid.NewGuid()
        Dim dba As New DBAccess()
        dba.CommandText = "INSERT INTO JobTitle (JobTitleID, JobTitle) VALUES (@JobTitleID @JobTitle)"
        dba.AddParameter("@JobTitle", JobTitle)
        dba.AddParameter("@JobTitleID", JobTitleID)
        dba.ExecuteNonQuery()
        actionString = "NewJobTitle:" & JobTitleID.ToString() & ":" & JobTitle
        '        RadAjaxManager1.ResponseScripts.Add("returnArg(""" & actionString & """);")
    End Sub
    'Private Sub DisplayMessage(ByVal text As String)
    '    RadGrid1.Controls.Add(New LiteralControl(String.Format("<span style='color:red'>{0}</span>", text)))
    'End Sub

    'Private Sub SetMessage(ByVal message As String)
    '    gridMessage = message
    'End Sub
    'Protected Sub RadGrid1_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles RadGrid1.DataBound
    '    If Not String.IsNullOrEmpty(gridMessage) Then
    '        DisplayMessage(gridMessage)
    '    End If
    'End Sub

    Private Sub RadGrid1_NeedDataSource1(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim dba As New DBAccess()
        dba.ConnectionString = "Data Source=reports.div-log.com;Initial Catalog=RTDS;Persist Security Info=True;User ID=RTDS;Password=southeast1"
        dba.CommandText = "SELECT [JobTitleID], [JobTitle], [IsActive] FROM [jobTitle] ORDER BY [JobTitle]"
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        RadGrid1.DataSource = dt
    End Sub
    Protected Sub RadGrid1_UpdateCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles RadGrid1.UpdateCommand
        Dim actionString As String = String.Empty

        Dim item As GridEditableItem = TryCast(e.Item, GridEditableItem)
        Dim JobTitleID = item.GetDataKeyValue("JobTitleID").ToString()
        Dim values As New Hashtable()
        item.ExtractValues(values)
        Dim JobTitle As String = values.Item("JobTitle")
        Dim IsActive As Boolean = values.Item("IsActive")
        Dim dba As New DBAccess()
        dba.CommandText = "UPDATE JobTitle SET JobTitle=@JobTitle, IsActive=@IsActive WHERE JobTitleID=@JobTitleID"
        dba.AddParameter("@JobTitle", JobTitle)
        dba.AddParameter("@JobTitleID", JobTitleID)
        dba.AddParameter("@IsActive", IsActive)
        dba.ExecuteNonQuery()
        actionString = "UpdateJobTitle:" & JobTitleID & ":" & JobTitle
        RadAjaxManager1.ResponseScripts.Add("returnArg(""" & actionString & """);")

    End Sub

    Protected Sub RadGrid1_InsertCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles RadGrid1.InsertCommand
        Dim actionString As String = String.Empty
        Dim item As GridEditableItem = TryCast(e.Item, GridEditableItem)
        Dim values As New Hashtable()
        item.ExtractValues(values)
        Dim IsActive As Boolean = values.Item("IsActive")
        Dim JobTitle As String = values.Item("JobTitle")
        Dim JobTitleID As Guid = Guid.NewGuid()
        Dim dba As New DBAccess()
        dba.CommandText = "INSERT INTO JobTitle (JobTitleID, JobTitle,IsActive) VALUES (@JobTitleID, @JobTitle, @IsActive)"
        dba.AddParameter("@JobTitle", JobTitle)
        dba.AddParameter("@JobTitleID", JobTitleID)
        dba.AddParameter("@IsActive", IsActive)
        dba.ExecuteNonQuery()
        actionString = "NewJobTitle:" & JobTitleID.ToString() & ":" & JobTitle
        RadAjaxManager1.ResponseScripts.Add("returnArg(""" & actionString & """);")

    End Sub

    Private Sub RadGrid1_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If TypeOf e.Item Is GridDataInsertItem Then
            Dim dataitem As GridDataItem = e.Item
            DirectCast(dataitem("IsActive").Controls(0), CheckBox).Checked = True
        End If
    End Sub
End Class