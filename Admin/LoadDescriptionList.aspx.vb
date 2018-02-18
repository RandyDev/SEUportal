Imports Telerik.Web.UI

Public Class LoadDescriptionList
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
        Dim Name As String = values.Item("Name")
        Dim ID As Guid = Guid.NewGuid()
        Dim InActive As Boolean = values.Item("InActive")
        Dim dba As New DBAccess()
        dba.CommandText = "INSERT INTO Description (ID, Name,InActive) VALUES (@ID, @Name,@InActive)"
        dba.AddParameter("@Name", Name)
        dba.AddParameter("@ID", ID)
        dba.AddParameter("@InActive", InActive)
        dba.ExecuteNonQuery()
        actionString = "NewJobTitle:" & ID.ToString() & ":" & Name
        RadAjaxManager1.ResponseScripts.Add("returnArg(""" & actionString & """);")
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
        dba.CommandText = "SELECT [ID], [Name], [InActive] FROM [Description] ORDER BY [Name]"
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        RadGrid1.DataSource = dt
    End Sub


    Protected Sub RadGrid1_UpdateCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles RadGrid1.UpdateCommand
        Dim actionString As String = String.Empty

        Dim item As GridEditableItem = TryCast(e.Item, GridEditableItem)
        Dim ID = item.GetDataKeyValue("ID").ToString()
        Dim values As New Hashtable()
        item.ExtractValues(values)
        Dim Name As String = values.Item("Name")
        Dim dba As New DBAccess()
        dba.CommandText = "UPDATE Description SET Name=@Name WHERE ID=@ID"
        dba.AddParameter("@Name", Name)
        dba.AddParameter("@ID", ID)
        dba.ExecuteNonQuery()
        actionString = "UpdateJobTitle:" & ID & ":" & Name
        RadAjaxManager1.ResponseScripts.Add("returnArg(""" & actionString & """);")

    End Sub

    Protected Sub RadGrid1_InsertCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles RadGrid1.InsertCommand
        Dim actionString As String = String.Empty
        Dim item As GridEditableItem = TryCast(e.Item, GridEditableItem)
        Dim values As New Hashtable()
        item.ExtractValues(values)
        Dim InActive As Boolean = values.Item("InActive")
        Dim Name As String = values.Item("Name")
        Dim ID As Guid = Guid.NewGuid()
        Dim dba As New DBAccess()
        dba.CommandText = "INSERT INTO Description (ID, Name, InActive) VALUES (@ID, @Name,@InActive)"
        dba.AddParameter("@Name", Name)
        dba.AddParameter("@ID", ID)
        dba.AddParameter("@JobTitleID", InActive)
        dba.ExecuteNonQuery()
        actionString = "NewLoadDescription" & ID.ToString() & ":" & Name
        RadAjaxManager1.ResponseScripts.Add("returnArg(""" & actionString & """);")

    End Sub

    Private Sub RadGrid1_UpdateCommand1(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.UpdateCommand
        Dim actionString As String = String.Empty
        Dim item As GridEditableItem = TryCast(e.Item, GridEditableItem)
        Dim values As New Hashtable()
        item.ExtractValues(values)
        Dim InActive As Boolean = values.Item("InActive")
        Dim Name As String = values.Item("Name")
        Dim ID As Guid = Guid.NewGuid()
        Dim dba As New DBAccess()
        dba.CommandText = "INSERT INTO Description (ID, Name, InActive) VALUES (@ID, @Name,@InActive)"
        dba.AddParameter("@Name", Name)
        dba.AddParameter("@ID", ID)
        dba.AddParameter("@JobTitleID", InActive)
        dba.ExecuteNonQuery()
        actionString = "NewLoadDescription" & ID.ToString() & ":" & Name
        RadAjaxManager1.ResponseScripts.Add("returnArg(""" & actionString & """);")
    End Sub
End Class