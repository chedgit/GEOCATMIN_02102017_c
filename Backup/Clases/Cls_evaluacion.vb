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
            SPATIALFILTER.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
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
    Public Sub consultacapaDM(ByVal v_codigo As String, ByVal seleccion_tema As String, ByVal p_Layer As String)
        pMap = pMxDoc.FocusMap
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim pFeatSelection As IFeatureSelection
        Dim pQueryFilter As IQueryFilter
        Dim afound As Boolean = False
        If V_caso_simu = "SI" Then
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = p_Layer Then
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
            Else
                For A As Integer = 0 To pMap.LayerCount - 1
                    If pMap.Layer(A).Name = p_Layer Then
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
        ElseIf seleccion_tema = "Prioritarios" Then
            pQueryFilter.WhereClause = "EVAL = 'PR'"
        ElseIf seleccion_tema = "EVAL" Then  ' Para consultar solo dm evaluado
            pQueryFilter.WhereClause = "EVAL = 'EV'"
        ElseIf seleccion_tema = "Prioritarios_v" Then
            pQueryFilter.WhereClause = "EVAL = 'PR' AND CALCULO <> 'NO'"
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
        ElseIf seleccion_tema = "Redenuncio" Then  'Para temas de area neta e interceptada
            pQueryFilter.WhereClause = lista_rd
            pQFilter.WhereClause = lista_rd

        ElseIf seleccion_tema = "LibreDen" Then  'Para temas de libre denunciabilidad
            pQueryFilter.WhereClause = lista_codigo
        ElseIf seleccion_tema = "Catastro" Then
            pQueryFilter.WhereClause = "CODIGOU <> ''"
        ElseIf seleccion_tema = "" Then
            pQueryFilter.WhereClause = "CODIGOU = '" & v_codigo & "'"
        ElseIf seleccion_tema = "Certificacion" Then
            pQueryFilter.WhereClause = "NOMBRE <> 'C'"
        End If

        pFeatSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, Nothing, Nothing)
        pFeatSelection = pFeatLayer
        pFeatureSelection = pFeatSelection
        If seleccion_tema = "union" Then  ' Para obtener solo el area neta
            v_adispo = pFeatSelection.SelectionSet.Count
        End If

    End Sub

    Public Sub actualizaregistrostema(ByVal tipo_consulta As String)

        'PROGRAMA PARA INSERTAR REGISTROS EN UN TEMA
        '*********************************************

        Dim pMap As IMap
        Dim pFeatLayer As IFeatureLayer
        Dim pFeatureClass As IFeatureClass
        pMap = pMxDoc.FocusMap
        pFeatLayer = pMap.Layer(0)
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

                        ElseIf ((v_estado_x = "L") Or (v_estado_x = "J")) Then
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
                        ElseIf ((v_estado_x = "B") Or (v_estado_x = "M") Or (v_estado_x = "G") Or (v_estado_x = "A") Or (v_estado_x = "S") Or (v_estado_x = "R")) Then
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
                End If
                pfeature = pFeatureCursor.NextFeature
            Loop
        Catch ex As Exception
            'MsgBox (ex.Message)
            MsgBox("Error en el proceso de evaluación, es probable que no se haya generado correctamente la evaluación de DM", MsgBoxStyle.Critical, "BDGEOCATMIN")
        End Try

    End Sub

    Public Sub EJECUTACRITERIOS_Libredenu()
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

                        ElseIf ((v_estado_x = "L") Or (v_estado_x = "J")) Then
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
            'Dim afound As Boolean = False
            'For A As Integer = 0 To pMap.LayerCount - 1
            ' If pMap.Layer(A).Name = player Then
            'pFeatLayer = pMxDoc.FocusMap.Layer(A)
            'afound = True : Exit For
            'End If
            'Next A
            'If Not afound Then
            'MsgBox("Layer No Existe.") : Exit Sub
            'End If
            pFeatLayer = pFeatureLayer_cat

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

    Function f_Intercepta_temas(ByVal loFeature As String, ByVal v_este_min As Double, ByVal v_norte_min As Double, ByVal v_este_max As Double, ByVal v_norte_max As Double, ByVal p_App As IApplication, Optional ByVal p_ShapeFile_Out As String = "")
        Dim pFLayer As IFeatureLayer
        pMap = pMxDoc.FocusMap
        If loFeature = "Distrito" Then
            pFLayer = pFeatureLayer_dist
        ElseIf loFeature = "Zona Reservada" Then
            pFLayer = pFeatureLayer_reseg
        ElseIf loFeature = "Zona Urbana" Then
            pFLayer = pFeatureLayer_urba
        ElseIf loFeature = "Cuadrangulo" Then
            pFLayer = pFeatureLayer_hoja
        ElseIf loFeature = "Departamento" Then
            pFLayer = pFeatureLayer_depa
        ElseIf loFeature = "Provincia" Then
            pFLayer = pFeatureLayer_prov
        End If
        Dim pActiveView As IActiveView
        Dim pDisplayTransform As IDisplayTransformation
        Dim pEnvelope As IEnvelope
        'pMxDoc = p_App.Document
        pActiveView = pMxDoc.FocusMap
        pDisplayTransform = pActiveView.ScreenDisplay.DisplayTransformation
        pEnvelope = pDisplayTransform.VisibleBounds

        'Aquí calculo el Extend
        ' pEnvelope.SetEmpty()
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
            '.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects ' esriSpatialRelIntersects
            .SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
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
                            Else
                                If pRow.Value(pRow.Fields.FindField("CD_DEPA")) <> 99 Then
                                    lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("CD_DEPA")) & "',"
                                    colecciones_cd_depa.Add(pRow.Value(pRow.Fields.FindField("CD_DEPA")))
                                    colecciones_depa.Add(pRow.Value(pRow.Fields.FindField("NM_DEPA")))
                                    colecciones_prov.Add(pRow.Value(pRow.Fields.FindField("NM_PROV")))
                                    colecciones_dist.Add(pRow.Value(pRow.Fields.FindField("NM_DIST")))
                                End If
                            End If
                        Case "Zona Urbana"
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("NM_URBA")) & "',"
                            colecciones_urba.Add(pRow.Value(pRow.Fields.FindField("NM_URBA")))
                            colecciones_codurba.Add(pRow.Value(pRow.Fields.FindField("CODIGO")))
                        Case "Zona Reservada"
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("NM_RESE")) & "',"
                            colecciones_rese.Add(pRow.Value(pRow.Fields.FindField("NM_RESE")))
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
                Case "Provincia"
                    lostr_Join_Codigos = "OBJECTID IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                Case "Distrito"
                    lostr_Join_Codigos = "OBJECTID IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                Case "Zona Urbana"
                    lostr_Join_Codigos = "NM_URBA IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                    'lostr_Join_Codigos_zu = "CODIGO IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                Case "Zona Reservada"
                    lostr_Join_Codigos = "NM_RESE IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                Case "Cuadrangulo"
                    'lostr_Join_Codigos = "CD_HOJA IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                    '   colecciones.Add(lostr_Join_Codigos)
                    'MsgBox(colecciones.Items(1).Value)
                Case "Cuadrangulo_1"
                    lostr_Join_Codigos = "CD_HOJA IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                Case Else
                    lostr_Join_Codigos = "OBJECTID IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
            End Select
            Return lostr_Join_Codigos
            pFeatSelection.Clear()
        End If
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
            Else
                If arch_cata = "" Then
                    pFeatureLayerD.DefinitionExpression = lista_nm_depa
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
                    Else
                        pQueryFilter.WhereClause = lista_nm_depa
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
                Else
                    If v_Tema = "Catastro" Then
                        'arch_cata = ""
                        cls_Catastro.Exportando_Temas("", Nom_Shapefile, p_App)
                    End If
                End If

                If arch_cata = "DMxregion" Then
                    pFeatureSelection.Clear()
                    Exit Sub
                End If
                pFeatureSelection = pFeatureLayerD
                pFeatureLayerD = pFeatureSelection
                Dim pFeatureLayer_1 As IFeatureLayer
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
                End If
            End If
        Next i
        pMxDoc.UpdateContents()
    End Sub

    Public Sub Eliminadataframe()
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
                Kill(glo_pathTMP & "\Prioritarios*.*")
            Catch ex As Exception
            End Try
            cls_Catastro.Exportando_Temas("", "Catastro", pApp)
            pFeatureSelection.Clear()
            loStrShapefile = "Prioritarios" & v_codigo
            cls_Catastro.Add_ShapeFile1("Prioritarios" & v_codigo, pApp, "Prioritarios")
        Else
            'Si hay para calcular area disponible
            MsgBox("El DM evaluado no presenta derechos prioritarios considerados para el cálculo de Area Disponible...", vbCritical, "OBSERVACION...")
            v_cantiprioritarios = 0
            Exit Sub
        End If
        loStrShapefile = "DM_" & v_codigo
        cls_Catastro.Add_ShapeFile1(loStrShapefile, m_application, "codigo")
        pMxDoc.ActiveView.Refresh()
    End Sub
    Public Sub Geoprocesamiento_temas(ByVal tema_procesing As String, ByVal p_App As IApplication, ByVal tipo_capa As String)
        'Programa para interceptar temas - DM evaluado VS DM prioritarios
        '*******************************************************************
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
            MsgBox("No se encuentra la capa de Derechos Mineros para realizar el calculo de área disponible", MsgBoxStyle.Information, "Observación...")
            Exit Sub
        End If
        tema1 = pFeatLayer
        tema1_fclass = tema1.FeatureClass
        If tipo_capa = "RESERVA" Then
            pMap.DeleteLayer(tema1)
        End If
        'Define segundo tema
        '*********************
        pMap = pMxDoc.FocusMap
        afound = False
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
        Else
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Prioritarios" & v_codigo Then
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
        If tipo_capa = "RESERVA" Then
            pMap.DeleteLayer(tema2)
        End If
        'Define tema de salida
        pFCName = New FeatureClassName
        pDatasetName = pFCName

        pNewWSName = New WorkspaceName
        pNewWSName.WorkspaceFactoryProgID = "esriCore.ShapeFileWorkspaceFactory"
        pNewWSName.PathName = glo_pathTMP
        pDatasetName.WorkspaceName = pNewWSName
        'Dim pfeeatureclass As IFeatureClass
        Dim pfeature As IFeature
        Dim pfeaturecursor As IFeatureCursor

        If tipo_capa = "RESERVA" Then
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
            tol = 1
            If tema_procesing = "interceccion" Then
                'tol = 0.0001
                pOutputFC = pBGP.Intersect(tema1_fclass, False, tema2_fclass, False, tol, pFCName)
            ElseIf tema_procesing = "union" Then
                'tol = 0.01
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
    End Sub
    Public Function GeneraAreaDisponible_DM(ByVal p_App As IApplication) As String


        If tipo_seleccion = "OP_11" Or tipo_seleccion = "OP_12" Then

            v_cantiprioritarios = 0
            Dim cls_eval As New Cls_evaluacion
            Dim cls_planos As New Cls_planos
            colecciones_planos.Clear()
            cls_planos.mueveposiciondataframe("CATASTRO MINERO", p_App)
            cls_Catastro.Quitar_Layer("DM_" & v_codigo, pApp)
            cls_Catastro.Quitar_Layer("Prioritarios" & v_codigo, pApp)
            cls_Catastro.Quitar_Layer("Areadispo", pApp)
            cls_Catastro.Quitar_Layer("Areadispo_" & v_codigo, pApp)
            cls_Catastro.Quitar_Layer("Areainter_" & v_codigo, pApp)
            pMxDoc.ActivatedView.Refresh()
            Try
                Kill(glo_pathTMP & "\Prioritarios*.*")
            Catch ex As Exception
            End Try
            Try
                Kill(glo_pathTMP & "\Areadispo*.*")
            Catch ex As Exception
            End Try
            Try
                Kill(glo_pathTMP & "\Areainter_*.*")
            Catch ex As Exception
            End Try
            cls_Catastro.Zoom_to_Layer("Catastro")
            cls_Catastro.creandotabla_Areasup()
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
            cls_eval.valida_dm_prioritarios(pApp)  ' oojo sale error en este procemiento verificar
            pMxDoc.ActiveView.Refresh()
            If v_cantiprioritarios > 0 Then
                fecha_archi_sup = DateTime.Now.Ticks.ToString
                cls_eval.Geoprocesamiento_temas("interceccion", pApp, "DM")
                pMxDoc.ActiveView.Refresh()
                cls_eval.calculaareapoligonos("Areainter_" & v_codigo, "")
                arch_cata = "union"
                cls_eval.Geoprocesamiento_temas("union", pApp, "DM")
                cls_eval.calculaareapoligonos("Areadispo_" & v_codigo, "")

                cls_Catastro.Quitar_Layer("DM_" & v_codigo, pApp)
                cls_Catastro.Quitar_Layer("Prioritarios" & v_codigo, pApp)
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
                        Data_Datatable(dr, lointPR + 1, pFeature.Value(pFields.FindField("EVAL")), pFeature.Value(pFields.FindField("ESTADO")), pFeature.Value(pFields.FindField("CONCESION")), pFeature.Value(pFields.FindField("CODIGOU")))
                        lodtPrioritario.Rows.Add(dr)
                        lointPR += 1
                    Case "PO"
                        'DERECHOS MINEROS POSTERIORES
                        dr = lodtPosterior.NewRow
                        Data_Datatable(dr, lointPO + 1, pFeature.Value(pFields.FindField("EVAL")), pFeature.Value(pFields.FindField("ESTADO")), pFeature.Value(pFields.FindField("CONCESION")), pFeature.Value(pFields.FindField("CODIGOU")))
                        lodtPosterior.Rows.Add(dr)
                        lointPO += 1
                    Case "SI"
                        ' DERECHOS MINEROS SIMULTANEOS
                        dr = lodtSimultaneo.NewRow
                        Data_Datatable(dr, lointSI + 1, pFeature.Value(pFields.FindField("EVAL")), pFeature.Value(pFields.FindField("ESTADO")), pFeature.Value(pFields.FindField("CONCESION")), pFeature.Value(pFields.FindField("CODIGOU")))
                        lodtSimultaneo.Rows.Add(dr)
                        lointSI += 1
                    Case "EX"
                        'DERECHOS MINEROS EXTINGUIDOS
                        dr = lodtExtinguido.NewRow
                        Data_Datatable(dr, lointEX + 1, pFeature.Value(pFields.FindField("EVAL")), pFeature.Value(pFields.FindField("ESTADO")), pFeature.Value(pFields.FindField("CONCESION")), pFeature.Value(pFields.FindField("CODIGOU")))
                        lodtExtinguido.Rows.Add(dr)
                        lointEX += 1
                    Case "AR"
                        'DERECHOS MINEROS ANTECESOR
                        dr = lodtRedenuncio.NewRow
                        Data_Datatable(dr, lointEX + 1, pFeature.Value(pFields.FindField("EVAL")), pFeature.Value(pFields.FindField("ESTADO")), pFeature.Value(pFields.FindField("CONCESION")), pFeature.Value(pFields.FindField("CODIGOU")))
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
        Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
        pSpatialReferenceEnv = New SpatialReferenceEnvironment
        Dim PSAD17, PSAD18, PSAD19 As ISpatialReference
        PSAD17 = pSpatialReferenceEnv.CreateProjectedCoordinateSystem(24877)
        PSAD18 = pSpatialReferenceEnv.CreateProjectedCoordinateSystem(24878)
        PSAD19 = pSpatialReferenceEnv.CreateProjectedCoordinateSystem(24879)
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
                If pMap.Layer(A).Name = "Catastro" Then
                    pFeatLayer1 = pMxDoc.FocusMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("Layer No Existe.")
                Exit Sub
            End If

            'afound = False
            'For A As Integer = 0 To pMap.LayerCount - 1
            '    If pMap.Layer(A).Name = "Catastro" Then
            '        pFeatLayer1 = pMxDoc.FocusMap.Layer(A)
            '        afound = True
            '        Exit For
            '    End If
            'Next A
            'If Not afound Then
            '    MsgBox("Layer No Existe.")
            '    Exit Sub
            'End If


            If pFeatLayer1.Name = "Catastro" Then
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
                pfeature = peaturecursor.NextFeature
            Loop

            'pfeature = fclas_tema.GetFeature(2)
            pfeature = fclas_tema.GetFeature(valor_ob)
            fclas_tema1 = pFeatLayer1.FeatureClass
            pFeatureCursor1 = fclas_tema1.Search(Nothing, False)
            pfeature1 = pFeatureCursor1.NextFeature
            pgeometria1 = pfeature1.Shape
            Select Case v_zona_dm
                Case 17
                    pfeature.Shape.SpatialReference = Datum_PSAD_18
                    pfeature.Shape.Project(Datum_PSAD_17)
                Case 18
                    pfeature.Shape.SpatialReference = Datum_PSAD_18
                Case 19
                    pfeature.Shape.SpatialReference = Datum_PSAD_18
                    pfeature.Shape.Project(Datum_PSAD_19)
            End Select
            pgeometria = pfeature.Shape
            Dim pProximity As IProximityOperator
            pProximity = pgeometria
            Dim distancia_fron1 As Double
            distancia_fron1 = (pProximity.ReturnDistance(pgeometria1) / 1000)
            distancia_fron = Math.Round(distancia_fron1, 3)
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
            If pMap.Layer(A).Name = "DM_" & v_codigo Then
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
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "1", False)
            tema = pMap.Layer(0)
            playerdefinition = tema
            playerdefinition.DefinitionExpression = "CD_DEPA <> '99'"
            tema.Visible = True
            pMxDoc.UpdateContents()
            cls_catastro.Shade_Poligono("Departamento", pApp)
            tema.Name = "Peru"
            pMxDoc.UpdateContents()
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "1", False)
            cls_eval.DefinitionExpressiontema(lista_nm_depa, p_app, "Departamento")
            tema = pMap.Layer(0)
            tema.Visible = True
            tema.Name = "Depa"
            arch_cata = "Hoja"
            cls_catastro.Color_Poligono_Simple(m_application, "Depa")
            cls_catastro.Zoom_to_Layer("Depa")
        ElseIf nombre_dataframe = "MAPA DE EMPALME" Then
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Carta, m_application, "1", False)
            tema = pMap.Layer(0)
            playerdefinition = tema
            playerdefinition.DefinitionExpression = "ESTADO <> 'Sin dato'"
            tema.Name = "Hojas IGN"
            tema.Visible = True
            cls_catastro.Shade_Poligono("Hojas IGN", pApp)

            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "1", False)
            cls_catastro.Shade_Poligono("Departamento", pApp)
            tema = pMap.Layer(0)
            playerdefinition = tema
            playerdefinition.DefinitionExpression = "CD_DEPA <> '99'"
            tema.Name = "Peru"

            tema.Visible = True
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Carta, m_application, "1", False)
            tema = pMap.Layer(0)
            'tema.Name = "Hoja"
            playerdefinition = tema
            playerdefinition.DefinitionExpression = lista_cartas
            tema = pMap.Layer(0)
            tema.Visible = True
            pMxDoc.UpdateContents()
            arch_cata = "Hoja"
            cls_catastro.Color_Poligono_Simple(m_application, "Cuadrangulo")
            cls_catastro.Zoom_to_Layer("Cuadrangulo")
            pMxDoc.UpdateContents()
            arch_cata = ""
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
        Dim cls_eval As New Cls_evaluacion
        caso_consulta = "CATASTRO MINERO"
        cls_eval.ActivaDataframe_Opcion(caso_consulta, m_application)
        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Cuadricula, m_application, "1", True)
        cls_Catastro.DefinitionExpression(lista_nm_depa, m_application, "Cuadricula Regional")
        cls_Catastro.Shade_Poligono("Cuadricula Regional", pApp)
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
        pGxLayer = New GxLayer
        pGxFile = pGxLayer
        strLayerPath = glo_pathStyle_paises & "PAISES.lyr"

        pGxFile.Path = strLayerPath
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
                Kill(glo_pathTMP & "\Prioritarios*.*")
            Catch ex As Exception
            End Try

            cls_Catastro.Exportando_Temas("", "Catastro", pApp)
            pFeatureSelection.Clear()
            loStrShapefile = "Prioritarios" & v_codigo
            cls_Catastro.Add_ShapeFile1("Prioritarios" & v_codigo, pApp, "Prioritarios")
        Else
            'Si hay para calcular area disponible
            'MsgBox("El DM evaluado no presenta derechos prioritarios considerados para el cálculo de Area Disponible...", vbCritical, "OBSERVACION...")
            v_cantiprioritarios = 0
            Exit Sub
        End If
        loStrShapefile = "DM_" & v_codigo
        cls_Catastro.Add_ShapeFile1(loStrShapefile, m_application, "codigo")
        pMxDoc.ActiveView.Refresh()
    End Sub
    Public Sub AddLayerFromFile1(ByVal pApp As IApplication, ByVal nlayer As String)
        Dim pGxLayer As IGxLayer
        Dim pGxFile As IGxFile
        Dim strLayerPath As String
        pGxLayer = New GxLayer
        pGxFile = pGxLayer
        'strLayerPath = "U:\Geocatmin\Estilos\Departamentos.lyr"
        strLayerPath = "U:\Geocatmin\Estilos\" & nlayer & ".lyr"
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


End Class
