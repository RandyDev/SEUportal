Imports Telerik.Web.UI
'Imports WebSupergoo.ABCpdf7

Public Class DuplicateReceipts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If RadCaptcha1.Visible Then
            RadCaptcha1.Focus()
        End If

    End Sub

    Public Function validateDupForm() As String
        Dim retString As String = String.Empty
        If RadDatePicker1.SelectedDate Is Nothing Then
            retString = "Date, "
        Else
            If Not IsDate(RadDatePicker1.SelectedDate.ToString) Then
                retString = "Valid Date, "
            End If
        End If
        If txtPOnumber.Text.Trim.Length < 1 Then
            retString &= "Complete PO #, "
        End If
        If txtAmount.Text.Trim.Length < 1 Then
            retString &= "Amount, "
        End If
        If Not retString = String.Empty Then

            retString = Left(retString, Len(retString) - 2) & " is required"
        End If

        Return retString
    End Function


    Private Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Dim valMsg = validateDupForm()
        If Not valMsg = String.Empty Then
            lblResult.Text = " &nbsp;  &nbsp;  &nbsp; <font color='red'>ALL fields required!</font>"
        Else
            Dim vDate As DateTime = FormatDateTime(RadDatePicker1.SelectedDate, DateFormat.ShortDate)
            Dim vPOnum As String = txtPOnumber.Text.Trim()
            Dim commentsPOnum As String = String.Empty
            Dim vAmount As Decimal = txtAmount.Text


            Dim dba As New DBAccess()
            dba.CommandText = "SELECT WorkOrder.ID AS woID, CONVERT(varchar(10),WorkOrder.LogDate,110) AS LogDate, Location.Name AS Location, Department.Name AS Department, WorkOrder.ReceiptNumber,  " & _
                "Carrier.Name AS Carrier, WorkOrder.TruckNumber, WorkOrder.TrailerNumber, WorkOrder.BadPallets, WorkOrder.CompTime, WorkOrder.Amount, WorkOrder.PurchaseOrder,  " & _
                "WorkOrder.Restacks, Description.Name AS LoadDescription, WorkOrder.Comments " & _
                "FROM WorkOrder INNER JOIN " & _
                "Location ON WorkOrder.LocationID = Location.ID INNER JOIN " & _
                "Department ON WorkOrder.DepartmentID = Department.ID INNER JOIN " & _
                "Carrier ON WorkOrder.CarrierID = Carrier.ID INNER JOIN " & _
                "Description ON WorkOrder.LoadDescriptionID = Description.ID " & _
                "WHERE CONVERT(varchar(10),WorkOrder.LogDate,110)=@vDate AND WorkOrder.Amount=@amount AND " & _
                "((WorkOrder.PurchaseOrder=@purchaseOrder) OR (WorkOrder.Comments LIKE '%" & vPOnum & "%'))"
            dba.AddParameter("@vDate", vDate)
            dba.AddParameter("@purchaseOrder", vPOnum)
            dba.AddParameter("@amount", vAmount)
            Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                Dim dup As New DuplicateReceipt()
                If Not IsDBNull(row.Item("woID")) Then dup.woID = row.Item("woID")
                If Not IsDBNull(row.Item("LogDate")) Then dup.LogDate = row.Item("LogDate")
                If Not IsDBNull(row.Item("Location")) Then dup.Location = row.Item("Location")
                If Not IsDBNull(row.Item("Department")) Then dup.Department = row.Item("Department")
                If Not IsDBNull(row.Item("ReceiptNumber")) Then dup.ReceiptNumber = row.Item("ReceiptNumber")
                If Not IsDBNull(row.Item("PurchaseOrder")) Then dup.PurchaseOrder = row.Item("PurchaseOrder")
                If Not IsDBNull(row.Item("Carrier")) Then dup.Carrier = row.Item("Carrier")
                If Not IsDBNull(row.Item("TruckNumber")) Then dup.TruckNumber = row.Item("TruckNumber")
                If Not IsDBNull(row.Item("TrailerNumber")) Then dup.TrailerNumber = row.Item("TrailerNumber")
                If Not IsDBNull(row.Item("BadPallets")) Then dup.BadPallets = row.Item("BadPallets")
                If Not IsDBNull(row.Item("CompTime")) Then dup.CompTime = row.Item("CompTime")
                If Not IsDBNull(row.Item("Amount")) Then dup.Amount = row.Item("Amount")
                If Not IsDBNull(row.Item("Restacks")) Then dup.Restacks = row.Item("Restacks")
                If Not IsDBNull(row.Item("LoadDescription")) Then dup.LoadDescription = row.Item("LoadDescription")
                If Not IsDBNull(row.Item("Comments")) Then dup.Comments = row.Item("Comments")

                'to do
                Dim lstItem As RadListBoxItem = New RadListBoxItem

                ' ********* begin appending work order GUID with a < (vPOnum found in comments) or a > (vPOnum is primary)
                ' ------ this is sent to seuDuplicateReceipts and decoded there
                If vPOnum <> dup.PurchaseOrder Then
                    commentsPOnum = vPOnum & " (c)"
                    lstItem.Attributes.Add("PurchaseOrder", dup.PurchaseOrder & "<br />" & commentsPOnum)
                    lstItem.Value = "<" & dup.woID.ToString
                Else
                    commentsPOnum = vPOnum
                    lstItem.Attributes.Add("PurchaseOrder", dup.PurchaseOrder)
                    lstItem.Value = ">" & dup.woID.ToString

                End If

                lstItem.Attributes.Add("Date", dup.LogDate)
                lstItem.Attributes.Add("Amount", dup.Amount)
                lstBoxworkorders.Items.Add(lstItem)

                lstItem.DataBind()

                txtAmount.Text = ""
                txtPOnumber.Text = ""
                lblResult.Text = "<font color='green'>PO# </font><span style=""font-size:14px;"">" & commentsPOnum & "</span> <font color='green'> added to que</font>"
            Else
                lblResult.Text = " &nbsp;  &nbsp;  &nbsp; <font color='red'>No matching record found ...</font>"

            End If

        End If

    End Sub

    Private Sub btnVerifyCaptcha_Click(sender As Object, e As System.EventArgs) Handles btnVerifyCaptcha.Click
        If Page.IsValid Then
            lblPageHelp.Visible = True
            RadAjaxPanel1.Visible = True
            pnlCaptcha.Visible = False
        End If
    End Sub



    Private Sub btnViewPrint_Click(sender As Object, e As System.EventArgs) Handles btnViewPrint.Click
        Dim dup As DuplicateReceipt = New DuplicateReceipt()


    End Sub
End Class