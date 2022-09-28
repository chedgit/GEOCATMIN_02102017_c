Imports ESRI.ArcGIS.GISClient
Imports ESRI.ArcGIS.Display
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.DataSourcesGDB
Imports esri.ArcGIS.DataSourcesOleDB
Imports ESRI.ArcGIS.GeoDatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Output
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Catalog
Imports PORTAL_Clases
Imports ESRI.ArcGIS.DataSourcesFile
Imports stdole


Public Class cls_DM_2
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
    Dim cls_Catastro As New cls_DM_1
    Function consulta_dm(ByVal lostr_Codigo_DM As String, ByVal loint_Index As Integer) As String
        Dim pPropset As IPropertySet
        pPropset = New PropertySet
        'pPropset.SetProperty("CONNECTSTRING", "Provider=MSDAORA.1;Data source=ORACLE;User ID=USERGIS;Password=USERGIS")
        'pPropset.SetProperty("CONNECTSTRING", "Provider=MSDAORA.1;Data source=DESA;User ID=USERGIS;Password=USERGIS")
        pPropset.SetProperty("CONNECTSTRING", "Provider=MSDAORA.1;Data source=DESA_DM;User ID=SISGEM;Password=SISGEM")
        'Creando sentencias del OLEDB
        Dim pWorkspace As IWorkspace
        Dim pWorkspaceFact As IWorkspaceFactory
        pWorkspaceFact = New OLEDBWorkspaceFactory
        pWorkspace = pWorkspaceFact.Open(pPropset, 0)
        Dim pFeatureWorkspace As IFeatureWorkspace
        pFeatureWorkspace = pWorkspace

        Dim pQueryDef As IQueryDef
        pQueryDef = pFeatureWorkspace.CreateQueryDef
        Select Case loint_Index
            Case 0
                'CONSTRUYENDO LA CONSULTA
                pQueryDef.Tables = " SISGEM.SG_D_PETITORIOS A, SISGEM.SC_V_COORDENADADET B "
                pQueryDef.SubFields = " A.CG_CODIGO, A.PE_NOMDER, A.CA_CODCAR, A.PE_ZONCAT, A.TE_TIPOEX, A.PE_VIGCAT," _
                                      & "A.PE_METALI, SISGEM.PACK_DBA_SG_D_PERTITULAR.ULT_VIGENTE(A.CG_CODIGO)  " _
                                      & "TITULAR, SISGEM.PACK_DBA_SG_D_HECTAEXPEDIENTE.HECTA(A.CG_CODIGO, '01') " _
                                      & " PE_HECFOR, A.SE_SITUEX, B.NC_NIVCOO,  B.CD_COREST, B.CD_CORNOR, NVL(B.CD_NUMPOL,1), " _
                                      & " B.CD_NUMVER, B.CD_VERPOL"
                pQueryDef.WhereClause = " A.CG_CODIGO = B.CG_CODIGO AND A.CG_CODIGO = '" + lostr_Codigo_DM + "' AND " _
                      & "B.NC_NIVCOO = SISGEM.PACK_DBA_SC_D_COORDENADADM.MAXNIVEL('" + lostr_Codigo_DM + "' )"
            Case 1
                pQueryDef.Tables = " SISGEM.SG_D_PETITORIOS A, SISGEM.SC_V_COORDENADADET B "
                pQueryDef.SubFields = " A.CG_CODIGO, A.PE_NOMDER, A.CA_CODCAR, A.PE_ZONCAT, A.TE_TIPOEX, A.PE_VIGCAT," _
                                      & "A.PE_METALI, SISGEM.PACK_DBA_SG_D_PERTITULAR.ULT_VIGENTE(A.CG_CODIGO)  " _
                                      & "TITULAR, SISGEM.PACK_DBA_SG_D_HECTAEXPEDIENTE.HECTA(A.CG_CODIGO, '01') " _
                                      & " PE_HECFOR, A.SE_SITUEX, B.NC_NIVCOO,  B.CD_COREST, B.CD_CORNOR, NVL(B.CD_NUMPOL,1), " _
                                      & " B.CD_NUMVER, B.CD_VERPOL"
                pQueryDef.WhereClause = " A.CG_CODIGO = B.CG_CODIGO AND A.PE_NOMDER LIKE '%" + lostr_Codigo_DM + "%'" ' AND " _
                '& "B.NC_NIVCOO = SISGEM.PACK_DBA_SC_D_COORDENADADM.MAXNIVEL('" + lostr_Codigo_DM + "' )"
        End Select
        'pQueryDef.Tables = " SISGEM.SG_D_PETITORIOS A "
        'pQueryDef.SubFields = " A.PE_ZONCAT"
        'pQueryDef.WhereClause = " A.CG_CODIGO =  '" + Codigo_DM + "'"

        Dim cursor_filas As ICursor
        Dim filas_dm As IRow
        cursor_filas = pQueryDef.Evaluate
        filas_dm = cursor_filas.NextRow
        Dim este As Double
        Dim norte As Double
        Dim j As Long
        Dim lostrZona As String = ""
        Do Until filas_dm Is Nothing
            j = j + 1
            este = filas_dm.Value(11)
            norte = filas_dm.Value(12)
            lostrZona = filas_dm.Value(3)
            filas_dm = cursor_filas.NextRow
        Loop
        filas_dm = Nothing
        filas_dm = Nothing
        pFeatureWorkspace = Nothing
        cursor_filas = Nothing
        Return lostrZona
    End Function

    Public Sub Genera_Polygon_sup(ByVal p_Consulta As String, ByVal lo_Zona As String, ByVal pApp As IApplication, ByVal p_Listbox As Object)
        Dim cls_Oracle As New cls_Oracle

        Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
        pSpatialReferenceEnv = New SpatialReferenceEnvironment
        Dim pStatusBar As ESRI.ArcGIS.esriSystem.IStatusBar
        Dim pProgbar As ESRI.ArcGIS.esriSystem.IStepProgressor
        Dim vZona As String = "18"
        Dim vCodigo, vCodigoSalvado As String
        Dim vEste, vNorte As Double
        Dim lodtbDatos As New DataTable
        Dim pFCLass As IFeatureClass
        Dim pActiveView As IActiveView
        Dim cls_eval As New Cls_evaluacion

        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        Dim afound As Boolean = False
        Dim pFeatLayer As IFeatureLayer
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Area_sup" Then
                pFeatLayer = pMxDoc.FocusMap.Layer(A)

                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("Layer No Existe.")
            Exit Sub
        End If
        pFCLass = pFeatLayer.FeatureClass

        Dim pEditor As IEditor = ActivarEditor(pApp, pFCLass)
        Dim Conta As Integer = 0
        Dim Vertices(500) As Point
        Dim vEstado As String = ""
        Dim pFeatureDM As IFeature
        pFeatureDM = pFCLass.CreateFeature
        Dim contador As Integer = 1


        Dim pQueryFilter As IQueryFilter = Nothing

        Dim n As Integer = 0
        Dim pPoint_Geo As IPoint
        Dim pFeatureDMP As IFeature
        Dim pDataset As IDataset
        Dim pFLayerDMP As IFeatureLayer
        Dim pFCLass_Point As IFeatureClass
        Dim lo_pFLayerDMP As Integer
        Dim bFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "gpt_Vertice_DM" Then
                lo_pFLayerDMP = A
                bFound = True
                Exit For
            End If
        Next A
        If Not bFound Then
            MsgBox("Could not find feature layer.")
            Exit Sub
        End If
        pFLayerDMP = pMxDoc.FocusMap.Layer(lo_pFLayerDMP)
        pFCLass_Point = pFLayerDMP.FeatureClass
        pDataset = pFCLass_Point

        pStatusBar = pApp.StatusBar
        pProgbar = pStatusBar.ProgressBar
        pProgbar.Position = 0

        Dim coordenada_DM(300) As Punto_DM
        Dim j As Integer
        Dim ptcol As IPointCollection
        Dim pt As IPoint
        Dim l As IPolygon
        l = pFeature.Shape
        ptcol = l
        Vertices(0) = New Point
        Vertices(0).PutCoords(vEste, vNorte)

        ReDim coordenada_DM(ptcol.PointCount)
        For j = 0 To ptcol.PointCount - 2
            pt = ptcol.Point(j)
            p_Listbox.Items.Add(Space(10) & RellenarComodin(j + 1, 3, "0") & Space(10) & Format(Math.Round(pt.X, 2), "###,###.00") & Space(10) & Format(Math.Round(pt.Y, 2), "###,###.00") & "")
            coordenada_DM(j).v = j + 1
            coordenada_DM(j).x = pt.X
            coordenada_DM(j).y = pt.Y
            pStatusBar.ShowProgressBar("Processing...", 0, ptcol.PointCount, 1, True)
            vEste = pt.X
            vNorte = pt.Y
            vCodigo = v_codigo

            ''********************
            If vCodigo <> vCodigoSalvado Then 'si es diferente Grabar los datos
                pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta) 'pPolygon
                If lo_Zona <> vZona Then
                    Select Case vZona
                        Case 18
                            pFeatureDM.Shape.SpatialReference = Datum_PSAD_18
                            Select Case lo_Zona
                                Case 17
                                    pFeatureDM.Shape.Project(Datum_PSAD_17)
                                Case 19
                                    pFeatureDM.Shape.Project(Datum_PSAD_19)
                            End Select
                        Case 17
                            pFeatureDM.Shape.SpatialReference = Datum_PSAD_17
                            Select Case lo_Zona
                                Case 18
                                    pFeatureDM.Shape.Project(Datum_PSAD_18)
                                Case 19
                                    pFeatureDM.Shape.Project(Datum_PSAD_19)
                            End Select
                        Case 19
                            pFeatureDM.Shape.SpatialReference = Datum_PSAD_19
                            Select Case lo_Zona
                                Case 17
                                    pFeatureDM.Shape.pProject(Datum_PSAD_17)
                                Case 18
                                    pFeatureDM.Shape.Project(Datum_PSAD_18)
                            End Select
                    End Select
                End If
                pFeatureDM.Store()
                vCodigoSalvado = vCodigo
                Conta = 0
                ReDim Vertices(500) 'inicializar variables
                Vertices(Conta) = New Point
                Vertices(Conta).PutCoords(vEste, vNorte)

                vCodigoSalvado = v_codigo
                pFeatureDM = pFCLass.CreateFeature

            Else
                Vertices(Conta) = New Point
                Vertices(Conta).PutCoords(vEste, vNorte)
            End If
            '****
            pFeatureDMP = pFCLass_Point.CreateFeature
            pPoint_Geo = New ESRI.ArcGIS.Geometry.Point
            pPoint_Geo.X = CType(vEste, Double) ' pPoint(Conta).X
            pPoint_Geo.Y = CType(vNorte, Double) 'pPoint(Conta).Y
            pFeatureDMP.Shape = pPoint_Geo
            'Get X and Y coordinates and Calculate X and Y fields
            pFeatureDMP.Value(pFeatureDMP.Fields.FindField("CODIGOU")) = v_codigo
            pFeatureDMP.Value(pFeatureDMP.Fields.FindField("VERTICE")) = Conta + 1
            pFeatureDMP.Store()
            pEditor.StopOperation("Add point")
            '****
            Conta = Conta + 1
            pStatusBar.StepProgressBar()
        Next (j) 'Loop
        pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta) 'pPolygon
        If lo_Zona <> vZona Then
            Select Case lo_Zona
                Case 18
                    pFeatureDM.Shape.SpatialReference = Datum_PSAD_18
                    Select Case vZona
                        Case 17
                            pFeatureDM.Shape.Project(Datum_PSAD_17)
                        Case 19
                            pFeatureDM.Shape.Project(Datum_PSAD_19)
                    End Select
                Case 17
                    pFeatureDM.Shape.SpatialReference = Datum_PSAD_17
                    Select Case vZona
                        Case 18
                            pFeatureDM.Shape.Project(Datum_PSAD_18)
                        Case 19
                            pFeatureDM.Shape.Project(Datum_PSAD_19)
                    End Select
                Case 19
                    pFeatureDM.Shape.SpatialReference = Datum_PSAD_19
                    Select Case vZona
                        Case 17
                            pFeatureDM.Shape.pProject(Datum_PSAD_17)
                        Case 18
                            pFeatureDM.Shape.Project(Datum_PSAD_18)
                    End Select
            End Select
        End If
        pFeatureDM.Store()
        pEditor.StopOperation("Add Polygon")
        pEditor.StopEditing(True)
        pStatusBar.HideProgressBar()
        pStatusBar.Message(0) = "Operación Completado"
        p_Listbox.ITEMS.CLEAR()
        '**************** 
    End Sub


    Public Sub Genera_Catastro_DM(ByVal p_Consulta As String, ByVal lo_Zona As String, ByVal pApp As IApplication)
        Dim cls_Oracle As New cls_Oracle
        Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
        pSpatialReferenceEnv = New SpatialReferenceEnvironment
        Dim pStatusBar As ESRI.ArcGIS.esriSystem.IStatusBar
        Dim pProgbar As ESRI.ArcGIS.esriSystem.IStepProgressor
        Dim vZona As String = "18"
        Dim vCodigo, vCodigoSalvado As String
        Dim vEste, vNorte As Double
        Dim lodtbDatos As New DataTable
        lodtbDatos = cls_Oracle.F_Obtiene_Datos_DM(p_Consulta)
        Dim strShapeFileName As String = "Catastro"
        Dim pFCLass As IFeatureClass = Open_Feature_GDB(pApp, "", strShapeFileName)
        cls_Catastro.Delete_Rows_FC_GDB("Catastro")
        Dim pEditor As IEditor = ActivarEditor(pApp, pFCLass)
        Dim Conta As Integer = 0
        Dim Vertices(500) As Point
        Dim L As Integer
        Dim vEstado As String = ""
        Dim pFeatureDM As IFeature
        pFeatureDM = pFCLass.CreateFeature
        Dim contador As Integer = 1
        SetParam(lodtbDatos)
        Dim FieldDataCount As Integer = lodtbDatos.Columns.Count - 4
        ReDim DataInfo(FieldDataCount)
        Dim n As Integer = 0
        '************
        Dim pPoint_Geo As IPoint
        Dim pFeatureDMP As IFeature
        Dim pDataset As IDataset
        Dim pFLayerDMP As IFeatureLayer
        Dim pFCLass_Point As IFeatureClass
        Dim lo_pFLayerDMP As Integer
        Dim bFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "gpt_Vertice_DM" Then
                lo_pFLayerDMP = A
                bFound = True
                Exit For
            End If
        Next A
        If Not bFound Then
            MsgBox("Could not find feature layer.")
            Exit Sub
        End If
        pFLayerDMP = pMxDoc.FocusMap.Layer(lo_pFLayerDMP)
        pFCLass_Point = pFLayerDMP.FeatureClass
        pDataset = pFCLass_Point
        '**********************
        For f As Integer = 0 To FieldDataCount - 1
            If lodtbDatos.Columns(f).ColumnName <> "CD_COREST" And lodtbDatos.Columns(f).ColumnName <> "CD_CORNOR" Then
                DataInfo(n).Index = f 'almaceno los campos 
                DataInfo(n).Campo = lodtbDatos.Columns(f).ColumnName
                n = n + 1
            End If
        Next f
        For L = 0 To FieldDataCount - 1
            pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
            pFeatureDM.Value(L + 3) = lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
            Exit For
        Next
        pStatusBar = pApp.StatusBar
        pProgbar = pStatusBar.ProgressBar
        pProgbar.Position = 0
        vCodigoSalvado = lodtbDatos.Rows(0).Item(DataXY.iCodigoID) ' pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
        vEste = lodtbDatos.Rows(0).Item(DataXY.iEsteX) 'pRow.Value(DataXY.iEsteX)
        vNorte = lodtbDatos.Rows(0).Item(DataXY.iNorteY) ' pRow.Value(DataXY.iNorteY)
        Vertices(0) = New Point
        Vertices(0).PutCoords(vEste, vNorte)
        'Do While Not pRow Is Nothing
        For i As Integer = 0 To lodtbDatos.Rows.Count - 1
            pStatusBar.ShowProgressBar("Processing...", 0, lodtbDatos.Rows.Count, 1, True)
            vEste = lodtbDatos.Rows(i).Item(DataXY.iEsteX) 'pRow.Value(DataXY.iEsteX) 'pRow.Value(pTable.Fields.FindField("ESTE"))
            vNorte = lodtbDatos.Rows(i).Item(DataXY.iNorteY) 'pRow.Value(DataXY.iNorteY) 'pRow.Value(pTable.Fields.FindField("NORTE"))
            vCodigo = lodtbDatos.Rows(i).Item(DataXY.iCodigoID) 'pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
            ''********************
            If vCodigo <> vCodigoSalvado Then 'si es diferente Grabar los datos
                pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta) 'pPolygon
                If lo_Zona <> vZona Then
                    Select Case vZona
                        Case 18
                            pFeatureDM.Shape.SpatialReference = Datum_PSAD_18
                            Select Case lo_Zona
                                Case 17
                                    pFeatureDM.Shape.Project(Datum_PSAD_17)
                                Case 19
                                    pFeatureDM.Shape.Project(Datum_PSAD_19)
                            End Select
                        Case 17
                            pFeatureDM.Shape.SpatialReference = Datum_PSAD_17
                            Select Case lo_Zona
                                Case 18
                                    pFeatureDM.Shape.Project(Datum_PSAD_18)
                                Case 19
                                    pFeatureDM.Shape.Project(Datum_PSAD_19)
                            End Select
                        Case 19
                            pFeatureDM.Shape.SpatialReference = Datum_PSAD_19
                            Select Case lo_Zona
                                Case 17
                                    pFeatureDM.Shape.pProject(Datum_PSAD_17)
                                Case 18
                                    pFeatureDM.Shape.Project(Datum_PSAD_18)
                            End Select
                    End Select
                End If
                pFeatureDM.Store()
                vCodigoSalvado = vCodigo
                Conta = 0
                ReDim Vertices(500) 'inicializar variables
                Vertices(Conta) = New Point
                Vertices(Conta).PutCoords(vEste, vNorte)
                vCodigoSalvado = lodtbDatos.Rows(0).Item(DataXY.iCodigoID) 'pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
                pFeatureDM = pFCLass.CreateFeature
                For L = 0 To FieldDataCount - 1 ' cargo detalle
                    'pFeatureDM.Value(L + 2) = lodtbDatos.Rows(L).Item(DataInfo(L + 1).Index) ' pRow.Value(DataInfo(L + 1).Index)
                    pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
                    pFeatureDM.Value(L + 3) = lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
                Next
                contador = contador + 1
                pFeatureDM.Value(19) = contador ' Campo Contador
            Else
                Vertices(Conta) = New Point
                Vertices(Conta).PutCoords(vEste, vNorte)
            End If
            '****
            pFeatureDMP = pFCLass_Point.CreateFeature
            pPoint_Geo = New ESRI.ArcGIS.Geometry.Point
            pPoint_Geo.X = CType(vEste, Double) ' pPoint(Conta).X
            pPoint_Geo.Y = CType(vNorte, Double) 'pPoint(Conta).Y
            pFeatureDMP.Shape = pPoint_Geo
            'Get X and Y coordinates and Calculate X and Y fields
            pFeatureDMP.Value(pFeatureDMP.Fields.FindField("CODIGOU")) = lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'contador
            pFeatureDMP.Value(pFeatureDMP.Fields.FindField("VERTICE")) = Conta + 1
            pFeatureDMP.Store()
            pEditor.StopOperation("Add point")
            '****
            Conta = Conta + 1
            pStatusBar.StepProgressBar()
        Next (i) 'Loop
        pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta) 'pPolygon
        If lo_Zona <> vZona Then
            Select Case lo_Zona
                Case 18
                    pFeatureDM.Shape.SpatialReference = Datum_PSAD_18
                    Select Case vZona
                        Case 17
                            pFeatureDM.Shape.Project(Datum_PSAD_17)
                        Case 19
                            pFeatureDM.Shape.Project(Datum_PSAD_19)
                    End Select
                Case 17
                    pFeatureDM.Shape.SpatialReference = Datum_PSAD_17
                    Select Case vZona
                        Case 18
                            pFeatureDM.Shape.Project(Datum_PSAD_18)
                        Case 19
                            pFeatureDM.Shape.Project(Datum_PSAD_19)
                    End Select
                Case 19
                    pFeatureDM.Shape.SpatialReference = Datum_PSAD_19
                    Select Case vZona
                        Case 17
                            pFeatureDM.Shape.pProject(Datum_PSAD_17)
                        Case 18
                            pFeatureDM.Shape.Project(Datum_PSAD_18)
                    End Select
            End Select
        End If
        pFeatureDM.Store()
        pEditor.StopOperation("Add Polygon")
        pEditor.StopEditing(True)
        pStatusBar.HideProgressBar()
        pStatusBar.Message(0) = "Operación Completado"
        '**************** 
    End Sub

    Public Sub Genera_Catastro_DMX(ByVal strShapeFileName As String, ByVal lo_Zona As String, ByVal pApp As IApplication)
        Dim cls_Oracle As New cls_Oracle
        Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
        pSpatialReferenceEnv = New SpatialReferenceEnvironment
        Dim pStatusBar As ESRI.ArcGIS.esriSystem.IStatusBar
        Dim pProgbar As ESRI.ArcGIS.esriSystem.IStepProgressor
        Dim vZona As String = "18"
        Dim vCodigo, vCodigoSalvado As String
        Dim vEste, vNorte As Double
        Dim lodtbDatos As New DataTable
        Dim lo_pFLayerDMP1 As IFeatureLayer = Nothing

        Dim pFCLass As IFeatureClass
        'lodtbDatos = cls_Oracle.F_Obtiene_Datos_DM(p_Consulta)
        'Dim strShapeFileName As String = "Catastro"
        'Dim pFCLass As IFeatureClass = Open_Feature_GDB(pApp, "", strShapeFileName)
        'cls_Catastro.Delete_Rows_FC_GDB("Catastro")

        Dim bFound1 As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Catastro" Then
                lo_pFLayerDMP1 = pMap.Layer(A)
                bFound1 = True
                Exit For
            End If
        Next A
        If Not bFound1 Then
            MsgBox("Could not find feature layer.")
            Exit Sub
        End If

        pFCLass = lo_pFLayerDMP1.FeatureClass

        Dim pEditor As IEditor = ActivarEditor(pApp, pFCLass)
        Dim Conta As Integer = 0
        Dim Vertices(500) As Point
        Dim L As Integer
        Dim vEstado As String = ""
        Dim pFeatureDM As IFeature
        pFeatureDM = pFCLass.CreateFeature
        Dim contador As Integer = 1
        SetParam(lodtbDatos)
        'Dim FieldDataCount As Integer = lodtbDatos.Columns.Count - 4
        Dim FieldDataCount As Integer = 4
        ReDim DataInfo(FieldDataCount)
        Dim n As Integer = 0
        '************
        Dim pPoint_Geo As IPoint
        Dim pFeatureDMP As IFeature
        Dim pDataset As IDataset
        Dim pFLayerDMP As IFeatureLayer
        Dim pFCLass_Point As IFeatureClass
        Dim lo_pFLayerDMP As Integer
        Dim bFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "gpt_Vertice_DM" Then
                lo_pFLayerDMP = A
                bFound = True
                Exit For
            End If
        Next A
        If Not bFound Then
            MsgBox("Could not find feature layer.")
            Exit Sub
        End If
        pFLayerDMP = pMxDoc.FocusMap.Layer(lo_pFLayerDMP)
        pFCLass_Point = pFLayerDMP.FeatureClass
        pDataset = pFCLass_Point
        '**********************
        For f As Integer = 0 To FieldDataCount - 1
            If lodtbDatos.Columns(f).ColumnName <> "CD_COREST" And lodtbDatos.Columns(f).ColumnName <> "CD_CORNOR" Then
                DataInfo(n).Index = f 'almaceno los campos 
                DataInfo(n).Campo = lodtbDatos.Columns(f).ColumnName
                n = n + 1
            End If
        Next f
        For L = 0 To FieldDataCount - 1
            pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
            pFeatureDM.Value(L + 3) = lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
            Exit For
        Next
        pStatusBar = pApp.StatusBar
        pProgbar = pStatusBar.ProgressBar
        pProgbar.Position = 0
        vCodigoSalvado = lodtbDatos.Rows(0).Item(DataXY.iCodigoID) ' pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
        vEste = lodtbDatos.Rows(0).Item(DataXY.iEsteX) 'pRow.Value(DataXY.iEsteX)
        vNorte = lodtbDatos.Rows(0).Item(DataXY.iNorteY) ' pRow.Value(DataXY.iNorteY)
        Vertices(0) = New Point
        Vertices(0).PutCoords(vEste, vNorte)
        'Do While Not pRow Is Nothing
        For i As Integer = 0 To lodtbDatos.Rows.Count - 1
            pStatusBar.ShowProgressBar("Processing...", 0, lodtbDatos.Rows.Count, 1, True)
            vEste = lodtbDatos.Rows(i).Item(DataXY.iEsteX) 'pRow.Value(DataXY.iEsteX) 'pRow.Value(pTable.Fields.FindField("ESTE"))
            vNorte = lodtbDatos.Rows(i).Item(DataXY.iNorteY) 'pRow.Value(DataXY.iNorteY) 'pRow.Value(pTable.Fields.FindField("NORTE"))
            vCodigo = lodtbDatos.Rows(i).Item(DataXY.iCodigoID) 'pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
            ''********************
            If vCodigo <> vCodigoSalvado Then 'si es diferente Grabar los datos
                pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta) 'pPolygon
                If lo_Zona <> vZona Then
                    Select Case vZona
                        Case 18
                            pFeatureDM.Shape.SpatialReference = Datum_PSAD_18
                            Select Case lo_Zona
                                Case 17
                                    pFeatureDM.Shape.Project(Datum_PSAD_17)
                                Case 19
                                    pFeatureDM.Shape.Project(Datum_PSAD_19)
                            End Select
                        Case 17
                            pFeatureDM.Shape.SpatialReference = Datum_PSAD_17
                            Select Case lo_Zona
                                Case 18
                                    pFeatureDM.Shape.Project(Datum_PSAD_18)
                                Case 19
                                    pFeatureDM.Shape.Project(Datum_PSAD_19)
                            End Select
                        Case 19
                            pFeatureDM.Shape.SpatialReference = Datum_PSAD_19
                            Select Case lo_Zona
                                Case 17
                                    pFeatureDM.Shape.pProject(Datum_PSAD_17)
                                Case 18
                                    pFeatureDM.Shape.Project(Datum_PSAD_18)
                            End Select
                    End Select
                End If
                pFeatureDM.Store()
                vCodigoSalvado = vCodigo
                Conta = 0
                ReDim Vertices(500) 'inicializar variables
                Vertices(Conta) = New Point
                Vertices(Conta).PutCoords(vEste, vNorte)
                vCodigoSalvado = lodtbDatos.Rows(0).Item(DataXY.iCodigoID) 'pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
                pFeatureDM = pFCLass.CreateFeature
                For L = 0 To FieldDataCount - 1 ' cargo detalle
                    'pFeatureDM.Value(L + 2) = lodtbDatos.Rows(L).Item(DataInfo(L + 1).Index) ' pRow.Value(DataInfo(L + 1).Index)
                    pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
                    pFeatureDM.Value(L + 3) = lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
                Next
                contador = contador + 1
                pFeatureDM.Value(19) = contador ' Campo Contador
            Else
                Vertices(Conta) = New Point
                Vertices(Conta).PutCoords(vEste, vNorte)
            End If
            '****
            pFeatureDMP = pFCLass_Point.CreateFeature
            pPoint_Geo = New ESRI.ArcGIS.Geometry.Point
            pPoint_Geo.X = CType(vEste, Double) ' pPoint(Conta).X
            pPoint_Geo.Y = CType(vNorte, Double) 'pPoint(Conta).Y
            pFeatureDMP.Shape = pPoint_Geo
            'Get X and Y coordinates and Calculate X and Y fields
            pFeatureDMP.Value(pFeatureDMP.Fields.FindField("CODIGOU")) = lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'contador
            pFeatureDMP.Value(pFeatureDMP.Fields.FindField("VERTICE")) = Conta + 1
            pFeatureDMP.Store()
            pEditor.StopOperation("Add point")
            '****
            Conta = Conta + 1
            pStatusBar.StepProgressBar()
        Next (i) 'Loop
        pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta) 'pPolygon
        If lo_Zona <> vZona Then
            Select Case lo_Zona
                Case 18
                    pFeatureDM.Shape.SpatialReference = Datum_PSAD_18
                    Select Case vZona
                        Case 17
                            pFeatureDM.Shape.Project(Datum_PSAD_17)
                        Case 19
                            pFeatureDM.Shape.Project(Datum_PSAD_19)
                    End Select
                Case 17
                    pFeatureDM.Shape.SpatialReference = Datum_PSAD_17
                    Select Case vZona
                        Case 18
                            pFeatureDM.Shape.Project(Datum_PSAD_18)
                        Case 19
                            pFeatureDM.Shape.Project(Datum_PSAD_19)
                    End Select
                Case 19
                    pFeatureDM.Shape.SpatialReference = Datum_PSAD_19
                    Select Case vZona
                        Case 17
                            pFeatureDM.Shape.pProject(Datum_PSAD_17)
                        Case 18
                            pFeatureDM.Shape.Project(Datum_PSAD_18)
                    End Select
            End Select
        End If
        pFeatureDM.Store()
        pEditor.StopOperation("Add Polygon")
        pEditor.StopEditing(True)
        pStatusBar.HideProgressBar()
        pStatusBar.Message(0) = "Operación Completado"
        '**************** 
    End Sub


    Public Sub Genera_Malla100Ha(ByVal p_Consulta As String, ByVal lo_Zona As String, ByVal pApp As IApplication)
        Dim cls_Oracle As New cls_Oracle
        Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
        pSpatialReferenceEnv = New SpatialReferenceEnvironment
        Dim pStatusBar As ESRI.ArcGIS.esriSystem.IStatusBar
        Dim pProgbar As ESRI.ArcGIS.esriSystem.IStepProgressor
        Dim vZona As String = "18"
        Dim vCodigo, vCodigoSalvado As String
        Dim vEste, vNorte As Double
        Dim lodtbDatos As New DataTable
        lodtbDatos = cls_Oracle.F_Obtiene_Datos_DM(p_Consulta)
        Dim strShapeFileName As String = "Malla_Area_18"
        Dim pFCLass As IFeatureClass = Open_Feature_GDB(pApp, "", strShapeFileName)
        'cls_Catastro.Delete_Rows_FC("Catastro")
        Dim pEditor As IEditor = ActivarEditor(pApp, pFCLass)
        Dim Conta As Integer = 0
        Dim Vertices(500) As Point
        Dim L As Integer
        Dim vEstado As String = ""
        Dim pFeatureDM As IFeature
        pFeatureDM = pFCLass.CreateFeature
        Dim contador As Integer = 1
        SetParam(lodtbDatos)
        Dim FieldDataCount As Integer = lodtbDatos.Columns.Count - 4
        ReDim DataInfo(FieldDataCount)
        Dim n As Integer = 0
        '************
        'Dim pPoint_Geo As IPoint
        'Dim pFeatureDMP As IFeature
        'Dim pDataset As IDataset
        'Dim pFLayerDMP As IFeatureLayer
        'Dim pFCLass_Point As IFeatureClass
        'Dim lo_pFLayerDMP As Integer
        'Dim bFound As Boolean = False
        'For A As Integer = 0 To pMap.LayerCount - 1
        '    If pMap.Layer(A).Name = "gpt_Vertice_DM" Then
        '        lo_pFLayerDMP = A
        '        bFound = True
        '        Exit For
        '    End If
        'Next A
        'If Not bFound Then
        '    MsgBox("Could not find feature layer.")
        '    Exit Sub
        'End If
        'pFLayerDMP = pMxDoc.FocusMap.Layer(lo_pFLayerDMP)
        'pFCLass_Point = pFLayerDMP.FeatureClass
        'pDataset = pFCLass_Point
        '**********************
        For f As Integer = 0 To FieldDataCount - 1
            If lodtbDatos.Columns(f).ColumnName <> "CD_COREST" And lodtbDatos.Columns(f).ColumnName <> "CD_CORNOR" Then
                DataInfo(n).Index = f 'almaceno los campos 
                DataInfo(n).Campo = lodtbDatos.Columns(f).ColumnName
                n = n + 1
            End If
        Next f
        For L = 0 To FieldDataCount - 1
            pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
            pFeatureDM.Value(L + 3) = lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
            Exit For
        Next
        pStatusBar = pApp.StatusBar
        pProgbar = pStatusBar.ProgressBar
        pProgbar.Position = 0
        vCodigoSalvado = lodtbDatos.Rows(0).Item(DataXY.iCodigoID) ' pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
        vEste = lodtbDatos.Rows(0).Item(DataXY.iEsteX) 'pRow.Value(DataXY.iEsteX)
        vNorte = lodtbDatos.Rows(0).Item(DataXY.iNorteY) ' pRow.Value(DataXY.iNorteY)
        Vertices(0) = New Point
        Vertices(0).PutCoords(vEste, vNorte)
        'Do While Not pRow Is Nothing
        For i As Integer = 0 To lodtbDatos.Rows.Count - 1
            pStatusBar.ShowProgressBar("Processing...", 0, lodtbDatos.Rows.Count, 1, True)
            vEste = lodtbDatos.Rows(i).Item(DataXY.iEsteX) 'pRow.Value(DataXY.iEsteX) 'pRow.Value(pTable.Fields.FindField("ESTE"))
            vNorte = lodtbDatos.Rows(i).Item(DataXY.iNorteY) 'pRow.Value(DataXY.iNorteY) 'pRow.Value(pTable.Fields.FindField("NORTE"))
            vCodigo = lodtbDatos.Rows(i).Item(DataXY.iCodigoID) 'pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
            ''********************
            If vCodigo <> vCodigoSalvado Then 'si es diferente Grabar los datos
                pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta) 'pPolygon
                If lo_Zona <> vZona Then
                    Select Case vZona
                        Case 18
                            pFeatureDM.Shape.SpatialReference = Datum_PSAD_18
                            Select Case lo_Zona
                                Case 17
                                    pFeatureDM.Shape.Project(Datum_PSAD_17)
                                Case 19
                                    pFeatureDM.Shape.Project(Datum_PSAD_19)
                            End Select
                        Case 17
                            pFeatureDM.Shape.SpatialReference = Datum_PSAD_17
                            Select Case lo_Zona
                                Case 18
                                    pFeatureDM.Shape.Project(Datum_PSAD_18)
                                Case 19
                                    pFeatureDM.Shape.Project(Datum_PSAD_19)
                            End Select
                        Case 19
                            pFeatureDM.Shape.SpatialReference = Datum_PSAD_19
                            Select Case lo_Zona
                                Case 17
                                    pFeatureDM.Shape.pProject(Datum_PSAD_17)
                                Case 18
                                    pFeatureDM.Shape.Project(Datum_PSAD_18)
                            End Select
                    End Select
                End If
                pFeatureDM.Store()
                vCodigoSalvado = vCodigo
                Conta = 0
                ReDim Vertices(500) 'inicializar variables
                Vertices(Conta) = New Point
                Vertices(Conta).PutCoords(vEste, vNorte)
                vCodigoSalvado = lodtbDatos.Rows(0).Item(DataXY.iCodigoID) 'pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
                pFeatureDM = pFCLass.CreateFeature
                For L = 0 To FieldDataCount - 1 ' cargo detalle
                    'pFeatureDM.Value(L + 2) = lodtbDatos.Rows(L).Item(DataInfo(L + 1).Index) ' pRow.Value(DataInfo(L + 1).Index)
                    pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
                    pFeatureDM.Value(L + 3) = lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
                Next
                contador = contador + 1
                pFeatureDM.Value(19) = contador ' Campo Contador
            Else
                Vertices(Conta) = New Point
                Vertices(Conta).PutCoords(vEste, vNorte)
            End If
            '****
            'pFeatureDMP = pFCLass_Point.CreateFeature
            'pPoint_Geo = New ESRI.ArcGIS.Geometry.Point
            'pPoint_Geo.X = CType(vEste, Double) ' pPoint(Conta).X
            'pPoint_Geo.Y = CType(vNorte, Double) 'pPoint(Conta).Y
            'pFeatureDMP.Shape = pPoint_Geo
            ''Get X and Y coordinates and Calculate X and Y fields
            'pFeatureDMP.Value(pFeatureDMP.Fields.FindField("CODIGOU")) = lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'contador
            'pFeatureDMP.Value(pFeatureDMP.Fields.FindField("VERTICE")) = Conta + 1
            'pFeatureDMP.Store()
            'pEditor.StopOperation("Add point")
            '****
            Conta = Conta + 1
            pStatusBar.StepProgressBar()
        Next (i) 'Loop
        pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta) 'pPolygon
        If lo_Zona <> vZona Then
            Select Case lo_Zona
                Case 18
                    pFeatureDM.Shape.SpatialReference = Datum_PSAD_18
                    Select Case vZona
                        Case 17
                            pFeatureDM.Shape.Project(Datum_PSAD_17)
                        Case 19
                            pFeatureDM.Shape.Project(Datum_PSAD_19)
                    End Select
                Case 17
                    pFeatureDM.Shape.SpatialReference = Datum_PSAD_17
                    Select Case vZona
                        Case 18
                            pFeatureDM.Shape.Project(Datum_PSAD_18)
                        Case 19
                            pFeatureDM.Shape.Project(Datum_PSAD_19)
                    End Select
                Case 19
                    pFeatureDM.Shape.SpatialReference = Datum_PSAD_19
                    Select Case vZona
                        Case 17
                            pFeatureDM.Shape.pProject(Datum_PSAD_17)
                        Case 18
                            pFeatureDM.Shape.Project(Datum_PSAD_18)
                    End Select
            End Select
        End If
        pFeatureDM.Store()
        pEditor.StopOperation("Add Polygon")
        pEditor.StopEditing(True)
        pStatusBar.HideProgressBar()
        pStatusBar.Message(0) = "Operación Completado"
        '**************** 
    End Sub
    Private Function DataToICursor(ByRef Path As String, ByRef TablaOrigen As String) As ITable
        pWorkspaceFactory = New AccessWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile("", 0)
        Dim pTable As ITable
        pTable = pFeatureWorkspace.OpenTable(TablaOrigen) 'TableName + ".dbf")
        DataToICursor = pTable
    End Function
    Public Function Open_Feature_GDB(ByRef m_application As IApplication, ByRef PathDestino As String, ByVal mFeatureClass As String) As IFeatureClass
        Dim pFeatureClass As IFeatureClass
        Dim pFeatureLayer As IFeatureLayer
        Dim pFeatureWorkspace As IFeatureWorkspace
        Dim pWorkspaceFactory As IWorkspaceFactory
        pMxDoc = m_application.Document
        pMap = pMxDoc.FocusMap
        pWorkspaceFactory = New AccessWorkspaceFactory 'ShapefileWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_PathGDB, 0)
        'Debug.Print(PathDestino)
        pFeatureClass = pFeatureWorkspace.OpenFeatureClass(mFeatureClass)
        pFeatureLayer = New FeatureLayerClass
        pFeatureLayer.FeatureClass = pFeatureClass
        pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName
        pMap.AddLayer(pFeatureLayer)
        pMxDoc.ActiveView.Refresh()
        Open_Feature_GDB = pFeatureClass
    End Function
    Public Function Open_Feature_SHP(ByRef m_application As IApplication, ByRef PathDestino As String, ByVal mFeatureClass As String) As IFeatureClass
        Dim pFeatureClass As IFeatureClass
        Dim pFeatureLayer As IFeatureLayer
        Dim pFeatureWorkspace As IFeatureWorkspace
        Dim pWorkspaceFactory As IWorkspaceFactory
        pMxDoc = m_application.Document
        pMap = pMxDoc.FocusMap
        'pWorkspaceFactory = New AccessWorkspaceFactory 'ShapefileWorkspaceFactory
        pWorkspaceFactory = New ShapefileWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_pathTMP, 0)
        'Debug.Print(PathDestino)
        pFeatureClass = pFeatureWorkspace.OpenFeatureClass(mFeatureClass)
        pFeatureLayer = New FeatureLayerClass
        pFeatureLayer.FeatureClass = pFeatureClass
        Select Case Mid(mFeatureClass, 1, 8)
            Case "Frontera"
                pFeatureLayer.Name = "Frontera" ' pFeatureLayer.FeatureClass.AliasName
            Case "Poligono"
                pFeatureLayer.Name = "Poligono" ' pFeatureLayer.FeatureClass.AliasName
            Case "Polyline"
                pFeatureLayer.Name = "Polylinea" ' pFeatureLayer.FeatureClass.AliasName
            Case "Puntosss"
                pFeatureLayer.Name = "Punto" ' pFeatureLayer.FeatureClass.AliasName
            Case Else
                pFeatureLayer.Name = "DM_Simulado" ' pFeatureLayer.FeatureClass.AliasName
                pFeatureLayer.Visible = True
        End Select

        pMap.AddLayer(pFeatureLayer)
        pMxDoc.ActiveView.Refresh()
        Open_Feature_SHP = pFeatureClass


    End Function

    Private Function ActivarEditor(ByRef m_application As IApplication, ByRef pFCLass As IFeatureClass) As IEditor
        Dim pWorkspace As IWorkspace
        Dim pEditor As IEditor
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
    Private Function QueryCarta(ByRef pTable As ITable, ByRef Query As String) As ICursor
        Dim pQueryFilter As IQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = Query ' "CARTA = '" & "16-G" & "'" ' query
        Dim pCursor As ICursor
        pCursor = pTable.Search(pQueryFilter, False)
        QueryCarta = pCursor
    End Function
    Private Function ListRow(ByRef pTable As ITable, ByRef Query As String) As Integer
        Dim pQueryFilter As IQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = Query
        Dim i As Integer = pTable.RowCount(pQueryFilter)
        ListRow = i
    End Function
    Private Sub SetParam(ByVal lodtbAsigna As DataTable)
        Dim f As Integer
        For f = 0 To lodtbAsigna.Columns.Count - 1 '(f).ColumnName - 1 '.Rows.Count - 1 '.Fields.FieldCount - 1
            If lodtbAsigna.Columns(f).ColumnName = "CD_COREST" Then '.Fields.Field(f).Name = "ESTE" Then 'DataXY.sEsteX Then
                DataXY.iEsteX = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "CD_CORNOR" Then 'DataXY.sNorteY Then
                DataXY.iNorteY = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "CG_CODIGO" Then 'DataXY.sCodigoID Then
                DataXY.iCodigoID = f
            End If
        Next f
    End Sub
    Private Function AdicionarConcesion2(ByRef pFCLass As IFeatureClass, ByRef Vertices() As Point, ByVal Conta As Short) As IPolygon
        Dim i As Integer
        'Dim pRings(40) As IRing  'Maximo 40 anillos
        Dim pPolygon As IPolygon = New ESRI.ArcGIS.Geometry.PolygonClass
        'pPolygon = New ESRI.ArcGIS.Geometry.PolygonClass
        'Dim P As Integer, Parts As Integer = 0, SubPolygono As Integer = 0
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
        AdicionarConcesion2 = pPolygon
    End Function
    Private Function AdicionarConcesion3(ByRef pFCLass As IFeatureClass, ByRef Vertices() As Point, ByVal Conta As Short) As IPolyline
        Dim i As Integer
        'Dim pRings(40) As IRing  'Maximo 40 anillos
        ' Dim pPolygon As IPolygon = New ESRI.ArcGIS.Geometry.PolygonClass

        Dim pPolygon As IPolyline = New ESRI.ArcGIS.Geometry.PolylineClass
        'Dim P As Integer, Parts As Integer = 0, SubPolygono As Integer = 0
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
            'pGeometryCollection = New Polygon
            pGeometryCollection = New Polyline

            For P = 0 To Parts - 1
                pGeometryCollection.AddGeometry(pRings(P))
                'Debug.Print("------>" + Parts.ToString)
            Next
            pPolygon = pGeometryCollection
        Else ' Poligono Normal
            'pPointCollection = New ESRI.ArcGIS.Geometry.PolygonClass 'spPolygon
            pPointCollection = New ESRI.ArcGIS.Geometry.PolylineClass 'spPolygon
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
        AdicionarConcesion3 = pPolygon
    End Function
    Private Function AdicionarConcesion4(ByRef pFCLass As IFeatureClass, ByRef Vertices() As Point, ByVal Conta As Short) As IPoint

        Dim i As Integer
        Dim pRings(40) As IRing  'Maximo 40 anillos
        'Dim pPolygon As IPolygon = New ESRI.ArcGIS.Geometry.PolygonClass
        Dim pPolygon As Point = New ESRI.ArcGIS.Geometry.PointClass
        'Dim P As Integer, Parts As Integer = 0, SubPolygono As Integer = 0
        Dim P As Integer, Parts As Integer = 0, SubPolygono = 0

        Dim Puntos As Point
        Dim pPointCollection As IPointCollection = New RingClass()
        'Dim pPointCollection As IPointCollection = New ESRI.ArcGIS.Geometry.PointClass

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
            pGeometryCollection = New Point
            For P = 0 To Parts - 1
                pGeometryCollection.AddGeometry(pRings(P))
                'Debug.Print("------>" + Parts.ToString)
            Next
            pPolygon = pGeometryCollection
        Else ' Poligono Normal
            pPointCollection = New ESRI.ArcGIS.Geometry.PointClass
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
        AdicionarConcesion4 = pPolygon
    End Function

    Private Function NuevoSubPolygono(ByRef ChekearVertice() As Point, ByRef punto As Point, ByVal actual As Short) As Short
        Dim j As Short, Resultado As Short
        Resultado = -1
        For j = 0 To actual
            If punto.X = ChekearVertice(j).X And punto.Y = ChekearVertice(j).Y Then
                Resultado = actual + 2
                Exit For
            End If
        Next
        NuevoSubPolygono = Resultado
    End Function
    Public Sub Genera_Catastro_NuevoM(ByVal p_ShapeFile As String, ByVal lodtbDatos As DataTable, ByVal p_Zona As String, ByVal pApp As IApplication, ByVal p_Existe As Object)
        Dim cls_Oracle As New cls_Oracle
        Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
        pSpatialReferenceEnv = New SpatialReferenceEnvironment
        Dim pStatusBar As ESRI.ArcGIS.esriSystem.IStatusBar
        Dim pProgbar As ESRI.ArcGIS.esriSystem.IStepProgressor
        Dim vCodigo, vCodigoSalvado As String
        Dim vEste, vNorte As Double
        Dim pFCLass As IFeatureClass = Open_Feature_SHP(pApp, "", p_ShapeFile)
        cls_Catastro.Delete_Rows_FC_SHP(p_ShapeFile)
        Dim pEditor As IEditor = ActivarEditor(pApp, pFCLass)
        Dim Conta As Integer = 0
        Dim Vertices(500) As Point
        Dim L As Integer
        Dim vEstado As String = ""
        Dim pFeatureDM As IFeature
        pFeatureDM = pFCLass.CreateFeature
        Dim contador As Integer = 1
        SetParam(lodtbDatos)
        Dim FieldDataCount As Integer = lodtbDatos.Columns.Count ' - 4
        ReDim DataInfo(FieldDataCount)
        Dim n As Integer = 0
        Dim pPoint_Geo As IPoint
        Dim pFeatureDMP As IFeature
        Dim pDataset As IDataset
        Dim pFLayerDMP As IFeatureLayer
        Dim pFCLass_Point As IFeatureClass
        Dim lo_pFLayerDMP As Integer
        Dim bFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "gpt_Vertice_DM" Then
                lo_pFLayerDMP = A
                bFound = True
                Exit For
            End If
        Next A
        If Not bFound Then
            MsgBox("Could not find feature layer.")
            Exit Sub
        End If
        pFLayerDMP = pMxDoc.FocusMap.Layer(lo_pFLayerDMP)
        pFCLass_Point = pFLayerDMP.FeatureClass
        pDataset = pFCLass_Point
        '**********************
        For f As Integer = 0 To FieldDataCount - 1
            If lodtbDatos.Columns(f).ColumnName <> "CD_COREST" And lodtbDatos.Columns(f).ColumnName <> "CD_CORNOR" Then
                DataInfo(n).Index = f 'almaceno los campos 
                DataInfo(n).Campo = lodtbDatos.Columns(f).ColumnName
                n = n + 1
            End If
        Next f
        For L = 0 To FieldDataCount - 1
            pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
            pFeatureDM.Value(L + 3) = lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
            Exit For
        Next
        pStatusBar = pApp.StatusBar
        pProgbar = pStatusBar.ProgressBar
        pProgbar.Position = 0
        vCodigoSalvado = lodtbDatos.Rows(0).Item(DataXY.iCodigoID) ' pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
        vEste = lodtbDatos.Rows(0).Item(DataXY.iEsteX) 'pRow.Value(DataXY.iEsteX)
        vNorte = lodtbDatos.Rows(0).Item(DataXY.iNorteY) ' pRow.Value(DataXY.iNorteY)
        Vertices(0) = New Point
        Vertices(0).PutCoords(vEste, vNorte)
        'Do While Not pRow Is Nothing
        For i As Integer = 0 To lodtbDatos.Rows.Count - 1
            pStatusBar.ShowProgressBar("Processing...", 0, lodtbDatos.Rows.Count, 1, True)
            vEste = lodtbDatos.Rows(i).Item(DataXY.iEsteX) 'pRow.Value(DataXY.iEsteX) 'pRow.Value(pTable.Fields.FindField("ESTE"))
            vNorte = lodtbDatos.Rows(i).Item(DataXY.iNorteY) 'pRow.Value(DataXY.iNorteY) 'pRow.Value(pTable.Fields.FindField("NORTE"))
            vCodigo = lodtbDatos.Rows(i).Item(DataXY.iCodigoID) 'pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
            ''********************
            If vCodigo <> vCodigoSalvado Then 'si es diferente Grabar los datos
                pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta) 'pPolygon
                pFeatureDM.Store()
                vCodigoSalvado = vCodigo
                Conta = 0
                ReDim Vertices(500) 'inicializar variables
                Vertices(Conta) = New Point
                Vertices(Conta).PutCoords(vEste, vNorte)
                vCodigoSalvado = lodtbDatos.Rows(0).Item(DataXY.iCodigoID) 'pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
                pFeatureDM = pFCLass.CreateFeature
                For L = 0 To FieldDataCount - 1 ' cargo detalle
                    pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
                    pFeatureDM.Value(L + 3) = lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
                Next
                contador = contador + 1
                pFeatureDM.Value(19) = contador ' Campo Contador
            Else
                Vertices(Conta) = New Point
                Vertices(Conta).PutCoords(vEste, vNorte)
            End If
            '****
            pFeatureDMP = pFCLass_Point.CreateFeature
            pPoint_Geo = New ESRI.ArcGIS.Geometry.Point
            pPoint_Geo.X = CType(vEste, Double) ' pPoint(Conta).X
            pPoint_Geo.Y = CType(vNorte, Double) 'pPoint(Conta).Y
            Select Case p_Zona
                Case 17
                    pPoint_Geo.SpatialReference = Datum_PSAD_18
                    pPoint_Geo.Project(Datum_PSAD_17)
                Case 19
                    pPoint_Geo.SpatialReference = Datum_PSAD_18
                    pPoint_Geo.Project(Datum_PSAD_19)
            End Select
            pFeatureDMP.Shape = pPoint_Geo
            pFeatureDMP.Value(pFeatureDMP.Fields.FindField("CODIGOU")) = lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'contador
            pFeatureDMP.Value(pFeatureDMP.Fields.FindField("VERTICE")) = Conta + 1
            pFeatureDMP.Store()
            pEditor.StopOperation("Add point")
            '****
            Conta = Conta + 1
            pStatusBar.StepProgressBar()
        Next (i) 'Loop
        pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta) 'pPolygon
        Dim pArea As IArea
        pArea = pFeatureDM.Shape
        If pArea.Area <> 0 Then
            p_Existe.text = 0
        Else
            MsgBox("No creo el Poligono, revisar coordenadas y ejecutar nuevamente", MsgBoxStyle.Information, "[BDGEOCATMIN]")
            pFeatureDM.Delete()
            pEditor.StopOperation("Delete Polygon")
            pEditor.StopEditing(True)
            p_Existe.text = -1
            pStatusBar.HideProgressBar()
            pStatusBar.Message(0) = "Operación Completado"
            Exit Sub
        End If
        pFeatureDM.Store()
        pEditor.StopOperation("Add Polygon")
        pEditor.StopEditing(True)
        pStatusBar.HideProgressBar()
        pStatusBar.Message(0) = "Operación Completado"
    End Sub
    Public Sub Genera_Poligono_100Ha(ByVal lodtbDatos As DataTable, ByVal p_Zona As String, ByVal pApp As IApplication, Optional ByVal p_Flag As String = "")
        Dim z As Integer = 1
        Dim loIntContPoligono As Integer = 1
        Dim Poligono_Ok As String = ""
        Dim cls_Oracle As New cls_Oracle
        Dim pArea As IArea
        Dim pStatusBar As ESRI.ArcGIS.esriSystem.IStatusBar
        Dim pProgbar As ESRI.ArcGIS.esriSystem.IStepProgressor
        Dim vZona As String = "18"
        Dim vCodigo, vCodigoSalvado As String
        Dim vEste, vNorte As Double
        Dim strShapeFileName As String = ""
        If p_Flag = "1" Then
            strShapeFileName = "gpo_geo_aster"
        ElseIf p_Flag = "2" Then
            strShapeFileName = "ZonaUrbana_10Ha"
        ElseIf p_Flag = "3" Then
            strShapeFileName = "AreaReserva_100Ha"
        ElseIf p_Flag = "4" Then
            strShapeFileName = "Recta_" & p_Zona
        End If
        Dim pFCLass As IFeatureClass = Open_Feature_GDB(pApp, "", strShapeFileName)
        Dim pEditor As IEditor = ActivarEditor(pApp, pFCLass)
        Dim Conta As Integer = 0
        Dim Vertices(400) As Point
        'Dim L As Integer
        Dim vEstado As String = ""
        Dim pFeatureDM As IFeature
        pFeatureDM = pFCLass.CreateFeature
        Dim contador As Integer = 1
        SetParam(lodtbDatos)
        Dim FieldDataCount As Integer = lodtbDatos.Columns.Count ' - 4
        ReDim DataInfo(FieldDataCount)
        Dim n As Integer = 0
        pStatusBar = pApp.StatusBar
        pProgbar = pStatusBar.ProgressBar
        pProgbar.Position = 0

        vCodigoSalvado = lodtbDatos.Rows(0).Item(DataXY.iCodigoID) ' pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
        vEste = lodtbDatos.Rows(0).Item(DataXY.iEsteX) 'pRow.Value(DataXY.iEsteX)
        vNorte = lodtbDatos.Rows(0).Item(DataXY.iNorteY) ' pRow.Value(DataXY.iNorteY)
        Vertices(0) = New Point
        Vertices(0).PutCoords(vEste, vNorte)
        Try
            For i As Integer = 0 To lodtbDatos.Rows.Count - 1
                pStatusBar.ShowProgressBar("Processing...", 0, lodtbDatos.Rows.Count, 1, True)
                vEste = lodtbDatos.Rows(i).Item(DataXY.iEsteX) 'pRow.Value(DataXY.iEsteX) 'pRow.Value(pTable.Fields.FindField("ESTE"))
                vNorte = lodtbDatos.Rows(i).Item(DataXY.iNorteY) 'pRow.Value(DataXY.iNorteY) 'pRow.Value(pTable.Fields.FindField("NORTE"))
                vCodigo = lodtbDatos.Rows(i).Item(DataXY.iCodigoID) 'pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
                ''********************
                If vCodigo <> vCodigoSalvado Then 'si es diferente Grabar los datos
                    pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta) 'pPolygon
                    pArea = pFeatureDM.Shape
                    Select Case p_Zona
                        Case "17"
                            pFeatureDM.Shape.SpatialReference = Datum_PSAD_17
                            pFeatureDM.Shape.Project(Datum_PSAD_18)
                        Case "19"
                            pFeatureDM.Shape.SpatialReference = Datum_PSAD_19
                            pFeatureDM.Shape.Project(Datum_PSAD_18)
                    End Select
                    If p_Flag <> "1" Then
                        Poligono_Ok = Verifica_Existencia_XY("Catastro_1", pArea.Centroid.X, pArea.Centroid.Y, gloZona, pApp)
                    End If
                    Select Case Poligono_Ok
                        Case ""
                            pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "DM_" & z + 1
                            'pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = z + 1
                            ''pFeatureDM.Value(pFeatureDM.Fields.FindField("POLIGONO")) = z + 1 'Conta
                            pFeatureDM.Store()
                        Case "Si"
                            If sele_cuadri = "100" Then
                                Select Case z
                                    Case 1
                                        'pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "Area_" & z ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)
                                        pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "A" 'lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)

                                    Case 2
                                        pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "B" ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)

                                    Case 3
                                        pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "C" ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)

                                    Case 4
                                        pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "D" ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)

                                    Case 5
                                        pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "E"  ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)

                                    Case 6
                                        pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "F"  ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)

                                    Case 7
                                        pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "G"  ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)

                                    Case 8
                                        pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "H"  ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)
                                    Case 9
                                        pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "I" ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)
                                    Case 10
                                        pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "J" ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)

                                End Select

                                pFeatureDM.Value(pFeatureDM.Fields.FindField("AREA")) = 100.0
                            ElseIf sele_cuadri = "10" Then
                                pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = z 'Conta
                                pFeatureDM.Value(pFeatureDM.Fields.FindField("AREA")) = 10.0
                            End If

                            pFeatureDM.Value(pFeatureDM.Fields.FindField("TIPO")) = "PE"
                            pFeatureDM.Value(pFeatureDM.Fields.FindField("POLIGONO")) = z 'Conta
                            z = z + 1
                            pFeatureDM.Store()
                        Case "No"
                            'pFeatureDM.Store()
                            pFeatureDM.Delete()
                    End Select
                    vCodigoSalvado = vCodigo
                    Conta = 0
                    Vertices(Conta) = New Point
                    Vertices(Conta).PutCoords(vEste, vNorte)
                    vCodigoSalvado = lodtbDatos.Rows(i).Item(DataXY.iCodigoID) 'pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
                    pFeatureDM = pFCLass.CreateFeature
                    contador = contador + 1
                Else
                    Vertices(Conta) = New Point
                    Vertices(Conta).PutCoords(vEste, vNorte)
                End If
                Conta = Conta + 1
                pStatusBar.StepProgressBar()
            Next i 'Loop
            pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta) 'pPolygon
            pArea = pFeatureDM.Shape
            Select Case p_Zona
                Case "17"
                    pFeatureDM.Shape.SpatialReference = Datum_PSAD_17
                    pFeatureDM.Shape.Project(Datum_PSAD_18)
                Case "19"
                    pFeatureDM.Shape.SpatialReference = Datum_PSAD_19
                    pFeatureDM.Shape.Project(Datum_PSAD_18)
            End Select
            If p_Flag <> "1" Then
                Poligono_Ok = Verifica_Existencia_XY("Catastro_1", pArea.Centroid.X, pArea.Centroid.Y, gloZona, pApp)
            End If
            Select Case Poligono_Ok
                Case ""
                    pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "DM_" & z + 1
                    pFeatureDM.Store()
                Case "Si"
                    If pArea.Area <> 0 Then
                    Else
                        MsgBox("No creo el Poligono, revisar coordenadas y ejecutar nuevamente", MsgBoxStyle.Information, "[BDGEOCATMIN]")
                        pFeatureDM.Delete()
                        pEditor.StopOperation("Delete Polygon")
                        pEditor.StopEditing(True)
                        Exit Sub
                    End If
                    'pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "Area_" & z ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)
                    If sele_cuadri = "100" Then
                        Select Case z


                            Case 1
                                'pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "Area_" & z ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)
                                pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "A" 'lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)
                            Case 2
                                pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "B" ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)

                            Case 3
                                pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "C" ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)
                            Case 4
                                pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "D" ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)
                            Case 5
                                pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "E"  ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)
                            Case 6
                                pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "F"  ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)
                            Case 7
                                pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "G"  ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)
                            Case 8
                                pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "H"  ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)
                            Case 9
                                pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "I" ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)
                            Case 10
                                pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "J" ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)
                        End Select
                        pFeatureDM.Value(pFeatureDM.Fields.FindField("AREA")) = 100.0
                        'pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = z ' + 1 'Conta
                    ElseIf sele_cuadri = "10" Then
                        pFeatureDM.Value(pFeatureDM.Fields.FindField("AREA")) = 10.0
                        pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = z ' + 1 'Conta

                    End If

                    pFeatureDM.Value(pFeatureDM.Fields.FindField("POLIGONO")) = z ' + 1 'Conta
                    pFeatureDM.Value(pFeatureDM.Fields.FindField("TIPO")) = "PE"
                    'pFeatureDM.Value(pFeatureDM.Fields.FindField("POLIGONO")) = z + 1 'Conta
                    pFeatureDM.Store()
                Case "No"
                    'pFeatureDM.Store()
                    pFeatureDM.Delete()
            End Select
            pEditor.StopOperation("Add Polygon")
            pEditor.StopEditing(True)
            pStatusBar.HideProgressBar()
            pStatusBar.Message(0) = "Operación Completado"

            pFeatureDM = Nothing
            pFCLass = Nothing
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        '**************** 
    End Sub

    Function Verifica_Existencia_XY(ByVal p_Layer As String, ByVal p_x As Double, ByVal p_y As Double, ByVal p_Zona As String, ByVal pApp As IApplication)
        'Dim temp As Double
        Dim pPoint As IPoint
        pPoint = New Point
        Dim pFLayer As IFeatureLayer = Nothing
        pMxDoc = pApp.Document
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
            MsgBox("Layer No Existe.") : Return "No" : Exit Function
        End If
        pPoint.X = p_x : pPoint.Y = p_y
        Dim pSpatialFilter As ISpatialFilter
        pSpatialFilter = New SpatialFilter
        With pSpatialFilter
            .Geometry = pPoint
            .SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects ' esriSpatialRelIntersects
        End With
        Dim pFeatureCursor As IFeatureCursor = pFLayer.Search(pSpatialFilter, True)
        Dim pRow As IRow = pFeatureCursor.NextFeature
        If pRow Is Nothing Then
            Return "No"
            'MsgBox("Las coordenadas Este, Norte, ó la Zona están fuera del Límite")
        End If
        Return "Si"
    End Function
    Public Sub Genera_Poligono_One(ByVal p_ShapeFile As String, ByVal lodtbDatos As DataTable, ByVal p_Zona As String, ByVal pApp As IApplication, ByVal p_Existe As Object)
        Dim cls_Oracle As New cls_Oracle
        Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
        pSpatialReferenceEnv = New SpatialReferenceEnvironment
        Dim pStatusBar As ESRI.ArcGIS.esriSystem.IStatusBar
        Dim pProgbar As ESRI.ArcGIS.esriSystem.IStepProgressor
        Dim vCodigo, vCodigoSalvado As String
        Dim vEste, vNorte As Double
        Dim pFCLass As IFeatureClass = Open_Feature_SHP(pApp, "", p_ShapeFile)
        ' cls_Catastro.Delete_Rows_FC_SHP(p_ShapeFile)
        Dim pEditor As IEditor = ActivarEditor(pApp, pFCLass)
        Dim Conta As Integer = 0
        Dim Vertices(500) As Point
        Dim L As Integer
        Dim vEstado As String = ""
        Dim pFeatureDM As IFeature
        pFeatureDM = pFCLass.CreateFeature
        Dim contador As Integer = 1
        SetParam(lodtbDatos)
        Dim FieldDataCount As Integer = lodtbDatos.Columns.Count ' - 4
        ReDim DataInfo(FieldDataCount)
        Dim n As Integer = 0
        Dim bFound As Boolean = False
        For f As Integer = 0 To FieldDataCount - 1
            If lodtbDatos.Columns(f).ColumnName <> "CD_COREST" And lodtbDatos.Columns(f).ColumnName <> "CD_CORNOR" Then
                DataInfo(n).Index = f 'almaceno los campos 
                DataInfo(n).Campo = lodtbDatos.Columns(f).ColumnName
                n = n + 1
            End If
        Next f
        For L = 0 To FieldDataCount - 1
            pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
            pFeatureDM.Value(L + 3) = lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
            Exit For
        Next
        pStatusBar = pApp.StatusBar
        pProgbar = pStatusBar.ProgressBar
        pProgbar.Position = 0
        vCodigoSalvado = lodtbDatos.Rows(0).Item(DataXY.iCodigoID) ' pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
        vEste = lodtbDatos.Rows(0).Item(DataXY.iEsteX) 'pRow.Value(DataXY.iEsteX)
        vNorte = lodtbDatos.Rows(0).Item(DataXY.iNorteY) ' pRow.Value(DataXY.iNorteY)
        Vertices(0) = New Point
        Vertices(0).PutCoords(vEste, vNorte)
        'Do While Not pRow Is Nothing
        For i As Integer = 0 To lodtbDatos.Rows.Count - 1
            pStatusBar.ShowProgressBar("Processing...", 0, lodtbDatos.Rows.Count, 1, True)
            vEste = lodtbDatos.Rows(i).Item(DataXY.iEsteX) 'pRow.Value(DataXY.iEsteX) 'pRow.Value(pTable.Fields.FindField("ESTE"))
            vNorte = lodtbDatos.Rows(i).Item(DataXY.iNorteY) 'pRow.Value(DataXY.iNorteY) 'pRow.Value(pTable.Fields.FindField("NORTE"))
            vCodigo = lodtbDatos.Rows(i).Item(DataXY.iCodigoID) 'pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
            ''********************
            If vCodigo <> vCodigoSalvado Then 'si es diferente Grabar los datos
                pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta) 'pPolygon
                pFeatureDM.Store()
                vCodigoSalvado = vCodigo
                Conta = 0
                ReDim Vertices(500) 'inicializar variables
                Vertices(Conta) = New Point
                Vertices(Conta).PutCoords(vEste, vNorte)
                vCodigoSalvado = lodtbDatos.Rows(0).Item(DataXY.iCodigoID) 'pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
                pFeatureDM = pFCLass.CreateFeature
                For L = 0 To FieldDataCount - 1 ' cargo detalle
                    pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
                    pFeatureDM.Value(L + 3) = lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
                Next
                contador = contador + 1
                pFeatureDM.Value(19) = contador ' Campo Contador
            Else
                Vertices(Conta) = New Point
                Vertices(Conta).PutCoords(vEste, vNorte)
            End If
            Conta = Conta + 1
            pStatusBar.StepProgressBar()
        Next (i) 'Loop
        pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta) 'pPolygon
        Dim pArea As IArea
        pArea = pFeatureDM.Shape
        If pArea.Area <> 0 Then
            p_Existe.text = 0
        Else
            MsgBox("No creo el Poligono de Frontera", MsgBoxStyle.Information, "[BDGEOCATMIN]")
            pFeatureDM.Delete()
            pEditor.StopOperation("Delete Polygon")
            pEditor.StopEditing(True)
            p_Existe.text = -1
            pStatusBar.HideProgressBar()
            pStatusBar.Message(0) = "Operación Completado"
            Exit Sub
        End If
        pFeatureDM.Store()
        pEditor.StopOperation("Add Polygon")
        pEditor.StopEditing(True)
        pStatusBar.HideProgressBar()
        pStatusBar.Message(0) = "Operación Completado"
    End Sub

    Public Sub Genera_Catastro_Nuevo_polylinea(ByVal p_ShapeFile As String, ByVal lodtbDatos As DataTable, ByVal p_Zona As String, ByVal m_Application As IApplication)

        Dim cls_Oracle As New cls_Oracle
        Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
        pSpatialReferenceEnv = New SpatialReferenceEnvironment
        Dim pStatusBar As ESRI.ArcGIS.esriSystem.IStatusBar
        Dim pProgbar As ESRI.ArcGIS.esriSystem.IStepProgressor
        Dim vCodigo, vCodigoSalvado As String
        Dim vEste, vNorte As Double
        Dim pFCLass As IFeatureClass = Open_Feature_SHP(m_Application, "", p_ShapeFile)
        cls_Catastro.Delete_Rows_FC_SHP(p_ShapeFile)
        Dim pEditor As IEditor = ActivarEditor(m_Application, pFCLass)
        Dim Conta As Integer = 0
        Dim Vertices(500) As Point
        Dim L As Integer
        Dim vEstado As String = ""
        Dim pFeatureDM As IFeature
        pFeatureDM = pFCLass.CreateFeature
        Dim contador As Integer = 1
        SetParam(lodtbDatos)
        Dim FieldDataCount As Integer = lodtbDatos.Columns.Count ' - 4
        ReDim DataInfo(FieldDataCount)
        Dim n As Integer = 0
        Dim pPoint_Geo As IPoint
        Dim pFeatureDMP As IFeature
        Dim pDataset As IDataset
        Dim pFLayerDMP As IFeatureLayer
        Dim pFCLass_Point As IFeatureClass
        Dim lo_pFLayerDMP As Integer
        Dim bFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "gpt_Vertice_DM" Then
                lo_pFLayerDMP = A
                bFound = True
                Exit For
            End If
        Next A
        If Not bFound Then
            MsgBox("Could not find feature layer.")
            Exit Sub
        End If
        pFLayerDMP = pMxDoc.FocusMap.Layer(lo_pFLayerDMP)
        pFCLass_Point = pFLayerDMP.FeatureClass
        pDataset = pFCLass_Point
        '**********************
        For f As Integer = 0 To FieldDataCount - 1
            If lodtbDatos.Columns(f).ColumnName <> "CD_COREST" And lodtbDatos.Columns(f).ColumnName <> "CD_CORNOR" Then
                DataInfo(n).Index = f 'almaceno los campos 
                DataInfo(n).Campo = lodtbDatos.Columns(f).ColumnName
                n = n + 1
            End If
        Next f
        For L = 0 To FieldDataCount - 1
            pFeatureDM.Value(L + 2) = contador
            pFeatureDM.Value(L + 3) = lodtbDatos.Rows(L).Item(DataInfo(L).Index)
            Exit For
        Next
        contador = contador + 1
        pStatusBar = m_Application.StatusBar
        pProgbar = pStatusBar.ProgressBar
        pProgbar.Position = 0
        vCodigoSalvado = lodtbDatos.Rows(0).Item(DataXY.iCodigoID)
        vEste = lodtbDatos.Rows(0).Item(DataXY.iEsteX)
        vNorte = lodtbDatos.Rows(0).Item(DataXY.iNorteY)
        Vertices(0) = New Point
        Vertices(0).PutCoords(vEste, vNorte)
        For i As Integer = 0 To lodtbDatos.Rows.Count - 1
            pStatusBar.ShowProgressBar("Processing...", 0, lodtbDatos.Rows.Count, 1, True)
            vEste = lodtbDatos.Rows(i).Item(DataXY.iEsteX)
            vNorte = lodtbDatos.Rows(i).Item(DataXY.iNorteY)
            vCodigo = lodtbDatos.Rows(i).Item(DataXY.iCodigoID)
            ''********************
            If vCodigo <> vCodigoSalvado Then 'si es diferente Grabar los datos
                pFeatureDM.Shape = AdicionarConcesion3(pFCLass, Vertices, Conta) 'pPolygon
                pFeatureDM.Store()
                vCodigoSalvado = vCodigo
                Conta = 0
                ReDim Vertices(500) 'inicializar variables
                Vertices(Conta) = New Point
                Vertices(Conta).PutCoords(vEste, vNorte)
                vCodigoSalvado = vCodigo
                'vCodigoSalvado = lodtbDatos.Rows(0).Item(DataXY.iCodigoID)
                pFeatureDM = pFCLass.CreateFeature
                For L = 0 To FieldDataCount - 1 ' cargo detalle
                    pFeatureDM.Value(L + 2) = contador
                    pFeatureDM.Value(L + 3) = lodtbDatos.Rows(L).Item(DataInfo(L).Index)
                    Exit For
                Next
                contador = contador + 1
                'pFeatureDM.Value(19) = contador ' Campo Contador
            Else
                Vertices(Conta) = New Point
                Vertices(Conta).PutCoords(vEste, vNorte)
            End If
            '****
            pFeatureDMP = pFCLass_Point.CreateFeature
            pPoint_Geo = New ESRI.ArcGIS.Geometry.Point
            pPoint_Geo.X = CType(vEste, Double)
            pPoint_Geo.Y = CType(vNorte, Double)
            Select Case p_Zona
                Case 17
                    pPoint_Geo.SpatialReference = Datum_PSAD_18
                    pPoint_Geo.Project(Datum_PSAD_17)
                Case 19
                    pPoint_Geo.SpatialReference = Datum_PSAD_18
                    pPoint_Geo.Project(Datum_PSAD_19)
            End Select
            pFeatureDMP.Shape = pPoint_Geo
            pFeatureDMP.Value(pFeatureDMP.Fields.FindField("CODIGOU")) = lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'contador
            pFeatureDMP.Value(pFeatureDMP.Fields.FindField("VERTICE")) = Conta + 1
            pFeatureDMP.Store()
            pEditor.StopOperation("Add point")
            '****
            Conta = Conta + 1
            pStatusBar.StepProgressBar()
            pFeatureDM.Shape = AdicionarConcesion3(pFCLass, Vertices, Conta) 'pPolygon
        Next (i) 'Loop
        'pFeatureDM.Shape = AdicionarConcesion3(pFCLass, Vertices, Conta) 'pPolygon
        'Dim pArea As IArea
        'pArea = pFeatureDM.Shape
        'If pArea.Area <> 0 Then
        'Else
        'MsgBox("No creo La polylinEa, revisar coordenadas y ejecutar nuevamente", MsgBoxStyle.Information, "[Aviso]")
        'pFeatureDM.Delete()
        'pEditor.StopOperation("Delete Polygon")
        'pEditor.StopEditing(True)
        'pStatusBar.HideProgressBar()
        'pStatusBar.Message(0) = "Operación Completado"
        'Exit Sub
        'End If
        pFeatureDM.Store()
        'pEditor.StopOperation("Add Polygon")
        pEditor.StopOperation("Add Line")

        pEditor.StopEditing(True)
        pStatusBar.HideProgressBar()
        pStatusBar.Message(0) = "Operación Completado"
    End Sub
    Public Sub Genera_Catastro_Nuevo_po(ByVal p_ShapeFile As String, ByVal lodtbDatos As DataTable, ByVal p_Zona As String, ByVal pApp As IApplication)
        Dim cls_Oracle As New cls_Oracle
        Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
        pSpatialReferenceEnv = New SpatialReferenceEnvironment
        Dim pStatusBar As ESRI.ArcGIS.esriSystem.IStatusBar
        Dim pProgbar As ESRI.ArcGIS.esriSystem.IStepProgressor
        Dim vCodigo, vCodigoSalvado As String
        Dim vEste, vNorte As Double
        Dim pFCLass As IFeatureClass = Open_Feature_SHP(pApp, "", p_ShapeFile)
        cls_Catastro.Delete_Rows_FC_SHP(p_ShapeFile)
        Dim pEditor As IEditor = ActivarEditor(pApp, pFCLass)
        Dim Conta As Integer = 0
        Dim Vertices(500) As Point
        Dim L As Integer
        Dim vEstado As String = ""
        Dim pFeatureDM As IFeature
        pFeatureDM = pFCLass.CreateFeature
        Dim contador As Integer = 1
        SetParam(lodtbDatos)
        Dim FieldDataCount As Integer = lodtbDatos.Columns.Count ' - 4
        ReDim DataInfo(FieldDataCount)
        Dim n As Integer = 0
        Dim pPoint_Geo As IPoint
        Dim pFeatureDMP As IFeature
        Dim pDataset As IDataset
        Dim pFLayerDMP As IFeatureLayer
        Dim pFCLass_Point As IFeatureClass
        Dim lo_pFLayerDMP As Integer
        Dim bFound As Boolean = False
        Dim vCodigo1 As String = ""
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "gpt_Vertice_DM" Then
                lo_pFLayerDMP = A
                bFound = True
                Exit For
            End If
        Next A
        If Not bFound Then
            MsgBox("Could not find feature layer.")
            Exit Sub
        End If
        pFLayerDMP = pMxDoc.FocusMap.Layer(lo_pFLayerDMP)
        pFCLass_Point = pFLayerDMP.FeatureClass
        pDataset = pFCLass_Point
        '**********************
        For f As Integer = 0 To FieldDataCount - 1
            If lodtbDatos.Columns(f).ColumnName <> "CD_COREST" And lodtbDatos.Columns(f).ColumnName <> "CD_CORNOR" Then
                DataInfo(n).Index = f 'almaceno los campos 
                DataInfo(n).Campo = lodtbDatos.Columns(f).ColumnName
                'lodtbDatos.Rows(i).Item(DataXY.iCodigoID)
                n = n + 1
            End If
        Next f
        For L = 0 To FieldDataCount - 1
            
            pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
            pFeatureDM.Value(L + 3) = lodtbDatos.Rows(0).Item(DataXY.iCodigoID)
            Exit For
        Next
        contador = contador + 1
        pStatusBar = pApp.StatusBar
        pProgbar = pStatusBar.ProgressBar
        pProgbar.Position = 0
        vCodigoSalvado = lodtbDatos.Rows(0).Item(DataXY.iCodigoID) ' pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
        vEste = lodtbDatos.Rows(0).Item(DataXY.iEsteX) 'pRow.Value(DataXY.iEsteX)
        vNorte = lodtbDatos.Rows(0).Item(DataXY.iNorteY) ' pRow.Value(DataXY.iNorteY)
        Vertices(0) = New Point
        Vertices(0).PutCoords(vEste, vNorte)

        For i As Integer = 0 To lodtbDatos.Rows.Count - 1
            pStatusBar.ShowProgressBar("Processing...", 0, lodtbDatos.Rows.Count, 1, True)
            vEste = lodtbDatos.Rows(i).Item(DataXY.iEsteX) 'pRow.Value(DataXY.iEsteX) 'pRow.Value(pTable.Fields.FindField("ESTE"))
            vNorte = lodtbDatos.Rows(i).Item(DataXY.iNorteY) 'pRow.Value(DataXY.iNorteY) 'pRow.Value(pTable.Fields.FindField("NORTE"))
            vCodigo = lodtbDatos.Rows(i).Item(DataXY.iCodigoID) 'pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
            ''********************
            If vCodigo <> vCodigoSalvado Then 'si es diferente Grabar los datos
                pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta) 'pPolygon
                pFeatureDM.Store()
                vCodigoSalvado = vCodigo
                Conta = 0
                ReDim Vertices(500) 'inicializar variables
                Vertices(Conta) = New Point
                Vertices(Conta).PutCoords(vEste, vNorte)
                vCodigoSalvado = vCodigo
                pFeatureDM = pFCLass.CreateFeature
                For L = 0 To FieldDataCount - 1 ' cargo detalle
                    'pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
                    'pFeatureDM.Value(L + 3) = lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
                    pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
                    pFeatureDM.Value(L + 3) = vCodigoSalvado
                    Exit For
                Next
                contador = contador + 1
            Else
                Vertices(Conta) = New Point
                Vertices(Conta).PutCoords(vEste, vNorte)
            End If

            pFeatureDMP = pFCLass_Point.CreateFeature
            pPoint_Geo = New ESRI.ArcGIS.Geometry.Point
            pPoint_Geo.X = CType(vEste, Double) ' pPoint(Conta).X
            pPoint_Geo.Y = CType(vNorte, Double) 'pPoint(Conta).Y
            Select Case p_Zona
                Case 17
                    pPoint_Geo.SpatialReference = Datum_PSAD_18
                    pPoint_Geo.Project(Datum_PSAD_17)
                Case 19
                    pPoint_Geo.SpatialReference = Datum_PSAD_18
                    pPoint_Geo.Project(Datum_PSAD_19)
            End Select
            pFeatureDMP.Shape = pPoint_Geo
            pFeatureDMP.Value(pFeatureDMP.Fields.FindField("CODIGOU")) = lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'contador
            pFeatureDMP.Value(pFeatureDMP.Fields.FindField("VERTICE")) = Conta + 1
            pFeatureDMP.Store()
            pEditor.StopOperation("Add point")

            Conta = Conta + 1
            pStatusBar.StepProgressBar()
            pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta) 'pPolygon
        Next (i)
        Dim pArea As IArea
        pArea = pFeatureDM.Shape
        If pArea.Area <> 0 Then
            'p_Existe.text = 0
        Else
            MsgBox("No Se creo el Poligono Correctamente, revise sus coordenadas de su table y ejecutar nuevamente", MsgBoxStyle.Information, "[BDGEOCATMIN]")
            pFeatureDM.Delete()
            pEditor.StopOperation("Delete Polygon")
            pEditor.StopEditing(True)
            'p_Existe.text = -1
            pStatusBar.HideProgressBar()
            pStatusBar.Message(0) = "Operación Completado"
            Exit Sub
        End If
        pFeatureDM.Store()
        pEditor.StopOperation("Add Polygon")
        pEditor.StopEditing(True)
        pStatusBar.HideProgressBar()
        pStatusBar.Message(0) = "Operación Completado"
    End Sub


    Public Sub Genera_Catastro_Nuevo_Point(ByVal p_ShapeFile As String, ByVal lodtbDatos As DataTable, ByVal p_Zona As String, ByVal pApp As IApplication)

        Dim cls_Oracle As New cls_Oracle
        Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
        pSpatialReferenceEnv = New SpatialReferenceEnvironment
        
        Dim vCodigo As String
        Dim vEste, vNorte As Double
        Dim pFCLass As IFeatureClass = Open_Feature_SHP(pApp, "", p_ShapeFile)
        'cls_Catastro.Delete_Rows_FC_SHP(p_ShapeFile)
        Dim pEditor As IEditor = ActivarEditor(pApp, pFCLass)
        Dim Conta As Integer = 0
        Dim Vertices(500) As Point
        Dim L As Integer
        Dim vEstado As String = ""
        
        Dim contador As Integer = 1
        SetParam(lodtbDatos)
        Dim FieldDataCount As Integer = lodtbDatos.Columns.Count ' - 4
        ReDim DataInfo(FieldDataCount)
        Dim n As Integer = 0
        Dim pPoint_Geo As IPoint
        Dim pFeatureDMP As IFeature
        Dim pDataset As IDataset
        Dim pFLayerDMP As IFeatureLayer
        Dim pFCLass_Point As IFeatureClass
        Dim lo_pFLayerDMP As Integer
        Dim bFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            'If pMap.Layer(A).Name = "gpt_Vertice_DM" Then
            If pMap.Layer(A).Name = "Punto" Then
                lo_pFLayerDMP = A
                bFound = True
                Exit For
            End If
        Next A
        If Not bFound Then
            MsgBox("Could not find feature layer.")
            Exit Sub
        End If
        pFLayerDMP = pMxDoc.FocusMap.Layer(lo_pFLayerDMP)
        pFCLass_Point = pFLayerDMP.FeatureClass
        pDataset = pFCLass_Point
        Dim pPointCollection As IPointCollection = New RingClass()
        Conta = 0
        Dim punto As Point = New Point
        For i As Integer = 0 To lodtbDatos.Rows.Count - 1
            'ReDim Vertices(500)
            vEste = lodtbDatos.Rows(i).Item(DataXY.iEsteX)
            vNorte = lodtbDatos.Rows(i).Item(DataXY.iNorteY)
            vCodigo = lodtbDatos.Rows(i).Item(DataXY.iCodigoID)
            Vertices(Conta) = New Point
            Vertices(Conta).PutCoords(vEste, vNorte)
            pFCLass_Point = pFLayerDMP.FeatureClass
            pFeatureDMP = pFCLass_Point.CreateFeature
            pPoint_Geo = New ESRI.ArcGIS.Geometry.Point
            pPoint_Geo.X = CType(vEste, Double)
            pPoint_Geo.Y = CType(vNorte, Double)

            Select Case p_Zona
                Case 17
                    pPoint_Geo.SpatialReference = Datum_PSAD_18
                    pPoint_Geo.Project(Datum_PSAD_17)
                Case 19
                    pPoint_Geo.SpatialReference = Datum_PSAD_18
                    pPoint_Geo.Project(Datum_PSAD_19)
            End Select
            pPoint_Geo.PutCoords(pPoint_Geo.X, pPoint_Geo.Y)
            pFeatureDMP.Shape = pPoint_Geo
            pFeatureDMP.Value(pFeatureDMP.Fields.FindField("CODIGO")) = vCodigo 'contador
            pFeatureDMP.Value(pFeatureDMP.Fields.FindField("CONTADOR")) = Conta + 1
            pFeatureDMP.Store()
            pEditor.StopOperation("Add point")
            Conta = Conta + 1
        Next (i)
        pEditor.StopEditing(True)
        
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
            MsgBox(".::Error - Eliminado Rows a FeatureClass::.", vbInformation, "Aviso")
        End Try
    End Sub
    Public Sub Conexion_Shapefile()
        pWorkspaceFactory = New ShapefileWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_pathTMP, 0)
    End Sub
    Public Sub AddLayerFileToMap(ByVal lo_Path As String, ByVal lo_LayerFile As String, ByVal m_Application As IApplication)
        Dim pGxLayer As IGxLayer
        Dim pGxFile As IGxFile
        pGxLayer = New GxLayer
        pGxFile = pGxLayer
        pGxFile.Path = lo_Path & lo_LayerFile 'filePath
        Dim pMxDoc As IMxDocument
        pMxDoc = m_Application.Document
        pMxDoc.FocusMap.AddLayer(pGxLayer.Layer)
    End Sub
End Class
