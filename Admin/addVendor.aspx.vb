Imports System.Data.SqlClient

Public Class addVendor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            ldal.setLocaCombo(puser, cbLocations)
            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
            'this whole one parent to one location is not very flexible
            'what if I want to become a consolidator???  hmmm???? didja ever think of that??
            lblParentCompany.Text = ldal.getParentNameByLocationID(cbLocations.SelectedValue)
            txtNumber.Attributes("onkeyup") = "decOnly(this);"
        End If
    End Sub

    Protected Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        Dim arg As String = e.Argument
        If arg.Contains("VendorLookup") Then
            Dim args() As String = Split(arg, ":")
            Dim locaID As Guid = New Guid(cbLocations.SelectedValue)
            Dim vnum As String = args(1).ToUpper
            If vnum.Length > 3 Then
                Dim dba As New DBAccess()
                dba.CommandText = "SELECT Vendor.ID AS VendorID, Vendor.Number, Vendor.Name AS VendorName " & _
                    "FROM Location INNER JOIN " & _
                    "Vendor ON Location.ParentCompanyID = Vendor.ParentCompanyID " & _
                    "WHERE (Location.ID = @locaID) AND (Vendor.Number = @vNum) "
                dba.AddParameter("@locaID", locaID)
                dba.AddParameter("@vNum", vnum)
                Dim reader As SqlDataReader = dba.ExecuteReader
                If reader.HasRows Then
                    reader.Read()
                    lblVendorName.Text = reader.Item(2)
                    txtVendorID.Value = reader.Item(0).ToString
                    btnSubmit.Text = "Update Vendor Name"
                    txtName.EmptyMessage = "Enter corrected spelling or rename vendor"
                    reader.Close()
                Else
                    lblVendorName.Text = "not found"
                    btnSubmit.Text = "Add New Vendor"
                    txtVendorID.Value = ""
                    txtName.EmptyMessage = "Vendor Name"
                End If
            Else
                If vnum.Length = 0 Then
                    lblVendorName.Text = ""
                    txtVendorID.Value = ""
                Else
                    txtVendorID.Value = ""
                    lblVendorName.Text = "not found"
                    btnSubmit.Text = "Add New Vendor"
                    txtName.EmptyMessage = "Vendor Name"
                End If
            End If

        End If
    End Sub

    Protected Sub getLocations()
        Dim ldal As New locaDAL()
        Dim dt As DataTable = ldal.getLocations()
        cbLocations.DataSource = dt
        cbLocations.DataTextField = "LocationName"
        cbLocations.DataValueField = "locaID"
        cbLocations.DataBind()
        cbLocations.ClearSelection()
    End Sub

    Protected Sub cbLocations_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        Dim ldal As New locaDAL()
        lblParentCompany.Text = ldal.getParentNameByLocationID(cbLocations.SelectedValue.ToString)
    End Sub

    Private Sub btnFindVendorName_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindVendorName.Click
        Dim pName As String = txtName.Text
        If pName.Length < 3 Then
            cbVendor.Visible = False
            btnSelectVendor.Visible = False
            lblFindMsg.Visible = True
            lblFindMsg.Text = "Please provide at least 3 characters"
            Exit Sub
        End If
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT Vendor.ID AS VendorID, Vendor.Number, Vendor.Number + ' - ' + Vendor.Name AS VendorName " & _
            "FROM Location INNER JOIN " & _
            "ParentCompany ON Location.ParentCompanyID = ParentCompany.ID INNER JOIN " & _
            "Vendor ON ParentCompany.ID = Vendor.ParentCompanyID " & _
            "WHERE (Location.ID = @locaID) AND Vendor.Name LIKE '%" & pName & "%'  " & _
            "ORDER BY Vendor.Name "
        dba.AddParameter("@locaID", cbLocations.SelectedValue)
        Dim ds As DataSet = dba.ExecuteDataSet
        Dim dt As DataTable = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            cbVendor.Visible = True
            btnSelectVendor.Visible = True
            cbVendor.DataSource = dt
            cbVendor.DataTextField = "VendorName"
            cbVendor.DataValueField = "VendorID"
            cbVendor.DataBind()
            cbVendor.ClearSelection()
            lblFindMsg.Visible = False
            lblFindMsg.Text = ""
        Else
            lblFindMsg.Visible = True
            lblFindMsg.Text = "Not Found"
            cbVendor.Visible = False
            btnSelectVendor.Visible = False
        End If

    End Sub

    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim actionString As String = String.Empty
        Dim dba As New DBAccess()
        Dim VendorNumber As String = txtNumber.Text.Trim()
        Dim VendorName As String = txtName.Text.Trim()
        Select Case btnSubmit.Text
            Case "Add New Vendor"
                If cbLocations.SelectedIndex < 0 Then
                    lblRequired.Text = "<font color='red'>Please Select Location</font>"
                    Exit Sub
                End If
                If VendorNumber.Length < 1 Then
                    If VendorName.Length < 1 Then
                        lblRequired.Text = "<font color='red'>Vendor Number and Name required</font>"
                    Else
                        lblRequired.Text = "<font color='red'>Vendor Number required</font>"
                    End If
                    Exit Sub
                Else
                    If VendorName.Length < 1 Then
                        lblRequired.Text = "<font color='red'>Vendor Name required</font>"
                        Exit Sub
                    End If
                End If


                Dim newVendorID As Guid = Guid.NewGuid()
                Dim ldal As New locaDAL()
                Dim newVendorParentCompanyID As Guid = ldal.getParentCompanyIDbyLocationID(cbLocations.SelectedValue)
                dba.CommandText = "INSERT INTO Vendor (ParentCompanyID, Number, Name, ID) VALUES (@ParentCompanyID, @Number, @Name, @ID)"
                dba.AddParameter("@ParentCompanyID", newVendorParentCompanyID)
                dba.AddParameter("@Number", VendorNumber)
                dba.AddParameter("@Name", VendorName)
                dba.AddParameter("@ID", newVendorID)
                dba.ExecuteNonQuery()
                actionString = "NewVendor:" + newVendorID.ToString & ":" & VendorNumber & ":" & VendorName
                RadAjaxManager1.ResponseScripts.Add("returnArg(""" & actionString & """);")
            Case "Update Vendor Name"
                dba.CommandText = "UPDATE Vendor SET Name=@vName WHERE ID=@vID"
                dba.AddParameter("@vName", VendorName)
                dba.AddParameter("@vID", txtVendorID.Value)
                dba.ExecuteNonQuery()
                actionString = "UpdateVendor:" & txtVendorID.Value & ":" & VendorNumber & ":" & VendorName
                RadAjaxManager1.ResponseScripts.Add("returnArg(""" & actionString & """);")
        End Select
    End Sub

    Private Sub btnSelectVendor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectVendor.Click
        Dim dba As New DBAccess()
        dba.CommandText = "Select Number, Name FROM Vendor WHERE ID = @vid"
        dba.AddParameter("@vid", cbVendor.SelectedValue)
        Dim reader As SqlDataReader = dba.ExecuteReader
        If reader.HasRows Then
            reader.Read()
            Dim VendorNumber As String = reader.Item(0)
            Dim VendorName As String = reader.Item(1)
            Dim actionString As String = String.Empty
            actionString = "SelectVendor:" & cbVendor.SelectedValue.ToString & ":" & VendorNumber & ":" & VendorName
            RadAjaxManager1.ResponseScripts.Add("returnArg(""" & actionString & """);")
        End If
    End Sub
End Class



