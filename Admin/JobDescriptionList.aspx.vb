Imports Telerik.Web.UI

Public Class JobDescriptionList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub RadGrid1_InsertCommand1(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.InsertCommand

    End Sub
    Protected Sub RadGrid1_ItemInserted(ByVal source As Object, ByVal e As Telerik.Web.UI.GridInsertedEventArgs) Handles RadGrid1.ItemInserted
        Dim actionString As String = String.Empty
        Dim item As Telerik.Web.UI.GridEditableItem = e.Item
        Dim values As New Hashtable()
        item.ExtractValues(values)
        Dim JobDescription As String = values.Item("JoTitle")
        Dim JobDescriptionID As Guid = Guid.NewGuid()
        Dim isHourly As Boolean = values.Item("IsHourly")
        Dim IsActive As Boolean = values.Item("IsActive")
        Dim dba As New DBAccess()
        dba.CommandText = "INSERT INTO JobDescriptions (ID, JobDescription, IsHourly, IsActive) VALUES (@JobDescriptionID @JobDescription,@IsHourly, @IsActive)"
        dba.AddParameter("@JobDescription", JobDescription)
        dba.AddParameter("@JobDescriptionID", JobDescriptionID)
        dba.AddParameter("@IsHourly", IsHourly)
        dba.AddParameter("@IsActive", IsActive)
        dba.ExecuteNonQuery()
        actionString = "NewJobDescription:" & JobDescriptionID.ToString() & ":" & JobDescription
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
        dba.CommandText = "SELECT [ID], [JobDescription], [IsHourly], [IsActive] FROM [JobDescriptions] ORDER BY [JobDescription]"
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        RadGrid1.DataSource = dt
    End Sub
    Protected Sub RadGrid1_UpdateCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles RadGrid1.UpdateCommand
        Dim actionString As String = String.Empty

        Dim item As GridEditableItem = TryCast(e.Item, GridEditableItem)
        Dim JobDescriptionID = item.GetDataKeyValue("ID").ToString()
        Dim values As New Hashtable()
        item.ExtractValues(values)
        Dim JobDescription As String = values.Item("JobDescription")
        Dim IsActive As Boolean = values.Item("IsActive")
        Dim IsHourly As Boolean = values.Item("IsHourly")
        Dim dba As New DBAccess()
        dba.CommandText = "UPDATE JobDescriptions SET JobDescription=@JobDescription, IsHourly=@IsHourly, IsActive=@IsActive WHERE ID=@JobDescriptionID"
        dba.AddParameter("@JobDescription", JobDescription)
        dba.AddParameter("@JobDescriptionID", JobDescriptionID)
        dba.AddParameter("@IsHourly", IsHourly)
        dba.AddParameter("@IsActive", IsActive)
        dba.ExecuteNonQuery()
        actionString = "UpdateJobDescription:" & JobDescriptionID & ":" & JobDescription
        RadAjaxManager1.ResponseScripts.Add("returnArg(""" & actionString & """);")

    End Sub

    Protected Sub RadGrid1_InsertCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles RadGrid1.InsertCommand
        Dim actionString As String = String.Empty
        Dim item As GridEditableItem = TryCast(e.Item, GridEditableItem)
        Dim values As New Hashtable()
        item.ExtractValues(values)
        Dim IsActive As Boolean = values.Item("IsActive")
        Dim IsHourly As Boolean = values.Item("IsHourly")
        Dim JobDescription As String = values.Item("JobDescription")
        Dim JobDescriptionID As Guid = Guid.NewGuid()
        Dim dba As New DBAccess()
        dba.CommandText = "INSERT INTO JobDescriptions (ID, JobDescription,IsHourly,IsActive) VALUES (@JobDescriptionID, @JobDescription, @IsHourly, @IsActive)"
        dba.AddParameter("@JobDescription", JobDescription)
        dba.AddParameter("@JobDescriptionID", JobDescriptionID)
        dba.AddParameter("@IsActive", IsActive)
        dba.AddParameter("@IsHourly", IsHourly)
        dba.ExecuteNonQuery()
        actionString = "NewJobDescription:" & JobDescriptionID.ToString() & ":" & JobDescription
        RadAjaxManager1.ResponseScripts.Add("returnArg(""" & actionString & """);")

    End Sub

    Private Sub RadGrid1_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If TypeOf e.Item Is GridDataInsertItem Then
            Dim dataitem As GridDataItem = e.Item
            DirectCast(dataitem("IsActive").Controls(0), CheckBox).Checked = True
        End If
    End Sub
End Class