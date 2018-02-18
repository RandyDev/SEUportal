Public Class ReplicaLogEntry
    Private _ID As Guid                     'generated
    Private _SeqNO As Int64                 'auto-generated
    Private _Created As Date                'date
    Private _AutorID As Guid                ' AuthorID/EmpID  currently RTDS user id: 9e2dd366-f2ff-8064-156d-44d4d6b7cd8a
    Private _ObjectID As Guid               'ID number of entry in ...
    Private _ObjectName As String           'TableName
    Private _ObjectValue As String          '<ObjectName><field1>DATA</field1><field2>DATA</field2><field3>DATA</field3></ObjectName>
    Private _ObjectOperation As Integer     '(0 - Write , 1 - Delete)
    Private _Actual As Boolean              'is the replica still actual and was not overridden by any further operations
    Private _PotentialConflict As Boolean   '- the flag shown replica object has an overlapped changes comes from different sources and actually was not applied to recipient DB

    Public Property ID() As Guid
        Get
            Return _ID
        End Get
        Set(ByVal value As Guid)
            _ID = value
        End Set
    End Property

    Public Property SeqNO() As Int64
        Get
            Return _SeqNO
        End Get
        Set(ByVal value As Int64)
            _SeqNO = value
        End Set
    End Property

    Public Property Created() As Date
        Get
            Return _Created
        End Get
        Set(ByVal value As Date)
            _Created = value
        End Set
    End Property

    Public Property AutorID() As Guid
        Get
            Return _AutorID
        End Get
        Set(ByVal value As Guid)
            _AutorID = value
        End Set
    End Property

    Public Property ObjectID() As Guid
        Get
            Return _ObjectID
        End Get
        Set(ByVal value As Guid)
            _ObjectID = value
        End Set
    End Property

    Public Property ObjectName() As String
        Get
            Return _ObjectName
        End Get
        Set(ByVal value As String)
            _ObjectName = value
        End Set
    End Property

    Public Property ObjectValue() As String
        Get
            Return _ObjectValue
        End Get
        Set(ByVal value As String)
            _ObjectValue = value
        End Set
    End Property

    Public Property ObjectOperation() As Integer
        Get
            Return _ObjectOperation
        End Get
        Set(ByVal value As Integer)
            _ObjectOperation = value
        End Set
    End Property

    Public Property Actual() As Boolean
        Get
            Return _Actual
        End Get
        Set(ByVal value As Boolean)
            _Actual = value
        End Set
    End Property

    Public Property PotentialConflict() As Boolean
        Get
            Return _PotentialConflict
        End Get
        Set(ByVal value As Boolean)
            _PotentialConflict = value
        End Set
    End Property




End Class
