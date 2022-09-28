Imports System
Imports System.IO
Imports System.Text
Imports System.Reflection

Public Class ModuleConfig
    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    ' Métodos
    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    ' Descripción: Función que se encarga de Obtener los parámetros del archivo 
    ' de configuración Web.config
    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    Public Shared Function GetSettings() As ModuleSettings
        Dim ldata As New ModuleSettings
        'Carga el archivo SISFACT.ini
        Dim objReader As New StreamReader(Path.GetDirectoryName([Assembly].GetExecutingAssembly().Location) & "\SISFACT.ini")
        'Dim objReader As New StreamReader("../SISFACT.ini")
        Dim cls_PORTAL_CONF_Seguridad As New PORTAL_CONF_Seguridad
        Dim sLine As String = ""
        Dim arrText As New ArrayList
        Do
            sLine = objReader.ReadLine()
            If Not sLine Is Nothing Then
                arrText.Add(sLine)
            End If
        Loop Until sLine Is Nothing
        objReader.Close()

        For Each sLine In arrText
            Dim lostrposicion As Integer = sLine.IndexOf("=")
            Dim lostrparte1 As String = Trim(sLine.Substring(0, lostrposicion))
            Dim lostrparte2 As String = cls_PORTAL_CONF_Seguridad.FT_Desencriptar(Trim(sLine.Substring(lostrposicion + 1)))
            Select Case lostrparte1
                Case "CadenaConexion"
                    ldata.CadenaConexion = lostrparte2
                Case "CadenaConexionSeguridad"
                    ldata.CadenaConexionSeguridad = lostrparte2
                Case "CadenaConexionGIS"
                    ldata.CadenaConexionGIS = lostrparte2
                Case "Esquema"
                    ldata.Esquema = lostrparte2
                Case "EsquemaSeguridad"
                    ldata.EsquemaSeguridad = lostrparte2
                Case "EsquemaGIS"
                    ldata.EsquemaGIS = lostrparte2
                Case "Servidor"
                    ldata.Servidor = lostrparte2
                Case "Unidad"
                    ldata.Unidad = lostrparte2

            End Select
        Next
        Return ldata
    End Function
End Class
