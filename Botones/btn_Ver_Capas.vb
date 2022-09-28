Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(btn_Ver_Capas.ClassId, btn_Ver_Capas.InterfaceId, btn_Ver_Capas.EventsId), _
 ProgId("GEOCATMIN.btn_Ver_Capas")> _
Public NotInheritable Class btn_Ver_Capas
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "58b2b2d2-c3a3-4843-8d98-baf6c7c473d2"
    Public Const InterfaceId As String = "2e590d76-fadd-4b61-bc3a-0ce98dfca685"
    Public Const EventsId As String = "38e2ef30-0da9-4245-adbe-f8dccaa4067b"
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
    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            If glo_Tool_BT_25 = True Then
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
        'TODO: Add btn_Ver_Capas.OnClick implementation
        ' If ((tipo_seleccion = "OP_11") Or (tipo_seleccion = "OP_2") Or (tipo_seleccion = "OP_3") Or (tipo_seleccion = "OP_4") Or (tipo_seleccion = "OP_9") Or (tipo_seleccion = "OP_12") Or (tipo_seleccion = "OP_1")) Then
        Dim m_form As New Frm_Agregarcapas

        If Not m_form.Visible Then
            m_form.m_application = m_application
            m_form.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
            m_form.Show()
            SetWindowLong(m_form.Handle.ToInt32(), GWL_HWNDPARENT, m_application.hWnd)
        End If
        ' End If

    End Sub
End Class



