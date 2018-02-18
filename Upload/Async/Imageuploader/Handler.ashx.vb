Imports System.Web
Imports Telerik.Web.UI
'Imports System.Data
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Data.SqlClient
Imports System.IO
'Imports DiversifiedLogistics.DBAccess


Public Class Handler
    Inherits AsyncUploadHandler
    Implements System.Web.SessionState.IRequiresSessionState

    Protected Overrides Function Process(ByVal file As UploadedFile, ByVal context As HttpContext, ByVal configuration As IAsyncUploadConfiguration, ByVal tempFileName As String) As IAsyncUploadResult
        ' Call the base Process method to save the file to the temporary folder
        ' base.Process(file, context, configuration, tempFileName);

        ' Populate the default (base) result into an object of type LoadPicAsyncUploadResult
        Dim result As LoadPicAsyncUploadResult = CreateDefaultUploadResult(Of LoadPicAsyncUploadResult)(file)

        Dim userID As Guid = Nothing
        Dim workOrderID As Guid = Nothing
        ' You can obtain any custom information passed from the page via casting the configuration parameter to your custom class
        Dim asyncConfiguration As LoadPicAsyncUploadConfiguration = TryCast(configuration, LoadPicAsyncUploadConfiguration)
        If asyncConfiguration IsNot Nothing Then
            userID = asyncConfiguration.UserID
            workOrderID = asyncConfiguration.WorkOrderID

        End If

        ' Populate any additional fields into the upload result.
        ' The upload result is available both on the client and on the server
        result.ImageID = InsertImage(file, userID, workOrderID)

        Return result
    End Function

    Public Function InsertImage(ByVal file As UploadedFile, ByVal userID As Guid, ByVal workOrderID As Guid) As Guid
        Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("rtdsConnectionString").ConnectionString

        Using conn As New SqlConnection(connectionString)
            Dim cmdText As String = "INSERT INTO LoadImages (ImageData, ImageName, UserID, WorkOrderID, ImageID, UpLoadDate) VALUES (@ImageData, @ImageName, @UserID, @WorkOrderID, @ImageID, @UpLoadDate)"
            Dim cmd As New SqlCommand(cmdText, conn)
            Dim imageData As Byte() = GetImageBytes(file.InputStream)
            Dim imgID As Guid = Guid.NewGuid()
            'Dim identityParam As New SqlParameter("@Identity", SqlDbType.Int, 0, "ImageID")
            'identityParam.Direction = ParameterDirection.Output
            cmd.Parameters.AddWithValue("@ImageData", imageData)
            cmd.Parameters.AddWithValue("@ImageName", file.GetName())
            cmd.Parameters.AddWithValue("@UserID", userID)
            cmd.Parameters.AddWithValue("@WorkOrderID", workOrderID)
            cmd.Parameters.AddWithValue("@ImageID", imgID)
            cmd.Parameters.AddWithValue("@UpLoadDate", Date.Now)
            '            cmd.Parameters.Add(identityParam)

            conn.Open()
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Dim er As String = ex.Message
            Finally
                conn.Close()
            End Try
            Dim dba As New DBAccess

            dba.CommandText = "Select locationid from workorder where id=@workorderid"
            dba.AddParameter("@workorderid", workOrderID)
            Dim locaid As String = dba.ExecuteScalar().ToString
            dba.CommandText = "Select hasPics from Location where id=@locaid"
            dba.AddParameter("@locaid", locaid)
            Dim haspics As Boolean = dba.ExecuteScalar
            If Not haspics Then
                dba.CommandText = "UPDATE Location SET hasPics=1 WHERE id=@locaid"
                dba.AddParameter("@locaid", locaid)
                dba.ExecuteNonQuery()
            End If


            Return imgID
            '           Return CInt(identityParam.Value)
        End Using
    End Function

    Public Function GetImageBytes(ByVal stream As Stream) As Byte()
        Dim buffer As Byte()

        Using image As Bitmap = ResizeImage(stream)
            Using ms As New MemoryStream()
                image.Save(ms, ImageFormat.Jpeg)

                'return the current position in the stream at the beginning
                ms.Position = 0

                buffer = New Byte(ms.Length - 1) {}
                ms.Read(buffer, 0, CInt(ms.Length))
                Return buffer
            End Using
        End Using
    End Function


    Public Function ResizeImage(ByVal stream As Stream) As Bitmap
        Dim originalImage As Image = Bitmap.FromStream(stream)

        Dim height As Integer = 384
        Dim width As Integer = 512

        Dim ratio As Double = Math.Min(originalImage.Width, originalImage.Height) / CDbl(Math.Max(originalImage.Width, originalImage.Height))

        If originalImage.Width > originalImage.Height Then
            height = Convert.ToInt32(height * ratio)
        Else
            width = Convert.ToInt32(width * ratio)
        End If

        Dim scaledImage As New Bitmap(width, height)

        Using g As Graphics = Graphics.FromImage(scaledImage)
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
            g.DrawImage(originalImage, 0, 0, width, height)
            Return scaledImage
        End Using

    End Function

End Class