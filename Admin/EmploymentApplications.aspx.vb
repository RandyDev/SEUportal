Imports Telerik.Web.UI

Public Class EmploymentApplications
    Inherits System.Web.UI.Page

    Public bgc As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            ' populate Locations Combo Box (cbLocations on aspx page)
            Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
            Dim ldal As New locaDAL
            loadapplicationcompanies()
            '            cbLocations.Enabled = User.IsInRole("Administrator") Or User.IsInRole("SysOp") Or User.IsInRole("Manager")
            lblCopy.Text = "<br /><br /><br /><br /><br /><span style=""font-size:24px; font-weight:bold; color:#cfcfcf;""><center>Employment Application Manager</center></span>"

        End If


    End Sub

    Protected Sub loadapplicationcompanies()
        cbCompany.Items.Clear()
        Dim dba As New DBAccess
        dba.CommandText = "SELECT companyID, companyName from  employmentApplicationCompanies"
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        cbCompany.DataSource = dt
        cbCompany.DataValueField = "companyID"
        cbCompany.DataTextField = "companyName"
        cbCompany.DataBind()
        cbCompany.SelectedValue = -1

    End Sub

    Private Sub cbCompany_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cbCompany.SelectedIndexChanged
        pnlViewApp.Visible = False
        cbLocations.Items.Clear()
        If cbCompany.SelectedIndex > -1 Then
            Dim compid As String = cbCompany.SelectedValue
            Dim dba As New DBAccess()
            dba.CommandText = "SELECT companyLocaCity + ', ' + companyLocaState as cLoca,companyLocationID from employmentApplicationLocations WHERE companyID=@companyID order by companyLocaCity, companyLocaState"
            dba.AddParameter("companyID", compid)
            Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
            cbLocations.DataSource = dt
            cbLocations.DataValueField = "companyLocationID"
            cbLocations.DataTextField = "cLoca"
            cbLocations.DataBind()
            cbLocations.SelectedIndex = -1
        End If
    End Sub

    Protected Sub ReloadForms(ByVal ja As EmploymentApplicationObject)
        clearForm()
        lblCopy.Visible = False
        lbtnDelete.Visible = User.IsInRole("Administrator") Or User.IsInRole("SysOp")
        lbtnDelete.CommandArgument = ja.EmploymentApplicationID.ToString()
        lbtnReject.CommandArgument = ja.EmploymentApplicationID.ToString()
        lblCopy.Style.Item("display") = "none"
        lblID.Text = ja.EmploymentApplicationID.ToString
        lblLocationID.Text = ja.LocationID.ToString
        lblFirstName.Text = ja.FirstName
        lblMiddleInitial.Text = ja.MiddleInitial
        lblLastName.Text = ja.LastName
        lblStreetAddress.Text = ja.StreetAddress
        lblCity.Text = ja.City
        lblState.Text = ja.State
        lblZip.Text = ja.Zip
        lblPrimaryPhone.Text = ja.PrimaryPhone
        lblAltPhone.Text = ja.AltPhone
        lblEmail.Text = ja.Email
        lblReferredby.Text = ja.Referredby

        lblDesiredPosition.Text = ja.DesiredPosition
        If ja.DesiredPosition > "" Then
            lblDesiredStartDate.Text = ja.DesiredStartDate
        End If
        lblDesiredSalary.Text = ja.DesiredSalary
        lblCurrentlyEmployed.Text = IIf(ja.CurrentlyEmployed, "Yes", "No")
        If ja.CurrentlyEmployed Then
            lblAskCurrentEmployer.Text = IIf(ja.AskCurrentEmployer, "Yes", "Do Not Call")
        End If
        lblAppliedBefore.Text = IIf(ja.AppliedBefore, "Yes", "No")

        lblAppliedBeforeLocation.Text = IIf(ja.AppliedBefore, ja.AppliedBeforeLocation, "")
        If ja.AppliedBefore Then
            If ja.AppliedBeforeDate > "1/1/1900" Then
                Dim abd As Date = Left(ja.AppliedBeforeDate, 3) & "/1/" & Right(ja.AppliedBeforeDate, 4)
                Dim dif As Integer = DateDiff(DateInterval.Month, abd, Date.Now)
                lblAppliedBeforeDate.Text = ja.AppliedBeforeDate & " " & getDateDifString(dif)
            Else
                lblAppliedBeforeDate.Text = ""
            End If
        Else
            lblAppliedBeforeDate.Text = ""
        End If

        lblEducation.Text = ja.EducationLevel
        If ja.EducationLevel = "None selected" Then lblEducation.Text = "<span style=""font-weight:normal;"">None selected</span>"
        lblSchool1.Text = IIf(ja.School1.Length > 1, ja.School1, "<span style=""font-weight:normal;"">n/a</span>")
        If ja.School1 > "" Then
            lblSchool1Location.Text = IIf(ja.School1Location.Length > 1, ja.School1Location, "<span style=""font-weight:normal;"">n/a</span>")
            lblSchool1YearsAttended.Text = IIf(ja.School1YearsAttended.Length > 0, ja.School1YearsAttended, "")
            lblSchool1Graduated.Text = IIf(ja.School1Graduated, "Yes", "No")
            lblSchool1SubjectsStudied.Text = IIf(ja.School1SubjectsStudied.Length > 1, ja.School1SubjectsStudied, "")
        End If
        lblSchool2.Text = IIf(ja.School2.Length > 1, ja.School2, "<span style=""font-weight:normal;"">n/a</span>")
        If ja.School2 > "" Then
            lblSchool2Location.Text = IIf(ja.School2Location.Length > 1, ja.School2Location, "<span style=""font-weight:normal;"">n/a</span>")
            lblSchool2YearsAttended.Text = IIf(ja.School2YearsAttended.Length > 0, ja.School2YearsAttended, "")
            lblSchool2Graduated.Text = IIf(ja.School2Graduated, "Yes", "No")
            lblSchool2SubjectsStudied.Text = IIf(ja.School2SubjectsStudied.Length > 1, ja.School2SubjectsStudied, "")
        End If
        lblSchool3.Text = IIf(ja.School3.Length > 1, ja.School3, "<span style=""font-weight:normal;"">n/a</span>")
        If ja.School3 > "" Then
            lblSchool3Location.Text = IIf(ja.School3Location.Length > 1, ja.School3Location, "<span style=""font-weight:normal;"">n/a</span>")
            lblSchool3YearsAttended.Text = IIf(ja.School3YearsAttended.Length > 0, ja.School3YearsAttended, "")
            lblSchool3Graduated.Text = IIf(ja.School3Graduated, "Yes", "No")
            lblSchool3SubjectsStudied.Text = IIf(ja.School3SubjectsStudied.Length > 1, ja.School3SubjectsStudied, "")
        End If
        lblSpecialSkills.Text = ja.SpecialSkills
        lblMilitaryBranch.Text = IIf(ja.MilitaryBranch > "", ja.MilitaryBranch, "")
        lblMilitaryRank.Text = IIf(ja.MilitaryRank > "", ja.MilitaryRank, "")


        If ja.MilitaryServiceFromDate > "1/1/1900" Then
            Dim fd As String = MonthName(Month(ja.MilitaryServiceFromDate), True) & " " & Year(ja.MilitaryServiceFromDate).ToString()
            Dim td As String = MonthName(Month(ja.MilitaryServiceToDate), True) & " " & Year(ja.MilitaryServiceToDate).ToString()
            Dim ln As Integer = DateDiff(DateInterval.Month, ja.MilitaryServiceFromDate, ja.MilitaryServiceToDate)
            Dim msd As String = String.Empty
            lblMilitaryDatesOfService.Text = fd & " - " & td & " " & getDateDifString(ln)
        Else
            lblMilitaryDatesOfService.Text = ""
        End If

        If ja.pe1FromDate > "1/1/1900" And ja.pe1ToDate > "1/1/1900" Then
            Dim dif As Integer = DateDiff(DateInterval.Month, ja.pe1FromDate, ja.pe1ToDate)
            Dim fm As String = MonthName(Month(ja.pe1FromDate), True)
            Dim fy As String = Right(Year(ja.pe1FromDate).ToString, 2)
            Dim tm As String = MonthName(Month(ja.pe1ToDate), True)
            Dim ty As String = Right(Year(ja.pe1ToDate).ToString, 2)
            lblPE1Dates.Text = fm & " " & fy & " - " & tm & " " & ty & "<br />" & getDateDifString(dif)

        End If

        lblPE1.Text = IIf(ja.PE1 > "", ja.PE1, "<span style=""font-weight:normal;"">n/a</span>")
        If ja.PE1 > "" Then
            lblPE1Location.Text = IIf(ja.PE1Location > "", ja.PE1Location, "<span style=""font-weight:normal;"">n/a</span>")
            lblPE1Phone.Text = IIf(ja.PE1phone > "", ja.PE1phone, "<span style=""font-weight:normal;"">n/a</span>")
            lblPE1Salary.Text = IIf(ja.PE1salary > "", ja.PE1salary, "<span style=""font-weight:normal;"">n/a</span>")
            lblPE1position.Text = IIf(ja.PE1position > "", ja.PE1position, "<span style=""font-weight:normal;"">n/a</span>")
            lblPE1ReasonForLeaving.Text = IIf(ja.PE1reasonForLeaving > "", ja.PE1reasonForLeaving, "<span style=""font-weight:normal;"">n/a</span>")
        End If

        If ja.pe2ToDate > "1/1/1900" And ja.pe1FromDate > "1/1/1900" Then
            trPE12.Style.Item("line-height") = "13px"
            trPE12.Style.Item("background-color") = "#FFEFEF"
            lblPE12interim.Text = getDateDifString(DateDiff(DateInterval.Month, ja.pe2ToDate, ja.pe1FromDate))
            If DateDiff(DateInterval.Month, ja.pe2ToDate, ja.pe1FromDate) < 0 Then
                lblPE12interim.Text &= " <-- jobs overlap?"
            Else
                lblPE12interim.Text &= " <-- between jobs"
            End If
        End If

        If ja.pe2FromDate > "1/1/1900" And ja.pe2ToDate > "1/1/1900" Then
            Dim dif As Integer = DateDiff(DateInterval.Month, ja.pe2FromDate, ja.pe2ToDate)
            Dim fm As String = MonthName(Month(ja.pe2FromDate), True)
            Dim fy As String = Right(Year(ja.pe2FromDate).ToString, 2)
            Dim tm As String = MonthName(Month(ja.pe2ToDate), True)
            Dim ty As String = Right(Year(ja.pe2ToDate).ToString, 2)
            lblPE2Dates.Text = fm & " " & fy & " - " & tm & " " & ty & "<br />" & getDateDifString(dif)
        End If
        lblPE2.Text = IIf(ja.PE2 > "", ja.PE2, "<span style=""font-weight:normal;"">n/a</span>")
        If ja.PE2 > "" Then
            lblPE2Location.Text = IIf(ja.PE2Location > "", ja.PE2Location, "<span style=""font-weight:normal;"">n/a</span>")
            lblPE2Phone.Text = IIf(ja.PE2phone > "", ja.PE2phone, "<span style=""font-weight:normal;"">n/a</span>")
            lblPE2Salary.Text = IIf(ja.PE2salary > "", ja.PE2salary, "<span style=""font-weight:normal;"">n/a</span>")
            lblPE2position.Text = IIf(ja.PE2position > "", ja.PE2position, "<span style=""font-weight:normal;"">n/a</span>")
            lblPE2ReasonForLeaving.Text = IIf(ja.PE2reasonForLeaving > "", ja.PE2reasonForLeaving, "<span style=""font-weight:normal;"">n/a</span>")
        End If
        If ja.pe3ToDate > "1/1/1900" And ja.pe2FromDate > "1/1/1900" Then
            trPE23.Style.Item("line-height") = "13px"
            trPE23.Style.Item("background-color") = "#FFEFEF"
            lblPE23interim.Text = getDateDifString(DateDiff(DateInterval.Month, ja.pe3ToDate, ja.pe2FromDate))
            If DateDiff(DateInterval.Month, ja.pe3ToDate, ja.pe2FromDate) < 0 Then
                lblPE23interim.Text &= " <-- jobs overlap?"
            Else
                lblPE23interim.Text &= " <-- between jobs"
            End If
        End If
        If ja.pe3FromDate > "1/1/1900" And ja.pe3ToDate > "1/1/1900" Then
            Dim dif As Integer = DateDiff(DateInterval.Month, ja.pe3FromDate, ja.pe3ToDate)
            Dim fm As String = MonthName(Month(ja.pe3FromDate), True)
            Dim fy As String = Right(Year(ja.pe3FromDate).ToString, 2)
            Dim tm As String = MonthName(Month(ja.pe3ToDate), True)
            Dim ty As String = Right(Year(ja.pe3ToDate).ToString, 2)
            lblPE3Dates.Text = fm & " " & fy & " - " & tm & " " & ty & "<br />" & getDateDifString(dif)
        End If
        lblPE3.Text = IIf(ja.PE3 > "", ja.PE3, "<span style=""font-weight:normal;"">n/a</span>")
        If ja.PE3 > "" Then
            lblPE3Location.Text = IIf(ja.PE3Location > "", ja.PE3Location, "<span style=""font-weight:normal;"">n/a</span>")
            lblPE3Phone.Text = IIf(ja.PE3phone > "", ja.PE3phone, "<span style=""font-weight:normal;"">n/a</span>")
            lblPE3Salary.Text = IIf(ja.PE3salary > "", ja.PE3salary, "<span style=""font-weight:normal;"">n/a</span>")
            lblPE3position.Text = IIf(ja.PE3position > "", ja.PE3position, "<span style=""font-weight:normal;"">n/a</span>")
            lblPE3ReasonForLeaving.Text = IIf(ja.PE3reasonForLeaving > "", ja.PE3reasonForLeaving, "<span style=""font-weight:normal;"">n/a</span>")
        End If
        If ja.pe4ToDate > "1/1/1900" And ja.pe3FromDate > "1/1/1900" Then
            trPE34.Style.Item("line-height") = "13px"
            trPE34.Style.Item("background-color") = "#FFEFEF"
            lblPE34interim.Text = getDateDifString(DateDiff(DateInterval.Month, ja.pe4ToDate, ja.pe3FromDate))
            If DateDiff(DateInterval.Month, ja.pe4ToDate, ja.pe3FromDate) < 0 Then
                lblPE34interim.Text &= " <-- jobs overlap?"
            Else
                lblPE34interim.Text &= " <-- between jobs"
            End If
        End If
        If ja.pe4FromDate > "1/1/1900" And ja.pe4ToDate > "1/1/1900" Then
            Dim dif As Integer = DateDiff(DateInterval.Month, ja.pe4FromDate, ja.pe4ToDate)
            Dim fm As String = MonthName(Month(ja.pe4FromDate), True)
            Dim fy As String = Right(Year(ja.pe4FromDate).ToString, 2)
            Dim tm As String = MonthName(Month(ja.pe4ToDate), True)
            Dim ty As String = Right(Year(ja.pe4ToDate).ToString, 2)
            lblPE4Dates.Text = fm & " " & fy & " - " & tm & " " & ty & "<br />" & getDateDifString(dif)
        End If
        lblPE4.Text = IIf(ja.PE4 > "", ja.PE4, "<span style=""font-weight:normal;"">n/a</span>")
        If ja.PE4 > "" Then
            lblPE4Location.Text = IIf(ja.PE4Location > "", ja.PE4Location, "<span style=""font-weight:normal;"">n/a</span>")
            lblPE4Phone.Text = IIf(ja.PE4phone > "", ja.PE4phone, "<span style=""font-weight:normal;"">n/a</span>")
            lblPE4Salary.Text = IIf(ja.PE4salary > "", ja.PE4salary, "<span style=""font-weight:normal;"">n/a</span>")
            lblPE4position.Text = IIf(ja.PE4position > "", ja.PE4position, "<span style=""font-weight:normal;"">n/a</span>")
            lblPE4ReasonForLeaving.Text = IIf(ja.PE4reasonForLeaving > "", ja.PE4reasonForLeaving, "<span style=""font-weight:normal;"">n/a</span>")
        End If

        lblReference1.Text = IIf(ja.Reference1 > "", ja.Reference1, "<span style=""font-weight:normal;"">n/a</span>")
        lblReference1YrsKnown.Text = ja.Reference1YrsKnown
        lblReference1Contact.Text = ja.Reference1Contact
        lblReference2.Text = IIf(ja.Reference2 > "", ja.Reference2, "<span style=""font-weight:normal;"">n/a</span>")
        lblReference2YrsKnown.Text = ja.Reference2YrsKnown
        lbLReference2Contact.Text = ja.Reference2Contact
        lblReference3.Text = IIf(ja.Reference3 > "", ja.Reference3, "<span style=""font-weight:normal;"">n/a</span>")
        lblReference3YrsKnown.Text = ja.Reference3YrsKnown
        lblReference3Contact.Text = ja.Reference3Contact




        lblEditApp.Text = "<span style=""font-size:11px;color:blue"" onmouseover=""this.style.cursor='pointer'"" onclick=""openEditApp('" & ja.EmploymentApplicationID.ToString & "');"">Edit Application</span>"
        lblViewPrint.Text = "<span style=""font-size:11px;color:blue"" onmouseover=""this.style.cursor='pointer'"" onclick=""openViewApp('" & ja.EmploymentApplicationID.ToString & "');"">Generate PDF</span>"

        RadRating1.Value = ja.Rating
        lbtnHireMe.Visible = ja.Rating > 2
        lblTimeStamp.Text = "Submitted: &nbsp;" & Format(ja.TimeStamp, "dd MMM yy - h:mm tt")
        lnkbtnSave.CommandArgument = ja.EmploymentApplicationID.ToString
        lnkbtnSave.Enabled = True
        getNotes(ja.EmploymentApplicationID.ToString)
    End Sub

    Sub clearForm()
        lblID.Text = ""
        lblLocationID.Text = ""
        lbtnDelete.CommandArgument = Nothing
        lbtnDelete.Visible = False

        lblFirstName.Text = ""
        lblMiddleInitial.Text = ""
        lblLastName.Text = ""
        lblStreetAddress.Text = ""
        lblCity.Text = ""
        lblState.Text = ""
        lblZip.Text = ""
        lblPrimaryPhone.Text = ""
        lblAltPhone.Text = ""
        lblEmail.Text = ""
        lblReferredby.Text = ""

        lblDesiredPosition.Text = ""
        lblDesiredStartDate.Text = ""
        lblDesiredSalary.Text = ""
        lblCurrentlyEmployed.Text = ""
        lblAskCurrentEmployer.Text = ""
        lblAppliedBefore.Text = ""
        lblAppliedBeforeLocation.Text = ""
        lblAppliedBeforeDate.Text = ""

        lblEducation.Text = ""
        lblSchool1.Text = ""
        lblSchool1Location.Text = ""
        lblSchool1YearsAttended.Text = ""
        lblSchool1Graduated.Text = ""
        lblSchool1SubjectsStudied.Text = ""
        lblSchool2.Text = ""
        lblSchool2Location.Text = ""
        lblSchool2YearsAttended.Text = ""
        lblSchool2Graduated.Text = ""
        lblSchool2SubjectsStudied.Text = ""
        lblSchool3.Text = ""
        lblSchool3Location.Text = ""
        lblSchool3YearsAttended.Text = ""
        lblSchool3Graduated.Text = ""
        lblSchool3SubjectsStudied.Text = ""

        lblSpecialSkills.Text = ""
        lblMilitaryBranch.Text = ""
        lblMilitaryRank.Text = ""
        lblMilitaryDatesOfService.Text = ""

        lblPE1Dates.Text = ""
        lblPE1.Text = ""
        lblPE1Location.Text = ""
        lblPE1Phone.Text = ""
        lblPE1Salary.Text = ""
        lblPE1position.Text = ""
        lblPE1ReasonForLeaving.Text = ""

        lblPE12interim.Text = ""
        trPE12.Style.Item("line-height") = "1px"
        lblPE2Dates.Text = ""
        lblPE2.Text = ""
        lblPE2Location.Text = ""
        lblPE2Phone.Text = ""
        lblPE2Salary.Text = ""
        lblPE2position.Text = ""
        lblPE2ReasonForLeaving.Text = ""

        lblPE23interim.Text = ""
        trPE23.Style.Item("line-height") = "1px"
        lblPE3Dates.Text = ""
        lblPE3.Text = ""
        lblPE3Location.Text = ""
        lblPE3Phone.Text = ""
        lblPE3Salary.Text = ""
        lblPE3position.Text = ""
        lblPE3ReasonForLeaving.Text = ""

        lblPE34interim.Text = ""
        trPE34.Style.Item("line-height") = "1px"
        lblPE4Dates.Text = ""
        lblPE4.Text = ""
        lblPE4Location.Text = ""
        lblPE4Phone.Text = ""
        lblPE4Salary.Text = ""
        lblPE4position.Text = ""
        lblPE4ReasonForLeaving.Text = ""

        lblReference1.Text = ""
        lblReference1YrsKnown.Text = ""
        lblReference1Contact.Text = ""
        lblReference2.Text = ""
        lblReference2YrsKnown.Text = ""
        lbLReference2Contact.Text = ""
        lblReference3.Text = ""
        lblReference3YrsKnown.Text = ""
        lblReference3Contact.Text = ""


        RadRating1.Value = 0
        lbtnHireMe.Visible = False
        lblTimeStamp.Text = ""
        lnkbtnSave.Enabled = False
        txtNotes.Text = ""
        lblPrevNotes.Text = ""
        '        lblCopy.Style.Item("display") = "block"
    End Sub

    Private Function cypherFromToDates(ByVal fDate As String, ByVal tDate As String) As String
        If fDate.Length = 8 And tDate.Length = 8 Then
            Dim fromDate As Date = Left(fDate, 3) & "/1/" & Right(fDate, 4)
            Dim toDate As Date = Left(tDate, 3) & "/1/" & Right(tDate, 4)


        Else

        End If
        '        If IsDate(fDate) Then
        'If IsDate(
        ' And IsDate(tDate) Then
        '                Dim fd As String = MonthName(Month(fDate), True) & "/" & Year(fDate).ToString()
        '                Dim td As String = MonthName(Month(tDate), True) & "/" & Year(tDate).ToString()
        '                Dim ln As Integer = DateDiff(DateInterval.Month, fDate, tDate)
        '                Dim msd As String = String.Empty
        '                If ln > 12 Then
        '                    Dim y As Integer = 0
        '                    Dim m As Integer = 0
        '                    y = ln / 12
        '                    m = ln Mod 12
        '                    lblMilitaryDatesOfService.Text = fd & " - " & td & " (" & y.ToString & " yr " & m.ToString & " mth)"
        '                Else
        '                    lblMilitaryDatesOfService.Text = fd & " - " & td & " (" & ln.ToString & " months)"
        '                End If
        '            End If
        Return " "
    End Function

    Private Function getDateDifString(ByVal ln As Integer) As String
        Dim retStr As String = String.Empty
        If ln > 12 Then
            Dim y As Integer = 0
            Dim m As Integer = 0
            m = ln Mod 12
            y = (ln - m) / 12
            retStr = "<span style=""font-weight:normal;"">( " & y.ToString & " yr " & m.ToString & " mo )</span>"
        Else
            retStr = "<span style=""font-weight:normal;"">( " & ln.ToString & " mo )</span>"
        End If
        Return retStr
    End Function

    Private Sub RadGrid1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If e.CommandName = "RowClick" Then
            Dim selval As String = RadGrid1.SelectedValue.ToString
            Dim appid As Guid = New Guid(selval)
            setSelectedAppID(appid)
            Dim ja As New EmploymentApplicationObject
            Dim edal As New empDAL
            ja = edal.getJobApp(appid.ToString)
            pnlViewApp.Visible = True
            ReloadForms(ja)
        End If
    End Sub


    Protected Sub RadGrid1_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles RadGrid1.PreRender
        If Not (Session("selectedItems") Is Nothing) Then
            Dim selectedItems As ArrayList = CType(Session("selectedItems"), ArrayList)
            Dim stackIndex As Int16
            For stackIndex = 0 To selectedItems.Count - 1
                Dim curItem As String = selectedItems(stackIndex).ToString
                For Each item As GridItem In RadGrid1.MasterTableView.Items
                    If TypeOf item Is GridDataItem Then
                        Dim dataItem As GridDataItem = CType(item, GridDataItem)
                        If curItem.Equals(dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("EmploymentApplicationID").ToString()) Then
                            dataItem.Selected = True
                        End If
                    End If
                Next
            Next
        End If
        'Dim headerItem As GridHeaderItem = TryCast(RadGrid1.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
        'Dim img As Image = New Image()
        'img.ImageUrl = "~/images/camera.jpg"
        'headerItem.BackColor = Drawing.Color.Yellow
        'headerItem("PicMe").Controls.AddAt(1, img)

    End Sub
    Private Sub setSelectedAppID(ByVal appid As Guid)
        Dim selectedItems As ArrayList
        '       If Session("selectedItems") Is Nothing Then
        selectedItems = New ArrayList
        '        Else
        '        selectedItems = CType(Session("selectedItems"), ArrayList)
        '        End If
        selectedItems.Add(appid)
        Session("selectedItems") = selectedItems
    End Sub

    Private Sub RadGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        Dim locaid As String = String.Empty
        Dim sql As String = String.Empty
        Dim filterStr As String = String.Empty
        Dim strFilter = txtNameFilter.Text.Trim()
        If strFilter.Length > 2 Then
            sql = "SELECT EmploymentApplicationID, FirstName + ' ' + LastName as Name, City, State, Rating, TimeStamp FROM EmploymentApplication "
            Dim txtNameFilter As String = " WHERE " & String.Format("((FirstName LIKE '%{0}%') OR (LastName LIKE '%{0}%')) ", strFilter)
            sql &= txtNameFilter
            dba.CommandText = sql
            dt = dba.ExecuteDataSet.Tables(0)
            RadGrid1.DataSource = dt
            Exit Sub
        End If
        If cbLocations.SelectedIndex > -1 Then
            locaid = cbLocations.SelectedValue
            sql = "SELECT EmploymentApplicationID, FirstName + ' ' + LastName as Name, City, State, Rating, TimeStamp FROM EmploymentApplication "
        Else
            sql = "SELECT EmploymentApplicationID, FirstName + ' ' + LastName as Name, City, State, Rating, TimeStamp FROM EmploymentApplication " & _
                "where LocationID='00000000-0000-0000-0000-000000000000' " & _
                "or LocationID='DA301631-CBA0-4CFC-8A83-708A75ABC7A6' " & _
                "or LocationID='5B734A1E-E9F1-4E9B-96C9-3617447B0946' " & _
                "or LocationID='88A87297-0BBE-4643-A82F-BC31E87BE187' order by timestamp desc"
            dba.CommandText = sql
            dt = dba.ExecuteDataSet.Tables(0)
            RadGrid1.DataSource = dt
            Exit Sub
        End If

        ' which list do you want?  0 = app pool   1 = reject list
        Dim strAppPool As String = String.Empty

        Select Case cbAppPool.SelectedItem.Text
            Case "Application Pool =< 6 mos"
                If locaid > "" Then
                    strAppPool = "WHERE Rating >= 0 and timestamp >= DATEADD(month, -6, GETDATE()) and LocationID=@locaid ORDER BY timestamp desc  "
                Else
                    strAppPool = "WHERE Rating >= 0 and timestamp >= DATEADD(month, -6, GETDATE()) ORDER BY timestamp desc  "

                End If

            Case "Archived Applications > 6 mos"
                If locaid > "" Then
                    strAppPool = "WHERE Rating >= 0 and timestamp <= DATEADD (month,-6, GETDATE()) and LocationID=@locaid ORDER BY timestamp desc    "
                Else
                    strAppPool = "WHERE Rating >= 0 and timestamp <= DATEADD (month,-6, GETDATE()) ORDER BY timestamp desc    "
                End If


            Case "Rejected Applications"
                If locaid > "" Then
                    strAppPool = "WHERE Rating < 0 and LocationID='locaid' ORDER BY timestamp desc   "
                Else
                    strAppPool = "WHERE Rating < 0 ORDER BY timestamp desc   "
                End If

        End Select

        'add filter to sql string
        sql &= strAppPool
        Dim strSort As String = String.Empty
        sql &= strSort
        dba.CommandText = sql
        If locaid > "" Then
            dba.AddParameter("@locaid", locaid)
        End If
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception
            Dim exmsg As String = ex.Message
        End Try
        RadGrid1.DataSource = dt
    End Sub

    Private Sub btnShowRecords_Click(sender As Object, e As System.EventArgs) Handles btnShowRecords.Click
        RadGrid1.Rebind()
        '        clearForm()
        '        pnlViewApp.Visible = False
        '        lblCopy.Style.Item("display") = "block"
    End Sub

    'Protected Sub lnkbtnClearRating_Click(sender As Object, e As EventArgs) Handles lnkbtnClearRating.Click
    '    RadRating1.Value = 0
    'End Sub

    Private Sub lnkbtnSave_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles lnkbtnSave.Command
        Dim appID As String = e.CommandArgument
        Dim dba As New DBAccess("rtds")
        Dim curRating As Integer = 0
        Dim radRating As Integer = RadRating1.Value
        dba.CommandText = "SELECT Rating FROM EmploymentApplication WHERE EmploymentApplicationID=@id"
        dba.AddParameter("@id", appID)
        curRating = dba.ExecuteScalar
        Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
        If RadRating1.Value <> curRating Then
            Dim note As String = "Rating changed from " & curRating & " to " & radRating
            dba.CommandText = "INSERT INTO [NOTES] (AppID, Note, timeStamp, UserID) VALUES (@AppID, @Note, @timeStamp, @UserID) "
            dba.AddParameter("@AppID", appID)
            dba.AddParameter("@Note", note)
            dba.AddParameter("@timeStamp", Date.Now)
            dba.AddParameter("@UserID", puser.ProviderUserKey)
            dba.ExecuteNonQuery()
            dba.CommandText = "UPDATE EmploymentApplication SET Rating=@rating WHERE EmploymentApplicationID=@id"
            dba.AddParameter("@rating", radRating)
            dba.AddParameter("@id", appID)
            dba.ExecuteNonQuery()
            RadGrid1.Rebind()
            If curRating = 0 Then
                '                clearForm()
                '                pnlViewApp.Visible = False
            End If
        End If
        If txtNotes.Text.Trim().Length > 5 Then
            dba.CommandText = "INSERT INTO [NOTES] (AppID, Note, timeStamp, UserID) VALUES (@AppID, @Note, @timeStamp, @UserID) "
            dba.AddParameter("@AppID", appID)
            dba.AddParameter("@Note", txtNotes.Text)
            dba.AddParameter("@timeStamp", Date.Now)
            dba.AddParameter("@UserID", puser.ProviderUserKey)
            dba.ExecuteNonQuery()
            txtNotes.Text = ""
        End If
        getNotes(appID)
    End Sub

    Private Sub getNotes(ByVal appID As String)
        lblPrevNotes.Text = ""
        Dim notes As String = String.Empty
        Dim dba As New DBAccess("rtds")
        dba.CommandText = "SELECT Note, TimeStamp, UserID FROM Notes WHERE AppID=@appID ORDER BY TimeStamp DESC"
        dba.AddParameter("@appID", appID)
        Dim dt As New DataTable
        dt = dba.ExecuteDataSet.Tables(0)
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                notes &= "<div style=""line-height:14px;""><font size=""1"">" & rw.Item("TimeStamp") & " - " & getUsersName(rw.Item("UserID").ToString) & "</font><br /><span style=""font-size:12px;"">"
                notes &= rw.Item("Note") & "</span></div><hr />"
            Next

        End If

        lblPrevNotes.Text = notes

    End Sub

    Public Function getUsersName(ByVal userID As String) As String
        Dim name As String = String.Empty
        Dim dba As New DBAccess("rtds")
        dba.CommandText = "SELECT firstName + ' ' + lastName AS NAME FROM UserProfile WHERE userID = @userID"
        dba.AddParameter("@userID", userID)
        name = dba.ExecuteScalar
        Return name
    End Function

    Private Sub cbLocations_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbLocations.SelectedIndexChanged
        pnlViewApp.Visible = False
        RadGrid1.Rebind()
        '        clearForm()
        '        pnlViewApp.Visible = False
    End Sub

    Private Sub lbtnDelete_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles lbtnDelete.Command
        Dim strAppID As String = e.CommandArgument
        If Utilities.IsValidGuid(strAppID) Then
            Dim dba As New DBAccess
            dba.CommandText = "DELETE FROM EmploymentApplication WHERE EmploymentApplicationID=@ID"
            dba.AddParameter("@ID", strAppID)
            Try
                dba.ExecuteNonQuery()
                RadGrid1.Rebind()
                clearForm()
                '                pnlViewApp.Visible = False
                '                lblCopy.Style.Item("display") = "block"
                Session("selectedItems") = Nothing
            Catch ex As Exception
            End Try
            pnlViewApp.Visible = False
            lblCopy.Visible = True

        End If
    End Sub

    Private Sub cbAppPool_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbAppPool.SelectedIndexChanged
        RadGrid1.Rebind()
        '        clearForm()
        '        pnlViewApp.Visible = False
    End Sub

    Private Sub lbtnReject_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles lbtnReject.Command
        Dim appID As String = e.CommandArgument


        If Utilities.IsValidGuid(appID) Then
            Dim varReasonText As String = txtNotes.Text
            If varReasonText.Length < 15 Then
                lblRejectError.Text = "Provide reason for rejection in textbox above<br />(minimum 15 characters) and try again"
            Else
                lblRejectError.Text = String.Empty
                Dim dba As New DBAccess()
                Dim puser As MembershipUser = Membership.GetUser(User.Identity.Name)
                Dim curRating As Integer = 0
                Dim radRating As Integer = RadRating1.Value

                dba.CommandText = "SELECT Rating FROM EmploymentApplication WHERE EmploymentApplicationID=@id"
                dba.AddParameter("@id", appID)
                curRating = dba.ExecuteScalar
                Dim note As String = "**** REJECTED **** <br />" & varReasonText
                dba.CommandText = "INSERT INTO [NOTES] (AppID, Note, timeStamp, UserID) VALUES (@AppID, @Note, @timeStamp, @UserID) "
                dba.AddParameter("@AppID", appID)
                dba.AddParameter("@Note", note)
                dba.AddParameter("@timeStamp", Date.Now)
                dba.AddParameter("@UserID", puser.ProviderUserKey)
                dba.ExecuteNonQuery()
                dba.CommandText = "UPDATE EmploymentApplication SET Rating=@rating WHERE EmploymentApplicationID=@id"
                dba.AddParameter("@rating", -1)
                dba.AddParameter("@id", appID)

                Try
                    dba.ExecuteNonQuery()
                    RadGrid1.Rebind()
                    txtNotes.Text = ""
                    RadRating1.Value = 0
                    getNotes(appID)
                    '                    clearForm()
                    '                    pnlViewApp.Visible = False
                    '                    lblCopy.Style.Item("display") = "block"
                Catch ex As Exception

                End Try

            End If
        End If

    End Sub


    Private Sub RadAjaxManager1_AjaxRequest(sender As Object, e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        Dim arg As String = e.Argument
        If arg = "EditApplication" Then
            Dim ja As New EmploymentApplicationObject
            Dim edal As New empDAL
            ja = edal.getJobApp(lbtnDelete.CommandArgument.ToString)
            RadGrid1.Rebind()
            ReloadForms(ja)
        End If

    End Sub


    Private Sub lbtnClear_Click(sender As Object, e As EventArgs) Handles lbtnClear.Click
        cbCompany.ClearCheckedItems()
        cbCompany.ClearSelection()
        cbLocations.ClearCheckedItems()
        cbLocations.ClearSelection()
        txtNameFilter.Text = String.Empty
    End Sub

End Class