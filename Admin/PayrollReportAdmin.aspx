<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PayrollReportAdmin.aspx.vb" Inherits="DiversifiedLogistics.PayrollReportAdmin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../styles/StyleSheet.css" rel="stylesheet" type="text/css" />

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
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="3600">
    </telerik:RadScriptManager>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) { 
            if (args.get_error() && args.get_error().name === 'Sys.WebForms.PageRequestManagerTimeoutException') { 
                            args.set_errorHandled(true); 
            } 
        }); 

        function onRequestStart(sender, args) {
            if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0) {
                args.set_enableAjax(false);
            }
        }
    </script>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSubmit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblspayPeriod" />
                    <telerik:AjaxUpdatedControl ControlID="btnSubmit" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlTitle" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel Transparency="50" ID="RadAjaxLoadingPanel1"  runat="server">
        <asp:Image id="Image1" runat="server" Width="110" Height="21" ImageUrl="~/images/forkliftani.gif"></asp:Image>
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div>
    <table > 
    <tr>
        <td>
            <telerik:RadComboBox ID="cbLocations" Width="150px" Filter="Contains" runat="server" /> 
        </td>
        <td>
            <telerik:RadDatePicker ID="dpStartDate" Width="110px" runat="server" />
        </td>
        <td style="font-size:11px; font-family:Arial;">
            Select a date within any week <asp:Label ID="lblspayPeriod" runat="server" Visible="false" />
        </td>
        <td>
            <asp:Button ID="btnSubmit" runat="server" Text="Show Selected Range" />
        </td>
 <td style="padding-left:225px;"><span onmouseover="this.style.cursor='help';"><asp:Label CssClass="lilBlueButton" ID="lblHelp" Text="help with this page" runat="server" /></span></td>
    </tr>
</table>

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1"
        ClientEvents-OnRequestStart="onRequestStart">

<telerik:RadGrid ID="RadGrid1" runat="server" AllowSorting="True" AllowAutomaticInserts="false"  ShowFooter="True"
    AutoGenerateColumns="False" Width="1082" Height="525">
    <ExportSettings HideStructureColumns="true" />
    <ClientSettings EnableRowHoverStyle="true">
        <Selecting CellSelectionMode="None"></Selecting>
        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
    </ClientSettings>
<MasterTableView DataKeyNames="EmployeeRTDSID" CommandItemDisplay="Top">
    <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
    <RowIndicatorColumn Visible="false" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>
    <ExpandCollapseColumn Visible="false" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
<Columns>
    <telerik:GridBoundColumn DataField="EmployeeRTDSID" Visible="false" ReadOnly="true"></telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="Name" HeaderText="Name" UniqueName="Name" >
        <HeaderStyle Width="125" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="EmployeeLogin" HeaderText="Login" UniqueName="EmployeeLogin" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="HourlyRate" DataFormatString="{0:C2}"  HeaderText="Hourly Rate" UniqueName="HourlyRate" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="PercentageRate" HeaderText="%" UniqueName="PercentageRate" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="TotalHours" HeaderText="Total Hours" FooterText=" " Aggregate="Sum" UniqueName="TotalHours" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>

    <telerik:GridBoundColumn DataField="TotalHoursHourly"  Aggregate="Sum" FooterText=" " HeaderText="Hourly Hours" UniqueName="HourlyHours" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>

    <telerik:GridBoundColumn DataField="RegHoursPay" DataFormatString="{0:C2}" Aggregate="Sum"  HeaderText="Reg Hours Pay" UniqueName="RegHoursPay" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="OTHours" HeaderText="OTHours" FooterText=" " Aggregate="Sum" UniqueName="OTHours" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="OTPay" DataFormatString="{0:C2}" Aggregate="Sum"  HeaderText="OTPay" UniqueName="OTPay" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="Business" DataFormatString="{0:C2}" Aggregate="Sum"  HeaderText="Business" UniqueName="Business" >
        <HeaderStyle Width="95" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="LoadPay" DataFormatString="{0:C2}" Aggregate="Sum"  HeaderText="LoadPay" UniqueName="LoadPay" >
        <HeaderStyle Width="55" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="HolidayPay" DataFormatString="{0:C2}" Aggregate="Sum"  HeaderText="Holiday Pay" UniqueName="HolidayPay" Visible="false" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="HalfTimePay" DataFormatString="{0:C2}"  HeaderText="Half Time" UniqueName="HalfTimePay" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="PercentageOTPay" DataFormatString="{0:C2}" Aggregate="Sum"  HeaderText="% OT Pay" UniqueName="PercentageOTPay" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="SpecialPay" DataFormatString="{0:C2}" Aggregate="Sum"  HeaderText="Other Pay" UniqueName="SpecialPay" >
        <HeaderStyle Width="55" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="GrossPay" DataFormatString="{0:C2}" Aggregate="Sum" FooterText=" " HeaderText="Gross Pay" UniqueName="GrossPay">
        <HeaderStyle Width="95" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
</Columns>
    <EditFormSettings>
        <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
    </EditFormSettings>
</MasterTableView>
    <FilterMenu EnableImageSprites="False"></FilterMenu>
</telerik:RadGrid>

</telerik:RadAjaxPanel>

<asp:Panel ID="pnlTitle" runat="server">
<br /><br /><br/><br /><br/>
            <table cellpadding="0" cellspacing="0" align="center" style="font-family:Arial;"><tr>
            <td align="right" style="font-size:26px;"><asp:Label ID="lblPageTitle" runat="server" /></td>
            </tr></table>

</asp:Panel>
    </div>
<telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="lblHelp" RelativeTo="Element"  
    ShowEvent="OnClick" Position="BottomLeft" HideEvent="ManualClose" 
     Animation="Resize" EnableShadow="true">
<span class="ttHeader">Weekly Payroll Summary</span>
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
        <td class="ttTitle">Basic Use</td>
    </tr>
    <tr>
        <td  class="ttBody">
             After selecting a location, use the date picker calendar to select any date within<br />
             a pay week and click the 'Show Selected Range' button. &nbsp;The <asp:Label id="lblPayWeek" runat="server" /><br />
             pay week encompassing your selected date will be processed and displayed. <br />
             Be patient, there is a lot to process and the report can take up to one minute to display.
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Exporting to Excel</td>
    </tr>
    <tr>
        <td  class="ttBody">
            To export this grid to Excel simply click the 'Export to Excel' icon in the upper <br />
            right corner of the grid and <span style="font-size:12px;font-weight:bold;">Please Be Patient</span>.<br />
            Generating the Excel spreadsheet can take up to one full minute +- and, for now, there is <br />
            no visual feedback other than your status bar (if turned on) at the bottom of your screen.  
        </td>
    </tr>
<%--    <tr>
        <td class="ttTitle">future use</td>
    </tr>
    <tr>
        <td  class="ttBody"">
            future use
        </td>
    </tr>--%>
</table><br />
<center>To Close - Click X in upper right corner</center>&nbsp;


</td></tr></table>

</telerik:RadToolTip>
    </form>
</body>
</html>
