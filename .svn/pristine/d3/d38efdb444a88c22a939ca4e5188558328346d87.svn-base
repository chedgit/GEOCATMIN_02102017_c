Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(btn_Dibujar_Poligono.ClassId, btn_Dibujar_Poligono.InterfaceId, btn_Dibujar_Poligono.EventsId), _
 ProgId("GEOCATMIN.btn_Dibujar_Poligono")> _
Public NotInheritable Class btn_Dibujar_Poligono
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "56d8aa1b-5bad-48d0-acc1-abbb8d7225d4"
    Public Const InterfaceId As String = "a18c5f19-9ff1-4118-8e1f-af69a3218589"
    Public Const EventsId As String = "bb840491-2a39-49de-ac4a-c192ed6f8e93"
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
        MyBase.m_category = "Dibujar Polígono"  'localizable text 
        MyBase.m_caption = "Dibujar Polígono"   'localizable text 
        MyBase.m_message = "Dibujar Polígono"   'localizable text 
        MyBase.m_toolTip = "Dibujar Polígono" 'localizable text 
        MyBase.m_name = "Dibujar Polígono"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

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
            If glo_Tool_BT_26 = True Then
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
        'TODO: Add btn_Dibujar_Poligono.OnClick implementation
        Dim pForm As New Frm_GraficaPoligono '  frm_Generar_Circulo
        'Dim pForm As New frm_Grafica_Excel
        'Dim pForm As New frm_Generar_Malla_100 '  frm_Generar_Circulo
        pForm.m_application = m_application
        pForm.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
        pForm.Show()
        SetWindowLong(pForm.Handle.ToInt32(), GWL_HWNDPARENT, m_application.hWnd)



        'Dim m_form1 As New Frm_Eval_LibreDenu

        'If Not m_form1.Visible Then
        '    m_form1.m_Application = m_application
        '    m_form1.Show()
        '    SetWindowLong(m_form1.Handle.ToInt32(), GWL_HWNDPARENT, m_application.hWnd)
        'End If


    End Sub

End Class



