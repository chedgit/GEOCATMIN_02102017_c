Imports System.data
Imports System.IO
Imports System.Collections
Imports PORTAL_Clases
Imports ESRI.ArcGIS.Framework
Imports System.Drawing
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.DataSourcesOleDB
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.GISClient
Imports ESRI.ArcGIS.Display
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Carto
Imports stdole
Imports Oracle.DataAccess.Client


'Imports ESRI.ArcGIS.Geometry
'Imports ESRI.ArcGIS.ArcMapUI

'Imports ESRI.ArcGIS.Output

'Imports ESRI.ArcGIS.Editor
'Imports ESRI.ArcGIS.Catalog
'Imports ESRI.ArcGIS.DataSourcesFile

'Imports ESRI.ArcGIS.DataSourcesRaster
'Imports ESRI.ArcGIS.CartoUI
'Imports ESRI.ArcGIS.GeoDatabaseUI



Public Class Frm_datos_Evaluacion
    Public m_application As IApplication
    Private cls_Catastro As New cls_DM_1
    Private Const Col_Sel_R = 0
    Private Const Col_conta = 1
    Private Const Col_Numero = 2
    Private Const Col_Codigo = 3
    Private Const Col_Nombre = 4
    Private Const Col_priori = 5
    Private Const Col_area = 6
    Private Const Col_tipo = 7
    Private Const Col_estado = 8

    Private Sub Frm_datos_Evaluacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.cboInforme.SelectedIndex = 0
        Dim cls_Oracle As New cls_Oracle
        Dim loContadorSGD As Integer, loContadorSGH As Integer
        INICIALIZAR_TIPO_INFORME()

        CARGAR_FORMULARIO_INICIAL()
        Dim lodtbDATO As New DataTable
        lodtbDATO = cls_Oracle.FT_VERIFICA_ESTADO_IT("SG_D_EVALGIS", v_codigo)
        If lodtbDATO.Rows.Count = 1 Then
            cboInforme.SelectedValue = lodtbDATO.Rows(0).Item("EG_FORMAT")
            v_Informe = cboInforme.SelectedValue
        Else
            cboInforme.SelectedIndex = 0
        End If
        lodtbDATO = cls_Oracle.FT_CONTADOR_IT("SG_D_EVALGIS", v_codigo)
        loContadorSGD = Integer.Parse(lodtbDATO.Rows(0).Item("CANTIDAD"))
        lodtbDATO = cls_Oracle.FT_CONTADOR_IT("SG_H_EVALGIS", v_codigo)
        loContadorSGH = Integer.Parse(lodtbDATO.Rows(0).Item("CANTIDAD"))
        'loContadorSGH = 1

        'loContadorSGD = 1
        If loContadorSGD + loContadorSGH > 0 Then
            lblRInforme.Text = "Existe: " & loContadorSGD & " Informe"
            If loContadorSGH = 0 Then
                lblRHistorico.Text = "No Existen Informes Históricos" ': " & loContadorSGD + loContadorSGH
            Else
                lblRHistorico.Text = "Existe: " & loContadorSGH & " Informe(s) Históricos" ': " & loContadorSGD + loContadorSGH
            End If

            'lblResultado.Text = "El Código: " & v_codigo & " tiene " & loContadorSGD + loContadorSGH & " versiones."
        Else
            lblRInforme.Text = "Nuevo Informe"
            lblRHistorico.Text = ""
        End If
    End Sub
    Private Sub CARGAR_FORMULARIO_INICIAL()
        conta_q = 0
        validad_rio = False

        Me.txt_posible_Au.Visible = False
        v_checkbox1 = "" : v_checkbox2 = "" : v_checkbox3 = "" : v_checkbox4 = "" : v_checkbox5 = ""
        v_checkbox6 = "" : v_checkbox7 = "" : v_checkbox8 = "" : v_checkbox9 = "" : v_checkbox10 = ""
        v_checkbox11 = "" : v_checkbox12 = "" : v_checkbox13 = "" : v_checkbox14 = "" : v_checkbox15 = ""
        v_checkbox16 = "" : v_checkbox17 = "" : v_checkbox18 = "" : v_checkbox19 = "" : v_checkbox20 = ""
        If validad_carr = True Then
            Me.Check_dato5.Checked = True
            Me.Check_dato6.Checked = True
        End If
        If validad_rio = True Then
            Me.Check_dato11.Checked = True
        End If
        Me.Txtpaises.Text = validad_paises
        Me.txtfrontera.Text = distancia_fron & " (Km.)"
        Me.txt_reserva.Text = lista_rese
        Me.txt_urbana.Text = lista_urba
        Me.txtCodigo.Text = v_codigo
        Me.txtNombre.Text = v_nombre_dm
        Dim lodtbDatos As New DataTable
        Dim pFeatureCursor As IFeatureCursor
        Dim pfeatureclas As IFeatureClass
        Dim pfeature As IFeature
        Dim criterio As String = ""
        Dim consulta1 As IQueryFilter
        consulta1 = New QueryFilter
        Dim conta As Integer = 0
        Dim dRow As DataRow

        lodtbDatos.Columns.Add("SELEC", Type.GetType("System.String"))
        lodtbDatos.Columns.Add("CONTADOR", Type.GetType("System.String"))
        lodtbDatos.Columns.Add("NUM.", Type.GetType("System.String"))
        lodtbDatos.Columns.Add("CODIGO", Type.GetType("System.String"))
        lodtbDatos.Columns.Add("NOMBRE", Type.GetType("System.String"))
        lodtbDatos.Columns.Add("PRIORIDAD", Type.GetType("System.String"))
        lodtbDatos.Columns.Add("AREA_INT", Type.GetType("System.Double"))
        lodtbDatos.Columns.Add("TIPO_EXP", Type.GetType("System.String"))
        lodtbDatos.Columns.Add("ESTADO", Type.GetType("System.String"))

        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Catastro" Then
                pLayer = pMap.Layer(A)
                aFound = True
                Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("No existe Capa de Catastro Minero", MsgBoxStyle.Information, "SIGCATMIN")
            DialogResult = Windows.Forms.DialogResult.Cancel
            Exit Sub
        End If



        pMap = pMxDoc.FocusMap
        If pLayer.Name = "Catastro" Then
            pFeatureLayer = pLayer
            pfeatureclas = pFeatureLayer.FeatureClass
            For t As Integer = 1 To 6
                If t = 1 Then
                    criterio = "PR"
                    conta = 0
                ElseIf t = 2 Then
                    criterio = "PO"
                    conta = 0
                ElseIf t = 3 Then
                    criterio = "SI"
                    conta = 0
                ElseIf t = 4 Then
                    criterio = "EX"
                    conta = 0
                ElseIf t = 5 Then
                    criterio = "RD"
                    conta = 0
                ElseIf t = 6 Then
                    criterio = "CO"
                    conta = 0
                End If
                consulta1.WhereClause = "EVAL = '" & criterio & "'"
                pFeatureCursor = pfeatureclas.Update(consulta1, False)
                pfeature = pFeatureCursor.NextFeature
                Do Until pfeature Is Nothing
                    conta = conta + 1
                    dRow = lodtbDatos.NewRow
                    dRow.Item("CONTADOR") = conta
                    dRow.Item("NUM.") = pfeature.Value(pfeature.Fields.FindField("CONTADOR"))
                    dRow.Item("CODIGO") = pfeature.Value(pfeature.Fields.FindField("CODIGOU"))
                    dRow.Item("NOMBRE") = pfeature.Value(pfeature.Fields.FindField("CONCESION"))
                    dRow.Item("PRIORIDAD") = pfeature.Value(pfeature.Fields.FindField("EVAL"))
                    dRow.Item("AREA_INT") = pfeature.Value(pfeature.Fields.FindField("AREA_INT"))
                    dRow.Item("TIPO_EXP") = pfeature.Value(pfeature.Fields.FindField("TIPO_EX"))
                    dRow.Item("ESTADO") = pfeature.Value(pfeature.Fields.FindField("ESTADO"))
                    lodtbDatos.Rows.Add(dRow)
                    pfeature = pFeatureCursor.NextFeature
                Loop
            Next t
            Me.dgdDetalle.DataSource = lodtbDatos
            PT_Estilo_Grilla_EVAL(lodtbDatos)
            PT_Agregar_Funciones_EVAL() : PT_Forma_Grilla_EVAL()
            Me.chkEstado.Checked = False : Me.chkEstado.Checked = True
            For i As Integer = 0 To Me.dgdDetalle.RowCount - 1
                If dgdDetalle.Item(i, "PRIORIDAD") <> "CO" Then
                    dgdDetalle.Item(i, "SELEC") = True
                Else
                    dgdDetalle.Item(i, "SELEC") = False
                End If
            Next
            Me.dgdDetalle.AllowUpdate = True
            '            Dim losender As New System.Object
            'Dim loe As New System.EventArgs
            dgdDetalle.Focus()
            pMxDoc.ActiveView.Refresh()
        End If
    End Sub

    Dim cnx_Oracle As New OracleConnection("Data Source=DESA;USER ID=SISGEM;PASSWORD=SISGEM")
    Dim cnx_Oracle1 As String = "Data Source=DESA;USER ID=SISGEM;PASSWORD=SISGEM"
    Private Sub INICIALIZAR_TIPO_INFORME_ANT()
        Dim cls_Oracle As New cls_Oracle
        Dim lodtbTipoInforme As New DataTable
        cls_Catastro.Pinta_Grilla_Dm(Me.dgdDetalle)
        Try
            lodtbTipoInforme = cls_Oracle.FT_SEL_LISTA_DESCRIPCION("1", "TIPOINFORME")
            'Dim lodtvCboTipo As New DataView(lodtbCboTipo)
            cboInforme.DataSource = lodtbTipoInforme

            cboInforme.DisplayMember = "DESCRIPCION"
            cboInforme.ValueMember = "CODIGO"
        Catch ex As Exception
        End Try
    End Sub

    Private Sub INICIALIZAR_TIPO_INFORME()
        Dim cls_Oracle As New cls_Oracle
        Dim lodtbTipoInforme As New DataTable
        cls_Catastro.Pinta_Grilla_Dm(Me.dgdDetalle)
        Try
            lodtbTipoInforme = cls_Oracle.FT_SEL_LISTA_INFORMES("TIPOINFORME")
            'Dim lodtvCboTipo As New DataView(lodtbCboTipo)
            cboInforme.DataSource = lodtbTipoInforme

            ' cboInforme.DisplayMember = "DESCRIPCION"
            ' cboInforme.ValueMember = "CODIGO"
        Catch ex As Exception
        End Try
    End Sub




    Private Sub PT_Agregar_Funciones_EVAL()
        Me.dgdDetalle.Columns(Col_Sel_R).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Numero).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Codigo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Nombre).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_priori).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_area).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_tipo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_estado).DefaultValue = ""
    End Sub
    Private Sub PT_Forma_Grilla_EVAL()
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).Width = 25
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).Width = 55
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Numero).Width = 50
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Width = 120
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_priori).Width = 65
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_area).Width = 60
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tipo).Width = 65
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_estado).Width = 65
        Me.dgdDetalle.Columns("SELEC").Caption = "Sel."
        Me.dgdDetalle.Columns("CONTADOR").Caption = "Contador"
        Me.dgdDetalle.Columns("NUM.").Caption = "Num."
        Me.dgdDetalle.Columns("CODIGO").Caption = "Código"
        Me.dgdDetalle.Columns("NOMBRE").Caption = "Nombre"
        Me.dgdDetalle.Columns("PRIORIDAD").Caption = "Prioridad"
        Me.dgdDetalle.Columns("AREA_INT").Caption = "Area Int."
        Me.dgdDetalle.Columns("TIPO_EXP").Caption = "Tipo Exp."
        Me.dgdDetalle.Columns("ESTADO").Caption = "Estado"
        Me.dgdDetalle.Splits(0).HeadingStyle.ForeColor = Color.Red

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Numero).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_area).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tipo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tipo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_estado).Locked = True

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Numero).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_priori).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_area).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tipo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_estado).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Numero).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_priori).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_area).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tipo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_estado).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
    End Sub

    Private Sub PT_Cargar_Grilla_EVAL(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdDetalle.DataSource = dvwDetalle
        Me.dgdDetalle.Columns(Col_Sel_R).Value = True
    End Sub

    Private Sub PT_Estilo_Grilla_EVAL(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(Col_Sel_R).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_conta).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Numero).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Codigo).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Nombre).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_priori).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_area).DefaultValue = 0
        padtbDetalle.Columns.Item(Col_tipo).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_estado).DefaultValue = ""
    End Sub

    Private Sub chkEstado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEstado.CheckedChanged
        Dim items As C1.Win.C1TrueDBGrid.ValueItems = Me.dgdDetalle.Columns("SELEC").ValueItems
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

    Private Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActualizar.Click
        Try
            conta_q = 0
            cls_Catastro.Consulta_atributos(m_application, Me.dgdDetalle)
        Catch ex As Exception
            MsgBox("No ha terminado el proceso correctamente", vbCritical, "Observacion  ")
        End Try
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim cls_Oracle As New cls_Oracle
        Dim lostrOpcion As String = ""
        Dim lostrVerifica As String = ""
        Dim lostrElimina As String = ""
        Dim vCodEVACheck As String = ""
        Dim vCodEVACheck1 As String = ""
        ' If cboInforme.SelectedValue = " " Then
        'MsgBox("Seleccione Tipo de Informe: ....", vbExclamation, "Evaluación de DM...")
        'cboInforme.Focus()
        'Exit Sub
        'End If
        glo_InformeDM = cboInforme.SelectedValue
        Dim val_obs_carta As String

        'lostrVerifica = cls_Oracle.FT_VERIFICA_CODIGO(v_codigo, cboInforme.SelectedValue, gstrUsuarioAcceso, gstrUsuarioClave, gstrDatabase)
        'If lostrVerifica = 1 Then
        '    lostrOpcion = MsgBox("¿ Información ya existe.. Desea Generar su Histórico .. ?" & vbNewLine & "Si:  Generar Histórico" & vbNewLine & "No:  Actualizar Información", MsgBoxStyle.YesNoCancel, "BDGEOCATMIN - Histórico")
        'End If
        'Select Case lostrOpcion
        '    Case "2" 'Cancel
        '        Exit Sub
        '    Case "6" 'Si, Generar Histórico y Grabar la Información.
        '        lostrVerifica = cls_Oracle.FT_GENERA_HISTORICO(v_codigo, cboInforme.SelectedValue, gstrUsuarioAcceso, gstrUsuarioClave, gstrDatabase)
        '    Case "7" 'No, Actualizar la Información.


        'lostrOpcion = MsgBox("¿ Desea Actualizar la Información.. ?", MsgBoxStyle.YesNo, "BDGEOCATMIN - Actualizar")
        'Select Case lostrOpcion
        '    Case "7" 'Cancel
        'Exit Sub
        '    Case "6" 'Si, Generar Histórico y Grabar la Información.
        'lostrVerifica = cls_Oracle.FT_GENERA_HISTORICO(v_codigo, cboInforme.SelectedValue, gstrUsuarioAcceso, gstrUsuarioClave, gstrDatabase)
        'lostrElimina = cls_Oracle.FT_ELIMINA_INFORME_TECNICO(v_codigo, cboInforme.SelectedValue, gstrUsuarioAcceso, gstrUsuarioClave, gstrDatabase)
        'End Select

        ' Dim clsWUrbina As New cls_BDEvaluacion
        Dim vCodEVA As String = "", vPrioridad As String = "", vCodigou As String = "", lostrAreaInt As String = ""
        For w As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
            Select Case Me.dgdDetalle.Item(w, "PRIORIDAD")
                Case "CO"
                    'vCodEVA = vCodEVA & w + 1 & " | " & Me.dgdDetalle.Item(w, "CODIGO") & " || "
                Case "PR", "SI", "PO", "EX", "RD"
                    vCodEVA = vCodEVA & w + 1 & " | " & Me.dgdDetalle.Item(w, "CODIGO") & " || "
                    vPrioridad = vPrioridad & w + 1 & " | " & Me.dgdDetalle.Item(w, "PRIORIDAD") & " || "
                    lostrAreaInt = lostrAreaInt & w + 1 & " | " & Me.dgdDetalle.Item(w, "AREA_INT") & " || "
            End Select
        Next
        Dim ListCheckBox As List(Of Windows.Forms.CheckBox)
        ListCheckBox = New List(Of Windows.Forms.CheckBox)
        ListCheckBox.Add(Check_dato1) : ListCheckBox.Add(Check_dato2)
        ListCheckBox.Add(Check_dato3) : ListCheckBox.Add(Check_dato4)
        ListCheckBox.Add(Check_dato5) : ListCheckBox.Add(Check_dato6)
        ListCheckBox.Add(Check_dato7) : ListCheckBox.Add(Check_dato8)
        ListCheckBox.Add(Check_dato9) : ListCheckBox.Add(Check_dato10)
        ListCheckBox.Add(Check_dato11) : ListCheckBox.Add(Check_dato12)
        ListCheckBox.Add(Check_dato13) : ListCheckBox.Add(Check_dato14)
        ListCheckBox.Add(Check_dato15) : ListCheckBox.Add(Check_dato16)
        ListCheckBox.Add(Check_dato17) : ListCheckBox.Add(Check_dato18)
        ListCheckBox.Add(Check_dato19) : ListCheckBox.Add(Check_dato20)
        ' If CType(sender, Windows.Forms.CheckBox).Checked Then
        Dim lostrCheck As String = "", lostrTipo As String = ""
        vCodEVACheck = ""
        Dim conta As Integer = 1
        For Each chkbox As Windows.Forms.CheckBox In ListCheckBox  'Cuando es observaciones Carta
            If chkbox.Checked Then ' AndAlso CType(sender, Windows.Forms.CheckBox).Name <> chkbox.Name Then
                Select Case chkbox.Name
                    Case "Check_dato1"
                        lostrCheck = lostrCheck & conta & " | " & "GT" & " || " 'Zona Agricola Total
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                    Case "Check_dato2"
                        lostrCheck = lostrCheck & conta & " | " & "GP" & " || " 'Zona Agricola Parcial
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                    Case "Check_dato3"
                        lostrCheck = lostrCheck & conta & " | " & "MT" & " || " 'Dominio Maritimo Total
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                    Case "Check_dato4"
                        lostrCheck = lostrCheck & conta & " | " & "MP" & " || " 'Dominio Maritimo Parcial
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                    Case "Check_dato5"
                        lostrCheck = lostrCheck & conta & " | " & "CA" & " || " 'Carretera Asfaltada
                        lostrTipo = lostrTipo & conta & " | " & "CA" & " || " 'Carretera Asfaltada
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                    Case "Check_dato6"
                        lostrCheck = lostrCheck & conta & " | " & "CF" & " || " 'Carretera Afirmada
                        lostrTipo = lostrTipo & conta & " | " & "CF" & " || " 'Carretera Afirmada
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                    Case "Check_dato7"
                        lostrCheck = lostrCheck & conta & " | " & "BT" & " || " 'Zona Bosque Total
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                    Case "Check_dato8"
                        lostrCheck = lostrCheck & conta & " | " & "BP" & " || " 'Zona Bosque Parcial
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                    Case "Check_dato9"
                        lostrCheck = lostrCheck & conta & " | " & "RT" & " || " 'Zona de Recubimiento Aerofotografico Total
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                    Case "Check_dato10"
                        lostrCheck = lostrCheck & conta & " | " & "RP" & " || " 'Zona de Recubimiento Aerofotografico Parcial
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                    Case "Check_dato11"
                        lostrCheck = lostrCheck & conta & " | " & "HD" & " || " 'Rio
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & Txtrio.Text & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        'Validad si ingreso nombre del Rio
                        If Txtrio.Text = "" Then
                            MsgBox("Debe de Ingresar el Nombre del Rio", MsgBoxStyle.Critical, "Observación..")
                            Exit Sub

                        End If
                    Case "Check_dato12"
                        lostrCheck = lostrCheck & conta & " | " & "CL" & " || " 'Canal
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                    Case "Check_dato13"
                        lostrCheck = lostrCheck & conta & " | " & "LG" & " || " 'Lagunas
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & Txtlaguna.Text & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        If Txtlaguna.Text = "" Then
                            MsgBox("Debe de Ingresar el Nombre de la Laguna", MsgBoxStyle.Critical, "Observación..")
                            Exit Sub

                        End If
                    Case "Check_dato14"
                        lostrCheck = lostrCheck & conta & " | " & "RS" & " || " 'Reservorio
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                    Case "Check_dato15"
                        lostrCheck = lostrCheck & conta & " | " & "FE" & " || " 'Linea Ferrea
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                    Case "Check_dato16"
                        lostrCheck = lostrCheck & conta & " | " & "TE" & " || " 'Linea de Alta tensión Eleéctrica
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                    Case "Check_dato17"
                        lostrCheck = lostrCheck & conta & " | " & "RQ" & " || " 'Restos Arqueológicos
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                    Case "Check_dato18"
                        lostrCheck = lostrCheck & conta & " | " & "TL" & " || " 'Zona de Traslape
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                    Case "Check_dato19"
                        lostrCheck = lostrCheck & conta & " | " & "FR" & " || " 'Linea de Frontera
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                    Case "Check_dato20"
                        lostrCheck = lostrCheck & conta & " | " & "PU" & " || " 'Posible Area Urbana
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & txt_posible_Au.Text & "||"
                        val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        If txt_posible_Au.Text = "" Then
                            MsgBox("Debe de Ingresar el Nombre de Posible Area Urbana", MsgBoxStyle.Critical, "Observación..")
                            Exit Sub

                        End If
                End Select
                conta = conta + 1
            End If
        Next
        Dim lostrRetorno_cuenta As String = ""
        Dim lostrRetorno As String = ""
        'lostrRetorno = cls_Oracle.FT_SG_D_EVALGIS("INS", txtCodigo.Text, cboInforme.SelectedValue, gstrCodigo_Usuario)
        lostrRetorno_cuenta = cls_Oracle.FT_SG_CUENTA_REG_IN_EVALGIS("SG_D_EVALGIS", v_codigo, cboInforme.SelectedValue, "", "")

        '*****OJO BORRADOR

        'Dim pRow As IRow
        'pWorkspaceFactory = New ShapefileWorkspaceFactory
        'pFeatureWorkspace = pWorkspaceFactory.OpenFromFile("C:\temp\", 0)
        'pTable = pFeatureWorkspace.OpenTable("RESUL5")
        'Dim ptableCursor As ICursor
        'Dim pfields As Fields
        'pfields = pTable.Fields

        'ptableCursor = pTable.Search(Nothing, True)
        'pRow = ptableCursor.NextRow
        'Dim lostrRetornoX As String = ""
        'pRow = ptableCursor.NextRow
        'Do Until pRow Is Nothing
        '    campo1 = pRow.Value(pfields.FindField("CG_CODIGO"))
        '    campo2 = pRow.Value(pfields.FindField("AN_FECIMA"))
        '    campo3 = pRow.Value(pfields.FindField("AN_IMAGEN"))
        '    campo4 = "SISGEM"
        '    lostrRetornoX = cls_Oracle.FT_SG_IMAGEN(campo1, campo2, campo3, campo4)
        '    pRow = ptableCursor.NextRow
        'Loop
        'MsgBox("TERMINO")

        'Exit Sub
        '----TERMINO BORRADOR


        'If lostrRetorno = 1 Then
        If lostrRetorno_cuenta = 0 Then
            var_fa_tipoactualiza = False  'Indicador Solo ingreso para utilizar en Areas Superpuestas
            lostrRetorno = cls_Oracle.FT_SG_D_EVALGIS("INS", v_codigo, cboInforme.SelectedValue, gstrCodigo_Usuario)
            'Prioritarios
            lostrRetorno = cls_Oracle.FT_SG_D_CARAC_EVALGIS("INS", v_codigo, cboInforme.SelectedValue, vCodEVA, vPrioridad, "", "", "", gstrUsuarioAcceso)
            'Caracteristicas checks
            If lostrCheck <> "" Then
                vCodEVA = vCodEVACheck
                'lostrRetorno = cls_Oracle.FT_SG_D_CARAC_EVALGIS("INS", txtCodigo.Text, cboInforme.SelectedValue, vCodEVA, lostrCheck, "", "", "", gstrUsuarioAcceso)
                lostrRetorno = cls_Oracle.FT_SG_D_CARAC_EVALGIS("INS", v_codigo, cboInforme.SelectedValue, " 1| " & v_codigo & " || ", lostrCheck, vCodEVACheck, "", "", gstrUsuarioAcceso)

            End If
            'Limite Frontera
            If txtfrontera.Text <> "" Then

                Dim loValorFrontera As Single = Single.Parse(Mid(txtfrontera.Text, 1, InStr(txtfrontera.Text, "(") - 2))
                If loValorFrontera > 0 Then 'Solo inserta valor > 0
                    Dim lostrRetorno1 As String
                    lostrRetorno1 = cls_Oracle.FT_SG_D_CARAC_EVALGIS("INS", v_codigo, cboInforme.SelectedValue, " 1| " & v_codigo & " || ", " 1| " & "LF" & " || ", " 1| " & txtfrontera.Text & " || ", " 1|" & loValorFrontera & "||", "", gstrUsuarioAcceso)
                End If

            End If
            If Val(lostrRetorno) > 0 Then
                MsgBox("La operación se realizó exitosamente.", vbExclamation, "Evaluación de DM...")
                glo_Tool_EVA_02 = True
                var_fa_validaeval = True
            Else
                MsgBox("No se pudo completar la operación, ya existe el Código", vbExclamation, "Evaluación de DM...")
                glo_Tool_EVA_01 = True
            End If
            'ElseIf lostrRetorno = 1 Then
        ElseIf lostrRetorno_cuenta = 1 Then

            Dim lostrOpcion1 As String = ""
            Dim lostrRetorno_eli As String = ""
            Dim lostrRetorno_act As String = ""
            lostrOpcion1 = MsgBox("¿ Ya existen Datos guardados para el Informe Técnico, Haga Click en .. ?" & vbNewLine & "SI:  Para Generar Historico de la información y Reemplazar los datos existentes" & vbNewLine & "NO:  Para Guardar informacion que reemplazara los datos existentes sin Generar Información Historica", MsgBoxStyle.YesNoCancel, "Evaluacion de DM...")

            Select Case lostrOpcion1
                Case "2" 'Cancel
                    Exit Sub  'No hace nada y sale del formulario evaluador
                Case "6" 'Si, Generar Histórico y Graba la Información reemplazandola.

                    lostrRetorno_eli = cls_Oracle.FT_GENERA_HISTORICO(v_codigo, cboInforme.SelectedValue)
                    var_fa_tipoactualiza = True  'Solo Actualizacion para utilizar en Areas Superpuestas
                    var_fa_actareasup1 = True
                Case "7" 'No Genera Histotico pero si Graba la informacion reemplanzandola

                    lostrRetorno_eli = cls_Oracle.FT_SG_D_EVALGIS("DEL", v_codigo, cboInforme.SelectedValue, gstrCodigo_Usuario)
                    var_fa_tipoactualiza = True  'Solo Actualizacion para utilizar en Areas Superpuestas
                    var_fa_actareasup2 = True
            End Select

            'Volviendo a ingresar datos previamente elimiados

            Dim vCodEVA1 As String = "", vPrioridad1 As String = "", vCodigou1 As String = "", lostrAreaInt1 As String = ""
            'For t As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1

            For t As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
                Select Case Me.dgdDetalle.Item(t, "PRIORIDAD")
                    Case "CO"
                        'vCodEVA = vCodEVA & w + 1 & " | " & Me.dgdDetalle.Item(w, "CODIGO") & " || "
                    Case "PR", "SI", "PO", "EX", "RD"
                        If dgdDetalle.Item(t, "SELEC") = True Then
                            vCodEVA1 = vCodEVA1 & t + 1 & " | " & Me.dgdDetalle.Item(t, "CODIGO") & " || "
                            vPrioridad1 = vPrioridad1 & t + 1 & " | " & Me.dgdDetalle.Item(t, "PRIORIDAD") & " || "
                            lostrAreaInt1 = lostrAreaInt1 & t + 1 & " | " & Me.dgdDetalle.Item(t, "AREA_INT") & " || "
                        End If

                End Select
            Next
            Dim ListCheckBox1 As List(Of Windows.Forms.CheckBox)
            ListCheckBox1 = New List(Of Windows.Forms.CheckBox)
            ListCheckBox1.Add(Check_dato1) : ListCheckBox1.Add(Check_dato2)
            ListCheckBox1.Add(Check_dato3) : ListCheckBox1.Add(Check_dato4)
            ListCheckBox1.Add(Check_dato5) : ListCheckBox1.Add(Check_dato6)
            ListCheckBox1.Add(Check_dato7) : ListCheckBox1.Add(Check_dato8)
            ListCheckBox1.Add(Check_dato9) : ListCheckBox1.Add(Check_dato10)
            ListCheckBox1.Add(Check_dato11) : ListCheckBox1.Add(Check_dato12)
            ListCheckBox1.Add(Check_dato13) : ListCheckBox1.Add(Check_dato14)
            ListCheckBox1.Add(Check_dato15) : ListCheckBox1.Add(Check_dato16)
            ListCheckBox1.Add(Check_dato17) : ListCheckBox1.Add(Check_dato18)
            ListCheckBox1.Add(Check_dato19) : ListCheckBox1.Add(Check_dato20)
            Dim lostrCheck1 As String = "", lostrTipo1 As String = ""

            vCodEVACheck1 = ""
            Dim conta1 As Integer = 1
            For Each chkbox As Windows.Forms.CheckBox In ListCheckBox1
                If chkbox.Checked Then ' AndAlso CType(sender, Windows.Forms.CheckBox).Name <> chkbox.Name Then
                    Select Case chkbox.Name
                        Case "Check_dato1"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "GT" & " || " 'Zona Agricola Total
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        Case "Check_dato2"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "GP" & " || " 'Zona Agricola Parcial
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        Case "Check_dato3"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "MT" & " || " 'Dominio Maritimo Total
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        Case "Check_dato4"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "MP" & " || " 'Dominio Maritimo Parcial
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        Case "Check_dato5"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "CA" & " || " 'Carretera Asfaltada
                            lostrTipo1 = lostrTipo1 & conta1 & " | " & "CA" & " || " 'Carretera Asfaltada
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        Case "Check_dato6"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "CF" & " || " 'Carretera Afirmada
                            lostrTipo1 = lostrTipo1 & conta1 & " | " & "CF" & " || " 'Carretera Afirmada
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        Case "Check_dato7"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "BT" & " || " 'Zona Bosque Total
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        Case "Check_dato8"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "BP" & " || " 'Zona Bosque Parcial
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        Case "Check_dato9"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "RT" & " || " 'Zona de Recubimiento Aerofotografico Total
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        Case "Check_dato10"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "RP" & " || " 'Zona de Recubimiento Aerofotografico Parcial
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        Case "Check_dato11"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "HD" & " || " 'Rio
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & Txtrio.Text & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                            If Txtrio.Text = "" Then
                                MsgBox("Debe de Ingresar el Nombre del Rio", MsgBoxStyle.Critical, "Observación..")
                                Exit Sub

                            End If
                        Case "Check_dato12"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "CL" & " || " 'Canal
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        Case "Check_dato13"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "LG" & " || " 'Lagunas
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & Txtlaguna.Text & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                            If Txtlaguna.Text = "" Then
                                MsgBox("Debe de Ingresar el Nombre de la Laguna", MsgBoxStyle.Critical, "Observación..")
                                Exit Sub

                            End If
                        Case "Check_dato14"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "RS" & " || " 'Reservorio
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        Case "Check_dato15"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "FE" & " || " 'Linea Ferrea
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        Case "Check_dato16"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "TE" & " || " 'Linea de Alta tensión Eleéctrica
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        Case "Check_dato17"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "RQ" & " || " 'Restos Arqueológicos
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        Case "Check_dato18"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "TL" & " || " 'Zona de Traslape
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        Case "Check_dato19"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "FR" & " || " 'Linea de Frontera
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                        Case "Check_dato20"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "AU" & " || " 'Posible Area Urbana
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & txt_posible_Au.Text & "||"
                            val_obs_carta = val_obs_carta & conta & " | " & "OB" & " || "
                            If txt_posible_Au.Text = "" Then
                                MsgBox("Debe de Ingresar el Nombre del Posible Area Urbana", MsgBoxStyle.Critical, "Observación..")
                                Exit Sub

                            End If
                    End Select
                    conta1 = conta1 + 1
                End If
            Next

            If lostrRetorno_eli = 1 Then
                lostrRetorno_act = cls_Oracle.FT_SG_D_EVALGIS("ACT", v_codigo, cboInforme.SelectedValue, gstrCodigo_Usuario)

                'Prioritarios
                lostrRetorno_act = cls_Oracle.FT_SG_D_CARAC_EVALGIS("ACT", v_codigo, cboInforme.SelectedValue, vCodEVA1, vPrioridad1, "", "", "", gstrUsuarioAcceso)
                'Caracteristicas checks
                If lostrCheck <> "" Then

                    vCodEVA = vCodEVACheck
                    'lostrRetorno_act = cls_Oracle.FT_SG_D_CARAC_EVALGIS("ACT", txtCodigo.Text, cboInforme.SelectedValue, vCodEVA1, lostrCheck1, "", "", "", gstrUsuarioAcceso)
                    'lostrRetorno_act = cls_Oracle.FT_SG_D_CARAC_EVALGIS("ACT", v_codigo, cboInforme.SelectedValue, " 1| " & v_codigo & " || ", lostrCheck1, vCodEVACheck, "", "", gstrUsuarioAcceso)
                    lostrRetorno_act = cls_Oracle.FT_SG_D_CARAC_EVALGIS("ACT", v_codigo, cboInforme.SelectedValue, " 1| " & v_codigo & " || ", lostrCheck1, vCodEVACheck, "", val_obs_carta, gstrUsuarioAcceso)

                End If
                'Limite Frontera
                If txtfrontera.Text <> "" Then

                    Dim loValorFrontera As Single = Single.Parse(Mid(txtfrontera.Text, 1, InStr(txtfrontera.Text, "(") - 2))
                    If loValorFrontera > 0 Then 'Solo inserta valor > 0
                        Dim lostrRetorno1 As String
                        lostrRetorno1 = cls_Oracle.FT_SG_D_CARAC_EVALGIS("ACT", v_codigo, cboInforme.SelectedValue, " 1| " & v_codigo & " || ", " 1| " & "LF" & " || ", " 1| " & txtfrontera.Text & " || ", " 1|" & loValorFrontera & "||", "", gstrUsuarioAcceso)
                    End If
                End If
                If Val(lostrRetorno_eli) > 0 Then
                    MsgBox("Se ha Actualizado Correctamente la Información a la Dase de Datos.", vbExclamation, "Evaluación de DM...")
                    glo_Tool_EVA_02 = True
                    var_fa_validaeval = True
                Else
                    MsgBox("No se pudo completar la operación, ya existe el Código", vbExclamation, "Evaluación de DM...")
                    glo_Tool_EVA_01 = True
                End If
            End If
            'End Select

        End If
    End Sub

    Private Sub dgdDetalle_AfterColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgdDetalle.AfterColUpdate
        Dim v_priori As String
        Dim v_codigo As String
        v_priori = Me.dgdDetalle.Item(Me.dgdDetalle.Row, "PRIORIDAD")
        v_codigo = Me.dgdDetalle.Item(Me.dgdDetalle.Row, "CODIGO")
        Me.dgdDetalle.Item(Me.dgdDetalle.Row, "PRIORIDAD") = UCase(Me.dgdDetalle.Item(Me.dgdDetalle.Row, "PRIORIDAD")).ToString
        colecciones_indi.Add(v_codigo & v_priori)
    End Sub

    Private Sub dgdDetalle_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgdDetalle.DoubleClick
        Dim lostrCodigou As String = Me.dgdDetalle.Item(Me.dgdDetalle.Row, "CODIGO")
        Busca_Codigo_DM(lostrCodigou)
    End Sub
    Private Sub dgdDetalle_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles dgdDetalle.RowColChange
        If contador_inicio > 1 Then
            v_priori_dm = Me.dgdDetalle.Item(Me.dgdDetalle.Row, "PRIORIDAD").ToString
            v_codigo_dm = Me.dgdDetalle.Item(Me.dgdDetalle.Row, "CODIGO").ToString
            If Len(v_priori_dm) > 1 Then
                Select Case v_priori_dm
                    Case "PR", "PO", "SI", "RP", "EX", "RD", "CO"
                    Case Else
                        MsgBox("No es posible considerar este indicador para modifcar resultados de evaluación", MsgBoxStyle.Information, "Observación")
                End Select
            End If
            conta_q = conta_q + 1
            Dim query_cadena1 As String
            If conta_q = 2 Then
                query_cadena1 = "CODIGOU =  '" & v_codigo_dm & "' AND EVAL =  '" & v_priori_dm & "'"
                query_cadena = query_cadena1
            ElseIf conta_q > 2 Then
                query_cadena = query_cadena & " OR " & "CODIGOU =  '" & v_codigo_dm & "' AND EVAL =  '" & v_priori_dm & "'"
            End If
        End If
    End Sub
    Private Sub Busca_Codigo_DM(ByVal p_Codigo As String)
        Dim cls_Catastro As New cls_DM_1
        cls_Catastro.DefinitionExpression_Campo("CODIGOU = '" & p_Codigo & "'", m_application, "Catastro")
    End Sub

    Private Sub Check_dato1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato1.CheckedChanged
        If Me.Check_dato1.Checked = True Then
            v_checkbox1 = Me.Check_dato1.Text
            Me.Check_dato2.Checked = False
            v_checkbox2 = ""
        Else
            v_checkbox1 = ""
        End If
    End Sub

    Private Sub Check_dato2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato2.CheckedChanged
        If Me.Check_dato2.Checked = True Then
            v_checkbox2 = Me.Check_dato2.Text
            Me.Check_dato1.Checked = False
            v_checkbox1 = ""
        Else
            v_checkbox2 = ""
        End If
    End Sub

    Private Sub Check_dato3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato3.CheckedChanged
        If Me.Check_dato3.Checked = True Then
            v_checkbox3 = Me.Check_dato3.Text
            Me.Check_dato4.Checked = False
            v_checkbox4 = ""
        Else
            v_checkbox3 = ""
        End If
    End Sub

    Private Sub Check_dato4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato4.CheckedChanged
        If Me.Check_dato4.Checked = True Then
            v_checkbox4 = Me.Check_dato4.Text
            Check_dato3.Checked = False
            v_checkbox3 = ""
        Else
            v_checkbox4 = ""
        End If
    End Sub
    Private Sub Check_dato9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.Check_dato9.Checked = True Then
            v_checkbox9 = Me.Check_dato9.Text
            Check_dato10.Checked = False
            v_checkbox10 = ""
        Else
            v_checkbox9 = ""
        End If
    End Sub

    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato7.CheckedChanged
        If Me.Check_dato7.Checked = True Then
            v_checkbox7 = Me.Check_dato7.Text
            Check_dato8.Checked = False
            v_checkbox8 = ""
        Else
            v_checkbox7 = ""
        End If
    End Sub

    Private Sub CheckBox8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.Check_dato8.Checked = True Then
            v_checkbox8 = Me.Check_dato8.Text
            Check_dato7.Checked = False
            v_checkbox7 = ""
        Else
            v_checkbox8 = ""
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato5.CheckedChanged
        If Me.Check_dato5.Checked = True Then
            v_checkbox5 = Me.Check_dato5.Text
            Check_dato6.Checked = False
            v_checkbox6 = ""
        Else
            v_checkbox5 = ""
        End If
    End Sub

    Private Sub Check_dato9_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato9.CheckedChanged
        If Me.Check_dato9.Checked = True Then
            v_checkbox9 = Me.Check_dato9.Text
            Check_dato10.Checked = False
            v_checkbox10 = ""
        Else
            v_checkbox9 = ""
        End If
    End Sub

    Private Sub Check_dato11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato11.CheckedChanged
        If Me.Check_dato11.Checked = True Then
            v_checkbox11 = Me.Check_dato11.Text
            'Me.Txtlaguna.Visible = True
            Txtrio.Visible = True
        Else
            v_checkbox11 = ""
            Me.Txtrio.Visible = False
        End If
    End Sub

    Private Sub Check_dato10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato10.CheckedChanged
        If Me.Check_dato10.Checked = True Then
            v_checkbox10 = Me.Check_dato10.Text
            Check_dato9.Checked = False
            v_checkbox9 = ""
        Else
            v_checkbox10 = ""
        End If
    End Sub

    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato6.CheckedChanged
        If Me.Check_dato6.Checked = True Then
            v_checkbox6 = Me.Check_dato6.Text
            Check_dato5.Checked = False
            v_checkbox6 = ""
        Else
            v_checkbox5 = ""
        End If
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub Check_dato13_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato13.CheckedChanged
        If Me.Check_dato13.Checked = True Then
            Me.Txtlaguna.Visible = True
        Else
            Me.Txtlaguna.Visible = False
        End If
    End Sub

    Private Sub dgdDetalle_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgdDetalle.KeyPress
        Select Case e.KeyChar.ToString
        End Select
    End Sub

    Private Sub Check_dato8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato8.CheckedChanged
        If Check_dato8.Checked = True Then
            v_checkbox8 = Check_dato8.Text
            Check_dato7.Checked = False
            v_checkbox8 = ""
        Else
            v_checkbox7 = ""
        End If
    End Sub

    Private Sub Check_dato20_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato20.CheckedChanged
        If Check_dato20.Checked = True Then
            txt_posible_Au.Visible = True
        Else
            txt_posible_Au.Text = ""
            txt_posible_Au.Visible = False
        End If
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        'Verficar si Codigo Existe
        Dim cls_Oracle As New cls_Oracle
        Dim lostrOpcion As String = ""
        Dim lostrVerifica As String = ""
        Dim lostrElimina As String = ""
        Dim vCodEVACheck As String = ""
        Dim vCodEVACheck1 As String = ""
        If cboInforme.SelectedValue = " " Then
            MsgBox("Seleccione Tipo de Informe: ....", vbExclamation, "Evaluación de DM...")
            cboInforme.Focus()
            Exit Sub
        End If
        glo_InformeDM = cboInforme.SelectedValue

        'lostrVerifica = cls_Oracle.FT_VERIFICA_CODIGO(v_codigo, cboInforme.SelectedValue, gstrUsuarioAcceso, gstrUsuarioClave, gstrDatabase)
        'If lostrVerifica = 1 Then
        '    lostrOpcion = MsgBox("¿ Información ya existe.. Desea Generar su Histórico .. ?" & vbNewLine & "Si:  Generar Histórico" & vbNewLine & "No:  Actualizar Información", MsgBoxStyle.YesNoCancel, "BDGEOCATMIN - Histórico")
        'End If
        'Select Case lostrOpcion
        '    Case "2" 'Cancel
        '        Exit Sub
        '    Case "6" 'Si, Generar Histórico y Grabar la Información.
        '        lostrVerifica = cls_Oracle.FT_GENERA_HISTORICO(v_codigo, cboInforme.SelectedValue, gstrUsuarioAcceso, gstrUsuarioClave, gstrDatabase)
        '    Case "7" 'No, Actualizar la Información.


        'lostrOpcion = MsgBox("¿ Desea Actualizar la Información.. ?", MsgBoxStyle.YesNo, "BDGEOCATMIN - Actualizar")
        'Select Case lostrOpcion
        '    Case "7" 'Cancel
        'Exit Sub
        '    Case "6" 'Si, Generar Histórico y Grabar la Información.
        'lostrVerifica = cls_Oracle.FT_GENERA_HISTORICO(v_codigo, cboInforme.SelectedValue, gstrUsuarioAcceso, gstrUsuarioClave, gstrDatabase)
        'lostrElimina = cls_Oracle.FT_ELIMINA_INFORME_TECNICO(v_codigo, cboInforme.SelectedValue, gstrUsuarioAcceso, gstrUsuarioClave, gstrDatabase)
        'End Select

        ' Dim clsWUrbina As New cls_BDEvaluacion
        Dim vCodEVA As String = "", vPrioridad As String = "", vCodigou As String = "", lostrAreaInt As String = ""
        For w As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
            Select Case Me.dgdDetalle.Item(w, "PRIORIDAD")
                Case "CO"
                    'vCodEVA = vCodEVA & w + 1 & " | " & Me.dgdDetalle.Item(w, "CODIGO") & " || "
                Case "PR", "SI", "PO", "EX", "RD"
                    vCodEVA = vCodEVA & w + 1 & " | " & Me.dgdDetalle.Item(w, "CODIGO") & " || "
                    vPrioridad = vPrioridad & w + 1 & " | " & Me.dgdDetalle.Item(w, "PRIORIDAD") & " || "
                    lostrAreaInt = lostrAreaInt & w + 1 & " | " & Me.dgdDetalle.Item(w, "AREA_INT") & " || "
            End Select
        Next
        Dim ListCheckBox As List(Of Windows.Forms.CheckBox)
        ListCheckBox = New List(Of Windows.Forms.CheckBox)
        ListCheckBox.Add(Check_dato1) : ListCheckBox.Add(Check_dato2)
        ListCheckBox.Add(Check_dato3) : ListCheckBox.Add(Check_dato4)
        ListCheckBox.Add(Check_dato5) : ListCheckBox.Add(Check_dato6)
        ListCheckBox.Add(Check_dato7) : ListCheckBox.Add(Check_dato8)
        ListCheckBox.Add(Check_dato9) : ListCheckBox.Add(Check_dato10)
        ListCheckBox.Add(Check_dato11) : ListCheckBox.Add(Check_dato12)
        ListCheckBox.Add(Check_dato13) : ListCheckBox.Add(Check_dato14)
        ListCheckBox.Add(Check_dato15) : ListCheckBox.Add(Check_dato16)
        ListCheckBox.Add(Check_dato17) : ListCheckBox.Add(Check_dato18)
        ListCheckBox.Add(Check_dato19) : ListCheckBox.Add(Check_dato20)
        ' If CType(sender, Windows.Forms.CheckBox).Checked Then
        Dim lostrCheck As String = "", lostrTipo As String = ""
        vCodEVACheck = ""
        Dim conta As Integer = 1
        For Each chkbox As Windows.Forms.CheckBox In ListCheckBox
            If chkbox.Checked Then ' AndAlso CType(sender, Windows.Forms.CheckBox).Name <> chkbox.Name Then
                Select Case chkbox.Name
                    Case "Check_dato1"
                        lostrCheck = lostrCheck & conta & " | " & "GT" & " || " 'Zona Agricola Total
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                    Case "Check_dato2"
                        lostrCheck = lostrCheck & conta & " | " & "GP" & " || " 'Zona Agricola Parcial
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                    Case "Check_dato3"
                        lostrCheck = lostrCheck & conta & " | " & "MT" & " || " 'Dominio Maritimo Total
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                    Case "Check_dato4"
                        lostrCheck = lostrCheck & conta & " | " & "MP" & " || " 'Dominio Maritimo Parcial
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                    Case "Check_dato5"
                        lostrCheck = lostrCheck & conta & " | " & "CA" & " || " 'Carretera Asfaltada
                        lostrTipo = lostrTipo & conta & " | " & "CA" & " || " 'Carretera Asfaltada
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                    Case "Check_dato6"
                        lostrCheck = lostrCheck & conta & " | " & "CF" & " || " 'Carretera Afirmada
                        lostrTipo = lostrTipo & conta & " | " & "CF" & " || " 'Carretera Afirmada
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                    Case "Check_dato7"
                        lostrCheck = lostrCheck & conta & " | " & "BT" & " || " 'Zona Bosque Total
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                    Case "Check_dato8"
                        lostrCheck = lostrCheck & conta & " | " & "BP" & " || " 'Zona Bosque Parcial
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                    Case "Check_dato9"
                        lostrCheck = lostrCheck & conta & " | " & "RT" & " || " 'Zona de Recubimiento Aerofotografico Total
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                    Case "Check_dato10"
                        lostrCheck = lostrCheck & conta & " | " & "RP" & " || " 'Zona de Recubimiento Aerofotografico Parcial
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                    Case "Check_dato11"
                        lostrCheck = lostrCheck & conta & " | " & "HD" & " || " 'Rio
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & Txtrio.Text & "||"
                        'Validad si ingreso nombre del Rio
                        If Txtrio.Text = "" Then
                            MsgBox("Debe de Ingresar el Nombre del Rio", MsgBoxStyle.Critical, "Observación..")
                            Exit Sub

                        End If
                    Case "Check_dato12"
                        lostrCheck = lostrCheck & conta & " | " & "CL" & " || " 'Canal
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                    Case "Check_dato13"
                        lostrCheck = lostrCheck & conta & " | " & "LG" & " || " 'Lagunas
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & Txtlaguna.Text & "||"
                    Case "Check_dato14"
                        lostrCheck = lostrCheck & conta & " | " & "RS" & " || " 'Reservorio
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                    Case "Check_dato15"
                        lostrCheck = lostrCheck & conta & " | " & "FE" & " || " 'Linea Ferrea
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                    Case "Check_dato16"
                        lostrCheck = lostrCheck & conta & " | " & "TE" & " || " 'Linea de Alta tensión Eleéctrica
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                    Case "Check_dato17"
                        lostrCheck = lostrCheck & conta & " | " & "RQ" & " || " 'Restos Arqueológicos
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                    Case "Check_dato18"
                        lostrCheck = lostrCheck & conta & " | " & "TL" & " || " 'Zona de Traslape
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                    Case "Check_dato19"
                        lostrCheck = lostrCheck & conta & " | " & "FR" & " || " 'Linea de Frontera
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                    Case "Check_dato20"
                        lostrCheck = lostrCheck & conta & " | " & "AU" & " || " 'Posible Area Urbana
                        vCodEVACheck = vCodEVACheck & conta + 1 & "|" & v_codigo & "||"
                End Select
                conta = conta + 1
            End If
        Next
        Dim lostrRetorno As String = ""
        lostrRetorno = cls_Oracle.FT_SG_D_EVALGIS("INS", v_codigo, cboInforme.SelectedValue, gstrCodigo_Usuario)

        '*****OJO BORRADOR

        'Dim campo1 As String
        'Dim campo2 As Date
        'Dim campo3 As String
        'Dim campo4 As String

        'Dim pRow As IRow
        'pWorkspaceFactory = New ShapefileWorkspaceFactory
        'pFeatureWorkspace = pWorkspaceFactory.OpenFromFile("C:\temp\", 0)
        'pTable = pFeatureWorkspace.OpenTable("RESUL5")
        'Dim ptableCursor As ICursor
        'Dim pfields As Fields
        'pfields = pTable.Fields

        'ptableCursor = pTable.Search(Nothing, True)
        'pRow = ptableCursor.NextRow
        'Dim lostrRetornoX As String = ""
        'pRow = ptableCursor.NextRow
        'Do Until pRow Is Nothing
        '    campo1 = pRow.Value(pfields.FindField("CG_CODIGO"))
        '    campo2 = pRow.Value(pfields.FindField("AN_FECIMA"))
        '    campo3 = pRow.Value(pfields.FindField("AN_IMAGEN"))
        '    campo4 = "SISGEM"
        '    lostrRetornoX = cls_Oracle.FT_SG_IMAGEN(campo1, campo2, campo3, campo4)
        '    pRow = ptableCursor.NextRow
        'Loop
        'MsgBox("TERMINO")

        'Exit Sub
        '----TERMINO BORRADOR


        If lostrRetorno = 1 Then
            var_fa_tipoactualiza = False  'Indicador Solo ingreso para utilizar en Areas Superpuestas

            'Prioritarios
            lostrRetorno = cls_Oracle.FT_SG_D_CARAC_EVALGIS("INS", v_codigo, cboInforme.SelectedValue, vCodEVA, vPrioridad, "", "", "", gstrUsuarioAcceso)
            'Caracteristicas checks
            If lostrCheck <> "" Then
                vCodEVA = vCodEVACheck
                'lostrRetorno = cls_Oracle.FT_SG_D_CARAC_EVALGIS("INS", txtCodigo.Text, cboInforme.SelectedValue, vCodEVA, lostrCheck, "", "", "", gstrUsuarioAcceso)
                lostrRetorno = cls_Oracle.FT_SG_D_CARAC_EVALGIS("INS", v_codigo, cboInforme.SelectedValue, " 1| " & v_codigo & " || ", lostrCheck, vCodEVACheck, "", "", gstrUsuarioAcceso)

            End If
            'Limite Frontera
            If txtfrontera.Text <> "" Then

                Dim loValorFrontera As Single = Single.Parse(Mid(txtfrontera.Text, 1, InStr(txtfrontera.Text, "(") - 2))
                If loValorFrontera > 0 Then 'Solo inserta valor > 0
                    Dim lostrRetorno1 As String
                    lostrRetorno1 = cls_Oracle.FT_SG_D_CARAC_EVALGIS("INS", v_codigo, cboInforme.SelectedValue, " 1| " & v_codigo & " || ", " 1| " & "LF" & " || ", " 1| " & txtfrontera.Text & " || ", " 1|" & loValorFrontera & "||", "", gstrUsuarioAcceso)
                End If

            End If
            If Val(lostrRetorno) > 0 Then
                MsgBox("La operación se realizó exitosamente.", vbExclamation, "Evaluación de DM...")
                glo_Tool_EVA_02 = True
                var_fa_validaeval = True
            Else
                MsgBox("No se pudo completar la operación, ya existe el Código", vbExclamation, "Evaluación de DM...")
                glo_Tool_EVA_01 = True
            End If
        ElseIf lostrRetorno = 0 Then
            Dim lostrOpcion1 As String = ""
            Dim lostrRetorno_eli As String = ""
            Dim lostrRetorno_act As String = ""
            lostrOpcion1 = MsgBox("¿ Ya existen Datos guardados para el Informe Técnico, Haga Click en .. ?" & vbNewLine & "SI:  Para Generar Historico de la información y Reemplazar los datos existentes" & vbNewLine & "NO:  Para Guardar informacion que reemplazara los datos existentes sin Generar Información Historica", MsgBoxStyle.YesNoCancel, "Evaluacion de DM...")

            Select Case lostrOpcion1
                Case "2" 'Cancel
                    Exit Sub  'No hace nada y sale del formulario evaluador
                Case "6" 'Si, Generar Histórico y Graba la Información reemplazandola.

                    lostrRetorno_eli = cls_Oracle.FT_GENERA_HISTORICO(v_codigo, cboInforme.SelectedValue)
                    var_fa_tipoactualiza = True  'Solo Actualizacion para utilizar en Areas Superpuestas
                    var_fa_actareasup1 = True
                Case "7" 'No Genera Histotico pero si Graba la informacion reemplanzandola

                    lostrRetorno_eli = cls_Oracle.FT_SG_D_EVALGIS("DEL", v_codigo, cboInforme.SelectedValue, gstrCodigo_Usuario)
                    var_fa_tipoactualiza = True  'Solo Actualizacion para utilizar en Areas Superpuestas
                    var_fa_actareasup2 = True
            End Select

            'Volviendo a ingresar datos previamente elimiados

            Dim vCodEVA1 As String = "", vPrioridad1 As String = "", vCodigou1 As String = "", lostrAreaInt1 As String = ""
            'For t As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1

            For t As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
                Select Case Me.dgdDetalle.Item(t, "PRIORIDAD")
                    Case "CO"
                        'vCodEVA = vCodEVA & w + 1 & " | " & Me.dgdDetalle.Item(w, "CODIGO") & " || "
                    Case "PR", "SI", "PO", "EX", "RD"
                        If dgdDetalle.Item(t, "SELEC") = True Then
                            vCodEVA1 = vCodEVA1 & t + 1 & " | " & Me.dgdDetalle.Item(t, "CODIGO") & " || "
                            vPrioridad1 = vPrioridad1 & t + 1 & " | " & Me.dgdDetalle.Item(t, "PRIORIDAD") & " || "
                            lostrAreaInt1 = lostrAreaInt1 & t + 1 & " | " & Me.dgdDetalle.Item(t, "AREA_INT") & " || "
                        End If

                End Select
            Next
            Dim ListCheckBox1 As List(Of Windows.Forms.CheckBox)
            ListCheckBox1 = New List(Of Windows.Forms.CheckBox)
            ListCheckBox1.Add(Check_dato1) : ListCheckBox1.Add(Check_dato2)
            ListCheckBox1.Add(Check_dato3) : ListCheckBox1.Add(Check_dato4)
            ListCheckBox1.Add(Check_dato5) : ListCheckBox1.Add(Check_dato6)
            ListCheckBox1.Add(Check_dato7) : ListCheckBox1.Add(Check_dato8)
            ListCheckBox1.Add(Check_dato9) : ListCheckBox1.Add(Check_dato10)
            ListCheckBox1.Add(Check_dato11) : ListCheckBox1.Add(Check_dato12)
            ListCheckBox1.Add(Check_dato13) : ListCheckBox1.Add(Check_dato14)
            ListCheckBox1.Add(Check_dato15) : ListCheckBox1.Add(Check_dato16)
            ListCheckBox1.Add(Check_dato17) : ListCheckBox1.Add(Check_dato18)
            ListCheckBox1.Add(Check_dato19) : ListCheckBox1.Add(Check_dato20)
            Dim lostrCheck1 As String = "", lostrTipo1 As String = ""

            vCodEVACheck1 = ""
            Dim conta1 As Integer = 1
            For Each chkbox As Windows.Forms.CheckBox In ListCheckBox1
                If chkbox.Checked Then ' AndAlso CType(sender, Windows.Forms.CheckBox).Name <> chkbox.Name Then
                    Select Case chkbox.Name
                        Case "Check_dato1"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "GT" & " || " 'Zona Agricola Total
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                        Case "Check_dato2"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "GP" & " || " 'Zona Agricola Parcial
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                        Case "Check_dato3"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "MT" & " || " 'Dominio Maritimo Total
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                        Case "Check_dato4"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "MP" & " || " 'Dominio Maritimo Parcial
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                        Case "Check_dato5"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "CA" & " || " 'Carretera Asfaltada
                            lostrTipo1 = lostrTipo1 & conta1 & " | " & "CA" & " || " 'Carretera Asfaltada
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                        Case "Check_dato6"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "CF" & " || " 'Carretera Afirmada
                            lostrTipo1 = lostrTipo1 & conta1 & " | " & "CF" & " || " 'Carretera Afirmada
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                        Case "Check_dato7"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "BT" & " || " 'Zona Bosque Total
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                        Case "Check_dato8"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "BP" & " || " 'Zona Bosque Parcial
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                        Case "Check_dato9"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "RT" & " || " 'Zona de Recubimiento Aerofotografico Total
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                        Case "Check_dato10"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "RP" & " || " 'Zona de Recubimiento Aerofotografico Parcial
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                        Case "Check_dato11"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "HD" & " || " 'Rio
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & Txtrio.Text & "||"
                            If Txtrio.Text = "" Then
                                MsgBox("Debe de Ingresar el Nombre del Rio", MsgBoxStyle.Critical, "Observación..")
                                Exit Sub

                            End If
                        Case "Check_dato12"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "CL" & " || " 'Canal
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                        Case "Check_dato13"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "LG" & " || " 'Lagunas
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & Txtlaguna.Text & "||"
                            If Txtlaguna.Text = "" Then
                                MsgBox("Debe de Ingresar el Nombre de la Laguna", MsgBoxStyle.Critical, "Observación..")
                                Exit Sub

                            End If
                        Case "Check_dato14"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "RS" & " || " 'Reservorio
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                        Case "Check_dato15"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "FE" & " || " 'Linea Ferrea
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                        Case "Check_dato16"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "TE" & " || " 'Linea de Alta tensión Eleéctrica
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                        Case "Check_dato17"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "RQ" & " || " 'Restos Arqueológicos
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                        Case "Check_dato18"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "TL" & " || " 'Zona de Traslape
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                        Case "Check_dato19"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "FR" & " || " 'Linea de Frontera
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                        Case "Check_dato20"
                            lostrCheck1 = lostrCheck1 & conta1 & " | " & "AU" & " || " 'Posible Area Urbana
                            vCodEVACheck1 = vCodEVACheck1 & conta1 + 1 & "|" & v_codigo & "||"
                            If txtCodigo.Text = "" Then
                                MsgBox("Debe de Ingresar el Nombre del Posible Area Urbana", MsgBoxStyle.Critical, "Observación..")
                                Exit Sub

                            End If
                    End Select
                    conta1 = conta1 + 1
                End If
            Next

            If lostrRetorno_eli = 1 Then
                lostrRetorno_act = cls_Oracle.FT_SG_D_EVALGIS("ACT", v_codigo, cboInforme.SelectedValue, gstrCodigo_Usuario)

                'Prioritarios
                lostrRetorno_act = cls_Oracle.FT_SG_D_CARAC_EVALGIS("ACT", v_codigo, cboInforme.SelectedValue, vCodEVA1, vPrioridad1, "", "", "", gstrUsuarioAcceso)
                'Caracteristicas checks
                If lostrCheck <> "" Then
                    vCodEVA = vCodEVACheck
                    'lostrRetorno_act = cls_Oracle.FT_SG_D_CARAC_EVALGIS("ACT", txtCodigo.Text, cboInforme.SelectedValue, vCodEVA1, lostrCheck1, "", "", "", gstrUsuarioAcceso)
                    lostrRetorno_act = cls_Oracle.FT_SG_D_CARAC_EVALGIS("ACT", v_codigo, cboInforme.SelectedValue, " 1| " & v_codigo & " || ", lostrCheck1, vCodEVACheck, "", "", gstrUsuarioAcceso)

                End If
                'Limite Frontera
                If txtfrontera.Text <> "" Then

                    Dim loValorFrontera As Single = Single.Parse(Mid(txtfrontera.Text, 1, InStr(txtfrontera.Text, "(") - 2))
                    If loValorFrontera > 0 Then 'Solo inserta valor > 0
                        Dim lostrRetorno1 As String
                        lostrRetorno1 = cls_Oracle.FT_SG_D_CARAC_EVALGIS("ACT", v_codigo, cboInforme.SelectedValue, " 1| " & v_codigo & " || ", " 1| " & "LF" & " || ", " 1| " & txtfrontera.Text & " || ", " 1|" & loValorFrontera & "||", "", gstrUsuarioAcceso)
                    End If
                End If
                If Val(lostrRetorno_eli) > 0 Then
                    MsgBox("Se Actualizón correctamente.", vbExclamation, "Evaluación de DM...")
                    glo_Tool_EVA_02 = True
                    var_fa_validaeval = True
                Else
                    MsgBox("No se pudo completar la operación, ya existe el Código", vbExclamation, "Evaluación de DM...")
                    glo_Tool_EVA_01 = True
                End If
            End If
            'End Select

        End If



    End Sub

    Private Sub dgdDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgdDetalle.Click

    End Sub

    Private Sub cboInforme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboInforme.Click

    End Sub

    Private Sub cboInforme_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboInforme.KeyDown

    End Sub

    Private Sub cboInforme_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboInforme.SelectedIndexChanged

    End Sub

    Private Sub gbxDatosevaluacion_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gbxDatosevaluacion.Enter

    End Sub

    Private Sub cboInforme_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboInforme.SelectedValueChanged

    End Sub

    Private Sub cboInforme_TabStopChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboInforme.TabStopChanged

    End Sub

    Private Sub cboInforme_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboInforme.TextChanged

    End Sub

    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub GroupBox7_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox7.Enter

    End Sub

    Private Sub Chk_dato21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Check_dato21_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If Me.Check_dato21.Checked = True Then
        '    v_checkbox9 = Me.Check_dato9.Text
        '    Check_dato10.Checked = False
        '    v_checkbox10 = ""
        'Else
        '    v_checkbox9 = ""
        'End If
    End Sub
End Class
