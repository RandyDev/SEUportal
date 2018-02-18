<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="showWO.aspx.vb" Inherits="DiversifiedLogistics.showWO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href='../Styles/master.css' type="text/css" media="screen" />
    <link rel="SHORTCUT ICON" href="../Images/favicon.ico"/>
    <title>SEU Load Viewer</title>
<style type="text/css">
.data{ font-size:14px;font-weight:bold;}
.lbl{font-size:12px;font-weight:normal;}
.lbl td{padding-left:24px;}
.ColorMeRed {color:Red;}
.tw {width:470px;}
.cb {font-size:11px;font-family:arial;font-weight:normal;}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblUnloadersV" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel Transparency="50" ID="RadAjaxLoadingPanel1"  runat="server">
        <asp:Image id="Image1" runat="server" Width="110" Height="21" ImageUrl="~/images/forkliftani.gif"></asp:Image>
    </telerik:RadAjaxLoadingPanel>

<table class="tw" cellpadding="0" cellspacing="0"><tr>
<td><b><asp:Label ID="locaLabel" Text = "" runat="server" /></b></td>
<td align="right" style="padding-right:4px;"><asp:Label ID="lblCB" runat="server" class="cb" /></td>
</tr></table>

<div class="lbl" style="border:1px solid black;width:470px;">
<table><tr>
<td>Date worked:<br /><asp:Label class="data" ID="lblDateWorked" runat="server" />
</td>
<td>Door:<br /> <asp:Label class="data" ID="lblDoorNumber" runat="server" />
</td>
<td>Amount:<br /> <asp:Label class="data" ID="lblAmount" runat="server" />
</td>
<td>Cash:<br /> <asp:Label class="data" ID="lblIsCash" runat="server" />
</td>
<td>Department:<br /> <asp:Label class="data" ID="lblDepartment" runat="server" />
</td>
</tr></table>
<table>
<tr>
<td>Carrier:<br />
<asp:Label class="data" ID="lblCarrierName" runat="server" />
</td>
<td>Truck #: &nbsp; <br /><asp:Label class="data" ID="lblTruckNumber" runat="server" /></td>
<td>Trailer #: &nbsp; <br /><asp:Label class="data" ID="lblTrailerNumber" runat="server" /></td>
<td>Purchase Order: &nbsp; <br /><asp:Label class="data" ID="lblPurchaseOrder" runat="server" /></td>
</tr>
</table>
<hr />
<table>
<tr>
<td colspan="3">Vendor number:<br /> <asp:Label class="data" ID="lblVendorNumber" runat="server" />
&nbsp; &nbsp; <asp:Label class="data" ID="lblVendorName" runat="server" />
</td>
</tr>
<tr>
<td>Pieces:<br /> <asp:Label class="data" ID="lblPieces" runat="server" /></td>
<td>Pallets Received:<br /> <asp:Label class="data" ID="lblPalletsReceived" runat="server" /></td>
<td>Load Description:<br /> <asp:Label class="data" ID="lblLoadDescription" runat="server" /></td>
</tr>
<tr>
    <td colspan="3">
Assigned employees: &nbsp; &nbsp;<asp:Label ID="lblEditUnloaders" runat="server" /> <br /> 
<asp:Label class="data" ID="lblUnloadersV" runat="server" />
    </td>
</tr>
</table>
<hr />
<table>
<tr>
<td>Pallets Unloaded: <br /><asp:Label class="data" ID="lblPalletsUnloaded" runat="server" /></td>
<td>App time: <br /><asp:Label class="data" ID="lblAppTime" runat="server" /></td>
<td>Gate Time: <br /><asp:Label class="data" ID="lblGateTime" runat="server" /></td>
<td>Arrival Time: <br /><asp:Label class="data" ID="lblArrivalTime" runat="server" /></td>
</tr>
</table>
<table>
<tr>
<td>Start Time: <br /><asp:Label class="data" ID="lblStartTime" runat="server" /></td>
<td>Completion Time: <br /><asp:Label class="data" ID="lblCompTime" runat="server" /></td>
<td>Total work time (calculated): <br /><asp:Label class="data" ID="lblTotalTime" runat="server" /></td>
</tr>
</table>
<table>
<tr>
<td>Bad Pallets: <br /><asp:Label class="data" ID="lblBadPallets" Width="25px" runat="server" /></td>
<td>Weight: <br /><asp:Label class="data" ID="lblWeight" Width="25px" runat="server" /></td>
<td>Restacks: <br /><asp:Label class="data" ID="lblRestacks" Width="25px" runat="server" /></td>
<td>Total items: <br /><asp:Label class="data" ID="lblTotalItems" Width="25px" runat="server" /></td>
<td>Load Type: &nbsp; &nbsp;<asp:LinkButton ID="lbtnEditLoadType" text="Change" runat="server" /> <br /><asp:Label class="data" ID="lblLoadType" runat="server" /><telerik:RadComboBox ID="cbLoadType" EmptyMessage="Select Load Type" runat="server" visible="false" /></td>
</tr>
</table>
<hr />
<table><tr>
<td>Check #: <asp:Label class="data" ID="lblCheckNumber" runat="server" /></td>
<td>BOL: <asp:Label class="data" ID="lblBOL" runat="server" /></td>
</tr>
<tr>
<td colspan="2">
Comments: <br /><asp:Label class="data" ID="lblComments" runat="server" />
</td>
</tr>
</table>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" VisibleStatusbar="false" >
    <Windows>
        <telerik:RadWindow ID="wUnloader" Height="260px" Width="410px"  
            ShowContentDuringLoad="true"  runat="server" Modal="True" 
            Title="Select Unloaders"
            OffsetElementID="<%=lblCarrierNamev.ClientID%>" 
            Left="400"
            Top="-100"
            OnClientClose = "OnClientCloseUnloader"
            Behaviors="Move, Close" />
    </Windows>
</telerik:RadWindowManager>
</div>
<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
<script type="text/javascript">
    function openUnloaders(arg) {
        var loca = "selectUnloaders.aspx?woid=" + arg;
        var oManager = GetRadWindowManager();
        oManager.open(loca, "wUnloader");
    }

    function OnClientCloseUnloader(sender, args) {
        if (args.get_argument() != null) {
            var arg = args.get_argument();
            var Unloaders = document.getElementById("<%= lblUnloadersV.ClientID %>")
            Unloaders.innerText = arg.split("|", 2)[0];
            Unloaders.style.color = 'blue';
            Unloaders.style.fontSize = '12px';
            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
            ajaxManager.ajaxRequest("Unloader:" + arg);

        }
    }
</script>
</telerik:RadCodeBlock>
    </form>
</body>
</html>
