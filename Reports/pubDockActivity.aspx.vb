'Imports System.Data.SqlClient
'Imports DiversifiedLogistics
'Imports Telerik.Web.UI

Public Class pubDockActivity
    Inherits System.Web.UI.Page


    Protected cntWorking As Integer = 0
    Protected cntWarning As Integer = 0
    Protected cntLate As Integer = 0
    Protected cntcLate As Integer = 0
    Protected cntComplete As Integer = 0
    Protected cntTotal As Integer = 0

    Protected locaID As String = String.Empty '"CFE1F703-F8D3-4F23-B30C-192672B13BCC"
    Protected hideCompleted As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '            Session("vState") = 1
            UpdateActiveList()
        End If

    End Sub

    Protected Sub UpdateActiveList()
        locaID = Request.QueryString("locaID")
        Dim wdal As New WorkOrderDAL()
        Dim dt As New DataTable
        dt = wdal.GetActiveWorkOrders(New Guid(locaID), hideCompleted)
        LoadList.Text = String.Empty
        Dim locadal As New locaDAL
        Dim offset As Integer = locadal.getLocaTimeZoneOffset(New Guid(locaID)) + 5
        Dim varTimeNow As DateTime = DateAdd(DateInterval.Hour, offset, Date.Now())
        For Each rw As DataRow In dt.Rows
            cntTotal += 1
            LoadList.Text &= CreateLoadRow(rw)
        Next
    End Sub

    Protected Function CreateLoadRow(ByVal rw As DataRow) As String

        Dim txtDockTime As String = "<center>- - -</center>"
        Dim txtStartTime As String = "<center>- - -</center>"
        Dim txtCompTime As String = "<center>- - -</center>"
        If Not IsDBNull(rw.Item("DockTime")) Then txtDockTime = Format(rw.Item("DockTime"), "hh:mm tt")

        Dim varCompTime As DateTime = IIf(IsDBNull(rw.Item("CompTime")), "1/1/1900", rw.Item("CompTime"))

        Dim isOpen As Boolean = True
        Dim rwStyle As String = "color:#000000;" 'String.Empty
        Dim diff As Long

        Dim locadal As New locaDAL
        Dim offset As Integer = locadal.getLocaTimeZoneOffset(New Guid(locaID)) + 5
        Dim varTimeNow As DateTime = DateAdd(DateInterval.Hour, offset, Date.Now())

        ' look at comptime to determine status
        'look at year diff
        If Not IsDBNull(rw.Item("StartTime")) Then
            txtStartTime = Format(rw.Item("StartTime"), "hh:mm tt")
            diff = DateDiff(DateInterval.Year, varCompTime, varTimeNow)
            If diff = 0 Or diff = 1 Then
                rwStyle = "background-color:#BCCCB4;"
                isOpen = False
                cntComplete += 1       'add 1 to completed
                diff = DateDiff(DateInterval.Minute, rw.Item("StartTime"), varCompTime)
                If diff > 120 Then
                    rwStyle &= "color:#FF0000;"
                    cntcLate += 1       'add 1 to completed Late
                End If
            End If
            If isOpen Then
                diff = DateDiff(DateInterval.Minute, rw.Item("StartTime"), varTimeNow)
                cntWorking += 1
                If diff > 119 Then
                    rwStyle = "color:#FF0000;"
                    cntLate += 1
                ElseIf diff > 89 Then
                    rwStyle = "background-color:#FFAA00;"
                    cntWarning += 1
                End If
            End If
        End If

        If varCompTime <> "1/1/1900" Then
            txtCompTime = Format(rw.Item("CompTime"), "hh:mm tt")
        End If



        Dim edal As New empDAL
        Dim unloaders As String = edal.GetUnloadersByWOIDString(rw.Item("ID").ToString)
        Dim str As String = "<tr style=" & rwStyle & ";"">" & _
        "<td>" & rw.Item("DoorNum") & "</td>" & _
        "<td>" & rw.Item("Vendor") & "</td>" & _
        "<td>" & rw.Item("PurchaseOrder") & "</td>" & _
        "<td>" & rw.Item("Carrier") & "</td>" & _
        "<td>" & rw.Item("TrailerNumber") & "</td>" & _
        "<td>" & txtDockTime & "</td>" & _
        "<td>" & txtStartTime & "</td>" & _
        "<td>" & txtCompTime & "</td>" & _
        "<td>" & rw.Item("Department") & "</td>" & _
        "<td>" & rw.Item("LoadType") & "</td>" & _
        "<td>" & unloaders & "</td>" & _
        "</tr>"

        Return str

    End Function

    Private Sub thuTime_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles thuTime.Tick
        '        Select Case Session("vState")
        '            Case 1
        '        Session("vState") = 2
        '        hideCompleted = True
        UpdateActiveList()
        '        thuTime.Interval = "15000"
        '            Case 2
        '        Session("vState") = 1
        '        hideCompleted = False
        '        UpdateActiveList()
        '        thuTime.Interval = "15000"
        '            Case 3
        '        Session("vState") = 2
        '        End Select
    End Sub

End Class