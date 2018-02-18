<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ScheduledLoads.aspx.vb" Inherits="DiversifiedLogistics.ScheduledLoads" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body style="font-family:Arial;">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
<%--        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGO">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mdiv" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
--%>    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
<br /><br />

    <div id="mdiv" style="width:750px;text-align:center;margin:auto;" runat="server">
<table align="center" cellpadding="7">
<tr>
<td>
<br /><br />
<table>
    <tr>
        <td><telerik:RadComboBox ID="cbLocations" Width="150px" Filter="Contains" runat="server" /> 
        </td>

        <td align="right" style="padding-left:50px;">
        <span style="font-size:27px;color:#cccccc;">Just-In-Time : Import Scheduled Loads</span>
        </td>
    </tr>
</table>
<asp:PlaceHolder ID="ph1" runat="server" EnableViewState="false" />
</td></tr></table>
    </div>
    </form>
</body>
</html>
