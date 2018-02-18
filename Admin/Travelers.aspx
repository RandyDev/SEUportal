<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Travelers.aspx.vb" Inherits="DiversifiedLogistics.Travelers" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"><html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .lilBlue {
            color:Blue;
            font-size:11px;
            font-family:Arial;
        }
    </style>
    <script type="text/javascript">
        function travtadDef() {
        alert("'Temporary Assigned Duty' is typically a \ncross town 'help out at the other shop' type of assignment. \nLoad Money (%) IS paid on TAD!");
        }
    </script>
</head>
<body>
<form id="form2" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSubmit">
                <updatedcontrols>
                    <telerik:AjaxUpdatedControl ControlID="lbTravelPool" />
                    <telerik:AjaxUpdatedControl ControlID="lbTravelTeam" />
                    <telerik:AjaxUpdatedControl ControlID="cbTravelLocations" />
                    <telerik:AjaxUpdatedControl ControlID="dpStartDate" />
                    <telerik:AjaxUpdatedControl ControlID="dpReturnDate" />
                    <telerik:AjaxUpdatedControl ControlID="btnSubmit" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="lblErrMessage" />
                </updatedcontrols>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid2" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid3" />  
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid3"
                       LoadingPanelID="RadAjaxLoadingPanel1" />

                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"></telerik:RadAjaxLoadingPanel>
 <div>
<table>
    <tr>
        <td valign="top">
            <table>
                <tr>
                    <td>
                        Location:<br />
                        <telerik:RadComboBox ID="cbLocations" Filter="Contains" AppendDataBoundItems="true" runat="server">
                            <Items>
                                <telerik:RadComboBoxItem Value="044E49DB-D27C-4752-85F5-3169FA8F5D3C" Text="Fernandina" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td valign="bottom">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td><asp:RadioButtonList ID="rblTravTad" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="Traveler" Text="Traveler" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="TAD" Text="TAD" Selected="False"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>&nbsp;&nbsp;&nbsp;<span class="lilBlue" onmouseover="this.style.cursor='pointer'" onclick="travtadDef()">What is TAD?</span></td>
                            </tr>
                        </table>
<%--                Travelers Requested: <asp:Label ID="lblTravelersRequested" runat="server" />--%> 
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td valign="top" rowspan="2">Employees:<br />
                        <telerik:RadListBox ID="lbTravelPool" runat="server" Width="175" ButtonSettings-TransferButtons="TransferFrom"  
                         TransferToID ="lbTravelTeam" AllowTransferDuplicates="false" TransferMode="Copy" AllowTransfer="true"
                         CheckBoxes="false" DataKeyField="ID" EmptyMessage="Select&nbsp;Location&nbsp; Above" SelectionMode="Multiple" 
                         ButtonSettings-ShowTransferAll="false"  AllowTransferOnDoubleClick="true" EnableDragAndDrop="true" ToolTip="Double Click or Drag Employees from here and drop in to 'Travelers' list" />
                    </td>
                    <td valign="top" rowspan="2">Travelers:<br />
                        <telerik:RadListBox ID="lbTravelTeam" EmptyMessage="Drag/Drop Employee(s) <br />onto this listbox"  runat="server"
                            AllowTransfer ="false" AllowTransferDuplicates="false" EnableDragAndDrop="true" 
                            DataKeyField="ID" SelectionMode="Multiple" AllowDelete ="true" ButtonSettings-Position="Left" ButtonSettings-ShowDelete="true"
                            ButtonSettings-ShowReorder="false" Height="160px" ButtonSettings-ShowTransferAll="false" />
                    </td>
                   <td valign="top">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>Destination:</td>
                            </tr>
                            <tr>
                                <td style="padding-left:7px;"><telerik:RadComboBox ID="cbTravelLocations" runat="server" EmptyMessage="Select Destination" /></td>
                            </tr>
                            <tr>
                                <td style="padding-top:5px;">Start Date:</td>
                            </tr>
                            <tr>
                                <td style="padding-left:7px;"><telerik:RadDatePicker ID="dpStartDate" Width="100px" runat="server" /></td>
                            </tr>
                            <tr>
                                <td style="padding-top:5px;">Return Date:</td>
                            </tr>
                            <tr>
                                <td style="padding-left:7px;"><telerik:RadDatePicker ID="dpReturnDate" Width="100px" runat="server" /></td>
                            </tr>
                            <tr>
                                <td style="padding-top:16px;" align="center"><asp:Button ID="btnSubmit" runat="server" Text="Submit" /></td>
                            </tr>
                        </table>

                   </td>
                </tr>
                <tr>
                    <td  valign="top"><asp:Label style="color:Red;" ID="lblErrMessage" runat="server" /></td>
                </tr>
            </table>
        </td>

        <td valign="top">
            Pending Assignments:<br />
            <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" 
                CellSpacing="0" GridLines="None" AllowSorting="True" ShowGroupPanel="True" >
                <ClientSettings AllowDragToGroup="True" EnableRowHoverStyle="true" ></ClientSettings>
                <MasterTableView DataKeyNames="travelID">
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="travelID" UniqueName="travelID" ReadOnly="true" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="rtdsEmployeeID" UniqueName="rtdsEmployeeID" ReadOnly="true" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="employeeName" UniqueName="employeeName" ReadOnly="true" HeaderText="Traveler Name" Visible="True"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="homeLocation" UniqueName="homeLocation" ReadOnly="true" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="homeLocaName" UniqueName="homeLocaName" ReadOnly="true" HeaderText="Home Location" Visible="True"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="travelLocation" UniqueName="travelLocation" ReadOnly="true" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="travelLocaName" UniqueName="travelLocaName"  ReadOnly="true" HeaderText="Travel Location" Visible="True"></telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn DataField="startDate" DataType="System.DateTime" HeaderText="Start" SortExpression="startDate" UniqueName="startDate">
                            <ItemTemplate>
                                <%# Format(Eval("startDate"), "MM/dd/yy")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadDatePicker runat="server" ID="dpStartDate" Culture="en-US" Width="110px"
                                    DbSelectedDate='<%# Eval("startDate") %>' />
                            </EditItemTemplate>
                       </telerik:GridTemplateColumn>
                       <telerik:GridTemplateColumn DataField="returnDate" DataType="System.DateTime" 
                            HeaderText="Return" SortExpression="returnDate" UniqueName="returnDate">
                            <ItemTemplate>
                                <%# IIf(Eval("returnDate") < Eval("startDate") Or Eval("returnDate") > "12/30/9999", " --- ", Format(Eval("returnDate"), "MM/dd/yy"))%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadDatePicker runat="server" ID="dpReturnDate" MaxDate="12/31/9999" MinDate="1/1/1900" Culture="en-US"  Width="110px"
                                    DbSelectedDate='<%# Eval("returnDate") %>'/>
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="salaryWeek" DataType="System.DateTime" 
                            HeaderText="Salary perWeeK" SortExpression="salaryWeek" UniqueName="salaryWeek">
                            <ItemTemplate>
                                <%# FormatCurrency(IIf(IsDBNull(Eval("salaryWeek")), 0, Eval("salaryWeek")) + IIf(IsDBNull(Eval("perDiemWeek")), 0, Eval("perDiemWeek")))%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadNumericTextBox runat="server" ID="numsalaryWeek"  Culture="en-US"  Width="75px"
                                    LabelWidth="64px" Type="Currency" DbValue='<%# Eval("salaryWeek") %>'/>
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="perDiemWeek" DataType="System.DateTime" Visible="false"
                            HeaderText="perDiem perWeeK" SortExpression="perDiemWeek" UniqueName="perDiemWeek">
                            <ItemTemplate>
                                <%# FormatCurrency(IIf(IsDBNull(Eval("perDiemWeek")), 0, Eval("perDiemWeek")))%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadNumericTextBox runat="server" ID="numperDiemWeek"  Culture="en-US" Enabled="false"
                                    LabelWidth="64px" Type="Currency"  Width="75px" DbValue='<%# Eval("perDiemWeek") %>'/> 
                                    %lt;--future use
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="loadMoney" HeaderText="isTAD" UniqueName="loadMoney" ReadOnly="true" Visible="True"></telerik:GridBoundColumn>
                        <telerik:GridButtonColumn ButtonType="LinkButton" CommandName="Edit" Text="Edit"
                            UniqueName="EditColumn" >
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                        <telerik:GridButtonColumn ConfirmText="Start This Assignment?" ConfirmDialogType="RadWindow"
                            ConfirmTitle="Begin Assignment" ButtonType="LinkButton" CommandName="BeginAssignment" Text="Begin Assignment"
                            UniqueName="BeginColumn" >
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                        <telerik:GridButtonColumn ConfirmText="Delete this Assignment?" ConfirmDialogType="RadWindow"
                            ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                            UniqueName="DeleteColumn">
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>

                    <EditFormSettings>
                    <EditColumn ></EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu EnableImageSprites="False"></FilterMenu>
                <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
            </telerik:RadGrid>
            <br />
            Active Assignments:<br />
            <telerik:RadGrid ID="RadGrid2" runat="server" AutoGenerateColumns="False" 
                CellSpacing="0" GridLines="None" AllowSorting="True" ShowGroupPanel="True" >
                <ClientSettings AllowDragToGroup="True"  EnableRowHoverStyle="true" ></ClientSettings>
                <MasterTableView DataKeyNames="travelID">
                    <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="travelID" UniqueName="travelID" ReadOnly="true" Visible="False">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="rtdsEmployeeID" UniqueName="rtdsEmployeeID" ReadOnly="true" Visible="False">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="employeeName" UniqueName="employeeName" ReadOnly="true" HeaderText="Traveler Name" Visible="True">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="homeLocation" UniqueName="homeLocation" ReadOnly="true" Visible="False">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="homeLocaName" UniqueName="homeLocaName" ReadOnly="true" HeaderText="Home Location" Visible="True">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="travelLocation" UniqueName="travelLocation" ReadOnly="true" Visible="False">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="travelLocaName" UniqueName="travelLocaName"  ReadOnly="true" HeaderText="Travel Location" Visible="True">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn DataField="startDate" DataType="System.DateTime" 
                            HeaderText="Start" SortExpression="startDate" UniqueName="startDate">
                            <ItemTemplate>
                                <%# Format(Eval("startDate"), "MM/dd/yy")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadDatePicker runat="server" ID="dpStartDate" Culture="en-US"  Width="110px"
                                    DbSelectedDate='<%# Eval("startDate") %>' />
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="returnDate" DataType="System.DateTime" 
                            HeaderText="Return" SortExpression="returnDate" UniqueName="returnDate">
                            <ItemTemplate>
                                <%# IIf(Eval("returnDate") < Eval("startDate") Or Eval("returnDate") > "12/30/9999", " --- ", Format(Eval("returnDate"), "MM/dd/yy"))%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadDatePicker runat="server" ID="dpreturnDate" MaxDate="12/31/9999" MinDate="1/1/1900" Culture="en-US"  Width="110px"
                                    DbSelectedDate='<%# Eval("returnDate") %>'/>
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="salaryWeek" DataType="System.DateTime" 
                            HeaderText="Salary perWeek" SortExpression="salaryWeek" UniqueName="salaryWeek">
                            <ItemTemplate>
                                <%# FormatCurrency(IIf(IsDBNull(Eval("salaryWeek")), 0, Eval("salaryWeek")) + IIf(IsDBNull(Eval("perDiemWeek")), 0, Eval("perDiemWeek")))%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadNumericTextBox runat="server" ID="numsalaryWeek"  Culture="en-US" 
                                    LabelWidth="64px" Type="Currency"  Width="75px" DbValue='<%# Eval("salaryWeek") %>'/>
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="perDiemWeek" DataType="System.DateTime" Visible="false"
                            HeaderText="perDiem perWeeK" SortExpression="perDiemWeek" UniqueName="perDiemWeek">
                            <ItemTemplate>
                                <%# FormatCurrency(IIf(IsDBNull(Eval("perDiemWeek")), 0, Eval("perDiemWeek")))%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadNumericTextBox runat="server" ID="numperDiemWeek"  Culture="en-US" Enabled="false"
                                    LabelWidth="64px" Type="Currency"  Width="75px" DbValue='<%# Eval("perDiemWeek") %>'/> %lt;--future use
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="loadMoney" HeaderText="isTAD" UniqueName="loadMoney" Visible="True" ReadOnly="true"></telerik:GridBoundColumn>
                            <telerik:GridButtonColumn ButtonType="LinkButton" CommandName="Edit" Text="Edit" UniqueName="EditColumn" >
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                            <telerik:GridButtonColumn ConfirmText="End This Assignment?" ConfirmDialogType="RadWindow"
                                ConfirmTitle="End Assignment" ButtonType="LinkButton" CommandName="EndAssignment" Text="End Assignment"
                                UniqueName="EndColumn">
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                            <telerik:GridButtonColumn ConfirmText="Delete this Assignment?" ConfirmDialogType="RadWindow"
                               ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete" UniqueName="DeleteColumn">
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu EnableImageSprites="False"></FilterMenu>
                <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
            </telerik:RadGrid>
            <br />
            Completed Assignments:<br />
            <telerik:radgrid ID="RadGrid3" runat="server" AutoGenerateColumns="False" 
                CellSpacing="0" GridLines="None" AllowSorting="True" ShowGroupPanel="True">
                <ClientSettings AllowDragToGroup="True" EnableRowHoverStyle="true"/>
                <MasterTableView DataKeyNames="travelID">
                    <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                   <Columns>
                    <telerik:GridBoundColumn DataField="travelID" UniqueName="travelID" ReadOnly="true" Visible="False"/>
                    <telerik:GridBoundColumn DataField="rtdsEmployeeID" UniqueName="rtdsEmployeeID" ReadOnly="true" Visible="False"/>
                    <telerik:GridBoundColumn DataField="employeeName" UniqueName="employeeName" ReadOnly="true" HeaderText="Traveler Name" Visible="True"/>
                    <telerik:GridBoundColumn DataField="homeLocation" UniqueName="homeLocation" ReadOnly="true" Visible="False"/>
                    <telerik:GridBoundColumn DataField="homeLocaName" UniqueName="homeLocaName" ReadOnly="true" HeaderText="Home Location" Visible="True"/>
                    <telerik:GridBoundColumn DataField="travelLocation" UniqueName="travelLocation" ReadOnly="true" Visible="False"/>
                    <telerik:GridBoundColumn DataField="travelLocaName" UniqueName="travelLocaName"  ReadOnly="true" HeaderText="Travel Location" Visible="True"/>
                    <telerik:GridBoundColumn DataField="startDate" UniqueName="startDate" DataFormatString="{0:MM/dd/yyyy}"  ReadOnly="true" HeaderText="Start" Visible="True"/>
                    <telerik:GridBoundColumn DataField="returnDate" UniqueName="returnDate" DataFormatString="{0:MM/dd/yyyy}"  ReadOnly="true" HeaderText="Return" Visible="True"/>
                    <telerik:GridBoundColumn DataField="loadMoney" HeaderText="isTAD" UniqueName="loadMoney"  Visible="True"/>
                    <telerik:GridButtonColumn ConfirmText="Delete this Assignment?" ConfirmDialogType="RadWindow"
                        ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                        UniqueName="DeleteColumn">
                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                    </telerik:GridButtonColumn>
                   </Columns>
                </MasterTableView>
                <FilterMenu EnableImageSprites="False"/>
                <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"/>
            </telerik:radgrid>
        </td>
    </tr>
</table>
</div>
</form>
</body>
</html>
