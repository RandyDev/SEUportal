<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="svcsRendered.aspx.vb" Inherits="DiversifiedLogistics.svcsRendered" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        /*.rgRow, .rgAltRow {
            height: 40px;
        }
        .RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1;
            margin-bottom: 8px; 
        }
        .RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2016.1.113.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2016.1.113.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2016.1.113.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top;opacity:1}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top;opacity:1}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top;opacity:1}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2016.1.113.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2016.1.113.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2016.1.113.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}*/
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        </telerik:RadAjaxManager>
<div id="divLocationSelect" runat="server">
        Location Name:<br />
            <telerik:RadComboBox RenderMode="Classic" ID="cbLocations" Width="150px" Filter="Contains" AutoPostBack="true" runat="server" DropDownAutoWidth="Enabled" />

                    <telerik:RadDatePicker RenderMode="Classic" ID="RadDatePickerFrom" runat="server" Width="140px">
                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;"
                             runat="server" RenderMode="Classic">
                        </Calendar>
                        <%--<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%" runat="server">
                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                            <FocusedStyle Resize="None"></FocusedStyle>
                            <DisabledStyle Resize="None"></DisabledStyle>
                            <InvalidStyle Resize="None"></InvalidStyle>
                            <HoveredStyle Resize="None"></HoveredStyle>
                            <EnabledStyle Resize="None"></EnabledStyle>
                        </DateInput>--%>

                        <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%" RenderMode="Classic" runat="server">
                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                        </DateInput>

                        <DatePopupButton></DatePopupButton>
                    </telerik:RadDatePicker>

                    <telerik:RadDatePicker  ID="RadDatePickerTo" RenderMode="Classic" runat="server" Width="140px" Culture="en-US">
                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" runat="server" EnableKeyboardNavigation="True" RenderMode="Classic"></Calendar>
                        <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%" runat="server" RenderMode="Classic">
                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                            <FocusedStyle Resize="None"></FocusedStyle>
                            <DisabledStyle Resize="None"></DisabledStyle>
                            <InvalidStyle Resize="None"></InvalidStyle>
                            <HoveredStyle Resize="None"></HoveredStyle>
                            <EnabledStyle Resize="None"></EnabledStyle>
                        </DateInput>
                        <DatePopupButton></DatePopupButton>
                    </telerik:RadDatePicker>

                    <telerik:radbutton ID="btnShowRecords" RenderMode="Classic" runat="server" Text="Show Records" />
        &nbsp; &nbsp;&nbsp; <asp:Label ID="lblcustbillingrate" runat="server"></asp:Label>

        <br /> 
</div>
<div>
<telerik:RadGrid RenderMode="Classic" ID="RadGrid1"  runat="server" Visible="false" CellSpacing="0" GridLines="None">
   <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
    <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" ReorderColumnsOnClient="True">
        <Selecting AllowRowSelect="True" />
        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
    </ClientSettings>
    <MasterTableView Width="825px"  AutoGenerateColumns="False" EditMode="InPlace" CommandItemDisplay="Top" DataKeyNames="ID,timeStamp">
        <Columns>
            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                <ItemStyle CssClass="MyImageButton" />
                <HeaderStyle Width="75px" />
            </telerik:GridEditCommandColumn>
            <telerik:GridBoundColumn DataField="ID" DataType="System.Guid" UniqueName="ID" Visible="false" />
            <telerik:GridTemplateColumn DataField="location" FilterControlAltText="Filter location column" 
                HeaderText="location" SortExpression="locationName" UniqueName="locationName" ReadOnly="true" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="locationLabel" runat="server" Text='<%# Eval("Location")%>' />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="jobDescriptionID" FilterControlAltText="Filter JobDescription column" HeaderText="Job Description" UniqueName="jobDescriptionid">
                <HeaderStyle Width="150px" />
<InsertItemTemplate>
                    <telerik:RadComboBox ID="cbJobDescription" Runat="server" RenderMode="Classic" AutoPostBack="true" OnSelectedIndexChanged="cbJobDescription_SelectedIndexChanged" 
                        DataSourceID="JobDescriptionDataSource" DataTextField="JobDescription" DataValueField="ID">
                     </telerik:RadComboBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorcbjobDescription" runat="server" ControlToValidate="cbJobDescription" ErrorMessage="Required" ForeColor="Red" />
</InsertItemTemplate>
                <EditItemTemplate>
                    <telerik:RadComboBox ID="cbJobDescription" Runat="server" RenderMode="Classic" AutoPostBack="true" OnSelectedIndexChanged="cbJobDescription_SelectedIndexChanged" 
                        DataSourceID="JobDescriptionDataSource" DataTextField="JobDescription" DataValueField="ID" SelectedValue='<%# Bind("JobDescriptionID")%>'>
                     </telerik:RadComboBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorcbjobDescription" runat="server" ControlToValidate="cbJobDescription" ErrorMessage="Required" ForeColor="Red" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="jobDescriptionLabel" runat="server" Text='<%# Eval("jobDescription") %>'></asp:Label>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridtemplateColumn DataField="date" FilterControlAltText="Filter date column" HeaderText="Job Date" SortExpression="date" UniqueName="date" DataType="System.DateTime">
                <HeaderStyle Width="90px" />
                <EditItemTemplate>
                    <telerik:RadDatePicker RenderMode="Classic" ID="dpJobDate" runat="server" Width="110PX"></telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="jobDateValidate" runat="server" ControlToValidate="dpJobDate" ErrorMessage="Required" ForeColor="Red" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblJobDate" runat="server" Text='<%# Eval("jobDate") %>'></asp:Label>
                </ItemTemplate>
            </telerik:GridtemplateColumn>

            <telerik:GridTemplateColumn DataField="employeeid" FilterControlAltText="Filter employee Name column" HeaderText="Employee Name" SortExpression="employeeName"  UniqueName="employeeid">
                <HeaderStyle Width="200px" />

                <EditItemTemplate>
                    <telerik:RadComboBox ID="cbEmployee" Runat="server" DropDownWidth="200px" Width="190px" SelectedValue='<%#Eval("employeeid")%>' DataCheckedField="EmpName"
                         DataSourceID="EmployeeDataSource" DataTextField="EmpName" DataValueField="employeeid" >
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorcbEmployee" runat="server" ControlToValidate="cbEmployee" ErrorMessage="Required" ForeColor="Red" />
                </EditItemTemplate>

                <ItemTemplate>
                    <asp:Label ID="employeeNameLabel" runat="server" Text='<%# Eval("employeeName") %>'></asp:Label>
                </ItemTemplate>

            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="amount" DataType="System.Decimal" HeaderText="Amount" UniqueName="amount">
                    <HeaderStyle  Width="85px"/>
                    <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server" />
<%--                        <%# Eval("Amount")%>--%>
                    </ItemTemplate>
                    <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="numAmount" MinValue="0" Width="70px" TabIndex="8" runat="server" EmptyMessage="$" Type="Currency" DbValue='<%# Eval("Amount")%>' />
                     <asp:RequiredFieldValidator ID="RequirednumAmount" runat="server" ControlToValidate="numAmount" ErrorMessage="Required" ForeColor="Red" />
                    </EditItemTemplate>


                </telerik:GridTemplateColumn>


            <telerik:GridtemplateColumn DataField="CreatedBy" FilterControlAltText="Filter CreatedBy column" HeaderText="CreatedBy" SortExpression="CreatedBy" UniqueName="CreatedBy" ReadOnly="true">
                <HeaderStyle Width="150px" />
               <ItemTemplate>
                  <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("CreatedBy") %>'></asp:Label>
               </ItemTemplate>
            </telerik:GridtemplateColumn>

            <telerik:GridtemplateColumn DataField="timeStamp"  FilterControlAltText="Filter timeStamp column" HeaderText="timeStamp" SortExpression="timeStamp" UniqueName="timeStamp" ReadOnly="true">
                <HeaderStyle Width="150px" />
               <ItemTemplate>
                  <asp:Label ID="lblTimeStamp" runat="server" Text='<%# Eval("timeStamp")%>'></asp:Label>
               </ItemTemplate>
               </telerik:GridtemplateColumn>
            
            <telerik:GridButtonColumn ImageUrl="~/images/redX.gif" ConfirmDialogWidth="330px"  ConfirmDialogHeight="130px" ConfirmTitle="DELETE Services Rendered Record!" ConfirmText="DELETE Services Rendered Record?\nAre you sure? ... if so, click 'OK'\n\n" ConfirmDialogType="RadWindow"
               ButtonType="ImageButton" CommandName="Delete" Text="Delete" UniqueName="DeleteColumn">
               <headerstyle Width="75px" />
                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
            </telerik:GridButtonColumn>
        </Columns>
                <NoRecordsTemplate>
            <table style="height: 75px;" border="0" >
                <tr>
                    <td style="text-align:center;vertical-align:middle;">
                        <br />
                        <br />
                        No records to display
                    </td>
                </tr>
            </table>
        </NoRecordsTemplate>
    </MasterTableView>
</telerik:RadGrid>

    <asp:SqlDataSource ID="JobDescriptionDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:rtdsConnectionString %>" 
        SelectCommand="SELECT JobDescriptions.JobDescription, JobDescriptions.ID
        FROM JobDescriptions INNER JOIN 
        LocationJobDescriptions ON JobDescriptions.ID = LocationJobDescriptions.JobDescriptionID
        WHERE (LocationJobDescriptions.LocationID = @locaID) AND (JobDescriptions.IsHourly = 1)">
        <SelectParameters>
            <asp:ControlParameter ControlID="cbLocations" Name="locaID" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="EmployeeDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:rtdsConnectionString %>"
         SelectCommand="SELECT DISTINCT E.ID AS employeeid, E.LastName, E.LastName + ', ' + E.FirstName + '  (' + E.Login + ')' AS EmpName FROM Employee AS E INNER JOIN Employment ON E.ID = Employment.EmployeeID WHERE (E.LocationID = @locaID) AND (Employment.DateOfDismiss &gt; @datenow) AND (Employment.PayType = 1 OR Employment.PayType = 2) AND (E.FirstName &lt;&gt; 'Truck') OR (E.LocationID = @locaID) AND (Employment.DateOfDismiss &gt; @datenow) AND (Employment.PayType = 1 OR Employment.PayType = 2) AND (E.LastName &lt;&gt; 'Driver') ORDER BY E.LastName, employeeid" >
        <SelectParameters>
            <asp:ControlParameter ControlID="cbLocations" Name="locaid" PropertyName="SelectedValue" />
            <asp:Parameter DefaultValue="getdate()" Name="datenow" />
        </SelectParameters>
    </asp:SqlDataSource>
</div>
    </form>
</body>
</html>
