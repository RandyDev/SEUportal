<%@ Page Language="vb" AsyncTimeout="3000" AutoEventWireup="false" CodeBehind="ScheduledLoadImporter.aspx.vb" Inherits="DiversifiedLogistics.ScheduledLoadImporter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        div.centered table{
    margin:0 auto;
    text-align:left; 
}
    </style>

</head>
<body>
    <form id="form1" runat="server">
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="cbLocations">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cbImportName" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="RadDatePicker2" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="divupload" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cbImportName">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadDatePicker2" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="divupload" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadDatePicker2">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divupload" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnImport">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divupload" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"></telerik:RadAjaxLoadingPanel>
        <table>
    <tr>
        <td colspan="2">
            <span style="font-size:23px;">Import Scheduled Loads, Universal Excel Importer </span>
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top;">
<table>
    <tr>
        <td>
            <telerik:RadComboBox ID="cbLocations" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadComboBox ID="cbImportName" AutoPostBack="true" EmptyMessage="^^Select Location FIRST" Width="261px" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
    <telerik:RadDatePicker ID="RadDatePicker2" AutoPostBack="true" DateInput-EmptyMessage="Select date->>" runat="server">
<DateInput ID="DateInput2" runat="server" DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%" AutoPostBack="True"/>
    </telerik:RadDatePicker>
        </td>
    </tr>
</table>
        </td>
    <td style="padding-left:24px;vertical-align:top;">
 <div id="divupload" style="margin:auto; border:1px solid black" visible="false"  runat="server">
 <table>
    <tr>
        <td colspan="2">
            <telerik:RadAsyncUpload RenderMode="Lightweight" ID="AsyncUpload1" runat="server" HideFileInput="true" MaxFileInputsCount="1"
                MultipleFileSelection="Disabled" AllowedFileExtensions=".xlsx" UploadedFilesRendering="BelowFileInput" >
                <Localization Select="Click to Select, or Drop Excel File Here" />
            </telerik:RadAsyncUpload>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="padding-top:7px;">    
<telerik:RadButton ID="btnImport" runat="server" Text="Process this File" />
        </td>
    </tr>
    <tr>
        <td class="auto-style1">    
            </td>
        <td class="auto-style1">    
            <telerik:RadLabel ID="lblresult" runat="server" text="---"/>
        </td>
    </tr>
  </table>
</div>
    </td>
   </tr>
</table>
    </form>
</body>
</html>
