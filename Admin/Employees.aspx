<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Employees.aspx.vb" Inherits="DiversifiedLogistics.Employees" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
.lbtnEmpName{
    color:Black;
    text-decoration:none;
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
    .style1
    {
        width: 462px;
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
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlWOedit" LoadingPanelID="RadAjaxLoadingPanel1" />
<%--                            <telerik:AjaxUpdatedControl ControlID="errMsg" />--%>
                    <telerik:AjaxUpdatedControl ControlID="lblCerts" />
                    <telerik:AjaxUpdatedControl ControlID="btnChangePhoto" />
                    <telerik:AjaxUpdatedControl ControlID="AsyncUpload1" />
                    <telerik:AjaxUpdatedControl ControlID="btnImageCancel" />
                    <telerik:AjaxUpdatedControl ControlID="btnSaveChanges" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cbLocations">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lbtnAddNew" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectLocation" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectEmployee" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWOedit" />
                    <telerik:AjaxUpdatedControl ControlID="pnlTitle" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnAddNew">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectEmployee" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWOedit" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lbtnAddNew" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectEmployee" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWOedit" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnSaveChanges" />
                    <telerik:AjaxUpdatedControl ControlID="btnCancel" />
                    <telerik:AjaxUpdatedControl ControlID="btnChangePhoto" />
                    <telerik:AjaxUpdatedControl ControlID="fldLocation" />
                    <telerik:AjaxUpdatedControl ControlID="lbtnAddEmployment" />
                    <telerik:AjaxUpdatedControl ControlID="fsAccess" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnChangeName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txt_rtdsFirstName" />
                    <telerik:AjaxUpdatedControl ControlID="txt_rtdsLastName" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cb_PayType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="num_PayRateHourly" />
                    <telerik:AjaxUpdatedControl ControlID="num_PayRatePercentage" />
                    <telerik:AjaxUpdatedControl ControlID="num_SalaryPay" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnResetPassword">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lbtnResetPassword" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cbrtdsAccountLocked">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cbrtdsAccountLocked" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnUpdateSEU">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lbtnUpdateSEU" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="errMsg" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnChangePhoto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnChangePhoto" />
                    <telerik:AjaxUpdatedControl ControlID="AsyncUpload1" />
                    <telerik:AjaxUpdatedControl ControlID="btnImageCancel" />
                    <telerik:AjaxUpdatedControl ControlID="btnSaveChanges" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnImageCancel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnChangePhoto" />
                    <telerik:AjaxUpdatedControl ControlID="AsyncUpload1"  />
                    <telerik:AjaxUpdatedControl ControlID="btnImageCancel"  />
                    <telerik:AjaxUpdatedControl ControlID="btnSaveChanges" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid2" />

                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSaveChanges">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lbtnAddNew" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWOedit"  />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectEmployee"  />
                    <telerik:AjaxUpdatedControl ControlID="btnSaveChanges" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCancel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lbtnAddNew" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectEmployee" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWOedit" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" />

<table cellpadding="0" cellspacing="0" style="width: 1180px">
    <tr>
        <td valign="top" style="width:225px;"> <%-- ******************* LEFT COLUMN ******************* --%>
<%-- ******************* start Page Controls ******************* --%>
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
<%-- ******************* end Page Controls ******************* --%>
<%-- ******************* start Left Side employee Grid **********************--%>
<telerik:RadGrid ID="RadGrid1" runat="server" GridLines="None" AutoGenerateColumns="False" >
    <MasterTableView DataKeyNames="ID" AutoGenerateColumns="false" >
        <Columns>
            <telerik:GridBoundColumn DataField="ID" Visible="false" UniqueName="column">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="EmpName" HeaderText="Employee Name" UniqueName="column1">
            </telerik:GridBoundColumn>
        </Columns>
    </MasterTableView>
    <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true" >
        <Selecting AllowRowSelect="true" />
    </ClientSettings>
</telerik:RadGrid>
<%-- ******************* end Left Side employee Grid **********************--%>
        </td> <%-- ******************* end Left column ******************* --%>


        <td></td><%-- *******************splitter column**********************--%>


        <td valign="top" style="padding-left:11px;"> <%-- ******************* start RIGHT COLUMN  ******************* --%>
<%-- ******************* default tool tip labels ******************* --%>
<table width="740" cellpadding="0" cellspacing="0">
    <tr>
        <td><asp:Label ID="lblSelectLocation" runat="server" Text="<<<----  Select Location" /></td>
        <td align="right" valign="top">
            <span onmouseover="this.style.cursor='help'"><asp:Label ID="lblHelpPage" CssClass="lilBlueButton" Text="help with this page" runat="server" /></span>
        </td></tr><tr>
        <td colspan="2"><asp:Label ID="lblSelectEmployee" runat="server" Text="<br /><br /><br /><br /><<<----  Select an Employee from left" /></td>
<%-- *******************start Right Side EDIT Form **********************--%>
</tr></table>

<asp:Panel ID="pnlWOedit" runat="server" Visible="false" style="max-width:775px;" >

    <fieldset id="fsEmployeeForm" runat="server" style="border:1px solid black;padding:0px 12px 7px 12px;" >
    <legend style="font-size:24px;" title="Click to change name" >
        <table id="tblChangeName" cellpadding="0" cellspacing="0">
            <tr>
                <td title="Click here to change name">
                    <asp:LinkButton ID="lbtnChangeName"  ToolTip="Click here to change name" CssClass="lbtnEmpName" runat="server" >
                    <asp:Label ID="lblEmpName" runat="server" /></asp:LinkButton></td><td>
                <table>
                <tr>
                <td style="padding:0 0 0 14px;">
                    <telerik:RadTextBox ID="txt_rtdsFirstName" TabIndex="1" EmptyMessage="First Name" Width="90px" runat="server" />
                </td>
                <td valign="top" style="padding:0 8px 0 0;">
                    <asp:Label CssClass="errMsgRed" ID="lblErrFname" runat="server" Visible="false" Text="*" />
                </td>
                <td valign="top" style="padding:0 0 0 0;">
                    <telerik:RadTextBox ID="txt_rtdsLastName" TabIndex="2" EmptyMessage="Last Name" Width="90px" runat="server" />
                </td>
                <td valign="top" style="padding:0 0 0 0;">
                    <asp:Label CssClass="errMsgRed" ID="lblErrLname" runat="server" Visible="false" Text="*" />&nbsp; </td></tr></table></td></tr></table></legend><%-- ******************* start Picture Comments Certifications **********************--%><table id="img/cerifications">
        <tr>
            <td style="background-color:#dddddd;"><telerik:RadBinaryImage ID="imgMugShot" ResizeMode="Fit" runat="server" /></td>
            <td valign="top">
                <fieldset id="fsComments" runat="server" style="padding:0 5px 5px 5px; font-family:Arial; font-size:11px;" >
                    <legend style="font-size:14px; font-weight:bold;">Comments</legend><telerik:RadTextBox ID="txt_Comments" TabIndex="3" TextMode="MultiLine" Rows="4" Width="250px" EmptyMessage="Comments" runat="server" /><br />
                </fieldset> 
                        </td><td valign="top">
                <fieldset id="fsCertifications" runat="server" style="padding:0 5px 5px 5px; font-family:Arial; font-size:11px;" >
                    <legend>Certifications&nbsp;&nbsp;<asp:LinkButton ID="lbtnEditCerts" CssClass="lilBlueButton" text="edit" runat="server" /></legend>
                    <asp:Label ID="lblCerts" runat="server" />
                </fieldset>
                        </td></tr></table><%-- ******************* end Picture Comments Certifications **********************--%><table>
        <tr>
<%-- ******************* start Left Side employment **********************--%>
            <td valign="top">
<%--                                            <td valign="middle" align="right"><asp:LinkButton CommandName="Delete" ID="lbtnTerminateEmployment" CssClass="lilBlueButton" runat="server" OnClientClick="if (!confirm('You are about to terminate this employee.\nAre you sure you want to terminate this employee?')) return false;" Text="Terminate Employee" /></td>--%>
            <fieldset id="fsEmployment" style="padding:5px;">
            <legend> Employment &nbsp; <span onmouseover="this.style.cursor='help';"><asp:Label ID="lblHelpEmployment" Text="help" CssClass="lilBlueButton" runat="server" /></span> &nbsp; </legend><table id="titlepaytype">
                <tr>
                    <td>
<%-- ******************* start Employment Grid **********************--%>
<telerik:RadGrid ID="RadGrid2" runat="server" AutoGenerateColumns="False" CellSpacing="0" GridLines="None">
    <MasterTableView DataKeyNames="ID" EditMode="InPlace">
        <CommandItemSettings ExportToPdfText="Export to PDF" />
        <RowIndicatorColumn Visible="False" />
        <ExpandCollapseColumn Visible="False" />
        <Columns>
            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                <ItemStyle CssClass="MyImageButton" />
            </telerik:GridEditCommandColumn>
            <telerik:GridBoundColumn DataField="EmployeeID" DataType="System.Guid"
                UniqueName="EmployeeID" ReadOnly="true" Visible="false">
            </telerik:GridBoundColumn>


            <telerik:GridTemplateColumn DataField="DateOfEmployment" HeaderText="Start Date" UniqueName="DateOfEmployment">
                <ItemTemplate>
                    <asp:Label ID="lblDateOfEmployment" Width="75" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadDatePicker ID="griddpEmploymentDate" Width="100px" runat="server" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>

            <telerik:GridTemplateColumn DataField="DateOfDismiss" HeaderText="End Date" UniqueName="DateOfDismiss">
                <ItemTemplate>
                    <asp:Label ID="lblDateOfDismiss" Width="75px" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadDatePicker ID="griddpDismissDate" Width="100px" runat="server" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>

            <telerik:GridTemplateColumn DataField="JobTitle" HeaderText="JobTitle" UniqueName="JobTitle">
                <HeaderStyle />
                <ItemTemplate>
                    <asp:Label ID="lblJobTitle" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadComboBox ID="gridcbJobTitle" TabIndex="4" Width="135px" EmptyMessage="Select JobTitle" runat="server" Filter="Contains" AllowCustomText="true">
                        <Items>
                            <telerik:RadComboBoxItem Text="Labor" runat="server" />
                            <telerik:RadComboBoxItem Text="Pallet Puller" runat="server" />
                            <telerik:RadComboBoxItem Text="Pallet Sorter" runat="server" />
                            <telerik:RadComboBoxItem Text="Unloader" runat="server" />
                            <telerik:RadComboBoxItem Text="Unloader Supervisor" runat="server" />
                            <telerik:RadComboBoxItem Text="Housekeeping" runat="server" />
                            <telerik:RadComboBoxItem Text="Office Agent" runat="server" />
                            <telerik:RadComboBoxItem Text="Scheduling Clerk" runat="server" />
                            <telerik:RadComboBoxItem Text="Will Call" runat="server" />
                        </Items>
                    </telerik:RadComboBox>
                </EditItemTemplate>
            </telerik:GridTemplateColumn>

            <telerik:GridTemplateColumn DataField="PayType" HeaderText="PayType" UniqueName="PayType">
                <HeaderStyle Width="80px" />
                <ItemTemplate>
                    <asp:Label ID="lblPayType" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadComboBox ID="gridcbPayType" runat="server" Width="80px" >
                        <Items>
                            <telerik:RadComboBoxItem Value="2" Text="Hourly" />
                            <telerik:RadComboBoxItem Value="1" Text="Percent" />
                        </Items>
                    </telerik:RadComboBox>
                </EditItemTemplate>
            </telerik:GridTemplateColumn>




            <telerik:GridTemplateColumn DataField="PayRateHourly" DataType="System.Double" HeaderText="Hrly" UniqueName="PayRateHourly">
                <HeaderStyle  Width="85px"/>
                <ItemTemplate>
                    <%# Eval("PayRateHourly")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadNumericTextBox ID="num_PayRateHourly" TabIndex="8" runat="server" EmptyMessage="$" Type="Currency" Value='<%# Eval("PayRateHourly") %>' Width="50px" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
                                 
            <telerik:GridTemplateColumn DataField="PayRatePercentage" DataType="System.Double" HeaderText="%" SortExpression="PayRatePercentage" UniqueName="PayRatePercentage">
                <HeaderStyle />
                <ItemTemplate>
                    <%# Eval("PayRatePercentage")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadNumericTextBox ID="num_PayRatePercentage" TabIndex="9" runat="server" EmptyMessage="%" NumberFormat-DecimalDigits="2" Type="Percent" Value='<%# Eval("PayRatePercentage") %>' Width="55px" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>

            <telerik:GridBoundColumn Visible="false" DataField="ID" DataType="System.Guid" HeaderText="ID" ReadOnly="True" UniqueName="ID">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn  visible="false" DataField="SpecialPay" ReadOnly="True" DataType="System.Double" UniqueName="SpecialPay">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn  visible="false" DataField="HolidayPay" ReadOnly="True" DataType="System.Double" UniqueName="HolidayPay">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn visible="false" DataField="SalaryPay" ReadOnly="True" DataType="System.Double" UniqueName="SalaryPay">
            </telerik:GridBoundColumn>
            <telerik:GridButtonColumn ImageUrl="~/images/redX.gif" ConfirmDialogWidth="495px"  ConfirmDialogHeight="115px" ConfirmTitle="Warning, Will Robinson, Warning!" ConfirmText="Delete this Employment Record!<br />Are you sure? ... if so, click 'OK' ... AND ... <br/><br/>Be SURE to check the remaining Start Dates and End Dates.<br/>You may need to adjust dates so that there are no lapses and no overlaps.  <br/><br/>IMPORTANT!! Be sure the 'End Date' for what will become the next current Employment record is blank so that you don't mistakenly terminate the employee. If this is not clear, click 'Cancel' and contact the IT Department.<br/>&nbsp;" ConfirmDialogType="RadWindow"
                ButtonType="ImageButton" CommandName="Delete" Text="Delete" UniqueName="DeleteColumn">
                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
            </telerik:GridButtonColumn>
        </Columns>
        <EditFormSettings >
            <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" UniqueName="EditCommandColumn1">
            </EditColumn>
        </EditFormSettings>
    </MasterTableView>
    <FilterMenu />
</telerik:RadGrid>
<%-- ******************* Add Employment Button **********************--%>
                    </td>
                </tr>
                <tr>
                    <td>

                <table id="tblGridCommands" width="100%" class="lilBlueButton">
                   <tr>
                    <td>
                        <asp:LinkButton ID="lbtnAddEmployment" runat="server" Text="Change Employment" />
                    </td>
                    <td align="right"><asp:LinkButton ID="lbtnShowAllEmployment" runat="server" Text="Show All" />
                    </td>
                    </tr>
                </table><%-- ******************* end Grid Commands Table **********************--%>

                    </td>
                </tr>
            </table>
<%-- ******************* end Employment Grid **********************--%>
                   <telerik:RadToolTip ID="ToolTipEmploymentHelp" runat="server" IsClientID="true" TargetControlID="lblHelpEmployment" 
                        RelativeTo="BrowserWindow" ShowEvent="OnClick" Position='Center' HideEvent="ManualClose" 
                        Animation="Resize" EnableShadow="true">
                        <span class="ttHeader">Employment</span> <table width="600" >
                            <tr>
                                <td style="padding:0 8px;">
                                    <br />
                                    <table>
                                        <tr>
                                            <td class="ttBody">
                                                When a new employee is hired and EACH time that the employee's job title, pay type and/or pay rate is changed,
                                                a NEW 'Employment' record is created and added to the grid. <br />&nbsp; </td></tr><tr>    
                                            <td class="ttTitle">Employment Grid</td></tr><tr>
                                            <td class="ttBody">
                                                The employment grid displays the three most recent employment records with the most recent record on top.&nbsp; 
                                                IF there are more than three records, you will see a 'Show All' link in the bottom right corner of the grid.<br /> To change an employee's job title, pay type or pay rate, click the '<span style="color:Blue;">Change Employment</span>' link below the grid. <br />&nbsp; </td></tr><tr>
                                            <td class="ttTitle">Change Employment</td></tr><tr>
                                            <td class="ttBody">
                                               Clicking this link will display a form to change or set a new Job Title, Primary Pay Type and Pay Rate(s) for this employee. 
                                               &nbsp;A new employment record will be created and will become effective on your selected 'Start Date'. (see form) <br />&nbsp; </td></tr></table><center>To Close - Click X in upper right corner</center></td></tr></table></telerik:RadToolTip><%-- ******************* Start Add New Employment Form **********************--%><asp:Panel ID="pnlAddEmployment" runat="server" Visible="false">
            <br />

            <fieldset id="fldAddEmployment" runat="server"><legend> New Employment &nbsp; <span onmouseover="this.style.cursor='help';"><asp:Label ID="lblHelpNewEmployment" Text="help" CssClass="lilBlueButton" runat="server" /></span> &nbsp;</legend><table>
                <%-- ******************* start Row One **********************--%>
                <tr>
                    <%-- ******************* start Left Column **********************--%>
                    <td valign="top">
                        <span class="heading">Job Title</span> <asp:Label CssClass="errMsgRed" ID="lblErrJobTitle" runat="server" Visible="false" Text="*" />  
                        <br />
                        <telerik:RadComboBox ID="cb_JobTitle" TabIndex="4" EmptyMessage="Select JobTitle" runat="server" Filter="Contains" AllowCustomText="true">
                            <Items>
                            <telerik:RadComboBoxItem Text="Labor" runat="server" />
                            <telerik:RadComboBoxItem Text="Pallet Puller" runat="server" />
                            <telerik:RadComboBoxItem Text="Pallet Sorter" runat="server" />
                            <telerik:RadComboBoxItem Text="Unloader" runat="server" />
                            <telerik:RadComboBoxItem Text="Unloader Supervisor" runat="server" />
                            <telerik:RadComboBoxItem Text="Housekeeping" runat="server" />
                            <telerik:RadComboBoxItem Text="Office Agent" runat="server" />
                            <telerik:RadComboBoxItem Text="Scheduling Clerk" runat="server" />
                            <telerik:RadComboBoxItem Text="Will Call" runat="server" />
                            </Items>
                        </telerik:RadComboBox><br />
                    </td> <%-- ******************* end Left Column **********************--%>
                    <%-- ******************* start Right Column **********************--%>
                    <td valign="top" style="padding-left:10px;">
                        <table id="tblEmpDates" cellpadding="0" cellspacing="0">
                            <tr>
                                <td valign="top"><span class="heading">Start Date</span> <asp:Label CssClass="errMsgRed" ID="lblErrStartDate" runat="server" Visible="false" Text="*" /> <asp:LinkButton ID="lbtnCalendarPayWeek" Text="Calendar" CommandName=" show Calendar" CssClass="lilBlueButton" runat="server" /><br />

                                    <telerik:RadComboBox ID="cbPayWeekStartDates" TabIndex="5" Width="200px" runat="server" AutoPostBack="true" />

                                    <telerik:RadDatePicker ID="dp_DateOfEmployment" TabIndex="6" Width="100px" runat="server" /><br />
                                </td>
                            </tr>
                        </table><br />
                    </td><%-- ******************* end Right Column **********************--%>
                </tr><%-- ******************* end Row One **********************--%>
                <%-- ******************* start Row Two **********************--%>
                <tr>
                    <td valign="top">
                        <span class="heading">Primary Pay Type</span> <asp:Label CssClass="errMsgRed" ID="lblErrPayType" runat="server" Visible="false" Text="*" /> &nbsp; <span onmouseover="this.style.cursor='help';"><asp:Label ID="lblHelpPayType" Text="help" CssClass="lilBlueButton" runat="server" /></span>
                        
                        <br />

                        <telerik:RadComboBox ID="cb_PayType" TabIndex="7" runat="server" AutoPostBack="true" EmptyMessage="Select Primary Pay Type" AllowCustomText="true">
                            <Items>
                                <telerik:RadComboBoxItem Value="2" Text="Hourly payment" runat="server" />
                                <telerik:RadComboBoxItem Value="1" Text="Percentage payment" runat="server" />
<%--                                <telerik:RadComboBoxItem Value="3" Text="Other payment" runat="server" />--%>
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td valign="top">
                        <fieldset ID="fsPayRate" runat="server" style="padding:5px;font-size:12px;">
                            <legend class="subLegend">Pay Rate(s) <asp:Label ID="lblErrPayRates" runat="server" CssClass="errMsgRed" Text="*" Visible="false" /></legend>
                            <table cellpadding="0" cellspacing="0" style="font-size:12px;font-family:Arial;">
                                <tr>
                                    <td valign="top">Hourly<br /> <telerik:RadNumericTextBox ID="num_PayRateHourly" TabIndex="8" runat="server" EmptyMessage="$" Type="Currency" Width="50px" />
                                    </td>
                                    <td style="font-size:10px;padding:0 5px 0 5px;" valign="bottom">and/or</td><td>Percent<br /> <telerik:RadNumericTextBox ID="num_PayRatePercentage" TabIndex="9" runat="server" EmptyMessage="%" NumberFormat-DecimalDigits="2" Type="Percent" Value="0" Width="55px" />
                                    </td>
<%--                                    <td style="font-size:10px;padding:0 5px 0 5px;" valign="bottom"> OR </td><td>Other<br /> <telerik:RadNumericTextBox ID="num_SalaryPay" runat="server" Type="Currency" Width="65px" />
                                    </td>--%>
                                </tr>
                            </table>
                        </fieldset>                    
                                </td></tr><%-- ******************* end Row Two **********************--%></table></fieldset> <telerik:RadToolTip ID="ToolTipHelpNewEmployment" runat="server" IsClientID="true" TargetControlID="lblHelpNewEmployment" 
                        RelativeTo="BrowserWindow" ShowEvent="OnClick" Position='Center' HideEvent="ManualClose" 
                        Animation="Resize" EnableShadow="true">
                        <span class="ttHeader">Add New Employment</span> <table width="475" >
                            <tr>
                                <td style="padding:0 8px;">
                                    <br />
                                    <table>
                                        <tr>    
                                            <td class="ttTitle">Job Title</td></tr><tr>
                                            <td class="ttBody">
                                                Select the employee's Job Title from the drop-down list.<br /> The one consideration here is 'Unloader Supervisor'. &nbsp;An employee's Job Title MUST be set to
                                                'Unloader Supervisor' if they are to be paid 'Supervisor Pay' in a pay period. &nbsp; <em>(see Start Date below)</em> <br />&nbsp; </td></tr><tr>
                                            <td class="ttTitle">Primary Pay Type</td></tr><tr>
                                            <td class="ttBody">
                                                It is important that the Primary Pay Type be set correctly.<br /> Please see the help file for Primary Pay Type. <br />&nbsp; </td></tr><tr>
                                            <td class="ttTitle">Pay Rate(s)</td></tr><tr>
                                            <td class="ttBody">
                                                <b>Hourly</b> - Enter the hourly pay rate.<br /> <b>Percentage</b> - example: for 27 and ¼ percent enter &nbsp;27.25<br /> Selecting the 'Percentage' pay type allows you to enter a Percentage rate AND and Hourly rate for 
                                                those times when you need this employee to clock in 'by the hour'. (setting the <em>isHourly</em> checkbox when clocking in)<br /> <b>Other</b> - This will open the 'Other Pay' window. <br />&nbsp; </td></tr><tr>
                                            <td class="ttTitle">Start Date</td></tr><tr>
                                            <td class="ttBody">
                                                Select a start date from the calendar control <br />&nbsp; </td></tr></table><center>To Close - Click X in upper right corner</center></td></tr></table></telerik:RadToolTip><telerik:RadToolTip ID="ToolTipHelpPayType" runat="server" IsClientID="true" TargetControlID="lblHelpPayType" 
                        RelativeTo="BrowserWindow" ShowEvent="OnClick" Position='Center' HideEvent="LeaveToolTip" 
                        Animation="Resize" EnableShadow="true" >
                        <span class="ttHeader">Primary Pay Type</span> <table width="475">
                            <tr>
                                <td style="padding:0 8px;">
                                    <br />
                                    <table>
                                        <tr>
                                            <td class="ttTitle">Important!</td></tr><tr>
                                            <td class="ttBody">
                                                It is important that the &#39;Primary Pay Type&#39; be properly set. <br />Some important payroll calculations will depend on this field. </td></tr><tr>    
                                            <td class="ttTitle">Hourly</td></tr><tr>
                                            <td class="ttBody">
                                                When this is set to &#39;Hourly&#39; the employee shall be paid an hourly rate <u>ONLY</u>. <br />An &#39;Hourly&#39; employee will NEVER be paid for &#39;load business&#39; and ALL clock-ins will be converted so that the <em>Hourly</em> field is always true (checked). 
                                                &nbsp;If you intend to pay this employee for <u>any</u> load business in current pay period then you MUST choose the &#39;Percentage&#39; pay type. </td></tr><tr>
                                            <td class="ttTitle">Percentage</td></tr><tr>
                                            <td class="ttBody">
                                                Choose this pay type if you intend to pay the employee a percentage of his/her <br />load work.&nbsp; This pay type will allow you to ALSO enter an hourly rate for those times when you need this employee to clock in 'by the hour'. <br />(-- <em>by checking the 'Hourly' box on the hand-held when clocking in</em> --) <br />&nbsp; </td></tr></table><center>To Close - Move your mouse away from this help screen</center></td></tr></table></telerik:RadToolTip></asp:Panel></fieldset> <asp:Panel ID="pnlLocations" runat="server">
        <fieldset id="fldLocation" runat="server">
            <legend>Location</legend><table class="smallTitle" width="100%" >
                <tr>
                    <td>Home Location: <br /><telerik:RadComboBox ID="cbHomeLocation" Width="125px" Filter="Contains" runat="server" EmptyMessage="Select Location" />
                    </td>
                    <td>Current Location: <br /><telerik:RadComboBox ID="cbCurrentLocation" Width="135px" Filter="Contains" runat="server" EmptyMessage="Select Location" />
                    </td>
                </tr>
            </table>
            </fieldset>
     </asp:Panel></td><%-- ******************* end Left Side employment **********************--%><%-- ******************* start Right Side Access **********************--%><td valign="top"> 

<fieldset id="fsAccess" runat="server" style="padding:5px;">
    <legend> Access &nbsp; <span onmouseover="this.style.cursor='help';"><asp:Label ID="lblHelpAccess" Text="help" CssClass="lilBlueButton" runat="server" /></span></legend>
    <table id="login">
            <tr>
                <td>
                    <span class="heading" title="PIN # for hand-held">PIN*</span> <asp:Label CssClass="errMsgRed" ID="lblErrPIN" runat="server" Visible="false" Text="*" />
                </td>
                <td valign="top" style="padding-left:7px;">
                    <span class="heading">LoginID</span> <asp:Label CssClass="errMsgRed" ID="lblErrUserName" runat="server" Text="*" />
                </td>
                <td valign="top" style="padding-left:7px;">
                    <span class="heading">Password</span> </td>
                <td valign="top" style="padding-left:7px;">
                    <span class="heading">Employee #</span> 

                </td>
            </tr>
        <tr>
                <td>
                    <telerik:RadMaskedTextBox ID="txt_rtdsPassword" TabIndex="10" Width="45px" ToolTip="PIN # for hand-held - Last 4 digits of SSN"
                        Mask="####" EmptyMessage="PIN #" runat="server" 
                        PromptChar="-" Rows="1" />
                </td>
                <td valign="top" style="padding-left:7px;">
                    <telerik:RadTextBox ID="txtUserName" ReadOnly="true" Width="45" Enabled="false" runat="server" /> 
                </td>
                <td valign="top" style="padding-left:7px;">
                    <asp:LinkButton ID="lbtnResetPassword" CommandName="ResetPassword" Text="Reset Password" CssClass="lilBlueButton" runat="server" />    
                </td>
                <td valign="top" style="padding-left:7px;">
                    <telerik:RadTextBox ID="txtpayrollempnum" ReadOnly="true" Width="69px" Enabled="false" runat="server" Height="16px" /></td>
            </tr>
        </table>

<%-- *******************Access rtds **********************--%>
<fieldset id="fsrtdsAccess" runat="server" class="heading" style="padding:3px;" visible="false">
<legend class="subLegend">RTDS Hand Held</legend><table>
        <tr>
            <td>
                <asp:CheckBox ID="cbrtdsAccountLocked" runat="server" Text="Account Locked" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="cbARMpda" runat="server" Text ="PDA app" /> &nbsp; <asp:CheckBox ID="cbARMweb" runat="server" Text="WEB app" /> &nbsp; <asp:CheckBox ID="cbARMadministrator" Text="Administrator" runat="server" />
            </td>
        </tr>
    </table>
</fieldset>
                               <%-- *******************Access seu.div-log **********************--%><fieldset id="fsseuAccess" runat="server" class="heading" style="padding:3px;">
    <legend class="subLegend">SEU.Div-Log.com </legend><table>
        <tr>
            <td style="width:120px;">
                <table cellpadding="0" cellspacing="0"><tr>
                    <td> <asp:Button ID="btnUnlockUser" runat="server" Text="Unlock Account" CommandName="UnlockUser" Visible="false" /></td>
                    <td><asp:CheckBox ID="cbdivlogIsApproved" runat="server" Text="Login Approved" /></td>
                </tr></table>       
            </td>
            <td>
                <asp:LinkButton ID="lbtnUpdateSEU" Text="Create SEU Account" CssClass="lilBlueButton" runat="server" Visible="false" />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblEmailAddress" runat="server" Text="eMail Address: &nbsp;" /><br /><telerik:RadTextBox ID="txt_eMail" TabIndex="11" EmptyMessage="example-> Login@Div-Log.com" Width="200px" runat="server" /> <asp:Label CssClass="errMsgRed" ID="lblErrEmail" runat="server" Visible="false" Text="*" />
    <table>
        <tr>
            <asp:Repeater ID="UsersRoleList" runat="server"  > 
	            <ItemTemplate> 
		            <td>
                        <asp:CheckBox runat="server" ID="RoleCheckBox" AutoPostBack="false" Text='<%# Container.DataItem %>'/>
                    </td>                       
	            </ItemTemplate> 
            </asp:Repeater> 
        </tr>
    </table>
</fieldset>

              </fieldset> <telerik:RadToolTip ID="ToolTipHelpAccess" runat="server" IsClientID="true" TargetControlID="lblHelpAccess" 
                        RelativeTo="BrowserWindow" ShowEvent="OnClick" Position='Center' HideEvent="ManualClose" 
                        Animation="Resize" EnableShadow="true" >
                        <span class="ttHeader">Access section</span> <table width="475" >
                            <tr>
                                <td style="padding:0 8px;">
                                    <table>
                                    <tr>
                                            <td>
                                                <span style="font-size:11px;">** New Hire will only  need enter PIN number in this section.</span></td></tr><tr>
                                            <td class="ttTitle">PIN number</td></tr><tr>
                                            <td class="ttBody">
                                                Corporate recommends using the last 4 digits of the employee's Social Security # </td></tr><tr>
                                            <td class="ttTitle">LoginID</td></tr><tr>
                                            <td class="ttBody">
                                                This number is auto-generated and cannot be changed. &nbsp;The default number will generally be a location ident
                                                followed by the next sequential employee number.&nbsp;  
                                                For exceptions, contact the IT Department. </td></tr><tr>
                                            <td class="ttTitle">Password</td></tr><tr>
                                            <td class="ttBody">
                                                Initially auto-generated in the format of their LoginID followed by the word 'welcome' in lowercase characters. &nbsp;example:  oc2345welcome<br /> Using their LoginID and this password the employee can log into the <br />SEU.Div-Log.com website to view their hours, earnings and more. <br />If the employee has changed the default password, you will see a 'Reset Password' link in place of the password. 
                                                &nbsp;Click the link IF you have need to reset the employee's password to the default new password. </td></tr><tr>
                                            <td class="ttTitle">SEU.Div-Log.com ---</td></tr><tr>
                                          <td class="ttTitle">Login Approved</td></tr><tr>
                                            <td class="ttBody">
                                                UN-check this box to prevent the employee from logging in to the web site. </td></tr><tr>
                                            <td class="ttTitle">eMail Address</td></tr><tr>
                                            <td class="ttBody">
                                                A default NON-working eMail address is initially assigned to each employee.<br /> If the employee has another eMail address, enter it in the space provided. </td></tr><tr>
                                            <td class="ttTitle">Employee Role</td></tr><tr>
                                            <td class="ttBody">
                                                Select the employee's current Role (or position) by selecting the appropriate check box(es). &nbsp;
                                                This will determine the employee's access level on this web portal. <br />&nbsp; </td></tr></table><center>To Close - Click X in upper right corner</center></td></tr></table></telerik:RadToolTip></td><%-- ******************* end Right Side Access **********************--%></tr></table></fieldset> <%-- end fsEmployeeForm  --%><div id="errDiv" style="padding:7px;">
    <asp:Label ID="errMsg" CssClass="errMsgRed" runat="server" Visible="false" />
</div>

 <table id="commandBtns" width="100%">
    <tr>
        <td valign="top" style="width:50%;">
            <asp:Button ID="btnChangePhoto" runat="server" Text="Upload New Picture" 
                Visible="false" /><telerik:RadAsyncUpload  runat="server" ID="AsyncUpload1" MaxFileInputsCount="1" OnClientFileUploaded="fileUploaded"
                 AllowedFileExtensions="jpeg,jpg,gif,png,bmp" OnClientValidationFailed="validationFailed" />
             <asp:Button ID="btnImageCancel" runat="server" Text="Cancel Upload" Visible="false" />
        </td>
        <td valign="top">
        <table width="100%"><tr>
                                            <td valign="top" style="text-align:center;width:50%;" >
                                                <asp:Button ID="btnSaveChanges" TabIndex="12" runat="server" Text="Save Changes" />
                                            </td>
                                            <td valign="top" style="text-align:center"> 
                                                <asp:Button ID="btnCancel" TabIndex="13" runat="server" Text="Cancel / Close" /> 
                                            </td>
        </tr>
        </table>
        </td>
        

</tr>
</table> 
</asp:Panel>
<%-- *******************end Right Side EDIT Form **********************--%>


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


<%-- *******************END Right Side WO Form **********************--%>
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
                                    &nbsp;At the point we begin to issue ID Badges, we will use this image. &nbsp;You DO NOT have to click the save button after uploading image. <br />&nbsp; </td></tr></table><center>To Close - Click X in upper right corner</center></telerik:RadToolTip><telerik:RadWindowManager ID="RadWindowManager1" runat="server" VisibleStatusbar="false" >
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

           </script>



</telerik:RadCodeBlock>

    <script type="text/javascript">
        function fileUploaded(sender, args) {
            $find('RadAjaxManager1').ajaxRequest();
            $telerik.$(".invalid").html("");
            sender.deleteFileInputAt(0);
        }

        function validationFailed(sender, args) {
            $telerik.$(".invalid")
                .html("Invalid extension, please choose an image file");
            sender.deleteFileInputAt(0);
        }
    </script>



    </div>
    </form>
</body>
</html>
