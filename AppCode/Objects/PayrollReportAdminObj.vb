Public Class PayrollReportAdminObj
    Private _ID As Guid
    Private _empName As String
    Private _EmpNumber As String
    Private _PayRateHourly As Decimal
    Private _PayRatePercentage As Decimal
    Private _ulAmount As Decimal
    Private _PercentHours As Decimal
    Private _AddCompAmount As Decimal
    Private _HourlyHours As Decimal
    Private _OTHours As Decimal
    Private _RegularPay As Decimal
    Private _TotalHours As Decimal
    Private _HalfTimePay As Decimal
    Private _GrossPay As Decimal

    Public Property ID() As Guid
        Get
            Return _ID
        End Get
        Set(ByVal value As Guid)
            _ID = value
        End Set
    End Property

Public Property empName() as String
        Get
            Return _empName
        End Get
        Set(ByVal value As String)
            _empName = value
        End Set
    End Property

    Public Property EmpNumber() As String
        Get
            Return _EmpNumber
        End Get
        Set(ByVal value As String)
            _EmpNumber = value
        End Set
    End Property

    Public Property PayRateHourly() As Decimal
        Get
            Return _PayRateHourly
        End Get
        Set(ByVal value As Decimal)
            _PayRateHourly = value
        End Set
    End Property

    Public Property PayRatePercentage() As Decimal
        Get
            Return _PayRatePercentage
        End Get
        Set(ByVal value As Decimal)
            _PayRatePercentage = value
        End Set
    End Property

    Public Property ulAmount() As Decimal
        Get
            Return _ulAmount
        End Get
        Set(ByVal value As Decimal)
            _ulAmount = value
        End Set
    End Property

Public Property PercentHours() as Decimal
        Get
            Return _PercentHours
        End Get
        Set(ByVal value As Decimal)
            _PercentHours = value
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

    Public Property HourlyHours() As Decimal
        Get
            Return _HourlyHours
        End Get
        Set(ByVal value As Decimal)
            _HourlyHours = value
        End Set
    End Property

    Public Property OTHours() As Decimal
        Get
            Return _OTHours
        End Get
        Set(ByVal value As Decimal)
            _OTHours = value
        End Set
    End Property

    Public Property RegularPay() As Decimal
        Get
            Return _RegularPay
        End Get
        Set(ByVal value As Decimal)
            _RegularPay = value
        End Set
    End Property

    Public Property TotalHours() As Decimal
        Get
            Return _TotalHours
        End Get
        Set(ByVal value As Decimal)
            _TotalHours = value
        End Set
    End Property

    Public Property HalfTimePay() As Decimal
        Get
            Return _HalfTimePay
        End Get
        Set(ByVal value As Decimal)
            _HalfTimePay = value
        End Set
    End Property
Public Property GrossPay() as Decimal
        Get
            Return _GrossPay
        End Get
        Set(ByVal value As Decimal)
            _GrossPay = value
        End Set
    End Property

End Class
