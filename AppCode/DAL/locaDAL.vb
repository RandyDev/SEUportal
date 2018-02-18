Imports System.Data
Imports System.Data.SqlClient
'Imports DiversifiedLogistics.Location
'Imports DiversifiedLogistics.ParentCompany
Imports Telerik.Web.UI

Public Class locaDAL
    Dim utl As New Utilities
    Public Function getLoadTypeIDByName(ByVal ltname As String) As Guid
        Dim retguid As Guid = Utilities.zeroGuid
        Dim dba As New DBAccess
        dba.CommandText = "SELECT ID FROM Department WHERE Name LIKE '@Name'"
        dba.AddParameter("@Name", ltname)
        Dim ret As Guid = dba.ExecuteScalar
        If Not ret = Nothing Then
            retguid = ret
        End If
        Return retguid
    End Function
    Public Function getVendorNumberByLocaIDAndName(ByVal locaid As String, ByVal vname As String) As String
        Dim retstr As String = String.Empty
        Dim dba As New DBAccess
        dba.CommandText = "SELECT Vendor.Number " & _
            "FROM Location INNER JOIN " & _
            "ParentCompany ON Location.ParentCompanyID = ParentCompany.ID INNER JOIN " & _
            "Vendor ON Location.ParentCompanyID = Vendor.ParentCompanyID " & _
            "WHERE Location.ID = @locaID AND Vendor.Name LIKE '%' + @vendorName + '%' "
        dba.AddParameter("@locaID", locaid)
        dba.AddParameter("@vendorName", vname)
        Try
            retstr = dba.ExecuteScalar
        Catch ex As Exception
            'ooophf
            Dim strerr As String = ex.Message
        End Try
        Return retstr
    End Function
    Public Function getVendorNameByLocaIDAndNumber(ByVal locaid As String, ByVal vnumber As String) As String
        Dim retstr As String = String.Empty
        Dim dba As New DBAccess
        dba.CommandText = "SELECT Vendor.Name " & _
            "FROM Location INNER JOIN " & _
            "ParentCompany ON Location.ParentCompanyID = ParentCompany.ID INNER JOIN " & _
            "Vendor ON Location.ParentCompanyID = Vendor.ParentCompanyID " & _
            "WHERE Location.ID = @locaID AND Vendor.Number = @vendorNumber"
        dba.AddParameter("@locaID", locaid)
        dba.AddParameter("@vendorNumber", vnumber)
        Try
            retstr = dba.ExecuteScalar
        Catch ex As Exception
            'ooophf
            Dim strerr As String = ex.Message
        End Try
        Return retstr
    End Function

    Public Function getDepartmenIDByName(ByVal dname As String) As Guid
        Dim retguid As Guid = Utilities.zeroGuid
        Dim dba As New DBAccess
        dba.CommandText = "SELECT ID FROM Department WHERE Name LIKE @Name"
        dba.AddParameter("@Name", dname)
        Dim ret As Guid = dba.ExecuteScalar
        If Not ret = Nothing Then
            retguid = ret
        End If
        Return retguid
    End Function

    Public Function getCBILocations() As DataTable
        Dim dt As New DataTable()
        Dim dba As New DBAccess()
        Dim sql As String = String.Empty
        sql = "SELECT Location.ID AS locaID, Location.Name AS LocationName, Location.ParentCompanyID, " &
            "ParentCompany.Name AS ParentCompanyName " &
            "FROM Location INNER JOIN " &
            "ParentCompany ON Location.ParentCompanyID = ParentCompany.ID "
        sql &= "WHERE (Location.Name LIKE '%CBI%') AND Location.inActive = 0 "
        sql &= "ORDER BY Location.Name"

        dba.CommandText = sql
        dt = dba.ExecuteDataSet.Tables(0)
        Return dt   'locaID, LocationName, ParentCompanyID, ParentCompanyName
        '             & _
    End Function
    Public Function getLocations(Optional ByVal withFern As Boolean = False) As DataTable
        Dim dt As New DataTable()
        Dim dba As New DBAccess()
        Dim sql As String = String.Empty
        sql = "SELECT Location.ID AS locaID, Location.Name AS LocationName, Location.ParentCompanyID, " &
            "ParentCompany.Name AS ParentCompanyName " &
            "FROM Location INNER JOIN " &
            "ParentCompany ON Location.ParentCompanyID = ParentCompany.ID "
        If withFern Then
            sql &= "WHERE (Location.inActive = 0) "
        Else
            sql &= "WHERE (Location.Name NOT LIKE 'Fernandina%') AND Location.inActive = 0 "
        End If
        sql &= "ORDER BY Location.Name"

        dba.CommandText = sql
        dt = dba.ExecuteDataSet.Tables(0)
        Return dt   'locaID, LocationName, ParentCompanyID, ParentCompanyName
        '             & _
    End Function

    Public Function getAllLocations() As DataTable
        Dim dt As New DataTable()
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT Location.ID AS locaID, Location.Name AS LocationName, Location.ParentCompanyID, ParentCompany.Name AS ParentCompanyName " & _
            "FROM Location LEFT OUTER JOIN " & _
            "ParentCompany ON Location.ParentCompanyID = ParentCompany.ID " & _
            "WHERE (Location.Name NOT LIKE 'Fernandina%') " & _
            "ORDER BY Location.InActive, LocationName "
        dt = dba.ExecuteDataSet.Tables(0)
        Return dt   'locaID, LocationName, ParentCompanyID, ParentCompanyName
    End Function

    Public Function getCBIUserLocaList(ByVal userID As Guid) As DataTable
        Dim dt As New DataTable()
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT UserLocations.locationID as locaID, Location.Name as LocationName " &
            "FROM UserLocations INNER JOIN " &
            "Location ON UserLocations.locationID = Location.ID " &
            "WHERE (UserLocations.userID = @userID) AND (Location.Name LIKE 'CBI%')"
        dba.AddParameter("@userID", userID)
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception
        End Try
        Return dt       'locaID, LocationName
    End Function
    Public Function getUserLocaList(ByVal userID As Guid) As DataTable
        Dim dt As New DataTable()
        Dim dba As New DBAccess()
        dba.CommandText = "Select UserLocations.locationID As locaID, Location.Name As LocationName " &
            "FROM UserLocations INNER JOIN " &
            "Location On UserLocations.locationID = Location.ID " & _
            "WHERE (UserLocations.userID = @userID)"
        dba.AddParameter("@userID", userID)
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception
        End Try
        Return dt       'locaID, LocationName
    End Function

    Public Function getLocationByName(ByVal str As String) As Location
        Dim loca As New Location
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        dba.CommandText = "Select Location.ParentCompanyID, Location.Name, Location.ID, Location.BeginDayOffset, " & _
            "Location.TimezoneOffset, Location.InActive,Location.hhPrintTimeStamp,Location.CheckCharge, Location.Dividend, Location.locazip, Location.locaCity, Location.locaState, Location.loginPrefix, " & _
            "ParentCompany.Name As ParentCompanyName " & _
            "FROM Location INNER JOIN " & _
            "ParentCompany On Location.ParentCompanyID = ParentCompany.ID " & _
            "WHERE Location.Name = @locaName"
        dba.AddParameter("@locaName", str)
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception
        End Try
        If dt.Rows.Count > 0 Then
            Dim rw As DataRow = dt.Rows(0)
            loca.ID = IIf(IsDBNull(rw.Item("ID")), Nothing, rw.Item("ID"))
            loca.ParentCompanyID = IIf(IsDBNull(rw.Item("ParentCompanyID")), Nothing, rw.Item("ParentCompanyID"))
            loca.Name = IIf(IsDBNull(rw.Item("Name")), Nothing, rw.Item("Name"))
            loca.BeginDayOffset = IIf(IsDBNull(rw.Item("BeginDayOffset")), Nothing, rw.Item("BeginDayOffset"))
            loca.TimezoneOffset = IIf(IsDBNull(rw.Item("TimezoneOffset")), Nothing, rw.Item("TimezoneOffset"))
            loca.InActive = IIf(IsDBNull(rw.Item("InActive")), Nothing, rw.Item("InActive"))
            loca.hhPrintTimeStamp = IIf(IsDBNull(rw.Item("hhPrintTimeStamp")), Nothing, rw.Item("hhPrintTimeStamp"))
            loca.ParentCompanyName = IIf(IsDBNull(rw.Item("ParentCompanyName")), Nothing, rw.Item("ParentCompanyName"))
            loca.CheckCharge = IIf(IsDBNull(rw.Item("CheckCharge")), 0, rw.Item("CheckCharge"))
            loca.Dividend = IIf(IsDBNull(rw.Item("Dividend")), 0, rw.Item("Dividend"))
            loca.locaCity = IIf(IsDBNull(rw.Item("locaCity")), Nothing, rw.Item("locaCity"))
            loca.locaState = IIf(IsDBNull(rw.Item("locaState")), Nothing, rw.Item("locaState"))
            loca.locazip = IIf(IsDBNull(rw.Item("locazip")), Nothing, rw.Item("locazip"))
            loca.loginPrefix = IIf(IsDBNull(rw.Item("loginPrefix")), Nothing, rw.Item("loginPrefix"))
        End If
        Return loca
    End Function
    Public Function getLocationByID(ByVal locaID As Guid) As Location
        Dim dt As New DataTable
        Dim loca As New Location
        Dim dba As New DBAccess()
        dba.CommandText = "Select Location.ParentCompanyID, Location.Name, Location.ID, Location.BeginDayOffset, " & _
            "Location.TimezoneOffset, Location.InActive,Location.hhPrintTimeStamp, Location.CheckCharge, Location.Dividend, Location.locazip, Location.locaCity, Location.locaState, Location.loginPrefix, Location.EnableBenefits,Location.AdministrativeFee, Location.CustomerFee, " & _
            "ParentCompany.Name As ParentCompanyName " & _
            "FROM Location INNER JOIN " & _
            "ParentCompany On Location.ParentCompanyID = ParentCompany.ID " & _
            "WHERE Location.ID = @locaID"
        dba.AddParameter("@locaID", locaID)
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception

        End Try
        If dt.Rows.Count > 0 Then
            Dim rw As DataRow = dt.Rows(0)
            loca.ID = locaID
            loca.ParentCompanyID = IIf(IsDBNull(rw.Item("ParentCompanyID")), Nothing, rw.Item("ParentCompanyID"))
            loca.Name = IIf(IsDBNull(rw.Item("Name")), Nothing, rw.Item("Name"))
            loca.BeginDayOffset = IIf(IsDBNull(rw.Item("BeginDayOffset")), Nothing, rw.Item("BeginDayOffset"))
            loca.TimezoneOffset = IIf(IsDBNull(rw.Item("TimezoneOffset")), Nothing, rw.Item("TimezoneOffset"))
            loca.InActive = IIf(IsDBNull(rw.Item("InActive")), Nothing, rw.Item("InActive"))
            loca.hhPrintTimeStamp = IIf(IsDBNull(rw.Item("hhPrintTimeStamp")), Nothing, rw.Item("hhPrintTimeStamp"))
            loca.ParentCompanyName = IIf(IsDBNull(rw.Item("ParentCompanyName")), Nothing, rw.Item("ParentCompanyName"))
            loca.CheckCharge = IIf(IsDBNull(rw.Item("CheckCharge")), 0, rw.Item("CheckCharge"))
            loca.Dividend = IIf(IsDBNull(rw.Item("Dividend")), 0, rw.Item("Dividend"))
            loca.locaCity = IIf(IsDBNull(rw.Item("locaCity")), Nothing, rw.Item("locaCity"))
            loca.locaState = IIf(IsDBNull(rw.Item("locaState")), Nothing, rw.Item("locaState"))
            loca.locazip = IIf(IsDBNull(rw.Item("locazip")), Nothing, rw.Item("locazip"))
            loca.loginPrefix = IIf(IsDBNull(rw.Item("loginPrefix")), Nothing, rw.Item("loginPrefix"))
            If IsDBNull(rw.Item("EnableBenefits")) Then
                loca.EnableSickLeave = False
            Else
                loca.EnableSickLeave = rw.Item("EnableBenefits")
            End If
            loca.AdministrativeFee = IIf(IsDBNull(rw.Item("AdministrativeFee")), 0, rw.Item("AdministrativeFee"))
            loca.CustomerFee = IIf(IsDBNull(rw.Item("CustomerFee")), 0, rw.Item("CustomerFee"))
        End If
        Return loca   'locaID, LocationName, ParentCompanyID, ParentCompanyName, Location.locazip, Location.locaCity, Location.locaState, loginPrefix
    End Function

    Public Function getLocationsByParentID(ByVal parentID As Guid) As DataTable
        Dim dt As New DataTable()
        Dim sql As String = "Select Location.ID As locaID, Location.Name As LocationName, Location.ParentCompanyID,  " & _
            "ParentCompany.Name As ParentCompanyName " & _
            "FROM Location INNER JOIN " & _
            "ParentCompany On Location.ParentCompanyID = ParentCompany.ID " & _
            "WHERE (Location.ParentCompanyID = @pcID) And Location.InActive = 0 " & _
            "ORDER BY Location.Name "
        Dim adapter As New SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings("rtdsConnectionString").ConnectionString)
        Dim param As New SqlParameter("pcID", parentID)
        adapter.SelectCommand.Parameters.Add(param)
        adapter.Fill(dt)
        Return dt     'locaID, LocationName, ParentCompanyID, ParentCompanyName

    End Function

    Public Function getLocationNameByID(ByVal locaID As String) As String
        Dim locaName As String = String.Empty
        Dim dba As New DBAccess()
        dba.CommandText = "Select Name FROM Location WHERE ID = @locaID"
        dba.AddParameter("@locaID", locaID)
        locaName = dba.ExecuteScalar
        Return locaName
    End Function

    Public Function getLoginPrefixByLocationID(ByVal locaID As String) As String
        Dim locaLoginPrefix As String = String.Empty
        Dim dba As New DBAccess()
        dba.CommandText = "Select loginPrefix FROM Location WHERE ID = @locaID"
        dba.AddParameter("@locaID", locaID)
        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
        If Not IsDBNull(dt.Rows(0).Item(0)) Then
            locaLoginPrefix = dt.Rows(0).Item(0)
        End If
        Return locaLoginPrefix
    End Function


    Public Function getLocaOffset(ByVal empID As Guid) As Integer
        Dim locaOffset As Integer
        Dim dba As New DBAccess()
        dba.CommandText = "Select locationID from employee where id = @empID"
        dba.AddParameter("@empID", empID)
        Dim locaID As Guid = dba.ExecuteScalar
        If locaID.ToString > "00000000-0000-0000-0000-000000000000" Then
            dba.CommandText = "Select BeginDayOffset FROM Location WHERE ID = @locaID"
            dba.AddParameter("@locaID", locaID)
            locaOffset = dba.ExecuteScalar
        End If
        Return locaOffset

    End Function

    Public Function getLocaBDOffset(ByVal locaID As Guid) As Integer
        Dim locaOffset As Integer
        Dim dba As New DBAccess()
        If locaID.ToString > "00000000-0000-0000-0000-000000000000" Then
            dba.CommandText = "Select BeginDayOffset FROM Location WHERE ID = @locaID"
            dba.AddParameter("@locaID", locaID)
            locaOffset = dba.ExecuteScalar
        End If
        Return locaOffset

    End Function

    Public Function getLocaTimeZoneOffset(ByVal locaID As Guid) As Integer
        Dim tzOffset As Integer = 0
        Dim dba As New DBAccess()
        dba.CommandText = "Select TimezoneOffset FROM Location WHERE ID=@ID"
        dba.AddParameter("@ID", locaID)
        Try
            tzOffset = dba.ExecuteScalar
        Catch ex As Exception
            tzOffset = 0
        End Try
        Return tzOffset
    End Function

    Public Function locaTime(ByVal thedate As DateTime, ByVal locaid As String) As DateTime
        Dim retdate As DateTime
        Dim str As String = String.Empty
        Dim tziList As New List(Of TimeZoneInfo)
        Dim dba As New DBAccess
        dba.CommandText = "SELECT TimezoneOffset FROM Location WHERE ID = @ID"
        dba.AddParameter("@ID", locaid)
        Dim tzoffset As Integer = dba.ExecuteScalar
        retdate = DateAdd(DateInterval.Hour, tzoffset, thedate)
        'System.TimeZoneInfo.ConvertTime(thedate, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        Return retdate
    End Function

    Public Function getParentCompanyIDbyLocationID(ByVal locaID As String) As Guid
        Dim sql As String = "Select ParentCompany.ID " & _
            "FROM ParentCompany INNER JOIN " & _
            "Location On ParentCompany.ID = Location.ParentCompanyID " & _
            "WHERE (Location.ID = @locaID)"
        Dim adapter As New SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings("rtdsConnectionString").ConnectionString)
        Dim param As New SqlParameter("locaID", locaID)
        adapter.SelectCommand.Parameters.Add(param)
        Dim dt As New DataTable()
        adapter.Fill(dt)
        getParentCompanyIDbyLocationID = dt.Rows(0).Item(0)
    End Function

    Public Function getParentName(ByVal parentID As Guid) As String
        Dim sql As String = "Select Name FROM ParentCompany WHERE ID = @pcID"
        Dim adapter As New SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings("rtdsConnectionString").ConnectionString)
        Dim param As New SqlParameter("pcID", parentID)
        adapter.SelectCommand.Parameters.Add(param)
        Dim dt As New DataTable()
        adapter.Fill(dt)
        getParentName = dt.Rows(0).Item(0)
    End Function

    Public Function getParentNameByLocationID(ByVal locaID As String) As String
        Dim sql As String = "Select ParentCompany.Name FROM Location INNER JOIN ParentCompany On Location.ParentCompanyID = ParentCompany.ID WHERE (Location.ID = @locaID)"
        Dim dba As New DBAccess()
        dba.CommandText = sql
        dba.AddParameter("@locaID", locaID)
        Dim pn As String = dba.ExecuteScalar
        Return pn
    End Function

    Public Function getParentCompanies() As DataTable
        Dim sql As String = "Select ID, Name FROM ParentCompany ORDER BY Name"
        Dim adapter As New SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings("rtdsConnectionString").ConnectionString)
        Dim dt As New DataTable()
        adapter.Fill(dt)
        Return dt   'ID, Names
    End Function

    Public Function getLocaPriceList(ByVal locaID As Guid) As List(Of PriceList)
        Dim dt As New DataTable
        Dim plList As New List(Of PriceList)
        Dim dba As New DBAccess()
        dba.CommandText = "Select PriceList.PriceID, PriceList.DepartmentID, PriceList.LoadtypeID, PriceList.LoadDescriptionID, PriceList.RatePerCase, PriceList.RatePerPallet, " & _
            "PriceList.PerPalletLow, PriceList.PerPalletHigh, PriceList.RatePerLoad, PriceList.RateBadPallet, PriceList.RateRestack, PriceList.PriceMax, " & _
            "PriceList.RateDoubleStack, PriceList.RatePinWheeled, Department.Name As DepartmentName, " & _
            "LoadType.Name As LoadTypeName, Description.Name As LoadDescriptionName " & _
            "FROM PriceList INNER JOIN " & _
            "Department On PriceList.DepartmentID = Department.ID INNER JOIN " & _
            "LoadType On PriceList.LoadtypeID = LoadType.ID INNER JOIN " & _
            "Description On PriceList.LoadDescriptionID = Description.ID " & _
            "WHERE LocationID = @locaID ORDER BY DepartmentName, LoadTypeName, LoadDescription"
        dba.AddParameter("@locaID", locaID)
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception
            Dim err As String = ex.Message
        End Try
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                Dim pl As New PriceList
                pl.PriceID = IIf(IsDBNull(rw.Item("PriceID")), Nothing, rw.Item("PriceID"))
                pl.LocationID = IIf(IsDBNull(rw.Item("LocationID")), Nothing, rw.Item("LocationID"))
                pl.DepartmentID = IIf(IsDBNull(rw.Item("DepartmentID")), Nothing, rw.Item("DepartmentID"))
                pl.LoadtypeID = IIf(IsDBNull(rw.Item("LoadtypeID")), Nothing, rw.Item("LoadtypeID"))
                pl.LoadDescriptionID = IIf(IsDBNull(rw.Item("LoadDescriptionID")), Nothing, rw.Item("LoadDescriptionID"))
                pl.RatePerCase = IIf(IsDBNull(rw.Item("RatePerCase")), Nothing, rw.Item("RatePerCase"))
                pl.RatePerPallet = IIf(IsDBNull(rw.Item("RatePerPallet")), Nothing, rw.Item("RatePerPallet"))
                pl.PerPalletLow = IIf(IsDBNull(rw.Item("PerPalletLow")), Nothing, rw.Item("PerPalletLow"))
                pl.PerPalletHigh = IIf(IsDBNull(rw.Item("PerPalletHigh")), Nothing, rw.Item("PerPalletHigh"))
                pl.RatePerLoad = IIf(IsDBNull(rw.Item("RatePerLoad")), Nothing, rw.Item("RatePerLoad"))
                pl.RateBadPallet = IIf(IsDBNull(rw.Item("RateBadPallet")), Nothing, rw.Item("RateBadPallet"))
                pl.RateRestack = IIf(IsDBNull(rw.Item("RateRestack")), Nothing, rw.Item("RateRestack"))
                pl.PriceMax = IIf(IsDBNull(rw.Item("PriceMax")), Nothing, rw.Item("PriceMax"))
                pl.RateDoubleStack = IIf(IsDBNull(rw.Item("RateDoubleStack")), Nothing, rw.Item("RateDoubleStack"))
                pl.RatePinWheeled = IIf(IsDBNull(rw.Item("RatePinWheeled")), Nothing, rw.Item("RatePinWheeled"))
                pl.DepartmentName = IIf(IsDBNull(rw.Item("DepartmentName")), Nothing, rw.Item("DepartmentName"))
                pl.LoadTypeName = IIf(IsDBNull(rw.Item("LoadTypeName")), Nothing, rw.Item("LoadTypeName"))
                pl.LoadDescriptionName = IIf(IsDBNull(rw.Item("LoadDescriptionName")), Nothing, rw.Item("LoadDescriptionName"))
                plList.Add(pl)
            Next
        End If
        Return plList
    End Function

    Public Function getLocaPriceListTable(ByVal locaID As Guid) As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        dba.CommandText = "Select PriceList.PriceID, PriceList.DepartmentID, PriceList.LoadtypeID, PriceList.LoadDescriptionID, PriceList.RatePerCase, PriceList.RatePerPallet, " & _
            "PriceList.PerPalletLow, PriceList.PerPalletHigh, PriceList.RatePerLoad, PriceList.RateBadPallet, PriceList.RateRestack, PriceList.PriceMax, " & _
            "PriceList.RateDoubleStack, PriceList.RatePinWheeled, Department.Name As DepartmentName, " & _
            "LoadType.Name As LoadTypeName, Description.Name As LoadDescriptionName " & _
            "FROM PriceList INNER JOIN " & _
            "Department On PriceList.DepartmentID = Department.ID INNER JOIN " & _
            "LoadType On PriceList.LoadtypeID = LoadType.ID INNER JOIN " & _
            "Description On PriceList.LoadDescriptionID = Description.ID " & _
            "WHERE LocationID = @locaID ORDER BY Department.Name, LoadType.Name"
        dba.AddParameter("@locaID", locaID)
        Try
            dt = dba.ExecuteDataSet.Tables(0)
        Catch ex As Exception

        End Try

        Return dt
    End Function


#Region "Location CRUD"
    Public Function insertLocation(ByVal loca As Location) As String
        Dim retString As String = Nothing
        Dim dba As New DBAccess()
        dba.CommandText = "INSERT INTO Location (ParentCompanyID, Name, ID, BeginDayOffset, TimezoneOffset, InActive,hhPrintTimeStamp, CheckCharge, Dividend, locaCity, locaState, locazip, loginPrefix, EnableBenefits, AdministrativeFee, CustomerFee) " & _
            "VALUES (@ParentCompanyID, @Name, @ID, @BeginDayOffset, @TimezoneOffset, @InActive,@hhPrintTimeStamp, @CheckCharge, @Dividend, @locaCity, @locaState, @locazip, @loginPrefix,@EnableBenefits, @AdministrativeFee, @CustomerFee)"
        dba.AddParameter("@ParentCompanyID", loca.ParentCompanyID)
        dba.AddParameter("@Name", loca.Name)
        dba.AddParameter("@ID", loca.ID)
        dba.AddParameter("@BeginDayOffset", loca.BeginDayOffset)
        dba.AddParameter("@TimezoneOffset", loca.TimezoneOffset)
        dba.AddParameter("@InActive", loca.InActive)
        dba.AddParameter("@hhPrintTimeStamp", loca.hhPrintTimeStamp)
        dba.AddParameter("@CheckCharge", loca.CheckCharge)
        dba.AddParameter("@Dividend", loca.Dividend)
        dba.AddParameter("@locaCity", loca.locaCity)
        dba.AddParameter("@locaState", loca.locaState)
        dba.AddParameter("@locazip", loca.locazip)
        dba.AddParameter("@loginPrefix", loca.loginPrefix)
        dba.AddParameter("@EnableBenefits", loca.EnableSickLeave)
        dba.AddParameter("@AdministrativeFee", loca.AdministrativeFee)
        dba.AddParameter("@CustomerFee", loca.CustomerFee)
        Try
            dba.ExecuteNonQuery()
        Catch ex As Exception
            retString = ex.Message
        End Try
        Return retString
    End Function

    Public Function updateLocation(ByVal loca As Location) As String
        Dim retString As String = Nothing
        Dim dba As New DBAccess()
        dba.CommandText = "UPDATE Location Set ParentCompanyID=@ParentCompanyID, Name=@Name, BeginDayOffset=@BeginDayOffset, TimezoneOffset=@TimezoneOffset, " & _
            "InActive=@InActive,hhPrintTimeStamp=@hhPrintTimeStamp, CheckCharge=@CheckCharge, Dividend=@Dividend, locaCity=@locaCity, locaState=@locaState, " & _
            "locazip=@locazip, loginPrefix=@loginPrefix, EnableBenefits= @EnableBenefits, AdministrativeFee=@AdministrativeFee, CustomerFee=@CustomerFee WHERE  ID=@ID"
        dba.AddParameter("@ParentCompanyID", loca.ParentCompanyID)
        dba.AddParameter("@Name", loca.Name)
        dba.AddParameter("@ID", loca.ID)
        dba.AddParameter("@BeginDayOffset", loca.BeginDayOffset)
        dba.AddParameter("@TimezoneOffset", loca.TimezoneOffset)
        dba.AddParameter("@InActive", loca.InActive)
        dba.AddParameter("@hhPrintTimeStamp", loca.hhPrintTimeStamp)
        dba.AddParameter("@CheckCharge", loca.CheckCharge)
        dba.AddParameter("@Dividend", loca.Dividend)
        dba.AddParameter("@locaCity", loca.locaCity)
        dba.AddParameter("@locaState", loca.locaState)
        dba.AddParameter("@locazip", loca.locazip)
        dba.AddParameter("@loginPrefix", loca.loginPrefix)
        dba.AddParameter("@EnableBenefits", loca.EnableSickLeave)
        dba.AddParameter("@AdministrativeFee", loca.AdministrativeFee)
        dba.AddParameter("@CustomerFee", loca.CustomerFee)
        Try
            dba.ExecuteNonQuery()
        Catch ex As Exception
            retString = ex.Message
        End Try
        Return retString
    End Function

    Public Sub setLocaComboCBI(ByVal usr As MembershipUser, ByRef cblocations As RadComboBox)
        Dim uid As String = usr.ProviderUserKey.ToString
        Dim rtdsID As String
        rtdsID = Utilities.getRTDSidByUserID(uid)
        Dim emp As New Employee
        If rtdsID <> "00000000-0000-0000-0000-000000000000" Then
            'if this is an hourly or percentage employee populate Employee object
            Dim rtdsIDtoGUID As New Guid(rtdsID)
            Dim empdal As New empDAL
            emp = empdal.GetEmployeeByID(rtdsIDtoGUID)
        End If

        Dim udal As New userDAL()
        Dim ldal As New locaDAL()
        'populate the ssUser object in the Employee object
        emp.ssUser = udal.getUserByName(usr.UserName)
        Dim dt As New DataTable
        dt = ldal.getUserLocaList(emp.ssUser.userID)
        If dt.Rows.Count > 0 Then   'this user is in the LocationUser table
            'set/replace LocationID in the Employee Object with top item in list
            emp.LocationID = dt.Rows(0).Item("locaID")

        Else
            dt = ldal.getCBILocations
        End If

        cblocations.DataSource = dt
        cblocations.DataTextField = "LocationName"
        cblocations.DataValueField = "locaID"
        cblocations.DataBind()
        For Each itm As RadComboBoxItem In cblocations.Items
            If Not itm.Text.Contains("CBI") Then
                cblocations.Items.Remove(itm)
            End If
        Next
        cblocations.DataBind()
        cblocations.AutoPostBack = True
        cblocations.EmptyMessage = "Select Location"
        cblocations.ClearSelection()
        cblocations.SelectedValue = emp.LocationID.ToString
    End Sub

    Public Sub setLocaCombo(ByVal usr As MembershipUser, ByRef cbLocations As RadComboBox, Optional ByVal withFern As Boolean = False)
        Dim uid As String = "54777A2C-68A0-47E0-BC63-9C22CEE81135"
        Dim rtdsID As String
        rtdsID = Utilities.getRTDSidByUserID(uid)
        Dim emp As New Employee
        If rtdsID <> "00000000-0000-0000-0000-000000000000" Then
            'if this is an hourly or percentage employee populate Employee object
            Dim rtdsIDtoGUID As New Guid(rtdsID)
            Dim empdal As New empDAL
            emp = empdal.GetEmployeeByID(rtdsIDtoGUID)
        End If

        Dim udal As New userDAL()
        Dim ldal As New locaDAL()
        'populate the ssUser object in the Employee object
        emp.ssUser = udal.getUserByName("Bill")
        Dim dt As New DataTable
        dt = ldal.getUserLocaList(emp.ssUser.userID)
        If dt.Rows.Count > 0 Then   'this user is in the LocationUser table
            'set/replace LocationID in the Employee Object with top item in list
            emp.LocationID = dt.Rows(0).Item("locaID")

        Else
            dt = ldal.getLocations(withFern)
        End If
        cbLocations.DataSource = dt
        cbLocations.DataTextField = "LocationName"
        cbLocations.DataValueField = "locaID"
        cbLocations.DataBind()
        cbLocations.AutoPostBack = True
        cbLocations.EmptyMessage = "Select Location"
        cbLocations.ClearSelection()
        cbLocations.SelectedValue = emp.LocationID.ToString
    End Sub

    Public Function getUserLocations(ByVal user As MembershipUser) As DataTable
        Dim dt As DataTable = Nothing
        Return dt

    End Function


    ' cannot delete location
    'Public Function deleteLocation(ByVal locaID As Guid) As String
    '    ' cannot delete location
    '    Dim retString As String = Nothing
    '    Dim dba As New DBAccess()
    '    dba.CommandText = "xx"
    '    dba.AddParameter("@xx", xx.xx)
    '    Try
    '        dba.ExecuteNonQuery()
    '    Catch ex As Exception
    '        retString = ex.Message
    '    End Try
    '    Return retString
    'End Function
#End Region


    Public Function GetJobTitles() As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        dba.CommandText = "Select JobTitle.JobTitleID As JobTitleID, JobTitle.JobTitle FROM JobTitle Order by JobTitle"
        dt = dba.ExecuteDataSet.Tables(0)
        Return dt
    End Function

    Public Function GetJobTitlesbyLocationID(ByVal locaid As String) As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        dba.CommandText = "Select Count(LocationJobTitle.LocationID) As lcount FROM LocationJobTitle WHERE LocationID=@locaid"
        dba.AddParameter("@locaid", New Guid(locaid))
        Dim num As Integer = dba.ExecuteScalar
        If num > 0 Then
            dba.CommandText = "Select JobTitle.JobTitleID, JobTitle.JobTitle " & _
                "FROM JobTitle INNER JOIN " & _
                "LocationJobTitle On JobTitle.JobTitleID = LocationJobTitle.JobTitleID " & _
                "WHERE (LocationJobTitle.LocationID = @locaid)" & _
                "ORDER BY JobTitle"
            dba.AddParameter("@locaid", New Guid(locaid))
            dt = dba.ExecuteDataSet.Tables(0)
            For Each row In dt.Rows
                Dim txt As String = row("jobTitle").ToString()
                Dim val As String = row("JobTitleID").ToString
                Return dt
            Next
        Else
            dt = GetJobTitles()
        End If
        Return dt
    End Function
    Public Function getLoadTypesByLocationID(Optional ByVal locaid As String = "") As DataTable
        Dim dt As DataTable = New DataTable
        Dim dba As New DBAccess
        dba.CommandText = "SELECT ID, Name FROM LoadType ORDER BY Name"
        Dim ds As New DataSet
        dt = dba.ExecuteDataSet.Tables(0)
        Return dt
    End Function

    Public Function GetDepartments() As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        dba.CommandText = "Select ID , Department.Name FROM Department Order by Name"
        dt = dba.ExecuteDataSet.Tables(0)
        Return dt
    End Function

    Public Function GetDepartmentsByLocationID(ByVal locaid As String, Optional retempty As Boolean = False) As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        dba.CommandText = "Select Count(*) FROM LocationDepartment WHERE LocationID=@locaid"
        dba.AddParameter("@locaid", locaid)
        Dim num As Integer = dba.ExecuteScalar
        If num > 0 Then
            dba.CommandText = "Select Department.ID, Department.Name " & _
                "FROM Department INNER JOIN " & _
                "LocationDepartment On Department.ID = LocationDepartment.DepartmentID " & _
                "WHERE (LocationDepartment.LocationID = @locaid)" & _
                "ORDER BY Name"
            dba.AddParameter("@locaid", New Guid(locaid))
            dt = dba.ExecuteDataSet.Tables(0)
            Return dt
        Else
            If retempty = True Then
                Return dt
            Else
                dt = GetDepartments()
            End If
        End If
        Return dt
    End Function

    Public Function GetLoadDescriptionIDByName(ByVal locaid As Guid, ByVal descname As String) As Guid
        Dim retguid As Guid = Utilities.zeroGuid
        Dim dba As New DBAccess
        dba.CommandText = "SELECT LocationDescription.DescriptionID " & _
            "FROM LocationDescription INNER JOIN " & _
            "Description ON LocationDescription.DescriptionID = Description.ID " & _
            "WHERE (LocationDescription.LocationID = @locaID) AND (Description.Name = @loaddescription)"
        dba.AddParameter("locaID", locaid)
        dba.AddParameter("loaddescription", descname)
        Dim did As Guid = dba.ExecuteScalar
        If Utilities.IsValidGuid(did.ToString) Then
            retguid = did
        End If
        Return retguid
    End Function

    Public Function GetLoadDescriptions() As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        dba.CommandText = "Select Name, ID FROM Description Order by Name"
        dt = dba.ExecuteDataSet.Tables(0)
        Return dt
    End Function

    Public Function GetLoadDescriptionsByLocationID(ByVal locaid As String) As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        dba.CommandText = "Select Count(*) FROM LocationDescription WHERE LocationID=@locaid "
        dba.AddParameter("@locaid", New Guid(locaid))
        Dim num As Integer = dba.ExecuteScalar
        If num > 0 Then
            dba.CommandText = "Select Description.Name, Description.ID, LocationDescription.LocationID " & _
                "FROM Description INNER JOIN " & _
                "LocationDescription On Description.ID = LocationDescription.DescriptionID " & _
                "WHERE (LocationDescription.LocationID = @locaid) order by Name"
            dba.AddParameter("@LocaID", locaid)
            dt = dba.ExecuteDataSet.Tables(0)
            Return dt
        Else
            dt = GetLoadDescriptions()
        End If
        Return dt
    End Function

    Public Function GetJobDescriptions() As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        dba.CommandText = "Select JobDescriptions.ID AS JobDescriptionID, JobDescription FROM JobDescriptions Order by JobDescription"
        dt = dba.ExecuteDataSet.Tables(0)
        Return dt
    End Function

    Public Function BenefitsEnabled(ByVal locaid As String) As Boolean
        Dim retboo As Boolean = False
        Dim dba As New DBAccess
        dba.CommandText = "Select enablebenefits from location where id = @locaid"
        dba.AddParameter("@locaid", locaid)
        retboo = dba.ExecuteScalar
        Return retboo
    End Function

    Public Function GetJobDescriptionsByLocationID(ByVal locaid As String, Optional retempty As Boolean = False) As DataTable
        Dim dt As New DataTable
        Dim dba As New DBAccess()
        dba.CommandText = "Select Count(*) FROM LocationJobDescriptions WHERE LocationID=@locaid "
        dba.AddParameter("@locaid", New Guid(locaid))
        Dim num As Integer = dba.ExecuteScalar
        If num > 0 Then
            dba.CommandText = "SELECT LocationJobDescriptions.LocationID, LocationJobDescriptions.JobDescriptionID, JobDescriptions.JobDescription " & _
                "FROM JobDescriptions INNER JOIN " & _
                "LocationJobDescriptions ON JobDescriptions.ID = LocationJobDescriptions.JobDescriptionID " & _
                "WHERE (LocationJobDescriptions.LocationID = @locaid) order by JobDescription"
            dba.AddParameter("@LocaID", locaid)
            dt = dba.ExecuteDataSet.Tables(0)
            Return dt
        Else
            If retempty = True Then
                Return dt
            Else
                dt = GetJobDescriptions()
            End If
        End If
        Return dt
    End Function

    Public Function getJobdescriptionNameByID(ByVal jdID As Guid) As String
        Dim dba As New DBAccess()
        dba.CommandText = "SELECT JobDescription from JobDescriptions WHERE ID = @ID"
        dba.AddParameter("@ID", jdID)
        Return dba.ExecuteScalar
    End Function

#Region "PriceList CRUD"
    Public Function insertPriceList(ByVal pl As PriceList) As String
        Dim retString As String = Nothing
        pl.PriceID = Guid.NewGuid()
        Dim dba As New DBAccess()
        dba.CommandText = "INSERT INTO PriceList (PriceID, LocationID, DepartmentID, LoadtypeID, LoadDescriptionID, RatePerCase, RatePerPallet, PerPalletLow, PerPalletHigh, RatePerLoad, RateBadPallet, RateRestack, PriceMax, RateDoubleStack, RatePinWheeled) " & _
            "VALUES (@PriceID, @LocationID, @DepartmentID, @LoadtypeID, @LoadDescriptionID, @RatePerCase, @RatePerPallet, @PerPalletLow, @PerPalletHigh, @RatePerLoad, @RateBadPallet, @RateRestack, @PriceMax, @RateDoubleStack, @RatePinWheeled)"
        dba.AddParameter("@PriceID", pl.PriceID)
        dba.AddParameter("@LocationID", pl.LocationID)
        dba.AddParameter("@DepartmentID", pl.DepartmentID)
        dba.AddParameter("@LoadtypeID", pl.LoadtypeID)
        dba.AddParameter("@LoadDescriptionID", pl.LoadDescriptionID)
        dba.AddParameter("@RatePerCase", pl.RatePerCase)
        dba.AddParameter("@RatePerPallet", pl.RatePerPallet)
        dba.AddParameter("@PerPalletLow", pl.PerPalletLow)
        dba.AddParameter("@PerPalletHigh", pl.PerPalletHigh)
        dba.AddParameter("@RatePerLoad", pl.RatePerLoad)
        dba.AddParameter("@RateBadPallet", pl.RateBadPallet)
        dba.AddParameter("@RateRestack", pl.RateRestack)
        dba.AddParameter("@PriceMax", pl.PriceMax)
        dba.AddParameter("@RateDoubleStack", pl.RateDoubleStack)
        dba.AddParameter("@RatePinWheeled", pl.RatePinWheeled)
        Try
            dba.ExecuteNonQuery()
        Catch ex As Exception
            retString = ex.Message
        End Try
        Return retString
    End Function
    Public Function updatePriceList(ByVal pl As PriceList) As String
        Dim retString As String = Nothing
        Dim dba As New DBAccess()
        dba.CommandText = "UPDATE PriceList Set DepartmentID=@DepartmentID, LoadtypeID=@LoadtypeID, LoadDescriptionID=@LoadDescriptionID, RatePerCase=@RatePerCase, RatePerPallet=@RatePerPallet, PerPalletLow=@PerPalletLow, PerPalletHigh=@PerPalletHigh, RatePerLoad=@RatePerLoad, RateBadPallet=@RateBadPallet, RateRestack=@RateRestack, PriceMax=@PriceMax, RateDoubleStack=@RateDoubleStack, RatePinWheeled=@RatePinWheeled WHERE PriceID = @PriceID"
        dba.AddParameter("@PriceID", pl.PriceID)
        dba.AddParameter("@DepartmentID", pl.DepartmentID)
        dba.AddParameter("@LoadtypeID", pl.LoadtypeID)
        dba.AddParameter("@LoadDescriptionID", pl.LoadDescriptionID)
        dba.AddParameter("@RatePerCase", pl.RatePerCase)
        dba.AddParameter("@RatePerPallet", pl.RatePerPallet)
        dba.AddParameter("@PerPalletLow", pl.PerPalletLow)
        dba.AddParameter("@PerPalletHigh", pl.PerPalletHigh)
        dba.AddParameter("@RatePerLoad", pl.RatePerLoad)
        dba.AddParameter("@RateBadPallet", pl.RateBadPallet)
        dba.AddParameter("@RateRestack", pl.RateRestack)
        dba.AddParameter("@PriceMax", pl.PriceMax)
        dba.AddParameter("@RateDoubleStack", pl.RateDoubleStack)
        dba.AddParameter("@RatePinWheeled", pl.RatePinWheeled)
        Try
            dba.ExecuteNonQuery()
        Catch ex As Exception
            retString = ex.Message
        End Try
        Return retString
    End Function
    Public Function deletePriceList(ByVal plID As Guid) As String
        Dim retString As String = Nothing
        Dim dba As New DBAccess()
        dba.CommandText = "DELETE FROM PriceList WHERE PriceID = @PriceID"
        dba.AddParameter("@PriceID", plID)
        Try
            dba.ExecuteNonQuery()
        Catch ex As Exception
            retString = ex.Message
        End Try
        Return retString
    End Function

    Public Function PrintReceipt(ByVal locaid As Guid) As Boolean
        Dim dba As New DBAccess
        dba.CommandText = "Select hhPrintTimeStamp from Location where ID=@ID"
        dba.AddParameter("@ID", locaid)
        Return dba.ExecuteScalar = 1
    End Function

#End Region


End Class
