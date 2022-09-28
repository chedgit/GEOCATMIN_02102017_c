Imports ESRI.ArcGIS.GISClient
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Display
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Output
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.DataSourcesOleDB
Imports ESRI.ArcGIS.Carto
Imports stdole
Imports PORTAL_Clases
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.DataSourcesRaster
Imports System.IO


Public Class cls_Usuario
    Public pApp As IApplication
    Private pPolygon As IGeometryCollection
    Private dr As Double
    Private x_inicio As Double
    Private y_inicio As Double
    Private m() As Double
    Private pFeatureVolcan As IFeature
    Private pFCLass As IFeatureClass
    Private pFLayerVolcan As IFeatureLayer
    Private pRing() As IRing
    Private pSegColl() As ISegmentCollection
    Private pEditor As IEditor
    Private pID As New UID
    Private pPoint_1 As IPoint
    Private pPoint_2 As IPoint
    Private Codigo_1 As String
    Private Codigo_2 As String
    Private cursor_filas As ICursor
    Private lointNumeroPoligono As Integer
    Private w As Integer

    Private ee_estaex_1 As String
    Private se_situex_1 As String
    Private te_tipoex_1 As String
    Private ca_codcar_1 As String
    Private estado_1 As String
    Private pe_vigcat_1 As String
    Private ee_estaex_2 As String
    Private se_situex_2 As String
    Private te_tipoex_2 As String
    Private ca_codcar_2 As String
    Private estado_2 As String
    Private titular_1 As String
    Private titular_2 As String
    Private pe_vigcat_2 As String
    Private num_pol_1 As String
    Private num_pol_2 As String
    Private poligono As Long
    Private nislas As Long
    Public pWorkspace As IWorkspace
    Public pFeatWorkspace As IFeatureWorkspace
    Public pTable As ITable
    Public pDataset As IDatasetName
    Public pFlayer As IFeatureLayer
    Private boo_Lista() As Boolean

    Public Sub genera_DM_INGEMMET(ByVal pApp As IApplication)
        Dim codigo As String = "010000105"
        Dim i, j, r, p, paso, w, lointNumeroPoligono, Nro_Registro, m_Codigo_NumReg As Integer
        Dim m(0 To 500, 0 To 1)
        Dim xNum As Long
        Dim pmap As IMap
        Dim pDataset As IDataset
        xNum = 100000
        ReDim pRing(0 To xNum)
        ReDim pSegColl(0 To xNum)
        Dim pWorkspaceFact As IWorkspaceFactory

        pMxDoc = pApp.Document
        pmap = pMxDoc.FocusMap
        Dim A As Integer
        Dim aFound As Boolean
        aFound = False
        For A = 0 To pmap.LayerCount - 1
            If pmap.Layer(A).Name = "Catastro" Then
                pFLayerVolcan = pMxDoc.FocusMap.Layer(A)
                aFound = True
                Exit For
            End If
        Next A

        If Not aFound Then
            MsgBox("Could not find feature layer.")
            Exit Sub
        End If
        'Starts the Edit Session
        pFCLass = pFLayerVolcan.FeatureClass
        pID.Value = "esriCore.Editor"
        pEditor = pApp.FindExtensionByCLSID(pID)
        pDataset = pFCLass
        pWorkspace = pDataset.Workspace
        pEditor.StartEditing(pWorkspace)
        pEditor.StartOperation()
        Dim pPropset As IPropertySet
        pPropset = New PropertySet
        'pPropset.SetProperty "CONNECTSTRING", "Provider=MSDAORA.1;Data source=ORACLE;User ID=ADSISGEM;Password=ACASTA"
        pPropset.SetProperty("CONNECTSTRING", "Provider=MSDAORA.1;Data source=DESA_DM;User ID=SISGEM;Password=SISGEM")
        'Creando sentencias del OLEDB
        pWorkspaceFact = New OLEDBWorkspaceFactory
        '   Dim pWorkspace As IWorkspace
        pWorkspace = pWorkspaceFact.Open(pPropset, 0)
        Dim pFeatureWorkspace As IFeatureWorkspace
        pFeatureWorkspace = pWorkspace

        Dim pQueryDef As IQueryDef
        pQueryDef = pFeatureWorkspace.CreateQueryDef

        'pQueryDef.Tables = " SG_D_PETITORIOS A, SC_V_COORDENADADET B "
        'pQueryDef.SubFields = " A.CG_CODIGO, A.PE_NOMDER, A.CA_CODCAR, A.PE_ZONCAT, A.TE_TIPOEX, " _
        '                    & " A.PE_METALI, SISGEM.PACK_DBA_SG_D_HECTAEXPEDIENTE.HECTA(A.CG_CODIGO, '01') " _
        '                    & " PE_HECFOR, A.SE_SITUEX, B.NC_NIVCOO,  B.CD_COREST, B.CD_CORNOR, NVL(B.CD_NUMPOL,1), " _
        '                    & " B.CD_NUMVER, B.CD_VERPOL, NVL(A.EE_ESTAEX,'Ñ'), A.SE_SITUEX, A.TE_TIPOEX, " _
        '                    & " SISGEM.PACK_DBA_SG_D_PETITORIO.ESTADO_GRAF(A.CG_CODIGO), NVL(A.PE_VIGCAT,'X')  "
        'pQueryDef.WhereClause = "  A.CG_CODIGO = B.CG_CODIGO AND A.PE_ZONCAT = '17' " _
        '                       & " ORDER BY A.CG_CODIGO, B.CD_NUMVER "

        pQueryDef.Tables = " SISGEM.SG_D_PETITORIOS A, SISGEM.SC_V_COORDENADADET B "
        pQueryDef.SubFields = " A.CG_CODIGO, A.PE_NOMDER, A.CA_CODCAR, A.PE_ZONCAT, A.TE_TIPOEX, A.PE_VIGCAT," _
                              & "A.PE_METALI, SISGEM.PACK_DBA_SG_D_PERTITULAR.ULT_VIGENTE(A.CG_CODIGO)  " _
                              & "TITULAR, SISGEM.PACK_DBA_SG_D_HECTAEXPEDIENTE.HECTA(A.CG_CODIGO, '01') " _
                              & " PE_HECFOR, A.SE_SITUEX, B.NC_NIVCOO,  B.CD_COREST, B.CD_CORNOR, NVL(B.CD_NUMPOL,1), " _
                              & " B.CD_NUMVER, B.CD_VERPOL"
        pQueryDef.WhereClause = " A.CG_CODIGO = B.CG_CODIGO AND A.CG_CODIGO = '" + codigo + "' AND " _
              & "B.NC_NIVCOO = SISGEM.PACK_DBA_SC_D_COORDENADADM.MAXNIVEL('" + codigo + "' )"

        'pQueryDef.Tables = " CATASTRO A "
        'pQueryDef.SubFields = " A.CG_CODIGO, A.CONCESION, A.CARTA, A.ZONA, A.TE_TIPOEX, " _
        '    & " A.PE_METALI, A.HECTAREA, A.SE_SITUEX, A.NC_NIVCOO, A.CD_COREST, " _
        '    & " A.CD_CORNOR, A.NUMPOL, A.CD_NUMVER, A.CD_VERPOL, NVL(A.ESTADO,'X'), A.TITULAR "
        'pQueryDef.WhereClause = "  A.ZONA = '19' AND A.VIGCAT = 'G' " _
        '                       & " ORDER BY A.CG_CODIGO, TO_NUMBER(A.CD_NUMVER) "
        'Dim cursor_filas As ICursor
        Dim filas_dm As IRow
        cursor_filas = pQueryDef.Evaluate
        filas_dm = cursor_filas.NextRow
        i = 0
        pPoint_1 = New Point
        'realizando la coleccion de coordenadas
        Codigo_1 = filas_dm.Value(0)
        'ca_codcar_1 = filas_dm.Value(2)
        'te_tipoex_1 = filas_dm.Value(4)
        'se_situex_1 = filas_dm.Value(7)
        pPoint_1.X = filas_dm.Value(11)
        pPoint_1.Y = filas_dm.Value(12)
        num_pol_1 = filas_dm.Value(13)
        'estado_1 = filas_dm.Value(14)
        'titular_1 = filas_dm.Value(15)
        '   ee_estaex_1 = filas_dm.Value(15)
        'pe_vigcat_1 = filas_dm.Value(18)
        i = i + 1
        filas_dm = cursor_filas.NextRow
        poligono = 0
        'Dim num_pol As Long
        j = 0
        Dim pLine As ILine
        r = 0
        p = 0
        pSegColl(poligono) = New Ring
        lointNumeroPoligono = 0
        paso = 0
        w = 0
        Do Until filas_dm Is Nothing
            pPoint_2 = New Point
            'realizando la coleccion de coordenadas
            Codigo_2 = filas_dm.Value(0)
            'ca_codcar_2 = filas_dm.Value(2)
            'te_tipoex_2 = filas_dm.Value(4)
            'se_situex_2 = filas_dm.Value(7)
            pPoint_2.X = filas_dm.Value(11)
            pPoint_2.Y = filas_dm.Value(12)
            num_pol_2 = filas_dm.Value(13)
            'estado_2 = filas_dm.Value(14)
            'titular_2 = filas_dm.Value(15)
            i = i + 1
            filas_dm = cursor_filas.NextRow
            Nro_Registro = i
            m_Codigo_NumReg = i
            If Codigo_1 = Codigo_2 Then
                If num_pol_1 = num_pol_2 Then
                    pLine = New Line
                    pLine.PutCoords(pPoint_1, pPoint_2)
                    If w = 0 Then
                        x_inicio = pPoint_1.X
                        y_inicio = pPoint_1.Y
                    End If
                    m(w, 0) = pPoint_1.X
                    m(w, 1) = pPoint_1.Y
                    pSegColl(poligono).AddSegment(pLine)
                    num_pol_1 = num_pol_2
                    Codigo_1 = Codigo_2
                    'ee_estaex_1 = ee_estaex_2
                    'se_situex_1 = se_situex_2
                    'te_tipoex_1 = te_tipoex_2
                    'ca_codcar_1 = ca_codcar_2
                    'estado_1 = estado_2
                    'pe_vigcat_1 = pe_vigcat_2
                    'titular_1 = titular_2
                    pPoint_1 = pPoint_2
                    w = w + 1
                Else
                    pRing(poligono) = pSegColl(poligono) 'QI entre ISegmentCollection
                    pRing(poligono).Close() 'Fermeture du polygone
                    poligono = poligono + 1
                    pSegColl(poligono) = New Ring
                    If paso = 0 Then
                        nislas = poligono - 1
                    End If
                    paso = paso + 1
                    num_pol_1 = num_pol_2
                    Codigo_1 = Codigo_2
                    'ee_estaex_1 = ee_estaex_2
                    'se_situex_1 = se_situex_2
                    'te_tipoex_1 = te_tipoex_2
                    'ca_codcar_1 = ca_codcar_2
                    'estado_1 = estado_2
                    'pe_vigcat_1 = pe_vigcat_2
                    'titular_1 = titular_2
                    pPoint_1 = pPoint_2
                End If
            Else
                m(w, 0) = pPoint_1.X
                m(w, 1) = pPoint_1.Y
                m(w + 1, 0) = x_inicio
                m(w + 1, 1) = y_inicio
                'Calcular Area
                Dim d0 As Double
                Dim d1 As Double
                d0 = 0
                d1 = 0
                For w = 0 To 500 - 1
                    If m(w, 0) <> 0 Or m(w, 1) <> 0 Then
                        d0 = d0 + m(w, 0) * m(w + 1, 1)
                        d1 = d1 + m(w, 1) * m(w + 1, 0)
                    Else
                        Exit For
                    End If
                Next w
                dr = Math.Abs((d0 - d1) / 2)
                Call Cerrar_Poligono(paso)
                ReDim m(0 To 500, 0 To 1)
                w = 0
                dr = 0
            End If
            j = j + 1
        Loop
        Call Cerrar_Poligono(paso)
        pEditor.StopEditing(True)
        Exit Sub
EH:
        MsgBox(Err.Description + Err.Number, vbInformation, "coneccion1" & Codigo_1)
    End Sub
    Private Sub Cerrar_Poligono(ByVal paso As Integer)
        If paso = 0 Then
            pRing(poligono) = pSegColl(poligono) 'QI entre ISegmentCollection
            pRing(poligono).Close() 'Fermeture du polygone
            Dim pPolygon As IGeometryCollection
            pPolygon = New Polygon
            pPolygon.AddGeometry(pRing(poligono))
            poligono = poligono + 1
            pSegColl(poligono) = New Ring
        ElseIf paso > 0 Then
            pRing(poligono) = pSegColl(poligono) 'QI entre ISegmentCollection
            pRing(poligono).Close() 'Fermeture du polygone
            poligono = poligono + 1
            pSegColl(poligono) = New Ring
            pPolygon = New Polygon
            For W = nislas To poligono - 1
                pPolygon.AddGeometry(pRing(W))
            Next W
        End If
        pFeatureVolcan = pFCLass.CreateFeature
        pFeatureVolcan.Value(pFeatureVolcan.Fields.FindField("OBJECTID")) = lointNumeroPoligono
        pFeatureVolcan.Value(pFeatureVolcan.Fields.FindField("CODIGOU")) = Codigo_1
        'pFeatureVolcan.Value(pFeatureVolcan.Fields.FindField("EE_ESTAEX")) = ee_estaex_1
        'pFeatureVolcan.Value(pFeatureVolcan.Fields.FindField("SE_SITUEX")) = se_situex_1
        'pFeatureVolcan.Value(pFeatureVolcan.Fields.FindField("TE_TIPOEX")) = te_tipoex_1
        'pFeatureVolcan.Value(pFeatureVolcan.Fields.FindField("CA_CODCAR")) = ca_codcar_1
        'pFeatureVolcan.Value(pFeatureVolcan.Fields.FindField("ESTADO")) = estado_1
        'pFeatureVolcan.Value(pFeatureVolcan.Fields.FindField("TITULAR")) = titular_1
        'pFeatureVolcan.Value(pFeatureVolcan.Fields.FindField("HA")) = dr
        '        pFeatureVolcan.Value(pFeatureVolcan.Fields.FindField("VIGCAT")) = pe_vigcat_1
        pFeatureVolcan.Shape = pPolygon
        pFeatureVolcan.Store()
        pEditor.StopOperation("Add Poygon")
        paso = 0
        lointNumeroPoligono = lointNumeroPoligono + 1
        'num_pol_1 = num_pol_2
        'Codigo_1 = Codigo_2
        'ee_estaex_1 = ee_estaex_2
        'se_situex_1 = se_situex_2
        'te_tipoex_1 = te_tipoex_2
        'ca_codcar_1 = ca_codcar_2
        'estado_1 = estado_2
        'pe_vigcat_1 = pe_vigcat_2
        'titular_1 = titular_2
        'pPoint_1 = pPoint_2
    End Sub

    Public Sub Activar_Layer_True_False(ByVal p_Opcion As Boolean, ByVal pApp As IApplication)
        pMxDoc = pApp.Document
        pMap = pMxDoc.FocusMap
        For x As Integer = 0 To pMap.LayerCount - 1
            If TypeOf pMap.Layer(x) Is IFeatureLayer Then
                Select Case pMap.Layer(x).Name
                    Case "Catastro"
                        pMap.Layer(x).Visible = p_Opcion
                    Case Else
                        pMap.Layer(x).Visible = True 'p_Opcion
                End Select
            End If
            '************* Si el layer el tipo Group FeatureLayer
            '    If TypeOf pMap.Layer(x) Is IGroupLayer Then
            '        Dim pGGFlayer As IGroupLayer
            '        pGGFlayer = pMap.Layer(x)
            '        If Not (pGGFlayer.Visible) Then pGGFlayer.Visible = p_Opcion
            '        pGFlayer = pGGFlayer
            '        For ii = 0 To pGFlayer.Count - 1
            '            If TypeOf pGFlayer.Layer(ii) Is IFeatureLayer Then
            '                If Not (pGFlayer.Layer(ii).Visible) Then pGFlayer.Layer(ii).Visible = p_Opcion
            '            End If
            '        Next ii
            '    End If
            '    '************* Si el layer el tipo WMS FeatureLayer
            '    If TypeOf pMap.Layer(x) Is IWMSGroupLayer Then
            '        Dim pWMSFlayer As IWMSGroupLayer
            '        If Not (pMap.Layer(x).Visible) Then pMap.Layer(x).Visible = p_Opcion
            '        pWMSFlayer = pMap.Layer(x)
            '        If Not (pWMSFlayer.Layer(0).Visible) Then pWMSFlayer.Layer(0).Visible = p_Opcion
            '        pGFlayer = pWMSFlayer.Layer(0)
            '        For ii = 0 To pGFlayer.Count - 1
            '            If TypeOf pGFlayer.Layer(ii) Is IWMSLayer Then
            '                If Not (pGFlayer.Layer(ii).Visible) Then pGFlayer.Layer(ii).Visible = p_Opcion
            '            End If
            '        Next ii
            '    End If
        Next
        pMxDoc.ActiveView.Refresh()
        pMxDoc.UpdateContents()
    End Sub

    Public Sub Activar_Layer_True_False_1(ByVal p_Opcion As Boolean, ByVal pApp As IApplication)

        pMxDoc = pApp.Document
        pMap = pMxDoc.FocusMap
        ReDim boo_Lista(pMap.LayerCount - 1)
        For x As Integer = 0 To pMap.LayerCount - 1
            boo_Lista(x) = pMap.Layer(x).Visible
            Select Case pMap.Layer(x).Name
                Case "Catastro"
                    pMap.Layer(x).Visible = p_Opcion
                Case Else
                    pMap.Layer(x).Visible = Not p_Opcion
            End Select
        Next

    End Sub
    Public Sub Activar_Lista_Layer(ByVal pApp As IApplication)
        pMxDoc = pApp.Document
        pMap = pMxDoc.FocusMap
        For x As Integer = 0 To pMap.LayerCount - 1
            pMap.Layer(x).Visible = boo_Lista(x)
        Next
        pMxDoc.ActiveView.Refresh()
        pMxDoc.UpdateContents()
    End Sub
End Class
