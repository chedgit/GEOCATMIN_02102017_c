Option Explicit On 
Imports Oracle.DataAccess.Client
Imports PORTAL_Configuracion

Public Class cls_BD_Seguridad

    ' define the local key and vector byte arrays
    Private ReadOnly key() As Byte = _
      {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, _
      15, 16, 17, 18, 19, 20, 21, 22, 23, 24}
    Private ReadOnly iv() As Byte = {8, 7, 6, 5, 4, 3, 2, 1}

    Public Function FT_Encriptar(ByRef pastrCadena As String) As String
        Dim des As New PORTAL_CONF_Crypto(key, iv)
        Return des.Encrypt(pastrCadena)
    End Function

    Public Function FT_Desencriptar(ByRef pastrCadena As String) As String
        Dim des As New PORTAL_CONF_Crypto(key, iv)
        Return des.Decrypt(pastrCadena)
    End Function

    Public Function FS_Existe_Usuario_tmp(ByVal pastrUsuario As String, ByVal pastrClave As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_USUARIO", OracleDbType.Varchar2, 32, pastrUsuario}, _
                                       {"VI_VA_CLAVE", OracleDbType.Varchar2, 32, pastrClave}}

        Dim lstrProcedimiento As String = "VENTAS.PKG_MUESTRAS_LABORATORIO.P_USUARIO_VAL"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableSeg(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registro de Usuario
    'Fecha: 21/03/2006 
    '*************************************************************************************

    Public Function FS_Existe_Usuario(ByVal pastrUsuario As String, ByVal pastrClave As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_USUARIO", OracleDbType.Varchar2, 32, pastrUsuario}, _
                                       {"VI_VA_CLAVE", OracleDbType.Varchar2, 32, pastrClave}}

        'Dim lstrProcedimiento As String = gstrEsquemaSeguridad & ".PKG_INTRANET.P_USUARIO_VAL"
        Dim lstrProcedimiento As String = "APPS" & ".PKG_INTRANET.P_USUARIO_VAL"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableSeg(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

    '*************************************************************************************
    'Descripción: Búsqueda de registro de Acceso del Usuario al Sistema
    'Fecha: 22/03/2006 
    '*************************************************************************************
    Public Function FS_Acceso_Sistema(ByVal pastrUsuario As String, ByVal pastrUrl As String) As DataTable
        Dim objDatos As New PORTAL_Clases.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"VI_VA_USUARIO", OracleDbType.Varchar2, 32, pastrUsuario}, _
                                       {"VI_VA_URL", OracleDbType.Varchar2, 256, pastrUrl}}

        Dim lstrProcedimiento As String = "APPS.PKG_INTRANET.SP_APLICACION_CS"
        Try
            Return objDatos.FPT_ExecSPReturnDataTableSeg(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try

    End Function

End Class
