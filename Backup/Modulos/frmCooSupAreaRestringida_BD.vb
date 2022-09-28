Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geodatabase
Imports PORTAL_Clases

Imports ESRI.ArcGIS.Carto
Public Class frmCooSupAreaRestringida_BD
    Public m_application As IApplication
    Private cls_catastro As New cls_DM_1
    Private cls_Oracle As New cls_Oracle

    Private Sub frmCooSupAreaRestringida_BD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim tema_procesing As String = ""
        cls_catastro.Load_FC_GDB("Catastro", "", m_application, True)
        'Dim pLayer As IFeatureLayer = Nothing
        'Dim pGeoFeatureLayer As IGeoFeatureLayer
        'pMxDoc = m_application.Document
        'pMap = pMxDoc.FocusMap
        'Dim afound As Boolean
        'For A As Integer = 0 To pMap.LayerCount - 1
        '    If pMap.Layer(A).Name = "Area Reservada" Then
        '        pLayer = pMap.Layer(A)
        '        afound = True
        '        Exit For
        '    End If
        'Next A
        'If Not afound Then
        '    MsgBox("Layer No Existe.")
        '    Exit Sub
        'End If
        'pGeoFeatureLayer = pLayer

        Dim valor_areasup As Long
        fecha_archi = DateTime.Now.Ticks.ToString
        Dim pNewWSName As IWorkspaceName
        Dim pFCName As IFeatureClassName
        Dim pDatasetName As IDatasetName
        Dim tema1 As IFeatureLayer
        Dim tema2 As IFeatureLayer

        'Define primer tema
        '********************
        Dim tema1_fclass As IFeatureClass
        Dim tema2_fclass As IFeatureClass
        Dim pFeatLayer As IFeatureLayer = Nothing
        pMap = pMxDoc.FocusMap
        Dim afound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Catastro_1" Then
                pFeatLayer = pMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra la capa de Derechos Mineros para realizar el calculo de área disponible", MsgBoxStyle.Information, "Observación...")
            Exit Sub
        End If
        tema1 = pFeatLayer
        tema1_fclass = tema1.FeatureClass

        'Define segundo tema
        '*********************
        afound = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Zona Reservada" Then
                pFeatLayer = pMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("EL DM no intersecta con Areas de Reserva", MsgBoxStyle.Information, "Observación...")
            Exit Sub
        End If
        tema2 = pFeatLayer
        tema2_fclass = tema2.FeatureClass
        'Define tema de salida
        pFCName = New FeatureClassName
        pDatasetName = pFCName
        pNewWSName = New WorkspaceName
        pNewWSName.WorkspaceFactoryProgID = "esriCore.ShapeFileWorkspaceFactory"
        pNewWSName.PathName = glo_pathTMP
        pDatasetName.WorkspaceName = pNewWSName
        Dim tol As Double = 0.0
        Dim pOutputFC As IFeatureClass
        pBGP = New BasicGeoprocessor
        'Define tolerancia
        tol = 1
        pOutputFC = pBGP.Intersect(tema1_fclass, False, tema2_fclass, False, Nothing, pFCName)
        Dim pOutputFeatLayer As IFeatureLayer
        pOutputFeatLayer = New FeatureLayer
        pOutputFeatLayer.FeatureClass = pOutputFC
        pOutputFeatLayer.Name = "Sup_Area_Restringida"
        valor_areasup = pOutputFeatLayer.FeatureClass.FeatureCount(Nothing)  'cuenta registros del tema en total
        pMap.AddLayer(pOutputFeatLayer)
        cls_catastro.Quitar_Layer("Catastro_1", m_application)


    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        var_fa_Coord_SuperAreaReserva = True
    End Sub
End Class