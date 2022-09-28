Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(btn_Plano_Simultaneo.ClassId, btn_Plano_Simultaneo.InterfaceId, btn_Plano_Simultaneo.EventsId), _
 ProgId("SIGCATMIN.btn_Plano_Simultaneo")> _
Public NotInheritable Class btn_Plano_Simultaneo
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "db2d62cf-f6e2-4ba5-9b27-e23f3301cb41"
    Public Const InterfaceId As String = "3e09be4d-fbbe-4d27-893d-f719f873ddde"
    Public Const EventsId As String = "79eb6a0d-4520-48f6-9d5d-92ce6e5bfafe"
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
        MyBase.m_category = "Plano Simultaneo"  'localizable text 
        MyBase.m_caption = "Plano Simultaneo"   'localizable text 
        MyBase.m_message = "Plano Simultaneo"   'localizable text 
        MyBase.m_toolTip = "Plano Simultaneo" 'localizable text 
        MyBase.m_name = "Plano Simultaneo"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

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
        Dim loCodigo As String = ""
        'TODO: Add btn_Plano_Simultaneo.OnClick implementation
        If v_tipo_exp = "PE" Then
            If Val(dt_dmsi.Rows.Count) <> "0" Then
                For w As Integer = 0 To dt_dmsi.Rows.Count - 1
                    loCodigo = loCodigo & "" & dt_dmsi.Rows(w).Item("codigo") & "_"
                Next
                loCodigo = Mid(loCodigo, 1, Len(loCodigo) - 1) & "_" & v_codigo & ""
                loCodigosim = loCodigo
                Dim pForm As New frm_grupo_pesiev
                pForm.m_application = m_application
                'pForm.ShowDialog()
                pForm.Show()
            Else
                MsgBox("No Existe DM Simultaneo ", MsgBoxStyle.Information, "[BDGEOCIENTÍFICA]")
                Exit Sub
            End If
        Else
            MsgBox("No Existe DM Simultaneo ", MsgBoxStyle.Information, "[BDGEOCIENTÍFICA]")
            Exit Sub
        End If
    End Sub
End Class



