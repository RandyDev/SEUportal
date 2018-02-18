Public Class FreightIssueImage
#Region "Private Variables"
    Private _ImageData As Byte
    Private _ImageName As String
    Private _UserID As Guid
    Private _ImageID As Guid
#End Region

#Region "Public Properties"
    Public Property ImageData() As Byte
        Get
            Return _ImageData
        End Get
        Set(ByVal value As Byte)
            _ImageData = value
        End Set
    End Property

    Public Property ImageName() As String
        Get
            Return _ImageName
        End Get
        Set(ByVal value As String)
            _ImageName = value
        End Set
    End Property

    Public Property UserID() As Guid
        Get
            Return _UserID
        End Get
        Set(ByVal value As Guid)
            _UserID = value
        End Set
    End Property

    Public Property ImageID() As Guid
        Get
            Return _ImageID
        End Get
        Set(ByVal value As Guid)
            _ImageID = value
        End Set
    End Property



#End Region


End Class
