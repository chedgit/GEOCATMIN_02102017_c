Imports ESRI.ArcGIS.Framework
Public Class frm_Grafica_Ubigeo_ok

    Public m_application As IApplication
    Private lodtbProvincia, lodtbDistrito As New DataTable
    Private cls_Catastro As New cls_DM_1
    Private cls_Prueba As New cls_Prueba
    Private lo_load_Dpto As Integer = 0
    Private lo_load_Prov As Integer = 0
    Private lo_load_Dist As Integer = 0
    Private lo_layer As String = ""
    Private lo_Filtro_Ubigeo As String = ""

    Private Sub cboDpto_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDpto.SelectedIndexChanged
        Dim X As Integer = 0
        If cboDpto.SelectedValue.ToString = "" Then Exit Sub
        Me.cboProv.SelectedIndex = 0
        Me.cboDist.SelectedIndex = 0
        Dim lodtRegistros As New DataTable
        For i As Integer = 0 To UBound(tmp_Prov) - 1
            If cboDpto.SelectedValue = Mid(tmp_Prov(i, 0), 1, 2) Then
                If X = 0 Then
                    lodtRegistros.Columns.Add("CODIGO")
                    lodtRegistros.Columns.Add("DESCRIPCION")
                    Dim dr1 As DataRow
                    dr1 = lodtRegistros.NewRow
                    Try
                        dr1.Item("CODIGO") = ""
                        dr1.Item("DESCRIPCION") = " --Seleccionar-- "
                    Catch ex As Exception
                    End Try
                    lodtRegistros.Rows.Add(dr1)
                    X = 1
                End If
                Dim dr As DataRow
                dr = lodtRegistros.NewRow
                dr.Item(0) = tmp_Prov(i, 0)
                dr.Item(1) = tmp_Prov(i, 1)
                lodtRegistros.Rows.Add(dr)
            End If
        Next
        Dim lodtvProvincia As New DataView(lodtRegistros, Nothing, "DESCRIPCION ASC", DataViewRowState.CurrentRows)
        Me.cboProv.DataSource = lodtvProvincia
        Me.cboProv.DisplayMember = "DESCRIPCION"
        Me.cboProv.ValueMember = "CODIGO"
    End Sub

    Private Sub cboProv_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboProv.SelectedIndexChanged
        Dim X As Integer = 0
        If cboProv.SelectedValue.ToString = "" Then Exit Sub
        Dim lodtRegistros As New DataTable
        For i As Integer = 0 To UBound(tmp_Dist) - 1
            If cboProv.SelectedValue = Mid(tmp_Dist(i, 0), 1, 4) Then
                If X = 0 Then
                    lodtRegistros.Columns.Add("CODIGO")
                    lodtRegistros.Columns.Add("DESCRIPCION")
                    Dim dr1 As DataRow
                    dr1 = lodtRegistros.NewRow
                    Try
                        dr1.Item("CODIGO") = ""
                        dr1.Item("DESCRIPCION") = " --Seleccionar-- "
                    Catch ex As Exception
                    End Try
                    lodtRegistros.Rows.Add(dr1)
                    X = 1
                End If
                Dim dr As DataRow
                dr = lodtRegistros.NewRow
                dr.Item(0) = tmp_Dist(i, 0)
                dr.Item(1) = tmp_Dist(i, 1)
                lodtRegistros.Rows.Add(dr)
            End If
        Next
        Dim lodtvDistrito As New DataView(lodtRegistros, Nothing, "DESCRIPCION ASC", DataViewRowState.CurrentRows)
        Me.cboDist.DataSource = lodtvDistrito
        Me.cboDist.DisplayMember = "DESCRIPCION"
        Me.cboDist.ValueMember = "CODIGO"
    End Sub
    Private Sub frm_Graficar_Ubigeo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'cboEstado.SelectedIndex = 0
        Pintar_Formulario()
        cls_Catastro.Borra_Todo_Feature("", m_application)
        cls_Catastro.Limpiar_Texto_Pantalla(m_application)
        cls_Catastro.Conexion_SDE(m_application)
        cls_Catastro.Actualizar_DM(cboZona.SelectedValue)
        cls_Catastro.Cargar_Combo("DOM_DEPARTAMENTO", cboDpto)
        cls_Catastro.Cargar_Combo("DOM_PROVINCIA", cboProv)
        cls_Catastro.Cargar_Combo("DOM_DISTRITO", cboDist)
    End Sub
    Public Sub Pintar_Formulario()
        Me.BackColor = Drawing.Color.FromArgb(242, 242, 240)
    End Sub

    Private Sub btnGraficar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGraficar.Click
        Dim lostrUbigeo As String = ""
        lo_layer = ""
        lo_Filtro_Ubigeo = ""
        If Me.cboDist.SelectedValue <> "" Then
            lostrUbigeo = Me.cboDist.SelectedValue
        ElseIf Me.cboProv.SelectedValue <> "" Then
            lostrUbigeo = Me.cboProv.SelectedValue
        ElseIf Me.cboDpto.SelectedValue <> "" Then
            lostrUbigeo = Me.cboDpto.SelectedValue
        End If
        Select Case Len(lostrUbigeo)
            Case 2
                If lo_load_Dpto = 0 Then
                    cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "1", True)
                    lo_load_Dpto = 1
                End If
                lo_layer = "Departamento"
                lo_Filtro_Ubigeo = "CD_DEPA = '" & cboDpto.SelectedValue & "'"
            Case 4
                If lo_load_Prov = 0 Then
                    cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Provincia, m_application, "1", True)
                    lo_load_Prov = 1
                End If
                lo_layer = "Provincia"
                lo_Filtro_Ubigeo = "CD_PROV = '" & cboProv.SelectedValue & "'"
            Case 6
                If lo_load_Dist = 0 Then
                    cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito, m_application, "1", True)
                    lo_load_Dist = 1
                End If
                lo_layer = "Distrito"
                lo_Filtro_Ubigeo = "CD_DIST = '" & cboDist.SelectedValue & "'"
        End Select
        Dim lodbtZona As New DataTable
        lodbtZona = cls_Prueba.ShowUniqueValues_Zona_SDE("DBF_DISTRITO_CATASTRO", "ZONA", lo_Filtro_Ubigeo, m_application)

        If lodbtZona.Rows.Count - 1 > 1 Then
            Me.cboZona.Visible = True
            Me.lblZona.Visible = True
            Me.lblZona.Text = "Esta " & lo_layer & " se encuentra entre 2 ZONAS, seleccione una ZONA"
            Dim lodtvZona As New DataView(lodbtZona, Nothing, "DESCRIPCION ASC", DataViewRowState.CurrentRows)
            cboZona.DataSource = lodtvZona
            cboZona.DisplayMember = "DESCRIPCION"
            cboZona.ValueMember = "CODIGO"
        Else
            Dim lodtvZona As New DataView(lodbtZona, Nothing, "DESCRIPCION ASC", DataViewRowState.CurrentRows)
            cboZona.DataSource = lodtvZona
            cboZona.DisplayMember = "DESCRIPCION"
            cboZona.ValueMember = "CODIGO"
            Me.cboZona.SelectedIndex = 1
        End If
    End Sub
    Private Sub cboZona_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboZona.SelectedIndexChanged
        Dim lodtbLeyenda As New DataTable
        Try
            If Me.cboZona.SelectedValue = "" Then
                MsgBox("Seleccione una Zona ..", MsgBoxStyle.Information, "BDGEOCATMIN")
                Exit Sub
            End If
            cls_Catastro.Actualizar_DM(cboZona.SelectedValue)
            Dim loStrShapefile As String = "Catastro" & DateTime.Now.Ticks.ToString()
            cls_Catastro.Delete_Rows_FC_GDB("Malla_" & cboZona.SelectedValue)
            cls_Catastro.Delete_Rows_FC_GDB("Mallap_" & cboZona.SelectedValue)
            cls_Catastro.Load_FC_GDB("Malla_" & cboZona.SelectedValue, "", m_application, True)
            cls_Catastro.Load_FC_GDB("Mallap_" & cboZona.SelectedValue, "", m_application, False)
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & cboZona.SelectedValue, m_application, "1", False)
            'cls_Catastro.Conexion_SDE(m_application)
            Dim lostrMensaje As String = MsgBox("¿ Desea suprimir Derechos de Libre Denunciabilidad ESTADO = Y? ", MsgBoxStyle.YesNo, "[BDGEOCATMIN] Suprimir Derechos Mineros")
            Select Case lostrMensaje
                Case 7
                    cls_Prueba.IntersectSelect_por_Limite(m_application, lo_Filtro_Ubigeo, "", lo_layer, xMin, yMin, xMax, yMax, txtExiste)
                Case 6
                    cls_Prueba.IntersectSelect_por_Limite(m_application, lo_Filtro_Ubigeo, "ESTADO <> 'Y'", lo_layer, xMin, yMin, xMax, yMax, txtExiste)
            End Select
            Select Case txtExiste.Text
                Case -1
                    cls_Catastro.Borra_Todo_Feature("", m_application)
                    cls_Catastro.Limpiar_Texto_Pantalla(m_application)
                    MsgBox("..No existe Denuncios en esta Zona..", MsgBoxStyle.Information, "BDGEOTCATMIN")
                    DialogResult = Windows.Forms.DialogResult.Cancel
                Case Else
                    'cls_Catastro.Expor_Tema(loStrShapefile, "Denun=No", m_application)
                    cls_Catastro.Expor_Tema(loStrShapefile, sele_denu, m_application)
                    cls_Catastro.Quitar_Layer("Catastro", m_application)
                    cls_Prueba.AddFieldDM(loStrShapefile) : cls_Catastro.Quitar_Layer(loStrShapefile, m_application)
                    cls_Catastro.Add_ShapeFile(loStrShapefile, m_application)
                    cls_Catastro.UpdateValue(lo_Filtro_Ubigeo, m_application, "Catastro")
                    'cls_Catastro.Genera_Tematico_Catastro(lo_Filtro_Ubigeo, m_application)
                    lodtbLeyenda = cls_Prueba.f_Genera_Leyenda_DM("Catastro", lo_Filtro_Ubigeo, m_application)
                    cls_Prueba.Poligono_Color_1("Catastro", lodtbLeyenda, glo_pathStyle & "\Catastro.style", "LEYENDA", "", m_application)
                    cls_Catastro.ShowLabel_DM("Catastro", m_application)
                    cls_Catastro.Seleccioname_Envelope(lo_Filtro_Ubigeo, lo_layer, xMin, yMin, xMax, yMax, cboZona.SelectedValue, m_application)
                    'cls_Catastro.Seleccioname_Envelope(lo_Filtro_Ubigeo, lo_layer, v_xmin, v_ymin, v_xmax, v_ymax, cboZona.SelectedValue, m_application)
                    cls_Catastro.Genera_Malla_UTM(CType(xMin.Text, Double), CType(yMin.Text, Double), CType(xMax.Text, Double), CType(yMax.Text, Double), cboZona.SelectedValue, m_application)
                    'cls_Catastro.Genera_Malla_UTM(v_xmin, v_ymin, v_xmax, v_ymax, cboZona.SelectedValue, m_application)
                    cls_Catastro.Rotular_texto_DM("Mallap_" & cboZona.SelectedValue, cboZona.SelectedValue, m_application)
                    cls_Catastro.Quitar_Layer("Mallap_" & cboZona.SelectedValue, m_application)
                    cls_Catastro.DefinitionExpression(lo_Filtro_Ubigeo, m_application, lo_layer)
                    cls_Catastro.Shade_Poligono(lo_layer, m_application)
                    'cls_Catastro.Style_Linea_GDB("Malla_" & cboZona.SelectedValue, glo_pathStyle & "\malla.style", "CLASE", m_application, "GDB")
                    cls_Catastro.ShowLabel_DM(lo_layer, m_application)
                    lodtbLeyenda = Nothing
                    DialogResult = Windows.Forms.DialogResult.Cancel
            End Select
        Catch ex As Exception
            ' MsgBox(ex.Message)
        End Try
    End Sub

End Class