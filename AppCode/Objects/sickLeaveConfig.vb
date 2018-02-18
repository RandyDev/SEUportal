Public Class sickLeaveConfig
    Private _LocationID As Guid
    Private _sickLeaveStart As DateTime
    Private _sickLeaveMaxAccrued As Integer
    Private _sickLeaveMinPerUse As Integer
    Private _sickLeaveEligibility As Integer
    Private _sickLeaveMaxUseAnnum As Integer
    Private _sickLeaveAccrualRate As Integer

    Public Property LocationID() As Guid
        Get
            Return _LocationID
        End Get
        Set(ByVal value As Guid)
            _LocationID = value
        End Set
    End Property

    Public Property sickLeaveStart() As DateTime
        Get
            Return _sickLeaveStart
        End Get
        Set(ByVal value As DateTime)
            _sickLeaveStart = value
        End Set
    End Property

    Public Property sickLeaveMaxAccrued() As Integer
        Get
            Return _sickLeaveMaxAccrued
        End Get
        Set(ByVal value As Integer)
            _sickLeaveMaxAccrued = value
        End Set
    End Property

    Public Property sickLeaveMinPerUse() As Integer
        Get
            Return _sickLeaveMinPerUse
        End Get
        Set(ByVal value As Integer)
            _sickLeaveMinPerUse = value
        End Set
    End Property

    Public Property sickLeaveEligibility() As Integer
        Get
            Return _sickLeaveEligibility
        End Get
        Set(ByVal value As Integer)
            _sickLeaveEligibility = value
        End Set
    End Property

    Public Property sickLeaveMaxUseAnnum() As Integer
        Get
            Return _sickLeaveMaxUseAnnum
        End Get
        Set(ByVal value As Integer)
            _sickLeaveMaxUseAnnum = value
        End Set
    End Property

    Public Property sickLeaveAccrualRate() As Integer
        Get
            Return _sickLeaveAccrualRate
        End Get
        Set(ByVal value As Integer)
            _sickLeaveAccrualRate = value
        End Set
    End Property


End Class
