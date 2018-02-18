Public Class additionalComp

#Region "Private Variables"
    Private _AddCompID As Guid
    Private _EmployeeID As Guid
    Private _AddCompStartDate As Date
    Private _AddCompEndDate As Date
    Private _AddCompDescriptionID As Guid
    Private _AddCompAmount As Decimal
    Private _AddCompComments As String
    Private _userID As Guid
    Private _TimeStamp As Date
#End Region

#Region "Public Properties"
    Public Property AddCompID() As Guid
        Get
            Return _AddCompID
        End Get
        Set(ByVal value As Guid)
            _AddCompID = value
        End Set
    End Property

    Public Property EmployeeID() As Guid
        Get
            Return _EmployeeID
        End Get
        Set(ByVal value As Guid)
            _EmployeeID = value
        End Set
    End Property

    Public Property AddCompStartDate() As Date
        Get
            Return _AddCompStartDate
        End Get
        Set(ByVal value As Date)
            _AddCompStartDate = value
        End Set
    End Property

    Public Property AddCompEndDate() As Date
        Get
            Return _AddCompEndDate
        End Get
        Set(ByVal value As Date)
            _AddCompEndDate = value
        End Set
    End Property

    Public Property AddCompDescriptionID() As Guid
        Get
            Return _AddCompDescriptionID
        End Get
        Set(ByVal value As Guid)
            _AddCompDescriptionID = value
        End Set
    End Property

    Public Property AddCompAmount() As Decimal
        Get
            Return _AddCompAmount
        End Get
        Set(ByVal value As Decimal)
            _AddCompAmount = value
        End Set
    End Property

    Public Property AddCompComments() As String
        Get
            Return _AddCompComments
        End Get
        Set(ByVal value As String)
            _AddCompComments = value
        End Set
    End Property

    Public Property userID() As Guid
        Get
            Return _userID
        End Get
        Set(ByVal value As Guid)
            _userID = value
        End Set
    End Property

    Public Property TimeStamp() As Date
        Get
            Return _TimeStamp
        End Get
        Set(ByVal value As Date)
            _TimeStamp = value
        End Set
    End Property
#End Region

End Class
