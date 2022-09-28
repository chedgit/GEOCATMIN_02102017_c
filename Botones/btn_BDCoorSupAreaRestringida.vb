Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(btn_BDCoorSupAreaRestringida.ClassId, btn_BDCoorSupAreaRestringida.InterfaceId, btn_BDCoorSupAreaRestringida.EventsId), _
 ProgId("GEOCATMIN.btn_BDCoorSupAreaRestringida")> _
Public NotInheritable Class btn_BDCoorSupAreaRestringida
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "abcf1490-14d2-4e38-8931-db118f3609cc"
    Public Const InterfaceId As String = "13195709-a717-40f5-b8fe-028c091538b5"
    Public Const EventsId As String = "9cc4bb68-70da-4ef6-927a-c8005c0d7328"
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
        MyBase.m_category = "Superposición a Areas Restringidas"  'localizable text 
        MyBase.m_caption = "Superposición a Areas Restringidas"   'localizable text 
        MyBase.m_message = "Superposición a Areas Restringidas"   'localizable text 
        MyBase.m_toolTip = "Superposición a Areas Restringidas" 'localizable text 
        MyBase.m_name = "Superposición a Areas Restringidas"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

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
        'TODO: Add btn_BDCoorSupAreaRestringida.OnClick implementation
        Dim cls_eval As New Cls_evaluacion
        Dim cls_catastro As New cls_DM_1
        cls_catastro.Add_ShapeFile1("DM_" & v_codigo, m_application, "codigo")
        cls_catastro.Add_ShapeFile1("Zona Reservada" & fecha_archi, m_application, "reserva")
        cls_eval.Geoprocesamiento_temas("interceccion", m_application, "RESERVA")
        Dim pForm As New Frm_DMxAreaRestringida
        pForm.m_application = m_application
        pForm.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
        pForm.ShowDialog()
        SetWindowLong(pForm.Handle.ToInt32(), GWL_HWNDPARENT, m_application.hWnd)
    End Sub
End Class



