Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports System.Windows.Forms
Imports ESRI.ArcGIS.GISClient
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Carto

<ComClass(Tool1_planoventa.ClassId, Tool1_planoventa.InterfaceId, Tool1_planoventa.EventsId), _
 ProgId("GEOCATMIN.Tool1_planoventa")> _
Public NotInheritable Class Tool1_planoventa
    Inherits BaseTool

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "28de6d77-c04f-4ed0-a7dd-d20f405bf1ef"
    Public Const InterfaceId As String = "9220129c-275d-42e8-8c39-76cc780e0221"
    Public Const EventsId As String = "bb4980b7-d00b-43c8-bbfa-9fa9ec4c6d67"
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
        MyBase.m_category = "Genera Plano Venta"  'localizable text 
        MyBase.m_caption = "Genera Plano Venta"   'localizable text 
        MyBase.m_message = "Genera Plano Venta"   'localizable text 
        MyBase.m_toolTip = "Genera Plano Venta" 'localizable text 
        MyBase.m_name = "Genera Plano Venta"  'unique id, non-localizable (e.g. "MyCategory_ArcMapTool")
        Try
            'TODO: change resource name if necessary
            Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
            MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
            MyBase.m_cursor = New System.Windows.Forms.Cursor(Me.GetType(), Me.GetType().Name + ".cur")
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
        End Try
    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            If glo_Tool_BT_20 = True Then
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
        'TODO: Add Tool1_planoventa.OnClick implementation
    End Sub

    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'TODO: Add Tool1_planoventa.OnMouseDown implementation
        'Dim cls_dm1 As New cls_DM_1
        'cls_dm1.generaplanoventa()
        tipo_plano = ""
        tipo_sel_escala = ""
        If ((tipo_seleccion = "OP_11") Or (tipo_seleccion = "OP_5") Or (tipo_seleccion = "OP_6") Or (tipo_seleccion = "OP_12")) Then
            MsgBox("La opción consultada no es del tipo Consulta DM, para Generar Plano Venta", MsgBoxStyle.Information, "BDGEOCATMIN...")
            Exit Sub
        End If
        Dim cls_dm1 As New cls_DM_1
        Dim formulario2 As New Frm_formatoplanos
        Dim pRubberBand As IRubberBand
        pMap = pMxDoc.FocusMap
        Dim pActiveView As IActiveView
        pActiveView = pMap
        pRubberBand = New RubberEnvelope
        pActiveView.Extent = pRubberBand.TrackNew(pActiveView.ScreenDisplay, Nothing)
        pActiveView.Refresh()
        If pMap.LayerCount = 0 Then
            MsgBox("No Existe el tema de Catastro en la Vista para Generar el Plano", vbCritical, "BDGEOCATMIN...")
            Exit Sub
        End If

        Dim pForm As New Frm_formatoplanos

        pForm.p_App = m_application
        pForm.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
        pForm.ShowDialog()
        Select Case pForm.DialogResult
            Case DialogResult.OK
                ' MsgBox("ok")
            Case DialogResult.Cancel
                pForm.Close()
        End Select
    End Sub

    Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'TODO: Add Tool1_planoventa.OnMouseMove implementation
    End Sub

    Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'TODO: Add Tool1_planoventa.OnMouseUp implementation
    End Sub
End Class

