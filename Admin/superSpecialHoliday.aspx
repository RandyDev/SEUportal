<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="superSpecialHoliday.aspx.vb" Inherits="DiversifiedLogistics.superSpecialHoliday" %>
<%--<%@ Register TagPrefix="uc1" TagName="AdditionalPay" Src="~/UserControls/AdditionalPay.ascx" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style type="text/css">
.spreadSheet
{
    width: 752px;
    background: url(../Images/spreadsheetBg.png) left top no-repeat;
    padding-top:55px;
    margin: 0 auto;
}
.bottomSheetFrame
{
    width: 752px;
    background: url(../Images/spreadsheetBottomBg.png) left bottom no-repeat;
    padding-bottom:14px;
    overflow: hidden;
}
.RadGrid_Office2007
{
    border-left: none !important;
}
.multiPage
{
    margin: 0 14px;
}
.tabStrip
{
    margin: 0 15px 0 14px !important;
}
.tabStrip .rtsLevel1
{
    padding:0 !important;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
        <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadTabStrip1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadTabStrip1" />
                        <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="LoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadMultiPage1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="LoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
<script type="text/javascript">
    function onTabSelecting(sender, args) {

        if (args.get_tab().get_pageViewID()) {
            args.get_tab().set_postBack(false);
        } else { 
//        alert("not canceling postback")
        }
    }
</script>
    <div class="spreadSheet">
    <div class="bottomSheetFrame">
    
    

        <telerik:RadMultiPage ID="RadMultiPage1" CssClass="multiPage" runat="server">
        </telerik:RadMultiPage>

        <telerik:RadTabStrip ID="RadTabStrip1" OnClientTabSelecting="onTabSelecting" Orientation="HorizontalBottom"
            cssclass="tabStrip" Skin="Office2007"  runat="server" MultiPageID="RadMultiPage1">
        </telerik:RadTabStrip>
    </div>
    </div>
    </form>
</body>
</html>
