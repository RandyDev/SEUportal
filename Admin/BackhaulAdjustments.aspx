<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BackhaulAdjustments.aspx.vb" Inherits="DiversifiedLogistics.BackhaulAdjustments" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
.allowed-attachments {
    vertical-align:bottom;
}
.async-attachment {
    display:inline-block;
}
.attachment-container {
    padding-bottom:20px;
}
div.centeredA{
    text-align:center;
}
div.centered table{
    margin:0 auto;
    text-align:left;
}   
.padresult{
padding:4px 7px 4px 7px;
}
         .auto-style1 {
            height: 21px;
        }
         </style>
</head>
<body>
<form id="form1" runat="server">
   	<telerik:RadScriptManager ID="RadScriptManager1" runat="server">

    </telerik:RadScriptManager>
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelsRenderMode="Inline">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cbLocations">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divul" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="lblerrLocation" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="raddatepicker1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divul" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="lblerrLocation" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnImport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divul" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="lblerrLocation" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
	</telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"></telerik:RadAjaxLoadingPanel>

<div style="margin:auto;align-self:center;font-family:Arial;font-size:16px; font-weight:bold;"> CBI Backhaul Billing Importer</div>
    <br />
    Select Load Date:<br />
    <telerik:RadDatePicker ID="RadDatePicker1" AutoPostBack="true" DateInput-EmptyMessage="Select date->>" runat="server">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;"></Calendar>

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%" AutoPostBack="True">
<EmptyMessageStyle Resize="None"></EmptyMessageStyle>

<ReadOnlyStyle Resize="None"></ReadOnlyStyle>

<FocusedStyle Resize="None"></FocusedStyle>

<DisabledStyle Resize="None"></DisabledStyle>

<InvalidStyle Resize="None"></InvalidStyle>

<HoveredStyle Resize="None"></HoveredStyle>

<EnabledStyle Resize="None"></EnabledStyle>
</DateInput>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
    </telerik:RadDatePicker>
    <telerik:RadComboBox ID="cbLocations" Width="150px" Filter="Contains" runat="server" AutoPostBack="true" /> 
    <asp:Label ID="lblerrLocation" ForeColor="Red" text="<- Please Select Location" runat="server" Visible="false" />

        <div id="divul" style="margin:auto;" class="centered" visible="false"  runat="server">

<table style="align-self:center; border:1px solid black;">
    <tr>
        <td></td>
        <td>
	        <div class="demo-container size-wide">
            <div class="attachment-container">
 <table>
     <tr>
         <td colspan="2" style="text-align:center;vertical-align:middle;font-weight:bold;">
             Define Upper Left Corner of data table
         </td>
</tr><tr>
      <td>
              On what row NUMBER does the data begin? 
          </td>
          <td>
              <telerik:RadNumericTextBox ID="numstartRow" NumberFormat-DecimalDigits="0" value="10" MinValue="1" runat="server" Height="19px" Width="40px"></telerik:RadNumericTextBox>
          </td>
      </tr>
      <tr>
          <td>
              On what column LETTER does the data begin?&nbsp;&nbsp; 
          </td>
          <td>
              <telerik:RadTextBox ID="txtstartcolumn" runat="server" Height="19px" Text="C" Width="40px"></telerik:RadTextBox>
          </td>
      </tr>
      <tr>
          <td colspan="2" style="border-top:2px solid black;">
              <telerik:RadAsyncUpload RenderMode="Lightweight" ID="AsyncUpload1" runat="server" HideFileInput="true" MaxFileInputsCount="1"
                MultipleFileSelection="Disabled" AllowedFileExtensions=".xlsx" UploadedFilesRendering="BelowFileInput" >
                <Localization Select="Click to Select, or Drop Excel File Here" />
              </telerik:RadAsyncUpload>
          </td>
      </tr>
</table>

            </div>
            </div>
        </td>
    </tr>
    <tr>
        <td>    
            &nbsp;</td>
        <td>    
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




    </form>
</body>
</html>
