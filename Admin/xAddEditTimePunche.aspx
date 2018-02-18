<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AddEditTimePunche.aspx.vb" Inherits="DiversifiedLogistics.AddEditTimePunche" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style type="text/css">
.lilBlueLink {
font-size:11px;
color: Blue;
}
.tpActionLink{
font-size:11px;
color: Blue;
}
.ErrorMsg {
font-size:12px;
color:Red;
font-family:Comic Sans MS; 
text-align:center;
}
</style>
</head>
<body>
<form id="form1" runat="server">
<div style="padding-right:10px;font-family:Arial;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="chkIsClosed">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="chkIsClosed" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="cbDepartment" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="cbJobDescription" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblJobDescription" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lbtpAction" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblErrorMsg" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="gridTIO" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="cbDepartment">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lbtpAction" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblErrorMsg" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtpAction">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridTIO" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblErrorMsg" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cbJobDescription">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cbDepartment" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lbtpAction" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblDepartment" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblJobDescription" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblErrorMsg" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="gridTIO">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridTIO" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblErrorMsg" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
<table>
  <tr>
    <td colspan="3">
<table cellpadding="0" cellspacing="0">
  <tr>
    <td><b><asp:Label ID="lblempName" style="font-size:16px;" runat="server" /></b></td>
    <td style="padding-left:8px;"><asp:CheckBox ID="chkIsClosed" runat="server" style="font-size:12px;" Text="Out for the day" /></td>
    <td align="right"> </td>
  </tr>
</table>
    </td>
  </tr>
  <tr>
    <td valign="top">
      <asp:Label ID="lblDateWorked" style="font-size:14px; font-weight:bold;" runat="server" />
      <telerik:RadDatePicker ID="dpDateWorked" Width="95px" runat="server" Visible="false" />
    </td>
    <td style="padding-left:12px;" valign="top">
        <table>
            <tr><td><asp:Label ID="lblDepartment" runat="server" Text="Department" Font-size="10px" /></td></tr>
            <tr><td><telerik:RadComboBox ID="cbDepartment" AutoPostBack="true" Width="215px" runat="server" EmptyMessage="Select Department" AllowCustomText="true" /></td></tr>
        </table>
    </td>
    <td style="padding-left:12px;" valign="top">
<asp:LinkButton  ID="lbtpAction" CssClass="tpActionLink" CommandName="Delete" 
    OnClientClick="if (!confirm('You are about to delete this Time Card AND               \nall associated in/out time punches!!\n\nDo you want to that??')) return false;" 
    runat="server" Text="Delete this TimeCard" />
</td>
  </tr>
</table>

<table style="width:306px;">
<tr>
<td>
<asp:Label ID="lblErrorMsg" Visible="false" CssClass="ErrorMsg" runat="server" />
<telerik:RadGrid ID="gridTIO" runat="server" ShowStatusBar="True" Width="315px" AllowAutomaticUpdates="true"
            AutoGenerateColumns="False" DataSourceID="SqlDataSource1" GroupPanelPosition="Top">
<GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>

    <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID, JobDescriptionID" Width="100%" 
        DataSourceID="SqlDataSource1" > 
        <CommandItemSettings ShowRefreshButton="false" />
        <RowIndicatorColumn>
            <HeaderStyle Width="20px" />
        </RowIndicatorColumn>
        <ExpandCollapseColumn>
            <HeaderStyle Width="20px" />
        </ExpandCollapseColumn>
        <Columns>
            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" >
                <ItemStyle CssClass="MyImageButton" />
            </telerik:GridEditCommandColumn>

            <telerik:GridBoundColumn DataField="ID" DataType="System.Guid" visible="false"
                UniqueName="ID" ReadOnly="True">
            </telerik:GridBoundColumn>
<%--            *********************** Data columns--%>
            <telerik:GridTemplateColumn DataField="TimeIn" DataType="System.DateTime" 
                HeaderText="TimeIn" SortExpression="TimeIn" UniqueName="TimeIn">
                <ItemTemplate>
                <%# Format(Eval("TimeIn"), "h:mm tt")%>
                </ItemTemplate>
                <InsertItemTemplate>
                    <telerik:RadTimePicker runat="server" ID="tpTimeIn" Culture="en-US" MinDate="1/1/1900" MaxDate="1/1/2050" 
                        SelectedDate='<%#Date.Now %>' />
                    <asp:RequiredFieldValidator Display="Static" ID="requiredtpTimeInValidator" runat="server" ErrorMessage="Required" ControlToValidate="tpTimeIn" />
                </InsertItemTemplate>
                <EditItemTemplate>
                    <telerik:RadTimePicker runat="server" ID="tpTimeIn" Culture="en-US" MinDate="1/1/1900" MaxDate="1/1/2050" 
                        DbSelectedDate='<%# Eval("TimeIn") %>' />
                    <asp:RequiredFieldValidator Display="Static" ID="requiredtpTimeInValidator" runat="server" ErrorMessage="*" ControlToValidate="tpTimeIn" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>

            <telerik:GridTemplateColumn DataField="TimeOut" DataType="System.DateTime" 
                HeaderText="TimeOut" UniqueName="TimeOut">
                <ItemTemplate>
                    <%# IIf(Eval("TimeOut") < Eval("TimeIn"), " --- ", Format(Eval("TimeOut"), "h:mm tt"))%>
                </ItemTemplate>
                <EditItemTemplate>
                <telerik:RadTimePicker runat="server" ID="tpTimeOut" MinDate="1/1/1900" Culture="en-US" 
                        DbSelectedDate='<%# Eval("TimeOut") %>'/>
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            
            <telerik:GridTemplateColumn DataField="JobDescription" DataType="System.String"
                HeaderText="Job Description" UniqueName="JobDescription">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "JobDescription") %>
                </ItemTemplate>
                <InsertItemTemplate>
                    <telerik:RadCombobox runat="server" ID="cbJobDescription" EmptyMessage="Select JobDescription" DataSourceID="SqlDataSource2" DataTextField="JobDescription" DataValueField="JobDescriptionID" />
                    <asp:RequiredFieldValidator ID="requiredJobDescriptionValidator" runat="server" ErrorMessage="Select Job Description" ControlToValidate="cbJobDescription" />
                </InsertItemTemplate>
            </telerik:GridTemplateColumn>
             
<%--            *********************** end Data columns--%>
                    <telerik:GridButtonColumn ConfirmText="Delete this shift?" ConfirmDialogType="RadWindow"
                        ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                        UniqueName="DeleteColumn">
                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                    </telerik:GridButtonColumn>
        </Columns>

<EditFormSettings>
<EditColumn UniqueName="EditCommandColumn1"></EditColumn>
</EditFormSettings>
    </MasterTableView>


</telerik:RadGrid>    

</td>
</tr><tr>
<td align="center">
<span class="lilBlueLink" onmouseover="this.style.cursor='pointer';" onclick="Close();">Close Window</span>
</td>

</tr></table>
    </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:rtdsConnectionString %>" 
            SelectCommand="SELECT TimeInOut.ID, TimeInOut.TimeIn, TimeInOut.TimeOut, JobDescription AS JobDescription, TimeInOut.JobDescriptionID FROM TimeInOut INNER JOIN JobDescriptions ON TimeInOut.JobDescriptionID = JobDescriptions.ID WHERE (TimeInOut.TimepuncheID = @TimepuncheID) ORDER BY TimeInOut.TimeIn">
            <SelectParameters>
                 <asp:SessionParameter SessionField="tpID" Name="TimepuncheID" />
            </SelectParameters>
        </asp:SqlDataSource>    
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:rtdsConnectionString %>" 
            SelectCommand="SELECT LocationJobDescriptions.JobDescriptionID, JobDescriptions.JobDescription FROM LocationJobDescriptions INNER JOIN JobDescriptions ON LocationJobDescriptions.JobDescriptionID = JobDescriptions.ID WHERE (LocationJobDescriptions.LocationID = @tLocaId)">
            <SelectParameters>
                 <asp:SessionParameter SessionField="tLocaId" Name="tLocaId" />
            </SelectParameters>
        </asp:SqlDataSource>


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
        if (retarg.indexOf("TimePunche") > -1) {
            oWindow.close(retarg);
        }else{
//            oWindow.argument = null;
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
