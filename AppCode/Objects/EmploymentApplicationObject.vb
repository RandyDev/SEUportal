
Public Class EmploymentApplicationObject

#Region "Private Variables"
    Private _EmploymentApplicationID As Guid    'uniqueidentifier
    Private _LocationID As Guid                 'uniqueidentifier
    Private _FirstName As String                'nvarchar(50)
    Private _MiddleInitial As String            'nvarchar(50)
    Private _LastName As String                 'nvarchar(50)
    Private _Referredby As String               'nvarchar(75)
    Private _StreetAddress As String            'nvarchar(125)
    Private _Zip As String                      'nchar(5)
    Private _City As String                     'nvarchar(75)
    Private _State As String                    'nchar(4)
    Private _PrimaryPhone As String             'nchar(14)
    Private _AltPhone As String                 'nchar(14)
    Private _Email As String                    'nvarchar(150)
    Private _DesiredPosition As String          'nvarchar(125)
    Private _DesiredStartDate As DateTime       'datetime
    Private _DesiredSalary As String            'nchar(25)
    Private _CurrentlyEmployed As Boolean       'bit
    Private _AskCurrentEmployer As Boolean      'bit
    Private _AppliedBefore As Boolean           'bit
    Private _AppliedBeforeLocation As String    'nvarchar(125)
    Private _AppliedBeforeDate As String        'nchar(8)
    Private _EducationLevel As String           'nchar(25)
    Private _School1 As String                  'nvarchar(125)
    Private _School1Location As String          'nvarchar(125)
    Private _School1YearsAttended As String     'nchar(2)
    Private _School1Graduated As Boolean        'bit
    Private _School1SubjectsStudied As String   'text
    Private _School2 As String                  'nvarchar(500)
    Private _School2Location As String          'nvarchar(125)
    Private _School2YearsAttended As String     'nchar(2)
    Private _School2Graduated As Boolean        'bit
    Private _School2SubjectsStudied As String   'text
    Private _School3 As String                  'nvarchar(500)
    Private _School3Location As String          'nvarchar(125)
    Private _School3YearsAttended As String     'nchar(2)
    Private _School3Graduated As Boolean        'bit
    Private _School3SubjectsStudied As String   'text
    Private _SpecialSkills As String            'text
    Private _MilitaryBranch As String           'nchar(25)
    Private _MilitaryServiceFromDate As DateTime    'datetime
    Private _MilitaryServiceToDate As DateTime      'datetime
    Private _MilitaryRank As String             'nchar(25)
    Private _pe1FromDate As DateTime            'datetime
    Private _pe1ToDate As DateTime              'datetime
    Private _PE1 As String                      'nvarchar(125)
    Private _PE1Location As String              'nvarchar(125)
    Private _PE1phone As String                 'nchar(14)
    Private _PE1salary As String                'nvarchar(25)
    Private _PE1position As String            'nvarchar(150)
    Private _PE1reasonForLeaving As String      'text
    Private _pe2FromDate As DateTime            'datetime
    Private _pe2ToDate As DateTime              'datetime
    Private _PE2 As String                      'nvarchar(125)
    Private _PE2Location As String              'nvarchar(125)
    Private _PE2phone As String                 'nchar(14)
    Private _PE2salary As String                'nvarchar(25)
    Private _PE2position As String            'nvarchar(150)
    Private _PE2reasonForLeaving As String      'text
    Private _pe3FromDate As DateTime            'datetime
    Private _pe3ToDate As DateTime              'datetime
    Private _PE3 As String                      'nvarchar(125)
    Private _PE3Location As String              'nvarchar(125)
    Private _PE3phone As String                 'nchar(14)
    Private _PE3salary As String                'nvarchar(25)
    Private _PE3position As String            'nvarchar(150)
    Private _PE3reasonForLeaving As String      'text
    Private _pe4FromDate As DateTime            'datetime
    Private _pe4ToDate As DateTime              'datetime
    Private _PE4 As String                      'nvarchar(125)
    Private _PE4Location As String              'nvarchar(125)
    Private _PE4phone As String                 'nchar(14)
    Private _PE4salary As String                'nvarchar(25)
    Private _PE4position As String            'nvarchar(150)
    Private _PE4reasonForLeaving As String      'text
    Private _Reference1 As String               'nvarchar(225)
    Private _Reference1YrsKnown As String       'nchar(2)
    Private _Reference1Contact As String        'nvarchar(150)
    Private _Reference2 As String               'nvarchar(225)
    Private _Reference2YrsKnown As String       'nchar(2)
    Private _Reference2Contact As String        'nvarchar(150)
    Private _Reference3 As String               'nvarchar(225)
    Private _Reference3YrsKnown As String       'nchar(2)
    Private _Reference3Contact As String        'nvarchar(150)
    Private _HasDL As Boolean                   'bit
    Private _DLtype As String                   'nchar(15)
    Private _Dlstate As String                  'nchar(4)
    Private _NumberDLviolations As Integer      'int
    Private _DLrevoked As Boolean               'bit
    Private _DLrevokeDetail As String           'text
    Private _Conviction As Boolean              'bit
    Private _ConvictionDetails As String        'text
    Private _Rating As Integer                  'int
    Private _TimeStamp As Date                  'datetime
    Private _ApplicantIP As String              'nchar(25)
    Private _Notes As List(Of AppNotes)         '

#End Region

#Region "Public Properties"
    Public Property EmploymentApplicationID() As Guid
        Get
            Return _EmploymentApplicationID
        End Get
        Set(ByVal value As Guid)
            _EmploymentApplicationID = value
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
    Public Property FirstName() As String
        Get
            Return _FirstName
        End Get
        Set(ByVal value As String)
            _FirstName = value
        End Set
    End Property

    Public Property MiddleInitial() As String
        Get
            Return _MiddleInitial
        End Get
        Set(ByVal value As String)
            _MiddleInitial = value
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

    Public Property Referredby() As String
        Get
            Return _Referredby
        End Get
        Set(ByVal value As String)
            _Referredby = value
        End Set
    End Property

    Public Property StreetAddress() As String
        Get
            Return _StreetAddress
        End Get
        Set(ByVal value As String)
            _StreetAddress = value
        End Set
    End Property

    Public Property Zip() As String
        Get
            Return _Zip
        End Get
        Set(ByVal value As String)
            _Zip = value
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

    Public Property PrimaryPhone() As String
        Get
            Return _PrimaryPhone
        End Get
        Set(ByVal value As String)
            _PrimaryPhone = value
        End Set
    End Property

    Public Property AltPhone() As String
        Get
            Return _AltPhone
        End Get
        Set(ByVal value As String)
            _AltPhone = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property

    Public Property DesiredPosition() As String
        Get
            Return _DesiredPosition
        End Get
        Set(ByVal value As String)
            _DesiredPosition = value
        End Set
    End Property

    Public Property DesiredStartDate() As DateTime
        Get
            Return _DesiredStartDate
        End Get
        Set(ByVal value As DateTime)
            _DesiredStartDate = value
        End Set
    End Property

    Public Property DesiredSalary() As String
        Get
            Return _DesiredSalary
        End Get
        Set(ByVal value As String)
            _DesiredSalary = value
        End Set
    End Property

    Public Property CurrentlyEmployed() As Boolean
        Get
            Return _CurrentlyEmployed
        End Get
        Set(ByVal value As Boolean)
            _CurrentlyEmployed = value
        End Set
    End Property

    Public Property AskCurrentEmployer() As Boolean
        Get
            Return _AskCurrentEmployer
        End Get
        Set(ByVal value As Boolean)
            _AskCurrentEmployer = value
        End Set
    End Property

    Public Property AppliedBefore() As Boolean
        Get
            Return _AppliedBefore
        End Get
        Set(ByVal value As Boolean)
            _AppliedBefore = value
        End Set
    End Property

    Public Property AppliedBeforeLocation() As String
        Get
            Return _AppliedBeforeLocation
        End Get
        Set(ByVal value As String)
            _AppliedBeforeLocation = value
        End Set
    End Property

    Public Property AppliedBeforeDate() As String
        Get
            Return _AppliedBeforeDate
        End Get
        Set(ByVal value As String)
            _AppliedBeforeDate = value
        End Set
    End Property

    Public Property EducationLevel() As String
        Get
            Return _EducationLevel
        End Get
        Set(ByVal value As String)
            _EducationLevel = value
        End Set
    End Property

    Public Property School1() As String
        Get
            Return _School1
        End Get
        Set(ByVal value As String)
            _School1 = value
        End Set
    End Property

    Public Property School1Location() As String
        Get
            Return _School1Location
        End Get
        Set(ByVal value As String)
            _School1Location = value
        End Set
    End Property

    Public Property School1YearsAttended() As String
        Get
            Return _School1YearsAttended
        End Get
        Set(ByVal value As String)
            _School1YearsAttended = value
        End Set
    End Property

    Public Property School1Graduated() As Boolean
        Get
            Return _School1Graduated
        End Get
        Set(ByVal value As Boolean)
            _School1Graduated = value
        End Set
    End Property

    Public Property School1SubjectsStudied() As String
        Get
            Return _School1SubjectsStudied
        End Get
        Set(ByVal value As String)
            _School1SubjectsStudied = value
        End Set
    End Property

    Public Property School2() As String
        Get
            Return _School2
        End Get
        Set(ByVal value As String)
            _School2 = value
        End Set
    End Property

    Public Property School2Location() As String
        Get
            Return _School2Location
        End Get
        Set(ByVal value As String)
            _School2Location = value
        End Set
    End Property

    Public Property School2YearsAttended() As String
        Get
            Return _School2YearsAttended
        End Get
        Set(ByVal value As String)
            _School2YearsAttended = value
        End Set
    End Property

    Public Property School2Graduated() As Boolean
        Get
            Return _School2Graduated
        End Get
        Set(ByVal value As Boolean)
            _School2Graduated = value
        End Set
    End Property

    Public Property School2SubjectsStudied() As String
        Get
            Return _School2SubjectsStudied
        End Get
        Set(ByVal value As String)
            _School2SubjectsStudied = value
        End Set
    End Property

    Public Property School3() As String
        Get
            Return _School3
        End Get
        Set(ByVal value As String)
            _School3 = value
        End Set
    End Property

    Public Property School3Location() As String
        Get
            Return _School3Location
        End Get
        Set(ByVal value As String)
            _School3Location = value
        End Set
    End Property

    Public Property School3YearsAttended() As String
        Get
            Return _School3YearsAttended
        End Get
        Set(ByVal value As String)
            _School3YearsAttended = value
        End Set
    End Property

    Public Property School3Graduated() As Boolean
        Get
            Return _School3Graduated
        End Get
        Set(ByVal value As Boolean)
            _School3Graduated = value
        End Set
    End Property

    Public Property School3SubjectsStudied() As String
        Get
            Return _School3SubjectsStudied
        End Get
        Set(ByVal value As String)
            _School3SubjectsStudied = value
        End Set
    End Property

    Public Property SpecialSkills() As String
        Get
            Return _SpecialSkills
        End Get
        Set(ByVal value As String)
            _SpecialSkills = value
        End Set
    End Property

    Public Property MilitaryBranch() As String
        Get
            Return _MilitaryBranch
        End Get
        Set(ByVal value As String)
            _MilitaryBranch = value
        End Set
    End Property

    Public Property MilitaryServiceFromDate() As DateTime
        Get
            Return _MilitaryServiceFromDate
        End Get
        Set(ByVal value As DateTime)
            _MilitaryServiceFromDate = value
        End Set
    End Property

    Public Property MilitaryServiceToDate() As DateTime
        Get
            Return _MilitaryServiceToDate
        End Get
        Set(ByVal value As DateTime)
            _MilitaryServiceToDate = value
        End Set
    End Property

    Public Property MilitaryRank() As String
        Get
            Return _MilitaryRank
        End Get
        Set(ByVal value As String)
            _MilitaryRank = value
        End Set
    End Property

    Public Property pe1FromDate() As DateTime
        Get
            Return _pe1FromDate
        End Get
        Set(ByVal value As DateTime)
            _pe1FromDate = value
        End Set
    End Property

    Public Property pe1ToDate() As DateTime
        Get
            Return _pe1ToDate
        End Get
        Set(ByVal value As DateTime)
            _pe1ToDate = value
        End Set
    End Property

    Public Property PE1() As String
        Get
            Return _PE1
        End Get
        Set(ByVal value As String)
            _PE1 = value
        End Set
    End Property

    Public Property PE1Location() As String
        Get
            Return _PE1Location
        End Get
        Set(ByVal value As String)
            _PE1Location = value
        End Set
    End Property

    Public Property PE1phone() As String
        Get
            Return _PE1phone
        End Get
        Set(ByVal value As String)
            _PE1phone = value
        End Set
    End Property

    Public Property PE1salary() As String
        Get
            Return _PE1salary
        End Get
        Set(ByVal value As String)
            _PE1salary = value
        End Set
    End Property

    Public Property PE1position() As String
        Get
            Return _PE1position
        End Get
        Set(ByVal value As String)
            _PE1position = value
        End Set
    End Property

    Public Property PE1reasonForLeaving() As String
        Get
            Return _PE1reasonForLeaving
        End Get
        Set(ByVal value As String)
            _PE1reasonForLeaving = value
        End Set
    End Property

    Public Property pe2FromDate() As DateTime
        Get
            Return _pe2FromDate
        End Get
        Set(ByVal value As DateTime)
            _pe2FromDate = value
        End Set
    End Property

    Public Property pe2ToDate() As DateTime
        Get
            Return _pe2ToDate
        End Get
        Set(ByVal value As DateTime)
            _pe2ToDate = value
        End Set
    End Property

    Public Property PE2() As String
        Get
            Return _PE2
        End Get
        Set(ByVal value As String)
            _PE2 = value
        End Set
    End Property

    Public Property PE2Location() As String
        Get
            Return _PE2Location
        End Get
        Set(ByVal value As String)
            _PE2Location = value
        End Set
    End Property

    Public Property PE2phone() As String
        Get
            Return _PE2phone
        End Get
        Set(ByVal value As String)
            _PE2phone = value
        End Set
    End Property

    Public Property PE2salary() As String
        Get
            Return _PE2salary
        End Get
        Set(ByVal value As String)
            _PE2salary = value
        End Set
    End Property

    Public Property PE2position() As String
        Get
            Return _PE2position
        End Get
        Set(ByVal value As String)
            _PE2position = value
        End Set
    End Property

    Public Property PE2reasonForLeaving() As String
        Get
            Return _PE2reasonForLeaving
        End Get
        Set(ByVal value As String)
            _PE2reasonForLeaving = value
        End Set
    End Property

    Public Property pe3FromDate() As DateTime
        Get
            Return _pe3FromDate
        End Get
        Set(ByVal value As DateTime)
            _pe3FromDate = value
        End Set
    End Property

    Public Property pe3ToDate() As DateTime
        Get
            Return _pe3ToDate
        End Get
        Set(ByVal value As DateTime)
            _pe3ToDate = value
        End Set
    End Property

    Public Property PE3() As String
        Get
            Return _PE3
        End Get
        Set(ByVal value As String)
            _PE3 = value
        End Set
    End Property

    Public Property PE3Location() As String
        Get
            Return _PE3Location
        End Get
        Set(ByVal value As String)
            _PE3Location = value
        End Set
    End Property

    Public Property PE3phone() As String
        Get
            Return _PE3phone
        End Get
        Set(ByVal value As String)
            _PE3phone = value
        End Set
    End Property

    Public Property PE3salary() As String
        Get
            Return _PE3salary
        End Get
        Set(ByVal value As String)
            _PE3salary = value
        End Set
    End Property

    Public Property PE3position() As String
        Get
            Return _PE3position
        End Get
        Set(ByVal value As String)
            _PE3position = value
        End Set
    End Property

    Public Property PE3reasonForLeaving() As String
        Get
            Return _PE3reasonForLeaving
        End Get
        Set(ByVal value As String)
            _PE3reasonForLeaving = value
        End Set
    End Property

    Public Property pe4FromDate() As DateTime
        Get
            Return _pe4FromDate
        End Get
        Set(ByVal value As DateTime)
            _pe4FromDate = value
        End Set
    End Property

    Public Property pe4ToDate() As DateTime
        Get
            Return _pe4ToDate
        End Get
        Set(ByVal value As DateTime)
            _pe4ToDate = value
        End Set
    End Property

    Public Property PE4() As String
        Get
            Return _PE4
        End Get
        Set(ByVal value As String)
            _PE4 = value
        End Set
    End Property

    Public Property PE4Location() As String
        Get
            Return _PE4Location
        End Get
        Set(ByVal value As String)
            _PE4Location = value
        End Set
    End Property

    Public Property PE4phone() As String
        Get
            Return _PE4phone
        End Get
        Set(ByVal value As String)
            _PE4phone = value
        End Set
    End Property

    Public Property PE4salary() As String
        Get
            Return _PE4salary
        End Get
        Set(ByVal value As String)
            _PE4salary = value
        End Set
    End Property

    Public Property PE4position() As String
        Get
            Return _PE4position
        End Get
        Set(ByVal value As String)
            _PE4position = value
        End Set
    End Property

    Public Property PE4reasonForLeaving() As String
        Get
            Return _PE4reasonForLeaving
        End Get
        Set(ByVal value As String)
            _PE4reasonForLeaving = value
        End Set
    End Property

    Public Property Reference1() As String
        Get
            Return _Reference1
        End Get
        Set(ByVal value As String)
            _Reference1 = value
        End Set
    End Property

    Public Property Reference1YrsKnown() As String
        Get
            Return _Reference1YrsKnown
        End Get
        Set(ByVal value As String)
            _Reference1YrsKnown = value
        End Set
    End Property

    Public Property Reference1Contact() As String
        Get
            Return _Reference1Contact
        End Get
        Set(ByVal value As String)
            _Reference1Contact = value
        End Set
    End Property

    Public Property Reference2() As String
        Get
            Return _Reference2
        End Get
        Set(ByVal value As String)
            _Reference2 = value
        End Set
    End Property

    Public Property Reference2YrsKnown() As String
        Get
            Return _Reference2YrsKnown
        End Get
        Set(ByVal value As String)
            _Reference2YrsKnown = value
        End Set
    End Property

    Public Property Reference2Contact() As String
        Get
            Return _Reference2Contact
        End Get
        Set(ByVal value As String)
            _Reference2Contact = value
        End Set
    End Property

    Public Property Reference3() As String
        Get
            Return _Reference3
        End Get
        Set(ByVal value As String)
            _Reference3 = value
        End Set
    End Property

    Public Property Reference3YrsKnown() As String
        Get
            Return _Reference3YrsKnown
        End Get
        Set(ByVal value As String)
            _Reference3YrsKnown = value
        End Set
    End Property

    Public Property Reference3Contact() As String
        Get
            Return _Reference3Contact
        End Get
        Set(ByVal value As String)
            _Reference3Contact = value
        End Set
    End Property

    Public Property HasDL() As Boolean
        Get
            Return _HasDL
        End Get
        Set(ByVal value As Boolean)
            _HasDL = value
        End Set
    End Property

    Public Property DLtype() As String
        Get
            Return _DLtype
        End Get
        Set(ByVal value As String)
            _DLtype = value
        End Set
    End Property

    Public Property Dlstate() As String
        Get
            Return _Dlstate
        End Get
        Set(ByVal value As String)
            _Dlstate = value
        End Set
    End Property

    Public Property NumberDLviolations() As String
        Get
            Return _NumberDLviolations
        End Get
        Set(ByVal value As String)
            _NumberDLviolations = value
        End Set
    End Property

    Public Property DLrevoked() As Boolean
        Get
            Return _DLrevoked
        End Get
        Set(ByVal value As Boolean)
            _DLrevoked = value
        End Set
    End Property

    Public Property DLrevokeDetail() As String
        Get
            Return _DLrevokeDetail
        End Get
        Set(ByVal value As String)
            _DLrevokeDetail = value
        End Set
    End Property

    Public Property Conviction() As Boolean
        Get
            Return _Conviction
        End Get
        Set(ByVal value As Boolean)
            _Conviction = value
        End Set
    End Property

    Public Property ConvictionDetails() As String
        Get
            Return _ConvictionDetails
        End Get
        Set(ByVal value As String)
            _ConvictionDetails = value
        End Set
    End Property

    Public Property Rating() As Integer
        Get
            Return _Rating
        End Get
        Set(ByVal value As Integer)
            _Rating = value
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

    Public Property ApplicantIP() As String
        Get
            Return _ApplicantIP
        End Get
        Set(ByVal value As String)
            _ApplicantIP = value
        End Set
    End Property

    Public Property Notes() As List(Of AppNotes)
        Get
            Return _Notes
        End Get
        Set(ByVal value As List(Of AppNotes))
            _Notes = value
        End Set
    End Property

#End Region

End Class

Public Class AppNotes
    Private _appID As Guid
    Private _note As String
    Private _TimeStamp As Date
    Private _userID As Guid
    Private _userName As String

    Public Property appID() As Guid
        Get
            Return _appID
        End Get
        Set(ByVal value As Guid)
            _appID = value
        End Set
    End Property
    Public Property note() As String
        Get
            Return _note
        End Get
        Set(ByVal value As String)
            _note = value
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
    Public Property userID() As Guid
        Get
            Return _userID
        End Get
        Set(ByVal value As Guid)
            _userID = value
        End Set
    End Property
    Public Property userName() As String
        Get
            Return _userName
        End Get
        Set(ByVal value As String)
            _userName = value
        End Set
    End Property



End Class