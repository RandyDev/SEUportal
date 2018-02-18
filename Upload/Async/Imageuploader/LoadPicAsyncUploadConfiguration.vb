Imports System
Imports Telerik.Web.UI

' The upload configuration object is passed from the page to the custom handler.
' You can customize it to include custom properties by extending the AsyncUploadConfiguration class.
' In this case we send the ID of the currently logged-in user to be stored in the database as the author of the image.
Public Class LoadPicAsyncUploadConfiguration
    Inherits AsyncUploadConfiguration

    Private m_userID As Guid
    Private m_workOrderID As Guid

    Public Property UserID() As Guid
        Get
            Return m_userID
        End Get

        Set(ByVal value As Guid)
            m_userID = value
        End Set
    End Property

    Public Property WorkOrderID() As Guid
        Get
            Return m_workOrderID
        End Get

        Set(ByVal value As Guid)
            m_workOrderID = value
        End Set
    End Property

End Class
