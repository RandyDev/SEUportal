<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DailyWeekly.aspx.vb" Inherits="DiversifiedLogistics.DailyWeekly" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daily/Weekly Report</title>
    <link href="../styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .lilBlueButton{
            font-size:11px;
            color:Blue;
            font-weight:normal;
        }
        .headTitle{
            font-size:24px;
            text-align:center;
            font-family:Arial;
            font-weight:bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
            <script type="text/javascript">
                function onRequestStart(sender, args) {
                    if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToCsvButton") >= 0) {
                        args.set_enableAjax(false);
                    }
                }
            </script>
        </telerik:RadCodeBlock>
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" ClientEvents-OnRequestStart="onRequestStart">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnShowRecords">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="WebBlue" />
    <div>
    <table align="center" width="950">
        <tr>
            <td>
                <span class="headTitle">Daily / Weekly Report</span>
            </td>
        </tr>
        <tr>
            <td align="left">
<table width="100%" style="font-family:Arial; font-size:12px; font-weight:bold;">
    <tr>
        <td width="180">Location:<br /><telerik:RadComboBox ID="cbLocations" runat="server" AllowCustomText="true" Filter="Contains" EmptyMessage="Select Location" /></td>
        <td width="130">Start Date:<br /><telerik:RadDatePicker ID="dpStartDate" Width="110px" runat="server" ></telerik:RadDatePicker></td>
        <td width="130">End Date:<br /><telerik:RadDatePicker ID="dpEndDate" Width="110px" runat="server" ></telerik:RadDatePicker></td>
        <td>&nbsp;<br /><asp:button ID="btnShowRecords" Text="Show Records" CommandName="Refresh" runat="server" /></td>
        <td align="right" valign="middle">
            <span onmouseover="this.style.cursor='help';"><asp:Label ID="lblHelp" CssClass="lilBlueButton" Text="help with this page" runat="server" /></span>
        </td>

    </tr>
</table>
            </td>
        </tr>
        <tr>
            <td>

        <telerik:RadGrid ID="RadGrid1" runat="server" ShowGroupPanel="False" GridLines= "None" AutoGenerateColumns="False"
            AllowSorting="True" AllowFilteringByColumn="False" ShowFooter="True" Skin="Default">
        <ExportSettings ExportOnlyData="true" HideStructureColumns="false">
            <Excel FileExtension="xls" Format="ExcelML" />
        </ExportSettings>
        <ClientSettings  Scrolling-ScrollHeight="425px" EnableRowHoverStyle="True" 
          AllowColumnsReorder="True" AllowDragToGroup="False" ReorderColumnsOnClient="True">
            <Selecting AllowRowSelect="True" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
<MasterTableView CssClass="wdBack" ShowGroupFooter="False" DataKeyNames="" GridLines="None" CommandItemDisplay="TopAndBottom">
<CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="False" ShowExportToExcelButton="True" ShowExportToWordButton="true" ShowExportToCsvButton="true" />
    <Columns>
        <telerik:GridBoundColumn DataField="Location" UniqueName="Location" HeaderText="Location" visible="False"  
          AllowSorting="false" Groupable="False" AllowFiltering="False">
            <HeaderStyle Width="115" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="WorkDate" UniqueName="Date" HeaderText="Date" visible="true"
          DataFormatString="{0:MM/dd/yyyy}" AllowSorting="true" Groupable="False" AllowFiltering="False">
            <HeaderStyle Width="85" />
        </telerik:GridBoundColumn>

        <telerik:GridBoundColumn DataField="NumOfPOs" UniqueName="NumOfPOs" HeaderText="# POs" visible="true"  
            DataFormatString="{0:N0}" Aggregate="Sum" FooterText="Total POs" AllowSorting="False" Groupable="False" AllowFiltering="False">
            <HeaderStyle Width="65" />
            <FooterStyle HorizontalAlign="Left" /> 
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="NumOfLoads" UniqueName="NumOfLoads" HeaderText="# Loads" visible="true"  
            DataFormatString="{0:N0}" Aggregate="Sum" FooterText="Total Loads" AllowSorting="true" Groupable="False" AllowFiltering="False">
            <HeaderStyle Width="65" />
            <FooterStyle HorizontalAlign="Left" /> 
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="PalUnld" UniqueName="PalUnld" HeaderText="Pal Unld" visible="true"  
            DataFormatString="{0:N0}" Aggregate="Sum" FooterText="Total Pallets" AllowSorting="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="65" />
            <FooterStyle HorizontalAlign="Left" /> 
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Pieces" UniqueName="Pieces" HeaderText="Pieces" visible="true"  
            DataFormatString="{0:N0}" Aggregate="Sum" FooterText="Total Pieces" AllowSorting="false" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="65" />
            <FooterStyle HorizontalAlign="Left" /> 
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="PalRecd" UniqueName="PalRecd" HeaderText="Pal Recvd" visible="true"  
            DataFormatString="{0:N0}" Aggregate="Sum" AllowSorting="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="65" />
            <FooterStyle HorizontalAlign="Left" /> 
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Bad" UniqueName="Bad" HeaderText="Bad" visible="true"  
            DataFormatString="{0:N0}" Aggregate="Sum" AllowSorting="false" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="65" />
            <FooterStyle HorizontalAlign="Left" /> 
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Resk" UniqueName="Resk" HeaderText="Restack" visible="true"  
            DataFormatString="{0:N0}" Aggregate="Sum" AllowSorting="false" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="65" />
            <FooterStyle HorizontalAlign="Left" /> 
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="HoursWorked" UniqueName="HoursWorked" HeaderText="HoursWorked" visible="true"  
            DataFormatString="{0:N2}" Aggregate="Sum" AllowSorting="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="65" />
            <FooterStyle HorizontalAlign="Left" /> 
        </telerik:GridBoundColumn>

        <telerik:GridBoundColumn DataField="PALph" UniqueName="PALph" HeaderText="Pal Hr" visible="true"  
           DataFormatString="{0:N2}" AllowSorting="false" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="85" />
           
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="PIEph" UniqueName="PIEph" HeaderText="Pcs Hr" visible="true"  
           DataFormatString="{0:N2}" AllowSorting="false" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="85" />
        </telerik:GridBoundColumn>
    
    </Columns>
</MasterTableView>
            </telerik:RadGrid>

            </td>
        </tr>
    </table>

<telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="lblHelp" Width="600px" RelativeTo="Element"  
    ShowEvent="OnClick" Position="BottomLeft" HideEvent="ManualClose"  
     Animation="Resize" EnableShadow="true">
<table cellpadding="0" cellspacing="0" width="100%"><tr>
<td><span class="ttHeader">Daily / Weekly Report</span>
</td>
</tr></table>

<table><tr><td style="padding:0 8px;">
<table>
    <tr>
        <td class="ttBody"> <br />
            Select a Location, Start Date and End Date then click the 'Show Records' button.
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Date Selector</td>
    </tr>
    <tr>
        <td  class="ttBody">
            Selecting dates can be accomplished a few ways.<br />
            First, simply type a date in the box ... or ...<br />
            Adjust the existing date. &nbsp;Place the cursor on a date part (month, date or year) then<br />
            use your arrow keys or scroll wheel to increment/decrement the date part ... or ...<br />
            Click the calendar control and select a date. &nbsp;While the calendar control is open you can,<br />
            click double arrows to move three months, click single arrow to move one month,<br />
            click the month name to open a month/year selector.
            <br />
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Sorting Grid by Column</td>
    </tr>
    <tr>
        <td  class="ttBody">
            Click the column's header one time to sort ascending (column header will display up arrow)<br />
            Click the column's header a second time to sort descending (column header will display down arrow)<br />
            Click the column's header a third time to remove the sort.<br />
            Following columns can be sorted: &nbsp;Date, # Loads, Pal Unld, Pieces, Pal Recvd and HoursWorked.<br />
            The initial default sort is by Date, ascending.<br />

        </td>
    </tr>
    <tr>
        <td class="ttTitle">Re-order Grid Columns</td>
    </tr>
    <tr>
        <td  class="ttBody">
            To re-order columns, drag and drop column headers to desired locations.<br />
            To drag and drop, place cursor over item. Click and hold mouse button as you <br />
            move cursor (drag item) to new location and then release mouse button (drop).
            <br />
        </td>
    </tr>
</table><br />
<center>To Close - Click X in upper right corner</center>

</td></tr></table>
</telerik:RadToolTip>


    </div>
</form>
</body>
</html>
