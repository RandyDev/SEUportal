<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="eMailPictures.aspx.vb" Inherits="DiversifiedLogistics.eMailPictures" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <div>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
        <asp:Label ID="lblLocaName" runat="server" />  <br /><br />
        Pictures last mailed: <asp:Label ID="lblLastMailed" runat="server" />
        <br />
<table><tr>
            <td>
            <telerik:RadDatePicker ID="dpStartDate" Width="110px" runat="server" />
        </td>
        <td>
            <telerik:RadDatePicker ID="dpEndDate" Width="110px" runat="server" />
        </td>
        <td>
            <asp:Button ID="btnSubmit" runat="server" Text="Show Selected Range"  />
        </td>
<td style="padding-left:15px">
                <asp:Linkbutton ID="lnkButton1" CssClass="lilblue" Text="Preview Photo Report (selected loads)" OnClientClick="PrintSelected();return false;" CausesValidation="false" Visible="false" runat="server" />
                <br /><asp:Linkbutton ID="Linkbutton1" CssClass="lilblue" Text="eMail Photo Report (selected loads)" OnClientClick="PrintSelected('email');return false;" CausesValidation="false" Visible="false" runat="server" />
</td>
       </tr></table>
</div>
Currently <asp:Label ID="lblNumPics" runat="server" /> pictures in <asp:Label ID="lblNumWorkOrders" runat="server" /> workorders.

        <br />
<telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" Width="926px" 
        CellSpacing="0" GridLines="None" AllowMultiRowSelection="True" AllowSorting="True">
        <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="True" >
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" />
            <Scrolling AllowScroll="True" ScrollHeight="500" UseStaticHeaders="True" />
        </ClientSettings>
<MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID" >
    <Columns> 
    <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" >
        <HeaderStyle Width="30px"/>
    </telerik:GridClientSelectColumn>

    <telerik:GridTemplateColumn UniqueName="PicCount" HeaderStyle-HorizontalAlign="Justify" HeaderStyle-CssClass="padmenot" HeaderImageUrl="~/images/camera.jpg" >
        <HeaderStyle Width="30px"/>
                <ItemTemplate>
            <%#IIf(IsDBNull(Eval("PicCount")), "--", Eval("PicCount"))%>
        </ItemTemplate>
    </telerik:GridTemplateColumn>

    <telerik:GridBoundColumn UniqueName="Department" DataField="Department" HeaderText="Department">
        <HeaderStyle Width="76px" />
    </telerik:GridBoundColumn>

    <telerik:GridTemplateColumn UniqueName="email" DataField="email" HeaderText="eMail" >
        <HeaderStyle Width="150px" />
                <ItemTemplate>
            <%#IIf(IsDBNull(Eval("email")), "NO eMail ON FILE", Eval("email"))%>
        </ItemTemplate>
    </telerik:GridTemplateColumn>

    <telerik:GridBoundColumn UniqueName="DockTime" DataField="DockTime" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy hh:mm}">
        <HeaderStyle Width="80px" />
    </telerik:GridBoundColumn>
    
    <telerik:GridBoundColumn UniqueName="PurchaseOrder" DataField="PurchaseOrder" HeaderText="PO #">
        <HeaderStyle Width="60px" />
    </telerik:GridBoundColumn>
    
    <telerik:GridBoundColumn UniqueName="BadPallets" DataField="BadPallets" HeaderText="BadPallets" Visible="false" />
    <telerik:GridBoundColumn UniqueName="Restacks" DataField="Restacks" HeaderText="Restacks" Visible="false" />
    <telerik:GridBoundColumn UniqueName="VendorNumber" DataField="VendorNumber" HeaderText="Vendor #">
        <HeaderStyle Width="85px" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn UniqueName="Vendor" DataField="Vendor" HeaderText="Vendor Name">
        <HeaderStyle Width="200px" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn UniqueName="Carrier" DataField="Carrier" HeaderText="Carrier">
        <HeaderStyle Width="200px" />
    </telerik:GridBoundColumn>
    <telerik:GridBoundColumn UniqueName="TrailerNumber" DataField="TrailerNumber" HeaderText="TrailerNumber" Visible="false" />
    <telerik:GridBoundColumn UniqueName="ID" DataField="ID" HeaderText="woid" Visible="false" />
    </Columns>
</MasterTableView>
</telerik:RadGrid>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" VisibleStatusbar="false" >
<Windows>
<telerik:RadWindow ID="winFreightIssues" Title="SEU Freight Issue Photo eMail Previewer" 
        Height="500" Width="400" Modal="True" Behaviors="Close, Resize, Maximize, Move" 
        EnableShadow="True" OnClientClose="closeit" runat="server" />

<telerik:RadWindow ID="RadWindow1" Title="SEU Freight Issue Photo eMailer" 
        Height="200" Width="400" Modal="True" Behaviors="Close" 
        OnClientClose="closeit" runat="server" />
</Windows>
</telerik:RadWindowManager>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">
    function PrintSelected(arg) {
        var grid = $find("<%=RadGrid1.ClientID %>");
                   var MasterTable = grid.get_masterTableView();
                   var selectedRows = MasterTable.get_selectedItems();
                   var printSelected = "";
                   if (selectedRows.length == 0) { alert("No rows selected"); return false; }
                   if (selectedRows.length > 50) { alert("Maximum 50 loads at a time.\nYou have " + selectedRows.length + " selected."); return false; }

                   for (var i = 0; i < selectedRows.length; i++) {
                       var row = selectedRows[i].getDataKeyValue("ID");
                       printSelected += row + ":";
                       //here cell.innerHTML holds the value of the cell 
                   }
                   if (arg == "email") {
                       emailPDFwin(printSelected)
                   } else {
                       openPDFwin(printSelected)
                   }

               }

    function emailPDFwin(arg) {
        var oManager = GetRadWindowManager();
        var loca = "/ClientSvcs/seuFreightIssues.aspx?woid=" + arg + "&email=true";
        oManager.open(loca, "RadWindow1");
    }

    function openPDFwin(arg) {
        var oManager = GetRadWindowManager();
        var loca = "/ClientSvcs/seuFreightIssues.aspx?woid=" + arg;
        oManager.open(loca, "winFreightIssues");
    }

               function closeit(oWnd, args) {
                   oWnd.setUrl("../seuLoader.aspx");
               }
           </script>
        </telerik:RadCodeBlock>    


    </form>
</body>
</html>
