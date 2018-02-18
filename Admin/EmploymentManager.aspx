<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EmploymentManager.aspx.vb" Inherits="DiversifiedLogistics.EmploymentManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employment Manager</title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            function onPopUpShowing(sender, args) {
                args.get_popUp().className += " popUpEditForm";
            }
        </script>
    </telerik:RadCodeBlock>        
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                   <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
           </telerik:AjaxSetting>
                       <telerik:AjaxSetting AjaxControlID="RadGrid1">
                           <UpdatedControls>
                               <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelCssClass="" />
                           </UpdatedControls>
                       </telerik:AjaxSetting>
                  </AjaxSettings>
        </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
         <telerik:RadFormDecorator RenderMode="Lightweight" ID="RadFormDecorator1" runat="server" DecorationZoneID="demo" DecoratedControls="All" EnableRoundedCorners="false" />
 
    <div>
<table cellpadding="0" cellspacing="0" style="width: 1180px">
    <tr>
        <td valign="top" style="width:225px;"> 
<%--                            <telerik:AjaxUpdatedControl ControlID="errMsg" />--%>
<%--                            <telerik:AjaxUpdatedControl ControlID="errMsg" />--%>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadComboBox ID="cbLocations" Width="150px" Filter="Contains" runat="server" /> 
                    </td>
                </tr>
            </table>            
            <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" runat="server" GridLines="None" AutoGenerateColumns="False" >
    <MasterTableView DataKeyNames="ID" AutoGenerateColumns="false" >
        <Columns>
            <telerik:GridBoundColumn DataField="ID" Visible="false" UniqueName="ID">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Name" HeaderText="Employee Name" UniqueName="Name">
            </telerik:GridBoundColumn>
           
        </Columns>

    </MasterTableView>
    <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true" >
        <Selecting AllowRowSelect="true" />
<ClientEvents />
    </ClientSettings>
</telerik:RadGrid>

            </td>
        <td style="padding:10px">&nbsp;</td>


        <td style="vertical-align:top;">
            <telerik:RadGrid ID="RadGrid2" runat="server" CellSpacing="-1" GridLines="Both" AllowSorting="True">
    <MasterTableView AutoGenerateColumns="False" DataKeyNames="ID,EmployeeID" Width="810px">
        <Columns>
            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                <HeaderStyle Width="25px" />
                <ItemStyle CssClass="MyImageButton" />
            </telerik:GridEditCommandColumn>
            <telerik:GridBoundColumn DataField="ID" DataType="System.Guid" Visible="false" FilterControlAltText="Filter ID column" HeaderText="ID" ReadOnly="True" SortExpression="ID" UniqueName="ID" />
            <telerik:GridBoundColumn DataField="EmployeeID" DataType="System.Guid" Visible="false" ReadOnly="True" FilterControlAltText="Filter EmployeeID column" HeaderText="EmployeeID" SortExpression="EmployeeID" UniqueName="EmployeeID" />
            <telerik:GridBoundColumn DataField="LocaName" ReadOnly="True" FilterControlAltText="Filter LocaName column" HeaderText="LocaName" SortExpression="LocaName" UniqueName="LocaName">
                <HeaderStyle Width="125px" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="EmployeeName" FilterControlAltText="Filter EmployeeName column" HeaderText="EmployeeName" ReadOnly="True" SortExpression="EmployeeName" UniqueName="EmployeeName">
                <HeaderStyle Width="150px" />
            </telerik:GridBoundColumn>
            <telerik:GridTemplateColumn HeaderText="JobTitle" DataField="JobTitleID" UniqueName="JobTitle" SortExpression="JobTitle">
                <HeaderStyle  Width="125px"/>
                <ItemTemplate>
                    <asp:Label ID="lblJobTitle" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadComboBox ID="gridcbJobTitle" runat="server"
                         Width="145px">
                    </telerik:RadComboBox>
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="PayType" HeaderText="PayType" UniqueName="PayType">
                <HeaderStyle Width="80px" />
                <ItemTemplate>
                    <asp:Label ID="lblPayType" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadComboBox ID="gridcbPayType" runat="server" Width="80px" >
                        <Items>
                            <telerik:RadComboBoxItem Value="2" Text="Hourly" />
                            <telerik:RadComboBoxItem Value="1" Text="Percent" />
                        </Items>
                    </telerik:RadComboBox>
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="PayRateHourly" DataType="System.Double" HeaderText="Hrly" SortExpression="PayRateHourly" UniqueName="PayRateHourly">
                <HeaderStyle  Width="85px"/>
                <ItemTemplate>
                    <%# Eval("PayRateHourly")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadNumericTextBox ID="num_PayRateHourly" TabIndex="8" runat="server" EmptyMessage="$" Type="Currency" Value='<%# Eval("PayRateHourly") %>' Width="50px" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="PayRatePercentage" DataType="System.Double" HeaderText="%" SortExpression="PayRatePercentage" UniqueName="PayRatePercentage">
                <HeaderStyle Width="30px" />
                <ItemTemplate>
                    <%# Eval("PayRatePercentage")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadNumericTextBox ID="num_PayRatePercentage"  TabIndex="9" runat="server" EmptyMessage="%" NumberFormat-DecimalDigits="2" Type="Percent" Value='<%# Eval("PayRatePercentage") %>' Width="55px" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="DateOfEmployment" HeaderText="Start Date" SortExpression="dateofemployment" ShowSortIcon="true" UniqueName="DateOfEmployment">
                <HeaderStyle Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblDateOfEmployment" Width="75" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadDatePicker ID="griddpEmploymentDate" Width="110px" runat="server" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="DateOfDismiss" HeaderText="End Date" UniqueName="DateOfDismiss">
                <HeaderStyle Width="65px" />
                <ItemTemplate>
                    <asp:Label ID="lblDateOfDismiss" Width="75px" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <telerik:RadDatePicker ID="griddpDismissDate" Width="100px" runat="server" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridButtonColumn ImageUrl="~/images/redX.gif" ConfirmDialogWidth="495px"  ConfirmDialogHeight="115px" ConfirmTitle="DELETE this change?" ConfirmText="DELETE this Pending Change to Employment Record!" ConfirmDialogType="RadWindow"
                ButtonType="ImageButton" CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" HeaderText="Delete">
                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                <HeaderStyle Width="50px" />
            </telerik:GridButtonColumn>
            <telerik:GridButtonColumn ImageUrl="~/images/comit.png" ConfirmDialogWidth="495px"  ConfirmDialogHeight="115px" ConfirmTitle="Excecute this change?" ConfirmText="Approve and Commit this Change to Employment Record?&nbsp;" ConfirmDialogType="RadWindow"
                ButtonType="ImageButton" CommandName="Commit" Text="Commit This Change" UniqueName="ExcecuteColumn" HeaderText="Commit">
                <HeaderStyle Width="50px" />
                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
            </telerik:GridButtonColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>

        </td>
    </tr>
</table>



    </div>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" VisibleStatusbar="false" >
<Windows>
<telerik:RadWindow ID="wEmploymentEditor" Height="380px" Width="520px"  
    ShowContentDuringLoad="false"   runat="server" Modal="true" 
    Title="SEU Employment Editor" 
    OnClientClose = "OnClientCloseEmploymentEditor"
    Behaviors="Move, Resize, Close" />
</Windows>
    </telerik:RadWindowManager>

<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
<script type="text/javascript">

    function openEmploymentEditor(arg) {
        var oManager = GetRadWindowManager();
        var loca = "ChangeEmployment.aspx?empID=" + arg
        oManager.open(loca, "wEmploymentEditor");
    }
    function OnClientCloseEmploymentEditor(sender, args) {
        sender.setUrl("../seuLoader.aspx");
        //        var arg = 'closed';
        var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
        ajaxManager.ajaxRequest("EmploymentEdit:" + args);
    }

    function decOnly(i) {
        var t = i.value;
        if (t.length > 0) {
            t = t.replace(/[^\d]+/g, '');
        }
        var s = t.split('.');
        if (s.length > 1) {
            s[1] = s[0] + '.' + s[1];
            s.shift(s);
        }
        i.value = s.join('');
    }

    //                function RowClick(sender, eventArgs) {
    //                    sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
    //                }
    //                
    //                function RowSelected(sender, eventArgs) {
    //                    alert(eventArgs.getDataKeyValue("ID="));
    ////                    sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
    //                }


</script>



</telerik:RadCodeBlock>
    </form>
</body>
</html>
