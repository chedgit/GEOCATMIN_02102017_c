Imports PORTAL_Clases
Imports ESRI.ArcGIS.Framework
Public Class frm_CartaIGN_Segun_Codigo
    Public m_application As IApplication
    Private cls_Oracle As New cls_Oracle
    Private cls_Catastro As New cls_DM_1
    Private cls_DM_2 As New cls_DM_2
    Private cls_Prueba As New cls_Prueba
    Private lodtbDatos As New DataTable
    Private cls_eval As New Cls_evaluacion
    Private cls_planos As New Cls_planos

    Private Sub txtConsulta_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtConsulta.KeyPress
        'If e.KeyChar = Chr(13) Then
        'Select Case Me.cboConsulta.SelectedIndex
        '   Case 0
        'Dim losender As New System.Object
        'Dim loe As New System.EventArgs
        'Me.btnBuscar_Click(losender, loe)
        '    Case 1
        'Dim losender As New System.Object
        'Dim loe As New System.EventArgs
        'Me.btnBuscar_Click(losender, loe)
        'End Select
        'End If
    End Sub

    Private Sub visualizar_datos_dm1(ByVal p_consulta As String, ByVal p_App As IApplication)
        'Me.lstCoordenada.Items.Clear()
        lodtbDatos = cls_Oracle.F_Obtiene_Datos_DM(p_consulta)
        Select Case lodtbDatos.Rows.Count
            Case 0
                MsgBox("No Existe Codigo: " & p_consulta & " en la Base de Datos....")
                Me.txtConsulta.Focus()
            Case Else
                cls_Catastro.Actualizar_DM(lodtbDatos.Rows(0).Item("PE_ZONCAT"))
                v_zona_dm = lodtbDatos.Rows(0).Item("PE_ZONCAT").ToString
                v_codigo = lodtbDatos.Rows(0).Item("CG_CODIGO").ToString

        End Select
        'End If
    End Sub


    'Private Sub frm_Graficar_Segun_Codigo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Private Sub frm_CartaIGN_Segun_Codigo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim m_form As New GEOCATMIN_IniLogin
        If gloint_Acceso = 0 Then m_form.ShowDialog()
        If m_form.DialogResult = Windows.Forms.DialogResult.OK Or gloint_Acceso = "1" Then
            gloint_Acceso = "1"
            Me.lblUsuario.Text = "Usuario: " & gstrNombre_Usuario 'gstrUsuarioAcceso
            'Me.clbLayer.SetItemCheckState(0, Windows.Forms.CheckState.Indeterminate)
            'Me.clbLayer.SetItemCheckState(1, Windows.Forms.CheckState.Indeterminate)
            loint_Intervalo = 0
            Dim cls_Catastro As New cls_DM_1
            Pintar_Formulario()
            Me.img_DM.ImageLocation = glo_pathTMP & "\Vacio.jpg"
            If caso_consulta = "CARTA IGN" Then
                img_DM.ImageLocation = glo_pathTMP & "\carta_ign.jpg"
                m_form.Text = "CONSULTA DM SEGÚN CARTA IGN"
            Else
                img_DM.ImageLocation = glo_pathTMP & "\demarca.gif"
                m_form.Text = "CONSULTA DM SEGÚN DEMARCACION POLITICA"
            End If
            'cls_Catastro.Borra_Todo_Feature("", m_application)
            'cls_Catastro.Limpiar_Texto_Pantalla(m_application)
            'Me.img_DM.ImageLocation = glo_Path & "\Vista_Previa\Vacio.jpg"
            'Me.btnGraficar.Enabled = False
            'Me.btnReporte.Enabled = False
        Else
            Me.Close()
        End If
    End Sub
    Public Sub Pintar_Formulario()
        Me.BackColor = Drawing.Color.FromArgb(242, 242, 240)
    End Sub

    Private Sub btnGraficar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGraficar.Click
        If Me.txtConsulta.Text = "" Then
            MsgBox("No ingreso ningun codigo de DM para consultar", MsgBoxStyle.Critical, "Observación..")
            Exit Sub
        End If

        cls_planos.mueveposiciondataframe(caso_consulta, m_application)
        Select Case Me.cboConsulta.SelectedIndex
            Case 0
                Try
                    Dim Form As New frm_Grafica_Segun_Nombre
                    Form.p_Consulta = Me.txtConsulta.Text
                    Form.m_application = m_application
                    Form.p_Tipo = 1
                    Form.ShowDialog()
                    Select Case Form.DialogResult
                        Case Windows.Forms.DialogResult.OK
                            cls_Catastro.Borra_Todo_Feature("", m_application)
                            cls_Catastro.Limpiar_Texto_Pantalla(m_application)
                            visualizar_datos_dm1(Form.dgdDetalle.Item(Form.dgdDetalle.Row, "CODIGO"), m_application)
                        Case Windows.Forms.DialogResult.Cancel
                            Dim losender As New System.Object
                            Dim loe As New System.EventArgs
                            'Me.btnOtraConsulta_Click(losender, loe)
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
                    Form.ShowDialog()
                    Select Case Form.DialogResult
                        Case Windows.Forms.DialogResult.OK
                            cls_Catastro.Borra_Todo_Feature("", m_application)
                            cls_Catastro.Limpiar_Texto_Pantalla(m_application)
                            visualizar_datos_dm1(Form.dgdDetalle.Item(Form.dgdDetalle.Row, "CODIGO"), m_application)
                        Case Windows.Forms.DialogResult.Cancel
                            Dim losender As New System.Object
                            Dim loe As New System.EventArgs
                            'Me.btnOtraConsulta_Click(losender, loe)
                    End Select
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
        End Select
        Me.Close()
        Dim loStrShapefile As String = "Catastro" & DateTime.Now.Ticks.ToString()
        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & v_zona_dm, m_application, "1", False)
        'cls_Catastro.Conexion_SDE(m_application)
        Try
            Kill(glo_pathTMP & "\DM_" & v_codigo & "*.*")
        Catch ex As Exception
        End Try
        cls_eval.consultacapaDM(v_codigo, "DM", "Catastro")
        'lista_nm_depa = v_codigo
        arch_cata = "Cata"
        cls_eval.DefinitionExpressiontema(v_codigo, m_application, "Catastro")
        cls_eval.obtienelimitesmaximos("Catastro")

        If caso_consulta = "CARTA IGN" Then
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Carta, m_application, "1", False)
            Dim lo_Filtro_Dpto2 As String = cls_eval.f_Intercepta_temas("Cuadrangulo", v_este_min, v_norte_min, v_este_max, v_norte_max, m_application, loStrShapefile)
            lista_hojas = lo_Filtro_Dpto2
        End If
        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "1", False)
        Dim lo_Filtro_Dpto1 As String = cls_eval.f_Intercepta_temas("Departamento", v_este_min, v_norte_min, v_este_max, v_norte_max, m_application, loStrShapefile)
        lista_nm_depa = lo_Filtro_Dpto1
        'Obtiene lista de departamentos
        Dim nm_depa As String = ""
        If colecciones_depa.Count = 0 Then
            lista_depa = ""
        End If
        For contador As Integer = 1 To colecciones_depa.Count
            nm_depa = colecciones_depa.Item(contador)
            If contador = 1 Then
                lista_depa = nm_depa
            ElseIf contador > 1 Then
                lista_depa = lista_depa & "," & nm_depa
            End If

        Next contador
        colecciones_depa.Clear()

        cls_Catastro.Quitar_Layer("Catastro", m_application)
        cls_Catastro.Quitar_Layer("Departamento", m_application)
        cls_Catastro.Quitar_Layer("Hojas", m_application)

        cls_planos.buscaadataframe(caso_consulta, False)
        If valida = False Then
            cls_eval.adicionadataframe(caso_consulta)
            cls_eval.activadataframe(caso_consulta)
        End If
        'pMap = pMxDoc.FocusMap
        If caso_consulta = "CARTA IGN" Then
            Dim cont_v As Integer
            Dim CARTA_V1 As String
            'Dim nmhojas1 As String
            Dim con_lista As Integer
            con_lista = colecciones.Count
            For cont_v = 1 To con_lista
                CARTA_V1 = colecciones.Item(cont_v)
                If cont_v = 1 Then
                    lista_cartas = "CD_HOJA =  '" & CARTA_V1 & "'"
                Else
                    lista_cartas = lista_cartas & " OR " & "CD_HOJA =  '" & CARTA_V1 & "'"
                End If
                carta_v = CARTA_V1.Replace("-", "")
                'cls_Catastro.AddImagen("U:\DATOS\ecw_cartas\" & carta_v & ".ECW", "1", m_application, True)
                cls_Catastro.AddImagen(glo_pathIGN & carta_v & ".ecw", "1", carta_v, m_application, True)
            Next cont_v
            colecciones.Clear()
            Dim nmhojas1 As String
            For cont_v = 1 To con_lista
                nmhojas1 = colecciones_nmhojas.Item(cont_v)
                If cont_v = 1 Then
                    lista_nmhojas = nmhojas1
                Else
                    lista_nmhojas = lista_nmhojas & "," & nmhojas1
                End If
            Next cont_v
            colecciones_nmhojas.Clear()
            loStrShapefile = "DM_" & v_codigo
            cls_Catastro.Add_ShapeFile(loStrShapefile, m_application)
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "1", True)
            cls_eval.DefinitionExpressiontema(lista_nm_depa, m_application, "Departamento")
            cls_Catastro.Shade_Poligono("Departamento", m_application)
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Provincia, m_application, "1", True)
            cls_eval.DefinitionExpressiontema(lista_nm_depa, m_application, "Provincia")
            cls_Catastro.Shade_Poligono("Provincia", m_application)
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito, m_application, "1", True)
            cls_eval.DefinitionExpressiontema(lista_nm_depa, m_application, "Distrito")
            cls_Catastro.Shade_Poligono("Distrito", m_application)
            'cls_Catastro.Quitar_Layer("Catastro", m_application)
        ElseIf caso_consulta = "DEMARCACION POLITICA" Then
            arch_cata = ""
            loStrShapefile = "DM_" & v_codigo
            cls_Catastro.Add_ShapeFile(loStrShapefile, m_application)
            cls_eval.obtienelimitesmaximos("Catastro")
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "1", True)
            cls_eval.DefinitionExpressiontema(lista_nm_depa, m_application, "Departamento")
            cls_Catastro.Shade_Poligono("Departamento", m_application)
            MsgBox(lista_nm_depa)
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito, m_application, "1", False)
            cls_eval.f_Intercepta_temas("Distrito", v_este_min, v_norte_min, v_este_max, v_norte_max, m_application, loStrShapefile)
            Dim nm_dist As String = ""
            If colecciones_dist.Count = 0 Then
                lista_dist = ""
            End If
            For contador As Integer = 1 To colecciones_dist.Count
                nm_dist = colecciones_dist.Item(contador)
                If contador = 1 Then
                    lista_dist = nm_dist
                ElseIf contador > 1 Then
                    lista_dist = lista_dist & "," & nm_dist
                End If
            Next contador
            colecciones_dist.Clear()
            'Obtiene lista de provincias x dm
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Provincia, m_application, "1", False)
            cls_eval.f_Intercepta_temas("Provincia", v_este_min, v_norte_min, v_este_max, v_norte_max, m_application, loStrShapefile)
            Dim nm_prov As String = ""
            If colecciones_prov.Count = 0 Then
                lista_prov = ""
            End If
            For contador As Integer = 1 To colecciones_prov.Count
                nm_prov = colecciones_prov.Item(contador)
                If contador = 1 Then
                    lista_prov = nm_prov
                ElseIf contador > 1 Then
                    lista_prov = lista_prov & "," & nm_prov
                End If
            Next contador
            colecciones_prov.Clear()
            cls_eval.DefinitionExpressiontema(lista_nm_depa, m_application, "Provincia")
            cls_Catastro.Shade_Poligono("Provincia", m_application)
            cls_eval.DefinitionExpressiontema(lista_nm_depa, m_application, "Distrito")
            cls_Catastro.Shade_Poligono("Distrito", m_application)
            cls_Catastro.ShowLabel_DM("Provincia", m_application)
            cls_Catastro.ShowLabel_DM("Departamento", m_application)
        End If
        cls_Catastro.Quitar_Layer("Catastro", m_application)
        loStrShapefile = "DM_" & v_codigo
        cls_Catastro.Add_ShapeFile(loStrShapefile, m_application)
        cls_Catastro.Zoom_to_Layer("Catastro")
        cls_Catastro.ShowLabel_DM("Distrito", m_application)
        pMxDoc.UpdateContents()
        pMap.MapScale = 100000
        escala_plano_carta = pMap.MapScale
    End Sub
End Class