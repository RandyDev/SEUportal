<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="FixDates.aspx.vb" Inherits="DiversifiedLogistics.FixDates" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeOut="3600">
        </telerik:RadScriptManager>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" EnableEmbeddedSkins="true" Skin="Simple" />
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="Button1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblCount" 
                    LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                <telerik:AjaxUpdatedControl ControlID="Button3" UpdatePanelHeight="" />
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelHeight="" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="Button2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblCount" 
                    LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                <telerik:AjaxUpdatedControl ControlID="Button3" UpdatePanelHeight="" />
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelHeight="" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="Button3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblCount" 
                    LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelHeight="" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
</telerik:RadAjaxLoadingPanel>
Work Order records with an Appointment, Gate, Arrival, Start or Comp time GREATER than: &nbsp;
    <telerik:RadDateInput ID="RadDateInput1" runat="server">
    </telerik:RadDateInput>
   
<asp:Button ID="Button1" runat="server" Text="Count em!" /> 
<asp:Button ID="Button2" runat="server" Text="Show em to me!" /> 

<hr /><hr />
<table><tr>
<td><asp:Label ID="lblCount" Text="---" runat="server" /></td>
<td style="padding-left:6px;">RESET YEAR to</td>
<td><telerik:RadTextBox ID="txtfYear" runat="server" Width="50px" Text="2010" /></td>
<td style="padding-left:4px;">... but only do it to <telerik:RadTextBox ID="txtfixCount" Width="50px" runat="server" Text="1000" /> records.</td>
<td style="padding-left:6px;"><asp:Button ID="Button3" runat="server" Text="Do It!" /> </td>
</tr></table>
 



    <telerik:RadGrid ID="RadGrid1" runat="server" EnableViewState="false">
    </telerik:RadGrid>
</form>
   </body>
   </html>