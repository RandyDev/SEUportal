'Imports System.IO
'Imports System.Web.Services
'Imports System.Runtime.Serialization
'Imports System.Collections.Generic
Imports Telerik.Web.UI
'Imports System.Web
Imports System.Data
'Imports System.Data.SqlClient
'Imports System.Drawing
'Imports System.Drawing.Imaging

Public Class LoadImages
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Populate the default (base) upload configuration into an object of type SampleAsyncUploadConfiguration

        Dim woid As String = Request.QueryString("woid").Trim.ToString
        If Not Utilities.IsValidGuid(woid) Then
            'NOT A VALID GUID FOR WORK ORDER ID
            ' TO DO
            ' do something else, exit, redirect, etc
        End If

        Dim config As LoadPicAsyncUploadConfiguration = RadAsyncUpload1.CreateDefaultUploadConfiguration(Of LoadPicAsyncUploadConfiguration)()
        ' then populate the fields (woid and userid)
        Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
        config.UserID = puser.ProviderUserKey
        config.WorkOrderID = New Guid(woid)
        ' finally set this configThe upload configuration will be available in the handler
        RadAsyncUpload1.UploadConfiguration = config

    End Sub

    Protected Sub RadAsyncUpload1_FileUploaded(ByVal sender As Object, ByVal e As FileUploadedEventArgs) Handles RadAsyncUpload1.FileUploaded
        Dim result As LoadPicAsyncUploadResult = TryCast(e.UploadResult, LoadPicAsyncUploadResult)
        Dim dba As New DBAccess()
        'dba.CommandText = "Select LocationID FROM WorkOder WHERE ID= @WorkOrderID"
        'Dim locaID As String = dba.ExecuteScalar().ToString

    End Sub

    Private Sub RadAjaxManager1_AjaxRequest(sender As Object, e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        If e.Argument.Contains("delete") Then
            Dim argArray As String() = Split(e.Argument, ":")
            Dim imgID As Guid = New Guid(argArray(1))
            Dim dba As New DBAccess()
            dba.CommandText = "DELETE FROM LoadImages WHERE ImageID=@ImageID"
            dba.AddParameter("@ImageID", imgID)
            dba.ExecuteNonQuery()

        Else
            Dim a As String = "  "
        End If



    End Sub

    Private Sub RadListView1_ItemCommand(sender As Object, e As Telerik.Web.UI.RadListViewCommandEventArgs) Handles RadListView1.ItemCommand
        If e.CommandName = RadListView.DeleteCommandName Then
            Dim imgid As String = e.CommandArgument.ToString
            Dim dba As New DBAccess
            dba.CommandText = "DELETE FROM LoadImages WHERE ImageID=@imageID"
            dba.AddParameter("@imageID", imgid)
            Try
                dba.ExecuteNonQuery()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub RadListView1_NeedDataSource(sender As Object, e As Telerik.Web.UI.RadListViewNeedDataSourceEventArgs) Handles RadListView1.NeedDataSource
        Dim woid As String = Request.QueryString("woid").Trim.ToString
        If Utilities.IsValidGuid(woid) Then

            Dim dba As New DBAccess
            dba.CommandText = "SELECT UP.FirstName + ' ' + UP.LastName AS Uploader, LI.ImageID, LI.ImageName, LI.ImageData " & _
                "FROM LoadImages LI INNER JOIN " & _
                "UserProfile UP ON LI.UserID = UP.userID " & _
                "WHERE LI.WorkOrderID=@woid "
            dba.AddParameter("@woid", woid)
            Dim dt As DataTable = New DataTable
            dt = dba.ExecuteDataSet.Tables(0)
            If dt.Rows.Count > 0 Then
                RadListView1.Visible = True
                lblExistingPictures.Text = "Current list of pictures on file:"
                btnPrint.OnClientClick = "openPDFwin('" & woid & "');return false;"
                btnPrint.Visible = True
                'looky this comment, it's locked in vs
                RadListView1.DataSource = dt
                '            lblExistingPictures.Text = "Existing Pictures"
            Else
                RadListView1.Visible = False
                btnPrint.Visible = False
                lblExistingPictures.Text = "No pictures on file"
            End If
        End If
    End Sub
End Class