<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClientUserAdmin.aspx.vb" Inherits="DiversifiedLogistics.ClientUserAdmin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style type="text/css">
.errLabel {
    color:Red;
    font-size:12px;
    font-weight:bold;
}
.errMsgRed{
    color:Red;
    font-size:12px;
}
body {
    font-family:Arial;
}
.tdText {
    font:11px Verdana;
	color:#333333;
}

a {
	font:11px Verdana;
	color:#315686;
	text-decoration:underline;
}
a:hover {
	color:Red;
}
.rowEditTable {
	background-color: #EEEEEE;
	border: 1px solid #000000;
}
.rowEditTable td {
	font-family: Verdana;
	font-size: 10px;
	color: #000000;		        
}

.rbsmall{
    font-size: 11px;
}
.lblUserNam {
    font-size:13px;
    font-weight:bold;
    padding-left:8px;
}
	</style>
</head>
<body>
<form id="form1" runat="server">
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" EnableEmbeddedSkins="true" Skin="Simple" />
    <div>
     <telerik:RadScriptBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">

    function validateUserName(source, arguments) {
        if (arguments.Value.length < 3) {
            arguments.IsValid = false;
        } else {
            arguments.IsValid = true;
        }
    }

    function validatePassword(source, arguments) {
        var vpw = arguments.Value;

        if (vpw.length < 4) {
            arguments.IsValid = false;
        } else {
            arguments.IsValid = true;
        }
    }
</script>
    </telerik:RadScriptBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rbSelectRole">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" 
                        LoadingPanelID="LoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="editUser" />
                    <telerik:AjaxUpdatedControl ControlID="eusr" LoadingPanelID="LoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnDelete">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" 
                        LoadingPanelID="LoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="editUser" />
                    <telerik:AjaxUpdatedControl ControlID="eusr" LoadingPanelID="LoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSubmit">
                <Updatedcontrols>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="lblerrtxtFname" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblerrtxtLname" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblerrtxtEmail" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblerrtxtUserName" />
                    <telerik:AjaxUpdatedControl ControlID="txtUserName" />
                    <telerik:AjaxUpdatedControl ControlID="lblUserName" />
                    <telerik:AjaxUpdatedControl ControlID="txtPassword" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lbtnResetPassword" UpdatePanelHeight="" />

                   <telerik:AjaxUpdatedControl ControlID="btnSubmit" LoadingPanelID="LoadingPanel1" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblResponse" UpdatePanelHeight="" />
                </Updatedcontrols>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCancel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lbLocationList" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lbAvailableLocations" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="btnCancel" LoadingPanelID="LoadingPanel1" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblResponse" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="editUser" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="UsersRoleList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlLocations" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="pnlClientList" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cbClientList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lbAvailableLocations" UpdatePanelHeight="" LoadingPanelID="LoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>



    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="LoadingPanel1" Runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

<table>
	<tr>
		<td width="25%" valign="top">  <%-- left side user grid and filter--%>
		<div style="margin:0px 20px;">
            <asp:RadioButtonList AutoPostBack="true" CssClass="rbsmall" ID="rbSelectRole" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Text="Show All" />
            <asp:ListItem Text="Client" />
            <asp:ListItem Text="Vendor" />
            <asp:ListItem Text="Carrier" />
            <asp:ListItem Text="Guest" />
            </asp:RadioButtonList>
            <span id="GridInstructions" style="font-size:11px;" runat="server">Click user name to edit and/or remove lockout</span>
        
<telerik:RadGrid ID="RadGrid1" runat="server" ClientSettings-Scrolling-AllowScroll="true"  Height="500px" 
    AutoGenerateColumns="False" GridLines="None">
<HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
<MasterTableView DataKeyNames="userName">
<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
    <Columns>
        <telerik:GridBoundColumn DataField="pid" DataType="System.Guid" 
            HeaderText="pid" ReadOnly="True" SortExpression="pid" UniqueName="pid" 
            Visible="False">
        </telerik:GridBoundColumn>
        <telerik:GridTemplateColumn DataField="lastName" HeaderText="Name : (LoginID)" 
            SortExpression="lastName" UniqueName="lastName" HeaderStyle-Width="215px">
            <ItemTemplate>
                <%#String.Format("{1}, {0} : ({2})", Eval("firstName"), Eval("lastName"), Eval("userName"))%>
            </ItemTemplate>
        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn DataField="LastLoginDate" HeaderText="Last Login" 
            SortExpression="LastLoginDate" UniqueName="LastLoginDate" HeaderStyle-Width="125px">
            <ItemTemplate>
            <%# Format(Eval("LastLoginDate"), "dd-MMM-yyyy ddd")%>
            </ItemTemplate>
        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn DataField="isOnline" HeaderText="isOnline" 
            SortExpression="isOnline" UniqueName="isOnline" HeaderStyle-Width="50px">
            <ItemTemplate>
                <%# IIf(Eval("isOnline").ToString = "False", "", "Yes")%>
            </ItemTemplate>
        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn DataField="IsLockedOut" HeaderText="Locked Out" DataType="System.Boolean"
            ShowSortIcon="false" HeaderStyle-Width="75px" UniqueName="isLockedOut">
            <ItemTemplate>
                <%#IIf(Eval("isLockedOut").ToString = "False", "", "Locked&nbsp;Out")%>
            </ItemTemplate>
        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn DataField="IsApproved" HeaderText="InAct" DataType="System.Boolean"
            ShowSortIcon="false" HeaderStyle-Width="75px" UniqueName="IsApproved">
            <ItemTemplate>
                <%# IIf(Eval("isApproved").ToString = "True", "", "InActive")%>
            </ItemTemplate>
        </telerik:GridTemplateColumn>
    </Columns>
</MasterTableView>
<ClientSettings enablepostbackonrowclick="true" EnableRowHoverStyle="true" >
    <Selecting AllowRowSelect="true" />
    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
</ClientSettings>
                </telerik:RadGrid>
			</div>
		</td>
		<td valign="top">  <%-- Right Side Edit form--%>

 <div id="editUser" runat="server">
<div id="frmHeader" style="margin:0px;" runat="server">
<table cellpadding="0" cellspacing="0">
    <tr>
        <td><h3><asp:Label ID="lblEditType" Text="Add New" runat="server"/> User</h3></td>
        <td align="right"><span id="Span1" style="padding-left:50px;" runat="server">
            <asp:Button Visible="false" id="btnDelete" Text="Delete This User" runat="server"  /></span>
        </td>
    </tr>
</table>
</div>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td>   <%-- Edit Form Left Side (form)--%>


<div id="eusr" runat="server">

<table class="rowEditTable">
	<tr>
	    <td>
            <table cellspacing="0" cellpadding="0"><tr><td>First Name: </td><td><asp:Label ID="lblerrtxtFname" CssClass="errMsgRed" Text="*" runat="server" /></td></tr></table>
            <asp:TextBox ID="txtFname" Width="75px" runat="server" />
        </td>
        <td>
            <table cellspacing="0" cellpadding="0"><tr><td>Last Name: </td><td align="left"><asp:Label ID="lblerrtxtLname" CssClass="errMsgRed" Text="*" runat="server" /></td></tr></table>
            <asp:TextBox ID="txtLname" Width="100px" runat="server" />
        </td>
		<td>
            Title:
            <br />
            <telerik:RadComboBox ID="cbTitle" runat="server" Filter="Contains" Width="160px" AllowCustomText="true" />
        </td>
    </tr>
    <tr>
		<td colspan="2">
            <table cellspacing="0" cellpadding="0"><tr><td>eMail Address: </td><td align="left"><asp:Label ID="lblerrtxtEmail" CssClass="errMsgRed" Text="*" runat="server" /></td></tr></table>
            <asp:TextBox ID="txtEmail" Width="185px" runat="server" />
        </td>
        <td>
            Phone:
            <br />
            <asp:TextBox ID="txtPhone" Width="95px" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            Cell Phone Address: 
            <br />
            <telerik:RadTextBox EmptyMessage="ex: CellNumber@txt.att.net" Width="185px" ID="txtCellText" runat="server" />
        </td>
        <td>
            <-- (for TEXT messages)<br /><-- [optional] 
        </td>
    </tr>
    <tr>
	    <td valign="top">
            <table cellspacing="0" cellpadding="0"><tr><td>LoginID: </td><td align="left"><asp:Label ID="lblerrtxtUserName" CssClass="errMsgRed" Text="*" runat="server" /></td></tr></table>
			<asp:TextBox ID="txtUserName" Width="75px"  runat="server"/>
            <asp:label ID="lblUserName" CssClass="lblUserNam" runat="server" Visible="false"></asp:label>
		</td>
		<td> Password:
            <br /><asp:TextBox ID="txtPassword" Width="100px" runat="server"></asp:TextBox>
            <asp:LinkButton ID="lbtnResetPassword" runat="server" CommandName="ResetPassword" CssClass="lilBlueButton" Text="Reset Password" />
		</td>
        <td style="padding-left:10px;"><asp:CheckBox ID="chkIsApproved" runat="server" Text="is Approved" />
        </td>
    </tr>
    <tr>
        <td colspan="3"><hr />

<asp:Panel ID="pnlLocations" runat="server" >
<table>
    <tr>
        <td><br />
            <telerik:RadListBox ID="lbAvailableLocations" runat="server" DataKeyField="locaID" AllowTransfer="True"
                TransferMode="Move" EnableDragAndDrop="True" TransferToID="lbLocationList" 
                Height="150px" Width="175px" ButtonSettings-ShowTransferAll="True" ButtonSettings-HorizontalAlign="Left"
                ButtonSettings-ShowTransfer="true" ButtonSettings-ShowReorder="False"
                EmptyMessage="All locations have been selected" 
                ButtonSettings-ShowDelete="False" >
            </telerik:RadListBox>
        </td>
        <td>Allow access these Locations<br />
            <telerik:RadListBox ID="lbLocationList"  
                runat="server" DataKeyField="locaID"
                EmptyMessage="<<-- None listed, move locations from list at left"  
                Width="175px" Height="150px" AllowDelete="true" EnableDragAndDrop="true"  
                ButtonSettings-ShowReorder="True" AllowReorder="True" ButtonSettings-HorizontalAlign="Left"
                ButtonSettings-ShowDelete="True" >
           </telerik:RadListBox>
        </td>
    </tr>
</table>
</asp:Panel>
        </td>
    </tr>
	<tr>
		<td align="center" colspan="3">
			<table width="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td width="50%" align="center">
            			<asp:Button Visible="true" id="btnSubmit" Text="Add User" CommandName="Insert" runat="server" />
					</td>
					<td width="50%" align="center">
                        <asp:Button ID="btnCancel" Text="Clear/Cancel" runat="server"  />
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
</div>


        </td>
        <td rowspan="2" valign="top">  <%-- Edit Form Right Side (Role Selectors)--%>
            <asp:Repeater ID="UsersRoleList" runat="server"> 
				<ItemTemplate> 
					<asp:CheckBox runat="server" ID="RoleCheckBox" AutoPostBack="true" OnCheckedChanged="chkLocaList" 
						Text='<%# Container.DataItem %>'/><br />
				</ItemTemplate> 
			</asp:Repeater> 
        </td>
<td valign="top">
<asp:Panel ID="pnlClientList" runat="server" Visible="false">
<telerik:RadComboBox ID="cbClientList" runat="server" Filter="Contains" EmptyMessage="Select Client" AutoPostBack="true" AllowCustomText="true" />
</asp:Panel>
</td>
    </tr>
</table>
  &nbsp; &nbsp; <asp:Label ID="lblResponse" CssClass="errMsgRed" runat="server" />
</div>

		</td>
	</tr>
</table>
    </div>
</form>
</body>
</html>
