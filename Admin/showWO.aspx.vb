Public Class showWO
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblEditUnloaders.Visible = Request("edit") = "editUnloader"
        If lblEditUnloaders.Visible Then
            lblEditUnloaders.Text = "<span style=""font-size:11px;color:blue"" onmouseover=""this.style.cursor='pointer'"" onclick=""openUnloaders('" & Request("q") & "');"">Change</span>"
        End If
        lbtnEditLoadType.Visible = Request("edit") = "editLoadType"


        Dim woid As String = Request("q")
        '        Dim woid As String = RadGrid1.SelectedValue.ToString
        Dim wodal As New WorkOrderDAL()
        Dim edal As New empDAL()
        Dim wo As WorkOrder = wodal.GetLoadByID(woid)
        lblDateWorked.Text = wo.LogDate
        lblDoorNumber.Text = wo.DoorNumber
        lblAmount.Text = FormatCurrency(wo.Amount, 2)
        lblIsCash.Text = IIf(wo.IsCash, "<span style=""color:green;"">YES</span>", "No")
        lblDepartment.Text = wo.Department
        If wo.CarrierName.ToUpper.IndexOf("SEE COMMENTS") <> -1 Then
            lblCarrierName.Text = "<span class=""ColorMeRed"">" & wo.CarrierName & "</span>"
        Else
            lblCarrierName.Text = wo.CarrierName
        End If
        lblTruckNumber.Text = wo.TruckNumber
        lblTrailerNumber.Text = wo.TrailerNumber
        lblPurchaseOrder.Text = wo.PurchaseOrder
        lblVendorNumber.Text = wo.VendorNumber
        lblVendorName.Text = wo.VendorName
        lblUnloadersV.Text = edal.GetUnloadersByWOIDString(woid)
        lblPieces.Text = wo.Pieces
        lblPalletsReceived.Text = wo.PalletsReceived
        lblLoadDescription.Text = wo.LoadDescription
        lblPalletsUnloaded.Text = wo.PalletsUnloaded
        lblAppTime.Text = Format(wo.AppointmentTime, "hh:mm tt")
        lblGateTime.Text = Format(wo.GateTime, "hh:mm tt")
        lblArrivalTime.Text = Format(wo.DockTime, "hh:mm tt")
        lblStartTime.Text = Format(wo.StartTime, "hh:mm tt")
        lblCompTime.Text = Format(wo.CompTime, "hh:mm tt")
        Dim totaltime As Integer = DateDiff(DateInterval.Minute, wo.StartTime, wo.CompTime)
        If totaltime > 120 Then
            lblTotalTime.Text = "<span style=""color:red;"">" & totaltime & " minutes</span>"
        ElseIf totaltime < 0 Then
            lblTotalTime.Text = ""
        Else
            lblTotalTime.Text = totaltime & " minutes</span>"
        End If

        lblBadPallets.Text = wo.BadPallets
        lblWeight.Text = wo.Weight
        lblRestacks.Text = wo.Restacks
        lblTotalItems.Text = wo.NumberOfItems
        lblLoadType.Text = wo.LoadType
        lblCheckNumber.Text = wo.CheckNumber
        lblBOL.Text = wo.BOL
        lblComments.Text = wo.Comments
        lblCB.Text = IIf(wo.CreatedBy.Contains(":"), wo.CreatedBy, "rtdsHandHeld")
        Dim locadal As New locaDAL
        locaLabel.Text = locadal.getLocationNameByID(wo.LocationID.ToString)
    End Sub


    Private Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        Dim arg As String = e.Argument
        Dim sarg() As String
        If arg.Contains("Unloader") Then
            sarg = Split(arg, "|")
            If sarg(0) = "Unloader:none listed" Then
                'remove unloaders
                '                txtUnloaderIDlist.Text = "listcleared"
            Else
                'save unloaders
                Dim woid As String = Request("q")
                If Utilities.IsValidGuid(woid) Then
                    Dim wdal As New WorkOrderDAL()
                    Dim newWorkOrder As WorkOrder = wdal.GetLoadByID(woid)
                    Dim elst As List(Of String) = New List(Of String)
                    Dim ulList() As String = Split(sarg(1), ":")
                    Dim i As Integer = ulList.Length
                    For x = 0 To i - 1
                        Dim emp As String = String.Empty
                        emp = ulList(x)
                        elst.Add(emp)
                    Next
                    newWorkOrder.Employee = elst
                    '                    wdal.UpdateWorkOrder(newWorkOrder)
                    If Request("edit") = "edit" And User.IsInRole("SysOp") Then
                        ' with permission
                        'wdal.UpdateAuditUnloaderChanges(newWorkOrder)
                        'Dim eml As New DivLogMail
                        'eml.To = "wwalklett@div-log.com"
                        'eml.Subject = "Changes made from Trouble-Shooter tool"
                        'Dim str As String = String.Empty
                        'str = User.Identity.Name & " changed the unloader list on" & vbCrLf
                        'str &= "PO #: " & newWorkOrder.PurchaseOrder & vbCrLf
                        'eml.Body = str
                        'eml.SendMail(eml)
                    End If
                    lblUnloadersV.Text = Right(sarg(0), Len(sarg(0)) - 9) & "&nbsp; &nbsp; &nbsp;<font color='red'>changes saved</font>"
                End If
            End If
        End If
    End Sub
End Class

