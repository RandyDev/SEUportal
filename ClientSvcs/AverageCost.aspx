<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AverageCost.aspx.vb" Inherits="DiversifiedLogistics.AverageCost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Average Cost Report</title>
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
    <span class="headTitle">Average Cost Report</span>
            </td>
        </tr>
        <tr>
            <td>
<table width="100%" style="font-family:Arial; font-size:12px; font-weight:bold;">
    <tr>
        <td width="220" valign="middle">Location: <telerik:RadComboBox ID="cbLocations" runat="server" AllowCustomText="true" Filter="Contains" EmptyMessage="Select Location" />
        </td>
        <td width="100" align="center">
            <asp:button ID="btnShowRecords" Text="Show Records" CommandName="Refresh" runat="server" />
        </td>
        <td align="right" style="font-weight:normal;"><asp:LinkButton ID="lbReportView" runat="server" Text="Report View" /></td>
</tr><tr>
        <td width="215" valign="middle">Start Date: <telerik:RadDatePicker ID="dpStartDate" runat="server" ></telerik:RadDatePicker>
        </td>
        <td width="210" valign="middle">End Date: <telerik:RadDatePicker ID="dpEndDate" runat="server" ></telerik:RadDatePicker>
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
<telerik:RadGrid ID="RadGrid1" runat="server" Width="500px" GridLines= "None" AutoGenerateColumns="False"
    AllowSorting="True" AllowFilteringByColumn="True" ShowGroupPanel="True" Skin="WebBlue">
    <ExportSettings FileName="AverageCostReport" />
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
        <telerik:GridBoundColumn allowsorting="true" UniqueName="Vendor" HeaderText="Customer ID" 
            DataField="Vendor" visible="true" Groupable="true" AllowFiltering="true">
            <HeaderStyle Width="225px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="true" UniqueName="VendorNumber" HeaderText="Vendor Number" 
            DataField="VendorNumber" visible="true" Groupable="true" AllowFiltering="true">
            <HeaderStyle Width="115" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="Cost" HeaderText="Cost" 
            DataField="Cost" DataFormatString="{0:C2}" visible="true" Groupable="false" AllowFiltering="false">
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

   </td> </tr></table>
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
