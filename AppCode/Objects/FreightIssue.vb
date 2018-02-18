Public Class FreightIssue
#Region "Private Variables"

    Private _woid As Guid
    Private _locaid As Guid
    Private _LogDate As Date
    Private _LoadNumber As String
    Private _Location As String
    Private _Department As String
    Private _email As String
    Private _emailCC As String
    Private _PurchaseOrder As String
    Private _Carrier As String
    Private _TrailerNumber As String
    Private _VendorName As String
    Private _VendorNumber As String
    Private _BadPallets As Integer
    Private _Restacks As Integer
    Private _Comments As String
    Private _Images As List(Of FreightIssueImage)
#End Region

#Region "Public Properties"

    Public Property woid() As Guid
        Get
            Return _woid
        End Get
        Set(ByVal value As Guid)
            _woid = value
        End Set
    End Property

    Public Property locaid() As Guid
        Get
            Return _locaid
        End Get
        Set(ByVal value As Guid)
            _locaid = value
        End Set
    End Property

    Public Property LogDate() As Date
        Get
            Return _LogDate
        End Get
        Set(ByVal value As Date)
            _LogDate = value
        End Set
    End Property

    Public Property LoadNumber() As String
        Get
            Return _LoadNumber
        End Get
        Set(ByVal value As String)
            _LoadNumber = value
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

    Public Property email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property

    Public Property emailCC() As String
        Get
            Return _emailCC
        End Get
        Set(ByVal value As String)
            _emailCC = value
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

    Public Property TrailerNumber() As String
        Get
            Return _TrailerNumber
        End Get
        Set(ByVal value As String)
            _TrailerNumber = value
        End Set
    End Property

    Public Property VendorName() As String
        Get
            Return _VendorName
        End Get
        Set(ByVal value As String)
            _VendorName = value
        End Set
    End Property

    Public Property VendorNumber() As String
        Get
            Return _VendorNumber
        End Get
        Set(ByVal value As String)
            _VendorNumber = value
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

    Public Property Restacks() As Integer
        Get
            Return _Restacks
        End Get
        Set(ByVal value As Integer)
            _Restacks = value
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

    Public Property Images() As List(Of FreightIssueImage)
        Get
            Return _Images
        End Get
        Set(ByVal value As List(Of FreightIssueImage))
            _Images = value
        End Set
    End Property

#End Region
End Class
