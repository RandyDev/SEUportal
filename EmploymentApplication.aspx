<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EmploymentApplication.aspx.vb" Inherits="DiversifiedLogistics.EmploymentApplication" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="styles/styles.css"  rel="stylesheet" type="text/css" />
    <link href="../styles/StyleSheet.css" rel="stylesheet" type="text/css" />
<style type="text/css"> 
#appHeader{ margin:0 auto; text-align:center; font-size:17px; font-weight:bold;}
.sectionTitle{padding-left:7px;background-color:#C4C5D9; font-weight:bold; color:#003300; }
.fieldTitleB{padding-left:7px;background-color:#efefef; font-size:11px; font-weight:bold; }
.fieldTitle{padding-left:7px;background-color:#efefef; font-size:11px; }
    
a:link {color: #000090; text-decoration: none}
a:visited {color: #000090; text-decoration: none}
a:hover {color: #000090; text-decoration: overline underline}
a:active {color: #000090; text-decoration: overline underline}
.title {font-family: arial; font-size:22px; font-weight:bold; color:black;}
.contact{font-family: arial; font-size:10px; font-weight:normal; color:black;}
.copy {font-family: verdana; font-size:10px; color:black;}
input{font-size:12px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
<script type="text/javascript">

    function decOnly(i) {
        var t = i.value;
        if (t.length > 0) {
            t = t.replace(/[^\d]+/g, '');
            //                t = t.replace(/[^\da-zA-Z]+/g, '');
            i.value = t;
        }
        if (t.length == 5) {
            var ajaxManager = $find("<%=RadAjaxManager1.ClientID%>");
            ajaxManager.ajaxRequest("ZipCodeLookup:" + t);
        }
    }

    function zipOnBlur() {
        var zipcontrol = $find("<%=txtZip.ClientID%>");
        var statecontrol = $find("<%=txtState.ClientID%>");
        var zip = zipcontrol.get_value();
        var state = statecontrol.get_value();
        alert(zip.length);
        if (state == "") {
            if (zip.length < 5) {
                alert("please enter valid zip code");
                zipcontrol.focus();
                alert("nuttin");
            } else {
                alert("going to look up " + zip);
 
                var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                ajaxManager.ajaxRequest("ZipCodeLookup:" + zip.get_texBoxValue());
    
        }
        }
    }


</script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCity" />
                    <telerik:AjaxUpdatedControl ControlID="txtState" />
                    <telerik:AjaxUpdatedControl ControlID="txtDLstate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cbcompany">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cbPreferredLocationtxtCity" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel Transparency="50" ID="RadAjaxLoadingPanel1"  runat="server">
        <asp:Image id="Image1" runat="server" Width="110" Height="21" ImageUrl="~/images/forkliftani.gif"></asp:Image>
    </telerik:RadAjaxLoadingPanel>
    <div style="margin:0 auto;max-width:600px;">
<asp:Panel ID="pnlApplication" runat="server" Visible="true" >
<asp:Label ID="lblerrStr" runat="server" Visible="false" />
<table id="appHeader" style="width:600px;">
    <tr>
        <td width="130">
            <asp:Image ID="imgapplicationLogo" Height="35px" runat="server" />
        </td>
        <td width="305" style="text-align:center;vertical-align:bottom;">
          Employment Application  
        </td>
        <td width="165" style="text-align:right;"><asp:Label ID="lblHelp" class="lilBlueButton" runat="server" Text="Help with this page" />
        </td>
    </tr>
<tr><td colspan="3">

<table cellspacing="0" style="width:600px;" border="1" align="center">
  <tr> 
    <td class="sectionTitle">Personal Information</td>
  </tr>
  <tr> 
    <td>  
        <table cellspacing="0" width="100%">
          <tr style="line-height:13px;"> 
            <td width="170" class="fieldTitle">First Name*</td>
            <td class="fieldTitle" width="40">M.I.</td>
            <td class="fieldTitle" width="170">Last Name*</td>
            <td width="200" class="fieldTitle">Referred By</td>
          </tr>
          <tr> 
            <td style="padding-left:4px;"><telerik:RadTextBox ID="txtFirstName" MaxLength="35" runat="server" /></td>
            <td><telerik:RadTextBox ID="txtMiddleNameInitial" Width="35px" MaxLength="1" runat="server" /></td>
            <td><telerik:RadTextBox ID="txtLastName" MaxLength="35" runat="server" /></td>
            <td><telerik:RadTextBox ID="txtReferredby" MaxLength="75" Width="175" runat="server" /></td>
          </tr>
        </table>

        <table cellspacing="0" width="100%">
          <tr style="line-height:13px;"> 
            <td class="fieldTitle" width="230">Street Address*</td>
            <td class="fieldTitle" width="55">Zip*</td>
            <td class="fieldTitle" width="80"></td>
            <td class="fieldTitle" width="235"></td>
          </tr>
          <tr> 
            <td style="padding-left:4px;"><telerik:RadTextBox ID="txtStreetAddress" Width="225px" MaxLength="125" runat="server" /></td>
            <td><telerik:RadTextBox ID="txtZip" runat="server" MaxLength="5" Width="50px" /></td>
            <td><telerik:RadTextBox ID="txtCity" Enabled="false"  MaxLength="75" runat="server" /></td>
            <td><telerik:RadTextBox ID="txtState" Enabled="false" Width="35px" MaxLength="2" runat="server" />
            </td>
          </tr>
        </table>
        <table cellspacing="0" width="100%">
          <tr style="line-height:13px;"> 
            <td class="fieldTitle">Primary&nbsp;Phone*</td>
            <td class="fieldTitle">Alternate Phone</td>
            <td class="fieldTitle">Email Address*    
                <asp:RegularExpressionValidator id="emailValidator" runat="server" Display="Dynamic" ErrorMessage="Please, enter valid e-mail address."
                ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
                ControlToValidate="txtEmail">
</asp:RegularExpressionValidator>
</td>
          </tr>
          <tr> 
            <td style="padding-left:4px;"><telerik:RadMaskedTextBox ID="txtPrimaryPhone" EmptyMessage=" " Mask="(###) ###-####" runat="server" />
            </td>
            <td>
                <telerik:RadMaskedTextBox ID="txtAltPhone" EmptyMessage=" " Mask="(###) ###-####" runat="server" />
            </td>
            <td><telerik:RadTextBox ID="txtEmail" MaxLength="150" Width="225px" runat="server" /></td>

          </tr>
        </table>
    </td>
  </tr>
  <tr> 
    <td class="sectionTitle">Employment Desired</td>
  </tr>
  <tr>
    <td>
        <table cellspacing="0" width="100%">
            <tr style="line-height:13px;">
                <td class="fieldTitle">Preferred Location*</td>
                <td class="fieldTitle" style="width:180px">Position</td>
                <td class="fieldTitle" style="width:170px">Salary Desired</td>
            </tr>
            <tr>
                <td style="padding-left:4px;"><telerik:RadComboBox ID="cbPreferredLocation" Width="200px" MaxLength="200" runat="server" /></td>
                <td style="padding-left:4px;"><telerik:RadTextBox ID="txtDesiredPosition" MaxLength="125" runat="server" /></td>
                <td><telerik:RadNumericTextBox ID="txtDesiredSalary" NumberFormat-DecimalDigits="2" Width="75px" runat="server" />
                <telerik:RadComboBox ID="cbDesiredSalaryPeriod" Width="65px" MaxLength="125" runat="server">
                <Items>
                <telerik:RadComboBoxItem Text="Hour" />
                <telerik:RadComboBoxItem Text="Week" />
                <telerik:RadComboBoxItem Text="Month" />
                <telerik:RadComboBoxItem Text="Year" />

                </Items>
                </telerik:RadComboBox>
                 </td>
            </tr>
        </table>
        <table cellspacing="0" width="100%">
            <tr style="line-height:13px;">
                <td class="fieldTitle" width="30%">Date you can start</td>
                <td class="fieldTitle"width="30%">Are you currently employed?</td>
                <td class="fieldTitle"width="30%">If so, may we contact your current employer?</td>
            </tr>
            <tr>
                <td >
                    <telerik:RadDatePicker ID="dpDesiredStartDate" runat="server" />
                </td>
                <td>
                    <asp:RadioButton ID="rbCurrentlyEmployedYes" GroupName="CurrentlyEmployed" Text="Yes" runat="server" />
                    <asp:RadioButton ID="rbCurrentlyEmployedNo" GroupName="CurrentlyEmployed" Text="No" Checked="true" runat="server" />
                </td>
                <td>
                    <asp:RadioButton ID="rbAskCurrentEmployerYes" GroupName="AskCurrentEmployer" Text="Yes" runat="server" />
                    <asp:RadioButton ID="rbAskCurrentEmployerNo" GroupName="AskCurrentEmployer" Text="No" Checked="true" runat="server" />
                </td>
            </tr>
        </table>
                <table cellspacing="0" width="100%">
            <tr style="line-height:13px;">
                <td class="fieldTitle" width="250px">Ever applied to this company in the past?</td>
                <td class="fieldTitle" width="150px">Where</td>
                <td class="fieldTitle">When</td>
            </tr>
            <tr>
                <td style="padding-left:40px;">
                    <asp:RadioButton ID="rbAppliedBeforeYes" GroupName="AppliedBefore" Text="Yes" runat="server" />
                    <asp:RadioButton ID="rbAppliedBeforeNo" GroupName="AppliedBefore" Text="No" Checked="true" runat="server" />
                </td>
                <td><telerik:RadTextBox ID="txtAppliedBeforeLocation" MaxLength="125" runat="server"  /></td>
                         <td><telerik:RadComboBox ID="cbAppliedBeforeMonth" runat="server" Width="45px" />
                        <telerik:RadComboBox ID="cbAppliedBeforeYear" Width="48px" runat="server" />
                        </td>
            </tr>
        </table>
    </td>
  </tr>
  <tr> 
    <td class="sectionTitle">Education/Training</td>
  </tr>
  <tr> 
    <td>

<%-- *************************    Education Level    *************************    --%>
        <table cellpadding="0" cellspacing="0" width="100%"><tr>
    <td>
        <table cellspacing="0" cellpadding="0" width="100%">
          <tr> 
            <td class="fieldTitle" width="145">Highest level of education</td>
            <td>
                <telerik:RadComboBox ID="cbEducationLevel" runat="server">
                    <Items>
                        <telerik:RadComboBoxItem Text="None selected" />
                        <telerik:RadComboBoxItem Text="Not high school graduate" />
                        <telerik:RadComboBoxItem Text="GED" />
                        <telerik:RadComboBoxItem Text="High school graduate" />
                        <telerik:RadComboBoxItem Text="Some college" />
                        <telerik:RadComboBoxItem Text="Associate's degree" />
                        <telerik:RadComboBoxItem Text="Bachelor's degree" />
                        <telerik:RadComboBoxItem Text="Master's degree" />
                    </Items>
                </telerik:RadComboBox>
            </td>
          </tr>
        </table>
    </td>
</tr></table>
<%-- *************************    Schools    *************************    --%>
        <table cellspacing="0" cellpadding="0" width="100%">
            <tr class="fieldTitle" style="font-size:11px; line-height:13px;">
                <td style="width:40px;"></td>
                <td style="padding-left:8px;">Name and Location of School</td>
                <td style="text-align: center">Years<br /> Attended</td>
                <td style="text-align: center">Graduated?<br /> Yes&nbsp;/&nbsp;No</td>
                <td style="padding-left:8px;">Subjects Studied</td>
            </tr>
            <tr>
                    <td class="fieldTitle" style="font-size:9px; line-height:11px; text-align: center;">HIGHSCHOOL<br />EQUIVALENT</td>
                <td><telerik:RadTextBox ID="txtSchool1" Width="200px" MaxLength="125" ToolTip="School Name" EmptyMessage="School Name" runat="server" /><br /><telerik:RadTextBox ID="txtSchool1Location" Width="200px" MaxLength="125" ToolTip="City, State" EmptyMessage="Location (City, State)" runat="server" /></td>
                <td style="text-align: center"><telerik:RadNumericTextBox ID="txtSchool1YearsAttended" MinValue="0" NumberFormat-DecimalDigits="0" MaxValue="10" Width="31px" runat="server" style="text-align: center" /></td>
                <td style="text-align: center"><telerik:RadComboBox ID="cbSchool1Graduated" Width="40px" runat="server"><Items><telerik:RadComboBoxItem Text="" /><telerik:RadComboBoxItem Text="Yes" /><telerik:RadComboBoxItem Text="No" /></Items></telerik:RadComboBox></td>
                <td><telerik:RadTextBox ID="txtSchool1SubjectsStudied" Width="175px" MaxLength="255" TextMode="MultiLine" Rows="2" runat="server" /></td>
            </tr>
            <tr id="collegeRow">
                <td class="fieldTitle" style="font-size:9px; line-height:11px; text-align: center;">COLLEGE</td>
                <td><telerik:RadTextBox ID="txtSchool2" Width="200px" MaxLength="125" ToolTip="School Name" EmptyMessage="School Name" runat="server" /><br /><telerik:RadTextBox ID="txtSchool2Location" Width="200px" MaxLength="125" ToolTip="City, State" EmptyMessage="Location (City, State)" runat="server" /></td>
                <td style="text-align: center"><telerik:RadNumericTextBox ID="txtSchool2YearsAttended" MinValue="0" NumberFormat-DecimalDigits="0" MaxValue="10" Width="31px" runat="server" style="text-align: center" /></td>
                <td style="text-align: center"><telerik:RadComboBox ID="cbSchool2Graduated" Width="40px" runat="server"><Items><telerik:RadComboBoxItem Text="" /><telerik:RadComboBoxItem Text="Yes" /><telerik:RadComboBoxItem Text="No" /></Items></telerik:RadComboBox></td>
                <td><telerik:RadTextBox ID="txtSchool2SubjectsStudied" Width="175px" MaxLength="255" TextMode="MultiLine" Rows="2" runat="server" /></td>
            </tr>
            <tr>
                <td class="fieldTitle" style="font-size:9px; line-height:11px; text-align: center;">TRADE/<br />BUSINESS</td>
                <td><telerik:RadTextBox ID="txtSchool3" Width="200px" MaxLength="125" ToolTip="School Name" EmptyMessage="School Name" runat="server" /><br /><telerik:RadTextBox ID="txtSchool3Location" Width="200px" MaxLength="125" ToolTip="City, State" EmptyMessage="Location (City, State)" runat="server" /></td>
                <td style="text-align: center"><telerik:RadNumericTextBox ID="txtSchool3YearsAttended" MinValue="0" NumberFormat-DecimalDigits="0" MaxValue="10" Width="31px" runat="server" style="text-align: center" /></td>
                <td style="text-align: center"><telerik:RadComboBox ID="cbSchool3Graduated" Width="40px" runat="server"><Items><telerik:RadComboBoxItem Text="" /><telerik:RadComboBoxItem Text="Yes" /><telerik:RadComboBoxItem Text="No" /></Items></telerik:RadComboBox></td>
                <td><telerik:RadTextBox ID="txtSchool3SubjectsStudied" Width="175px" MaxLength="255" TextMode="MultiLine" Rows="2" runat="server" /></td>
            </tr>
        </table>
<%-- *************************    Special Study / Training / Skills    *************************    --%>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr style="line-height:13px;">
                <td class="fieldTitle">Subjects of special study, research work, special training or skills</td>
            </tr>
            <tr>
                <td style="padding-left:4px;">
                    <telerik:RadTextBox ID="txtSpecialSkills" Width="585px" runat="server" />
                </td>
            </tr>
        </table>
<%-- *************************    Military Service    *************************    --%>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr style="line-height:13px;">
                <td class="fieldTitle" style="width:125px;">US Military Branch</td>
                <td class="fieldTitle" style="padding-left:8px; width:245px;">Dates of Service</td>
                <td class="fieldTitle">Rank</td>
            </tr>
            <tr>
                <td style="padding-left:4px;">
                    <telerik:RadComboBox ID="cbMilitaryBranch" Width="100px" runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Text="" />
                            <telerik:RadComboBoxItem Text="Air Force" />
                            <telerik:RadComboBoxItem Text="Army" />
                            <telerik:RadComboBoxItem Text="Coast Guard" />
                            <telerik:RadComboBoxItem Text="Marines" />
                            <telerik:RadComboBoxItem Text="Navy" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td>
                    <table cellpadding="0" cellspacing="0">
                <tr>
                    <td class="fieldTitle">From:</td>
                    <td><telerik:RadComboBox ID="cbMilitaryServiceFromDateMonth" EnableViewState="true" runat="server" Width="45px" />
                        </td>
                    <td><telerik:RadComboBox ID="cbMilitaryServiceFromDateYear" EnableViewState="true" Width="50px" runat="server" />
                    </td>
                    <td class="fieldTitle" style="padding-left:7px;">To:</td>
                    <td><telerik:RadComboBox ID="cbMilitaryServiceToDateMonth" runat="server" EnableViewState="true" Width="45px" />
                    </td>
                    <td><telerik:RadComboBox ID="cbMilitaryServiceToDateYear" Width="50px" EnableViewState="true" runat="server" />
                    </td>
                </tr>
            </table>
                </td>
                <td>
                    <telerik:RadComboBox ID="cbMilitaryRank" Width="60px" runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Text="" />
                            <telerik:RadComboBoxItem Text="E-1" />
                            <telerik:RadComboBoxItem Text="E-2" />
                            <telerik:RadComboBoxItem Text="E-3" />
                            <telerik:RadComboBoxItem Text="E-4" />
                            <telerik:RadComboBoxItem Text="E-5" />
                            <telerik:RadComboBoxItem Text="E-6" />
                            <telerik:RadComboBoxItem Text="E-7" />
                            <telerik:RadComboBoxItem Text="Other" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

    </td>
  </tr>
<%-- *************************    Former Employment    *************************    --%>
  <tr> 
    <td class="sectionTitle">Former Employment 
        <span style="font-size:11px; color:Black;font-weight:normal;"> 
            &nbsp;&nbsp;&nbsp; (starting with most current ... last 4 employers or last 10 years of employment)
        </span>
    </td>
  </tr>
  <tr> 
    <td class="text"> <div align="left"> 
        <table cellspacing="0" width="100%">
          <tr style="line-height:13px;" class="fieldTitle"> 
            <td style="text-align: center">Month, Year</td>
            <td>Employer Name / Location</td>
            <td style="padding-left:6px;">Phone &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Salary</td>
            <td style="padding-left:6px;">Reason&nbsp;for&nbsp;Leaving</td>
          </tr>
          <tr>
            <td>
                <table cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="fieldTitle" style="font-size:9px; line-height:11px; text-align: center;">TO</td>
                        <td><telerik:RadComboBox ID="cbPE1ToMonth" EnableViewState="true" runat="server" Width="45px" />
                        </td>
                        <td><telerik:RadComboBox ID="cbPE1ToYear" EnableViewState="true" Width="48px" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldTitle" style="font-size:9px; line-height:11px; text-align: center;">FROM</td>
                        <td><telerik:RadComboBox ID="cbPE1FromMonth" EnableViewState="true" runat="server" Width="45px" />
                        </td>
                        <td><telerik:RadComboBox ID="cbPE1FromYear" EnableViewState="true" Width="48px" runat="server" />
                        </td>
                    </tr>
                </table>
            </td> 

            <td><telerik:RadTextBox ID="txtPE1" Width="150px" MaxLength="125" ToolTip="Company Name" EmptyMessage="Employer Name" runat="server" /><br /><telerik:RadTextBox ID="txtPE1Location" Width="150px" MaxLength="125" ToolTip="City, State" EmptyMessage="Location (City, State)" runat="server" /></td>
            <td>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top"><telerik:RadMaskedTextBox ID="txtPE1phone" Width="85px" runat="server" EmptyMessage=" " Mask="(###) ###-####" /></td> 
            <td style="padding-left:7px;" valign="top"><telerik:RadTextBox ID="txtPE1salary" MaxLength="25" Width="65px" runat="server" /></td> 
        </tr>
        <tr>
            <td colspan="2" valign="top"><telerik:RadTextBox ID="txtPE1position" MaxLength="125" EmptyMessage="Position" Width="157px" runat="server" /></td>
        </tr>
    </table>
            </td>
            
            <td><telerik:RadTextBox ID="txtPE1reasonForLeaving" MaxLength="150" TextMode="MultiLine" Height="40px" Rows="2" runat="server" /></td> 

          </tr>
          <tr>
            <td>
                <table cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="fieldTitle" style="font-size:9px; line-height:11px; text-align: center;">TO</td>
                        <td><telerik:RadComboBox ID="cbPE2ToMonth" runat="server" EnableViewState="true" Width="45px" />
                        </td>
                        <td><telerik:RadComboBox ID="cbPE2ToYear" Width="48px" EnableViewState="true" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldTitle" style="font-size:9px; line-height:11px; text-align: center;">FROM</td>
                        <td><telerik:RadComboBox ID="cbPE2FromMonth" runat="server" EnableViewState="true" Width="45px" />
                        </td>
                        <td><telerik:RadComboBox ID="cbPE2FromYear" Width="48px" EnableViewState="true" runat="server" />
                        </td>
                    </tr>
                </table>
            </td> 

            <td><telerik:RadTextBox ID="txtPE2" Width="150px" MaxLength="125" ToolTip="Company Name" EmptyMessage="Employer Name" runat="server" /><br /><telerik:RadTextBox ID="txtPE2Location" Width="150px" MaxLength="125" ToolTip="City, State" EmptyMessage="Location (City, State)" runat="server" /></td>
            <td>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top"><telerik:RadMaskedTextBox ID="txtPE2phone" Width="85px" runat="server" EmptyMessage=" " Mask="(###) ###-####" /></td> 
            <td style="padding-left:7px;" valign="top"><telerik:RadTextBox ID="txtPE2salary" MaxLength="25" Width="65px" runat="server" /></td> 
        </tr>
        <tr>
            <td colspan="2" valign="top"><telerik:RadTextBox ID="txtPE2position" MaxLength="125" EmptyMessage="Position" Width="157px" runat="server" /></td>
        </tr>
    </table>
            </td>
            
            <td><telerik:RadTextBox ID="txtPE2reasonForLeaving" MaxLength="255" TextMode="MultiLine" Height="40px" Rows="2" runat="server" /></td> 

          </tr>
          <tr>
            <td>
                <table cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="fieldTitle" style="font-size:9px; line-height:11px; text-align: center;">TO</td>
                        <td><telerik:RadComboBox ID="cbPE3ToMonth" EnableViewState="true" runat="server" Width="45px" />
                        </td>
                        <td><telerik:RadComboBox ID="cbPE3ToYear" EnableViewState="true" Width="48px" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldTitle" style="font-size:9px; line-height:11px; text-align: center;">FROM</td>
                        <td><telerik:RadComboBox ID="cbPE3FromMonth" runat="server" EnableViewState="true" Width="45px" />
                        </td>
                        <td><telerik:RadComboBox ID="cbPE3FromYear" Width="48px" EnableViewState="true" runat="server" />
                        </td>
                    </tr>
                </table>
            </td> 

            <td><telerik:RadTextBox ID="txtPE3" Width="150px" MaxLength="125" ToolTip="Company Name" EmptyMessage="Employer Name" runat="server" /><br /><telerik:RadTextBox ID="txtPE3Location" Width="150px" MaxLength="125" ToolTip="City, State" EmptyMessage="Location (City, State)" runat="server" /></td>
            <td>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top"><telerik:RadMaskedTextBox ID="txtPE3phone" Width="85px" runat="server" EmptyMessage=" " Mask="(###) ###-####" /></td> 
            <td style="padding-left:7px;" valign="top"><telerik:RadTextBox ID="txtPE3salary" MaxLength="25" Width="65px" runat="server" /></td> 
        </tr>
        <tr>
            <td colspan="2" valign="top"><telerik:RadTextBox ID="txtPE3position" MaxLength="125" EmptyMessage="Position" Width="157px" runat="server" /></td>
        </tr>
    </table>
            </td>
            
            <td><telerik:RadTextBox ID="txtPE3reasonForLeaving" MaxLength="255" TextMode="MultiLine" Height="40px" Rows="2" runat="server" /></td> 

          </tr>
          <tr>
            <td>
                <table cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="fieldTitle" style="font-size:9px; line-height:11px; text-align: center;">TO</td>
                        <td><telerik:RadComboBox ID="cbPE4ToMonth" runat="server" Width="45px" />
                        </td>
                        <td><telerik:RadComboBox ID="cbPE4ToYear" Width="48px" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldTitle" style="font-size:9px; line-height:11px; text-align: center;">FROM</td>
                        <td><telerik:RadComboBox ID="cbPE4FromMonth" runat="server" Width="45px" />
                        </td>
                        <td><telerik:RadComboBox ID="cbPE4FromYear" Width="48px" runat="server" />
                        </td>
                    </tr>
                </table>
            </td> 

            <td><telerik:RadTextBox ID="txtPE4" Width="150px" MaxLength="125" ToolTip="Company Name" EmptyMessage="Employer Name" runat="server" /><br /><telerik:RadTextBox ID="txtPE4Location" Width="150px" MaxLength="125" ToolTip="City, State" EmptyMessage="Location (City, State)" runat="server" /></td>
            <td>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top"><telerik:RadMaskedTextBox ID="txtPE4phone" Width="85px" runat="server" EmptyMessage=" " Mask="(###) ###-####" /></td> 
            <td style="padding-left:7px;" valign="top"><telerik:RadTextBox ID="txtPE4salary" MaxLength="25" Width="65px" runat="server" /></td> 
        </tr>
        <tr>
            <td colspan="2" valign="top"><telerik:RadTextBox ID="txtPE4position" MaxLength="125" EmptyMessage="Position" Width="157px" runat="server" /></td>
        </tr>
    </table>
            </td>
            
            <td><telerik:RadTextBox ID="txtPE4reasonForLeaving" MaxLength="255" TextMode="MultiLine" Height="40px" Rows="2" runat="server" /></td> 

          </tr>
        </table>
      </div></td>
  </tr>
  <tr> 
    <td class="sectionTitle">References 
        <span style="font-size:11px; color:Black;font-weight:normal;">
            &nbsp;&nbsp;&nbsp; (You must have known these persons for a minimum of one year)
        </span>
    </td>
  </tr>
  <tr> 
    <td class="text"> <div align="left"> 
        <table cellspacing="0" width="100%">
          <tr style="line-height:13px;" class="fieldTitle"> 
            <td>&nbsp;</td>
            <td style="padding-left:7px;">Name</td>
            <td>Years Known</td>
            <td style="padding-left:6px;">Phone number or eMail address</td>
          </tr>
          <tr> 
            <td align="center" width="25" class="fieldTitle">1</td>
            <td><telerik:RadTextBox ID="txtReference1" Width="200px" MaxLength="125" runat="server" /></td>
            <td><telerik:RadNumericTextBox ID="txxtReference1YrsKnown" MinValue="0" NumberFormat-DecimalDigits="0" MaxValue="50" Width="75px" runat="server" /></td>
            <td><telerik:RadTextBox ID="txtReference1Contact" Width="250px" MaxLength="150" runat="server" /></td>
          </tr>
          <tr> 
            <td align="center" class="fieldTitle">2</td>
            <td><telerik:RadTextBox ID="txtReference2" Width="200px" MaxLength="125" runat="server" /></td>
            <td><telerik:RadNumericTextBox ID="txtReference2YrsKnown" MinValue="0" NumberFormat-DecimalDigits="0" MaxValue="50" Width="75px" runat="server" /></td>
            <td><telerik:RadTextBox ID="txtReference2Contact" Width="250px" MaxLength="150" runat="server" /></td>
          </tr>
          <tr> 
            <td align="center" class="fieldTitle">3</td>
            <td><telerik:RadTextBox ID="txtReference3" Width="200px" MaxLength="125" runat="server" /></td>
            <td><telerik:RadNumericTextBox ID="txtReference3YrsKnown" MinValue="0" NumberFormat-DecimalDigits="0" MaxValue="50" Width="75px" runat="server" /></td>
            <td><telerik:RadTextBox ID="txtReference3Contact" Width="250px" MaxLength="150" runat="server" /></td>
          </tr>
        </table>
      </div></td>
  </tr>
  <tr> 
    <td bgcolor="#C4C5D9" class="text"><font color="#003300"><strong><asp:Label ID="lblAckTitle" Text="Acknowledgement / Verification" runat="server" /></strong></font></td>
  </tr>
  <tr style="line-height:13px;" class="fieldTitle"> 
    <td style="text-align:justify;padding:7px;font-size:11px;"><asp:panel ID="pnlAck" runat="server">
            <%= companyName %> is an equal opportunity employer. <%= companyName %> does not discriminate in its employment practices on account of an applicant&#39;s race, color, religion, national origin, citizenship status, ancestry, age, sex, sexual orientation, marital status, physical or mental disability, military status or unfavorable discharge from military service. <strong>
            <br />
            <br />
            </strong> 
        
            I understand that neither the completion of this application nor any other part of my consideration for employment establishes any obligation for <%= companyName %> to hire me. If I am hired, I understand that both <%= companyName %> and/or I can terminate my employment at any time and for any reason, with or without cause and without prior notice. I understand that no representative of <%= companyName %> has the authority to make any assurance to the contrary. <strong>
            <br />
            <br />
            </strong>
            I attest with my signature below that I have given to <%= companyName %> true and complete information on this application. No requested information has been concealed. I authorize <%= companyName %> to contact previous employers and references provided for employment verification. I understand that I will be expected to provide proof of eligibility to work in the U.S. and may be subject to a pre-employment drug screen and national criminal background check. If any information I have provided is untrue, or if I have concealed material information, I understand that this will constitute cause for the denial of employment or immediate dismissal.
            <br />
            <br /> <center><strong>By submitting this application you attest to and agree with the statements above.</strong></center><br /> </asp:panel>
<center>
<asp:Label ID="lblApplicantIP" Text="" runat="server" />
<span class="subbtn">
       <asp:Label ID="lblHelpABrother" Visible="false" runat="server" Text="<br />Help a brother out! &nbsp;Please be sure to FIRST: record any changes YOU made in the notes section.<br />Notes section you say? &nbsp;Click the blue 'help with this page' link in the upper right corner<br />no need to close this window." />
      <asp:Label ID="lblErr" runat="server" Text="<br /><span style='text-decoration:underline;'>BEFORE</span> clicking the submit button, you may <a style='font-size:13px; text-decoration:underline;' href='javascript:if(window.print)window.print()'>print a copy</a> of this application for your records" /><br /> <br /> 
        <asp:Button runat="server" ID="btnSubmit" OnClientClick="this.value='One Moment Please'" text="Submit Application" />
</span> </center>
        </td>
  </tr>

</table>

</td>
</tr>
<tr>
<td colspan="3" style="text-align:left">
<table cellpadding="0" cellspacing="0" width="100%">
<tr style="line-height:13px;font-size:10px;">
<td style="padding-left:8px;color:#AfAfAf;">Diversified Logistics Employment Application v3.0</td>
</tr>
</table>


</td>
</tr>
</table>

</asp:Panel>


    
<telerik:RadToolTip ID="RadToolTip1" runat="server" Modal="true" TargetControlID="lblHelp" Width="550px" Height="522px" RelativeTo="BrowserWindow"
   Animation="Resize" ShowEvent="OnClick" Position="Center" HideEvent="ManualClose"  
      EnableShadow="true">
<span class="ttHeader">READING THIS could improve your chances ...</span>
<table><tr><td style="padding:0 8px;">
<table>
    <tr>
        <td class="ttTitle">BEFORE you start, gather all necessary information</td>
    </tr>
    <tr>
        <td  class="ttBody">
        Please read these instructions completely and look over the entire application to be sure you have all the information you will need before you begin.<br />
It is IMPORTANT that you fully complete the application to the best of your ability.


        </td>
    </tr>
    <tr>
        <td class="ttTitle">Application Instructions</td>
    </tr>
    <tr>
        <td  class="ttBody">
Thank you for your interest in the Diversified Logistics family of companies. &nbsp;Our online application should only take you but a few
minutes to complete, however there are a few points we would like to first cover in these instructions.<br />
<b>Personal Information</b><br />
It is important that we are able contact you. &nbsp;Please provide all pertinent contact information and if you were asked by one of
our employees or managers to apply, please provide their name in the 'Referred by' field.<br />
<b>Employment Desired</b><br />
Be sure to tell us what position you are applying for and date you can start. &nbsp;Answer the remaining questions as appropriate.
<br />
<b>Education/Training</b><br />
Select your highest level of education from the dropdown box.<br />
For any schools listed, you must provide the location (City and State).<br />
Next, list any special training or skills you have and, if you were in the military, provide dates served and your pay grade when you got out.<br />
<b>Former Employment</b><br />
List your CURRENT or most recent employer first. &nbsp;Each employer listed MUST include the dates you were employed (month and year), the employer's location (City and State) and a phone number. &nbsp;
If you don't know them, look them up BEFORE completing this application.<br />
If you are still working there, select <%=moyr%> (this month) for the 'TO' month and year dates and 'Current' under 'Reason for Leaving'.
<br />
<b>References</b><br />
Please provide AT LEAST ONE reference you have known for at least one year.<br />
        </td>
    </tr>
    <tr>
        <td class="ttTitle">Before you click the 'Submit Application' button</td>
    </tr>
    <tr>
        <td  class="ttBody">
            Review your responses to be sure they are complete. &nbsp;Remember, this application is often our first impression of you. &nbsp;Get it right! &nbsp;
            Next, read the Acknowledgement / Verification statement. &nbsp;Your IP address and timestamp will serve as your signature.<br />
            If you wish, use the '<font color="blue">print a copy</font>' link to print a copy of the application for your records.
        </td>
    </tr>
</table><br />
<center>To Close - Click X in upper right corner</center>&nbsp;
</td></tr></table>
</telerik:RadToolTip>
    </div>
<br /><br /><br /><br />
<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
<script type="text/javascript">
    function pageLoad() {
        var currentWindow = GetRadWindow();
    }

    function GetRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)
        return oWindow;
    }

//    function cancelAndClose() {
//        var oWindow = GetRadWindow();
//        oWindow.argument = null;
//        oWindow.close();
//    }
//    
    function returnArg(arg) {
        var oWnd = GetRadWindow();
        oWnd.close(arg);
    }
    </script>
</telerik:RadScriptBlock>

    </form>

</body>
</html>
