Public Class DivLogEmployee

    Private _dleID As Guid
    Private _FirstName As String
    Private _LastName As String
    Private _Race As String
    Private _Gender As String
    Private _DOB As Date
    Private _ssn As String
    Private _Address As DivLogEmployeeAddress
    Private _Employment As DivLogEmployeeEmployment
    Private _Health As DivLogEmployeeHealth

    Public Property dleID() As Guid
        Get
            Return _dleID
        End Get
        Set(ByVal value As Guid)
            _dleID = value
        End Set
    End Property

    Public Property FirstName() As String
        Get
            Return _FirstName
        End Get
        Set(ByVal value As String)
            _FirstName = value
        End Set
    End Property

    Public Property LastName() As String
        Get
            Return _LastName
        End Get
        Set(ByVal value As String)
            _LastName = value
        End Set
    End Property

    Public Property Race() As String
        Get
            Return _Race
        End Get
        Set(ByVal value As String)
            _Race = value
        End Set
    End Property

    Public Property Gender() As String
        Get
            Return _Gender
        End Get
        Set(ByVal value As String)
            _Gender = value
        End Set
    End Property

    Public Property DOB() As Date
        Get
            Return _DOB
        End Get
        Set(ByVal value As Date)
            _DOB = value
        End Set
    End Property

    Public Property ssn() As String
        Get
            Return _ssn
        End Get
        Set(ByVal value As String)
            _ssn = value
        End Set
    End Property

    Public Property Address() As DivLogEmployeeAddress
        Get
            Return _Address
        End Get
        Set(ByVal value As DivLogEmployeeAddress)
            _Address = value
        End Set
    End Property

    Public Property Employment() As DivLogEmployeeEmployment
        Get
            Return _Employment
        End Get
        Set(ByVal value As DivLogEmployeeEmployment)
            _Employment = value
        End Set
    End Property

    Public Property Health() As DivLogEmployeeHealth
        Get
            Return _Health
        End Get
        Set(ByVal value As DivLogEmployeeHealth)
            _Health = value
        End Set
    End Property


End Class
Public Class DivLogEmployeeAddress
    Private _Address As String
    Private _City As String
    Private _State As String
    Public Property Address() As String
        Get
            Return _Address
        End Get
        Set(ByVal value As String)
            _Address = value
        End Set
    End Property

    Public Property City() As String
        Get
            Return _City
        End Get
        Set(ByVal value As String)
            _City = value
        End Set
    End Property

    Public Property State() As String
        Get
            Return _State
        End Get
        Set(ByVal value As String)
            _State = value
        End Set
    End Property


End Class
Public Class DivLogEmployeeEmployment
    Private _Position As String
    Private _DateOfDismiss As Date
    Public Property Position() As String
        Get
            Return _Position
        End Get
        Set(ByVal value As String)
            _Position = value
        End Set
    End Property

    Public Property DateOfDismiss() As Date
        Get
            Return _DateOfDismiss
        End Get
        Set(ByVal value As Date)
            _DateOfDismiss = value
        End Set
    End Property


End Class
Public Class DivLogEmployeeHealth
    Private _InsCompanyName As String
    Private _somethingelse As String
    Public Property InsCompanyName() As String
        Get
            Return _InsCompanyName
        End Get
        Set(ByVal value As String)
            _InsCompanyName = value
        End Set
    End Property

    Public Property somethingelse() As String
        Get
            Return _somethingelse
        End Get
        Set(ByVal value As String)
            _somethingelse = value
        End Set
    End Property


End Class
