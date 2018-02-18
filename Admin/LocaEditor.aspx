<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LocaEditor.aspx.vb" Inherits="DiversifiedLogistics.LocaEditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style type="text/css">
body{
font-family:Arial;
} 
.size11{
font-size:11px;
}
.lilBlueButton{
font-size:11px;
color:Blue;
}
    .auto-style1 {
        height: 122px;
        width: 713px;
    }
    .auto-style2 {
        width: 153px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        //<![CDATA[
        function openAddNewParent() {
            var oManager = GetRadWindowManager();
            var loca = "ParentCompanyList.aspx";
            oManager.open(loca, "RadWindow1");
        }
        function openJobTitles() {
            var oManager = GetRadWindowManager();
            var loca = "JobTitleList.aspx";
            oManager.open(loca, "RadWindow2");
        }
        function openLoadDescriptions() {
            var oManager = GetRadWindowManager();
            var loca = "LoadDescriptionList.aspx";
            oManager.open(loca, "RadWindow3");
        }
        function openDepartments() {
            var oManager = GetRadWindowManager();
            var loca = "DepartmentsList.aspx";
            oManager.open(loca, "RadWindow4");
        }
        function openJobDescriptions() {
            var oManager = GetRadWindowManager();
            var loca = "JobDescriptionList.aspx";
            oManager.open(loca, "RadWindow2");
        }

        function OnClientClose(oWnd, args) {
            //get the transferred arguments
             if (args.get_argument() != null) {
                 var arg = args.get_argument();
                 var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                ajaxManager.ajaxRequest(arg);
            }
        }

        function decOnly(i) {
            var t = i.value;
            if (t.length > 0) {
                t = t.replace(/[^\d]+/g, '');
//                t = t.replace(/[^\da-zA-Z]+/g, '');
                i.value = t;
            }
            if (t.length == 5) {
                var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                ajaxManager.ajaxRequest("ZipCodeLookup:" + t);
            }
        }
        //]]>
    </script>
    </telerik:RadCodeBlock>
    <div>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style=" padding-right:8px;" valign="top">Location &nbsp; &nbsp; &nbsp; <asp:LinkButton CssClass="lilBlueButton" ID="lbtnAddLocation" runat="server" Text="Add New" ToolTip="Add NEW Location"  /><br />
                    <telerik:RadComboBox ID="cbLocations" runat="server" AutoPostBack="true" AllowCustomText="true" EmptyMessage="Select Location" />
                </td>
                <td style="padding-left:15px;">
                    <asp:Label ID="lblCopy" runat="server" />
                    <asp:Panel ID="pnlLocaEdit" runat="server" style=" max-height:122px; margin-bottom: 0px">
                        <table class="size11" style="border:1px solid black;">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Location Name:<br />
                                        <telerik:RadTextBox ID="txtLocationName" runat="server" />
                                    </td>
                                    <td>
                                        Login Prefix:<br />
                                        <telerik:RadTextBox MaxLength="3" Width="45px" ID="txtLoginPrefix" ToolTip="2 or 3 character login prefix&#13;for auto generated UserIDs" runat="server" />
                                    </td>
                                    <td style="padding-left:7px;">
                                                Zip Code (physical location)<br />
                                                    <telerik:RadTextBox ID="txtZip" runat="server" Width="45px" />
                                                    <asp:Label ID="lblCityState" runat="server" />
                                                </td>
                                                <td style="padding-left:7px;">Check Charge<br />
                                                    <telerik:RadNumericTextBox ID="txtCheckCharge" runat="server" MaxValue="25" MinValue="0" NumberFormat-DecimalDigits="2" Type="Currency" Value="0" Width="45" />
                                                </td>
                                                <td style="padding-left:7px;">Dividend<br />
                                                    <telerik:RadNumericTextBox ID="txtDividend" runat="server" MaxValue="100" MinValue="0" NumberFormat-DecimalDigits="1" Type="Percent" Value="0" Width="50" />
                                                </td>
                                                <td style="padding-left:7px;">Administrative Fee<br />
                                                    <telerik:RadNumericTextBox ID="numAdministrativeFee" runat="server" MaxValue="100" MinValue="0" NumberFormat-DecimalDigits="2" Type="Currency" Value="0" Width="50" />
                                                </td>
                                                <td style="padding-left:7px;">Customer Fee<br />
                                                    <telerik:RadNumericTextBox ID="numCustomerFee" runat="server" MaxValue="100" MinValue="0" NumberFormat-DecimalDigits="2" Type="Currency" Value="0" Width="50" />
                                                </td>
                                                <td style="padding-left:7px;">
                                                    <asp:CheckBox ID="chkInActive" runat="server" AutoPostBack="true" Text="Location INactive" TextAlign="Left" />
                                                    <br />
                                                    <asp:CheckBox ID="chkPrintTimeStamp" runat="server" Text="Print Reciept TimeStamp" TextAlign="Left" />
                                                </td>
                    </tr>
                </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table><tr>
                        <td>Parent Company &nbsp; &nbsp; &nbsp; 
                        <span onmouseover="this.style.cursor='pointer';" title="Add NEW Parent Company&#13;Edit Parent Company Name" onclick="openAddNewParent();return false;" style="color:blue;" ID="spanAddParent" runat="server">Add/Edit</span><br />
            <telerik:RadComboBox ID="cbParentCompany" AllowCustomText="true" EmptyMessage="Select Client Company" runat="server" 
                DataSourceID="ClientsDataSource" DataTextField="Name" DataValueField="ID" /></td>
                        <td style="padding-left:7px;">Time Zone:<br />
<telerik:RadComboBox ID="cbTimeZones" runat="server">
    <Items>
        <telerik:RadComboBoxItem Value="-5" Text="(-5) Eastern Time Zone"  />
        <telerik:RadComboBoxItem Value="-6" Text="(-6) Central Time Zone"  />
        <telerik:RadComboBoxItem Value="-7" Text="(-7) Mountain Time Zone"  />
        <telerik:RadComboBoxItem Value="-8" Text="(-8) Pacific Time Zone"  />
    </Items>
</telerik:RadComboBox></td>
                        <td style="padding-left:7px;">
            Negative OffSet for <br />Start of Business (Hours)<br />
           &nbsp; &nbsp; <telerik:RadNumericTextBox ID="txtBeginDatOffset" NumberFormat-DecimalDigits="0" MaxValue="0" MinValue="-7" Width="25" 
                Value="0"  runat="server" DataType="System.Int16" />
                        </td>
                        <td style="padding-left:7px;">
                            Enable&nbsp;SickLeave?<br />
                            <asp:RadioButtonList ID="rblistEnableSickLeave" AutoPostBack="true" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Yes">YES</asp:ListItem>
                                <asp:ListItem Value="No">NO</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>

</tr></table>
                        </td>
                        <td align="right" valign="bottom" class="auto-style2">
                           <asp:LinkButton ID="lbtnSaveChanges" runat="server" CssClass="lilBlueButton" text="Save Changes" />
                        </td>
                    </tr>
                </table>
                        <div margin:auto;>              
                            <table class="size11" width="100%">
                        <tr>
                            <td>
                                <asp:Label style="color:Red;" ID="lblResponse" runat="server" />
                            </td>
                            <td align="right">
                                <span title="This is the Location ID&#13;Dbl click first octet and drag right to select"><asp:Label style="color:#cfcfcf;" ID="lblLocaID" runat="server" /></span>
                            </td>
                        </tr>
                    </table>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table><br />
        <hr />
        <table>
            <tr>
                <td valign="top" style="padding-left:15px;">
                    <telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" runat="server" SelectedIndex="1">
                        <Tabs>
                            <telerik:RadTab PageViewID="RadPageView1" runat="server" Text="JobTitles/LoadDescriptions/Departments"></telerik:RadTab>
                            <telerik:RadTab PageViewID="radpageview3" runat="server" Text="JobDescriptions/DepartmentEmail" Selected="True"></telerik:RadTab>
                            <telerik:RadTab PageViewID="radpageview2" runat="server" Text="PriceList"></telerik:RadTab>
                            <telerik:RadTab PageViewID="radpageview4" runat="server" Text="Benefit Configuration" Visible="false"></telerik:RadTab>
                        </Tabs>

                    </telerik:RadTabStrip>
<telerik:RadMultiPage ID="radmultipage1" SelectedIndex="1" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="black" >
    <telerik:RadPageView ID="RadPageView1" Selected="true" Visible="true" runat="server">
        <table>
            <tr>
                <td colspan="2">JobTitles: &nbsp; 
                <span onmouseover="this.style.cursor='pointer';" title="Edit Job Titles Master List" onclick="openJobTitles();return false;" style="color:blue;" class="lilBlueButton" ID="span1" runat="server">Edit Job Titles Master List</span><br />
                <br />
        </td>
        <td colspan="2">Load Descriptions: 
            <span onmouseover="this.style.cursor='pointer';" title="Edit Load Decriptions List" onclick="openLoadDescriptions();return false;" style="color:blue;" class="lilBlueButton" ID="span3" runat="server">Edit Load Descriptions Master List</span><br />

        </td>
        <td colspan="2" valign="top">Departments: &nbsp; 
        <span onmouseover="this.style.cursor='pointer';" title="Edit Departments Master List" onclick="openDepartments();return false;" class="lilBlueButton" style="color:blue;" ID="span2" runat="server">Edit Departments Master List</span><br />

            <br />
        </td>
    </tr>
    <tr>
        <td valign="top">
            <telerik:RadListBox ID="JobTitleList" TransferMode="Move" AllowTransfer="true" 
                TransferToID="lbAvailableJobTitles" runat="server" DataKeyField="JobTitleID"
                EmptyMessage="None assigned, select from Available list." ButtonSettings-Position="right"  
                Width="175px" Height="125px" AllowDelete="false" ButtonSettings-ShowTransferAll="False" 
                ButtonSettings-ShowReorder="False" AllowReorder="true" EnableDragAndDrop="True"
                ButtonSettings-ShowDelete="False" AllowTransferDuplicates="false"  >
            </telerik:RadListBox><br /><br />
            <asp:Button ID="btnSaveJobTitles" Text="Assign this list" runat="server" CommandName="SaveJobTitles" />
        </td>
        <td valign="top">
            <telerik:RadListBox ID="lbAvailableJobTitles" runat="server" datakeyfield="JobTitleID" AllowTransfer="True"
                EnableDragAndDrop="True" TransferToID="JobTitleList"
                Height="225px" Width="175px" ButtonSettings-ShowTransferAll="false"
                ButtonSettings-ShowTransfer="False"  ButtonSettings-ShowReorder="False" 
                EmptyMessage="All Job Titles assigned, edit Master list to add more!" 
                ButtonSettings-ShowDelete="False" >
            </telerik:RadListBox>
        </td>
        <td valign="top">
            <telerik:RadListBox ID="LoadDescriptionsList" TransferMode="Move" AllowTransfer="true" 
                TransferToID="lbAvailableLoadDescriptions" runat="server" DataKeyField="ID"
                EmptyMessage="None assigned, select from Available list." ButtonSettings-Position="right"  
                Width="175px" Height="250px" AllowDelete="false" ButtonSettings-ShowTransferAll="False" 
                ButtonSettings-ShowReorder="False" AllowReorder="true" EnableDragAndDrop="True"
                ButtonSettings-ShowDelete="False" AllowTransferDuplicates="false"  />
            <br /><br />
            <asp:Button ID="Button1" Text="Assign this list" runat="server" CommandName="SaveLoadDescriptions" />
        </td>
        <td valign="top">
        	<telerik:RadListBox ID="lbAvailableLoadDescriptions" runat="server" datakeyfield="ID" AllowTransfer="True"
                  EnableDragAndDrop="True" TransferToID="LoadDescriptionsList"
                  Height="350px" Width="175px" ButtonSettings-ShowTransferAll="false"
                  ButtonSettings-ShowTransfer="True"  ButtonSettings-ShowReorder="False" 
                  EmptyMessage="All Load Descriptions assigned, edit Master list to add more!" 
                  ButtonSettings-ShowDelete="False" >
             </telerik:RadListBox>
        </td>    
        <td valign="top">
            <telerik:RadListBox ID="DepartmentList" TransferMode="Move" AllowTransfer="true" 
                TransferToID="lbAvailableDepartments" runat="server" DataKeyField="ID"
                EmptyMessage="None assigned, select from Available list." ButtonSettings-Position="right"  
                Width="175px" Height="250px" AllowDelete="false" ButtonSettings-ShowTransferAll="False" 
                ButtonSettings-ShowReorder="False" AllowReorder="true" EnableDragAndDrop="True"
                ButtonSettings-ShowDelete="False" AllowTransferDuplicates="false"  />
             <br /><br />
            <asp:Button ID="Button2" Text="Assign this list" runat="server" CommandName="SaveDepartments" />
        </td>
        <td valign="top">
        	<telerik:RadListBox ID="lbAvailableDepartments" runat="server" datakeyfield="ID" AllowTransfer="True"
                 EnableDragAndDrop="True" TransferToID="DepartmentList"
                 Height="350px" Width="175px" ButtonSettings-ShowTransferAll="false"
                 ButtonSettings-ShowTransfer="False"  ButtonSettings-ShowReorder="False" 
                 EmptyMessage="All Load Departments assigned, edit Master list to add more!" 
                 ButtonSettings-ShowDelete="False" >
            </telerik:RadListBox>
        </td>
    </tr>
</table>
         



</telerik:RadPageView>

<telerik:RadPageView ID="RadPageView3" Selected="true" Visible="true" runat="server">

<table>
    <tr>
        <td colspan="2">Job Descriptions: &nbsp; 
        <span onmouseover="this.style.cursor='pointer';" title="Edit Job Description Master List" onclick="openJobDescriptions();return false;" style="color:blue;" class="lilBlueButton" ID="span4" runat="server">Edit Job Descriptions Master List</span><br />

            </td><td style="padding-left:15px">Billing Rates</td><td style="padding-left:15px">Department eMails</td>
    </tr>
    <tr>
        <td valign="top">
            <telerik:RadListBox ID="JobDescriptionList" AutoPostBack="True" AllowTransfer="True"
                 AutoPostBackOnTransfer="true"  
                TransferToID="lbAvailableJobDescriptions" runat="server" DataKeyField="JobDescriptionID"
                EmptyMessage="None assigned, select from Available list." ButtonSettings-Position="right"  
                Width="175px" Height="125px" ButtonSettings-ShowTransferAll="False" 
                ButtonSettings-ShowReorder="False" AllowReorder="True" EnableDragAndDrop="True"
                ButtonSettings-ShowDelete="False"  >
            </telerik:RadListBox><br /><br />
            <asp:Button ID="Button3" Text="Assign this list" runat="server" CommandName="SaveJobDescriptions" />
        </td>
        <td valign="top">
            <telerik:RadListBox ID="lbAvailableJobDescriptions" runat="server" datakeyfield="JobDescriptionID" AllowTransfer="True"
                EnableDragAndDrop="True" TransferToID="JobDescriptionList"
                Height="225px" Width="175px" ButtonSettings-ShowTransferAll="false"
                ButtonSettings-ShowTransfer="False"  ButtonSettings-ShowReorder="False" 
                EmptyMessage="All Job Descriptions assigned, edit Master list to add more!" 
                ButtonSettings-ShowDelete="False" >
            </telerik:RadListBox>
        </td>
        <td valign="top" style=" padding-left:15px">
    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateEditColumn="True" CellSpacing="-1" GridLines="Both">
        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="ID" EditMode="InPlace">
            <Columns>
                <telerik:GridBoundColumn DataField="ID" UniqueName="ID" Visible="false" ReadOnly="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="JobDescription" ReadOnly="true" FilterControlAltText="Filter JobDescription column" HeaderText="JobDescription" SortExpression="JobDescription" UniqueName="JobDescription">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="CustBillingRate" DataType="System.Decimal" HeaderText="Billing Rate" UniqueName="CustBillingRate">
                    <HeaderStyle  Width="85px"/>
                    <ItemTemplate>
                    <asp:Label ID="lblCustBillingRate" runat="server" />
<%--                        <%# Eval("CustBillingRate")%>--%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadNumericTextBox ID="numCustBillingRate" MinValue="0" Width="70px" TabIndex="8" runat="server" EmptyMessage="$" Type="Currency" DbValue='<%# Eval("CustBillingRate")%>' />
                        <asp:RequiredFieldValidator ID="RequirednumCustBillingRate" runat="server" ControlToValidate="numCustBillingRate" ErrorMessage="Required" ForeColor="Red" />
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
            </telerik:RadGrid>


        </td>
        <td style="vertical-align:top;padding-left:15px">

            <telerik:RadGrid ID="RadGrid2" runat="server" AutoGenerateEditColumn="True" AutoGenerateColumns="False" CellSpacing="-1" GridLines="Both">
                <MasterTableView DataKeyNames="LocationID, DepartmentID" >
                    <Columns>
                        <telerik:GridBoundColumn DataField="LocationID" DataType="System.Guid" ReadOnly="true" Visible="false" SortExpression="LocationID" UniqueName="LocationID" />
                        <telerik:GridBoundColumn DataField="DepartmentID" DataType="System.Guid" ReadOnly="true"  Visible="false" HeaderText="DepartmentID" SortExpression="DepartmentID" UniqueName="DepartmentID" />
                        <telerik:GridBoundColumn DataField="DeptName"  HeaderText="DeptName" ReadOnly="true" SortExpression="DeptName" UniqueName="DeptName" /> 
                        <telerik:GridTemplateColumn DataField="email"  HeaderText="email" SortExpression="email" UniqueName="email">
                            <ItemTemplate>
                                <asp:Label ID="lblemail" runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadTextBox ID="txtemail" runat="server" Width="250px" Text='<%# Eval("email")%>' />
                                <asp:RegularExpressionValidator ID="regexemailValid" runat="server" 
                                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                    ControlToValidate="txtemail" ErrorMessage="Invalid Email Format" />
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn DataField="emailCC"  HeaderText="emailCC" SortExpression="emailCC" UniqueName="emailCC">
                            <ItemTemplate>
                                <asp:Label ID="lblemailCC" runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadTextBox ID="txtemailCC" runat="server" Width="250px" Text='<%# Eval("emailCC")%>' />
                                <asp:RegularExpressionValidator ID="regexemailCCValid" runat="server" 
                                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                    ControlToValidate="txtemailCC" ErrorMessage="Invalid Email Format" />
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridCheckBoxColumn DataField="hasPics" DataType="System.Boolean" ReadOnly="true" FilterControlAltText="Filter hasPics column" HeaderText="hasPics" SortExpression="hasPics" UniqueName="hasPics" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>




        </td>


    </tr>
</table>



</telerik:RadPageView>

<telerik:RadPageView ID="RadPageView2" TabIndex="0" runat="server">
<asp:Panel ID="pnlLocaPriceEdit" runat="server" Visible="false">
<hr />
    Price List:<br />
<telerik:RadGrid ID="RadGridPriceList" runat="server" AutoGenerateColumns="False" 
        CellSpacing="0" GridLines="None" AllowSorting="True" 
        AllowFilteringByColumn="True">
    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="True" 
        ReorderColumnsOnClient="True">
        <Selecting CellSelectionMode="None" />
    </ClientSettings>
    <MasterTableView CommandItemDisplay="Top" AllowSorting="true" DataKeyNames="PriceID" EditMode="InPlace">
        <CommandItemSettings ExportToPdfText="Export to PDF" />
        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
            Visible="True">
        </RowIndicatorColumn>
        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
            Visible="True">
        </ExpandCollapseColumn>
        <Columns>
            <telerik:GridBoundColumn DataField="PriceID" ReadOnly="true" Visible="false" />
            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                <ItemStyle CssClass="MyImageButton" />
            </telerik:GridEditCommandColumn>
            <telerik:GridTemplateColumn HeaderText="Department" DataField="DepartmentName" UniqueName="DepartmentName" SortExpression="DepartmentName">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "DepartmentName")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadComboBox ID="cbDepartment" runat="server" 
                        DataSourceID="DepartmentsDataSource" DataTextField="Name" DataValueField="ID" 
                        SelectedValue='<%#Bind("DepartmentID") %>' Width="95px">
                    </telerik:RadComboBox>
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Load Type" UniqueName="LoadTypeName" DataField="LoadTypeName" SortExpression="LoadTypeName" ShowSortIcon="true">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "LoadTypeName")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadComboBox ID="cbLoadType" runat="server" 
                        DataSourceID="LoadtypesDataSource" DataTextField="Name" DataValueField="ID" 
                        SelectedValue='<%#Bind("LoadtypeID") %>' Width="95px">
                    </telerik:RadComboBox>
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Description" DataField="LoadDescriptionName" UniqueName="LoadDescriptionName" SortExpression="LoadDescriptionName">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "LoadDescriptionName")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadComboBox ID="cbDescription" EnableLoadOnDemand="true" DataTextField="Name"
                         OnItemsRequested="cbDescription_ItemsRequested" datavaluefield="ID" autopostback="true"
                        HighlighttemplatedItems="true" OnSelectedIndexChanged="onSelectedIndexChangedHandler" runat="server" 
                        SelectedValue='<%#Bind("LoadDescriptionID") %>' 
                        Width="125px">
                    </telerik:RadComboBox>
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Per Case" ShowSortIcon="false" AllowFiltering="false" >
                <ItemTemplate>
                    <%# String.Format("{0:c}", Convert.ToDouble(Container.DataItem("RatePerCase")))%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadNumericTextBox ID="num_RatePerCase" runat="server" 
                        DbValue='<%#Bind("RatePerCase") %>' EmptyMessage="$" Type="number" 
                        Width="40px" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Per Pallet" ShowSortIcon="false" AllowFiltering="false">
                <ItemTemplate>
                    <%# String.Format("{0:c}", Convert.ToDouble(Container.DataItem("RatePerPallet")))%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadNumericTextBox ID="num_RatePerPallet" runat="server" 
                        DbValue='<%#Bind("RatePerPallet") %>' EmptyMessage="$" Type="Currency" 
                        Width="40px" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Pallet Low" ShowSortIcon="false" AllowFiltering="false">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "PerPalletLow")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadNumericTextBox ID="num_PerPalletLow" runat="server" 
                        DbValue='<%#Bind("PerPalletLow") %>' EmptyMessage="-" Width="40px" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Pallet High" ShowSortIcon="false" AllowFiltering="false">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "PerPalletHigh")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadNumericTextBox ID="num_PerPalletHigh" runat="server" 
                        DbValue='<%#Bind("PerPalletHigh") %>' EmptyMessage="-" Width="40px" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="PerLoad" ShowSortIcon="false" AllowFiltering="false">
                <ItemTemplate>
                    <%# String.Format("{0:c}", Convert.ToDouble(Container.DataItem("RatePerLoad")))%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadNumericTextBox ID="num_RatePerLoad" runat="server" 
                        DbValue='<%#Bind("RatePerLoad") %>' EmptyMessage="$" Type="Currency" 
                        Width="40px" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="BadPallet" ShowSortIcon="false" AllowFiltering="false">
                <ItemTemplate>
                    <%# String.Format("{0:c}", Convert.ToDouble(Container.DataItem("RateBadPallet")))%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadNumericTextBox ID="num_RateBadPallet" runat="server" 
                        DbValue='<%#Bind("RateBadPallet") %>' EmptyMessage="$" Type="Currency" 
                        Width="40px" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Restack" ShowSortIcon="false" AllowFiltering="false">
                <ItemTemplate>
                    <%# String.Format("{0:c}", Convert.ToDouble(Container.DataItem("RateRestack")))%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadNumericTextBox ID="num_RateRestack" runat="server" 
                        DbValue='<%#Bind("RateRestack") %>' EmptyMessage="$" Type="Currency" 
                        Width="40px" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Max $" ShowSortIcon="false" AllowFiltering="false">
                <ItemTemplate>
                    <%# String.Format("{0:c}", Convert.ToDouble(Container.DataItem("PriceMax")))%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadNumericTextBox ID="num_PriceMax" runat="server" 
                        DbValue='<%#Bind("PriceMax") %>' EmptyMessage="$" Type="Currency" 
                        Width="40px" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Dbl Stack" ShowSortIcon="false" AllowFiltering="false">
                <ItemTemplate>
                    <%# String.Format("{0:c}", Convert.ToDouble(Container.DataItem("RateDoubleStack")))%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadNumericTextBox ID="num_RateDoubleStack" runat="server" 
                        DbValue='<%#Bind("RateDoubleStack") %>' EmptyMessage="$" Type="Currency" 
                        Width="40px" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Pin Wheeled" ShowSortIcon="false" AllowFiltering="false">
                <ItemTemplate >
                    <%# String.Format("{0:c}", Convert.ToDouble(Container.DataItem("RatePinWheeled")))%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadNumericTextBox ID="num_RatePinWheeled" runat="server" 
                        DbValue='<%#Bind("RatePinWheeled") %>' EmptyMessage="$" Type="Currency" 
                        Width="40px" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" 
                ConfirmDialogType="RadWindow" ConfirmText="Delete this Price Line?" 
                ConfirmTitle="Delete" Text="Delete" UniqueName="DeleteColumn">
                <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" />
            </telerik:GridButtonColumn>
        </Columns>
        <EditFormSettings>
            <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" 
                UniqueName="EditCommandColumn1">
            </EditColumn>
        </EditFormSettings>
    </MasterTableView>
    <FilterMenu EnableImageSprites="False">
    </FilterMenu>

</telerik:RadGrid>
</asp:Panel>
</telerik:RadPageView>
<telerik:RadPageView ID="RadPageView4" TabIndex="0" runat="server">
    <table style="border:1px solid black;">
        <tr>
            <td colspan="2" style="text-align:center">
<u>Sick Leave Configuration</u>
            </td>
        </tr>
        <tr>
            <td>Start Date:</td>
            <td><telerik:RadDatePicker ID="dpStartBenefitsDate" MinDate ="1/1/1900" MaxDate ="12/31/9999" runat="server"></telerik:RadDatePicker></td>
        </tr>
        <tr>
            <td>Accrural Rate = 1 hr earned for each </td>
            <td><telerik:RadNumericTextBox ID="numAccrualRate" NumberFormat-DecimalDigits="0" value="30" runat="server" Width="40px" /> hours worked </td>
        </tr>
        <tr>
            <td>Max accrued Per Annum</td>
            <td><telerik:RadNumericTextBox ID="numMaxHours" NumberFormat-DecimalDigits="0" runat="server" Width="40px" /> Hours</td>
        </tr>
        <tr>
            <td>Minimum per use</td>
            <td><telerik:RadNumericTextBox ID="numMinHours" NumberFormat-DecimalDigits="0" runat="server" Width="40px" /> Hours</td>
        </tr>
        <tr>
            <td>Eligible after </td>
            <td><telerik:RadNumericTextBox ID="numEligibleDays" NumberFormat-DecimalDigits="0" runat="server" Width="40px" /> Days</td>
        </tr>
        <tr>
            <td>Max Allowed </td>
            <td><telerik:RadNumericTextBox ID="numMaxPerAnnum" NumberFormat-DecimalDigits="0" runat="server" Width="40px" /> Hours Per Annum</td>
        </tr>
        <tr>
            <td colspan="2">
                <telerik:RadButton ID="btnSubmitBenefits" Text ="Save Benefits Configuration" runat="server" />
                &nbsp;<asp:label ID="lblsubmitBenefitsresponse" Visible="false" ForeColor="Red" runat="server" /></td>
        </tr>

    </table>
    </telerik:RadPageView>
</telerik:RadMultiPage>


    
        
        </td>
    </tr>
</table>



    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" Title="Add/Edit Parent Company" VisibleStatusbar="false"
        runat="server" Skin="Sunset" Modal="true" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close" OnClientClose="OnClientClose"
                NavigateUrl="ParentCompanyList.aspx" Width="525px" Height="565px">
            </telerik:RadWindow>            
            <telerik:RadWindow ID="RadWindow2" runat="server" Behaviors="Close" Title="Add New Job Title" OnClientClose="OnClientClose"
                NavigateUrl="JobTitleList.aspx" Width="525px" Height="565px">
            </telerik:RadWindow>            
            <telerik:RadWindow ID="RadWindow3" runat="server" Behaviors="Close" Title="Add/Edit Load Description" OnClientClose="OnClientClose"
                NavigateUrl="LoadDescriptionList.aspx" Width="525px" Height="565px">
            </telerik:RadWindow>
            <telerik:RadWindow ID="RadWindow4" runat="server" Behaviors="Close" Title="Add/Edit Departments" OnClientClose="OnClientClose"
                NavigateUrl="DepartmentsList.aspx" Width="525px" Height="565px">
            </telerik:RadWindow>
            <telerik:RadWindow ID="RadWindow5" runat="server" Behaviors="Close" Title="Add New Job Description" OnClientClose="OnClientClose"
                NavigateUrl="JobDesctiptionList.aspx" Width="525px" Height="565px">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>



         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:rtdsConnectionString %>" SelectCommand="SELECT [JobTitle], [JobTitleID], [IsActive] FROM [JobTitle]"></asp:SqlDataSource>





<asp:SqlDataSource ID="DepartmentsDataSource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:rtdsConnectionString %>" 
    SelectCommand="SELECT ID, Name FROM Department ORDER BY Name"></asp:SqlDataSource>
<asp:SqlDataSource ID="LoadtypesDataSource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:rtdsConnectionString %>" 
    SelectCommand="SELECT ID, Name FROM LoadType ORDER BY Name"></asp:SqlDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
<asp:SqlDataSource ID="LoadDescriptionsDataSource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:rtdsConnectionString %>" 
    SelectCommand="SELECT Description.ID, Description.Name, Description.InActive, LocationDescription.LocationID FROM Description INNER JOIN LocationDescription ON Description.ID = LocationDescription.DescriptionID WHERE (LocationDescription.LocationID = @locaid) ORDER BY Description.Name">
    <SelectParameters>
        <asp:ControlParameter ControlID="cbLocations" Name="locaid" PropertyName="SelectedValue" />
    </SelectParameters>
        </asp:SqlDataSource>
<asp:SqlDataSource ID="ClientsDataSource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:rtdsConnectionString %>" 
    SelectCommand="SELECT ID, Name FROM ParentCompany ORDER BY Name"></asp:SqlDataSource>





    </div>
    </form>
</body>
</html>
