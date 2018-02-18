<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="seuFTPImportConfig.aspx.vb" Inherits="DiversifiedLogistics.seuFTPImportConfig" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
    <div>
<h2>seuFTPimporter Settings</h2>
        <table>
    <tr>
        <td>Start Folder</td>
        <td><telerik:RadTextBox ID="txtStartFolder" Width="350px" runat="server" /></td>
    </tr>
    <tr>
        <td>Primary Recipient</td>
        <td><telerik:RadTextBox ID="txteMail" Width="250px" runat="server" /></td>
    </tr>
    <tr>
        <td>CC Recipient(s)</td>
        <td><telerik:RadTextBox ID="txteMailCC" Width="350px" runat="server" /></td>
    </tr>
    <tr>
        <td>Mail Port</td>
        <td><telerik:RadNumericTextBox Width="40px" NumberFormat-DecimalDigits="0" ID="numMailPort" runat="server" >
<NegativeStyle Resize="None"></NegativeStyle>

<NumberFormat ZeroPattern="n" DecimalDigits="0"></NumberFormat>

<EmptyMessageStyle Resize="None"></EmptyMessageStyle>

<ReadOnlyStyle Resize="None"></ReadOnlyStyle>

<FocusedStyle Resize="None"></FocusedStyle>

<DisabledStyle Resize="None"></DisabledStyle>

<InvalidStyle Resize="None"></InvalidStyle>

<HoveredStyle Resize="None"></HoveredStyle>

<EnabledStyle Resize="None"></EnabledStyle>
            </telerik:RadNumericTextBox>
        </td>
    </tr>
    <tr>
        <td>Mail Server</td>
        <td><telerik:RadTextBox ID="txtMailServer" Width="250px" runat="server" /></td>
    </tr>
    <tr>
        <td>Mail AuthUserName</td>
        <td><telerik:RadTextBox ID="txtAuthUserName" Width="250px" runat="server" /></td>
    </tr>
    <tr>
        <td>Mail AuthPassword</td>
        <td><telerik:RadTextBox ID="txtAuthPassword" Width="250px" runat="server" /></td>
    </tr>
</table>
        
        <telerik:RadButton ID="btnSaveChanges" runat="server" Text="Save Changes" />
    </div>
    </form>
</body>
</html>
