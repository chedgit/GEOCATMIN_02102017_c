'Imports System.Drawing
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
Imports PORTAL_Clases
Imports System.Xml
Imports System.Text
Imports stdole
Imports System.IO

Public Class cls_DM_1
    Structure Punto_DM
        Dim v As Integer
        Dim x As Double
        Dim y As Double
    End Structure

    'Public pApp As IApplication   'OJO DESCOMENTE
    Public m_application As IApplication
    Public cls_Oracle As New cls_Oracle
    Private cls_catastro As cls_DM_1
    'Public cls_eval As New Cls_evaluacion
    Public Sub Conexion_GeoDatabase()
        pWorkspaceFactory = New AccessWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_PathGDB, 0)
    End Sub
    Public Sub Conexion_Shapefile()
        pWorkspaceFactory = New ShapefileWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_pathTMP, 0)
    End Sub

    Public Sub Consulta_atributos(ByVal p_App As IApplication, ByVal p_ListBox As Object)
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim pFeatureClass As IFeatureClass
        Dim pMxDoc As IMxDocument
        pMxDoc = p_App.Document
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
        pFeatureClass = pFeatLayer.FeatureClass
        Dim pQFilter As IQueryFilter
        Dim pUpdateFeatures As IFeatureCursor
        Dim codigo As String
        pQFilter = New QueryFilter
        pQFilter.WhereClause = query_cadena
        pUpdateFeatures = pFeatureClass.Update(pQFilter, True)
        Dim pFeature As IFeature
        Dim codigo_p As String
        Dim v_prueba As String
        Dim priori_p As String
        pFeature = pUpdateFeatures.NextFeature
        Try
            Dim CONTA As Long
            CONTA = pFeatureClass.FeatureCount(pQFilter)
            Do Until pFeature Is Nothing
                codigo = pFeature.Value(pUpdateFeatures.FindField("CODIGOU"))
                For contador As Integer = 1 To colecciones_indi.Count
                    v_prueba = colecciones_indi.Item(contador)
                    codigo_p = Left(v_prueba, Len(v_prueba) - 2)
                    If codigo = codigo_p Then
                        priori_p = Right(v_prueba, 2)
                        pFeature.Value(pUpdateFeatures.FindField("EVAL")) = priori_p.ToUpper
                        pUpdateFeatures.UpdateFeature(pFeature)
                    End If
                Next contador
                pFeature = pUpdateFeatures.NextFeature
            Loop
            pMxDoc.ActiveView.Refresh()
            MsgBox("Se actualizo el criterio de evaluación en la parte grafica", MsgBoxStyle.Information, "Observación")
        Catch ex As Exception
            MsgBox("Error al Actualizar criterio evaluación", MsgBoxStyle.Information, "Observación")
        End Try
    End Sub

    Public Sub DefinitionExpression_areasup(ByVal lo_Filtro As String, ByRef m_application As IApplication, ByVal Nom_Shapefile As String)
        If lo_Filtro = "" Then Exit Sub
        Dim v_Tema As String

        v_Tema = "Areainter_" & v_codigo
        Nom_Shapefile = v_Tema
        Dim pActiveView As IActiveView
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim pFeatureLayerD As IFeatureLayerDefinition
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        Dim afound As Boolean = False
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
        pFeatureLayerD = pFeatLayer
        'pFeatureLayerD.DefinitionExpression = lo_Filtro
        pMxDoc.UpdateContents()
        pActiveView.Refresh()
        Dim pFeatureSelection As IFeatureSelection
        'If Not pFeatureSelection Is Nothing Then
        Dim pQueryFilter As IQueryFilter
        pFeatureSelection = pFeatLayer
        ' Prepare a query filter.
        pQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = lo_Filtro
        ' Refresh or erase any previous selection.
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

        Dim pCmdItem As ICommandItem
        Dim pUID As New UID
        pUID.Value = "esriArcMapUI.ZoomToSelectedCommand"
        pCmdItem = m_application.Document.CommandBars.Find(pUID)
        pCmdItem.Execute()
        Dim pFeatureLayer_1 As IFeatureLayer
        pFeatureLayer_1 = pFeatureLayerD.CreateSelectionLayer(v_Tema & "_sup", True, vbNullString, vbNullString)
        'pFeatureSelection.Clear()
        pMap.AddLayer(pFeatureLayer_1)
        pFeatureLayer_1.Name = "Area_sup"
        For A As Integer = 0 To pMap.LayerCount - 1
            pFeatLayer = pMxDoc.FocusMap.Layer(A)
            If pMap.Layer(A).Name = "Area_sup" Then
            Else
                pFeatLayer.Visible = False
            End If
        Next A
        pMxDoc.ActiveView.Refresh()

    End Sub

    Public Sub Seleccionar_Items_x_Codigo_areasup(ByVal p_Filtro As String, ByVal p_App As IApplication, ByVal p_Listbox As Object, ByVal tipo_capa As String)
        Dim pQueryFilter As IQueryFilter
        Dim cls_catastro As New cls_DM_1
        Dim fclas_tema As IFeatureClass
        loStrShapefile1 = ""
        'v_hecta_reg = 0
        Dim pActiveView As IActiveView
        Dim cls_eval As New Cls_evaluacion
        ' pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        Dim afound As Boolean = False

        Dim pFeatureLayer As IFeatureLayer = Nothing
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        If tipo_capa = "AreaRese_super" Then
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "AreaRese_super" Then
                    pFeatureLayer = pMxDoc.FocusMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
        ElseIf tipo_capa = "Areainter_" & v_codigo Then
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Areainter_" & v_codigo Then
                    pFeatureLayer = pMxDoc.FocusMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
        End If


        If Not afound Then
            MsgBox("Layer No Existe.")
            Exit Sub
        End If
        Dim pFeatureSelection As IFeatureSelection
        pFeatureSelection = pFeatureLayer
        pQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = p_Filtro
        p_Filtro1 = p_Filtro
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        Dim pFSel As IFeatureSelection
        pFSel = pFeatureLayer
        Dim pFCursor As IFeatureCursor
        fclas_tema = pFeatureLayer.FeatureClass
        pFCursor = pFeatureLayer.Search(pQueryFilter, True)
        pFeature = pFCursor.NextFeature
        pFields = fclas_tema.Fields
        cls_catastro.Listar_Vertice_Area(pFeature.Shape, p_Listbox)
        Dim numpol As String = ""
        Dim valor_numpol As Integer
        p_Listbox.ITEMS.CLEAR()
        Dim coordenada_DM(300) As Punto_DM
        Dim lodtbDatos As New DataTable
        If pFeatureLayer.Name = "Catastro" Then
            lodtbDatos = cls_Oracle.F_Obtiene_Datos_DM(v_codigo)
            Select Case lodtbDatos.Rows.Count
                Case 0
                    MsgBox("No Existe Codigo: " & v_codigo & " en la Base de Datos....")
                Case Else
                    numpol = lodtbDatos.Rows(0).Item("CD_NUMPOL").ToString
                    If numpol = "" Then
                        valor_numpol = 0
                    Else
                        valor_numpol = 1
                    End If
            End Select
        Else   'ESTO DIFERENTE A CAPA CATASTRO
            valor_numpol = 0
        End If
        If valor_numpol = 0 Then
            Dim h, j As Integer
            Dim ptcol As IPointCollection

            Dim pt As IPoint
            Dim l As IPolygon
            If pFeatureLayer.Name = "Areainter_" & v_codigo Then
                p_Listbox.Items.Add("Código = " & pFeature.Value(pFeature.Fields.FindField("CODIGOU_1"))) ' -- & Space(5) & "Nombre = " & pFeature.Value(pFeature.Fields.FindField("CONCESION")))
                p_Listbox.Items.Add("Nombre = " & pFeature.Value(pFeature.Fields.FindField("CONCESIO_1")))
            ElseIf pFeatureLayer.Name = "Areadispo_" & v_codigo Then
                p_Listbox.Items.Add("Código = " & pFeature.Value(pFeature.Fields.FindField("CODIGOU"))) ' -- & Space(5) & "Nombre = " & pFeature.Value(pFeature.Fields.FindField("CONCESION")))
            ElseIf pFeatureLayer.Name = "AreaRese_super" Then
                p_Listbox.Items.Add("Código = " & pFeature.Value(pFeature.Fields.FindField("CODIGO"))) ' -- & Space(5) & "Nombre = " & pFeature.Value(pFeature.Fields.FindField("CONCESION")))
            
            Else
                p_Listbox.Items.Add("Código = " & pFeature.Value(pFeature.Fields.FindField("CODIGOU"))) ' -- & Space(5) & "Nombre = " & pFeature.Value(pFeature.Fields.FindField("CONCESION")))
                p_Listbox.Items.Add("Nombre = " & pFeature.Value(pFeature.Fields.FindField("CONCESION")))
            End If

            p_Listbox.Items.Add("----------------------------------------------------------------------------------")
            p_Listbox.Items.Add(Space(3) & " Vert." & Space(10) & "Norte" & Space(10) & "Este")
            p_Listbox.Items.Add("----------------------------------------------------------------------------------")
            l = pFeature.Shape
            ptcol = l
            ReDim coordenada_DM(ptcol.PointCount)
            For j = 0 To ptcol.PointCount - 2
                pt = ptcol.Point(j)
                p_Listbox.Items.Add(Space(5) & RellenarComodin(j + 1, 3, "0") & Space(5) & Format(Math.Round(pt.Y, 3), "###,###.00") & Space(5) & Format(Math.Round(pt.X, 3), "###,###.00") & "")
                coordenada_DM(j).v = j + 1
                coordenada_DM(j).x = Format(Math.Round(pt.X, 3), "###,###.00") ' pt.X
                coordenada_DM(j).y = Format(Math.Round(pt.Y, 3), "###,###.00") 'pt.Y
            Next j
            'Calcular Area
            coordenada_DM(j).x = coordenada_DM(0).x
            coordenada_DM(j).y = coordenada_DM(0).y
            Dim d0, d1, dr As Double
            d0 = 0 : d1 = 0 : dr = 0
            For h = 0 To j - 1
                If coordenada_DM(h).x <> 0 Or coordenada_DM(h).y <> 0 Then
                    d0 = d0 + coordenada_DM(h).x * coordenada_DM(h + 1).y
                    d1 = d1 + coordenada_DM(h).y * coordenada_DM(h + 1).x
                Else
                    Exit For
                End If
            Next h
            dr = Math.Round(Math.Abs((d0 - d1) / 2) / 10000, 4)  'termino
            p_Listbox.Items.Add("----------------------------------------------------------------------------------")
            p_Listbox.Items.Add(Space(10) & "Area UTM = " & Format(Math.Round(dr, 4), "###,###.0000") & "  (Ha)")
            p_Listbox.Items.Add("----------------------------------------------------------------------------------")
        Else
            Dim v_area As Double

            v_area = pFeature.Value(pFeature.Fields.FindField("HECTAGIS"))
            p_Listbox.Items.Add("Código = " & lodtbDatos.Rows(0).Item("CG_CODIGO").ToString) ' -- & Space(5) & "Nombre = " & pFeature.Value(pFeature.Fields.FindField("CONCESION")))
            p_Listbox.Items.Add("Nombre = " & lodtbDatos.Rows(0).Item("PE_NOMDER").ToString)
            p_Listbox.Items.Add("----------------------------------------------------------------------------------")
            p_Listbox.Items.Add(Space(3) & " Vert." & Space(10) & "Norte" & Space(10) & "Este")
            p_Listbox.Items.Add("----------------------------------------------------------------------------------")
            For i As Integer = 0 To lodtbDatos.Rows.Count - 1
                p_Listbox.Items.Add(Space(5) & RellenarComodin(i + 1, 3, "0") & "   " & _
            Format(Math.Round(lodtbDatos.Rows(i).Item("CD_COREST"), 3), "###,###.#0") & _
             "   " & Format(Math.Round(lodtbDatos.Rows(i).Item("CD_CORNOR"), 3), "###,###.#0") & "")
            Next
            p_Listbox.Items.Add("----------------------------------------------------------------------------------")
            p_Listbox.Items.Add(Space(10) & "Area UTM = " & Format(Math.Round(v_area, 4), "###,###.0000") & "  (Ha)")
            p_Listbox.Items.Add("----------------------------------------------------------------------------------")
        End If
    End Sub

    Public Sub Seleccionar_Items_x_Codigo_dist(ByVal p_Filtro As String, ByVal p_App As IApplication, ByVal tipo_capa As String)
        Dim pQueryFilter As IQueryFilter
        Dim pActiveView As IActiveView

        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        Dim afound As Boolean = False

        Dim pFeatureLayer As IFeatureLayer
        pMap = pMxDoc.FocusMap
        pActiveView = pMap

        If tipo_capa = "Distrito" Then
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Distrito" Then
                    pFeatureLayer = pMxDoc.FocusMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
        End If


        If Not afound Then
            MsgBox("Layer No Existe.")
            Exit Sub
        End If

        Dim pFeatureSelection1 As IFeatureSelection

        pFeatureSelection1 = pFeatureLayer
        pQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = p_Filtro
        p_Filtro1 = p_Filtro
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFeatureSelection1.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        
    End Sub


    Public Sub Conexion_SDE(ByVal p_App As IApplication)
        Try
            Try
                pMap = pMxDoc.FocusMap
            Catch ex As Exception
                pMxDoc = p_App.Document
                pMap = pMxDoc.FocusMap
            End Try
            'Dim pPropset As IPropertySet
            pPropset = New PropertySet
            With pPropset
                .SetProperty("SERVER", glo_Server_SDE)
                .SetProperty("INSTANCE", glo_Instance_SDE)
                '.SetProperty "Database", "sde" ' Ignored with ArcSDE for Oracle 
                .SetProperty("USER", glo_User_SDE)
                .SetProperty("PASSWORD", glo_Password_SDE)
                .SetProperty("VERSION", glo_Version_SDE)
            End With
            pWorkspaceFactory = New SdeWorkspaceFactory
            pFeatureWorkspace = pWorkspaceFactory.Open(pPropset, 0) ' p_App.hWnd) '0)
            glo_Inicio_SDE = True
        Catch ex As Exception
            glo_Inicio_SDE = False
        End Try

    End Sub
    Public Sub Conexion_SDE_Catastro(ByVal p_App As IApplication)
        Try
            Try
                pMap = pMxDoc.FocusMap
            Catch ex As Exception
                pMxDoc = p_App.Document
                pMap = pMxDoc.FocusMap
            End Try
            Dim pPropset As IPropertySet
            pPropset = New PropertySet
            With pPropset
                .SetProperty("SERVER", glo_Server_SDE)
                .SetProperty("INSTANCE", glo_Instance_SDE_1)
                '.SetProperty "Database", "sde" ' Ignored with ArcSDE for Oracle 
                .SetProperty("USER", glo_User_SDE)
                .SetProperty("PASSWORD", glo_Password_SDE)
                .SetProperty("VERSION", glo_Version_SDE)
            End With
            pWorkspaceFactory = New SdeWorkspaceFactory
            pFeatureWorkspace = pWorkspaceFactory.Open(pPropset, 0) ' p_App.hWnd) '0)
            glo_Inicio_SDE = True
        Catch ex As Exception
            glo_Inicio_SDE = False
        End Try
    End Sub

    Public Sub Leer_Dbf(ByVal p_Codigo As String, ByVal p_Existe As Object)
        Dim pRow As IRow
        'Dim pTable As ITable
        'Dim pWorkspaceFactory As IWorkspaceFactory
        pWorkspaceFactory = New ShapefileWorkspaceFactory
        'Dim pFWS As IFeatureWorkspace
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_Path & "DBF\", 0)
        pTable = pFeatureWorkspace.OpenTable("Reporte")
        Dim ptableCursor As ICursor
        Dim pfields As Fields
        pfields = pTable.Fields
        Dim pQueryFilter As IQueryFilter
        'pfields3 = pTable.Fields
        pQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = "CODIGO = '" & p_Codigo & "'"
        ptableCursor = pTable.Search(pQueryFilter, True)
        pRow = ptableCursor.NextRow
        Dim lostrAccion As String
        Dim lostrFecha As String
        Do Until pRow Is Nothing
            lostrAccion = pRow.Value(pfields.FindField("ACCION")) ' - -vvvvv
            lostrFecha = pRow.Value(pfields.FindField("FECHA"))
            pRow = ptableCursor.NextRow
            p_Existe.text = "Si"
        Loop
    End Sub

    Public Sub Leer_datos_libredenu()
        Dim pRow As IRow
        'Dim pTable As ITable
        'Dim pWorkspaceFactory As IWorkspaceFactory
        pWorkspaceFactory = New ShapefileWorkspaceFactory
        'Dim pFWS As IFeatureWorkspace
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_Path, 0)
        pTable = pFeatureWorkspace.OpenTable("listadodm")
        Dim ptableCursor As ICursor
        Dim pfields As Fields
        pfields = pTable.Fields
        ' Dim pQueryFilter As IQueryFilter
        'pfields3 = pTable.Fields
        'pQueryFilter = New QueryFilter
        'pQueryFilter.WhereClause = "CODIGO = '" & p_Codigo & "'"
        ptableCursor = pTable.Search(Nothing, False)
        pRow = ptableCursor.NextRow
        Dim v_codigop As String
        Dim cuenta As Integer = 0
        ' Dim lista_codigo As String

        Do Until pRow Is Nothing
            cuenta = cuenta + 1
            v_codigop = pRow.Value(pfields.FindField("CODIGO"))
            If cuenta = 1 Then

                lista_codigo = "CODIGOU =  '" & v_codigop & "'"
            ElseIf cuenta > 1 Then
                lista_codigo = lista_codigo & " OR " & "CODIGOU =  '" & v_codigop & "'"

            End If
            pRow = ptableCursor.NextRow
        Loop
        ' MsgBox(lista_codigo)
    End Sub


    Public Sub Seleccionar_Items_x_Codigo(ByVal p_Filtro As String, ByVal p_App As IApplication, ByVal p_Listbox As Object)
        Dim pQueryFilter As IQueryFilter = Nothing
        Dim cls_catastro As New cls_DM_1
        Dim fclas_tema As IFeatureClass
        loStrShapefile1 = ""
        Dim codigo_depa1 As String = ""
        Dim codigo_depa As String = ""
        Dim v_porcen_x As Double
        v_hecta_reg = 0
        Dim pActiveView As IActiveView
        Dim cls_eval As New Cls_evaluacion
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        Dim afound As Boolean = False
        Dim pFeatureSelection As IFeatureSelection = pFeatureLayer
        pQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = p_Filtro
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        Dim pFSel As IFeatureSelection
        pFSel = pFeatureLayer
        Dim pFCursor As IFeatureCursor
        fclas_tema = pFeatureLayer.FeatureClass
        pFCursor = pFeatureLayer.Search(pQueryFilter, True)
        pFeature = pFCursor.NextFeature
        If pFSel.SelectionSet.Count = 0 Then
            MsgBox("No hay ninguna Selección")
            Exit Sub
        End If
        Dim val_codigo As String
        pFields = fclas_tema.Fields
        If pFeatureLayer.Name = "Catastro" Then
            val_codigo = pFeature.Value(pFields.FindField("CODIGOU"))
            consulta_dms = "CODIGOU =  '" & val_codigo & "'"
        End If
        If caso_opcion_tools = "Por Region" Then
            arch_cata = "DMxregion"
            cls_eval.DefinitionExpressiontema(consulta_dms, p_App, "DMxregion")
            cls_catastro.Add_ShapeFile1(loStrShapefile1, p_App, "DMxregion")
            cls_eval.obtienelimitesmaximos("DMxregion")
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, p_App, "1", False)
            Dim lo_Filtro_Dpto As String = cls_catastro.f_Intercepta_FC("Departamento", v_este_min, v_norte_min, v_este_max, v_norte_max, p_App)
            cls_catastro.DefinitionExpression(lo_Filtro_Dpto, p_App, "Departamento")
            cls_eval.intersectadepa()
            pMxDoc.UpdateContents()
            Dim ptema As IFeatureLayer
            ptema = pMap.Layer(0)
            fclas_tema = ptema.FeatureClass
            Dim pFeatureCursor As IFeatureCursor
            Dim pFeature As IFeature
            pFields = fclas_tema.Fields
            pFeatureCursor = fclas_tema.Search(Nothing, False)
            pFeature = pFeatureCursor.NextFeature
            v_hecta_reg = 0
            'Dim cuenta_r As Long = 0
            Dim conta As Long
            Dim lodtRegistro As New DataTable
            conta = fclas_tema.FeatureCount(Nothing)
            lodtRegistro.Columns.Add("DPTO", Type.GetType("System.String"))
            lodtRegistro.Columns.Add("HECTA", Type.GetType("System.Double"))
            lodtRegistro.Columns.Add("PORCE", Type.GetType("System.Double"))
            Do Until pFeature Is Nothing
                'cuenta_r = cuenta_r + 1
                Select Case pFeature.Value(pFields.FindField("HECTAGIS"))
                    Case 0
                        v_porcen_x = 0
                    Case Else
                        v_porcen_x = FormatNumber((pFeature.Value(pFields.FindField("HECTAGIS")) / v_hectagis_x) * 100, 2)
                End Select
                Dim dRow As DataRow
                dRow = lodtRegistro.NewRow
                dRow.Item("DPTO") = pFeature.Value(pFields.FindField("NM_DEPA"))
                dRow.Item("HECTA") = pFeature.Value(pFields.FindField("HECTAGIS"))
                dRow.Item("PORCE") = Format(Math.Round(v_porcen_x, 2), "###.00") 'v_porcen_x
                lodtRegistro.Rows.Add(dRow)
                pFeature = pFeatureCursor.NextFeature
            Loop
            Dim lodtvOrdena As New DataView(lodtRegistro, Nothing, "DPTO ASC", DataViewRowState.CurrentRows)
            Dim lo_Dpto As String = ""
            Dim lostrDpto As String = lodtvOrdena.Item(0).Row(0)
            Dim dlo_Ha As Double = 0
            Dim dlo_Pe As Double = 0
            lodtTotales = New DataTable
            lodtTotales.Columns.Add("DPTO", Type.GetType("System.String"))
            lodtTotales.Columns.Add("HECTA", Type.GetType("System.String"))
            lodtTotales.Columns.Add("PORCE", Type.GetType("System.String"))
            Dim dRow_1 As DataRow
            For i As Integer = 0 To lodtvOrdena.Count - 1
                If lodtvOrdena.Item(i).Row(0) = lostrDpto Then
                    lo_Dpto = lostrDpto
                    dlo_Ha = dlo_Ha + lodtvOrdena.Item(i).Row(1)
                    dlo_Pe = dlo_Pe + lodtvOrdena.Item(i).Row(2)
                    If i = lodtvOrdena.Count - 1 Then
                        dRow_1 = lodtTotales.NewRow
                        dRow_1.Item("DPTO") = lo_Dpto
                        dRow_1.Item("HECTA") = Format(Math.Round(dlo_Ha, 2), "###.00") 'dlo_Ha
                        dRow_1.Item("PORCE") = Format(Math.Round(dlo_Pe, 2), "###.00") 'dlo_Pe
                        lodtTotales.Rows.Add(dRow_1)
                        '******
                        dRow_1 = lodtTotales.NewRow
                        dRow_1.Item("DPTO") = ""
                        dRow_1.Item("HECTA") = "====="
                        dRow_1.Item("PORCE") = "====="
                        lodtTotales.Rows.Add(dRow_1)
                        dRow_1 = lodtTotales.NewRow
                        dRow_1.Item("DPTO") = "TOTAL:"
                        dRow_1.Item("HECTA") = v_hectagis_x
                        dRow_1.Item("PORCE") = Format(Math.Round(100, 2), "###.00") 'dlo_Pe
                        lodtTotales.Rows.Add(dRow_1)
                    End If
                Else
                    'Guardar
                    dRow_1 = lodtTotales.NewRow
                    dRow_1.Item("DPTO") = lo_Dpto
                    dRow_1.Item("HECTA") = Format(Math.Round(dlo_Ha, 2), "###.00") 'dlo_Ha
                    dRow_1.Item("PORCE") = Format(Math.Round(dlo_Pe, 2), "###.00") 'dlo_Pe
                    lodtTotales.Rows.Add(dRow_1)
                    lo_Dpto = "" : dlo_Ha = 0 : dlo_Pe = 0
                    lostrDpto = lodtvOrdena.Item(i).Row(0)
                    lo_Dpto = lostrDpto
                    dlo_Ha = dlo_Ha + lodtvOrdena.Item(i).Row(1)
                    dlo_Pe = dlo_Pe + lodtvOrdena.Item(i).Row(2)
                    If i = lodtvOrdena.Count - 1 Then
                        dRow_1 = lodtTotales.NewRow
                        dRow_1.Item("DPTO") = lo_Dpto
                        dRow_1.Item("HECTA") = Format(Math.Round(dlo_Ha, 2), "###.00") 'dlo_Ha
                        dRow_1.Item("PORCE") = Format(Math.Round(dlo_Pe, 2), "###.00") 'dlo_Pe
                        lodtTotales.Rows.Add(dRow_1)
                        '******
                        dRow_1 = lodtTotales.NewRow
                        dRow_1.Item("DPTO") = ""
                        dRow_1.Item("HECTA") = "====="
                        dRow_1.Item("PORCE") = "====="
                        lodtTotales.Rows.Add(dRow_1)
                        dRow_1 = lodtTotales.NewRow
                        dRow_1.Item("DPTO") = "TOTAL:"
                        dRow_1.Item("HECTA") = Format(Math.Round(v_hectagis_x, 2), "###,###.00") 'v_hectagis_x
                        dRow_1.Item("PORCE") = Format(Math.Round(100, 2), "###.00") '100
                        lodtTotales.Rows.Add(dRow_1)
                    End If
                End If
            Next
            ptema = pMap.Layer(0)
            pMap.DeleteLayer(ptema)
            ptema = pMap.Layer(0)
            pMap.DeleteLayer(ptema)
            ptema = pMap.Layer(0)
            pMap.DeleteLayer(ptema)
            pMxDoc.UpdateContents()
        ElseIf caso_opcion_tools = "Por XY" Then
            'Genera previamente listado txt de coordenadas

            Const ruta As String = "C:\listado.txt"
            Dim sw As New System.IO.StreamWriter(ruta)

            cls_catastro.Listar_Vertice_Area(pFeature.Shape, p_Listbox)
            'cls_catastro.rotulatexto_dm_poligono("Catastro", p_App)
            Dim numpol As String = ""
            Dim valor_numpol As Integer
            p_Listbox.ITEMS.CLEAR()
            Dim coordenada_DM(300) As Punto_DM
            Dim lodtbDatos As New DataTable
            If pFeatureLayer.Name = "Catastro" Then
                lodtbDatos = cls_Oracle.F_Obtiene_Datos_DM(val_codigo)
                Select Case lodtbDatos.Rows.Count
                    Case 0
                        MsgBox("No Existe Codigo: " & val_codigo & " en la Base de Datos....")
                    Case Else
                        numpol = lodtbDatos.Rows(0).Item("CD_NUMPOL").ToString
                        If numpol = "" Then
                            valor_numpol = 0
                        Else
                            valor_numpol = 1
                        End If
                End Select
            Else   'ESTO DIFERENTE A CAPA CATASTRO
                valor_numpol = 0
            End If
            If valor_numpol = 0 Then
                Dim h, j As Integer
                Dim ptcol As IPointCollection

                Dim pt As IPoint
                Dim l As IPolygon
                If pFeatureLayer.Name = "Areainter_" & v_codigo Then
                    p_Listbox.Items.Add("Código = " & pFeature.Value(pFeature.Fields.FindField("CODIGOU_1"))) ' -- & Space(5) & "Nombre = " & pFeature.Value(pFeature.Fields.FindField("CONCESION")))
                    p_Listbox.Items.Add("Nombre = " & pFeature.Value(pFeature.Fields.FindField("CONCESIO_1")))
                    sw.WriteLine("Código = " & pFeature.Value(pFeature.Fields.FindField("CODIGOU_1")))
                    sw.WriteLine("Nombre = " & pFeature.Value(pFeature.Fields.FindField("CONCESIO_1")))
                ElseIf pFeatureLayer.Name = "Areadispo_" & v_codigo Then
                    p_Listbox.Items.Add("Código = " & pFeature.Value(pFeature.Fields.FindField("CODIGOU"))) ' -- & Space(5) & "Nombre = " & pFeature.Value(pFeature.Fields.FindField("CONCESION")))
                    sw.WriteLine("Código = " & pFeature.Value(pFeature.Fields.FindField("CODIGOU")))
                ElseIf pFeatureLayer.Name = "Catastro" Then
                    p_Listbox.Items.Add("Código = " & pFeature.Value(pFeature.Fields.FindField("CODIGOU"))) ' -- & Space(5) & "Nombre = " & pFeature.Value(pFeature.Fields.FindField("CONCESION")))
                    p_Listbox.Items.Add("Nombre = " & pFeature.Value(pFeature.Fields.FindField("CONCESION")))
                    sw.WriteLine("Código = " & pFeature.Value(pFeature.Fields.FindField("CODIGOU")))
                    sw.WriteLine("Nombre = " & pFeature.Value(pFeature.Fields.FindField("CONCESION")))
                Else
                    p_Listbox.Items.Add("Código = " & "00000001") ' -- & Space(5) & "Nombre = " & pFeature.Value(pFeature.Fields.FindField("CONCESION")))
                    p_Listbox.Items.Add("Nombre = " & "Simulado") ' -- & Space(5) & "Nombre = " & pFeature.Value(pFeature.Fields.FindField("CONCESION")))
                    sw.WriteLine("Código = " & "00000001")
                    sw.WriteLine("Nombre = " & "Simulado")
                End If

                p_Listbox.Items.Add("----------------------------------------------------------------------------------")
                sw.WriteLine("--------------------------------------------------")
                p_Listbox.Items.Add(Space(3) & " Vert." & Space(10) & "Norte" & Space(10) & "Este")
                sw.WriteLine(Space(3) & " Vert." & Space(10) & "Norte" & Space(10) & "Este")
                p_Listbox.Items.Add("----------------------------------------------------------------------------------")
                sw.WriteLine("--------------------------------------------------")

                l = pFeature.Shape
                ptcol = l
                ReDim coordenada_DM(ptcol.PointCount)
                For j = 0 To ptcol.PointCount - 2
                    pt = ptcol.Point(j)
                    p_Listbox.Items.Add(Space(5) & RellenarComodin(j + 1, 3, "0") & Space(5) & Format(Math.Round(pt.Y, 3), "###,###.00") & Space(5) & Format(Math.Round(pt.X, 3), "###,###.00") & "")
                    coordenada_DM(j).v = j + 1
                    coordenada_DM(j).x = Format(Math.Round(pt.X, 3), "###,###.00") ' pt.X
                    coordenada_DM(j).y = Format(Math.Round(pt.Y, 3), "###,###.00") 'pt.Y
                    sw.WriteLine(Space(5) & RellenarComodin(j + 1, 3, "0") & Space(5) & Format(Math.Round(pt.Y, 3), "###,###.00") & Space(5) & Format(Math.Round(pt.X, 3), "###,###.00") & "")

                Next j
                'Calcular Area
                coordenada_DM(j).x = coordenada_DM(0).x
                coordenada_DM(j).y = coordenada_DM(0).y
                Dim d0, d1, dr As Double
                d0 = 0 : d1 = 0 : dr = 0
                For h = 0 To j - 1
                    If coordenada_DM(h).x <> 0 Or coordenada_DM(h).y <> 0 Then
                        d0 = d0 + coordenada_DM(h).x * coordenada_DM(h + 1).y
                        d1 = d1 + coordenada_DM(h).y * coordenada_DM(h + 1).x
                    Else
                        Exit For
                    End If
                Next h
                dr = Math.Round(Math.Abs((d0 - d1) / 2) / 10000, 4)  'termino
                p_Listbox.Items.Add("----------------------------------------------------------------------------------")
                sw.WriteLine("--------------------------------------------------")
                p_Listbox.Items.Add(Space(10) & "Area UTM = " & Format(Math.Round(dr, 4), "###,###.0000") & "  (Ha)")
                sw.WriteLine(Space(10) & "Area UTM = " & Format(Math.Round(dr, 4), "###,###.0000") & "  (Ha)")
                p_Listbox.Items.Add("----------------------------------------------------------------------------------")
                sw.WriteLine("--------------------------------------------------")


            Else
                Dim v_area As Double

                v_area = pFeature.Value(pFeature.Fields.FindField("HECTAGIS"))
                p_Listbox.Items.Add("Código = " & lodtbDatos.Rows(0).Item("CG_CODIGO").ToString) ' -- & Space(5) & "Nombre = " & pFeature.Value(pFeature.Fields.FindField("CONCESION")))
                sw.WriteLine("Código = " & lodtbDatos.Rows(0).Item("CG_CODIGO").ToString)
                p_Listbox.Items.Add("Nombre = " & lodtbDatos.Rows(0).Item("PE_NOMDER").ToString)
                sw.WriteLine("Nombre = " & lodtbDatos.Rows(0).Item("PE_NOMDER").ToString)
                p_Listbox.Items.Add("----------------------------------------------------------------------------------")
                sw.WriteLine("--------------------------------------------------")
                p_Listbox.Items.Add(Space(3) & " Vert." & Space(10) & "Norte" & Space(10) & "Este")
                sw.WriteLine(Space(3) & " Vert." & Space(10) & "Norte" & Space(10) & "Este")
                p_Listbox.Items.Add("----------------------------------------------------------------------------------")
                sw.WriteLine("--------------------------------------------------")

                For i As Integer = 0 To lodtbDatos.Rows.Count - 1
                    p_Listbox.Items.Add(Space(5) & RellenarComodin(i + 1, 3, "0") & "   " & _
                Format(Math.Round(lodtbDatos.Rows(i).Item("CD_COREST"), 3), "###,###.#0") & _
                 "   " & Format(Math.Round(lodtbDatos.Rows(i).Item("CD_CORNOR"), 3), "###,###.#0") & "")

                    sw.WriteLine(Space(5) & RellenarComodin(i + 1, 3, "0") & "   " & Format(Math.Round(lodtbDatos.Rows(i).Item("CD_COREST"), 3), "###,###.#0") & "   " & Format(Math.Round(lodtbDatos.Rows(i).Item("CD_CORNOR"), 3), "###,###.#0"))
                Next
                sw.WriteLine("--------------------------------------------------")
                p_Listbox.Items.Add("----------------------------------------------------------------------------------")
                p_Listbox.Items.Add(Space(10) & "Area UTM = " & Format(Math.Round(v_area, 4), "###,###.0000") & "  (Ha)")
                sw.WriteLine(Space(10) & "Area UTM = " & Format(Math.Round(v_area, 4), "###,###.0000") & "  (Ha)")
                p_Listbox.Items.Add("----------------------------------------------------------------------------------")
                sw.WriteLine("--------------------------------------------------")

            End If
            sw.Close()

            'Dim p As New Process()
            'p.StartInfo = New ProcessStartInfo("notepad.exe", "C:\LISTADO.txt")
            'p.Start()
          

        End If
    End Sub

    Public Sub Seleccionar_Items_Layer(ByVal p_Layer As String, ByVal p_Filtro As String, ByVal p_App As IApplication)
        Dim pQueryFilter As IQueryFilter = Nothing
        Dim pActiveView As IActiveView
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        Dim afound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = p_Layer Then
                pFeatureLayer = pMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra el Layer")
            Exit Sub
        End If
        Dim pFeatureSelection As IFeatureSelection = pFeatureLayer
        If Not pFeatureSelection Is Nothing Then
            ' Prepare a query filter.
            pQueryFilter = New QueryFilter
            pQueryFilter.WhereClause = "" '"CODIGOU = '010039303'" & " Order By CONCESIO_1"
            ' Refresh or erase any previous selection.

            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        End If
        ' Refresh again to draw the new selection.
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        Dim pFSel As IFeatureSelection
        pFSel = pFeatureLayer
        If pFSel.SelectionSet.Count = 0 Then
            MsgBox("No hay Areas Superpuestas en el DM.")
            Exit Sub
        End If
        Dim pFCursor As IFeatureCursor
        pFCursor = pFeatureLayer.Search(pQueryFilter, True)
        pFeature = pFCursor.NextFeature
        Dim coordenada_DM(300) As Punto_DM
        Dim h, j As Integer
        Dim ptcol As IPointCollection
        Dim pt As IPoint
        Dim l As IPolygon
        Dim lostrCoordenada As String = ""
        Dim lodtTabla As New DataTable
        lodtTabla.Columns.Add("CODIGO", Type.GetType("System.Double"))
        lodtTabla.Columns.Add("CODIGOU", Type.GetType("System.String"))
        lodtTabla.Columns.Add("CONCESION", Type.GetType("System.String"))
        lodtTabla.Columns.Add("VERTICE", Type.GetType("System.Double"))
        lodtTabla.Columns.Add("NORTE", Type.GetType("System.Double"))
        lodtTabla.Columns.Add("ESTE", Type.GetType("System.Double"))
        lodtTabla.Columns.Add("AREA", Type.GetType("System.Double"))
        lodtTabla.Columns.Add("NUM_AREA", Type.GetType("System.Double"))
        Dim dRow As DataRow
        Dim n As Integer
        Dim lo_Fila As Integer = 1
        Dim lo_find As Boolean = True
        Dim lo_valor_Area As Integer = 1
        Do Until pFeature Is Nothing
            l = pFeature.Shape
            ptcol = l
            ReDim coordenada_DM(ptcol.PointCount)
            For j = 0 To ptcol.PointCount - 2
                pt = ptcol.Point(j)
                coordenada_DM(j).v = j + 1
                coordenada_DM(j).x = Format(Math.Round(pt.X, 3), "###,###.00") ' pt.X
                coordenada_DM(j).y = Format(Math.Round(pt.Y, 3), "###,###.00") 'pt.Y
            Next j
            'Calcular Area
            coordenada_DM(j).x = coordenada_DM(0).x : coordenada_DM(j).y = coordenada_DM(0).y
            Dim d0, d1, dr As Double
            d0 = 0 : d1 = 0 : dr = 0
            For h = 0 To j
                If coordenada_DM(h).x <> 0 Or coordenada_DM(h).y <> 0 Then
                    d0 = d0 + coordenada_DM(h).x * coordenada_DM(h + 1).y
                    d1 = d1 + coordenada_DM(h).y * coordenada_DM(h + 1).x
                Else
                    Exit For
                End If
            Next h
            dr = Format(Math.Round(Math.Abs((d0 - d1) / 2) / 10000, 5), "###,###.0000")
            For n = 0 To UBound(coordenada_DM) - 2
                Select Case lo_find
                    Case True
                        dRow = lodtTabla.NewRow
                        dRow.Item("CODIGO") = lo_Fila
                        dRow.Item("CODIGOU") = pFeature.Value(5)
                        dRow.Item("CONCESION") = pFeature.Value(6)
                        dRow.Item("VERTICE") = n + 1
                        dRow.Item("NORTE") = coordenada_DM(n).y
                        dRow.Item("ESTE") = coordenada_DM(n).x
                        dRow.Item("AREA") = dr
                        dRow.Item("NUM_AREA") = lo_valor_Area
                        lodtTabla.Rows.Add(dRow)
                    Case False
                        For a As Integer = 0 To lodtTabla.Rows.Count - 1
                            If lodtTabla.Rows(a).Item("CONCESION") = pFeature.Value(6) Then
                                lo_find = True
                                lo_valor_Area = lodtTabla.Rows(a).Item("NUM_AREA") + 1
                                Exit For
                            Else
                                lo_find = True
                                lo_valor_Area = 1
                            End If
                        Next
                        dRow = lodtTabla.NewRow
                        dRow.Item("CODIGO") = lo_Fila
                        dRow.Item("CODIGOU") = pFeature.Value(5)
                        dRow.Item("CONCESION") = pFeature.Value(6)
                        dRow.Item("VERTICE") = n + 1
                        dRow.Item("NORTE") = coordenada_DM(n).y
                        dRow.Item("ESTE") = coordenada_DM(n).x
                        dRow.Item("AREA") = dr
                        dRow.Item("NUM_AREA") = lo_valor_Area
                        lodtTabla.Rows.Add(dRow)
                End Select
            Next
            lo_find = False
            pFeature = pFCursor.NextFeature
            lo_Fila = lo_Fila + 1
        Loop
        Dim lodtOrdenado As New DataTable
        Dim lodtvUbigeo As New DataView(lodtTabla, Nothing, "CONCESION  ASC", DataViewRowState.CurrentRows)
        For i As Integer = 0 To lodtvUbigeo.Count - 1
            If i = 0 Then
                lodtOrdenado.Columns.Add("CODIGO") : lodtOrdenado.Columns.Add("CODIGOU")
                lodtOrdenado.Columns.Add("CONCESION") : lodtOrdenado.Columns.Add("VERTICE")
                lodtOrdenado.Columns.Add("NORTE") : lodtOrdenado.Columns.Add("ESTE")
                lodtOrdenado.Columns.Add("AREA") : lodtOrdenado.Columns.Add("NUM_AREA")
            End If
            Dim dr As DataRow
            dr = lodtOrdenado.NewRow
            dr.Item(0) = lodtvUbigeo.Item(i).Row(0) : dr.Item(1) = lodtvUbigeo.Item(i).Row(1)
            dr.Item(2) = lodtvUbigeo.Item(i).Row(2) & " (Area-" & lodtvUbigeo.Item(i).Row(7) & ")"
            dr.Item(3) = lodtvUbigeo.Item(i).Row(3)
            dr.Item(4) = lodtvUbigeo.Item(i).Row(4) : dr.Item(5) = lodtvUbigeo.Item(i).Row(5)
            dr.Item(6) = lodtvUbigeo.Item(i).Row(6) : dr.Item(7) = lodtvUbigeo.Item(i).Row(7)
            lodtOrdenado.Rows.Add(dr)
        Next
        If lodtOrdenado.Rows.Count <> 0 Then
            Dim frm_Rpt As New rpt_Reporte_2
            frm_Rpt.C1Report1.DataSource.Recordset = New DataView(lodtOrdenado)
            Dim m_ReportDefinitionFile As String
            m_ReportDefinitionFile = glo_pathREP & "\rpt_Reporte_DM_02.xml"
            frm_Rpt.C1Report1.Load(m_ReportDefinitionFile, "Reporte_Detalle_DM")
            frm_Rpt.C1Report1.DataSource.Recordset = New DataView(lodtOrdenado)
            frm_Rpt.C1Report1.Fields("FECHA_DOCUMENTO").Text = Date.Now
            'frm_Rpt.C1Report1.Fields("LBLTITULO_1").Text = "Consulta por: " & loglo_Titulo
            frm_Rpt.C1Report1.Fields("LBLTITULO_1").Text = "Consulta por: Áreas Superpuestas"
            frm_Rpt.C1Report1.Fields("CODIGO_DM").Text = v_codigo
            frm_Rpt.C1Report1.Fields("CONCESION_DM").Text = v_nombre_dm
            frm_Rpt.C1Report1.Fields("AREA_TOTAL_DM").Text = v_area_eval
            frm_Rpt.C1Report1.Fields("AREA DISPONIBLE_DM").Text = v_area_dispo
            frm_Rpt.Show() : frm_Rpt.C1Report1.Render()
        End If
    End Sub

    Public Sub Delete_Rows_FC(ByVal p_Layer As String)
        Dim pQueryFilter As IQueryFilter
        Try
            Conexion_GeoDatabase()
            Dim pTable As ITable
            pTable = pFeatureWorkspace.OpenTable(p_Layer)
            pQueryFilter = New QueryFilter
            pQueryFilter.WhereClause = "OBJECTID IS NOT NULL"
            pTable.DeleteSearchedRows(pQueryFilter)
        Catch ex As Exception
            MsgBox(".::Error - Eliminado Rows a FeatureClass::.", vbInformation, "GEOCATMIN")
        End Try
    End Sub
    Public Sub Load_FC(ByVal p_FeatureClass As String, ByVal p_Layer As String, ByVal p_App As IApplication, ByVal p_Visible As Boolean)
        Try
            pMxDoc = p_App.Document
            pMap = pMxDoc.FocusMap
            Conexion_GeoDatabase()
            pFeatureClass = pFeatureWorkspace.OpenFeatureClass(p_FeatureClass)
            pFeatureLayer = New FeatureLayerClass
            pFeatureLayer.FeatureClass = pFeatureClass
            Select Case p_FeatureClass
                Case "Malla_17", "Malla_18", "Malla_19"
                    pFeatureLayer.Name = "Malla"
                    pFeatureLayer.Visible = p_Visible
                Case "Catastro"
                    pFeatureLayer.Name = "Catastro_1"
                    pFeatureLayer.Visible = p_Visible
                Case Else
                    pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName
            End Select
            pMap.AddLayer(pFeatureLayer)
            'pMap.Layer(0).Visible = True
            pMxDoc.ActiveView.Refresh()
        Catch ex As Exception
            MsgBox(".::Error - Cargango FeatureClass::.", vbInformation, "GEOCATMIN")
        End Try
    End Sub

    Public Sub Genera_Malla_UTM(ByVal v_este_min As Double, ByVal v_norte_min As Double, ByVal v_este_max As Double, ByVal v_norte_max As Double, ByVal lo_Zona As String, ByVal p_App As IApplication)
        Conexion_GeoDatabase()
        Dim pPoint(10, 10) As IPoint
        Dim v_xMin, v_xMax, v_yMin, v_yMax As Double
        Dim loint_Buffer As Integer = 0
        v_xMin = (Int((v_este_min - loint_Buffer) / 1000) * 1000)
        v_xMax = (Int((v_este_max + loint_Buffer) / 1000) * 1000)
        v_yMin = (Int((v_norte_min - loint_Buffer) / 1000) * 1000)
        v_yMax = (Int((v_norte_max + loint_Buffer) / 1000) * 1000)
        Dim con_lista As Integer = 6
        Dim intervalo As Integer = ((((v_yMax - v_yMin) * 4) / 60) / 1000) '* 1000
        Select Case intervalo
            Case 0
                intervalo = 1000
            Case 1, 2, 3, 4, 5
                intervalo = 2000
            Case 6, 7, 8, 9, 10
                intervalo = 4000
            Case 11, 12, 13, 14, 15
                intervalo = 8000
            Case 16, 17, 18, 19, 20
                intervalo = 12000
            Case 21, 22, 23, 24, 25
                intervalo = 16000
            Case Else
                intervalo = 20000
        End Select
        'If (v_xMin / 1000) Mod 2 <> 0 Then v_xMin = v_xMin - 1000
        'If (v_xMax / 1000) Mod 2 <> 0 Then v_xMax = v_xMax + 1000
        'If (v_yMin / 1000) Mod 2 <> 0 Then v_yMin = v_yMin - 1000
        'If (v_yMax / 1000) Mod 2 <> 0 Then v_yMax = v_yMax + 1000
        Try
            pMxDoc = p_App.Document
            pMap = pMxDoc.FocusMap
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Malla" Then
                    pFeatureLayer = pMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If
            pWorkspaceFactory = New AccessWorkspaceFactory
            pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_PathGDB, 0)
            Dim distancia As Long
            Dim pID As New UID
            Dim pEditor As IEditor
            pFeatureClass = pFeatureLayer.FeatureClass
            pID.Value = "esriCore.Editor"
            pEditor = p_App.FindExtensionByCLSID(pID)
            Dim pdataset As IDataset
            pdataset = pFeatureClass
            pFeatureWorkspace = pdataset.Workspace
            pEditor.StartEditing(pFeatureWorkspace)
            pEditor.StartOperation()
            'Declarando la coleccion de puntos 
            Dim xNum As Long
            Dim pPointColl As IPointCollection
            Dim Fila_x As String = ""
            Dim Fila_y As String = ""
            Dim pLine As ILine
            Dim pGeometryColl As IGeometryCollection 'Objeto de tipo de geometria del tema 
            Dim pSegmentColl As ISegmentCollection 'Objeto de segmento de coleccion 
            Dim i As Long
            ''Definiendo parametros 
            xNum = 100
            ReDim pPoint(0 To xNum, 0 To 3)
            distancia = 0
            Dim distancia1 As Long
            distancia1 = 0
            '''''''Genera Malla Point
            Dim pFLayerDMP As IFeatureLayer = Nothing
            Dim pFeatureDMP As IFeature
            Dim pFCLass1 As IFeatureClass
            Dim pPointUTM As IPoint
            afound = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Mallap_" & lo_Zona Then
                    pFLayerDMP = pMxDoc.FocusMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If
            pFCLass1 = pFLayerDMP.FeatureClass
            pID.Value = "esriCore.Editor"
            pEditor = p_App.FindExtensionByCLSID(pID)
            pdataset = pFCLass1
            '''''''''''--------------
            For i = 0 To xNum
                If v_xMin + distancia > v_xMax Then
                    If Fila_x <> "OK" Then
                        pFeature = pFeatureClass.CreateFeature
                        pPointColl = New Polyline ' Definiendo el tipo de geometria 
                        pGeometryColl = New Polyline 'Asignando 
                        pPoint(i, 0) = New ESRI.ArcGIS.Geometry.Point
                        pPoint(i, 1) = New ESRI.ArcGIS.Geometry.Point
                        pPoint(i, 0).PutCoords((v_xMax), v_yMin)
                        pPoint(i, 1).PutCoords((v_xMax), v_yMax)
                        pSegmentColl = New ESRI.ArcGIS.Geometry.Path 'Generando segmento de linea 
                        pLine = New Line
                        pLine.PutCoords(pPoint(i, 0), pPoint(i, 1))
                        pSegmentColl.AddSegment(pLine)
                        pGeometryColl.AddGeometry(pSegmentColl)
                        pFeature.Shape = pGeometryColl
                        pFeature.Value(pFeature.Fields.FindField("CLASE")) = 1 'contador
                        distancia = distancia + intervalo
                        pFeature.Store()
                        Fila_x = "OK"
                    End If
                Else
                    pFeature = pFeatureClass.CreateFeature
                    pPointColl = New Polyline ' Definiendo el tipo de geometria 
                    pGeometryColl = New Polyline 'Asignando 
                    pPoint(i, 0) = New ESRI.ArcGIS.Geometry.Point
                    pPoint(i, 1) = New ESRI.ArcGIS.Geometry.Point
                    pPoint(i, 0).PutCoords((v_xMin + distancia), v_yMin)
                    pPoint(i, 1).PutCoords((v_xMin + distancia), v_yMax)
                    pSegmentColl = New ESRI.ArcGIS.Geometry.Path 'Generando segmento de linea 
                    pLine = New Line
                    pLine.PutCoords(pPoint(i, 0), pPoint(i, 1))
                    pSegmentColl.AddSegment(pLine)
                    'Añadiendo la linea 
                    pGeometryColl.AddGeometry(pSegmentColl)
                    pFeature.Shape = pGeometryColl
                    'Asignando la coleccion a la linea 
                    Select Case i
                        Case 0
                            pFeature.Value(pFeature.Fields.FindField("CLASE")) = 1
                        Case Else
                            pFeature.Value(pFeature.Fields.FindField("CLASE")) = 2
                    End Select
                    distancia = distancia + intervalo
                    pFeature.Store()
                    If i <> 0 Then
                        pFeatureDMP = pFCLass1.CreateFeature
                        pPointUTM = New ESRI.ArcGIS.Geometry.Point
                        pPointUTM.X = pPoint(i, 0).X
                        pPointUTM.Y = v_yMin - 100
                        pFeatureDMP.Shape = pPointUTM
                        pFeatureDMP.Value(pFeatureDMP.Fields.FindField("VALOR")) = (pPoint(i, 0).X / 1000)
                        pFeatureDMP.Store()
                        pEditor.StopOperation("Add point")
                    End If
                End If
                If v_yMin + distancia1 > v_yMax Then
                    If Fila_y <> "OK" Then
                        pFeature = pFeatureClass.CreateFeature
                        pPointColl = New Polyline ' Definiendo el tipo de geometria 
                        pGeometryColl = New Polyline 'Asignando 
                        pPoint(i, 2) = New ESRI.ArcGIS.Geometry.Point
                        pPoint(i, 3) = New ESRI.ArcGIS.Geometry.Point
                        pPoint(i, 2).PutCoords(v_xMin, v_yMax)
                        pPoint(i, 3).PutCoords(v_xMax, v_yMax)
                        pSegmentColl = New ESRI.ArcGIS.Geometry.Path
                        pLine = New Line
                        pLine.PutCoords(pPoint(i, 2), pPoint(i, 3))
                        pSegmentColl.AddSegment(pLine)
                        pGeometryColl.AddGeometry(pSegmentColl)
                        pFeature.Shape = pGeometryColl
                        pFeature.Value(pFeature.Fields.FindField("CLASE")) = 1
                        distancia1 = distancia1 + intervalo
                        pFeature.Store()
                        Fila_y = "OK"
                    End If
                Else
                    pFeature = pFeatureClass.CreateFeature
                    pPointColl = New Polyline ' Definiendo el tipo de geometria 
                    pGeometryColl = New Polyline 'Asignando 
                    pPoint(i, 2) = New ESRI.ArcGIS.Geometry.Point
                    pPoint(i, 3) = New ESRI.ArcGIS.Geometry.Point
                    pPoint(i, 2).PutCoords(v_xMin, v_yMin + distancia1)
                    pPoint(i, 3).PutCoords(v_xMax, v_yMin + distancia1)
                    pSegmentColl = New ESRI.ArcGIS.Geometry.Path
                    pLine = New Line
                    pLine.PutCoords(pPoint(i, 2), pPoint(i, 3))
                    pSegmentColl.AddSegment(pLine)
                    pGeometryColl.AddGeometry(pSegmentColl)
                    pFeature.Shape = pGeometryColl
                    Select Case i
                        Case 0
                            pFeature.Value(pFeature.Fields.FindField("CLASE")) = 1
                        Case Else
                            pFeature.Value(pFeature.Fields.FindField("CLASE")) = 2
                    End Select
                    distancia1 = distancia1 + intervalo
                    pFeature.Store()
                    If i <> 0 Then
                        pFeatureDMP = pFCLass1.CreateFeature
                        pPointUTM = New ESRI.ArcGIS.Geometry.Point
                        pPointUTM.X = pPoint(i, 2).X - 100
                        pPointUTM.Y = pPoint(i, 3).Y
                        pFeatureDMP.Shape = pPointUTM
                        pFeatureDMP.Value(pFeatureDMP.Fields.FindField("VALOR")) = (pPoint(i, 3).Y / 1000)
                        pFeatureDMP.Store()
                        pEditor.StopOperation("Add point")
                    End If
                End If
            Next i
            pEditor.StopOperation("Add Line")
            pEditor.StopEditing(True)
            pMxDoc.ActivatedView.Refresh()
        Catch ex As Exception
            MsgBox(".::Error - Generación de la Malla::.", vbInformation, "GEOCATMIN")
        End Try
        loint_Intervalo = intervalo
        HazZoom(v_xMin - intervalo, v_yMin - intervalo, v_xMax + intervalo, v_yMax + intervalo, 0, p_App)
    End Sub
    Public Sub Genera_MallaUTM_0(ByVal v_este_min As Double, ByVal v_norte_min As Double, ByVal v_este_max As Double, ByVal v_norte_max As Double, ByVal lo_Zona As String, ByVal p_App As IApplication)
        For i As Double = 1 To 2 Step 1000
            For j As Double = 1 To 2 Step 1000

            Next
        Next
    End Sub

    Public Sub Genera_Malla_100Ha(ByVal v_este_min As Double, ByVal v_norte_min As Double, ByVal v_este_max As Double, ByVal v_norte_max As Double, ByVal lo_Zona As String, ByVal p_App As IApplication)
        Conexion_GeoDatabase()
        Dim pPoint(10, 10) As IPoint
        Dim v_xMin, v_xMax, v_yMin, v_yMax As Double
        Dim con_lista As Integer = 6
        Dim intervalo As Integer = 1000
        v_xMin = (Int(v_este_min)) : v_xMax = (Int(v_este_max))
        v_yMin = (Int(v_norte_min)) : v_yMax = (Int(v_norte_max))
        Try
            pMxDoc = p_App.Document
            pMap = pMxDoc.FocusMap
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Malla_Area_18" Then
                    pFeatureLayer = pMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If
            pWorkspaceFactory = New AccessWorkspaceFactory
            pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_PathGDB, 0)
            Dim distancia As Long
            Dim pID As New UID
            Dim pEditor As IEditor
            pFeatureClass = pFeatureLayer.FeatureClass
            pID.Value = "esriCore.Editor"
            pEditor = p_App.FindExtensionByCLSID(pID)
            Dim pdataset As IDataset
            pdataset = pFeatureClass
            pFeatureWorkspace = pdataset.Workspace
            pEditor.StartEditing(pFeatureWorkspace)
            pEditor.StartOperation()
            'Declarando la coleccion de puntos 
            Dim xNum As Long
            Dim pPointColl As IPointCollection
            Dim Fila_x As String = "" : Dim Fila_y As String = ""
            Dim pLine As ILine
            Dim pGeometryColl As IGeometryCollection 'Objeto de tipo de geometria del tema 
            Dim pSegmentColl As ISegmentCollection 'Objeto de segmento de coleccion 
            Dim i As Long
            ''Definiendo parametros 
            xNum = 100
            ReDim pPoint(0 To xNum, 0 To 3)
            distancia = 0
            Dim distancia1 As Long
            distancia1 = 0
            For i = 0 To xNum
                If v_xMin + distancia >= v_xMax Then
                    If Fila_x <> "OK" Then
                        pFeature = pFeatureClass.CreateFeature
                        pPointColl = New Polyline ' Definiendo el tipo de geometria 
                        pGeometryColl = New Polyline 'Asignando 
                        pPoint(i, 0) = New ESRI.ArcGIS.Geometry.Point
                        pPoint(i, 1) = New ESRI.ArcGIS.Geometry.Point
                        pPoint(i, 0).PutCoords((v_xMax), v_yMin)
                        pPoint(i, 1).PutCoords((v_xMax), v_yMax)
                        pSegmentColl = New ESRI.ArcGIS.Geometry.Path 'Generando segmento de linea 
                        pLine = New Line
                        pLine.PutCoords(pPoint(i, 0), pPoint(i, 1))
                        pSegmentColl.AddSegment(pLine)
                        pGeometryColl.AddGeometry(pSegmentColl)
                        pFeature.Shape = pGeometryColl
                        'pFeature.Value(pFeature.Fields.FindField("CLASE")) = 1 'contador
                        distancia = distancia + intervalo
                        pFeature.Store()
                        Fila_x = "OK"
                    End If
                Else
                    pFeature = pFeatureClass.CreateFeature
                    pPointColl = New Polyline ' Definiendo el tipo de geometria 
                    pGeometryColl = New Polyline 'Asignando 
                    pPoint(i, 0) = New ESRI.ArcGIS.Geometry.Point
                    pPoint(i, 1) = New ESRI.ArcGIS.Geometry.Point
                    pPoint(i, 0).PutCoords((v_xMin + distancia), v_yMin)
                    pPoint(i, 1).PutCoords((v_xMin + distancia), v_yMax)
                    pSegmentColl = New ESRI.ArcGIS.Geometry.Path 'Generando segmento de linea 
                    pLine = New Line
                    pLine.PutCoords(pPoint(i, 0), pPoint(i, 1))
                    pSegmentColl.AddSegment(pLine)
                    'Añadiendo la linea 
                    pGeometryColl.AddGeometry(pSegmentColl)
                    pFeature.Shape = pGeometryColl
                    'Asignando la coleccion a la linea 
                    'pFeature.Value(pFeature.Fields.FindField("CLASE")) = 1
                    distancia = distancia + intervalo
                    pFeature.Store()
                    If i <> 0 Then
                        'pFeatureDMP = pFCLass1.CreateFeature
                        'pPointUTM = New ESRI.ArcGIS.Geometry.Point
                        'pPointUTM.X = pPoint(i, 0).X
                        'pPointUTM.Y = v_yMin - 100
                        'pFeatureDMP.Shape = pPointUTM
                        'pFeatureDMP.Value(pFeatureDMP.Fields.FindField("VALOR")) = (pPoint(i, 0).X / 1000)
                        'pFeatureDMP.Store()
                        'pEditor.StopOperation("Add point")
                    End If
                End If
                If v_yMin + distancia1 >= v_yMax Then
                    If Fila_y <> "OK" Then
                        pFeature = pFeatureClass.CreateFeature
                        pPointColl = New Polyline ' Definiendo el tipo de geometria 
                        pGeometryColl = New Polyline 'Asignando 
                        pPoint(i, 2) = New ESRI.ArcGIS.Geometry.Point
                        pPoint(i, 3) = New ESRI.ArcGIS.Geometry.Point
                        pPoint(i, 2).PutCoords(v_xMin, v_yMax)
                        pPoint(i, 3).PutCoords(v_xMax, v_yMax)
                        pSegmentColl = New ESRI.ArcGIS.Geometry.Path
                        pLine = New Line
                        pLine.PutCoords(pPoint(i, 2), pPoint(i, 3))
                        pSegmentColl.AddSegment(pLine)
                        pGeometryColl.AddGeometry(pSegmentColl)
                        pFeature.Shape = pGeometryColl
                        'pFeature.Value(pFeature.Fields.FindField("CLASE")) = 1
                        distancia1 = distancia1 + intervalo
                        pFeature.Store()
                        Fila_y = "OK"
                    End If
                Else
                    pFeature = pFeatureClass.CreateFeature
                    pPointColl = New Polyline ' Definiendo el tipo de geometria 
                    pGeometryColl = New Polyline 'Asignando 
                    pPoint(i, 2) = New ESRI.ArcGIS.Geometry.Point
                    pPoint(i, 3) = New ESRI.ArcGIS.Geometry.Point
                    pPoint(i, 2).PutCoords(v_xMin, v_yMin + distancia1)
                    pPoint(i, 3).PutCoords(v_xMax, v_yMin + distancia1)
                    pSegmentColl = New ESRI.ArcGIS.Geometry.Path
                    pLine = New Line
                    pLine.PutCoords(pPoint(i, 2), pPoint(i, 3))
                    pSegmentColl.AddSegment(pLine)
                    pGeometryColl.AddGeometry(pSegmentColl)
                    pFeature.Shape = pGeometryColl
                    'pFeature.Value(pFeature.Fields.FindField("CLASE")) = 1
                    distancia1 = distancia1 + intervalo
                    pFeature.Store()
                    If i <> 0 Then
                        'pFeatureDMP = pFCLass1.CreateFeature
                        'pPointUTM = New ESRI.ArcGIS.Geometry.Point
                        'pPointUTM.X = pPoint(i, 2).X - 100
                        'pPointUTM.Y = pPoint(i, 3).Y
                        'pFeatureDMP.Shape = pPointUTM
                        'pFeatureDMP.Value(pFeatureDMP.Fields.FindField("VALOR")) = (pPoint(i, 3).Y / 1000)
                        'pFeatureDMP.Store()
                        'pEditor.StopOperation("Add point")
                    End If
                End If
            Next i
            pEditor.StopOperation("Add Line")
            pEditor.StopEditing(True)
            pMxDoc.ActivatedView.Refresh()
        Catch ex As Exception
            MsgBox(".::Error - Generación de la Malla::.", vbInformation, "GEOCATMIN")
        End Try
        loint_Intervalo = intervalo
        HazZoom(v_xMin - intervalo, v_yMin - intervalo, v_xMax + intervalo, v_yMax + intervalo, 0, p_App)
    End Sub

    Public Sub Seleccioname_Envelope(ByVal p_Filtro As String, ByVal p_Layer As String, ByVal Xmin As Object, ByVal yMin As Object, ByVal xMax As Object, ByVal yMax As Object, ByVal p_Zona As String, ByVal p_App As IApplication)
        Dim pActiveView As IActiveView
        Dim pFeatureSelection As IFeatureSelection
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        Dim afound As Boolean
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = p_Layer Then
                pFeatureLayer = pMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("Layer No Existe.")
            Exit Sub
        End If
        Dim pQueryFilter As IQueryFilter
        pQueryFilter = New QueryFilter
        pQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = p_Filtro
        pFeatureSelection = pFeatureLayer
        pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        'Refresh again to draw the new selection.
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        Dim pFSel As IFeatureSelection = pFeatureLayer
        'Get the selected features
        Dim pSelSet As ISelectionSet
        If pFSel.SelectionSet.Count = 0 Then
            MsgBox("No hay ninguna Selección")
            Exit Sub
        End If
        pSelSet = pFSel.SelectionSet
        Dim pEnumGeom As IEnumGeometry
        Dim pEnumGeomBind As IEnumGeometryBind
        pEnumGeom = New EnumFeatureGeometry
        pEnumGeomBind = pEnumGeom
        pEnumGeomBind.BindGeometrySource(Nothing, pSelSet)
        Dim pGeomFactory As IGeometryFactory
        pGeomFactory = New GeometryEnvironment
        Dim pGeom As IGeometry
        pGeom = pGeomFactory.CreateGeometryFromEnumerator(pEnumGeom)
        pMxDoc.ActiveView.Extent = pGeom.Envelope
        pMxDoc.ActiveView.Refresh()
        pSpatialReferenceEnvelope = New SpatialReferenceEnvironment
        'pGeom.Envelope.SpatialReference = Datum_PSAD_18
        Select Case p_Zona
            Case "17"
                pGeom.Project(Datum_PSAD_17)
            Case "19"
                pGeom.Project(Datum_PSAD_19)
        End Select
        Dim loint_X As Integer = (pGeom.Envelope.XMax - pGeom.Envelope.XMin) * 0.08
        Dim loint_Y As Integer = (pGeom.Envelope.YMax - pGeom.Envelope.YMin) * 0.08
        Xmin.Text = pGeom.Envelope.XMin : xMax.Text = pGeom.Envelope.XMax
        yMin.Text = pGeom.Envelope.YMin : yMax.Text = pGeom.Envelope.YMax
        HazZoom(pGeom.Envelope.XMin - loint_X, pGeom.Envelope.YMin - loint_Y, pGeom.Envelope.XMax + loint_X, pGeom.Envelope.YMax + loint_Y, 0, p_App)
        pFeatureSelection.Clear()
    End Sub

    Public Sub Cargar_ListBox(ByVal domainName As String, ByVal controlName As Object)
        Dim pWSDomains As IWorkspaceDomains
        Dim pDomain As IDomain
        Dim pCodedValueDomain As ICodedValueDomain
        pWSDomains = pFeatureWorkspace
        pDomain = pWSDomains.DomainByName(domainName)
        pCodedValueDomain = pDomain
        'Clear the combo box list 
        Dim dt1 As New DataTable
        dt1.Columns.Add("Numero", Type.GetType("System.Double"))
        dt1.Columns.Add("Codigo", Type.GetType("System.String"))
        dt1.Columns.Add("Nombre", Type.GetType("System.String"))
        'Dim loint_Codi(501, 2) As String
        For IntDomainCount As Integer = 0 To (pCodedValueDomain.CodeCount - 1)
            Dim dr1 As DataRow
            dr1 = dt1.NewRow
            dr1.Item("Numero") = CType(Mid(pCodedValueDomain.Value(IntDomainCount), 1, InStr(pCodedValueDomain.Value(IntDomainCount), "-") - 1), Integer)
            dr1.Item("Codigo") = pCodedValueDomain.Value(IntDomainCount)
            dr1.Item("Nombre") = pCodedValueDomain.Name(IntDomainCount)
            dt1.Rows.Add(dr1)
        Next IntDomainCount
        Dim lodtvOrdena As New DataView(dt1, Nothing, "Numero ASC", DataViewRowState.CurrentRows)
        For i As Integer = 0 To lodtvOrdena.Count - 1
            Dim lostrValor As String = lodtvOrdena.Item(i).Row(1)
            Dim lostrName As String = lodtvOrdena.Item(i).Row(2)
            controlName.items.add("  " & lostrValor.ToUpper & "  ( " & lostrName & " )")
        Next
    End Sub
    Public Sub Cargar_Combo(ByVal domainName As String, ByVal controlName As Object)
        Dim pWSDomains As IWorkspaceDomains
        Dim pDomain As IDomain
        Dim pCodedValueDomain As ICodedValueDomain
        pWSDomains = pFeatureWorkspace
        pDomain = pWSDomains.DomainByName(domainName)
        pCodedValueDomain = pDomain
        'Clear the combo box list 
        'controlName.Items.Clear()
        Dim dt1 As New DataTable
        dt1.Columns.Add("Codigo", Type.GetType("System.String"))
        dt1.Columns.Add("Nombre", Type.GetType("System.String"))
        Dim dr As DataRow
        dr = dt1.NewRow
        dr.Item("Codigo") = ""
        dr.Item("Nombre") = "--Seleccionar--"
        dt1.Rows.Add(dr)
        Dim IntDomainCount As Integer
        For IntDomainCount = 0 To (pCodedValueDomain.CodeCount - 1)
            If domainName = "DOM_DEPARTAMENTO" Then
                Dim dr1 As DataRow
                dr1 = dt1.NewRow
                dr1.Item("Codigo") = pCodedValueDomain.Value(IntDomainCount)
                dr1.Item("Nombre") = pCodedValueDomain.Name(IntDomainCount)
                dt1.Rows.Add(dr1)
            End If
            If domainName = "DOM_PROVINCIA" Then
                tmp_Prov(IntDomainCount, 0) = pCodedValueDomain.Value(IntDomainCount)
                tmp_Prov(IntDomainCount, 1) = pCodedValueDomain.Name(IntDomainCount)
            End If
            If domainName = "DOM_DISTRITO" Then
                tmp_Dist(IntDomainCount, 0) = pCodedValueDomain.Value(IntDomainCount)
                tmp_Dist(IntDomainCount, 1) = pCodedValueDomain.Name(IntDomainCount)
            End If
        Next IntDomainCount
        controlName.DisplayMember = "Nombre"
        controlName.ValueMember = "Codigo"
        controlName.datasource = dt1.DefaultView
    End Sub
    Public Function FT_BuscarRegistro(ByVal paloNombreCampo As String, ByVal paloCodigoInterno As Integer, ByVal paloITable As String, _
    Optional ByVal paloAdicional As String = "") As DataTable
        If paloITable = "GED_M_UBIGEO_PROV" Or paloITable = "GED_M_UBIGEO_DIST" Then
            pTable = pFeatureWorkspace.OpenTable(paloITable)
        End If
        Dim pFeatureCursor As ICursor
        Dim pQueryFilter As IQueryFilter
        Dim lodtRegistros As New DataTable
        Dim loNumCol, loNumColTemp As Int16
        pFields = pTable.Fields
        loNumCol = pFields.FieldCount
        For c As Int16 = 0 To loNumCol - 1
            lodtRegistros.Columns.Add(pFields.Field(c).Name)
        Next
        pQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = paloNombreCampo & " = " & paloCodigoInterno
        pFeatureCursor = pTable.Search(pQueryFilter, False)

        If paloAdicional = "1" Then
            Dim dr1 As DataRow
            dr1 = lodtRegistros.NewRow
            Try
                If paloITable = "GED_M_LISTA" Then
                    dr1.Item("CODIGO") = ""
                    dr1.Item("DESCRIPCION") = "--Seleccionar--"
                End If
            Catch ex As Exception
            End Try
            'Next
            lodtRegistros.Rows.Add(dr1)
        End If
        Dim pRow As IRow
        pRow = pFeatureCursor.NextRow
        Do Until pRow Is Nothing
            If Not IsDBNull(pRow.Value(loNumColTemp)) Then
                Dim dr As DataRow
                dr = lodtRegistros.NewRow
                For c As Int16 = 0 To loNumCol - 1
                    Try
                        If Not IsDBNull(pRow.Value(c)) Then dr.Item(c) = CType(pRow.Value(c), String)
                    Catch ex As Exception
                    End Try
                Next
                lodtRegistros.Rows.Add(dr)
            End If
            pRow = pFeatureCursor.NextRow
        Loop
        Return lodtRegistros
    End Function
    Public Sub Color_Poligono_Simple(ByVal p_App As IApplication, ByVal p_Layer As String)
        Dim pGeoFeatureL As IGeoFeatureLayer
        Dim pSimpleRenderer As ISimpleRenderer
        Dim pSimpleFillS As ISimpleFillSymbol
        Dim pLineSymbol As ISimpleLineSymbol
        Try
            pMap = pMxDoc.FocusMap
            pSimpleFillS = New SimpleFillSymbol
            Dim pFeatLayer As IFeatureLayer = Nothing
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = p_Layer Then
                    pFeatLayer = pMap.Layer(A)
                    pGeoFeatureL = pFeatLayer
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
            End If
            If arch_cata = "union" Then
                'pSimpleFillS.Style = esriSimpleFillStyle.esriSFSBackwardDiagonal
                pSimpleFillS.Style = esriSimpleFillStyle.esriSFSForwardDiagonal
                pSimpleFillS.Color = GetRGBColor(230, 76, 0)
                pLineSymbol = New SimpleLineSymbol
                pLineSymbol.Style = esriSimpleLineStyle.esriSLSInsideFrame
                pLineSymbol.Color = GetRGBColor(230, 76, 0)
            ElseIf arch_cata = "Interceccion" Then
                pSimpleFillS.Style = esriSimpleFillStyle.esriSFSForwardDiagonal
                pSimpleFillS.Color = GetRGBColor(56, 168, 0)
                pLineSymbol = New SimpleLineSymbol
                'pLineSymbol.Style = esriSimpleLineStyle.esriSLSInsideFrame
                pLineSymbol.Style = esriSimpleLineStyle.esriSLSInsideFrame
                pLineSymbol.Color = GetRGBColor(56, 168, 0)
            ElseIf arch_cata = "traslape" Then
                pSimpleFillS.Style = esriSimpleFillStyle.esriSFSCross
                pSimpleFillS.Color = GetRGBColor(255, 255, 0)
                pLineSymbol = New SimpleLineSymbol
                'pLineSymbol.Style = esriSimpleLineStyle.esriSLSInsideFrame
                pLineSymbol.Style = esriSimpleLineStyle.esriSLSDot
                pLineSymbol.Color = GetRGBColor(230, 230, 0)
            ElseIf arch_cata = "Zona Urbana" Or p_Layer = "Zona Urbana" Then
                'pSimpleFillS.Style = esriSimpleFillStyle.esriSFSBackwardDiagonal
                pSimpleFillS.Style = esriSimpleFillStyle.esriSFSForwardDiagonal
                pSimpleFillS.Color = GetRGBColor(227, 158, 0)
                pLineSymbol = New SimpleLineSymbol
                pLineSymbol.Style = esriSimpleLineStyle.esriSLSInsideFrame
                pLineSymbol.Color = GetRGBColor(227, 158, 0)
            ElseIf arch_cata = "Zona Reservada" Or p_Layer = "Zona Reservada" Then
                'pSimpleFillS.Style = esriSimpleFillStyle.esriSFSBackwardDiagonal
                pSimpleFillS.Style = esriSimpleFillStyle.esriSFSForwardDiagonal
                pSimpleFillS.Color = GetRGBColor(76, 230, 0)
                pLineSymbol = New SimpleLineSymbol
                pLineSymbol.Style = esriSimpleLineStyle.esriSLSInsideFrame
                pLineSymbol.Color = GetRGBColor(76, 230, 0)
            ElseIf arch_cata = "DM_Uso_Minero" Or p_Layer = "DM_Uso_Minero" Then
                'pSimpleFillS.Style = esriSimpleFillStyle.esriSFSBackwardDiagonal
                pSimpleFillS.Style = esriSimpleFillStyle.esriSFSSolid
                pSimpleFillS.Color = GetRGBColor(115, 223, 255)
                pLineSymbol = New SimpleLineSymbol
                pLineSymbol.Style = esriSimpleLineStyle.esriSLSInsideFrame
                pLineSymbol.Color = GetRGBColor(115, 223, 255)
            ElseIf arch_cata = "DM_Actividad_Minera" Or p_Layer = "DM_Actividad_Minera" Then
                'pSimpleFillS.Style = esriSimpleFillStyle.esriSFSBackwardDiagonal
                pSimpleFillS.Style = esriSimpleFillStyle.esriSFSSolid
                pSimpleFillS.Color = GetRGBColor(255, 255, 115)
                pLineSymbol = New SimpleLineSymbol
                pLineSymbol.Style = esriSimpleLineStyle.esriSLSInsideFrame
                pLineSymbol.Color = GetRGBColor(255, 255, 115)


            Else
                pSimpleFillS.Style = esriSimpleFillStyle.esriSFSBackwardDiagonal
                pSimpleFillS.Color = GetRGBColor(76, 230, 0)
                pLineSymbol = New SimpleLineSymbol
                pLineSymbol.Style = esriSimpleLineStyle.esriSLSInsideFrame
                pLineSymbol.Color = GetRGBColor(76, 230, 0)
            End If
            pLineSymbol.Width = 1
            pSimpleFillS.Outline = pLineSymbol
            pSimpleRenderer = New SimpleRenderer
            pSimpleRenderer.Symbol = pSimpleFillS
            pGeoFeatureL.Renderer = pSimpleRenderer
            pMxDoc.ActiveView.Refresh()
            pMxDoc.UpdateContents()
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetRGBColor(ByVal yourRed As Long, ByVal yourGreen As Long, _
    ByVal yourBlue As Long) As IRgbColor
        Dim pRGB As IRgbColor
        pRGB = New RgbColor
        With pRGB
            .Red = yourRed
            .Green = yourGreen
            .Blue = yourBlue
            .UseWindowsDithering = True
        End With
        GetRGBColor = pRGB
    End Function
    Function f_Seleccionar_Items(ByVal p_Filtro As String, ByVal p_App As IApplication)
        pMxDoc = p_App.Document
        Dim pPoint As IPoint
        pPoint = pMxDoc.CurrentLocation
        Dim pActiveView As IActiveView
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        'Dim A As Integer
        ' Part 2: Select features.
        Dim pQueryFilter As IQueryFilter
        Dim pocultatema As IFeatureLayerDefinition
        Dim pFeatureSelection As IFeatureSelection
        ' Prepare a query filter.
        pQueryFilter = New QueryFilter
        pocultatema = pFeatureLayer
        pocultatema.DefinitionExpression = p_Filtro '"CODIGOU = " & "'" & Me.txtConsulta.Text & "'"
        pMap.AddLayer(pFeatureLayer)
        pFeatureLayer = pMap.Layer(0)
        pQueryFilter.WhereClause = p_Filtro '"CODIGOU = " & "'" & Me.txtConsulta.Text & "'"
        pFeatureSelection = pocultatema
        pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        'Refresh again to draw the new selection.
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        Dim pFSel As IFeatureSelection
        pFSel = pFeatureLayer
        'Get the selected features
        Dim pSelSet As ISelectionSet
        If pFSel.SelectionSet.Count = 0 Then
            'MsgBox("No hay ninguna Selección")
            Return "No"
            Exit Function
        End If
        'Dim pRow As IRow 'Cuando es tabla
        Dim pFeatureCursor As IFeatureCursor
        pFeatureCursor = pFeatureLayer.Search(pQueryFilter, True)
        Dim pFeature As IFeature
        'Dim pRow As IRow
        pFeature = pFeatureCursor.NextFeature
        If Not pFeature Is Nothing Then
            glostrNaturaleza = pFeature.Value(pFeature.Fields.FindField("NATURALEZA"))
            glostrPartida = pFeature.Value(pFeature.Fields.FindField("PARTIDA"))
            glostrPadron = pFeature.Value(pFeature.Fields.FindField("PADRON"))
            glostrJefatura = pFeature.Value(pFeature.Fields.FindField("JEFATURA"))
            glostrTipoDer = pFeature.Value(pFeature.Fields.FindField("D_ESTADO"))
        End If
        pSelSet = pFSel.SelectionSet
        Dim pEnumGeom As IEnumGeometry
        Dim pEnumGeomBind As IEnumGeometryBind
        pEnumGeom = New EnumFeatureGeometry
        pEnumGeomBind = pEnumGeom
        pEnumGeomBind.BindGeometrySource(Nothing, pSelSet)
        Dim pGeomFactory As IGeometryFactory
        pGeomFactory = New GeometryEnvironment
        Dim pGeom As IGeometry
        pGeom = pGeomFactory.CreateGeometryFromEnumerator(pEnumGeom)
        pMxDoc.ActiveView.Extent = pGeom.Envelope
        pMxDoc.ActiveView.Refresh()
        Dim lointBuffer As Integer = 50
        HazZoom(pGeom.Envelope.XMin - lointBuffer, pGeom.Envelope.YMin - lointBuffer, pGeom.Envelope.XMax + lointBuffer, pGeom.Envelope.YMax + lointBuffer, 0, p_App)
        Return "Si"
    End Function
    Public Sub Seleccionar_Items(ByVal p_Filtro As String, ByVal p_App As IApplication)
        pMxDoc = p_App.Document
        Dim pPoint As IPoint
        pPoint = pMxDoc.CurrentLocation
        Dim pActiveView As IActiveView
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        'Dim A As Integer
        ' Part 2: Select features.
        Dim pQueryFilter As IQueryFilter
        Dim pocultatema As IFeatureLayerDefinition
        Dim pFeatureSelection As IFeatureSelection
        ' Prepare a query filter.
        pQueryFilter = New QueryFilter
        pocultatema = pFeatureLayer
        pocultatema.DefinitionExpression = p_Filtro '"CODIGOU = " & "'" & Me.txtConsulta.Text & "'"
        pMap.AddLayer(pFeatureLayer)
        pFeatureLayer = pMap.Layer(0)
        pQueryFilter.WhereClause = p_Filtro '"CODIGOU = " & "'" & Me.txtConsulta.Text & "'"
        pFeatureSelection = pocultatema
        pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        'Refresh again to draw the new selection.
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        Dim pFSel As IFeatureSelection
        pFSel = pFeatureLayer
        'Get the selected features
        Dim pSelSet As ISelectionSet
        If pFSel.SelectionSet.Count = 0 Then
            MsgBox("No hay ninguna Selección")
            Exit Sub
        End If
        'Dim pRow As IRow 'Cuando es tabla
        Dim pFeatureCursor As IFeatureCursor
        pFeatureCursor = pFeatureLayer.Search(pQueryFilter, True)
        Dim pFeature As IFeature
        'Dim pRow As IRow
        pFeature = pFeatureCursor.NextFeature
        If Not pFeature Is Nothing Then
            glostrNaturaleza = pFeature.Value(pFeature.Fields.FindField("NATURALEZA"))
            glostrPartida = pFeature.Value(pFeature.Fields.FindField("PARTIDA"))
            glostrPadron = pFeature.Value(pFeature.Fields.FindField("PADRON"))
            glostrJefatura = pFeature.Value(pFeature.Fields.FindField("JEFATURA"))
            glostrTipoDer = pFeature.Value(pFeature.Fields.FindField("D_ESTADO"))
        End If
        pSelSet = pFSel.SelectionSet
        Dim pEnumGeom As IEnumGeometry
        Dim pEnumGeomBind As IEnumGeometryBind
        pEnumGeom = New EnumFeatureGeometry
        pEnumGeomBind = pEnumGeom
        pEnumGeomBind.BindGeometrySource(Nothing, pSelSet)
        Dim pGeomFactory As IGeometryFactory
        pGeomFactory = New GeometryEnvironment
        Dim pGeom As IGeometry
        pGeom = pGeomFactory.CreateGeometryFromEnumerator(pEnumGeom)
        pMxDoc.ActiveView.Extent = pGeom.Envelope
        pMxDoc.ActiveView.Refresh()
        Dim lointBuffer As Integer = 50
        HazZoom(pGeom.Envelope.XMin - lointBuffer, pGeom.Envelope.YMin - lointBuffer, pGeom.Envelope.XMax + lointBuffer, pGeom.Envelope.YMax + lointBuffer, 0, p_App)
    End Sub
    Public Sub Poligono_Color_GDB(ByVal p_FeatureClass As String, ByVal p_Style As String, _
    ByVal p_Campo As String, ByVal p_Zona As String, ByVal p_Valor As String, ByVal p_Categoria As String, _
    ByVal p_App As IApplication, ByVal p_Filtro As String)
        ' Conexion_SDE(p_App)
        Dim afound As Boolean
        pMxDoc = p_App.Document
        'Dim p_Layer As ILayer
        Try
            pTable = pFeatureWorkspace.OpenTable(glo_Owner_Layer_SDE & p_FeatureClass)
            Dim pQueryDef As IQueryDef
            Dim pRow As IRow
            Dim pCursor As ICursor
            Dim pDataset As IDataset
            pDataset = pTable
            Dim pFeatureWorkspace1 As IFeatureWorkspace
            pFeatureWorkspace1 = pDataset.Workspace

            pQueryDef = pFeatureWorkspace1.CreateQueryDef
            With pQueryDef
                .Tables = pDataset.Name
                .WhereClause = p_Filtro
                .SubFields = "DISTINCT(" & p_Campo & ")"
                pCursor = .Evaluate
            End With
            pRow = pCursor.NextRow

            Dim pUniqueValueRenderer As IUniqueValueRenderer
            ' Define the renderer.
            pUniqueValueRenderer = New UniqueValueRenderer
            pUniqueValueRenderer.FieldCount = 1
            pUniqueValueRenderer.Field(0) = p_Campo
            Dim pStyleStorage As IStyleGalleryStorage
            Dim pStyleGallery As IStyleGallery
            pStyleGallery = New StyleGallery
            pStyleStorage = pStyleGallery
            Dim pEnumStyleGall As IEnumStyleGalleryItem
            Dim pStyleItem As IStyleGalleryItem
            Dim pFillSymbol As IFillSymbol = Nothing
            Dim pStyleGlry As IStyleGallery
            pStyleGlry = New StyleGallery
            Dim pStylStor As IStyleGalleryStorage
            pStylStor = pStyleGlry
            pStylStor.AddFile(p_Style)
            pEnumStyleGall = pStyleGallery.Items("Fill Symbols", p_Style, p_Categoria)
            pEnumStyleGall.Reset()
            pStyleItem = pEnumStyleGall.Next
            Do Until pRow Is Nothing
                If pFillSymbol Is Nothing Then
                    pEnumStyleGall.Reset()
                    pStyleItem = pEnumStyleGall.Next
                End If
                Do While Not pStyleItem Is Nothing   'Loop through and access each marker
                    pFillSymbol = Nothing
                    Select Case p_Valor
                        Case "Numero"
                            If pStyleItem.ID = pRow.Value(0).ToString Then
                                pFillSymbol = pStyleItem.Item
                                Exit Do
                            End If
                            If pStyleItem.ID > pRow.Value(0).ToString Then
                                pRow = pCursor.NextRow
                                If pRow Is Nothing Then Exit Do
                            Else
                                pStyleItem = pEnumStyleGall.Next
                            End If
                        Case "Cadena"

                            If pStyleItem.Name = pRow.Value(0).ToString Then
                                'pRow = pCursor.NextRow
                                pFillSymbol = pStyleItem.Item
                                Exit Do
                                
                            Else
                                pStyleItem = pEnumStyleGall.Next
                            End If
                            If pStyleItem.Name > pRow.Value(0).ToString Then

                                pRow = pCursor.NextRow
                                If pRow Is Nothing Then Exit Do
                            Else
                                pStyleItem = pEnumStyleGall.Next
                            End If
                    End Select
                Loop
                If pStyleItem Is Nothing Then
                Else
                    ' 'Select Case p_Valor
                    ''Case "Numero"
                    If Len(pStyleItem.Name) > 0 And Not (pRow Is Nothing) Then
                        pUniqueValueRenderer.AddValue(pRow.Value(0), "", pFillSymbol)
                    End If
                    'End Select
                    End If
                    pRow = pCursor.NextRow
            Loop
            Dim pLayer As IFeatureLayer = Nothing
            Dim pGeoFeatureLayer As IGeoFeatureLayer
            pMxDoc = p_App.Document
            pMap = pMxDoc.FocusMap
            Select Case p_FeatureClass
                Case "GPO_CMI_CATASTRO_MINERO_17", "GPO_CMI_CATASTRO_MINERO_18", "GPO_CMI_CATASTRO_MINERO_19"
                    p_FeatureClass = "Catastro"
                Case gstrFC_Departamento
                    p_FeatureClass = "Departamento"
                Case gstrFC_Provincia
                    p_FeatureClass = "Provincia"
                Case gstrFC_Distrito
                    p_FeatureClass = "Distrito"
                Case "GPO_ZUR_ZONA_URBANA_17", "GPO_ZUR_ZONA_URBANA_18", "GPO_ZUR_ZONA_URBANA_19"
                    p_FeatureClass = "Zona Urbana"
                Case gstrFC_AReservada56, "GPO_ARE_AREA_RESERVADA_17", "GPO_ARE_AREA_RESERVADA_18", "GPO_ARE_AREA_RESERVADA_19"
                    p_FeatureClass = "Zona Reservada"
                Case gstrFC_AReservada56, "GPT_CAM_CERTIFICACION_AMB_G56"
                    p_FeatureClass = "Certificacion Ambiental"
                Case "GPO_GEO_GEOLOGIA"
                    p_FeatureClass = "Geologia"
                Case "GPO_MEM_AREA_ACTIVIDAD_MINERA"
                    p_FeatureClass = "Actividad Minera"
                Case "GPO_MEM_USO_MINERO"
                    p_FeatureClass = "Uso Minero"

            End Select

            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = p_FeatureClass Then '& "_" & strZona Then
                    pLayer = pMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If
            'pLayer = pMap.Layer(1) ' 1 Catastro
            pGeoFeatureLayer = pLayer
            pGeoFeatureLayer.Renderer = pUniqueValueRenderer
            pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, Nothing)
            pMxDoc.UpdateContents()
        Catch ex As Exception
        End Try
    End Sub
    Function f_Genera_Leyenda_DM(ByVal p_Layer As String, ByVal p_App As IApplication)
        Dim lo_Campo As String = ""
        Dim lodtOrdena As New DataTable
        Select Case p_Layer
            Case "Certificacion Ambiental"
                lo_Campo = "TC_ESTCER"
            Case "Zona Reservada"
                lo_Campo = "CODIGO"
            Case "Drenaje"
                lo_Campo = "CD_DEPA"
            Case "Area_Restringida"
                lo_Campo = "CLASE"
            Case "Area_Urbana"
                lo_Campo = "TP_URBA"
            Case "Departamento"
                lo_Campo = "NM_DEPA"
        End Select
        Dim pQueryFilter As IQueryFilter
        Dim lostrCadena As String = ""
        Dim lodtRegistros As New DataTable
        Try
            pMxDoc = p_App.Document
            pMap = pMxDoc.FocusMap
            Dim pLayer As ILayer = Nothing
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name.ToUpper = p_Layer.ToUpper Then
                    pLayer = pMxDoc.FocusMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("Layer No Existe.")
                Return ""
                Exit Function
            End If
            'Dim pflayer As IFeatureLayer
            pFeatureLayer = pLayer
            Dim pLyr As IGeoFeatureLayer
            pLyr = pFeatureLayer
            'Dim pFeatureclass As IFeatureClass
            pFeatureClass = pFeatureLayer.FeatureClass
            pQueryFilter = New QueryFilter
            Dim pFeatCursor As IFeatureCursor
            pFeatCursor = pFeatureClass.Search(pQueryFilter, False)
            Dim rx As IRandomColorRamp
            rx = New RandomColorRamp
            Dim pRender As IUniqueValueRenderer, n As Long
            pRender = New UniqueValueRenderer
            Dim symd As ISimpleFillSymbol
            symd = New SimpleFillSymbol
            'Leyenda para la parte inicial de la leyenda (color Blanco)
            symd.Style = esriSimpleFillStyle.esriSFSSolid ' esriSFSSolid
            symd.Outline.Width = 0.4
            Dim myColor As IColor
            myColor = New RgbColor
            myColor.RGB = RGB(255, 255, 255)
            symd.Color = myColor
            pRender.FieldCount = 1
            pRender.Field(0) = lo_Campo
            pRender.DefaultSymbol = symd
            pRender.UseDefaultSymbol = True
            'Barriendo los registros
            Dim pFeat As IFeature
            n = pFeatureClass.FeatureCount(pQueryFilter)
            Dim i As Integer = 0
            Dim ValFound As Boolean
            Dim NoValFound As Boolean
            Dim uh As Integer
            Dim pFields As IFields
            Dim iField As Integer
            'Dim pField As IField
            pFields = pFeatureClass.Fields
            iField = pFields.FindField(lo_Campo)
            Do Until i = n
                Dim symx As ISimpleFillSymbol
                symx = New SimpleFillSymbol
                symx.Style = esriSimpleFillStyle.esriSFSForwardDiagonal ' esriSFSForwardDiagonal
                pFeat = pFeatCursor.NextFeature
                Dim X As String
                X = pFeat.Value(iField)
                If X <> "" Then
                    ValFound = False
                    For uh = 0 To (pRender.ValueCount - 1)
                        If pRender.Value(uh) = X Then
                            NoValFound = True
                            Exit For
                        End If
                    Next uh
                    If Not ValFound Then
                        pRender.AddValue(X, lo_Campo, symx)
                        pRender.Label(X) = X
                        pRender.Symbol(X) = symx
                    End If
                End If
                i = i + 1
            Loop
            rx.Size = pRender.ValueCount
            rx.CreateRamp(True)
            Dim RColors As IEnumColors, ny As Long
            RColors = rx.Colors
            RColors.Reset()
            For ny = 0 To (pRender.ValueCount - 1)
                'Dim xv As String = pRender.Value(ny)
                If ny = 0 Then lodtRegistros.Columns.Add(lo_Campo)
                Dim dr As DataRow
                dr = lodtRegistros.NewRow
                dr.Item(0) = pRender.Value(ny)
                lodtRegistros.Rows.Add(dr)
            Next
            Dim lodtvOrdena As New DataView(lodtRegistros, Nothing, lo_Campo & " ASC", DataViewRowState.CurrentRows)
            For i = 0 To lodtvOrdena.Count - 1
                If i = 0 Then lodtOrdena.Columns.Add(lo_Campo)
                Dim dr As DataRow
                dr = lodtOrdena.NewRow
                dr.Item(0) = lodtvOrdena.Item(i).Row(0)
                lodtOrdena.Rows.Add(dr)
            Next
        Catch ex As Exception
            MsgBox("Error generando Leyenda" & p_Layer, MsgBoxStyle.Information, "Aviso")
        End Try
        Return lodtOrdena
    End Function
    Public Sub Poligono_Color_GDB3(ByVal lo_FeatureClass As String, ByVal lo_Tabla As DataTable, ByVal p_Style As String, ByVal loCampo As String, ByVal p_Categoria As String, ByVal p_App As IApplication)
        Dim pUniqueValueRenderer As IUniqueValueRenderer
        ' Define the renderer.
        pUniqueValueRenderer = New UniqueValueRenderer
        pUniqueValueRenderer.FieldCount = 1
        pUniqueValueRenderer.Field(0) = loCampo
        Dim pStyleStorage As IStyleGalleryStorage
        Dim pStyleGallery As IStyleGallery
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        pStyleGallery = New StyleGallery
        pStyleStorage = pStyleGallery
        Dim pEnumStyleGall As IEnumStyleGalleryItem
        Dim pStyleItem As IStyleGalleryItem
        'Dim pFillSymbol As IFillSymbol = Nothing
        Dim pFillSymbol As IMarkerSymbol = Nothing
        Dim pStyleGlry As IStyleGallery
        pStyleGlry = New StyleGallery
        Dim pStylStor As IStyleGalleryStorage
        pStylStor = pStyleGlry
        pStylStor.AddFile(p_Style)
        ' pEnumStyleGall = pStyleGallery.Items("Fill Symbols", loStyle, p_Categoria)
        pEnumStyleGall = pStyleGallery.Items("Marker Symbols", p_Style, p_Categoria)
        pEnumStyleGall.Reset()
        pStyleItem = pEnumStyleGall.Next
        'Do Until pRow Is Nothing
        For i As Integer = 0 To lo_Tabla.Rows.Count - 1
            If pFillSymbol Is Nothing Then
                pEnumStyleGall.Reset()
                pStyleItem = pEnumStyleGall.Next
            End If
            Do While Not pStyleItem Is Nothing   'Loop through and access each marker
                pFillSymbol = Nothing
                If pStyleItem.Name = lo_Tabla.Rows(i).Item(loCampo) Then
                    'If pStyleItem.Name = pRow.Value(0) Then
                    pFillSymbol = pStyleItem.Item
                    Exit Do
                End If
                If pStyleItem.Name > lo_Tabla.Rows(i).Item(loCampo) Then
                Else
                    pStyleItem = pEnumStyleGall.Next
                End If
            Loop
            If pStyleItem Is Nothing Then
            Else
                'If Len(pStyleItem.Name) > 0 And Not (pRow Is Nothing) Then
                pUniqueValueRenderer.AddValue(lo_Tabla.Rows(i).Item(loCampo), "", pFillSymbol)
                Select Case pStyleItem.Name
                    Case "Explotación"
                        pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "Explotación"
                    Case "Exploración"
                        pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "Exploración"
                    Case "Exploración o Explotación"
                        pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "Exploración o Explotación"
                    Case "Cierre"
                        pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "Cierre"
                        'Case "C"
                        '   pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "Media Cautelar"
                        'Case "X"
                        '   pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "Areas Bloqueadas"
                        'Case "X"
                        '   pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "Areas Bloqueadas"
                        'Case "A", "S", "R", "M"
                        '   pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "Cant,Terre, Erizados, Trám.Minero"
                        'Case "B"
                        '   pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "Planta Beneficio"
                        'Case "F"
                        '   pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "Areas Protegidas"

                        'End Select
                        'pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "D.M." & glo_codigou
                End Select
            End If
        Next
        Dim pLayer As IFeatureLayer = Nothing
        Dim pGeoFeatureLayer As IGeoFeatureLayer
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        Dim Afound As Boolean
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name.ToUpper = lo_FeatureClass.ToUpper Then
                pLayer = pMap.Layer(A)
                Afound = True
                Exit For
            End If
        Next A
        If Not Afound Then
            MsgBox("No se encuentra el Layer")
            Exit Sub
        End If
        pGeoFeatureLayer = pLayer
        pGeoFeatureLayer.Renderer = pUniqueValueRenderer
        pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, Nothing)
        pMxDoc.UpdateContents()
    End Sub

    

   
    Public Sub Limpiar_Texto_Pantalla(ByVal p_App As IApplication)
        pMxDoc = p_App.Document
        Dim pElement As IElement
        Dim pGraphicsContainer As IGraphicsContainer
        Dim pGraphicsContainerSel As IGraphicsContainerSelect
        pGraphicsContainer = pMxDoc.ActiveView
        pGraphicsContainerSel = pMxDoc.ActiveView
        pGraphicsContainer.Reset()
        Dim pTextElement As ITextElement
        pElement = pGraphicsContainer.Next
        While Not pElement Is Nothing
            If TypeOf pElement Is ITextElement Then
                pTextElement = pElement
                Dim pElementProps As IElementProperties
                pElementProps = pTextElement
                If pElementProps.Type = "Text" Then
                    pGraphicsContainerSel.SelectElement(pElement)
                    'Borra el texto seleccionado
                    pGraphicsContainer.DeleteElement(pTextElement)
                    pGraphicsContainer.Reset()
                    pTextElement = Nothing
                End If
            End If
            pElement = pGraphicsContainer.Next
        End While
        pMxDoc.ActiveView.Refresh()
    End Sub
    Public Sub Style_Linea_GDB(ByVal p_Layer As String, ByVal loStyle As String, ByVal p_Campo As String, ByVal p_App As IApplication, ByVal p_Conexion As String, Optional ByVal p_TipoCampo As String = "Codigo")
        Dim pTable As ITable
        Select Case p_Conexion
            Case "GDB"
                Conexion_GeoDatabase()
            Case "SDE"
                Conexion_SDE(p_App)
        End Select
        'Conexion_GeoDB()
        pTable = pFeatureWorkspace.OpenTable(p_Layer)
        'pTable = pFeatws.OpenTable(p_Layer)
        Dim pQueryDef As IQueryDef
        Dim pRow As IRow
        Dim pCursor As ICursor
        'Dim pFeatureWorkspace As IFeatureWorkspace
        Dim pDataset As IDataset
        pDataset = pTable
        pFeatureWorkspace = pDataset.Workspace
        pQueryDef = pFeatureWorkspace.CreateQueryDef
        With pQueryDef
            .Tables = pDataset.Name
            .SubFields = "DISTINCT(" & p_Campo & ")"
            pCursor = .Evaluate
        End With
        pRow = pCursor.NextRow
        Dim x As Integer = 0
        Dim valor(100) As String
        Dim pUniqueValueRenderer As IUniqueValueRenderer
        pUniqueValueRenderer = New UniqueValueRenderer
        pUniqueValueRenderer.FieldCount = 1
        pUniqueValueRenderer.Field(0) = p_Campo
        Dim pStyleStorage As IStyleGalleryStorage
        Dim pStyleGallery As IStyleGallery
        'Dim pStyleClass As IStyleGalleryClass
        pStyleGallery = New StyleGallery
        pStyleStorage = pStyleGallery
        Dim pEnumStyleGall As IEnumStyleGalleryItem
        Dim pStyleItem As IStyleGalleryItem
        Dim pFillSymbol As ILineSymbol = Nothing
        Dim pStyleGlry As IStyleGallery
        pStyleGlry = New StyleGallery
        Dim pStylStor As IStyleGalleryStorage
        pStylStor = pStyleGlry
        pStylStor.AddFile(loStyle)
        pEnumStyleGall = pStyleGallery.Items("Line Symbols", loStyle, "Default")
        pEnumStyleGall.Reset()
        pStyleItem = pEnumStyleGall.Next
        Do Until pRow Is Nothing
            If pFillSymbol Is Nothing Then
                pEnumStyleGall.Reset()
                pStyleItem = pEnumStyleGall.Next
            End If
            Do While Not pStyleItem Is Nothing
                pFillSymbol = Nothing
                Select Case p_TipoCampo
                    Case "Codigo"
                        If pStyleItem.ID = pRow.Value(0) Then
                            pFillSymbol = pStyleItem.Item
                            Exit Do
                        End If
                        If pStyleItem.ID > pRow.Value(0) Then
                            pRow = pCursor.NextRow
                            If pRow Is Nothing Then Exit Do
                        Else
                            pStyleItem = pEnumStyleGall.Next
                        End If
                    Case "Nombre"

                        If pStyleItem.Name = pRow.Value(0) Then
                            pFillSymbol = pStyleItem.Item
                            Exit Do
                        End If
                        If pStyleItem.Name > pRow.Value(0) Then
                            pRow = pCursor.NextRow
                            If pRow Is Nothing Then Exit Do
                        Else
                            pStyleItem = pEnumStyleGall.Next
                        End If
                End Select
            Loop
            If pStyleItem Is Nothing Then
            Else
                If Len(pStyleItem.Name) > 0 And Not (pRow Is Nothing) Then
                    pUniqueValueRenderer.AddValue(pRow.Value(0), "", pFillSymbol)
                    pUniqueValueRenderer.Label(pRow.Value(0)) = pStyleItem.Name
                End If
            End If
            pRow = pCursor.NextRow
        Loop
        Dim pMap As IMap
        Dim pLayer As IFeatureLayer = Nothing
        Dim pGeoFeatureLayer As IGeoFeatureLayer
        Dim pMxDoc As IMxDocument
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        Dim aFound As Boolean = False
        Select Case p_Layer
            Case "Malla_17", "Malla_18", "Malla_19"
                p_Layer = Mid(p_Layer, 1, 5)
            Case "GPO_ARE_AREA_RESERVADA_17", "GPO_ARE_AREA_RESERVADA_18", "GPO_ARE_AREA_RESERVADA_19", "DATA_GIS.GPO_ARE_AREA_RESERVADA_G56"
                p_Layer = "Zona Reservada"
            Case "DATA_GIS.GLI_FRO_FRONTERA"
                p_Layer = "Limite Frontera"
        End Select

        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = p_Layer Then
                pLayer = pMxDoc.FocusMap.Layer(A)
                aFound = True
                Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("Error. No existe Layer...")
            Exit Sub
        End If
        pGeoFeatureLayer = pLayer
        pGeoFeatureLayer.Renderer = pUniqueValueRenderer
        pMxDoc.ActiveView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeography, pLayer, Nothing)
        pMxDoc.UpdateContents()
    End Sub

    Public Sub Rotular_texto_DM(ByVal p_Layer As String, ByVal p_Zona As String, ByVal p_App As IApplication)
        Dim fclas_tema As IFeatureClass
        Dim pfeature As IFeature
        Dim lostrEstado As String
        Dim c1 As Long
        Dim c2 As Long
        Try
            pMxDoc = p_App.Document
            pMap = pMxDoc.FocusMap
            Dim pGraphicsContainer As IGraphicsContainer
            Dim pElement As IElement
            Dim pActiveView As IActiveView
            Dim pArea As IArea
            Dim pFeatureCursor As IFeatureCursor = Nothing
            Dim pLayer As IFeatureLayer = Nothing
            Dim aFound As Boolean
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = p_Layer Then
                    pLayer = pMxDoc.FocusMap.Layer(A)
                    aFound = True
                    Exit For
                End If
            Next A
            If Not aFound Then
                MsgBox("No Existe Layer Malla Punto", MsgBoxStyle.Information, "GEOCATMIN")
                Exit Sub
            End If
            fclas_tema = pLayer.FeatureClass
            pActiveView = pMap
            pGraphicsContainer = pMap.BasicGraphicsLayer
            Select Case p_Layer
                Case "Mallap_" & p_Zona
                    pFields = fclas_tema.Fields
                    c1 = pFields.FindField("VALOR")
                    pFields = fclas_tema.Fields
                    c2 = pFields.FindField("VALOR")
                    pFields = fclas_tema.Fields
                    pFeatureCursor = pLayer.Search(Nothing, False)
                    fclas_tema = pLayer.FeatureClass
                Case "gpt_Vertice_DM"
                    pFields = fclas_tema.Fields
                    c1 = pFields.FindField("VERTICE")
                    pFeatureCursor = pLayer.Search(Nothing, False)
                    fclas_tema = pLayer.FeatureClass
            End Select
            Dim contador As String
            pfeature = pFeatureCursor.NextFeature
            Select Case p_Layer
                Case "Mallap_" & p_Zona
                    Do Until pfeature Is Nothing
                        pArea = pfeature.Extent.Envelope
                        lostrEstado = pfeature.Value(c1)
                        contador = pfeature.Value(c2)
                        pElement = MakeATextElement(p_Layer, pArea.Centroid, contador)
                        pGraphicsContainer.AddElement(pElement, 0)
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)
                        pfeature = pFeatureCursor.NextFeature
                    Loop
                Case "gpt_Vertice_DM"
                    Do Until pfeature Is Nothing
                        pArea = pfeature.Extent.Envelope
                        contador = pfeature.Value(c1)
                        pElement = MakeATextElement(p_Layer, pArea.Centroid, contador)
                        pGraphicsContainer.AddElement(pElement, 0)
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)
                        pfeature = pFeatureCursor.NextFeature
                    Loop
            End Select
        Catch ex As Exception
            MsgBox(".::Generando Malla Punto Invalido::.", vbInformation, "GEOCATMIN")
        End Try
    End Sub
    'Public Sub rotulatexto_dm_poligono(ByVal capa As String, ByVal p_app As IApplication)
    Public Sub rotulatexto_dm_poligono(ByVal p_app As IApplication, ByVal x As Integer, ByVal y As Integer)
        'PROGRAMA PARA ROTULAR TEXTOS COORDENADAS DE POLIGONOS
        Try
            Dim pMxDoc As IMxDocument
            pMxDoc = p_app.Document
            Dim pPoint(500) As IPoint
            Dim pPoint1 As IPoint
            Dim pFeatureSelection As IFeatureSelection
            Dim pSelectionSet As ISelectionSet
            Dim pmap As IMap
            Dim pGraphicsContainer As IGraphicsContainer
            Dim pElement As IElement
            Dim pActiveView As IActiveView
            Dim pFeatureCursor As IFeatureCursor
            Dim pMxApp As IMxApplication
            pMxApp = p_app
            Dim pSpatialFilter As ISpatialFilter
            Dim pfeature As IFeature
            pmap = pMxDoc.FocusMap
            Dim pLayer As IFeatureLayer
            pLayer = pMxDoc.SelectedLayer
            pPoint1 = pMxApp.Display.DisplayTransformation.ToMapPoint(x, y)
            pSpatialFilter = New SpatialFilter
            pSpatialFilter.Geometry = pPoint1
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects ' esriSpatialRelIntersects
            pFeatureCursor = pLayer.Search(pSpatialFilter, True)
            pfeature = pFeatureCursor.NextFeature
            If pfeature Is Nothing Then
                MsgBox("No Seleccionó ningun feature de la capa ", vbCritical, "Observacion...")
                Exit Sub
            End If

            Dim pSelectionEnv As ISelectionEnvironment
            Dim intSelMethod As Integer
            pSelectionEnv = pMxApp.SelectionEnvironment
            intSelMethod = pSelectionEnv.CombinationMethod
            pSelectionEnv.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew
            pmap.SelectByShape(pPoint1, pSelectionEnv, False)  ' Para seleccionar varios poligonos interceptados
            pSelectionEnv.CombinationMethod = intSelMethod
            pMxDoc.ActiveView.Refresh()
            pFeatureSelection = pLayer
            pSelectionSet = pFeatureSelection.SelectionSet
            pSelectionSet.Search(Nothing, True, pFeatureCursor)
            pFeatureClass = pLayer.FeatureClass
            pActiveView = pmap
            pGraphicsContainer = pmap.BasicGraphicsLayer
            Dim ptcol As IPointCollection
            Dim j As Integer
            pfeature = pFeatureCursor.NextFeature
            ptcol = pfeature.Shape

            'OBTENIENDO VALORES DE LAS COORDENADAS DE CADA VERTICE
            For j = 0 To ptcol.PointCount - 2
                pPoint(j) = ptcol.Point(j)
                pElement = MakeATextElement1(pPoint(j).X, pPoint(j).Y, j + 1)  ' APLICA FUNCION DE CREAR TEXTO
                '   AGREGA EL ELEMENTO TEXTO AL MAPA
                pGraphicsContainer.AddElement(pElement, 0)
                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)   'refresh
            Next j
        Catch ex As Exception
            MsgBox("Rotulando la capa es Invalido...", MsgBoxStyle.Critical, "BDGEOCATMIN")
        End Try

    End Sub

    Public Function MakeATextElement(ByVal p_Layer As String, ByVal pPoint As IPoint, ByVal texto As String) As IElement
        Dim pRgbColor As IRgbColor
        Dim pTextElement As ITextElement
        Dim pTextSymbol As ITextSymbol
        Dim pElement As IElement
        pRgbColor = New RgbColor
        Dim loint_Tamaño As Integer
        Dim lostr_Font As String = ""
        Select Case Mid(p_Layer, 1, 7)
            Case "Mallap_"
                pRgbColor.RGB = RGB(255, 0, 0)
                loint_Tamaño = 7.25
                lostr_Font = "Times"
            Case "gpt_Ver"
                pRgbColor.RGB = RGB(0, 0, 255)
                loint_Tamaño = 7.5
                lostr_Font = "Arial Narrow"

        End Select
        pTextElement = New TextElement
        pElement = pTextElement
        pElement.Geometry = pPoint
        'FONT DEL TEXTO
        Dim pFontDisp As IFontDisp
        pFontDisp = New stdole.StdFont
        pFontDisp.Name = lostr_Font '"Arial Narrow"
        pFontDisp.Bold = False
        'ASIGNA SIMBOLOGIA
        pTextSymbol = New TextSymbol
        pTextSymbol.Font = pFontDisp
        pTextSymbol.Color = pRgbColor
        pTextSymbol.Size = loint_Tamaño
        pTextElement.Symbol = pTextSymbol
        pTextElement.Text = texto
        MakeATextElement = pTextElement
    End Function

    Public Sub Delete_Rows_FC_GDB(ByVal p_Layer As String)
        Dim pQueryFilter As IQueryFilter
        Try
            Conexion_GeoDatabase()
            Dim pTable As ITable
            pTable = pFeatureWorkspace.OpenTable(p_Layer)
            pQueryFilter = New QueryFilter
            pQueryFilter.WhereClause = "OBJECTID IS NOT NULL"
            pTable.DeleteSearchedRows(pQueryFilter)
        Catch ex As Exception
            MsgBox(".::Error - Eliminado Rows a FeatureClass::.", vbInformation, "GEOCATMIN")
        End Try
    End Sub
    Public Sub Delete_Rows_FC_SHP(ByVal p_Layer As String)
        Dim pQueryFilter As IQueryFilter
        Try
            Conexion_Shapefile()
            Dim pTable As ITable
            pTable = pFeatureWorkspace.OpenTable(p_Layer)
            pQueryFilter = New QueryFilter
            pQueryFilter.WhereClause = "FID IS NOT NULL"
            pTable.DeleteSearchedRows(pQueryFilter)
        Catch ex As Exception
            MsgBox(".::Error - Eliminado Rows a FeatureClass::.", vbInformation, "GEOCATMIN")
        End Try
    End Sub

    Public Sub Load_FC_GDB(ByVal p_FeatureClass As String, ByVal p_Layer As String, ByVal p_App As IApplication, ByVal p_Visible As Boolean)
        Try
            pMxDoc = p_App.Document
            pmap = pMxDoc.FocusMap
            Conexion_GeoDatabase()
            pFeatureClass = pFeatureWorkspace.OpenFeatureClass(p_FeatureClass)
            pFeatureLayer = New FeatureLayerClass
            pFeatureLayer.FeatureClass = pFeatureClass
            Select Case p_FeatureClass
                Case "Malla_17", "Malla_18", "Malla_19"
                    pFeatureLayer.Name = "Malla"
                    pFeatureLayer.Visible = p_Visible
                Case "Catastro"
                    pFeatureLayer.Name = "Catastro_1"
                    pFeatureLayer.Visible = p_Visible
                Case "Punto"
                    pFeatureLayer.Name = "Punto"
                    pFeatureLayer.Visible = p_Visible
                Case "Polylinea"
                    pFeatureLayer.Name = "Polylinea"
                    pFeatureLayer.Visible = p_Visible
                Case Else
                    pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName
            End Select
            pmap.AddLayer(pFeatureLayer) 'pMap.Layer(0).Visible = True
            pMxDoc.ActiveView.Refresh()
        Catch ex As Exception
            MsgBox(".::Error - Cargango FeatureClass::.", vbInformation, "GEOCATMIN")
        End Try
    End Sub
    Public Sub Load_FC_SHP(ByVal p_FeatureClass As String, ByVal p_Layer As String, ByVal p_App As IApplication, ByVal p_Visible As Boolean)
        Try
            pMxDoc = p_App.Document
            pmap = pMxDoc.FocusMap
            Conexion_Shapefile()
            pFeatureClass = pFeatureWorkspace.OpenFeatureClass(p_FeatureClass)
            pFeatureLayer = New FeatureLayerClass
            pFeatureLayer.FeatureClass = pFeatureClass
            If Mid(p_FeatureClass, 1, 8) = "Simulado" Then
                pFeatureLayer.Name = "DM_Simulado"
            End If
            pmap.AddLayer(pFeatureLayer) : pmap.Layer(0).Visible = True
            pMxDoc.ActiveView.Refresh()
        Catch ex As Exception
            MsgBox(".::Error - Cargango ShapeFile::.", vbInformation, "GEOCATMIN")
        End Try
    End Sub

    Public Sub Poligono_Style(ByVal loArchivo As String, _
                                ByVal loCapa As String, _
                                ByVal loStyle As String, _
                                ByVal loCampo As String)
        Dim pTable As ITable
        Dim pFact As IWorkspaceFactory
        Dim pWorkspace As IWorkspace
        Dim pFeatws As IFeatureWorkspace

        pFact = New ESRI.ArcGIS.DataSourcesGDB.AccessWorkspaceFactory
        pWorkspace = pFact.OpenFromFile(loArchivo, 0)

        pFeatws = pWorkspace
        pTable = pFeatws.OpenTable(loCapa)
        Dim pQueryDef As IQueryDef
        Dim pRow As IRow
        Dim pCursor As ICursor
        Dim pFeatureWorkspace As IFeatureWorkspace
        Dim pDataset As IDataset
        pDataset = pTable
        pFeatureWorkspace = pDataset.Workspace
        pQueryDef = pFeatureWorkspace.CreateQueryDef
        With pQueryDef
            .Tables = pDataset.Name
            .SubFields = "DISTINCT(" & loCampo & ")"
            pCursor = .Evaluate
        End With
        pRow = pCursor.NextRow
        Dim x As Integer
        x = 0
        Dim valor(100) As String
        Dim pUniqueValueRenderer As IUniqueValueRenderer
        ' Define the renderer.
        pUniqueValueRenderer = New UniqueValueRenderer
        pUniqueValueRenderer.FieldCount = 1
        pUniqueValueRenderer.Field(0) = loCampo '"CODI"
        Dim pStyleStorage As IStyleGalleryStorage
        Dim pStyleGallery As IStyleGallery
        'Dim pStyleClass As IStyleGalleryClass
        pMxDoc = m_application.Document

        pStyleGallery = New StyleGallery
        pStyleStorage = pStyleGallery
        Dim pEnumStyleGall As IEnumStyleGalleryItem
        Dim pStyleItem As IStyleGalleryItem
        Dim pFillSymbol As IFillSymbol = Nothing
        Dim pStyleGlry As IStyleGallery
        pStyleGlry = New StyleGallery
        Dim pStylStor As IStyleGalleryStorage
        pStylStor = pStyleGlry
        pStylStor.AddFile(loStyle)
        pEnumStyleGall = pStyleGallery.Items("Fill Symbols", loStyle, "Default")
        pEnumStyleGall.Reset()
        pStyleItem = pEnumStyleGall.Next
        Do Until pRow Is Nothing
            If pFillSymbol Is Nothing Then
                pEnumStyleGall.Reset()
                pStyleItem = pEnumStyleGall.Next
                'pStyleItem.Name = pRow.Value(0)
            End If
            Do While Not pStyleItem Is Nothing
                pFillSymbol = Nothing
                If pStyleItem.ID = pRow.Value(0) Then
                    pFillSymbol = pStyleItem.Item
                    Exit Do
                End If
                If pStyleItem.ID > pRow.Value(0) Then
                    pRow = pCursor.NextRow
                    If pRow Is Nothing Then Exit Do
                Else
                    pStyleItem = pEnumStyleGall.Next
                End If
            Loop
            If pStyleItem Is Nothing Then
            Else
                If Len(pStyleItem.Name) > 0 And Not (pRow Is Nothing) Then
                    pUniqueValueRenderer.AddValue(pRow.Value(0), "", pFillSymbol)
                    pUniqueValueRenderer.Label(pRow.Value(0)) = pStyleItem.Name
                End If
            End If
            pRow = pCursor.NextRow
        Loop
        Dim pMap As IMap
        Dim pLayer As IFeatureLayer
        Dim pGeoFeatureLayer As IGeoFeatureLayer
        'pMxDoc = ThisDocument
        'pMap = pMxDoc.FocusMap
        pMxDoc = m_application.Document
        pMap = pMxDoc.FocusMap
        pLayer = pMap.Layer(0)
        Dim afound As Boolean
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = loCapa Then
                pLayer = pMap.Layer(A)
                'pLayer = pMap.Layer(0)
                afound = True
                Exit For
            End If
        Next A
        '        End Select
        If Not afound Then
            MsgBox("Layer No Existe.")
            Exit Sub
        End If
        pGeoFeatureLayer = pLayer
        pGeoFeatureLayer.Renderer = pUniqueValueRenderer
        pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, Nothing)
        pMxDoc.UpdateContents()
    End Sub
    Public Sub Add_ShapeFile(ByVal p_Shapefile As String, ByVal p_App As IApplication)
        Dim pWorkspaceFactory As IWorkspaceFactory
        pWorkspaceFactory = New ShapefileWorkspaceFactory
        Dim pWorkSpace As IFeatureWorkspace
        pWorkSpace = pWorkspaceFactory.OpenFromFile(glo_pathTMP & "\", 0)
        Dim pClass As IFeatureClass
        pClass = pWorkSpace.OpenFeatureClass(p_Shapefile)


        Dim pLayer As IFeatureLayer
        pLayer = New FeatureLayer
        pLayer.FeatureClass = pClass
        pLayer.Name = pClass.AliasName
        'pMxDoc = p_App.Document
        pMxDoc.FocusMap.AddLayer(pLayer)
        Select Case Mid(pLayer.Name, 1, 8)
            Case "Frontera"
                pLayer.Name = "Limite Frontera"
            Case "Libreden"
                pLayer.Name = "Libreden"
            Case Else
                pLayer.Name = "Catastro"
        End Select
        pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, Nothing)
        pMxDoc.UpdateContents()
    End Sub


    Public Sub Expor_Tema(ByVal p_Nombre_Archivo As String, ByVal sele_denu As Boolean, ByVal p_App As IApplication)
        Dim pQueryFilter As IQueryFilter
        Dim tema As IFeatureLayer = Nothing
        Dim fclas_tema As IFeatureClass 'Clase del tema
        Dim pFeatureSelection As IFeatureSelection
        Dim directorio As IWorkspaceFactory  'Directorio de trabajo
        Dim carpeta As IWorkspace            'Carpeta de trabajo
        Dim pDataSetName_In As IDatasetName
        Dim pdataset As IDataset
        Dim pDataSetName_Out As IDatasetName
        Dim pExportOperation As ESRI.ArcGIS.GeoDatabaseUI.IExportOperation
        Dim strPathName As String
        Dim pActiveView As IActiveView
        pMxDoc = p_App.Document
        pmap = pMxDoc.FocusMap
        pActiveView = pmap
        If p_Nombre_Archivo = "Zona Reservada" Then
            'tema = pFeatureLayer_reseg
            tema = pFeatureLayer_rese
        ElseIf p_Nombre_Archivo = "Zona Urbana" Then
            tema = pFeatureLayer
        Else
            tema = pFeatureLayer_cat
        End If
        'tema = pFeatureLayer_cat
        fclas_tema = tema.FeatureClass
        pdataset = fclas_tema
        pDataSetName_In = pdataset.FullName
        pDataSetName_Out = New FeatureClassName
        pExportOperation = New ExportOperation
        pFeatureSelection = tema
        If pFeatureSelection.SelectionSet.Count > 0 Then
            v_existe = True
            strPathName = glo_pathTMP '"D:\BDGEOCATMIN"
            Dim pDataSet_Ws As IDataset
            Dim pWorkSpaceName As IWorkspaceName
            directorio = New ShapefileWorkspaceFactory
            carpeta = directorio.OpenFromFile(strPathName, 0)
            pDataSet_Ws = carpeta
            pWorkSpaceName = pDataSet_Ws.FullName
            If p_Nombre_Archivo = "Zona Reservada" Then
                pDataSetName_Out.Name = p_Nombre_Archivo & fecha_archi
            ElseIf p_Nombre_Archivo = "Zona Urbana" Then
                pDataSetName_Out.Name = p_Nombre_Archivo & fecha_archi
            Else
                pDataSetName_Out.Name = p_Nombre_Archivo
            End If

            'pDataSetName_Out.Name = p_Nombre_Archivo
            pDataSetName_Out.WorkspaceName = pWorkSpaceName
            Dim cls_Usuario As New cls_Usuario
            'cls_Usuario.Activar_Layer_True_False(False, p_App)

            'Select Case sele_denu
            'Case sele_denu = True
            If sele_denu = True Then
                pExportOperation.ExportFeatureClass(pDataSetName_In, Nothing, pFeatureSelection.SelectionSet, Nothing, pDataSetName_Out, 0)
                'Case sele_denu = False
            Else
                pQueryFilter = New QueryFilter
                pQueryFilter.WhereClause = " ESTADO <> 'Y' "
                pExportOperation.ExportFeatureClass(pDataSetName_In, pQueryFilter, pFeatureSelection.SelectionSet, Nothing, pDataSetName_Out, 0)
                'End Select
            End If
       
        End If

        'pDataSetName_Out.Name = p_Nombre_Archivo & fecha_archi3
        'pExportOperation.ExportFeatureClass(pDataSetName_In, "", pFeatureSelection.SelectionSet, Nothing, pDataSetName_Out, 0)
        'Else
        '    If p_Nombre_Archivo = "Zona Reservada" Then
        '        v_existe = True
        '        strPathName = glo_pathTMP '"D:\BDGEOCATMIN"
        '        Dim pDataSet_Ws As IDataset
        '        Dim pWorkSpaceName As IWorkspaceName
        '        directorio = New ShapefileWorkspaceFactory
        '        carpeta = directorio.OpenFromFile(strPathName, 0)
        '        pDataSet_Ws = carpeta
        '        pWorkSpaceName = pDataSet_Ws.FullName
        '        If p_Nombre_Archivo = "Zona Reservada" Then
        '            pDataSetName_Out.Name = p_Nombre_Archivo & fecha_archi
        '        ElseIf p_Nombre_Archivo = "Zona Urbana" Then
        '            pDataSetName_Out.Name = p_Nombre_Archivo & fecha_archi
        '        Else
        '            pDataSetName_Out.Name = p_Nombre_Archivo
        '        End If


        '        pDataSetName_Out.WorkspaceName = pWorkSpaceName

        '        pExportOperation.ExportFeatureClass(pDataSetName_In, Nothing, pFeatureSelection.SelectionSet, Nothing, pDataSetName_Out, 0)

        'End If
        'End If
    End Sub
    Public Sub Exportando_Temas(ByVal seleccion_tema As String, ByVal p_Nombre_Archivo As String, ByVal p_App As IApplication)
        Dim tema As IFeatureLayer = Nothing

        Dim fclas_tema As IFeatureClass 'Clase del tema
        Dim pFeatureSelection As IFeatureSelection
        Dim directorio As IWorkspaceFactory  'Directorio de trabajo
        Dim carpeta As IWorkspace            'Carpeta de trabajo
        Dim pDataSetName_In As IDatasetName
        Dim pdataset As IDataset
        Dim pDataSetName_Out As IDatasetName
        Dim pExportOperation As ESRI.ArcGIS.GeoDatabaseUI.IExportOperation
        Dim strPathName As String
        Dim pActiveView As IActiveView
        loStrShapefile1 = "DM_" & DateTime.Now.Ticks.ToString
        loStrShapefile2 = "Libreden" & DateTime.Now.Ticks.ToString
        Dim loStrShapefile_EX As String
        'loStrShapefile_EX = "Situacion_" & DateTime.Now.Ticks.ToString
        pmap = pMxDoc.FocusMap
        Dim afound As Boolean = False
        pActiveView = pmap
        If seleccion_tema = "DM" Then
            'tema = pFeatureLayer_cat
            If V_caso_simu = "SI" Then
                For A As Integer = 0 To pmap.LayerCount - 1
                    If pmap.Layer(A).Name = p_Nombre_Archivo Then
                        tema = pmap.Layer(A)

                        afound = True
                        Exit For
                    End If
                Next A
                If Not afound Then
                    MsgBox("No se encuentra el Layer")
                    Exit Sub
                End If
            Else
                tema = pFeatureLayer_cat
            End If
        ElseIf seleccion_tema = "Certificacion" Then
            For A As Integer = 0 To pmap.LayerCount - 1
                If pmap.Layer(A).Name = p_Nombre_Archivo Then
                    tema = pmap.Layer(A)

                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If

        ElseIf seleccion_tema = "Zona Reservada" Then
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = p_Nombre_Archivo Then
                    tema = pMap.Layer(A)

                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If

        ElseIf seleccion_tema = "Catastro" Then
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Catastro" Then
                    tema = pMap.Layer(A)

                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If
        ElseIf seleccion_tema = "Situacion" Then
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Catastro" Then
                    tema = pMap.Layer(A)

                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If
        Else
            If p_Nombre_Archivo = "DMxregion" Then
                p_Nombre_Archivo = "Catastro"
           
            End If

            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = p_Nombre_Archivo Then
                    tema = pMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If
        End If
        fclas_tema = tema.FeatureClass
        pdataset = fclas_tema
        pDataSetName_In = pdataset.FullName
        pDataSetName_Out = New FeatureClassName
        pExportOperation = New ExportOperation
        Dim pDataSet_Ws As IDataset
        Dim pWorkSpaceName As IWorkspaceName
        If seleccion_tema <> "Catastro" Then
            pFeatureSelection = tema
            If pFeatureSelection.SelectionSet.Count > 0 Then
                strPathName = glo_pathTMP
                'Dim pDataSet_Ws As IDataset
                'Dim pWorkSpaceName As IWorkspaceName
                directorio = New ShapefileWorkspaceFactory
                carpeta = directorio.OpenFromFile(strPathName, 0)
                pDataSet_Ws = carpeta
                pWorkSpaceName = pDataSet_Ws.FullName
                If arch_cata = "Cata" Then
                    pDataSetName_Out.Name = "DM_" & v_codigo
                ElseIf arch_cata = "Interceccion" Then
                    pDataSetName_Out.Name = "Prioritarios" & v_codigo
                ElseIf arch_cata = "union" Then
                    pDataSetName_Out.Name = "Areadispo_" & v_codigo & fecha_archi_sup
                ElseIf arch_cata = "DMxregion" Then
                    pDataSetName_Out.Name = loStrShapefile1
                ElseIf arch_cata = "Redenuncio" Then
                    pDataSetName_Out.Name = "Antecesor"
                ElseIf arch_cata = "Certificacion Ambiental" Then
                    pDataSetName_Out.Name = "Certificacion" & fecha_archi3
                ElseIf arch_cata = "Libreden" Then
                    pDataSetName_Out.Name = loStrShapefile2
                ElseIf arch_cata = "Zona Reservada" Then
                    pDataSetName_Out.Name = loStrShapefile2

                Else
                    If p_Nombre_Archivo = "Situacion" Then
                        pDataSetName_Out.Name = "Situacion_" & fecha_dm_ex
                    Else
                        pDataSetName_Out.Name = p_Nombre_Archivo
                    End If
                End If
                pDataSetName_Out.WorkspaceName = pWorkSpaceName
                pExportOperation.ExportFeatureClass(pDataSetName_In, Nothing, pFeatureSelection.SelectionSet, Nothing, pDataSetName_Out, 0)
               

            End If
        Else
            strPathName = glo_pathTMP
            pFeatureSelection = tema
            'Dim pDataSet_Ws As IDataset
            'Dim pWorkSpaceName As IWorkspaceName
            directorio = New ShapefileWorkspaceFactory
            carpeta = directorio.OpenFromFile(strPathName, 0)
            pDataSet_Ws = carpeta
            pWorkSpaceName = pDataSet_Ws.FullName
            pDataSetName_Out.Name = p_Nombre_Archivo

            pDataSetName_Out.WorkspaceName = pWorkSpaceName
            pExportOperation.ExportFeatureClass(pDataSetName_In, Nothing, pFeatureSelection, Nothing, pDataSetName_Out, 0)

        End If

    End Sub


    Public Sub ShowLabel_DM(ByVal p_Layer As String, ByVal p_App As IApplication)
        Dim pGeoFeatureL As IGeoFeatureLayer = Nothing
        Dim pLineLabelP As ILineLabelPosition
        Dim pLabelEngineLP As ILabelEngineLayerProperties
        Dim pAnnotateLayerP As IAnnotateLayerProperties

        pMxDoc = p_App.Document
        Dim afound As Boolean = False
        For A As Integer = 0 To pmap.LayerCount - 1
            If pmap.Layer(A).Name = p_Layer Then '"Catastro" Then
                pGeoFeatureL = pMxDoc.FocusMap.Layer(A)
                pGeoFeatureL.ShowTips = True
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra el Layer")
            Exit Sub
        End If
        'pGeoFeatureL = pMxDoc.FocusMap.Layer(0)
        pGeoFeatureL.AnnotationProperties.Clear()

        pLineLabelP = New LineLabelPosition
        With pLineLabelP
            .Above = False
            .AtEnd = False
            .Below = False
            .Horizontal = False
            .InLine = True
            .OnTop = True
            .Parallel = True
            .ProduceCurvedLabels = True


        End With
        pLabelEngineLP = New LabelEngineLayerProperties
        Select Case p_Layer
            Case "Catastro"
                With pLabelEngineLP
                    .Symbol = New TextSymbol
                    .IsExpressionSimple = False
                    '.Expression = "Function FindLabel ( [CODIGOU])" _
                    '    & "FindLabel = [CODIGOU]" _
                    '    & " End Function"

                    .Expression = "Function FindLabel ( [CONTADOR] , [LEYENDA] ) " & vbNewLine & _
                    "IF [LEYENDA] = " & Chr(34) & "G1" & Chr(34) & " Then " & vbNewLine & _
                    "FindLabel = " & Chr(34) & "<CLR red='56' green='168' blue='0'> <FNT name='Arial Narrow ' size='7'> <BOL> " & Chr(34) & " & [CONTADOR]  & " & Chr(34) & "</BOL> </FNT> </CLR>" & Chr(34) & vbNewLine & _
                    " ELSEIF [LEYENDA] = " & Chr(34) & "G2" & Chr(34) & " Then " & vbNewLine & _
                    "FindLabel = " & Chr(34) & "<CLR red='230' green='0' blue='0'> <FNT name='Arial Narrow ' size='7'> <BOL> " & Chr(34) & " & [CONTADOR]  & " & Chr(34) & "</BOL> </FNT> </CLR>" & Chr(34) & vbNewLine & _
                    " ELSEIF [LEYENDA] = " & Chr(34) & "G3" & Chr(34) & " Then " & vbNewLine & _
                    "FindLabel = " & Chr(34) & "<CLR red='0' green='77' blue='168'> <FNT name='Arial Narrow ' size='7'> <BOL> " & Chr(34) & " & [CONTADOR]  & " & Chr(34) & "</BOL> </FNT> </CLR>" & Chr(34) & vbNewLine & _
                    " ELSEIF [LEYENDA] = " & Chr(34) & "G4" & Chr(34) & " Then " & vbNewLine & _
                    "FindLabel = " & Chr(34) & "<CLR red='0' green='0' blue='0'> <FNT name='Arial Narrow ' size='7'> <BOL> " & Chr(34) & " & [CONTADOR]  & " & Chr(34) & "</BOL> </FNT> </CLR>" & Chr(34) & vbNewLine & _
                    " ELSEIF [LEYENDA] = " & Chr(34) & "G5" & Chr(34) & " Then " & vbNewLine & _
                    "FindLabel = " & Chr(34) & "<CLR red='0' green='235' blue='223'> <FNT name='Arial Narrow ' size='7'> <BOL> " & Chr(34) & " & [CONTADOR]  & " & Chr(34) & "</BOL> </FNT> </CLR>" & Chr(34) & vbNewLine & _
                    " END IF " & "End Function"
                    '        .IsExpressionSimple = True
                    '        .Expression = "[ZONENAME27]"
                    .BasicOverposterLayerProperties.LineLabelPosition = pLineLabelP
                End With
            Case "Departamento"
                With pLabelEngineLP
                    .Symbol = New TextSymbol
                    .IsExpressionSimple = False
                    '.Expression = "Function FindLabel ( [NM_DIST])"
                    .Expression = "Function FindLabel ( [NM_DEPA]) " & vbNewLine & _
                    "FindLabel = " & Chr(34) & "<CLR red='0' green='0' blue='0'> <FNT name='Arial Narrow ' size='12'> <BOL> " & Chr(34) & " & [NM_DEPA] & " & Chr(34) & "</BOL> </FNT> </CLR>" & Chr(34) & vbNewLine & _
                    " End Function"
                    .BasicOverposterLayerProperties.LineLabelPosition = pLineLabelP
                End With
            Case "Provincia"
                With pLabelEngineLP
                    .Symbol = New TextSymbol
                    .IsExpressionSimple = False
                    '.Expression = "Function FindLabel ( [NM_DIST])"
                    .Expression = "Function FindLabel ( [NM_PROV]) " & vbNewLine & _
                    "FindLabel = " & Chr(34) & "<CLR red='52' green='52' blue='52'> <FNT name='Arial Narrow ' size='9'> <BOL> " & Chr(34) & " & [NM_PROV] & " & Chr(34) & "</BOL> </FNT> </CLR>" & Chr(34) & vbNewLine & _
                    " End Function"
                    .BasicOverposterLayerProperties.LineLabelPosition = pLineLabelP
                End With
            Case "Distrito"
                With pLabelEngineLP
                    .Symbol = New TextSymbol
                    .IsExpressionSimple = False
                    '.Expression = "Function FindLabel ( [NM_DIST])"
                    .Expression = "Function FindLabel ( [NM_DIST]) " & vbNewLine & _
                    "FindLabel = " & Chr(34) & "<CLR red='78' green='78' blue='78'> <FNT name='Arial Narrow ' size='7'> <BOL> " & Chr(34) & " & [NM_DIST] & " & Chr(34) & "</BOL> </FNT> </CLR>" & Chr(34) & vbNewLine & _
                    " End Function"
                    .BasicOverposterLayerProperties.LineLabelPosition = pLineLabelP
                End With
            Case "AreaReserva_100Ha"
                With pLabelEngineLP
                    .Symbol = New TextSymbol
                    .IsExpressionSimple = False
                    '.Expression = "Function FindLabel ( [NM_DIST])"
                    '.Expression = "Function FindLabel ( [CODIGOU]) " & vbNewLine & _
                    .Expression = "Function FindLabel ( [OBJECTID]) " & vbNewLine & _
                    "FindLabel = " & Chr(34) & "<CLR red='255' green='0' blue='0'> <FNT name='Arial Narrow ' size='8'> <BOL> " & Chr(34) & " & [OBJECTID] & " & Chr(34) & "</BOL> </FNT> </CLR>" & Chr(34) & vbNewLine & _
                    " End Function"
                    '.Expression = "Function FindLabel ( [CODIGOU])" _
                    '   & "FindLabel = [CODIGOU]" _
                    '  & " End Function"
                    .BasicOverposterLayerProperties.LineLabelPosition = pLineLabelP
                    '.IsExpressionSimple = True
                End With
            Case "Cuadriculas"
                With pLabelEngineLP
                    .Symbol = New TextSymbol
                    .IsExpressionSimple = False
                    '.Expression = "Function FindLabel ( [NM_DIST])"
                    '.Expression = "Function FindLabel ( [CODIGOU]) " & vbNewLine & _
                    .Expression = "Function FindLabel ( [CODIGOU]) " & vbNewLine & _
                    "FindLabel = " & Chr(34) & "<CLR red='255' green='0' blue='0'> <FNT name='Arial Narrow ' size='8'> <BOL> " & Chr(34) & " & [CODIGOU] & " & Chr(34) & "</BOL> </FNT> </CLR>" & Chr(34) & vbNewLine & _
                    " End Function"
                    '.Expression = "Function FindLabel ( [CODIGOU])" _
                    '   & "FindLabel = [CODIGOU]" _
                    '  & " End Function"
                    .BasicOverposterLayerProperties.LineLabelPosition = pLineLabelP
                    '.IsExpressionSimple = True
                End With


            Case "gpt_Vertice_DM"
                With pLabelEngineLP
                    .Symbol = New TextSymbol
                    .IsExpressionSimple = False
                    '.Expression = "Function FindLabel ( [NM_DIST])"
                    .Expression = "Function FindLabel ( [OBJECTID]) " & vbNewLine & _
                    "FindLabel = " & Chr(34) & "<CLR red='0' green='0' blue='0'> <FNT name='Arial Narrow ' size='12'> <BOL> " & Chr(34) & " & [NM_DIST] & " & Chr(34) & "</BOL> </FNT> </CLR>" & Chr(34) & vbNewLine & _
                    " End Function"
                    .BasicOverposterLayerProperties.LineLabelPosition = pLineLabelP
                End With
            Case "Zona Reservada"

                With pLabelEngineLP
                    .Symbol = New TextSymbol
                    .IsExpressionSimple = False
                    '.Expression = "Function FindLabel ( [NM_DIST])"
                    .Expression = "Function FindLabel ( [NM_RESE]) " & vbNewLine & _
                    "FindLabel = " & Chr(34) & "<CLR red='255' green='0' blue='0'> <FNT name='Arial Narrow ' size='8'> <BOL> " & Chr(34) & " & [NM_RESE] & " & Chr(34) & "</BOL> </FNT> </CLR>" & Chr(34) & vbNewLine & _
                    " End Function"
                    .BasicOverposterLayerProperties.LineLabelPosition = pLineLabelP
                End With



        End Select
        pAnnotateLayerP = pLabelEngineLP
        With pAnnotateLayerP
            .DisplayAnnotation = True
            .FeatureLayer = pGeoFeatureL
            .LabelWhichFeatures = esriLabelWhichFeatures.esriVisibleFeatures ' esriVisibleFeatures
            .WhereClause = ""
        End With
        pGeoFeatureL.AnnotationProperties.Add(pLabelEngineLP)
        pGeoFeatureL.DisplayAnnotation = True
        pMxDoc.ActiveView.Refresh()

    End Sub
    Public Sub Selecccionar_Objeto(ByVal p_Layer As String, ByVal p_Filtro As String, ByVal p_App As IApplication, ByVal Xmin As Object, ByVal yMin As Object, ByVal xMax As Object, ByVal yMax As Object, ByVal lostr_OutShapeFile As String)
        'Dim loStrShapefile As String = "Catastro" & DateTime.Now.Ticks.ToString()
        Dim pActiveView As IActiveView
        pMxDoc = p_App.Document
        pmap = pMxDoc.FocusMap
        pActiveView = pmap
        Dim afound As Boolean = False
        For A As Integer = 0 To pmap.LayerCount - 1
            If pmap.Layer(A).Name = p_Layer Then
                pFeatureLayer = pmap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra el Layer")
            Exit Sub
        End If
        ' Part 2: Select features.
        Dim pFeatureSelection As IFeatureSelection = pFeatureLayer
        If Not pFeatureSelection Is Nothing Then
            Dim pQueryFilter As IQueryFilter
            ' Prepare a query filter.
            pQueryFilter = New QueryFilter
            pQueryFilter.WhereClause = p_Filtro ' "QDR_CODIGO_ALFANUMERICO = '" & p_Codigo_Carta.ToLower & "'"
            ' Refresh or erase any previous selection.
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        End If
        ' Refresh again to draw the new selection.
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        Dim pFSel As IFeatureSelection
        pFSel = pFeatureLayer
        Dim pSelSet As ISelectionSet
        If pFSel.SelectionSet.Count = 0 Then
            MsgBox("No hay ninguna Selección")
            Exit Sub
        End If
        pSelSet = pFSel.SelectionSet
        Dim pEnumGeom As IEnumGeometry
        Dim pEnumGeomBind As IEnumGeometryBind
        pEnumGeom = New EnumFeatureGeometry
        pEnumGeomBind = pEnumGeom
        pEnumGeomBind.BindGeometrySource(Nothing, pSelSet)
        Dim pGeomFactory As IGeometryFactory
        pGeomFactory = New GeometryEnvironment
        Dim pGeom As IGeometry
        pGeom = pGeomFactory.CreateGeometryFromEnumerator(pEnumGeom)
        pMxDoc.ActiveView.Extent = pGeom.Envelope
        Xmin.text = pGeom.Envelope.XMin
        yMin.text = pGeom.Envelope.YMin
        xMax.text = pGeom.Envelope.XMax
        yMax.text = pGeom.Envelope.YMax
        Dim lo_Filtro As String = f_Intercepta_Envelope("Catastro", pGeom.Envelope, p_App)
        DefinitionExpression(lo_Filtro, p_App, lostr_OutShapeFile)
        pMxDoc.ActiveView.Refresh()
    End Sub

    Function f_Intercepta_FC(ByVal loFeature As String, ByVal xMin As Double, ByVal yMin As Double, ByVal xMax As Double, ByVal yMax As Double, ByVal p_App As IApplication, Optional ByVal p_ShapeFile_Out As String = "")

        Dim pFLayer As IFeatureLayer = Nothing
        Dim lostr_Join_Codigos As String = ""

        pMxDoc = p_App.Document
        pmap = pMxDoc.FocusMap
        If loFeature = "Distrito" Then
            pFLayer = pFeatureLayer_dist
        ElseIf loFeature = "Zona Reservada" Then
            'pFLayer = pFeatureLayer_reseg
            pFLayer = pFeatureLayer_rese
        ElseIf loFeature = "Zona Urbana" Then
            pFLayer = pFeatureLayer_urba
        ElseIf loFeature = "Cuadrangulo" Then
            pFLayer = pFeatureLayer_hoja
        ElseIf loFeature = "Departamento" Then
            pFLayer = pFeatureLayer_depa
        ElseIf loFeature = "Provincia" Then
            pFLayer = pFeatureLayer_prov
        ElseIf loFeature = "Catastro" Then
            pFLayer = pFeatureLayer_cat
        ElseIf loFeature = "Capitales Distritales" Then
            pFLayer = pFeatureLayer_capdist
        ElseIf loFeature = "Zona Traslape" Then
            pFLayer = pFeatureLayer_tras
        ElseIf loFeature = "Certificacion Ambiental" Then
            pFLayer = pFeatureLayer_certi
        ElseIf loFeature = "Red_Hidrografica" Then
            pFLayer = pFeatureLayer_rio
        ElseIf loFeature = "Centros Poblados" Then
            pFLayer = pFeatureLayer_ccpp
        ElseIf loFeature = "DM_Uso_Minero" Then
            pFLayer = pFeatureLayer_usomin
        ElseIf loFeature = "DM_Actividad_Minera" Then
            pFLayer = pFeatureLayer_Actmin
        End If
        Dim pActiveView As IActiveView
        Dim pDisplayTransform As IDisplayTransformation
        Dim pEnvelope As IEnvelope
        'pMxDoc = p_App.Document
        pActiveView = pMxDoc.FocusMap
        pDisplayTransform = pActiveView.ScreenDisplay.DisplayTransformation
        pEnvelope = pDisplayTransform.VisibleBounds
        'Aquí calculo el Extend
        pEnvelope.SetEmpty()
        pEnvelope.XMin = xMin
        pEnvelope.YMin = yMin
        pEnvelope.XMax = xMax
        pEnvelope.YMax = yMax
        pDisplayTransform.VisibleBounds = pEnvelope
        'pActiveView.Refresh()
        Dim pSpatialFilter As ISpatialFilter

        Dim pFeatSelection As IFeatureSelection

        pSpatialFilter = New SpatialFilter
        With pSpatialFilter
            .Geometry = pEnvelope
            .SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects ' esriSpatialRelIntersects
        End With
        '******************
        'Seleccionar los Registros Interceptados
        'Dim pFeatSelection As IFeatureSelection
        Dim pSelectionSet As ISelectionSet
        pFeatSelection = pFLayer
        pFeatSelection.SelectFeatures(pSpatialFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        pSelectionSet = pFeatSelection.SelectionSet

        If pSelectionSet.Count = 0 Then
            If loFeature = "Certificacion Ambiental" Then
                canti_capa_certi = 0
            ElseIf loFeature = "DM_Uso_Minero" Then

                canti_capa_usomin = 0
            ElseIf loFeature = "DM_Actividad_Minera" Then
                canti_capa_actmin = 0

            End If
            v_existe = False : Exit Function

        End If
        If loFeature = "Catastro" Then
            ' Exit Function
            If listado_dm_eli <> "" Then
                Dim pQueryFilter As IQueryFilter
                pQueryFilter = New QueryFilter
                pQueryFilter.WhereClause = listado_dm_eli
                pFeatSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultSubtract, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, Nothing, Nothing)
                pSelectionSet = pFeatSelection.SelectionSet
            End If

            'aqui
            Expor_Tema(p_ShapeFile_Out, sele_denu, p_App)
        Else

            If loFeature = "DM_Uso_Minero" Then
                canti_capa_usomin = pFeatSelection.SelectionSet.Count
            ElseIf loFeature = "DM_Actividad_Minera" Then
                canti_capa_actmin = 0
                canti_capa_actmin = pFeatSelection.SelectionSet.Count
            End If

            Dim pFeatureCursor As IFeatureCursor
            pFeatureCursor = pFLayer.Search(pSpatialFilter, True)
            Dim pRow As IRow
            pRow = pFeatureCursor.NextFeature
            If pRow Is Nothing Then
                'MsgBox("No Existen D.M. en este tipo de busqueda", MsgBoxStyle.Information, "Observación...")
                Return ""
                v_existe = False
                Exit Function
            End If
            v_existe = True
            Do Until pRow Is Nothing
                If pRow.Value(1).ToString <> "" Then
                    Select Case loFeature
                        Case "Catastro"
                            lostr_Join_Codigos = lostr_Join_Codigos & pRow.Value(pRow.Fields.FindField("OBJECTID")) & ","
                        Case "Departamento"
                            If (pRow.Value(pRow.Fields.FindField("NM_DEPA")) = "MAR") Then
                            ElseIf (pRow.Value(pRow.Fields.FindField("NM_DEPA")) = "FUERA DEL PERU") Then
                            Else
                                lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("NM_DEPA")) & "',"
                            End If
                        Case "Provincia"
                            If (pRow.Value(pRow.Fields.FindField("CD_PROV")) = "9901") Then
                            ElseIf (pRow.Value(pRow.Fields.FindField("CD_PROV")) = "9903") Then
                            Else
                                lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("OBJECTID")) & "',"
                            End If
                        Case "Distrito"
                            If caso_consulta = "CARTA IGN" Or caso_consulta = "DEMARCACION POLITICA" Then
                                lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("OBJECTID")) & "',"
                            Else
                                If (pRow.Value(pRow.Fields.FindField("CD_DIST")) = "990101") Then
                                ElseIf (pRow.Value(pRow.Fields.FindField("CD_DIST")) = "990301") Then
                                Else
                                    lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("OBJECTID")) & "',"
                                End If
                            End If
                        Case "Zona Urbana"
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("NOMBRE")) & "',"
                        Case "Zona Reservada"
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("NM_RESE")) & "',"
                        Case "Cuadrangulo"
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("CD_HOJA")) & "',"
                        Case "Limite Frontera"
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("CODIGO")) & "',"
                        Case "Zona Traslape"
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(2) & "',"
                        Case "Limite de Zona"
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("ZONA")) & "',"
                        Case "Capitales Distritales"
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("DISTRITO")) & "',"
                        Case "Red_Hidrografica"
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("CD_DEPA")) & "',"
                        Case "Certificacion Ambiental"

                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("ID_RECURSO")) & "',"
                        Case "DM_Uso_Minero"

                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("CODIGO")) & "',"
                        Case "DM_Actividad_Minera"

                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("CODIGO")) & "',"
                            lostr_Join_Codigos_AREA = pRow.Value(pRow.Fields.FindField("ANOPRO"))
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
                    lostr_Join_Codigos = "NOMBRE IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                Case "Zona Reservada"
                    lostr_Join_Codigos = "NM_RESE IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                Case "Cuadrangulo"
                    lostr_Join_Codigos = lostr_Join_Codigos
                Case "Zona Traslape"
                    lostr_Join_Codigos = "DESCRIP IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                Case "Limite de Zona"
                    lostr_Join_Codigos = lostr_Join_Codigos
                Case "Capitales Distritales"
                    lostr_Join_Codigos = "DISTRITO IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                Case "Red_Hidrografica"
                    lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("CD_DEPA")) & "',"
                Case "Certificacion Ambiental"

                    lostr_Join_Codigos = "ID_RECURSO (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                Case "DM_Uso_Minero"

                    lostr_Join_Codigos = "CODIGO (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
                Case "DM_Actividad_Minera"

                    lostr_Join_Codigos = "CODIGO (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"


                Case Else
                    lostr_Join_Codigos = "OBJECTID IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
            End Select
            End If
        Return lostr_Join_Codigos


    End Function

    Public Sub UpdateValue_Dema_multiple(ByVal lo_Filtro As String, ByVal p_App As IApplication)
        Dim lodtbDemarca As New DataTable
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim pFeatureClass As IFeatureClass
        pMxDoc = p_App.Document
        Dim afound As Boolean = False
        Dim parametro As Integer = 0
        Dim valor1 As Integer = 0

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
        pFeatureClass = pFeatLayer.FeatureClass
        Dim pQFilter As IQueryFilter
        Dim pUpdateFeatures As IFeatureCursor

        'Dim fecha As String
        'Dim MyDate As Date
        'MyDate = Now
        'fecha = RellenarComodin(MyDate.Year, 2, "0") & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & MyDate.Day
        'v_fec_denun = fecha & " 00:00"
        'v_hor_denun = DateTime.Now.Hour & ":" & (DateTime.Now.Minute) & ":" & (DateTime.Now.Second)
        ' Prepare a query filter.
        pQFilter = New QueryFilter
        pUpdateFeatures = pFeatureClass.Update(pQFilter, False)
        Dim pFeature As IFeature
        pFeature = pUpdateFeatures.NextFeature
        Dim contador As Integer = 0
        Dim conta As Integer
        Dim lostr_Join_Codigos As String
        Dim valor As String

        Try
            Do Until pFeature Is Nothing
                contador = contador + 1
                pFeature.Value(pUpdateFeatures.FindField("CONTADOR")) = contador
                conta = Len(pFeature.Value(pFeature.Fields.FindField("DEMAGIS")))
                If conta = 6 Then
                    parametro = 1
                ElseIf conta = 13 Then
                    parametro = 2
                ElseIf conta = 20 Then
                    parametro = 3
                ElseIf conta = 27 Then
                    parametro = 4
                ElseIf conta = 34 Then
                    parametro = 5
                ElseIf conta = 41 Then
                    parametro = 6
                ElseIf conta = 48 Then
                    parametro = 7
                ElseIf conta = 55 Then
                    parametro = 8
                ElseIf conta = 62 Then
                    parametro = 9
                ElseIf conta = 69 Then
                    parametro = 10
                End If
                If parametro = 1 Then
                    valor = Mid(pFeature.Value(pFeature.Fields.FindField("DEMAGIS")), 1, 6)
                    lostr_Join_Codigos = ""
                    lostr_Join_Codigos = "'" & valor & "',"

                ElseIf parametro > 1 Then
                    lostr_Join_Codigos = ""
                    For k As Integer = 1 To parametro
                        If k = 1 Then
                            valor = Mid(pFeature.Value(pFeature.Fields.FindField("DEMAGIS")), 1, 6)
                            lostr_Join_Codigos = "'" & valor & "',"

                        Else
                            valor1 = 1
                            valor = Mid(pFeature.Value(pFeature.Fields.FindField("DEMAGIS")), valor1 + 7, 6)
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & valor & "',"

                        End If
                    Next k
                End If

                lostr_Join_Codigos = Left(lostr_Join_Codigos, (Len(lostr_Join_Codigos) - 1))
                'Dim dRow1 As DataRow
                Dim lista_dists As String
                Dim lista_provs As String
                Dim lista_depas As String
                Dim nm_dists As String
                Dim nm_provs As String
                Dim nm_depas As String
                Dim nm_depas1 As String
                Dim nm_dists1 As String
                Dim nm_provs1 As String
                'lostr_Join_Codigos = "'090409','090404','090404'"
                lodtbDemarca = cls_Oracle.F_Obtiene_Datos_UBIGEO1(lostr_Join_Codigos)
                If lodtbDemarca.Rows.Count > 0 Then
                    For r As Integer = 0 To lodtbDemarca.Rows.Count - 1
                        nm_depas = lodtbDemarca.Rows(r).Item("DPTO")
                        nm_provs = lodtbDemarca.Rows(r).Item("PROV")
                        nm_dists = lodtbDemarca.Rows(r).Item("DIST")
                        If r = 0 Then
                            lista_depas = nm_depas
                            nm_depas1 = nm_depas
                        ElseIf r > 0 Then
                            If nm_depas <> nm_depas1 Then
                                lista_depas = lista_depas & "," & nm_depas
                                nm_depas1 = nm_depas
                            End If
                        End If

                        'nm_provs = lodtbDemarca.Rows(r).Item("PROV")
                        If r = 0 Then
                            lista_provs = nm_provs
                            nm_provs1 = nm_provs
                        ElseIf r > 0 Then
                            If nm_provs1 <> nm_provs Then
                                lista_provs = lista_provs & "," & nm_provs
                                nm_provs1 = nm_provs
                            End If
                        End If

                        ' nm_dists = lodtbDemarca.Rows(r).Item("DIST")
                        If r = 0 Then
                            lista_dists = nm_dists
                            nm_dists1 = nm_dists
                        ElseIf r > 0 Then
                            If nm_dists <> nm_dists1 Then
                                lista_dists = lista_dists & "," & nm_dists
                                nm_dists1 = nm_dists
                            End If
                        End If

                    Next r


                    pFeature.Value(pUpdateFeatures.FindField("DPTOS")) = lista_depas
                    pFeature.Value(pUpdateFeatures.FindField("PROVS")) = lista_provs
                    pFeature.Value(pUpdateFeatures.FindField("DISTS")) = lista_dists

                End If


                pUpdateFeatures.UpdateFeature(pFeature)
                pFeature = pUpdateFeatures.NextFeature
            Loop


        Catch ex As Exception
            MsgBox("ERROR UPDATE_VALUE1")
        End Try
    End Sub
    Public Sub UpdateValuemejo_bakup(ByVal lo_Filtro As String, ByVal p_App As IApplication, Optional ByVal p_Codigo As String = "")
        Dim lodtbDemarca As New DataTable
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim pFeatureClass As IFeatureClass
        pMxDoc = p_App.Document
        Dim afound As Boolean = False
        Dim parametro As Integer = 0
        Dim valor1 As Integer = 0

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
        pFeatureClass = pFeatLayer.FeatureClass
        Dim pQFilter As IQueryFilter
        Dim pUpdateFeatures As IFeatureCursor

        Dim fecha As String
        Dim MyDate As Date
        MyDate = Now
        fecha = RellenarComodin(MyDate.Year, 2, "0") & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & MyDate.Day
        v_fec_denun = fecha & " 00:00"
        v_hor_denun = DateTime.Now.Hour & ":" & (DateTime.Now.Minute) & ":" & (DateTime.Now.Second)
        ' Prepare a query filter.
        pQFilter = New QueryFilter
        pUpdateFeatures = pFeatureClass.Update(pQFilter, False)
        Dim pFeature As IFeature
        pFeature = pUpdateFeatures.NextFeature
        Dim contador As Integer = 0
        Dim conta As Integer
        Dim lostr_Join_Codigos As String
        Dim valor As String

        Try
            Do Until pFeature Is Nothing
                contador = contador + 1
                pFeature.Value(pUpdateFeatures.FindField("CONTADOR")) = contador
                Select Case pFeature.Value(pUpdateFeatures.FindField("ESTADO"))
                    Case "P" ', "K"
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = "G1" ' Petitorio Tramite
                    Case "D"
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = "G2" ' Denuncio Tramite
                    Case "E", "N", "Q", "T"
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = "G3"  'Denuncios Titulados
                    Case "C", "F", "J", "L", "Y", "9", "X"
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = "G4"  'Denuncios Extinguidos
                    Case "A", "B", "S", "M", "G", "R", "Z", "K"
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = "G5"  'Otros
                    Case Else
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = ""
                        pFeature.Value(pUpdateFeatures.FindField("EVAL")) = "EV"
                        pFeature.Value(pUpdateFeatures.FindField("TIPO_EX")) = "PE"
                        pFeature.Value(pUpdateFeatures.FindField("ESTADO")) = "P"
                        pFeature.Value(pUpdateFeatures.FindField("CONCESION")) = "Dm_Simulado"
                        pFeature.Value(pUpdateFeatures.FindField("FEC_DENU")) = v_fec_denun
                        pFeature.Value(pUpdateFeatures.FindField("HOR_DENU")) = v_hor_denun
                        pFeature.Value(pUpdateFeatures.FindField("CODIGOU")) = v_codigo
                        pFeature.Value(pUpdateFeatures.FindField("ZONA")) = v_zona_dm
                        pFeature.Value(pUpdateFeatures.FindField("CARTA")) = "CARTA"
                End Select

                ' lodtbDemarca = cls_Oracle.F_Obtiene_Datos_UBIGEO(Mid(pFeature.Value(pFeature.Fields.FindField("DEMAGIS")), 1, 6), gstrUsuarioAcceso, gstrUsuarioClave, gstrDatabase)
                'If lodtbDemarca.Rows.Count > 0 Then
                '    pFeature.Value(pUpdateFeatures.FindField("DPTO")) = lodtbDemarca.Rows(0).Item("DPTO")
                '    pFeature.Value(pUpdateFeatures.FindField("PROV")) = lodtbDemarca.Rows(0).Item("PROV")
                '    pFeature.Value(pUpdateFeatures.FindField("DIST")) = lodtbDemarca.Rows(0).Item("DIST")
                'End If

                'Dim conta As Integer
                'Dim lostr_Join_Codigos As String
                'Dim valor As String
                'Dim parametro As Integer = 0
                conta = Len(pFeature.Value(pFeature.Fields.FindField("DEMAGIS")))
                If conta = 6 Then
                    parametro = 1
                ElseIf conta = 13 Then
                    parametro = 2
                ElseIf conta = 20 Then
                    parametro = 3
                ElseIf conta = 27 Then
                    parametro = 4
                ElseIf conta = 34 Then
                    parametro = 5
                ElseIf conta = 41 Then
                    parametro = 6
                ElseIf conta = 48 Then
                    parametro = 7
                ElseIf conta = 55 Then
                    parametro = 8
                ElseIf conta = 62 Then
                    parametro = 9
                ElseIf conta = 69 Then
                    parametro = 10
                End If
                If parametro = 1 Then
                    valor = Mid(pFeature.Value(pFeature.Fields.FindField("DEMAGIS")), 1, 6)
                    lostr_Join_Codigos = ""
                    lostr_Join_Codigos = "'" & valor & "',"

                ElseIf parametro > 1 Then
                    lostr_Join_Codigos = ""
                    For k As Integer = 1 To parametro
                        If k = 1 Then
                            valor = Mid(pFeature.Value(pFeature.Fields.FindField("DEMAGIS")), 1, 6)
                            lostr_Join_Codigos = "'" & valor & "',"

                        Else
                            valor1 = 1
                            valor = Mid(pFeature.Value(pFeature.Fields.FindField("DEMAGIS")), valor1 + 7, 6)
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & valor & "',"

                        End If
                    Next k
                End If

                lostr_Join_Codigos = Left(lostr_Join_Codigos, (Len(lostr_Join_Codigos) - 1))
                'Dim dRow1 As DataRow
                Dim lista_dists As String
                Dim lista_provs As String
                Dim lista_depas As String
                Dim nm_dists As String
                Dim nm_provs As String
                Dim nm_depas As String
                Dim nm_depas1 As String
                Dim nm_dists1 As String
                Dim nm_provs1 As String
                'lostr_Join_Codigos = "'090409','090404','090404'"
                lodtbDemarca = cls_Oracle.F_Obtiene_Datos_UBIGEO1(lostr_Join_Codigos)
                If lodtbDemarca.Rows.Count > 0 Then
                    For r As Integer = 0 To lodtbDemarca.Rows.Count - 1
                        nm_depas = lodtbDemarca.Rows(r).Item("DPTO")
                        nm_provs = lodtbDemarca.Rows(r).Item("PROV")
                        nm_dists = lodtbDemarca.Rows(r).Item("DIST")
                        If r = 0 Then
                            lista_depas = nm_depas
                            nm_depas1 = nm_depas
                        ElseIf r > 0 Then
                            If nm_depas <> nm_depas1 Then
                                lista_depas = lista_depas & "," & nm_depas
                                nm_depas1 = nm_depas
                            End If
                        End If

                        'nm_provs = lodtbDemarca.Rows(r).Item("PROV")
                        If r = 0 Then
                            lista_provs = nm_provs
                            nm_provs1 = nm_provs
                        ElseIf r > 0 Then
                            If nm_provs1 <> nm_provs Then
                                lista_provs = lista_provs & "," & nm_provs
                                nm_provs1 = nm_provs
                            End If
                        End If

                        ' nm_dists = lodtbDemarca.Rows(r).Item("DIST")
                        If r = 0 Then
                            lista_dists = nm_dists
                            nm_dists1 = nm_dists
                        ElseIf r > 0 Then
                            If nm_dists <> nm_dists1 Then
                                lista_dists = lista_dists & "," & nm_dists
                                nm_dists1 = nm_dists
                            End If
                        End If

                    Next r


                    'MsgBox(lista_dists, MsgBoxStyle.Critical, "dis")

                    'pFeature.Value(pUpdateFeatures.FindField("DPTO")) = lodtbDemarca.Rows(0).Item("DPTO").ToString
                    'pFeature.Value(pUpdateFeatures.FindField("PROV")) = lodtbDemarca.Rows(0).Item("PROV").ToString
                    'pFeature.Value(pUpdateFeatures.FindField("DIST")) = lodtbDemarca.Rows(0).Item("DIST").ToString


                    pFeature.Value(pUpdateFeatures.FindField("DPTOS")) = lista_depas
                    pFeature.Value(pUpdateFeatures.FindField("PROVS")) = lista_provs
                    pFeature.Value(pUpdateFeatures.FindField("DISTS")) = lista_dists

                End If

                'lodtbDemarca = cls_Oracle.F_Obtiene_Datos_UBIGEO1(lostr_Join_Codigos)

                'If lodtbDemarca.Rows.Count > 0 Then
                '    pFeature.Value(pUpdateFeatures.FindField("DPTO")) = lodtbDemarca.Rows(0).Item("DPTO").ToString
                '    pFeature.Value(pUpdateFeatures.FindField("PROV")) = lodtbDemarca.Rows(0).Item("PROV").ToString
                '    pFeature.Value(pUpdateFeatures.FindField("DIST")) = lodtbDemarca.Rows(0).Item("DIST").ToString

                'End If



                pUpdateFeatures.UpdateFeature(pFeature)
                pFeature = pUpdateFeatures.NextFeature
            Loop

            'lodtbDemarca = cls_Oracle.F_Obtiene_Datos_UBIGEO1(lostr_Join_Codigos)

            'If lodtbDemarca.Rows.Count > 0 Then
            '    pFeature.Value(pUpdateFeatures.FindField("DPTO")) = lodtbDemarca.Rows(0).Item("DPTO").ToString
            '    pFeature.Value(pUpdateFeatures.FindField("PROV")) = lodtbDemarca.Rows(0).Item("PROV").ToString
            '    pFeature.Value(pUpdateFeatures.FindField("DIST")) = lodtbDemarca.Rows(0).Item("DIST").ToString

            'End If


        Catch ex As Exception
            MsgBox("ERROR UPDATE_VALUE")
        End Try
    End Sub
    Public Sub UpdateValue2(ByVal lo_Filtro As String, ByVal p_App As IApplication, Optional ByVal p_Codigo As String = "")
        Dim lodtbDemarca As New DataTable
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim pFeatureClass As IFeatureClass
        pMxDoc = p_App.Document
        Dim afound As Boolean = False
        Dim parametro As Integer = 0
        Dim valor1 As Integer = 0

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
        pFeatureClass = pFeatLayer.FeatureClass
        Dim pQFilter As IQueryFilter
        Dim pUpdateFeatures As IFeatureCursor

        Dim fecha As String
        Dim MyDate As Date
        MyDate = Now
        fecha = RellenarComodin(MyDate.Year, 2, "0") & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & MyDate.Day
        v_fec_denun = fecha & " 00:00"
        v_hor_denun = DateTime.Now.Hour & ":" & (DateTime.Now.Minute) & ":" & (DateTime.Now.Second)
        ' Prepare a query filter.
        pQFilter = New QueryFilter
        pUpdateFeatures = pFeatureClass.Update(pQFilter, False)
        Dim pFeature As IFeature
        pFeature = pUpdateFeatures.NextFeature
        Dim contador As Integer = 0
        Dim conta As Integer
        Dim lostr_Join_Codigos As String
        Dim valor As String

        Try
            Do Until pFeature Is Nothing
                contador = contador + 1
                pFeature.Value(pUpdateFeatures.FindField("CONTADOR")) = contador
                Select Case pFeature.Value(pUpdateFeatures.FindField("ESTADO"))
                    Case "P" ', "K"
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = "G1" ' Petitorio Tramite
                    Case "D"
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = "G2" ' Denuncio Tramite
                    Case "E", "N", "Q", "T"
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = "G3"  'Denuncios Titulados
                    Case "C", "F", "J", "L", "Y", "9", "X"
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = "G4"  'Denuncios Extinguidos
                    Case "A", "B", "S", "M", "G", "R", "Z", "K"
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = "G5"  'Otros
                    Case Else
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = ""
                        pFeature.Value(pUpdateFeatures.FindField("EVAL")) = "EV"
                        pFeature.Value(pUpdateFeatures.FindField("TIPO_EX")) = "PE"
                        pFeature.Value(pUpdateFeatures.FindField("ESTADO")) = "P"
                        pFeature.Value(pUpdateFeatures.FindField("CONCESION")) = "Dm_Simulado"
                        pFeature.Value(pUpdateFeatures.FindField("FEC_DENU")) = v_fec_denun
                        pFeature.Value(pUpdateFeatures.FindField("HOR_DENU")) = v_hor_denun
                        pFeature.Value(pUpdateFeatures.FindField("CODIGOU")) = v_codigo
                        pFeature.Value(pUpdateFeatures.FindField("ZONA")) = v_zona_dm
                        pFeature.Value(pUpdateFeatures.FindField("CARTA")) = "CARTA"
                End Select

                ' lodtbDemarca = cls_Oracle.F_Obtiene_Datos_UBIGEO(Mid(pFeature.Value(pFeature.Fields.FindField("DEMAGIS")), 1, 6), gstrUsuarioAcceso, gstrUsuarioClave, gstrDatabase)
                'If lodtbDemarca.Rows.Count > 0 Then
                '    pFeature.Value(pUpdateFeatures.FindField("DPTO")) = lodtbDemarca.Rows(0).Item("DPTO")
                '    pFeature.Value(pUpdateFeatures.FindField("PROV")) = lodtbDemarca.Rows(0).Item("PROV")
                '    pFeature.Value(pUpdateFeatures.FindField("DIST")) = lodtbDemarca.Rows(0).Item("DIST")
                'End If

                'Dim conta As Integer
                'Dim lostr_Join_Codigos As String
                'Dim valor As String
                'Dim parametro As Integer = 0
                conta = Len(pFeature.Value(pFeature.Fields.FindField("DEMAGIS")))
                If conta = 6 Then
                    parametro = 1
                ElseIf conta = 13 Then
                    parametro = 2
                ElseIf conta = 20 Then
                    parametro = 3
                ElseIf conta = 27 Then
                    parametro = 4
                ElseIf conta = 34 Then
                    parametro = 5
                ElseIf conta = 41 Then
                    parametro = 6
                ElseIf conta = 48 Then
                    parametro = 7
                ElseIf conta = 55 Then
                    parametro = 8
                ElseIf conta = 62 Then
                    parametro = 9
                ElseIf conta = 69 Then
                    parametro = 10
                End If
                If parametro = 1 Then
                    valor = Mid(pFeature.Value(pFeature.Fields.FindField("DEMAGIS")), 1, 6)
                    lostr_Join_Codigos = ""
                    lostr_Join_Codigos = "'" & valor & "',"

                ElseIf parametro > 1 Then
                    lostr_Join_Codigos = ""
                    For k As Integer = 1 To parametro
                        If k = 1 Then
                            valor = Mid(pFeature.Value(pFeature.Fields.FindField("DEMAGIS")), 1, 6)
                            lostr_Join_Codigos = "'" & valor & "',"

                        Else
                            valor1 = 1
                            valor = Mid(pFeature.Value(pFeature.Fields.FindField("DEMAGIS")), valor1 + 7, 6)
                            lostr_Join_Codigos = lostr_Join_Codigos & "'" & valor & "',"

                        End If
                    Next k
                End If

                lostr_Join_Codigos = Left(lostr_Join_Codigos, (Len(lostr_Join_Codigos) - 1))
                'Dim dRow1 As DataRow
                Dim lista_dists As String
                Dim lista_provs As String
                Dim lista_depas As String
                Dim nm_dists As String
                Dim nm_provs As String
                Dim nm_depas As String
                Dim nm_depas1 As String
                Dim nm_dists1 As String
                Dim nm_provs1 As String
                'lostr_Join_Codigos = "'090409','090404','090404'"
                lodtbDemarca = cls_Oracle.F_Obtiene_Datos_UBIGEO1(lostr_Join_Codigos)
                If lodtbDemarca.Rows.Count > 0 Then
                    For r As Integer = 0 To lodtbDemarca.Rows.Count - 1
                        nm_depas = lodtbDemarca.Rows(r).Item("DPTO")
                        nm_provs = lodtbDemarca.Rows(r).Item("PROV")
                        nm_dists = lodtbDemarca.Rows(r).Item("DIST")
                        If r = 0 Then
                            lista_depas = nm_depas
                            nm_depas1 = nm_depas
                        ElseIf r > 0 Then
                            If nm_depas <> nm_depas1 Then
                                lista_depas = lista_depas & "," & nm_depas
                                nm_depas1 = nm_depas
                            End If
                        End If

                        'nm_provs = lodtbDemarca.Rows(r).Item("PROV")
                        If r = 0 Then
                            lista_provs = nm_provs
                            nm_provs1 = nm_provs
                        ElseIf r > 0 Then
                            If nm_provs1 <> nm_provs Then
                                lista_provs = lista_provs & "," & nm_provs
                                nm_provs1 = nm_provs
                            End If
                        End If

                        ' nm_dists = lodtbDemarca.Rows(r).Item("DIST")
                        If r = 0 Then
                            lista_dists = nm_dists
                            nm_dists1 = nm_dists
                        ElseIf r > 0 Then
                            If nm_dists <> nm_dists1 Then
                                lista_dists = lista_dists & "," & nm_dists
                                nm_dists1 = nm_dists
                            End If
                        End If

                    Next r


                    'MsgBox(lista_dists, MsgBoxStyle.Critical, "dis")

                    'pFeature.Value(pUpdateFeatures.FindField("DPTO")) = lodtbDemarca.Rows(0).Item("DPTO").ToString
                    'pFeature.Value(pUpdateFeatures.FindField("PROV")) = lodtbDemarca.Rows(0).Item("PROV").ToString
                    'pFeature.Value(pUpdateFeatures.FindField("DIST")) = lodtbDemarca.Rows(0).Item("DIST").ToString


                    pFeature.Value(pUpdateFeatures.FindField("DPTOS")) = lista_depas
                    pFeature.Value(pUpdateFeatures.FindField("PROVS")) = lista_provs
                    pFeature.Value(pUpdateFeatures.FindField("DISTS")) = lista_dists

                End If

                'lodtbDemarca = cls_Oracle.F_Obtiene_Datos_UBIGEO1(lostr_Join_Codigos)

                'If lodtbDemarca.Rows.Count > 0 Then
                '    pFeature.Value(pUpdateFeatures.FindField("DPTO")) = lodtbDemarca.Rows(0).Item("DPTO").ToString
                '    pFeature.Value(pUpdateFeatures.FindField("PROV")) = lodtbDemarca.Rows(0).Item("PROV").ToString
                '    pFeature.Value(pUpdateFeatures.FindField("DIST")) = lodtbDemarca.Rows(0).Item("DIST").ToString

                'End If



                pUpdateFeatures.UpdateFeature(pFeature)
                pFeature = pUpdateFeatures.NextFeature
            Loop

            'lodtbDemarca = cls_Oracle.F_Obtiene_Datos_UBIGEO1(lostr_Join_Codigos)

            'If lodtbDemarca.Rows.Count > 0 Then
            '    pFeature.Value(pUpdateFeatures.FindField("DPTO")) = lodtbDemarca.Rows(0).Item("DPTO").ToString
            '    pFeature.Value(pUpdateFeatures.FindField("PROV")) = lodtbDemarca.Rows(0).Item("PROV").ToString
            '    pFeature.Value(pUpdateFeatures.FindField("DIST")) = lodtbDemarca.Rows(0).Item("DIST").ToString

            'End If


        Catch ex As Exception
            MsgBox("ERROR UPDATE_VALUE")
        End Try
    End Sub

    Public Sub Obtienedatos_libredenu(ByVal lo_Filtro As String, ByVal p_App As IApplication, Optional ByVal p_Codigo As String = "")
        Try
            Dim lodtbDemarca As New DataTable
            Dim pFeatLayer As IFeatureLayer = Nothing
            Dim pFeatureClass As IFeatureClass
            'pMxDoc = pApp.Document


            Dim afound As Boolean = False
            Dim parametro As Integer = 0
            Dim valor1 As Integer = 0
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Libreden" Then
                    pFeatLayer = pMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If
            pFeatureClass = pFeatLayer.FeatureClass
            Dim pFeatureCursor As IFeatureCursor


            Dim pTable As ITable
            Dim pWorkspaceFactory1 As IWorkspaceFactory
            pWorkspaceFactory1 = New ShapefileWorkspaceFactory
            Dim pFWS As IFeatureWorkspace
            pFWS = pWorkspaceFactory1.OpenFromFile(glo_pathTMP, 0)
            pTable = pFWS.OpenTable("Reporte1_Libredenu" & fecha_archi)
            Dim ptableCursor As ICursor
            Dim pfields3 As Fields
            Dim pRow As IRow
            Dim v_tp_rese As String
            Dim v_cd_rese As String
            Dim v_nm_rese As String
            Dim v_nm_rese1 As String
            Dim v_clase As String
            Dim v_categori As String
            Dim v_nivelSuper As String
            Dim v_tipoSuper1 As String
            Dim v_tipoSuper As String
            Dim v_nm_urba As String
            Dim v_nm_urba1 As String
            Dim v_nonmbre As String
            pFeatureCursor = pFeatureClass.Search(Nothing, False)
            Dim pFeature As IFeature
            pFeature = pFeatureCursor.NextFeature
            Dim contador As Integer = 0

            Dim v_codigo As String
            Dim lodbtExisteAR As New DataTable
            Dim lodbtExistezu As New DataTable
            Dim lodbtExisteZT As New DataTable
            Dim lodbtExistePT As New DataTable
            Dim lodbtExisteDM As New DataTable
            Dim lodbtExiste_paises As New DataTable
            Dim lodbtExiste_fron As New DataTable
            Dim lodbtExiste_ZU_AF As New DataTable
            Dim lodbtExiste_XY As New DataTable
            Dim conta_t As Integer
            v_nm_rese = ""
            v_nm_urba = ""
            v_tipoSuper = ""
            Dim v_xmin As Double
            Dim v_ymin As Double
            Dim v_xmax As Double
            Dim v_ymax As Double


            Dim lo_xMin As Double
            Dim lo_xMax As Double
            Dim lo_yMin As Double
            Dim lo_yMax As Double
            Dim loStrShapefile As String
            ' cls_catastro.PT_CargarFeatureClass_SDE1(gstrFC_Catastro_Minero & v_zona_dm, p_App, "1", False)
            cls_catastro.PT_CargarFeatureClass_SDE("DATA_GIS.DS_CATASTRO_MINERO_PSAD56_" & v_zona_dm, p_App, "1", False)
            'Exit Sub

            ' cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & gloZona, p_App, "1", False)
            'Try
            Do Until pFeature Is Nothing
                contador = contador + 1
                v_codigo = pFeature.Value(pFeatureCursor.FindField("CODIGOU"))
                gloZona = pFeature.Value(pFeatureCursor.FindField("ZONA"))
                lodbtExiste_XY = cls_Oracle.FT_Obtiene_datos_XYminmax(v_codigo, gstrFC_Catastro_Minero & gloZona)
                If lodbtExiste_XY.Rows.Count > 0 Then
                    v_xmin = lodbtExiste_XY.Rows(0).Item("MINX")
                    v_xmax = lodbtExiste_XY.Rows(0).Item("MAXX")
                    v_ymin = lodbtExiste_XY.Rows(0).Item("MINY")
                    v_ymax = lodbtExiste_XY.Rows(0).Item("MAXY")

                    lo_xMin = (v_xmin - 4000)
                    lo_xMax = (v_xmax + 4000)
                    lo_yMin = (v_ymin - 4000)
                    lo_yMax = (v_ymax + 4000)
                    'loStrShapefile = "DM_" & v_codigo
                    'cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & gloZona, p_App, "1", False)
                    'cls_catastro.f_Intercepta_FC("Catastro", lo_xMin, lo_yMin, lo_xMax, lo_yMax, p_App, loStrShapefile)



                End If
                loStrShapefile = "DM_" & v_codigo
                cls_catastro.f_Intercepta_FC("Catastro", lo_xMin, lo_yMin, lo_xMax, lo_yMax, m_application, loStrShapefile)
                v_nonmbre = pFeature.Value(pFeatureCursor.FindField("CONCESION"))
                'pFeatureCursor = pFeatLayer.Search(Nothing, False)
                'pFeature = pFeatureCursor.NextFeature
                'pgeometria = pFeature.Shape
                'pEnvelope = pgeometria.Envelope
                'v_este_min1 = pEnvelope.XMin : v_este_max1 = pEnvelope.XMax
                'v_norte_min1 = pEnvelope.YMin : v_norte_max1 = pEnvelope.YMax
                'v_este_min = v_este_min1 : v_este_max = v_este_max1
                'v_norte_min = v_norte_min1 : v_norte_max = v_norte_max1
                pRow = pTable.CreateRow
                pfields3 = pTable.Fields
                pRow.Value(pfields3.FindField("CG_CODIGO")) = v_codigo
                pRow.Value(pfields3.FindField("EST_SUPER")) = "LIBRE"
                pRow.Value(pfields3.FindField("NOMBRE_DM")) = v_nonmbre
                Try
                    lodbtExisteAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(5, gstrFC_Catastro_Minero & gloZona, gstrFC_AReservada & gloZona, v_codigo)
                    If lodbtExisteAR.Rows.Count = 0 Then
                        Try
                            lodbtExisteAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(6, gstrFC_Catastro_Minero & gloZona, gstrFC_AReservada & gloZona, v_codigo)
                        Catch ex As Exception
                        End Try

                    End If
                Catch ex As Exception
                End Try

                'Try
                'lodbtExisteAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(5, gstrFC_Catastro_Minero & gloZona, gstrFC_AReservada & gloZona, v_codigo)
                'Catch ex As Exception
                'End Try

                If lodbtExisteAR.Rows.Count >= 1 Then
                    For contador1 As Integer = 0 To lodbtExisteAR.Rows.Count - 1
                        v_tp_rese = lodbtExisteAR.Rows(contador1).Item("TIPO")
                        v_cd_rese = lodbtExisteAR.Rows(contador1).Item("CODIGO")
                        v_nm_rese1 = lodbtExisteAR.Rows(contador1).Item("NOMBRE")
                        v_clase = lodbtExisteAR.Rows(contador1).Item("CLASE")
                        v_nivelSuper = lodbtExisteAR.Rows(contador1).Item("SUPER")
                        pfields3 = pTable.Fields
                        ptableCursor = pTable.Search(Nothing, False)
                        v_nm_rese = v_nm_rese & "," & v_nm_rese1
                        'Dim conta_t As Long
                        conta_t = conta_t + 1
                        valor1 = valor1 + 1
                        If v_nivelSuper = "1" Then
                            'pRow.Value(pfields3.FindField("NIV_SUPER")) = "TOTAL"
                            v_tipoSuper1 = "Sup. Total en AR"
                        ElseIf v_nivelSuper = "2" Then
                            'pRow.Value(pfields3.FindField("NIV_SUPER")) = "PARCIAL"
                            v_tipoSuper1 = "Sup. Parcial en AR"
                        End If
                        v_tipoSuper = v_tipoSuper & "," & v_tipoSuper1

                        If v_tp_rese = "AREA NATURAL" Then
                            If v_clase = "ANP" Then
                                pRow.Value(pfields3.FindField("CLASE_N")) = "SI"
                            ElseIf v_clase = "AMORTIGUAMIENTO" Then
                                pRow.Value(pfields3.FindField("CLASE_A")) = "SI"
                            Else
                                pRow.Value(pfields3.FindField("CLASE_V")) = "SI"
                            End If
                        ElseIf v_tp_rese = "ZONA ARQUEOLOGICA" Then
                            pRow.Value(pfields3.FindField("ZONA_A")) = "SI"
                        ElseIf v_tp_rese = "PROYECTO ESPECIAL" Then
                            pRow.Value(pfields3.FindField("PROY_E")) = "SI"
                        ElseIf v_tp_rese = "PUERTO Y/O AEROPUERTOS" Then
                            pRow.Value(pfields3.FindField("PUERTO")) = "SI"
                        ElseIf v_tp_rese = "OTRA AREA RESTRINGIDA" Then
                            pRow.Value(pfields3.FindField("OT")) = "SI"
                        ElseIf v_tp_rese = "ZONA DE RESERVA TURISTICA" Then
                            pRow.Value(pfields3.FindField("ZONA_T")) = "SI"
                        ElseIf v_tp_rese = "ANAP" Then
                            pRow.Value(pfields3.FindField("ANAP")) = "SI"

                        End If

                    Next contador1
                    v_nm_rese = Right(v_nm_rese, Len(v_nm_rese) - 1)
                    pRow.Value(pfields3.FindField("NM_RESE")) = v_nm_rese

                    v_nm_rese = ""

                End If


                'Dim lodbtExisteAu As New DataTable
                'Try
                '    lodbtExistezu = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(3, gstrFC_Catastro_Minero & gloZona, gstrFC_ZUrbana & gloZona, v_codigo)
                'Catch ex As Exception
                'End Try

                Try
                    lodbtExistezu = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(13, gstrFC_Catastro_Minero & gloZona, gstrFC_ZUrbana & gloZona, v_codigo)
                    If lodbtExistezu.Rows.Count = 0 Then
                        Try
                            lodbtExistezu = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(14, gstrFC_Catastro_Minero & gloZona, gstrFC_ZUrbana & gloZona, v_codigo)
                        Catch ex As Exception
                        End Try

                    End If
                Catch ex As Exception
                End Try


                If lodbtExistezu.Rows.Count >= 1 Then
                    For contador1 As Integer = 0 To lodbtExistezu.Rows.Count - 1
                        v_categori = lodbtExistezu.Rows(contador1).Item("CATEGORIA")
                        v_nivelSuper = lodbtExistezu.Rows(contador1).Item("SUPER")
                        v_nm_urba1 = lodbtExisteAR.Rows(contador1).Item("NOMBRE")
                        pfields3 = pTable.Fields
                        ptableCursor = pTable.Search(Nothing, False)
                        v_nm_urba = v_nm_urba & "," & v_nm_urba1
                        If v_categori = "AREA URBANA" Or v_categori = "AREA URBANA Y/O EXPANSION URBANA" Or v_categori = "AREA URBANA Y EXPANSION URBANA" Then
                            pRow.Value(pfields3.FindField("URBA_U")) = "SI"
                        ElseIf v_categori = "EXPANSION URBANA" Then
                            pRow.Value(pfields3.FindField("URBA_EX")) = "SI"
                        End If

                        If v_nivelSuper = "1" Then
                            'pRow.Value(pfields3.FindField("NIV_SUPER")) = "TOTAL"
                            v_tipoSuper1 = "Sup. Total en ZU"
                        ElseIf v_nivelSuper = "2" Then
                            'pRow.Value(pfields3.FindField("NIV_SUPER")) = "PARCIAL"
                            v_tipoSuper1 = "Sup. Parcial en ZU"
                        End If
                        v_tipoSuper = v_tipoSuper & "," & v_tipoSuper1

                        v_nm_urba = Right(v_nm_urba, Len(v_nm_urba) - 1)
                        pRow.Value(pfields3.FindField("NM_URBA")) = v_nm_urba

                        v_nm_urba = ""

                    Next contador1


                End If
                If v_tipoSuper <> "" Then
                    v_tipoSuper = Right(v_tipoSuper, Len(v_tipoSuper) - 1)
                    pRow.Value(pfields3.FindField("NIV_SUPER")) = v_tipoSuper
                End If
                v_tipoSuper = ""
                pRow.Store()

                'Verificacion para Dm superpuesto a Zona de Traslape
                '----------------------------------------------------


                Try
                    lodbtExisteZT = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(16, gstrFC_Catastro_Minero & gloZona, gstrFC_ZTraslape, v_codigo)

                Catch ex As Exception
                End Try


                If lodbtExisteZT.Rows.Count >= 1 Then
                    'For contador1 As Integer = 0 To lodbtExisteZT.Rows.Count - 1
                    'v_nivelSuper = lodbtExisteZT.Rows(contador1).Item("SUPER")
                    pfields3 = pTable.Fields
                    ptableCursor = pTable.Search(Nothing, False)
                    'If v_nivelSuper = "1" Then
                    pRow.Value(pfields3.FindField("ZONA_T")) = "SI"
                    'End If
                    'Next contador1
                End If


                'Verificacion para Dm superpuesto a Dm protegidos
                '----------------------------------------------------


                Try
                    lodbtExistePT = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(17, gstrFC_Catastro_Minero & gloZona, gstrFC_Catastro_Minero & gloZona, v_codigo)

                Catch ex As Exception
                End Try

                If lodbtExistePT.Rows.Count >= 1 Then
                    'For contador1 As Integer = 0 To lodbtExistePT.Rows.Count - 1
                    'v_nivelSuper = lodbtExistePT.Rows(contador1).Item("SUPER")
                    pfields3 = pTable.Fields
                    ptableCursor = pTable.Search(Nothing, False)
                    'If v_nivelSuper = "1" Then
                    pRow.Value(pfields3.FindField("PROTG")) = "SI"
                    'End If
                    ' Next contador1
                End If
                'Verificacion para Dm superpuesto a Dm 
                '----------------------------------------------------

                Try
                    lodbtExisteDM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(15, gstrFC_Catastro_Minero & gloZona, gstrFC_Catastro_Minero & gloZona, v_codigo)

                Catch ex As Exception
                End Try
                If lodbtExisteDM.Rows.Count >= 1 Then
                    pfields3 = pTable.Fields
                    ptableCursor = pTable.Search(Nothing, False)
                    pRow.Value(pfields3.FindField("DM")) = "SI"
                End If

                'Dm superpuesto a Frontera de Paises (determina en que pais esta


                lodbtExiste_paises = cls_Oracle.F_Obtiene_Datos_DMXPAISES(v_codigo)
                If lodbtExiste_paises.Rows.Count = 0 Then
                    Dim NM As String
                    validad_paises = ""
                    For i As Integer = 0 To lodbtExiste_paises.Rows.Count - 1
                        NM = lodbtExiste_paises.Rows(i).Item("NOMBRE").ToString
                        If NM <> "PERU" Then
                            validad_paises = validad_paises & "/" & NM

                            pfields3 = pTable.Fields
                            ptableCursor = pTable.Search(Nothing, False)
                            pRow.Value(pfields3.FindField("FRONT_P")) = "SI"


                        End If
                    Next i
                    validad_paises = Right(validad_paises, Len(validad_paises) - 1)
                End If

                'Dm supuesto a 1000m de la linea de frontera

                Try
                    lodbtExiste_fron = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(18, gstrFC_Catastro_Minero & gloZona, gstrFC_Frontera, v_codigo)

                Catch ex As Exception
                End Try

                If lodbtExiste_fron.Rows.Count >= 1 Then
                    pfields3 = pTable.Fields
                    ptableCursor = pTable.Search(Nothing, False)
                    pRow.Value(pfields3.FindField("FRONTERA")) = "SI"
                End If

                'Dm supuesto a 1000m de la zona Urbana

                Try
                    lodbtExiste_ZU_AF = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(19, gstrFC_Catastro_Minero & gloZona, gstrFC_ZUrbana & gloZona, v_codigo)
                Catch ex As Exception
                End Try
                If lodbtExiste_ZU_AF.Rows.Count >= 1 Then
                    pfields3 = pTable.Fields
                    ptableCursor = pTable.Search(Nothing, False)
                    pRow.Value(pfields3.FindField("URB_AL")) = "SI"
                End If


                'pRow.Store()
                If lodbtExisteAR.Rows.Count > 0 Or lodbtExistezu.Rows.Count > 0 Or lodbtExisteZT.Rows.Count > 0 Or lodbtExistePT.Rows.Count > 0 Or lodbtExisteDM.Rows.Count > 0 Or lodbtExiste_paises.Rows.Count > 1 Or lodbtExiste_fron.Rows.Count > 0 Or lodbtExiste_ZU_AF.Rows.Count > 0 Then
                    pfields3 = pTable.Fields
                    ptableCursor = pTable.Search(Nothing, False)
                    pRow.Value(pfields3.FindField("EST_SUPER")) = "SUPERPUESTO"
                    'ElseIf lodbtExisteAR.Rows.Count = 0 And lodbtExistezu.Rows.Count = 0 And lodbtExisteZT.Rows.Count = 0 And lodbtExistePT.Rows.Count = 0 And lodbtExisteDM.Rows.Count = 0 And lodbtExiste_paises.Rows.Count = 1 And lodbtExiste_fron.Rows.Count = 0 And lodbtExiste_ZU_AF.Rows.Count = 0 Then
                    '    pfields3 = pTable.Fields
                    '    ptableCursor = pTable.Search(Nothing, False)
                    '    pRow.Value(pfields3.FindField("EST_SUPER")) = "LIBRE"

                End If
                pRow.Store()
                pFeature = pFeatureCursor.NextFeature
            Loop

            MsgBox("TERMINO")
            'Catch ex As Exception
            ' MsgBox("Error en Obtener DM para Libre Denunciabilidad...")
            ' End Try
        Catch ex As Exception
            MsgBox("Error")
        End Try
    End Sub
    Public Sub Obtienedatos_PadronMineroDBF(ByVal lo_Filtro As String, ByVal p_App As IApplication, Optional ByVal p_Codigo As String = "")
        Dim lodtbDemarca As New DataTable
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim pFeatureClass As IFeatureClass
        pMxDoc = p_App.Document

        Dim afound As Boolean = False
        Dim parametro As Integer = 0
        Dim valor1 As Integer = 0

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
        pFeatureClass = pFeatLayer.FeatureClass
        Dim pFeatureCursor As IFeatureCursor
        Dim contador As Long
        contador = pFeatureClass.FeatureCount(Nothing)

        Dim pTable As ITable
        Dim pWorkspaceFactory1 As IWorkspaceFactory
        pWorkspaceFactory1 = New ShapefileWorkspaceFactory
        Dim pFWS As IFeatureWorkspace
        pFWS = pWorkspaceFactory1.OpenFromFile(glo_pathTMP, 0)
        pTable = pFWS.OpenTable("Reporte_padron" & fecha_archi)
        Dim ptableCursor As ICursor
        Dim pfields3 As Fields
        Dim pRow As IRow

        Dim valor_areaneta As Double

        pFeatureCursor = pFeatureClass.Search(Nothing, False)
        Dim pFeature As IFeature
        pFeature = pFeatureCursor.NextFeature
        Dim conta_t As Integer = 0

        Dim v_codigo As String
        Dim lodbtExisteAR As New DataTable
        Dim lodbtExistezu As New DataTable

        conta_t = 0
        Try
            Do Until pFeature Is Nothing
                'contador = contador + 1
                v_codigo = pFeature.Value(pFeatureCursor.FindField("CODIGOU"))
                'gloZona = pFeature.Value(pFeatureCursor.FindField("ZONA"))
                conta_t = conta_t + 1
                p_App.Caption = "PROCESO CALCULO AREAS NETAS:  " & conta_t.ToString & "...DE " & contador

                Try
                    lodbtExisteAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(12, "DATA_GIS.CATA_T", "DATA_GIS.CATA_NT", v_codigo)

                Catch ex As Exception
                End Try

                If lodbtExisteAR.Rows.Count > 0 Then
                    pRow = pTable.CreateRow
                    pfields3 = pTable.Fields
                    For contador1 As Integer = 0 To lodbtExisteAR.Rows.Count - 1
                        v_codigo = lodbtExisteAR.Rows(contador1).Item("CODIGO")
                        valor_areaneta = lodbtExisteAR.Rows(contador1).Item("AREANETA")
                        pfields3 = pTable.Fields
                        ptableCursor = pTable.Search(Nothing, False)
                        pRow.Value(pfields3.FindField("CG_CODIGO")) = v_codigo
                        pRow.Value(pfields3.FindField("AREADISPO")) = valor_areaneta


                    Next contador1
                    pRow.Store()
                End If



                pFeature = pFeatureCursor.NextFeature
            Loop
            MsgBox("TERMINO")
        Catch ex As Exception
            MsgBox("Error en Obtener DM para Proceso Padron Minero...")
        End Try
    End Sub

    Public Sub Obtienedatos_PadronMinero(ByVal lo_Filtro As String, ByVal p_App As IApplication, Optional ByVal p_Codigo As String = "")
        Dim lodtbDemarca As New DataTable
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim pFeatureClass As IFeatureClass
        pMxDoc = p_App.Document

        Dim afound As Boolean = False
        Dim parametro As Integer = 0
        Dim valor1 As Integer = 0

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
        pFeatureClass = pFeatLayer.FeatureClass
        Dim pFeatureCursor As IFeatureCursor
        Dim contador As Long
        contador = pFeatureClass.FeatureCount(Nothing)


        ''SE DESCOMENTO
        'Dim pTable As ITable
        'Dim pWorkspaceFactory1 As IWorkspaceFactory
        'pWorkspaceFactory1 = New ShapefileWorkspaceFactory
        'Dim pFWS As IFeatureWorkspace
        'pFWS = pWorkspaceFactory1.OpenFromFile(glo_pathTMP, 0)
        'pTable = pFWS.OpenTable("Reporte_padron" & fecha_archi)
        'Dim ptableCursor As ICursor
        'Dim pfields3 As Fields
        'Dim pRow As IRow
        ''SE DESCOMENTO

        Dim valor_areaneta As Double

        pFeatureCursor = pFeatureClass.Search(Nothing, False)
        Dim pFeature As IFeature
        pFeature = pFeatureCursor.NextFeature
        Dim conta_t As Integer = 0

        Dim v_codigo As String
        Dim lodbtExisteAR As New DataTable
        Dim lodbtExistezu As New DataTable
        Dim cls_ejecuta As New cls_Oracle

        conta_t = 0
        Try
            Do Until pFeature Is Nothing
                'contador = contador + 1
                v_codigo = pFeature.Value(pFeatureCursor.FindField("CODIGOU"))
                'gloZona = pFeature.Value(pFeatureCursor.FindField("ZONA"))
                conta_t = conta_t + 1
                p_App.Caption = "PROCESO CALCULO AREAS NETAS:  " & conta_t.ToString & "...DE " & contador

                Try
                    'If v_codigo = "010084498" Then
                    '    MsgBox("ES")
                    'End If
                    lodbtExisteAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(12, "DATA_GIS.CATA_T_" & v_Zona, "DATA_GIS.CATA_NT_" & v_Zona, v_codigo)

                Catch ex As Exception
                End Try

                If lodbtExisteAR.Rows.Count > 0 Then
                    'pRow = pTable.CreateRow
                    'pfields3 = pTable.Fields
                    'For contador1 As Integer = 0 To lodbtExisteAR.Rows.Count - 1
                    v_codigo = lodbtExisteAR.Rows(0).Item("CODIGO")
                    valor_areaneta = lodbtExisteAR.Rows(0).Item("AREANETA")

                    cls_ejecuta.FT_Guarda_AreaNeta_Padron(v_codigo, valor_areaneta, "V")

                    ''SE DESCOMENTO

                    ''Area Disponible

                    'pRow = pTable.CreateRow
                    'pfields3 = pTable.Fields
                    ''For contador1 As Integer = 0 To lodbtExisteAR.Rows.Count - 1

                    'pfields3 = pTable.Fields
                    'ptableCursor = pTable.Search(Nothing, False)
                    'pRow.Value(pfields3.FindField("CG_CODIGO")) = v_codigo
                    'pRow.Value(pfields3.FindField("AREADISPO")) = valor_areaneta

                    '' Next contador1
                    'pRow.Store()
                End If



                pFeature = pFeatureCursor.NextFeature
            Loop

        Catch ex As Exception
            MsgBox("Error en Obtener DM para Proceso Padron Minero...")
        End Try
    End Sub



    Public Sub UpdateValue(ByVal lo_Filtro As String, ByVal p_App As IApplication, Optional ByVal p_Codigo As String = "")
        Dim lodtbDemarca As New DataTable
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim pFeatureClass As IFeatureClass
        pMxDoc = p_App.Document
        Dim afound As Boolean = False
        Dim parametro As Integer = 0
        Dim valor1 As Integer = 0

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
        pFeatureClass = pFeatLayer.FeatureClass
        Dim pQFilter As IQueryFilter
        Dim pUpdateFeatures As IFeatureCursor

        Dim fecha As String
        Dim MyDate As Date
        MyDate = Now
        fecha = RellenarComodin(MyDate.Year, 2, "0") & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & MyDate.Day
        v_fec_denun = fecha & " 00:00"
        v_hor_denun = DateTime.Now.Hour & ":" & (DateTime.Now.Minute) & ":" & (DateTime.Now.Second)
        ' Prepare a query filter.
        pQFilter = New QueryFilter
        pUpdateFeatures = pFeatureClass.Update(pQFilter, False)
        Dim pFeature As IFeature
        pFeature = pUpdateFeatures.NextFeature
        Dim contador As Integer = 0
        Dim conta As Integer
        Dim lostr_Join_Codigos As String
        Dim valor As String

        Try
            Do Until pFeature Is Nothing
                contador = contador + 1
                pFeature.Value(pUpdateFeatures.FindField("CONTADOR")) = contador
                Select Case pFeature.Value(pUpdateFeatures.FindField("ESTADO"))
                    Case "P" ', "K"
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = "G1" ' Petitorio Tramite
                    Case "D"
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = "G2" ' Denuncio Tramite
                    Case "E", "N", "Q", "T"
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = "G3"  'Denuncios Titulados
                    Case "C", "F", "J", "L", "Y", "9", "X"
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = "G4"  'Denuncios Extinguidos
                    Case "A", "B", "S", "M", "G", "R", "Z", "K"
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = "G5"  'Otros
                    Case Else
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = ""
                        pFeature.Value(pUpdateFeatures.FindField("EVAL")) = "EV"
                        pFeature.Value(pUpdateFeatures.FindField("TIPO_EX")) = "PE"
                        pFeature.Value(pUpdateFeatures.FindField("ESTADO")) = "P"
                        pFeature.Value(pUpdateFeatures.FindField("CONCESION")) = "Dm_Simulado"
                        pFeature.Value(pUpdateFeatures.FindField("FEC_DENU")) = v_fec_denun
                        pFeature.Value(pUpdateFeatures.FindField("HOR_DENU")) = v_hor_denun
                        pFeature.Value(pUpdateFeatures.FindField("CODIGOU")) = v_codigo
                        pFeature.Value(pUpdateFeatures.FindField("ZONA")) = v_zona_dm
                        pFeature.Value(pUpdateFeatures.FindField("CARTA")) = "CARTA"
                End Select

                lodtbDemarca = cls_Oracle.F_Obtiene_Datos_UBIGEO(Mid(pFeature.Value(pFeature.Fields.FindField("DEMAGIS")), 1, 6))
                If lodtbDemarca.Rows.Count > 0 Then
                    pFeature.Value(pUpdateFeatures.FindField("DPTO")) = lodtbDemarca.Rows(0).Item("DPTO")
                    pFeature.Value(pUpdateFeatures.FindField("PROV")) = lodtbDemarca.Rows(0).Item("PROV")
                    pFeature.Value(pUpdateFeatures.FindField("DIST")) = lodtbDemarca.Rows(0).Item("DIST")
                End If
                pUpdateFeatures.UpdateFeature(pFeature)
                pFeature = pUpdateFeatures.NextFeature
            Loop

        Catch ex As Exception
            MsgBox("ERROR UPDATE_VALUE")
        End Try
    End Sub



    Public Sub Update_Value_DM(ByVal p_App As IApplication, Optional ByVal p_Codigo As String = "")
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim pFeatureClass As IFeatureClass
        pMxDoc = p_App.Document
        Dim afound As Boolean = False
        For A As Integer = 0 To pmap.LayerCount - 1
            If pmap.Layer(A).Name = "Catastro" Then
                pFeatLayer = pmap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra el Layer")
            Exit Sub
        End If
        pFeatureClass = pFeatLayer.FeatureClass
        Dim pQFilter As IQueryFilter
        Dim pUpdateFeatures As IFeatureCursor
        ' Prepare a query filter.
        pQFilter = New QueryFilter
        pQFilter.WhereClause = "CODIGOU = '" & p_Codigo & "'"
        pUpdateFeatures = pFeatureClass.Update(pQFilter, False)
        Dim pFeature As IFeature
        pFeature = pUpdateFeatures.NextFeature
        Try
            Do Until pFeature Is Nothing
                Select Case pFeature.Value(pUpdateFeatures.FindField("CODIGOU"))
                    Case p_Codigo
                        pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = "G6"
                        'Exit Do
                End Select
                pUpdateFeatures.UpdateFeature(pFeature)
                pFeature = pUpdateFeatures.NextFeature
            Loop
        Catch ex As Exception
            MsgBox("ERROR Update_Value_DM")
        End Try
    End Sub
    Public Sub Genera_Tematico_Catastro(ByVal lo_Filtro As String, ByVal p_App As IApplication)
        Dim lo_Campo As String = "LEYENDA"
        Dim Consulta As IQueryFilter
        Try
            pMxDoc = p_App.Document
            pmap = pMxDoc.FocusMap
            Dim pLayer As ILayer = Nothing
            Dim afound As Boolean = False
            For A As Integer = 0 To pmap.LayerCount - 1
                If pmap.Layer(A).Name = "Catastro" Then
                    pLayer = pMxDoc.FocusMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("Layer No Existe.")
                Exit Sub
            End If
            'pLayer = pMap.Layer(0) ' Catastro
            Dim pflayer As IFeatureLayer = Nothing
            pflayer = pLayer
            Dim pLyr As IGeoFeatureLayer
            pLyr = pflayer
            Dim pFeatCls As IFeatureClass
            pFeatCls = pflayer.FeatureClass
            Consulta = New QueryFilter
            Dim pFeatCursor As IFeatureCursor
            'Consulta.WhereClause = lo_Filtro
            pFeatCursor = pFeatCls.Search(Consulta, False)
            Dim rx As IRandomColorRamp
            rx = New RandomColorRamp
            Dim pRender As IUniqueValueRenderer, n As Long
            pRender = New UniqueValueRenderer
            Dim symd As ISimpleFillSymbol
            symd = New SimpleFillSymbol
            'Leyenda para la parte inicial de la leyenda (color Blanco)
            symd.Style = esriSimpleFillStyle.esriSFSSolid ' esriSFSSolid
            symd.Outline.Width = 0.4
            Dim myColor As IColor
            myColor = New RgbColor
            myColor.RGB = RGB(255, 255, 255)
            symd.Color = myColor
            pRender.FieldCount = 1
            pRender.Field(0) = lo_Campo
            pRender.DefaultSymbol = symd
            pRender.UseDefaultSymbol = True
            'Barriendo los registros
            Dim pFeat As IFeature
            n = pFeatCls.FeatureCount(Consulta)
            Dim i As Integer
            i = 0
            Dim ValFound As Boolean
            Dim NoValFound As Boolean
            Dim uh As Integer
            Dim pFields As IFields
            Dim iField As Integer
            'Dim pField As IField
            pFields = pFeatCls.Fields
            iField = pFields.FindField(lo_Campo)
            Dim pLineSymbol1 As ISimpleLineSymbol
            Dim RgbColor1 As IRgbColor
            Do Until i = n
                Dim symx As ISimpleFillSymbol
                symx = New SimpleFillSymbol
                symx.Style = esriSimpleFillStyle.esriSFSForwardDiagonal ' esriSFSForwardDiagonal
                RgbColor1 = New RgbColor
                RgbColor1.RGB = RGB(205, 0, 237)
                symx.Color = RgbColor1
                RgbColor1 = New RgbColor
                RgbColor1.RGB = RGB(205, 0, 237)
                pLineSymbol1 = symx.Outline
                pLineSymbol1.Style = esriSimpleLineStyle.esriSLSSolid ' esriSLSSolid
                pLineSymbol1.Color = RgbColor1
                pFeat = pFeatCursor.NextFeature
                Dim X As String
                X = pFeat.Value(iField)
                If X <> "" Then
                    ValFound = False
                    For uh = 0 To (pRender.ValueCount - 1)
                        If pRender.Value(uh) = X Then
                            NoValFound = True
                            Exit For
                        End If
                    Next uh
                    If Not ValFound Then
                        pRender.AddValue(X, lo_Campo, symx)
                        pRender.Label(X) = X
                        pRender.Symbol(X) = symx
                    End If
                End If
                i = i + 1
            Loop
            rx.Size = pRender.ValueCount
            rx.CreateRamp(True)
            Dim RColors As IEnumColors, ny As Long
            RColors = rx.Colors
            RColors.Reset()
            'Asignando leyenda al valor tematico del Catastro Minero
            Dim pFillSymbol As ISimpleFillSymbol
            Dim pLineSymbol As ISimpleLineSymbol
            Dim RgbColor As IRgbColor
            For ny = 0 To (pRender.ValueCount - 1)
                Dim xv As String
                xv = pRender.Value(ny)
                pFillSymbol = pRender.Symbol(xv)
                pFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow ' esriSFSHollow
                pLineSymbol = pFillSymbol.Outline
                pLineSymbol.Width = 1.25
                RgbColor = New RgbColor
                Select Case xv
                    Case "G1" ' En Tramite Verde
                        RgbColor.RGB = RGB(0, 255, 0)
                    Case "G2" 'En Tramite D.L. 109 Rojo
                        RgbColor.RGB = RGB(255, 0, 0)
                    Case "G3" ' Titulados Azul
                        RgbColor.RGB = RGB(0, 0, 255)
                    Case "G4" 'Extinguidos Negro
                        RgbColor.RGB = RGB(0, 0, 0)
                    Case "G5" 'Otros Marron
                        RgbColor.RGB = RGB(0, 255, 255)
                End Select
                pLineSymbol.Color = RgbColor
                pFillSymbol.Outline = pLineSymbol
                pRender.DefaultSymbol = pFillSymbol
                pRender.UseDefaultSymbol = True
                pLyr.Renderer = pRender
            Next ny
            pRender.FieldType(0) = True
            pLyr.Renderer = pRender
            pLyr.DisplayField = lo_Campo
            Dim hx As IRendererPropertyPage
            hx = New UniqueValuePropertyPage
            pLyr.RendererPropertyPageClassID = hx.ClassID
            pMxDoc.ActiveView.ContentsChanged()
            pMxDoc.UpdateContents()
            pMxDoc.ActiveView.Refresh()
        Catch ex As Exception
            MsgBox("No ha terminado de generar tematico", vbCritical, "Observacion  ")
        End Try
    End Sub
    Public Sub Genera_Tematico_Estamin(ByVal pApp As IApplication)
        Dim lo_Campo As String = "TIPO"
        Dim Consulta As IQueryFilter
        Try
            pMxDoc = pApp.Document
            pMap = pMxDoc.FocusMap
            Dim pLayer As ILayer = Nothing
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Situacion_DM" Then
                    pLayer = pMxDoc.FocusMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("Layer No Existe.")
                Exit Sub
            End If
            'pLayer = pMap.Layer(0) ' Catastro
            Dim pflayer As IFeatureLayer = Nothing
            pflayer = pLayer
            Dim pLyr As IGeoFeatureLayer
            pLyr = pflayer
            Dim pFeatCls As IFeatureClass
            pFeatCls = pflayer.FeatureClass
            Consulta = New QueryFilter
            Dim pFeatCursor As IFeatureCursor
            'Consulta.WhereClause = lo_Filtro
            pFeatCursor = pFeatCls.Search(Consulta, False)
            Dim rx As IRandomColorRamp
            rx = New RandomColorRamp
            Dim pRender As IUniqueValueRenderer, n As Long
            pRender = New UniqueValueRenderer
            Dim symd As ISimpleFillSymbol
            symd = New SimpleFillSymbol
            'Leyenda para la parte inicial de la leyenda (color Blanco)

            symd.Style = esriSimpleFillStyle.esriSFSNull

            symd.Outline.Width = 0.2
            Dim myColor As IColor
            myColor = New RgbColor
            myColor.RGB = RGB(110, 110, 110)
            symd.Color = myColor
            pRender.FieldCount = 1
            pRender.Field(0) = lo_Campo
            pRender.DefaultSymbol = symd
            pRender.UseDefaultSymbol = True
            'Barriendo los registros

            Dim pFeat As IFeature
            n = pFeatCls.FeatureCount(Consulta)
            Dim i As Integer
            i = 0
            Dim ValFound As Boolean
            Dim NoValFound As Boolean
            Dim uh As Integer
            Dim pFields As IFields
            Dim iField As Integer
            'Dim pField As IField
            pFields = pFeatCls.Fields
            iField = pFields.FindField(lo_Campo)
            'Dim pLineSymbol1 As ISimpleLineSymbol
            Dim RgbColor1 As IRgbColor
            Do Until i = n
                Dim symx As ISimpleFillSymbol
                symx = New SimpleFillSymbol
                'symx.Style = esriSimpleFillStyle.esriSFSForwardDiagonal ' esriSFSForwardDiagonal
                RgbColor1 = New RgbColor
                RgbColor1.NullColor = True
                RgbColor1.UseWindowsDithering = True
                symx.Color = RgbColor1

                pFeat = pFeatCursor.NextFeature
                Dim X As String
                X = pFeat.Value(iField)
                If X <> "" Then
                    ValFound = False
                    For uh = 0 To (pRender.ValueCount - 1)
                        If pRender.Value(uh) = X Then
                            NoValFound = True
                            Exit For
                        End If
                    Next uh
                    If Not ValFound Then
                        pRender.AddValue(X, lo_Campo, symx)
                        pRender.Label(X) = X
                        pRender.Symbol(X) = symx
                    End If
                End If
                i = i + 1
            Loop
            rx.Size = pRender.ValueCount
            rx.CreateRamp(True)
            Dim RColors As IEnumColors, ny As Long
            RColors = rx.Colors
            RColors.Reset()
            'Asignando leyenda al valor tematico del tema

            Dim pFillSymbol As ISimpleFillSymbol
            Dim pLineSymbol As ISimpleLineSymbol
            Dim RgbColor As IRgbColor
            For ny = 0 To (pRender.ValueCount - 1)
                Dim xv As String
                xv = pRender.Value(ny)
                pFillSymbol = pRender.Symbol(xv)
                pFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow ' esriSFSHollow
                pLineSymbol = pFillSymbol.Outline
                pLineSymbol.Width = 2.5
                RgbColor = New RgbColor
                'Aplica colores segun el tipo
                Select Case xv
                    Case "EXPLORACIÓN" ' DM en Exploracion
                        RgbColor.RGB = RGB(0, 169, 230)
                    Case "EXPLOTACIÓN" 'DM en Explotacion
                        RgbColor.RGB = RGB(230, 152, 0)
                    Case " "  'DM vacio color nulo para que no se vea color
                        RgbColor.NullColor = True
                        RgbColor.UseWindowsDithering = True

                End Select
                pLineSymbol.Color = RgbColor
                pFillSymbol.Outline = pLineSymbol
                pRender.DefaultSymbol = pFillSymbol
                pRender.UseDefaultSymbol = True
                pLyr.Renderer = pRender
            Next ny
            pRender.FieldType(0) = True
            pLyr.Renderer = pRender
            pLyr.DisplayField = lo_Campo
            Dim hx As IRendererPropertyPage
            hx = New UniqueValuePropertyPage
            pLyr.RendererPropertyPageClassID = hx.ClassID
            pMxDoc.ActiveView.ContentsChanged()
            pMxDoc.UpdateContents()
            pMxDoc.ActiveView.Refresh()
        Catch ex As Exception
            MsgBox("No se puede generar tematico para Dm en Exploración y Explotación...", vbCritical, "Observacion  ")
        End Try
    End Sub
    Public Sub GeneratematicoCatastro(ByVal p_app As IApplication)
        Dim lo_Campo As String = "D_ESTADO"
        Dim Consulta As IQueryFilter
        On Error GoTo EH
        'Dim documento As IMxDocument
        pMxDoc = p_app.Document
        'Dim pMap As IMap
        pmap = pMxDoc.FocusMap
        Dim pLayer As ILayer
        pLayer = pmap.Layer(0) ' Catastro

        Dim pflayer As IFeatureLayer
        pflayer = pLayer
        Dim pLyr As IGeoFeatureLayer
        pLyr = pflayer

        Dim pFeatCls As IFeatureClass
        pFeatCls = pflayer.FeatureClass
        Consulta = New QueryFilter
        Dim pFeatCursor As IFeatureCursor
        pFeatCursor = pFeatCls.Search(Consulta, False)
        Dim rx As IRandomColorRamp
        rx = New RandomColorRamp

        Dim pRender As IUniqueValueRenderer, n As Long
        pRender = New UniqueValueRenderer
        Dim symd As ISimpleFillSymbol
        symd = New SimpleFillSymbol
        'Leyenda para la parte inicial de la leyenda (color Blanco)
        symd.Style = esriSimpleFillStyle.esriSFSSolid ' esriSFSSolid
        symd.Outline.Width = 0.4
        Dim myColor As IColor
        myColor = New RgbColor
        myColor.RGB = RGB(255, 255, 255)
        symd.Color = myColor
        pRender.FieldCount = 1
        pRender.Field(0) = lo_Campo
        pRender.DefaultSymbol = symd
        pRender.UseDefaultSymbol = True

        'Barriendo los registros

        Dim pFeat As IFeature
        n = pFeatCls.FeatureCount(Consulta)
        Dim i As Integer
        i = 0
        Dim ValFound As Boolean
        Dim NoValFound As Boolean
        Dim uh As Integer
        Dim pFields As IFields
        Dim iField As Integer

        Dim pField As IField
        pFields = pFeatCls.Fields
        iField = pFields.FindField(lo_Campo)

        'Dim pFillSymbol1 As ISimpleFillSymbol
        Dim pLineSymbol1 As ISimpleLineSymbol
        Dim RgbColor1 As IRgbColor

        Do Until i = n
            Dim symx As ISimpleFillSymbol
            symx = New SimpleFillSymbol
            symx.Style = esriSimpleFillStyle.esriSFSForwardDiagonal ' esriSFSForwardDiagonal
            'Set pLineSymbol1 = symx.Outline
            'pLineSymbol1.Width = 0.01
            RgbColor1 = New RgbColor
            RgbColor1.RGB = RGB(205, 0, 237)
            symx.Color = RgbColor1
            RgbColor1 = New RgbColor
            RgbColor1.RGB = RGB(205, 0, 237)
            pLineSymbol1 = symx.Outline
            pLineSymbol1.Style = esriSimpleLineStyle.esriSLSSolid ' esriSLSSolid
            pLineSymbol1.Color = RgbColor1

            pFeat = pFeatCursor.NextFeature
            Dim X As String
            X = pFeat.Value(iField)
            If X <> "" Then
                ValFound = False
                For uh = 0 To (pRender.ValueCount - 1)
                    If pRender.Value(uh) = X Then
                        NoValFound = True
                        Exit For
                    End If
                Next uh
                If Not ValFound Then
                    pRender.AddValue(X, lo_Campo, symx)
                    pRender.Label(X) = X
                    pRender.Symbol(X) = symx
                End If
            End If
            i = i + 1
        Loop

        rx.Size = pRender.ValueCount
        rx.CreateRamp(True)
        Dim RColors As IEnumColors, ny As Long
        RColors = rx.Colors
        RColors.Reset()

        'Asignando leyenda al valor tematico del Catastro Minero

        Dim pFillSymbol As ISimpleFillSymbol = Nothing
        Dim pLineSymbol As ISimpleLineSymbol = Nothing
        Dim RgbColor As IRgbColor

        For ny = 0 To (pRender.ValueCount - 1)
            Dim xv As String
            xv = pRender.Value(ny)
            If ((xv = "D.M. Consultado") And (ny = 0)) Then  ' Si es cero
                xv = "D.M. Consultado1"  'artificio cuando dm_evaluado = 0
            Else


                'Proceso normal cuando D.M. EVALUADO NO ES CERO
                If xv = "D.M. Extinguidos" Then
                    pFillSymbol = pRender.Symbol(xv)
                    pFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow ' esriSFSHollow
                    pLineSymbol = pFillSymbol.Outline
                    pLineSymbol.Width = 0.01
                    RgbColor = New RgbColor
                    RgbColor.RGB = RGB(0, 0, 0)
                    pLineSymbol.Color = RgbColor
                ElseIf xv = "D.M. en Trámite" Then
                    pFillSymbol = pRender.Symbol(xv)
                    pFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow ' esriSFSHollow
                    pLineSymbol = pFillSymbol.Outline
                    pLineSymbol.Width = 0.01
                    RgbColor = New RgbColor
                    RgbColor.RGB = RGB(0, 255, 0)
                    pLineSymbol.Color = RgbColor
                ElseIf xv = "D.M. en Trámite (DL 109)" Then
                    pFillSymbol = pRender.Symbol(xv)
                    pFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow ' esriSFSHollow
                    pLineSymbol = pFillSymbol.Outline
                    pLineSymbol.Width = 0.01
                    RgbColor = New RgbColor
                    RgbColor.RGB = RGB(255, 0, 0)
                    pLineSymbol.Color = RgbColor
                ElseIf xv = "Otros" Then
                    pFillSymbol = pRender.Symbol(xv)
                    pFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow ' esriSFSHollow
                    pLineSymbol = pFillSymbol.Outline
                    pLineSymbol.Width = 0.01
                    RgbColor = New RgbColor
                    RgbColor.RGB = RGB(78, 0, 0)
                    pLineSymbol.Color = RgbColor
                ElseIf xv = "D.M. Titulados" Then
                    pFillSymbol = pRender.Symbol(xv)
                    pFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow ' esriSFSHollow
                    pLineSymbol = pFillSymbol.Outline
                    pLineSymbol.Width = 0.01
                    RgbColor = New RgbColor
                    RgbColor.RGB = RGB(0, 0, 255)
                    pLineSymbol.Color = RgbColor
                ElseIf xv = "D.M. Evaluado" Then
                    pFillSymbol = pRender.Symbol(xv)
                    pFillSymbol.Style = esriSimpleFillStyle.esriSFSForwardDiagonal ' esriSFSForwardDiagonal
                    pLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid ' esriSLSSolid
                    pLineSymbol.Width = 0.01
                    RgbColor = New RgbColor
                    RgbColor.RGB = RGB(205, 0, 237)
                    pFillSymbol.Color = RgbColor
                    RgbColor = New RgbColor
                    RgbColor.RGB = RGB(205, 0, 237)
                    pLineSymbol.Color = RgbColor
                ElseIf xv = "D.M. Consultado" Then
                    pFillSymbol = pRender.Symbol(xv)
                    pFillSymbol.Style = esriSimpleFillStyle.esriSFSForwardDiagonal ' esriSFSForwardDiagonal
                    pLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid '  esriSLSSolid
                    pLineSymbol.Width = 0.01
                    RgbColor = New RgbColor
                    RgbColor.RGB = RGB(205, 0, 237)
                    pFillSymbol.Color = RgbColor
                    RgbColor = New RgbColor
                    RgbColor.RGB = RGB(205, 0, 237)
                    pLineSymbol.Color = RgbColor
                End If
                pFillSymbol.Outline = pLineSymbol
                pRender.DefaultLabel = ""
                pRender.DefaultSymbol = pFillSymbol
                pRender.UseDefaultSymbol = True
                pLyr.Renderer = pRender
            End If
        Next ny
        pRender.FieldType(0) = True
        pLyr.Renderer = pRender
        pLyr.DisplayField = lo_Campo
        Dim hx As IRendererPropertyPage
        hx = New UniqueValuePropertyPage
        pLyr.RendererPropertyPageClassID = hx.ClassID
        pMxDoc.ActiveView.ContentsChanged()
        pMxDoc.UpdateContents()
        pMxDoc.ActiveView.Refresh()

        Exit Sub
EH:
        MsgBox("No ha terminado de generar tematico", vbCritical, "Observacion")

    End Sub




    Function F_Lista_Codigo_DM(ByRef pLayer As IFeatureLayer)

        Dim loint_registro As Integer = 0
        Dim lostr_Codigou As String = Nothing
        Dim pFeatureSelection As IFeatureSelection
        pFeatureSelection = pLayer
        Dim pFCursor As IFeatureCursor = Nothing
        pFeatureSelection.SelectionSet.Search(Nothing, False, pFCursor)
        Dim Registro_DM As IFeature
        Dim lostr_Join_Codigos As String = ""
        Registro_DM = pFCursor.NextFeature

        Do Until Registro_DM Is Nothing
            loint_registro = loint_registro + 1
            lostr_Codigou = Registro_DM.Value(Registro_DM.Class.FindField("CODIGOU"))
            'Registro_DM.Value(Registro_DM.Fields.FindField("LEYENDA")) = "G1"
            lostr_Join_Codigos = lostr_Join_Codigos & "'" & lostr_Codigou & "',"
            Registro_DM = pFCursor.NextFeature
        Loop
        lostr_Join_Codigos = "CODIGOU IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
        Registro_DM = Nothing
        pFeatureSelection = Nothing
        Return lostr_Join_Codigos
    End Function

    Public Sub DefinitionExpression(ByVal lo_Filtro As String, ByVal p_App As IApplication, ByVal Nom_Shapefile As String)
        If lo_Filtro = "" Then Exit Sub
        Dim v_Tema As String
        Select Case Nom_Shapefile
            Case "Departamento"
                v_Tema = "Departamento"
            Case "Provincia"
                v_Tema = "Provincia"
            Case "Distrito"
                v_Tema = "Distrito"
            Case "Zona Reservada"
                v_Tema = "Zona Reservada"
            Case "Zona Urbana"
                v_Tema = "Zona Urbana"
            Case "Geologia"
                v_Tema = "Geologia"
            Case "Drenaje"
                v_Tema = "Drenaje"
            Case "Vias"
                v_Tema = "Vias"
            Case "Centro Poblado"
                v_Tema = "Centro Poblado"
            Case "Cuadricula Regional"
                v_Tema = "Cuadricula Regional"
            Case "Zona Traslape"
                v_Tema = "Zona Traslape"
            Case "Limite de Zona"
                v_Tema = "Limite de Zona"
            Case "Capitales Distritales"
                v_Tema = "Capitales Distritales"
            Case "Certificacion Ambiental"
                v_Tema = "Certificacion Ambiental"
            Case "DM_Uso_Minero"
                v_Tema = "DM_Uso_Minero"
            Case "DM_Actividad_Minera"
                v_Tema = "DM_Actividad_Minera"
            Case Else
                v_Tema = "Catastro"
        End Select
        Dim pActiveView As IActiveView
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim pFeatureLayerD As IFeatureLayerDefinition
        pmap = pMxDoc.FocusMap
        pActiveView = pmap
        Dim afound As Boolean = False
        For A As Integer = 0 To pmap.LayerCount - 1
            If pmap.Layer(A).Name = v_Tema Then
                pFeatLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("Layer No Existe.")
            Exit Sub
        End If
        pFeatureLayerD = pFeatLayer
        If v_Tema <> "Certificacion Ambiental" Then
            If v_Tema = "DM_Uso_Minero" Then

            ElseIf v_Tema = "DM_Actividad_Minera" Then

            Else
                pFeatureLayerD.DefinitionExpression = lo_Filtro
                pMxDoc.UpdateContents()
                pActiveView.Refresh()
                Dim pFeatureSelection As IFeatureSelection = pFeatLayer
                If Not pFeatureSelection Is Nothing Then
                    Dim pQueryFilter As IQueryFilter
                    ' Prepare a query filter.
                    pQueryFilter = New QueryFilter
                    pQueryFilter.WhereClause = lo_Filtro
                    ' Refresh or erase any previous selection.
                    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                    pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
                    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

                End If
            End If

        End If

        If pFeatureSelection Is Nothing Then If v_Tema = "Catastro" Then Expor_Tema(Nom_Shapefile, sele_denu, p_App)
        pFeatureSelection = pFeatureLayerD
        pFeatureLayerD = pFeatureSelection
        canti_cartas = pFeatureSelection.SelectionSet.Count
        Dim pFeatureLayer_1 As IFeatureLayer
        pFeatureLayer_1 = pFeatureLayerD.CreateSelectionLayer(v_Tema, True, "", "")
        pmap.DeleteLayer(pFeatLayer)
        pmap.AddLayer(pFeatureLayer_1)
        If v_Tema = "Catastro" Then
            pFeatureLayer_1.Visible = True
        ElseIf v_Tema = "Zona Reservada" Then
            pFeatureLayer_1.Visible = True
        ElseIf v_Tema = "Zona Urbana" Then
            pFeatureLayer_1.Visible = True
        ElseIf v_Tema = "DM_Uso_Minero" Then
            pFeatureLayer_1.Visible = True
        ElseIf v_Tema = "DM_Actividad_Minera" Then
            pFeatureLayer_1.Visible = True
        ElseIf v_Tema = "Certificacion Ambiental" Then
            pFeatureLayer_1.Visible = True
        Else
            pFeatureLayer_1.Visible = False
        End If
        'pFeatureSelection.Clear()
        pMxDoc.UpdateContents()

    End Sub

    Public Sub DefinitionExpression_1(ByVal lo_Filtro As String, ByVal p_App As IApplication, ByVal Nom_Shapefile As String)
        If lo_Filtro = "" Then Exit Sub
        Dim v_Tema As String
        Select Case Nom_Shapefile
            Case "Departamento"
                v_Tema = "Departamento"
            Case "Provincia"
                v_Tema = "Provincia"
            Case "Distrito"
                v_Tema = "Distrito"
            Case "Zona Reservada"
                v_Tema = "Zona Reservada"
            Case "Zona Urbana"
                v_Tema = "Zona Urbana"
            Case "Geologia"
                v_Tema = "Geologia"
            Case Else
                v_Tema = "Catastro"
        End Select
        'If Mid(Nom_Shapefile, 1, 8) = "Catastro" Then
        '    v_Tema = "Catastro"
        'End If
        Dim pActiveView As IActiveView
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim pFeatureLayerD As IFeatureLayerDefinition
        pMxDoc = p_App.Document
        pmap = pMxDoc.FocusMap
        pActiveView = pmap
        Dim afound As Boolean = False
        For A As Integer = 0 To pmap.LayerCount - 1
            If pmap.Layer(A).Name = v_Tema Then
                pFeatLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("Layer No Existe.")
            Exit Sub
        End If
        pFeatureLayerD = pFeatLayer
        pFeatureLayerD.DefinitionExpression = lo_Filtro
        pMxDoc.UpdateContents()
        pActiveView.Refresh()
        Dim pFeatureSelection As IFeatureSelection = pFeatLayer
        If Not pFeatureSelection Is Nothing Then
            Dim pQueryFilter As IQueryFilter
            ' Prepare a query filter.
            pQueryFilter = New QueryFilter
            pQueryFilter.WhereClause = lo_Filtro
            ' Refresh or erase any previous selection.
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        End If
        'If pFeatureSelection Is Nothing Then If v_Tema = "Catastro" Then Expor_Tema(Nom_Shapefile, "Denun=No", p_App)
        If pFeatureSelection Is Nothing Then If v_Tema = "Catastro" Then Expor_Tema(Nom_Shapefile, sele_denu, p_App)
        pFeatureSelection = pFeatureLayerD
        pFeatureLayerD = pFeatureSelection
        Dim pFeatureLayer_1 As IFeatureLayer
        pFeatureLayer_1 = pFeatureLayerD.CreateSelectionLayer(v_Tema, True, vbNullString, "")
        pmap.DeleteLayer(pFeatLayer)
        pmap.AddLayer(pFeatureLayer_1)
        pMxDoc.UpdateContents()

    End Sub
    Public Sub DefinitionExpression_Campo(ByVal lo_Filtro As String, ByVal p_App As IApplication, ByVal Nom_Shapefile As String)
        Dim pActiveView As IActiveView
        'pMxDoc = p_App.Document
        pmap = pMxDoc.FocusMap
        pActiveView = pmap
        Dim afound As Boolean = False
        For A As Integer = 0 To pmap.LayerCount - 1
            If pmap.Layer(A).Name = Nom_Shapefile Then
                pFeatureLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("Layer No Existe.")
            Exit Sub
        End If
        Dim pFeatureSelection As IFeatureSelection = pFeatureLayer
        If Not pFeatureSelection Is Nothing Then
            Dim pQueryFilter As IQueryFilter
            ' Prepare a query filter.
            pQueryFilter = New QueryFilter
            pQueryFilter.WhereClause = lo_Filtro
            ' Refresh or erase any previous selection.
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pActiveView.Refresh()
        End If
    End Sub
    Public Sub DefinitionExpression_Campo_Zoom(ByVal lo_Filtro As String, ByVal p_App As IApplication, ByVal Nom_Shapefile As String)
        Dim pActiveView As IActiveView
        'pMxDoc = p_App.Document
        pmap = pMxDoc.FocusMap
        pActiveView = pmap
        Dim afound As Boolean = False
        For A As Integer = 0 To pmap.LayerCount - 1
            If pmap.Layer(A).Name = Nom_Shapefile Then
                pFeatureLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("Layer No Existe.")
            Exit Sub
        End If
        Dim pFeatureSelection As IFeatureSelection = pFeatureLayer
        If Not pFeatureSelection Is Nothing Then
            Dim pQueryFilter As IQueryFilter
            ' Prepare a query filter.
            pQueryFilter = New QueryFilter
            pQueryFilter.WhereClause = lo_Filtro
            ' Refresh or erase any previous selection.
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            'pActiveView.Refresh()
            Dim pCmdItem As ICommandItem
            Dim pUID As New UID
            pUID.Value = "esriArcMapUI.ZoomToSelectedCommand"
            pCmdItem = p_App.Document.CommandBars.Find(pUID)
            pCmdItem.Execute()
            pMxDoc.ActiveView.Refresh()
        End If
    End Sub
    'Public Sub Activar_Layer()
    '    'Dim pDoc As IMxDocument
    '    'pDoc = p_App.Document
    '    'Dim pMap As IMap
    '    pMap = pDoc.FocusMap
    '    Dim pGFlayer As ICompositeLayer
    '    Dim ii As Integer
    '    x = 0 'pMap.LayerCount - 1
    '    'For x = pMap.LayerCount - 1 To pMap.LayerCount
    '    For x = 0 To pMap.LayerCount - 1
    '        '************* Si el layer el tipo FeatureLayer
    '        If TypeOf pMap.Layer(x) Is IFeatureLayer Then
    '            If Not (pMap.Layer(x).Visible) Then pMap.Layer(x).Visible = True
    '        End If
    '        '************* Si el layer el tipo Group FeatureLayer
    '        If TypeOf pMap.Layer(x) Is IGroupLayer Then
    '            Dim pGGFlayer As IGroupLayer
    '            pGGFlayer = pMap.Layer(x)
    '            If Not (pGGFlayer.Visible) Then pGGFlayer.Visible = True
    '            pGFlayer = pGGFlayer
    '            For ii = 0 To pGFlayer.Count - 1
    '                If TypeOf pGFlayer.Layer(ii) Is IFeatureLayer Then
    '                    If Not (pGFlayer.Layer(ii).Visible) Then pGFlayer.Layer(ii).Visible = True
    '                End If
    '            Next ii
    '        End If
    '        '************* Si el layer el tipo WMS FeatureLayer
    '        If TypeOf pMap.Layer(x) Is IWMSGroupLayer Then
    '            Dim pWMSFlayer As IWMSGroupLayer
    '            If Not (pMap.Layer(x).Visible) Then pMap.Layer(x).Visible = True
    '            pWMSFlayer = pMap.Layer(x)
    '            If Not (pWMSFlayer.Layer(0).Visible) Then pWMSFlayer.Layer(0).Visible = True
    '            pGFlayer = pWMSFlayer.Layer(0)
    '            For ii = 0 To pGFlayer.Count - 1
    '                If TypeOf pGFlayer.Layer(ii) Is IWMSLayer Then
    '                    If Not (pGFlayer.Layer(ii).Visible) Then pGFlayer.Layer(ii).Visible = True
    '                End If
    '            Next ii
    '        End If
    '    Next
    '    pDoc.ActiveView.Refresh()
    'End Sub

    Public Sub AddFeatureShapeFile(ByVal p_Shapefile As String, ByVal p_app As IApplication)
        'Dim pMxDoc As IMxDocument
        'Dim pMap As IMap
        Dim pWorkspaceFactory As IWorkspaceFactory
        Dim pFeatureWorkspace As IFeatureWorkspace
        Dim pFeatureLayer As IFeatureLayer
        Dim pFeatureClass As IFeatureClass
        ' Specify the workspace and the feature class.
        pWorkspaceFactory = New ShapefileWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_Path & "\SHAPE\PRUEBA\", 0)
        pFeatureClass = pFeatureWorkspace.OpenFeatureClass(p_Shapefile)
        ' Prepare a feature layer.
        pFeatureLayer = New FeatureLayer
        pFeatureLayer.FeatureClass = pFeatureClass
        pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName
        ' Add the feature layer to the active map.
        pMxDoc = p_app.Document
        pmap = pMxDoc.FocusMap
        pmap.AddLayer(pFeatureLayer)
        ' Refresh the active view.
        pMxDoc.ActiveView.Refresh()
    End Sub

    Public Sub Zoom_to_Layer(ByVal p_Layer As String)
        'Dim documento As IMxDocument
        'documento = ThisDocument
        'Dim pMap As IMap
        Dim pLayer As ILayer = Nothing
        'If Nom_archivo = "CMALLAS" Then
        '    pLayer = pMxDoc.FocusMap.Layer(0)
        'Else

        Dim afound As Boolean = False
        For p_FClass_index As Integer = 0 To pmap.LayerCount - 1
            If pmap.Layer(p_FClass_index).Name = p_Layer Then
                pLayer = pMxDoc.FocusMap.Layer(p_FClass_index)
                afound = True
                Exit For
            End If
        Next p_FClass_index
        If Not afound Then
            'MsgBox("Layer No Existe.")
            Exit Sub
        End If
        'pLayer = pMxDoc.FocusMap.Layer(p_FClass_index)
        'End If
        pMxDoc.ActiveView.Extent = pLayer.AreaOfInterest
        pMxDoc.ActiveView.Refresh()
    End Sub
    Public Sub PT_CargarFeatureClass_SDE(ByVal mFeatureClass As String, ByVal pApp As IApplication, ByVal lo_opcion As String, ByVal lo_Visible As Boolean)
        Conexion_SDE(pApp)
        Try
            If glo_Version_SDE = "SDE.DEFAULT" Then

                If mFeatureClass = "CQUI0543.GPO_ARE_AREA_RESERVADA_18" Then
                    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                    'ElseIf mFeatureClass = "CQUI0543.GPO_ARE_AREA_RESERVADA_G56" Then
                    '    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                    'ElseIf mFeatureClass = "CQUI0543.GPO_ZUR_ZONA_URBANA_18" Then
                    '    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                    'ElseIf mFeatureClass = "CQUI0543.GPO_ZUR_ZONA_URBANA_G56" Then
                    '    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                ElseIf mFeatureClass = "CQUI0543.GPO_ARE_AREA_RESERVADA_17" Then
                    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                ElseIf mFeatureClass = "FLAT1065.GPO_ARE_AREA_RESERVADA_17" Then
                    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                ElseIf mFeatureClass = "CQUI0543.GPO_ARE_AREA_RESERVADA_19" Then
                    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                    'ElseIf mFeatureClass = "CQUI0543.GPO_ZUR_ZONA_URBANA_17" Then
                    '    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                    'ElseIf mFeatureClass = "CQUI0543.GPO_ZUR_ZONA_URBANA_19" Then
                    '    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                    'ElseIf mFeatureClass = "CQUI0543.GPO_ARE_AREA_RESERVADA_G84" Then
                    '    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                    'ElseIf mFeatureClass = "CQUI0543.GPO_ZUR_ZONA_URBANA_G84" Then
                    '    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                    'Else               
                Else
                    'Solo instancia 5151 para Catastro
                    If mFeatureClass = "GPO_CMI_CATASTRO_MINERO_17" Then
                        Conexion_SDE_Catastro(pApp)
                    ElseIf mFeatureClass = "GPO_CMI_CATASTRO_MINERO_18" Then
                        Conexion_SDE_Catastro(pApp)
                    ElseIf mFeatureClass = "GPO_CMI_CATASTRO_MINERO_19" Then
                    ElseIf mFeatureClass = "CATA_T_17" Or mFeatureClass = "CATA_T_18" Or mFeatureClass = "CATA_T_19" Then
                        Conexion_SDE_Catastro(pApp)
                    ElseIf mFeatureClass = "GPO_AUM_AREA_USO_MINERO_G56" Or mFeatureClass = "GPO_AAC_AREA_ACT_MINERA_G56" Then

                    End If

                    pFeatureClass = pFeatureWorkspace.OpenTable(glo_Owner_Layer_SDE & mFeatureClass)
                End If
            Else

                pFeatureClass = pFeatureWorkspace.OpenTable(Mid(glo_Version_SDE, 1, InStr(glo_Version_SDE, ".")) & mFeatureClass)
            End If
            pFeatureLayer = New FeatureLayerClass
            pFeatureLayer.FeatureClass = pFeatureClass
            Select Case mFeatureClass
                Case gstrFC_Departamento
                    pFeatureLayer.Name = "Departamento"
                    pFeatureLayer_depa = pFeatureLayer
                Case gstrFC_Provincia
                    pFeatureLayer.Name = "Provincia"
                    pFeatureLayer_prov = pFeatureLayer
                Case gstrFC_Distrito
                    pFeatureLayer.Name = "Distrito"
                    pFeatureLayer_dist = pFeatureLayer
                Case "GPO_CMI_CATASTRO_MINERO_17", "GPO_CMI_CATASTRO_MINERO_18", "GPO_CMI_CATASTRO_MINERO_19"

                    pFeatureLayer.Name = "Catastro"
                    pFeatureLayer_cat = pFeatureLayer
                Case "GPO_AUM_AREA_USO_MINERO_G56"
                    pFeatureLayer.Name = "DM_Uso_Minero"
                    pFeatureLayer_usomin = pFeatureLayer
                Case "GPO_AAC_AREA_ACT_MINERA_G56"
                    pFeatureLayer.Name = "DM_Actividad_Minera"
                    pFeatureLayer_Actmin = pFeatureLayer
                Case gstrFC_Cuadricula
                    pFeatureLayer.Name = "Cuadricula Regional"
                Case gstrFC_Rios
                    pFeatureLayer.Name = "Drenaje"
                Case gstrFC_Carretera
                    pFeatureLayer.Name = "Vias"
                Case gstrFC_CPoblado
                    pFeatureLayer.Name = "Centro Poblado"
                Case gstrFC_Frontera
                    pFeatureLayer.Name = "Limite Frontera"
                    pFeatureLayer_fron = pFeatureLayer
                Case "GPO_ARE_AREA_RESERVADA_17", "GPO_ARE_AREA_RESERVADA_18", "GPO_ARE_AREA_RESERVADA_19"
                    pFeatureLayer.Name = "Zona Reservada"
                    pFeatureLayer_rese = pFeatureLayer
                Case gstrFC_AReservada56
                    pFeatureLayer.Name = "Zona Reservada"
                    pFeatureLayer_reseg = pFeatureLayer
                Case "GPO_ZUR_ZONA_URBANA_17", "GPO_ZUR_ZONA_URBANA_18", "GPO_ZUR_ZONA_URBANA_19"
                    pFeatureLayer.Name = "Zona Urbana"
                    pFeatureLayer_urba = pFeatureLayer
                Case gstrFC_Carta
                    pFeatureLayer.Name = "Cuadrangulo"
                    pFeatureLayer_hoja = pFeatureLayer
                Case "GPO_GEO_GEOLOGIA"
                    pFeatureLayer.Name = "Geologia"
                Case gstrFC_ZTraslape
                    pFeatureLayer.Name = "Zona Traslape"
                    pFeatureLayer_tras = pFeatureLayer
                Case "GPO_LZO_LIMITE_ZONAS"
                    pFeatureLayer.Name = "Limite de Zona"
                Case gstrFC_CDistrito
                    pFeatureLayer.Name = "Capitales Distritales"
                    pFeatureLayer_capdist = pFeatureLayer
                Case "GPO_PAI_PAISES"
                    pFeatureLayer.Name = "Frontera Paises"
                Case "GPO_ARE_AREA_RESERVADA_G84"
                    pFeatureLayer.Name = "Zona Reservada"
                Case gstrFC_ZUrbana56
                    pFeatureLayer.Name = "Zona Urbana"
                    pFeatureLayer_urba = pFeatureLayer
                Case "GPO_ZUR_ZONA_URBANA_G84"
                    pFeatureLayer.Name = "Zona Urbana"

                Case "GPT_CAM_CERTIFICACION_AMB_G56"
                    pFeatureLayer.Name = "Certificacion Ambiental"
                    pFeatureLayer_certi = pFeatureLayer

                Case "CQUI0543.GPO_ARE_AREA_RESERVADA_18"
                    pFeatureLayer.Name = "Area Reservada UTM 18"
                Case "CQUI0543.GPO_ARE_AREA_RESERVADA_17"
                    pFeatureLayer.Name = "Area Reservada UTM 17"
                Case "CQUI0543.GPO_ARE_AREA_RESERVADA_19"
                    pFeatureLayer.Name = "Area Reservada UTM 19"
                Case "CQUI0543.GPO_RES_RESERVADA_GEOPSAD56"
                    pFeatureLayer.Name = "Area Reservada GEO"
                Case "CQUI0543.GPO_ZUR_ZONA_URBANA_18"
                    pFeatureLayer.Name = "Zona Urbana UTM 18"
                Case "CQUI0543.GPO_ZUR_ZONA_URBANA_17"
                    pFeatureLayer.Name = "Zona Urbana UTM 17"
                Case "CQUI0543.GPO_ZUR_ZONA_URBANA_19"
                    pFeatureLayer.Name = "Zona Urbana UTM 19"
                Case "CQUI0543.GPO_ZUR_ZONA_URBANA_G84"
                    pFeatureLayer.Name = "Zona Urbana G84"
                Case "CQUI0543.GPO_ARE_AREA_RESERVADA_G84"
                    pFeatureLayer.Name = "Area Reservada G84"
                Case "CQUI0543.GPO_ZUR_ZONA_URBANA_1"
                    pFeatureLayer.Name = "Zona Urbanas GEO"
                Case "CQUI0543.GPO_RESERVA_GEO"
                    pFeatureLayer.Name = "Zona Reserva GEO"
                Case "FLAT1065.GPO_ARE_AREA_RESERVADA_17"
                    pFeatureLayer.Name = "Area Reservada UTM 17"
                    pFeatureLayer_reseg = pFeatureLayer
                Case "CATA_T_17"
                    pFeatureLayer.Name = "Catastro"
                Case "CATA_T_18"
                    pFeatureLayer.Name = "Catastro"
                Case "CATA_T_19"
                    pFeatureLayer.Name = "Catastro"


            End Select
            ' Add the feature layer to the active map.  
            Select Case lo_opcion
                Case "1"
                    pmap.AddLayer(pFeatureLayer)
                    pFeatureLayer.Visible = lo_Visible
                    pMxDoc.ActiveView.Refresh()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message & " -  Error Cargar Feature")
            'MsgBox(ex.Message.ToString)

        End Try
    End Sub

    Public Sub PT_CargarFeatureClass_SDE1(ByVal mFeatureClass As String, ByVal p_App As IApplication, ByVal lo_opcion As String, ByVal lo_Visible As Boolean)
        Conexion_SDE(p_App)
        Try
            If glo_Version_SDE = "SDE.DEFAULT" Then

                If mFeatureClass = "CQUI0543.GPO_ARE_AREA_RESERVADA_18" Then
                    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                    'ElseIf mFeatureClass = "CQUI0543.GPO_ARE_AREA_RESERVADA_G56" Then
                    '    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                    'ElseIf mFeatureClass = "CQUI0543.GPO_ZUR_ZONA_URBANA_18" Then
                    '    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                    'ElseIf mFeatureClass = "CQUI0543.GPO_ZUR_ZONA_URBANA_G56" Then
                    '    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                ElseIf mFeatureClass = "CQUI0543.GPO_ARE_AREA_RESERVADA_17" Then
                    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                ElseIf mFeatureClass = "FLAT1065.GPO_ARE_AREA_RESERVADA_17" Then
                    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                ElseIf mFeatureClass = "CQUI0543.GPO_ARE_AREA_RESERVADA_19" Then
                    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                    'ElseIf mFeatureClass = "CQUI0543.GPO_ZUR_ZONA_URBANA_17" Then
                    '    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                    'ElseIf mFeatureClass = "CQUI0543.GPO_ZUR_ZONA_URBANA_19" Then
                    '    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                    'ElseIf mFeatureClass = "CQUI0543.GPO_ARE_AREA_RESERVADA_G84" Then
                    '    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                    'ElseIf mFeatureClass = "CQUI0543.GPO_ZUR_ZONA_URBANA_G84" Then
                    '    pFeatureClass = pFeatureWorkspace.OpenTable(mFeatureClass)
                    'Else               
                Else
                    'Solo instancia 5151 para Catastro
                    If mFeatureClass = "GPO_CMI_CATASTRO_MINERO_17" Then
                        Conexion_SDE_Catastro(p_App)
                    ElseIf mFeatureClass = "GPO_CMI_CATASTRO_MINERO_18" Then
                        Conexion_SDE_Catastro(p_App)
                    ElseIf mFeatureClass = "GPO_CMI_CATASTRO_MINERO_19" Then
                    ElseIf mFeatureClass = "CATA_T_17" Or mFeatureClass = "CATA_T_18" Or mFeatureClass = "CATA_T_19" Then
                        Conexion_SDE_Catastro(p_App)
                    ElseIf mFeatureClass = "GPO_AUM_AREA_USO_MINERO_G56" Or mFeatureClass = "GPO_AAC_AREA_ACT_MINERA_G56" Then

                    End If

                    pFeatureClass = pFeatureWorkspace.OpenTable(glo_Owner_Layer_SDE & mFeatureClass)
                End If
            Else

                pFeatureClass = pFeatureWorkspace.OpenTable(Mid(glo_Version_SDE, 1, InStr(glo_Version_SDE, ".")) & mFeatureClass)
            End If
            pFeatureLayer = New FeatureLayerClass
            pFeatureLayer.FeatureClass = pFeatureClass
            Select Case mFeatureClass
                Case gstrFC_Departamento
                    pFeatureLayer.Name = "Departamento"
                    pFeatureLayer_depa = pFeatureLayer
                Case gstrFC_Provincia
                    pFeatureLayer.Name = "Provincia"
                    pFeatureLayer_prov = pFeatureLayer
                Case gstrFC_Distrito
                    pFeatureLayer.Name = "Distrito"
                    pFeatureLayer_dist = pFeatureLayer
                Case "GPO_CMI_CATASTRO_MINERO_17", "GPO_CMI_CATASTRO_MINERO_18", "GPO_CMI_CATASTRO_MINERO_19"

                    pFeatureLayer.Name = "Catastro"
                    pFeatureLayer_cat = pFeatureLayer
                Case "GPO_AUM_AREA_USO_MINERO_G56"
                    pFeatureLayer.Name = "DM_Uso_Minero"
                    pFeatureLayer_usomin = pFeatureLayer
                Case "GPO_AAC_AREA_ACT_MINERA_G56"
                    pFeatureLayer.Name = "DM_Actividad_Minera"
                    pFeatureLayer_Actmin = pFeatureLayer
                Case gstrFC_Cuadricula
                    pFeatureLayer.Name = "Cuadricula Regional"
                Case gstrFC_Rios
                    pFeatureLayer.Name = "Drenaje"
                Case gstrFC_Carretera
                    pFeatureLayer.Name = "Vias"
                Case gstrFC_CPoblado
                    pFeatureLayer.Name = "Centro Poblado"
                Case gstrFC_Frontera
                    pFeatureLayer.Name = "Limite Frontera"
                    pFeatureLayer_fron = pFeatureLayer
                Case "GPO_ARE_AREA_RESERVADA_17", "GPO_ARE_AREA_RESERVADA_18", "GPO_ARE_AREA_RESERVADA_19"
                    pFeatureLayer.Name = "Zona Reservada"
                    pFeatureLayer_rese = pFeatureLayer
                Case gstrFC_AReservada56
                    pFeatureLayer.Name = "Zona Reservada"
                    pFeatureLayer_reseg = pFeatureLayer
                Case "GPO_ZUR_ZONA_URBANA_17", "GPO_ZUR_ZONA_URBANA_18", "GPO_ZUR_ZONA_URBANA_19"
                    pFeatureLayer.Name = "Zona Urbana"
                    pFeatureLayer_urba = pFeatureLayer
                Case gstrFC_Carta
                    pFeatureLayer.Name = "Cuadrangulo"
                    pFeatureLayer_hoja = pFeatureLayer
                Case "GPO_GEO_GEOLOGIA"
                    pFeatureLayer.Name = "Geologia"
                Case gstrFC_ZTraslape
                    pFeatureLayer.Name = "Zona Traslape"
                    pFeatureLayer_tras = pFeatureLayer
                Case "GPO_LZO_LIMITE_ZONAS"
                    pFeatureLayer.Name = "Limite de Zona"
                Case gstrFC_CDistrito
                    pFeatureLayer.Name = "Capitales Distritales"
                    pFeatureLayer_capdist = pFeatureLayer
                Case "GPO_PAI_PAISES"
                    pFeatureLayer.Name = "Frontera Paises"
                Case "GPO_ARE_AREA_RESERVADA_G84"
                    pFeatureLayer.Name = "Zona Reservada"
                Case gstrFC_ZUrbana56
                    pFeatureLayer.Name = "Zona Urbana"
                    pFeatureLayer_urba = pFeatureLayer
                Case "GPO_ZUR_ZONA_URBANA_G84"
                    pFeatureLayer.Name = "Zona Urbana"

                Case "GPT_CAM_CERTIFICACION_AMB_G56"
                    pFeatureLayer.Name = "Certificacion Ambiental"
                    pFeatureLayer_certi = pFeatureLayer

                Case "CQUI0543.GPO_ARE_AREA_RESERVADA_18"
                    pFeatureLayer.Name = "Area Reservada UTM 18"
                Case "CQUI0543.GPO_ARE_AREA_RESERVADA_17"
                    pFeatureLayer.Name = "Area Reservada UTM 17"
                Case "CQUI0543.GPO_ARE_AREA_RESERVADA_19"
                    pFeatureLayer.Name = "Area Reservada UTM 19"
                Case "CQUI0543.GPO_RES_RESERVADA_GEOPSAD56"
                    pFeatureLayer.Name = "Area Reservada GEO"
                Case "CQUI0543.GPO_ZUR_ZONA_URBANA_18"
                    pFeatureLayer.Name = "Zona Urbana UTM 18"
                Case "CQUI0543.GPO_ZUR_ZONA_URBANA_17"
                    pFeatureLayer.Name = "Zona Urbana UTM 17"
                Case "CQUI0543.GPO_ZUR_ZONA_URBANA_19"
                    pFeatureLayer.Name = "Zona Urbana UTM 19"
                Case "CQUI0543.GPO_ZUR_ZONA_URBANA_G84"
                    pFeatureLayer.Name = "Zona Urbana G84"
                Case "CQUI0543.GPO_ARE_AREA_RESERVADA_G84"
                    pFeatureLayer.Name = "Area Reservada G84"
                Case "CQUI0543.GPO_ZUR_ZONA_URBANA_1"
                    pFeatureLayer.Name = "Zona Urbanas GEO"
                Case "CQUI0543.GPO_RESERVA_GEO"
                    pFeatureLayer.Name = "Zona Reserva GEO"
                Case "FLAT1065.GPO_ARE_AREA_RESERVADA_17"
                    pFeatureLayer.Name = "Area Reservada UTM 17"
                    pFeatureLayer_reseg = pFeatureLayer
                Case "CATA_T_17"
                    pFeatureLayer.Name = "Catastro"
                Case "CATA_T_18"
                    pFeatureLayer.Name = "Catastro"
                Case "CATA_T_19"
                    pFeatureLayer.Name = "Catastro"


            End Select
            ' Add the feature layer to the active map.  
            Select Case lo_opcion
                Case "1"
                    pMap.AddLayer(pFeatureLayer)
                    pFeatureLayer.Visible = lo_Visible
                    pMxDoc.ActiveView.Refresh()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message & " -  Error Cargar Feature")
            'MsgBox(ex.Message.ToString)

        End Try
    End Sub


    Public Sub HazZoom(ByVal X_min As Double, ByVal Y_min As Double, ByVal X_max As Double, ByVal Y_max As Double, ByVal Factor As Double, ByVal pApp As IApplication)
        Dim pActiveView As IActiveView
        Dim pDisplayTransform As IDisplayTransformation
        Dim pEnvelope As IEnvelope
        pMxDoc = pApp.Document
        pActiveView = pMxDoc.FocusMap
        pDisplayTransform = pActiveView.ScreenDisplay.DisplayTransformation
        pEnvelope = pDisplayTransform.VisibleBounds
        pEnvelope.SetEmpty()
        'Coordenadas UTM
        pEnvelope.XMin = X_min - Factor
        pEnvelope.YMin = Y_min - Factor
        pEnvelope.XMax = X_max + Factor
        pEnvelope.YMax = Y_max + Factor
        pDisplayTransform.VisibleBounds = pEnvelope
        pActiveView.Refresh()
    End Sub
    Public Function MakeATextElement1(ByVal pEste As Double, ByVal pNorte As Double, ByVal texto As String) 'As IElement
        Dim v_Tamaño As Integer
        'DECLARA VARIABLES DEL SIMBOLO

        'Dim v_Tamaño As Integer = 18
        Dim PRgbColor As IRgbColor
        Dim pTextElement As ITextElement
        Dim pTextSymbol As ITextSymbol
        'Dim fnt As IFontDisp
        Dim pElement As IElement
        Dim pPoint As IPoint
        pPoint = New Point
        PRgbColor = New RgbColor
        v_Tamaño = 11
        PRgbColor.RGB = RGB(255, 0, 0)
        pTextElement = New TextElement
        pElement = pTextElement
        pPoint.X = pEste
        pPoint.Y = pNorte
        pElement.Geometry = pPoint
        'FONT DEL TEXTO
        Dim pFontDisp As IFontDisp
        pFontDisp = New stdole.StdFont
        pFontDisp.Name = "TAHOMA"
        pFontDisp.Bold = False

        'ASIGNA SIMBOLOGIA
        pTextSymbol = New TextSymbol
        pTextSymbol.Font = pFontDisp
        pTextSymbol.Color = PRgbColor
        pTextSymbol.Size = v_Tamaño
        pTextElement.Symbol = pTextSymbol
        pTextElement.Text = texto
        MakeATextElement1 = pTextElement
    End Function

    Public Sub Genera_Imagen_DM(ByVal p_Archivo As String)
        'Programa para generar vista previa al DM
        '*******************************************
        Dim pActiveView As IActiveView
        Dim pExport As IExport
        Dim pPixelBoundsEnv As IEnvelope
        Dim exportRECT As tagRECT
        Dim iOutputResolution As Integer
        Dim iScreenResolution As Integer
        Dim hdc As Long
        Try
            pActiveView = pMxDoc.ActiveView
            pmap = pMxDoc.FocusMap
            pExport = New ExportJPEG
            'Ruta del nuevo archivo
            pExport.ExportFileName = glo_Path & "\temporal\" & p_Archivo & "." & Right(pExport.Filter, 3)
            'Propiedades de resolución
            'iScreenResolution = 75  'Resolution
            'iOutputResolution = 200

            iScreenResolution = 75  'Resolution
            iOutputResolution = 144

            pExport.Resolution = iOutputResolution
            With exportRECT
                .left = 0
                .top = 0
                .right = (pActiveView.ExportFrame.right * (iOutputResolution / iScreenResolution)) / 8
                .bottom = (pActiveView.ExportFrame.bottom * (iOutputResolution / iScreenResolution)) / 8
            End With

            'Consiguiendo limites maximos del DM
            pPixelBoundsEnv = New Envelope
            pPixelBoundsEnv.PutCoords(exportRECT.left, exportRECT.top, exportRECT.right, exportRECT.bottom)
            pExport.PixelBounds = pPixelBoundsEnv
            hdc = pExport.StartExporting

            'Exportando la vista
            pActiveView.Output(hdc, pExport.Resolution, exportRECT, Nothing, Nothing)
            pExport.FinishExporting()
            pExport.Cleanup()
        Catch ex As Exception
            'MsgBox("ERROR -- NO SE PUDO GENERAR LA IMAGEN PREVIA Derecho Minero , " & _
            '             "COMUNICARSE CON OSI Y SALIR DE LA SESIÓN DEL ARCIGS", vbInformation, "OBSERVACION...")
        End Try
    End Sub
    Public Sub ClearLayerSelection(ByVal pFeatureLayer As IFeatureLayer)
        Dim pFeatureSelection As IFeatureSelection = pFeatureLayer
        If Not pFeatureSelection Is Nothing Then
            pFeatureSelection.Clear()
        End If
    End Sub
    Public Sub LayerSelection(ByVal pFeatureLayer As IFeatureLayer)
        Dim pFeatureSelection As IFeatureSelection = pFeatureLayer
        If Not pFeatureSelection Is Nothing Then
            pFeatureSelection.Clear()
        Else
            pFeatureLayer = pFeatureSelection.SelectionSet
        End If
    End Sub
    Public Sub Borra_Todo_Feature(ByVal lostrFeature As String, ByVal p_App As IApplication)
        'Programa para borrar temas de la vista
        'On Error GoTo EH
        Dim m_pEnumLayers As IEnumLayer
        Dim m_pLayer As ILayer
        pMxDoc = p_App.Document
        pmap = pMxDoc.FocusMap
        If pmap.LayerCount > 0 Then
            m_pEnumLayers = pmap.Layers
            m_pLayer = m_pEnumLayers.Next
            Do Until m_pLayer Is Nothing
                pmap.DeleteLayer(m_pLayer)
                m_pLayer = m_pEnumLayers.Next
            Loop
        End If
        pMxDoc.UpdateContents()

    End Sub
    Public Sub Quitar_Layer(ByVal lostrFeature As String, ByVal p_App As IApplication)
        Dim m_pEnumLayers As IEnumLayer
        Dim m_pLayer As ILayer
        'pMxDoc = p_App.Document
        pmap = pMxDoc.FocusMap
        If pmap.LayerCount > 0 Then
            m_pEnumLayers = pmap.Layers
            m_pLayer = m_pEnumLayers.Next
            For j As Integer = 0 To pmap.LayerCount - 1
                If pmap.Layer(j).Name = lostrFeature Then
                    pmap.DeleteLayer(m_pLayer)
                    Exit Sub
                Else
                End If
                m_pLayer = m_pEnumLayers.Next
            Next
        End If
        pMxDoc.UpdateContents()
    End Sub

    Public Sub Shade_Poligono(ByVal p_Layer As String, ByVal pApp As IApplication)
        Dim m_pEnumLayers As IEnumLayer
        Dim m_pLayer As ILayer
        Dim pLyr As IGeoFeatureLayer
        Dim pRender As IUniqueValueRenderer
        Dim pFillSymbol As ISimpleFillSymbol
        Dim pLineSymbol As ISimpleLineSymbol
        Dim RgbColor As IRgbColor
        Try
            'pMxDoc = pApp.Document
            pmap = pMxDoc.FocusMap
            If pmap.LayerCount > 0 Then
                m_pEnumLayers = pmap.Layers
                m_pLayer = m_pEnumLayers.Next
                Do Until m_pLayer Is Nothing
                    If InStr(m_pLayer.Name, "ECW") = 0 Then
                        pLyr = m_pLayer
                        pRender = New UniqueValueRenderer
                        If m_pLayer.Name.ToUpper = "Drenaje" Or m_pLayer.Name.ToUpper = "Vias" Then
                            pLineSymbol = New LineFillSymbol
                            pLineSymbol = pLineSymbol.Color
                            pLineSymbol.Width = 0.1
                        Else
                            'If m_pLayer.Name.ToUpper = p_Layer.ToUpper Then
                            pFillSymbol = New SimpleFillSymbol
                            pFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow
                            pLineSymbol = pFillSymbol.Outline
                            pLineSymbol.Width = 0.1
                            RgbColor = New RgbColor
                            Select Case p_Layer
                                Case "Malla_17", "Malla_18", "Malla_19"
                                    RgbColor.RGB = RGB(220, 220, 220)
                                Case "CATA17", "CATA18", "CATA19"
                                    RgbColor.RGB = RGB(0, 0, 255)
                                Case "Zona Reservada"
                                    pLineSymbol.Width = 2

                                    'RgbColor.RGB = RGB(0, 115, 76)
                                    RgbColor.RGB = RGB(56, 168, 0)
                                Case "Zona Urbana"
                                    RgbColor.RGB = RGB(255, 200, 0)
                                Case "Departamento"
                                    RgbColor.RGB = RGB(137, 90, 68)
                                Case "Provincia"
                                    RgbColor.RGB = RGB(0, 120, 0)
                                Case "Distrito"
                                    RgbColor.RGB = RGB(255, 0, 0)
                                Case "Catastro"
                                    'RgbColor.RGB = RGB(100, 100, 100)
                                    RgbColor.RGB = RGB(255, 0, 0)
                                    pLineSymbol.Width = 2
                                Case "Cuadrangulo"
                                    RgbColor.RGB = RGB(100, 100, 100)
                                Case "Limite de Zona"
                                    RgbColor.RGB = RGB(255, 0, 0)
                                Case "Capitales Distritales"
                                    RgbColor.RGB = RGB(0, 197, 255)
                                Case "AreaReserva_100Ha"
                                    RgbColor.RGB = RGB(255, 0, 0)
                                    pLineSymbol.Width = 2
                                Case "Cuadriculas"
                                    RgbColor.RGB = RGB(255, 0, 0)
                                    pLineSymbol.Width = 2
                                Case "Cuadriculas_10HAS"
                                    RgbColor.RGB = RGB(255, 0, 0)
                                    pLineSymbol.Width = 2
                               
                                Case "Cuadriculas_100HAS"
                                    RgbColor.RGB = RGB(255, 0, 0)
                                    pLineSymbol.Width = 2
                                Case "Drenaje"
                                    'pLineSymbol = New LineFillSymbol
                                    'pLineSymbol = pLineSymbol.Color
                                    'pLineSymbol.Width = 0.1
                                    RgbColor.RGB = RGB(0, 197, 255)
                                Case "Vias"
                                    'pLineSymbol = New LineFillSymbol
                                    'pLineSymbol = pLineSymbol.Color
                                    'pLineSymbol.Width = 0.1
                                    RgbColor.RGB = RGB(255, 0, 0)
                                Case "Hojas IGN"
                                    RgbColor.RGB = RGB(0, 197, 255)
                                Case "Cuadricula Regional"
                                    RgbColor.RGB = RGB(255, 0, 0)
                                Case "Rectangulo"
                                    RgbColor.RGB = RGB(255, 0, 0)
                                Case "Poligono"
                                    RgbColor.RGB = RGB(255, 0, 0)
                                Case "Zona Reservada"
                                    RgbColor.RGB = RGB(56, 168, 0)
                                Case loStrShapefile_ld
                                    RgbColor.RGB = RGB(255, 0, 0)
                                    pLineSymbol.Width = 2

                            End Select
                            pLineSymbol.Color = RgbColor
                            pFillSymbol.Outline = pLineSymbol
                            pRender.DefaultLabel = ""
                            pRender.DefaultSymbol = pFillSymbol
                            pRender.UseDefaultSymbol = True
                            pLyr.Renderer = pRender
                            Exit Do
                        End If
                    End If

                    m_pLayer = m_pEnumLayers.Next
                Loop
                pMxDoc.UpdateContents()
                pMxDoc.ActiveView.Refresh()
            End If
        Catch ex As Exception
        End Try
    End Sub


    Public Sub Actualizar_DM(ByVal lo_Zona As String)
        Try
            Dim pSpatRefFact As ISpatialReferenceFactory
            Dim pPCS As IProjectedCoordinateSystem = Nothing
            pSpatRefFact = New SpatialReferenceEnvironment
            Select Case lo_Zona
                Case 17
                    pPCS = pSpatRefFact.CreateProjectedCoordinateSystem(24877)
                Case 18
                    pPCS = pSpatRefFact.CreateProjectedCoordinateSystem(24878)
                Case 19
                    pPCS = pSpatRefFact.CreateProjectedCoordinateSystem(24879)
            End Select
            pmap.SpatialReference = pPCS
            pMxDoc.ActivatedView.Refresh()
        Catch ex As Exception
            'MsgBox(ex.Message & " -  Sistema de Proyección")
        End Try
    End Sub
    Public Sub Selecciona_Radio(ByRef m_pApp As IApplication)
        'Public Sub Selecciona_Radio(ByVal p_xMin As Double, ByVal p_yMin As Double, ByVal p_xMax As Double, ByVal p_yMax As Double, ByRef m_pApp As IApplication)
        pMxDoc = m_pApp.Document
        pmap = pMxDoc.FocusMap
        Dim rubberEnv As IRubberBand = New RubberEnvelopeClass()
        Dim geom As IGeometry
        Dim tempEnv As IEnvelope = New EnvelopeClass()
        Dim DisplayTransformation As IDisplayTransformation
        Dim SelectRect As tagRECT
        DisplayTransformation = pMxDoc.ActivatedView.ScreenDisplay.DisplayTransformation
        SelectRect.left = 0
        SelectRect.top = 0
        SelectRect.right = DisplayTransformation.DeviceFrame.right
        SelectRect.bottom = DisplayTransformation.DeviceFrame.bottom
        Dim dispTrans As IDisplayTransformation = pMxDoc.ActivatedView.ScreenDisplay.DisplayTransformation
        dispTrans.TransformRect(tempEnv, SelectRect, 4)
        geom = CType(tempEnv, IGeometry)
        Dim spatialReference As ISpatialReference = pmap.SpatialReference
        geom.SpatialReference = spatialReference
        geom.Envelope.XMin = 0 ' p_xMin
        geom.Envelope.YMin = 0 ' p_yMin
        geom.Envelope.XMax = 0 'p_xMax
        geom.Envelope.YMax = 0 'p_yMax
        pmap.SelectByShape(geom, Nothing, True)
        pMxDoc.ActivatedView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeoSelection, Nothing, pMxDoc.ActivatedView.ScreenDisplay.DisplayTransformation.FittedBounds)
    End Sub
    Public Sub Selecciona_Envelope(ByVal p_xMin As Object, ByVal p_yMin As Object, ByVal p_xMax As Object, ByVal p_yMax As Object, ByRef m_pApp As IApplication)
        pMxDoc = m_pApp.Document
        pmap = pMxDoc.FocusMap
        Dim rubberEnv As IRubberBand = New RubberEnvelopeClass()
        Dim geom As IGeometry
        Dim tempEnv As IEnvelope = New EnvelopeClass()
        Dim DisplayTransformation As IDisplayTransformation
        Dim SelectRect As tagRECT
        DisplayTransformation = pMxDoc.ActivatedView.ScreenDisplay.DisplayTransformation
        SelectRect.left = 0
        SelectRect.top = 0
        SelectRect.right = DisplayTransformation.DeviceFrame.right
        SelectRect.bottom = DisplayTransformation.DeviceFrame.bottom
        Dim dispTrans As IDisplayTransformation = pMxDoc.ActivatedView.ScreenDisplay.DisplayTransformation
        dispTrans.TransformRect(tempEnv, SelectRect, 4)
        geom = CType(tempEnv, IGeometry)
        Dim spatialReference As ISpatialReference = pmap.SpatialReference
        geom.SpatialReference = spatialReference
        p_xMin.text = geom.Envelope.XMin
        p_yMin.text = geom.Envelope.YMin
        p_xMax.text = geom.Envelope.XMax
        p_yMax.text = geom.Envelope.YMax
    End Sub


    Public Sub PintarPoligono(ByVal loArchivo As String, _
                                ByVal loCapa As String, _
                                ByVal loStyle As String, _
                                ByVal loCampo As String, ByVal p_App As IApplication)
        Dim pTable As ITable
        pMxDoc = p_App.Document
        Dim p_Layer As ILayer
        p_Layer = pMxDoc.FocusMap.Layer(1)
        'pFeatws = pWorkspace
        pTable = p_Layer 'WorkspaceFactory.OpenTable(loCapa)
        Dim pQueryDef As IQueryDef
        Dim pRow As IRow
        Dim pCursor As ICursor
        Dim pFeatureWorkspace As IFeatureWorkspace
        Dim pDataset As IDataset
        pDataset = pTable
        pFeatureWorkspace = pDataset.Workspace
        pQueryDef = pFeatureWorkspace.CreateQueryDef
        With pQueryDef
            .Tables = pDataset.Name ' Fully qualified table name
            .SubFields = "DISTINCT(" & loCampo & ")"
            '.SubFields = "DISTINCT(" & sFieldName & ")"
            pCursor = .Evaluate
        End With
        pRow = pCursor.NextRow
        Dim x As Integer
        x = 0
        Dim valor(100) As String
        Dim pUniqueValueRenderer As IUniqueValueRenderer
        'Dim pSym As IFillSymbol
        ' Define the renderer.
        pUniqueValueRenderer = New UniqueValueRenderer
        pUniqueValueRenderer.FieldCount = 1
        pUniqueValueRenderer.Field(0) = loCampo '"CODI"
        Dim pStyleStorage As IStyleGalleryStorage
        Dim pStyleGallery As IStyleGallery
        'Dim pStyleClass As IStyleGalleryClass
        'Dim pMxDoc As IMxDocument
        pMxDoc = p_App.Document
        pStyleGallery = New StyleGallery 'pMxDoc.StyleGallery
        pStyleStorage = pStyleGallery
        Dim pEnumStyleGall As IEnumStyleGalleryItem
        Dim pStyleItem As IStyleGalleryItem

        'Dim pLineSymbol As ILineSymbol
        Dim pFillSymbol As IFillSymbol = Nothing
        'Dim pMarkerSymbol As IMarkerSymbol
        '*******************  Verificar ******************
        Dim pStyleGlry As IStyleGallery
        pStyleGlry = New StyleGallery
        'Dim pRgbColor As IRgbColor
        'Dim pStylePath As String
        Dim pStylStor As IStyleGalleryStorage
        pStylStor = pStyleGlry
        pStylStor.AddFile(loStyle)
        pEnumStyleGall = pStyleGallery.Items("Fill Symbols", loStyle, "Default")
        pEnumStyleGall.Reset()
        pStyleItem = pEnumStyleGall.Next
        Do Until pRow Is Nothing
            If pFillSymbol Is Nothing Then
                pEnumStyleGall.Reset()
                pStyleItem = pEnumStyleGall.Next
                'pStyleItem.Name = pRow.Value(0)
            End If
            Do While Not pStyleItem Is Nothing   'Loop through and access each marker
                pFillSymbol = Nothing
                If pStyleItem.ID = pRow.Value(0) Then
                    pFillSymbol = pStyleItem.Item
                    Exit Do
                End If
                If pStyleItem.ID > pRow.Value(0) Then
                    pRow = pCursor.NextRow
                    If pRow Is Nothing Then Exit Do
                Else
                    pStyleItem = pEnumStyleGall.Next
                End If
            Loop
            If pStyleItem Is Nothing Then
            Else
                If Len(pStyleItem.Name) > 0 And Not (pRow Is Nothing) Then
                    pUniqueValueRenderer.AddValue(pRow.Value(0), "", pFillSymbol)
                    pUniqueValueRenderer.Label(pRow.Value(0)) = pStyleItem.Name
                End If
            End If
            pRow = pCursor.NextRow
        Loop
        'Dim pMap As IMap
        Dim pLayer As IFeatureLayer
        Dim pGeoFeatureLayer As IGeoFeatureLayer
        pMxDoc = p_App.Document
        pmap = pMxDoc.FocusMap
        pLayer = pmap.Layer(0)
        pGeoFeatureLayer = pLayer
        pGeoFeatureLayer.Renderer = pUniqueValueRenderer
        pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, Nothing)
        pMxDoc.UpdateContents()
    End Sub

    Public Sub Genera_Plano_Reporte(ByVal papp As IApplication)
        On Error GoTo EH
        'Dim rutaPlantilla As String
        ''rutaPlantilla = "U:\DATOS\LEYENDA\Plantillas\plantilla_cartaign2.mxt"
        ''rutaPlantilla = "Q:\ARCVIEW\VBA\gis9.2\final\PLANTILLA\Plantilla_listadodm1.mxt"
        'rutaPlantilla = "D:\Archivos de programa\ArcGIS\Bin\Templates\SouthAmerica.mxt"
        'Dim rutalayout As IGxFile
        'rutalayout = New GxMap
        'rutalayout.Path = rutaPlantilla
        'Dim pGxPageLayout As IGxMapPageLayout
        'pGxPageLayout = rutalayout
        'Dim pPageLayout As IPageLayout
        'pPageLayout = pGxPageLayout.PageLayout
        'pMxDoc = pApp.Document
        'pPageLayout.ReplaceMaps(pMxDoc.Maps)
        'pMxDoc.PageLayout = pPageLayout


        Dim rutaPlantilla As String
        'rutaPlantilla = "F:\GEOCATMIN\GEOCATMIN_170309\Plantilla\plantilla_cartaign2.mxt"
        rutaPlantilla = "F:\GEOCATMIN\GEOCATMIN_170309\Plantilla\PlantillaDM.mxt"
        Dim rutalayout As IGxFile
        rutalayout = New GxMap
        rutalayout.Path = rutaPlantilla
        Dim pGxPageLayout As ESRI.ArcGIS.Catalog.IGxMapPageLayout
        pGxPageLayout = rutalayout
        Dim pPageLayout As ESRI.ArcGIS.Carto.IPageLayout
        pPageLayout = pGxPageLayout.PageLayout
        'Dim documento As IMxDocument
        'documento = pMxDoc
        pPageLayout.ReplaceMaps(pMxDoc.Maps)
        pMxDoc.PageLayout = pPageLayout


        Exit Sub
EH:

        MsgBox("ERROR -- NO SE TERMINO CORRECTAMENTE AL ABRIR EL PLANO REPORTE, " & _
                     "COMUNICARSE CON OSI Y SALIR DE LA SESIÓN DEL ARCIGS", vbInformation, "OBSERVACION...")

    End Sub

    Public Sub AgregarTextosLayoutreporte(ByVal pApp As IApplication, ByVal v_codigo As String, ByVal v_Zona As String, _
    ByVal v_carta As String, ByVal v_Nombre As String, ByVal v_Titular As String, ByVal v_Padron As String, _
    ByVal v_Partida As String, ByVal v_Naturaleza As String, ByVal v_Jefatura As String, _
    ByVal v_Hect_Form As String, ByVal v_Vertice As String, ByVal v_TipoDerecho As String, _
    ByVal v_Hectareas As String, ByVal coordenada_DM As Object)

        'PROGRAMA PARA GENERAR REPORTE PREVIO DEL DM
        '*********************************************
        On Error GoTo EH
        Dim caso_simula As String = "Evaluacion"

        Dim pEnv As IEnvelope
        Dim pFont As IFontDisp
        Dim pPageLayout As IPageLayout
        Dim pActiveView As IActiveView
        Dim pGraphicsContainer As IGraphicsContainer
        Dim pElement As IElement
        Dim pTextElement As ITextElement
        Dim pPoint As IPoint
        Dim pTxtSym As IFormattedTextSymbol
        pMxDoc = pApp.Document
        Dim contatexto As Integer
        Dim myColor As IRgbColor
        pPageLayout = pMxDoc.PageLayout
        pPageLayout.Page.Units = esriUnits.esriCentimeters
        pActiveView = pPageLayout
        pGraphicsContainer = pPageLayout
        'Para obtener fecha
        Dim fecha_plano As Date
        fecha_plano = Now 'Date
        Dim posi_y As Double
        Dim posi_y1 As Double
        posi_y = 26.5
        posi_y1 = 25.6
        'Colocando textos fijos al plano
        For contatexto = 1 To 9
            pTextElement = New TextElement
            'Punto de ubicación del texto
            pEnv = New Envelope
            pPoint = New Point
            If contatexto = 1 Then
                If caso_simula = "simula" Then
                    pTextElement.Text = "CODIGO DEL DM :  " & v_codigo '000000001"
                ElseIf caso_simula = "Evaluacion" Then
                    pTextElement.Text = "CODIGO DEL DM :  " & v_codigo
                End If
                pPoint.X = 5.5
                pPoint.Y = 26.5
                pPoint.X = 10.2
                pPoint.Y = 25.6
            ElseIf contatexto = 2 Then
                If caso_simula = "simula" Then
                    pTextElement.Text = "NOMBRE DEL DM : DM SIMULADO"
                ElseIf caso_simula = "Evaluacion" Then
                    pTextElement.Text = "NOMBRE DEL DM :" & v_Nombre
                End If
                pPoint.X = 5.5
                pPoint.Y = 25.9
                pPoint.X = 10.2
                pPoint.Y = 25.0#
            ElseIf contatexto = 3 Then
                pTextElement.Text = "CARTA : " & " " & v_carta
                pPoint.X = 5.5
                pPoint.Y = 25.3
                pPoint.X = 10.2
                pPoint.Y = 24.4
            ElseIf contatexto = 4 Then
                pTextElement.Text = "SITUACION :" & "situacion_dm"
                pPoint.X = 5.5
                pPoint.Y = 24.7
                pPoint.X = 10.2
                pPoint.Y = 23.8
            ElseIf contatexto = 5 Then
                pTextElement.Text = "TITULAR :" & v_Titular
                pPoint.X = 5.5
                pPoint.Y = 24.1
                pPoint.X = 10.2
                pPoint.Y = 23.2
            ElseIf contatexto = 6 Then
                pTextElement.Text = "ZONA :" & v_Zona
                pPoint.X = 5.5
                pPoint.Y = 23.5
                pPoint.X = 10.2
                pPoint.Y = 22.6
            ElseIf contatexto = 7 Then
                pTextElement.Text = "TIPO DE DERECHO MINERO :" & v_TipoDerecho
                pPoint.X = 5.5
                pPoint.Y = 22.9
                pPoint.X = 10.2
                pPoint.Y = 22.0#
            ElseIf contatexto = 8 Then
                pTextElement.Text = "COORDENADAS DEL D.M."
                pPoint.X = 5.5
                pPoint.Y = 22.3
                pPoint.X = 10.2
                pPoint.Y = 21.4
            ElseIf contatexto = 9 Then
                pTextElement.Text = "VERTICE          ESTE             NORTE"
                pPoint.X = 5.5
                pPoint.Y = 21.7
                pPoint.X = 10.2
                pPoint.Y = 20.8
            End If

            pEnv.UpperRight = pPoint
            pElement = pTextElement
            pElement.Geometry = pEnv
            pTxtSym = New TextSymbol


            pFont = New StdFont
            pFont.Name = "Tahoma"
            pFont.Size = 9
            pFont.Bold = False
            pTxtSym.Font = pFont
            myColor = New RgbColor
            myColor.RGB = RGB(0, 0, 0)
            pTxtSym.Color = myColor

            'Propiedades del Simbolo

            pTxtSym.Angle = 0
            pTxtSym.RightToLeft = False
            pTxtSym.VerticalAlignment = esriTextVerticalAlignment.esriTVABaseline
            pTxtSym.HorizontalAlignment = esriTextHorizontalAlignment.esriTHAFull
            pTxtSym.CharacterSpacing = 20
            pTxtSym.Case = esriTextCase.esriTCNormal

            pTextElement.Symbol = pTxtSym
            pGraphicsContainer.AddElement(pTextElement, 1)
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)
        Next
        'CONSULTANDO A LA BASE DE DATOS PARA OBTENER COORDENADAS
        posi_y1 = 20.8
        posi_y = 20.2
        For j As Integer = 0 To 500
            pTextElement = New TextElement
            pEnv = New Envelope
            pPoint = New Point
            If coordenada_DM(j).v = 0 Then Exit Sub
            '    'ESCRIBIENDO EN EL REPORTE
            pTextElement.Text = "    " & RellenarComodin(coordenada_DM(j).v, 3, "0") & "            " & Format(coordenada_DM(j).x, "###,###") & "          " & Format(coordenada_DM(j).y, "###,###")
            pPoint.X = 5.5
            pPoint.Y = posi_y1 - 0.6
            pPoint.X = 10.2
            pPoint.Y = posi_y
            pEnv.UpperRight = pPoint
            pElement = pTextElement
            pElement.Geometry = pEnv
            pTxtSym = New TextSymbol

            'fuente del texto
            pFont = New StdFont
            pFont.Name = "Tahoma"
            pFont.Size = 9
            pFont.Bold = False
            pTxtSym.Font = pFont
            myColor = New RgbColor
            myColor.RGB = RGB(0, 0, 0)
            pTxtSym.Color = myColor

            'Propiedades del Simbolo
            pTxtSym.Angle = 0
            pTxtSym.RightToLeft = False
            pTxtSym.VerticalAlignment = esriTextVerticalAlignment.esriTVABaseline
            pTxtSym.HorizontalAlignment = esriTextHorizontalAlignment.esriTHAFull
            pTxtSym.CharacterSpacing = 20
            pTxtSym.Case = esriTextCase.esriTCNormal
            pTextElement.Symbol = pTxtSym
            pGraphicsContainer.AddElement(pTextElement, 1)
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)
            posi_y1 = posi_y1 - 0.6
            posi_y = posi_y - 0.6
            'filas_dm = cursor_filas.NextRow
            'Loop
        Next j
        Exit Sub

EH:
        MsgBox("ERROR -- NO SE PUEDE GEMERAR REPORTE PREVIO, COMUNICARSE CON OSI " & _
                   "Y SALIR DE LA SESIÓN DEL ARCGIS", vbInformation, "OBSERVACION")

    End Sub
    Public Sub Insertar_Imagen_DM(ByVal pApp As IApplication)
        On Error GoTo EH
        Dim PCOMANDO As ICommandItem
        Dim pPictElement As IPictureElement
        pPictElement = New JpgPictureElement
        If Dir(glo_pathTMP & "\VistaPrevia.jpg", vbArchive) <> "" Then
            pPictElement.ImportPictureFromFile(glo_pathTMP & "\VistaPrevia.jpg")
            Dim pElement As IElement
            pElement = pPictElement
            pMxDoc = pApp.Document
            Dim vistaactiva As IActiveView
            vistaactiva = pMxDoc.PageLayout
            Dim pEnv As IEnvelope
            pEnv = vistaactiva.Extent
            'Definiendo limites de la imagen
            Dim xMin As Double
            Dim yMin As Double
            Dim xMax As Double
            Dim yMax As Double
            xMin = 1.7
            xMax = 8.8
            yMax = 25.5
            yMin = 20.9
            pEnv.PutCoords(xMin, yMin, xMax, yMax)  ' aplica limites
            pElement.Geometry = pEnv
            Dim pGC As IGraphicsContainer
            pGC = vistaactiva
            pGC.AddElement(pElement, 0)
        Else
            Exit Sub
        End If
        Exit Sub
EH:
        'MsgBox("ERROR -- NO SE TERMINO CORRECTAMENTE DE INSERTAR IMAGEN EN PLANO, " & _
        '"COMUNICARSE CON OSI Y SALIR DE LA SESIÓN DEL ARCIGS", vbInformation, "OBSERVACION...")
    End Sub
    Public Sub AddImagen(ByVal sFullPath As String, ByVal loTipo As String, ByVal carta As String, ByVal pApp As IApplication, ByVal lo_Visible As Boolean)
        'Dim pRasterLayer As RasterLayer
        Dim Raster As IRasterLayer
        Try
            pMxDoc = pApp.Document
            pmap = pMxDoc.FocusMap
            Raster = New RasterLayer
            Raster.CreateFromFilePath(sFullPath)
            If loTipo = "1" Then
                Raster.Name = "Carta_" & carta
            ElseIf loTipo = "2" Then
                Raster.Name = "Imagen_Satelital"
            ElseIf loTipo = "3" Then
                Raster.Name = "Imagen_Geologica_" & carta
            End If
            Raster.Renderer.ResamplingType = rstResamplingTypes.RSP_CubicConvolution
            'Raster.PyramidPresent = True
            'Raster.DataFrameExtent.Envelope=
            Dim pLayerEffects As ILayerEffects
            pLayerEffects = Raster
            If pLayerEffects.SupportsTransparency Then pLayerEffects.Transparency = 0
            Raster.Visible = lo_Visible
            pmap.AddLayer(Raster)
            pMxDoc.UpdateContents()
            pMxDoc.ActiveView.Refresh()
        Catch ex As Exception
            'MsgBox("No se encuentra la carta disponible para este DM en Raster, Comunicarse con él Area de Catastro Minero", MsgBoxStyle.Information, "Observación..")
        End Try
    End Sub

    Public Sub ShowUniqueValues(ByVal pTable As ITable, ByVal sFieldName As String, ByVal loSQL As String, ByVal lo_Zona As String, ByVal p_filtro As String)
        Dim lo_codigo(1000) As String
        Dim pQueryDef As IQueryDef
        Dim pRow As IRow
        Dim pCursor As ICursor
        Dim pFeatureWorkspace As IFeatureWorkspace
        Dim pDataset As IDataset

        pDataset = pTable
        pFeatureWorkspace = pDataset.Workspace
        pQueryDef = pFeatureWorkspace.CreateQueryDef
        With pQueryDef
            .Tables = pDataset.Name ' Fully qualified table name
            '.WhereClause = p_Filtro
            .SubFields = "DISTINCT(" & sFieldName & ")"
            pCursor = .Evaluate
        End With
        pRow = pCursor.NextRow
        Dim i As Integer = 0
        Dim lo_codigo_filtro As String = ""
        Dim lo_busca As String = ""
        Do Until pRow Is Nothing
            lo_codigo_filtro = lo_codigo_filtro & "'" & pRow.Value(0) & "', "
            i = i + 1
            pRow = pCursor.NextRow
        Loop
        If i = 0 Then
            MsgBox("No hay información de las Coordenadas especificadas ", MsgBoxStyle.Information, "Aviso")
            Exit Sub
        End If
        lo_codigo_filtro = "CODIGOU IN ( " & Mid(lo_codigo_filtro, 1, Len(lo_codigo_filtro) - 2) & ")"

    End Sub
    Public Sub Pinta(ByVal p_App As IApplication)
        'Dim pMxDoc As IMxDocument
        pMxDoc = p_App.Document 'ThisDocument

        Dim pStyleItems As IEnumStyleGalleryItem
        pStyleItems = pMxDoc.StyleGallery.Items("Fill Symbols", "ESRI.style", "Default")
        Dim pGalleryItem As IStyleGalleryItem

        Dim pRenderer As ISimpleRenderer
        Dim pGeoFeatureLayer As IGeoFeatureLayer
        Dim i As Long
        For i = 0 To pMxDoc.FocusMap.LayerCount - 1
            If (TypeOf pMxDoc.FocusMap.Layer(i) Is IFeatureLayer) Then
                pGeoFeatureLayer = pMxDoc.FocusMap.Layer(i)
                If (pGeoFeatureLayer.FeatureClass.ShapeType = esriGeometryType.esriGeometryPolygon) Then ' esriGeometryPolygon) Then
                    pStyleItems.Reset()
                    pGalleryItem = pStyleItems.Next
                    Do While (Not pGalleryItem Is Nothing)
                        If (pGeoFeatureLayer.Name = pGalleryItem.Name) Then
                            pRenderer = pGeoFeatureLayer.Renderer
                            pRenderer.Symbol = pGalleryItem.Item
                            Exit Do
                        End If
                        pGalleryItem = pStyleItems.Next
                    Loop
                End If
            End If
        Next i
        pMxDoc.ActivatedView.PartialRefresh(esriViewDrawPhase.esriViewGeography, Nothing, Nothing)
    End Sub
    Function f_Intercepta_Envelope(ByVal loFeature As String, ByVal pGeom As IGeometry, ByVal p_App As IApplication)
        Dim pFLayer As IFeatureLayer = Nothing
        pMxDoc = p_App.Document
        pmap = pMxDoc.FocusMap
        Dim afound As Boolean
        For A As Integer = 0 To pmap.LayerCount - 1
            If pmap.Layer(A).Name = loFeature Then
                pFLayer = pMxDoc.FocusMap.Layer(A)
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
        'pEnvelope.XMin = xMin
        'pEnvelope.YMin = yMin
        'pEnvelope.XMax = xMax
        'pEnvelope.YMax = yMax
        pDisplayTransform.VisibleBounds = pGeom ' pEnvelope
        'pActiveView.Refresh()
        Dim pSpatialFilter As ISpatialFilter
        pSpatialFilter = New SpatialFilter
        With pSpatialFilter
            .Geometry = pGeom ' pEnvelope
            .SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects ' esriSpatialRelIntersects
        End With
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
                        lostr_Join_Codigos = lostr_Join_Codigos & pRow.Value(pRow.Fields.FindField("CD_DEPA")) & ","
                    Case "Provincia"
                        lostr_Join_Codigos = lostr_Join_Codigos & pRow.Value(pRow.Fields.FindField("CD_PROV")) & ","
                    Case "Distrito"
                        lostr_Join_Codigos = lostr_Join_Codigos & pRow.Value(pRow.Fields.FindField("CD_DIST")) & ","
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
                lostr_Join_Codigos = "CD_PROV IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
            Case "Distrito"
                lostr_Join_Codigos = "CD_DIST IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
            Case "Zona Urbana"
                lostr_Join_Codigos = "CODIGO IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
            Case "Zona Reservada"
                lostr_Join_Codigos = "CODIGO IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
            Case Else
                lostr_Join_Codigos = "OBJECTID IN (" & Mid(lostr_Join_Codigos, 1, Len(lostr_Join_Codigos) - 1) & ")"
        End Select
        Return lostr_Join_Codigos
    End Function
    Public Sub Add_ShapeFile2(ByVal p_Shapefile As String, ByVal p_App As IApplication, ByVal aliastema As String)
        Try
            Dim pWorkspaceFactory As IWorkspaceFactory
            pWorkspaceFactory = New ShapefileWorkspaceFactory
            Dim pWorkSpace As IFeatureWorkspace
            pWorkSpace = pWorkspaceFactory.OpenFromFile(glo_pathTMP & "\", 0)
            Dim pClass As IFeatureClass
            pClass = pWorkSpace.OpenFeatureClass(p_Shapefile)
            Dim pLayer As IFeatureLayer
            pLayer = New FeatureLayer
            pLayer.FeatureClass = pClass
            pLayer.Name = pClass.AliasName
            pMxDoc.FocusMap.AddLayer(pLayer)
            If aliastema = "codigo" Then
                pLayer.Name = "DM_" & v_codigo
            ElseIf aliastema = "Prioritarios" Then
                pLayer.Name = "Prioritarios" & v_codigo
            ElseIf aliastema = "Simu" Then
                pLayer.Name = "Catastro"
            ElseIf aliastema = "DMxregion" Then
                pLayer.Name = "DMxregion"
            ElseIf aliastema = "Antecesor" Then
                pLayer.Name = "Antecesor"
            ElseIf aliastema = "reserva" Then
                pLayer.Name = "Area Reservap"
            ElseIf aliastema = "Situacion" Then
                pLayer.Name = "Situacion_DM"
            Else
                pLayer.Name = "Areadispo_" & v_codigo
            End If
            pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, Nothing)
            pMxDoc.UpdateContents()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub generaplanoventa()
        Dim formulario2 As New Frm_formatoplanos
        Dim pRubberBand As IRubberBand
        pmap = pMxDoc.FocusMap
        Dim pActiveView As IActiveView
        pActiveView = pmap
        pRubberBand = New RubberEnvelope
        pActiveView.Extent = pRubberBand.TrackNew(pActiveView.ScreenDisplay, Nothing)
        pActiveView.Refresh()
        If pmap.LayerCount = 0 Then
            MsgBox("NO EXISTE TEMAS EN LA VISTA PARA GENERAR PLANO", vbCritical, "OBSERVACION...")
            Exit Sub
        End If
        formulario2.Show()
    End Sub
    Public Sub PT_CambiarCordSysDataFrame(ByVal pApp As IApplication)
        ' Part 1: Define the active map.
        Dim pMxDoc As IMxDocument
        Dim pMap As IMap
        Dim pPCS, pPCS1 As ISpatialReference 'IProjectedCoordinateSystem
        pMxDoc = pApp.Document
        pMap = pMxDoc.FocusMap
        pPCS = pMap.SpatialReference
        ' Part 2: Create a Projected Coordinate System.
        Dim pSpatRefFact As ISpatialReferenceFactory
        pSpatRefFact = New SpatialReferenceEnvironment
        Try
            pPCS1 = pSpatRefFact.CreateGeographicCoordinateSystem(ESRI.ArcGIS.Geometry.esriSRGeoCSType.esriSRGeoCS_WGS1984) 'pSpatRefFact.CreateProjectedCoordinateSystem(ESRI.ArcGIS.Geometry.esriSRGeoCSType.esriSRGeoCS_WGS1984)
            If pPCS.Name <> pPCS1.Name Then pMap.SpatialReference = pPCS1
        Catch ex As Exception
        End Try
    End Sub
    Public Sub busca_Layer(ByVal pApp As IApplication)
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim afound As Boolean = False
        For A As Integer = 0 To pmap.LayerCount - 1
            If pmap.Layer(A).Name = "Catastro" Then
                pFeatLayer = pmap.Layer(A)
                pFeatLayer.Name = "Catastro1"
                afound = True
                Exit For

            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra el Layer")
            Exit Sub
        End If

        pFeatLayer.Name = "Catastro1"
        pMxDoc.ActiveView.Refresh()

    End Sub

    Public Sub busca_Layer1(ByVal pApp As IApplication, ByVal capa As String)
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim afound As Boolean = False
        If capa = "Catastro" Then
            capa = "X1"
        End If
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = capa Then
                pFeatLayer = pMap.Layer(A)
                pFeatLayer.Name = "Catastro"
                afound = True
                Exit For
            Else
                pFeatLayer = pMap.Layer(A)
                pFeatLayer.Name = loStrShapefile_ld
                afound = True
                Exit For
            End If
        Next A
        'If Not afound Then
        'MsgBox("No se encuentra la capa")
        'Exit Sub
        'End If

        pMxDoc.ActiveView.Refresh()

    End Sub


    Public Sub Shade_Poligono_blanco(ByVal pApp As IApplication)
        Dim m_pEnumLayers As IEnumLayer
        Dim m_pLayer As ILayer
        Dim pLyr As IGeoFeatureLayer
        Dim pRender As IUniqueValueRenderer
        Dim pFillSymbol As ISimpleFillSymbol
        Dim pLineSymbol As ISimpleLineSymbol
        Dim RgbColor As IRgbColor
        Try
            pMxDoc = pApp.Document
            pmap = pMxDoc.FocusMap
            If pmap.LayerCount > 0 Then
                m_pEnumLayers = pmap.Layers
                m_pLayer = m_pEnumLayers.Next
                Do Until m_pLayer Is Nothing
                    If InStr(m_pLayer.Name, "ECW") = 0 Then
                        pLyr = m_pLayer
                        pRender = New UniqueValueRenderer
                        pFillSymbol = New SimpleFillSymbol
                        pFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow
                        pLineSymbol = pFillSymbol.Outline
                        pLineSymbol.Width = 0.1
                        RgbColor = New RgbColor
                        RgbColor.RGB = RGB(255, 255, 255)

                        pLineSymbol.Color = RgbColor
                        pFillSymbol.Outline = pLineSymbol
                        pRender.DefaultLabel = ""
                        pRender.DefaultSymbol = pFillSymbol
                        pRender.UseDefaultSymbol = True
                        pLyr.Renderer = pRender

                    End If
                    m_pLayer = m_pEnumLayers.Next
                Loop

                pMxDoc.UpdateContents()
                pMxDoc.ActiveView.Refresh()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub Consulta_DM_LibDenu(ByVal pApp As IApplication, ByVal clbLayer As Windows.Forms.CheckedListBox, ByVal p_existe As Object)


        Try
            Dim cls_Eval As New Cls_evaluacion
            Dim cls_catastro As New cls_DM_1
            Dim lodtbLeyenda As New DataTable
            fecha_archi = DateTime.Now.Ticks.ToString()
            'Dim loStrShapefile As String = "Catastro" & DateTime.Now.Ticks.ToString()
            Dim loStrShapefile As String = "Catastro" & fecha_archi

            Dim cls_Prueba As New cls_Prueba

            cls_catastro.Borra_Todo_Feature("", pApp) : cls_catastro.Limpiar_Texto_Pantalla(pApp)
            cls_catastro.Delete_Rows_FC_GDB("Malla_" & v_zona_dm)
            cls_catastro.Delete_Rows_FC_GDB("Mallap_" & v_zona_dm)
            cls_catastro.Load_FC_GDB("Malla_" & v_zona_dm, "", pApp, True)
            cls_catastro.Load_FC_GDB("Mallap_" & v_zona_dm, "", pApp, True)
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & v_zona_dm, m_application, "1", False)
            cls_catastro.Actualizar_DM(v_zona_dm)
            cls_catastro.Leer_datos_libredenu()
            ' cls_catastro.Add_ShapeFile("Libreden", pApp)
            arch_cata = "Libreden"
            cls_Eval.consultacapaDM("", "LibreDen", "Catastro")

            cls_catastro.Exportando_Temas("", "Catastro", pApp)
            cls_catastro.Quitar_Layer("Catastro", pApp)
            cls_catastro.Add_ShapeFile(loStrShapefile2, pApp)
            cls_catastro.creandotabla_Rep_Libredenu()


            'cls_catastro.Obtienedatos_libredenu("", pApp)

            'Procedimientos de Libre Denunciabilidad


            Dim lodtbDemarca As New DataTable
            Dim pFeatLayer As IFeatureLayer = Nothing
            Dim pFeatureClass As IFeatureClass

            Dim afound As Boolean = False
            Dim parametro As Integer = 0
            Dim valor1 As Integer = 0
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Libreden" Then
                    pFeatLayer = pMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If
            pFeatureClass = pFeatLayer.FeatureClass
            Dim pFeatureCursor As IFeatureCursor


            Dim pTable As ITable
            Dim pWorkspaceFactory1 As IWorkspaceFactory
            pWorkspaceFactory1 = New ShapefileWorkspaceFactory
            Dim pFWS As IFeatureWorkspace
            pFWS = pWorkspaceFactory1.OpenFromFile(glo_pathTMP, 0)
            pTable = pFWS.OpenTable("Reporte1_Libredenu" & fecha_archi)
            Dim ptableCursor As ICursor
            Dim pfields3 As Fields
            Dim pRow As IRow
            Dim v_tp_rese As String
            Dim v_cd_rese As String
            Dim v_nm_rese As String
            Dim v_nm_rese1 As String
            Dim v_clase As String
            Dim v_categori As String
            Dim v_nivelSuper As String
            Dim v_tipoSuper1 As String
            Dim v_tipoSuper As String
            Dim v_nm_urba As String
            Dim v_nm_urba1 As String
            Dim v_nonmbre As String
            pFeatureCursor = pFeatureClass.Search(Nothing, False)
            Dim pFeature As IFeature
            pFeature = pFeatureCursor.NextFeature
            Dim contador As Integer = 0

            'Dim v_codigo As String
            Dim lodbtExisteAR As New DataTable
            Dim lodbtExistezu As New DataTable
            Dim lodbtExisteZT As New DataTable
            Dim lodbtExistePT As New DataTable
            Dim lodbtExisteDM As New DataTable
            Dim lodbtExiste_paises As New DataTable
            Dim lodbtExiste_fron As New DataTable
            Dim lodbtExiste_ZU_AF As New DataTable
            Dim lodbtExiste_XY As New DataTable
            Dim conta_t As Integer
            v_nm_rese = ""
            v_nm_urba = ""
            v_tipoSuper = ""
            Dim v_xmin As Double
            Dim v_ymin As Double
            Dim v_xmax As Double
            Dim v_ymax As Double


            Dim lo_xMin As Double
            Dim lo_xMax As Double
            Dim lo_yMin As Double
            Dim lo_yMax As Double

            Dim loStrShapefile1 As String

            Do Until pFeature Is Nothing
                loStrShapefile_ld = "Dm_" & DateTime.Now.Ticks.ToString()
                contador = contador + 1
                v_codigo = pFeature.Value(pFeatureCursor.FindField("CODIGOU"))

                gloZona = pFeature.Value(pFeatureCursor.FindField("ZONA"))

                lodbtExiste_XY = cls_Oracle.FT_Obtiene_datos_XYminmax(v_codigo, gstrFC_Catastro_Minero & gloZona)
                If lodbtExiste_XY.Rows.Count > 0 Then
                    v_xmin = lodbtExiste_XY.Rows(0).Item("MINX")
                    v_xmax = lodbtExiste_XY.Rows(0).Item("MAXX")
                    v_ymin = lodbtExiste_XY.Rows(0).Item("MINY")
                    v_ymax = lodbtExiste_XY.Rows(0).Item("MAXY")

                    lo_xMin = (v_xmin - 4000)
                    lo_xMax = (v_xmax + 4000)
                    lo_yMin = (v_ymin - 4000)
                    lo_yMax = (v_ymax + 4000)
                    'loStrShapefile1 = "DM_" & v_codigo
                    cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & gloZona, m_application, "2", False)
                    cls_catastro.Actualizar_DM(gloZona)
                    Dim lo_Filtro As String = cls_catastro.f_Intercepta_FC("Catastro", lo_xMin, lo_yMin, lo_xMax, lo_yMax, pApp, loStrShapefile_ld)
                    cls_Prueba.AddFieldDM(loStrShapefile_ld)


                    'cls_catastro.Add_ShapeFile(loStrShapefile1, pApp)
                    cls_Eval.consultacapaDM(v_codigo, "", loStrShapefile_ld)
                    cls_Eval.seleccionadmsegunditancia(loStrShapefile_ld, "Interceptando")


                    cls_Eval.consultacapaDM(v_codigo, "", loStrShapefile_ld)
                    cls_Eval.seleccionadmsegunditancia(loStrShapefile_ld, "colidantes")
                    cls_Eval.actualizaregistrostema("XVECINOS")   ' Actualiza registros en el tema para casos VE (DM vecinos)
                    cls_Eval.actualizaregistrostema("XDM")
                    cls_Eval.EJECUTACRITERIOS_Libredenu()
                    'cls_catastro.Quitar_Layer(loStrShapefile_ld, pApp)
                    cls_catastro.busca_Layer1(pApp, loStrShapefile_ld)

                    cls_catastro.UpdateValue(lo_Filtro, pApp)
                    cls_catastro.Update_Value_DM(pApp, v_codigo)
                    lodtbLeyenda = cls_Prueba.f_Genera_Leyenda_DM("Catastro", lo_Filtro, pApp)

                    'lodtbLeyenda = cls_Prueba.f_Genera_Leyenda_DM("Catastro", "", pApp)
                    cls_Prueba.Poligono_Color_1("Catastro", lodtbLeyenda, glo_pathStyle & "\Catastro.style", "LEYENDA", "", pApp)


                    cls_catastro.rotulatexto_dm("Catastro", pApp)
                    cls_catastro.Genera_Malla_UTM(lo_xMin, lo_yMin, lo_xMax, lo_yMax, v_zona_dm, pApp)
                    cls_catastro.Rotular_texto_DM("Mallap_" & v_zona_dm, v_zona_dm, pApp)
                    cls_catastro.Quitar_Layer("Mallap_" & v_zona_dm, pApp)
                    cls_catastro.Style_Linea_GDB("Malla_" & v_zona_dm, glo_pathStyle & "\malla.style", "CLASE", pApp, "GDB")
                    cls_catastro.Zoom_to_Layer("Catastro")
                    cls_catastro.busca_Layer1(pApp, "Catastro")
                    cls_catastro.Quitar_Layer("Libreden", pApp)

                    ' cls_catastro.Shade_Poligono("Catastro", m_application)
                   

                End If


                'loStrShapefile1 = "DM_" & v_codigo
                'cls_catastro.f_Intercepta_FC("Catastro", lo_xMin, lo_yMin, lo_xMax, lo_yMax, m_application, loStrShapefile1)
                v_nonmbre = pFeature.Value(pFeatureCursor.FindField("CONCESION"))


                pRow = pTable.CreateRow
                pfields3 = pTable.Fields
                pRow.Value(pfields3.FindField("CG_CODIGO")) = v_codigo
                pRow.Value(pfields3.FindField("EST_SUPER")) = "LIBRE"
                pRow.Value(pfields3.FindField("NOMBRE_DM")) = v_nonmbre
                Try
                    lodbtExisteAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(5, gstrFC_Catastro_Minero & gloZona, gstrFC_AReservada & gloZona, v_codigo)
                    If lodbtExisteAR.Rows.Count = 0 Then
                        Try
                            lodbtExisteAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(6, gstrFC_Catastro_Minero & gloZona, gstrFC_AReservada & gloZona, v_codigo)
                        Catch ex As Exception
                        End Try

                    End If
                Catch ex As Exception
                End Try

                'Try
                'lodbtExisteAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(5, gstrFC_Catastro_Minero & gloZona, gstrFC_AReservada & gloZona, v_codigo)
                'Catch ex As Exception
                'End Try

                If lodbtExisteAR.Rows.Count >= 1 Then
                    For contador1 As Integer = 0 To lodbtExisteAR.Rows.Count - 1
                        v_tp_rese = lodbtExisteAR.Rows(contador1).Item("TIPO")
                        v_cd_rese = lodbtExisteAR.Rows(contador1).Item("CODIGO")
                        v_nm_rese1 = lodbtExisteAR.Rows(contador1).Item("NOMBRE")
                        v_clase = lodbtExisteAR.Rows(contador1).Item("CLASE")
                        v_nivelSuper = lodbtExisteAR.Rows(contador1).Item("SUPER")
                        pfields3 = pTable.Fields
                        ptableCursor = pTable.Search(Nothing, False)
                        v_nm_rese = v_nm_rese & "," & v_nm_rese1
                        'Dim conta_t As Long
                        conta_t = conta_t + 1
                        valor1 = valor1 + 1
                        If v_nivelSuper = "1" Then
                            'pRow.Value(pfields3.FindField("NIV_SUPER")) = "TOTAL"
                            v_tipoSuper1 = "Sup. Total en AR"
                        ElseIf v_nivelSuper = "2" Then
                            'pRow.Value(pfields3.FindField("NIV_SUPER")) = "PARCIAL"
                            v_tipoSuper1 = "Sup. Parcial en AR"
                        End If
                        v_tipoSuper = v_tipoSuper & "," & v_tipoSuper1

                        If v_tp_rese = "AREA NATURAL" Then
                            If v_clase = "ANP" Then
                                pRow.Value(pfields3.FindField("CLASE_N")) = "SI"
                            ElseIf v_clase = "AMORTIGUAMIENTO" Then
                                pRow.Value(pfields3.FindField("CLASE_A")) = "SI"
                            Else
                                pRow.Value(pfields3.FindField("CLASE_V")) = "SI"
                            End If
                        ElseIf v_tp_rese = "ZONA ARQUEOLOGICA" Then
                            pRow.Value(pfields3.FindField("ZONA_A")) = "SI"
                        ElseIf v_tp_rese = "PROYECTO ESPECIAL" Then
                            pRow.Value(pfields3.FindField("PROY_E")) = "SI"
                        ElseIf v_tp_rese = "PUERTO Y/O AEROPUERTOS" Then
                            pRow.Value(pfields3.FindField("PUERTO")) = "SI"
                        ElseIf v_tp_rese = "OTRA AREA RESTRINGIDA" Then
                            pRow.Value(pfields3.FindField("OT")) = "SI"
                        ElseIf v_tp_rese = "ZONA DE RESERVA TURISTICA" Then
                            pRow.Value(pfields3.FindField("ZONA_T")) = "SI"
                        ElseIf v_tp_rese = "ANAP" Then
                            pRow.Value(pfields3.FindField("ANAP")) = "SI"

                        End If

                    Next contador1
                    v_nm_rese = Right(v_nm_rese, Len(v_nm_rese) - 1)
                    pRow.Value(pfields3.FindField("NM_RESE")) = v_nm_rese

                    v_nm_rese = ""

                End If


                'Dim lodbtExisteAu As New DataTable
                'Try
                '    lodbtExistezu = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(3, gstrFC_Catastro_Minero & gloZona, gstrFC_ZUrbana & gloZona, v_codigo)
                'Catch ex As Exception
                'End Try

                Try
                    lodbtExistezu = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(13, gstrFC_Catastro_Minero & gloZona, gstrFC_ZUrbana & gloZona, v_codigo)
                    If lodbtExistezu.Rows.Count = 0 Then
                        Try
                            lodbtExistezu = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(14, gstrFC_Catastro_Minero & gloZona, gstrFC_ZUrbana & gloZona, v_codigo)
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception
                End Try


                If lodbtExistezu.Rows.Count >= 1 Then
                    For contador1 As Integer = 0 To lodbtExistezu.Rows.Count - 1
                        v_categori = lodbtExistezu.Rows(contador1).Item("CATEGORIA")
                        v_nivelSuper = lodbtExistezu.Rows(contador1).Item("SUPER")
                        v_nm_urba1 = lodbtExisteAR.Rows(contador1).Item("NOMBRE")
                        pfields3 = pTable.Fields
                        ptableCursor = pTable.Search(Nothing, False)
                        v_nm_urba = v_nm_urba & "," & v_nm_urba1
                        If v_categori = "AREA URBANA" Or v_categori = "AREA URBANA Y/O EXPANSION URBANA" Or v_categori = "AREA URBANA Y EXPANSION URBANA" Then
                            pRow.Value(pfields3.FindField("URBA_U")) = "SI"
                        ElseIf v_categori = "EXPANSION URBANA" Then
                            pRow.Value(pfields3.FindField("URBA_EX")) = "SI"
                        End If

                        If v_nivelSuper = "1" Then
                            'pRow.Value(pfields3.FindField("NIV_SUPER")) = "TOTAL"
                            v_tipoSuper1 = "Sup. Total en ZU"
                        ElseIf v_nivelSuper = "2" Then
                            'pRow.Value(pfields3.FindField("NIV_SUPER")) = "PARCIAL"
                            v_tipoSuper1 = "Sup. Parcial en ZU"
                        End If
                        v_tipoSuper = v_tipoSuper & "," & v_tipoSuper1

                        v_nm_urba = Right(v_nm_urba, Len(v_nm_urba) - 1)
                        pRow.Value(pfields3.FindField("NM_URBA")) = v_nm_urba
                        v_nm_urba = ""
                    Next contador1


                End If
                If v_tipoSuper <> "" Then
                    v_tipoSuper = Right(v_tipoSuper, Len(v_tipoSuper) - 1)
                    pRow.Value(pfields3.FindField("NIV_SUPER")) = v_tipoSuper
                End If
                v_tipoSuper = ""
                pRow.Store()

                'Verificacion para Dm superpuesto a Zona de Traslape
                '----------------------------------------------------
                Try
                    lodbtExisteZT = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(16, gstrFC_Catastro_Minero & gloZona, gstrFC_ZTraslape, v_codigo)

                Catch ex As Exception
                End Try


                If lodbtExisteZT.Rows.Count >= 1 Then
                    'For contador1 As Integer = 0 To lodbtExisteZT.Rows.Count - 1
                    'v_nivelSuper = lodbtExisteZT.Rows(contador1).Item("SUPER")
                    pfields3 = pTable.Fields
                    ptableCursor = pTable.Search(Nothing, False)
                    'If v_nivelSuper = "1" Then
                    pRow.Value(pfields3.FindField("ZONA_T")) = "SI"
                    'End If
                    'Next contador1
                End If


                'Verificacion para Dm superpuesto a Dm protegidos
                '----------------------------------------------------
                Try
                    lodbtExistePT = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(17, gstrFC_Catastro_Minero & gloZona, gstrFC_Catastro_Minero & gloZona, v_codigo)

                Catch ex As Exception
                End Try

                If lodbtExistePT.Rows.Count >= 1 Then
                    'For contador1 As Integer = 0 To lodbtExistePT.Rows.Count - 1
                    'v_nivelSuper = lodbtExistePT.Rows(contador1).Item("SUPER")
                    pfields3 = pTable.Fields
                    ptableCursor = pTable.Search(Nothing, False)
                    'If v_nivelSuper = "1" Then
                    pRow.Value(pfields3.FindField("PROTG")) = "SI"
                    'End If
                    ' Next contador1
                End If
                'Verificacion para Dm superpuesto a Dm 
                '----------------------------------------------------

                Try
                    lodbtExisteDM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(15, gstrFC_Catastro_Minero & gloZona, gstrFC_Catastro_Minero & gloZona, v_codigo)

                Catch ex As Exception
                End Try
                If lodbtExisteDM.Rows.Count >= 1 Then
                    pfields3 = pTable.Fields
                    ptableCursor = pTable.Search(Nothing, False)
                    pRow.Value(pfields3.FindField("DM")) = "SI"
                End If

                'Dm superpuesto a Frontera de Paises (determina en que pais esta


                lodbtExiste_paises = cls_Oracle.F_Obtiene_Datos_DMXPAISES(v_codigo)
                If lodbtExiste_paises.Rows.Count = 0 Then
                    Dim NM As String
                    validad_paises = ""
                    For i As Integer = 0 To lodbtExiste_paises.Rows.Count - 1
                        NM = lodbtExiste_paises.Rows(i).Item("NOMBRE").ToString
                        If NM <> "PERU" Then
                            validad_paises = validad_paises & "/" & NM

                            pfields3 = pTable.Fields
                            ptableCursor = pTable.Search(Nothing, False)
                            pRow.Value(pfields3.FindField("FRONT_P")) = "SI"


                        End If
                    Next i
                    validad_paises = Right(validad_paises, Len(validad_paises) - 1)
                End If

                'Dm supuesto a 1000m de la linea de frontera

                Try
                    lodbtExiste_fron = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(18, gstrFC_Catastro_Minero & gloZona, gstrFC_Frontera, v_codigo)

                Catch ex As Exception
                End Try

                If lodbtExiste_fron.Rows.Count >= 1 Then
                    pfields3 = pTable.Fields
                    ptableCursor = pTable.Search(Nothing, False)
                    pRow.Value(pfields3.FindField("FRONTERA")) = "SI"
                End If

                'Dm supuesto a 1000m de la zona Urbana

                Try
                    lodbtExiste_ZU_AF = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(19, gstrFC_Catastro_Minero & gloZona, gstrFC_ZUrbana & gloZona, v_codigo)
                Catch ex As Exception
                End Try
                If lodbtExiste_ZU_AF.Rows.Count >= 1 Then
                    pfields3 = pTable.Fields
                    ptableCursor = pTable.Search(Nothing, False)
                    pRow.Value(pfields3.FindField("URB_AL")) = "SI"
                End If


                'pRow.Store()
                If lodbtExisteAR.Rows.Count > 0 Or lodbtExistezu.Rows.Count > 0 Or lodbtExisteZT.Rows.Count > 0 Or lodbtExistePT.Rows.Count > 0 Or lodbtExisteDM.Rows.Count > 0 Or lodbtExiste_paises.Rows.Count > 1 Or lodbtExiste_fron.Rows.Count > 0 Or lodbtExiste_ZU_AF.Rows.Count > 0 Then
                    pfields3 = pTable.Fields
                    ptableCursor = pTable.Search(Nothing, False)
                    pRow.Value(pfields3.FindField("EST_SUPER")) = "SUPERPUESTO"
                    'ElseIf lodbtExisteAR.Rows.Count = 0 And lodbtExistezu.Rows.Count = 0 And lodbtExisteZT.Rows.Count = 0 And lodbtExistePT.Rows.Count = 0 And lodbtExisteDM.Rows.Count = 0 And lodbtExiste_paises.Rows.Count = 1 And lodbtExiste_fron.Rows.Count = 0 And lodbtExiste_ZU_AF.Rows.Count = 0 Then
                    '    pfields3 = pTable.Fields
                    '    ptableCursor = pTable.Search(Nothing, False)
                    '    pRow.Value(pfields3.FindField("EST_SUPER")) = "LIBRE"

                End If
                pRow.Store()
                'Dim lo_Filtro As String = cls_catastro.f_Intercepta_FC("Catastro", lo_xMin, lo_yMin, lo_xMax, lo_yMax, pApp, loStrShapefile_ld)
                cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_" & v_zona_dm, m_application, "1", False)

                Dim lo_Filtro_Area_Reserva As String = cls_catastro.f_Intercepta_FC("Zona Reservada", lo_xMin, lo_yMin, lo_xMax, lo_yMax, pApp)
                If lo_Filtro_Area_Reserva = "" Then
                    cls_catastro.Quitar_Layer("Zona Reservada", m_application)
                Else

                    cls_catastro.Expor_Tema("Zona Reservada", True, pApp)
                    cls_catastro.Quitar_Layer("Zona Reservada", m_application)
                    cls_catastro.Add_ShapeFile1("Zona Reservada" & fecha_archi, m_application, "Zona Reservada")
                    cls_catastro.Shade_Poligono("Zona Reservada", m_application)

                    cls_catastro.rotulatexto_dm("Zona Reservada", pApp)
                End If

                cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_" & v_zona_dm, m_application, "1", False)
                Dim lo_Filtro_Zona_Urbana As String = cls_catastro.f_Intercepta_FC("Zona Urbana", lo_xMin, lo_yMin, lo_xMax, lo_yMax, pApp)
                If lo_Filtro_Zona_Urbana = "" Then
                    cls_catastro.Quitar_Layer("Zona Urbana", m_application)
                Else
                    cls_catastro.DefinitionExpression(lo_Filtro_Zona_Urbana, pApp, "Zona Urbana")
                    cls_catastro.Color_Poligono_Simple(m_application, "Zona Urbana")
                    cls_catastro.rotulatexto_dm("Zona Urbana", pApp)
                End If


                pFeature = pFeatureCursor.NextFeature
                Exit Sub

            Loop

            MsgBox("TERMINO")










            'cls_catastro.Obtienedatos_libredenu("", m_application)

            'Dim lo_Filtro As String = cls_catastro.f_Intercepta_FC("Catastro", lo_xMin, lo_yMin, lo_xMax, lo_yMax, pApp, loStrShapefile)
            'If v_existe = False Then
            '    p_existe.text = "-1"
            '    cls_Eval.cierra_ejecutable()
            '    cls_catastro.Borra_Todo_Feature("", pApp)
            '    cls_catastro.Limpiar_Texto_Pantalla(pApp)
            '    ' MsgBox("No existe Derechos Mineros en el Area de Interes indicado..", MsgBoxStyle.Information, "Observación...")
            '    Exit Sub
            'ElseIf v_existe = True Then
            '    p_existe.text = "1"
            '    cls_catastro.Quitar_Layer("Catastro", pApp)
            '    cls_Prueba.AddFieldDM(loStrShapefile) : cls_catastro.Quitar_Layer(loStrShapefile, pApp)
            '    cls_catastro.Add_ShapeFile(loStrShapefile, pApp)
            '    cls_catastro.UpdateValue(lo_Filtro, pApp)
            '    lodtbLeyenda = cls_Prueba.f_Genera_Leyenda_DM("Catastro", lo_Filtro, pApp)
            '    cls_Prueba.Poligono_Color_1("Catastro", lodtbLeyenda, glo_pathStyle & "\Catastro.style", "LEYENDA", "", pApp)
            '    'cls_catastro.ShowLabel_DM("Catastro", pApp)
            '    cls_catastro.rotulatexto_dm("Catastro", pApp)
            '    cls_catastro.Genera_Malla_UTM(lo_xMin, lo_yMin, lo_xMax, lo_yMax, v_zona_dm, pApp)
            '    cls_catastro.Rotular_texto_DM("Mallap_" & v_zona_dm, v_zona_dm, pApp)
            '    cls_catastro.Quitar_Layer("Mallap_" & v_zona_dm, pApp)
            '    ' cls_catastro.Style_Linea_GDB("Malla_" & v_zona_dm, glo_pathStyle & "\malla.style", "CLASE", pApp, "GDB")
            '    glo_xMin = lo_xMin : glo_yMin = lo_yMin : glo_xMax = lo_xMax : glo_yMax = lo_yMax
            'End If

        Catch ex As Exception
            MsgBox(ex.Message & " -  Error Cargar Feature")
            'MsgBox(ex.Message.ToString)

        End Try
    End Sub
    Public Sub Consulta_DM_PadronMinero(ByVal pApp As IApplication, ByVal clbLayer As Windows.Forms.CheckedListBox, ByVal p_existe As Object)
        Dim cls_Eval As New Cls_evaluacion
        Dim cls_catastro As New cls_DM_1
        Dim lodtbLeyenda As New DataTable
        fecha_archi = DateTime.Now.Ticks.ToString()
        'Dim loStrShapefile As String = "Catastro" & fecha_archi
        'Dim cls_Prueba As New cls_Prueba
        'cls_catastro.Borra_Todo_Feature("", pApp) : cls_catastro.Limpiar_Texto_Pantalla(pApp)
        'cls_catastro.Delete_Rows_FC_GDB("Malla_" & v_zona_dm)
        'cls_catastro.Delete_Rows_FC_GDB("Mallap_" & v_zona_dm)
        'cls_catastro.Load_FC_GDB("Malla_" & v_zona_dm, "", pApp, True)
        'cls_catastro.Load_FC_GDB("Mallap_" & v_zona_dm, "", pApp, True)
        For i As Integer = 17 To 19
            If i = 17 Then
                cls_catastro.PT_CargarFeatureClass_SDE("CATA_T_17", m_application, "1", False)
                v_Zona = "17"

            ElseIf i = 18 Then
                cls_catastro.PT_CargarFeatureClass_SDE("CATA_T_18", m_application, "1", False)
                v_Zona = "18"
            ElseIf i = 19 Then
                cls_catastro.PT_CargarFeatureClass_SDE("CATA_T_19", m_application, "1", False)
                v_Zona = "19"
            End If


            'cls_catastro.PT_CargarFeatureClass_SDE("CATA_T_" & i, m_application, "1", False)


            'cls_catastro.Actualizar_DM(v_zona_dm)
            'cls_catastro.Leer_datos_libredenu()
            'arch_cata = "Libredenu"
            'cls_Eval.consultacapaDM("", "LibreDenu", "Catastro")

            'cls_catastro.Exportando_Temas("", "Catastro", pApp)
            'cls_catastro.Quitar_Layer("Catastro", pApp)
            'cls_catastro.Add_ShapeFile(loStrShapefile2, pApp)
            'cls_catastro.Creando_temporal_padronminero()
            cls_catastro.Obtienedatos_PadronMinero("", pApp)

        Next i


        'Dim lo_Filtro As String = cls_catastro.f_Intercepta_FC("Catastro", lo_xMin, lo_yMin, lo_xMax, lo_yMax, pApp, loStrShapefile)
        'If v_existe = False Then
        '    p_existe.text = "-1"
        '    cls_Eval.cierra_ejecutable()
        '    cls_catastro.Borra_Todo_Feature("", pApp)
        '    cls_catastro.Limpiar_Texto_Pantalla(pApp)
        '    ' MsgBox("No existe Derechos Mineros en el Area de Interes indicado..", MsgBoxStyle.Information, "Observación...")
        '    Exit Sub
        'ElseIf v_existe = True Then
        '    p_existe.text = "1"
        '    cls_catastro.Quitar_Layer("Catastro", pApp)
        '    cls_Prueba.AddFieldDM(loStrShapefile) : cls_catastro.Quitar_Layer(loStrShapefile, pApp)
        '    cls_catastro.Add_ShapeFile(loStrShapefile, pApp)
        '    cls_catastro.UpdateValue(lo_Filtro, pApp)
        '    lodtbLeyenda = cls_Prueba.f_Genera_Leyenda_DM("Catastro", lo_Filtro, pApp)
        '    cls_Prueba.Poligono_Color_1("Catastro", lodtbLeyenda, glo_pathStyle & "\Catastro.style", "LEYENDA", "", pApp)
        '    'cls_catastro.ShowLabel_DM("Catastro", pApp)
        '    cls_catastro.rotulatexto_dm("Catastro", pApp)
        '    cls_catastro.Genera_Malla_UTM(lo_xMin, lo_yMin, lo_xMax, lo_yMax, v_zona_dm, pApp)
        '    cls_catastro.Rotular_texto_DM("Mallap_" & v_zona_dm, v_zona_dm, pApp)
        '    cls_catastro.Quitar_Layer("Mallap_" & v_zona_dm, pApp)
        '    ' cls_catastro.Style_Linea_GDB("Malla_" & v_zona_dm, glo_pathStyle & "\malla.style", "CLASE", pApp, "GDB")
        '    glo_xMin = lo_xMin : glo_yMin = lo_yMin : glo_xMax = lo_xMax : glo_yMax = lo_yMax
        'End If
    End Sub

    Public Sub Consulta_XY(ByVal pApp As IApplication, ByVal clbLayer As Windows.Forms.CheckedListBox, ByVal p_existe As Object)
        Dim cls_Eval As New Cls_evaluacion
        Dim cls_catastro As New cls_DM_1
        Dim lodtbLeyenda As New DataTable
        Dim loStrShapefile As String = "Catastro" & DateTime.Now.Ticks.ToString()
        Dim cls_Prueba As New cls_Prueba
        If Not IsNumeric(v_dato1) Or Len(v_dato1.ToString) <> 6 Then
            MsgBox(".::Ingrese Correctamente la Coordenada Este.::")
            'v_dato1.Focus()
            Exit Sub
        End If
        If Not IsNumeric(v_dato2) Or Len(v_dato2.ToString) <> 7 Then
            MsgBox(".::Ingrese Correctamente la Coordenada Norte.::")
            'v_dato2.Focus()
            Exit Sub
        End If
        If Not IsNumeric(v_radio) Then
            MsgBox(".::Ingrese Correctamente el Radio.::")
            'Me.txtRadio.Focus()
            Exit Sub
        End If
        Dim lo_xMin = (v_dato1 - v_radio * 1000) : Dim lo_xMax = (v_dato1 + v_radio * 1000)
        Dim lo_yMin = (v_dato2 - v_radio * 1000) : Dim lo_yMax = (v_dato2 + v_radio * 1000)
        cls_catastro.Borra_Todo_Feature("", pApp) : cls_catastro.Limpiar_Texto_Pantalla(pApp)
        cls_catastro.Delete_Rows_FC_GDB("Malla_" & v_zona_dm)
        cls_catastro.Delete_Rows_FC_GDB("Mallap_" & v_zona_dm)
        cls_catastro.Load_FC_GDB("Malla_" & v_zona_dm, "", pApp, True)
        cls_catastro.Load_FC_GDB("Mallap_" & v_zona_dm, "", pApp, True)
        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & v_zona_dm, m_application, "1", False)
        'cls_catastro.Conexion_SDE(m_application)
        cls_catastro.Actualizar_DM(v_zona_dm)
        Dim lo_Filtro As String = cls_catastro.f_Intercepta_FC("Catastro", lo_xMin, lo_yMin, lo_xMax, lo_yMax, pApp, loStrShapefile)
        If v_existe = False Then
            p_existe.text = "-1"
            cls_Eval.cierra_ejecutable()
            cls_catastro.Borra_Todo_Feature("", pApp)
            cls_catastro.Limpiar_Texto_Pantalla(pApp)
            ' MsgBox("No existe Derechos Mineros en el Area de Interes indicado..", MsgBoxStyle.Information, "Observación...")
            Exit Sub
        ElseIf v_existe = True Then
            p_existe.text = "1"
            cls_catastro.Quitar_Layer("Catastro", pApp)
            cls_Prueba.AddFieldDM(loStrShapefile) : cls_catastro.Quitar_Layer(loStrShapefile, pApp)
            cls_catastro.Add_ShapeFile(loStrShapefile, pApp)
            cls_catastro.UpdateValue(lo_Filtro, pApp)
            lodtbLeyenda = cls_Prueba.f_Genera_Leyenda_DM("Catastro", lo_Filtro, pApp)
            cls_Prueba.Poligono_Color_1("Catastro", lodtbLeyenda, glo_pathStyle & "\Catastro.style", "LEYENDA", "", pApp)
            'cls_catastro.ShowLabel_DM("Catastro", pApp)
            cls_catastro.rotulatexto_dm("Catastro", pApp)
            cls_catastro.Genera_Malla_UTM(lo_xMin, lo_yMin, lo_xMax, lo_yMax, v_zona_dm, pApp)
            cls_catastro.Rotular_texto_DM("Mallap_" & v_zona_dm, v_zona_dm, pApp)
            cls_catastro.Quitar_Layer("Mallap_" & v_zona_dm, pApp)
            ' cls_catastro.Style_Linea_GDB("Malla_" & v_zona_dm, glo_pathStyle & "\malla.style", "CLASE", pApp, "GDB")
            glo_xMin = lo_xMin : glo_yMin = lo_yMin : glo_xMax = lo_xMax : glo_yMax = lo_yMax
        End If
    End Sub
    Public Sub consulta_limitesmaximos(ByVal pApp As IApplication, ByVal clbLayer As Windows.Forms.CheckedListBox)
        Dim cls_catastro As New cls_DM_1
        Dim lodtbLeyenda As New DataTable
        Dim cls_Prueba As New cls_Prueba
        Dim lo_xMin = v_dato1 : Dim lo_xMax = v_dato3
        Dim lo_yMin = v_dato2 : Dim lo_yMax = v_dato4
        Dim v_Boo_Dpto As Boolean = False
        Dim v_Boo_Prov As Boolean = False
        Dim v_Boo_Dist As Boolean = False
        Dim v_Boo_cp As Boolean = False
        Dim v_Boo_fron As Boolean = False
        If Not IsNumeric(v_dato1) Or Len(v_dato1.ToString) <> 6 Then
            MsgBox(".::Ingrese Correctamente la Coordenada del Este Mínimo...")
            Exit Sub
        End If
        If Not IsNumeric(v_dato2) Or Len(v_dato2.ToString) <> 7 Then
            MsgBox(".::Ingrese Correctamente la Coordenada del Norte Mínimo...")
            Exit Sub
        End If
        If Not IsNumeric(v_dato3) Or Len(v_dato3.ToString) <> 6 Then
            MsgBox(".::Ingrese Correctamente la Coordenada del Este Máximo...")
            Exit Sub
        End If
        If Not IsNumeric(v_dato4) Or Len(v_dato4.ToString) <> 7 Then
            MsgBox(".::Ingrese Correctamente la Coordenada del Norte Máximo...")
            Exit Sub
        End If
        Dim loStrShapefile As String = "Catastro" & DateTime.Now.Ticks.ToString()
        cls_catastro.Delete_Rows_FC_GDB("Malla_" & v_zona_dm)
        cls_catastro.Delete_Rows_FC_GDB("Mallap_" & v_zona_dm)
        cls_catastro.Load_FC_GDB("Malla_" & v_zona_dm, "", pApp, True)
        cls_catastro.Load_FC_GDB("Mallap_" & v_zona_dm, "", pApp, True)
        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_ZTraslape, m_application, "1", True)
        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & v_zona_dm, m_application, "1", False)
        'cls_catastro.Conexion_SDE(m_application)
        cls_catastro.Actualizar_DM(v_zona_dm)

        Dim lo_Filtro As String = cls_catastro.f_Intercepta_FC("Catastro", lo_xMin, lo_yMin, lo_xMax, lo_yMax, pApp, loStrShapefile)
        If v_existe = False Then
            cls_catastro.Borra_Todo_Feature("", pApp)
            cls_catastro.Limpiar_Texto_Pantalla(pApp)
            MsgBox("No existe Derechos Mineros en el Area de Interes indicado..", MsgBoxStyle.Information, "Observación...")
            Exit Sub
        ElseIf v_existe = True Then
            cls_catastro.Quitar_Layer("Catastro", pApp)
            cls_Prueba.AddFieldDM(loStrShapefile) : cls_catastro.Quitar_Layer(loStrShapefile, pApp)
            cls_catastro.Add_ShapeFile(loStrShapefile, pApp)
            cls_catastro.UpdateValue(lo_Filtro, pApp)
            lodtbLeyenda = cls_Prueba.f_Genera_Leyenda_DM("Catastro", lo_Filtro, pApp)
            cls_Prueba.Poligono_Color_1("Catastro", lodtbLeyenda, glo_pathStyle & "\Catastro.style", "LEYENDA", "", pApp)
            'cls_Catastro.Genera_Tematico_Catastro(lo_Filtro, m_application)
            'cls_Catastro.Poligono_Color_GDB(gstrFC_Catastro_Minero & Me.cboZona.Text, glo_Stile & "\CATASTRO.style", "LEYENDA", "", "Cadena", "Default", m_application, lo_Filtro)

            'cls_catastro.ShowLabel_DM("Catastro", pApp)
            cls_catastro.rotulatexto_dm("Catastro", pApp)
            cls_catastro.Genera_Malla_UTM(lo_xMin, lo_yMin, lo_xMax, lo_yMax, v_zona_dm, pApp)
            cls_catastro.Rotular_texto_DM("Mallap_" & v_zona_dm, v_zona_dm, pApp)
            cls_catastro.Quitar_Layer("Mallap_" & v_zona_dm, pApp)
            'cls_catastro.Style_Linea_GDB("Malla_" & v_zona_dm, glo_pathStyle & "\malla.style", "CLASE", pApp, "GDB")

            Dim lo_Filtro_tras As String = cls_catastro.f_Intercepta_FC("Zona Traslape", lo_xMin, lo_yMin, lo_xMax, lo_yMax, pApp)
            If lo_Filtro_tras = "" Then
                cls_catastro.Quitar_Layer("Zona Traslape", pApp)
            Else
                MsgBox("El Area de Interes indicado se encuentra en zona de traslape...", MsgBoxStyle.Information, "Observación...")
                cls_catastro.DefinitionExpression(lo_Filtro_tras, pApp, "Zona Traslape")
                arch_cata = "traslape"
                cls_catastro.Color_Poligono_Simple(m_application, "Zona Traslape")
            End If
            glo_xMin = lo_xMin : glo_yMin = lo_yMin : glo_xMax = lo_xMax : glo_yMax = lo_yMax

        End If
    End Sub

    Public Sub consulta_seguncartadm(ByVal pApp As IApplication)
        Dim cls_catastro As New cls_DM_1
        Dim cls_prueba As New cls_Prueba
        Dim lodtbLeyenda As New DataTable
        Dim cls_Demis As New cls_Demis
        Dim v_Boo_Dpto As Boolean = False
        Dim v_Boo_Prov As Boolean = False
        Dim v_Boo_Dist As Boolean = False
        Dim Form1 As New Frm_Eval_segun_codigo
        For i As Integer = 0 To Form1.clbLayer.Items.Count - 1
            Select Case Form1.clbLayer.GetItemText(i)
                Case 2
                    If Form1.clbLayer.GetItemChecked(i) = True Then
                        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "1", False)
                        v_Boo_Dpto = True
                    End If
                Case 3
                    If Form1.clbLayer.GetItemChecked(i) = True Then
                        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Provincia, m_application, "1", False)
                        v_Boo_Prov = True
                    End If
                Case 4
                    If Form1.clbLayer.GetItemChecked(i) = True Then
                        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito, m_application, "1", False)
                        v_Boo_Dist = True
                    End If
                Case 5
                    If Form1.clbLayer.GetItemChecked(i) = True Then
                        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Frontera, m_application, "1", False)
                    End If
                Case 6
                    If Form1.clbLayer.GetItemChecked(i) = True Then
                        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Cuadricula, m_application, "1", False)
                    End If
                Case 7
                    If Form1.clbLayer.GetItemChecked(i) = True Then
                        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Rios, m_application, "1", False)
                    End If
                Case 8
                    If Form1.clbLayer.GetItemChecked(i) = True Then
                        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Carretera, m_application, "1", False)
                    End If
                Case 9
                    If Form1.clbLayer.GetItemChecked(i) = True Then
                        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_CPoblado, m_application, "1", False)
                    End If
            End Select
        Next i
        cls_catastro.Delete_Rows_FC_GDB("Malla_" & v_zona_dm)
        cls_catastro.Delete_Rows_FC_GDB("Mallap_" & v_zona_dm)
        cls_catastro.Load_FC_GDB("Malla_" & v_zona_dm, "", pApp, True)
        cls_catastro.Load_FC_GDB("Mallap_" & v_zona_dm, "", pApp, True)
        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_AReservada & v_zona_dm, m_application, "1", False)
        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_ZUrbana & v_zona_dm, m_application, "1", False)
        Dim loStrShapefile As String = "Catastro" & DateTime.Now.Ticks.ToString()
        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & v_zona_dm, m_application, "1", False)
        ' cls_catastro.Conexion_SDE(m_application)
        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_LHojas, m_application, "1", False)
        cls_catastro.Actualizar_DM(v_zona_dm)
        Dim lo_Filtro As String = "hoja = '" & v_codigo.ToLower & "'"
        cls_prueba.IntersectSelect_por_Limite(pApp, lo_Filtro, "ESTADO <> 'Y'", "Cuadrangulo", Form1.xMin, Form1.yMin, Form1.xMax, Form1.yMax, Form1.txtExiste)
        Select Case Form1.txtExiste.Text
            Case -1
                cls_catastro.Borra_Todo_Feature("", m_application)
                cls_catastro.Limpiar_Texto_Pantalla(m_application)
                MsgBox("..No existe Derechos Mineros en esta Hoja..", MsgBoxStyle.Information, "BDGEOTCATMIN")
            Case Else
                'cls_Catastro.Expor_Tema(loStrShapefile, "Denun=Yes", m_application)
                cls_catastro.Expor_Tema(loStrShapefile, sele_denu, pApp)
                Dim lo_Filtro_Zona_Urbana As String = cls_catastro.f_Intercepta_FC("Zona Urbana", v_dato1 * 1000, v_dato2 * 1000, v_dato3 * 1000, v_dato4 * 1000, pApp)
                If lo_Filtro_Zona_Urbana = "" Then
                    cls_catastro.Quitar_Layer("Zona Urbana", pApp)
                Else
                    cls_catastro.DefinitionExpression(lo_Filtro_Zona_Urbana, pApp, "Zona Urbana")
                    Try
                        'cls_catastro.Poligono_Color_GDB(gstrFC_ZUrbana & v_zona_dm, glo_pathStyle & "\ZONA_URBANA.style", "NOMBRE", "", "Cadena", "", m_application, lo_Filtro_Zona_Urbana)
                    Catch ex As Exception
                    End Try
                End If

                Dim lo_Filtro_Area_Reserva As String = cls_catastro.f_Intercepta_FC("Zona Reservada", v_dato1 * 1000, v_dato2 * 1000, v_dato3 * 1000, v_dato4 * 1000, pApp)
                If lo_Filtro_Area_Reserva = "" Then
                    cls_catastro.Quitar_Layer("Zona Reservada", pApp)
                Else
                    cls_catastro.DefinitionExpression(lo_Filtro_Area_Reserva, pApp, "Zona Reservada")
                    'cls_catastro.Poligono_Color_GDB(gstrFC_AReservada & v_zona_dm, glo_pathStyle & "\AREA_RESERVA.style", "NM_RESE", "", "Cadena", "", pApp, lo_Filtro_Area_Reserva)
                End If
                cls_catastro.ClearLayerSelection(pFeatureLayer)
                'cls_Catastro.AddImagen(glo_Path & "\ecw_cartas\" & Me.txtCodigo.Text.Replace("-", "") & ".ECW", 1, m_application, False)
                'cls_Catastro.AddImagen("F:\ms4w en 10.102.0.239\apps\pmapper\pmapper_demodata\ign\GEO56\" & Me.txtCodigo.Text.Replace("-", "") & ".ECW", "", m_application, True)
                'cls_Catastro.AddImagen("U:\DATOS\ECW_CARTAS\" & Me.txtCodigo.Text.Replace("-", "") & ".ECW", "", m_application, True)

                cls_catastro.AddImagen(glo_pathImaSat & "\" & v_codigo.Replace("-", "") & ".jp2", 2, v_codigo, pApp, False)
                cls_catastro.Quitar_Layer("Catastro", pApp)
                cls_catastro.Quitar_Layer("Cuadrangulo", pApp)
                cls_prueba.AddFieldDM(loStrShapefile) : cls_catastro.Quitar_Layer(loStrShapefile, pApp)
                cls_catastro.Add_ShapeFile(loStrShapefile, pApp)
                MsgBox(lo_Filtro)
                cls_catastro.UpdateValue(lo_Filtro, pApp, v_codigo)
                MsgBox(lo_Filtro)
                lodtbLeyenda = cls_prueba.f_Genera_Leyenda_DM("Catastro", lo_Filtro, pApp)
                cls_prueba.Poligono_Color_1("Catastro", lodtbLeyenda, glo_pathStyle & "\Catastro.style", "LEYENDA", "Default", pApp)
                lodtbLeyenda = Nothing
                cls_catastro.ShowLabel_DM("Catastro", pApp)

                'cls_catastro.Genera_Malla_UTM(CType(txtXMin.Text, Double) * 1000, CType(txtYMin.Text, Double) * 1000, CType(txtXMax.Text, Double) * 1000, CType(txtyMax.Text, Double) * 1000, Me.txtZona.Text, m_application)
                cls_catastro.Genera_Malla_UTM(v_dato1 * 1000, v_dato2 * 1000, v_dato3 * 1000, v_dato4 * 1000, v_zona_dm, pApp)
                cls_catastro.Rotular_texto_DM("Mallap_" & v_zona_dm, v_zona_dm, pApp)
                cls_catastro.Quitar_Layer("Mallap_" & v_zona_dm, pApp)
                'cls_catastro.Style_Linea_GDB("Malla_" & v_zona_dm, glo_pathStyle & "\malla.style", "CLASE", pApp, "GDB")
                If v_Boo_Dpto = True Then
                    Dim lo_Filtro_Dpto As String = cls_catastro.f_Intercepta_FC("Departamento", v_dato1 * 1000, v_dato2 * 1000, v_dato3 * 1000, v_dato4 * 1000, pApp)
                    cls_catastro.DefinitionExpression(lo_Filtro_Dpto, pApp, "Departamento")
                    cls_catastro.Poligono_Color_GDB(gstrFC_Departamento, glo_pathStyle & "\DEPARTAMENTO.style", "NM_DEPA", "", "Cadena", "", pApp, lo_Filtro_Dpto)
                End If
                If v_Boo_Prov = True Then
                    Dim lo_Filtro_Prov As String = cls_catastro.f_Intercepta_FC("Provincia", v_dato1 * 1000, v_dato2 * 1000, v_dato3 * 1000, v_dato4 * 1000, pApp)
                    cls_catastro.DefinitionExpression(lo_Filtro_Prov, pApp, "Provincia")
                    cls_catastro.Poligono_Color_GDB(gstrFC_Provincia, glo_pathStyle & "\PROVINCIA.style", "NM_PROV", "", "Cadena", "", pApp, lo_Filtro_Prov)
                End If
                If v_Boo_Dist = True Then
                    Dim lo_Filtro_Dist As String = cls_catastro.f_Intercepta_FC("Distrito", v_dato1 * 1000, v_dato2 * 1000, v_dato3 * 1000, v_dato4 * 1000, pApp)
                    cls_catastro.DefinitionExpression(lo_Filtro_Dist, pApp, "Distrito")
                    cls_catastro.Shade_Poligono("Distrito", pApp)
                End If

                '***************************
                'lo_Filtro = ("HOJA LIKE '" & Me.txtCodigo.Text & "%'").ToLower  '--- "HOJA LIKE '33-s%'"
                'cls_Catastro.DefinitionExpression(lo_Filtro, m_application, "Geologia")
                'lodtbLeyenda = cls_Prueba.f_Genera_Leyenda_Geo("Geologia", lo_Filtro, m_application)
                'If lodtbLeyenda.Rows.Count <> 0 Then
                '    cls_Prueba.Poligono_Color_2("Geologia", lodtbLeyenda, glo_pathStyle & "\Geologia_100.style", "CODI", "Geología", m_application)
                '    lodtbLeyenda = Nothing
                'Else
                '    cls_Catastro.Quitar_Layer("Geologia", m_application)
                'End If
                'cls_Demis.PC_AddWMSLayer("http://www2.demis.nl/wms/wms.asp?wms=WorldMap&Demis World Map", m_application)
                pMxDoc.ActivatedView.Refresh()
                'DialogResult = Windows.Forms.DialogResult.Cancel
        End Select
    End Sub

    Public Sub Consulta_Area_Restringida(ByVal pApp As IApplication, ByVal p_ListBox As Object, ByVal p_txtExiste As Object)
        cadena_query_ar = ""
        Dim cls_catastro As New cls_DM_1
        Dim cls_Eval As New Cls_evaluacion
        Dim cls_prueba As New cls_Prueba
        Dim lodtbLeyenda As New DataTable
        Dim lostrZona As String = ""
        Dim lo_Inicio As Integer = 0
        Dim cod_rese As String
        Dim cod_rese1 As String
        Dim zona_sele As String = ""
        Dim zona_sele1 As String
        Dim clase_sele As String
        Dim categori_sele As String
        Dim clase_sele1 As String
        Dim cod_opcion As String = ""
        Dim nom_rese As String = ""
        Dim cadena_query_ubica As String = ""
        Dim cadena_query As String = ""
        Dim num As Long = 0
        Dim pForm As New Frm_Eval_segun_codigo
        Dim lostrFiltro As String = ""
        Dim loStrShapefile As String = "Catastro" & DateTime.Now.Ticks.ToString()
        cod_rese1 = ""
        zona_sele1 = ""
        clase_sele1 = ""
        '*****************************
        For i As Integer = 0 To p_ListBox.RowCount - 1
            If p_ListBox.Item(i, "FLG_SEL") Then
                num = num + 1
                If num > 2 Then
                    p_txtExiste.Text = -5
                    Exit Sub
                Else
                    cod_rese = p_ListBox.item(i, "CG_CODIGO").ToString 'p_ListBox.Items(i).SubItems(0).Text
                    lostrZona = p_ListBox.item(i, "ZA_ZONA").ToString 'p_ListBox.Items(i).SubItems(2).Text
                    clase_sele = p_ListBox.item(i, "PA_DESCRI").ToString ' p_ListBox.Items(i).SubItems(3).Text
                    nom_rese = p_ListBox.item(i, "PE_NOMARE").ToString 'p_ListBox.Items(i).SubItems(1).Text
                    categori_sele = p_ListBox.item(i, "CA_DESCAT").ToString 'p_ListBox.Items(i).SubItems(1).Text

                    If categori_sele = "PARQUE NACIONAL" Or categori_sele = "SANTUARIO NACIONAL" Or categori_sele = "SANTUARIO HISTORICO" Or categori_sele = "RESERVA PAISAJISTICA" Or categori_sele = "REFUGIO DE VIDA SILVESTRE" Or categori_sele = "RESERVA NACIONAL" Or categori_sele = "RESERVA COMUNAL" Or categori_sele = "COTO DE CAZA" Or categori_sele = "BOSQUE DE PROTECCION" Or categori_sele = "ZONA RESERVADA" Then
                        clase_sele = clase_sele
                    Else
                        clase_sele = " "
                    End If

                    v_zona_dm = lostrZona
                    If cod_rese1 = cod_rese Then
                        cod_rese1 = cod_rese
                        If zona_sele1 = zona_sele Then
                            zona_sele1 = zona_sele
                        Else
                            If zona_sele1 <> "" Then
                                p_txtExiste.Text = -4
                                Exit Sub
                            Else
                                zona_sele1 = zona_sele
                            End If
                        End If
                        'haciendo la cadena de consulta
                        cod_opcion = Microsoft.VisualBasic.Left(cod_rese, 2)
                        If cod_opcion <> "ZU" Then
                            If num = 1 Then
                                cadena_query_ubica = "CODIGO =  '" & cod_rese & "' AND CLASE =  '" & clase_sele & "'"
                                cadena_query = cadena_query_ubica
                            Else
                                cadena_query = cadena_query & " OR " & "CODIGO =  '" & cod_rese & "' AND CLASE =  '" & clase_sele & "'"
                            End If
                        ElseIf cod_opcion = "ZU" Then
                            If num = 1 Then
                                cadena_query_ubica = "CODIGO =  '" & cod_rese & "'"
                                cadena_query = cadena_query_ubica
                            Else
                                cadena_query = cadena_query & " OR " & "CODIGO =  '" & cod_rese & "'"
                            End If
                        End If
                    Else
                        'validando codigo area reservada
                        If cod_rese1 <> "" Then
                            p_txtExiste.Text = -3
                            Exit Sub
                        Else
                            cod_rese1 = cod_rese
                            If zona_sele1 = zona_sele Then
                                zona_sele1 = zona_sele
                            Else
                                If zona_sele1 <> "" Then
                                    p_txtExiste.Text = -4
                                    Exit Sub
                                Else
                                    zona_sele1 = zona_sele
                                End If
                            End If
                            cod_opcion = Microsoft.VisualBasic.Left(cod_rese, 2)
                            If cod_opcion <> "ZU" Then
                                If num = 1 Then
                                    cadena_query_ubica = "CODIGO =  '" & cod_rese & "' AND CLASE =  '" & clase_sele & "'"
                                    cadena_query = cadena_query_ubica
                                Else
                                    cadena_query = cadena_query & " OR " & "CODIGO =  '" & cod_rese & "' AND CLASE =  '" & clase_sele & "'"
                                End If
                            ElseIf cod_opcion = "ZU" Then
                                If num = 1 Then
                                    cadena_query_ubica = "CODIGO =  '" & cod_rese & "'"
                                    cadena_query = cadena_query_ubica
                                Else
                                    cadena_query = cadena_query & " OR " & "CODIGO =  '" & cod_rese & "'"
                                End If
                            End If
                        End If
                    End If

                End If
            End If
        Next i
        '*********************
        lostrFiltro = cadena_query
        cadena_query_ar = cadena_query
        cls_catastro.Borra_Todo_Feature("", pApp)
        cls_catastro.Limpiar_Texto_Pantalla(pApp)
        If lostrZona <> "" Then
            cls_catastro.Delete_Rows_FC_GDB("Malla_" & lostrZona)
            cls_catastro.Delete_Rows_FC_GDB("Mallap_" & lostrZona)
            cls_catastro.Load_FC_GDB("Malla_" & lostrZona, "", pApp, True)
            cls_catastro.Load_FC_GDB("Mallap_" & lostrZona, "", pApp, True)
            cls_catastro.Actualizar_DM(lostrZona)
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & lostrZona, m_application, "1", False)
            'cls_catastro.Conexion_SDE(m_application)
            Select Case cod_opcion
                Case "ZU"
                    cod_opcion_Rese = "ZU"
                    cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_ZUrbana & lostrZona, pApp, "1", False)
                    cls_prueba.IntersectSelect_por_Limite(pApp, lostrFiltro, "", "Zona Urbana", pForm.xMin, pForm.yMin, pForm.xMax, pForm.yMax, pForm.txtExiste)
                Case Else 'Varios "AN"
                    cod_opcion_Rese = "AN"
                    cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_AReservada & lostrZona, m_application, "1", False)
                    cls_prueba.IntersectSelect_por_Limite(pApp, lostrFiltro, "", "Zona Reservada", pForm.xMin, pForm.yMin, pForm.xMax, pForm.yMax, pForm.txtExiste)
            End Select
            p_txtExiste.Text = pForm.txtExiste.Text
            Select Case pForm.txtExiste.Text
                Case -1
                    cls_catastro.Quitar_Layer("Catastro", pApp)
                    'Select Case cod_opcion
                    '    Case "ZU"

                    ' cls_catastro.DefinitionExpression(lostrFiltro, pApp, "Zona Urbana")
                    '        cls_catastro.ClearLayerSelection(pFeatureLayer)
                    '        cls_Eval.obtienelimitesmaximos("Zona Urbana")
                    '    Case Else

                    '        cls_catastro.DefinitionExpression(lostrFiltro, pApp, "Zona Reservada")
                    '        cls_catastro.ClearLayerSelection(pFeatureLayer)
                    '        cls_Eval.obtienelimitesmaximos("Zona Reservada")
                    'End Select
                    cls_catastro.Genera_Malla_UTM(v_este_min, v_norte_min, v_este_max, v_norte_max, lostrZona, pApp)
                    cls_catastro.Rotular_texto_DM("Mallap_" & lostrZona, lostrZona, pApp)
                    cls_catastro.Quitar_Layer("Mallap_" & lostrZona, pApp)
                    'cls_catastro.Style_Linea_GDB("Malla_" & lostrZona, glo_pathStyle & "\malla.style", "CLASE", pApp, "GDB")
                    pMxDoc.ActivatedView.Refresh()
                    cls_Eval.cierra_ejecutable()
                    'MsgBox("..No existe Derechos Mineros en esta Area..", MsgBoxStyle.Information, "BDGEOTCATMIN")
                Case -2
                    cls_catastro.Quitar_Layer("Catastro", pApp)
                    cls_catastro.Quitar_Layer("Zona Urbana", pApp)
                    cls_catastro.Quitar_Layer("Mallap_" & lostrZona, pApp)
                    cls_catastro.Quitar_Layer("Malla", pApp)
                Case Else
                    cls_catastro.Expor_Tema(loStrShapefile, sele_denu, pApp)
                    Select Case cod_opcion
                        Case "ZU"
                            'cls_catastro.DefinitionExpression(lostrFiltro, pApp, "Zona Urbana")
                        Case Else '"AN"
                            'cls_catastro.DefinitionExpression(lostrFiltro, pApp, "Zona Reservada")
                    End Select
                    cls_catastro.ClearLayerSelection(pFeatureLayer)
                    cls_catastro.Quitar_Layer("Catastro", pApp)
                    cls_prueba.AddFieldDM(loStrShapefile) : cls_catastro.Quitar_Layer(loStrShapefile, pApp)
                    cls_catastro.Add_ShapeFile(loStrShapefile, pApp)
                    cls_catastro.UpdateValue(lostrFiltro, pApp, "")
                    lodtbLeyenda = cls_prueba.f_Genera_Leyenda_DM("Catastro", lostrFiltro, pApp)
                    cls_prueba.Poligono_Color_1("Catastro", lodtbLeyenda, glo_pathStyle & "\Catastro.style", "LEYENDA", "Default", pApp)
                    lodtbLeyenda = Nothing
                    'cls_catastro.ShowLabel_DM("Catastro", pApp)
                    cls_catastro.rotulatexto_dm("Catastro", pApp)
                    cls_catastro.Genera_Malla_UTM(CType(pForm.xMin.Text, Double), CType(pForm.yMin.Text, Double), CType(pForm.xMax.Text, Double), CType(pForm.yMax.Text, Double), lostrZona, pApp)
                    glo_xMin = CType(pForm.xMin.Text, Double)
                    glo_xMax = CType(pForm.xMax.Text, Double)
                    glo_yMin = CType(pForm.yMin.Text, Double)
                    glo_yMax = CType(pForm.yMax.Text, Double)
                    cls_catastro.Rotular_texto_DM("Mallap_" & lostrZona, lostrZona, pApp)
                    cls_catastro.Quitar_Layer("Mallap_" & lostrZona, pApp)
                    cls_catastro.Quitar_Layer("Zona Urbana", pApp)
                    cls_catastro.Quitar_Layer("Zona Reservada", pApp)
                    pMxDoc.ActivatedView.Refresh()
            End Select
        End If
    End Sub

    Public Sub Consulta_segun_codigoDM(ByVal pApp As IApplication, ByVal p_clbLayer As Object)
        Dim cls_eval As New Cls_evaluacion
        Dim cls_prueba As New cls_Prueba
        Dim cls_catastro As New cls_DM_1
        Dim lodtbLeyenda As New DataTable
        Dim lostrMensaje As String = 6
        Dim loStrShapefile As String = "Catastro" & DateTime.Now.Ticks.ToString()
        Dim lo_xMin = (v_dato1 - v_radio * 1000) : Dim lo_xMax = (v_dato3 + v_radio * 1000)
        Dim lo_yMin = (v_dato2 - v_radio * 1000) : Dim lo_yMax = (v_dato4 + v_radio * 1000)
        gloEsteMin = v_dato1 : gloEsteMax = v_dato3
        gloNorteMin = v_dato2 : gloNorteMax = v_dato4 : gloZona = v_zona_dm
        'Esta parte se comento para utilizar proceso intermedio - actualizacion catastro

        'Dim v_existe_dm As Boolean = False
        'v_existe_dm = cls_eval.Leer_Dbf(v_codigo)
        'If v_existe_dm = True Then
        'cls_catastro.Add_ShapeFile_bdgis("cata" & v_zona_dm, pApp)
        'Else
        'cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & v_zona_dm, pApp, "1", True)
        'End If
        If v_vigcat <> "G" Then
            cls_eval.cierra_ejecutable()
            MsgBox("Las Coordenadas de este Derecho Minero no" & vbNewLine & "están Habilitadas para el Sistema de Graficación", MsgBoxStyle.OkOnly, "BDGEOCATMIN")
            lostrMensaje = MsgBox("¿ Desea graficar de todos modos esta área de interés ?", MsgBoxStyle.YesNo, "BDGEOCATMIN")
        End If
        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & v_zona_dm, pApp, "1", True)
        'cls_catastro.Conexion_SDE(m_application)
        loglo_MensajeDM = lostrMensaje
        Select Case lostrMensaje
            Case 6
                cls_catastro.Delete_Rows_FC_GDB("Malla_" & v_zona_dm)
                cls_catastro.Delete_Rows_FC_GDB("Mallap_" & v_zona_dm)
                cls_catastro.Load_FC_GDB("Malla_" & v_zona_dm, "", pApp, True)
                cls_catastro.Load_FC_GDB("Mallap_" & v_zona_dm, "", pApp, True)
                Dim lo_Filtro_DM = cls_catastro.f_Intercepta_FC("Catastro", lo_xMin, lo_yMin, lo_xMax, lo_yMax, pApp, loStrShapefile)
                glo_xMin = lo_xMin : glo_yMin = lo_yMin : glo_xMax = lo_xMax : glo_yMax = lo_yMax
                cls_catastro.Quitar_Layer("Catastro", pApp)
                cls_prueba.AddFieldDM(loStrShapefile) : cls_catastro.Quitar_Layer(loStrShapefile, pApp)
                cls_catastro.Add_ShapeFile(loStrShapefile, pApp)
                cls_catastro.UpdateValue(lo_Filtro_DM, pApp)
                cls_catastro.Update_Value_DM(pApp, v_codigo)
                lodtbLeyenda = cls_prueba.f_Genera_Leyenda_DM("Catastro", lo_Filtro_DM, pApp)
                cls_prueba.Poligono_Color_1("Catastro", lodtbLeyenda, glo_pathStyle & "\Catastro.style", "LEYENDA", "", pApp)
                'cls_catastro.ShowLabel_DM("Catastro", pApp)
                cls_catastro.rotulatexto_dm("Catastro", pApp)
                cls_catastro.Genera_Malla_UTM(lo_xMin, lo_yMin, lo_xMax, lo_yMax, v_zona_dm, pApp)
                cls_catastro.Rotular_texto_DM("Mallap_" & v_zona_dm, v_zona_dm, pApp)
                cls_catastro.Quitar_Layer("Mallap_" & v_zona_dm, pApp)
            Case 7
                cls_catastro.Borra_Todo_Feature("", pApp)
        End Select
    End Sub

    Public Sub FT_Obtiene_Envelope_Layer(ByVal p_Layer As String, ByVal p_Codigo As String, ByVal p_Campo As String, ByRef xMin As Object, ByRef yMin As Object, ByRef xMax As Object, ByRef yMax As Object, ByVal pApp As IApplication)
        pMxDoc = pApp.Document
        pMap = pMxDoc.FocusMap
        Dim pFeatureLayer As IFeatureLayer = Nothing
        For i As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(i).Name = p_Layer Then
                pFeatureLayer = pMap.Layer(i)
                Exit For
            End If
        Next i
        Dim pActiveView As IActiveView
        Dim pContentsView As IContentsView
        pActiveView = pMap
        pContentsView = pMxDoc.CurrentContentsView
        pActiveView.Extent = pFeatureLayer.AreaOfInterest
        pMxDoc.ActiveView.Refresh() ' refresh the map
        xMin = pFeatureLayer.AreaOfInterest.Envelope.XMin
        yMin = pFeatureLayer.AreaOfInterest.Envelope.YMin
        xMax = pFeatureLayer.AreaOfInterest.Envelope.XMax
        yMax = pFeatureLayer.AreaOfInterest.Envelope.YMax
    End Sub
    Public Sub Consulta_Segun_Carta_DM(ByVal pApp As IApplication, ByVal clbLayer As Windows.Forms.CheckedListBox, ByVal p_Tipo As String)
        Dim pActive As IActiveView

        Dim cls_catastro As New cls_DM_1
        Dim cls_prueba As New cls_Prueba
        Dim cls_eval As New Cls_evaluacion
        Dim lodtbLeyenda As New DataTable
        Dim cls_Demis As New cls_Demis
        Dim FORM1 As New Frm_Eval_segun_codigo
        Dim loStrShapefile As String = "Catastro" & DateTime.Now.Ticks.ToString()
        If ((v_dato1 < v_dato1_mod) Or (v_dato2 < v_dato2_mod) Or (v_dato3 > v_dato3_mod) Or (v_dato4 > v_dato4_mod)) Then
            valida_selecartas = True
        Else
            valida_selecartas = False
        End If
        v_existe = False
        cls_catastro.Delete_Rows_FC_GDB("Malla_" & v_zona_dm)
        cls_catastro.Delete_Rows_FC_GDB("Mallap_" & v_zona_dm)
        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & v_zona_dm, m_application, "1", False)
        'cls_catastro.Conexion_SDE(m_application)
        cls_catastro.Actualizar_DM(v_zona_dm)
        Dim lo_Filtro As String = "hoja = '" & v_codigo.ToLower & "'"
        cls_catastro.f_Intercepta_FC("Catastro", v_dato1 * 1000, v_dato2 * 1000, v_dato3 * 1000, v_dato4 * 1000, pApp, loStrShapefile)

        'cls_catastro.PT_CargarFeatureClass_SDE("GPO_MEM_USO_MINERO", m_application, "1", False)
        'Dim lo_Filtro_Area_Uso_Minero As String = cls_catastro.f_Intercepta_FC("Uso Minero", v_dato1 * 1000, v_dato2 * 1000, v_dato3 * 1000, v_dato4 * 1000, pApp, loStrShapefile)
        ''Dim lo_Filtro_Area_Reserva As String = cls_catastro.f_Intercepta_FC("Zona Reservada", glo_xMin, glo_yMin, glo_xMax, glo_yMax, m_application)
        'If lo_Filtro_Area_Uso_Minero = "" Then
        '    cls_catastro.Quitar_Layer("Uso Minero", m_application)
        'Else

        '    cls_catastro.DefinitionExpression(lo_Filtro_Area_Uso_Minero, m_application, "Uso Minero")
        'End If

        'Exit Sub

        'cls_catastro.PT_CargarFeatureClass_SDE("GPO_MEM_AREA_ACTIVIDAD_MINERA", m_application, "1", False)
        'cls_catastro.f_Intercepta_FC("Actividad Minera", v_dato1 * 1000, v_dato2 * 1000, v_dato3 * 1000, v_dato4 * 1000, pApp, loStrShapefile)


        If v_existe = False Then
            cls_eval.cierra_ejecutable()
            cls_catastro.Borra_Todo_Feature("", pApp)
            cls_catastro.Limpiar_Texto_Pantalla(pApp)
            MsgBox("No existe Derechos Mineros en esta Hoja..", MsgBoxStyle.Information, "Observación...")
            Exit Sub
        ElseIf v_existe = True Then
            cls_catastro.Quitar_Layer("Catastro", pApp)
            cls_catastro.Load_FC_GDB("Malla_" & v_zona_dm, "", pApp, True)
            cls_catastro.Load_FC_GDB("Mallap_" & v_zona_dm, "", pApp, True)
        End If
        Select Case p_Tipo
            Case "OP_1"
                'cls_catastro.AddImagen(glo_pathIGN & "\" & v_codigo.Replace("-", "") & ".ECW", 1, v_codigo, pApp, False)
                '************************************
                Dim filtro_carta As String = "NAME = '" & LCase(v_codigo.Replace("-", "")) & ".ecw'"
                Conexion_SDE(pApp)
                'pActive = pMap
                pRasterCatalog = GetRasterCatalog(pApp, "DATA_GIS.DS_IGN_CARTA_NACIONAL_GEOPSA56")
                AddRasterCatalogLayer(pApp, pRasterCatalog)
                MyDefinitionQuery("DATA_GIS.DS_IGN_CARTA_NACIONAL_GEOPSA56", pApp, v_codigo, filtro_carta, "1")
                pFSelQuery.Clear()
                cls_catastro.Quitar_Layer("DATA_GIS.DS_IGN_CARTA_NACIONAL_GEOPSA56", pApp)


            Case "OP_14"
                'cls_catastro.AddImagen(glo_pathGEO & "\" & v_codigo.Replace("-", "") & ".ECW", 3, v_codigo, pApp, False)
                Dim filtro_carta As String = "NAME = '" & LCase(v_codigo.Replace("-", "")) & ".ecw'"
                Conexion_SDE(pApp)
                'pActive = pMap
                pRasterCatalog = GetRasterCatalog(pApp, "DATA_GIS.DS_IMAGEN_CARTA_GEOLOGICA")
                AddRasterCatalogLayer(pApp, pRasterCatalog)
                MyDefinitionQuery("DATA_GIS.DS_IMAGEN_CARTA_GEOLOGICA", pApp, v_codigo, filtro_carta, "1")
                pFSelQuery.Clear()
                cls_catastro.Quitar_Layer("DATA_GIS.DS_IMAGEN_CARTA_GEOLOGICA", pApp)

        End Select

        'cls_Catastro.AddImagen("F:\ms4w en 10.102.0.239\apps\pmapper\pmapper_demodata\ign\GEO56\" & Me.txtCodigo.Text.Replace("-", "") & ".ECW", "", m_application, True)
        'cls_Catastro.AddImagen("U:\DATOS\ECW_CARTAS\" & Me.txtCodigo.Text.Replace("-", "") & ".ECW", "", m_application, True)
        'cls_catastro.AddImagen(glo_pathImaSat & "\" & v_codigo.Replace("-", "") & ".jp2", 2, pApp, False)
        'cls_catastro.AddImagen(glo_pathGEO & "\" & v_codigo.Replace("-", "") & ".ECW", "3", pApp, True)


        cls_prueba.AddFieldDM(loStrShapefile) : cls_catastro.Quitar_Layer(loStrShapefile, pApp)
        cls_catastro.Add_ShapeFile(loStrShapefile, pApp)
        cls_catastro.UpdateValue(lo_Filtro, pApp, v_codigo)
        lodtbLeyenda = cls_prueba.f_Genera_Leyenda_DM("Catastro", lo_Filtro, pApp)
        cls_prueba.Poligono_Color_1("Catastro", lodtbLeyenda, glo_pathStyle & "\Catastro.style", "LEYENDA", "Default", pApp)
        lodtbLeyenda = Nothing
        'cls_catastro.ShowLabel_DM("Catastro", pApp)
        cls_catastro.rotulatexto_dm("Catastro", pApp)
        'cls_catastro.Genera_Malla_UTM(CType(txtXMin.Text, Double) * 1000, CType(txtYMin.Text, Double) * 1000, CType(txtXMax.Text, Double) * 1000, CType(txtyMax.Text, Double) * 1000, Me.txtZona.Text, m_application)
        cls_catastro.Genera_Malla_UTM(v_dato1 * 1000, v_dato2 * 1000, v_dato3 * 1000, v_dato4 * 1000, v_zona_dm, pApp)
        cls_catastro.Rotular_texto_DM("Mallap_" & v_zona_dm, v_zona_dm, pApp)
        cls_catastro.Quitar_Layer("Mallap_" & v_zona_dm, pApp)

        'cls_catastro.Style_Linea_GDB("Malla_" & v_zona_dm, glo_pathStyle & "\malla.style", "CLASE", pApp, "GDB")
        ' cls_Demis.PC_AddWMSLayer("http://www2.demis.nl/wms/wms.asp?wms=WorldMap&Demis World Map", m_application)
        glo_xMin = v_dato1 * 1000 : glo_yMin = v_dato2 * 1000 : glo_xMax = v_dato3 * 1000 : glo_yMax = v_dato4 * 1000
        pMxDoc.ActivatedView.Refresh()

        'DialogResult = Windows.Forms.DialogResult.Cancel
        'End If
    End Sub

    Public Sub Consulta_Evaluacion_DM(ByVal pApp As IApplication, ByVal p_clbLayer As Object)
        Try
            fecha_archi = DateTime.Now.Ticks.ToString
            Dim cls_eval As New Cls_evaluacion
            Dim cls_prueba As New cls_Prueba
            Dim cls_catastro As New cls_DM_1
            'Dim formulario As New Frm_Eval_segun_codigo
            Dim lostrFiltro As String = ""
            Dim capa_sele As ISelectionSet
            Dim pFeatureCursor As IFeatureCursor
            Dim pfeature As IFeature
            valida = False
            distancia_fron = 0
            Dim lodtbLeyenda As New DataTable
            Dim form1 As New Frm_Eval_segun_codigo
            Dim lostrMensaje As String = 6
            'Dim loglo_MensajeDM As String = 6
            Dim loStrShapefile As String = "Catastro" & fecha_archi
            Dim lo_xMin As Double = (v_dato1 - v_radio * 1000)
            Dim lo_xMax As Double = (v_dato3 + v_radio * 1000)
            Dim lo_yMin As Double = (v_dato2 - v_radio * 1000)
            Dim lo_yMax As Double = (v_dato4 + v_radio * 1000)
            gloEsteMin = v_dato1 : gloEsteMax = v_dato3
            gloNorteMin = v_dato2 : gloNorteMax = v_dato4 : gloZona = v_zona_dm
            loglo_MensajeDM = lostrMensaje
            If V_caso_simu = "SI" Then v_vigcat = "G"
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & v_zona_dm, pApp, "2", True)

            'cls_catastro.Conexion_SDE(m_application)

            cls_catastro.Delete_Rows_FC_GDB("Malla_" & v_zona_dm)
            cls_catastro.Delete_Rows_FC_GDB("Mallap_" & v_zona_dm)
            cls_catastro.Load_FC_GDB("Malla_" & v_zona_dm, "", pApp, True)
            cls_catastro.Load_FC_GDB("Mallap_" & v_zona_dm, "", pApp, True)
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito, m_application, "2", False)
            'cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_AReservada56, m_application, "1", False)
            ' cls_catastro.Conexion_SDE(m_application)
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_ZUrbana & v_zona_dm, m_application, "2", True)
            Dim lo_Filtro As String
            lo_Filtro = cls_catastro.f_Intercepta_FC("Catastro", lo_xMin, lo_yMin, lo_xMax, lo_yMax, pApp, loStrShapefile)
            glo_xMin = lo_xMin : glo_yMin = lo_yMin : glo_xMax = lo_xMax : glo_yMax = lo_yMax
            If V_caso_simu = "" Then
                Try
                    Kill(glo_pathTMP & "\DM_" & v_codigo & "*.*")
                Catch ex As Exception
                End Try
                cls_eval.consultacapaDM(v_codigo, "DM", "Catastro")
                If pFeatureSelection.SelectionSet.Count = 0 Then
                    'MsgBox("No Existe el DM Consultado en la Base Grafica", MsgBoxStyle.Critical, "[BDGEOCATMIN]")
                    loglo_MensajeDM = 7
                    cls_eval.cierra_ejecutable()
                    cls_catastro.Borra_Todo_Feature("", pApp)
                    cls_catastro.Limpiar_Texto_Pantalla(pApp)
                    MsgBox("No existe el Derecho Minero en la Base Grafica..", MsgBoxStyle.Information, "[BDGEOCATMIN]")
                    Exit Sub

                End If
                arch_cata = "Cata"
                cls_eval.DefinitionExpressiontema(v_codigo, pApp, "Catastro")
                cls_eval.obtienelimitesmaximos("Catastro")
            Else
                v_codigo = "000000001"
                cls_catastro.Add_ShapeFile1("Simulado" & fecha_archi2, pApp, "codigo")
            End If

            cls_eval.f_Intercepta_temas("Distrito", v_este_min, v_norte_min, v_este_max, v_norte_max, pApp, loStrShapefile)

            Dim nm_dist As String = ""
            If colecciones_dist.Count = 0 Then
                lista_dist = ""
            End If
            For contador As Integer = 1 To colecciones_dist.Count
                nm_dist = colecciones_dist.Item(contador)
                If contador = 1 Then
                    lista_dist = nm_dist
                ElseIf contador > 1 Then
                    lista_dist = lista_dist & "," & nm_dist
                End If
            Next contador
            colecciones_dist.Clear()

            Dim nm_depa As String = ""
            Dim nm_depa1 As String = ""
            If colecciones_depa.Count = 0 Then
                lista_depa = ""
            End If
            For contador As Integer = 1 To colecciones_depa.Count
                nm_depa = colecciones_depa.Item(contador)
                If contador = 1 Then
                    lista_depa = nm_depa
                    lista_nm_depa = "NM_DEPA =  '" & nm_depa & "'"
                    nm_depa1 = nm_depa

                ElseIf contador > 1 Then
                    If nm_depa <> nm_depa1 Then
                        lista_depa = lista_depa & "," & nm_depa
                        lista_nm_depa = lista_nm_depa & " OR " & "NM_DEPA =  '" & nm_depa & "'"
                        nm_depa1 = nm_depa
                    End If
                End If
            Next contador
            colecciones_depa.Clear()
            'Obtiene lista de provincias x dm

            Dim nm_prov As String = ""
            Dim nm_prov1 As String = ""
            If colecciones_prov.Count = 0 Then
                lista_prov = ""
            End If
            For contador As Integer = 1 To colecciones_prov.Count
                nm_prov = colecciones_prov.Item(contador)
                If contador = 1 Then
                    lista_prov = nm_prov
                    nm_prov1 = nm_prov
                ElseIf contador > 1 Then
                    If nm_prov <> nm_prov1 Then
                        lista_prov = lista_prov & "," & nm_prov
                        nm_prov1 = nm_prov
                    End If
                End If
            Next contador
            colecciones_prov.Clear()
            'cls_catastro.Quitar_Layer("Distrito", pApp)  '

            Dim lodbtExisteAR As New DataTable
            Try
                lodbtExisteAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(2, gstrFC_Catastro_Minero & gloZona, gstrFC_AReservada & gloZona, v_codigo)
            Catch ex As Exception

            End Try

            'Seleccionar las areas reservadas x DM evaluado - PROCESO ANTIGUO
            '---------------------------------------------------------------

            'cls_eval.f_Intercepta_temas("Zona Reservada", v_este_min, v_norte_min, v_este_max, v_norte_max, pApp, loStrShapefile)
            'cls_catastro.Expor_Tema("Zona Reservada", True, pApp)
            'Dim nm_rese As String = ""
            'If colecciones_rese.Count = 0 Then
            '    lista_rese = "El DM evaluado no se encuentra superpuesto a un área reservada"
            'End If
            'For contador As Integer = 1 To colecciones_rese.Count
            '    nm_rese = colecciones_rese.Item(contador)
            '    If contador = 1 Then
            '        lista_rese = nm_rese
            '    ElseIf contador > 1 Then
            '        lista_rese = lista_rese & "," & nm_rese
            '    End If
            'Next contador
            'colecciones_rese.Clear()

            Dim nm_rese As String = ""
            If lodbtExisteAR.Rows.Count = 0 Then
                lista_rese = "El DM evaluado no se encuentra superpuesto a un Area Reservada"
            End If

            If lodbtExisteAR.Rows.Count >= 1 Then
                For contador As Integer = 0 To lodbtExisteAR.Rows.Count - 1
                    nm_rese = lodbtExisteAR.Rows(contador).Item("NM_RESE")
                    If contador = 0 Then
                        lista_rese = nm_rese
                    ElseIf contador > 0 Then
                        lista_rese = lista_rese & "," & nm_rese
                    End If
                Next contador
            End If

            'Seleccionar las areas urbanas x DM evaluado - PROCESO ANTIGUO
            '------------------------------------------------------------
            'cls_eval.f_Intercepta_temas("Zona Urbana", v_este_min, v_norte_min, v_este_max, v_norte_max, pApp, loStrShapefile)
            'cls_catastro.Expor_Tema("Zona Urbana", True, pApp)
            'Dim nm_urba As String = ""
            'Dim cod_urba As String = ""
            'If colecciones_urba.Count = 0 Then
            '    lista_urba = "El DM evaluado no se encuentra superpuesto a un área urbana"
            'End If
            'For contador As Integer = 1 To colecciones_urba.Count
            '    nm_urba = colecciones_urba.Item(contador)
            '    If contador = 1 Then
            '        lista_urba = nm_urba
            '    ElseIf contador > 1 Then
            '        lista_urba = lista_urba & "," & nm_urba
            '    End If
            'Next contador
            'colecciones_urba.Clear()

            Dim lodbtExisteAu As New DataTable
            Try
                lodbtExisteAu = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(3, gstrFC_Catastro_Minero & gloZona, gstrFC_ZUrbana & gloZona, v_codigo)
            Catch ex As Exception
            End Try


            Dim nm_urba As String = ""
            If lodbtExisteAu.Rows.Count = 0 Then
                lista_urba = "El DM evaluado no se encuentra superpuesto a un Area Urbana"
            End If

            If lodbtExisteAR.Rows.Count >= 1 Then
                For contador As Integer = 0 To lodbtExisteAu.Rows.Count - 1
                    nm_urba = lodbtExisteAu.Rows(contador).Item("NM_URBA")
                    If contador = 0 Then
                        lista_urba = nm_urba
                    ElseIf contador > 0 Then
                        lista_urba = lista_urba & "," & nm_urba
                    End If
                Next contador
            End If


            ''Se aumento para capturar demarcacion principal ZU
            ''--------------------------------------------------

            'For contador As Integer = 1 To colecciones_codurba.Count
            '    cod_urba = colecciones_codurba.Item(contador)

            'Next contador
            ''colecciones_codurba.Clear()

            'Comentado para ser utilizado en formatos

            seleccion_capa = "Rio"
            Dim valor As String
            valor = cls_Oracle.FT_Selecciona_capaxDM(v_codigo, seleccion_capa)
            If Val(valor) > 0 Then
                validad_rio = True
            Else
                validad_rio = False
            End If

            seleccion_capa = "Carretera"

            valor = cls_Oracle.FT_Selecciona_capaxDM(v_codigo, seleccion_capa)
            If Val(valor) > 0 Then
                validad_carr = True
            Else
                validad_carr = False
            End If

            seleccion_capa = "Frontera"

            valor = cls_Oracle.FT_Selecciona_capaxDM(v_codigo, seleccion_capa)
            If Val(valor) > 0 Then
                validad_front = True
            Else
                validad_front = False
            End If
            If validad_front = True Then
                Dim valor1 As New DataTable
                valor1 = cls_Oracle.F_Obtiene_Datos_DMXPAISES(v_codigo)

                If valor1.Rows.Count = 0 Then
                Else
                    Dim NM As String
                    validad_paises = ""
                    For i As Integer = 0 To valor1.Rows.Count - 1
                        NM = valor1.Rows(i).Item("NOMBRE").ToString
                        If NM <> "PERU" Then
                            validad_paises = validad_paises & "/" & NM
                        End If
                    Next i

                    validad_paises = Right(validad_paises, Len(validad_paises) - 1)

                End If
            End If
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Carta, m_application, "2", False)
            Dim lo_Filtro_Dpto2 As String = cls_eval.f_Intercepta_temas("Cuadrangulo", v_este_min, v_norte_min, v_este_max, v_norte_max, pApp, loStrShapefile)
            lista_hojas = lo_Filtro_Dpto2
            Dim cd_carta As String
            For contador As Integer = 1 To colecciones.Count
                cd_carta = colecciones.Item(contador)
                If contador = 1 Then
                    lista_cartas = cd_carta
                ElseIf contador > 1 Then
                    lista_cartas = lista_cartas & "," & cd_carta
                End If
            Next contador
            cls_prueba.AddFieldDM(loStrShapefile) : cls_catastro.Quitar_Layer(loStrShapefile, pApp)
            cls_catastro.Add_ShapeFile(loStrShapefile, pApp)
            If V_caso_simu = "SI" Then
                cls_eval.insertafeature_tema()
                cls_catastro.Quitar_Layer("DM_" & v_codigo, pApp)
            End If
            cls_catastro.UpdateValue(lo_Filtro, pApp)
            cls_catastro.Update_Value_DM(pApp, v_codigo)


            lodtbLeyenda = cls_prueba.f_Genera_Leyenda_DM("Catastro", lo_Filtro, pApp)


            If V_caso_simu = "SI" Then
                Try
                    Kill(glo_pathTMP & "\DM_" & v_codigo & "*.*")
                Catch ex As Exception
                End Try
                cls_eval.consultacapaDM(v_codigo, "", "Catastro")
                arch_cata = "Cata"
                cls_catastro.Exportando_Temas("DM", "Catastro", pApp)
            End If
            If v_vigcat = "G" Then
                cls_eval.consultacapaDM(v_codigo, "", "Catastro")

                cls_eval.seleccionadmsegunditancia("Catastro", "Interceptando")

                cls_eval.consultacapaDM(v_codigo, "", "Catastro")
                cls_eval.seleccionadmsegunditancia("Catastro", "colidantes")


                cls_eval.actualizaregistrostema("XVECINOS")   ' Actualiza registros en el tema para casos VE (DM vecinos)
                cls_eval.actualizaregistrostema("XDM")
                cls_eval.EJECUTACRITERIOS()
                cls_eval.validaprioritarios()
                arch_cata = "Interceccion"
                cls_eval.consulta_dm_prioritarios(pApp)

                If v_cantiprioritarios > 0 Then
                    cls_eval.Geoprocesamiento_temas("interceccion", pApp, "DM")
                    cls_eval.calculaareapoligonos("Areainter_" & v_codigo, "evaluacion")
                    cls_catastro.Quitar_Layer("DM_" & v_codigo, pApp)
                    cls_catastro.Quitar_Layer("Areainter_" & v_codigo, pApp)
                    cls_catastro.Quitar_Layer("Prioritarios" & v_codigo, pApp)
                    pMxDoc.ActivatedView.Refresh()


                End If
                ' PARA CASOS DE BUSCAR ANTECESOR DEL DM EVALUADO CASO REDENUNCIO
                '*******************************************************************
                If v_tipo_exp = "RD" Then
                    Dim codigov As String = ""
                    Dim codigov1 As String = ""
                    Dim con_rd As Integer = colecciones_rd.Count
                    If con_rd > 0 Then
                        For contador As Integer = 1 To colecciones_rd.Count
                            codigov = colecciones_rd.Item(contador)
                            If contador = 1 Then
                                codigov1 = codigov
                                lista_rd = "CODIGOU =  '" & codigov & "'"
                            ElseIf contador > 1 Then
                                lista_rd = lista_rd & " OR " & "CODIGOU =  '" & codigov & "'"
                            End If
                        Next contador
                        colecciones_rd.Clear()
                        cls_eval.consultacapaDM("", "Redenuncio", "Catastro")
                        capa_sele = pFeatureSelection.SelectionSet
                        If capa_sele.Count > 0 Then
                            Try
                                Kill(glo_pathTMP & "\Antecesor*.*")
                            Catch ex As Exception
                            End Try
                            arch_cata = "Redenuncio"
                            cls_catastro.Exportando_Temas("", "Catastro", pApp)
                            pFeatureSelection.Clear()
                            loStrShapefile = "Antecesor"
                            cls_catastro.Add_ShapeFile1("Antecesor", pApp, "Antecesor")
                            cls_catastro.Add_ShapeFile1("DM_" & v_codigo, m_application, "codigo")
                            cls_eval.intercepta_temas("interceccion", pApp, False)
                            If valida = True Then
                                pFeatureLayer = pMap.Layer(0)
                                pFeatureClass = pFeatureLayer.FeatureClass
                                If pFeatureLayer.FeatureClass.FeatureCount(Nothing) > 0 Then
                                    cls_eval.calculaareapoligonos("Antecesor_f", "Redenuncio")
                                    pFeatureLayer = pMap.Layer(0)
                                    'pFeatureClass = pFeatureLayer.FeatureClass
                                    pFeatureClass = pFeatureLayer.FeatureClass
                                    pFeatureCursor = pFeatureClass.Search(Nothing, False)
                                    pFields = pFeatureClass.Fields
                                    Dim v_area_f As Double
                                    pfeature = pFeatureCursor.NextFeature
                                    Do Until pfeature Is Nothing
                                        v_area_f = pfeature.Value(pFields.FindField("HECTAGIS"))
                                        v_area_f = Format(Math.Round(v_area_f, 4), "###,###.0000")
                                        If v_area_eval - v_area_f <= 0.01 Then
                                            colecciones_rd.Add(pfeature.Value(pFields.FindField("CODIGOU_1")))
                                        End If
                                        pfeature = pFeatureCursor.NextFeature
                                    Loop
                                    lista_rd = ""
                                    Dim con_rd2 As Integer = colecciones_rd.Count
                                    If con_rd2 > 0 Then
                                        For contador As Integer = 1 To colecciones_rd.Count
                                            codigov = colecciones_rd.Item(contador)
                                            If contador = 1 Then
                                                lista_rd = "CODIGOU =  '" & codigov & "'"
                                            ElseIf contador > 1 Then
                                                lista_rd = lista_rd & " OR " & "CODIGOU =  '" & codigov & "'"
                                            End If
                                        Next contador
                                    End If
                                    colecciones_rd.Clear()
                                    cls_eval.consultacapaDM("", "Redenuncio", "Catastro")
                                    lista_rd = ""
                                    pFeatureClass = pFeatureLayer.FeatureClass
                                    pFeatureCursor = pFeatureClass.Update(pQFilter, True)
                                    pFields = pFeatureClass.Fields
                                    pfeature = pFeatureCursor.NextFeature
                                    Do Until pfeature Is Nothing
                                        pfeature.Value(pFeatureCursor.FindField("EVAL")) = "AR"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        pfeature = pFeatureCursor.NextFeature
                                    Loop
                                End If
                            End If
                        End If
                        pFeatureLayer = pMap.Layer(0)
                        pMap.DeleteLayer(pFeatureLayer)
                        pFeatureLayer = pMap.Layer(0)
                        pMap.DeleteLayer(pFeatureLayer)
                        pFeatureLayer = pMap.Layer(0)
                        pMap.DeleteLayer(pFeatureLayer)
                        pMxDoc.UpdateContents()
                    End If
                End If
                cls_prueba.Poligono_Color_1("Catastro", lodtbLeyenda, glo_pathStyle & "\Catastro.style", "LEYENDA", "", pApp)


                'cls_catastro.ShowLabel_DM("Catastro", pApp)
                cls_catastro.rotulatexto_dm("Catastro", pApp)
                cls_catastro.Genera_Malla_UTM(lo_xMin, lo_yMin, lo_xMax, lo_yMax, v_zona_dm, pApp)
                cls_catastro.Rotular_texto_DM("Mallap_" & v_zona_dm, v_zona_dm, pApp)
                cls_catastro.Quitar_Layer("Mallap_" & v_zona_dm, pApp)
                cls_catastro.Style_Linea_GDB("Malla_" & v_zona_dm, glo_pathStyle & "\malla.style", "CLASE", pApp, "GDB")
                'Obteniendo Calculo de distancia a la frontera
                cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Frontera, m_application, "1", True)
                If V_caso_simu = "SI" Then
                    loStrShapefile = "Simulado" & fecha_archi2
                    cls_catastro.Add_ShapeFile1(loStrShapefile, pApp, "Simu")
                Else
                    loStrShapefile = "DM_" & v_codigo
                    cls_catastro.Add_ShapeFile(loStrShapefile, pApp)
                End If
                cls_eval.calculadistanciafrontera(pApp)
                pMap.DeleteLayer(pMap.Layer(0))
                pMxDoc.ActivatedView.Refresh()
                pMxDoc.UpdateContents()
                If distancia_fron <= 50 Then
                    cls_catastro.Style_Linea_GDB(glo_Owner_Layer_SDE & gstrFC_Frontera, glo_pathStyle & "\FRONTERA.style", "NM_FRON", pApp, "SDE", "Nombre")
                Else
                    cls_catastro.Quitar_Layer("Limite Frontera", pApp)
                End If

                'Demarcacion Politica

               
                caso_consulta = "DEMARCACION POLITICA"
                cls_eval.adicionadataframe("DEMARCACION POLITICA")
                arch_cata = ""
                cls_eval.activadataframe("DEMARCACION POLITICA")


                If V_caso_simu = "SI" Then
                    loStrShapefile = "Simulado" & fecha_archi2
                    cls_catastro.Add_ShapeFile1(loStrShapefile, pApp, "Simu")
                Else
                    loStrShapefile = "DM_" & v_codigo
                    cls_catastro.Add_ShapeFile(loStrShapefile, pApp)
                End If
                cls_catastro.Shade_Poligono("Catastro", m_application)

                cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, pApp, "1", False)
                cls_eval.DefinitionExpressiontema(lista_nm_depa, pApp, "Departamento")
                cls_catastro.Shade_Poligono("Departamento", pApp)
                cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Provincia, pApp, "1", False)
                cls_eval.DefinitionExpressiontema(lista_nm_depa, pApp, "Provincia")
                cls_catastro.Shade_Poligono("Provincia", pApp)
                cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito, pApp, "1", False)
                cls_eval.DefinitionExpressiontema(lista_nm_depa, pApp, "Distrito")
                cls_catastro.Shade_Poligono("Distrito", pApp)
                'If V_caso_simu = "SI" Then
                '    loStrShapefile = "Simulado" & fecha_archi2
                '    cls_catastro.Add_ShapeFile1(loStrShapefile, pApp, "Simu")
                'Else
                '    loStrShapefile = "DM_" & v_codigo
                '    cls_catastro.Add_ShapeFile(loStrShapefile, pApp)
                'End If
                cls_catastro.ShowLabel_DM("Distrito", pApp)
                'cls_catastro.rotulatexto_dm("Distrito", pApp)
                'cls_catastro.rotulatexto_dm("Provincia", pApp)
                'cls_catastro.rotulatexto_dm("Departamento", pApp)
                cls_catastro.ShowLabel_DM("Provincia", pApp)
                cls_catastro.ShowLabel_DM("Departamento", pApp)
                cls_catastro.Ordenacapa_vista("Catastro")
                cls_catastro.Zoom_to_Layer("Catastro")
                'arch_cata = "Catastro"
                'cls_catastro.Color_Poligono_Simple(pApp, "Catastro")


                'cls_catastro.Shade_Poligono("Catastro", m_application)
                cls_eval.asigna_escaladataframe("DEMARCACION POLITICA")
                escala_plano_dema = pMap.MapScale

                'Agrega Carta IGN
                arch_cata = ""
                caso_consulta = "CARTA IGN"
                cls_eval.adicionadataframe("CARTA IGN")
                cls_eval.activadataframe("CARTA IGN")

                Dim cont_v As Integer
                Dim CARTA_V1 As String
                Dim nmhojas1 As String
                Dim con_lista As Integer
                con_lista = colecciones.Count
                Dim lista_cartas_ecw As String

                For cont_v = 1 To con_lista
                    CARTA_V1 = colecciones.Item(cont_v)
                    carta_v = CARTA_V1.Replace("-", "")
                    If cont_v = 1 Then
                        lista_cartas = "CD_HOJA =  '" & CARTA_V1 & "'"
                        ' lista_cd_cartas = CARTA_V1
                        lista_cartas_ecw = "NAME =  '" & LCase(carta_v) & ".ecw'"
                        lista_cd_cartas = CARTA_V1
                    Else
                        lista_cartas = lista_cartas & " OR " & "CD_HOJA =  '" & CARTA_V1 & "'"
                        'lista_cd_cartas = lista_cd_cartas & "," & CARTA_V1
                        lista_cartas_ecw = lista_cartas_ecw & " OR " & "NAME =  '" & LCase(carta_v) & ".ecw'"
                        lista_cd_cartas = lista_cd_cartas & "," & CARTA_V1
                    End If
                    carta_v = CARTA_V1.Replace("-", "")

                    'cls_catastro.AddImagen(glo_pathIGN & carta_v & ".ECW", "1", carta_v, pApp, True)

                    ''************************************
                    Dim filtro_carta As String = "NAME = '" & LCase(carta_v.Replace("-", "")) & ".ecw'"
                    'Dim filtro_carta As String = "NAME = '" & carta_v & ".ecw'"
                    'Dim pActiveView As IActiveView
                    Conexion_SDE(pApp)
                    'pActiveView = pMap
                    pRasterCatalog = GetRasterCatalog(pApp, "DATA_GIS.DS_IGN_CARTA_NACIONAL_GEOPSA56")
                    AddRasterCatalogLayer(pApp, pRasterCatalog)
                    MyDefinitionQuery("DATA_GIS.DS_IGN_CARTA_NACIONAL_GEOPSA56", pApp, carta_v, filtro_carta, "1")
                    pFSelQuery.Clear()
                    cls_catastro.Quitar_Layer("DATA_GIS.DS_IGN_CARTA_NACIONAL_GEOPSA56", pApp)

                Next cont_v
                colecciones.Clear()
                For cont_v = 1 To con_lista
                    nmhojas1 = colecciones_nmhojas.Item(cont_v)
                    If cont_v = 1 Then
                        lista_nmhojas_ign = nmhojas1
                    Else
                        lista_nmhojas_ign = lista_nmhojas & "," & nmhojas1
                    End If
                Next cont_v
                colecciones_nmhojas.Clear()

                If V_caso_simu = "SI" Then
                    loStrShapefile = "Simulado" & fecha_archi2
                    cls_catastro.Add_ShapeFile1(loStrShapefile, pApp, "Simu")
                Else
                    loStrShapefile = "DM_" & v_codigo
                    cls_catastro.Add_ShapeFile(loStrShapefile, pApp)
                End If
                cls_catastro.Shade_Poligono("Catastro", m_application)
                cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "1", False)
                cls_eval.DefinitionExpressiontema(lista_nm_depa, pApp, "Departamento")
                cls_catastro.Shade_Poligono("Departamento", pApp)
                cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Provincia, m_application, "1", False)
                cls_eval.DefinitionExpressiontema(lista_nm_depa, pApp, "Provincia")
                cls_catastro.Shade_Poligono("Provincia", pApp)
                cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito, m_application, "1", False)
                cls_eval.DefinitionExpressiontema(lista_nm_depa, pApp, "Distrito")
                cls_catastro.Shade_Poligono("Distrito", pApp)
                'If V_caso_simu = "SI" Then
                '    loStrShapefile = "Simulado" & fecha_archi2
                '    cls_catastro.Add_ShapeFile1(loStrShapefile, pApp, "Simu")
                'Else
                '    loStrShapefile = "DM_" & v_codigo
                '    cls_catastro.Add_ShapeFile(loStrShapefile, pApp)
                'End If
                cls_catastro.Ordenacapa_vista("Catastro")
                cls_catastro.Zoom_to_Layer("Catastro")
                
                'cls_catastro.Shade_Poligono("Catastro", m_application)
                cls_catastro.ShowLabel_DM("Distrito", pApp)
                'cls_catastro.rotulatexto_dm("Distrito", pApp)
                cls_eval.asigna_escaladataframe("CARTA IGN")
                escala_plano_carta = pMap.MapScale
                arch_cata = ""
                caso_consulta = "CATASTRO MINERO"
                cls_eval.activadataframe("CATASTRO MINERO")
                'Dim m_form As New Frm_Resultado_Eval
                'If Not m_form.Visible Then
                'x = 1
                'm_form.m_application = m_application
                'm_form.Show()
                'End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Observación...")
        End Try
    End Sub

    Public Sub Consulta_Evaluacion_DM_X(ByVal pApp As IApplication, ByVal p_clbLayer As Object)
        Try
            fecha_archi = DateTime.Now.Ticks.ToString
            Dim cls_eval As New Cls_evaluacion
            Dim cls_prueba As New cls_Prueba
            Dim cls_catastro As New cls_DM_1
            'Dim formulario As New Frm_Eval_segun_codigo
            Dim lostrFiltro As String = ""
            Dim capa_sele As ISelectionSet
            Dim pFeatureCursor As IFeatureCursor
            Dim pfeature As IFeature
            valida = False
            distancia_fron = 0
            Dim lodtbLeyenda As New DataTable
            Dim form1 As New Frm_Eval_segun_codigo
            Dim lostrMensaje As String = 6
            'Dim loglo_MensajeDM As String = 6
            Dim loStrShapefile As String = "Catastro" & fecha_archi
            Dim lo_xMin As Double = (v_dato1 - v_radio * 1000)
            Dim lo_xMax As Double = (v_dato3 + v_radio * 1000)
            Dim lo_yMin As Double = (v_dato2 - v_radio * 1000)
            Dim lo_yMax As Double = (v_dato4 + v_radio * 1000)
            gloEsteMin = v_dato1 : gloEsteMax = v_dato3

            gloNorteMin = v_dato2 : gloNorteMax = v_dato4 : gloZona = v_zona_dm
            loglo_MensajeDM = lostrMensaje
            If V_caso_simu = "SI" Then v_vigcat = "G"
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & v_zona_dm, pApp, "2", True)
            'cls_catastro.Conexion_SDE(m_application)
            cls_catastro.Delete_Rows_FC_GDB("Malla_" & v_zona_dm)
            cls_catastro.Delete_Rows_FC_GDB("Mallap_" & v_zona_dm)
            cls_catastro.Load_FC_GDB("Malla_" & v_zona_dm, "", pApp, True)
            cls_catastro.Load_FC_GDB("Mallap_" & v_zona_dm, "", pApp, True)
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito, m_application, "2", False)
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_AReservada56, m_application, "2", False)
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_ZUrbana & v_zona_dm, m_application, "2", True)
            Dim lo_Filtro As String
            lo_Filtro = cls_catastro.f_Intercepta_FC("Catastro", lo_xMin, lo_yMin, lo_xMax, lo_yMax, pApp, loStrShapefile)
            glo_xMin = lo_xMin : glo_yMin = lo_yMin : glo_xMax = lo_xMax : glo_yMax = lo_yMax

            If V_caso_simu = "" Then
                Try
                    Kill(glo_pathTMP & "\DM_" & v_codigo & "*.*")
                Catch ex As Exception
                End Try
                cls_eval.consultacapaDM(v_codigo, "DM", "Catastro")
                If pFeatureSelection.SelectionSet.Count = 0 Then
                    'MsgBox("No Existe el DM Consultado en la Base Grafica", MsgBoxStyle.Critical, "[BDGEOCATMIN]")
                    loglo_MensajeDM = 7
                    cls_eval.cierra_ejecutable()
                    cls_catastro.Borra_Todo_Feature("", pApp)
                    cls_catastro.Limpiar_Texto_Pantalla(pApp)
                    MsgBox("No existe el Derecho Minero en la Base Grafica..", MsgBoxStyle.Information, "[BDGEOCATMIN]")
                    Exit Sub

                End If
                arch_cata = "Cata"
                cls_eval.DefinitionExpressiontema(v_codigo, pApp, "Catastro")
                cls_eval.obtienelimitesmaximos("Catastro")
            Else
                v_codigo = "000000001"
                cls_catastro.Add_ShapeFile1("Simulado" & fecha_archi2, pApp, "codigo")
            End If

            cls_eval.f_Intercepta_temas("Distrito", v_este_min, v_norte_min, v_este_max, v_norte_max, pApp, loStrShapefile)

            Dim nm_dist As String = ""
            If colecciones_dist.Count = 0 Then
                lista_dist = ""
            End If
            For contador As Integer = 1 To colecciones_dist.Count
                nm_dist = colecciones_dist.Item(contador)
                If contador = 1 Then
                    lista_dist = nm_dist
                ElseIf contador > 1 Then
                    lista_dist = lista_dist & "," & nm_dist
                End If
            Next contador
            colecciones_dist.Clear()

            Dim nm_depa As String = ""
            Dim nm_depa1 As String = ""
            If colecciones_depa.Count = 0 Then
                lista_depa = ""
            End If
            For contador As Integer = 1 To colecciones_depa.Count
                nm_depa = colecciones_depa.Item(contador)
                If contador = 1 Then
                    lista_depa = nm_depa
                    lista_nm_depa = "NM_DEPA =  '" & nm_depa & "'"
                    nm_depa1 = nm_depa

                ElseIf contador > 1 Then
                    If nm_depa <> nm_depa1 Then
                        lista_depa = lista_depa & "," & nm_depa
                        lista_nm_depa = lista_nm_depa & " OR " & "NM_DEPA =  '" & nm_depa & "'"
                        nm_depa1 = nm_depa
                    End If
                End If
            Next contador
            colecciones_depa.Clear()
            'Obtiene lista de provincias x dm

            Dim nm_prov As String = ""
            Dim nm_prov1 As String = ""
            If colecciones_prov.Count = 0 Then
                lista_prov = ""
            End If
            For contador As Integer = 1 To colecciones_prov.Count
                nm_prov = colecciones_prov.Item(contador)
                If contador = 1 Then
                    lista_prov = nm_prov
                    nm_prov1 = nm_prov
                ElseIf contador > 1 Then
                    If nm_prov <> nm_prov1 Then
                        lista_prov = lista_prov & "," & nm_prov
                        nm_prov1 = nm_prov
                    End If
                End If
            Next contador
            colecciones_prov.Clear()
            'cls_catastro.Quitar_Layer("Distrito", pApp)  '


            'Seleccionar las areas reservadas x DM evaluado
            cls_eval.f_Intercepta_temas("Zona Reservada", v_este_min, v_norte_min, v_este_max, v_norte_max, pApp, loStrShapefile)

            Dim nm_rese As String = ""
            If colecciones_rese.Count = 0 Then
                lista_rese = "El DM evaluado no se encuentra superpuesto a un área reservada"
            End If
            For contador As Integer = 1 To colecciones_rese.Count
                nm_rese = colecciones_rese.Item(contador)
                If contador = 1 Then
                    lista_rese = nm_rese
                ElseIf contador > 1 Then
                    lista_rese = lista_rese & "," & nm_rese
                End If
            Next contador
            colecciones_rese.Clear()
            'Seleccionar las areas urbanas x DM evaluado
            cls_eval.f_Intercepta_temas("Zona Urbana", v_este_min, v_norte_min, v_este_max, v_norte_max, pApp, loStrShapefile)
            Dim nm_urba As String = ""
            If colecciones_urba.Count = 0 Then
                lista_urba = "El DM evaluado no se encuentra superpuesto a un área urbana"
            End If
            For contador As Integer = 1 To colecciones_urba.Count
                nm_urba = colecciones_urba.Item(contador)
                If contador = 1 Then
                    lista_urba = nm_urba
                ElseIf contador > 1 Then
                    lista_urba = lista_urba & "," & nm_urba
                End If
            Next contador
            colecciones_urba.Clear()
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Carta, m_application, "2", False)
            Dim lo_Filtro_Dpto2 As String = cls_eval.f_Intercepta_temas("Cuadrangulo", v_este_min, v_norte_min, v_este_max, v_norte_max, pApp, loStrShapefile)

            lista_hojas = lo_Filtro_Dpto2
            Dim cd_carta As String
            For contador As Integer = 1 To colecciones.Count
                cd_carta = colecciones.Item(contador)
                If contador = 1 Then
                    lista_cartas = cd_carta
                ElseIf contador > 1 Then
                    lista_cartas = lista_cartas & "," & cd_carta
                End If
            Next contador
            cls_prueba.AddFieldDM(loStrShapefile) : cls_catastro.Quitar_Layer(loStrShapefile, pApp)
            cls_catastro.Add_ShapeFile(loStrShapefile, pApp)
            If V_caso_simu = "SI" Then
                cls_eval.insertafeature_tema()
                cls_catastro.Quitar_Layer("DM_" & v_codigo, pApp)
            End If
            cls_catastro.UpdateValue(lo_Filtro, pApp)
            cls_catastro.Update_Value_DM(pApp, v_codigo)
            lodtbLeyenda = cls_prueba.f_Genera_Leyenda_DM("Catastro", lo_Filtro, pApp)
            If V_caso_simu = "SI" Then
                Try
                    Kill(glo_pathTMP & "\DM_" & v_codigo & "*.*")
                Catch ex As Exception
                End Try
                cls_eval.consultacapaDM(v_codigo, "", "Catastro")
                arch_cata = "Cata"
                cls_catastro.Exportando_Temas("DM", "Catastro", pApp)
            End If


            If v_vigcat = "G" Then
                cls_eval.consultacapaDM(v_codigo, "", "Catastro")
                cls_eval.seleccionadmsegunditancia("Catastro", "Interceptando")
                cls_eval.consultacapaDM(v_codigo, "", "Catastro")
                cls_eval.seleccionadmsegunditancia("Catastro", "colidantes")
                cls_eval.actualizaregistrostema("XVECINOS")   ' Actualiza registros en el tema para casos VE (DM vecinos)
                cls_eval.actualizaregistrostema("XDM")
                cls_eval.EJECUTACRITERIOS()
                cls_eval.validaprioritarios()
                ' PARA CASOS DE BUSCAR ANTECESOR DEL DM EVALUADO CASO REDENUNCIO
                '*******************************************************************
                If v_tipo_exp = "RD" Then

                    Dim codigov As String = ""
                    Dim codigov1 As String = ""
                    Dim con_rd As Integer = colecciones_rd.Count
                    If con_rd > 0 Then
                        For contador As Integer = 1 To colecciones_rd.Count
                            codigov = colecciones_rd.Item(contador)
                            If contador = 1 Then
                                codigov1 = codigov
                                lista_rd = "CODIGOU =  '" & codigov & "'"
                            ElseIf contador > 1 Then
                                lista_rd = lista_rd & " OR " & "CODIGOU =  '" & codigov & "'"
                            End If
                        Next contador
                        colecciones_rd.Clear()
                        cls_eval.consultacapaDM("", "Redenuncio", "Catastro")
                        capa_sele = pFeatureSelection.SelectionSet
                        If capa_sele.Count > 0 Then
                            Try
                                Kill(glo_pathTMP & "\Antecesor*.*")
                            Catch ex As Exception
                            End Try
                            arch_cata = "Redenuncio"
                            cls_catastro.Exportando_Temas("", "Catastro", pApp)
                            pFeatureSelection.Clear()
                            loStrShapefile = "Antecesor"
                            cls_catastro.Add_ShapeFile1("Antecesor", pApp, "Antecesor")
                            cls_catastro.Add_ShapeFile1("DM_" & v_codigo, m_application, "codigo")
                            cls_eval.intercepta_temas("interceccion", pApp, False)
                            If valida = True Then
                                pFeatureLayer = pMap.Layer(0)
                                pFeatureClass = pFeatureLayer.FeatureClass
                                If pFeatureLayer.FeatureClass.FeatureCount(Nothing) > 0 Then
                                    cls_eval.calculaareapoligonos("Antecesor_f", "Redenuncio")
                                    pFeatureLayer = pMap.Layer(0)
                                    pFeatureClass = pFeatureLayer.FeatureClass
                                    pFeatureClass = pFeatureLayer.FeatureClass
                                    pFeatureCursor = pFeatureClass.Search(Nothing, False)
                                    pFields = pFeatureClass.Fields
                                    Dim v_area_f As Double
                                    pfeature = pFeatureCursor.NextFeature
                                    Do Until pfeature Is Nothing
                                        v_area_f = pfeature.Value(pFields.FindField("HECTAGIS"))
                                        v_area_f = Format(Math.Round(v_area_f, 4), "###,###.0000")
                                        If v_area_eval - v_area_f <= 0.01 Then
                                            colecciones_rd.Add(pfeature.Value(pFields.FindField("CODIGOU_1")))
                                        End If
                                        pfeature = pFeatureCursor.NextFeature
                                    Loop
                                    lista_rd = ""
                                    Dim con_rd2 As Integer = colecciones_rd.Count
                                    If con_rd2 > 0 Then
                                        For contador As Integer = 1 To colecciones_rd.Count
                                            codigov = colecciones_rd.Item(contador)
                                            If contador = 1 Then
                                                lista_rd = "CODIGOU =  '" & codigov & "'"
                                            ElseIf contador > 1 Then
                                                lista_rd = lista_rd & " OR " & "CODIGOU =  '" & codigov & "'"
                                            End If
                                        Next contador
                                    End If
                                    colecciones_rd.Clear()
                                    cls_eval.consultacapaDM("", "Redenuncio", "Catastro")
                                    lista_rd = ""
                                    pFeatureClass = pFeatureLayer.FeatureClass
                                    pFeatureCursor = pFeatureClass.Update(pQFilter, True)
                                    pFields = pFeatureClass.Fields
                                    pfeature = pFeatureCursor.NextFeature
                                    Do Until pfeature Is Nothing
                                        pfeature.Value(pFeatureCursor.FindField("EVAL")) = "AR"
                                        pFeatureCursor.UpdateFeature(pfeature)
                                        pfeature = pFeatureCursor.NextFeature
                                    Loop
                                End If
                            End If
                        End If
                        pFeatureLayer = pMap.Layer(0)
                        pMap.DeleteLayer(pFeatureLayer)
                        pFeatureLayer = pMap.Layer(0)
                        pMap.DeleteLayer(pFeatureLayer)
                        pFeatureLayer = pMap.Layer(0)
                        pMap.DeleteLayer(pFeatureLayer)
                        pMxDoc.UpdateContents()
                    End If
                End If
                cls_prueba.Poligono_Color_1("Catastro", lodtbLeyenda, glo_pathStyle & "\Catastro.style", "LEYENDA", "", pApp)
                'cls_catastro.ShowLabel_DM("Catastro", pApp)
                cls_catastro.rotulatexto_dm("Catastro", pApp)
                cls_catastro.Genera_Malla_UTM(lo_xMin, lo_yMin, lo_xMax, lo_yMax, v_zona_dm, pApp)
                cls_catastro.Rotular_texto_DM("Mallap_" & v_zona_dm, v_zona_dm, pApp)
                cls_catastro.Quitar_Layer("Mallap_" & v_zona_dm, pApp)
                cls_catastro.Style_Linea_GDB("Malla_" & v_zona_dm, glo_pathStyle & "\malla.style", "CLASE", pApp, "GDB")
                'Obteniendo Calculo de distancia a la frontera
                cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Frontera, m_application, "1", True)
                If V_caso_simu = "SI" Then
                    loStrShapefile = "Simulado" & fecha_archi2
                    cls_catastro.Add_ShapeFile1(loStrShapefile, pApp, "Simu")
                Else
                    loStrShapefile = "DM_" & v_codigo
                    cls_catastro.Add_ShapeFile(loStrShapefile, pApp)
                End If
                cls_eval.calculadistanciafrontera(pApp)
                pMap.DeleteLayer(pMap.Layer(0))
                pMxDoc.ActivatedView.Refresh()
                pMxDoc.UpdateContents()

                If distancia_fron <= 50 Then
                    'cls_eval.AddLayerFromFile(pApp)
                    cls_catastro.Style_Linea_GDB(glo_Owner_Layer_SDE & gstrFC_Frontera, glo_pathStyle & "\FRONTERA.style", "NM_FRON", pApp, "SDE", "Nombre")
                    'cls_catastro.PT_CargarFeatureClass_SDE("GPO_PAI_PAISES", m_application, "1", False)
                Else
                    cls_catastro.Quitar_Layer("Limite Frontera", pApp)
                End If
                'cls_catastro.Zoom_to_Layer("Catastro")
                'cls_eval.asigna_escaladataframe("CATASTRO MINERO")
                'escala_plano_eval = pMap.MapScale
                caso_consulta = "DEMARCACION POLITICA"
                cls_eval.adicionadataframe("DEMARCACION POLITICA")
                arch_cata = ""
                cls_eval.activadataframe("DEMARCACION POLITICA")
                cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, pApp, "1", False)
                cls_eval.DefinitionExpressiontema(lista_nm_depa, pApp, "Departamento")

                cls_catastro.Shade_Poligono("Departamento", pApp)
                cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Provincia, pApp, "1", False)
                cls_eval.DefinitionExpressiontema(lista_nm_depa, pApp, "Provincia")
                cls_catastro.Shade_Poligono("Provincia", pApp)
                cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito, pApp, "1", False)
                cls_eval.DefinitionExpressiontema(lista_nm_depa, pApp, "Distrito")
                cls_catastro.Shade_Poligono("Distrito", pApp)
                If V_caso_simu = "SI" Then
                    loStrShapefile = "Simulado" & fecha_archi2
                    cls_catastro.Add_ShapeFile1(loStrShapefile, pApp, "Simu")
                Else
                    loStrShapefile = "DM_" & v_codigo
                    cls_catastro.Add_ShapeFile(loStrShapefile, pApp)
                End If
                cls_catastro.ShowLabel_DM("Distrito", pApp)
                cls_catastro.ShowLabel_DM("Provincia", pApp)
                cls_catastro.ShowLabel_DM("Departamento", pApp)
                cls_catastro.Zoom_to_Layer("Catastro")
                arch_cata = "Catastro"
                cls_catastro.Color_Poligono_Simple(pApp, "Catastro")
                cls_eval.asigna_escaladataframe("DEMARCACION POLITICA")
                escala_plano_dema = pMap.MapScale

                'Agrega Carta IGN
                arch_cata = ""
                caso_consulta = "CARTA IGN"
                cls_eval.adicionadataframe("CARTA IGN")
                cls_eval.activadataframe("CARTA IGN")

                Dim cont_v As Integer
                Dim CARTA_V1 As String
                Dim nmhojas1 As String
                Dim con_lista As Integer
                con_lista = colecciones.Count
                For cont_v = 1 To con_lista
                    CARTA_V1 = colecciones.Item(cont_v)
                    If cont_v = 1 Then
                        lista_cartas = "CD_HOJA =  '" & CARTA_V1 & "'"
                        lista_cd_cartas = CARTA_V1
                    Else
                        lista_cartas = lista_cartas & " OR " & "CD_HOJA =  '" & CARTA_V1 & "'"
                        lista_cd_cartas = lista_cd_cartas & "," & CARTA_V1
                    End If
                    carta_v = CARTA_V1.Replace("-", "")
                    cls_catastro.AddImagen(glo_pathIGN & carta_v & ".ECW", "1", carta_v, pApp, True)

                Next cont_v
                colecciones.Clear()
                For cont_v = 1 To con_lista
                    nmhojas1 = colecciones_nmhojas.Item(cont_v)
                    If cont_v = 1 Then
                        lista_nmhojas = nmhojas1
                    Else
                        lista_nmhojas = lista_nmhojas & "," & nmhojas1
                    End If
                Next cont_v
                colecciones_nmhojas.Clear()
                cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "1", False)
                cls_eval.DefinitionExpressiontema(lista_nm_depa, pApp, "Departamento")
                cls_catastro.Shade_Poligono("Departamento", pApp)
                cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Provincia, m_application, "1", False)
                cls_eval.DefinitionExpressiontema(lista_nm_depa, pApp, "Provincia")
                cls_catastro.Shade_Poligono("Provincia", pApp)
                cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito, m_application, "1", False)
                cls_eval.DefinitionExpressiontema(lista_nm_depa, pApp, "Distrito")
                cls_catastro.Shade_Poligono("Distrito", pApp)
                If V_caso_simu = "SI" Then
                    loStrShapefile = "Simulado" & fecha_archi2
                    cls_catastro.Add_ShapeFile1(loStrShapefile, pApp, "Simu")
                Else
                    loStrShapefile = "DM_" & v_codigo
                    cls_catastro.Add_ShapeFile(loStrShapefile, pApp)
                End If

                cls_catastro.Zoom_to_Layer("Catastro")
                arch_cata = "Catastro"
                cls_catastro.Color_Poligono_Simple(m_application, "Catastro")
                cls_catastro.ShowLabel_DM("Distrito", pApp)
                cls_eval.asigna_escaladataframe("CARTA IGN")
                escala_plano_carta = pMap.MapScale
                arch_cata = ""
                caso_consulta = "CATASTRO MINERO"
                cls_eval.activadataframe("CATASTRO MINERO")
                Dim m_form As New Frm_Resultado_Eval
                If Not m_form.Visible Then
                    x = 1
                    m_form.m_application = m_application
                    m_form.Show()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Observación...")
        End Try
    End Sub


    Function f_Intercepta_Carta(ByVal loFeature As String, ByVal xMin As Double, ByVal yMin As Double, ByVal xMax As Double, _
                                ByVal yMax As Double, ByVal p_Zona As String, ByVal p_App As IApplication)
        Dim pFLayer As IFeatureLayer = Nothing
        Dim lostr_Join_Codigos As String = ""
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
        'Select Case p_Zona
        '    Case 17
        '        pFLayer.Shape.SpatialReference = Datum_PSAD_18
        '        pFLayer.Shape.Project(Datum_PSAD_17)
        '    Case 19
        '        pFLayer.Shape.SpatialReference = Datum_PSAD_18
        '        pFLayer.Shape.Project(Datum_PSAD_19)
        'End Select
        Dim pActiveView As IActiveView
        Dim pDisplayTransform As IDisplayTransformation
        Dim pEnvelope As IEnvelope
        pActiveView = pMxDoc.FocusMap
        pDisplayTransform = pActiveView.ScreenDisplay.DisplayTransformation
        pEnvelope = pDisplayTransform.VisibleBounds
        'Aquí calculo el Extend
        pEnvelope.SetEmpty()
        pEnvelope.XMin = xMin : pEnvelope.YMin = yMin
        pEnvelope.XMax = xMax : pEnvelope.YMax = yMax
        pDisplayTransform.VisibleBounds = pEnvelope

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
        Dim pFeatureCursor As IFeatureCursor
        pFeatureCursor = pFLayer.Search(pSpatialFilter, True)
        Dim pRow As IRow
        pRow = pFeatureCursor.NextFeature
        If pRow Is Nothing Then
            Return ""
            v_existe = False
            Exit Function
        End If
        Do Until pRow Is Nothing
            If pRow.Value(1).ToString <> "" Then
                Select Case loFeature
                    Case "Cuadrangulo"
                        lostr_Join_Codigos = lostr_Join_Codigos & "'" & pRow.Value(pRow.Fields.FindField("CD_HOJA")) & "',"
                End Select
                pRow = pFeatureCursor.NextFeature
            End If
        Loop
        Return lostr_Join_Codigos
    End Function

    Private Sub Listar_Vertice_Area(ByVal l As IPolygon, ByVal p_ListBox As Object)
        Dim coordenada_DM(300) As Punto_DM
        Dim h, j As Integer
        Dim ptcol As IPointCollection
        Dim pt As IPoint
        'Dim l As IPolygon
        p_ListBox.Items.Add("")
        p_ListBox.Items.Add("----------------------------------------------------------------------------------")
        p_ListBox.Items.Add(Space(7) & " Vert." & Space(14) & "Este" & Space(20) & "Norte")
        p_ListBox.Items.Add("----------------------------------------------------------------------------------")
        'l = pFeature.Shape
        ptcol = l
        ReDim coordenada_DM(ptcol.PointCount)
        For j = 0 To ptcol.PointCount - 2
            pt = ptcol.Point(j)
            p_ListBox.Items.Add(Space(10) & RellenarComodin(j + 1, 3, "0") & Space(10) & Format(Math.Round(pt.X, 2), "###,###.00") & Space(10) & Format(Math.Round(pt.Y, 2), "###,###.00") & "")
            coordenada_DM(j).v = j + 1
            coordenada_DM(j).x = pt.X
            coordenada_DM(j).y = pt.Y

        Next j

        'Calcular Area
        coordenada_DM(j).x = coordenada_DM(0).x
        coordenada_DM(j).y = coordenada_DM(0).y
        Dim d0, d1, dr As Double
        d0 = d1 = dr = 0
        For h = 0 To j  ' UBound(coordenada_DM) - 1
            If coordenada_DM(h).x <> 0 Or coordenada_DM(h).y <> 0 Then
                d0 = d0 + Math.Round(coordenada_DM(h).x, 2) * Math.Round(coordenada_DM(h + 1).y, 2)
                d1 = d1 + Math.Round(coordenada_DM(h).y, 2) * Math.Round(coordenada_DM(h + 1).x, 2)
            Else
                Exit For
            End If
        Next h
        dr = Math.Round(Math.Abs((d0 - d1) / 2) / 10000, 4)
        p_ListBox.Items.Add("")
        p_ListBox.Items.Add("----------------------------------------------------------------------------------")
        p_ListBox.Items.Add(Space(15) & "Area UTM = " & Format(Math.Round(dr, 2), "###,###.00") & "  (Ha)")
        p_ListBox.Items.Add("----------------------------------------------------------------------------------")
    End Sub

    Public Sub Verifica_areasuperpuesta(ByVal p_Shapefile As String, ByVal p_App As IApplication)
        Try
            Dim pWorkspaceFactory As IWorkspaceFactory
            pWorkspaceFactory = New ShapefileWorkspaceFactory
            Dim pWorkSpace As IFeatureWorkspace
            pWorkSpace = pWorkspaceFactory.OpenFromFile(glo_pathTMP & "\", 0)
            Dim pClass As IFeatureClass
            pClass = pWorkSpace.OpenFeatureClass(p_Shapefile)
            If pClass.FeatureCount(Nothing) = 0 Then
                MsgBox("No Existen Areas Superpuestas del DM Evaluado para Generar Plano de Areas Superpuestas...", MsgBoxStyle.Information, "BDGEOCATMIN")
                Exit Sub
            Else
                v_existe_sup = True
            End If


        Catch ex As Exception
            v_existe_sup = False
            colecciones_planos.Clear()
        End Try
    End Sub

    Public Sub creandotabla_Areasup()
        Try
            Dim tabla As ITable
            Dim pFWS As IFeatureWorkspace
            Dim pWorkspaceFactory As IWorkspaceFactory
            Dim pFieldsEdit As IFieldsEdit
            Dim pFieldEdit As IFieldEdit
            Dim pField As IField
            Dim pFields As IFields = Nothing
            pWorkspaceFactory = New ShapefileWorkspaceFactory
            pFWS = pWorkspaceFactory.OpenFromFile(glo_pathTMP, 0)
            If pFields Is Nothing Then
                pFields = New Fields
                pFieldsEdit = pFields
                pFieldsEdit.FieldCount_2 = 1
                pField = New Field
                pFieldEdit = pField
                With pFieldEdit
                    .Length_2 = 16
                    .Name_2 = "CODIGO"
                    .Type_2 = esriFieldType.esriFieldTypeString
                End With
                pFieldsEdit.Field_2(0) = pField
            End If
            tabla = pFWS.CreateTable("Areainter_" & v_codigo & "_t", pFields, Nothing, Nothing, "")
            Dim pFields2 As IFields
            Dim pField2 As IField
            Dim pFieldEdit2 As IFieldEdit
            pField2 = New Field
            pFieldEdit2 = pField2
            pFields2 = tabla.Fields
            pFieldEdit2 = pField2
            With pFieldEdit2
                .Length_2 = 100
                .Name_2 = "NOMBRE"
                .Type_2 = esriFieldType.esriFieldTypeString
            End With
            tabla.AddField(pFieldEdit2)
            With pFieldEdit2
                .Length_2 = 50
                .Name_2 = "HOJA"
                .Type_2 = esriFieldType.esriFieldTypeString
            End With
            tabla.AddField(pFieldEdit2)
            With pFieldEdit2
                .Type_2 = esriFieldType.esriFieldTypeInteger
                .Name_2 = "VERTICE"
                .Precision_2 = 5
            End With
            tabla.AddField(pFieldEdit2)
            With pFieldEdit2
                .Type_2 = esriFieldType.esriFieldTypeDouble
                .Name_2 = "ESTE"
                .Precision_2 = 10
                .Scale_2 = 2
            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Type_2 = esriFieldType.esriFieldTypeDouble
                .Name_2 = "NORTE"
                .Precision_2 = 10
                .Scale_2 = 2
            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Type_2 = esriFieldType.esriFieldTypeDouble
                .Name_2 = "AREA"
                .Precision_2 = 10
                .Scale_2 = 4
            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "CONTADOR"
                .Precision_2 = 10
            End With
            tabla.AddField(pFieldEdit2)
        Catch ex As Exception
            MsgBox("Error en generar tabla", MsgBoxStyle.Critical, "Obsrevación...")
        End Try
    End Sub
    Public Function RoundF(ByVal Number As Double, Optional ByVal NumOfDigits As Long = 0) As Double
        If Not IsNumeric(Number) Then Exit Function
        Dim iPos As Long
        iPos = InStr(Number, ".")
        Dim dbDesimalPart As String 'Long
        Dim dbRoundPart As Long
        If iPos > 0 Then
            dbDesimalPart = Mid(Number, iPos + 1)
        End If
        If NumOfDigits < Len(CStr(dbDesimalPart)) Then
            dbRoundPart = Mid(dbDesimalPart, NumOfDigits + 1, 1)
        End If
        If dbRoundPart > 4 Then
            dbDesimalPart = Microsoft.VisualBasic.Left(dbDesimalPart, NumOfDigits) + 1
        Else
            dbDesimalPart = Microsoft.VisualBasic.Left(dbDesimalPart, NumOfDigits)
        End If
        RoundF = Val(Int(Number) & "." & dbDesimalPart)
    End Function
    Public Sub Pinta_Grilla_Dm(ByVal p_dgdDetalle As Object)
        p_dgdDetalle.BackColor = System.Drawing.Color.FromArgb(242, 242, 240)
        p_dgdDetalle.HeadingStyle.BackColor = System.Drawing.Color.FromArgb(207, 209, 221)
        p_dgdDetalle.OddRowStyle.BackColor = System.Drawing.Color.FromArgb(229, 232, 239)
        p_dgdDetalle.EvenRowStyle.BackColor = System.Drawing.Color.FromArgb(242, 242, 240)
    End Sub
    Public Function Seleccionar_Items_x_Codigo_1(ByVal p_Filtro As String, ByVal p_App As IApplication) As IFeature
        Dim frm_Consulta As New frm_Grafica_Consulta_DM
        Dim pQueryFilter As IQueryFilter = Nothing
        Dim pActiveView As IActiveView
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        Dim afound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Catastro" Then
                pFeatureLayer = pMap.Layer(A)
                afound = True : Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra el Layer") : Return Nothing : Exit Function
        End If
        Dim pFeatureSelection As IFeatureSelection = pFeatureLayer
        If Not pFeatureSelection Is Nothing Then
            ' Prepare a query filter.
            pQueryFilter = New QueryFilter
            pQueryFilter_pol = New QueryFilter
            pQueryFilter.WhereClause = p_Filtro
            pQueryFilter_pol = pQueryFilter
            ' Refresh or erase any previous selection.
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        End If
        ' Refresh again to draw the new selection.
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        Dim pFSel As IFeatureSelection
        pFSel = pFeatureLayer
        Dim pFCursor As IFeatureCursor
        pFCursor = pFeatureLayer.Search(pQueryFilter, True)
        pFeature = pFCursor.NextFeature
        If pFSel.SelectionSet.Count = 0 Then
            MsgBox("No hay ninguna Selección") : Return Nothing : Exit Function
        End If
        Return pFeature
    End Function

    Public Sub Clip_SDE(ByVal p_input As String, ByVal p_clip As String, ByVal p_Output As String, ByVal pApp As IApplication)
        'cls_catastro.PT_Connectar()
        'Conexion_SDE(pApp)
        Dim pInfc As IFeatureClass
        pInfc = pFeatureWorkspace.OpenFeatureClass(p_input)
        Dim pWorkspaceFactory1 As IWorkspaceFactory
        Dim pFeatureWorkspace1 As IFeatureWorkspace

        pWorkspaceFactory1 = New ShapefileWorkspaceFactory
        pFeatureWorkspace1 = pWorkspaceFactory1.OpenFromFile(glo_pathTMP, 0)

        Dim pClipfc As IFeatureClass
        pClipfc = pFeatureWorkspace1.OpenFeatureClass(p_clip)
        Dim pGP As Object
        pGP = CreateObject("esriGeoprocessing.GPDispatch.1")
        Try
            pGP.Workspace = glo_pathTMP
            pGP.Clip_analysis(pInfc, pClipfc, p_Output)
        Catch ex As Exception
            MsgBox(pGP.GetMessages(), vbOKOnly, "Clip")
        End Try
    End Sub
    Public Sub Genera_Poligono(ByVal p_Coordenada As Object, ByVal p_Zona As String, ByVal p_App As IApplication)
        Dim cls_catastro As New cls_DM_1
        Dim loStrFCaja As String = "F_" & DateTime.Now.Ticks.ToString()
        fecha_archi2 = DateTime.Now.Ticks.ToString()
        Dim cls_eval As New Cls_evaluacion
        Dim frm As New Frm_Eval_segun_codigo
        Dim cls_Prueba As New cls_Prueba
        Dim cls_DM_2 As New cls_DM_2
        Dim lodtRegistro As New DataTable
        For i As Integer = 0 To p_Coordenada.Items.Count - 1
            p_Coordenada.SelectedIndex = i
            Select Case i
                Case 0
                    lodtRegistro.Columns.Add("CG_CODIGO", Type.GetType("System.String"))
                    lodtRegistro.Columns.Add("PE_NOMDER", Type.GetType("System.String"))
                    lodtRegistro.Columns.Add("CD_COREST", Type.GetType("System.Double"))
                    lodtRegistro.Columns.Add("CD_CORNOR", Type.GetType("System.Double"))
                    lodtRegistro.Columns.Add("CD_NUMVER", Type.GetType("System.Double"))
            End Select
            Dim dRow As DataRow
            dRow = lodtRegistro.NewRow
            dRow.Item("CG_CODIGO") = "00000000"
            dRow.Item("PE_NOMDER") = "Frontera"
            Dim lostrEste As String = Mid(p_Coordenada.Text, 1, InStr(p_Coordenada.Text, ";") - 1)
            dRow.Item("CD_COREST") = CType(Mid(lostrEste, InStr(lostrEste, ":") + 3), Double)
            dRow.Item("CD_CORNOR") = CType(Mid(p_Coordenada.Text, InStr(p_Coordenada.Text, ";") + 2), Double)
            dRow.Item("CD_NUMVER") = i + 1
            lodtRegistro.Rows.Add(dRow)
        Next
        Dim lodtvOrdena_xEste As New DataView(lodtRegistro, Nothing, "CD_COREST ASC", DataViewRowState.CurrentRows)
        glo_xMin = lodtvOrdena_xEste.Item(0).Row("CD_COREST")
        glo_xMax = lodtvOrdena_xEste.Item(lodtvOrdena_xEste.Count - 1).Row("CD_COREST")
        Dim lodtvOrdena_y As New DataView(lodtRegistro, Nothing, "CD_CORNOR ASC", DataViewRowState.CurrentRows)
        glo_yMin = lodtvOrdena_y.Item(0).Row("CD_CORNOR")
        glo_yMax = lodtvOrdena_y.Item(lodtvOrdena_y.Count - 1).Row("CD_CORNOR")
        cls_Prueba.Create_Shapefile_DM(glo_pathTMP, loStrFCaja, p_Zona, "Poligono")
        cls_DM_2.Genera_Poligono_One(loStrFCaja, lodtRegistro, p_Zona, p_App, frm.txtExiste)
        Dim loStrFrontera As String = "Frontera_" & DateTime.Now.Ticks.ToString()
        Clip_SDE(glo_Owner_Layer_SDE & gstrFC_Frontera, loStrFCaja, loStrFrontera, p_App)
        Dim lostr_Verifica As String = Check_FeatureClass(loStrFrontera, p_App)
        Select Case lostr_Verifica
            Case "0"
                cls_catastro.Quitar_Layer("DM_Simulado", p_App)
                cls_catastro.Quitar_Layer(loStrFrontera, p_App)
            Case Else
                cls_catastro.Quitar_Layer("DM_Simulado", p_App)
                cls_catastro.Quitar_Layer(loStrFrontera, p_App)
                cls_catastro.Add_ShapeFile(loStrFrontera, p_App)
                cls_catastro.Style_Linea_GDB(glo_Owner_Layer_SDE & gstrFC_Frontera, glo_pathStyle & "\FRONTERA.style", "NM_FRON", p_App, "SDE", "Nombre")
        End Select
        '  MsgBox("")

        'cls_Prueba.Create_Shapefile_DM(glo_pathTMP, loStrFrontera, p_Zona)
        'cls_DM_2.Genera_Poligono_One(loStrFrontera, lodtRegistro, p_Zona, p_App, frm.txtExiste)
        'Clip_SDE(gstrFC_Frontera, loStrFrontera, "Limite_Frontera", p_App)
    End Sub

    Public Function Leer_Tabla(ByVal p_Tabla As String, ByVal p_Filtro As String) As DataTable
        Dim pQueryDef As IQueryDef
        Dim pRow As IRow
        Dim pRow_1 As IRow
        Dim pCursor As ICursor
        Dim pCursor_1 As ICursor
        Dim pDataset As IDataset
        Dim pDataset_1 As IDataset
        Dim lodtbTipo As New DataTable
        Try
            Conexion_GeoDatabase()
            pTable = pFeatureWorkspace.OpenTable(p_Tabla)
            pDataset = pTable
            pFeatureWorkspace = pDataset.Workspace
            pQueryDef = pFeatureWorkspace.CreateQueryDef
            With pQueryDef
                .Tables = pDataset.Name
                .WhereClause = p_Filtro ' "USUARIO = '" & p_Usuario & "'"
                pCursor = .Evaluate
            End With
            pRow = pCursor.NextRow
            Dim dRow As DataRow
            'Dim dRow_1 As DataRow
            Select Case p_Tabla
                Case "PERFIL_MENU"
                    Dim lo_Filtro_Menu As String = ""
                    lodtbTipo.Columns.Add("CODIGO", Type.GetType("System.String"))
                    lodtbTipo.Columns.Add("DESCRIPCION", Type.GetType("System.String"))
                    Do Until pRow Is Nothing
                        If pRow.Value(1) = p_Filtro Then
                            lo_Filtro_Menu = lo_Filtro_Menu & pRow.Value(2) & ","
                        End If
                        pRow = pCursor.NextRow
                    Loop
                    lo_Filtro_Menu = Mid(lo_Filtro_Menu, 1, Len(lo_Filtro_Menu) - 1)
                    pTable = pFeatureWorkspace.OpenTable("TABLA_OPCIONES")
                    pDataset = pTable
                    With pQueryDef
                        .Tables = pDataset.Name
                        .WhereClause = "NUM IN ( " & lo_Filtro_Menu & " ) AND MENU = 'MENU' ORDER BY DESCRIPCION"
                        pCursor = .Evaluate
                    End With
                    pRow = pCursor.NextRow
                    dRow = lodtbTipo.NewRow
                    dRow.Item("CODIGO") = ""
                    dRow.Item("DESCRIPCION") = "-- Seleccione una Opción --"
                    lodtbTipo.Rows.Add(dRow)
                    Do Until pRow Is Nothing
                        dRow = lodtbTipo.NewRow
                        dRow.Item("CODIGO") = pRow.Value(2)
                        dRow.Item("DESCRIPCION") = pRow.Value(3)
                        lodtbTipo.Rows.Add(dRow)
                        pRow = pCursor.NextRow
                    Loop
                Case "PERFIL_BOTON"
                    lodtbTipo.Columns.Add("CODIGO", Type.GetType("System.Double"))
                    lodtbTipo.Columns.Add("DESCRIPCION", Type.GetType("System.String"))
                    lodtbTipo.Columns.Add("GUID", Type.GetType("System.String"))
                    lodtbTipo.Columns.Add("ORDEN", Type.GetType("System.Double"))
                    Do Until pRow Is Nothing
                        If pRow.Value(1) = p_Filtro Then
                            dRow = lodtbTipo.NewRow
                            pTable = pFeatureWorkspace.OpenTable("TABLA_OPCIONES")
                            pDataset_1 = pTable
                            With pQueryDef
                                .Tables = pDataset_1.Name
                                .WhereClause = "NUM = " & pRow.Value(2) & " AND MENU = 'BOTON'"
                                pCursor_1 = .Evaluate
                            End With
                            pRow_1 = pCursor_1.NextRow

                            Do Until pRow_1 Is Nothing
                                dRow = lodtbTipo.NewRow
                                dRow.Item("CODIGO") = pRow.Value(2)
                                dRow.Item("DESCRIPCION") = pRow_1.Value(3)
                                dRow.Item("GUID") = pRow_1.Value(5)
                                dRow.Item("ORDEN") = pRow_1.Value(6)
                                lodtbTipo.Rows.Add(dRow)
                                pRow_1 = pCursor_1.NextRow
                            Loop
                        End If
                        pRow = pCursor.NextRow
                    Loop
                    Dim lodtvTipo As New DataView(lodtbTipo, Nothing, "ORDEN ASC", DataViewRowState.CurrentRows)
                    lodtbTipo = New DataTable
                    lodtbTipo.Columns.Add("CODIGO", Type.GetType("System.Double"))
                    lodtbTipo.Columns.Add("DESCRIPCION", Type.GetType("System.String"))
                    lodtbTipo.Columns.Add("GUID", Type.GetType("System.String"))
                    lodtbTipo.Columns.Add("ORDEN", Type.GetType("System.Double"))
                    For i As Integer = 0 To lodtvTipo.Count - 1
                        dRow = lodtbTipo.NewRow
                        dRow.Item("CODIGO") = lodtvTipo.Item(i).Row(0)
                        dRow.Item("DESCRIPCION") = lodtvTipo.Item(i).Row(1)
                        dRow.Item("GUID") = lodtvTipo.Item(i).Row(2)
                        dRow.Item("ORDEN") = lodtvTipo.Item(i).Row(3)
                        lodtbTipo.Rows.Add(dRow)
                    Next
                    'MsgBox("2")
                Case "TABLA_ACCESO"
                    lodtbTipo.Columns.Add("PERFIL", Type.GetType("System.Double"))
                    Do Until pRow Is Nothing
                        dRow = lodtbTipo.NewRow
                        dRow.Item("PERFIL") = pRow.Value(7)
                        lodtbTipo.Rows.Add(dRow)
                        pRow = pCursor.NextRow
                    Loop
                Case "TABLA_OPCIONES"
                    lodtbTipo.Columns.Add("CODIGO", Type.GetType("System.String"))
                    lodtbTipo.Columns.Add("DESCRIPCION", Type.GetType("System.String"))
                    dRow = lodtbTipo.NewRow
                    dRow.Item("CODIGO") = ""
                    dRow.Item("DESCRIPCION") = "-- Seleccione una Opción --"
                    lodtbTipo.Rows.Add(dRow)
                    Do Until pRow Is Nothing
                        dRow = lodtbTipo.NewRow
                        dRow.Item("CODIGO") = pRow.Value(2)
                        dRow.Item("DESCRIPCION") = pRow.Value(3)
                        lodtbTipo.Rows.Add(dRow)
                        pRow = pCursor.NextRow
                    Loop
            End Select
            Return lodtbTipo
        Catch ex As Exception
            Return Nothing
            MsgBox(".::No Puedo leer Tabla Acceso::.", vbInformation, "GEOCATMIN")
        End Try
    End Function
    Public Sub Redondeovertices_featureclass()

        'Programa para redondear los vertices de un featureclass
        Try
            Dim coordenada_DM(300) As Punto_DM
            Dim h, j As Integer
            Dim ptcol As IPointCollection
            Dim pt As IPoint
            Dim l As IPolygon
            l = pfeature_eval.Shape
            Dim v_este1 As Double
            Dim v_norte1 As Double

            ptcol = l
            ReDim coordenada_DM(ptcol.PointCount)
            For j = 0 To ptcol.PointCount - 2
                pt = ptcol.Point(j)
                v_norte1 = Format(Math.Round(pt.Y, 3), "###,###.00")
                v_este1 = Format(Math.Round(pt.X, 3), "###,###.00")
                coordenada_DM(j).v = j + 1
                coordenada_DM(j).x = Format(Math.Round(pt.X, 3), "###,###.00") ' pt.X
                coordenada_DM(j).y = Format(Math.Round(pt.Y, 3), "###,###.00") 'pt.Y
            Next j

            'Calcular Area
            coordenada_DM(j).x = coordenada_DM(0).x
            coordenada_DM(j).y = coordenada_DM(0).y
            Dim d0, d1, dr As Double
            d0 = d1 = dr = 0
            For h = 0 To j  ' UBound(coordenada_DM) - 1
                If coordenada_DM(h).x <> 0 Or coordenada_DM(h).y <> 0 Then
                    'd0 = d0 + Math.Round(coordenada_DM(h).x, 3) * Math.Round(coordenada_DM(h + 1).y, 3)
                    'd1 = d1 + Math.Round(coordenada_DM(h).y, 3) * Math.Round(coordenada_DM(h + 1).x, 3)
                    d0 = d0 + coordenada_DM(h).x * coordenada_DM(h + 1).y
                    d1 = d1 + coordenada_DM(h).y * coordenada_DM(h + 1).x

                Else
                    Exit For
                End If
            Next h
            dr = Math.Round(Math.Abs((d0 - d1) / 2) / 10000, 4)  'termino
            v_area_eval = Format(Math.Round(dr, 4), "###,###.0000")
            'MsgBox(area_eval)
        Catch ex As Exception
            MsgBox("ERROR --EN CALCULO DE AREA", vbInformation, "OBSERVACION...")
        End Try
    End Sub
    Public Sub Consulta_Segun_CartaIGN(ByVal pApp As IApplication)
        Dim cls_eval As New Cls_evaluacion
        Dim cls_planos As New Cls_planos
        Dim cls_catastro As New cls_DM_1
        Dim nm_depa As String = ""
        Dim nm_prov As String = ""
        Dim nm_dist As String = ""
        lista_cd_cartas = ""
        lista_cartas = ""
        lista_nmhojas = ""
        Dim loStrShapefile As String = "Catastro" & DateTime.Now.Ticks.ToString()
        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & v_zona_dm, m_application, "1", False)
        ' cls_catastro.Conexion_SDE(m_application)
        Try
            Kill(glo_pathTMP & "\DM_" & v_codigo & "*.*")
        Catch ex As Exception
        End Try
        cls_eval.consultacapaDM(v_codigo, "DM", "Catastro")
        arch_cata = "Cata"
        cls_eval.DefinitionExpressiontema(v_codigo, pApp, "Catastro")
        If v_existe = False Then
            cls_eval.cierra_ejecutable()
            MsgBox("Derecho Minero no se Visualiza ....", MsgBoxStyle.Information, "[BDGeocatmin]")

            Exit Sub
        End If
        cls_eval.obtienelimitesmaximos("Catastro")
        If caso_consulta = "CARTA IGN" Then
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Carta, m_application, "1", False)
            Dim lo_Filtro_Dpto2 As String = cls_eval.f_Intercepta_temas("Cuadrangulo", v_este_min, v_norte_min, v_este_max, v_norte_max, m_application, loStrShapefile)
            lista_hojas = lo_Filtro_Dpto2
        End If
        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "1", False)
        Dim lo_Filtro_Dpto1 As String = cls_eval.f_Intercepta_temas("Departamento", v_este_min, v_norte_min, v_este_max, v_norte_max, m_application, loStrShapefile)
        lista_nm_depa = lo_Filtro_Dpto1
        If colecciones_depa.Count = 0 Then lista_depa = ""
        For contador As Integer = 1 To colecciones_depa.Count
            nm_depa = colecciones_depa.Item(contador)
            If contador = 1 Then
                lista_depa = nm_depa
            ElseIf contador > 1 Then
                lista_depa = lista_depa & "," & nm_depa
            End If
        Next contador
        colecciones_depa.Clear()
        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito, m_application, "1", False)
        cls_eval.f_Intercepta_temas("Distrito", v_este_min, v_norte_min, v_este_max, v_norte_max, m_application, loStrShapefile)
        'Dim nm_dist As String = ""
        If colecciones_dist.Count = 0 Then
            lista_dist = ""
        End If
        For contador As Integer = 1 To colecciones_dist.Count
            nm_dist = colecciones_dist.Item(contador)
            If contador = 1 Then
                lista_dist = nm_dist
            ElseIf contador > 1 Then
                lista_dist = lista_dist & "," & nm_dist
            End If
        Next contador
        colecciones_dist.Clear()
        'Obtiene lista de provincias x dm
        cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Provincia, m_application, "1", False)
        cls_eval.f_Intercepta_temas("Provincia", v_este_min, v_norte_min, v_este_max, v_norte_max, m_application, loStrShapefile)
        'Dim nm_prov As String = ""
        If colecciones_prov.Count = 0 Then
            lista_prov = ""
        End If
        For contador As Integer = 1 To colecciones_prov.Count
            nm_prov = colecciones_prov.Item(contador)
            If contador = 1 Then
                lista_prov = nm_prov
            ElseIf contador > 1 Then
                lista_prov = lista_prov & "," & nm_prov
            End If
        Next contador
        colecciones_prov.Clear()
        cls_catastro.Quitar_Layer("Catastro", m_application)
        cls_catastro.Quitar_Layer("Departamento", m_application)
        cls_catastro.Quitar_Layer("Provincia", m_application)
        cls_catastro.Quitar_Layer("Distrito", m_application)
        cls_catastro.Quitar_Layer("Cuadrangulo", m_application)
        'Dim pActiveView As IActiveView

        If caso_consulta = "CARTA IGN" Then
            Dim cont_v As Integer
            Dim CARTA_V1 As String
            Dim con_lista As Integer
            con_lista = colecciones.Count
            For cont_v = 1 To con_lista
                CARTA_V1 = colecciones.Item(cont_v)
                carta_v = CARTA_V1.Replace("-", "")
                If cont_v = 1 Then
                    lista_cartas = "CD_HOJA =  '" & CARTA_V1 & "'"
                    lista_cd_cartas = CARTA_V1
                Else
                    lista_cartas = lista_cartas & " OR " & "CD_HOJA =  '" & CARTA_V1 & "'"
                    lista_cd_cartas = lista_cd_cartas & "," & CARTA_V1
                End If

                'carta_v = CARTA_V1.Replace("-", "")

                'cls_catastro.AddImagen(glo_pathIGN & carta_v & ".ECW", "1", carta_v, pApp, True)
                Dim filtro_carta As String = "NAME = '" & LCase(carta_v.Replace("-", "")) & ".ecw'"
                'Dim filtro_carta As String = "NAME = '" & carta_v & ".ecw'"
                Conexion_SDE(pApp)
                'pActiveView = pMap
                pRasterCatalog = GetRasterCatalog(pApp, "DATA_GIS.DS_IGN_CARTA_NACIONAL_GEOPSA56")
                AddRasterCatalogLayer(pApp, pRasterCatalog)
                MyDefinitionQuery("DATA_GIS.DS_IGN_CARTA_NACIONAL_GEOPSA56", pApp, carta_v, filtro_carta, "1")
                pFSelQuery.Clear()
                cls_catastro.Quitar_Layer("DATA_GIS.DS_IGN_CARTA_NACIONAL_GEOPSA56", pApp)

            Next cont_v
            colecciones.Clear()
            Dim nmhojas1 As String
            For cont_v = 1 To con_lista
                nmhojas1 = colecciones_nmhojas.Item(cont_v)
                If cont_v = 1 Then
                    lista_nmhojas = nmhojas1
                Else
                    lista_nmhojas = lista_nmhojas & "," & nmhojas1
                End If
            Next cont_v
            colecciones_nmhojas.Clear()
            loStrShapefile = "DM_" & v_codigo
            cls_catastro.Add_ShapeFile(loStrShapefile, m_application)
            arch_cata = ""
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, pApp, "1", True)
            cls_eval.DefinitionExpressiontema(lista_nm_depa, pApp, "Departamento")
            cls_catastro.Shade_Poligono("Departamento", pApp)

            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Provincia, pApp, "1", True)
            cls_eval.DefinitionExpressiontema(lista_nm_depa, pApp, "Provincia")
            cls_catastro.Shade_Poligono("Provincia", pApp)

            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito, pApp, "1", True)
            cls_eval.DefinitionExpressiontema(lista_nm_depa, pApp, "Distrito")
            cls_catastro.Shade_Poligono("Distrito", pApp)

            cls_catastro.Quitar_Layer("Catastro", m_application)
            loStrShapefile = "DM_" & v_codigo
            cls_catastro.Add_ShapeFile(loStrShapefile, m_application)
            cls_catastro.Zoom_to_Layer("Catastro")
            arch_cata = "Catastro"
            cls_catastro.Color_Poligono_Simple(m_application, "Catastro")
            cls_catastro.ShowLabel_DM("Distrito", pApp)
            pMxDoc.UpdateContents()
            'pMap.MapScale = 100000
            cls_eval.asigna_escaladataframe("CARTA IGN")
            escala_plano_carta = pMap.MapScale
        ElseIf caso_consulta = "DEMARCACION POLITICA" Then
            arch_cata = ""
            loStrShapefile = "DM_" & v_codigo
            cls_catastro.Add_ShapeFile(loStrShapefile, m_application)
            cls_eval.obtienelimitesmaximos("Catastro")
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "1", True)
            cls_eval.DefinitionExpressiontema(lista_nm_depa, pApp, "Departamento")
            cls_catastro.Shade_Poligono("Departamento", pApp)
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito, m_application, "1", False)
            cls_eval.f_Intercepta_temas("Distrito", v_este_min, v_norte_min, v_este_max, v_norte_max, m_application, loStrShapefile)
            'Dim nm_dist As String = ""
            If colecciones_dist.Count = 0 Then
                lista_dist = ""
            End If
            For contador As Integer = 1 To colecciones_dist.Count
                nm_dist = colecciones_dist.Item(contador)
                If contador = 1 Then
                    lista_dist = nm_dist
                ElseIf contador > 1 Then
                    lista_dist = lista_dist & "," & nm_dist
                End If
            Next contador
            colecciones_dist.Clear()
            'Obtiene lista de provincias x dm
            cls_catastro.PT_CargarFeatureClass_SDE(gstrFC_Provincia, m_application, "1", False)
            cls_eval.f_Intercepta_temas("Provincia", v_este_min, v_norte_min, v_este_max, v_norte_max, m_application, loStrShapefile)
            If colecciones_prov.Count = 0 Then
                lista_prov = ""
            End If
            For contador As Integer = 1 To colecciones_prov.Count
                nm_prov = colecciones_prov.Item(contador)
                If contador = 1 Then
                    lista_prov = nm_prov
                ElseIf contador > 1 Then
                    lista_prov = lista_prov & "," & nm_prov
                End If
            Next contador
            colecciones_prov.Clear()
            cls_eval.DefinitionExpressiontema(lista_nm_depa, pApp, "Provincia")
            cls_catastro.Shade_Poligono("Provincia", pApp)
            cls_eval.DefinitionExpressiontema(lista_nm_depa, pApp, "Distrito")
            cls_catastro.Shade_Poligono("Distrito", pApp)
            cls_catastro.ShowLabel_DM("Provincia", pApp)
            cls_catastro.ShowLabel_DM("Departamento", pApp)
            cls_catastro.Zoom_to_Layer("Catastro")
            cls_catastro.Color_Poligono_Simple(m_application, "Catastro")
            cls_eval.asigna_escaladataframe("DEMARCACION POLITICA")
            escala_plano_dema = pMap.MapScale
        End If
    End Sub
    Public Sub Obtiene_Limite_Maximo(ByVal player As String, ByVal pApp As IApplication)
        Try
            Dim pFeatureCursor As IFeatureCursor
            Dim pfeature As IFeature
            Dim pgeometria As IGeometry
            Dim pEnvelope As IEnvelope
            Dim pActiveView As IActiveView
            'Dim pFeatLayer As IFeatureLayer = Nothing
            pMxDoc = pApp.Document
            pMap = pMxDoc.FocusMap
            pActiveView = pMap
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = player Then
                    'pFeatLayer = pMxDoc.FocusMap.Layer(A)
                    pFeatureLayer = pMap.Layer(A)
                    afound = True : Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("Layer No Existe.") : Exit Sub
            End If
            pFeatureCursor = pFeatureLayer.Search(Nothing, False)
            '**********
            Dim pFSel As IFeatureSelection
            pFSel = pFeatureLayer
            'If pFSel.SelectionSet.Count = 0 Then
            '    MsgBox("Actualizar la GeoDataBase, No encuentro el DM.", MsgBoxStyle.Information, "[BDGeocatmin]")
            '    Exit Sub
            'End If
            '**********
            pfeature = pFeatureCursor.NextFeature
            'Obteniendo datos de los linites maximo del dm
            pgeometria = pfeature.Shape
            pEnvelope = pgeometria.Envelope
            glo_xMin = pEnvelope.XMin : glo_yMin = pEnvelope.YMin
            glo_xMax = pEnvelope.XMax : glo_yMax = pEnvelope.YMax
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Function Check_FeatureClass(ByVal p_Layer As String, ByVal p_App As IApplication) As String
        Dim pCursor As ICursor
        pMxDoc = p_App.Document
        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = p_Layer Then
                pFeatureLayer = pMap.Layer(A)
                aFound = True
                Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("No existe capa de Ubigeo")
            Return ""
            Exit Function
        End If
        pTable = pFeatureLayer.FeatureClass
        Dim pQueryDef As IQueryDef
        Dim pDataset As IDataset = pTable
        'pDataset = pTable
        pFeatureWorkspace = pDataset.Workspace
        pQueryDef = pFeatureWorkspace.CreateQueryDef
        With pQueryDef
            .Tables = pDataset.Name
            '.WhereClause = p_Filtro.ToUpper
            pCursor = .Evaluate
        End With
        Return pTable.RowCount(Nothing)
        'Return lodtRegistros
    End Function
    Public Sub Ver_Google_Earth(ByVal p_xMin As Double, ByVal p_yMin As Double, ByVal p_xMax As Double, ByVal p_yMax As Double, ByVal p_Zona As String)
        Dim WGS84_i As IPoint = New Point
        Dim WGS84_f As IPoint = New Point
        WGS84_i = PSAD56_WGS84(p_xMin, p_yMin, p_Zona)
        WGS84_f = PSAD56_WGS84(p_xMax, p_yMax, p_Zona)
        Dim nombrefile As String = glo_pathTMP & "\" & DateTime.Now.Ticks.ToString() + ".kml"
        'Definimos el archivo XML
        Dim writer As New XmlTextWriter(nombrefile, Encoding.UTF8)
        'Empezamos a escribir
        writer.WriteStartDocument()
        writer.WriteStartElement("kml")
        writer.WriteAttributeString("xmlns", "http://www.opengis.net/kml/2.2")
        writer.WriteAttributeString("xmlns:gx", "http://www.google.com/kml/ext/2.2")
        writer.WriteAttributeString("xmlns:kml", "http://www.opengis.net/kml/2.2")
        writer.WriteAttributeString("xmlns:atom", "http://www.w3.org/2005/Atom")
        writer.WriteStartElement("GroundOverlay")
        writer.WriteElementString("name", "INGEMMET-Geocatmin")
        writer.WriteStartElement("Icon")
        'writer.WriteElementString("href", "http://metadatos.ingemmet.gob.pe/cgi-bin/mapserv.exe?map=C:/ms4w/data/mapfiles/_wmscatastro.map&VERSION=1.1.1&REQUEST=GetMap&SRS=EPSG:4326&WIDTH=512&HEIGHT=512&LAYERS=metadatos,catageo.shp&STYLES=default,default&TRANSPARENT=TRUE&FORMAT=image/png&")
        'writer.WriteElementString("href", "http://geocatmin.ingemmet.gob.pe/arcgis/services/SERV_CATASTRO/MapServer/WMSServer?")
        writer.WriteElementString("href", "http://srvgeocatmin/ArcGIS/services/SERV_CATASTRO/MapServer/WMSServer?request=GetCapabilities&service=WMS")

        'http://geocatmin.ingemmet.gob.pe/descargas/kmz/wms_catastro.kmz
        writer.WriteElementString("viewRefreshMode", "onStop")
        writer.WriteElementString("viewBoundScale", 0.75)
        writer.WriteEndElement()
        writer.WriteStartElement("LatLonBox")
        writer.WriteElementString("north", 0) '-9
        writer.WriteElementString("south", -20) '-9.25
        writer.WriteElementString("east", -68) '-77
        writer.WriteElementString("west", -82) '-77.25
        'writer.WriteElementString("north", WGS84_f.Y) '-9
        'writer.WriteElementString("south", WGS84_i.Y) '-9.25
        'writer.WriteElementString("east", WGS84_f.X) '-77
        'writer.WriteElementString("west", WGS84_i.X) '-77.25
        writer.WriteEndElement()
        writer.WriteEndElement()
        writer.WriteEndDocument()
        writer.Close()
        Process.Start(nombrefile)
    End Sub
    Private Function PSAD56_WGS84(ByRef pointX As Integer, ByRef pointY As Integer, ByVal p_Zona As Integer)
        Dim WGS84Point As IPoint = New Point
        Dim SpatRefFact As ISpatialReferenceFactory
        Dim PSAD56_UTM As ISpatialReference = Nothing
        Dim G_WGS84 As ISpatialReference = Nothing
        Dim TestStr As String
        SpatRefFact = New SpatialReferenceEnvironment()
        Select Case p_Zona
            Case 17
                PSAD56_UTM = SpatRefFact.CreateProjectedCoordinateSystem(24877) '24878  24879
            Case 18
                PSAD56_UTM = SpatRefFact.CreateProjectedCoordinateSystem(24878) '24878  24879
            Case 19
                PSAD56_UTM = SpatRefFact.CreateProjectedCoordinateSystem(24879) '24878  24879
        End Select
        G_WGS84 = SpatRefFact.CreateGeographicCoordinateSystem(4326)
        WGS84Point.PutCoords(pointX, pointY)
        WGS84Point.SpatialReference = PSAD56_UTM
        WGS84Point.Project(G_WGS84)
        TestStr = Format(WGS84Point.X, "###0.00000") + "," + Format(WGS84Point.Y, "###0.00000") + "  " + Format(pointX, "###0.00000") + "," + Format(pointY, "###0.00000")
        'MsgBox(TestStr)
        PSAD56_WGS84 = WGS84Point
    End Function
    Public Sub Ordenacapa_vista(ByVal p_Layer As String)
        'PROGRAMA PARA ORDENAR LAS CAPAS EN LA VISTA SEGUN ALIAS
        Try
            pMap = pMxDoc.FocusMap
            Dim pMapLayers As IMapLayers
            Dim capaordenada As ILayer
            Dim i, j, indiceOrdenada As Integer
            pMapLayers = pMap
            'BARRE LAS CAPAS
            For i = 0 To pMapLayers.LayerCount - 2
                capaordenada = pMapLayers.Layer(i)
                'capaordenada = p_Layer
                indiceOrdenada = i  'OBTIENE 1ER INDICADOR
                For j = i + 1 To pMapLayers.LayerCount - 1
                    If (pMapLayers.Layer(j).Name < capaordenada.Name) Then
                        capaordenada = pMapLayers.Layer(j)  'VALIDA
                        indiceOrdenada = j
                    End If
                Next
                pMapLayers.MoveLayer(pMapLayers.Layer(i), indiceOrdenada)  'MUEVE SEGUN EL ORDEN
                pMapLayers.MoveLayer(capaordenada, i)
            Next
        Catch ex As Exception
            MsgBox("No se ordeno la capa Catastro", MsgBoxStyle.Information, "[BDGEOCATMIN]")
        End Try
    End Sub

    Public Sub DE_FINAL(ByVal p_app As IApplication)
        pMxDoc = p_app.Document
        pMap = pMxDoc.FocusMap
        Dim pFLayer As IFeatureLayer
        pFLayer = pMap.Layer(0)
        ' Set the definition query of the layer 'Establecer la consulta de definición de la capa de 
        Dim pFLDef As IFeatureLayerDefinition
        pFLDef = pFLayer ' QI from the FeatureLayer PFLDef Set P = pFLayer 'de la FeatureLayer 
        pFLDef.DefinitionExpression = "NM_DEPA = 'CAJAMARCA'" ' set the definition expression pFLDef.DefinitionExpression = "SUB_REGION = 'Pacífico'" 'Establecer la expresión de definición 
        pMxDoc.ActiveView.Refresh() ' refresh the map pMxDoc.ActiveView.Refresh "actualizar el mapa 

        ' Make a selection on the Layer "Hacer una selección en la capa de 

        Dim pQueryFilter As IQueryFilter
        pQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = "NM_DEPA = 'CAJAMARCA'" ' set the query filter pQueryFilter.WhereClause = "Pop2000> 5000000" 'Establecer el filtro de consulta 
        Dim pFSel As IFeatureSelection
        pFSel = pFLayer
        pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFSel.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False) ' apply the selection pFSel.SelectFeatures pQueryFilter, esriSelectionResultNew, False 'aplicar la selección 
        pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing) ' refresh the selection esriViewGeoSelection pMxDoc.ActiveView.PartialRefresh, Nada, Nada actualiza 'la selección 
        ' Create a new selection layer using the current selection 'Crear una capa nueva con la selección de la selección actual 
        Dim pSelFLayer As IFeatureLayer
        pSelFLayer = pFLDef.CreateSelectionLayer("Selection_Layer", True, vbNullString, vbNullString)
        pFSel.Clear() ' clear selection on original layer claros de selección pFSel.Clear 'en la capa original 
        pMap.AddLayer(pSelFLayer) ' add new layer to the map pSelFLayer pMap.AddLayer 'Agregar nueva capa en el mapa 
    End Sub
    'Public Sub Add_ShapeFile_bdgis(ByVal p_Shapefile As String, ByVal p_App As IApplication)
    '    Dim pWorkspaceFactory As IWorkspaceFactory
    '    pWorkspaceFactory = New ShapefileWorkspaceFactory
    '    Dim pWorkSpace As IFeatureWorkspace
    '    pWorkSpace = pWorkspaceFactory.OpenFromFile(glo_pathGeoCatMin & "datos\shape\prueba\", 0)
    '    Dim pClass As IFeatureClass
    '    pClass = pWorkSpace.OpenFeatureClass(p_Shapefile)
    '    Dim pLayer As IFeatureLayer
    '    pLayer = New FeatureLayer
    '    pLayer.FeatureClass = pClass
    '    pLayer.Name = pClass.AliasName
    '    'pMxDoc = p_App.Document
    '    pMxDoc.FocusMap.AddLayer(pLayer)
    '    pLayer.Name = "Catastro"
    '    pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, Nothing)
    '    pMxDoc.UpdateContents()
    '    pFeatureLayer_cat = pLayer
    'End Sub
    Public Sub Add_ShapeFile_bdgis(ByVal p_Shapefile As String, ByVal p_App As IApplication)
        Dim pWorkspaceFactory As IWorkspaceFactory
        pWorkspaceFactory = New ShapefileWorkspaceFactory
        Dim pWorkSpace As IFeatureWorkspace
        pWorkSpace = pWorkspaceFactory.OpenFromFile(glo_pathServidor & "datos\shape\prueba\", 0)
        Dim pClass As IFeatureClass
        pClass = pWorkSpace.OpenFeatureClass(p_Shapefile)
        Dim pLayer As IFeatureLayer
        pLayer = New FeatureLayer
        pLayer.FeatureClass = pClass
        pLayer.Name = pClass.AliasName
        pFeatureLayer_cat = pLayer
        'pMxDoc = p_App.Document
        'pMxDoc.FocusMap.AddLayer(pLayer)
        'pLayer.Name = "Catastro"
        pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, Nothing)
        pMxDoc.UpdateContents()
    End Sub
    Public Sub rotulatexto_dm(ByVal capa As String, ByVal p_app As IApplication)
        'PROGRAMA PARA ROTULAR TEXTOS DE DM EN LA VISTA
        Dim pMxDoc As IMxDocument
        pMxDoc = p_app.Document
        Dim pmap As IMap
        Dim pGraphicsContainer As IGraphicsContainer
        Dim pElement As IElement
        Dim pActiveView As IActiveView
        Dim pArea As IArea
        Dim pFeatureCursor As IFeatureCursor
        pmap = pMxDoc.FocusMap
        Dim pLayer As IFeatureLayer
        'Dim pLayer As ILayer = Nothing
        Dim afound As Boolean = False
        For A As Integer = 0 To pmap.LayerCount - 1
            If pmap.Layer(A).Name = capa Then
                pLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("Layer No Existe.")
            Exit Sub
        End If
        Dim fclas_tema As IFeatureClass
        fclas_tema = pLayer.FeatureClass
        'pLayer.ShowTips = True
        pActiveView = pmap
        pGraphicsContainer = pmap.BasicGraphicsLayer

        'BUSCANDO LOS CAMPOS
        Dim campo1 As String
        Dim campo2 As String

        If capa = "Catastro" Then

            pFields = fclas_tema.Fields
            campo1 = pFields.FindField("LEYENDA")
            pFields = fclas_tema.Fields
            campo2 = pFields.FindField("CONTADOR")
            pFields = fclas_tema.Fields

        ElseIf capa = "Cuadriculas" Then
            pFields = fclas_tema.Fields
            campo1 = pFields.FindField("CODIGOU")
            pFields = fclas_tema.Fields
            campo2 = pFields.FindField("CODIGOU")
            pFields = fclas_tema.Fields
        ElseIf capa = "Zona Reservada" Then
            pFields = fclas_tema.Fields
            'campo1 = pFields.FindField("CODIGOU")
            'pFields = fclas_tema.Fields
            campo2 = pFields.FindField("NM_RESE")
            pFields = fclas_tema.Fields
        ElseIf capa = "Zona Urbana" Then
            pFields = fclas_tema.Fields

            campo2 = pFields.FindField("NM_URBA")
            pFields = fclas_tema.Fields
        ElseIf capa = "Distrito" Then
            pFields = fclas_tema.Fields
            campo2 = pFields.FindField("NM_DIST")
            pFields = fclas_tema.Fields
        ElseIf capa = "Provincia" Then
            pFields = fclas_tema.Fields
            campo2 = pFields.FindField("NM_PROV")
            pFields = fclas_tema.Fields
        ElseIf capa = "Departamento" Then
            pFields = fclas_tema.Fields
            campo2 = pFields.FindField("NM_DEPA")
            pFields = fclas_tema.Fields
        ElseIf capa = loStrShapefile_ld Then
            pFields = fclas_tema.Fields
            campo1 = pFields.FindField("LEYENDA")
            pFields = fclas_tema.Fields
            campo2 = pFields.FindField("CONTADOR")
            pFields = fclas_tema.Fields

        End If



        pFeatureCursor = pLayer.Search(Nothing, False)
        fclas_tema = pLayer.FeatureClass
        Dim cont_dato As Integer = 0
        Dim contador As String
        Dim nombre_dm_x1 As String

        pFeature = pFeatureCursor.NextFeature

        'OBTENIENDO VALORES DE LOS DM Y CONTADOR

        Do Until pFeature Is Nothing
            pArea = pFeature.Shape
            If capa = "Cuadriculas" Then
                nombre_dm_x = ""
                cont_dato = cont_dato + 1
                nombre_dm_x1 = pFeature.Value(campo1)
                If cont_dato = 1 Then
                    nombre_datos = nombre_dm_x1
                Else
                    nombre_datos = nombre_datos & "," & nombre_dm_x1
                End If
                'nombre_datos = Left(nombre_datos, (Len(nombre_datos) - 1))
            ElseIf capa = "Catastro" Then
                nombre_datos = "XXXXXXX"
                nombre_dm_x = pFeature.Value(campo1)
            ElseIf capa = "Zona Reservada" Then
                nombre_datos = "XXXXXXX"
                nombre_dm_x = "rese"
            ElseIf capa = "Distrito" Then
                nombre_datos = "XXXXXXX"
                nombre_dm_x = "Dist"
            ElseIf capa = "Provincia" Then
                nombre_datos = "XXXXXXX"
                nombre_dm_x = "Prov"
            ElseIf capa = "Departamento" Then
                nombre_datos = "XXXXXXX"
                nombre_dm_x = "Depa"
            ElseIf capa = "Zona Urbana" Then
                nombre_datos = "XXXXXXX"
                nombre_dm_x = "Urba"
            End If

            contador = pFeature.Value(campo2)
            pElement = MakeATextElement(pArea.Centroid, contador)  ' APLICA FUNCION DE CREAR TEXTO

            '   AGREGA EL ELEMENTO TEXTO AL MAPA
            pGraphicsContainer.AddElement(pElement, 0)
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)   'refresh
            pFeature = pFeatureCursor.NextFeature
        Loop
        ' nombre_datos = Left(nombre_datos, (Len(nombre_datos) - 1))
    End Sub

    Public Function MakeATextElement(ByVal pPoint As IPoint, ByVal texto As String) As IElement

        'DECLARA VARIABLES DEL SIMBOLO
        Dim pRgbColor As IRgbColor
        Dim pTextElement As ITextElement
        Dim pTextSymbol As ITextSymbol
        Dim fnt As IFontDisp
        Dim pElement As IElement

        'CLASIFICA SEGUN TIPO DE DM
        pRgbColor = New RgbColor
        If nombre_dm_x = "G3" Then
            pRgbColor.RGB = RGB(0, 0, 255)
        ElseIf nombre_dm_x = "G1" Then
            pRgbColor.RGB = RGB(0, 255, 0)
        ElseIf nombre_dm_x = "G2" Then
            pRgbColor.RGB = RGB(255, 0, 0)
        ElseIf nombre_dm_x = "G4" Then
            pRgbColor.RGB = RGB(0, 0, 0)
        ElseIf nombre_dm_x = "G5" Then
            pRgbColor.RGB = RGB(78, 0, 0)
        ElseIf nombre_dm_x = "G6" Then
            pRgbColor.RGB = RGB(205, 0, 237)
        ElseIf nombre_dm_x = "rese" Then
            pRgbColor.RGB = RGB(255, 0, 0)
        ElseIf nombre_dm_x = "Dist" Then
            pRgbColor.RGB = RGB(255, 0, 0)
        ElseIf nombre_dm_x = "Prov" Then
            pRgbColor.RGB = RGB(0, 120, 0)
        ElseIf nombre_dm_x = "Depa" Then
            pRgbColor.RGB = RGB(137, 90, 68)
        ElseIf nombre_dm_x = "Urba" Then
            pRgbColor.RGB = RGB(227, 158, 0)
        Else
            pRgbColor.RGB = RGB(0, 0, 255)
        End If
        pTextElement = New TextElement
        pElement = pTextElement
        pElement.Geometry = pPoint
        'FONT DEL TEXTO
        Dim pFontDisp As IFontDisp
        pFontDisp = New stdole.StdFont
        pFontDisp.Name = "TAHOMA"
        pFontDisp.Bold = False

        'ASIGNA SIMBOLOGIA
        pTextSymbol = New TextSymbol
        pTextSymbol.Font = pFontDisp
        pTextSymbol.Color = pRgbColor
        If nombre_dm_x = "rese" Then
            pTextSymbol.Size = 5
        ElseIf nombre_dm_x = "Urba" Then
            pTextSymbol.Size = 8
        ElseIf nombre_dm_x = "Prov" Then
            pTextSymbol.Size = 15
        ElseIf nombre_dm_x = "Depa" Then
            pTextSymbol.Size = 25
        Else
            pTextSymbol.Size = 7
        End If

        pTextElement.Symbol = pTextSymbol
        pTextElement.Text = texto
        MakeATextElement = pTextElement
    End Function

    Public Sub Add_capa_catnomin(ByVal p_Shapefile As String, ByVal p_App As IApplication, ByVal fecha As String, ByVal ruta As String)
        Try
            Dim pWorkspaceFactory As IWorkspaceFactory
            pWorkspaceFactory = New ShapefileWorkspaceFactory
            Dim pWorkSpace As IFeatureWorkspace
            pWorkSpace = pWorkspaceFactory.OpenFromFile(ruta, 0)
            pInFeatureClass = pWorkSpace.OpenFeatureClass(p_Shapefile.ToUpper)
            pLayer = New FeatureLayer
            pFeatureLayeras_rese = New FeatureLayer
            pFeatureLayeras_rese.FeatureClass = pInFeatureClass
            pFeatureLayeras_rese.Name = UCase(pInFeatureClass.AliasName)
            pMxDoc.UpdateContents()
        Catch ex As Exception
            MsgBox("No existe la capa " & p_Shapefile & ",Revisar previamente su informacón", MsgBoxStyle.Critical, "Observación")
        End Try
    End Sub
    Public Sub Eliminafesture(ByVal pFeatureTable As ITable, ByVal pQueryFilter As IQueryFilter)

        Try
            pWorkspaceEdit = pFeatureWorkspace
            pWorkspaceEdit.StartEditing(True)
            pWorkspaceEdit.StartEditOperation()
            pFeatureTable.DeleteSearchedRows(pQueryFilter)
            pWorkspaceEdit.StopEditOperation()
            pWorkspaceEdit.StopEditing(True)
        Catch ex As Exception
            MsgBox("Error al Eliminar un featureclass en el Geodatabase -- Comunicarse con OSI", MsgBoxStyle.Critical, "OBSERVACIÓN...")

        End Try

    End Sub
    Public Sub UniqueSymbols(ByVal m_Application As IApplication, ByVal loFeature As String)
        ' Part 1: Prepare a unique value renderer.
        Dim pUniqueValueRenderer As IUniqueValueRenderer
        Dim pSym1 As ILineSymbol
        Dim pSym2 As IMarkerSymbol

        ' Define the renderer.
        pUniqueValueRenderer = New UniqueValueRenderer
        pUniqueValueRenderer.FieldCount = 1
        pUniqueValueRenderer.Field(0) = "DEPARTA"
        ' Add the first symbol to the renderer.
        If loFeature = "Drenaje" Then
            pSym1 = New SimpleLineSymbol
            pSym1.Color = GetRGBColor(0, 112, 255)
            pSym1.Width = 1
        ElseIf loFeature = "Centro Poblado" Then
            pSym2 = New SimpleMarkerSymbol
            pSym2.Color = GetRGBColor(115, 76, 0)
            pSym2.Size = 4
        End If

        Dim nm_depa As String = ""
        For contador As Integer = 1 To colecciones_depa.Count
            nm_depa = colecciones_depa.Item(contador)
            If loFeature = "Drenaje" Then
                pUniqueValueRenderer.AddValue(nm_depa, "", pSym1)
            ElseIf loFeature = "Centro Poblado" Then
                pUniqueValueRenderer.AddValue(nm_depa, "", pSym2)
            End If

        Next contador

        Dim pGeoFeatureLayer As IGeoFeatureLayer
        pMxDoc = m_Application.Document 'ThisDocument
        pMap = pMxDoc.FocusMap
        pLayer = pMap.Layer(0)
        pGeoFeatureLayer = pLayer
        pGeoFeatureLayer.Renderer = pUniqueValueRenderer
        pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, Nothing)
        pMxDoc.UpdateContents()
    End Sub


    Public Sub CreaFeature(ByVal pFeatureClass As IFeatureClass, ByVal pGeometry As IGeometry)
        Try
            Dim pFeature As IFeature
            pFeature = pFeatureClass.CreateFeature
            pFeature.Shape = pGeometry
            pFeature.Store()

            ''Private Sub InsertFeature(ByVal pFeatureClass As IFeatureClass, ByVal pGeometry As IGeometry)
            'Dim pFeatureBuffer As IFeatureBuffer
            'Dim pFeatureCursor As IFeatureCursor'

            'pFeatureCursor = pFeatureClass.Insert(True)
            'pFeatureBuffer = pFeatureClass.CreateFeatureBuffer

            'pFeatureBuffer.Shape = pGeometry
            'pFeatureCursor.InsertFeature(pFeatureBuffer)
            'End Sub


        Catch ex As Exception
            'MsgBox("Error al Crear un featureclass en el Geodatabase -- Comunicarse con OSI", MsgBoxStyle.Critical, "OBSERVACIÓN...")

        End Try
    End Sub
    Public Sub Add_ShapeFile1(ByVal p_Shapefile As String, ByVal p_App As IApplication, ByVal aliastema As String, Optional ByRef loLayer As Boolean = False)
        Try
            Dim pWorkspaceFactory As IWorkspaceFactory
            pWorkspaceFactory = New ShapefileWorkspaceFactory
            Dim pWorkSpace As IFeatureWorkspace
            pWorkSpace = pWorkspaceFactory.OpenFromFile(glo_pathTMP & "\", 0)
            Dim pClass As IFeatureClass
            pClass = pWorkSpace.OpenFeatureClass(p_Shapefile)
            Dim pLayer As IFeatureLayer
            pLayer = New FeatureLayer
            pLayer.FeatureClass = pClass
            pLayer.Name = pClass.AliasName
            pMxDoc.FocusMap.AddLayer(pLayer)
            If aliastema = "codigo" Then
                pLayer.Name = "DM_" & v_codigo
                loLayer = True
            ElseIf aliastema = "Prioritarios" Then
                pLayer.Name = "Prioritarios" & v_codigo
            ElseIf aliastema = "Simu" Then
                pLayer.Name = "Catastro"
            ElseIf aliastema = "DMxregion" Then
                pLayer.Name = "DMxregion"
            ElseIf aliastema = "Antecesor" Then
                pLayer.Name = "Antecesor"
            ElseIf aliastema = "Rectangulo" Then
                pLayer.Name = "Rectangulo"
            ElseIf aliastema = "Certificacion" & fecha_archi3 Then
                pLayer.Name = "Certificacion Ambiental"
            ElseIf aliastema = "RESERVA" Then
                pLayer.Name = "Area_Reserva_" & fecha_archi
                loLayer = True
            ElseIf aliastema = "URBANA" Then
                pLayer.Name = "Area_Urbana_" & fecha_archi
                loLayer = True
            ElseIf aliastema = "Zona Reservada" Then
                pLayer.Name = "Zona Reservada"
                loLayer = True
            Else
                pLayer.Name = "Areadispo_" & v_codigo
            End If
            pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, Nothing)
            pMxDoc.UpdateContents()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub actualiza_criterioDM(ByVal p_App As IApplication, ByVal p_Codigo As String, ByVal p_prioridad As String)
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim pFeatureClass As IFeatureClass
        Dim pMxDoc As IMxDocument
        pMxDoc = p_App.Document
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
        pFeatureClass = pFeatLayer.FeatureClass
        Dim pQFilter As IQueryFilter
        Dim pUpdateFeatures As IFeatureCursor
        Dim codigo As String
        pQFilter = New QueryFilter
        pQFilter.WhereClause = query_cadena
        pUpdateFeatures = pFeatureClass.Update(pQFilter, True)
        Dim pFeature As IFeature
        Dim codigo_p As String
        Dim v_prueba As String
        Dim priori_p As String
        pFeature = pUpdateFeatures.NextFeature
        Try
            Do Until pFeature Is Nothing
                codigo = pFeature.Value(pUpdateFeatures.FindField("CODIGOU"))
                For contador As Integer = 1 To colecciones_indi.Count
                    v_prueba = colecciones_indi.Item(contador)
                    codigo_p = Left(v_prueba, (Len(v_prueba) - 2))
                    If codigo = codigo_p Then
                        priori_p = Right(v_prueba, 2)
                        pFeature.Value(pUpdateFeatures.FindField("EVAL")) = priori_p
                        pUpdateFeatures.UpdateFeature(pFeature)
                    End If
                Next contador
                pFeature = pUpdateFeatures.NextFeature
            Loop
            pMxDoc.ActiveView.Refresh()
            MsgBox("Se actualizo el criterio de evaluación en la parte grafica", MsgBoxStyle.Information, "Observación")
        Catch ex As Exception
            MsgBox("Error al Actualizar criterio evaluación", MsgBoxStyle.Information, "Observación")
        End Try
    End Sub

    Public Sub anotaciones_caparese(ByVal p_Layer As String, ByVal p_App As IApplication)

        Try
            'Programa para label a la capa de catastro Minero
            '---------------------------------------------------

            Dim pMxDoc As IMxDocument
            Dim vistaactiva As IActiveView
            Dim pdataset As IDataset
            Dim pAnnotationLayer As IAnnotationLayer
            Dim pGeoFeatureLayer As IGeoFeatureLayer
            Dim pFClass As IFeatureClass

            'Para darle font a las anotaciones

            Dim myColor As IRgbColor
            Dim myFont As IFontDisp
            Dim pAnnotateLayerPropertiesCollection As IAnnotateLayerPropertiesCollection
            Dim pMapAnnoPropsColl As IAnnotateLayerPropertiesCollection
            Dim pAnnotateLayerProperties As IAnnotateLayerProperties
            Dim pLabelEngineLayerProperties As ILabelEngineLayerProperties2
            Dim pOverposterLayerProperties As IOverposterLayerProperties2
            Dim pClone As IClone
            Dim propsIndex As Long
            Dim pCompositeGraphicsLayer As ICompositeGraphicsLayer
            Dim pRefScale As IGraphicsLayerScale
            Dim pGraphicsLayer As IGraphicsLayer
            Dim pScreenDisplay As IScreenDisplay
            Dim pAnnotateMapProps As IAnnotateMapProperties
            Dim pAnnotateMap2 As IAnnotateMap2
            Dim pTrackCancel As ITrackCancel
            Dim pMapOverposter As IMapOverposter
            Dim pOverposterProperties As IOverposterProperties
            Dim pOverflowGraphicsContainer As IOverflowGraphicsContainer

            Dim pElement As IElement
            Dim pUnplacedElements As IElementCollection
            Dim lngUnplacedIndex As Long
            Dim lngUnplacedCount As Long

            Dim pSymbolColl As ISymbolCollection
            Dim pLELayerProps As ILabelEngineLayerProperties
            Dim aAnnoVBScriptEngine As IAnnotationExpressionEngine
            pMxDoc = p_App.Document
            Dim pmap As IMap
            Dim escalaf As Double


            pmap = pMxDoc.FocusMap
            escalaf = pmap.MapScale

            Dim pLayer As IFeatureLayer
            ' nom_archivo = "ZONA RESERVADA"
            ' Call buscatema(nom_archivo)

            Dim afound As Boolean = False
            For A As Integer = 0 To pmap.LayerCount - 1
                If pmap.Layer(A).Name = p_Layer Then '"Catastro" Then
                    pLayer = pMxDoc.FocusMap.Layer(A)
                    ' pGeoFeatureL.ShowTips = True
                    afound = True
                    pGeoFeatureLayer = pLayer
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If
            'pGeoFeatureL = pMxDoc.FocusMap.Layer(0)


            'Tamaño al texto

            Dim myTxtSym As ITextSymbol
            myTxtSym = New TextSymbol
            myFont = New StdFont
            myFont.Name = "Verdana"
            myFont.Size = 19
            myTxtSym.Font = myFont
            'Color

            myColor = New RgbColor
            myColor.RGB = RGB(255, 0, 0)
            myTxtSym.Color = myColor

            'Igualando

            'pGeoFeatureLayer = pLayer
            pMapAnnoPropsColl = New AnnotateLayerPropertiesCollection 'a new properties collection which will be populated
            pAnnotateLayerPropertiesCollection = pGeoFeatureLayer.AnnotationProperties
            For propsIndex = 0 To (pAnnotateLayerPropertiesCollection.Count - 1)
                pAnnotateLayerPropertiesCollection.QueryItem(propsIndex, pAnnotateLayerProperties)
                If Not pAnnotateLayerProperties Is Nothing Then
                    pClone = pAnnotateLayerProperties
                    pMapAnnoPropsColl.Add(pClone.Clone)
                End If
            Next propsIndex

            pAnnotateLayerProperties = Nothing
            pMapOverposter = pmap
            pOverposterProperties = pMapOverposter.OverposterProperties
            'pCompositeGraphicsLayer = pMap.BasicGraphicsLayer
            'pGraphicsLayer = pCompositeGraphicsLayer.AddLayer(pLayer.Name, pGeoFeatureLayer)
            pCompositeGraphicsLayer = pmap.BasicGraphicsLayer
            pGraphicsLayer = pCompositeGraphicsLayer
            'Set pGraphicsLayer = pCompositeGraphicsLayer

            pRefScale = pGraphicsLayer

            'pRefScale.ReferenceScale = 100000
            pRefScale.ReferenceScale = escalaf
            vistaactiva = pmap
            pScreenDisplay = vistaactiva.ScreenDisplay
            pGraphicsLayer.Activate(pScreenDisplay)

            pSymbolColl = New SymbolCollection
            pSymbolColl.Symbol(0) = myTxtSym
            For propsIndex = 0 To (pMapAnnoPropsColl.Count - 1)
                pMapAnnoPropsColl.QueryItem(propsIndex, pAnnotateLayerProperties)       'Consigue propiedades dela  colleccion
                If Not pAnnotateLayerProperties Is Nothing Then
                    pAnnotateLayerProperties.FeatureLayer = pGeoFeatureLayer         'Punto del feature layer
                    pAnnotateLayerProperties.GraphicsContainer = pGraphicsLayer      'Asignando la anotacion de destion para el label
                    pAnnotateLayerProperties.AddUnplacedToGraphicsContainer = False
                    pAnnotateLayerProperties.CreateUnplacedElements = True               'creando unplaced elements
                    pAnnotateLayerProperties.DisplayAnnotation = True
                    pAnnotateLayerProperties.FeatureLinked = False
                    pAnnotateLayerProperties.LabelWhichFeatures = esriLabelWhichFeatures.esriAllFeatures       'labels/anno for the full extent.
                    pAnnotateLayerProperties.UseOutput = True
                    pLabelEngineLayerProperties = pAnnotateLayerProperties
                    pOverposterLayerProperties = pLabelEngineLayerProperties.OverposterLayerProperties
                    pOverposterLayerProperties.TagUnplaced = True
                    aAnnoVBScriptEngine = New AnnotationVBScriptEngine
                    pLELayerProps = pAnnotateLayerProperties
                    pLELayerProps.ExpressionParser = aAnnoVBScriptEngine

                    'Rotulando por su campo contador
                    pLELayerProps.Expression = "[CODIGOU]"
                    pLELayerProps.IsExpressionSimple = True
                    pLELayerProps.Offset = 0
                    pLELayerProps.SymbolID = 0
                    pLELayerProps.Symbol = myTxtSym
                End If

            Next propsIndex
            pMapAnnoPropsColl.Sort()

            pAnnotateMapProps = New AnnotateMapProperties
            pAnnotateMapProps.AnnotateLayerPropertiesCollection = pMapAnnoPropsColl
            pTrackCancel = New CancelTracker
            pAnnotateMap2 = pmap.AnnotationEngine
            pAnnotateMap2.Label(pOverposterProperties, pAnnotateMapProps, pmap, pTrackCancel)
            For propsIndex = 0 To (pMapAnnoPropsColl.Count - 1)
                pMapAnnoPropsColl.QueryItem(propsIndex, pAnnotateLayerProperties, Nothing, pUnplacedElements)
                If Not pAnnotateLayerProperties Is Nothing Then
                    lngUnplacedCount = lngUnplacedCount + pUnplacedElements.Count
                    If pUnplacedElements.Count > 0 Then
                        pOverflowGraphicsContainer = pGraphicsLayer
                        For lngUnplacedIndex = 0 To (pUnplacedElements.Count - 1)
                            pUnplacedElements.QueryItem(lngUnplacedIndex, pElement)
                            pOverflowGraphicsContainer.AddOverflowElement(pElement)
                        Next lngUnplacedIndex
                    End If
                    pAnnotateLayerProperties.FeatureLayer = Nothing
                End If
            Next propsIndex

            pGeoFeatureLayer.DisplayAnnotation = True
            vistaactiva = pmap
            vistaactiva.ContentsChanged()
            vistaactiva.Refresh()
            pMxDoc.UpdateContents()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Label_Feature(ByVal p_Layer As String, ByVal p_Campo As String, ByVal p_Font As String, ByVal p_FontSize As Double, ByVal p_ColorRed As Integer, ByVal p_ColorGreen As Integer, ByVal p_ColorBlue As Integer, ByVal pApp As IApplication)
        'Private Sub Anno(ByVal pGeoFeatlyr As IGeoFeatureLayer, ByVal field As String)
        Dim pGeoFeatureL As IGeoFeatureLayer
        Dim A As Integer
        'Dim pLineLabelP As ILineLabelPosition
        'Dim pLabelEngineLP As ILabelEngineLayerProperties
        'Dim pAnnotateLayerP As IAnnotateLayerProperties
        Dim lo_Encontre As Boolean
        lo_Encontre = False
        pMxDoc = pApp.Document
        'pMxDoc = m_application.Document
        pMap = pMxDoc.FocusMap
        For A = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = p_Layer Then
                lo_Encontre = True
                Exit For
            End If
        Next
        If lo_Encontre = False Then
            MsgBox("No Existe el Layer: " & p_Layer)
            Exit Sub
        End If

        pGeoFeatureL = pMxDoc.FocusMap.Layer(A)
        Dim pAnnoProps As IAnnotateLayerPropertiesCollection
        pAnnoProps = pGeoFeatureL.AnnotationProperties
        pAnnoProps.Clear()

        Dim pAnnoLayerProps As IAnnotateLayerProperties
        Dim pPosition As ILineLabelPosition
        Dim pPlacement As ILineLabelPlacementPriorities
        Dim pBasic As IBasicOverposterLayerProperties
        Dim pLabelEngine As ILabelEngineLayerProperties
        Dim pTextSym As ITextSymbol
        pTextSym = New TextSymbol
        Dim pFont As stdole.StdFont
        pFont = New stdole.StdFont
        pFont.Name = p_Font '"Arial Narrow"
        pFont.Size = p_FontSize '"7" 'iFontSize
        pFont.Bold = True
        pTextSym.Font = pFont
        Dim pSymColor As IRgbColor
        pSymColor = New RgbColor
        With pSymColor
            .Red = p_ColorRed
            .Green = p_ColorGreen
            .Blue = p_ColorBlue
        End With
        pTextSym.Color = pSymColor
        pPosition = New LineLabelPosition
        pPosition.Parallel = False
        pPosition.Perpendicular = True
        pPlacement = New LineLabelPlacementPriorities
        pBasic = New BasicOverposterLayerProperties
        pBasic.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolyline
        pBasic.LineLabelPlacementPriorities = pPlacement
        pBasic.LineLabelPosition = pPosition
        pLabelEngine = New LabelEngineLayerProperties
        pLabelEngine.Symbol = pTextSym
        pLabelEngine.BasicOverposterLayerProperties = pBasic
        pLabelEngine.Expression = p_Campo
        pAnnoLayerProps = pLabelEngine
        pAnnoProps.Add(pAnnoLayerProps)
        pGeoFeatureL.DisplayAnnotation = True
    End Sub
    Public Sub Genera_Linea(ByVal lodtbTabla As DataTable, ByVal lo_Zona As String, ByVal m_Application As IApplication)
        Dim directorio As IWorkspaceFactory
        Dim tema As IFeatureLayer = Nothing
        Dim fclas_tema As IFeatureClass = Nothing
        Dim carpeta As IWorkspace
        'Dim pfeature As IFeature
        Dim pPoint(10, 10) As IPoint
        Try
            pMxDoc = m_Application.Document
            pMap = pMxDoc.FocusMap
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name.ToUpper = "LINEA_" & lo_Zona Then
                    tema = pMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If
            directorio = New AccessWorkspaceFactory
            carpeta = pWorkspaceFactory.OpenFromFile(glo_PathGDB, 0)
            Dim vCodigo, vCodigoSalvado As String
            Dim pID As New UID
            Dim pEditor As IEditor
            fclas_tema = tema.FeatureClass
            pID.Value = "esriCore.Editor"
            pEditor = m_Application.FindExtensionByCLSID(pID)
            'Objeto de edicion del tema 
            Dim pdataset As IDataset
            pdataset = fclas_tema
            ' Esto asignara el mismo directorio del tema 
            carpeta = pdataset.Workspace
            pEditor.StartEditing(carpeta)
            pEditor.StartOperation()
            'Declarando la coleccion de puntos 
            Dim pPointColl As IPointCollection
            Dim pLine As ILine
            Dim pGeometryColl As IGeometryCollection 'Objeto de tipo de geometria del tema.
            Dim pSegmentColl As ISegmentCollection 'Objeto de segmento de coleccion.
            pID.Value = "esriCore.Editor"
            pEditor = m_Application.FindExtensionByCLSID(pID)
            vCodigoSalvado = lodtbTabla.Rows(2).Item("CG_CODIGO")

            For i As Integer = 0 To lodtbTabla.Rows.Count - 2
                pFeature = fclas_tema.CreateFeature
                pPointColl = New Polyline ' Definiendo el tipo de geometria 
                pGeometryColl = New Polyline 'Asignando 
                vCodigo = lodtbTabla.Rows(i).Item("CG_CODIGO")
                pPoint(i, 0) = New ESRI.ArcGIS.Geometry.Point
                pPoint(i, 1) = New ESRI.ArcGIS.Geometry.Point
                If vCodigo <> vCodigoSalvado Then
                    pPoint(i, 0).PutCoords(lodtbTabla.Rows(i).Item("CD_COREST"), lodtbTabla.Rows(i).Item("CD_CORNOR"))
                    pPoint(i, 1).PutCoords(lodtbTabla.Rows(i + 1).Item("CD_COREST"), lodtbTabla.Rows(i + 1).Item("CD_CORNOR"))
                    pSegmentColl = New ESRI.ArcGIS.Geometry.Path 'Generando segmento de linea 
                    pLine = New Line
                    pLine.PutCoords(pPoint(i, 0), pPoint(i, 1))
                    pSegmentColl.AddSegment(pLine)
                    'Añadiendo la linea 
                    pGeometryColl.AddGeometry(pSegmentColl)
                    pFeature.Shape = pGeometryColl
                    'pFeature.Store()
                Else
                    pPoint(i, 0).PutCoords(lodtbTabla.Rows(i).Item("CD_COREST"), lodtbTabla.Rows(i).Item("CD_CORNOR"))
                    pPoint(i, 1).PutCoords(lodtbTabla.Rows(i + 1).Item("CD_COREST"), lodtbTabla.Rows(i + 1).Item("CD_CORNOR"))
                    pSegmentColl = New ESRI.ArcGIS.Geometry.Path 'Generando segmento de linea 
                    pLine = New Line
                    pLine.PutCoords(pPoint(i, 0), pPoint(i, 1))
                    pSegmentColl.AddSegment(pLine)
                    'Añadiendo la linea 
                    pGeometryColl.AddGeometry(pSegmentColl)
                    pFeature.Shape = pGeometryColl
                End If

            Next i
            pFeature.Store()
            pEditor.StopOperation("Add Line")
            pEditor.StopEditing(True)
            pMxDoc.ActivatedView.Refresh()
        Catch ex As Exception
            MsgBox("Generación de Línea falló", MsgBoxStyle.Information, "GisCatMin")
        End Try
    End Sub
    Public Function GetRasterCatalog(ByVal pApp As IApplication, ByVal rasterCatalogName As String) As IRasterCatalog
        PrasterWorkspaceEx = DirectCast(pWorkspaceFactory.Open(pPropset, 0), IRasterWorkspaceEx)
        pRasterCatalog = PrasterWorkspaceEx.OpenRasterCatalog(rasterCatalogName)
        Return pRasterCatalog
    End Function

    Public Shared Sub AddRasterCatalogLayer(ByVal pApp As IApplication, ByVal rasterCatalog As ESRI.ArcGIS.Geodatabase.IRasterCatalog)
        rastercatalogLayer = New GdbRasterCatalogLayerClass
        pMap = pMxDoc.FocusMap
        'Dim map As ESRI.ArcGIS.Carto.IMap = activeView.FocusMap
        rastercatalogLayer.Setup(CType(rasterCatalog, ITable))
        pMap.AddLayer(CType(rastercatalogLayer, ILayer))
    End Sub
    Public Sub MyDefinitionQuery(ByVal nombrerasters As String, ByVal p_app As IApplication, ByVal v_Carta As String, ByVal Query_cartas As String, Optional ByVal variable As String = "") ', ByVal v_cantCarta As Integer)
        Dim pFLayer As IFeatureLayer
        Dim afound As Boolean = False
        Dim pFLDef As IFeatureLayerDefinition
        Dim pQueryFilter As IQueryFilter
        pMxDoc = p_app.Document
        pMap = pMxDoc.FocusMap

        For A As Integer = 0 To pMap.LayerCount - 1
            'If pMap.Layer(A).Name = "DATA_GIS.DS_IGN_CARTA_NACIONAL_GEOPSA56" Then ' v_Tema Then
            If pMap.Layer(A).Name = nombrerasters Then ' v_Tema Then
                pFLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A

        If Not afound Then
            MsgBox("Carta No Existe en la Vista...")
            Exit Sub
        End If

        pFLDef = pFLayer
        pFLDef.DefinitionExpression = Query_cartas '"NAME = '" & v_hoja & ".ecw'"
        pMxDoc.ActiveView.Refresh()

        pQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = Query_cartas '"NAME = '" & v_hoja & ".ecw'"

        pFSelQuery = pFLayer
        pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFSelQuery.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pMxDoc.ActiveView.Refresh()

        If variable = "1" Then
            Dim pUID As New UID
            pUID.Value = "esriArcMapUI.ZoomToSelectedCommand"

            Dim pFeatureLayer_1 As IFeatureLayer
            pFeatureLayer_1 = pFLDef.CreateSelectionLayer("_sup", True, vbNullString, vbNullString)
            pMap.AddLayer(pFeatureLayer_1)
            pFeatureLayer_1.Name = "Hoja_" & v_Carta.Replace("-", "")

            'For A As Integer = 0 To pMap.LayerCount - 1
            '    pFSelQuery = pMxDoc.FocusMap.Layer(A)
            '    If pMap.Layer(A).Name = "Hoja_" & v_Carta.Replace("-", "") Then
            '    End If
            'Next A
        End If
        pMxDoc.ActiveView.Refresh()
    End Sub

    Public Sub ZOOM_SELECTED(ByVal nombreraster As String, ByVal p_app As IApplication)
        pMxDoc = p_app.Document
        pMap = pMxDoc.FocusMap
        Dim afound As Boolean = False

        Dim pLayer As IFeatureLayer
        Dim pFSel As IFeatureSelection
        'pLayer = pMap.Layer(0)
        'pFSel = pLayer

        For A As Integer = 0 To pMap.LayerCount - 1
            'If pMap.Layer(A).Name = "DATA_GIS.DS_IGN_CARTA_NACIONAL_GEOPSA56" Then ' v_Tema Then
            If pMap.Layer(A).Name = nombreraster Then ' v_Tema Then
                pLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A

        If Not afound Then
            MsgBox("Carta No Existe en la Vista...")
            Exit Sub
        End If

        pFSel = pLayer

        Dim pSelSet As ISelectionSet
        pSelSet = pFSel.SelectionSet

        Dim pEnumGeom As IEnumGeometry
        Dim pEnumGeomBind As IEnumGeometryBind

        pEnumGeom = New EnumFeatureGeometry
        pEnumGeomBind = pEnumGeom
        pEnumGeomBind.BindGeometrySource(Nothing, pSelSet)

        Dim pGeomFactory As IGeometryFactory
        pGeomFactory = New GeometryEnvironment

        Dim pGeom As IGeometry
        pGeom = pGeomFactory.CreateGeometryFromEnumerator(pEnumGeom)

        pMxDoc.ActiveView.Extent = pGeom.Envelope
        pMxDoc.ActiveView.Refresh()
    End Sub

    Public Sub guardar_Listbox_txt(ByVal p_Listbox As Object, ByVal ruta As String, ByVal pstrSaveOrLoad As String)
        Dim i As Long
        'System.IO()
        'SYSTEM.OperatingSystem ruta For Output As #1
        'objFile = objFile.OpenText("C:\MyFile.txt")
        'Const fic As String = "C:\listado.txt"
        'Dim sw As New System.IO.StreamWriter(fic)
        ''escrile los elementos al txt

        'For i = 0 To p_Listbox.ListCount - 1
        '    p_Listbox.Selected(i) = True
        '    'Print #1, p_dgdDetalle.List(p_dgdDetalle.ListIndex)
        '    sw.WriteLine(p_Listbox.List(p_Listbox.ListIndex))

        'Next
        ''Close #1  'cierra txt
        'sw.Close()

        '   Const fic As String = "E:\tmp\Prueba.txt"
        '  Dim texto As String = "Érase una vez una vieja con un moño..."

        ' Dim sw As New System.IO.StreamWriter(fic)
        ' sw.WriteLine(texto)
        ' sw.Close()

    End Sub
  
    Public Sub creandotabla_Rep_Libredenu()
        Try
            Dim tabla As ITable
            Dim pFWS As IFeatureWorkspace
            Dim pWorkspaceFactory As IWorkspaceFactory
            Dim pFieldsEdit As IFieldsEdit
            Dim pFieldEdit As IFieldEdit
            Dim pField As IField
            Dim pFields As IFields = Nothing
            pWorkspaceFactory = New ShapefileWorkspaceFactory
            pFWS = pWorkspaceFactory.OpenFromFile(glo_pathTMP, 0)
            If pFields Is Nothing Then
                pFields = New Fields
                pFieldsEdit = pFields
                pFieldsEdit.FieldCount_2 = 1
                pField = New Field
                pFieldEdit = pField
                With pFieldEdit
                    .Length_2 = 16
                    .Name_2 = "CG_CODIGO"
                    .Type_2 = esriFieldType.esriFieldTypeString
                End With
                pFieldsEdit.Field_2(0) = pField
            End If
            tabla = pFWS.CreateTable("Reporte1_Libredenu" & fecha_archi, pFields, Nothing, Nothing, "")
            Dim pFields2 As IFields
            Dim pField2 As IField
            Dim pFieldEdit2 As IFieldEdit
            pField2 = New Field
            pFieldEdit2 = pField2
            pFields2 = tabla.Fields
            pFieldEdit2 = pField2

            With pFieldEdit2
                .Length_2 = 20
                .Name_2 = "EST_SUPER"
                .Type_2 = esriFieldType.esriFieldTypeString
            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Length_2 = 50
                .Name_2 = "NIV_SUPER"
                .Type_2 = esriFieldType.esriFieldTypeString
            End With
            tabla.AddField(pFieldEdit2)
            With pFieldEdit2
                .Length_2 = 10
                .Name_2 = "URBA_U"
                .Type_2 = esriFieldType.esriFieldTypeString
            End With
            tabla.AddField(pFieldEdit2)
            With pFieldEdit2
                .Length_2 = 10
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "URBA_EX"
            End With
            tabla.AddField(pFieldEdit2)
            With pFieldEdit2
                .Length_2 = 10
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "CLASE_N"

            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Length_2 = 10
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "CLASE_A"
            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Length_2 = 10
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "CLASE_V"
            End With
            tabla.AddField(pFieldEdit2)


            With pFieldEdit2
                .Length_2 = 10
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "ZONA_A"
            End With
            tabla.AddField(pFieldEdit2)


            With pFieldEdit2
                .Length_2 = 10
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "PROY_E"

            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Length_2 = 10
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "OT"

            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Length_2 = 10
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "PUERTO"

            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Length_2 = 10
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "RESE_T"

            End With
            tabla.AddField(pFieldEdit2)


            With pFieldEdit2
                .Length_2 = 10
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "OTRO"

            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Length_2 = 10
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "ZONA_T"

            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Length_2 = 10
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "FRONTERA"

            End With
            tabla.AddField(pFieldEdit2)


            With pFieldEdit2
                .Length_2 = 10
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "URB_AL"

            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Length_2 = 10
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "DM"

            End With
            tabla.AddField(pFieldEdit2)


            With pFieldEdit2
                .Length_2 = 10
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "PROTG"

            End With
            tabla.AddField(pFieldEdit2)


            With pFieldEdit2
                .Length_2 = 10
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "FRONT_P"

            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Length_2 = 10
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "ANAP"

            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Length_2 = 100
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "NOMBRE_DM"

            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Length_2 = 16
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "TIPO_EX"

            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Length_2 = 16
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "SITU_EX"

            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Length_2 = 16
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "ESTADO1"

            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Length_2 = 250
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "NM_RESE"

            End With
            tabla.AddField(pFieldEdit2)

            With pFieldEdit2
                .Length_2 = 200
                .Type_2 = esriFieldType.esriFieldTypeString
                .Name_2 = "NM_URBA"

            End With
            tabla.AddField(pFieldEdit2)



            With pFieldEdit2
                .Type_2 = esriFieldType.esriFieldTypeDouble
                .Name_2 = "HA_GIS"
                .Precision_2 = 10
                .Scale_2 = 4

            End With
            tabla.AddField(pFieldEdit2)




        Catch ex As Exception
            MsgBox("Error en generar tabla", MsgBoxStyle.Critical, "Obsrevación...")
        End Try
    End Sub

    Public Sub Creando_temporal_padronminero()
        Try
            Dim tabla As ITable
            Dim pFWS As IFeatureWorkspace
            Dim pWorkspaceFactory As IWorkspaceFactory
            Dim pFieldsEdit As IFieldsEdit
            Dim pFieldEdit As IFieldEdit
            Dim pField As IField
            Dim pFields As IFields = Nothing
            pWorkspaceFactory = New ShapefileWorkspaceFactory
            pFWS = pWorkspaceFactory.OpenFromFile(glo_pathTMP, 0)
            If pFields Is Nothing Then
                pFields = New Fields
                pFieldsEdit = pFields
                pFieldsEdit.FieldCount_2 = 1
                pField = New Field
                pFieldEdit = pField
                With pFieldEdit
                    .Length_2 = 16
                    .Name_2 = "CG_CODIGO"
                    .Type_2 = esriFieldType.esriFieldTypeString
                End With
                pFieldsEdit.Field_2(0) = pField
            End If
            tabla = pFWS.CreateTable("Reporte_padron" & fecha_archi, pFields, Nothing, Nothing, "")
            Dim pFields2 As IFields
            Dim pField2 As IField
            Dim pFieldEdit2 As IFieldEdit
            pField2 = New Field
            pFieldEdit2 = pField2
            pFields2 = tabla.Fields
            pFieldEdit2 = pField2
            With pFieldEdit2
                .Type_2 = esriFieldType.esriFieldTypeDouble
                .Name_2 = "AREADISPO"
                .Precision_2 = 10
                .Scale_2 = 4

            End With
            tabla.AddField(pFieldEdit2)




        Catch ex As Exception
            MsgBox("Error en generar tabla", MsgBoxStyle.Critical, "Obsrevación...")
        End Try
    End Sub



    Public Function CalculaInterseccion_neto(ByVal pTipo)
        Dim cls_oracle As New cls_Oracle
        'pMxDoc = p_App.Document
        Dim pOriginalLayer As IFeatureLayer = Nothing
        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "DATA_GIS.CATA_NT" Then
                pOriginalLayer = pMap.Layer(A)
                aFound = True
                Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("No existe el Layer: ", MsgBoxStyle.Information, "[BDGEOCATMIN]")
            Return 0
            Exit Function
        End If
        Dim pPoint As IPoint
        Dim pPoly As IPolygon
        Dim pPolyPointColl As IPointCollection
        Dim i As Integer
        Dim lostrZU As String = ""
        '-Dim pQF As IQueryFilter
        pQFilter = New QueryFilter
        'pQFilter.WhereClause = "" ' to get all features
        Dim pFeatCursor As IFeatureCursor
        'pFeatCursor = pOriginalLayer.FeatureClass.Search(Nothing, False)
        Dim pFeat As IFeature
        'pFeat = pFeatCursor.NextFeature
        Dim lostrClase As String = ""
        Dim loStrCoordenada As String
        Dim loTotPoligono As Integer = 0
        Dim loTotParcial As Integer = 0
        Dim loTotLleno As Integer = 0
        Dim loTotLibre As Integer = 0
        Dim lodtbSeleccion As New Data.DataTable
        loTotPoligono = 0
        Dim loCampo As String = ""

        Dim pQueryFilter As IQueryFilter
        pQueryFilter = New QueryFilter
        Dim pActiveView As IActiveView
        pActiveView = pMap

        Dim pFeatureSelection As IFeatureSelection = pOriginalLayer
        pQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = "CODIGOU = '09009686X01' OR CODIGOU = '09852814Y01' OR CODIGOU = '09009685X01'"

        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

        Dim PSELE As ISelectionSet
        pFeatCursor = pOriginalLayer.FeatureClass.Search(pQueryFilter, True)

        PSELE = pFeatureSelection.SelectionSet

        Dim CON As Integer
        CON = PSELE.Count

        loStrCoordenada = ""


        pFeat = pFeatCursor.NextFeature

        Do Until pFeat Is Nothing
            'loStrCoordenada = ""
            pPoly = pFeat.Shape
            pPolyPointColl = pPoly
            v_codigo = pFeat.Value(pFeatCursor.FindField("CODIGOU"))
            ' loop through all points in feature polygon
            For i = 0 To pPolyPointColl.PointCount - 1
                pPoint = pPolyPointColl.Point(i)
                loStrCoordenada = loStrCoordenada & pPoint.X & " " & pPoint.Y & ", "
            Next i  ' get next point in polygon
            loStrCoordenada = Mid(loStrCoordenada, 1, Len(loStrCoordenada) - 2)
            pFeat = pFeatCursor.NextFeature
        Loop


        Dim lodbtTotal = cls_oracle.FT_Intersecta_Fclass_Oracle_1("9", "DATA_GIS.CATA_T", loStrCoordenada, "010556207")
        'Dim lodbtExisteAR As New DataTable
        'Try
        '    lodbtExisteAR = cls_oracle.FT_Intersecta_Fclass_Oracle_1(8, "DATA_GIS.CATA_T", loStrCoordenada, v_codigo)
        'Catch ex As Exception
        '    'lodbtExisteAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(2, gstrFC_Catastro_Minero & gloZona, gstrFC_AReservada & gloZona, v_codigo, gstrUsuarioAcceso, gstrUsuarioClave, gstrDatabaseGIS)
        'End Try
        Dim lodbtExisteAR As New DataTable
        MsgBox(lodbtExisteAR.Rows.Count, MsgBoxStyle.Critical, "")
        Dim CODIGO As String
        Dim AREA As String
        If lodbtExisteAR.Rows.Count >= 1 Then
            For contador1 As Integer = 0 To lodbtExisteAR.Rows.Count - 1
                CODIGO = lodbtExisteAR.Rows(contador1).Item("CODIGOU")
                AREA = lodbtExisteAR.Rows(contador1).Item("AREANETA")

            Next contador1

        End If
    End Function

    Public Sub Cargar_FeatureClass_SDE()
        Dim cls_Oracle As New cls_Oracle
        Dim ldtLayer As New DataTable
        'ldtLayer = cls_Oracle.F_Obtiene_Lista_Layer("1", "SIGGEOCATMIN_LAYERS")
        Try
            ldtLayer = cls_Oracle.FT_SEL_LISTA_DESCRIPCION("1", "SIGGCTMIN_FC")
            For r As Integer = 0 To ldtLayer.Rows.Count - 1
                Select Case ldtLayer.Rows(r).Item(0)
                    Case "CDIST"
                        gstrFC_CDistrito = ldtLayer.Rows(r).Item(1)
                    Case "DPTO"
                        gstrFC_Departamento = ldtLayer.Rows(r).Item(1)
                    Case "PROV"
                        gstrFC_Provincia = ldtLayer.Rows(r).Item(1)
                    Case "DIST"
                        gstrFC_Distrito = ldtLayer.Rows(r).Item(1)
                    Case "RIOS"
                        gstrFC_Rios = ldtLayer.Rows(r).Item(1)
                    Case "CARRETERA"
                        gstrFC_Carretera = ldtLayer.Rows(r).Item(1)
                    Case "CPOBLADO"
                        gstrFC_CPoblado = ldtLayer.Rows(r).Item(1)
                    Case "CMINERO"
                        gstrFC_Catastro_Minero = ldtLayer.Rows(r).Item(1)
                    Case "CUADRICULA"
                        gstrFC_Cuadricula = ldtLayer.Rows(r).Item(1)
                    Case "ARESERVADA"
                        gstrFC_AReservada = ldtLayer.Rows(r).Item(1)
                    Case "ZURBANA"
                        gstrFC_ZUrbana = ldtLayer.Rows(r).Item(1)
                    Case "ZURBANA56"
                        gstrFC_ZUrbana56 = ldtLayer.Rows(r).Item(1)
                    Case "ARESERVA56"
                        gstrFC_AReservada56 = ldtLayer.Rows(r).Item(1)
                    Case "ZTRASLAPE"
                        gstrFC_ZTraslape = ldtLayer.Rows(r).Item(1)
                    Case "FRONTERA"
                        gstrFC_Frontera = ldtLayer.Rows(r).Item(1)
                    Case "LHOJAS"
                        gstrFC_LHojas = ldtLayer.Rows(r).Item(1)
                    Case "CARTA"
                        gstrFC_Carta = ldtLayer.Rows(r).Item(1)
                End Select
            Next
        Catch ex As Exception
            MsgBox("Error de Asignación de Feature Class.... ", MsgBoxStyle.Information, "[BDGEOCATMIN]")
        End Try
    End Sub


End Class

