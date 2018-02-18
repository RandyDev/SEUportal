
Public Class DuplicateReceipt
#Region "Private Variables"
    Private _woID As Guid
    Private _LogDate As DateTime
    Private _DoorNumber As String
    Private _Location As String
    Private _Department As String
    Private _ReceiptNumber As String
    Private _PurchaseOrder As String
    Private _Carrier As String
    Private _TruckNumber As String
    Private _TrailerNumber As String
    Private _BadPallets As Integer
    Private _CompTime As DateTime
    Private _Amount As Decimal
    Private _Restacks As Integer
    Private _LoadType As String
    Private _LoadDescription As String
    Private _CheckNumber As String
    Private _PaymentType As String
    Private _Comments As String
#End Region

#Region "Public Properties"
    Public Property woID() As Guid
        Get
            Return _woID
        End Get
        Set(ByVal value As Guid)
            _woID = value
        End Set
    End Property

    Public Property LogDate() As DateTime
        Get
            Return _LogDate
        End Get
        Set(ByVal value As DateTime)
            _LogDate = value
        End Set
    End Property

    Public Property DoorNumber() As String
        Get
            Return _DoorNumber
        End Get
        Set(ByVal value As String)
            _DoorNumber = value
        End Set
    End Property

    Public Property Location() As String
        Get
            Return _Location
        End Get
        Set(ByVal value As String)
            _Location = value
        End Set
    End Property

    Public Property Department() As String
        Get
            Return _Department
        End Get
        Set(ByVal value As String)
            _Department = value
        End Set
    End Property

    Public Property ReceiptNumber() As String
        Get
            Return _ReceiptNumber
        End Get
        Set(ByVal value As String)
            _ReceiptNumber = value
        End Set
    End Property

    Public Property PurchaseOrder() As String
        Get
            Return _PurchaseOrder
        End Get
        Set(ByVal value As String)
            _PurchaseOrder = value
        End Set
    End Property

    Public Property Carrier() As String
        Get
            Return _Carrier
        End Get
        Set(ByVal value As String)
            _Carrier = value
        End Set
    End Property

    Public Property TruckNumber() As String
        Get
            Return _TruckNumber
        End Get
        Set(ByVal value As String)
            _TruckNumber = value
        End Set
    End Property

    Public Property TrailerNumber() As String
        Get
            Return _TrailerNumber
        End Get
        Set(ByVal value As String)
            _TrailerNumber = value
        End Set
    End Property

    Public Property BadPallets() As Integer
        Get
            Return _BadPallets
        End Get
        Set(ByVal value As Integer)
            _BadPallets = value
        End Set
    End Property

    Public Property CompTime() As DateTime
        Get
            Return _CompTime
        End Get
        Set(ByVal value As DateTime)
            _CompTime = value
        End Set
    End Property

    Public Property Amount() As Decimal
        Get
            Return _Amount
        End Get
        Set(ByVal value As Decimal)
            _Amount = value
        End Set
    End Property

    Public Property Restacks() As Integer
        Get
            Return _Restacks
        End Get
        Set(ByVal value As Integer)
            _Restacks = value
        End Set
    End Property

    Public Property LoadDescription() As String
        Get
            Return _LoadDescription
        End Get
        Set(ByVal value As String)
            _LoadDescription = value
        End Set
    End Property

    Public Property PaymentType() As String
        Get
            Return _PaymentType
        End Get
        Set(ByVal value As String)
            _PaymentType = value
        End Set
    End Property

    Public Property CheckNumber() As String
        Get
            Return _CheckNumber
        End Get
        Set(ByVal value As String)
            _CheckNumber = value
        End Set
    End Property

    Public Property LoadType() As String
        Get
            Return _LoadType
        End Get
        Set(ByVal value As String)
            _LoadType = value
        End Set
    End Property

    Public Property Comments() As String
        Get
            Return _Comments
        End Get
        Set(ByVal value As String)
            _Comments = value
        End Set
    End Property

#End Region

End Class
