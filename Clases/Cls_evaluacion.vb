Imports ESRI.ArcGIS.GISClient
Imports ESRI.ArcGIS.Display
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Output
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.CartoUI
Imports ESRI.ArcGIS.GeoDatabaseUI
Imports ESRI.ArcGIS.ADF
Imports stdole
Imports PORTAL_Clases

Public Class Cls_evaluacion
    Structure Punto_DM
        Dim v As Integer
        Dim x As Double
        Dim y As Double
    End Structure
    Public cls_Catastro As New cls_DM_1
    Public pApp As IApplication
    Private extension As String = ""
    Private Property lista_Cd_Geologia_bufer As String


    Public Sub seleccionadmsegunditancia(ByVal capa As String, ByVal tipo_seleccion_tema As String)
        'Programa para seleccionar poligonos en base a una distancia -  esto a fin de calcular los DM CONLINDANTES
        '------------------------------------------------------------------------------------------------------------
        pMap = pMxDoc.FocusMap
        Dim pFeatLayer As IFeatureLayer
        Dim pFeatureCursor2 As IFeatureCursor = Nothing
        Dim pfeature As IFeature
        Dim v_codigo_rd As String
        Dim v_estado_rd As String

        Dim SPATIALFILTER As ISpatialFilter
        Dim pElement As IElement
        Dim PTOPOLOG As ITopologicalOperator
        SPATIALFILTER = New SpatialFilter
        If tipo_seleccion_tema = "Interceptando" Then
            SPATIALFILTER.SpatialRel = esriSpatialRelEnum.esriSpatialRelIndexIntersects
        ElseIf tipo_seleccion_tema = "colidantes" Then
            SPATIALFILTER.SpatialRel = esriSpatialRelEnum.esriSpatialRelTouches
        End If
        Dim pFeatSelection As IFeatureSelection
        Dim pSelSet As ISelectionSet
        Dim b As Integer
        Dim afound As Boolean = False

        For A As Integer = 0 To pMap.LayerCount - 1
            'If pMap.Layer(A).Name = "Catastro" Then
            If pMap.Layer(A).Name = capa Then
                pFeatLayer = pMap.Layer(A)
                b = A
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra la capa Catastro para generar evaluación DM", MsgBoxStyle.Information, "[BDGEOCATMIN]")
            Exit Sub
        End If
        pFeatLayer = pMap.Layer(b)
        pMxDoc.ActiveView.Refresh()
        pFeatSelection = pFeatLayer
        pSelSet = pFeatSelection.SelectionSet
        pSelSet.Search(SPATIALFILTER, False, pFeatureCursor2)
        pfeature = pFeatureCursor2.NextFeature
        Do Until pfeature Is Nothing
            PTOPOLOG = pfeature.Shape
            pElement = New PolygonElement
            pElement.Geometry = PTOPOLOG.Buffer(0)
            'pElement.Geometry = PTOPOLOG.Buffer(b)
            SPATIALFILTER.Geometry = pElement.Geometry
            pFeatSelection.SelectFeatures(SPATIALFILTER, esriSelectionResultEnum.esriSelectionResultNew, False)
            pfeature = pFeatureCursor2.NextFeature
        Loop
        pSelSet = pFeatSelection.SelectionSet
        'Calculando los DM colindantes al Evaluado (Superpuestos y adyacentes)

        '-----------------------------------------------------------------------

        If (pSelSet.Count > 0) Then  'Si dm seleccionados calculara indicador
            Dim indexClass As Integer
            Dim pCityFClass As IFeatureClass
            pCityFClass = pFeatLayer.FeatureClass
            'pCityFClass = pFeatureLayer_cat.FeatureClass
            pFields = pCityFClass.Fields
            indexClass = pCityFClass.FindField("EVAL")
            pFeatureCursor2 = pCityFClass.Update(SPATIALFILTER, False)
            pfeature = pFeatureCursor2.NextFeature
            If tipo_seleccion_tema = "Interceptando" Then
                Do Until pfeature Is Nothing
                    pfeature.Value(indexClass) = "IN"
                    pFeatureCursor2.UpdateFeature(pfeature)
                    v_codigo_rd = pfeature.Value(pFields.FindField("CODIGOU"))
                    v_estado_rd = pfeature.Value(pFields.FindField("ESTADO"))
                    If v_estado_rd = "F" Then
                        colecciones_rd.Add(v_codigo_rd)
                    End If
                    pfeature = pFeatureCursor2.NextFeature
                Loop
            ElseIf tipo_seleccion_tema = "colidantes" Then
                Do Until pfeature Is Nothing
                    pfeature.Value(indexClass) = "CO"
                    pFeatureCursor2.UpdateFeature(pfeature)
                    pfeature = pFeatureCursor2.NextFeature
                Loop
            End If
        End If
        pFeatSelection.Clear()

    End Sub

    Public Sub selecciona_dms_demarca(ByVal capa As String)
        'Programa para seleccionar poligonos en base a una distancia -  esto a fin de calcular los DM CONLINDANTES
        '------------------------------------------------------------------------------------------------------------
        pMap = pMxDoc.FocusMap
        Dim pFeatLayer As IFeatureLayer
        Dim pFeatureCursor2 As IFeatureCursor = Nothing
        Dim pfeature As IFeature
        Dim v_codigo_rd As String
        Dim v_estado_rd As String

        Dim SPATIALFILTER As ISpatialFilter
        Dim pElement As IElement
        Dim PTOPOLOG As ITopologicalOperator
        SPATIALFILTER = New SpatialFilter

        SPATIALFILTER.SpatialRel = esriSpatialRelEnum.esriSpatialRelIndexIntersects
        
        Dim pFeatSelection As IFeatureSelection
        Dim pSelSet As ISelectionSet
        Dim b As Integer
        Dim afound As Boolean = False

        For A As Integer = 0 To pMap.LayerCount - 1
            'If pMap.Layer(A).Name = "Catastro" Then
            If pMap.Layer(A).Name = capa Then
                pFeatLayer = pMap.Layer(A)
                b = A
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra la capa  para generar la seleccion de Areas", MsgBoxStyle.Information, "[SIGCATMIN]")
            Exit Sub
        End If
        pFeatLayer = pMap.Layer(b)
        pMxDoc.ActiveView.Refresh()
        pFeatSelection = pFeatLayer
        pSelSet = pFeatSelection.SelectionSet
        pSelSet.Search(SPATIALFILTER, False, pFeatureCursor2)
        pfeature = pFeatureCursor2.NextFeature
        Do Until pfeature Is Nothing
            PTOPOLOG = pfeature.Shape
            pElement = New PolygonElement
            pElement.Geometry = PTOPOLOG.Buffer(0)
            'pElement.Geometry = PTOPOLOG.Buffer(b)
            SPATIALFILTER.Geometry = pElement.Geometry
            pFeatSelection.SelectFeatures(SPATIALFILTER, esriSelectionResultEnum.esriSelectionResultNew, False)
            pfeature = pFeatureCursor2.NextFeature
        Loop
        pSelSet = pFeatSelection.SelectionSet
        'Calculando los DM colindantes al Evaluado (Superpuestos y adyacentes)

        '-----------------------------------------------------------------------

        'If (pSelSet.Count > 0) Then  'Si dm seleccionados calculara indicador
        '    Dim indexClass As Integer
        '    Dim pCityFClass As IFeatureClass
        '    pCityFClass = pFeatLayer.FeatureClass
        '    'pCityFClass = pFeatureLayer_cat.FeatureClass
        '    pFields = pCityFClass.Fields
        '    indexClass = pCityFClass.FindField("EVAL")
        '    pFeatureCursor2 = pCityFClass.Update(SPATIALFILTER, False)
        '    pfeature = pFeatureCursor2.NextFeature
        '    If tipo_seleccion_tema = "Interceptando" Then
        '        Do Until pfeature Is Nothing
        '            pfeature.Value(indexClass) = "IN"
        '            pFeatureCursor2.UpdateFeature(pfeature)
        '            v_codigo_rd = pfeature.Value(pFields.FindField("CODIGOU"))
        '            v_estado_rd = pfeature.Value(pFields.FindField("ESTADO"))
        '            If v_estado_rd = "F" Then
        '                colecciones_rd.Add(v_codigo_rd)
        '            End If
        '            pfeature = pFeatureCursor2.NextFeature
        '        Loop
        '    ElseIf tipo_seleccion_tema = "colidantes" Then
        '        Do Until pfeature Is Nothing
        '            pfeature.Value(indexClass) = "CO"
        '            pFeatureCursor2.UpdateFeature(pfeature)
        '            pfeature = pFeatureCursor2.NextFeature
        '        Loop
        '    End If
        'End If
        'pFeatSelection.Clear()

    End Sub


    Public Sub ActualizandoRegistros(ByVal capa As String)
        'Programa para Actualizar datos a fin de calcular los DM CONLINDANTES, SUPERPUESTOS
        '------------------------------------------------------------------------------------------------------------

        Try

            Dim pFeatLayer As IFeatureLayer = Nothing
            Dim pfeature As IFeature
            Dim v_codigo_rd As String
            Dim v_estado_rd As String

            Dim afound As Boolean = False

            Dim b As Integer = 0
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = capa Then
                    pFeatLayer = pMap.Layer(A)
                    b = A
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra la capa Catastro para generar evaluación DM", MsgBoxStyle.Information, "[BDGEOCATMIN]")
                Exit Sub
            End If
            pFeatLayer = pMap.Layer(b)
            Dim pQueryFilter As IQueryFilter
            Dim seleccion_tema As String
            Dim pFeatureClass As IFeatureClass
            Dim IndexClass As String
            Dim IndeAREA As String
            pFeatureClass = pFeatLayer.FeatureClass
            Dim registroencontrado() As DataRow  'Para buscar en datatable


            Dim pFeatSelection As IFeatureSelection
            Dim psele As ISelectionSet

            For j As Integer = 1 To 3

                Dim pUpdateFeatures1 As IFeatureCursor
                pQueryFilter = New QueryFilter
                pMap = pMxDoc.FocusMap
                If j = 1 Then
                    seleccion_tema = "Superpuesto"
                ElseIf j = 2 Then
                    seleccion_tema = "Colindantes"
                ElseIf j = 3 Then
                    seleccion_tema = "Vecinos"
                End If

                If seleccion_tema = "Superpuesto" Then
                    pQueryFilter.WhereClause = lista_codigo_sup
                ElseIf seleccion_tema = "Colindantes" Then
                    pQueryFilter.WhereClause = lista_codigo_colin
                ElseIf seleccion_tema = "Vecinos" Then
                    pQueryFilter.WhereClause = "EVAL =  ''"
                End If

                ' pUpdateFeatures1 = pFeatureClass.Update(pQueryFilter, False)
                ' pfeature = pUpdateFeatures1.NextFeature
                IndexClass = pFeatureClass.FindField("EVAL")
                IndeAREA = pFeatureClass.FindField("AREA_INT")

                Dim p_areasup As Double = 0.0
                Dim contador1 As Integer = 0
                Dim codigo_bus As String
                Dim contador_area As String

                'MsgBox(colecciones_AREA_SUP.Count)
                If seleccion_tema = "Superpuesto" Then
                    If lista_codigo_sup <> "" Then

                        For contador As Integer = 1 To colecciones_AREA_SUP.Count

                            Dim SearchString As String = colecciones_AREA_SUP.Item(contador)
                            '  Dim SearchChar As String = "-"
                            Dim SearchChar As String = "."
                            Dim TestPos As Integer

                            TestPos = InStr(1, SearchString, SearchChar, CompareMethod.Text)
                            'Se colo este ultimo para obtener area sup
                            p_areasup = Mid(colecciones_AREA_SUP.Item(contador), 1, InStr(colecciones_AREA_SUP.Item(contador), "-") - 1)

                            'p_areasup = Left(colecciones_AREA_SUP.Item(contador), TestPos - 1)

                            If p_areasup > 0.0 Then
                                contador_area = Mid(colecciones_AREA_SUP.Item(contador), 1, InStr(colecciones_AREA_SUP.Item(contador), "-") - 1)
                                '  codigo_bus = Right(colecciones_AREA_SUP.Item(contador), (Len(colecciones_AREA_SUP.Item(contador)) - TestPos))
                                codigo_bus = Right(colecciones_AREA_SUP.Item(contador), (Len(colecciones_AREA_SUP.Item(contador)) - (contador_area.Length + 1)))

                                pQueryFilter = New QueryFilter
                                pQueryFilter.WhereClause = "CODIGOU = '" & codigo_bus & "' "
                                pUpdateFeatures1 = pFeatureClass.Update(pQueryFilter, True)
                                pfeature = pUpdateFeatures1.NextFeature
                                Do Until pfeature Is Nothing
                                    ' If v_estado_rd = "F" Then
                                    'colecciones_rd.Add(v_codigo_rd)
                                    'End If
                                    v_estado_rd = pfeature.Value(pUpdateFeatures1.FindField("ESTADO"))
                                    If v_estado_rd = "F" Then
                                        colecciones_rd.Add(v_codigo_rd)
                                    End If
                                    pfeature.Value(IndexClass) = "IN" 'PARA CASOS DE DM SUPERPUESTOS AL DM EVALUADO
                                    pfeature.Value(pFeatureClass.Fields.FindField("AREA_INT")) = p_areasup
                                    pUpdateFeatures1.UpdateFeature(pfeature)

                                    pfeature = pUpdateFeatures1.NextFeature
                                Loop
                            End If

                        Next contador


                    End If

                ElseIf seleccion_tema = "Colindantes" Then  'PARA CASOS DE DM COLINDANTES AL DM EVALUADO
                    If lista_codigo_colin <> "" Then
                        pFeatureClass = pFeatLayer.FeatureClass
                        ' pQueryFilter = New QueryFilter
                        pUpdateFeatures1 = pFeatureClass.Update(pQueryFilter, True)
                        pfeature = pUpdateFeatures1.NextFeature
                        Do Until pfeature Is Nothing
                            pfeature.Value(IndexClass) = "CO"
                            pfeature.Value(IndeAREA) = 0.0
                            pUpdateFeatures1.UpdateFeature(pfeature)
                            pfeature = pUpdateFeatures1.NextFeature
                        Loop
                    End If
                ElseIf seleccion_tema = "Vecinos" Then  'PARA CASOS DE DM VECINOS
                    pFeatureClass = pFeatLayer.FeatureClass
                    ' pQueryFilter = New QueryFilter
                    pUpdateFeatures1 = pFeatureClass.Update(pQueryFilter, True)
                    pfeature = pUpdateFeatures1.NextFeature
                    Do Until pfeature Is Nothing
                        If pfeature.Value(pUpdateFeatures1.FindField("CODIGOU")) = v_codigo Then
                            pfeature.Value(IndexClass) = "EV"
                        Else
                            pfeature.Value(IndexClass) = "VE"
                            pfeature.Value(IndeAREA) = 0.0
                        End If

                        pUpdateFeatures1.UpdateFeature(pfeature)
                        pfeature = pUpdateFeatures1.NextFeature
                    Loop
                End If

            Next j
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub consultacapaDM(ByVal v_codigo As String, ByVal seleccion_tema As String, ByVal p_Layer As String)
        Try
            pMap = pMxDoc.FocusMap
            Dim pFeatLayer As IFeatureLayer = Nothing
            Dim pFeatSelection As IFeatureSelection
            Dim pQueryFilter As IQueryFilter
            Dim afound As Boolean = False

            'Dim fclas_tema As IFeatureLayer
            pFeatLayer = New FeatureLayer

            If V_caso_simu = "SI" Or v_opcion_modulo = "OP_29" Or v_opcion_modulo = "OP_30" Then  ' OJO SE ESTA AGREGANDO Or v_opcion_modulo = "OP_29" Or v_opcion_modulo = "OP_30" 
                For A As Integer = 0 To pMap.LayerCount - 1
                    If pMap.Layer(A).Name.ToUpper = p_Layer.ToUpper Then
                        pFeatLayer = pMap.Layer(A)
                        pFeatureLayer = pFeatLayer
                        afound = True
                        Exit For
                    End If
                Next A
                If Not afound Then
                    MsgBox("No se encuentra el Layer")
                    Exit Sub
                End If
            Else

                If seleccion_tema = "DM" Then   'para catastro 
                    pFeatLayer = pFeatureLayer_cat
                ElseIf seleccion_tema = "Cuadric" Then
                    pFeatLayer = pFeatureLayer_boletin
                ElseIf seleccion_tema = "Simultaneot" Then
                    pFeatLayer = pFeatureLayer_cat
                ElseIf seleccion_tema = "Simultaneo_i" Then
                    pFeatLayer = pFeatureLayer_cat
                ElseIf seleccion_tema = "Simultaneo_rd" Then
                    pFeatLayer = pFeatureLayer_cat
                    'ElseIf seleccion_tema = "Acumulaciont" Then
                    '    pFeatLayer = pFeatureLayer_cat
                ElseIf seleccion_tema = "dmsim1" Then
                    pFeatLayer = pFeatureLayer_cat
                ElseIf seleccion_tema = "dmsim2" Then
                    pFeatLayer = pFeatureLayer_cat
                ElseIf seleccion_tema = "Cat_exporta" Then
                    pFeatLayer = pFeatureLayer_tmp
                ElseIf seleccion_tema = "X_reservas" Then
                    pFeatLayer = pFeatureLayer_rese

                Else
                    For A As Integer = 0 To pMap.LayerCount - 1
                        If pMap.Layer(A).Name.ToUpper = p_Layer.ToUpper Then
                            pFeatLayer = pMap.Layer(A)
                            pFeatureLayer = pFeatLayer
                            afound = True
                            Exit For
                        End If
                    Next A
                    If Not afound Then
                        MsgBox("No se encuentra el Layer")
                        Exit Sub
                    End If
                End If
            End If

            pQueryFilter = New QueryFilter
            pQFilter = New QueryFilter
            pMap = pMxDoc.FocusMap
            pFeatSelection = pFeatLayer
            If seleccion_tema = "DM" Then
                pQueryFilter.WhereClause = "CODIGOU = '" & v_codigo & "'"
            ElseIf seleccion_tema = "poligonoDM" Then
                pQueryFilter.WhereClause = "CODIGOU = '" & v_codigo & "'"
            ElseIf seleccion_tema = "Prioritarios" Then
                pQueryFilter.WhereClause = "EVAL = 'PR'"
            ElseIf seleccion_tema = "EVAL" Then  ' Para consultar solo dm evaluado
                pQueryFilter.WhereClause = "EVAL = 'EV'"
            ElseIf seleccion_tema = "Prioritarios_v" Then
                pQueryFilter.WhereClause = "EVAL = 'PR' AND CALCULO <> 'NO'"
                'pQueryFilter.WhereClause = "EVAL = 'PR' or EVAL = 'IN' AND CALCULO <> 'NO'"      'Se cambio, para considerar a los extinguidos, correo de ERamal
            ElseIf seleccion_tema = "union" Then  ' Para obtener solo el area neta
                pQueryFilter.WhereClause = "CODIGOU <> '' AND CODIGOU_1 = ''"
            ElseIf seleccion_tema = "DM_COOR" Then
                pQueryFilter.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
            ElseIf seleccion_tema = "DM_SUP" Then  'Para temas de area neta e interceptada
                'MyValue = Val(V_conta)  ' para convertir string a numero
                'pQueryFilter.WhereClause = "CONTADOR = " & MyValue & ""
            ElseIf seleccion_tema = "xdepa" Then  'Para temas de area neta e interceptada
                pQueryFilter.WhereClause = lista_depa
            ElseIf seleccion_tema = "Redenuncio" Then  'Para temas de area neta e interceptada
                pQueryFilter.WhereClause = lista_rd
                pQFilter.WhereClause = lista_rd
            ElseIf seleccion_tema = "RESERVA" Then  'Para temas de area neta e interceptada
                pQueryFilter.WhereClause = Consulta_Areas_rese
            ElseIf seleccion_tema = "URBANA" Then  'Para temas de area neta e interceptada
                pQueryFilter.WhereClause = Consulta_Areas_rese
            ElseIf seleccion_tema = "LibreDen" Then  'Para temas de libre denunciabilidad
                pQueryFilter.WhereClause = lista_codigo
            ElseIf seleccion_tema = "Catastro" Then
                pQueryFilter.WhereClause = "CODIGOU <> ''"
            ElseIf seleccion_tema = "Estado" Then
                pQueryFilter.WhereClause = "ESTADO = '" & v_codigo & "'"
            ElseIf seleccion_tema = "" Then
                pQueryFilter.WhereClause = "CODIGOU = '" & v_codigo & "'"
            ElseIf seleccion_tema = "Estado" Then
                pQueryFilter.WhereClause = "ESTADO = '" & v_estado & "'"
            ElseIf seleccion_tema = "Certificacion" Then
                pQueryFilter.WhereClause = "NOMBRE <> 'C'"
            ElseIf seleccion_tema = "LibreDenu" Then
                pQueryFilter.WhereClause = Consulta_dm_eval_libden
            ElseIf seleccion_tema = "Interceccion" Then
                pQueryFilter.WhereClause = "EVAL =  'IN'"
            ElseIf seleccion_tema = "Cat_exporta" Then
                pQueryFilter.WhereClause = "CODIGOU <>  ''"
            ElseIf seleccion_tema = "Padron" Then  'Para temas de libre denunciabilidad
                pQueryFilter.WhereClause = lista_codigo_pad
            ElseIf seleccion_tema = "Acumula" Then  'Para temas de libre denunciabilidad
                'pQueryFilter.WhereClause = "CODIGOU IS NOT NULL"
                pQueryFilter.WhereClause = "CODIGOU <> ''"
            ElseIf seleccion_tema = "rese_acum" Then  'Para temas de libre denunciabilidad
                'pQueryFilter.WhereClause = "CODIGOU IS NOT NULL"
                pQueryFilter.WhereClause = "CODIGOU <> ''"
            ElseIf seleccion_tema = "urba_acum" Then  'Para temas de libre denunciabilidad
                'pQueryFilter.WhereClause = "CODIGOU IS NOT NULL"
                pQueryFilter.WhereClause = "CODIGOU <> ''"
            ElseIf seleccion_tema = "Cuadric" Then  'Para temas de libre denunciabilidad
                pQueryFilter.WhereClause = lista_cuad_sim
            ElseIf seleccion_tema = "Cuadri" Then  'Para temas de libre denunciabilidad
                pQueryFilter.WhereClause = lista_cuad_sim
            ElseIf seleccion_tema = "Cuadrid" Then  'Para temas de libre denunciabilidad
                pQueryFilter.WhereClause = "CD_CUAD = '" & cd_cuad_sim & "'"
            ElseIf seleccion_tema = "Simultaneo" Then  'Para temas de libre denunciabilidad
                pQueryFilter.WhereClause = lista_dm_sim
            ElseIf seleccion_tema = "Simultaneot" Then  'Para temas de libre denunciabilidad
                pQueryFilter.WhereClause = lista_codigo_sim
            ElseIf seleccion_tema = "Simultaneo_rd" Then  'Para temas de libre denunciabilidad
                pQueryFilter.WhereClause = lista_codigo_sim_rd
            ElseIf seleccion_tema = "Simultaneo_i" Then  'Para temas de libre denunciabilidad
                pQueryFilter.WhereClause = lista_codigo_simu
            ElseIf seleccion_tema = "Acumulaciont" Then  'Para temas de libre denunciabilidad
                'pQueryFilter.WhereClause = lista_codigo_sim_rd 
                pQueryFilter.WhereClause = lista_codi_integracum
            ElseIf seleccion_tema = "Cuadrisi" Then  'Para temas de libre denunciabilidad
                pQueryFilter.WhereClause = lista_cod_cdcuad_simu
            ElseIf seleccion_tema = "dmsim1" Then  'Para temas de libre denunciabilidad
                pQueryFilter.WhereClause = codisim1
            ElseIf seleccion_tema = "dmsim2" Then  'Para temas de libre denunciabilidad
                pQueryFilter.WhereClause = codisim2
            ElseIf seleccion_tema = "PMA" Then  'Para temas de libre denunciabilidad
                pQueryFilter.WhereClause = lista_codigo_pma
            ElseIf seleccion_tema = "Acceditario" Then  'Para temas de libre denunciabilidad
                pQueryFilter.WhereClause = lista_codigo_acce
            ElseIf seleccion_tema = "Provincia" Then  'Para temas de libre denunciabilidad   lista_nm_depa
                pQueryFilter.WhereClause = lista_codigo_prov
            ElseIf seleccion_tema = "Departamento" Then  'Para temas de libre denunciabilidad   
                pQueryFilter.WhereClause = lista_nm_depa
            ElseIf seleccion_tema = "AREAS_RESE" Then
                ' pQueryFilter.WhereClause = "CODIGOU <> '' AND CODIGOU_1 = ''"
                pQueryFilter.WhereClause = "CODIGO <> '' or CODIGO = ''"
            ElseIf seleccion_tema = "Geologico" Then  'Para temas de libre denunciabilidad
                pQueryFilter.WhereClause = "CODIGOU = '" & lista_codigo & "'"
            ElseIf seleccion_tema = "X_capgeo" Then  'Para temas de libre denunciabilidad
                pQueryFilter.WhereClause = "CODIGO = '" & v_codigo & "'"
            ElseIf seleccion_tema = "X_reservas" Then  'Para temas de libre denunciabilidad
                pQueryFilter.WhereClause = "CODIGO <> '" & v_codigo & "'"
                '  ElseIf seleccion_tema = "X_reservas" Then  'Para temas de libre denunciabilidad
                '     pQueryFilter.WhereClause = "ARCHIVO = '" & v_archivo & "'"

            End If
            'pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, Nothing, Nothing)
            pFeatSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)

            ' pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, Nothing, Nothing)
            pFeatSelection = pFeatLayer

            pFeatureSelection = pFeatSelection
            v_adispo = pFeatSelection.SelectionSet.Count  ''''''' SE AGREGA PARA PROBAR SELECCION
            pQFilter = pQueryFilter
            If seleccion_tema = "union" Then  ' Para obtener solo el area neta
                v_adispo = pFeatSelection.SelectionSet.Count
            ElseIf seleccion_tema = "Padron" Then
                pQueryFilter_pd = New QueryFilter
                pQueryFilter_pd = pQueryFilter
            ElseIf seleccion_tema = "Simultaneo" Then
                pQueryFilter_si = New QueryFilter
                pQueryFilter_si = pQueryFilter
            ElseIf seleccion_tema = "Simultaneot" Then
                pQueryFilter_si = New QueryFilter
                pQueryFilter_si = pQueryFilter
            ElseIf seleccion_tema = "Simultaneo_rd" Then
                pQueryFilter_si = New QueryFilter
                pQueryFilter_si = pQueryFilter
            ElseIf seleccion_tema = "Simultaneo_i" Then
                pQueryFilter_si = New QueryFilter
                pQueryFilter_si = pQueryFilter
            ElseIf seleccion_tema = "Cuadrisi" Then
                pQueryFilter_si = New QueryFilter
                pQueryFilter_si = pQueryFilter
            ElseIf seleccion_tema = "Cuadric" Then
                pQueryFilter_si = New QueryFilter
                pQueryFilter_si = pQueryFilter
            ElseIf seleccion_tema = "Cuadri" Then
                pQueryFilter_si = New QueryFilter
                pQueryFilter_si = pQueryFilter
            ElseIf seleccion_tema = "Cuadrid" Then
                pQueryFilter_si = New QueryFilter
                pQueryFilter_si = pQueryFilter
            ElseIf seleccion_tema = "Acumula" Then
                pQueryFilter_si = New QueryFilter
                pQueryFilter_si = pQueryFilter
            ElseIf seleccion_tema = "PMA" Then
                pQueryFilter_pma = New QueryFilter
                pQueryFilter_pma = pQueryFilter
            ElseIf seleccion_tema = "Provincia" Then
                pQueryFilter_pma = New QueryFilter
                pQueryFilter_pma = pQueryFilter
            ElseIf seleccion_tema = "Departamento" Then
                pQueryFilter_pma = New QueryFilter
                pQueryFilter_pma = pQueryFilter
            End If
        Catch ex As Exception
            MsgBox("Error al realizar consultas a las Capas", MsgBoxStyle.Critical, "SIGCATMIN")
            Exit Sub

        End Try
    End Sub

    Public Sub consultacapaDM_sim(ByVal v_codigo As String, ByVal seleccion_tema As String, ByVal p_Layer As String)
        Try
            pMap = pMxDoc.FocusMap
            Dim pFeatLayer As IFeatureLayer = Nothing
            Dim pFeatSelection As IFeatureSelection
            Dim pQueryFilter As IQueryFilter
            Dim afound As Boolean = False
            Dim fclas_tema As IFeatureClass
            'Dim fclas_tema As IFeatureLayer
            Dim c1 As String = ""
            Dim c2 As String = ""

            If V_caso_simu = "SI" Then
                For A As Integer = 0 To pMap.LayerCount - 1
                    If pMap.Layer(A).Name.ToUpper = p_Layer.ToUpper Then
                        pFeatLayer = pMap.Layer(A)
                        pFeatureLayer = pFeatLayer
                        afound = True
                        Exit For
                    End If
                Next A
                If Not afound Then
                    MsgBox("No se encuentra el Layer")
                    Exit Sub
                End If
            Else
                If seleccion_tema = "DM" Then   'para catastro 
                    pFeatLayer = pFeatureLayer_cat
                ElseIf seleccion_tema = "Cat_exporta" Then
                    pFeatLayer = pFeatureLayer_tmp
                ElseIf seleccion_tema = "X_reservas" Then
                    pFeatLayer = pFeatureLayer_rese
                ElseIf seleccion_tema = "Cuadrid" Then
                    pFeatLayer = pFeatureLayer_tmp
                Else
                    For A As Integer = 0 To pMap.LayerCount - 1
                        If pMap.Layer(A).Name.ToUpper = p_Layer.ToUpper Then
                            pFeatLayer = pMap.Layer(A)
                            pFeatureLayer = pFeatLayer
                            afound = True
                            Exit For
                        End If
                    Next A
                    If Not afound Then
                        MsgBox("No se encuentra el Layer")
                        Exit Sub
                    End If
                End If
            End If
            pQueryFilter = New QueryFilter
            pQFilter = New QueryFilter
            pMap = pMxDoc.FocusMap
            pFeatSelection = pFeatLayer

            If seleccion_tema = "Cuadrid" Then  'Para temas de libre denunciabilidad
                pQueryFilter.WhereClause = "SUM_SHAPE_ > 0"
            End If
            pFeatSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
            pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, Nothing, Nothing)

            'Buscando los campos del tema
            fclas_tema = pFeatLayer.FeatureClass
            pFields = fclas_tema.Fields
            If Val(num_cuasim) <> "1" Then
                c1 = pFields.FindField("SUM_SHAPE_")
            Else
                c2 = pFields.FindField("Shape_area")
            End If
            pMap = pMxDoc.FocusMap
            pFeatureCursor = pFeatLayer.Search(Nothing, False)

            'Obteniendo los valores de los campos
            Dim conta_an As String
            Dim v_sum_shape As String

            pFeature = pFeatureCursor.NextFeature
            If Val(num_cuasim) <> "1" Then
                'pFeature = pFeatureCursor.NextFeature
                Do Until pFeature Is Nothing
                    conta_an = conta_an + 1
                    v_sum_shape = pFeature.Value(c1)
                    v_areasim = CInt(Int(v_sum_shape) / 10000)
                    'v_areasim = CInt(num_cuasim * 100)
                    pFeature = pFeatureCursor.NextFeature
                Loop
            Else
                'pFeature = pFeatureCursor.NextFeature
                Do Until pFeature Is Nothing
                    conta_an = conta_an + 1
                    v_sum_shape = pFeature.Value(c2)
                    v_areasim = CInt(Int(v_sum_shape) / 10000)
                    pFeature = pFeatureCursor.NextFeature
                Loop
            End If

        Catch ex As Exception
            MsgBox("Error al realizar consultas a las Capas", MsgBoxStyle.Critical, "SIGCATMIN")
            Exit Sub

        End Try
    End Sub

    Public Sub consultacapaDM_update(ByVal v_codigo As String, ByVal seleccion_tema As String, ByVal p_Layer As String)

        pMap = pMxDoc.FocusMap
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim pFeatSelection As IFeatureSelection
        Dim afound As Boolean = False

        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name.ToUpper = p_Layer.ToUpper Then
                pFeatLayer = pMap.Layer(A)
                pFeatureLayer = pFeatLayer
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra el Layer")
            Exit Sub
        End If

        pMap = pMxDoc.FocusMap
        pFeatSelection = pFeatLayer

        Dim pQFilter As IQueryFilter
        pQFilter = New QueryFilter
        Dim pUpdateFeatures As IFeatureCursor
        Dim pFeature As IFeature

        If v_integracumulacion.Rows.Count > 0 Then
            For contador As Integer = 0 To v_integracumulacion.Rows.Count - 1
                Dim codigoDM As String = ""
                codigoDM = v_integracumulacion.Rows(contador).Item("CODIGOU")

                pQFilter.WhereClause = "CODIGOU = '" & codigoDM & "'"
                'pQueryFilter.WhereClause = "CODIGOU = 'codigoDM'"

                pUpdateFeatures = pFeatureClass.Update(pQFilter, False)
                pFeature = pUpdateFeatures.NextFeature

                Select Case pFeature.Value(pUpdateFeatures.FindField("CODIGOU"))
                    Case codigoDM
                        pFeature.Value(pUpdateFeatures.FindField("EVAL")) = "XX"
                End Select
                pUpdateFeatures.UpdateFeature(pFeature)
                pFeature = pUpdateFeatures.NextFeature
            Next contador
        End If

    End Sub

    Public Sub actualizaregistrostema(ByVal tipo_consulta As String, ByVal player As String)

        'PROGRAMA PARA INSERTAR REGISTROS EN UN TEMA
        '*********************************************

        Dim pMap As IMap
        Dim pFeatLayer As IFeatureLayer
        Dim pFeatureClass As IFeatureClass
        pMap = pMxDoc.FocusMap

        Dim aFound As Boolean = False

        'Ahora se busca la capa variable

        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = player Then
                pFeatLayer = pMap.Layer(A)
                aFound = True
                Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("No existe Capa para ser evaluada", MsgBoxStyle.Information, "BDGEOCATMIN")

            Exit Sub
        End If

        pFeatureClass = pFeatLayer.FeatureClass
        Dim consulta1 As IQueryFilter

        Dim pUpdateFeatures1 As IFeatureCursor

        consulta1 = New QueryFilter
        If tipo_consulta = "XDM" Then
            consulta1.WhereClause = "CODIGOU = '" & v_codigo & "'"
        ElseIf tipo_consulta = "XVECINOS" Then
            Codigo_dm_v = ""
            consulta1.WhereClause = "EVAL = '" & Codigo_dm_v & "'"
        ElseIf tipo_consulta = "XSIMU" Then
            consulta1.WhereClause = "CODIGOU = ''"
        ElseIf tipo_consulta = "LEYENDA" Then
            consulta1.WhereClause = "EVAL = 'EV'"
        End If

        pUpdateFeatures1 = pFeatureClass.Update(consulta1, False)
        'Dim indexClass As Integer
        Dim pFeature As IFeature


        Dim c1 As String
        Dim c2 As String
        Dim c3 As String
        Dim c4 As String
        Dim c5 As String
        Dim c6 As String
        Dim c7 As String
        Dim c12 As String
        Dim c13 As String
        Dim c14 As String

        'Buscando otros campos

        c1 = pUpdateFeatures1.FindField("EVAL")
        c2 = pUpdateFeatures1.FindField("TIPO_EX")
        c3 = pUpdateFeatures1.FindField("ESTADO")
        c4 = pUpdateFeatures1.FindField("FEC_DENU")
        c5 = pUpdateFeatures1.FindField("CONCESION")
        c6 = pUpdateFeatures1.FindField("HOR_DENU")
        c7 = pUpdateFeatures1.FindField("CODIGOU")
        c12 = pUpdateFeatures1.FindField("ZONA")
        c13 = pUpdateFeatures1.FindField("CARTA")
        c14 = pUpdateFeatures1.FindField("LEYENDA1")


        pFeature = pUpdateFeatures1.NextFeature
        'Dim F2 As String

        If tipo_consulta = "XDM" Then   ' PARA CASOS QUE SOLO ES EV
            Do Until pFeature Is Nothing
                pFeature.Value(c1) = "EV"
                pUpdateFeatures1.UpdateFeature(pFeature)
                pFeature = pUpdateFeatures1.NextFeature
            Loop
        ElseIf tipo_consulta = "XVECINOS" Then  'PARA CASOS DE DM VECINOS AL DM EVALUADO
            Do Until pFeature Is Nothing
                pFeature.Value(c1) = "VE"
                pUpdateFeatures1.UpdateFeature(pFeature)
                pFeature = pUpdateFeatures1.NextFeature
            Loop
        ElseIf tipo_consulta = "LEYENDA" Then  'PARA CASOS DE LEYENDA AL TEMA

            Do Until pFeature Is Nothing
                pFeature.Value(c14) = "D.M. Evaluado"
                pUpdateFeatures1.UpdateFeature(pFeature)
                pFeature = pUpdateFeatures1.NextFeature
            Loop
        ElseIf tipo_consulta = "XSIMU" Then
            'v_estado = "P"
            'v_tipo_exp = "--"
            'nombre_dm = "DM SIMULADO"

            'Calculando fecha y hora del Sistema para el Dm simulado

            'Dim m As String
            'MyDate = Date
            'm = Format(MyDate, "yyyy-mm-dd")
            'v_fec_denun = m & " 00:00"
            'Hora = Time
            'v_hor_denun = (Hour(Hora)) & ":" & (Minute(Hora)) & ":" & (Second(Hora))

            'Do Until pFeature Is Nothing
            ' pFeature.Value(c1) = "EV"
            ' pFeature.Value(c2) = v_tipo_exp
            ' pFeature.Value(c3) = v_estado
            ' pFeature.Value(c4) = v_fec_denun
            'pFeature.Value(c5) = nombre_dm
            'pFeature.Value(c6) = v_hor_denun
            'pFeature.Value(c7) = CODIGO_DM
            'pFeature.Value(c12) = ZONA_DM
            'pFeature.Value(c13) = carta_dm
            'pUpdateFeatures1.UpdateFeature(pFeature)
            'pFeature = pUpdateFeatures1.NextFeature
            'Loop
        End If
    End Sub



    Public Sub EJECUTACRITERIOS()
        Try
            ''''''''''''''''''''''''''  Para capturar DM simultaneos '''''''''''''''''''''
            Dim lodtSimultaneo As New DataTable
            Dim dr As DataRow
            Dim lointSI As Integer = 0
            Inicializa_Datatable(lodtSimultaneo)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim c1 As String
            Dim c2 As String
            Dim c3 As String
            Dim c4 As String
            Dim c5 As String
            Dim c6 As String
            Dim c7 As String
            Dim C8 As String
            Dim C9 As String
            Dim c10 As String
            Dim c1_x As String
            Dim consulta As IQueryFilter
            consulta = New QueryFilter
            pMap = pMxDoc.FocusMap
            Dim pFeatureLayer_t As IFeatureLayer
            pFeatureLayer_t = pMap.Layer(0)
            Dim pfeatureselection As IFeatureSelection
            Dim capa_sele As ISelectionSet
            Dim pFeatureCursor As IFeatureCursor = Nothing
            Dim fclas_tema As IFeatureClass
            Dim pfeature As IFeature
            'Realizando Query
            pfeatureselection = pFeatureLayer_t
            consulta.WhereClause = "CODIGOU = '" & v_codigo & "'"
            pfeatureselection.SelectFeatures(consulta, esriSelectionResultEnum.esriSelectionResultNew, False)
            pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pfeatureselection = pFeatureLayer_t
            capa_sele = pfeatureselection.SelectionSet
            pFeatureCursor = pFeatureLayer_t.Search(consulta, True)
            fclas_tema = pFeatureLayer_t.FeatureClass
            'Buscando los campos del tema
            pFields = fclas_tema.Fields
            c1 = pFields.FindField("EVAL")
            c2 = pFields.FindField("TIPO_EX")
            c3 = pFields.FindField("ESTADO")
            c4 = pFields.FindField("FEC_DENU")
            c5 = pFields.FindField("CONCESION")
            c6 = pFields.FindField("HOR_DENU")
            c7 = pFields.FindField("FEC_LIB")
            C8 = pFields.FindField("IDENTI")
            C9 = pFields.FindField("DEMAGIS")
            pfeature = pFeatureCursor.NextFeature
            pfeature_eval = pfeature
            cls_Catastro.Redondeovertices_featureclass()   'obtiene area segun vertices redondeados
            'Obteniendo datos del Derecho Minero evaluado
            '----------------------------------------------
            'Colocando Numero segun el Estado 
            Do Until pfeature Is Nothing
                v_eval = pfeature.Value(c1)
                v_tipo_exp = pfeature.Value(c2)
                v_estado = pfeature.Value(c3)
                'Esta parte es para darle contador al estado
                If v_estado = "A" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "B" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "C" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "D" Then
                    v_estado_u_eval = 3
                ElseIf v_estado = "E" Then
                    v_estado_u_eval = 1
                ElseIf v_estado = "F" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "J" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "H" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "L" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "N" Then
                    v_estado_u_eval = 1
                ElseIf v_estado = "P" Then
                    v_estado_u_eval = 4
                ElseIf v_estado = "S" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "T" Then
                    v_estado_u_eval = 2
                ElseIf v_estado = "Y" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "K" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "Q" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "M" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "X" Then
                    v_estado_u_eval = 0.5
                ElseIf v_estado = "G" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "R" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "Z" Then
                    v_estado_u_eval = 0
                ElseIf v_estado = "V" Then
                    v_estado_u_eval = 9
                End If

                'RellenarComodin(MyDate.Year, 2, "0") & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & MyDate.Day
                If IsDate(pfeature.Value(c4)) = True Then
                    v_fec_denun = (pfeature.Value(c4)).ToString
                Else
                    'v_fec_denun = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & RellenarComodin(MyDate.Year, 2, "0").ToString
                    v_fec_denun = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"
                End If
                v_hor_denun = (pfeature.Value(c6)).ToString
                If IsDate(pfeature.Value(c4)) = True Then
                    v_fec_eval = (pfeature.Value(c4))
                Else
                    v_fec_eval = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"

                End If
                If IsDate(pfeature.Value(c7)) = True Then
                    v_fec_libdenu = (pfeature.Value(c7))
                Else
                    v_fec_libdenu = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"
                End If
                v_identi_eval = (pfeature.Value(C8)).ToString
                v_demagis = Trim(pfeature.Value(C9)).ToString
                pfeature = pFeatureCursor.NextFeature
            Loop
            pfeatureselection.Clear()  'Limpiando seleccion del tema

            'Volviendo a buscar los campos
            Dim pUpdateFeatures1 As IFeatureCursor
            fclas_tema = pFeatureLayer_t.FeatureClass
            pFields = fclas_tema.Fields
            c1 = pFields.FindField("EVAL")
            c2 = pFields.FindField("TIPO_EX")
            c3 = pFields.FindField("ESTADO")
            c4 = pFields.FindField("FEC_DENU")
            c5 = pFields.FindField("CONCESION")
            c6 = pFields.FindField("HOR_DENU")
            c7 = pFields.FindField("CODIGOU")
            C8 = pFields.FindField("FEC_LIB")
            C9 = pFields.FindField("IDENTI")
            c10 = pFields.FindField("DE_IDEN")
            Dim consulta1 As IQueryFilter
            consulta1 = New QueryFilter
            Dim pFeature_x As IFeature
            'pFeatureCursor = fclas_tema.Search(Nothing, False)
            pFeatureCursor = fclas_tema.Update(Nothing, False)
            pfeature = pFeatureCursor.NextFeature

            'Recorriendo los registros del tema de catastro minero
            Do Until pfeature Is Nothing
                v_eval_x = pfeature.Value(c1).ToString
                v_tipo_exp_x = pfeature.Value(c2).ToString
                v_estado_x = pfeature.Value(c3).ToString
                'Esta parte es para darle contador al estado
                If v_estado_x = "A" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "B" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "C" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "D" Then
                    v_estado_u = 3
                ElseIf v_estado_x = "E" Then
                    v_estado_u = 1
                ElseIf v_estado_x = "F" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "J" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "H" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "L" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "N" Then
                    v_estado_u = 1
                ElseIf v_estado_x = "P" Then
                    v_estado_u = 4
                ElseIf v_estado_x = "S" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "T" Then
                    v_estado_u = 2
                ElseIf v_estado_x = "Y" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "K" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "Q" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "M" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "X" Then
                    v_estado_u = 0.5
                ElseIf v_estado_x = "G" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "R" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "Z" Then
                    v_estado_u = 0
                ElseIf v_estado_x = "V" Then
                    v_estado_u = 9
                End If
                If IsDate(pfeature.Value(c4)) = True Then
                    v_fec_denun_x = (pfeature.Value(c4))
                Else
                    v_fec_denun_x = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"
                End If
                nombre_dm_x = (pfeature.Value(c5)).ToString
                v_hor_denun_x = (pfeature.Value(c6)).ToString
                v_codigo_x = (pfeature.Value(c7)).ToString


                If IsDate(pfeature.Value(C8)) = True Then
                    v_fec_libdenu_x = (pfeature.Value(C8))
                Else
                    v_fec_libdenu_x = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"
                End If
                v_identi_eval_x = (pfeature.Value(C9)).ToString
                v_incopora_x = (pfeature.Value(c10)).ToString
                c1_x = pFields.FindField("EVAL")
                If v_eval_x = "IN" Then  'Solo para casos que tenga esta indicador "IN"
                    If v_estado = "P" Then  'Para casos de DM que son petitorios (Estado P - DM evaluado)
                        If ((v_estado_x = "D") Or (v_estado_x = "F")) Then
                            If v_tipo_exp = "PE" Then  'Tipo de expediente del Dm evaluado --PE
                                'Asigna valor del criterio
                                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Do Until pFeature_x Is Nothing
                                    pfeature.Value(c1_x) = "PR"
                                    pFeatureCursor.UpdateFeature(pfeature)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Loop
                                End If

                            ElseIf v_tipo_exp = "RD" Then 'Tipo de expediente del Dm evaluado --RD
                                If v_estado_x = "D" Then
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PO"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If
                                ElseIf v_estado_x = "F" Then
                                    v_fec_libdenu = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "2080"
                                    If v_fec_eval < v_fec_libdenu_x Then
                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PR"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    Else
                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "EX"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    End If
                                End If
                            End If

                        ElseIf ((v_estado_x = "X") And ((v_tipo_exp_x = "PE") Or (v_tipo_exp_x = "RD"))) Then
                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                'pFeature_x = pUpdateFeatures1.NextFeature
                                'Do Until pFeature_x Is Nothing
                                pfeature.Value(c1_x) = "EX"
                                pFeatureCursor.UpdateFeature(pfeature)
                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                'pFeature_x = pUpdateFeatures1.NextFeature
                                'Loop
                            End If
                        ElseIf v_estado_x = "Y" Then
                            If v_fec_eval < v_fec_libdenu_x Then
                                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Do Until pFeature_x Is Nothing
                                    pfeature.Value(c1_x) = "PR"
                                    pFeatureCursor.UpdateFeature(pfeature)
                                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Loop
                                End If

                            Else
                                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Do Until pFeature_x Is Nothing
                                    pfeature.Value(c1_x) = "EX"
                                    pFeatureCursor.UpdateFeature(pfeature)
                                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Loop
                                End If
                            End If

                        ElseIf ((v_estado_x = "L") Or (v_estado_x = "J") Or (v_estado_x = "H")) Then
                            If v_tipo_exp = "RD" Then
                                If (v_tipo_exp_x = "PE") Then
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "EX"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If

                                Else
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "EX"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If
                                End If
                            ElseIf v_tipo_exp = "PE" Then
                                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Do Until pFeature_x Is Nothing
                                    pfeature.Value(c1_x) = "PR"
                                    pFeatureCursor.UpdateFeature(pfeature)
                                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Loop
                                End If

                            End If
                        ElseIf ((v_estado_x = "B") Or (v_estado_x = "V") Or (v_estado_x = "M") Or (v_estado_x = "G") Or (v_estado_x = "A") Or (v_estado_x = "S") Or (v_estado_x = "R")) Then
                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                'pFeature_x = pUpdateFeatures1.NextFeature
                                'Do Until pFeature_x Is Nothing
                                pfeature.Value(c1_x) = "PO"
                                pFeatureCursor.UpdateFeature(pfeature)
                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                'pFeature_x = pUpdateFeatures1.NextFeature
                                'Loop
                            End If
                            'caso1
                            'Verficando D.M. Evaluado vs sistema de cuadriculas y Sistema antiguo (Redenuncios)
                            '-----------------------------------------------------------------------------------------
                        Else
                            'CASO 1
                            If (((v_identi_eval <> "01-10") And (v_identi_eval_x <> "01-10")) Or ((v_identi_eval = "01-10") And (v_identi_eval_x = "01-10"))) Then
                                If v_estado_x <> "P" Then
                                    If v_tipo_exp = "PE" Then
                                        If ((((v_estado_x = "K") Or (v_estado_x = "Q") Or (v_estado_x = "C") Or (v_estado_x = "N") Or (v_estado_x = "E") Or (v_estado_x = "T")) And ((v_tipo_exp_x = "DN") Or (v_tipo_exp_x = "AC") Or (v_tipo_exp_x = "PE"))) Or ((v_estado_x = "X") And (v_incopora_x = "I") And ((v_tipo_exp_x = "DN") Or (v_tipo_exp_x = "AC")))) Then
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PR"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If
                                        End If

                                    ElseIf v_tipo_exp = "RD" Then
                                        If (((v_estado_x = "T") Or (v_estado_x = "X") Or (v_estado_x = "C")) And (v_tipo_exp_x = "RD")) Then
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PR"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If

                                        End If
                                    End If

                                ElseIf (v_estado_x = "P") Then
                                    'If (fecha_eval.IsNull = False) Then
                                    'If IsDBNull(v_fec_eval) = False Then
                                    If IsDate(v_fec_eval) = True Then
                                        'If IsDBNull(v_fec_denun_x.ToString) = False Then
                                        If IsDate(v_fec_denun_x) = True Then
                                            If v_fec_denun_x < v_fec_eval Then
                                                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                                    'Do Until pFeature_x Is Nothing
                                                    pfeature.Value(c1_x) = "PR"
                                                    pFeatureCursor.UpdateFeature(pfeature)
                                                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                                    'Loop
                                                End If
                                            ElseIf v_fec_denun_x > v_fec_eval Then
                                                If v_estado_x = "P" Then
                                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                                        'Do Until pFeature_x Is Nothing
                                                        pfeature.Value(c1_x) = "PO"
                                                        pFeatureCursor.UpdateFeature(pfeature)
                                                        'UpdateFeatures1.UpdateFeature(pFeature_x)
                                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                                        'Loop
                                                    End If

                                                End If

                                            ElseIf v_fec_denun_x = v_fec_eval Then
                                                If v_hor_denun_x < v_hor_denun Then
                                                    If v_estado_x = "P" Then
                                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                            'pFeature_x = pUpdateFeatures1.NextFeature

                                                            'Do Until pFeature_x Is Nothing
                                                            pfeature.Value(c1_x) = "PR"
                                                            pFeatureCursor.UpdateFeature(pfeature)
                                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                                            'Loop
                                                        End If
                                                    End If
                                                ElseIf (v_hor_denun_x > v_hor_denun) Then
                                                    If (v_estado_x = "P") Then
                                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                                            'Do Until pFeature_x Is Nothing
                                                            pfeature.Value(c1_x) = "PO"
                                                            pFeatureCursor.UpdateFeature(pfeature)
                                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                                            'Loop
                                                        End If
                                                    End If

                                                ElseIf (v_hor_denun_x = v_hor_denun) Then
                                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                                    'Do Until pFeature_x Is Nothing
                                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                        pfeature.Value(c1_x) = "SI"
                                                        pFeatureCursor.UpdateFeature(pfeature)
                                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                        'pFeature_x = pUpdateFeatures1.NextFeature

                                                        'Loop
                                                    End If

                                                End If
                                            End If
                                        Else
                                            If v_estado_u < 2 Then
                                                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                                    'Do Until pFeature_x Is Nothing
                                                    pfeature.Value(c1_x) = "PR"
                                                    pFeatureCursor.UpdateFeature(pfeature)
                                                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                                    'Loop
                                                End If
                                            Else
                                                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                    'pFeature_x = pUpdateFeatures1.NextFeature

                                                    'Do Until pFeature_x Is Nothing
                                                    pfeature.Value(c1_x) = "PO"
                                                    pFeatureCursor.UpdateFeature(pfeature)
                                                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                                    'Loop
                                                End If

                                            End If
                                        End If
                                    Else
                                        'Para caso especial del DM para tomar el valor del estado
                                        If v_estado_u < 2 Then
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature

                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PR"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If

                                        Else
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PO"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If


                                        End If
                                    End If

                                End If
                            End If   'Termino esta parte
                            'Caso 2
                            'Verificando D.M. (Redenuncio) Vs Sistemas de Cuadriculas
                            '----------------------------------------------------------
                            If ((v_identi_eval = "01-10") And (v_identi_eval_x <> "01-10")) Then
                                If ((((v_estado_x = "Q") Or (v_estado_x = "C") Or (v_estado_x = "N") Or (v_estado_x = "E")) And ((v_tipo_exp_x = "DN") Or (v_tipo_exp_x = "AC") Or (v_tipo_exp_x = "PE"))) Or ((v_estado_x = "X") And (v_incopora = "I") And ((v_tipo_exp_x = "DN") Or (v_tipo_exp_x = "AC")))) Then
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PR"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If


                                ElseIf ((v_estado_x = "P") Or (v_estado_x = "T") Or (v_estado_x = "K")) Then
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PO"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If

                                End If
                            End If
                            'Caso 3
                            'Evaluando D.M. Evaluado Petitorio VS. Redenuncio
                            '--------------------------------------------------------
                            If ((v_identi_eval <> "01-10") And (v_identi_eval_x = "01-10")) Then
                                If (((v_estado_x = "P") Or (v_estado_x = "T") Or (v_estado_x = "C") Or (v_estado_x = "X")) And (v_tipo_exp_x = "RD")) Then
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PR"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If

                                End If
                            End If
                        End If
                    Else
                        'CRITERIO DE EVALUACION PARA DM EVALUADO DIFERENTE A PETITORIO - CRITERIOS REFERENCIALES
                        '************************************************************************************
                        'Caso 1...
                        If ((v_identi_eval = "01-10") And (v_identi_eval_x <> "01-10")) Then
                            'If IsDBNull(v_fec_eval.ToString) = False Then
                            If IsDate(v_fec_eval) = True Then
                                'If IsDBNull(v_fec_denun_x.ToString) = False Then
                                If IsDate(v_fec_denun_x) = True Then

                                    'MsgBox(v_fec_denun_x, MsgBoxStyle.Critical, v_fec_eval)
                                    If v_fec_denun_x < v_fec_eval Then
                                        If ((v_estado_x = "E") Or (v_estado_x = "N")) Then

                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PR"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If

                                        Else

                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PO"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If
                                        End If

                                        'ElseIf v_fec_denun_x > fecha_eval Then
                                    ElseIf v_fec_denun_x > v_fec_eval Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PO"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                        'ElseIf (v_fec_denun_x = fecha_eval) Then
                                    ElseIf (v_fec_denun_x = v_fec_eval) Then

                                        If (v_hor_denun_x < v_hor_denun) Then
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PR"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If

                                        ElseIf (v_hor_denun_x > v_hor_denun) Then
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PO"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If


                                        ElseIf (v_hor_denun_x = v_hor_denun) Then
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "SI"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If

                                        End If
                                    End If
                                Else
                                    If v_estado_u < 2 Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PR"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    Else
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PO"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    End If
                                End If
                            Else

                                'Caso especial para tomar el valor del estado
                                If v_estado_u < 2 Then
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PR"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If

                                Else
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PO"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If

                                End If
                            End If
                        End If

                        'Caso 2 y 3...

                        If (((v_identi_eval <> "01-10") And (v_identi_eval_x <> "01-10")) Or ((v_identi_eval = "01-10") And (v_identi_eval_x = "01-10"))) Then

                            'If IsDBNull(v_fec_eval) = False Then
                            If IsDate(v_fec_eval) = True Then
                                'If IsDBNull(v_fec_denun_x) = False Then
                                If IsDate(v_fec_denun_x) = True Then

                                    If v_fec_denun_x < v_fec_eval Then
                                        'If IsNull(v_fec_denun_x) = True Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PR"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    ElseIf v_fec_denun_x > v_fec_eval Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PO"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    ElseIf v_fec_denun_x = v_fec_eval Then
                                        If v_hor_denun_x < v_hor_denun Then
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PR"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If

                                        ElseIf (v_hor_denun_x > v_hor_denun) Then
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PO"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If

                                        ElseIf (v_hor_denun_x = v_hor_denun) Then
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "SI"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If
                                        End If
                                    End If

                                Else

                                    If v_estado_u < v_estado_u_eval Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PR"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If
                                    ElseIf v_estado_u > v_estado_u_eval Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PO"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                        'Caso cuando el valor del estado es igual al valor del estado del DM evaluado

                                    ElseIf v_estado_u = v_estado_u_eval Then
                                        'v_cod_ev = Mid(v_codigo, 2, 6)
                                        v_cod_ev = Microsoft.VisualBasic.Mid(v_codigo, 2, 6)
                                        v_corre1 = IsNumeric(v_cod_ev)
                                        If v_corre1 = True Then
                                            'v_cod_ev = Mid(v_codigo, 2, 6)
                                            v_cod_ev = Microsoft.VisualBasic.Mid(v_codigo, 2, 6)

                                            v_valor1 = Val(v_cod_ev)
                                        Else
                                            'v_cod_ev = Mid(v_codigo, 2, 5)
                                            v_cod_ev = Microsoft.VisualBasic.Mid(v_codigo, 2, 5)

                                        End If
                                        'v_cod_x = Mid(v_codigo_x, 2, 6)
                                        v_cod_x = Microsoft.VisualBasic.Mid(v_codigo_x, 2, 6)
                                        v_corre2 = IsNumeric(v_cod_x)
                                        If v_corre2 = True Then
                                            'v_cod_x = Mid(v_codigo_x, 2, 6)
                                            v_cod_x = Microsoft.VisualBasic.Mid(v_codigo_x, 2, 6)
                                            v_valor2 = Val(v_cod_x)
                                        Else
                                            'v_cod_x = Mid(v_codigo_x, 2, 5)
                                            v_cod_x = Microsoft.VisualBasic.Mid(v_codigo_x, 2, 5)
                                            v_valor2 = Val(v_cod_x)
                                        End If
                                        'v_letra_ev = Mid(v_codigo, 8, 1)
                                        v_letra_ev = Microsoft.VisualBasic.Mid(v_codigo, 8, 1)

                                        'v_letra_x = Mid(v_codigo_x, 8, 1)
                                        v_letra_x = Microsoft.VisualBasic.Mid(v_codigo_x, 8, 1)

                                        If (((v_letra_ev = "X") And (v_letra_x = "Y")) Or ((v_letra_ev = "Z") And (v_letra_x = "Y")) Or ((v_letra_ev = "Z") And (v_letra_x = "X"))) Then
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PO"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                ' pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If

                                        ElseIf (((v_letra_ev = "Y") And (v_letra_x = "X")) Or ((v_letra_ev = "Y") And (v_letra_x = "Z")) Or ((v_letra_ev = "X") And (v_letra_x = "Z"))) Then
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PR"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If

                                        ElseIf (v_letra_ev = v_letra_x) Then
                                            If v_valor1 < v_valor2 Then
                                                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                                    'Do Until pFeature_x Is Nothing
                                                    pfeature.Value(c1_x) = "PR"
                                                    pFeatureCursor.UpdateFeature(pfeature)
                                                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                                    'Loop
                                                End If

                                            Else
                                                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                                    'Do Until pFeature_x Is Nothing
                                                    pfeature.Value(c1_x) = "PO"
                                                    pFeatureCursor.UpdateFeature(pfeature)
                                                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                                    'Loop
                                                End If

                                            End If
                                        End If
                                    End If
                                End If
                            Else
                                'Caso especial para tomar el valor del estado
                                If v_estado_u < v_estado_u_eval Then
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PR"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If
                                ElseIf v_estado_u > v_estado_u_eval Then
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        ' pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PO"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If


                                    'Toma el valor del estado del DM Vs valor del estado del DM evaluado
                                ElseIf v_estado_u = v_estado_u_eval Then
                                    'v_cod_ev = Mid(v_codigo, 2, 6)
                                    v_cod_ev = Microsoft.VisualBasic.Mid(v_codigo, 2, 6)

                                    v_corre1 = IsNumeric(v_cod_ev)
                                    If v_corre1 = True Then
                                        v_cod_ev = Microsoft.VisualBasic.Mid(v_codigo, 2, 6)
                                        'v_cod_ev = Mid(v_codigo, 2, 6)
                                        v_valor1 = Val(v_cod_ev)
                                    Else

                                        v_cod_ev = Microsoft.VisualBasic.Mid(v_codigo, 2, 5)

                                    End If
                                    v_cod_x = Mid(v_codigo_x, 2, 6)
                                    v_corre2 = IsNumeric(v_cod_x)
                                    If v_corre2 = True Then
                                        'v_cod_x = Mid(v_codigo_x, 2, 6)
                                        v_cod_x = Microsoft.VisualBasic.Mid(v_codigo_x, 2, 6)
                                        v_valor2 = Val(v_cod_x)
                                    Else
                                        'v_cod_x = Mid(v_codigo_x, 2, 5)
                                        v_cod_x = Microsoft.VisualBasic.Mid(v_codigo_x, 2, 5)

                                        v_valor2 = Val(v_cod_x)
                                    End If
                                    'v_letra_ev = Mid(v_codigo, 8, 1)
                                    v_letra_ev = Microsoft.VisualBasic.Mid(v_codigo, 8, 1)

                                    'v_letra_x = Mid(v_codigo_x, 8, 1)
                                    v_letra_x = Microsoft.VisualBasic.Mid(v_codigo_x, 8, 1)

                                    If (((v_letra_ev = "X") And (v_letra_x = "Y")) Or ((v_letra_ev = "Z") And (v_letra_x = "Y")) Or ((v_letra_ev = "Z") And (v_letra_x = "X"))) Then

                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PO"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    ElseIf (((v_letra_ev = "Y") And (v_letra_x = "X")) Or ((v_letra_ev = "Y") And (v_letra_x = "Z")) Or ((v_letra_ev = "X") And (v_letra_x = "Z"))) Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PR"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    ElseIf (v_letra_ev = v_letra_x) Then
                                        If v_valor1 < v_valor2 Then
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PR"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If

                                        Else
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PO"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If

                                        End If
                                    End If
                                End If
                            End If
                        End If

                        'caso 4...

                        If ((v_identi_eval <> "01-10") And (v_identi_eval_x = "01-10")) Then
                            'If IsDBNull(v_fec_eval.ToString) = False Then
                            If IsDate(v_fec_eval) = True Then
                                'If IsDBNull(v_fec_denun_x.ToString) = False Then
                                If IsDate(v_fec_denun_x) = True Then
                                    If v_fec_denun_x <= v_fec_eval Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PR"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If


                                    ElseIf (v_fec_denun_x > v_fec_eval) Then
                                        If ((v_estado = "E") Or (v_estado = "N")) Then
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PO"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If

                                        Else
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PR"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If

                                        End If
                                    ElseIf (v_fec_denun_x = v_fec_eval) Then
                                        If (v_hor_denun_x < v_hor_denun) Then
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PR"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If


                                        ElseIf (v_hor_denun_x > v_hor_denun) Then
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PO"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If

                                        ElseIf (v_hor_denun_x = v_hor_denun) Then
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "SI"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If

                                        End If
                                    End If

                                Else
                                    If v_estado_u_eval < 2 Then
                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PR"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    Else
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PO"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    End If
                                End If
                            Else
                                If v_estado_u_eval < 2 Then
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PO"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If

                                Else

                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PR"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If

                                End If
                            End If
                        End If
                    End If
                End If
                ''''''''''''''''''''  prueba DM Simultaneo  '''''''''''''''''''''''''''''
                'Inicializa_Datatable(lodtSimultaneo)
                Select Case pfeature.Value(pFields.FindField("EVAL"))
                    Case "SI"
                        ' DERECHOS MINEROS SIMULTANEOS
                        dr = lodtSimultaneo.NewRow
                        'Data_Datatable(dr, lointSI + 1, pFeature.Value(pFields.FindField("EVAL")), pFeature.Value(pFields.FindField("ESTADO")), pFeature.Value(pFields.FindField("CONCESION")), pFeature.Value(pFields.FindField("CODIGOU")))
                        Data_Datatable(dr, pfeature.Value(pFields.FindField("CONTADOR")), pfeature.Value(pFields.FindField("EVAL")), pfeature.Value(pFields.FindField("ESTADO")), pfeature.Value(pFields.FindField("CONCESION")), pfeature.Value(pFields.FindField("CODIGOU")))
                        lodtSimultaneo.Rows.Add(dr)
                        lointSI += 1
                End Select
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                pfeature = pFeatureCursor.NextFeature
            Loop
            dt_dmsi = lodtSimultaneo
        Catch ex As Exception
            'MsgBox (ex.Message)
            MsgBox("Error en el proceso de evaluación, es probable que no se haya generado correctamente la evaluación de DM", MsgBoxStyle.Critical, "BDGEOCATMIN")
        End Try

    End Sub

    Public Sub EJECUTACRITERIOS_Libredenu(ByVal player As String)
        Try
            Dim c1 As String
            Dim c2 As String
            Dim c3 As String
            Dim c4 As String
            Dim c5 As String
            Dim c6 As String
            Dim c7 As String
            Dim C8 As String
            Dim C9 As String
            Dim c10 As String
            Dim c1_x As String
            Dim consulta As IQueryFilter
            consulta = New QueryFilter
            pMap = pMxDoc.FocusMap
            Dim pFeatureLayer_t As IFeatureLayer
            'pFeatureLayer_t = pMap.Layer(0)

            Dim aFound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = player Then
                    pFeatureLayer_t = pMap.Layer(A)
                    aFound = True
                    Exit For
                End If
            Next A
            If Not aFound Then
                MsgBox("No existe Capa para ser evaluada", MsgBoxStyle.Information, "BDGEOCATMIN")

                Exit Sub
            End If


            Dim pfeatureselection As IFeatureSelection
            Dim capa_sele As ISelectionSet
            Dim pFeatureCursor As IFeatureCursor = Nothing
            Dim fclas_tema As IFeatureClass
            Dim pfeature As IFeature
            'Realizando Query
            pfeatureselection = pFeatureLayer_t
            consulta.WhereClause = "CODIGOU = '" & v_codigo & "'"
            pfeatureselection.SelectFeatures(consulta, esriSelectionResultEnum.esriSelectionResultNew, False)
            pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pfeatureselection = pFeatureLayer_t
            capa_sele = pfeatureselection.SelectionSet
            pFeatureCursor = pFeatureLayer_t.Search(consulta, True)
            fclas_tema = pFeatureLayer_t.FeatureClass
            'Buscando los campos del tema
            pFields = fclas_tema.Fields
            c1 = pFields.FindField("EVAL")
            c2 = pFields.FindField("TIPO_EX")
            c3 = pFields.FindField("ESTADO")
            c4 = pFields.FindField("FEC_DENU")
            c5 = pFields.FindField("CONCESION")
            c6 = pFields.FindField("HOR_DENU")
            c7 = pFields.FindField("FEC_LIB")
            C8 = pFields.FindField("IDENTI")
            C9 = pFields.FindField("DEMAGIS")
            pfeature = pFeatureCursor.NextFeature
            pfeature_eval = pfeature
            ' cls_Catastro.Redondeovertices_featureclass()   'obtiene area segun vertices redondeados
            'Obteniendo datos del Derecho Minero evaluado
            '----------------------------------------------
            Do Until pfeature Is Nothing
                v_eval = pfeature.Value(c1)
                v_tipo_exp = pfeature.Value(c2)
                v_estado = pfeature.Value(c3)
                'Esta parte es para darle contador al estado
                'If v_estado = "A" Then
                '    v_estado_u_eval = 9
                'ElseIf v_estado = "B" Then
                '    v_estado_u_eval = 9
                'ElseIf v_estado = "C" Then
                '    v_estado_u_eval = 9
                'ElseIf v_estado = "D" Then
                '    v_estado_u_eval = 3
                'ElseIf v_estado = "E" Then
                '    v_estado_u_eval = 1
                'ElseIf v_estado = "F" Then
                '    v_estado_u_eval = 9
                'ElseIf v_estado = "J" Then
                '    v_estado_u_eval = 9
                'ElseIf v_estado = "L" Then
                '    v_estado_u_eval = 9
                'ElseIf v_estado = "N" Then
                '    v_estado_u_eval = 1
                'ElseIf v_estado = "P" Then
                '    v_estado_u_eval = 4
                'ElseIf v_estado = "S" Then
                '    v_estado_u_eval = 9
                'ElseIf v_estado = "T" Then
                '    v_estado_u_eval = 2
                'ElseIf v_estado = "Y" Then
                '    v_estado_u_eval = 9
                'ElseIf v_estado = "K" Then
                '    v_estado_u_eval = 9
                'ElseIf v_estado = "Q" Then
                '    v_estado_u_eval = 9
                'ElseIf v_estado = "M" Then
                '    v_estado_u_eval = 9
                'ElseIf v_estado = "X" Then
                '    v_estado_u_eval = 0.5
                'ElseIf v_estado = "G" Then
                '    v_estado_u_eval = 9
                'ElseIf v_estado = "R" Then
                '    v_estado_u_eval = 9
                'ElseIf v_estado = "Z" Then
                '    v_estado_u_eval = 0
                'End If

                'RellenarComodin(MyDate.Year, 2, "0") & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & MyDate.Day
                If IsDate(pfeature.Value(c4)) = True Then
                    v_fec_denun = (pfeature.Value(c4)).ToString
                Else
                    'v_fec_denun = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & RellenarComodin(MyDate.Year, 2, "0").ToString
                    v_fec_denun = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"
                End If
                v_hor_denun = (pfeature.Value(c6)).ToString
                If IsDate(pfeature.Value(c4)) = True Then
                    v_fec_eval = (pfeature.Value(c4))
                Else
                    v_fec_eval = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"

                End If
                If IsDate(pfeature.Value(c7)) = True Then
                    v_fec_libdenu = (pfeature.Value(c7))
                Else
                    v_fec_libdenu = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"
                End If
                v_identi_eval = (pfeature.Value(C8)).ToString
                v_demagis = Trim(pfeature.Value(C9)).ToString
                pfeature = pFeatureCursor.NextFeature
            Loop
            pfeatureselection.Clear()  'Limpiando seleccion del tema

            'Volviendo a buscar los campos
            Dim pUpdateFeatures1 As IFeatureCursor
            fclas_tema = pFeatureLayer_t.FeatureClass
            pFields = fclas_tema.Fields
            c1 = pFields.FindField("EVAL")
            c2 = pFields.FindField("TIPO_EX")
            c3 = pFields.FindField("ESTADO")
            c4 = pFields.FindField("FEC_DENU")
            c5 = pFields.FindField("CONCESION")
            c6 = pFields.FindField("HOR_DENU")
            c7 = pFields.FindField("CODIGOU")
            C8 = pFields.FindField("FEC_LIB")
            C9 = pFields.FindField("IDENTI")
            c10 = pFields.FindField("DE_IDEN")
            Dim consulta1 As IQueryFilter
            consulta1 = New QueryFilter
            Dim pFeature_x As IFeature
            'pFeatureCursor = fclas_tema.Search(Nothing, False)
            pFeatureCursor = fclas_tema.Update(Nothing, False)
            pfeature = pFeatureCursor.NextFeature

            'Recorriendo los registros del tema de catastro minero
            Do Until pfeature Is Nothing
                v_eval_x = pfeature.Value(c1).ToString
                v_tipo_exp_x = pfeature.Value(c2).ToString
                v_estado_x = pfeature.Value(c3).ToString
                'Esta parte es para darle contador al estado
                'If v_estado_x = "A" Then
                '    v_estado_u = 9
                'ElseIf v_estado_x = "B" Then
                '    v_estado_u = 9
                'ElseIf v_estado_x = "C" Then
                '    v_estado_u = 9
                'ElseIf v_estado_x = "D" Then
                '    v_estado_u = 3
                'ElseIf v_estado_x = "E" Then
                '    v_estado_u = 1
                'ElseIf v_estado_x = "F" Then
                '    v_estado_u = 9
                'ElseIf v_estado_x = "J" Then
                '    v_estado_u = 9
                'ElseIf v_estado_x = "L" Then
                '    v_estado_u = 9
                'ElseIf v_estado_x = "N" Then
                '    v_estado_u = 1
                'ElseIf v_estado_x = "P" Then
                '    v_estado_u = 4
                'ElseIf v_estado_x = "S" Then
                '    v_estado_u = 9
                'ElseIf v_estado_x = "T" Then
                '    v_estado_u = 2
                'ElseIf v_estado_x = "Y" Then
                '    v_estado_u = 9
                'ElseIf v_estado_x = "K" Then
                '    v_estado_u = 9
                'ElseIf v_estado_x = "Q" Then
                '    v_estado_u = 9
                'ElseIf v_estado_x = "M" Then
                '    v_estado_u = 9
                'ElseIf v_estado_x = "X" Then
                '    v_estado_u = 0.5
                'ElseIf v_estado_x = "G" Then
                '    v_estado_u = 9
                'ElseIf v_estado_x = "R" Then
                '    v_estado_u = 9
                'ElseIf v_estado_x = "Z" Then
                '    v_estado_u = 0
                'End If
                If IsDate(pfeature.Value(c4)) = True Then
                    v_fec_denun_x = (pfeature.Value(c4))
                Else
                    v_fec_denun_x = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"
                End If
                nombre_dm_x = (pfeature.Value(c5)).ToString
                v_hor_denun_x = (pfeature.Value(c6)).ToString
                v_codigo_x = (pfeature.Value(c7)).ToString
                If IsDate(pfeature.Value(C8)) = True Then
                    v_fec_libdenu_x = (pfeature.Value(C8))
                Else
                    v_fec_libdenu_x = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"
                End If
                v_identi_eval_x = (pfeature.Value(C9)).ToString
                v_incopora_x = (pfeature.Value(c10)).ToString
                c1_x = pFields.FindField("EVAL")
                If v_eval_x = "IN" Then  'Solo para casos que tenga esta indicador "IN"
                    '    If v_estado = "P" Then  'Para casos de DM que son petitorios (Estado P - DM evaluado)
                    '        If ((v_estado_x = "D") Or (v_estado_x = "F")) Then
                    '            If v_tipo_exp = "PE" Then  'Tipo de expediente del Dm evaluado --PE
                    '                'Asigna valor del criterio
                    '                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                    'pFeature_x = pUpdateFeatures1.NextFeature
                    '                    'Do Until pFeature_x Is Nothing
                    '                    pfeature.Value(c1_x) = "PR"
                    '                    pFeatureCursor.UpdateFeature(pfeature)
                    '                    'pFeature_x = pUpdateFeatures1.NextFeature
                    '                    'Loop
                    '                End If

                    '            ElseIf v_tipo_exp = "RD" Then 'Tipo de expediente del Dm evaluado --RD
                    '                If v_estado_x = "D" Then
                    '                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                        'pFeature_x = pUpdateFeatures1.NextFeature
                    '                        'Do Until pFeature_x Is Nothing
                    '                        pfeature.Value(c1_x) = "PO"
                    '                        pFeatureCursor.UpdateFeature(pfeature)
                    '                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                        'pFeature_x = pUpdateFeatures1.NextFeature
                    '                        'Loop
                    '                    End If
                    '                ElseIf v_estado_x = "F" Then
                    '                    v_fec_libdenu = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "2080"
                    '                    If v_fec_eval < v_fec_libdenu_x Then
                    '                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                            'pFeature_x = pUpdateFeatures1.NextFeature
                    '                            'Do Until pFeature_x Is Nothing
                    '                            pfeature.Value(c1_x) = "PR"
                    '                            pFeatureCursor.UpdateFeature(pfeature)
                    '                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                            'pFeature_x = pUpdateFeatures1.NextFeature
                    '                            'Loop
                    '                        End If

                    '                    Else
                    '                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                            'pFeature_x = pUpdateFeatures1.NextFeature
                    '                            'Do Until pFeature_x Is Nothing
                    '                            pfeature.Value(c1_x) = "EX"
                    '                            pFeatureCursor.UpdateFeature(pfeature)
                    '                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                            'pFeature_x = pUpdateFeatures1.NextFeature
                    '                            'Loop
                    '                        End If

                    '                    End If
                    '                End If
                    '            End If

                    '        ElseIf ((v_estado_x = "X") And ((v_tipo_exp_x = "PE") Or (v_tipo_exp_x = "RD"))) Then
                    '            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                'pFeature_x = pUpdateFeatures1.NextFeature
                    '                'Do Until pFeature_x Is Nothing
                    '                pfeature.Value(c1_x) = "EX"
                    '                pFeatureCursor.UpdateFeature(pfeature)
                    '                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                'pFeature_x = pUpdateFeatures1.NextFeature
                    '                'Loop
                    '            End If
                    '        ElseIf v_estado_x = "Y" Then
                    '            If v_fec_eval < v_fec_libdenu_x Then
                    '                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                    'pFeature_x = pUpdateFeatures1.NextFeature
                    '                    'Do Until pFeature_x Is Nothing
                    '                    pfeature.Value(c1_x) = "PR"
                    '                    pFeatureCursor.UpdateFeature(pfeature)
                    '                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                    'pFeature_x = pUpdateFeatures1.NextFeature
                    '                    'Loop
                    '                End If

                    '            Else
                    '                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                    'pFeature_x = pUpdateFeatures1.NextFeature
                    '                    'Do Until pFeature_x Is Nothing
                    '                    pfeature.Value(c1_x) = "EX"
                    '                    pFeatureCursor.UpdateFeature(pfeature)
                    '                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                    'pFeature_x = pUpdateFeatures1.NextFeature
                    '                    'Loop
                    '                End If
                    '            End If

                    '        ElseIf ((v_estado_x = "L") Or (v_estado_x = "J")) Then
                    '            If v_tipo_exp = "RD" Then
                    '                If (v_tipo_exp_x = "PE") Then
                    '                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                        'pFeature_x = pUpdateFeatures1.NextFeature
                    '                        'Do Until pFeature_x Is Nothing
                    '                        pfeature.Value(c1_x) = "EX"
                    '                        pFeatureCursor.UpdateFeature(pfeature)
                    '                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                        'pFeature_x = pUpdateFeatures1.NextFeature
                    '                        'Loop
                    '                    End If

                    '                Else
                    '                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                        'pFeature_x = pUpdateFeatures1.NextFeature
                    '                        'Do Until pFeature_x Is Nothing
                    '                        pfeature.Value(c1_x) = "EX"
                    '                        pFeatureCursor.UpdateFeature(pfeature)
                    '                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                        'pFeature_x = pUpdateFeatures1.NextFeature
                    '                        'Loop
                    '                    End If
                    '                End If
                    '            ElseIf v_tipo_exp = "PE" Then
                    '                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                    'pFeature_x = pUpdateFeatures1.NextFeature
                    '                    'Do Until pFeature_x Is Nothing
                    '                    pfeature.Value(c1_x) = "PR"
                    '                    pFeatureCursor.UpdateFeature(pfeature)
                    '                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                    'pFeature_x = pUpdateFeatures1.NextFeature
                    '                    'Loop
                    '                End If

                    '            End If
                    '        ElseIf ((v_estado_x = "B") Or (v_estado_x = "M") Or (v_estado_x = "G") Or (v_estado_x = "A") Or (v_estado_x = "S") Or (v_estado_x = "R")) Then
                    '            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                'pFeature_x = pUpdateFeatures1.NextFeature
                    '                'Do Until pFeature_x Is Nothing
                    '                pfeature.Value(c1_x) = "PO"
                    '                pFeatureCursor.UpdateFeature(pfeature)
                    '                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                'pFeature_x = pUpdateFeatures1.NextFeature
                    '                'Loop
                    '            End If
                    '            'caso1
                    '            'Verficando D.M. Evaluado vs sistema de cuadriculas y Sistema antiguo (Redenuncios)
                    '            '-----------------------------------------------------------------------------------------
                    '        Else
                    '            If (((v_identi_eval <> "01-10") And (v_identi_eval_x <> "01-10")) Or ((v_identi_eval = "01-10") And (v_identi_eval_x = "01-10"))) Then
                    '                If v_estado_x <> "P" Then
                    '                    If v_tipo_exp = "PE" Then
                    '                        If ((((v_estado_x = "K") Or (v_estado_x = "Q") Or (v_estado_x = "C") Or (v_estado_x = "N") Or (v_estado_x = "E") Or (v_estado_x = "T")) And ((v_tipo_exp_x = "DN") Or (v_tipo_exp_x = "AC") Or (v_tipo_exp_x = "PE"))) Or ((v_estado_x = "X") And (v_incopora_x = "I") And ((v_tipo_exp_x = "DN") Or (v_tipo_exp_x = "AC")))) Then
                    '                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                                'pFeature_x = pUpdateFeatures1.NextFeature
                    '                                'Do Until pFeature_x Is Nothing
                    '                                pfeature.Value(c1_x) = "PR"
                    '                                pFeatureCursor.UpdateFeature(pfeature)
                    '                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                                'pFeature_x = pUpdateFeatures1.NextFeature
                    '                                'Loop
                    '                            End If
                    '                        End If

                    '                    ElseIf v_tipo_exp = "RD" Then
                    '                        If (((v_estado_x = "T") Or (v_estado_x = "X") Or (v_estado_x = "C")) And (v_tipo_exp_x = "RD")) Then
                    '                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                                'pFeature_x = pUpdateFeatures1.NextFeature
                    '                                'Do Until pFeature_x Is Nothing
                    '                                pfeature.Value(c1_x) = "PR"
                    '                                pFeatureCursor.UpdateFeature(pfeature)
                    '                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                                'pFeature_x = pUpdateFeatures1.NextFeature
                    '                                'Loop
                    '                            End If

                    '                        End If
                    '                    End If

                    '                ElseIf (v_estado_x = "P") Then
                    '                    'If (fecha_eval.IsNull = False) Then
                    '                    'If IsDBNull(v_fec_eval) = False Then
                    '                    If IsDate(v_fec_eval) = True Then
                    '                        'If IsDBNull(v_fec_denun_x.ToString) = False Then
                    '                        If IsDate(v_fec_denun_x) = True Then
                    '                            If v_fec_denun_x < v_fec_eval Then
                    '                                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                                    'pFeature_x = pUpdateFeatures1.NextFeature
                    '                                    'Do Until pFeature_x Is Nothing
                    '                                    pfeature.Value(c1_x) = "PR"
                    '                                    pFeatureCursor.UpdateFeature(pfeature)
                    '                                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                                    'pFeature_x = pUpdateFeatures1.NextFeature
                    '                                    'Loop
                    '                                End If
                    '                            ElseIf v_fec_denun_x > v_fec_eval Then
                    '                                If v_estado_x = "P" Then
                    '                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                                        'pFeature_x = pUpdateFeatures1.NextFeature
                    '                                        'Do Until pFeature_x Is Nothing
                    '                                        pfeature.Value(c1_x) = "PO"
                    '                                        pFeatureCursor.UpdateFeature(pfeature)
                    '                                        'UpdateFeatures1.UpdateFeature(pFeature_x)
                    '                                        'pFeature_x = pUpdateFeatures1.NextFeature
                    '                                        'Loop
                    '                                    End If

                    '                                End If

                    '                            ElseIf v_fec_denun_x = v_fec_eval Then
                    '                                If v_hor_denun_x < v_hor_denun Then
                    '                                    If v_estado_x = "P" Then
                    '                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                                            'pFeature_x = pUpdateFeatures1.NextFeature

                    '                                            'Do Until pFeature_x Is Nothing
                    '                                            pfeature.Value(c1_x) = "PR"
                    '                                            pFeatureCursor.UpdateFeature(pfeature)
                    '                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                                            'pFeature_x = pUpdateFeatures1.NextFeature
                    '                                            'Loop
                    '                                        End If
                    '                                    End If
                    '                                ElseIf (v_hor_denun_x > v_hor_denun) Then
                    '                                    If (v_estado_x = "P") Then
                    '                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                                            'pFeature_x = pUpdateFeatures1.NextFeature
                    '                                            'Do Until pFeature_x Is Nothing
                    '                                            pfeature.Value(c1_x) = "PO"
                    '                                            pFeatureCursor.UpdateFeature(pfeature)
                    '                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                                            'pFeature_x = pUpdateFeatures1.NextFeature
                    '                                            'Loop
                    '                                        End If
                    '                                    End If

                    '                                ElseIf (v_hor_denun_x = v_hor_denun) Then
                    '                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                                    'pFeature_x = pUpdateFeatures1.NextFeature
                    '                                    'Do Until pFeature_x Is Nothing
                    '                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                                        pfeature.Value(c1_x) = "SI"
                    '                                        pFeatureCursor.UpdateFeature(pfeature)
                    '                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                                        'pFeature_x = pUpdateFeatures1.NextFeature

                    '                                        'Loop
                    '                                    End If

                    '                                End If
                    '                            End If
                    '                        Else
                    '                            If v_estado_u < 2 Then
                    '                                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                                    'pFeature_x = pUpdateFeatures1.NextFeature
                    '                                    'Do Until pFeature_x Is Nothing
                    '                                    pfeature.Value(c1_x) = "PR"
                    '                                    pFeatureCursor.UpdateFeature(pfeature)
                    '                                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                                    'pFeature_x = pUpdateFeatures1.NextFeature
                    '                                    'Loop
                    '                                End If
                    '                            Else
                    '                                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                                    'pFeature_x = pUpdateFeatures1.NextFeature

                    '                                    'Do Until pFeature_x Is Nothing
                    '                                    pfeature.Value(c1_x) = "PO"
                    '                                    pFeatureCursor.UpdateFeature(pfeature)
                    '                                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                                    'pFeature_x = pUpdateFeatures1.NextFeature
                    '                                    'Loop
                    '                                End If

                    '                            End If
                    '                        End If
                    '                    Else
                    '                        'Para caso especial del DM para tomar el valor del estado
                    '                        If v_estado_u < 2 Then
                    '                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                                'pFeature_x = pUpdateFeatures1.NextFeature

                    '                                'Do Until pFeature_x Is Nothing
                    '                                pfeature.Value(c1_x) = "PR"
                    '                                pFeatureCursor.UpdateFeature(pfeature)
                    '                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                                'pFeature_x = pUpdateFeatures1.NextFeature
                    '                                'Loop
                    '                            End If

                    '                        Else
                    '                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '                                'pFeature_x = pUpdateFeatures1.NextFeature
                    '                                'Do Until pFeature_x Is Nothing
                    '                                pfeature.Value(c1_x) = "PO"
                    '                                pFeatureCursor.UpdateFeature(pfeature)
                    '                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '                                'pFeature_x = pUpdateFeatures1.NextFeature
                    '                                'Loop
                    '                            End If


                    '                        End If
                    '                    End If

                    '                End If
                    '            End If   'Termino esta parte
                    'Caso 2
                    'Verificando D.M. (Redenuncio) Vs Sistemas de Cuadriculas
                    '----------------------------------------------------------
                    'If ((v_identi_eval = "01-10") And (v_identi_eval_x <> "01-10")) Then
                    '    If ((((v_estado_x = "Q") Or (v_estado_x = "C") Or (v_estado_x = "N") Or (v_estado_x = "E")) And ((v_tipo_exp_x = "DN") Or (v_tipo_exp_x = "AC") Or (v_tipo_exp_x = "PE"))) Or ((v_estado_x = "X") And (v_incopora = "I") And ((v_tipo_exp_x = "DN") Or (v_tipo_exp_x = "AC")))) Then
                    '        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '            pFeature_x = pUpdateFeatures1.NextFeature
                    '            'Do Until pFeature_x Is Nothing
                    '            pfeature.Value(c1_x) = "PR"
                    '            pFeatureCursor.UpdateFeature(pfeature)
                    '            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '            'pFeature_x = pUpdateFeatures1.NextFeature
                    '            'Loop
                    '        End If


                    '    ElseIf ((v_estado_x = "P") Or (v_estado_x = "T") Or (v_estado_x = "K")) Then
                    '        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '            'pFeature_x = pUpdateFeatures1.NextFeature
                    '            'Do Until pFeature_x Is Nothing
                    '            pfeature.Value(c1_x) = "PO"
                    '            pFeatureCursor.UpdateFeature(pfeature)
                    '            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '            'pFeature_x = pUpdateFeatures1.NextFeature
                    '            'Loop
                    '        End If

                    '    End If
                    'End If
                    ''Caso 3
                    ''Evaluando D.M. Evaluado Petitorio VS. Redenuncio
                    ''--------------------------------------------------------
                    'If ((v_identi_eval <> "01-10") And (v_identi_eval_x = "01-10")) Then
                    '    If (((v_estado_x = "P") Or (v_estado_x = "T") Or (v_estado_x = "C") Or (v_estado_x = "X")) And (v_tipo_exp_x = "RD")) Then
                    '        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    '        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                    '            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                    '            'pFeature_x = pUpdateFeatures1.NextFeature
                    '            'Do Until pFeature_x Is Nothing
                    '            pfeature.Value(c1_x) = "PR"
                    '            pFeatureCursor.UpdateFeature(pfeature)
                    '            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                    '            'pFeature_x = pUpdateFeatures1.NextFeature
                    '            'Loop
                    '        End If

                    '    End If
                    'End If
                    '        End If
                    'Else
                    'CRITERIO DE EVALUACION PARA DM EVALUADO DIFERENTE A PETITORIO - CRITERIOS REFERENCIALES
                    '************************************************************************************
                    'Caso 1...
                    If ((v_identi_eval = "01-10") And (v_identi_eval_x <> "01-10")) Then
                        'If IsDBNull(v_fec_eval.ToString) = False Then
                        If IsDate(v_fec_eval) = True Then
                            'If IsDBNull(v_fec_denun_x.ToString) = False Then
                            If IsDate(v_fec_denun_x) = True Then

                                'MsgBox(v_fec_denun_x, MsgBoxStyle.Critical, v_fec_eval)
                                If v_fec_denun_x < v_fec_eval Then
                                    If ((v_estado_x = "E") Or (v_estado_x = "N")) Then

                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PR"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    Else

                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PO"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If
                                    End If

                                    'ElseIf v_fec_denun_x > fecha_eval Then
                                ElseIf v_fec_denun_x > v_fec_eval Then
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PO"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If

                                    'ElseIf (v_fec_denun_x = fecha_eval) Then
                                ElseIf (v_fec_denun_x = v_fec_eval) Then

                                    If (v_hor_denun_x < v_hor_denun) Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PR"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    ElseIf (v_hor_denun_x > v_hor_denun) Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PO"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If


                                    ElseIf (v_hor_denun_x = v_hor_denun) Then
                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "SI"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    End If
                                End If
                            Else
                                If v_estado_u < 2 Then
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PR"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If

                                Else
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PO"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If

                                End If
                            End If
                        Else

                            'Caso especial para tomar el valor del estado
                            If v_estado_u < 2 Then
                                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Do Until pFeature_x Is Nothing
                                    pfeature.Value(c1_x) = "PR"
                                    pFeatureCursor.UpdateFeature(pfeature)
                                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Loop
                                End If

                            Else
                                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Do Until pFeature_x Is Nothing
                                    pfeature.Value(c1_x) = "PO"
                                    pFeatureCursor.UpdateFeature(pfeature)
                                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Loop
                                End If

                            End If
                        End If
                    End If

                    'Caso 2 y 3...

                    If (((v_identi_eval <> "01-10") And (v_identi_eval_x <> "01-10")) Or ((v_identi_eval = "01-10") And (v_identi_eval_x = "01-10"))) Then

                        'If IsDBNull(v_fec_eval) = False Then
                        If IsDate(v_fec_eval) = True Then
                            'If IsDBNull(v_fec_denun_x) = False Then
                            If IsDate(v_fec_denun_x) = True Then

                                If v_fec_denun_x < v_fec_eval Then
                                    'If IsNull(v_fec_denun_x) = True Then
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PR"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If

                                ElseIf v_fec_denun_x > v_fec_eval Then
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PO"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If

                                ElseIf v_fec_denun_x = v_fec_eval Then
                                    If v_hor_denun_x < v_hor_denun Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PR"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    ElseIf (v_hor_denun_x > v_hor_denun) Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PO"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    ElseIf (v_hor_denun_x = v_hor_denun) Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "SI"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If
                                    End If
                                End If

                            Else

                                If v_estado_u < v_estado_u_eval Then
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PR"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If
                                ElseIf v_estado_u > v_estado_u_eval Then
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PO"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If

                                    'Caso cuando el valor del estado es igual al valor del estado del DM evaluado

                                ElseIf v_estado_u = v_estado_u_eval Then
                                    'v_cod_ev = Mid(v_codigo, 2, 6)
                                    v_cod_ev = Microsoft.VisualBasic.Mid(v_codigo, 2, 6)
                                    v_corre1 = IsNumeric(v_cod_ev)
                                    If v_corre1 = True Then
                                        'v_cod_ev = Mid(v_codigo, 2, 6)
                                        v_cod_ev = Microsoft.VisualBasic.Mid(v_codigo, 2, 6)

                                        v_valor1 = Val(v_cod_ev)
                                    Else
                                        'v_cod_ev = Mid(v_codigo, 2, 5)
                                        v_cod_ev = Microsoft.VisualBasic.Mid(v_codigo, 2, 5)

                                    End If
                                    'v_cod_x = Mid(v_codigo_x, 2, 6)
                                    v_cod_x = Microsoft.VisualBasic.Mid(v_codigo_x, 2, 6)
                                    v_corre2 = IsNumeric(v_cod_x)
                                    If v_corre2 = True Then
                                        'v_cod_x = Mid(v_codigo_x, 2, 6)
                                        v_cod_x = Microsoft.VisualBasic.Mid(v_codigo_x, 2, 6)
                                        v_valor2 = Val(v_cod_x)
                                    Else
                                        'v_cod_x = Mid(v_codigo_x, 2, 5)
                                        v_cod_x = Microsoft.VisualBasic.Mid(v_codigo_x, 2, 5)
                                        v_valor2 = Val(v_cod_x)
                                    End If
                                    'v_letra_ev = Mid(v_codigo, 8, 1)
                                    v_letra_ev = Microsoft.VisualBasic.Mid(v_codigo, 8, 1)

                                    'v_letra_x = Mid(v_codigo_x, 8, 1)
                                    v_letra_x = Microsoft.VisualBasic.Mid(v_codigo, 8, 1)

                                    If (((v_letra_ev = "X") And (v_letra_x = "Y")) Or ((v_letra_ev = "Z") And (v_letra_x = "Y")) Or ((v_letra_ev = "Z") And (v_letra_x = "X"))) Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PO"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            ' pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    ElseIf (((v_letra_ev = "Y") And (v_letra_x = "X")) Or ((v_letra_ev = "Y") And (v_letra_x = "Z")) Or ((v_letra_ev = "X") And (v_letra_x = "Z"))) Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PR"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    ElseIf (v_letra_ev = v_letra_x) Then
                                        If v_valor1 < v_valor2 Then
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PR"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If

                                        Else
                                            'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                                'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Do Until pFeature_x Is Nothing
                                                pfeature.Value(c1_x) = "PO"
                                                pFeatureCursor.UpdateFeature(pfeature)
                                                'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                'pFeature_x = pUpdateFeatures1.NextFeature
                                                'Loop
                                            End If

                                        End If
                                    End If
                                End If
                            End If
                        Else
                            'Caso especial para tomar el valor del estado
                            If v_estado_u < v_estado_u_eval Then
                                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Do Until pFeature_x Is Nothing
                                    pfeature.Value(c1_x) = "PR"
                                    pFeatureCursor.UpdateFeature(pfeature)
                                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Loop
                                End If
                            ElseIf v_estado_u > v_estado_u_eval Then
                                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                    ' pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Do Until pFeature_x Is Nothing
                                    pfeature.Value(c1_x) = "PO"
                                    pFeatureCursor.UpdateFeature(pfeature)
                                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Loop
                                End If


                                'Toma el valor del estado del DM Vs valor del estado del DM evaluado
                            ElseIf v_estado_u = v_estado_u_eval Then
                                'v_cod_ev = Mid(v_codigo, 2, 6)
                                v_cod_ev = Microsoft.VisualBasic.Mid(v_codigo, 2, 6)

                                v_corre1 = IsNumeric(v_cod_ev)
                                If v_corre1 = True Then
                                    v_cod_ev = Microsoft.VisualBasic.Mid(v_codigo, 2, 6)
                                    'v_cod_ev = Mid(v_codigo, 2, 6)
                                    v_valor1 = Val(v_cod_ev)
                                Else

                                    v_cod_ev = Microsoft.VisualBasic.Mid(v_codigo, 2, 5)

                                End If
                                v_cod_x = Mid(v_codigo_x, 2, 6)
                                v_corre2 = IsNumeric(v_cod_x)
                                If v_corre2 = True Then
                                    'v_cod_x = Mid(v_codigo_x, 2, 6)
                                    v_cod_x = Microsoft.VisualBasic.Mid(v_codigo_x, 2, 6)
                                    v_valor2 = Val(v_cod_x)
                                Else
                                    'v_cod_x = Mid(v_codigo_x, 2, 5)
                                    v_cod_x = Microsoft.VisualBasic.Mid(v_codigo_x, 2, 5)

                                    v_valor2 = Val(v_cod_x)
                                End If
                                'v_letra_ev = Mid(v_codigo, 8, 1)
                                v_letra_ev = Microsoft.VisualBasic.Mid(v_codigo, 8, 1)

                                'v_letra_x = Mid(v_codigo_x, 8, 1)
                                v_letra_x = Microsoft.VisualBasic.Mid(v_codigo_x, 8, 1)

                                If (((v_letra_ev = "X") And (v_letra_x = "Y")) Or ((v_letra_ev = "Z") And (v_letra_x = "Y")) Or ((v_letra_ev = "Z") And (v_letra_x = "X"))) Then

                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PO"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If

                                ElseIf (((v_letra_ev = "Y") And (v_letra_x = "X")) Or ((v_letra_ev = "Y") And (v_letra_x = "Z")) Or ((v_letra_ev = "X") And (v_letra_x = "Z"))) Then
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PR"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If

                                ElseIf (v_letra_ev = v_letra_x) Then
                                    If v_valor1 < v_valor2 Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PR"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    Else
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PO"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    End If
                                End If
                            End If
                        End If
                    End If

                    'caso 4...

                    If ((v_identi_eval <> "01-10") And (v_identi_eval_x = "01-10")) Then
                        'If IsDBNull(v_fec_eval.ToString) = False Then
                        If IsDate(v_fec_eval) = True Then
                            'If IsDBNull(v_fec_denun_x.ToString) = False Then
                            If IsDate(v_fec_denun_x) = True Then
                                If v_fec_denun_x <= v_fec_eval Then
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PR"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If


                                ElseIf (v_fec_denun_x > v_fec_eval) Then
                                    If ((v_estado = "E") Or (v_estado = "N")) Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PO"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    Else
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PR"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    End If
                                ElseIf (v_fec_denun_x = v_fec_eval) Then
                                    If (v_hor_denun_x < v_hor_denun) Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PR"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If


                                    ElseIf (v_hor_denun_x > v_hor_denun) Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "PO"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    ElseIf (v_hor_denun_x = v_hor_denun) Then
                                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                            'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Do Until pFeature_x Is Nothing
                                            pfeature.Value(c1_x) = "SI"
                                            pFeatureCursor.UpdateFeature(pfeature)
                                            'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            'pFeature_x = pUpdateFeatures1.NextFeature
                                            'Loop
                                        End If

                                    End If
                                End If

                            Else
                                If v_estado_u_eval < 2 Then
                                    consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PR"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If

                                Else
                                    'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Do Until pFeature_x Is Nothing
                                        pfeature.Value(c1_x) = "PO"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        'pFeature_x = pUpdateFeatures1.NextFeature
                                        'Loop
                                    End If

                                End If
                            End If
                        Else
                            If v_estado_u_eval < 2 Then
                                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Do Until pFeature_x Is Nothing
                                    pfeature.Value(c1_x) = "PO"
                                    pFeatureCursor.UpdateFeature(pfeature)
                                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Loop
                                End If

                            Else

                                'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                If pfeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_x Then
                                    'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Do Until pFeature_x Is Nothing
                                    pfeature.Value(c1_x) = "PR"
                                    pFeatureCursor.UpdateFeature(pfeature)
                                    'pUpdateFeatures1.UpdateFeature(pFeature_x)
                                    'pFeature_x = pUpdateFeatures1.NextFeature
                                    'Loop
                                End If

                            End If
                        End If
                    End If
                End If
                ' End If
                pfeature = pFeatureCursor.NextFeature
            Loop
        Catch ex As Exception
            'MsgBox (ex.Message)
            MsgBox("Error en el proceso de evaluación, es probable que no se haya generado correctamente la evaluación de DM", MsgBoxStyle.Critical, "BDGEOCATMIN")
        End Try
    End Sub





    Public Sub EJECUTACRITERIOS1()
        Try
            Dim c1 As String
            Dim c2 As String
            Dim c3 As String
            Dim c4 As String
            Dim c5 As String
            Dim c6 As String
            Dim c7 As String
            Dim C8 As String
            Dim C9 As String
            Dim c10 As String
            Dim c1_x As String
            Dim consulta As IQueryFilter
            consulta = New QueryFilter
            pMap = pMxDoc.FocusMap
            Dim pFeatureLayer_t As IFeatureLayer
            pFeatureLayer_t = pMap.Layer(0)
            Dim pfeatureselection As IFeatureSelection
            Dim capa_sele As ISelectionSet
            Dim pFeatureCursor As IFeatureCursor = Nothing
            Dim fclas_tema As IFeatureClass
            Dim pfeature As IFeature
            'Realizando Query
            pfeatureselection = pFeatureLayer_t
            consulta.WhereClause = "CODIGOU = '" & v_codigo & "'"
            pfeatureselection.SelectFeatures(consulta, esriSelectionResultEnum.esriSelectionResultNew, False)
            pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pfeatureselection = pFeatureLayer_t
            capa_sele = pfeatureselection.SelectionSet
            pFeatureCursor = pFeatureLayer_t.Search(consulta, True)
            fclas_tema = pFeatureLayer_t.FeatureClass
            'Buscando los campos del tema
            pFields = fclas_tema.Fields
            c1 = pFields.FindField("EVAL")
            c2 = pFields.FindField("TIPO_EX")
            c3 = pFields.FindField("ESTADO")
            c4 = pFields.FindField("FEC_DENU")
            c5 = pFields.FindField("CONCESION")
            c6 = pFields.FindField("HOR_DENU")
            c7 = pFields.FindField("FEC_LIB")
            C8 = pFields.FindField("IDENTI")
            C9 = pFields.FindField("DEMAGIS")
            pfeature = pFeatureCursor.NextFeature
            pfeature_eval = pfeature
            cls_Catastro.Redondeovertices_featureclass()   'obtiene area segun vertices redondeados
            'Obteniendo datos del Derecho Minero evaluado
            '----------------------------------------------
            Do Until pfeature Is Nothing
                v_eval = pfeature.Value(c1)
                v_tipo_exp = pfeature.Value(c2)
                v_estado = pfeature.Value(c3)
                'Esta parte es para darle contador al estado
                If v_estado = "A" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "B" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "C" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "D" Then
                    v_estado_u_eval = 3
                ElseIf v_estado = "E" Then
                    v_estado_u_eval = 1
                ElseIf v_estado = "F" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "J" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "L" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "N" Then
                    v_estado_u_eval = 1
                ElseIf v_estado = "P" Then
                    v_estado_u_eval = 4
                ElseIf v_estado = "S" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "T" Then
                    v_estado_u_eval = 2
                ElseIf v_estado = "Y" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "K" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "Q" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "M" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "X" Then
                    v_estado_u_eval = 0.5
                ElseIf v_estado = "G" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "R" Then
                    v_estado_u_eval = 9
                ElseIf v_estado = "Z" Then
                    v_estado_u_eval = 0
                End If

                'RellenarComodin(MyDate.Year, 2, "0") & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & MyDate.Day
                If IsDate(pfeature.Value(c4)) = True Then
                    v_fec_denun = (pfeature.Value(c4)).ToString
                Else
                    'v_fec_denun = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & RellenarComodin(MyDate.Year, 2, "0").ToString
                    v_fec_denun = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"
                End If
                v_hor_denun = (pfeature.Value(c6)).ToString
                If IsDate(pfeature.Value(c4)) = True Then
                    v_fec_eval = (pfeature.Value(c4))
                Else
                    v_fec_eval = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"

                End If
                If IsDate(pfeature.Value(c7)) = True Then
                    v_fec_libdenu = (pfeature.Value(c7))
                Else
                    v_fec_libdenu = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"
                End If
                v_identi_eval = (pfeature.Value(C8)).ToString
                v_demagis = Trim(pfeature.Value(C9)).ToString
                pfeature = pFeatureCursor.NextFeature
            Loop
            pfeatureselection.Clear()  'Limpiando seleccion del tema

            'Volviendo a buscar los campos
            Dim pUpdateFeatures1 As IFeatureCursor
            fclas_tema = pFeatureLayer_t.FeatureClass
            pFields = fclas_tema.Fields
            c1 = pFields.FindField("EVAL")
            c2 = pFields.FindField("TIPO_EX")
            c3 = pFields.FindField("ESTADO")
            c4 = pFields.FindField("FEC_DENU")
            c5 = pFields.FindField("CONCESION")
            c6 = pFields.FindField("HOR_DENU")
            c7 = pFields.FindField("CODIGOU")
            C8 = pFields.FindField("FEC_LIB")
            C9 = pFields.FindField("IDENTI")
            c10 = pFields.FindField("DE_IDEN")
            Dim consulta1 As IQueryFilter
            consulta1 = New QueryFilter
            Dim pFeature_x As IFeature
            pFeatureCursor = fclas_tema.Search(Nothing, False)
            pfeature = pFeatureCursor.NextFeature
            'Recorriendo los registros del tema de catastro minero
            Do Until pfeature Is Nothing
                v_eval_x = pfeature.Value(c1).ToString
                v_tipo_exp_x = pfeature.Value(c2).ToString
                v_estado_x = pfeature.Value(c3).ToString
                'Esta parte es para darle contador al estado
                If v_estado_x = "A" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "B" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "C" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "D" Then
                    v_estado_u = 3
                ElseIf v_estado_x = "E" Then
                    v_estado_u = 1
                ElseIf v_estado_x = "F" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "J" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "H" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "L" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "N" Then
                    v_estado_u = 1
                ElseIf v_estado_x = "P" Then
                    v_estado_u = 4
                ElseIf v_estado_x = "S" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "T" Then
                    v_estado_u = 2
                ElseIf v_estado_x = "Y" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "K" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "Q" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "M" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "X" Then
                    v_estado_u = 0.5
                ElseIf v_estado_x = "G" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "R" Then
                    v_estado_u = 9
                ElseIf v_estado_x = "Z" Then
                    v_estado_u = 0
                End If
                If IsDate(pfeature.Value(c4)) = True Then
                    v_fec_denun_x = (pfeature.Value(c4))
                Else
                    v_fec_denun_x = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"
                End If
                nombre_dm_x = (pfeature.Value(c5)).ToString
                v_hor_denun_x = (pfeature.Value(c6)).ToString
                v_codigo_x = (pfeature.Value(c7)).ToString
                If IsDate(pfeature.Value(C8)) = True Then
                    v_fec_libdenu_x = (pfeature.Value(C8))
                Else
                    v_fec_libdenu_x = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"
                End If
                v_identi_eval_x = (pfeature.Value(C9)).ToString
                v_incopora_x = (pfeature.Value(c10)).ToString
                c1_x = pFields.FindField("EVAL")
                If v_eval_x = "IN" Then  'Solo para casos que tenga esta indicador "IN"
                    If v_estado = "P" Then  'Para casos de DM que son petitorios (Estado P - DM evaluado)
                        If ((v_estado_x = "D") Or (v_estado_x = "F")) Then
                            If v_tipo_exp = "PE" Then  'Tipo de expediente del Dm evaluado --PE
                                'Asigna valor del criterio
                                consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                pFeature_x = pUpdateFeatures1.NextFeature
                                Do Until pFeature_x Is Nothing
                                    pFeature_x.Value(c1_x) = "PR"
                                    pUpdateFeatures1.UpdateFeature(pFeature_x)
                                    pFeature_x = pUpdateFeatures1.NextFeature
                                Loop
                            ElseIf v_tipo_exp = "RD" Then 'Tipo de expediente del Dm evaluado --RD
                                If v_estado_x = "D" Then
                                    consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    pFeature_x = pUpdateFeatures1.NextFeature
                                    Do Until pFeature_x Is Nothing
                                        pFeature_x.Value(c1_x) = "PO"
                                        pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                    Loop
                                ElseIf v_estado_x = "F" Then
                                    v_fec_libdenu = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "2080"
                                    If v_fec_eval < v_fec_libdenu_x Then
                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                        Do Until pFeature_x Is Nothing
                                            pFeature_x.Value(c1_x) = "PR"
                                            pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                        Loop
                                    Else
                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                        Do Until pFeature_x Is Nothing
                                            pFeature_x.Value(c1_x) = "EX"
                                            pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                        Loop
                                    End If
                                End If
                            End If

                        ElseIf ((v_estado_x = "X") And ((v_tipo_exp_x = "PE") Or (v_tipo_exp_x = "RD"))) Then
                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                            pFeature_x = pUpdateFeatures1.NextFeature
                            Do Until pFeature_x Is Nothing
                                pFeature_x.Value(c1_x) = "EX"
                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                pFeature_x = pUpdateFeatures1.NextFeature
                            Loop

                        ElseIf v_estado_x = "Y" Then
                            If v_fec_eval < v_fec_libdenu_x Then
                                consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                pFeature_x = pUpdateFeatures1.NextFeature
                                Do Until pFeature_x Is Nothing
                                    pFeature_x.Value(c1_x) = "PR"
                                    pUpdateFeatures1.UpdateFeature(pFeature_x)
                                    pFeature_x = pUpdateFeatures1.NextFeature
                                Loop
                            Else
                                consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                pFeature_x = pUpdateFeatures1.NextFeature
                                Do Until pFeature_x Is Nothing
                                    pFeature_x.Value(c1_x) = "EX"
                                    pUpdateFeatures1.UpdateFeature(pFeature_x)
                                    pFeature_x = pUpdateFeatures1.NextFeature
                                Loop
                            End If

                        ElseIf ((v_estado_x = "L") Or (v_estado_x = "J") Or (v_estado_x = "H")) Then
                            If v_tipo_exp = "RD" Then
                                If (v_tipo_exp_x = "PE") Then
                                    consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    pFeature_x = pUpdateFeatures1.NextFeature
                                    Do Until pFeature_x Is Nothing
                                        pFeature_x.Value(c1_x) = "EX"
                                        pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                    Loop
                                Else
                                    consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    pFeature_x = pUpdateFeatures1.NextFeature
                                    Do Until pFeature_x Is Nothing
                                        pFeature_x.Value(c1_x) = "EX"
                                        pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                    Loop
                                End If
                            ElseIf v_tipo_exp = "PE" Then
                                consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                pFeature_x = pUpdateFeatures1.NextFeature
                                Do Until pFeature_x Is Nothing
                                    pFeature_x.Value(c1_x) = "PR"
                                    pUpdateFeatures1.UpdateFeature(pFeature_x)
                                    pFeature_x = pUpdateFeatures1.NextFeature
                                Loop
                            End If
                        ElseIf ((v_estado_x = "B") Or (v_estado_x = "M") Or (v_estado_x = "G") Or (v_estado_x = "A") Or (v_estado_x = "S") Or (v_estado_x = "R")) Then
                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                            pFeature_x = pUpdateFeatures1.NextFeature
                            Do Until pFeature_x Is Nothing
                                pFeature_x.Value(c1_x) = "PO"
                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                pFeature_x = pUpdateFeatures1.NextFeature
                            Loop
                            'caso1
                            'Verficando D.M. Evaluado vs sistema de cuadriculas y Sistema antiguo (Redenuncios)
                            '-----------------------------------------------------------------------------------------
                        Else
                            If (((v_identi_eval <> "01-10") And (v_identi_eval_x <> "01-10")) Or ((v_identi_eval = "01-10") And (v_identi_eval_x = "01-10"))) Then
                                If v_estado_x <> "P" Then
                                    If v_tipo_exp = "PE" Then
                                        If ((((v_estado_x = "K") Or (v_estado_x = "Q") Or (v_estado_x = "C") Or (v_estado_x = "N") Or (v_estado_x = "E") Or (v_estado_x = "T")) And ((v_tipo_exp_x = "DN") Or (v_tipo_exp_x = "AC") Or (v_tipo_exp_x = "PE"))) Or ((v_estado_x = "X") And (v_incopora_x = "I") And ((v_tipo_exp_x = "DN") Or (v_tipo_exp_x = "AC")))) Then
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "PR"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        End If

                                    ElseIf v_tipo_exp = "RD" Then
                                        If (((v_estado_x = "T") Or (v_estado_x = "X") Or (v_estado_x = "C")) And (v_tipo_exp_x = "RD")) Then
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "PR"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        End If
                                    End If

                                ElseIf (v_estado_x = "P") Then
                                    'If (fecha_eval.IsNull = False) Then
                                    'If IsDBNull(v_fec_eval) = False Then
                                    If IsDate(v_fec_eval) = True Then
                                        'If IsDBNull(v_fec_denun_x.ToString) = False Then
                                        If IsDate(v_fec_denun_x) = True Then
                                            If v_fec_denun_x < v_fec_eval Then
                                                consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                                pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                                Do Until pFeature_x Is Nothing
                                                    pFeature_x.Value(c1_x) = "PR"
                                                    pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                    pFeature_x = pUpdateFeatures1.NextFeature
                                                Loop

                                            ElseIf v_fec_denun_x > v_fec_eval Then
                                                If v_estado_x = "P" Then
                                                    consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                                    pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                    pFeature_x = pUpdateFeatures1.NextFeature
                                                    Do Until pFeature_x Is Nothing
                                                        pFeature_x.Value(c1_x) = "PO"
                                                        pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                        pFeature_x = pUpdateFeatures1.NextFeature
                                                    Loop
                                                End If

                                            ElseIf v_fec_denun_x = v_fec_eval Then
                                                If v_hor_denun_x < v_hor_denun Then
                                                    If v_estado_x = "P" Then
                                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                                        pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                        pFeature_x = pUpdateFeatures1.NextFeature

                                                        Do Until pFeature_x Is Nothing
                                                            pFeature_x.Value(c1_x) = "PR"
                                                            pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                            pFeature_x = pUpdateFeatures1.NextFeature
                                                        Loop
                                                    End If
                                                ElseIf (v_hor_denun_x > v_hor_denun) Then
                                                    If (v_estado_x = "P") Then
                                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                                        pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                        pFeature_x = pUpdateFeatures1.NextFeature
                                                        Do Until pFeature_x Is Nothing
                                                            pFeature_x.Value(c1_x) = "PO"
                                                            pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                            pFeature_x = pUpdateFeatures1.NextFeature
                                                        Loop
                                                    End If

                                                ElseIf (v_hor_denun_x = v_hor_denun) Then
                                                    consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                                    pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                    pFeature_x = pUpdateFeatures1.NextFeature
                                                    Do Until pFeature_x Is Nothing
                                                        pFeature_x.Value(c1_x) = "SI"
                                                        pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                        pFeature_x = pUpdateFeatures1.NextFeature
                                                    Loop

                                                End If
                                            End If
                                        Else
                                            If v_estado_u < 2 Then
                                                consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                                pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                                Do Until pFeature_x Is Nothing
                                                    pFeature_x.Value(c1_x) = "PR"
                                                    pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                    pFeature_x = pUpdateFeatures1.NextFeature
                                                Loop
                                            Else
                                                consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                                pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                pFeature_x = pUpdateFeatures1.NextFeature

                                                Do Until pFeature_x Is Nothing
                                                    pFeature_x.Value(c1_x) = "PO"
                                                    pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                    pFeature_x = pUpdateFeatures1.NextFeature
                                                Loop
                                            End If
                                        End If
                                    Else
                                        'Para caso especial del DM para tomar el valor del estado
                                        If v_estado_u < 2 Then
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature

                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "PR"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        Else
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "PO"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        End If
                                    End If

                                End If
                            End If   'Termino esta parte
                            'Caso 2
                            'Verificando D.M. (Redenuncio) Vs Sistemas de Cuadriculas
                            '----------------------------------------------------------
                            If ((v_identi_eval = "01-10") And (v_identi_eval_x <> "01-10")) Then
                                If ((((v_estado_x = "Q") Or (v_estado_x = "C") Or (v_estado_x = "N") Or (v_estado_x = "E")) And ((v_tipo_exp_x = "DN") Or (v_tipo_exp_x = "AC") Or (v_tipo_exp_x = "PE"))) Or ((v_estado_x = "X") And (v_incopora = "I") And ((v_tipo_exp_x = "DN") Or (v_tipo_exp_x = "AC")))) Then
                                    consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    pFeature_x = pUpdateFeatures1.NextFeature
                                    Do Until pFeature_x Is Nothing
                                        pFeature_x.Value(c1_x) = "PR"
                                        pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                    Loop
                                ElseIf ((v_estado_x = "P") Or (v_estado_x = "T") Or (v_estado_x = "K")) Then
                                    consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    pFeature_x = pUpdateFeatures1.NextFeature
                                    Do Until pFeature_x Is Nothing
                                        pFeature_x.Value(c1_x) = "PO"
                                        pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                    Loop
                                End If
                            End If
                            'Caso 3
                            'Evaluando D.M. Evaluado Petitorio VS. Redenuncio
                            '--------------------------------------------------------
                            If ((v_identi_eval <> "01-10") And (v_identi_eval_x = "01-10")) Then
                                If (((v_estado_x = "P") Or (v_estado_x = "T") Or (v_estado_x = "C") Or (v_estado_x = "X")) And (v_tipo_exp_x = "RD")) Then
                                    consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    pFeature_x = pUpdateFeatures1.NextFeature
                                    Do Until pFeature_x Is Nothing
                                        pFeature_x.Value(c1_x) = "PR"
                                        pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                    Loop
                                End If
                            End If
                        End If
                    Else
                        'CRITERIO DE EVALUACION PARA DM EVALUADO DIFERENTE A PETITORIO - CRITERIOS REFERENCIALES
                        '************************************************************************************
                        'Caso 1...
                        If ((v_identi_eval = "01-10") And (v_identi_eval_x <> "01-10")) Then
                            'If IsDBNull(v_fec_eval.ToString) = False Then
                            If IsDate(v_fec_eval) = True Then
                                'If IsDBNull(v_fec_denun_x.ToString) = False Then
                                If IsDate(v_fec_denun_x) = True Then

                                    'MsgBox(v_fec_denun_x, MsgBoxStyle.Critical, v_fec_eval)
                                    If v_fec_denun_x < v_fec_eval Then
                                        If ((v_estado_x = "E") Or (v_estado_x = "N")) Then

                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "PR"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        Else

                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "PO"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        End If

                                        'ElseIf v_fec_denun_x > fecha_eval Then
                                    ElseIf v_fec_denun_x > v_fec_eval Then
                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                        Do Until pFeature_x Is Nothing
                                            pFeature_x.Value(c1_x) = "PO"
                                            pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                        Loop
                                        'ElseIf (v_fec_denun_x = fecha_eval) Then
                                    ElseIf (v_fec_denun_x = v_fec_eval) Then

                                        If (v_hor_denun_x < v_hor_denun) Then
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "PR"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        ElseIf (v_hor_denun_x > v_hor_denun) Then
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "PO"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop

                                        ElseIf (v_hor_denun_x = v_hor_denun) Then
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "SI"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        End If
                                    End If
                                Else
                                    If v_estado_u < 2 Then
                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                        Do Until pFeature_x Is Nothing
                                            pFeature_x.Value(c1_x) = "PR"
                                            pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                        Loop
                                    Else
                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                        Do Until pFeature_x Is Nothing
                                            pFeature_x.Value(c1_x) = "PO"
                                            pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                        Loop
                                    End If
                                End If
                            Else

                                'Caso especial para tomar el valor del estado
                                If v_estado_u < 2 Then
                                    consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    pFeature_x = pUpdateFeatures1.NextFeature
                                    Do Until pFeature_x Is Nothing
                                        pFeature_x.Value(c1_x) = "PR"
                                        pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                    Loop
                                Else
                                    consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    pFeature_x = pUpdateFeatures1.NextFeature
                                    Do Until pFeature_x Is Nothing
                                        pFeature_x.Value(c1_x) = "PO"
                                        pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                    Loop
                                End If
                            End If
                        End If

                        'Caso 2 y 3...

                        If (((v_identi_eval <> "01-10") And (v_identi_eval_x <> "01-10")) Or ((v_identi_eval = "01-10") And (v_identi_eval_x = "01-10"))) Then

                            'If IsDBNull(v_fec_eval) = False Then
                            If IsDate(v_fec_eval) = True Then
                                'If IsDBNull(v_fec_denun_x) = False Then
                                If IsDate(v_fec_denun_x) = True Then

                                    If v_fec_denun_x < v_fec_eval Then
                                        'If IsNull(v_fec_denun_x) = True Then
                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                        Do Until pFeature_x Is Nothing
                                            pFeature_x.Value(c1_x) = "PR"
                                            pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                        Loop
                                    ElseIf v_fec_denun_x > v_fec_eval Then
                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                        Do Until pFeature_x Is Nothing
                                            pFeature_x.Value(c1_x) = "PO"
                                            pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                        Loop
                                    ElseIf v_fec_denun_x = v_fec_eval Then
                                        If v_hor_denun_x < v_hor_denun Then
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "PR"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        ElseIf (v_hor_denun_x > v_hor_denun) Then
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "PO"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        ElseIf (v_hor_denun_x = v_hor_denun) Then
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "SI"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        End If
                                    End If

                                Else

                                    If v_estado_u < v_estado_u_eval Then
                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                        Do Until pFeature_x Is Nothing
                                            pFeature_x.Value(c1_x) = "PR"
                                            pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                        Loop
                                    ElseIf v_estado_u > v_estado_u_eval Then
                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                        Do Until pFeature_x Is Nothing
                                            pFeature_x.Value(c1_x) = "PO"
                                            pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                        Loop

                                        'Caso cuando el valor del estado es igual al valor del estado del DM evaluado

                                    ElseIf v_estado_u = v_estado_u_eval Then
                                        'v_cod_ev = Mid(v_codigo, 2, 6)
                                        v_cod_ev = Microsoft.VisualBasic.Mid(v_codigo, 2, 6)
                                        v_corre1 = IsNumeric(v_cod_ev)
                                        If v_corre1 = True Then
                                            'v_cod_ev = Mid(v_codigo, 2, 6)
                                            v_cod_ev = Microsoft.VisualBasic.Mid(v_codigo, 2, 6)

                                            v_valor1 = Val(v_cod_ev)
                                        Else
                                            'v_cod_ev = Mid(v_codigo, 2, 5)
                                            v_cod_ev = Microsoft.VisualBasic.Mid(v_codigo, 2, 5)

                                        End If
                                        'v_cod_x = Mid(v_codigo_x, 2, 6)
                                        v_cod_x = Microsoft.VisualBasic.Mid(v_codigo_x, 2, 6)
                                        v_corre2 = IsNumeric(v_cod_x)
                                        If v_corre2 = True Then
                                            'v_cod_x = Mid(v_codigo_x, 2, 6)
                                            v_cod_x = Microsoft.VisualBasic.Mid(v_codigo_x, 2, 6)
                                            v_valor2 = Val(v_cod_x)
                                        Else
                                            'v_cod_x = Mid(v_codigo_x, 2, 5)
                                            v_cod_x = Microsoft.VisualBasic.Mid(v_codigo_x, 2, 5)
                                            v_valor2 = Val(v_cod_x)
                                        End If
                                        'v_letra_ev = Mid(v_codigo, 8, 1)
                                        v_letra_ev = Microsoft.VisualBasic.Mid(v_codigo, 8, 1)

                                        'v_letra_x = Mid(v_codigo_x, 8, 1)
                                        v_letra_x = Microsoft.VisualBasic.Mid(v_codigo, 8, 1)

                                        If (((v_letra_ev = "X") And (v_letra_x = "Y")) Or ((v_letra_ev = "Z") And (v_letra_x = "Y")) Or ((v_letra_ev = "Z") And (v_letra_x = "X"))) Then
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "PO"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        ElseIf (((v_letra_ev = "Y") And (v_letra_x = "X")) Or ((v_letra_ev = "Y") And (v_letra_x = "Z")) Or ((v_letra_ev = "X") And (v_letra_x = "Z"))) Then
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "PR"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        ElseIf (v_letra_ev = v_letra_x) Then
                                            If v_valor1 < v_valor2 Then
                                                consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                                pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                                Do Until pFeature_x Is Nothing
                                                    pFeature_x.Value(c1_x) = "PR"
                                                    pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                    pFeature_x = pUpdateFeatures1.NextFeature
                                                Loop
                                            Else
                                                consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                                pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                                Do Until pFeature_x Is Nothing
                                                    pFeature_x.Value(c1_x) = "PO"
                                                    pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                    pFeature_x = pUpdateFeatures1.NextFeature
                                                Loop
                                            End If
                                        End If
                                    End If
                                End If
                            Else
                                'Caso especial para tomar el valor del estado
                                If v_estado_u < v_estado_u_eval Then
                                    consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    pFeature_x = pUpdateFeatures1.NextFeature
                                    Do Until pFeature_x Is Nothing
                                        pFeature_x.Value(c1_x) = "PR"
                                        pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                    Loop
                                ElseIf v_estado_u > v_estado_u_eval Then
                                    consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    pFeature_x = pUpdateFeatures1.NextFeature
                                    Do Until pFeature_x Is Nothing
                                        pFeature_x.Value(c1_x) = "PO"
                                        pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                    Loop

                                    'Toma el valor del estado del DM Vs valor del estado del DM evaluado
                                ElseIf v_estado_u = v_estado_u_eval Then
                                    'v_cod_ev = Mid(v_codigo, 2, 6)
                                    v_cod_ev = Microsoft.VisualBasic.Mid(v_codigo, 2, 6)

                                    v_corre1 = IsNumeric(v_cod_ev)
                                    If v_corre1 = True Then
                                        v_cod_ev = Microsoft.VisualBasic.Mid(v_codigo, 2, 6)
                                        'v_cod_ev = Mid(v_codigo, 2, 6)
                                        v_valor1 = Val(v_cod_ev)
                                    Else

                                        v_cod_ev = Microsoft.VisualBasic.Mid(v_codigo, 2, 5)

                                    End If
                                    v_cod_x = Mid(v_codigo_x, 2, 6)
                                    v_corre2 = IsNumeric(v_cod_x)
                                    If v_corre2 = True Then
                                        'v_cod_x = Mid(v_codigo_x, 2, 6)
                                        v_cod_x = Microsoft.VisualBasic.Mid(v_codigo_x, 2, 6)
                                        v_valor2 = Val(v_cod_x)
                                    Else
                                        'v_cod_x = Mid(v_codigo_x, 2, 5)
                                        v_cod_x = Microsoft.VisualBasic.Mid(v_codigo_x, 2, 5)

                                        v_valor2 = Val(v_cod_x)
                                    End If
                                    'v_letra_ev = Mid(v_codigo, 8, 1)
                                    v_letra_ev = Microsoft.VisualBasic.Mid(v_codigo, 8, 1)

                                    'v_letra_x = Mid(v_codigo_x, 8, 1)
                                    v_letra_x = Microsoft.VisualBasic.Mid(v_codigo_x, 8, 1)

                                    If (((v_letra_ev = "X") And (v_letra_x = "Y")) Or ((v_letra_ev = "Z") And (v_letra_x = "Y")) Or ((v_letra_ev = "Z") And (v_letra_x = "X"))) Then

                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                        Do Until pFeature_x Is Nothing
                                            pFeature_x.Value(c1_x) = "PO"
                                            pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                        Loop
                                    ElseIf (((v_letra_ev = "Y") And (v_letra_x = "X")) Or ((v_letra_ev = "Y") And (v_letra_x = "Z")) Or ((v_letra_ev = "X") And (v_letra_x = "Z"))) Then
                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                        Do Until pFeature_x Is Nothing
                                            pFeature_x.Value(c1_x) = "PR"
                                            pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                        Loop
                                    ElseIf (v_letra_ev = v_letra_x) Then
                                        If v_valor1 < v_valor2 Then
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "PR"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        Else
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "PO"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        End If
                                    End If
                                End If
                            End If
                        End If

                        'caso 4...

                        If ((v_identi_eval <> "01-10") And (v_identi_eval_x = "01-10")) Then
                            'If IsDBNull(v_fec_eval.ToString) = False Then
                            If IsDate(v_fec_eval) = True Then
                                'If IsDBNull(v_fec_denun_x.ToString) = False Then
                                If IsDate(v_fec_denun_x) = True Then
                                    If v_fec_denun_x <= v_fec_eval Then
                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                        Do Until pFeature_x Is Nothing
                                            pFeature_x.Value(c1_x) = "PR"
                                            pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                        Loop

                                    ElseIf (v_fec_denun_x > v_fec_eval) Then
                                        If ((v_estado = "E") Or (v_estado = "N")) Then
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "PO"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        Else
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "PR"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        End If
                                    ElseIf (v_fec_denun_x = v_fec_eval) Then
                                        If (v_hor_denun_x < v_hor_denun) Then
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "PR"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop

                                        ElseIf (v_hor_denun_x > v_hor_denun) Then
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "PO"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        ElseIf (v_hor_denun_x = v_hor_denun) Then
                                            consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                            pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                            Do Until pFeature_x Is Nothing
                                                pFeature_x.Value(c1_x) = "SI"
                                                pUpdateFeatures1.UpdateFeature(pFeature_x)
                                                pFeature_x = pUpdateFeatures1.NextFeature
                                            Loop
                                        End If
                                    End If

                                Else
                                    If v_estado_u_eval < 2 Then
                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                        Do Until pFeature_x Is Nothing
                                            pFeature_x.Value(c1_x) = "PR"
                                            pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                        Loop
                                    Else
                                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                        pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                        Do Until pFeature_x Is Nothing
                                            pFeature_x.Value(c1_x) = "PO"
                                            pUpdateFeatures1.UpdateFeature(pFeature_x)
                                            pFeature_x = pUpdateFeatures1.NextFeature
                                        Loop
                                    End If
                                End If
                            Else
                                If v_estado_u_eval < 2 Then
                                    consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    pFeature_x = pUpdateFeatures1.NextFeature
                                    Do Until pFeature_x Is Nothing
                                        pFeature_x.Value(c1_x) = "PO"
                                        pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                    Loop
                                Else

                                    consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                                    pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                                    pFeature_x = pUpdateFeatures1.NextFeature
                                    Do Until pFeature_x Is Nothing
                                        pFeature_x.Value(c1_x) = "PR "
                                        pUpdateFeatures1.UpdateFeature(pFeature_x)
                                        pFeature_x = pUpdateFeatures1.NextFeature
                                    Loop
                                End If
                            End If
                        End If
                    End If
                End If
                pfeature = pFeatureCursor.NextFeature
            Loop
        Catch ex As Exception
            'MsgBox (ex.Message)
            MsgBox("Error en el proceso de evaluación, es probable que no se haya generado correctamente la evaluación de DM", MsgBoxStyle.Critical, "BDGEOCATMIN")
        End Try

    End Sub




    Public Sub obtienelimitesmaximos(ByVal player As String)
        'Programa para obtener Limite extremos del DM
        Dim v_este_min1 As Double : Dim v_este_max1 As Double
        Dim v_norte_min1 As Double : Dim v_norte_max1 As Double
        Dim pFeatureCursor As IFeatureCursor
        Dim pfeature As IFeature
        Dim pgeometria As IGeometry
        Dim pEnvelope As IEnvelope
        Dim pActiveView As IActiveView
        Dim pFeatLayer As IFeatureLayer = Nothing
        Try
            pMap = pMxDoc.FocusMap
            pActiveView = pMap
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = player Then
                    pFeatLayer = pMxDoc.FocusMap.Layer(A)
                    pFeatLayer.Visible = True
                    afound = True : Exit For
                End If
            Next A
            If Not afound Then
                'MsgBox("Layer No Existe.") : Exit Sub
                pFeatLayer = pFeatureLayer_cat  ' toma el total de catastro
            End If
            'pFeatLayer = pFeatureLayer_cat
            ' Exit Sub

            pFeatureCursor = pFeatLayer.Search(Nothing, False)
            pfeature = pFeatureCursor.NextFeature
            pgeometria = pfeature.Shape
            pEnvelope = pgeometria.Envelope
            v_este_min1 = pEnvelope.XMin : v_este_max1 = pEnvelope.XMax
            v_norte_min1 = pEnvelope.YMin : v_norte_max1 = pEnvelope.YMax
            v_este_min = v_este_min1 : v_este_max = v_este_max1
            v_norte_min = v_norte_min1 : v_norte_max = v_norte_max1
        Catch ex As Exception
            MsgBox("No Existe el DM Consultado en la Base Grafica", MsgBoxStyle.Critical, "[BDGEOCATMIN]")
        End Try

    End Sub

    Function f_Intercepta_temas(ByVal loFeature As String, ByVal v_este_min1 As Double, ByVal v_norte_min1 As Double, ByVal v_este_max1 As Double, ByVal v_norte_max1 As Double, ByVal p_App As IApplication, Optional ByVal p_ShapeFile_Out As String = "")
        Try
            Dim pFLayer As IFeatureLayer
            pMap = pMxDoc.FocusMap
            If loFeature = "Distrito" Then
                pFLayer = pFeatureLayer_dist
            ElseIf loFeature = "Zona Reservada" Then
                'pFLayer = pFeatureLayer_reseg
                pFLayer = pFeatureLayer_rese
            ElseIf loFeature = "Zona Urbana" Then
                pFLayer = pFeatureLayer_urba
            ElseIf loFeature = "Drenaje" Then
                pFLayer = pFeatureLayer
            ElseIf loFeature = "Vias" Then
                pFLayer = pFeatureLayer
            ElseIf loFeature = "Limite Frontera" Then
                pFLayer = pFeatureLayer
            ElseIf loFeature = "Cuadrangulo" Then
                pFLayer = pFeatureLayer_hoja
            ElseIf loFeature = "Departamento" Then
                pFLayer = pFeatureLayer_depa
            ElseIf loFeature = "Provincia" Then
                pFLayer = pFeatureLayer_prov
            ElseIf loFeature = "Geo_bol100" Then
                pFLayer = pFeatureLayer_Geo_bol100
            ElseIf loFeature = "Geo_fran50" Then
                pFLayer = pFeatureLayer_Geo_fran50
            ElseIf loFeature = "Geo_fran100" Then
                pFLayer = pFeatureLayer_Geo_fran100
            ElseIf loFeature = "Boletin" Then
                pFLayer = pFeatureLayer_boletin
            End If

            '******************* se agrega para acumulacion, division ...  ******************************************
            Dim pgeometria As IGeometry
            If v_opcion_modulo = "OP_29" Or v_opcion_modulo = "OP_30" Then
                Dim pFeatLayer As IFeatureLayer = Nothing
                Dim fclas_tema As IFeatureClass
                'Dim pgeometria As IGeometry
                Dim pfeature As IFeature
                pMap = pMxDoc.FocusMap
                Dim afound As Boolean = False
                If v_opcion_modulo = "OP_29" Then
                    For A As Integer = 0 To pMap.LayerCount - 1
                        If pMap.Layer(A).Name = "Acumulacion" Then
                            pFeatLayer = pMxDoc.FocusMap.Layer(A)
                            afound = True
                            Exit For
                        End If
                    Next A
                ElseIf v_opcion_modulo = "OP_30" Then
                    For A As Integer = 0 To pMap.LayerCount - 1
                        If pMap.Layer(A).Name = "Division" Then
                            pFeatLayer = pMxDoc.FocusMap.Layer(A)
                            afound = True
                            Exit For
                        End If
                    Next A
                End If
                fclas_tema = pFeatLayer.FeatureClass

                Dim peaturecursor As IFeatureCursor
                peaturecursor = fclas_tema.Search(Nothing, False)
                pfeature = peaturecursor.NextFeature

                pgeometria = pfeature.Shape
            End If
                '************************************************************************************************

                Dim pActiveView As IActiveView
                Dim pDisplayTransform As IDisplayTransformation
                Dim pEnvelope As IEnvelope
                'pMxDoc = p_App.Document
                pActiveView = pMxDoc.FocusMap
                pDisplayTransform = pActiveView.ScreenDisplay.DisplayTransformation
                pEnvelope = pDisplayTransform.VisibleBounds

                'Aquí calculo el Extend
                ' pEnvelope.SetEmpty()
                pEnvelope.XMin = v_este_min1
                pEnvelope.YMin = v_norte_min1
                pEnvelope.XMax = v_este_max1
                pEnvelope.YMax = v_norte_max1
                pDisplayTransform.VisibleBounds = pEnvelope
                'pActiveView.Refresh()
                Dim pSpatialFilter As ISpatialFilter
                pSpatialFilter = New SpatialFilter
                If v_opcion_modulo = "OP_29" Or v_opcion_modulo = "OP_30" Then
                    With pSpatialFilter
                        .Geometry = pgeometria
                        .SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
                    End With
                Else
                    With pSpatialFilter
                        .Geometry = pEnvelope
                        .SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
                    End With
                End If

                '******************
                'Seleccionar los Registros Interceptados
                Dim pFeatSelection As IFeatureSelection
                Dim pSelectionSet As ISelectionSet
                pFeatSelection = pFLayer
                Dim cod_carta As String

                pFeatSelection.SelectFeatures(pSpatialFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
                pSelectionSet = pFeatSelection.SelectionSet

                Cuenta_int = pSelectionSet.Count

                'MsgBox(isel.Count & "" & pFLayer.Name
                If loFeature = "Catastro" Then
                    'Expor_Tema(p_ShapeFile_Out, p_App)
                Else
                    Dim pFeatureCursor As IFeatureCursor
                    pFeatureCursor = pFLayer.Search(pSpatialFilter, True)
                    Dim pRow As IRow
                    pRow = pFeatureCursor.NextFeature
                    If pRow Is Nothing Then
                        'MsgBox(".:: No Existe Intercepción ::.", MsgBoxStyle.Information, "BDGEOCATMIN")
                        Return ""
                        Exit Function
                    End If
                    Dim lostr_Join_Codigos As String = ""
                    Do Until pRow Is Nothing
                        If pRow.Value(1).ToString <> "" Then
                            Select Case loFeature
                                Case "Catastro"
                                    lostr_Join_Codigos = lostr_Join_Codigos & pRow.Value(pRow.Fields.FindField("OBJECTID")) & ","
                                Case "Departamento"
                                    If caso_consulta = "CARTA IGN" Or caso_consulta = "DEMARCACION POLITICA" Or caso_consulta = "CATASTRO MINERO" Then
                                        lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("CD_DEPA")) & "',"
                                        ''lostr_Join_Codigos1 = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("NM_DEPA")) & "',"
                                        colecciones_cd_depa.Add(pRow.Value(pRow.Fields.FindField("CD_DEPA")))
                                        colecciones_depa.Add(pRow.Value(pRow.Fields.FindField("NM_DEPA")))
                                    Else
                                        If pRow.Value(pRow.Fields.FindField("CD_DEPA")) <> 99 Then
                                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("CD_DEPA")) & "',"
                                            'lostr_Join_Codigos1 = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("NM_DEPA")) & "',"
                                            colecciones_cd_depa.Add(pRow.Value(pRow.Fields.FindField("CD_DEPA")))
                                            colecciones_depa.Add(pRow.Value(pRow.Fields.FindField("NM_DEPA")))
                                        End If
                                    End If
                                Case "Geo_bol100"
                                    lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("OBJECTID")) & "',"
                                    colecciones_cd_bol_geologia.Add(pRow.Value(pRow.Fields.FindField("OBJECTID")))

                                Case "Geo_fran100"
                                    ' If caso_consulta = "MAPA GEOLOGICO" Then
                                    'lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("OBJECTID")) & "',"

                                    'colecciones_cd_goelogia_100.Add(pRow.Value(pRow.Fields.FindField("OBJECTID")))


                                    Select Case arch_cata

                                        Case "Geologia_fran100_buf"
                                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("OBJECTID")) & "',"
                                            colecciones_cd_goelogia_100_buf.Add(pRow.Value(pRow.Fields.FindField("OBJECTID")))
                                        Case "Geologia_fran100"

                                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("OBJECTID")) & "',"
                                            '  cod_carta = pRow.Value(pRow.Fields.FindField("OBJECTID"))
                                            colecciones_cd_goelogia_100.Add(pRow.Value(pRow.Fields.FindField("OBJECTID")))

                                            colecciones_car_goelogia_100.Add(pRow.Value(pRow.Fields.FindField("QDR_CODIGO")))

                                            ' colecciones_car_goelogia_50.Add(pRow.Value(pRow.Fields.FindField("QDR_CODIGO")))
                                            'colecciones_depa.Add(pRow.Value(pRow.Fields.FindField("NM_DEPA")))
                                            'End If
                                    End Select


                                    'colecciones_depa.Add(pRow.Value(pRow.Fields.FindField("NM_DEPA")))
                                    'End If
                                Case "Geo_fran50"
                                    Select Case arch_cata

                                        Case "Geologia_fran50_buf"
                                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("OBJECTID")) & "',"
                                            colecciones_cd_goelogia_50_buf.Add(pRow.Value(pRow.Fields.FindField("OBJECTID")))
                                        Case "Geologia_fran50"

                                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("OBJECTID")) & "',"
                                            '  cod_carta = pRow.Value(pRow.Fields.FindField("OBJECTID"))
                                            colecciones_cd_goelogia_50.Add(pRow.Value(pRow.Fields.FindField("OBJECTID")))

                                            colecciones_car_goelogia_50.Add(pRow.Value(pRow.Fields.FindField("QDR_CODIGO")))

                                            ' colecciones_car_goelogia_50.Add(pRow.Value(pRow.Fields.FindField("QDR_CODIGO")))
                                            'colecciones_depa.Add(pRow.Value(pRow.Fields.FindField("NM_DEPA")))
                                            'End If
                                    End Select

                                Case "Boletin"
                                    'Almacena el nro boletin y titulo en la coleccion para llenar en el plano
                                    lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("NUMERO_BOL")) & "',"

                                    colecciones_boletin.Add(pRow.Value(pRow.Fields.FindField("NUMERO_BOL")) & "-" & pRow.Value(pRow.Fields.FindField("TITULO")))
                                Case "Provincia"
                                    lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("OBJECTID")) & "',"
                                    colecciones_prov.Add(pRow.Value(pRow.Fields.FindField("NM_PROV")))
                                Case "Distrito"
                                    'lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("OBJECTID")) & "',"
                                    'colecciones_dist.Add(pRow.Value(pRow.Fields.FindField("NM_DIST")))
                                    If caso_consulta = "CARTA IGN" Or caso_consulta = "DEMARCACION POLITICA" Or caso_consulta = "CATASTRO MINERO" Then

                                        lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("CD_DIST")) & "',"
                                        colecciones_cd_depa.Add(pRow.Value(pRow.Fields.FindField("CD_DEPA")))
                                        colecciones_depa.Add(pRow.Value(pRow.Fields.FindField("NM_DEPA")))
                                        colecciones_prov.Add(pRow.Value(pRow.Fields.FindField("NM_PROV")))

                                        colecciones_dist.Add(pRow.Value(pRow.Fields.FindField("NM_DIST")))
                                        'MsgBox(pRow.Value(pRow.Fields.FindField("NM_DIST")))

                                    Else
                                        If pRow.Value(pRow.Fields.FindField("CD_DEPA")) <> 99 Then
                                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("CD_DEPA")) & "',"
                                            colecciones_cd_depa.Add(pRow.Value(pRow.Fields.FindField("CD_DEPA")))
                                            ' colecciones_depa.Add(pRow.Value(pRow.Fields.FindField("NM_DEPA")))
                                            'colecciones_prov.Add(pRow.Value(pRow.Fields.FindField("NM_PROV")))
                                            'colecciones_dist.Add(pRow.Value(pRow.Fields.FindField("NM_DIST")))
                                        End If
                                    End If
                                Case "Zona Urbana"
                                    lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("NM_URBA")) & "',"
                                    colecciones_urba.Add(pRow.Value(pRow.Fields.FindField("NM_URBA")))
                                    colecciones_codurba.Add(pRow.Value(pRow.Fields.FindField("CODIGO")))
                                Case "Zona Reservada"
                                    lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("NM_RESE")) & "',"
                                    colecciones_rese.Add(pRow.Value(pRow.Fields.FindField("NM_RESE")))
                                Case "Limite Frontera"
                                    lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("NOMBRE")) & "',"
                                    colecciones_front.Add(pRow.Value(pRow.Fields.FindField("NOMBRE")))
                                Case "Cuadrangulo"
                                    lostr_Join_Codigos = pRow.Value(pRow.Fields.FindField("CD_HOJA"))
                                    colecciones.Add(lostr_Join_Codigos)
                                    colecciones_nmhojas.Add(pRow.Value(pRow.Fields.FindField("NM_HOJA")))
                                Case "Cuadrangulo_1"
                                    lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("CD_HOJA")) & "',"
                                Case Else
                                    lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(1) & "',"

                            End Select
                            pRow = pFeatureCursor.NextFeature
                        End If
                    Loop

                    Select Case loFeature
                        Case "Departamento"
                            lostr_Join_Codigos = "CD_DEPA IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                        Case "Geo_fran100"
                            lostr_Join_Codigos = "OBJECTID IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                        Case "Geo_fran50"
                            lostr_Join_Codigos = "OBJECTID IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                        Case "Geo_bol100"
                            lostr_Join_Codigos = "OBJECTID IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                        Case "Provincia"
                            lostr_Join_Codigos = "OBJECTID IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                        Case "Distrito"
                            lostr_Join_Codigos = "OBJECTID IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                        Case "Zona Urbana"
                            lostr_Join_Codigos = "NM_URBA IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                            'lostr_Join_Codigos_zu = "CODIGO IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                        Case "Zona Reservada"
                            lostr_Join_Codigos = "NM_RESE IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"

                        Case "Limite Frontera"

                            lostr_Join_Codigos = "NOMBRE IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"


                        Case "Cuadrangulo"
                            'lostr_Join_Codigos = "CD_HOJA IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                            '   colecciones.Add(lostr_Join_Codigos)
                            'MsgBox(colecciones.Items(1).Value)
                        Case "Cuadrangulo_1"
                            lostr_Join_Codigos = "CD_HOJA IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                        Case "Boletin"
                            lostr_Join_Codigos = "NUMERO_BOL IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                        Case Else
                            lostr_Join_Codigos = "OBJECTID IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                    End Select
                    Return lostr_Join_Codigos
                    pFeatSelection.Clear()
                End If
        Catch ex As Exception
            MsgBox("Error en interceccion...", MsgBoxStyle.Critical, "BDGEOCATMIN")

        End Try

    End Function
    'Function f_Intercepta_temas(ByVal loFeature As String, ByVal v_este_min As Double, ByVal v_norte_min As Double, ByVal v_este_max As Double, ByVal v_norte_max As Double, ByVal p_App As IApplication, Optional ByVal p_ShapeFile_Out As String = "")
    '    Dim pFLayer As IFeatureLayer = Nothing
    '    'pMxDoc = p_App.Document
    '    pMap = pMxDoc.FocusMap
    '    Dim afound As Boolean
    '    For A As Integer = 0 To pMap.LayerCount - 1
    '        If pMap.Layer(A).Name = loFeature Then
    '            pFLayer = pMxDoc.FocusMap.Layer(A)
    '            pFLayer.Visible = False
    '            afound = True
    '            Exit For
    '        End If
    '    Next A
    '    If Not afound Then
    '        MsgBox("Layer No Existe.")
    '        Return ""
    '        Exit Function
    '    End If
    '    Dim pActiveView As IActiveView
    '    Dim pDisplayTransform As IDisplayTransformation
    '    Dim pEnvelope As IEnvelope
    '    'pMxDoc = p_App.Document
    '    pActiveView = pMxDoc.FocusMap
    '    pDisplayTransform = pActiveView.ScreenDisplay.DisplayTransformation
    '    pEnvelope = pDisplayTransform.VisibleBounds
    '    'Aquí calculo el Extend
    '    pEnvelope.SetEmpty()
    '    pEnvelope.XMin = v_este_min
    '    pEnvelope.YMin = v_norte_min
    '    pEnvelope.XMax = v_este_max
    '    pEnvelope.YMax = v_norte_max
    '    pDisplayTransform.VisibleBounds = pEnvelope
    '    'pActiveView.Refresh()
    '    Dim pSpatialFilter As ISpatialFilter
    '    pSpatialFilter = New SpatialFilter
    '    With pSpatialFilter
    '        .Geometry = pEnvelope
    '        .SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects ' esriSpatialRelIntersects
    '    End With
    '    '******************
    '    'Seleccionar los Registros Interceptados
    '    Dim pFeatSelection As IFeatureSelection
    '    Dim pSelectionSet As ISelectionSet
    '    pFeatSelection = pFLayer
    '    'MsgBox(pFeatSelection.SelectionSet.Count)
    '    pFeatSelection.SelectFeatures(pSpatialFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
    '    pSelectionSet = pFeatSelection.SelectionSet
    '    'MsgBox(isel.Count & "" & pFLayer.Name
    '    If loFeature = "Catastro" Then
    '        'Expor_Tema(p_ShapeFile_Out, p_App)
    '    Else
    '        Dim pFeatureCursor As IFeatureCursor
    '        pFeatureCursor = pFLayer.Search(pSpatialFilter, True)
    '        Dim pRow As IRow
    '        pRow = pFeatureCursor.NextFeature
    '        If pRow Is Nothing Then
    '            'MsgBox(".:: No Existe Intercepción ::.", MsgBoxStyle.Information, "BDGEOCATMIN")
    '            Return ""
    '            Exit Function
    '        End If
    '        Dim lostr_Join_Codigos As String = ""
    '        Do Until pRow Is Nothing
    '            If pRow.Value(1).ToString <> "" Then
    '                Select Case loFeature
    '                    Case "Catastro"
    '                        lostr_Join_Codigos = lostr_Join_Codigos & pRow.Value(pRow.Fields.FindField("OBJECTID")) & ","
    '                    Case "Departamento"
    '                        If caso_consulta = "CARTA IGN" Or caso_consulta = "DEMARCACION POLITICA" Then
    '                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("CD_DEPA")) & "',"
    '                            ''lostr_Join_Codigos1 = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("NM_DEPA")) & "',"
    '                            colecciones_cd_depa.Add(pRow.Value(pRow.Fields.FindField("CD_DEPA")))
    '                            colecciones_depa.Add(pRow.Value(pRow.Fields.FindField("NM_DEPA")))
    '                        Else
    '                            If pRow.Value(pRow.Fields.FindField("CD_DEPA")) <> 99 Then
    '                                lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("CD_DEPA")) & "',"
    '                                'lostr_Join_Codigos1 = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("NM_DEPA")) & "',"
    '                                colecciones_cd_depa.Add(pRow.Value(pRow.Fields.FindField("CD_DEPA")))
    '                                colecciones_depa.Add(pRow.Value(pRow.Fields.FindField("NM_DEPA")))
    '                            End If
    '                        End If

    '                    Case "Provincia"
    '                        lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("OBJECTID")) & "',"
    '                        colecciones_prov.Add(pRow.Value(pRow.Fields.FindField("NM_PROV")))
    '                    Case "Distrito"
    '                        lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("OBJECTID")) & "',"
    '                        colecciones_dist.Add(pRow.Value(pRow.Fields.FindField("NM_DIST")))
    '                    Case "Zona Urbana"
    '                        lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("NM_URBA")) & "',"
    '                        colecciones_urba.Add(pRow.Value(pRow.Fields.FindField("NM_URBA")))
    '                    Case "Area Reservada"
    '                        lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("NM_RESE")) & "',"
    '                        colecciones_rese.Add(pRow.Value(pRow.Fields.FindField("NM_RESE")))
    '                    Case "Cuadrangulo"
    '                        lostr_Join_Codigos = pRow.Value(pRow.Fields.FindField("CD_HOJA"))
    '                        colecciones.Add(lostr_Join_Codigos)
    '                        colecciones_nmhojas.Add(pRow.Value(pRow.Fields.FindField("NM_HOJA")))
    '                    Case Else
    '                        lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(1) & "',"

    '                End Select
    '                pRow = pFeatureCursor.NextFeature
    '            End If
    '        Loop
    '        Select Case loFeature
    '            Case "Departamento"
    '                lostr_Join_Codigos = "CD_DEPA IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
    '            Case "Provincia"
    '                lostr_Join_Codigos = "OBJECTID IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
    '            Case "Distrito"
    '                lostr_Join_Codigos = "OBJECTID IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
    '            Case "Zona Urbana"
    '                lostr_Join_Codigos = "NM_URBA IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
    '            Case "Area Reservada"
    '                lostr_Join_Codigos = "NM_RESE IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
    '            Case "Cuadrangulo"
    '                'lostr_Join_Codigos = "CD_HOJA IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
    '                '   colecciones.Add(lostr_Join_Codigos)
    '            Case Else
    '                lostr_Join_Codigos = "OBJECTID IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
    '        End Select
    '        Return lostr_Join_Codigos
    '        pFeatSelection.Clear()
    '    End If
    'End Function

    Function f_Intercepta_temas_X(ByVal loFeature As String, ByVal v_este_min As Double, ByVal v_norte_min As Double, ByVal v_este_max As Double, ByVal v_norte_max As Double, ByVal p_App As IApplication, Optional ByVal p_ShapeFile_Out As String = "")
        Dim pFLayer As IFeatureLayer = Nothing
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        Dim afound As Boolean
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = loFeature Then
                pFLayer = pMxDoc.FocusMap.Layer(A)
                pFLayer.Visible = True
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("Layer No Existe.")
            Return ""
            Exit Function
        End If
        Dim pActiveView As IActiveView
        Dim pDisplayTransform As IDisplayTransformation
        Dim pEnvelope As IEnvelope
        pMxDoc = p_App.Document
        pActiveView = pMxDoc.FocusMap
        pDisplayTransform = pActiveView.ScreenDisplay.DisplayTransformation
        pEnvelope = pDisplayTransform.VisibleBounds
        'Aquí calculo el Extend
        pEnvelope.SetEmpty()
        pEnvelope.XMin = v_este_min
        pEnvelope.YMin = v_norte_min
        pEnvelope.XMax = v_este_max
        pEnvelope.YMax = v_norte_max

        pDisplayTransform.VisibleBounds = pEnvelope
        'pActiveView.Refresh()
        Dim pSpatialFilter As ISpatialFilter
        pSpatialFilter = New SpatialFilter
        With pSpatialFilter
            .Geometry = pEnvelope
            .SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects ' esriSpatialRelIntersects
        End With

        '******************
        'Seleccionar los Registros Interceptados
        Dim pFeatSelection As IFeatureSelection
        Dim pSelectionSet As ISelectionSet
        pFeatSelection = pFLayer
        pFeatSelection.SelectFeatures(pSpatialFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        pSelectionSet = pFeatSelection.SelectionSet
        'MsgBox(isel.Count & "" & pFLayer.Name
        If loFeature = "Catastro" Then
            'Expor_Tema(p_ShapeFile_Out, p_App)
        Else
            Dim pFeatureCursor As IFeatureCursor
            pFeatureCursor = pFLayer.Search(pSpatialFilter, True)
            Dim pRow As IRow
            pRow = pFeatureCursor.NextFeature
            If pRow Is Nothing Then
                'MsgBox(".:: No Existe Intercepción ::.", MsgBoxStyle.Information, "BDGEOCATMIN")
                Return ""
                Exit Function
            End If
            Dim lostr_Join_Codigos As String = ""
            'Dim x As New Collection
            Do Until pRow Is Nothing
                If pRow.Value(1).ToString <> "" Then
                    Select Case loFeature
                        Case "Catastro"
                            lostr_Join_Codigos = lostr_Join_Codigos & pRow.Value(pRow.Fields.FindField("OBJECTID")) & ","
                        Case "Departamento"
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("NM_DEPA")) & "',"
                            colecciones_depa.Add(pRow.Value(pRow.Fields.FindField("NM_DEPA")))
                        Case "Provincia"
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("OBJECTID")) & "',"
                            colecciones_prov.Add(pRow.Value(pRow.Fields.FindField("NM_PROV")))
                        Case "Distrito"
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("OBJECTID")) & "',"
                            colecciones_dist.Add(pRow.Value(pRow.Fields.FindField("NM_DIST")))
                        Case "Zona Urbana"
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("NM_URBA")) & "',"
                            colecciones_urba.Add(pRow.Value(pRow.Fields.FindField("NM_URBA")))
                        Case "Zona Reservada"
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("NM_RESE")) & "',"
                            colecciones_rese.Add(pRow.Value(pRow.Fields.FindField("NM_RESE")))
                        Case "Hojas"
                            lostr_Join_Codigos = pRow.Value(pRow.Fields.FindField("CD_HOJA"))
                            colecciones.Add(lostr_Join_Codigos)
                            colecciones_nmhojas.Add(pRow.Value(pRow.Fields.FindField("NM_HOJA")))
                        Case Else
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(1) & "',"
                    End Select
                    pRow = pFeatureCursor.NextFeature
                End If
            Loop
            Select Case loFeature
                Case "Departamento"
                    lostr_Join_Codigos = "NM_DEPA IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                Case "Provincia"
                    lostr_Join_Codigos = "OBJECTID IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                Case "Distrito"
                    lostr_Join_Codigos = "OBJECTID IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                Case "Zona Urbana"
                    lostr_Join_Codigos = "NM_URBA IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                Case "Zona Reservada"
                    lostr_Join_Codigos = "NM_RESE IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                Case "Hojas"
                    'lostr_Join_Codigos = "CD_HOJA IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                    '   colecciones.Add(lostr_Join_Codigos)
                Case Else
                    lostr_Join_Codigos = "OBJECTID IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
            End Select
            Return lostr_Join_Codigos
            pFeatSelection.Clear()
        End If
    End Function

    Public Sub DefinitionExpressiontema(ByVal p_Nombre_Archivo As String, ByVal p_App As IApplication, ByVal Nom_Shapefile As String)
        'Dim pMxApp As IMxApplication
        'pMxApp = p_App
        Dim pFeatLayer As IFeatureLayer = Nothing
        v_existe = False
        Try

            If v_codigo = "" Then Exit Sub
            Dim v_Tema As String
            Select Case Nom_Shapefile
                Case "Departamento"
                    v_Tema = "Departamento"
                    pFeatLayer = pFeatureLayer_depa
                Case "Provincia"
                    v_Tema = "Provincia"
                    pFeatLayer = pFeatureLayer_prov
                Case "Distrito"
                    v_Tema = "Distrito"
                    pFeatLayer = pFeatureLayer_dist
                    pFeatLayer.ShowTips = True
                Case "Zona Reservada"
                    v_Tema = "Zona Reservada"
                    pFeatLayer = pFeatureLayer_reseg
                Case "Zona Urbana"
                    v_Tema = "Zona Urbana"
                    pFeatLayer = pFeatureLayer_rese
                Case "Geologia"
                    v_Tema = "Geologia"
                Case "Cuadricula Regional"
                    v_Tema = "Cuadricula Regional"
                Case "Capitales Distritales"
                    v_Tema = "Capitales Distritales"
                    pFeatLayer = pFeatureLayer_capdist
                Case "DMxregion"
                    v_Tema = "Catastro"
                Case "Geo_bol100"
                    v_Tema = "Geo_bol100"
                    pFeatLayer = pFeatureLayer_Geo_bol100
                    ' pFeatLayer.ShowTips = True
                Case "Geo_fran100"
                    v_Tema = "Geo_fran100"
                    pFeatLayer = pFeatureLayer_Geo_fran100
                    'pFeatLayer.ShowTips = True

                Case "Geo_fran50"
                    v_Tema = "Geo_fran50"
                    pFeatLayer = pFeatureLayer_Geo_fran50
                    ' pFeatLayer.ShowTips = True

                Case Else
                    v_Tema = "Catastro"
                    pFeatLayer = pFeatureLayer_cat
            End Select

            Dim pActiveView As IActiveView
            Dim pFeatureLayerD As IFeatureLayerDefinition
            'pMxDoc = p_App.Document
            pMap = pMxDoc.FocusMap
            pActiveView = pMap
            Dim afound As Boolean = False
            If Nom_Shapefile = "DMxregion" Then
                For A As Integer = 0 To pMap.LayerCount - 1
                    If pMap.Layer(A).Name = v_Tema Then
                        pFeatLayer = pMxDoc.FocusMap.Layer(A)
                        afound = True
                        Exit For
                    End If
                Next A
                If Not afound Then
                    MsgBox("Layer No Existe.")
                    Exit Sub
                End If
            End If

            pFeatureLayerD = pFeatLayer
            If arch_cata = "Cata" Then
                pFeatureLayerD.DefinitionExpression = "CODIGOU =  '" & v_codigo & "'"
            ElseIf arch_cata = "Geologia_bol" Then
                pFeatureLayerD.DefinitionExpression = lista_Cd_bol_Geologia
            ElseIf arch_cata = "Geologia_bol_buf" Then
                pFeatureLayerD.DefinitionExpression = lista_Cd_bol_Geologia_bufer
            ElseIf arch_cata = "Geologia_fran50" Then
                pFeatureLayerD.DefinitionExpression = lista_Cd_Geologia_fran50
            ElseIf arch_cata = "Geologia_fran50_buf" Then
                pFeatureLayerD.DefinitionExpression = lista_Cd_Geologia_bufer50
            ElseIf arch_cata = "Geologia_fran100" Then
                pFeatureLayerD.DefinitionExpression = lista_Cd_Geologia_fran100
            ElseIf arch_cata = "Geologia_fran100_buf" Then
                pFeatureLayerD.DefinitionExpression = lista_Cd_Geologia_bufer100
            Else
                If arch_cata = "" Then
                    ' pFeatureLayerD.DefinitionExpression = lista_nm_depa  'comentado porque no toma el valor del argumento
                    pFeatureLayerD.DefinitionExpression = p_Nombre_Archivo 'cambiado porque toma del argumento en ingreso
                End If
            End If
            pMxDoc.UpdateContents()
            pActiveView.Refresh()
            'Dim pFeatureSelection As IFeatureSelection = pFeatLayer
            Dim pQueryFilter As IQueryFilter
            Dim pFeatureSelection As IFeatureSelection
            If Nom_Shapefile = "DMxregion" Then
                pFeatureSelection = pFeatLayer
            Else
                'pFeatureSelection = pFeatureLayer_cat
                pFeatureSelection = pFeatLayer
                If Not pFeatureSelection Is Nothing Then
                    'Dim pQueryFilter As IQueryFilter
                    ' Prepare a query filter.
                    pQueryFilter = New QueryFilter
                    If arch_cata = "Cata" Then
                        pQueryFilter.WhereClause = "CODIGOU =  '" & v_codigo & "'"
                        'If arch_cata = "DMxregion" Then
                        'pQueryFilter.WhereClause = "CODIGOU =  '" & v_codigo & "'"
                        'pQueryFilter.WhereClause = consulta_dms
                    ElseIf arch_cata = "Geologia_bol" Then
                        pQueryFilter.WhereClause = lista_Cd_bol_Geologia
                    ElseIf arch_cata = "Geologia_bol_buf" Then
                        pQueryFilter.WhereClause = lista_Cd_bol_Geologia_bufer
                    ElseIf arch_cata = "Geologia_fran50" Then
                        pQueryFilter.WhereClause = lista_Cd_Geologia_fran50
                    ElseIf arch_cata = "Geologia_fran50_buf" Then
                        pQueryFilter.WhereClause = lista_Cd_Geologia_bufer50
                    ElseIf arch_cata = "Geologia_fran100" Then
                        pQueryFilter.WhereClause = lista_Cd_Geologia_fran100
                    ElseIf arch_cata = "Geologia_fran100_buf" Then
                        pQueryFilter.WhereClause = lista_Cd_Geologia_bufer100
                    Else
                        'pQueryFilter.WhereClause = lista_nm_depa  'comentado porque no toma el valor del argumento
                        pQueryFilter.WhereClause = p_Nombre_Archivo  'cambiado porque toma del argumento en ingreso

                    End If
                End If
                ' Refresh or erase any previous selection.
                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pMxDoc.ActiveView.Refresh()

            End If
            Select Case pFeatureSelection.SelectionSet.Count
                Case 0
                    v_existe = False
                    Exit Sub
            End Select
            v_existe = True
            If arch_cata = "Cata" Then
                cls_Catastro.Exportando_Temas("DM", Nom_Shapefile, p_App)
            Else
                If Nom_Shapefile = "DMxregion" Then
                    cls_Catastro.Exportando_Temas("", Nom_Shapefile, p_App)
                ElseIf Nom_Shapefile = "Geo_bol100" Then
                    cls_Catastro.Exportando_Temas("Geo_bol100", Nom_Shapefile, p_App)
                ElseIf Nom_Shapefile = "Geo_fran100" Then
                    cls_Catastro.Exportando_Temas("Geo_fran100", Nom_Shapefile, p_App)
                ElseIf Nom_Shapefile = "Geo_fran50" Then
                    cls_Catastro.Exportando_Temas("Geo_fran50", Nom_Shapefile, p_App)


                Else
                    If v_Tema = "Catastro" Then
                        'arch_cata = ""
                        cls_Catastro.Exportando_Temas("", Nom_Shapefile, p_App)
                    End If
                End If
                'Exit Sub

                If arch_cata = "DMxregion" Then
                    pFeatureSelection.Clear()
                    Exit Sub
                End If
                pFeatureSelection = pFeatureLayerD
                pFeatureLayerD = pFeatureSelection
                Dim pFeatureLayer_1 As IFeatureLayer
                pFeatureLayer_1 = Nothing
                pFeatureLayer_1 = pFeatureLayerD.CreateSelectionLayer(v_Tema, True, vbNullString, "")
                pMap.DeleteLayer(pFeatLayer)
                pMap.AddLayer(pFeatureLayer_1)
                pMxDoc.UpdateContents()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub adicionadataframe(ByVal sele_mapa As String)
        Dim cls_planos As New Cls_planos
        Dim mapa_nuevo As IMap
        Dim cls_eval As New Cls_evaluacion
        Dim pMapElement As IElement
        Dim pGeoExt As IGeometry

        'pMxDoc = p_App.Document
        Dim pMaps As IMaps
        'Dim pMxDocument As IMxDocument
        Dim pActiveView As IActiveView
        'pMxDocument = ThisDocument
        pActiveView = pMxDoc.ActiveView
        pMaps = pMxDoc.Maps
        'dim
        mapa_nuevo = pMaps.Create
        If sele_mapa = "CARTA IGN" Then
            mapa_nuevo.Name = "CARTA IGN"
            mapa_nuevo.MapUnits = esriUnits.esriDecimalDegrees
            mapa_nuevo.DistanceUnits = esriUnits.esriDecimalDegrees
        ElseIf sele_mapa = "DEMARCACION POLITICA" Then
            mapa_nuevo.Name = "DEMARCACION POLITICA"
            mapa_nuevo.MapUnits = esriUnits.esriMeters
            mapa_nuevo.DistanceUnits = esriUnits.esriMeters
        ElseIf sele_mapa = "INTEGRANTES ACUMULACION" Then
            mapa_nuevo.Name = "INTEGRANTES ACUMULACION"
            mapa_nuevo.MapUnits = esriUnits.esriMeters
            mapa_nuevo.DistanceUnits = esriUnits.esriMeters
        ElseIf sele_mapa = "ACUMULACION DM" Then
            mapa_nuevo.Name = "ACUMULACION DM"
            mapa_nuevo.MapUnits = esriUnits.esriMeters
            mapa_nuevo.DistanceUnits = esriUnits.esriMeters
        ElseIf sele_mapa = "ACUMULACION AFECTACION" Then
            mapa_nuevo.Name = "ACUMULACION AFECTACION"
            mapa_nuevo.MapUnits = esriUnits.esriMeters
            mapa_nuevo.DistanceUnits = esriUnits.esriMeters
        ElseIf sele_mapa = "ACUMULACION BENEFICIO" Then
            mapa_nuevo.Name = "ACUMULACION BENEFICIO"
            mapa_nuevo.MapUnits = esriUnits.esriMeters
            mapa_nuevo.DistanceUnits = esriUnits.esriMeters
        ElseIf sele_mapa = "ACUMULACION ARESTRINGIDA" Then
            mapa_nuevo.Name = "ACUMULACION ARESTRINGIDA"
            mapa_nuevo.MapUnits = esriUnits.esriMeters
            mapa_nuevo.DistanceUnits = esriUnits.esriMeters
        ElseIf sele_mapa = "MAPA GEOLOGICO" Then
            mapa_nuevo.Name = "MAPA GEOLOGICO"
            mapa_nuevo.MapUnits = esriUnits.esriMeters
            mapa_nuevo.DistanceUnits = esriUnits.esriMeters
        ElseIf sele_mapa = "MAPA UBICACION" Then
            cls_planos.removedataframeadicional("DEMARCACION POLITICA")
            mapa_nuevo.Name = "MAPA UBICACION"
            mapa_nuevo.MapUnits = esriUnits.esriMeters
            mapa_nuevo.DistanceUnits = esriUnits.esriMeters
        ElseIf sele_mapa = "MAPA EMPALME" Then
            cls_planos.removedataframeadicional("CARTA IGN")
            mapa_nuevo.Name = "MAPA DE EMPALME"
            mapa_nuevo.MapUnits = esriUnits.esriMeters
            mapa_nuevo.DistanceUnits = esriUnits.esriMeters
        ElseIf sele_mapa = "MAPA_UBICACION" Then

            mapa_nuevo.Name = "MAPA_UBICACION"
            mapa_nuevo.MapUnits = esriUnits.esriMeters
            mapa_nuevo.DistanceUnits = esriUnits.esriMeters


        ElseIf sele_mapa = "PLANO UBICACION" Then
            cls_planos.removedataframeadicional("CATASTRO MINERO")
            mapa_nuevo.Name = "PLANO UBICACION"
            mapa_nuevo.MapUnits = esriUnits.esriMeters
            mapa_nuevo.DistanceUnits = esriUnits.esriMeters

        ElseIf sele_mapa = "DM SIMULTANEO" Then
            mapa_nuevo.Name = "DM SIMULTANEO"
            mapa_nuevo.MapUnits = esriUnits.esriMeters
            mapa_nuevo.DistanceUnits = esriUnits.esriMeters
        End If
        pMxDoc.ActivatedView.Refresh()
        Dim pMapFrame As IMapFrame
        Dim pMapExt As IEnvelope
        pMapFrame = New MapFrame
        pMapExt = New Envelope
        pMapFrame.Map = mapa_nuevo

        If sele_mapa = "MAPA UBICACION" Then
            pMapExt.XMin = 21.9
            pMapExt.YMin = 12.9
            pMapExt.XMax = 25.9
            pMapExt.YMax = 17.2
        ElseIf sele_mapa = "MAPA_UBICACION" Then
            If sele_plano1 = "Plano A4 Horizontal" Then
                pMapExt.XMin = 21.359
                pMapExt.YMin = 13.765
                pMapExt.XMax = 28.59
                pMapExt.YMax = 19.044
            ElseIf sele_plano1 = "Plano A4 Vertical" Then
                pMapExt.XMin = 1.0259
                pMapExt.YMin = 21.8272
                pMapExt.XMax = 8.9719
                pMapExt.YMax = 27.6403
            ElseIf sele_plano1 = "Plano A3 Horizontal" Then
                pMapExt.XMin = 30.158
                pMapExt.YMin = 19.122
                pMapExt.XMax = 40.461
                pMapExt.YMax = 26.607
            ElseIf sele_plano1 = "Plano A3 Vertical" Then
                pMapExt.XMin = 1.025
                pMapExt.YMin = 30.93
                pMapExt.XMax = 11.7863
                pMapExt.YMax = 38.7379
            ElseIf sele_plano1 = "Plano A2 Horizontal" Then
                pMapExt.XMin = 42.92
                pMapExt.YMin = 27.3
                pMapExt.XMax = 57.674
                pMapExt.YMax = 38.06
            ElseIf sele_plano1 = "Plano A2 Vertical" Then
                pMapExt.XMin = 1.2164
                pMapExt.YMin = 43.8253
                pMapExt.XMax = 16.347
                pMapExt.YMax = 54.8163
            ElseIf sele_plano1 = "Plano A1 Horizontal" Then
                pMapExt.XMin = 60.933
                pMapExt.YMin = 38.161
                pMapExt.XMax = 82.0
                pMapExt.YMax = 53.5527
            ElseIf sele_plano1 = "Plano A1 Vertical" Then
                pMapExt.XMin = 1.7056
                pMapExt.YMin = 62.1809
                pMapExt.XMax = 24.8697
                pMapExt.YMax = 79.0338
            ElseIf sele_plano1 = "Plano A0 Horizontal" Then
                pMapExt.XMin = 85.2582
                pMapExt.YMin = 54.4985
                pMapExt.XMax = 114.4231
                pMapExt.YMax = 75.7366
            ElseIf sele_plano1 = "Plano A0 Vertical" Then
                pMapExt.XMin = 2.2843
                pMapExt.YMin = 88.8833
                pMapExt.XMax = 33.703
                pMapExt.YMax = 111.69
            End If
        ElseIf sele_mapa = "MAPA EMPALME" Then
            pMapExt.XMin = 21.9
            pMapExt.YMin = 12.9
            pMapExt.XMax = 25.9
            pMapExt.YMax = 17.2
        ElseIf sele_mapa = "PLANO UBICACION" Then
            pMapExt.XMin = 21.9
            pMapExt.YMin = 12.9
            pMapExt.XMax = 25.9
            pMapExt.YMax = 17.2
        Else
            pMapExt.XMin = 1.5
            pMapExt.YMin = 1.05
            pMapExt.XMax = 17.58
            pMapExt.YMax = 19.5
        End If

        pMapElement = pMapFrame
        pGeoExt = pMapExt
        If TypeOf pActiveView Is IPageLayout Then
            pMxDoc.ActiveView.FocusMap = mapa_nuevo
        Else
            pMxDoc.ActiveView = mapa_nuevo
        End If

        pActiveView.Refresh()
        pMxDoc.ActivatedView.Refresh()
        pMapElement.Geometry = pGeoExt
        If pActiveView.IsMapActivated Then
            pMxDoc.ActiveView = mapa_nuevo
            'Dim pGraphicsContainer As IGraphicsContainer
            pGraphicsContainer = pMxDoc.PageLayout
            pGraphicsContainer.AddElement(pMapElement, 2)
        Else
            pActiveView.GraphicsContainer.AddElement(pMapElement, 2)
            'pMapElement.Activate(p_App.Display)
            pMxDoc.UpdateContents()
        End If

        'pMapFrame1.ExtentType = esriExtentTypeEnum.esriExtentScale
        'If sele_mapa = "MAPA UBICACION" Then
        ' cls_planos.temasubicacion("MAPA UBICACION", m_application)
        ' End If

    End Sub
    Public Sub adicionadataframe_antes_150312(ByVal sele_mapa As String)
        Dim cls_planos As New Cls_planos
        Dim mapa_nuevo As IMap
        Dim cls_eval As New Cls_evaluacion
        Dim pMapElement As IElement
        Dim pGeoExt As IGeometry

        'pMxDoc = p_App.Document
        Dim pMaps As IMaps
        'Dim pMxDocument As IMxDocument
        Dim pActiveView As IActiveView
        'pMxDocument = ThisDocument
        pActiveView = pMxDoc.ActiveView
        pMaps = pMxDoc.Maps
        'dim
        mapa_nuevo = pMaps.Create
        If sele_mapa = "CARTA IGN" Then
            mapa_nuevo.Name = "CARTA IGN"
            mapa_nuevo.MapUnits = esriUnits.esriDecimalDegrees
            mapa_nuevo.DistanceUnits = esriUnits.esriDecimalDegrees
        ElseIf sele_mapa = "DEMARCACION POLITICA" Then
            mapa_nuevo.Name = "DEMARCACION POLITICA"
            mapa_nuevo.MapUnits = esriUnits.esriMeters
            mapa_nuevo.DistanceUnits = esriUnits.esriMeters
        ElseIf sele_mapa = "MAPA UBICACION" Then
            cls_planos.removedataframeadicional("DEMARCACION POLITICA")
            mapa_nuevo.Name = "MAPA UBICACION"
            mapa_nuevo.MapUnits = esriUnits.esriMeters
            mapa_nuevo.DistanceUnits = esriUnits.esriMeters
        ElseIf sele_mapa = "MAPA EMPALME" Then
            cls_planos.removedataframeadicional("CARTA IGN")
            mapa_nuevo.Name = "MAPA DE EMPALME"
            mapa_nuevo.MapUnits = esriUnits.esriMeters
            mapa_nuevo.DistanceUnits = esriUnits.esriMeters
        ElseIf sele_mapa = "MAPA_UBICACION" Then
            mapa_nuevo.Name = "MAPA_UBICACION"
            mapa_nuevo.MapUnits = esriUnits.esriMeters
            mapa_nuevo.DistanceUnits = esriUnits.esriMeters
        End If
        pMxDoc.ActivatedView.Refresh()
        Dim pMapFrame As IMapFrame
        Dim pMapExt As IEnvelope
        pMapFrame = New MapFrame
        pMapExt = New Envelope
        pMapFrame.Map = mapa_nuevo

        If sele_mapa = "MAPA UBICACION" Then
            pMapExt.XMin = 21.9
            pMapExt.YMin = 12.9
            pMapExt.XMax = 25.9
            pMapExt.YMax = 17.2
        ElseIf sele_mapa = "MAPA_UBICACION" Then
            If sele_plano1 = "Plano A4 Horizontal" Then
                pMapExt.XMin = 21.8
                pMapExt.YMin = 13.9
                pMapExt.XMax = 28.95
                pMapExt.YMax = 19.1
            ElseIf sele_plano1 = "Plano A4 Vertical" Then
                pMapExt.XMin = 1.5
                pMapExt.YMin = 21.5
                pMapExt.XMax = 8.6
                pMapExt.YMax = 29.05
            ElseIf sele_plano1 = "Plano A3 Horizontal" Then
                pMapExt.XMin = 30.8
                pMapExt.YMin = 19.9
                pMapExt.XMax = 40.95
                pMapExt.YMax = 27.1
            ElseIf sele_plano1 = "Plano A3 Vertical" Then
                pMapExt.XMin = 1.7
                pMapExt.YMin = 31.5
                pMapExt.XMax = 11.9
                pMapExt.YMax = 41.05
            ElseIf sele_plano1 = "Plano A2 Horizontal" Then
                pMapExt.XMin = 43.5
                pMapExt.YMin = 28.1
                pMapExt.XMax = 57.7
                pMapExt.YMax = 38.5
            ElseIf sele_plano1 = "Plano A2 Vertical" Then
                pMapExt.XMin = 2.2
                pMapExt.YMin = 45.0
                pMapExt.XMax = 20.5
                pMapExt.YMax = 58.1
            ElseIf sele_plano1 = "Plano A1 Horizontal" Then
                pMapExt.XMin = 61.8
                pMapExt.YMin = 39.9
                pMapExt.XMax = 81.95
                pMapExt.YMax = 54.5
            ElseIf sele_plano1 = "Plano A1 Vertical" Then
                pMapExt.XMin = 3.0
                pMapExt.YMin = 63.5
                pMapExt.XMax = 28.9
                pMapExt.YMax = 83.1
            ElseIf sele_plano1 = "Plano A0 Horizontal" Then
                pMapExt.XMin = 87.8
                pMapExt.YMin = 56.5
                pMapExt.XMax = 115.9
                pMapExt.YMax = 76.1
            ElseIf sele_plano1 = "Plano A0 Vertical" Then
                pMapExt.XMin = 2.4
                pMapExt.YMin = 90.5
                pMapExt.XMax = 40.2
                pMapExt.YMax = 116.0
            End If
        ElseIf sele_mapa = "MAPA EMPALME" Then
            pMapExt.XMin = 21.9
            pMapExt.YMin = 12.9
            pMapExt.XMax = 25.9
            pMapExt.YMax = 17.2
        Else
            pMapExt.XMin = 1.5
            pMapExt.YMin = 1.05
            pMapExt.XMax = 17.58
            pMapExt.YMax = 19.5
        End If

        pMapElement = pMapFrame
        pGeoExt = pMapExt
        If TypeOf pActiveView Is IPageLayout Then
            pMxDoc.ActiveView.FocusMap = mapa_nuevo
        Else
            pMxDoc.ActiveView = mapa_nuevo
        End If

        pActiveView.Refresh()
        pMxDoc.ActivatedView.Refresh()
        pMapElement.Geometry = pGeoExt
        If pActiveView.IsMapActivated Then
            pMxDoc.ActiveView = mapa_nuevo
            'Dim pGraphicsContainer As IGraphicsContainer
            pGraphicsContainer = pMxDoc.PageLayout
            pGraphicsContainer.AddElement(pMapElement, 2)
        Else
            pActiveView.GraphicsContainer.AddElement(pMapElement, 2)
            'pMapElement.Activate(p_App.Display)
            pMxDoc.UpdateContents()
        End If

        'pMapFrame1.ExtentType = esriExtentTypeEnum.esriExtentScale
        'If sele_mapa = "MAPA UBICACION" Then
        ' cls_planos.temasubicacion("MAPA UBICACION", m_application)
        ' End If

    End Sub

    Public Sub activadataframe(ByVal nombre_dataframe As String)
        'Activando el datraframe Catastro Minero

        Dim contador As Integer = 0
        Dim mapas As IMaps
        mapas = pMxDoc.Maps
        Dim mapa As IMap
        Dim i As Integer
        pMxDoc.UpdateContents()
        For i = 0 To mapas.Count - 1
            mapa = mapas.Item(i)
            If nombre_dataframe = caso_consulta Then
                If caso_consulta = "CATASTRO MINERO" Then
                    pMxDoc.ActiveView = mapa
                    pMxDoc.UpdateContents()
                    contador = i
                    Exit For
                ElseIf caso_consulta = "CARTA IGN" Then
                    pMxDoc.ActiveView = mapa
                    pMxDoc.UpdateContents()
                    If mapa.Name = "MAPA DE EMPALME" Then
                        mapas.RemoveAt(i)
                        pMxDoc.UpdateContents()
                        Exit Sub
                    End If
                    pMxDoc.UpdateContents()
                ElseIf caso_consulta = "DEMARCACION POLITICA" Then
                    pMxDoc.ActiveView = mapa
                    pMxDoc.UpdateContents()
                    If mapa.Name = "MAPA UBICACION" Then
                        mapas.RemoveAt(i)
                        pMxDoc.UpdateContents()
                        Exit Sub
                    End If
                    pMxDoc.UpdateContents()
                ElseIf caso_consulta = "MAPA GEOLOGICO" Then
                    pMxDoc.ActiveView = mapa
                    pMxDoc.UpdateContents()
                    'Exit Sub
                ElseIf caso_consulta = "DM SIMULTANEO" Then
                    pMxDoc.ActiveView = mapa
                    pMxDoc.UpdateContents()
                    Exit Sub
                ElseIf caso_consulta = "ACUMULACION ARESTRINGIDA" Then
                    pMxDoc.ActiveView = mapa
                    pMxDoc.UpdateContents()
                ElseIf caso_consulta = "ACUMULACION AFECTACION" Then
                    pMxDoc.ActiveView = mapa
                    pMxDoc.UpdateContents()
                ElseIf caso_consulta = "ACUMULACION BENEFICIO" Then
                    pMxDoc.ActiveView = mapa
                    pMxDoc.UpdateContents()
                ElseIf caso_consulta = "INTEGRANTES ACUMULACION" Then
                    pMxDoc.ActiveView = mapa
                    pMxDoc.UpdateContents()
                    'Exit For
                ElseIf caso_consulta = "ACUMULACION DM" Then
                    pMxDoc.ActiveView = mapa
                    pMxDoc.UpdateContents()
                End If

            End If

        Next i
        pMxDoc.UpdateContents()
    End Sub

    Public Sub Eliminadataframe(ByVal nombre_dataframe As String)
        Dim pos As Integer
        Dim cls_eval As New Cls_evaluacion
        'Dim pMxDoc As IMxDocument
        Dim pMaps As IMaps2
        Dim NroMaps As Integer
        'Dim pMap As IMap
        'pMxDoc = ThisDocument
        pMaps = pMxDoc.Maps
        pMap = pMxDoc.FocusMap

        If pMap.Name = "DM SIMULTANEO" Then
            pMaps.RemoveAt(0)
            pMxDoc.UpdateContents()
        End If

        If pMap.Name <> nombre_dataframe Then
            cls_eval.activadataframe(nombre_dataframe)
            pMxDoc.UpdateContents()
        End If
        NroMaps = pMaps.Count
        Dim k As Integer
        If ((NroMaps = 1) And (pMap.Name = nombre_dataframe)) Then
        ElseIf ((NroMaps > 1) And (pMap.Name = nombre_dataframe)) Then
            For k = 1 To NroMaps - 1
                If (pos >= 0) And (pos <= (NroMaps - 1)) Then
                    If (NroMaps - 1 = 0) Then
                        'Si hay mas de 1 mapa en la vista
                        If NroMaps > 1 Then
                            pMaps.RemoveAt(1)  ' Remueve el de ubicacion
                        End If
                    Else
                        If NroMaps > 1 Then
                            pMaps.RemoveAt(1) ' remueve el frama mapa de ubicacion
                        End If
                    End If
                    pMxDoc.UpdateContents()
                End If
            Next k
        End If
    End Sub

    Public Sub Eliminadataframe_sim(ByVal nombre_dataframe As String)
        Dim pos As Integer
        Dim cls_eval As New Cls_evaluacion
        'Dim pMxDoc As IMxDocument
        Dim pMaps As IMaps2
        Dim NroMaps As Integer
        'Dim pMap As IMap
        'pMxDoc = ThisDocument
        pMaps = pMxDoc.Maps
        pMap = pMxDoc.FocusMap

        If pMap.Name = "DM SIMULTANEO" Then
            pMaps.RemoveAt(0)
            pMxDoc.UpdateContents()
        End If

    End Sub

    Public Sub Eliminadataframe_ant()
        Dim pos As Integer
        Dim cls_eval As New Cls_evaluacion
        'Dim pMxDoc As IMxDocument
        Dim pMaps As IMaps2
        Dim NroMaps As Integer
        'Dim pMap As IMap
        'pMxDoc = ThisDocument
        pMaps = pMxDoc.Maps
        pMap = pMxDoc.FocusMap
        If pMap.Name <> "CATASTRO MINERO" Then
            cls_eval.activadataframe("CATASTRO MINERO")
            pMxDoc.UpdateContents()
        End If
        NroMaps = pMaps.Count
        Dim k As Integer
        If ((NroMaps = 1) And (pMap.Name = "CATASTRO MINERO")) Then
        ElseIf ((NroMaps > 1) And (pMap.Name = "CATASTRO MINERO")) Then
            For k = 1 To NroMaps - 1
                If (pos >= 0) And (pos <= (NroMaps - 1)) Then
                    If (NroMaps - 1 = 0) Then
                        'Si hay mas de 1 mapa en la vista
                        If NroMaps > 1 Then
                            pMaps.RemoveAt(1)  ' Remueve el de ubicacion
                        End If
                    Else
                        If NroMaps > 1 Then
                            pMaps.RemoveAt(1) ' remueve el frama mapa de ubicacion
                        End If
                    End If
                    pMxDoc.UpdateContents()
                End If
            Next k
        End If
    End Sub



    Public Sub verprioritatios(ByVal criterio As String, ByVal criterio1 As String, ByVal criterio2 As String, ByVal criterio3 As String, ByVal criterio4 As String, ByVal criterio5 As String, ByVal criterio6 As String, ByVal criterio7 As String, ByVal criterio8 As String, ByVal pApp As IApplication)

        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim pdefiniciontema As IFeatureLayerDefinition
        'Dim pfeatureselecion As IFeatureSelection
        Dim pfeaturesele As ISelectionSet
        Dim pActiveView As IActiveView
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        Dim b As Integer
        Dim afound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Catastro" Then
                pFeatLayer = pMap.Layer(A)
                b = A
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra el Layer")
            Exit Sub
        End If
        pFeatureSelection = pFeatLayer
        pMxDoc.ActivatedView.Refresh()
        Dim consulta As IQueryFilter
        consulta = New QueryFilter
        If (criterio = "" And criterio1 = "" And criterio2 = "" And criterio3 = "" And criterio4 = "" And criterio5 = "" And criterio6 = "" And criterio7 = "" And criterio8 = "") Then
            consulta.WhereClause = "EVAL = 'EV' Or EVAL = '" & criterio1 & "' or EVAL = '" & criterio2 & "' or EVAL = '" & criterio3 & "' or EVAL = '" & criterio4 & "' or EVAL = '" & criterio5 & "' or EVAL = '" & criterio6 & "' or EVAL = '" & criterio7 & "' or EVAL = '" & criterio8 & "'"
        Else
            consulta.WhereClause = "EVAL = '" & criterio & "'  OR EVAL = '" & criterio1 & "' or EVAL = '" & criterio2 & "' or EVAL = '" & criterio3 & "' or EVAL = '" & criterio4 & "' or EVAL = '" & criterio5 & "' or EVAL = '" & criterio6 & "' or EVAL = '" & criterio7 & "' or EVAL = '" & criterio8 & "'"
        End If
        'consulta.WhereClause = "EVAL = '" & criterio1 & "' or EVAL = '" & criterio2 & "' or EVAL = '" & criterio3 & "' or EVAL = '" & criterio4 & "' or EVAL = '" & criterio5 & "' or EVAL = '" & criterio6 & "' or EVAL = '" & criterio7 & "' or EVAL = '" & criterio8 & "'"
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFeatureSelection.SelectFeatures(consulta, esriSelectionResultEnum.esriSelectionResultNew, False)
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pfeaturesele = pFeatureSelection.SelectionSet
        'MsgBox(consulta.WhereClause)
        'MsgBox(pfeaturesele.Count)
        If pfeaturesele.Count > 0 Then
            pFeatureSelection.Clear()
            pdefiniciontema = pFeatLayer
            pdefiniciontema.DefinitionExpression = "EVAL = 'EV'  or EVAL = '" & criterio1 & "' or EVAL = '" & criterio2 & "' or EVAL = '" & criterio3 & "' or EVAL = '" & criterio4 & "' or EVAL = '" & criterio5 & "' or EVAL = '" & criterio6 & "' or EVAL = '" & criterio7 & "' or EVAL = '" & criterio8 & "'"
            pMxDoc.ActiveView.Refresh()
            cls_Catastro.Limpiar_Texto_Pantalla(pApp)
            cls_Catastro.Load_FC_GDB("Mallap_" & v_zona_dm, "", pApp, True)
            cls_Catastro.Rotular_texto_DM("Mallap_" & v_zona_dm, v_zona_dm, pApp)
            cls_Catastro.Quitar_Layer("Mallap_" & v_zona_dm, pApp)
            cls_Catastro.rotulatexto_dm("Catastro", pApp)
        Else
            MsgBox("No existe este caso de evaluación para el DM", MsgBoxStyle.Information, "[BDGEOCATMIN]")
            Exit Sub
        End If

    End Sub

    Public Sub valida_dm_prioritarios(ByVal p_App As IApplication)
        Dim cls_eval As New Cls_evaluacion
        Dim pFeatLayer As IFeatureLayer = Nothing
        'Dim pMxApp As IMxApplication
        'pMxApp = pApp
        Dim pFeatureSelection As IFeatureSelection
        Dim capa_sele As ISelectionSet
        pMap = pMxDoc.FocusMap
        Dim afound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Catastro" Then
                pFeatLayer = pMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra el Layer")
            Exit Sub
        End If
        Dim loStrShapefile As String
        cls_eval.consultacapaDM("", "Prioritarios_v", "Catastro")
        pFeatureSelection = pFeatLayer
        capa_sele = pFeatureSelection.SelectionSet
        If capa_sele.Count > 0 Then  ' Si existe genera capa de dm prioritarios
            v_cantiprioritarios = 1
            Try
                'Kill(glo_pathTMP & "\Prioritarios*.*")
                Kill(glo_pathTMP & "Prioritarios*.*")
            Catch ex As Exception
            End Try
            cls_Catastro.Exportando_Temas("", "Catastro", pApp)
            pFeatureSelection.Clear()
            loStrShapefile = "Prioritarios" & v_codigo & fecha_archi_prioritario
            'gloNumprioritario = v_codigo & fecha_archi_sup
            cls_Catastro.Add_ShapeFile1("Prioritarios" & v_codigo & fecha_archi_prioritario, pApp, "Prioritarios")
        Else
            'Si hay para calcular area disponible
            MsgBox("El DM evaluado no presenta derechos prioritarios considerados para el cálculo de Area Disponible...", vbCritical, "OBSERVACION...")
            v_cantiprioritarios = 0
            Exit Sub
        End If
        '  loStrShapefile = "DM_" & v_codigo

        ' cls_Catastro.Add_ShapeFile1(loStrShapefile, m_application, "codigo")
        cls_Catastro.Add_ShapeFile1(loStrShapefile_dm, m_application, "codigo")
        pMxDoc.ActiveView.Refresh()
    End Sub
    Function Extraer(ByVal Path As String, ByVal Caracter As String) As String
        Dim ret As String
        If Caracter = "." And InStr(Path, Caracter) = 0 Then Return ""
        ret = Microsoft.VisualBasic.Right(Path, Len(Path) - InStrRev(Path, Caracter))
        ' -- Retorna el valor   
        Extraer = ret
    End Function

    Public Function Islocked(ByVal sfile As String) As Boolean
        Try
            Islocked = False
            FileOpen(1, sfile, OpenMode.Binary)
            Lock(1)
            Unlock(1)
            FileClose(1)

        Catch
            FileClose(1)
            '  Islocked = True
            Unlock(1)
            FileClose(1)
        End Try

    End Function

    Public Sub Geoprocesamiento_temas_shp(ByVal tema_procesing As String, ByVal p_App As IApplication, ByVal tipo_capa As String)
        'Programa para interceptar temas - DM evaluado VS DM prioritarios
        '*******************************************************************
        Try
            Dim sfile As String
            Dim Ext As String

            Dim cls_eval As New Cls_evaluacion
            Dim cls_catastro As New cls_DM_1
            Dim loStrShapefile As String
            Dim valor_areasup As Long
            Dim valor_areadispo As Long

            'Dim fecha_archip As String = ""
            'fecha_archip = DateTime.Now.Ticks.ToString

            Dim pNewWSName As IWorkspaceName
            Dim pFCName As IFeatureClassName
            Dim pDatasetName As IDatasetName
            ' pMxDoc = p_App.Document
            Dim tema1 As IFeatureLayer
            Dim tema2 As IFeatureLayer

            'Define primer tema
            '********************
            Dim tema1_fclass As IFeatureClass
            Dim tema2_fclass As IFeatureClass
            Dim pFeatLayer As IFeatureLayer = Nothing

            pMap = pMxDoc.FocusMap

            'se comento esta parte

            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "DM_" & v_codigo Then
                    pFeatLayer = pMap.Layer(A)
                    afound = True
                    'tema1 = pMap.Layer(0)
                    Exit For
                End If
            Next A
            If Not afound Then
                cls_catastro.Quitar_Layer("DM_" & v_codigo, p_App)
                cls_catastro.Quitar_Layer("RESERVA" & fecha_archi, p_App)
                cls_catastro.Quitar_Layer("AreaNatural" & fecha_archi, p_App)
                MsgBox("No se encuentra la capa de Derechos Mineros para realizar el calculo de área disponible", MsgBoxStyle.Information, "Observación...")
                Exit Sub
            End If

            tema1 = pFeatLayer

            tema1_fclass = tema1.FeatureClass
            If tipo_capa = "RESERVA" Or tipo_capa = "AreaNatural" Then
                pMap.DeleteLayer(tema1)
            End If
            'Define segundo tema
            '*********************
            pMap = pMxDoc.FocusMap

            If tipo_capa = "RESERVA" Then
                For A As Integer = 0 To pMap.LayerCount - 1
                    'If pMap.Layer(A).Name = "Area Reservap" Then
                    If pMap.Layer(A).Name = "Area_Reserva_" & fecha_archi Then
                        pFeatLayer = pMap.Layer(A)
                        afound = True
                        Exit For
                    End If
                Next A
                'Borra si existe tema existente de reserva superpuesta a dm
                Dim pFeatLayerb As IFeatureLayer
                For e As Integer = 0 To pMap.LayerCount - 1
                    If pMap.Layer(e).Name = "AreaRese_super" Then
                        pFeatLayerb = pMap.Layer(e)
                        pMap.DeleteLayer(pFeatLayerb)
                        Exit For
                    End If
                Next e
                pMxDoc.UpdateContents()

            ElseIf tipo_capa = "AreaNatural" Then
                For A As Integer = 0 To pMap.LayerCount - 1
                    'If pMap.Layer(A).Name = "Area Reservap" Then
                    If pMap.Layer(A).Name = "Area_Natural_" & fecha_archi Then
                        pFeatLayer = pMap.Layer(A)
                        afound = True
                        Exit For
                    End If
                Next A
                'Borra si existe tema existente de reserva superpuesta a dm
                Dim pFeatLayerb As IFeatureLayer
                For e As Integer = 0 To pMap.LayerCount - 1
                    If pMap.Layer(e).Name = "AreaRese_super" Then

                        pFeatLayerb = pMap.Layer(e)
                        pMap.DeleteLayer(pFeatLayerb)
                        Exit For
                    End If
                Next e
                pMxDoc.UpdateContents()
            Else
                'se comento
                For A As Integer = 0 To pMap.LayerCount - 1
                    If pMap.Layer(A).Name = "Prioritarios" & v_codigo & fecha_archi_prioritario Then
                        pFeatLayer = pMap.Layer(A)
                        afound = True
                        'tema2 = pFeatLayer
                        Exit For
                    End If
                Next A
            End If
            If Not afound Then
                MsgBox("EL tema " & pFeatLayer.Name & "no se encuentra en la vista", MsgBoxStyle.Information, "Observación...")
                Exit Sub
            End If
            tema2 = pFeatLayer
            tema2_fclass = tema2.FeatureClass

            If tipo_capa = "RESERVA" Or tipo_capa = "AreaNatutral" Then
                pMap.DeleteLayer(tema2)
            End If
            'Define tema de salida
            pFCName = New FeatureClassName
            pDatasetName = pFCName

            pNewWSName = New WorkspaceName
            pNewWSName.WorkspaceFactoryProgID = "esriCore.ShapeFileWorkspaceFactory"
            ' pNewWSName.PathName = glo_pathTMP
            pNewWSName.PathName = glo_pathTMP & "\"
            pDatasetName.WorkspaceName = pNewWSName
            'Dim pfeeatureclass As IFeatureClass
            Dim pfeature As IFeature
            Dim pfeaturecursor As IFeatureCursor

            If tipo_capa = "RESERVA" Or tipo_capa = "AreaNatural" Then
                Dim fecharese As String
                fecharese = DateTime.Now.Ticks.ToString()
                pDatasetName.Name = "Area_Resesup_" & fecharese
                Dim tol As Double = 0.0
                Dim pOutputFC As IFeatureClass
                pBGP = New BasicGeoprocessor
                'Define tolerancia
                tol = 1
                pOutputFC = pBGP.Intersect(tema1_fclass, False, tema2_fclass, False, tol, pFCName)
                Dim pOutputFeatLayer As IFeatureLayer
                pOutputFeatLayer = New FeatureLayer
                pOutputFeatLayer.FeatureClass = pOutputFC
                pOutputFeatLayer.Name = "AreaRese_super"
                valor_areasup = pOutputFeatLayer.FeatureClass.FeatureCount(Nothing)  'cuenta registros del tema en total
                pMap.AddLayer(pOutputFeatLayer)
                pFeatureClass = pOutputFeatLayer.FeatureClass
                pfeaturecursor = pOutputFeatLayer.Search(Nothing, False)
                pfeature = pfeaturecursor.NextFeature
                Dim valor As String
                Dim pQueryFilter As IQueryFilter
                Do Until pfeature Is Nothing
                    valor = pfeature.Value(pFeatureClass.Fields.FindField("CODIGO"))
                    colecciones_supAR.Add(valor)
                    pfeature = pfeaturecursor.NextFeature
                Loop
                Dim valor1 As String
                Dim cuenta As Integer
                Dim pfeaturecursor1 As IFeatureCursor
                Dim pfeature1 As IFeature
                'Dim indexClass As Integer
                'indexClass = pFeatureClass.Fields.FindField("HAS")
                For contador As Integer = 1 To colecciones_supAR.Count
                    valor1 = colecciones_supAR.Item(contador)
                    pQueryFilter = New QueryFilter
                    pQueryFilter.WhereClause = "CODIGO = '" & valor1 & "' "
                    pfeaturecursor1 = pFeatureClass.Update(pQueryFilter, True)
                    pfeature1 = pfeaturecursor1.NextFeature
                    cuenta = 0
                    Do Until pfeature1 Is Nothing

                        cuenta = cuenta + 1
                        pfeature1.Value(pFeatureClass.Fields.FindField("HAS")) = cuenta
                        pfeaturecursor1.UpdateFeature(pfeature1)
                        pfeature1 = pfeaturecursor1.NextFeature
                    Loop
                Next contador

            Else
                fecha_archi_sup = DateTime.Now.Ticks.ToString()
                If tema_procesing = "interceccion" Then

                    pDatasetName.Name = "Areainter_" & v_codigo & fecha_archi_sup
                    'pDatasetName.Name = "Areainter_" & v_codigo & fecha_archi
                    'fecha_archi1 = fecha_archi
                    fecha_archi1 = fecha_archi_sup

                ElseIf tema_procesing = "union" Then
                    'pDatasetName.Name = "Areadispo" & fecha_archi
                    pDatasetName.Name = "Areadispo" & fecha_archi_sup
                    'pDatasetName.Name = "Areadispo" & fecha_archi1
                End If
                ' Perfomance del area interceptada
                Dim tol As Double = 0.0
                Dim pOutputFC As IFeatureClass
                pBGP = New BasicGeoprocessor
                'Define tolerancia
                ' tol = 1
                If tema_procesing = "interceccion" Then
                    'tol = 0.0001
                    tol = 0.01
                    pOutputFC = pBGP.Intersect(tema1_fclass, False, tema2_fclass, False, tol, pFCName)
                ElseIf tema_procesing = "union" Then
                    tol = 0.001      ' 0.01
                    pOutputFC = pBGP.Union(tema1_fclass, False, tema2_fclass, False, tol, pFCName)
                End If
                Dim pOutputFeatLayer As IFeatureLayer
                pOutputFeatLayer = New FeatureLayer
                pOutputFeatLayer.FeatureClass = pOutputFC
                If tema_procesing = "union" Then
                    pOutputFeatLayer.Name = "Areadispo"
                    valor_areadispo = pOutputFeatLayer.FeatureClass.FeatureCount(Nothing)  'cuenta registros del tema en total
                ElseIf tema_procesing = "interceccion" Then
                    pOutputFeatLayer.Name = "Areainter_" & v_codigo
                    valor_areasup = pOutputFeatLayer.FeatureClass.FeatureCount(Nothing)  'cuenta registros del tema en total
                End If
                'pOutputFeatLayer.Name = pOutputFC.AliasName
                pMap.AddLayer(pOutputFeatLayer)
                If tema_procesing = "union" Then
                    If valor_areadispo > 0 Then  'Si solo tiene valor area disponible inicial
                        cls_eval.consultacapaDM("", "union", "Areadispo")   'verifica area disponible resultantes
                        If v_adispo > 0 Then
                            cls_catastro.Exportando_Temas("", "Areadispo", pApp)
                            'loStrShapefile = "Areadispo_" & v_codigo & fecha_archi
                            loStrShapefile = "Areadispo_" & v_codigo & fecha_archi_sup
                            cls_catastro.Add_ShapeFile1("Areadispo_" & v_codigo & fecha_archi_sup, pApp, "")
                            pMap.Layer(0).Name = "Areadispo_" & v_codigo
                            pMxDoc.ActivatedView.Refresh()
                            cls_catastro.Color_Poligono_Simple(m_application, "Areadispo_" & v_codigo)
                        Else
                            cls_catastro.Quitar_Layer("Areadispo", pApp)
                            MsgBox("El D.M. Evaluado no tiene Area Disponible", MsgBoxStyle.Information, "Observación..")
                        End If
                    Else
                        MsgBox("El D.M. Evaluado no tiene Area Disponible", MsgBoxStyle.Information, "Observación..")
                    End If
                Else
                    If valor_areasup = 0 Then
                        cls_catastro.Quitar_Layer("Areainter_" & v_codigo, pApp)
                    Else
                        cls_catastro.Color_Poligono_Simple(m_application, "Areainter_" & v_codigo)
                    End If
                End If
            End If
            pMxDoc.ActivatedView.Refresh()
        Catch EX As Exception
            EX.Message.ToString()

        End Try

    End Sub

    Public Sub Geoprocesamiento_temas(ByVal tema_procesing As String, ByVal p_App As IApplication, ByVal tipo_capa As String)
        'Programa para interceptar temas - DM evaluado VS DM prioritarios
        '*******************************************************************
        Try
            If tema_procesing = "dissolve" Then
                Dim cls_eval As New Cls_evaluacion
                Dim cls_catastro As New cls_DM_1
                Dim pFeatLayer As IFeatureLayer = Nothing
                Dim pInputTable As ITable
                Dim afound As Boolean = False

                For A As Integer = 0 To pMap.LayerCount - 1
                    If pMap.Layer(A).Name = tipo_capa Then
                        pFeatLayer = pMap.Layer(A)
                        afound = True
                        Exit For
                    End If
                Next A

                pInputTable = pFeatLayer

                cls_catastro.Quitar_Layer("Areainter_" & v_codigo, pApp)

                Dim pNewWSName As IWorkspaceName
                Dim pFCName As IFeatureClassName
                Dim pDatasetName As IDatasetName

                pMap = pMxDoc.FocusMap

                pFCName = New FeatureClassName
                pDatasetName = pFCName

                pNewWSName = New WorkspaceName
                pNewWSName.WorkspaceFactoryProgID = "esriCore.ShapeFileWorkspaceFactory"
                pNewWSName.PathName = glo_pathTMP & "\"
                pDatasetName.Name = "Areainter_" & v_codigo & fecha_archi_sup & "_"
                pDatasetName.WorkspaceName = pNewWSName

                Dim pBGP As IBasicGeoprocessor
                Dim pOutputFC As IFeatureClass
                pBGP = New BasicGeoprocessor
                ' Run dissolve.
                'pOutputFC = pBGP.Dissolve(pInputTable, False, "Codigou_1", "Dissolve.Shape, Sum.Shape_ar_1", pFCName)
                ' pOutputFC = pBGP.Dissolve(pInputTable, False, "Codigou_1", "Dissolve.Shape, First.Concesio_1, Sum.Shape_ar_1", pFCName)
                pOutputFC = pBGP.Dissolve(pInputTable, False, "Codigou_1", "Dissolve.Shape, First.Concesio_1, First.Codigou_1,Sum.Shape_ar_1", pFCName)
                '   pOutputFC = pBGP.Dissolve(pInputTable, False, "Tit_conc_1", "Dissolve.Shape, First.Codigou_1, First.Concesio_1, Sum.Shape_ar_1", pFCName)

                Dim pOutputFeatLayer As IFeatureLayer
                pOutputFeatLayer = New FeatureLayer
                pOutputFeatLayer.FeatureClass = pOutputFC
                pOutputFeatLayer.Name = "Areainter_" & v_codigo

            Else
                'Dim S As System.IO
                'Dim S As FileName

                Dim sfile As String
                Dim Ext As String
                ' extension = Extraer(S, ".")

                ' For Each S In System.IO.Directory.GetFiles("C:\BDGEOCATMIN\Temporal")
                'Dim S As String() = System.IO.Directory.GetFiles("C:\BDGEOCATMIN\Temporal")

                'Var = S
                'Var = StrReverse(Mid(StrReverse(Var), 2, 100))
                'extension = Extraer(S, ".")
                'Ext = extension
                'Ext = UCase(StrReverse(Mid(StrReverse(Ext), 1, 4)))
                'If Ext = "LOCK" Then
                ' System.IO.File.Delete(S)
                ' End If
                '  If UBound(S) >= 0 Then
                '    For i As Integer = 0 To UBound(S)
                '        sfile = S(i)
                '        If Islocked(sfile) Then

                '            System.IO.File.Delete(sfile)
                '        End If

                '    Next

                'End If
                'Next

                Dim cls_eval As New Cls_evaluacion
                Dim cls_catastro As New cls_DM_1
                Dim loStrShapefile As String
                Dim valor_areasup As Long
                Dim valor_areadispo As Long

                'Dim fecha_archip As String = ""
                'fecha_archip = DateTime.Now.Ticks.ToString
                ' fecha_archi = DateTime.Now.Ticks.ToString ///////////////////
                Dim pNewWSName As IWorkspaceName
                Dim pFCName As IFeatureClassName
                Dim pDatasetName As IDatasetName
                ' pMxDoc = p_App.Document
                Dim tema1 As IFeatureLayer
                Dim tema2 As IFeatureLayer

                'Define primer tema
                '********************
                Dim tema1_fclass As IFeatureClass
                Dim tema2_fclass As IFeatureClass
                Dim pFeatLayer As IFeatureLayer = Nothing

                pMap = pMxDoc.FocusMap

                'se comento esta parte

                Dim afound As Boolean = False
                For A As Integer = 0 To pMap.LayerCount - 1
                    If pMap.Layer(A).Name = "DM_" & v_codigo Then
                        pFeatLayer = pMap.Layer(A)
                        afound = True
                        'tema1 = pMap.Layer(0)
                        Exit For
                    End If
                Next A
                If Not afound Then
                    cls_catastro.Quitar_Layer("DM_" & v_codigo, p_App)
                    cls_catastro.Quitar_Layer("RESERVA" & fecha_archi, p_App)
                    cls_catastro.Quitar_Layer("AreaNatural" & fecha_archi, p_App)
                    MsgBox("No se encuentra la capa de Derechos Mineros para realizar el calculo de área disponible", MsgBoxStyle.Information, "Observación...")
                    Exit Sub
                End If

                tema1 = pFeatLayer
                'termino de comentar
                ' se planteo la idea de trabajar con variables publicas

                ' tema1 = pfeaturelayer_1

                tema1_fclass = tema1.FeatureClass
                If tipo_capa = "RESERVA" Or tipo_capa = "AreaNatural" Then
                    pMap.DeleteLayer(tema1)
                End If
                'Define segundo tema
                '*********************
                pMap = pMxDoc.FocusMap

                '  afound = False
                If tipo_capa = "RESERVA" Then
                    For A As Integer = 0 To pMap.LayerCount - 1

                        'If pMap.Layer(A).Name = "Area Reservap" Then
                        If pMap.Layer(A).Name = "Area_Reserva_" & fecha_archi Then
                            pFeatLayer = pMap.Layer(A)
                            afound = True
                            Exit For
                        End If
                    Next A
                    'Borra si existe tema existente de reserva superpuesta a dm
                    Dim pFeatLayerb As IFeatureLayer
                    For e As Integer = 0 To pMap.LayerCount - 1
                        If pMap.Layer(e).Name = "AreaRese_super" Then

                            pFeatLayerb = pMap.Layer(e)
                            pMap.DeleteLayer(pFeatLayerb)
                            Exit For
                        End If
                    Next e
                    pMxDoc.UpdateContents()

                ElseIf tipo_capa = "AreaNatural" Then
                    For A As Integer = 0 To pMap.LayerCount - 1

                        'If pMap.Layer(A).Name = "Area Reservap" Then
                        If pMap.Layer(A).Name = "Area_Natural_" & fecha_archi Then
                            pFeatLayer = pMap.Layer(A)
                            afound = True
                            Exit For
                        End If
                    Next A
                    'Borra si existe tema existente de reserva superpuesta a dm
                    Dim pFeatLayerb As IFeatureLayer
                    For e As Integer = 0 To pMap.LayerCount - 1
                        If pMap.Layer(e).Name = "AreaRese_super" Then

                            pFeatLayerb = pMap.Layer(e)
                            pMap.DeleteLayer(pFeatLayerb)
                            Exit For
                        End If
                    Next e
                    pMxDoc.UpdateContents()
                Else
                    'se comento
                    For A As Integer = 0 To pMap.LayerCount - 1
                        If pMap.Layer(A).Name = "Prioritarios" & v_codigo & fecha_archi_prioritario Then
                            pFeatLayer = pMap.Layer(A)
                            afound = True
                            'tema2 = pFeatLayer
                            Exit For
                        End If
                    Next A
                End If
                If Not afound Then
                    MsgBox("EL tema " & pFeatLayer.Name & "no se encuentra en la vista", MsgBoxStyle.Information, "Observación...")
                    Exit Sub

                    'tema2 = pFeatLayer
                    ' tema2 = pfeaturelayer_2

                    ' tema2_fclass = tema2.FeatureClass
                End If
                tema2 = pFeatLayer
                tema2_fclass = tema2.FeatureClass

                If tipo_capa = "RESERVA" Or tipo_capa = "AreaNatutral" Then
                    pMap.DeleteLayer(tema2)
                End If
                'Define tema de salida
                pFCName = New FeatureClassName
                pDatasetName = pFCName

                pNewWSName = New WorkspaceName
                pNewWSName.WorkspaceFactoryProgID = "esriCore.ShapeFileWorkspaceFactory"
                ' pNewWSName.PathName = glo_pathTMP
                pNewWSName.PathName = glo_pathTMP & "\"
                pDatasetName.WorkspaceName = pNewWSName
                'Dim pfeeatureclass As IFeatureClass
                Dim pfeature As IFeature
                Dim pfeaturecursor As IFeatureCursor

                If tipo_capa = "RESERVA" Or tipo_capa = "AreaNatural" Then
                    Dim fecharese As String
                    fecharese = DateTime.Now.Ticks.ToString()
                    pDatasetName.Name = "Area_Resesup_" & fecharese
                    Dim tol As Double = 0.0
                    Dim pOutputFC As IFeatureClass
                    pBGP = New BasicGeoprocessor
                    'Define tolerancia
                    tol = 1
                    pOutputFC = pBGP.Intersect(tema1_fclass, False, tema2_fclass, False, tol, pFCName)
                    Dim pOutputFeatLayer As IFeatureLayer
                    pOutputFeatLayer = New FeatureLayer
                    pOutputFeatLayer.FeatureClass = pOutputFC
                    pOutputFeatLayer.Name = "AreaRese_super"
                    valor_areasup = pOutputFeatLayer.FeatureClass.FeatureCount(Nothing)  'cuenta registros del tema en total
                    pMap.AddLayer(pOutputFeatLayer)
                    pFeatureClass = pOutputFeatLayer.FeatureClass
                    pfeaturecursor = pOutputFeatLayer.Search(Nothing, False)
                    pfeature = pfeaturecursor.NextFeature
                    Dim valor As String
                    Dim pQueryFilter As IQueryFilter
                    Do Until pfeature Is Nothing
                        valor = pfeature.Value(pFeatureClass.Fields.FindField("CODIGO"))
                        colecciones_supAR.Add(valor)
                        pfeature = pfeaturecursor.NextFeature
                    Loop
                    Dim valor1 As String
                    Dim cuenta As Integer
                    Dim pfeaturecursor1 As IFeatureCursor
                    Dim pfeature1 As IFeature
                    'Dim indexClass As Integer
                    'indexClass = pFeatureClass.Fields.FindField("HAS")
                    For contador As Integer = 1 To colecciones_supAR.Count
                        valor1 = colecciones_supAR.Item(contador)
                        pQueryFilter = New QueryFilter
                        pQueryFilter.WhereClause = "CODIGO = '" & valor1 & "' "
                        pfeaturecursor1 = pFeatureClass.Update(pQueryFilter, True)
                        pfeature1 = pfeaturecursor1.NextFeature
                        cuenta = 0
                        Do Until pfeature1 Is Nothing

                            cuenta = cuenta + 1
                            pfeature1.Value(pFeatureClass.Fields.FindField("HAS")) = cuenta
                            pfeaturecursor1.UpdateFeature(pfeature1)
                            pfeature1 = pfeaturecursor1.NextFeature
                        Loop

                        'Dim indexClass As Integer
                        'Dim pCityFClass As IFeatureClass
                        'pCityFClass = pFeatLayer.FeatureClass
                        ''pCityFClass = pFeatureLayer_cat.FeatureClass
                        'pFields = pCityFClass.Fields
                        'indexClass = pCityFClass.FindField("EVAL")
                        'pFeatureCursor2 = pCityFClass.Update(SpatialFilter, False)
                        'pfeature = pFeatureCursor2.NextFeature

                        'consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                        'pUpdateFeatures1 = fclas_tema.Update(consulta1, False)
                        'pFeature_x = pUpdateFeatures1.NextFeature
                        'Do Until pFeature_x Is Nothing
                        '    pFeature_x.Value(c1_x) = "PO"
                        '    pUpdateFeatures1.UpdateFeature(pFeature_x)
                        '    pFeature_x = pUpdateFeatures1.NextFeature
                        'Loop

                    Next contador
                Else
                    fecha_archi_sup = DateTime.Now.Ticks.ToString()
                    If tema_procesing = "interceccion" Then

                        pDatasetName.Name = "Areainter_" & v_codigo & fecha_archi_sup
                        'pDatasetName.Name = "Areainter_" & v_codigo & fecha_archi
                        'fecha_archi1 = fecha_archi
                        fecha_archi1 = fecha_archi_sup

                    ElseIf tema_procesing = "union" Then
                        'pDatasetName.Name = "Areadispo" & fecha_archi
                        pDatasetName.Name = "Areadispo" & fecha_archi_sup
                        'pDatasetName.Name = "Areadispo" & fecha_archi1
                    End If
                    ' Perfomance del area interceptada
                    Dim tol As Double = 0.0
                    Dim pOutputFC As IFeatureClass
                    pBGP = New BasicGeoprocessor
                    'Define tolerancia
                    ' tol = 1
                    If tema_procesing = "interceccion" Then
                        'tol = 0.0001
                        tol = 0.01
                        pOutputFC = pBGP.Intersect(tema1_fclass, False, tema2_fclass, False, tol, pFCName)

                    ElseIf tema_procesing = "union" Then
                        tol = 0.001      ' 0.01
                        pOutputFC = pBGP.Union(tema1_fclass, False, tema2_fclass, False, tol, pFCName)
                    End If
                    Dim pOutputFeatLayer As IFeatureLayer
                    pOutputFeatLayer = New FeatureLayer
                    pOutputFeatLayer.FeatureClass = pOutputFC
                    If tema_procesing = "union" Then
                        pOutputFeatLayer.Name = "Areadispo"
                        valor_areadispo = pOutputFeatLayer.FeatureClass.FeatureCount(Nothing)  'cuenta registros del tema en total
                    ElseIf tema_procesing = "interceccion" Then
                        pOutputFeatLayer.Name = "Areainter_" & v_codigo
                        valor_areasup = pOutputFeatLayer.FeatureClass.FeatureCount(Nothing)  'cuenta registros del tema en total
                    End If
                    'pOutputFeatLayer.Name = pOutputFC.AliasName
                    pMap.AddLayer(pOutputFeatLayer)
                    If tema_procesing = "union" Then
                        If valor_areadispo > 0 Then  'Si solo tiene valor area disponible inicial
                            cls_eval.consultacapaDM("", "union", "Areadispo")   'verifica area disponible resultantes
                            If v_adispo > 0 Then
                                cls_catastro.Exportando_Temas("", "Areadispo", pApp)
                                'loStrShapefile = "Areadispo_" & v_codigo & fecha_archi
                                loStrShapefile = "Areadispo_" & v_codigo & fecha_archi_sup
                                cls_catastro.Add_ShapeFile1("Areadispo_" & v_codigo & fecha_archi_sup, pApp, "")
                                pMap.Layer(0).Name = "Areadispo_" & v_codigo
                                pMxDoc.ActivatedView.Refresh()
                                cls_catastro.Color_Poligono_Simple(m_application, "Areadispo_" & v_codigo)
                            Else
                                cls_catastro.Quitar_Layer("Areadispo", pApp)
                                MsgBox("El D.M. Evaluado no tiene Area Disponible", MsgBoxStyle.Information, "Observación..")
                            End If
                        Else
                            MsgBox("El D.M. Evaluado no tiene Area Disponible", MsgBoxStyle.Information, "Observación..")
                        End If
                    Else
                        If valor_areasup = 0 Then
                            cls_catastro.Quitar_Layer("Areainter_" & v_codigo, pApp)
                        Else
                            cls_catastro.Color_Poligono_Simple(m_application, "Areainter_" & v_codigo)
                            '  cls_catastro.Color_Poligono_Simple(m_application, "Areainter_" & v_codigo)
                        End If
                    End If
                End If
                pMxDoc.ActivatedView.Refresh()
            End If

        Catch EX As Exception
            EX.Message.ToString()

        End Try

    End Sub
    Public Function GeneraAreaDisponible_DM(ByVal p_App As IApplication) As String

        fecha_archi_prioritario = DateTime.Now.Ticks.ToString
        If tipo_seleccion = "OP_11" Or tipo_seleccion = "OP_12" Or tipo_seleccion = "OP_29" Then

            v_cantiprioritarios = 0
            Dim cls_eval As New Cls_evaluacion
            Dim cls_planos As New Cls_planos
            colecciones_planos.Clear()
            cls_planos.mueveposiciondataframe("CATASTRO MINERO", p_App)
            cls_Catastro.Quitar_Layer("DM_" & v_codigo, pApp)
            cls_Catastro.Quitar_Layer("Prioritarios" & v_codigo & fecha_archi_prioritario, pApp)
            'cls_Catastro.Quitar_Layer("Prioritarios" & gloNumprioritario, pApp)
            cls_Catastro.Quitar_Layer("Areadispo", pApp)
            cls_Catastro.Quitar_Layer("Areadispo_" & v_codigo, pApp)
            cls_Catastro.Quitar_Layer("Areainter_" & v_codigo, pApp)
            pMxDoc.ActivatedView.Refresh()
            Try
                ' Kill(glo_pathTMP & "\Prioritarios*.*")
                Kill(glo_pathTMP & "Prioritarios*.*")
            Catch ex As Exception

            End Try
            Try
                '  Kill(glo_pathTMP & "\Areadispo*.*")
                Kill(glo_pathTMP & "Areadispo*.*")
            Catch ex As Exception
            End Try
            Try
                ' Kill(glo_pathTMP & "\Areainter_*.*")
                Kill(glo_pathTMP & "Areainter_*.*")
            Catch ex As Exception
            End Try
            cls_Catastro.Zoom_to_Layer("Catastro")
            ' cls_Catastro.creandotabla_Areasup()
            pMxDoc.ActivatedView.Refresh()
            Dim pFeatLayer As IFeatureLayer
            pMxDoc = p_App.Document
            pMap = pMxDoc.FocusMap
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Catastro" Then
                    pFeatLayer = pMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Return ""
                Exit Function
            End If
            arch_cata = "Interceccion"
            cls_eval.valida_dm_prioritarios(pApp)  ' ojo sale error en este procemiento verificar
            pMxDoc.ActiveView.Refresh()
            If v_cantiprioritarios > 0 Then
                fecha_archi_sup_t = DateTime.Now.Ticks.ToString
                cls_Catastro.creandotabla_Areasup()
                cls_eval.Geoprocesamiento_temas("interceccion", pApp, "DM")

                ' VALIDACION DE REGISTROS     '''''''''''''''''''''''''''''
                Dim v_validación_codigo As String = "No Tiene"
                cls_Catastro.Add_ShapeFile_tmp("Areainter_" & v_codigo & fecha_archi_sup, pApp)
                pFeatLayer = pMap.Layer(0)
                pFeatureClass = pFeatLayer.FeatureClass
                pFeatureCursor = pFeatureClass.Search(Nothing, False)

                Dim v_codigo_ev As String
                Dim pQFilter As IQueryFilter
                Dim pfeatureselection1 As IFeatureSelection
                Dim pselectionset1 As ISelectionSet
                Dim cantidad_reg As Integer
                Dim pFeature As IFeature
                pfeatureselection1 = pFeatLayer
                pFeature = pFeatureCursor.NextFeature

                Do Until pFeature Is Nothing
                    v_codigo_ev = pFeature.Value(pFeatureCursor.FindField("CODIGOU_1"))

                    pQFilter = New QueryFilter
                    pQFilter.WhereClause = "CODIGOU_1 = '" & v_codigo_ev & "'"
                    pfeatureselection1.SelectFeatures(pQFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
                    pselectionset1 = pfeatureselection1.SelectionSet
                    cantidad_reg = pselectionset1.Count
                    If (cantidad_reg > 1) Then
                        v_validación_codigo = "Tiene"
                        Exit Do
                    End If
                    pFeature = pFeatureCursor.NextFeature
                Loop

                'cls_Catastro.Quitar_Layer("Areainter_" & v_codigo, pApp)

                If v_validación_codigo = "Tiene" Then

                    Dim p_shapefile_diss = "Areainter_" & v_codigo '& fecha_archi_sup
                    cls_eval.Geoprocesamiento_temas("dissolve", pApp, p_shapefile_diss)

                    cls_Catastro.Add_ShapeFile_tmp("Areainter_" & v_codigo & fecha_archi_sup & "_", pApp)
                    cls_eval.agregacampotema_tpm(p_shapefile_diss, "Areaint")

                    cls_Catastro.Add_ShapeFile("Areainter_" & v_codigo & fecha_archi_sup & "_", pApp)
                    cls_Catastro.Update_Value_DM_areaint(pApp, p_shapefile_diss, "")
                    cls_Catastro.Color_Poligono_Simple(m_application, "Areainter_" & v_codigo)
                End If


                ''''''''''''''''''''''''''''''''''''''''''  SE AGREGA PARA EL CASO REPORTADO POR VICTOR MORENO
                'Dim p_shapefile_diss = "Areainter_" & v_codigo '& fecha_archi_sup
                'cls_eval.Geoprocesamiento_temas("dissolve", pApp, p_shapefile_diss)

                'cls_Catastro.Add_ShapeFile_tmp("Areainter_" & v_codigo & fecha_archi_sup & "_", pApp)
                'cls_eval.agregacampotema_tpm(p_shapefile_diss, "Areaint")

                'cls_Catastro.Add_ShapeFile("Areainter_" & v_codigo & fecha_archi_sup & "_", pApp)
                'cls_Catastro.Update_Value_DM_areaint(pApp, p_shapefile_diss, "")

                '''''''''''''''''''''''''''''''''''''''''

                pMxDoc.ActiveView.Refresh()
                cls_eval.calculaareapoligonos("Areainter_" & v_codigo, "")
                arch_cata = "union"
                cls_eval.Geoprocesamiento_temas("union", pApp, "DM")
                cls_eval.calculaareapoligonos("Areadispo_" & v_codigo, "")

                cls_Catastro.Quitar_Layer("DM_" & v_codigo, pApp)
                cls_Catastro.Quitar_Layer("Prioritarios" & v_codigo & fecha_archi_prioritario, pApp)
                'cls_Catastro.Quitar_Layer("Prioritarios" & gloNumprioritario, pApp)
                cls_Catastro.Quitar_Layer("Areadispo", pApp)
                pMxDoc.ActivatedView.Refresh()
                'v_cantiprioritarios = 0
                Return "1"
            Else
                Return ""
                Exit Function

            End If
        Else
            v_cantiprioritarios = 0
            MsgBox("No genero la opción de Evaluación de DM", MsgBoxStyle.Critical, "BDGEOCATMIN")
            Return ""
        End If
    End Function

    Public Sub Inserta_Resultados_DM(ByVal lodtPrioritario As Object, ByVal lodtPosterior As Object, ByVal lodtSimultaneo As Object, ByVal lodtExtinguido As Object, ByVal lodtRedenuncio As Object, ByVal p_Codigo As Object, ByVal p_Concesion As Object, ByVal p_App As IApplication)
        Dim dr As DataRow
        pMap = pMxDoc.FocusMap
        Dim pFeatureCursor As IFeatureCursor
        Dim pFields As IFields
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim pfeatureclass As IFeatureClass
        Dim afound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Catastro" Then
                pFeatLayer = pMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra el Layer")
            Exit Sub
        End If
        pfeatureclass = pFeatLayer.FeatureClass
        pFields = pfeatureclass.Fields
        pFeatureCursor = pfeatureclass.Search(Nothing, False)
        pFeature = pFeatureCursor.NextFeature
        Dim lointPR, lointPO, lointSI, lointEX, lointRD As Integer
        lointPR = 0 : lointPO = 0 : lointSI = 0 : lointEX = 0 : lointRD = 0
        Try
            Inicializa_Datatable(lodtPrioritario) : Inicializa_Datatable(lodtPosterior)
            Inicializa_Datatable(lodtSimultaneo) : Inicializa_Datatable(lodtExtinguido) : Inicializa_Datatable(lodtRedenuncio)
            Do Until pFeature Is Nothing
                If pFeature.Value(pFields.FindField("LEYENDA")) = "G6" Then
                    p_Codigo.text = pFeature.Value(pFields.FindField("CODIGOU"))
                    p_Concesion.text = pFeature.Value(pFields.FindField("CONCESION"))
                End If
                Select Case pFeature.Value(pFields.FindField("EVAL"))
                    Case "PR"
                        'DERECHOS MINEROS PRIORITARIOS
                        dr = lodtPrioritario.NewRow
                        'Data_Datatable(dr, lointPR + 1, pFeature.Value(pFields.FindField("EVAL")), pFeature.Value(pFields.FindField("ESTADO")), pFeature.Value(pFields.FindField("CONCESION")), pFeature.Value(pFields.FindField("CODIGOU")))  'se cambio por la siguiente linea campo contador
                        Data_Datatable(dr, pFeature.Value(pFields.FindField("CONTADOR")), pFeature.Value(pFields.FindField("EVAL")), pFeature.Value(pFields.FindField("ESTADO")), pFeature.Value(pFields.FindField("CONCESION")), pFeature.Value(pFields.FindField("CODIGOU")))
                        lodtPrioritario.Rows.Add(dr)
                        lointPR += 1
                    Case "PO"
                        'DERECHOS MINEROS POSTERIORES
                        dr = lodtPosterior.NewRow
                        'Data_Datatable(dr, lointPO + 1, pFeature.Value(pFields.FindField("EVAL")), pFeature.Value(pFields.FindField("ESTADO")), pFeature.Value(pFields.FindField("CONCESION")), pFeature.Value(pFields.FindField("CODIGOU")))
                        Data_Datatable(dr, pFeature.Value(pFields.FindField("CONTADOR")), pFeature.Value(pFields.FindField("EVAL")), pFeature.Value(pFields.FindField("ESTADO")), pFeature.Value(pFields.FindField("CONCESION")), pFeature.Value(pFields.FindField("CODIGOU")))
                        lodtPosterior.Rows.Add(dr)
                        lointPO += 1
                    Case "SI"
                        ' DERECHOS MINEROS SIMULTANEOS
                        dr = lodtSimultaneo.NewRow
                        'Data_Datatable(dr, lointSI + 1, pFeature.Value(pFields.FindField("EVAL")), pFeature.Value(pFields.FindField("ESTADO")), pFeature.Value(pFields.FindField("CONCESION")), pFeature.Value(pFields.FindField("CODIGOU")))
                        Data_Datatable(dr, pFeature.Value(pFields.FindField("CONTADOR")), pFeature.Value(pFields.FindField("EVAL")), pFeature.Value(pFields.FindField("ESTADO")), pFeature.Value(pFields.FindField("CONCESION")), pFeature.Value(pFields.FindField("CODIGOU")))
                        lodtSimultaneo.Rows.Add(dr)
                        lointSI += 1
                    Case "EX"
                        'DERECHOS MINEROS EXTINGUIDOS
                        dr = lodtExtinguido.NewRow
                        'Data_Datatable(dr, lointEX + 1, pFeature.Value(pFields.FindField("EVAL")), pFeature.Value(pFields.FindField("ESTADO")), pFeature.Value(pFields.FindField("CONCESION")), pFeature.Value(pFields.FindField("CODIGOU")))
                        Data_Datatable(dr, pFeature.Value(pFields.FindField("CONTADOR")), pFeature.Value(pFields.FindField("EVAL")), pFeature.Value(pFields.FindField("ESTADO")), pFeature.Value(pFields.FindField("CONCESION")), pFeature.Value(pFields.FindField("CODIGOU")))
                        lodtExtinguido.Rows.Add(dr)
                        lointEX += 1
                    Case "AR"
                        'DERECHOS MINEROS ANTECESOR
                        dr = lodtRedenuncio.NewRow
                        'Data_Datatable(dr, lointEX + 1, pFeature.Value(pFields.FindField("EVAL")), pFeature.Value(pFields.FindField("ESTADO")), pFeature.Value(pFields.FindField("CONCESION")), pFeature.Value(pFields.FindField("CODIGOU")))
                        Data_Datatable(dr, pFeature.Value(pFields.FindField("CONTADOR")), pFeature.Value(pFields.FindField("EVAL")), pFeature.Value(pFields.FindField("ESTADO")), pFeature.Value(pFields.FindField("CONCESION")), pFeature.Value(pFields.FindField("CODIGOU")))
                        lodtRedenuncio.Rows.Add(dr)
                        lointRD += 1
                End Select
                pFeature = pFeatureCursor.NextFeature
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Data_Datatable(ByVal dr As DataRow, ByVal lointNum As Integer, ByVal lostrEvaluacion As String, ByVal lostrEstado As String, ByVal lostrConcesion As String, ByVal lostrCodigo As String)
        dr.Item(0) = lointNum
        dr.Item(1) = lostrEvaluacion
        dr.Item(2) = lostrEstado
        dr.Item(3) = lostrConcesion
        dr.Item(4) = lostrCodigo
    End Sub
    Private Sub Inicializa_Datatable(ByVal lobtTemporal As DataTable)
        lobtTemporal.Columns.Add("NUM")
        lobtTemporal.Columns.Add("EVALUACION")
        lobtTemporal.Columns.Add("ESTADO")
        lobtTemporal.Columns.Add("CONCESION")
        lobtTemporal.Columns.Add("CODIGO")
    End Sub

    Public Sub agregacampotema()

        'Programa para borrar campos de los dm interceptados

        Dim i As Integer
        Dim fclas_tema As IFeatureClass
        Dim pFields As IFields
        Dim pField As IField
        Dim pFieldEdit As IFieldEdit
        pField = New Field
        pFieldEdit = pField

        Dim pFeatLayer As IFeatureLayer = Nothing
        pMap = pMxDoc.FocusMap
        Dim afound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Areainter_" & v_codigo Then
                pFeatLayer = pMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra la capa de Derechos Mineros para realizar el calculo de área disponible", MsgBoxStyle.Information, "Observación...")
            Exit Sub
        End If

        fclas_tema = pFeatLayer.FeatureClass
        pFields = fclas_tema.Fields
        i = fclas_tema.FindField("LEYENDA1")
        pField = pFields.Field(i)
        fclas_tema.DeleteField(pField)

        'Crea campo al tema

        'campo1 = New Field
        pFieldEdit = pField
        With pFieldEdit
            .Type_2 = esriFieldType.esriFieldTypeDouble
            .Name_2 = "AREA_FINAL"
            .Precision_2 = 10
            .Scale_2 = 4
        End With
        fclas_tema.AddField(pFieldEdit)


    End Sub

    Public Sub agregacampotema_tpm(ByVal p_Shapefile As String, ByVal caso As String)
        'Programa para borrar campos de los dm interceptados
        Dim i As Integer
        Dim fclas_tema As IFeatureClass
        Dim pFields As IFields
        Dim pField As IField
        Dim pFieldEdit As IFieldEdit
        pField = New Field
        pFieldEdit = pField
        Dim pFeatureCursor As IFeatureCursor
        Dim pFeatLayer As IFeatureLayer = Nothing
        pMap = pMxDoc.FocusMap
        Dim afound As Boolean = False
        'pFeatLayer = pFeatureLayer_tmp
        If caso = "AREA RESERVADA" Or caso = "ZONA URBANA" Or caso = "CODIGO" Or caso = "estamin" Then
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name.ToUpper = p_Shapefile.ToUpper Then
                    pFeatLayer = pMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra la capa en la vista", MsgBoxStyle.Information, "Observación...")
                Exit Sub
            End If
        Else  'para difeente a reservas
            pFeatLayer = pFeatureLayer_tmp
        End If
        fclas_tema = pFeatLayer.FeatureClass

        'Crea campo al tema
        'campo1 = New Field
        ' Dim pQueryFilter As IQueryFilter
        ' If caso = "Reservas" Then

        If caso = "CODIGO" Then  'ESTA OPCION SOLO CREARA CAMPO CODIGO A LAS AREAS RESTRINGIDAS
            pFeatureCursor = fclas_tema.Search(Nothing, False)
            If pFeatureCursor.FindField("CODIGO") = -1 Then
                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "CODIGO"
                    .Precision_2 = 16

                End With
                fclas_tema.AddField(pFieldEdit)
            End If
        ElseIf caso = "estamin" Then
            pFeatureCursor = fclas_tema.Search(Nothing, False)
            If pFeatureCursor.FindField("TIPO") = -1 Then
                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "TIPO"
                    .Precision_2 = 60
                End With
                fclas_tema.AddField(pFieldEdit)
            End If

            pFeatureCursor = fclas_tema.Search(Nothing, False)
            If pFeatureCursor.FindField("CONTADOR") = -1 Then
                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "CONTADOR"
                    .Precision_2 = 20

                End With
                fclas_tema.AddField(pFieldEdit)
            End If

        ElseIf caso = "Simultaneo" Then
            pFeatureCursor = fclas_tema.Search(Nothing, False)
            If pFeatureCursor.FindField("SUBGRUPO") = -1 Then
                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "SUBGRUPO"
                    .Precision_2 = 3
                End With
                fclas_tema.AddField(pFieldEdit)
            End If
        ElseIf caso = "Cuadri_sim" Then
            pFeatureCursor = fclas_tema.Search(Nothing, False)
            If pFeatureCursor.FindField("SUBGRUPO") = -1 Then

                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "SUBGRUPO"
                    .Precision_2 = 3
                End With
                fclas_tema.AddField(pFieldEdit)
            End If
        ElseIf caso = "Cuadri_dsim" Then
            pFeatureCursor = fclas_tema.Search(Nothing, False)
            If pFeatureCursor.FindField("CD_CUAD") = -1 Then
                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "CD_CUAD"
                    .Precision_2 = 12
                End With
                fclas_tema.AddField(pFieldEdit)

                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "SUBGRUPO"
                    .Precision_2 = 3
                End With
                fclas_tema.AddField(pFieldEdit)
            End If
        ElseIf caso = "Cata_sim" Then
            pFeatureCursor = fclas_tema.Search(Nothing, False)
            If pFeatureCursor.FindField("LEYENDA") = -1 Then

                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "LEYENDA"
                    .Precision_2 = 4
                End With
                fclas_tema.AddField(pFieldEdit)
            End If
        ElseIf caso = "Areaint" Then
            pFeatureCursor = fclas_tema.Search(Nothing, False)
            If pFeatureCursor.FindField("CD_CUAD") = -1 Then
                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "CD_CUAD"
                    .Precision_2 = 12
                End With
                fclas_tema.AddField(pFieldEdit)

                'pFieldEdit = pField
                'With pFieldEdit
                '    .Type_2 = esriFieldType.esriFieldTypeString
                '    .Name_2 = "CODIGOU_2"
                '    .Precision_2 = 13
                'End With
                'fclas_tema.AddField(pFieldEdit)

                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "CONCESIO_1"
                    .Precision_2 = 50
                End With
                fclas_tema.AddField(pFieldEdit)
            End If
        ElseIf caso = "Acceditario" Then
            pFeatureCursor = fclas_tema.Search(Nothing, False)
            If pFeatureCursor.FindField("FEC_RET") = -1 Then
                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "FEC_RET"
                    .Precision_2 = 20
                End With
                fclas_tema.AddField(pFieldEdit)
            End If
        Else
            If tipo_catanominero = "AREA RESERVADA" Then
                'pQueryFilter = New QueryFilter
                pFeatureCursor = fclas_tema.Search(Nothing, False)
                If pFeatureCursor.FindField("CODIGO") = -1 Then
                    pFieldEdit = pField
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "CODIGO"
                        .Precision_2 = 16
                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If
                pFieldEdit = pField
                If pFeatureCursor.FindField("NOMBRE") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "NOMBRE"
                        .Precision_2 = 80
                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField
                If pFeatureCursor.FindField("AREA") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeDouble
                        .Name_2 = "AREA"
                        .Precision_2 = 20
                        .Scale_2 = 4
                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If
                pFieldEdit = pField

                If pFeatureCursor.FindField("NM_RESE") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "NM_RESE"
                        .Precision_2 = 100
                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If
                pFieldEdit = pField

                If pFeatureCursor.FindField("TP_RESE") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "TP_RESE"
                        .Precision_2 = 40

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField

                If pFeatureCursor.FindField("CATEGORI") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "CATEGORI"
                        .Precision_2 = 50

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField
                If pFeatureCursor.FindField("CONTADOR") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "CONTADOR"
                        .Precision_2 = 20

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField
                If pFeatureCursor.FindField("CLASE") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "CLASE"
                        .Precision_2 = 30

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If
                pFieldEdit = pField
                If pFeatureCursor.FindField("ZONA") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "ZONA"
                        .Precision_2 = 20

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If
                pFieldEdit = pField
                If pFeatureCursor.FindField("HAS") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeDouble
                        .Name_2 = "HAS"
                        .Precision_2 = 20
                        .Scale_2 = 4
                    End With
                    fclas_tema.AddField(pFieldEdit)

                End If
                pFieldEdit = pField
                If pFeatureCursor.FindField("ZONEX") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "ZONEX"
                        .Precision_2 = 10

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField
                If pFeatureCursor.FindField("ARCHIVO") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "ARCHIVO"
                        .Precision_2 = 256

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If
                pFieldEdit = pField
                If pFeatureCursor.FindField("NORMA") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "NORMA"
                        .Precision_2 = 100

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField

                If pFeatureCursor.FindField("FEC_ING") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "FEC_ING"
                        .Precision_2 = 100

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField

                If pFeatureCursor.FindField("FEC_PUB") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeDate
                        .Name_2 = "FEC_PUB"
                        .Precision_2 = 100

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField

                If pFeatureCursor.FindField("ENTIDAD") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "ENTIDAD"
                        .Precision_2 = 80

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If
                pFieldEdit = pField
                If pFeatureCursor.FindField("USO") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "USO"
                        .Precision_2 = 20

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If


                pFieldEdit = pField
                If pFeatureCursor.FindField("EST_GRAF") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "EST_GRAF"
                        .Precision_2 = 10

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField

                If pFeatureCursor.FindField("LEYENDA") = -1 Then
                    pFieldEdit = pField
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "LEYENDA"
                        .Precision_2 = 10

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

            ElseIf tipo_catanominero = "ZONA URBANA" Then   'CREA LOS CAMPOS PARA CAPA DE ZONAS URBANAS
                pFeatureCursor = fclas_tema.Search(Nothing, False)

                If pFeatureCursor.FindField("CODIGO") = -1 Then

                    pFieldEdit = pField
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "CODIGO"
                        .Precision_2 = 16

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField


                If pFeatureCursor.FindField("NOMBRE") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "NOMBRE"
                        .Precision_2 = 80

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If


                pFieldEdit = pField
                If pFeatureCursor.FindField("AREA") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeDouble
                        .Name_2 = "AREA"
                        .Precision_2 = 20
                        .Scale_2 = 4
                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField

                If pFeatureCursor.FindField("NM_URBA") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "NM_URBA"
                        .Precision_2 = 100

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If
                pFieldEdit = pField

                If pFeatureCursor.FindField("TP_URBA") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "TP_URBA"
                        .Precision_2 = 40

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField

                If pFeatureCursor.FindField("CATEGORI") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "CATEGORI"
                        .Precision_2 = 50

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField
                If pFeatureCursor.FindField("CONTADOR") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "CONTADOR"
                        .Precision_2 = 20

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField
                If pFeatureCursor.FindField("ORDENANZA") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "ORDENANZA"
                        .Precision_2 = 30

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If
                pFieldEdit = pField
                If pFeatureCursor.FindField("ZONA") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "ZONA"
                        .Precision_2 = 20

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If
                pFieldEdit = pField
                If pFeatureCursor.FindField("HAS") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeDouble
                        .Name_2 = "HAS"
                        .Precision_2 = 20
                        .Scale_2 = 4
                    End With
                    fclas_tema.AddField(pFieldEdit)

                End If
                pFieldEdit = pField
                If pFeatureCursor.FindField("ZONEX") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "ZONEX"
                        .Precision_2 = 10

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField
                If pFeatureCursor.FindField("ARCHIVO") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "ARCHIVO"
                        .Precision_2 = 256

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If
                pFieldEdit = pField
                If pFeatureCursor.FindField("NORMA") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "NORMA"
                        .Precision_2 = 100

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField

                If pFeatureCursor.FindField("FEC_ING") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "FEC_ING"
                        .Precision_2 = 100

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField


                If pFeatureCursor.FindField("FEC_PUB") = -1 Then
                    With pFieldEdit
                        ' .Type_2 = esriFieldType.esriFieldTypeString
                        .Type_2 = esriFieldType.esriFieldTypeDate
                        .Name_2 = "FEC_PUB"
                        .Precision_2 = 100

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField

                If pFeatureCursor.FindField("ENTIDAD") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "ENTIDAD"
                        .Precision_2 = 80

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If
                pFieldEdit = pField
                If pFeatureCursor.FindField("USO") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "USO"
                        .Precision_2 = 20

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If


                pFieldEdit = pField
                If pFeatureCursor.FindField("EST_GRAF") = -1 Then
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "EST_GRAF"
                        .Precision_2 = 10

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If

                pFieldEdit = pField

                If pFeatureCursor.FindField("LEYENDA") = -1 Then
                    pFieldEdit = pField
                    With pFieldEdit
                        .Type_2 = esriFieldType.esriFieldTypeString
                        .Name_2 = "LEYENDA"
                        .Precision_2 = 10

                    End With
                    fclas_tema.AddField(pFieldEdit)
                End If


                '  pFeaturepout = pFeat

            Else  'para tema catastro

                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "EVAL"
                    .Precision_2 = 10

                End With
                fclas_tema.AddField(pFieldEdit)


                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "CALCULO"
                    .Precision_2 = 10

                End With
                fclas_tema.AddField(pFieldEdit)

                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeDouble
                    .Name_2 = "AREA_INT"
                    .Precision_2 = 20
                    .Scale_2 = 4
                End With
                fclas_tema.AddField(pFieldEdit)

                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "DPTO"
                    .Precision_2 = 10

                End With
                fclas_tema.AddField(pFieldEdit)

                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "PROV"
                    .Precision_2 = 40

                End With
                fclas_tema.AddField(pFieldEdit)
                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "DIST"
                    .Precision_2 = 50

                End With
                fclas_tema.AddField(pFieldEdit)

                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "CONTADOR"
                    .Precision_2 = 20

                End With
                fclas_tema.AddField(pFieldEdit)

                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString

                    .Name_2 = "NUM_RESOL"
                    .Precision_2 = 30

                End With
                fclas_tema.AddField(pFieldEdit)

                pFieldEdit = pField
                With pFieldEdit
                    ' .Type_2 = esriFieldType.esriFieldTypeString
                    .Type_2 = esriFieldType.esriFieldTypeDate
                    .Name_2 = "FEC_RESOL"
                    .Precision_2 = 20

                End With
                fclas_tema.AddField(pFieldEdit)


                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "CALIF"
                    .Precision_2 = 10

                End With
                fclas_tema.AddField(pFieldEdit)


                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "DISTS"
                    .Precision_2 = 256

                End With
                fclas_tema.AddField(pFieldEdit)
                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "PROVS"
                    .Precision_2 = 256

                End With
                fclas_tema.AddField(pFieldEdit)

                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "DPTOS"
                    .Precision_2 = 256

                End With
                fclas_tema.AddField(pFieldEdit)

                pFieldEdit = pField
                With pFieldEdit
                    .Type_2 = esriFieldType.esriFieldTypeString
                    .Name_2 = "TIPO"
                    .Precision_2 = 80

                End With
                fclas_tema.AddField(pFieldEdit)

            End If
        End If

        fclas_tema = Nothing
        pFieldEdit = Nothing

    End Sub

    Public Sub borracampostema()
        'Programa para borrar campos de los dm interceptados
        'Dim i As Integer
        Dim fclas_tema As IFeatureClass
        Dim pFields2 As IFields
        Dim pField2 As IField
        Dim pFieldEdit2 As IFieldEdit
        pField2 = New Field
        pFieldEdit2 = pField2
        Dim pFeatLayer As IFeatureLayer = Nothing
        pMap = pMxDoc.FocusMap
        Dim afound As Boolean = False
        Try
            If arch_cata = "Interceccion" Then
                For A As Integer = 0 To pMap.LayerCount - 1
                    If pMap.Layer(A).Name = "Areainter_" & v_codigo Then
                        pFeatLayer = pMap.Layer(A)
                        afound = True
                        Exit For
                    End If
                Next A

            ElseIf arch_cata = "union" Then
                For A As Integer = 0 To pMap.LayerCount - 1
                    If pMap.Layer(A).Name = "Areadispo_" & v_codigo Then
                        pFeatLayer = pMap.Layer(A)
                        afound = True
                        Exit For
                    End If
                Next A
            End If
            If Not afound Then
                MsgBox("No se encuentra la capa de Derechos Mineros para realizar el calculo de área disponible", MsgBoxStyle.Information, "Observación...")
                Exit Sub
            End If
            fclas_tema = pFeatLayer.FeatureClass

            pFields2 = fclas_tema.Fields


            'Borra los campos de forma masiva - mejora
            For k As Integer = pFields2.FieldCount - 1 To 5 Step -1   'posicion 5 es porque quedan los campos
                'For k As Integer = 1 To pFields2.FieldCount - 1  'posicion 5 es porque quedan los campos
                Try
                    pField2 = pFields2.Field(k)
                    Select Case pField2.Name
                        Case "CODIGOU", "CONTADOR", "Shape", "OBJECTID", "CONCESIO_1", "CODIGOU_1"
                        Case Else
                            fclas_tema.DeleteField(pField2)
                    End Select
                Catch ex As Exception
                    MsgBox("Error en eliminar los campos del tema", MsgBoxStyle.Critical, "Observación...")
                End Try
            Next k


            ''i = fclas_tema.FindField("DIST")
            ''pField = pFields.Field(i)
            ''fclas_tema.DeleteField(pField)
            'Termino de borrar los campos que no sirven
            'Crea campos al tema
            pFieldEdit2 = pField2
            With pFieldEdit2
                .Type_2 = esriFieldType.esriFieldTypeDouble
                .Name_2 = "AREA_FINAL"
                .Precision_2 = 10
                .Scale_2 = 4
            End With
            fclas_tema.AddField(pFieldEdit2)

            'With pFieldEdit2
            '.Type_2 = esriFieldType.esriFieldTypeString
            '.Name_2 = "CONTADOR"
            '.Length_2 = 8
            'End With
            'fclas_tema.AddField(pFieldEdit2)


        Catch ex As Exception
            MsgBox("No ha terminado de eliminar los campos", vbCritical, "Observacion...")
        End Try

    End Sub

    Public Sub Geoprocesamiento_temas_sim(ByVal tema_procesing As String, ByVal p_App As IApplication, ByVal tipo_capa As String)

        ' Part 1: Define the input table.
        Dim pMxDoc As IMxDocument
        Dim pMap As IMap
        Dim pInputFeatLayer As IFeatureLayer
        Dim pInputTable As ITable
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        pInputFeatLayer = pMap.Layer(0)
        pInputTable = pInputFeatLayer

        ' Part 2: Define the output dataset.
        Dim pNewWSName As IWorkspaceName
        Dim pFCName As IFeatureClassName
        Dim pDatasetName As IDatasetName
        pFCName = New FeatureClassName
        pDatasetName = pFCName
        pNewWSName = New WorkspaceName
        pNewWSName.WorkspaceFactoryProgID = "esriCore.ShapefileWorkspaceFactory"
        pNewWSName.PathName = glo_pathTMP & "\"
        'pNewWSName.PathName = "c:\data\chap10"
        'pDatasetName.Name = "Cuadri_dsim" & fecha_archi & ""
        pDatasetName.Name = tipo_capa
        pDatasetName.WorkspaceName = pNewWSName

        ' Part 3: Perform dissolve.
        Dim pBGP As IBasicGeoprocessor
        Dim pOutputFC As IFeatureClass
        pBGP = New BasicGeoprocessor
        ' Run dissolve.
        pOutputFC = pBGP.Dissolve(pInputTable, False, "Zona", "Dissolve.Shape, Sum.Shape_area", pFCName)
        pInputFeatLayer = Nothing
        pInputFeatLayer = Nothing
        pInputTable = Nothing
        pFCName = Nothing
        pNewWSName = Nothing
        pDatasetName = Nothing
        pBGP = Nothing
        pOutputFC = Nothing
        pMap = Nothing
        pMxDoc = Nothing

        ' Part 4: Create the output feature layer and add the layer to the active map.
        'Dim pOutputFeatLayer As IFeatureLayer
        'pOutputFeatLayer = New FeatureLayer
        'pOutputFeatLayer.FeatureClass = pOutputFC
        'pOutputFeatLayer.Name = pOutputFC.AliasName
        'pMap.AddLayer(pOutputFeatLayer)

    End Sub

    Public Sub Geoprocesamiento_temas_int(ByVal tema_procesing As String, ByVal p_App As IApplication, ByVal tipo_capa As String)

        ' Part 1: Define the input table.
        Dim pMxDoc As IMxDocument
        Dim pMap As IMap
        Dim pInputLayer As IFeatureLayer
        Dim pOverlayLayer As IFeatureLayer
        Dim pInputFC As IFeatureClass
        Dim pOverlayFC As IFeatureClass
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        pInputLayer = pMap.Layer(0)
        pInputFC = pInputLayer.FeatureClass
        ' Define the overlay feature class.
        pOverlayLayer = pMap.Layer(1)
        pOverlayFC = pOverlayLayer.FeatureClass

        ' Part 2: Define the output dataset.
        Dim pNewWSName As IWorkspaceName
        Dim pFCName As IFeatureClassName
        Dim pDatasetName As IDatasetName
        pFCName = New FeatureClassName
        pDatasetName = pFCName
        pNewWSName = New WorkspaceName
        pNewWSName.WorkspaceFactoryProgID = "esriCore.ShapeFileWorkspaceFactory"
        pNewWSName.PathName = glo_pathTMP & "\"
        pDatasetName.WorkspaceName = pNewWSName
        'pNewWSName.PathName = "c:\data\chap10"
        'pDatasetName.Name = "dmsim_int" & fecha_archi & ""
        pDatasetName.Name = "Cuadri_dsim" & fecha_archi & ""

        ' Part 3: Perform intersect.
        Dim pBGP As IBasicGeoprocessor
        Dim tol As Double
        Dim pOutputFC As IFeatureClass
        ' Define a basic geoprocessor object.
        pBGP = New BasicGeoprocessor
        ' Use the default tolerance.
        tol = 0.0#
        ' Run intersect.
        pOutputFC = pBGP.Intersect(pInputFC, False, pOverlayFC, False, tol, pFCName)

        ' Part 4: Create the output layer and add it to the active map.
        Dim pOutputFeatLayer As IFeatureLayer
        pOutputFeatLayer = New FeatureLayer
        pOutputFeatLayer.FeatureClass = pOutputFC
        pOutputFeatLayer.Name = pOutputFC.AliasName
        pMap.AddLayer(pOutputFeatLayer)

    End Sub

    Public Sub Geoprocesamiento_temas_interseccion(ByVal tema_procesing As String, ByVal p_App As IApplication, ByVal tipo_capa As String)

        ' Part 1: Define the input table.
        Dim pMxDoc As IMxDocument
        Dim pMap As IMap
        Dim pInputLayer As IFeatureLayer
        Dim pOverlayLayer As IFeatureLayer
        Dim pInputFC As IFeatureClass
        Dim pOverlayFC As IFeatureClass

        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap

        Dim afound As Boolean
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = tipo_capa Then
                pInputLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("Layer No Existe para intercertar.")
            Exit Sub
        End If

        '  pInputLayer = pMap.Layer(0)
        pInputFC = pInputLayer.FeatureClass
        ' Define the overlay feature class.

        'capa 1

        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Zona Reservada" Then
                pOverlayLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("Layer No Existe capa de reservas para intercertar.")
            Exit Sub
        End If

        ' pOverlayLayer = pMap.Layer(1)
        pOverlayFC = pOverlayLayer.FeatureClass

        ' Part 2: Define the output dataset.
        Dim pNewWSName As IWorkspaceName
        Dim pFCName As IFeatureClassName
        Dim pDatasetName As IDatasetName
        pFCName = New FeatureClassName
        pDatasetName = pFCName
        pNewWSName = New WorkspaceName
        pNewWSName.WorkspaceFactoryProgID = "esriCore.ShapeFileWorkspaceFactory"
        pNewWSName.PathName = glo_pathTMP & "\"
        pDatasetName.WorkspaceName = pNewWSName
        'pNewWSName.PathName = "c:\data\chap10"
        'pDatasetName.Name = "dmsim_int" & fecha_archi & ""
        pDatasetName.Name = "rese_acum" & fecha_archi & ""

        ' Part 3: Perform intersect.
        Dim pBGP As IBasicGeoprocessor
        Dim tol As Double
        Dim pOutputFC As IFeatureClass
        ' Define a basic geoprocessor object.
        pBGP = New BasicGeoprocessor
        ' Use the default tolerance.
        tol = 0.01
        ' Run intersect.
        pOutputFC = pBGP.Intersect(pInputFC, False, pOverlayFC, False, tol, pFCName)

        ' Part 4: Create the output layer and add it to the active map.
        Dim pOutputFeatLayer As IFeatureLayer
        pOutputFeatLayer = New FeatureLayer
        pOutputFeatLayer.FeatureClass = pOutputFC
        pOutputFeatLayer.Name = pOutputFC.AliasName
        pMap.AddLayer(pOutputFeatLayer)

    End Sub

    Public Sub Geoprocesamiento_temas_interseccion1(ByVal capa_output As String, ByVal p_App As IApplication, ByVal capa_input As String, ByVal capa_over As String)

        ' Part 1: Define the input table.
        Dim pMxDoc As IMxDocument
        Dim pMap As IMap
        Dim pInputLayer As IFeatureLayer
        Dim pOverlayLayer As IFeatureLayer
        Dim pInputFC As IFeatureClass
        Dim pOverlayFC As IFeatureClass

        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap

        Dim afound As Boolean
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = capa_input Then
                'If pMap.Layer(A).Name = tipo_capa Then
                pInputLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No Existe capa para interceptar.")
            Exit Sub
        End If

        '  pInputLayer = pMap.Layer(0)
        pInputFC = pInputLayer.FeatureClass
        ' Define the overlay feature class.

        'capa 1

        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = capa_over Then
                'If pMap.Layer(A).Name = "Zona Reservada" Then
                pOverlayLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No Existe capa para interceptar.")
            Exit Sub
        End If

        ' pOverlayLayer = pMap.Layer(1)
        pOverlayFC = pOverlayLayer.FeatureClass

        ' Part 2: Define the output dataset.
        Dim pNewWSName As IWorkspaceName
        Dim pFCName As IFeatureClassName
        Dim pDatasetName As IDatasetName
        pFCName = New FeatureClassName
        pDatasetName = pFCName
        pNewWSName = New WorkspaceName
        pNewWSName.WorkspaceFactoryProgID = "esriCore.ShapeFileWorkspaceFactory"
        pNewWSName.PathName = glo_pathTMP & "\"
        pDatasetName.WorkspaceName = pNewWSName
        'pNewWSName.PathName = "c:\data\chap10"
        'pDatasetName.Name = "dmsim_int" & fecha_archi & ""
        pDatasetName.Name = capa_output
        'pDatasetName.Name = "rese_acum" & fecha_archi & ""

        ' Part 3: Perform intersect.
        Dim pBGP As IBasicGeoprocessor
        Dim tol As Double
        Dim pOutputFC As IFeatureClass
        ' Define a basic geoprocessor object.
        pBGP = New BasicGeoprocessor
        ' Use the default tolerance.
        tol = 0.01
        ' Run intersect.
        pOutputFC = pBGP.Intersect(pInputFC, False, pOverlayFC, False, tol, pFCName)

        ' Part 4: Create the output layer and add it to the active map.
        Dim pOutputFeatLayer As IFeatureLayer
        pOutputFeatLayer = New FeatureLayer
        pOutputFeatLayer.FeatureClass = pOutputFC
        pOutputFeatLayer.Name = pOutputFC.AliasName
        pMap.AddLayer(pOutputFeatLayer)

    End Sub

    Public Sub Geoprocesamiento_temas2(ByVal capa_output As String, ByVal p_App As IApplication, ByVal capa_input As String, ByVal capa_over As String, ByVal geoproceso As String)
        ' Part 1: Define the input table.
        Dim pMxDoc As IMxDocument
        Dim pMap As IMap
        Dim pInputLayer As IFeatureLayer
        Dim pOverlayLayer As IFeatureLayer
        Dim pInputFC As IFeatureClass
        Dim pOverlayFC As IFeatureClass

        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap

        Dim afound As Boolean
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = capa_input Then
                pInputLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No Existe capa para interceptar.")
            Exit Sub
        End If
        pInputFC = pInputLayer.FeatureClass

        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = capa_over Then
                pOverlayLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No Existe capa para interceptar.")
            Exit Sub
        End If
        pOverlayFC = pOverlayLayer.FeatureClass

        ' Part 2: Define the output dataset.
        Dim pNewWSName As IWorkspaceName
        Dim pFCName As IFeatureClassName
        Dim pDatasetName As IDatasetName
        pFCName = New FeatureClassName
        pDatasetName = pFCName
        pNewWSName = New WorkspaceName
        pNewWSName.WorkspaceFactoryProgID = "esriCore.ShapeFileWorkspaceFactory"
        pNewWSName.PathName = glo_pathTMP & "\"
        pDatasetName.WorkspaceName = pNewWSName
        pDatasetName.Name = capa_output

        If geoproceso = "int" Then

            ' Part 3: Perform intersect.
            Dim pBGP As IBasicGeoprocessor
            Dim tol As Double
            Dim pOutputFC As IFeatureClass
            ' Define a basic geoprocessor object.
            pBGP = New BasicGeoprocessor
            ' Use the default tolerance.
            tol = 0.01
            ' Run intersect.
            pOutputFC = pBGP.Intersect(pInputFC, False, pOverlayFC, False, tol, pFCName)

            ' Part 4: Create the output layer and add it to the active map.
            Dim pOutputFeatLayer As IFeatureLayer
            pOutputFeatLayer = New FeatureLayer
            pOutputFeatLayer.FeatureClass = pOutputFC
            pOutputFeatLayer.Name = pOutputFC.AliasName
            pMap.AddLayer(pOutputFeatLayer)

        ElseIf geoproceso = "clip" Then
            Dim pFeatClassName As IFeatureClassName
            pFeatClassName = New FeatureClassName
            With pFeatClassName
                .FeatureType = esriFeatureType.esriFTSimple   'esriFTSimple
                .ShapeFieldName = "Shape"
                .ShapeType = pOverlayFC.ShapeType
            End With

            ' Perform the clip
            Dim pBGP As IBasicGeoprocessor
            pBGP = New BasicGeoprocessor
            Dim pOutputFC As IFeatureClass
            Dim tol As Double
            tol = 0.0#

            pOutputFC = pBGP.Clip(pInputFC, False, pOverlayFC, False, tol, pFCName)

            ' Add the output layer (clipped features) to the map
            Dim pOutputFeatLayer As IFeatureLayer
            pOutputFeatLayer = New FeatureLayer
            pOutputFeatLayer.FeatureClass = pOutputFC
            pOutputFeatLayer.Name = pOutputFC.AliasName
            pMxDoc.FocusMap.AddLayer(pOutputFeatLayer)

        End If

    End Sub

    Public Sub Geoprocesamiento_temas_convert(ByVal tema_procesing As String, ByVal p_App As IApplication, ByVal tipo_capa As String)

        ' Part 1: Define the INPUT.
        Dim pWorkspaceName As IWorkspaceName
        Dim pFeatureClassName As IFeatureClassName
        Dim pDatasetName As IDatasetName
        'Define the workspace.
        pWorkspaceName = New WorkspaceName
        pWorkspaceName.WorkspaceFactoryProgID = "esricore.AccessWorkspaceFactory"

        If v_sistema = "PSAD-56" Then
            pWorkspaceName.PathName = "c:\BDGEOCATMIN\BDGEOCATMIN.mdb"
        Else
            pWorkspaceName.PathName = "c:\BDGEOCATMIN\BDGEOCATMIN_84.mdb"
        End If
        'pWorkspaceName.PathName = "c:\BDGEOCATMIN\BDGEOCATMIN_84.mdb"

        ' Define the dataset.
        pFeatureClassName = New FeatureClassName
        pDatasetName = pFeatureClassName
        pDatasetName.WorkspaceName = pWorkspaceName
        pDatasetName.Name = "Catastro_" & v_zona_dm & ""

        ' Part 2: Define the OUTPUT.
        Dim pOutShpWorkspaceName As IWorkspaceName
        Dim pFCName As IFeatureClassName
        Dim pShpDatasetName As IDatasetName
        ' Define the workspace.
        pOutShpWorkspaceName = New WorkspaceName
        pOutShpWorkspaceName.PathName = "C:\BDGEOCATMIN\Temporal"
        pOutShpWorkspaceName.WorkspaceFactoryProgID = "esriCore.ShapefileWorkspaceFactory"
        ' Define the dataset.
        pFCName = New FeatureClassName
        pShpDatasetName = pFCName
        'pShpDatasetName.Name = "Acumulacion" & fecha_archi_sim & ""
        'pShpDatasetName.Name = "Acumulacion.shp"
        pShpDatasetName.Name = tipo_capa & ".shp"
        pShpDatasetName.WorkspaceName = pOutShpWorkspaceName

        ' Part 3: Perform data conversion.
        Dim pFCToShp As IFeatureDataConverter
        pFCToShp = New FeatureDataConverter
        pFCToShp.ConvertFeatureClass(pFeatureClassName, Nothing, Nothing, pFCName, Nothing, Nothing, "", 1000, 0)
        'MsgBox("Access to Shp conversion complete!")

    End Sub

    Public Sub AddFeatureClass_shp(ByVal tema_procesing As String, ByVal p_App As IApplication, ByVal tipo_capa As String)

        Dim pMxDoc As IMxDocument
        Dim pMap As IMap
        Dim pWorkspaceFactory As IWorkspaceFactory
        Dim pFeatureWorkspace As IFeatureWorkspace
        Dim pFeatureLayer As IFeatureLayer
        Dim pFeatureClass As IFeatureClass
        ' Specify the workspace and the feature class.
        pWorkspaceFactory = New ShapefileWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile("C:\BDGEOCATMIN\Temporal\", 0)
        pFeatureClass = pFeatureWorkspace.OpenFeatureClass(tipo_capa)
        ' Prepare a feature layer.
        pFeatureLayer = New FeatureLayer
        pFeatureLayer.FeatureClass = pFeatureClass
        pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName
        ' Add the feature layer to the active map.
        pMxDoc = p_App.Document
        'pMxDoc = ThisDocument
        pMap = pMxDoc.FocusMap
        pMap.AddLayer(pFeatureLayer)
        ' Refresh the active view.
        pMxDoc.ActiveView.Refresh()

    End Sub

    Public Sub calculaareapoligonos(ByVal p_layer As String, ByVal seleccion_tema As String)
        Try
            v_area_dispo = 0.0
            Dim cls_eval As New Cls_evaluacion
            Dim pFeatLayer As IFeatureLayer = Nothing
            Dim fclas_tema As IFeatureClass
            Dim fclas_tema1 As IFeatureClass
            pMap = pMxDoc.FocusMap
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = p_layer Then
                    pFeatLayer = pMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                'MsgBox("No se encuentra la capa de Derechos Mineros para realizar el calculo de área disponible", MsgBoxStyle.Information, "Observación...")
                Exit Sub
            End If
            pMxDoc.ActiveView.Refresh()
            'Calculando area de los poligonos
            fclas_tema = pFeatLayer.FeatureClass
            Dim intArea As Integer
            Dim pArea As IArea
            Dim pUpdateCursor As IFeatureCursor
            Dim l As IPolygon
            Dim coordenada_DM(300) As Punto_DM
            Dim h, j As Integer
            Dim pt As IPoint
            Dim este1 As Double
            Dim norte1 As Double
            Dim areaf1 As Double
            Dim v_area_dispo1 As Double
            Dim v_codigo_x As String
            Dim consulta1 As IQueryFilter
            Dim pUpdateFeatures1 As IFeatureCursor
            Dim pFeature_x As IFeature
            consulta1 = New QueryFilter
            Dim pFields As IFields
            Dim pFeatLayer1 As IFeatureLayer = Nothing
            pMap = pMxDoc.FocusMap
            If seleccion_tema = "evaluacion" Then
                For A As Integer = 0 To pMap.LayerCount - 1
                    If pMap.Layer(A).Name = "Catastro" Then
                        pFeatLayer1 = pMap.Layer(A)
                        fclas_tema1 = pFeatLayer1.FeatureClass
                        afound = True
                        Exit For
                    End If
                Next A
                If Not afound Then
                    MsgBox("No se encuentra el Layer")
                    Exit Sub
                End If
            End If
            If seleccion_tema = "Redenuncio" Then
                pUpdateCursor = fclas_tema.Update(Nothing, False)
                intArea = pUpdateCursor.FindField("HECTAGIS")
            ElseIf seleccion_tema = "evaluacion" Then
                pUpdateCursor = fclas_tema.Update(Nothing, False)
                pFields = fclas_tema.Fields
            Else
                cls_eval.borracampostema()


                pMxDoc.ActiveView.Refresh()
                pUpdateCursor = fclas_tema.Update(Nothing, False)
                intArea = pUpdateCursor.FindField("AREA_FINAL")
                pFields = fclas_tema.Fields
            End If
            If pFeatLayer.Name = "Areainter_" & v_codigo Then
                pFeature = pUpdateCursor.NextFeature
                Do Until pFeature Is Nothing
                    v_codigo_x = pFeature.Value(pFields.FindField("CODIGOU_1"))
                    l = pFeature.Shape
                    ptcol = l
                    ReDim coordenada_DM(ptcol.PointCount)
                    For i As Integer = 0 To ptcol.PointCount - 1
                        coordenada_DM(i).x = 0
                        coordenada_DM(i).y = 0
                    Next
                    'comienza tu segundo bucle, el que recorre los puntos de cada polilinea
                    conta_vert = 0
                    j_vert = 0
                    For j = 0 To ptcol.PointCount - 2
                        conta_vert = conta_vert + 1
                        pt = ptcol.Point(j)
                        este1 = pt.X
                        norte1 = pt.Y
                        'Redonde de coordenadas
                        norte1 = Format(Math.Round(norte1, 3), "###,###.00")
                        este1 = Format(Math.Round(este1, 3), "###,###.00")
                        coordenada_DM(j).v = j + 1
                        coordenada_DM(j).x = este1
                        coordenada_DM(j).y = norte1
                    Next j ' ----- fin de bucle interno
                    'Calcular Area según coordenadas redondeadas
                    coordenada_DM(j).x = coordenada_DM(0).x
                    coordenada_DM(j).y = coordenada_DM(0).y
                    Dim d0, d1, dr As Double
                    d0 = 0 : d1 = 0 : dr = 0
                    For h = 0 To j - 1  ' UBound(coordenada_DM) - 1
                        If coordenada_DM(h).x <> 0 Or coordenada_DM(h).y <> 0 Then
                            d0 = d0 + coordenada_DM(h).x * coordenada_DM(h + 1).y
                            d1 = d1 + coordenada_DM(h).y * coordenada_DM(h + 1).x
                        Else
                            Exit For
                        End If
                    Next h
                    dr = Math.Round(Math.Abs((d0 - d1) / 2) / 10000, 4)  'termino
                    areaf1 = Format(Math.Round(dr, 4), "###,###.0000")
                    If seleccion_tema = "evaluacion" Then
                        consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                        pUpdateFeatures1 = fclas_tema1.Update(consulta1, False)
                        intArea = pUpdateFeatures1.FindField("AREA_INT")
                        pFeature_x = pUpdateFeatures1.NextFeature
                        Do Until pFeature_x Is Nothing
                            pFeature_x.Value(intArea) = areaf1
                            pUpdateFeatures1.UpdateFeature(pFeature_x)
                            pFeature_x = pUpdateFeatures1.NextFeature
                        Loop
                    Else
                        pFeature.Value(intArea) = areaf1

                        pUpdateCursor.UpdateFeature(pFeature)
                    End If
                    pFeature = pUpdateCursor.NextFeature
                Loop
            ElseIf pFeatLayer.Name = "Areadispo_" & v_codigo Then
                If pFeatLayer.FeatureClass.FeatureCount(Nothing) > 1 Then  'Si tiene varios poligonos
                    v_area_dispo1 = 0
                    pFeature = pUpdateCursor.NextFeature
                    Do Until pFeature Is Nothing
                        pArea = pFeature.Shape
                        pFeature.Value(intArea) = (pArea.Area) / 10000
                        pUpdateCursor.UpdateFeature(pFeature)
                        v_area_dispo1 = (v_area_dispo1 + ((pArea.Area) / 10000))
                        'v_area_dispo = (pArea.Area) / 10000
                        pFeature = pUpdateCursor.NextFeature
                    Loop
                    v_area_dispo = v_area_dispo1
                Else
                    pFeature = pUpdateCursor.NextFeature
                    Do Until pFeature Is Nothing
                        l = pFeature.Shape
                        pArea = pFeature.Shape 'captura area
                        'Genera Area calculado por el software
                        v_area_dispo1 = (pArea.Area) / 10000
                        'Leer las coordenadas para volver a calcular
                        ptcol = l
                        ReDim coordenada_DM(ptcol.PointCount)
                        For i As Integer = 0 To ptcol.PointCount - 1
                            coordenada_DM(i).x = 0
                            coordenada_DM(i).y = 0
                        Next
                        'comienza tu segundo bucle, el que recorre los puntos de cada polilinea
                        conta_vert = 0
                        j_vert = 0
                        For j = 0 To ptcol.PointCount - 2
                            conta_vert = conta_vert + 1
                            pt = ptcol.Point(j)
                            este1 = pt.X
                            norte1 = pt.Y
                            'Redonde de coordenadas
                            norte1 = Format(Math.Round(norte1, 3), "###,###.00")
                            este1 = Format(Math.Round(este1, 3), "###,###.00")
                            coordenada_DM(j).v = j + 1
                            coordenada_DM(j).x = este1
                            coordenada_DM(j).y = norte1
                        Next j ' ----- fin de bucle interno
                        'Calcular Area según coordenadas redondeadas
                        coordenada_DM(j).x = coordenada_DM(0).x
                        coordenada_DM(j).y = coordenada_DM(0).y
                        Dim d0, d1, dr As Double
                        d0 = 0 : d1 = 0 : dr = 0
                        For h = 0 To j - 1  ' UBound(coordenada_DM) - 1
                            If coordenada_DM(h).x <> 0 Or coordenada_DM(h).y <> 0 Then
                                d0 = d0 + coordenada_DM(h).x * coordenada_DM(h + 1).y
                                d1 = d1 + coordenada_DM(h).y * coordenada_DM(h + 1).x
                            Else
                                Exit For
                            End If
                        Next h
                        dr = Math.Round(Math.Abs((d0 - d1) / 2) / 10000, 4)  'termino
                        areaf1 = Format(Math.Round(dr, 4), "###,###.0000")

                        'Validacion solo si Area calculada es diferente al generado por redondeo (paso solo con Areas Disponibles con Huecos)
                        Dim validacion As Double = 0.0
                        validacion = System.Math.Abs(v_area_dispo1 - areaf1)
                        If validacion > 1 Then
                            areaf1 = v_area_dispo1

                        End If
                        pFeature.Value(intArea) = areaf1
                        pUpdateCursor.UpdateFeature(pFeature)
                        pFeature = pUpdateCursor.NextFeature

                    Loop
                    v_area_dispo = areaf1
                End If
            End If
        Catch ex As Exception
            MsgBox("Error:  Cálculo de Area del Polígono ...", MsgBoxStyle.Information, "[BDGEOCATMIN]")
        End Try
    End Sub

    Public Sub calculaareapoligonos_X(ByVal p_layer As String, ByVal seleccion_tema As String)
        Try
            v_area_dispo = 0.0
            Dim cls_eval As New Cls_evaluacion
            Dim pFeatLayer As IFeatureLayer = Nothing
            Dim fclas_tema As IFeatureClass
            pMap = pMxDoc.FocusMap
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = p_layer Then
                    pFeatLayer = pMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                'MsgBox("No se encuentra la capa de Derechos Mineros para realizar el calculo de área disponible", MsgBoxStyle.Information, "Observación...")
                Exit Sub
            End If
            'cls_eval.borracampostema()
            pMxDoc.ActiveView.Refresh()
            'Calculando area de los poligonos
            fclas_tema = pFeatLayer.FeatureClass
            Dim intArea As Integer
            Dim pArea As IArea
            Dim pUpdateCursor As IFeatureCursor
            Dim l As IPolygon
            Dim coordenada_DM(300) As Punto_DM
            Dim h, j As Integer
            Dim pt As IPoint
            Dim este1 As Double
            Dim norte1 As Double
            Dim areaf1 As Double
            Dim v_area_dispo1 As Double
            'pUpdateCursor = fclas_tema.Update(Nothing, False)
            If seleccion_tema = "Redenuncio" Then
                pUpdateCursor = fclas_tema.Update(Nothing, False)
                intArea = pUpdateCursor.FindField("HECTAGIS")
            Else
                cls_eval.borracampostema()
                pMxDoc.ActiveView.Refresh()
                pUpdateCursor = fclas_tema.Update(Nothing, False)
                intArea = pUpdateCursor.FindField("AREA_FINAL")
            End If
            If pFeatLayer.Name = "Areainter_" & v_codigo Then
                pFeature = pUpdateCursor.NextFeature
                Do Until pFeature Is Nothing
                    l = pFeature.Shape
                    ptcol = l
                    ReDim coordenada_DM(ptcol.PointCount)
                    For i As Integer = 0 To ptcol.PointCount - 1
                        coordenada_DM(i).x = 0
                        coordenada_DM(i).y = 0
                    Next
                    'comienza tu segundo bucle, el que recorre los puntos de cada polilinea

                    conta_vert = 0
                    j_vert = 0
                    For j = 0 To ptcol.PointCount - 2
                        conta_vert = conta_vert + 1
                        pt = ptcol.Point(j)
                        este1 = pt.X
                        norte1 = pt.Y

                        'Redonde de coordenadas
                        norte1 = Format(Math.Round(norte1, 3), "###,###.00")
                        este1 = Format(Math.Round(este1, 3), "###,###.00")
                        coordenada_DM(j).v = j + 1
                        coordenada_DM(j).x = este1
                        coordenada_DM(j).y = norte1
                    Next j ' ----- fin de bucle interno

                    'Calcular Area según coordenadas redondeadas
                    coordenada_DM(j).x = coordenada_DM(0).x
                    coordenada_DM(j).y = coordenada_DM(0).y
                    Dim d0, d1, dr As Double
                    d0 = 0 : d1 = 0 : dr = 0
                    For h = 0 To j - 1  ' UBound(coordenada_DM) - 1
                        If coordenada_DM(h).x <> 0 Or coordenada_DM(h).y <> 0 Then
                            d0 = d0 + coordenada_DM(h).x * coordenada_DM(h + 1).y
                            d1 = d1 + coordenada_DM(h).y * coordenada_DM(h + 1).x
                        Else
                            Exit For
                        End If
                    Next h
                    dr = Math.Round(Math.Abs((d0 - d1) / 2) / 10000, 4)  'termino
                    areaf1 = Format(Math.Round(dr, 4), "###,###.0000")
                    'pArea = pFeature.Shape
                    'If (pArea.Area) / 10000 > 0.0001 Then
                    pFeature.Value(intArea) = areaf1
                    'If (pArea.Area) / 10000 > 0.0001 Then
                    pUpdateCursor.UpdateFeature(pFeature)
                    'End If
                    'v_area_dispo = (pArea.Area) / 10000
                    pFeature = pUpdateCursor.NextFeature
                Loop
            ElseIf pFeatLayer.Name = "Areadispo_" & v_codigo Then
                If pFeatLayer.FeatureClass.FeatureCount(Nothing) > 1 Then
                    v_area_dispo1 = 0
                    pFeature = pUpdateCursor.NextFeature
                    Do Until pFeature Is Nothing
                        pArea = pFeature.Shape
                        pFeature.Value(intArea) = (pArea.Area) / 10000
                        pUpdateCursor.UpdateFeature(pFeature)
                        v_area_dispo1 = (v_area_dispo1 + ((pArea.Area) / 10000))
                        'v_area_dispo = (pArea.Area) / 10000
                        pFeature = pUpdateCursor.NextFeature
                    Loop
                    v_area_dispo = v_area_dispo1
                Else
                    pFeature = pUpdateCursor.NextFeature
                    Do Until pFeature Is Nothing
                        l = pFeature.Shape
                        ptcol = l
                        ReDim coordenada_DM(ptcol.PointCount)
                        For i As Integer = 0 To ptcol.PointCount - 1
                            coordenada_DM(i).x = 0
                            coordenada_DM(i).y = 0
                        Next
                        'comienza tu segundo bucle, el que recorre los puntos de cada polilinea

                        conta_vert = 0
                        j_vert = 0
                        For j = 0 To ptcol.PointCount - 2
                            conta_vert = conta_vert + 1
                            pt = ptcol.Point(j)
                            este1 = pt.X
                            norte1 = pt.Y
                            'Redonde de coordenadas
                            norte1 = Format(Math.Round(norte1, 3), "###,###.00")
                            este1 = Format(Math.Round(este1, 3), "###,###.00")
                            coordenada_DM(j).v = j + 1
                            coordenada_DM(j).x = este1
                            coordenada_DM(j).y = norte1
                        Next j ' ----- fin de bucle interno

                        'Calcular Area según coordenadas redondeadas

                        coordenada_DM(j).x = coordenada_DM(0).x
                        coordenada_DM(j).y = coordenada_DM(0).y
                        Dim d0, d1, dr As Double
                        d0 = 0 : d1 = 0 : dr = 0
                        For h = 0 To j - 1  ' UBound(coordenada_DM) - 1
                            If coordenada_DM(h).x <> 0 Or coordenada_DM(h).y <> 0 Then
                                d0 = d0 + coordenada_DM(h).x * coordenada_DM(h + 1).y
                                d1 = d1 + coordenada_DM(h).y * coordenada_DM(h + 1).x
                            Else
                                Exit For
                            End If
                        Next h
                        dr = Math.Round(Math.Abs((d0 - d1) / 2) / 10000, 4)  'termino
                        areaf1 = Format(Math.Round(dr, 4), "###,###.0000")
                        pFeature.Value(intArea) = areaf1
                        pUpdateCursor.UpdateFeature(pFeature)
                        pFeature = pUpdateCursor.NextFeature

                    Loop
                    v_area_dispo = areaf1
                End If
            End If
        Catch ex As Exception
            MsgBox("No se tiene acceso a la Unidad U ...", MsgBoxStyle.Information, "[BDGEOCATMIN]")
        End Try
    End Sub

    Public Sub calculadistanciafrontera(ByVal pApp As IApplication)
        '''''''''''''''''''' BLOQUE ANTIGUO ''''''''''''''''''''''''
        'Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
        'pSpatialReferenceEnv = New SpatialReferenceEnvironment
        ''Dim PSAD17, PSAD18, PSAD19 As ISpatialReference
        ''PSAD17 = pSpatialReferenceEnv.CreateProjectedCoordinateSystem(24877)
        ''PSAD18 = pSpatialReferenceEnv.CreateProjectedCoordinateSystem(24878)
        ''PSAD19 = pSpatialReferenceEnv.CreateProjectedCoordinateSystem(24879)
        'Dim pFeatureCursor1 As IFeatureCursor
        'Try
        '    pMap = pMxDoc.FocusMap
        '    Dim pFeatLayer As IFeatureLayer = Nothing
        '    Dim fclas_tema As IFeatureClass
        '    Dim pgeometria As IGeometry
        '    Dim pfeature As IFeature
        '    pMap = pMxDoc.FocusMap
        '    Dim afound As Boolean = False
        '    For A As Integer = 0 To pMap.LayerCount - 1
        '        If pMap.Layer(A).Name = "Limite Frontera" Then
        '            pFeatLayer = pMxDoc.FocusMap.Layer(A)
        '            afound = True
        '            Exit For
        '        End If
        '    Next A
        '    If Not afound Then
        '        MsgBox("No existe la caopa de linea de frontera para realizar calculo de distancia", MsgBoxStyle.Information, "Observación...")
        '        Exit Sub
        '    End If
        '    fclas_tema = pFeatLayer.FeatureClass
        '    'Tema de evaluación
        '    Dim pFeatLayer1 As IFeatureLayer = Nothing
        '    Dim fclas_tema1 As IFeatureClass = Nothing
        '    Dim pgeometria1 As IGeometry
        '    Dim pfeature1 As IFeature
        '    afound = False
        '    'pFeatLayer1 = pMxDoc.FocusMap.Layer(0)
        '    afound = False
        '    For A As Integer = 0 To pMap.LayerCount - 1
        '        If pMap.Layer(A).Name = "Catastro" Then
        '            pFeatLayer1 = pMxDoc.FocusMap.Layer(A)
        '            afound = True
        '            Exit For
        '        End If
        '    Next A
        '    If Not afound Then
        '        MsgBox("Layer No Existe.")
        '        Exit Sub
        '    End If

        '    'afound = False
        '    'For A As Integer = 0 To pMap.LayerCount - 1
        '    '    If pMap.Layer(A).Name = "Catastro" Then
        '    '        pFeatLayer1 = pMxDoc.FocusMap.Layer(A)
        '    '        afound = True
        '    '        Exit For
        '    '    End If
        '    'Next A
        '    'If Not afound Then
        '    '    MsgBox("Layer No Existe.")
        '    '    Exit Sub
        '    'End If

        '    If pFeatLayer1.Name = "Catastro" Then
        '        afound = True
        '    End If
        '    If Not afound Then
        '        MsgBox("No se encuentra la capa Catastro", MsgBoxStyle.Critical, "Observación...")
        '        Exit Sub
        '    End If
        '    pMxDoc.UpdateContents()
        '    fclas_tema = pFeatLayer.FeatureClass

        '    Dim valor_ob As Integer
        '    Dim peaturecursor As IFeatureCursor
        '    peaturecursor = fclas_tema.Search(Nothing, False)
        '    pfeature = peaturecursor.NextFeature
        '    Do Until pfeature Is Nothing
        '        valor_ob = pfeature.Value(peaturecursor.FindField("OBJECTID"))
        '        pfeature = peaturecursor.NextFeature
        '    Loop

        '    'pfeature = fclas_tema.GetFeature(2)
        '    pfeature = fclas_tema.GetFeature(valor_ob)
        '    fclas_tema1 = pFeatLayer1.FeatureClass
        '    pFeatureCursor1 = fclas_tema1.Search(Nothing, False)
        '    pfeature1 = pFeatureCursor1.NextFeature
        '    pgeometria1 = pfeature1.Shape
        '    Select Case v_zona_dm
        '        Case 17

        '            'ppfeature.Shape.SpatialReference = Datum_PSAD_18
        '            'pfeature.Shape.Project(Datum_PSAD_17)
        '            pfeature.Shape.SpatialReference = Datum_PSAD_17
        '        Case 18
        '            pfeature.Shape.SpatialReference = Datum_PSAD_18
        '        Case 19
        '            'ppfeature.Shape.SpatialReference = Datum_PSAD_18
        '            'pfeature.Shape.Project(Datum_PSAD_19)
        '            pfeature.Shape.SpatialReference = Datum_PSAD_19
        '    End Select
        '    pgeometria = pfeature.Shape
        '    Dim pProximity As IProximityOperator
        '    pProximity = pgeometria
        '    Dim distancia_fron1 As Double
        '    distancia_fron1 = (pProximity.ReturnDistance(pgeometria1) / 1000)
        '    distancia_fron = Math.Round(distancia_fron1, 3)
        'Catch ex As Exception
        '    MsgBox("Error en calculo de Distancia de Frontera", MsgBoxStyle.Critical, "Observación..")
        'End Try
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        colecciones_front.Clear()
        Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
        pSpatialReferenceEnv = New SpatialReferenceEnvironment
  
        Dim pFeatureCursor1 As IFeatureCursor
        Try
            pMap = pMxDoc.FocusMap
            Dim pFeatLayer As IFeatureLayer = Nothing
            Dim fclas_tema As IFeatureClass
            Dim pgeometria As IGeometry
            Dim pfeature As IFeature
            pMap = pMxDoc.FocusMap
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Limite Frontera" Then
                    pFeatLayer = pMxDoc.FocusMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No existe la caopa de linea de frontera para realizar calculo de distancia", MsgBoxStyle.Information, "Observación...")
                Exit Sub
            End If
            fclas_tema = pFeatLayer.FeatureClass
            'Tema de evaluación
            Dim pFeatLayer1 As IFeatureLayer = Nothing
            Dim fclas_tema1 As IFeatureClass = Nothing
            Dim pgeometria1 As IGeometry
            Dim pfeature1 As IFeature
            afound = False
            'pFeatLayer1 = pMxDoc.FocusMap.Layer(0)
            afound = False
            For A As Integer = 0 To pMap.LayerCount - 1
                'If pMap.Layer(A).Name = "Catastro" Then
                If pMap.Layer(A).Name = loStrShapefile_dm Then
                    pFeatLayer1 = pMxDoc.FocusMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("Layer No Existe.")
                Exit Sub
            End If

            'If pFeatLayer1.Name = "Catastro" Then
            If pFeatLayer1.Name = loStrShapefile_dm Then
                afound = True
            End If
            If Not afound Then
                MsgBox("No se encuentra la capa Catastro", MsgBoxStyle.Critical, "Observación...")
                Exit Sub
            End If
            pMxDoc.UpdateContents()
            fclas_tema = pFeatLayer.FeatureClass

            Dim valor_ob As Integer
            Dim peaturecursor As IFeatureCursor
            peaturecursor = fclas_tema.Search(Nothing, False)
            pfeature = peaturecursor.NextFeature
            Do Until pfeature Is Nothing
                valor_ob = pfeature.Value(peaturecursor.FindField("OBJECTID"))
                'comentado, ya no es una sola lina
                ' pfeature = peaturecursor.NextFeature
                ' Loop

                'pfeature = fclas_tema.GetFeature(2)
                pfeature = fclas_tema.GetFeature(valor_ob)
                fclas_tema1 = pFeatLayer1.FeatureClass
                pFeatureCursor1 = fclas_tema1.Search(Nothing, False)
                pfeature1 = pFeatureCursor1.NextFeature
                pgeometria1 = pfeature1.Shape
                Select Case v_zona_dm
                    Case 17
                        'ppfeature.Shape.SpatialReference = Datum_PSAD_18
                        'pfeature.Shape.Project(Datum_PSAD_17)
                        pfeature.Shape.SpatialReference = Datum_PSAD_17
                    Case 18
                        pfeature.Shape.SpatialReference = Datum_PSAD_18
                    Case 19
                        'ppfeature.Shape.SpatialReference = Datum_PSAD_18
                        'pfeature.Shape.Project(Datum_PSAD_19)
                        pfeature.Shape.SpatialReference = Datum_PSAD_19
                End Select
                pgeometria = pfeature.Shape
                Dim pProximity As IProximityOperator
                pProximity = pgeometria
                Dim distancia_fron1 As Double
                distancia_fron1 = (pProximity.ReturnDistance(pgeometria1) / 1000)
                distancia_fron = Math.Round(distancia_fron1, 3)
                colecciones_front.Add(distancia_fron)  'añadiendo las distancia a la colección
                pfeature = peaturecursor.NextFeature
            Loop

            '*********************************************************************
            'Obteniendo el mínimo valor de la coleccion para el calculo distancia
            Dim numerof As Double = 1000000.0
            Dim numerof2 As Double = 1000000.0
            For f As Integer = 1 To colecciones_front.Count
                numerof = colecciones_front.Item(f)
                If f = 1 Then
                    numerof2 = numerof
                ElseIf f > 1 Then
                    If numerof2 <= numerof Then
                        ' queda valor original del primer datos
                    Else
                        numerof2 = numerof  'toma el menor valor
                    End If
                End If
            Next f
            distancia_fron = numerof2
            distancia_fron = Math.Round(distancia_fron, 3)
            'MsgBox(distancia_fron, MsgBoxStyle.Critical, "distancia")
        Catch ex As Exception
            MsgBox("Error en calculo de Distancia de Frontera", MsgBoxStyle.Critical, "Observación..")
        End Try

    End Sub
    Public Sub Calcula_Distancia(ByVal p_App As IApplication)
        'Dim pDoc As IMxDocument
        'Dim pMap As IMap
        Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
        pSpatialReferenceEnv = New SpatialReferenceEnvironment
        Dim PSAD17, PSAD18, PSAD19 As ISpatialReference
        PSAD17 = pSpatialReferenceEnv.CreateProjectedCoordinateSystem(24877)
        PSAD18 = pSpatialReferenceEnv.CreateProjectedCoordinateSystem(24878)
        PSAD19 = pSpatialReferenceEnv.CreateProjectedCoordinateSystem(24879)
        Try
            pMxDoc = p_App.Document
            pMap = pMxDoc.FocusMap
            Dim pFLayer As IFeatureLayer
            Dim pFLayer1 As IFeatureLayer
            pFLayer = pMap.Layer(0)
            pFLayer1 = pMap.Layer(1)
            Dim pFeat As IFeature
            Dim pFeat1 As IFeature
            Dim dDist As Double
            Dim pFc As IFeatureClass
            Dim pFc1 As IFeatureClass
            pFc = pFLayer.FeatureClass
            pFc1 = pFLayer1.FeatureClass
            'Get features 1 and 4
            pFeat = pFc.GetFeature(1)
            pFeat1 = pFc1.GetFeature(500)
            MsgBox(pFeat.Value(2) & "-----------" & pFeat1.Value(2))
            Dim pProxOp As IProximityOperator
            Dim pGeom As IGeometry
            Dim pGeom1 As IGeometry
            pGeom1 = pFeat1.Shape

            pFeat.Shape.SpatialReference = PSAD17
            pFeat.Shape.Project(PSAD18)
            pGeom = pFeat.Shape

            'pGeom.SpatialReference = PSAD17
            'pGeom.Project(PSAD18)
            pProxOp = pGeom
            'Find the Distance
            dDist = pProxOp.ReturnDistance(pGeom1)
            MsgBox("The distance is: " & dDist)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub cierra_ejecutable()
        KillProcess("Barra_Proceso.exe")
    End Sub

    Public Sub KillProcess(ByVal processName As String)
        'Funcion para cerrar proceso de espera

        Dim oWMI
        Dim ret
        Dim oServices
        Dim oService
        Dim servicename

        oWMI = GetObject("winmgmts:")
        oServices = oWMI.InstancesOf("win32_process")

        For Each oService In oServices
            servicename = _
                LCase(Trim(CStr(oService.name) & ""))
            If InStr(1, servicename, _
                LCase(processName), vbTextCompare) > 0 Then
                ret = oService.Terminate
            End If
        Next
        oServices = Nothing
        oWMI = Nothing

    End Sub

    Public Sub validaprioritarios()
        Dim v_estado As String
        v_estado = "P"
        Dim cls_eval As New Cls_evaluacion
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim fclas_tema As IFeatureClass
        Dim pFeatureSelection As IFeatureSelection
        Dim capa_sele As ISelectionSet
        pMap = pMxDoc.FocusMap
        Dim afound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Catastro" Then
                pFeatLayer = pMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra la capa de Derechos Mineros para realizar el calculo de área disponible", MsgBoxStyle.Information, "Observación...")
            Exit Sub
        End If

        cls_eval.consultacapaDM("", "Prioritarios", "Catastro")


        Dim consulta1 As IQueryFilter
        Dim pfields As IFields
        consulta1 = New QueryFilter
        Dim pUpdateFeatures As IFeatureCursor
        Dim pFeature_x As IFeature

        pFeatureSelection = pFeatLayer
        capa_sele = pFeatureSelection.SelectionSet
        capa_sele.Search(Nothing, True, pFeatureCursor)
        fclas_tema = pFeatLayer.FeatureClass
        pfields = fclas_tema.Fields
        Dim pfeature As IFeature

        Dim v_eval_x As String
        Dim v_estado_x As String
        Dim nombre_dm_x As String
        Dim v_codigo_x As String
        Dim v_identi_eval_x As String
        Dim v_incopora_x As String
        pfeature = pFeatureCursor.NextFeature

        Do Until pfeature Is Nothing
            v_eval_x = pfeature.Value(pfields.FindField("EVAL"))
            v_estado_x = pfeature.Value(pfields.FindField("ESTADO"))
            nombre_dm_x = pfeature.Value(pfields.FindField("CONCESION"))
            v_codigo_x = pfeature.Value(pfields.FindField("CODIGOU"))
            v_identi_eval_x = pfeature.Value(pfields.FindField("IDENTI"))
            v_incopora_x = pfeature.Value(pfields.FindField("DE_IDEN"))
            If v_estado = "P" Then  ' Si el DM evaluado tiene estado = "P"
                'Criterios de DM prioritarios el cual no se le debe calcular area disponible
                If ((v_estado_x = "D") Or (((v_estado_x = "Q") Or (v_estado_x = "N") Or (v_estado_x = "E") Or (v_estado_x = "C")) And (v_identi_eval_x <> "01-10") And (v_incopora_x <> "I"))) Then
                    'Asigna valor del criterio
                    consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    pUpdateFeatures = fclas_tema.Update(consulta1, False)
                    pFeature_x = pUpdateFeatures.NextFeature
                    Do Until pFeature_x Is Nothing
                        pFeature_x.Value(pfields.FindField("CALCULO")) = "NO"
                        pUpdateFeatures.UpdateFeature(pFeature_x)
                        pFeature_x = pUpdateFeatures.NextFeature
                    Loop
                ElseIf ((v_estado_x = "C") And (v_identi_eval_x = "01-10") And (v_incopora_x <> "I")) Then
                    consulta1.WhereClause = "CODIGOU = '" & v_codigo_x & "'"
                    pUpdateFeatures = fclas_tema.Update(consulta1, False)
                    pFeature_x = pUpdateFeatures.NextFeature
                    Do Until pFeature_x Is Nothing
                        pFeature_x.Value(pfields.FindField("CALCULO")) = "NO"
                        pUpdateFeatures.UpdateFeature(pFeature_x)
                        pFeature_x = pUpdateFeatures.NextFeature
                    Loop
                End If
            End If

            pfeature = pFeatureCursor.NextFeature
        Loop
        pFeatureSelection.Clear()  'LIMPIANDO LA SELECCION
    End Sub

    Public Sub busca_capavista(ByVal p_Layer As String, ByVal afound As Boolean)

        'Programa para borrar campos de los dm interceptados

        Dim pFeatLayer As IFeatureLayer
        pMap = pMxDoc.FocusMap
        'Dim afound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = p_Layer Then
                pFeatLayer = pMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra la capa de Derechos Mineros para realizar el calculo de área disponible", MsgBoxStyle.Information, "Observación...")
            Exit Sub
        End If
    End Sub
    Public Sub insertafeature_tema()

        'Programa para insertar features a otro tema ( Caso DM simulado al tema total de DM)
        '----------------------------------------------------------------------------------

        Dim tema1 As IFeatureLayer
        Dim tema2 As IFeatureLayer
        Dim pfeaturecursor2 As IFeatureCursor
        Dim pfeaturecursor1 As IFeatureCursor
        Dim pgeometria As IGeometry

        'Define primer tema
        '********************

        Dim tema1_fclass As IFeatureClass
        Dim tema2_fclass As IFeatureClass
        Dim pFeatLayer As IFeatureLayer = Nothing
        pMap = pMxDoc.FocusMap
        Dim afound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Catastro" Then
                pFeatLayer = pMap.Layer(A)
                afound = True
                tema1 = pFeatLayer
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra la capa de Derechos ", MsgBoxStyle.Information, "Observación...")
            Exit Sub
        End If
        tema1 = pFeatLayer
        tema1_fclass = tema1.FeatureClass

        'Define segundo tema
        '*********************
        pMap = pMxDoc.FocusMap
        afound = False
        For A As Integer = 0 To pMap.LayerCount - 1
            '  If pMap.Layer(A).Name = "DM_" & v_codigo Then
            If pMap.Layer(A).Name = "DM_Simulado" Then
                pFeatLayer = pMap.Layer(A)
                afound = True
                tema2 = pFeatLayer
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra la capa de Derechos", MsgBoxStyle.Information, "Observación...")
            Exit Sub
        End If

        tema2 = pFeatLayer
        tema2_fclass = tema2.FeatureClass

        Dim pFeatureBuffer As IFeatureBuffer  ' Objeto que devolvera forma del poligono a insertar
        Dim pfeature2 As IFeature

        pfeaturecursor2 = tema2_fclass.Search(Nothing, False)
        pfeature2 = pfeaturecursor2.NextFeature
        pgeometria = pfeature2.Shape
        pfeaturecursor1 = tema1_fclass.Insert(True)  ' preparando el insert feature
        pFeatureBuffer = tema1_fclass.CreateFeatureBuffer  ' crea forma del shape al tema principal
        pFeatureBuffer.Shape = pgeometria  ' iguala forma del shape
        pfeaturecursor1.InsertFeature(pFeatureBuffer)  'nserta poligono al tema principal

    End Sub

    Public Sub asigna_escaladataframe(ByVal nombre_dataframe As String)
        escalaf = pMap.MapScale
        If nombre_dataframe = "CATASTRO MINERO" Then
            If escalaf <= 5000 Then
                escala_s = 5000
            ElseIf (escalaf > 5000) And (escalaf <= 7500) Then
                escala_s = 7500
            ElseIf (escalaf > 7500) And (escalaf <= 10000) Then
                escala_s = 8000
            ElseIf (escalaf > 10000) And (escalaf <= 15000) Then
                escala_s = 10000
            ElseIf (escalaf > 15000) And (escalaf <= 25000) Then
                escala_s = 20000
            ElseIf (escalaf > 25000) And (escalaf <= 50000) Then
                escala_s = 50000
            ElseIf (escalaf > 50000) And (escalaf <= 75000) Then
                escala_s = 75000
            ElseIf (escalaf > 75000) And (escalaf <= 125000) Then
                escala_s = 100000
            ElseIf (escalaf > 125000) And (escalaf <= 150000) Then
                escala_s = 125000
            ElseIf (escalaf > 150000) And (escalaf <= 200000) Then
                escala_s = 150000
            ElseIf (escalaf > 200000) And (escalaf <= 250000) Then
                escala_s = 200000
            ElseIf (escalaf > 250000) And (escalaf <= 300000) Then
                escala_s = 250000
            ElseIf (escalaf > 300000) And (escalaf <= 1000000) Then
                escala_s = 500000
            ElseIf escalaf > 1000000 Then
                escala_s = 1000000
            End If
        ElseIf nombre_dataframe = "DM SIMULTANEO" Then
            If escalaf <= 5000 Then
                escala_s = 5000
            ElseIf (escalaf > 5000) And (escalaf <= 7500) Then
                escala_s = 7500
            ElseIf (escalaf > 7500) And (escalaf <= 10000) Then
                escala_s = 8000
            ElseIf (escalaf > 10000) And (escalaf <= 15000) Then
                escala_s = 10000
            ElseIf (escalaf > 15000) And (escalaf <= 25000) Then
                escala_s = 20000
            ElseIf (escalaf > 25000) And (escalaf <= 50000) Then
                escala_s = 50000
            ElseIf (escalaf > 50000) And (escalaf <= 75000) Then
                escala_s = 75000
            ElseIf (escalaf > 75000) And (escalaf <= 125000) Then
                escala_s = 100000
            ElseIf (escalaf > 125000) And (escalaf <= 150000) Then
                escala_s = 125000
            ElseIf (escalaf > 150000) And (escalaf <= 200000) Then
                escala_s = 150000
            ElseIf (escalaf > 200000) And (escalaf <= 250000) Then
                escala_s = 200000
            ElseIf (escalaf > 250000) And (escalaf <= 300000) Then
                escala_s = 250000
            ElseIf (escalaf > 300000) And (escalaf <= 1000000) Then
                escala_s = 500000
            ElseIf escalaf > 1000000 Then
                escala_s = 1000000
            End If
        ElseIf nombre_dataframe = "INTEGRANTES ACUMULACION" Then
            If escalaf <= 5000 Then
                escala_s = 5000
            ElseIf (escalaf > 5000) And (escalaf <= 7500) Then
                escala_s = 7500
            ElseIf (escalaf > 7500) And (escalaf <= 10000) Then
                escala_s = 10000
            ElseIf (escalaf > 10000) And (escalaf <= 15000) Then
                escala_s = 15000
            ElseIf (escalaf > 15000) And (escalaf <= 25000) Then
                escala_s = 25000
            ElseIf (escalaf > 25000) And (escalaf <= 50000) Then
                escala_s = 50000
            ElseIf (escalaf > 50000) And (escalaf <= 75000) Then
                escala_s = 75000
            ElseIf (escalaf > 75000) And (escalaf <= 125000) Then
                escala_s = 100000
            ElseIf (escalaf > 125000) And (escalaf <= 150000) Then
                escala_s = 125000
            ElseIf (escalaf > 150000) And (escalaf <= 200000) Then
                escala_s = 150000
            ElseIf (escalaf > 200000) And (escalaf <= 250000) Then
                escala_s = 200000
            ElseIf (escalaf > 250000) And (escalaf <= 300000) Then
                escala_s = 250000
            ElseIf (escalaf > 300000) And (escalaf <= 1000000) Then
                escala_s = 500000
            ElseIf escalaf > 1000000 Then
                escala_s = 1000000
            End If
        ElseIf nombre_dataframe = "ACUMULACION DM" Then
            If escalaf <= 5000 Then
                escala_s = 5000
            ElseIf (escalaf > 5000) And (escalaf <= 7500) Then
                escala_s = 7500
            ElseIf (escalaf > 7500) And (escalaf <= 10000) Then
                escala_s = 10000
            ElseIf (escalaf > 10000) And (escalaf <= 15000) Then
                escala_s = 15000
            ElseIf (escalaf > 15000) And (escalaf <= 25000) Then
                escala_s = 25000
            ElseIf (escalaf > 25000) And (escalaf <= 50000) Then
                escala_s = 50000
            ElseIf (escalaf > 50000) And (escalaf <= 75000) Then
                escala_s = 75000
            ElseIf (escalaf > 75000) And (escalaf <= 125000) Then
                escala_s = 100000
            ElseIf (escalaf > 125000) And (escalaf <= 150000) Then
                escala_s = 125000
            ElseIf (escalaf > 150000) And (escalaf <= 200000) Then
                escala_s = 150000
            ElseIf (escalaf > 200000) And (escalaf <= 250000) Then
                escala_s = 200000
            ElseIf (escalaf > 250000) And (escalaf <= 300000) Then
                escala_s = 250000
            ElseIf (escalaf > 300000) And (escalaf <= 1000000) Then
                escala_s = 500000
            ElseIf escalaf > 1000000 Then
                escala_s = 1000000
            End If
        ElseIf nombre_dataframe = "ACUMULACION AFECTACION" Then
            If escalaf <= 5000 Then
                escala_s = 5000
            ElseIf (escalaf > 5000) And (escalaf <= 7500) Then
                escala_s = 7500
            ElseIf (escalaf > 7500) And (escalaf <= 10000) Then
                escala_s = 10000
            ElseIf (escalaf > 10000) And (escalaf <= 15000) Then
                escala_s = 15000
            ElseIf (escalaf > 15000) And (escalaf <= 25000) Then
                escala_s = 25000
            ElseIf (escalaf > 25000) And (escalaf <= 50000) Then
                escala_s = 50000
            ElseIf (escalaf > 50000) And (escalaf <= 75000) Then
                escala_s = 75000
            ElseIf (escalaf > 75000) And (escalaf <= 125000) Then
                escala_s = 100000
            ElseIf (escalaf > 125000) And (escalaf <= 150000) Then
                escala_s = 125000
            ElseIf (escalaf > 150000) And (escalaf <= 200000) Then
                escala_s = 150000
            ElseIf (escalaf > 200000) And (escalaf <= 250000) Then
                escala_s = 200000
            ElseIf (escalaf > 250000) And (escalaf <= 300000) Then
                escala_s = 250000
            ElseIf (escalaf > 300000) And (escalaf <= 1000000) Then
                escala_s = 500000
            ElseIf escalaf > 1000000 Then
                escala_s = 1000000
            End If
        ElseIf nombre_dataframe = "ACUMULACION BENEFICIO" Then
            If escalaf <= 5000 Then
                escala_s = 5000
            ElseIf (escalaf > 5000) And (escalaf <= 7500) Then
                escala_s = 7500
            ElseIf (escalaf > 7500) And (escalaf <= 10000) Then
                escala_s = 10000
            ElseIf (escalaf > 10000) And (escalaf <= 15000) Then
                escala_s = 15000
            ElseIf (escalaf > 15000) And (escalaf <= 25000) Then
                escala_s = 25000
            ElseIf (escalaf > 25000) And (escalaf <= 50000) Then
                escala_s = 50000
            ElseIf (escalaf > 50000) And (escalaf <= 75000) Then
                escala_s = 75000
            ElseIf (escalaf > 75000) And (escalaf <= 125000) Then
                escala_s = 100000
            ElseIf (escalaf > 125000) And (escalaf <= 150000) Then
                escala_s = 125000
            ElseIf (escalaf > 150000) And (escalaf <= 200000) Then
                escala_s = 150000
            ElseIf (escalaf > 200000) And (escalaf <= 250000) Then
                escala_s = 200000
            ElseIf (escalaf > 250000) And (escalaf <= 300000) Then
                escala_s = 250000
            ElseIf (escalaf > 300000) And (escalaf <= 1000000) Then
                escala_s = 500000
            ElseIf escalaf > 1000000 Then
                escala_s = 1000000
            End If
        ElseIf nombre_dataframe = "ACUMULACION ARESTRINGIDA" Then
            If escalaf <= 5000 Then
                escala_s = 5000
            ElseIf (escalaf > 5000) And (escalaf <= 7500) Then
                escala_s = 7500
            ElseIf (escalaf > 7500) And (escalaf <= 10000) Then
                escala_s = 10000
            ElseIf (escalaf > 10000) And (escalaf <= 15000) Then
                escala_s = 15000
            ElseIf (escalaf > 15000) And (escalaf <= 25000) Then
                escala_s = 25000
            ElseIf (escalaf > 25000) And (escalaf <= 50000) Then
                escala_s = 50000
            ElseIf (escalaf > 50000) And (escalaf <= 75000) Then
                escala_s = 75000
            ElseIf (escalaf > 75000) And (escalaf <= 125000) Then
                escala_s = 100000
            ElseIf (escalaf > 125000) And (escalaf <= 150000) Then
                escala_s = 125000
            ElseIf (escalaf > 150000) And (escalaf <= 200000) Then
                escala_s = 150000
            ElseIf (escalaf > 200000) And (escalaf <= 250000) Then
                escala_s = 200000
            ElseIf (escalaf > 250000) And (escalaf <= 300000) Then
                escala_s = 250000
            ElseIf (escalaf > 300000) And (escalaf <= 1000000) Then
                escala_s = 500000
            ElseIf escalaf > 1000000 Then
                escala_s = 1000000
            End If
        Else
            'escalaf = pMap.MapScale   ' para capturar escala
            If escalaf <= 5000 Then
                escala_s = 5000
                escala_s = escala_s * 5
            ElseIf (escalaf > 5000) And (escalaf <= 7500) Then
                escala_s = 5000
                escala_s = escala_s * 5
            ElseIf (escalaf > 7500) And (escalaf <= 10000) Then
                escala_s = 7500
                escala_s = escala_s * 5
            ElseIf (escalaf > 10000) And (escalaf <= 15000) Then
                escala_s = 10000
                escala_s = escala_s * 5
            ElseIf (escalaf > 15000) And (escalaf <= 25000) Then
                escala_s = 20000
                escala_s = escala_s * 5
            ElseIf (escalaf > 25000) And (escalaf <= 50000) Then
                escala_s = 25000
                escala_s = escala_s * 5
            ElseIf (escalaf > 50000) And (escalaf <= 75000) Then
                escala_s = 50000
                escala_s = escala_s * 5
            ElseIf (escalaf > 75000) And (escalaf <= 100000) Then
                escala_s = 75000
                escala_s = escala_s * 5
            ElseIf (escalaf > 100000) And (escalaf <= 125000) Then
                escala_s = 100000
                escala_s = escala_s * 5
            ElseIf (escalaf > 125000) And (escalaf <= 150000) Then
                escala_s = 125000
                escala_s = escala_s * 5
            ElseIf (escalaf > 150000) And (escalaf <= 200000) Then
                escala_s = 150000
                escala_s = escala_s * 5
            ElseIf (escalaf > 200000) And (escalaf <= 250000) Then
                escala_s = 200000
                escala_s = escala_s * 5
            ElseIf (escalaf > 250000) And (escalaf <= 300000) Then
                escala_s = 250000
                escala_s = escala_s * 5
            ElseIf (escalaf > 300000) And (escalaf <= 1000000) Then
                escala_s = 500000
                escala_s = escala_s * 5
            ElseIf escalaf > 1000000 Then
                escala_s = 1000000
                escala_s = escala_s * 5
            End If
        End If

        pMap.MapScale = escala_s

    End Sub
    Public Sub temasubicacion(ByVal nombre_dataframe As String, ByVal p_app As IApplication)

        'Añadiendo las capas
        Dim cls_catastro As New cls_DM_1
        Dim cls_eval As New Cls_evaluacion
        Dim playerdefinition As IFeatureLayerDefinition
        Dim tema As IFeatureLayer

        pMap = pMxDoc.FocusMap
        If nombre_dataframe = "MAPA UBICACION" Then
            arch_cata = ""


                If v_sistema = "PSAD-56" Then

                    cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento_Z & v_zona_dm, m_application, "1", False)
                Else
                    cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento_WGS & v_zona_dm, m_application, "1", False)
                End If
            tema = pMap.Layer(0)
            'playerdefinition = tema
            'playerdefinition.DefinitionExpression = "CD_DEPA <> '99'"
            tema.Visible = True
            pMxDoc.UpdateContents()
            'cls_catastro.Shade_Poligono("Departamento", pApp)
            cls_catastro.Color_Poligono_Simple(m_application, "Departamento")
            tema.Name = "Peru"
            pMxDoc.UpdateContents()

            If v_sistema = "PSAD-56" Then
                cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento_Z & v_zona_dm, m_application, "1", False)
            Else
                cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento_WGS & v_zona_dm, m_application, "1", False)
            End If
            '  cls_eval.DefinitionExpressiontema(lista_nm_depa, p_app, "Departamento")
            cls_eval.DefinitionExpressiontema(lista_depa_sele, p_app, "Departamento")
            tema = pMap.Layer(0)
            tema.Visible = True
            tema.Name = "Depa"
            'arch_cata = "Hoja"
            arch_cata = "Depa"
            cls_catastro.Color_Poligono_Simple(m_application, "Depa")
            cls_catastro.Zoom_to_Layer("Depa")
        ElseIf nombre_dataframe = "MAPA DE EMPALME" Then


            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Carta, m_application, "1", False)
            arch_cata = ""
            tema = pMap.Layer(0)
            playerdefinition = tema
            playerdefinition.DefinitionExpression = "ESTADO <> 'Sin dato'"
            tema.Name = "Hojas IGN"
            tema.Visible = True
            ' cls_catastro.Shade_Poligono("Hojas IGN", pApp)
            cls_catastro.Color_Poligono_Simple(m_application, "Hojas IGN")
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento_Z & v_zona_dm, m_application, "1", False)
            cls_catastro.Color_Poligono_Simple(m_application, "Departamento")
            ' cls_catastro.Shade_Poligono("Departamento", pApp)

            tema = pMap.Layer(0)


            'playerdefinition = tema
            'playerdefinition.DefinitionExpression = "CD_DEPA <> '99'"
            tema.Name = "Peru"
            tema.Visible = True
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Carta, m_application, "1", False)
            tema = New FeatureLayer
            tema = pMap.Layer(0)
            'tema.Name = "Hoja"   
            playerdefinition = tema

            If v_opcion_modulo = "OP_29" Or v_opcion_modulo = "OP_30" Then   ''''''''''''  se esta agregando
                lista_cartas = "CD_HOJA =  '" & lista_cartas & "'"
                playerdefinition.DefinitionExpression = UCase(lista_cartas)
            Else
                playerdefinition.DefinitionExpression = UCase(lista_cartas)
            End If

            'playerdefinition.DefinitionExpression = UCase(lista_cartas) ''''''''''''''''''''''''''''OJO

            tema = pMap.Layer(0)
            tema.Visible = True
            pMxDoc.UpdateContents()

            arch_cata = "Hoja"
            cls_catastro.Color_Poligono_Simple(m_application, "Cuadrangulo")
            cls_catastro.Zoom_to_Layer("Cuadrangulo")
            pMxDoc.UpdateContents()
            arch_cata = ""

        ElseIf nombre_dataframe = "PLANO UBICACION" Then

            If v_opcion_Rese_sele = "AN" Then

                cls_catastro.Add_ShapeFile1("Zona Reservada" & fecha_archi, m_application, "Zona Reservada")
                cls_catastro.Color_Poligono_Simple(m_application, "Zona Reservada")
                cls_catastro.rotulatexto_dm("Zona Reservada", m_application)

            ElseIf v_opcion_Rese_sele = "ZU" Then


                cls_catastro.Add_ShapeFile1("Zona Urbana" & fecha_archi, m_application, "Zona Urbana")
                cls_catastro.Color_Poligono_Simple(m_application, "Zona Urbana")
                cls_catastro.rotulatexto_dm("Zona Urbana", m_application)

            End If
            If v_sistema = "PSAD-56" Then

                cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito_Z & v_zona_dm, m_application, "1", True)
            Else
                cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito_WGS & v_zona_dm, m_application, "1", True)
            End If
            ' cls_catastro.DefinitionExpression(consulta_lista_dist, p_app, "Distrito")
            cls_catastro.DefinitionExpression(consulta_lista_dist_ubi, p_app, "Distrito")

            '  cls_catastro.DefinitionExpression_1(consulta_lista_dist, m_application, "Distrito")

            cls_catastro.Color_Poligono_Simple(m_application, "Distrito")
            cls_catastro.ShowLabel_DM("Distrito", p_app)




            End If
    End Sub

    Public Sub ActivaDataframe_Opcion(ByVal nombre_dataframe As String, ByVal p_App As IApplication)
        'Activando el datraframe Catastro Minero
        Dim contador As Integer = 0
        Dim mapas As IMaps
        mapas = pMxDoc.Maps
        Dim mapa As IMap
        Dim i As Integer
        'pMxDoc = p_App.Document
        For i = 0 To mapas.Count - 1
            mapa = mapas.Item(i)
            If nombre_dataframe = caso_consulta Then
                If mapa.Name = caso_consulta Then
                    'mapa = pMxDoc.FocusMap
                    pMxDoc.ActiveView = mapa
                    pMxDoc.UpdateContents()
                    contador = i
                    Exit For
                End If
            Else
            End If
        Next i
        pMxDoc.UpdateContents()
    End Sub
    Public Sub agrega_limiteregional(ByVal pApp As IApplication)
        Try
            Dim cls_eval As New Cls_evaluacion
            caso_consulta = "CATASTRO MINERO"
            cls_eval.ActivaDataframe_Opcion(caso_consulta, m_application)
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Cuadricula, m_application, "1", True)
            'cls_Catastro.DefinitionExpression(lista_nm_depa, m_application, "Cuadricula Regional")
            cls_Catastro.DefinitionExpression(lista_depa_sele, m_application, "Cuadricula Regional")
            '  cls_Catastro.Shade_Poligono("Cuadricula Regional", pApp)
            cls_Catastro.Color_Poligono_Simple(m_application, "Cuadricula Regional")
        Catch EX As Exception
            MsgBox("Error al Cargar capa de cuadriculas regionales", MsgBoxStyle.Critical, "SIGCATMIN")
        End Try

    End Sub
    Public Sub intersectadepa()
        Dim tema1 As IFeatureLayer
        Dim tema2 As IFeatureLayer
        Dim Nombre_depa As String = ""
        Dim v_porcen_x As Double = 0
        Dim archivo1 = "Sup_" & DateTime.Now.Ticks.ToString
        Dim pFeature As IFeature
        Dim tema1_fclass As IFeatureClass
        Dim tema2_fclass As IFeatureClass
        Dim fclas_tema As IFeatureClass
        Dim pFeatureCursor As IFeatureCursor
        pMap = pMxDoc.FocusMap
        tema1 = pMap.Layer(1)  'tema dm
        tema1_fclass = tema1.FeatureClass
        pFeatureCursor = tema1_fclass.Search(Nothing, False)
        pFeature = pFeatureCursor.NextFeature
        Do Until pFeature Is Nothing
            v_hectagis_x = pFeature.Value(pFields.FindField("HECTAGIS"))
            pFeature = pFeatureCursor.NextFeature
        Loop
        'Define segundo tema
        tema2 = pMap.Layer(0)  'tema demarca
        tema2_fclass = tema2.FeatureClass
        Dim pNewWSName As IWorkspaceName
        Dim pFCName As IFeatureClassName
        Dim pDatasetName As IDatasetName
        pFCName = New FeatureClassName
        pDatasetName = pFCName
        pNewWSName = New WorkspaceName
        pNewWSName.WorkspaceFactoryProgID = "esriCore.ShapeFileWorkspaceFactory"
        pNewWSName.PathName = glo_pathTMP
        pDatasetName.WorkspaceName = pNewWSName
        pDatasetName.Name = archivo1
        ' Permance del area interceptada
        'Dim pBGP As IBasicGeoprocessor
        Dim tol As Double
        Dim pOutputFC As IFeatureClass
        pBGP = New BasicGeoprocessor
        'Define tolerancia
        tol = 0.01
        'tol = 0.01
        pOutputFC = pBGP.Intersect(tema1_fclass, False, tema2_fclass, False, tol, pFCName)
        Dim pOutputFeatLayer As IFeatureLayer
        'Añandiendo el tema a a la vista

        pOutputFeatLayer = New FeatureLayer
        pOutputFeatLayer.FeatureClass = pOutputFC
        pOutputFeatLayer.Name = pOutputFC.AliasName
        fclas_tema = pOutputFeatLayer.FeatureClass
        pMap.AddLayer(pOutputFeatLayer)

        'Calculando area del poligono interceptado
        Dim intArea As Integer
        Dim pArea As IArea
        Dim pUpdateCursor As IFeatureCursor
        pUpdateCursor = fclas_tema.Update(Nothing, False)
        intArea = pUpdateCursor.FindField("HECTAGIS")
        'intArea = pUpdateCursor.FindField("AREA_FINAL")
        pFeature = pUpdateCursor.NextFeature
        Do Until pFeature Is Nothing
            pArea = pFeature.Shape
            pFeature.Value(intArea) = (pArea.Area) / 10000
            pUpdateCursor.UpdateFeature(pFeature)
            pFeature = pUpdateCursor.NextFeature
        Loop
    End Sub
    Public Sub intercepta_temas(ByVal tema_procesing As String, ByVal p_App As IApplication, ByVal v_existe As Boolean)

        'Programa para interceptar temas - DM evaluado VS DM prioritarios
        '*******************************************************************
        Try
            Dim cls_eval As New Cls_evaluacion
            Dim cls_catastro As New cls_DM_1
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
                If pMap.Layer(A).Name = "DM_" & v_codigo Then
                    pFeatLayer = pMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra la capa de Derechos Mineros para realizar el cálculo de área disponible", MsgBoxStyle.Information, "Observación...")
                Exit Sub
            End If
            tema1 = pFeatLayer
            tema1_fclass = tema1.FeatureClass

            'Define segundo tema
            '*********************
            pMap = pMxDoc.FocusMap
            afound = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Antecesor" Then
                    pFeatLayer = pMap.Layer(A)
                    afound = True
                    'tema2 = pFeatLayer
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No existe el tema de Antecesor del DM Redenuncio", MsgBoxStyle.Information, "Observación...")
                Exit Sub
            End If
            tema2 = pFeatLayer
            tema2_fclass = tema2.FeatureClass
            pFCName = New FeatureClassName
            pDatasetName = pFCName
            pNewWSName = New WorkspaceName
            pNewWSName.WorkspaceFactoryProgID = "esriCore.ShapeFileWorkspaceFactory"
            pNewWSName.PathName = glo_pathTMP
            pDatasetName.WorkspaceName = pNewWSName
            pDatasetName.Name = "Antecesor_f"
            ' Perfomance del area interceptada
            Dim tol As Double = 0.0
            Dim pOutputFC As IFeatureClass
            pBGP = New BasicGeoprocessor
            'Define tolerancia
            tol = 0.01
            pOutputFC = pBGP.Intersect(tema1_fclass, False, tema2_fclass, False, tol, pFCName)
            Dim pOutputFeatLayer As IFeatureLayer
            pOutputFeatLayer = New FeatureLayer
            pOutputFeatLayer.FeatureClass = pOutputFC
            pOutputFeatLayer.Name = "Antecesor_f"
            pMap.AddLayer(pOutputFeatLayer)
            pMxDoc.ActivatedView.Refresh()
            valida = True
        Catch ex As Exception
            valida = False
        End Try
    End Sub
    Public Sub AddLayerFromFile(ByVal pApp As IApplication)
        Dim pGxLayer As IGxLayer
        Dim pGxFile As IGxFile
        Dim strLayerPath As String
        Dim datum As String
        datum = v_sistema
        pGxLayer = New GxLayer
        pGxFile = pGxLayer
        If v_sistema = "PSAD-56" Then
            strLayerPath = glo_pathStyle_paises & "PAISES.lyr"
        ElseIf v_sistema = "WGS-84" Then
            strLayerPath = glo_pathStyle_paises & "Paises_84.lyr"


        End If
        ' strLayerPath = "C:\temp\hoja.lyr"
        pGxFile.Path = strLayerPath
        If Not pGxLayer.Layer Is Nothing Then
            pMap = pMxDoc.FocusMap
            pMap.AddLayer(pGxLayer.Layer)
        End If
    End Sub

    Public Sub AddLayerFromFile_frontera(ByVal pApp As IApplication)
        Dim pGxLayer As IGxLayer
        Dim pGxFile As IGxFile
        Dim strLayerPath As String
        Dim datum As String
        datum = v_sistema
        pGxLayer = New GxLayer
        pGxFile = pGxLayer
        If v_sistema = "PSAD-56" Then
            strLayerPath = glo_pathStyle_paises & "PAISES.lyr"
        ElseIf v_sistema = "WGS-84" Then
            strLayerPath = glo_pathStyle_paises & "Paises_84.lyr"
        End If

        ' strLayerPath = "C:\temp\hoja.lyr"
        pGxFile.Path = strLayerPath
        If Not pGxLayer.Layer Is Nothing Then
            pMap = pMxDoc.FocusMap
            pMap.AddLayer(pGxLayer.Layer)
        End If







    End Sub

    Public Sub AddLayerFromFile_acceditario(ByVal pApp As IApplication, ByVal p_Zona As String)
        Dim pGxLayer As IGxLayer
        Dim pGxFile As IGxFile
        Dim strLayerPath As String

        ' BLOQUE QUE AGREGA ACCEDITARIOS
        pGxLayer = New GxLayer
        pGxFile = pGxLayer
        'strLayerPath = glo_pathStyle_paises & "PAISES.lyr"
        strLayerPath = glo_pathStyle_acceditario & carpeta_datum & "\acceditario" & p_Zona & ".lyr"

        ' strLayerPath = "C:\temp\hoja.lyr"
        pGxFile.Path = strLayerPath
        pGxLayer.Layer.Visible = False

        If Not pGxLayer.Layer Is Nothing Then
            pMap = pMxDoc.FocusMap
            pMap.AddLayer(pGxLayer.Layer)
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'agrega capa dac
        'If v_sistema = "WGS-84" Then
        pGxLayer = New GxLayer
        pGxFile = pGxLayer
        strLayerPath = glo_pathStyle_acceditario & carpeta_datum & "\RNDC_RS" & p_Zona & ".lyr"

        ' strLayerPath = "C:\temp\hoja.lyr"
        pGxFile.Path = strLayerPath
        pGxLayer.Layer.Visible = False

        If Not pGxLayer.Layer Is Nothing Then
            pMap = pMxDoc.FocusMap
            pMap.AddLayer(pGxLayer.Layer)
        End If
        'End If
        'If v_sistema = "WGS-84" Then
        '    pGxLayer = New GxLayer
        '    pGxFile = pGxLayer
        '    strLayerPath = glo_pathStyle_acceditario & carpeta_datum & "\dac_" & p_Zona & ".lyr"

        '    ' strLayerPath = "C:\temp\hoja.lyr"
        '    pGxFile.Path = strLayerPath
        '    pGxLayer.Layer.Visible = False

        '    If Not pGxLayer.Layer Is Nothing Then
        '        pMap = pMxDoc.FocusMap
        '        pMap.AddLayer(pGxLayer.Layer)
        '    End If
        'End If
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        ' BLOQUE QUE AGREGA IMAGENES
        pGxLayer = New GxLayer
        pGxFile = pGxLayer
        strLayerPath = glo_pathStyle_paises & "INTRANET_SERV_CATALOGO_IMAGENES.lyr"

        pGxFile.Path = strLayerPath
        pGxLayer.Layer.Visible = False

        If Not pGxLayer.Layer Is Nothing Then
            pMap = pMxDoc.FocusMap
            pMap.AddLayer(pGxLayer.Layer)
        End If

    End Sub

    Public Sub AddLayerFromFile_afectacion(ByVal pApp As IApplication, ByVal p_Zona As String)
        Dim pGxLayer As IGxLayer
        Dim pGxFile As IGxFile
        Dim strLayerPath As String

        ' BLOQUE QUE AGREGA ACCEDITARIOS
        pGxLayer = New GxLayer
        pGxFile = pGxLayer
        strLayerPath = glo_pathTMP & "\afectacion" & p_Zona & ".shp"

        pGxFile.Path = strLayerPath
        pGxLayer.Layer.Visible = False

        If Not pGxLayer.Layer Is Nothing Then
            pMap = pMxDoc.FocusMap
            pMap.AddLayer(pGxLayer.Layer)
        End If
       
    End Sub

    Public Function Leer_Dbf(ByVal p_Codigo As String) As Boolean
        Dim pRow As IRow
        pWorkspaceFactory = New ShapefileWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_pathServidor & "Geocatmin\DBF\", 0)
        pTable = pFeatureWorkspace.OpenTable("Repor_act")
        Dim ptableCursor As ICursor
        Dim pfields As Fields
        pfields = pTable.Fields
        Dim pQueryFilter As IQueryFilter
        pQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = "CODIGO = '" & p_Codigo & "' and FECHA = '" & fecha_tabla & "'"
        ptableCursor = pTable.Search(pQueryFilter, True)
        pRow = ptableCursor.NextRow
        Dim lostrAccion As String
        Do Until pRow Is Nothing
            pRow = ptableCursor.NextRow
            Return True
        Loop
        'Query fecha
        pQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = "FECHA = '" & fecha_tabla & "'"
        ptableCursor = pTable.Search(pQueryFilter, True)
        pRow = ptableCursor.NextRow
        Dim cuenta As Integer = 0
        Dim codigo As String
        Do Until pRow Is Nothing
            cuenta = cuenta + 1
            lostrAccion = pRow.Value(pfields.FindField("ACCION"))
            codigo = pRow.Value(pfields.FindField("codigo"))
            If lostrAccion = "DELI" Then
                If cuenta = 1 Then
                    listado_dm_eli = "CODIGOU =  '" & codigo & "'"
                ElseIf cuenta > 1 Then
                    listado_dm_eli = listado_dm_eli & " OR " & "CODIGOU =  '" & codigo & "'"
                End If
            End If
            pRow = ptableCursor.NextRow
        Loop
    End Function

    Public Function Consulta_tabla_ProvColin()
        Dim pRow As IRow
        pWorkspaceFactory = New ShapefileWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_pathServidor & "Geocatmin\Estilos\", 0)
        pTable = pFeatureWorkspace.OpenTable("Lista_provc")
        Dim ptableCursor As ICursor
        Dim pfields As Fields
        pfields = pTable.Fields
        Dim pQueryFilter As IQueryFilter
        pQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = lista_cod_region_sele
        ptableCursor = pTable.Search(pQueryFilter, True)
        pRow = ptableCursor.NextRow


        Dim cuenta As Integer = 0
        Dim codigo As String
        Do Until pRow Is Nothing
            cuenta = cuenta + 1
            codigo = pRow.Value(pfields.FindField("Cod_Provc"))


            If cuenta = 1 Then
                lista_prov_colin = "CD_PROV =  '" & codigo & "'"
            ElseIf cuenta > 1 Then
                lista_prov_colin = lista_prov_colin & " OR " & "CD_PROV =  '" & codigo & "'"
            End If

            pRow = ptableCursor.NextRow

        Loop
        '   MsgBox(lista_prov_colin)
    End Function


    Public Sub consulta_dm_prioritarios(ByVal p_App As IApplication)  'para encontrar interceccion dm x prioritarios
        Dim cls_eval As New Cls_evaluacion
        Dim pFeatLayer As IFeatureLayer = Nothing
        'Dim pMxApp As IMxApplication
        'pMxApp = pApp
        Dim pFeatureSelection As IFeatureSelection
        Dim capa_sele As ISelectionSet
        pMap = pMxDoc.FocusMap
        Dim afound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Catastro" Then
                pFeatLayer = pMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra el Layer")
            Exit Sub
        End If
        Dim loStrShapefile As String
        cls_eval.consultacapaDM("", "Prioritarios_v", "Catastro")
        pFeatureSelection = pFeatLayer
        capa_sele = pFeatureSelection.SelectionSet
        If capa_sele.Count > 0 Then  ' Si existe genera capa de dm prioritarios
            v_cantiprioritarios = 1
            Try
                'Kill(glo_pathTMP & "\Prioritarios*.*")
                Kill(glo_pathTMP & "Prioritarios*.*")
            Catch ex As Exception
            End Try
            fecha_archi_prioritario = DateTime.Now.Ticks.ToString

            '  cls_Catastro.Exportando_Temas("Prioritarios_v", "Catastro", pApp)
            cls_Catastro.Exportando_Temas("", "Catastro", pApp)
            pFeatureSelection.Clear()
            loStrShapefile = "Prioritarios" & v_codigo & fecha_archi_prioritario

            'gloNumprioritario = v_codigo & fecha_archi_sup
            cls_Catastro.Add_ShapeFile1("Prioritarios" & v_codigo & fecha_archi_prioritario, pApp, "Prioritarios")
            ' cls_Catastro.Add_ShapeFile3("Prioritarios" & v_codigo & fecha_archi_prioritario, pApp)

        Else
            'Si hay para calcular area disponible
            'MsgBox("El DM evaluado no presenta derechos prioritarios considerados para el cálculo de Area Disponible...", vbCritical, "OBSERVACION...")
            v_cantiprioritarios = 0
            Exit Sub
        End If
        'loStrShapefile = "DM_" & v_codigo
        loStrShapefile = loStrShapefile_dm
        cls_Catastro.Add_ShapeFile1(loStrShapefile, m_application, "codigo")
        ' cls_Catastro.Add_ShapeFile3(loStrShapefile, m_application)


        pMxDoc.ActiveView.Refresh()
    End Sub
    Public Sub AddLayerFromFile1(ByVal pApp As IApplication, ByVal nlayer As String)
        Dim pGxLayer As IGxLayer
        Dim pGxFile As IGxFile
        Dim strLayerPath As String
        pGxLayer = New GxLayer
        pGxFile = pGxLayer

        If nlayer = "Departamentos" Then
            'strLayerPath = "U:\Geocatmin\Estilos\Departamentos.lyr"
            strLayerPath = "U:\Geocatmin\Estilos\" & nlayer & v_zona_dm & ".lyr"
        ElseIf nlayer = "Departamentos_wgs" Then
            strLayerPath = "U:\Geocatmin\Estilos\" & nlayer & v_zona_dm & ".lyr"
        ElseIf nlayer = "Frontera_" & v_zona_dm Then
            strLayerPath = "U:\Geocatmin\Estilos\" & nlayer & ".lyr"
        ElseIf nlayer = "Frontera_WGS" & v_zona_dm Then
            strLayerPath = "U:\Geocatmin\Estilos\" & nlayer & ".lyr"
        End If
        pGxFile.Path = strLayerPath
        If Not pGxLayer.Layer Is Nothing Then
            pMap = pMxDoc.FocusMap
            pMap.AddLayer(pGxLayer.Layer)
        End If
    End Sub

    Public Sub ver_DM_segunresoluciones(ByVal pApp As IApplication)
        Try
            Dim pFeatLayer As IFeatureLayer = Nothing
            Dim cls_prueba As New cls_Prueba
            Dim fclas_tema As IFeatureClass
            Dim pActiveView As IActiveView
            Dim pFeatureCursor As IFeatureCursor
            pMap = pMxDoc.FocusMap
            pActiveView = pMap
            Dim b As Integer
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Catastro" Then
                    pFeatLayer = pMap.Layer(A)
                    b = A
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If

            fclas_tema = pFeatLayer.FeatureClass
            Dim pFeature As IFeature
            Dim v_codigo_reso As String
            Dim cls_Oracle As New cls_Oracle
            Dim lodtbDatos As New DataTable
            Dim datol As String
            Dim dato2 As String
            Dim dato2x As String
            Dim dato3 As String
            Dim dato4 As String
            Dim dato4x As String
            Dim dato5 As String
            pFeatureCursor = fclas_tema.Update(Nothing, False)
            pFeature = pFeatureCursor.NextFeature
            Do Until pFeature Is Nothing
                v_codigo_reso = pFeature.Value(pFeatureCursor.FindField("CODIGOU"))
                lodtbDatos = cls_Oracle.P_Obtiene_Datos_DM_RESO(v_codigo_reso)
                If lodtbDatos.Rows.Count > 0 Then
                    datol = lodtbDatos.Rows(0).Item("RESOL_TIT").ToString
                    If IsDate(lodtbDatos.Rows(0).Item("FEC_TITU")) = False Then
                        dato2 = Format(lodtbDatos.Rows(0).Item("FEC_TITU").ToString, "18000101")
                        dato2x = lodtbDatos.Rows(0).Item("FEC_TITU").ToString
                    Else
                        dato2 = Format(lodtbDatos.Rows(0).Item("FEC_TITU"), "yyyymmdd")
                        dato2x = lodtbDatos.Rows(0).Item("FEC_TITU")
                    End If
                    dato3 = lodtbDatos.Rows(0).Item("RESOL_EXT").ToString
                    If IsDate(lodtbDatos.Rows(0).Item("FEC_EXT")) = False Then
                        dato4 = Format(lodtbDatos.Rows(0).Item("FEC_EXT").ToString, "18000101")
                        dato4x = lodtbDatos.Rows(0).Item("FEC_EXT").ToString
                    Else
                        dato4 = Format(lodtbDatos.Rows(0).Item("FEC_EXT"), "yyyymmdd")
                        dato4x = lodtbDatos.Rows(0).Item("FEC_EXT")
                    End If
                    dato5 = lodtbDatos.Rows(0).Item("CALI").ToString
                    If dato2 > dato4 Then
                        If pFeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_reso Then
                            pFeature.Value(pFeatureCursor.FindField("NUM_RESOL")) = datol
                            pFeature.Value(pFeatureCursor.FindField("FEC_RESOL")) = dato2x
                            pFeature.Value(pFeatureCursor.FindField("CALIF")) = dato5
                            pFeatureCursor.UpdateFeature(pFeature)
                        End If
                    Else

                        If pFeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_reso Then
                            pFeature.Value(pFeatureCursor.FindField("NUM_RESOL")) = dato3
                            pFeature.Value(pFeatureCursor.FindField("FEC_RESOL")) = dato4x
                            pFeature.Value(pFeatureCursor.FindField("CALIF")) = dato5
                            pFeatureCursor.UpdateFeature(pFeature)
                        End If
                    End If
                End If
                pFeature = pFeatureCursor.NextFeature
            Loop
        Catch ex As Exception
            MsgBox("No se Calculo los datos para la Capa de DM.", MsgBoxStyle.Information, "[BDGEOCATMIN]")
        End Try
        MsgBox("Se Termino Satisfactoriamente la consulta solicitada...", MsgBoxStyle.Information, "[BDGEOCATMIN]")

    End Sub

    Public Sub Genera_DM_ESTAMIN_ANTERIOR(ByVal pApp As IApplication)
        Try
            Dim cls_eval As New Cls_evaluacion
            fecha_dm_ex = DateTime.Now.Ticks.ToString
            'loStrShapefile_EX = "Situacion_" & fecha_dm_ex
            Dim pFeatLayer As IFeatureLayer = Nothing
            Dim cls_prueba As New cls_Prueba
            Dim fclas_tema As IFeatureClass
            Dim pActiveView As IActiveView
            Dim pFeatureCursor As IFeatureCursor
            pMap = pMxDoc.FocusMap
            pActiveView = pMap
            Dim b As Integer
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Catastro" Then
                    pFeatLayer = pMap.Layer(A)
                    b = A
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If
            cls_eval.consultacapaDM("", "Catastro", "Catastro")
            pFeatureSelection = pFeatLayer
            cls_Catastro.Exportando_Temas("Situacion", "Situacion", pApp)
            pFeatureSelection.Clear()
            cls_Catastro.Add_ShapeFile2("Situacion_" & fecha_dm_ex, pApp, "Situacion")
            Dim c As Integer
            Dim afound1 As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Situacion_DM" Then
                    pFeatLayer = pMap.Layer(A)
                    c = A
                    afound1 = True
                    Exit For
                End If
            Next A
            If Not afound1 Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If
            fclas_tema = pFeatLayer.FeatureClass
            Dim pFeature As IFeature
            Dim v_codigo_estamin As String
            Dim cls_Oracle As New cls_Oracle
            Dim lodtbDatos As New DataTable
            Dim lodtbDatos1 As New DataTable
            Dim dato As String
            Dim datol As String

            pFeatureCursor = fclas_tema.Update(Nothing, False)
            pFeature = pFeatureCursor.NextFeature
            Do Until pFeature Is Nothing
                v_codigo_estamin = pFeature.Value(pFeatureCursor.FindField("CODIGOU"))

                lodtbDatos = cls_Oracle.P_Obtiene_Datos_DM_ESTAMIN(v_codigo_estamin)
                If lodtbDatos.Rows.Count > 0 Then
                    dato = lodtbDatos.Rows(0).Item("SITUACIONUP").ToString
                    If pFeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_estamin Then
                        pFeature.Value(pFeatureCursor.FindField("TIPO")) = dato
                        pFeatureCursor.UpdateFeature(pFeature)
                    End If
                Else
                    lodtbDatos1 = cls_Oracle.P_Obtiene_Datos_DM_INTEGRANTE_UEA(v_codigo_estamin)
                    If lodtbDatos1.Rows.Count > 0 Then
                        'datol = lodtbDatos1.Rows(0).Item("CG_CODIGO").ToString
                        For r As Integer = 0 To lodtbDatos1.Rows.Count - 1
                            datol = lodtbDatos1.Rows(r).Item("CG_CODIGO").ToString
                            lodtbDatos = cls_Oracle.P_Obtiene_Datos_DM_ESTAMIN(datol)
                            If lodtbDatos.Rows.Count > 0 Then
                                dato = lodtbDatos.Rows(0).Item("SITUACIONUP").ToString
                                If pFeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_estamin Then
                                    pFeature.Value(pFeatureCursor.FindField("TIPO")) = dato
                                    pFeatureCursor.UpdateFeature(pFeature)
                                End If
                            End If
                        Next r
                    End If
                End If

                pFeature = pFeatureCursor.NextFeature
            Loop
            cls_Catastro.Genera_Tematico_Estamin(pApp)

          

        Catch ex As Exception
            MsgBox("No se Calculo los datos para la Capa de DM.", MsgBoxStyle.Information, "[BDGEOCATMIN]")
        End Try
        MsgBox("Se Termino Satisfactoriamente la consulta solicitada...", MsgBoxStyle.Information, "[BDGEOCATMIN]")


    End Sub
    Public Sub Genera_DM_ESTAMIN(ByVal pApp As IApplication)
        Try
            Dim cls_eval As New Cls_evaluacion
            fecha_dm_ex = DateTime.Now.Ticks.ToString
            'loStrShapefile_EX = "Situacion_" & fecha_dm_ex
            Dim pFeatLayer As IFeatureLayer = Nothing
            Dim cls_prueba As New cls_Prueba
            Dim fclas_tema As IFeatureClass
            Dim pActiveView As IActiveView
            Dim pFeatureCursor As IFeatureCursor
            pMap = pMxDoc.FocusMap
            pActiveView = pMap
            Dim b As Integer
            Dim afound As Boolean = False


            Dim player As ILayer
            player = pMxDoc.SelectedLayer
            'Se incluyo esta parte para cualquier poligono


            If player Is Nothing Then  ' capa no seleccionada
                MsgBox("NO HA SELECCIONADO NINGUNA CAPA PARA CONSULTAR LAS COORDENADAS", vbCritical, "SIGCATMIN...")
                Exit Sub

            Else
                pFeatLayer = player
                player.Name = "Catastro"
                pMxDoc.UpdateContents()
                pMxDoc.ActiveView.Refresh()

            End If


            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Catastro" Then
                    pFeatLayer = pMap.Layer(A)
                    b = A
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If
            cls_eval.consultacapaDM("", "Catastro", "Catastro")
            pFeatureSelection = pFeatLayer
            cls_Catastro.Exportando_Temas("Situacion", "Situacion", pApp)
            pFeatureSelection.Clear()
            cls_Catastro.Add_ShapeFile2("Situacion_" & fecha_dm_ex, pApp, "Situacion")
            cls_eval.agregacampotema_tpm("Situacion_DM", "estamin") 'se puso 
            Dim c As Integer
            Dim afound1 As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Situacion_DM" Then
                    pFeatLayer = pMap.Layer(A)
                    c = A
                    afound1 = True
                    Exit For
                End If
            Next A
            If Not afound1 Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If
            fclas_tema = pFeatLayer.FeatureClass
            Dim pFeature As IFeature
            Dim v_codigo_estamin As String
            Dim cls_Oracle As New cls_Oracle
            Dim lodtbDatos As New DataTable
            Dim lodtbDatos1 As New DataTable
            Dim dato As String
            Dim datol As String

            pFeatureCursor = fclas_tema.Update(Nothing, False)
            pFeature = pFeatureCursor.NextFeature
            Do Until pFeature Is Nothing
                v_codigo_estamin = pFeature.Value(pFeatureCursor.FindField("CODIGOU"))
                Try
                    lodtbDatos = cls_Oracle.P_Obtiene_Datos_DM_ESTAMIN(v_codigo_estamin)
                    If lodtbDatos.Rows.Count > 0 Then
                        dato = lodtbDatos.Rows(0).Item("SITUACIONUP").ToString
                        If pFeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_estamin Then
                            pFeature.Value(pFeatureCursor.FindField("TIPO")) = dato
                            pFeatureCursor.UpdateFeature(pFeature)
                        End If
                    Else
                        Try
                            lodtbDatos1 = cls_Oracle.P_Obtiene_Datos_DM_INTEGRANTE_UEA(v_codigo_estamin)
                            If lodtbDatos1.Rows.Count > 0 Then
                                'datol = lodtbDatos1.Rows(0).Item("CG_CODIGO").ToString
                                For r As Integer = 0 To lodtbDatos1.Rows.Count - 1
                                    datol = lodtbDatos1.Rows(r).Item("CG_CODIGO").ToString
                                    Try
                                        lodtbDatos = cls_Oracle.P_Obtiene_Datos_DM_ESTAMIN(datol)
                                        If lodtbDatos.Rows.Count > 0 Then
                                            dato = lodtbDatos.Rows(0).Item("SITUACIONUP").ToString
                                            If pFeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_estamin Then
                                                pFeature.Value(pFeatureCursor.FindField("TIPO")) = dato
                                                pFeatureCursor.UpdateFeature(pFeature)
                                            End If
                                        End If

                                    Catch ex As Exception
                                        ' MsgBox("4", MsgBoxStyle.Critical, "4")
                                    End Try

                                Next r
                            End If

                        Catch ex As Exception
                            ' MsgBox("4", MsgBoxStyle.Critical, "4")
                        End Try

                    End If

                Catch ex As Exception
                    ' MsgBox("4", MsgBoxStyle.Critical, "4")
                End Try

                pFeature = pFeatureCursor.NextFeature
            Loop


        'Realizando las consultas a la tabla para calculos valores en los camopos para la leyenda
        '-----------------------------------------------------------------------------------------
            ' MsgBox("2", MsgBoxStyle.Critical, "gis2")
        Dim pFields As IFields
        Dim ii As Integer

        pFields = fclas_tema.Fields
        ii = pFields.FindField("TIPO")
        Dim pFeatureSelection1 As IFeatureSelection

        Dim pCursor As ICursor
        Dim pCalculator As ICalculator
        pFeatureSelection1 = pFeatLayer

        Dim pQueryFilter As IQueryFilter
        'Para casos de de Exploracion

        pQueryFilter = New QueryFilter
        pMap = pMxDoc.FocusMap

        pQueryFilter.WhereClause = "TIPO = 'EXPLORACIÓN'"

        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        Dim pFSel As ISelectionSet
        pFSel = pFeatureSelection1.SelectionSet

        If (pFSel.Count > 0) Then
            pCursor = fclas_tema.Update(pQueryFilter, True)
            '  pCursor = fclas_tema.Update(Nothing, True)
            pCalculator = New Calculator
            With pCalculator
                .Cursor = pCursor
                '   .Expression = "(([Pop2000] - [Pop1990]) / [Pop1990]) * 100"
                ' .Expression = "([LEYENDA])"
                .Expression = """CONCESIÓN MINERA EN EXPLORACIÓN (1)"""
                .Field = "TIPO"
            End With
            pCalculator.Calculate()

        End If

        'Para casos de de Explotacion

        pQueryFilter = New QueryFilter
        pMap = pMxDoc.FocusMap

        pQueryFilter.WhereClause = "TIPO = 'EXPLOTACIÓN'"

        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        ' Dim pFSel As ISelectionSet
        pFSel = pFeatureSelection1.SelectionSet

        If (pFSel.Count > 0) Then
            pCursor = fclas_tema.Update(pQueryFilter, True)
            '  pCursor = fclas_tema.Update(Nothing, True)
            pCalculator = New Calculator
            With pCalculator
                .Cursor = pCursor
                '   .Expression = "(([Pop2000] - [Pop1990]) / [Pop1990]) * 100"
                ' .Expression = "([LEYENDA])"
                .Expression = """CONCESIÓN MINERA EN EXPLOTACIÓN (1)"""
                .Field = "TIPO"
            End With
            pCalculator.Calculate()

        End If


        'Para DM de Solicitud de derecho minero

        pQueryFilter = New QueryFilter
        pMap = pMxDoc.FocusMap

        pQueryFilter.WhereClause = "TIPO = ''"

        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)


        pQueryFilter.WhereClause = "LEYENDA = 'G2'"

        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultAnd, False)
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

        pFSel = pFeatureSelection1.SelectionSet

        If (pFSel.Count > 0) Then
            pCursor = fclas_tema.Update(pQueryFilter, True)
            '  pCursor = fclas_tema.Update(Nothing, True)
            pCalculator = New Calculator
            With pCalculator
                .Cursor = pCursor
                '   .Expression = "(([Pop2000] - [Pop1990]) / [Pop1990]) * 100"
                ' .Expression = "([LEYENDA])"
                .Expression = """SOLICITUD DE DERECHO MINERO"""
                .Field = "TIPO"
            End With
            pCalculator.Calculate()

        End If


        'Para DM de CONCESIÓN MINERA EXTINGUIDA

        pQueryFilter = New QueryFilter
        pMap = pMxDoc.FocusMap

        pQueryFilter.WhereClause = "TIPO = ''"

        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)


        pQueryFilter.WhereClause = "LEYENDA = 'G4'"

        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultAnd, False)
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

        pFSel = pFeatureSelection1.SelectionSet

        If (pFSel.Count > 0) Then
            pCursor = fclas_tema.Update(pQueryFilter, True)
            '  pCursor = fclas_tema.Update(Nothing, True)
            pCalculator = New Calculator
            With pCalculator
                .Cursor = pCursor
                '   .Expression = "(([Pop2000] - [Pop1990]) / [Pop1990]) * 100"
                ' .Expression = "([LEYENDA])"
                .Expression = """CONCESIÓN MINERA EXTINGUIDA"""
                .Field = "TIPO"
            End With
            pCalculator.Calculate()

        End If


        'Para DM de PLANTAS DE BENEFICIO, CANTERAS (ESTADO)

        pQueryFilter = New QueryFilter
        pMap = pMxDoc.FocusMap

        pQueryFilter.WhereClause = "TIPO = ''"

        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)


        pQueryFilter.WhereClause = "LEYENDA = 'G5'"

        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultAnd, False)
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

        pFSel = pFeatureSelection1.SelectionSet

        If (pFSel.Count > 0) Then
            pCursor = fclas_tema.Update(pQueryFilter, True)
            '  pCursor = fclas_tema.Update(Nothing, True)
            pCalculator = New Calculator
            With pCalculator
                .Cursor = pCursor
                '   .Expression = "(([Pop2000] - [Pop1990]) / [Pop1990]) * 100"
                ' .Expression = "([LEYENDA])"
                .Expression = """PLANTAS DE BENEFICIO, CANTERAS (ESTADO)"""
                .Field = "TIPO"
            End With
            pCalculator.Calculate()

        End If


        'Para DM sin actividad minera


        pQueryFilter = New QueryFilter
        pMap = pMxDoc.FocusMap

        pQueryFilter.WhereClause = "TIPO = ''"

        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)



        pFSel = pFeatureSelection1.SelectionSet

        If (pFSel.Count > 0) Then
            pCursor = fclas_tema.Update(pQueryFilter, True)
            '  pCursor = fclas_tema.Update(Nothing, True)
            pCalculator = New Calculator
            With pCalculator
                .Cursor = pCursor
                '   .Expression = "(([Pop2000] - [Pop1990]) / [Pop1990]) * 100"
                ' .Expression = "([LEYENDA])"
                .Expression = """CONCESIÓN SIN ACTIVIDAD MINERA"""
                .Field = "TIPO"
            End With
            pCalculator.Calculate()

        End If

        pFeatureSelection1.Clear()


        cls_Catastro.Genera_Tematico_Estamin(pApp)


        cls_Catastro.Quitar_Layer("Catastro", pApp)
        cls_Catastro.Limpiar_Texto_Pantalla(pApp)

        cls_Catastro.rotulatexto_dm("Situacion_DM", pApp)
        cls_Catastro.Load_FC_GDB("Mallap_" & v_zona_dm, "", pApp, True)
        cls_Catastro.Rotular_texto_DM("Mallap_" & v_zona_dm, v_zona_dm, pApp)
        cls_Catastro.rotulatexto_dm("Zona Reservada", pApp)
        cls_Catastro.rotulatexto_dm("Zona Urbana", pApp)
        cls_Catastro.Quitar_Layer("Mallap_" & v_zona_dm, pApp)


        Catch ex As Exception
            MsgBox("No se Calculo los datos para la Capa de DM.", MsgBoxStyle.Information, "[SIGCATMIN]")
        End Try
        MsgBox("Se Termino Satisfactoriamente la consulta solicitada...", MsgBoxStyle.Information, "[SIGCATMIN]")


    End Sub

    Public Sub Genera_DM_ESTAMIN_ant_implementacion(ByVal pApp As IApplication)
        Try
            Dim cls_eval As New Cls_evaluacion
            fecha_dm_ex = DateTime.Now.Ticks.ToString
            'loStrShapefile_EX = "Situacion_" & fecha_dm_ex
            Dim pFeatLayer As IFeatureLayer = Nothing
            Dim cls_prueba As New cls_Prueba
            Dim fclas_tema As IFeatureClass
            Dim pActiveView As IActiveView
            Dim pFeatureCursor As IFeatureCursor
            pMap = pMxDoc.FocusMap
            pActiveView = pMap
            Dim b As Integer
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Catastro" Then
                    pFeatLayer = pMap.Layer(A)
                    b = A
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If
            cls_eval.consultacapaDM("", "Catastro", "Catastro")
            pFeatureSelection = pFeatLayer
            cls_Catastro.Exportando_Temas("Situacion", "Situacion", pApp)
            pFeatureSelection.Clear()
            cls_Catastro.Add_ShapeFile2("Situacion_" & fecha_dm_ex, pApp, "Situacion")
            Dim c As Integer
            Dim afound1 As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Situacion_DM" Then
                    pFeatLayer = pMap.Layer(A)
                    c = A
                    afound1 = True
                    Exit For
                End If
            Next A
            If Not afound1 Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If
            fclas_tema = pFeatLayer.FeatureClass
            Dim pFeature As IFeature
            Dim v_codigo_estamin As String
            Dim cls_Oracle As New cls_Oracle
            Dim lodtbDatos As New DataTable
            Dim lodtbDatos1 As New DataTable
            Dim dato As String
            Dim datol As String

            pFeatureCursor = fclas_tema.Update(Nothing, False)
            pFeature = pFeatureCursor.NextFeature
            Do Until pFeature Is Nothing
                v_codigo_estamin = pFeature.Value(pFeatureCursor.FindField("CODIGOU"))

                lodtbDatos = cls_Oracle.FT_obtiene_situacion_dm(v_codigo_estamin)


                'If lodbtExisteAR.Rows.Count >= 1 Then
                '    For contador As Integer = 0 To lodbtExisteAR.Rows.Count - 1
                '        nm_rese = lodbtExisteAR.Rows(contador).Item("NM_RESE")
                '        If contador = 0 Then
                '            lista_rese = nm_rese
                '        ElseIf contador > 0 Then
                '            lista_rese = lista_rese & "," & nm_rese
                '        End If
                '    Next contador
                'End If

                If lodtbDatos.Rows.Count > 0 Then
                    For contador As Integer = 0 To lodtbDatos.Rows.Count - 1
                        dato = lodtbDatos.Rows(contador).Item("CODIGO").ToString
                        'dato = lodtbDatos.Rows(0).Item("SITUACIONUP").ToString
                        '  dato = lodtbDatos.Rows(0).Item("CODIGO").ToString
                        '  If pFeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_estamin Then
                        If dato = v_codigo_estamin Then
                            datol = lodtbDatos.Rows(contador).Item("SITUACION").ToString
                            pFeature.Value(pFeatureCursor.FindField("TIPO")) = datol
                            pFeatureCursor.UpdateFeature(pFeature)
                        End If
                    Next contador
                    'Else
                    '   lodtbDatos1 = cls_Oracle.P_Obtiene_Datos_DM_INTEGRANTE_UEA(v_codigo_estamin)
                    '  If lodtbDatos1.Rows.Count > 0 Then
                    ''datol = lodtbDatos1.Rows(0).Item("CG_CODIGO").ToString
                    'For r As Integer = 0 To lodtbDatos1.Rows.Count - 1
                    'datol = lodtbDatos1.Rows(r).Item("CG_CODIGO").ToString
                    'lodtbDatos = cls_Oracle.P_Obtiene_Datos_DM_ESTAMIN(datol)
                    'If lodtbDatos.Rows.Count > 0 Then
                    ' dato = lodtbDatos.Rows(0).Item("SITUACIONUP").ToString
                    ' If pFeature.Value(pFeatureCursor.FindField("CODIGOU")) = v_codigo_estamin Then
                    ' pFeature.Value(pFeatureCursor.FindField("TIPO")) = dato
                    ' pFeatureCursor.UpdateFeature(pFeature)
                    'End If
                    ' End If
                    '    Next r
                    'End If
                End If

                pFeature = pFeatureCursor.NextFeature
            Loop


            'Realizando las consultas a la tabla para calculos valores en los camopos para la leyenda
            '-----------------------------------------------------------------------------------------

            Dim pFields As IFields
            Dim ii As Integer

            pFields = fclas_tema.Fields
            ii = pFields.FindField("TIPO")
            Dim pFeatureSelection1 As IFeatureSelection

            Dim pCursor As ICursor
            Dim pCalculator As ICalculator
            pFeatureSelection1 = pFeatLayer

            Dim pQueryFilter As IQueryFilter
            'Para casos de de Exploracion

            pQueryFilter = New QueryFilter
            pMap = pMxDoc.FocusMap

            pQueryFilter.WhereClause = "TIPO = 'EXPLORACIÓN'"

            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            Dim pFSel As ISelectionSet
            pFSel = pFeatureSelection1.SelectionSet

            If (pFSel.Count > 0) Then
                pCursor = fclas_tema.Update(pQueryFilter, True)
                '  pCursor = fclas_tema.Update(Nothing, True)
                pCalculator = New Calculator
                With pCalculator
                    .Cursor = pCursor
                    '   .Expression = "(([Pop2000] - [Pop1990]) / [Pop1990]) * 100"
                    ' .Expression = "([LEYENDA])"
                    .Expression = """CONCESIÓN MINERA EN EXPLORACIÓN (1)"""
                    .Field = "TIPO"
                End With
                pCalculator.Calculate()

            End If

            'Para casos de de Explotacion

            pQueryFilter = New QueryFilter
            pMap = pMxDoc.FocusMap

            pQueryFilter.WhereClause = "TIPO = 'EXPLOTACIÓN'"

            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            ' Dim pFSel As ISelectionSet
            pFSel = pFeatureSelection1.SelectionSet

            If (pFSel.Count > 0) Then
                pCursor = fclas_tema.Update(pQueryFilter, True)
                '  pCursor = fclas_tema.Update(Nothing, True)
                pCalculator = New Calculator
                With pCalculator
                    .Cursor = pCursor
                    '   .Expression = "(([Pop2000] - [Pop1990]) / [Pop1990]) * 100"
                    ' .Expression = "([LEYENDA])"
                    .Expression = """CONCESIÓN MINERA EN EXPLOTACIÓN (1)"""
                    .Field = "TIPO"
                End With
                pCalculator.Calculate()

            End If


            'Para DM de Solicitud de derecho minero

            pQueryFilter = New QueryFilter
            pMap = pMxDoc.FocusMap

            pQueryFilter.WhereClause = "TIPO = ''"

            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)


            pQueryFilter.WhereClause = "LEYENDA = 'G2'"

            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultAnd, False)
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

            pFSel = pFeatureSelection1.SelectionSet

            If (pFSel.Count > 0) Then
                pCursor = fclas_tema.Update(pQueryFilter, True)
                '  pCursor = fclas_tema.Update(Nothing, True)
                pCalculator = New Calculator
                With pCalculator
                    .Cursor = pCursor
                    '   .Expression = "(([Pop2000] - [Pop1990]) / [Pop1990]) * 100"
                    ' .Expression = "([LEYENDA])"
                    .Expression = """SOLICITUD DE DERECHO MINERO"""
                    .Field = "TIPO"
                End With
                pCalculator.Calculate()

            End If


            'Para DM de CONCESIÓN MINERA EXTINGUIDA

            pQueryFilter = New QueryFilter
            pMap = pMxDoc.FocusMap

            pQueryFilter.WhereClause = "TIPO = ''"

            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)


            pQueryFilter.WhereClause = "LEYENDA = 'G4'"

            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultAnd, False)
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

            pFSel = pFeatureSelection1.SelectionSet

            If (pFSel.Count > 0) Then
                pCursor = fclas_tema.Update(pQueryFilter, True)
                '  pCursor = fclas_tema.Update(Nothing, True)
                pCalculator = New Calculator
                With pCalculator
                    .Cursor = pCursor
                    '   .Expression = "(([Pop2000] - [Pop1990]) / [Pop1990]) * 100"
                    ' .Expression = "([LEYENDA])"
                    .Expression = """CONCESIÓN MINERA EXTINGUIDA"""
                    .Field = "TIPO"
                End With
                pCalculator.Calculate()

            End If


            'Para DM de PLANTAS DE BENEFICIO, CANTERAS (ESTADO)

            pQueryFilter = New QueryFilter
            pMap = pMxDoc.FocusMap

            pQueryFilter.WhereClause = "TIPO = ''"

            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)


            pQueryFilter.WhereClause = "LEYENDA = 'G5'"

            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultAnd, False)
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

            pFSel = pFeatureSelection1.SelectionSet

            If (pFSel.Count > 0) Then
                pCursor = fclas_tema.Update(pQueryFilter, True)
                '  pCursor = fclas_tema.Update(Nothing, True)
                pCalculator = New Calculator
                With pCalculator
                    .Cursor = pCursor
                    '   .Expression = "(([Pop2000] - [Pop1990]) / [Pop1990]) * 100"
                    ' .Expression = "([LEYENDA])"
                    .Expression = """PLANTAS DE BENEFICIO, CANTERAS (ESTADO)"""
                    .Field = "TIPO"
                End With
                pCalculator.Calculate()

            End If


            'Para DM sin actividad minera


            pQueryFilter = New QueryFilter
            pMap = pMxDoc.FocusMap

            pQueryFilter.WhereClause = "TIPO = ''"

            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)



            pFSel = pFeatureSelection1.SelectionSet

            If (pFSel.Count > 0) Then
                pCursor = fclas_tema.Update(pQueryFilter, True)
                '  pCursor = fclas_tema.Update(Nothing, True)
                pCalculator = New Calculator
                With pCalculator
                    .Cursor = pCursor
                    '   .Expression = "(([Pop2000] - [Pop1990]) / [Pop1990]) * 100"
                    ' .Expression = "([LEYENDA])"
                    .Expression = """CONCESIÓN SIN ACTIVIDAD MINERA"""
                    .Field = "TIPO"
                End With
                pCalculator.Calculate()

            End If

            pFeatureSelection1.Clear()

            cls_Catastro.Genera_Tematico_Estamin(pApp)
            cls_Catastro.Quitar_Layer("Catastro", pApp)
            cls_Catastro.Limpiar_Texto_Pantalla(pApp)

            cls_Catastro.rotulatexto_dm("Situacion_DM", pApp)
            cls_Catastro.Load_FC_GDB("Mallap_" & v_zona_dm, "", pApp, True)
            cls_Catastro.Rotular_texto_DM("Mallap_" & v_zona_dm, v_zona_dm, pApp)
            cls_Catastro.rotulatexto_dm("Zona Reservada", pApp)
            cls_Catastro.rotulatexto_dm("Zona Urbana", pApp)
            cls_Catastro.Quitar_Layer("Mallap_" & v_zona_dm, pApp)


        Catch ex As Exception
            MsgBox("No se Calculo los datos para la Capa de DM.", MsgBoxStyle.Information, "[BDGEOCATMIN]")
        End Try
        MsgBox("Se Termino Satisfactoriamente la consulta solicitada...", MsgBoxStyle.Information, "[BDGEOCATMIN]")


    End Sub


    Private Function lista_Cd_Geologia() As String
        Throw New NotImplementedException
    End Function

    'Private Function Islocked(ByVal Var As String) As Boolean
    '    Throw New NotImplementedException
    'End Function


End Class
