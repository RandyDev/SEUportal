<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="DiversifiedLogistics._Default1" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

 <script type="text/javascript" src="http://support.div-log.com/JavaScript.ashx?fileMask=Optional/ChatScripting"></script>
     

<script language="javascript" type="text/javascript">
    (function () {
        var c = document.createElement('script');
        c.type = 'text/javascript'; c.async = true;
        c.src = "http://support.div-log.com/ChatLink.ashx?config=3&id=stlivechat0";
        var s = document.getElementsByTagName('script')[0];
        s.parentNode.insertBefore(c, s);
    })();
</script>


    <style type="text/css">
        .style1
        {
            text-decoration: underline;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
<br />
<table style="border:1px solid #888888;" align="center" width="700">
    <tr>
        <td>
<br />
<table align="center" style="font-family:Arial; font-size:14px;" width="675">
    <tr>
        <td style="text-align:center;">
            <asp:Image ID="imgLogo" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
Welcome to the custom <span class="style1">SEU - </span> 
            <asp:Label ID="lblParentCompanyName" runat="server" CssClass="style1" /> landing page. &nbsp;As we work on developing 
this custom page into a useful dash-board, we will gladly welcome any comments or suggestions you may have as to the type of content, controls and features you would like to see here.
<br /><br />
In the mean-time, we have a variety of reports available to include a real-time Dock Activity report, Production and Vendor reports, 
 several Client reports and more.<br />
            <br />
            <center>All avaliable by hovering your mouse over 'Reports' in the menu at the top of the page.</center>
        </td>
    </tr>
    <tr>
        <td style="text-align:center;">
        <br /><br />
           <div id="stlivechat0"></div>
<br /><br />
      <span style="font-family:Arial; font-size:13px;"> If you find our Live Chat is OffLine,
            <br />
you can click the button and leave a message with our support staff.<br />
            (<em>this will open in a new window or tab</em>)</span>

        </td>
    </tr>

</table>
<br /><br />

        </td>
    </tr>
</table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" Interval="3000" OnTick="Timer1_Tick">
                </asp:Timer>
                <telerik:RadChart ID="Radchart2" runat="server" Height="370px" Width="570px"
                    EnableViewState="False" Visible="false">
                    <Series>
                        <telerik:ChartSeries Name="Sales" DefaultLabelValue="#ITEM">
                            <Items>
                                <telerik:ChartSeriesItem Name="Beverages" YValue="10000">
                                </telerik:ChartSeriesItem>
                                <telerik:ChartSeriesItem Name="Produce" YValue="7500">
                                </telerik:ChartSeriesItem>
                                <telerik:ChartSeriesItem Name="Poultry" YValue="9000">
                                </telerik:ChartSeriesItem>
                                <telerik:ChartSeriesItem Name="Grains" YValue="11200">
                                </telerik:ChartSeriesItem>
                            </Items>
                        </telerik:ChartSeries>
                    </Series>
                </telerik:RadChart>
            </ContentTemplate>
        </asp:UpdatePanel>





        <telerik:RadTicker ID="RadTicker1" runat="server">
        </telerik:RadTicker>
        <telerik:RadRotator ID="RadRotator1" runat="server">
        </telerik:RadRotator>

<asp:Label ID="lblSiteStaff" runat="server" />




    </div>
    </form>
</body>
</html>
