<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ParentCompanyList.aspx.vb" Inherits="DiversifiedLogistics.ParentCompanyList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style type="text/css">
        .MyImageButton
        {
            cursor: hand;
        }
        .EditFormHeader td
        {
            font-size: 14px;
            padding: 4px !important;
            color: #0066cc;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            function gridCreated(sender, args) {
            }
 
        </script>
    </telerik:RadCodeBlock>

<div id="divGrid" style="width:475px;">
<telerik:RadGrid ID="RadGrid1" GridLines="None" runat="server" PageSize="25" 
    AllowPaging="true" AutoGenerateColumns="false" >
    <PagerStyle Mode="NextPrevAndNumeric" />
    <MasterTableView Width="100%" CommandItemDisplay="TopAndBottom"  DataKeyNames="ID" 
        HorizontalAlign="NotSet" AutoGenerateColumns="false">
        <Columns>
            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                <ItemStyle CssClass="MyImageButton" />
            </telerik:GridEditCommandColumn>

            <telerik:GridBoundColumn DataField="Name" HeaderText="Company Name" 
                UniqueName="Name" ColumnEditorID="GridTextBoxColumnEditor1">
            </telerik:GridBoundColumn>

            <telerik:GridBoundColumn DataField="ID" 
                ReadOnly="true" HeaderText="ID" UniqueName="ID">
            </telerik:GridBoundColumn>
            </Columns>
            <EditFormSettings ColumnNumber="1" CaptionFormatString="Edit name"
                InsertCaption="New Parent Company">
                <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3" BackColor="White"
                    Width="100%" />
                <FormTableStyle CellSpacing="0" CellPadding="2" Height="50px" BackColor="White" />
                <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                <EditColumn ButtonType="ImageButton" InsertText="Insert New Parent Company" UpdateText="Update record"
                    UniqueName="EditCommandColumn1" CancelText="Cancel edit">
                </EditColumn>
                <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <ClientEvents OnRowDblClick="RowDblClick" OnGridCreated="gridCreated" />
        </ClientSettings>
    </telerik:RadGrid>
    <telerik:GridTextBoxColumnEditor ID="GridTextBoxColumnEditor1" runat="server" TextBoxStyle-Width="200px" />

</div>
    </div>
<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
<script type="text/javascript">
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
</telerik:RadScriptBlock>    

    </form>
</body>
</html>
