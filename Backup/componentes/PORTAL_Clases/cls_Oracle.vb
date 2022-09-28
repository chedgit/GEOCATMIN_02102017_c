Option Explicit On
Imports Oracle.DataAccess.Client

Public Class cls_Oracle

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

    Public Function FT_SG_D_EVALGIS(ByVal pastrAccion As String, ByVal pastrCodigo As String, _
     ByVal pastrEGFormat As String, ByVal pastrUsuario As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_ACCION", OracleDbType.Varchar2, 13, pastrAccion}, _
                                       {"V_CGCODIGO", OracleDbType.Varchar2, 13, pastrCodigo}, _
                                       {"V_EGFORMAT", OracleDbType.Varchar2, 2, pastrEGFormat}, _
                                       {"V_USLOGUSE", OracleDbType.Varchar2, 32, pastrUsuario}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SG_D_EVALGIS1"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
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
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SG_D_CARAC_EVALGIS1"
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
                                       {"V_CG_CODEVA", OracleDbType.Clob, 4000000, pastrCG_CODEVA}, _
                                       {"V_AG_AREA", OracleDbType.Clob, 4000000, pastrAG_AREA}, _
                                       {"V_IE_CODIGO", OracleDbType.Varchar2, 1000, pastrIE_CODIGO}, _
                                       {"V_AG_HECTAR", OracleDbType.Clob, 4000000, pastrAG_HECTAR}, _
                                       {"V_TIPO", OracleDbType.Varchar2, 4000, pastrTipo}, _
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
    Public Function FT_SG_D_COORD_EVALGIS2(ByVal pastrAccion As String, ByVal pastr_CGCODIGO As String, _
    ByVal pastrEGFORMAT As String, ByVal pastrCG_CODEVA As String, ByVal pastrAG_AREA As String, ByVal V_vertice As String, _
    ByVal pastrIE_CODIGO As String, ByVal pastrCV_ESTE As String, ByVal pastrCV_NORTE As String, _
    ByVal pastrAG_HECTAR As String, ByVal pastrUsuario As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_ACCION", OracleDbType.Varchar2, 3, pastrAccion}, _
                                       {"V_CGCODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
                                       {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrEGFORMAT}, _
                                       {"V_CG_CODEVA", OracleDbType.Varchar2, 15, pastrCG_CODEVA}, _
                                       {"V_AG_AREA", OracleDbType.Varchar2, 10, pastrAG_AREA}, _
                                       {"V_NU_ORDEN", OracleDbType.Varchar2, 10, V_vertice}, _
                                       {"V_IE_CODIGO", OracleDbType.Varchar2, 10, pastrIE_CODIGO}, _
                                       {"V_CV_ESTE", OracleDbType.Clob, 4000000, pastrCV_ESTE}, _
                                       {"V_CV_NORTE", OracleDbType.Clob, 4000000, pastrCV_NORTE}, _
                                       {"V_USLOGUSE", OracleDbType.Varchar2, 32, pastrUsuario}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SG_D_COORD_EVALGIS2"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_SG_D_COORD_EVALGIS(ByVal pastrAccion As String, ByVal pastr_CGCODIGO As String, _
ByVal pastrEGFORMAT As String, ByVal pastrCG_CODEVA As String, ByVal pastrAG_AREA As String, _
ByVal pastrIE_CODIGO As String, ByVal pastrCV_ESTE As String, ByVal pastrCV_NORTE As String, _
ByVal pastrAG_HECTAR As String, ByVal pastrUsuario As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_ACCION", OracleDbType.Varchar2, 3, pastrAccion}, _
                                       {"V_CGCODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
                                       {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrEGFORMAT}, _
                                       {"V_CG_CODEVA", OracleDbType.Varchar2, 15, pastrCG_CODEVA}, _
                                       {"V_AG_AREA", OracleDbType.Varchar2, 10, pastrAG_AREA}, _
                                       {"V_IE_CODIGO", OracleDbType.Varchar2, 10, pastrIE_CODIGO}, _
                                       {"V_CV_ESTE", OracleDbType.Clob, 4000000, pastrCV_ESTE}, _
                                       {"V_CV_NORTE", OracleDbType.Clob, 4000000, pastrCV_NORTE}, _
                                       {"V_USLOGUSE", OracleDbType.Varchar2, 32, pastrUsuario}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SG_D_COORD_EVALGIS1"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
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

    Public Function FT_Man_Observacion_CartaDM(ByVal pastrCodigo As String, ByVal pastrCodeva As String, ByVal pastrIndica As String, _
    ByVal pastrUsufor As String, ByVal pastrLoguse As String, ByVal pastrOpcion As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastrCodigo}, _
                                       {"V_CG_CODEVA", OracleDbType.Varchar2, 13, pastrCodeva}, _
                                       {"V_ET_INDICA", OracleDbType.Varchar2, 1000, pastrIndica}, _
                                       {"V_ET_USUFOR", OracleDbType.Varchar2, 8, pastrUsufor}, _
                                       {"V_US_LOGUSE", OracleDbType.Varchar2, 8, pastrLoguse}, _
                                       {"V_OPCION", OracleDbType.Varchar2, 7, pastrOpcion}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.SP_INS_UPD_OBSERVACION"
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
    Public Function F_Obtiene_Carta(ByVal pastrCampo As String, ByVal pastrDato As String) As DataTable
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
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_SEL_DATOS_DM"
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
    Public Function FT_SG_CUENTA_REG_IN_AREAS_EVALGIS(ByVal pastr_CGCODIGO As String, _
    ByVal pastrEG_FORMAT As String, ByVal pastrIE_CODIGO As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
                                       {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrEG_FORMAT}, _
                                       {"V_IE_CODIGO", OracleDbType.Varchar2, 2, pastrIE_CODIGO}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.EXISTE_REG_AREASGIS"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function FT_SG_CUENTA_REG_IN_EVALGIS(ByVal pastr_CGCODIGO As String, _
  ByVal pastrEG_FORMAT As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
                                       {"V_EG_FORMAT", OracleDbType.Varchar2, 2, pastrEG_FORMAT}}
        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.EXISTE_REG_EVALGIS"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_SG_CUENTA_REG_IN_CARACEVALGIS(ByVal pastr_CGCODIGO As String, _
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


    Public Function FT_Guarda_AreaNeta_Padron(ByVal pastr_CGCODIGO As String, _
   ByVal pastrAG_AREA As String, ByVal pastrIE_CODIGO As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CG_CODIGO", OracleDbType.Varchar2, 13, pastr_CGCODIGO}, _
                                       {"V_AG_AREA", OracleDbType.Double, 13, pastrAG_AREA}, _
                                       {"V_IE_CODIGO", OracleDbType.Varchar2, 4, pastrIE_CODIGO}}

        Dim lstrProcedimiento As String = "PACK_DBA_SG_D_EVALGIS.P_REGISTRA_AREASNETA"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


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


End Class
