<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DepartmentsList.aspx.vb" Inherits="DiversifiedLogistics.DepartmentsList" %>

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
            <br />
            <strong>Departments Master List  </strong>&nbsp;
            <telerik:RadGrid ID="RadGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellSpacing="0"  GridLines="None"  width="400">
                <ClientSettings AllowColumnsReorder="True" EnableRowHoverStyle="true" ReorderColumnsOnClient="True">
                    <Selecting CellSelectionMode="None" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                </ClientSettings>
                <MasterTableView AllowSorting="true" CommandItemDisplay="Top" DataKeyNames="ID" EditMode="InPlace" width="350px">
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                    </ExpandCollapseColumn>
                    <Columns>
            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                <ItemStyle CssClass="MyImageButton" />
            </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter JobTitle column" HeaderText="Name" SortExpression="NameJobTitle" UniqueName="Name" />
                        <telerik:GridBoundColumn DataField="ID" Visible="false" ReadOnly="true" DataType="System.Guid" FilterControlAltText="Filter JobTitleID column" HeaderText="JobTitleID" SortExpression="JobTitleID" UniqueName="JobTitleID" />
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
            </telerik:RadGrid>
    
    </div>
    </form>
</body>
</html>
