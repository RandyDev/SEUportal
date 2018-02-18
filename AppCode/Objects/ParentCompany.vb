Public Class ParentCompany

#Region "Private Variables"
Private _Name As String
Private _ID As Guid
#End Region

#Region "Public Properties"
Public Property Name() As String
    Get
        Return _Name
    End Get
    Set(ByVal value As String)
        _Name = value
    End Set
End Property

Public Property ID() As Guid
    Get
        Return _ID
    End Get
    Set(ByVal value As Guid)
        _ID = value
    End Set
End Property
#End Region



End Class
