<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClientGridTips.aspx.vb" Inherits="DiversifiedLogistics.ClientGridTips" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style type="text/css">
body{
    font-family:Arial;
}
.hdr{
    font-size:12px;
    font-weight:bold;
    padding-top:7px;
}
.dtl{
    font-size:11px;
    font-weight:normal;
    padding-left:7px;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0">
    <tr><td class="hdr">Grid to large for screen?&nbsp; Maximize your browser window.</td></tr>
    <tr><td class="dtl">In Internet Explorer your F-11 key acts as a full screen toggle.<br />
                        Most browsers have a similar feature. <em>Try it now</em>
    </td></tr>
    <tr><td class="hdr">Sorting</td></tr>
    <tr><td class="dtl">Hover the text in a column header, the pointer will change to a hand.<br />
                        Click once to sort Ascending. &nbsp;Click again to sort Descending. <br />
                        Click third time to clear column sort. &nbsp;<em>not all columns sortable</em>
    </td></tr>
    <tr><td class="hdr">Reordering</td></tr>
    <tr><td class="dtl">Hover the header (not on text), the pointer will change to a crosshair.<br />
                        Click and Drag column to new location, release mouse button.
    </td></tr>
    <tr><td class="hdr">Grouping</td></tr>
    <tr><td class="dtl">Hover the header (not on text), the pointer will change to a crosshair.<br />
                        Click and Drag the header to the 'Grouping Row' at top of grid and release.<br />
                        You may drag multiple columns onto grouping row for a more hierarchical view.<br />
                        Drag header outside of grouping row to remove grouping.<br />
                        <em>not all columns groupable</em>
                        
    </td></tr>
    <tr><td class="hdr">Filtering</td></tr>
    <tr><td class="dtl">Some columns allow filtering. (textbox and filter button just below header)<br />
                        Enter search or filter text in textbox, click filter button and choose filter.<br />
                        NOTE: Filters are CaSe Sensitive. &nbsp;'Carbona' and 'CARBONA' are NOT a match.<br />
                        To clear filter, click filter button and choose 'NoFilter' at top of list.
    </td></tr>
    <tr><td class="hdr">Exporting</td></tr>
    <tr><td class="dtl">Why would you want to? &nbsp;Our real time data grids are really cool.<br />
                        You will find the export icons on the bottom right corner of the grid. <br />
                        Supports: Excel, PDF, Word and CSV (comma delimited).
    </td></tr>
<tr><td class="dtl" style="padding:8px 0 0 0;">
                        Leave this window open for reference as you explore our grid's feature set.<br />
                        Just drag this window out of the way and you can still interact with the grid.<br />
                        Enjoy!
                        
</td></tr>

    </table>
    </div>
    </form>
</body>
</html>
