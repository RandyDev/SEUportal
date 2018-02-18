<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pubDockActivity.aspx.vb" Inherits="DiversifiedLogistics.pubDockActivity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Current Dock Activity Report</title>
    <link href="../styles/StyleSheet.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.trans{
 background-color:transparent;
}

</style>

<telerik:RadScriptBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var ty = 0;
        function getScrollXY() {
            var x = 0, y = 0;
            if (typeof (window.pageYOffset) == 'number') {
                // Netscape
                x = window.pageXOffset;
                y = window.pageYOffset;
            } else if (document.body && (document.body.scrollLeft || document.body.scrollTop)) {
                // DOM
                x = document.body.scrollLeft;
                y = document.body.scrollTop;
            } else if (document.documentElement && (document.documentElement.scrollLeft || document.documentElement.scrollTop)) {
                // IE6 standards compliant mode
                x = document.documentElement.scrollLeft;
                y = document.documentElement.scrollTop;
            }
            return [x, y];
        }

        function getpos() {
            var xy = getScrollXY();
            var x = xy[0];
            var y = xy[1];
            if (y > ty) {
                ty = y;
            } else {
                sleep(5000);
                document.documentElement.scrollTop = 0;
                ty = 0;
                sleep(7000);
            }
        }

        function pageScroll() {
            window.scrollBy(0, 1);   // horizontal and vertical scroll increments
            getpos();
            scrolldelay = setTimeout('pageScroll()', 50);  // scrolls every # milliseconds
        }

        function sleep(delay) {
            var start = new Date().getTime();
            while (new Date().getTime() < start + delay);
        }

    </script>
</telerik:RadScriptBlock>

<style type="text/css">
</style>
</head>
<body onload="pageScroll()">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" EnableEmbeddedSkins="true" Skin="Simple" />
    <div>
    <telerik:RadAjaxLoadingPanel Transparency="30" ID="RadAjaxLoadingPanel1" runat="server" >
           <table style="width:100%;height:100%;background-color:transparent;">
                <tr><td align="center" valign="middle">
                <asp:Image ID="seuLogoAni" runat="server" ImageUrl="~/images/SEUloadingani.gif" AlternateText="Updating List ..." BorderWidth="0px" />
                </td></tr>
            </table>
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager DefaultLoadingPanelID="RadAjaxLoadingPanel1" ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="thuTime">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
<asp:Panel ID="Panel1" runat="server">
<asp:Timer id="thuTime" Interval="180000" runat="server"></asp:Timer>
<table cellspacing="0" style="width: 100%; border-collapse:collapse;" border="1">
    <thead>
        <tr class="rlvHeader">
            <th>Dr #</th>
            <th>Vendor</th>
            <th>PO #</th>
            <th>Carrier</th>
            <th>Trailer</th>
            <th>Dock Time</th>
            <th>Start</th>
            <th>Finish</th>
            <th>Department</th>
            <th>LoadType</th>
            <th>Unloaders</th>
         </tr>
    </thead>
    <tfoot>
        <tr class="rlvHeader">
            <th>Dr #</th>
            <th>Vendor</th>
            <th>PO #</th>
            <th>Carrier</th>
            <th>Trailer</th>
            <th>Dock Time</th>
            <th>Start</th>
            <th>Finish</th>
            <th>Department</th>
            <th>LoadType</th>
            <th>Unloaders</th>
        </tr>
    </tfoot>
    <tbody style="font-size:12px;"> 
        <asp:Literal ID="LoadList" runat="server" />
    </tbody>
</table>
</asp:Panel>
                        </div>

</form>

</body>
</html>
