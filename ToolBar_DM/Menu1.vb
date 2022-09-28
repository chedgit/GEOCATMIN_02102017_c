Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(Menu1.ClassId, Menu1.InterfaceId, Menu1.EventsId), _
 ProgId("GEOCATMIN.Menu1")> _
Public NotInheritable Class Menu1
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "20a72030-e469-4e58-b960-2b804c619501"
    Public Const InterfaceId As String = "b7376b26-ee0c-4b20-9a2a-975b2b91ad0f"
    Public Const EventsId As String = "fe1a9ea7-fbc6-4a96-9f04-d3af8f0e48ee"
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
        MyBase.m_category = "Derecho Minero x Código"  'localizable text 
        MyBase.m_caption = "Derecho Minero x Código"   'localizable text 
        MyBase.m_message = "Derecho Minero x Código"   'localizable text 
        MyBase.m_toolTip = "Derecho Minero x Código" 'localizable text 
        MyBase.m_name = "Derecho Minero x Código"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

        Try
            'TODO: change bitmap name if necessary
            Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
            MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
        End Try


    End Sub


    Public Overrides Sub OnCreate(ByVal hook As Object)
        'If Not hook Is Nothing Then
        '    m_application = CType(hook, IApplication)

        '    'Disable if it is not ArcMap
        '    If TypeOf hook Is IMxApplication Then
        '        MyBase.m_enabled = True
        '    Else
        '        MyBase.m_enabled = False
        '    End If
        'End If

        '' TODO:  Add other initialization code
    End Sub


    Public Overrides Sub OnClick()

        'TODO: Add tool_Segun_Codigo.OnClick implementation

        Dim m_form As New Frm_Eval_segun_codigo

        If Not m_form.Visible Then
            x = 1
            m_form.m_application = m_application
            m_form.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
            m_form.Show()
            SetWindowLong(m_form.Handle.ToInt32(), GWL_HWNDPARENT, m_application.hWnd)
        End If

    End Sub




End Class



