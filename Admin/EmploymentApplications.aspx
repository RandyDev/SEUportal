<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EmploymentApplications.aspx.vb" Inherits="DiversifiedLogistics.EmploymentApplications" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="../styles/StyleSheet.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.resp{font-size:12px; font-weight:bold;}
.SectionHeader{font-size:14px;background-color:#cccccc;}
.shortRow{line-height:13px;} 
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlViewApp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cbLocations">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlViewApp" />
                    <telerik:AjaxUpdatedControl ControlID="lblCopy" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlViewApp" />
                    <telerik:AjaxUpdatedControl ControlID="lblCopy" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="lbtnDelete">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlViewApp" />
                    <telerik:AjaxUpdatedControl ControlID="lblCopy" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lnkbtnClearRating">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadRating1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lnkbtnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlViewApp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnReject">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlViewApp" />
                    <telerik:AjaxUpdatedControl ControlID="lblCopy" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div>
<table width="100%" style="min-width:1280px;">
    <tr>  
<%-- ******************** Left Side ******************** --%>
        <td valign="top" width="465">
<%--********************************************* Selection Controls *********************************************--%>
<%--********************************************* WATCH THESE WIDTHS *********************************************--%>
<%--********************************************* Getting outta control *********************************************--%>
<table cellpadding="0" cellspacing="0" width="465">
    <tr>
        <td style="padding-bottom:8px;">Company: &nbsp; &nbsp; <asp:LinkButton ID="lbtnClear" runat="server" Text="Clear" /> <br />
            <telerik:RadComboBox ID="cbCompany" EmptyMessage="Select Company" AutoPostBack="true" Width="194px" runat="server" /> 
        </td>
        <td style="padding-bottom:8px;" colspan="2">Location:<br />
            <telerik:RadComboBox ID="cbLocations" EmptyMessage="Select Location" AutoPostBack="true" Width="194px" Filter="Contains" runat="server" /> 

        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadComboBox ID="cbAppPool" Width="194px" runat="server" AutoPostBack="true">
                <Items>
                   <telerik:RadComboBoxItem Text="Application Pool =< 6 mos" />
                   <telerik:RadComboBoxItem Text="Archived Applications > 6 mos" />
                   <telerik:RadComboBoxItem Text="Rejected Applications" />
                </Items> 
            </telerik:RadComboBox>
        </td>
        <td style="padding-left:20px;">
            <telerik:RadTextBox ID="txtNameFilter" Width="150px" EmptyMessage="partial first OR last name" Runat="server" />
        </td>
        <td align="right">
            <asp:Button Text="Search/REFRESH" ID="btnShowRecords" runat="server" />
        </td>
    </tr>
</table>
<%--********************************************* Grid List *********************************************--%>
<telerik:RadGrid ID="RadGrid1" runat="server" AllowSorting="True" Width="475px"
                AutoGenerateColumns="False" CellSpacing="0" GridLines="None" 
                ShowGroupPanel="True" Height="550px" >
    <HeaderContextMenu EnableAutoScroll="true"></HeaderContextMenu>
    <MasterTableView DataKeyNames="EmploymentApplicationID">

<RowIndicatorColumn Visible="False" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

<ExpandCollapseColumn Visible="False" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
        <Columns>
            <telerik:GridBoundColumn DataField="EmploymentApplicationID" Visible="false">

            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Rating" HeaderText="1-5" Groupable="false">
                <HeaderStyle Width="48px" />
            </telerik:GridBoundColumn>

            <telerik:GridBoundColumn DataField="Name" HeaderText="Name" Groupable="false">
                <HeaderStyle Width="140px" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="City" HeaderText="City" Groupable="true">
                <HeaderStyle Width="110px" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="State" HeaderText="State" Groupable="true">
                <HeaderStyle Width="45px" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="TimeStamp" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Date" Groupable="false">
                <HeaderStyle Width="65px" />
            </telerik:GridBoundColumn>
        </Columns>
<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>
    
    </MasterTableView>
    <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true" 
        AllowDragToGroup="True" >
        <Selecting AllowRowSelect="true" />
        <scrolling allowscroll="True" usestaticheaders="True" />
    </ClientSettings>
<FilterMenu EnableImageSprites="False"></FilterMenu>
</telerik:RadGrid>
        </td>
<%-- ******************** Right Side ******************** --%>
        <td valign="top" style="padding-left:7px;">
<%--********************************************* Help button *********************************************--%>
            <table width="95%" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left" style="padding-left:12px;"></td>
                    <td style="padding-right:50px;" align="right">
                        <span onmouseover="this.style.cursor='help';"><asp:Label class="resp" CssClass="lilBlueButton" ID="lblHelp" Text="help with this page" runat="server" /></span>
                    </td>
                </tr>
            </table>
<%--********************************************* Application and Rating/Notes *********************************************--%>
           <asp:Panel ID="pnlViewApp" runat="server" Visible="false">
<table style="font-size:11px;" width="100%">
    <tr>
        <td width="550">
        <div style="max-width:550px;width:550px;">
<table cellpadding="0" cellspacing="0" align="center" width="90%">
    <tr>
        <td>
            <asp:Label class="resp" ID="lblTimeStamp" runat="server" />
            <asp:Label class="resp" ID="lblID" runat="server" Visible="false" />
            <asp:Label class="resp" ID="lblLocationID" runat="server" Visible="false" />
        </td>
        <td>
            <asp:Label ID="lblViewPrint" runat="server" />
        </td>
        <td align="right">
            <asp:Label ID="lblEditApp" runat="server" />
        </td>
    </tr>
</table>
<%--********************************************* Application *********************************************--%>
<table cellpadding="0" cellspacing="0" style="border:1px solid gray;" width="550"><tr><td>
<%--********************************************* Personal Info *********************************************--%>
<table width="100%">
    <tr>
        <td colspan="2" class="SectionHeader">
            Personal Information
        </td>
    </tr>
    <tr>
        <td valign="top" width="50%">
            <asp:Label class="resp" ID="lblFirstName" runat="server" />&nbsp;<asp:Label class="resp" ID="lblMiddleInitial" runat="server" />&nbsp;<asp:Label class="resp" ID="lblLastName" runat="server" /><br />
            <asp:Label class="resp" ID="lblStreetAddress" runat="server" /><br />
            <asp:Label class="resp" ID="lblCity" runat="server" />, <asp:Label class="resp" ID="lblState" runat="server" /> &nbsp;<asp:Label class="resp" ID="lblZip" runat="server" /><br />
            <asp:Label class="resp" ID="lblEmail" runat="server" />
        </td>
        <td style="padding-left:8px;">
            <table>
                <tr>
                    <td>Primary Phone:</td><td> <asp:Label class="resp" ID="lblPrimaryPhone" runat="server" /></td>
                </tr><tr>
                    <td>Alt Phone:</td><td> <asp:Label class="resp" ID="lblAltPhone" runat="server" /></td>
                </tr><tr>
                    <td>Referred By:</td><td> <asp:Label class="resp" ID="lblReferredby" runat="server" /></td>
            </tr></table>
        </td>
    </tr>
</table>
<%--********************************************* Employment Desired *********************************************--%>
<table width="100%">
    <tr>
        <td colspan="3" class="SectionHeader">
            Employment Desired
        </td>
    </tr>
    <tr>
        <td>Position: &nbsp;<asp:Label class="resp" ID="lblDesiredPosition" runat="server" />
        </td>
        <td>Date Available: &nbsp;<asp:Label class="resp" ID="lblDesiredStartDate" runat="server" />
        </td>
        <td>Salary: &nbsp;<asp:Label class="resp" ID="lblDesiredSalary" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Currently Employed: &nbsp;<asp:Label class="resp" ID="lblCurrentlyEmployed" runat="server" /></td>
        <td colspan="2">May call current employer: &nbsp;<asp:Label class="resp" ID="lblAskCurrentEmployer" runat="server" /></td>
    </tr>
    <tr>
        <td>Applied Before: &nbsp;<asp:Label class="resp" ID="lblAppliedBefore" runat="server" /></td>
        <td>Where: &nbsp;<asp:Label class="resp" ID="lblAppliedBeforeLocation" runat="server" /></td>
        <td>When: &nbsp;<asp:Label class="resp" ID="lblAppliedBeforeDate" runat="server" /></td>
    </tr>

</table>
<%--********************************************* Education/Training *********************************************--%>
<table width="100%">
    <tr>
        <td colspan="5" class="SectionHeader">
            Education / Training
        </td>
    </tr>
    <tr>
        <td colspan="5">
            Highest level of education: &nbsp;<asp:Label class="resp" ID="lblEducation" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="width:59px;"></td>
        <td>School / Location</td>
        <td>Yrs</td>
        <td>Graduate</td>
        <td>Subjects Studied</td>
    </tr>
    <tr class="shortRow">
        <td style="width:59px;font-size:10px; text-align:center; vertical-align:middle;">High School</td>
        <td>
            <asp:Label class="resp" ID="lblSchool1" runat="server" /><br /><asp:Label class="resp" ID="lblSchool1Location" runat="server" />
        </td>
        <td style="padding-left:8px;">
            <asp:Label class="resp" ID="lblSchool1YearsAttended" runat="server" />
        </td>
        <td style="padding-left:8px;">
            <asp:Label class="resp" ID="lblSchool1Graduated" runat="server" />
        </td>
        <td style="padding-left:8px;">
            <asp:Label class="resp" ID="lblSchool1SubjectsStudied" runat="server" />
        </td>
    </tr>
    <tr style="line-height:1px; background-color:#cfcfcf;"><td colspan="5"></td></tr>
    <tr class="shortRow">
        <td style="width:59px;font-size:10px; text-align:center; vertical-align:middle;">College</td>
        <td>
            <asp:Label class="resp" ID="lblSchool2" runat="server" /><br /><asp:Label class="resp" ID="lblSchool2Location" runat="server" />
        </td>
        <td style="padding-left:8px;">
            <asp:Label class="resp" ID="lblSchool2YearsAttended" runat="server" />
        </td>
        <td style="padding-left:8px;">
            <asp:Label class="resp" ID="lblSchool2Graduated" runat="server" />
        </td>
        <td style="padding-left:8px;">
            <asp:Label class="resp" ID="lblSchool2SubjectsStudied" runat="server" />
        </td>
    </tr>
    <tr style="line-height:1px; background-color:#cfcfcf;"><td colspan="5"></td></tr>
    <tr class="shortRow">
        <td style="width:59px;font-size:10px; text-align:center; vertical-align:middle;">Trade School</td>
        <td>
            <asp:Label class="resp" ID="lblSchool3" runat="server" /><br /><asp:Label class="resp" ID="lblSchool3Location" runat="server" />
        </td>
        <td style="padding-left:8px;">
            <asp:Label class="resp" ID="lblSchool3YearsAttended" runat="server" />
        </td>
        <td style="padding-left:8px;">
            <asp:Label class="resp" ID="lblSchool3Graduated" runat="server" />
        </td>
        <td style="padding-left:8px;">
            <asp:Label class="resp" ID="lblSchool3SubjectsStudied" runat="server" />
        </td>
    </tr>
    <tr style="line-height:1px; background-color:#afafaf;"><td colspan="5"></td></tr>
    <tr class="shortRow">
        <td colspan="5">
            Subjects of special study, research work, special training or skills
        </td>
    </tr>
    <tr class="shortRow">
        <td colspan="5">
            <asp:Label class="resp" ID="lblSpecialSkills" runat="server" />
        </td>
    </tr>
    <tr style="line-height:1px; background-color:#cfcfcf;"><td colspan="5"></td></tr>
    <tr class="shortRow">
        <td colspan="5">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr class="shortRow">
            <td width="130">Military Branch<br />&nbsp; <asp:Label class="resp" ID="lblMilitaryBranch" runat="server" /></td>
            <td width="230">Dates of service<br />&nbsp; <asp:Label class="resp" ID="lblMilitaryDatesOfService" runat="server" /></td>
            <td>Rank <br />&nbsp; <asp:Label class="resp" ID="lblMilitaryRank" runat="server" /></td>
            </tr>
        </table>        
        </td>
    </tr>

</table>
<%--********************************************* Former Employment *********************************************--%>
<table width="100%">
    <tr>
        <td colspan="4" class="SectionHeader">
            Former Employment
        </td>
    </tr>
    <tr class="shortRow">
        <td>From - To</td>
        <td>Employer/Location</td>
        <td>Phone/Salary/Position</td>
        <td>Reason for Leaving</td>
    </tr>
    <tr class="shortRow">
        <td width="90">
            <asp:Label ID="lblPE1Dates" runat="server" />
        </td>
        <td>
            <asp:Label class="resp" ID="lblPE1" runat="server" /><br /><asp:Label ID="lblPE1Location" runat="server" />
        </td> 
        <td>
            <asp:Label class="resp" ID="lblPE1Phone" runat="server" />  &nbsp; <asp:Label ID="lblPE1Salary" runat="server" /><br />
            <asp:Label ID="lblPE1position" runat="server" />
        </td>
        <td>
            <asp:Label ID="lblPE1ReasonForLeaving" runat="server" />
        </td>
    </tr>
    <tr id="trPE12" runat="server" style="line-height:1px;"><td style="border:1px solid #cfcfcf;" colspan="4"><asp:Label ID="lblPE12interim" runat="server" /></td></tr>
    <tr class="shortRow">
        <td>
            <asp:Label ID="lblPE2Dates" runat="server" />
        </td>
        <td>
            <asp:Label class="resp" ID="lblPE2" runat="server" /><br /><asp:Label ID="lblPE2Location" runat="server" />
        </td>
        <td>
            <asp:Label class="resp" ID="lblPE2Phone" runat="server" />  &nbsp; <asp:Label ID="lblPE2Salary" runat="server" /><br />
            <asp:Label ID="lblPE2position" runat="server" />
        </td>
        <td>
            <asp:Label ID="lblPE2ReasonForLeaving" runat="server" />
        </td>
    </tr>
    <tr id="trPE23" runat="server" style="line-height:1px;"><td style="border:1px solid #cfcfcf;" colspan="4"><asp:Label ID="lblPE23interim" runat="server" /></td></tr>
    <tr class="shortRow">
        <td>
            <asp:Label ID="lblPE3Dates" runat="server" />
        </td>
        <td>
            <asp:Label class="resp" ID="lblPE3" runat="server" /><br /><asp:Label ID="lblPE3Location" runat="server" />
        </td>
        <td>
            <asp:Label class="resp" ID="lblPE3Phone" runat="server" />  &nbsp; <asp:Label ID="lblPE3Salary" runat="server" /><br />
            <asp:Label ID="lblPE3position" runat="server" />
        </td>
        <td>
            <asp:Label ID="lblPE3ReasonForLeaving" runat="server" />
        </td>
    </tr>
    <tr id="trPE34" runat="server" style="line-height:12px;"><td style="border:1px solid #cfcfcf;" colspan="4"><asp:Label ID="lblPE34interim" runat="server" /></td></tr>
    <tr class="shortRow">
        <td>
            <asp:Label ID="lblPE4Dates" runat="server" />
        </td>
        <td>
            <asp:Label class="resp" ID="lblPE4" runat="server" /><br /><asp:Label ID="lblPE4Location" runat="server" />
        </td>
        <td>
            <asp:Label class="resp" ID="lblPE4Phone" runat="server" />  &nbsp; <asp:Label ID="lblPE4Salary" runat="server" /><br />
            <asp:Label ID="lblPE4position" runat="server" />
        </td>
        <td>
            <asp:Label ID="lblPE4ReasonForLeaving" runat="server" />
        </td>
    </tr>

</table>
<%--********************************************* References *********************************************--%>
<table width="100%">
    <tr>
        <td colspan="3" class="SectionHeader">
            References
        </td>
    </tr>
    <tr class="shortRow">
        <td>Name</td>
        <td>Yrs Known</td>
        <td>Contact Info</td>
    </tr>
    <tr class="shortRow">
        <td>
            <asp:Label class="resp" ID="lblReference1" runat="server" />
        </td>
        <td>
            <asp:Label class="resp" ID="lblReference1YrsKnown" runat="server" />
        </td>
        <td>
            <asp:Label class="resp" ID="lblReference1Contact" runat="server" />
        </td>
    </tr>
    <tr style="line-height:1px; background-color:#cfcfcf;"><td colspan="3"></td></tr>
    <tr class="shortRow">
        <td>
            <asp:Label class="resp" ID="lblReference2" runat="server" />
        </td>
        <td>
            <asp:Label class="resp" ID="lblReference2YrsKnown" runat="server" />
        </td>
        <td>
            <asp:Label class="resp" ID="lbLReference2Contact" runat="server" />
        </td>
    </tr>
    <tr style="line-height:1px; background-color:#cfcfcf;"><td colspan="3"></td></tr>
    <tr class="shortRow">
        <td>
            <asp:Label class="resp" ID="lblReference3" runat="server" />
        </td>
        <td>
            <asp:Label class="resp" ID="lblReference3YrsKnown" runat="server" />
        </td>
        <td>
            <asp:Label class="resp" ID="lblReference3Contact" runat="server" />
        </td>
    </tr>
</table>
<%--********************************************* Background *********************************************--%>
</td></tr></table>
        </div>
<%--********************************************* DELETE an application *********************************************--%>
        <asp:LinkButton ID="lbtnDelete" runat="server" ToolTip="Use ONLY in case of duplicate record!&#13;There will be NO confirmation.&#13;This can NOT be undone." Text="Delete this record (Duplicates ONLY)" />

        </td>   
        <td valign="top"><br />
<%--********************************************* Rating *********************************************--%>
<table><tr>
<%--        <td><asp:LinkButton ID="lnkbtnClearRating" Text="zero" CssClass="lilBlueButton" runat="server" Visible="false" Enabled="false" /></td>--%>
        <td style="padding-left:12px;"><telerik:RadRating ID="RadRating1" ItemCount="5" Orientation="Horizontal" runat="server" Height="24px"></telerik:RadRating></td>
        <td align="right" style="padding-left:45px;"><asp:LinkButton ID="lnkbtnSave" Text="Save" CssClass="lilBlueButton" runat="server" /></td>
</tr></table>

<%--********************************************* Notes *********************************************--%>
<telerik:RadTextBox runat="server" TextMode="MultiLine" Rows="5" Width="225px" Columns="80" ID="txtNotes">
</telerik:RadTextBox>
<%--********************************************* Hire Me *********************************************--%>
<br />
<div style="max-width:220; text-align:right;"><asp:LinkButton ID="lbtnHireMe" runat="server" Text="Hire this Applicant" /></div>
<hr />
<asp:Label ID="lblPrevNotes" runat="server" /><br />
<table width="100%"><tr><td align="right" style="padding-right:12px;">
<%--********************************************* REJECT an Application *********************************************--%>
<asp:LinkButton ID="lbtnReject" runat="server" Text="REJECT Applicant/Application" ToolTip="You will need to provide Reason in notes section" /><br />
<asp:Label ID="lblRejectError" runat="server" ForeColor="Red" />
</td></tr></table>

</td>
    </tr>
</table>

            </asp:Panel>
            <asp:Label ID="lblCopy" runat="server" />
            
        </td>
    </tr>
</table>
<telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="lblHelp" RelativeTo="Element"  
    ShowEvent="OnClick" Position="BottomLeft" HideEvent="ManualClose"  
     Animation="Resize" EnableShadow="true">
<table cellpadding="0" cellspacing="0" width="100%"><tr>
<td><span class="ttHeader">Employment Application Manager</span>
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
        <td class="ttTitle">Application List (left side)</td>
    </tr>
    <tr>
        <td  class="ttBody">
By default the application list will show "Application Pool" (applications rated 1 thru 5) [see below].<br />
            Use drop down box to change view to &quot;New Applications&quot; or &quot;Rejected 
            Applications&quot;. &nbsp;List will auto-refresh.<br />
            Enter a partial name (min 3 characters) in the middle text box and click the 
            &#39;Display Records&#39; button to find a specific applicant.<br />
            Click the title (header) of any column to <strong>sort in ascending order</strong>. 
            &nbsp;Click again to <strong>sort in descending</strong> order. &nbsp;Click a third time to 
            remove the sort.<br /> By default, the &#39;Application Pool&#39; is sorted by (State, 
            City, Rating).&nbsp; &#39;New Applications and Rejected Applications are sorted with most 
            recent at the top.<br /> You can also <strong>use grouping to find an applicant 
            pool in your area</strong>. &nbsp;Click the &#39;State&#39; column header and drag and drop 
            it onto the grouping row on top of the list.<br />
Next, do the same with the 'City' column header. &nbsp;Now you have a list grouped by State then City.  &nbsp;Next click the '1-5' column header twice to sort in descending order.<br />
Now your lists show the top qualified applicants at the top of each 'City' grouping.<br />
Click an applicant row to view / review their application.<br />
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Application (center)</td>
    </tr>
    <tr>
        <td  class="ttBody">
        The application information is displayed in sections just like the actual application filled out by the applicant.<br />
        An 'n/a' indicates 'no answer' provided by the applicant.<br />
NOTE: &nbsp;            You may notice that some dates are followed by &#39;date math&#39; in parentheses to 
            show elapsed or cumulative time.<br />
            A negative number between Previous Employments might mean applicant held two jobs at the same time.<br />
             &nbsp; &nbsp; example:  <span style="background-color:#FFEFEF;"> ( -6 months )  <-- jobs overlap? </span> might mean that for six months applicant was employed at both places.
            <br />
            <u>Remember</u>: &nbsp;These calculations are NOT verified numbers, but simply date calculations based on applicant supplied dates.

        </td>
    </tr>
    <tr>
        <td class="ttTitle">HR Review (right side)</td>
    </tr>
    <tr>
        <td  class="ttBody">
HR Review has three parts.<br />
<b>Applicant Rating</b><br />
            Applicant ratings are on a 0 to 5 scale where:<br /> 0 (zero) is reserved for 
            NEW applications that have not had an initial HR review.<br />
            1 (one) and 2 (two) star(s) is applied to applicants still in the vetting 
            process.<br /> At any point, an application can be rejected by providing a 
            reason in the text box and clicking the &#39;REJECT&#39; link below the notes.<br /> 
            Once the applicant / application is vetted, the applicant is given a rating of 
            3, 4 or 5 stars based on quality of qualifications.<br />
            When you change an applicants rating, you MUST click the blue &#39;Save&#39; link in the 
            upper right corner.<br /> Once an applicant has three or more stars you may 
            click the &#39;<font color='blue'>Hire this Applicant</font>&#39; link for step next.  <-- coming soon!<br />  
<b>Add a Note</b><br />
As you review the application and call references etc you can add notes or questions here.<br />
Enter your comments, questions, verification results, etc and click the blue 'Save' link in the upper right corner.<br />
<b>Previous Notes</b><br />
            Previous note are listed (most recent at the top) with a timestamp and the note 
            owners name.<br /> If you create a note AND change the applicants rating, 2(two) 
            notes will be generated.
        </td>
    </tr>
</table><br />
<center>To Close - Click X in upper right corner</center>


</td></tr></table>

</telerik:RadToolTip>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" VisibleStatusbar="false" >
<Windows>
<telerik:RadWindow ID="wEditApp" AutoSize="true" Title="SEU - Employment Application Editor" Skin="Sunset" OnClientClose="closeit"
     ShowContentDuringLoad="true"  runat="server" Behaviors="Move, Resize, Maximize, Close" />
</Windows>
</telerik:RadWindowManager>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">
    function openEditApp(arg) {
        //        document.getElementById("< %= btnSaveChanges.ClientID % >").disabled = true;
        var oManager = GetRadWindowManager();
        var loca = "../EmploymentApplication.aspx?appid=" + arg;
        oManager.open(loca, "wEditApp");
    }

    function openViewApp(arg) {
        //        document.getElementById("< %= btnSaveChanges.ClientID % >").disabled = true;
        var oManager = GetRadWindowManager();
        var loca = "../Admin/EmploymentApplicationForm.aspx?appid=" + arg;
        var lwin = oManager.getWindowByName("wEditApp");
        lwin.set_minHeight(500);
        lwin.set_minWidth(400);

        oManager.open(loca, "wEditApp");
    }

    function blankAppMe() {
        //        document.getElementById("< %= btnSaveChanges.ClientID % >").disabled = true;
        var oManager = GetRadWindowManager();
        var loca = "../Admin/EmploymentApplicationForm.aspx?dld=yes";
        var lwin = oManager.getWindowByName("wEditApp");
        lwin.set_minHeight(500);
        lwin.set_minWidth(400);
        oManager.open(loca, "wEditApp");
    }
    

    function closeit(oWnd, args) {
        oWnd.setUrl("../seuLoader.aspx");
        if (args.get_argument() != null) {
            var arg = args.get_argument();
            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
            ajaxManager.ajaxRequest(arg);
        }
    }


</script>
</telerik:RadCodeBlock>


    </div>
    </form>
</body>
</html>
