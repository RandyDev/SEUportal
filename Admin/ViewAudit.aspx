<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="ViewAudit.aspx.vb" Inherits="DiversifiedLogistics.ViewAudit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" EnableEmbeddedSkins="true" Skin="Simple" />
    <div>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnShowRange">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnDeleteSelected" 
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" 
                        UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnDeleteSelected">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" 
                        LoadingPanelID="RadAjaxLoadingPanel2" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" 
                        LoadingPanelID="RadAjaxLoadingPanel2" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxLoadingPanel Transparency="50" ID="RadAjaxLoadingPanel2"  runat="server" Width="210px" Height="210px">
 <asp:Image id="Image1" runat="server" Width="200px" Height="200px" ImageUrl="~/images/rogersFish.gif"></asp:Image>
    </telerik:RadAjaxLoadingPanel>


<table width="100%"><tr>
<td>
        <telerik:RadDatePicker ID="RadDatePicker1" runat="server">
        </telerik:RadDatePicker>
        <telerik:RadDatePicker ID="RadDatePicker2" runat="server">
        </telerik:RadDatePicker>
<telerik:RadTextBox ID="txtFilter" EmptyMessage="TableName/FieldName Filter" runat="server" />
<asp:Button ID="btnShowRange" Text="Show Selected Range" runat="server" />
</td>
<td align="right">
<asp:Button ID="btnDeleteSelected" Text="Delete Selected Records" runat="server" />
</td>
</tr></table>
    <telerik:RadGrid ID="RadGrid1" runat="server" AllowSorting="True" GroupingEnabled="true" Height="600px"
        ShowGroupPanel="True" AllowMultiRowSelection="true">
<ClientSettings EnableRowHoverStyle="true" AllowDragToGroup="true" >
    <Selecting AllowRowSelect="True" />
    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
    <ClientEvents OnRowClick="OpenWorkOrder" />
</ClientSettings>
 <ExportSettings FileName="DivLogAuditTable" HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true" />

<MasterTableView AutoGenerateColumns="False" DataKeyNames="AuditID" CommandItemDisplay="Bottom" 
             ClientDataKeyNames="PK">
<CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="true"  ExportToExcelText="Export to Excel"></CommandItemSettings>

    <Columns>
         <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn">
            <HeaderStyle Width="35px" />
         </telerik:GridClientSelectColumn>
        <telerik:GridBoundColumn DataField="AuditID" DataType="System.Guid" 
            HeaderText="AuditID" ReadOnly="True" SortExpression="AuditID" 
            UniqueName="AuditID" Visible="false">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Type" HeaderText="Type" 
            SortExpression="Type" UniqueName="Type" Visible="false">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="PK" HeaderText="Record ID" SortExpression="PK" 
            UniqueName="PK">
            <HeaderStyle Width="250px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="TableName" HeaderText="TableName" 
            SortExpression="TableName" UniqueName="TableName">
            <HeaderStyle Width="100px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="FieldName" HeaderText="FieldName" 
            SortExpression="FieldName" UniqueName="FieldName">
            <HeaderStyle Width="100px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="OldValue" HeaderText="OldValue" 
            SortExpression="OldValue" UniqueName="OldValue">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="NewValue" HeaderText="NewValue" 
            SortExpression="NewValue" UniqueName="NewValue">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="UpdateDate" DataType="System.DateTime" 
            HeaderText="UpdateDate" SortExpression="UpdateDate" UniqueName="UpdateDate">
            <HeaderStyle Width="150px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="UserName" HeaderText="UserName" 
            SortExpression="UserName" UniqueName="UserName">
            <HeaderStyle Width="125px" />
        </telerik:GridBoundColumn>
    </Columns>
 
</MasterTableView>
    </telerik:RadGrid>
<telerik:RadWindow ID="winWO" ShowContentDuringLoad="false" 
        VisibleStatusbar="false" AutoSize="true" runat="server" 
        Behaviors="Close, Pin, Move" Skin="Sunset" BackColor="White" 
        EnableShadow="True">
</telerik:RadWindow>
<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
<script type="text/javascript">
    function onRequestStart(sender, args) {

        if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToCsvButton") >= 0) {
            args.set_enableAjax(false);
        } else {
            if (args.get_eventTarget().indexOf("btnDeleteSelected") != -1) {
                var oWnd = $find("<%= winWO.ClientID %>");
                oWnd.hide();
                if (!window.confirm("Yea well, you mashed the 'Delete Selected Records' button,\nand I'm sure you meant to, but still ...\nwe're required to offer the obligitory:\nAre you SURE you want to delete the selected record(s)?"))
                    return false;
            } 
        }
    }
  

    function OpenWorkOrder(sender, args) {
        var oWnd = $find("<%= winWO.ClientID %>");
        oWnd.setUrl("showWO.aspx?q=" + args.getDataKeyValue("PK"));
        oWnd.show();
    }
</script>
</telerik:RadScriptBlock>

    </div>
    </form>
</body>
</html>
