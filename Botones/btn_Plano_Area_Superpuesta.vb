Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(btn_Plano_Area_Superpuesta.ClassId, btn_Plano_Area_Superpuesta.InterfaceId, btn_Plano_Area_Superpuesta.EventsId), _
 ProgId("GEOCATMIN.btn_Plano_Area_Superpuesta")> _
Public NotInheritable Class btn_Plano_Area_Superpuesta
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "2140c9a0-3896-4b14-907e-ce928de3eac3"
    Public Const InterfaceId As String = "31238468-f8b8-43a1-80ea-c1e78533bbbb"
    Public Const EventsId As String = "18fbebea-18cc-49ae-b3f0-e0c48cd91d4d"
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
        MyBase.m_category = "Plano Areas Superpuestas"  'localizable text 
        MyBase.m_caption = "Plano Areas Superpuestas"   'localizable text 
        MyBase.m_message = "Plano Areas Superpuestas"   'localizable text 
        MyBase.m_toolTip = "Plano Areas Superpuestas" 'localizable text 
        MyBase.m_name = "Plano Areas Superpuestas"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

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
            If glo_Tool_BT_12 = True Then
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
        'TODO: Add btn_VerVecinos.OnClick implementation

        'se comento esta parte hoy 9/10/15 para ver areas restringidas raya vale
        '------------------------------
        Dim cls_Catastro As New cls_DM_1
        'cls_eval.verprioritatios("", "", "VE", "", "", "", "")



        'SE COMENTO DESDE AQUI 
        Dim cls_planos As New Cls_planos
        Dim cls_eval As New Cls_evaluacion
        If v_calculAreaint = False Then
            cls_eval.GeneraAreaDisponible_DM(m_application)
        End If


        s_tipo_plano = "Plano_areasuperpuesta"
        cls_planos.Genera_Plano_Area_Disponible(m_application)


        '--------------------------------------------------------------
        'no vale abajo
        ''If v_calculAreaint = False Then
        ''    fecha_archi_sup_t = DateTime.Now.Ticks.ToString
        ''    cls_Catastro.creandotabla_Areasup()
        ''End If
        ''cls_planos.Genera_Plano_Acumulacion(m_application)

        '  Dim cls_planos As New Cls_planos
        ' Dim cls_Catastro As New cls_DM_1
        'cls_planos.Genera_Plano_Areasup_dmxAreaRestringida(m_application)


    End Sub
End Class



