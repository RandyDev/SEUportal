<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ChangeEmployment.aspx.vb" Inherits="DiversifiedLogistics.ChangeEmployment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <div>
                <fieldset id="fldAddEmployment" runat="server"><legend>Change / Update Employment <%--&nbsp; <span onmouseover="this.style.cursor='help';"><asp:Label ID="lblHelpNewEmployment" Text="help" CssClass="lilBlueButton" runat="server" /></span>--%> &nbsp;</legend>
                <br />
                <table>
                <%-- ******************* start Row One **********************--%>
                <tr>
                    <%-- ******************* start Left Column **********************--%>
                    <td valign="top">
                        <span class="heading">Job Title</span> <asp:Label CssClass="errMsgRed" ID="lblErrJobTitle" runat="server" Visible="false" Text="*" />  
                        <br />
                        <telerik:RadComboBox ID="cb_JobTitle" EmptyMessage="Select JobTitle" runat="server" />
                        <br />
                    </td> <%-- ******************* end Left Column **********************--%>
                    <%-- ******************* start Right Column **********************--%>
                    <td valign="top" style="padding-left:25px;">
                        <table id="tblEmpDates" cellpadding="0" cellspacing="0">
                            <tr>
                                <td valign="top"><span class="heading">Effective Date</span> <asp:Label CssClass="errMsgRed" ID="lblErrStartDate" runat="server" Visible="false" Text="*" /> <asp:LinkButton ID="lbtnCalendarPayWeek" Text="show Calendar" CommandName="Calendar" CssClass="lilBlueButton" runat="server" /><br />
                                    <telerik:RadComboBox ID="cbPayWeekStartDates" Width="200px" runat="server" AutoPostBack="true" /> <telerik:RadDatePicker ID="dp_DateOfEmployment" Width="100px" runat="server" /><br />
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

                        <telerik:RadComboBox ID="cb_PayType" runat="server" AutoPostBack="true" EmptyMessage="Select Primary Pay Type" AllowCustomText="true">
                            <Items>
                                <telerik:RadComboBoxItem Value="2" Text="Hourly payment" runat="server" />
                                <telerik:RadComboBoxItem Value="1" Text="Percentage payment" runat="server" />
<%--                                <telerik:RadComboBoxItem Value="3" Text="Other payment" runat="server" />--%>
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td valign="top" style="padding-left:25px;">
                        <fieldset ID="fsPayRate" runat="server" style="padding:5px;font-size:12px;">
                            <legend class="subLegend">Pay Rate(s) <asp:Label ID="lblErrPayRates" runat="server" CssClass="errMsgRed" Text="*" Visible="false" /></legend>
                            <table cellpadding="0" cellspacing="0" style="font-size:12px;font-family:Arial;">
                                <tr>
                                    <td valign="top">Hourly<br /> <telerik:RadNumericTextBox ID="num_PayRateHourly" runat="server" EmptyMessage="$" Type="Currency" Width="45px" />
                                    </td>
                                    <td style="font-size:10px;padding:0 5px 8px 5px;" valign="bottom">and/or</td><td>Percent<br /> <telerik:RadNumericTextBox ID="num_PayRatePercentage" runat="server" EmptyMessage="%" NumberFormat-DecimalDigits="2" Type="Percent" Value="0" Width="55px" />
                                    </td><td style="font-size:11px;padding:0 5px 0 5px;" valign="bottom">ex:  27&frac14; %<br />enter: 27.25</td>
<%--                                    <td style="font-size:10px;padding:0 5px 0 5px;" valign="bottom"> OR </td><td>Other<br /> <telerik:RadNumericTextBox ID="num_SalaryPay" runat="server" Type="Currency" Width="65px" />
                                    </td>--%>
                                </tr>
                            </table>

                        </fieldset>        

                             </td></tr><%-- ******************* end Row Two **********************--%></table>

                             </fieldset> 
<asp:Label ID="lblError" class="ColorMeRed" runat="server" Visible="false" />
                                     <table width="100%" style="text-align:center"><tr>
                                            <td valign="top">
                                                <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" />
                                            </td>
                                            <td valign="top"> 
                                                <asp:Button ID="btnCancel" OnClientClick="cancelAndClose();" runat="server" Text="Cancel" /> 
                                            </td>

        </tr>
        </table>



                   <telerik:RadToolTip ID="ToolTipHelpNewEmployment" runat="server" IsClientID="true" TargetControlID="lblHelpNewEmployment" 
                        RelativeTo="BrowserWindow" ShowEvent="OnClick" Position='Center' HideEvent="ManualClose" 
                        Animation="Resize" EnableShadow="true">
                       <span class="ttHeader">Change/Update Employment</span> 
                       <table width="475" >
                           <tr>
                               <td style="padding: 0 8px;">
                                   <table>
                                       <tr>
                                           <td class="ttTitle">Primary Pay Type</td></tr><tr>
                                           <td class="ttBody">
                                               It is important that the Primary Pay Type be set correctly.<br /> Please see help for Primary Pay Type. </td></tr><tr>
                                           <td class="ttTitle">Pay Rate(s)</td></tr><tr>
                                           <td class="ttBody">
                                               <b>Hourly</b> - Enter the hourly pay rate.<br /> <b>Percentage</b> - example: for 27 and ¼ percent enter &nbsp;27.25<br /> Selecting the 'Percentage' pay type allows you to enter a Percentage rate AND and Hourly rate for 
                                               those times when you need this employee to clock in 'by the hour'. (setting the <em>isHourly</em> checkbox when clocking in)</td></tr><tr>
                                           <td class="ttTitle">Start Date</td></tr><tr>
                                           <td class="ttBody">
                                              Select a Start Date from the drop down list.  The available selections will be:<br /> <b><asp:Label ID="lblHelpThisPayPeriodStart" runat="server" /></b> (effective at start of current pay period)<br /> <b><asp:Label ID="lblHelpNextPayPeriodStart" runat="server" /></b> (effective at start of next pay period)<br /> also, for 4 days into the current pay period you will be able to choose:<br /> <b><asp:Label ID="lblHelpPreviousPayPeriodStart" runat="server" /></b> (effective at start of previous pay period)<br /> If you need to set a different date, please contact the HR Department. <br />&nbsp; 
                                           </td>
                                       </tr>
                                   </table>
                                   <center>To Close - Click the '<u>round</u>' X in upper right corner</center>
                               </td>
                           </tr>
                       </table>
                   </telerik:RadToolTip>
                    <telerik:RadToolTip ID="ToolTipHelpPayType" runat="server" IsClientID="true" TargetControlID="lblHelpPayType" 
                        RelativeTo="BrowserWindow" ShowEvent="OnClick" Position='Center' HideEvent="LeaveToolTip" 
                        Animation="Resize" EnableShadow="true" >
                        <span class="ttHeader">Primary Pay Type</span> 
                        <table width="475">
                            <tr>
                                <td style="padding:0 8px;">
                                    <br />
                                    <table>
                                        <tr>
                                            <td class="ttTitle">Important!</td></tr><tr>
                                            <td class="ttBody">
                                                It is important that the &#39;Primary Pay Type&#39; be properly set. <br />Some important payroll calculations will depend on this field. 
                                            </td>
                                        </tr><tr>    
                                            <td class="ttTitle">Hourly</td></tr><tr>
                                            <td class="ttBody">
                                                When this is set to &#39;Hourly&#39; the employee shall be paid an hourly rate <u>ONLY</u>. <br />An &#39;Hourly&#39; employee will NEVER be paid for &#39;load business&#39; and ALL clock-ins will be converted so that the <em>Hourly</em> field is always true (checked). 
                                                &nbsp;If you intend to pay this employee for <u>any</u> load business in current pay period then you MUST choose the &#39;Percentage&#39; pay type.
                                            </td>
                                        </tr><tr>
                                            <td class="ttTitle">Percentage</td></tr><tr>
                                            <td class="ttBody">
                                                Choose this pay type if you intend to pay the employee a percentage of his/her <br />load work.&nbsp; This pay type will allow you to ALSO enter an hourly rate for those times when you need this employee to clock in 'by the hour'. <br />(-- <em> by checking the 'Hourly' box on the hand-held when clocking in</em> --)
                                                <br />&nbsp; 
                                            </td>
                                        </tr>
                                    </table>
                                    <center>To Close - Move your mouse away from this help screen</center>
                                </td>
                            </tr>
                        </table>
                    </telerik:RadToolTip>

    </div>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" VisibleStatusbar="false" >
<Windows>
<telerik:RadWindow ID="wEmploymentEditor" Height="380px" Width="520px"  
    ShowContentDuringLoad="false"   runat="server" Modal="true" 
    Title="SEU Employment Editor" 
    OnClientClose = "OnClientCloseEmploymentEditor"
    Behaviors="Move, Resize, Close" />
</Windows>
    </telerik:RadWindowManager>
<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
<script type="text/javascript">
    function pageLoad() {
        var currentWindow = GetRadWindow();
    }
    function GetRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)
        return oWindow;
    }

    function cancelAndClose() {
        var oWindow = GetRadWindow();
        oWindow.argument = "X";
        oWindow.close();
    }

    function setReturnArg(arg) {
        var oWnd = GetRadWindow();
        oWnd.close(arg);
    }
    function decOnly(i) {
        var unicode = event.keyCode ? event.keyCode : event.charCode;
        if (unicode == 37 || unicode == 39) {    // ignore a left or right arrow press
            return
        }
        var t = i.value;
        if (t.length > 0) {
            t = t.replace(/[^\da-zA-Z&\s]+/g, '');
            i.value = t.toUpperCase();
        }
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

</script>
</telerik:RadScriptBlock>


    </form>
</body>
</html>

