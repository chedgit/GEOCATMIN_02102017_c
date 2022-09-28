Option Explicit On 
Imports Oracle.DataAccess.Client

Public Class clsBD_Tablas_Generales

    Public Function FT_Tot_Servicio(ByVal pastrTipo As String, ByVal pastrNumero As String) As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos

        Dim Parametros(,) As Object = {{"v_Tipo", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"v_Numero", OracleDbType.Varchar2, 4, pastrNumero}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_TOTAL_SERVICIOS"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FS_Sel_Nota(ByVal pastrNumero As String, ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"V_NUMERO", OracleDbType.Varchar2, 8, pastrNumero}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_NOTA"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function


    Public Function FS_Sel_Lista_Doc_Detalle(ByVal pastrTipo As String, ByVal pastrNumero As String, ByVal pastrsSelecion As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"V_NUMERO", OracleDbType.Varchar2, 4, pastrNumero}, _
                                       {"V_SELECCION", OracleDbType.Varchar2, 1, pastrsSelecion}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DOC_TRABAJO_ITEM"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    Public Function FS_Sel_Lista_Combo1(ByVal pastrAcceso As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_ACCESO", OracleDbType.Varchar2, 2, pastrAcceso}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_CBO_ESTADO_LAB"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    'Public Function FS_Sel_Lista_Combo1(ByVal pastrTipo As String) As DataTable
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 32, pastrTipo}}
    '    Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_ESTADO_DOCS_GML"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try
    'End Function


    Public Function FS_Sel_Muestras(ByVal pastrTipo As String, ByVal pastrNumero As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"V_NUMERO", OracleDbType.Varchar2, 4, pastrNumero}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_TIPO_MUESTRA1"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FT_Retorna_Estado(ByVal pastrTipo As String, _
                                ByVal pastrNumero As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"V_NUMERO", OracleDbType.Varchar2, 4, pastrNumero}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_ESTADO_DOCUMENTO"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FS_Sel_Lista_Usuario_Jefe(ByVal pastrTipo As String, ByVal pastrAcceso As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_USUARIO", OracleDbType.Varchar2, 32, pastrTipo}, _
                                       {"V_ACCESO", OracleDbType.Varchar2, 1, pastrAcceso}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.CHK_USUARIO_LAB"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FS_Sel_Historial_BD(ByVal pastrTipo As String, _
                                     ByVal pastrNroDoc As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TIPO", OracleDbType.Varchar2, 20, pastrTipo}, _
                                       {"V_NUMERO", OracleDbType.Varchar2, 20, pastrNroDoc}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_HISTORIAL"

        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FT_Ver_Estado_Doc(ByVal pastrTipo As String, ByVal pastrNumero As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"V_NUMERO", OracleDbType.Varchar2, 4, pastrNumero}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.VER_ESTADO_DOC"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FS_Sel_Lista_Personal(ByVal pastrIdPadre As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_LABORATORIO", OracleDbType.Varchar2, 3, pastrIdPadre}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_LAB_PERSONAL"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FS_Sel_Lista_Direccion(ByVal pastrIdPadre As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_ID_PADRE", OracleDbType.Varchar2, 3, pastrIdPadre}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_LABORATORIO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_Man_Tabla_Estado(ByVal pastrAccion As String, ByVal pastrTipo As String, _
                                ByVal pastrNumero As String, ByVal pastrEstado As String, _
                                ByVal pastrFecha As String, ByVal lostrUsuario As String, _
                                ByVal pastrLaboratorio As String, ByVal pastrAsigna As String, ByVal pastrNota As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos

        Dim Parametros(,) As Object = {{"V_ACCION", OracleDbType.Varchar2, 15, pastrAccion}, _
                                       {"V_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"V_NUMERO", OracleDbType.Varchar2, 4, pastrNumero}, _
                                       {"V_ESTADO", OracleDbType.Varchar2, 2, pastrEstado}, _
                                       {"V_FECHA", OracleDbType.Varchar2, 10, pastrFecha}, _
                                       {"V_USUARIO", OracleDbType.Varchar2, 32, lostrUsuario}, _
                                       {"V_LABORATORIOS", OracleDbType.Varchar2, 3, pastrLaboratorio}, _
                                       {"V_ASIGNA", OracleDbType.Varchar2, 32, pastrAsigna}, _
                                       {"V_NOTA", OracleDbType.Varchar2, 1000, pastrNota}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_MANTO_TABLA_ESTADO2"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FS_Sel_Tipo_Muestra(ByVal pastrNumero As String, ByVal pastrTipo As String, ByVal pastrCodServicio As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_NUMERO", OracleDbType.Varchar2, 2, pastrNumero}, _
                                       {"V_TIPO", OracleDbType.Varchar2, 8, pastrTipo}, _
                                       {"V_COD_SERVICIO", OracleDbType.Varchar2, 10, pastrCodServicio}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_TIPO_MUESTRA"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FS_Sel_Lista_Proyecto(ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TIPO_PROYECTO", OracleDbType.Varchar2, 2, pastrTipo}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_LISTA_PROYECTO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_Mantenimiento_Muestras(ByVal pastrAccion As String, ByVal pastrTipo_doc_trabajo As String, ByVal pastrNumero_doc_trabajo As String, _
                                   ByVal pastrCodCampo As String, ByVal pastrUtmx As String, _
                                   ByVal pastrUtmy As String, ByVal pastrLugar As String, _
                                   ByVal pastrCod_Hoja As String, ByVal pastrNom_Dpto As String, _
                                   ByVal pastrNom_Prov As String, ByVal pastrNom_Dist As String, _
                                   ByVal pastrObservacion As String, _
                                   ByVal pastrArea As String, ByVal pastrProy As String, ByVal pastrUsuario As String, _
                                   ByVal pastrCodServicio As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos

        Dim Parametros(,) As Object = {{"v_Accion", OracleDbType.Varchar2, 7, pastrAccion}, _
                                       {"v_tipo_doc_trabajo", OracleDbType.Varchar2, 2, pastrTipo_doc_trabajo}, _
                                       {"v_numero_doc_trabajo", OracleDbType.Varchar2, 4, pastrNumero_doc_trabajo}, _
                                       {"v_cod_campo", OracleDbType.Clob, 4000000, pastrCodCampo}, _
                                       {"v_utm_x", OracleDbType.Clob, 4000000, pastrUtmx}, _
                                       {"v_utm_y", OracleDbType.Clob, 4000000, pastrUtmy}, _
                                       {"v_lugar", OracleDbType.Clob, 4000000, pastrLugar}, _
                                       {"v_cod_hoja", OracleDbType.Clob, 4000000, pastrCod_Hoja}, _
                                       {"v_Nom_Dpto", OracleDbType.Clob, 4000000, pastrNom_Dpto}, _
                                       {"v_Nom_Prov", OracleDbType.Clob, 4000000, pastrNom_Prov}, _
                                       {"v_Nom_Dist", OracleDbType.Clob, 4000000, pastrNom_Dist}, _
                                       {"v_observacion", OracleDbType.Clob, 4000000, pastrObservacion}, _
                                       {"v_Area", OracleDbType.Varchar2, 5, pastrArea}, _
                                       {"v_Proyecto", OracleDbType.Varchar2, 10, pastrProy}, _
                                       {"v_Usuario", OracleDbType.Varchar2, 32, pastrUsuario}, _
                                       {"v_Cod_Servicio", OracleDbType.Varchar2, 10, pastrCodServicio}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_OPERACION_MANTO_MUESTRAS"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_Mantenimiento_Muestras_TMP(ByVal pastrAccion As String, ByVal pastrTipo_doc_trabajo As String, ByVal pastrNumero_doc_trabajo As String, _
                                       ByVal pastrCodCampo As String, ByVal pastrUtmx As String, _
                                       ByVal pastrUtmy As String, ByVal pastrLugar As String, _
                                       ByVal pastrCod_Hoja As String, ByVal pastrNom_Dpto As String, _
                                       ByVal pastrNom_Prov As String, ByVal pastrNom_Dist As String, _
                                       ByVal pastrObservacion As String, _
                                       ByVal pastrArea As String, ByVal pastrProy As String, ByVal pastrUsuario As String, _
                                       ByVal pastrCodServicio As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"v_Accion", OracleDbType.Varchar2, 7, pastrAccion}, _
                                      {"v_tipo_doc_trabajo", OracleDbType.Varchar2, 2, pastrTipo_doc_trabajo}, _
                                      {"v_numero_doc_trabajo", OracleDbType.Varchar2, 4, pastrNumero_doc_trabajo}, _
                                      {"v_cod_campo", OracleDbType.Clob, 4000000, pastrCodCampo}, _
                                      {"v_utm_x", OracleDbType.Clob, 4000000, pastrUtmx}, _
                                      {"v_utm_y", OracleDbType.Clob, 4000000, pastrUtmy}, _
                                      {"v_lugar", OracleDbType.Clob, 4000000, pastrLugar}, _
                                      {"v_cod_hoja", OracleDbType.Clob, 4000000, pastrCod_Hoja}, _
                                      {"v_Nom_Dpto", OracleDbType.Clob, 4000000, pastrNom_Dpto}, _
                                      {"v_Nom_Prov", OracleDbType.Clob, 4000000, pastrNom_Prov}, _
                                      {"v_Nom_Dist", OracleDbType.Clob, 4000000, pastrNom_Dist}, _
                                      {"v_observacion", OracleDbType.Clob, 4000000, pastrObservacion}, _
                                      {"v_Area", OracleDbType.Varchar2, 5, pastrArea}, _
                                      {"v_Proyecto", OracleDbType.Varchar2, 10, pastrProy}, _
                                      {"v_Usuario", OracleDbType.Varchar2, 32, pastrUsuario}, _
                                      {"v_Cod_Servicio", OracleDbType.Varchar2, 10, pastrCodServicio}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_OPERACION_MANTO_MUESTRAS_TMP"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function P_SEL_Departamento() As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DEPARTAMENTOS "
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function P_SEL_Provincia(ByVal V_DEPARTAMENTO As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO", OracleDbType.Varchar2, 32, V_DEPARTAMENTO}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_PROVINCIAS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function P_SEL_Distrito(ByVal V_Provincia As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO", OracleDbType.Varchar2, 32, V_Provincia}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DISTRITOS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FS_Sel_Hoja_Ini_Orden(ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 32, pastrTipo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_HOJA_INI_ORDEN"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    Public Function FS_Sel_Lista_Usuario(ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_USUARIO", OracleDbType.Varchar2, 32, pastrTipo}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_LISTA_USUARIO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function



    Public Function FS_Sel_Lista_Producto(ByVal pastrClaseServicio As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_CLASE_SERVICIO", OracleDbType.Varchar2, 50, pastrClaseServicio}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_LISTA_PRODUCTO_1"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FS_Sel_Lista_Combo(ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 32, pastrTipo}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_PARAMETROS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_Man_Orden_Trabajo1(ByVal pastrAccion As String, ByVal pastrTipo As String, ByVal pastrNumero As String, _
                                   ByVal pastrOriginal As String, ByVal pastrCopia As String, _
                                   ByVal pastrNombre As String, ByVal pastrDireccion As String, _
                                   ByVal pastrEmail As String, ByVal pastrEnvio_Email As String, ByVal pastrFax As String, _
                                   ByVal pastrAgua As String, ByVal pastrRoca As String, _
                                   ByVal pastrMineral As String, ByVal pastrSedimento As String, _
                                   ByVal pastrOtros As String, ByVal pastr30Dias As String, _
                                   ByVal pastr90Dias As String, _
                                   ByVal pastrDescartar As String, ByVal pastrRetomar As String, _
                                   ByVal pastrEM As String, ByVal pastrEP As String, _
                                   ByVal lostrAM As String, ByVal lostrPM As String, _
                                   ByVal lostrAQ As String, ByVal lostrIS As String, ByVal pastrNum_Muestra As String, _
                                   ByVal pastrCod_Muestra As String, ByVal lostrTip_Muestra As String, _
                                   ByVal lostrDet_Muestra As String, ByVal lostrFecha_Entrega As String, _
                                   ByVal lostrTipo_Servicio As String, ByVal lostrEstado As String, _
                                   ByVal lostrObservacion As String, ByVal lostrClase_Prod As String, _
                                   ByVal lostrSerie_Prod As String, ByVal lostrNumero_Prod As String, _
                                   ByVal lostrProyecto As String, ByVal lostrArea As String, _
                                   ByVal lostrUsuario As String, _
                                   ByVal pastrCodCampo As String, ByVal pastrUtmx As String, _
                                   ByVal pastrUtmy As String, ByVal pastrLugar As String, _
                                   ByVal pastrCod_Hoja As String, ByVal pastrNomDpto As String, _
                                   ByVal pastrNomProv As String, ByVal pastrNomDist As String, _
                                   ByVal pastrObservacion As String, _
                                   ByVal pastrCodServicio As String, ByVal pastrCorr_Muestra As String, _
                                   ByVal pastrNota As String, ByVal pastrCantidad As String, _
                                   ByVal pastrLinea As String, ByVal pastrDatoCampo As String) As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos

        Dim Parametros(,) As Object = {{"v_Accion", OracleDbType.Varchar2, 8, pastrAccion}, _
                                       {"v_Tipo", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"v_Numero", OracleDbType.Varchar2, 4, pastrNumero}, _
                                       {"v_envio_informe_a", OracleDbType.Varchar2, 1, pastrOriginal}, _
                                       {"v_envio_informe_b", OracleDbType.Varchar2, 1, pastrCopia}, _
                                       {"v_envio_nombre_a", OracleDbType.Varchar2, 64, pastrNombre}, _
                                       {"v_envio_direccion_a", OracleDbType.Varchar2, 64, pastrDireccion}, _
                                       {"v_envio_nombre_b", OracleDbType.Varchar2, 32, pastrEmail}, _
                                       {"v_envio_email", OracleDbType.Varchar2, 1, pastrEnvio_Email}, _
                                       {"v_envio_fax_b", OracleDbType.Varchar2, 32, pastrFax}, _
                                       {"v_muestra_agua", OracleDbType.Varchar2, 1, pastrAgua}, _
                                       {"v_muestra_roca", OracleDbType.Varchar2, 1, pastrRoca}, _
                                       {"v_muestra_mineral", OracleDbType.Varchar2, 1, pastrMineral}, _
                                       {"v_muestra_sedimento", OracleDbType.Varchar2, 1, pastrSedimento}, _
                                       {"v_muestra_otro", OracleDbType.Varchar2, 1, pastrOtros}, _
                                       {"v_contramuestra_alm_30", OracleDbType.Varchar2, 1, pastr30Dias}, _
                                       {"v_contramuestra_alm_90", OracleDbType.Varchar2, 1, pastr90Dias}, _
                                       {"v_contramuestra_descarta", OracleDbType.Varchar2, 1, pastrDescartar}, _
                                       {"v_contramuestra_retorno", OracleDbType.Varchar2, 1, pastrRetomar}, _
                                       {"V_SERVICIO_EST_MICRO", OracleDbType.Varchar2, 1, pastrEM}, _
                                       {"V_SERVICIO_EST_PALEO", OracleDbType.Varchar2, 1, pastrEP}, _
                                       {"V_SERVICIO_EST_MINER", OracleDbType.Varchar2, 1, lostrAM}, _
                                       {"V_SERVICIO_EST_MUEST", OracleDbType.Varchar2, 1, lostrPM}, _
                                       {"V_SERVICIO_EST_QUIMI", OracleDbType.Varchar2, 1, lostrAQ}, _
                                       {"V_SERVICIO_EST_IMAGE", OracleDbType.Varchar2, 1, lostrIS}, _
                                       {"V_NUM_MUESTRA", OracleDbType.Clob, 4000000, pastrNum_Muestra}, _
                                       {"V_COD_MUESTRA", OracleDbType.Clob, 4000000, pastrCod_Muestra}, _
                                       {"V_TIP_MUESTRA", OracleDbType.Clob, 4000000, lostrTip_Muestra}, _
                                       {"V_DET_MUESTRA", OracleDbType.Clob, 4000000, lostrDet_Muestra}, _
                                       {"V_FECHA_ENTREGA", OracleDbType.Varchar2, 10, lostrFecha_Entrega}, _
                                       {"V_TIPO_SOLICITANTE", OracleDbType.Varchar2, 1, lostrTipo_Servicio}, _
                                       {"V_ESTADO", OracleDbType.Varchar2, 2, lostrEstado}, _
                                       {"V_OBSERVACIONES", OracleDbType.Varchar2, 4000, lostrObservacion}, _
                                       {"V_CLASE_PRODUCTO", OracleDbType.Clob, 4000000, lostrClase_Prod}, _
                                       {"V_SERIE_PRODUCTO", OracleDbType.Clob, 4000000, lostrSerie_Prod}, _
                                       {"V_NUMERO_PRODUCTO", OracleDbType.Clob, 4000000, lostrNumero_Prod}, _
                                       {"V_PROYECTO", OracleDbType.Varchar2, 10, lostrProyecto}, _
                                       {"V_AREA", OracleDbType.Varchar2, 5, lostrArea}, _
                                       {"V_USUARIO", OracleDbType.Varchar2, 32, lostrUsuario}, _
                                       {"v_cod_campo", OracleDbType.Clob, 4000000, pastrCodCampo}, _
                                       {"v_utm_x", OracleDbType.Clob, 4000000, pastrUtmx}, _
                                       {"v_utm_y", OracleDbType.Clob, 4000000, pastrUtmy}, _
                                       {"v_lugar", OracleDbType.Clob, 4000000, pastrLugar}, _
                                       {"v_cod_hoja", OracleDbType.Clob, 4000000, pastrCod_Hoja}, _
                                       {"v_Nom_Dpto", OracleDbType.Clob, 4000000, pastrNomDpto}, _
                                       {"v_Nom_Prov", OracleDbType.Clob, 4000000, pastrNomProv}, _
                                       {"v_Nom_Dist", OracleDbType.Clob, 4000000, pastrNomDist}, _
                                       {"v_observacion", OracleDbType.Clob, 4000000, pastrObservacion}, _
                                       {"v_Cod_Servicio", OracleDbType.Clob, 4000000, pastrCodServicio}, _
                                       {"v_Corr_Muestra", OracleDbType.Clob, 4000000, pastrCorr_Muestra}, _
                                       {"v_Nota", OracleDbType.Varchar2, 1000, pastrNota}, _
                                       {"v_Cantidad", OracleDbType.Clob, 4000000, pastrCantidad}, _
                                       {"v_Linea", OracleDbType.Clob, 4000000, pastrLinea}, _
                                       {"v_DatoCampo", OracleDbType.Clob, 4000000, pastrDatoCampo}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_OPERACION_MANTO_ORDEN3"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FT_Man_Orden_Trabajo(ByVal pastrAccion As String, ByVal pastrTipo As String, ByVal pastrNumero As String, _
                                   ByVal pastrOriginal As String, ByVal pastrCopia As String, _
                                   ByVal pastrNombre As String, ByVal pastrDireccion As String, _
                                   ByVal pastrEmail As String, ByVal pastrEnvio_Email As String, ByVal pastrFax As String, _
                                   ByVal pastrAgua As String, ByVal pastrRoca As String, _
                                   ByVal pastrMineral As String, ByVal pastrSedimento As String, _
                                   ByVal pastrOtros As String, ByVal pastr30Dias As String, _
                                   ByVal pastr90Dias As String, _
                                   ByVal pastrDescartar As String, ByVal pastrRetomar As String, _
                                   ByVal pastrEM As String, ByVal pastrEP As String, _
                                   ByVal lostrAM As String, ByVal lostrPM As String, _
                                   ByVal lostrAQ As String, ByVal lostrIS As String, ByVal pastrNum_Muestra As String, _
                                   ByVal pastrCod_Muestra As String, ByVal lostrTip_Muestra As String, _
                                   ByVal lostrDet_Muestra As String, ByVal lostrFecha_Entrega As String, _
                                   ByVal lostrTipo_Servicio As String, ByVal lostrEstado As String, _
                                   ByVal lostrObservacion As String, ByVal lostrClase_Prod As String, _
                                   ByVal lostrSerie_Prod As String, ByVal lostrNumero_Prod As String, _
                                   ByVal lostrProyecto As String, ByVal lostrArea As String, _
                                   ByVal lostrUsuario As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos

        Dim Parametros(,) As Object = {{"v_Accion", OracleDbType.Varchar2, 8, pastrAccion}, _
                                       {"v_Tipo", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"v_Numero", OracleDbType.Varchar2, 4, pastrNumero}, _
                                       {"v_envio_informe_a", OracleDbType.Varchar2, 1, pastrOriginal}, _
                                       {"v_envio_informe_b", OracleDbType.Varchar2, 1, pastrCopia}, _
                                       {"v_envio_nombre_a", OracleDbType.Varchar2, 64, pastrNombre}, _
                                       {"v_envio_direccion_a", OracleDbType.Varchar2, 64, pastrDireccion}, _
                                       {"v_envio_nombre_b", OracleDbType.Varchar2, 32, pastrEmail}, _
                                       {"v_envio_email", OracleDbType.Varchar2, 1, pastrEnvio_Email}, _
                                       {"v_envio_fax_b", OracleDbType.Varchar2, 32, pastrFax}, _
                                       {"v_muestra_agua", OracleDbType.Varchar2, 1, pastrAgua}, _
                                       {"v_muestra_roca", OracleDbType.Varchar2, 1, pastrRoca}, _
                                       {"v_muestra_mineral", OracleDbType.Varchar2, 1, pastrMineral}, _
                                       {"v_muestra_sedimento", OracleDbType.Varchar2, 1, pastrSedimento}, _
                                       {"v_muestra_otro", OracleDbType.Varchar2, 1, pastrOtros}, _
                                       {"v_contramuestra_alm_30", OracleDbType.Varchar2, 1, pastr30Dias}, _
                                       {"v_contramuestra_alm_90", OracleDbType.Varchar2, 1, pastr90Dias}, _
                                       {"v_contramuestra_descarta", OracleDbType.Varchar2, 1, pastrDescartar}, _
                                       {"v_contramuestra_retorno", OracleDbType.Varchar2, 1, pastrRetomar}, _
                                       {"V_SERVICIO_EST_MICRO", OracleDbType.Varchar2, 1, pastrEM}, _
                                       {"V_SERVICIO_EST_PALEO", OracleDbType.Varchar2, 1, pastrEP}, _
                                       {"V_SERVICIO_EST_MINER", OracleDbType.Varchar2, 1, lostrAM}, _
                                       {"V_SERVICIO_EST_MUEST", OracleDbType.Varchar2, 1, lostrPM}, _
                                       {"V_SERVICIO_EST_QUIMI", OracleDbType.Varchar2, 1, lostrAQ}, _
                                       {"V_SERVICIO_EST_IMAGE", OracleDbType.Varchar2, 1, lostrIS}, _
                                       {"V_NUM_MUESTRA", OracleDbType.Clob, 4000000, pastrNum_Muestra}, _
                                       {"V_COD_MUESTRA", OracleDbType.Clob, 4000000, pastrCod_Muestra}, _
                                       {"V_TIP_MUESTRA", OracleDbType.Clob, 4000000, lostrTip_Muestra}, _
                                       {"V_DET_MUESTRA", OracleDbType.Clob, 4000000, lostrDet_Muestra}, _
                                       {"V_FECHA_ENTREGA", OracleDbType.Varchar2, 10, lostrFecha_Entrega}, _
                                       {"V_TIPO_SOLICITANTE", OracleDbType.Varchar2, 1, lostrTipo_Servicio}, _
                                       {"V_ESTADO", OracleDbType.Varchar2, 2, lostrEstado}, _
                                       {"V_OBSERVACIONES", OracleDbType.Varchar2, 4000, lostrObservacion}, _
                                       {"V_CLASE_PRODUCTO", OracleDbType.Clob, 4000000, lostrClase_Prod}, _
                                       {"V_SERIE_PRODUCTO", OracleDbType.Clob, 4000000, lostrSerie_Prod}, _
                                       {"V_NUMERO_PRODUCTO", OracleDbType.Clob, 4000000, lostrNumero_Prod}, _
                                       {"V_PROYECTO", OracleDbType.Varchar2, 10, lostrProyecto}, _
                                       {"V_AREA", OracleDbType.Varchar2, 5, lostrArea}, _
                                       {"V_USUARIO", OracleDbType.Varchar2, 32, lostrUsuario}}


        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_OPERACION_MANTO_ORDEN"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FS_ESTADO_CHK(ByVal pastrNumero As String, ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_NUMERO", OracleDbType.Varchar2, 8, pastrNumero}, _
                                       {"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_ESTADO_CHK"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    'Public Function FS_Sel_Orden_Detalle(ByVal pastrTipo As String, ByVal pastrNumero As String) As DataTable
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
    '                                   {"VI_NU_NUMERO", OracleDbType.Varchar2, 8, pastrNumero}}
    '    'Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_REP_DET_OT"
    '    Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_TRAER_DATA_MUESTRA"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try

    'End Function

    Public Function P_TRAER_DATA_MUESTRA_1(ByVal pastrTipo As String, ByVal pastrNumero As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Varchar2, 8, pastrNumero}}
        'Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_REP_DET_OT"
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_TRAER_DATA_MUESTRA_1"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    Public Function FS_Sel_Orden_Detalle(ByVal pastrTipo As String, ByVal pastrNumero As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Varchar2, 8, pastrNumero}}
        'Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_REP_DET_OT"
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_TRAER_DATA_MUESTRA"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    Public Function FS_Sel_Orden_Detalle_1(ByVal pastrTipo As String, ByVal pastrNumero As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Varchar2, 8, pastrNumero}}
        'Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_REP_DET_OT"
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_TRAER_DATA_MUESTRA_1"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    Public Function FS_Sel_Orden_Detalle_2(ByVal pastrTipo As String, ByVal pastrNumero As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Varchar2, 8, pastrNumero}}
        'Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_REP_DET_OT"
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_TRAER_DATA_MUESTRA_2"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    Public Function FS_Sel_Orden_Detalle_Muestra(ByVal pastrTipo As String, ByVal pastrNumero As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"V_NUMERO", OracleDbType.Varchar2, 8, pastrNumero}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_TIPO_MUESTRA1"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    Public Function FS_Sel_Reporte_Solicitud(ByVal pastrNumero As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_NUMERO", OracleDbType.Varchar2, 8, pastrNumero}}
        'Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_REP_SO"
        'Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_REPORTE_SOLICITUD"
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_CAB_REP_SO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function



    Public Function FS_Sel_Orden_Trabajo_Reporte(ByVal pastrNumero As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_NUMERO", OracleDbType.Varchar2, 8, pastrNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_REP_OT"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FS_Sel_Orden_Trabajo(ByVal pastrNumero As String, ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_NUMERO", OracleDbType.Varchar2, 8, pastrNumero}, _
                                       {"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_ORDEN_TRABAJO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function



    '*************************************************************************************
    'Descripción: Selecciona los Items de las Ordenes de Trabajo
    'Fecha: 17/05/2007
    '*************************************************************************************
    Public Function FS_Sel_Lista_Trabajo(ByVal pastrUsuario As String, ByVal pastrFechaInicio As String, _
    ByVal pastrFechaFin As String, ByVal pastrEstado As String, ByVal pastrTipoSolicitante As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_USUARIO", OracleDbType.Varchar2, 32, pastrUsuario}, _
                                       {"VI_VA_FECHA_INICIO", OracleDbType.Varchar2, 12, pastrFechaInicio}, _
                                       {"VI_VA_FECHA_FIN", OracleDbType.Varchar2, 12, pastrFechaFin}, _
                                       {"VI_VA_ESTADO", OracleDbType.Varchar2, 15, pastrEstado}, _
                                       {"VI_VA_TIPO_SOLICITANTE", OracleDbType.Varchar2, 1, pastrTipoSolicitante}}
        'gstrEsquema
        Dim lstrProcedimiento As String = "VENTAS.PKG_MUESTRAS_LABORATORIO.P_SEL_ORDEN_X_FECHA_1"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registro de Documento de Pago
    'Fecha: 21/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Doc_Venta(ByVal pastrTipo As String, ByVal pastrSerie As String, _
                                    ByVal pastrNumero As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
                                       {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_DOC_PAGO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registro de Items del Documento de Pago
    'Fecha: 21/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Doc_Venta_Item(ByVal pastrTipo As String, ByVal pastrSerie As String, _
                                          ByVal pastrNumero As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
                                       {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_DOC_PAGO_ITEM"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registro de Items del Documento de Pago
    'Fecha: 21/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Doc_Venta_Item_Imp(ByVal pastrTipo As String, ByVal pastrSerie As String, _
                                              ByVal pastrNumero As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
                                       {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_DOC_PAGO_ITEM_IMP"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registro de Items del Documento de Pago con precios actualizados
    'Fecha: 21/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Doc_Venta_Item_Act(ByVal pastrTipo As String, ByVal pastrSerie As String, _
                                              ByVal pastrNumero As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
                                       {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_DOC_PAGO_ITEM_ACT"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

#Region "    PROCEDIMIENTOS DE ACCESO A LAS ORDENES DE TRABAJO DE LABORATORIO "

    '*************************************************************************************
    'Descripción: Selecciona todos los documentos relacionados con una determinada Orden de Trabajo
    'Fecha: 28/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_DsItem_Ref_DtItem(ByVal pastrTipo_DT As String, _
                                             ByVal pastrNumero_DT As String, _
                                             ByVal pastrCorre_DT As Integer) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO_DOCTRABAJO", OracleDbType.Varchar2, 2, pastrTipo_DT}, _
                                       {"VI_NU_NUMERO_DOCTRABAJO", OracleDbType.Int64, 4, pastrNumero_DT}, _
                                       {"VI_NU_CORRE_DOCTRABAJO", OracleDbType.Int32, 2, pastrCorre_DT}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DSITEM_REF_DTITEM"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function


    '*************************************************************************************
    'Descripción: Autogeneración de Codigos de las Ordenes de Trabajo y Solicitudes
    'Fecha: 28/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_DocPago_xCli_xTipo(ByVal pastrTipo As String, _
                                              ByVal pastrCodCliente As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_VA_IDCLIENTE", OracleDbType.Varchar2, 11, pastrCodCliente}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_DOC_PAGO_xCLI_xTIPO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Autogeneración de Codigos de las Ordenes de Trabajo y Solicitudes
    'Fecha: 28/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_Pedido_xClie(ByVal pastrCodCliente As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_IDCLIENTE", OracleDbType.Varchar2, 11, pastrCodCliente}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_PEDIDO_xCLI"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Autogeneración de Codigos de las Ordenes de Trabajo y Solicitudes
    'Fecha: 21/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Gen_IDDocTrabajo(ByVal pastrTipo As String) As Integer
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 10, pastrTipo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_GENERA_NUMERO"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    Public Function FS_Sel_DocTrabajo(ByVal pastrTipo As String, ByVal pastrNumero As Integer) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 10, pastrTipo}, _
                                        {"VI_NU_NUMERO", OracleDbType.Int32, 5, pastrNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DOC_TRABAJO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    Public Function FS_Obt_Numero_DT(ByVal pastrTipo As String, ByVal pastrNro_Doc As String) As Integer
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 10, pastrTipo}, _
                                       {"VI_VA_NRO_DOC", OracleDbType.Varchar2, 25, pastrNro_Doc}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_OBT_OT_Numero"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    Public Function FS_Sel_DocTrabajo_xNroDoc(ByVal pastrNro_Doc As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_NRO_DOC", OracleDbType.Varchar2, 25, pastrNro_Doc}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_SOL_TRABAJO_xNroDoc"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Selecciona los Items de las Ordenes de Trabajo
    'Fecha: 17/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_SolTrabajo() As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_NIVEL_VISTA", OracleDbType.Int16, 5, 1}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DOCS_TRABAJO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Selecciona los Items de las Ordenes de Trabajo
    'Fecha: 29/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_SolTrabajo_Items() As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_NIVEL_VISTA", OracleDbType.Int16, 5, 1}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_ALL_DOC_TRABAJO_ITEM"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Selecciona los Items de las Ordenes de Trabajo
    'Fecha: 17/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_DocsTrabajo(ByVal pastrNivelVista As Integer, ByVal pastrFechaInicio As String, ByVal pastrFechaFin As String, ByVal pastrEstado As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_NIVEL_VISTA", OracleDbType.Int16, 5, pastrNivelVista}, _
                                       {"VI_VA_FECHA_INICIO", OracleDbType.Varchar2, 12, pastrFechaInicio}, _
                                       {"VI_VA_FECHA_FIN", OracleDbType.Varchar2, 12, pastrFechaFin}, _
                                       {"VI_VA_ESTADO", OracleDbType.Varchar2, 15, pastrEstado}}
        'gstrEsquema
        Dim lstrProcedimiento As String = "VENTAS.PKG_MUESTRAS_LABORATORIO.P_SEL_DOCS_TRABAJO_XFECEST"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Selecciona los Items de las Ordenes de Trabajo
    'Fecha: 19/06/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_DocsTrabajo_WithItems(ByVal pastrNivelVista As Integer, ByVal pastrFechaInicio As String, ByVal pastrFechaFin As String, ByVal pastrEstado As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_NIVEL_VISTA", OracleDbType.Int16, 5, pastrNivelVista}, _
                                       {"VI_VA_FECHA_INICIO", OracleDbType.Varchar2, 12, pastrFechaInicio}, _
                                       {"VI_VA_FECHA_FIN", OracleDbType.Varchar2, 12, pastrFechaFin}, _
                                       {"VI_VA_ESTADO", OracleDbType.Varchar2, 15, pastrEstado}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DTS_conITEMS_XFECEST"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    'AGREGADO EN AGOSTO 
    Public Overloads Function FS_Sel_DocsTrabajo_Items(ByVal pastrFechaInicio As String, ByVal pastrFechaFin As String, ByVal pastrEstado As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_FECHA_INICIO", OracleDbType.Varchar2, 12, pastrFechaInicio}, _
                                       {"VI_VA_FECHA_FIN", OracleDbType.Varchar2, 12, pastrFechaFin}, _
                                       {"VI_VA_ESTADO", OracleDbType.Varchar2, 15, pastrEstado}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DTS_ITEMS_XFECEST"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Selecciona los Items de las Ordenes de Trabajo
    'Fecha: 17/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Overloads Function FS_Sel_DocsTrabajo_Items(ByVal pastrNivelVista As Integer) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_NIVEL_VISTA", OracleDbType.Int16, 5, pastrNivelVista}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_ALL_DOC_TRABAJO_ITEM"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Selecciona los Items de las Ordenes de Trabajo
    'Fecha: 16/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_DocTrabajo_Items(ByVal pastrTipo As String, _
                                            ByVal pastrNumero As Integer) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int32, 10, pastrNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.p_sel_doc_trabajo_item"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    Public Function FS_Sel_OrdenTrabajo(ByVal pastrTipo As String, _
                                        ByVal pastrNumero As Integer) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int32, 10, pastrNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_ORDEN_TRABAJO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Búsqueda de los Servicios que brinda Laboratorio
    'Fecha: 16/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_Servicios_Laboratorio() As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_SERVICIOS_LABORATORIO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de los Servicios por Laboratorio
    'Fecha: 06/06/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_Servicios_x_Laboratorio(ByVal IdLaboratorio As Integer) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_IDLABORATORIO", OracleDbType.Int32, 4, IdLaboratorio}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_SERVICIOS_x_LABORATORIO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de los Servicios por Laboratorio Externo
    'Fecha: 06/08/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_Servicios_x_LabExterno(ByVal IdLabExterno As Integer) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_IDLABEXTERNO", OracleDbType.Int32, 4, IdLabExterno}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_SERVICIOS_x_LABEXTERNO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Búsqueda de los Laboratorios por Doc. de Trabajo
    'Fecha: 13/06/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_Laboratorios_x_Doc_Trabajo(ByVal Tipo_Doc_Trabajo As String, _
                                                      ByVal Numero_Doc_Trabajo As Integer) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, Tipo_Doc_Trabajo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int32, 2, Numero_Doc_Trabajo}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_LABORAT_X_DOC_TRABAJO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Búsqueda de los Laboratorios por Sol. de Trabajo
    'Fecha: 13/06/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_Laboratorios_x_Sol_Trabajo(ByVal Nro_Doc_Completo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_NRO_DOC", OracleDbType.Varchar2, 25, Nro_Doc_Completo}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_LABORAT_X_SOL_TRABAJO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Búsqueda de las Muestras por Doc. de Trabajo
    'Fecha: 14/06/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_Muestras_x_Doc_Trabajo(ByVal Tipo_Doc_Trabajo As String, _
                                                  ByVal Numero_Doc_Trabajo As Integer) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, Tipo_Doc_Trabajo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int32, 2, Numero_Doc_Trabajo}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_MUESTRAS_X_DOC_TRABAJO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Búsqueda de las Muestras por Sol. de Trabajo
    'Fecha: 14/06/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_Muestras_x_Sol_Trabajo(ByVal Nro_Doc As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_NRO_DOC", OracleDbType.Varchar2, 25, Nro_Doc}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_MUESTRAS_X_SOL_TRABAJO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Búsqueda de las Muestras por Doc. de Trabajo
    'Fecha: 14/06/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_Muestra_Items_x_DocTrabajo(ByVal Tipo_Doc_Trabajo As String, _
                                                      ByVal Numero_Doc_Trabajo As Integer, _
                                                      ByVal Id_Muestra As Integer) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, Tipo_Doc_Trabajo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int32, 2, Numero_Doc_Trabajo}, _
                                       {"VI_NU_MUESTRA", OracleDbType.Int32, 3, Id_Muestra}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_MUESTRA_ITEMS_X_DT"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de las Muestras por Doc. de Trabajo
    'Fecha: 14/06/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_Muestra_Items_x_SolTrabajo(ByVal Nro_Doc As String, _
                                                      ByVal Id_Muestra As Integer) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_NRO_DOC", OracleDbType.Varchar2, 25, Nro_Doc}, _
                                       {"VI_NU_MUESTRA", OracleDbType.Int32, 3, Id_Muestra}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_MUESTRA_ITEMS_X_ST"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de los Laboratorios
    'Fecha: 16/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_Laboratorios() As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_LABORATORIOS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Búsqueda de las Direcciones
    'Fecha: 07/06/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_Direcciones(Optional ByVal Id_Direccion As Integer = -1) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {}
        Dim Parametros1(,) As Object = {{"VI_NU_IDAREA", OracleDbType.Varchar2, 20, Id_Direccion}}
        Dim lstrProcedimiento As String
        If Id_Direccion = -1 Then
            lstrProcedimiento = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DIRECCIONES"
        Else
            Parametros = Parametros1
            lstrProcedimiento = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DIRECCION"
        End If
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    Public Function FS_Sel_Direcciones_xNro_Docs(ByVal pastrCadNroDoc As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CAD_NRO_DOC", OracleDbType.Varchar2, 5000, pastrCadNroDoc}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DIRECCIONES_xST"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FS_Sel_NroSolicitudes(ByVal IdUsuario As String, Optional ByVal IdDireccion As Integer = -1, Optional ByVal IdProyecto As Integer = -1) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_IDUSUARIO", OracleDbType.Varchar2, 32, IdUsuario}, _
                                        {"VI_NU_IDDIRECCION", OracleDbType.Int32, 5, IdDireccion}, _
                                        {"VI_VA_IDPROYECTO", OracleDbType.Int32, 5, IdProyecto}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_NRO_SOLS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    Public Function FS_Sel_NroSolicitudes_Pasadas(ByVal IdUsuario As String, Optional ByVal IdDireccion As Integer = -1, Optional ByVal IdProyecto As Integer = -1) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_IDUSUARIO", OracleDbType.Varchar2, 32, IdUsuario}, _
                                       {"VI_NU_IDDIRECCION", OracleDbType.Int32, 5, IdDireccion}, _
                                       {"VI_VA_IDPROYECTO", OracleDbType.Int32, 5, IdProyecto}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_NRO_SOLS_PAS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    Public Function FS_Sel_Ruta(ByVal Nombre_Ruta As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_NOMB_CARPETA", OracleDbType.Varchar2, 20, Nombre_Ruta}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_OBT_RUTA_DESTINO"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de los Proyectos
    'Fecha: 07/06/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_Proyectos(ByVal pastrUsuario As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_USUARIO", OracleDbType.Varchar2, 20, pastrUsuario}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_PROYECTOS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    Public Function FS_Sel_Proyectos_xNro_Docs(ByVal pastrCadNroDoc As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CAD_NRO_DOC", OracleDbType.Varchar2, 5000, pastrCadNroDoc}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_PROYECTOS_xST"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FS_Sel_Responsable_Proyecto(ByVal pastrId_Proyecto As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_IDPROYECTO", OracleDbType.Int32, 6, pastrId_Proyecto}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_RESP_PROYECTOS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda del Laboratorio segun su servicio
    'Fecha: 22/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_Laboratorio_x_Servicio(ByVal pastrClase As String, ByVal pastrSerie As String, ByVal pastrNumero As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CLASE_PRODUCTO", OracleDbType.Varchar2, 2, pastrClase}, _
                                       {"VI_VA_SERIE_PRODUCTO", OracleDbType.Varchar2, 5, pastrSerie}, _
                                       {"VI_VA_NUMERO_PRODUCTO", OracleDbType.Varchar2, 10, pastrNumero}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_LABORATORIO_X_SERVICIO"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Carga los tipos de Documentos que ha manejado el Cliente
    'Fecha: 22/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_TipoDocsPed_x_Cliente(ByVal pastrIDCliente As String, ByVal pastrFechaMenor As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO_CLIENTE", OracleDbType.Varchar2, 11, pastrIDCliente}, _
                                        {"VI_VA_FECHA_MENOR", OracleDbType.Varchar2, 20, pastrFechaMenor}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_TIPODOCSPED_X_CLIENTE"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    Public Function FS_Sel_NumDocsPed_x_Cliente(ByVal pastrTipoDoc As String, ByVal pastrIDCliente As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO_CLIENTE", OracleDbType.Varchar2, 11, pastrIDCliente}, _
                                       {"VI_VA_TIPO_DOCUMENTO", OracleDbType.Varchar2, 5, pastrTipoDoc}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_NUMDOCSPED_X_CLIENTE"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Selecciona los Posibles Responsables deacuerdo a un Servicio
    'Fecha: 29/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Sel_Responsables_Laboratorio() As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_USUARIOS_LABORATORIO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Ingresa un Doc de Trabajo
    'Fecha: 22/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Ins_Doc_Trabajo(ByVal pastrTipo As String, _
                                       ByVal pastrNumero As Integer, _
                                       ByVal pastrAnho As Integer, _
                                       ByVal pastrFechaDocumento As String, _
                                       ByVal pastrFechaRecepcion As String, _
                                       ByVal pastrFechaAtencion As String, _
                                       ByVal pastrFechaEntrega As String, _
                                       ByVal pastrObservaciones As String, _
                                       ByVal pastrTipoSolicitante As String, _
                                       ByVal pastrNroMuestras As Integer, _
                                       ByVal pastrUsuario As String, _
                                       Optional ByVal pastrIDCliente As String = "", _
                                       Optional ByVal pastrDireccion As String = "", _
                                       Optional ByVal pastrProyecto As String = "", _
                                       Optional ByVal pastrLabExterno As String = "") As String


        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int32, 6, pastrNumero}, _
                                       {"VI_NU_ANHO", OracleDbType.Int32, 4, pastrAnho}, _
                                       {"VI_VA_FECHA_DOCUMENTO", OracleDbType.Varchar2, 30, pastrFechaDocumento}, _
                                       {"VI_VA_FECHA_RECEPCION", OracleDbType.Varchar2, 30, pastrFechaRecepcion}, _
                                       {"VI_VA_FECHA_ATENCION", OracleDbType.Varchar2, 30, pastrFechaAtencion}, _
                                       {"VI_VA_FECHA_ENTREGA", OracleDbType.Varchar2, 30, pastrFechaEntrega}, _
                                       {"VI_VA_OBSERVACIONES", OracleDbType.Varchar2, 4000, pastrObservaciones}, _
                                       {"VI_VA_TIPO_SOLICITANTE", OracleDbType.Varchar2, 2, pastrTipoSolicitante}, _
                                       {"VI_NU_NRO_MUESTRAS", OracleDbType.Int32, 5, pastrNroMuestras}, _
                                       {"VI_VA_CODIGO_CLIENTE", OracleDbType.Varchar2, 11, pastrIDCliente}, _
                                       {"VI_NU_CODIGO_DIRECCION", OracleDbType.Varchar2, 8, pastrDireccion}, _
                                       {"VI_NU_CODIGO_PROYECTO", OracleDbType.Varchar2, 8, pastrProyecto}, _
                                       {"VI_VA_CODIGO_LABEXTERNO", OracleDbType.Varchar2, 11, pastrLabExterno}, _
                                       {"VI_VA_USUARIO_REGISTRO", OracleDbType.Varchar2, 11, pastrUsuario}}



        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_INS_DOC_TRABAJO"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Ingresa el Detalle del Doc de Trabajo
    'Fecha: 22/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Ins_Doc_Trabajo_Items(ByVal pastrTipo As String, _
                                             ByVal pastrNumero As Integer, _
                                             ByVal pastrCorrelativo As Integer, _
                                             ByVal pastrClaseProducto As String, _
                                             ByVal pastrSerieProducto As String, _
                                             ByVal pastrNumeroProducto As String, _
                                             ByVal pastrDetalle As String, _
                                             ByVal pastrCantidad As Integer, _
                                             ByVal pastrResponsable As String, _
                                             ByVal pastrFechaInicio As String, _
                                             ByVal pastrFechaFin As String, _
                                             ByVal pastrEstado As String) As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int32, 4, pastrNumero}, _
                                       {"VI_NU_CORRELATIVO", OracleDbType.Int32, 4, pastrCorrelativo}, _
                                       {"VI_VA_CLASE_PRODUCTO", OracleDbType.Varchar2, 2, pastrClaseProducto}, _
                                       {"VI_VA_SERIE_PRODUCTO", OracleDbType.Varchar2, 3, pastrSerieProducto}, _
                                       {"VI_VA_NUMERO_PRODUCTO", OracleDbType.Varchar2, 16, pastrNumeroProducto}, _
                                       {"VI_VA_DETALLE", OracleDbType.Varchar2, 200, pastrDetalle}, _
                                       {"VI_NU_CANTIDAD", OracleDbType.Int32, 4, pastrCantidad}, _
                                       {"VI_VA_RESPONSABLE", OracleDbType.Varchar2, 20, pastrResponsable}, _
                                       {"VI_VA_FECHA_INICIO", OracleDbType.Varchar2, 30, pastrFechaInicio}, _
                                       {"VI_VA_FECHA_FIN", OracleDbType.Varchar2, 30, pastrFechaFin}, _
                                       {"VI_VA_ESTADO", OracleDbType.Varchar2, 15, pastrEstado}}


        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_INS_DOC_TRABAJO_ITEM"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Actualizar un Doc de Trabajo
    'Fecha: 07/06/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Ins_Muestras(ByVal pastrCodigo_muestra As String, _
                                    Optional ByVal pastrClase As String = "", _
                                    Optional ByVal pastrMatRecipiente As String = "", _
                                    Optional ByVal pastrIndPreservante As Boolean = False, _
                                    Optional ByVal pastrPreservante As String = "", _
                                    Optional ByVal pastrObservaciones As String = "") As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_COD_MUESTRA", OracleDbType.Varchar2, 11, pastrCodigo_muestra}, _
                                       {"VI_VA_CLASE", OracleDbType.Varchar2, 2, pastrClase}, _
                                       {"VI_VA_MAT_RECIPIENTE", OracleDbType.Varchar2, 10, pastrMatRecipiente}, _
                                       {"VI_CH_IND_PRESERVANTE", OracleDbType.Int16, 1, pastrIndPreservante}, _
                                       {"VI_VA_PRESERVANTE", OracleDbType.Varchar2, 64, pastrPreservante}, _
                                       {"VI_VA_OBSERVACIONES", OracleDbType.Varchar2, 4000, pastrObservaciones}}



        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_INS_MUESTRA"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    Public Function FS_Ins_Detalle_Muestra(ByVal pastrTipo As String, _
                                           ByVal pastrNumero As Integer, _
                                           ByVal pastrCorrelativo As Integer, _
                                           ByVal pastrCodigo_Interno_muestra As Integer, _
                                           ByVal pastrDetalle As String, _
                                           ByVal pastrCantidad As String, _
                                           ByVal pastrEstado As String) As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 11, pastrTipo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int32, 4, pastrNumero}, _
                                       {"VI_NU_CORRELATIVO", OracleDbType.Int32, 2, pastrCorrelativo}, _
                                       {"VI_NU_CODINT_MUESTRA", OracleDbType.Int32, 3, pastrCodigo_Interno_muestra}, _
                                       {"VI_VA_DETALLE", OracleDbType.Varchar2, 1000, pastrDetalle}, _
                                       {"VI_NU_CANTIDAD", OracleDbType.Decimal, 10, pastrCantidad}, _
                                       {"VI_VA_ESTADO", OracleDbType.Varchar2, 20, pastrEstado}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_INS_DT_MUESTRA_ITEM"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Ingresa una Orden de Trabajo
    'Fecha: 19/06/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Ins_Orden_Trabajo(ByVal pastrTipo As String, _
                                         ByVal pastrNumero As Integer, _
                                         ByVal pastrAnho As Integer, _
                                         ByVal pastrFechaDocumento As String, _
                                         ByVal pastrFechaRecepcion As String, _
                                         ByVal pastrFechaAtencion As String, _
                                         ByVal pastrFechaEntrega As String, _
                                         ByVal pastrObservaciones As String, _
                                         ByVal pastrUsuario As String, _
                                         ByVal pastrIDCliente As String, _
                                         ByVal pastrCorrelativos As String, _
                                         ByVal pastrClaseProds As String, _
                                         ByVal pastrSerieProds As String, _
                                         ByVal pastrNroProds As String, _
                                         ByVal pastrDetalles As String, _
                                         ByVal pastrCantidads As String, _
                                         ByVal pastrResponsables As String, _
                                         ByVal pastrFechaInicios As String, _
                                         ByVal pastrFechaFins As String, _
                                         ByVal pastrEstados As String, _
                                         ByVal pastrDocRefs As String, _
                                         ByVal pastrTitulo As String, _
                                         ByVal pastrDescripcion As String, _
                                         ByVal pastrArchivo As String) As String


        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int32, 6, pastrNumero}, _
                                       {"VI_NU_ANHO", OracleDbType.Int32, 4, pastrAnho}, _
                                       {"VI_VA_FECHA_DOCUMENTO", OracleDbType.Varchar2, 30, pastrFechaDocumento}, _
                                       {"VI_VA_FECHA_RECEPCION", OracleDbType.Varchar2, 30, pastrFechaRecepcion}, _
                                       {"VI_VA_FECHA_ATENCION", OracleDbType.Varchar2, 30, pastrFechaAtencion}, _
                                       {"VI_VA_FECHA_ENTREGA", OracleDbType.Varchar2, 30, pastrFechaEntrega}, _
                                       {"VI_VA_OBSERVACIONES", OracleDbType.Varchar2, 4000, pastrObservaciones}, _
                                       {"VI_VA_CODIGO_CLIENTE", OracleDbType.Varchar2, 11, pastrIDCliente}, _
                                       {"VI_VA_USUARIO_REGISTRO", OracleDbType.Varchar2, 20, pastrUsuario}, _
                                       {"VI_CL_CORRELATIVO", OracleDbType.Long, 400000, pastrCorrelativos}, _
                                       {"VI_CL_CLASE_PRODUCTO", OracleDbType.Long, 900, pastrClaseProds}, _
                                       {"VI_CL_SERIE_PRODUCTO", OracleDbType.Long, 1000, pastrSerieProds}, _
                                       {"VI_CL_NUMERO_PRODUCTO", OracleDbType.Long, 1200, pastrNroProds}, _
                                       {"VI_CL_DETALLE", OracleDbType.Long, 21000, pastrDetalles}, _
                                       {"VI_CL_CANTIDAD", OracleDbType.Long, 1100, pastrCantidads}, _
                                       {"VI_CL_RESPONSABLE", OracleDbType.Long, 3100, pastrResponsables}, _
                                       {"VI_CL_FECHA_INICIO", OracleDbType.Long, 3900, pastrFechaInicios}, _
                                       {"VI_CL_FECHA_FIN", OracleDbType.Long, 3900, pastrFechaFins}, _
                                       {"VI_CL_ESTADO", OracleDbType.Long, 2200, pastrEstados}, _
                                       {"VI_CL_DOC_REF", OracleDbType.Long, 3500, pastrDocRefs}, _
                                       {"VI_CL_TITULO", OracleDbType.Long, 40000, pastrTitulo}, _
                                       {"VI_CL_DESCRIPCION", OracleDbType.Long, 40000, pastrDescripcion}, _
                                       {"VI_CL_ARCHIVO", OracleDbType.Long, 40000, pastrArchivo}}


        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_INS_ORDEN_TRABAJO"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Ingresa una Solicitud de Trabajo
    'Fecha: 20/06/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Ins_Solicitud_Trabajo(ByVal pastrTipo As String, _
                                             ByVal pastrNroDoc As String, _
                                             ByVal pastrAnho As Integer, _
                                             ByVal pastrFechaDocumento As String, _
                                             ByVal pastrFechaEspAprov As String, _
                                             ByVal pastrFechaAprovResp As String, _
                                             ByVal pastrFechaAprovDir As String, _
                                             ByVal pastrFechaAprovDirLab As String, _
                                             ByVal pastrObservaciones_S As String, _
                                             ByVal pastrNroMuestras As Integer, _
                                             ByVal pastrIDDireccion As String, _
                                             ByVal pastrIDProyecto As String, _
                                             ByVal pastrUsuario As String, _
                                             ByVal pastrTipoDoc_ID As Integer, _
                                             ByVal pastrFormato As Integer, _
                                             ByVal pastrProveido As String, _
                                             ByVal pastrCorrelativos As String, _
                                             ByVal pastrClaseProds As String, _
                                             ByVal pastrSerieProds As String, _
                                             ByVal pastrNroProds As String, _
                                             ByVal pastrDetalle_DSs As String, _
                                             ByVal pastrCantidad_DSs As String, _
                                             ByVal pastrResponsables As String, _
                                             ByVal pastrFechaInicios As String, _
                                             ByVal pastrFechaFins As String, _
                                             ByVal pastrEstado_DSs As String, _
                                             ByVal pastrRealizadoEn As String, _
                                             ByVal pastrIdLabExt As String, _
                                             ByVal pastrIDMuestras As String, _
                                             ByVal pastrClase_Ms As String, _
                                             ByVal pastrMatRecipientes As String, _
                                             ByVal pastrIndPreservantes As String, _
                                             ByVal pastrPreservantes As String, _
                                             ByVal pastrObservaciones_Ms As String, _
                                             ByVal pastrDetalle_DMs As String, _
                                             ByVal pastrCantidad_DMs As String, _
                                             ByVal pastrEstado_DMs As String) As String


        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_VA_NRO_DOC", OracleDbType.Varchar2, 25, pastrNroDoc}, _
                                       {"VI_NU_ANHO", OracleDbType.Int32, 4, pastrAnho}, _
                                       {"VI_VA_FECHA_DOCUMENTO", OracleDbType.Varchar2, 30, pastrFechaDocumento}, _
                                       {"VI_VA_FECHA_ESPE_APRO", OracleDbType.Varchar2, 30, pastrFechaEspAprov}, _
                                       {"VI_VA_FECHA_APRO_RESP", OracleDbType.Varchar2, 30, pastrFechaAprovResp}, _
                                       {"VI_VA_FECHA_APRO_DIR", OracleDbType.Varchar2, 30, pastrFechaAprovDir}, _
                                       {"VI_VA_FECHA_APRO_DLAB", OracleDbType.Varchar2, 30, pastrFechaAprovDirLab}, _
                                       {"VI_VA_OBSERVACIONES_S", OracleDbType.Varchar2, 4000, pastrObservaciones_S}, _
                                       {"VI_NU_NRO_MUESTRAS", OracleDbType.Int32, 4, pastrNroMuestras}, _
                                       {"VI_NU_CODIGO_DIRECCION", OracleDbType.Int32, 8, pastrIDDireccion}, _
                                       {"VI_NU_CODIGO_PROYECTO", OracleDbType.Varchar2, 8, pastrIDProyecto}, _
                                       {"VI_VA_USUARIO_REGISTRO", OracleDbType.Varchar2, 20, pastrUsuario}, _
                                       {"VI_NU_TIPODOC_ID", OracleDbType.Int32, 4, pastrTipoDoc_ID}, _
                                       {"VI_NU_FORMATO", OracleDbType.Int16, 3, pastrFormato}, _
                                       {"VI_VA_PROVEIDO", OracleDbType.Varchar2, 1000, pastrProveido}, _
                                       {"VI_CL_CORRELATIVO", OracleDbType.Long, 2000000, pastrCorrelativos}, _
                                       {"VI_CL_CLASE_PRODUCTO", OracleDbType.Long, 45000, pastrClaseProds}, _
                                       {"VI_CL_SERIE_PRODUCTO", OracleDbType.Long, 5000, pastrSerieProds}, _
                                       {"VI_CL_NUMERO_PRODUCTO", OracleDbType.Long, 6000, pastrNroProds}, _
                                       {"VI_CL_DETALLE_DS", OracleDbType.Long, 105000, pastrDetalle_DSs}, _
                                       {"VI_CL_CANTIDAD_DS", OracleDbType.Long, 5500, pastrCantidad_DSs}, _
                                       {"VI_CL_RESPONSABLE", OracleDbType.Long, 15500, pastrResponsables}, _
                                       {"VI_CL_FECHA_INICIO", OracleDbType.Long, 20000, pastrFechaInicios}, _
                                       {"VI_CL_FECHA_FIN", OracleDbType.Long, 20000, pastrFechaFins}, _
                                       {"VI_CL_ESTADO_DS", OracleDbType.Long, 11000, pastrEstado_DSs}, _
                                       {"VI_CL_REALIZADO_EN", OracleDbType.Long, 1000, pastrRealizadoEn}, _
                                       {"VI_CL_COD_LABEXT", OracleDbType.Long, 11000, pastrIdLabExt}, _
                                       {"VI_CL_COD_MUESTRA", OracleDbType.Long, 2000000, pastrIDMuestras}, _
                                       {"VI_CL_CLASE_M", OracleDbType.Long, 2000000, pastrClase_Ms}, _
                                       {"VI_CL_MAT_RECIPIENTE", OracleDbType.Long, 2000000, pastrMatRecipientes}, _
                                       {"VI_CL_IND_PRESERVANTE", OracleDbType.Long, 2000000, pastrIndPreservantes}, _
                                       {"VI_CL_PRESERVANTE", OracleDbType.Long, 2000000, pastrPreservantes}, _
                                       {"VI_CL_OBSERVACIONES_M", OracleDbType.Long, 2000000, pastrObservaciones_Ms}, _
                                       {"VI_CL_DETALLE_DM", OracleDbType.Long, 2000000, pastrDetalle_DMs}, _
                                       {"VI_CL_CANTIDAD_DM", OracleDbType.Long, 2000000, pastrCantidad_DMs}, _
                                       {"VI_CL_ESTADO_DM", OracleDbType.Long, 2000000, pastrEstado_DMs}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_INS_SOLICITUD_TRABAJO"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Actualizar un Doc de Trabajo
    'Fecha: 22/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Upd_Doc_Trabajo(ByVal pastrTipo As String, _
                                       ByVal pastrNumero As Integer, _
                                       ByVal pastrFechaDocumento As String, _
                                       ByVal pastrFechaRecepcion As String, _
                                       ByVal pastrFechaAtencion As String, _
                                       ByVal pastrFechaEntrega As String, _
                                       ByVal pastrObservaciones As String, _
                                       ByVal pastrTipoSolicitante As String, _
                                       ByVal pastrNroMuestras As Integer, _
                                       Optional ByVal pastrIDCliente As String = "", _
                                       Optional ByVal pastrDireccion As String = "", _
                                       Optional ByVal pastrProyecto As String = "", _
                                       Optional ByVal pastrLabExterno As String = "") As String


        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int32, 4, pastrNumero}, _
                                       {"VI_VA_FECHA_DOCUMENTO", OracleDbType.Varchar2, 30, pastrFechaDocumento}, _
                                       {"VI_VA_FECHA_RECEPCION", OracleDbType.Varchar2, 30, pastrFechaRecepcion}, _
                                       {"VI_VA_FECHA_ATENCION", OracleDbType.Varchar2, 30, pastrFechaAtencion}, _
                                       {"VI_VA_FECHA_ENTREGA", OracleDbType.Varchar2, 30, pastrFechaEntrega}, _
                                       {"VI_VA_OBSERVACIONES", OracleDbType.Varchar2, 4000, pastrObservaciones}, _
                                       {"VI_VA_TIPO_SOLICITANTE", OracleDbType.Varchar2, 2, pastrTipoSolicitante}, _
                                       {"VI_NU_NRO_MUESTRAS", OracleDbType.Int32, 5, pastrNroMuestras}, _
                                       {"VI_VA_CODIGO_CLIENTE", OracleDbType.Varchar2, 11, pastrIDCliente}, _
                                       {"VI_NU_CODIGO_DIRECCION", OracleDbType.Varchar2, 6, pastrDireccion}, _
                                       {"VI_NU_CODIGO_PROYECTO", OracleDbType.Varchar2, 6, pastrProyecto}, _
                                       {"VI_VA_CODIGO_LABEXTERNO", OracleDbType.Varchar2, 11, pastrLabExterno}}



        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_UPD_DOC_TRABAJO"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Actualizar el Detalle del Doc de Trabajo
    'Fecha: 22/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Upd_Doc_Trabajo_Items(ByVal pastrTipo As String, _
                                             ByVal pastrNumero As Integer, _
                                             ByVal pastrCorrelativo As Integer, _
                                             ByVal pastrClaseProducto As String, _
                                             ByVal pastrSerieProducto As String, _
                                             ByVal pastrNumeroProducto As String, _
                                             ByVal pastrDetalle As String, _
                                             ByVal pastrCantidad As Integer, _
                                             ByVal pastrResponsable As String, _
                                             ByVal pastrFechaInicio As String, _
                                             ByVal pastrFechaFin As String, _
                                             ByVal pastrEstado As String) As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int32, 4, pastrNumero}, _
                                       {"VI_NU_CORRELATIVO", OracleDbType.Int32, 4, pastrCorrelativo}, _
                                       {"VI_VA_CLASE_PRODUCTO", OracleDbType.Varchar2, 2, pastrClaseProducto}, _
                                       {"VI_VA_SERIE_PRODUCTO", OracleDbType.Varchar2, 3, pastrSerieProducto}, _
                                       {"VI_VA_NUMERO_PRODUCTO", OracleDbType.Varchar2, 16, pastrNumeroProducto}, _
                                       {"VI_VA_DETALLE", OracleDbType.Varchar2, 200, pastrDetalle}, _
                                       {"VI_NU_CANTIDAD", OracleDbType.Int32, 4, pastrCantidad}, _
                                       {"VI_VA_RESPONSABLE", OracleDbType.Varchar2, 20, pastrResponsable}, _
                                       {"VI_VA_FECHA_INICIO", OracleDbType.Varchar2, 30, pastrFechaInicio}, _
                                       {"VI_VA_FECHA_FIN", OracleDbType.Varchar2, 30, pastrFechaFin}, _
                                       {"VI_VA_ESTADO", OracleDbType.Varchar2, 15, pastrEstado}}




        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_UPD_DOC_TRABAJO_ITEM"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Actualizar un Doc de Trabajo
    'Fecha: 07/06/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Upd_Muestras(ByVal pastrCodigo_Interno As Integer, _
                                    ByVal pastrCodigo_muestra As String, _
                                    ByVal pastrClase As String, _
                                    ByVal pastrMatRecipiente As String, _
                                    ByVal pastrIndPreservante As Boolean, _
                                    ByVal pastrPreservante As String, _
                                    ByVal pastrObservaciones As String) As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_CODINT_MUESTRA", OracleDbType.Int32, 3, pastrCodigo_Interno}, _
                                       {"VI_VA_COD_MUESTRA", OracleDbType.Varchar2, 11, pastrCodigo_muestra}, _
                                       {"VI_VA_CLASE", OracleDbType.Varchar2, 2, pastrClase}, _
                                       {"VI_VA_MAT_RECIPIENTE", OracleDbType.Varchar2, 10, pastrMatRecipiente}, _
                                       {"VI_CH_IND_PRESERVANTE", OracleDbType.Int16, 1, pastrIndPreservante}, _
                                       {"VI_VA_PRESERVANTE", OracleDbType.Varchar2, 64, pastrPreservante}, _
                                       {"VI_VA_OBSERVACIONES", OracleDbType.Varchar2, 4000, pastrObservaciones}}



        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_UPD_MUESTRA"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    Public Function FS_Upd_Orden_Trabajo(ByVal pastrTipo As String, _
                                         ByVal pastrNroDoc As String, _
                                         ByVal pastrFechaDocumento As String, _
                                         ByVal pastrFechaRecepcion As String, _
                                         ByVal pastrFechaAtencion As String, _
                                         ByVal pastrFechaEntrega As String, _
                                         ByVal pastrObservaciones As String, _
                                         ByVal pastrIDCliente As String, _
                                         ByVal pastrInsCorrelativos As String, _
                                         ByVal pastrInsClaseProds As String, _
                                         ByVal pastrInsSerieProds As String, _
                                         ByVal pastrInsNroProds As String, _
                                         ByVal pastrInsDetalles As String, _
                                         ByVal pastrInsCantidads As String, _
                                         ByVal pastrInsResponsables As String, _
                                         ByVal pastrInsFechaInicios As String, _
                                         ByVal pastrInsFechaFins As String, _
                                         ByVal pastrInsEstados As String, _
                                         ByVal pastrInsDocRefs As String, _
                                         ByVal pastrUpdCorrelativos As String, _
                                         ByVal pastrUpdClaseProds As String, _
                                         ByVal pastrUpdSerieProds As String, _
                                         ByVal pastrUpdNroProds As String, _
                                         ByVal pastrUpdDetalles As String, _
                                         ByVal pastrUpdCantidads As String, _
                                         ByVal pastrUpdResponsables As String, _
                                         ByVal pastrUpdFechaInicios As String, _
                                         ByVal pastrUpdFechaFins As String, _
                                         ByVal pastrUpdEstados As String, _
                                         ByVal pastrDelCorrelativos As String, _
                                         ByVal pastrDelDocRefs As String, _
                                         ByVal pastrTitulo As String, _
                                         ByVal pastrDescripcion As String, _
                                         ByVal pastrArchivo As String) As String


        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_VA_NRO_DOC", OracleDbType.Int32, 6, pastrNroDoc}, _
                                       {"VI_VA_FECHA_DOCUMENTO", OracleDbType.Varchar2, 30, pastrFechaDocumento}, _
                                       {"VI_VA_FECHA_RECEPCION", OracleDbType.Varchar2, 30, pastrFechaRecepcion}, _
                                       {"VI_VA_FECHA_ATENCION", OracleDbType.Varchar2, 30, pastrFechaAtencion}, _
                                       {"VI_VA_FECHA_ENTREGA", OracleDbType.Varchar2, 30, pastrFechaEntrega}, _
                                       {"VI_VA_OBSERVACIONES", OracleDbType.Varchar2, 4000, pastrObservaciones}, _
                                       {"VI_VA_CODIGO_CLIENTE", OracleDbType.Varchar2, 11, pastrIDCliente}, _
                                       {"VI_CL_INS_CORREL", OracleDbType.Long, 400000, pastrInsCorrelativos}, _
                                       {"VI_CL_INS_CLS_PROD", OracleDbType.Long, 900, pastrInsClaseProds}, _
                                       {"VI_CL_INS_SER_PROD", OracleDbType.Long, 1000, pastrInsSerieProds}, _
                                       {"VI_CL_INS_NRO_PROD", OracleDbType.Long, 1200, pastrInsNroProds}, _
                                       {"VI_CL_INS_DETALLE", OracleDbType.Long, 21000, pastrInsDetalles}, _
                                       {"VI_CL_INS_CANTIDAD", OracleDbType.Long, 1100, pastrInsCantidads}, _
                                       {"VI_CL_INS_RESPONS", OracleDbType.Long, 3100, pastrInsResponsables}, _
                                       {"VI_CL_INS_FEC_INIC", OracleDbType.Long, 3900, pastrInsFechaInicios}, _
                                       {"VI_CL_INS_FEC_FIN", OracleDbType.Long, 3900, pastrInsFechaFins}, _
                                       {"VI_CL_INS_ESTADO", OracleDbType.Long, 2200, pastrInsEstados}, _
                                       {"VI_CL_INS_DOC_REF", OracleDbType.Long, 3500, pastrInsDocRefs}, _
                                       {"VI_CL_UPD_CORREL", OracleDbType.Long, 400000, pastrUpdCorrelativos}, _
                                       {"VI_CL_UPD_CLS_PROD", OracleDbType.Long, 900, pastrUpdClaseProds}, _
                                       {"VI_CL_UPD_SER_PROD", OracleDbType.Long, 1000, pastrUpdSerieProds}, _
                                       {"VI_CL_UPD_NRO_PROD", OracleDbType.Long, 1200, pastrUpdNroProds}, _
                                       {"VI_CL_UPD_DETALLE", OracleDbType.Long, 21000, pastrUpdDetalles}, _
                                       {"VI_CL_UPD_CANTIDAD", OracleDbType.Long, 1100, pastrUpdCantidads}, _
                                       {"VI_CL_UPD_RESPONS", OracleDbType.Long, 3100, pastrUpdResponsables}, _
                                       {"VI_CL_UPD_FEC_INIC", OracleDbType.Long, 3900, pastrUpdFechaInicios}, _
                                       {"VI_CL_UPD_FEC_FIN", OracleDbType.Long, 3900, pastrUpdFechaFins}, _
                                       {"VI_CL_UPD_ESTADO", OracleDbType.Long, 2200, pastrUpdEstados}, _
                                       {"VI_CL_DEL_CORREL", OracleDbType.Long, 400000, pastrDelCorrelativos}, _
                                       {"VI_CL_DEL_DOC_REF", OracleDbType.Long, 3500, pastrDelDocRefs}, _
                                       {"VI_CL_TITULO", OracleDbType.Long, 40000, pastrTitulo}, _
                                       {"VI_CL_DESCRIPCION", OracleDbType.Long, 40000, pastrDescripcion}, _
                                       {"VI_CL_ARCHIVO", OracleDbType.Long, 40000, pastrArchivo}}


        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_UPD_ORDEN_TRABAJO"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    Public Function FS_Upd_Solicitud_Trabajo(ByVal pastrTipo As String, _
                                             ByVal pastrNumero As Integer, _
                                             ByVal pastrFechaDocumento As String, _
                                             ByVal pastrFechaEspAprov As String, _
                                             ByVal pastrFechaAprovResp As String, _
                                             ByVal pastrFechaAprovDir As String, _
                                             ByVal pastrFechaAprovDirLab As String, _
                                             ByVal pastrObservaciones_S As String, _
                                             ByVal pastrNroMuestras As Integer, _
                                             ByVal pastrIDDireccion As String, _
                                             ByVal pastrIDProyecto As String, _
                                             ByVal pastrUsuario As String, _
                                             ByVal pastrFormato As Integer, _
                                             ByVal pastrProveido As String, _
                                             ByVal pastrIns_Correlativos As String, _
                                             ByVal pastrIns_ClaseProds As String, _
                                             ByVal pastrIns_SerieProds As String, _
                                             ByVal pastrIns_NroProds As String, _
                                             ByVal pastrIns_Detalle_DSs As String, _
                                             ByVal pastrIns_Cantidad_DSs As String, _
                                             ByVal pastrIns_Responsables As String, _
                                             ByVal pastrIns_FechaInicios As String, _
                                             ByVal pastrIns_FechaFins As String, _
                                             ByVal pastrIns_Estado_DSs As String, _
                                             ByVal pastrIns_RealizadoEn As String, _
                                             ByVal pastrIns_IdLabExt As String, _
                                             ByVal pastrIns_IDMuestras As String, _
                                             ByVal pastrIns_Clase_Ms As String, _
                                             ByVal pastrIns_MatRecipientes As String, _
                                             ByVal pastrIns_IndPreservantes As String, _
                                             ByVal pastrIns_Preservantes As String, _
                                             ByVal pastrIns_Observaciones_Ms As String, _
                                             ByVal pastrIns_Corre_DMs As String, _
                                             ByVal pastrIns_Codint_DMs As String, _
                                             ByVal pastrIns_Detalle_DMs As String, _
                                             ByVal pastrIns_Cantidad_DMs As String, _
                                             ByVal pastrIns_Estado_DMs As String, _
                                             ByVal pastrUpd_Correlativos As String, _
                                             ByVal pastrUpd_ClaseProds As String, _
                                             ByVal pastrUpd_SerieProds As String, _
                                             ByVal pastrUpd_NroProds As String, _
                                             ByVal pastrUpd_Detalle_DSs As String, _
                                             ByVal pastrUpd_Cantidad_DSs As String, _
                                             ByVal pastrUpd_Responsables As String, _
                                             ByVal pastrUpd_FechaInicios As String, _
                                             ByVal pastrUpd_FechaFins As String, _
                                             ByVal pastrUpd_Estado_DSs As String, _
                                             ByVal pastrUpdRealizadoEn As String, _
                                             ByVal pastrUpdIdLabExt As String, _
                                             ByVal pastrUpd_IDIntMuestras As String, _
                                             ByVal pastrUpd_IDMuestras As String, _
                                             ByVal pastrUpd_Clase_Ms As String, _
                                             ByVal pastrUpd_MatRecipientes As String, _
                                             ByVal pastrUpd_IndPreservantes As String, _
                                             ByVal pastrUpd_Preservantes As String, _
                                             ByVal pastrUpd_Observaciones_Ms As String, _
                                             ByVal pastrDel_Correlativos As String, _
                                             ByVal pastrDel_IDIntMuestras As String, _
                                             ByVal pastrDel_Corre_DMs As String, _
                                             ByVal pastrDel_Codint_DMs As String) As String


        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int32, 6, pastrNumero}, _
                                       {"VI_VA_FECHA_DOCUMENTO", OracleDbType.Varchar2, 30, pastrFechaDocumento}, _
                                       {"VI_VA_FECHA_ESPE_APRO", OracleDbType.Varchar2, 30, pastrFechaEspAprov}, _
                                       {"VI_VA_FECHA_APRO_RESP", OracleDbType.Varchar2, 30, pastrFechaAprovResp}, _
                                       {"VI_VA_FECHA_APRO_DIR", OracleDbType.Varchar2, 30, pastrFechaAprovDir}, _
                                       {"VI_VA_FECHA_APRO_DLAB", OracleDbType.Varchar2, 30, pastrFechaAprovDirLab}, _
                                       {"VI_VA_OBSERVACIONES_S", OracleDbType.Varchar2, 4000, pastrObservaciones_S}, _
                                       {"VI_NU_NRO_MUESTRAS", OracleDbType.Int32, 4, pastrNroMuestras}, _
                                       {"VI_NU_CODIGO_DIRECCION", OracleDbType.Int32, 8, pastrIDDireccion}, _
                                       {"VI_NU_CODIGO_PROYECTO", OracleDbType.Int32, 8, pastrIDProyecto}, _
                                       {"VI_VA_USUARIO", OracleDbType.Varchar2, 11, pastrUsuario}, _
                                       {"VI_NU_FORMATO", OracleDbType.Varchar2, 11, pastrFormato}, _
                                       {"VI_VA_PROVEIDO", OracleDbType.Varchar2, 1000, pastrProveido}, _
                                       {"VI_CL_INS_CORRELATIVO", OracleDbType.Long, 2000000, pastrIns_Correlativos}, _
                                       {"VI_CL_INS_CLASE_PRODUCTO", OracleDbType.Long, 45000, pastrIns_ClaseProds}, _
                                       {"VI_CL_INS_SERIE_PRODUCTO", OracleDbType.Long, 5000, pastrIns_SerieProds}, _
                                       {"VI_CL_INS_NUMERO_PRODUCTO", OracleDbType.Long, 6000, pastrIns_NroProds}, _
                                       {"VI_CL_INS_DETALLE_DS", OracleDbType.Long, 105000, pastrIns_Detalle_DSs}, _
                                       {"VI_CL_INS_CANTIDAD_DS", OracleDbType.Long, 5500, pastrIns_Cantidad_DSs}, _
                                       {"VI_CL_INS_RESPONSABLE", OracleDbType.Long, 15500, pastrIns_Responsables}, _
                                       {"VI_CL_INS_FECHA_INICIO", OracleDbType.Long, 20000, pastrIns_FechaInicios}, _
                                       {"VI_CL_INS_FECHA_FIN", OracleDbType.Long, 20000, pastrIns_FechaFins}, _
                                       {"VI_CL_INS_ESTADO_DS", OracleDbType.Long, 11000, pastrIns_Estado_DSs}, _
                                       {"VI_CL_INS_REALIZADO_EN", OracleDbType.Long, 1000, pastrIns_RealizadoEn}, _
                                       {"VI_CL_INS_COD_LABEXT", OracleDbType.Long, 11000, pastrIns_IdLabExt}, _
                                       {"VI_CL_INS_COD_MUESTRA", OracleDbType.Long, 2000000, pastrIns_IDMuestras}, _
                                       {"VI_CL_INS_CLASE_M", OracleDbType.Long, 2000000, pastrIns_Clase_Ms}, _
                                       {"VI_CL_INS_MAT_RECIPIENTE", OracleDbType.Long, 2000000, pastrIns_MatRecipientes}, _
                                       {"VI_CL_INS_IND_PRESERVANTE", OracleDbType.Long, 2000000, pastrIns_IndPreservantes}, _
                                       {"VI_CL_INS_PRESERVANTE", OracleDbType.Long, 2000000, pastrIns_Preservantes}, _
                                       {"VI_CL_INS_OBSERVACIONES_M", OracleDbType.Long, 2000000, pastrIns_Observaciones_Ms}, _
                                       {"VI_CL_INS_CORRE_DM", OracleDbType.Long, 2000000, pastrIns_Corre_DMs}, _
                                       {"VI_CL_INS_CODINT_DM", OracleDbType.Long, 2000000, pastrIns_Codint_DMs}, _
                                       {"VI_CL_INS_DETALLE_DM", OracleDbType.Long, 2000000, pastrIns_Detalle_DMs}, _
                                       {"VI_CL_INS_CANTIDAD_DM", OracleDbType.Long, 2000000, pastrIns_Cantidad_DMs}, _
                                       {"VI_CL_INS_ESTADO_DM", OracleDbType.Long, 2000000, pastrIns_Estado_DMs}, _
                                       {"VI_CL_UPD_CORRELATIVO", OracleDbType.Long, 2000000, pastrUpd_Correlativos}, _
                                       {"VI_CL_UPD_CLASE_PRODUCTO", OracleDbType.Long, 45000, pastrUpd_ClaseProds}, _
                                       {"VI_CL_UPD_SERIE_PRODUCTO", OracleDbType.Long, 5000, pastrUpd_SerieProds}, _
                                       {"VI_CL_UPD_NUMERO_PRODUCTO", OracleDbType.Long, 6000, pastrUpd_NroProds}, _
                                       {"VI_CL_UPD_DETALLE_DS", OracleDbType.Long, 105000, pastrUpd_Detalle_DSs}, _
                                       {"VI_CL_UPD_CANTIDAD_DS", OracleDbType.Long, 5500, pastrUpd_Cantidad_DSs}, _
                                       {"VI_CL_UPD_RESPONSABLE", OracleDbType.Long, 15500, pastrUpd_Responsables}, _
                                       {"VI_CL_UPD_FECHA_INICIO", OracleDbType.Long, 20000, pastrUpd_FechaInicios}, _
                                       {"VI_CL_UPD_FECHA_FIN", OracleDbType.Long, 20000, pastrUpd_FechaFins}, _
                                       {"VI_CL_UPD_ESTADO_DS", OracleDbType.Long, 11000, pastrUpd_Estado_DSs}, _
                                       {"VI_CL_UPD_REALIZADO_EN", OracleDbType.Long, 1000, pastrUpdRealizadoEn}, _
                                       {"VI_CL_UPD_COD_LABEXT", OracleDbType.Long, 11000, pastrUpdIdLabExt}, _
                                       {"VI_CL_UPD_CODINT_MUESTRA", OracleDbType.Long, 2000000, pastrUpd_IDIntMuestras}, _
                                       {"VI_CL_UPD_COD_MUESTRA", OracleDbType.Long, 2000000, pastrUpd_IDMuestras}, _
                                       {"VI_CL_UPD_CLASE_M", OracleDbType.Long, 2000000, pastrUpd_Clase_Ms}, _
                                       {"VI_CL_UPD_MAT_RECIPIENTE", OracleDbType.Long, 2000000, pastrUpd_MatRecipientes}, _
                                       {"VI_CL_UPD_IND_PRESERVANTE", OracleDbType.Long, 2000000, pastrUpd_IndPreservantes}, _
                                       {"VI_CL_UPD_PRESERVANTE", OracleDbType.Long, 2000000, pastrUpd_Preservantes}, _
                                       {"VI_CL_UPD_OBSERVACIONES_M", OracleDbType.Long, 2000000, pastrUpd_Observaciones_Ms}, _
                                       {"VI_CL_DEL_CORRELATIVO", OracleDbType.Long, 2000000, pastrDel_Correlativos}, _
                                       {"VI_CL_DEL_CODINT_MUESTRA", OracleDbType.Long, 2000000, pastrDel_IDIntMuestras}, _
                                       {"VI_CL_DEL_CORRE_DM", OracleDbType.Long, 2000000, pastrDel_Corre_DMs}, _
                                       {"VI_CL_DEL_CODINT_DM", OracleDbType.Long, 2000000, pastrDel_Codint_DMs}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_UPD_SOLICITUD_TRABAJO"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    Public Function FS_Upd_Cambia_Estado(ByVal pastrTipo As String, _
                                         ByVal pastrNroDoc As String, _
                                         ByVal pastrArriba_Abajo As Integer, _
                                         ByVal pastrUsuario As String, _
                                         ByVal pastrProveido As String, _
                                         ByVal pastrUpdCorrelativo As String, _
                                         ByVal pastrUpdClase_Prod As String, _
                                         ByVal pastrUpdSerie_Prod As String, _
                                         ByVal pastrUpdNumero_Prod As String, _
                                         ByVal pastrUpdResponsable As String) As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_VA_NRODOC", OracleDbType.Varchar2, 25, pastrNroDoc}, _
                                       {"VI_NU_ARRIBA_ABAJO", OracleDbType.Int32, 4, pastrArriba_Abajo}, _
                                       {"VI_VA_USUARIO_INICIO", OracleDbType.Varchar2, 32, pastrUsuario}, _
                                       {"VI_VA_PROVEIDO", OracleDbType.Varchar2, 1000, pastrProveido}, _
                                       {"VI_CL_UPD_CORRELATIVO", OracleDbType.Varchar2, 1000, pastrUpdCorrelativo}, _
                                       {"VI_CL_UPD_CLASE_PRODUCTO", OracleDbType.Varchar2, 1000, pastrUpdClase_Prod}, _
                                       {"VI_CL_UPD_SERIE_PRODUCTO", OracleDbType.Varchar2, 1000, pastrUpdSerie_Prod}, _
                                       {"VI_CL_UPD_NUMERO_PRODUCTO", OracleDbType.Varchar2, 1000, pastrUpdNumero_Prod}, _
                                       {"VI_CL_UPD_RESPONSABLE", OracleDbType.Varchar2, 1000, pastrUpdResponsable}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_UPD_CAMBIA_ESTADO"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    Public Function FS_Upd_Solicitud_Laboratorio(ByVal pastrTipo As String, _
                                                 ByVal pastrNumero As Integer, _
                                                 ByVal pastrFechaRecepcion As String, _
                                                 ByVal pastrFechaAtencion As String, _
                                                 ByVal pastrFechaEntrega As String, _
                                                 ByVal pastrUpd_Correlativos As String, _
                                                 ByVal pastrUpd_ClaseProds As String, _
                                                 ByVal pastrUpd_SerieProds As String, _
                                                 ByVal pastrUpd_NroProds As String, _
                                                 ByVal pastrUpd_Detalle_DSs As String, _
                                                 ByVal pastrUpd_Cantidad_DSs As String, _
                                                 ByVal pastrUpd_Responsables As String, _
                                                 ByVal pastrUpd_FechaInicios As String, _
                                                 ByVal pastrUpd_FechaFins As String, _
                                                 ByVal pastrUpd_Estado_DSs As String, _
                                                 ByVal pastrUpd_IDIntMuestras As String, _
                                                 ByVal pastrTitulo As String, _
                                                 ByVal pastrDescripcion As String, _
                                                 ByVal pastrArchivo As String, _
                                                 ByVal pastrUpd_Observaciones_Ms As String) As String


        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int32, 6, pastrNumero}, _
                                       {"VI_VA_FECHA_RECEPCION", OracleDbType.Varchar2, 30, pastrFechaRecepcion}, _
                                       {"VI_VA_FECHA_ATENCION", OracleDbType.Varchar2, 30, pastrFechaAtencion}, _
                                       {"VI_VA_FECHA_ENTREGA", OracleDbType.Varchar2, 30, pastrFechaEntrega}, _
                                       {"VI_CL_UPD_CORRELATIVO", OracleDbType.Long, 2000000, pastrUpd_Correlativos}, _
                                       {"VI_CL_UPD_CLASE_PRODUCTO", OracleDbType.Long, 45000, pastrUpd_ClaseProds}, _
                                       {"VI_CL_UPD_SERIE_PRODUCTO", OracleDbType.Long, 5000, pastrUpd_SerieProds}, _
                                       {"VI_CL_UPD_NUMERO_PRODUCTO", OracleDbType.Long, 6000, pastrUpd_NroProds}, _
                                       {"VI_CL_UPD_DETALLE_DS", OracleDbType.Long, 105000, pastrUpd_Detalle_DSs}, _
                                       {"VI_CL_UPD_CANTIDAD_DS", OracleDbType.Long, 5500, pastrUpd_Cantidad_DSs}, _
                                       {"VI_CL_UPD_RESPONSABLE", OracleDbType.Long, 15500, pastrUpd_Responsables}, _
                                       {"VI_CL_UPD_FECHA_INICIO", OracleDbType.Long, 20000, pastrUpd_FechaInicios}, _
                                       {"VI_CL_UPD_FECHA_FIN", OracleDbType.Long, 20000, pastrUpd_FechaFins}, _
                                       {"VI_CL_UPD_ESTADO_DS", OracleDbType.Long, 11000, pastrUpd_Estado_DSs}, _
                                       {"VI_CL_UPD_CODINT_MUESTRA", OracleDbType.Long, 2000000, pastrUpd_IDIntMuestras}, _
                                       {"VI_CL_UPD_OBSERVACIONES_M", OracleDbType.Long, 2000000, pastrUpd_Observaciones_Ms}, _
                                       {"VI_CL_TITULO", OracleDbType.Long, 20000, pastrTitulo}, _
                                       {"VI_CL_DESCRIPCION", OracleDbType.Long, 20000, pastrDescripcion}, _
                                       {"VI_CL_ARCHIVO", OracleDbType.Long, 20000, pastrArchivo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_UPD_SOLIC_LABORATORIO"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function


    '*************************************************************************************
    'Descripción: Procedimiento de Eliminación de los ITEMS de Documentos de Trabajo
    'Fecha: 25/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Del_Doc_Trabajo_Items(ByVal pastrTipo As String, _
                                             ByVal pastrNumero As Integer, _
                                             ByVal pastrCorrelativo As Integer) As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int32, 4, pastrNumero}, _
                                       {"VI_NU_CORRELATIVO", OracleDbType.Int32, 4, pastrCorrelativo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_DEL_DOC_TRABAJO_ITEM"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    Public Function FS_Del_Muestra(ByVal pastrCodInterno As Integer) As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_CODINT_MUESTRA", OracleDbType.Int32, 3, pastrCodInterno}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_DEL_MUESTRA"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    Public Function FS_Del_Detalle_Muestra(ByVal pastrTipo As String, _
                                           ByVal pastrNumero As Integer, _
                                           ByVal pastrCorrelativo As Integer, _
                                           ByVal pastrCodigo_Interno_muestra As Integer) As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 11, pastrTipo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int32, 4, pastrNumero}, _
                                       {"VI_NU_CORRELATIVO", OracleDbType.Int32, 2, pastrCorrelativo}, _
                                       {"VI_NU_CODINT_MUESTRA", OracleDbType.Int32, 3, pastrCodigo_Interno_muestra}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_DEL_DT_MUESTRA_ITEM"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Referencia las Proformas con los Doc de Trabajo
    'Fecha: 22/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Ref_ProfItem_con_DTItem(ByVal pastrNroProforma As Integer, _
                                               ByVal pastrCorreProforma As Integer, _
                                               ByVal pastrTipoDocTrabajo As String, _
                                               ByVal pastrNroDocTrabajo As Integer, _
                                               ByVal pastrCorreDocTrabajo As Integer) As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_NRO_PROFORMA", OracleDbType.Int32, 5, pastrNroProforma}, _
                                       {"VI_NU_CORRE_PROFORMA", OracleDbType.Int32, 2, pastrCorreProforma}, _
                                       {"VI_VA_TIPO_DOCTRABAJO", OracleDbType.Varchar2, 2, pastrTipoDocTrabajo}, _
                                       {"VI_NU_NRO_DOCTRABAJO", OracleDbType.Int32, 4, pastrNroDocTrabajo}, _
                                       {"VI_NU_CORRE_DOCTRABAJO", OracleDbType.Int32, 2, pastrCorreDocTrabajo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_REF_PR_ITEM_CON_DT_ITEM"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '*************************************************************************************
    'Descripción: Referencia las Boletas o Facturas con los Doc de Trabajo
    'Fecha: 22/05/2007
    'Creador: Walter Bueno
    '*************************************************************************************
    Public Function FS_Ref_DPItem_con_DTItem(ByVal pastrTipoDocPago As String, _
                                             ByVal pastrSerieDocPago As String, _
                                             ByVal pastrNroDocPago As String, _
                                             ByVal pastrCorreDocPago As Integer, _
                                             ByVal pastrTipoDocTrabajo As String, _
                                             ByVal pastrNroDocTrabajo As Integer, _
                                             ByVal pastrCorreDocTrabajo As Integer) As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO_DOCPAGO", OracleDbType.Varchar2, 2, pastrTipoDocPago}, _
                                       {"VI_VA_SERIE_DOCPAGO", OracleDbType.Varchar2, 3, pastrSerieDocPago}, _
                                       {"VI_VA_NRO_DOCPAGO", OracleDbType.Varchar2, 10, pastrNroDocPago}, _
                                       {"VI_NU_CORRE_DOCPAGO", OracleDbType.Int32, 2, pastrCorreDocPago}, _
                                       {"VI_VA_TIPO_DOCTRABAJO", OracleDbType.Varchar2, 2, pastrTipoDocTrabajo}, _
                                       {"VI_NU_NRO_DOCTRABAJO", OracleDbType.Int32, 5, pastrNroDocTrabajo}, _
                                       {"VI_NU_CORRE_DOCTRABAJO", OracleDbType.Int32, 2, pastrCorreDocTrabajo}}


        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_REF_DP_ITEM_CON_DT_ITEM"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    Public Function FS_Vrf_Servicio_Existe(ByVal pastrClaseProducto As String, _
                                                  ByVal pastrSerieProducto As String, _
                                                  ByVal pastrNroProducto As String) As DataTable

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO_DOCPAGO", OracleDbType.Varchar2, 2, pastrClaseProducto}, _
                                       {"VI_VA_SERIE_DOCPAGO", OracleDbType.Varchar2, 3, pastrSerieProducto}, _
                                       {"VI_VA_NRO_DOCPAGO", OracleDbType.Varchar2, 10, pastrNroProducto}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_VRF_EXISTENCIA_SERVICIO"

        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FS_Sel_Docs_x_Cliente(ByVal pastrIDCliente As String, Optional ByVal pastrMenorFecha As String = "01/01/0001") As DataTable

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_VISTA", OracleDbType.Int32, 4, 0}, _
                                       {"VI_VA_CODIGO_CLIENTE", OracleDbType.Int32, 4, pastrIDCliente}, _
                                       {"VI_VA_FECHA_MENOR", OracleDbType.Varchar2, 20, pastrMenorFecha}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DOCS_X_CLIENTE"

        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FS_Sel_Docs_SinAsoc_x_Cliente(ByVal pastrIDCliente As String, Optional ByVal pastrMenorFecha As String = "01/01/0001") As DataTable
        pastrMenorFecha = "15/01/2007"
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_VISTA", OracleDbType.Int32, 4, 1}, _
                                       {"VI_VA_CODIGO_CLIENTE", OracleDbType.Int32, 4, pastrIDCliente}, _
                                       {"VI_VA_FECHA_MENOR", OracleDbType.Varchar2, 20, pastrMenorFecha}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DOCS_X_CLIENTE"

        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FS_Sel_Docs_Items_x_Cliente(ByVal pastrIDCliente As String, Optional ByVal pastrFechaminima As String = "01/01/0001") As DataTable

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_VISTA", OracleDbType.Int32, 4, 0}, _
                                       {"VI_VA_CODIGO_CLIENTE", OracleDbType.Int32, 4, pastrIDCliente}, _
                                       {"VI_VA_FECHA_MENOR", OracleDbType.Varchar2, 20, pastrFechaminima}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DOCS_ITEMS_X_CLIENTE"

        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FS_Sel_Docs_SinAsoc_Items_x_Cliente(ByVal pastrIDCliente As String, Optional ByVal pastrFechaMinima As String = "01/01/0001 12:00:00 am") As DataTable

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_VISTA", OracleDbType.Int32, 4, 1}, _
                                       {"VI_VA_CODIGO_CLIENTE", OracleDbType.Int32, 4, pastrIDCliente}, _
                                       {"VI_VA_FECHA_MENOR", OracleDbType.Varchar2, 20, pastrFechaMinima}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DOCS_ITEMS_X_CLIENTE"

        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FS_Sel_Doc_Resultantes(ByVal pastrNombRuta As String, ByVal pastrTipo As String, ByVal pastrNumero As Integer) As DataTable

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_NOMB_RUTA", OracleDbType.Varchar2, 200, pastrNombRuta}, _
                                       {"VI_VA_TIPO", OracleDbType.Varchar2, 3, pastrTipo}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int32, 4, pastrNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DOC_RESULTANTES"

        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FS_Gen_Correlativo_xArea(ByVal pastrIdArea As Int16, ByVal pastrIDTipoDoc As Integer, ByVal pastrAnho As Integer) As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"P_AREA", OracleDbType.Int16, 4, pastrIdArea}, _
                                       {"P_TIPO_DOC", OracleDbType.Int16, 4, pastrIDTipoDoc}, _
                                       {"P_ANHO", OracleDbType.Int32, 5, pastrAnho}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.PROC_CORRELATIVO"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function Fs_Sel_Nivel_Vista(ByVal pastrUsuario As String) As Int16
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_USUARIO", OracleDbType.Varchar2, 15, pastrUsuario}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_NIVEL_VISTA"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function Fs_Sel_Direccion_xUsuario(ByVal pastrUsuario As String) As Int32
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_USUARIO", OracleDbType.Varchar2, 15, pastrUsuario}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DIRECCION_xUSUARIO"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Overloads Function FS_Sel_DatosUsuario_xDireccion(ByVal pastrIdDireccion As Integer) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_IDDIRECCION", OracleDbType.Int32, 4, pastrIdDireccion}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DATOSUSUARIO_XDIR"

        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Function FS_Busc_Usuario_xDireccion(ByVal pastrIdDireccion As Integer) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_IDDIRECCION", OracleDbType.Int32, 4, pastrIdDireccion}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_BUSC_USUARIO_XDIR"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    Public Overloads Function FS_Sel_DatosUsuario(ByVal pastrIdUsuario As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_USUARIO", OracleDbType.Varchar2, 20, pastrIdUsuario}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DATOSUSUARIO"

        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Overloads Function FS_Sel_DatosUsuario_xDocumento(ByVal pastrTipo As String, ByVal pastrNumero As Integer, ByVal pastrUsuarioDev As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO_DOC", OracleDbType.Varchar2, 20, pastrTipo}, _
                                       {"VI_VA_NRO_DOC", OracleDbType.Int32, 5, pastrNumero}, _
                                       {"VI_VA_USUARIO_FIN", OracleDbType.Varchar2, 20, pastrUsuarioDev}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_DATOSUSUARIO_EMISOR_XDOC"

        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function


    Public Function FS_Sel_Historial(ByVal pastrTipo As String, _
                                     ByVal pastrNroDoc As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 20, pastrTipo}, _
                                       {"VI_VA_NRODOC", OracleDbType.Varchar2, 25, pastrNroDoc}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_HISTORIAL"

        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function FS_Sel_Actuaciones(ByVal pastrUsuario As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_USUARIO", OracleDbType.Varchar2, 32, pastrUsuario}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_SEL_ACTUACION"

        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function FS_Es_Creador(ByVal pastrUsuario As String) As Boolean
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_USUARIO", OracleDbType.Varchar2, 32, pastrUsuario}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_ES_CREADOR"

        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
    Public Function FS_Tiene_Acceso(ByVal pastrUsuario As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_USUARIO", OracleDbType.Varchar2, 32, pastrUsuario}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_TIENE_ACCESO"

        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

#End Region

    '*************************************************************************************
    'Descripción: Inserta un registro de Documento de Venta
    'Fecha: 20/03/2006 
    '*************************************************************************************
    Public Function FT_Ins_Doc_Venta(ByVal pastrTipo As String, ByVal pastrSerie As String, _
                                     ByVal pastrNumero As String, ByVal pastrCodigoCliente As String, _
                                     ByVal pastrFecha As String, ByVal padblTipoCambio As Double, _
                                     ByVal padblValorVenta As Double, ByVal padblIncrementosVarios As Double, _
                                     ByVal padblDescuento As Double, ByVal padblIGV As Double, _
                                     ByVal padblTotal As Double, ByVal pastrMoneda As String, _
                                     ByVal pastrUsuarioRegistro As String, ByVal pastrFormaPago As String, _
                                     ByVal pastrNumeroPedido As String, ByVal pastrCantidad As String, _
                                     ByVal pastrUnidadMedida As String, ByVal pastrPrecioUnitario As String, _
                                     ByVal pastrClaseProducto As String, ByVal pastrSerieProducto As String, _
                                     ByVal pastrNumeroProducto As String, ByVal pastrSubTotal As String, _
                                     ByVal paobjDetalle As Object, ByVal paobjCd As Object, _
                                     ByVal pastrGlosa As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
                                       {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}, _
                                       {"VI_VA_CODIGO_CLIENTE", OracleDbType.Varchar2, 11, pastrCodigoCliente}, _
                                       {"VI_VA_FECHA", OracleDbType.Varchar2, 10, pastrFecha}, _
                                       {"VI_NU_TIPO_CAMBIO", OracleDbType.Double, 15, padblTipoCambio}, _
                                       {"VI_NU_VALOR_VENTA", OracleDbType.Double, 15, padblValorVenta}, _
                                       {"VI_NU_INCREMENTOS_VARIOS", OracleDbType.Double, 15, padblIncrementosVarios}, _
                                       {"VI_NU_DESCUENTO", OracleDbType.Double, 15, padblDescuento}, _
                                       {"VI_NU_IGV", OracleDbType.Double, 15, padblIGV}, _
                                       {"VI_NU_TOTAL", OracleDbType.Double, 15, padblTotal}, _
                                       {"VI_VA_MONEDA", OracleDbType.Varchar2, 64, pastrMoneda}, _
                                       {"VI_VA_USUARIO_REGISTRO", OracleDbType.Varchar2, 32, pastrUsuarioRegistro}, _
                                       {"VI_VA_FORMA_PAGO", OracleDbType.Varchar2, 64, pastrFormaPago}, _
                                       {"VI_VA_NUMERO_PEDIDO", OracleDbType.Varchar2, 8, pastrNumeroPedido}, _
                                       {"VI_VA_CANTIDAD", OracleDbType.Clob, 4000000, pastrCantidad}, _
                                       {"VI_VA_UNIDAD_MEDIDA", OracleDbType.Clob, 4000000, pastrUnidadMedida}, _
                                       {"VI_VA_PRECIO_UNITARIO", OracleDbType.Clob, 4000000, pastrPrecioUnitario}, _
                                       {"VI_VA_CLASE_PRODUCTO", OracleDbType.Clob, 4000000, pastrClaseProducto}, _
                                       {"VI_VA_SERIE_PRODUCTO", OracleDbType.Clob, 4000000, pastrSerieProducto}, _
                                       {"VI_VA_NUMERO_PRODUCTO", OracleDbType.Clob, 4000000, pastrNumeroProducto}, _
                                       {"VI_VA_SUBTOTAL", OracleDbType.Clob, 4000000, pastrSubTotal}, _
                                       {"VI_VA_DETALLE", OracleDbType.Clob, 4000000, paobjDetalle}, _
                                       {"VI_VA_CD", OracleDbType.Clob, 4000000, paobjCd}, _
                                       {"VI_VA_GLOSA", OracleDbType.Varchar2, 256, pastrGlosa}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_INS_DOC_PAGO"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Inserta un registro de Documento de Venta
    'Fecha: 20/03/2006 
    '*************************************************************************************
    Public Function FT_Mod_Doc_Venta(ByVal pastrTipo As String, ByVal pastrSerie As String, _
                                     ByVal pastrNumero As String, ByVal pastrCodigoCliente As String, _
                                     ByVal pastrFecha As String, ByVal padblTipoCambio As Double, _
                                     ByVal padblValorVenta As Double, ByVal padblIncrementosVarios As Double, _
                                     ByVal padblDescuento As Double, ByVal padblIGV As Double, _
                                     ByVal padblTotal As Double, ByVal pastrMoneda As String, _
                                     ByVal pastrFormaPago As String, ByVal pastrNumeroPedido As String, _
                                     ByVal pastrCantidad As String, ByVal pastrUnidadMedida As String, _
                                     ByVal pastrPrecioUnitario As String, ByVal pastrClaseProducto As String, _
                                     ByVal pastrSerieProducto As String, ByVal pastrNumeroProducto As String, _
                                     ByVal pastrSubTotal As String, ByVal paobjDetalle As Object, _
                                     ByVal paobjCd As Object, ByVal pastrGlosa As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
                                       {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}, _
                                       {"VI_VA_CODIGO_CLIENTE", OracleDbType.Varchar2, 11, pastrCodigoCliente}, _
                                       {"VI_VA_FECHA", OracleDbType.Varchar2, 10, pastrFecha}, _
                                       {"VI_NU_TIPO_CAMBIO", OracleDbType.Double, 15, padblTipoCambio}, _
                                       {"VI_NU_VALOR_VENTA", OracleDbType.Double, 15, padblValorVenta}, _
                                       {"VI_NU_INCREMENTOS_VARIOS", OracleDbType.Double, 15, padblIncrementosVarios}, _
                                       {"VI_NU_DESCUENTO", OracleDbType.Double, 15, padblDescuento}, _
                                       {"VI_NU_IGV", OracleDbType.Double, 15, padblIGV}, _
                                       {"VI_NU_TOTAL", OracleDbType.Double, 15, padblTotal}, _
                                       {"VI_VA_MONEDA", OracleDbType.Varchar2, 64, pastrMoneda}, _
                                       {"VI_VA_FORMA_PAGO", OracleDbType.Varchar2, 64, pastrFormaPago}, _
                                       {"VI_VA_NUMERO_PEDIDO", OracleDbType.Varchar2, 8, pastrNumeroPedido}, _
                                       {"VI_VA_CANTIDAD", OracleDbType.Clob, 4000000, pastrCantidad}, _
                                       {"VI_VA_UNIDAD_MEDIDA", OracleDbType.Clob, 4000000, pastrUnidadMedida}, _
                                       {"VI_VA_PRECIO_UNITARIO", OracleDbType.Clob, 4000000, pastrPrecioUnitario}, _
                                       {"VI_VA_CLASE_PRODUCTO", OracleDbType.Clob, 4000000, pastrClaseProducto}, _
                                       {"VI_VA_SERIE_PRODUCTO", OracleDbType.Clob, 4000000, pastrSerieProducto}, _
                                       {"VI_VA_NUMERO_PRODUCTO", OracleDbType.Clob, 4000000, pastrNumeroProducto}, _
                                       {"VI_VA_SUBTOTAL", OracleDbType.Clob, 4000000, pastrSubTotal}, _
                                       {"VI_VA_DETALLE", OracleDbType.Clob, 4000000, paobjDetalle}, _
                                       {"VI_VA_CD", OracleDbType.Clob, 4000000, paobjCd}, _
                                       {"VI_VA_GLOSA", OracleDbType.Varchar2, 256, pastrGlosa}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_UPD_DOC_PAGO"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*****************************************************************************************************
    'Descripción: Anula un registro de Documento de Venta
    'Fecha: 24/03/2006 
    '****************************************************************************************************
    Public Function FT_Anu_Doc_Venta(ByVal pastrTipo As String, ByVal pastrSerie As String, _
                                     ByVal pastrNumero As String) As String
        Dim lstrprocedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim arrParametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                          {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
                                          {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}}

        lstrprocedimiento = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_ANU_DOC_PAGO"

        Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrprocedimiento, arrParametros)
        objDatos = Nothing
    End Function

    '*****************************************************************************************************
    'Descripción: Anula un registro de Documento de Venta
    'Fecha: 24/03/2006 
    '****************************************************************************************************
    Public Function FT_Apr_Anu_Doc_Venta(ByVal pastrTipo As String, ByVal pastrSerie As String, _
                                         ByVal pastrNumero As String) As String
        Dim lstrprocedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim arrParametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                          {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
                                          {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}}

        lstrprocedimiento = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_APR_ANU_DOC_PAGO"

        Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrprocedimiento, arrParametros)
        objDatos = Nothing
    End Function

    '*****************************************************************************************************
    'Descripción: Anula un registro de Documento de Venta
    'Fecha: 24/03/2006 
    '****************************************************************************************************
    Public Function FT_Anu_Rap_Doc_Venta(ByVal pastrTipo As String, ByVal pastrSerie As String, _
                                         ByVal pastrNumero As String, ByVal pastrFecha As String) As String
        Dim lstrprocedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim arrParametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                          {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
                                          {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}, _
                                          {"VI_VA_FECHA", OracleDbType.Varchar2, 10, pastrFecha}}

        lstrprocedimiento = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_ANU_RAP_DOC_PAGO"

        Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrprocedimiento, arrParametros)
        objDatos = Nothing
    End Function

    '*****************************************************************************************************
    'Descripción: Imprime un registro de Documento de Venta
    'Fecha: 29/03/2006 
    '****************************************************************************************************
    Public Function FT_Imp_Doc_Venta(ByVal pastrTipo As String, ByVal pastrSerie As String, _
                                     ByVal pastrNumero As String) As String
        Dim lstrprocedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim arrParametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                          {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
                                          {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}}

        lstrprocedimiento = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_IMP_DOC_PAGO"

        Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrprocedimiento, arrParametros)
        objDatos = Nothing
    End Function

    '*************************************************************************************
    'Descripción: Inserta un registro de Proforma de Venta
    'Fecha: 22/03/2006 
    '*************************************************************************************
    Public Function FT_Ins_Proforma(ByVal paintNumero As Int64, ByVal pastrCodigoCliente As String, _
                                    ByVal pastrFecha As String, ByVal padblTipoCambio As Double, _
                                    ByVal padblValorVenta As Double, ByVal padblIncrementosVarios As Double, _
                                    ByVal padblDescuento As Double, ByVal padblIGV As Double, _
                                    ByVal padblTotal As Double, ByVal pastrMoneda As String, _
                                    ByVal pastrEstado As String, ByVal pastrUsuarioRegistro As String, _
                                    ByVal pastrFormaPago As String, ByVal pastrComentario As String, _
                                    ByVal pastrCantidad As String, ByVal pastrUnidadMedida As String, _
                                    ByVal pastrPrecioUnitario As String, ByVal pastrClaseProducto As String, _
                                    ByVal pastrSerieProducto As String, ByVal pastrNumeroProducto As String, _
                                    ByVal pastrSubTotal As String, ByVal paobjDetalle As Object, _
                                    ByVal paobjCd As Object) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_NUMERO", OracleDbType.Int64, 8, paintNumero}, _
                                       {"VI_VA_CODIGO_CLIENTE", OracleDbType.Varchar2, 11, pastrCodigoCliente}, _
                                       {"VI_VA_FECHA", OracleDbType.Varchar2, 10, pastrFecha}, _
                                       {"VI_NU_TIPO_CAMBIO", OracleDbType.Double, 15, padblTipoCambio}, _
                                       {"VI_NU_VALOR_VENTA", OracleDbType.Double, 15, padblValorVenta}, _
                                       {"VI_NU_INCREMENTOS_VARIOS", OracleDbType.Double, 15, padblIncrementosVarios}, _
                                       {"VI_NU_DESCUENTO", OracleDbType.Double, 15, padblDescuento}, _
                                       {"VI_NU_IGV", OracleDbType.Double, 15, padblIGV}, _
                                       {"VI_NU_TOTAL", OracleDbType.Double, 15, padblTotal}, _
                                       {"VI_VA_MONEDA", OracleDbType.Varchar2, 64, pastrMoneda}, _
                                       {"VI_VA_ESTADO", OracleDbType.Varchar2, 1, pastrEstado}, _
                                       {"VI_VA_USUARIO_REGISTRO", OracleDbType.Varchar2, 32, pastrUsuarioRegistro}, _
                                       {"VI_VA_FORMA_PAGO", OracleDbType.Varchar2, 64, pastrFormaPago}, _
                                       {"VI_VA_COMENTARIO", OracleDbType.Varchar2, 4000, pastrComentario}, _
                                       {"VI_VA_CANTIDAD", OracleDbType.Clob, 4000000, pastrCantidad}, _
                                       {"VI_VA_UNIDAD_MEDIDA", OracleDbType.Clob, 4000000, pastrUnidadMedida}, _
                                       {"VI_VA_PRECIO_UNITARIO", OracleDbType.Clob, 4000000, pastrPrecioUnitario}, _
                                       {"VI_VA_CLASE_PRODUCTO", OracleDbType.Clob, 4000000, pastrClaseProducto}, _
                                       {"VI_VA_SERIE_PRODUCTO", OracleDbType.Clob, 4000000, pastrSerieProducto}, _
                                       {"VI_VA_NUMERO_PRODUCTO", OracleDbType.Clob, 4000000, pastrNumeroProducto}, _
                                       {"VI_VA_SUBTOTAL", OracleDbType.Clob, 4000000, pastrSubTotal}, _
                                       {"VI_VA_DETALLE", OracleDbType.Clob, 4000000, paobjDetalle}, _
                                       {"VI_VA_CD", OracleDbType.Clob, 4000000, paobjCd}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_INS_PEDIDO"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Modifica un registro de Proforma de Venta
    'Fecha: 30/03/2006 
    '*************************************************************************************
    Public Function FT_Mod_Proforma(ByVal paintNumero As Int64, ByVal pastrCodigoCliente As String, _
                                    ByVal pastrFecha As String, ByVal padblTipoCambio As Double, _
                                    ByVal padblValorVenta As Double, ByVal padblIncrementosVarios As Double, _
                                    ByVal padblDescuento As Double, ByVal padblIGV As Double, _
                                    ByVal padblTotal As Double, ByVal pastrMoneda As String, _
                                    ByVal pastrFormaPago As String, ByVal pastrComentario As String, _
                                    ByVal pastrCantidad As String, ByVal pastrUnidadMedida As String, _
                                    ByVal pastrPrecioUnitario As String, ByVal pastrClaseProducto As String, _
                                    ByVal pastrSerieProducto As String, ByVal pastrNumeroProducto As String, _
                                    ByVal pastrSubTotal As String, ByVal paobjDetalle As Object, _
                                    ByVal paobjCd As Object) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_NUMERO", OracleDbType.Int64, 8, paintNumero}, _
                                       {"VI_VA_CODIGO_CLIENTE", OracleDbType.Varchar2, 11, pastrCodigoCliente}, _
                                       {"VI_VA_FECHA", OracleDbType.Varchar2, 10, pastrFecha}, _
                                       {"VI_NU_TIPO_CAMBIO", OracleDbType.Double, 15, padblTipoCambio}, _
                                       {"VI_NU_VALOR_VENTA", OracleDbType.Double, 15, padblValorVenta}, _
                                       {"VI_NU_INCREMENTOS_VARIOS", OracleDbType.Double, 15, padblIncrementosVarios}, _
                                       {"VI_NU_DESCUENTO", OracleDbType.Double, 15, padblDescuento}, _
                                       {"VI_NU_IGV", OracleDbType.Double, 15, padblIGV}, _
                                       {"VI_NU_TOTAL", OracleDbType.Double, 15, padblTotal}, _
                                       {"VI_VA_MONEDA", OracleDbType.Varchar2, 64, pastrMoneda}, _
                                       {"VI_VA_FORMA_PAGO", OracleDbType.Varchar2, 64, pastrFormaPago}, _
                                       {"VI_VA_COMENTARIO", OracleDbType.Varchar2, 4000, pastrComentario}, _
                                       {"VI_VA_CANTIDAD", OracleDbType.Clob, 4000000, pastrCantidad}, _
                                       {"VI_VA_UNIDAD_MEDIDA", OracleDbType.Clob, 4000000, pastrUnidadMedida}, _
                                       {"VI_VA_PRECIO_UNITARIO", OracleDbType.Clob, 4000000, pastrPrecioUnitario}, _
                                       {"VI_VA_CLASE_PRODUCTO", OracleDbType.Clob, 4000000, pastrClaseProducto}, _
                                       {"VI_VA_SERIE_PRODUCTO", OracleDbType.Clob, 4000000, pastrSerieProducto}, _
                                       {"VI_VA_NUMERO_PRODUCTO", OracleDbType.Clob, 4000000, pastrNumeroProducto}, _
                                       {"VI_VA_SUBTOTAL", OracleDbType.Clob, 4000000, pastrSubTotal}, _
                                       {"VI_VA_DETALLE", OracleDbType.Clob, 4000000, paobjDetalle}, _
                                       {"VI_VA_CD", OracleDbType.Clob, 4000000, paobjCd}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_UPD_PEDIDO"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*****************************************************************************************************
    'Descripción: Anula un registro de Proforma
    'Fecha: 24/03/2006 
    '****************************************************************************************************
    Public Function FT_Anu_Proforma(ByVal paintNumero As Int64) As String
        Dim lstrprocedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim arrParametros(,) As Object = {{"VI_NU_NUMERO", OracleDbType.Int64, 8, paintNumero}}

        lstrprocedimiento = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_ANU_PROFORMA"

        Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrprocedimiento, arrParametros)
        objDatos = Nothing
    End Function

    '*****************************************************************************************************
    'Descripción: Imprime un registro de Proforma
    'Fecha: 29/03/2006 
    '****************************************************************************************************
    Public Function FT_Imp_Proforma(ByVal paintNumero As Int64) As String
        Dim lstrprocedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim arrParametros(,) As Object = {{"VI_NU_NUMERO", OracleDbType.Int64, 8, paintNumero}}

        lstrprocedimiento = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_IMP_PROFORMA"

        Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrprocedimiento, arrParametros)
        objDatos = Nothing
    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registro de Documento de Pago
    'Fecha: 21/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Proforma(ByVal pastrNumero As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_NUMERO", OracleDbType.Varchar2, 8, pastrNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_PEDIDO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registro de Items del Documento de Pago
    'Fecha: 21/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Proforma_Item(ByVal pastrNumero As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_NUMERO", OracleDbType.Varchar2, 8, pastrNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_PEDIDO_ITEM"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registro de Items de Proforma con precios actualizados
    'Fecha: 21/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Proforma_Item_Act(ByVal pastrNumero As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_NUMERO", OracleDbType.Varchar2, 8, pastrNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_PEDIDO_ITEM_ACT"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Inserta un registro de Tipo de Cambio
    'Fecha: 24/03/2006 
    '*************************************************************************************
    Public Function FT_Ins_Tipo_Cambio(ByVal pastrFecha As String, ByVal padblCompra As String, _
                                       ByVal padblVenta As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_FECHA", OracleDbType.Varchar2, 10, pastrFecha}, _
                                       {"VI_NU_COMPRA", OracleDbType.Varchar2, 15, padblCompra}, _
                                       {"VI_NU_VENTA", OracleDbType.Varchar2, 15, padblVenta}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_INS_TIPO_CAMBIO"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registro de Tipo de Cambio
    'Fecha: 21/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Tipo_Cambio(ByVal pastrFecha As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_FECHA", OracleDbType.Varchar2, 10, pastrFecha}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_TIPO_CAMBIO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Inserta un registro de Cliente
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FT_Ins_Cliente(ByVal pastrNombre As String, ByVal pastrTipo As String, _
                                   ByVal pastrDireccion As String, ByVal pastrDistrito As String, _
                                   ByVal pastrProvincia As String, ByVal pastrDepartamento As String, _
                                   ByVal pastrPais As String, ByVal pastrTelefono As String, _
                                   ByVal pastrFax As String, ByVal pastrEmail As String, _
                                   ByVal pastrFormaPago As String, ByVal pastrDni As String, _
                                   ByVal pastrRuc As String, ByVal pastrRepresentante As String, _
                                   ByVal pastrContacto As String, ByVal pastrDPPM As String, _
                                   ByVal pastrAgenteRetencion As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_NOMBRE", OracleDbType.Varchar2, 128, pastrNombre}, _
                                       {"VI_VA_TIPO_CLIENTE", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_VA_DIRECCION", OracleDbType.Varchar2, 200, pastrDireccion}, _
                                       {"VI_VA_DISTRITO", OracleDbType.Varchar2, 64, pastrDistrito}, _
                                       {"VI_VA_PROVINCIA", OracleDbType.Varchar2, 64, pastrProvincia}, _
                                       {"VI_VA_DEPARTAMENTO", OracleDbType.Varchar2, 64, pastrDepartamento}, _
                                       {"VI_VA_PAIS", OracleDbType.Varchar2, 64, pastrPais}, _
                                       {"VI_VA_TELEFONO", OracleDbType.Varchar2, 100, pastrTelefono}, _
                                       {"VI_VA_FAX", OracleDbType.Varchar2, 100, pastrFax}, _
                                       {"VI_VA_EMAIL", OracleDbType.Varchar2, 100, pastrEmail}, _
                                       {"VI_VA_FORMA_PAGO_PREFERIDA", OracleDbType.Varchar2, 64, pastrFormaPago}, _
                                       {"VI_VA_DNI", OracleDbType.Varchar2, 20, pastrDni}, _
                                       {"VI_VA_RUC", OracleDbType.Varchar2, 20, pastrRuc}, _
                                       {"VI_VA_NOMBRE_REPRESENTANTE_LEG", OracleDbType.Varchar2, 128, pastrRepresentante}, _
                                       {"VI_VA_CONTACTO", OracleDbType.Varchar2, 100, pastrContacto}, _
                                       {"VI_VA_DPPM", OracleDbType.Varchar2, 1, pastrDPPM}, _
                                       {"VI_VA_AGENTE_RETENCION", OracleDbType.Varchar2, 1, pastrAgenteRetencion}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_INS_CLIENTE"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    '*************************************************************************************
    'Descripción: Inserta un registro de Cliente
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FT_Mod_Cliente(ByVal pastrCodigo As String, ByVal pastrNombre As String, _
                                   ByVal pastrTipo As String, ByVal pastrDireccion As String, _
                                   ByVal pastrDistrito As String, ByVal pastrProvincia As String, _
                                   ByVal pastrDepartamento As String, ByVal pastrPais As String, _
                                   ByVal pastrTelefono As String, ByVal pastrFax As String, _
                                   ByVal pastrEmail As String, ByVal pastrFormaPago As String, _
                                   ByVal pastrDni As String, ByVal pastrRuc As String, _
                                   ByVal pastrRepresentante As String, ByVal pastrContacto As String, _
                                   ByVal pastrDPPM As String, ByVal pastrAgenteRetencion As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO", OracleDbType.Varchar2, 11, pastrCodigo}, _
                                       {"VI_VA_NOMBRE", OracleDbType.Varchar2, 128, pastrNombre}, _
                                       {"VI_VA_TIPO_CLIENTE", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_VA_DIRECCION", OracleDbType.Varchar2, 200, pastrDireccion}, _
                                       {"VI_VA_DISTRITO", OracleDbType.Varchar2, 64, pastrDistrito}, _
                                       {"VI_VA_PROVINCIA", OracleDbType.Varchar2, 64, pastrProvincia}, _
                                       {"VI_VA_DEPARTAMENTO", OracleDbType.Varchar2, 64, pastrDepartamento}, _
                                       {"VI_VA_PAIS", OracleDbType.Varchar2, 64, pastrPais}, _
                                       {"VI_VA_TELEFONO", OracleDbType.Varchar2, 100, pastrTelefono}, _
                                       {"VI_VA_FAX", OracleDbType.Varchar2, 100, pastrFax}, _
                                       {"VI_VA_EMAIL", OracleDbType.Varchar2, 100, pastrEmail}, _
                                       {"VI_VA_FORMA_PAGO_PREFERIDA", OracleDbType.Varchar2, 64, pastrFormaPago}, _
                                       {"VI_VA_DNI", OracleDbType.Varchar2, 20, pastrDni}, _
                                       {"VI_VA_RUC", OracleDbType.Varchar2, 20, pastrRuc}, _
                                       {"VI_VA_NOMBRE_REPRESENTANTE_LEG", OracleDbType.Varchar2, 128, pastrRepresentante}, _
                                       {"VI_VA_CONTACTO", OracleDbType.Varchar2, 100, pastrContacto}, _
                                       {"VI_VA_DPPM", OracleDbType.Varchar2, 1, pastrDPPM}, _
                                       {"VI_VA_AGENTE_RETENCION", OracleDbType.Varchar2, 1, pastrAgenteRetencion}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_UPD_CLIENTE"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    '*****************************************************************************************************
    'Descripción: Elimina un registro de Cliente
    'Fecha: 27/03/2006 
    '****************************************************************************************************
    Public Function FT_Eli_Cliente(ByVal pastrCodigo As String) As String
        Dim lstrprocedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim arrParametros(,) As Object = {{"VI_VA_CODIGO", OracleDbType.Varchar2, 11, pastrCodigo}}

        lstrprocedimiento = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_DEL_CLIENTE"

        Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrprocedimiento, arrParametros)
        objDatos = Nothing
    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registros de Cliente
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Clientes(ByVal pastrNombre As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_NOMBRE", OracleDbType.Varchar2, 128, pastrNombre}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_CLIENTES"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registro de Cliente
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Cliente(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO", OracleDbType.Varchar2, 11, pastrCodigo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_CLIENTE"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Inserta un registro de Cliente
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FT_Ins_Producto(ByVal pastrClase As String, ByVal pastrSerie As String, _
                                    ByVal pastrNumero As String, ByVal pastrTitulo As String, _
                                    ByVal pastrUnidad As String, ByVal pastrPrecio As String, _
                                    ByVal pastrMoneda As String, ByVal pastrStock As String, _
                                    ByVal pastrPuntoReposicion As String, ByVal pastrAfectoDescuento As String, _
                                    ByVal pastrCuentaContable20 As String, ByVal pastrCuentaContable12 As String, _
                                    ByVal pastrObservaciones As String, ByVal pastrArchivoCaratula As String, _
                                    ByVal pastrArchivoCaratulaPequenho As String, ByVal pastrIdioma As String, _
                                    ByVal pastrFecha As String, ByVal pastrParteTomo As String, _
                                    ByVal pastrCodigoAlmacen As String, ByVal pastrCodigoInterno As String, _
                                    ByVal pastrBoletin As String, ByVal pastrUsuario As String, _
                                    ByVal pastrValorAdquisicion As String, ByVal pastrDetalles As String, _
                                    ByVal pastrResumen As String, ByVal pastrCostoPromedio As String, _
                                    ByVal pastrTipoOperacion As String, ByVal pastrClasePadre As String, _
                                    ByVal pastrSeriePadre As String, ByVal pastrNumeroPadre As String, _
                                    ByVal pastrRegimenDetraccion As String, ByVal pastrRequiereDatoCds As String) As String

        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CLASE", OracleDbType.Varchar2, 2, pastrClase}, _
                                       {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
                                       {"VI_VA_NUMERO", OracleDbType.Varchar2, 5, pastrNumero}, _
                                       {"VI_VA_TITULO", OracleDbType.Varchar2, 255, pastrTitulo}, _
                                       {"VI_VA_UNIDAD_MEDIDA", OracleDbType.Varchar2, 3, pastrUnidad}, _
                                       {"VI_VA_PRECIO", OracleDbType.Varchar2, 13, pastrPrecio}, _
                                       {"VI_VA_MONEDA", OracleDbType.Varchar2, 3, pastrMoneda}, _
                                       {"VI_VA_STOCK", OracleDbType.Varchar2, 13, pastrStock}, _
                                       {"VI_VA_PUNTO_REPOSICION", OracleDbType.Varchar2, 13, pastrPuntoReposicion}, _
                                       {"VI_VA_AFECTO_DESCUENTO_30", OracleDbType.Varchar2, 1, pastrAfectoDescuento}, _
                                       {"VI_VA_CUENTA_CONTABLE_20", OracleDbType.Varchar2, 10, pastrCuentaContable20}, _
                                       {"VI_VA_CUENTA_CONTABLE_12", OracleDbType.Varchar2, 10, pastrCuentaContable12}, _
                                       {"VI_VA_OBSERVACIONES", OracleDbType.Varchar2, 1024, pastrObservaciones}, _
                                       {"VI_VA_ARCHIVO_CARATULA", OracleDbType.Varchar2, 64, pastrArchivoCaratula}, _
                                       {"VI_VA_ARCHIVO_CARATULA_PEQUENO", OracleDbType.Varchar2, 64, pastrArchivoCaratulaPequenho}, _
                                       {"VI_VA_IDIOMA", OracleDbType.Varchar2, 20, pastrIdioma}, _
                                       {"VI_VA_FECHA_PUBLICACION", OracleDbType.Varchar2, 8, pastrFecha}, _
                                       {"VI_VA_PARTE_TOMO", OracleDbType.Varchar2, 256, pastrParteTomo}, _
                                       {"VI_VA_CODIGO_EN_ALMACEN", OracleDbType.Varchar2, 10, pastrCodigoAlmacen}, _
                                       {"VI_VA_CODIGO_INTERNO", OracleDbType.Varchar2, 10, pastrCodigoInterno}, _
                                       {"VI_VA_BOLETIN", OracleDbType.Varchar2, 10, pastrBoletin}, _
                                       {"VI_VA_USUARIO_CREACION", OracleDbType.Varchar2, 32, pastrUsuario}, _
                                       {"VI_VA_VALOR_ADQUISICION", OracleDbType.Varchar2, 13, pastrValorAdquisicion}, _
                                       {"VI_VA_DETALLES", OracleDbType.Varchar2, 128, pastrDetalles}, _
                                       {"VI_VA_RESUMEN", OracleDbType.Clob, 4000000, pastrResumen}, _
                                       {"VI_VA_COSTO_PROMEDIO", OracleDbType.Varchar2, 13, pastrCostoPromedio}, _
                                       {"VI_VA_TIPO_OPERACION", OracleDbType.Varchar2, 2, pastrTipoOperacion}, _
                                       {"VI_VA_CLASE_PADRE", OracleDbType.Varchar2, 2, pastrClasePadre}, _
                                       {"VI_VA_SERIE_PADRE", OracleDbType.Varchar2, 3, pastrSeriePadre}, _
                                       {"VI_VA_NUMERO_PADRE", OracleDbType.Varchar2, 5, pastrNumeroPadre}, _
                                       {"VI_VA_SUJETO_REGIMEN_DETRACCIO", OracleDbType.Varchar2, 1, pastrRegimenDetraccion}, _
                                       {"VI_VA_REQUIERE_DATO_CDS", OracleDbType.Varchar2, 1, pastrRequiereDatoCds}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_INS_PRODUCTO"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    '*************************************************************************************
    'Descripción: Modifica un registro de Producto
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FT_Mod_Producto(ByVal pastrClase As String, ByVal pastrSerie As String, _
                                    ByVal pastrNumero As String, ByVal pastrTitulo As String, _
                                    ByVal pastrUnidad As String, ByVal pastrPrecio As String, _
                                    ByVal pastrMoneda As String, ByVal pastrStock As String, _
                                    ByVal pastrPuntoReposicion As String, ByVal pastrAfectoDescuento As String, _
                                    ByVal pastrCuentaContable20 As String, ByVal pastrCuentaContable12 As String, _
                                    ByVal pastrObservaciones As String, ByVal pastrArchivoCaratula As String, _
                                    ByVal pastrArchivoCaratulaPequenho As String, ByVal pastrIdioma As String, _
                                    ByVal pastrFecha As String, ByVal pastrParteTomo As String, _
                                    ByVal pastrCodigoAlmacen As String, ByVal pastrCodigoInterno As String, _
                                    ByVal pastrBoletin As String, ByVal pastrValorAdquisicion As String, _
                                    ByVal pastrDetalles As String, ByVal pastrResumen As String, _
                                    ByVal pastrCostoPromedio As String, ByVal pastrTipoOperacion As String, _
                                    ByVal pastrClasePadre As String, ByVal pastrSeriePadre As String, _
                                    ByVal pastrNumeroPadre As String, ByVal pastrRegimenDetraccion As String, _
                                    ByVal pastrRequiereDatoCds As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CLASE", OracleDbType.Varchar2, 2, pastrClase}, _
                                       {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
                                       {"VI_VA_NUMERO", OracleDbType.Varchar2, 5, pastrNumero}, _
                                       {"VI_VA_TITULO", OracleDbType.Varchar2, 255, pastrTitulo}, _
                                       {"VI_VA_UNIDAD_MEDIDA", OracleDbType.Varchar2, 3, pastrUnidad}, _
                                       {"VI_VA_PRECIO", OracleDbType.Varchar2, 13, pastrPrecio}, _
                                       {"VI_VA_MONEDA", OracleDbType.Varchar2, 3, pastrMoneda}, _
                                       {"VI_VA_STOCK", OracleDbType.Varchar2, 13, pastrStock}, _
                                       {"VI_VA_PUNTO_REPOSICION", OracleDbType.Varchar2, 13, pastrPuntoReposicion}, _
                                       {"VI_VA_AFECTO_DESCUENTO_30", OracleDbType.Varchar2, 1, pastrAfectoDescuento}, _
                                       {"VI_VA_CUENTA_CONTABLE_20", OracleDbType.Varchar2, 10, pastrCuentaContable20}, _
                                       {"VI_VA_CUENTA_CONTABLE_12", OracleDbType.Varchar2, 10, pastrCuentaContable12}, _
                                       {"VI_VA_OBSERVACIONES", OracleDbType.Varchar2, 1024, pastrObservaciones}, _
                                       {"VI_VA_ARCHIVO_CARATULA", OracleDbType.Varchar2, 64, pastrArchivoCaratula}, _
                                       {"VI_VA_ARCHIVO_CARATULA_PEQUENO", OracleDbType.Varchar2, 64, pastrArchivoCaratulaPequenho}, _
                                       {"VI_VA_IDIOMA", OracleDbType.Varchar2, 20, pastrIdioma}, _
                                       {"VI_VA_FECHA_PUBLICACION", OracleDbType.Varchar2, 8, pastrFecha}, _
                                       {"VI_VA_PARTE_TOMO", OracleDbType.Varchar2, 256, pastrParteTomo}, _
                                       {"VI_VA_CODIGO_EN_ALMACEN", OracleDbType.Varchar2, 10, pastrCodigoAlmacen}, _
                                       {"VI_VA_CODIGO_INTERNO", OracleDbType.Varchar2, 10, pastrCodigoInterno}, _
                                       {"VI_VA_BOLETIN", OracleDbType.Varchar2, 10, pastrBoletin}, _
                                       {"VI_VA_VALOR_ADQUISICION", OracleDbType.Varchar2, 13, pastrValorAdquisicion}, _
                                       {"VI_VA_DETALLES", OracleDbType.Varchar2, 128, pastrDetalles}, _
                                       {"VI_VA_RESUMEN", OracleDbType.Clob, 4000000, pastrResumen}, _
                                       {"VI_VA_COSTO_PROMEDIO", OracleDbType.Varchar2, 13, pastrCostoPromedio}, _
                                       {"VI_VA_TIPO_OPERACION", OracleDbType.Varchar2, 2, pastrTipoOperacion}, _
                                       {"VI_VA_CLASE_PADRE", OracleDbType.Varchar2, 2, pastrClasePadre}, _
                                       {"VI_VA_SERIE_PADRE", OracleDbType.Varchar2, 3, pastrSeriePadre}, _
                                       {"VI_VA_NUMERO_PADRE", OracleDbType.Varchar2, 5, pastrNumeroPadre}, _
                                       {"VI_VA_SUJETO_REGIMEN_DETRACCIO", OracleDbType.Varchar2, 1, pastrRegimenDetraccion}, _
                                       {"VI_VA_REQUIERE_DATO_CDS", OracleDbType.Varchar2, 1, pastrRequiereDatoCds}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_UPD_PRODUCTO"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    '*****************************************************************************************************
    'Descripción: Elimina un registro de Producto
    'Fecha: 27/03/2006 
    '****************************************************************************************************
    Public Function FT_Eli_Producto(ByVal pastrClase As String, ByVal pastrSerie As String, _
                                    ByVal pastrNumero As String) As String
        Dim lstrprocedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim arrParametros(,) As Object = {{"VI_VA_CLASE", OracleDbType.Varchar2, 2, pastrClase}, _
                                          {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
                                          {"VI_VA_NUMERO", OracleDbType.Varchar2, 5, pastrNumero}}

        lstrprocedimiento = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_DEL_PRODUCTO"

        Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrprocedimiento, arrParametros)
        objDatos = Nothing
    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registros de Producto
    'Fecha: 28/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Productos(ByVal pastrTitulo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TITULO", OracleDbType.Varchar2, 128, pastrTitulo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_PRODUCTOS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registro de Producto
    'Fecha: 28/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Producto(ByVal pastrClase As String, ByVal pastrSerie As String, _
                                    ByVal pastrNumero As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CLASE", OracleDbType.Varchar2, 2, pastrClase}, _
                                       {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
                                       {"VI_VA_NUMERO", OracleDbType.Varchar2, 5, pastrNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_PRODUCTO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Inserta un registro de Movimiento de Almacén
    'Fecha: 26/04/2006 
    '*************************************************************************************
    Public Function FT_Ins_MovAlmacen(ByVal pastrCodigoAlmacen As String, ByVal pastrTipoMovimiento As String, _
                                      ByVal paintNumero As Int64, ByVal pastrTipoOperacion As String, _
                                      ByVal pastrCodigoProveedor As String, ByVal pastrFecha As String, _
                                      ByVal pastrTipoDocReferencia As String, _
                                      ByVal pastrNumeroDocReferencia As String, ByVal pastrDetalle As String, _
                                      ByVal pastrCantidad As String, ByVal pastrUnidadMedida As String, _
                                      ByVal pastrPrecioUnitario As String, ByVal pastrClaseProducto As String, _
                                      ByVal pastrSerieProducto As String, ByVal pastrNumeroProducto As String, _
                                      ByVal pastrSubTotal As String, ByVal pastrMoneda As String, _
                                      ByVal pastrTipoCambio As String, ByVal pastrHora As String, _
                                      ByVal pastrCodigoDependencia As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO_ALMACEN", OracleDbType.Varchar2, 2, pastrCodigoAlmacen}, _
                                       {"VI_VA_TIPO_MOVIMIENTO", OracleDbType.Varchar2, 10, pastrTipoMovimiento}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int64, 6, paintNumero}, _
                                       {"VI_VA_TIPO_OPERACION", OracleDbType.Varchar2, 2, pastrTipoOperacion}, _
                                       {"VI_VA_CODIGO_PROVEEDOR", OracleDbType.Varchar2, 11, pastrCodigoProveedor}, _
                                       {"VI_VA_FECHA", OracleDbType.Varchar2, 10, pastrFecha}, _
                                       {"VI_VA_TIPO_DOC_REFERENCIA", OracleDbType.Varchar2, 2, pastrTipoDocReferencia}, _
                                       {"VI_VA_NUMERO_DOC_REFERENCIA", OracleDbType.Varchar2, 10, pastrNumeroDocReferencia}, _
                                       {"VI_VA_DETALLE", OracleDbType.Varchar2, 255, pastrDetalle}, _
                                       {"VI_VA_CANTIDAD", OracleDbType.Clob, 4000000, pastrCantidad}, _
                                       {"VI_VA_UNIDAD_MEDIDA", OracleDbType.Clob, 4000000, pastrUnidadMedida}, _
                                       {"VI_VA_PRECIO_UNITARIO", OracleDbType.Clob, 4000000, pastrPrecioUnitario}, _
                                       {"VI_VA_CLASE_PRODUCTO", OracleDbType.Clob, 4000000, pastrClaseProducto}, _
                                       {"VI_VA_SERIE_PRODUCTO", OracleDbType.Clob, 4000000, pastrSerieProducto}, _
                                       {"VI_VA_NUMERO_PRODUCTO", OracleDbType.Clob, 4000000, pastrNumeroProducto}, _
                                       {"VI_VA_SUBTOTAL", OracleDbType.Clob, 4000000, pastrSubTotal}, _
                                       {"VI_VA_MONEDA", OracleDbType.Clob, 4000000, pastrMoneda}, _
                                       {"VI_VA_TIPO_CAMBIO", OracleDbType.Clob, 4000000, pastrTipoCambio}, _
                                       {"VI_VA_HORA", OracleDbType.Varchar2, 10, pastrHora}, _
                                       {"VI_VA_CODIGO_DEPENDENCIA", OracleDbType.Varchar2, 11, pastrCodigoDependencia}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_INS_MOVALMACEN"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Actualiza un registro de Movimiento de Almacén
    'Fecha: 26/04/2006 
    '*************************************************************************************
    Public Function FT_Mod_MovAlmacen(ByVal pastrCodigoAlmacen As String, ByVal pastrTipoMovimiento As String, _
                                      ByVal paintNumero As Int64, ByVal pastrTipoOperacion As String, _
                                      ByVal pastrCodigoProveedor As String, ByVal pastrFecha As String, _
                                      ByVal pastrTipoDocReferencia As String, _
                                      ByVal pastrNumeroDocReferencia As String, ByVal pastrDetalle As String, _
                                      ByVal pastrCantidad As String, ByVal pastrUnidadMedida As String, _
                                      ByVal pastrPrecioUnitario As String, ByVal pastrClaseProducto As String, _
                                      ByVal pastrSerieProducto As String, ByVal pastrNumeroProducto As String, _
                                      ByVal pastrSubTotal As String, ByVal pastrMoneda As String, _
                                      ByVal pastrTipoCambio As String, ByVal pastrHora As String, _
                                      ByVal pastrCodigoDependencia As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO_ALMACEN", OracleDbType.Varchar2, 2, pastrCodigoAlmacen}, _
                                       {"VI_VA_TIPO_MOVIMIENTO", OracleDbType.Varchar2, 10, pastrTipoMovimiento}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int64, 6, paintNumero}, _
                                       {"VI_VA_TIPO_OPERACION", OracleDbType.Varchar2, 2, pastrTipoOperacion}, _
                                       {"VI_VA_CODIGO_PROVEEDOR", OracleDbType.Varchar2, 11, pastrCodigoProveedor}, _
                                       {"VI_VA_FECHA", OracleDbType.Varchar2, 10, pastrFecha}, _
                                       {"VI_VA_TIPO_DOC_REFERENCIA", OracleDbType.Varchar2, 2, pastrTipoDocReferencia}, _
                                       {"VI_VA_NUMERO_DOC_REFERENCIA", OracleDbType.Varchar2, 10, pastrNumeroDocReferencia}, _
                                       {"VI_VA_DETALLE", OracleDbType.Varchar2, 255, pastrDetalle}, _
                                       {"VI_VA_CANTIDAD", OracleDbType.Clob, 4000000, pastrCantidad}, _
                                       {"VI_VA_UNIDAD_MEDIDA", OracleDbType.Clob, 4000000, pastrUnidadMedida}, _
                                       {"VI_VA_PRECIO_UNITARIO", OracleDbType.Clob, 4000000, pastrPrecioUnitario}, _
                                       {"VI_VA_CLASE_PRODUCTO", OracleDbType.Clob, 4000000, pastrClaseProducto}, _
                                       {"VI_VA_SERIE_PRODUCTO", OracleDbType.Clob, 4000000, pastrSerieProducto}, _
                                       {"VI_VA_NUMERO_PRODUCTO", OracleDbType.Clob, 4000000, pastrNumeroProducto}, _
                                       {"VI_VA_SUBTOTAL", OracleDbType.Clob, 4000000, pastrSubTotal}, _
                                       {"VI_VA_MONEDA", OracleDbType.Clob, 4000000, pastrMoneda}, _
                                       {"VI_VA_TIPO_CAMBIO", OracleDbType.Clob, 4000000, pastrTipoCambio}, _
                                       {"VI_VA_HORA", OracleDbType.Varchar2, 10, pastrHora}, _
                                       {"VI_VA_CODIGO_DEPENDENCIA", OracleDbType.Varchar2, 11, pastrCodigoDependencia}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_UPD_MOVALMACEN"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*****************************************************************************************************
    'Descripción: Anula un registro de Movimiento de Almacén
    'Fecha: 26/04/2006 
    '****************************************************************************************************
    Public Function FT_Anu_MovAlmacen(ByVal pastrCodigoAlmacen As String, ByVal pastrTipoMovimiento As String, _
                                      ByVal paintNumero As Int64) As String
        Dim lstrprocedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim arrParametros(,) As Object = {{"VI_VA_CODIGO_ALMACEN", OracleDbType.Varchar2, 2, pastrCodigoAlmacen}, _
                                          {"VI_VA_TIPO_MOVIMIENTO", OracleDbType.Varchar2, 10, pastrTipoMovimiento}, _
                                          {"VI_NU_NUMERO", OracleDbType.Int64, 6, paintNumero}}

        lstrprocedimiento = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_ANU_MOVALMACEN"

        Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrprocedimiento, arrParametros)
        objDatos = Nothing
    End Function

    '*****************************************************************************************************
    'Descripción: Aprueba un registro de Movimiento de Almacén
    'Fecha: 26/04/2006 
    '****************************************************************************************************
    Public Function FT_Apr_MovAlmacen(ByVal pastrCodigoAlmacen As String, ByVal pastrTipoMovimiento As String, _
                                      ByVal paintNumero As Int64) As String
        Dim lstrprocedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim arrParametros(,) As Object = {{"VI_VA_CODIGO_ALMACEN", OracleDbType.Varchar2, 2, pastrCodigoAlmacen}, _
                                          {"VI_VA_TIPO_MOVIMIENTO", OracleDbType.Varchar2, 10, pastrTipoMovimiento}, _
                                          {"VI_NU_NUMERO", OracleDbType.Int64, 6, paintNumero}}

        lstrprocedimiento = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_APR_MOVALMACEN"

        Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrprocedimiento, arrParametros)
        objDatos = Nothing
    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registro de Movimiento de Almacen
    'Fecha: 04/05/2006 
    '*************************************************************************************
    Public Function FS_Sel_Mov_Almacen(ByVal pastrCodigoAlmacen As String, ByVal pastrTipoMovimiento As String, _
                                       ByVal paintNumero As Int64) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO_ALMACEN", OracleDbType.Varchar2, 2, pastrCodigoAlmacen}, _
                                       {"VI_VA_TIPO_MOVIMIENTO", OracleDbType.Varchar2, 10, pastrTipoMovimiento}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int64, 6, paintNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_MOVALMACEN"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registro de Movimiento de Almacen
    'Fecha: 04/05/2006 
    '*************************************************************************************
    Public Function FS_Sel_Det_Mov_Almacen(ByVal pastrCodigoAlmacen As String, ByVal pastrTipoMovimiento As String, _
                                           ByVal paintNumero As Int64) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO_ALMACEN", OracleDbType.Varchar2, 2, pastrCodigoAlmacen}, _
                                       {"VI_VA_TIPO_MOVIMIENTO", OracleDbType.Varchar2, 10, pastrTipoMovimiento}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int64, 6, paintNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_DET_MOVALMACEN"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*****************************************************************************************************
    'Descripción: Registrar Cierre de Mes
    'Fecha: 26/04/2006 
    '****************************************************************************************************
    Public Function FT_Ins_Cierre_Mes(ByVal pastrCodigoAlmacen As String, ByVal pastrAnho As String, _
                                      ByVal pastrMes As String, ByVal padblTipoCambio As Double) As String
        Dim lstrprocedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim arrParametros(,) As Object = {{"VI_VA_CODIGO_ALMACEN", OracleDbType.Varchar2, 2, pastrCodigoAlmacen}, _
                                          {"VI_VA_ANHO", OracleDbType.Varchar2, 4, pastrAnho}, _
                                          {"VI_VA_MES", OracleDbType.Varchar2, 2, pastrMes}, _
                                          {"VI_NU_TIPO_CAMBIO", OracleDbType.Double, 11, padblTipoCambio}}

        lstrprocedimiento = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_INS_CIERRE_MES"

        Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrprocedimiento, arrParametros)
        objDatos = Nothing
    End Function

    '*************************************************************************************
    'Descripción: Inserta un registro de Proveedor
    'Fecha: 25/04/2006 
    '*************************************************************************************
    Public Function FT_Ins_Proveedor(ByVal pastrNombre As String, ByVal pastrTipo As String, _
                                     ByVal pastrRuc As String, ByVal pastrDni As String, _
                                     ByVal pastrDireccion As String, ByVal pastrTelefono As String, _
                                     ByVal pastrEmail As String, ByVal pastrFax As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_NOMBRE", OracleDbType.Varchar2, 128, pastrNombre}, _
                                       {"VI_VA_TIPO_PERSONA", OracleDbType.Varchar2, 1, pastrTipo}, _
                                       {"VI_VA_RUC", OracleDbType.Varchar2, 20, pastrRuc}, _
                                       {"VI_VA_DNI", OracleDbType.Varchar2, 20, pastrDni}, _
                                       {"VI_VA_DIRECCION", OracleDbType.Varchar2, 200, pastrDireccion}, _
                                       {"VI_VA_TELEFONO", OracleDbType.Varchar2, 100, pastrTelefono}, _
                                       {"VI_VA_EMAIL", OracleDbType.Varchar2, 100, pastrEmail}, _
                                       {"VI_VA_FAX", OracleDbType.Varchar2, 100, pastrFax}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_INS_PROVEEDOR"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    '*************************************************************************************
    'Descripción: Inserta un registro de Proveedor
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FT_Mod_Proveedor(ByVal pastrCodigo As String, ByVal pastrNombre As String, _
                                     ByVal pastrTipo As String, ByVal pastrRuc As String, _
                                     ByVal pastrDni As String, ByVal pastrDireccion As String, _
                                     ByVal pastrTelefono As String, ByVal pastrEmail As String, _
                                     ByVal pastrFax As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO", OracleDbType.Varchar2, 11, pastrCodigo}, _
                                       {"VI_VA_NOMBRE", OracleDbType.Varchar2, 128, pastrNombre}, _
                                       {"VI_VA_TIPO_PERSONA", OracleDbType.Varchar2, 1, pastrTipo}, _
                                       {"VI_VA_RUC", OracleDbType.Varchar2, 20, pastrRuc}, _
                                       {"VI_VA_DNI", OracleDbType.Varchar2, 20, pastrDni}, _
                                       {"VI_VA_DIRECCION", OracleDbType.Varchar2, 200, pastrDireccion}, _
                                       {"VI_VA_TELEFONO", OracleDbType.Varchar2, 100, pastrTelefono}, _
                                       {"VI_VA_EMAIL", OracleDbType.Varchar2, 100, pastrEmail}, _
                                       {"VI_VA_FAX", OracleDbType.Varchar2, 100, pastrFax}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_UPD_PROVEEDOR"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    '*****************************************************************************************************
    'Descripción: Elimina un registro de Proveedor
    'Fecha: 27/03/2006 
    '****************************************************************************************************
    Public Function FT_Eli_Proveedor(ByVal pastrCodigo As String) As String
        Dim lstrprocedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim arrParametros(,) As Object = {{"VI_VA_CODIGO", OracleDbType.Varchar2, 11, pastrCodigo}}

        lstrprocedimiento = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_DEL_PROVEEDOR"

        Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrprocedimiento, arrParametros)
        objDatos = Nothing
    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registros de Proveedor
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Proveedores(ByVal pastrNombre As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_NOMBRE", OracleDbType.Varchar2, 128, pastrNombre}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_PROVEEDORES"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registro de Proveedor
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Proveedor(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO", OracleDbType.Varchar2, 11, pastrCodigo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_PROVEEDOR"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor
    'Fecha: 05/05/2006 
    '*************************************************************************************
    Public Function FS_Sel_Numeracion(ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_NUMERACION"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Actualiza la Numeración de Documentos
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FT_Mod_Numeracion(ByVal pastrTipo As String, ByVal paintSerie As Int64, _
                                      ByVal paintNumero As Int64) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
                                       {"VI_NU_SERIE", OracleDbType.Int64, 20, paintSerie}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int64, 20, paintNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_UPD_NUMERACION"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor
    'Fecha: 09/05/2006 
    '*************************************************************************************
    Public Function FS_Sel_Dependencia(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_CODIGO", OracleDbType.Varchar2, 11, pastrCodigo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_DEPENDENCIA"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function



    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    ''*************************************************************************************
    ''Descripción: Búsqueda de registro de Documento de Pago
    ''Fecha: 21/03/2006 
    ''*************************************************************************************
    'Public Function FS_Sel_Doc_Venta(ByVal pastrTipo As String, ByVal pastrSerie As String, _
    '                                ByVal pastrNumero As String) As DataTable
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
    '                                   {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
    '                                   {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}}

    '    Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_DOC_PAGO"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try

    'End Function

    ''*************************************************************************************
    ''Descripción: Búsqueda de registro de Items del Documento de Pago
    ''Fecha: 21/03/2006 
    ''*************************************************************************************
    'Public Function FS_Sel_Doc_Venta_Item(ByVal pastrTipo As String, ByVal pastrSerie As String, _
    '                                      ByVal pastrNumero As String) As DataTable
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
    '                                   {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
    '                                   {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}}

    '    Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_DOC_PAGO_ITEM"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try

    'End Function

    ''*************************************************************************************
    ''Descripción: Búsqueda de registro de Items del Documento de Pago
    ''Fecha: 21/03/2006 
    ''*************************************************************************************
    'Public Function FS_Sel_Doc_Venta_Item_Imp(ByVal pastrTipo As String, ByVal pastrSerie As String, _
    '                                          ByVal pastrNumero As String) As DataTable
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
    '                                   {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
    '                                   {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}}

    '    Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_DOC_PAGO_ITEM_IMP"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try

    'End Function

    ''*************************************************************************************
    ''Descripción: Búsqueda de registro de Items del Documento de Pago con precios actualizados
    ''Fecha: 21/03/2006 
    ''*************************************************************************************
    'Public Function FS_Sel_Doc_Venta_Item_Act(ByVal pastrTipo As String, ByVal pastrSerie As String, _
    '                                          ByVal pastrNumero As String) As DataTable
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
    '                                   {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
    '                                   {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}}

    '    Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_DOC_PAGO_ITEM_ACT"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try

    'End Function

    '*************************************************************************************
    'Descripción: Inserta un registro de Documento de Venta
    'Fecha: 20/03/2006 
    '*************************************************************************************
    Public Function FT_Ins_Recibo(ByVal paintNumero As Int64, ByVal pastrCodigoCliente As String, _
                                  ByVal pastrFecha As String, ByVal padblTotal As Double, _
                                  ByVal pastrMoneda As String, ByVal pastrUsuarioRegistro As String, _
                                  ByVal pastrGlosa As String, ByVal pastrReferencia As String, _
                                  ByVal pastrCuentas As String, ByVal padblTipoCambio As Double, _
                                  ByVal pastrTipoProveedor As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_NUMERO", OracleDbType.Int64, 10, paintNumero}, _
                                       {"VI_VA_CODIGO_CLIENTE", OracleDbType.Varchar2, 11, pastrCodigoCliente}, _
                                       {"VI_VA_FECHA", OracleDbType.Varchar2, 10, pastrFecha}, _
                                       {"VI_NU_TOTAL", OracleDbType.Double, 15, padblTotal}, _
                                       {"VI_VA_MONEDA", OracleDbType.Varchar2, 64, pastrMoneda}, _
                                       {"VI_VA_USUARIO_REGISTRO", OracleDbType.Varchar2, 32, pastrUsuarioRegistro}, _
                                       {"VI_VA_GLOSA", OracleDbType.Varchar2, 1024, pastrGlosa}, _
                                       {"VI_VA_REFERENCIA", OracleDbType.Varchar2, 512, pastrReferencia}, _
                                       {"VI_VA_CUENTAS", OracleDbType.Varchar2, 1024, pastrCuentas}, _
                                       {"VI_NU_TIPO_CAMBIO", OracleDbType.Double, 15, padblTipoCambio}, _
                                       {"VI_VA_TIPO_PROVEEDOR", OracleDbType.Varchar2, 1, pastrTipoProveedor}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_INS_RECIBO"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Inserta un registro de Documento de Venta
    'Fecha: 20/03/2006 
    '*************************************************************************************
    Public Function FT_Mod_Recibo(ByVal paintNumero As Int64, ByVal pastrCodigoCliente As String, _
                                  ByVal pastrFecha As String, ByVal padblTotal As Double, _
                                  ByVal pastrMoneda As String, ByVal pastrUsuarioRegistro As String, _
                                  ByVal pastrGlosa As String, ByVal pastrReferencia As String, _
                                  ByVal pastrCuentas As String, ByVal padblTipoCambio As Double, _
                                  ByVal pastrTipoProveedor As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_NUMERO", OracleDbType.Int64, 10, paintNumero}, _
                                       {"VI_VA_CODIGO_CLIENTE", OracleDbType.Varchar2, 11, pastrCodigoCliente}, _
                                       {"VI_VA_FECHA", OracleDbType.Varchar2, 10, pastrFecha}, _
                                       {"VI_NU_TOTAL", OracleDbType.Double, 15, padblTotal}, _
                                       {"VI_VA_MONEDA", OracleDbType.Varchar2, 64, pastrMoneda}, _
                                       {"VI_VA_USUARIO_REGISTRO", OracleDbType.Varchar2, 32, pastrUsuarioRegistro}, _
                                       {"VI_VA_GLOSA", OracleDbType.Varchar2, 1024, pastrGlosa}, _
                                       {"VI_VA_REFERENCIA", OracleDbType.Varchar2, 512, pastrReferencia}, _
                                       {"VI_VA_CUENTAS", OracleDbType.Varchar2, 1024, pastrCuentas}, _
                                       {"VI_NU_TIPO_CAMBIO", OracleDbType.Double, 15, padblTipoCambio}, _
                                       {"VI_VA_TIPO_PROVEEDOR", OracleDbType.Varchar2, 1, pastrTipoProveedor}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_UPD_RECIBO"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registro de Recibo
    'Fecha: 15/11/2006 
    '*************************************************************************************
    Public Function FS_Sel_Recibo(ByVal paintNumero As Int64) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_NUMERO", OracleDbType.Int64, 8, paintNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_RECIBO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*****************************************************************************************************
    'Descripción: Anula un registro de Proforma
    'Fecha: 24/03/2006 
    '****************************************************************************************************
    Public Function FT_Anu_Recibo(ByVal paintNumero As Int64) As String
        Dim lstrprocedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim arrParametros(,) As Object = {{"VI_NU_NUMERO", OracleDbType.Int64, 8, paintNumero}}

        lstrprocedimiento = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_ANU_RECIBO"

        Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrprocedimiento, arrParametros)
        objDatos = Nothing
    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registro de Recibo
    'Fecha: 15/11/2006 
    '*************************************************************************************
    Public Function FS_Sel_Cuentas_Bancarias() As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_CUENTAS_BANCARIAS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registro de Recibo
    'Fecha: 15/11/2006 
    '*************************************************************************************
    Public Function FS_Sel_Cuentas_Bancarias_Fecha(ByVal pastrFecha As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_FECHA", OracleDbType.Varchar2, 10, pastrFecha}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_SEL_CUENTAS_BANCARIAS_FECHA"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Inserta un registro de Documento de Venta
    'Fecha: 20/03/2006 
    '*************************************************************************************
    Public Function FT_Ins_Liq_Cuenta(ByVal pastrFecha As String, ByVal pastrCantidad As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_FECHA", OracleDbType.Varchar2, 10, pastrFecha}, _
                                       {"VI_VA_CANTIDAD", OracleDbType.Varchar2, 2000, pastrCantidad}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_INS_LIQ_CUENTA"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Inserta un registro de Documento de Venta
    'Fecha: 20/03/2006 
    '*************************************************************************************
    Public Function FT_Obt_Moneda_Cuenta(ByVal pastrCodigoCuenta As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO_CUENTA", OracleDbType.Varchar2, 64, pastrCodigoCuenta}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_OBT_MONEDA_CUENTA"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    ''*************************************************************************************
    ''Descripción: Inserta un registro de Documento de Venta
    ''Fecha: 20/03/2006 
    ''*************************************************************************************
    'Public Function FT_Mod_Doc_Venta(ByVal pastrTipo As String, ByVal pastrSerie As String, _
    '                                 ByVal pastrNumero As String, ByVal pastrCodigoCliente As String, _
    '                                 ByVal pastrFecha As String, ByVal padblTipoCambio As Double, _
    '                                 ByVal padblValorVenta As Double, ByVal padblIncrementosVarios As Double, _
    '                                 ByVal padblDescuento As Double, ByVal padblIGV As Double, _
    '                                 ByVal padblTotal As Double, ByVal pastrMoneda As String, _
    '                                 ByVal pastrFormaPago As String, ByVal pastrNumeroPedido As String, _
    '                                 ByVal pastrCantidad As String, ByVal pastrUnidadMedida As String, _
    '                                 ByVal pastrPrecioUnitario As String, ByVal pastrClaseProducto As String, _
    '                                 ByVal pastrSerieProducto As String, ByVal pastrNumeroProducto As String, _
    '                                 ByVal pastrSubTotal As String, ByVal paobjDetalle As Object, _
    '                                 ByVal paobjCd As Object, ByVal pastrGlosa As String) As String
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
    '                                   {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
    '                                   {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}, _
    '                                   {"VI_VA_CODIGO_CLIENTE", OracleDbType.Varchar2, 11, pastrCodigoCliente}, _
    '                                   {"VI_VA_FECHA", OracleDbType.Varchar2, 10, pastrFecha}, _
    '                                   {"VI_NU_TIPO_CAMBIO", OracleDbType.Double, 15, padblTipoCambio}, _
    '                                   {"VI_NU_VALOR_VENTA", OracleDbType.Double, 15, padblValorVenta}, _
    '                                   {"VI_NU_INCREMENTOS_VARIOS", OracleDbType.Double, 15, padblIncrementosVarios}, _
    '                                   {"VI_NU_DESCUENTO", OracleDbType.Double, 15, padblDescuento}, _
    '                                   {"VI_NU_IGV", OracleDbType.Double, 15, padblIGV}, _
    '                                   {"VI_NU_TOTAL", OracleDbType.Double, 15, padblTotal}, _
    '                                   {"VI_VA_MONEDA", OracleDbType.Varchar2, 64, pastrMoneda}, _
    '                                   {"VI_VA_FORMA_PAGO", OracleDbType.Varchar2, 64, pastrFormaPago}, _
    '                                   {"VI_VA_NUMERO_PEDIDO", OracleDbType.Varchar2, 8, pastrNumeroPedido}, _
    '                                   {"VI_VA_CANTIDAD", OracleDbType.Clob, 4000000, pastrCantidad}, _
    '                                   {"VI_VA_UNIDAD_MEDIDA", OracleDbType.Clob, 4000000, pastrUnidadMedida}, _
    '                                   {"VI_VA_PRECIO_UNITARIO", OracleDbType.Clob, 4000000, pastrPrecioUnitario}, _
    '                                   {"VI_VA_CLASE_PRODUCTO", OracleDbType.Clob, 4000000, pastrClaseProducto}, _
    '                                   {"VI_VA_SERIE_PRODUCTO", OracleDbType.Clob, 4000000, pastrSerieProducto}, _
    '                                   {"VI_VA_NUMERO_PRODUCTO", OracleDbType.Clob, 4000000, pastrNumeroProducto}, _
    '                                   {"VI_VA_SUBTOTAL", OracleDbType.Clob, 4000000, pastrSubTotal}, _
    '                                   {"VI_VA_DETALLE", OracleDbType.Clob, 4000000, paobjDetalle}, _
    '                                   {"VI_VA_CD", OracleDbType.Clob, 4000000, paobjCd}, _
    '                                   {"VI_VA_GLOSA", OracleDbType.Varchar2, 256, pastrGlosa}}

    '    Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_UPD_DOC_PAGO"
    '    Try
    '        Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
    '        objDatos = Nothing
    '    Catch
    '        Throw
    '    End Try

    'End Function

    ''*****************************************************************************************************
    ''Descripción: Anula un registro de Documento de Venta
    ''Fecha: 24/03/2006 
    ''****************************************************************************************************
    'Public Function FT_Anu_Doc_Venta(ByVal pastrTipo As String, ByVal pastrSerie As String, _
    '                                 ByVal pastrNumero As String) As String
    '    Dim lstrprocedimiento As String
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    Dim arrParametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
    '                                      {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
    '                                      {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}}

    '    lstrprocedimiento = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_ANU_DOC_PAGO"

    '    Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrprocedimiento, arrParametros)
    '    objDatos = Nothing
    'End Function

    ''*****************************************************************************************************
    ''Descripción: Anula un registro de Documento de Venta
    ''Fecha: 24/03/2006 
    ''****************************************************************************************************
    'Public Function FT_Apr_Anu_Doc_Venta(ByVal pastrTipo As String, ByVal pastrSerie As String, _
    '                                     ByVal pastrNumero As String) As String
    '    Dim lstrprocedimiento As String
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    Dim arrParametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
    '                                      {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
    '                                      {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}}

    '    lstrprocedimiento = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_APR_ANU_DOC_PAGO"

    '    Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrprocedimiento, arrParametros)
    '    objDatos = Nothing
    'End Function

    ''*****************************************************************************************************
    ''Descripción: Anula un registro de Documento de Venta
    ''Fecha: 24/03/2006 
    ''****************************************************************************************************
    'Public Function FT_Anu_Rap_Doc_Venta(ByVal pastrTipo As String, ByVal pastrSerie As String, _
    '                                     ByVal pastrNumero As String, ByVal pastrFecha As String) As String
    '    Dim lstrprocedimiento As String
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    Dim arrParametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
    '                                      {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
    '                                      {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}, _
    '                                      {"VI_VA_FECHA", OracleDbType.Varchar2, 10, pastrFecha}}

    '    lstrprocedimiento = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_ANU_RAP_DOC_PAGO"

    '    Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrprocedimiento, arrParametros)
    '    objDatos = Nothing
    'End Function

    ''*****************************************************************************************************
    ''Descripción: Imprime un registro de Documento de Venta
    ''Fecha: 29/03/2006 
    ''****************************************************************************************************
    'Public Function FT_Imp_Doc_Venta(ByVal pastrTipo As String, ByVal pastrSerie As String, _
    '                                 ByVal pastrNumero As String) As String
    '    Dim lstrprocedimiento As String
    '    Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
    '    Dim arrParametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}, _
    '                                      {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
    '                                      {"VI_VA_NUMERO", OracleDbType.Varchar2, 10, pastrNumero}}

    '    lstrprocedimiento = gstrEsquema & ".PKG_MANTENIMIENTO_FCT.P_IMP_DOC_PAGO"

    '    Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrprocedimiento, arrParametros)
    '    objDatos = Nothing
    'End Function

    Public Function FT_P_TRABAJO_NUMERO(ByVal pastrTipo_Doc_Trabajo As String) As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"V_TIPO_DOC_TRABAJO", OracleDbType.Varchar2, 2, pastrTipo_Doc_Trabajo}}
        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_MUESTRAS_LABORATORIO.P_TRABAJO_NUMERO"
        Try
            Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function




End Class