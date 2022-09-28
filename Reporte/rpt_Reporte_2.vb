Imports System.Xml
Imports C1.Win.C1Preview
Imports C1.Win.C1Report
Imports System.IO
Public Class rpt_Reporte_2
    Dim m_ReportDefinitionFile As String

    Private Sub Reporte_2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Reporte de " & C1Report1.ReportName
        ppv.Document = C1Report1.Document
    End Sub
End Class