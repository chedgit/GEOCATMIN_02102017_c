Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(btn_graficarDM_Masivo.ClassId, btn_graficarDM_Masivo.InterfaceId, btn_graficarDM_Masivo.EventsId), _
 ProgId("SIGCATMIN.btn_graficarDM_Masivo")> _
Public NotInheritable Class btn_graficarDM_Masivo
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "de52ff67-5344-4657-a138-59348cc8ad7c"
    Public Const InterfaceId As String = "96cd82e3-6fd7-495f-9111-7c94ee7c999e"
    Public Const EventsId As String = "d737008e-e48a-48c8-b526-1ee8834b5263"
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
        MyBase.m_category = "Generar_Plano_DM_Masivo"  'localizable text 
        MyBase.m_caption = "Generar_Plano_DM_Masivo"   'localizable text 
        MyBase.m_message = "Generar planos catastrales Masivo"   'localizable text 
        MyBase.m_toolTip = "Generar planos catastrales eval. Masivo" 'localizable text 
        MyBase.m_name = "Generar_Plano_DM_Masivo"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")





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
        'TODO: Add btn_graficarDM_Masivo.OnClick implementation


        If x = 0 Then  'entra para conectarse por primera vez
            Dim m_form As New Frm_Eval_segun_codigo
            If Not m_form.Visible Then
                x = 1
                m_form.m_Application = m_application
                m_form.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
                m_form.Show()
                SetWindowLong(m_form.Handle.ToInt32(), GWL_HWNDPARENT, m_application.hWnd)
            End If


        End If

        Dim pForm As New frmImportar_excel
        pForm.m_Application = m_application
        pForm.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
        pForm.Show()
        SetWindowLong(pForm.Handle.ToInt32(), GWL_HWNDPARENT, m_application.hWnd)


    End Sub
End Class



