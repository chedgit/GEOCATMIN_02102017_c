Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(btn_Zoom_to_Layer.ClassId, btn_Zoom_to_Layer.InterfaceId, btn_Zoom_to_Layer.EventsId), _
 ProgId("GEOCATMIN.btn_Zoom_to_Layer")> _
Public NotInheritable Class btn_Zoom_to_Layer
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "1293a1b1-3bdd-45ac-a4c5-f73e1f4dd0c2"
    Public Const InterfaceId As String = "78312a91-3f1b-4dcb-813a-ea115f1a2675"
    Public Const EventsId As String = "5e607f71-9724-44a2-b9e2-50db289c3fbf"
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
        ' MyBase.m_category = "Centrar capa Catastro"  'localizable text 
        ' MyBase.m_caption = "Centrar capa Catastro"   'localizable text 
        ' MyBase.m_message = "Centrar capa Catastro"   'localizable text 
        ' MyBase.m_toolTip = "Centrar capa Catastro" 'localizable text 
        ' MyBase.m_name = "Centrar capa Catastro"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

        MyBase.m_category = "Ver Capas"  'localizable text 
        MyBase.m_caption = "Ver Capas"   'localizable text 
        MyBase.m_message = "Ver Capas"   'localizable text 
        MyBase.m_toolTip = "Ver Capas" 'localizable text 
        MyBase.m_name = "Ver Capas"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

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
        'TODO: Add btn_Zoom_to_Layer.OnClick implementation
        Dim cls_catastro As New cls_DM_1
        cls_catastro.Zoom_to_Layer("Catastro")


        Dim m_form As New Frm_Agregarcapas

        If Not m_form.Visible Then
            m_form.m_application = m_application
            m_form.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
            m_form.Show()
            SetWindowLong(m_form.Handle.ToInt32(), GWL_HWNDPARENT, m_application.hWnd)
        End If


    End Sub
End Class



