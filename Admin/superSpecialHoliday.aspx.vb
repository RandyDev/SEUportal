Imports System
Imports Telerik.Web.UI

Public Class superSpecialHoliday
    Inherits System.Web.UI.Page

    Dim pPasser As PropertyPasser = New PropertyPasser()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim s As String = "0"
            Session("mt") = s
            Dim tpdal As New TimePuncheDAL()
            Dim vTabText As String = String.Empty
            'get start of current payperiod
            Dim sdate As Date = tpdal.getPayStartDate(Date.Now())
            'subtract one day
            sdate = DateAdd(DateInterval.Day, -1, sdate)
            'get start of previous pay period
            sdate = tpdal.getPayStartDate(sdate)
            'display 6 tabs ... one per week
            For i = 1 To 6
                Dim tab As RadTab = New RadTab()
                tab.Text = getTabText(sdate)
                tab.Value = tab.Text
                RadTabStrip1.Tabs.Add(tab)
                If Date.Now() >= sdate And Date.Now() < DateAdd(DateInterval.Day, 7, sdate) Then
                    tab.Selected = True
                    AddPageView(tab)

                    'Dim str As String = tab.Text
                    'Dim dt As Date = Left(str, 6) + DatePart(DateInterval.Year, Date.Now).ToString
                    'pPasser.LocaID = New Guid()
                    'pPasser.sDate = dt
                    'tab.PageViewID = tab.Value
                    'tab.PageView.Selected = True
                End If
                sdate = DateAdd(DateInterval.Day, 7, sdate)
            Next

        End If
    End Sub

    Private Function getTabText(ByVal dt As Date) As String
        Dim tstr As String = String.Empty
        Dim sdate As Date = dt
        Dim stdate As String = Format(sdate, "dd MMM")
        Dim edate As Date = DateAdd(DateInterval.Day, 6, sdate)
        Dim etdate As String = Format(edate, "dd MMM")
        tstr = stdate + "-" + etdate
        Return tstr
    End Function

    Private Sub RadMultiPage1_PageViewCreated(sender As Object, e As Telerik.Web.UI.RadMultiPageEventArgs) Handles RadMultiPage1.PageViewCreated

        Dim userControlName As String = "~/UserControls/AdditionalPay.ascx"
        Dim userControl As Control = Page.LoadControl(userControlName)
        userControl.ID = e.PageView.ID + "_userControl"
        
        Dim str As String = e.PageView.ID
        Dim dt As Date = Left(str, 6) + DatePart(DateInterval.Year, Date.Now).ToString
        pPasser.LocaID = New Guid()

        pPasser.sDate = dt

        'userControl.locaID = pPasser.LocaID
        '        userControl.sDate = pPasser.sDate

        e.PageView.Controls.Add(userControl)

    End Sub

    Private Sub AddPageView(ByVal tab As RadTab)
        Dim pageView As RadPageView = New RadPageView()
        pageView.ID = tab.Text
        RadMultiPage1.PageViews.Add(pageView)
        pageView.CssClass = "pageView"
        tab.PageViewID = pageView.ID
    End Sub

    Private Sub RadTabStrip1_TabClick(sender As Object, e As Telerik.Web.UI.RadTabStripEventArgs) Handles RadTabStrip1.TabClick
        '        Dim str As String = e.Tab.Text
        '        Dim dt As Date = Left(str, 6) + DatePart(DateInterval.Year, Date.Now).ToString
        '        pPasser.LocaID = New Guid()
        '        pPasser.sDate = dt
        AddPageView(e.Tab)
        e.Tab.PageViewID = e.Tab.Text
        e.Tab.PageView.Selected = True

    End Sub
End Class