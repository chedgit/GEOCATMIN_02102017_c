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

        Dim sEsteX_E As String
        Dim sNorteY_E As String

        Dim iEsteX_E As Integer
        Dim iNorteY_E As Integer


        Dim iCodigoID As Integer
        Dim itipoex As Integer
        Dim isituexID As Integer
        Dim iestaexID As Integer
        Dim icodcarID As Integer
        Dim icartaID As Integer
        Dim ipezoncatID As Integer
        Dim itippubID As Integer
        Dim ideideID As Integer
        Dim inumpol As Integer
        Dim campo_E As Integer
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
        Try
            Dim cls_Oracle As New cls_Oracle
            Dim lodtbDatos_hecta As DataTable
            Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
            pSpatialReferenceEnv = New SpatialReferenceEnvironment
            Dim pStatusBar As ESRI.ArcGIS.esriSystem.IStatusBar
            Dim pProgbar As ESRI.ArcGIS.esriSystem.IStepProgressor
            Dim vZona As String = "18"
            Dim vCodigo, vCodigoSalvado As String
            Dim vEste, vNorte As Double
            Dim lodtbDatos As New DataTable
            Dim tipo_ac As String
            '    lodtbDatos = cls_Oracle.F_Obtiene_Datos_DM(p_Consulta)

            ' If v_sistema = "PSAD-56" Then
            '  lodtbDatos = cls_Oracle.F_Obtiene_Datos_DM(p_Consulta)
            If catastro_h = "1" Then
                lodtbDatos = cls_Oracle.F_SEL_LISTA_DM_HISTORICO(v_codigo, "", fecha_h & "1231", "2")
            ElseIf v_opcion_modulo = "OP_29" Then
                If v_sistema = "PSAD-56" Then
                    tipo_ac = "51"
                    lodtbDatos = cls_Oracle.FT_DATA_ACUMULACION(p_Consulta, tipo_ac)
                ElseIf v_sistema = "WGS-84" Then
                    tipo_ac = "1"
                    'lodtbDatos = cls_Oracle.F_Obtiene_Datos_DM_84_AC(p_Consulta)
                    lodtbDatos = cls_Oracle.FT_DATA_ACUMULACION(p_Consulta, tipo_ac)
                End If
            ElseIf v_opcion_modulo = "OP_30" Then
                If v_sistema = "PSAD-56" Then
                    tipo_ac = "41"
                    lodtbDatos = cls_Oracle.FT_DATA_ACUMULACION(p_Consulta, tipo_ac)
                ElseIf v_sistema = "WGS-84" Then
                    tipo_ac = "4"
                    'lodtbDatos = cls_Oracle.F_Obtiene_Datos_DM_84_AC(p_Consulta)
                    lodtbDatos = cls_Oracle.FT_DATA_ACUMULACION(p_Consulta, tipo_ac)
                End If
            Else
                lodtbDatos = cls_Oracle.F_Obtiene_Datos_DM_84(p_Consulta)    ' trae coordenadas del DM
            End If

            ' ElseIf v_sistema = "WGS-84" Then
            ' lodtbDatos = cls_Oracle.F_Obtiene_Datos_DM_84(p_Consulta)
            ' ElseIf v_sistema = "WGS84_1" Then
            ' lodtbDatos = cls_Oracle.F_Obtiene_Datos_DM_84_1(p_Consulta)
            '  End If

            'Dim strShapeFileName As String = "Catastro"
            Dim strShapeFileName As String = "Catastro_" & lo_Zona
            Dim pFCLass As IFeatureClass = Open_Feature_GDB(pApp, "", strShapeFileName)
            'cls_Catastro.Delete_Rows_FC_GDB("Catastro")
            cls_Catastro.Delete_Rows_FC_GDB("Catastro_" & lo_Zona)
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
                If lodtbDatos.Columns(f).ColumnName <> "CD_COREST" And lodtbDatos.Columns(f).ColumnName <> "CD_CORNOR" And lodtbDatos.Columns(f).ColumnName <> "CD_COREST_E" And lodtbDatos.Columns(f).ColumnName <> "CD_CORNOR_E" Then
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
            If v_equivalente = V_Sistemasele Then
                vEste = lodtbDatos.Rows(0).Item(DataXY.iEsteX) 'pRow.Value(DataXY.iEsteX)
                vNorte = lodtbDatos.Rows(0).Item(DataXY.iNorteY) ' pRow.Value(DataXY.iNorteY)
            Else
                vEste = lodtbDatos.Rows(0).Item(DataXY.iEsteX_E) 'pRow.Value(DataXY.iEsteX)
                vNorte = lodtbDatos.Rows(0).Item(DataXY.iNorteY_E) ' pRow.Value(DataXY.iNorteY)
            End If

            If v_opcion_modulo <> "OP_29" And v_opcion_modulo <> "OP_30" Then
                'If v_sistema = "PSAD-56" Then
                val_contador_pol = lodtbDatos.Rows(0).Item("CD_NUMPOL").ToString

                'val_contador_pol = lodtbDatos.Rows(0).Item("CD_CORNOR").ToString

                'Else
                '  val_contador_pol = lodtbDatos.Rows(0).Item("CD_NUMPOL").ToString
                ' End If
            End If

            Vertices(0) = New Point
            Vertices(0).PutCoords(vEste, vNorte)

            'Do While Not pRow Is Nothing
            ' Dim V1 As String

            For i As Integer = 0 To lodtbDatos.Rows.Count - 1
                pStatusBar.ShowProgressBar("Processing...", 0, lodtbDatos.Rows.Count, 1, True)
                If v_equivalente = V_Sistemasele Then
                    vEste = lodtbDatos.Rows(i).Item(DataXY.iEsteX)
                    vNorte = lodtbDatos.Rows(i).Item(DataXY.iNorteY) 'pRow.Value(DataXY.iNorteY) 'pRow.Value(pTable.Fields.FindField("NORTE"))

                Else
                    vEste = lodtbDatos.Rows(i).Item(DataXY.iEsteX_E)
                    vNorte = lodtbDatos.Rows(i).Item(DataXY.iNorteY_E) 'pRow.Value(DataXY.iNorteY) 'pRow.Value(pTable.Fields.FindField("NORTE"))
                End If

                vCodigo = lodtbDatos.Rows(i).Item(DataXY.iCodigoID) 'pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
                ''*******************

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

            'Calculando Area en base a las coordenadas redondeadas
            '--------------------------------------------------------
            '  Dim pArea As IArea

            poly = pFeatureDM.Shape
            ptcol = poly
            ReDim coordenada_DM(ptcol.PointCount)
            For j = 0 To ptcol.PointCount - 2
                pt = ptcol.Point(j)
                coordenada_DM(j).v = j + 1
                'coordenada_DM(j).x = Format(Math.Round(pt.X, 3), "###,###.00") ' pt.X
                'coordenada_DM(j).y = Format(Math.Round(pt.Y, 3), "###,###.00") 'pt.Y

                coordenada_DM(j).x = pt.X
                coordenada_DM(j).y = pt.Y

            Next j
            'Calcular del Area
            If val_contador_pol <> "" Then

                '    '--------------------------------------------------------
                If V_Sistemasele = "01" Then  '56


                    lodtbDatos_hecta = cls_Oracle.FT_obtiene_hectagis_dm(v_codigo, "DATA_GIS.GPO_CMI_CATASTRO_MINERO_" & lo_Zona)
                Else
                    lodtbDatos_hecta = cls_Oracle.FT_obtiene_hectagis_dm(v_codigo, "DATA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_" & lo_Zona)
                End If

                For i As Integer = 0 To lodtbDatos_hecta.Rows.Count - 1
                    pStatusBar.ShowProgressBar("Processing...", 0, lodtbDatos.Rows.Count, 1, True)
                    Area_utm = lodtbDatos_hecta.Rows(i).Item("HECTAGIS")
                    Area_utm = Format(Math.Round(Area_utm, 4), "###,###.0000")
                    'Area_utm = Format(Math.Round(Area_utm, 4), "###,###.0000")
                    '   MsgBox(Area_utm, MsgBoxStyle.Exclamation, "area")
                Next i

            Else
                coordenada_DM(j).x = coordenada_DM(0).x
                coordenada_DM(j).y = coordenada_DM(0).y
                Dim d0, d1, dr As Double
                d0 = 0 : d1 = 0 : dr = 0
                For h = 0 To j - 1
                    If coordenada_DM(h).x <> 0 Or coordenada_DM(h).y <> 0 Then
                        'd0 = d0 + coordenada_DM(h).x * coordenada_DM(h + 1).y
                        'd1 = d1 + coordenada_DM(h).y * coordenada_DM(h + 1).x

                        d0 = d0 + Math.Round(coordenada_DM(h).x, 2) * Math.Round(coordenada_DM(h + 1).y, 2)
                        d1 = d1 + Math.Round(coordenada_DM(h).y, 2) * Math.Round(coordenada_DM(h + 1).x, 2)

                    Else
                        Exit For
                    End If
                Next h
                dr = Math.Round(Math.Abs((d0 - d1) / 2) / 10000, 4)  'termino
                Area_utm = Format(Math.Round(dr, 4), "###,###.0000")
                '  MsgBox(Area_utm, MsgBoxStyle.Information, "")
            End If

                'comentado en esta version porque no es necesario proyectar sino en su zona debe estar
                'If lo_Zona <> vZona Then
                '    Select Case lo_Zona
                '        Case 18
                '            pFeatureDM.Shape.SpatialReference = Datum_PSAD_18
                '            Select Case vZona
                '                Case 17
                '                    pFeatureDM.Shape.Project(Datum_PSAD_17)
                '                Case 19
                '                    pFeatureDM.Shape.Project(Datum_PSAD_19)
                '            End Select
                '        Case 17
                '            pFeatureDM.Shape.SpatialReference = Datum_PSAD_17
                '            Select Case vZona
                '                Case 18
                '                    pFeatureDM.Shape.Project(Datum_PSAD_18)
                '                Case 19
                '                    pFeatureDM.Shape.Project(Datum_PSAD_19)
                '            End Select
                '        Case 19
                '            pFeatureDM.Shape.SpatialReference = Datum_PSAD_19
                '            Select Case vZona
                '                Case 17
                '                    pFeatureDM.Shape.pProject(Datum_PSAD_17)
                '                Case 18
                '                    pFeatureDM.Shape.Project(Datum_PSAD_18)
                '            End Select
                '    End Select
                'End If

                pFeatureDM.Store()
                pEditor.StopOperation("Add Polygon")
                pEditor.StopEditing(True)
                ' poly = pFeatureDM.Shape
                pStatusBar.HideProgressBar()
                pStatusBar.Message(0) = "Operación Completado"

                ''Calculando Area en base a las coordenadas redondeadas
                ''--------------------------------------------------------
                'ptcol = poly
                'ReDim coordenada_DM(ptcol.PointCount)
                'For j = 0 To ptcol.PointCount - 2
                '    pt = ptcol.Point(j)
                '    coordenada_DM(j).v = j + 1
                '    coordenada_DM(j).x = Format(Math.Round(pt.X, 3), "###,###.00") ' pt.X
                '    coordenada_DM(j).y = Format(Math.Round(pt.Y, 3), "###,###.00") 'pt.Y

                'Next j
                ''Calcular del Area

                'coordenada_DM(j).x = coordenada_DM(0).x
                'coordenada_DM(j).y = coordenada_DM(0).y
                'Dim d0, d1, dr As Double
                'd0 = 0 : d1 = 0 : dr = 0
                'For h = 0 To j - 1
                '    If coordenada_DM(h).x <> 0 Or coordenada_DM(h).y <> 0 Then
                '        d0 = d0 + coordenada_DM(h).x * coordenada_DM(h + 1).y
                '        d1 = d1 + coordenada_DM(h).y * coordenada_DM(h + 1).x
                '    Else
                '        Exit For
                '    End If
                'Next h
                'dr = Math.Round(Math.Abs((d0 - d1) / 2) / 10000, 4)  'termino
            'Area_utm = Format(Math.Round(dr, 4), "###,###.0000")


            ' *************se agrega para probar  ***********************

        Catch ex As Exception
            MsgBox(".::Error - Al generar poligono::.", vbInformation, "Aviso")
        End Try
    End Sub
    Public Sub Genera_Catastro_DM_simu(ByVal lodtbDatos As DataTable, ByVal lo_Zona As String, ByVal pApp As IApplication)
        Try
            Dim cls_Oracle As New cls_Oracle
            Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
            pSpatialReferenceEnv = New SpatialReferenceEnvironment
            Dim pStatusBar As ESRI.ArcGIS.esriSystem.IStatusBar
            Dim pProgbar As ESRI.ArcGIS.esriSystem.IStepProgressor
            '  Dim vZona As String = "18"
            Dim vCodigo, vCodigoSalvado As String
            Dim vEste, vNorte As Double


            'Dim strShapeFileName As String = "Catastro"
            Dim strShapeFileName As String = "Catastro_" & lo_Zona
            Dim pFCLass As IFeatureClass = Open_Feature_GDB(pApp, "", strShapeFileName)
            'cls_Catastro.Delete_Rows_FC_GDB("Catastro")
            cls_Catastro.Delete_Rows_FC_GDB("Catastro_" & lo_Zona)
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

                    'If lo_Zona <> vZona Then
                    '    Select Case vZona
                    '        Case 18
                    '            pFeatureDM.Shape.SpatialReference = Datum_PSAD_18
                    '            Select Case lo_Zona
                    '                Case 17
                    '                    pFeatureDM.Shape.Project(Datum_PSAD_17)
                    '                Case 19
                    '                    pFeatureDM.Shape.Project(Datum_PSAD_19)
                    '            End Select
                    '        Case 17
                    '            pFeatureDM.Shape.SpatialReference = Datum_PSAD_17
                    '            Select Case lo_Zona
                    '                Case 18
                    '                    pFeatureDM.Shape.Project(Datum_PSAD_18)
                    '                Case 19
                    '                    pFeatureDM.Shape.Project(Datum_PSAD_19)
                    '            End Select
                    '        Case 19
                    '            pFeatureDM.Shape.SpatialReference = Datum_PSAD_19
                    '            Select Case lo_Zona
                    '                Case 17
                    '                    pFeatureDM.Shape.pProject(Datum_PSAD_17)
                    '                Case 18
                    '                    pFeatureDM.Shape.Project(Datum_PSAD_18)
                    '            End Select
                    '    End Select
                    'End If
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

            'If p_Consulta <> "" Then
            '    'Calculando Area en base a las coordenadas redondeadas
            '    '--------------------------------------------------------
            '    poly = pFeatureDM.Shape
            '    ptcol = poly
            '    ReDim coordenada_DM(ptcol.PointCount)
            '    For j = 0 To ptcol.PointCount - 2
            '        pt = ptcol.Point(j)
            '        coordenada_DM(j).v = j + 1
            '        'coordenada_DM(j).x = Format(Math.Round(pt.X, 3), "###,###.00") ' pt.X
            '        'coordenada_DM(j).y = Format(Math.Round(pt.Y, 3), "###,###.00") 'pt.Y

            '        coordenada_DM(j).x = pt.X
            '        coordenada_DM(j).y = pt.Y

            '    Next j
            '    'Calcular del Area

            '    coordenada_DM(j).x = coordenada_DM(0).x
            '    coordenada_DM(j).y = coordenada_DM(0).y
            '    Dim d0, d1, dr As Double
            '    d0 = 0 : d1 = 0 : dr = 0
            '    For h = 0 To j - 1
            '        If coordenada_DM(h).x <> 0 Or coordenada_DM(h).y <> 0 Then
            '            'd0 = d0 + coordenada_DM(h).x * coordenada_DM(h + 1).y
            '            'd1 = d1 + coordenada_DM(h).y * coordenada_DM(h + 1).x

            '            d0 = d0 + Math.Round(coordenada_DM(h).x, 2) * Math.Round(coordenada_DM(h + 1).y, 2)
            '            d1 = d1 + Math.Round(coordenada_DM(h).y, 2) * Math.Round(coordenada_DM(h + 1).x, 2)

            '        Else
            '            Exit For
            '        End If
            '    Next h
            '    dr = Math.Round(Math.Abs((d0 - d1) / 2) / 10000, 4)  'termino
            '    Area_utm = Format(Math.Round(dr, 4), "###,###.0000")




            'comentado en esta version porque no es necesario proyectar sino en su zona debe estar
            'If lo_Zona <> vZona Then
            '    Select Case lo_Zona
            '        Case 18
            '            pFeatureDM.Shape.SpatialReference = Datum_PSAD_18
            '            Select Case vZona
            '                Case 17
            '                    pFeatureDM.Shape.Project(Datum_PSAD_17)
            '                Case 19
            '                    pFeatureDM.Shape.Project(Datum_PSAD_19)
            '            End Select
            '        Case 17
            '            pFeatureDM.Shape.SpatialReference = Datum_PSAD_17
            '            Select Case vZona
            '                Case 18
            '                    pFeatureDM.Shape.Project(Datum_PSAD_18)
            '                Case 19
            '                    pFeatureDM.Shape.Project(Datum_PSAD_19)
            '            End Select
            '        Case 19
            '            pFeatureDM.Shape.SpatialReference = Datum_PSAD_19
            '            Select Case vZona
            '                Case 17
            '                    pFeatureDM.Shape.pProject(Datum_PSAD_17)
            '                Case 18
            '                    pFeatureDM.Shape.Project(Datum_PSAD_18)
            '            End Select
            '    End Select
            'End If
            ' End If
            pFeatureDM.Store()
            pEditor.StopOperation("Add Polygon")
            pEditor.StopEditing(True)
            ' poly = pFeatureDM.Shape
            pStatusBar.HideProgressBar()
            pStatusBar.Message(0) = "Operación Completado"

            ''Calculando Area en base a las coordenadas redondeadas
            ''--------------------------------------------------------
            'ptcol = poly
            'ReDim coordenada_DM(ptcol.PointCount)
            'For j = 0 To ptcol.PointCount - 2
            '    pt = ptcol.Point(j)
            '    coordenada_DM(j).v = j + 1
            '    coordenada_DM(j).x = Format(Math.Round(pt.X, 3), "###,###.00") ' pt.X
            '    coordenada_DM(j).y = Format(Math.Round(pt.Y, 3), "###,###.00") 'pt.Y

            'Next j
            ''Calcular del Area

            'coordenada_DM(j).x = coordenada_DM(0).x
            'coordenada_DM(j).y = coordenada_DM(0).y
            'Dim d0, d1, dr As Double
            'd0 = 0 : d1 = 0 : dr = 0
            'For h = 0 To j - 1
            '    If coordenada_DM(h).x <> 0 Or coordenada_DM(h).y <> 0 Then
            '        d0 = d0 + coordenada_DM(h).x * coordenada_DM(h + 1).y
            '        d1 = d1 + coordenada_DM(h).y * coordenada_DM(h + 1).x
            '    Else
            '        Exit For
            '    End If
            'Next h
            'dr = Math.Round(Math.Abs((d0 - d1) / 2) / 10000, 4)  'termino
            'Area_utm = Format(Math.Round(dr, 4), "###,###.0000")
        Catch ex As Exception
            MsgBox(".::Error - Al generar poligono::.", vbInformation, "Aviso")
        End Try
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
        'hay que ver en simulado
        ' If pFeatureLayer.Name = "Catastro_" & V_zona_simu Then ' 
        'pFeatureLayer.Name = "DM_Simulado"
        'End If

        pFeatureLayer.Visible = True
        pMap.AddLayer(pFeatureLayer)
        pMxDoc.ActiveView.Refresh()
        Open_Feature_GDB = pFeatureClass
    End Function
    Public Function Open_Feature_SHP(ByRef m_application As IApplication, ByRef PathDestino As String, ByVal mFeatureClass As String) As IFeatureClass

        'Try
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
        'Catch ex As Exception
        'MsgBox(ex.Message)
        'End Try


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
        pEditor.StartOperation()  'chequear edicion
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
        'Almacena los campos al datagrid
        Dim f As Integer
        For f = 0 To lodtbAsigna.Columns.Count - 1 '(f).ColumnName - 1 '.Rows.Count - 1 '.Fields.FieldCount - 1
            If lodtbAsigna.Columns(f).ColumnName = "CD_COREST" Then '.Fields.Field(f).Name = "ESTE" Then 'DataXY.sEsteX Then
                DataXY.iEsteX = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "CD_CORNOR" Then 'DataXY.sNorteY Then
                DataXY.iNorteY = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "CD_COREST_E" Then 'DataXY.sNorteY Then
                DataXY.iEsteX_E = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "CD_CORNOR_E" Then 'DataXY.sNorteY Then
                DataXY.iNorteY_E = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "CG_CODIGO" Then 'DataXY.sCodigoID Then
                DataXY.iCodigoID = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "TE_TIPOEX" Then 'DataXY.sCodigoID Then
                DataXY.itipoex = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "SE_SITUEX" Then 'DataXY.sCodigoID Then
                DataXY.isituexID = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "EE_ESTAEX" Then 'DataXY.sCodigoID Then
                DataXY.iestaexID = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "CE_CODCAR" Then 'DataXY.sCodigoID Then
                DataXY.icodcarID = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "CARTA" Then 'DataXY.sCodigoID Then
                DataXY.icartaID = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "PE_ZONCAT" Then 'DataXY.sCodigoID Then
                DataXY.ipezoncatID = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "LD_TIPPUB" Then 'DataXY.sCodigoID Then
                DataXY.itippubID = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "DE_IDE" Then 'DataXY.sCodigoID Then
                DataXY.ideideID = f
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

    Private Function NuevoSubPolygono_DH(ByRef ChekearVertice() As Point, ByRef punto As Point, ByVal actual As Short) As Short
        Dim j As Short, Resultado As Short
        Resultado = -1
        For j = 0 To actual
            If punto.X = ChekearVertice(j).X And punto.Y = ChekearVertice(j).Y Then
                Resultado = actual + 2
                Exit For
            End If
        Next
        NuevoSubPolygono_DH = Resultado
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
        For j = 0 To actual     'ojo: se comenta el If, para evitar que se trunque cuando hay un vertice repetido (caso de los DM = F)
            If val_contador_pol <> "" Then  'solo entrara para dm caso con hueco. modificado 11/06/15

                If punto.X = ChekearVertice(j).X And punto.Y = ChekearVertice(j).Y Then
                    Resultado = actual + 2
                    Exit For
                End If
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
        '  Exit Sub
        ' arch_cata = "Cat_exporta"
        ' cls_Catastro.Exportando_Temas("Cat_exporta", p_ShapeFile, pApp)
        ' ' cls_Catastro.Quitar_Layer(loStrShapefile, pApp)
        ' cls_Catastro.Add_ShapeFile(loStrShapefile_cat, pApp)

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
        'Dim vZona As String = "18"
        Dim vCodigo, vCodigoSalvado As String
        Dim vEste, vNorte As Double
        Dim strShapeFileName As String = ""
        If p_Flag = "1" Then
            strShapeFileName = "gpo_geo_aster"
        ElseIf p_Flag = "2" Then
            strShapeFileName = "ZonaUrbana_10Ha"
        ElseIf p_Flag = "3" Then
            strShapeFileName = "AreaReserva_100Ha_" & p_Zona
        ElseIf p_Flag = "4" Then
            strShapeFileName = "Recta_" & p_Zona
        End If
        Dim pFCLass As IFeatureClass = Open_Feature_GDB(pApp, "", strShapeFileName)
        ' cls_Catastro.Delete_Rows_FC_GDB(strShapeFileName & p_Zona)

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
                            'p pFeatureDM.Shape.Project(Datum_PSAD_18)
                        Case "19"
                            pFeatureDM.Shape.SpatialReference = Datum_PSAD_19
                            ' pFeatureDM.Shape.Project(Datum_PSAD_18)
                    End Select
                    If p_Flag <> "1" Then
                        Poligono_Ok = Verifica_Existencia_XY("Catastro_1", pArea.Centroid.X, pArea.Centroid.Y, gloZona, pApp)
                        'Poligono_Ok = Verifica_Existencia_XY(strShapeFileName, pArea.Centroid.X, pArea.Centroid.Y, gloZona, pApp)
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
                    ' pFeatureDM.Shape.Project(Datum_PSAD_18)
                Case "19"
                    pFeatureDM.Shape.SpatialReference = Datum_PSAD_19
                    ' pFeatureDM.Shape.Project(Datum_PSAD_18)
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
                    '  pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGO")) = v_codigo
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
            ' MsgBox("No creo el Poligono de Frontera", MsgBoxStyle.Information, "[BDGEOCATMIN]")
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

    Public Sub Genera_Polygono_DH(ByVal p_ShapeFile As String, ByVal lodtbDatos As DataTable, _
                               ByVal p_Zona As String, ByVal pApp As IApplication, _
                               ByVal p_Existe As Object)
        Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
        Dim cls_DM_1 As New cls_DM_1
        pSpatialReferenceEnv = New SpatialReferenceEnvironment
        Dim pStatusBar As ESRI.ArcGIS.esriSystem.IStatusBar
        Dim pProgbar As ESRI.ArcGIS.esriSystem.IStepProgressor
        Dim vCodigo, vCodigoSalvado As String
        Dim vEste, vNorte As Double
        Dim pFCLass As IFeatureClass = Open_Feature_SHP(pApp, "", p_ShapeFile)
        Delete_Rows_FC_SHP(p_ShapeFile)
        Dim pEditor As IEditor = ActivarEditor(pApp, pFCLass)
        Dim Conta As Integer = 0
        Dim Vertices(500) As Point
        Dim L As Integer
        Dim vEstado As String = ""
        Dim pFeatureDM As IFeature
        pFeatureDM = pFCLass.CreateFeature
        Dim contador As Integer = 1
        SetParam_DH(lodtbDatos)
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
        '****************************************
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
            If lodtbDatos.Columns(f).ColumnName <> "CD_COREST" And _
                lodtbDatos.Columns(f).ColumnName <> "CD_CORNOR" _
                Then
                If lodtbDatos.Columns(f).ColumnName = "CG_CODIGO" Then
                    DataInfo(n).Index = f 'almaceno los campos 
                    DataInfo(n).Campo = lodtbDatos.Columns(f).ColumnName
                    n = n + 1
                End If
            End If
        Next f
        For L = 0 To FieldDataCount - 1
            pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
            pFeatureDM.Value(L + 3) = Mid(lodtbDatos.Rows(L).Item(DataInfo(L).Index), 1, InStr(lodtbDatos.Rows(L).Item(DataInfo(L).Index), "_") - 1)
            pFeatureDM.Value(L + 6) = lodtbDatos.Rows(L).Item("ANNO") 'pRow.Value(DataInfo(L + 1).Index)
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

        Try
            For i As Integer = 0 To lodtbDatos.Rows.Count - 1
                pStatusBar.ShowProgressBar("Processing...", 0, lodtbDatos.Rows.Count, 1, True)
                vEste = lodtbDatos.Rows(i).Item(DataXY.iEsteX) 'pRow.Value(DataXY.iEsteX) 'pRow.Value(pTable.Fields.FindField("ESTE"))
                vNorte = lodtbDatos.Rows(i).Item(DataXY.iNorteY) 'pRow.Value(DataXY.iNorteY) 'pRow.Value(pTable.Fields.FindField("NORTE"))
                vCodigo = lodtbDatos.Rows(i).Item(DataXY.iCodigoID) 'pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
                ''********************
                If vCodigo <> vCodigoSalvado Then 'si es diferente Grabar los datos
                    pFeatureDM.Shape = AdicionarConcesion_DH(pFCLass, Vertices, Conta) 'pPolygon
                    pFeatureDM.Store()
                    vCodigoSalvado = vCodigo
                    Conta = 0
                    ReDim Vertices(500) 'inicializar variables
                    Vertices(Conta) = New Point
                    Vertices(Conta).PutCoords(vEste, vNorte)
                    vCodigoSalvado = lodtbDatos.Rows(i).Item(DataXY.iCodigoID) 'pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
                    pFeatureDM = pFCLass.CreateFeature
                    For L = 0 To FieldDataCount - 1 ' cargo detalle
                        Select Case lodtbDatos.Columns(L).ColumnName
                            Case "CG_CODIGO" ', "CD_COREST", "CD_CORNOR"
                                pFeatureDM.Value(L + 2) = contador 'lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
                                pFeatureDM.Value(L + 3) = Mid((lodtbDatos.Rows(i).Item(DataInfo(L).Index)).ToString, 1, InStr((lodtbDatos.Rows(L).Item(DataInfo(L).Index)).ToString, "_") - 1)
                                pFeatureDM.Value(L + 6) = (lodtbDatos.Rows(i).Item("ANNO")).ToString 'pRow.Value(DataInfo(L + 1).Index)
                        End Select
                        'If lodtbDatos.Columns(L).ColumnName = "CG_CODIGO" Then
                        '    pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
                        '    pFeatureDM.Value(L + 3) = (lodtbDatos.Rows(L).Item(DataInfo(L).Index)).ToString 'pRow.Value(DataInfo(L + 1).Index)
                        'End If
                    Next
                    contador = contador + 1
                    pFeatureDM.Value(19) = contador ' Campo Contador
                Else
                    Vertices(Conta) = New Point
                    Vertices(Conta).PutCoords(vEste, vNorte)
                End If
                '*****************************************************
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
                '*********************************************************************
                Conta = Conta + 1
                pStatusBar.StepProgressBar()
            Next (i) 'Loop
            pFeatureDM.Shape = AdicionarConcesion_DH(pFCLass, Vertices, Conta) 'pPolygon
            Dim pArea As IArea
            pArea = pFeatureDM.Shape
            If pArea.Area <> 0 Then
                p_Existe = True
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
            cls_DM_1.Zoom_to_Layer_DH("Poligono", pApp)
            pFeatureDM = Nothing
            pFeatureDMP = Nothing
            pFCLass = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Function AdicionarConcesion_DH(ByRef pFCLass As IFeatureClass, ByRef Vertices() As Point, ByVal Conta As Short) As IPolygon
        Dim i As Integer
        'Dim pRings(40) As IRing  'Maximo 40 anillos
        Dim pPolygon As IPolygon = New ESRI.ArcGIS.Geometry.PolygonClass
        Dim P As Integer, Parts As Integer = 0
        SubPolygono = 0
        Dim Puntos As Point
        Dim pPointCollection As IPointCollection = New RingClass()
        Puntos = New Point
        Puntos.PutCoords(Vertices(0).X, Vertices(0).Y)
        pPointCollection.AddPoint(Puntos)
        For i = 1 To Conta - 1
            SubPolygono = NuevoSubPolygono_DH(Vertices, Vertices(i), i - 1)
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
        AdicionarConcesion_DH = pPolygon
    End Function

    Public Sub SetParam_DH(ByVal lodtbAsigna As DataTable)
        'Almacena los campos al datagrid
        Dim f As Integer
        For f = 0 To lodtbAsigna.Columns.Count - 1 '(f).ColumnName - 1 '.Rows.Count - 1 '.Fields.FieldCount - 1
            If lodtbAsigna.Columns(f).ColumnName = "CD_COREST" Then '.Fields.Field(f).Name = "ESTE" Then 'DataXY.sEsteX Then
                DataXY.iEsteX = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "CD_CORNOR" Then 'DataXY.sNorteY Then
                DataXY.iNorteY = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "CG_CODIGO" Then 'DataXY.sCodigoID Then
                DataXY.iCodigoID = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "TE_TIPOEX" Then 'DataXY.sCodigoID Then
                DataXY.itipoex = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "SE_SITUEX" Then 'DataXY.sCodigoID Then
                DataXY.isituexID = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "EE_ESTAEX" Then 'DataXY.sCodigoID Then
                DataXY.iestaexID = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "CE_CODCAR" Then 'DataXY.sCodigoID Then
                DataXY.icodcarID = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "CARTA" Then 'DataXY.sCodigoID Then
                DataXY.icartaID = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "PE_ZONCAT" Then 'DataXY.sCodigoID Then
                DataXY.ipezoncatID = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "LD_TIPPUB" Then 'DataXY.sCodigoID Then
                DataXY.itippubID = f
            ElseIf lodtbAsigna.Columns(f).ColumnName = "DE_IDE" Then 'DataXY.sCodigoID Then
                DataXY.ideideID = f
            End If
        Next f
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
        'Programa que genera poligonos

        Dim cls_Oracle As New cls_Oracle
        Dim pSpatialReferenceEnv As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
        pSpatialReferenceEnv = New SpatialReferenceEnvironment
        Dim pStatusBar As ESRI.ArcGIS.esriSystem.IStatusBar
        Dim pProgbar As ESRI.ArcGIS.esriSystem.IStepProgressor
        Dim vCodigo, vCodigoSalvado As String
        Dim vtipoex, vsituex, vestaex, vcodcar, vcarta, vzoncat, vtippub, vide As String

        Dim vEste, vNorte As Double

        Dim pFCLass As IFeatureClass = Open_Feature_SHP(pApp, "", p_ShapeFile)

        ' cls_Catastro.Delete_Rows_FC_SHP(p_ShapeFile)

        Dim pEditor As IEditor = ActivarEditor(pApp, pFCLass)
        Dim Conta As Integer = 0
        ' Dim Vertices(500) As Point
        Dim Vertices(100000) As Point
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
            MsgBox("NO se encontro la capa..")
            Exit Sub
        End If
        pFLayerDMP = pMxDoc.FocusMap.Layer(lo_pFLayerDMP)
        pFCLass_Point = pFLayerDMP.FeatureClass
        pDataset = pFCLass_Point
        '**********************
        For f As Integer = 0 To FieldDataCount - 1

            'igualar N  por f cuando no existe campo
            ' If lodtbDatos.Columns(f).ColumnName <> "CD_COREST" And lodtbDatos.Columns(f).ColumnName <> "CD_CORNOR" Then
            If lodtbDatos.Columns(f).ColumnName <> "CD_COREST" And lodtbDatos.Columns(f).ColumnName <> "CD_CORNOR" And lodtbDatos.Columns(f).ColumnName <> "CD_NUMVER" Then
                DataInfo(n).Index = f 'almaceno los campos 
                DataInfo(n).Campo = lodtbDatos.Columns(f).ColumnName
                'DataInfo(f).Campo = lodtbDatos.Columns(f).ColumnName
                'lodtbDatos.Rows(i).Item(DataXY.iCodigoID)
                n = n + 1
            End If
        Next f
        For L = 0 To FieldDataCount - 1

            'pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
            'pFeatureDM.Value(L + 3) = lodtbDatos.Rows(0).Item(DataXY.iCodigoID)
            'pFeatureDM.Value(L + 4) = lodtbDatos.Rows(0).Item(DataXY.itipoex)
            'pFeatureDM.Value(L + 5) = lodtbDatos.Rows(0).Item(DataXY.isituexID)
            'pFeatureDM.Value(L + 6) = lodtbDatos.Rows(0).Item(DataXY.iestaexID)
            'pFeatureDM.Value(L + 7) = lodtbDatos.Rows(0).Item(DataXY.icodcarID)
            'pFeatureDM.Value(L + 11) = lodtbDatos.Rows(0).Item(DataXY.icartaID)
            'pFeatureDM.Value(L + 12) = lodtbDatos.Rows(0).Item(DataXY.ipezoncatID)
            'pFeatureDM.Value(L + 13) = lodtbDatos.Rows(0).Item(DataXY.itippubID)
            'pFeatureDM.Value(L + 14) = lodtbDatos.Rows(0).Item(DataXY.ideideID)

            pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
            pFeatureDM.Value(L + 3) = lodtbDatos.Rows(0).Item(DataXY.iCodigoID)
            pFeatureDM.Value(L + 4) = lodtbDatos.Rows(0).Item(DataXY.itipoex)
            pFeatureDM.Value(L + 5) = lodtbDatos.Rows(0).Item(DataXY.isituexID)
            pFeatureDM.Value(L + 6) = lodtbDatos.Rows(0).Item(DataXY.iestaexID)
            pFeatureDM.Value(L + 7) = lodtbDatos.Rows(0).Item(DataXY.icodcarID)
            pFeatureDM.Value(L + 8) = lodtbDatos.Rows(0).Item(DataXY.icartaID)
            pFeatureDM.Value(L + 9) = lodtbDatos.Rows(0).Item(DataXY.ipezoncatID)
            pFeatureDM.Value(L + 10) = lodtbDatos.Rows(0).Item(DataXY.itippubID)
            pFeatureDM.Value(L + 11) = lodtbDatos.Rows(0).Item(DataXY.ideideID)
            Exit For
        Next
        contador = contador + 1
        pStatusBar = pApp.StatusBar
        pProgbar = pStatusBar.ProgressBar
        pProgbar.Position = 0
        vCodigoSalvado = lodtbDatos.Rows(0).Item(DataXY.iCodigoID) ' pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))
        vEste = lodtbDatos.Rows(0).Item(DataXY.iEsteX) 'pRow.Value(DataXY.iEsteX)
        vNorte = lodtbDatos.Rows(0).Item(DataXY.iNorteY) ' pRow.Value(DataXY.iNorteY)


        'pFeatureDMP = pFCLass_Point.CreateFeature
        'pPoint_Geo = New ESRI.ArcGIS.Geometry.Point
        'pPoint_Geo.X = CType(vEste, Double) ' pPoint(Conta).X
        'pPoint_Geo.Y = CType(vNorte, Double) 'pPoint(Conta).Y


        ''Select Case p_Zona
        ''   Case 17
        'pPoint_Geo.SpatialReference = Datum_PSAD_18
        'pPoint_Geo.Project(Datum_PSAD_18)
        ''    Case 19
        '' pPoint_Geo.SpatialReference = Datum_PSAD_18
        '' pPoint_Geo.Project(Datum_PSAD_18)
        '' End Select

        'pFeatureDMP.Shape = pPoint_Geo
        'vEste = pPoint_Geo.X
        'vNorte = pPoint_Geo.Y




        Vertices(0) = New Point
        Vertices(0).PutCoords(vEste, vNorte)

        For i As Integer = 0 To lodtbDatos.Rows.Count - 1
            pStatusBar.ShowProgressBar("Processing...", 0, lodtbDatos.Rows.Count, 1, True)


            vEste = lodtbDatos.Rows(i).Item(DataXY.iEsteX) 'pRow.Value(DataXY.iEsteX) 'pRow.Value(pTable.Fields.FindField("ESTE"))
            vNorte = lodtbDatos.Rows(i).Item(DataXY.iNorteY) 'pRow.Value(DataXY.iNorteY) 'pRow.Value(pTable.Fields.FindField("NORTE"))
            vCodigo = lodtbDatos.Rows(i).Item(DataXY.iCodigoID) 'pRow.Value(DataXY.iCodigoID) 'pRow.Value(pTable.Fields.FindField("CODIGOU"))

            'Cuando es para Padron Minero

            vtipoex = lodtbDatos.Rows(i).Item(DataXY.itipoex)
            vsituex = lodtbDatos.Rows(i).Item(DataXY.isituexID)
            vestaex = lodtbDatos.Rows(i).Item(DataXY.iestaexID)
            vcodcar = lodtbDatos.Rows(i).Item(DataXY.icodcarID)
            vcarta = lodtbDatos.Rows(i).Item(DataXY.icartaID)
            vzoncat = lodtbDatos.Rows(i).Item(DataXY.ipezoncatID)
            vtippub = lodtbDatos.Rows(i).Item(DataXY.itippubID)
            vide = lodtbDatos.Rows(i).Item(DataXY.ideideID)
            pApp.Caption = "GRAFICANDO : " & vCodigo & " Zona : " & vzoncat & "... " & i & " de... " & lodtbDatos.Rows.Count

            pFeatureDMP = pFCLass_Point.CreateFeature
            pPoint_Geo = New ESRI.ArcGIS.Geometry.Point
            pPoint_Geo.X = CType(vEste, Double) ' pPoint(Conta).X
            pPoint_Geo.Y = CType(vNorte, Double) 'pPoint(Conta).Y


            ' Select Case vzoncat
            Select Case p_Zona
                Case 17
                    pPoint_Geo.SpatialReference = Datum_PSAD_18
                    pPoint_Geo.Project(Datum_PSAD_17)
                Case 19
                    pPoint_Geo.SpatialReference = Datum_PSAD_18
                    pPoint_Geo.Project(Datum_PSAD_19)
            End Select

            pFeatureDMP.Shape = pPoint_Geo
            vEste = pPoint_Geo.X
            vNorte = pPoint_Geo.Y

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

                    'pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
                    'pFeatureDM.Value(L + 3) = vCodigoSalvado
                    'pFeatureDM.Value(L + 4) = vtipoex
                    'pFeatureDM.Value(L + 5) = vsituex
                    'pFeatureDM.Value(L + 6) = vestaex
                    'pFeatureDM.Value(L + 7) = vcodcar
                    'pFeatureDM.Value(L + 11) = vcarta
                    'pFeatureDM.Value(L + 12) = vzoncat
                    'pFeatureDM.Value(L + 13) = vtippub
                    'pFeatureDM.Value(L + 14) = vide



                    pFeatureDM.Value(L + 2) = contador ' lodtbDatos.Rows(L).Item(DataInfo(L).Index) 'pRow.Value(DataInfo(L + 1).Index)
                    pFeatureDM.Value(L + 3) = vCodigoSalvado
                    pFeatureDM.Value(L + 4) = vtipoex
                    pFeatureDM.Value(L + 5) = vsituex
                    pFeatureDM.Value(L + 6) = vestaex
                    pFeatureDM.Value(L + 7) = vcodcar
                    pFeatureDM.Value(L + 8) = vcarta
                    pFeatureDM.Value(L + 9) = vzoncat
                    pFeatureDM.Value(L + 10) = vtippub
                    pFeatureDM.Value(L + 11) = vide

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

            'Select Case p_Zona
            'Select Case vzoncat
            '    Case 17
            '        pPoint_Geo.SpatialReference = Datum_PSAD_18
            '        pPoint_Geo.Project(Datum_PSAD_17)
            '    Case 19
            '        pPoint_Geo.SpatialReference = Datum_PSAD_18
            '        pPoint_Geo.Project(Datum_PSAD_19)
            'End Select

            'Select Case vzoncat


            Select Case p_Zona
                Case 17
                    'pPoint_Geo.SpatialReference = Datum_PSAD_18
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


        'pPoint = New ESRI.ArcGIS.Geometry.Point
        'pPoint.X = glo_xMin : pPoint.Y = glo_yMin
        'pPoint.SpatialReference = Datum_PSAD_17 : pPoint.Project(Datum_PSAD_18)
        '' xMin.Text = pPoint.X : yMin.Text = pPoint.Y
        'pPoint = New ESRI.ArcGIS.Geometry.Point
        'pPoint.X = glo_xMax : pPoint.Y = glo_yMax
        'pPoint.SpatialReference = Datum_PSAD_17 : pPoint.Project(Datum_PSAD_18)
        ''xMax.Text = pPoint.X : yMax.Text = pPoint.Y
        ''glo_xMin = Int(xMin.Text / 1000) * 1000 : glo_xMax = Int(xMax.Text / 1000) * 1000
        'glo_yMin = Int(yMin.Text / 1000) * 1000 : glo_yMax = Int(yMax.Text / 1000) * 1000

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
