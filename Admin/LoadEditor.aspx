<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoadEditor.aspx.vb" Inherits="DiversifiedLogistics.LoadEditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
    <title></title>
<style type="text/css">
.data{ font-size:14px;font-weight:bold;}
.lbl{font-size:12px;font-weight:normal;}
.lbl td{padding-left:24px;}
.ColorMeRed {color:Red;}

    .style2
    {
        width: 113px;
    }

    .style3
    {
        width: 407px;
    }
.lilBlue{
    font-family:Arial;
    color:Blue;
    text-decoration:underline;
    padding-top:18px;
}
    .padmenot{
    padding:0px;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <%--<telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />--%>
    <div>

        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
<%--            <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="btnSaveChanges" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="pnlWOedit"  />
                     <telerik:AjaxUpdatedControl ControlID="pnlWOinfo"  />
                     <telerik:AjaxUpdatedControl ControlID="btnEdit"  />
                    <telerik:AjaxUpdatedControl ControlID="txtVendorName" LoadingPanelID="RadAjaxLoadingPanel1" />
                 </UpdatedControls>
            </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSubmit">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="LoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnClearForm">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="btnClearForm" LoadingPanelID="LoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="lblEmptyMessage" />
                        <telerik:AjaxUpdatedControl ControlID="pnlWOinfo"  />
                        <telerik:AjaxUpdatedControl ControlID="pnlWOedit"  />
                    </UpdatedControls>
                </telerik:AjaxSetting>

                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="btnClearForm" />
                        <telerik:AjaxUpdatedControl ControlID="lblCreatedBy" />
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                        <telerik:AjaxUpdatedControl ControlID="lblEmptyMessage" 
                            LoadingPanelID="LoadingPanel1" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="pnlWOinfo" 
                            LoadingPanelID="LoadingPanel1" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="pnlWOedit" 
                            LoadingPanelID="LoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnEdit">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pnlWOinfo" 
                            LoadingPanelID="LoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="pnlWOedit" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnCloseWO">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                        <telerik:AjaxUpdatedControl ControlID="btnclickit" 
                            LoadingPanelID="LoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnDeleteLoad">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblCreatedBy" />
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" 
                            LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblEmptyMessage" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="pnlWOinfo" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="pnlWOedit" UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSaveChanges">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pnlWOinfo" 
                            LoadingPanelID="LoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="pnlWOedit" 
                            LoadingPanelID="LoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" />

                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnCancel">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="btnClearForm" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="lblEmptyMessage" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="pnlWOinfo" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="pnlWOedit" UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>--%>
        </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="LoadingPanel1" Runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxLoadingPanel Transparency="50" ID="RadAjaxLoadingPanel2"  runat="server" Width="210px" Height="210px">
 <asp:Image id="Image1" runat="server" Width="200px" Height="200px"></asp:Image>
    </telerik:RadAjaxLoadingPanel>

<table cellpadding="0" cellspacing="0" style="width: 1180px">
    <tr>
        <td class="style3">
<table class="lbl">
    <tr>
        <td colspan="4">
            <telerik:RadTextBox ID="txtFilter" runat="server" />&nbsp;&lt;&lt;&lt;&nbsp;
            <span style="font-size:9px; font-family:Arial;">Filter by: Department, PO #, Vendor #, Load Type or Comments</span>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadComboBox ID="cbLocations" Width="150px" Filter="Contains" runat="server" /> 
        </td>
        <td>
            <telerik:RadDatePicker ID="dpStartDate" Width="110px" runat="server" />
        </td>
        <td>
            <telerik:RadDatePicker ID="dpEndDate" Width="110px" runat="server" />
        </td>
        <td>
            <asp:Button ID="btnSubmit" runat="server" Text="Show Selected Range"  />
        </td>

    </tr>
</table>
        </td>
        <td valign="bottom" align="center">
<asp:Button ID="btnClearForm" runat="server" Text="Add New" />
        </td>
        <td valign="bottom"> 
<button ID="radbut1" runat="server" style="background-color:red;" visible="false">Email<br />Pictures</button> 
        </td>
<td valign="bottom" align="right" width="300"><asp:Label CssClass="lbl" ID="lblCreatedBy" runat="server" Visible="false" /></td>
    </tr>   
</table>

<table>
    <tr>
        <td valign="top">
        <%-- *******************start Left Side WO Grid **********************--%>
<telerik:RadGrid ID="RadGrid1" Width="700px"  Height="580px" runat="server" GridLines="None"  
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
    <telerik:GridTemplateColumn>
        <HeaderStyle Width="30px" />
                <ItemTemplate>
            <%#IIf(IsDBNull(Eval("userID")), "<img src='../images/LockRedOpen.jpg'>", "<img src='../images/LockGreenClosed.jpg'>")%>
        </ItemTemplate>
    </telerik:GridTemplateColumn>

    <telerik:GridTemplateColumn SortExpression="LogDate" UniqueName="Date" 
      HeaderText="LogDate" DataField="LogDate" ItemStyle-Wrap="False" 
      GroupByExpression="LogDate" Groupable="False">
        <HeaderStyle Width="110px" />
        <ItemTemplate>
            <%# Format(Eval("LogDate"), "dd-MMM-yyyy ddd")%>
        </ItemTemplate>
        <ItemStyle Wrap="False"></ItemStyle>
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
        <HeaderStyle Width="60px" />
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
<%-- *******************end Left Side WO Grid **********************--%>
</td>
<td></td>
<td valign="top">
<%-- *******************start Right Side WO Form **********************--%>

<center><asp:Label ID="lblEmptyMessage" runat="server" Text="<<<----  Select a Load from left" /></center>


<asp:Panel ID="pnlWOinfo" runat="server" Visible="false">
<%-- *******************start Right Side INFO Form **********************--%>
<div class="lbl" style="border:1px solid black;width:470px;">
<table>
<tr><td colspan="4">Date worked: &nbsp;<br /><asp:Label CssClass="data" ID="lblDateWorked" runat="server" /></td></tr>
    <tr>
<td>Load Number:<br /><asp:Label CssClass="data" ID="lblLoadNumber" runat="server" />
</td>
<td>Door:<br /> <asp:Label CssClass="data" ID="lblDoorNumber" runat="server" />
</td>
<td>Cash:<br /> <asp:Label CssClass="data" ID="lblIsCash" runat="server" />
</td>
<td>Department:<br /> <asp:Label CssClass="data" ID="lblDepartment" runat="server" />
</td>
</tr></table>
<table>
<tr>
<td>Carrier:<br />
<asp:Label CssClass="data" ID="lblCarrierName" runat="server" />
</td>
<td>Truck #: &nbsp; <br /><asp:Label CssClass="data" ID="lblTruckNumber" runat="server" /></td>
<td>Trailer #: &nbsp; <br /><asp:Label CssClass="data" ID="lblTrailerNumber" runat="server" /></td>
<td>Purchase Order: &nbsp; <br /><asp:Label CssClass="data" ID="lblPurchaseOrder" runat="server" /></td>
</tr>
</table>
<hr />
<table>
<tr>
<td colspan="3">Vendor number:<br /> <asp:Label CssClass="data" ID="lblVendorNumber" runat="server" />
&nbsp; &nbsp; <asp:Label CssClass="data" ID="lblVendorName" runat="server" />
</td>
</tr>
<tr>
<td style="vertical-align:top;">Pieces:<br /> <asp:Label CssClass="data" ID="lblPieces" runat="server" /></td>
<td style="vertical-align:top;">Pallets Received:<br /> <asp:Label CssClass="data" ID="lblPalletsReceived" runat="server" /></td>
<td style="vertical-align:top;">Load Description:<br /> <asp:Label CssClass="data" ID="lblLoadDescription" runat="server" /></td>
</tr>
<tr>
    <td colspan="3">
Assigned employees:<br /> 
<asp:Label CssClass="data" ID="lblUnloadersV" runat="server" />
    </td>
</tr>
</table>
<hr />
<table>
<tr>
<td style="vertical-align:top;">Pallets Unloaded: <br /><asp:Label CssClass="data" ID="lblPalletsUnloaded" runat="server" /></td>
<td style="vertical-align:top;">App time: <br /><asp:Label CssClass="data" ID="lblAppTime" runat="server" /></td>
<td style="vertical-align:top;">Gate Time: <br /><asp:Label CssClass="data" ID="lblGateTime" runat="server" /></td>
<td style="vertical-align:top;">Arrival Time: <br /><asp:Label CssClass="data" ID="lblArrivalTime" runat="server" /></td>
</tr>
</table>
<table>
<tr>
<td style="vertical-align:top;">Start Time: <br /><asp:Label CssClass="data" ID="lblStartTime" runat="server" /></td>
<td style="vertical-align:top;">Completion Time: <br /><asp:Label CssClass="data" ID="lblCompTime" runat="server" /></td>
<td style="vertical-align:top;">Total work time (calculated): <br /><asp:Label CssClass="data" ID="lblTotalTime" runat="server" /></td>
</tr>
</table>
<table>
<tr>
<td>Bad Pallets: <br /><asp:Label CssClass="data" ID="lblBadPallets" Width="25px" runat="server" /></td>
<td>Weight: <br /><asp:Label CssClass="data" ID="lblWeight" Width="25px" runat="server" /></td>
<td>Restacks: <br /><asp:Label CssClass="data" ID="lblRestacks" Width="25px" runat="server" /></td>
<td>Total items: <br /><asp:Label CssClass="data" ID="lblTotalItems" Width="25px" runat="server" /></td>
<td>Load Type: <br /><asp:Label CssClass="data" ID="lblLoadType" runat="server" /></td>
</tr>
</table>
<hr />
<table>
    <tr>
<td>Check/Transaction #:<br /> <asp:Label CssClass="data" ID="lblCheckNumber" runat="server" /></td>
<td>Amount:<br /> <asp:Label CssClass="data" ID="lblAmount" runat="server" /><br />
    Split Amount:<br /> <asp:Label CssClass="data" ID="LblSplitAmount" runat="server" />
</td>

    </tr>
    <tr>
<td colspan="2">BOL/PRO/Seal #:<br /> <asp:Label CssClass="data" ID="lblBOL" runat="server" /></td>
</tr>
<tr>
<td colspan="2">
Comments: <br /><asp:Label CssClass="data" ID="lblComments" runat="server" />
</td>
</tr>
</table>
</div>
<table width="95%" align="center">
    <tr>
        <td valign="top" style="width:80px;padding-top:9px;">
            <asp:Button ID="btnEdit" Text="Edit" runat="server" />
        </td>
        <td valign="top" class="lilBlue">
            <span onmouseover="this.style.pointer='pointer';"><asp:Label ID="lblLoadImages" runat="server" /></span> <br />
        </td>
        <td style="padding-top:9px;">
            <div style="float:right;text-align:center;font-size:11px; font-weight:bold; color:Red;">
            <asp:Panel id="btnclickit" runat="server">
                <asp:Button ID="btnCloseWO" CommandArgument="LockLoad"  Text="CLOSE / LOCK this Load" runat="server" /><br />
                Clicking this button<br /> REMOVES the Dock Supervisor's <br />ability to edit this Load</asp:Panel>
            </div>
        </td>
    </tr>
</table>
<br />
 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <asp:Button ID="btnDeleteLoad" runat="server" OnClientClick="if (!confirm('Delete This Load Record?')) return false;" Visible="false" Text="Delete This Load Record?" />
</asp:Panel>

<%-- *******************start Right Side EDIT Form **********************--%>
<asp:Panel ID="pnlWOedit" runat="server" Visible="False">
<div class="lbl" style="border:1px solid black;width:470px" >
<table><tr>
<td colspan="4">Date worked: &nbsp;<telerik:RadDatePicker ID="dpDateWorked" Width="95px" runat="server" />
</td>
</tr><tr>
    <td>Load Number: <br /><telerik:RadTextBox ID="txtLoadNumber" runat="server" Width="90px" /></td>
<td>Door:<br /> <telerik:RadTextBox ID="txtDoorNumber" Width="30px" runat="server" />
</td>
<td valign="top">Cash:<br />
</td>
<td>Department:<br /> <telerik:RadComboBox ID="cbDepartment" Width="100px" runat="server" AllowCustomText="true" EmptyMessage="Select ..."  />
</td>
</tr></table>
<table cellspacing="0" cellpadding="0">
    <tr>
        <td>Carrier: &nbsp; <span onclick="openCarrier();" onmouseover="this.style.cursor='pointer';" style="color:blue;font-size:11px;"><asp:Label ID="lblChangeSelectCarrier" Text="Chxange" runat="server" /></span><br />
    <asp:Label CssClass="data" ID="lblCarrierNamev" runat="server" /><asp:TextBox ID="txtCarrierIDv" runat="server" Visible="false" />
</td>
        <td>Truck #: &nbsp; <br /><telerik:RadTextBox ID="txtTruckNumber" Width="45px" runat="server" /></td>
        <td>Trailer #: &nbsp; <br /><telerik:RadTextBox ID="txtTrailerNumber" Width="50px" runat="server" /></td>
        <td>Purchase Order: &nbsp; <br /><telerik:RadTextBox ID="txtPurchaseOrder" Width="80px" runat="server" /></td>
    </tr>
</table>
<hr />
<table cellpadding="0" cellspacing="0">
    <tr>
        <td align="left" colspan="3">
            <table cellpadding="0" cellspacing="0"><tr>
                <td style="padding-left:0px;">Vendor Number: &nbsp; </td>
                <td><span onclick="openVendor();" onmouseover="this.style.cursor='pointer';" style="color:blue;font-size:11px;"><asp:Label ID="lblEditVendorList" Text="Vendor List" runat="server" /></span></td>
            </tr><tr>
                <td style="padding-left:0px;"><telerik:RadTextBox ID="txtVendorNumber" Width="70px" runat="server" /></td>
                <td style="padding-left:7px;" valign="middle"><asp:Label CssClass="data" ID="txtVendorName" runat="server" /></td>
            </tr></table>
        </td>
    </tr>
    <tr>
<td>Pieces:<br /> <telerik:RadNumericTextBox  Width="45px" ID="txtPieces" runat="server" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="," /></td>
<td>Pallets Received:<br /> <telerik:RadNumericTextBox Width="30px" ID="txtPalletsReceived" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="," runat="server" /></td>
<td>Load Description:<br /> <telerik:RadComboBox ID="cbLoadDescription" runat="server" AllowCustomText="true" EmptyMessage="Select ..." /></td>
</tr>
    <tr>
        <td colspan="3">
Assigned employees: &nbsp; &nbsp;<asp:Label ID="lblEditUnloaders" runat="server" /><br /> 
<asp:Label CssClass="data" ID="txtUnloaders" runat="server" /><asp:TextBox ID="txtUnloaderIDlist" Text="nc" runat="server" Visible="false" />
    </td>
    </tr>
</table>
<hr />
<table>
<tr>
<td>Pallets Unloaded: <br /><telerik:RadNumericTextBox ID="txtPalletsUnloaded" Width="45" NumberFormat-DecimalDigits="0" runat="server" /></td>
<td>App time: <br /><telerik:RadTimePicker ID="txtAppTime" MinDate="1/1/1900" MaxDate="12/31/2299" runat="server" Width="65px" TimePopupButton-Visible="false" /></td>
<td>Gate Time: <br /><telerik:RadTimePicker ID="txtGateTime" MinDate="1/1/1900" MaxDate="12/31/2299" runat="server" Width="65px" TimePopupButton-Visible="false" /></td>
<td>Arrival Time: <br /><telerik:RadTimePicker ID="txtArrivalTime" MinDate="1/1/1900" MaxDate="12/31/2299" runat="server" Width="65px"  TimePopupButton-Visible="false"/></td>
</tr>
</table>
<table>
<tr>
<td>Start Time: <br /><telerik:RadTimePicker MinDate="1/1/1900" MaxDate="12/31/2299" ID="txtStartTime" runat="server" Width="65px"  TimePopupButton-Visible="false"/></td>
<td>Completion Time: <br /><telerik:RadTimePicker MinDate="1/1/1900" MaxDate="12/31/2299" ID="txtCompTime" runat="server" Width="65px"  TimePopupButton-Visible="false"/></td>
<td>Total work time (calculated): <br />&nbsp;</td>
</tr>
</table>
<table>
<tr>
<td>Bad Pallets: <br /><telerik:RadNumericTextBox ID="txtBadPallets" Width="25px" NumberFormat-DecimalDigits="0" runat="server" /></td>
<td>Weight: <br /><telerik:RadNumericTextBox ID="txtWeight" Width="45px" runat="server"  NumberFormat-DecimalDigits="0"/></td>
<td>Restacks: <br /><telerik:RadNumericTextBox ID="txtRestacks" Width="25px" runat="server" NumberFormat-DecimalDigits="0" /></td>
<td>Total items: <br /><telerik:RadNumericTextBox ID="txtTotalItems" Width="45px" runat="server" NumberFormat-DecimalDigits="0" /></td>
<td>Load Type: <br /><telerik:RadComboBox ID="txtLoadType" Width="100px" runat="server" AllowCustomText="true" EmptyMessage="Select ..." /></td>
</tr>
</table>
<hr />
<table>
    <tr>
<td>Check #: <telerik:RadTextBox ID="txtCheckNumber" runat="server" /></td>
<td>Amount:
<br /> 
<telerik:RadNumericTextBox ID="txxtAmount" Type="Currency" EmptyMessage="$" width="55px" runat="server" /><br />
    Split Amount:
<br /> 
<telerik:RadNumericTextBox ID="txtSplitAmount" Type="Currency" EmptyMessage="$" width="55px" runat="server" /><br />
</td>

    </tr>
    <tr>
<td colspan="2">BOL/PRO/SEAL #: <telerik:RadTextBox ID="txtBOL" runat="server" /></td>
</tr>
<tr>
<td colspan="2">
Comments: <br /><telerik:RadTextBox TextMode="MultiLine" Width="400px" Rows="5" ID="txtComments" runat="server" />
</td>
</tr>
</table>
</div>
<table width="95%" align="center">
    <tr>
        <td valign="top" style="padding-top:9px;">
            <asp:Button ID="btnSaveChanges" Text="Save Changes" CausesValidation="true" runat="server" />
<br /><asp:Label ID="lblChangesSaved" runat="server" Text="Saved" style="font-size:10px;visibility:hidden;color:Blue;" />
        </td>
        <td valign="top" class="lilBlue">
            <span onmouseover="this.style.pointer='pointer';"><asp:Label ID="lblEditLoadImages" runat="server" /></span> 
        </td>
        <td align="right" valign="top" style="padding-top:9px;">
            <asp:Button ID="btnCancel" runat="server" Text="Return to View Mode" />
        </td>
    </tr>
</table> 
<asp:Label ID="vid" runat="server" style="visibility:hidden;" />
</asp:Panel>

<%-- *******************END Right Side WO Form **********************--%>
        </td>

    </tr>
</table>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" VisibleStatusbar="false" >
<Windows>
<telerik:RadWindow ID="wCarrier" Height="375px" Width="260px"  
    ShowContentDuringLoad="false"   runat="server" Modal="True" 
    Title="Select Carrier"
    OffsetElementID="<%=lblCarrierNamev.ClientID%>" 
    Left="400"
    Top="-100"
    OnClientClose = "OnClientCloseCarrier"
    Behaviors="Move, Resize, Close" />
<telerik:RadWindow ID="wVendor" Height="500px" Width="410px"  
    ShowContentDuringLoad="true"  runat="server" Modal="True" 
    Title="Select Vendor"
    OffsetElementID="<%=lblCarrierNamev.ClientID%>" 
    Left="400"
    Top="-100"
    OnClientClose = "OnClientCloseVendor"
    Behaviors="Move, Close" />
<telerik:RadWindow ID="wUnloader" Height="260px" Width="410px"  
    ShowContentDuringLoad="true"  runat="server" Modal="True" 
    Title="Select Unloaders"
    OffsetElementID="<%=lblCarrierNamev.ClientID%>" 
    Left="400"
    Top="-100"
    OnClientClose = "OnClientCloseUnloader"
    Behaviors="Move, Close" />
<telerik:RadWindow ID="wLoadImages" Height="560px" Width="875px"  
    ShowContentDuringLoad="false"  runat="server" Modal="True" 
    Title="SEU - Load Image Manager" OnClientClose="closeit"
    Behaviors="Move, Resize, Close" />
<telerik:RadWindow ID="winEmailPics" Height="560px" Width="975px"  
    ShowContentDuringLoad="true"  runat="server" Modal="True" 
    Title="eMail Freight Issue Pictures"
        OnClientClose = "OnClientCloseUnloader"
    Behaviors="Move, Resize, Close" />

</Windows>
    </telerik:RadWindowManager>

<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
<script type="text/javascript">

    function openPicEmailer(arg) {
        //        document.getElementById("<%= btnSaveChanges.ClientID %>").disabled = true;
        var oManager = GetRadWindowManager();
        var loca = "../admin/eMailPictures.aspx?locaid=" + arg;
        oManager.open(loca, "winEmailPics");
    }

    function openLoadImages(arg) {
        //        document.getElementById("<%= btnSaveChanges.ClientID %>").disabled = true;
        var oManager = GetRadWindowManager();
        var loca = "../Upload/Async/Imageuploader/LoadImages.aspx?woid=" + arg;
        oManager.open(loca, "wLoadImages");
    }

    function openQuikAddLoadImages(arg) {
//        document.getElementById("< %= btnSaveChanges.ClientID %>").disabled = true;
        var oManager = GetRadWindowManager();
        var loca = "../Upload/Async/Imageuploader/QuikAddLoadImages.aspx?woid=" + arg;
        oManager.open(loca, "wLoadImages");
    }


    function openCarrier() {
//        document.getElementById("< %= btnSaveChanges.ClientID %>").disabled = true;
        var oManager = GetRadWindowManager();
        var loca = "selectCarrier.aspx"
        oManager.open(loca, "wCarrier");
   
    }
    function OnClientCloseCarrier(sender, args) {
        if (args.get_argument() != null) {
            var arg = args.get_argument();
            var CarrierName = document.getElementById("<%= lblCarrierNamev.ClientID %>")
            CarrierName.innerText = arg.split(":", 2)[0];
            CarrierName.style.fontSize = '12px';
            CarrierName.style.color = 'blue';
            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
            ajaxManager.ajaxRequest("Carrier:" + arg);
            //                oWnd.close();
        }else{
            document.getElementById("<%= btnSaveChanges.ClientID %>").disabled = false;

        }

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
            VendorName.style.color = 'blue';
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
    
//    function OnClientDropDownClosed(sender, args) {
//        sender.clearItems();
//        //        alert(sender);
//        if (args.get_domEvent().stopPropagation)
//            args.get_domEvent().stopPropagation();
//    }

    function btnSaveClick() {
        var SavedBtn = document.getElementById("<%= lblChangesSaved.ClientID %>");
        SavedBtn.style.visibility = 'visible';

        var VendorName = document.getElementById("<%= txtVendorName.ClientID %>");
        VendorName.style.color = 'black';
        var CarrierName = document.getElementById("<%= lblCarrierNamev.ClientID %>");
        CarrierName.style.color = 'black';
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

           </script>



</telerik:RadCodeBlock>

    </div>
    </form>
</body>
</html>
