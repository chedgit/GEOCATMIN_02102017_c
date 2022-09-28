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
Public Class cls_Prueba
    Private cls_Catastro As New cls_DM_1

    Private Sub Show_Vertice(ByVal p_App As IApplication)
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        Dim pFLayer As IFeatureLayer
        pFLayer = pMap.Layer(0)

        Dim pFClass As IFeatureClass
        pFClass = pFLayer.FeatureClass
        Dim pFCursor As IFeatureCursor
        pFCursor = pFClass.Update(Nothing, True)
        Dim pFeature As IFeature
        pFeature = pFCursor.NextFeature
        Dim pFSel As IFeatureSelection
        pFSel = pFLayer
        Dim pSelSet As ISelectionSet
        Select Case pFSel.SelectionSet.Count
            Case 0
                MsgBox("No hay ninguna Selección ")
                Exit Sub
        End Select
        pSelSet = pFSel.SelectionSet
        Dim pQFilter As IQueryFilter
        pQFilter = New QueryFilter
        pQFilter.WhereClause = ""
        Dim pFeatureCursor As IFeatureCursor = Nothing
        pSelSet.Search(pQFilter, False, pFeatureCursor)
        pFeature = pFeatureCursor.NextFeature
        While Not pFeature Is Nothing
            MsgBox(pFeature.Value(28))
            pFeature = pFeatureCursor.NextFeature
        End While
    End Sub
    Function Consulta_DM_x_Nombre(ByVal p_Layer As String, ByVal p_Filtro As String, ByVal p_App As IApplication)
        x = 0
        Dim pCursor As ICursor
        Dim pRow As IRow
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
            .WhereClause = p_Filtro.ToUpper
            pCursor = .Evaluate
        End With
        Dim lostr_Zona As String = ""
        pRow = pCursor.NextRow
        Dim lodtRegistros As New DataTable
        Do Until pRow Is Nothing
            If x = 0 Then
                lodtRegistros.Columns.Add("CODIGO")
                lodtRegistros.Columns.Add("NOMBRE")
                lodtRegistros.Columns.Add("TIPO")
                lodtRegistros.Columns.Add("CARTA")
                lodtRegistros.Columns.Add("SITUACION")
                lodtRegistros.Columns.Add("TITULAR")
                x = 1
            End If
            Dim dr As DataRow
            dr = lodtRegistros.NewRow
            dr.Item(0) = (pRow.Value(pRow.Fields.FindField("CODIGOU"))).ToString
            dr.Item(1) = pRow.Value(pRow.Fields.FindField("CONCESION"))
            dr.Item(2) = pRow.Value(pRow.Fields.FindField("TIPO_EX"))
            dr.Item(3) = pRow.Value(pRow.Fields.FindField("CARTA"))
            dr.Item(4) = pRow.Value(pRow.Fields.FindField("D_ESTADO"))
            dr.Item(5) = pRow.Value(pRow.Fields.FindField("TIT_CONCES"))
            lodtRegistros.Rows.Add(dr)
            pRow = pCursor.NextRow
        Loop
        'Dim lodtvOrdena As New DataView(lodtRegistros, Nothing, "NOMBRE ASC", DataViewRowState.CurrentRows)
        Return lodtRegistros
    End Function
    'Public Sub SeleccionarDM_x_Codigo(ByVal lo_CodigoDM As String, ByVal p_App As IApplication)

    '    pMxDoc = p_App.Document
    '    Dim pPoint As IPoint
    '    pPoint = pMxDoc.CurrentLocation
    '    Dim pActiveView As IActiveView
    '    pMap = pMxDoc.FocusMap
    '    pActiveView = pMap
    '    Dim pQueryFilter As IQueryFilter = New QueryFilter
    '    Dim pLayerDef As IFeatureLayerDefinition = pFeatureLayer
    '    Dim pFeatureSelection As IFeatureSelection
    '    pLayerDef.DefinitionExpression = "CODIGOU = " & "'" & lo_CodigoDM & "'"
    '    pMap.AddLayer(pFeatureLayer)
    '    pFeatureLayer = pMap.Layer(0)
    '    pQueryFilter.WhereClause = "CODIGOU = " & "'" & lo_CodigoDM & "'"
    '    pFeatureSelection = pLayerDef
    '    pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
    '    'Refresh again to draw the new selection.
    '    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
    '    Dim pFSel As IFeatureSelection
    '    pFSel = pFeatureLayer
    '    'Get the selected features
    '    Dim pSelSet As ISelectionSet
    '    If pFSel.SelectionSet.Count = 0 Then
    '        MsgBox("No hay ninguna Selección") : Exit Sub
    '    End If
    '    pSelSet = pFSel.SelectionSet
    '    Dim pEnumGeom As IEnumGeometry
    '    Dim pEnumGeomBind As IEnumGeometryBind
    '    pEnumGeom = New EnumFeatureGeometry
    '    pEnumGeomBind = pEnumGeom
    '    pEnumGeomBind.BindGeometrySource(Nothing, pSelSet)
    '    Dim pGeomFactory As IGeometryFactory
    '    pGeomFactory = New GeometryEnvironment
    '    Dim pGeom As IGeometry
    '    pGeom = pGeomFactory.CreateGeometryFromEnumerator(pEnumGeom)
    '    pMxDoc.ActiveView.Extent = pGeom.Envelope
    '    pMxDoc.ActiveView.Refresh()
    '    xMin.Text = pGeom.Envelope.XMin
    '    xMax.Text = pGeom.Envelope.XMax
    '    yMin.Text = pGeom.Envelope.YMin
    '    yMax.Text = pGeom.Envelope.YMax
    '    cls_Catastro.HazZoom(pGeom.Envelope.XMin, pGeom.Envelope.YMin, pGeom.Envelope.XMax, pGeom.Envelope.YMax, 100, pApp)
    'End Sub

    Function f_Genera_Leyenda_DM(ByVal p_Layer As String, ByVal lo_Filtro_DM As String, ByVal p_App As IApplication)
        Dim lo_Campo As String = ""
        Dim lodtOrdena As New DataTable
        Select Case p_Layer
            Case "Catastro"
                lo_Campo = "LEYENDA"
            Case "Geologia"
                lo_Campo = "CODI"
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
                If pMap.Layer(A).Name = p_Layer Then
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
            'MsgBox(lodtvOrdena.Item(0).Row(0))
            For i = 0 To lodtvOrdena.Count - 1
                If i = 0 Then lodtOrdena.Columns.Add(lo_Campo)
                Dim dr As DataRow
                dr = lodtOrdena.NewRow
                dr.Item(0) = lodtvOrdena.Item(i).Row(0)
                lodtOrdena.Rows.Add(dr)
            Next

        Catch ex As Exception
            MsgBox("No se realizo Distinct de Leyenda de " & p_Layer, MsgBoxStyle.Information, "Observacion  ")
        End Try
        Return lodtOrdena
    End Function
    Function f_Genera_Leyenda_Geo(ByVal p_Layer As String, ByVal p_Filtro As String, ByVal p_App As IApplication)
        Dim lo_Campo As String = ""
        Select Case p_Layer
            Case "Catastro"
                lo_Campo = "LEYENDA"
            Case "Geologia"
                lo_Campo = "CODI"
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
                If pMap.Layer(A).Name = p_Layer Then
                    pLayer = pMxDoc.FocusMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("Layer No Existe.")
                Return Nothing
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
            pQueryFilter.WhereClause = p_Filtro
            pFeatCursor = pFeatureClass.Search(pQueryFilter, False)
            '*******
            Dim pRow As IRow = pFeatCursor.NextFeature
            Dim lo_ArrayCodigo(200) As Integer
            Dim w As Integer = 0
            Dim lo_encontre As Boolean = False
            lodtRegistros.Columns.Add(lo_Campo, System.Type.GetType("System.Decimal"))
            Try
                lo_ArrayCodigo(w) = pRow.Value(1)
                Do Until pRow Is Nothing
                    lo_encontre = False
                    For x As Integer = 0 To w
                        Select Case x
                            'Case 0
                            '    lo_ArrayCodigo(w) = pRow.Value(1)
                            Case Else
                                If lo_ArrayCodigo(x) = pRow.Value(1) Then
                                    lo_encontre = True
                                End If
                        End Select

                    Next
                    If lo_encontre = False Or w = 0 Then
                        Dim dr As DataRow
                        dr = lodtRegistros.NewRow
                        dr.Item(0) = pRow.Value(1)
                        lodtRegistros.Rows.Add(dr)
                        lo_ArrayCodigo(w) = pRow.Value(1)
                        w = w + 1
                    End If
                    pRow = pFeatCursor.NextFeature
                Loop
            Catch ex As Exception
                MsgBox(w)
            End Try

            'If pRow Is Nothing Then
            '    lodtRegistros.Columns.Add(lo_Campo, System.Type.GetType("System.Decimal"))
            '    Return lodtRegistros
            '    Exit Function
            'End If
            ''***********************
            'Dim rx As IRandomColorRamp
            'rx = New RandomColorRamp
            'Dim pRender As IUniqueValueRenderer, n As Long
            'pRender = New UniqueValueRenderer
            'Dim symd As ISimpleFillSymbol
            'symd = New SimpleFillSymbol
            ''Leyenda para la parte inicial de la leyenda (color Blanco)
            'symd.Style = esriSimpleFillStyle.esriSFSSolid ' esriSFSSolid
            'symd.Outline.Width = 0.4
            'Dim myColor As IColor
            'myColor = New RgbColor
            'myColor.RGB = RGB(255, 255, 255)
            'symd.Color = myColor
            'pRender.FieldCount = 1
            'pRender.Field(0) = lo_Campo
            'pRender.DefaultSymbol = symd
            'pRender.UseDefaultSymbol = True
            ''Barriendo los registros
            'Dim pFeat As IFeature
            'n = pFeatureClass.FeatureCount(pQueryFilter)
            'Dim i As Integer = 0
            'Dim ValFound As Boolean
            'Dim NoValFound As Boolean
            'Dim uh As Integer
            'Dim pFields As IFields
            'Dim iField As Integer
            ''Dim pField As IField
            'pFields = pFeatureClass.Fields
            'iField = pFields.FindField(lo_Campo)
            'Do Until i = n
            '    Dim symx As ISimpleFillSymbol
            '    symx = New SimpleFillSymbol
            '    symx.Style = esriSimpleFillStyle.esriSFSForwardDiagonal ' esriSFSForwardDiagonal
            '    pFeat = pFeatCursor.NextFeature
            '    Dim X As String
            '    X = pFeat.Value(iField)
            '    If X <> "" Then
            '        ValFound = False
            '        For uh = 0 To (pRender.ValueCount - 1)
            '            If pRender.Value(uh) = X Then
            '                NoValFound = True
            '                Exit For
            '            End If
            '        Next uh
            '        If Not ValFound Then
            '            pRender.AddValue(X, lo_Campo, symx)
            '            pRender.Label(X) = X
            '            pRender.Symbol(X) = symx
            '        End If
            '    End If
            '    i = i + 1
            'Loop
            'rx.Size = pRender.ValueCount
            'rx.CreateRamp(True)
            'Dim RColors As IEnumColors, ny As Long
            'RColors = rx.Colors
            'RColors.Reset()
            'For ny = 0 To (pRender.ValueCount - 1)
            '    'Dim xv As String = pRender.Value(ny)
            '    If ny = 0 Then lodtRegistros.Columns.Add(lo_Campo, System.Type.GetType("System.Decimal"))
            '    Dim dr As DataRow
            '    dr = lodtRegistros.NewRow
            '    dr.Item(0) = pRender.Value(ny)
            '    lodtRegistros.Rows.Add(dr)
            'Next
            Dim lodtvOrdena As New DataView(lodtRegistros, Nothing, lo_Campo & " ASC", DataViewRowState.CurrentRows)
            'MsgBox(lodtvOrdena.Item(0).Row(0))
            Dim lodtOrdena As New DataTable
            For i As Integer = 0 To lodtvOrdena.Count - 1
                If i = 0 Then lodtOrdena.Columns.Add(lo_Campo)
                Dim dr As DataRow
                dr = lodtOrdena.NewRow
                dr.Item(0) = lodtvOrdena.Item(i).Row(0)
                lodtOrdena.Rows.Add(dr)
            Next
            Return lodtOrdena
        Catch ex As Exception
            'MsgBox("No se realizo Distinct de Leyenda de " & p_Layer, MsgBoxStyle.Information, "Observacion  ")
            Return ""
        End Try
    End Function
    Public Sub Poligono_Color_1(ByVal lo_FeatureClass As String, ByVal lo_Tabla As DataTable, ByVal loStyle As String, ByVal loCampo As String, ByVal p_Categoria As String, ByVal p_App As IApplication)
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
        Dim pFillSymbol As IFillSymbol = Nothing
        Dim pStyleGlry As IStyleGallery
        pStyleGlry = New StyleGallery
        Dim pStylStor As IStyleGalleryStorage
        pStylStor = pStyleGlry
        pStylStor.AddFile(loStyle)
        pEnumStyleGall = pStyleGallery.Items("Fill Symbols", loStyle, p_Categoria)
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
                    Case "G1"
                        pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "D.M. en Trámite" 'Color Tramite
                    Case "G2"
                        pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "D.M. en Trámite D.L. 109" 'Color Tramite 109
                    Case "G3"
                        pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "D.M. Titulados" 'Color Titulado
                    Case "G4"
                        pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "D.M. Extinguido" 'Color Extinguido
                    Case "G5"
                        pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "D.M. - Otros" ' Color Planta, deposito
                    Case "G6"
                        pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "DM_" & v_codigo
                End Select
            End If
            'End If
            'pRow = pCursor.NextRow
        Next 'Loop
        Dim pLayer As IFeatureLayer = Nothing
        Dim pGeoFeatureLayer As IGeoFeatureLayer
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        Dim Afound As Boolean
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = lo_FeatureClass Then '& "_" & strZona Then
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


    Public Sub Poligono_Color_2(ByVal lo_FeatureClass As String, ByVal lo_Tabla As DataTable, ByVal loStyle As String, ByVal loCampo As String, ByVal p_Categoria As String, ByVal p_App As IApplication)
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
        Dim pFillSymbol As IFillSymbol = Nothing
        Dim pStyleGlry As IStyleGallery
        pStyleGlry = New StyleGallery
        Dim pStylStor As IStyleGalleryStorage
        pStylStor = pStyleGlry
        pStylStor.AddFile(loStyle)
        pEnumStyleGall = pStyleGallery.Items("Fill Symbols", loStyle, p_Categoria)
        pEnumStyleGall.Reset()
        pStyleItem = pEnumStyleGall.Next
        'Do Until pRow Is Nothing
        Try
            For i As Integer = 0 To lo_Tabla.Rows.Count - 1
                If pFillSymbol Is Nothing Then
                    pEnumStyleGall.Reset()
                    pStyleItem = pEnumStyleGall.Next
                End If
                Do While Not pStyleItem Is Nothing   'Loop through and access each marker
                    If lo_Tabla.Rows(i).Item(loCampo) Then pStyleItem = pEnumStyleGall.Next
                    pFillSymbol = Nothing
                    If lo_Tabla.Rows(i).Item(loCampo) <= pStyleItem.ID Then
                        pFillSymbol = pStyleItem.Item
                        Exit Do
                    End If
                    pStyleItem = pEnumStyleGall.Next
                Loop
                If pStyleItem Is Nothing Then
                Else
                    pUniqueValueRenderer.AddValue(lo_Tabla.Rows(i).Item(loCampo), Nothing, pFillSymbol)
                    pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = Mid(pStyleItem.Name, 6)
                    'pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = pStyleItem.Name
                End If
            Next
        Catch ex As Exception
            ' MsgBox(ex.Message)
        End Try
        Dim pLayer As IFeatureLayer = Nothing
        Dim pGeoFeatureLayer As IGeoFeatureLayer
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        Dim Afound As Boolean
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = lo_FeatureClass Then '& "_" & strZona Then
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


    Public Sub Poligono_Color_GDB(ByVal lo_FeatureClass As String, ByVal loStyle As String, ByVal loCampo As String, ByVal strZona As String)
        pWorkspaceFactory = New ShapefileWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_Path, 0)
        pTable = pFeatureWorkspace.OpenTable(lo_FeatureClass)

        'Dim player As ILayer
        'player = pMap.Layer(0).Name
        Dim pTableName As String = pMap.Layer(0).Name

        Dim pQueryDef As IQueryDef
        Dim pRow As IRow
        Dim pCursor As ICursor
        'Dim pDataset As IDataset
        'pDataset = pTable
        'pFeatureWorkspace = pTable.Workspace
        pQueryDef = pFeatureWorkspace.CreateQueryDef
        With pQueryDef
            .Tables = pTableName
            .SubFields = "DISTINCT(" & loCampo & ")"
            pCursor = .Evaluate
        End With
        pRow = pCursor.NextRow

        Dim pUniqueValueRenderer As IUniqueValueRenderer
        ' Define the renderer.
        pUniqueValueRenderer = New UniqueValueRenderer
        pUniqueValueRenderer.FieldCount = 1
        pUniqueValueRenderer.Field(0) = loCampo
        Dim pStyleStorage As IStyleGalleryStorage
        Dim pStyleGallery As IStyleGallery

        pMxDoc = m_application.Document
        pMap = pMxDoc.FocusMap
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
            End If
            Do While Not pStyleItem Is Nothing   'Loop through and access each marker
                pFillSymbol = Nothing
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
            Loop
            If pStyleItem Is Nothing Then
            Else
                If Len(pStyleItem.Name) > 0 And Not (pRow Is Nothing) Then
                    pUniqueValueRenderer.AddValue(pRow.Value(0), "", pFillSymbol)
                    Select Case pStyleItem.Name
                        Case "G1"
                            pUniqueValueRenderer.Label(pRow.Value(0)) = "D.M. Titulados" 'Color Titulado
                        Case "G2"
                            pUniqueValueRenderer.Label(pRow.Value(0)) = "D.M. en Trámite" 'Color Tramite
                        Case "G3"
                            pUniqueValueRenderer.Label(pRow.Value(0)) = "D.M. en Trámite D.L. 109" 'Color Tramite 109
                        Case "G4"
                            pUniqueValueRenderer.Label(pRow.Value(0)) = "D.M. Extinguido" 'Color Planta
                        Case "G5"
                            pUniqueValueRenderer.Label(pRow.Value(0)) = "D.M. - Otros" ' Color Extinguido
                    End Select
                End If
            End If
            pRow = pCursor.NextRow
        Loop
        Dim pLayer As IFeatureLayer = Nothing
        Dim pGeoFeatureLayer As IGeoFeatureLayer
        pMxDoc = m_application.Document
        pMap = pMxDoc.FocusMap
        Dim Afound As Boolean
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = lo_FeatureClass Then '& "_" & strZona Then
                pLayer = pMap.Layer(A)
                Afound = True
                Exit For
            End If
        Next A
        If Not Afound Then
            MsgBox("No se encuentra el Layer")
            Exit Sub
        End If
        'pLayer = pMap.Layer(1) ' 1 Catastro
        pGeoFeatureLayer = pLayer
        pGeoFeatureLayer.Renderer = pUniqueValueRenderer
        pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, Nothing)
        pMxDoc.UpdateContents()
    End Sub

    Public Sub IntersectSelect_por_Limite(ByVal p_App As IApplication, ByVal p_Filtro_Ubigeo As String, ByVal p_Filtro_DM As String, ByVal p_layer As String, ByVal Xmin As Object, ByVal yMin As Object, ByVal xMax As Object, ByVal yMax As Object, ByVal p_Existe As Object)
        ' Part 1: Create a cursor of interstates.
        p_Existe.text = 0
        Dim pLayerOne As IFeatureLayer = Nothing
        Dim pFeatSelection As IFeatureSelection
        Dim pQueryFilter As IQueryFilter
        Dim pLayerSelSet As ISelectionSet
        Dim pLayerCursor As IFeatureCursor = Nothing
        pMxDoc = p_App.Document ' ThisDocument
        pMap = pMxDoc.FocusMap
        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = p_layer Then
                pLayerOne = pMap.Layer(A) 'Layer de Dpto
                aFound = True
                Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("No existe el Layer: " & p_layer, MsgBoxStyle.Information, "[BDGEOCATMIN]")
            Exit Sub
        End If
        pFeatSelection = pLayerOne
        ' Select interstates.
        pQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = p_Filtro_Ubigeo.ToUpper

        pFeatSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        ' Create a feature cursor of selected interstates.
        pLayerSelSet = pFeatSelection.SelectionSet
        If pLayerSelSet.Count = 0 Then
            p_Existe.text = -2 : Exit Sub
        End If
        pLayerSelSet.Search(Nothing, False, pLayerCursor)
        ' Part 2: Select high-growth counties that intersect an interstate.

        Dim pLayer_Two As IFeatureLayer = Nothing
        'Dim pElement As IElement
        Dim pLayerSelection As IFeatureSelection
        Dim p_Ifeature As IFeature
        Dim pSpatialFilter As ISpatialFilter
        'Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Catastro" Then
                pLayer_Two = pMxDoc.FocusMap.Layer(A) ' Catastro
                aFound = True
                Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("No existe capa de Catastro")
            Exit Sub
        End If
        'pLayer_Two = pMxDoc.FocusMap.Layer(0)
        Select Case sele_denu
            Case True
                p_Filtro_DM = " ESTADO <> 'Y' "
        End Select
        pLayerSelection = pLayer_Two
        ' Prepare a spatial filter.
        pSpatialFilter = New SpatialFilter
        pSpatialFilter.WhereClause = p_Filtro_DM
        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects ' esriSpatialRelIntersects
        ' Step through each interstate and select counties.
        p_Ifeature = pLayerCursor.NextFeature

        Do Until p_Ifeature Is Nothing
            ' Define the geometry of the spatial filter.
            pSpatialFilter.Geometry = p_Ifeature.Shape
            ' Select counties and add them to the selection set.
            pLayerSelection.SelectFeatures(pSpatialFilter, esriSelectionResultEnum.esriSelectionResultAdd, False) ' esriSelectionResultAdd, False)

            p_Ifeature = pLayerCursor.NextFeature
        Loop
        'p_swSelection = pLayerSelection.SelectionSet.Count
        If pLayerSelection.SelectionSet.Count = 0 Then
            p_Existe.text = -1 : Exit Sub
        Else
            p_Existe.text = pLayerSelection.SelectionSet.Count
        End If
        ' Part 3: Draw all selected features and report number of counties selected.
        Dim pActiveView As IActiveView
        Dim pLayer_SelSet As ISelectionSet
        pActiveView = pMxDoc.FocusMap
        ' Draw all selected features.
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pLayer_SelSet = pLayerSelection.SelectionSet

        'MsgBox("There are " & pCountySelSet.Count & " Counties selected")
        Dim pEnumGeom As IEnumGeometry
        Dim pEnumGeomBind As IEnumGeometryBind
        pEnumGeom = New EnumFeatureGeometry
        pEnumGeomBind = pEnumGeom
        pEnumGeomBind.BindGeometrySource(Nothing, pLayerSelSet)
        Dim pGeomFactory As IGeometryFactory
        pGeomFactory = New GeometryEnvironment
        Dim pGeom As IGeometry
        pGeom = pGeomFactory.CreateGeometryFromEnumerator(pEnumGeom)
        pMxDoc.ActiveView.Extent = pGeom.Envelope
        Xmin.Text = pGeom.Envelope.XMin
        yMin.text = pGeom.Envelope.YMin
        xMax.text = pGeom.Envelope.XMax
        yMax.text = pGeom.Envelope.YMax
    End Sub
    Public Sub Intersection_por_Cartas(ByVal p_App As IApplication, ByVal p_Filtro_Ubigeo As String, ByVal p_Filtro_DM As String, ByVal p_layer As String, ByVal Xmin As Object, ByVal yMin As Object, ByVal xMax As Object, ByVal yMax As Object, ByVal p_Existe As Object)
        ' Part 1: Create a cursor of interstates.
        'Dim pMxDoc As IMxDocument
        'Dim pMap As IMap
        p_Existe.text = 0
        Dim pLayerOne As IFeatureLayer = Nothing
        Dim pFeatSelection As IFeatureSelection
        Dim pQueryFilter As IQueryFilter
        Dim pLayerSelSet As ISelectionSet
        Dim pLayerCursor As IFeatureCursor = Nothing
        pMxDoc = p_App.Document ' ThisDocument
        pMap = pMxDoc.FocusMap
        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = p_layer Then
                pLayerOne = pMap.Layer(A) 'Layer de Dpto
                aFound = True
                Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("No existe el Layer: " & p_layer, MsgBoxStyle.Information, "[BDGEOCATMIN]")
            Exit Sub
        End If
        '        pLayerOne = pMap.Layer(1) 'Layer de Dpto
        pFeatSelection = pLayerOne
        ' Select interstates.
        pQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = p_Filtro_Ubigeo '.ToLower
        pFeatSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        ' Create a feature cursor of selected interstates.
        pLayerSelSet = pFeatSelection.SelectionSet
        pLayerSelSet.Search(Nothing, False, pLayerCursor)
        ' Part 2: Select high-growth counties that intersect an interstate.

        Dim pLayer_Two As IFeatureLayer = Nothing
        'Dim pElement As IElement
        Dim pLayerSelection As IFeatureSelection
        Dim p_Ifeature As IFeature
        Dim pSpatialFilter As ISpatialFilter
        'Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Cuadrangulo" Then
                pLayer_Two = pMxDoc.FocusMap.Layer(A) ' Catastro
                aFound = True
                Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("No existe capa de Catastro")
            Exit Sub
        End If
        'pLayer_Two = pMxDoc.FocusMap.Layer(0)
        pLayerSelection = pLayer_Two
        ' Prepare a spatial filter.
        pSpatialFilter = New SpatialFilter
        pSpatialFilter.WhereClause = p_Filtro_DM
        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects ' esriSpatialRelIntersects
        ' Step through each interstate and select counties.
        p_Ifeature = pLayerCursor.NextFeature

        Do Until p_Ifeature Is Nothing
            ' Define the geometry of the spatial filter.
            pSpatialFilter.Geometry = p_Ifeature.Shape
            ' Select counties and add them to the selection set.
            pLayerSelection.SelectFeatures(pSpatialFilter, esriSelectionResultEnum.esriSelectionResultAdd, False) ' esriSelectionResultAdd, False)
            p_Ifeature = pLayerCursor.NextFeature
        Loop
        'p_swSelection = pLayerSelection.SelectionSet.Count
        If pLayerSelection.SelectionSet.Count = 0 Then
            p_Existe.text = -1 : Exit Sub
        Else
            p_Existe.text = pLayerSelection.SelectionSet.Count
        End If
        ' Part 3: Draw all selected features and report number of counties selected.
        Dim pActiveView As IActiveView
        Dim pLayer_SelSet As ISelectionSet
        pActiveView = pMxDoc.FocusMap
        ' Draw all selected features.
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pLayer_SelSet = pLayerSelection.SelectionSet

        'MsgBox("There are " & pCountySelSet.Count & " Counties selected")
        Dim pEnumGeom As IEnumGeometry
        Dim pEnumGeomBind As IEnumGeometryBind
        pEnumGeom = New EnumFeatureGeometry
        pEnumGeomBind = pEnumGeom
        pEnumGeomBind.BindGeometrySource(Nothing, pLayerSelSet)
        Dim pGeomFactory As IGeometryFactory
        pGeomFactory = New GeometryEnvironment
        Dim pGeom As IGeometry
        pGeom = pGeomFactory.CreateGeometryFromEnumerator(pEnumGeom)
        pMxDoc.ActiveView.Extent = pGeom.Envelope
        Xmin.Text = pGeom.Envelope.XMin
        yMin.text = pGeom.Envelope.YMin
        xMax.text = pGeom.Envelope.XMax
        yMax.text = pGeom.Envelope.YMax
    End Sub
    Public Sub IntersectSelect_Feature_Class(ByVal p_Layer_0 As String, ByVal p_Filtro_0 As String, ByVal p_Layer_1 As String, ByVal p_Filtro_1 As String, ByVal Xmin As Object, ByVal yMin As Object, ByVal xMax As Object, ByVal yMax As Object, ByVal p_Existe As Object, ByVal p_App As IApplication)
        Dim pLayerOne As IFeatureLayer = Nothing
        Dim pFeatSelection As IFeatureSelection
        Dim pQueryFilter As IQueryFilter
        Dim pLayerSelSet As ISelectionSet
        Dim pLayerCursor As IFeatureCursor = Nothing
        pMxDoc = p_App.Document ' ThisDocument
        pMap = pMxDoc.FocusMap
        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = p_Layer_0 Then
                pLayerOne = pMap.Layer(A) 'Layer de Dpto
                aFound = True : Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("No existe el Layer: " & p_Layer_0, MsgBoxStyle.Information, "[BDGEOCATMIN]") : Exit Sub
        End If
        pFeatSelection = pLayerOne
        ' Select interstates.
        pQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = p_Filtro_0.ToUpper
        pFeatSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        ' Create a feature cursor of selected interstates.
        pLayerSelSet = pFeatSelection.SelectionSet
        pLayerSelSet.Search(Nothing, False, pLayerCursor)
        ' Part 2: Select high-growth counties that intersect an interstate.
        Dim pLayer_Two As IFeatureLayer = Nothing
        'Dim pElement As IElement
        Dim pLayerSelection As IFeatureSelection
        Dim p_Ifeature As IFeature
        Dim pSpatialFilter As ISpatialFilter
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = p_Layer_1 Then
                pLayer_Two = pMxDoc.FocusMap.Layer(A) ' Catastro
                aFound = True : Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("No existe el Layer: " & p_Layer_1, MsgBoxStyle.Information, "[BDGEOCATMIN]")
            Exit Sub
        End If
        pLayerSelection = pLayer_Two
        ' Prepare a spatial filter.
        pSpatialFilter = New SpatialFilter
        pSpatialFilter.WhereClause = p_Filtro_1
        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects ' esriSpatialRelIntersects
        ' Step through each interstate and select counties.
        p_Ifeature = pLayerCursor.NextFeature

        Do Until p_Ifeature Is Nothing
            ' Define the geometry of the spatial filter.
            pSpatialFilter.Geometry = p_Ifeature.Shape
            ' Select counties and add them to the selection set.
            pLayerSelection.SelectFeatures(pSpatialFilter, esriSelectionResultEnum.esriSelectionResultAdd, False) ' esriSelectionResultAdd, False)
            p_Ifeature = pLayerCursor.NextFeature
        Loop
        'p_swSelection = pLayerSelection.SelectionSet.Count
        If pLayerSelection.SelectionSet.Count = 0 Then
            p_Existe.text = -1 : Exit Sub
        Else
            p_Existe.text = pLayerSelection.SelectionSet.Count
        End If
        ' Part 3: Draw all selected features and report number of counties selected.
        Dim pActiveView As IActiveView
        Dim pLayer_SelSet As ISelectionSet
        pActiveView = pMxDoc.FocusMap
        ' Draw all selected features.
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pLayer_SelSet = pLayerSelection.SelectionSet
        Dim pEnumGeom As IEnumGeometry
        Dim pEnumGeomBind As IEnumGeometryBind
        pEnumGeom = New EnumFeatureGeometry
        pEnumGeomBind = pEnumGeom
        pEnumGeomBind.BindGeometrySource(Nothing, pLayerSelSet)
        Dim pGeomFactory As IGeometryFactory
        pGeomFactory = New GeometryEnvironment
        Dim pGeom As IGeometry
        pGeom = pGeomFactory.CreateGeometryFromEnumerator(pEnumGeom)
        cls_Catastro.ClearLayerSelection(pLayerOne)
        pMxDoc.ActiveView.Extent = pGeom.Envelope
        Xmin.Text = pGeom.Envelope.XMin
        yMin.text = pGeom.Envelope.YMin
        xMax.text = pGeom.Envelope.XMax
        yMax.text = pGeom.Envelope.YMax
    End Sub




    Public Sub SpatialRef_Envelope(ByVal p_layer As String, ByVal p_App As IApplication)
        Dim pFeatureLayer As IFeatureLayer = Nothing
        Dim pGeoDataset As IGeoDataset
        Dim pSpatialRef As ISpatialReference
        Dim pEnvelope As IEnvelope
        Dim MinX, MaxX, MinY, MaxY As Double
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        ' Define the input geodataset.
        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = p_layer Then
                pFeatureLayer = pMap.Layer(A)
                aFound = True
                Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("No existe capa de Ubigeo")
            Exit Sub
        End If

        pGeoDataset = pFeatureLayer  'QI
        ' Derive the spatial reference and extent of the geodataset.
        pSpatialRef = pGeoDataset.SpatialReference
        pEnvelope = pGeoDataset.Extent
        ' Get minx, miny, maxx, and maxy.
        MinX = pEnvelope.XMin
        MinY = pEnvelope.YMin
        MaxX = pEnvelope.XMax
        MaxY = pEnvelope.YMax
        ' Report the geodataset information.
        MsgBox("The layer's spatial reference is: " & pSpatialRef.Name)
        MsgBox("Minimum X is: " & MinX & "   Minimum Y is: " & MinY & Chr(10) & _
        "Maximum X is: " & MaxX & "  Maximum Y is: " & MaxY)
    End Sub

    Function ShowUniqueValues_Zona_GDB(ByVal p_Table As String, ByVal sFieldName As String, ByVal p_Filtro As String)
        Dim pFeatureWorkspace As IFeatureWorkspace
        x = 0
        pWorkspaceFactory = New AccessWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_PathGDB, 0)
        Dim pTable As ITable
        pTable = pFeatureWorkspace.OpenTable(p_Table)
        Dim pQueryDef As IQueryDef
        Dim pRow As IRow
        Dim pCursor As ICursor
        Dim pDataset As IDataset
        pDataset = pTable
        pFeatureWorkspace = pDataset.Workspace
        pQueryDef = pFeatureWorkspace.CreateQueryDef
        With pQueryDef
            .Tables = pDataset.Name ' Fully qualified table name
            .WhereClause = p_Filtro
            .SubFields = "DISTINCT(" & sFieldName & ")"
            pCursor = .Evaluate
        End With
        Dim lostr_Zona As String = ""
        pRow = pCursor.NextRow
        Dim lodtRegistros As New DataTable
        Do Until pRow Is Nothing
            If x = 0 Then
                lodtRegistros.Columns.Add("CODIGO")
                lodtRegistros.Columns.Add("DESCRIPCION")
                Dim dr1 As DataRow
                dr1 = lodtRegistros.NewRow
                Try
                    dr1.Item("CODIGO") = ""
                    dr1.Item("DESCRIPCION") = " --Seleccionar-- "
                Catch ex As Exception
                End Try
                lodtRegistros.Rows.Add(dr1)
                x = 1
            End If
            Dim dr As DataRow
            dr = lodtRegistros.NewRow
            dr.Item(0) = pRow.Value(0)
            dr.Item(1) = pRow.Value(0)
            lodtRegistros.Rows.Add(dr)
            pRow = pCursor.NextRow
        Loop
        Return lodtRegistros
    End Function
    Function ShowUniqueValues_Zona_SDE(ByVal p_Table As String, ByVal sFieldName As String, ByVal p_Filtro As String, ByVal p_App As IApplication)
        Dim x As Integer = 0
        cls_Catastro.Conexion_SDE(p_App)
        pTable = pFeatureWorkspace.OpenTable(p_Table)
        Dim pQueryDef As IQueryDef
        Dim pRow As IRow
        Dim pCursor As ICursor
        Dim pDataset As IDataset
        pDataset = pTable
        pFeatureWorkspace = pDataset.Workspace
        pQueryDef = pFeatureWorkspace.CreateQueryDef
        With pQueryDef
            .Tables = pDataset.Name ' Fully qualified table name
            .WhereClause = p_Filtro
            .SubFields = "DISTINCT(" & sFieldName & ")"
            pCursor = .Evaluate
        End With
        Dim lostr_Zona As String = ""
        pRow = pCursor.NextRow
        Dim lodtRegistros As New DataTable
        Do Until pRow Is Nothing
            If x = 0 Then
                lodtRegistros.Columns.Add("CODIGO")
                lodtRegistros.Columns.Add("DESCRIPCION")
                Dim dr1 As DataRow
                dr1 = lodtRegistros.NewRow
                Try
                    dr1.Item("CODIGO") = ""
                    dr1.Item("DESCRIPCION") = " --Seleccionar-- "
                Catch ex As Exception
                End Try
                lodtRegistros.Rows.Add(dr1)
                x = 1
            End If
            Dim dr As DataRow
            dr = lodtRegistros.NewRow
            dr.Item(0) = pRow.Value(0)
            dr.Item(1) = pRow.Value(0)
            lodtRegistros.Rows.Add(dr)
            pRow = pCursor.NextRow
        Loop
        Return lodtRegistros
    End Function

    Function ShowUniqueValues_Ubigeo(ByVal p_Table As String, ByVal p_Filtro As String)
        Dim pFeatureWorkspace As IFeatureWorkspace
        Dim lodtRegistros As New DataTable
        x = 0
        pWorkspaceFactory = New AccessWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_PathGDB, 0)
        Dim pTable As ITable
        pTable = pFeatureWorkspace.OpenTable(p_Table)
        '****************
        'cls_Catastro.Conexion_SDE(p_App)
        'pTable = pFeatureWorkspace.OpenTable("DBF_DISTRITO_CATASTRO")
        '*******
        Dim pQueryDef As IQueryDef
        Dim pRow As IRow
        Dim pCursor As ICursor
        Dim pDataset As IDataset
        pDataset = pTable
        pFeatureWorkspace = pDataset.Workspace
        pQueryDef = pFeatureWorkspace.CreateQueryDef
        Try
            Select Case Mid(p_Filtro, 1, 7)
                Case "NM_DEPA"
                    With pQueryDef
                        .Tables = pDataset.Name ' Fully qualified table name
                        .WhereClause = p_Filtro & " AND MID(CD_DIST,3,4) = '0101'" & " ORDER BY CD_DEPA"
                        '.SubFields = "DISTINCT(" & "NM_DEPA,NM_PROV,NM_DIST" & ")"
                        pCursor = .Evaluate
                    End With
                Case "NM_PROV"
                    With pQueryDef
                        .Tables = pDataset.Name ' Fully qualified table name
                        .WhereClause = p_Filtro & " ORDER BY CD_PROV" '& " AND MID(CD_DIST,3,4) = '0101'"
                        '.SubFields = "DISTINCT(" & sFieldName & ")"
                        pCursor = .Evaluate
                    End With
                Case "NM_DIST"
                    With pQueryDef
                        .Tables = pDataset.Name ' Fully qualified table name
                        .WhereClause = p_Filtro & " ORDER BY CD_DIST"
                        '.SubFields = "DISTINCT(" & sFieldName & ")"
                        pCursor = .Evaluate
                    End With
            End Select
            Dim lostr_Zona As String = ""
            pRow = pCursor.NextRow
            If x = 0 Then
                lodtRegistros.Columns.Add("CODIGO") '0
                lodtRegistros.Columns.Add("DPTO") '3 UBIGEO 7
                lodtRegistros.Columns.Add("PROV") '2 UBIGEO 6
                lodtRegistros.Columns.Add("DIST") '1 UBIGEO 5
                lodtRegistros.Columns.Add("CAP_DIST") '4
                lodtRegistros.Columns.Add("ZONA") '8
                lodtRegistros.Columns.Add("UBIGEO") '8
                x = 1
            End If
            Dim lo_cd_Prov As String = ""
            Dim lo_cd_Prov1 As String = ""
            Do Until pRow Is Nothing
                Select Case Mid(p_Filtro, 1, 7)
                    Case "NM_DEPA"
                        lo_cd_Prov1 = pRow.Value(7)
                    Case "NM_PROV"
                        lo_cd_Prov1 = pRow.Value(6)
                    Case "NM_DIST"
                        lo_cd_Prov1 = pRow.Value(5)
                End Select
                If lo_cd_Prov <> lo_cd_Prov1 Then
                    Dim dr As DataRow
                    dr = lodtRegistros.NewRow
                    dr.Item(0) = x
                    dr.Item(1) = pRow.Value(3)
                    dr.Item(2) = pRow.Value(2)
                    dr.Item(3) = pRow.Value(1)
                    dr.Item(4) = pRow.Value(4)
                    dr.Item(5) = pRow.Value(8)
                    lo_cd_Prov = lo_cd_Prov1
                    dr.Item(6) = lo_cd_Prov1
                    lodtRegistros.Rows.Add(dr)
                    x = x + 1
                End If
                pRow = pCursor.NextRow
            Loop
        Catch ex As Exception
        End Try
        Dim lodtOrdenado As New DataTable
        Dim lodtvUbigeo As New DataView(lodtRegistros, Nothing, "DPTO ASC", DataViewRowState.CurrentRows)
        For i As Integer = 0 To lodtvUbigeo.Count - 1
            If i = 0 Then
                lodtOrdenado.Columns.Add("CODIGO") '0
                lodtOrdenado.Columns.Add("DPTO") '3 UBIGEO 7
                lodtOrdenado.Columns.Add("PROV") '2 UBIGEO 6
                lodtOrdenado.Columns.Add("DIST") '1 UBIGEO 5
                lodtOrdenado.Columns.Add("CAP_DIST") '4
                lodtOrdenado.Columns.Add("ZONA") '8
                lodtOrdenado.Columns.Add("UBIGEO") '8
            End If
            Dim dr As DataRow
            dr = lodtOrdenado.NewRow
            dr.Item(0) = i + 1 'lodtvUbigeo.Item(i).Row(0)
            dr.Item(1) = lodtvUbigeo.Item(i).Row(1)
            dr.Item(2) = lodtvUbigeo.Item(i).Row(2)
            dr.Item(3) = lodtvUbigeo.Item(i).Row(3)
            dr.Item(4) = lodtvUbigeo.Item(i).Row(4)
            dr.Item(5) = lodtvUbigeo.Item(i).Row(5)
            dr.Item(6) = lodtvUbigeo.Item(i).Row(6)
            lodtOrdenado.Rows.Add(dr)
        Next
        Return lodtOrdenado
        'CboZona.DataSource = lodtvZona
        'CboZona.DisplayMember = "DESCRIPCION"
        'CboZona.ValueMember = "CODIGO"
    End Function
    Function ShowUniqueValues_Ubigeo_SDE(ByVal p_Table As String, ByVal p_Filtro As String, ByVal p_App As IApplication)
        Dim lodtRegistros As New DataTable 'xxxxxx
        Dim pTable As ITable
        cls_Catastro.Conexion_SDE(p_App)
        pTable = pFeatureWorkspace.OpenTable(p_Table)
        Dim pQueryDef As IQueryDef
        Dim pRow As IRow
        Dim pCursor As ICursor
        Dim pDataset As IDataset
        pDataset = pTable
        pFeatureWorkspace = pDataset.Workspace
        pQueryDef = pFeatureWorkspace.CreateQueryDef
        Try
            Select Case Mid(p_Filtro, 1, 7)
                Case "NM_DEPA"
                    With pQueryDef
                        .Tables = pDataset.Name ' Fully qualified table name
                        .WhereClause = p_Filtro & " AND SUBSTR(CD_DIST,3,4) = '0101'" '& " ORDER BY CD_DEPA"
                        pCursor = .Evaluate
                    End With
                Case "NM_PROV"
                    With pQueryDef
                        .Tables = pDataset.Name ' Fully qualified table name
                        .WhereClause = p_Filtro '& " ORDER BY CD_PROV" '& " AND MID(CD_DIST,3,4) = '0101'"
                        pCursor = .Evaluate
                    End With
                Case "NM_DIST"
                    With pQueryDef
                        .Tables = pDataset.Name ' Fully qualified table name
                        .WhereClause = p_Filtro ' & " ORDER BY CD_DIST"
                        pCursor = .Evaluate
                    End With
            End Select
            Dim lostr_Zona As String = ""
            pRow = pCursor.NextRow
            lodtRegistros.Columns.Add("CODIGO", Type.GetType("System.Double")) '0
            lodtRegistros.Columns.Add("DPTO", Type.GetType("System.String")) '3 UBIGEO 7
            lodtRegistros.Columns.Add("PROV", Type.GetType("System.String")) '2 UBIGEO 6
            lodtRegistros.Columns.Add("DIST", Type.GetType("System.String")) '1 UBIGEO 5
            lodtRegistros.Columns.Add("CAP_DIST", Type.GetType("System.String")) '4
            lodtRegistros.Columns.Add("ZONA", Type.GetType("System.Double")) '8
            lodtRegistros.Columns.Add("UBIGEO", Type.GetType("System.String")) '8
            x = 1
            Dim lo_cd_Prov As String = ""
            Dim lo_cd_Prov1 As String = ""
            Do Until pRow Is Nothing
                Select Case Mid(p_Filtro, 1, 7)
                    Case "NM_DEPA"
                        lo_cd_Prov1 = pRow.Value(7)
                    Case "NM_PROV"
                        lo_cd_Prov1 = pRow.Value(6)
                    Case "NM_DIST"
                        lo_cd_Prov1 = pRow.Value(5)
                End Select
                If lo_cd_Prov <> lo_cd_Prov1 Then
                    Dim dr As DataRow
                    dr = lodtRegistros.NewRow
                    dr.Item(0) = x
                    dr.Item(1) = pRow.Value(3)
                    dr.Item(2) = pRow.Value(2)
                    dr.Item(3) = pRow.Value(1)
                    dr.Item(4) = pRow.Value(4)
                    dr.Item(5) = pRow.Value(8)
                    lo_cd_Prov = lo_cd_Prov1
                    dr.Item(6) = lo_cd_Prov1
                    lodtRegistros.Rows.Add(dr)
                    x = x + 1
                End If
                pRow = pCursor.NextRow
            Loop
        Catch ex As Exception
        End Try
        Dim lodtOrdenado As New DataTable
        Dim lodtvUbigeo As New DataView(lodtRegistros, Nothing, "DPTO ASC", DataViewRowState.CurrentRows)
        For i As Integer = 0 To lodtvUbigeo.Count - 1
            If i = 0 Then
                lodtOrdenado.Columns.Add("CODIGO", Type.GetType("System.Double")) '0
                lodtOrdenado.Columns.Add("DPTO", Type.GetType("System.String")) '3 UBIGEO 7
                lodtOrdenado.Columns.Add("PROV", Type.GetType("System.String")) '2 UBIGEO 6
                lodtOrdenado.Columns.Add("DIST", Type.GetType("System.String")) '1 UBIGEO 5
                lodtOrdenado.Columns.Add("CAP_DIST", Type.GetType("System.String")) '4
                lodtOrdenado.Columns.Add("ZONA", Type.GetType("System.Double")) '8
                lodtOrdenado.Columns.Add("UBIGEO", Type.GetType("System.String")) '8
            End If
            Dim dr As DataRow
            dr = lodtOrdenado.NewRow
            dr.Item(0) = i + 1 'lodtvUbigeo.Item(i).Row(0)
            dr.Item(1) = lodtvUbigeo.Item(i).Row(1)
            dr.Item(2) = lodtvUbigeo.Item(i).Row(2)
            dr.Item(3) = lodtvUbigeo.Item(i).Row(3)
            dr.Item(4) = lodtvUbigeo.Item(i).Row(4)
            dr.Item(5) = lodtvUbigeo.Item(i).Row(5)
            dr.Item(6) = lodtvUbigeo.Item(i).Row(6)
            lodtOrdenado.Rows.Add(dr)
        Next
        Return lodtOrdenado
    End Function

    Public Function Get_Unique_Values_FC(ByVal p_Layer As String, ByVal p_Campo As String, ByVal p_App As IApplication, ByVal tipo_opcion As String)
        Dim pCursor As ICursor
        Dim pRow As IRow
        'pMxDoc = p_App.Document
        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = p_Layer Then
                pFeatureLayer = pMap.Layer(A)
                aFound = True : Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("No existe Layer de Catastro", MsgBoxStyle.Information, "[BDGeocientífica]")
            Return ""
            Exit Function
        End If

        If tipo_opcion = "1" Then
            Dim lo_carta As String = ""
            Dim lo_cartas As String = ""
            lo_cartas = v_codigo

        Else
            pTable = pFeatureLayer.FeatureClass
            Dim pQueryDef As IQueryDef
            Dim pDataset As IDataset = pTable
            pFeatureWorkspace = pDataset.Workspace
            pQueryDef = pFeatureWorkspace.CreateQueryDef
            With pQueryDef
                .Tables = pDataset.Name
                .SubFields = p_Campo
                pCursor = .Evaluate
            End With
            Dim lostr_Zona As String = ""
            pRow = pCursor.NextRow
            Dim lodtRegistros As New DataTable
            Dim lo_carta As String = ""
            Dim lo_cartas As String = ""
            Dim lo_Flag As Integer = 0
            Do Until pRow Is Nothing
                Select Case lo_Flag
                    Case 0
                        lo_carta = pRow.Value(pRow.Fields.FindField(p_Campo))
                        lo_cartas = lo_cartas & lo_carta & ", "
                        lo_Flag = 1
                    Case 1
                        lo_carta = pRow.Value(pRow.Fields.FindField(p_Campo))
                        If InStr(lo_cartas, lo_carta) = 0 Then
                            lo_cartas = lo_cartas & lo_carta & ", "
                        End If
                End Select
                pRow = pCursor.NextRow
            Loop


            'lo_cartas = Mid(lo_cartas, 1, Len(lo_cartas) - 2)
            Return Mid(lo_cartas, 1, Len(lo_cartas) - 2)
        End If

    End Function

    Private Function GetUniqueValues(ByVal pTable As ITable, ByVal fieldName As String) As ICursor
        Dim pFilter As IQueryFilter
        pFilter = New QueryFilter
        pFilter.SubFields = "DISTINCT (" + fieldName + ")"
        GetUniqueValues = pTable.Search(pFilter, False)
    End Function
    Public Sub AddImagen(ByVal sFullPath As String, ByVal loTipo As String)
        Try
            'Dim pRasterLayer As RasterLayer
            Dim Raster As IRasterLayer
            pMxDoc = m_application.Document
            pMap = pMxDoc.FocusMap
            Raster = New RasterLayer
            Raster.CreateFromFilePath(sFullPath)
            Dim loImagen As String = Mid(sFullPath, InStr(sFullPath, ".") - 3, 3)
            If loTipo = "1" Then
                Raster.Name = "Ima_IGN_" & loImagen
            ElseIf loTipo = "2" Then
                Raster.Name = "Ima_SAT" & loImagen
            End If
            Dim pLayerEffects As ILayerEffects
            pLayerEffects = Raster
            If pLayerEffects.SupportsTransparency Then pLayerEffects.Transparency = 0
            Raster.Visible = False
            pMap.AddLayer(Raster)
            pMxDoc.UpdateContents()
            pMxDoc.ActiveView.Refresh()
        Catch ex As Exception
            'MsgBox("ERROR IMAGEN")
        End Try
    End Sub
    Public Sub ZoomToDomainExtent(ByVal p_App As IApplication)
        Dim pActiveView As IActiveView
        Dim pContentsView As IContentsView
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        pContentsView = pMxDoc.CurrentContentsView

        Dim pLayer As ILayer
        'Dim pDataSet As IDataset
        Dim pFeatureLayer As IFeatureLayer
        Dim pGeoDataset As IGeoDataset

        If Not TypeOf pContentsView.SelectedItem Is ILayer Then Exit Sub
        pLayer = pContentsView.SelectedItem
        pFeatureLayer = pLayer
        pGeoDataset = pFeatureLayer

        Dim pSpatialReference As ISpatialReference
        Dim pEnvelope As IEnvelope
        pSpatialReference = pGeoDataset.SpatialReference
        pEnvelope = New Envelope

        Dim dXmax As Double
        Dim dYmax As Double
        Dim dXmin As Double
        Dim dYmin As Double

        pSpatialReference.GetDomain(dXmin, dXmax, dYmin, dYmax)
        pEnvelope.XMax = dXmax
        pEnvelope.YMax = dYmax
        pEnvelope.XMin = dXmin
        pEnvelope.YMin = dYmin
        pActiveView.Extent = pEnvelope
        pActiveView.Refresh()
    End Sub
    Public Sub CreateShapeFileFields(ByRef fldsEdit As IFieldsEdit, ByVal strFieldName As String, ByVal ftFieldType As esriFieldType, _
                                        ByVal iLength As Short, ByVal iPrecision As Short, ByVal iScale As Short, ByVal p_App As IApplication)
        Dim pField As IField
        Dim pFieldEdit As IFieldEdit
        pField = New Field
        pFieldEdit = pField
        pFieldEdit.Name_2 = strFieldName
        pFieldEdit.Type_2 = ftFieldType
        '***********
        'Dim pMxDoc As IMxDocument
        pMxDoc = p_App.Document
        '***********
        Select Case ftFieldType
            Case esriFieldType.esriFieldTypeOID
                pFieldEdit.IsNullable_2 = False
                pFieldEdit.AliasName_2 = "Object OID"
            Case esriFieldType.esriFieldTypeGeometry
                Dim pGeomDef As IGeometryDef
                Dim pGeomDefEdit As IGeometryDefEdit
                pGeomDef = New GeometryDef
                pGeomDefEdit = pGeomDef
                pGeomDefEdit.GeometryType_2 = ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon
                pGeomDefEdit.SpatialReference_2 = pMxDoc.FocusMap.SpatialReference
                pFieldEdit.GeometryDef_2 = pGeomDef
            Case esriFieldType.esriFieldTypeDouble
                pFieldEdit.Length_2 = iLength
                pFieldEdit.Precision_2 = iPrecision
                pFieldEdit.Scale_2 = iScale
            Case Else
                pFieldEdit.Length_2 = iLength
        End Select
        fldsEdit.AddField(pField)
    End Sub
    Public Sub AddFieldDM(ByVal p_Shapefile As String)
        Dim pGP As Object
        pGP = CreateObject("esriGeoprocessing.GPDispatch.1")
        Try
            pGP.Workspace = glo_pathTMP
            ' note: the inputs can also be objects; however the output must be a string.
            'pGP.buffer_analysis(filePath + "\" + inputName, "D:\temp\bufferfc1.shp", "1")
            pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "LEYENDA", "TEXT", "2")
            'Agregar mas campos para la evaluación
            pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "EVAL", "TEXT", "2")
            pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "CALCULO", "TEXT", "2")
            'pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "LEYENDA1", "TEXT", "20")
            pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "AREA_INT", "DOUBLE", "20", "4")
            pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "DPTO", "TEXT", "30")
            pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "PROV", "TEXT", "40")
            pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "DIST", "TEXT", "40")
            pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "CONTADOR", "TEXT", "20")
            pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "NUM_RESOL", "TEXT", "30")
            pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "FEC_RESOL", "TEXT", "12")
            pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "CALIF", "TEXT", "6")
            pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "DISTS", "TEXT", "256")
            pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "PROVS", "TEXT", "256")
            pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "DPTOS", "TEXT", "256")
            pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "TIPO", "TEXT", "80")

           
        Catch ex As Exception
            MsgBox(pGP.GetMessages(), vbOKOnly, "No se creo campo Leyenda")
        End Try

    End Sub
    Public Sub AddFieldDM1(ByVal p_Shapefile As String)


        Dim pGP As Object
        pGP = CreateObject("esriGeoprocessing.GPDispatch.1")
        Try
            pGP.Workspace = glo_pathTMP
            ' note: the inputs can also be objects; however the output must be a string.

            pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "NUM_RESOL", "TEXT", "30")
            pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "FEC_RESOL", "TEXT", "12")
            pGP.addfield(glo_pathTMP + "\" + p_Shapefile & ".shp", "CALIF", "TEXT", "6")

        Catch ex As Exception
            MsgBox(pGP.GetMessages(), vbOKOnly, "No se creo los campos")
        End Try

    End Sub
    Public Sub Test()
        Dim filePath As String
        filePath = glo_Path & "\Program Files\ArcGIS\Bin\TemplateData\USA"
        Dim inputName As String
        Dim clipName As String
        inputName = "cities.shp"
        Dim pGP As Object
        pGP = CreateObject("esriGeoprocessing.GPDispatch.1")
        On Error GoTo EH
        pGP.Workspace = filePath
        ' note: the inputs can also be objects; however the output must be a string.
        pGP.buffer_analysis(filePath + "\" + inputName, "D:\temp\bufferfc1.shp", "1")
        Exit Sub
EH:
        MsgBox(pGP.GetMessages(), vbOKOnly, "Test")
    End Sub

    Public Sub Carga_Data_Ubigeo(ByVal Coord_x As Double, ByVal Coord_y As Double, ByVal p_Zona As Integer, ByVal p_Layer As String, ByVal p_App As IApplication)
        Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
        pSpatialReferenceEnv = New SpatialReferenceEnvironment
        Dim temp As Double
        Dim pPoint As IPoint
        If Coord_x > Coord_y Then
            temp = Coord_x
            Coord_x = Coord_y
            Coord_y = temp
        End If
        pPoint = New Point
        pPoint.X = Coord_x
        pPoint.Y = Coord_y
        '    pSpatialRefFactory = New SpatialReferenceEnvironment
        '    Select Case p_Zona
        '        Case 17
        '            pSpatialRef = pSpatialRefFactory.CreateProjectedCoordinateSystem(32717) '("esriSRProjCS_WGS1984UTM_17S")
        '        Case 18
        '            pSpatialRef = pSpatialRefFactory.CreateProjectedCoordinateSystem(32718) '("esriSRProjCS_WGS1984UTM_18S")
        '        Case 19
        '            pSpatialRef = pSpatialRefFactory.CreateProjectedCoordinateSystem(32719) '("esriSRProjCS_WGS1984UTM_19S")
        '    End Select
        '    pUTMSR = pSpatialRefFactory.CreateGeographicCoordinateSystem(4326) '(esriSRGeoCS_WGS1984)
        '    pPoint.SpatialReference = pSpatialRef

        '    pFeatureDM.Shape.Project(PSAD17)

        pPoint.Project(Datum_PSAD_17)
        'End If
        Dim x_geo, y_geo As Double
        x_geo = pPoint.X
        y_geo = pPoint.Y
        Dim pFLayer As IFeatureLayer = Nothing
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        Dim afound As Boolean
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = p_Layer Then
                pFLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("Layer No Existe.")
            Exit Sub
        End If
        pPoint = pMxDoc.CurrentLocation
        pPoint.X = x_geo
        pPoint.Y = y_geo
        'Call Calcula_Geo_Grados(pPoint.X, pPoint.Y)
        Dim pSpatialFilter As ISpatialFilter
        pSpatialFilter = New SpatialFilter
        With pSpatialFilter
            .Geometry = pPoint
            .SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects ' esriSpatialRelIntersects
        End With
        Dim pFeatureCursor As IFeatureCursor
        pFeatureCursor = pFLayer.Search(pSpatialFilter, True)
        Dim pRow As IRow
        pRow = pFeatureCursor.NextFeature
        If pRow Is Nothing Then
            'Borra_Feature("GPO_MAPA_BASE")
            'MsgBox("Las coordenadas Este, Norte, ó la Zona están fuera del Límite")
            'logloEstadoLayer = "NO"
            Exit Sub
        End If
        Select Case p_Layer
            Case "Distrito"
                gostrDpto = pRow.Value(pRow.Fields.FindField("NM_DEPA"))
                gostrProv = pRow.Value(pRow.Fields.FindField("NM_PROV")) 'pRow.Value(4)
                gostrDist = pRow.Value(pRow.Fields.FindField("NM_DIST")) 'pRow.Value(3)
                gostrUbigeo = pRow.Value(7)
                'Case "GPO_QDR_CUADRANGULO"
            Case "GPO_QTE_CUADRANTE"
                gostrHoja = pRow.Value(2) & " (" & pRow.Value(3) & ")"
                gostrCod_IGN = pRow.Value(4)
                gostrCod_Franja = pRow.Value(5)
                gostrCod_QTE = pRow.Value(6)
            Case "GPO_CUE_CUENCAS"
                'lostrCuenca = UCase(Mid(pRow.Value(4), 1, 1)) & LCase(Mid(pRow.Value(4), 2))
            Case "GPO_MAPA_BASE"
                gostrHoja = pRow.Value(2) & " (" & pRow.Value(3) & ")"
                gostrCod_IGN = pRow.Value(4)
                gostrCod_Franja = pRow.Value(5)
                gostrCod_QTE = pRow.Value(6)
                gostrDpto = pRow.Value(9)
                gostrProv = pRow.Value(8)
                gostrDist = pRow.Value(7)
                gostrUbigeo = pRow.Value(12)
        End Select
    End Sub
    Public Sub PT_validarNumeros(ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal loNumEnteros As Int16, ByVal loNumDecimales As Int16, ByVal loStrControl As Windows.Forms.TextBox)
        '****** Permite validar el ingreso de Numeros (0-9, .), y tambien la longitud de lso Enteros y los Decimales
        If (Not Char.IsDigit(e.KeyChar)) Then
            ' de igual forma se podría comprobar si es caracter: e.KeyChar.IsLetter
            ' si es un caracter minusculas: e.KeyChar.IsLower ...etc
            If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) _
                Or (Microsoft.VisualBasic.Asc(e.KeyChar) = 46) Then
                'e.Handled = False ' esto invalida la tecla pulsada
                ' verifica que no ingresen mas de un punto decimal
                If FC_ContarCaracteresDuplicados(loStrControl.Text & e.KeyChar, ".") > 1 Then
                    e.KeyChar = ""
                End If
                e.Handled = False ' esto invalida la tecla pulsada
            Else
                e.KeyChar = ""
                e.Handled = False ' esto invalida la tecla pulsada
            End If
        Else
            ' Compara los Enteros
            If Len(Fix(Val(loStrControl.Text & e.KeyChar)).ToString) > loNumEnteros Then
                e.KeyChar = ""
                e.Handled = False ' esto invalida la tecla pulsada
            End If
            ' Compara los Decimales
            If Len(Mid(loStrControl.Text, Len(Fix(Val(loStrControl.Text & e.KeyChar))).ToString)) > loNumDecimales Then
                e.KeyChar = ""
                e.Handled = False ' esto invalida la tecla pulsada
            End If
        End If

    End Sub
    Public Function FC_ContarCaracteresDuplicados(ByVal cadena As String, ByVal caracter As Char) As Integer
        Dim n As Integer
        Dim contador As Integer = 0
        For n = 0 To Len(cadena) - 1
            If cadena.Chars(n) = caracter Then
                contador = contador + 1
            End If
        Next n
        Return contador
    End Function
    Public Sub Crear_Shapefile(ByVal p_App As IApplication)
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        Dim fclas_tema As IFeatureClass 'Clase del tema
        'Dim pSpatialRefFactory As ISpatialReferenceFactory
        'Dim carpeta_t As IFeatureWorkspace
        Dim campos As IFields
        Dim camposEdit As IFieldsEdit
        Dim campo As IField
        Dim campoEdit As IFieldEdit
        cls_Catastro.Conexion_Shapefile()
        campos = New Fields
        camposEdit = campos
        'Generando los campos
        campo = New Field
        campoEdit = campo
        campoEdit.Name_2 = "Shape"
        campoEdit.Type_2 = esriFieldType.esriFieldTypeGeometry ' esriFieldTypeGeometry


        Dim pGeomDef As IGeometryDef
        Dim pGeomDefEdit As IGeometryDefEdit
        pGeomDef = New GeometryDef
        pGeomDefEdit = pGeomDef
        With pGeomDefEdit
            .GeometryType_2 = esriGeometryType.esriGeometryPolygon ' esriGeometryPolygon
            .SpatialReference_2 = Datum_PSAD_17 'pMap.SpatialReference 'New UnknownCoordinateSystem
        End With
        campoEdit.GeometryDef_2 = pGeomDef
        camposEdit.AddField(campo)
        'Nombre del tema
        fclas_tema = pFeatureWorkspace.CreateFeatureClass("XXXX", campos, Nothing, Nothing, esriFeatureType.esriFTSimple, "Shape", "")
    End Sub

    Public Sub Create_Shapefile_DM(ByVal p_Path As String, ByVal p_Name_Shapefile As String, ByVal p_Zona As String, ByVal tipo As String)
        Const strShapeFieldName As String = "Shape"
        ' Open the folder to contain the shapefile as a workspace 
        Dim pFWS As IFeatureWorkspace
        Dim pWorkspaceFactory As IWorkspaceFactory
        pWorkspaceFactory = New ShapefileWorkspaceFactory
        pFWS = pWorkspaceFactory.OpenFromFile(p_Path, 0)
        ' Set up a simple fields collection 
        Dim pFields As IFields
        Dim pFieldsEdit As IFieldsEdit
        pFields = New ESRI.ArcGIS.Geodatabase.Fields ' esriGeoDatabase.Fields 
        pFieldsEdit = pFields
        Dim pField As IField
        Dim pFieldEdit As IFieldEdit
        ' Make the shape field 
        ' it will need a geometry definition, with a spatial reference 
        pField = New ESRI.ArcGIS.Geodatabase.Field
        pFieldEdit = pField
        pFieldEdit.Name_2 = strShapeFieldName
        pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry
        ' esriFieldTypeGeometry 
        Dim pGeomDef As IGeometryDef
        Dim pGeomDefEdit As IGeometryDefEdit
        pGeomDef = New GeometryDef
        pGeomDefEdit = pGeomDef
        Select Case Mid(p_Name_Shapefile, 1, 2)
            Case "F_"
                Datum_PSAD = Datum_PSAD_18
            Case Else
                Select Case p_Zona
                    Case 17
                        Datum_PSAD = Datum_PSAD_17
                    Case 18
                        Datum_PSAD = Datum_PSAD_18
                    Case 19
                        Datum_PSAD = Datum_PSAD_19
                End Select
        End Select
        With pGeomDefEdit
            If tipo = "Poligono" Then
                .GeometryType_2 = esriGeometryType.esriGeometryPolygon
            ElseIf tipo = "Polylinea" Then
                .GeometryType_2 = esriGeometryType.esriGeometryPolyline
            ElseIf tipo = "Point" Then
                .GeometryType_2 = esriGeometryType.esriGeometryPoint
            End If

            .SpatialReference_2 = Datum_PSAD
        End With
        pFieldEdit.GeometryDef_2 = pGeomDef
        pFieldsEdit.AddField(pField)
        ' Add another miscellaneous text field 
        pField = New ESRI.ArcGIS.Geodatabase.Field
        pFieldEdit = pField
        With pFieldEdit
            .Length_2 = 10
            '.Name_2 = "Codigo"
            .Name_2 = "Contador"
            .Type_2 = esriFieldType.esriFieldTypeString
        End With
        pFieldsEdit.AddField(pField)
        pField = New ESRI.ArcGIS.Geodatabase.Field
        pFieldEdit = pField
        With pFieldEdit
            .Length_2 = 25
            '.Name_2 = "Name"
            .Name_2 = "Codigo"
            .Type_2 = esriFieldType.esriFieldTypeString
        End With
        pFieldsEdit.AddField(pField)
        ' Create the shapefile 
        ' (some parameters apply to geodatabase options and can be defaulted as Nothing) 
        Dim pFeatClass As IFeatureClass
        pFeatClass = pFWS.CreateFeatureClass(p_Name_Shapefile, pFields, Nothing, _
        Nothing, esriFeatureType.esriFTSimple, strShapeFieldName, "")
    End Sub

    Public Sub Zoom_to_Layer(ByVal p_Layer As String, ByVal p_App As IApplication)
        'Programa para obtener Limite extremos del DM
        Dim pFeatureCursor As IFeatureCursor
        Dim pfeature As IFeature
        Dim pgeometria As IGeometry
        Dim pEnvelope As IEnvelope
        Dim pActiveView As IActiveView
        Dim pFeatLayer As IFeatureLayer = Nothing
        'Dim pFeatureLayerD As IFeatureLayerDefinition
        'pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        Dim afound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = p_Layer Then
                pFeatLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("Layer No Existe.")
            Exit Sub
        End If
        pFeatureCursor = pFeatLayer.Search(Nothing, False)
        pfeature = pFeatureCursor.NextFeature
        pgeometria = pfeature.Shape
        pEnvelope = pgeometria.Envelope
        Dim v_este_min1 As Double
        Dim v_este_max1 As Double
        Dim v_norte_min1 As Double
        Dim v_norte_max1 As Double
        v_este_min1 = pEnvelope.XMin
        v_este_max1 = pEnvelope.XMax
        v_norte_min1 = pEnvelope.YMin
        v_norte_max1 = pEnvelope.YMax
        v_este_min = v_este_min1 - 500
        v_este_max = v_este_max1 + 500
        v_norte_min = v_norte_min1 - 500
        v_norte_max = v_norte_max1 + 500
        cls_Catastro.HazZoom(v_este_min, v_norte_min, v_este_max, v_norte_max, 0, p_App)
    End Sub

    Public Sub Check_Data(ByVal p_Layer As String, ByVal p_App As IApplication)
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        Dim pFLayer As IFeatureLayer

        pFLayer = pMap.Layer(0)

        Dim pFClass As IFeatureClass
        pFClass = pFLayer.FeatureClass
        Dim pFCursor As IFeatureCursor
        pFCursor = pFClass.Update(Nothing, True)
        Dim pFeature As IFeature
        pFeature = pFCursor.NextFeature
        Dim pFSel As IFeatureSelection
        pFSel = pFLayer
        Dim pSelSet As ISelectionSet
        Select Case pFSel.SelectionSet.Count
            Case 0
                MsgBox("No hay ninguna Selección ")
                Exit Sub
        End Select
        pSelSet = pFSel.SelectionSet
        Dim pQFilter As IQueryFilter
        pQFilter = New QueryFilter
        pQFilter.WhereClause = ""
        Dim pFeatureCursor As IFeatureCursor = Nothing
        pSelSet.Search(pQFilter, False, pFeatureCursor)
        pFeature = pFeatureCursor.NextFeature
        While Not pFeature Is Nothing
            MsgBox(pFeature.Value(28))
            pFeature = pFeatureCursor.NextFeature
        End While

    End Sub

End Class
