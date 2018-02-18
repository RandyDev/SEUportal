<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DuplicateReceipts.aspx.vb" Inherits="DiversifiedLogistics.DuplicateReceipts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title> 
    <link href="../styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../styles/styles.css" rel="stylesheet" type="text/css" />

 <style type="text/css"> 
    .rlbGroup, .rlbList { overflow: auto !important; }
    .rlbGroup {
         min-height:40px; 
    }
</style>

</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
				<div ID="divheader" runat="server">





					<div class="logo" style="width:100%" >
                    <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width:33%;">                    <img src="../images/spacer.gif" width="25" alt="" />
   <asp:HyperLink ID="mstLogo" NavigateUrl="~/default.aspx" ImageUrl="~/images/logo.png" ToolTip="Click to Home Page" runat="server" /></td>
                        <td style="width:34%;" align="center"><asp:Image ID="imgClientServices" ImageUrl="../images/ClientSvcs.png" runat="server" Visible="true" /></td>
                        <td style="width:33%;"><div style="padding-top:20px; float:right;">
    <asp:UpdatePanel ID="upthuTime" runat="server">
        <ContentTemplate>
	        <span style="float:right;padding-right:10px;" class="breadcrumb">	&nbsp; &nbsp; <%=String.Format("{0:dd MMM yyyy - hh:mm tt}", DateTime.Now)%>
            <asp:Timer id="thuTime" Interval="60000" runat="server"></asp:Timer>		</span>
        </ContentTemplate>
    </asp:UpdatePanel>

</div>
</td>
                    </tr>
                    </table>
                    </div>				
                </div>
<br />
<h1 style="text-align:center">Duplicate Receipt Generator </h1>
<div style="margin:0 auto;"> 
<div style="text-align:right; margin:0 auto;width:650px"><span onmouseover="this.style.cursor='help';"><asp:Label CssClass="lilBlueButton" ID="lblPageHelp" Visible="false" runat="server" Text="help with this page" /></span></div>

<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Visible="false">
<table align="center" style="border:1px solid black;" >
    <tr>
        <td valign="top">
<div style="width:290px;">
<fieldset id="fldSearchPOs" style="padding:4px;" runat="server">
<legend>Search Purchase Orders</legend>
    <table>
        <tr>
            <td>
    Date:<br /><telerik:RadDatePicker Width="110" ID="RadDatePicker1" runat="server">
        </telerik:RadDatePicker>
            </td>
            <td>
    PO Number:<br /><telerik:RadTextBox ID="txtPOnumber" Width="80" runat="server">
        </telerik:RadTextBox>
            </td>
            <td>
    Amount:<br /><telerik:RadNumericTextBox ID="txtAmount" Width="60" runat="server" DataType="System.Decimal" IncrementSettings-InterceptMouseWheel="False" IncrementSettings-InterceptArrowKeys="False" IncrementSettings-Step="0" NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="," NumberFormat-DecimalSeparator="." NumberFormat-GroupSizes="3">
        </telerik:RadNumericTextBox>
            </td>
        </tr>
<tr>
<td colspan="2"><asp:Label ID="lblResult" style="font-size:11px;" runat="server" /></td>
<td align="right"><asp:Button ID="Button1" CommandName="Submit" runat="server" Text="Submit" /></td>
</tr>
    </table>
</fieldset>
       </div>
        </td>
        <td rowspan="2" valign="top">
<div style="float:left;width:275px;padding:0 0 0 50px;">
Selected Purchase Orders:<br />
<telerik:RadListBox ID="lstBoxworkorders" AllowDelete="true" runat="server" Width="255" EmptyMessage="que is empty">
<HeaderTemplate>
<table width="255"><tr><td style="width:85px;">Date</td><td style="width:85px;">PO #</td><td style="width:85px;">Amount</td></tr></table>
</HeaderTemplate>
<ItemTemplate>
<table><tr>
<td style="width:85px;"><%# Convert.ToDateTime(DataBinder.Eval(Container, "Attributes['Date']")).ToShortDateString%></td>
<td style="width:85px;"> <%# DataBinder.Eval(Container, "Attributes['PurchaseOrder']").ToString%></td>
<td style="width:85px;"><%# Convert.ToInt32(DataBinder.Eval(Container, "Attributes['Amount']")).ToString("C2")%></td>
</tr>
</table>
</ItemTemplate>


</telerik:RadListBox>
<br /><table width="100%">
<tr>
<td>
<telerik:RadTextBox ID="txtEmail" runat="server" Width="175" EmptyMessage="eMail Address (optional)" Visible="true" />
</td>
<td>
<asp:Button ID="btnViewPrint" OnClientClick="OpenWinDupReceipts();" runat="server" Text="View/Print" />
</td></tr></table>


</div>

        </td>
    </tr>
</table>
</telerik:RadAjaxPanel>
</div>
<asp:Panel ID="pnlCaptcha" runat="server" DefaultButton="btnVerifyCaptcha" Visible="true">
<div style="margin:0 auto;">
<center><asp:Label ID="lblcaptcha" runat="server">
 Please pass the CAPTCHA to access the Receipt Generator <br />
<font size="1">Completely Automated Public Turing test to tell Computers and Humans Apart</font></asp:Label></center>
<br /><br />  <center>      <telerik:RadCaptcha ID="RadCaptcha1" runat="server" CaptchaTextBoxLabel="<br />Type the code from the image">
        </telerik:RadCaptcha>
<asp:Button ID="btnVerifyCaptcha" text="Submit" runat="server" />
</center>
</div>
</asp:Panel>

<telerik:RadToolTip ID="radToolTipPageHelp" runat="server" ManualClose="True" 
        RelativeTo="BrowserWindow" ShowEvent="OnClick" Width="615px" 
        TargetControlID="lblPageHelp" ToolTip="New users, click here!" 
        Position="TopCenter" OffsetY="270" Animation="Slide" ShowCallout="False" 
        style="margin-right: 0px">
<table width="100%">
    <tr>
        <td>
<fieldset style="padding:4px; margin-right: 0px;" runat="server" id="fldsetInstructions">
<table>
    <tr>
        <td class="ttTitle">'Search Purchase Orders' control</td>
    </tr>
    <tr>
        <td class="ttBody">
            Enter the date, PO # and amount. &nbsp; <em>All fields are required.</em><br />
            Click the Submit button to perform search.<br />
            If a match is found, it is added to the 'Selected Purchase Orders' list on the right side of the page.<br />
            Repeat these steps for each Purchase Order / Duplicate Receipt you need.
        </td>
    </tr>
    <tr>
        <td class="ttTitle">'Selected Purchase Orders' list</td>
    </tr>
    <tr>
        <td class="ttBody">The list of purchase order(s) for which you have requested a duplicate receipt, 
        in que and ready for delivery.
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Why is there a (c) next to my purchase order number?</td>
    </tr>
    <tr>
        <td class="ttBody">There are times when multiple purchase orders are covered in a single load or work order.<br />
        In these cases, one PO# is selected as the primary and the other PO#'s are placed in the load comments field.<br />
        If the PO# you searched for is found in the comments field there will be a (c) displayed next to it. <br />
        In the 'Selected Purchase Orders' que it will be displayed just below the primary PO#. 
        </td>
    </tr>
    <tr>
        <td class="ttTitle">View/Print w/ optional eMail</td>
    </tr>
    <tr>
        <td class="ttBody">Click this button to open your duplicate receipt(s) as a .pdf document.<br />
        You will need Acrobat Reader to view this file. &nbsp; It's free. &nbsp; <a href="http://get.adobe.com/reader/" target="_blank">Get it here!</a><br />
        IF you provide a valid eMail address, the document will be emailed prior to being displayed.<br />
        The Acrobat Reader will provide controls for you to print and/or save the document.
        </td>
    </tr>
</table>

</fieldset>
<center class="ttBody">To Close - Click X in upper right corner</center>
        </td>
    </tr>
</table>
</telerik:RadToolTip>

<telerik:RadWindow ID="winDupReceipts" Title="SEU Duplicate Receipt Generator" 
        VisibleStatusbar="false" runat="server" Height="625" Width="500"
        Behaviors="Close, Resize, Maximize, Move" Skin="Sunset" EnableShadow="True">
</telerik:RadWindow>




<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
<script type="text/javascript">
    function OpenWinDupReceipts() {
        var eml = $find("<%= txtEmail.ClientID %>").get_textBoxValue();


        var list = $find("<%= lstBoxworkorders.ClientID %>");
        var items = list.get_items();
        var arg = "";
        items.forEach(function (item) {
            arg += item.get_value() + ":"
        });
        var oWnd = $find("<%= winDupReceipts.ClientID %>");
        oWnd.setUrl("../ClientSvcs/seuDuplicateReceipts.aspx?eml=" + eml + "&woid=" + arg.substring(0, arg.length - 1));
        oWnd.show();
    }

    function listBoxLoad(sender, args) {
//        var elementHeight = $telerik.getSize(sender.get_items().getItem(0).get_element()).height;
//        sender._groupElement.style.height = "24px";  //(elementHeight * 5) + "px";

        }




</script>
</telerik:RadScriptBlock>
 



    </form>
</body>
</html>
