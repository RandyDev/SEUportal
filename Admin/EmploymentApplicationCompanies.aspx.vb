Imports Telerik.Web.UI

Public Class EmploymentApplicationCompanies
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            loadcompanies()
        End If
    End Sub
    Private Sub loadcompanies()
        cbemploymentcompanies.Items.Clear()
        Dim dba As New DBAccess
        dba.CommandText = "SELECT companyID, CompanyName FROM employmentApplicationCompanies"
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        cbemploymentcompanies.DataSource = dt
        cbemploymentcompanies.DataTextField = "CompanyName"
        cbemploymentcompanies.DataValueField = "companyID"
        cbemploymentcompanies.DataBind()
        cbemploymentcompanies.Text = ""
        cbemploymentcompanies.EmptyMessage = "Select Company"
    End Sub

    Private Sub btnNewCompany_Click(sender As Object, e As EventArgs) Handles btnNewCompany.Click
        Dim ecompany As String = txtCompanyName.Text.Trim
        Dim ecompanyabr As String = "---"
        Dim ecompanyid As Guid = Guid.NewGuid()
        Dim dba As New DBAccess
        dba.CommandText = "INSERT INTO employmentApplicationCompanies (companyID,companyName,companyABR) values(@companyID,@companyName,@companyABR)"
        dba.AddParameter("@companyID", ecompanyid)
        dba.AddParameter("@companyName", ecompany)
        dba.AddParameter("@companyABR", ecompanyabr)
        dba.ExecuteNonQuery()
        loadcompanies()
        txtCompanyName.Text = ""
        lblcompanySaved.Visible = True
    End Sub

    Private Sub cbemploymentcompanies_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbemploymentcompanies.SelectedIndexChanged
        lblcompanySaved.Visible = False
        RadGrid1.Rebind()
    End Sub

    Private Sub RadGrid1_DeleteCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.DeleteCommand
        If TypeOf e.Item Is GridDataItem Then
            Dim itm As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim delitem As Guid = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("companyLocationID")
            Dim ldal As New locaDAL()
            Dim dba As New DBAccess
            dba.CommandText = "DELETE FROM employmentApplicationLocations WHERE companyLocationID=@companyLocationID"
            dba.AddParameter("@companyLocationID", delitem)
            dba.ExecuteNonQuery()
        End If
    End Sub

    Private Sub RadGrid1_InsertCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid1.InsertCommand
        Dim item As GridEditableItem = TryCast(e.Item, GridEditableItem)
        Dim values As New Hashtable()
        item.ExtractValues(values)
        Dim companyLocationID As Guid = Guid.NewGuid()
        Dim companyID As String = cbemploymentcompanies.SelectedValue
        Dim companyLocaName As String = IIf(values.Item("companyLocaName") Is Nothing, "", values.Item("companyLocaName"))
        Dim companyLocaAddress As String = IIf(values.Item("companyLocaAddress") Is Nothing, "", values.Item("companyLocaAddress"))
        Dim companyLocaCity As String = values.Item("companyLocaCity")
        Dim companyLocaState As String = values.Item("companyLocaState")
        Dim companyLocaZip As String = IIf(values.Item("companyLocaZip") Is Nothing, "", values.Item("companyLocaZip"))

        Dim dba As New DBAccess()
        dba.CommandText = "INSERT INTO employmentApplicationLocations (companyLocationID,companyID,companyLocaName,companyLocaAddress,companyLocaCity,companyLocaState,companyLocaZip) " & _
              "VALUES (@companyLocationID,@companyID,@companyLocaName,@companyLocaAddress,@companyLocaCity,@companyLocaState,@companyLocaZip)"
        dba.AddParameter("@companyLocationID", companyLocationID.ToString.Trim)
        dba.AddParameter("@companyID", companyID.ToString.Trim)
        dba.AddParameter("@companyLocaName", companyLocaName.ToString.Trim)
        dba.AddParameter("@companyLocaAddress", companyLocaAddress.ToString.Trim)
        dba.AddParameter("@companyLocaCity", companyLocaCity.ToString.Trim)
        dba.AddParameter("@companyLocaState", companyLocaState.ToString.Trim)
        dba.AddParameter("@companyLocaZip", companyLocaZip.ToString.Trim)
        dba.ExecuteNonQuery()
    End Sub

    Private Sub RadGrid1_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim companyID As String = Utilities.zeroGuid.ToString
        If cbemploymentcompanies.SelectedIndex > -1 Then
            companyID = cbemploymentcompanies.SelectedValue
            Dim dba As New DBAccess
            dba.CommandText = "SELECT companyLocationID,companyID, companyLocaName,companyLocaAddress,companyLocaCity,companyLocaState, companyLocaZip FROM employmentApplicationLocations WHERE companyID=@companyID"
            dba.AddParameter("@companyID", companyID)
            Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
            RadGrid1.DataSource = dt
        End If
    End Sub

    Private Sub btnDelCompany_Click(sender As Object, e As EventArgs) Handles btnDelCompany.Click
        Dim delme As String = cbemploymentcompanies.SelectedValue
        Dim dba As New DBAccess
        dba.CommandText = "DELETE FROM employmentApplicationCompanies Where companyID=@companyID"
        dba.AddParameter("@companyID", delme)
        dba.ExecuteNonQuery()
        dba.CommandText = "DELETE FROM employmentApplicationLocations Where companyID=@companyID"
        dba.AddParameter("@companyID", delme)
        dba.ExecuteNonQuery()
        RadGrid1.Rebind()
        loadcompanies()
    End Sub
End Class