Imports System
Imports Telerik.Web.UI

' The result object is returned from the handler to the page.
' You can include custom fields in the result by extending the AsyncUploadResult class.
' In this case we return the ID of the image record.
Public Class LoadPicAsyncUploadResult
    Inherits AsyncUploadResult

    Private m_imageID As Guid

    Public Property ImageID() As Guid
        Get
            Return m_imageID
        End Get
        Set(ByVal value As Guid)
            m_imageID = value
        End Set
    End Property
End Class
