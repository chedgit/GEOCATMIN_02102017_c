Option Explicit On
Imports Oracle.DataAccess.Client
Imports Microsoft.VisualBasic


Public Class cls_Oracle

    Public Function F_Item_Data_DM(ByVal pastrCodigo As String) As DataTable
        Dim lstrProcedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"v_codigo", OracleDbType.Varchar2, 10, pastrCodigo}}
        lstrProcedimiento = "DATA_GIS.PACK_DBA_GIS.P_LISTA_REG_CATA_MINERO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_CONTADOR_IT(ByVal pastrTabla As String, ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = { _
                            {"V_TABLA", OracleDbType.Varchar2, 20, pastrTabla}, _
                            {"V_CODIGO", OracleDbType.Varchar2, 15, pastrCodigo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_CONTADOR_IT"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Pto_Region(ByVal v_dato1 As String, ByVal v_dato2 As String, ByVal v_zona_dm As String) As String
        Dim lstrProcedimiento As String = ""
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"v_ESTE", OracleDbType.Varchar2, 13, v_dato1}, _
                                       {"v_NORTE", OracleDbType.Varchar2, 13, v_dato2}, _
                                       {"v_opcion_1", OracleDbType.Varchar2, 2, v_zona_dm}}
        lstrProcedimiento = "DATA_GIS.PACK_DBA_GIS.P_OBTENER_DATA1"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUTGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_VERIFICA_ESTADO_IT(ByVal pastrTabla As String, ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = { _
                            {"V_TABLA", OracleDbType.Varchar2, 20, pastrTabla}, _
                            {"V_CODIGO", OracleDbType.Varchar2, 15, pastrCodigo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_EG_FORMAT_IT"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_SETEAR_PARAMETRO_ENTRADA(ByVal pastrCodigo As String, ByVal pastrTipoInforme As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}, _
                                       {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrTipoInforme}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_ELIMINAR_INFORME_DM"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    'Public Function F_SEL_LISTA_DM_HISTORICO(ByVal pastrCodigo As String, ByVal pastrFecha As String) As DataTable
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    Dim Parametros(,) As Object = {{"CODIGO", OracleDbType.Varchar2, 25, pastrCodigo}, _
    '                                   {"FECHA", OracleDbType.Varchar2, 8, pastrFecha}}
    '    Dim lstrProcedimiento As String = "SISGEM.PACK_DBA_SG_D_EVALGIS.P_SEL_DM_HISTORICO"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try
    'End Function

    Public Function F_SEL_LISTA_DM_HISTORICO(ByVal pastrCodigo As String, ByVal pastrNombre As String, ByVal pastrFecha As String, ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 12, pastrCodigo}, _
                                       {"V_NOMBRE", OracleDbType.Varchar2, 25, pastrNombre}, _
                                       {"V_FECHA", OracleDbType.Varchar2, 25, pastrFecha}, _
                                       {"V_TIPO", OracleDbType.Varchar2, 8, pastrTipo}}
        Dim lstrProcedimiento As String = "SISGEM.PACK_DBA_SG_D_EVALGIS.P_SEL_DM_HISTORICO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_SEL_FECHA_DM_HISTORICO(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 25, pastrCodigo}}
        Dim lstrProcedimiento As String = "SISGEM.PACK_DBA_SG_D_EVALGIS.P_FECHA_DM_HISTORICO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_ELIMINA_INFORME_TECNICO(ByVal pastrCodigo As String, ByVal pastrTipoInforme As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}, _
                                       {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrTipoInforme}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_ELIMINAR_INFORME_DM"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function FT_SEL_LISTA_DESCRIPCION(ByVal pastrSeleccion As String, ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = { _
                            {"V_SELECCION", OracleDbType.Varchar2, 1, pastrSeleccion}, _
                            {"V_TIPO", OracleDbType.Varchar2, 32, pastrTipo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DESCRIPCION"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_SEL_LISTA_DM_COORDENADA(ByVal pastrCodigo As String, ByVal pastrFecha As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"CODIGO", OracleDbType.Varchar2, 25, pastrCodigo}, _
                                       {"FECHA", OracleDbType.Varchar2, 8, pastrFecha}}
        Dim lstrProcedimiento As String = "SISGEM.PACK_DBA_SG_D_EVALGIS.P_SEL_DM_COORDENADA"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_GENERA_HISTORICO(ByVal pastrCodigo As String, ByVal pastrTipoInforme As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}, _
                                       {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrTipoInforme}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_GENERAR_HISTORICO"


        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function FT_GENERA_HISTORICO_LIBDEN(ByVal pastrCodigo As String, ByVal pastrTipoInforme As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}, _
                                       {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrTipoInforme}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_GENERAR_HISTORICO_LIBDEN"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_VERIFICA_CODIGO(ByVal pastrCodigo As String, ByVal pastrTipoInforme As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}, _
                                       {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrTipoInforme}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_VERIFICA_CODIGO"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    'Public Function F_Obtiene_Lista_Layer(ByVal pastrTipo As String) As DataTable
    '    'strConexion = "User Id=" & "BDTECNICA" & ";Password=" & "BDTECNICA" & ";Data Source=" & "BDGEOREF" & ";Connection Timeout=60;"
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 32, pastrTipo}}
    '    Dim lstrProcedimiento As String = "PKG_SISTEMA_CONSULTAS.P_SEL_PARAMETROS_GML"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try
    'End Function

   

    Public Function F_Obtiene_Role_Usuario(ByVal pastrTipo As String, ByVal pastrRol As String, ByVal pstrConexionGis As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TIPO", OracleDbType.Varchar2, 1, pastrTipo}, _
                                       {"V_ROL", OracleDbType.Varchar2, 30, pastrRol}}
        Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_ROLES_USERS"
        Try
            strConexionGIS = pstrConexionGis
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function F_Obtiene_Menu_x_Opcion_1(ByVal pastrOpcion As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_OPCION_MENU", OracleDbType.Varchar2, 6, pastrOpcion}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_BOTON_X_MENU_1"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function FT_Int_Total(ByVal pTipo As String, ByVal pCoordenada As String, ByVal pzona As String, _
    ByVal player As String, ByVal pCodigoRes As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"vTipo", OracleDbType.Varchar2, 1, pTipo}, _
                                       {"vCoordenada", OracleDbType.Varchar2, 4000, pCoordenada}, _
                                       {"vzona", OracleDbType.Varchar2, 2, pzona}, _
                                       {"vlayer", OracleDbType.Varchar2, 50, player}, _
                                       {"v_codreserva", OracleDbType.Varchar2, 50, pCodigoRes}}
        Dim lstrProcedimiento As String = "PACK_DBA_GIS.p_Verifica_IntTotal"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function P_Int_Total(ByVal pTipo As String, ByVal pCoordenada As String, ByVal pzona As String, ByVal player As String, _
    ByVal pCodigoRes As String, ByVal pclase As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"vTipo", OracleDbType.Varchar2, 1, pTipo}, _
                                       {"vCoordenada", OracleDbType.Varchar2, 4000, pCoordenada}, _
                                       {"vzona", OracleDbType.Varchar2, 2, pzona}, _
                                       {"vlayer", OracleDbType.Varchar2, 50, player}, _
                                       {"v_codreserva", OracleDbType.Varchar2, 20, pCodigoRes}, _
                                       {"v_clase", OracleDbType.Varchar2, 20, pclase}}
        Dim lstrProcedimiento As String = "PACK_DBA_GIS.p_ejecuta_Int_Total"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function P_EliminaRegistros(ByVal pcodigo As String, ByVal player As String, ByVal ptipo As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"v_codigo", OracleDbType.Varchar2, 20, pcodigo}, _
                                       {"vlayer", OracleDbType.Varchar2, 50, player}, _
                                       {"v_tipo", OracleDbType.Varchar2, 2, ptipo}}

        Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_eliminar_Featureclass"
        Try

            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing



        Catch
            Throw
        End Try
    End Function

    Public Function FT_Int_Parcial(ByVal pTipo As String, ByVal pCoordenada As String, ByVal pzona As String, _
    ByVal player As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"vTipo", OracleDbType.Varchar2, 1, pTipo}, _
                                      {"vCoordenada", OracleDbType.Varchar2, 4000, pCoordenada}, _
                                      {"vzona", OracleDbType.Varchar2, 2, pzona}, _
                                      {"vlayer", OracleDbType.Varchar2, 50, player}}
        Dim lstrProcedimiento As String = "PACK_DBA_GIS.p_Verifica_IntParcial"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function P_Int_Parcial(ByVal pTipo As String, ByVal pCoordenada As String, ByVal pzona As String, ByVal player As String, ByVal pcodigo As String, _
    ByVal pclase As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"vTipo", OracleDbType.Varchar2, 1, pTipo}, _
                                      {"vCoordenada", OracleDbType.Varchar2, 8000, pCoordenada}, _
                                      {"vzona", OracleDbType.Varchar2, 2, pzona}, _
                                      {"vlayer", OracleDbType.Varchar2, 50, player}, _
                                      {"v_codReserva", OracleDbType.Varchar2, 20, pcodigo}, _
                                      {"v_clase", OracleDbType.Varchar2, 50, pclase}}
        Dim lstrProcedimiento As String = "PACK_DBA_GIS.p_ejecuta_Int_Parcial"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_SG_D_Libden_EVALGIS(ByVal pastrAccion As String, ByVal pastrCodigo As String, _
     ByVal pastrEGFormat As String, ByVal pastrUsuario As String, ByVal feclibden As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_ACCION", OracleDbType.Varchar2, 13, pastrAccion}, _
                                       {"V_CGCODIGO", OracleDbType.Varchar2, 13, pastrCodigo}, _
                                       {"V_EGFORMAT", OracleDbType.Varchar2, 2, pastrEGFormat}, _
                                       {"V_USLOGUSE", OracleDbType.Varchar2, 32, pastrUsuario}, _
                                       {"V_FECHA", OracleDbType.Varchar2, 32, feclibden}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_DATOS_LIBDENU_EVALGIS"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FT_Fec_Libden_EVALGIS(ByVal pastrEGFormat As String, ByVal feclibden As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos

        Dim Parametros(,) As Object = {{"V_EGFORMAT", OracleDbType.Varchar2, 2, pastrEGFormat}, _
                                      {"V_FECHA", OracleDbType.Varchar2, 32, feclibden}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_VERIFICA_FEC_LIBDEN"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing


        Catch
            Throw
        End Try
    End Function

    Public Function FT_dmsim(ByVal fecsimul As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos
        Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}}
        Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_ejecuta_sc_d_pesicu"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_dmsim_1(ByVal fecsimul As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos
        Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}}
        'Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_ejecuta_sc_d_pesicu"
        Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_ejecuta_sc_d_pesicu_1"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_dmsim_2(ByVal fecsimul As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos
        Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}}
        'Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_ejecuta_sc_d_pesicu"
        Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_ejecuta_sc_d_pesicu_2"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_cuad_simul_ld(ByVal fecsimul As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos
        Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}}
        'Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_ejecuta_sc_d_pesicu"
        'Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_ejecuta_sc_d_pesicu_1"
        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_SEL_CUAD_SIMUL_LD"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_grupo_dmsimul_ld(ByVal fecsimul As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos
        Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}}
        'Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_ejecuta_sc_d_pesicu"
        'Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_ejecuta_sc_d_pesicu_2"
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_GRUPO_DM_SIMUL_LD"
        Try
            'Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_dmsimzo(ByVal fecsimul As String, ByVal v_zona_dm As String, ByVal tipoex As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}, _
                                       {"V_ZONA", OracleDbType.Varchar2, 2, v_zona_dm}, _
                                      {"V_TIPOEX", OracleDbType.Varchar2, 2, tipoex}}

        'Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}}
        'Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_consulta_sc_d_pesimul"
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DM_SIMUL_LD"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_cuadsimzo(ByVal fecsimul As String, ByVal v_zona_dm As String, ByVal tipoex As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos
        Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}, _
                                       {"V_ZONA", OracleDbType.Varchar2, 2, v_zona_dm}, _
                                      {"V_TIPOEX", OracleDbType.Varchar2, 2, tipoex}}

        'Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}}
        'Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_consulta_sc_d_pesimul"
        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_SEL_CUAD_SIMULXZONA_LD"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    'Public Function FT_fecsimul(ByVal fecsimul As String) As DataTable
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos
    '    Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}}
    '    Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_sc_d_pesimul"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try
    'End Function

    Public Function FT_fecsimul(ByVal fecsimul As String, ByVal v_zona_dm As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos
        Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}, _
                                      {"V_ZONA", OracleDbType.Varchar2, 2, v_zona_dm}}

        'Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}}
        Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_sc_d_pesimul"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_FCNUM(ByVal fecsimul As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}}
        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_verifica_fcnum"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUTGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_verifica_bdsimultaneos(ByVal fecsimul As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}}
        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_verifica_bdsimultaneos"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUTGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_ELIMINA_DM_GDB(ByVal fecsimul As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}}
        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_DEL_DM_SIM_GDB_LD"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUTGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_ELIMINA_DM_ACCEDITARIO(ByVal v_tipo As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"v_tipo", OracleDbType.Varchar2, 3, v_tipo}}
        'Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_DEL_DM_ACCEDITARIO"
        Dim lstrProcedimiento As String = "DATA_GIS.PROC_ELIMINA_ACCEDITARIO"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUTGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    'Public Function FT_petpma() As DataTable
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    'Tabla de petitorios PMA vigentes
    '    Dim Parametros(,) As Object = {}
    '    Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_consulta_sc_d_petpma"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try
    'End Function

    Public Function FT_petpma(ByVal datum As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        'Tabla de petitorios PMA vigentes
        Dim Parametros(,) As Object = {{"datum_i", OracleDbType.Varchar2, 8, datum}}
        Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_consulta_sc_d_petpma"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_fecsup(ByVal fecsup As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos

        Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsup}}
        Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_Consulta_sc_d_regdiariope"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    'Public Function FT_grusim(ByVal grusim As String) As DataTable
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos

    '    Dim Parametros(,) As Object = {{"V_GRUPO", OracleDbType.Varchar2, 32, grusim}}
    '    Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_Consulta_sc_d_pesig"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try
    'End Function

    'Public Function FT_grusim(ByVal grusim As String, ByVal tipo As String) As DataTable
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos

    '    Dim Parametros(,) As Object = {{"V_GRUPO", OracleDbType.Varchar2, 32, grusim}, _
    '                                  {"V_TIPO", OracleDbType.Varchar2, 2, tipo}}
    '    Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_Consulta_sc_d_pesig"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try
    'End Function
    Public Function FT_grusim(ByVal grusim As String, ByVal tipo As String, ByVal datum_origen As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos

        Dim Parametros(,) As Object = {{"V_GRUPO", OracleDbType.Varchar2, 32, grusim}, _
                                      {"V_TIPO", OracleDbType.Varchar2, 2, tipo}, _
                                      {"datum_i", OracleDbType.Varchar2, 8, datum_origen}}
        'Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_Consulta_sc_d_pesig"
        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_SEL_DM_SIMUL_EV"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_pesiev(ByVal loCodigosim As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos

        Dim Parametros(,) As Object = {{"v_codigou", OracleDbType.Varchar2, 100, loCodigosim}}
        'Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_pesiev"
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_INS_DM_SIMUL_EV"
        Try
            'Return objDatos.FPT_ExecSPReturnValueOUTPUTGIS(lstrProcedimiento, Parametros)
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_sc_d_cupesiev(ByVal datum_origen As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos

        Dim Parametros(,) As Object = {{"datum_i", OracleDbType.Varchar2, 8, datum_origen}}
        'Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_SEL_CUAD_SIMUL_EV"
        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_SEL_CUAD_SIMUL_EV"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    'Public Function FT_sc_d_pesiev() As DataTable
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos

    '    Dim Parametros(,) As Object = {}
    '    Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_ejecuta_sc_d_pesiev"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try
    'End Function

    Public Function FT_sc_d_pesiev(ByVal datum_origen As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos

        Dim Parametros(,) As Object = {{"datum_i", OracleDbType.Varchar2, 8, datum_origen}}
        'Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_ejecuta_sc_d_pesiev"
        'Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_TOTAL_DM_SIMUL_EV"
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_GRUPO_DM_SIMUL_EV"
        Try
            'Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    'Public Function FT_consulta_sc_d_pesiev() As DataTable
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos

    '    Dim Parametros(,) As Object = {}
    '    Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_consulta_sc_d_pesiev"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try
    'End Function

    Public Function FT_consulta_sc_d_pesiev(ByVal codigo_eval As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos

        Dim Parametros(,) As Object = {{"v_codigo", OracleDbType.Varchar2, 20, codigo_eval}}
        Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_consulta_sc_d_pesiev"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    'Public Function FT_cuasim(ByVal grusimf As String) As DataTable
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos

    '    Dim Parametros(,) As Object = {{"V_GRUPO", OracleDbType.Varchar2, 32, grusimf}}
    '    Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_Consulta_sc_d_pesicg"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try
    'End Function

    Public Function FT_cuasim(ByVal grusimf As String, ByVal tipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos

        Dim Parametros(,) As Object = {{"V_GRUPO", OracleDbType.Varchar2, 32, grusimf}, _
                                      {"V_TIPO", OracleDbType.Varchar2, 2, tipo}}

        'Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}}
        'Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_consulta_sc_d_pesicg"
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_GRUPO_CUAD_SIMUL_EV"
        Try
            'Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_coorsim(ByVal codigo_eval As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos

        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 20, codigo_eval}}
        'Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_coor_sc_d_pesi"
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_COORD_CUAD_SIMUL_EV"
        Try
            'Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_hojasim(ByVal codigo_eval As String, ByVal tipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos

        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 20, codigo_eval}, _
                                      {"V_TIPO", OracleDbType.Varchar2, 2, tipo}}
        'Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_Consulta_sc_d_hojapesi"
        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_SEL_HOJA_DM_SIMUL_EV"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    'Public Function FT_petsimul(ByVal fecsup As String) As DataTable
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos

    '    Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsup}}
    '    Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_Consulta_sc_d_pesicu"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try
    'End Function

    'Public Function FT_petsimul() As DataTable
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos

    '    Dim Parametros(,) As Object = {}
    '    Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_ejecuta_sc_d_pesicug"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try
    'End Function

    Public Function FT_gruposimul(ByVal fecsimul As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos

        Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}}
        'Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_ejecuta_sc_d_pesicug"
        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_UPD_GRUPO_DM_SIMUL_LD"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_petsimul(ByVal fecsimul As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        'Para encontrar cual es la fecha de libredenunciabilidad guardada en la base de datos

        Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}}
        'Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_ejecuta_sc_d_pesicug"
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_REPO_GRUPO_DM_SIMUL_LD"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_pesird() As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {}

        'Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_ejecuta_sc_d_pesird"
        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_SEL_RD_SIMUL_LD"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_DMXGRSIMUL(ByVal fecsimul As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos

        Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 32, fecsimul}}
        'Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_SEL_DMXGRSIMUL_LD"
        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_SEL_DMXGRSIMUL_LD"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FT_SG_D_EVALGIS(ByVal pastrAccion As String, ByVal pastrCodigo As String, _
     ByVal pastrEGFormat As String, ByVal pastrUsuario As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_ACCION", OracleDbType.Varchar2, 13, pastrAccion}, _
                                       {"V_CGCODIGO", OracleDbType.Varchar2, 13, pastrCodigo}, _
                                       {"V_EGFORMAT", OracleDbType.Varchar2, 2, pastrEGFormat}, _
                                       {"V_USLOGUSE", OracleDbType.Varchar2, 32, pastrUsuario}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SG_D_EVALGIS"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_distancia(ByVal v_codigo As String, ByVal v_zona_dm As String, ByVal v_sistema As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos

        Dim Parametros(,) As Object = {{"v_codigo", OracleDbType.Varchar2, 32, v_codigo}, _
                                      {"v_zona", OracleDbType.Varchar2, 2, v_zona_dm}, _
                                      {"datum_i", OracleDbType.Varchar2, 8, v_sistema}}

        Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_CAL_DIST_DM_FRONTERA"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_Int_Catastro_Historico(ByVal v_codigo As String, ByVal v_fecha As String, ByVal v_zona As String, ByVal v_tipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos

        Dim Parametros(,) As Object = {{"v_codigo", OracleDbType.Varchar2, 32, v_codigo}, _
                                      {"v_fecha", OracleDbType.Varchar2, 10, v_fecha}, _
                                      {"v_zona", OracleDbType.Varchar2, 2, v_zona}, _
                                      {"v_tipo", OracleDbType.Varchar2, 2, v_tipo}}

        Dim lstrProcedimiento As String = "DATA_CAT.PACK_DBA_SIGCATMIN.P_SEL_INT_CATASTRO_HISTORICO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_Intersecta_Fclass_Oracle_1(ByVal pTipo As String, ByVal pLayer1 As String, _
    ByVal pLayer2 As String, ByVal pCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"v_Tipo", OracleDbType.Varchar2, 2, pTipo}, _
                                       {"v_layer_1", OracleDbType.Varchar2, 50, pLayer1}, _
                                       {"v_layer_2", OracleDbType.Varchar2, 50, pLayer2}, _
                                       {"v_codigo", OracleDbType.Varchar2, 20, pCodigo}}
        Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_Int_Catastro"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try


    End Function
    Public Function FT_Intersecta_Fclass_Oracle_capas(ByVal pTipo As String, ByVal pLayer1 As String, _
  ByVal pLayer2 As String, ByVal pCodigo As String, ByVal psistema As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"v_zona", OracleDbType.Varchar2, 2, pTipo}, _
                                       {"v_layer_1", OracleDbType.Varchar2, 50, pLayer1}, _
                                       {"v_layer_2", OracleDbType.Varchar2, 50, pLayer2}, _
                                       {"v_archi", OracleDbType.Varchar2, 20, pCodigo}, _
                                       {"v_sistema", OracleDbType.Varchar2, 20, psistema}}
        Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_INT_capas"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try


    End Function


    Public Function FT_SG_D_CARAC_EVALGIS(ByVal pastrAccion As String, ByVal pastr_CGCODIGO As String, _
        ByVal pastrEGFORMAT As String, ByVal pastrCG_CODEVA As String, ByVal pastrIE_CODIGO As String, _
        ByVal pastrEG_DESCRI As String, ByVal pastrEG_VALOR As String, _
        ByVal pastrEG_TIPO As String, ByVal pastrUsuario As String) As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_ACCION", OracleDbType.Varchar2, 3, pastrAccion}, _
                                       {"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
                                       {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrEGFORMAT}, _
                                       {"V_CG_CODEVA", OracleDbType.Clob, 4000000, pastrCG_CODEVA}, _
                                       {"V_IE_CODIGO", OracleDbType.Clob, 4000000, pastrIE_CODIGO}, _
                                       {"V_EG_DESCRI", OracleDbType.Clob, 4000000, pastrEG_DESCRI}, _
                                       {"V_EG_VALOR", OracleDbType.Clob, 4000000, pastrEG_VALOR}, _
                                       {"V_EG_TIPO", OracleDbType.Clob, 4000000, pastrEG_TIPO}, _
                                       {"V_USLOGUSE", OracleDbType.Varchar2, 32, pastrUsuario}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SG_D_CARAC_EVALGIS"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function FT_Guarda_AreaNeta_Padron(ByVal pastr_CGCODIGO As String, _
        ByVal pastrEG_VALOR As String, ByVal pastrUsuario As String, ByVal pastrEG_TIPO As String) As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
                                       {"V_EG_VALOR", OracleDbType.Varchar2, 32, pastrEG_VALOR}, _
                                        {"V_USLOGUSE", OracleDbType.Varchar2, 32, pastrUsuario}, _
                                       {"V_EG_TIPO", OracleDbType.Varchar2, 32, pastrEG_TIPO}}

        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_AREASNETAS"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FT_SG_D_AREAS_EVALGIS(ByVal pastrAccion As String, ByVal pastr_CGCODIGO As String, _
ByVal pastrEG_FORMAT As String, ByVal pastrCG_CODEVA As String, ByVal pastrAG_AREA As String, _
ByVal pastrIE_CODIGO As String, ByVal pastrAG_HECTAR As String, ByVal pastrTipo As String, ByVal pastrUsuario As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos

        Dim Parametros(,) As Object = {{"V_ACCION", OracleDbType.Varchar2, 3, pastrAccion}, _
                                     {"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
                                     {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrEG_FORMAT}, _
                                     {"V_CG_CODEVA", OracleDbType.Varchar2, 13, pastrCG_CODEVA}, _
                                     {"V_AG_AREA", OracleDbType.Varchar2, 13, pastrAG_AREA}, _
                                     {"V_IE_CODIGO", OracleDbType.Varchar2, 13, pastrIE_CODIGO}, _
                                     {"V_AG_HECTAR", OracleDbType.Varchar2, 20, pastrAG_HECTAR}, _
                                     {"V_EG_TIPO", OracleDbType.Varchar2, 13, pastrTipo}, _
                                     {"V_USLOGUSE", OracleDbType.Varchar2, 32, pastrUsuario}}



        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SG_D_AREAS_EVALGIS1"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try



    End Function







    Public Function FT_SG_H_AREAS_EVALGIS(ByVal pastrAccion As String, ByVal pastr_CGCODIGO As String, _
ByVal pastrEG_FORMAT As String, ByVal pastrCG_CODEVA As String, ByVal pastrHG_SECUEN As String, ByVal pastrIE_CODIGO As String, _
ByVal pastrEG_DESCRI As String, ByVal pastrEG_VALOR As Double, ByVal pastrTIPO As String, ByVal pastrUsuario As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_ACCION", OracleDbType.Varchar2, 3, pastrAccion}, _
                                       {"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
                                       {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrEG_FORMAT}, _
                                       {"V_CG_CODEVA", OracleDbType.Clob, 4000000, pastrCG_CODEVA}, _
                                       {"V_HG_SECUEN", OracleDbType.Varchar2, 13, pastrHG_SECUEN}, _
                                       {"V_IE_CODIGO", OracleDbType.Varchar2, 3, pastrIE_CODIGO}, _
                                       {"V_EG_DESCRI", OracleDbType.Varchar2, 200, pastrEG_DESCRI}, _
                                       {"V_EG_VALOR", OracleDbType.Clob, 4000000, pastrEG_VALOR}, _
                                       {"V_TIPO", OracleDbType.Clob, 4000000, pastrTIPO}, _
                                       {"V_USLOGUSE", OracleDbType.Varchar2, 32, pastrUsuario}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SG_H_EVALGIS1"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function FT_SG_D_COORD_EVALGIS1(ByVal pastrAccion As String, ByVal pastr_CGCODIGO As String, _
    ByVal pastrEGFORMAT As String, ByVal pastrCG_CODEVA As String, ByVal pastrAG_AREA As String, ByVal V_vertice As String, _
    ByVal pastrIE_CODIGO As String, ByVal pastrCV_ESTE As String, ByVal pastrCV_NORTE As String, _
     ByVal pastrUsuario As String, ByVal pastrEG_VALOR As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_ACCION", OracleDbType.Varchar2, 3, pastrAccion}, _
                                       {"V_CGCODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
                                       {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrEGFORMAT}, _
                                       {"V_CG_CODEVA", OracleDbType.Varchar2, 15, pastrCG_CODEVA}, _
                                       {"V_AG_AREA", OracleDbType.Varchar2, 13, pastrAG_AREA}, _
                                       {"V_NU_ORDEN", OracleDbType.Varchar2, 10, V_vertice}, _
                                       {"V_IE_CODIGO", OracleDbType.Varchar2, 10, pastrIE_CODIGO}, _
                                       {"V_CV_ESTE", OracleDbType.Clob, 4000000, pastrCV_ESTE}, _
                                       {"V_CV_NORTE", OracleDbType.Clob, 4000000, pastrCV_NORTE}, _
                                       {"V_USLOGUSE", OracleDbType.Varchar2, 32, pastrUsuario}, _
                                      {"V_AG_HECTAR", OracleDbType.Clob, 4000000, pastrEG_VALOR}}

        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SG_D_COORD_EVALGIS_mod"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_SG_D_COORD_EVALGIS(ByVal pastrAccion As String, ByVal pastr_CGCODIGO As String, _
ByVal pastrEGFORMAT As String, ByVal pastrCG_CODEVA As String, ByVal pastrAG_AREA As String, _
ByVal pastr_NUMVER As String, ByVal pastrIE_CODIGO As String, ByVal pastrCV_ESTE As String, ByVal pastrCV_NORTE As String, _
ByVal pastrAG_HECTAR As String, ByVal pastrUsuario As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_ACCION", OracleDbType.Varchar2, 3, pastrAccion}, _
                                       {"V_CGCODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
                                       {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrEGFORMAT}, _
                                       {"V_CG_CODEVA", OracleDbType.Varchar2, 15, pastrCG_CODEVA}, _
                                       {"V_AG_AREA", OracleDbType.Varchar2, 10, pastrAG_AREA}, _
                                       {"V_CV_NUMVER", OracleDbType.Varchar2, 10, pastr_NUMVER}, _
                                       {"V_IE_CODIGO", OracleDbType.Varchar2, 10, pastrIE_CODIGO}, _
                                       {"V_CV_ESTE", OracleDbType.Clob, 4000000, pastrCV_ESTE}, _
                                       {"V_CV_NORTE", OracleDbType.Clob, 4000000, pastrCV_NORTE}, _
                                       {"V_USLOGUSE", OracleDbType.Varchar2, 32, pastrUsuario}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SG_D_COORD_EVALGIS"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_SIMULTANEO_EVALGIS(ByVal v_tabla As String, ByVal v_fecsim As String, ByVal v_grupo As String, ByVal v_subgrupo As String, ByVal v_numdm As String, _
                                          ByVal v_numcuasim As String, ByVal v_numver As String, ByVal v_este As String, ByVal v_norte As String, ByVal v_codigo As String, _
                                          ByVal v_zona_dm As String, ByVal v_hoja As String, ByVal v_dema As String) As String
        'Dim Resultado As String = String.Empty
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        'objDatos = New PORTAL_Clases.clsBD_ManejoDatos()
        Dim Parametros(,) As Object = {{"tabla", OracleDbType.Varchar2, 3, v_tabla}, _
                                       {"fecha_sim", OracleDbType.Varchar2, 30, v_fecsim}, _
                                       {"grupo", OracleDbType.Varchar2, 3, v_grupo}, _
                                       {"subgrupo", OracleDbType.Varchar2, 3, v_subgrupo}, _
                                       {"cantidad", OracleDbType.Varchar2, 3, v_numdm}, _
                                       {"area", OracleDbType.Varchar2, 20, v_numcuasim}, _
                                       {"vertice", OracleDbType.Varchar2, 3, v_numver}, _
                                       {"este", OracleDbType.Varchar2, 20, v_este}, _
                                       {"norte", OracleDbType.Varchar2, 20, v_norte}, _
                                       {"codigo", OracleDbType.Varchar2, 13, v_codigo}, _
                                       {"zona", OracleDbType.Varchar2, 3, v_zona_dm}, _
                                       {"hoja", OracleDbType.Varchar2, 10, v_hoja}, _
                                       {"dema", OracleDbType.Varchar2, 10, v_dema}}

        Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_SIMULTANEO_EVALGIS"
        Try
            'MsgBox(Parametros.ToString())

            'Resultado = objDatos.FPT_ExecSPReturnValueOUTPUTGIS(lstrProcedimiento, Parametros).ToString()
            Return objDatos.FPT_ExecSPReturnValueOUTPUTGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch 'ex As Exception
            Throw 'MsgBox(ex.Message + ". _ Stack: " + ex.InnerException.ToString())
        End Try
        'Return Resultado
    End Function

    Public Function FT_INS_TABLASIMUL(ByVal v_tabla As String, ByVal v_fecsim As String, ByVal v_grupo As String, ByVal v_subgrupo As String, ByVal v_numdm As String, _
                                          ByVal v_numcuasim As String, ByVal v_numver As String, ByVal v_este As String, ByVal v_norte As String, ByVal v_codigo As String, _
                                          ByVal v_zona As String, ByVal v_hoja As String, ByVal v_dema As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"tabla", OracleDbType.Varchar2, 3, v_tabla}, _
                                       {"fecha_sim", OracleDbType.Varchar2, 30, v_fecsim}, _
                                       {"grupo", OracleDbType.Varchar2, 3, v_grupo}, _
                                       {"subgrupo", OracleDbType.Varchar2, 3, v_subgrupo}, _
                                       {"cantidad", OracleDbType.Varchar2, 3, v_numdm}, _
                                       {"area", OracleDbType.Varchar2, 20, v_numcuasim}, _
                                       {"vertice", OracleDbType.Varchar2, 3, v_numver}, _
                                       {"este", OracleDbType.Varchar2, 20, v_este}, _
                                       {"norte", OracleDbType.Varchar2, 20, v_norte}, _
                                       {"codigo", OracleDbType.Varchar2, 13, v_codigo}, _
                                       {"zona", OracleDbType.Varchar2, 3, v_zona}, _
                                       {"hoja", OracleDbType.Varchar2, 10, v_hoja}, _
                                       {"dema", OracleDbType.Varchar2, 10, v_dema}}

        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_INS_TABLAS_SIMUL"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUTGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function P_INS_SIMULTANEOS(ByVal v_fecsim As String, ByVal v_grupo As String, ByVal v_subgrupo As String, ByVal v_numdm As String, ByVal v_numcuasim As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"fecha_sim", OracleDbType.Varchar2, 30, v_fecsim}, _
                                       {"grupo", OracleDbType.Varchar2, 3, v_grupo}, _
                                       {"subgrupo", OracleDbType.Varchar2, 3, v_subgrupo}, _
                                       {"cantidad", OracleDbType.Varchar2, 14, v_numdm}, _
                                       {"area", OracleDbType.Varchar2, 20, v_numcuasim}}

        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_INS_SIMULTANEOS"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUTGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_INS_SIMULCOOR(ByVal v_fecsim As String, ByVal v_grupo As String, ByVal v_subgrupo As String, ByVal v_numver As String, ByVal v_este As String, ByVal v_norte As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"fecha_sim", OracleDbType.Varchar2, 30, v_fecsim}, _
                                       {"grupo", OracleDbType.Varchar2, 3, v_grupo}, _
                                       {"subgrupo", OracleDbType.Varchar2, 3, v_subgrupo}, _
                                       {"vertice", OracleDbType.Varchar2, 40, v_numver}, _
                                       {"este", OracleDbType.Varchar2, 40, v_este}, _
                                       {"norte", OracleDbType.Varchar2, 40, v_norte}}
                      
        '{"este", OracleDbType.Varchar2, 40, v_este}, _
        '{"norte", OracleDbType.Varchar2, 40, v_norte}}
        '{"este", OracleDbType.Clob, 4000000, v_este}, 
        '{"norte", OracleDbType.Clob, 4000000, v_norte}}

        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_INS_SG_D_SIMULCOOR"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUTGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    'v_fecsim VARCHAR2,v_grupo VARCHAR2,v_subgrupo VARCHAR2,v_codigo VARCHAR2,v_zona VARCHAR2,retorno OUT VARCHAR2
    Public Function FT_INS_DMXGRSIMUL(ByVal v_fecsim As String, ByVal v_grupo As String, ByVal v_subgrupo As String, ByVal v_codigo As String, ByVal v_zona_dm As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"fecha_sim", OracleDbType.Varchar2, 30, v_fecsim}, _
                                       {"grupo", OracleDbType.Varchar2, 3, v_grupo}, _
                                       {"subgrupo", OracleDbType.Varchar2, 3, v_subgrupo}, _
                                       {"codigo", OracleDbType.Varchar2, 13, v_codigo}, _
                                       {"zona", OracleDbType.Varchar2, 3, v_zona_dm}}

        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_INS_SG_D_DMXGRSIMUL"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUTGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_INS_CARTAXDERESIMUL(ByVal v_fecsim As String, ByVal v_grupo As String, ByVal v_subgrupo As String, ByVal v_hoja As String, ByVal v_codigo As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"fecha_sim", OracleDbType.Varchar2, 30, v_fecsim}, _
                                       {"grupo", OracleDbType.Varchar2, 3, v_grupo}, _
                                       {"subgrupo", OracleDbType.Varchar2, 3, v_subgrupo}, _
                                       {"hoja", OracleDbType.Varchar2, 10, v_hoja}, _
                                       {"codigo", OracleDbType.Varchar2, 13, v_codigo}}

        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_INS_SG_D_CARTAXDERESIMUL"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUTGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_INS_DEMAXDERESIMUL(ByVal v_fecsim As String, ByVal v_grupo As String, ByVal v_subgrupo As String, ByVal v_dema As String, ByVal v_codigo As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"fecha_sim", OracleDbType.Varchar2, 30, v_fecsim}, _
                                       {"grupo", OracleDbType.Varchar2, 3, v_grupo}, _
                                       {"subgrupo", OracleDbType.Varchar2, 3, v_subgrupo}, _
                                       {"hoja", OracleDbType.Varchar2, 10, v_dema}, _
                                       {"codigo", OracleDbType.Varchar2, 13, v_codigo}}

        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_INS_SG_D_DEMAXDERESIMUL"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUTGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_Registro(ByVal pastrTipo As String, ByVal pastrOpcion As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TIPO", OracleDbType.Varchar2, 6, pastrTipo}, _
                                       {"v_AG_OPCION", OracleDbType.Varchar2, 50, pastrOpcion}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_REGISTRA_GCM"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_Selecciona_x_Zona(ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_UBIGEO", OracleDbType.Varchar2, 6, pastrTipo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_UBIGEO_ZONA"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_Selecciona_x_Ubigeo(ByVal pastrTipo As String, ByVal pastrDato As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TIPO", OracleDbType.Varchar2, 1, pastrTipo}, _
                                       {"V_DATO", OracleDbType.Varchar2, 100, pastrDato}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_LISTA_UBIGEO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Obtiene_Menu_x_Opcion(ByVal pastrOpcion As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_OPCION_MENU", OracleDbType.Varchar2, 6, pastrOpcion}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_BOTON_X_MENU_0"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function F_Obtiene_Botones_x_Opcion(ByVal pastrPerfil As String, ByVal pastrItems As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_PERFIL", OracleDbType.Varchar2, 1, pastrPerfil}, _
                                       {"V_BOTON", OracleDbType.Varchar2, 1000, pastrItems}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_MENU_BOTON_DM"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Obtiene_Boton_Usuario(ByVal pastrPerfil As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_PERFIL", OracleDbType.Varchar2, 1, pastrPerfil}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_BOTON_DM"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Obtiene_Menu_Usuario_1(ByVal pastrPerfil As String, ByVal pastrUsuario As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_USUARIO", OracleDbType.Varchar2, 32, pastrUsuario}, _
                                       {"V_PERFIL", OracleDbType.Varchar2, 3, pastrPerfil}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_MENU_DM_2"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Obtiene_Perfil_Usuario(ByVal pastrUsuario As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_USUARIO", OracleDbType.Varchar2, 32, pastrUsuario}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_PERFIL_DM"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FT_Cuenta_Registro(ByVal pastrTipo As String, ByVal pastrBuscar As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TIPO", OracleDbType.Varchar2, 1, pastrTipo}, _
                                       {"V_BUSCA", OracleDbType.Varchar2, 13, pastrBuscar}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.SP_CUENTA_REGISTRO"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_Man_Observacion_CartaDM_desa(ByVal pastrCodigo As String, ByVal pastrformat As String, ByVal pastrCodeva As String, ByVal pastrIndica As String, _
    ByVal pastrUsufor As String, ByVal pastrdescri As String, ByVal pastrLoguse As String, ByVal pastrOpcion As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}, _
                                       {"V_CG_FORMAT", OracleDbType.Varchar2, 13, pastrformat}, _
                                       {"V_CG_CODEVA", OracleDbType.Varchar2, 13, pastrCodeva}, _
                                       {"V_ET_INDICA", OracleDbType.Varchar2, 1000, pastrIndica}, _
                                       {"V_ET_USUFOR", OracleDbType.Varchar2, 8, pastrUsufor}, _
                                        {"V_DESCRI", OracleDbType.Varchar2, 1000, pastrdescri}, _
                                       {"V_US_LOGUSE", OracleDbType.Varchar2, 8, pastrLoguse}, _
                                       {"V_OPCION", OracleDbType.Varchar2, 7, pastrOpcion}}
        'Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.SP_INS_UPD_OBSERVACION"
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.SP_INS_UPD_OBSERVA_CARTA_IGN"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_Man_Observacion_CartaDM_FORM(ByVal pastrCodigo As String, ByVal pastrCodeva As String, ByVal pastrIndica As String, _
   ByVal pastrUsufor As String, ByVal pastrdescri As String, ByVal pastrLoguse As String, ByVal pastrOpcion As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}, _
                                      {"V_CG_CODEVA", OracleDbType.Varchar2, 13, pastrCodeva}, _
                                      {"V_ET_INDICA", OracleDbType.Varchar2, 1000, pastrIndica}, _
                                      {"V_ET_USUFOR", OracleDbType.Varchar2, 8, pastrUsufor}, _
                                       {"V_DESCRI", OracleDbType.Varchar2, 1000, pastrdescri}, _
                                       {"V_US_LOGUSE", OracleDbType.Varchar2, 8, pastrLoguse}, _
                                       {"V_OPCION", OracleDbType.Varchar2, 7, pastrOpcion}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.SP_INS_UPD_OBSERVACION"
        'Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.SP_INS_UPD_OBSERVA_CARTA_IGN"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function FT_Man_Observacion_CartaDM(ByVal pastrCodigo As String, ByVal pastrCodeva As String, ByVal pastrIndica As String, _
  ByVal pastrUsufor As String, ByVal pastrdescri As String, ByVal pastrLoguse As String, ByVal pastrOpcion As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}, _
                                      {"V_CG_CODEVA", OracleDbType.Varchar2, 13, pastrCodeva}, _
                                      {"V_ET_INDICA", OracleDbType.Varchar2, 1000, pastrIndica}, _
                                      {"V_ET_USUFOR", OracleDbType.Varchar2, 8, pastrUsufor}, _
                                       {"V_DESCRI", OracleDbType.Varchar2, 1000, pastrdescri}, _
                                       {"V_US_LOGUSE", OracleDbType.Varchar2, 8, pastrLoguse}, _
                                       {"V_OPCION", OracleDbType.Varchar2, 7, pastrOpcion}}

        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.SP_INS_UPD_OBSERVA_CARTAIGN"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function




    Public Function FT_Estado_Observacion(ByVal pastrCodigo As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.SP_VERIFICA_OBSERVACION"
        'Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.SP_VERIFICA_OBSERVACION_CARTA"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function F_Verifica_Usuario(ByVal pastrUsuario As String, ByVal p_gloUsuConexionOracle As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_USUARIO", OracleDbType.Varchar2, 32, pastrUsuario}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_VERIFICA_USUARIO"
        Try
            strConexion = p_gloUsuConexionOracle
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function F_Obtiene_Area_Reserva(ByVal pastrTipo As String, ByVal pastrCampo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TIPO", OracleDbType.Varchar2, 6, pastrTipo}, _
                                       {"V_BUSCA", OracleDbType.Varchar2, 30, pastrCampo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_AREA_RESERVA"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function F_Obtiene_Carta_oficial(ByVal pastrCampo As String, ByVal pastrDato As String, ByVal pdatum As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CAMPO", OracleDbType.Varchar2, 12, pastrCampo}, _
                                       {"V_DATO", OracleDbType.Varchar2, 40, pastrDato}, _
                                         {"V_DATUM", OracleDbType.Varchar2, 40, pdatum}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_CARTA_OFICIAL"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Obtiene_Carta_ANT(ByVal pastrCampo As String, ByVal pastrDato As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CAMPO", OracleDbType.Varchar2, 12, pastrCampo}, _
                                      {"V_DATO", OracleDbType.Varchar2, 40, pastrDato}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_CARTA"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function




    Public Function F_Obtiene_Datos_DM(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_DM_WGS_84_OFICIAL"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Obtiene_Datos_Obs_carta(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}}

        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.SP_VERIFICA_DESCRI_OBS_CARTA"

        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Obtiene_Datos_Obs_carta_desa(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}}
        'Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.SP_VERIFICA_OBSERVACION_CARTA"
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.SP_VERIFICA_DESCRI_OBS_CARTA"

        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Obtiene_Datos_DM_84_dervig(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_DM_84"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Obtiene_Datos_DM_84(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_DM_WGS_84_OFICIAL"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_DATA_ACUMULACION(ByVal v_codigo As String, ByVal v_tipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos

        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 32, v_codigo}, _
                                      {"V_TIPO", OracleDbType.Varchar2, 2, v_tipo}}

        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_SEL_DATOS_DM_ACUMULACION"
        'Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_DM_ACUMULACION"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            'Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_DATA_ACCEDITARIO(ByVal v_codigo As String, ByVal v_tipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos

        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 32, v_codigo}, _
                                      {"V_TIPO", OracleDbType.Varchar2, 2, v_tipo}}

        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_SEL_DATOS_DM_ACCEDITARIO"
        'Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_DM_ACUMULACION"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            'Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Obtiene_Datos_DM_84_AC(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SIGCATMIN.P_SEL_DATOS_DM_WGS_84_AC"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Obtiene_Datos_DM_84_1(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_DM_WGS84_1"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Obtiene_Datos_DM_ACUMULACION(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_DM_ACUMULACION"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function F_OBTIENE_DM_UNIQUE(ByVal pastrCodigo As String, ByVal pastrTipo As Integer) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}, _
                                       {"V_TIPO", OracleDbType.Varchar2, 1, pastrTipo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DM_UNIQUE"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function F_Obtiene_Datos_UBIGEO(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_UBIGEO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function F_Obtiene_Datos_UBIGEO1(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 255, pastrCodigo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_UBIGEO_MULTIPLE"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_Selecciona_capaxDM(ByVal pastrCodigo As String, ByVal seleccion_capa As String) As String
        Dim lstrProcedimiento As String = ""
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}}
        If seleccion_capa = "Rio" Then
            lstrProcedimiento = "DATA_GIS.PACK_DBA_GIS.P_Verifica_Int_cat_rio"
        ElseIf seleccion_capa = "Carretera" Then
            lstrProcedimiento = "DATA_GIS.PACK_DBA_GIS.P_Verifica_Int_cat_carretera"
        ElseIf seleccion_capa = "Frontera" Then
            lstrProcedimiento = "DATA_GIS.PACK_DBA_GIS.P_Verifica_Int_cat_front"
        End If
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUTGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_Selecciona_capaxDM_new(ByVal pastrCodigo1 As String, ByVal seleccion_capa1 As String, ByVal datumorigen1 As String) As String
        Dim lstrProcedimiento As String = ""
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos

        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo1}, _
                                       {"SELECCION", OracleDbType.Varchar2, 30, seleccion_capa1}, _
                                       {"DATUM", OracleDbType.Varchar2, 10, datumorigen1}}
        lstrProcedimiento = "DATA_GIS.PACK_DBA_GIS.P_Verifica_Int_Cata"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUTGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Obtiene_Datos_DMXPAISES(ByVal pastrCodigo As String) As DataTable
        Dim lstrProcedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"NOMBRE", OracleDbType.Varchar2, 100, pastrCodigo}}
        lstrProcedimiento = "DATA_GIS.PACK_DBA_GIS.p_Verifica_Int_cat_Paises"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Obtiene_Datos_DMXPAISES_new(ByVal pastrCodigo As String, ByVal datumorigen As String) As DataTable
        Dim lstrProcedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 100, pastrCodigo}, _
                                       {"DATUM", OracleDbType.Varchar2, 10, datumorigen}}
        lstrProcedimiento = "DATA_GIS.PACK_DBA_GIS.P_Verifica_Int_Cat_Paises_new"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_SG_IMAGEN(ByVal pastrAccion As String, ByVal pastrCodigo As Date, _
    ByVal pastrEGFormat As String, ByVal pastrUsuario As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastrAccion}, _
                                       {"V_ANIMAGEN", OracleDbType.Date, 13, pastrCodigo}, _
                                       {"V_CG_CODIGO1", OracleDbType.Varchar2, 13, pastrEGFormat}, _
                                       {"V_USLOGUSE", OracleDbType.Varchar2, 32, pastrUsuario}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SC_H_INSIMAGEN"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function FT_SG_CUENTA_REG_IN_AREAS_EVALGISX(ByVal pastr_CGCODIGO As String, _
    ByVal pastrEG_FORMAT As String, ByVal pastrIE_CODIGO As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
                                       {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrEG_FORMAT}, _
                                       {"V_IE_CODIGO", OracleDbType.Varchar2, 2, pastrIE_CODIGO}}
        'Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.EXISTE_REG_AREASGIS"
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_EXISTE_REG_EVALGIS"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function FT_SG_CUENTA_REG_IN_EVALGISX(ByVal pastr_CGCODIGO As String, _
  ByVal pastrEG_FORMAT As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
                                       {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrEG_FORMAT}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_EXISTE_REG_EVALGIS"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_SG_CUENTA_REG_LIB_CARACEVALGIS(ByVal pastr_CGCODIGO As String, _
     ByVal pastrEG_FORMAT As String, ByVal V_FECHA As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
                                     {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrEG_FORMAT}, _
                                     {"V_FECHA", OracleDbType.Varchar2, 32, V_FECHA}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.EXISTE_REG_LIBDEN_CARACEVALGIS"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function FT_SG_CUENTA_REG_IN_CARACEVALGISX(ByVal pastr_CGCODIGO As String, _
    ByVal pastrEG_FORMAT As String, ByVal pastrCG_CODEVA As String, ByVal pastrIE_CODIGO As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
                                       {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrEG_FORMAT}, _
                                       {"V_CG_CODEVA", OracleDbType.Clob, 4000000, pastrCG_CODEVA}, _
                                       {"V_IE_CODIGO", OracleDbType.Varchar2, 2, pastrIE_CODIGO}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.EXISTE_REG_CARACEVALGIS"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function FT_SG_CUENTA_REG_IN_EVALGIS(ByVal pastr_TABLA As String, ByVal pastr_CGCODIGO As String, _
   ByVal pastrEG_FORMAT As String, ByVal pastrCG_CODEVA As String, ByVal pastrIE_CODIGO As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TABLA", OracleDbType.Varchar2, 30, pastr_TABLA}, _
                                       {"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
                                       {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrEG_FORMAT}, _
                                       {"V_CG_CODEVA", OracleDbType.Clob, 4000000, pastrCG_CODEVA}, _
                                       {"V_IE_CODIGO", OracleDbType.Varchar2, 2, pastrIE_CODIGO}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_EXISTE_REG_EVALGIS"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FT_SG_CUENTA_REG_AREAS_PROTEGIDASX(ByVal pastr_CGCODIGO As String, _
    ByVal pastrIE_CODIGO As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
                                      {"V_IE_CODIGO", OracleDbType.Varchar2, 2, pastrIE_CODIGO}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.EXISTE_REG_AreasProtrgidas"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function



    Public Function P_Obtiene_Datos_DM_RESO(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_RESO_DM"
        Try


            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function P_Obtiene_Datos_DM_SITUACION(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_OBTIENE_DATOS_SITUACION"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function P_Obtiene_Datos_DM_ESTAMIN(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_DM_ESTAMIN"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function P_Obtiene_Datos_DM_INTEGRANTE_UEA(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_DM_INTEGRANTE_UEA"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    ' Public Function FT_Guarda_AreaNeta_Padron(ByVal pastr_CGCODIGO As String, _
    'ByVal pastrAG_AREA As String, ByVal pastrIE_CODIGO As String) As String
    '     Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '     Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
    '                                    {"V_AG_AREA", OracleDbType.Double, 13, pastrAG_AREA}, _
    '                                    {"V_IE_CODIGO", OracleDbType.Varchar2, 4, pastrIE_CODIGO}}

    '     Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_REGISTRA_AREASNETA"
    '     Try
    '         Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
    '         objDatos = Nothing
    '     Catch
    '         Throw
    '     End Try
    ' End Function


    Public Function FT_Obtiene_datos_XYminmax(ByVal pCodigo As String, ByVal pLayer1 As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"v_codigo", OracleDbType.Varchar2, 20, pCodigo}, _
                                       {"v_layer_1", OracleDbType.Varchar2, 50, pLayer1}}
        Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_SEL_DATOS_XY"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function F_Obtiene_Datos_DMxPadron_NT(ByVal pastrCodigo As String, ByVal pastrZONA As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 13, pastrCodigo}, _
                                      {"V_ZONA", OracleDbType.Varchar2, 2, pastrZONA}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_PADRON_DM_NT_COD"

        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function F_Obtiene_Datos_DMxPadron_EVAL(ByVal pastrCodigo As String, ByVal pastrZONA As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 13, pastrCodigo}, _
                                      {"V_ZONA", OracleDbType.Varchar2, 2, pastrZONA}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_PADRON_DM_EVAL_COD"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Obtiene_fecha_Padron_EVAL(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_VERIFICA_FEC_PAD"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function FT_SG_D_LIBDEN_CARAC_EVALGIS(ByVal pastrAccion As String, ByVal pastr_CGCODIGO As String, _
      ByVal pastrEGFORMAT As String, ByVal pastrCG_CODEVA As String, ByVal pastrIE_CODIGO As String, _
      ByVal pastrEG_DESCRI As String, ByVal pastrEG_VALOR As String, _
      ByVal pastrEG_TIPO As String, ByVal pastrUsuario As String, ByVal feclibden As String) As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_ACCION", OracleDbType.Varchar2, 3, pastrAccion}, _
                                       {"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
                                       {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrEGFORMAT}, _
                                       {"V_CG_CODEVA", OracleDbType.Clob, 4000000, pastrCG_CODEVA}, _
                                       {"V_IE_CODIGO", OracleDbType.Clob, 4000000, pastrIE_CODIGO}, _
                                       {"V_EG_DESCRI", OracleDbType.Clob, 4000000, pastrEG_DESCRI}, _
                                       {"V_EG_VALOR", OracleDbType.Clob, 4000000, pastrEG_VALOR}, _
                                       {"V_EG_TIPO", OracleDbType.Clob, 4000000, pastrEG_TIPO}, _
                                       {"V_USLOGUSE", OracleDbType.Varchar2, 32, pastrUsuario}, _
                                       {"V_FECHA", OracleDbType.Varchar2, 32, feclibden}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_DATOS_LIBDENU_CARACEVALGIS"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function F_Obtiene_Tipo_AreaRestringida() As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_CATNOMIN_TIPO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function FT_Int_tiporesexdepa(ByVal pTipo As String, ByVal pLayer1 As String, _
   ByVal pLayer2 As String, ByVal pCodigo As String, ByVal ptipoar As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"v_Tipo", OracleDbType.Varchar2, 2, pTipo}, _
                                       {"v_layer_1", OracleDbType.Varchar2, 50, pLayer1}, _
                                       {"v_layer_2", OracleDbType.Varchar2, 50, pLayer2}, _
                                       {"v_codigo", OracleDbType.Varchar2, 20, pCodigo}, _
                                       {"v_tipoar", OracleDbType.Varchar2, 50, ptipoar}}
        Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_INT_RESEXDEPA"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing

        Catch
            Throw
        End Try
    End Function

    Public Function FT_Ver_Observacion_CartaIGN(ByVal pastrCodigo As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}}

        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.SP_VERIFICA_OBSERVACION_CARTA"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_Ver_Area_Restringida(ByVal pastrCodigo As String, ByVal pclase As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 30, pastrCodigo}, _
                                      {"V_CLASE", OracleDbType.Varchar2, 2, pclase}}

        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_AREAS_RESTRINGIDAS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Obtiene_Datos_DM_Proteg(ByVal pastrZONA As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_ZONA", OracleDbType.Varchar2, 2, pastrZONA}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_DM_PROTEG"

        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Item_Data_DM_AREA(ByVal pastrCodigo As String) As DataTable
        Dim lstrProcedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"v_cod_dm", OracleDbType.Varchar2, 13, pastrCodigo}}
        lstrProcedimiento = "PACK_DBA_SG_D_EVALGIS.P_OBTIENE_AREA_DM"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function F_Obtiene_Datos_DMXFECHA(ByVal pfecha As String) As DataTable
        Dim lstrProcedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_FECHA", OracleDbType.Varchar2, 100, pfecha}}
        lstrProcedimiento = "DATA_GIS.PACK_DBA_GIS.P_OBTIENE_FECHA_DM"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    'Para guardar ubigeo de las areas restringidas

    Public Function FT_Actualizar_DemaXAnm(ByVal past_codigo_anm As String, ByVal pastrCodigo As String, _
    ByVal pastrUsuario As String, ByVal pastr_seccion As String, ByVal pastr_tipo As String, ByVal pastr_datum As String, ByVal pastr_accion As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, past_codigo_anm}, _
                                       {"V_DE_CODDEM", OracleDbType.Varchar2, 13, pastrCodigo}, _
                                       {"V_USLOGUSE", OracleDbType.Varchar2, 13, pastrUsuario}, _
                                        {"V_CA_SESSION", OracleDbType.Varchar2, 13, pastr_seccion}, _
                                       {"V_DGTIPDEM", OracleDbType.Varchar2, 13, pastr_tipo}, _
                                       {"V_DATUM", OracleDbType.Varchar2, 13, pastr_datum}, _
                                       {"V_ACCION", OracleDbType.Varchar2, 13, pastr_accion}}

        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_REGISTRA_DEMAXANM"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    'Para guardar CARTAS de las areas restringidas

    Public Function FT_Actualizar_cartaXAnm(ByVal past_codigo_anm As String, ByVal pastr_Codigo_carta As String, _
    ByVal pastrUsuario As String, ByVal pastr_tipo As String, ByVal pastr_sesion As String, ByVal pastr_datum As String, ByVal pastr_accion As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, past_codigo_anm}, _
                                       {"V_CA_CODCDAR", OracleDbType.Varchar2, 13, pastr_Codigo_carta}, _
                                       {"V_US_LOGUSE", OracleDbType.Varchar2, 13, pastrUsuario}, _
                                       {"V_CA_TIPCAR", OracleDbType.Varchar2, 13, pastr_tipo}, _
                                       {"V_CA_SESSION", OracleDbType.Varchar2, 32, pastr_sesion}, _
                                       {"V_CA_DATUM", OracleDbType.Varchar2, 32, pastr_datum}, _
                                       {"V_ACCION", OracleDbType.Varchar2, 13, pastr_accion}}

        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_REGISTRA_CARTAXANM"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    'Para descodififcar el 

    Public Function F_consultar_usuario(ByVal past_codigo_anm As String, ByVal pastr_clave_anm As String) As DataTable

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_usuario", OracleDbType.Varchar2, 13, past_codigo_anm}, _
                                       {"V_Contra", OracleDbType.Varchar2, 13, pastr_clave_anm}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_OBTIENE_USUARIO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    'Para guardar AREAS de las areas restringidas

    Public Function FT_Actualizar_AreasXAnm(ByVal past_accion As String, ByVal past_codigo_anm As String, ByVal pastr_tipo As String, _
    ByVal pastr_area As String, ByVal pastrUsuario As String, ByVal pastr_sesion As String, ByVal pastr_datum As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_ACCION", OracleDbType.Varchar2, 13, past_accion}, _
                                       {"V_CG_CODIGO", OracleDbType.Varchar2, 13, past_codigo_anm}, _
                                       {"V_TIPO", OracleDbType.Varchar2, 13, pastr_tipo}, _
                                       {"V_AREA", OracleDbType.Varchar2, 24, pastr_area}, _
                                       {"V_US_LOGUSE", OracleDbType.Varchar2, 13, pastrUsuario}, _
                                       {"V_SESSION", OracleDbType.Varchar2, 32, pastr_sesion}, _
                                       {"V_DATUM", OracleDbType.Varchar2, 32, pastr_datum}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_REGISTRA_AREAXANM"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    'Para Traer datos de areas restringidas procesadas en el dia
    Public Function FT_consultar_proceso_Anm(ByVal pastrEGFormat As String, ByVal fecha As String) As DataTable

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_FORMAT", OracleDbType.Varchar2, 13, pastrEGFormat}, _
                                       {"V_FECHA", OracleDbType.Varchar2, 32, fecha}}

        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_FEC_ANM"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_Actualiza_tabla_temp_prod(ByVal past_codigo_anm As String, ByVal past_clase_anm As String, ByVal pastr_sesion As String, ByVal pastr_sistema As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"XCODIGO", OracleDbType.Varchar2, 13, past_codigo_anm}, _
                                     {"V_CLASE", OracleDbType.Varchar2, 13, past_clase_anm}, _
                                     {"V_SESION", OracleDbType.Clob, 4000000, pastr_sesion}, _
                                      {"V_DATUM", OracleDbType.Varchar2, 13, pastr_sistema}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_ACTUALIZA_DATOS_ANM"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

 

    'Public Function FT_SG_D_CARAC_EVALGIS(ByVal pastrAccion As String, ByVal pastr_CGCODIGO As String, _
    '  ByVal pastrEGFORMAT As String, ByVal pastrCG_CODEVA As String, ByVal pastrIE_CODIGO As String, _
    '  ByVal pastrEG_DESCRI As String, ByVal pastrEG_VALOR As String, _
    '  ByVal pastrEG_TIPO As String, ByVal pastrUsuario As String) As String

    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    Dim Parametros(,) As Object = {{"V_ACCION", OracleDbType.Varchar2, 3, pastrAccion}, _
    '                                   {"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
    '                                   {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrEGFORMAT}, _
    '                                   {"V_CG_CODEVA", OracleDbType.Clob, 4000000, pastrCG_CODEVA}, _
    '                                   {"V_IE_CODIGO", OracleDbType.Clob, 4000000, pastrIE_CODIGO}, _
    '                                   {"V_EG_DESCRI", OracleDbType.Clob, 4000000, pastrEG_DESCRI}, _
    '                                   {"V_EG_VALOR", OracleDbType.Clob, 4000000, pastrEG_VALOR}, _
    '                                   {"V_EG_TIPO", OracleDbType.Clob, 4000000, pastrEG_TIPO}, _
    '                                   {"V_USLOGUSE", OracleDbType.Varchar2, 32, pastrUsuario}}
    '    Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SG_D_CARAC_EVALGIS"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try
    'End Function


    Public Function FT_ACTUALIZA_IMAGEM_ANM(ByVal pastr_CGCODIGO As String, _
     ByVal past_name As String, ByVal pastr_Usuario As String, ByVal past_name_wgs As String) As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 16, pastr_CGCODIGO}, _
                                      {"V_AN_IMAGEN", OracleDbType.Varchar2, 24, past_name}, _
                                      {"V_USLOGUSE", OracleDbType.Varchar2, 32, pastr_Usuario}, _
                                      {"V_AN_IMAGEN_W84", OracleDbType.Varchar2, 32, past_name_wgs}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_REGISTRA_IMAGENXANM"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FT_obtiene_hectagis_dm(ByVal codigo As String, ByVal capa As String) As DataTable

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"v_Codigo", OracleDbType.Varchar2, 13, codigo}, _
                                       {"v_layer_1", OracleDbType.Varchar2, 80, capa}}

        Dim lstrProcedimiento As String = "DATA_GIS.PACK_DBA_GIS.P_obtiene_area_DM"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableGIS(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FT_obtiene_situacion_dm(ByVal codigo As String) As DataTable

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"v_Codigo", OracleDbType.Varchar2, 13, codigo}}


        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_OBTIENE_DATOS_SITUACION"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FT_SG_D_REGION_X_AR(ByVal pastrAccion As String, ByVal pastprese As String, _
     ByVal pastnmrese As String, ByVal pastdepa As String, ByVal pastareaini As String, ByVal pastareasup As String, ByVal pastporcen As String, ByVal pastUSUARIO As String, ByVal pastobs As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_ACCION", OracleDbType.Varchar2, 13, pastrAccion}, _
                                       {"V_CG_TPRESE", OracleDbType.Varchar2, 13, pastprese}, _
                                       {"V_NM_TPRESE", OracleDbType.Varchar2, 60, pastnmrese}, _
                                       {"V_CG_DEPA", OracleDbType.Varchar2, 32, pastdepa}, _
                                       {"V_CG_AREAINI", OracleDbType.Varchar2, 32, pastareaini}, _
                                        {"V_CG_AREANETA", OracleDbType.Varchar2, 32, pastareasup}, _
                                        {"V_CG_PORCEN", OracleDbType.Varchar2, 32, pastporcen}, _
                                        {"V_CG_USUARIO", OracleDbType.Varchar2, 32, pastUSUARIO}, _
                                        {"V_CG_OBSERVA", OracleDbType.Varchar2, 32, pastobs}}



        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SG_D_REGION_X_AR"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FT_OBTIENE_TIPORESE(ByVal pastnmrese As String) As DataTable
        Dim lstrProcedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TIPO", OracleDbType.Varchar2, 80, pastnmrese}}
        lstrProcedimiento = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_TIPORESE"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch

            Throw
        End Try
    End Function


    Public Function FT_SEL_LISTA_INFORMES(ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TIPO", OracleDbType.Varchar2, 32, pastrTipo}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_INFORMES"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FT_SG_CUENTA_REG_TABLA_TPRESE(ByVal pastr_TABLA As String, ByVal pastr_CGCODIGO As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TABLA", OracleDbType.Varchar2, 30, pastr_TABLA}, _
                                       {"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}}
                                   
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_EXISTE_TPRESE"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FT_SG_CUENTA_REG_RESE(ByVal pARCHIVO As String, ByVal pTipo As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_ARCHIVO", OracleDbType.Varchar2, 30, pARCHIVO}, _
                                        {"V_TIPO", OracleDbType.Varchar2, 50, pTipo}}

        Dim lstrProcedimiento As String = "PACK_DBA_GIS.p_EXISTE_p_archivo_area"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)

            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FT_ELIMINA_REG_RESE(ByVal pTipo As String, ByVal pARCHIVO As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TIPO", OracleDbType.Varchar2, 30, pTipo}, _
                                        {"V_ARCHIVO", OracleDbType.Varchar2, 50, pARCHIVO}}

        Dim lstrProcedimiento As String = "PACK_DBA_GIS.P_ELIMINA_REGISTROS_ANM"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUTGIS(lstrProcedimiento, Parametros)

            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function




End Class
