Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports System.Windows.Forms

<ComClass(Tool_Porcentaje_Region.ClassId, Tool_Porcentaje_Region.InterfaceId, Tool_Porcentaje_Region.EventsId), _
 ProgId("GEOCATMIN.Tool_Porcentaje_Region")> _
Public NotInheritable Class Tool_Porcentaje_Region
    Inherits BaseTool

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "91297545-dade-4764-a587-fbe363631507"
    Public Const InterfaceId As String = "8141d577-6445-454b-9317-308a7302017e"
    Public Const EventsId As String = "9f39be57-66b0-44b7-aa5a-916da7f0e528"
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
        ' TODO: Define values for the public properties
        MyBase.m_category = "Cálcula Porcentaje Región"  'localizable text 
        MyBase.m_caption = "Cálcula Porcentaje Región"   'localizable text 
        MyBase.m_message = "Cálcula Porcentaje Región"   'localizable text 
        MyBase.m_toolTip = "Cálcula Porcentaje Región" 'localizable text 
        MyBase.m_name = "Cálcula Porcentaje Región"  'unique id, non-localizable (e.g. "MyCategory_ArcMapTool")

        Try
            'TODO: change resource name if necessary
            Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
            MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
            MyBase.m_cursor = New System.Windows.Forms.Cursor(Me.GetType(), Me.GetType().Name + ".cur")
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
        'TODO: Add Tool_Porcentaje_Region.OnClick implementation
       
    End Sub
    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            If glo_Tool_BT_18 = True Then
                Return True
            Else
                Return False
            End If
            Return MyBase.Enabled
        End Get
    End Property
    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'Dim cls_catastro As New cls_DM_1
        'pEste = X
        'pNorte = Y
        Dim pForm As New frm_Grafica_Consulta_XY
        pForm.pApp = m_application
        pForm.pEste = X
        pForm.pNorte = Y
        pForm.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
        caso_opcion_tools = "Por Region"
        pForm.ShowDialog()
        SetWindowLong(pForm.Handle.ToInt32(), GWL_HWNDPARENT, m_application.hWnd)
        Select Case pForm.DialogResult
            Case DialogResult.OK
            Case DialogResult.Cancel
                pForm.Close()
        End Select
        'cls_catastro.rotulatexto_dm_poligono(m_application, X, Y)
    End Sub

    Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'TODO: Add Tool_Porcentaje_Region.OnMouseMove implementation
    End Sub

    Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'TODO: Add Tool_Porcentaje_Region.OnMouseUp implementation
    End Sub
End Class

