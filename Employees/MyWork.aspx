<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MyWork.aspx.vb" Inherits="DiversifiedLogistics.MyWork" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<link href="../Styles/Stylesheet.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.lblName{
font-size:15px;
font-weight:bold;
}
.lilBlueButton{
font-size:11px;
color:Blue;
font-weight:normal;
}
body{
font-family:Arial;
}
</style>
<script type="text/javascript">
    function toggleTimeSheet() {
        var div1 = document.getElementById('divTimeSheet');
        if (div1.style.display == 'none') {
            div1.style.display = 'inline'
        } else {
            div1.style.display = 'none'
        }
    }
        function toggleBusiness() {
            var div1 = document.getElementById('divBusiness');
            if (div1.style.display == 'none') {
                div1.style.display = 'inline'
            } else {
                div1.style.display = 'none'
            }
        }
//        function toggleBusiness() {
//            var div1 = document.getElementById('divTempBusiness');
//            if (div1.style.display == 'none') {
//                div1.style.display = 'inline'
//            } else {
//                div1.style.display = 'none'
//            }
//        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <center><asp:Label ID="lblShowTimeSheet" runat="server" /> &nbsp;Current Pay Period&nbsp; <asp:Label ID="lblShowBusiness" runat="server" /><br />
        <asp:Label ID="lblPayPeriod" runat="server" /></center>

<table><tr>
<td valign="top"><%--******************** Time Sheet **************************--%>
<div id="divTimeSheet" style="margin:auto; text-align:left;" runat="server" >
<table cellpadding="0" cellspacing="0" width="550" align="center"><tr><td>My Time:</td><td align="right"><span onmouseover="this.style.cursor='help'"><asp:Label ID="lblTimeSheetHelp" CssClass="lilBlueButton" Text="help with timesheet" runat="server" /></span> &nbsp; </td></tr></table>
<table width="550" border="1" align="center"><tr><td>
            <fieldset style="padding-left:5px;">
                <legend style="font-family:Arial; font-size:14px; font-weight:bold;">Current Pay-Period :  <asp:Label ID="lblcwk" style="font-size:11px;" runat="server" /> - <asp:Label style="font-size:11px;" Text="" ID="lblttlcptime" runat="server" /></legend>
                <table style="font-family:arial;font-size:11px;">
                    <tr>
                        <asp:Label style="font-size:11px;" ID="lblCurpp" runat="server" />
                     </tr>
                 </table>
             </fieldset>
</td></tr></table>

</div>

</td>
<td valign="top" style="padding-left:80px;"><%--******************** Business **************************--%>
<div id="divTempBusiness" style="margin:auto; text-align:left; width:450px;" visible="false"  runat="server">
<center>
<br /><br /><br /><br /><br />
Business Total calculator is temporarily out of service<br />
We should have the updated version up by March 19<br />
Apologies for any inconvenience

</center>

</div>
    <div id="divBusiness" style="margin:auto; text-align:left;" visible="true" runat="server">
    <table cellpadding="0" cellspacing="0" width="510" align="center"><tr><td>My Business:</td><td align="right"><span onmouseover="this.style.cursor='help'"><asp:Label ID="lblBusinessHelp" CssClass="lilBlueButton" Text="help with business list" runat="server" /></span> &nbsp; </td></tr></table>
        <telerik:RadGrid ID="RadGrid1" AutoGenerateColumns="False" ShowFooter="True"
            EnableLinqExpressions="False" GridLines="None" runat="server" 
            PageSize="20" Width="510px">
            <PagerStyle Mode="NextPrevAndNumeric" />
            <ClientSettings>
                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            </ClientSettings>
            <MasterTableView TableLayout="Auto" DataKeyNames="ulCount, Amount, PayRatePercentage" >
                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                <Columns>
                    <telerik:GridTemplateColumn SortExpression="LogDate" UniqueName="Date" 
                        HeaderText="LogDate" DataField="LogDate" ItemStyle-Wrap="False" 
                        GroupByExpression="LogDate" Groupable="False">
                        <HeaderStyle width="95px"/>
                        <ItemTemplate>
                            <%# Format(Eval("LogDate"), "ddd dd-MMM-yyyy")%>
                        </ItemTemplate>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </telerik:GridTemplateColumn>

                    <telerik:GridBoundColumn DataField="PurchaseOrder" HeaderTooltip="Purchase Order Number" HeaderText="PO #"
                        SortExpression="PurchaseOrder" UniqueName="PurchaseOrder" HeaderStyle-Width="45px">
<HeaderStyle Width="45px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="DoorNumber" HeaderTooltip="The number on/over the door where you worked this load :)" HeaderText="Door #"
                        SortExpression="DoorNumber" UniqueName="DoorNumber" HeaderStyle-Width="45px">
<HeaderStyle Width="45px"></HeaderStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridNumericColumn DataField="ulCount" HeaderTooltip="Number of Unloaders" HeaderText="Unldrs"
                        SortExpression="ulCount" UniqueName="ulCount" HeaderStyle-Width="55px">
<HeaderStyle Width="55px"></HeaderStyle>
                    </telerik:GridNumericColumn>
                    <telerik:GridNumericColumn DataField="Amount" DataFormatString="{0:C}" HeaderTooltip="Load Amount $" HeaderText="Load Amount"
                        SortExpression="Amount" UniqueName="Amount" HeaderStyle-Width="75px">
<HeaderStyle Width="75px"></HeaderStyle>
                    </telerik:GridNumericColumn>
                    <telerik:GridCalculatedColumn HeaderText="Your Business" HeaderTooltip="(Load Amount / Unldrs) * Your Pay Rate" DataFormatString="{0:C}" UniqueName="YourShare" DataType="System.Double"
                        DataFields="Amount, ulCount, PayRatePercentage" Expression="{0}/{1}" HeaderStyle-Width="75px" 
                        Aggregate="Custom" />

                </Columns>
            </MasterTableView>



        </telerik:RadGrid>

    </div>

</td>
</tr></table>
<telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="lblTimeSheetHelp" RelativeTo="BrowserWindow"  
    ShowEvent="OnClick" Position="Center" HideEvent="ManualClose" 
     Animation="Resize" EnableShadow="true">
<span class="ttHeader">My Time Sheet</span>
<table><tr><td style="padding:0 8px;">
<table>
    <tr>
        <td class="ttTitle">Time Sheets</td>
    </tr>
    <tr>
        <td  class="ttBody">
            
            You are looking at your 'time sheet' for the current pay period ending on <%= endPayPeriod %>.<br />
            The top line shows the pay period start and end dates, your total time worked for 'This Week'<br />
            and, if we are in the second week, it will also show your total hours worked 'Last Week'.<br />
            This time sheet shows all your time cards for the pay period.<br />
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Time Cards</td>
    </tr>
    <tr>
        <td  class="ttBody">An electronic time card is created for each day when you clock in to a department.<br />
            If you work in more than one department on the same day, you will see a time card<br />
            for each deparment you worked in on that day.<br />
            Each time card includes: the date worked, the department worked and all the times you<br />
            clocked in and out of that department on that date. &nbsp;Each time you clock out, the total hours<br />
            and minutes are calculated and displayed in [brackets]. &nbsp;If the [brackets] are <font color='red'>red</font>, that means<br />
            you worked without taking a break for longer than we recommend.<br />
        </td>
    </tr>
</table>
<br />
<center>To Close - Click X in upper right corner</center>&nbsp;


</td></tr></table>

</telerik:RadToolTip>

<telerik:RadToolTip ID="RadToolTip2" runat="server" TargetControlID="lblBusinessHelp" RelativeTo="BrowserWindow"
    ShowEvent="OnClick" Position="Center" HideEvent="ManualClose" 
     Animation="Resize" EnableShadow="true">
<span class="ttHeader">My Business</span>
<table><tr><td style="padding:0 8px;">
<table>
    <tr>
        <td class="ttTitle">What am I looking at here?</td>
    </tr>
    <tr>
        <td  class="ttBody">
            
            This list shows you the date, purchase order number and door number of every load<br />
            you've worked during this pay period. &nbsp;For each load you will see the number of<br />
            unloaders that worked the load and the total load business amount. <br />
            'Your Business' is the 'Load Amount' divided by the number of unloaders. <br /> 

        </td>
    </tr>
    <tr>
        <td class="ttTitle">What is the 'Gross Payout' amount?</td>
    </tr>
    <tr>
        <td  class="ttBody">
            This is the amount you earned, based on your current percentage pay rate and <br />
            before taxes, insurance or any other deductions are applied.
        </td>
    </tr>
</table><br />
<center>To Close - Click X in upper right corner</center>&nbsp;


</td></tr></table>

</telerik:RadToolTip>




    </form>
</body>
</html>
