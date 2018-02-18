<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RequestTravelers.aspx.vb" Inherits="DiversifiedLogistics.RequestTravelers" %>

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
    <div>
    <table>
        <tr>
            <td>Travel To:<br />
                <telerik:RadComboBox ID="cbDLocation" EmptyMessage="To Be Announced" runat="server" />
            </td>
            <td>Start Date:<br />
                <telerik:RadDatePicker ID="dpStartDate" runat="server" />
            </td>
            <td>Return Date:<br />
                <telerik:RadDatePicker ID="dbReturnDate" runat="server" />
            </td>
            <td>
                <asp:Button ID="btnSubmit" CommandName="requestTravelers" runat="server" Text="Submit" />
            </td>
        </tr>
    </table>
    <asp:PlaceHolder ID="phLocaList" runat="server" />
    </div>
    </form>
</body>
</html>
