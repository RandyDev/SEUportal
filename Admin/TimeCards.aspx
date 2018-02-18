<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TimeCards.aspx.vb" Inherits="DiversifiedLogistics.TimeCards" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">

</script>
    </telerik:RadCodeBlock>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" EnableHandlerDetection="true" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelHeight="" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="gridAddComp" UpdatePanelHeight="" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlCurrentPayPeriod" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cbLocations">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelHeight="" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectLocation" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectEmployee" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWOedit" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblSelectLocation" 
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectEmployee" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWOedit" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnSaveComments">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lbtnSaveComments" 
                        LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server">
<img src="../images/ajax-loader-smallBar.gif" />
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel3" runat="server">
<img src="../images/ajax-loader-smallBar.gif" width="165" height="24" />
    </telerik:RadAjaxLoadingPanel>
    <div>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td valign="top" width="25%">
        <%-- *******************start Left Side WO Grid **********************--%>
<table class="lbl">
    <tr>
        <td>
            <telerik:RadComboBox ID="cbLocations" Width="150px" runat="server" Filter="Contains" />
        </td>
    </tr>
<tr>
<td colspan="2">
    <telerik:RadGrid ID="RadGrid1" runat="server" GridLines="None" Width="275px" >
<MasterTableView AutoGenerateColumns="False" DataKeyNames="empID" Width="275px" >
    <Columns>
        <telerik:GridBoundColumn DataField="empID" DataType="System.Guid" 
           ReadOnly="True" UniqueName="empID" Visible="false">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="empName" HeaderText="Name" UniqueName="Name">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Department" HeaderText="Clocked In" UniqueName="Department">
        </telerik:GridBoundColumn>
</Columns>
</MasterTableView>
            <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true" >
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
        </telerik:RadGrid>

</td></tr>
</table>

<%-- *******************end Left Side WO Grid **********************--%>
        </td>

        <td></td>

        <td valign="top" style="padding-left:11px;">
<div style="min-width:850px; padding-right:50px; text-align:right;">
                        <span onmouseover="this.style.cursor='help';"><asp:Label class="resp" CssClass="lilBlueButton" ID="lblHelp" Text="help with this page" runat="server" /></span>
</div>
<%-- *******************start Right Side Employee Form **********************--%>
<asp:Label ID="lblSelectLocation" runat="server" Text="<<<----  Select Location" />
<asp:Label ID="lblSelectEmployee" runat="server" Text="<br /><br /><br /><br /><<<----  Select an Employee from left" />

<%-- *******************start Right Side EDIT Form **********************--%>
<asp:Panel ID="pnlWOedit" runat="server" Visible="False">

<fieldset style="border:1px solid black; padding:0px 12px 1px 12px;" >
    <legend style="font-size:24px; font-family:Arial; font-weight:bold;">
        <asp:Label ID="lblEmpName" runat="server" />
    </legend>
<table>
    <tr>
    <td valign="top"><telerik:RadBinaryImage ID="imgMugShot"  Width="75" ResizeMode="Fit" 
            runat="server" /></td>
    <td valign="top">
<fieldset style="padding:0 5px 5px 5px; font-family:Arial; font-size:11px;" >
<legend style="font-size:14px; font-weight:bold;">
Comments</legend>
<table cellpadding="0" cellspacing="0">
<tr>
<td align="right"><div id="divSaveComments" class="showme" runat="server">
<asp:LinkButton ID="lbtnSaveComments" CssClass="lilBlueButton" Text="save comments" runat="server" /></div></td>
</tr><tr>
<td>
<telerik:RadTextBox ID="txt_Comments" TextMode="MultiLine" Rows="5" Width="300px" EmptyMessage="Comments" runat="server" /><br />
</td>
</tr>
</table>
</fieldset> 
    </td>
<td valign="top">
<fieldset style="padding:0 5px 5px 5px; font-family:Arial; font-size:11px;" >
<legend style="font-size:14px; font-weight:bold;">
Certifications </legend>
<asp:Label ID="lblCerts" runat="server" />
</fieldset>
<fieldset style="padding:0 5px 5px 5px; font-family:Arial; font-size:11px;" >
<legend style="font-size:14px; font-weight:bold;">
Additional / Other Compensation this Pay Period </legend>
<%--<span style="text-align:left;" onmouseover="this.style.cursor='pointer';"><asp:Label ID="lblAddComp" style="font-family:Arial;font-size:10px;color:blue;" runat="server" /></span>--%>
<telerik:RadGrid ID="gridAddComp" runat="server" 

        CellSpacing="0" GridLines="None" >

    <ClientSettings>
        <Selecting AllowRowSelect="True" />
    </ClientSettings>
    <MasterTableView AutoGenerateColumns="False" DataKeyNames="AddCompID" >
        <CommandItemSettings ExportToPdfText="Export to PDF" />
        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
            Visible="False">
        </RowIndicatorColumn>
        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
            Visible="False">
        </ExpandCollapseColumn>
        <Columns>
            <telerik:GridBoundColumn DataField="AddCompID" DataType="System.Guid" Visible="false"
                FilterControlAltText="Filter AddCompID column" HeaderText="AddCompID" 
                ReadOnly="True" SortExpression="AddCompID" UniqueName="AddCompID">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="EmployeeID" DataType="System.Guid" Visible="false" 
                FilterControlAltText="Filter EmployeeID column" HeaderText="EmployeeID" 
                SortExpression="EmployeeID" UniqueName="EmployeeID">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="CompDesc"
                FilterControlAltText="Filter CompDesc column" HeaderText="Description" 
                SortExpression="CompDesc" UniqueName="CompDesc">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="AddCompAmount" DataType="System.Decimal" 
                FilterControlAltText="Filter AddCompAmount column" HeaderText="Amount" 
                SortExpression="AddCompAmount" UniqueName="AddCompAmount" DataFormatString="{0:C2}">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="AddCompStartDate" 
                DataType="System.DateTime" DataFormatString="{0:ddd MM/dd}"
                FilterControlAltText="Filter AddCompStartDate column" 
                HeaderText="Start" SortExpression="AddCompStartDate" 
                UniqueName="AddCompStartDate">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="AddCompEndDate" DataType="System.DateTime" 
                FilterControlAltText="Filter AddCompEndDate column" HeaderText="End"  DataFormatString="{0:ddd MM/dd}"
                SortExpression="AddCompEndDate" UniqueName="AddCompEndDate">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="AddCompComments" 
                FilterControlAltText="Filter AddCompComments column" 
                HeaderText="Comments" SortExpression="AddCompComments" 
                UniqueName="AddCompComments">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="userID" DataType="System.Guid" Visible="false"
                FilterControlAltText="Filter userID column" HeaderText="userID" 
                SortExpression="userID" UniqueName="userID">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="TimeStamp" DataType="System.DateTime" Visible="false"
                FilterControlAltText="Filter TimeStamp column" HeaderText="TimeStamp" 
                SortExpression="TimeStamp" UniqueName="TimeStamp">
            </telerik:GridBoundColumn>
            <telerik:GridCheckBoxColumn DataField="Credit" DataType="System.Boolean" Visible="false"
                FilterControlAltText="Filter Credit column" HeaderText="Credit" 
                SortExpression="Credit" UniqueName="Credit">
            </telerik:GridCheckBoxColumn>
            <telerik:GridCheckBoxColumn DataField="InActive" DataType="System.Boolean" Visible="false" 
                FilterControlAltText="Filter InActive column" HeaderText="InActive" 
                SortExpression="InActive" UniqueName="InActive">
            </telerik:GridCheckBoxColumn>
        </Columns>
        <EditFormSettings>
            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
            </EditColumn>
        </EditFormSettings>
    </MasterTableView>
    <FilterMenu EnableImageSprites="False">
    </FilterMenu>
    </telerik:RadGrid>

</fieldset>


</td>
    </tr>
    <tr>
    <td colspan="2">
    <span id="spKey" style="font-size:11px; font-family:Arial;" runat="server">
<em>each</em> day an employee is given a TimeCard for <em>each department</em> they work in <em>that day</em>.
</span>

    </td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0">
<tr>
<td>
<table cellpadding="0" cellspacing="0" width="100%"><tr>
<td>
<span style="font-size:11px; font-family:Arial;" runat="server">Click Date/Department to edit TimeCard</span>
</td>
<td align="right"><span onmouseover="this.style.cursor='pointer';">
<asp:Label ID="lblAddNewLinkButton" Visible="true" style="font-family:Arial;font-size:10px;color:blue;" runat="server" /></span>
</td>
</tr></table>

</td>
</tr>
<tr>
<td>
<table border="1" style="font-size: 11px;">
    <tr>
        <td valign="top">
<telerik:RadAjaxPanel ID="pnlCurrentPayPeriod" runat="server">
            <fieldset style="padding-left:5px;">
                <legend style="font-family:Arial; font-size:14px; font-weight:bold;">Current Pay-Period :  <asp:Label ID="lblcwk" style="font-size:11px;" runat="server" /> - <asp:Label style="font-size:11px;" Text="" ID="lblttlcptime" runat="server" /></legend>
                <table style="font-family:arial;">
                    <tr>
                        <asp:Label style="font-size:11px;" ID="lblCurpp" runat="server" />
                     </tr>
                 </table>
             </fieldset>
</telerik:RadAjaxPanel>
        </td>
    </tr>
    <tr>
        <td valign="top">
        <telerik:RadAjaxPanel ID="pnlPreviousPayPeriod" runat="server">
            <fieldset style="padding-left:5px;">
                <legend style="font-family:Arial; font-size:14px;">Previous Pay-Period : <asp:Label ID="lblpwk" style="font-size:11px;" runat="server" /> - <asp:Label style="font-size:11px;" ID="lblttlpptime" Text="" runat="server" /></legend>
                <table style="font-family:arial;">
                    <tr>
                        <asp:Label style="font-size:11px;" ID="lblPrepp" runat="server" />
                     </tr>
                 </table>
             </fieldset>
        </telerik:RadAjaxPanel>
        
        </td>
    </tr>
</table>
</td>
</tr>
</table>

</fieldset>
</asp:Panel>
<%-- *******************END Right Side WO Form **********************--%>
        </td>
    </tr>
</table>

<telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="lblHelp" RelativeTo="Element"  
    ShowEvent="OnClick" Position="BottomLeft" HideEvent="ManualClose"  
     Animation="Resize" EnableShadow="true">
<table cellpadding="0" cellspacing="0" width="100%"><tr>
<td><span class="ttHeader">Time Card Manager</span>
</td>
<td align="right" style="padding-right:12px;">
</td>
</tr></table>

<table><tr><td style="padding:0 8px;">
<table>
    <tr>
        <td class="ttTitle">Use FULL Screen</td>
    </tr>
    <tr>
        <td class="ttBody">
            The F11 key on your keyboard will toggle most browsers to full screen.<br />
            F11 again to toggle back. &nbsp;Try it now!&nbsp; &nbsp;(MSIE and Chrome tested)
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Employee List (left side)</td>
    </tr>
    <tr>
        <td  class="ttBody">
            This list shows the names of all employees and the last (most recent) department 
            to which they were assigned.<br /> 
            The deparment name (&#39;Clocked in&#39; column) is color coded as follows:
            <br />
            <span style="color:#AAAAAA">Grocery</span> &nbsp;&lt;&mdash; grayed out indicates most recent time card is closed out. ('Out for the day' is checked)<br />
            <span style="color:Orange">Grocery</span> &nbsp;&lt;&mdash; orange indicates the employee is not clocked in but not out for the day. (lunch or break)<br />
            <span style="color:Green">Grocery</span> &nbsp; &lt;&mdash; green indicates the employee is currently Clocked IN (on the clock)<br />
            *<span style="color:Green">Grocery</span> &nbsp;&lt;&mdash; *green preceeded by an asterisk (*) indicates Clocked In AND being <font color="#A54242">paid by the hour</font><br />
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Time Sheet (right side)</td>
    </tr>
    <tr>
        <td  class="ttBody">
        The Time Sheet displays the employee's name, a photo (if available), a comments editor, a list of equipment<br />
        certifications and ... all the employee's time cards for the current and previous pay periods.<br />
        You may add/edit/change the comments at any time then simply click the '<font color="blue">save comments</font>' link.
                <div style="height:8px;line-height:1px;"></div>
        A 'Time Card' is created for an employee for each department they work in on any given day.<br />
        This is to say that if on a Monday an employee works in both the Grocery AND Freezer departments, <br />
        that employee's Time Sheet will show TWO 'Time Cards' for that day.<br />
        Time cards are displayed left to right, newest to oldest.
        <div style="height:8px;line-height:1px;"></div>
            Each time card consists of a date, color coded department name and color coded 
            set(s) of time punches.<br />
        The department name is <font color="orange">orange</font> if the time card NOT checked '<font color="orange">Out for the day'</font>, otherwise it is <font color="#AAAAAA">grayed out</font>.<br />
        The times will be black when the employee is being paid on a percentage basis. <br />
        If the employee is being <font color="#A54242">paid by the hour</font>, the times will be <font color="#A54242">brick red</font>.<br />
        If the employee is still on the clock a blue <font color="blue">clock out</font> link will be present.<br />
            Below each set of times will be the total hours and minutes worked during that 
            shift, enclosed in square<br />
            brackets [xx:xx]. &nbsp;If the employee is still clocked in, elapsed hours and minutes are shown.<br />
        If the brackets are orange: <font color="orange">[</font>xx:xx<font color="orange">]</font> the employee worked more that five (5) hour without a break.<br />
        If the brackets are red: <font color="red">[</font>xx:xx<font color="red">]</font> the employee worked more that six (6) hour without a break.
                <div style="height:8px;line-height:1px;"></div>

<table><tr style="line-height:12px;">
<td valign="top">NOTE: </td>
<td>
        Unlike the rest of the page, the previous pay period section does NOT update when you edit a time card.<br />
        If you need to refresh this section, re-click on the employee's name in Employee List.
</td>

</tr></table>

        </td>
    </tr>
</table><br />
<center>To Close - Click X in upper right corner</center>


</td></tr></table>

</telerik:RadToolTip>



<telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>

<telerik:RadWindow ID="wTimePunche" Width="465px" Height="485px" 
    ShowContentDuringLoad="false"   runat="server" Modal="true" 
    Title="Add/Edit Department TimeCard"
    style="min-height:297px;"
    VisibleStatusbar="false"
    OnClientClose = "OnClientCloseTimePunche"
    Behaviors="Move, Resize, Close" />

<%--<telerik:RadWindow ID="wAdditionalCompensation" Width="465px" Height="485px" 
    ShowContentDuringLoad="false"   runat="server" Modal="True" 
    Title="Add/Edit Compensation"
    style="min-height:297px;"
    VisibleStatusbar="false"
    OnClientClose = "OnClientCloseCompensation"
    Behaviors="Move, Resize, Close" />--%>

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
//            GetRadWindow().close();
        }else{
            document.getElementById("< %= btnSaveChanges.ClientID % >").disabled = false;

        }

    }

    function GetRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;//Will work in Moz in all cases, including clasic dialog
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;//IE (and Moz az well)
        return oWindow;
    }

    function toggleSaveBtnOn() {
        document.getElementById('divSaveComments').className='showme';
    }

//    function openCompensation(tpID) {
//        //        document.getElementById("< %= btnSaveChanges.ClientID % >").disabled = true;
//        var oManager = GetRadWindowManager();
//        var loca = "AddEditAdditionalCompensation.aspx?empID=" + acID
//        oManager.open(loca, "wAdditionalCompensation");

//    }
//    function openNewCompensation(empID, locaID) {
//        //        document.getElementById("< %= btnSaveChanges.ClientID % >").disabled = true;
//        var oManager = GetRadWindowManager();
//        var loca = "AddEditAdditionalCompensation.aspx?empID=" + empID + "&locaID=" + locaID

//        oManager.open(loca, "wAdditionalCompensation");

//    }
//    function OnClientCloseCompensation(sender, args) {
//        if (args.get_argument() != null) {
//            var arg = args.get_argument();
////            var ajaxManager = $find("< %= RadAjaxManager1.ClientID %>");
//            ajaxManager.ajaxRequest("Compensation:" + arg);
//            //                oWnd.close();
//        } else {
//            //            document.getElementById("< %= btnSaveChanges.ClientID % >").disabled = false;

//        }

//    }

</script>
</telerik:RadCodeBlock> 

    </div>
    </form>
</body>
</html>
