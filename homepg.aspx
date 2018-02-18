<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="homepg.aspx.vb" Inherits="DiversifiedLogistics.homepg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<link href="Styles/styles.css"  rel="stylesheet" type="text/css" />
<style type="text/css">
.lblName{
font-size:15px;
font-weight:bold;
}
.btnMoveImports{
font-size: 16px;
    }
.lilBlueButton{
font-size:11px;
color:Blue;
font-weight:normal;
}
</style>
<script type="text/javascript">
    function toggleTimeSheet() {
        var div1 = document.getElementById('divTimeSheet');
        if (div1.style.display == 'none') {
            div1.style.display = 'inline'
        } else {
            div1.style.display = 'none'
        }
    }
</script>

<style type="text/css">
       .rgCollapse
       {
           display:none !important;
       }
       .rgExpand
       {
          display:none !important;
       }
       #tblCertReport
       {
           border:1px solid black;
           }
       .CertHeaderText
       {
           font-size:14px;
           }
       .CertHeaderCell
       {
           text-align:center;
        }

.loca
{
       padding:0 12px 0 12px;
 
    }
       .expired
       {
           padding:0 12px 0 12px;
           color:Red;
           }
       .expiring
       {
           padding:0 12px 0 12px;
           color:Orange;
           }
       .lnkbtn
       {
           padding:0 12px 0 12px;
           }
 </style>
 </head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <%--<telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" 
        DecoratedControls="Buttons" Skin="Office2007" />--%>    
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnLoadEditorDataEntry">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnLoadEditorDataEntry" 
                        UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSubmitProfile">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblChangePassword" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="pnlUpdateProfile" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"></telerik:RadAjaxLoadingPanel>
    
<table width="100%" align="center"><tr><td style="padding-right:20px;" align="right">
<div id="stlivechat0"></div></td></tr></table>

    <center><br />
<table>
    <tr>
        <td>
            <telerik:Radbutton ID="btnLoadEditor" Width="200px" Font-Size="24px" ToolTip="Web-based vrsion&#013;of handheld Load Editor" Text="Load Editor" runat="server" Height="65px" visible="false"></telerik:Radbutton>
        </td>
    </tr>
</table>

<br />

 Home Page for <br />
<asp:Label CssClass="lblName" ID="lblFirstLast" runat="server" />
<br />

<asp:Label ID="lblChangePassword" runat="server" Visible="false" />
<asp:Panel ID="pnlUpdateProfile" runat="server" Visible="false">
<table align="center" style="border:1px solid red;">
<tr>
<td>Hi <asp:Label ID="lblName" runat="server" /><br />
New security measures now prevent passwords from being retrieved by '<u>anyone</u>'<br />
Lost passwords will need to be reset by a Manager, except,<br />
managers don't have time to keep up w/ your password, so ...<br /><br />

    Type or Select a security question and provide your response.<br />
<telerik:RadComboBox ID="RadComboBox1" Width="215" runat="server" MaxLength="40" AllowCustomText="true" EmptyMessage="Type or Select a security question">
<Items>
<telerik:RadComboBoxItem Text="My mother's maiden name is ..." runat="server" />
<telerik:RadComboBoxItem Text="The engine in my first car is/was ..." runat="server" />
<telerik:RadComboBoxItem Text="My favorite pet's name is ..." runat="server" />
<telerik:RadComboBoxItem Text="Name of city where I was born is ..." runat="server" />
<telerik:RadComboBoxItem Text="I would rather be ..." runat="server" />
</Items>
        </telerik:RadComboBox> &nbsp; &nbsp;
        <telerik:RadTextBox ID="txt_Response" EmptyMessage="Your Response" runat="server">
        </telerik:RadTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
            ErrorMessage="Required" ControlToValidate="txt_Response" />
<br />
<asp:Button ID="btnSubmitProfile" CommandName="UpdateUser" Text="Submit" runat="server" />
<br />&nbsp;Once you submit this, we can allow you to reset your own password w/o a Managers help.&nbsp;<br />
(the '<em>preferred</em>' method)
<br />

</td></tr></table>
</asp:Panel>

<asp:Label ID="lblPayPeriod" runat="server" /><br />
        <br /><br /><br /><br />

<br /><br /><br />
<asp:Button ID="btnMoveImports" Height="40" CssClass="btnMoveImports" Visible="false" runat="server" Text="Hey Bill - &nbsp;Somebody forgot to properly clear the No-Show imported loads. &nbsp;You'll need to click this button to pick up their slack." />
</center>
    
<div style="text-align:left; width:510px;margin:auto;">
<asp:Table  CellPadding="0" CellSpacing="0" GridLines="Both" ID="tblCertReport" Visible="false" runat="server">
</asp:Table>
<telerik:RadGrid ID="gridCertifications" runat="server" CellSpacing="0" width="500px"
            GridLines="None" ShowGroupPanel="False" EnableViewState="false" Visible="false">
<MasterTableView AutoGenerateColumns="False">
    <Columns>
        <telerik:GridBoundColumn DataField="Location" Visible="false"
            FilterControlAltText="Filter Location column" HeaderText="Location" 
            SortExpression="Location" UniqueName="Location">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Emp Number"  Visible="false"
            FilterControlAltText="Filter Emp Number column" HeaderText="Emp Number" 
            SortExpression="Emp Number" UniqueName="EmpNumber">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="eName" 
            FilterControlAltText="Filter eName column" HeaderText="Name"  visible="false"
            SortExpression="eName" UniqueName="eName">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="FirstName" 
            FilterControlAltText="Filter FirstName column" HeaderText="FirstName" Display="false"
            SortExpression="FirstName" UniqueName="FirstName">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="LastName" 
            FilterControlAltText="Filter LastName column" HeaderText="LastName" Display="false"
            SortExpression="LastName" UniqueName="LastName">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="JobTitle" 
            FilterControlAltText="Filter JobTitle column" HeaderText="JobTitle"  Display="false"
            SortExpression="JobTitle" UniqueName="JobTitle">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Certification" 
            FilterControlAltText="Filter Certification column" HeaderText="" 
            SortExpression="Certification" UniqueName="Certification">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Issued" 
            FilterControlAltText="Filter Issued column" HeaderText="Issued" ReadOnly="True" 
            SortExpression="Issued" UniqueName="Issued">
        </telerik:GridBoundColumn>
    </Columns>
<GroupHeaderTemplate>
<asp:Label ID="lblGH" runat="server" Text='<%# Eval("eName") %>' />
</GroupHeaderTemplate>
                <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="eName" />
                    </GroupByFields>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="eName"  FieldAlias="eName" />
                    </SelectFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
</MasterTableView>
</telerik:RadGrid>
</div>


<script type="text/javascript">
    (function () {
        var c = document.createElement('script');
        c.type = 'text/javascript'; c.async = true;
        c.src = "http://support.div-log.com/ChatLink.ashx?config=0&id=stlivechat0";
        var s = document.getElementsByTagName('script')[0];
        s.parentNode.insertBefore(c, s);
    })();
</script>

 


    </form>
</body>
</html>
