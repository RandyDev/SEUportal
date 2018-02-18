Public Class SvcsRenderedobj
    Dim ldal As New locaDAL
#Region "Private variables"

    Private _ID As Guid
    Private _jobDescriptionID As Guid
    Private _jobDescription As String
    Private _locationID As Guid
    Private _location As String
    Private _jobDate As String
    Private _employeeID As Guid
    Private _employeeName As String
    Private _amount As Decimal
    Private _CreatedByID As Guid
    Private _CreatedBy As String
    Private _CreatedIP As String
    Private _timeStamp As Date
    Private _isCurrent As Boolean
#End Region


#Region "Public properties"
    Public Property ID() As Guid
        Get
            Return _ID
        End Get
        Set(ByVal value As Guid)
            _ID = value
        End Set
    End Property

    Public Property jobDescriptionID() As Guid
        Get
            Return _jobDescriptionID
        End Get
        Set(ByVal value As Guid)
            _jobDescriptionID = value
        End Set
    End Property

    Public Property jobDescription() As String
        Get
            Return _jobDescription
        End Get
        Set(ByVal value As String)
            _jobDescription = value
        End Set
    End Property

    Public Property locationID() As Guid
        Get
            Return _locationID
        End Get
        Set(ByVal value As Guid)
            _locationID = value
        End Set
    End Property

    Public Property location() As String
        Get
            Return _location
        End Get
        Set(ByVal value As String)
            _location = value
        End Set
    End Property

    Public Property jobDate() As String
        Get
            Return _jobDate
        End Get
        Set(ByVal value As String)
            _jobDate = value
        End Set
    End Property

    Public Property employeeID() As Guid
        Get
            Return _employeeID
        End Get
        Set(ByVal value As Guid)
            _employeeID = value
            Dim edal As New empDAL
        End Set
    End Property

    Public Property employeeName() As String
        Get
            Return _employeeName
        End Get
        Set(ByVal value As String)
            _employeeName = value
        End Set
    End Property

    Public Property amount() As Decimal
        Get
            Return _amount
        End Get
        Set(ByVal value As Decimal)
            _amount = value
        End Set
    End Property

    Public Property CreatedByID() As Guid
        Get
            Return _CreatedByID
        End Get
        Set(ByVal value As Guid)
            _CreatedByID = value
            Dim udal As New userDAL
        End Set
    End Property

    Public Property CreatedBy() As String
        Get
            Return _CreatedBy
        End Get
        Set(ByVal value As String)
            _CreatedBy = value
        End Set
    End Property

    Public Property CreatedIP() As String
        Get
            Return _CreatedIP
        End Get
        Set(ByVal value As String)
            _CreatedIP = value
        End Set
    End Property

    Public Property timeStamp() As Date
        Get
            Return _timeStamp
        End Get
        Set(ByVal value As Date)
            _timeStamp = value
        End Set
    End Property

    Public Property isCurrent() As Boolean
        Get
            Return _isCurrent
        End Get
        Set(ByVal value As Boolean)
            _isCurrent = value
        End Set
    End Property

#End Region


End Class
