<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FreightIssues.aspx.vb" Inherits="DiversifiedLogistics.FreightIssues" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
<style type="text/css">
.data{ font-size:14px;font-weight:bold;}
.lbl{font-size:12px;font-weight:normal;}
.lbl td{padding-left:24px;}
.ColorMeRed {color:Red;}

    .lilBlue{
    font-family:Arial;
font-size:12px;
    color:Blue;
    text-decoration:underline;
    padding-top:18px;
}
    .padmenot{
    padding:0px;
    }
</style>
</head>
<body>
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
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" ClientEvents-OnRequestStart="mngRequestStarted">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadButton1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadButton1" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <ClientEvents OnRequestStart="mngRequestStarted" />
    </telerik:RadAjaxManager>
<telerik:RadAjaxLoadingPanel Transparency="50" ID="RadAjaxLoadingPanel2"  runat="server" Width="210px" Height="210px">
 <asp:Image id="Image1" runat="server" Width="200px" Height="200px" ImageUrl="~/images/rogersFish.gif"></asp:Image>
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <div>
<table width="95%">
<%--    <tr>
        <td colspan="4">
            <telerik:RadTextBox ID="txtFilter" runat="server" />&nbsp;&lt;&lt;&lt;&nbsp;
            <span style="font-size:9px; font-family:Arial;"">Filter by: Department, PO #, Vendor #, Load Type or Comments</span>
        </td>
    </tr>--%>
    <tr>
        <td width="120">
            <telerik:RadDatePicker ID="dpStartDate" Width="110px" runat="server" />
        </td>
        <td width="120">
            <telerik:RadDatePicker ID="dpEndDate" Width="110px" runat="server" />
        </td>
        <td width="150">
            <telerik:RadComboBox ID="cbLocations" AutoPostBack="true" Width="150px" AllowCustomText="true" Filter="Contains" EmptyMessage="Select Location"  runat="server" /> 
        </td>
        <td width="150">
            <telerik:RadButton ID="RadButton1" runat="server" Text="Show Records">
            </telerik:RadButton>
        </td>
        <td width="32" align="right" valign="middle"><asp:ImageButton ID="mngBtnExcelz" Visible="false" runat="server" ImageUrl="~/images/Excel-16.gif" OnClick="btnExcel_Click" ToolTip="Export to Excel" /></td>

<td align="right" style="padding-right:125px; font-family:Arial;font-size:13px;">
Skin Selector: <telerik:RadSkinManager ID="RadSfkinManager1" runat="server" ShowChooser="True" Skin="Sunset" >
    <TargetControls>
    </TargetControls>
</telerik:RadSkinManager>
</td>
<td align="right">
<span onmouseover="this.style.cursor='help';"><asp:Label class="resp" CssClass="lilBlue" ID="lblHelp" Text="help with this page" runat="server" /></span>
</td>
    </tr>
</table>
            <asp:Label ID="lblCopy" runat="server" />

        <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" 
            AllowFilteringByColumn="True" AllowSorting="True" CellSpacing="0" 
            GridLines="None" ShowGroupPanel="True">
            <GroupingSettings CaseSensitive="False" />
        <ClientSettings EnableRowHoverStyle="True" ColumnsReorderMethod="Reorder"  
                AllowDragToGroup="True" AllowColumnsReorder="True" 
                ReorderColumnsOnClient="True" >
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" />
            <Scrolling AllowScroll="True" ScrollHeight="500" UseStaticHeaders="True" />
<Resizing AllowColumnResize="true" EnableRealTimeResize="true" />    
        </ClientSettings>
<MasterTableView AllowMultiColumnSorting="True">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
<Columns>


    <telerik:GridBoundColumn UniqueName="LogDate" AllowFiltering="false" Groupable="true" AllowSorting="true" DataField="LogDate" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}">
        <HeaderStyle Width="80px" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn UniqueName="Location" AllowFiltering="false" DataField="Location" HeaderText="Location">
        <HeaderStyle Width="105px" />
    </telerik:GridBoundColumn>

    <telerik:GridBoundColumn UniqueName="Department" AllowFiltering="false" DataField="Department" HeaderText="Department" Visible="true">
        <HeaderStyle Width="90px" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn UniqueName="PurchaseOrder" Groupable="false" DataField="PurchaseOrder" HeaderText="PO #">
        <HeaderStyle Width="90px" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn UniqueName="Vendor"  DataField="Vendor" HeaderText="Vendor">
        <HeaderStyle Width="200px" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn UniqueName="VendorNumber" DataField="VendorNumber" HeaderText="Vendor#" Visible="true" >
        <HeaderStyle Width="90px" />
    </telerik:GridBoundColumn>
    
    <telerik:GridBoundColumn UniqueName="Carrier" DataField="Carrier" HeaderText="Carrier">
        <HeaderStyle Width="200px" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn UniqueName="Restacks" AllowFiltering="false" Groupable="false" DataField="Restacks" HeaderText="Restacks" Visible="true" >
        <HeaderStyle Width="60px" />
    </telerik:GridBoundColumn>

    <telerik:GridBoundColumn UniqueName="BadPallets" AllowFiltering="false" Groupable="false" DataField="BadPallets" HeaderText="BadPallets" Visible="true" >
        <HeaderStyle Width="65px" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn UniqueName="Comments" Groupable="false" AllowSorting="false" DataField="Comments" HeaderText="Comments" Visible="true" />


    <telerik:GridTemplateColumn UniqueName="PicCount" AllowFiltering="false" Groupable="false" HeaderStyle-HorizontalAlign="Justify" HeaderStyle-CssClass="padmenot" HeaderImageUrl="~/images/camera.jpg" >
        <HeaderStyle Width="30px"/>
                <ItemTemplate>
            <%#IIf(IsDBNull(Eval("PicCount")), "--", Eval("PicCount"))%>
        </ItemTemplate>
    </telerik:GridTemplateColumn>

    <telerik:GridBoundColumn UniqueName="TrailerNumber" DataField="TrailerNumber" HeaderText="TrailerNumber" Visible="false" />

    <telerik:GridBoundColumn UniqueName="ID" DataField="ID" HeaderText="woid" Visible="false" />

</Columns>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>
</MasterTableView>

<FilterMenu EnableImageSprites="False"></FilterMenu>
        </telerik:RadGrid>
    </div>

<telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="lblHelp" RelativeTo="Element" 
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

    </form>
</body>
</html>
