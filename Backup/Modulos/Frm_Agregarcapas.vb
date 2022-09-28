Imports System.IO
Imports System.Collections
Imports PORTAL_Clases
Imports ESRI.ArcGIS.Framework
Imports System.Drawing
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.DataSourcesOleDB
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.esriSystem

Imports ESRI.ArcGIS.GISClient
Imports ESRI.ArcGIS.Display
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Carto
Imports stdole

Public Class Frm_Agregarcapas
    Public m_application As IApplication
    Private cls_Catastro As New cls_DM_1
    Private cls_DM_2 As New cls_DM_2
    Private cls_Prueba As New cls_Prueba
    Private cls_eval As New Cls_evaluacion
    Private cls_planos As New Cls_planos
    Private cls_Usuario As New cls_Usuario
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        ' cls_Usuario.Activar_Layer_True_False(True, m_application)
        Activar_Capas_DM1(clbLayer, v_zona_dm)
    End Sub
    Public Sub Activar_Capas_DM1(ByVal p_clbLayer As Object, ByVal p_Zona As String)
        'If Me.cboZona.SelectedIndex = 0 Then Exit Sub
        'Dim pFeatureLayer As IFeatureLayer
        caso_consulta = "CATASTRO MINERO"
        cls_eval.ActivaDataframe_Opcion(caso_consulta, m_application)
        Dim lo_Filtro_Dpto As String = ""
        pMap = pMxDoc.FocusMap
        Dim afound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Catastro" Then
                pFeatureLayer = pMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra el tema Catastro en el mapa", MsgBoxStyle.Information, "BGGEOCATMIN")
            Exit Sub
        End If
        Me.Close()


        If p_clbLayer.CheckedItems.Count <= 0 Then Exit Sub
        'Dim glo_xMin_1, glo_yMin_1, glo_xMax_1, glo_yMax_1 As Double
        'glo_xMin_1 = glo_xMin : glo_xMax_1 = glo_xMax
        'glo_yMin_1 = glo_yMin : glo_yMax_1 = glo_yMax

        If p_clbLayer.CheckedItems.Count <= 0 Then Exit Sub
        If glo_xMin <> 0 And glo_yMin <> 0 And glo_xMax <> 0 And glo_yMax <> 0 Then
            'If v_este_min <> 0 And v_norte_min <> 0 And v_este_max <> 0 And v_norte_max <> 0 Then
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "1", False)

            lo_Filtro_Dpto = cls_eval.f_Intercepta_temas("Departamento", glo_xMin, glo_yMin, glo_xMax, glo_yMax, m_application)
            cls_Catastro.Quitar_Layer("Departamento", m_application)
        Else
            MsgBox("No se puede agregar capas al tema", MsgBoxStyle.Information, "[BDGeocatmin]")
            Exit Sub
        End If

        For i As Integer = 0 To p_clbLayer.CheckedItems.Count - 1
            Select Case p_clbLayer.CheckedItems.Item(i) '.SelectedItem '.GetItemText(i) clbLayer.CheckedItems.Item(1)
                Case "Capitales Distritales"
                    cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_CDistrito, m_application, "1", False)
                    Dim lo_Filtro_cp As String = cls_Catastro.f_Intercepta_FC("Capitales Distritales", glo_xMin, glo_yMin, glo_xMax, glo_yMax, m_application)
                    If lo_Filtro_cp <> "" Then
                        cls_Catastro.DefinitionExpression(lo_Filtro_cp, m_application, "Capitales Distritales")
                        cls_Catastro.Shade_Poligono("Capitales Distritales", m_application)
                    Else
                        cls_Catastro.Quitar_Layer("Capitales Distritales", m_application)
                    End If
                Case "Limite Departamental"
                    cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "1", False)
                    cls_Catastro.DefinitionExpression(lo_Filtro_Dpto, m_application, "Departamento")
                    cls_Catastro.Shade_Poligono("Departamento", m_application)

                Case "Limite Provincial"
                    cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Provincia, m_application, "1", False)
                    cls_Catastro.DefinitionExpression(lo_Filtro_Dpto, m_application, "Provincia")
                    cls_Catastro.Shade_Poligono("Provincia", m_application)

                Case "Limite Distrital"
                    cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito, m_application, "1", False)
                    Exit Sub

                    cls_Catastro.DefinitionExpression(lo_Filtro_Dpto, m_application, "Distrito")
                    cls_Catastro.Shade_Poligono("Distrito", m_application)
                Case "Red Hidrografica"
                    cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Rios, m_application, "1", False)
                    cls_Catastro.DefinitionExpression(lo_Filtro_Dpto, m_application, "Drenaje")
                    cls_Catastro.Shade_Poligono("Drenaje", m_application)
                Case "Red Vial"
                    cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Carretera, m_application, "1", False)
                    cls_Catastro.DefinitionExpression(lo_Filtro_Dpto, m_application, "Vias")
                    cls_Catastro.Shade_Poligono("Vias", m_application)

                Case "Centros Poblados"
                    cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_CPoblado, m_application, "1", False)
                    cls_Catastro.DefinitionExpression(lo_Filtro_Dpto, m_application, "Centro Poblado")

            End Select
        Next i
        cls_Catastro.Zoom_to_Layer("Catastro")

        ' cls_Catastro.HazZoom(glo_xMin - loint_Intervalo, glo_yMin - loint_Intervalo, glo_xMax + loint_Intervalo, glo_yMax + loint_Intervalo, 0, m_application)
    End Sub

    Private Sub Frm_Agregarcapas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub
End Class