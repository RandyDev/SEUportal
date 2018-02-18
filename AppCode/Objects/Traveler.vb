Public Class Traveler

    Private _travelID As Guid
    Private _rtdsEmployeeID As Guid
    Private _homeLocation As Guid
    Private _travelLocation As Guid
    Private _startDate As DateTime
    Private _returnDate As DateTime
    Private _loadMoney As Boolean
    Private _salaryWeek As Decimal
    Private _perDiemWeek As Decimal
    Private _EmployeeName As String
    Private _homeLocaName As String
    Private _travelLocaName As String

    Public Property travelID() As Guid
        Get
            Return _travelID
        End Get
        Set(ByVal value As Guid)
            _travelID = value
        End Set
    End Property

    Public Property rtdsEmployeeID() As Guid
        Get
            Return _rtdsEmployeeID
        End Get
        Set(ByVal value As Guid)
            _rtdsEmployeeID = value
        End Set
    End Property

    Public Property homeLocation() As Guid
        Get
            Return _homeLocation
        End Get
        Set(ByVal value As Guid)
            _homeLocation = value
        End Set
    End Property

    Public Property travelLocation() As Guid
        Get
            Return _travelLocation
        End Get
        Set(ByVal value As Guid)
            _travelLocation = value
        End Set
    End Property

    Public Property startDate() As DateTime
        Get
            Return _startDate
        End Get
        Set(ByVal value As DateTime)
            _startDate = value
        End Set
    End Property

    Public Property returnDate() As DateTime
        Get
            Return _returnDate
        End Get
        Set(ByVal value As DateTime)
            _returnDate = value
        End Set
    End Property

    Public Property loadMoney() As Boolean
        Get
            Return _loadMoney
        End Get
        Set(ByVal value As Boolean)
            _loadMoney = value
        End Set
    End Property

    Public Property salaryWeek() As Decimal
        Get
            Return _salaryWeek
        End Get
        Set(ByVal value As Decimal)
            _salaryWeek = value
        End Set
    End Property

    Public Property perDiemWeek() As Decimal
        Get
            Return _perDiemWeek
        End Get
        Set(ByVal value As Decimal)
            _perDiemWeek = value
        End Set
    End Property

    Public Property EmployeeName() As String
        Get
            Return _EmployeeName
        End Get
        Set(ByVal value As String)
            _EmployeeName = value
        End Set
    End Property

    Public Property homeLocaName() As String
        Get
            Return _homeLocaName
        End Get
        Set(ByVal value As String)
            _homeLocaName = value
        End Set
    End Property

    Public Property travelLocaName() As String
        Get
            Return _travelLocaName
        End Get
        Set(ByVal value As String)
            _travelLocaName = value
        End Set
    End Property


End Class
