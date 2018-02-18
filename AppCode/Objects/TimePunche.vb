Public Class TimePunche
    Private _EmployeeID As Guid
    Private _DepartmentID As Guid
    Private _DepartmentName As String
    Private _LocationID As Guid
    Private _LocationName As String
    Private _DateWorked As DateTime
    Private _IsClosed As Boolean
    Private _ID As Guid
    Private _tpList As List(Of TimeInOut)

#Region "Public Properties"
    Public Property EmployeeID() As Guid
        Get
            Return _EmployeeID
        End Get
        Set(ByVal value As Guid)
            _EmployeeID = value
        End Set
    End Property

    Public Property DepartmentID() As Guid
        Get
            Return _DepartmentID
        End Get
        Set(ByVal value As Guid)
            _DepartmentID = value
        End Set
    End Property

    Public Property DepartmentName() As String
        Get
            Return _DepartmentName
        End Get
        Set(ByVal value As String)
            _DepartmentName = value
        End Set
    End Property
    Public Property LocationID() As Guid
        Get
            Return _LocationID
        End Get
        Set(ByVal value As Guid)
            _LocationID = value
        End Set
    End Property

    Public Property LocationName() As String
        Get
            Return _LocationName
        End Get
        Set(ByVal value As String)
            _LocationName = value
        End Set
    End Property

    Public Property DateWorked() As DateTime
        Get
            Return _DateWorked
        End Get
        Set(ByVal value As DateTime)
            _DateWorked = value
        End Set
    End Property

    Public Property IsClosed() As Boolean
        Get
            Return _IsClosed
        End Get
        Set(ByVal value As Boolean)
            _IsClosed = value
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

    Public Property tpList As List(Of TimeInOut)
        Get
            Return _tpList
        End Get
        Set(ByVal value As List(Of TimeInOut))
            _tpList = value
        End Set
    End Property
#End Region

End Class


Public Class TimeInOut
    Private _TimepuncheID As Guid
    Private _TimeIn As DateTime
    Private _TimeOut As DateTime
    Private _JobDescriptionID As Guid
    Private _HoursWorked As Decimal
    Private _Hours As String
    Private _isHourly As Boolean
    Private _ID As Guid

#Region "Public Properties"
    Public Property TimepuncheID() As Guid
        Get
            Return _TimepuncheID
        End Get
        Set(ByVal value As Guid)
            _TimepuncheID = value
        End Set
    End Property

    Public Property TimeIn() As DateTime
        Get
            Return _TimeIn
        End Get
        Set(ByVal value As DateTime)
            _TimeIn = value
        End Set
    End Property

    Public Property TimeOut() As DateTime
        Get
            Return _TimeOut
        End Get
        Set(ByVal value As DateTime)
            _TimeOut = value
        End Set
    End Property

    Public Property HoursWorked() As Decimal
        Get
            Return _HoursWorked
        End Get
        Set(ByVal value As Decimal)
            _HoursWorked = value
        End Set
    End Property

    Public Property Hours() As String
        Get
            Return _Hours
        End Get
        Set(ByVal value As String)
            _Hours = value
        End Set
    End Property

    Public Property isHourly() As Boolean
        Get
            Return _isHourly
        End Get
        Set(ByVal value As Boolean)
            _isHourly = value
        End Set
    End Property

    Public Property JobDescriptionID() As Guid
        Get
            Return _JobDescriptionID
        End Get
        Set(ByVal value As Guid)
            _JobDescriptionID = value
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
