Imports System.Drawing
Imports PORTAL_Clases
Imports ESRI.ArcGIS.Framework
'Imports ESRI.ArcGIS.GISClient
Imports ESRI.ArcGIS.Display
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Output
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.CartoUI
Imports ESRI.ArcGIS.GeoDatabaseUI
Imports stdole

Public Class frm_grupo_pesiev
    Public m_application As IApplication
    'Public papp As IApplication
    ' Simultaneo ced
    Private Const Col_flg_sel = 0
    Private Const Col_grupo = 1
    Private Const Col_num_dm = 2
    Private Const Col_zonac = 3
    Private clsOracle As New cls_Oracle
    Private cls_Catastro As New cls_DM_1
    Private cls_eval As New Cls_evaluacion


    Public Sub PT_Inicializar_Grilla_Simultaneoc()
        Dim v_gpesiev As New DataTable
        v_gpesiev.Columns.Add("FLG_SEL", GetType(String))
        v_gpesiev.Columns.Add("GRUPO", GetType(String))
        v_gpesiev.Columns.Add("NUM_DM", GetType(String))
        v_gpesiev.Columns.Add("ZONAC", GetType(String))
        PT_Estilo_Grilla_Simultaneoc(v_gpesiev) : PT_Cargar_Grilla_Simultaneoc(v_gpesiev)
        PT_Agregar_Funciones_Simultaneoc() : PT_Forma_Grilla_Simultaneoc()
    End Sub

    Private Sub PT_Estilo_Grilla_Simultaneoc(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(Col_flg_sel).DefaultValue = 0
        padtbDetalle.Columns.Item(Col_grupo).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_num_dm).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_zonac).DefaultValue = ""
    End Sub

    Private Sub PT_Cargar_Grilla_Simultaneoc(ByVal padtbDetalle As DataTable)
        Dim v_gpesiev As New DataView(padtbDetalle)
        Me.dgdDetalle.DataSource = v_gpesiev
        Me.dgdDetalle.Columns(Col_flg_sel).Value = False
        'Pinta_Grilla_Ubigeo()
    End Sub

    Private Sub PT_Agregar_Funciones_Simultaneoc()
        Me.dgdDetalle.Columns(Col_flg_sel).DefaultValue = 0
        Me.dgdDetalle.Columns(Col_grupo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_num_dm).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_zonac).DefaultValue = ""
    End Sub

    Private Sub PT_Forma_Grilla_Simultaneoc()
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_flg_sel).Width = 20
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_grupo).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_num_dm).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zonac).Width = 50

        Me.dgdDetalle.Columns("FLG_SEL").Caption = "Sel."
        Me.dgdDetalle.Columns("GRUPO").Caption = "Grupo"
        Me.dgdDetalle.Columns("NUM_DM").Caption = "Número_DM"
        Me.dgdDetalle.Columns("ZONAC").Caption = "Zona"

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_flg_sel).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_grupo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_num_dm).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zonac).Locked = True

        Me.dgdDetalle.Splits(0).HeadingStyle.ForeColor = Color.Black
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_flg_sel).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_grupo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_num_dm).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zonac).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_flg_sel).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_grupo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_num_dm).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zonac).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
    End Sub

    Private Sub frm_grupo_pesiev_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim loCodigo As String = ""
        'If Val(dt_dmsi.Rows.Count) <> "0" Then
        '    For w As Integer = 0 To dt_dmsi.Rows.Count - 1
        '        loCodigo = loCodigo & "" & dt_dmsi.Rows(w).Item("codigo") & "_"
        '    Next
        '    loCodigo = Mid(loCodigo, 1, Len(loCodigo) - 1) & "_" & v_codigo & ""
        '    loCodigosim = loCodigo
        '    loCodigo = ""
        '    ''''''''''''''''''''''''''''''''''''CAPTURA CODIGOU DE UN DATATABLE  '''''''''''''''''''''''
        '    'Dim lodtbEval As New DataTable
        '    'Dim dv As New DataView
        '    'Dim num_dmsim As String
        '    'Dim loCodigo As String = ""
        '    'dv = lodtbReporte.DefaultView
        '    'dv.RowFilter = "EVAL = 'SI'"
        '    'lodtbEval = dv.ToTable
        '    'num_dmsim = lodtbEval.Rows.Count
        '    'If Val(num_dmsim) <> "0" Then
        '    '    For w As Integer = 0 To lodtbEval.Rows.Count - 1
        '    '        loCodigo = loCodigo & "" & lodtbEval.Rows(w).Item("codigou") & "_"
        '    '    Next
        '    '    loCodigo = Mid(loCodigo, 1, Len(loCodigo) - 1) & "_" & v_codigo & ""
        '    '    loCodigosim = loCodigo
        '    'End If
        '    'loCodigo = ""
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Dim cls_Catastro As New cls_DM_1
        Dim cls_catastro As New cls_DM_1
        Dim cls_Eval As New Cls_evaluacion
        Dim cls_prueba As New cls_Prueba
        Dim cls_planos As New Cls_planos
        Dim v_nupesiev As String
        Dim num As Integer = 0
        Dim vnum_dm As String = ""
        Dim grusim As String = ""
        Dim grusimf As String = ""
        Dim v_pesiev As New DataTable
        Dim v_cupesiev As New DataTable
        Dim v_gpesiev As New DataTable
        Dim datum_origen As String = ""
        If Val(loCodigosim) <> "0" Then
            datum_origen = v_sistema
            v_nupesiev = clsOracle.FT_pesiev(loCodigosim)

            'NUEVO
            v_cupesiev = clsOracle.FT_sc_d_cupesiev(datum_origen)       ' cuadriculas simultaneas de los DM Simultaneos

            ''''''

            'v_pesiev = clsOracle.FT_sc_d_pesiev()           'Consolidado de grupos y subgrupos
            'v_pesiev = clsOracle.FT_sc_d_pesiev(datum_origen)           'Consolidado de grupos y subgrupos

            v_gpesiev = clsOracle.FT_sc_d_pesiev(datum_origen)           'Consolidado de grupos y subgrupos   ---> LISTADO DEL DATAGRID

            'v_gpesiev = clsOracle.FT_consulta_sc_d_pesiev()     'listado del Datagrid
            'v_gpesiev = clsOracle.FT_consulta_sc_d_pesiev(v_codigo)     'listado del Datagrid
            dgdDetalle.DataSource = v_gpesiev
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'PT_Inicializar_Grilla_Simultaneoc()     ' ojo   esto debe estar comentado
            PT_Agregar_Funciones_Simultaneoc()
            Me.chkEstado.Checked = False : Me.chkEstado.Checked = True
            Me.dgdDetalle.AllowUpdate = True
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'chkEstado_CheckedChanged(Nothing,Nothing)
        End If

        'Dim cls_planos As New Cls_planos
        'Cls_planos.generaplanosimultaneoev(m_application)
        'cls_catastro.Consulta_DM_Simultaneocev(m_application, Me.dgdDetalle, "")

    End Sub

    Private Sub chkEstado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEstado.CheckedChanged
        Dim items As C1.Win.C1TrueDBGrid.ValueItems = Me.dgdDetalle.Columns("FLG_SEL").ValueItems
        If Me.chkEstado.Checked Then
            ' we're going to translate values - the datasource needs to hold at least 3 states
            items.Translate = True
            ' each click will cycle thru the various checkbox states
            items.CycleOnClick = True
            ' display the cell as a checkbox
            items.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox
            ' now associate underlying db values with the checked state
            items.Values.Clear()
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("0", False)) ' unchecked
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("1", True)) ' checked
            '  items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("2", "INDETERMINATE")) ' indeterminate state
        Else
            items.Translate = False
            items.CycleOnClick = False
            items.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.Normal
        End If
    End Sub

    Private Sub btn_Graficar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Graficar.Click
        Dim cls_planos As New Cls_planos
        Dim loBoo_flg As Boolean = False
        op_sim = 1
        For w As Integer = 0 To Me.dgdDetalle.RowCount - 1
            If Me.dgdDetalle.Item(w, "FLG_SEL") Then
                loBoo_flg = True
            End If
        Next
        If loBoo_flg = False Then
            MsgBox("Seleccione un Item de la Lista", MsgBoxStyle.Information, "[BDGeocatmin]")
            Exit Sub
        End If
        Me.Hide() : Dim RetVal
        'btnGraficar.Cursor = System.Windows.Forms.Cursors.AppStarting

        cls_Catastro.Consulta_DM_Simultaneocev(m_application, Me.dgdDetalle, txtExiste)
        'If Val(num_cuasim) = "1" Then
        '    cls_Catastro.ShowLabel_DM3("Cuadricula_sim", m_application)
        'Else
        '    cls_Catastro.ShowLabel_DM3("Cuadricula_dsim", m_application)
        'End If
        cls_Catastro.rotulatexto_dm("Catastro_sim", m_application)
        If Val(num_cuasim) = "1" Then
            cls_Catastro.rotulatexto_dm("Cuadricula_sim", m_application)
        Else
            cls_Catastro.rotulatexto_dm("Cuadricula_dsim", m_application)
        End If

        cls_planos.generaplanosimultaneoev(m_application)

        Me.Show()

        'Dim lostrRetorno As String = cls_Oracle.FT_Registro(1, lostrOpcioncbo)
        cls_eval.cierra_ejecutable()
        op_sim = 0
        esc_sim = ""
        'Abre los botones de las herramientas
        'BOTON_MENU(True)
        'btnGraficar.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
    End Sub

    Private Sub btn_cerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cerrar.Click
        Me.Close()
    End Sub

End Class