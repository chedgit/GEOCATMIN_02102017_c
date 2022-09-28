Module Portal_Modulo
    Private objConexion As New PORTAL_Clases.clsBD_Conexion
    Public strConexion As String = objConexion.Conexion
    Public strConexionSeguridad As String = objConexion.ConexionSeguridad
    Public strConexionGIS As String = objConexion.ConexionGIS
    Public gstrEsquema As String = objConexion.Esquema
    Public gstrEsquemaSeguridad As String = objConexion.EsquemaSeguridad
    Public gstrEsquemaGIS As String = objConexion.EsquemaGIS
    Public gstrUnidad As String = objConexion.Unidad
End Module
