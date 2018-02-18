<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DockMonitor_DockActivity.aspx.vb" Inherits="DiversifiedLogistics.DockMonitor_DockActivity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style type="text/css">
.ticker{
font-size:20px;
 font-family: Comic Sans MS;
}
</style>
</head>

        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

            <script type="text/javascript">
                var refreshTimer;
                var scrollTimer;
                var tempYpos = 0;

//                function pageLoad() {
//                    refreshTimer = setTimeout("InitiateAjaxRequest('argMe')", 15000);
//               }

                function InitiateAjaxRequest(arguments) {
//                    centerLoadingPanel();
                    clearTimeout(refreshTimer);
                    var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                    ajaxManager.ajaxRequest(arguments);
                }

                function responseEnd(sender, eventArgs) {
                    //            alert("response.End : " + eventArgs);
                    clearTimeout(refreshTimer);
                }



                function getpos() {
                    var grid = $find("<%=RadGrid1.ClientID %>");
                    var scrollArea = document.getElementById("<%= RadGrid1.ClientID %>" + "_GridData");

                    var y = scrollArea.scrollTop;
                    if (y > tempYpos) {
                        tempYpos = y;
                    } else {
                        sleep(5000);
                       scrollArea.scrollTop = 0;
                        tempYpos = 0;
                        sleep(7000);
                    }
                }

                function pageScroll() {
                     var scrollArea = document.getElementById("<%= RadGrid1.ClientID %>" + "_GridData");
                    scrollArea.scrollTop += 1
//                    window.scrollBy(0, 1);   // horizontal and vertical scroll increments
                    getpos();
                    scrollTimer = setTimeout('pageScroll()', 40);  // scrolls every # milliseconds
                }

                function sleep(delay) {
                    var start = new Date().getTime();
                    while (new Date().getTime() < start + delay);
                }
                function centerLoadingPanel() {
                    centerElementOnScreen($get("<%= RadAjaxLoadingPanel1.ClientID %>"));
                }

                function centerElementOnScreen(element) {
                    var scrollTop = document.body.scrollTop;
                    var scrollLeft = document.body.scrollLeft;
                    var viewPortHeight = document.body.clientHeight;
                    var viewPortWidth = document.body.clientWidth;
                    if (document.compatMode == "CSS1Compat") {
                        viewPortHeight = document.documentElement.clientHeight;
                        viewPortWidth = document.documentElement.clientWidth;
                        if (!$telerik.isSafari) {
                            scrollTop = document.documentElement.scrollTop;
                            scrollLeft = document.documentElement.scrollLeft;
                        }
                    }
                    var topOffset = Math.ceil(viewPortHeight / 2 - element.offsetHeight / 2);
                    var leftOffset = Math.ceil(viewPortWidth / 2 - element.offsetWidth / 2);
                    var top = scrollTop + topOffset - 40;
                    var left = scrollLeft + leftOffset - 70;
                    element.style.position = "absolute";
                    element.style.top = top + "px";
                    element.style.left = left + "px";
                }

            </script>

        </telerik:RadCodeBlock>
        
        <body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblGridTitle" />
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                        <telerik:AjaxUpdatedControl ControlID="pnlTime" 
                            LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel Transparency="35" ID="RadAjaxLoadingPanel1"  runat="server">
        <asp:Image id="Image1" runat="server" Width="110" Height="21" ImageUrl="~/images/forkliftani.gif"></asp:Image>
    </telerik:RadAjaxLoadingPanel>

<telerik:RadComboBox ID="cbLocation" runat="server" Visible="false" />
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
    <div style="margin: 0 auto; width:1180px;">

<table width="100%">
    <tr>
        <td width="33%">
            <asp:Image ID="imgClientLogo" runat="server" Height="75px" />
        </td>
<td width="34%" align="center">
        <span style="Font-family:Arial;font-size:16px;"><asp:Label ID="lblLocaName" runat="server" /></span>
</td>
        <td  width="33%" align="right">
        <span style="Font-family:arial;font-size:17px;">
            <asp:Label ID="lblDate" runat="server"/></span><br />
        <span style="Font-family:arial;font-size:24px;">
<asp:Label ID="lblGridTitle" runat="server" /></span>        </td>
    </tr>
</table>

<div  style="font-size:18px;margin: 0 auto; width:1160px;">

        <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="false">
        <MasterTableView CssClass="ticker" >
<NoRecordsTemplate>
<br /><br /><br /><br /><br /><br />
<center><span style="font-family:arial;font-size:35px;">... all working loads complete ...<br /><br />No Acitve Loads</span></center> 

</NoRecordsTemplate>
        <Columns>
        <telerik:GridBoundColumn DataField="DoorNum" UniqueName="DoorNum" HeaderText="Dr #">
            <HeaderStyle Width="35px" />
        </telerik:GridBoundColumn>  
        <telerik:GridBoundColumn DataField="Vendor" UniqueName="Vendor" HeaderText="Vendor">
            <HeaderStyle Width="200px" />
        </telerik:GridBoundColumn>  
        <telerik:GridBoundColumn DataField="PurchaseOrder" UniqueName="PurchaseOrder" HeaderText="PO #">
            <HeaderStyle Width="45px" />
        </telerik:GridBoundColumn>  
        <telerik:GridBoundColumn DataField="Carrier" UniqueName="Carrier" HeaderText="Carrier">
            <HeaderStyle Width="200px" />
        </telerik:GridBoundColumn>  
        <telerik:GridBoundColumn DataField="TrailerNumber" UniqueName="Trailer" HeaderText="Trailer">
            <HeaderStyle Width="45px" />
        </telerik:GridBoundColumn>  
        <telerik:GridBoundColumn DataField="DockTime" UniqueName="DockTime" HeaderText="Dock Time">
           <HeaderStyle Width="65px" />
        </telerik:GridBoundColumn>  
        <telerik:GridBoundColumn DataField="StartTime" UniqueName="StartTime" HeaderText="Start">
            <HeaderStyle Width="60px" />
        </telerik:GridBoundColumn>  
        <telerik:GridBoundColumn DataField="CompTime" UniqueName="CompTime" HeaderText="Finish">
            <HeaderStyle Width="60px" />
        </telerik:GridBoundColumn>  
        <telerik:GridBoundColumn DataField="Department" UniqueName="Department" HeaderText="Department">
            <HeaderStyle Width="70px" />
        </telerik:GridBoundColumn>  
        <telerik:GridBoundColumn DataField="Unloaders" UniqueName="Unloaders" HeaderText="Unloaders">
            <HeaderStyle Width="200px" />
        </telerik:GridBoundColumn>  
        </Columns>
        </MasterTableView>

            <ClientSettings>
                <Scrolling AllowScroll="true" ScrollHeight="675px" UseStaticHeaders="true" />
            </ClientSettings>
        </telerik:RadGrid>
    </div>

<table width="100%">
    <tr>
        <td width="20%" valign="top">
            <asp:Image ID="imgSEUlogo" runat="server" ImageUrl="~/images/SouthEastUnloading.jpg" Height="50px" />
        </td>
        <td align="center" style="padding-top:10px;" valign="middle">
            <telerik:RadRotator ScrollDirection="Up" EnableRandomOrder='true' FrameDuration="6000" Width="500" Height="75" ItemHeight="75" WrapFrames="true" CssClass="ticker" AutoStart="true" runat="server" ID="Radticker1" >
            <ItemTemplate>
            <asp:Label ID="lblRotatorItem" runat="server"><%# DataBinder.Eval(Container.DataItem, "Name") %></asp:Label>
</ItemTemplate>
            </telerik:RadRotator >

        </td>
        <td valign="top" align="right" width="20%">
            <span style="Font-family:arial;font-size:13px;">Last Updated:</span><br />
<telerik:RadAjaxPanel ID="pnlTime" runat="server" Width="110">
            <span style="Font-family:arial;font-size:15px;"><asp:Label ID="lblGridFooter" Text="grid footer" runat="server" /></span></telerik:RadAjaxPanel>
        </td>
    </tr>
</table>

</div>
</telerik:RadAjaxPanel>
    </form>
</body>
</html>
