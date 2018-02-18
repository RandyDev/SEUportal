<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PayrollSummary.aspx.vb" Inherits="DiversifiedLogistics.PayrollSummary" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../styles/StyleSheet.css" rel="stylesheet" type="text/css" />

        <style type="text/css">
        .lilBlueButton{
            font-size:11px;
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
            .style1
            {
                text-decoration: underline;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeOut="3600" />

    <script type="text/javascript">




        function onRequestStart(sender, args) {
            if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0) {
                args.set_enableAjax(false);
            }
        }

        function disableSubmit() { 
        
        
        }
    </script>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" ClientEvents-OnRequestStart="onRequestStart" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSubmit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="panelTabs" />
                    <telerik:AjaxUpdatedControl ControlID="pnlTitle" />
                    <telerik:AjaxUpdatedControl ControlID="lblerrloca" />
                    <telerik:AjaxUpdatedControl ControlID="btnSubmit" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel Transparency="50" ID="RadAjaxLoadingPanel1"  runat="server">
        <asp:Image id="Image1" runat="server" Width="110" Height="21" ImageUrl="~/images/forkliftani.gif"></asp:Image>
    </telerik:RadAjaxLoadingPanel>

    <div>
    <table > 
    <tr>
        <td valign="top">
            <telerik:RadComboBox ID="cbLocations" Width="150px" Filter="Contains" runat="server" /><br /><asp:Label Visible="false" Text="^^ select location ^^"  ForeColor="Red" Font-Size="Smaller" ID="lblerrloca" runat="server" />
        </td>
        <td valign="top">
            <telerik:RadDatePicker ID="dpStartDate" Width="110px" runat="server" />
            
        </td>
        <td style="font-size:11px; font-family:Arial;">
           <-- Select a date within any pay period  &nbsp; &nbsp; <asp:Label ID="lblspayPeriod" runat="server" Visible="false" />
        </td>
        <td>
            <asp:Button ID="btnSubmit" runat="server" Text="Show Selected Range" />
        </td>
 <td style="padding-left:225px;"><span onmouseover="this.style.cursor='help';"><asp:Label CssClass="lilBlueButton" ID="lblHelp" Text="help with this page" runat="server" /></span></td>
    </tr>
</table><br />
<br />
    <asp:Panel ID="panelTabs" Visible="false" runat="server" >
            <telerik:RadTabStrip ID="RadTabStrip1" runat="server"  MultiPageID="RadMultiPage1"
                SelectedIndex="0" >
                <Tabs>
                    <telerik:RadTab Text="Week 1" />
                    <telerik:RadTab Text="Week 2" />
                    <telerik:RadTab Text="Pay Period" />
                    <telerik:RadTab Text ="Additional Compensation - Detail" />
                </Tabs>
            </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
            <telerik:RadPageView ID="RadPageView1" runat="server">
<telerik:RadGrid ID="RadGrid1" runat="server" AllowSorting="True" AllowAutomaticInserts="false"  ShowFooter="True"
    AutoGenerateColumns="False" Width="1082" Height="525">
    <ExportSettings HideStructureColumns="true" />
    <ClientSettings EnableRowHoverStyle="true">
        <Selecting CellSelectionMode="None"></Selecting>
        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
    </ClientSettings>
<MasterTableView DataKeyNames="ID" CommandItemDisplay="Top">
    <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
    <RowIndicatorColumn Visible="false" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>
    <ExpandCollapseColumn Visible="false" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
<Columns>
    <telerik:GridBoundColumn DataField="ID" Visible="false" ReadOnly="true"></telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="EmpName" HeaderText="Name" UniqueName="EmpName" >
        <HeaderStyle Width="125" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="EmpNumber" HeaderText="Employee #" UniqueName="EmpNumber" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="ulAmount" DataFormatString="{0:C2}" Aggregate="Sum"  HeaderText="Business" UniqueName="ulAmount" >
        <HeaderStyle Width="75" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="PercentHours" Aggregate="Sum" FooterText=" " HeaderText="% Hours" UniqueName="PercentHours" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="HourlyHours" HeaderText="Hourly Hours" FooterText=" " Aggregate="Sum" UniqueName="HourlyHours" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>

    <telerik:GridBoundColumn DataField="TotalHours"  Aggregate="Sum" FooterText=" " HeaderText="Total Hours" UniqueName="TotalHours" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="OTHours" HeaderText="OverTime Hours" FooterText=" " Aggregate="Sum" UniqueName="OTHours" >
        <HeaderStyle Width="50" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>

    <telerik:GridBoundColumn DataField="PayRateHOurly" DataFormatString="{0:C2}"    HeaderText="Hourly Rate" UniqueName="PayRateHOurly" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="PayRatePercentage" DataFormatString="{0:N0}%"  HeaderText="% Rate" UniqueName="PayRatePercentage" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="AddCompAmount" DataFormatString="{0:C2}" Aggregate="Sum"  HeaderText="Additional Compensation" UniqueName="AddCompAmount" >
        <HeaderStyle Width="75" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="RegularPay" DataFormatString="{0:C2}" Aggregate="Sum" FooterText=" " HeaderText="Regular Pay" UniqueName="RegularPay">
        <HeaderStyle Width="95" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="HalfTimePay" Aggregate="Sum" DataFormatString="{0:C2}"  HeaderText="Half Time" UniqueName="HalfTimePay" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
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
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView2" runat="server">
<telerik:RadGrid ID="RadGrid2" runat="server"  ShowFooter="True" AutoGenerateColumns="False" Width="1082px" Height="525" MasterTableView-ShowFooter="true" CellSpacing="0" GridLines="None">
    <ExportSettings HideStructureColumns="true" />
    <ClientSettings EnableRowHoverStyle="true">
        <Selecting CellSelectionMode="None"></Selecting>
        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
    </ClientSettings>
<MasterTableView DataKeyNames="ID" CommandItemDisplay="Top">
    <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
    <RowIndicatorColumn Visible="false" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>
    <ExpandCollapseColumn Visible="false" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
<NoRecordsTemplate>
<br /><br />
<center>
Week 2 has not started ... No Records Found!
</center>
<br /><br />
</NoRecordsTemplate>
<Columns>
    <telerik:GridBoundColumn DataField="ID" Visible="false" ReadOnly="true"></telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="EmpName" HeaderText="Name" UniqueName="EmpName" >
        <HeaderStyle Width="125" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="EmpNumber" HeaderText="Employee #" UniqueName="EmpNumber" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="ulAmount" DataFormatString="{0:C2}" Aggregate="Sum"  HeaderText="Business" UniqueName="ulAmount" >
        <HeaderStyle Width="75" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="PercentHours" Aggregate="Sum" FooterText=" " HeaderText="% Hours" UniqueName="PercentHours" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="HourlyHours" HeaderText="Hourly Hours" FooterText=" " Aggregate="Sum" UniqueName="HourlyHours" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>

    <telerik:GridBoundColumn DataField="TotalHours"  Aggregate="Sum" FooterText=" " HeaderText="Total Hours" UniqueName="TotalHours" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="OTHours" HeaderText="OverTime Hours" FooterText=" " Aggregate="Sum" UniqueName="OTHours" >
        <HeaderStyle Width="50" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>

    <telerik:GridBoundColumn DataField="PayRateHOurly" DataFormatString="{0:C2}"    HeaderText="Hourly Rate" UniqueName="PayRateHOurly" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="PayRatePercentage" DataFormatString="{0:N0}%"  HeaderText="% Rate" UniqueName="PayRatePercentage" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="AddCompAmount" DataFormatString="{0:C2}" Aggregate="Sum"  HeaderText="Additional Compensation" UniqueName="AddCompAmount" >
        <HeaderStyle Width="75" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="RegularPay" DataFormatString="{0:C2}" Aggregate="Sum"  HeaderText="Regular Pay" UniqueName="RegularPay" Visible="true" >
        <HeaderStyle Width="95" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="HalfTimePay" DataFormatString="{0:C2}" Aggregate="Sum" HeaderText="Half Time" UniqueName="HalfTimePay" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
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
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView3" runat="server">

<telerik:RadGrid ID="RadGrid3" runat="server" AllowSorting="True" AllowAutomaticInserts="false"  ShowFooter="True"
    AutoGenerateColumns="False" Width="1082" Height="525">
    <ExportSettings HideStructureColumns="true" />
    <ClientSettings EnableRowHoverStyle="true">
        <Selecting CellSelectionMode="None"></Selecting>
        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
    </ClientSettings>
<MasterTableView DataKeyNames="ID" CommandItemDisplay="Top">
    <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
    <RowIndicatorColumn Visible="false" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>
    <ExpandCollapseColumn Visible="false" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
<Columns>
    <telerik:GridBoundColumn DataField="ID" Visible="false" ReadOnly="true"></telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="EmpName" HeaderText="Name" UniqueName="EmpName" >
        <HeaderStyle Width="125" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="EmpNumber" HeaderText="Employee #" UniqueName="EmpNumber" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="ulAmount" DataFormatString="{0:C2}" Aggregate="Sum"  HeaderText="Business" UniqueName="ulAmount" >
        <HeaderStyle Width="75" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="PercentHours" Aggregate="Sum" FooterText=" " HeaderText="% Hours" UniqueName="PercentHours" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="HourlyHours" HeaderText="Hourly Hours" FooterText=" " Aggregate="Sum" UniqueName="HourlyHours" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>

    <telerik:GridBoundColumn DataField="TotalHours"  Aggregate="Sum" FooterText=" " HeaderText="Total Hours" UniqueName="TotalHours" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="OTHours" HeaderText="OverTime Hours" FooterText=" " Aggregate="Sum" UniqueName="OTHours" >
        <HeaderStyle Width="50" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>

    <telerik:GridBoundColumn DataField="PayRateHOurly" DataFormatString="{0:C2}"    HeaderText="Hourly Rate" UniqueName="PayRateHOurly" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="PayRatePercentage" DataFormatString="{0:N0}%"  HeaderText="% Rate" UniqueName="PayRatePercentage" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="AddCompAmount" DataFormatString="{0:C2}" Aggregate="Sum"  HeaderText="Additional Compensation" UniqueName="AddCompAmount" >
        <HeaderStyle Width="75" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="RegularPay" DataFormatString="{0:C2}" Aggregate="Sum" FooterText=" " HeaderText="Regular Pay" UniqueName="RegularPay">
        <HeaderStyle Width="95" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="HalfTimePay" DataFormatString="{0:C2}" Aggregate="Sum" HeaderText="Half Time" UniqueName="HalfTimePay" >
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
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
        </telerik:RadPageView>
            <telerik:RadPageView runat="server">

<telerik:RadGrid ID="RadGrid4" runat="server" AllowSorting="True" AllowAutomaticInserts="false"  ShowFooter="True"
    AutoGenerateColumns="False" Width="885" Height="525">
    <ExportSettings HideStructureColumns="true" />
    <ClientSettings EnableRowHoverStyle="true">
        <Selecting CellSelectionMode="None"></Selecting>
        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
    </ClientSettings>
<MasterTableView DataKeyNames="EmpNumber" CommandItemDisplay="Top">
    <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>
    <RowIndicatorColumn Visible="false" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>
    <ExpandCollapseColumn Visible="false" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
<Columns>
    <telerik:GridBoundColumn DataField="EmpName" HeaderText="Name" UniqueName="EmpName" runat="server" >
        <HeaderStyle Width="65" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="EmpNumber" HeaderText="EmpNumber" UniqueName="EmpNumber"  runat="server">
        <HeaderStyle Width="45" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="AddCompAmount" DataFormatString="{0:C2}"  Aggregate="Sum" FooterText=" "  HeaderText="Add Comp Amount" UniqueName="AddCompAmount" >
        <HeaderStyle Width="65"  HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        <FooterStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="CompDescription" HeaderText="Comp Description" UniqueName="CompDescription"  runat="server">
        <HeaderStyle Width="100" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="AddCompComments" HeaderText="Add Comp Comments" UniqueName="AddCompComments"  runat="server">
        <HeaderStyle Width="125" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="AddCompStartDate" HeaderText="Add Comp Start Date" DataFormatString="{0:MM/dd/yyyy}" UniqueName="AddCompStartDate"  runat="server">
        <HeaderStyle Width="100" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="AddCompEndDate" HeaderText="Add Comp End Date" DataFormatString="{0:MM/dd/yyyy}" UniqueName="AddCompEndDate"  runat="server">
        <HeaderStyle Width="100" HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </telerik:GridBoundColumn>
</Columns>
    </MasterTableView>
    </telerik:RadGrid>        

            </telerik:RadPageView>
        </telerik:RadMultiPage>

        



</asp:Panel>

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
<span class="ttHeader" >Payroll Summary</span>
<table width="460" ><tr><td style="padding:0 8px;">
<table>
    <tr>
        <td class="ttTitle">Use FULL Screen</td>
    </tr>
    <tr>
        <td class="ttBody">
            The F11 key on your keyboard will toggle most browsers to full screen.<br />
            F11 again to toggle back. &nbsp; &nbsp;(MSIE and Chrome tested)<br />
            You can even try it now, while reading this help file.
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Basic Use</td>
    </tr>
    <tr>
        <td  class="ttBody">
             After selecting a location, use the date picker calendar to select any date within a pay period then click the &#39;Show Selected 
             Range&#39; button. &nbsp;The <asp:Label id="lblPayWeek" runat="server" />
             &nbsp;for each pay week encompassing your selected date will be processed and displayed. &nbsp;
             <span class="style1"><strong>Please be patient</strong></span>, there is a lot to 
             process and <span class="style1"><strong>the report can take as long as 2 or 3 
             minutes</strong></span>. &nbsp; <em>&nbsp;tested: Olean: 34 seconds - Bessemer: 
             2 mins 11 seconds</em><br />&nbsp;</td>
    </tr>
    <tr>
        <td class="ttTitle">About the Payroll Grids</td>
    </tr>
    <tr>
        <td  class="ttBody">
            Note that the 'Business' column DOES NOT represent the business at this location.<br />
            It is the total employee&#39;s business for all those employees Home Based at this 
            location, regardless of where they performed the work. (travelers and TAD&#39;s)
             
            <br />
            &nbsp;
             
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Exporting to Excel</td>
    </tr>
    <tr>
        <td  class="ttBody">
            To export this grid to Excel simply click the 'Export to Excel' icon in the upper <br />
            right corner of the grid and <span style="font-size:12px;font-weight:bold;">Please Be Patient</span>.<br />
            Generating the Excel spreadsheet can take up to 3 full minutes +- and, for now, 
            there is 
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
