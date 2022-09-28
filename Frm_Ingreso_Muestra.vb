Public Class Frm_Ingreso_Muestra
    Public txhTitulo As String = ""
    Public txhArea As String = ""
    Public txhProyecto As String = ""
    Public txhResponsable As String = ""
    Public txhOrden As String = 0
    Public txhTipo As String = ""
    Public txhCodServicio As String = ""
    Public txhCodMuestra As String = ""

    Private Const ColNumero = 0
    Private Const ColCodCampo = 1
    Private Const Colutmx = 2
    Private Const Colutmy = 3
    Private Const ColLugar = 4
    Private Const ColHoja = 5
    Private Const ColDepa = 6
    Private Const ColProv = 7
    Private Const ColDist = 8
    Private Const ColObservacion = 9
    Private lodtbMuestra_1 As New DataTable
    Private lodtbOrden As New DataTable
    Private objclsBD_Tablas_Generales As New clsBD_Tablas_Generales
    Private lostrEstado As String = "INS"

    Private Sub Frm_Ingreso_Muestra_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If txhTipo <> "OT" Then
            Me.lblTrabajo.Text = txhTitulo
            Me.txtDireccion.Text = txhArea
            Me.txtProyecto.Text = txhProyecto
            Me.txtResponsable.Text = Me.txhResponsable
        Else
            Me.txtDireccion.Text = ""
            Me.txtProyecto.Text = ""
            Me.txtResponsable.Text = ""
            Me.txtDireccion.Visible = False
            Me.txtProyecto.Visible = False
            Me.txtResponsable.Visible = False
            Me.lblDireccion.Visible = False
            Me.lblProyecto.Visible = False
            Me.lblResponsable.Visible = False
        End If

        dtpFecha.Text = Mid(DateTime.Now, 1, 10)
        Call PT_Inicializar_Grilla_Ensayo()


        If txhOrden = 0 Then
            lodtbOrden = objclsBD_Tablas_Generales.FS_Sel_Tipo_Muestra(0, txhTipo, txhCodServicio)
            Me.txtCod_Muestra.Text = txhCodMuestra

            If lodtbOrden.Rows.Count = 0 Then Exit Sub
            Me.dgd_Muestra.DataSource = lodtbOrden
            PT_Agregar_Funciones_Ensayo() : PT_Forma_Grilla_Ensayo() : Carga_Tipo_Muestra()
            lostrEstado = "UPD"
        Else
            lodtbOrden = objclsBD_Tablas_Generales.FS_Sel_Tipo_Muestra(txhOrden, txhTipo, txhCodServicio)
            Me.dgd_Muestra.DataSource = lodtbOrden
            PT_Agregar_Funciones_Ensayo() : PT_Forma_Grilla_Ensayo() : Carga_Tipo_Muestra()
            lostrEstado = "UPD"
            Me.txtCant_Muestra.Text = Me.dgd_Muestra.BindingContext(Me.dgd_Muestra.DataSource, Me.dgd_Muestra.DataMember).Count
            Me.txtCod_Muestra.Text = txhCodMuestra
        End If
    End Sub
    Private Sub Inicializar_Formulario()
        'Dim lodtbTipoDocPago As New DataTable
        'Dim lodtbEstado As New DataTable
        'Dim lodtbTipoSolicitante As New DataTable
        'Dim lodtbProyecto As New DataTable
        'Dim lodtbArea As New DataTable
        'Try
        '    'Seleccciona El Area y lista los Proyectos
        '    Dim lostrArea As String = ""
        '    Select Case gstrAreaUsuario
        '        Case 11 : lostrArea = "GR"
        '        Case 12 : lostrArea = "GE"
        '        Case 13, 27 : lostrArea = "GA"
        '    End Select
        '    lodtbProyecto = objclsBD_Tablas_Generales.FS_Sel_Lista_Proyecto(lostrArea)
        '    Dim lodtvProyecto As New DataView(lodtbProyecto)
        '    Me.cboProyecto.DisplayMember = "DESCRIPCION"
        '    Me.cboProyecto.ValueMember = "CODIGO"
        '    Me.cboProyecto.DataSource = lodtvProyecto


        '    lodtbArea = objclsBD_Tablas_Generales.FS_Sel_Lista_Proyecto("")
        '    Dim lodtvArea As New DataView(lodtbArea)
        '    Me.cboDireccion.DisplayMember = "DESCRIPCION"
        '    Me.cboDireccion.ValueMember = "CODIGO"
        '    Me.cboDireccion.DataSource = lodtvArea
        'Catch ex As Exception
        '    PT_Excepcion(ex)
        'End Try
    End Sub

    Public Sub PT_Inicializar_Grilla_Ensayo()
        Dim dvwLista_Orden As New DataView(lodtbMuestra_1)
        lodtbMuestra_1.Columns.Add("NUMERO", GetType(String))
        lodtbMuestra_1.Columns.Add("COD_CAMPO", GetType(String))
        lodtbMuestra_1.Columns.Add("UTM_X", GetType(String))
        lodtbMuestra_1.Columns.Add("UTM_Y", GetType(String))
        lodtbMuestra_1.Columns.Add("LUGAR", GetType(String))
        lodtbMuestra_1.Columns.Add("NOM_HOJA", GetType(String))
        lodtbMuestra_1.Columns.Add("NOM_DPTO", GetType(String))
        lodtbMuestra_1.Columns.Add("NOM_PROV", GetType(String))
        lodtbMuestra_1.Columns.Add("NOM_DIST", GetType(String))
        lodtbMuestra_1.Columns.Add("OBSERVACION", GetType(String))
        PT_Estilo_Grilla_Ensayo(lodtbMuestra_1) : PT_Cargar_Grilla_Ensayo(lodtbMuestra_1)
        PT_Agregar_Funciones_Ensayo() : PT_Forma_Grilla_Ensayo() ' : Carga_Tipo_Muestra()
    End Sub
    Private Sub PT_Estilo_Grilla_Ensayo(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(ColNumero).DefaultValue = ""
        padtbDetalle.Columns.Item(ColCodCampo).DefaultValue = ""
        padtbDetalle.Columns.Item(Colutmx).DefaultValue = ""
        padtbDetalle.Columns.Item(Colutmy).DefaultValue = ""
        padtbDetalle.Columns.Item(ColLugar).DefaultValue = ""
        padtbDetalle.Columns.Item(ColHoja).DefaultValue = ""
        padtbDetalle.Columns.Item(ColDepa).DefaultValue = ""
        padtbDetalle.Columns.Item(ColProv).DefaultValue = ""
        padtbDetalle.Columns.Item(ColDist).DefaultValue = ""
        padtbDetalle.Columns.Item(ColObservacion).DefaultValue = ""
    End Sub
    Private Sub PT_Cargar_Grilla_Ensayo(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgd_Muestra.DataSource = dvwDetalle
        Pinta_Grilla_Detalle()
    End Sub
    Public Sub Pinta_Grilla_Detalle()
        Me.dgd_Muestra.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgd_Muestra.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        Me.dgd_Muestra.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        Me.dgd_Muestra.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
    End Sub
    Private Sub PT_Agregar_Funciones_Ensayo()
        Me.dgd_Muestra.Columns(ColNumero).DefaultValue = ""
        Me.dgd_Muestra.Columns(ColCodCampo).DefaultValue = ""
        Me.dgd_Muestra.Columns(Colutmx).DefaultValue = ""
        Me.dgd_Muestra.Columns(Colutmy).DefaultValue = ""
        Me.dgd_Muestra.Columns(ColLugar).DefaultValue = ""
        Me.dgd_Muestra.Columns(ColHoja).DropDown = Me.ddcHoja
        'Me.dgd_Muestra.Columns(ColDepa).DropDown = Me.ddcDpto
        'Me.dgd_Muestra.Columns(ColProv).DropDown = Me.ddcProv
        'Me.dgd_Muestra.Columns(ColDist).DropDown = Me.ddcDist
        Me.dgd_Muestra.Columns(ColDepa).DefaultValue = ""
        Me.dgd_Muestra.Columns(ColProv).DefaultValue = ""
        Me.dgd_Muestra.Columns(ColDist).DefaultValue = ""
        Me.dgd_Muestra.Columns(ColObservacion).DefaultValue = ""
    End Sub
    Private Sub PT_Forma_Grilla_Ensayo()
        Me.dgd_Muestra.Splits(0).DisplayColumns(ColNumero).Width = 50
        Me.dgd_Muestra.Splits(0).DisplayColumns(ColCodCampo).Width = 80
        Me.dgd_Muestra.Splits(0).DisplayColumns(Colutmx).Width = 60
        Me.dgd_Muestra.Splits(0).DisplayColumns(Colutmy).Width = 70
        Me.dgd_Muestra.Splits(0).DisplayColumns(ColLugar).Width = 80
        Me.dgd_Muestra.Splits(0).DisplayColumns(ColHoja).Width = 100
        Me.dgd_Muestra.Splits(0).DisplayColumns(ColDepa).Width = 100
        Me.dgd_Muestra.Splits(0).DisplayColumns(ColProv).Width = 100
        Me.dgd_Muestra.Splits(0).DisplayColumns(ColDist).Width = 100
        Me.dgd_Muestra.Splits(0).DisplayColumns(ColObservacion).Width = 250

        Me.dgd_Muestra.Columns("NUMERO").Caption = "Núm."
        Me.dgd_Muestra.Columns("COD_CAMPO").Caption = "Código Muestra"
        Me.dgd_Muestra.Columns("UTM_X").Caption = "Utm X"
        Me.dgd_Muestra.Columns("UTM_Y").Caption = "Utm Y"
        Me.dgd_Muestra.Columns("LUGAR").Caption = "Lugar"
        Me.dgd_Muestra.Columns("NOM_HOJA").Caption = "Hoja"
        Me.dgd_Muestra.Columns("NOM_DPTO").Caption = "Departamento"
        Me.dgd_Muestra.Columns("NOM_PROV").Caption = "Provincia"
        Me.dgd_Muestra.Columns("NOM_DIST").Caption = "Distrito"
        Me.dgd_Muestra.Columns("OBSERVACION").Caption = "Observaciones"

        'Me.dgd_Muestra.Splits(0).DisplayColumns(ColNumero).Locked = True
        'Me.dgd_Muestra.Splits(0).DisplayColumns(ColCodCampo).Locked = True
        'Me.dgd_Muestra.Splits(0).DisplayColumns(Colutmx).Locked = True
        Me.dgd_Muestra.Splits(0).DisplayColumns(ColNumero).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgd_Muestra.Splits(0).DisplayColumns(ColCodCampo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgd_Muestra.Splits(0).DisplayColumns(Colutmx).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgd_Muestra.Splits(0).DisplayColumns(Colutmy).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgd_Muestra.Splits(0).DisplayColumns(ColLugar).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgd_Muestra.Splits(0).DisplayColumns(ColHoja).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgd_Muestra.Splits(0).DisplayColumns(ColDepa).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgd_Muestra.Splits(0).DisplayColumns(ColProv).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgd_Muestra.Splits(0).DisplayColumns(ColDist).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgd_Muestra.Splits(0).DisplayColumns(ColObservacion).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
    End Sub
    Private Sub Carga_Tipo_Muestra()
        'Carga Cuadrángulos
        Dim lodtbParametroTI As New DataTable
        Dim lodtbParametroIN As New DataTable
        'Cargar las clases
        lodtbParametroTI = objclsBD_Tablas_Generales.FS_Sel_Hoja_Ini_Orden("CUADRANGULO")
        Dim i As Integer
        Dim v As C1.Win.C1TrueDBGrid.ValueItemCollection
        v = Me.dgd_Muestra.Columns(5).ValueItems.Values
        For i = 0 To lodtbParametroTI.Rows.Count - 1
            v.Add(New C1.Win.C1TrueDBGrid.ValueItem(lodtbParametroTI.Rows(i).Item("CODIGO").ToString, lodtbParametroTI.Rows(i).Item("DESCRIPCION").ToString))
        Next
        Me.dgd_Muestra.Columns(5).ValueItems.Translate = True
        Me.dgd_Muestra.Columns(5).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox
        Me.dgd_Muestra.Splits(0).DisplayColumns(5).DropDownList = True
        Call Carga_Dpto() : Call Carga_Provincia() : Call Carga_Distrito()
    End Sub
    Public Sub Carga_Dpto()
        '-------------Carga Dptos.
        Dim lodtbParametroTI1 As New DataTable
        Dim lodtbParametroIN1 As New DataTable
        'Cargar las clases
        lodtbParametroTI1 = objclsBD_Tablas_Generales.P_SEL_Departamento
        Dim i1 As Integer
        Dim v1 As C1.Win.C1TrueDBGrid.ValueItemCollection
        v1 = Me.dgd_Muestra.Columns(6).ValueItems.Values
        For i1 = 0 To lodtbParametroTI1.Rows.Count - 1
            v1.Add(New C1.Win.C1TrueDBGrid.ValueItem(lodtbParametroTI1.Rows(i1).Item("CODIGO").ToString, lodtbParametroTI1.Rows(i1).Item("DESCRIPCION").ToString))
        Next
        Me.dgd_Muestra.Columns(6).ValueItems.Translate = True
        Me.dgd_Muestra.Columns(6).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox
        Me.dgd_Muestra.Splits(0).DisplayColumns(6).DropDownList = True
    End Sub
    Private Sub Carga_Provincia()
        '------------Carga Provincia
        Dim lodtbParametroTI2 As New DataTable
        Dim lodtbParametroIN2 As New DataTable
        'Cargar las clases
        If Not IsDBNull(Me.dgd_Muestra.Item(Me.dgd_Muestra.Row, "NOM_DPTO")) Then
            lodtbParametroTI2 = objclsBD_Tablas_Generales.P_SEL_Provincia(Me.dgd_Muestra.Item(Me.dgd_Muestra.Row, "NOM_DPTO"))
            Dim i2 As Integer
            Dim v2 As C1.Win.C1TrueDBGrid.ValueItemCollection
            v2 = Me.dgd_Muestra.Columns(7).ValueItems.Values
            For i2 = 0 To lodtbParametroTI2.Rows.Count - 1
                v2.Add(New C1.Win.C1TrueDBGrid.ValueItem(lodtbParametroTI2.Rows(i2).Item("CODIGO").ToString, lodtbParametroTI2.Rows(i2).Item("DESCRIPCION").ToString))
            Next
            Me.dgd_Muestra.Columns(7).ValueItems.Translate = True
            Me.dgd_Muestra.Columns(7).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox
            Me.dgd_Muestra.Splits(0).DisplayColumns(7).DropDownList = True
        End If
    End Sub
    Public Sub Carga_Distrito()
        ''-------------Carga Distrito
        'Dim lodtbParametroTI3 As New DataTable
        'Dim lodtbParametroIN3 As New DataTable
        ''Cargar las clases
        'lodtbParametroTI3 = objclsBD_Tablas_Generales.P_SEL_Distrito(Me.dgd_Muestra2.Item(Me.dgd_Muestra2.Row, "NOM_PROV"))
        'Dim i3 As Integer
        'Dim v3 As C1.Win.C1TrueDBGrid.ValueItemCollection
        'v3 = Me.dgd_Muestra2.Columns(8).ValueItems.Values
        'For i3 = 0 To lodtbParametroTI3.Rows.Count - 1
        '    v3.Add(New C1.Win.C1TrueDBGrid.ValueItem(lodtbParametroTI3.Rows(i3).Item("CODIGO").ToString, lodtbParametroTI3.Rows(i3).Item("DESCRIPCION").ToString))
        'Next
        'Me.dgd_Muestra2.Columns(8).ValueItems.Translate = True
        'Me.dgd_Muestra2.Columns(8).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox
        'Me.dgd_Muestra2.Splits(0).DisplayColumns(8).DropDownList = True

    End Sub


    Private Sub dgd_Muestra_AfterColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs)
        If Me.dgd_Muestra.Col = 6 Then
            Dim lodtbParametroTI2 As New DataTable
            Dim lodtbParametroIN2 As New DataTable
            'Cargar las clases
            lodtbParametroTI2 = objclsBD_Tablas_Generales.P_SEL_Provincia(Me.dgd_Muestra.Item(Me.dgd_Muestra.Row, "NOM_DPTO"))
            Dim i2 As Integer
            Dim v2 As C1.Win.C1TrueDBGrid.ValueItemCollection
            v2 = Me.dgd_Muestra.Columns(7).ValueItems.Values
            v2.Clear()
            For i2 = 0 To lodtbParametroTI2.Rows.Count - 1
                v2.Add(New C1.Win.C1TrueDBGrid.ValueItem(lodtbParametroTI2.Rows(i2).Item("CODIGO").ToString, lodtbParametroTI2.Rows(i2).Item("DESCRIPCION").ToString))
            Next
            Me.dgd_Muestra.Columns(7).ValueItems.Translate = True
            Me.dgd_Muestra.Columns(7).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox
            Me.dgd_Muestra.Splits(0).DisplayColumns(7).DropDownList = True
        End If
    End Sub

    Private Sub dgd_Muestra_ButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgd_Muestra.ButtonClick
        If Me.dgd_Muestra.Col = 7 Then
            Dim lodtbParametroTI2 As New DataTable
            Dim lodtbParametroIN2 As New DataTable
            'Cargar las clases
            lodtbParametroTI2 = objclsBD_Tablas_Generales.P_SEL_Provincia(Me.dgd_Muestra.Item(Me.dgd_Muestra.Row, "NOM_DPTO"))
            Dim i2 As Integer
            Dim v2 As C1.Win.C1TrueDBGrid.ValueItemCollection
            v2 = Me.dgd_Muestra.Columns(7).ValueItems.Values
            v2.Clear()
            For i2 = 0 To lodtbParametroTI2.Rows.Count - 1
                v2.Add(New C1.Win.C1TrueDBGrid.ValueItem(lodtbParametroTI2.Rows(i2).Item("CODIGO").ToString, lodtbParametroTI2.Rows(i2).Item("DESCRIPCION").ToString))
            Next
            Me.dgd_Muestra.Columns(7).ValueItems.Translate = True
            Me.dgd_Muestra.Columns(7).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox
            Me.dgd_Muestra.Splits(0).DisplayColumns(7).DropDownList = True
        ElseIf Me.dgd_Muestra.Col = 8 Then
            Dim lodtbParametroTI3 As New DataTable
            Dim lodtbParametroIN3 As New DataTable
            'Cargar las clases
            lodtbParametroTI3 = objclsBD_Tablas_Generales.P_SEL_Distrito(Me.dgd_Muestra.Item(Me.dgd_Muestra.Row, "NOM_PROV"))
            Dim i3 As Integer
            Dim v3 As C1.Win.C1TrueDBGrid.ValueItemCollection
            v3 = Me.dgd_Muestra.Columns(8).ValueItems.Values
            v3.Clear()
            For i3 = 0 To lodtbParametroTI3.Rows.Count - 1
                v3.Add(New C1.Win.C1TrueDBGrid.ValueItem(lodtbParametroTI3.Rows(i3).Item("CODIGO").ToString, lodtbParametroTI3.Rows(i3).Item("DESCRIPCION").ToString))
            Next
            Me.dgd_Muestra.Columns(8).ValueItems.Translate = True
            Me.dgd_Muestra.Columns(8).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox
            Me.dgd_Muestra.Splits(0).DisplayColumns(8).DropDownList = True
        End If
    End Sub

    Private Sub Inicializa_Grilla()
        'Dim lointTotalMuestras As Integer
        'lointTotalMuestras = 1
        'For i As Integer = 0 To lointTotalMuestras
        '    Dim lodtrMuestra As DataRow
        '    lodtrMuestra = lodtbMuestra_1.NewRow()
        '    lodtrMuestra(0) = i + 1
        '    lodtrMuestra(1) = "Muestra_" & i + 1
        '    lodtrMuestra(2) = ""
        '    lodtrMuestra(3) = ""
        '    lodtrMuestra(4) = ""
        '    lodtrMuestra(5) = ""
        '    lodtrMuestra(6) = ""
        '    lodtrMuestra(7) = ""
        '    lodtrMuestra(8) = ""
        '    ' lodtrMuestra(9) = ""
        '    lodtbMuestra_1.Rows.Add(lodtrMuestra)
        '    Me.dgd_Muestra.DataSource = lodtbMuestra_1
        '    PT_Agregar_Funciones_Ensayo() : PT_Forma_Grilla_Ensayo() : Carga_Tipo_Muestra()
        'Next i
        'Me.txtTotal.Text = lointTotalMuestras + 1
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnGenerar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        'If Me.txtCant_Muestra.Text Then
        Dim lointTotalMuestras As Integer
        lointTotalMuestras = Me.txtCant_Muestra.Text
        For i As Integer = 1 To lointTotalMuestras
            Dim lodtrMuestra As DataRow
            lodtrMuestra = lodtbMuestra_1.NewRow()
            lodtrMuestra(0) = i
            lodtrMuestra(1) = Me.txtCod_Muestra.Text & RellenarComodin(i, 2, "0")
            lodtrMuestra(2) = ""
            lodtrMuestra(3) = ""
            lodtrMuestra(4) = ""
            lodtrMuestra(5) = ""
            lodtrMuestra(6) = ""
            lodtrMuestra(7) = ""
            lodtrMuestra(8) = ""
            lodtrMuestra(9) = ""
            lodtbMuestra_1.Rows.Add(lodtrMuestra)
            Me.dgd_Muestra.DataSource = lodtbMuestra_1
            PT_Agregar_Funciones_Ensayo() : PT_Forma_Grilla_Ensayo() : Carga_Tipo_Muestra()
        Next i
        'ME.TXTMe.txtTotal.Text = lointTotalMuestras
    End Sub

    Private Sub BtnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAceptar.Click
        'Guarda Temporalmente 

        '        lodtbMuestra_1 = lodtbOrden

        Dim lostrNumero As String = ""
        Dim lostrCodCampo As String = ""
        Dim lostrUtmx As String = ""
        Dim lostrUtmy As String = ""
        Dim lostrLugar As String = ""
        Dim lostrNomHoja As String = ""
        Dim lostrDpto As String = ""
        Dim lostrProv As String = ""
        Dim lostrDist As String = ""
        Dim lostrObserva As String = ""

        Me.txtDireccion.Text = txhArea
        Me.txtProyecto.Text = txhProyecto
        Me.txtResponsable.Text = Me.txhResponsable
        Dim loArea, loProy, loUsuario As String
        If txhTipo = "SO" Then
            loArea = Mid(Me.txtDireccion.Text, 1, InStr(Me.txtDireccion.Text, ":") - 1)
            loProy = Mid(Me.txtProyecto.Text, 1, InStr(Me.txtProyecto.Text, ":") - 1)
            loUsuario = Mid(Me.txtResponsable.Text, 1, InStr(Me.txtResponsable.Text, ":") - 1)
        Else
            loArea = ""
            loProy = ""
            loUsuario = ""
        End If
        For j As Integer = 0 To Me.dgd_Muestra.BindingContext(Me.dgd_Muestra.DataSource, Me.dgd_Muestra.DataMember).Count - 1
            If Not IsDBNull(Me.dgd_Muestra.Item(j, "NUMERO")) Then
                lostrNumero = lostrNumero & j + 1 & " | " & Me.dgd_Muestra.Item(j, "NUMERO") & " || "
                lostrCodCampo = lostrCodCampo & j + 1 & " | " & Me.dgd_Muestra.Item(j, "COD_CAMPO") & " || "
                lostrUtmx = lostrUtmx & j + 1 & " | " & Me.dgd_Muestra.Item(j, "UTM_X") & " || "
                lostrUtmy = lostrUtmy & j + 1 & " | " & Me.dgd_Muestra.Item(j, "UTM_Y") & " || "
                lostrLugar = lostrLugar & j + 1 & " | " & Me.dgd_Muestra.Item(j, "LUGAR") & " || "
                lostrNomHoja = lostrNomHoja & j + 1 & " | " & Me.dgd_Muestra.Item(j, "NOM_HOJA") & " || "
                lostrDpto = lostrDpto & j + 1 & " | " & Me.dgd_Muestra.Item(j, "NOM_DPTO") & " || "
                lostrProv = lostrProv & j + 1 & " | " & Me.dgd_Muestra.Item(j, "NOM_PROV") & " || "
                lostrDist = lostrDist & j + 1 & " | " & Me.dgd_Muestra.Item(j, "NOM_DIST") & " || "
                lostrObserva = lostrObserva & j + 1 & " | " & Me.dgd_Muestra.Item(j, "OBSERVACION") & " || "
            End If
        Next
        'If MsgBox("¿ Desea grabar los datos ingresados ? ", MsgBoxStyle.YesNo, "BDGeocientífica") = vbNo Then
        '    Exit Sub
        'End If
        Dim lostrRetorno As String = "0"
        Try
            If lostrEstado = "UPD" And txhOrden <> 0 Then
                lostrRetorno = objclsBD_Tablas_Generales.FT_Mantenimiento_Muestras("UPDATE", txhTipo, txhOrden, lostrCodCampo, _
                lostrUtmx, lostrUtmy, lostrLugar, lostrNomHoja, lostrDpto, lostrProv, lostrDist, lostrObserva, loArea, loProy, loUsuario, txhCodServicio)
            ElseIf lostrEstado = "UPD" And txhOrden = 0 Then
                lostrRetorno = objclsBD_Tablas_Generales.FT_Mantenimiento_Muestras_TMP("UPDATE", txhTipo, txhOrden, lostrCodCampo, _
                lostrUtmx, lostrUtmy, lostrLugar, lostrNomHoja, lostrDpto, lostrProv, lostrDist, lostrObserva, loArea, loProy, loUsuario, txhCodServicio)
            ElseIf lostrEstado = "INS" Then
                lostrRetorno = objclsBD_Tablas_Generales.FT_Mantenimiento_Muestras_TMP("INSERTA", txhTipo, txhOrden, lostrCodCampo, _
                lostrUtmx, lostrUtmy, lostrLugar, lostrNomHoja, lostrDpto, lostrProv, lostrDist, lostrObserva, loArea, loProy, loUsuario, txhCodServicio)
            End If
            If lostrRetorno = "1" Then
                '                MsgBox("La operación se realizó exitosamente.", vbExclamation, "[Dirección de Laboratorios]")
                lostrEstado = "UPD"
            End If
            ', lostrEM, lostrEP, lostrAM, lostrPM, lostrAQ, lostrIS)C:\Sistema de Control de Muestras\Sistema de Control de Muestras\Aplic_CMLaboratorio\FrmVerAsociados.vb
        Catch ex As Exception
            MsgBox(ex.Source & vbNewLine & ex.Message)

        End Try
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class