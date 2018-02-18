Imports System
'Imports System.Collections
'Imports System.ComponentModel
Imports System.Data
'Imports System.Drawing
'Imports System.Web
'Imports System.Web.SessionState
'Imports System.Web.UI
'Imports System.Web.UI.WebControls
'Imports System.Web.UI.HtmlControls
'Imports Telerik.Web.UI
'Imports Telerik.Charting
'Imports Telerik.Charting.Styles

Partial Class Dashboard
    Inherits System.Web.UI.Page


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Dim objectDataSource As New Telerik.Reporting.ObjectDataSource()
            objectDataSource.DataSource = GetAllData()
            '            objectDataSource.DataMember = "tableNameInDataSourceDataSet"
            objectDataSource.CalculatedFields.Add(New Telerik.Reporting.CalculatedField("DblName", GetType(String), "=Fields.Name + ' ' + Fields.Name"))


            'Creating a new report
            Dim report As New Telerik.Reporting.Report()

            ' Assigning the ObjectDataSource component to the DataSource property of the report.
            report.DataSource = objectDataSource


            ' Use the InstanceReportSource to pass the report to the viewer for displaying.
            Dim reportSource As New Telerik.Reporting.InstanceReportSource

            reportSource.ReportDocument = report ' New SEUreports.ForkliftCerts()

            '            reportSource.Parameters.Add("location", "Ocala")


            ' Assigning the report to the report viewer.
            reportviewer1.ReportSource = reportSource

            ' Calling the RefreshReport method in case this is a WinForms application.
            '            reportviewer1.RefreshReport()



        End If
    End Sub 'Page_Load
#Region "Web Form Designer generated code"

    Protected Overrides Sub OnInit(ByVal e As EventArgs)
        '
        ' CODEGEN: This call is required by the ASP.NET Web Form Designer.
        '
        InitializeComponent()
        MyBase.OnInit(e)
    End Sub 'OnInit


    '/        Required method for Designer support - do not modify
    '/        the contents of this method with the code editor.
    '/ </summary>
    Private Sub InitializeComponent()
    End Sub 'InitializeComponent

#End Region



    Shared Function GetAllData() As DataSet
        Dim dba As New DBAccess
        dba.CommandText = "Select * from location"
        Dim ds As DataSet = dba.ExecuteDataSet
        Return ds
    End Function


End Class