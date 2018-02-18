<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ScheduledLoadImporterSetup.aspx.vb" Inherits="DiversifiedLogistics.ScheduledLoadImporterSetup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="../styles/StyleSheet.css" rel="stylesheet" type="text/css" />

<style type= "text/css">
.b{
border:1px solid black; 
    }


    .auto-style1 {
        width: 71px;
    }


    </style>
    <script type="text/javascript">
        function deconly(i) {
            alert("oophf");
            var t = i.value;
            if (t.length > 0) {
                //   t = t.replace(/[^\d]+/g, '');
                t = t.replace(/[^\da-zA-Z]+/g, '');
                i.value = t;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="cbLocations">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cbImportName" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="divsetup" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cbImportName">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="btnRemoveConfig" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="btnNEWConfig" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="divsetup" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnRemoveConfig">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cbImportName" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="divsetup" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnNEWConfig">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cbImportName" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="divsetup" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rb1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divsetup" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rb2">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divsetup" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rb3">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divsetup" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rblistHasDate">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="chkLogDate" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="txtLogDateColumn" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSave">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cbImportName" />
                        <telerik:AjaxUpdatedControl ControlID="btnRemoveConfig" />
                        <telerik:AjaxUpdatedControl ControlID="btnNEWConfig" />
                        <telerik:AjaxUpdatedControl ControlID="btnSave" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel Transparency="50" ID="RadAjaxLoadingPanel1"  runat="server">
        <asp:Image id="Image1" runat="server" Width="110" Height="21" ImageUrl="~/images/forkliftani.gif"></asp:Image>
    </telerik:RadAjaxLoadingPanel><table style="width:100%;">
<tr><td style="text-align:right;padding-right:45px"><span onmouseover="this.style.cursor='help'"><asp:Label ID="lblhelp" ForeColor="red" Font-Bold="true" Text="READ ME FIRST" runat="server" /></span></td></tr>
</table>
<table>
    <tr>
        <td class="b"><asp:Label id="Label1" Text="Select Location" runat="server" /><br />
            <telerik:RadComboBox ID="cbLocations" EmptyMessage="Select Location" Width="150px" Filter="Contains" runat="server" /> 
        </td>
        <td class="b" style="text-align:center;font-weight:bold;font-size:15px;">
            SEU Universal Scheduled Load Importer SETUP
        </td>
    </tr>
    <tr>
        <td class="b" style="vertical-align:top;">
            <telerik:RadComboBox ID="cbImportName" EmptyMessage="^^Select Location FIRST" Width="261px" AutoPostBack="true" ToolTip="Providing a name for this configuration, each site can have multiple configurations." runat="server" />
            <br /> ^ Saved configs will be in this dropdown^<br />
<table style="width:100%;"><tr><td style="border:none;"><asp:Button ID="btnRemoveConfig" Text="Remove Config" runat="server" /></td><td style="text-align:right; border:none;"><asp:Button ID="btnNEWConfig" Visible="false" Text="Create New Config" runat="server" /></td></tr></table>

        </td>
        <td class="b">
            <div style="border:1px solid black;" id="divsetup" runat="server">
<table cellspacing="0" cellpadding="0">
    <tr>
        <td class="b">
            <asp:Label id="lblImportName" Text="Config Name" tooltip= "Providing a Name allows each location to have multiple configurations." runat="server" />
        </td>
            <td class="b" colspan="3">
            <telerik:Radtextbox ID="txtImportName"  Columns="25" EmptyMessage="Enter Name" ToolTip="Providing a Name allows each location to have multiple configurations."  runat="server" Width="255px" />
        </td>
    </tr>
    <tr>
        <td class="b"> Import Type</td>
         <td class="b"><asp:RadioButton ID="rb1" Text="INS" GroupName="importtype" AutoPostBack="true" runat="server" /></td>                      
         <td class="b"><asp:RadioButton ID="rb2" Text="UPD" GroupName="importtype" AutoPostBack="true" runat="server" /></td>                      
         <td class="b"><asp:RadioButton ID="rb3" Text="INS/UPD" GroupName="importtype" AutoPostBack="true" runat="server" /></td>                      
    </tr>
    <tr>
        <td class="b">First Row </td><td class="b" colspan="3"><telerik:RadNumericTextBox ID="numFirstRow" NumberFormat-DecimalDigits="0" MinValue="1" Value="1"  runat="server" Width="35px" /></td>
    </tr>
    <tr>
        <td class="b">Has Date Column? </td><td class="b" colspan="3"> <asp:RadioButtonList AutoPostBack="true" RepeatDirection="Horizontal" ID="rblistHasDate" runat="server">
            <asp:ListItem text="YES" Value="1"></asp:ListItem><asp:ListItem text="NO" Value="0" Selected="True"></asp:ListItem></asp:RadioButtonList></td>
    </tr>
</table>
<table>
<tr style="font-weight:bold;"> <td class="b"> Field Name</td><td class="b">UpDate?</td><td class="b">Column Letter</td><td class="b">Default Value</td></tr>
<tr><td class="b">LogDate</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkLogDate" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtLogDateColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b" style="text-align:center;">----</td></tr>
<tr><td class="b">Status</td><td class="b" style="text-align:center;">----</td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtStatusColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtStatusDefault" Width="125px" EmptyMessage="128" runat="server" /></td></tr>   
<tr><td class="b">LoadNumber</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkLoadNumber" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtLoadNumberColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtLoadNumberDefault" EmptyMessage="Calculated" Width="125px" runat="server" /></td></tr>
<tr><td class="b">Department</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkDepartment" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtDepartmentIDColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:RadCombobox ID="cbDepartmentIDDefault" AllowCustomText="true" EmptyMessage="Null OR Select" Width="150px" runat="server" /></td></tr>
<tr><td class="b">LoadType</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkLoadType" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtLoadTypeIDColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:RadCombobox ID="cbLoadTypeIDDefault" AllowCustomText="true" EmptyMessage="Null OR Select" Width="150px" runat="server" /></td></tr>
<tr><td class="b">Vendor</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkVendor" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtVendorColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtVendorDefault" EmptyMessage="Null" Width="125px" runat="server" /></td></tr>
<tr><td class="b">VendorNumber</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkVendorNumber" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtVendorNumberColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtVendorNumberDefault" EmptyMessage="Null" Width="125px" runat="server" /></td></tr>
<%--<tr><td class="b">CustomerID</td><td class="b" style="text-align:center;">----</td><td class="b" style="text-align:center;">n/a</td><td class="b"><telerik:Radtextbox ID="txtCustomerIDDefault" EmptyMessage="Calculated" Width="125px" runat="server" /></td></tr>--%>
<tr><td class="b">PurchaseOrder</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkPurchaseOrder" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtPurchaseOrderColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtPurchaseOrderDefault" EmptyMessage="Null" Width="125px" runat="server" /></td></tr>
<tr><td class="b">Amount</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkAmount" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtAmountColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtAmountDefault" Width="125px" EmptyMessage="-1" runat="server" /></td></tr>
<tr><td class="b">CheckNumber</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkCheckNumber" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtCheckNumberColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtCheckNumberDefault" Width="125px" EmptyMessage="Null" runat="server" /></td></tr>
<tr><td class="b">SplitPaymentAmount</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkSplitPaymentAmount" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtSplitPaymentAmountColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtSplitPaymentAmountDefault" EmptyMessage="0" Width="125px" runat="server" /></td></tr>
<tr><td class="b">ReceiptNumber</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkReceiptNumber" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtReceiptNumberColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtReceiptNumberDefault" Width="125px" EmptyMessage="Calculated" runat="server" /></td></tr>
<tr><td class="b">LoadDescription</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkLoadDescription" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtLoadDescriptionIDColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:RadCombobox ID="cbLoadDescriptionID" AllowCustomText="true" EmptyMessage="Null OR Select"  Width="150px" runat="server" /></td></tr>
<tr><td class="b">Carrier</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkCarrier" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtCarrierIDColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b">
    <telerik:RadCombobox RenderMode="Lightweight" ID="cbCarrierOD" runat="server" Width="200px" 
                EmptyMessage="Null OR Select" DataSourceID="SqlDataSource1" DataTextField="Name"
                DataValueField="ID" EnableAutomaticLoadOnDemand="True" ItemsPerRequest="10"
                ShowMoreResultsBox="True" EnableVirtualScrolling="True" />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:rtdsConnectionString %>" SelectCommand="SELECT [Name], [ID] FROM [Carrier] ORDER By [Name]"></asp:SqlDataSource>
    </td>
</tr>
<tr><td class="b">TruckNumber</td><td style="text-align:center;"><asp:CheckBox ID="chkTruckNumber" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtTruckNumberColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtTruckNumberDefault" EmptyMessage="Null" Width="125px" runat="server" /></td></tr>
<tr><td class="b">TrailerNumber</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkTrailerNumber" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtTrailerNumberColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtTrailerNumberDefault" EmptyMessage="Null" Width="125px" runat="server" /></td></tr>
<tr><td class="b">AppointmentTime</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkAppointmentTime" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtAppointmentTimeColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtAppointmentTimeDefault" EmptyMessage="1/1/1900 12:00 AM" Width="125px" runat="server" />&nbsp;hh:mm</td></tr>
<tr><td class="b">GateTime</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkGateTime" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtGateTimeColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtGateTimeDefault" EmptyMessage="1/1/1900 12:00 AM" Width="125px" runat="server" />&nbsp;hh:mm</td></tr>
<tr><td class="b">DockTime</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkDockTime" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtDockTimeColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtDockTimeDefault" EmptyMessage="1/1/1900 12:00 AM" Width="125px" runat="server" />&nbsp;hh:mm</td></tr>
<tr><td class="b">StartTime</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkStartTime" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtStartTimeColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtStartTimeDefault" EmptyMessage="1/1/1900 12:00 AM" Width="125px" runat="server" />&nbsp;hh:mm</td></tr>
<tr><td class="b">CompTime</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkCompTime" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtCompTimeColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtCompTimeDefault"  EmptyMessage="1/1/1900 12:00 AM" Width="125px" runat="server" />&nbsp;hh:mm</td></tr>
<tr><td class="b">PalletsUnloaded</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkPalletsUnloaded" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtPalletsUnloadedColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtPalletsUnloadedDefault" EmptyMessage="-1" Width="125px" runat="server" /></td></tr>
<tr><td class="b">DoorNumber</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkDoorNumber" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtDoorNumberColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtDoorNumberDefault" EmptyMessage="Null" Width="125px" runat="server" /></td></tr>
<tr><td class="b">Pieces</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkPieces" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtPiecesColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtPiecesDefault" EmptyMessage="-1" Width="125px" runat="server" /></td></tr>
<tr><td class="b">Weight</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkWeight" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtWeightColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtWeightDefault" EmptyMessage="-1" Width="125px" runat="server" /></td></tr>
<tr><td class="b">Restacks</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkRestacks" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtRestacksColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtRestacksDefault" EmptyMessage="-1" Width="125px" runat="server" /></td></tr>
<tr><td class="b">PalletsReceived</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkPalletsReceived" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtPalletsReceivedColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtPalletsReceivedDefault" EmptyMessage="-1" Width="125px" runat="server" /></td></tr>
<tr><td class="b">BadPallets</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkBadPallets" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtBadPalletsColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtBadPalletsDefault" EmptyMessage="-1" Width="125px" runat="server" /></td></tr>
<tr><td class="b">NumberOfItems</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkNumberOfItems" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtNumberOfItemsColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtNumberOfItemsDefault" EmptyMessage="-1" Width="125px" runat="server" /></td></tr>
<tr><td class="b">BOL</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkBOL" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtBOLColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtBOLDefault" EmptyMessage="Null" Width="125px" runat="server" /></td></tr>
<tr><td class="b">Comments</td><td class="b" style="text-align:center;"><asp:CheckBox ID="chkComments" Text="" runat="server" /></td><td class="b" style="text-align:center;"><telerik:Radtextbox ID="txtCommentsColumn" MaxLength="1" ClientEvents-OnKeyPress="OnKeyPress" Width="25px" runat="server" /></td><td class="b"><telerik:Radtextbox ID="txtCommentsDefault" EmptyMessage="Null" Width="125px" runat="server" /></td></tr>
<tr><td class="b" colspan="4" style="text-align:left; border:none;"><telerik:RadButton id="btnSave" Text="Save Configuration" runat="server" /></td></tr>
</table>
            </div>
        </td>
    </tr>

</table>

        <telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="lblhelp" Width="500px" RelativeTo="Element"
    ShowEvent="OnClick" Position="BottomLeft" OffsetX="50" OffsetY="-123"  HideEvent="ManualClose"
     Animation="Resize" EnableShadow="true">

<table style="width:500px;">
    <tr>
        <td><span class="ttHeader">Universal Scheduled Load Importer SETUP Help</span></td>
    </tr>
    <tr>
        <td class="ttBody"> 
            The Universal Scheduled Load Importer will allow you to import, (<em>once defined</em>), virtually any OPENxml compliant spreadsheet into the RTDS workorder table.
            The software will do this irrespective of the uploaded layout.  The only requirement is that the data rows be contained in a contiguous table anywhere within a given worksheet.
            You will use this page to define a configuration for a given uploaded spreadsheet.  This is a ONE time setup for standardized spreadsheets.  
            This control will allow you to Create, Update and Remove named configurations
        </td>
    </tr>

    <tr>
        <td class="ttTitle" style="text-align:center;">&nbsp;<span style="color:firebrick;"> This is a highly versitile and DANGEROUS piece of software!&nbsp;
            <br />
            If you have the slightest doubt, STOP and seek guidance from the IT Director.</span></td>
    </tr>
    <tr>
        <td class="ttBody">
            Start by selecting a Location from the Location dropdown box.
            <br /> If there are no defined configurations for the selected location the configurator will open to the just below the page title.
            <br />Otherwise you may select from the dropdown list of Saved Cofigs (<em>for review or edit</em>), or choose to 'Create New Config' by clicking the button
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Config Name, Import Type, First Row, Has Date</td>
    </tr>
    <tr>
        <td class="ttBody"> 
            When the configurator opens you will see Config Name, Import Type, First Row and Has Date at the top<br />
            <table>
                <tr>
                    <td style="vertical-align:top;" class="auto-style1"><strong>Config Name</strong></td>
                    <td style="padding-left:8px;">
                        Provide a name for this configuration. Each location may have multiple configurations.
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align:top;" class="auto-style1"><strong>Import Type</strong></td>
                    <td style="padding-left:8px">
                        <table>
                            <tr>
                                <td style="vertical-align:top;">
                                    <strong>INS</strong>
                                </td>
                                <td style="padding-left:8px;">
                                    search database and insert workorders not found<br /><sup style="color:red;font-weight:bold;">1</sup>appends " - INSERT" to the Name you provide
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align:top;">
                                    <strong>UPD</strong>
                                </td>
                                <td style="padding-left:8px;">
                                    search database and update found workorders
                                    <br /><sup style="color:red;font-weight:bold;">1</sup>appends " - UPDATE" to the Name you provide
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align:top;">
                                    <strong>INS/UPD </strong>
                                </td>
                                <td style="padding-left:8px;">
                                    search database and insert workorders not found. found workorders will be updated
                                    <br /><sup style="color:red;font-weight:bold;">1</sup>appends " - INSERT / UPDATE" to the Name you provide
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align:top;" class="auto-style1"><strong>First Row</strong></td>
                    <td style="padding-left:8px">
                        This is the first row of the data,<b> DO NOT INCLUDE header row</b>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align:top;">
                        <strong>Has Date</strong>
                    </td>
                    <td style="padding-left:8px">
                        If the imported sheet(s) have a defined date column, the uploader will NOT be prompted for a date.
                    </td>
                </tr>
                 <tr>
                    <td colspan="2" class="b">
                        <sup style="color:red;font-weight:bold;">1</sup>
                        appended text only appears in the Saved configs dropdown box
                        <br />&nbsp; &nbsp; <em>eg</em>: if you name a configuration &#39;Config One&#39; and select the INS radio button,
                        <br />&nbsp; &nbsp; it will appear in the Saved Configs dropdown list as &#39;Config One - INSERT&#39;
                        <br />&nbsp; &nbsp; DO NOT add these yourself, provide only the Cofig Name, the software will do the rest. 
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="ttTitle"> The configurator</td>
    </tr>
    <tr class="b">
        <td class="ttBody">
            <b><sup style="color:red;font-weight:bold;">2</sup>Field Name</b>: These are the fields in the RTDS workorder table.
        </td>
    </tr>
    <tr class="b">
        <td class="ttBody">
            <b>UpDate</b>: If the Import Type is UPD or INS/UPD then those checked rows will update a found workorder record.<br />
        </td>
    </tr>
    <tr class ="b">
        <td class="ttBody">
            <b>Column Letter</b>:  The spreadsheet column letter containing field data (takes precedence over Default Value)
        </td>
    </tr>
    <tr class="b">
        <td class="ttBody">
            <b>Default Value</b>:  IF column letter is blank, this value will be used.
        </td>
    </tr>
    <tr>
        <td class="ttBody">
           <sup style="color:red;font-weight:bold;">2</sup>The following workorder table data fields neither importable nor updateable.
             
<table>
    <tr>
        <td style="vertical-align:top;padding-left:14px;">
            <b>LocationID</b>: from Location Selector
            <br /><b>LogNumber</b>: obsolete
            <br /><b>CustomerID</b>: calculated
            <br /><b>IsCash</b>:  calculated
        </td>
        <td style="vertical-align:top;padding-left:14px;">
            <b>WorkOrderID</b>: calculated      
            <br /><b>ccFee</b>: n/a
            <br /><b>PaymentType</b>: calculated
            <br /><b>CreatedBy</b>: calculated
        </td>
    </tr></table>
        </td>
    </tr>
        </table>
            <br />
<center>To Close - Click the X in the black circle in upper right corner</center>
            </telerik:RadToolTip>
<script type="text/javascript">
    var textBox;
    var text = new String();
    function OnKeyPress(sender, args) {
        textBox = sender;
        text = args.get_keyCharacter();
        text = text.toUpperCase();
        setTimeout("textBox.set_value(text);", 100);
        return ((text >= 65 && text <= 90) || text == 8);
        return ((key >= 65 && key <= 90) || key == 8);
    }
</script>     </form>
</body>
</html>
