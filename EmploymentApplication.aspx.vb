Imports System.Data
'Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class EmploymentApplication
    Inherits System.Web.UI.Page

    Public surNull As Date = CDate("1/1/1900")
    Public moyr As String = MonthName(Month(Date.Now), True) & " " & Year(Date.Now)
    Public companyid As String = String.Empty
    Public companyName As String = String.Empty
    Public locaid As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim dt As DataTable = Nothing
            If Request("co") > "" Then
                Select Case UCase(Request("co"))
                    Case "FCP"
                        imgapplicationLogo.ImageUrl = "~/images/FCP.jpg"
                        dt = getcompanyid(UCase(Request("co")))
                    Case "SEU"
                        imgapplicationLogo.ImageUrl = "~/images/seu.jpg"
                        dt = getcompanyid(UCase(Request("co")))
                    Case "TCE"
                        imgapplicationLogo.ImageUrl = "~/images/TripleCenterprise.jpg"
                        dt = getcompanyid(UCase(Request("co")))
                End Select
                companyid = dt.Rows(0).Item("CompanyID").ToString
                companyName = dt.Rows(0).Item("CompanyName")
                loadcbPreferredLocation(companyid)

            ElseIf Request("appid") > "" Then
                Dim apID As String = Request("appid").ToString
                Dim dba As New DBAccess
                dba.CommandText = "SELECT eac.companyName, eac.companyABR, eac.companyID, ea.LocationID " & _
                    "FROM employmentApplicationCompanies eac INNER JOIN " & _
                    "employmentApplicationLocations eal ON eac.companyID = eal.companyID RIGHT OUTER JOIN " & _
                    "EmploymentApplication ea ON eal.companyLocationID = ea.LocationID " & _
                    "WHERE (ea.EmploymentApplicationID = @EmploymentApplicationID) "
                dba.AddParameter("@EmploymentApplicationID", Request("appid"))
                dt = dba.ExecuteDataSet.Tables(0)
                Dim row As DataRow = dt.Rows(0)
                If Not IsDBNull(row.Item(0)) Then 'WE HAve data
                    companyName = row.Item(0)
                    companyid = row.Item(2).ToString
                    locaid = row.Item(3).ToString

                    Select Case row.Item(1)
                        Case "FCP"
                            imgapplicationLogo.ImageUrl = "~/images/FCP.jpg"
                        Case "SEU"
                            imgapplicationLogo.ImageUrl = "~/images/seu.jpg"
                        Case "TCE"
                            imgapplicationLogo.ImageUrl = "~/images/TripleCenterprise.jpg"
                    End Select
                Else ' don't have data
                    companyName = String.Empty
                    companyid = String.Empty
                    locaid = String.Empty
                End If 'has data
                loadcbPreferredLocation()
            Else
                'Missing required parameters
                Response.Redirect("/UnauthorizedNoMaster.aspx")
            End If

            lblApplicantIP.Text = "<br />Your IP Address: " & ipaddress & " &nbsp;<>&nbsp; TimeStamp: " & Date.Now()


            dpDesiredStartDate.SelectedDate = Date.Now()
            cbAppliedBeforeMonth = setComboMonths(cbAppliedBeforeMonth)
            cbMilitaryServiceFromDateMonth = setComboMonths(cbMilitaryServiceFromDateMonth)
            cbMilitaryServiceToDateMonth = setComboMonths(cbMilitaryServiceToDateMonth)
            cbPE1FromMonth = setComboMonths(cbPE1FromMonth)
            cbPE1ToMonth = setComboMonths(cbPE1ToMonth)
            cbPE2FromMonth = setComboMonths(cbPE2FromMonth)
            cbPE2ToMonth = setComboMonths(cbPE2ToMonth)
            cbPE3FromMonth = setComboMonths(cbPE3FromMonth)
            cbPE3ToMonth = setComboMonths(cbPE3ToMonth)
            cbPE4FromMonth = setComboMonths(cbPE4FromMonth)
            cbPE4ToMonth = setComboMonths(cbPE4ToMonth)
            cbMilitaryServiceFromDateYear = setComboYears(cbMilitaryServiceFromDateYear, 60)
            cbMilitaryServiceToDateYear = setComboYears(cbMilitaryServiceToDateYear, 40)
            cbMilitaryServiceFromDateYear.MaxHeight = 200
            cbMilitaryServiceToDateYear.MaxHeight = 200
            cbMilitaryServiceFromDateYear.DropDownWidth = 55
            cbMilitaryServiceToDateYear.DropDownWidth = 55
            cbMilitaryServiceFromDateYear.AllowCustomText = True
            cbAppliedBeforeYear = setComboYears(cbAppliedBeforeYear)
            cbPE1FromYear = setComboYears(cbPE1FromYear)
            cbPE1ToYear = setComboYears(cbPE1ToYear)
            cbPE2FromYear = setComboYears(cbPE2FromYear)
            cbPE2ToYear = setComboYears(cbPE2ToYear)
            cbPE3FromYear = setComboYears(cbPE3FromYear)
            cbPE3ToYear = setComboYears(cbPE3ToYear)
            cbPE4FromYear = setComboYears(cbPE4FromYear)
            cbPE4ToYear = setComboYears(cbPE4ToYear)

            Dim reqStr As String = HttpContext.Current.Request.QueryString("appid")
            If Not reqStr Is Nothing Then
                Dim ja As New EmploymentApplicationObject()
                If Utilities.IsValidGuid(reqStr) Then
                    Dim edal As New empDAL
                    ja = edal.getJobApp(reqStr)
                    LoadForm(ja)
                    pnlAck.Visible = False
                    lblApplicantIP.Visible = False
                    lblHelpABrother.Visible = True
                    btnSubmit.Text = "Save Changes"
                    lblErr.Visible = False
                    lblAckTitle.Text = "<center>BEFORE submitting any changes ...</center>"
                    btnSubmit.Enabled = True
                    btnSubmit.CommandArgument = ja.EmploymentApplicationID.ToString
                    btnSubmit.CommandName = "Update"
                Else
                    Response.Write("<br /> <br /> <br /> <center>don't be silly ... that's not even a valid GUID</center>")                    '
                    pnlApplication.Visible = False
                End If
            Else
                btnSubmit.CommandName = "AddNew"
                btnSubmit.CommandArgument = String.Empty
                RadToolTip1.Show()
            End If
        End If 'Not is PostBack
        txtZip.Attributes("onkeyup") = "decOnly(this);return false;"
        '        txtZip.Attributes("onblur") = "zipOnBlur();return false;"
    End Sub

    Public Function IpAddress() As String
        Dim strIpAddress As String
        strIpAddress = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If strIpAddress Is Nothing Then
            strIpAddress = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
        End If
        Return strIpAddress
    End Function

    Protected Sub loadcbPreferredLocation(ByVal companyid As String)
        cbPreferredLocation.Items.Clear()
        Dim dba As New DBAccess
        Dim dt As DataTable
        dba.CommandText = "SELECT companyLocaCity + ', ' + companyLocaState as local, companyID, companyLocationID FROM employmentApplicationLocations WHERE companyID=@companyID order by companyLocaCity,companyLocaState"
        dba.AddParameter("@companyID", companyid)
        dt = dba.ExecuteDataSet.Tables(0)
        cbPreferredLocation.DataSource = dt
        cbPreferredLocation.DataTextField = "local"
        cbPreferredLocation.DataValueField = "companyLocationID"
        cbPreferredLocation.DataBind()
        cbPreferredLocation.Text = ""
        cbPreferredLocation.EmptyMessage = "Select Preferred Location"
    End Sub
    Protected Sub loadcbPreferredLocation()
        cbPreferredLocation.Items.Clear()
        Dim dba As New DBAccess
        Dim dt As DataTable
        dba.CommandText = "SELECT employmentApplicationCompanies.companyABR + ' - ' + employmentApplicationLocations.companyLocaCity + ', ' + employmentApplicationLocations.companyLocaState " & _
            "AS locationName, employmentApplicationLocations.companyLocationID " & _
            "FROM employmentApplicationCompanies INNER JOIN " & _
            "employmentApplicationLocations ON employmentApplicationCompanies.companyID = employmentApplicationLocations.companyID " & _
            "order by locationName"
        dt = dba.ExecuteDataSet.Tables(0)
        cbPreferredLocation.DataSource = dt
        cbPreferredLocation.DataTextField = "locationName"
        cbPreferredLocation.DataValueField = "companyLocationID"
        cbPreferredLocation.DataBind()
        cbPreferredLocation.Text = ""
        cbPreferredLocation.EmptyMessage = "Select Preferred Location"
    End Sub

    Protected Sub LoadForm(ByVal ja As EmploymentApplicationObject)
        txtFirstName.Text = ja.FirstName
        txtMiddleNameInitial.Text = ja.MiddleInitial
        Dim dba As New DBAccess
        Dim dt As DataTable = New DataTable
        If Utilities.IsValidGuid(ja.LocationID.ToString) Then ' look up company
            dba.CommandText = "SELECT employmentApplicationCompanies.companyABR + ' - ' + employmentApplicationLocations.companyLocaCity + ', ' + employmentApplicationLocations.companyLocaState " & _
                "AS locationName " & _
                "FROM employmentApplicationCompanies INNER JOIN " & _
                "employmentApplicationLocations ON employmentApplicationCompanies.companyID = employmentApplicationLocations.companyID " & _
                "WHERE companyLocationID=@companyLocationID"
            dba.AddParameter("@companyLocationID", ja.LocationID)
            Dim locaName As String = dba.ExecuteScalar
            If Not locaName Is Nothing Then cbPreferredLocation.Text = locaName
            If cbPreferredLocation.Text = locaName Then cbPreferredLocation.SelectedValue = ja.LocationID.ToString
        End If



        txtLastName.Text = ja.LastName
        txtReferredby.Text = ja.Referredby
        txtStreetAddress.Text = ja.StreetAddress
        txtCity.Text = ja.City
        txtState.Text = ja.State
        txtZip.Text = ja.Zip
        txtPrimaryPhone.Text = ja.PrimaryPhone
        txtAltPhone.Text = ja.AltPhone
        txtEmail.Text = ja.Email
        txtDesiredPosition.Text = ja.DesiredPosition
        dpDesiredStartDate.SelectedDate = ja.DesiredStartDate
        If ja.DesiredSalary.Length > 5 Then
            Dim ar As Array = Split(ja.DesiredSalary, " ")
            txtDesiredSalary.Text = ar(0)
            Dim mitem As RadComboBoxItem = cbDesiredSalaryPeriod.FindItemByText(ar(1))
            mitem.Selected = True
        End If
        rbCurrentlyEmployedNo.Checked = Not ja.CurrentlyEmployed
        rbCurrentlyEmployedYes.Checked = ja.CurrentlyEmployed

        rbAskCurrentEmployerNo.Checked = Not ja.AskCurrentEmployer
        rbAskCurrentEmployerYes.Checked = ja.AskCurrentEmployer

        rbAppliedBeforeNo.Checked = Not ja.AppliedBefore
        rbAppliedBeforeYes.Checked = ja.AppliedBefore

        txtAppliedBeforeLocation.Text = ja.AppliedBeforeLocation

        If IsDate(ja.AppliedBeforeDate) Then
            If ja.AppliedBeforeDate > surNull Then
                Dim mitem As RadComboBoxItem = cbAppliedBeforeMonth.FindItemByText(MonthName(Month(ja.AppliedBeforeDate), True))
                mitem.Selected = True
                Dim yitem As RadComboBoxItem = cbAppliedBeforeYear.FindItemByText(Year(ja.AppliedBeforeDate).ToString)
                yitem.Selected = True
            End If
        End If
        Dim cbEd As RadComboBoxItem = cbEducationLevel.FindItemByText(ja.EducationLevel)
        cbEd.Selected = True
        txtSchool1.Text = ja.School1
        txtSchool1Location.Text = ja.School1Location
        txtSchool1YearsAttended.Text = ja.School1YearsAttended

        If ja.School1.Length > 0 Then
            Dim mitem As RadComboBoxItem = cbSchool1Graduated.FindItemByText(IIf(ja.School1Graduated, "Yes", "No"))
            mitem.Selected = True
        End If

        txtSchool1SubjectsStudied.Text = ja.School1SubjectsStudied
        txtSchool2.Text = ja.School2
        txtSchool2Location.Text = ja.School2Location
        txtSchool2YearsAttended.Text = ja.School2YearsAttended
        If ja.School2.Length > 0 Then
            Dim mitem As RadComboBoxItem = cbSchool2Graduated.FindItemByText(IIf(ja.School2Graduated, "Yes", "No"))
            mitem.Selected = True
        End If
        txtSchool2SubjectsStudied.Text = ja.School2SubjectsStudied
        txtSchool3.Text = ja.School3
        txtSchool3Location.Text = ja.School3Location
        txtSchool3YearsAttended.Text = ja.School3YearsAttended
        If ja.School3.Length > 0 Then
            Dim mitem As RadComboBoxItem = cbSchool3Graduated.FindItemByText(IIf(ja.School3Graduated, "Yes", "No"))
            mitem.Selected = True
        End If
        txtSchool3SubjectsStudied.Text = ja.School3SubjectsStudied
        txtSpecialSkills.Text = ja.SpecialSkills

        Dim milCBI As RadComboBoxItem = cbMilitaryBranch.FindItemByText(ja.MilitaryBranch)
        milCBI.Selected = True

        If IsDate(ja.MilitaryServiceFromDate) And ja.MilitaryServiceFromDate > surNull Then
            Dim mitem As RadComboBoxItem = cbMilitaryServiceFromDateMonth.FindItemByText(MonthName(Month(ja.MilitaryServiceFromDate), True))
            mitem.Selected = True
            Dim yitem As RadComboBoxItem = cbMilitaryServiceFromDateYear.FindItemByText(Year(ja.MilitaryServiceFromDate).ToString)
            yitem.Selected = True
        End If
        If IsDate(ja.MilitaryServiceToDate) And ja.MilitaryServiceToDate > surNull Then
            Dim mitem As RadComboBoxItem = cbMilitaryServiceToDateMonth.FindItemByText(MonthName(Month(ja.MilitaryServiceToDate), True))
            mitem.Selected = True
            Dim yitem As RadComboBoxItem = cbMilitaryServiceToDateYear.FindItemByText(Year(ja.MilitaryServiceToDate).ToString)
            yitem.Selected = True
        End If
        Dim milRank As RadComboBoxItem = cbMilitaryRank.FindItemByText(ja.MilitaryRank)
        milRank.Selected = True

        If IsDate(ja.pe1FromDate) Then
            If ja.pe1FromDate > surNull Then
                Dim mitem As RadComboBoxItem = cbPE1FromMonth.FindItemByText(MonthName(Month(ja.pe1FromDate), True))
                mitem.Selected = True
                Dim yitem As RadComboBoxItem = cbPE1FromYear.FindItemByText(Year(ja.pe1FromDate).ToString)
                yitem.Selected = True
            End If
        End If
        If IsDate(ja.pe1ToDate) Then
            If ja.pe1ToDate > surNull Then
                Dim mitem As RadComboBoxItem = cbPE1ToMonth.FindItemByText(MonthName(Month(ja.pe1ToDate), True))
                mitem.Selected = True
                Dim yitem As RadComboBoxItem = cbPE1ToYear.FindItemByText(Year(ja.pe1ToDate).ToString)
                yitem.Selected = True
            End If
        End If
        If IsDate(ja.pe2FromDate) Then
            If ja.pe2FromDate > surNull Then
                Dim mitem As RadComboBoxItem = cbPE2FromMonth.FindItemByText(MonthName(Month(ja.pe2FromDate), True))
                mitem.Selected = True
                Dim yitem As RadComboBoxItem = cbPE2FromYear.FindItemByText(Year(ja.pe2FromDate).ToString)
                yitem.Selected = True
            End If
        End If
        If IsDate(ja.pe2ToDate) Then
            If ja.pe2ToDate > surNull Then
                Dim mitem As RadComboBoxItem = cbPE2ToMonth.FindItemByText(MonthName(Month(ja.pe2ToDate), True))
                mitem.Selected = True
                Dim yitem As RadComboBoxItem = cbPE2ToYear.FindItemByText(Year(ja.pe2ToDate).ToString)
                yitem.Selected = True
            End If
        End If
        If IsDate(ja.pe3FromDate) Then
            If ja.pe3FromDate > surNull Then
                Dim mitem As RadComboBoxItem = cbPE3FromMonth.FindItemByText(MonthName(Month(ja.pe3FromDate), True))
                mitem.Selected = True
                Dim yitem As RadComboBoxItem = cbPE3FromYear.FindItemByText(Year(ja.pe3FromDate).ToString)
                yitem.Selected = True
            End If
        End If
        If IsDate(ja.pe3ToDate) Then
            If ja.pe3ToDate > surNull Then
                Dim mitem As RadComboBoxItem = cbPE3ToMonth.FindItemByText(MonthName(Month(ja.pe3ToDate), True))
                mitem.Selected = True
                Dim yitem As RadComboBoxItem = cbPE3ToYear.FindItemByText(Year(ja.pe3ToDate).ToString)
                yitem.Selected = True
            End If
        End If
        If IsDate(ja.pe4FromDate) Then
            If ja.pe4FromDate > surNull Then
                Dim mitem As RadComboBoxItem = cbPE4FromMonth.FindItemByText(MonthName(Month(ja.pe4FromDate), True))
                mitem.Selected = True
                Dim yitem As RadComboBoxItem = cbPE4FromYear.FindItemByText(Year(ja.pe4FromDate).ToString)
                yitem.Selected = True
            End If
        End If
        If IsDate(ja.pe4ToDate) Then
            If ja.pe4ToDate > surNull Then
                Dim mitem As RadComboBoxItem = cbPE4ToMonth.FindItemByText(MonthName(Month(ja.pe4ToDate), True))
                mitem.Selected = True
                Dim yitem As RadComboBoxItem = cbPE4ToYear.FindItemByText(Year(ja.pe4ToDate).ToString)
                yitem.Selected = True
            End If
        End If

        txtPE1.Text = ja.PE1
        txtPE1Location.Text = ja.PE1Location
        txtPE1phone.Text = ja.PE1phone
        txtPE1salary.Text = ja.PE1salary
        txtPE1position.Text = ja.PE1position
        txtPE1reasonForLeaving.Text = ja.PE1reasonForLeaving
        txtPE2.Text = ja.PE2
        txtPE2Location.Text = ja.PE2Location
        txtPE2phone.Text = ja.PE2phone
        txtPE2salary.Text = ja.PE2salary
        txtPE2position.Text = ja.PE2position
        txtPE2reasonForLeaving.Text = ja.PE2reasonForLeaving
        txtPE3.Text = ja.PE3
        txtPE3Location.Text = ja.PE3Location
        txtPE3phone.Text = ja.PE3phone
        txtPE3salary.Text = ja.PE3salary
        txtPE3position.Text = ja.PE3position
        txtPE3reasonForLeaving.Text = ja.PE3reasonForLeaving
        txtPE4.Text = ja.PE4
        txtPE4Location.Text = ja.PE4Location
        txtPE4phone.Text = ja.PE4phone
        txtPE4salary.Text = ja.PE4salary
        txtPE4position.Text = ja.PE4position
        txtPE4reasonForLeaving.Text = ja.PE4reasonForLeaving
        txtReference1.Text = ja.Reference1
        txxtReference1YrsKnown.Text = ja.Reference1YrsKnown
        txtReference1Contact.Text = ja.Reference1Contact
        txtReference2.Text = ja.Reference2
        txtReference2YrsKnown.Text = ja.Reference2YrsKnown
        txtReference2Contact.Text = ja.Reference2Contact
        txtReference3.Text = ja.Reference3
        txtReference3YrsKnown.Text = ja.Reference3YrsKnown
        txtReference3Contact.Text = ja.Reference3Contact

        '        ja.EmploymentApplicationID = Guid.NewGuid()
        '        ja.LocationID = Utilities.zeroGuid
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        lblerrStr.Visible = False
        Dim errString As String = validateForm()
        Dim surrogateNullDate As Date = surNull
        If errString > String.Empty Then
            lblerrStr.Visible = True
            errString = errString.Replace("\n", "<br />")
            errString = "<center>Oophf!!</center>" & errString
            lblerrStr.Text = "<table style=""border:1px solid red;"" align=""center""><tr><td>" & errString & "</td></tr></table>"
        Else
            Dim ja As New EmploymentApplicationObject
            ja.LocationID = New Guid(cbPreferredLocation.SelectedValue)
            ja.FirstName = txtFirstName.Text.Trim()
            ja.MiddleInitial = txtMiddleNameInitial.Text.Trim()
            ja.LastName = txtLastName.Text.Trim()
            ja.Referredby = txtReferredby.Text.Trim()
            ja.StreetAddress = txtStreetAddress.Text.Trim()
            ja.City = txtCity.Text.Trim()
            ja.State = txtState.Text.Trim()
            ja.Zip = txtZip.Text.Trim()
            ja.PrimaryPhone = IIf(txtPrimaryPhone.Text.Length > 0, txtPrimaryPhone.TextWithLiterals, "")
            ja.AltPhone = IIf(txtAltPhone.Text.Length > 0, txtAltPhone.TextWithLiterals, "")
            ja.Email = txtEmail.Text.Trim()
            ja.DesiredPosition = txtDesiredPosition.Text.Trim()
            ja.DesiredStartDate = dpDesiredStartDate.SelectedDate
            ja.DesiredSalary = IIf(txtDesiredSalary.Text.Length > 0, txtDesiredSalary.Text & " " & cbDesiredSalaryPeriod.SelectedItem.Text, "")
            ja.CurrentlyEmployed = rbCurrentlyEmployedYes.Checked
            ja.AskCurrentEmployer = rbAskCurrentEmployerYes.Checked
            ja.AppliedBefore = rbAppliedBeforeYes.Checked
            ja.AppliedBeforeLocation = txtAppliedBeforeLocation.Text.Trim()
            ja.AppliedBeforeDate = cbAppliedBeforeMonth.SelectedItem.Text & " " & cbAppliedBeforeYear.SelectedItem.Text
            ja.EducationLevel = cbEducationLevel.SelectedItem.Text.Trim()
            ja.School1 = txtSchool1.Text.Trim()
            ja.School1Location = txtSchool1Location.Text.Trim()
            ja.School1YearsAttended = txtSchool1YearsAttended.Text.Trim()
            If cbSchool1Graduated.SelectedIndex > -1 Then ja.School1Graduated = cbSchool1Graduated.SelectedItem.Text = "Yes"
            ja.School1SubjectsStudied = txtSchool1SubjectsStudied.Text.Trim()
            ja.School2 = txtSchool2.Text.Trim()
            ja.School2Location = txtSchool2Location.Text.Trim()
            ja.School2YearsAttended = txtSchool2YearsAttended.Text.Trim()
            If cbSchool2Graduated.SelectedIndex > -1 Then ja.School2Graduated = cbSchool2Graduated.SelectedItem.Text = "Yes"
            ja.School2SubjectsStudied = txtSchool2SubjectsStudied.Text.Trim()
            ja.School3 = txtSchool3.Text.Trim()
            ja.School3Location = txtSchool3Location.Text.Trim()
            ja.School3YearsAttended = txtSchool3YearsAttended.Text.Trim()
            If cbSchool3Graduated.SelectedIndex > -1 Then ja.School3Graduated = cbSchool3Graduated.SelectedItem.Text = "Yes"
            ja.School3SubjectsStudied = txtSchool3SubjectsStudied.Text.Trim()
            ja.SpecialSkills = txtSpecialSkills.Text.Trim()
            ja.MilitaryBranch = cbMilitaryBranch.SelectedItem.Text.Trim()
            If cbMilitaryServiceFromDateMonth.Text > "" And cbMilitaryServiceFromDateYear.Text > "" Then
                ja.MilitaryServiceFromDate = cbMilitaryServiceFromDateMonth.Text & "/15/" & cbMilitaryServiceFromDateYear.Text
            Else
                ja.MilitaryServiceFromDate = surrogateNullDate
            End If
            If cbMilitaryServiceToDateMonth.Text > "" And cbMilitaryServiceToDateYear.Text > "" Then
                ja.MilitaryServiceToDate = cbMilitaryServiceToDateMonth.Text & "/15/" & cbMilitaryServiceToDateYear.Text
            Else
                ja.MilitaryServiceToDate = surrogateNullDate
            End If
            ja.MilitaryRank = cbMilitaryRank.SelectedItem.Text.Trim()
            ja.pe1FromDate = IIf(cbPE1FromMonth.Text > "" And cbPE1FromYear.Text > "", cbPE1FromMonth.Text & "/15/" & cbPE1FromYear.Text, surrogateNullDate)
            ja.pe1ToDate = IIf(cbPE1ToMonth.Text > "" And cbPE1ToYear.Text > "", cbPE1ToMonth.Text & "/15/" & cbPE1ToYear.Text, surrogateNullDate)
            ja.pe2FromDate = IIf(cbPE2FromMonth.Text > "" And cbPE2FromYear.Text > "", cbPE2FromMonth.Text & "/15/" & cbPE2FromYear.Text, surrogateNullDate)
            ja.pe2ToDate = IIf(cbPE2ToMonth.Text > "" And cbPE2ToYear.Text > "", cbPE2ToMonth.Text & "/15/" & cbPE2ToYear.Text, surrogateNullDate)
            ja.pe3FromDate = IIf(cbPE3FromMonth.Text > "" And cbPE3FromYear.Text > "", cbPE3FromMonth.SelectedItem.Text & "/15/" & cbPE3FromYear.Text, surrogateNullDate)
            ja.pe3ToDate = IIf(cbPE3ToMonth.Text > "" And cbPE3ToYear.Text > "", cbPE3ToMonth.Text & "/15/" & cbPE3ToYear.Text, surrogateNullDate)
            ja.pe4FromDate = IIf(cbPE4FromMonth.Text > "" And cbPE4FromYear.Text > "", cbPE4FromMonth.Text & "/15/" & cbPE4FromYear.Text, surrogateNullDate)
            ja.pe4ToDate = IIf(cbPE4ToMonth.Text > "" And cbPE4ToYear.Text > "", cbPE4ToMonth.Text & "/15/" & cbPE4ToYear.Text, surrogateNullDate)

            ja.PE1 = txtPE1.Text.Trim()
            ja.PE1Location = txtPE1Location.Text.Trim()
            ja.PE1phone = IIf(txtPE1phone.Text.Length > 0, txtPE1phone.TextWithLiterals, "")
            ja.PE1salary = txtPE1salary.Text.Trim()
            ja.PE1position = txtPE1position.Text.Trim()
            ja.PE1reasonForLeaving = txtPE1reasonForLeaving.Text.Trim()
            ja.PE2 = txtPE2.Text.Trim()
            ja.PE2Location = txtPE2Location.Text.Trim()
            ja.PE2phone = IIf(txtPE2phone.Text.Length > 0, txtPE2phone.TextWithLiterals, "")
            ja.PE2salary = txtPE2salary.Text.Trim()
            ja.PE2position = txtPE2position.Text.Trim()
            ja.PE2reasonForLeaving = txtPE2reasonForLeaving.Text.Trim()
            ja.PE3 = txtPE3.Text.Trim()
            ja.PE3Location = txtPE3Location.Text.Trim()
            ja.PE3phone = IIf(txtPE3phone.Text.Length > 0, txtPE3phone.TextWithLiterals, "")
            ja.PE3salary = txtPE3salary.Text.Trim()
            ja.PE3position = txtPE3position.Text.Trim()
            ja.PE3reasonForLeaving = txtPE3reasonForLeaving.Text.Trim()
            ja.PE4 = txtPE4.Text.Trim()
            ja.PE4Location = txtPE4Location.Text.Trim()
            ja.PE4phone = IIf(txtPE4phone.Text.Length > 0, txtPE4phone.TextWithLiterals, "")
            ja.PE4salary = txtPE4salary.Text.Trim()
            ja.PE4position = txtPE4position.Text.Trim()
            ja.PE4reasonForLeaving = txtPE4reasonForLeaving.Text.Trim()
            ja.Reference1 = txtReference1.Text.Trim()
            ja.Reference1YrsKnown = txxtReference1YrsKnown.Text.Trim()
            ja.Reference1Contact = txtReference1Contact.Text.Trim()
            ja.Reference2 = txtReference2.Text.Trim()
            ja.Reference2YrsKnown = txtReference2YrsKnown.Text.Trim()
            ja.Reference2Contact = txtReference2Contact.Text.Trim()
            ja.Reference3 = txtReference3.Text.Trim()
            ja.Reference3YrsKnown = txtReference3YrsKnown.Text.Trim()
            ja.Reference3Contact = txtReference3Contact.Text.Trim()


            Dim sqlStr As String = String.Empty
            Select Case btnSubmit.CommandName
                Case "Update"
                    ja.EmploymentApplicationID = New Guid(btnSubmit.CommandArgument)
                    ja.LocationID = New Guid(cbPreferredLocation.SelectedValue)
                    'not on update
                    '                    ja.Rating = RadRating.v0
                    '                    ja.TimeStamp = Date.Now()
                    '                    ja.ApplicantIP = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")

                    sqlStr = "UPDATE EmploymentApplication SET LocationID=@LocationID, FirstName=@FirstName, MiddleInitial=@MiddleInitial, LastName=@LastName, Referredby=@Referredby, " & _
                       "StreetAddress=@StreetAddress, Zip=@Zip, City=@City, State=@State, PrimaryPhone=@PrimaryPhone, AltPhone=@AltPhone, " & _
                       "Email=@Email, DesiredPosition=@DesiredPosition, DesiredStartDate=@DesiredStartDate, DesiredSalary=@DesiredSalary, " & _
                       "CurrentlyEmployed=@CurrentlyEmployed, AskCurrentEmployer=@AskCurrentEmployer, AppliedBefore=@AppliedBefore, " & _
                       "AppliedBeforeLocation=@AppliedBeforeLocation, AppliedBeforeDate=@AppliedBeforeDate, EducationLevel=@EducationLevel, " & _
                       "School1=@School1, School1Location=@School1Location, School1YearsAttended=@School1YearsAttended, School1Graduated=@School1Graduated, " & _
                       "School1SubjectsStudied=@School1SubjectsStudied, School2=@School2, School2Location=@School2Location, " & _
                       "School2YearsAttended=@School2YearsAttended, School2Graduated=@School2Graduated, School2SubjectsStudied=@School2SubjectsStudied, " & _
                       "School3=@School3, School3Location=@School3Location, School3YearsAttended=@School3YearsAttended, School3Graduated=@School3Graduated, " & _
                       "School3SubjectsStudied=@School3SubjectsStudied, SpecialSkills=@SpecialSkills, MilitaryBranch=@MilitaryBranch, " & _
                       "MilitaryServiceFromDate=@MilitaryServiceFromDate, MilitaryServiceToDate=@MilitaryServiceToDate, MilitaryRank=@MilitaryRank, " & _
                       "pe1FromDate=@pe1FromDate, pe1ToDate=@pe1ToDate, PE1=@PE1, PE1Location=@PE1Location, PE1phone=@PE1phone, PE1salary=@PE1salary, " & _
                       "PE1position=@PE1position, PE1reasonForLeaving=@PE1reasonForLeaving, pe2FromDate=@pe2FromDate, pe2ToDate=@pe2ToDate, PE2=@PE2, " & _
                       "PE2Location=@PE2Location, PE2phone=@PE2phone, PE2salary=@PE2salary, PE2position=@PE2position, " & _
                       "PE2reasonForLeaving=@PE2reasonForLeaving, pe3FromDate=@pe3FromDate, pe3ToDate=@pe3ToDate, PE3=@PE3, PE3Location=@PE3Location, " & _
                       "PE3phone=@PE3phone, PE3salary=@PE3salary, PE3position=@PE3position, PE3reasonForLeaving=@PE3reasonForLeaving, " & _
                       "pe4FromDate=@pe4FromDate, pe4ToDate=@pe4ToDate, PE4=@PE4, PE4Location=@PE4Location, PE4phone=@PE4phone, PE4salary=@PE4salary, " & _
                       "PE4position=@PE4position, PE4reasonForLeaving=@PE4reasonForLeaving, Reference1=@Reference1, " & _
                       "Reference1YrsKnown=@Reference1YrsKnown, Reference1Contact=@Reference1Contact, Reference2=@Reference2, " & _
                       "Reference2YrsKnown=@Reference2YrsKnown, Reference2Contact=@Reference2Contact, Reference3=@Reference3, " & _
                       "Reference3YrsKnown=@Reference3YrsKnown, Reference3Contact=@Reference3Contact " & _
                       "WHERE EmploymentApplicationID=@EmploymentApplicationID"
                Case "AddNew"
                    ja.EmploymentApplicationID = Guid.NewGuid()
                    ja.LocationID = New Guid(cbPreferredLocation.SelectedValue)
                    ja.Rating = 0
                    ja.TimeStamp = Date.Now()
                    ja.ApplicantIP = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
                    sqlStr = "INSERT INTO EmploymentApplication (EmploymentApplicationID,LocationID,FirstName,MiddleInitial,LastName,Referredby,StreetAddress,Zip,City,State, " & _
                       "PrimaryPhone,AltPhone,Email,DesiredPosition,DesiredStartDate,DesiredSalary,CurrentlyEmployed,AskCurrentEmployer, " & _
                       "AppliedBefore,AppliedBeforeLocation,AppliedBeforeDate,EducationLevel,School1,School1Location,School1YearsAttended,School1Graduated, " & _
                       "School1SubjectsStudied,School2,School2Location,School2YearsAttended,School2Graduated,School2SubjectsStudied,School3,School3Location,School3YearsAttended, " & _
                       "School3Graduated,School3SubjectsStudied,SpecialSkills,MilitaryBranch,MilitaryServiceFromDate,MilitaryServiceToDate,MilitaryRank, " & _
                       "pe1FromDate,pe1ToDate,PE1,PE1Location,PE1phone,PE1salary,PE1position,PE1reasonForLeaving,pe2FromDate,pe2ToDate,PE2,PE2Location,PE2phone,PE2salary, " & _
                       "PE2position,PE2reasonForLeaving,pe3FromDate,pe3ToDate,PE3,PE3Location,PE3phone,PE3salary,PE3position,PE3reasonForLeaving,pe4FromDate, " & _
                       "pe4ToDate,PE4,PE4Location,PE4phone,PE4salary,PE4position,PE4reasonForLeaving,Reference1,Reference1YrsKnown,Reference1Contact,Reference2, " & _
                       "Reference2YrsKnown,Reference2Contact,Reference3,Reference3YrsKnown,Reference3Contact, " & _
                       "Rating,TimeStamp,ApplicantIP) " & _
                       "VALUES (@EmploymentApplicationID, @LocationID, @FirstName, @MiddleInitial, @LastName, @Referredby, @StreetAddress, @Zip, @City, @State, " & _
                       "@PrimaryPhone, @AltPhone, @Email, @DesiredPosition, @DesiredStartDate, @DesiredSalary, @CurrentlyEmployed, @AskCurrentEmployer, " & _
                       "@AppliedBefore, @AppliedBeforeLocation, @AppliedBeforeDate, @EducationLevel, @School1,@School1Location, @School1YearsAttended, @School1Graduated, " & _
                       "@School1SubjectsStudied, @School2, @School2Location, @School2YearsAttended, @School2Graduated, @School2SubjectsStudied, @School3, @School3Location, @School3YearsAttended, " & _
                       "@School3Graduated, @School3SubjectsStudied, @SpecialSkills, @MilitaryBranch, @MilitaryServiceFromDate, @MilitaryServiceToDate, @MilitaryRank, " & _
                       "@pe1FromDate, @pe1ToDate, @PE1, @PE1Location, @PE1phone, @PE1salary, @PE1position, @PE1reasonForLeaving, @pe2FromDate, @pe2ToDate, @PE2, @PE2Location, @PE2phone, @PE2salary, " & _
                       "@PE2position, @PE2reasonForLeaving, @pe3FromDate, @pe3ToDate, @PE3, @PE3Location, @PE3phone, @PE3salary, @PE3position, @PE3reasonForLeaving, @pe4FromDate, " & _
                       "@pe4ToDate, @PE4, @PE4Location, @PE4phone, @PE4salary, @PE4position, @PE4reasonForLeaving, @Reference1, @Reference1YrsKnown, @Reference1Contact, @Reference2, " & _
                       "@Reference2YrsKnown, @Reference2Contact, @Reference3, @Reference3YrsKnown, @Reference3Contact, " & _
                       "@Rating, @TimeStamp, @ApplicantIP)"
            End Select

            Dim dba As New DBAccess("rtds")
            dba.CommandText = sqlStr
            dba.AddParameter("@EmploymentApplicationID", ja.EmploymentApplicationID)
            dba.AddParameter("@LocationID", ja.LocationID)
            dba.AddParameter("@FirstName", ja.FirstName)
            dba.AddParameter("@MiddleInitial", ja.MiddleInitial)
            dba.AddParameter("@LastName", ja.LastName)
            dba.AddParameter("@Referredby", ja.Referredby)
            dba.AddParameter("@StreetAddress", ja.StreetAddress)
            dba.AddParameter("@Zip", ja.Zip)
            dba.AddParameter("@City", ja.City)
            dba.AddParameter("@State", ja.State)
            dba.AddParameter("@PrimaryPhone", ja.PrimaryPhone)
            dba.AddParameter("@AltPhone", ja.AltPhone)
            dba.AddParameter("@Email", ja.Email)
            dba.AddParameter("@DesiredPosition", ja.DesiredPosition)
            dba.AddParameter("@DesiredStartDate", ja.DesiredStartDate)
            dba.AddParameter("@DesiredSalary", ja.DesiredSalary)
            dba.AddParameter("@CurrentlyEmployed", ja.CurrentlyEmployed)
            dba.AddParameter("@AskCurrentEmployer", ja.AskCurrentEmployer)
            dba.AddParameter("@AppliedBefore", ja.AppliedBefore)
            dba.AddParameter("@AppliedBeforeLocation", ja.AppliedBeforeLocation)
            dba.AddParameter("@AppliedBeforeDate", ja.AppliedBeforeDate)
            dba.AddParameter("@EducationLevel", ja.EducationLevel)
            dba.AddParameter("@School1", ja.School1)
            dba.AddParameter("@School1Location", ja.School1Location)
            dba.AddParameter("@School1YearsAttended", ja.School1YearsAttended)
            dba.AddParameter("@School1Graduated", ja.School1Graduated)
            dba.AddParameter("@School1SubjectsStudied", ja.School1SubjectsStudied)
            dba.AddParameter("@School2", ja.School2)
            dba.AddParameter("@School2Location", ja.School2Location)
            dba.AddParameter("@School2YearsAttended", ja.School2YearsAttended)
            dba.AddParameter("@School2Graduated", ja.School2Graduated)
            dba.AddParameter("@School2SubjectsStudied", ja.School2SubjectsStudied)
            dba.AddParameter("@School3", ja.School3)
            dba.AddParameter("@School3Location", ja.School3Location)
            dba.AddParameter("@School3YearsAttended", ja.School3YearsAttended)
            dba.AddParameter("@School3Graduated", ja.School3Graduated)
            dba.AddParameter("@School3SubjectsStudied", ja.School3SubjectsStudied)
            dba.AddParameter("@SpecialSkills", ja.SpecialSkills)
            dba.AddParameter("@MilitaryBranch", ja.MilitaryBranch)
            dba.AddParameter("@MilitaryServiceFromDate", ja.MilitaryServiceFromDate)
            dba.AddParameter("@MilitaryServiceToDate", ja.MilitaryServiceToDate)
            dba.AddParameter("@MilitaryRank", ja.MilitaryRank)
            dba.AddParameter("@pe1FromDate", ja.pe1FromDate)
            dba.AddParameter("@pe1ToDate", ja.pe1ToDate)
            dba.AddParameter("@PE1", ja.PE1)
            dba.AddParameter("@PE1Location", ja.PE1Location)
            dba.AddParameter("@PE1phone", ja.PE1phone)
            dba.AddParameter("@PE1salary", ja.PE1salary)
            dba.AddParameter("@PE1position", ja.PE1position)
            dba.AddParameter("@PE1reasonForLeaving", ja.PE1reasonForLeaving)
            dba.AddParameter("@pe2FromDate", ja.pe2FromDate)
            dba.AddParameter("@pe2ToDate", ja.pe2ToDate)
            dba.AddParameter("@PE2", ja.PE2)
            dba.AddParameter("@PE2Location", ja.PE2Location)
            dba.AddParameter("@PE2phone", ja.PE2phone)
            dba.AddParameter("@PE2salary", ja.PE2salary)
            dba.AddParameter("@PE2position", ja.PE2position)
            dba.AddParameter("@PE2reasonForLeaving", ja.PE2reasonForLeaving)
            dba.AddParameter("@pe3FromDate", ja.pe3FromDate)
            dba.AddParameter("@pe3ToDate", ja.pe3ToDate)
            dba.AddParameter("@PE3", ja.PE3)
            dba.AddParameter("@PE3Location", ja.PE3Location)
            dba.AddParameter("@PE3phone", ja.PE3phone)
            dba.AddParameter("@PE3salary", ja.PE3salary)
            dba.AddParameter("@PE3position", ja.PE3position)
            dba.AddParameter("@PE3reasonForLeaving", ja.PE3reasonForLeaving)
            dba.AddParameter("@pe4FromDate", ja.pe4FromDate)
            dba.AddParameter("@pe4ToDate", ja.pe4ToDate)
            dba.AddParameter("@PE4", ja.PE4)
            dba.AddParameter("@PE4Location", ja.PE4Location)
            dba.AddParameter("@PE4phone", ja.PE4phone)
            dba.AddParameter("@PE4salary", ja.PE4salary)
            dba.AddParameter("@PE4position", ja.PE4position)
            dba.AddParameter("@PE4reasonForLeaving", ja.PE4reasonForLeaving)
            dba.AddParameter("@Reference1", ja.Reference1)
            dba.AddParameter("@Reference1YrsKnown", ja.Reference1YrsKnown)
            dba.AddParameter("@Reference1Contact", ja.Reference1Contact)
            dba.AddParameter("@Reference2", ja.Reference2)
            dba.AddParameter("@Reference2YrsKnown", ja.Reference2YrsKnown)
            dba.AddParameter("@Reference2Contact", ja.Reference2Contact)
            dba.AddParameter("@Reference3", ja.Reference3)
            dba.AddParameter("@Reference3YrsKnown", ja.Reference3YrsKnown)
            dba.AddParameter("@Reference3Contact", ja.Reference3Contact)
            If btnSubmit.CommandName = "AddNew" Then
                dba.AddParameter("@Rating", ja.Rating)
                dba.AddParameter("@TimeStamp", ja.TimeStamp)
                dba.AddParameter("@ApplicantIP", ja.ApplicantIP)
            End If

            Dim dbaResponse As Integer = 0
            Try
                dbaResponse = dba.ExecuteNonQuery()
            Catch ex As Exception
                errString = ex.Message
            End Try

            If dbaResponse > 0 Then
                Select Case btnSubmit.CommandName
                    Case "AddNew"
                        Dim mailer As New rwMailer()
                        mailer.To = "Applications@Div-Log.com"
                        mailer.Subject = "New Online Application"
                        mailer.Body = "A new employment application has been submitted by: " & vbCrLf & ja.FirstName & " " & ja.LastName & " in " & ja.City & ", " & ja.State
                        mailer.Body &= vbCrLf & vbCrLf & "http://SEU.Div-Log.com"
                        rwMailer.SendMail(mailer)
                        '                       pnlApplication.Visible = False
                        Response.Redirect("EmploymentApplicationSAVED.aspx")
                        '                        pnlAppSaved.Visible = True
                    Case "Update"
                        Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
                        Dim note As String = "**** Application Edited **** <br />"
                        dba.CommandText = "INSERT INTO [NOTES] (AppID, Note, timeStamp, UserID) VALUES (@AppID, @Note, @timeStamp, @UserID) "
                        dba.AddParameter("@AppID", ja.EmploymentApplicationID)
                        dba.AddParameter("@Note", note)
                        dba.AddParameter("@timeStamp", Date.Now)
                        dba.AddParameter("@UserID", puser.ProviderUserKey)
                        dba.ExecuteNonQuery()
                        Dim actionString As String = String.Empty
                        actionString = "EditApplication"
                        RadAjaxManager1.ResponseScripts.Add("returnArg(""" & actionString & """);")
                        btnSubmit.Text = "Save Changes"

                End Select
            End If

        End If


    End Sub

    Protected Function validateForm() As String
        Dim retString As String = String.Empty
        If txtFirstName.Text.Trim() = "" Then
            retString = "<b>Personal Information:</b> First Name Required\n"
        End If
        If txtLastName.Text.Trim() = "" Then
            retString &= "<b>Personal Information:</b> Last Name Required\n"
        End If
        If txtStreetAddress.Text.Trim() = "" Then
            retString &= "<b>Personal Information:</b> Street Address Required\n"
        End If
        If txtZip.Text.Trim() = "" Then
            retString &= "<b>Personal Information:</b> Zip Code Required\n"
        End If
        If txtPrimaryPhone.Text.Trim() = String.Empty Then
            retString &= "<b>Personal Information:</b> Primary Phone Required\n"
        ElseIf txtPrimaryPhone.Text.Trim().Length < 10 Then
            retString &= "<b>Personal Information:</b> Primary Phone should contain 10 digits\n"
        End If
        If txtEmail.Text.Trim().Length > 0 Then
            If Not Utilities.isValidEmail(txtEmail.Text.Trim()) Then
                retString &= "<b>Personal Information:</b> eMail Address format should be: &nbsp;yourAccount@domain.tld\n"
            End If
        Else
            retString &= "<b>Personal Information:</b> eMail Address is REQUIRED\n"

        End If
        If cbPreferredLocation.Text = "Select Preferred Location" Or cbPreferredLocation.Text = "" Then
            retString &= "<b>Employment Desired:</b> Select Preferred Location.\n"
        End If
        If txtDesiredPosition.Text.Trim() = "" Then
            retString &= "<b>Employment Desired:</b> What position are you applying for?\n"
        End If
        If rbAppliedBeforeYes.Checked Then
            If txtAppliedBeforeLocation.Text.Trim() = "" Then
                retString &= "<b>Employment Desired:</b> Where did you previously apply with our company?\n"
            End If
        End If
        If cbEducationLevel.SelectedIndex = 0 Then
            retString &= "<b>Education/Training:</b> Select highest level of education\n"
        End If
        If txtSchool1.Text.Length > 2 Then
            If txtSchool1Location.Text.Length < 5 Then
                retString &= "<b>Education/Training:</b> " & txtSchool1.Text.Trim() & " - Enter a location (City, State)\n"
            End If
        End If

        If txtSchool2.Text.Length > 2 Then
            If txtSchool2Location.Text.Length < 5 Then
                retString &= "<b>Education/Training:</b> " & txtSchool2.Text.Trim() & " - Enter a location (City, State)\n"
            End If
        End If

        If txtSchool3.Text.Length > 2 Then
            If txtSchool3Location.Text.Length < 5 Then
                retString &= "<b>Education/Training:</b> " & txtSchool3.Text.Trim() & " - Enter a location (City, State)\n"
            End If
        End If
        If cbMilitaryBranch.SelectedIndex > 0 Then
            If cbMilitaryServiceFromDateMonth.Text = "" Or cbMilitaryServiceFromDateYear.Text = "" _
                Or cbMilitaryServiceToDateMonth.Text = "" Or cbMilitaryServiceToDateYear.Text = "" Then
                retString &= "<b>Military ServiceTextb> " & cbMilitaryBranch.SelectedItem.Text & ": Please provide dates of service\n"
            End If
            If cbMilitaryRank.SelectedIndex = 0 Then
                retString &= "<b>Military Service:</b> " & cbMilitaryBranch.SelectedItem.Text & ": Select your rank (pay-grade) when you got out\n"
            End If

        End If

        If txtPE1.Text.Length > 2 Then
            If cbPE1FromMonth.Text = "" Or cbPE1FromYear.Text = "" _
                Or cbPE1ToMonth.Text = "" Or cbPE1ToYear.Text = "" Then
                retString &= "<b>Previous Employment:</b> " & txtPE1.Text.Trim() & " - Provide dates of employment\n"
            Else
                Dim d1 As Date = cbPE1FromMonth.Text & " 1 " & cbPE1FromYear.Text
                Dim d2 As Date = cbPE1ToMonth.Text & " 1 " & cbPE1ToYear.Text
                If d2 < d1 Then
                    retString &= "<b>Previous Employment:</b> " & txtPE1.Text.Trim() & " - TO date must come <u>after</u> FROM date.\n"
                End If
            End If
            If txtPE1Location.Text.Length < 3 Then
                retString &= "<b>Previous Employment:</b> " & txtPE1.Text.Trim() & " -  Enter location (City, State)\n"
            End If
            If txtPE1phone.Text.Trim() = String.Empty Then
                retString &= "<b>Previous Employment:</b> " & txtPE1.Text.Trim() & " -  Please provide phone number\n"
            ElseIf txtPE1phone.Text.Trim().Length < 10 Then
                retString &= "<b>Previous Employment:</b> " & txtPE1.Text.Trim() & " -  Phone should contain 10 digits\n"
            End If
            If txtPE1reasonForLeaving.Text.Length < 3 Then
                retString &= "<b>Previous Employment:</b> " & txtPE1.Text.Trim() & " -  Enter reason for leaving or 'Current' if still there\n"
            End If
        End If

        If txtPE2.Text.Length > 2 Then
            If cbPE2FromMonth.Text = "" Or cbPE2FromYear.Text = "" _
                Or cbPE2ToMonth.Text = "" Or cbPE2ToYear.Text = "" Then
                retString &= "<b>Previous Employment:</b> " & txtPE2.Text.Trim() & " - Provide dates of employment\n"
            Else
                Dim d1 As Date = cbPE2FromMonth.Text & " 1 " & cbPE2FromYear.Text
                Dim d2 As Date = cbPE2ToMonth.Text & " 1 " & cbPE2ToYear.Text
                If d2 < d1 Then
                    retString &= "<b>Previous Employment:</b> " & txtPE2.Text.Trim() & " - TO date must come <u>after</u> FROM date.\n"
                End If

            End If
            If txtPE2Location.Text.Length < 3 Then
                retString &= "<b>Previous Employment:</b> " & txtPE2.Text.Trim() & " -  Enter location (City, State)\n"
            End If
            If txtPE2phone.Text.Trim() = String.Empty Then
                retString &= "<b>Previous Employment:</b> " & txtPE2.Text.Trim() & " -  Please provide phone number\n"
            ElseIf txtPE2phone.Text.Trim().Length < 10 Then
                retString &= "<b>Previous Employment:</b> " & txtPE2.Text.Trim() & " -  Phone should contain 10 digits\n"
            End If
            If txtPE2reasonForLeaving.Text.Length < 3 Then
                retString &= "<b>Previous Employment:</b> " & txtPE2.Text.Trim() & " -  Enter reason for leaving\n"
            End If
        End If

        If txtPE3.Text.Length > 2 Then
            If cbPE3FromMonth.Text = "" Or cbPE3FromYear.Text = "" _
                Or cbPE3ToMonth.Text = "" Or cbPE3ToYear.Text = "" Then
                retString &= "<b>Previous Employment:</b> " & txtPE3.Text.Trim() & " - Provide dates of employment\n"
            Else
                Dim d1 As Date = cbPE3FromMonth.Text & " 1 " & cbPE3FromYear.Text
                Dim d2 As Date = cbPE3ToMonth.Text & " 1 " & cbPE3ToYear.Text
                If d2 < d1 Then
                    retString &= "<b>Previous Employment:</b> " & txtPE3.Text.Trim() & " - TO date must come <u>after</u> FROM date.\n"
                End If
            End If
            If txtPE3Location.Text.Length < 3 Then
                retString &= "<b>Previous Employment:</b> " & txtPE3.Text.Trim() & " -  Enter location (City, State)\n"
            End If
            If txtPE3phone.Text.Trim() = String.Empty Then
                retString &= "<b>Previous Employment:</b> " & txtPE3.Text.Trim() & " -  Please provide phone number\n"
            ElseIf txtPE3phone.Text.Trim().Length < 10 Then
                retString &= "<b>Previous Employment:</b> " & txtPE3.Text.Trim() & " -  Phone should contain 10 digits\n"
            End If
            If txtPE3reasonForLeaving.Text.Length < 3 Then
                retString &= "<b>Previous Employment:</b> " & txtPE3.Text.Trim() & " -  Enter reason for leaving\n"
            End If
        End If

        If txtPE4.Text.Length > 2 Then
            If cbPE4FromMonth.Text = "" Or cbPE4FromYear.Text = "" _
                Or cbPE4ToMonth.Text = "" Or cbPE4ToYear.Text = "" Then
                retString &= "<b>Previous Employment:</b> " & txtPE4.Text.Trim() & " - Provide dates of employment\n"
            Else
                Dim d1 As Date = cbPE4FromMonth.Text & " 1 " & cbPE4FromYear.Text
                Dim d2 As Date = cbPE4ToMonth.Text & " 1 " & cbPE4ToYear.Text
                If d2 < d1 Then
                    retString &= "<b>Previous Employment:</b> " & txtPE4.Text.Trim() & " - TO date must come <u>after</u> FROM date.\n"
                End If
            End If
            If txtPE4Location.Text.Length < 3 Then
                retString &= "<b>Previous Employment:</b> " & txtPE4.Text.Trim() & " -  Enter location (City, State)\n"
            End If
            If txtPE4phone.Text.Trim() = String.Empty Then
                retString &= "<b>Previous Employment:</b> " & txtPE4.Text.Trim() & " -  Please provide phone number\n"
            ElseIf txtPE4phone.Text.Trim().Length < 10 Then
                retString &= "<b>Previous Employment:</b> " & txtPE4.Text.Trim() & " -  Phone should contain 10 digits\n"
            End If

            If txtPE4phone.Text.Length < 10 Then
            End If
            If txtPE4reasonForLeaving.Text.Length < 3 Then
                retString &= "<b>Previous Employment:</b> " & txtPE4.Text.Trim() & " -  Enter reason for leaving\n"
            End If
        End If



        Return retString
    End Function

    Function setComboYears(ByRef cb As RadComboBox, Optional ByVal yrs As Integer = 15) As RadComboBox
        cb.Items.Clear()
        Dim fitm As RadComboBoxItem = New RadComboBoxItem
        fitm.Value = ""
        fitm.Text = ""
        cb.Items.Add(fitm)
        Dim yr As Integer = Date.Now.Year
        Dim i As Integer
        For i = 0 To yrs
            Dim itm As RadComboBoxItem = New RadComboBoxItem
            itm.Value = yr - i.ToString()
            itm.Text = yr - i.ToString()
            cb.Items.Add(itm)
        Next
        Return cb
    End Function

    Function setComboMonths(ByRef cb As RadComboBox) As RadComboBox
        cb.Items.Clear()
        Dim itm As RadComboBoxItem = New RadComboBoxItem
        itm.Text = ""
        itm.Value = ""
        cb.Items.Add(itm)
        Dim itm1 As RadComboBoxItem = New RadComboBoxItem
        itm1.Text = "Jan"
        itm.Value = "1"
        cb.Items.Add(itm1)
        Dim itm2 As RadComboBoxItem = New RadComboBoxItem
        itm2.Text = "Feb"
        itm2.Value = "2"
        cb.Items.Add(itm2)
        Dim itm3 As RadComboBoxItem = New RadComboBoxItem
        itm3.Text = "Mar"
        itm.Value = "3"
        cb.Items.Add(itm3)
        Dim itm4 As RadComboBoxItem = New RadComboBoxItem
        itm4.Text = "Apr"
        itm.Value = "4"
        cb.Items.Add(itm4)
        Dim itm5 As RadComboBoxItem = New RadComboBoxItem
        itm5.Text = "May"
        itm5.Value = "5"
        cb.Items.Add(itm5)
        Dim itm6 As RadComboBoxItem = New RadComboBoxItem
        itm6.Text = "Jun"
        itm6.Value = "6"
        cb.Items.Add(itm6)
        Dim itm7 As RadComboBoxItem = New RadComboBoxItem
        itm7.Text = "Jul"
        itm7.Value = "7"
        cb.Items.Add(itm7)
        Dim itm8 As RadComboBoxItem = New RadComboBoxItem
        itm8.Text = "Aug"
        itm8.Value = "8"
        cb.Items.Add(itm8)
        Dim itm9 As RadComboBoxItem = New RadComboBoxItem
        itm9.Text = "Sep"
        itm9.Value = "9"
        cb.Items.Add(itm9)
        Dim itm10 As RadComboBoxItem = New RadComboBoxItem
        itm10.Text = "Oct"
        itm10.Value = "10"
        cb.Items.Add(itm10)
        Dim itm11 As RadComboBoxItem = New RadComboBoxItem
        itm11.Text = "Nov"
        itm11.Value = "11"
        cb.Items.Add(itm11)
        Dim itm12 As RadComboBoxItem = New RadComboBoxItem
        itm12.Text = "Dec"
        itm12.Value = "12"
        cb.Items.Add(itm12)
        Return cb
    End Function

    Private Sub RadAjaxManager1_AjaxRequest(sender As Object, e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        Dim arg As String = e.Argument
        Dim sarg() As String
        If arg.Contains("ZipCodeLookup") Then
            sarg = Split(arg, ":")
            Dim utl As New Utilities
            Dim zipinf() As String = utl.ZipCityState(sarg(1))
            If zipinf(0) <> "ZIP not found" Then
                txtCity.Text = zipinf(0)
                txtState.Text = zipinf(1)
            Else
                txtCity.Text = zipinf(0)
            End If
        End If
    End Sub

    Private Sub txtZip_TextChanged(sender As Object, e As EventArgs) Handles txtZip.TextChanged
        Dim utl As New Utilities
        If txtZip.Text.Length = 5 Then
            Dim zipinf() As String = utl.ZipCityState(txtZip.Text)
            If zipinf(0) <> "ZIP not found" Then
                txtCity.Text = zipinf(0)
                txtState.Text = zipinf(1)
            Else
                txtCity.Text = zipinf(0)
            End If
        End If
    End Sub


#Region "ComboBoxes"

    Private Sub populatepreferredLocation()
        cbPreferredLocation.Items.Clear()
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT companyABR + ':' + companyLocaCity + ', ' + companyLocaState as cLoca,companyLocationID from employmentApplicationLocations order by companyabr"
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        cbPreferredLocation.DataSource = dt
        cbPreferredLocation.DataValueField = "companyLocationID"
        cbPreferredLocation.DataTextField = "cLoca"
        cbPreferredLocation.DataBind()
        '                        cbPreferredLocation.SelectedIndex = -1
    End Sub
#End Region ' "ComboBoxes"


    Private Function getcompanyid(ByVal cabr As String) As DataTable
        Dim retdt As DataTable = Nothing
        Dim dba As New DBAccess
        dba.CommandText = "SELECT CompanyID, CompanyName FROM employmentApplicationCompanies WHERE companyABR = @companyABR"
        dba.AddParameter("@companyABR", cabr)
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        retdt = dt
        Return retdt
    End Function
End Class


