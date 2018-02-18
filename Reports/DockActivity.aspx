<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="DockActivity.aspx.vb" Inherits="DiversifiedLogistics.DockActivity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Current Dock Activity Report</title>
    <link href="../styles/StyleSheet.css" rel="stylesheet" type="text/css" />
<telerik:RadScriptBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
    </script>
</telerik:RadScriptBlock>

<style type="text/css">
</style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" EnableEmbeddedSkins="true" Skin="Simple" />
    <div>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" 
        runat="server" Skin="Default" >
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel Transparency="50" ID="RadAjaxLoadingPanel2"  runat="server">
        <asp:Image id="Image1" runat="server" Width="110" Height="18" ImageUrl="~/images/ajax-loader-smallBar.gif"></asp:Image>
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager DefaultLoadingPanelID="RadAjaxLoadingPanel1" ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cbLocations">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblTimeNow" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="Panel2" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="Panel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="thuTime">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblTimeNow" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="Panel2" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadioButtonList1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblTimeNow" LoadingPanelID="RadAjaxLoadingPanel2" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="Panel2" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnRefresh">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblTimeNow" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="Panel2" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkHideCompleted">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblTimeNow" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="chkHideCompleted" />
                    <telerik:AjaxUpdatedControl ControlID="lblHideCompletedStatus" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="Panel2" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="Panel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
<table style="font-size:12px;">
    <tr>
        <td >Location: </td><td>
            <asp:Label ID="lblLocationName" runat="server" />
            <telerik:RadComboBox ID="cbLocations" runat="server" Visible="true" Filter="Contains" 
                AutoPostBack="true" AllowCustomText="true" EmptyMessage="Select Location" />
            <br />
            <span class="smallTitle" onmouseover="this.style.cursor='help';" title="Time of Day @ this location"><asp:Label ID="lblTimeNow" runat="server" /></span>
        </td>
        <td style="padding-left:25px;">Refresh Rate:</td>
        <td><asp:Timer id="thuTime" Interval="300000" runat="server"></asp:Timer>
            <asp:RadioButtonList ID="RadioButtonList1" AutoPostBack="true" RepeatDirection="Horizontal" runat="server">
                <asp:ListItem Value="1" Text="1 Minute" />
                <asp:ListItem Value="5" Text="5 Minutes" Selected="True" />
                <asp:ListItem Value="10" Text="10 Minutes" />
                <asp:ListItem Value="15" Text="15 Minutes" />
            </asp:RadioButtonList>
        </td>
        <td style="padding-left:25px;">
            <asp:Button Text="Refresh Now" ID="btnRefresh" UseSubmitBehavior="false" runat="server" />
        </td>
        <td style="padding-left:25px;">
            <table class="smallTitle" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2"><asp:CheckBox ID="chkHideCompleted" Text="Hide Completed Loads" ToolTip="UNcheck this box to show ALL loads" Checked="true" runat="server" AutoPostBack="true" />
                    </td>
                </tr>
                <tr>
                    <td style="padding-right:3px;">'completed' loads showing:</td>
                    <td><asp:Label ID="lblHideCompletedStatus" Text="NO" runat="server" /></td>
                </tr>
            </table>
        </td>
        <td style="padding-left:25px;">
            <span class="smallTitle">Running Totals:&nbsp; &nbsp;<span  onmouseover="this.style.cursor='help';"> &nbsp; &nbsp;<asp:Label ID="lblHelp" Font-Underline="true" ForeColor="Blue" runat="server" Text="KEY" /> </span></span><br />
<asp:Panel ID="Panel2" runat="server">

           <table style="border-collapse:collapse;border:1px solid black;font-size:14px;" border="1" cellspacing="0">
                <tr onmouseover="this.style.cursor='arrow';">
                    <td align="center" title="Currently Working" style="padding:0 4px 0 4px;"><asp:Label ID="lblWorking" Text="--" runat="server" /> </td>
                    <td align="center" title="Approaching 2 hours" style="padding:0 4px 0 4px;background-color:#FFAA00;"><asp:Label ID="lblWarning" Text="--" runat="server" /></td>
                    <td align="center" title="Over 2 hours" style="color:Red;padding:0 4px 0 4px;"><asp:Label ID="lblTwoPlus" Text="--" runat="server" /></td>
                    <td align="center" title="Completed - Over 2 hours" style="padding:0 4px 0 4px;color:Red;background-color:#BCCCB4;"><asp:Label ID="lblTwoPlusComplete" Text="--" runat="server" /></td>
                    <td align="center" title="Completed On Time" style="padding:0 4px 0 4px;background-color:#BCCCB4;"><asp:Label ID="lblComplete" Text="--" runat="server" /></td>
                    <td align="center" style="padding:0 4px 0 4px;"title="Total Loads (Currently Working + Completed)"><asp:Label ID="lblTotalLoads" Text="--" runat="server" /></td>
                </tr>
            </table>
</asp:Panel>
         </td>
         <td style="padding-left:25px;"><span onmouseover="this.style.cursor='help';"><asp:Label ID="lblTips" runat="server" ForeColor="Blue" Font-Underline="true" Text="Help with this page" /></span> </td>
    </tr>
</table>

<asp:Panel ID="Panel1" runat="server" Visible="false">

<table cellspacing="0" style="width: 100%; border-collapse:collapse;" border="1">
    <thead>
        <tr class="rlvHeader">
            <th>Dr #</th>
            <th>Vendor</th>
            <th>PO #</th>
            <th>Carrier</th>
            <th>Trailer</th>
            <th>Appt</th>
            <th>Dock</th>
            <th>Start</th>
            <th>Finish</th>
            <th>Department</th>
            <th>LoadType</th>
            <th>Unloaders</th>
         </tr>
    </thead>
    <tfoot>
        <tr class="rlvHeader">
            <th>Dr #</th>
            <th>Vendor</th>
            <th>PO #</th>
            <th>Carrier</th>
            <th>Trailer</th>
            <th>Appt</th>
            <th>Dock</th>
            <th>Start</th>
            <th>Finish</th>
            <th>Department</th>
            <th>LoadType</th>
            <th>Unloaders</th>
        </tr>
    </tfoot>
    <tbody style="font-size:12px;">
        <asp:Literal ID="LoadList" runat="server" />
    </tbody>
</table>
<%--<br />**********************************************************************************************************************************<br />

    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="true">

    </telerik:RadGrid>
--%>
</asp:Panel>

<telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="lblHelp" RelativeTo="Element" OffsetY="15" ShowEvent="OnClick"
    Position="BottomLeft" RenderInPageRoot="true" HideEvent="ManualClose" Animation="Resize" EnableShadow="true">
<span class="ttHeader">Running Totals Key</span>
<table><tr><td style="padding: 0 8px;">

    <table>
        <tr>
            <td colspan="2" style="padding-top:7px;">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="middle">example:  &nbsp; </td>
                        <td>
        <table cellpadding="0" cellspacing="0" style="border:1px solid black;">
        <tr>
            <td style="width:18px;vertical-align:top;text-align:center;border-right:1px solid #cccccc;">12</td>
            <td style="width:18px;text-align:center;background-color:#FFAA00;border-right:1px solid #cccccc;">1</td>
            <td style="width:18px;vertical-align:top;text-align:center;color:Red;border-right:1px solid #cccccc;">5</td>
            <td style="width:18px;vertical-align:top;text-align:center;color:Red;background-color:#BCCCB4;border-right:1px solid #cccccc;">3</td>
            <td style="width:18px;vertical-align:top;text-align:center;background-color:#BCCCB4;border-right:1px solid #cccccc;">73</td>
            <td style="width:18px;text-align:center;vertical-align:top;">88</td>
        </tr>
        </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align:top;text-align:center;border:1px solid #666666;padding:0 4px 0 4px;">12</td>
            <td style="border:1px solid #666666;">&nbsp; Twelve loads currently being worked 
            </td>
        </tr>
        <tr>
            <td style="text-align:center;background-color:#FFAA00;border:1px solid #666666;">1</td>
            <td style="border:1px solid #666666;">... of those twelve, one is over 90 minutes in 
            </td>
        </tr>
        <tr>
            <td style="vertical-align:top;text-align:center;color:Red;border:1px solid #666666;">5</td>
            <td style="border:1px solid #666666;">... of those twelve, five are over 2 hours in
            </td>
        </tr>
        <tr>
            <td style="vertical-align:top;text-align:center;color:Red;background-color:#BCCCB4;border:1px solid #666666;">3</td>
            <td style="border:1px solid #666666;"> <b>*</b> Number of loads completed (over 2 hours)
            </td>
        </tr>
        <tr>
            <td style="vertical-align:top;text-align:center;background-color:#BCCCB4;border:1px solid #666666;">73</td>
            <td style="border:1px solid #666666;"> <b>*</b> Number of loads completed&nbsp; (less than 2 hours)
            </td>
        </tr>
        <tr>
            <td style="text-align:center;vertical-align:top;border:1px solid #666666;">88</td>
            <td style="border:1px solid #666666;"> <b>*</b> Total loads for the day (Completed + Currently Working)
            </td>
        </tr>
    </table>
    <table><tr>
    <td style="text-align:center;vertical-align:top;"><b>*</b></td>
    <td style="font-size:11px;">These totals not available when 'Hide Completed' is checked.<br />
    To see these totals, UN-check the 'Hide Completed' checkbox</td>
    </tr></table>
</td></tr></table>&nbsp;
<center>To Close - Click X in upper right corner</center>&nbsp;
                        </telerik:RadToolTip>

<telerik:RadToolTip ID="RadToolTip2" runat="server" TargetControlID="lblTips" RelativeTo="Element"  
    ShowEvent="OnClick" Position="BottomLeft" HideEvent="ManualClose" 
     Animation="Resize" EnableShadow="true">
<span class="ttHeader">SEU Tips & Tricks</span>
<table><tr><td style="padding:0 8px;">
<table>
    <tr>
        <td class="ttTitle">Use FULL Screen</td>
    </tr>
    <tr>
        <td class="ttBody">
            The F11 key on your keyboard will toggle most browsers to full screen.<br />
            F11 again to toggle back. &nbsp; &nbsp;(MSIE and Chrome tested)
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Find ANYTHING instantly</td>
    </tr>
    <tr>
        <td  class="ttBody">
            Use your browser's Find command, usually Ctrl-F &nbsp; &nbsp;(MSIE and Chrome tested)<br />
            Numbers, names, times, partial names, partial numbers, anything on the page<br />
             ... all found <span style="text-decoration:underline;">instantly</span> AS you type!
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Hide Completed Loads</td>
    </tr>
    <tr>
        <td  class="ttBody">
            When the checkbox in this control is CHECKED, all completed loads are hidden.<br />
            This 'dramatically' reduces refresh time for a quick overview of current activity.<br />
            (this is the default view for SEU staff)<br />
            UNcheck this control to include all completed loads.  This will take longer to load but <br />
            will provide you an accurate chronological view of all the day's activities.<br />
            (this is the default view for Clients, Carriers and Vendors)  [for Searching, see previous tip]<br />
            A status indicator below the control will display a red <font color="red">NO</font> to indicate when you<br />
            are not looking at a complete list.
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Running Totals</td>
    </tr>
    <tr>
        <td  class="ttBody">
            The 'Running Totals' control will give you a quick-shot count of the day's activities.<br />
            Hover your mouse over each number for brief tool-tip or Click the <font color='blue'>KEY</font> for detailed example.
        </td>
    </tr>
</table><br />
<center>To Close - Click X in upper right corner</center>&nbsp;


</td></tr></table>

</telerik:RadToolTip>
                        </div>

</form>

</body>
</html>
