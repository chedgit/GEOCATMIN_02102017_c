Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(btn_GenerarPlano.ClassId, btn_GenerarPlano.InterfaceId, btn_GenerarPlano.EventsId), _
 ProgId("GEOCATMIN.btn_GenerarPlano")> _
Public NotInheritable Class btn_GenerarPlano
    Inherits BaseCommand
#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "63699105-0897-47a6-8dd5-0746700a005e"
    Public Const InterfaceId As String = "2df19f9d-c705-4367-947e-229877ebf748"
    Public Const EventsId As String = "5cef66ca-363a-46bb-81dc-fbc7ac2dbfec"
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
        MyBase.m_category = "Plano Evaluación"  'localizable text 
        MyBase.m_caption = "Plano Evaluación"   'localizable text 
        MyBase.m_message = "Plano Evaluación"   'localizable text 
        MyBase.m_toolTip = "Plano Evaluación" 'localizable text 
        MyBase.m_name = "Genera Plano Evaluación"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")
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
            If glo_Tool_BT_13 = True Then
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
        'TODO: Add btn_GenerarPlano.OnClick implementation
        Dim cls_planos As New Cls_planos
        caso_consulta = "CATASTRO MINERO"
        cls_planos.genera_planoevaluacion(m_application, "Evaluacion")


    End Sub
End Class



