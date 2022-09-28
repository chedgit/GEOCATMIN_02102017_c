Imports System.Drawing
Imports PORTAL_Clases
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.GISClient
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

Public Class Frm_Resultado_Eval
    Public m_application As IApplication
    Public papp As IApplication
    Private Const Col_Num = 0
    Private Const Col_Evaluacion = 1
    Private Const Col_Estado = 2
    Private Const Col_Concesion = 3
    Private Const Col_Codigo = 4

    Private Sub PINTAR_FORMULARIO()
        Me.BackColor = Drawing.Color.FromArgb(242, 242, 240)
        Me.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdExtinguido.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdExtinguido.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        Me.dgdExtinguido.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        Me.dgdExtinguido.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdPrioritario.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdPrioritario.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        Me.dgdPrioritario.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        Me.dgdPrioritario.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdPosterior.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdPosterior.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        Me.dgdPosterior.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        Me.dgdPosterior.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdSimultaneo.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdSimultaneo.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        Me.dgdSimultaneo.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        Me.dgdSimultaneo.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdRedenuncio.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdRedenuncio.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        Me.dgdRedenuncio.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        Me.dgdRedenuncio.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
        Me.Tabdatos.BackColor = Color.FromArgb(242, 242, 240)
    End Sub
    Public Sub PT_Inicializar_Grilla_PR()
        'Dim dvwLista_DM As New DataView(lodtbLista_DM)
        Dim lodtbPrioritario_DM As New DataTable
        lodtbPrioritario_DM.Columns.Add("NUM", GetType(String))
        lodtbPrioritario_DM.Columns.Add("EVALUACION", GetType(String))
        lodtbPrioritario_DM.Columns.Add("ESTADO", GetType(String))
        lodtbPrioritario_DM.Columns.Add("CONCESION", GetType(String))
        lodtbPrioritario_DM.Columns.Add("CODIGO", GetType(String))
        PT_Estilo_Grilla_PR(lodtbPrioritario_DM) : PT_Cargar_Grilla_PR(lodtbPrioritario_DM)
        PT_Agregar_Funciones_PR() : PT_Forma_Grilla_PR()
    End Sub
    Public Sub PT_Inicializar_Grilla_PO()
        Dim lodtbPosterior_DM As New DataTable
        lodtbPosterior_DM.Columns.Add("NUM", GetType(String))
        lodtbPosterior_DM.Columns.Add("EVALUACION", GetType(String))
        lodtbPosterior_DM.Columns.Add("ESTADO", GetType(String))
        lodtbPosterior_DM.Columns.Add("CONCESION", GetType(String))
        lodtbPosterior_DM.Columns.Add("CODIGO", GetType(String))
        PT_Estilo_Grilla_PR(lodtbPosterior_DM) : PT_Cargar_Grilla_PO(lodtbPosterior_DM)
        PT_Agregar_Funciones_PO() : PT_Forma_Grilla_PO()
    End Sub
    Public Sub PT_Inicializar_Grilla_SI()
        Dim lodtbSimultaneo_DM As New DataTable
        lodtbSimultaneo_DM.Columns.Add("NUM", GetType(String))
        lodtbSimultaneo_DM.Columns.Add("EVALUACION", GetType(String))
        lodtbSimultaneo_DM.Columns.Add("ESTADO", GetType(String))
        lodtbSimultaneo_DM.Columns.Add("CONCESION", GetType(String))
        lodtbSimultaneo_DM.Columns.Add("CODIGO", GetType(String))
        PT_Estilo_Grilla_PR(lodtbSimultaneo_DM) : PT_Cargar_Grilla_SI(lodtbSimultaneo_DM)
        PT_Agregar_Funciones_SI() : PT_Forma_Grilla_SI()
    End Sub
    Public Sub PT_Inicializar_Grilla_EX()
        Dim lodtbExtinguido_DM As New DataTable
        lodtbExtinguido_DM.Columns.Add("NUM", GetType(String))
        lodtbExtinguido_DM.Columns.Add("EVALUACION", GetType(String))
        lodtbExtinguido_DM.Columns.Add("ESTADO", GetType(String))
        lodtbExtinguido_DM.Columns.Add("CONCESION", GetType(String))
        lodtbExtinguido_DM.Columns.Add("CODIGO", GetType(String))
        PT_Estilo_Grilla_PR(lodtbExtinguido_DM) : PT_Cargar_Grilla_EX(lodtbExtinguido_DM)
        PT_Agregar_Funciones_EX() : PT_Forma_Grilla_EX()
    End Sub
    Public Sub PT_Inicializar_Grilla_RD()
        Dim lodtbredenuncios_DM As New DataTable
        lodtbredenuncios_DM.Columns.Add("NUM", GetType(String))
        lodtbredenuncios_DM.Columns.Add("EVALUACION", GetType(String))
        lodtbredenuncios_DM.Columns.Add("ESTADO", GetType(String))
        lodtbredenuncios_DM.Columns.Add("CONCESION", GetType(String))
        lodtbredenuncios_DM.Columns.Add("CODIGO", GetType(String))
        PT_Estilo_Grilla_RD(lodtbredenuncios_DM) : PT_Cargar_Grilla_RD(lodtbredenuncios_DM)
        PT_Agregar_Funciones_RD() : PT_Forma_Grilla_RD()
    End Sub


    Private Sub PT_Estilo_Grilla_PR(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(Col_Num).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Evaluacion).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Estado).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Concesion).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Codigo).DefaultValue = ""
    End Sub
    Private Sub PT_Estilo_Grilla_RD(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(Col_Num).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Evaluacion).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Estado).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Concesion).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Codigo).DefaultValue = ""
    End Sub

    Private Sub PT_Cargar_Grilla_PR(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdPrioritario.DataSource = dvwDetalle
        Pinta_Grilla_PR()
    End Sub
    Private Sub PT_Cargar_Grilla_PO(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdPosterior.DataSource = dvwDetalle
        Pinta_Grilla_PR()
    End Sub
    Private Sub PT_Cargar_Grilla_SI(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdSimultaneo.DataSource = dvwDetalle
        Pinta_Grilla_PR()
    End Sub
    Private Sub PT_Cargar_Grilla_EX(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdExtinguido.DataSource = dvwDetalle
        Pinta_Grilla_PR()
    End Sub
    Private Sub PT_Cargar_Grilla_RD(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdRedenuncio.DataSource = dvwDetalle
        Pinta_Grilla_RD()
    End Sub

    Public Sub Pinta_Grilla_PR()
        Me.dgdPrioritario.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdPrioritario.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        Me.dgdPrioritario.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        Me.dgdPrioritario.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
    End Sub
    Public Sub Pinta_Grilla_RD()
        Me.dgdRedenuncio.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdRedenuncio.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        Me.dgdRedenuncio.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        Me.dgdRedenuncio.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
    End Sub
    Private Sub PT_Agregar_Funciones_PR() 'ByVal dgdTemporal As C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        Me.dgdPrioritario.Columns(Col_Num).DefaultValue = ""
        Me.dgdPrioritario.Columns(Col_Evaluacion).DefaultValue = ""
        Me.dgdPrioritario.Columns(Col_Estado).DefaultValue = ""
        Me.dgdPrioritario.Columns(Col_Concesion).DefaultValue = ""
        Me.dgdPrioritario.Columns(Col_Codigo).DefaultValue = ""
    End Sub
    Private Sub PT_Forma_Grilla_PR() 'ByVal dgdTemporal As C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        Me.dgdPrioritario.Splits(0).DisplayColumns(Col_Num).Width = 20
        Me.dgdPrioritario.Splits(0).DisplayColumns(Col_Evaluacion).Width = 70
        dgdPrioritario.Splits(0).DisplayColumns(Col_Estado).Width = 70
        dgdPrioritario.Splits(0).DisplayColumns(Col_Concesion).Width = 175
        dgdPrioritario.Splits(0).DisplayColumns(Col_Codigo).Width = 70
        dgdPrioritario.Columns("NUM").Caption = "N°"
        dgdPrioritario.Columns("EVALUACION").Caption = "Evaluación"
        dgdPrioritario.Columns("ESTADO").Caption = "Estado"
        dgdPrioritario.Columns("CONCESION").Caption = "Concesión"
        dgdPrioritario.Columns("CODIGO").Caption = "Código"
        Me.dgdPrioritario.Splits(0).DisplayColumns(Col_Num).Locked = True
        Me.dgdPrioritario.Splits(0).DisplayColumns(Col_Evaluacion).Locked = True
        Me.dgdPrioritario.Splits(0).DisplayColumns(Col_Estado).Locked = True
        Me.dgdPrioritario.Splits(0).DisplayColumns(Col_Codigo).Locked = True
        dgdPrioritario.Splits(0).DisplayColumns(Col_Num).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        dgdPrioritario.Splits(0).DisplayColumns(Col_Evaluacion).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        dgdPrioritario.Splits(0).DisplayColumns(Col_Estado).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        dgdPrioritario.Splits(0).DisplayColumns(Col_Concesion).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        dgdPrioritario.Splits(0).DisplayColumns(Col_Codigo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        dgdPrioritario.Splits(0).DisplayColumns(Col_Num).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        dgdPrioritario.Splits(0).DisplayColumns(Col_Evaluacion).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        dgdPrioritario.Splits(0).DisplayColumns(Col_Estado).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        dgdPrioritario.Splits(0).DisplayColumns(Col_Concesion).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
        dgdPrioritario.Splits(0).DisplayColumns(Col_Codigo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
    End Sub
    Private Sub PT_Agregar_Funciones_PO() 'ByVal dgdTemporal As C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        Me.dgdPosterior.Columns(Col_Num).DefaultValue = ""
        Me.dgdPosterior.Columns(Col_Evaluacion).DefaultValue = ""
        Me.dgdPosterior.Columns(Col_Estado).DefaultValue = ""
        Me.dgdPosterior.Columns(Col_Concesion).DefaultValue = ""
        Me.dgdPosterior.Columns(Col_Codigo).DefaultValue = ""
    End Sub
    Private Sub PT_Forma_Grilla_PO() 'ByVal dgdTemporal As C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        dgdPosterior.Splits(0).DisplayColumns(Col_Num).Width = 20
        dgdPosterior.Splits(0).DisplayColumns(Col_Evaluacion).Width = 70
        dgdPosterior.Splits(0).DisplayColumns(Col_Estado).Width = 70
        dgdPosterior.Splits(0).DisplayColumns(Col_Concesion).Width = 175
        dgdPosterior.Splits(0).DisplayColumns(Col_Codigo).Width = 70
        dgdPosterior.Columns("NUM").Caption = "N°"
        dgdPosterior.Columns("EVALUACION").Caption = "Evaluación"
        dgdPosterior.Columns("ESTADO").Caption = "Estado"
        dgdPosterior.Columns("CONCESION").Caption = "Concesión"
        dgdPosterior.Columns("CODIGO").Caption = "Código"
        dgdPosterior.Splits(0).DisplayColumns(Col_Num).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        dgdPosterior.Splits(0).DisplayColumns(Col_Evaluacion).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        dgdPosterior.Splits(0).DisplayColumns(Col_Estado).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        dgdPosterior.Splits(0).DisplayColumns(Col_Concesion).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        dgdPosterior.Splits(0).DisplayColumns(Col_Codigo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

        dgdPosterior.Splits(0).DisplayColumns(Col_Num).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        dgdPosterior.Splits(0).DisplayColumns(Col_Evaluacion).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        dgdPosterior.Splits(0).DisplayColumns(Col_Estado).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        dgdPosterior.Splits(0).DisplayColumns(Col_Concesion).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
        dgdPosterior.Splits(0).DisplayColumns(Col_Codigo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
    End Sub
    Private Sub PT_Agregar_Funciones_SI() 'ByVal dgdTemporal As C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        Me.dgdSimultaneo.Columns(Col_Num).DefaultValue = ""
        Me.dgdSimultaneo.Columns(Col_Evaluacion).DefaultValue = ""
        Me.dgdSimultaneo.Columns(Col_Estado).DefaultValue = ""
        Me.dgdSimultaneo.Columns(Col_Concesion).DefaultValue = ""
        Me.dgdSimultaneo.Columns(Col_Codigo).DefaultValue = ""
    End Sub
    Private Sub PT_Forma_Grilla_SI() 'ByVal dgdTemporal As C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        Me.dgdSimultaneo.Splits(0).DisplayColumns(Col_Num).Width = 20
        Me.dgdSimultaneo.Splits(0).DisplayColumns(Col_Evaluacion).Width = 70
        Me.dgdSimultaneo.Splits(0).DisplayColumns(Col_Estado).Width = 70
        Me.dgdSimultaneo.Splits(0).DisplayColumns(Col_Concesion).Width = 175
        Me.dgdSimultaneo.Splits(0).DisplayColumns(Col_Codigo).Width = 70
        Me.dgdSimultaneo.Columns("NUM").Caption = "N°"
        Me.dgdSimultaneo.Columns("EVALUACION").Caption = "Evaluación"
        Me.dgdSimultaneo.Columns("ESTADO").Caption = "Estado"
        Me.dgdSimultaneo.Columns("CONCESION").Caption = "Concesión"
        Me.dgdSimultaneo.Columns("CODIGO").Caption = "Código"
        Me.dgdSimultaneo.Splits(0).DisplayColumns(Col_Num).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdSimultaneo.Splits(0).DisplayColumns(Col_Evaluacion).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdSimultaneo.Splits(0).DisplayColumns(Col_Estado).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdSimultaneo.Splits(0).DisplayColumns(Col_Concesion).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdSimultaneo.Splits(0).DisplayColumns(Col_Codigo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

        Me.dgdSimultaneo.Splits(0).DisplayColumns(Col_Num).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdSimultaneo.Splits(0).DisplayColumns(Col_Evaluacion).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdSimultaneo.Splits(0).DisplayColumns(Col_Estado).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdSimultaneo.Splits(0).DisplayColumns(Col_Concesion).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
        Me.dgdSimultaneo.Splits(0).DisplayColumns(Col_Codigo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
    End Sub
    Private Sub PT_Agregar_Funciones_EX() 'ByVal dgdTemporal As C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        Me.dgdExtinguido.Columns(Col_Num).DefaultValue = ""
        Me.dgdExtinguido.Columns(Col_Evaluacion).DefaultValue = ""
        Me.dgdExtinguido.Columns(Col_Estado).DefaultValue = ""
        Me.dgdExtinguido.Columns(Col_Concesion).DefaultValue = ""
        Me.dgdExtinguido.Columns(Col_Codigo).DefaultValue = ""
    End Sub
    Private Sub PT_Agregar_Funciones_RD() 'ByVal dgdTemporal As C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        Me.dgdRedenuncio.Columns(Col_Num).DefaultValue = ""
        Me.dgdRedenuncio.Columns(Col_Evaluacion).DefaultValue = ""
        Me.dgdRedenuncio.Columns(Col_Estado).DefaultValue = ""
        Me.dgdRedenuncio.Columns(Col_Concesion).DefaultValue = ""
        Me.dgdRedenuncio.Columns(Col_Codigo).DefaultValue = ""
    End Sub
    Private Sub PT_Forma_Grilla_EX() 'ByVal dgdTemporal As C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        Me.dgdExtinguido.Splits(0).DisplayColumns(Col_NUM).Width = 20
        Me.dgdExtinguido.Splits(0).DisplayColumns(Col_Evaluacion).Width = 70
        Me.dgdExtinguido.Splits(0).DisplayColumns(Col_Estado).Width = 70
        Me.dgdExtinguido.Splits(0).DisplayColumns(Col_Concesion).Width = 175
        Me.dgdExtinguido.Splits(0).DisplayColumns(Col_Codigo).Width = 70
        Me.dgdExtinguido.Columns("NUM").Caption = "N°"
        Me.dgdExtinguido.Columns("EVALUACION").Caption = "Evaluación"
        Me.dgdExtinguido.Columns("ESTADO").Caption = "Estado"
        Me.dgdExtinguido.Columns("CONCESION").Caption = "Concesión"
        Me.dgdExtinguido.Columns("CODIGO").Caption = "Código"
        Me.dgdExtinguido.Splits(0).DisplayColumns(Col_Num).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdExtinguido.Splits(0).DisplayColumns(Col_Evaluacion).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdExtinguido.Splits(0).DisplayColumns(Col_Estado).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdExtinguido.Splits(0).DisplayColumns(Col_Concesion).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdExtinguido.Splits(0).DisplayColumns(Col_Codigo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

        Me.dgdExtinguido.Splits(0).DisplayColumns(Col_Num).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdExtinguido.Splits(0).DisplayColumns(Col_Evaluacion).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdExtinguido.Splits(0).DisplayColumns(Col_Estado).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdExtinguido.Splits(0).DisplayColumns(Col_Concesion).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
        Me.dgdExtinguido.Splits(0).DisplayColumns(Col_Codigo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
    End Sub
    Private Sub PT_Forma_Grilla_RD() 'ByVal dgdTemporal As C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        Me.dgdRedenuncio.Splits(0).DisplayColumns(Col_Num).Width = 20
        Me.dgdRedenuncio.Splits(0).DisplayColumns(Col_Evaluacion).Width = 70
        Me.dgdRedenuncio.Splits(0).DisplayColumns(Col_Estado).Width = 70
        Me.dgdRedenuncio.Splits(0).DisplayColumns(Col_Concesion).Width = 175
        Me.dgdRedenuncio.Splits(0).DisplayColumns(Col_Codigo).Width = 70
        Me.dgdRedenuncio.Columns("NUM").Caption = "N°"
        Me.dgdRedenuncio.Columns("EVALUACION").Caption = "Evaluación"
        Me.dgdRedenuncio.Columns("ESTADO").Caption = "Estado"
        Me.dgdRedenuncio.Columns("CONCESION").Caption = "Concesión"
        Me.dgdRedenuncio.Columns("CODIGO").Caption = "Código"
        Me.dgdRedenuncio.Splits(0).DisplayColumns(Col_Num).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdRedenuncio.Splits(0).DisplayColumns(Col_Evaluacion).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdRedenuncio.Splits(0).DisplayColumns(Col_Estado).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdRedenuncio.Splits(0).DisplayColumns(Col_Concesion).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdRedenuncio.Splits(0).DisplayColumns(Col_Codigo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdRedenuncio.Splits(0).DisplayColumns(Col_Num).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdRedenuncio.Splits(0).DisplayColumns(Col_Evaluacion).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdRedenuncio.Splits(0).DisplayColumns(Col_Estado).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdRedenuncio.Splits(0).DisplayColumns(Col_Concesion).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
        Me.dgdRedenuncio.Splits(0).DisplayColumns(Col_Codigo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
    End Sub



    Private Sub dgdPrioritario_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgdPrioritario.DoubleClick
        Dim lostrCodigou As String = Me.dgdPrioritario.Item(Me.dgdPrioritario.Row, 4)
        Busca_Codigo_DM(lostrCodigou)
    End Sub
    Private Sub Tabdatos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tabdatos.SelectedIndexChanged
        Busca_Codigo_DM("")
        Select Case Tabdatos.SelectedIndex
            Case 0
                txtValor.Text = "D.M. Prioritario (" & Me.dgdPrioritario.BindingContext(Me.dgdPrioritario.DataSource, Me.dgdPrioritario.DataMember).Count & ")"
            Case 1
                txtValor.Text = "D.M. Posterior (" & Me.dgdPosterior.BindingContext(Me.dgdPosterior.DataSource, Me.dgdPosterior.DataMember).Count & ")"
            Case 2
                txtValor.Text = "D.M. Simultaneo (" & Me.dgdSimultaneo.BindingContext(Me.dgdSimultaneo.DataSource, Me.dgdSimultaneo.DataMember).Count & ")"
            Case 3
                txtValor.Text = "D.M. Extinguido (" & Me.dgdExtinguido.BindingContext(Me.dgdExtinguido.DataSource, Me.dgdExtinguido.DataMember).Count & ")"
            Case 4
                txtValor.Text = "D.M. Antecesor (" & Me.dgdRedenuncio.BindingContext(Me.dgdRedenuncio.DataSource, Me.dgdRedenuncio.DataMember).Count & ")"
        End Select
    End Sub
    Private Sub Busca_Codigo_DM(ByVal p_Codigo As String)
        Dim cls_Catastro As New cls_DM_1
        cls_Catastro.DefinitionExpression_Campo("CODIGOU = '" & p_Codigo & "'", papp, "Catastro")
    End Sub
    Private Sub dgdPosterior_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgdPosterior.DoubleClick
        Dim lostrCodigou As String = Me.dgdPosterior.Item(Me.dgdPosterior.Row, 4)
        Busca_Codigo_DM(lostrCodigou)
    End Sub
    Private Sub dgdSimultaneo_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgdSimultaneo.DoubleClick
        Dim lostrCodigou As String = Me.dgdPrioritario.Item(Me.dgdPrioritario.Row, 4)
        Busca_Codigo_DM(lostrCodigou)
    End Sub

    Private Sub dgdExtinguido_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgdExtinguido.DoubleClick
        Dim lostrCodigou As String = Me.dgdPrioritario.Item(Me.dgdPrioritario.Row, 4)
        Busca_Codigo_DM(lostrCodigou)
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub dgdSimultaneo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgdSimultaneo.Click

    End Sub

    Private Sub txtzonaurbana_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub dgdRedenuncio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgdRedenuncio.Click

    End Sub

    Private Sub dgdSimultaneo_Expand(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.BandEventArgs) Handles dgdSimultaneo.Expand

    End Sub

    Private Sub dgdRedenuncio_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgdRedenuncio.DoubleClick
        Dim lostrCodigou As String = Me.dgdRedenuncio.Item(Me.dgdRedenuncio.Row, 4)
        Busca_Codigo_DM(lostrCodigou)
    End Sub

    Private Sub Frm_Resultado_Eval_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtValor.Text = ""
        Dim lodtbAN, lodtbPO, lodtbSI, lodtbEX, lodtbRD As New DataTable
        Try
            PINTAR_FORMULARIO()
            PT_Inicializar_Grilla_PR()
            PT_Inicializar_Grilla_PO()
            PT_Inicializar_Grilla_SI()
            PT_Inicializar_Grilla_EX()
            PT_Inicializar_Grilla_RD()
            Dim cls_Evaluacion As New Cls_evaluacion
            cls_Evaluacion.Inserta_Resultados_DM(lodtbAN, lodtbPO, lodtbSI, lodtbEX, lodtbRD, txtCodigo, txtNombre, papp)
            Me.dgdPrioritario.DataSource = lodtbAN
            Me.dgdPosterior.DataSource = lodtbPO
            Me.dgdSimultaneo.DataSource = lodtbSI
            Me.dgdExtinguido.DataSource = lodtbEX
            Me.dgdRedenuncio.DataSource = lodtbRD
            PT_Agregar_Funciones_PR() : PT_Forma_Grilla_PR()
            PT_Agregar_Funciones_PO() : PT_Forma_Grilla_PO()
            PT_Agregar_Funciones_SI() : PT_Forma_Grilla_SI()
            PT_Agregar_Funciones_EX() : PT_Forma_Grilla_EX()
            PT_Agregar_Funciones_RD() : PT_Forma_Grilla_RD()
            Me.txtfrontera.Text = "Distancia de la línea de frontera de " & distancia_fron & " (Km.)"
            If lista_urba = "" Then
                Me.txt_urbana.Text = "No se encuentra superpuesto a un Area urbana"
            Else
                Me.txt_urbana.Text = lista_urba
                ' lista_urba = ""
            End If
            If lista_rese = "" Then
                Me.txt_reserva.Text = "No se encuentra superpuesto a un Area de Reserva"
            Else
                Me.txt_reserva.Text = lista_rese
                ' lista_rese = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class