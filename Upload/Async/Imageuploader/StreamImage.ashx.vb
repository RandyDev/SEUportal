Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Drawing.Imaging

Public Class StreamImage1
    Implements System.Web.IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim id As Guid = New Guid(context.Request.QueryString("imageID").ToString)
        Dim rs As Double = 1
        Dim isGuid As Boolean = Utilities.IsValidGuid(context.Request.QueryString("imageID"))
        Dim scale As Double = Double.TryParse(context.Request.QueryString("scale"), rs)
        If Not isGuid Then
            context.Response.[End]()
        End If
        Dim imageData As Byte() = GetImage(rs, id)
        context.Response.ContentType = "image/jpeg"
        context.Response.BinaryWrite(imageData)
        context.Response.Flush()
    End Sub

    Private Function GetImage(ByVal scale As Double, ByVal id As Guid) As Byte()
        Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("rtdsConnectionString").ConnectionString
        Using conn As New SqlConnection(connectionString)
            Dim cmdText As String = "SELECT ImageData FROM LoadImages WHERE ImageID = @ImageID;"
            Dim cmd As New SqlCommand(cmdText, conn)
            Dim idParam As New SqlParameter("@ImageID", SqlDbType.UniqueIdentifier)
            idParam.Value = id
            cmd.Parameters.Add(idParam)
            conn.Open()
            Using reader As SqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    Dim dbImageByteArray As Byte() = DirectCast(reader("ImageData"), Byte())
                    '                    Return dbImageByteArray
                    Return ResizeImage(scale, dbImageByteArray)
                Else
                    Throw New ArgumentException("Invalid ID")
                End If
            End Using
        End Using
    End Function
    'byte() to bitmap
    Public Function ResizeImage(ByVal scaleFactor As Double, ByVal dbImageByteArray As Byte()) As Byte()

        '        Dim image As Image = image.FromStream(stream)
        ' byte() to Stream
        Dim memoryStream As MemoryStream = New MemoryStream(dbImageByteArray, False)
        'stream to image
        Dim imgFromDB As Image = Image.FromStream(memoryStream)

        Dim newWidth As Integer = (imgFromDB.Width * scaleFactor)
        Dim newHeight As Integer = (imgFromDB.Height * scaleFactor)

        Dim abort As New Image.GetThumbnailImageAbort(AddressOf ThumbnailCallback)
        Dim thumb As Image = imgFromDB.GetThumbnailImage(newWidth, newHeight, abort, IntPtr.Zero)
        Dim ms As MemoryStream = New MemoryStream

        thumb.Save(ms, imgFromDB.RawFormat)
        thumb.Dispose()
        Return GetImageBytes(ms)
        '        Return dbImageByteArray ' btmap
    End Function

    Public Function GetImageBytes(ByVal stream As Stream) As Byte()
        Dim buffer As Byte()
        Dim imgFromDB As Image = Image.FromStream(stream)
        Using image As Bitmap = imgFromDB
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

    'Function GetImageClip(ByVal btmap As Bitmap) As Stream
    '    Dim result As MemoryStream = New MemoryStream
    '    Dim original As Bitmap = btmap
    '    Dim newimage As Bitmap = New Bitmap(160, 120)
    '    Dim g As Graphics = Graphics.FromImage(newimage)
    '    g.DrawImage(original, 0, 0, 160, 120)
    '    g.Dispose()
    '    original.Dispose()
    '    newimage.Save(result, System.Drawing.Imaging.ImageFormat.Jpeg)
    '    Return result
    'End Function



    Public Function ThumbnailCallback() As Boolean
        Return False
    End Function

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property


End Class