<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EditClientProfile.aspx.vb" Inherits="DiversifiedLogistics.EditClientProfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>   
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />
    <div>
    <br /><br /><br />
    <table align="center">
    <tr>
        <td>Edit Profile</td>
    </tr>
    <tr>
        <td>
<table align="left" style="border:1px solid black;">
    <tr>
        <td>
            <table>
                <tr>
                    <td>First Name:<br />
                        <telerik:RadTextBox ID="txtFname" Width="125px" runat="server" />
                    </td>
                    <td>Mi:<br />
                        <telerik:RadTextBox ID="txtMi" Width="25px" runat="server" />
                    </td>
                    <td>Last Name:<br />
                        <telerik:RadTextBox ID="txtLastName" Width="125px" runat="server" />
                    </td>
                    <td>Title:<br />
                        <telerik:RadTextBox ID="txtTitle" Width="125px" runat="server" />
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>eMail Address:<br />
                        <telerik:RadTextBox ID="txteMail" Width="175px" runat="server" />
                    </td>
                    <td>Phone Number:<br />
                        <telerik:RadTextBox ID="txtPhone" Width="159px" runat="server" />
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>Old Password:<br />
                        <telerik:RadTextBox ID="txtOldPassword" EmptyMessage="no change" Width="159px" runat="server" />
                    </td>
                    <td>New Password:<br />
                        <telerik:RadTextBox ID="txtPassword" EmptyMessage="no change" Width="159px" runat="server" />
                    </td>
                    <td align="right" valign="bottom"><asp:button ID="btnSubmit" Text="Submit" runat="server" />
                    
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
        </td>

    </tr>
    
    </table>
<br />
            <center><asp:Label ID="lblResult" runat="server" /></center>
    </div>
    </form>
</body>
</html>
