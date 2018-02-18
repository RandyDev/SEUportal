Public Class importConfigDataItem
    Private _ConfigID As Guid
    Private _FieldName As String
    Private _canUpdate As Boolean
    Private _columnLetter As String
    Private _DefaultValue As String

#Region "Public properties"

    Public Property ConfigID() As Guid
        Get
            Return _ConfigID
        End Get
        Set(ByVal value As Guid)
            _ConfigID = value
        End Set
    End Property

    Public Property canUpdate() As Boolean
        Get
            Return _canUpdate
        End Get
        Set(ByVal value As Boolean)
            _canUpdate = value
        End Set
    End Property

    Public Property FieldName() As String
        Get
            Return _FieldName
        End Get
        Set(ByVal value As String)
            _FieldName = value
        End Set
    End Property

    Public Property columnLetter() As String
        Get
            Return _columnLetter
        End Get
        Set(ByVal value As String)
            _columnLetter = value
        End Set
    End Property

    Public Property DefaultValue() As String
        Get
            Return _DefaultValue
        End Get
        Set(ByVal value As String)
            _DefaultValue = value
        End Set
    End Property

#End Region


End Class

