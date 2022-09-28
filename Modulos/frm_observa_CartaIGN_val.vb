Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto
Imports PORTAL_Clases
Imports System.Drawing
Imports ESRI.ArcGIS.esriSystem
Imports Microsoft.Office.Interop
Imports System.Data
Imports System.IO
Imports System.Windows.Forms
Imports ESRI.ArcGIS.DataSourcesFile

Public Class frm_observa_CartaIGN_val

    Private dt As New DataTable
    Public pApp As IApplication

    Private cls_Catastro As New cls_DM_1

    Public m_application As IApplication
    Private Const Col_Sel_R As Integer = 0
    Private Const Col_Codigo As Integer = 1
    Private Const Col_CodEval As Integer = 2
    Private Const Col_Tipoinfo As Integer = 3
    Private Const Col_Indobs As Integer = 4
    Private Const Col_descri As Integer = 5
    Private lodtbLista_DM As New DataTable

    Private Property tipo_selec_catnomin As String
    
    Private Sub PT_Estilo_Grilla_EVAL(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(Col_Sel_R).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Codigo).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_CodEval).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Tipoinfo).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Indobs).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_descri).DefaultValue = ""

    End Sub
    Private Sub PT_Cargar_Grilla_EVAL(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdDetalle.DataSource = dvwDetalle
        Me.dgdDetalle.Columns(Col_Sel_R).Value = True
    End Sub

    Private Sub PT_Agregar_Funciones_EVAL()
        Me.dgdDetalle.Columns(Col_Sel_R).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Codigo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_CodEval).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Tipoinfo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Indobs).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_descri).DefaultValue = ""

    End Sub

    Private Sub PT_Forma_Grilla_EVAL()
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).Width = 30
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_CodEval).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Tipoinfo).Width = 70

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Indobs).Width = 70
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_descri).Width = 150
        
        'Me.dgdDetalle.Columns("SELEC").Caption = "SELEC"
        'Me.dgdDetalle.Columns("CG_CODIGO").Caption = "CODIGO"
        'Me.dgdDetalle.Columns("CG_CODEVA").Caption = "COD_EVA"
        'Me.dgdDetalle.Columns("EG_FORMAT").Caption = "INFORME"
        'Me.dgdDetalle.Columns("IE_CODIGO").Caption = "INDICADOR"
        'Me.dgdDetalle.Columns("EG_DESCRI").Caption = "DESCRIPCION"


        'Me.dgdDetalle.Columns("SELEC").Caption = "SELEC"
        'Me.dgdDetalle.Columns("CG_CODIGO").Caption = "CODIGO"
        'Me.dgdDetalle.Columns("CG_CODEVA").Caption = "COD_EVA"
        'Me.dgdDetalle.Columns("EG_FORMAT").Caption = "INFORME"
        'Me.dgdDetalle.Columns("ET_INDICA").Caption = "INDICADOR"
        'Me.dgdDetalle.Columns("EG_DESCRI").Caption = "DESCRIPCION"

        Me.dgdDetalle.Columns("CG_CODIGO").Caption = "CODIGO"
        Me.dgdDetalle.Columns("CG_CODEVA").Caption = "COD_EVA"
        Me.dgdDetalle.Columns("ET_FORMAT").Caption = "INFORME"
        Me.dgdDetalle.Columns("ET_INDICA").Caption = "INDICADOR"
        Me.dgdDetalle.Columns("ET_DESCRI").Caption = "DESCRIPCION"


        Me.dgdDetalle.Splits(0).HeadingStyle.ForeColor = Color.Blue
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_CodEval).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Tipoinfo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Indobs).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_descri).Locked = True

     
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_CodEval).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Tipoinfo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Indobs).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_descri).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_CodEval).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Tipoinfo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Indobs).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_descri).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify

    End Sub

    Public Sub Pinta_Grilla_Dm()
        Me.dgdDetalle.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdDetalle.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        Me.dgdDetalle.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        Me.dgdDetalle.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
    End Sub
    Public Sub PT_Inicializar_Grilla()

        'lodtbLista_DM.Columns.Add("SELEC", GetType(String))
        'lodtbLista_DM.Columns.Add("CG_CODIGO", GetType(String))
        'lodtbLista_DM.Columns.Add("CG_CODEVA", GetType(String))
        'lodtbLista_DM.Columns.Add("EG_FORMAT", GetType(String))
        'lodtbLista_DM.Columns.Add("IE_CODIGO", GetType(String))
        'lodtbLista_DM.Columns.Add("EG_DESCRI", GetType(String))

        lodtbLista_DM.Columns.Add("SELEC", GetType(String))
        lodtbLista_DM.Columns.Add("CG_CODIGO", GetType(String))
        lodtbLista_DM.Columns.Add("CG_CODEVA", GetType(String))
        lodtbLista_DM.Columns.Add("ET_FORMAT", GetType(String))
        lodtbLista_DM.Columns.Add("ET_INDICA", GetType(String))
        lodtbLista_DM.Columns.Add("ET_DESCRI", GetType(String))


    End Sub



    Private Sub frm_observa_CartaIGN_val_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Pinta_Grilla_Dm()
        PT_Inicializar_Grilla()

        Try
            conta_q = 0
            cls_Catastro.Carga_Observaciones_IGN_otros(m_application, Me.dgdDetalle, Me.lodtbLista_DM)

            Me.dgdDetalle.DataSource = lodtbLista_DM
            PT_Estilo_Grilla_EVAL(lodtbLista_DM) : PT_Cargar_Grilla_EVAL(lodtbLista_DM)
            PT_Agregar_Funciones_EVAL() : PT_Forma_Grilla_EVAL()

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

    Private Sub dgdDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgdDetalle.Click

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

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActualizar.Click
        'Esta parte desactiva lo que el usuario desmarca de lo existen en la bd
        Dim vindica As String = "", vformato As String = "", vCodigou As String = ""

        For w As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
            'Select Case Me.dgdDetalle.Item(w, "IE_CODIGO")  'PARA SG_D_CARAC_EVALGIS
            Select Case Me.dgdDetalle.Item(w, "ET_INDICA")  'PARA SG_D_CARAC_EVALGIS
                ' Case "GT", "GP", "MT", "MP", "CS", "CF", "BT", "RT", "RP", "HD", "CL", "LG", "RS", "FE", "TE", "RQ", "TL", "FR", "AU"
                Case "GT", "GP", "MT", "MP", "CS", "CF", "BT", "RT", "RP", "RH", "CL", "LG", "RS", "FE", "TE", "RQ", "TL", "FR", "AU"
                    If dgdDetalle.Item(w, "SELEC") = True Then
                        'vindica = Me.dgdDetalle.Item(w, "IE_CODIGO")
                        vindica = Me.dgdDetalle.Item(w, "ET_INDICA")
                        '   colecciones_obs_update.Add(vindica)
                    ElseIf dgdDetalle.Item(w, "SELEC") = False Then
                        '  vindica = Me.dgdDetalle.Item(w, "IE_CODIGO")
                        vindica = Me.dgdDetalle.Item(w, "ET_INDICA")
                        colecciones_obs_upd_borrar.Add(vindica)
                    End If

            End Select
        Next
        Me.Close()


        If pMap.LayerCount = 0 Then
            MsgBox("No Existe las capas en Carta IGN para generar observación Carta IGN...", MsgBoxStyle.Information, "BDGEOCATMN")
            Exit Sub
        End If

        Dim cls_planos As New Cls_planos

        colecciones_obs.Clear()
        cls_planos.Genera_ObservacionesCarta()

    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()

    End Sub
End Class