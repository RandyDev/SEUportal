﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="JobDescriptionList.aspx.vb" Inherits="DiversifiedLogistics.JobDescriptionList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
        <div>
            <strong>Job Descriptions Master List  </strong>&nbsp;
            <br />
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
                        <telerik:GridBoundColumn DataField="JobDescription" FilterControlAltText="Filter JobDescription column" HeaderText="JobDescription" SortExpression="JobDescription" UniqueName="JobDescription" />
                        <telerik:GridBoundColumn DataField="ID" Visible="false" ReadOnly="true" DataType="System.Guid" UniqueName="ID" />
                        <telerik:GridCheckBoxColumn DataField="IsHourly" DataType="System.Boolean" FilterControlAltText="Filter IsHourly column" HeaderText="IsHourly" SortExpression="IsHourly" UniqueName="IsHourly">
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridCheckBoxColumn DataField="IsActive" DataType="System.Boolean" FilterControlAltText="Filter IsActive column" HeaderText="IsActive" SortExpression="IsActive" UniqueName="IsActive">
                        </telerik:GridCheckBoxColumn>
                    </Columns>
            <EditFormSettings ColumnNumber="1" CaptionFormatString="Edit name"
                InsertCaption="New JobDescription">
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