<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Reports.aspx.vb" Inherits="DiversifiedLogistics.Reports" %>


<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=11.2.17.1025, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
<style type="text/css">
#lblInstructions{font-size:15px; color:Green;text-align:center;}
#lblReportName{font-size:24px; text-align:center; color:#888888;} 
</style>
</head>
<body>
    <form id="form1" runat="server">
                <telerik:RadScriptManager ID="RadScriptManager1" runat="server"  AsyncPostBackTimeOut="600">
        </telerik:RadScriptManager>
        
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager> 
<telerik:RadSkinManager ID="radSkinManager1" EnableEmbeddedBaseStylesheet="true" EnableEmbeddedSkins="true" runat="server" />
    <div>
<%--    <telerik:RadGrid ID="RadGrid1" runat="server" /><asp:Button id="button1" runat="server" text="Mash Me" />--%>
    <table style="font-family:Arial; font-size:12px; text-align:center; width:95%;" >
        <tr>
            <td align="left">
                <table>
                    <tr>
                        <td><asp:Label ID="lblVendorNumber" Text="Vendor Number:<br />" runat="server" />
                            <asp:TextBox ID="txtVendorNumber" Width="100px" runat="server" />
                        </td>
                        <td><asp:Label ID="lblLocation" Text="Location:<br />" runat="server" />
                            <telerik:RadComboBox ID="cbLocations" runat="server" EmptyMessage="Select Location" />
                        </td>
                        <td><asp:Label ID="lblStartDate" Text="Start date:<br />" runat="server" />
                            <telerik:RadDatePicker ID="dpStartDate" runat="server" />
                        </td>
                        <td><asp:Label ID="lblEndDate" Text="End date:<br />" runat="server" />
                            <telerik:RadDatePicker ID="dpEndDate" runat="server" />
                        </td>
                        <td><asp:Label ID="lblDepartment" Text="Department (optional):<br />" ToolTip="Leave empty for ALL Departments" runat="server" />
                            <telerik:RadComboBox ID="cbDepartment" ToolTip="Leave empty ALL Departments" AllowCustomText="true" EmptyMessage="Select Department"  runat="server" />
                        </td>
                        <td valign="bottom" style="padding-left:8px;">
                            <asp:Button ID="btnRunReport" runat="server" Text="View Report" />
                        </td>
                        <td style="padding-left:25px;">
                            <asp:LinkButton ID="lbSwitch" runat="server" Text="Interactive Grid View" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblInstructions" Text="<br><br><br><center><font color=green>Please select parameters</font<br><center>For fastest result please limit date range to a week or less" runat="server" /><br />
                <asp:Label ID="lblReportName" runat="server" />
                <telerik:ReportViewer ID="ReportViewer1" Width="100%" Height="690px" runat="server"></telerik:ReportViewer>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>

           