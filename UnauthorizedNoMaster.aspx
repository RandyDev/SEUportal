<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UnauthorizedNoMaster.aspx.vb" Inherits="DiversifiedLogistics.UnauthorizedNoMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>Diversified Logistics</title>
	<telerik:RadStyleSheetManager  runat="server" ID="RadStyleSheet1" />
	<link href="../styles/styles.css" rel="stylesheet" type="text/css" />

   <script type="text/javascript">

       function pageLoad() {
       }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
<%--************************************************************************--%>
<%--************************************************************************--%>

		<telerik:RadFormDecorator runat="server" ID="RadFormDecorator1" 
            DecoratedControls="Default,Zone" />
<br /><br /><br />
       <div style="width:464px;margin:auto;text-align:center;border:1px solid red; padding:0px 12px 0px 12px;">
         <h2 style="color:firebrick">Unauthorized Access</h2>
     <p>
          You have attempted to access a page that you are not authorized to view,<br />
         or is missing required parameters.
     </p>
     <p style="text-align:left">
          If you have any questions, please contact the site administrator.
     </p>

<p style="text-align:left">         Please provide the following information:
<ul style="text-align:left;">
          <li>Your current IP Address:<asp:Label ID="IEEPEE" runat="server">
          </asp:Label> <asp:Label ID="lblIP" runat="server"/></li>
          <li>Current date and time: <asp:Label ID="lblTime" runat="server" /> CST</li>
</ul></p>
    </div>

    </form>
</body>
</html>

