Imports ESRI.ArcGIS.DataSourcesOleDB
Imports ESRI.ArcGIS.GISClient
Imports ESRI.ArcGIS.Display
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Output
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.CartoUI
Imports ESRI.ArcGIS.GeoDatabaseUI
Imports PORTAL_Clases
Imports stdole

Public Class GEOCATMIN_IniLogin_1
    Public pPropset As IPropertySet
    Public pWorkspace As IWorkspace
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        'Dim cls_Conextion As New cls_Conexion_Oracle
        'cls_Conextion.Conexion_Oracle_SDE("", Me.txtUsuario.Text, Me.txtContrasena.Text, "", "")
        permisobd()
    End Sub
    Public Sub permisobd()
        'PROGRAMA PARA CONECTARSE A LA BASE DE DATOS A NIVEL DE USUARIO
        '*****************************************************************
        Dim cad_usuario, clave_usuario As String
        Dim m_Codigo_NumReg As Integer
        Dim conta_p As Integer
        Dim clave_valida As Boolean
        Try
            pPropset = New PropertySet
            Dim c_usuario As String
            'Dim aplicacion As IApplication
            'aplicacion = Application
            cad_usuario = Me.txtUsuario.Text
            clave_usuario = Me.txtContrasena.Text
            pPropset.SetProperty("CONNECTSTRING", "Provider=MSDAORA.1;Data source=ORACLE;User ID=" & cad_usuario & ";Password=" & clave_usuario)
            'Creando sentencias del OLEDB
            Dim pWorkspaceFact As IWorkspaceFactory
            pWorkspaceFact = New OLEDBWorkspaceFactory
            pWorkspace = pWorkspaceFact.Open(pPropset, 0)
            Dim pFeatureWorkspace As IFeatureWorkspace
            pFeatureWorkspace = pWorkspace
            Dim pQueryDef As IQueryDef
            pQueryDef = pFeatureWorkspace.CreateQueryDef

            'Construyendo la consulta
            pQueryDef.Tables = "SG_M_USUARIOS "
            pQueryDef.SubFields = "US_LOGUSE, US_NOMUSE "
            pQueryDef.WhereClause = "US_LOGUSE = '" & cad_usuario & "'"

            Dim cursor_filas As ICursor
            Dim filas_dm As IRow
            cursor_filas = pQueryDef.Evaluate
            filas_dm = cursor_filas.NextRow
            Dim i As Long
            i = 0
            m_Codigo_NumReg = i
            Do Until filas_dm Is Nothing
                c_usuario = filas_dm.Value(0) & ""
                i = i + 1
                filas_dm = cursor_filas.NextRow
                m_Codigo_NumReg = i
            Loop

            If m_Codigo_NumReg = 0 Then
                MsgBox("NO EXISTE EL USUARIO REGISTRADO PARA REALIZAR LA CONSULTA", vbCritical, "OBSERVACION...")
                Exit Sub
            End If

            ' Me.Hide()
            MsgBox("BIENVENIDO AL SISTEMA DE EVALUACION DE DERECHOS MINEROS", vbExclamation, "ARCGIS")
            clave_valida = True


        Catch ex As Exception
            clave_valida = False
            conta_p = conta_p + 1
            MsgBox("EL USUARIO Y/O PASSWORD ES INCORRECTO PARA ACCEDER A LA APLICACION, VERIFICAR SI SUS DATOS INGRESADOS SON CORRECTOS", vbInformation, "OBSERVACION")
            If conta_p = 3 Then
                MsgBox("ES EL TERCER INTENTO, LA APLICACIÓN SE CERRARÁ, VERIFICAR SI SU USUARIO Y/O PASSWORD INGRESADO ES EL CORRECTO, VERIFICAR...", vbCritical, "OBSERVACIÓN...")
                Me.Hide() 'FRM_PERMISO.Hide()
                'aplicacion.Shutdown()
            End If
        End Try
    End Sub

    Private Sub GEOCATMIN_IniLogin_1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class