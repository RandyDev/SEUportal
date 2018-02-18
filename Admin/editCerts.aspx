<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="editCerts.aspx.vb" Inherits="DiversifiedLogistics.editCerts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            var currentTextBox = null;
            var currentDatePicker = null;

            //This method is called to handle the onclick and onfocus client side events for the texbox
            function showPopup(sender, e) {
                //this is a reference to the texbox which raised the event
                //see the methods exposed through the $telerik static client library here - http://www.telerik.com/help/aspnet-ajax/telerik-static-client-library.html 

                currentTextBox = sender.tagName == "INPUT" ? sender : $telerik.getPreviousHtmlNode(sender);

                //this gets a reference to the datepicker, which will be shown, to facilitate
                //the selection of a date
                var datePicker = $find("<%= RadDatePicker1.ClientID %>");

                //this variable is used to store a reference to the date picker, which is currently 
                //active
                currentDatePicker = datePicker;

                //this method first parses the date, that the user entered or selected, and then
                //sets it as a selected date to the picker
                datePicker.set_selectedDate(currentDatePicker.get_dateInput().parseDate(currentTextBox.value));

                //the code lines below show the calendar, which is used to select a date. The showPopup
                //function takes three arguments - the x and y coordinates where to show the calendar, as 
                //well as its height, derived from the offsetHeight property of the textbox
                var position = datePicker.getElementPosition(currentTextBox);
                datePicker.showPopup(position.x, position.y + currentTextBox.offsetHeight);
            }

            //this handler is used to set the text of the TextBox to the value of selected from the popup 
            function dateSelected(sender, args) {
                if (currentTextBox != null) {
                    //currentTextBox is the currently selected TextBox. Its value is set to the newly selected
                    //value of the picker
                    currentTextBox.value = args.get_newValue();
                }
            }

            //this function is used to parse the date entered or selected by the user
            function parseDate(sender, e) {
                if (currentDatePicker != null) {
                    var date = currentDatePicker.get_dateInput().parseDate(sender.value);
                    var dateInput = currentDatePicker.get_dateInput();

                    if (date == null) {
                        date = currentDatePicker.get_selectedDate();
                    }

                    var formattedDate = dateInput.get_dateFormatInfo().FormatDate(date, dateInput.get_displayDateFormat());
                    sender.value = formattedDate;
                }
            }

            function toggleAddCert(){
                var div1 = document.getElementById('divAddCert');
            	if (div1.style.display == 'none') {
		            div1.style.display = 'inline'
	            } else {
		            div1.style.display = 'none'
	            }
            }
        </script>

    </telerik:RadCodeBlock>
    <style type="text/css">
.lilBlueButton{
font-size:11px;
color:Blue;
}
        .MyImageButton
        {
           cursor: hand;
        }
        .EditFormHeader td
        {
            font-size: 14px;
            padding: 4px !important;
            color: #0066cc;
        }
        body{
        font-family:Arial;
        }

        </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
        <telerik:RadDatePicker ID="RadDatePicker1" Style="display: none;" MinDate="01/01/1900"
            MaxDate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:RadDatePicker>

    <div>


<fieldset style="padding:2px;">
<legend style="font-size:13px;" ><span class="lilBlueButton" onmouseover="this.style.cursor='pointer'" onclick="toggleAddCert();">Add New Certification</span> <asp:Label ID="lblEmpName" runat="server" /></legend>
<div id="divAddCert" runat="server">

<table runat="server">
    <tr>
            <td style="font-size:12px;">Cert Date:<br />    <telerik:RadDatePicker ID="RadDatePicker2" runat="server" Width="95" />

        </td>

        <td style="font-size:12px;"><table cellpadding="0" cellspacing="0"><tr><td>Certification:</td><td>&nbsp; <asp:LinkButton ID="lbtnAddCert" CssClass="lilBlueButton" runat="server" Text="add this certification" /></td></tr></table>
            <telerik:RadComboBox EmptyMessage="Select Cerification" width="350" AutoPostBack="true"
                ID="RadComboBox1" AllowCustomText="True" ShowDropDownOnTextboxClick ="true" runat="server" 
                DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="ID" />

        </td>
    </tr>
</table>
</div>
</fieldset>

<telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" >
<MasterTableView EditMode="InPlace" DataKeyNames="ID" CommandItemDisplay="None">
    <Columns>
        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
            <ItemStyle CssClass="MyImageButton" />
        </telerik:GridEditCommandColumn>

        <telerik:GridBoundColumn DataField="ID" DataType="System.Guid" HeaderText="ID" 
            ReadOnly="True" SortExpression="ID" UniqueName="ID" Visible="false">
        </telerik:GridBoundColumn>

        <telerik:GridTemplateColumn HeaderText="Cert Date" DataField="Date">
            <ItemTemplate>
                <%# Format(Eval("Date"), "dd-MMM-yyyy")%>
            </ItemTemplate>
            <EditItemTemplate>
                <telerik:RadDatePicker ID="rdpCertDate" runat="server" Width="55px" DatePopupButton-Visible="false" dbSelectedDate='<%# Eval("Date", "{0:d}") %>' />
                <%--<asp:TextBox ID="TextBox1" Width="65px" CssClass="radEnabledCss_Default" Text='< %# Eval("Date", "{0:d}") % >'
                    onblur="parseDate(this, event)"
                    runat="server"></asp:TextBox>
                    <asp:Image ID="popupImage" runat="server" ImageUrl="~/images/datePickerPopup.gif" AlternateText="Click to open Calendar popup"
                        onclick="showPopup(this, event)" />--%>
            </EditItemTemplate>
        </telerik:GridTemplateColumn>
        <telerik:GridBoundColumn DataField="Name" ReadOnly="true" HeaderText="Certification" />

        <telerik:GridButtonColumn ImageUrl="~/images/redX.gif" ConfirmDialogWidth="330px"  ConfirmDialogHeight="115px" ConfirmTitle="Warning, Will Robinson, Warning!" ConfirmText="Delete this certification!\nAre you sure?" ConfirmDialogType="RadWindow"
            ButtonType="ImageButton" CommandName="Delete" Text="Delete"
            UniqueName="DeleteColumn">
            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
        </telerik:GridButtonColumn>

    </Columns>
</MasterTableView>
        </telerik:RadGrid>


        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:rtdsConnectionString %>" 
            SelectCommand="SELECT Certification.ID, Certification.Date, CertificationType.Name, CertificationType.ID AS ctID FROM Certification INNER JOIN CertificationType ON Certification.TypeID = CertificationType.ID WHERE (Certification.EmployeeID = @empID) ORDER BY CertificationType.Name">
            <SelectParameters>
                <asp:QueryStringParameter Name="empID" QueryStringField="empID" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:rtdsConnectionString %>" 
            SelectCommand="SELECT ID, Name FROM CertificationType ORDER BY Name">
        </asp:SqlDataSource>



    </div>


<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
<script type="text/javascript">
    function GetRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)
        return oWindow;
    }

    function cancelAndClose() {
        var oWindow = GetRadWindow();
        oWindow.argument = "X";
        oWindow.close(arg);
    }

    function returnArg(arg) {
        var oWnd = GetRadWindow();
        oWnd.close(arg);
    }
    function decOnly(i) {
        var unicode = event.keyCode ? event.keyCode : event.charCode;
        if (unicode == 37 || unicode == 39) {    // ignore a left or right arrow press
            return
        }
        var t = i.value;
        if (t.length > 0) {
            t = t.replace(/[^\da-zA-Z&\s]+/g, '');
            i.value = t.toUpperCase();
        }
    }

</script>
</telerik:RadScriptBlock>



    </form>
</body>
</html>
