<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoadsDetails.aspx.vb" Inherits="DiversifiedLogistics.LoadsDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Loads Details Report</title>
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
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" 
        Skin="WebBlue" />
    <div>
    <table align="center">
        <tr>
            <td>
    <span class="headTitle">Loads Details Report</span>
            </td>
        </tr>
        <tr>
            <td>
<table width="100%" style="font-family:Arial; font-size:12px; font-weight:bold;">
    <tr>
        <td width="220" valign="middle">Location: <telerik:RadComboBox ID="cbLocations" runat="server" AllowCustomText="true" Filter="Contains" EmptyMessage="Select Location" />
        </td>
        <td width="215" valign="middle">Start Date: <telerik:RadDatePicker ID="dpStartDate" runat="server" ></telerik:RadDatePicker>
        </td>
        <td width="210" valign="middle">End Date: <telerik:RadDatePicker ID="dpEndDate" runat="server" ></telerik:RadDatePicker>
        </td>
        <td width="100">
            <asp:button ID="btnShowRecords" Text="Show Records" CommandName="Refresh" runat="server" />
        </td>
        <td style="padding-left:25px;font-weight:normal;"><asp:LinkButton ID="lbReportView" runat="server" Text="Report View" /></td>
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
<telerik:RadGrid ID="RadGrid1" runat="server" Width="1024px" GridLines= "None" AutoGenerateColumns="False"
    AllowSorting="True" AllowFilteringByColumn="True" ShowGroupPanel="True" Skin="WebBlue">
    <ExportSettings FileName="LoadsDetailsReport" />
    <ClientSettings  Scrolling-ScrollHeight="450px" EnableRowHoverStyle="true" AllowColumnsReorder="True" AllowDragToGroup="True" 
        ReorderColumnsOnClient="True">
        <Selecting AllowRowSelect="True" />
        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
    </ClientSettings>
<MasterTableView CssClass="wdBack"  DataKeyNames="" GridLines="None" CommandItemDisplay="None">
    <Columns>
        <telerik:GridBoundColumn AllowSorting="false" UniqueName="Location" HeaderText="Location" 
            DataField="Location" visible="false" Groupable="false">
            <HeaderStyle Width="110" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Date" UniqueName="Date" HeaderText="Date" HeaderTooltip="Date" 
            ItemStyle-Wrap="false" DataFormatString="{0:MM/dd/yyyy}" AllowSorting="false" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="85" />
        </telerik:GridBoundColumn> 
        <telerik:GridBoundColumn AllowSorting="true" UniqueName="Dept" HeaderText="Department" 
            DataField="Dept" visible="true" Groupable="true" AllowFiltering="true">
            <HeaderStyle Width="115" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn AllowSorting="true" UniqueName="LoadType" HeaderText="Load Type" 
            DataField="LoadType" visible="true" Groupable="true" AllowFiltering="true">
            <HeaderStyle Width="115" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn AllowSorting="true" UniqueName="CustomerName" HeaderText="Customer Name" 
            DataField="CustomerName" visible="true" Groupable="true" AllowFiltering="true">
            <HeaderStyle Width="225" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn AllowSorting="true" UniqueName="VendorNum" HeaderText="Vendor #" 
            DataField="VendorNum" visible="true" Groupable="true" AllowFiltering="true">
            <HeaderStyle Width="115" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn AllowSorting="false" UniqueName="Rect" HeaderText="Recpt #" 
            DataField="Rect" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="85" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn AllowSorting="true" UniqueName="PO" HeaderText="PO #" 
            DataField="PO" visible="true" Groupable="true" AllowFiltering="true">
            <HeaderStyle Width="115" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Amount" UniqueName="Amount" HeaderText="Amount"
            ItemStyle-Wrap="false" DataFormatString="{0:C2}" AllowSorting="false" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="85px" />
        </telerik:GridBoundColumn> 
        <telerik:GridBoundColumn AllowSorting="true" UniqueName="LoadDesc" HeaderText="Description" 
            DataField="LoadDesc" visible="true" Groupable="true" AllowFiltering="true">
            <HeaderStyle Width="115px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn AllowSorting="true" UniqueName="Carrier" HeaderText="Carrier" 
            DataField="Carrier" visible="true" Groupable="true" AllowFiltering="true">
            <HeaderStyle Width="115px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn AllowSorting="false" UniqueName="Truck" HeaderText="Truck" 
            DataField="Truck" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="45px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn AllowSorting="true" UniqueName="Trailer" HeaderText="Trailer" 
            DataField="Trailer" visible="true" Groupable="false" AllowFiltering="true">
            <HeaderStyle Width="115px" />
        </telerik:GridBoundColumn>

        <telerik:GridBoundColumn DataField="ApptTime" UniqueName="ApptTime" HeaderText="Appointment" HeaderTooltip="Appointment Time" 
            ItemStyle-Wrap="false" DataFormatString="{0:hh:mm tt}" AllowSorting="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="85px" />
        </telerik:GridBoundColumn> 
        <telerik:GridBoundColumn DataField="Gate" UniqueName="Gate" HeaderText="Gate" HeaderTooltip="Gate Time" 
            ItemStyle-Wrap="false" DataFormatString="{0:hh:mm tt}" AllowSorting="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="85px" />
        </telerik:GridBoundColumn> 
        <telerik:GridBoundColumn DataField="Dock" UniqueName="Dock" HeaderText="Dock" HeaderTooltip="Dock Time" 
            ItemStyle-Wrap="false" DataFormatString="{0:hh:mm tt}" AllowSorting="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="85px" />
        </telerik:GridBoundColumn> 
        <telerik:GridBoundColumn DataField="Start" UniqueName="Start" HeaderText="Start" HeaderTooltip="Start Time" 
            ItemStyle-Wrap="false" DataFormatString="{0:hh:mm tt}" AllowSorting="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="85px" />
        </telerik:GridBoundColumn> 
        <telerik:GridBoundColumn DataField="Comp" UniqueName="Comp" HeaderText="Complete" HeaderTooltip="Completion Time" 
            ItemStyle-Wrap="false" DataFormatString="{0:hh:mm tt}" AllowSorting="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="85px" />
        </telerik:GridBoundColumn> 
        <telerik:GridBoundColumn AllowSorting="false" UniqueName="PallUnld" HeaderText="Pallets Unld"  HeaderTooltip="Pallets Unloaded"
            DataField="PallUnld" DataFormatString="{0:N0}" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="60" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn AllowSorting="true" UniqueName="Door" HeaderText="Door #" HeaderTooltip="Door Number"
            DataField="Door" visible="true" Groupable="true" AllowFiltering="true">
            <HeaderStyle Width="115" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn AllowSorting="false" UniqueName="Pieces" HeaderText="Pieces" HeaderTooltip="Pieces"
            DataField="Pieces" DataFormatString="{0:N0}"  visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="55" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn AllowSorting="false" UniqueName="Weight" HeaderText="Weight" 
            DataField="Weight" DataFormatString="{0:N0}" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="55" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn AllowSorting="false" UniqueName="Restacks" HeaderText="Restacks" 
            DataField="Restacks" DataFormatString="{0:N0}" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="55" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn AllowSorting="false" UniqueName="PalRecd" HeaderText="Pallets Recvd" 
            DataField="PalRecd" DataFormatString="{0:N0}" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="55" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="BadPal" HeaderText="Bad Pallets" 
            DataField="BadPal" DataFormatString="{0:N0}" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="45" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="Items" HeaderText="Items" 
            DataField="Items" DataFormatString="{0:N0}" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="45" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="Check" HeaderText="Check" 
            DataField="Check" visible="true" Groupable="false" AllowFiltering="true">
            <HeaderStyle Width="115" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="BOL" HeaderText="BOL" 
            DataField="BOL" visible="true" Groupable="false" AllowFiltering="true">
            <HeaderStyle Width="115" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="true" UniqueName="CreatedBy" HeaderText="Created By" 
            DataField="CreatedBy" visible="true" Groupable="true" AllowFiltering="true">
            <HeaderStyle Width="115" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="Comments" HeaderText="Comments" 
            DataField="Comments" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="115" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="Unloaders" HeaderText="Unloaders" 
            DataField="Unloaders" visible="true" Groupable="false" AllowFiltering="true">
            <HeaderStyle Width="115" />
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
