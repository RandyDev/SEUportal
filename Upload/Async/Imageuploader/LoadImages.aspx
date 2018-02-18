<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoadImages.aspx.vb" Inherits="DiversifiedLogistics.LoadImages" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
body{ font-family:Arial;}
.dbtn{
height: 32px;
width: 75px;
}
.imageContainer{
    border:1px solid black;
    width:513px;
    min-height:50px;
}
.lilblue
{
    color:blue;
    font-size:11px;
    text-decoration:underline;
}

.photo-container
{
    padding: 5px;
    float: left;
}
.data-container
{
    height: 90%;
    width: auto;
}
       .ruBrowse
       {
           background-position: 0 -46px !important;
           width: 122px !important;
       }
        .style1
        {
            width: 341px;
            vertical-align: top;

        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" 
                Name="Telerik.Web.UI.Common.Core.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" 
                Name="Telerik.Web.UI.Common.jQuery.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" 
                Name="Telerik.Web.UI.Common.jQueryInclude.js">
            </asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadListView1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadAsyncUpload1" />
                    <telerik:AjaxUpdatedControl ControlID="lblExistingPictures" />
                    <telerik:AjaxUpdatedControl ControlID="divImg" />
                    <telerik:AjaxUpdatedControl ControlID="RadListView1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

    <script type="text/javascript">
    //<![CDATA[
        function fileUploaded(sender, args) {
            var id = args.get_fileInfo().ImageID;
            $(".imageContainer")
                //.empty()
                .append($("<img />")
                    .attr("src", getImageUrl(id)));
            $(".info")
                .html("Close window to add these pictures");
        }

        function InitiateAjaxRequest(arguments) {
            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
            ajaxManager.ajaxRequest(arguments);
        }

        function getImageUrl(id) {
            var url = window.location.href;
            var handler = "StreamImage.ashx?scale=.5&imageID=" + id;
            var index = url.lastIndexOf("/");
            var completeUrl = url.substring(0, index + 1) + handler;
            return completeUrl;
        }

        function fileUploadRemoving(sender, args) {
            var index = args.get_rowIndex();
            //          alert("deletin : "  + args.get_fileName() + " : " + args.get_fileInfo().ImageID);
            InitiateAjaxRequest("delete:" + args.get_fileInfo().ImageID);
            $(".imageContainer img:eq(" + index + ")").remove();
        }
    //]]>
    </script>
    </telerik:RadCodeBlock>


<table>
    <tr>
        <td class="style1">
            <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" Width="275" 
                HttpHandlerUrl="~/Upload/Async/Imageuploader/Handler.ashx" 
                OnClientFileUploaded="fileUploaded" OnClientFileUploadRemoving="fileUploadRemoving" MultipleFileSelection="Automatic"
                AllowedFileExtensions="jpeg,jpg,gif,png,bmp" >
         <Localization Select="Add Picture(s)" />
            </telerik:RadAsyncUpload>
        </td>
        <td valign="top"><asp:Label ID="lblNewPictures" runat="server" />
<div id="divImg" style="width:515px;" runat="server">
<table cellpadding="0" cellspacing="0" width="100%">
<tr>
<td><span class="info">Uploaded Pictures </span></td>
<td align="right" style="padding-right:8px;"><span onmouseover="this.style.cursor='help';"><asp:Label runat="server" ID="lblHelp" CssClass="lilblue" Text="help with this page" /></span></td>
</tr></table>
   
    <div class="imageContainer"></div>
</div>
        </td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0"><tr>
<td><asp:Label ID="lblExistingPictures" runat="server" /> </td>
<td> &nbsp; &nbsp;<asp:LinkButton class="lilblue" ID="btnPrint" Text="View Photo Report" Visible="false" runat="server" />
</td></tr></table>
        <telerik:RadListView ID="RadListView1" runat="server"
            ItemPlaceholderID="ListViewContainer">
            <LayoutTemplate>
                <asp:PlaceHolder runat="server" id="ListViewContainer" />
            </LayoutTemplate>
            <ItemTemplate>
                <fieldset style="float: left; width: 266px; height: 215px;">
                        <div class="photo-container">
                            <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" DataValue='<%#Eval("ImageData") %>'
                                AutoAdjustImageControlSize="false" Width="256px" Height="192px" ToolTip='<%#Eval("ImageName", "{0}") %>'
                                AlternateText='<%#Eval("ImageName", "{0}") %>' />

                                <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                <td>
                                <span style="font-size:11px; font-family:Arial;">
                                <asp:LinkButton ID="lnkBtnDelete" CommandName="Delete" CommandArgument='<%#Eval("ImageID").ToString %>' Text="delete" runat="server" /></span>
                                </td>
                                <td align="right">
                                <span style="font-size:11px; font-family:Arial;"><%#Eval("Uploader")%></span>
                                </td>
                                </tr>
                                </table>
                                 
                        </div>
                </fieldset>
            </ItemTemplate>
        </telerik:RadListView>
        <div style="clear: both;">
        </div>

    </div>
<telerik:RadToolTip ID="tooltip1" runat="server" TargetControlID="lblHelp" 
        HideEvent="ManualClose" ShowEvent="OnClick" Animation="Resize"  Position="BottomLeft" RelativeTo="Element">
<table width="510" style="border:1px solid black;">
<tr><td class="ttHeader">SEU Load Image Manager</td></tr>
<tr><td class="ttTitle" style="padding-left:8px;">Add Picture(s)</td></tr>
<tr>
    <td class="ttBody">
Click the 'Add Picture(s)' button to open a file selection dialog box.<br />
Navigate to the folder (or camera) where your pictures are stored.<br />
Select the picture(s) you want to send up for this load.<br />
 &nbsp; &nbsp;(hold your Ctrl key to select multiple pictures)<br />
Click the 'Open' button in the file selection dialog and the upload will automatically begin.<br />
The filename of the selected picture(s) will be listed above the upload control.<br />
Uploaded pictures will be displayed two abreast to the right of the upload control.<br />
When finished, simply close the Load Image Manager. (x in upper right corner)<br />
    </td>
</tr>
<tr><td class="ttTitle" style="padding-left:8px;">Existing Pictures</td></tr>
<tr>
    <td class="ttBody" >
Existing pictures, if any, are displayed three across below the upload control.<br />
Below each image is a delete button and the name of the person that uploaded the image.<br />
IMPORTANT! The 'delete' buttons are small AND far away from any other control for a reason.<br />
If you click a delete button it is assumed you did it on purpose.<br />
We will not waste your time with a confirmation. The image will immediately be deleted.<br />
    </td>
</tr>
<tr><td class="ttTitle" style="padding-left:8px;">Tips and Tricks</td></tr>
<tr>
    <td class="ttBody">Press your F11 key to make your browser full screen. &nbsp;Press again to return to normal.<br />
Lot's of pictures? <br />
&nbsp; &nbsp;Use the 'maximize' icon in the upper right corner of the Load Image Manager.<br />
<br />
    </td>
</tr>
<tr>
<td align="center" style="font-size:11px;"><hr style="width:80%;height:1px;" />click X in upper right corner of this help screen to close</td>
</tr>
</table>



</telerik:RadToolTip>
<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
<script type="text/javascript">
    function GetRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    }
    
     function openPDFwin(arg) {
        var parentPage = GetRadWindow().BrowserWindow;
        var parentRadWindowManager = parentPage.GetRadWindowManager();
        var oWnd2 = parentRadWindowManager.open("../ClientSvcs/seuFreightIssues.aspx?woid=" + arg, "winFreightIssues");
        window.setTimeout(function () {
            oWnd2.setActive(true);
        }, 0);
    }

//    function OpenIssuesPDF(arg) {
//       alert("showpdf " + arg);
//        var list = $find("< %= lstBoxworkorders.ClientID % >");
//        var items = list.get_items();
//        var arg = "";
//        items.forEach(function (item) {
//            arg += item.get_value() + ":"
//        });
//        var oWnd = $find("< %= winFreightIssues.ClientID % >");
//        oWnd.setUrl("../ClientSvcs/SEU_LoadIssuePictures.aspx?woid=" + arg);
//        oWnd.show();
//    }

//    function listBoxLoad(sender, args) {
        //        var elementHeight = $telerik.getSize(sender.get_items().getItem(0).get_element()).height;
        //        sender._groupElement.style.height = "24px";  //(elementHeight * 5) + "px";

//    }




</script>
</telerik:RadScriptBlock>

    </form>
</body>
</html>
