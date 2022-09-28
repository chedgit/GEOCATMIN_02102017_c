Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports PORTAL_Clases


<ComClass(btnGuardaFA.ClassId, btnGuardaFA.InterfaceId, btnGuardaFA.EventsId), _
 ProgId("GEOCATMIN.btnGuardaFA")> _
Public NotInheritable Class btnGuardaFA
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "525e1265-242a-4408-a989-80803163f6b1"
    Public Const InterfaceId As String = "3a80a42b-8ace-4a87-9266-153f6c72a616"
    Public Const EventsId As String = "50695e9b-9407-45e6-b79a-df9cb1f4bf7a"
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
        MyBase.m_category = "Guardar Informe F.A."  'localizable text 
        MyBase.m_caption = "Guardar Informe F.A."   'localizable text 
        MyBase.m_message = "Guardar Informe F.A."   'localizable text 
        MyBase.m_toolTip = "Guardar Informe F.A." 'localizable text 
        MyBase.m_name = "Guardar Informe F.A."  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

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
        'TODO: Add btnGuardaFA.OnClick implementation
        'Guarda los datos de las opciones validadas por el usuario para informe tecnico de evaluación
        '---------------------------------------------------------------------------------------------
        Dim lointRpta1 As String
        Dim lostrRetorno_val As String
        Dim cls_Oracle As cls_Oracle

        lointRpta1 = MsgBox("¿ Desea Guardar los Datos Según las Opciones Trabajadas para su Informe Técnico de EValuación..." & vbNewLine & _
          "Caso Contrario, Seleccionar NO ?", MsgBoxStyle.YesNo, "BDGEOCATMIN")
        Select Case lointRpta1

            Case 6 ''Si guarda informe
                valida_informe = "Guardado"
                If var_fa_validaeval = True Or var_fa_AreaSuper = True Or var_Fa_AreaReserva = True Or var_fa_Coord_SuperAreaReserva = True Or var_Fa_Zonaurbana = True Or var_Fa_AreasNaturales = True Then
                    Accion_proceso = True

                    Dim m_form As New Form_DatosProcesados
                    If Not m_form.Visible Then
                        m_form.m_application = m_application
                        m_form.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
                        m_form.ShowDialog()

                    End If
                End If



                ' MsgBox("Se ha Guardado los datos según las opciones trabajadas por Ud. para el Informe Técnico de Evaluación..", MsgBoxStyle.Exclamation, "BDGEOCATMIN")

            Case 7 '  'Si no guarda ningun datos
                lostrRetorno_val = cls_Oracle.FT_SG_D_EVALGIS("DEL", v_codigo, glo_InformeDM, gstrCodigo_Usuario)
                var_fa_validaeval = False
                var_fa_AreaSuper = False
                var_Fa_AreaReserva = False
                var_fa_Coord_SuperAreaReserva = False
                var_Fa_Zonaurbana = False
                var_Fa_AreasNaturales = False
        End Select

    End Sub
End Class



