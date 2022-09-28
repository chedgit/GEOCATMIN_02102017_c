Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(btn_Ver_Google.ClassId, btn_Ver_Google.InterfaceId, btn_Ver_Google.EventsId), _
 ProgId("GEOCATMIN.btn_Ver_Google")> _
Public NotInheritable Class btn_Ver_Google
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "ad4f85e4-c121-48c9-b284-5bace0d28aba"
    Public Const InterfaceId As String = "acdf7fc5-4093-4967-891f-f6c85c215068"
    Public Const EventsId As String = "a83e86e2-e36b-4c87-a2a4-b913ff4ecdd3"
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
        MyBase.m_category = "DM en Google Earth"  'localizable text 
        MyBase.m_caption = "DM en Google Earth"   'localizable text 
        MyBase.m_message = "DM en Google Earth"   'localizable text 
        MyBase.m_toolTip = "DM en Google Earth" 'localizable text 
        MyBase.m_name = "DM en Google Earth"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")
        Try
            'TODO: change bitmap name if necessary
            Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
            MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
        End Try


    End Sub

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
        ' Dim cls_Catastro As New cls_DM_1
        ' cls_Catastro.Ver_Google_Earth(glo_xMin, glo_yMin, glo_xMax, glo_yMax, v_zona_dm)

        Dim clsGoogleEarth As New cls_Google_Earth
        clsGoogleEarth.Genera_KML_Google(m_application, v_zona_dm)  'Se hizo otra forma de llamarlo


    
    End Sub
End Class



