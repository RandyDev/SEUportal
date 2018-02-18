<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="selectCarrier.aspx.vb" Inherits="DiversifiedLogistics.selectCarrier" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
<style type="text/css">
body{font-family:Arial;}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnCarrierSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnAddCarrier" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblCarriers" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="cbCarrier" UpdatePanelHeight="" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="btnSaveCarrier" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cbCarrier">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnAddCarrier" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="btnSaveCarrier" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />
<center>
<table>
<tr>
<td>
<telerik:RadTextBox ID="txtCarrier" runat="server" />
</td>
<td>
<asp:Button ID="btnAddCarrier" Text="Add NEW" runat="server" Visible="false" />
</td>
</tr>
<tr>
<td colspan="2">
<asp:Button ID="btnCarrierSearch" Text="Search Carriers" runat="server" />
</td>
</tr>
</table>

<br />
<br />
    <asp:Label ID="lblCarriers" runat="server" style="font-size:12px;" Visible="false" />
    <telerik:RadComboBox ID="cbCarrier" AutoPostBack="true" runat="server" Width="200px" Height="150px" EmptyMessage="Select Carrier" 
         Filter="Contains" AllowCustomText="true" Visible="false" />
    <asp:Button ID="btnSaveCarrier" runat="server" Visible="false" Text="Apply Selected" />
    <br />
<span style="font-size:11px;">Search Tips:<br />
2 - 4 characters finds carriers<br /> 'begining with' <br />
5 or more characters finds carriers<br /> 'containing' </span>
</center>
<fieldset style="padding:4px;"><legend style="font-size:11px;padding:4px;">Quick Links (Inbound/Backhaul ONLY)</legend>
<table align="center" cellpadding="0" cellspacing="0">
<tr><td align="center"><asp:Button ID="btnCB" Text="Cheney Brothers" runat="server" /></td></tr>
<tr><td align="center" style="padding-top:4px;"><asp:Button ID="btnOD" Text="O.D. Backhaul" runat="server" /></td></tr>
</table>

</fieldset>
<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
<script type="text/javascript">

    function OnClientDropDownClosed(sender, args) {
        //        sender.clearItems();
        if (args.get_domEvent().stopPropagation)
            args.get_domEvent().stopPropagation();
    }

    function pageLoad() {
        var currentWindow = GetRadWindow();
    }

    function GetRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)
        return oWindow;
    }

    function cancelAndClose() {
        var oWindow = GetRadWindow();
        oWindow.argument = null;
        oWindow.close();
    }

    function returnArg(arg) {
        var oWnd = GetRadWindow();
        oWnd.close(arg);
    }
    function decOnly(i) {
        var unicode = event.keyCode ? event.keyCode : event.charCode;
        if (unicode == 37 || unicode == 39) {    // ignore a left or right arrow press
            return
        }
         var t = i.value;
        if (t.length > 0) {
            t = t.replace(/[^\da-zA-Z&\s]+/g, '');
            i.value = t.toUpperCase();
        }
    }

</script>
</telerik:RadScriptBlock>


    </form>
</body>
</html>
