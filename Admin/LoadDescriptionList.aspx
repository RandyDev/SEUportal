<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoadDescriptionList.aspx.vb" Inherits="DiversifiedLogistics.LoadDescriptionList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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

            <strong>Load Description Master List  </strong>&nbsp;
            <br />
            <telerik:RadGrid ID="RadGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellSpacing="0"  GridLines="None" width="400px">
                <ClientSettings AllowColumnsReorder="True" EnableRowHoverStyle="true" ReorderColumnsOnClient="True">
                </ClientSettings>
                <MasterTableView AllowSorting="true" CommandItemDisplay="Top"  DataKeyNames="ID" EditMode="InPlace">
                    <Columns>
            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                <ItemStyle CssClass="MyImageButton" />
            </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="ID" DataType="System.Guid" Visible="false" FilterControlAltText="Filter ID column" HeaderText="ID" ReadOnly="true" SortExpression="ID" UniqueName="ID" />
                        <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter Name column" HeaderText="Name" SortExpression="Name" UniqueName="Name" />
                        <telerik:GridCheckBoxColumn DataField="InActive" DataType="System.Boolean" FilterControlAltText="Filter InActive column" HeaderText="InActive" SortExpression="InActive" UniqueName="InActive">
                        </telerik:GridCheckBoxColumn>
                    </Columns>
            <EditFormSettings ColumnNumber="1" CaptionFormatString="Edit name"
                InsertCaption="New JobTitle">
                <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3" BackColor="White"
                    Width="100%" />
                <FormTableStyle CellSpacing="0" CellPadding="2" Height="50px" BackColor="White" />
                <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                <EditColumn ButtonType="ImageButton" InsertText="Insert New Load Description" UpdateText="Update record"
                    UniqueName="EditCommandColumn1" CancelText="Cancel edit">
                </EditColumn>
                <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
            </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
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
