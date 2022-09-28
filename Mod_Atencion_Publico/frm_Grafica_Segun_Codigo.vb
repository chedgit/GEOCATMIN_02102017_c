Imports PORTAL_Clases
Imports ESRI.ArcGIS.Framework
Public Class frm_Grafica_Segun_Codigo
    Public m_application As IApplication
    Private cls_Oracle As New cls_Oracle
    Private cls_Catastro As New cls_DM_1
    Private cls_DM_2 As New cls_DM_2
    Private cls_Prueba As New cls_Prueba
    Private lodtbDatos As New DataTable


    Private Sub txtConsulta_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtConsulta.KeyPress
        If e.KeyChar = Chr(13) Then
            Select Case Me.cboConsulta.SelectedIndex
                Case 0
                    Dim losender As New System.Object
                    Dim loe As New System.EventArgs
                    Me.btnBuscar_Click(losender, loe)
                Case 1
                    Dim losender As New System.Object
                    Dim loe As New System.EventArgs
                    Me.btnBuscar_Click(losender, loe)
            End Select
        End If
    End Sub
    Private Sub visualizar_datos_dm(ByVal p_consulta As String, ByVal p_App As IApplication)
        Me.lstCoordenada.Items.Clear()
        lodtbDatos = cls_Oracle.F_Obtiene_Datos_DM(p_consulta)
        Select Case lodtbDatos.Rows.Count
            Case 0
                MsgBox("No Existe Codigo: " & p_consulta & " en la Base de Datos....")
                Me.txtConsulta.Focus()
            Case Else
                cls_Catastro.Actualizar_DM(lodtbDatos.Rows(0).Item("PE_ZONCAT"))
                Me.txtNombre.Text = lodtbDatos.Rows(0).Item("PE_NOMDER").ToString
                Me.txtTitular.Text = lodtbDatos.Rows(0).Item("TITULAR").ToString
                Me.txtCodigo.Text = lodtbDatos.Rows(0).Item("CG_CODIGO").ToString
                Me.txtZona.Text = lodtbDatos.Rows(0).Item("PE_ZONCAT").ToString
                Me.txtCarta.Text = lodtbDatos.Rows(0).Item("CA_CODCAR").ToString
                Me.txtVigCat.Text = lodtbDatos.Rows(0).Item("PE_VIGCAT").ToString
                Me.txtVigCat.Text = lodtbDatos.Rows(0).Item("PE_VIGCAT").ToString
                cls_Catastro.Borra_Todo_Feature("", m_application)
                '*******************
                cls_Catastro.Load_FC_GDB("gpt_Vertice_DM", "", m_application, True)
                cls_Catastro.Delete_Rows_FC_GDB("gpt_Vertice_DM")
                '*******************
                cls_DM_2.Genera_Catastro_DM(p_consulta, lodtbDatos.Rows(0).Item("PE_ZONCAT"), p_App)
                cls_Catastro.Rotular_texto_DM("gpt_Vertice_DM", Me.txtZona.Text, m_application)
                cls_Catastro.Quitar_Layer("gpt_Vertice_DM", m_application)
                'cls_Catastro.ShowLabel_DM("gpt_Vertice_DM", m_application)
                Dim lo_Filtro As String = "CODIGOU = '" & Me.txtCodigo.Text & "'"
                cls_Catastro.Seleccioname_Envelope(lo_Filtro, "Catastro", xMin, yMin, xMax, yMax, Me.txtZona.Text, m_application)
                'cls_Catastro.Seleccioname_Envelope(lo_Filtro, "Catastro", v_xmin, v_ymin, v_xmax, v_ymax, Me.txtZona.Text, m_application)
                Me.lstCoordenada.Items.Add(" Vert. " & "   Este  " & "     " & "  Norte  ")
                Me.lstCoordenada.Items.Add("--------------------------------------------")
                For i As Integer = 0 To lodtbDatos.Rows.Count - 1
                    Me.lstCoordenada.Items.Add(" " & RellenarComodin(i + 1, 3, "0") & "   " & Format(lodtbDatos.Rows(i).Item("CD_COREST"), "###,###.00") & "   " & Format(lodtbDatos.Rows(i).Item("CD_CORNOR"), "###,###.00") & "")
                Next
                cls_Catastro.ClearLayerSelection(pFeatureLayer)
                cls_Catastro.Color_Poligono_Simple(m_application, "Catastro")
                cls_Catastro.Genera_Imagen_DM("VistaPrevia", "VistaPrevia")
                img_DM.ImageLocation = glo_pathTMP & "\VistaPrevia.jpg"
                cls_Catastro.Shade_Poligono("Catastro", m_application)
                cls_Catastro.Genera_Imagen_DM("VistaPrevia_1", "VistaPrevia")
                cls_Catastro.Borra_Todo_Feature("", m_application)
                cls_Catastro.Limpiar_Texto_Pantalla(m_application)
                Me.txtRadio.Enabled = True
                Dim lodtvOrdena_x As New DataView(lodtbDatos, Nothing, "CD_COREST ASC", DataViewRowState.CurrentRows)
                xMin.Text = lodtvOrdena_x.Item(0).Row("CD_COREST")
                xMax.Text = lodtvOrdena_x.Item(lodtvOrdena_x.Count - 1).Row("CD_COREST")
                Dim lodtvOrdena_y As New DataView(lodtbDatos, Nothing, "CD_CORNOR ASC", DataViewRowState.CurrentRows)
                yMin.Text = lodtvOrdena_y.Item(0).Row("CD_CORNOR")
                yMax.Text = lodtvOrdena_y.Item(lodtvOrdena_y.Count - 1).Row("CD_CORNOR")
                Me.btnGraficar.Enabled = True
                Me.btnReporte.Enabled = True
        End Select
        'End If
    End Sub

    Private Sub frm_Graficar_Segun_Codigo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim m_form As New GEOCATMIN_IniLogin
        If gloint_Acceso = 0 Then m_form.ShowDialog()
        m_form.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
        If m_form.DialogResult = Windows.Forms.DialogResult.OK Or gloint_Acceso = "1" Then
            gloint_Acceso = "1"
            Me.lblUsuario.Text = "Usuario: " & gstrNombre_Usuario 'gstrUsuarioAcceso
            Me.clbLayer.SetItemCheckState(0, Windows.Forms.CheckState.Indeterminate)
            Me.clbLayer.SetItemCheckState(1, Windows.Forms.CheckState.Indeterminate)
            loint_Intervalo = 0
            Dim cls_Catastro As New cls_DM_1
            Pintar_Formulario()
            cls_Catastro.Borra_Todo_Feature("", m_application)
            cls_Catastro.Limpiar_Texto_Pantalla(m_application)
            Me.img_DM.ImageLocation = glo_pathTMP & "\Vacio.jpg"
            Me.btnGraficar.Enabled = False
            Me.btnReporte.Enabled = False
        Else
            Me.Close()
        End If
    End Sub
    Public Sub Pintar_Formulario()
        Me.BackColor = Drawing.Color.FromArgb(242, 242, 240)
    End Sub

    Private Sub btnGraficar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGraficar.Click
        Dim lodtbLeyenda As New DataTable
        Dim lostrMensaje As String = 6
        Dim loStrShapefile As String = "Catastro" & DateTime.Now.Ticks.ToString()
        Dim lo_xMin = CType(Me.xMin.Text, Double) - (CType(Me.txtRadio.Text, Integer) * 1000)
        Dim lo_xMax = CType(Me.xMax.Text, Double) + (CType(Me.txtRadio.Text, Integer) * 1000)
        Dim lo_yMin = CType(Me.yMin.Text, Double) - (CType(Me.txtRadio.Text, Integer) * 1000)
        Dim lo_yMax = CType(Me.yMax.Text, Double) + (CType(Me.txtRadio.Text, Integer) * 1000)
        gloEsteMin = Me.xMin.Text : gloEsteMax = Me.xMax.Text
        gloNorteMin = Me.yMin.Text : gloNorteMax = Me.yMax.Text : gloZona = Me.txtZona.Text
        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & Me.txtZona.Text, m_application, "1", False)
        'cls_Catastro.Conexion_SDE(m_application)
        If Me.txtVigCat.Text <> "G" Then
            MsgBox("LAS COORDENADAS DE ESTE DERECHO MINERO NO" & vbNewLine & "ESTAN HABILITADAS PARA EL SISTEMA DE GRAFICACIÓN", MsgBoxStyle.OkOnly, "BDGEOCATMIN")
            lostrMensaje = MsgBox("¿ Desea graficar de todos modos esta área de interés ?", MsgBoxStyle.YesNo, "BDGEOCATMIN")
        End If
        Select Case lostrMensaje
            Case 6
                cls_Catastro.Delete_Rows_FC_GDB("Malla_" & Me.txtZona.Text)
                cls_Catastro.Delete_Rows_FC_GDB("Mallap_" & Me.txtZona.Text)
                cls_Catastro.Load_FC_GDB("Malla_" & Me.txtZona.Text, "", m_application, True)
                cls_Catastro.Load_FC_GDB("Mallap_" & Me.txtZona.Text, "", m_application, True)
                cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "1", False)

                cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Provincia, m_application, "1", False)
                cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito, m_application, "1", False)
                cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_AReservada & Me.txtZona.Text, m_application, "1", True)
                cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_ZUrbana & Me.txtZona.Text, m_application, "1", True)
                Dim lo_Filtro_DM = cls_Catastro.f_Intercepta_FC("Catastro", lo_xMin, lo_yMin, lo_xMax, lo_yMax, m_application, loStrShapefile)
                cls_Catastro.Quitar_Layer("Catastro", m_application)
                cls_Prueba.AddFieldDM(loStrShapefile) : cls_Catastro.Quitar_Layer(loStrShapefile, m_application)
                cls_Catastro.Add_ShapeFile(loStrShapefile, m_application)
                cls_Catastro.UpdateValue(lo_Filtro_DM, m_application, "Catastro")
                cls_Catastro.Update_Value_DM(m_application, "Catastro", Me.txtCodigo.Text)
                lodtbLeyenda = cls_Prueba.f_Genera_Leyenda_DM("Catastro", lo_Filtro_DM, m_application)
                cls_Prueba.Poligono_Color_1("Catastro", lodtbLeyenda, glo_pathStyle & "\Catastro.style", "LEYENDA", "", m_application)
                cls_Catastro.ShowLabel_DM("Catastro", m_application)
                cls_Catastro.Genera_Malla_UTM(lo_xMin, lo_yMin, lo_xMax, lo_yMax, Me.txtZona.Text, m_application)
                cls_Catastro.Rotular_texto_DM("Mallap_" & Me.txtZona.Text, Me.txtZona.Text, m_application)
                cls_Catastro.Quitar_Layer("Mallap_" & Me.txtZona.Text, m_application)
                'cls_Catastro.Style_Linea_GDB("Malla_" & Me.txtZona.Text, glo_pathStyle & "\malla.style", "CLASE", m_application, "GDB")
                Dim lo_Filtro_Zona_Urbana As String = cls_Catastro.f_Intercepta_FC("Zona Urbana", lo_xMin, lo_yMin, lo_xMax, lo_yMax, m_application)
                If lo_Filtro_Zona_Urbana = "" Then
                    cls_Catastro.Quitar_Layer("Zona Urbana", m_application)
                End If
                Dim lo_Filtro_Area_Reserva As String = cls_Catastro.f_Intercepta_FC("Zona Reservada", lo_xMin, lo_yMin, lo_xMax, lo_yMax, m_application)
                If lo_Filtro_Area_Reserva = "" Then
                    cls_Catastro.Quitar_Layer("Zona Reservada", m_application)
                End If
                cls_Catastro.DefinitionExpression(lo_Filtro_Area_Reserva, m_application, "Zona Reservada")
                cls_Catastro.DefinitionExpression(lo_Filtro_Zona_Urbana, m_application, "Zona Urbana")
                cls_Catastro.Shade_Poligono("Zona Urbana", m_application)
                cls_Catastro.Shade_Poligono("Zona Reservada", m_application)
                Dim v_Boo_Dpto As Boolean = True : Dim v_Boo_Prov As Boolean = True : Dim v_Boo_Dist As Boolean = True
                If v_Boo_Dpto = True Then
                    Dim lo_Filtro_Dpto As String = cls_Catastro.f_Intercepta_FC("Departamento", lo_xMin, lo_yMin, lo_xMax, lo_yMax, m_application)
                    cls_Catastro.DefinitionExpression(lo_Filtro_Dpto, m_application, "Departamento")
                    cls_Catastro.Shade_Poligono("Departamento", m_application)
                End If
                If v_Boo_Prov = True Then
                    Dim lo_Filtro_Prov As String = cls_Catastro.f_Intercepta_FC("Provincia", lo_xMin, lo_yMin, lo_xMax, lo_yMax, m_application)
                    cls_Catastro.DefinitionExpression(lo_Filtro_Prov, m_application, "Provincia")
                    cls_Catastro.Shade_Poligono("Provincia", m_application)
                End If
                If v_Boo_Dist = True Then
                    Dim lo_Filtro_Dist As String = cls_Catastro.f_Intercepta_FC("Distrito", lo_xMin, lo_yMin, lo_xMax, lo_yMax, m_application)
                    cls_Catastro.DefinitionExpression(lo_Filtro_Dist, m_application, "Distrito")
                    cls_Catastro.Shade_Poligono("Distrito", m_application)
                End If
                cls_Catastro.ShowLabel_DM("Distrito", m_application)
                'cls_Catastro.Zoom_to_Layer("Catastro")
                cls_Catastro.HazZoom(lo_xMin - loint_Intervalo, lo_yMin - loint_Intervalo, lo_xMax + loint_Intervalo, lo_yMax + loint_Intervalo, 0, m_application)
                lodtbLeyenda = Nothing
                Me.Close()
            Case 7
                cls_Catastro.Borra_Todo_Feature("", m_application)
        End Select
        'End If
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        If Me.txtConsulta.Text = "" Then Exit Sub
        Select Case Me.cboConsulta.SelectedIndex
            Case 0
                Try
                    Dim Form As New frm_Grafica_Segun_Nombre
                    Form.p_Consulta = Me.txtConsulta.Text
                    Form.m_application = m_application
                    Form.p_Tipo = 1
                    Form.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
                    Form.ShowDialog()
                    Select Case Form.DialogResult
                        Case Windows.Forms.DialogResult.OK
                            cls_Catastro.Borra_Todo_Feature("", m_application)
                            cls_Catastro.Limpiar_Texto_Pantalla(m_application)
                            visualizar_datos_dm(Form.dgdDetalle.Item(Form.dgdDetalle.Row, "CODIGO"), m_application)
                        Case Windows.Forms.DialogResult.Cancel
                            Dim losender As New System.Object
                            Dim loe As New System.EventArgs
                            Me.btnOtraConsulta_Click(losender, loe)
                    End Select
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Case 1
                Try
                    Dim Form As New frm_Grafica_Segun_Nombre
                    Form.p_Consulta = Me.txtConsulta.Text
                    Form.m_application = m_application
                    Form.p_Tipo = 2
                    Form.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
                    Form.ShowDialog()
                    Select Case Form.DialogResult
                        Case Windows.Forms.DialogResult.OK
                            cls_Catastro.Borra_Todo_Feature("", m_application)
                            cls_Catastro.Limpiar_Texto_Pantalla(m_application)
                            visualizar_datos_dm(Form.dgdDetalle.Item(Form.dgdDetalle.Row, "CODIGO"), m_application)
                        Case Windows.Forms.DialogResult.Cancel
                            Dim losender As New System.Object
                            Dim loe As New System.EventArgs
                            Me.btnOtraConsulta_Click(losender, loe)
                    End Select
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
        End Select
    End Sub
    Private Sub btnOtraConsulta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOtraConsulta.Click
        Me.txtConsulta.Text = ""
        Me.txtNombre.Text = ""
        Me.txtTitular.Text = ""
        Me.txtCodigo.Text = ""
        Me.txtZona.Text = ""
        Me.txtCarta.Text = ""
        Me.lstCoordenada.Items.Clear()
        Me.txtConsulta.Focus()
        Me.txtRadio.Enabled = False
        Me.img_DM.ImageLocation = glo_pathTMP & "\Vacio.jpg"
    End Sub
    Private Sub cboConsulta_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboConsulta.SelectedIndexChanged
        Me.txtConsulta.Text = ""
        Me.txtConsulta.Focus()
    End Sub

    Private Sub btnReporte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReporte.Click
        Dim frm_Rpt As New rpt_Reporte_1 ' rptOrden_Detalle
        frm_Rpt.C1Report1.DataSource.Recordset = New DataView(lodtbDatos)
        Dim m_ReportDefinitionFile As String
        m_ReportDefinitionFile = glo_Path & "\Reporte\rpt_Reporte.xml"
        frm_Rpt.C1Report1.Load(m_ReportDefinitionFile, "Derecho_Minero")
        frm_Rpt.C1Report1.DataSource.Recordset = New DataView(lodtbDatos)
        frm_Rpt.C1Report1.Fields("IMA_DM").Picture = glo_pathTMP & "\VistaPrevia.jpg"
        frm_Rpt.C1Report1.Fields("IMA_DM1").Picture = glo_pathTMP & "\VistaPrevia_1.jpg"
        Select Case glostrNaturaleza
            Case "M"
                frm_Rpt.C1Report1.Fields("NATURALEZA").Text = "Metálico"
            Case "NM"
                frm_Rpt.C1Report1.Fields("NATURALEZA").Text = "No Metálico"
        End Select
        frm_Rpt.C1Report1.Fields("PARTIDA").Text = glostrPartida
        frm_Rpt.C1Report1.Fields("PADRON").Text = glostrPadron
        frm_Rpt.C1Report1.Fields("JEFATURA").Text = glostrJefatura
        frm_Rpt.C1Report1.Fields("TIPO_DERECHO").Text = glostrTipoDer
        frm_Rpt.C1Report1.Fields("VERTICE").Text = lodtbDatos.Rows.Count
        frm_Rpt.Show()
        frm_Rpt.C1Report1.Render()
    End Sub

    Private Sub gbxDatosUbicacion_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gbxDatosUbicacion.Enter

    End Sub

    Private Sub txtConsulta_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtConsulta.TextChanged

    End Sub
End Class