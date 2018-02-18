<%@ Page Language="vb" AutoEventWireup="false" EnableEventValidation="false" validateRequest="false" CodeBehind="EmployeeLoadsHoursVsBusinessSummary.aspx.vb" Inherits="DiversifiedLogistics.EmployeeLoadsHoursVsBusinessSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {  
            height: 23px;
        }
.MyGridClass .rgDataDiv
{
        height : auto !important ;
}
.RadDock  .rgDataDiv
{
        height: auto !important ;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" >
    </telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSubmit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnSubmit" 
                        LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="lblttlbusloca" />
                    <telerik:AjaxUpdatedControl ControlID="lblttlbusunl" />
                    <telerik:AjaxUpdatedControl ControlID="lblttlbusdif" />
                    <telerik:AjaxUpdatedControl ControlID="pnlTroubleShooter" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDeposit" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnTroubleShoot">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlTroubleShooter" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel Transparency="50" ID="RadAjaxLoadingPanel2"  runat="server">
        <asp:Image id="Image1" runat="server" Width="110" Height="21" ImageUrl="~/images/forkliftani.gif"></asp:Image>
    </telerik:RadAjaxLoadingPanel>
    <div>
    <table>
        <tr>
        <td>
            <telerik:RadComboBox ID="cbLocations" Width="150px" Filter="Contains" runat="server" /> 
        </td>
        <td>
            <telerik:RadDatePicker ID="dpStartDate" AutoPostBack="true" Width="110px" 
                runat="server" >
            </telerik:RadDatePicker>
        </td>
        <td>
            <telerik:RadDatePicker ID="dpEndDate" AutoPostBack="true" Width="110px" 
                runat="server" >
            </telerik:RadDatePicker>
        </td>
        <td>
            <asp:Button ID="btnSubmit" runat="server" Text="Show Selected Range" />
        </td>
        <td style="padding-left:25px;">
           <span onmouseover="this.style.cursor='help';"><asp:Label CssClass="smallTitle" ID="lblPageHelp" runat="server" ForeColor="Blue" Font-Underline="true" Text="Help with this page" /></span>
        </td>

    </tr>
</table>
<table><tr><td style="width:370px;padding-right:9px;" valign="top">
<telerik:RadGrid ID="RadGrid1" runat="server" CellSpacing="0" ShowFooter="True" AutoGenerateColumns="False" >

<MasterTableView GroupsDefaultExpanded="false" GroupLoadMode="Server" ShowGroupFooter="true" 
        AllowSorting="false" DataKeyNames="woid" ClientDataKeyNames="woid" ShowFooter="True">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
    <Columns>
        <telerik:GridBoundColumn DataField="DatePo"  
            HeaderText="Date &nbsp;::&nbsp; PO #" ReadOnly="True" 
            UniqueName="DatePo">
            <HeaderStyle Width="160px"  />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn Aggregate="Count" FooterText="# Loads" 
            DataField="woid" DataType="System.Guid" 
            HeaderText="woid" 
            UniqueName="woid" Visible="false">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Amount" DataType="System.Double"  
            HeaderText="Amount"  DataFormatString="{0:C2}" AllowSorting="false" AllowFiltering="false" 
            UniqueName="Amount" Visible="true"><ItemStyle HorizontalAlign="Right" /> <HeaderStyle Width="60px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="ulCount" DataType="System.Int32" 
            HeaderText="ulCount" AllowSorting="false" AllowFiltering="false" 
            ReadOnly="True" UniqueName="ulCount"><ItemStyle HorizontalAlign="Center" />
            <HeaderStyle Width="50px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn Aggregate="Sum" FooterText="Total Business" DataFormatString="{0:C2}" DataField="ulAmount" DataType="System.Double" 
            HeaderText="ulAmount" AllowSorting="false" AllowFiltering="false"  
            ReadOnly="True" UniqueName="ulAmount"><ItemStyle HorizontalAlign="Right" />
            <HeaderStyle Width="60px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Employee"
            ReadOnly="True" SortExpression="Employee" UniqueName="Employee" Visible="false">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Name" 
            HeaderText="Name" Visible="false" 
            UniqueName="Name">
        </telerik:GridBoundColumn>
    </Columns>
<FooterStyle HorizontalAlign="Right" />
    <GroupByExpressions>
        <telerik:GridGroupByExpression>
            <GroupByFields>
                <telerik:GridGroupByField FieldName="Employee" HeaderText="" />
            </GroupByFields>
            <SelectFields>
                <telerik:GridGroupByField FieldName="Employee" />
                <telerik:GridGroupByField FieldName="ulAmount" Aggregate="Sum" />
            </SelectFields>
        </telerik:GridGroupByExpression>
    </GroupByExpressions>
    
    
</MasterTableView>
    <ClientSettings>
        <Selecting AllowRowSelect="True" />
        <ClientEvents OnRowClick="OpenWorkOrder" />
    </ClientSettings>
    <GroupingSettings ShowUnGroupButton="true" RetainGroupFootersVisibility="false"  />

    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
</telerik:RadGrid>

</td>
<td valign="top">
    <table style="font-family:Arial;font-size:11px; font-weight:bold;">
        <tr>
            <td valign="top" >
                <asp:Label ID="lblttlbusloca" runat="server" />
            </td>
        </tr><tr>
            <td class="style1">
                <asp:Label ID="lblttlbusunl" runat="server" />
            </td>
        </tr><tr>
            <td valign="top" >
                <asp:Label ID="lblttlbusdif" runat="server" />
            </td>
        </tr>
        <tr>
        <td style="padding-top:24px;">
<telerik:RadAjaxPanel ID="pnlDeposit" runat="server">
<fieldset style="font-weight:normal;" id="fsDeposit" runat="server">
<legend style="padding-left:3px;"><asp:Label ID="lblDeposit" runat="server" /></legend>
<table style="font-weight:normal;">
<tr>
<td><asp:Label ID="lblNumChecks" Text="0" runat="server" /></td>
<td style="padding-left:7px;">Number of checks</td>
</tr>
<tr>
<td><asp:Label ID="lblCheckTotal" Text="$0" runat="server" /></td>
<td style="padding-left:7px;">Total Check Charges</td>
</tr>
<tr>
<td><asp:Label ID="lblAdministrationFee" Text="$0" runat="server" /></td>
<td style="padding-left:7px;">Total Admin Fee</td>
</tr>
<tr>
<td><asp:Label ID="lblCustomerFeeTotal" style="border-bottom: 1px solid;" Text="$0" runat="server" /></td>
<td style="padding-left:7px;">Total Customer Fee</td>
</tr>
<tr>
<td><asp:Label ID="lblchecksfees" Text="$0" runat="server" /></td>
<td style="padding-left:7px;">Total checks & Fees</td>
</tr>
<tr>
<td><asp:Label ID="lbllocabusiness" style="border-bottom: 1px solid;" Text="$0" runat="server" /></td>
<td style="padding-left:7px;">Total Location Business</td>
</tr>
<tr>
<td><asp:Label ID="lblCashTotal" style="border-bottom: 3px double;" Text="$0" runat="server" /></td>
<td style="padding-left:7px;">Total w/ checks & Fees</td>
</tr>
</table>
</fieldset>
</telerik:RadAjaxPanel>
        </td>
        </tr>
    </table>
</td>
<td style="padding-left:12px;width:450px;" valign="top">

<telerik:RadAjaxPanel ID="pnlTroubleShooter" runat="server">

<fieldset style="padding:3px; width:335px; font-family:Arial;" id="fldTS" runat="server">
<legend>Trouble-Shooter&reg; Result &nbsp; &nbsp; <asp:LinkButton ID="lbtnTroubleShoot" Text="refresh" runat="server" />
&nbsp; &nbsp; &nbsp; <span onmouseover="this.style.cursor='help';" onclick="openToolTip2();"> <asp:Label CssClass="smallTitle" ID="lblTShelp" runat="server" ForeColor="Blue" Font-Underline="true" Text="Help with this control" /></span></legend>
<table cellpadding="0" cellspacing="0"><tr><td>
<table style="font-size:14px;">
    <tr>
        <td> Missing Unloaders:</td>
    </tr>
    <tr>
        <td style="padding-left:12px;font-size:12px;"><asp:Label ID="lblNoUnLoaders" runat="server" /></td>
    </tr>
    <tr>
        <td>Duplicate Unloaders:</td>
    </tr>
    <tr>
        <td style="padding-left:12px;font-size:12px;"><asp:Label ID="lblDupeUnLoaders" runat="server" /></td>
    </tr>
</table>
</td><td valign="top">
<table style="font-size:14px;">
    <tr>
        <td> Duplicate Purchase Orders:</td>
    </tr>
    <tr>
        <td style="padding-left:12px;font-size:12px;"><asp:Label ID="lblDupePOs" runat="server" /></td>
    </tr>
    <tr>
        <td>Missing Load Types:</td>
    </tr>
    <tr>
        <td style="padding-left:12px;font-size:12px;"><asp:Label ID="lblMissingLoadTypes" runat="server" /></td>
    </tr>
</table>
</td></tr></table>
</fieldset>
</telerik:RadAjaxPanel>
</td></tr></table>
<telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="lblPageHelp" RelativeTo="Element"  
    ShowEvent="OnClick" Position="BottomRight" HideEvent="ManualClose" 
     Animation="Resize" EnableShadow="true">
<span class="ttHeader">Unloader Business Summary: Tips & Tricks</span>
<table><tr><td style="padding:0 8px;"><br />
<table>
    <tr>
        <td class="ttTitle">Employee Grid (left side)</td>
    </tr>
    <tr>
        <td  class="ttBody">
            Select a location, start date and end date.&nbsp; Click &#39;Show Selected Range&#39;<br /> A 
            grid will populate with your unloader&#39;s total business for the selected<br /> 
            date range.&nbsp; Click the &#39;&gt;&#39; symbol next to the employee&#39;s name and a list of<br /> 
            all the loads worked by that employee will be displayed. <br />
            With the list expanded, click on a load to display the work order for that load.
            <br />
</td>
    </tr>
    <tr>
        <td class="ttTitle">Reconciliation (center)</td>
    </tr>
    <tr>
        <td  class="ttBody">
            The numbers to the right of the grid and the Trouble-Shooter controls (see below)<br />
            should help you reconcile your location business and your unloader&#39;s business<br />
            without having to load a separate business summary and other reconciliation reports.
            <br /><br />
            The 'Check Info' box displays the total number (count) of checks, the total amount <br />
            of check fees (where implemented) and the 'Total w/ checks' represents TOTAL receipts<br />
            (including applicable check fees).
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Trouble-Shooter Control</td>
    </tr>
    <tr>
        <td  class="ttBody">
            The new Trouble-Shooter control on the right is being developed to<br /> 
            eliminate the need to bounce from report to report to edit form and back
            <br />
            to find and correct data entry errors.<br />
            <br />
            Each configured issue is listed. &nbsp;IF an issue is found you will see a <font color='blue'>view</font> 
            link
            <br />
            for EACH problem found.&nbsp; See Trouble-Shooter help for specifics on each
            <br />
            tool in the Trouble-Shooter control</td>
    </tr>
</table><br />
<center>To Close - Click X in upper right corner</center>&nbsp;


</td></tr></table>

</telerik:RadToolTip>

<telerik:RadWindow ID="winWO" ShowContentDuringLoad="false" Title="SEU Load Viewer" 
        VisibleStatusbar="false" AutoSize="true" runat="server" 
        Behaviors="Close, Pin, Move" Skin="Sunset" BackColor="White" 
        EnableShadow="True">
</telerik:RadWindow>

<telerik:RadWindow ID="winTroubleShooterHelp" 
            Title="SEU Load Ticket Trouble-Shooter&reg;" VisibleStatusbar="false" AutoSize="true"
    runat="server" Behaviors="Close, Pin, Move, Resize" EnableShadow="true" 
            Width="302px">
    <ContentTemplate><table width="510"><tr><td style="padding:8px;">
<span style="font-size:12px;" >
This control is actually a collection of controls to help you locate and correct many of the <br />
issues that can cause your location numbers to be out of balance.  &nbsp;Each control in the <br />
trouble-shooter is designed to locate specific types of issues, conflicts and/or omissions.<br />
Click the <font color='blue'>view</font> link for EACH issue to open the load ticket and make corrections.  <br />
Click the Trouble-Shooter <font color="navy">refresh</font> link as needed to re-run the trouble-shooter.<br />
When all done, re-calculate the page by clicking 'Show Selected Range' (top of page)
</span>
<table>
    <tr>
        <td class="ttTitle">Missing Unloaders</td>
    </tr>
    <tr>
        <td  class="ttBody">
For EACH <font color='blue'>view</font> link in the control: &nbsp;Click the link to open the Load Viewer.<br />
In the Load Viewer, click the blue <font color='blue'>Change</font> link next to 'Assigned employees.<br />
In the 'Select Unloaders' dialog box, select the unloaders from the list and click 'Apply this list'<br />
When the fork lift operator is done you will see <font color="red">changes saved</font> next to the employee list.<br />
Close the load viewer and move to the next issue.<br />
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Duplicate Unloaders</td>
    </tr>
    <tr>
        <td  class="ttBody">
            Just like the 'Missing Unloaders' tool except you're removing a duplicate name from the list. <br />
        </td>
    </tr>
</table>
</td></tr></table>
    </ContentTemplate>

    
    
    </telerik:RadWindow>


    </div>
<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
<script type="text/javascript">
    function OpenWorkOrderTS(arg) {
        var oWnd = $find("<%= winWO.ClientID %>");
        oWnd.setUrl("../admin/showWO.aspx?q=" + arg);
        oWnd.show();
//        alert("Load Viewer / Editor not available in demo version.\nPlease use appropriate MSRS report.   ");
    }

    function OpenWorkOrder(sender, args) {
//        alert("Load Viewer not available in demo version.\nPlease use Load Editor or appropriate MSRS report.   ");
        var oWnd = $find("<%= winWO.ClientID %>");
        oWnd.setUrl("../admin/showWO.aspx?q=" + args.getDataKeyValue("woid"));
        oWnd.show();
    }
    function openToolTip2() {
        var TSToolTip = $find("<%= winTroubleShooterHelp.ClientID %>");
        TSToolTip.show();
    }

</script>
</telerik:RadScriptBlock>
    </form>
</body>
</html>
