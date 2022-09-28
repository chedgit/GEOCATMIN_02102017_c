'Imports OS
Imports PORTAL_Clases
Imports System.Drawing
Imports ESRI.ArcGIS.Framework
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem
Imports System.Data
Imports System.IO
Imports Microsoft.Office.Interop

Public Class frmCalculaARES
    'PROCESOS
    Private Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Long, ByVal bInheritHandle As Long, ByVal dwProcessId As Long) As Long
    Private Declare Function GetExitCodeProcess Lib "kernel32" (ByVal hProcess As Long, ByVal lpExitCode As Long) As Long
    Private dt As New DataTable
    'PAUSA
    Private Declare Function GetTickCount Lib "Kernel32.dll" () As Long
    Const STILL_ACTIVE = &H103
    Const PROCESS_QUERY_INFORMATION = &H400

    ''FUNCION DE SLEEP PARA ESPERAR UNOS MILISEGUNDOS
    Sub Pausa(ByVal HowLong As Long)
        Dim u, tick As Long
        tick = GetTickCount()
        Do
            Application.DoEvents()
        Loop Until tick + HowLong < GetTickCount
    End Sub

    Public m_application As IApplication
    Private cls_Oracle As New cls_Oracle
    Private lodtbReporte_Excel As New DataTable

    Private Sub frmCalculaARES_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.btnCargar.Enabled = False
        Dim ds As New DataSet
        Dim s1 As Decimal = 0
        Dim s2 As Decimal = 0
        Try
            ds.ReadXml("c:\bdgeocatmin\Temporal\arestringida\arestringida.xml")
            dgdDetalle1.DataSource = ds.Tables(0)
            dgdDetalle.DataSource = ds.Tables(0)
            dgdDetalle.Columns(1).Visible = False
            dgdDetalle.Columns(2).Visible = False
            dgdDetalle.Columns(3).Visible = False
            dgdDetalle.Columns(4).Visible = False
            dgdDetalle.Columns(5).Visible = False
            dt = ds.Tables(0)
            fn_Grilla(dgdDetalle1, dt)
            PT_Agregar_Funciones_Detalle_02()
            PT_Forma_Grilla_Detalle_02()
            cboRegion.Items.Add("TOTAL PERÚ")
            For r As Integer = 0 To dt.Rows.Count - 1
                Select Case dt.Rows(r).Item(0)
                    Case "MAR", "FUERA DEL PERU"
                    Case Else
                        s1 = s1 + dt.Rows(r).Item("DEPA_HAS")
                        s2 = s2 + dt.Rows(r).Item("ARES_HAS")
                End Select
                cboRegion.Items.Add(dt.Rows(r).Item("NM_DEPA"))
            Next
            txtArea1.Text = Format(s1, "###,###,###,###.##")
            txtArea2.Text = Format(s2, "###,###,###,###.##")
            txtPorcentaje.Text = Format((s2 / s1) * 100, "###,###,###,###.##")
            cboRegion.SelectedIndex = 0
            logloFlagGrilla = 1
                    Catch ex As Exception
        End Try
    End Sub
    
    Public Sub cargar_grilla()
        Dim lodtbDatos As New DataTable
        Pinta_Grilla()
        Try
            Application.DoEvents()
            'lodtbReporte_Excel = cls_Oracle.FT_Calcula_ARES()
            'glo_PathGDB = "C:\BDGEOCATMIN\Temporal\ARESTRINGIDA\"
            'p_Table = "ares_dep_d.shp"
            'p_Filtro = "NM_DEPA = 'LIMA'"
            'Contador_Table(,  "")
            'dgdDetalle.DataSource = Cargar_Grilla_shp("C:\BDGEOCATMIN\Temporal\ARESTRINGIDA\", "ares_dep_d")
            'lodtbReporte_Excel = lodtbDatos
            'dgdDetalle.DataSource = lodtbReporte_Excel
            'fn_Grilla(dgdDetalle, Cargar_Grilla_shp("C:\BDGEOCATMIN\Temporal\ARESTRINGIDA\", "ares_dep_d"))
            fn_Grilla(dgdDetalle1, Cargar_Grilla_shp("C:\BDGEOCATMIN\Temporal\ARESTRINGIDA\", "aresdep_id"))
            PT_Agregar_Funciones_Detalle_02()
            PT_Forma_Grilla_Detalle_02()
            Dim dt As New DataTable
            dt = dgdDetalle1.DataSource
            Dim glodstControl1 As New DataSet
            glodstControl1.Tables.Add(dt.Copy)
            'glodstControl1.WriteXml("c:\TEMP\control.xml")
            glodstControl1.WriteXml("c:\bdgeocatmin\Temporal\arestringida\arestringida.xml")
        Catch ex As Exception
            MsgBox("Error", MsgBoxStyle.Information, "Aviso")
        End Try
    End Sub

    Private Function fn_Grilla(ByVal p_DataGridView As DataGridView, ByVal p_lodbtTabla As DataTable) As DataGridView
        With p_DataGridView
            .DataSource = p_lodbtTabla
            .AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue
            '.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .AutoResizeColumns() ' ------->>> ESTA ES LA INSTRUCCION QUE NECESITAS..
            .DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        Return p_DataGridView
    End Function

    Private Sub PT_Agregar_Funciones_Detalle_02()
        Try
            dgdDetalle1.Columns("nm_depa").HeaderText = "Depa_Nombre"
            dgdDetalle1.Columns("depa_has").HeaderText = "Depa_Area (Ha)"
            dgdDetalle1.Columns("ares_has").HeaderText = "ARAM (Ha)"
            dgdDetalle1.Columns("por_ares_i").HeaderText = "% Area Ocupada"
            dgdDetalle1.Columns("por_libre").HeaderText = "% Area Libre"
            dgdDetalle1.Columns("cantidad").HeaderText = "ARAM (unidades)"
            'dgdDetalle1.Columns("col_Anap").HeaderText = "Anap"
            'dgdDetalle1.Columns("col_arna").HeaderText = "Area Natural"
            'dgdDetalle1.Columns("col_ctmi").HeaderText = "Concesion de Transporte Minero"
            'dgdDetalle1.Columns("col_oare").HeaderText = "Otra Area Restringida"
            'dgdDetalle1.Columns("col_pmar").HeaderText = "Productores Mineros Artesanales"
            'dgdDetalle1.Columns("col_pana").HeaderText = "Propuesta de Area Natural"
            'dgdDetalle1.Columns("col_pres").HeaderText = "Proyecto Especial"
            'dgdDetalle1.Columns("col_puae").HeaderText = "Puerto y/o Aeropuerto"
            'dgdDetalle1.Columns("col_zoar").HeaderText = "Zona Arqueologica"
            'dgdDetalle1.Columns("col_zour").HeaderText = "Zona Urbana"

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub PT_Agregar_Funciones_Detalle_03()
        Try
            dgdDetalle.Columns("nm_depa").HeaderText = "Depa_Nombre"
            dgdDetalle.Columns("depa_has").HeaderText = "Depa_Area (Ha)"
            dgdDetalle.Columns("ares_has").HeaderText = "ARAM (Ha)"
            dgdDetalle.Columns("por_ares_i").HeaderText = "% Area Ocupada"
            dgdDetalle.Columns("por_libre").HeaderText = "% Area Libre"
            dgdDetalle.Columns("cantidad").HeaderText = "ARAM (unidades)"
            dgdDetalle.Columns("col_Anap").HeaderText = "Anap"
            dgdDetalle.Columns("por_Anap").HeaderText = "% Anap"
            dgdDetalle.Columns("col_arna").HeaderText = "Área Natural"
            dgdDetalle.Columns("por_arna").HeaderText = "% Área Natural"
            dgdDetalle.Columns("col_ctmi").HeaderText = "Concesión de Transporte Minero"
            dgdDetalle.Columns("por_ctmi").HeaderText = "% Concesión de Transporte Minero"
            dgdDetalle.Columns("col_oare").HeaderText = "Otra Área Restringida"
            dgdDetalle.Columns("por_oare").HeaderText = "% Otra Área Restringida"
            dgdDetalle.Columns("col_pmar").HeaderText = "Productores Mineros Artesanales"
            dgdDetalle.Columns("por_pmar").HeaderText = "% Productores Mineros Artesanales"
            dgdDetalle.Columns("col_pana").HeaderText = "Propuesta de Área Natural"
            dgdDetalle.Columns("por_pana").HeaderText = "% Propuesta de Área Natural"
            dgdDetalle.Columns("col_pres").HeaderText = "Proyecto Especial"
            dgdDetalle.Columns("por_pres").HeaderText = "% Proyecto Especial"
            dgdDetalle.Columns("col_puae").HeaderText = "Puerto y/o Aeropuerto"
            dgdDetalle.Columns("por_puae").HeaderText = "% Puerto y/o Aeropuerto"
            dgdDetalle.Columns("col_zoar").HeaderText = "Zona Arqueológica"
            dgdDetalle.Columns("por_zoar").HeaderText = "% Zona Arqueológica"
            dgdDetalle.Columns("col_zour").HeaderText = "Zona Urbana"
            dgdDetalle.Columns("por_zour").HeaderText = "% Zona Urbana"

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub PT_Forma_Grilla_Detalle_02()
        dgdDetalle1.Columns("nm_depa").Width = 120
        dgdDetalle1.Columns("depa_has").Width = 110
        dgdDetalle1.Columns("ares_has").Width = 110
        dgdDetalle1.Columns("por_ares_i").Width = 100
        dgdDetalle1.Columns("por_libre").Width = 100
        dgdDetalle1.Columns("cantidad").Width = 70

        'dgdDetalle1.Columns("col_anap").Width = 100
        'dgdDetalle1.Columns("col_arna").Width = 100
        'dgdDetalle1.Columns("col_ctmi").Width = 100
        'dgdDetalle1.Columns("col_oare").Width = 100
        'dgdDetalle1.Columns("col_pmar").Width = 100
        'dgdDetalle1.Columns("col_pana").Width = 100
        'dgdDetalle1.Columns("col_pres").Width = 100
        'dgdDetalle1.Columns("col_puae").Width = 100
        'dgdDetalle1.Columns("col_zoar").Width = 100
        'dgdDetalle1.Columns("col_zour").Width = 100

        'dgdDetalle.Splits(0).HeadingStyle.ForeColor = Color.MidnightBlue

        'La alineación de las celdas de cada columna 
        dgdDetalle1.Columns("nm_depa").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        dgdDetalle1.Columns("depa_has").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle1.Columns("ares_has").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle1.Columns("por_ares_i").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle1.Columns("por_libre").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle1.Columns("cantidad").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        'dgdDetalle1.Columns("col_anap").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'dgdDetalle1.Columns("col_arna").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'dgdDetalle1.Columns("col_ctmi").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'dgdDetalle1.Columns("col_oare").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'dgdDetalle1.Columns("col_pmar").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'dgdDetalle1.Columns("col_pana").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'dgdDetalle1.Columns("col_pres").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'dgdDetalle1.Columns("col_puae").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'dgdDetalle1.Columns("col_zoar").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'dgdDetalle1.Columns("col_zour").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        'La alinación del encabezado de cada columna 
        dgdDetalle1.Columns("nm_depa").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle1.Columns("depa_has").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle1.Columns("ares_has").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle1.Columns("por_ares_i").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle1.Columns("por_libre").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle1.Columns("cantidad").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        'dgdDetalle1.Columns("col_anap").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'dgdDetalle1.Columns("col_arna").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'dgdDetalle1.Columns("col_ctmi").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'dgdDetalle1.Columns("col_oare").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'dgdDetalle1.Columns("col_pmar").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'dgdDetalle1.Columns("col_pana").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'dgdDetalle1.Columns("col_pres").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'dgdDetalle1.Columns("col_puae").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'dgdDetalle1.Columns("col_zoar").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'dgdDetalle1.Columns("col_zour").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        'El formato de las columnas numéricas 
        'DataGridView1.Columns("T_Llegada").DefaultCellStyle.Format = "##,##0.00"
        dgdDetalle1.Columns("depa_has").DefaultCellStyle.Format = "###,###,##0.00"
        dgdDetalle1.Columns("ares_has").DefaultCellStyle.Format = "###,###,##0.00"
        dgdDetalle1.Columns("por_ares_i").DefaultCellStyle.Format = "###"
        dgdDetalle1.Columns("por_libre").DefaultCellStyle.Format = "###"
        dgdDetalle1.Columns("cantidad").DefaultCellStyle.Format = "#,###"

        'dgdDetalle1.Columns("col_anap").DefaultCellStyle.Format = "###,##0.00"
        'dgdDetalle1.Columns("col_arna").DefaultCellStyle.Format = "###,##0.00"
        'dgdDetalle1.Columns("col_ctmi").DefaultCellStyle.Format = "###,##0.00"
        'dgdDetalle1.Columns("col_oare").DefaultCellStyle.Format = "###,##0.00"
        'dgdDetalle1.Columns("col_pmar").DefaultCellStyle.Format = "####,##0.00"
        'dgdDetalle1.Columns("col_pana").DefaultCellStyle.Format = "###,##0.00"
        'dgdDetalle1.Columns("col_pres").DefaultCellStyle.Format = "###,##0.00"
        'dgdDetalle1.Columns("col_puae").DefaultCellStyle.Format = "###,##0.00"
        'dgdDetalle1.Columns("col_zoar").DefaultCellStyle.Format = "###,##0.00"
        'dgdDetalle1.Columns("col_zour").DefaultCellStyle.Format = "###,##0.00"
    End Sub
    Private Sub PT_Forma_Grilla_Detalle_03()
        dgdDetalle.Columns("nm_depa").Width = 120
        dgdDetalle.Columns("depa_has").Width = 110
        dgdDetalle.Columns("ares_has").Width = 110
        dgdDetalle.Columns("por_ares_i").Width = 100
        dgdDetalle.Columns("por_libre").Width = 100
        dgdDetalle.Columns("cantidad").Width = 70
        dgdDetalle.Columns("col_anap").Width = 100
        dgdDetalle.Columns("por_anap").Width = 100
        dgdDetalle.Columns("col_arna").Width = 100
        dgdDetalle.Columns("por_arna").Width = 100
        dgdDetalle.Columns("col_ctmi").Width = 100
        dgdDetalle.Columns("por_ctmi").Width = 100
        dgdDetalle.Columns("col_oare").Width = 100
        dgdDetalle.Columns("por_oare").Width = 100
        dgdDetalle.Columns("col_pmar").Width = 100
        dgdDetalle.Columns("por_pmar").Width = 100
        dgdDetalle.Columns("col_pana").Width = 100
        dgdDetalle.Columns("por_pana").Width = 100
        dgdDetalle.Columns("col_pres").Width = 100
        dgdDetalle.Columns("por_pres").Width = 100
        dgdDetalle.Columns("col_puae").Width = 100
        dgdDetalle.Columns("por_puae").Width = 100
        dgdDetalle.Columns("col_zoar").Width = 100
        dgdDetalle.Columns("por_zoar").Width = 100
        dgdDetalle.Columns("col_zour").Width = 100
        dgdDetalle.Columns("por_zour").Width = 100

        'dgdDetalle.Splits(0).HeadingStyle.ForeColor = Color.MidnightBlue

        'La alineación de las celdas de cada columna 
        dgdDetalle.Columns("nm_depa").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        dgdDetalle.Columns("depa_has").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("ares_has").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("por_ares_i").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("por_libre").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("cantidad").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("col_anap").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("por_anap").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("col_arna").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("por_arna").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("col_ctmi").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("por_ctmi").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("col_oare").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("por_oare").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("col_pmar").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("por_pmar").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("col_pana").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("por_pana").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("col_pres").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("por_pres").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("col_puae").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("por_puae").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("col_zoar").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("por_zoar").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("col_zour").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgdDetalle.Columns("por_zour").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        'La alinación del encabezado de cada columna 
        dgdDetalle.Columns("nm_depa").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("depa_has").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("ares_has").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("por_ares_i").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("por_libre").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("cantidad").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("col_anap").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("por_anap").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("col_arna").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("por_arna").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("col_ctmi").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("por_ctmi").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("col_oare").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("por_oare").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("col_pmar").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("por_pmar").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("col_pana").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("por_pana").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("col_pres").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("por_pres").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("col_puae").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("por_puae").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("col_zoar").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("por_zoar").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("col_zour").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgdDetalle.Columns("por_zour").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        'El formato de las columnas numéricas 
        'DataGridView1.Columns("T_Llegada").DefaultCellStyle.Format = "##,##0.00"
        'dgdDetalle1.Columns("depa_has").DefaultCellStyle.Format = "###,###,##0.00"
        'dgdDetalle1.Columns("ares_has").DefaultCellStyle.Format = "###,###,##0.00"
        'dgdDetalle1.Columns("por_ares_i").DefaultCellStyle.Format = "###"
        'dgdDetalle1.Columns("por_libre").DefaultCellStyle.Format = "###"
        'dgdDetalle1.Columns("cantidad").DefaultCellStyle.Format = "#,###"

        dgdDetalle.Columns("col_anap").DefaultCellStyle.Format = "###,##0.00"
        dgdDetalle.Columns("por_anap").DefaultCellStyle.Format = "###,##0.00"
        dgdDetalle.Columns("col_arna").DefaultCellStyle.Format = "###,##0.00"
        dgdDetalle.Columns("por_arna").DefaultCellStyle.Format = "###,##0.00"
        dgdDetalle.Columns("col_ctmi").DefaultCellStyle.Format = "###,##0.00"
        dgdDetalle.Columns("por_ctmi").DefaultCellStyle.Format = "###,##0.00"
        dgdDetalle.Columns("col_oare").DefaultCellStyle.Format = "###,##0.00"
        dgdDetalle.Columns("por_oare").DefaultCellStyle.Format = "###,##0.00"
        dgdDetalle.Columns("col_pmar").DefaultCellStyle.Format = "####,##0.00"
        dgdDetalle.Columns("por_pmar").DefaultCellStyle.Format = "####,##0.00"
        dgdDetalle.Columns("col_pana").DefaultCellStyle.Format = "###,##0.00"
        dgdDetalle.Columns("por_pana").DefaultCellStyle.Format = "###,##0.00"
        dgdDetalle.Columns("col_pres").DefaultCellStyle.Format = "###,##0.00"
        dgdDetalle.Columns("por_pres").DefaultCellStyle.Format = "###,##0.00"
        dgdDetalle.Columns("col_puae").DefaultCellStyle.Format = "###,##0.00"
        dgdDetalle.Columns("por_puae").DefaultCellStyle.Format = "###,##0.00"
        dgdDetalle.Columns("col_zoar").DefaultCellStyle.Format = "###,##0.00"
        dgdDetalle.Columns("por_zoar").DefaultCellStyle.Format = "###,##0.00"
        dgdDetalle.Columns("col_zour").DefaultCellStyle.Format = "###,##0.00"
        dgdDetalle.Columns("por_zour").DefaultCellStyle.Format = "###,##0.00"
    End Sub

    'Private Sub Leer_Tabla_1()
    '    Dim pRow As IRow
    '    'Dim pTable As ITable
    '    'Dim pWorkspaceFactory As IWorkspaceFactory
    '    pWorkspaceFactory = New ShapefileWorkspaceFactory
    '    'Dim pFWS As IFeatureWorkspace
    '    pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_Path & "DBF\", 0)
    '    pTable = pFeatureWorkspace.OpenTable("Reporte")
    '    Dim ptableCursor As ICursor
    '    Dim pfields As Fields
    '    pfields = pTable.Fields
    '    Dim pQueryFilter As IQueryFilter
    '    'pfields3 = pTable.Fields
    '    pQueryFilter = New QueryFilter
    '    pQueryFilter.WhereClause = "CODIGO = '" & p_Codigo & "'"
    '    ptableCursor = pTable.Search(pQueryFilter, True)
    '    pRow = ptableCursor.NextRow
    '    Dim lostrAccion As String
    '    Dim lostrFecha As String
    '    Do Until pRow Is Nothing
    '        lostrAccion = pRow.Value(pfields.FindField("ACCION")) ' - -vvvvv
    '        lostrFecha = pRow.Value(pfields.FindField("FECHA"))
    '        pRow = ptableCursor.NextRow
    '        p_Existe.text = "Si"
    '    Loop
    'End Sub



    Private Sub chkProcesarGIS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    Function ExeEspera(ByVal COMANDO As String)
        Dim hProcess As Long
        Dim RetVal As Long
        hProcess = OpenProcess(PROCESS_QUERY_INFORMATION, False, Shell(COMANDO, vbMinimizedNoFocus))
        Do
            GetExitCodeProcess(hProcess, RetVal)
            Application.DoEvents()
            Pausa(100)
        Loop While RetVal = STILL_ACTIVE
    End Function


    Public Sub Pinta_Grilla()
        dgdDetalle1.BackColor = Color.FromArgb(242, 242, 240)
        'dgdDetalle.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        'dgdDetalle.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        'dgdDetalle.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Close()
    End Sub


    Public Sub ExportarDatosExcel(ByVal DataGridView1 As DataGridView, ByVal titulo As String)
        Dim m_Excel As New Excel.Application
        m_Excel.Cursor = Excel.XlMousePointer.xlWait
        m_Excel.Visible = True
        Dim objLibroExcel As Excel.Workbook = m_Excel.Workbooks.Add
        Dim objHojaExcel As Excel.Worksheet = objLibroExcel.Worksheets(1)
        With objHojaExcel
            .Visible = Excel.XlSheetVisibility.xlSheetVisible
            .Activate()
            'Encabezado.
            .Range("A1:L1").Merge()
            .Range("A1:L1").Value = "INGEMMET"
            .Range("A1:L1").Font.Bold = True
            .Range("A1:L1").Font.Size = 16
            'Texto despues del encabezado.
            .Range("A2:L2").Merge()
            .Range("A2:L2").Value = titulo
            .Range("A2:L2").Font.Bold = True
            .Range("A2:L2").Font.Size = 10
            'Nombres
            'Estilo a titulos de la tabla.
            .Range("A6:P6").Font.Bold = True
            'Establecer tipo de letra al rango determinado.
            .Range("A1:P37").Font.Name = "Tahoma"
            'Los datos se registran a partir de la columna A, fila 4.
            Const primeraLetra As Char = "A"
            Const primerNumero As Short = 6
            Dim Letra As Char, UltimaLetra As Char
            Dim Numero As Integer, UltimoNumero As Integer
            Dim cod_letra As Byte = Asc(primeraLetra) - 1
            Dim sepDec As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
            Dim sepMil As String = Application.CurrentCulture.NumberFormat.NumberGroupSeparator
            Dim strColumna As String = ""
            Dim LetraIzq As String = ""
            Dim cod_LetraIzq As Byte = Asc(primeraLetra) - 1
            Letra = primeraLetra
            Numero = primerNumero
            Dim objCelda As Excel.Range
            For Each c As DataGridViewColumn In DataGridView1.Columns
                If c.Visible Then
                    : If Letra = "Z" Then
                        Letra = primeraLetra
                        cod_letra = Asc(primeraLetra)
                        cod_LetraIzq += 1
                        LetraIzq = Chr(cod_LetraIzq)
                    Else
                        cod_letra += 1
                        Letra = Chr(cod_letra)
                    End If
                    strColumna = LetraIzq + Letra + Numero.ToString
                    objCelda = .Range(strColumna, Type.Missing)
                    objCelda.Value = c.HeaderText
                    objCelda.EntireColumn.Font.Size = 10
                    'Establece un formato a los numeros por Default.
                    'objCelda.EntireColumn.NumberFormat = c.DefaultCellStyle.Format
                    If c.ValueType Is GetType(Decimal) OrElse c.ValueType Is GetType(Double) Then
                        objCelda.EntireColumn.NumberFormat = "#" + sepMil + "0" + sepDec + "00"
                    End If
                End If
            Next
            Dim objRangoEncab As Excel.Range = .Range(primeraLetra + Numero.ToString, LetraIzq + Letra + Numero.ToString)
            objRangoEncab.BorderAround(1, Excel.XlBorderWeight.xlMedium)
            UltimaLetra = Letra
            Dim UltimaLetraIzq As String = LetraIzq
            'Cargar Datos del DataGridView.
            Dim i As Integer = Numero + 1
            For Each reg As DataGridViewRow In DataGridView1.Rows
                LetraIzq = ""
                cod_LetraIzq = Asc(primeraLetra) - 1
                Letra = primeraLetra
                cod_letra = Asc(primeraLetra) - 1
                For Each c As DataGridViewColumn In DataGridView1.Columns
                    If c.Visible Then
                        If Letra = "Z" Then
                            Letra = primeraLetra
                            cod_letra = Asc(primeraLetra)
                            cod_LetraIzq += 1
                            LetraIzq = Chr(cod_LetraIzq)
                        Else
                            cod_letra += 1
                            Letra = Chr(cod_letra)
                        End If
                        strColumna = LetraIzq + Letra
                        'Aqui se realiza la carga de datos.
                        .Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", reg.Cells(c.Index).Value)
                        'Establece las propiedades de los datos del DataGridView por Default.
                        '.Cells(i, strColumna) = IIf(IsDBNull(reg.(c.DataPropertyName)), c.DefaultCellStyle.NullValue, reg(c.DataPropertyName))
                        '.Range(strColumna + i, strColumna + i).In()
                    End If
                Next
                Dim objRangoReg As Excel.Range = .Range(primeraLetra + i.ToString, strColumna + i.ToString)
                objRangoReg.Rows.BorderAround()
                objRangoReg.Select()
                i += 1
            Next
            UltimoNumero = i
            'Dibujar las líneas de las columnas.
            LetraIzq = ""
            cod_LetraIzq = Asc("A")
            cod_letra = Asc(primeraLetra)
            Letra = primeraLetra
            For Each c As DataGridViewColumn In DataGridView1.Columns
                If c.Visible Then
                    objCelda = .Range(LetraIzq + Letra + primerNumero.ToString, LetraIzq + Letra + (UltimoNumero - 1).ToString)
                    objCelda.BorderAround()
                    If Letra = "Z" Then
                        Letra = primeraLetra
                        cod_letra = Asc(primeraLetra)
                        LetraIzq = Chr(cod_LetraIzq)
                        cod_LetraIzq += 1
                    Else
                        cod_letra += 1
                        Letra = Chr(cod_letra)
                    End If
                End If
            Next
            'Dibujar el border exterior grueso de la tabla.
            Dim objRango As Excel.Range = .Range(primeraLetra + primerNumero.ToString, UltimaLetraIzq + UltimaLetra + (UltimoNumero - 1).ToString)
            objRango.Select()
            objRango.Columns.AutoFit()
            objRango.Columns.BorderAround(1, Excel.XlBorderWeight.xlMedium)
        End With
        m_Excel.Cursor = Excel.XlMousePointer.xlDefault

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            'Intentar generar el documento.
            'Se adjunta un texto debajo del encabezado 
            'ExportarDatosExcel(dgdDetalle1, "Reporte de Areas Restringidas ")
            Select Case tp_Calculo_Area2.SelectedIndex
                Case 0
                    ExportarDatosExcel(dgdDetalle1, "Reporte de Areas Restringidas ")
                Case 1
                    ExportarDatosExcel(dgdDetalle, "Reporte de Areas Naturales ")
            End Select
        Catch ex As Exception
            'Si el intento es fallido, mostrar MsgBox.
            MessageBox.Show("No se puede generar el documento Excel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Function Contador_Table(ByVal p_Table As String, ByVal p_Filtro As String)
        Dim pFeatureWorkspace As IFeatureWorkspace
        x = 0
        glo_PathGDB = "C:\BDGEOCATMIN\Temporal\ARESTRINGIDA\"
        ' p_Table = "aresdep_u.shp"
        ' p_Filtro = "NM_DEPA = 'LIMA'"
        pWorkspaceFactory = New ShapefileWorkspaceFactory 'AccessWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_PathGDB, 0)
        Dim pTable As ITable
        pTable = pFeatureWorkspace.OpenTable(p_Table)
        '**************************
        Dim pDataset As IDataset
        Dim pCursor As ICursor
        pDataset = pTable
        pFeatureWorkspace = pDataset.Workspace
        Dim pQueryDef As IQueryDef
        pQueryDef = pFeatureWorkspace.CreateQueryDef
        With pQueryDef
            .Tables = pDataset.Name ' Fully qualified table name
            .WhereClause = p_Filtro
            ' .SubFields = "DISTINCT CODIGO"
            pCursor = .Evaluate
        End With
        ''**************************
        Dim pCantRegistro As Integer = ListRow(pTable, p_Filtro)
        'Dim loCodigo(pCantRegistro) As String
        Dim pRow As IRow
        Dim c As Integer = 0
        pRow = pCursor.NextRow
        Dim lodtRegistros As New DataTable
        lodtRegistros.Columns.Add("CODIGO", Type.GetType("System.String"))
        Do Until pRow Is Nothing
            Dim dr As DataRow
            dr = lodtRegistros.NewRow
            dr.Item(0) = pRow.Value(pRow.Fields.FindField("CODIGO")).ToString
            lodtRegistros.Rows.Add(dr)
            pRow = pCursor.NextRow
            'c = c + 1
        Loop
        Dim viewDetalle As DataView = lodtRegistros.DefaultView
        'viewDetalle.RowFilter = "DOMINIO = '" & loDtbDominoFiltro.Rows(0).Item("CODIGO_ITEM").ToString & "'"
        Dim lodtbItems As New DataTable
        lodtbItems = viewDetalle.ToTable("CODIGO", True)

        'Dim pCantRegistro As Integer = ListRow(pTable, p_Filtro)
        'Dim pCantRegistro As Integer = 0 ' GetUniqueValues(pTable, "CODIGO")
        ' ShowUniqueValues(pTable, "CODIGO")

        Return lodtbItems.Rows.Count '    pCantRegistro
    End Function

    Function Cargar_Grilla_shp(ByVal p_Path As String, ByVal p_Table As String) As DataTable
        Dim pFeatureWorkspace As IFeatureWorkspace
        pWorkspaceFactory = New ShapefileWorkspaceFactory 'AccessWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(p_Path, 0)
        Dim pTable As ITable
        pTable = pFeatureWorkspace.OpenTable(p_Table)
        Dim pQueryDef As IQueryDef
        Dim pRow As IRow
        Dim pCursor As ICursor
        Dim pDataset As IDataset
        pDataset = pTable
        pFeatureWorkspace = pDataset.Workspace
        pQueryDef = pFeatureWorkspace.CreateQueryDef
        With pQueryDef
            .Tables = pDataset.Name ' Fully qualified table name
            ' .WhereClause = p_Filtro
            '.SubFields = "DISTINCT(" & "CODIGO" & ")"
            pCursor = .Evaluate
        End With
        Dim lostr_Zona As String = ""
        pRow = pCursor.NextRow
        'Type.GetType("System.String")
        Dim lodtRegistros As New DataTable
        lodtRegistros.Columns.Add("NM_DEPA", Type.GetType("System.String"))
        lodtRegistros.Columns.Add("DEPA_HAS", Type.GetType("System.Double"))
        lodtRegistros.Columns.Add("ARES_HAS", Type.GetType("System.Double"))
        lodtRegistros.Columns.Add("POR_ARES_I", Type.GetType("System.Double"))
        lodtRegistros.Columns.Add("POR_LIBRE", Type.GetType("System.Double"))
        lodtRegistros.Columns.Add("CANTIDAD", Type.GetType("System.Double"))

        'lodtRegistros.Columns.Add("col_Anap", Type.GetType("System.String"))
        'lodtRegistros.Columns.Add("col_arna", Type.GetType("System.String"))
        'lodtRegistros.Columns.Add("col_ctmi", Type.GetType("System.String"))
        'lodtRegistros.Columns.Add("col_oare", Type.GetType("System.String"))
        'lodtRegistros.Columns.Add("col_pmar", Type.GetType("System.String"))
        'lodtRegistros.Columns.Add("col_pana", Type.GetType("System.String"))
        'lodtRegistros.Columns.Add("col_pres", Type.GetType("System.String"))
        'lodtRegistros.Columns.Add("col_puae", Type.GetType("System.String"))
        'lodtRegistros.Columns.Add("col_zoar", Type.GetType("System.String"))
        'lodtRegistros.Columns.Add("col_zour", Type.GetType("System.String"))

        Do Until pRow Is Nothing
            Dim dr As DataRow
            dr = lodtRegistros.NewRow
            dr.Item(0) = pRow.Value(pRow.Fields.FindField("NM_DEPA")).ToString
            dr.Item(1) = CType(pRow.Value(pRow.Fields.FindField("DEPA_HAS")).ToString, Double)
            dr.Item(2) = CType(pRow.Value(pRow.Fields.FindField("ARES_HAS")).ToString, Double)
            dr.Item(3) = CType(pRow.Value(pRow.Fields.FindField("POR_ARES_I")).ToString, Double)
            dr.Item(4) = CType(pRow.Value(pRow.Fields.FindField("POR_LIBRE")).ToString, Double)
            dr.Item(5) = CType(Contador_Table("aresdep_u", "NM_DEPA = '" & pRow.Value(pRow.Fields.FindField("NM_DEPA")).ToString & "'"), Double)
            lodtRegistros.Rows.Add(dr)
            pRow = pCursor.NextRow
        Loop
        Return lodtRegistros
    End Function

    Private Function ListRow(ByRef pTable As ITable, ByRef Query As String) As Integer
        Dim pQueryFilter As IQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = Query
        Dim i As Integer = pTable.RowCount(pQueryFilter)
        ListRow = i
    End Function

    Private Sub codigo_distinct(ByVal m_Application As IApplication)
        Dim pMxDoc As IMxDocument
        Dim pFLayer As IFeatureLayer = Nothing
        Dim pData As IDataStatistics
        Dim pCursor As ICursor
        Dim pStatResults As IStatisticsResults
        pMxDoc = m_Application.Document 'ThisDocument
        'pFLayer = pMxDoc.FocusMap.Layer(0)

        pMap = pMxDoc.FocusMap
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "aresdep_u" Then
                pFLayer = pMxDoc.FocusMap.Layer(A)
                Exit For
            End If
        Next A

        pCursor = pFLayer.Search(Nothing, False)
        pData = New DataStatistics
        pData.Field = "CODIGO"
        pData.Cursor = pCursor

        Dim pEnumVar As IEnumVariantSimple, value As Object
        pEnumVar = pData.UniqueValues
        value = pEnumVar.Next
        Do Until value Is ""
            Debug.Print("value - " & value)
            value = pEnumVar.Next
        Loop

        pCursor = pFLayer.Search(Nothing, False)
        pData.Cursor = pCursor
        pStatResults = pData.Statistics
        Debug.Print("mean - " & pStatResults.Mean)
    End Sub

    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        'Dim RetVal
        Me.btnCargar.Enabled = True
        Try
            Application.DoEvents()
            Process.Start("C:\BDGEOCATMIN\ares_ud.bat", 0)
            'Process.Start("C:\BDGEOCATMIN\Estilos\Calcula_Ares_02.bat", 1)
            'Process.Start("C:\BDGEOCATMIN\Estilos\Calcula_Ares_03.bat", 2)
            'Process.Start("C:\BDGEOCATMIN\Estilos\area_res1.py")
            'For w As Integer = 0 To 1000000000 : Next
            'Process.Start("C:\BDGEOCATMIN\Estilos\ares_sql.py")
            'For w As Integer = 0 To 1000000000 : Next
            'Process.Start("C:\BDGEOCATMIN\Estilos\area_dep_int1.py")
            ''Shell("C:\BDGEOCATMIN\Estilos\Calcula_Ares.bat", AppWinStyle.Hide, True)
            'ExeEspera("C:\BDGEOCATMIN\Estilos\Calcula_Ares_01.bat")
            'ExeEspera("C:\BDGEOCATMIN\Estilos\Calcula_Ares_02.bat")
            'ExeEspera("C:\BDGEOCATMIN\Estilos\Calcula_Ares_03.bat")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        MsgBox("Una vez que termine de cerrar la aplicacion," & vbNewLine & "presionar botón: Cargar Información", MsgBoxStyle.Information, "Aviso")
    End Sub

    Private Sub btnCargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCargar.Click
        cargar_grilla()
    End Sub

    Private Sub cboRegion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRegion.SelectedIndexChanged
        Dim dvwRegion As New DataView
        Dim ds As New DataSet
        Dim s1 As Decimal = 0
        Dim s2 As Decimal = 0
        Try
            dvwRegion = dt.DefaultView
            dgdDetalle1.DataSource = dvwRegion
            If cboRegion.SelectedIndex = 0 Then
                ' cboRegion.Items.Clear()
                dvwRegion.RowFilter = Nothing
                dgdDetalle1.DataSource = dvwRegion
                For r As Integer = 0 To dt.Rows.Count - 1
                    Select Case dt.Rows(r).Item(0)
                        Case "MAR", "FUERA DEL PERU"
                        Case Else
                            s1 = s1 + dt.Rows(r).Item("DEPA_HAS")
                            s2 = s2 + dt.Rows(r).Item("ARES_HAS")
                    End Select
                    cboRegion.Items.Add(dt.Rows(r).Item("NM_DEPA"))
                Next
                txtArea1.Text = Format(s1, "###,###,###,###.##")
                txtArea2.Text = Format(s2, "###,###,###,###.##")
                txtPorcentaje.Text = Format((s2 / s1) * 100, "###,###,###,###.##")
                ' "NM_DEPA = '" & cboRegion.Text & "'"
                '   frmCalculaARES_Load(Nothing, Nothing)
            Else
                dvwRegion.RowFilter = "NM_DEPA = '" & cboRegion.Text & "'"
                dgdDetalle1.DataSource = dvwRegion
                txtArea1.Text = Format(CType(dvwRegion.Item(0).Row(1), Decimal), "###,###,###,###.##")
                txtArea2.Text = Format(CType(dvwRegion.Item(0).Row(2), Decimal), "###,###,###,###.##")
                txtPorcentaje.Text = Format(((CType(dvwRegion.Item(0).Row(2), Decimal)) / CType(dvwRegion.Item(0).Row(1), Decimal)) * 100, "###,###,###,###.##")
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub Cargar_Campos_Datagrid()
        For w As Integer = 0 To dgdDetalle1.RowCount - 1
            Genera_Tabla("anatdep_d.shp", "nm_depa = '" & dt.Rows(w).Item("nm_depa") & "'", w)
        Next
    End Sub

    Public Sub Genera_Tabla(ByVal p_Table As String, ByVal p_Filtro As String, ByVal pFila As Integer)
        Dim pFeatureWorkspace As IFeatureWorkspace
        x = 0
        glo_PathGDB = "C:\BDGEOCATMIN\Temporal\ARESTRINGIDA\"
        pWorkspaceFactory = New ShapefileWorkspaceFactory 'AccessWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_PathGDB, 0)
        Dim pTable As ITable
        pTable = pFeatureWorkspace.OpenTable(p_Table)
        '**************************
        Dim pDataset As IDataset
        Dim pCursor As ICursor
        pDataset = pTable
        pFeatureWorkspace = pDataset.Workspace
        Dim pQueryDef As IQueryDef
        pQueryDef = pFeatureWorkspace.CreateQueryDef
        With pQueryDef
            .Tables = pDataset.Name ' Fully qualified table name
            .WhereClause = p_Filtro
            ' .SubFields = "DISTINCT CODIGO"
            pCursor = .Evaluate
        End With
        ''**************************
        Dim pCantRegistro As Integer = ListRow(pTable, p_Filtro)
        'Dim loCodigo(pCantRegistro) As String
        Dim pRow As IRow
        Dim c As Integer = 0
        pRow = pCursor.NextRow
        'Dim lodtRegistros As New DataTable
        'lodtRegistros.Columns.Add("CODIGO", Type.GetType("System.String"))
        Dim p_nom_tprese As String = ""
        Dim p_has_tprese As String = ""
        Dim por_has_tprese As String = ""
        Dim PColumna As Integer = 5
        Do Until pRow Is Nothing
            'Dim dr As DataRow
            'dr = lodtRegistros.NewRow
            'dr.Item(0) = pRow.Value(pRow.Fields.FindField("CODIGO")).ToString
            'p_Nom_TpRese = pRow.Value(pRow.Fields.FindField("TP_RESE")).ToString
            p_has_tprese = pRow.Value(pRow.Fields.FindField("ANAT_HA")).ToString
            por_has_tprese = pRow.Value(pRow.Fields.FindField("POR_ANAT")).ToString
            Select Case UCase(pRow.Value(pRow.Fields.FindField("TP_RESE")).ToString)
                Case "ANAP"
                    dgdDetalle.Rows(pFila).Cells(6).Value = p_has_tprese
                    dgdDetalle.Rows(pFila).Cells(7).Value = por_has_tprese
                Case "AREA NATURAL"
                    dgdDetalle.Rows(pFila).Cells(8).Value = p_has_tprese
                    dgdDetalle.Rows(pFila).Cells(9).Value = por_has_tprese
                Case "CONCESION DE TRANSPORTE MINERO"
                    dgdDetalle.Rows(pFila).Cells(10).Value = p_has_tprese
                    dgdDetalle.Rows(pFila).Cells(11).Value = por_has_tprese
                Case "OTRA AREA RESTRINGIDA"
                    dgdDetalle.Rows(pFila).Cells(12).Value = p_has_tprese
                    dgdDetalle.Rows(pFila).Cells(13).Value = por_has_tprese
                Case "PRODUCTORES MINEROS ARTESANALES"
                    dgdDetalle.Rows(pFila).Cells(14).Value = p_has_tprese
                    dgdDetalle.Rows(pFila).Cells(15).Value = por_has_tprese
                Case "PROPUESTA DE AREA NATURAL"
                    dgdDetalle.Rows(pFila).Cells(16).Value = p_has_tprese
                    dgdDetalle.Rows(pFila).Cells(17).Value = por_has_tprese
                Case "PROYECTO ESPECIAL"
                    dgdDetalle.Rows(pFila).Cells(18).Value = p_has_tprese
                    dgdDetalle.Rows(pFila).Cells(19).Value = por_has_tprese
                Case "PUERTO Y/O AEROPUERTOS"
                    dgdDetalle.Rows(pFila).Cells(20).Value = p_has_tprese
                    dgdDetalle.Rows(pFila).Cells(21).Value = por_has_tprese
                Case "ZONA ARQUEOLOGICA"
                    dgdDetalle.Rows(pFila).Cells(22).Value = p_has_tprese
                    dgdDetalle.Rows(pFila).Cells(23).Value = por_has_tprese
                Case "ZONA URBANA"
                    dgdDetalle.Rows(pFila).Cells(24).Value = p_has_tprese
                    dgdDetalle.Rows(pFila).Cells(25).Value = por_has_tprese
            End Select
            pRow = pCursor.NextRow
        Loop
    End Sub

    Private Sub Genera_Data_Grilla()
        Dim col_anap As New DataGridViewTextBoxColumn()
        Dim por_anap As New DataGridViewTextBoxColumn()
        Dim col_arna As New DataGridViewTextBoxColumn()
        Dim por_arna As New DataGridViewTextBoxColumn()
        Dim col_ctmi As New DataGridViewTextBoxColumn()
        Dim por_ctmi As New DataGridViewTextBoxColumn()
        Dim col_oare As New DataGridViewTextBoxColumn()
        Dim por_oare As New DataGridViewTextBoxColumn()
        Dim col_pmar As New DataGridViewTextBoxColumn()
        Dim por_pmar As New DataGridViewTextBoxColumn()
        Dim col_pana As New DataGridViewTextBoxColumn()
        Dim por_pana As New DataGridViewTextBoxColumn()
        Dim col_pres As New DataGridViewTextBoxColumn()
        Dim por_pres As New DataGridViewTextBoxColumn()
        Dim col_puae As New DataGridViewTextBoxColumn()
        Dim por_puae As New DataGridViewTextBoxColumn()
        Dim col_zoar As New DataGridViewTextBoxColumn()
        Dim por_zoar As New DataGridViewTextBoxColumn()
        Dim col_zour As New DataGridViewTextBoxColumn()
        Dim por_zour As New DataGridViewTextBoxColumn()
        col_anap.HeaderText = "Anap" : col_anap.Name = "col_Anap"
        por_anap.HeaderText = "% Anap" : por_anap.Name = "por_Anap"
        col_arna.HeaderText = "Área Natural" : col_arna.Name = "col_arna"
        por_arna.HeaderText = "% Área Natural" : por_arna.Name = "por_arna"
        col_ctmi.HeaderText = "Concesión de Transporte Minero" : col_ctmi.Name = "col_ctmi"
        por_ctmi.HeaderText = "% Concesión de Transporte Minero" : por_ctmi.Name = "por_ctmi"
        col_oare.HeaderText = "Otra Área Restringida" : col_oare.Name = "col_oare"
        por_oare.HeaderText = "% Otra Área Restringida" : por_oare.Name = "por_oare"
        col_pmar.HeaderText = "Productores Mineros Artesanales" : col_pmar.Name = "col_pmar"
        por_pmar.HeaderText = "% Productores Mineros Artesanales" : por_pmar.Name = "por_pmar"
        col_pana.HeaderText = "Propuesta de Área Natural" : col_pana.Name = "col_pana"
        por_pana.HeaderText = "% Propuesta de Área Natural" : por_pana.Name = "por_pana"
        col_pres.HeaderText = "Proyecto Especial" : col_pres.Name = "col_pres"
        por_pres.HeaderText = "% Proyecto Especial" : por_pres.Name = "por_pres"
        col_puae.HeaderText = "Puerto y/o Aeropuerto" : col_puae.Name = "col_puae"
        por_puae.HeaderText = "% Puerto y/o Aeropuerto" : por_puae.Name = "por_puae"
        col_zoar.HeaderText = "Zona Arqueológica" : col_zoar.Name = "col_zoar"
        por_zoar.HeaderText = "% Zona Arqueológica" : por_zoar.Name = "por_zoar"
        col_zour.HeaderText = "Zona Urbana" : col_zour.Name = "col_zour"
        por_zour.HeaderText = "% Zona Urbana" : por_zour.Name = "por_zour"
        dgdDetalle.Columns.Add(col_anap.Name, col_anap.HeaderText)
        dgdDetalle.Columns.Add(por_anap.Name, por_anap.HeaderText)
        dgdDetalle.Columns.Add(col_arna.Name, col_arna.HeaderText)
        dgdDetalle.Columns.Add(por_arna.Name, por_arna.HeaderText)
        dgdDetalle.Columns.Add(col_ctmi.Name, col_ctmi.HeaderText)
        dgdDetalle.Columns.Add(por_ctmi.Name, por_ctmi.HeaderText)
        dgdDetalle.Columns.Add(col_oare.Name, col_oare.HeaderText)
        dgdDetalle.Columns.Add(por_oare.Name, por_oare.HeaderText)
        dgdDetalle.Columns.Add(col_pmar.Name, col_pmar.HeaderText)
        dgdDetalle.Columns.Add(por_pmar.Name, por_pmar.HeaderText)
        dgdDetalle.Columns.Add(col_pana.Name, col_pana.HeaderText)
        dgdDetalle.Columns.Add(por_pana.Name, por_pana.HeaderText)
        dgdDetalle.Columns.Add(col_pres.Name, col_pres.HeaderText)
        dgdDetalle.Columns.Add(por_pres.Name, por_pres.HeaderText)
        dgdDetalle.Columns.Add(col_puae.Name, col_puae.HeaderText)
        dgdDetalle.Columns.Add(por_puae.Name, por_puae.HeaderText)
        dgdDetalle.Columns.Add(col_zoar.Name, col_zoar.HeaderText)
        dgdDetalle.Columns.Add(por_zoar.Name, por_zoar.HeaderText)
        dgdDetalle.Columns.Add(col_zour.Name, col_zour.HeaderText)
        dgdDetalle.Columns.Add(por_zour.Name, por_zour.HeaderText)
        'dgdDetalle.Columns(1).Visible = False
        'dgdDetalle.Columns(2).Visible = False
        'dgdDetalle.Columns(3).Visible = False
        'dgdDetalle.Columns(4).Visible = False
        'dgdDetalle.Columns(5).Visible = False
        For w As Integer = 0 To dgdDetalle1.RowCount - 1
            Genera_Tabla("anatdep_d.shp", "nm_depa = '" & dt.Rows(w).Item("nm_depa") & "'", w)
        Next
    End Sub

    Private Sub tp_Calculo_Area2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tp_Calculo_Area2.Click
        If logloFlagGrilla = 0 Then Exit Sub
        If tp_Calculo_Area2.SelectedIndex = 1 Then
            Genera_Data_Grilla()
            With dgdDetalle
                .AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue
                '.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .AutoResizeColumns() ' ------->>> ESTA ES LA INSTRUCCION QUE NECESITAS..
                .DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            PT_Agregar_Funciones_Detalle_03()
            PT_Forma_Grilla_Detalle_03()
            logloFlagGrilla = 0
        End If
    End Sub
End Class