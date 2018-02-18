Public Class FixDates
    Inherits System.Web.UI.Page
    Private timeOut As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Server.ScriptTimeout = 3600 'in seconds - 1 hour = 60 secs x 60 min  
            Dim yr As Integer = Year(Date.Now) + 1
            Dim fdate As Date = "1/1/" & yr
            RadDateInput1.SelectedDate = fdate
            Button3.Visible = False
            
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        lblCount.Text = Countem().ToString & " Records &nbsp; &nbsp;"
        RadGrid1.Visible = False
        Button3.Visible = True

    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim dt As DataTable = Showem()
        RadGrid1.Visible = True
        RadGrid1.DataSource = dt
        RadGrid1.DataBind()
        lblCount.Text = dt.Rows.Count.ToString() & " Records"
        Button3.Visible = True
    End Sub

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        '        System.Threading.Thread.Sleep(300000)   ' 60000 1 min 120000 2 min 180000 3 min
        Dim fyear As Integer = Year(RadDateInput1.SelectedDate)
        Dim newyear As Integer = txtfYear.Text
        Dim dba As New DBAccess()
        If IsNumeric(txtfixCount.Text) Then
            dba.CommandText = "SELECT TOP " & txtfixCount.Text & " WorkOrder.ID, Location.Name, WorkOrder.AppointmentTime, WorkOrder.GateTime, WorkOrder.DockTime, WorkOrder.StartTime, WorkOrder.CompTime FROM WorkOrder INNER JOIN Location ON WorkOrder.LocationID = Location.ID WHERE (WorkOrder.AppointmentTime > CONVERT (DATETIME, @fDate, 102)) OR (WorkOrder.GateTime > CONVERT (DATETIME, @fDate, 102)) OR (WorkOrder.DockTime > CONVERT (DATETIME, @fDate, 102)) OR (WorkOrder.StartTime > CONVERT (DATETIME, @fDate, 102)) OR (WorkOrder.CompTime > CONVERT (DATETIME, @fDate, 102)) ORDER BY StartTime"
        Else
            dba.CommandText = "SELECT WorkOrder.ID, Location.Name, WorkOrder.AppointmentTime, WorkOrder.GateTime, WorkOrder.DockTime, WorkOrder.StartTime, WorkOrder.CompTime FROM WorkOrder INNER JOIN Location ON WorkOrder.LocationID = Location.ID WHERE (WorkOrder.AppointmentTime > CONVERT (DATETIME, @fDate, 102)) OR (WorkOrder.GateTime > CONVERT (DATETIME, @fDate, 102)) OR (WorkOrder.DockTime > CONVERT (DATETIME, @fDate, 102)) OR (WorkOrder.StartTime > CONVERT (DATETIME, @fDate, 102)) OR (WorkOrder.CompTime > CONVERT (DATETIME, @fDate, 102)) ORDER BY StartTime"
        End If
        dba.AddParameter("@fDate", RadDateInput1.SelectedDate)
        Dim ds As DataSet = dba.ExecuteDataSet
        Dim dt As DataTable = ds.Tables(0)
        Dim fdList As New List(Of datefixer)
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                Dim dfxr As New datefixer()
                dfxr.woid = rw.Item("ID")
                dfxr.AppointmentTime = IIf(Not IsDBNull(rw.Item("AppointmentTime")), rw.Item("AppointmentTime"), "1/1/1900")
                dfxr.GateTime = IIf(Not IsDBNull(rw.Item("GateTime")), rw.Item("GateTime"), "1/1/1900")
                dfxr.DockTime = IIf(Not IsDBNull(rw.Item("DockTime")), rw.Item("DockTime"), "1/1/1900")
                dfxr.StartTime = IIf(Not IsDBNull(rw.Item("StartTime")), rw.Item("StartTime"), "1/1/1900")
                dfxr.CompTime = IIf(Not IsDBNull(rw.Item("CompTime")), rw.Item("CompTime"), "1/1/1900")
                If DatePart(DateInterval.Year, dfxr.AppointmentTime) > fyear Then
                    Dim dif As Integer = DatePart(DateInterval.Year, dfxr.AppointmentTime) - newyear
                    dfxr.AppointmentTime = DateAdd(DateInterval.Year, -dif, dfxr.AppointmentTime)
                End If
                If DatePart(DateInterval.Year, dfxr.GateTime) > fyear Then
                    Dim dif As Integer = DatePart(DateInterval.Year, dfxr.GateTime) - newyear
                    dfxr.GateTime = DateAdd(DateInterval.Year, -dif, dfxr.GateTime)
                End If
                If DatePart(DateInterval.Year, dfxr.DockTime) > fyear Then
                    Dim dif As Integer = DatePart(DateInterval.Year, dfxr.DockTime) - newyear
                    dfxr.DockTime = DateAdd(DateInterval.Year, -dif, dfxr.DockTime)
                End If
                If DatePart(DateInterval.Year, dfxr.StartTime) > fyear Then
                    Dim dif As Integer = DatePart(DateInterval.Year, dfxr.StartTime) - newyear
                    dfxr.StartTime = DateAdd(DateInterval.Year, -dif, dfxr.StartTime)
                End If
                If DatePart(DateInterval.Year, dfxr.CompTime) > fyear Then
                    Dim dif As Integer = DatePart(DateInterval.Year, dfxr.CompTime) - newyear
                    dfxr.CompTime = DateAdd(DateInterval.Year, -dif, dfxr.CompTime)
                End If
                fdList.Add(dfxr)
            Next
        End If
        saveem(fdList)
        RadGrid1.Visible = True

        RadGrid1.DataSource = fdList
        RadGrid1.DataBind()
        lblCount.Text = Countem().ToString & " Records"


    End Sub
    Protected Function Countem() As Integer
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT COUNT(WorkOrder.ID) FROM WorkOrder WHERE (WorkOrder.AppointmentTime > CONVERT (DATETIME, @fDate, 102)) OR (WorkOrder.GateTime > CONVERT (DATETIME, @fDate, 102)) OR (WorkOrder.DockTime > CONVERT (DATETIME, @fDate, 102)) OR (WorkOrder.StartTime > CONVERT (DATETIME, @fDate, 102)) OR (WorkOrder.CompTime > CONVERT (DATETIME, @fDate, 102))"
        dba.AddParameter("@fDate", RadDateInput1.SelectedDate)
        Dim dc As Integer = dba.ExecuteScalar
        Return dc
    End Function

    Protected Function Showem() As DataTable
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT WorkOrder.ID, Location.Name, WorkOrder.AppointmentTime, WorkOrder.GateTime, WorkOrder.DockTime, WorkOrder.StartTime, WorkOrder.CompTime FROM WorkOrder INNER JOIN Location ON WorkOrder.LocationID = Location.ID WHERE (WorkOrder.AppointmentTime > CONVERT (DATETIME, @fDate, 102)) OR (WorkOrder.GateTime > CONVERT (DATETIME, @fDate, 102)) OR (WorkOrder.DockTime > CONVERT (DATETIME, @fDate, 102)) OR (WorkOrder.StartTime > CONVERT (DATETIME, @fDate, 102)) OR (WorkOrder.CompTime > CONVERT (DATETIME, @fDate, 102)) ORDER BY StartTime"
        dba.AddParameter("@fDate", RadDateInput1.SelectedDate)
        Dim ds As DataSet = dba.ExecuteDataSet
        Dim dt As DataTable = ds.Tables(0)
        Return dt
    End Function

    Protected Sub saveem(ByRef fdList As List(Of datefixer))
        Dim dba As New DBAccess()
        For Each fd As datefixer In fdList
            dba.CommandText = "UPDATE WorkOrder Set AppointmentTime = @AppointmentTime, GateTime = @GateTime, DockTime = @DockTime, StartTime = @StartTime, CompTime = @CompTime WHERE ID = @woid"
            dba.AddParameter("@AppointmentTime", fd.AppointmentTime)
            dba.AddParameter("@GateTime", fd.GateTime)
            dba.AddParameter("@DockTime", fd.DockTime)
            dba.AddParameter("@StartTime", fd.StartTime)
            dba.AddParameter("@CompTime", fd.CompTime)
            dba.AddParameter("@woid", fd.woid)
            dba.ExecuteNonQuery()
        Next
    End Sub

    Private Sub FixDates_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload

        '        Server.ScriptTimeout = timeOut
    End Sub


End Class
Public Class datefixer
#Region "Private Variables"
    Private _woid As Guid
    Private _AppointmentTime As Date
    Private _GateTime As Date
    Private _DockTime As Date
    Private _StartTime As Date
    Private _CompTime As Date
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

    Public Property AppointmentTime() As Date
        Get
            Return _AppointmentTime
        End Get
        Set(ByVal value As Date)
            _AppointmentTime = value
        End Set
    End Property

    Public Property GateTime() As Date
        Get
            Return _GateTime
        End Get
        Set(ByVal value As Date)
            _GateTime = value
        End Set
    End Property

    Public Property DockTime() As Date
        Get
            Return _DockTime
        End Get
        Set(ByVal value As Date)
            _DockTime = value
        End Set
    End Property

    Public Property StartTime() As Date
        Get
            Return _StartTime
        End Get
        Set(ByVal value As Date)
            _StartTime = value
        End Set
    End Property

    Public Property CompTime() As Date
        Get
            Return _CompTime
        End Get
        Set(ByVal value As Date)
            _CompTime = value
        End Set
    End Property
#End Region
End Class