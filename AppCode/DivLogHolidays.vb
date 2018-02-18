Public Class DivLogHolidays

    Public Function GetHoliday(ByVal sdate As Date) As List(Of Holiday)
        'set edate to begin of next week ... search for less than
        Dim edate As Date = DateAdd(DateInterval.Day, 7, sdate)
        Dim hList As New List(Of Holiday)
        Dim holiday As New Holiday
        Dim todaysDate As Date = FormatDateTime(Date.Now(), DateFormat.ShortDate)
        Dim thisYear As Integer = Year(todaysDate)

        '****** January
        'if you know the month and day
        Dim NewYearsDay As Date = DateSerial(thisYear, 1, 1)    'jan 1
        If todaysDate > NewYearsDay Then NewYearsDay = DateSerial(thisYear + 1, 1, 1)
        'if you want the Nth weekdayname in a month
        Dim MartinLutherKingDay = NDow(thisYear, 1, 3, 2) 'third monday in january
        If todaysDate > MartinLutherKingDay Then MartinLutherKingDay = NDow(thisYear + 1, 1, 3, 2)
        '        If NewYearsDay >= sdate And NewYearsDay < edate Then
        holiday = New Holiday
        holiday.hdate = NewYearsDay
        holiday.hname = "New Year's Day"
        holiday.hdateWeekDayName = WeekdayName(Weekday(NewYearsDay))
        holiday.hdateObserve = Observed(NewYearsDay)
        hList.Add(holiday)
        '        End If
        '        If MartinLutherKingDay >= sdate And MartinLutherKingDay < edate Then
        holiday = New Holiday
        holiday.hdate = MartinLutherKingDay
        holiday.hname = "Martin Luther King Day"
        holiday.hdateWeekDayName = WeekdayName(Weekday(MartinLutherKingDay))
        holiday.hdateObserve = Observed(MartinLutherKingDay)
        hList.Add(holiday)
        '        End If
        '****** July
        Dim IndepedenceDay As Date = DateSerial(thisYear, 7, 4) 'jul 4
        If todaysDate > IndepedenceDay Then IndepedenceDay = DateSerial(thisYear + 1, 7, 4)
        'Dim BillsDay As Date = DateSerial(thisYear, 1, 23) 'jul 24
        'If todaysDate > BillsDay Then BillsDay = DateSerial(thisYear + 1, 1, 23)
        '        If IndepedenceDay >= sdate And IndepedenceDay < edate Then
        holiday = New Holiday
        holiday.hdate = IndepedenceDay
        holiday.hname = "Indepedence Day"
        holiday.hdateWeekDayName = WeekdayName(Weekday(IndepedenceDay))
        holiday.hdateObserve = Observed(IndepedenceDay)
        hList.Add(holiday)
        '        End If
        '        If BillsDay >= sdate And BillsDay < edate Then
        'holiday = New Holiday
        'holiday.hdate = BillsDay
        'holiday.hname = "Bill's Special Day - " & Year(BillsDay) - 1959.ToString()
        'holiday.hdateWeekDayName = WeekdayName(Weekday(BillsDay))
        'holiday.hdateObserve = Observed(BillsDay)
        'hList.Add(holiday)
        '        End If
        '****** September
        Dim LaborDay As Date = NDow(thisYear, 9, 1, 2)  'first Monday in Sept
        If todaysDate > LaborDay Then LaborDay = NDow(thisYear + 1, 9, 1, 2)
        '        If LaborDay >= sdate And LaborDay < edate Then
        holiday = New Holiday
        holiday.hdate = LaborDay
        holiday.hname = "Labor Day"
        holiday.hdateWeekDayName = WeekdayName(Weekday(LaborDay))
        holiday.hdateObserve = Observed(LaborDay)
        hList.Add(holiday)
        '        End If
        '****** November
        Dim Thanksgiving As Date = ThanksgivingDate(thisYear) '4th thursday in November
        If todaysDate > Thanksgiving Then Thanksgiving = ThanksgivingDate(thisYear + 1)
        '        If Thanksgiving >= sdate And Thanksgiving < edate Then
        holiday = New Holiday
        holiday.hdate = Thanksgiving
        holiday.hname = "Thanksgiving"
        holiday.hdateWeekDayName = WeekdayName(Weekday(Thanksgiving))
        holiday.hdateObserve = Observed(Thanksgiving)
        hList.Add(holiday)
        '        End If
        '****** December
        Dim Christmas As Date = DateSerial(thisYear, 12, 25)    'dec 25th
        If todaysDate > Christmas Then Christmas = DateSerial(thisYear + 1, 12, 25)
        '        If Christmas >= sdate And Christmas < edate Then
        holiday = New Holiday
        holiday.hdate = Christmas
        holiday.hname = "Christmas"
        holiday.hdateWeekDayName = WeekdayName(Weekday(Christmas))
        holiday.hdateObserve = Observed(Christmas)
        hList.Add(holiday)
        '        End If
        hList.Sort(Function(x, y) x.hdate.CompareTo(y.hdate))
        Return hList
    End Function

    Function Observed(TheDate As Date) As Date
        If Weekday(TheDate, vbSunday) = 1 Then
            Observed = DateAdd(DateInterval.Day, 1, TheDate)
        ElseIf Weekday(TheDate, vbSunday) = 7 Then
            Observed = DateAdd(DateInterval.Day, -1, TheDate)
        Else
            Observed = TheDate
        End If
    End Function
    'where TheDate is the date if the holiday.

    Public Function ThanksgivingDate(Yr As Integer) As Date
        'subtract 
        ThanksgivingDate = DateSerial(Yr, 11, 29 - Weekday(DateSerial(Yr, 11, 1), vbFriday))
    End Function

    'To return the date of the 3rd Monday in January of 2012, use
    '=NDow (2012, 1, 3, 2)
    Public Function NDow(Y As Integer, M As Integer, N As Integer, DOW As Integer) As Date
        NDow = DateSerial(Y, M, (8 - Weekday(DateSerial(Y, M, 1), (DOW + 1) Mod 8)) + ((N - 1) * 7))
    End Function


    'Calling this function will tell us how many Mondays there are in May, 2012.
    '=DOWsInMonth(2012, 5, 2)
    Public Function DOWsInMonth(Yr As Integer, M As Integer, _
    DOW As Integer) As Integer
        On Error GoTo EndFunction
        Dim I As Integer
        Dim Lim As Integer
        Lim = Day(DateSerial(Yr, M + 1, 0))
        DOWsInMonth = 0
        For I = 1 To Lim
            If WeekDay(DateSerial(Yr, M, I)) = DOW Then
                DOWsInMonth = DOWsInMonth + 1
            End If
        Next I
        Exit Function
EndFunction:
        DOWsInMonth = 0
    End Function


End Class


Public Class Holiday
    Private _hdate As Date
    Private _hname As String
    Private _hdateWeekDayName As String
    Private _hdateObserve As Date

    Public Property hdate() As Date
        Get
            Return _hdate
        End Get
        Set(ByVal value As Date)
            _hdate = value
        End Set
    End Property

    Public Property hname() As String
        Get
            Return _hname
        End Get
        Set(ByVal value As String)
            _hname = value
        End Set
    End Property

    Public Property hdateWeekDayName() As String
        Get
            Return _hdateWeekDayName
        End Get
        Set(ByVal value As String)
            _hdateWeekDayName = value
        End Set
    End Property

    Public Property hdateObserve() As Date
        Get
            Return _hdateObserve
        End Get
        Set(ByVal value As Date)
            _hdateObserve = value
        End Set
    End Property


End Class