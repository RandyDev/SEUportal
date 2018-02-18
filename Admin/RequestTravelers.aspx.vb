Imports Telerik.Web.UI

Public Class RequestTravelers
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            doLocaList()

        End If
    End Sub

    Protected Sub doLocaList()
        Dim ldal As New locaDAL
        Dim dt As DataTable = ldal.getLocations

        cbDLocation.DataSource = dt
        cbDLocation.DataTextField = "LocationName"
        cbDLocation.DataValueField = "locaID"
        cbDLocation.DataBind()
        cbDLocation.ClearSelection()
        '        Dim i As Integer = 0
        For Each rw As DataRow In dt.Rows
            Dim lnam As String = rw.Item("LocationName")


            Dim ntxtBox As RadNumericTextBox = New RadNumericTextBox()
            ntxtBox.MinValue = 0
            ntxtBox.Value = 0
            '            txt.MaxValue = 6
            ntxtBox.ID = "txt" & Replace(lnam, " ", "_")
            ntxtBox.NumberFormat.DecimalDigits = 0
            ntxtBox.Width = 35
            '            ntxtBox.AutoPostBack = True
            '            ntxtBox.CausesValidation = True
            '            phLocaList.Controls.Add(txt)
            form1.Controls.Add(ntxtBox)

            Dim lit1 As New Literal
            lit1.Text = " : "
            '            phLocaList.Controls.Add(lit1)
            form1.Controls.Add(lit1)

            Dim lbl As New Label
            lbl.ID = "lbl" & lnam
            lbl.Text = lnam
            '            phLocaList.Controls.Add(lbl)
            form1.Controls.Add(lbl)


            Dim lit2 As New Literal
            lit2.Text = "<br />"
            '            phLocaList.Controls.Add(lit2)
            form1.Controls.Add(lit2)
            '            i += 1

        Next


    End Sub

    Private Sub btnSubmit_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnSubmit.Command
        If e.CommandName = "requestTravelers" Then
            Dim dloca As String
            '            Dim hloca As String
            Dim sdate As Date
            Dim rdate As Date = FormatDateTime("1/1/1990", DateFormat.ShortDate)
            Dim empID As String
            If cbDLocation.SelectedIndex = -1 Then
                dloca = "00000000-0000-0000-000000000000"
            Else
                dloca = cbDLocation.SelectedValue
            End If
            sdate = dpStartDate.SelectedDate
            If Not dbReturnDate.SelectedDate Is Nothing Then
                rdate = dbReturnDate.SelectedDate
            End If
            empID = "00000000-0000-0000-000000000000"

            For Each item As RadComboBoxItem In cbDLocation.Items
                Dim lnam As String = "txt" & Replace(item.Text, " ", "_")
                Dim ntxtBox As RadNumericTextBox = CType(form1.FindControl(lnam), RadNumericTextBox)
                '                Dim val As Integer = ntxtBox.Value
                Dim a As String = String.Empty
            Next
            For Each wc As WebControl In form1.Controls
                Dim id As String = wc.ID


            Next
        End If
    End Sub


    Private Sub TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
End Class