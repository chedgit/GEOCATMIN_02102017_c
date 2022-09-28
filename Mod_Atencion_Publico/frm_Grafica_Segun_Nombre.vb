Imports PORTAL_Clases
Imports ESRI.ArcGIS.Framework
Imports System.Drawing
Public Class frm_Grafica_Segun_Nombre
    Public m_application As IApplication
    Public p_Consulta As String = ""
    Public p_Tipo As Integer
    Private lodtbLista_DM As New DataTable
    Private Const Col_Codigo = 0
    Private Const Col_Nombre = 1
    Private Const Col_Carta = 2
    Private Const Col_Zona = 3
    Private Const Col_Tipo = 4
    Private Const Col_Naturaleza = 5
    Private Const Col_Hectarea = 6
    Private Const Col_Estado = 7

    Private Sub frm_Graficar_Segun_Nombre_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim cls_Prueba As New cls_Prueba
        Dim cls_Catastro As New cls_DM_1
        Dim cls_Oracle As New cls_Oracle
        Me.lblUsuario.Text = "Usuario: " & gstrNombre_Usuario 'gstrUsuarioAcceso
        PT_Inicializar_Grilla_DM()
        Dim lodbListaDM As New DataTable
        'cls_Catastro.PT_CargarFeatureClass_SDE("GPO_CMI_CATASTRO_MINERO_18", m_application, "1", False)
        'lodbListaDM = cls_Prueba.Consulta_DM_x_Nombre("Catastro", p_Consulta, m_application)
        'lodbListaDM = cls_Oracle.F_OBTIENE_DM_UNIQUE(p_Consulta, p_Tipo)

        Select Case lodbListaDM.Rows.Count
            Case 0
                MsgBox("No Existe ningún Registo con esta consulta " & p_Consulta, MsgBoxStyle.Information, "[BDGEOCATMIN]")
                DialogResult = Windows.Forms.DialogResult.Cancel
            Case 1
                Me.dgdDetalle.DataSource = lodbListaDM
                DialogResult = Windows.Forms.DialogResult.OK
                Exit Sub
            Case Else
                Me.dgdDetalle.DataSource = lodbListaDM
                Me.lblMensaje.Text = "Se han encontrado " & lodbListaDM.Rows.Count & " ocurrencias, seleccione una de ellas:"
                PT_Agregar_Funciones_DM() : PT_Forma_Grilla_DM()
        End Select
    End Sub
    Public Sub PT_Inicializar_Grilla_DM()
        'Dim dvwLista_DM As New DataView(lodtbLista_DM)
        lodtbLista_DM.Columns.Add("CODIGO", GetType(String))
        lodtbLista_DM.Columns.Add("NOMBRE", GetType(String))
        lodtbLista_DM.Columns.Add("CARTA", GetType(String))
        lodtbLista_DM.Columns.Add("ZONA", GetType(String))
        lodtbLista_DM.Columns.Add("TIPO", GetType(String))
        lodtbLista_DM.Columns.Add("NATURALEZA", GetType(String))
        lodtbLista_DM.Columns.Add("HECTAREA", GetType(String))
        lodtbLista_DM.Columns.Add("ESTADO", GetType(String))
        PT_Estilo_Grilla_DM(lodtbLista_DM) : PT_Cargar_Grilla_DM(lodtbLista_DM)
        PT_Agregar_Funciones_DM() : PT_Forma_Grilla_DM()
    End Sub
    Private Sub PT_Estilo_Grilla_DM(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(Col_Codigo).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Nombre).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Carta).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Zona).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Tipo).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Naturaleza).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Hectarea).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Estado).DefaultValue = ""
    End Sub
    Private Sub PT_Cargar_Grilla_DM(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdDetalle.DataSource = dvwDetalle
        Pinta_Grilla_Dm()
    End Sub
    Public Sub Pinta_Grilla_Dm()
        Me.dgdDetalle.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdDetalle.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        Me.dgdDetalle.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        Me.dgdDetalle.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
    End Sub
    Private Sub PT_Agregar_Funciones_DM()
        Me.dgdDetalle.Columns(Col_Codigo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Nombre).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Carta).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Zona).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Tipo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Naturaleza).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Hectarea).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Estado).DefaultValue = ""
    End Sub
    Private Sub PT_Forma_Grilla_DM()
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Width = 200
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Carta).Width = 60
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Zona).Width = 40
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Tipo).Width = 40
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Naturaleza).Width = 60
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Hectarea).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Estado).Width = 40
        Me.dgdDetalle.Columns("CODIGO").Caption = "Código"
        Me.dgdDetalle.Columns("NOMBRE").Caption = "Concesión"
        Me.dgdDetalle.Columns("CARTA").Caption = "Hoja IGN"
        Me.dgdDetalle.Columns("ZONA").Caption = "Zona"
        Me.dgdDetalle.Columns("TIPO").Caption = "Tipo"
        Me.dgdDetalle.Columns("NATURALEZA").Caption = "Naturaleza"
        Me.dgdDetalle.Columns("HECTAREA").Caption = "Hectarea"
        Me.dgdDetalle.Columns("ESTADO").Caption = "Estado"
        'Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Locked = True
        'Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Locked = True
        'Me.dgdDetalle.Splits(0).DisplayColumns(Col_Tipo).Locked = True
        'Me.dgdDetalle.Splits(0).DisplayColumns(Col_Carta).Locked = True
        'Me.dgdDetalle.Splits(0).DisplayColumns(Col_Situacion).Locked = True
        'Me.dgdDetalle.Splits(0).DisplayColumns(Col_Titular).Locked = True
        'Me.dgdDetalle.Splits(0).DisplayColumns("COD_SERVICIO").AllowSizing = True
        'Me.dgdDetalle.Splits(0).DisplayColumns("COD_SERVICIO").Style.WrapText = True
        'Me.dgdDetalle.Splits(0).DisplayColumns("DESCRIPCION").AllowSizing = True
        'Me.dgdDetalle.Splits(0).DisplayColumns("DESCRIPCION").Style.WrapText = True
        'Me.dgdDetalle.Splits(0).DisplayColumns("OBSERVACION").AllowSizing = True
        'Me.dgdDetalle.Splits(0).DisplayColumns("OBSERVACION").Style.WrapText = True
        'Me.dgd_Orden_Trabajo.Splits(0).AllowRowSizing = RowSizingEnum.AllRows
        'Me.dgd_Orden_Trabajo.AllowRowSizing = RowSizingEnum.IndividualRows
        'Me.dgd_Orden_Trabajo.Splits(0).Rows(0).Height = 50
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Carta).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Zona).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Tipo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Naturaleza).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Hectarea).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Estado).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Zona).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Carta).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Tipo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Naturaleza).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Hectarea).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Estado).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
    End Sub
    Private Sub dgdDetalle_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgdDetalle.DoubleClick
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnGraficar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGraficar.Click
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
End Class