<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="addVendor.aspx.vb" Inherits="DiversifiedLogistics.addVendor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblVendorName" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtName" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="btnSubmit" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtVendorID" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cbLocations">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblParentCompany" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnSubmit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblRequired" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnFindVendorName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnFindVendorName" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblFindMsg" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="cbVendor" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="btnSelectVendor" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div>
<asp:Label ID="lblLocaID" runat="server" />

<table><tr>
<td><telerik:RadComboBox ID="cbLocations" Filter="Contains" runat="server" /> :</td>
<td style="padding-left:10px;"><asp:Label ID="lblParentCompany" runat="server" />
</td>

</tr></table>

<table cellpadding="0" cellspacing="0">
    <tr>
        <td width="10%">
            <telerik:RadTextBox ID="txtNumber" runat="server" Width="90px" EmptyMessage="Vendor Number" /></td>
        <td style="padding-left:10px;"><asp:Label ID="lblVendorName" runat="server" /></td>
    </tr>
</table>
<table>
    <tr>
        <td colspan="2">
<table cellpadding="0" cellspacing="0"><tr><td>
               <telerik:RadTextBox ID="txtName" runat="server" Width="225px" EmptyMessage="Vendor Name" />
        </td>
        <td>
            <asp:Button ID="btnSubmit" runat="server" CausesValidation="true" Text="Add New Vendor" /><input type="hidden" id="txtVendorID" runat="server" />
            </td></tr></table>
        </td>
    </tr>
</table>
<asp:Label ID="lblRequired" runat="server" />

<hr />
<table><tr>
<td>
<span style="font-family:Arial;font-size:10px;">... or, enter partial spelling above and ...</span>
    <br />
    <asp:Button ID="btnFindVendorName" runat="server" Text="Find By Name" /></td>
<td><asp:Label ID="lblFindMsg" runat="server" /></td>
</tr></table>
<telerik:RadComboBox ID="cbVendor" runat="server" EmptyMessage="Select Vendor"  visible="false"
        Filter="Contains" Height="260px" Width="345px" AllowCustomText="true" /><br />
        <asp:Button ID="btnSelectVendor" Text="Use Selected Vendor" runat="server" visible="false" />
    </div>
<telerik:RadCodeBlock ID="cb1" runat="server">
<script type="text/javascript">
    function decOnly(i) {
        var t = i.value;
        if (t.length > 0) {
            t = t.replace(/[^\da-zA-Z]+/g, '');
            i.value = t;
        }
        var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
        ajaxManager.ajaxRequest("VendorLookup:" + t);
    }
    </script>
</telerik:RadCodeBlock>
<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
<script type="text/javascript">
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
    

</script>
</telerik:RadScriptBlock>    
    </form>
</body>
</html>
