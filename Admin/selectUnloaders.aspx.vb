Imports Telerik.Web.UI

Partial Public Class selectUnloaders
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim woid As String = Request("woid")
            Dim loca As Boolean = Request("gtype") = "locaID"
            If loca Then
                Dim edal As New empDAL()
                Dim dt As DataTable = edal.getUnloadersByLocation(Guid.Parse(woid))
                lbAvailableUnloaders.DataSource = dt
                lbAvailableUnloaders.DataValueField = "ID"
                lbAvailableUnloaders.DataTextField = "Name"
                lbAvailableUnloaders.DataBind()
            Else
                populateUnloaders(woid)
                populateAvailableLoaders(woid)
            End If
        End If
    End Sub

    Protected Sub populateUnloaders(ByVal woid As String)
        Dim edal As New empDAL()
        Dim dt As DataTable = edal.GetUnloadersByWOID(woid)
        lbUnloaderList.DataSource = dt
        lbUnloaderList.DataValueField = "ID"
        lbUnloaderList.DataTextField = "Name"
        lbUnloaderList.DataBind()
    End Sub

    Protected Sub populateAvailableLoaders(ByVal woid As String)
        Dim dba As New DBAccess()
        dba.CommandText = "Select LocationID FROM WorkOrder WHERE ID = @woid"
        dba.AddParameter("@woid", woid)
        Dim locaID As Guid = dba.ExecuteScalar
        Dim edal As New empDAL()
        Dim dt As DataTable = edal.getUnloadersByLocation(locaID)
        lbAvailableUnloaders.DataSource = dt
        lbAvailableUnloaders.DataValueField = "ID"
        lbAvailableUnloaders.DataTextField = "Name"
        lbAvailableUnloaders.DataBind()
        Dim uloaders As RadListBoxItemCollection = lbUnloaderList.Items
        For Each item As RadListBoxItem In uloaders
            Dim itemToRemove As RadListBoxItem = lbAvailableUnloaders.FindItemByText(item.Text)
            lbAvailableUnloaders.Items.Remove(itemToRemove)
        Next


    End Sub


    Protected Sub btnApplyUnloaders_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApplyUnloaders.Click
        Dim ulc As RadListBoxItemCollection
        ulc = lbUnloaderList.Items
        Dim actionString As String = String.Empty
        Dim empList As String = String.Empty
        For Each it As RadListBoxItem In ulc
            actionString &= Replace(it.Text, "'", "*") & " - "
            empList &= it.Value & ":"
        Next
        If actionString.Length > 3 Then
            actionString = Left(actionString, Len(actionString) - 3)
            empList = Left(empList, Len(empList) - 1)
            actionString &= "|" & empList
        Else
            actionString = "none listed"
        End If
        RadAjaxManager1.ResponseScripts.Add("returnArg('" & actionString & "');")
    End Sub
End Class