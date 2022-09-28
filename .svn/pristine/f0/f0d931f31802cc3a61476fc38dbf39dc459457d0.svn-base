Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto
Imports PORTAL_Clases
Imports System.Drawing
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.DataSourcesFile
Imports System.Data
Imports System.IO
Imports System.Collections
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.DataSourcesOleDB
Imports ESRI.ArcGIS.GISClient
Imports ESRI.ArcGIS.Display
Imports System.Runtime.InteropServices
Imports stdole
Imports Oracle.DataAccess.Client


'Imports ESRI.ArcGIS.Output
'Imports ESRI.ArcGIS.Editor
'Imports ESRI.ArcGIS.Catalog
'Imports ESRI.ArcGIS.CartoUI
'Imports ESRI.ArcGIS.GeoDatabaseUI


Public Class Frm_Eval_LibreDenu
    Public pApp As IApplication
    Public pEste As Double
    Public pNorte As Double
    Private lodtbUbigeo As New DataTable
    Private cls_Oracle As New cls_Oracle
    Private Const Col_Sel_R = 0
    Private Const Col_conta = 1
    Private Const Col_Codigo = 2
    Private Const Col_Niv_Sup = 3
    Private Const Col_cod_Urba = 4
    Private Const Col_Cod_Rese = 5
    Private Const Col_Cod_Eval = 6
    Private Const Col_Zona_urb = 7
    Private Const Col_Exp_urb = 8
    Private Const Col_Nucleo = 9
    Private Const Col_Amort = 10
    Private Const Col_Sin_N = 11
    Private Const Col_Anap = 12
    Private Const Col_Zona_a = 13
    Private Const Col_Proy_e = 14
    Private Const Col_Otra_R = 15
    Private Const Col_Puerto = 16
    Private Const Col_Rese_t = 17
    Private Const Col_Otro = 18
    Private Const Col_traslape = 19
    Private Const Col_frontera = 20
    Private Const Col_urba_af = 21
    'Private Const Col_dm = 22
    Private Const Col_prot = 22
    Private Const Col_fpais = 23
    Private Const Col_dm_Pr = 24
    Private Const Col_dm_Po = 25
    Private Const Col_dm_si = 26
    Private Const Col_dm_ex = 27
    Private Const Col_dm_rd = 28

    Private cls_Catastro As New cls_DM_1
    Private cls_Prueba As New cls_Prueba
    Private cls_DM_2 As New cls_DM_2

    Public m_Application As IApplication
    Private lodtbLista_DM As New DataTable

    Private Sub Frm_Eval_LibreDenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'If v_usuario.ToUpper = "PAST0960" Then
        If gstrCodigo_Usuario = "PAST0960" Then
            Me.btnGrabar.Enabled = False
            Me.btnGrabar.Visible = False
        End If
        Pinta_Grilla_Dm()

        PT_Inicializar_Grilla_libredenu()

        Try
            conta_q = 0
            ' arcpy.overwriteOutput = True
            cls_Catastro.Actualiza_carga_lihredenu(m_Application, Me.dgdDetalle, Me.lodtbLista_DM)

            Me.dgdDetalle.DataSource = lodtbLista_DM
            Me.txtarea.Text = v_area_eval

            Me.txtCodigo.Text = v_codigo
            Me.txtNombre.Text = v_nombre

            ' Me.Txtfecha.Text = v_tipoex
            Me.txtestado.Text = v_estado
            ' Me.Txtfecha.Text = "01/06/2012"   'fecha simulada
            PT_Estilo_Grilla(lodtbLista_DM) : PT_Cargar_Grilla_libredenu(lodtbLista_DM)
            PT_Agregar_Funciones_libredenu() : PT_Forma_Grilla_libredenu()

            Me.chkEstado.Checked = False : Me.chkEstado.Checked = True
            For i As Integer = 0 To Me.dgdDetalle.RowCount - 1
                dgdDetalle.Item(i, "Selec") = True
            Next
            Me.dgdDetalle.AllowUpdate = True

            dgdDetalle.Focus()
            pMxDoc.ActiveView.Refresh()

        Catch ex As Exception
            MsgBox("No ha terminado el proceso de cargar correctamente", vbCritical, "Observacion  ")
        End Try

    End Sub


    Public Sub PT_Inicializar_Grilla_libredenu()

        ' Dim lodtbLista_DM As New DataTable
        lodtbLista_DM.Columns.Add("Selec", GetType(String))
        lodtbLista_DM.Columns.Add("Contador", GetType(String))
        lodtbLista_DM.Columns.Add("Codigo", GetType(String))
        lodtbLista_DM.Columns.Add("Niv_Sup", GetType(String))
        lodtbLista_DM.Columns.Add("Cod_Urba", GetType(String))
        lodtbLista_DM.Columns.Add("Cod_Rese", GetType(String))
        lodtbLista_DM.Columns.Add("Cod_Eval", GetType(String))
        lodtbLista_DM.Columns.Add("Zona_Urb", GetType(String))
        lodtbLista_DM.Columns.Add("Exp_urb", GetType(String))
        lodtbLista_DM.Columns.Add("Nucleo", GetType(String))
        lodtbLista_DM.Columns.Add("Amort", GetType(String))
        lodtbLista_DM.Columns.Add("Sin_N", GetType(String))
        lodtbLista_DM.Columns.Add("Anap", GetType(String))
        lodtbLista_DM.Columns.Add("Zona_A", GetType(String))
        lodtbLista_DM.Columns.Add("Proy_E", GetType(String))
        lodtbLista_DM.Columns.Add("Otra_R", GetType(String))
        lodtbLista_DM.Columns.Add("Puerto", GetType(String))
        lodtbLista_DM.Columns.Add("Rese_T", GetType(String))
        lodtbLista_DM.Columns.Add("Otro", GetType(String))
        lodtbLista_DM.Columns.Add("Traslape", GetType(String))
        lodtbLista_DM.Columns.Add("Frontera", GetType(String))
        lodtbLista_DM.Columns.Add("Urba_Af", GetType(String))
        'lodtbLista_DM.Columns.Add("DM", GetType(String))
        lodtbLista_DM.Columns.Add("Prot", GetType(String))
        lodtbLista_DM.Columns.Add("Fpais", GetType(String))
        lodtbLista_DM.Columns.Add("PR", GetType(String))
        lodtbLista_DM.Columns.Add("PO", GetType(String))
        lodtbLista_DM.Columns.Add("SI", GetType(String))
        lodtbLista_DM.Columns.Add("EX", GetType(String))
        lodtbLista_DM.Columns.Add("RD", GetType(String))


    End Sub



    Private Sub PT_Cargar_Grilla_libredenu(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdDetalle.DataSource = dvwDetalle
        Me.dgdDetalle.Columns(Col_Sel_R).Value = True

    End Sub


    Sub PT_Estilo_Grilla(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(Col_Sel_R).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_conta).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Codigo).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Niv_Sup).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_cod_Urba).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Cod_Rese).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Cod_Eval).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Zona_urb).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Exp_urb).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Nucleo).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Amort).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Sin_N).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Anap).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Zona_a).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Proy_e).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Otra_R).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Puerto).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Rese_t).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Otro).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_traslape).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_frontera).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_urba_af).DefaultValue = ""
        'padtbDetalle.Columns.Item(Col_dm).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_prot).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_fpais).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_dm_Pr).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_dm_Po).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_dm_si).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_dm_ex).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_dm_rd).DefaultValue = ""

    End Sub
    Public Sub Pinta_Grilla_Dm()
        Me.dgdDetalle.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdDetalle.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        Me.dgdDetalle.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        Me.dgdDetalle.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
    End Sub
    Private Sub chkEstado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEstado.CheckedChanged
        Dim items As C1.Win.C1TrueDBGrid.ValueItems = Me.dgdDetalle.Columns("Selec").ValueItems
        If Me.chkEstado.Checked Then
            items.Translate = True
            items.CycleOnClick = True
            items.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("0", False)) ' unchecked
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("1", True)) ' checked
        Else
            items.Translate = True
            items.CycleOnClick = True
            items.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("0", False)) ' unchecked
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("1", True)) ' checked
        End If
    End Sub

    Private Sub PT_Agregar_Funciones_libredenu()
        Me.dgdDetalle.Columns(Col_Sel_R).DefaultValue = 0
        Me.dgdDetalle.Columns(Col_conta).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Codigo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Niv_Sup).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_cod_Urba).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Cod_Rese).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Cod_Eval).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Zona_urb).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Exp_urb).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Nucleo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Amort).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Sin_N).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Anap).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Zona_a).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Proy_e).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Otra_R).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Puerto).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Rese_t).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Otro).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_traslape).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_frontera).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_urba_af).DefaultValue = ""
        'Me.dgdDetalle.Columns(Col_dm).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_prot).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_fpais).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_dm_Pr).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_dm_Po).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_dm_si).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_dm_ex).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_dm_rd).DefaultValue = ""


    End Sub
    Private Sub PT_Forma_Grilla_libredenu()
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).Width = 40
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).Width = 50
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Width = 70
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Niv_Sup).Width = 100
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_cod_Urba).Width = 60
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cod_Rese).Width = 60
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cod_Eval).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Zona_urb).Width = 120
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Exp_urb).Width = 120
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nucleo).Width = 120
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Amort).Width = 120
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sin_N).Width = 120
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Anap).Width = 120
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Zona_a).Width = 120
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Proy_e).Width = 120
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Otra_R).Width = 120
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Puerto).Width = 120
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Rese_t).Width = 120
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Otro).Width = 120
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_traslape).Width = 50
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_frontera).Width = 50
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_urba_af).Width = 50
        'Me.dgdDetalle.Splits(0).DisplayColumns(Col_dm).Width = 50
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_prot).Width = 50
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_fpais).Width = 50
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_dm_Pr).Width = 30
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_dm_Po).Width = 30
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_dm_si).Width = 30
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_dm_ex).Width = 30
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_dm_rd).Width = 30

        Me.dgdDetalle.Columns("Selec").Caption = "Sel."
        Me.dgdDetalle.Columns("Contador").Caption = "Contador"
        Me.dgdDetalle.Columns("Codigo").Caption = "Código"
        Me.dgdDetalle.Columns("Niv_Sup").Caption = "Niv_Sup"
        Me.dgdDetalle.Columns("Cod_Urba").Caption = "Cod_Urba"
        Me.dgdDetalle.Columns("Cod_Rese").Caption = "Cod_Rese"
        Me.dgdDetalle.Columns("Cod_Eval").Caption = "Cod_Eval"
        Me.dgdDetalle.Columns("Zona_Urb").Caption = "Zona_Urb"
        Me.dgdDetalle.Columns("Exp_Urb").Caption = "Exp_Urb"
        Me.dgdDetalle.Columns("Nucleo").Caption = "Nucleo"
        Me.dgdDetalle.Columns("Amort").Caption = "Amort"
        Me.dgdDetalle.Columns("Sin_N").Caption = "Sin_Clase"
        Me.dgdDetalle.Columns("Anap").Caption = "Anap"
        Me.dgdDetalle.Columns("Zona_A").Caption = "Zona_Arq"
        Me.dgdDetalle.Columns("Proy_E").Caption = "Proy_Esp"
        Me.dgdDetalle.Columns("Otra_R").Caption = "Otra_Ar"
        Me.dgdDetalle.Columns("Puerto").Caption = "Puerto"
        Me.dgdDetalle.Columns("Rese_T").Caption = "Rese_t"
        Me.dgdDetalle.Columns("Otro").Caption = "Otro"
        Me.dgdDetalle.Columns("Traslape").Caption = "Traslape"
        Me.dgdDetalle.Columns("Frontera").Caption = "Frontera"
        Me.dgdDetalle.Columns("Urba_Af").Caption = "Urba_Af"
        ' Me.dgdDetalle.Columns("Dm").Caption = "DM"
        Me.dgdDetalle.Columns("Prot").Caption = "Prot"
        Me.dgdDetalle.Columns("Fpais").Caption = "Pais"
        Me.dgdDetalle.Columns("PR").Caption = "PR"
        Me.dgdDetalle.Columns("PO").Caption = "PO"
        Me.dgdDetalle.Columns("SI").Caption = "SI"
        Me.dgdDetalle.Columns("EX").Caption = "Ex"
        Me.dgdDetalle.Columns("RD").Caption = "RD"


        Me.dgdDetalle.Splits(0).HeadingStyle.ForeColor = Color.Red
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Niv_Sup).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_cod_Urba).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cod_Rese).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cod_Eval).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Zona_urb).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Exp_urb).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nucleo).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Amort).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sin_N).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Anap).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Zona_a).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Proy_e).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Otra_R).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Puerto).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Rese_t).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Otro).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_traslape).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_frontera).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_urba_af).Locked = False
        'Me.dgdDetalle.Splits(0).DisplayColumns(Col_dm).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_prot).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_fpais).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_dm_Pr).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_dm_Po).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_dm_si).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_dm_ex).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_dm_rd).Locked = False

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Niv_Sup).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_cod_Urba).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cod_Rese).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cod_Eval).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Zona_urb).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Exp_urb).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nucleo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Amort).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sin_N).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Anap).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Zona_a).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Proy_e).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Otra_R).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Puerto).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Rese_t).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Otro).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_traslape).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_frontera).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_urba_af).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        'Me.dgdDetalle.Splits(0).DisplayColumns(Col_dm).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_prot).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_fpais).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_dm_Pr).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_dm_Po).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_dm_si).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_dm_ex).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_dm_rd).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
    End Sub

    Private Sub PT_Cargar_Grilla_EVAL(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdDetalle.DataSource = dvwDetalle
        Me.dgdDetalle.Columns(Col_Sel_R).Value = True
    End Sub

    Private Sub PT_Estilo_Grilla_EVAL(ByRef lodtbLista_DM As DataTable)
        ''   lodtbLista_DM.Columns.Item(Col_Sel_R).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_conta).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_Codigo).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_Niv_Sup).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_Zona_urb).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_Exp_urb).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_Nucleo).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_Amort).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_Sin_N).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_Anap).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_Zona_a).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_Proy_e).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_Otra_R).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_Puerto).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_Rese_t).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_Otro).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_traslape).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_frontera).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_urba_af).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_dm).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_prot).DefaultValue = ""
        'lodtbLista_DM.Columns.Item(Col_fpais).DefaultValue = ""

    End Sub

    Private Function Col_Numero() As Integer
        Throw New NotImplementedException
    End Function

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActualizar.Click
        Try
            conta_q = 0
            cls_Catastro.Actualiza_tabla_lihredenu(m_Application, Me.dgdDetalle)
        Catch ex As Exception
            MsgBox("No ha terminado el proceso correctamente", vbCritical, "Observacion  ")
        End Try
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

        Try
            ' pubfec_libdenu = Me.Txtfecha.Text
            pubfec_libdenu = Format(dtpFecha1.Value)
            conta_q = 0

            cls_Catastro.Grabar_informacion_lihredenu(m_Application, Me.dgdDetalle)
        Catch ex As Exception
            MsgBox("No ha terminado el proceso correctamente", vbCritical, "Observacion  ")
        End Try


    End Sub

    Private Sub Txtfecha_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub dtpFecha1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha1.ValueChanged

    End Sub
End Class