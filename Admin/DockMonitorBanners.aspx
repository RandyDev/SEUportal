<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DockMonitorBanners.aspx.vb" Inherits="DiversifiedLogistics.DockMonitorBanners" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

<style type="text/css">

    #wrapper {
    width=100%;
    }
    #divcbLocations {
    width:159px;
    }
    #divEditor {
    }
    .auto-style1 {
        color: #FF0000;
    }
</style>



</head>
<body>
  <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager" runat="server"></telerik:RadScriptManager>
    <div id="wrapper">
        <table>
            <tr>
                <td style="vertical-align:top">
                    <table>
                        <tr>
                            <td>Location: <telerik:RadLabel ID="lblerr" Visible="false" ForeColor="Red" runat="server" /><br />
                                <telerik:RadComboBox ID="cbLocations" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Sort A-Z: &nbsp; 
                                <telerik:RadComboBox ID="cbSort" Width="40" runat="server">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="A" />
                                        <telerik:RadComboBoxItem Text="B" />
                                        <telerik:RadComboBoxItem Text="C" />
                                        <telerik:RadComboBoxItem Text="D" />
                                        <telerik:RadComboBoxItem Text="E" />
                                        <telerik:RadComboBoxItem Text="F" />
                                        <telerik:RadComboBoxItem Text="G" />
                                        <telerik:RadComboBoxItem Text="H" />
                                        <telerik:RadComboBoxItem Text="I" />
                                        <telerik:RadComboBoxItem Text="J" />
                                        <telerik:RadComboBoxItem Text="K" />
                                        <telerik:RadComboBoxItem Text="L" />
                                        <telerik:RadComboBoxItem Text="M" />
                                        <telerik:RadComboBoxItem Text="N" />
                                        <telerik:RadComboBoxItem Text="O" />
                                        <telerik:RadComboBoxItem Text="P" />
                                        <telerik:RadComboBoxItem Text="Q" />
                                        <telerik:RadComboBoxItem Text="R" />
                                        <telerik:RadComboBoxItem Text="S" />
                                        <telerik:RadComboBoxItem Text="U" />
                                        <telerik:RadComboBoxItem Text="V" />
                                        <telerik:RadComboBoxItem Text="W" />
                                        <telerik:RadComboBoxItem Text="X" />
                                        <telerik:RadComboBoxItem Text="Y" />
                                        <telerik:RadComboBoxItem Text="Z" />
                                    </Items>

                                 </telerik:RadComboBox>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                Enabled: &nbsp; 
                                <telerik:RadCheckBox ID="checkEnabled" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnSave" Text="Save New Banner" runat="server"></telerik:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br /> <telerik:RadButton id="btnClear" Text ="CLEAR Editor" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center">
                                <br /> <telerik:RadButton id="btnDelete" Text ="DELETE Selected" runat="server" />
                                <br /><span style="color:darkred;font-family:'Segoe UI';">No Confirmation!</span>
                            </td>
                        </tr>

                    </table>
                </td>
                <td>
                    <br /><table><tr><td style="vertical-align:top;">NOTE:</td>
                        <td style="font-family:'Segoe UI'"> If there is a scroll bar in the editor, the<span class="auto-style1"> Banner will NOT fit</span>! <em>also</em> <u>DON&#39; FORGET to CENTER your content!</u>
                            <br />Use [LocationName] to inject Location Name and Use [ParentCompany] to inject Location&#39;s Parent Company<br />
                            Use [First] [Last] to inject Employee into Birthday Message </td></tr></table>                    
                <telerik:RadEditor ID="RadEditor1" runat="server" Width="865px" Height="120px" style="color: #FF3300">
                    <ImageManager EnableAsyncUpload="true" ViewPaths="~/images/banners" UploadPaths="~images/banners" DeletePaths ="~/images/banners" />
                    <Tools>

                       <telerik:EditorToolGroup>
                           <telerik:EditorTool Name="Cut"/>
                           <telerik:EditorTool Name="Copy"/>
                           <telerik:EditorTool Name="Paste"/>
                       </telerik:EditorToolGroup>
                       <telerik:EditorToolGroup>
                           <telerik:EditorTool Name ="JustifyCenter" />
                           <telerik:EditorTool Name="FontName"/>
                           <telerik:EditorTool Name="FontSize"/>
                           <telerik:EditorTool Name="ForeColor"/>
                           <telerik:EditorTool Name="BackColor"/>
                           <telerik:EditorTool Name = "ColorPicker" />
                           <telerik:EditorTool Name = "FormatPainter" />
                           <telerik:EditorTool Name="Bold"/>
                           <telerik:EditorTool Name="Italic" />
                           <telerik:EditorTool Name = "StrikeThrough" />
                           <telerik:EditorTool Name="Underline" />
                       </telerik:EditorToolGroup>
                       <telerik:EditorToolGroup>
                           <telerik:EditorTool Name = "ImageManager" />
                           <telerik:EditorTool Name = "InsertDate" />
                           <telerik:EditorTool Name = "TableWizard" />
                       </telerik:EditorToolGroup>
                    </Tools>
                </telerik:RadEditor><br />
                    SELECT Banner to EDIT:<br />
                    <telerik:RadGrid ID="gridBanners" runat="server" CellSpacing="-1" DataSourceID="BannerDataSource" GridLines="Both">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                        <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                            <Selecting AllowRowSelect="True" />
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        </ClientSettings>
                        <MasterTableView AutoGenerateColumns="False" DataSourceID="BannerDataSource" DataKeyNames="BannerID" GridLines="None">
                            <Columns>
<%--                                <telerik:GridButtonColumn ImageUrl="~/images/redX.gif" ConfirmDialogWidth="495px"  ConfirmDialogHeight="115px" 
                                    ConfirmTitle="DELETE BANNER!" ConfirmText="DELETE Selected Banner?" ConfirmDialogType="RadWindow" 
                                    ButtonType="ImageButton" CommandName="Delete" Text="Delete" UniqueName="DeleteColumn">
                                  <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    <HeaderStyle Width="45px" />
                                </telerik:GridButtonColumn>--%>
                                <telerik:GridBoundColumn DataField="BannerID" DataType="System.Guid" FilterControlAltText="Filter BannerID column" HeaderText="BannerID" SortExpression="BannerID" UniqueName="BannerID" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="LocationID" DataType="System.Guid" FilterControlAltText="Filter LocationID column" HeaderText="LocationID" SortExpression="LocationID" UniqueName="LocationID" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="LocationName" FilterControlAltText="Filter LocationName column" HeaderText="LocationName" SortExpression="LocationName" UniqueName="LocationName" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SortOrder" HeaderText="Sort" SortExpression="SortOrder" UniqueName="SortOrder">
                                    <HeaderStyle Width="45px" />

                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="Enabled" DataType="System.Boolean" HeaderText="Show" UniqueName="Enabled">
                                    <HeaderStyle Width="45px" />
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridBoundColumn DataField="Banner" HeaderText="Banner" UniqueName="Banner">
                                    <HeaderStyle Width="860px" HorizontalAlign="center" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>

                    <asp:SqlDataSource ID="BannerDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:rtdsConnectionString %>" SelectCommand="SELECT BannerID, LocationID, LocationName,SortOrder,Enabled, Banner FROM DockMonitorBanners WHERE LocationID = @locaID Order By SortOrder">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="cbLocations" Name="locaID" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                </td>
            </tr>

            <tr>
                <td style="vertical-align:top">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>

        </table>
    </div>
  </form>
</body>
</html>
