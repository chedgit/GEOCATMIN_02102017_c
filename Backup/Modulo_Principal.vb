Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports PORTAL_Clases

Module Modulo_Principal
    'Modificado 23/08/2010
    Public objConexion As New PORTAL_Clases.clsBD_Conexion
    Public gstrConexion As String = ""
    '*******
    Public gstrUsuarioAcceso As String
    Public gstrUsuarioClave As String
    Public gstrDatabase As String = "ORACLE"
    'Public gstrDatabase As String = "DESA"
    Public gstrDatabaseGIS As String = "BDGEOCAT"
    Public gstrCodigo_Usuario As String = "" 'Codigo de Usuario
    Public gstrNombre_Usuario As String = "" 'Nombre Completo Usuario
    Public pgloUsuConexionGIS As String = ""
    Public pgloUsuConexionOracle As String = ""
    '******GEODATABASE - SDE
    'Public glo_Server_SDE As String = "SRVDESA03" '
    Public glo_Server_SDE As String = "10.102.0.30" '
    Public glo_Instance_SDE As String = "5152"
    Public glo_Instance_SDE_1 As String = "5151"
    Public glo_User_SDE As String = ""
    Public glo_Password_SDE As String = ""
    Public glo_Owner_Layer_SDE As String = "DATA_GIS."
    Public glo_Version_SDE As String = "SDE.DEFAULT"
    Public glo_InformeDM As String = ""
    '*******
    Public cadena_query_ar As String = ""
    Public GloInt_Opcion As Integer = 0
    Public glo_Inicio_SDE As String = False
    Public glo_Desarrollo_BD As String = ""
    Public gstrPerfil_Usuario As String = ""
    'Bottones de la Barra de Herramientas
    Public glo_Tool_BT_1 As Boolean = True
    Public glo_Tool_BT_2 As Boolean = True
    Public glo_Tool_BT_3 As Boolean = True
    Public glo_Tool_BT_4 As Boolean = True ' Ver todos OK
    Public glo_Tool_BT_5 As Boolean = True 'ver Anterior, Posterior, Colindante
    Public glo_Tool_BT_6 As Boolean = True 'Posterior
    Public glo_Tool_BT_7 As Boolean = True 'Simultaneo
    Public glo_Tool_BT_8 As Boolean = True 'Extinguido
    Public glo_Tool_BT_9 As Boolean = True
    Public glo_Tool_BT_10 As Boolean = True
    Public glo_Tool_BT_11 As Boolean = True 'Colindante
    Public glo_Tool_BT_12 As Boolean = True
    Public glo_Tool_BT_13 As Boolean = True
    Public glo_Tool_BT_14 As Boolean = True
    Public glo_Tool_BT_15 As Boolean = True
    Public glo_Tool_BT_16 As Boolean = True
    Public glo_Tool_BT_17 As Boolean = True
    Public glo_Tool_BT_18 As Boolean = True
    Public glo_Tool_BT_19 As Boolean = True
    Public glo_Tool_BT_20 As Boolean = True
    Public glo_Tool_BT_21 As Boolean = True 'Evaluado OK
    Public glo_Tool_BT_22 As Boolean = True 'Anterior - Prioritaros
    Public glo_Tool_BT_23 As Boolean = True 'Anterior y Posterior
    Public glo_Tool_BT_24 As Boolean = True 'Redenuncio
    Public glo_Tool_BT_25 As Boolean = True
    Public glo_Tool_BT_26 As Boolean = True
    Public glo_int_Perfil As Integer = 0
    Public glo_Tool_EVA_01 As Boolean = True
    Public glo_Tool_EVA_02 As Boolean = False
    '*******
    'Feature Class
    Public gstrFC_CDistrito = ""
    Public gstrFC_Departamento = ""
    Public gstrFC_Provincia = ""
    Public gstrFC_Distrito = ""
    Public gstrFC_Rios = ""
    Public gstrFC_Carretera = ""
    Public gstrFC_CPoblado = ""
    Public gstrFC_Catastro_Minero = ""
    Public gstrFC_Cuadricula = ""
    Public gstrFC_AReservada = ""
    Public gstrFC_ZUrbana = ""
    Public gstrFC_ZUrbana56 = ""
    Public gstrFC_AReservada56 As String = ""
    Public gstrFC_ZTraslape = ""
    Public gstrFC_Frontera = ""
    Public gstrFC_LHojas = ""
    Public gstrFC_Carta = ""
    Public v_Informe As String = ""
    '**************
    Public Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Int32, ByVal nIndex As Int32, ByVal dwNewLong As Int32) As Int32
    Public Const GWL_HWNDPARENT = (-8)
    Public pSpatialReferenceEnvelope As SpatialReferenceEnvironment = New SpatialReferenceEnvironment
    Public Datum_PSAD As ISpatialReference = Nothing 'pSpatialReferenceEnvelope.CreateProjectedCoordinateSystem(esriSRProjCSType.esriSRProjCS_PSADUTM_17S)
    Public Datum_PSAD_17 As ISpatialReference = pSpatialReferenceEnvelope.CreateProjectedCoordinateSystem(esriSRProjCSType.esriSRProjCS_PSADUTM_17S)
    Public Datum_PSAD_18 As ISpatialReference = pSpatialReferenceEnvelope.CreateProjectedCoordinateSystem(esriSRProjCSType.esriSRProjCS_PSADUTM_18S)
    Public Datum_PSAD_19 As ISpatialReference = pSpatialReferenceEnvelope.CreateProjectedCoordinateSystem(esriSRProjCSType.esriSRProjCS_PSADUTM_19S)
    Public gostrDpto, gostrProv, gostrDist, gostrUbigeo, gostrHoja, gostrCod_IGN, gostrCod_QTE, gostrCod_Franja As String
    Public v_Boo_Dpto, v_Boo_Prov, v_Boo_Dist, v_Boo_Cuadricula, v_Boo_Hidrografia As Boolean
    Public v_Boo_Red_Vial, v_Boo_Pueblo, v_Boo_Frontera As Boolean
    '*
    Public gloTool_Generar_Malla As Boolean = False
    Public glo_Layer_Simulado As String = ""
    '*
    Public lodtbdgdInterno As New DataTable
    Public lodtTotales As New DataTable
    Public lodtbPerfil As New DataTable
    Public loglo_MensajeDM As String = ""
    Public glostrUnidad As String = objConexion.Unidad
    Public loglo_Titulo As String = ""
    Public logloNumRegistro As Integer
    Public logloNumRegistroNew As Integer
    Public pWorkspaceFactory As IWorkspaceFactory
    Public pFeatureWorkspace As IFeatureWorkspace
    Public pFeatureSelection As IFeatureSelection
    Public pFeatureLayer As IFeatureLayer
    Public pFeatureClass As IFeatureClass
    Public pSelectionSet As ISelectionSet
    Public pFeatureCursor As IFeatureCursor
    Public pLayer As ILayer
    Public pFeature As IFeature
    Public pFields As IFields
    Public pTable As ITable
    Public m_application As IApplication
    Public pMxDoc As IMxDocument
    Public pMap As Map
    Public pPoint As IPoint
    Public x, y As Double
    Public glo_Path As String = glostrUnidad & "BDGEOCATMIN\"
    Public glo_DirectorCM = ""
    Public glo_Elaboradop = ""
    Public glo_pathServidor = "" '"\\jerupaja\bdgis\" 'glo_pathGeoCatMin
    Public glo_pathTMP As String = glo_Path & "Temporal"
    Public glo_PathGDB As String = glo_Path & "bdgeocatmin.mdb"
    Public glo_pathStyle As String = glo_Path & "Estilos"
    Public glo_pathStyle_paises As String = "U:\Geocatmin\Estilos\"
    Public glo_pathIGN As String = ""

    Public glo_pathImaSat As String = ""
    Public glo_pathMXT As String = ""
    Public glo_pathGEO As String = ""
    Public glo_pathREP As String = ""
    Public glo_pathPNG As String = ""
    'Public glo_pathIGN As String = glo_pathGeoCatMin & "datos\ecw_cartas"
    'Public glo_pathImaSat As String = glo_pathGeoCatMin & "Geocatmin\Imagenes\"
    'Public glo_pathMXT As String = glo_pathGeoCatMin & "Geocatmin\Plantillas\"
    'Public glo_pathGEO As String = glo_pathGeoCatMin & "Geocatmin\Geologia\"
    'Public glo_pathREP As String = glo_pathGeoCatMin & "Geocatmin\Reporte\"
    'Public glo_pathPNG As String = glo_pathGeoCatMin & "Geocatmin\Menu_Imagen\"

    Public tmp_Prov(200, 1) As String
    Public tmp_Dist(1830, 1) As String
    Public loint_Intervalo As Integer
    Public glostrNaturaleza As String = ""
    Public glostrPartida As String = ""
    Public glostrPadron As String = ""
    Public glostrJefatura As String = ""
    Public glostrTipoDer As String = ""
    Public gloint_Acceso As String

    Public txhAreaUsuario As String = ""
    Public txhSimular As String = "No"
    Public gloEsteMin As Double = 0
    Public gloEsteMax As Double = 0
    Public gloNorteMin As Double = 0
    Public gloNorteMax As Double = 0
    Public gloZona As Integer = 0
    Public glo_xMin As Double : Public glo_yMin As Double : Public glo_xMax As Double : Public glo_yMax As Double
    Public lo_Index_Carta As String = ""

    'para evaluacion ********

    Public v_codigo As String
    Public tipo_seleccion As String
    Public tipo_consulta As String
    Public Codigo_dm_v As String
    Public v_incopora As String

    'Esto es para los DM diferentes al Evaluado

    Public nombre_dm_x As String
    Public v_tipo_exp_x As String
    Public v_fec_denun_x As Date
    Public v_hor_denun_x As String

    Public v_estado_x As String
    Public v_estado_u As Double
    Public v_estado_u_eval As Double
    Public v_eval_x As String
    Public v_hecta_x As String
    Public v_hecta_reg As Double

    Public v_demagis As String
    Public v_codigo_x As String
    Public v_fec_libdenu_x As Date
    Public v_identi_eval_x As String
    Public v_incopora_x As String
    Public MyDate As Date
    Public Hora As Object


    Public tipo_dm As String
    Public ZONA_DM As String
    Public carta_dm As String
    Public situacion_dm As String
    Public v_tipo_exp As String
    Public v_fec_denun As Date
    Public v_hor_denun As String
    Public v_estado As String
    Public v_eval As String
    Public v_fec_eval As Date
    Public v_fec_libdenu As Date
    Public v_identi_eval As String
    Public titular As String
    Public p_grafica As String
    Public V_conta As String
    Public v_distrito As String
    Public v_provincia As String
    Public v_departamento As String
    Public v_deiden As String
    Public v_depubl As String
    Public v_naturaleza As String

    Public v_corre1 As Boolean
    Public v_letra_x As String
    Public v_letra_ev As String
    Public v_cod_x As String
    Public v_cod_ev As String

    Public v_corre2 As Boolean
    Public v_valor1 As Long
    Public v_valor2 As Long

    'demarcacion


    Public nombre_dataframe As String
    Public lista_nm_depa As String = ""
    Public lista_nm_depa_mod As String = ""
    Public lista_hojas As String = ""
    Public arch_cata As String = ""
    Public v_este_min As Double = 0
    Public v_este_max As Double = 0
    Public v_norte_min As Double = 0
    Public v_norte_max As Double = 0
    Public colecciones As New Collection
    Public carta_v As String = ""
    Public conta_po_posi_y As Double
    Public conta_po_posi_y1 As Double
    Public conta_si_posi_y As Double
    Public conta_si_posi_y1 As Double
    Public conta_ex_posi_y As Double
    Public conta_ex_posi_y1 As Double
    Public posi_y_priori1_CNM As Double
    Public posi_y_priori_CNM As Double
    Public conta_hoja As Integer
    Public pMapFrame1 As IMapFrame
    Public Cuenta_po As Integer
    Public Cuenta_an As Integer
    Public Cuenta_si As Integer
    Public Cuenta_ex As Integer
    Public pGC As IGraphicsContainer
    Public escalaf As Integer
    Public caso_simula As String
    Public sele_plano As String = ""
    Public distancia_fron As Double = 0
    Public lista_rese As String = ""
    Public lista_urba As String = ""
    Public colecciones_rese As New Collection
    Public colecciones_urba As New Collection
    Public V_caso_simu As String = ""
    Public V_zona_simu As String
    Public escala_s As Integer
    Public sele_plano_sup As String
    Public m_pMapGrid As IMapGrid
    Public pMeasuredGrid As IMeasuredGrid
    Public colecciones_dist As New Collection
    Public colecciones_prov As New Collection
    Public colecciones_depa As New Collection
    Public lista_depa As String = ""
    Public lista_depa_mod As String = ""
    Public lista_prov As String = ""
    Public lista_dist As String = ""
    Public v_nombre_dm As String = ""
    Public v_zona_dm As String
    Public v_fecha_dm As String = ""
    Public escala_plano_eval As Integer
    Public escala_plano_dema As Integer
    Public escala_plano_carta As Integer
    Public lista_cartas As String
    Public colecciones_nmhojas As New Collection
    Public lista_nmhojas As String
    Public lista_nmhojas_ign As String
    Public pGraphicsContainer As IGraphicsContainer
    Public pMapFrame As IFrameElement
    Public pSymbolBackground As ISymbolBackground
    Public casoleyenda As String = ""
    Public valida As Boolean
    Public caso_consulta As String
    Public conta_opcion_cartaIGN As Integer = 0  'contador de opciones dataframe
    Public conta_opcion_Demarca As Integer = 0

    'nuevo

    Public v_dato1 As Double
    Public v_dato2 As Double
    Public v_dato3 As Double
    Public v_dato4 As Double
    Public v_dato5 As Double
    Public v_dato6 As Double
    Public v_radio As Integer
    Public sele_denu As Boolean
    Public v_nombre As String
    Public v_carta_dm As String
    Public v_titular_dm As String
    Public v_vigcat As String

    Public v_existe As Boolean
    Public v_dato1_mod As Double
    Public v_dato2_mod As Double
    Public v_dato3_mod As Double
    Public v_dato4_mod As Double
    Public valida_selecartas As Boolean = False
    Public canti_cartas As Long = 0
    Public colecciones_cd_dist As New Collection
    Public colecciones_cd_prov As New Collection
    Public colecciones_cd_depa As New Collection

    Public pBGP As IBasicGeoprocessor
    Public v_adispo As Long
    Public v_asuper As Long
    Public v_cantiprioritarios As Long
    Public fecha_archi As String = ""
    Public fecha_archi_sup As String = ""
    Public fecha_archi1 As String = ""
    Public conta_hoja_sup As Long = 0
    Public conta_reg As Long

    Public posi_y_t As Double
    Public posi_y1_t As Double
    Public pTextElement3 As ITextElement
    Public pTextElement2 As ITextElement
    Public pElement1 As IElement
    Public pElement2 As IElement
    Public pPoint3 As IPoint
    Public pEnv As IEnvelope
    Public pTextElement1 As ITextElement
    Public posi_y As Double
    Public posi_y1 As Double
    Public ptcol As IPointCollection
    Public conta_vert As Long
    Public j_vert As Long
    Public pPoint1 As IPoint
    Public pPoint2 As IPoint
    Public posi_y_m As Double
    Public posi_y1_m As Double
    Public este2 As Double
    Public norte2 As Double
    Public pElement3 As IElement
    Public s_tipo_plano As String
    Public corta_nplano As String
    Public conta_hoja_s As String

    Public v_existe_sup As Boolean = False
    Public colecciones_planos As New Collection
    Public pTxtSym3 As IFormattedTextSymbol
    Public pTxtSym2 As IFormattedTextSymbol
    Public pTxtSym1 As IFormattedTextSymbol
    Public pTxtSym4 As IFormattedTextSymbol
    Public pPageLayout As IPageLayout
    Public areaf As Double
    Public v_pasohoja As Boolean = True
    Public colecciones_AreaSup As New Collection
    Public fecha_archi2 As String = ""

    Public v_area_eval As Double = 0
    Public pfeature_eval As IFeature
    Public v_area_dispo As Double

    Public posi_y1_list As Double
    Public posi_y2_list As Double

    'Public form_cartaign As New Frm_observa_CartaIGN
    'observaciones carta

    Public v_checkbox1 As String = ""
    Public v_checkbox2 As String = ""
    Public v_checkbox3 As String = ""
    Public v_checkbox4 As String = ""
    Public v_checkbox5 As String = ""
    Public v_checkbox6 As String = ""
    Public v_checkbox7 As String = ""
    Public v_checkbox8 As String = ""
    Public v_checkbox9 As String = ""
    Public v_checkbox10 As String = ""
    Public v_checkbox11 As String = ""
    Public v_checkbox12 As String = ""
    Public v_checkbox13 As String = ""
    Public v_checkbox14 As String = ""
    Public v_checkbox15 As String = ""
    Public v_checkbox16 As String = ""
    Public v_checkbox17 As String = ""
    Public v_checkbox18 As String = ""
    Public v_checkbox19 As String = ""
    Public v_checkbox20 As String = ""
    Public v_txt_rio As String = ""
    Public v_txt_laguna As String = ""



    Public colecciones_obs As New Collection
    Public lista_cd_cartas As String
    Public caso_opcion_tools As String
    Public loStrShapefile1 As String
    Public loStrShapefile2 As String
    Public loStrShapefile_ld As String

    Public v_hectagis_x As Double
    Public valor_codhoja As String
    Public valor_hoja As String
    Public valor_nmhoja As String
    Public valor_zoncat As String

    Public v_posi_pr As Boolean
    Public v_posi_po As Boolean
    Public v_posi_si As Boolean
    Public v_posi_ex As Boolean
    Public v_posi_rd As Boolean

    Public colecciones_rd As New Collection
    Public lista_rd As String
    Public pQFilter As IQueryFilter
    Public Cuenta_rd As Integer
    Public conta_rd_posi_y As Double
    Public conta_rd_posi_y1 As Double
    Public contador_hojas As Long = 0
    Public consulta_dms As String

    Public fecha_tabla As String = ""
    Public listado_dm_eli As String = ""
    Public pFeatureLayer_cat As IFeatureLayer
    Public pFeatureLayer_dist As IFeatureLayer
    Public pFeatureLayer_prov As IFeatureLayer
    Public pFeatureLayer_depa As IFeatureLayer
    Public pFeatureLayer_tras As IFeatureLayer
    Public pFeatureLayer_rese As IFeatureLayer
    Public pFeatureLayer_urba As IFeatureLayer
    Public pFeatureLayer_fron As IFeatureLayer
    Public pFeatureLayer_hoja As IFeatureLayer
    Public pFeatureLayer_reseg As IFeatureLayer
    Public pFeatureLayer_capdist As IFeatureLayer
    Public pFeatureLayer_certi As IFeatureLayer
    Public pFeatureLayer_usomin As IFeatureLayer
    Public pFeatureLayer_Actmin As IFeatureLayer

    'caso actualizar  reserva

    Public v_tipoproceso As String = ""
    Public pWorkspaceEdit As IWorkspaceEdit
    Public pInFeatureClass As IFeatureClass
    Public tipo_areas As String = ""
    Public tipo_catanominero As String = ""
    Public pFeatureLayeras_rese As IFeatureLayer
    Public ruta_shputm As String = "U:\DATOS\SHAPE\Reservas\Historico\"
    Public ruta_shp As String = "c:\gis\SHAPE\"
    Public pOutFeatureClass As IFeatureClass

    'PARA FORMATOS

    Public Prioridad_dm As String
    Public query_cadena As String
    Public conta_q As Integer
    Public v_priori_dm As String
    Public v_codigo_dm As String
    Public colecciones_indi As New Collection

    Public pFeatureLayer_rio As IFeatureLayer
    Public pFeatureLayer_carre As IFeatureLayer
    Public pFeatureLayer_ccpp As IFeatureLayer
    Public validar_rio As Boolean = False
    Public validar_car As Boolean = False

    Public pastrDatabase1 As String = "BDGEOCAT"
    Public validad_rio As Boolean = False
    Public validad_carr As Boolean = False
    Public validad_paises As String = ""
    Public validad_front As Boolean = False
    Public seleccion_capa As String
    Public contador_inicio As Integer = 0
    Public p_Filtro1 As String


    Public var_fa_validaeval As Boolean = False
    Public var_fa_AreaSuper As Boolean = False
    Public var_fa_Coord_SuperAreaReserva As Boolean = False
    Public var_Fa_Zonaurbana As Boolean = False
    Public var_Fa_AreaReserva As Boolean = False
    Public var_Fa_nuevofa As Boolean = False
    Public var_Fa_guardarfa As Boolean = False

    Public var_fa_tipoactualiza As Boolean = False

    Public var_fa_actareasup1 As Boolean = False
    Public var_fa_actareasup2 As Boolean = False

    'Para plantillas de planos
    '*******************************

    Public Filtro_Ubigeo_dist As String
    Public Filtro_Ubigeo_prov As String
    Public Filtro_Ubigeo_depa As String
    Public existe_area As Boolean = False

    Public fecha_archi3 As String = ""
    Public v_explo_v_expta1 As Integer
    Public v_explo1 As Integer
    Public v_expta1 As Integer
    Public v_cierre1 As Integer

    Public sele_plano2 As String
    Public v_usuario As String
    Public v_zonasel As String
    Public sele_plano1 As String
    Public tipo_opcion As String
    Public tipo_plano As String
    Public tipo_sel_escala As String
    Public cod_opcion_Rese As String = ""
    Public canti_capa_certi As Integer = 1
    Public canti_capa_usomin As Integer = 0
    Public canti_capa_actmin As Integer = 0

    Public Accion_proceso As Boolean
    Public Consulta_Areas_rese As String = ""

    Public colecciones_supAR As New Collection
    Public nombre_datos As String = ""

    'para graficar desde excel
    Public v_Zona As String
    Public glo_codigou As String = ""
    Public g_Zona As String = ""
    Public lo_glo_xMin, lo_glo_yMin, lo_glo_xMax, lo_glo_yMax As Double
    Public lo_glo_x As Decimal = 0
    Public lo_glo_y As Decimal = 0

    'para formatos
    Public cuentaareasrese As String = ""
    Public pRings(40) As IRing
    Public pPolygon As IPolygon
    Public SubPolygono As Integer

    Public colecciones_codurba As New Collection

    'PARA CARTA ING DEL GEODATABASE
    ' Public rasterCatalog As IRasterCatalog
    Public pFSelQuery As IFeatureSelection
    Public pPropset As ESRI.ArcGIS.esriSystem.IPropertySet
    Public PrasterWorkspaceEx As IRasterWorkspaceEx

    Public pRasterCatalog As IRasterCatalog
    'Public pRasterCatalog As IRasterCatalogName
    Public rastercatalogLayer As ESRI.ArcGIS.Carto.IGdbRasterCatalogLayer

    Public sele_opcion_cuadri As Boolean
    Public sele_cuadri As String = ""

    Public v_calculAreaint As Boolean

    Public pQueryFilter_pol As IQueryFilter

    'Para libredenu
    Public lista_codigo As String


    Public fecha_dm_ex As String
    Public lostr_Join_Codigos_AREA As String = ""

    '************************************************************
    'Descripción: Rellena una cadena con un Comodin a la izquierda
    'Fecha: 28/03/2006
    '************************************************************
    Public Function RellenarComodin(ByVal pastrCadena As String, ByVal paintLongitud As Integer, _
                                    ByVal pastrComodin As String) As String
        Dim lostrCadena As String = Trim(pastrCadena)
        Dim lointLongitud As Integer = lostrCadena.Length
        For i As Integer = lointLongitud To paintLongitud - 1
            lostrCadena = pastrComodin & lostrCadena
        Next
        Return lostrCadena
    End Function
    Public Function Texto_Alta_Baja(ByVal pastrCadena As String)
        Dim lo_Union As String = ""
        Dim loCadena As String = Mid(pastrCadena, 1, InStr(pastrCadena, " ") - 1)
        Dim loCadena_1 As String = Mid(pastrCadena, InStr(pastrCadena, " ") + 1)
        Dim lo_Find As Integer = InStr(pastrCadena, " ") + 1
        Do Until loCadena Is Nothing
            lo_Union = lo_Union + Mid(loCadena, 1, 1).ToUpper & Mid(loCadena, 2).ToLower & " "
            Try
                loCadena = Mid(loCadena_1, 1, InStr(loCadena_1, " ") - 1)
            Catch ex As Exception
                loCadena = Nothing
            End Try
            loCadena_1 = Mid(loCadena_1, InStr(loCadena_1, " ") + 1)
        Loop
        lo_Union = lo_Union + Mid(loCadena_1, 1, 1).ToUpper & Mid(loCadena_1, 2).ToLower
        Return lo_Union
    End Function
End Module
