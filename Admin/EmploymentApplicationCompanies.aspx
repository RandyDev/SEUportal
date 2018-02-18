<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EmploymentApplicationCompanies.aspx.vb" Inherits="DiversifiedLogistics.EmploymentApplicationCompanies" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style type="text/css">
    CompanySection {
    width:425px;
    float:left;
    }
    LocationSection { 
    width:425px;
        }

    .auto-style1 {
        width: 181px;
    }

</style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
<table><tr><td>
<div id="CompanySection">
<table>
    <tr style="border:1px solid black; vertical-align:top;" >
        <td style="vertical-align:top;" class="auto-style1" >
            Select Company:
            <telerik:RadComboBox ID="cbemploymentcompanies" AutoPostBack="true" runat="server" />
            </td>
        <td colspan="2">
                    <table style="border:1px solid black;">
                <tr>
                    <td style="vertical-align:top;">
                        CompanyName:<telerik:RadTextBox ID="txtCompanyName" runat="server"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
<table>
    <tr>
        <td style="text-align:left;"><telerik:RadButton ID="btnNewCompany" runat="server" Text="Add New Company"></telerik:RadButton>
</td>
        <td style="text-align:center;"><asp:Label ID="lblcompanySaved" ForeColor="Red" Text="Saved" runat="server" Visible="false" /></td>
        <td style="text-align:right;"><telerik:RadButton id="btnDelCompany" runat="server" Text="Delete Company" /></td>
    </tr>
</table>
                        
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
</div>
    </td></tr></table>
 <div id="divlocationList" runat="server">

<telerik:RadGrid ID="RadGrid1" runat="server" Width="550" AllowSorting="True">
         <ClientSettings>
             <Selecting AllowRowSelect="True" />
             <Scrolling AllowScroll="True" UseStaticHeaders="True" />
         </ClientSettings>
         <MasterTableView DataKeyNames="companyLocationID" CommandItemSettings-AddNewRecordText="Add NEW Location" NoMasterRecordsText="No Locations Defined"  CommandItemDisplay="Top" AutoGenerateColumns="False" >
             <Columns>
                 <telerik:GridBoundColumn DataField="companyLocationID" DataType="System.Guid" readonly="True" visible="false" UniqueName="companyLocationID" />
                 <telerik:GridBoundColumn DataField="companyID" DataType="System.Guid" readonly="True" visible="false" UniqueName="companyID" />
                 <telerik:GridBoundColumn DataField="companyLocaName" HeaderText="Name" Visible="False" UniqueName="companyLocaName">
                 </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="companyLocaCity"  HeaderText="City" Visible="false" UniqueName="companyLocaCity">
                 </telerik:GridBoundColumn>
                 <telerik:gridtemplatecolumn HeaderText="City, ST" >
                     <HeaderStyle Width="130" />
                     <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "companyLocaCity")%>, <%# DataBinder.Eval(Container.DataItem, "companyLocaState")%>
                    </ItemTemplate>
                 </telerik:gridtemplatecolumn>
                 <telerik:GridBoundColumn DataField="companyLocaState" HeaderText="State" Visible="False" UniqueName="companyLocaState">
                 </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="companyLocaZip" HeaderText="Zip" UniqueName="companyLocaZip">
                     <HeaderStyle Width="50" />
                 </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="companyLocaAddress" HeaderText="Street" UniqueName="companyLocaAddress">
                     <HeaderStyle Width="130" />
                 </telerik:GridBoundColumn>
            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" 
                ConfirmDialogType="RadWindow" ConfirmText="Delete this Location?" 
                ConfirmTitle="Delete" Text="Delete" UniqueName="DeleteColumn">
                <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" />
                    <HeaderStyle Width="35" />
            </telerik:GridButtonColumn>             

             </Columns>
         </MasterTableView>
     </telerik:RadGrid>



</div>

</form>
</body>
</html>
