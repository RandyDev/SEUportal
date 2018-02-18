<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClearNoShowScheduledLoads.aspx.vb" Inherits="DiversifiedLogistics.ClearNoShowScheduledLoads" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="../styles/StyleSheet.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .style1
        {
            width: 397px;
        }
        body{font-family:Arial;}
        .style2
        {
            color: #FF0000;
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    Working ...
    </telerik:RadAjaxLoadingPanel>
    <div>
<br />
<table width="800" align="center">
    <tr>
        <td width="15%">&nbsp;
        </td>
        <td width="70%" align="center">
            <span style="font-size:27px;color:#cccccc;">Clear No-Show Scheduled Loads</span>
        </td>
        <td width="15%" align="right"><span onmouseover="this.style.cursor='help';" style="font-size:11px;color:Blue;"><asp:Label ID="lblHelp" Text="help with this page" runat="server" /></span>
        </td>
    </tr>
</table>
<br />
    <table align="center"><tr><td>
    <table>
        <tr>
            <td><asp:label id="lblcbLocation" Text="Location:" runat="server" /><br />
                <telerik:RadComboBox ID="cbLocation" runat="server" AutoPostBack="true" EmptyMessage="Select Location" />
            </td>
            <td style="padding-left:12px;"><asp:Label ID="lblcbDepartment" Text="Department:" runat="server" /><br />
                <telerik:RadComboBox ID="cbDepartment" runat="server" AutoPostBack="true" EmptyMessage="Select Department" />
            </td>
        </tr>
    </table>

        <telerik:RadGrid ID="RadGrid1" runat="server" CellSpacing="0" GridLines="None" Width="300px">
<MasterTableView AutoGenerateColumns="False" DataKeyNames="ID">

    <Columns>
        <telerik:GridBoundColumn DataField="ID" DataType="System.Guid" 
            FilterControlAltText="Filter ID column" HeaderText="ID" SortExpression="ID" 
            UniqueName="ID" Visible="false">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Status" DataType="System.Int32" 
            FilterControlAltText="Filter Status column" HeaderText="Status" 
            SortExpression="Status" UniqueName="Status" Visible="false">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="LogDate" DataType="System.DateTime" 
            FilterControlAltText="Filter LogDate column" HeaderText="Log Date" 
            SortExpression="LogDate" UniqueName="LogDate" Visible="false">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="LoadNumber" DataType="System.Int64" 
            FilterControlAltText="Filter LoadNumber column" HeaderText="Load Number" 
            SortExpression="LoadNumber" UniqueName="LoadNumber">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="PurchaseOrder" 
            FilterControlAltText="Filter PurchaseOrder column" HeaderText="PO#" 
            SortExpression="PurchaseOrder" UniqueName="PurchaseOrder">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="DoorNumber" 
            FilterControlAltText="Filter DoorNumber column" HeaderText="Door Number" 
            SortExpression="DoorNumber" UniqueName="DoorNumber">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="CreatedBy" 
            FilterControlAltText="Filter CreatedBy column" HeaderText="CreatedBy" 
            SortExpression="CreatedBy" UniqueName="CreatedBy" Visible="false">
        </telerik:GridBoundColumn>
    </Columns>

</MasterTableView>

<FilterMenu EnableImageSprites="False"></FilterMenu>
        </telerik:RadGrid>

        <asp:Label ID="lbldun" runat="server" Visible="false" />

</td><td style="padding-left:25px;" class="style1" valign="top">
<br />
<asp:Button ID="btnCloseEm" runat="server" Text="Close displayed loads!" Visible="false" />
 
<asp:Panel ID="pnlEndOfDayInstructions" Width="364px" Visible="false" 
            runat="server">
<br /><div style="text-align:center; font-family:arial; font-size:15px;" >
The <asp:Label ID="lblBillsLilCounter" runat="server" /> items at left have been closed.<br /><br />
    First, go perform &#39;End of Day RESET&#39;
    <br />
        on <span class="style2"><strong>any and all</strong></span> PDAs logged in to 
        this department.<br />Do that now!<br />
        <br />
        THEN ... <u>and this is IMPORTANT</u>!<br /><br />
<b><u><span style="font-size:18px;">AFTER</span></u></b> you have closed out your PDA<br />Click the button below.<br /><br />
<asp:Button ID="btnDeleteLoads" Text="Commit/Remove No Show Loads" runat="server" />
</div>
</asp:Panel>
<asp:Panel ID="pnlDONE" runat="server" Width="364px" Visible="false">
<center>
<br /><br /><br />Thanks!<br />
Yer done. <br />
Have a most pleasant day.
</center>

</asp:Panel>


</td>
</tr></table> 
<telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="lblHelp" RelativeTo="BrowserWindow"
    ShowEvent="OnClick" Position="Center" HideEvent="ManualClose"  Animation="Resize" EnableShadow="true">
<table cellpadding="0" cellspacing="0" width="100%"><tr>
<td><span class="ttHeader">How to clear pre-loaded no shows.</span>
</td>
<td align="right" style="padding-right:12px;">
</td>
</tr></table>

<table><tr><td style="padding:0 8px;">
<table>
    <tr>
        <td class="ttTitle">Use FULL Screen</td>
    </tr>
    <tr>
        <td class="ttBody">
            The F11 key on your keyboard will toggle most browsers to full screen.<br />
            F11 again to toggle back. &nbsp;Try it now!&nbsp; &nbsp;(MSIE and Chrome tested)
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Select your location and department</td>
    </tr>
    <tr>
        <td  class="ttBody">
        <b>Select a location</b>, if not already pre-selected, using the Location: dropdown box.<br />
        Once a location is selected, the Department: dropdown box will populate and display a list of departments <br />
        where there are loads that have not been closed.  &nbsp;If your department is not listed, then all of your imported <br />
        and/or pre-loaded work orders (loads) are closed and you don't need this tool today. &nbsp;You should be able to close<br />
        out (End of Day RESET) your PDA(s) as you normally would. ... otherwise ...<br />
        <b>Select your department</b> and after a moment you will see a list of loads that should match the un-closed loads on your PDA(s)<br />


        </td>
    </tr>
    <tr>
        <td class="ttTitle">Confirm/Verify and Close no-show loads</td>
    </tr>
    <tr>
        <td  class="ttBody">
        When the list of no-show loads appears, to the right there will appear a new button that will close the loads for you.<br />
        Note that the button shows a count that should match the number of un-closed loads on your PDA(s).<br />
        Once you see that the list (and count) shown on the screen match the un-closed loads on your device, click the button.
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Perform 'End of Day RESET' on PDA(s)</td>
    </tr>
    <tr>
        <td  class="ttBody">
        After closing the loads you will be instructed to perform 'End of Day RESET' on all PDAs logged in to the selected department.<br />
        If you have multiple devices logged on to this same department, make sure all of them have been closed out.
        </td>
    </tr>
    <tr>
        <td class="ttTitle">REMOVE no-show Loads</td>
    </tr>
    <tr>
        <td  class="ttBody">
AFTER having closed out your PDA(s), click the 'Commit/Remove No Show Loads' button. <br />
After a moment the page will refresh and show you a list of removed loads.
        </td>
    </tr>


</table><br />
<center>To Close - Click X in upper right corner</center>


</td></tr></table>

</telerik:RadToolTip>

 <br />
    </div>
    </form>
</body>
</html>
