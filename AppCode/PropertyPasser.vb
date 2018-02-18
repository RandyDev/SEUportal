Imports System

Public Class PropertyPasser
    Inherits System.Web.UI.UserControl

    Public Sub New()
    End Sub

    Public m_LocaID As Guid
    Public m_sDate As Date

    Public Property LocaID() As Guid
        Get
            Return m_LocaID
        End Get
        Set(value As Guid)
            m_LocaID = value
        End Set
    End Property

    Public Property sDate() As Date
        Get
            Return m_sDate
        End Get
        Set(value As Date)
            m_sDate = value
        End Set
    End Property


    Public Function GetLocaID() As Guid
        Return m_LocaID
    End Function

    Public Function GetsDate() As Date
        Return m_sDate
    End Function

End Class
