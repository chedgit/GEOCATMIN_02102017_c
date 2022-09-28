Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports System.Runtime.InteropServices

<ComClass(Barra_General.ClassId, Barra_General.InterfaceId, Barra_General.EventsId), _
 ProgId("GEOCATMIN.ArcGISToolbar1")> _
Public NotInheritable Class Barra_General
    Inherits BaseToolbar

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
    Public Const ClassId As String = "c0250d52-4906-4d65-86ca-b7ee931edee9"
    Public Const InterfaceId As String = "bde855a6-880c-47a2-aba7-d598e01d15cc"
    Public Const EventsId As String = "3b03ecdf-5782-4d18-b61a-db7f0b2849a4"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        'TODO: Define your toolbar here by adding items        '
        'AddItem("esriArcMapUI.ZoomOutTool")
        BeginGroup() 'Separator
        'AddItem("{FBF8C3FB-0480-11D2-8D21-080009EE4E51}", 1) 'undo command
        AddItem(New Guid("6ed1d0e8-5c70-46f4-a47f-2a19a1964ef4"), 1) 'Formulario Principal
        AddItem(New Guid("01ba55bb-6c30-4eb3-9d73-74546d5eed66"), 1) 'Desabilitar Conexión

        'AddItem(New Guid("8f1c8b9e-c773-4433-ba84-d905761803f8"), 1) 'Tool Consulta DM
        'AddItem(New Guid("5ac787f1-758c-4307-8039-fb6c1e0491f6"), 1) 'Tool Consulta XY
        'AddItem(New Guid("65cbee81-a537-44bc-8e12-a9d662805900"), 1) 'Tool Reporte_Derechos Mineros
        'AddItem(New Guid("e54aa01c-4a76-4f2e-8076-bd33c00db760"), 1) 'Resultado de Evaluación 
        'AddItem(New Guid("dde8d224-ace4-4dc5-b4e8-5e332a12d3b4"), 1) 'Calculo Area Disponible
        'AddItem(New Guid("63699105-0897-47a6-8dd5-0746700a005e"), 1) 'Generar Plano
        'AddItem(New Guid("c9e3dfab-6f39-47d3-9fb1-09e70c9f6209"), 1) 'Ver Anterior,Po, Co
        'AddItem(New Guid("966f461b-beec-4e4b-845a-4126fe301e08"), 1) 'Ver Anteriores
        'AddItem(New Guid("1dfad8e1-e54b-43cf-9cd0-d65e1774b4ca"), 1) 'Ver AP,Po
        'AddItem(New Guid("95f72a52-2b61-41a8-ba25-091a7152d049"), 1) 'Ver Colindantes
        'AddItem(New Guid("43ae33ec-c7ad-41d2-970b-b70b8c5822b6"), 1) 'Ver Evaluado
        'AddItem(New Guid("994997fe-e112-478e-9e8a-7129c9ec9d98"), 1) 'Ver Extinguidos
        'AddItem(New Guid("148cbae7-4302-4eff-b364-b74e4883dbe6"), 1) 'Ver Posteriores
        'AddItem(New Guid("e9f195c0-a8af-429c-9416-37a81116a25c"), 1) 'Ver Simulataneo
        'AddItem(New Guid("be8c98c9-7f99-4c97-adbb-0dd27e9cd11b"), 1) 'Ver Todos
        'AddItem(New Guid("2140c9a0-3896-4b14-907e-ce928de3eac3"), 1) 'Plano Areas Superpuestas
        'AddItem(New Guid("869c9c09-4872-4bcb-be56-a5f9d44bf6b5"), 1) 'Generar Malla 100 Ha.
        'AddItem(New Guid("b5becf24-39e0-44a7-a0a5-6eeda6ca1de7"), 1) 'Genera_Plano_Demarca
        'AddItem(New Guid("c7d95da2-ab70-4695-8522-7f4e613171a8"), 1) 'Genera_Plano_carta *
        'AddItem(New Guid("f7b6646a-731d-4a38-b12e-d71fd3f0472e"), 1) 'Genera Observ. de Carta
        'AddItem(New Guid("91297545-dade-4764-a587-fbe363631507"), 1) 'Calcula Porcentaje de la Región
        'AddItem(New Guid("5628d1d7-1a48-48ca-9b5f-1ba90cb370ab"), 1) 'Agregar Limites Regional
        'AddItem(New Guid("28de6d77-c04f-4ed0-a7dd-d20f405bf1ef"), 1) 'Genera Plano Venta *
        '******************************************
        '******************************************
        'AddItem(New Guid("fdf0e2a9-884b-4731-a917-0eabccb90a79"), 1) 'Simular DM
        ' AddItem(New Guid("20fc5094-8c6f-4f68-a0e4-3770f2d6101f"), 1) 'Menu
        'AddItem(New Guid("bd86deb6-e5bb-4dc6-93ec-d2383ce9b108"), 1) 'covert to Geodatabase
        'AddItem(New Guid("eb0cfe31-3817-4595-8948-d9804526b82d"), 1) 'Genera Poligonos Aster
    End Sub

    Public Overrides ReadOnly Property Caption() As String
        Get
            'TODO: Replace bar caption
            Return "Sistema BDGeocatmin - INGEMMET"
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            'TODO: Replace bar ID
            Return "ArcGISToolbar1"
        End Get
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        'Private Sub Elimina_Archivo_Temporal()
        Try
            Kill(glo_pathTMP & "\*.*") ' Eliminar Directorio Temporal
        Catch ex As Exception
        End Try
        'End Sub
    End Sub



End Class
