
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.Display
Imports stdole
Imports System.Windows.Forms
Imports Oracle.DataAccess.Client
Imports Microsoft.Office.Interop
Imports PORTAL_Clases
Public Class cls_excel
    Public m_application As IApplication
    Private Structure VerXY
        Dim sCodigoID As String
        Dim sEsteX As String
        Dim sNorteY As String
        Dim iEsteX As Integer
        Dim iNorteY As Integer
        Dim iCodigoID As Integer
    End Structure
    Private DataXY As VerXY
    Private Structure FieldData
        Dim Index As Integer
        Dim Campo As String
    End Structure
    Structure Punto_DM
        Dim v As Integer
        Dim x As Double
        Dim y As Double
    End Structure
    Private DataInfo() As FieldData
    Public pRings(40) As IRing
    Public pPolygon As IPolygon
    Public SubPolygono As Integer

    Public Datum As ISpatialReference

    Dim cls_catastro As New cls_DM_1

    Dim cls_planos As New Cls_planos

    Public Sub CreateShapefile(ByVal p_Path As String, ByVal p_Name_Shapefile As String, ByVal p_Datum As String)
        Dim pFWS As IFeatureWorkspace
        Dim pWorkspaceFactory As IWorkspaceFactory
        Try
            Const strShapeFieldName As String = "Shape"
            ' Open the folder to contain the shapefile as a workspace 
            pWorkspaceFactory = New ShapefileWorkspaceFactory
            pFWS = pWorkspaceFactory.OpenFromFile(p_Path, 0)
            ' Set up a simple fields collection 
            Dim pFields As IFields
            Dim pFieldsEdit As IFieldsEdit
            pFields = New Fields
            pFieldsEdit = pFields
            Dim pField As IField
            Dim pFieldEdit As IFieldEdit
            ' Make the shape field 
            pField = New Field
            pFieldEdit = pField
            pFieldEdit.Name_2 = strShapeFieldName
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry
            ' esriFieldTypeGeometry 
            Dim pGeomDef As IGeometryDef
            Dim pGeomDefEdit As IGeometryDefEdit
            pGeomDef = New GeometryDef
            pGeomDefEdit = pGeomDef
            Select Case p_Datum
                Case "UTM WGS84 17S"
                    Datum = Datum_PSAD_17
                Case "UTM WGS84 18S"
                    Datum = Datum_PSAD_18

                Case "UTM WGS84 19S"
                    Datum = Datum_PSAD_19

            End Select
            With pGeomDefEdit
                ' Select Case p_Geometria
                '    Case "POLIGONO"
                pGeomDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon
                '   Case "LINEA"
                '.GeometryType_2 = esriGeometryType.esriGeometryPolyline
                '    Case "PUNTO"
                '.GeometryType_2 = esriGeometryType.esriGeometryPoint
                'End Select
                .SpatialReference_2 = Datum
            End With
            pFieldEdit.GeometryDef_2 = pGeomDef
            pFieldsEdit.AddField(pField)
            'Adicionando los campos
            pField = New ESRI.ArcGIS.Geodatabase.Field
            pField = New Field
            pFieldEdit = pField
            With pFieldEdit
                .Length_2 = 20
                .Name_2 = "CODIGO"
                .Type_2 = esriFieldType.esriFieldTypeString
            End With
            pFieldsEdit.AddField(pField)
            ' Create the shapefile 
            ' (some parameters apply to geodatabase options and can be defaulted as Nothing) 
            Dim pFeatClass As IFeatureClass
            pFeatClass = pFWS.CreateFeatureClass(p_Name_Shapefile, pFields, Nothing, _
            Nothing, esriFeatureType.esriFTSimple, strShapeFieldName, "")
        Catch ex As Exception
        End Try
        pFWS = Nothing
    End Sub

    Public Function ActivarEditor(ByVal m_Application As IMxApplication, ByVal pFCLass As IFeatureClass) As IEditor
        Dim pWorkspace As IWorkspace
        Dim pEditor As IEditor
        Dim pID As New UID
        Dim pDataset As IDataset
        Try
            pID.Value = "esriCore.Editor"
            pEditor = m_Application.FindExtensionByCLSID(pID)
            pDataset = pFCLass
            pWorkspace = pDataset.Workspace
            pEditor.StartEditing(pWorkspace)
            pEditor.StartOperation()
            ActivarEditor = pEditor
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub Procesa_datosexcel(ByVal p_ShapeFile As String, ByVal lodtbDatos As DataTable, ByVal m_application As IApplication)

        'Programar para leer datos del excel y graficarlo masivamente
        '----------------------------------------------------------------
        Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
        pSpatialReferenceEnv = New SpatialReferenceEnvironment
        Dim pTipo As String = ""
        Dim vCodigo, vCodigoSalvado As String
        Dim vzoncat As String = ""

        Try

            Dim contador As Integer = 1
            SetParam(lodtbDatos)
            Dim FieldDataCount As Integer = lodtbDatos.Columns.Count ' - 4
            ' ReDim DataInfo(FieldDataCount)
            Dim n As Integer = 0
            Dim bFound As Boolean = False
            Dim vCodigo1 As String = ""

            contador = contador + 1
            vCodigoSalvado = lodtbDatos.Rows(0).Item(DataXY.iCodigoID)

            Dim cls_Oracle As New cls_Oracle
            Dim CUENTA As Integer

            Dim lodbListaDM As New DataTable

            Dim listadm As New DataTable

            listadm.Columns.Add("CODIGO", Type.GetType("System.String"))

            Dim dRow1 As DataRow


            For j As Integer = 0 To lodtbDatos.Rows.Count - 1
                dRow1 = listadm.NewRow

                Try
                    v_codigo = lodtbDatos.Rows(j).Item(DataXY.iCodigoID)

                    If v_codigo <> "" Then
                        dRow1.Item("CODIGO") = v_codigo
                        listadm.Rows.Add(v_codigo)
                        dRow1 = lodtbDatos.NewRow
                    End If
                Catch ex As Exception

                End Try
            Next


            For i As Integer = 0 To lodtbDatos.Rows.Count - 1
                v_codigo = lodtbDatos.Rows(i).Item(DataXY.iCodigoID)
                CUENTA = CUENTA + 1
                lodbListaDM = cls_Oracle.F_OBTIENE_DM_UNIQUE(v_codigo, 1)
                v_zona_dm = lodbListaDM.Rows(0).Item("ZONA").ToString
                v_vigcat = lodbListaDM.Rows(0).Item("PE_VIGCAT").ToString


                m_application.Caption = "PROCESO  MASIVO DE EVALUACION DE DERECHOS MINEROS :  " & v_codigo & "  " & CUENTA.ToString & "...DE " & listadm.Rows.Count
                cls_catastro.Consulta_Evaluacion_DM_masivo(m_application)


                '   Dim cls_planos As New Cls_planos
                caso_consulta = "CATASTRO MINERO"
                tipo_seleccion = "OP_11"
                cls_planos.genera_planoevaluacion(m_application, "Evaluacion")

                pMxDoc.UpdateContents()
                pMxDoc.ActiveView.Refresh()


                cls_catastro.Genera_Imagen_DM(v_codigo, "Evaluacion")


            Next i

        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetParam(ByVal lodtbAsigna As DataTable)
        Dim f As Integer
        For f = 0 To lodtbAsigna.Columns.Count - 1
            'If lodtbAsigna.Columns(f).ColumnName = gloNameEste Then
            'DataXY.iEsteX = f
            'ElseIf lodtbAsigna.Columns(f).ColumnName = gloNameNorte Then
            'DataXY.iNorteY = f
            If lodtbAsigna.Columns(f).ColumnName = gloNameCodigo Then
                DataXY.iCodigoID = f
            End If
        Next f
    End Sub

    Private Function ActivarEditor(ByRef m_application As IApplication, ByRef pFCLass As IFeatureClass) As IEditor
        Dim pWorkspace As IWorkspace
        Dim pEditor As IEditor = Nothing

        Dim pID As New UID
        Dim pDataset As IDataset
        pID.Value = "esriCore.Editor"
        pEditor = m_application.FindExtensionByCLSID(pID)
        pDataset = pFCLass
        pWorkspace = pDataset.Workspace
        pEditor.StartEditing(pWorkspace)
        pEditor.StartOperation()
        ActivarEditor = pEditor
    End Function

    Private Function Adicionarpolygon2(ByRef pFCLass As IFeatureClass, ByRef Vertices() As Point, ByVal Conta As Short) As IPolygon
        Dim i As Integer

        Dim pPolygon As IPolygon = New ESRI.ArcGIS.Geometry.PolygonClass
        Dim P As Integer, Parts As Integer = 0, SubPolygono As Integer = 0
        SubPolygono = 0
        Dim Puntos As Point
        Dim pPointCollection As IPointCollection = New RingClass()
        Puntos = New Point
        Puntos.PutCoords(Vertices(0).X, Vertices(0).Y)
        pPointCollection.AddPoint(Puntos)
        For i = 1 To Conta - 1
            SubPolygono = NuevoSubPolygono(Vertices, Vertices(i), i - 1)
            Puntos = New Point
            Puntos.PutCoords(Vertices(i).X, Vertices(i).Y)
            pPointCollection.AddPoint(Puntos)
            If SubPolygono <> -1 Then
                pRings(Parts) = pPointCollection
                Parts = Parts + 1
                pPointCollection = New RingClass()
            End If
        Next
        If Parts <> 0 Then 'Poligono con anillos
            Dim pGeometryCollection As IGeometryCollection
            pGeometryCollection = New Polygon
            For P = 0 To Parts - 1
                pGeometryCollection.AddGeometry(pRings(P))
                'Debug.Print("------>" + Parts.ToString)
            Next
            pPolygon = pGeometryCollection
        Else ' Poligono Normal
            pPointCollection = New ESRI.ArcGIS.Geometry.PolygonClass 'spPolygon
            For i = 0 To Conta - 1
                Puntos = New Point
                Puntos.PutCoords(Vertices(i).X, Vertices(i).Y)
                pPointCollection.AddPoint(Puntos)
            Next
            Puntos = New Point
            Puntos.PutCoords(Vertices(0).X, Vertices(0).Y)
            pPointCollection.AddPoint(Puntos)
            pPolygon = pPointCollection
        End If
        Adicionarpolygon2 = pPolygon
    End Function

    Private Function Adicionarpolygon3(ByRef pFCLass As IFeatureClass, ByRef Vertices() As Point, ByVal Conta As Short) As IPolyline
        Dim i As Integer
        Dim pPolygon As IPolyline = New ESRI.ArcGIS.Geometry.PolylineClass
        Dim P As Integer, Parts As Integer = 0
        SubPolygono = 0
        Dim Puntos As Point
        Dim pPointCollection As IPointCollection = New RingClass()
        Puntos = New Point
        Puntos.PutCoords(Vertices(0).X, Vertices(0).Y)
        pPointCollection.AddPoint(Puntos)
        For i = 1 To Conta - 1
            SubPolygono = NuevoSubPolygono(Vertices, Vertices(i), i - 1)
            Puntos = New Point
            Puntos.PutCoords(Vertices(i).X, Vertices(i).Y)
            pPointCollection.AddPoint(Puntos)
            If SubPolygono <> -1 Then
                pRings(Parts) = pPointCollection
                Parts = Parts + 1
                pPointCollection = New RingClass()
            End If
        Next
        If Parts <> 0 Then 'Poligono con anillos
            Dim pGeometryCollection As IGeometryCollection
            pGeometryCollection = New Polyline

            For P = 0 To Parts - 1
                pGeometryCollection.AddGeometry(pRings(P))
            Next
            pPolygon = pGeometryCollection
        Else ' Poligono Normal
            pPointCollection = New ESRI.ArcGIS.Geometry.PolylineClass
            For i = 0 To Conta - 1
                Puntos = New Point
                Puntos.PutCoords(Vertices(i).X, Vertices(i).Y)
                pPointCollection.AddPoint(Puntos)
            Next
            Puntos = New Point
            Puntos.PutCoords(Vertices(0).X, Vertices(0).Y)
            pPointCollection.AddPoint(Puntos)
            pPolygon = pPointCollection
        End If
        Adicionarpolygon3 = pPolygon
    End Function
    Private Function NuevoSubPolygono(ByRef ChekearVertice() As Point, ByRef punto As Point, ByVal actual As Short) As Short
        Dim j As Short, Resultado As Short
        Resultado = -1
        For j = 0 To actual
            'If punto.X = ChekearVertice(j).X And punto.Y = ChekearVertice(j).Y Then
            '    Resultado = actual + 2
            '    Exit For
            'End If
        Next
        NuevoSubPolygono = Resultado
    End Function
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
            MsgBox(".::Error - Eliminado Rows a FeatureClass::.", vbInformation, "Aviso")
        End Try
    End Sub
    Public Sub Delete_Rows_Tabla(ByVal p_Layer As String, ByVal MxDocument As IMxDocument)
        Dim pQueryFilter As IQueryFilter
        Try
            Conexion_SDE(MxDocument)
            Dim pTable As ITable
            pTable = pFeatureWorkspace.OpenTable(glo_Owner_Layer_SDE & "." & p_Layer)
            pQueryFilter = New QueryFilter
            pQueryFilter.WhereClause = "OBJECTID IS NOT NULL"
            pTable.DeleteSearchedRows(pQueryFilter)
            pTable = Nothing
        Catch ex As Exception
            '  MsgBox(".::Error - Eliminado Rows a FeatureClass::.", vbInformation, "Aviso")
        End Try
    End Sub
    Public Sub Conexion_Shapefile()
        pWorkspaceFactory = New ShapefileWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_pathTMP, 0)
    End Sub
    Public Function Open_Feature_SHP(ByVal mFeatureClass As String, ByVal m_applicacion As IApplication) As IFeatureClass
        Dim pFeatureClass As IFeatureClass
        Dim pFeatureLayer As IFeatureLayer
        Dim pFeatureWorkspace As IFeatureWorkspace
        Dim pWorkspaceFactory As IWorkspaceFactory
        pMxDoc = m_applicacion.Document

        pMap = pMxDoc.FocusMap
        pWorkspaceFactory = New ShapefileWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_pathTMP, 0)
        pFeatureClass = pFeatureWorkspace.OpenFeatureClass(mFeatureClass)
        pFeatureLayer = New FeatureLayerClass
        pFeatureLayer.FeatureClass = pFeatureClass
        'Select Case pGeometria
        ' Case "POLIGONO"
        pFeatureLayer.Name = "Poligono"
        '  Case "LINEA"
        '  pFeatureLayer.Name = "Polylinea"
        '   Case "PUNTO"
        '  pFeatureLayer.Name = "Punto"
        '   Case Else
        ' pFeatureLayer.Name = "xxxx"
        pFeatureLayer.Visible = True
        '   End Select
        pMap.AddLayer(pFeatureLayer)
        pMxDoc.ActiveView.Refresh()
        Open_Feature_SHP = pFeatureClass
    End Function
    'Public Sub SelectMapFeatures(ByVal pLayer As String, ByVal MxDocument As IMxDocument)
    '    pMxDoc = MxDocument
    '    pMap = pMxDoc.FocusMap
    '    pActiveView = pMap
    '    Dim bFound As Boolean = False
    '    For A As Integer = 0 To pMap.LayerCount - 1
    '        If pMap.Layer(A).Name.ToUpper = pLayer.ToUpper Then
    '            pFeatureLayer = pMap.Layer(A)
    '            bFound = True
    '            Exit For
    '        End If
    '    Next A
    '    If Not bFound Then
    '        ' MsgBox("No Existe Layer - ShowUniqueValuesCategory.")
    '        Exit Sub
    '    End If
    '    pFeatureSelection = pFeatureLayer 'QI
    '    'Flag the original selection
    '    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
    '    'Perform the selection
    '    pFeatureSelection.SelectFeatures(Nothing, esriSelectionResultEnum.esriSelectionResultNew, False)
    '    'Flag the new selection
    '    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
    'End Sub

    Public Sub ZoomToSelectedFeatures(ByVal MxDocument As IMxDocument)
        Dim pActiveView As IActiveView
        pMxDoc = MxDocument
        Dim pEnumFeature As IEnumFeature
        Dim pFeature As IFeature

        pMap = pMxDoc.FocusMap
        pEnumFeature = pMap.FeatureSelection
        pFeature = pEnumFeature.Next()
        Dim pEnvelope As IEnvelope
        pEnvelope = New Envelope
        Do While Not pFeature Is Nothing
            pEnvelope.Union(pFeature.Shape.Envelope)
            pFeature = pEnumFeature.Next()
        Loop
        pEnvelope.Expand(1.2, 1.2, True)
        pActiveView = pMxDoc.ActiveView
        pActiveView.Extent = pEnvelope
        pMap.ClearSelection()
        pActiveView.Refresh()
    End Sub

    Public Sub ZoomToSelectedFeatures(ByVal pZoom As Integer, ByVal MxDocument As IMxDocument)
        Dim pActiveView As IActiveView
        pMxDoc = MxDocument
        Dim pEnumFeature As IEnumFeature
        Dim pFeature As IFeature
        pMap = pMxDoc.FocusMap
        pEnumFeature = pMap.FeatureSelection
        pFeature = pEnumFeature.Next()
        Dim pEnvelope As IEnvelope
        pEnvelope = New Envelope
        Do While Not pFeature Is Nothing
            pEnvelope.Union(pFeature.Shape.Envelope)
            pFeature = pEnumFeature.Next()
        Loop
        pEnvelope.Expand(pZoom, pZoom, True)
        pActiveView = pMxDoc.ActiveView
        pActiveView.Extent = pEnvelope
        pMap.ClearSelection()
        pActiveView.Refresh()
    End Sub

    'Public Sub Genera_Punto(ByVal p_ShapeFile As String, ByVal lodtbDatos As DataTable, ByVal p_Zona As String, ByVal MxDocument As MxDocument)
    '    Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
    '    pSpatialReferenceEnv = New SpatialReferenceEnvironment
    '    Dim vCodigo As String
    '    Dim vEste, vNorte As Double
    '    Dim pFCLass As IFeatureClass = Open_Feature_SHP(p_ShapeFile, "PUNTO", MxDocument)
    '    Dim pEditor As IEditor = ActivarEditor(MxDocument, pFCLass)
    '    Dim Conta As Integer = 0
    '    Dim Vertices(500) As Point
    '    Dim vEstado As String = ""
    '    Dim contador As Integer = 1
    '    SetParam(lodtbDatos)
    '    Dim FieldDataCount As Integer = lodtbDatos.Columns.Count ' - 4
    '    ReDim DataInfo(FieldDataCount)
    '    Dim n As Integer = 0
    '    Dim pPoint_Geo As IPoint
    '    Dim pFeatureDMP As IFeature
    '    Dim pDataset As IDataset
    '    Dim pFLayerDMP As IFeatureLayer
    '    Dim pFCLass_Point As IFeatureClass
    '    Dim lo_pFLayerDMP As Integer
    '    Dim bFound As Boolean = False
    '    For A As Integer = 0 To pMap.LayerCount - 1
    '        If pMap.Layer(A).Name.ToUpper = "PUNTO" Then
    '            lo_pFLayerDMP = A
    '            bFound = True
    '            Exit For
    '        End If
    '    Next A
    '    If Not bFound Then
    '        MsgBox("Could not find feature layer.")
    '        Exit Sub
    '    End If
    '    pFLayerDMP = pMxDoc.FocusMap.Layer(lo_pFLayerDMP)
    '    pFCLass_Point = pFLayerDMP.FeatureClass
    '    pDataset = pFCLass_Point
    '    Dim pPointCollection As IPointCollection = New RingClass()
    '    Conta = 0
    '    Dim punto As Point = New Point
    '    For i As Integer = 0 To lodtbDatos.Rows.Count - 1
    '        'ReDim Vertices(500)
    '        vEste = lodtbDatos.Rows(i).Item(DataXY.iEsteX)
    '        vNorte = lodtbDatos.Rows(i).Item(DataXY.iNorteY)
    '        vCodigo = lodtbDatos.Rows(i).Item(DataXY.iCodigoID)
    '        Vertices(Conta) = New Point
    '        Vertices(Conta).PutCoords(vEste, vNorte)
    '        pFCLass_Point = pFLayerDMP.FeatureClass
    '        pFeatureDMP = pFCLass_Point.CreateFeature
    '        pPoint_Geo = New ESRI.ArcGIS.Geometry.Point
    '        pPoint_Geo.X = CType(vEste, Double)
    '        pPoint_Geo.Y = CType(vNorte, Double)
    '        Select Case p_Zona
    '            Case 17
    '                'pPoint_Geo.SpatialReference = Datum_PSAD_18
    '                'pPoint_Geo.Project(Datum_PSAD_17)
    '            Case 19
    '                'pPoint_Geo.SpatialReference = Datum_PSAD_18
    '                'pPoint_Geo.Project(Datum_PSAD_19)
    '        End Select
    '        pPoint_Geo.PutCoords(pPoint_Geo.X, pPoint_Geo.Y)
    '        pFeatureDMP.Shape = pPoint_Geo
    '        pFeatureDMP.Value(pFeatureDMP.Fields.FindField("CODIGO")) = vCodigo 'contador
    '        pFeatureDMP.Store()
    '        pEditor.StopOperation("Add point")
    '        Conta = Conta + 1
    '    Next (i)
    '    pEditor.StopEditing(True)

    'End Sub

    Public Sub Conexion_FileGeoDatabase(ByVal p_Path As String)
        Try
            pWorkspaceFactory = New FileGDBWorkspaceFactory
            pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(p_Path, 0)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Function ListRow(ByRef pTable As ITable, ByRef Query As String) As Integer
        Dim pQueryFilter As IQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = Query
        Dim i As Integer = pTable.RowCount(pQueryFilter)
        ListRow = i
    End Function

    Public Sub CargarFC(ByVal p_FeatureClass As String, ByVal p_Visible As Boolean, ByVal MxDocument As IMxDocument)
        Try
            pMxDoc = MxDocument
            pMap = pMxDoc.FocusMap
            '  If Conexion_SDE(MxDocument) Then
            pFeatureClass = pFeatureWorkspace.OpenFeatureClass(glo_Owner_Layer_SDE & "." & p_FeatureClass)
            pFeatureLayer = New FeatureLayerClass
            pFeatureLayer.FeatureClass = pFeatureClass
            pFeatureLayer.Name = Mid(pFeatureLayer.FeatureClass.AliasName, InStr(pFeatureLayer.FeatureClass.AliasName, ".") + 1)
            pFeatureLayer.Visible = p_Visible
            pMap.AddLayer(pFeatureLayer)
            pMxDoc.ActiveView.Refresh()
            '   End If

        Catch ex As Exception
            Try
                ' If Conexion_SDE(MxDocument) Then
                pTable = pFeatureWorkspace.OpenTable(glo_Owner_Layer_SDE & "." & p_FeatureClass)
                Add_Table_TOC(pTable, MxDocument)
                '  End If
            Catch ex1 As Exception
                MsgBox("No cargo capa .." & p_FeatureClass, MsgBoxStyle.Information, "Aviso")
            End Try

        End Try
    End Sub

    Private Sub Add_Table_TOC(ByVal pTable As ITable, ByVal MxDocument As IMxDocument)
        pMxDoc = MxDocument
        pMap = pMxDoc.FocusMap
        ' Create a new standalone table and add it
        ' to the collection of the focus map
        Dim pStTab As IStandaloneTable
        pStTab = New StandaloneTable
        pStTab.Table = pTable
        Dim pStTabColl As IStandaloneTableCollection
        pStTabColl = pMap
        pStTabColl.AddStandaloneTable(pStTab)
        ' Refresh the TOC
        pMxDoc.UpdateContents()
    End Sub

    Public Function CargarFCs(ByVal p_FeatureClass As String, ByVal p_Visible As Boolean, ByVal MxDocument As IMxDocument) As Boolean
        Try
            pMxDoc = MxDocument
            pMap = pMxDoc.FocusMap
            '  If Conexion_SDE(MxDocument) Then
            pFeatureClass = pFeatureWorkspace.OpenFeatureClass(glo_Owner_Layer_SDE & "." & p_FeatureClass)
            pFeatureLayer = New FeatureLayerClass
            pFeatureLayer.FeatureClass = pFeatureClass
            pFeatureLayer.Name = Mid(pFeatureLayer.FeatureClass.AliasName, InStr(pFeatureLayer.FeatureClass.AliasName, ".") + 1)
            pFeatureLayer.Visible = p_Visible
            pMap.AddLayer(pFeatureLayer)
            pMxDoc.ActiveView.Refresh()
            Return True
            '  End If
        Catch ex As Exception
            Return False
            MsgBox("No tiene privilegios en feature class.." & p_FeatureClass, MsgBoxStyle.Information, "Aviso")
        End Try
    End Function

    Public Sub Consulta_Tabla(ByVal p_Table As String, ByVal p_Query As String, ByVal MxDocument As IMxDocument)
        Conexion_SDE(MxDocument)
        Dim pRow As IRow
        pTable = pFeatureWorkspace.OpenTable(glo_Owner_Layer_SDE & "." & p_Table)
        Dim pCantRegistro As Integer = ListRow(pTable, "")
        Dim pTableSort As ITableSort
        pTableSort = New TableSort
        Try
            With pTableSort
                .Table = pTable
                .QueryFilter = Nothing
                .Fields = "ORDEN"
                .Ascending("ORDEN") = False ' TRUE is ascending, if set to FALSE then descending
                .Sort(Nothing)
            End With
            Dim pCursor As ICursor
            pCursor = pTableSort.Rows
            pRow = pCursor.NextRow
            Do While Not pRow Is Nothing
                CargarFC(pRow.Value(pRow.Fields.FindField("LAYER")), True, MxDocument)
                pRow = pCursor.NextRow
            Loop
            pTable = Nothing
            pCursor = Nothing

        Catch ex As Exception
        End Try
    End Sub
    Public Function Consulta_Tabla_1(ByVal p_Table As String, ByVal p_Query As String, ByVal MxDocument As IMxDocument) As Boolean
        Dim pTable As ITable
        Dim pRow As IRow
        Try
            Conexion_SDE(MxDocument)
            pTable = pFeatureWorkspace.OpenTable(p_Table)
            Dim ptableCursor As ICursor
            Dim pfields As Fields
            pfields = pTable.Fields
            Dim pQueryFilter As IQueryFilter
            'pfields3 = pTable.Fields
            pQueryFilter = New QueryFilter
            pQueryFilter.WhereClause = p_Query
            ptableCursor = pTable.Search(pQueryFilter, True)
            pRow = ptableCursor.NextRow
            Do Until pRow Is Nothing
                Return True
                pRow = ptableCursor.NextRow
            Loop
            Return False
        Catch ex As Exception
        End Try
    End Function

    'Public Function Consulta_Tabla_ORDEN(ByVal p_Table As String, ByVal p_Query As String, ByVal MxDocument As IMxDocument) As String
    '    Dim pRow As IRow
    '    Dim ptableCursor As ICursor
    '    Try
    '        Conexion_SDE(MxDocument)
    '        pTable = pFeatureWorkspace.OpenTable(glo_Owner_Layer_SDE & "." & p_Table)
    '        pFields = pTable.Fields
    '        pQueryFilter = New QueryFilter
    '        pQueryFilter.WhereClause = p_Query
    '        ptableCursor = pTable.Search(pQueryFilter, True)
    '        pRow = ptableCursor.NextRow
    '        Do Until pRow Is Nothing
    '            Return pRow.Value(pFields.FindField("ORDEN"))
    '            pRow = ptableCursor.NextRow
    '        Loop
    '    Catch ex As Exception
    '    End Try
    '    Return 1000
    '    pTable = Nothing
    '    ptableCursor = Nothing


    'End Function


    Public Function Consulta_Tabla_GDoc(ByVal p_Table As String, ByVal p_Query As String, ByVal MxDocument As IMxDocument) As DataTable

        Dim pTable As ITable
        Conexion_SDE(MxDocument)
        pTable = pFeatureWorkspace.OpenTable(p_Table)
        '**************************
        Dim pDataset As IDataset
        Dim pCursor As ICursor
        pDataset = pTable
        pFeatureWorkspace = pDataset.Workspace
        Dim pQueryDef As IQueryDef
        pQueryDef = pFeatureWorkspace.CreateQueryDef
        Try
            With pQueryDef
                .Tables = pDataset.Name ' Fully qualified table name
                .WhereClause = p_Query
                pCursor = .Evaluate
            End With
            Dim pCantRegistro As Integer = ListRow(pTable, p_Query)
            Dim pRow As IRow
            Dim c As Integer = 0
            pRow = pCursor.NextRow
            Dim drow As DataRow
            Dim LodtbTabla As New DataTable
            LodtbTabla.Columns.Add("DESCRIPCION", Type.GetType("System.String"))
            Do Until pRow Is Nothing
                drow = LodtbTabla.NewRow
                drow.Item("DESCRIPCION") = pRow.Value(pRow.Fields.FindField("DESCRIPCION"))
                LodtbTabla.Rows.Add(drow)
                pRow = pCursor.NextRow
            Loop
            Return LodtbTabla
        Catch ex As Exception

        End Try
        Return Nothing
    End Function

    'Function DefinitionQuery(ByVal p_NameLayer As String, ByVal p_Filtro As String, ByVal MxDocument As IMxDocument) As Boolean
    '    pMxDoc = MxDocument
    '    Dim pActiveView As IActiveView
    '    pMap = pMxDoc.FocusMap
    '    pActiveView = pMap
    '    Dim pQueryFilter As IQueryFilter
    '    Dim pocultatema As IFeatureLayerDefinition
    '    Dim pFeatureSelection As IFeatureSelection
    '    ' Prepare a query filter.
    '    Try
    '        pFeatureLayer = GroupLayer(p_NameLayer)
    '        pQueryFilter = New QueryFilter
    '        pocultatema = pFeatureLayer
    '        pocultatema.DefinitionExpression = p_Filtro
    '        pQueryFilter.WhereClause = p_Filtro
    '        pFeatureSelection = pocultatema
    '        pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
    '        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    

    'Public Function Conexion_SDE1(ByVal cod As String) As DataTable
    '    Try
    '        Dim conn As New OracleConnection(conOraDB)
    '        conn.Open()
    '        Dim cmd As New OracleCommand
    '        cmd.Connection = conn
    '        cmd.CommandText = cod
    '        cmd.CommandType = CommandType.Text
    '        Dim dr As OracleDataReader = cmd.ExecuteReader()
    '        Dim dtSchema As DataTable = dr.GetSchemaTable()
    '        Dim dt As DataTable = New DataTable()
    '        Dim listCols As List(Of DataColumn) = New List(Of DataColumn)()
    '        If Not dtSchema Is Nothing Then
    '            For Each drow As DataRow In dtSchema.Rows
    '                Dim columnName As String = System.Convert.ToString(drow("ColumnName"))
    '                Dim column As DataColumn = New DataColumn(columnName, CType(drow("DataType"), Type))
    '                listCols.Add(column)
    '                dt.Columns.Add(column)
    '            Next drow
    '        End If
    '        ' Read rows from DataReader and populate the DataTable
    '        Do While dr.Read()
    '            Dim dataRow As DataRow = dt.NewRow()
    '            For i As Integer = 0 To listCols.Count - 1
    '                dataRow((CType(listCols(i), DataColumn))) = dr(i)
    '            Next i
    '            dt.Rows.Add(dataRow)
    '        Loop
    '        Return dt
    '        conn.Close()
    '    Catch ex As Exception
    '        ' handle error
    '        Return Nothing
    '    End Try
    'End Function


    Public Function Conexion_SDE(ByVal MxDocument As IMxDocument) As Boolean
        Try
            pMxDoc = MxDocument
            pMap = pMxDoc.FocusMap
            Dim pPropset As IPropertySet
            pPropset = New PropertySetClass
            With pPropset
                .SetProperty("SERVER", glo_Server_SDE) '"192.168.1.200") 'glo_Server_SDE)
                .SetProperty("INSTANCE", glo_Instance_SDE)
                '   .SetProperty("INSTANCE", "SDE:ORACLE:LOCALHOST/BDGEOCAT") 'glo_Instance_SDE)
                ' .SetProperty("Database", "GEOGAS") ' Ignored with ArcSDE for Oracle 
                '  .SetProperty("AUTHENTICATION_MODE", "DBMS")
                .SetProperty("USER", glo_User_SDE)
                .SetProperty("PASSWORD", glo_Password_SDE)
                .SetProperty("VERSION", glo_Version_SDE)
            End With
            pWorkspaceFactory = New SdeWorkspaceFactory()
            'pFeatureWorkspace = pWorkspaceFactory.OpenFromString("sde:oracle11g:192.168.1.200:1521/GEOGAS;user=sde;password=jazani2014", 1)
            '  pFeatureWorkspace = pWorkspaceFactory.OpenFromString("sde:Oracle11g:localhost:1521/bdprueba;user=tgp;password=TGP__", 0)
            'pFeatureWorkspace = pWorkspaceFactory.OpenFromString("server=192.168.1.200;database=geogas;instance=5151;user=sde;password=jazani2014;version=sde.default", 0)


            'Dim connectionProps As IPropertySet = New PropertySet
            'With connectionProps
            '    .SetProperty("DBCLIENT", "Oracle11g")
            '    .SetProperty("INSTANCE", "192.168.1.200:1521/geogas")
            '    .SetProperty("AUTHENTICATION_MODE", "DBMS")
            '    .SetProperty("USER", "sde")
            '    .SetProperty("PASSWORD", "jazani2014")

            'End With
            pFeatureWorkspace = pWorkspaceFactory.Open(pPropset, 0)

            Return True
        Catch ex As Exception
            Return False
        End Try


        '   "server=192.168.1.200;database=geogas;instance=5151;user=sde;password=jazani2014;version=sde.default"



    End Function
    'Public Function verEstadoSDE(ByVal pUsuario As String) As Boolean
    '    Dim dt As New DataTable
    '    Dim Usuario As String = ""
    '    dt = Usuarios_ConectadosSDE()
    '    For r As Integer = 0 To dt.Rows.Count - 1
    '        Usuario = dt.Rows(r).Item("usuario")
    '        If Usuario.ToUpper = pUsuario.ToUpper Then
    '            Return True
    '            Exit Function
    '        End If
    '    Next
    '    Return False
    'End Function




    Public Sub Execute_GUID(ByVal pCodigo As String, ByVal pSubtipo As Integer)
        Dim pUID As New UID
        Dim pCmdItem As ICommandItem
        Dim Application As IApplication
        ' Use the GUID of the Save command
        pUID.Value = pCodigo ' "{119591DB-0255-11D2-8D20-080009EE4E51}"
        ' or you can use the ProgID
        Application = m_application.Application
        If pSubtipo <> 0 Then
            pUID.SubType = pSubtipo '1
        End If
        pCmdItem = Application.Document.CommandBars.Find(pUID)
        pCmdItem.Execute()
    End Sub


    Public Sub DefinitionExpression_Campo(ByVal lo_Filtro As String, ByVal Nom_Shapefile As String, ByVal MxDocument As IMxDocument)
        Dim pActiveView As IActiveView
        pMxDoc = MxDocument
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        Dim afound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If Mid(pMap.Layer(A).Name, InStr(pMap.Layer(A).Name, ".") + 1).ToUpper = Mid(Nom_Shapefile, InStr(Nom_Shapefile, ".") + 1).ToUpper Then
                pFeatureLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("Layer No Existe.")
            Exit Sub
        End If
        Try
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
                Execute_GUID("{AB073B49-DE5E-11D1-AA80-00C04FA37860}", 0)
                pActiveView.Refresh()
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub

    Public Function Existe_Layer(ByVal Nom_Shapefile As String, ByVal MxDocument As IMxDocument) As Boolean
        Dim pActiveView As IActiveView
        pMxDoc = MxDocument
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        Dim afound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If Mid(pMap.Layer(A).Name, InStr(pMap.Layer(A).Name, ".") + 1).ToUpper = Mid(Nom_Shapefile, InStr(Nom_Shapefile, ".") + 1).ToUpper Then
                Return True
            End If
        Next A
        Return False
    End Function

    ' Public Sub Cargar_Plantilla(ByVal pPlantilla As String, ByVal MxDocument As IMxDocument)
    '    pMxDoc = MxDocument
    '    Dim rutaPlantilla As String
    '    Try
    '        'rutaPlantilla = "E:\Modulo\Modulo_SIGAS\SIGAS\SIGAS\Plantilla\" & pPlantilla
    '        rutaPlantilla = glo_PathMXD & pPlantilla
    '        Dim rutalayout As IGxFile
    '        rutalayout = New GxMap
    '        rutalayout.Path = rutaPlantilla
    '        Dim pGxPageLayout As ESRI.ArcGIS.Catalog.IGxMapPageLayout
    '        pGxPageLayout = rutalayout
    '        Dim pPageLayout As ESRI.ArcGIS.Carto.IPageLayout
    '        pPageLayout = pGxPageLayout.PageLayout
    '        pPageLayout.ReplaceMaps(pMxDoc.Maps)
    '        pMxDoc.PageLayout = pPageLayout
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Function Cargar_Texto(ByVal tipoletra As String, ByVal texto As String, ByVal x As Double, ByVal y As Double, ByVal tamano As Double, ByVal color As Integer, ByVal negrita As Integer, _
    '    ByVal centrado As Integer, ByVal mayusc As Integer, ByVal espacio As Integer, ByVal MxDocument As IMxDocument, Optional ByVal inf_prel As Integer = 0)

    '    Dim pPageLayout As IPageLayout
    '    Dim pGC As IGraphicsContainer
    '    Dim pTxtElem As ITextElement
    '    Dim pTxtSym As ITextSymbol
    '    pMxDoc = MxDocument
    '    pMap = pMxDoc.FocusMap

    '    pPageLayout = pMxDoc.PageLayout
    '    pGC = pPageLayout
    '    pGC.Reset()

    '    Dim myFont As IFontDisp
    '    myFont = New StdFont
    '    'myFont.Name = "Arial"
    '    myFont.Name = tipoletra
    '    myFont.Size = tamano
    '    myFont.Bold = IIf(negrita = 1, True, False)

    '    'Colores

    '    Dim myColor As IRgbColor
    '    myColor = New RgbColor
    '    If color = 0 Or color = 1 Then
    '        myColor.Red = 0
    '        myColor.Green = 0
    '        myColor.Blue = IIf(color = 1, 255, 0)
    '    Else
    '        myColor.Blue = 219
    '        myColor.Green = 219
    '        myColor.Red = 219
    '    End If

    '    Dim pFormatSymbol As IFormattedTextSymbol
    '    pFormatSymbol = New TextSymbol

    '    ' agregado

    '    Dim pFillSymb As IFillSymbol
    '    pFillSymb = New SimpleFillSymbol

    '    Dim pColor As IColor
    '    pColor = New RgbColor
    '    pColor.NullColor = True

    '    pFillSymb.Color = pColor

    '    Dim symbol As ILineSymbol
    '    symbol = New SimpleLineSymbol

    '    symbol.Width = 0.2
    '    Dim pColor2 As IColor
    '    pColor2 = New RgbColor
    '    pColor2.RGB = RGB(225, 225, 225)
    '    symbol.Color = pColor2

    '    pFillSymb.Outline = symbol
    '    'hasta aqui
    '    With pFormatSymbol
    '        If inf_prel <> 0 Then
    '            .FillSymbol = pFillSymb
    '            .Font = myFont
    '            .Angle = 45
    '        Else
    '            .Leading = espacio
    '            .Color = myColor
    '            .Font = myFont
    '        End If
    '        If centrado = 0 Then
    '            .HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft ' esriTHALeft
    '        End If
    '        If centrado = 2 Then
    '            .HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight ' esriTHARight
    '        End If

    '    End With

    '    'Elementos
    '    pTxtElem = New TextElement

    '    pTxtSym = New TextSymbol
    '    With pTxtSym
    '        .Color = myColor
    '        .Font = myFont

    '        If centrado = 0 Then
    '            .HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft ' esriTHALeft
    '        End If
    '        If centrado = 2 Then
    '            .HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight ' esriTHARight
    '        End If

    '    End With

    '    If espacio <> 0 Or inf_prel <> 0 Then
    '        pTxtElem.Symbol = pFormatSymbol
    '    Else
    '        pTxtElem.Symbol = pTxtSym
    '    End If

    '    pTxtElem.Text = IIf(mayusc = 1, UCase(texto), texto)

    '    Dim pEnv As IEnvelope
    '    pEnv = New Envelope
    '    Dim pPoint As IPoint
    '    pPoint = New Point
    '    pPoint.X = x
    '    pPoint.Y = y
    '    pEnv.LowerLeft = pPoint

    '    Dim pelem As IElement
    '    pelem = pTxtElem
    '    pelem.Geometry = pEnv

    '    'Agregar elementos
    '    pGC.AddElement(pelem, 0)
    '    Return ""
    'End Function

    'Public Sub SelectMapFeaturesByAttributeQuery(ByVal activeView As ESRI.ArcGIS.Carto.IActiveView, ByVal featureLayer As ESRI.ArcGIS.Carto.IFeatureLayer, ByVal whereClause As System.String)
    '    If activeView Is Nothing OrElse featureLayer Is Nothing OrElse whereClause Is Nothing Then
    '        Return
    '    End If
    '    Dim featureSelection As ESRI.ArcGIS.Carto.IFeatureSelection = TryCast(featureLayer, ESRI.ArcGIS.Carto.IFeatureSelection) ' Dynamic Cast
    '    Dim queryFilter As ESRI.ArcGIS.Geodatabase.IQueryFilter = New ESRI.ArcGIS.Geodatabase.QueryFilterClass
    '    queryFilter.WhereClause = whereClause
    '    ' Invalidate only the selection cache. Flag the original selection
    '    activeView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
    '    ' Perform the selection
    '    featureSelection.SelectFeatures(queryFilter, ESRI.ArcGIS.Carto.esriSelectionResultEnum.esriSelectionResultNew, False)
    '    ' Flag the new selection
    '    activeView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
    'End Sub

    'Public Sub ClearSelectedMapFeatures(ByVal activeView As ESRI.ArcGIS.Carto.IActiveView, ByVal featureLayer As ESRI.ArcGIS.Carto.IFeatureLayer)
    '    If activeView Is Nothing OrElse featureLayer Is Nothing Then
    '        Return
    '    End If
    '    Dim featureSelection As ESRI.ArcGIS.Carto.IFeatureSelection = TryCast(featureLayer, ESRI.ArcGIS.Carto.IFeatureSelection) ' Dynamic Cast
    '    ' Invalidate only the selection cache. Flag the original selection
    '    activeView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
    '    ' Clear the selection
    '    featureSelection.Clear()
    '    ' Flag the new selection
    '    activeView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
    'End Sub


    'Public Sub SelectByGraphic(ByVal pElement As IElement, ByVal MxDocument As IMxDocument)
    '    ' Once the graphic is created we need to select the selectable
    '    ' features using the graphic
    '    Dim pMxDoc As IMxDocument
    '    Dim pActiveView As IActiveView

    '    pMxDoc = MxDocument
    '    pActiveView = pMxDoc.ActiveView

    '    '...Set the selectable layer & keep track of what's selectable
    '    Dim SelectionArray() As Integer    '...Declare dynamic array.
    '    Dim pFeatureLayer As IFeatureLayer

    '    ReDim SelectionArray(pMxDoc.FocusMap.LayerCount)    '...Allocate elements.
    '    For i = 0 To pMxDoc.FocusMap.LayerCount - 1
    '        pFeatureLayer = pMxDoc.FocusMap.Layer(i)

    '        If pFeatureLayer.Selectable = True Then
    '            SelectionArray(i) = 1 'Selectable
    '            If GetLayer(pFeatureLayer.Name, MxDocument) = True Then
    '                pFeatureLayer.Selectable = True
    '            Else
    '                pFeatureLayer.Selectable = False
    '            End If
    '        Else
    '            SelectionArray(i) = 2 'Not Selectable
    '            If GetLayer(pFeatureLayer.Name, MxDocument) = True Then
    '                pFeatureLayer.Selectable = True
    '            Else
    '                pFeatureLayer.Selectable = False
    '            End If
    '        End If

    '    Next i

    '    '...Make the selection
    '    Dim pEnv As ISelectionEnvironment
    '    Dim pGeo As IGeometry

    '    pEnv = New SelectionEnvironment
    '    pGeo = pElement.Geometry

    '    '...we want to add to what ever is currently selected
    '    'If optAddTo.Value = True Then
    '    '    pEnv.CombinationMethod = esriSelectionResultAdd
    '    'Else
    '    pEnv.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew
    '    'End If

    '    pEnv.AreaSelectionMethod = esriSpatialRelEnum.esriSpatialRelIntersects
    '    pMxDoc.FocusMap.SelectByShape(pGeo, pEnv, False)
    '    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
    '    pEnv.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew

    '    '...Reset the selectable layers for the map
    '    For i = 0 To pMxDoc.FocusMap.LayerCount - 1
    '        pFeatureLayer = pMxDoc.FocusMap.Layer(i)
    '        If SelectionArray(i) = 1 Then
    '            pFeatureLayer.Selectable = True
    '        Else
    '            pFeatureLayer.Selectable = False
    '        End If
    '    Next i



    'End Sub

    'Private Function GetLayer(ByVal strLayerName As String, ByVal MxDocument As IMxDocument) As Boolean
    '    ' This function accepts a layer name and returns
    '    ' the layer if available, otherwise returns "Nothing".
    '    '
    '    ' (1) Access the document's map
    '    Dim pMxDoc As IMxDocument
    '    Dim pMap As IMap
    '    pMxDoc = MxDocument
    '    pMap = pMxDoc.FocusMap
    '    '
    '    ' (2) Search through layers for the given layer name
    '    Dim lngIndex As Long
    '    GetLayer = Nothing
    '    For lngIndex = 0 To pMap.LayerCount - 1
    '        If pMap.Layer(lngIndex).Name = strLayerName Then
    '            ' GetLayer = pMap.Layer(lngIndex)
    '            Return True
    '            'Exit For
    '        End If
    '    Next lngIndex
    '    Return False

    'End Function

    'Public Function SelectMapFeaturesByAttributeQuery_1(ByVal activeView As ESRI.ArcGIS.Carto.IActiveView, _
    '                                                    ByVal pFeatureLayer As ESRI.ArcGIS.Carto.IFeatureLayer, _
    '                                                    ByVal pCampo As String, _
    '                                                    ByVal whereClause As System.String) As String

    '    If activeView Is Nothing OrElse pFeatureLayer Is Nothing OrElse whereClause Is Nothing Then
    '        Return ""
    '    End If
    '    Try
    '        Dim pFeatureSelection As ESRI.ArcGIS.Carto.IFeatureSelection = TryCast(pFeatureLayer, ESRI.ArcGIS.Carto.IFeatureSelection) ' Dynamic Cast
    '        Dim pSelectionSet As ISelectionSet = pFeatureSelection.SelectionSet
    '        Dim pFeatureCursor As IFeatureCursor = Nothing
    '        pSelectionSet.Search(Nothing, True, pFeatureCursor)
    '        pFeature = pFeatureCursor.NextFeature
    '        Do While Not pFeature Is Nothing
    '            Return pFeature.Value(pFeature.Fields.FindField(pCampo)).ToString()
    '            pFeature = pFeatureCursor.NextFeature
    '        Loop
    '    Catch ex As Exception
    '    End Try
    '    Return ""
    'End Function

    'Public Function SelectMapFeaturesByAttributeQuery_2(ByVal activeView As ESRI.ArcGIS.Carto.IActiveView, _
    '                                                   ByVal pFeatureLayer As ESRI.ArcGIS.Carto.IFeatureLayer, _
    '                                                   ByVal pCampo As String, _
    '                                                   ByVal whereClause As System.String) As DataTable

    '    Dim LodtbItems As New DataTable
    '    Dim dRow As DataRow
    '    If activeView Is Nothing OrElse pFeatureLayer Is Nothing OrElse whereClause Is Nothing Then
    '        Return Nothing
    '    End If
    '    LodtbItems.Columns.Add("ITEMS", Type.GetType("System.String"))
    '    LodtbItems.Columns.Add("OBJECTID", Type.GetType("System.String"))
    '    Try
    '        Dim pFeatureSelection As ESRI.ArcGIS.Carto.IFeatureSelection = TryCast(pFeatureLayer, ESRI.ArcGIS.Carto.IFeatureSelection) ' Dynamic Cast
    '        Dim pSelectionSet As ISelectionSet = pFeatureSelection.SelectionSet
    '        Dim pFeatureCursor As IFeatureCursor = Nothing
    '        pSelectionSet.Search(Nothing, True, pFeatureCursor)
    '        pFeature = pFeatureCursor.NextFeature
    '        Do While Not pFeature Is Nothing
    '            'Return pFeature.Value(pFeature.Fields.FindField(pCampo)).ToString()
    '            dRow = LodtbItems.NewRow
    '            dRow.Item("ITEMS") = pFeature.Value(pFeature.Fields.FindField(pCampo))
    '            dRow.Item("OBJECTID") = pFeature.Value(pFeature.Fields.FindField("OBJECTID"))
    '            LodtbItems.Rows.Add(dRow)
    '            pFeature = pFeatureCursor.NextFeature
    '        Loop
    '        Return LodtbItems
    '    Catch ex As Exception
    '    End Try
    '    Return Nothing
    'End Function

    'Public Function SelectMapFeaturesByAttributeQuery_Count(ByVal activeView As ESRI.ArcGIS.Carto.IActiveView, _
    '                                                    ByVal pFeatureLayer As ESRI.ArcGIS.Carto.IFeatureLayer, _
    '                                                    ByVal pCampo As String, _
    '                                                    ByVal whereClause As System.String) As String

    '    If activeView Is Nothing OrElse pFeatureLayer Is Nothing OrElse whereClause Is Nothing Then
    '        Return ""
    '    End If
    '    Try
    '        Dim pFeatureSelection As ESRI.ArcGIS.Carto.IFeatureSelection = TryCast(pFeatureLayer, ESRI.ArcGIS.Carto.IFeatureSelection) ' Dynamic Cast
    '        Dim pSelectionSet As ISelectionSet = pFeatureSelection.SelectionSet
    '        Return pFeatureSelection.SelectionSet.Count
    '    Catch ex As Exception
    '        Return 0
    '    End Try
    '    Return ""
    'End Function


    'Public Function FT_CargarTabla(ByVal paloITable As String, ByVal pNompQuery As String, ByVal MxDocument As IMxDocument) As DataTable
    '    Dim lodtRegistros As New DataTable
    '    Try
    '        Conexion_SDE(MxDocument)
    '        pTable = pFeatureWorkspace.OpenTable(paloITable)
    '        Dim pFeatureCursor As ICursor
    '        Dim pQueryFilter As IQueryFilter
    '        Dim loNumCol, loNumColTemp, sw As Int16
    '        pFields = pTable.Fields
    '        loNumCol = pFields.FieldCount
    '        For c As Int16 = 0 To loNumCol - 1
    '            If pFields.Field(c).Name = "Shape" Then
    '                lodtRegistros.Columns.Add("SHAPE")
    '                loNumColTemp = c
    '                sw = 1
    '            Else
    '                lodtRegistros.Columns.Add(pFields.Field(c).Name)
    '            End If
    '        Next
    '        pQueryFilter = New QueryFilter
    '        pQueryFilter.WhereClause = Nothing
    '        pFeatureCursor = pTable.Search(pQueryFilter, False)
    '        Dim pRow As IRow
    '        pRow = pFeatureCursor.NextRow
    '        Do Until pRow Is Nothing
    '            Dim c1 As Integer = -1
    '            Dim dr As DataRow
    '            dr = lodtRegistros.NewRow
    '            For c As Int16 = 0 To loNumCol - 1
    '                If loNumColTemp <> c Then
    '                    c1 = c1 + 1
    '                    Try
    '                        If Not IsDBNull(pRow.Value(c)) Then dr.Item(c) = CType(pRow.Value(c), String)
    '                    Catch ex As Exception
    '                        dr.Item(c) = Nothing
    '                    End Try

    '                Else
    '                    If sw = 1 Then c1 = c1 + 1
    '                End If
    '            Next
    '            lodtRegistros.Rows.Add(dr)
    '            pRow = pFeatureCursor.NextRow
    '        Loop
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    '    Return lodtRegistros
    'End Function
    'Public Function FT_Cargar_Registro(ByVal pNomTable As String, ByVal pNompQuery As String, ByVal MxDocument As IMxDocument, Optional ByVal paloAdicional As String = "") As DataTable
    '    Try
    '        Conexion_SDE(MxDocument)
    '        pTable = pFeatureWorkspace.OpenTable(glo_Owner_Layer_SDE & "." & pNomTable)
    '        Dim pFeatureCursor As ICursor
    '        Dim pQueryFilter As IQueryFilter
    '        Dim lodtRegistros As New DataTable
    '        Dim loNumCol, loNumColTemp As Int16
    '        pFields = pTable.Fields
    '        loNumCol = pFields.FieldCount
    '        For c As Int16 = 0 To loNumCol - 1
    '            lodtRegistros.Columns.Add(pFields.Field(c).Name)
    '        Next
    '        pQueryFilter = New QueryFilter
    '        pQueryFilter.WhereClause = pNompQuery
    '        pFeatureCursor = pTable.Search(pQueryFilter, False)

    '        If paloAdicional = "1" Then
    '            Dim dr1 As DataRow
    '            dr1 = lodtRegistros.NewRow
    '            Try
    '                If pNomTable = "GED_M_LISTA" Then
    '                    dr1.Item("CODIGO") = ""
    '                    dr1.Item("DESCRIPCION") = "--Seleccionar--"
    '                End If
    '            Catch ex As Exception
    '            End Try
    '            'Next
    '            lodtRegistros.Rows.Add(dr1)
    '        End If
    '        Dim pRow As IRow
    '        pRow = pFeatureCursor.NextRow
    '        Do Until pRow Is Nothing
    '            If Not IsDBNull(pRow.Value(loNumColTemp)) Then
    '                Dim dr As DataRow
    '                dr = lodtRegistros.NewRow
    '                For c As Int16 = 0 To loNumCol - 1
    '                    Try
    '                        If Not IsDBNull(pRow.Value(c)) Then dr.Item(c) = CType(pRow.Value(c), String)
    '                    Catch ex As Exception
    '                    End Try
    '                Next
    '                lodtRegistros.Rows.Add(dr)
    '            End If
    '            pRow = pFeatureCursor.NextRow
    '        Loop
    '        Return lodtRegistros
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    'Public Shared Sub CreateRowInNonVersionedEditSession(ByVal workspace As IWorkspace, _
    '                                                     ByVal table As ITable)

    '    ' Cast the workspace to the IWorkspaceEdit2 and IMultiuserWorkspaceEdit interfaces.
    '    Dim workspaceEdit2 As IWorkspaceEdit2 = CType(workspace, IWorkspaceEdit2)
    '    Dim muWorkspaceEdit As IMultiuserWorkspaceEdit = CType(workspace, IMultiuserWorkspaceEdit)

    '    ' Make sure that non-versioned editing is supported. If not, throw an exception.
    '    If Not muWorkspaceEdit.SupportsMultiuserEditSessionMode(esriMultiuserEditSessionMode.esriMESMNonVersioned) Then
    '        Throw New ArgumentException("The workspace does not support non-versioned editing.")
    '    End If

    '    ' Start a non-versioned edit session.
    '    muWorkspaceEdit.StartMultiuserEditing(esriMultiuserEditSessionMode.esriMESMNonVersioned)

    '    ' Create a new row. The row's attribute values should be set here, and if
    '    ' a feature is being created, the shape should be set as well.
    '    Dim row As IRow = table.CreateRow()
    '    row.Store()

    '    ' Stop the edit session. The saveEdits parameter indicates the edit session
    '    ' will be committed.
    '    workspaceEdit2.StopEditing(True)
    'End Sub

    'Public Sub DataTableToExcel(ByVal pDataTable As DataTable, ByVal pNomTabla As String)
    '    Dim vFileName As String = glo_PathTemporal & pNomTabla & "_" + DateTime.Now.Ticks.ToString() & ".XLS"
    '    vFileName = vFileName.Replace("/", "\")
    '    Try
    '        FileOpen(1, vFileName, OpenMode.Output)
    '        Dim sb As String = ""
    '        Dim dc As DataColumn
    '        For Each dc In pDataTable.Columns
    '            sb &= dc.Caption & Microsoft.VisualBasic.ControlChars.Tab
    '        Next
    '        PrintLine(1, sb)
    '        Dim i As Integer = 0
    '        Dim dr As DataRow
    '        For Each dr In pDataTable.Rows
    '            i = 0 : sb = ""
    '            For Each dc In pDataTable.Columns
    '                If Not IsDBNull(dr(i)) Then
    '                    sb &= CStr(dr(i)) & Microsoft.VisualBasic.ControlChars.Tab
    '                Else
    '                    sb &= Microsoft.VisualBasic.ControlChars.Tab
    '                End If
    '                i += 1
    '            Next

    '            PrintLine(1, sb)
    '        Next
    '        FileClose(1)
    '        ' TextToExcel(vFileName)
    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Public Sub AddLayerFile(ByVal pathLayerName As String, ByVal pActiva As Boolean, ByVal MxDocument As IMxDocument)
    '    Dim pGxLayer As IGxLayer
    '    Dim pGxFile As IGxFile
    '    pMxDoc = MxDocument
    '    pMap = pMxDoc.FocusMap
    '    pGxLayer = New GxLayer
    '    pGxFile = pGxLayer
    '    pGxFile.Path = pathLayerName
    '    pLayer = pGxLayer.Layer
    '    pLayer.Visible = pActiva
    '    pMap.AddLayer(pLayer)
    'End Sub

    'Public Sub Visible_Layer(ByVal p_FeatureClass As String, ByVal p_Visible As Boolean, ByVal MxDocument As IMxDocument)
    '    Try
    '        pMxDoc = MxDocument
    '        pMap = pMxDoc.FocusMap
    '        pFeatureLayer = New FeatureLayerClass
    '        For A As Integer = 0 To pMap.LayerCount - 1
    '            If pMap.Layer(A).Name.ToUpper = p_FeatureClass.ToUpper Then
    '                pFeatureLayer = pMap.Layer(A)
    '                'bFound = True
    '                Exit For
    '            End If
    '        Next A
    '        If pFeatureLayer.Visible = False Then
    '            pFeatureLayer.Visible = p_Visible
    '        End If
    '        Dim vistaactiva As IActiveView
    '        vistaactiva = pMap
    '        vistaactiva.ContentsChanged()
    '        vistaactiva.Refresh()
    '        pMxDoc.UpdateContents()
    '    Catch ex As Exception
    '        MsgBox("Error ...", MsgBoxStyle.Information, "Aviso")
    '    End Try
    'End Sub

    'Public Sub DeleteGraphicsByName(ByVal pMxDoc As IMxDocument, ByVal sElementName As String, Optional ByVal bRefresh As Boolean = False)

    '    'Delete the named graphics from the gra container
    '    Dim pGraCon As IGraphicsContainer
    '    Dim pElementProp As IElementProperties
    '    Dim pElement As IElement

    '    ' Get the graphics container
    '    pGraCon = pMxDoc.FocusMap
    '    pGraCon.Reset()

    '    'Search for and delete the previous graphic by name
    '    pElementProp = pGraCon.Next
    '    Do Until pElementProp Is Nothing
    '        'If pElementProp.Name = sElementName Then
    '        pElement = pElementProp
    '        pGraCon.DeleteElement(pElement)
    '        'End If
    '        pElementProp = pGraCon.Next
    '    Loop
    '    If bRefresh Then
    '        pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)
    '    End If

    'End Sub





    'Public Sub ExportarDatosExcel(ByVal DataGridView1 As DataGridView, ByVal titulo As String)
    '    Dim m_Excel As New Excel.Application
    '    m_Excel.Cursor = Excel.XlMousePointer.xlWait
    '    m_Excel.Visible = True
    '    Dim objLibroExcel As Excel.Workbook = m_Excel.Workbooks.Add
    '    Dim objHojaExcel As Excel.Worksheet = objLibroExcel.Worksheets(1)
    '    With objHojaExcel
    '        .Visible = Excel.XlSheetVisibility.xlSheetVisible
    '        .Activate()
    '        'Encabezado.
    '        .Range("A1:L1").Merge()
    '        .Range("A1:L1").Value = "REPORTE - " & "Layer: " & titulo
    '        .Range("A1:L1").Font.Bold = True
    '        .Range("A1:L1").Font.Size = 16
    '        'Texto despues del encabezado.
    '        .Range("A2:L2").Merge()
    '        .Range("A2:L2").Value = "" ' titulo
    '        .Range("A2:L2").Font.Bold = True
    '        .Range("A2:L2").Font.Size = 10
    '        'Nombres
    '        'Estilo a titulos de la tabla.
    '        .Range("A6:P6").Font.Bold = True
    '        'Establecer tipo de letra al rango determinado.
    '        .Range("A1:P37").Font.Name = "Tahoma"
    '        'Los datos se registran a partir de la columna A, fila 4.
    '        Const primeraLetra As Char = "A"
    '        Const primerNumero As Short = 6
    '        Dim Letra As Char, UltimaLetra As Char
    '        Dim Numero As Integer, UltimoNumero As Integer
    '        Dim cod_letra As Byte = Asc(primeraLetra) - 1
    '        Dim sepDec As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
    '        Dim sepMil As String = Application.CurrentCulture.NumberFormat.NumberGroupSeparator
    '        Dim strColumna As String = ""
    '        Dim LetraIzq As String = ""
    '        Dim cod_LetraIzq As Byte = Asc(primeraLetra) - 1
    '        Letra = primeraLetra
    '        Numero = primerNumero
    '        Dim objCelda As Excel.Range
    '        For Each c As DataGridViewColumn In DataGridView1.Columns
    '            If c.Visible Then
    '                : If Letra = "Z" Then
    '                    Letra = primeraLetra
    '                    cod_letra = Asc(primeraLetra)
    '                    cod_LetraIzq += 1
    '                    LetraIzq = Chr(cod_LetraIzq)
    '                Else
    '                    cod_letra += 1
    '                    Letra = Chr(cod_letra)
    '                End If
    '                strColumna = LetraIzq + Letra + Numero.ToString
    '                objCelda = .Range(strColumna, Type.Missing)
    '                objCelda.Value = c.HeaderText
    '                objCelda.EntireColumn.Font.Size = 10
    '                'Establece un formato a los numeros por Default.
    '                'objCelda.EntireColumn.NumberFormat = c.DefaultCellStyle.Format
    '                If c.ValueType Is GetType(Decimal) OrElse c.ValueType Is GetType(Double) Then
    '                    objCelda.EntireColumn.NumberFormat = "#" + sepMil + "0" + sepDec + "00"
    '                End If
    '            End If
    '        Next
    '        Dim objRangoEncab As Excel.Range = .Range(primeraLetra + Numero.ToString, LetraIzq + Letra + Numero.ToString)
    '        objRangoEncab.BorderAround(1, Excel.XlBorderWeight.xlMedium)
    '        UltimaLetra = Letra
    '        Dim UltimaLetraIzq As String = LetraIzq
    '        'Cargar Datos del DataGridView.
    '        Dim i As Integer = Numero + 1
    '        For Each reg As DataGridViewRow In DataGridView1.Rows
    '            LetraIzq = ""
    '            cod_LetraIzq = Asc(primeraLetra) - 1
    '            Letra = primeraLetra
    '            cod_letra = Asc(primeraLetra) - 1
    '            For Each c As DataGridViewColumn In DataGridView1.Columns
    '                If c.Visible Then
    '                    If Letra = "Z" Then
    '                        Letra = primeraLetra
    '                        cod_letra = Asc(primeraLetra)
    '                        cod_LetraIzq += 1
    '                        LetraIzq = Chr(cod_LetraIzq)
    '                    Else
    '                        cod_letra += 1
    '                        Letra = Chr(cod_letra)
    '                    End If
    '                    strColumna = LetraIzq + Letra
    '                    'Aqui se realiza la carga de datos.
    '                    .Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", "'" & reg.Cells(c.Index).Value)
    '                    'Establece las propiedades de los datos del DataGridView por Default.
    '                    '.Cells(i, strColumna) = IIf(IsDBNull(reg.(c.DataPropertyName)), c.DefaultCellStyle.NullValue, reg(c.DataPropertyName))
    '                    '.Range(strColumna + i, strColumna + i).In()
    '                End If
    '            Next
    '            Dim objRangoReg As Excel.Range = .Range(primeraLetra + i.ToString, strColumna + i.ToString)
    '            objRangoReg.Rows.BorderAround()
    '            objRangoReg.Select()
    '            i += 1
    '        Next
    '        UltimoNumero = i
    '        'Dibujar las líneas de las columnas.
    '        LetraIzq = ""
    '        cod_LetraIzq = Asc("A")
    '        cod_letra = Asc(primeraLetra)
    '        Letra = primeraLetra
    '        For Each c As DataGridViewColumn In DataGridView1.Columns
    '            If c.Visible Then
    '                objCelda = .Range(LetraIzq + Letra + primerNumero.ToString, LetraIzq + Letra + (UltimoNumero - 1).ToString)
    '                objCelda.BorderAround()
    '                If Letra = "Z" Then
    '                    Letra = primeraLetra
    '                    cod_letra = Asc(primeraLetra)
    '                    LetraIzq = Chr(cod_LetraIzq)
    '                    cod_LetraIzq += 1
    '                Else
    '                    cod_letra += 1
    '                    Letra = Chr(cod_letra)
    '                End If
    '            End If
    '        Next
    '        'Dibujar el border exterior grueso de la tabla.
    '        Dim objRango As Excel.Range = .Range(primeraLetra + primerNumero.ToString, UltimaLetraIzq + UltimaLetra + (UltimoNumero - 1).ToString)
    '        objRango.Select()
    '        objRango.Columns.AutoFit()
    '        objRango.Columns.BorderAround(1, Excel.XlBorderWeight.xlMedium)
    '    End With
    '    m_Excel.Cursor = Excel.XlMousePointer.xlDefault
    'End Sub




    'Public Function Leer_Vertices(ByVal pNombreLayer As String, ByVal MxDocument As IMxDocument) As String
    '    Dim loCoordenada As String = ""
    '    pMxDoc = MxDocument
    '    Dim aFound As Boolean = False
    '    'If Not Existe_Layer(pNombreLayer, MxDocument) Then
    '    '    CargarFC(pNombreLayer, True, MxDocument)
    '    'End If
    '    Try
    '        For A As Integer = 0 To pMap.LayerCount - 1
    '            If pMap.Layer(A).Name.ToUpper = pNombreLayer.ToUpper Then
    '                pFeatureLayer = pMap.Layer(A)
    '                aFound = True
    '                Exit For
    '            End If
    '        Next A
    '        If Not aFound Then
    '            MsgBox("No existe el Layer: pNombreLayer ", MsgBoxStyle.Information, "[Aviso]")
    '            Return 0
    '            Exit Function
    '        End If
    '        pActiveView = pMap
    '        Dim pFeatureSelection As IFeatureSelection = pFeatureLayer
    '        'pQueryFilter = New QueryFilter
    '        'pQueryFilter.WhereClause = ""
    '        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
    '        pFeatureSelection.SelectFeatures(Nothing, esriSelectionResultEnum.esriSelectionResultNew, False)
    '        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
    '        Dim pPolyPointColl As IPointCollection
    '        pFeatureCursor = pFeatureLayer.FeatureClass.Search(pQueryFilter, True)
    '        pFeature = pFeatureCursor.NextFeature
    '        pSelectionSet = pFeatureSelection.SelectionSet
    '        Do While Not pFeature Is Nothing
    '            pPolygon = pFeature.Shape
    '            pPolyPointColl = pPolygon
    '            For i = 0 To pPolyPointColl.PointCount - 1
    '                pPoint = pPolyPointColl.Point(i)
    '                loCoordenada = loCoordenada & CType(Math.Round((pPoint.X), 2), String).Replace(",", ".") & " " & CType(Math.Round(pPoint.Y, 2), String).Replace(",", ".") & ", "
    '            Next i  ' get next point in polygon
    '            loCoordenada = Mid(loCoordenada, 1, Len(loCoordenada) - 2)
    '            pFeature = pFeatureCursor.NextFeature
    '        Loop
    '    Catch ex As Exception
    '    End Try
    '    Return loCoordenada
    'End Function
    'Public Function Leer_Vertices_ID(ByVal pNombreLayer As String, ByVal pQuery As String, ByVal MxDocument As IMxDocument) As String
    '    Dim loCoordenada As String = ""
    '    pMxDoc = MxDocument
    '    Dim aFound As Boolean = False
    '    Try
    '        For A As Integer = 0 To pMap.LayerCount - 1
    '            If pMap.Layer(A).Name.ToUpper = pNombreLayer.ToUpper Then
    '                pFeatureLayer = pMap.Layer(A)
    '                aFound = True
    '                Exit For
    '            End If
    '        Next A
    '        If Not aFound Then
    '            MsgBox("No existe el Layer: " & pNombreLayer, MsgBoxStyle.Information, "[Aviso]")
    '            Return 0
    '            Exit Function
    '        End If
    '        pActiveView = pMap
    '        Dim pFeatureSelection As IFeatureSelection = pFeatureLayer
    '        pQueryFilter = New QueryFilter
    '        pQueryFilter.WhereClause = pQuery
    '        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
    '        pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
    '        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
    '        Dim pPolyPointColl As IPointCollection
    '        pFeatureCursor = pFeatureLayer.FeatureClass.Search(pQueryFilter, True)
    '        pFeature = pFeatureCursor.NextFeature
    '        pSelectionSet = pFeatureSelection.SelectionSet
    '        Do While Not pFeature Is Nothing
    '            pPolygon = pFeature.Shape
    '            pPolyPointColl = pPolygon
    '            For i = 0 To pPolyPointColl.PointCount - 1
    '                pPoint = pPolyPointColl.Point(i)
    '                loCoordenada = loCoordenada & CType(Math.Round((pPoint.X), 2), String).Replace(",", ".") & " " & CType(Math.Round(pPoint.Y, 2), String).Replace(",", ".") & ", "
    '            Next i  ' get next point in polygon
    '            loCoordenada = Mid(loCoordenada, 1, Len(loCoordenada) - 2)
    '            pFeature = pFeatureCursor.NextFeature
    '        Loop
    '    Catch ex As Exception
    '    End Try
    '    Return loCoordenada
    'End Function

    'Public Function Field_FileExpression(ByVal pLayer As String, ByVal MxDocument As IMxDocument) As String
    '    'Dim pMxDoc As IMxDocument
    '    'Dim pFLayer As IFeatureLayer
    '    Dim pDEP As IDisplayExpressionProperties = Nothing
    '    Dim pIDS As IDisplayString
    '    Try
    '        pMxDoc = MxDocument
    '        ' pFLayer = pMxDoc.SelectedLayer
    '        For A As Integer = 0 To pMap.LayerCount - 1
    '            If pMap.Layer(A).Name.ToUpper = pLayer.ToUpper Then
    '                pFeatureLayer = pMap.Layer(A)
    '                Exit For
    '            End If
    '        Next A
    '        pIDS = pFeatureLayer 'pFLayer
    '        pDEP = pIDS.ExpressionProperties
    '    Catch ex As Exception
    '        Return "OBJECTID"
    '    End Try

    '    Dim ci As String = InStr(pDEP.Expression, "[")
    '    Dim cf As String = InStr(pDEP.Expression, "]")
    '    Return Mid(pDEP.Expression, ci + 1, cf - ci - 1)
    'End Function
    'Public Sub Quitar_Layer(ByVal lostrFeature As String, ByVal MxDocument As IMxDocument)
    '    Dim m_pEnumLayers As IEnumLayer
    '    Dim m_pLayer As ILayer
    '    pMxDoc = MxDocument
    '    pMap = pMxDoc.FocusMap
    '    If pMap.LayerCount > 0 Then
    '        m_pEnumLayers = pMap.Layers
    '        m_pLayer = m_pEnumLayers.Next
    '        For j As Integer = 0 To pMap.LayerCount - 1
    '            If pMap.Layer(j).Name = lostrFeature Then
    '                pMap.DeleteLayer(m_pLayer)
    '                Exit Sub
    '            Else
    '            End If
    '            m_pLayer = m_pEnumLayers.Next
    '        Next
    '    End If
    '    pMxDoc.UpdateContents()
    '    pMxDoc.ActivatedView.Refresh()
    'End Sub
End Class
