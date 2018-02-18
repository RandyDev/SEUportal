<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ChangeEmployment.ascx.vb" Inherits="DiversifiedLogistics.ChangeEmployment1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    th, td {
        padding: 1px;
    }
    .vtop {
        vertical-align: top;
    }
    .vbot {
        vertical-align: bottom; 
    }
    .vmid {
        vertical-align: middle; 
    }
    .tcellspace0 {
 border-collapse: collapse; border-spacing: 0; 
    }
    .tcellspacing5 {
 border-collapse: separate; border-spacing: 5px; 
    }

</style>
<table id="Table2" border="0" class="tcellspace0" border-spacing: 2px;>  <%--/*cellspacing = 5 */--%>
    <tr class="EditFormHeader">
        <td colspan="2">
            <b>Employment Details</b>
        </td>
    </tr>
    <tr>
        <td>
                <fieldset id="fldAddEmployment" runat="server"><legend>Change / Update Employment <asp:Label ID="lblempname" runat="server" /> <%--&nbsp; <span onmouseover="this.style.cursor='help';"><asp:Label ID="lblHelpNewEmployment" Text="help" CssClass="lilBlueButton" runat="server" /></span>--%> &nbsp;</legend>
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
                    <td class="vtop" style="padding-left:25px;">
                        <table id="tblEmpDates" class="tcellspace">
                            <tr>
                                <td class="vtop, tcellspace0"><span class="heading">Effective Date</span> <asp:Label CssClass="errMsgRed" ID="lblErrStartDate" runat="server" Visible="false" Text="*" /> <asp:LinkButton ID="lbtnCalendarPayWeek" Text="show Calendar" CommandName="Calendar" CssClass="lilBlueButton" runat="server" /><br />
                                    <telerik:RadComboBox ID="cbPayWeekStartDates" Width="200px" runat="server" AutoPostBack="true" /> <telerik:RadDatePicker ID="dp_DateOfEmployment" Width="100px" runat="server" /><br />
                                </td>
                            </tr>
                        </table><br />
                    </td><%-- ******************* end Right Column **********************--%>
                </tr><%-- ******************* end Row One **********************--%>
                <%-- ******************* start Row Two **********************--%>
                <tr>
                    <td class="vtop">
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
                    <td class="vtop" style="padding-left:25px;">
                        <fieldset ID="fsPayRate" runat="server" style="padding:5px;font-size:12px;">
                            <legend class="subLegend">Pay Rate(s) <asp:Label ID="lblErrPayRates" runat="server" CssClass="errMsgRed" Text="*" Visible="false" /></legend>
                            <table style="font-size:12px;font-family:Arial;">
                                <tr>
                                    <td class="vtop">Hourly<br /> <telerik:RadNumericTextBox ID="num_PayRateHourly" runat="server" EmptyMessage="$" Type="Currency" Width="45px" />
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
                                     <table style="margin: 0 atuo;"><tr>
                                            <td class="vtop">
                                                <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" />
                                            </td>
                                            <td class="vtop"> 
                                                <asp:Button ID="Button1" OnClientClick="cancelAndClose();" runat="server" Text="Cancel" /> 
                                            </td>

        </tr>
        </table>
        </td>
    </tr>
    <tr>
        <td colspan="2"></td>
    </tr>
    <tr>
        <%--<td></td>--%>
        <td></td>
    </tr>
<%--    <tr>
        <td style="text-align:right;" colspan="2">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                Visible='<%# (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>--%>
</table>
                   <telerik:RadToolTip ID="ToolTipHelpNewEmployment" runat="server" IsClientID="true" TargetControlID="lblHelpNewEmployment" 
                        RelativeTo="BrowserWindow" ShowEvent="OnClick" Position='Center' HideEvent="ManualClose" 
                        Animation="Resize" EnableShadow="true">
                       <span class="ttHeader">Change/Update Employment</span> 
                       <table style="width:475px;" >
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
                        <table style="width:475px">
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
