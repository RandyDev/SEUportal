Public Class seuFTPImportConfig
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dba As New DBAccess
        If Not IsPostBack Then
            dba.CommandText = "select* from seuFTPimportConfig"
            Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
            Dim row As DataRow = dt.Rows(0)
            txtStartFolder.Text = row.Item("startFolder")
            txteMail.Text = row.Item("email")
            txteMailCC.Text = row.Item("emailCC")
            txtMailServer.Text = row.Item("mailServer")
            numMailPort.Text = row.Item("mailPort")
            txtAuthUserName.Text = row.Item("AuthUsername")
            txtAuthPassword.Text = row.Item("AuthPassword")
        End If

    End Sub

    Private Sub btnSaveChanges_Click(sender As Object, e As EventArgs) Handles btnSaveChanges.Click
        Dim dba As New DBAccess
        dba.CommandText = "UPDATE seuFTPimportConfig Set startFolder=@startFolder, email=@email, emailCC=@emailCC, mailServer=@mailServer, mailPort=@mailPort, AuthUsername=@AuthUsername, AuthPassword=@AuthPassword "
        dba.AddParameter("@startFolder", txtStartFolder.Text)
        dba.AddParameter("@email", txteMail.Text)
        dba.AddParameter("@emailCC", txteMailCC.Text)
        dba.AddParameter("@mailServer", txtMailServer.Text)
        dba.AddParameter("@mailPort", numMailPort.Text)
        dba.AddParameter("@AuthUsername", txtAuthUserName.Text)
        dba.AddParameter("@AuthPassword", txtAuthPassword.Text)
        dba.ExecuteNonQuery()
    End Sub

End Class