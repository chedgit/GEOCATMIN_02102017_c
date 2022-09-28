Module PORTAL_Modulo
    Private objConexion As New PORTAL_ClasesSQL.clsBD_Conexion
    Public strConexionSQL As String = objConexion.Conexion
    Public gstrEsquemaSQL As String = objConexion.EsquemaSeguridad
End Module
