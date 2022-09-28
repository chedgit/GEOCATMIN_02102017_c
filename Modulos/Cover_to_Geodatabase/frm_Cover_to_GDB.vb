Imports ESRI.ArcGIS.GISClient
Imports ESRI.ArcGIS.Display
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
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

Imports ESRI.ArcGIS.Framework


Public Class frm_Cover_to_GDB
    Public m_application As IApplication

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        'CoverageToAccess()
        'Exit Sub

        Dim cadena_1 As String = ""
        'On Error Resume Next

        Dim pWorkspaceFactory As IWorkspaceFactory
        Dim pFeatureWorkspace As IFeatureWorkspace
        Dim pFeatureLayer As IFeatureLayer
        'Dim pMxDocument As IMxDocument
        'Dim pExtent As IEnvelope
        'Dim pMap As IMap
        Dim capas As String = ""
        Dim capa As String = ""
        Dim x_max, y_max, x_min, y_min As Double
        Dim capa_x_max As String = ""
        Dim capa_y_max As String = ""
        Dim capa_x_min As String = ""
        Dim capa_y_min As String = ""

        Dim listacov(60) As String
        Dim listafc(60) As String
        Dim cuad As String = ""
        'Dim DTS As String
        Dim FC As String
        Dim Escala, zona, ruta As String
        Dim Cuad_m As String = ""


        Escala = cboEscala.Text 'value
        cuad = txtHoja.Text
        zona = cboZona.Text
        ruta = Me.txtUbica.Text

        If (cuad = "") Then
            MsgBox("El campo Cuadrángulo está vacío...", vbCritical)
            Exit Sub
        End If

        If (ruta = "") Then
            MsgBox("El campo Ruta está vacío...", vbCritical)
            Exit Sub
        End If

        Dim pWorkFact As IWorkspaceFactory2
        Dim pWorkspace As IWorkspace
        pWorkFact = New SdeWorkspaceFactory
        pWorkspace = pWorkFact.OpenFromString("server=10.102.0.12;instance=5151;user=BDGINGE;password=BDGINGE;version=SDE.DEFAULT;", 0)

        'Dim pWorkFact As IWorkspaceFactory
        'Dim pWorkspace As IFeatureWorkspace
        'pWorkFact = New AccessWorkspaceFactory
        'pWorkspace = pWorkFact.OpenFromFile("F:\COMPOSICION\26-N.MDB", 0)

        If (Len(cuad) = 5) Then
            Cuad_m = Microsoft.VisualBasic.Left(cuad, 2) & Microsoft.VisualBasic.Right(cuad, 2)
        ElseIf (Len(cuad) = 4) Then
            Cuad_m = Microsoft.VisualBasic.Left(cuad, 2) & Microsoft.VisualBasic.Right(cuad, 1)
        End If

        'If cuad Is Null Then
        '  Exit Sub
        'End If

        '*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
        'Lista de coverage de la carta geológica...
        '*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
        listacov(1) = "geolutmm:polygon"
        listacov(2) = "mallautm:arc"
        'listacov(1) = "alteutm:polygon"
        'listacov(2) = "buzautm:arc"
        'listacov(3) = "cajautm:polygon"
        'listacov(4) = "cerroutm:arc"
        'listacov(5) = "ciudutm:polygon"
        'listacov(6) = "cotautm:point"
        'listacov(7) = "drenutm:arc"
        'listacov(8) = "escarutm:arc"
        'listacov(9) = "fallutm:arc"
        'listacov(10) = "fosilutm:point"
        'listacov(11) = "geolutmm:arc"
        'listacov(12) = "geolutmm:polygon"
        'listacov(13) = "laguutm:arc"
        'listacov(14) = "laguutm:polygon"
        'listacov(15) = "leyenda:arc"
        'listacov(16) = "leyenda:polygon"
        'listacov(17) = "mallautmm:arc"
        'listacov(18) = "plieutm:arc"
        'listacov(19) = "puebutm:point"
        'listacov(20) = "simbologia:arc"
        'listacov(21) = "simbologia:point"
        'listacov(22) = "topoutm:arc"
        'listacov(23) = "viasutm:arc"


        'Create a new ArcInfoWorkspaceFactory object and open a ArcInfo folder
        pWorkspaceFactory = New ArcInfoWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(ruta, 0)

        x_max = 0
        y_max = 0
        x_min = 1000000
        y_min = 10000000

        For cov As Integer = 1 To 2

            capa = listacov(cov)
            pFeatureLayer = New FeatureLayer
            pFeatureLayer.FeatureClass = pFeatureWorkspace.OpenFeatureClass(capa)

            '-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
            'Verificando si existe la cobertura...
            '-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

            If Not pFeatureLayer.FeatureClass Is Nothing Then

                capas = capas & " / " & capa

                Dim pFeatureCursor As IFeatureCursor
                pFeatureCursor = pFeatureLayer.FeatureClass.Search(Nothing, False)

                Dim pFeat As IFeature
                pFeat = pFeatureCursor.NextFeature

                'Barriendo cada feature para determinar el dominio del DataSet...

                Do Until pFeat Is Nothing
                    If (pFeat.Extent.XMax > x_max) Then
                        x_max = pFeat.Extent.XMax
                        capa_x_max = capa
                    End If
                    If (pFeat.Extent.YMax > y_max) Then
                        y_max = pFeat.Extent.YMax
                        capa_y_max = capa
                    End If
                    If (pFeat.Extent.XMin < x_min) Then
                        x_min = pFeat.Extent.XMin
                        capa_x_min = capa
                    End If
                    If (pFeat.Extent.YMin < y_min) Then
                        y_min = pFeat.Extent.YMin
                        capa_y_min = capa
                    End If
                    pFeat = pFeatureCursor.NextFeature
                Loop

            End If

        Next cov

        x_max = x_max + 5000
        y_max = y_max + 5000
        x_min = x_min - 5000
        y_min = y_min - 5000

        cadena_1 = " X Min. : " & x_min & " Capa: " & capa_x_min & "  X Max. : " & x_max & "  Capa: " & capa_x_max & _
                   " Y Min. : " & y_min & " Capa: " & capa_y_min & "  Y Max. : " & y_max & "  Capa: " & capa_y_max

        '-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
        'Definiendo sistema de coordenadas...
        '-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*

        Dim pSpatRefFact As ISpatialReferenceFactory
        pSpatRefFact = New SpatialReferenceEnvironment

        'Create the pre-defined projected coordinate system object

        Dim pProjCoordSys As IProjectedCoordinateSystem = Nothing

        If (zona = "17") Then
            pProjCoordSys = pSpatRefFact.CreateProjectedCoordinateSystem(esriSRProjCSType.esriSRProjCS_WGS1984UTM_17S)
        ElseIf (zona = "18") Then
            pProjCoordSys = pSpatRefFact.CreateProjectedCoordinateSystem(esriSRProjCSType.esriSRProjCS_WGS1984UTM_18S)
        ElseIf (zona = "19") Then
            pProjCoordSys = pSpatRefFact.CreateProjectedCoordinateSystem(esriSRProjCSType.esriSRProjCS_WGS1984UTM_19S)
        End If

        Dim pSpatRef As ISpatialReference
        pSpatRef = pProjCoordSys

        'Set the domain extents for the new dataset
        pSpatRef.SetDomain(x_min, x_max, y_min, y_max)

        '-*-*-*-*-*-*-*-*-*-*-
        'Creando el DataSet...
        '-*-*-*-*-*-*-*-*-*-*-
        ' Call createDataset("CG" & Escala & "_" & Cuad_m, pWorkspace, pSpatRef)

        '-*-*-*-*-*-*-*-*-*-*-*-*-*-
        'Creando los FeatureClass...
        '-*-*-*-*-*-*-*-*-*-*-*-*-*-

        listafc(1) = "Geologia" & Escala & Cuad_m & "_poly "
        listafc(2) = "Malla" & Escala & Cuad_m & "_line"
        'listafc(1) = "Alteracion" & Escala & Cuad_m & "_poly "
        'listafc(2) = "Buzamiento" & Escala & Cuad_m & "_line"
        'listafc(3) = "Caja" & Escala & Cuad_m & "_poly "
        'listafc(4) = "cerro" & Escala & Cuad_m & "_line"
        'listafc(5) = "Ciudad" & Escala & Cuad_m & "_poly "
        'listafc(6) = "Cota" & Escala & Cuad_m & "_point"
        'listafc(7) = "Drenaje" & Escala & Cuad_m & "_line"
        'listafc(8) = "Escarpa" & Escala & Cuad_m & "_line"
        'listafc(9) = "Falla" & Escala & Cuad_m & "_line"
        'listafc(10) = "Fosil" & Escala & Cuad_m & "_point"
        'listafc(11) = "Geologia" & Escala & Cuad_m & "_line"
        'listafc(12) = "Geologia" & Escala & Cuad_m & "_poly "
        'listafc(13) = "Laguna" & Escala & Cuad_m & "_line"
        'listafc(14) = "Laguna" & Escala & Cuad_m & "_poly "
        'listafc(15) = "Leyenda" & Escala & Cuad_m & "_line"
        'listafc(16) = "Leyenda" & Escala & Cuad_m & "_poly "
        'listafc(17) = "Malla" & Escala & Cuad_m & "_line"
        'listafc(18) = "Malla" & Escala & Cuad_m & "_line"
        'listafc(19) = "Pueblo" & Escala & Cuad_m & "_point"
        'listafc(20) = "Simbologia" & Escala & Cuad_m & "_line"
        'listafc(21) = "Simbologia" & Escala & Cuad_m & "_point"
        'listafc(22) = "Topografia" & Escala & Cuad_m & "_line"
        'listafc(23) = "Vias" & Escala & Cuad_m & "_line"

        ' +++ Set connection properties. Change the properties to match your
        ' +++ server name, instance, user name and password for your SDE database

        Dim poutsdepropset As IPropertySet
        poutsdepropset = New PropertySet
        With poutsdepropset
            .SetProperty("server", "10.102.0.12")
            .SetProperty("instance", "5151")
            .SetProperty("user", "BDGINGE")
            .SetProperty("password", "BDGINGE")
            .SetProperty("version", "SDE.DEFAULT")
        End With

        ' +++ create a new feature datset name object for the output sde feature dataset
        Dim poutsdeworkspacename As IWorkspaceName
        poutsdeworkspacename = New WorkspaceName

        poutsdeworkspacename.ConnectionProperties = poutsdepropset
        poutsdeworkspacename.WorkspaceFactoryProgID = "esricore.sdeworkspacefactory.1"

        ''pWorkFact = New AccessWorkspaceFactory
        ''pWorkspace = pWorkFact.OpenFromFile("F:\COMPOSICION\26-N.MDB", 0)

        'pWorkFact = New SdeWorkspaceFactory
        'pWorkspace = pWorkFact.OpenFromString("server=10.102.0.12;instance=5151;user=bdginge;password=bdginge;version=SDE.DEFAULT;", 0)

        Dim pOutSDEFeatDSName As IFeatureDatasetName
        pOutSDEFeatDSName = New FeatureDatasetName

        Dim pSDEDSname As IDatasetName
        pSDEDSname = pOutSDEFeatDSName
        pSDEDSname.WorkspaceName = poutsdeworkspacename
        pSDEDSname.Name = "CG" & Escala & "_" & Cuad_m

        'Barra de progreso....
        '---------------------
        'Dim psbar As IStatusBar
        Dim psbar As ESRI.ArcGIS.esriSystem.IStatusBar
        psbar = m_application.StatusBar
        Dim pPro As IStepProgressor
        pPro = psbar.ProgressBar

        'Set pfeature = pFeatCursor.NextFeature
        'lFieldIndex = pfeature.Fields.FindField("POP1990")

        'Dim PauseTime, Start, Finish, TotalTime
        Dim PauseTime, Start
        PauseTime = 0.5
        pPro.MinRange = 1
        pPro.MaxRange = 60
        pPro.StepValue = 1
        '----------------------

        For cov As Integer = 1 To 2

            capa = listacov(cov)
            FC = listafc(cov)
            pFeatureLayer = New FeatureLayer
            pFeatureLayer.FeatureClass = pFeatureWorkspace.OpenFeatureClass(capa)

            If Not pFeatureLayer.FeatureClass Is Nothing Then

                ' +++ Get the name object for the input shapefile workspace
                Dim pInCovWorkspaceName As IWorkspaceName
                pInCovWorkspaceName = New WorkspaceName
                pInCovWorkspaceName.PathName = ruta
                pInCovWorkspaceName.WorkspaceFactoryProgID = "esriCore.ArcInfoWorkspaceFactory.1"

                Dim pInCovFeatCLSNm As IFeatureClassName
                pInCovFeatCLSNm = New FeatureClassName

                Dim pCovDatasetName As IDatasetName
                pCovDatasetName = pInCovFeatCLSNm
                pCovDatasetName.Name = capa
                pCovDatasetName.WorkspaceName = pInCovWorkspaceName

                ' +++ create the new output FeatureClass name object that will be passed
                ' +++ into the conversion function
                Dim pOutputDSName As IDatasetName
                Dim pOutputFCName As IFeatureClassName
                pOutputFCName = New FeatureClassName
                pOutputDSName = pOutputFCName
                Dim pInDSNAme As IDatasetName

                ' +++ Set the new FeatureClass name to be the same as the input FeatureClass name
                pInDSNAme = pInCovFeatCLSNm
                pOutputDSName.Name = FC

                ' +++ Open the input Shapefile FeatureClass object, so that we can get its fields
                Dim pName As IName
                Dim pInCovFeatCls As IFeatureClass
                pName = pInCovFeatCLSNm
                pInCovFeatCls = pName.Open

                ' +++ Get the fields for the input feature class and run them through
                ' +++ field checker to make sure there are no illegal or duplicate field names
                Dim pOutSDEFlds As IFields = Nothing
                Dim pInCovFlds As IFields
                Dim pFldChk As IFieldChecker
                Dim i As Long
                Dim pGeoField As IField = Nothing
                Dim pOutSDEGeoDef As IGeometryDef
                Dim pOutSDEGeoDefEdit As IGeometryDefEdit
                pInCovFlds = pInCovFeatCls.Fields
                pFldChk = New FieldChecker
                pFldChk.Validate(pInCovFlds, Nothing, pOutSDEFlds)

                ' +++ Loop through the output fields to find the geometry field
                For i = 0 To pOutSDEFlds.FieldCount
                    If pOutSDEFlds.Field(i).Type = esriFieldType.esriFieldTypeGeometry Then ' esriFieldTypeGeometry Then
                        pGeoField = pOutSDEFlds.Field(i)
                        Exit For
                    End If
                Next i

                ' +++ Get the geometry field's geometry definition
                pOutSDEGeoDef = pGeoField.GeometryDef

                ' +++ Give the geometry definition a spatial index grid count and grid size
                pOutSDEGeoDefEdit = pOutSDEGeoDef
                pOutSDEGeoDefEdit.GridCount_2 = 1 '.GridCount = 1
                pOutSDEGeoDefEdit.GridSize_2(0) = 1500000 '.GridSize(0) = 1500000

                ' +++ Now use IFeatureDataConverter::Convert to create the output FeatureDataset and
                ' +++ FeatureClass.
                Dim pCovToFc As IFeatureDataConverter
                pCovToFc = New FeatureDataConverter
                pCovToFc.ConvertFeatureClass(pInCovFeatCLSNm, Nothing, pOutSDEFeatDSName, _
                                   pOutputFCName, Nothing, pOutSDEFlds, Nothing, 0, 0)
                'MsgBox capa, vbCritical, "Convertir ArcInfo Coverage"
            End If
            'Barra de progreso....
            '---------------------
            pPro.Position = cov
            pPro.Message = "Migración a la GDB...Procesando Coverage de ArcInfo : [ " & capa & " ] " & Str(cov) & " de 60"
            pPro.Step()
            pPro.Show()
            Start = Timer
            'Do While Timer < Start + PauseTime
            '    DoEvents()
            'Loop
            '---------------------
        Next cov
        pPro.Hide()
        MsgBox("La carga de información a la GeoDataBase ha terminado!...", vbInformation, "Creación de FeatureClass...")
    End Sub


    Public Function createDataset(ByVal Name As String, ByVal workspace As IWorkspace, _
    Optional ByVal pSR As ISpatialReference = Nothing) As IFeatureDataset

        '' createDataset:  creates a dataset in a workspace
        '' ISpatialReference is optional but should generally be defined by the caller
        ''
        Dim pFeatureWorkspace As IFeatureWorkspace
        On Error GoTo EH

        createDataset = Nothing
        If workspace Is Nothing Then Exit Function

        ' if the spatial reference is not passed in, then create an unknown one here
        If (pSR Is Nothing) Then 'Or IsMissing(pSR) Then
            pSR = New UnknownCoordinateSystem
            pSR.SetDomain(0, 21474.83645, 0, 21474.83645)
            pSR.SetFalseOriginAndUnits(0, 0, 100000)
            pSR.SetMDomain(0, 21474.83645)
            pSR.SetZDomain(0, 21474.83645)
            pSR.SetZFalseOriginAndUnits(0, 100000)
        End If

        pFeatureWorkspace = workspace

        ' create the feature dataset
        createDataset = pFeatureWorkspace.CreateFeatureDataset(Name, pSR)

        Exit Function
EH:
        MsgBox(Err.Description, vbInformation, "CreateDataset")
        MsgBox("No se ha creado correctamente el DataSet ", vbCritical, "CreateDataset")

    End Function

    Private Sub frm_Cover_to_GDB_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboZona.Items.Add("17")
        cboZona.Items.Add("18")
        cboZona.Items.Add("19")
        cboEscala.Items.Add("50k")
        cboEscala.Items.Add("100k")
        Me.cboZona.SelectedIndex = 0
        Me.cboEscala.SelectedIndex = 0
    End Sub

    Private Sub CoverageToAccess()
        Dim listacov(23) As String
        'Dim listafc(23) As String
        '*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
        'Lista de coverage de la carta geológica...
        '*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

        listacov(1) = "buzautm:arc"
        listacov(2) = "drenutm:arc"
        listacov(3) = "fallutm:arc"
        listacov(4) = "fosilutm:point"
        listacov(5) = "geolutmm:polygon"
        listacov(6) = "leyenda:arc"
        listacov(7) = "mallautmm:arc"
        listacov(8) = "minautm:point"
        listacov(9) = "petroutm:point"
        listacov(10) = "plieutm:arc"
        listacov(11) = "puebutm:point"
        listacov(12) = "simbologia:arc"
        listacov(13) = "topoutm:arc"
        listacov(14) = "viasutm:arc"
        Dim lo_capa = 14

        'listacov(1) = "alteutm:polygon"
        'listacov(2) = "buzautm:arc"
        'listacov(3) = "cajautm:polygon"
        'listacov(4) = "cerroutm:arc"
        'listacov(5) = "ciudutm:polygon"
        'listacov(6) = "cotautm:point"
        'listacov(7) = "drenutm:arc"
        'listacov(8) = "escarutm:arc"
        'listacov(9) = "fallutm:arc"
        'listacov(10) = "fosilutm:point"
        'listacov(11) = "geolutmm:arc"
        'listacov(12) = "geolutmm:polygon"
        'listacov(13) = "laguutm:arc"
        'listacov(14) = "laguutm:polygon"
        'listacov(15) = "leyenda:arc"
        'listacov(16) = "leyenda:polygon"
        'listacov(17) = "mallautmm:arc"
        'listacov(18) = "plieutm:arc"
        'listacov(19) = "puebutm:point"
        'listacov(20) = "simbologia:arc"
        'listacov(21) = "simbologia:point"
        'listacov(22) = "topoutm:arc"
        'listacov(23) = "viasutm:arc"
        'Dim escala, cuad_m As String
        'listafc(1) = "alteutm" & escala & cuad_m & "_poly "
        'listafc(2) = "buzautm" & escala & cuad_m & "_line"
        'listafc(3) = "cajautm" & escala & cuad_m & "_poly "
        'listafc(4) = "cerroutm" & escala & cuad_m & "_line"
        'listafc(5) = "ciudutm" & escala & cuad_m & "_poly "
        'listafc(6) = "cotautm" & escala & cuad_m & "_point"
        'listafc(7) = "drenutm" & escala & cuad_m & "_line"
        'listafc(8) = "escarutm" & escala & cuad_m & "_line"
        'listafc(9) = "fallutm" & escala & cuad_m & "_line"
        'listafc(10) = "Fosil" & Escala & Cuad_m & "_point"
        'listafc(11) = "Geologia" & Escala & Cuad_m & "_line"
        'listafc(12) = "geolutmm" ' & escala & cuad_m & "_poly "
        'listafc(13) = "Laguna" & Escala & Cuad_m & "_line"
        'listafc(14) = "Laguna" & Escala & Cuad_m & "_poly "
        'listafc(15) = "Leyenda" & Escala & Cuad_m & "_line"
        'listafc(16) = "Leyenda" & Escala & Cuad_m & "_poly "
        'listafc(17) = "Malla" & Escala & Cuad_m & "_line"
        'listafc(18) = "Malla" & Escala & Cuad_m & "_line"
        'listafc(19) = "Pueblo" & Escala & Cuad_m & "_point"
        'listafc(20) = "Simbologia" & Escala & Cuad_m & "_line"
        'listafc(21) = "Simbologia" & Escala & Cuad_m & "_point"
        'listafc(22) = "Topografia" & Escala & Cuad_m & "_line"
        'listafc(23) = "Vias" & Escala & Cuad_m & "_line"



        Dim ruta As String = "f:\Composicion"
        ' Part 1: Define the output.
        Dim pPropset As IPropertySet
        Dim pOutAcFact As IWorkspaceFactory
        Dim pOutAcWorkspaceName As IWorkspaceName
        Dim pOutAcFCName As IFeatureClassName
        Dim pOutAcDSName As IDatasetName
        ' Set the property named database.
        pPropset = New PropertySet
        'pPropset.SetProperty("Database", "c:\data\chap6")
        pPropset.SetProperty("Database", ruta)
        ' Define the workspace.
        pOutAcFact = New AccessWorkspaceFactory
        'pOutAcWorkspaceName = pOutAcFact.Create("c:\data\chap6", "emida", pPropset, 0)
        pOutAcWorkspaceName = pOutAcFact.Create(ruta, "geo33R", pPropset, 0)
        ' Define the dataset.
        pOutAcFCName = New FeatureClassName
        pOutAcDSName = pOutAcFCName
        pOutAcDSName.WorkspaceName = pOutAcWorkspaceName
        For i As Integer = 1 To lo_capa
            'pOutAcDSName.Name = "breakstrm"
            pOutAcDSName.Name = Mid(listacov(i), 1, InStr(listacov(i), ":") - 1)
            ' Part 2: Specify the Input.
            Dim pInCovWorkspaceName As IWorkspaceName
            Dim pFCName As IFeatureClassName
            Dim pCovDatasetName As IDatasetName
            ' Define the workspace.
            pInCovWorkspaceName = New WorkspaceName
            'pInCovWorkspaceName.PathName = "c:\data\chap6"
            pInCovWorkspaceName.PathName = ruta
            pInCovWorkspaceName.WorkspaceFactoryProgID = "esriCore.ArcInfoWorkspaceFactory.1"
            ' Define the dataset of the coverage to be converted.
            pFCName = New FeatureClassName
            pCovDatasetName = pFCName
            pCovDatasetName.Name = listacov(i).ToString ' "geolutmm:polygon"

            pCovDatasetName.WorkspaceName = pInCovWorkspaceName

            ' Part 3: Perform data conversion.
            Dim pCovtoFC As IFeatureDataConverter
            pCovtoFC = New FeatureDataConverter
            pCovtoFC.ConvertFeatureClass(pFCName, Nothing, Nothing, pOutAcFCName, Nothing, Nothing, "", 1000, 0)
        Next i
        MsgBox("Coverage conversion complete!")

    End Sub

End Class