Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geodatabase



<ComClass(Generar_Layer_Aster.ClassId, Generar_Layer_Aster.InterfaceId, Generar_Layer_Aster.EventsId), _
 ProgId("GEOCATMIN.Generar_Layer_Aster")> _
Public NotInheritable Class Generar_Layer_Aster
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "eb0cfe31-3817-4595-8948-d9804526b82d"
    Public Const InterfaceId As String = "f1f57a10-060a-4225-9256-3943689e1c96"
    Public Const EventsId As String = "50044087-1d0c-4d10-a45e-d68946dab9a8"
#End Region

#Region "COM Registration Function(s)"
    <ComRegisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub RegisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryRegistration(registerType)

        'Add any COM registration code after the ArcGISCategoryRegistration() call

    End Sub

    <ComUnregisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub UnregisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryUnregistration(registerType)

        'Add any COM unregistration code after the ArcGISCategoryUnregistration() call

    End Sub

#Region "ArcGIS Component Category Registrar generated code"
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Register(regKey)

    End Sub
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Unregister(regKey)

    End Sub

#End Region
#End Region


    Private m_application As IApplication

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        ' TODO: Define values for the public properties
        MyBase.m_category = "Genera Poligono"  'localizable text 
        MyBase.m_caption = "Genera Poligono"   'localizable text 
        MyBase.m_message = "Genera Poligono"   'localizable text 
        MyBase.m_toolTip = "Genera Poligono" 'localizable text 
        MyBase.m_name = "Genera Poligono"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

        Try
            'TODO: change bitmap name if necessary
            Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
            MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
        End Try


    End Sub
    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            If glo_Tool_BT_3 = True Then
                Return True
            Else
                Return False
            End If
            Return MyBase.Enabled
        End Get
    End Property

    Public Overrides Sub OnCreate(ByVal hook As Object)
        If Not hook Is Nothing Then
            m_application = CType(hook, IApplication)

            'Disable if it is not ArcMap
            If TypeOf hook Is IMxApplication Then
                MyBase.m_enabled = True
            Else
                MyBase.m_enabled = False
            End If
        End If

        ' TODO:  Add other initialization code
    End Sub

    Public Overrides Sub OnClick()
        Dim cls_Catastro As New cls_DM_1
        Dim cls_DM_2 As New cls_DM_2
        'TODO: Add Generar_Layer_Aster.OnClick implementation
        Dim pTable As ITable
        Dim lodtbDatos As New DataTable
        cls_Catastro.Conexion_GeoDatabase()
        pTable = pFeatureWorkspace.OpenTable("Tabla_Inventario_Aster")
        Dim pFeatureCursor As ICursor
        Dim lodtRegistros As New DataTable
        Dim loNumCol, loNumColTemp As Int16
        pFields = pTable.Fields
        loNumCol = pFields.FieldCount
        Dim c As Int16
        For c = 1 To loNumCol - 1
            lodtRegistros.Columns.Add(pFields.Field(c).Name)
        Next
        pFeatureCursor = pTable.Search(Nothing, False)
        Dim pRow As IRow
        pRow = pFeatureCursor.NextRow
        Dim k As Int16 = 0
        Try
            Do Until pRow Is Nothing
                If Not IsDBNull(pRow.Value(loNumColTemp)) Then
                    Dim dr As DataRow
                    dr = lodtRegistros.NewRow
                    For c = 1 To loNumCol - 1
                        Try
                            If Not IsDBNull(pRow.Value(c)) Then dr.Item(c - 1) = CType(pRow.Value(c), String)
                        Catch ex As Exception
                        End Try
                    Next
                    lodtRegistros.Rows.Add(dr)
                End If
                For j As Integer = 0 To 3
                    'MsgBox(lodtRegistros.Rows(c).Item("N1").ToString)
                    If k = 0 And j = 0 Then
                        lodtbDatos.Columns.Add("CG_CODIGO", Type.GetType("System.String"))
                        lodtbDatos.Columns.Add("PE_NOMDER", Type.GetType("System.String"))
                        lodtbDatos.Columns.Add("CD_COREST", Type.GetType("System.Double"))
                        lodtbDatos.Columns.Add("CD_CORNOR", Type.GetType("System.Double"))
                        lodtbDatos.Columns.Add("CD_NUMVER", Type.GetType("System.Double"))
                    End If
                    Dim dRow As DataRow
                    dRow = lodtbDatos.NewRow
                    dRow.Item("CG_CODIGO") = lodtRegistros.Rows(k).Item("Numero").ToString '"DM_" & k + 1
                    dRow.Item("PE_NOMDER") = lodtRegistros.Rows(k).Item("LocalGranuleID").ToString '"DM_" & k + 1
                    dRow.Item("CD_COREST") = lodtRegistros.Rows(k).Item("E" & j + 1).ToString
                    dRow.Item("CD_CORNOR") = lodtRegistros.Rows(k).Item("N" & j + 1).ToString
                    dRow.Item("CD_NUMVER") = j + 1
                    lodtbDatos.Rows.Add(dRow)
                Next
                k = k + 1
                pRow = pFeatureCursor.NextRow
            Loop
            'MsgBox(lodtRegistros)
            'cls_Catastro.Load_FC("gpo_geo_aster", "", m_application, True)
            cls_Catastro.Delete_Rows_FC_GDB("gpo_geo_aster")
            cls_DM_2.Genera_Poligono_100Ha(lodtbDatos, gloZona, m_application, 1)
        Catch ex As Exception
            MsgBox(ex.Message & "" & k)
        End Try
    End Sub
End Class



