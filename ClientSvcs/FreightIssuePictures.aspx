<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FreightIssuePictures.aspx.vb" Inherits="DiversifiedLogistics.FreightIssuePictures" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .lilBlueButton{
            font-size:11px;
            font-weight:normal;
            color:Blue;
        }
.lilblue
{
    font-weight:normal;
    color:blue;
    font-size:11px;
    text-decoration:underline;
}
        .headTitle{
            font-size:24px;
            text-align:center;
            font-family:Arial;
            font-weight:bold;
        }
        .exportRow{   
            background-image:url(../images/header-colds.png);
            background-position:0 -304px; /* add 20px to #header.background-position */
            text-align:right;
            padding-top:4px;
        }
        body{ font-family:Arial;}
        .fieldTitle{font-size:12px;}
        .field{font-size:14px;font-weight:bold;}
    </style>
</head>
<body id="theBody" runat="server">
    <form id="form1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
            <script type="text/javascript">
                function mngRequestStarted(ajaxManager, eventArgs) {
                    if (eventArgs.get_eventTarget().indexOf("mngBtn") != -1)
                        eventArgs.set_enableAjax(false);
                }
                function pnlRequestStarted(ajaxPanel, eventArgs) {
                    if (eventArgs.get_eventTarget().indexOf("pnlBtn") != -1)
                        eventArgs.set_enableAjax(false);
                }

                function PrintSelected() {
                    var grid = $find("<%=RadGrid1.ClientID %>");
                    var MasterTable = grid.get_masterTableView();
                    var selectedRows = MasterTable.get_selectedItems();
                    var printSelected = "";
                    if (selectedRows.length == 0) { alert("No rows selected"); return false; }
                    if (selectedRows.length > 50) {alert("Maximum 50 loads at a time.\nYou have " + selectedRows.length + " selected.");return false;}
                    
                    for (var i = 0; i < selectedRows.length; i++) {
                        var row = selectedRows[i].getDataKeyValue("ID");
                        printSelected += row + ":";
                        //here cell.innerHTML holds the value of the cell 
                    }
                    openPDFwin(printSelected);

              
                 }
            </script>
        </telerik:RadCodeBlock>    
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cbLocations">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnShowRecords">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlDetail" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="lnkButton1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />

    <div>
    <table style="font-family:Arial; font-size:12px; font-weight:bold;">
    <tr>
        <td width="220" valign="middle">Location: <telerik:RadComboBox ID="cbLocations" runat="server" AllowCustomText="true" Filter="Contains" EmptyMessage="Select Location" />
        </td>
        <td width="215" valign="middle">Start Date: <telerik:RadDatePicker ID="dpStartDate" runat="server" ></telerik:RadDatePicker>
        </td>
        <td width="210" valign="middle">End Date: <telerik:RadDatePicker ID="dpEndDate" runat="server" ></telerik:RadDatePicker>
        </td>
        <td width="100">
            <asp:button ID="btnShowRecords" Text="Show Records" CommandName="Refresh" runat="server" />
        </td>
        <td class="lilBlueButton" style="padding-left:20px;">
            <asp:Linkbutton ID="lnkButton1" CssClass="lilblue" Text="View Photo Reports (selected loads)" OnClientClick="PrintSelected();return false;" CausesValidation="false" Visible="false" runat="server" />
        </td>
        <td style="padding-left:50px;" title="Click me!">
        <span onmouseover="this.style.cursor='help';"><asp:Label runat="server" ID="lblHelp" CssClass="lilblue" Text="help with this page" /></span>
        </td>

    </tr>
</table>
<div>
</div>
<table><tr>
<td>
    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" Width="776px" 
        CellSpacing="0" GridLines="None" AllowMultiRowSelection="True" AllowSorting="True">
        <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="True" >
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" />
            <Scrolling AllowScroll="True" ScrollHeight="500" UseStaticHeaders="True" />
        </ClientSettings>
<MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID" >
    <Columns> 
    <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" >
        <HeaderStyle Width="30px"/>
    </telerik:GridClientSelectColumn>

    <telerik:GridTemplateColumn UniqueName="PicCount" HeaderStyle-HorizontalAlign="Justify" HeaderStyle-CssClass="padmenot" HeaderImageUrl="~/images/camera.jpg" >
        <HeaderStyle Width="30px"/>
                <ItemTemplate>
            <%#IIf(IsDBNull(Eval("PicCount")), "--", Eval("PicCount"))%>
        </ItemTemplate>
    </telerik:GridTemplateColumn>

    <telerik:GridBoundColumn UniqueName="LocationName" DataField="Location" HeaderText="Location" Visible="false">
        <HeaderStyle Width="105px" />
    </telerik:GridBoundColumn>

    <telerik:GridBoundColumn UniqueName="LogDate" DataField="LogDate" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}">
        <HeaderStyle Width="80px" />
    </telerik:GridBoundColumn>
    
    <telerik:GridBoundColumn UniqueName="PurchaseOrder" DataField="PurchaseOrder" HeaderText="PO #">
        <HeaderStyle Width="60px" />
    </telerik:GridBoundColumn>
    
    <telerik:GridBoundColumn UniqueName="Department" DataField="Department" HeaderText="Department" Visible="false" />
    <telerik:GridBoundColumn UniqueName="BadPallets" DataField="BadPallets" HeaderText="BadPallets" Visible="false" />
    <telerik:GridBoundColumn UniqueName="Restacks" DataField="Restacks" HeaderText="Restacks" Visible="false" />
    <telerik:GridBoundColumn UniqueName="VendorNumber" DataField="VendorNumber" HeaderText="Vendor #">
        <HeaderStyle Width="85px" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn UniqueName="Vendor" DataField="Vendor" HeaderText="Vendor Name">
        <HeaderStyle Width="200px" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn UniqueName="Carrier" DataField="Carrier" HeaderText="Carrier">
        <HeaderStyle Width="200px" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn UniqueName="TrailerNumber" DataField="TrailerNumber" HeaderText="TrailerNumber" Visible="false" />
    <telerik:GridBoundColumn UniqueName="ID" DataField="ID" HeaderText="woid" Visible="false" />
    </Columns>
</MasterTableView>
</telerik:RadGrid>
</td>
<td valign="top">
<telerik:RadAjaxPanel ID="pnlDetail" runat="server" Visible="false">
<table class="fieldTitle">
    <tr>
        <td>
            <table style="border:1px solid black;"><tr>
                    <td style="padding-left:13px;">
                      Date:<br />
                        &nbsp;  <asp:Label ID="lblDate" runat="server" CssClass="field" />
                    </td>
                    <td style="padding-left:13px;">
                        PO #:<br />
                        &nbsp;  <asp:Label ID="lblPurchaseOrder" CssClass="field" runat="server" />
                    </td>
                    <td style="padding:0 13px 0 13px;">
                        Location:<br />
                          &nbsp;  <asp:Label ID="lblLocation" CssClass="field" runat="server" />
                    </td>
            </tr>
                 <tr>
                    <td style="padding-left:13px;">
                        Restacks:<br /> 
                        &nbsp;  <asp:Label ID="lblRestacks" runat="server" CssClass="field" />
                    </td>
                    <td style="padding-left:13px;">
                        Bad Pallets:<br /> 
                        &nbsp;  <asp:Label ID="lblBadPallets" runat="server" CssClass="field" />
                    </td>
                    <td style="padding:0 13px 0 13px;">
                        Department:<br />
                        &nbsp;  <asp:Label ID="lblDepartment" CssClass="field" runat="server" />
                    </td>
                </tr>           
                <tr>
                    <td colspan="3">
            <table>
                <tr>
                    <td style="padding-left:13px;">
                        Vendor #:<br />
                        &nbsp; <asp:Label ID="lblVendorNumber" runat="server" CssClass="field" />
                    </td>
                    <td style="padding:0 7px 0 13px;">
                        Vendor:<br />
                        &nbsp; <asp:Label ID="lblVendor" runat="server" CssClass="field" />
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:13px;">
                        Trailer #:<br />
                        &nbsp; <asp:Label ID="lblTrailerNumber" runat="server" CssClass="field" />
                    </td>
                    <td style="padding:0 7px 0 13px;">
                        Carrier:<br />
                        &nbsp; <asp:Label ID="lblCarrier" runat="server" CssClass="field" />
                    </td>
                </tr>
            </table>
                    </td>
    
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
<table align="center" width="85%"><tr>
         <td valign="top" class="lilBlue">
            <span onmouseover="this.style.pointer='pointer';"><asp:Label ID="lblEditLoadImages" runat="server" /></span> 
        </td>
         <td align="right" valign="top" class="lilBlue">
            <span onmouseover="this.style.pointer='pointer';"><asp:Label ID="lblViewPhotoReport" runat="server" /></span> 
        </td>

</tr></table> 
        </td>
    </tr>   
 </table>

 
 </telerik:RadAjaxPanel>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" VisibleStatusbar="false" >
<Windows>
<telerik:RadWindow ID="wLoadImages" Title="SEU - Load Image Manager" 
    Height="560px" Width="875px"  
    ShowContentDuringLoad="false"  runat="server" Modal="True" 
    OnClientClose="closeit"
    Behaviors="Move, Resize, Maximize, Close" />
<telerik:RadWindow ID="winFreightIssues" Title="SEU Freight Issue Photo Report Generator" 
        Height="500" Width="400" Modal="True" Behaviors="Close, Resize, Maximize, Move" 
        Skin="Sunset" EnableShadow="True" OnClientClose="closeit" runat="server" />
</Windows>
</telerik:RadWindowManager>

</td>
</tr>
</table>
    </div>
<telerik:RadToolTip ID="tooltip1" runat="server" TargetControlID="lblHelp" 
        HideEvent="ManualClose" ShowEvent="OnClick" Animation="Resize"  Position="BottomLeft" RelativeTo="Element">
<table width="510" style="border:1px solid black;">
<tr><td class="ttHeader">SEU Freight Issue Pictures</td></tr>
<tr>
    <td class="ttBody">
Select date range and click 'Show Records' button.<br />
Leave 'Location' field blank to show records from all Locations available to you.
    </td>
</tr>
<tr><td class="ttTitle" style="padding-left:8px;">Sorting the List</td></tr>
<tr>
    <td class="ttBody">Click the column name in the top row. &nbsp;Click once to sort in ascending order, click again to sort in descending order and click again to remove sort order.
    <br />
    </td>
</tr>
<tr><td class="ttTitle" style="padding-left:8px;">Selecting Loads</td></tr>
<tr>
    <td class="ttBody" >
Clicking a row in the grid will:<br />
&nbsp; &nbsp;(1) display the load information to the right of the grid, and <br />
&nbsp; &nbsp;(2) que that load for batch printing (as indicated by the checkbox)<br />
To select multiple loads hold your CTRL key down and click the row(s) you want to print, or
use the checkboxes along the left side of the grid.<br />
To select a range, click the first row, hold your SHIFT key down and click the last row.<br />
To select ALL records check the checkbox in the upper left corner of the grid.
    </td>
</tr>
<tr><td class="ttTitle" style="padding-left:8px;">Managing Freight Issue Pictures</td></tr>
<tr>
    <td class="ttBody">Select a load from the grid to display the load details to the right of the grid.<br />
      Click the <font color='blue'>Manage Pictures</font> link just below the load details.<br />
    </td>
</tr>
<tr><td class="ttTitle" style="padding-left:8px;">Create/View Photo Report(s)</td></tr>
<tr>
    <td class="ttBody">To create/view a single report:<br />
    &nbsp; &nbsp;Select a load from the grid to display the load details to the right of the grid.<br />
    &nbsp; &nbsp;Click the <font color='blue'>View Photo Report</font> link just below the load details.<br />
    To create/view multiple reports:<br />
    &nbsp; &nbsp;Select multiple loads (max 50) from the grid. (see 'Selecting Loads' above)<br />
    &nbsp; &nbsp;Click the <font color='darkblue'>View Photo Reports (selected loads)</font> link at the top of the page. <br />
    &nbsp; &nbsp; &nbsp; &nbsp;(just right of the 'Show Records' button)<br />
    NOTE: multiple reports will be created in the order they are selected.<br />
    
    </td>
</tr>
<tr><td class="ttTitle" style="padding-left:8px;">Tips and Tricks</td></tr>
<tr>
    <td class="ttBody">(1) Press your F11 key to make your browser full screen. &nbsp;Press again to return to normal.<br />
(2) While viewing photo reports, resize the Photo Report window and adjust the PDF zoom<br />&nbsp; &nbsp; &nbsp; percentage. 
&nbsp;A setting of around 84% will look roughly the same as a printed page.  
<br />
(3) Multiple reports are created in the order they are selected. &nbsp;(yea, I said it twice)<br />
    </td>
</tr>
<tr>
<td align="center" style="font-size:11px;"><hr style="width:80%;height:1px;" />click X in upper right corner of this help screen to close</td>
</tr>
</table>

        </telerik:RadToolTip>


<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">
    function openLoadImages(arg) {
        //        document.getElementById("< %= btnSaveChanges.ClientID % >").disabled = true;
        var oManager = GetRadWindowManager();
        var loca = "../Upload/Async/Imageuploader/LoadImages.aspx?woid=" + arg;
        oManager.open(loca, "wLoadImages");
    }

    function openQuikAddLoadImages(arg) {
        //        document.getElementById("< %= btnSaveChanges.ClientID % >").disabled = true;
        var oManager = GetRadWindowManager();
        var loca = "../Upload/Async/Imageuploader/QuikAddLoadImages.aspx?woid=" + arg;
        oManager.open(loca, "wLoadImages");
    }
    
    function closeit(oWnd, args) {
        oWnd.setUrl("../seuLoader.aspx");
    }


    function openPDFwin(arg) {
        var oManager = GetRadWindowManager();
        var loca = "/ClientSvcs/seuFreightIssues.aspx?woid=" + arg;
        oManager.open(loca, "winFreightIssues");
    }
           </script>

</telerik:RadCodeBlock>



    </form>
</body>
</html>
