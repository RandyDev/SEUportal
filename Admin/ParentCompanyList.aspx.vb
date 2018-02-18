Imports Telerik.Web.UI

Public Class ParentCompanyList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub RadGrid1_ItemUpdated(ByVal source As Object, ByVal e As Telerik.Web.UI.GridUpdatedEventArgs) Handles RadGrid1.ItemUpdated
        Dim item As GridEditableItem = e.Item
        Dim id As String = item.GetDataKeyValue("ID").ToString()

        If Not e.Exception Is Nothing Then
            e.KeepInEditMode = True
            e.ExceptionHandled = True
            SetMessage("Product with ID " + id + " cannot be updated. Reason: " + e.Exception.Message)
        Else
            SetMessage("Product with ID " + id + " is updated!")
        End If
    End Sub

    Protected Sub RadGrid1_ItemInserted(ByVal source As Object, ByVal e As Telerik.Web.UI.GridInsertedEventArgs) Handles RadGrid1.ItemInserted
        If Not e.Exception Is Nothing Then
            e.ExceptionHandled = True
            e.KeepInInsertMode = True
            SetMessage("Company cannot be inserted. Reason: " + e.Exception.Message)
        Else
            SetMessage("New Company is inserted!")
        End If
    End Sub

    Private Sub DisplayMessage(ByVal text As String)
        RadGrid1.Controls.Add(New LiteralControl(String.Format("<span style='color:red'>{0}</span>", text)))
    End Sub

    Private Sub SetMessage(ByVal message As String)
        gridMessage = message
    End Sub

    Private gridMessage As String = Nothing
    Protected Sub RadGrid1_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles RadGrid1.DataBound
        If Not String.IsNullOrEmpty(gridMessage) Then
            DisplayMessage(gridMessage)
        End If
    End Sub

    Protected Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim dba As New DBAccess()
        dba.ConnectionString = "Data Source=reports.div-log.com;Initial Catalog=RTDS;Persist Security Info=True;User ID=RTDS;Password=southeast1"
        dba.CommandText = "SELECT [Name], [ID] FROM [ParentCompany] ORDER BY [Name]"
        RadGrid1.DataSource = dba.ExecuteDataSet.Tables(0)
    End Sub

    Protected Sub RadGrid1_UpdateCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles RadGrid1.UpdateCommand
        Dim actionString As String = String.Empty

        Dim item As GridEditableItem = TryCast(e.Item, GridEditableItem)
        Dim companyID = item.GetDataKeyValue("ID").ToString()
        Dim values As New Hashtable()
        item.ExtractValues(values)
        Dim name As String = values.Item("Name")
        Dim dba As New DBAccess()
        dba.CommandText = "UPDATE ParentCompany SET Name=@Name WHERE ID=@ID"
        dba.AddParameter("@Name", Name)
        dba.AddParameter("@ID", companyID)
        dba.ExecuteNonQuery()
        actionString = "UpdateParent:" & companyID & ":" & name
        RadAjaxManager1.ResponseScripts.Add("returnArg(""" & actionString & """);")

    End Sub

    Protected Sub RadGrid1_InsertCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles RadGrid1.InsertCommand
        Dim actionString As String = String.Empty
        Dim item As GridEditableItem = TryCast(e.Item, GridEditableItem)
        Dim values As New Hashtable()
        item.ExtractValues(values)
        Dim name As String = values.Item("Name")
        Dim id As Guid = Guid.NewGuid()
        Dim dba As New DBAccess()
        dba.CommandText = "INSERT INTO ParentCompany (ID, Name) VALUES (@ID, @Name)"
        dba.AddParameter("@Name", name)
        dba.AddParameter("@ID", id)
        dba.ExecuteNonQuery()
        actionString = "NewParent:" & id.ToString() & ":" & name
        RadAjaxManager1.ResponseScripts.Add("returnArg(""" & actionString & """);")

    End Sub
End Class
