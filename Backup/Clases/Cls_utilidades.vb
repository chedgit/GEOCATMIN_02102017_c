Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.DataSourcesFile
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Output
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Catalog
Imports stdole

Public Class cls_Utilidades
    Private DataInfo() As FieldData
    Private DataXY As VerXY
    Private Structure FieldData
        Dim Index As Integer
        Dim Campo As String
    End Structure
    Private Structure VerXY
        Dim sCodigoID As String
        Dim sEsteX As String
        Dim sNorteY As String
        Dim iEsteX As Integer
        Dim iNorteY As Integer
        Dim iCodigoID As Integer
    End Structure
    ''Private Sub SetParam(ByVal lodtbAsigna As DataTable)
    ''    Dim f As Integer
    ''    For f = 0 To lodtbAsigna.Columns.Count - 1 '(f).ColumnName - 1 '.Rows.Count - 1 '.Fields.FieldCount - 1
    ''        If lodtbAsigna.Columns(f).ColumnName = "ESTE" Then '.Fields.Field(f).Name = "ESTE" Then 'DataXY.sEsteX Then
    ''            DataXY.iEsteX = f
    ''        ElseIf lodtbAsigna.Columns(f).ColumnName = "NORTE" Then 'DataXY.sNorteY Then
    ''            DataXY.iNorteY = f
    ''        ElseIf lodtbAsigna.Columns(f).ColumnName = "CODIGO" Then 'DataXY.sCodigoID Then
    ''            DataXY.iCodigoID = f
    ''        End If
    ''    Next f
    ''End Sub

    'se puso

    'Private DataXY As VerXY
    'Private Structure FieldData
    '    Dim Index As Integer
    '    Dim Campo As String
    'End Structure
    'Private DataInfo() As FieldData

    'Public Sub Genera_Circulo(ByVal lodtbDatos As DataTable, ByVal p_Zona As String, ByVal pApp As IApplication, Optional ByVal p_Flag As String = "")
    '    Dim z As Integer = 0
    '    Dim Poligono_Ok As String = ""
    '    Dim pArea As IArea
    '    Dim pStatusBar As ESRI.ArcGIS.esriSystem.IStatusBar
    '    Dim pProgbar As ESRI.ArcGIS.esriSystem.IStepProgressor
    '    Dim vCodigo, vCodigoSalvado As String
    '    Dim vEste, vNorte As Double
    '    Dim pFCLass As IFeatureClass = Open_Feature_GDB(pApp, "", "POLIGONO_" & v_Zona)
    '    Dim pEditor As IEditor = ActivarEditor(pApp, pFCLass)
    '    Dim Conta As Integer = 0
    '    Dim Vertices(200) As Point
    '    Dim vEstado As String = ""
    '    Dim pFeatureDM As IFeature
    '    pFeatureDM = pFCLass.CreateFeature
    '    Dim contador As Integer = 1
    '    SetParam(lodtbDatos)
    '    Dim FieldDataCount As Integer = lodtbDatos.Columns.Count ' - 4
    '    ReDim DataInfo(FieldDataCount)
    '    Dim n As Integer = 0
    '    pStatusBar = pApp.StatusBar
    '    pProgbar = pStatusBar.ProgressBar
    '    pProgbar.Position = 0
    '    vCodigoSalvado = lodtbDatos.Rows(0).Item(DataXY.iCodigoID)
    '    vEste = lodtbDatos.Rows(0).Item(DataXY.iEsteX)
    '    vNorte = lodtbDatos.Rows(0).Item(DataXY.iNorteY)
    '    Vertices(0) = New Point
    '    Vertices(0).PutCoords(vEste, vNorte)
    '    Try
    '        For i As Integer = 0 To lodtbDatos.Rows.Count - 1
    '            pStatusBar.ShowProgressBar("Processing...", 0, lodtbDatos.Rows.Count, 1, True)
    '            vEste = lodtbDatos.Rows(i).Item(DataXY.iEsteX)
    '            vNorte = lodtbDatos.Rows(i).Item(DataXY.iNorteY)
    '            vCodigo = lodtbDatos.Rows(i).Item(DataXY.iCodigoID)
    '            ''********************
    '            If vCodigo <> vCodigoSalvado Then 'si es diferente Grabar los datos
    '                pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta)
    '                pArea = pFeatureDM.Shape
    '                If p_Flag <> "1" Then
    '                    Poligono_Ok = Verifica_Existencia_XY("Catastro_1", pArea.Centroid.X, pArea.Centroid.Y, p_Zona, pApp)
    '                End If
    '                Select Case Poligono_Ok
    '                    Case ""
    '                        pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = vCodigoSalvado
    '                        pFeatureDM.Store()
    '                    Case "Si"
    '                        z = z + 1
    '                        If z = 188 Then
    '                        End If
    '                        pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "DM_1"
    '                        pFeatureDM.Value(pFeatureDM.Fields.FindField("POLIGONO")) = z
    '                        pFeatureDM.Store()
    '                    Case "No"
    '                        pFeatureDM.Delete()
    '                End Select
    '                vCodigoSalvado = vCodigo
    '                Conta = 0
    '                Vertices(Conta) = New Point
    '                Vertices(Conta).PutCoords(vEste, vNorte)
    '                vCodigoSalvado = lodtbDatos.Rows(i).Item(DataXY.iCodigoID)
    '                pFeatureDM = pFCLass.CreateFeature
    '                contador = contador + 1
    '            Else
    '                Vertices(Conta) = New Point
    '                Vertices(Conta).PutCoords(vEste, vNorte)
    '            End If
    '            Conta = Conta + 1
    '            pStatusBar.StepProgressBar()
    '        Next i
    '        pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta) 'pPolygon
    '        pArea = pFeatureDM.Shape
    '        If p_Flag <> "1" Then
    '            Poligono_Ok = Verifica_Existencia_XY("Catastro_1", pArea.Centroid.X, pArea.Centroid.Y, p_Zona, pApp)
    '        End If
    '        Select Case Poligono_Ok
    '            Case ""
    '                pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = vCodigoSalvado '"DM_" & z + 1
    '                pFeatureDM.Store()
    '            Case "Si"
    '                If pArea.Area <> 0 Then
    '                Else
    '                    MsgBox("No creo el Poligono, revisar coordenadas y ejecutar nuevamente", MsgBoxStyle.Information, "[BDGEOCATMIN]")
    '                    pFeatureDM.Delete()
    '                    pEditor.StopOperation("Delete Polygon")
    '                    pEditor.StopEditing(True)
    '                    Exit Sub
    '                End If
    '                pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "DM_1" ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)
    '                pFeatureDM.Store()
    '            Case "No"
    '                pFeatureDM.Delete()
    '        End Select
    '        pEditor.StopOperation("Add Polygon")
    '        pEditor.StopEditing(True)
    '        pStatusBar.HideProgressBar()
    '        pStatusBar.Message(0) = "Operación Completado"

    '        pFeatureDM = Nothing
    '        pFCLass = Nothing
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try
    '    '**************** 
    'End Sub
    'Private Function AdicionarConcesion2(ByRef pFCLass As IFeatureClass, ByRef Vertices() As Point, ByVal Conta As Short) As IPolygon
    '    Dim i As Integer
    '    Dim pRings(40) As IRing  'Maximo 40 anillos
    '    Dim pPolygon As IPolygon = New ESRI.ArcGIS.Geometry.PolygonClass
    '    Dim P As Integer, Parts As Integer = 0, SubPolygono As Integer = 0
    '    Dim Puntos As Point
    '    Dim pPointCollection As IPointCollection = New RingClass()
    '    Puntos = New Point
    '    Puntos.PutCoords(Vertices(0).X, Vertices(0).Y)
    '    pPointCollection.AddPoint(Puntos)
    '    For i = 1 To Conta - 1
    '        SubPolygono = NuevoSubPolygono(Vertices, Vertices(i), i - 1)
    '        Puntos = New Point
    '        Puntos.PutCoords(Vertices(i).X, Vertices(i).Y)
    '        pPointCollection.AddPoint(Puntos)
    '        If SubPolygono <> -1 Then
    '            pRings(Parts) = pPointCollection
    '            Parts = Parts + 1
    '            pPointCollection = New RingClass()
    '        End If
    '    Next
    '    If Parts <> 0 Then 'Poligono con anillos
    '        Dim pGeometryCollection As IGeometryCollection
    '        pGeometryCollection = New Polygon
    '        For P = 0 To Parts - 1
    '            pGeometryCollection.AddGeometry(pRings(P))
    '            'Debug.Print("------>" + Parts.ToString)
    '        Next
    '        pPolygon = pGeometryCollection
    '    Else ' Poligono Normal
    '        pPointCollection = New ESRI.ArcGIS.Geometry.PolygonClass 'spPolygon
    '        For i = 0 To Conta - 1
    '            Puntos = New Point
    '            Puntos.PutCoords(Vertices(i).X, Vertices(i).Y)
    '            pPointCollection.AddPoint(Puntos)
    '        Next
    '        Puntos = New Point
    '        Puntos.PutCoords(Vertices(0).X, Vertices(0).Y)
    '        pPointCollection.AddPoint(Puntos)
    '        pPolygon = pPointCollection
    '    End If
    '    AdicionarConcesion2 = pPolygon
    'End Function
    'Private Function NuevoSubPolygono(ByRef ChekearVertice() As Point, ByRef punto As Point, ByVal actual As Short) As Short
    '    Dim j As Short, Resultado As Short
    '    Resultado = -1
    '    For j = 0 To actual
    '        If punto.X = ChekearVertice(j).X And punto.Y = ChekearVertice(j).Y Then
    '            Resultado = actual + 2
    '            Exit For
    '        End If
    '    Next
    '    NuevoSubPolygono = Resultado
    'End Function
    'Function Verifica_Existencia_XY(ByVal p_Layer As String, ByVal p_x As Double, ByVal p_y As Double, ByVal p_Zona As String, ByVal pApp As IApplication)
    '    'Dim temp As Double
    '    Dim pPoint As IPoint
    '    pPoint = New Point
    '    Dim pFLayer As IFeatureLayer = Nothing
    '    pMxDoc = pApp.Document
    '    pMap = pMxDoc.FocusMap
    '    Dim afound As Boolean
    '    For A As Integer = 0 To pMap.LayerCount - 1
    '        If pMap.Layer(A).Name = p_Layer Then
    '            pFLayer = pMxDoc.FocusMap.Layer(A)
    '            afound = True
    '            Exit For
    '        End If
    '    Next A
    '    If Not afound Then
    '        MsgBox("Layer No Existe.") : Return "No" : Exit Function
    '    End If
    '    pPoint.X = p_x : pPoint.Y = p_y
    '    Dim pSpatialFilter As ISpatialFilter
    '    pSpatialFilter = New SpatialFilter
    '    With pSpatialFilter
    '        .Geometry = pPoint
    '        .SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects ' esriSpatialRelIntersects
    '    End With
    '    Dim pFeatureCursor As IFeatureCursor = pFLayer.Search(pSpatialFilter, True)
    '    Dim pRow As IRow = pFeatureCursor.NextFeature
    '    If pRow Is Nothing Then
    '        Return "No"
    '        'MsgBox("Las coordenadas Este, Norte, ó la Zona están fuera del Límite")
    '    End If
    '    Return "Si"
    'End Function
    'Public Function Open_Feature_GDB(ByRef m_application As IApplication, ByRef PathDestino As String, ByVal mFeatureClass As String) As IFeatureClass
    '    Dim pFeatureClass As IFeatureClass
    '    Dim pFeatureLayer As IFeatureLayer
    '    Dim pFeatureWorkspace As IFeatureWorkspace
    '    Dim pWorkspaceFactory As IWorkspaceFactory
    '    pMxDoc = m_application.Document
    '    pMap = pMxDoc.FocusMap
    '    pWorkspaceFactory = New AccessWorkspaceFactory 'ShapefileWorkspaceFactory
    '    pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_PathGDB, 0)
    '    pFeatureClass = pFeatureWorkspace.OpenFeatureClass(mFeatureClass)
    '    pFeatureLayer = New FeatureLayerClass
    '    pFeatureLayer.FeatureClass = pFeatureClass
    '    pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName
    '    pMap.AddLayer(pFeatureLayer)
    '    pMxDoc.ActiveView.Refresh()
    '    Open_Feature_GDB = pFeatureClass
    'End Function
    'Private Function ActivarEditor(ByRef m_application As IApplication, ByRef pFCLass As IFeatureClass) As IEditor
    '    Dim pWorkspace As IWorkspace
    '    Dim pEditor As IEditor
    '    Dim pID As New UID
    '    Dim pDataset As IDataset
    '    pID.Value = "esriCore.Editor"
    '    pEditor = m_application.FindExtensionByCLSID(pID)
    '    pDataset = pFCLass
    '    pWorkspace = pDataset.Workspace
    '    pEditor.StartEditing(pWorkspace)
    '    pEditor.StartOperation()
    '    ActivarEditor = pEditor
    'End Function
    'Private Sub SetParam(ByVal lodtbAsigna As DataTable)
    '    Dim f As Integer
    '    For f = 0 To lodtbAsigna.Columns.Count - 1
    '        If lodtbAsigna.Columns(f).ColumnName = "ESTE" Then
    '            DataXY.iEsteX = f
    '        ElseIf lodtbAsigna.Columns(f).ColumnName = "NORTE" Then 'DataXY.sNorteY Then
    '            DataXY.iNorteY = f
    '        ElseIf lodtbAsigna.Columns(f).ColumnName = "CODIGO" Then 'DataXY.sCodigoID Then
    '            DataXY.iCodigoID = f
    '        End If
    '    Next f
    'End Sub

    Public Sub Genera_Linea(ByVal lodtbTabla As DataTable, ByVal lo_Zona As String, ByVal m_Application As IApplication)
        Dim directorio As IWorkspaceFactory
        Dim tema As IFeatureLayer
        Dim fclas_tema As IFeatureClass
        Dim carpeta As IWorkspace
        'Dim pfeature As IFeature
        Dim pPoint(10, 10) As IPoint
        Try
            pMxDoc = m_Application.Document
            pMap = pMxDoc.FocusMap
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name.ToUpper = "LINEA" Then
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
            For i As Integer = 0 To lodtbTabla.Rows.Count - 2
                pfeature = fclas_tema.CreateFeature
                pPointColl = New Polyline ' Definiendo el tipo de geometria 
                pGeometryColl = New Polyline 'Asignando 
                pPoint(i, 0) = New ESRI.ArcGIS.Geometry.Point
                pPoint(i, 1) = New ESRI.ArcGIS.Geometry.Point
                pPoint(i, 0).PutCoords(lodtbTabla.Rows(i).Item("CD_COREST"), lodtbTabla.Rows(i).Item("CD_CORNOR"))
                pPoint(i, 1).PutCoords(lodtbTabla.Rows(i + 1).Item("CD_COREST"), lodtbTabla.Rows(i + 1).Item("CD_CORNOR"))
                pSegmentColl = New ESRI.ArcGIS.Geometry.Path 'Generando segmento de linea 
                pLine = New Line
                pLine.PutCoords(pPoint(i, 0), pPoint(i, 1))
                pSegmentColl.AddSegment(pLine)
                'Añadiendo la linea 
                pGeometryColl.AddGeometry(pSegmentColl)
                pfeature.Shape = pGeometryColl
                pfeature.Store()
            Next i
            pEditor.StopOperation("Add Line")
            pEditor.StopEditing(True)
            pMxDoc.ActivatedView.Refresh()
        Catch ex As Exception
            MsgBox("Generación de Línea falló", MsgBoxStyle.Information, "GisCatMin")
        End Try
    End Sub



End Class


