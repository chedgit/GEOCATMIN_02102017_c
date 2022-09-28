Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geometry


<ComClass(m_Form1.ClassId, m_Form1.InterfaceId, m_Form1.EventsId), _
 ProgId("GEOCATMIN.m_Form1")> _
Public NotInheritable Class m_Form1
    Inherits BaseTool

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "e9d3995c-7a24-4122-8a51-14acef480c5b"
    Public Const InterfaceId As String = "eb767009-96d0-495f-8c39-994e665fabe8"
    Public Const EventsId As String = "4d4353f0-cdd2-4a95-8dbd-bfa9320c27a2"
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
        MyBase.m_category = "Catastro Minero DM"  'localizable text 
        MyBase.m_caption = "Catastro Minero DM"   'localizable text 
        MyBase.m_message = "Catastro Minero DM"   'localizable text 
        MyBase.m_toolTip = "Catastro Minero DM" 'localizable text 
        MyBase.m_name = "Catastro Minero DM"  'unique id, non-localizable (e.g. "MyCategory_ArcMapTool")

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

        'TODO: Add tool_Segun_Codigo.OnClick implementation
     
        Dim m_form As New Frm_Eval_segun_codigo

        If Not m_form.Visible Then
            x = 1
            m_form.m_application = m_application
            m_form.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
            m_form.Show()
            SetWindowLong(m_form.Handle.ToInt32(), GWL_HWNDPARENT, m_application.hWnd)
        End If

    End Sub

    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        ''TODO: Add m_Form1.OnMouseDown implementation
        ''Try
        'Dim pMxApplication As IMxApplication
        'pMxApplication = m_application
        'Dim pPoint As IPoint = pMxApplication.Display.DisplayTransformation.ToMapPoint(X, Y)
        'Dim pForm1 As New frm_Ingreso_Codigo
        ''Dim parent As Win32HWNDWrapper
        ''parent = New Win32HWNDWrapper(pApplication.hWnd)
        ''    pDialog = New Puntos.IngresarPuntos(pMxDocument, pApplication)
        ''    pDialog.txtX.Text = pPoint.X
        ''    pDialog.txtY.Text = pPoint.Y
        ''    pDialog.ShowDialog(parent)
        ''    pDialog.Focus()
        ''Catch exception As Exception
        ''End Try
        'pForm1.v_Este = pPoint.X
        'pForm1.v_Norte = pPoint.Y
        ''pForm1.v_RefSpatial = pPoint.SpatialReference.Name
        ''        pForm1.Show()
        'MsgBox("X= " & pPoint.X & "   Y= " & pPoint.Y & "  Referencia Spatial= " & pPoint.SpatialReference.Name)
    End Sub

    Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'TODO: Add m_Form1.OnMouseMove implementation
    End Sub

    Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'TODO: Add m_Form1.OnMouseUp implementation
    End Sub

#Region "Use Custom Tool on Windows Form"
    ' ArcGIS Snippet Title:
    ' Use Custom Tool on Windows Form
    ' 
    ' Long Description:
    ' Connect a tool embedded in a Windows Form with the ArcGIS Application Framework.
    ' 
    ' Add the following references to the project:
    ' ESRI.ArcGIS.Framework
    ' ESRI.ArcGIS.System
    ' 
    ' Intended ArcGIS Products for this snippet:
    ' ArcGIS Desktop (ArcEditor, ArcInfo, ArcView)
    ' 
    ' Applicable ArcGIS Product Versions:
    ' 9.2
    ' 9.3
    ' 
    ' Required ArcGIS Extensions:
    ' (NONE)
    ' 
    ' Notes:
    ' This snippet is intended to be inserted at the base level of a Class.
    ' It is not intended to be nested within an existing Function or Sub.
    ' 

    '''<summary>Connect a tool embedded in a Windows Form with the ArcGIS Application Framework.</summary>
    ''' 
    '''<param name="application">An IApplication interface.</param>
    '''<param name="uidValue">A System.String that is where your 'Application_Root_Namespace'  +  '.'  +  'Your_COM_Class_Tool'. The 'Application_Root_Namespace' is obtained in Visual Studio by clicking Project menu. Ex: "MyArcGISApplication.MyCustomTool"</param>
    ''' 
    '''<remarks>
    '''This snippet assumes you have a Windows Form that is launched as a result of a BaseCommand click in ArcGIS Desktop.
    '''See the walkthrough document ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ESRI.EDNv9.2/NET_Desktop/01c01659-cdf8-4579-9c87-2b965e872d84.htm for an example of how to create and use a BaseCommand. 
    '''On the Windows Form you have a Windows Button, that when you click will execute the functionality of another custom COM Class that Inherits ESRI.ArcGIS.ADF.BaseClasses.BaseTool.
    '''This snippet would be inserted the Windows Form code behind file (.vb or .cs) and called as a result of the Windows Button click event.
    '''</remarks>
    Public Sub UseCustomToolOnWindowsForm(ByVal application As ESRI.ArcGIS.Framework.IApplication, ByVal uidValue As System.String)
        Dim UIDCls As ESRI.ArcGIS.esriSystem.IUID = New ESRI.ArcGIS.esriSystem.UIDClass
        UIDCls.Value = uidValue
        Dim commandItem As ESRI.ArcGIS.Framework.ICommandItem = application.Document.CommandBars.Find(UIDCls)
        If commandItem Is Nothing Then
            Exit Sub
        End If
        application.CurrentTool = commandItem
    End Sub

#Region "Get Command on Toolbar by Names"
    ' ArcGIS Snippet Title:
    ' Get Command on Toolbar by Names
    ' 
    ' Long Description:
    ' Find a command item particularly on a toolbar.
    ' 
    ' Add the following references to the project:
    ' ESRI.ArcGIS.Framework
    ' ESRI.ArcGIS.System
    ' 
    ' Intended ArcGIS Products for this snippet:
    ' ArcGIS Desktop (ArcEditor, ArcInfo, ArcView)
    ' 
    ' Applicable ArcGIS Product Versions:
    ' 9.2
    ' 9.3
    ' 
    ' Required ArcGIS Extensions:
    ' (NONE)
    ' 
    ' Notes:
    ' This snippet is intended to be inserted at the base level of a Class.
    ' It is not intended to be nested within an existing Function or Sub.
    ' 

    '''<summary>Find a command item particularly on a toolbar.</summary>
    '''  
    '''<param name="application">An IApplication interface.</param>
    '''<param name="toolbarName">A System.String that is the name of the toolbar to return. Example: "esriArcMapUI.StandardToolBar"</param>
    '''<param name="commandName">A System.String that is the name of the command to return. Example: "esriFramework.HelpContentsCommand"</param>
    '''   
    '''<returns>An ICommandItem interface.</returns>
    '''  
    '''<remarks>Refer to the EDN document http://edndoc.esri.com/arcobjects/9.1/default.asp?URL=/arcobjects/9.1/ArcGISDevHelp/TechnicalDocuments/Guids/ArcMapIds.htm for a listing of available CLSID's and ProgID's that can be used as the toolbarName and commandName parameters.</remarks>
    Public Function GetCommandOnToolbar(ByVal application As ESRI.ArcGIS.Framework.IApplication, ByVal toolbarName As System.String, ByVal commandName As System.String) As ESRI.ArcGIS.Framework.ICommandItem

        Dim commandBars As ESRI.ArcGIS.Framework.ICommandBars = application.Document.CommandBars
        Dim barID As ESRI.ArcGIS.esriSystem.UID = New ESRI.ArcGIS.esriSystem.UIDClass
        barID.Value = toolbarName ' Example: "esriArcMapUI.StandardToolBar"
        Dim barItem As ESRI.ArcGIS.Framework.ICommandItem = commandBars.Find(barID, False, False)

        If Not (barItem Is Nothing) AndAlso barItem.Type = ESRI.ArcGIS.Framework.esriCommandTypes.esriCmdTypeToolbar Then

            Dim commandBar As ESRI.ArcGIS.Framework.ICommandBar = CType(barItem, ESRI.ArcGIS.Framework.ICommandBar)
            Dim commandID As ESRI.ArcGIS.esriSystem.UID = New ESRI.ArcGIS.esriSystem.UIDClass
            commandID.Value = commandName ' Example: "esriArcMapUI.AddDataCommand"
            Return commandBar.Find(commandID, False)

        Else

            Return Nothing

        End If

    End Function
#End Region


#End Region


End Class

