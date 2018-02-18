<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AddEditAdditionalCompensation.aspx.vb" Inherits="DiversifiedLogistics.AddEditAdditionalCompensation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../styles/StyleSheet.css" rel="stylesheet" type="text/css" />

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">

</script>
    </telerik:RadCodeBlock>
    <style type="text/css">
    
    .lilBlueButton{
    color:Blue;
    font-weight:normal;
    font-size: 11px;
    padding-left:5px;
}
    .currentColor{color:Blue;}
    .previousColor{color:Red;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server">
<img src="../images/ajax-loader-smallBar.gif" alt="Loading Image" />
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel3" runat="server">
<img src="../images/ajax-loader-smallBar.gif" width="165" height="24" alt="Loading Image" />
    </telerik:RadAjaxLoadingPanel>
    <div>

<table cellpadding="0" cellspacing="0">
    <tr>
        <td colspan="3">
            <table width="100%">
    <tr>
        <td width="10%" valign="top">
            <telerik:RadComboBox ID="cbLocations" Width="150px" runat="server" Filter="Contains" />
        </td>
        <td style="padding-left:30px;">
        <asp:Label ID="lblPPSelect" Text="Select Pay Period:<br />" Visible="false" runat="server" /><asp:RadioButtonList AutoPostBack="true" ID="rbLst" RepeatDirection="Horizontal" runat="server"  />
        </td>
        <td align="right" valign="top">
            <span onmouseover="this.style.cursor='help'"><asp:Label ID="lblHelp" CssClass="lilBlueButton" Text="help with this page" runat="server" /></span>
        </td>
    </tr>

            </table>      
<asp:Label ID="lblSelectLocation" runat="server" Text="<br />^^ Select Location ^^"/>
 
        </td>
    </tr>
    <tr>
        <td valign="top" width="25%">
        <%-- *******************start Left Side WO Grid **********************--%>

<table class="lbl" >
    <tr>
        <td>
    <telerik:RadGrid ID="RadGrid1" runat="server" GridLines="None" Width="275px" >
<MasterTableView AutoGenerateColumns="False" DataKeyNames="ID" Width="275px" >
    <Columns>
        <telerik:GridBoundColumn DataField="ID" DataType="System.Guid" UniqueName="ID" ReadOnly="True" Visible="false" />
        <telerik:GridBoundColumn DataField="Emp" HeaderText="Name" UniqueName="Emp">
        </telerik:GridBoundColumn>
        <telerik:GridTemplateColumn DataField="OAPlus" HeaderText="OAPlus" UniqueName="OAPlus">
            <ItemTemplate>
                <asp:Label ID="lblOAPlus" runat="server" />
            </ItemTemplate>
        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn DataField="OAMinus" HeaderText="OAMinus" UniqueName="OAMinus">
            <ItemTemplate>
                <asp:Label ID="lblOAMinus" runat="server" />
            </ItemTemplate>
        </telerik:GridTemplateColumn>
        <telerik:GridBoundColumn DataField="JobTitle" DataType="System.String" UniqueName="JobTitle" ReadOnly="True" Visible="false" />
        <telerik:GridBoundColumn DataField="CurrentLoca" DataType="System.Guid" UniqueName="CurrentLoca" ReadOnly="True" Visible="false" />
        <telerik:GridBoundColumn DataField="HomeLoca" DataType="System.Guid" UniqueName="HomeLoca" ReadOnly="True" Visible="false" />

</Columns>
</MasterTableView>
            <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true" >
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
        </telerik:RadGrid>

</td>
    </tr>
</table>

<%-- *******************end Left Side WO Grid **********************--%>
        </td>

        <td></td>

        <td valign="top" style="padding-left:11px;">
<%-- *******************start Right Side Employee Form **********************--%>
<%-- ******************* default tool tip labels ******************* --%>
<table width="740" cellpadding="0" cellspacing="0">
<tr>
        <td colspan="2"><asp:Label ID="lblSelectEmployee" runat="server" Text="<br /><br /><br /><br /><<<----  Select an Employee from left" /></td>
</tr><tr>
<td colspan="2">
<asp:Panel ID="pnlWOedit" runat="server" Visible="False">

<fieldset style="border:1px solid black; padding:0px 12px 1px 12px;" >
    <legend style="font-size:24px; font-family:Arial; font-weight:bold;">
        <asp:Label ID="lblEmpName" runat="server" />
    </legend>

<table>
    <tr>
    <td valign="top"><telerik:RadBinaryImage ID="imgMugShot"  Width="75" ResizeMode="Fit" 
            runat="server" /></td>
    <td valign="top" style="padding-left:50px;">
Comments<br />
<telerik:RadTextBox ID="txt_Comments" TextMode="MultiLine" Rows="5" Width="300px" EmptyMessage="Comments" runat="server" /><br />
    </td>
</tr></table>
<hr />
    Additional / Other Compensation - <asp:Label ID="lblCurPrevPayPeriod" runat="server" /> Pay Period 

    <telerik:RadGrid ID="RadGrid2" runat="server" Visible="false" CellSpacing="0" GridLines="None">
        <MasterTableView AutoGenerateColumns="False" EditMode="InPlace" DataKeyNames="AddCompID, EmployeeID" CommandItemDisplay="Top" >
            <CommandItemSettings ExportToPdfText="Export to PDF" />
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                Visible="False">
            </RowIndicatorColumn>
            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                Visible="False">
            </ExpandCollapseColumn>
            <Columns>
            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                <ItemStyle CssClass="MyImageButton" />
            </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="AddCompID" DataType="System.Guid" Visible="false" 
                    FilterControlAltText="Filter AddCompID column" HeaderText="AddCompID" 
                    ReadOnly="True" SortExpression="AddCompID" UniqueName="AddCompID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EmployeeID" DataType="System.Guid" Visible="false" ReadOnly="True" 
                    FilterControlAltText="Filter EmployeeID column" HeaderText="EmployeeID" 
                    SortExpression="EmployeeID" UniqueName="EmployeeID">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Description">
                <ItemTemplate>
                <%# Eval("CompDesc")%>
                </ItemTemplate>                
                <EditItemTemplate>
                    <telerik:RadComboBox ID="cbAddCompDescription" Width="130px" runat="server" SelectedValue='<%#Eval("AddCompDescriptionID") %>' DataCheckedField="CompDesc" 
                        DataSourceID="SqlDataSourceAddCompDescriptions" DataValueField="AddCompDescriptionID" DataTextField="CompDesc" >
                    </telerik:RadComboBox>
                </EditItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn DataField="AddCompStartDate" DataType="System.DateTime" HeaderText="Start" UniqueName="AddCompStartDate" >
                    <ItemTemplate>
                        <%# Format(Eval("AddCompStartDate"), "dd MMM yy")%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadDatePicker runat="server" Width="105px" ID="dpAddCompStartDate" Culture="en-US" 
                            DbSelectedDate='<%# Eval("AddCompStartDate") %>'/>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="AddCompEndDate" DataType="System.DateTime" HeaderText="End" UniqueName="AddCompEndDate" >
                    <ItemTemplate>
                        <%# Format(Eval("AddCompEndDate"), "dd MMM yy")%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadDatePicker runat="server" Width="105px" ID="dpAddCompEndDate" Culture="en-US" 
                            DbSelectedDate='<%# Eval("AddCompEndDate") %>'/>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="AddCompAmount" DataType="System.Decimal" HeaderText="Amount" UniqueName="AddCompAmount">
                    <HeaderStyle  Width="85px"/>
                    <ItemTemplate>
                    <asp:Label ID="lblAddCompAmount" runat="server" />
<%--                        <%# Eval("AddCompAmount")%>--%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadNumericTextBox ID="numAddCompAmount" MinValue="0" Width="70px" TabIndex="8" runat="server" EmptyMessage="$" Type="Currency" DbValue='<%# Eval("AddCompAmount") %>' />
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>


                <telerik:GridTemplateColumn HeaderText="Comments">
                <ItemTemplate>
                <%# Eval("AddCompComments")%>
                </ItemTemplate>                
                <EditItemTemplate>
                    <telerik:RadTextBox ID="txtAddCompComments" Text='<%#Eval("AddCompComments") %>' runat="server" />
                </EditItemTemplate>

                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn DataField="userID" DataType="System.Guid" Visible="false" ReadOnly="true"
                    FilterControlAltText="Filter userID column" HeaderText="userID" 
                    SortExpression="userID" UniqueName="userID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Credit" DataType="System.Byte" Visible="false" ReadOnly="true"
                     UniqueName="Credit">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="JobTitleJobTitle" DataType="System.String" Visible="false" ReadOnly="true"
                    FilterControlAltText="Filter userID column" HeaderText="JobTitle" 
                    SortExpression="JobTitle" UniqueName="JobTitle">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="TimeStamp" DataType="System.DateTime" visible="false"
                    FilterControlAltText="Filter TimeStamp column" HeaderText="TimeStamp"  ReadOnly="true"
                    SortExpression="TimeStamp" UniqueName="TimeStamp">
                </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn ImageUrl="~/images/redX.gif" ConfirmDialogWidth="330px"  ConfirmDialogHeight="130px" ConfirmTitle="DELETE Additional Compensation Record!" ConfirmText="DELETE Additional Compensation Record?<br />Are you sure? ... if so, click 'OK'<br/><br/>" ConfirmDialogType="RadWindow"
                ButtonType="ImageButton" CommandName="Delete" Text="Delete" UniqueName="DeleteColumn">
                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
            </telerik:GridButtonColumn>


            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
<br />
                    <asp:SqlDataSource ID="SqlDataSourceAddCompDescriptions" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:rtdsConnectionString %>" 
                        SelectCommand="SELECT [AddCompDescriptionID], [CompDesc] FROM [AddCompDesc] WHERE ([InActive] = @InActive) ORDER BY [Credit] DESC">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="InActive" />
                        </SelectParameters>
                    </asp:SqlDataSource>

















</fieldset>
</asp:Panel>
</td>
</tr></table>
<%-- *******************start Right Side EDIT Form **********************--%>
<%-- *******************END Right Side WO Form **********************--%>
<asp:Panel ID="pnlTitle" runat="server">
<br /><br /><br/><br /><br/>
            <table cellpadding="0" cellspacing="0" align="center" style="font-family:Arial;"><tr>
            <td align="center" style="font-size:26px;"><asp:Label ID="lblPageTitle" Text="Other Compensation<br />Supervisor / Travel / Holiday Pay" runat="server" /></td>
            </tr></table>

</asp:Panel>

        </td>
    </tr>
</table>

<telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="lblHelp" Width="650px" RelativeTo="Element"  
    ShowEvent="OnClick" Position="BottomLeft" HideEvent="ManualClose"  
     Animation="Resize" EnableShadow="true">
<table cellpadding="0" cellspacing="0" width="100%"><tr>
<td><span class="ttHeader">Other Compensation <span style="font-size:12px;">Supervisor / Travel / Holiday Pay</span></span>
</td>
</tr></table>

<table><tr><td style="padding:0 8px;">
<table>
    <tr>
        <td class="ttBody"> 
            While hourly pay and percentage pay are auto-calculated, <span style="text-decoration:underline;">all OTHER pay MUST be entered 
            for <em>each pay period</em> using this form</span>. &nbsp;'Other' pay includes +plus amounts (Supervisor, Travel, Holiday) 
            and -minus amounts (Supplies, Damage, Drug Test, etc)
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Employee List (left side)</td>
    </tr>
    <tr>
        <td  class="ttBody">
            Once you have selected a location from the dropdown box (if enabled), this list shows all employees currently at the selected location. &nbsp;Following 
            their name and login ID are two amounts. &nbsp;The first amount is the total additional compensation for the pay period and the second number (in parentheses) 
            is the total deductions to be applied for the pay period.<br />
            The list will be sorted so that any entries for the pay period and <span style="color:Blue">Supervisor Unloaders</span> will float to the top of the 
            list. &nbsp;<span style="color:Red">Travelers</span> and <span style="color:Red">TAD</span> assignments do not float until 
            an Other pay record has been created.<br />
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Additional / Other Compensation Grid</td>
    </tr>
    <tr>
        <td  class="ttBody">
        The form is simple.
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="vertical-align:top;padding-right:5px;">1</td>
                <td>Select a description for the amount you want to enter.</td>
            </tr>
            <tr>
                <td style="vertical-align:top;padding-right:5px;">2</td>
                <td>Select a Start date using the pop-up calendar control. &nbsp;(*<em>see below</em>)</td>
            </tr>
            <tr>
                <td style="vertical-align:top;padding-right:5px;">3</td>
                <td>Optionally select an End date. &nbsp;You may leave it blank for single event (Holiday) or deduction amount OR<br />
                if you do select a date be sure not to span across two separate pay-weeks. &nbsp;(*<em>see below</em>)
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;padding-right:5px;">4</td>
                <td>Enter the Amount you want to apply.
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;padding-right:5px;">5</td>
                <td>Optionally add a comment for this record. &nbsp;Unlike the general comments at the top of the form, this comment is
                private to this record.
                </td>
            </tr>
        </table>
        ** IMPORTANT!!
        As you are aware, payroll is calculated by the week and then combined to make a 2 week pay period. &nbsp;With 
        this in mind, it is important that each entry be confined to a single pay-week. &nbsp;If you need to pay a 
        supervisor or traveler for two weeks, you will need to make two entries in this table. &nbsp;One for each pay week in the pay period.<br /> 
        Example:<br />
        The  pay 'period' selected runs from <asp:Label ID="lblspay1" runat="server" /> through <asp:Label ID="lblepay2" runat="server" /><br />
        Week 1 runs from <asp:Label ID="lblspay1a" runat="server" /> through <asp:Label ID="lblepay1" runat="server" /><br />
        Week 2 runs from <asp:Label ID="lblspay2" runat="server" /> through <asp:Label ID="lblepay2a" runat="server" /><br />
        You must be sure that no entry spans across <asp:Label ID="lblepay1a" runat="server" /> and <asp:Label ID="lblspay2a" runat="server" />
        <br />

        </td>
    </tr>
    <tr>
        <td class="ttTitle">Pay Period Selector (top of page)</td>
    </tr>

    <tr>
        <td  class="ttBody">
            For the most part, you will be working only in the current pay period and that will be the only selection available. &nbsp;However, 
            to allow time to finish up the previous pay period, you will be able to select the previous pay period for 5 days after the end of any pay period.<br />
            example: if a pay period ends on a <asp:Label ID="lbldowend" runat="server" /> you will see it in the pay week selector at the top of the page 
            through the following <asp:Label ID="lbldowend5" runat="server" />.
        <br />
        </td>
    </tr>
</table><br />
<center>To Close - Click X in upper right corner</center>

</td></tr></table>
</telerik:RadToolTip>



<telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>

<telerik:RadWindow ID="wTimePunche" Width="365px" Height="435px" 
    ShowContentDuringLoad="false"   runat="server" Modal="false" 
    Title="Add/Edit Department TimeCard"
    style="min-height:297px;"
    VisibleStatusbar="false"
    OnClientClose = "OnClientCloseTimePunche"
    Behaviors="Move, Resize" />

<telerik:RadWindow ID="wAdditionalCompensation" Width="365px" Height="435px" 
    ShowContentDuringLoad="false"   runat="server" Modal="True" 
    Title="Add/Edit Compensation"
    style="min-height:297px;"
    VisibleStatusbar="false"
    OnClientClose = "OnClientCloseCompensation"
    Behaviors="Move, Resize, Close" />

    </Windows>
</telerik:RadWindowManager>


        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
<script type="text/javascript">

    function openTimePunche(tpID) {
        //        document.getElementById("< %= btnSaveChanges.ClientID % >").disabled = true;
        var oManager = GetRadWindowManager();
        var loca = "AddEditTimePunche.aspx?tpID=" + tpID
        oManager.open(loca, "wTimePunche");

    }
    function openNewTimePunche(empID, locaID) {
        //        document.getElementById("< %= btnSaveChanges.ClientID % >").disabled = true;
        var oManager = GetRadWindowManager();
        var loca = "AddEditTimePunche.aspx?empID=" + empID + "&locaID=" + locaID

        oManager.open(loca, "wTimePunche");

    }
    function OnClientCloseTimePunche(sender, args) {
        if (args.get_argument() != null) {
            var arg = args.get_argument();
            //            var CarrierName = document.getElementById("< %= lblCarrierNamev.ClientID % >")
            //            CarrierName.innerText = arg.split(":", 2)[0];
            //            CarrierName.style.fontSize = '12px';
            //            CarrierName.style.color = 'blue';
            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
            ajaxManager.ajaxRequest("TimePunche:" + arg);
            //                oWnd.close();
        } else {
            //            document.getElementById("< %= btnSaveChanges.ClientID % >").disabled = false;

        }

    }

    function toggleSaveBtnOn() {
        document.getElementById('divSaveComments').className = 'showme';
    }

    function openCompensation(tpID) {
        //        document.getElementById("< %= btnSaveChanges.ClientID % >").disabled = true;
        var oManager = GetRadWindowManager();
        var loca = "AddEditAdditionalCompensation.aspx?empID=" + acID
        oManager.open(loca, "wAdditionalCompensation");

    }
    function openNewCompensation(empID, locaID) {
        //        document.getElementById("< %= btnSaveChanges.ClientID % >").disabled = true;
        var oManager = GetRadWindowManager();
        var loca = "AddEditAdditionalCompensation.aspx?empID=" + empID + "&locaID=" + locaID

        oManager.open(loca, "wAdditionalCompensation");

    }
    function OnClientCloseCompensation(sender, args) {
        if (args.get_argument() != null) {
            var arg = args.get_argument();
            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
            ajaxManager.ajaxRequest("Compensation:" + arg);
            //                oWnd.close();
        } else {
            //            document.getElementById("< %= btnSaveChanges.ClientID % >").disabled = false;

        }
    }

</script>
</telerik:RadCodeBlock> 

    </div>
    </form>
</body>
</html>
