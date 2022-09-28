Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.DataSourcesOleDB
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem
Imports PORTAL_Clases

Public Class cls_Conexion_Oracle
    Public pPropset As IPropertySet
    Public pWorkspace As IWorkspace

    Public Sub Conexion_Oracle_SDE(ByVal p_Server As String, ByVal p_Usuario As String, ByVal p_Password As String, ByVal p_Version As String, ByVal p_App As IApplication)
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        Dim pPropset As IPropertySet
        pPropset = New PropertySet
        With pPropset
            .SetProperty("SERVER", p_Server) '"10.102.0.12")
            .SetProperty("INSTANCE", "5151")
            '.SetProperty "Database", "sde" ' Ignored with ArcSDE for Oracle 
            .SetProperty("USER", p_Usuario)
            .SetProperty("PASSWORD", p_Password)
            .SetProperty("VERSION", p_Version)
        End With
        pWorkspaceFactory = New SdeWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.Open(pPropset, p_App.hWnd) '0)
    End Sub

    Public Function Leer_Tabla_Oracle(ByVal pastrCodigo As String, ByVal pastrPosicion As String) As ICursor
        Dim lodtTabla As New DataTable
        Dim cursor_filas As ICursor
        '*****************************************************************
        Try
            pPropset = New PropertySet
            pPropset.SetProperty("CONNECTSTRING", "Provider=MSDAORA.1;Data source=DESA_DM;User ID=" & gstrUsuarioAcceso & ";Password=" & gstrUsuarioClave)
            'Creando sentencias del OLEDB
            Dim pWorkspaceFact As IWorkspaceFactory
            pWorkspaceFact = New OLEDBWorkspaceFactory
            pWorkspace = pWorkspaceFact.Open(pPropset, 0)
            Dim pFeatureWorkspace As IFeatureWorkspace
            pFeatureWorkspace = pWorkspace
            Dim pQueryDef As IQueryDef
            pQueryDef = pFeatureWorkspace.CreateQueryDef
            'Construyendo la consulta
            pQueryDef.Tables = "SG_D_PETITORIOS A, SC_V_COORDENADADET B"
            pQueryDef.SubFields = "DISTINCT A.CG_CODIGO CODIGO, A.PE_NOMDER NOMBRE, A.CA_CODCAR CARTA, A.PE_ZONCAT ZONA," & _
                                  "A.TE_TIPOEX TIPO, A.PE_METALI NATURALEZA, " & _
                                  "PACK_DBA_SG_D_HECTAEXPEDIENTE.HECTA(A.CG_CODIGO, '01') HECTAREA, A.EE_ESTAEX ESTADO"
            Select Case pastrPosicion
                Case 1
                    pQueryDef.WhereClause = "A.CG_CODIGO = B.CG_CODIGO AND A.CG_CODIGO = '" & pastrCodigo.ToUpper & "'"
                Case 2
                    pQueryDef.WhereClause = "A.CG_CODIGO = B.CG_CODIGO AND A.PE_NOMDER = '" & pastrCodigo.ToUpper & "'"
            End Select
            cursor_filas = pQueryDef.Evaluate
            Return cursor_filas
        Catch ex As Exception
        End Try
    End Function
End Class

