Public Class PORTAL_CONF_Seguridad

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

End Class