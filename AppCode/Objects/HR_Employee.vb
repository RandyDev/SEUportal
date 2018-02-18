

Public Class HR_Employee

    Private _EmpTableID As Integer
    Private _EmployeeLastName As String
    Private _EmployeeFirstName As String
    Private _EmployeeNickName As String
    Private _EmploeeRace As String
    Private _EmployeeGender As String
    Private _EmployeeDOB As String
    Private _EmployeeSSnumber As String
    Private _EmployeeNumber As String
    Private _EmployeePhone As String
    Private _EmployeeCell As String
    Private _EmployeeAddress As String
    Private _EmployeeCity As String
    Private _EmployeeState As String
    Private _EmployeeZip As String
    Private _EmpNotificationName As String
    Private _EmpNotificationNumber As String
    Private _EmpNotificationAddress As String
    Private _EmployeeLocation As Integer
    Private _EmployeeSiteCompany As Integer
    Private _EmployeeJobTitle As Integer
    Private _EmployeePayType As Integer
    Private _EmployeeFullTimePartTime As Boolean
    Private _EmployeeHireDate As Date
    Private _EmployeeTerminationDate As Date
    Private _SeperationReason As String
    Private _EmployeeUser As String
    Private _EmployeePassword As String
    Private _EmplyeeSecurity As Integer
    Private _EmployeeActive As Integer
    Private _EmployeeEmail As String
    Private _LastEdited As Date
    Private _LastEditUser As String
    Private _InsHealthName As Integer
    Private _InsHealthAmount As Decimal
    Private _InsHealthCover As Integer
    Private _InsHealthComment As String
    Private _InsHealthDocument As Byte()
    Private _InsHealthDocumentName As String
    Private _InsLifeName As Integer
    Private _InsLifeAmount As Decimal
    Private _InsLifeCover As Integer
    Private _InsLifeComment As String
    Private _InsLifeDocument As Byte()
    Private _InsLifeDocumentName As String
    Private _InsDentalName As Integer
    Private _InsDentalAmount As Decimal
    Private _InsDentalCover As Integer
    Private _InsDentalComment As String
    Private _InsDentalDocument As Byte()
    Private _InsDentalDocumentName As String
    Private _InsDisabilityName As Integer
    Private _InsDisabilityAmount As Decimal
    Private _InsDisabilityCover As Integer
    Private _InsDisabilityComment As String
    Private _InsDisabilityDocument As Byte()
    Private _InsDisabilityDocumentName As String
    Private _RetName As Integer
    Private _RetAmountMoney As Decimal
    Private _RetAmountPercent As Decimal
    Private _RetComment As String
    Private _RetDocument As String
    Private _RetDocumentName As String
    Private _EmployeeJobCat As Integer

    Public Property EmpTableID() As Integer
        Get
            Return _EmpTableID
        End Get
        Set(ByVal value As Integer)
            _EmpTableID = value
        End Set
    End Property

    Public Property EmployeeLastName() As String
        Get
            Return _EmployeeLastName
        End Get
        Set(ByVal value As String)
            _EmployeeLastName = value
        End Set
    End Property

    Public Property EmployeeFirstName() As String
        Get
            Return _EmployeeFirstName
        End Get
        Set(ByVal value As String)
            _EmployeeFirstName = value
        End Set
    End Property

    Public Property EmployeeNickName() As String
        Get
            Return _EmployeeNickName
        End Get
        Set(ByVal value As String)
            _EmployeeNickName = value
        End Set
    End Property

    Public Property EmploeeRace() As String
        Get
            Return _EmploeeRace
        End Get
        Set(ByVal value As String)
            _EmploeeRace = value
        End Set
    End Property

    Public Property EmployeeGender() As String
        Get
            Return _EmployeeGender
        End Get
        Set(ByVal value As String)
            _EmployeeGender = value
        End Set
    End Property

    Public Property EmployeeDOB() As String
        Get
            Return _EmployeeDOB
        End Get
        Set(ByVal value As String)
            _EmployeeDOB = value
        End Set
    End Property

    Public Property EmployeeSSnumber() As String
        Get
            Return _EmployeeSSnumber
        End Get
        Set(ByVal value As String)
            _EmployeeSSnumber = value
        End Set
    End Property

    Public Property EmployeeNumber() As String
        Get
            Return _EmployeeNumber
        End Get
        Set(ByVal value As String)
            _EmployeeNumber = value
        End Set
    End Property

    Public Property EmployeePhone() As String
        Get
            Return _EmployeePhone
        End Get
        Set(ByVal value As String)
            _EmployeePhone = value
        End Set
    End Property

    Public Property EmployeeCell() As String
        Get
            Return _EmployeeCell
        End Get
        Set(ByVal value As String)
            _EmployeeCell = value
        End Set
    End Property

    Public Property EmployeeAddress() As String
        Get
            Return _EmployeeAddress
        End Get
        Set(ByVal value As String)
            _EmployeeAddress = value
        End Set
    End Property

    Public Property EmployeeCity() As String
        Get
            Return _EmployeeCity
        End Get
        Set(ByVal value As String)
            _EmployeeCity = value
        End Set
    End Property

    Public Property EmployeeState() As String
        Get
            Return _EmployeeState
        End Get
        Set(ByVal value As String)
            _EmployeeState = value
        End Set
    End Property

    Public Property EmployeeZip() As String
        Get
            Return _EmployeeZip
        End Get
        Set(ByVal value As String)
            _EmployeeZip = value
        End Set
    End Property

    Public Property EmpNotificationName() As String
        Get
            Return _EmpNotificationName
        End Get
        Set(ByVal value As String)
            _EmpNotificationName = value
        End Set
    End Property

    Public Property EmpNotificationNumber() As String
        Get
            Return _EmpNotificationNumber
        End Get
        Set(ByVal value As String)
            _EmpNotificationNumber = value
        End Set
    End Property

    Public Property EmpNotificationAddress() As String
        Get
            Return _EmpNotificationAddress
        End Get
        Set(ByVal value As String)
            _EmpNotificationAddress = value
        End Set
    End Property

    Public Property EmployeeLocation() As Integer
        Get
            Return _EmployeeLocation
        End Get
        Set(ByVal value As Integer)
            _EmployeeLocation = value
        End Set
    End Property

    Public Property EmployeeSiteCompany() As Integer
        Get
            Return _EmployeeSiteCompany
        End Get
        Set(ByVal value As Integer)
            _EmployeeSiteCompany = value
        End Set
    End Property

    Public Property EmployeeJobTitle() As Integer
        Get
            Return _EmployeeJobTitle
        End Get
        Set(ByVal value As Integer)
            _EmployeeJobTitle = value
        End Set
    End Property

    Public Property EmployeePayType() As Integer
        Get
            Return _EmployeePayType
        End Get
        Set(ByVal value As Integer)
            _EmployeePayType = value
        End Set
    End Property

    Public Property EmployeeFullTimePartTime() As Boolean
        Get
            Return _EmployeeFullTimePartTime
        End Get
        Set(ByVal value As Boolean)
            _EmployeeFullTimePartTime = value
        End Set
    End Property

    Public Property EmployeeHireDate() As Date
        Get
            Return _EmployeeHireDate
        End Get
        Set(ByVal value As Date)
            _EmployeeHireDate = value
        End Set
    End Property

    Public Property EmployeeTerminationDate() As Date
        Get
            Return _EmployeeTerminationDate
        End Get
        Set(ByVal value As Date)
            _EmployeeTerminationDate = value
        End Set
    End Property

    Public Property SeperationReason() As String
        Get
            Return _SeperationReason
        End Get
        Set(ByVal value As String)
            _SeperationReason = value
        End Set
    End Property

    Public Property EmployeeUser() As String
        Get
            Return _EmployeeUser
        End Get
        Set(ByVal value As String)
            _EmployeeUser = value
        End Set
    End Property

    Public Property EmployeePassword() As String
        Get
            Return _EmployeePassword
        End Get
        Set(ByVal value As String)
            _EmployeePassword = value
        End Set
    End Property

    Public Property EmplyeeSecurity() As Integer
        Get
            Return _EmplyeeSecurity
        End Get
        Set(ByVal value As Integer)
            _EmplyeeSecurity = value
        End Set
    End Property

    Public Property EmployeeActive() As Integer
        Get
            Return _EmployeeActive
        End Get
        Set(ByVal value As Integer)
            _EmployeeActive = value
        End Set
    End Property

    Public Property EmployeeEmail() As String
        Get
            Return _EmployeeEmail
        End Get
        Set(ByVal value As String)
            _EmployeeEmail = value
        End Set
    End Property

    Public Property LastEdited() As Date
        Get
            Return _LastEdited
        End Get
        Set(ByVal value As Date)
            _LastEdited = value
        End Set
    End Property

    Public Property LastEditUser() As String
        Get
            Return _LastEditUser
        End Get
        Set(ByVal value As String)
            _LastEditUser = value
        End Set
    End Property

    Public Property InsHealthName() As Integer
        Get
            Return _InsHealthName
        End Get
        Set(ByVal value As Integer)
            _InsHealthName = value
        End Set
    End Property

    Public Property InsHealthAmount() As Decimal
        Get
            Return _InsHealthAmount
        End Get
        Set(ByVal value As Decimal)
            _InsHealthAmount = value
        End Set
    End Property

    Public Property InsHealthCover() As Integer
        Get
            Return _InsHealthCover
        End Get
        Set(ByVal value As Integer)
            _InsHealthCover = value
        End Set
    End Property

    Public Property InsHealthComment() As String
        Get
            Return _InsHealthComment
        End Get
        Set(ByVal value As String)
            _InsHealthComment = value
        End Set
    End Property

    Public Property InsHealthDocument() As Byte()
        Get
            Return _InsHealthDocument
        End Get
        Set(ByVal value As Byte())
            _InsHealthDocument = value
        End Set
    End Property

    Public Property InsHealthDocumentName() As String
        Get
            Return _InsHealthDocumentName
        End Get
        Set(ByVal value As String)
            _InsHealthDocumentName = value
        End Set
    End Property

    Public Property InsLifeName() As Integer
        Get
            Return _InsLifeName
        End Get
        Set(ByVal value As Integer)
            _InsLifeName = value
        End Set
    End Property

    Public Property InsLifeAmount() As Decimal
        Get
            Return _InsLifeAmount
        End Get
        Set(ByVal value As Decimal)
            _InsLifeAmount = value
        End Set
    End Property

    Public Property InsLifeCover() As Integer
        Get
            Return _InsLifeCover
        End Get
        Set(ByVal value As Integer)
            _InsLifeCover = value
        End Set
    End Property

    Public Property InsLifeComment() As String
        Get
            Return _InsLifeComment
        End Get
        Set(ByVal value As String)
            _InsLifeComment = value
        End Set
    End Property

    Public Property InsLifeDocument() As Byte()
        Get
            Return _InsLifeDocument
        End Get
        Set(ByVal value As Byte())
            _InsLifeDocument = value
        End Set
    End Property

    Public Property InsLifeDocumentName() As String
        Get
            Return _InsLifeDocumentName
        End Get
        Set(ByVal value As String)
            _InsLifeDocumentName = value
        End Set
    End Property

    Public Property InsDentalName() As Integer
        Get
            Return _InsDentalName
        End Get
        Set(ByVal value As Integer)
            _InsDentalName = value
        End Set
    End Property

    Public Property InsDentalAmount() As Decimal
        Get
            Return _InsDentalAmount
        End Get
        Set(ByVal value As Decimal)
            _InsDentalAmount = value
        End Set
    End Property

    Public Property InsDentalCover() As Integer
        Get
            Return _InsDentalCover
        End Get
        Set(ByVal value As Integer)
            _InsDentalCover = value
        End Set
    End Property

    Public Property InsDentalComment() As String
        Get
            Return _InsDentalComment
        End Get
        Set(ByVal value As String)
            _InsDentalComment = value
        End Set
    End Property

    Public Property InsDentalDocument() As Byte()
        Get
            Return _InsDentalDocument
        End Get
        Set(ByVal value As Byte())
            _InsDentalDocument = value
        End Set
    End Property

    Public Property InsDentalDocumentName() As String
        Get
            Return _InsDentalDocumentName
        End Get
        Set(ByVal value As String)
            _InsDentalDocumentName = value
        End Set
    End Property

    Public Property InsDisabilityName() As Integer
        Get
            Return _InsDisabilityName
        End Get
        Set(ByVal value As Integer)
            _InsDisabilityName = value
        End Set
    End Property

    Public Property InsDisabilityAmount() As Decimal
        Get
            Return _InsDisabilityAmount
        End Get
        Set(ByVal value As Decimal)
            _InsDisabilityAmount = value
        End Set
    End Property

    Public Property InsDisabilityCover() As Integer
        Get
            Return _InsDisabilityCover
        End Get
        Set(ByVal value As Integer)
            _InsDisabilityCover = value
        End Set
    End Property

    Public Property InsDisabilityComment() As String
        Get
            Return _InsDisabilityComment
        End Get
        Set(ByVal value As String)
            _InsDisabilityComment = value
        End Set
    End Property

    Public Property InsDisabilityDocument() As Byte()
        Get
            Return _InsDisabilityDocument
        End Get
        Set(ByVal value As Byte())
            _InsDisabilityDocument = value
        End Set
    End Property

    Public Property InsDisabilityDocumentName() As String
        Get
            Return _InsDisabilityDocumentName
        End Get
        Set(ByVal value As String)
            _InsDisabilityDocumentName = value
        End Set
    End Property

    Public Property RetName() As Integer
        Get
            Return _RetName
        End Get
        Set(ByVal value As Integer)
            _RetName = value
        End Set
    End Property

    Public Property RetAmountMoney() As Decimal
        Get
            Return _RetAmountMoney
        End Get
        Set(ByVal value As Decimal)
            _RetAmountMoney = value
        End Set
    End Property

    Public Property RetAmountPercent() As Decimal
        Get
            Return _RetAmountPercent
        End Get
        Set(ByVal value As Decimal)
            _RetAmountPercent = value
        End Set
    End Property

    Public Property RetComment() As String
        Get
            Return _RetComment
        End Get
        Set(ByVal value As String)
            _RetComment = value
        End Set
    End Property

    Public Property RetDocument() As String
        Get
            Return _RetDocument
        End Get
        Set(ByVal value As String)
            _RetDocument = value
        End Set
    End Property

    Public Property RetDocumentName() As String
        Get
            Return _RetDocumentName
        End Get
        Set(ByVal value As String)
            _RetDocumentName = value
        End Set
    End Property

    Public Property EmployeeJobCat() As Integer
        Get
            Return _EmployeeJobCat
        End Get
        Set(ByVal value As Integer)
            _EmployeeJobCat = value
        End Set
    End Property

End Class
