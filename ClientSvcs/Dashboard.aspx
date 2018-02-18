<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Dashboard.aspx.vb" Inherits="DiversifiedLogistics.Dashboard" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>




<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=11.1.17.614, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en-US">
<head id="Head1" runat="server">
<title></title>
    <!-- custom head section -->
</head>
<body class="BODY">
    <!-- end of custom head section -->
    <form runat="server" id="mainForm" method="post">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <!-- content start -->
<div class="bigModule">
</div> 
        <!-- content end -->
     <telerik:ReportViewer runat="server" ID="reportviewer1" Width="100%" Height="700px" BorderStyle="None">
    </telerik:ReportViewer>
    </form>
</body>
</html>
