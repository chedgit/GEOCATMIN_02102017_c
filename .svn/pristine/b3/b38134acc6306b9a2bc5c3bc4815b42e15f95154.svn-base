Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports PORTAL_Clases

<ComClass(btnNuevoFA.ClassId, btnNuevoFA.InterfaceId, btnNuevoFA.EventsId), _
 ProgId("GEOCATMIN.btnNuevoFA")> _
Public NotInheritable Class btnNuevoFA
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "3a3750e5-88dd-415e-a4a6-de6b70b9bdd6"
    Public Const InterfaceId As String = "10878463-85a5-43ea-9cd9-eaf50fd3828b"
    Public Const EventsId As String = "458b95f9-2357-406d-aa6d-be62528a041d"
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
        MyBase.m_category = "Generar Histórico FA"  'localizable text 
        MyBase.m_caption = "Generar Histórico FA"   'localizable text 
        MyBase.m_message = "Generar Histórico FA"   'localizable text 
        MyBase.m_toolTip = "Generar Histórico FA" 'localizable text 
        MyBase.m_name = "Generar Histórico FA"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

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
        'TODO: Add btnNuevoFA.OnClick implementation
        Dim cls_Oracle As New cls_Oracle
        Dim lostrVerifica As String = ""
        Dim lostrOpcion As String = ""

        If glo_InformeDM <> "0" Then
            lostrOpcion = MsgBox("¿ Desea Guardar sus datos como Histórico de este DM = " & v_codigo & "? ", MsgBoxStyle.YesNo, "BDGEOCATMIN - Histórico")
            Select Case lostrOpcion
                Case "7" 'Cancela
                    Exit Sub
                Case "6"
                    lostrVerifica = cls_Oracle.FT_GENERA_HISTORICO(v_codigo, glo_InformeDM)
                    MsgBox("Se Ha Guardado el histórico Satisfactoriamente del DM = " & v_codigo, MsgBoxStyle.Information, "Formatos Automáticos")
            End Select
        Else
            MsgBox("No se ouede generar Histórico", MsgBoxStyle.Critical, "Formatos Automáticos ...")
        End If

    End Sub

End Class



