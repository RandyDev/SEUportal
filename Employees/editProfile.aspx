<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="editProfile.aspx.vb" Inherits="DiversifiedLogistics.editProfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style type="text/css">
.botborder{
 border-bottom:1px solid #aaaaaa;
}

.phoneNumber{
font-size:14px;
}
.lblErr{
color:Red;
font-size:11px;
font-weight:bold;
}

body{
font-family:Arial;
}


.lilBlueButton{
font-size:11px;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnUpdateProfile">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnUpdateProfile" 
                        LoadingPanelID="RadAjaxLoadingPanel2" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="errProfileMsg" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnChangePassword">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnChangePassword" 
                        LoadingPanelID="RadAjaxLoadingPanel2" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="errMsg" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel Transparency="70" ID="RadAjaxLoadingPanel2"  runat="server">
    <asp:Image id="imgSmallLoaderImage" runat="server" style="border-style:none;" height="30px"  ImageUrl="~/images/ajax-loader-smallBar.gif"></asp:Image>
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />
    <div>
    <br />
    <br />
<center><span style="font-size:18px;">Profile Editor</span> &nbsp;<span style="font-size:10px;">v0.5</span></center>
    <br />
<table align="center">
    <tr>
        <td valign="top" style="padding-right:15px;">
            <table align="center" style="border-collapse:collapse;border:1px solid black;">

    <tr>
        <td align="center" colspan="4" style=" background-color:#cccccc; border-bottom:1px solid #888888;">
            Change Password 
        </td>
    </tr>
    <tr>
<td class="botborder" style="padding-right:5px;">step 1</td>
        <td class="botborder">Old Password:<br />
            <telerik:RadTextBox ID="txtOldpass" ToolTip="This response is CaSe SenSitIve" runat="server" />
        </td>
        <td class="botborder" valign="bottom"> OR </td>
        <td class="botborder"><asp:Label ID="lblQuestion" runat="server" /><br />
            <telerik:RadTextBox ID="txtAnswer" ToolTip="This response is CaSe SenSitIve" runat="server" />
        </td>
    </tr>
    <tr>
<td style="padding-right:5px;">step 2</td>
        <td>New Password:<br />
            <telerik:RadTextBox ID="txtNewpass" runat="server" ToolTip="This response is CaSe SenSitIve" />
        </td>
        <td colspan="2" style="text-align:center;"> <asp:Button ID="btnChangePassword" CommandName="ChangePassword" text="Change Password" runat="server" />
        </td>
    </tr>
</table>
<center><asp:Label ID="errMsg" CssClass="lblErr" runat="server" /></center>
        </td>
    <td valign="top">
<asp:Panel ID="pnlEmpContact" runat="server">
<table align="center" style="border-collapse:collapse;border:1px solid black;">
    <tr>
        <td align="center" colspan="2" 
            style=" background-color:#cccccc; border-bottom:1px solid #888888;">
            Contact Info 
        </td>
    </tr>
    <tr>
        <td colspan="2">
            eMail Address: <asp:Label ID="lblErrEmail" Text="*" CssClass="lblErr" runat="server" Visible="false" /><br />
            <telerik:RadTextBox ID="txtEmail" Width="225px" Runat="server">
            </telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td>
            Phone Number:<br />
            <telerik:RadMaskedTextBox CssClass="phoneNumber" ID="txtPhone" PromptChar="_" Width="100px" Mask="(###) ###-####" runat="server" />
        </td>
        <td>*Carrier: (*see note)<br />
            <telerik:RadComboBox ID="cbCellCarrier" EmptyMessage="Select Cell Carrier" runat="server" />
        </td>
    </tr>
    <tr>
        <td></td>
        <td> <asp:Button ID="btnUpdateProfile" text="Update Contact Info" runat="server" /></td>
    </tr>
</table><center><asp:Label ID="errProfileMsg" CssClass="lblErr" runat="server" /></center>
</asp:Panel>

    </td>
    
    
    </tr>
    
    </table>
    <table align="center"><tr><td>
<span style="font-size:11px;">    Currently the profile editor only allows you to change your password and, if you are an employee or supervisor, <br />change your eMail address and/or Phone number.<br />
        All other personnel contact the Administrator to set / update your eMail 
        address or phone number. </span>&nbsp;</td></tr></table>
    </div>
    </form>
</body>
</html>
