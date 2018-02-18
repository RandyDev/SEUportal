<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BackhaulInbound.aspx.vb" Inherits="DiversifiedLogistics.BackhaulInbound" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style type="text/css">
    td{font-family:Arial; font-size:11px;}
    .MyImageButton
        {
           cursor: hand;
        }

</style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblVendorName" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lbAvailableUnloaders"  />
                    <telerik:AjaxUpdatedControl ControlID="lbUnloaderList"  />

                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cbLocations">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlDynamicStuff" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblChangeSelectCarrier" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="tpDockTime">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tpStartTime" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="tpEndTime" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="tpStartTime">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tpEndTime" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblRowCount" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonAdd">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblRowCount" />
                    <telerik:AjaxUpdatedControl ControlID="btnNewBatch" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDynamicStuff" />
                    <telerik:AjaxUpdatedControl ControlID="pnlStaticStuff" />

                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />
    <div>
<table cellpadding="0" cellspacing="0"><tr><td valign="top" width="660">  
<asp:Panel ID="pnlStaticStuff" runat="server">
<table cellpadding="2" cellspacing="2">
    <tr>
        <td>Date:<br />
           <telerik:RadDatePicker ID="LogDate" Width="100px" runat="server" />
        </td>
        <td>Location:<br />
            <telerik:RadComboBox ID="cbLocations" Filter="Contains" Width="110px" runat="server" />
        </td>
        <td>Department:<br />
            <telerik:RadComboBox ID="cbDepartment" runat="server" Width="120px" AllowCustomText="true" Filter="Contains" EmptyMessage="Select Department" />
            
        </td>
        <td>Load Type:<br />
            <telerik:RadComboBox ID="cbLoadType" runat="server" Width="120px" AllowCustomText="true" Filter="Contains" EmptyMessage="Select Load Type" />
        </td>
        <td>Description:<br />
            <telerik:RadComboBox ID="cbLoadDescription" runat="server" AllowCustomText="true" Filter="Contains" EmptyMessage="Select Load Description" />
        </td>
    </tr>
</table>
<table cellpadding="2" cellspacing="2">
    <tr>
        <td>Door #:<br />
            <telerik:RadTextBox ID="txtDoorNumber" Width="40px" runat="server" />
        </td>
        <td valign="top">Carrier: &nbsp;<span id="spSelectCarrier" onclick="openCarrier();" onmouseover="this.style.cursor='pointer';" style="color:blue;font-size:11px;" runat="server">Select</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
    <asp:Label CssClass="data" ID="lblCarrierNamev" runat="server" /><asp:TextBox ID="txtCarrierIDv" runat="server" Visible="false" /><asp:TextBox ID="htxtCarrierName" runat="server" Visible="false" />
        </td>
        <td>Truck #:<br /> 
            <telerik:RadTextBox ID="txtTruckNumber" Width="75px" runat="server" EmptyMessage="DROP" />
        </td>
        <td>Trailer #:<br />
            <telerik:RadTextBox ID="txtTrailerNumber" Width="75px" runat="server" />
        </td>
        <td>Dock Time:<br />
            <telerik:RadTimePicker ID="tpDockTime" Width="55px" runat="server" TimePopupButton-Visible="false" />
        </td>
        <td>Start Time:<br />
        <telerik:RadTimePicker ID="tpStartTime" Width="55px" runat="server" TimePopupButton-Visible="false"  />
        </td>
        <td>End Time:<br />
        <telerik:RadTimePicker ID="tpEndTime" Width="55px" runat="server" TimePopupButton-Visible="false" />
        </td>
    </tr>
</table>
</asp:Panel>
<br />
<hr />
<br />
<asp:Panel ID="pnlDynamicStuff" runat="server" Visible="false" DefaultButton="ButtonAdd">

            <table cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td valign="top">
                        Purchase Order:<br />
                        <telerik:RadTextBox ID="txtPONumber" Width="80px" runat="server" />
                    </td>
                    <td><asp:Label ID="lblVendorNumber" runat="server" Text="Vendor Number" />:<br />
                        <telerik:RadTextBox ID="txtVendorNumber" Width="80px" runat="server" />
                    </td>
                    <td valign="top">Pallets&nbsp;Unld:<br />
                        <telerik:RadNumericTextBox ID="txtPalletsUnloaded" Width="45px" NumberFormat-DecimalDigits="0" runat="server" />
                    </td>
                    <td valign="top">Weight:<br />
                        <telerik:RadNumericTextBox ID="txtWeight" Width="45px" NumberFormat-DecimalDigits="0" runat="server" />
                    </td>
                    <td valign="top">Pallets&nbsp;Rec.<br />
                        <telerik:RadNumericTextBox ID="txtPalletsReceived" Width="45px" NumberFormat-DecimalDigits="0" runat="server" />
                    </td>
                    <td valign="top">Pieces:<br />
                        <telerik:RadNumericTextBox ID="txtPieces" ClientEvents-OnValueChanged="gimmeTotal" Width="45px" NumberFormat-DecimalDigits="0" runat="server" />
                    </td>
                    <td valign="top">Restacks:<br />
                        <telerik:RadNumericTextBox ID="txtRestacks" ClientEvents-OnValueChanged="gimmeTotal" Width="45px" NumberFormat-DecimalDigits="0" runat="server" />
                    </td>
                    <td valign="top">Bad&nbsp;Pallets:<br />
                        <telerik:RadNumericTextBox ID="txtBadPallets" ClientEvents-OnValueChanged="gimmeTotal" Width="45px" NumberFormat-DecimalDigits="0" runat="server" />
                    </td>
                    <td valign="top">Items:<br />
                        <telerik:RadNumericTextBox ID="txtNumberItems" Width="45px" NumberFormat-DecimalDigits="0" runat="server" />
                    </td>
                    <td valign="top">*Amount: <br />
                        <telerik:RadNumericTextBox TabIndex="900" ID="txtAmount" Width="45px" NumberFormat-DecimalDigits="2" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="padding-top:0px">
                        <asp:Label ID="lblVendorName" runat="server" />
                    </td>
                    <td colspan="2" align="right" style="padding-right: 12px;">
                        <asp:CheckBox ID="cbIsRounded" AutoPostBack="false" TabIndex="1000" runat="server" text="*Round Up?" />
                    </td>
                    <td>
                        &cent;<telerik:RadNumericTextBox ID="txtPiecePrice" TabIndex="1000" Width="45px" ClientEvents-OnValueChanged="gimmeTotal" NumberFormat-DecimalDigits="1" Value="3" MinValue="3" MaxValue="8" IncrementSettings-Step=".5" ShowSpinButtons="true" runat='server' />
                    </td>
                    <td>
                        $<telerik:RadNumericTextBox ID="txtRestackPrice" TabIndex="1000" Width="45px" ClientEvents-OnValueChanged="gimmeTotal" NumberFormat-DecimalDigits="2" Value="3" MinValue="3" MaxValue="5" IncrementSettings-Step=".5" ShowSpinButtons="true" runat='server' />
                    </td>
                    <td>
                        $<telerik:RadNumericTextBox ID="txtBadPalletPrice" TabIndex="1000" Width="45px" ClientEvents-OnValueChanged="gimmeTotal" NumberFormat-DecimalDigits="2" Value="3" MinValue="3" MaxValue="5" IncrementSettings-Step=".5" ShowSpinButtons="true" runat='server' />
                    </td>
                    <td colspan="2" align="right">
                        <asp:Button ID="ButtonAdd" CausesValidation="true" TabIndex="50" ValidationGroup="StaticGroup" runat='server' Text="Add Record" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" valign="top">Comments:<br />
                        <telerik:RadTextBox ID="txtComments" TextMode="MultiLine" Rows="5" Width="280px" runat="server" />
                    </td>
                    <td colspan="6">
    <table><tr>
        <td valign="top">Unloaders Assigned:<br />
           <telerik:RadListBox ID="lbUnloaderList" runat="server" AllowReorder="true" Width="165px" Height="165px" 
                ButtonSettings-Position="Left" EmptyMessage="Select Vendor to enable unloader selection" />
          
        </td>
        <td valign="top">Unloaders this location:<br />
            	<table cellpadding="0" cellspacing="0">
                	<tr>
                    	<td> 
            <telerik:RadListBox ID="lbAvailableUnloaders" runat="server" Width="165px" Height="165px" 
                AllowTransfer="true" TransferToID="lbUnloaderList" AllowTransferDuplicates="false" AllowTransferOnDoubleClick="true"
                EnableDragAndDrop="true" SelectionMode="Multiple" ToolTip="Double-Click Name to Transfer" ButtonSettings-Position="Left" EmptyMessage="You've used everybody up, call in the reserves!"   />
                
<%--                 DataKeyField="ID"
                 
                 AllowDelete="false" ButtonSettings-ShowTransfer="True" 
                ButtonSettings-ShowReorder="False" AllowReorder="false"
                ButtonSettings-ShowDelete="false" 
           	    ButtonSettings-Position="Left" 
                TransferMode="Move"   
                ButtonSettings-ShowTransferAll="False" />
--%>                        </td>
                    </tr>

                </table>

        </td>
    </tr></table>
                    </td>
                </tr>
            </table>

            <asp:ValidationSummary ValidationGroup="StaticGroup" ID="StaticValidationSummary" runat="server" />
            <asp:ValidationSummary ValidationGroup="DynamicGroup" ID="DynamicValidationSummary" runat="server" />

</asp:Panel>

</td>
<td valign="top">
<telerik:RadGrid ID="RadGrid1" runat="server" GridLines="None">
<ClientSettings EnableRowHoverStyle="true"></ClientSettings>
<MasterTableView AutoGenerateColumns="false" DataKeyNames="ID" Height="25px" >
<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
<NoRecordsTemplate>No records to display</NoRecordsTemplate>
<RowIndicatorColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>
    <Columns>
        <telerik:GridBoundColumn DataField="ID" DataType="System.Guid" HeaderText="ID" 
            ReadOnly="True" SortExpression="ID" UniqueName="ID" Visible="false">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="PurchaseOrder" HeaderText="PO #" 
            SortExpression="PurchaseOrder" UniqueName="PurchaseOrder">
            <HeaderStyle Width="50px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="VendorNumber" HeaderText="Vendor&nbsp;#" 
            SortExpression="VendorNumber" UniqueName="VendorNumber">
            <HeaderStyle Width="50px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="PalletsUnloaded" DataType="System.Int64" 
            HeaderText="Pallets Unld" SortExpression="PalletsUnloaded" 
            UniqueName="PalletsUnloaded">
            <HeaderStyle Width="40px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Weight" DataType="System.Int64" 
            HeaderText="Weight" SortExpression="Weight" UniqueName="Weight">
            <HeaderStyle Width="40px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="PalletsReceived" DataType="System.Int64" 
            HeaderText="Pallets Rec" SortExpression="PalletsReceived" UniqueName="PalletsReceived">
            <HeaderStyle Width="40px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Pieces" DataType="System.Int64" 
            HeaderText="Pieces" SortExpression="Pieces" UniqueName="Pieces">
            <HeaderStyle Width="40px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Restacks" DataType="System.Int64" 
            HeaderText="Restacks" SortExpression="Restacks" UniqueName="Restacks">
            <HeaderStyle Width="40px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="BadPallets" DataType="System.Int64" 
            HeaderText="Bad Pallets" SortExpression="BadPallets" UniqueName="BadPallets">
            <HeaderStyle Width="40px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="NumberOfItems" DataType="System.Int64" 
            HeaderText="Items" SortExpression="NumberOfItems" UniqueName="NumberOfItems">
            <HeaderStyle Width="40px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Amount" DataType="System.Double" 
            HeaderText="Amount" SortExpression="Amount" UniqueName="Amount">
            <HeaderStyle Width="40px" />
        </telerik:GridBoundColumn>
        <telerik:GridButtonColumn ImageUrl="~/images/redX.gif" ConfirmDialogWidth="330px"  ConfirmDialogHeight="115px" ConfirmTitle="Warning, Will Robinson, Warning!" ConfirmText="This PO is about to enter the Twilight Zone!<br />Are you sure?" ConfirmDialogType="RadWindow"
            ButtonType="ImageButton" CommandName="Delete" Text="Delete"
            UniqueName="DeleteColumn">
            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
        </telerik:GridButtonColumn>
    </Columns>
</MasterTableView>
        </telerik:RadGrid>
<table width="100%"><tr>
<td style="padding-left:15px;"> <asp:Label ID="lblRowCount" runat="server" /></td>
<td align="right" style="padding-right:15px;"><asp:Button ID="btnNewBatch" Text="Clear / Start New Batch" runat="server" Visible="false" /></td>
</tr></table>

</td>


</tr></table>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" VisibleStatusbar="false" >
<Windows>
<telerik:RadWindow ID="wCarrier" Height="375px" Width="260px"  
    ShowContentDuringLoad="true"  runat="server" Modal="True" 
    Title="Select Carrier"
    OffsetElementID="<%=lblCarrierNamev.ClientID%>" 
    Left="400"
    Top="-100"
    NavigateUrl="selectCarrier.aspx" 
    OnClientClose = "OnClientCloseCarrier"
    Behaviors="Move, Close" />
<telerik:RadWindow ID="wVendor" Height="500px" Width="410px"  
    ShowContentDuringLoad="true"  runat="server" Modal="True" 
    Title="Select Vendor"
    OffsetElementID="<%=lblCarrierNamev.ClientID%>" 
    Left="400"
    Top="-100"
    OnClientClose = "OnClientCloseVendor"
    Behaviors="Move, Close" />
</Windows>
    </telerik:RadWindowManager>
</div>
<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
<script type="text/javascript">
    function openCarrier() {
        var locaCombo = $find("<%= cbLocations.ClientID %>");
        if (locaCombo.get_text() == "Select Location" ) {
            alert("Please select a location");
            return false;
        } else {
            var oManager = GetRadWindowManager();
            oManager.open(null, "wCarrier");
        }
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
        }
    }
    // ************************** Edit Vendor Script ************************
    function openVendor() {
        var oManager = GetRadWindowManager();
        var loca = "addVendor.aspx";
        oManager.open(loca, "wVendor");
    }

    function OnClientCloseVendor(sender, args) {
        if (args.get_argument() != null) {
            var arg = args.get_argument();
            var sarg = arg.split(":");
            var VendorNumber = $find("<%= txtVendorNumber.ClientID %>");
            var VendorName = document.getElementById("<%= lblVendorName.ClientID %>");
            VendorNumber.set_value(sarg[2]);
            VendorName.innerText = sarg[2] + "-" + sarg[3];
            VendorName.style.color = 'blue';
            VendorName.style.fontSize = '12px';
            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
            ajaxManager.ajaxRequest(arg);
        }
    }

    function decOnly(i) {
        var t = i.value;
        if (t.length > 0) {
            t = t.replace(/[^\da-zA-Z]+/g, '');
            i.value = t;
        }
        var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
        ajaxManager.ajaxRequest("VendorLookup:" + t);
    }



    function gimmeTotal(sender, eventArgs) {
        var pcsinput = $find("<%=txtPiecePrice.ClientID %>");
        var txtpcs = $find("<%=txtPieces.ClientID %>");
        var resinput = $find("<%=txtRestackPrice.ClientID %>");
        var txtres = $find("<%=txtRestacks.ClientID %>");
        var bdpinput = $find("<%=txtBadPalletPrice.ClientID %>");
        var txtbdp = $find("<%=txtBadPallets.ClientID %>");
        var amountinput = $find("<%=txtAmount.ClientID %>");

        var pcs = (pcsinput.get_value() * txtpcs.get_value()) / 100;
        var res = resinput.get_value() * txtres.get_value();
        var bdp = bdpinput.get_value() * txtbdp.get_value();
        var amount = pcs + res + bdp;

        var chkBx = document.getElementById("cbIsRounded");
        if (chkBx.checked) {
            if (amount % 1 > 0) {
                var mod = amount % 1;
                amount = amount + 1 - mod;
            }
        }
        amountinput.set_value(amount);
    }

    </script>
</telerik:RadCodeBlock>

    </form>
</body>
</html>
