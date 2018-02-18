<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdditionalCost.aspx.vb" Inherits="DiversifiedLogistics.AdditionalCost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Additional Costs</title>
    <style type="text/css">
        .lilBlueButton{
            font-size:11px;
            font-weight:bold;
            color:Blue;
        }

        .headTitle{
            font-size:24px;
            text-align:center;
            font-family:Arial;
            font-weight:bold;
        }
        .exportRow{   
            background-image:url(../images/header-colds.png);
            background-position:0 -304px; /* add 20px to #header.background-position */
            text-align:right;
            padding-top:4px;
        }
    </style>
</head>
<body id="theBody" runat="server">
    <form id="form1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
            <script type="text/javascript">
                function mngRequestStarted(ajaxManager, eventArgs) {
                    if (eventArgs.get_eventTarget().indexOf("mngBtn") != -1)
                        eventArgs.set_enableAjax(false);
                }
                function pnlRequestStarted(ajaxPanel, eventArgs) {
                    if (eventArgs.get_eventTarget().indexOf("pnlBtn") != -1)
                        eventArgs.set_enableAjax(false);
                }
            </script>
        </telerik:RadCodeBlock>    
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" ClientEvents-OnRequestStart="mngRequestStarted">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnShowRecords">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="mngBtnExcel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="mngBtnExcel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="mngBtnWord">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="mngBtnWord" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="mngBtnCSV">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="mngBtnCSV" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="mngBtnPDF">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="mngBtnPDF" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <ClientEvents OnRequestStart="mngRequestStarted" />
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />
    <div>
    <table align="center">
        <tr>
            <td>
    <span class="headTitle">Additional Cost Report</span>
            </td>
        </tr>
        <tr>
            <td>
<table width="100%" style="font-family:Arial; font-size:12px; font-weight:bold;">
    <tr>
        <td width="220" valign="middle">Location: <br /><telerik:RadComboBox ID="cbLocations" runat="server" AllowCustomText="true" Filter="Contains" EmptyMessage="Select Location" />
        </td>
        <td width="215" valign="middle">Start Date: <br /><telerik:RadDatePicker ID="dpStartDate" runat="server" ></telerik:RadDatePicker>
        </td>
        <td width="210" valign="middle">End Date: <br /><telerik:RadDatePicker ID="dpEndDate" runat="server" ></telerik:RadDatePicker>
        </td>
        <td width="100">
            <asp:button ID="btnShowRecords" Text="Show Records" CommandName="Refresh" runat="server" />
        </td>
        <td style="padding-left:25px;font-weight:normal;"><asp:LinkButton ID="lbReportView" runat="server" Text="Report View" />
        </td>
<td style="padding-left:15px;">
Skin Selector: <br /><telerik:RadSkinManager ID="RadSfkinManager1" runat="server" ShowChooser="True" Skin="Sunset" >
    <TargetControls>
    </TargetControls>
</telerik:RadSkinManager>
</td>
        <td align="right"  valign="middle">

            <asp:LinkButton ID="lbtnHelp" Text="help" OnClientClick="openHelp();return false;" runat="server" />
        </td>

    </tr>
</table>
            </td>
        </tr>
        <tr>
            <td>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td>
<telerik:RadGrid ID="RadGrid1" runat="server" Width="1010px" GridLines= "None" AutoGenerateColumns="False"
    AllowSorting="True" AllowFilteringByColumn="True" ShowGroupPanel="True">
    <ExportSettings FileName="AdditionalCost" />
    <ClientSettings  Scrolling-ScrollHeight="450px" EnableRowHoverStyle="true" AllowColumnsReorder="True" AllowDragToGroup="True" 
        ReorderColumnsOnClient="True">
        <Selecting AllowRowSelect="True" />
        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
    </ClientSettings>
<MasterTableView CssClass="wdBack"  DataKeyNames="" GridLines="None" CommandItemDisplay="None">
    <Columns>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="Location" HeaderText="Location" 
            DataField="Location" visible="false" Groupable="false">
            <HeaderStyle Width="110" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn AllowSorting="true" UniqueName="Date" HeaderText="Date" 
            DataField="Date" DataFormatString="{0:MM/dd/yyyy}" visible="true" Groupable="true" AllowFiltering="false">
            <HeaderStyle Width="85" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="true" UniqueName="Department" HeaderText="Department" 
            DataField="Department" visible="true" Groupable="true" AllowFiltering="true">
            <HeaderStyle Width="115" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="true" UniqueName="CustomerID" HeaderText="Customer ID" 
            DataField="CustomerID" visible="true" Groupable="true" AllowFiltering="true">
            <HeaderStyle Width="225" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="true" UniqueName="PO" HeaderText="PO #" 
            DataField="PO" visible="true" Groupable="false" AllowFiltering="true">
            <HeaderStyle Width="115" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="BadPallets" HeaderText="Bad Pallets" 
            DataField="BadPallets" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="75" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="true" UniqueName="Carrier" HeaderText="Carrier" 
            DataField="Carrier" visible="true" Groupable="true" AllowFiltering="true">
            <HeaderStyle Width="125" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="Restacks" HeaderText="Restacks" 
            DataField="Restacks" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="65" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="true" UniqueName="LoadType" HeaderText="Load Type" 
            DataField="LoadType" visible="true" Groupable="true" AllowFiltering="true">
            <HeaderStyle Width="115" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="Amount" HeaderText="Amount" HeaderTooltip="Additional Cost" 
            DataField="Amount" DataFormatString="{0:C2}" visible="true" Groupable="False" AllowFiltering="false">
            <HeaderStyle Width="65" />
        </telerik:GridBoundColumn>
    </Columns>
</MasterTableView>
</telerik:RadGrid>
        </td>
    </tr>
    <tr>
        <td class="exportRow">
        <asp:ImageButton ID="mngBtnExcelz" runat="server" ImageUrl="~/images/Excel-16.gif" OnClick="btnExcel_Click" ToolTip="Export to Excel" /> &nbsp; 
        <asp:ImageButton ID="mngBtnCSVz" runat="server" ImageUrl="~/images/Icon_csv.gif" OnClick="btnCSV_Click" ToolTip="Export to CSV" /> &nbsp; 
        </td>
    </tr>
</table>

    </td></tr></table>
    </div>

<%--<telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="lblHelp" RelativeTo="Element" 
    ShowEvent="OnClick" Position="BottomLeft" HideEvent="ManualClose"  
     Animation="Resize" EnableShadow="true">
<table cellpadding="0" cellspacing="0" width="100%"><tr>
<td><span class="ttHeader">Freight Issues Report</span></td>
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
        <td class="ttTitle">Select Records</td>
    </tr>
    <tr>
        <td  class="ttBody">
            First, select a &#39;Start Date&#39; and &#39;End Date&#39;.&nbsp; By default the previous 2 weeks are selected.<br />
             Next, either &#39;Select Location&#39; from the 
            dropdown box or click the &#39;Show All Locations&#39; button.<br /> </td>
    </tr>
    <tr>
        <td class="ttTitle">Working the Grid</td>
    </tr>
    <tr>
        <td  class="ttBody">
            If you click the &#39;Show All Locations&#39; button the initial grid will be sorted 
            by:&nbsp;&nbsp; Location then Date (most recent at the top) and Department.<br /> If you select a 
            location from the drop down box, the grid will not have a &#39;Location&#39; column.<br />
            <strong>Sorting</strong><br />
             Click on column name to sort in ascending order. Note up icon next to column 
            name and column is now shaded to indicate &#39;sorted&#39; column.<br />
             Click on column name again to sort in descending order.&nbsp; Click on column name a 
            third time to remove/clear sorting.<br />
             You may sort on multiple columns to suit your needs.<br />
             <strong>Grouping</strong><br /> 
             Move your cursor over a column header until changes to a cross-hair. &nbsp;Click, 
            Drag and Drop column(s) into the &#39;Grouping&#39; bar at the top of the grid.<br />
             If grouping by multiple columns, you can re-arrange grouping in the &#39;Grouping&#39; 
            bar by dragging to new position on the bar.<br />
             To &#39;Remove&#39; a grouping, drag and drop the column to any where outside the 
            &#39;Grouping&#39; bar.<br />
             The following fields can not be grouped: PO#, Restacks, BadPallets and Comments.<br />
            <strong>Filtering</strong><br />
            Some columns have a filter control just below the column name.&nbsp; Type your query 
            into the text box then click the filter icon next to it and select a filter.<br />
            To Clear/Remove a filter, click the filter icon and choose &#39;NoFilter&#39;.&nbsp; You may 
            filter multiple columns and the queries are NOT caSe Sensitive.<br />
            <strong>Reorder Columns</strong><br />
             Move your cursor over a column header until changes to a cross-hair. &nbsp;Click and 
            drag the column to a new location and release.<br />
            <strong>Resize Columns</strong><br />
             Move your cursor to the separator between column headers and it will change to 
            a left-right arrow.<br /> Click and drag the column separator to resize the 
            column.<br />
            <strong>Export to Excel</strong><br />
            Click the Excel icon at the top of the page.&nbsp; The filename for the exported file 
            will be:<br />
            FreightIssuesSummary_<em>location</em>_<em>startDate</em>_<em>endDate</em>.xls<br />

        </td>
    </tr>
    <tr>
        <td class="ttBody">
            &nbsp;</td>
    </tr>
</table><br />
<center>To Close - Click X in upper right corner</center>


</td></tr></table>

</telerik:RadToolTip>
--%>


<telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
    EnableShadow="true" ShowOnTopWhenMaximized="false">
    <Windows>
        <telerik:RadWindow ID="wHelp" Title="SEU Data Grid: Tips and Tricks" VisibleOnPageLoad="false" 
            runat="server" Width="450" Height="525" Behaviors="Move, Close" VisibleStatusbar="false" >
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">
    function openHelp() {
        var oManager = GetRadWindowManager();
        var loca = "ClientGridTips.aspx";
        oManager.open(loca, "wHelp");
    }
</script>
</telerik:RadCodeBlock>

    </form>
</body>
</html>
