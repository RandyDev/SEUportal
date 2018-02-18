<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="selectUnloaders.aspx.vb" Inherits="DiversifiedLogistics.selectUnloaders" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />
    <div>
    <table><tr>
        <td valign="top">Assigned: <br />
           <telerik:RadListBox ID="lbUnloaderList" TransferMode="Move" AllowTransfer="true" 
           TransferToID="lbAvailableUnloaders" runat="server" DataKeyField="ID"
           EmptyMessage="None listed, select from Available list."  
           Width="175px" Height="125px" AllowDelete="false" ButtonSettings-ShowTransferAll="False" 
           ButtonSettings-ShowReorder="False" AllowReorder="True" EnableDragAndDrop="True"
           ButtonSettings-ShowDelete="False" >
           

           </telerik:RadListBox>
           
        </td>
        <td valign="top">
            	<table cellpadding="0" cellspacing="0">
                	<tr>
                    	<td >Available:<br />
                        	<telerik:RadListBox ID="lbAvailableUnloaders" runat="server" DataKeyField="ID" AllowTransfer="True"
                            	TransferMode="Move" EnableDragAndDrop="True" TransferToID="lbUnloaderList" 
                                 Height="150px" Width="175px" ButtonSettings-ShowTransferAll="False" 
                                 ButtonSettings-ShowTransfer="False" ButtonSettings-ShowReorder="False" 
                                 EmptyMessage="You've used everybody up, call in the reserves!" 
                                 ButtonSettings-ShowDelete="False" >
                            </telerik:RadListBox>
                        </td>
                    </tr>

                </table>

        </td>
    </tr></table>
<asp:Button ID="btnApplyUnloaders" runat="server" Text="Apply this List" />
    </div>
     <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    
<script language="javascript" type="text/javascript">

function pageLoad() {
    var currentWindow = GetRadWindow();
}

function GetRadWindow() {
    var oWindow = null;
    if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)
    return oWindow;
}

function cancelAndClose() {
    var oWindow = GetRadWindow();
    oWindow.argument = null;
    oWindow.close();
}

function returnArg(arg) {
    var oWnd = GetRadWindow();
    oWnd.close(arg);
}

</script>
        </telerik:RadCodeBlock>
    </form>
</body>
</html>
