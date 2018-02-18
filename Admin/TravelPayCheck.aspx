<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TravelPayCheck.aspx.vb" Inherits="DiversifiedLogistics.TravelPayCheck" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
<style type="text/css">
.data{ font-size:14px;font-weight:bold;}
.lbl{font-size:12px;font-weight:normal;}
.lbl td{padding-left:24px;}
.ColorMeRed {color:Red;}
.picme{width: 72px; height: 96px;}

.lilBlueButton{
text-decoration:none;
font-size:11px;
color:Blue;
font-family:arial;
}
.hideme {
 display:none;
}
.showme{
 display:inline;
}
body{font-family:Arial;}
</style>
</head>
<body>
    <form id="form2" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

    <div>
<table width="90%" align="center">
    <tr>
        <td align="left">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="top">
            Location:<br /><telerik:RadComboBox ID="cbLocations" Filter="Contains" Width="110px" runat="server" />
                    </td>
                    <td style="padding-left:54px;">
             Pay Period:<br /><asp:RadioButtonList AutoPostBack="true" ID="rbLst" CellPadding-left="12" runat="server"  />
                    </td>
                </tr>
            </table>    
        </td>
        <td align="right">
            <asp:Label ID="lblHelpPage" runat="server" CssClass="lilBlueButton" Text="help&nbsp;with&nbsp;this&nbsp;page" />
        </td>
    </tr>
</table>
<hr />
<table style="font-size:13px;">
    <tr>
        <td style="padding-left:12px;" valign="top">
        Supervisors:
        <telerik:RadGrid ID="RadGrid3" runat="server" CellSpacing="0" GridLines="None" 
                AutoGenerateColumns="False" ShowFooter="true" >
            <ClientSettings EnableRowHoverStyle="true">
            </ClientSettings>
<MasterTableView DataKeyNames="EmployeeID">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn Visible="False" FilterControlAltText="Filter RowIndicator column">
</RowIndicatorColumn>

<ExpandCollapseColumn Visible="False" FilterControlAltText="Filter ExpandColumn column">
</ExpandCollapseColumn>

    <Columns>



        <telerik:GridBoundColumn DataField="EmployeeID" DataType="System.Guid"  Visible="False" ReadOnly="true"
            FilterControlAltText="Filter EmployeeID column" HeaderText="EmployeeID" 
            SortExpression="EmployeeID" UniqueName="EmployeeID">
        </telerik:GridBoundColumn>

        <telerik:GridBoundColumn DataField="EmployeeName"  ReadOnly="true"
            FilterControlAltText="Filter EmployeeName column" HeaderText="Supervisor's Name" 
            SortExpression="EmployeeName" UniqueName="EmployeeName">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Login"  ReadOnly="true"
            FilterControlAltText="Filter Login column" HeaderText="Login" 
            SortExpression="Login" UniqueName="Login">
        </telerik:GridBoundColumn>

        <telerik:GridTemplateColumn datafield="Week1" DataType="System.Decimal" HeaderText="Week 1" UniqueName="Week1">
                <ItemTemplate>
                <%# FormatCurrency(IIf(IsDBNull(Eval("Week1")), 0, Eval("Week1")))%>
                </ItemTemplate>
                <EditItemTemplate>
                <telerik:RadNumericTextBox runat="server" ID="numWeek1"  Culture="en-US" 
                     LabelWidth="64px" Type="Currency" Width="60px" DbValue='<%# Eval("Week1") %>'/>
<telerik:RadTextBox ID="txtWeek1" Width="225px" EmptyMessage="Optional Comments" runat="server" />
                </EditItemTemplate>
        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn datafield="Week2" DataType="System.Decimal" HeaderText="Week 2" UniqueName="Week2">
                <ItemTemplate>
                <%# FormatCurrency(IIf(IsDBNull(Eval("Week2")), 0, Eval("Week2")))%>
                </ItemTemplate>
                <EditItemTemplate>
                <telerik:RadNumericTextBox runat="server" ID="numWeek2"  Culture="en-US" 
                     LabelWidth="64px" Type="Currency" Width="60px" DbValue='<%# Eval("Week2") %>'/>
<telerik:RadTextBox ID="txtWeek2" Width="225px" EmptyMessage="Optional Comments" runat="server" />
                </EditItemTemplate>

        </telerik:GridTemplateColumn>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                <ItemStyle CssClass="MyImageButton" />
            </telerik:GridEditCommandColumn>

    </Columns>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>

</MasterTableView>

<FilterMenu EnableImageSprites="False"></FilterMenu>
        </telerik:RadGrid>



        </td>
        <td valign="top" style="padding-left:25px;" rowspan="2" >Travelers:
<%--        '****************** TRAVELERS TABLE *************************--%>
<telerik:RadGrid ID="RadGrid1" runat="server" CellSpacing="0" GridLines="None" 
  AutoGenerateColumns="false" ShowFooter="true" >
    <ClientSettings EnableRowHoverStyle="true">
    </ClientSettings>
    <MasterTableView DataKeyNames="travelID, rtdsEmployeeID" >
        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
        <RowIndicatorColumn Visible="False" FilterControlAltText="Filter RowIndicator column">
        </RowIndicatorColumn>
        <ExpandCollapseColumn Visible="False" FilterControlAltText="Filter ExpandColumn column">
        </ExpandCollapseColumn>
        <Columns>
            <telerik:GridBoundColumn DataField="rtdsEmployeeID" DataType="System.Guid"  Visible="False" ReadOnly="true"
                FilterControlAltText="Filter rtdsEmployeeID column" HeaderText="rtdsEmployeeID" 
                SortExpression="rtdsEmployeeID" UniqueName="rtdsEmployeeID">
            </telerik:GridBoundColumn>
            <telerik:GridTemplateColumn DataField="LastName" HeaderText="Employee" SortExpression="LastName" UniqueName="LastName">
                <HeaderStyle Width="200px" />
                <ItemStyle Width="200px" />
                    <ItemTemplate>
                        <%# Eval("FirstName")%> <%# Eval("LastName")%> : <%# Eval("Login")%> 
                    </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn DataField="FirstName"  Visible="False" ReadOnly="true"
                HeaderText="FirstName" SortExpression="FirstName" UniqueName="FirstName">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Login"  Visible="False" ReadOnly="true"
                FilterControlAltText="Filter Login column" HeaderText="Login" 
                SortExpression="Login" UniqueName="Login">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="HomeLocaName"  ReadOnly="true"
                FilterControlAltText="Filter HomeLocaName column" HeaderText="Home" 
                SortExpression="HomeLocaName" UniqueName="HomeLocaName">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="TravelLocaName"  ReadOnly="true"
                FilterControlAltText="Filter TravelLocaName column" HeaderText="Travel" 
                SortExpression="TravelLocaName" UniqueName="TravelLocaName">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="startdate" DataType="System.DateTime" ItemStyle-HorizontalAlign="Center"  ReadOnly="true"
                FilterControlAltText="Filter startdate column" HeaderText="startdate"  DataFormatString="{0:dd MMM yyy}"  
                SortExpression="startdate" UniqueName="startdate">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </telerik:GridBoundColumn>
            <telerik:GridTemplateColumn DataField="returndate" DataType="System.DateTime" ItemStyle-HorizontalAlign="Center"  ReadOnly="true"
                FilterControlAltText="Filter returndate column" HeaderText="returndate" 
                SortExpression="returndate" UniqueName="returndate">
                <ItemTemplate>
                    <asp:Label ID="lblRetDate" runat='server' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn DataField="Week1AddCompID"  Visible="False" ReadOnly="true" 
                HeaderText="Week1AddCompID" SortExpression="Week1AddCompID" UniqueName="Week1AddCompID">
            </telerik:GridBoundColumn>
            <telerik:GridTemplateColumn datafield="Week1AddCompAmount" DataType="System.Decimal" Aggregate="Sum" HeaderText="Week 1" UniqueName="Week1AddCompAmount">
                <ItemTemplate>
                    <%# FormatCurrency(IIf(IsDBNull(Eval("Week1AddCompAmount")), 0, Eval("Week1AddCompAmount")))%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadNumericTextBox runat="server" ID="numWeek1AddCompAmount"  Culture="en-US"  Aggregate="Sum"
                        LabelWidth="64px" Type="Currency" Width="60px" DbValue='<%# Eval("Week1AddCompAmount") %>'/>
                    <telerik:RadTextBox ID="txtWeek1AddCompComments" Width="225px" EmptyMessage="Optional Comments" runat="server" Text='<%# Eval("Week1AddCompComments") %>' />
                    <asp:Label ID="lblWeek1UserTimeStamp" runat="server" Text='<%# Eval("Week1UserName").ToString() & " : " & Eval("Week1TimeStamp").ToString() %>' />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn DataField="Week2AddCompID"  Visible="False" ReadOnly="true"
                HeaderText="Week2AddCompID" SortExpression="Week2AddCompID" UniqueName="Week2AddCompID">
            </telerik:GridBoundColumn>
            <telerik:GridTemplateColumn datafield="Week2AddCompAmount" Aggregate="Sum" DataType="System.Decimal" HeaderText="Week 2" UniqueName="Week2AddCompAmount">
                <ItemTemplate>
                    <%# FormatCurrency(IIf(IsDBNull(Eval("Week2AddCompAmount")), 0, Eval("Week2AddCompAmount")))%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadNumericTextBox runat="server" ID="numWeek2AddCompAmount"  Culture="en-US"
                         LabelWidth="64px" Type="Currency" Width="60px" DbValue='<%# Eval("Week2AddCompAmount") %>'/>
                    <telerik:RadTextBox ID="txtWeek2AddCompComments" Width="225px" EmptyMessage="Optional Comments" runat="server" Text='<%# Eval("Week2AddCompComments") %>' />&nbsp;&nbsp;<asp:Label ID="lblWeek2UserTimeStamp" runat="server" Text='<%# Eval("Week2UserName").ToString() & " : " & Eval("Week2TimeStamp").ToString() %>' />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                <ItemStyle CssClass="MyImageButton" />
            </telerik:GridEditCommandColumn>
            <telerik:GridBoundColumn DataField="travelID" DataType="System.Guid"  Visible="False" 
                FilterControlAltText="Filter travelID column" HeaderText="travelID" 
                ReadOnly="True" SortExpression="travelID" UniqueName="travelID">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="HomeLocaID" DataType="System.Guid"  Visible="False" ReadOnly="true"
                FilterControlAltText="Filter HomeLocaID column" HeaderText="HomeLocaID" 
                SortExpression="HomeLocaID" UniqueName="HomeLocaID">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="TravelLocaID" DataType="System.Guid"  Visible="False" ReadOnly="true"
                FilterControlAltText="Filter TravelLocaID column" HeaderText="TravelLocaID" 
                SortExpression="TravelLocaID" UniqueName="TravelLocaID">
            </telerik:GridBoundColumn>
        </Columns>
        <EditFormSettings>
        <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
        </EditFormSettings>
    </MasterTableView>
    <FilterMenu EnableImageSprites="False"></FilterMenu>
</telerik:RadGrid>
        </td>
    </tr>
    <tr>
        <td style="padding-top:50px;">
        Holiday Schedule <span style="font-size:11px">(No holidays this pay period)</span>
        <telerik:RadGrid ID="RadGrid2" runat="server" Width="415px" >
        <MasterTableView AutoGenerateColumns="false" Width="415px">
        <Columns>
        <telerik:GridBoundColumn ItemStyle-Width="150px" DataField="hname" UniqueName="hname" HeaderText="Holiday">
            <HeaderStyle Width="150px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn ItemStyle-Width="85px" DataField="hdate" DataFormatString="{0:dd MMM yyyy}" UniqueName="hdate" HeaderText="Date">
            <HeaderStyle Width="85px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn ItemStyle-Width="95px"  DataField="hdateWeekDayName" UniqueName="hdateWeekDayName" HeaderText="Day">
            <HeaderStyle Width="95px" />
        </telerik:GridBoundColumn>
        <telerik:GridTemplateColumn ItemStyle-Width="85px" DataField="hdateObserve" UniqueName="hdateObserve" HeaderText="Observed On" Visible="false">
            <HeaderStyle Width="85px" />
            <ItemTemplate>
            <asp:Label ID="lblObservedOnDate" runat="server" />
            </ItemTemplate>            
        </telerik:GridTemplateColumn>
        
        </Columns>
        </MasterTableView>

        </telerik:RadGrid>
        </td>
    </tr>


</table>





    

    </div>
<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
<script type="text/javascript">
    var retarg = "";

    function pageLoad() {
        var currentWindow = GetRadWindow();
    }

    function GetRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)
        return oWindow;
    }

    function Close() {
        var oWindow = GetRadWindow();
        alert(retarg);
        if (retarg.indexOf("AdditionalCompensation") > -1) {
            oWindow.close(retarg);
        } else {
            oWindow.argument = null;
            oWindow.close(retarg);
        }
    }

    function setReturnArg(arg) {
        retarg = arg;
    }


</script>
</telerik:RadScriptBlock>
    </form>
</body>
</html>
