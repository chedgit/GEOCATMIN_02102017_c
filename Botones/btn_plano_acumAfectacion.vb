Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(btn_plano_acumAfectacion.ClassId, btn_plano_acumAfectacion.InterfaceId, btn_plano_acumAfectacion.EventsId), _
 ProgId("SIGCATMIN.btn_plano_acumAfectacion")> _
Public NotInheritable Class btn_plano_acumAfectacion
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "6a4567f7-7fc9-4b49-bcf2-2762734ca0de"
    Public Const InterfaceId As String = "622bf091-1e1c-4909-9d04-fdd3392b0464"
    Public Const EventsId As String = "07be1bee-f570-46c2-bd2c-210a5e0eca22"
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
        MyBase.m_category = "Plano de Afectaciones_Acumulacion"  'localizable text 
        MyBase.m_caption = "Plano de Afectaciones_Acumulacion"   'localizable text 
        MyBase.m_message = "Plano de Afectaciones_Acumulacion"   'localizable text 
        MyBase.m_toolTip = "Plano de Afectaciones_Acumulacion" 'localizable text 
        MyBase.m_name = "Plano de Afectaciones_Acumulacion"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

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
        'TODO: Add btn_plano_acumAfectacion.OnClick implementation
        If v_afecta > 0 Then
            Dim cls_planos As New Cls_planos
            caso_consulta = "ACUMULACION AFECTACION"
            cls_planos.generaplano_AfectacionAcumulacion(m_application)
            'cls_planos.generaplanoacumulacion(m_application)
        Else
            MsgBox("No hay presencia de Afectaciones del Estado en el Área")
            Exit Sub
        End If

    End Sub
End Class



