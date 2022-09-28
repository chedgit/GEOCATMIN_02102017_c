Option Explicit On 
Imports System.Data.SqlClient
Imports PORTAL_Configuracion

Public Class clsBD_Conexion
    Private objConfiguracion As Configuracion
    Private _strCadenaConexion As String = ""
    Private _strCadenaConexionSeguridad As String = ""
    Private _strServidor As String = ""
    Private _strEsquema As String = ""
    Private _strEsquemaSeguridad As String = ""

    Public ReadOnly Property Conexion()
        Get
            Try
                'PRODUCCION: WBBARI01:SA:ESTRELLA
                'DESARROLLO: APQDCA01,SA,REDES50
                'Dim sCadena As New System.Text.StringBuilder( _
                '    "data source=WBBARI01;" & _
                '    "initial catalog=BackusAD_commerce;" & _
                '    "persist security info=False;" & _
                '    "user id=sa; " & _
                '    "password=estrella;")
                Dim objConfiguracion As New Configuracion
                'Dim sCadena As New System.Text.StringBuilder(objConfiguracion.CadenaConexionSeguridad)
                Dim sCadena As New System.Text.StringBuilder(_strCadenaConexionSeguridad)
                Return sCadena.ToString
            Catch
                Throw New System.Exception("No se puede establecer la cadena de conexion.")
            End Try
        End Get
    End Property

    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
    ' TODO: Constructores / Destructores
    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
    Sub New()
        InicializarClass()
    End Sub
    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
    ' Descripción: 
    ' Método privado para la inicialización de Variables y Objetos de la Clase
    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
    Private Sub InicializarClass()
        objConfiguracion = New Configuracion
        _strCadenaConexion = objConfiguracion.CadenaConexion
        _strCadenaConexionSeguridad = objConfiguracion.CadenaConexionSeguridad
        _strServidor = objConfiguracion.Servidor
        _strEsquema = objConfiguracion.Esquema
        _strEsquemaSeguridad = objConfiguracion.EsquemaSeguridad
    End Sub

    Public Property CadenaConexion() As String
        Get
            Return _strCadenaConexion
        End Get
        Set(ByVal Value As String)
            _strCadenaConexion = Value
        End Set
    End Property

    Public Property CadenaConexionSeguridad() As String
        Get
            Return _strCadenaConexionSeguridad
        End Get
        Set(ByVal Value As String)
            _strCadenaConexionSeguridad = Value
        End Set
    End Property

    Public Property Servidor() As String
        Get
            Return _strServidor
        End Get
        Set(ByVal Value As String)
            _strServidor = Value
        End Set
    End Property

    Public Property Esquema() As String
        Get
            Return _strEsquema
        End Get
        Set(ByVal Value As String)
            _strEsquema = Value
        End Set
    End Property

    Public Property EsquemaSeguridad() As String
        Get
            Return _strEsquemaSeguridad
        End Get
        Set(ByVal Value As String)
            _strEsquemaSeguridad = Value
        End Set
    End Property


End Class
Public Class clsBD_ManejoDatos

#Region "CallStoreProcedures"

    '*************************************************************************************
    'Nombre:ExecSPReturnDataSet
    'Descripción: Ejecuta un Procedimiento Almacenado sin parámetros, retornando un DataSet
    'Retorna: Object
    '*************************************************************************************
    Public Overloads Function FPT_ExecSPReturnDataSet(ByVal ProcedimientoAlmacenado As String) As System.Data.DataSet
        Dim dap As New SqlDataAdapter(ProcedimientoAlmacenado, strConexionSQL)
        Dim dst As New DataSet
        Dim par As SqlParameter
        Try
            With dap.SelectCommand
                .CommandType = CommandType.StoredProcedure
            End With
            dap.Fill(dst)
            Return (dst)
            dap.Dispose()
        Catch
            Throw
        End Try
    End Function

    '*************************************************************************************
    'Nombre:ExecSPReturnDataSet
    'Descripción: Ejecuta un Procedimiento Almacenado con parámetros, retornando un DataSet
    'Retorna: Object
    '*************************************************************************************
    Public Overloads Function FPT_ExecSPReturnDataSet(ByVal ProcedimientoAlmacenado As String, ByVal Params(,) As System.Object) As System.Data.DataSet
        Dim dap As New SqlDataAdapter(ProcedimientoAlmacenado, strConexionSQL)
        Dim dst As New DataSet
        Dim i As Integer
        Try
            With dap.SelectCommand
                .CommandType = CommandType.StoredProcedure
                FPT_LoadParameters(dap.SelectCommand, Params)
            End With
            dap.Fill(dst)
            Return (dst)
        Catch
            Throw
        End Try
    End Function

    '*************************************************************************************
    'Nombre:ExecSPReturnDataTable
    'Descripción: Ejecuta un Procedimiento Almacenado sin parámetros, retornando un DataTable
    'Retorna: Object
    '*************************************************************************************
    Public Overloads Function FPT_ExecSPReturnDataTable(ByVal ProcedimientoAlmacenado As String) As System.Data.DataTable
        Return FPT_ExecSPReturnDataSet(ProcedimientoAlmacenado).Tables(0).Copy
    End Function

    '*************************************************************************************
    'Nombre:ExecSPReturnDataTable
    'Descripción: Ejecuta un Procedimiento Almacenado con parámetros, retornando un DataTable
    'Retorna: Object
    '*************************************************************************************
    Public Overloads Function FPT_ExecSPReturnDataTable(ByVal ProcedimientoAlmacenado As String, ByVal Params(,) As System.Object) As System.Data.DataTable
        Return FPT_ExecSPReturnDataSet(ProcedimientoAlmacenado, Params).Tables(0).Copy
    End Function

    Public Overloads Function FPT_FPS_ExecSPReturnEscalar(ByVal ProcedimientoAlmacenado As String) As System.Object
        Dim cn As New SqlConnection(strConexionSQL)
        Dim cmd As New SqlCommand(ProcedimientoAlmacenado, cn)
        Dim Trx As SqlTransaction
        Dim N As Object
        Try
            cn.Open()
            With cmd
                .CommandType = CommandType.StoredProcedure
                Trx = cn.BeginTransaction
                .Transaction = Trx
                N = .ExecuteScalar
            End With
            Trx.Commit()
            Return (N)
        Catch
            Trx.Rollback()
            Throw
        Finally
            With cmd.Connection
                If .State <> ConnectionState.Closed Then
                    .Close()
                End If
            End With
        End Try
    End Function

    Public Overloads Function FPS_ExecSPReturnEscalar(ByVal ProcedimientoAlmacenado As String, ByVal Params(,) As System.Object) As System.Object
        Dim cn As New SqlConnection(strConexionSQL)
        Dim Cmd As New SqlCommand(ProcedimientoAlmacenado, cn)
        Dim Trx As SqlTransaction
        Dim N As Object
        Try
            cn.Open()
            With Cmd
                .CommandType = CommandType.StoredProcedure
                FPT_LoadParameters(Cmd, Params)
                Trx = cn.BeginTransaction
                .Transaction = Trx
                N = .ExecuteScalar
            End With
            Trx.Commit()
            Return (N)
        Catch
            Trx.Rollback()
            Throw
        Finally
            With Cmd.Connection
                If .State <> ConnectionState.Closed Then
                    .Close()
                End If
            End With
        End Try
    End Function

    Public Overloads Function FPS_ExecSPReturnReader(ByVal ProcedimientoAlmacenado As String) As SqlDataReader
        Dim Cmd As SqlCommand
        Dim Dr As SqlDataReader
        Try
            Cmd = New SqlCommand(ProcedimientoAlmacenado)
            With Cmd
                .Connection = New SqlConnection(strConexionSQL)
                .CommandType = CommandType.StoredProcedure
                .Connection.Open()
                Dr = .ExecuteReader()
                Return Dr
            End With
        Catch
            Throw
        End Try
    End Function

    Public Overloads Function FPS_ExecSPReturnReader(ByVal ProcedimientoAlmacenado As String, ByVal Params(,) As System.Object) As SqlDataReader
        Dim Cmd As SqlCommand
        Dim Dr As SqlDataReader
        Try
            Cmd = New SqlCommand(ProcedimientoAlmacenado)
            With Cmd
                .Connection = New SqlConnection(strConexionSQL)
                .CommandType = CommandType.StoredProcedure
                FPT_LoadParameters(Cmd, Params)
                .Connection.Open()
                Dr = .ExecuteReader()
                Return Dr
            End With
        Catch
            Throw
        End Try
    End Function

    '*************************************************************************************
    'Nombre:ExecSPReturnValueOUTPUT
    'Descripción: Ejecuta un Procedimiento Almacenado retornando un valor de cualquier tipo
    'Retorna: Object
    '*************************************************************************************
    Public Overloads Function FPT_ExecSPReturnValueOUTPUT(ByVal ProcedimientoAlmacenado As String, ByVal Params(,) As System.Object) As System.Object
        Dim cn As New SqlConnection(strConexionSQL)
        Dim cmd As New SqlCommand(ProcedimientoAlmacenado, cn)
        Dim Trx As SqlTransaction
        Try
            cn.Open()
            With cmd
                .CommandType = CommandType.StoredProcedure
                FPT_LoadParameters(cmd, Params)
                Trx = cn.BeginTransaction
                .Transaction = Trx
                Dim Par As SqlParameter
                Par = .Parameters.Add("@VO_VA_RETORNO", SqlDbType.VarChar, 500)
                Par.Direction = ParameterDirection.Output
                .ExecuteNonQuery()
                Trx.Commit()
                Return .Parameters("@VO_VA_RETORNO").Value
            End With
        Catch
            Trx.Rollback()
            Throw
        Finally
            With cmd.Connection
                If .State <> ConnectionState.Closed Then
                    .Close()
                End If
            End With
        End Try
    End Function

    '*************************************************************************************
    'Nombre:ExecSPReturnValueOUTPUT
    'Descripción: Ejecuta un Procedimiento Almacenado retornando un valor de tipo string
    'Retorna: Object
    '*************************************************************************************
    Public Overloads Function FPT_ExecSPReturnValueOUTPUT(ByVal ProcedimientoAlmacenado As String) As System.Object
        Dim cn As New SqlConnection(strConexionSQL)
        Dim cmd As New SqlCommand(ProcedimientoAlmacenado, cn)
        Dim Trx As SqlTransaction
        Try
            cn.Open()
            With cmd
                .CommandType = CommandType.StoredProcedure
                Trx = cn.BeginTransaction
                .Transaction = Trx
                Dim Par As SqlParameter
                Par = .Parameters.Add("@VO_VA_RETORNO", SqlDbType.VarChar, 500)
                Par.Direction = ParameterDirection.Output
                .ExecuteNonQuery()
                Trx.Commit()
                Return .Parameters("@VO_VA_RETORNO").Value
            End With
        Catch
            Dim Ex As SqlError
            Trx.Rollback()
            Throw New Exception("Error")
        Finally
            With cmd.Connection
                If .State <> ConnectionState.Closed Then
                    .Close()
                End If
            End With
        End Try
    End Function

#End Region

#Region "Management"

    '*************************************************************************************
    'Nombre:LoadParameters
    'Descripción: Carga los parámetros
    'Retorna: Object
    '*************************************************************************************
    Protected Sub FPT_LoadParameters(ByVal Comando As SqlCommand, ByVal Parms(,) As Object)
        Dim i As Integer
        With Comando
            For i = Parms.GetLowerBound(0) To Parms.GetUpperBound(0)
                Dim Par As New SqlParameter
                Par.ParameterName = Parms(i, 0)
                Par.SqlDbType = Parms(i, 1)
                Par.Size = Parms(i, 2)
                Par.Value = Parms(i, 3)
                .Parameters.Add(Par)
            Next
        End With
    End Sub

#End Region

End Class