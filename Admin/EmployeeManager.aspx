<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EmployeeManager.aspx.vb" Inherits="DiversifiedLogistics.EmployeeManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Employee Manager</title>
    <link href="../styles/StyleSheet.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.lblErr{
    color:Red;
    font-size:13px;
    font-weight:bold;
}
.data{ font-size:14px;font-weight:bold;}
.lbl{font-size:12px;font-weight:normal;} 
.lbl td{padding-left:24px;}
.errMsgRed{
    color:Red;
    font-size:12px;

}
#lblEmpName
{
    color: Blue;
font-weight:bold;
font-size:20px;
}

.cUserName{
color:#555555;
font-size:12px;
}
.ColorMeRed {color:Red;}
.lilBlueButton{
    color:Blue;
    font-weight:normal;
    font-size: 11px;
    padding-left:5px;
}
body{
    font-family:Arial;
}
.picme{
    width: 72px;
    height: 96px;
}
legend{
    font-size:14px;
    font-weight: bold;
}
.subLegend{
    font-size:12px;
    font-weight: bold;
}
.heading{
    font-size:13px;
}

div.RadUpload .ruBrowse
   {
       background-position: 0 -23px;
       width: 65px;
   }
   div.RadUpload_Default .ruFileWrap .ruButtonHover
   {
       background-position: 100% -23px !important;
   }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cbLocations">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lbtnAddNew" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlTabs" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlNewName" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectEmployee" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectLocation" />
                    <telerik:AjaxUpdatedControl ControlID="pnlTitle" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlTabs" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnAddNew">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlTabs" />
                    <telerik:AjaxUpdatedControl ControlID="pnlNewName" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectEmployee" />
                    <telerik:AjaxUpdatedControl ControlID="pnlTitle" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lbtnAddNew" />
                    <telerik:AjaxUpdatedControl ControlID="pnlTabs" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlNewName" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectEmployee" />
                    <telerik:AjaxUpdatedControl ControlID="pnlTitle" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCancel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lbtnAddNew" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlTabs" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectEmployee" />
                    <telerik:AjaxUpdatedControl ControlID="pnlTitle" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSaveChanges">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlTabs" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" />

<table cellpadding="0" cellspacing="0" style="width: 1180px">
    <tr>
        <td valign="top" style="width:225px;"> 
<%--                            <telerik:AjaxUpdatedControl ControlID="errMsg" />--%>
<%--                            <telerik:AjaxUpdatedControl ControlID="errMsg" />--%>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadComboBox ID="cbLocations" Width="150px" Filter="Contains" runat="server" /> 
                    </td>
                    <td style="padding-left:20px;">
                        <asp:LinkButton ToolTip="Click to open New Hire form" ID="lbtnAddNew" runat="server" CommandName="ClearForm" CssClass="lilBlueButton" text="New Hire" />
                    </td>
                </tr>
            </table>            
            <%-- ******************* LEFT COLUMN ******************* --%>
            <%-- ******************* start Page Controls ******************* --%>
<telerik:RadGrid ID="RadGrid1" runat="server" GridLines="None" AutoGenerateColumns="false" >
    <MasterTableView DataKeyNames="ID" AutoGenerateColumns="false" >
        <Columns>
            <telerik:GridBoundColumn DataField="ID" Visible="false" UniqueName="column">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Name" HeaderText="Employee Name" UniqueName="EmpName">
            </telerik:GridBoundColumn>
        </Columns>
    </MasterTableView>
    <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true" >
        <Selecting AllowRowSelect="true" />
    </ClientSettings>
</telerik:RadGrid>

        </td>             <%-- ******************* end Page Controls ******************* --%>
        
        <td></td><%-- ******************* end Left Side employee Grid **********************--%>


        <td valign="top" style="padding-left:11px;"> 
        <%-- ******************* end Left column ******************* --%>

<%-- *******************Location Selector and help link at top of right side **********************--%>
<table width="740" cellpadding="0" cellspacing="0">
    <tr><td><asp:Label ID="lblSelectLocation" runat="server" Text="<<<----  Select Location" /></td>
        <td align="right" valign="top">
            <span onmouseover="this.style.cursor='help'"><asp:Label ID="lblHelpPage" CssClass="lilBlueButton" Text="help with this page" runat="server" /></span>
    </td></tr>
</table>
        <%-- ******************* start RIGHT COLUMN  ******************* --%>

<asp:Panel ID="pnlTabs" runat="server" Visible="false" >
    <asp:Label ID="lblEmpName" runat="server" /><br />
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
<telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" runat="server" 
        SelectedIndex="2">
    <Tabs>
        <telerik:RadTab Text="Employee" Value="Employee" runat="server"></telerik:RadTab>
        <telerik:RadTab Text="Personnel" Value="Personnel" runat="server"></telerik:RadTab>
        <telerik:RadTab Text="Employee ID" Value="EmpID" runat="server" Selected="True"></telerik:RadTab>
        <telerik:RadTab Text="Payroll/SickLeave" Value="Payroll/SickLeave" runat="server"></telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
            </td>
        </tr>
        <tr>
            <td>
<telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="2">
<%-- ******************************************************* --%>
<%-- ******************* PAGE VIEW ONE  ******************* --%>
<%-- ******************************************************* --%>
    <telerik:RadPageView ID="RadPageView1" runat="server" style="width:775px; border:1px solid black;" >
        <table id="img/cerifications">
            <tr>
                <td style="background-color:#dddddd;">
                    <telerik:RadBinaryImage ID="imgMugShot" runat="server" ResizeMode="Fit" />
                </td>
                <td valign="top">
                    <fieldset id="fsComments" runat="server" style="padding:0 5px 5px 5px; font-family:Arial; font-size:11px;">
                        <legend style="font-size:14px; font-weight:bold;">Comments</legend>
                        <telerik:RadTextBox ID="txt_Comments" runat="server" EmptyMessage="Comments" Rows="4" TabIndex="3" TextMode="MultiLine" Width="250px" /><br />
                    </fieldset> 
                </td>
                <td valign="top">
                    <fieldset id="fsCertifications" runat="server" style="padding:0 5px 5px 5px; font-family:Arial; font-size:11px;">
                        <legend>Certifications&nbsp;&nbsp;<asp:LinkButton ID="lbtnEditCerts" runat="server" CssClass="lilBlueButton" text="edit" /></legend>
                        <asp:Label ID="lblCerts" runat="server" />
                    </fieldset> 
                </td>
            </tr>
        </table><%-- ******************* start Picture Comments Certifications **********************--%>
        <table>
            <tr>
                <td valign="top">
                    <fieldset id="fsEmployment" style="padding:5px;">
                        <legend>Employment &nbsp; <span onmouseover="this.style.cursor='help';"><asp:Label ID="lblHelpEmployment" runat="server" CssClass="lilBlueButton" Text="help" /></span>&nbsp; </legend>
                        <table id="titlepaytype">
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlNewName" runat="server" Visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <span class="heading">First Name</span> 
                                                    <asp:Label ID="lblErrFname" runat="server" CssClass="errMsgRed" Text="*" Visible="false" /><br />
                                                    <telerik:RadTextBox ID="txtNewFirstName" runat="server" EmptyMessage="First Name" TabIndex="1" Width="90px" />
                                                </td>
                                                <td>
                                                    <span class="heading">Last Name</span> 
                                                    <asp:Label ID="lblErrLname" runat="server" CssClass="errMsgRed" Text="*" Visible="false" /><br />
                                                    <telerik:RadTextBox ID="txtNewLastName" runat="server" EmptyMessage="Last Name" TabIndex="2" Width="110px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel><%-- ******************* start Employment Grid **********************--%>
                                    <telerik:RadGrid ID="RadGrid2" runat="server" AutoGenerateColumns="False" CellSpacing="0" GridLines="None">
                                        <MasterTableView DataKeyNames="ID" EditMode="InPlace"><CommandItemSettings ExportToPdfText="Export to PDF" />
                                            <RowIndicatorColumn Visible="False" />
                                            <ExpandCollapseColumn Visible="False" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                                    <ItemStyle CssClass="MyImageButton" />
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn DataField="EmployeeID" DataType="System.Guid" ReadOnly="true" UniqueName="EmployeeID" Visible="false"/>
                                                <telerik:GridTemplateColumn DataField="DateOfEmployment" HeaderText="Start Date" UniqueName="DateOfEmployment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDateOfEmployment" runat="server" Width="75" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDatePicker ID="griddpEmploymentDate" runat="server" Width="100px" />
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="DateOfDismiss" HeaderText="End Date" UniqueName="DateOfDismiss">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDateOfDismiss" runat="server" Width="75px" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDatePicker ID="griddpDismissDate" runat="server" Width="100px" />
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="JobTitleID" HeaderText="JobTitle" SortExpression="JobTitle" UniqueName="JobTitle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJobTitle" runat="server" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadComboBox ID="gridcbJobTitle" runat="server" DataSourceID="JobTitlesDataSource" DataTextField="JobTitle" DataValueField="JobTitle" SelectedValue='<%#Bind("Jobtitle")%>' Width="145px" />
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="PayType" HeaderText="PayType" UniqueName="PayType">
                                                    <HeaderStyle Width="80px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPayType" runat="server" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadComboBox ID="gridcbPayType" runat="server" Width="80px">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Hourly" Value="2" />
                                                                <telerik:RadComboBoxItem Text="Percent" Value="1" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="PayRateHourly" DataType="System.Double" HeaderText="Hrly" UniqueName="PayRateHourly">
                                                    <HeaderStyle Width="85px" />
                                                    <ItemTemplate>
                                                        <%# Eval("PayRateHourly")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="num_PayRateHourly" runat="server" EmptyMessage="$" TabIndex="8" Type="Currency" Value='<%# Eval("PayRateHourly") %>' Width="50px" />
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="PayRatePercentage" DataType="System.Double" HeaderText="%" SortExpression="PayRatePercentage" UniqueName="PayRatePercentage">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <%# Eval("PayRatePercentage")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="num_PayRatePercentage" runat="server" EmptyMessage="%" NumberFormat-DecimalDigits="2" TabIndex="9" Type="Percent" Value='<%# Eval("PayRatePercentage") %>' Width="55px" />
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="ID" DataType="System.Guid" HeaderText="ID" ReadOnly="True" UniqueName="ID" Visible="false" />
                                                <telerik:GridBoundColumn DataField="SpecialPay" DataType="System.Double" ReadOnly="True" UniqueName="SpecialPay" visible="false" />
                                                <telerik:GridBoundColumn DataField="HolidayPay" DataType="System.Double" ReadOnly="True" UniqueName="HolidayPay" visible="false" />
                                                <telerik:GridBoundColumn DataField="SalaryPay" DataType="System.Double" ReadOnly="True" UniqueName="SalaryPay" visible="false" />
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="115px" ConfirmDialogType="RadWindow" ConfirmDialogWidth="495px" 
                                                    ConfirmText="Delete this Employment Record!&lt;br /&gt;Are you sure? ... if so, click 'OK' ... AND ... &lt;br/&gt;&lt;br/&gt;Be SURE to check the remaining Start Dates and End Dates.&lt;br/&gt;You may need to adjust dates so that there are no lapses and no overlaps.  &lt;br/&gt;&lt;br/&gt;IMPORTANT!! Be sure the 'End Date' for what will become the next current Employment record is blank so that you don't mistakenly terminate the employee. If this is not clear, click 'Cancel' and contact the IT Department.&lt;br/&gt;&nbsp;" 
                                                    ConfirmTitle="Warning, Will Robinson, Warning!" ImageUrl="~/images/redX.gif" Text="Delete" UniqueName="DeleteColumn">
                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" />
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                            <EditFormSettings>
                                                <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" UniqueName="EditCommandColumn1" />
                                            </EditFormSettings>
                                        </MasterTableView>
                                        <FilterMenu />
                                    </telerik:RadGrid><%-- ******************* Add Employment Button **********************--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="tblGridCommands" class="lilBlueButton" width="100%">
                                        <tr>
                                            <td><asp:LinkButton ID="lbtnAddEmployment" runat="server" Text="Change Employment" /></td>
                                            <td align="right"><asp:LinkButton ID="lbtnShowAllEmployment" runat="server" Text="Show All" /></td>
                                        </tr>
                                    </table><%-- ******************* end Grid Commands Table **********************--%>
                                </td>
                            </tr>
                        </table><%-- ******************* end Employment Grid **********************--%>
                        <%-- ******************* Start Add New Employment Form **********************--%>
                        <asp:Panel ID="pnlAddEmployment" runat="server" Visible="false"><br />
                            <fieldset id="fldAddEmployment" runat="server">
                                <legend>
                                    New Employment &nbsp; <span onmouseover="this.style.cursor='help';">
                                    <asp:Label ID="lblHelpNewEmployment" runat="server" CssClass="lilBlueButton" Text="help" /></span>&nbsp;
                                </legend>
                                <table><%-- ******************* start Row One **********************--%>
                                    <tr><%-- ******************* start Left Column **********************--%>
                                        <td valign="top">
                                            <span class="heading">Job Title</span> 
                                            <asp:Label ID="lblErrJobTitle" runat="server" CssClass="errMsgRed" Text="*" Visible="false" /><br />
                                            <telerik:RadComboBox ID="cb_JobTitle" runat="server" EmptyMessage="Select JobTitle" Filter="Contains" TabIndex="4" /><br />
                                        </td><%-- ******************* end Left Column **********************--%><%-- ******************* start Right Column **********************--%>
                                        <td style="padding-left:10px;" valign="top">
                                            <table id="tblEmpDates" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td valign="top">
                                                        <span class="heading">Start Date</span> 
                                                        <asp:Label ID="lblErrStartDate" runat="server" CssClass="errMsgRed" Text="*" Visible="false" />
                                                        <asp:LinkButton ID="lbtnCalendarPayWeek" runat="server" CommandName=" show Calendar" CssClass="lilBlueButton" Text="Calendar" /><br />
                                                        <telerik:RadComboBox ID="cbPayWeekStartDates" runat="server" AutoPostBack="true" TabIndex="5" Width="200px" />
                                                        <telerik:RadDatePicker ID="dp_DateOfEmployment" runat="server" TabIndex="6" Width="100px" /><br />
                                                    </td>
                                                </tr>
                                            </table><br />
                                        </td><%-- ******************* end Right Column **********************--%>
                                    </tr><%-- ******************* end Row One **********************--%>
                                    <%-- ******************* start Row Two **********************--%>
                                    <tr>
                                        <td valign="top">
                                            <span class="heading">Primary Pay Type</span> 
                                            <asp:Label ID="lblErrPayType" runat="server" CssClass="errMsgRed" Text="*" Visible="false" />&nbsp; 
                                            <span onmouseover="this.style.cursor='help';">
                                               <asp:Label ID="lblHelpPayType" runat="server" CssClass="lilBlueButton" Text="help" />
                                            </span><br />
                                            <telerik:RadComboBox ID="cb_PayType" runat="server" AllowCustomText="true" AutoPostBack="true" EmptyMessage="Select Primary Pay Type" TabIndex="7">
                                                <Items>
                                                    <telerik:RadComboBoxItem runat="server" Text="Hourly payment" Value="2" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Percentage payment" Value="1" />
                                                    <%--                                <telerik:RadComboBoxItem Value="3" Text="Other payment" runat="server" />--%>
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td valign="top">
                                            <fieldset id="fsPayRate" runat="server" style="padding:5px;font-size:12px;">
                                                <legend class="subLegend">Pay Rate(s) <asp:Label ID="lblErrPayRates" runat="server" CssClass="errMsgRed" Text="*" Visible="false" /></legend>
                                                <table cellpadding="0" cellspacing="0" style="font-size:12px;font-family:Arial;">
                                                    <tr>
                                                        <td valign="top">
                                                            Hourly<br /> 
                                                            <telerik:RadNumericTextBox ID="num_PayRateHourly" runat="server" EmptyMessage="$" TabIndex="8" Type="Currency" Width="50px" />
                                                        </td>
                                                        <td style="font-size:10px;padding:0 5px 0 5px;" valign="bottom">and/or</td>
                                                        <td>
                                                            Percent<br /> 
                                                            <telerik:RadNumericTextBox ID="num_PayRatePercentage" runat="server" EmptyMessage="%" NumberFormat-DecimalDigits="2" TabIndex="9" Type="Percent" Value="0" Width="55px" />
                                                        </td>
                                                        <%--                                    <td style="font-size:10px;padding:0 5px 0 5px;" valign="bottom"> OR </td><td>Other<br /> <telerik:RadNumericTextBox ID="num_SalaryPay" runat="server" Type="Currency" Width="65px" /></td>--%>
                                                    </tr>
                                                </table>
                                            </fieldset> 
                                        </td>
                                    </tr><%-- ******************* end Row Two **********************--%>
                                </table>
                            </fieldset> 
                            <telerik:RadToolTip ID="ToolTipHelpNewEmployment" runat="server" Animation="Resize" 
                                EnableShadow="true" HideEvent="ManualClose" IsClientID="true" Position="Center" 
                                RelativeTo="BrowserWindow" ShowEvent="OnClick" TargetControlID="lblHelpNewEmployment">
                                <span class="ttHeader">Add New Employment</span> 
                                <table width="475">
                                    <tr>
                                        <td style="padding:0 8px;"><br />
                                            <table>
                                                <tr>
                                                    <td class="ttTitle">Job Title</td>
                                                </tr>
                                                <tr>
                                                    <td class="ttBody">
                                                        Select the employee&#39;s Job Title from the drop-down list.<br /> 
                                                        The one consideration here is &#39;Unloader Supervisor&#39;. &nbsp;An employee&#39;s Job Title MUST be set 
                                                        to &#39;Unloader Supervisor&#39; if they are to be paid &#39;Supervisor Pay&#39; in a pay period. &nbsp; 
                                                        <em>(see Start Date below)</em> <br />&nbsp; 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ttTitle">Primary Pay Type</td>
                                                </tr>
                                                <tr>
                                                    <td class="ttBody">
                                                        It is important that the Primary Pay Type be set correctly.<br /> Please see the help file for Primary Pay Type. <br />&nbsp; 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ttTitle">Pay Rate(s)</td>
                                                </tr>
                                                <tr>
                                                    <td class="ttBody">
                                                        <b>Hourly</b> - Enter the hourly pay rate.<br /> <b>Percentage</b> - example: for 27 and ¼ percent 
                                                        enter &nbsp;27.25<br /> Selecting the &#39;Percentage&#39; pay type allows you to enter a Percentage rate 
                                                        AND and Hourly rate for those times when you need this employee to clock in &#39;by the hour&#39;.
                                                        (setting the <em>isHourly</em> checkbox when clocking in)<br /> <b>Other</b> - 
                                                        This will open the &#39;Other Pay&#39; window. <br />&nbsp; 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ttTitle">Start Date</td>
                                                </tr>
                                                <tr>
                                                    <td class="ttBody">Select a start date from the calendar control <br />&nbsp; </td>
                                                </tr>
                                            </table>
                                            <center>To Close - Click X in upper right corner</center>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadToolTip>
                            <telerik:RadToolTip ID="ToolTipHelpPayType" runat="server" Animation="Resize" EnableShadow="true" 
                                HideEvent="LeaveToolTip" IsClientID="true" Position="Center" RelativeTo="BrowserWindow" 
                                ShowEvent="OnClick" TargetControlID="lblHelpPayType"><span class="ttHeader">Primary Pay Type</span> 
                                <table width="475">
                                    <tr>
                                        <td style="padding:0 8px;"><br />
                                            <table>
                                                <tr>
                                                    <td class="ttTitle">Important!</td>

                                                </tr>
                                                <tr>
                                                    <td class="ttBody">
                                                        It is important that the &#39;Primary Pay Type&#39; be properly set. 
                                                        <br />Some important payroll calculations will depend on this field. 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ttTitle">Hourly</td>
                                                </tr>
                                                <tr>
                                                    <td class="ttBody">
                                                        When this is set to &#39;Hourly&#39; the employee shall be paid an hourly rate <u>ONLY</u>. <br />
                                                        An &#39;Hourly&#39; employee will NEVER be paid for &#39;load business&#39; and 
                                                        ALL clock-ins will be converted so that the <em>Hourly</em> field is always true (checked). 
                                                        &nbsp;If you intend to pay this employee for <u>any</u> load business in current pay period 
                                                        then you MUST choose the &#39;Percentage&#39; pay type. 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ttTitle">Percentage</td>
                                                </tr>
                                                <tr>
                                                    <td class="ttBody">
                                                        Choose this pay type if you intend to pay the employee a percentage of his/her <br />load work.&nbsp; 
                                                        This pay type will allow you to ALSO enter an hourly rate for those times when you need this employee to 
                                                        clock in &#39;by the hour&#39;. <br />(-- <em>by checking the &#39;Hourly&#39; 
                                                            box on the hand-held when clocking in</em> --) <br />&nbsp; 
                                                    </td>
                                                </tr>
                                            </table>
                                            <center>To Close - Move your mouse away from this help screen</center>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadToolTip>
                        </asp:Panel>
                    </fieldset> 
                    <asp:Panel ID="pnlLocations" runat="server">
                        <fieldset id="fldLocation" runat="server">
                            <legend>Location</legend>
                            <table class="smallTitle" width="100%">
                                <tr>
                                    <td>
                                        Home Location: <br />
                                        <telerik:RadComboBox ID="cbHomeLocation" runat="server" EmptyMessage="Select Location" Filter="Contains" Width="125px" />
                                    </td>
                                    <td>
                                        Current Location: <br />
                                        <telerik:RadComboBox ID="cbCurrentLocation" runat="server" EmptyMessage="Select Location" Filter="Contains" Width="135px" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset> 
                    </asp:Panel>
                </td>
                <td valign="top">
                    <fieldset id="fsAccess" runat="server" style="padding:5px; " >
                        <legend>Access &nbsp; <span onmouseover="this.style.cursor='help';">
                            <asp:Label ID="lblHelpAccess" runat="server" CssClass="lilBlueButton" Text="help" /></span>
                        </legend>
                        <table id="login">
                            <tr>
                                <td>
                                    <span class="heading" title="PIN # for hand-held">PIN*</span> 
                                    <asp:Label ID="lblErrPIN" runat="server" CssClass="errMsgRed" Text="*" Visible="false" />
                                </td>
                                <td style="padding-left:7px;" valign="top">
                                    <span class="heading">LoginID</span> 
                                    <asp:Label ID="lblErrUserName" runat="server" CssClass="errMsgRed" Text="*" />
                                </td>
                                <td style="padding-left:7px;" valign="top"><span class="heading">Password</span> </td>
                                <td style="padding-left:7px;" valign="top"><span class="heading">Payroll #</span> </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadMaskedTextBox ID="txt_rtdsPassword" runat="server" 
                                        EmptyMessage="PIN #" Mask="####" PromptChar="-" Rows="1" 
                                        TabIndex="10" ToolTip="PIN # for hand-held - Last 4 digits of SSN" Width="45px" />
                                </td>
                                <td style="padding-left:7px;" valign="top">
                                    <telerik:RadTextBox ID="txtUserName" runat="server" Enabled="false" ReadOnly="true" Width="55" />
                                </td>
                                <td style="padding-left:7px;" valign="top">
                                    <asp:LinkButton ID="lbtnResetPassword" runat="server" 
                                        CommandName="ResetPassword" CssClass="lilBlueButton" 
                                        Text="Reset Password" />
                                </td>
                                <td style="padding-left:7px;" valign="top">
                                    <telerik:RadTextBox ID="txtpayrollempnum" runat="server" Width="69px" />
                                </td>
                            </tr>
                        </table><%-- *******************Access rtds **********************--%>
                        <fieldset id="fsrtdsAccess" runat="server" class="heading" style="padding:3px;" visible="false">
                            <legend class="subLegend">RTDS Hand Held</legend>
                            <table>
                                <tr>
                                    <td><asp:CheckBox ID="cbrtdsAccountLocked" runat="server" Text="Account Locked" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="cbARMpda" runat="server" Text="PDA app" />&nbsp; 
                                        <asp:CheckBox ID="cbARMweb" runat="server" Text="WEB app" />&nbsp; 
                                        <asp:CheckBox ID="cbARMadministrator" runat="server" Text="Administrator" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset> <%-- *******************Access seu.div-log **********************--%>
                        <fieldset id="fsseuAccess" runat="server" class="heading" style="padding:3px;">
                            <legend class="subLegend">SEU.Div-Log.com </legend>
                            <table>
                                <tr>
                                    <td style="width:120px;">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td><asp:Button ID="btnUnlockUser" runat="server" CommandName="UnlockUser" Text="Unlock Account" Visible="false" /></td>
                                                <td><asp:CheckBox ID="cbdivlogIsApproved" runat="server" Text="Login Approved" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtnUpdateSEU" runat="server" CssClass="lilBlueButton" Text="Create SEU Account" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblEmailAddress" runat="server" Text="eMail Address: &nbsp;" /><br />
                            <telerik:RadTextBox ID="txt_eMail" runat="server" EmptyMessage="example-&gt; Login@Div-Log.com" TabIndex="11" Width="200px" />
                            <asp:Label ID="lblErrEmail" runat="server" CssClass="errMsgRed" Text="*" Visible="false" />
                            <table>
                                <tr>
                                    <asp:Repeater ID="UsersRoleList" runat="server">
                                        <ItemTemplate>
                                            <td><asp:CheckBox ID="RoleCheckBox" runat="server" AutoPostBack="false" Text="<%# Container.DataItem %>" /></td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </table>
                        </fieldset> 
                    </fieldset> 
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       </td><%-- ******************* end Right Side Access **********************--%>

            </tr></table><telerik:RadToolTip ID="ToolTipHelpAccess" runat="server" IsClientID="true" TargetControlID="lblHelpAccess" 
      RelativeTo="BrowserWindow" ShowEvent="OnClick" Position='Center' HideEvent="ManualClose" 
      Animation="Resize" EnableShadow="true" ><span class="ttHeader">Access section</span> 
                                                        
                                                        <table width="475" >
                                                            <tr>
                                                                <td style="padding:0 8px;">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <span style="font-size:11px;">** New Hire will only need enter PIN number in this section.</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ttTitle">PIN number</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ttBody">
                                                                                Corporate recommends using the last 4 digits of the employee&#39;s Social Security # 
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ttTitle">LoginID</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ttBody">
                                                                                This number is auto-generated and cannot be changed. 
                                                                                &#160;The default number will generally be a location ident followed by the next sequential 
                                                                                employee number.&#160; For exceptions, contact the IT Department. 
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ttTitle">Password</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ttBody">
                                                                                Initially auto-generated in the format of their LoginID followed by the word &#39;welcome&#39; 
                                                                                in lowercase characters. &#160;example: oc2345welcome<br /> Using their LoginID and this 
                                                                                password the employee can log into the <br />SEU.Div-Log.com website to view their hours, 
                                                                                earnings and more. <br />If the employee has changed the default password, you will see a
                                                                                &#39;Reset Password&#39; link in place of the password. &#160;Click the link IF you have 
                                                                                need to reset the employee&#39;s password to the default new password. 
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ttTitle">SEU.Div-Log.com ---</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ttTitle">Login Approved</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ttBody">UN-check this box to prevent the employee from logging in to the web site. </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ttTitle">eMail Address</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ttBody">
                                                                                A default NON-working eMail address is initially assigned to each employee.<br /> 
                                                                                If the employee has another eMail address, enter it in the space provided. 
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ttTitle">Employee Role</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ttBody">
                                                                                Select the employee&#39;s current Role (or position) by selecting the appropriate check box(es). 
                                                                                &#160; This will determine the employee&#39;s access level on this web portal. <br />&nbsp; 
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <center>To Close - Click X in upper right corner</center>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        </telerik:RadToolTip>
        
        <telerik:RadToolTip ID="ToolTipEmploymentHelp" runat="server" IsClientID="true" TargetControlID="lblHelpEmployment" 
                        RelativeTo="BrowserWindow" ShowEvent="OnClick" Position='Center' HideEvent="ManualClose" 
                        Animation="Resize" EnableShadow="true"><span class="ttHeader">Employment</span> 
          <table width="600" >
              <tr>
                  <td style="padding:0 8px;"><br />
                      <table>
                          <tr>
                              <td class="ttBody">
                                  When a new employee is hired and EACH time that the employee&#39;s job title, pay type and/or pay rate is changed, a 
                                  NEW &#39;Employment&#39; record is created and added to the grid. <br />&nbsp; 
                              </td>
                          </tr>
                          <tr>
                              <td class="ttTitle">Employment Grid</td>
                          </tr>
                          <tr>
                              <td class="ttBody">
                                  The employment grid displays the three most recent employment records with the most recent record on top.&#160; 
                                  IF there are more than three records, you will see a &#39;Show All&#39; link in the bottom right corner of the grid.<br /> 
                                  To change an employee's job title, pay type or pay rate, click the '<span style="color:Blue;">Change Employment</span>' 
                                  link below the grid. <br />
                                  &nbsp; 
                              </td>
                          </tr>
                          <tr>
                              <td class="ttTitle">Change Employment</td>
                          </tr>
                          <tr>
                              <td class="ttBody">
                                  Clicking this link will display a form to change or set a new Job Title, Primary Pay Type and Pay Rate(s) for this employee.
                                  &#160;A new employment record will be created and will become effective on your selected &#39;Start Date&#39;. 
                                  (see form) <br />&nbsp; 
                              </td>
                          </tr>
                      </table>
                      <center>To Close - Click X in upper right corner</center>
                  </td>
              </tr>
          </table>
      </telerik:RadToolTip>

    </telerik:RadPageView>
<%-- ******************************************************* --%>
<%-- ******************* PAGE VIEW TWO  ******************* --%>
<%-- ******************************************************* --%>
    <telerik:RadPageView ID="RadPageView2" runat="server" style="border:1px solid black;"><br/>
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>First Name<br /><telerik:RadTextBox ID="txt_rtdsFirstName" TabIndex="1" EmptyMessage="First Name" Width="90px" runat="server" /></td>
                            <td>Last Name<br /><telerik:RadTextBox ID="txt_rtdsLastName" TabIndex="2" EmptyMessage="Last Name" Width="110px" runat="server" /></td>
                            <td>Race<br /> 
                                <telerik:RadComboBox ID="cbRace" EmptyMessage="Select Race" Width="215" Runat="server">
                <Items>
                    <telerik:RadComboBoxItem Value="American Indian/Alasks Native (not Hispanic or Latino)" Text="American Indian/Alasks Native (not Hispanic or Latino)" />
                    <telerik:RadComboBoxItem Value="Asian (not Hispanic or Latino)" Text="Asian (not Hispanic or Latino)" />
                    <telerik:RadComboBoxItem Value="Black (not Hispanic or Latino)" Text="Black (not Hispanic or Latino)" />
                    <telerik:RadComboBoxItem Value="Hispanic or Latino" Text="Hispanic or Latino" />
                    <telerik:RadComboBoxItem Value="Native Hawaiin/ Pacific Islander (not Hispanic or Latino)" Text="Native Hawaiin/ Pacific Islander (not Hispanic or Latino)" />
                    <telerik:RadComboBoxItem Value="White (not Hispanic or Latino)" Text="White (not Hispanic or Latino)" />
                    <telerik:RadComboBoxItem Value="Two or more races (not Hispanic or Latino)" Text="Two or more races (not Hispanic or Latino)" />
                </Items>
                                </telerik:RadComboBox><br />
                                <asp:RadioButtonList ID="radioGender" runat="server">
                                    <asp:ListItem text="Female" Value="Female" />
                                    <asp:ListItem Text="Male" Value="Male" />
                                    <asp:ListItem Text="Other" Value="Other" />
                                 </asp:RadioButtonList> 
                               <telerik:RadDateInput ID="RadDateInputBirthdate" EmptyMessage="DOB - MM/DD/YYYY" Runat="server" />

                            </td>
                            <td style="padding-left:24px;">SSN<br /> 
                                <telerik:RadMaskedTextBox ID="txtSSN" Width="85" EmptyMessage="SSN" HideOnBlur="true" Mask="###-##-####" PromptChar="_" DisplayMask="xxx-xx-####" runat="server"><ClientEvents OnLoad="initializeForSSN" /></telerik:RadMaskedTextBox></td></tr></table>
                    <table>
                        <tr>
                            <td valign="top">Address<br /> 
                                <table>
                                    <tr>
                                        <td colspan="3"><telerik:RadTextBox ID="txtAddress" EmptyMessage="Street Address" Width="207" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td><telerik:RadTextBox ID="txtCity" Width="110" EmptyMessage="enter zip code first" Enabled="false" runat="server" /></td>
                                        <td>
                                            <telerik:RadTextBox ID="txtState" MaxLength="2" style="text-transform: uppercase;"  Width="30" EmptyMessage="--" Enabled="false" runat="server" >
                                                <ClientEvents OnValueChanged="ValueChanged" /></telerik:RadTextBox></td><td><telerik:RadMaskedTextBox ID="txtZip" HideOnBlur="true" 
                                                    EmptyMessage="Zip" Mask="#####" Width="45" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td style="padding-left:4px;">
                                            Home Phone<br /> 
                                            <telerik:RadMaskedTextBox ID="txtPrimaryPhone" HideOnBlur="true" Width="100" EmptyMessage="Phone #" 
                                                Mask="(###) ###-####" runat="server" />
                                        </td>
                                        <td>
                                            Cell&nbsp;/&nbsp;Alt&nbsp;Phone<br /> 
                                            <telerik:RadMaskedTextBox ID="txtAltPhone" HideOnBlur="true" Width="100" EmptyMessage="Alt Phone #" 
                                                Mask="(###) ###-####" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <font color="red">Emergency Contact Info</font><br /> 
                                <table>
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtEmergencyContactName" Width="200" EmptyMessage="Emergency Contact" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadMaskedTextBox ID="txtEmergencyContactPhone" HideOnBlur="true" Width="100" EmptyMessage="Phone" 
                                                Mask="(###) ###-####" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtEmergencyContactAddress" Width="200" EmptyMessage="Address" TextMode="MultiLine" Rows="3" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="padding-left:12px; padding-right:12px;" valign="top">
                    <div style="width:300px; text-align:center;"><center>Online Documents <br /><br />
                        <font color="gray">future use</font></center>
                    </div>
                </td>
            </tr>
        </table><br />
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td align="right">
                    <asp:LinkButton ID="lbtnTerminateEmployment" runat="server" CommandName="Delete" CssClass="lilBlueButton" 
                        OnClientClick="if (!confirm('You are about to terminate this employee.\nAre you sure you want to terminate this employee?')) return false;" 
                        Text="Terminate Employee" />
                    </td>

            </tr>
            <tr>
                <td style="padding-left:12px;">
                    <telerik:RadTextBox ID="txtTerminationReason" runat="server" EmptyMessage="Termination Reason" Rows="3" TextMode="MultiLine" Width="328px" />
                </td>
            </tr>
        </table>
        <%--<table>
          <tr>
            <td>
                <telerik:RadComboBox ID="cbEmployedBy" Runat="server" Text="Race">
                    <Items>
                        <telerik:RadComboBoxItem Value="1" Text="Diversified Logistics" />
                        <telerik:RadComboBoxItem Value="2" Text="Diversified Pallets, Inc" />
                        <telerik:RadComboBoxItem Value="3" Text="First Coast Pallets" />
                        <telerik:RadComboBoxItem Value="9" Text="Southeast Unloading" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td>
                <asp:RadioButtonList ID="rbFullPartTime" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Text="Full Time" Value="Full Time" Selected="True" />
                    <asp:ListItem Text="Part Time" Value="Part Time" />
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr> 
        <td colspan="2">--%>

    </telerik:RadPageView>
<%-- ******************************************************* --%>
<%-- ******************* PAGE VIEW PHOTO ID  ******************* --%>
<%-- ******************************************************* --%>
    <telerik:RadPageView ID="RadPageViewPhotoID" runat="server" style="border:1px solid black;"><br /><table><tr><td align="center" valign="middle"><table align="center"><tr><td align="center"><div id="divPhoto2" runat="server" style=" background-color:#cccccc; height:120px; width:90px; max-height:120px; max-width:90px;"><telerik:RadBinaryImage ID="imgMugShot2" Width="90px" Height="120px" ResizeMode="Fit" runat="server" /></div></td></tr><tr><td align="center"><asp:Button ID="btnChangePhoto" runat="server" Text="Upload New Picture" Visible="false" /><telerik:RadAsyncUpload  runat="server" ID="AsyncUpload1" InputSize="16"  OnClientFileUploaded="fileUploaded"
            AllowedFileExtensions="jpeg,jpg,gif,png,bmp" OnClientValidationFailed="validationFailed" 
                                 Width="225px" /><br /><asp:Button ID="btnImageCancel" runat="server" Text="Cancel Upload" Visible="false" /></td></tr></table></td><td style="padding-left:50px;padding-bottom:20px"><table><tr><td><div style="margin:auto;width:308px;height:195px; background-repeat:no-repeat; background-image:url('../images/Div-ID-BG.png');"><table align="center" width="237"><tr><td align="center"><br /><br /><br /><br /><br /><font size="2" color="gray">&nbsp;future use</font> </td></tr></table></div></td></tr></table></td></tr></table></telerik:RadPageView>
<%-- ******************************************************* --%>
<%-- ******************* PAGE VIEW Payroll  ******************* --%>
<%-- ******************************************************* --%>
    <telerik:RadPageView ID="RadPageViewPayroll" runat="server" style="color:black; border:1px solid black;" Visible="false">
        <div style="width:580px; height:245px;padding:14px;">
            <br />
            <table>
                <tr>
                    <td style="text-align:right;padding-right:7px;">Accumulated Sick Leave: </td>
                    <td><telerik:radlabel ID="lblSickLeaveEarned" runat="server" />&nbsp;hrs </td>
                </tr>
                <tr>
                    <td style="text-align:right;padding-right:7px;">Hours Available: </td>
                    <td><telerik:radlabel ID="lblCurrentBalance" runat="server" /></td>
                </tr>
                <tr>
                    <td style="text-align:right;padding-right:7px;">Authorize Sick Leave: (min <asp:Label ID="lblMinLeave" runat="server" />&nbsp;hrs)</td>
                    <td><telerik:RadNumericTextBox id="numSickLeaveAuthorize" runat="server" NumberFormat-DecimalDigits="0" Width="75px" /></td>
                </tr>
                <tr>
                    <td style="text-align:right;padding-right:7px;">First Day Missed: </td>
                    <td><telerik:RadDatePicker ID="dpStartSickLeave" runat="server" /></td>
                </tr>
                <tr>
                    <td style="text-align:right;padding-right:7px;">Last Day Missed: </td>
                    <td><telerik:RadDatePicker ID="dpEndSickLeave" runat="server" /></td>
                </tr>
                <tr>
                    <td style="padding-right:7px;vertical-align:top;">Comments: </td>
                    <td><telerik:RadTextBox ID="txtSickLeaveComments" TextMode="MultiLine" Rows="5" Width="250px" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br /><br /><br />
<telerik:RadGrid ID="gridUsedSickLeave" runat="server" Visible="false">
            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
            <MasterTableView AutoGenerateColumns="False" DataKeyNames ="employeeID,timeStamp">
                <Columns>
                    <telerik:GridBoundColumn DataField="employeeID" ReadOnly="true" Visible="false" DataType="System.Guid" HeaderText="employeeID" UniqueName="employeeID" />
                    <telerik:GridBoundColumn DataField="firstDay" DataFormatString="{0:M/d/yyyy}" DataType="System.DateTime" HeaderText="firstDay" UniqueName="firstDay" />
                    <telerik:GridBoundColumn DataField="lastDay" DataFormatString="{0:M/d/yyyy}" DataType="System.DateTime" HeaderText="lastDay" UniqueName="lastDa" />
                    <telerik:GridBoundColumn DataField="hoursUsed" DataFormatString="{0:N0}" DataType="System.Decimal" HeaderText="hoursUsed" UniqueName="hoursUsed"/>
                    <telerik:GridBoundColumn DataField="notes" HeaderText="notes" UniqueName="notes">
                        <HeaderStyle Width="200px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn DataField="createdBy" ReadOnly="true" DataType="System.Guid" HeaderText="Auth By" UniqueName="createdBy">
                        <HeaderStyle Width="100px" />
                        <ItemTemplate>
                            <telerik:RadLabel ID="lblAuthName" runat="server" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="timeStamp" DataFormatString="{0:dd/MMM-hh:mm tt}" ReadOnly="true" DataType="System.DateTime" HeaderText="timeStamp" UniqueName="timeStamp" />
                    <telerik:GridButtonColumn ImageUrl="~/images/redX.gif" 
                        ConfirmDialogWidth="495px"  ConfirmDialogHeight="115px" ConfirmTitle="Warning, Will Robinson, Warning!" 
                        ConfirmText="Delete this Sick Leave Record!<br />Are you sure? <br/>&nbsp;" ConfirmDialogType="RadWindow"
                        ButtonType="ImageButton" CommandName="Delete" Text="Delete" UniqueName="DeleteColumn">
                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                    </telerik:GridButtonColumn>
               </Columns>
            </MasterTableView>
        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </div>

        </telerik:RadPageView>
<%-- ******************************************************* --%>
</telerik:RadMultiPage>
        <table width="100%">
        <tr>
            <td width="50%">
                <div id="errDiv" style="padding:7px;">
                    <asp:Label ID="errMsg" CssClass="errMsgRed" runat="server" Visible="false" />
                </div>
            </td>
            <td align="right">
                <asp:Button ID="btnSaveChanges" TabIndex="12" runat="server" Text="Save Changes" />
                <telerik:RadButton ID="btnAuthSickLeave" Text="Authorize SickLeave" runat="server" />
            </td>
        <td align="right">
            <asp:Button ID="btnCancel" TabIndex="13" runat="server" Text="Cancel / Close" />
        </td>
        </tr>
    </table>

<%-- ******************************************************* --%>
            </asp:Panel>

<asp:Label ID="lblSelectEmployee" runat="server" Text="<br /><br /><br /><br /><<<----  Select an Employee from left" />

<asp:Panel ID="pnlTitle" runat="server" Visible="false">
<br /><br /><br/><br /><br/>
            <table cellpadding="0" cellspacing="0" align="center" style="font-family:Arial;"><tr>
            <td align="center" style="font-size:26px;"><asp:Label ID="lblPageTitle" Text="" runat="server" /></td>
            </tr></table>
</asp:Panel>

        </td>
    </tr>

</table>

     <br />
<%-- ******************************************************* --%>
<%-- ********************** TOOL TIPS ********************** --%>
<%-- ******************************************************* --%>
    <telerik:RadToolTip ID="RadToolTip1" runat="server" IsClientID="true" TargetControlID="lblHelpPage" 
                        RelativeTo="BrowserWindow" ShowEvent="OnClick" Position='Center' HideEvent="ManualClose"
                        Animation="Resize" EnableShadow="true"  >
                        <span class="ttHeader">Employee Manager</span> <table width="600">
                            <tr>
                                <td class="ttTitle">New Hire</td></tr><tr>
                                            <td class="ttBody">
                                                To add a new hire, click the <span style="color:Blue;">New Hire</span> link above the employee list next to location selector. <em>(a location must be selected)</em> &nbsp;Also, the link will NOT be visible while an employee record is being
                                                displayed or edited. &nbsp;If an employee record is open, you must click the 'Cancel / Close' button to make the link visible again. </td></tr><tr>
                                <td class="ttTitle">Employee Name</td></tr><tr>
                                            <td class="ttBody">
                                                To edit the employee's name, click on their name (the big blue characters). <br />After making changes, click 'Save Changes' button. </td></tr><tr>
                                <td class="ttTitle">Comments</td></tr><tr>
                                <td class="ttBody">
                                    Enter comments as you wish. &nbsp;Click 'Save Changes' button. </td></tr><tr>
                                <td class="ttTitle">Certifications</td></tr><tr>
                                <td class="ttBody">
                                    To help you in keeping them current, certification dates will appear in <span style="color:black;">Black</span>, then <span style="color:Orange">Orange</span>, 
                                    then <span style="color:Red;">Red</span> as they approach expiration. 
                                    &nbsp;Click the <span style="color:Blue;">edit</span> link to manage the employee's certifications. &nbsp;You DO NOT have to
                                    click the save button after editing certifications. </td></tr><tr>
                                <td class="ttTitle">Employment / Location</td></tr><tr>
                                <td class="ttBody">
                                    Click the <span style="color:Blue;">help</span> link in the 'Employment' section. </td></tr><tr>
                                <td class="ttTitle">Access</td></tr><tr>
                                <td class="ttBody">
                                    Click the <span style="color:Blue;">help</span> link in the 'Access' section. </td></tr><tr>
                                <td class="ttTitle">MugShot&reg;</td></tr><tr>
                                <td class="ttBody">
                                    PLEASE provide, as best possible, a nice "Portrait" style picture. &nbsp; If you have any questions as to how to 
                                    frame an employee MugShot&reg; please study your driver license picture. &nbsp;Close-up, neutral background, portrait orientation 
                                    (camera / phone on it's side) 
                                    &nbsp;At the point we begin to issue ID Badges, we will use this image. &nbsp;You DO NOT have to click the save button after uploading image. <br />&nbsp; </td></tr></table><center>To Close - Click X in upper right corner</center></telerik:RadToolTip>



<telerik:RadWindowManager ID="RadWindowManager1" runat="server" VisibleStatusbar="false" >
<Windows>
<telerik:RadWindow ID="wCertEditor" Height="400px" Width="520px"  
    ShowContentDuringLoad="false"   runat="server" Modal="true" 
    Title="Div-Log Certification Manager"
    OnClientClose = "OnClientCloseCertEditor"
    Behaviors="Move, Resize, Close" />
<telerik:RadWindow ID="wEmploymentEditor" Height="380px" Width="520px"  
    ShowContentDuringLoad="false"   runat="server" Modal="true" 
    Title="SEU Employment Editor" 
    OnClientClose = "OnClientCloseEmploymentEditor"
    Behaviors="Move, Resize, Close" />
<telerik:RadWindow ID="wEmpTAD" Height="500px" Width="410px"  
    ShowContentDuringLoad="true"  runat="server" Modal="True" 
    Title="Employments and TADs"
    Left="400"
    Top="-100"
    OnClientClose = "OnClientCloseEmpTAD"
    Behaviors="Move, Close" />
</Windows>
    </telerik:RadWindowManager>

<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
<script type="text/javascript">
    function openCertEditor(arg) {
        document.getElementById("<%= btnSaveChanges.ClientID %>").disabled = true;
        var oManager = GetRadWindowManager();
        var loca = "editCerts.aspx?empID=" + arg
        oManager.open(loca, "wCertEditor");

    }
    function OnClientCloseCertEditor(sender, args) {
        document.getElementById("<%= btnSaveChanges.ClientID %>").disabled = false;
        var arg = 'closed';
        var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
        ajaxManager.ajaxRequest("CertEdit:" + arg);

    }

    function openEmploymentEditor(arg) {
        document.getElementById("<%= btnSaveChanges.ClientID %>").disabled = true;
        var oManager = GetRadWindowManager();
        var loca = "ChangeEmployment.aspx?empID=" + arg
        oManager.open(loca, "wEmploymentEditor");
    }
    function OnClientCloseEmploymentEditor(sender, args) {
        sender.setUrl("../seuLoader.aspx");
        document.getElementById("<%= btnSaveChanges.ClientID %>").disabled = false;
        //        var arg = 'closed';
        var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
        ajaxManager.ajaxRequest("EmploymentEdit:" + args);

    }

    function openEmpTAD() {
        //        var vnum = document.getElementById("< %= vid.ClientID % >").innerText;
        var oManager = GetRadWindowManager();
        var loca = "empTAD.aspx";
        oManager.open(loca, "wEmpTAD");
    }

    function OnClientCloseEmpTAD(sender, args) {
        if (args.get_argument() != null) {
            var arg = args.get_argument();
            var sarg = arg.split(":");
            //            var VendorNumber = $find("< %= txtVendorNumber.ClientID % >");
            //            var VendorName = document.getElementById("< %= txtVendorName.ClientID % >");
            //            VendorNumber.set_value(sarg[2]);
            //            VendorName.innerText = sarg[2] + "-" + sarg[3];
            //            VendorName.style.color = 'blue';
            //            VendorName.style.fontSize = '12px';
            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
            ajaxManager.ajaxRequest(arg);

        }
        //            $find("< %= RadAjaxManager1.ClientID % >").ajaxRequest(sarg);



        //                    var dv = document.getElementById("divCompInfo")
        //        dv.style.visibility = "visible";

        //        if (args.get_argument() != null) {
        //            var arg = args.get_argument();
        //            var sarg = "Client:" + arg;
        //            $find("< %= RadAjaxManager1.ClientID % >").ajaxRequest(sarg);
        //            
        //            

    }
    //    function openUnloaders(arg) {
    //        var loca = "selectUnloaders.aspx?woid=" + arg;
    //        var oManager = GetRadWindowManager();
    //        oManager.open(loca, "wUnloader");
    //    }

    //    function OnClientCloseUnloader(sender, args) {
    //        if (args.get_argument() != null) {
    //            var arg = args.get_argument();
    //            var Unloaders = document.getElementById("< %= txtUnloaders.ClientID % >")
    //            Unloaders.innerText = arg.split("|", 2)[0];
    //            Unloaders.style.color = 'blue';
    //            Unloaders.style.fontSize = '12px';
    //            var ajaxManager = $find("< %= RadAjaxManager1.ClientID % >");
    //            ajaxManager.ajaxRequest("Unloader:" + arg);

    //        }
    //    }

    //    function OnClientDropDownClosed(sender, args) {
    //        sender.clearItems();
    //        //        alert(sender);
    //        if (args.get_domEvent().stopPropagation)
    //            args.get_domEvent().stopPropagation();
    //    }

    //    function btnSaveClick() {
    //        var SavedBtn = document.getElementById("< %= lblChangesSaved.ClientID % >");
    //        SavedBtn.style.visibility = 'visible';

    //        var VendorName = document.getElementById("< %= txtVendorName.ClientID % >");
    //        VendorName.style.color = 'black';
    //        var CarrierName = document.getElementById("< %= lblCarrierNamev.ClientID % >");
    //        CarrierName.style.color = 'black';
    //        var UnloaderList = document.getElementById("< %= txtUnloaders.ClientID % >");
    //        UnloaderList.style.color = 'black';

    //    }
    function decOnly(i) {
        var t = i.value;
        if (t.length > 0) {
            t = t.replace(/[^\d]+/g, '');
        }
        var s = t.split('.');
        if (s.length > 1) {
            s[1] = s[0] + '.' + s[1];
            s.shift(s);
        }
        i.value = s.join('');
    }

    //                function RowClick(sender, eventArgs) {
    //                    sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
    //                }
    //                
    //                function RowSelected(sender, eventArgs) {
    //                    alert(eventArgs.getDataKeyValue("ID="));
    ////                    sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
    //                }
    //    function btnEditClick() {
    //        document.getElementById("< %=pnlWOinfo.ClientID% >").style.display = "none";
    //        document.getElementById("< %=pnlWOedit.ClientID% >").style.display = "inline";
    //        return false;
    //    }
    //    function btnCancelClick() {
    //        document.getElementById("< %=pnlWOinfo.ClientID% >").style.display = "inline";
    //        document.getElementById("< %=pnlWOedit.ClientID% >").style.display = "none";
    //        return false;
    //    }


    function ZipIt(i) {
        var t = i.value;
        if (t.length > 0) {
            t = t.replace(/[^\d]+/g, '');
            //                t = t.replace(/[^\da-zA-Z]+/g, '');
            i.value = t;
        }
        if (t.length == 5) {
            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");

            ajaxManager.ajaxRequest("ZipCodeLookup:" + t);
        }
    }


       function ValueChanged(sender, args) {

if (args.NewValue.length > 0) {

           sender.SetValue(args.NewValue.toUpperCase());
}
       }

</script>



</telerik:RadCodeBlock>

    <script type="text/javascript">

        function initializeForSSN(sender, args) {
            //indicates the number of visible digits from the end of the value
            sender._visibleDigits = 4;

            //cache the original get_displayValue method and overwrite it
            sender._originalGetDisplayValue = sender.get_displayValue;
            sender.get_displayValue = Function.createDelegate(sender, function () {
                var value = this.get_valueWithPrompt();
                if (!isNaN(parseInt(value)))    //if value can be parsed into a number
                {
                    //cache the original get_valueWithPrompt method and overwrite it
                    //we need this because the original get_displayValue uses this method
                    //to retrieve the value to be displayed
                    this._originalGetValueWithPrompt = this.get_valueWithPrompt;
                    this.get_valueWithPrompt = Function.createDelegate(this, function () {
                        var original = this._originalGetValueWithPrompt.call(this);

                        if (!isNaN(parseInt(original))) {
                            //if value can be parsed in to a number and it has
                            //more digits than the visible digits number, we
                            //want to return the last N digits, where N = this._visibleDigits
                            if (original.length > this._visibleDigits) {
                                original = original.substr(original.length - this._visibleDigits);
                            }
                        }

                        return original;
                    });

                    //call the original get_displayValue with the overwritten get_valueWithPrompt.
                    var displayValue = this._originalGetDisplayValue.call(this);
                    //restore the original get_valueWithPrompt method
                    this.get_valueWithPrompt = this._originalGetValueWithPrompt;

                    return displayValue;
                }

                //if original value with prompt cannot be parsed into a number,
                //fall back to the original get_displayValue method
                return this._originalGetDisplayValue.call(this);
            });
        }

        function xxinitializeForSSN(sender, args) {
            sender._originalGetDisplayValue = sender.get_displayValue;
            sender.get_displayValue = function () {
                var value = this.get_valueWithPrompt();

                // hack: add this part to clip the display value to the last 4 characters
                if (value.length > 4) {
                    value = value.substr(value.length - 4);
                }
                //end of hack

                while (value.length < this._displayLength) {
                    value += this.get_promptChar();
                }

                this._UpdateDisplayPartsInRange(value, 0, this._displayLength);
                if (this._displayParts)
                    return this._GetVisibleValues(this._displayParts);
                else
                    return this._GetVisibleValues(this._parts);
            }
        }

        function fileUploaded(sender, args) {
            $find('RadAjaxManager1').ajaxRequest();
            $telerik.$(".invalid").html("Nice Try");
            sender.deleteFileInputAt(0);
        }

        function validationFailed(sender, args) {
            $telerik.$(".invalid")
                .html("Invalid extension, please choose an image file");
            sender.deleteFileInputAt(0);
        }


    </script>

    </div>
        <asp:SqlDataSource ID="JobTitlesDataSource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:rtdsConnectionString %>" 
    SelectCommand="SELECT JobTitle.JobTitleID, JobTitle.JobTitle FROM JobTitle INNER JOIN LocationJobTitle ON JobTitle.JobTitleID = LocationJobTitle.JobTitleID WHERE (LocationJobTitle.LocationID = @locaid) ORDER BY JobTitle" >
            <SelectParameters>
                <asp:ControlParameter ControlID="cbLocations" Name="locaID" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>

    </form>
</body>
</html>
