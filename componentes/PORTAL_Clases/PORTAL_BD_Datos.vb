Option Explicit On
Imports Oracle.DataAccess.Client
Imports PORTAL_Configuracion


Public Class clsBD_Conexion
    Private objConfiguracion As Configuracion
    Private _strCadenaConexion As String = ""
    Private _strCadenaConexionSeguridad As String = ""
    Private _strCadenaConexionGIS As String = ""
    Private _strServidor As String = ""
    Private _strEsquema As String = ""
    Private _strEsquemaSeguridad As String = ""
    Private _strEsquemaGIS As String = ""
    Private _strUnidad As String = ""

    Public ReadOnly Property Conexion() As String
        Get
            Try
                Dim sCadena As New System.Text.StringBuilder(_strCadenaConexion)
                Return sCadena.ToString
            Catch
                Throw New System.Exception("No se puede establecer la cadena de conexion.")
            End Try
        End Get
    End Property

    Public ReadOnly Property ConexionSeguridad() As String
        Get
            Try
                Dim sCadena As New System.Text.StringBuilder(_strCadenaConexionSeguridad)
                Return sCadena.ToString
            Catch
                Throw New System.Exception("No se puede establecer la cadena de conexion.")
            End Try
        End Get
    End Property
    Public ReadOnly Property ConexionGIS() As String
        Get
            Try
                Dim sCadena As New System.Text.StringBuilder(_strCadenaConexionGIS)
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
        Try
            objConfiguracion = New Configuracion()
            _strCadenaConexion = objConfiguracion.CadenaConexion
            _strCadenaConexionSeguridad = objConfiguracion.CadenaConexionSeguridad
            _strCadenaConexionGIS = objConfiguracion.CadenaConexionGIS
            _strServidor = objConfiguracion.Servidor
            _strEsquema = objConfiguracion.Esquema
            _strEsquemaSeguridad = objConfiguracion.EsquemaSeguridad
            _strEsquemaGIS = objConfiguracion.EsquemaGIS
            _strUnidad = objConfiguracion.unidad
            ' InicializarClass()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
    ' Descripción: 
    ' Método privado para la inicialización de Variables y Objetos de la Clase
    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
    Private Sub InicializarClass()
        objConfiguracion = New Configuracion()
        _strCadenaConexion = objConfiguracion.CadenaConexion
        _strCadenaConexionSeguridad = objConfiguracion.CadenaConexionSeguridad
        _strCadenaConexionGIS = objConfiguracion.CadenaConexionGIS
        _strServidor = objConfiguracion.Servidor
        _strEsquema = objConfiguracion.Esquema
        _strEsquemaSeguridad = objConfiguracion.EsquemaSeguridad
        _strEsquemaGIS = objConfiguracion.EsquemaGIS
        _strUnidad = objConfiguracion.unidad
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
    Public Property CadenaConexionGIS() As String
        Get
            Return _strCadenaConexionGIS
        End Get
        Set(ByVal Value As String)
            _strCadenaConexionGIS = Value
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

    Public Property EsquemaGIS() As String
        Get
            Return _strEsquemaGIS
        End Get
        Set(ByVal Value As String)
            _strEsquemaGIS = Value
        End Set
    End Property

    Public Property Unidad() As String
        Get
            Return _strUnidad
        End Get
        Set(ByVal Value As String)
            _strUnidad = Value
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
        Dim dap As New OracleDataAdapter(ProcedimientoAlmacenado, strConexion)
        Dim dst As New DataSet
        Try
            With dap.SelectCommand
                .CommandType = CommandType.StoredProcedure
                Dim Par As OracleParameter
                Par = .Parameters.Add("VO_NORMAS", OracleDbType.RefCursor)
                Par.Direction = ParameterDirection.Output
            End With
            dap.Fill(dst)
            Return (dst)
        Catch
            Throw
        Finally
            dst.Dispose()
            dap.SelectCommand.Connection.Close()
            dap.SelectCommand.Connection.Dispose()
            dap.Dispose()
        End Try

    End Function

    '**********************************************************************************
    'Nombre:ExecSPReturnDataSet
    'Descripción: Ejecuta un Procedimiento Almacenado con parámetros, retornando un DataSet
    'Retorna: Object
    '*************************************************************************************
    Public Overloads Function FPT_ExecSPReturnDataSet(ByVal ProcedimientoAlmacenado As String, ByVal Params(,) As System.Object) As System.Data.DataSet
        Dim dap As New OracleDataAdapter(ProcedimientoAlmacenado, strConexion)
        Dim dst As New DataSet
        Try
            With dap.SelectCommand
                .CommandType = CommandType.StoredProcedure
                FPT_LoadParameters(dap.SelectCommand, Params)
                Dim Par As OracleParameter
                Par = .Parameters.Add("VO_NORMAS", OracleDbType.RefCursor)
                Par.Direction = ParameterDirection.Output
            End With
            dap.Fill(dst)
            Return (dst)
        Catch
            Throw
        Finally
            dst.Dispose()
            dap.SelectCommand.Connection.Close()
            dap.SelectCommand.Connection.Dispose()
            dap.Dispose()
        End Try
    End Function
    Public Overloads Function FPT_ExecSPReturnDataGIS(ByVal ProcedimientoAlmacenado As String, ByVal Params(,) As System.Object) As System.Data.DataSet
        Dim dap As New OracleDataAdapter(ProcedimientoAlmacenado, strConexionGIS)
        Dim dst As New DataSet
        Try
            With dap.SelectCommand
                .CommandType = CommandType.StoredProcedure
                FPT_LoadParameters(dap.SelectCommand, Params)
                Dim Par As OracleParameter
                Par = .Parameters.Add("VO_NORMAS", OracleDbType.RefCursor)
                Par.Direction = ParameterDirection.Output
            End With
            dap.Fill(dst)
            Return (dst)
        Catch
            Throw
        Finally
            dst.Dispose()
            dap.SelectCommand.Connection.Close()
            dap.SelectCommand.Connection.Dispose()
            dap.Dispose()
        End Try
    End Function

    '**********************************************************************************
    'Nombre:ExecSPReturnDataSetSeg
    'Descripción: Ejecuta un Procedimiento Almacenado con parámetros, retornando un DataSet
    'Retorna: Object
    '*************************************************************************************
    Public Overloads Function FPT_ExecSPReturnDataSetSeg(ByVal ProcedimientoAlmacenado As String, ByVal Params(,) As System.Object) As System.Data.DataSet
        'strConexionSeguridad = "User Id=WURB0904;Password=DIEGO;Data Source=ORACLE;Connection Timeout=60;"
        Dim dap As New OracleDataAdapter(ProcedimientoAlmacenado, strConexionSeguridad)
        Dim dst As New DataSet
        Try
            With dap.SelectCommand
                .CommandType = CommandType.StoredProcedure
                FPT_LoadParameters(dap.SelectCommand, Params)
                Dim Par As OracleParameter
                Par = .Parameters.Add("VO_NORMAS", OracleDbType.RefCursor)
                Par.Direction = ParameterDirection.Output
            End With
            dap.Fill(dst)
            Return (dst)
        Catch
            Throw
        Finally
            dst.Dispose()
            dap.SelectCommand.Connection.Close()
            dap.SelectCommand.Connection.Dispose()
            dap.Dispose()
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
    Public Overloads Function FPT_ExecSPReturnDataTableGIS(ByVal ProcedimientoAlmacenado As String, ByVal Params(,) As System.Object) As System.Data.DataTable
        Return FPT_ExecSPReturnDataGIS(ProcedimientoAlmacenado, Params).Tables(0).Copy
    End Function
    '*************************************************************************************
    'Nombre:ExecSPReturnDataTableSeg
    'Descripción: Ejecuta un Procedimiento Almacenado con parámetros, retornando un DataTable
    'Retorna: Object
    '*************************************************************************************
    Public Overloads Function FPT_ExecSPReturnDataTableSeg(ByVal ProcedimientoAlmacenado As String, ByVal Params(,) As System.Object) As System.Data.DataTable
        Return FPT_ExecSPReturnDataSetSeg(ProcedimientoAlmacenado, Params).Tables(0).Copy

    End Function

    Public Overloads Function FPS_ExecSPReturnReader(ByVal ProcedimientoAlmacenado As String) As OracleDataReader
        Dim Cmd As New OracleCommand
        ' strConexion = "User Id=SISGEM;Password=SISGEM;Data Source=DESA_DM;Connection Timeout=60;"
        Try
            Cmd = New OracleCommand(ProcedimientoAlmacenado)
            With Cmd
                .Connection = New OracleConnection(strConexion)
                .CommandType = CommandType.StoredProcedure
                .Connection.Open()
                Dim Par As OracleParameter
                Par = .Parameters.Add("VO_CURSOR", OracleDbType.RefCursor)
                Par.Direction = ParameterDirection.Output
                Return .ExecuteReader()
            End With
        Catch
            Throw
        Finally
            Cmd.Dispose()
        End Try

    End Function

    Public Overloads Function FPS_ExecSPReturnReader(ByVal ProcedimientoAlmacenado As String, ByVal Params(,) As System.Object) As OracleDataReader
        Dim Cmd As New OracleCommand
        Try
            Cmd = New OracleCommand(ProcedimientoAlmacenado)
            With Cmd
                .Connection = New OracleConnection(strConexion)
                .CommandType = CommandType.StoredProcedure
                .Connection.Open()
                FPT_LoadParameters(Cmd, Params)
                Dim Par As OracleParameter
                Par = .Parameters.Add("VO_CURSOR", OracleDbType.RefCursor)
                Par.Direction = ParameterDirection.Output
                Return .ExecuteReader
            End With
        Catch
            Throw
        Finally
            Cmd.Dispose()
        End Try

    End Function

    '*************************************************************************************
    'Nombre:ExecSPReturnValueOUTPUT
    'Descripción: Ejecuta un Procedimiento Almacenado retornando un valor de cualquier tipo
    'Retorna: Object
    '*************************************************************************************
Public Overloads Function FPT_ExecSPReturnValueOUTPUT(ByVal ProcedimientoAlmacenado As String, ByVal Params(,) As System.Object) As System.Object
        Dim cn As New OracleConnection(strConexion)
        Dim cmd As New OracleCommand(ProcedimientoAlmacenado, cn)
        Dim Trx As OracleTransaction
        Trx = Nothing
        Try
            cn.Open()
            With cmd
                .CommandType = CommandType.StoredProcedure
                FPT_LoadParameters(cmd, Params)
                Trx = cn.BeginTransaction
                Dim Par As OracleParameter
                Par = .Parameters.Add("VO_VA_RETORNO", OracleDbType.Varchar2, 500)
                Par.Direction = ParameterDirection.Output
                .ExecuteNonQuery()
                Trx.Commit()
                Return .Parameters("VO_VA_RETORNO").Value.Value.ToString()
            End With
        Catch
            Trx.Rollback()
            Throw
        Finally
            cmd.Connection.Close()
            cmd.Connection.Dispose()
            cn.Close()
            cn.Dispose()
            With cmd.Connection
                If .State <> ConnectionState.Closed Then
                    .Close()
                End If
            End With
            cmd.Dispose()
        End Try
    End Function
    Public Overloads Function FPT_ExecSPReturnValueOUTPUTGIS(ByVal ProcedimientoAlmacenado As String, ByVal Params(,) As System.Object) As System.Object
        Dim cn As New OracleConnection(strConexionGIS)
        Dim cmd As New OracleCommand(ProcedimientoAlmacenado, cn)
        Dim Trx As OracleTransaction
        Trx = Nothing
        Try
            cn.Open()
            With cmd
                .CommandType = CommandType.StoredProcedure
                FPT_LoadParameters(cmd, Params)
                Trx = cn.BeginTransaction
                Dim Par As OracleParameter
                Par = .Parameters.Add("VO_VA_RETORNO", OracleDbType.Varchar2, 500)
                Par.Direction = ParameterDirection.Output
                .ExecuteNonQuery()
                Trx.Commit()
                Return .Parameters("VO_VA_RETORNO").Value.Value.ToString()
            End With
        Catch
            Trx.Rollback()
            Throw
        Finally
            cmd.Connection.Close()
            cmd.Connection.Dispose()
            cn.Close()
            cn.Dispose()
            With cmd.Connection
                If .State <> ConnectionState.Closed Then
                    .Close()
                End If
            End With
            cmd.Dispose()
        End Try
    End Function

    '*************************************************************************************
    'Nombre:ExecSPReturnValueOUTPUT
    'Descripción: Ejecuta un Procedimiento Almacenado retornando un valor de tipo string
    'Retorna: Object
    '*************************************************************************************

    Public Overloads Function FPT_ExecSPReturnValueOUTPUT(ByVal ProcedimientoAlmacenado As String) As System.Object
        Dim cn As New OracleConnection(strConexion)
        Dim cmd As New OracleCommand(ProcedimientoAlmacenado, cn)
        Dim Trx As OracleTransaction
        Trx = Nothing
        Try
            cn.Open()
            With cmd
                .CommandType = CommandType.StoredProcedure
                Trx = cn.BeginTransaction
                Dim Par As OracleParameter
                Par = .Parameters.Add("VO_VA_RETORNO", OracleDbType.Varchar2, 500)
                Par.Direction = ParameterDirection.Output
                .ExecuteNonQuery()
                Trx.Commit()
                Return .Parameters("VO_VA_RETORNO").Value.Value
            End With
        Catch
            Trx.Rollback()
            Throw
        Finally
            cmd.Connection.Close()
            cmd.Connection.Dispose()
            cn.Close()
            cn.Dispose()
            With cmd.Connection
                If .State <> ConnectionState.Closed Then
                    .Close()
                End If
            End With
            cmd.Dispose()
        End Try
    End Function

#End Region

#Region "PadreHIjo"
    '*************************************************************************************
    'Nombre:ExecSPReturnDataSet
    'Descripción: Ejecuta un Procedimiento Almacenado con parámetros, retornando un DataSet
    'Retorna: Object
    '*************************************************************************************
    Public Overloads Function FPT_ExecSP_PHRetDataSet(ByVal ProcedimientoAlmacenado1 As String, ByVal Params1(,) As System.Object, ByVal ProcedimientoAlmacenado2 As String, ByVal Params2(,) As System.Object) As System.Data.DataSet
        Dim cn As New OracleConnection(strConexion)
        Dim dap As New OracleDataAdapter
        Dim dst As New DataSet
        Try
            dap.SelectCommand = New OracleCommand
            With dap.SelectCommand
                .Connection = cn
                .CommandText = ProcedimientoAlmacenado1
                .CommandType = CommandType.StoredProcedure
                FPT_LoadParameters(dap.SelectCommand, Params1)
                dap.Fill(dst, "Padre")
            End With
        Catch
            Throw
        Finally
            dap.SelectCommand.Connection.Close()
            dap.SelectCommand.Connection.Dispose()
            dap.SelectCommand.Dispose()
            cn.Close()
            cn.Dispose()
            dap.Dispose()
            dst.Dispose()
        End Try
        Try
            dap.SelectCommand = New OracleCommand
            With dap.SelectCommand
                .Connection = cn
                .CommandText = ProcedimientoAlmacenado2
                .CommandType = CommandType.StoredProcedure
                FPT_LoadParameters(dap.SelectCommand, Params2)
                dap.Fill(dst, "Hijo")
            End With
        Catch
            Throw
        Finally
            dap.SelectCommand.Connection.Close()
            dap.SelectCommand.Connection.Dispose()
            dap.SelectCommand.Dispose()
            cn.Close()
            cn.Dispose()
            dap.Dispose()
            dst.Dispose()
        End Try
        Return (dst)
    End Function

#End Region

#Region "Management"
    '*************************************************************************************
    'Nombre:LoadParameters
    'Descripción: Carga los parámetros
    'Retorna: Object
    '*************************************************************************************
    Protected Sub FPT_LoadParameters(ByVal Comando As OracleCommand, ByVal Parms(,) As Object)
        Dim i As Integer
        With Comando
            For i = Parms.GetLowerBound(0) To Parms.GetUpperBound(0)
                Dim Par As New OracleParameter
                Par.ParameterName = Parms(i, 0)
                Par.OracleDbType = Parms(i, 1)
                Par.Size = Parms(i, 2)
                Par.Value = Parms(i, 3)
                .Parameters.Add(Par)
            Next
        End With
    End Sub

#End Region

End Class
