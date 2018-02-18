<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoadEditorDataEntry.aspx.vb" Inherits="DiversifiedLogistics.LoadEditorDataEntry" %>

<%@ Import Namespace="DiversifiedLogistics" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Load Editor DATA ENTRY</title>
    <link href="../styles/StyleSheet.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .data { font-size:14px;font-weight:bold;}
    .lbl {font-size:12px;font-weight:normal;}
    .lbl td {padding-left:24px;}
    .ColorMeRed {color:Red;}
    .fullw {width: 100%;}
    .style2 {width: 113px;}
    .leftGrid {width: 407px;vertical-align:top; padding-right:14px}
    .rightforms {width: 407px;}
    .lilBlue {font-family:Arial;color:Blue;text-decoration:underline;padding-top:18px;}
    .padmenot {padding:0px;}
    .aligntop {vertical-align:top;}
    .alignleft {text-align: left;}
        .lilBlueButton{
    color:Blue;
    font-weight:normal;
    font-size: 11px;
    padding-left:5px;
}
    .auto-style1 {
        text-decoration: underline;
    }
    .auto-style2 {
        height: 27px;
    }
    .auto-style3 {
        width: 102px;
    }
    .auto-style4 {
        width: 336px;
    }
</style>
</head>
<body>
<form id="form1" runat="server">
<telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="myctrls" />
                <telerik:AjaxUpdatedControl ControlID="pnlWOinfo" />
                <telerik:AjaxUpdatedControl ControlID="pnlWOedit" LoadingPanelID="LoadingPanel1" />
                <telerik:AjaxUpdatedControl ControlID="pnlWOedit" LoadingPanelID="LoadingPanel1" />
                <telerik:AjaxUpdatedControl ControlID="RadGrid1s" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cbLocations">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="btnClearForm" />
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="LoadingPanel1" />
                <telerik:AjaxUpdatedControl ControlID="myctrls" />
                <telerik:AjaxUpdatedControl ControlID="pnlWOinfo" UpdatePanelHeight="" UpdatePanelCssClass="" />
                <telerik:AjaxUpdatedControl ControlID="pnlWOedit" UpdatePanelHeight="" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="btnRefresh">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="btnClearForm" />
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelCssClass="" LoadingPanelID="LoadingPanel1" />
                <telerik:AjaxUpdatedControl ControlID="myctrls" />
                <telerik:AjaxUpdatedControl ControlID="pnlWOinfo" />
                <telerik:AjaxUpdatedControl ControlID="pnlWOedit" UpdatePanelCssClass="" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="btnClearForm">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="btnClearForm" />
                <telerik:AjaxUpdatedControl ControlID="myctrls" />
                <telerik:AjaxUpdatedControl ControlID="pnlWOinfo" UpdatePanelCssClass="" />
                <telerik:AjaxUpdatedControl ControlID="pnlWOedit" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RadGrid1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="btnClearForm" />
                <telerik:AjaxUpdatedControl ControlID="myctrls" />
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelCssClass="" />
                <telerik:AjaxUpdatedControl ControlID="pnlWOinfo" LoadingPanelID="LoadingPanel1" UpdatePanelCssClass="" />
                <telerik:AjaxUpdatedControl ControlID="pnlWOedit" LoadingPanelID="LoadingPanel1" UpdatePanelCssClass="" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="btnPrintWO">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="btnClearForm" UpdatePanelCssClass="" />
                <telerik:AjaxUpdatedControl ControlID="myctrls" />
                <telerik:AjaxUpdatedControl ControlID="pnlWOinfo" />
                <telerik:AjaxUpdatedControl ControlID="pnlWOedit" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtDoorNumber">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lbltxtArrivalTime" UpdatePanelCssClass=""></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="lbltxtArrivalTime" LoadingPanelID="LoadingPanel1" UpdatePanelCssClass="" />
                <telerik:AjaxUpdatedControl ControlID="pnlWOedit" LoadingPanelID="LoadingPanel1" UpdatePanelCssClass="" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtVendorNumber">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblVendorName" />
                <telerik:AjaxUpdatedControl ControlID="txtVendorName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rbgroup">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblCheckNumber" UpdatePanelCssClass="" />
                <telerik:AjaxUpdatedControl ControlID="txt74CheckNumber" UpdatePanelCssClass="" />
                <telerik:AjaxUpdatedControl ControlID="lblAmount" />
                <telerik:AjaxUpdatedControl ControlID="txt74Amount" />
                <telerik:AjaxUpdatedControl ControlID="lblSplitAmount" />
                <telerik:AjaxUpdatedControl ControlID="txt74SplitAmount" UpdatePanelCssClass="" />
                <telerik:AjaxUpdatedControl ControlID="errAmount" />
                <telerik:AjaxUpdatedControl ControlID="txxtAmount" />
                <telerik:AjaxUpdatedControl ControlID="editlblCheckNumber" />
                <telerik:AjaxUpdatedControl ControlID="txtCheckNumber" />
                <telerik:AjaxUpdatedControl ControlID="lblAddCash" />
                <telerik:AjaxUpdatedControl ControlID="txtSplitAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="btnSaveChanges">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="btnClearForm" UpdatePanelCssClass="" />
                <telerik:AjaxUpdatedControl ControlID="myctrls" />
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelCssClass="" LoadingPanelID="LoadingPanel1" />
                <telerik:AjaxUpdatedControl ControlID="pnlWOinfo" UpdatePanelCssClass="" LoadingPanelID="LoadingPanel1" />
                <telerik:AjaxUpdatedControl ControlID="pnlWOedit" LoadingPanelID="LoadingPanel1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="btnCancel">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="btnClearForm" />
                <telerik:AjaxUpdatedControl ControlID="myctrls" />
                <telerik:AjaxUpdatedControl ControlID="pnlWOinfo" />
                <telerik:AjaxUpdatedControl ControlID="pnlWOedit" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cbLoadType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txxtAmount" />
                <telerik:AjaxUpdatedControl ControlID="txtSplitAmount" UpdatePanelCssClass="" />
                <telerik:AjaxUpdatedControl ControlID="txtCheckNumber" />
                <telerik:AjaxUpdatedControl ControlID="rbgroup" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadAjaxLoadingPanel ID="LoadingPanel1" Runat="server" Skin="Default"></telerik:RadAjaxLoadingPanel>
<%-- *******************start main table **********************--%>
<table cellpadding="0" cellspacing="0">
    <tr>
        <%-- *******************start Left Side WO Grid **********************--%>
        <td class="leftgrid,aligntop">

            <table cellpadding="0" cellspacing="0" class="lbl">
    <tr>        <%-- *******************Controls **********************--%>
        <td>
            <telerik:RadComboBox ID="cbLocations" Width="150px" Filter="Contains" runat="server" /> 
        </td>
        <td style="width:125px">
            <span style="font-family:Arial;"><asp:label  ID="lbldpStartDate" style="font-size:13px; font-weight:bold;" runat="server" /></span>
        </td>
        <td ><asp:button ID="btnRefresh" runat="server" Text="Refresh" /></td>
        <td valign="bottom" align="center">
            <asp:Button ID="btnClearForm" runat="server" Text="Add New" />
        </td>
    </tr>
    <tr>   <%-- ******************* WO Grid **********************--%>
        <td colspan="4">
<telerik:RadGrid ID="RadGrid1" Width="600px"  Height="522px" runat="server" GridLines="None"  
    AllowSorting="True" AutoGenerateColumns="False">
    <HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
<MasterTableView DataKeyNames="ID" GridLines="None">
<Columns>
    <telerik:GridTemplateColumn UniqueName="PicMe" HeaderStyle-HorizontalAlign="Justify" HeaderStyle-CssClass="padmenot" HeaderImageUrl="~/images/camera.jpg" >
        <HeaderStyle Width="30px"/>
        <ItemTemplate>
            <%#IIf(IsDBNull(Eval("PicCount")), "--", Eval("PicCount"))%>
        </ItemTemplate>
    </telerik:GridTemplateColumn>
    <telerik:GridTemplateColumn  HeaderText="Start" ItemStyle-Wrap="False" DataField="StartTime" 
        uniqueName="StartTime" Groupable="False">
        <HeaderStyle Width="60px" />
        <ItemTemplate>
            <asp:Label ID="lblStartTime" runat="server" />
            <% '# Eval("StartTime")%>
        </ItemTemplate>
    </telerik:GridTemplateColumn>
    <telerik:GridTemplateColumn UniqueName="CompTime" ItemStyle-Wrap="False" HeaderText="T.T.C." HeaderTooltip="Time To Complete" 
        DataField="CompTime" Groupable="False">
        <HeaderStyle Width="60px" />
        <ItemTemplate>
            <asp:Label ID="lblCompTime" runat="server" />
            <%'# Format(Eval("CompTime"), "hh:mm tt")%>
        </ItemTemplate>
    </telerik:GridTemplateColumn>
    <telerik:GridBoundColumn AllowSorting="true" UniqueName="DoorNumber" HeaderText="Door" DataField="DoorNumber">
        <HeaderStyle Width="40px" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn AllowSorting="true" UniqueName="PurchaseOrder" 
        HeaderText="PO #" DataField="PurchaseOrder" Groupable="False">
        <HeaderStyle Width="90px" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn DataField="Amount" UniqueName="Amount" HeaderText="Amount" DataFormatString="{0:C2}">
        <HeaderStyle Width="60px" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn AllowSorting="true" Groupable="true" UniqueName="Vendor" HeaderText="Vendor" DataField="VendorName">
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn AllowSorting="true" Groupable="true" UniqueName="Carrier" HeaderText="Carrier" DataField="CarrierName">
    </telerik:GridBoundColumn>
<%--                    <telerik:GridTemplateColumn UniqueName="Comments" HeaderText="Comments" DataField="Comments">
                        <ItemTemplate>
                            <%# format(Eval("CompTime"),"hh:mm tt") %>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>--%>
</Columns>
</MasterTableView>
    <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true" >
        <Selecting AllowRowSelect="true" />
        <scrolling allowscroll="True" usestaticheaders="True" />
    </ClientSettings>
</telerik:RadGrid>
            </td>
    </tr>
</table>

        </td>
        <td>&nbsp;</td>
        <td  class="rightforms, aligntop"><%-- ******************* Right Side **********************--%>
<table cellpadding="0" cellspacing="0" class="fullw" id="myctrls" runat="server">
    <tr>   <%-- *******************right side Controls **********************--%>
        <td>&nbsp;</td><td width="407px" style="vertical-align:bottom;text-align:right;"><span style="cursor:help;"><asp:Label ID="lblhelp" CssClass="lilBlueButton" Text="help with this page" runat="server" /></span> </td> 
    </tr>
    <tr>
        <td style="text-align:center"><asp:Label ID="lblerr" ForeColor="Red" Text="** REQUIRED!" Visible="false" runat="server" /><asp:Label ID="lblEmptyMessage" runat="server" Text="<br /><br /><br /><br /><<<----  Select a Load from left" /></td>
        <td style="vertical-align:bottom;text-align:right;"><asp:Label CssClass="lbl" ID="lblCreatedBy" runat="server" Visible="false" /></td>
    </tr>   
</table>
<table cellpadding="0" cellspacing="0" class="fullw">
    <tr>
        <td class="aligntop" ><%-- *******************start Right td cell of Main Form  **********************--%>

<asp:Panel ID="pnlWOinfo" runat="server" Visible="false"><%-- *******************start Right Side INFO Form **********************--%>
<div class="lbl" style="border:1px solid black;width:470px;">
    <%-- *******************start Section One **********************--%>
<table cellpadding="0" cellspacing="0" class="fullw">
    <tr>
        <td class="aligntop">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="3">Date worked: &nbsp;<br /><asp:Label CssClass="data" ID="lblDateWorked" runat="server" /></td>
                </tr>
                <tr>
                    <td>Load Number:<br /><asp:Label CssClass="data" ID="lblLoadNumber" runat="server" /></td>
                    <td>Door:<br /><asp:Label CssClass="data" ID="lblDoorNumber" runat="server" /></td>
                    <td>Carrier:<br /><asp:Label CssClass="data" ID="lblCarrierName" runat="server" /></td>
                </tr>
            </table>
        </td>
    </tr>
</table><%-- *******************end Row One Section One pnlWOinfo Panel **********************--%>

<table cellpadding="0" cellspacing="0">
    <tr>
        <td>Truck #: &nbsp; <br /><asp:Label CssClass="data" ID="lblTruckNumber" runat="server" /></td>
        <td>Trailer #: &nbsp; <br /><asp:Label CssClass="data" ID="lblTrailerNumber" runat="server" /></td>
        <td>Purchase Order: &nbsp; <br /><asp:Label CssClass="data" ID="lblPurchaseOrder" runat="server" /></td>
        <td>Department:<br /> <asp:Label CssClass="data" ID="lblDepartment" runat="server" /></td>
    </tr>
</table>
    <%-- *******************end Section One INFO **********************--%>

<hr />    
    <%-- *******************start Section Two INFO **********************--%>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td class="alignleft" colspan="3">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="padding-left:0px;">
                        Vendor number:
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="padding-left:0px;"><asp:Label CssClass="data" ID="lblVendorNumber" runat="server" /></td>
                    <td style="padding-left:7px;" valign="middle"><asp:Label CssClass="data" ID="lblVendorName" runat="server" /></td>
                </tr>
            </table> 
        </td>
    </tr>
    <tr>
        <td class="aligntop">
            Pallets Unloaded: <br />
            <asp:Label CssClass="data" ID="lblPalletsUnloaded" runat="server" />
        </td>
        <td>
            Pieces:<br /> 
            <asp:Label CssClass="data" ID="lblPieces" runat="server" />
        </td>
        <td>
            Load Description:<br />
            <asp:Label CssClass="data" ID="lblLoadDescription" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            Assigned employees:<br /> 
            <asp:Label CssClass="data" ID="lblUnloadersV" runat="server" />
        </td>
    </tr>
</table>
    <%-- *******************end Section Two INFO **********************--%>

<hr />
        <%-- *******************start Section Three INFO **********************--%>

<table cellpadding="0" cellspacing="0">
    <tr>
        <td class="aligntop">Pallets Received:<br /> <asp:Label CssClass="data" ID="lblPalletsReceived" runat="server" /></td>
        <td class="aligntop">App time: <br /><asp:Label CssClass="data" ID="lblAppTime" runat="server" /></td>
        <td class="aligntop">Gate Time: <br /><asp:Label CssClass="data" ID="lblGateTime" runat="server" /></td>
        <td class="aligntop">Arrival Time: <br /><asp:Label CssClass="data" ID="lblArrivalTime" runat="server" /></td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td class="aligntop">Start Time: <br /><asp:Label CssClass="data" ID="lblStartTime" runat="server" /></td>
        <td class="aligntop">Completion Time: <br /><asp:Label CssClass="data" ID="lblCompTime" runat="server" /></td>
        <td class="aligntop">Total work time (calculated): <br /><asp:Label CssClass="data" ID="lblTotalTime" runat="server" /></td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td class="aligntop">Bad Pallets: <br /><asp:Label CssClass="data" ID="lblBadPallets" Width="25px" runat="server" /></td>
        <td class="aligntop">Weight: <br /><asp:Label CssClass="data" ID="lblWeight" Width="25px" runat="server" /></td>
        <td class="aligntop">Restacks: <br /><asp:Label CssClass="data" ID="lblRestacks" Width="25px" runat="server" /></td>
        <td class="aligntop">Total items: <br /><asp:Label CssClass="data" ID="lblTotalItems" Width="25px" runat="server" /></td>
        <td class="aligntop">Load Type: <br /><asp:Label CssClass="data" ID="lblLoadType" runat="server" /></td>
    </tr>
</table>
    <%-- *******************end Section Three INFO **********************--%>
<hr />
    <%-- *******************start Section Four INFO **********************--%>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <asp:RadioButtonList ID="rbgroup74" AutoPostBack="true" RepeatDirection="Horizontal" runat="server">
                <asp:ListItem Text="Cash" />
                <asp:ListItem Text="Check" />
                <asp:ListItem Text="Card" />
                <asp:ListItem Text="Split" />
            </asp:RadioButtonList>
        </td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0" class="fullw">
    <tr>
        <td class="aligntop">Amount:</td><td><asp:Label ID="lblINFOCheckNumber" Text="Check/Transaction #:" runat="server" /> <asp:Label ID="Label5" Text="**" ForeColor="Red" runat="server" Visible="false" /></td><td><asp:Label CssClass="lbl" ID="lblINFOSplitAmount" Text="Split Amount" runat="server" /></td>
    </tr>
        <tr>
            <td><span style="cursor:help"><asp:Label CssClass="data" ID="lblAmount" runat="server" /></span><br />
                <telerik:RadNumericTextBox ID="txt74Amount" Type="Currency" EmptyMessage="$" width="55px" runat="server" Visible="false" />
            </td>
            <td class="aligntop">
                                <telerik:RadTextBox ID="txt74CheckNumber" runat="server" />
            </td>
            <td style="vertical-align:top;">    
                <asp:Label CssClass="data" ID="lblSplitAmount" runat="server" />
                <telerik:RadNumericTextBox ID="txt74SplitAmount" Type="Currency" EmptyMessage="$" width="55px" Visible="false" runat="server" />
            </td>
        </tr>
        <tr>
             <td colspan="3">BOL/PRO/Seal #: <asp:Label CssClass="data" ID="lblBOL" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="3">
               Comments: <br /><asp:Label CssClass="data" ID="lblComments" runat="server" /><telerik:RadTextBox TextMode="MultiLine" Width="400px" Rows="5" ID="txt74comments" Visible="false" runat="server" Height="60px" />
            </td>
        </tr>
    </table>
    <%-- *******************end Section Four INFO **********************--%>
</div>
    <%-- ******************* INFO Controls Table **********************--%>

<table cellpadding="0" cellspacing="0" width="95%" style="align-self:center;" >
    <tr>
        <td valign="top" style="width:80px;padding-top:9px;">&nbsp;</td>
        <td valign="top" class="lilBlue">
            <span onmouseover="this.style.pointer='pointer';"><asp:Label ID="lblLoadImages" runat="server" /></span> <br />
        </td>
        <td style="padding-top:9px;">
            <div style="float:right;text-align:center;font-size:11px; font-weight:bold; color:Red;">
                <asp:Panel id="btnclickit" runat="server">
                    <asp:Button ID="btnPrintWO" CommandArgument="Print" Text="Save / PRINT Receipt" runat="server" /><br />
                </asp:Panel>
            </div>
        </td>
    </tr>
</table>
<br />
 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
</asp:Panel>    <%-- *******************end pnlWOinfo Panel **********************--%>


<%-- *******************start Right Side pnlWOedit Panel**********************--%>
<asp:Panel ID="pnlWOedit" runat="server" Visible="False" style="margin-left: 0px">
<div class="lbl" style="border:1px solid black;width:470px" >

<table cellpadding="0" cellspacing="0" width="100%"><%-- *******************Start Section ONE pnlWOedit Panel **********************--%>
    <tr><td colspan="3">Date Worked: &nbsp;<asp:Label ID="lbldpDateWorked" Width="125px" runat="server" /></td></tr>
    <tr>
        <td class="aligntop,alignleft" style=" padding-left:0px; float:left;">
            <table cellpadding="0" cellspacing="0" class="alignleft" style="margin-left: 0px">
                <tr>
                    <td class="aligntop">Load Number:</td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadTextBox ID="txtLoadNumber" AutoPostBack="true" Width="90px" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
               <td class="aligntop,alignleft" style=" padding-left:0px; float:left;">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>Door:<asp:Label ID="errDoor" Text="**" ForeColor="Red" runat="server" Visible="false" /></td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadTextBox ID="txtDoorNumber" AutoPostBack="true" Width="30px" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
                 <td class="aligntop,alignleft" style=" padding-left:0px; float:left;">
            <table cellpadding="0" cellspacing="0">
                <tr>
                            <td>
                        Carrier:<asp:Label ID="errCarrier" Text="**" ForeColor="Red" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                            <td >
                        <telerik:RadCombobox RenderMode="Lightweight" ID="cbCarrier" runat="server" Width="195px" 
                            EmptyMessage="Select Carrier" DataSourceID="SqlDataSource1" DataTextField="Name"
                            DataValueField="ID" EnableAutomaticLoadOnDemand="True" ItemsPerRequest="10"
                            ShowMoreResultsBox="True" EnableVirtualScrolling="True" />
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:rtdsConnectionString %>" 
                            SelectCommand="SELECT [Name], [ID] FROM [Carrier] ORDER By [Name]"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table><%-- *******************end Row One Section One pnlWOedit Panel **********************--%>

<table width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td>Truck #:<asp:Label ID="errTruck" Text="**" ForeColor="Red" runat="server" Visible="false" /> &nbsp; <br /><telerik:RadTextBox ID="txtTruckNumber" Width="45px" runat="server" /></td>
        <td>Trailer #:<asp:Label ID="errTrailer" Text="**" ForeColor="Red" runat="server" Visible="false" /> &nbsp; <br /><telerik:RadTextBox ID="txtTrailerNumber" Width="50px" runat="server" /></td>
        <td>Purchase Order:<asp:Label ID="errPurchaseOrder" Text="**" ForeColor="Red" runat="server" Visible="false" /> &nbsp; <br /><telerik:RadTextBox ID="txtPurchaseOrder" Width="80px" runat="server" /></td>
        <td>Department:<asp:Label ID="errDepartment" Text="**" ForeColor="Red" runat="server" Visible="false" /><br /> <telerik:RadComboBox ID="cbDepartment" Width="100px" runat="server" AllowCustomText="true" EmptyMessage="Select ..."  /></td>
    </tr>
</table>
    <%-- *******************end Section One pnlWOedit Panel **********************--%>

<hr />
    <%-- *******************Start Section Two pnlWOedit Panel **********************--%>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td align="left" colspan="3">
            <table cellpadding="0" cellspacing="0"><tr>
                <td style="padding-left:0px;">Vendor Number:<asp:Label ID="errVendorNumber" Text="**" ForeColor="Red" runat="server" Visible="false" /> &nbsp; </td>
                <td></td>
            </tr><tr>
                <td style="padding-left:0px;"><telerik:RadTextBox ID="txtVendorNumber" Width="70px" runat="server" /></td>
                <td style="padding-left:7px;" valign="middle"><asp:Label CssClass="data" ID="txtVendorName" runat="server" /></td>
            </tr></table>
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top;">
            Pallets Unloaded:<asp:Label ID="errPalletsUnloaded" Text="**" ForeColor="Red" runat="server" Visible="false" /><br />
            <telerik:RadNumericTextBox ID="txtPalletsUnloaded" Width="45" NumberFormat-DecimalDigits="0" runat="server" />
        </td>
        <td>
            Pieces:<asp:Label ID="errPieces" Text="**" ForeColor="Red" runat="server" Visible="false" /><br />
            <telerik:RadNumericTextBox  Width="50px" ID="txtPieces" runat="server" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="," />
        </td>
        <td>
            Load Description:<asp:Label ID="errLoadDescription" Text="**" ForeColor="Red" runat="server" Visible="false" /><br />
            <telerik:RadComboBox ID="cbLoadDescription" runat="server" AllowCustomText="true" EmptyMessage="Select ..." />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            Assigned employees:<asp:Label ID="errunloaders" Text="**" ForeColor="Red" runat="server" Visible="false" /> &nbsp; &nbsp;<asp:Label ID="lblEditUnloaders" runat="server" Visible="false" /><br />
            <asp:Label CssClass="data" ForeColor="Orange"  ID="txtUnloaders"  runat="server" /><asp:TextBox ID="txtUnloaderNamelist" Visible="false" runat="server" /><asp:TextBox ID="txtUnloaderIDlist" runat="server" Visible="false" />
        </td>
    </tr>
</table>
<%-- *******************end Section TWO pnlWOedit Panel **********************--%>

<hr />
    <%-- *******************start Section Three pnlWOedit Panel **********************--%>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td>Pallets Received:<asp:Label ID="errPalletsReceived" Text="**" ForeColor="Red" runat="server" Visible="false" /><br /> <telerik:RadNumericTextBox Width="45" ID="txtPalletsReceived" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="," runat="server" /></td>
        <td style="vertical-align:top;">App time: <br /><telerik:RadTimePicker ID="txtAppTime" MinDate="1/1/1900" MaxDate="12/31/2299" runat="server" Width="65px" TimePopupButton-Visible="false" /></td>
        <td style="vertical-align:top;">Gate Time: <br /><asp:label ID="lbltxtGateTime" runat="server" Width="65px" />&nbsp;</td>
        <td style="vertical-align:top;">Arrival Time: <br /><asp:label ID="lbltxtArrivalTime" runat="server" Width="65px" />&nbsp;</td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td style="vertical-align:top;">Start Time: <br /><asp:label ID="lbltxtStartTime" runat="server" Width="65px" /><asp:TextBox ID="txtStartTime" runat="server" Visible="false" /> &nbsp;</td>
        <td style="vertical-align:top;">Completion Time: <br /><asp:label ID="lbltxtCompTime" runat="server" Width="65px" style="height: 15px" /><asp:TextBox ID="txtCompTime" runat="server" Visible="false" />&nbsp;</td>
        <td style="vertical-align:top;"><asp:Label ID="lblEditTotalTimeLabel" Text="Total work time" runat="server" /><br /><asp:Label CssClass="data" ID="lblEditTotalTime" runat="server" />&nbsp;</td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td style="vertical-align:top;">Bad Pallets:<asp:Label ID="errBadPallets" Text="**" ForeColor="Red" runat="server" Visible="false" /> <br /><telerik:RadNumericTextBox ID="txtBadPallets" AutoPostBack="true" Width="45" NumberFormat-DecimalDigits="0" runat="server" /></td>
        <td style="vertical-align:top;">Weight:<asp:Label ID="errWeight" Text="**" ForeColor="Red" runat="server" Visible="false" /> <br /><telerik:RadNumericTextBox ID="txtWeight" Width="45px" runat="server"  NumberFormat-DecimalDigits="0"/></td>
        <td style="vertical-align:top;">Restacks:<asp:Label ID="errRestacks" Text="**" ForeColor="Red" runat="server" Visible="false" /> <br /><telerik:RadNumericTextBox ID="txtRestacks" AutoPostBack="true" Width="45" runat="server" NumberFormat-DecimalDigits="0" /></td>
        <td style="vertical-align:top;">Total items:<asp:Label ID="errTotalItems" Text="**" ForeColor="Red" runat="server" Visible="false" /> <br /><telerik:RadNumericTextBox ID="txtTotalItems" Width="45px" runat="server" NumberFormat-DecimalDigits="0" /></td>
        <td style="vertical-align:top;">Load Type:<asp:Label ID="errLoadType" Text="**" ForeColor="Red" runat="server" Visible="false" /> <br /><telerik:RadComboBox ID="cbLoadType" Width="100px" AutoPostBack="true" runat="server" AllowCustomText="true" EmptyMessage="Select ..." /></td>
    </tr>
</table>
    <%-- *******************end Section Three pnlWOedit Panel **********************--%>
<hr />
    <%-- *******************start Section Four pnlWOedit Panel **********************--%>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <asp:RadioButtonList ID="rbgroup" AutoPostBack="true" RepeatDirection="Horizontal" runat="server">
                <asp:ListItem Text="Cash" />
                <asp:ListItem Text="Check" />
                <asp:ListItem Text="Card" />
                <asp:ListItem Text="Split" />
            </asp:RadioButtonList>
        </td>
    </tr>
</table>

    <table style="width:400px" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width:100px">Amount:&nbsp;<asp:Label ID="errAmount" Text="**" ForeColor="Red" runat="server" Visible="false" /> </td>
            <td style="width:150px"><asp:label id="editlblCheckNumber" Text="Check/Transaction #: " runat="server" />&nbsp;<asp:Label ID="errCheckNumber" Text="**" ForeColor="Red" runat="server" Visible="false" /></td>
            <td style="width:150px"><asp:label  id="lblAddCash" Text="Split&nbsp;Amount: " runat="server" />&nbsp;<asp:Label ID="errSplitAmount" Text="**" ForeColor="Red" runat="server" Visible="false" /></td>
        </tr>
        <tr>
            <td style="width:100px"><telerik:RadNumericTextBox ID="txxtAmount" Width="65px" Type="Currency" EmptyMessage="$" runat="server" /></td>
            <td style="width:150px"><telerik:RadTextBox ID="txtCheckNumber" runat="server" /></td>
            <td style="width:150px"><telerik:RadNumericTextBox ID="txtSplitAmount" Width="50px" Type="Currency" EmptyMessage="$" runat="server" /></td>
        </tr>
    </table>

    <table cellpadding="0" cellspacing="0">
    <tr>
        <td style="padding-top:7px;">BOL/PRO/Seal #:
            <telerik:RadTextBox ID="txtBOL" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            Comments: <br /><telerik:RadTextBox TextMode="MultiLine" Width="400px" Rows="4" ID="txtComments" runat="server" Height="40px" />
        </td>
    </tr>
    </table>
    <%-- *******************end Section Four pnlWOedit Panel **********************--%>
    <%-- *******************pnlWOedit Panel Controls **********************--%>
<table cellpadding="0" cellspacing="0" width="95%" style="align-self:center;">
    <tr>
        <td valign="top" style="padding-top:9px;">
            <asp:Button ID="btnSaveChanges" Text="Save/Close" CausesValidation="true" runat="server" /><br />
            <asp:Label ID="lblChangesSaved" Visible="false" runat="server" Text="Saved" style="font-size:10px;color:Blue;" />
        </td>
        <td valign="top" class="lilBlue">
            <span onmouseover="this.style.pointer='pointer';"><asp:Label ID="lblEditLoadImages" runat="server" /></span> 
        </td>
        <td align="right" valign="top" style="padding-top:9px;">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel - NO SAVE" />
        </td>
    </tr>
</table> 
<asp:Label ID="vid" runat="server" style="visibility:hidden;" />
    </div>
    <telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="lblhelp" IsClientID="true" Width="650px" RelativeTo="Element"  
    ShowEvent="OnClick" Position="BottomLeft" OffsetX="500" OffsetY="70"  HideEvent="ManualClose"  
     Animation="FlyIn"  EnableShadow="true">
<table cellpadding="0" cellspacing="0" width="100%"><tr>
<td class="attHeader" style="text-align:center"><span>Online Load Editor DATA ENTRY</span>
</td>
</tr></table>

<table><tr><td style="padding:0 8px;">
<table>
    <tr>
        <td class="ttTitle">Use FULL Screen</td>
    </tr>
    <tr>
        <td class="ttBody">
            The F11 key on your keyboard will toggle most browsers to full screen.<br />
            F11 again to toggle back. &nbsp;Try it now!&nbsp; &nbsp;(MSIE and Chrome tested)
        </td>
    </tr>
        <tr>
        <td  class="ttBody">
            Once you have selected a location from the dropdown box (if enabled), the grid (behind this help screen) lists all of <b>TODAYS</b> Loads. (<em>Only</em>)
            <br />
        </td>
    </tr>
    <tr>
        <td class="ttTitle">PRO Tips and things to take note of</td>
    </tr>
    <tr>
        <td  class="ttBody">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="vertical-align:top;padding-right:5px;">1</td>
                <td><span class="auto-style1">Pro Tip</span>: After entering the <strong>Door Number</strong>, click anywhere just outside the textbox <span class="auto-style1"><em>before</em></span> you click the next box to give the form a chance to automatically set the Arrival/Dock Time. <em>(see #4 PRO Tip)</em></td>
            </tr>
            <tr>
                <td style="vertical-align:top;padding-right:5px;" class="auto-style2">2</td>
                <td class="auto-style2"><span class="auto-style1"><strong>NEW</strong></span>: The Carrier selector will Load On Demand as you begin to type. IF your carrier is not found, please clear the textbox and search for NOT LISTED, then, provide that carrier&#39;s name in the <strong>Comments</strong> Section.</td>
            </tr>
            <tr>
                <td style="vertical-align:top;padding-right:5px;">3</td>
                <td><span class="auto-style1">note</span>: If the <strong>Vendor Number </strong>you&nbsp; enter is &#39;not found&#39;&nbsp; you MUST provide the <strong>Vendor Name</strong> in the <strong>Comments</strong> section.</td>
            </tr>
            <tr>
                <td style="vertical-align:top;padding-right:5px;">4</td>
                <td><span class="auto-style1">PRO Tip</span>: Similar to the Door Number textbox, the <strong>Bad Pallets</strong> and <strong>Restacks</strong> Textboxes each do a quick validation when you enter a value then click the next field. This may cause you to have to click the next field twice. Avoid this by clicking just outside of these fields before clicking the next field.</td>
            </tr>
            <tr>
                <td style="vertical-align:top;padding-right:5px;">5</td>
                <td><span class="auto-style1">note</span>: You will not be able to <strong>select unloaders</strong> or enter <strong>Bad Pallets</strong> or <strong>Restacks</strong> when you &#39;<strong>Add New</strong>&#39; load. Each load must first be created and saved. Suggestion: Complete top section, then save. (in fact, you will not be able to save any load until the top section, at a minimum, is complete)</td>
            </tr>
            <tr>
                <td style="vertical-align:top;padding-right:5px;">6</td>
                <td><span class="auto-style1">note</span>: You will not be able to enter <strong>Bad Pallets</strong> or <strong>Restacks</strong> before selecting unloaders and completing all required fields.
                    <br />
                    If you save Bad Pallets or Restacks without having completed the required fields, you may have to re-enter some of the information.</td>
            </tr>            
            <tr>
                <td style="vertical-align:top;padding-right:5px;">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="vertical-align:top;padding-right:5px;">-</td>
                <td>If you like, you may leave this help file open while entering your first few loads.</td>
            </tr>
            <tr>
                <td style="vertical-align:top;padding-right:5px;">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="vertical-align:top;padding-right:5px;">&nbsp;</td>
                <td style="text-align:center">Remember, you are literally putting your good name on each load record you create ... do it right ...<br /> Do it with Pride!</td>
            </tr>
        </table>

        </td>
    </tr>

</table><br />
<center>To Close - Click X in upper right corner</center>

</td></tr></table>
</telerik:RadToolTip>

    </asp:Panel>            <%-- *******************END Right Side WO Form **********************--%>
        </td>
    </tr>
</table>

            </td>
    </tr>
</table>
<%-- '***********************************************************************************************************************************
                                                          TOOL TIP
     '***********************************************************************************************************************************--%>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" VisibleStatusbar="false" >
<Windows>

<telerik:RadWindow ID="winDupReceipts"
        VisibleStatusbar="false" runat="server" Height="625" Width="500"
         Skin="Sunset" EnableShadow="True"
    ShowContentDuringLoad="true"  Modal="True" 
    Title="SEU Receipt Generator"
    OffsetElementID="<%=cbCarrierName.ClientID%>" 
    Left="400"
    Top="-100"
    OnClientClose = "OnClientCloseVendor"
    Behaviors="Close, Resize, Maximize, Move" />

<telerik:RadWindow ID="wVendor" Height="500px" Width="410px"  
    ShowContentDuringLoad="true"  runat="server" Modal="True" 
    Title="Select Vendor"
    OffsetElementID="<%=cbCarrierName.ClientID%>" 
    Left="400"
    Top="-100"
    OnClientClose = "OnClientCloseVendor"
    Behaviors="Move, Close" />

<telerik:RadWindow ID="wUnloader" Height="260px" Width="410px"  
    ShowContentDuringLoad="true"  runat="server" Modal="True" 
    Title="Select Unloaders"
    OffsetElementID="<%=cbCarrierName.ClientID%>" 
    Left="400"
    Top="-100"
    OnClientClose = "OnClientCloseUnloader"
    Behaviors="Move, Close" />

<telerik:RadWindow ID="wLoadImages" Height="560px" Width="875px"  
    ShowContentDuringLoad="false"  runat="server" Modal="True" 
    Title="SEU - Load Image Manager" OnClientClose="closeit"
    Behaviors="Move, Resize, Maximize, Close" />

<telerik:RadWindow ID="winFreightIssues" Title="SEU Freight Issue Photo Report Generator" 
        VisibleStatusbar="false" runat="server" Height="625" Width="500" Modal="True"
        Behaviors="Close, Resize, Maximize, Move" Skin="Sunset" EnableShadow="True" OnClientClose="closeit">
</telerik:RadWindow>
</Windows>
    </telerik:RadWindowManager>

<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
<script type="text/javascript">
        function callBackFn(arg) {
//            alert("this is the client-side callback function. The RadAlert returned: " + arg);
        }
    function openLoadImages(arg) {
        var oManager = GetRadWindowManager();
        var loca = "../Upload/Async/Imageuploader/LoadImages.aspx?woid=" + arg;
        oManager.open(loca, "wLoadImages");
    }

    function openQuikAddLoadImages(arg) {
        document.getElementById("<%= btnSaveChanges.ClientID %>").disabled = true;
        var oManager = GetRadWindowManager();
        var loca = "../Upload/Async/Imageuploader/QuikAddLoadImages.aspx?woid=" + arg;
        oManager.open(loca, "wLoadImages");
    }




        // ************************** Edit Vendor Script ************************
    function openVendor() {
        //        var vnum = document.getElementById("<%= vid.ClientID %>").innerText;
        var oManager = GetRadWindowManager();
        var loca = "addVendor.aspx";
        oManager.open(loca, "wVendor");
    }

    function OnClientCloseVendor(sender, args) {
        if (args.get_argument() != null) {
            var arg = args.get_argument();
            var sarg = arg.split(":");
            var VendorNumber = $find("<%= txtVendorNumber.ClientID %>");
            var VendorName = document.getElementById("<%= txtVendorName.ClientID %>");
            VendorNumber.set_value(sarg[2]);
            VendorName.innerText = sarg[2] + "-" + sarg[3];
            VendorName.style.fontSize = '12px';
            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
            ajaxManager.ajaxRequest(arg);

            }
            //            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest(sarg);
            //                    var dv = document.getElementById("divCompInfo")
            //        dv.style.visibility = "visible";
            //        if (args.get_argument() != null) {
            //            var arg = args.get_argument();
            //            var sarg = "Client:" + arg;
            //            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest(sarg);
            //            
            //            
                }

    function openUnloaders(arg) {
        var loca = "selectUnloaders.aspx?woid=" + arg;
        var oManager = GetRadWindowManager();
        oManager.open(loca, "wUnloader");
    }

    function OnClientCloseUnloader(sender, args) {
        if (args.get_argument() != null) {
            var arg = args.get_argument();
            var Unloaders = document.getElementById("<%= txtUnloaders.ClientID %>")
            Unloaders.innerText = arg.split("|", 2)[0];
            Unloaders.style.color = 'blue';
            Unloaders.style.fontSize = '12px';
            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
            ajaxManager.ajaxRequest("Unloader:" + arg);

        }
    }

    function closeit(oWnd, args) {
        oWnd.setUrl("../seuLoader.aspx");
    }


            //function OnClientDropDownClosed(sender, args) {
            //    sender.clearItems();
            //            alert(sender);
            //    if (args.get_domEvent().stopPropagation)
            //        args.get_domEvent().stopPropagation();
            //}

    function btnSaveClick() {
        var SavedBtn = document.getElementById("<%= lblChangesSaved.ClientID %>");
        SavedBtn.style.visibility = 'visible';

        var VendorName = document.getElementById("<%= txtVendorName.ClientID %>");
        VendorName.style.color = 'black';
        var UnloaderList = document.getElementById("<%= txtUnloaders.ClientID %>");
        UnloaderList.style.color = 'black';

    }

    function vendorLookup(i) {
        var t = i.value;
        if (t.length > 0) {
            t = t.replace(/[^\da-zA-Z]+/g, '');
            i.value = t;
        }
        if (t.length > 2) {
            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
            ajaxManager.ajaxRequest("VendorLookup:" + t);
        }
    }



            //                function RowClick(sender, eventArgs) {
            //                    sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            //                }
            //                
            //                function RowSelected(sender, eventArgs) {
            //                    alert(eventArgs.getDataKeyValue("ID="));
            ////                    sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            //                }
    function btnEditClick() {
        document.getElementById("<%=pnlWOinfo.ClientID%>").style.display = "none";
                    document.getElementById("<%=pnlWOedit.ClientID%>").style.display = "inline";
                    return false;
                }
                function btnCancelClick() {
                    document.getElementById("<%=pnlWOinfo.ClientID%>").style.display = "inline";
                    document.getElementById("<%=pnlWOedit.ClientID%>").style.display = "none";
                    return false;
                }

    function OpenWinDupReceipts(arg) {
        var oWnd = $find("<%= winDupReceipts.ClientID %>");
             oWnd.setUrl("../ClientSvcs/seuDuplicateReceipts.aspx?woid=" + arg);
             oWnd.show();
         }

    
</script>



</telerik:RadCodeBlock>


<%--<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
<script type="text/javascript">

</script>
</telerik:RadScriptBlock>--%>
 

    </form>


</body>
</html>

