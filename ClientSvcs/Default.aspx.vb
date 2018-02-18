Imports Telerik.Charting

Public Class _Default1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Dim dt1 As DataTable = badPalletsByVendor()
            'RadGrid1.DataSource = dt1
            'RadGrid1.DataBind()
            'Dim dt2 As DataTable = restacksByVendor()
            'RadGrid2.DataSource = dt2
            'RadGrid2.DataBind()
            'loadPalletsByDayChart()
            '            UpdateChart()

        End If

        Const TIMEOUT As Double = 5000
        Dim aTimer As New System.Timers.Timer(TIMEOUT)
        aTimer.Start()
        aTimer.Interval = TIMEOUT


        Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
        Dim uid As String = puser.ProviderUserKey.ToString
        Dim rtdsID As String
        rtdsID = Utilities.getRTDSidByUserID(uid)
        Dim emp As New Employee
        If rtdsID <> "00000000-0000-0000-0000-000000000000" Then
            Dim rtdsIDtoGUID As New Guid(rtdsID)
            Dim empdal As New empDAL
            emp = empdal.GetEmployeeByID(rtdsIDtoGUID)
        End If
        Dim udal As New userDAL()
        Dim ldal As New locaDAL()
        emp.ssUser = udal.getUserByName(puser.UserName)
        Dim dt As New DataTable
        If emp.ssUser.myRoles.Count = 1 And (emp.ssUser.myRoles(0) = "Manager" Or emp.ssUser.myRoles(0) = "Client" Or emp.ssUser.myRoles(0) = "Guest") Then
            dt = ldal.getUserLocaList(emp.ssUser.userID)
            emp.LocationID = dt.Rows(0).Item("locaID")
        Else
            dt = ldal.getLocations
        End If

        If Utilities.IsValidGuid(emp.LocationID.ToString) Then
            Dim pname As String = ldal.getParentNameByLocationID(emp.LocationID.ToString)
            lblParentCompanyName.Text = IIf(pname.Length > 0, pname, "")
            Dim rootDir As String = Server.MapPath("~/")
            Dim logo As String = Utilities.getLogo(emp.LocationID, rootDir)
            imgLogo.ImageUrl = logo
        End If


    End Sub

    Private Sub UpdateChart()
        Radchart2.PlotArea.XAxis.Clear()
        Radchart2.PlotArea.XAxis.AddItem("Monday")
        Radchart2.PlotArea.XAxis.AddItem("Tuesday")
        Radchart2.PlotArea.XAxis.AddItem("Wednesday")
        Radchart2.PlotArea.XAxis.AddItem("Thursday")
        Radchart2.PlotArea.XAxis.AddItem("Friday")
        Radchart2.PlotArea.XAxis.AddItem("Saturday")
        Radchart2.PlotArea.XAxis.AddItem("Sunday")

        Dim salesDataSeries As ChartSeries = Radchart2.Series(0)

        salesDataSeries.Clear()

        If Not (salesDataSeries Is Nothing) Then
            Dim r As New Random()

            Dim i As Integer
            For i = 0 To 6
                salesDataSeries.AddItem(r.NextDouble())
            Next i
        End If
    End Sub


    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs)
        '        UpdateChart()
    End Sub

End Class
