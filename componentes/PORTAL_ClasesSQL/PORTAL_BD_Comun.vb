Option Explicit On 
Imports System.Data.SqlClient

Public Class clsBD_Comun

    '*************************************************************************************
    'Descripción: Retorna usuarios segun las opciones para el reporte
    'Fecha: 07/03/2006
    '*************************************************************************************
    Public Function FS_Employees() As DataSet

        Dim objDatos As New PORTAL_ClasesSQL.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {}

        Dim lstrProcedimiento As String = gstrEsquemaSQL & ".P_SEL_EMPLOYEES"
        Try
            Return objDatos.FPT_ExecSPReturnDataSet(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    '*************************************************************************************
    'Descripción: Retorna usuarios segun las opciones
    'Fecha: 07/03/2006
    '*************************************************************************************
    Public Function FPS_Usuarios_Opciones(ByVal pstrEmpresa As String, _
                                          ByVal pstrAplicacion As String, _
                                          ByVal pstrUsuario As String, _
                                          ByVal pstrOpciones As String, _
                                          ByVal pstrNroOpciones As Integer) As SqlDataReader

        Dim objDatos As New PORTAL_ClasesSQL.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"@empresaid", SqlDbType.Char, 7, pstrEmpresa}, _
                                          {"@aplicacionid", SqlDbType.Char, 3, pstrAplicacion}, _
                                          {"@usuarioid", SqlDbType.VarChar, 128, pstrUsuario}, _
                                          {"@opciones", SqlDbType.VarChar, 1000, pstrOpciones}, _
                                          {"@nu_opciones", SqlDbType.Int, 1, pstrNroOpciones}}

        Dim lstrProcedimiento As String = gstrEsquemaSQL & ".SP_SNB_Usuarios_Opciones"
        Try
            Return objDatos.FPS_ExecSPReturnReader(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function

    '*************************************************************************************
    'Descripción: Retorna usuarios segun las opciones para el reporte
    'Fecha: 07/03/2006
    '*************************************************************************************
    Public Function FPS_Usuarios_Reporte(ByVal pstrEmpresa As String, _
                                          ByVal pstrAplicacion As String, _
                                          ByVal pstrUsuario As String, _
                                          ByVal pstrOpciones As String) As DataSet

        Dim objDatos As New PORTAL_ClasesSQL.clsBD_ManejoDatos
        Dim Parametros(,) As Object = {{"@empresaid", SqlDbType.Char, 7, pstrEmpresa}, _
                                          {"@aplicacionid", SqlDbType.Char, 3, pstrAplicacion}, _
                                          {"@usuarioid", SqlDbType.VarChar, 128, pstrUsuario}, _
                                          {"@opciones", SqlDbType.VarChar, 1000, pstrOpciones}}

        Dim lstrProcedimiento As String = "dbo.SP_SNB_Usuarios_Reporte"
        Try
            Return objDatos.FPT_ExecSPReturnDataSet(lstrProcedimiento, Parametros)
            objDatos = Nothing
        Catch
            Throw
        End Try
    End Function
End Class
