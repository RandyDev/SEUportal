<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="signin.aspx.vb" Inherits="DiversifiedLogistics.signin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>Diversified Logistics</title>
	<telerik:RadStyleSheetManager runat="server"  ID="RadStyleSheet1" />
	<link href="../styles/styles.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        if (self != top) { top.location.replace(location); }

   
    </script>

</head>
<body>
    <form id="form1" runat="server">
    	<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
		<Scripts>
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
		</Scripts>
    </telerik:RadScriptManager>
<%--************************************************************************--%>
<%--************************************************************************--%>

		<telerik:RadFormDecorator runat="server" ID="RadFormDecorator1" 
            DecoratedControls="Default,Zone" />
				<div id="divheader">
					<div class="logo" ><img src="../Images/spacer.gif" width="25" alt="" />
   <asp:HyperLink ID="mstLogo" NavigateUrl="~/default.aspx" ImageUrl="~/images/logo.png" ToolTip="Click to Home Page" runat="server" />
							
					</div>
   				</div>
<div style="float:right;padding-right:20px;"><a href="ClientSvcs/DuplicateReceipts.aspx" title="Must know Date, PO# and Amount">Self-Serve Duplicate Receipts</a></div>

<br /><br /><br /><br /><br /><br /><br />
<center>
<%--	<asp:Login ID="bpLogIn" runat="server" DisplayRememberMe="true" RememberMeSet="true">
		<LayoutTemplate>--%>

<asp:Panel ID="pnlLogin" runat="server" DefaultButton="LoginButton">
        <table border="0"  
            style="border-collapse:collapse;">
            <tr>
                <td style="padding:7px;">
                    <table border="0" style=" border:1px solid black;">
                        <tr>
                            <td align="center" colspan="2" class="LoginHeader">
                                <asp:Label ID="lblHeader" Text="Please Log In" runat="server" />
                            </td>
                      </tr>
                        <tr>
                            <td colspan="2" style="height:4px; font-size:11px;"><asp:Label ID="lblQuestion" runat="server" /></td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-top:8px;">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName"><asp:Label ID="lblUserName" Text="Login ID: " runat="server" /> </asp:Label>&nbsp; 
                            </td>
                            <td style="padding-top:8px;"><asp:TextBox ID="UserName" runat="server" Font-Size="0.8em" /> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                    ToolTip="User Name is required." ValidationGroup="Login2" Text="*" />
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                    ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                    ToolTip="User Name is required." ValidationGroup="Login1" Text="*" />
                            </td>
                        </tr>
<tr>
                            <td align="right" style="padding-top:12px;">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Text="Password: "></asp:Label></td><td style="padding-top:12px;"><asp:TextBox ID="Password" runat="server" Font-Size="0.8em" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                    ControlToValidate="Password" ErrorMessage="Password is required." 
                                    ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator></td></tr><tr>
                            <td align="left" colspan="2">
                                <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table cellpadding="0" width="100%" cellspacing="0"><tr>
                                    <td align="left"><asp:LinkButton ID="lbtnForgotPassword" Text="forgot password" CssClass="lilBlueButton" CommandName="forgotPassword" runat="server" ValidationGroup="Login2"/></td>
                                    <td align="right">
                                        <asp:Button ID="LoginButton" runat="server" BackColor="#FFFBFF" 
                                            BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CommandName="Login" 
                                            Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" Text="Log In" 
                                            ValidationGroup="Login1" />
                                    </td>
                                </tr></table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <asp:Label ID="rpInstructions" runat="server" Visible="false">
            <br />
            Enter the correct Case SensitivE response to your security question above and click the <u>Show my password</u> link.<br />
Or<br />Use the button below to have your password eMailed to you.<br />
        </asp:Label><span style="color:Red;"><asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal></span><br /><asp:Button ID="btnSendNewPassword" runat="server" Text="Yes" Visible="false" />&nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="btnNoPass" runat="server" Text=" No " Visible="false" /> 


</asp:Panel>
	
<%--		</LayoutTemplate>
	</asp:Login>--%>
</center>


    </form>




    </body>
    </html>