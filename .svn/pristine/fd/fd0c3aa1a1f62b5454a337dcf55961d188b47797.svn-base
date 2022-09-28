Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports System.Runtime.InteropServices

<ComClass(Menu_Base.ClassId, Menu_Base.InterfaceId, Menu_Base.EventsId), _
 ProgId("GEOCATMIN.Menu_Base")> _
Public NotInheritable Class Menu_Base
    Inherits BaseMenu

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
    ''' <summary>
    ''' Required method for ArcGIS Component Category registration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommandBars.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommandBars.Unregister(regKey)

    End Sub

#End Region
#End Region

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "20fc5094-8c6f-4f68-a0e4-3770f2d6101f"
    Public Const InterfaceId As String = "3ba240b1-523b-44d6-853f-0b84ee225056"
    Public Const EventsId As String = "472a1e70-bb7f-49a5-b14b-7bddadb33424"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        '
        'TODO: Define your menu here by adding items
        '
        'AddItem("esriArcMapUI.ZoomInFixedCommand")
        'BeginGroup() 'Separator
        AddItem(New Guid("5f82a4a6-2e02-478d-bfbb-5944ae36771c"), 1) 'redo command
        AddItem(New Guid("c78c5cfc-79f3-43a6-ad37-97ceb525e0b8"), 3) 'Según XY
        AddItem(New Guid("cb61dfac-21d7-438c-9520-4dc1dcaac418"), 4) 'Segun Límite
        AddItem(New Guid("03699ad9-d99e-4ab7-9651-7910d90a91a7"), 5) 'Segun Código
        AddItem(New Guid("afb74fdc-f0f1-46e5-863a-cf4e0fc59e42"), 6) 'Segun Ubigeo
        AddItem(New Guid("0e0d204e-af3a-40ff-a171-996cae0eb96a"), 7) 'Segun Varias Hojas
        AddItem(New Guid("a7396aa1-9bbe-4495-96cd-fb5ad15fe234"), 8) 'Segun Area de Reserva

    End Sub

    Public Overrides ReadOnly Property Caption() As String
        Get
            'TODO: Replace bar caption
            Return "Menu DM"
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            'TODO: Replace bar ID
            Try
                Kill(glo_pathTMP & "\catastro*.*")
            Catch ex As Exception
            End Try
            Return "Menu_Base"
        End Get
    End Property

End Class


