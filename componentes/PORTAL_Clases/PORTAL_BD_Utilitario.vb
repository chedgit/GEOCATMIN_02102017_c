Option Explicit On 
Imports Oracle.DataAccess.Client

Public Class clsBD_Utilitario

    '*************************************************************************************
    'Descripción: Devuelve un cursor con la lista de clientes
    'Fecha: 15/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Clientes(ByVal pastrNombre As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_NOMBRE", OracleDbType.Varchar2, 128, pastrNombre}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_CLIENTES"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con la lista de clientes
    'Fecha: 15/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Cliente(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO", OracleDbType.Varchar2, 11, pastrCodigo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_CLIENTE"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con la lista de proveedores
    'Fecha: 20/04/2006 
    '*************************************************************************************
    Public Function FS_Sel_Proveedores(ByVal pastrNombre As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_NOMBRE", OracleDbType.Varchar2, 128, pastrNombre}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_PROVEEDORES"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con la lista de clientes
    'Fecha: 15/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Proveedor(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO", OracleDbType.Varchar2, 11, pastrCodigo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_PROVEEDOR"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con la lista de empleados
    'Fecha: 21/11/2006 
    '*************************************************************************************
    Public Function FS_Sel_Empleados(ByVal pastrNombre As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_NOMBRE", OracleDbType.Varchar2, 128, pastrNombre}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_EMPLEADOS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con la lista de clientes
    'Fecha: 15/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Empleado(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO", OracleDbType.Varchar2, 11, pastrCodigo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_EMPLEADO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con la lista de proveedores
    'Fecha: 20/04/2006 
    '*************************************************************************************
    Public Function FS_Sel_Dependencias(ByVal pastrNombre As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_NOMBRE", OracleDbType.Varchar2, 128, pastrNombre}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_DEPENDENCIAS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
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

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_DEPENDENCIA"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con la lista de productos
    'Fecha: 15/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Productos(ByVal pastrNombre As String, ByVal pastrClase As String, _
                                     ByVal pastrSerie As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_NOMBRE", OracleDbType.Varchar2, 255, pastrNombre}, _
                                       {"VI_VA_CLASE", OracleDbType.Varchar2, 2, pastrClase}, _
                                       {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_PRODUCTOS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con la lista de productos
    'Fecha: 24/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Productos_Base(ByVal pastrClase As String, ByVal pastrSerie As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CLASE", OracleDbType.Varchar2, 2, pastrClase}, _
                                       {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_PRODUCTOS_BASE"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con la lista de productos
    'Fecha: 15/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Producto(ByVal pastrClase As String, ByVal pastrSerie As String, _
                                    ByVal pastrNumero As String, ByVal padblTipoCambio As Double) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CLASE", OracleDbType.Varchar2, 2, pastrClase}, _
                                       {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
                                       {"VI_VA_NUMERO", OracleDbType.Varchar2, 5, pastrNumero}, _
                                       {"VI_VA_TIPO_CAMBIO", OracleDbType.Double, 15, padblTipoCambio}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_PRODUCTO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con la lista de productos
    'Fecha: 24/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Producto_Base(ByVal pastrClase As String, ByVal pastrSerie As String, _
                                         ByVal pastrNumero As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CLASE", OracleDbType.Varchar2, 2, pastrClase}, _
                                       {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerie}, _
                                       {"VI_VA_NUMERO", OracleDbType.Varchar2, 5, pastrNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_PRODUCTO_BASE"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registros de Lista de Parámetros
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Parametros(ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 32, pastrTipo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_PARAMETROS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registros de Lista de Parámetros
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Parametro(ByVal pastrTipo As String, ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 32, pastrTipo}, _
                                       {"VI_VA_CODIGO", OracleDbType.Varchar2, 64, pastrCodigo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_PARAMETRO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registros de Lista de Parámetros
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Parametros_Visibles(ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 32, pastrTipo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_PARAMETROS_VISIBLES"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registros de Lista de Parámetros
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Parametros_Varios(ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 32, pastrTipo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_PARAMETROS_VARIOS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registros de Lista de Parámetros
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Parametros_Varios_Ini(ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 32, pastrTipo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_PARAMETROS_VARIOS_INI"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registros de Lista de Parámetros
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Parametros_Ini(ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 32, pastrTipo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_PARAMETROS_INI"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registros de Lista de Parámetros
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Opciones_Reporte(ByVal pastrTipo As String, ByVal pastrDetalle As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 32, pastrTipo}, _
                                       {"VI_VA_DETALLE", OracleDbType.Varchar2, 2000, pastrDetalle}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_OPCIONES_REPORTE"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registros de Lista de Clases
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Clases_Ini() As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_CLASES_INI"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registros de Lista de Clases
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Clases() As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_CLASES"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registros de Lista de Series de una Clase
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Series_Ini(ByVal pastrClase As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CLASE", OracleDbType.Varchar2, 64, pastrClase}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_SERIES_INI"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registros de Lista de Series de una Clase
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Series(ByVal pastrClase As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CLASE", OracleDbType.Varchar2, 64, pastrClase}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_SERIES"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de Correlativo de Número de Producto
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Num_Producto(ByVal pastrClase As String, ByVal pastrSerie As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CLASE", OracleDbType.Varchar2, 64, pastrClase}, _
                                       {"VI_VA_SERIE", OracleDbType.Varchar2, 64, pastrSerie}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_NUM_PRODUCTO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registros de Lista de Departamentos
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Departamentos() As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_DEPARTAMENTOS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registros de Lista de Departamentos
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Provincias(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO", OracleDbType.Varchar2, 64, pastrCodigo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_PROVINCIAS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registros de Lista de Departamentos
    'Fecha: 27/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Distritos(ByVal pastrCodigo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO", OracleDbType.Varchar2, 64, pastrCodigo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_DISTRITOS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con la fecha y hora
    'Fecha: 15/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Time() As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_TIME"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con el tipo de cambio y la fecha del tipo de cambio
    'Fecha: 15/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Tipo_Cambio(ByVal pastrFecha As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_FECHA", OracleDbType.Varchar2, 10, pastrFecha}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_TIPO_CAMBIO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con el tipo de cambio y la fecha del tipo de cambio
    'Fecha: 15/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Tipo_Cambio_Ini(ByVal pastrFecha As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_FECHA", OracleDbType.Varchar2, 10, pastrFecha}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_TIPO_CAMBIO_INI"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con el IGV
    'Fecha: 15/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_IGV() As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_IGV"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con la serie y el número de documento
    'Fecha: 15/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Serie_Y_Nro_Doc(ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_SERIE_Y_NRO_DOC"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con la serie y el número de documento
    'Fecha: 15/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Nro_Recibo(ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_NRO_RECIBO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con la serie y el número de documento
    'Fecha: 15/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Serie_Y_Nro_Doc_Imp(ByVal pastrTipo As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_TIPO", OracleDbType.Varchar2, 2, pastrTipo}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_SERIE_Y_NRO_DOC_IMP"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con el número de pedido
    'Fecha: 22/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Nro_Pedido() As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_NRO_PEDIDO"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con el número de pedido
    'Fecha: 22/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Nro_Pedido_Imp() As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_NRO_PEDIDO_IMP"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con el tipo de cambio y la fecha del tipo de cambio
    'Fecha: 15/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Registro_Ventas(ByVal pastrMoneda As String, ByVal pastrFechaIni As String, _
                                           ByVal pastrFechaFin As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_MONEDA", OracleDbType.Varchar2, 3, pastrMoneda}, _
                                       {"VI_VA_FECHA_INI", OracleDbType.Varchar2, 10, pastrFechaIni}, _
                                       {"VI_VA_FECHA_FIN", OracleDbType.Varchar2, 10, pastrFechaFin}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_REGISTRO_VENTAS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con el tipo de cambio y la fecha del tipo de cambio
    'Fecha: 15/03/2006 
    '*************************************************************************************
    Public Function FS_Sel_Liquidacion(ByVal pastrMoneda As String, ByVal pastrFechaIni As String, _
                                       ByVal pastrFechaFin As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_MONEDA", OracleDbType.Varchar2, 3, pastrMoneda}, _
                                       {"VI_VA_FECHA_INI", OracleDbType.Varchar2, 10, pastrFechaIni}, _
                                       {"VI_VA_FECHA_FIN", OracleDbType.Varchar2, 10, pastrFechaFin}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_LIQUIDACION_DIARIA"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con el número del movimiento del almacén
    'Fecha: 24/04/2006 
    '*************************************************************************************
    Public Function FS_Sel_Nro_MovAlmacen(ByVal pastrCodigoAlmacen As String, ByVal pastrTipoMovimiento As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO_ALMACEN", OracleDbType.Varchar2, 2, pastrCodigoAlmacen}, _
                                       {"VI_VA_TIPO_MOVIMIENTO", OracleDbType.Varchar2, 10, pastrTipoMovimiento}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_NRO_MOVALMACEN"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor que indica la cantidad de movimientos con ese número
    'Fecha: 04/05/2006 
    '*************************************************************************************
    Public Function FS_Sel_Existe_Nro_Mov(ByVal pastrCodigoAlmacen As String, ByVal pastrTipoMovimiento As String, _
                                          ByVal paintNumero As Integer) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO_ALMACEN", OracleDbType.Varchar2, 2, pastrCodigoAlmacen}, _
                                       {"VI_VA_TIPO_MOVIMIENTO", OracleDbType.Varchar2, 10, pastrTipoMovimiento}, _
                                       {"VI_NU_NUMERO", OracleDbType.Int64, 6, paintNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_EXISTE_NRO_MOV"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con el número del movimiento del almacén
    'Fecha: 24/04/2006 
    '*************************************************************************************
    Public Function FS_Sel_MovAlmacenes(ByVal pastrCodigoAlmacen As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO_ALMACEN", OracleDbType.Varchar2, 2, pastrCodigoAlmacen}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_MOVALMACENES"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con el número del movimiento del almacén
    'Fecha: 24/04/2006 
    '*************************************************************************************
    Public Function FS_Sel_Det_MovAlmacen(ByVal pastrCodigoAlmacen As String, ByVal pastrTipoMovimiento As String, _
                                          ByVal paintNumero As Int64) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO_ALMACEN", OracleDbType.Varchar2, 2, pastrCodigoAlmacen}, _
                                       {"TIPO_MOVIMIENTO", OracleDbType.Varchar2, 10, pastrTipoMovimiento}, _
                                       {"NUMERO", OracleDbType.Int64, 6, paintNumero}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_DET_MOVALMACEN"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*****************************************************************************************************
    'Descripción: Verifica si ya se procedió al Cierre de Mes
    'Fecha: 26/04/2006 
    '****************************************************************************************************
    Public Function FT_Ver_Cierre_Mes(ByVal pastrCodigoAlmacen As String, ByVal pastrAnho As String, _
                                      ByVal pastrMes As String) As String
        Dim lstrprocedimiento As String
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim arrParametros(,) As Object = {{"VI_VA_CODIGO_ALMACEN", OracleDbType.Varchar2, 2, pastrCodigoAlmacen}, _
                                          {"VI_VA_ANHO", OracleDbType.Varchar2, 4, pastrAnho}, _
                                          {"VI_VA_MES", OracleDbType.Varchar2, 2, pastrMes}}

        lstrprocedimiento = gstrEsquema & ".PKG_UTILITARIO_FCT.P_VER_CIERRE_MES"

        Return objDatos.FPT_ExecSPReturnValueOUTPUT(lstrprocedimiento, arrParametros)
        objDatos = Nothing
    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con el kardex
    'Fecha: 02/05/2006 
    '*************************************************************************************
    Public Function FS_Sel_Kardex(ByVal pastrCodAlmacen As String, ByVal pastrClaseProducto As String, _
                                  ByVal pastrSerieProducto As String, ByVal pastrNumeroProducto As String, _
                                  ByVal paintAnoIni As Integer, ByVal paintAnoFin As Integer, _
                                  ByVal paintMesIni As Integer, ByVal paintMesFin As Integer, _
                                  ByVal pastrMoneda As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO_ALMACEN", OracleDbType.Varchar2, 3, pastrCodAlmacen}, _
                                       {"VI_VA_CLASE", OracleDbType.Varchar2, 2, pastrClaseProducto}, _
                                       {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerieProducto}, _
                                       {"VI_VA_NUMERO", OracleDbType.Varchar2, 5, pastrNumeroProducto}, _
                                       {"VI_VA_ANHO_INI", OracleDbType.Int32, 4, paintAnoIni}, _
                                       {"VI_VA_ANHO_FIN", OracleDbType.Int32, 4, paintAnoFin}, _
                                       {"VI_VA_MES_INI", OracleDbType.Int32, 2, paintMesIni}, _
                                       {"VI_VA_MES_FIN", OracleDbType.Int32, 2, paintMesFin}, _
                                       {"VI_VA_MONEDA", OracleDbType.Varchar2, 3, pastrMoneda}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_MOV_KARDEX"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con el saldo kardex
    'Fecha: 02/05/2006 
    '*************************************************************************************
    Public Function FS_Sel_Saldo_Kardex(ByVal pastrCodAlmacen As String, ByVal pastrClaseProducto As String, _
                                  ByVal pastrSerieProducto As String, ByVal pastrNumeroProducto As String, _
                                  ByVal paintAno As Integer, ByVal paintMes As Integer, _
                                  ByVal pastrMoneda As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO_ALMACEN", OracleDbType.Varchar2, 3, pastrCodAlmacen}, _
                                       {"VI_VA_CLASE", OracleDbType.Varchar2, 2, pastrClaseProducto}, _
                                       {"VI_VA_SERIE", OracleDbType.Varchar2, 3, pastrSerieProducto}, _
                                       {"VI_VA_NUMERO", OracleDbType.Varchar2, 5, pastrNumeroProducto}, _
                                       {"VI_VA_ANHO", OracleDbType.Int32, 4, paintAno}, _
                                       {"VI_VA_MES", OracleDbType.Int32, 2, paintMes}, _
                                       {"VI_VA_MONEDA", OracleDbType.Varchar2, 3, pastrMoneda}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_SALDO_KARDEX"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Devuelve un cursor con el Inventario de PT
    'Fecha: 02/05/2006 
    '*************************************************************************************
    Public Function FS_Sel_Inventario_PT(ByVal pastrCodAlmacen As String, ByVal paintAno As Integer, _
                                         ByVal paintMes As Integer, ByVal pastrMoneda As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO_ALMACEN", OracleDbType.Varchar2, 3, pastrCodAlmacen}, _
                                       {"VI_VA_ANHO", OracleDbType.Int32, 4, paintAno}, _
                                       {"VI_VA_MES", OracleDbType.Int32, 2, paintMes}, _
                                       {"VI_VA_MONEDA", OracleDbType.Varchar2, 3, pastrMoneda}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_INVENTARIO_PT"
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
    Public Function FS_Sel_Saldo_Inicial(ByVal pastrCodAlmacen As String, ByVal paintAno As Integer, _
                                         ByVal paintMes As Integer, ByVal pastrMoneda As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO_ALMACEN", OracleDbType.Varchar2, 3, pastrCodAlmacen}, _
                                       {"VI_VA_ANHO", OracleDbType.Int32, 4, paintAno}, _
                                       {"VI_VA_MES", OracleDbType.Int32, 2, paintMes}, _
                                       {"VI_VA_MONEDA", OracleDbType.Varchar2, 3, pastrMoneda}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_SALDO_INICIAL"
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
    Public Function FS_Sel_Control_ES(ByVal pastrCodAlmacen As String, ByVal paintAno As Integer, _
                                      ByVal paintMes As Integer, ByVal pastrMoneda As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_CODIGO_ALMACEN", OracleDbType.Varchar2, 3, pastrCodAlmacen}, _
                                       {"VI_VA_ANHO", OracleDbType.Int32, 4, paintAno}, _
                                       {"VI_VA_MES", OracleDbType.Int32, 2, paintMes}, _
                                       {"VI_VA_MONEDA", OracleDbType.Varchar2, 3, pastrMoneda}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_CONTROL_ES"
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
    Public Function FS_Sel_Internamientos(ByVal paintAno As Integer, ByVal paintMes As Integer) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_ANHO", OracleDbType.Int32, 4, paintAno}, _
                                       {"VI_VA_MES", OracleDbType.Int32, 2, paintMes}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_INTERNAMIENTOS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '**************************************************************************************************************
    '**************************************************************************************************************
    '*************************************************************************************
    'Descripción: Búsqueda de registros de opciones para armar el menu
    'Fecha: 25/11/2005 
    '*************************************************************************************
    Public Function FS_Sel_Menu_Opcion(ByVal paintAplicacion As Int64, ByVal pastrUsuario As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_NU_APLICACION", OracleDbType.Int64, 5, paintAplicacion}, _
                                       {"VI_VA_USUARIO", OracleDbType.Varchar2, 32, pastrUsuario}}

        Dim lstrProcedimiento As String = gstrEsquema & ".PKG_UTILITARIO_FCT.P_SEL_MENU_OPCION"
        Try
            Return objDatos.FPT_ExecSPReturnDataTable(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function
    '**************************************************************************************************************
    '**************************************************************************************************************

End Class