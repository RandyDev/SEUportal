Public Partial Class selectCarrier
    Inherits System.Web.UI.Page


    Protected Sub btnSaveCarrier_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveCarrier.Click
        Dim actionString As String = cbCarrier.Text
        actionString &= ":" & cbCarrier.SelectedValue.ToString
        RadAjaxManager1.ResponseScripts.Add("returnArg('" & actionString & "');")
    End Sub

    Private Sub selectCarrier_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            cbCarrier.ClearSelection()
            cbCarrier.Text = ""
            cbCarrier.Visible = False
            btnSaveCarrier.Visible = False
            lblCarriers.Visible = True
            lblCarriers.Text = "Enter partial Carrier Name then click 'Search Carriers'"
            txtCarrier.Attributes("onkeyup") = "decOnly(this);"
        End If

    End Sub

    Protected Sub btnCarrierSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCarrierSearch.Click

        Dim dba As New DBAccess()
        Dim sstr As String = txtCarrier.Text.Trim
        If sstr.Length < 2 Then
            cbCarrier.Visible = False
            btnSaveCarrier.Visible = False
            lblCarriers.Visible = True
            lblCarriers.Text = "Please provide at least 2 characters"
            Exit Sub
        ElseIf sstr.Length > 4 Then
            dba.CommandText = "SELECT ID, Name FROM Carrier WHERE Name LIKE '%" & sstr & "%' ORDER BY Name"
        Else
            dba.CommandText = "SELECT ID, Name FROM Carrier WHERE Name LIKE '" & sstr & "%' ORDER BY Name"
        End If
        Dim ds As DataSet = dba.ExecuteDataSet
        Dim dt As DataTable = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            dt.Rows.Add(Split("00000000-0000-0000-0000-000000000000:No Match Found", ":"))
            cbCarrier.Visible = True
            btnSaveCarrier.Visible = True
            lblCarriers.Visible = False
            cbCarrier.DataSource = dt
            cbCarrier.DataTextField = "Name"
            cbCarrier.DataValueField = "ID"
            cbCarrier.DataBind()
            cbCarrier.ClearSelection()
            cbCarrier.Text = ""
            btnAddCarrier.Visible = False
            btnSaveCarrier.Visible = False
        Else
            cbCarrier.Visible = False
            btnSaveCarrier.Visible = False
            lblCarriers.Visible = True
            lblCarriers.Text = "No Match Found"
            btnAddCarrier.Visible = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
            btnSaveCarrier.Visible = False
        End If

    End Sub

    Private Sub btnAddCarrier_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddCarrier.Click
        Dim actionString As String = txtCarrier.Text.Trim()
        Dim ncid As Guid = Guid.NewGuid()
        Dim timestamp As DateTime = Date.Now()
        Dim empdal As New empDAL()
        Dim rtdsUserID As String = Utilities.getRTDSidByUserID(HttpContext.Current.Session("userID").ToString)
        Dim emp As Employee = empdal.GetEmployeeByID(New Guid(rtdsUserID))
        Dim UserIP As String = emp.rtdsFirstName & " " & emp.rtdsLastName
        If UserIP.Length < 5 Then UserIP = HttpContext.Current.User.Identity.Name
        UserIP &= " : " & HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")

        Dim dba As New DBAccess()
        dba.CommandText = "INSERT INTO Carrier (Name, ID, UserIP, timestamp) VALUES (@Name, @ID, @UserIP, @timestamp)"
        dba.AddParameter("@Name", actionString)
        dba.AddParameter("@ID", ncid)
        dba.AddParameter("@UserIP", UserIP)
        dba.AddParameter("@timestamp", timestamp)
        dba.ExecuteNonQuery()
        actionString &= ":" & ncid.ToString
        RadAjaxManager1.ResponseScripts.Add("returnArg('" & actionString & "');")
    End Sub

    Private Sub cbCarrier_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbCarrier.SelectedIndexChanged
        Dim txt As String = e.Text
        If txt = "No Match Found" Then
            btnAddCarrier.Visible = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
            btnSaveCarrier.Visible = False
        Else
            btnAddCarrier.Visible = False
            btnSaveCarrier.Visible = True
        End If
    End Sub

    Private Sub btnCB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCB.Click
        Dim actionString As String = "CHENEY BROTHERS"
        actionString &= ":820D004D-B4D8-46D7-917B-7BBAC8D7D492"
        RadAjaxManager1.ResponseScripts.Add("returnArg('" & actionString & "');")
    End Sub

    Private Sub btnOD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOD.Click
        Dim actionString As String = "OD"
        actionString &= ":5D91F68D-C579-4062-ABDA-235F3011503E"
        RadAjaxManager1.ResponseScripts.Add("returnArg('" & actionString & "');")
    End Sub
End Class