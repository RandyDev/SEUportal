Imports Telerik.Web.UI

Public Class editCerts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim empID As String = Request("empID")
            RadDatePicker2.SelectedDate = Date.Now()
            lbtnAddCert.CommandArgument = empID
            lbtnAddCert.Visible = False
            divAddCert.Style.Item("display") = "none"

        End If
    End Sub



    'Protected Sub btnSaveCarrier_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveCarrier.Click
    '    'Dim actionString As String = cbCarrier.Text
    '    'actionString &= ":" & cbCarrier.SelectedValue.ToString
    '    'RadAjaxManager1.ResponseScripts.Add("returnArg('" & actionString & "');")
    'End Sub

    Private Sub RadComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBox1.SelectedIndexChanged
        lbtnAddCert.Visible = RadComboBox1.SelectedIndex > -1
        divAddCert.Style.Item("display") = "block"

    End Sub

    Private Sub RadGrid1_DeleteCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.DeleteCommand
        If TypeOf e.Item Is GridDataItem Then
            Dim itm As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim delitem As Guid = itm.OwnerTableView.DataKeyValues(itm.ItemIndex)("ID")
            Dim dba As New DBAccess()
            dba.CommandText = "DELETE FROM Certification WHERE ID = @ID"
            dba.AddParameter("@ID", delitem)
            dba.ExecuteNonQuery()
        End If
        '        RadAjaxManager1.ResponseScripts.Add("setReturnArg('Certification:delete');")
    End Sub



    Private Sub lbtnAddCert_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles lbtnAddCert.Command
        Dim cert As New Certification
        cert.certDate = RadDatePicker2.SelectedDate
        cert.TypeID = New Guid(RadComboBox1.SelectedValue.ToString)
        cert.EmployeeID = New Guid(lbtnAddCert.CommandArgument.ToString)
        cert.ID = Guid.NewGuid()
        Dim dba As New DBAccess()
        dba.CommandText = "INSERT INTO Certification (EmployeeID, TypeID, Date, ID) VALUES (@EmployeeID, @TypeID, @Date, @ID)"
        dba.AddParameter("@EmployeeID", cert.EmployeeID)
        dba.AddParameter("@TypeID", cert.TypeID)
        dba.AddParameter("@Date", cert.certDate)
        dba.AddParameter("@ID", cert.ID)
        Try
            dba.ExecuteNonQuery()
        Catch ex As Exception

        End Try
        RadGrid1.DataBind()
        lbtnAddCert.Visible = False
        RadComboBox1.ClearSelection()
        RadComboBox1.Text = ""

    End Sub

    Private Sub RadGrid1_UpdateCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.UpdateCommand

        If (TypeOf e.Item Is GridEditableItem AndAlso _
         e.Item.IsInEditMode) Then
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)


            Dim cert As New Certification
            cert.ID = editedItem.GetDataKeyValue("ID")

            Dim sDate As RadDatePicker = CType(editedItem.FindControl("rdpCertDate"), RadDatePicker)
            cert.certDate = sDate.SelectedDate
            Dim dba As New DBAccess()
            dba.CommandText = "UPDATE Certification SET [Date]=@date WHERE ID=@ID"
            dba.AddParameter("@date", cert.certDate)
            dba.AddParameter("@ID", cert.ID)
            Try
                dba.ExecuteNonQuery()
            Catch ex As Exception

            End Try
        End If
        RadGrid1.Rebind()
    End Sub
End Class