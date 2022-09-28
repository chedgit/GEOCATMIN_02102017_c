Imports System.Drawing
Imports PORTAL_Clases
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.GISClient
Imports ESRI.ArcGIS.Display
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Output
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.CartoUI
Imports ESRI.ArcGIS.GeoDatabaseUI
Imports stdole
Public Class Frm_listadoescalaplanos
    'Public m_Application As IApplication
    Public p_app As IApplication
    Private Sub Frm_listadoescalaplanos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.btnGraficar.Enabled = False
    End Sub
    Private Sub btnGraficar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGraficar.Click
       
        Dim cls_planos As New Cls_planos
        Dim sele_plano_sup As String
        sele_plano_sup = Me.lstescala.Text
        Dim canti As Integer
        canti = Len(sele_plano_sup)
        'Seleccionado la escala del plano
        If canti = 18 Then
            sele_plano_sup = Mid(sele_plano_sup, 13, 5)
        ElseIf canti = 20 Then
            sele_plano_sup = Mid(sele_plano_sup, 15, 5)
        ElseIf canti = 21 Then
            sele_plano_sup = Mid(sele_plano_sup, 15, 6)
        End If
        If sele_plano_sup = 10000 Then
            escalaf = 10000
        ElseIf sele_plano_sup = 14000 Then
            escalaf = 14000
        ElseIf sele_plano_sup = 20000 Then
            escalaf = 20000
        ElseIf sele_plano_sup = 25000 Then
            escalaf = 25000
        ElseIf sele_plano_sup = 40000 Then
            escalaf = 40000
        ElseIf sele_plano_sup = 50000 Then
            escalaf = 50000
        ElseIf sele_plano_sup = 60000 Then
            escalaf = 60000
        ElseIf sele_plano_sup = 75000 Then
            escalaf = 75000
        ElseIf sele_plano_sup = 100000 Then
            escalaf = 100000
        Else
            Exit Sub
        End If
        Me.Close()
        cls_planos.generaplano_Ate_publico(p_app)

    End Sub

    Private Sub lstescala_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstescala.SelectedIndexChanged
        If Me.lstescala.TopIndex <> -1 Then
            Me.btnGraficar.Enabled = True
        End If
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()

    End Sub
End Class