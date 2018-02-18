<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="DiversifiedLogistics._Default" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<telerik:RadStyleSheetManager runat="server" ID="RadStyleSheet1" />

    <link href="styles/styles.css" rel="stylesheet" type="text/css" media="screen" />
    <link rel="SHORTCUT ICON" href="../Images/favicon.ico"/>
    <title>Diversified Logistics</title>
<script type="text/javascript">
    //function OnClientItemClicking(sender, args) {
    //    if (args.get_item().get_isOpen() == true) {
    //        args.set_cancel(true);
    //        args.get_item().close;
    //    }
    //}
</script>
</head>
<body>

    <form id="form1" runat="server" >
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server"  AsyncPostBackTimeOut="600">
        </telerik:RadScriptManager>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" 
            EnableEmbeddedSkins="true" Skin="Default" />
        <telerik:RadSplitter Skin="Simple" runat="Server" ID="RadSplitter1" Width="100%" BorderSize="0"
			BorderStyle="None" PanesBorderSize="0" Height="100%" Orientation="Horizontal"
			VisibleDuringInit="false">
			<telerik:RadPane ID="topPane" Scrolling="None" runat="server" Height="75" MinHeight="75" MaxHeight="75">
				<div ID="divheader" runat="server">
					<div class="logo" style="width:100%" >
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width:33%;">
                                <img src="../images/spacer.gif" width="25" alt="" />
                                <asp:HyperLink ID="mstLogo" NavigateUrl="~/default.aspx" ImageUrl="~/images/logo.png" ToolTip="Click to Home Page" runat="server" />
                            </td>
                            <td style="width:34%;" align="center">
                                <asp:Image ID="imgClientServices" ImageUrl="../images/ClientSvcs.png" runat="server" Visible="false" />
                            </td>
                            <td style="width:33%;">
                                <div style="padding-top:20px; float:right;">
                                    <asp:UpdatePanel ID="upthuTime" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
	                                        <span style="float:right;padding-right:10px;" class="breadcrumb">	
                                                &nbsp; &nbsp; <asp:label id="lbltime" runat="server">
                                                    <%=String.Format("{0:dd MMM yyyy - hh:mm tt}", DateTime.Now)%>
                                                              </asp:label>
                                                <asp:Timer id="thuTime" Interval="60000" runat="server" />
	                                        </span>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:Panel ID="lnkLogOut" Visible="true" runat="server" EnableViewState="false">
 		                                <span class="breadcrumb" style="float:right;">
                                            <asp:LoginStatus ID="LoginStatus2" style="color:White;" runat="server" LogoutAction="Redirect" LogoutPageUrl="~/Default.aspx" />
<%--<asp:LinkButton ID="lbLogOff" CssClass="logout" Text="Logout" OnClientClick="window.close();return false;" runat="server" />--%>
                                            &nbsp; <asp:Label id="lbluserName" runat="server" />
                                    	</span>
                                    </asp:Panel>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>				
            </div>
			<telerik:RadMenu RenderMode="Lightweight" ID="RadMenu1" runat="server" EnableRoundedCorners="true" ShowToggleHandle="true"
            EnableShadows="true" EnableTextHTMLEncoding="true" DataValueField="Text" Style="position:absolute;z-index:3000" 
                DataSourceID="XmlDataSource1" DataNavigateUrlField="Url" DataTextField="Text" 
                Width="100%" />
            
            <asp:XmlDataSource ID="XmlDataSource0" runat="server" DataFile="~/menus/sysopMenu.xml" XPath="/Items/Item" />
<%--            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/menus/adminMenu.xml" XPath="/Items/Item" />
            <asp:XmlDataSource ID="XmlDataSource2" runat="server" DataFile="~/menus/managerMenu.xml" XPath="/Items/Item" />
            <asp:XmlDataSource ID="XmlDataSource3" runat="server" DataFile="~/menus/menu.xml" XPath="/Items/Item" />
            <asp:XmlDataSource ID="XmlDataSource4" runat="server" DataFile="~/menus/clientMenu.xml" XPath="/Items/Item" />
            <asp:XmlDataSource ID="XmlDataSource5" runat="server" DataFile="~/menus/clientMenu.xml" XPath="/Items/Item" />
            <asp:XmlDataSource ID="XmlDataSource6" runat="server" DataFile="~/menus/clientMenu.xml" XPath="/Items/Item" />--%>
        </telerik:RadPane>
    <telerik:RadSplitBar ID="RadSplitBar1" runat="server" />
		    <telerik:RadPane runat="server" Scrolling="Y" ID="cPane">
            </telerik:RadPane>
        </telerik:RadSplitter>
    </form>
    <telerik:RadScriptBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript" language="javascript">
            function learnMe(vid) {
                tVideo.setUrl(vid);
                tVideo.Open();
                tVideo.screenCenter();
            }
            function unloadme() {
                tVideo.setUrl("");
            }
        </script>
    </telerik:RadScriptBlock>
<%--************************************************************************--%>  
<%--************************************************************************--%>
</body>
</html>
