<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PalletsPiecesManHour.aspx.vb" Inherits="DiversifiedLogistics.PalletsPiecesManHour" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Pallets/Pieces per Man Hour</title>
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
    <span class="headTitle">Pallets / Pieces per Man-Hour Report</span>
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
<telerik:RadGrid ID="RadGrid1" runat="server" Width="910px" GridLines= "None" AutoGenerateColumns="False"
    Skin="WebBlue">
    <ExportSettings FileName="AdditionalCost" />
<MasterTableView CssClass="wdBack"  DataKeyNames="" GridLines="None" CommandItemDisplay="None">
    <Columns>
        <telerik:GridBoundColumn allowsorting="true" UniqueName="Location" HeaderText="Location" 
            DataField="Location" visible="false" Groupable="true" AllowFiltering="true">
            <HeaderStyle Width="115" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="LogDate" UniqueName="LogDate" HeaderText="LogDate" HeaderTooltip="LogDate" 
            ItemStyle-Wrap="false" Visible="false" DataFormatString="{0:MM/dd/yyyy}" AllowSorting="false" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="85" />
        </telerik:GridBoundColumn> 
        <telerik:GridBoundColumn DataField="LogDate1" UniqueName="LogDate1" HeaderText="LogDate1" HeaderTooltip="LogDate1" 
            ItemStyle-Wrap="false" Visible="false" DataFormatString="{0:MM/dd/yyyy}" AllowSorting="false" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="85" />
        </telerik:GridBoundColumn> 
        <telerik:GridBoundColumn allowsorting="false" UniqueName="NumOfPOs" HeaderText="# of POs"  headertooltip="Number of POs"
            DataField="NumOfPOs" DataFormatString="{0:N0}" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="65" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="NumOfLoads" HeaderText="# of Loads"  headertooltip="Number of Loads"
            DataField="NumOfLoads" DataFormatString="{0:N0}" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="65" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="PalUnld" HeaderText="Pallets Unld"  headertooltip="Pallets Unloaded"
            DataField="PalUnld" DataFormatString="{0:N0}" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="65" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="Pieces" HeaderText="Pieces" 
            DataField="Pieces" DataFormatString="{0:N0}" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="65" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="PalRecd" HeaderText="Pallets Recvd" headertooltip="Pallets Recieved" 
            DataField="PalRecd" DataFormatString="{0:N0}" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="65" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="Bad" HeaderText="Bad" headertooltip="Bad Pallets"
            DataField="Bad" DataFormatString="{0:N0}" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="65" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="Resk" HeaderText="Restacks"  HeaderTooltip="Restacks"
            DataField="Resk" DataFormatString="{0:N0}" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="65" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="TotalHours" HeaderText="Total Hours" 
            DataField="TotalHours" DataFormatString="{0:N0}" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="65" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="pph" HeaderText="Pallets per Hr" HeaderTooltip="Pallets per Hour" 
            DataField="pph" DataFormatString="{0:N0}" visible="true" Groupable="false" AllowFiltering="false">
            <HeaderStyle Width="65" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn allowsorting="false" UniqueName="cph" HeaderText="Cases per Hr" HeaderTooltip="Cases per Hour"
            DataField="cph" DataFormatString="{0:N0}" visible="true" Groupable="false" AllowFiltering="false">
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
