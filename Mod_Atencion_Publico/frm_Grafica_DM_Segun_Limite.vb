Imports ESRI.ArcGIS.Framework
Public Class frm_Grafica_DM_Segun_Limite
    Public m_application As IApplication
    Private cls_Catastro As New cls_DM_1

    Private Sub btnGraficar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGraficar.Click
        Dim lodtbLeyenda As New DataTable
        Dim cls_Prueba As New cls_Prueba
        Dim lo_xMin = CType(Me.txtEsteMin.Text, Double) : Dim lo_xMax = CType(Me.txtEsteMax.Text, Double)
        Dim lo_yMin = CType(Me.txtNorteMin.Text, Double) : Dim lo_yMax = CType(Me.txtNorteMax.Text, Double)
        Dim loStrShapefile As String = "Catastro" & DateTime.Now.Ticks.ToString()
        cls_Catastro.Delete_Rows_FC_GDB("Malla_" & Me.cboZona.Text)
        cls_Catastro.Delete_Rows_FC_GDB("Mallap_" & Me.cboZona.Text)
        cls_Catastro.Load_FC_GDB("Malla_" & Me.cboZona.Text, "", m_application, True)
        cls_Catastro.Load_FC_GDB("Mallap_" & Me.cboZona.Text, "", m_application, True)
        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "1", False)
        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Provincia, m_application, "1", False)
        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito, m_application, "1", False)
        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_AReservada & Me.cboZona.Text, m_application, "1", True)
        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_ZUrbana & Me.cboZona.Text, m_application, "1", True)
        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & Me.cboZona.Text, m_application, "1", False)
        'cls_Catastro.Conexion_SDE(m_application)
        cls_Catastro.Actualizar_DM(Me.cboZona.Text)
        Dim lo_Filtro As String = cls_Catastro.f_Intercepta_FC("Catastro", lo_xMin, lo_yMin, lo_xMax, lo_yMax, m_application, loStrShapefile)
        'If lo_Filtro = "" Then
        '    MsgBox("No hay Ningún Derecho Minero en estas Coordenadas", MsgBoxStyle.Information, "BDGEOCATMIN")
        '    Exit Sub
        'End If
        'cls_Catastro.DefinitionExpression(lo_Filtro, m_application, loStrShapefile)
        cls_Catastro.Quitar_Layer("Catastro", m_application)
        cls_Prueba.AddFieldDM(loStrShapefile) : cls_Catastro.Quitar_Layer(loStrShapefile, m_application)
        cls_Catastro.Add_ShapeFile(loStrShapefile, m_application)
        'cls_Catastro.HazZoom(lo_xMin, lo_yMin, lo_xMax, lo_yMax, 0, m_application)
        cls_Catastro.UpdateValue(lo_Filtro, m_application, "Catastro")
        lodtbLeyenda = cls_Prueba.f_Genera_Leyenda_DM("Catastro", lo_Filtro, m_application)
        cls_Prueba.Poligono_Color_1("Catastro", lodtbLeyenda, glo_pathStyle & "\Catastro.style", "LEYENDA", "", m_application)
        'cls_Catastro.Genera_Tematico_Catastro(lo_Filtro, m_application)
        'cls_Catastro.Poligono_Color_GDB(gstrFC_Catastro_Minero & Me.cboZona.Text, glo_Stile & "\CATASTRO.style", "LEYENDA", "", "Cadena", "Default", m_application, lo_Filtro)
        cls_Catastro.ShowLabel_DM("Catastro", m_application)
        cls_Catastro.Genera_Malla_UTM(lo_xMin, lo_yMin, lo_xMax, lo_yMax, Me.cboZona.Text, m_application)
        cls_Catastro.Rotular_texto_DM("Mallap_" & Me.cboZona.Text, Me.cboZona.Text, m_application)
        cls_Catastro.Quitar_Layer("Mallap_" & Me.cboZona.Text, m_application)
        'cls_Catastro.Style_Linea_GDB("Malla_" & Me.cboZona.Text, glo_pathStyle & "\malla.style", "CLASE", m_application, "GDB")
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
            'cls_Catastro.Poligono_Color_GDB("GPO_DIS_DEPARTAMENTO_CAT", glo_Stile & "\DEPARTAMENTO.style", "NM_DEPA", "", m_application)
        End If
        If v_Boo_Prov = True Then
            Dim lo_Filtro_Prov As String = cls_Catastro.f_Intercepta_FC("Provincia", lo_xMin, lo_yMin, lo_xMax, lo_yMax, m_application)
            cls_Catastro.DefinitionExpression(lo_Filtro_Prov, m_application, "Provincia")
            cls_Catastro.Shade_Poligono("Provincia", m_application)
            'cls_Catastro.Poligono_Color_GDB("GPO_DIS_DEPARTAMENTO_CAT", glo_Stile & "\DEPARTAMENTO.style", "NM_DEPA", "", m_application)
        End If
        If v_Boo_Dist = True Then
            Dim lo_Filtro_Dist As String = cls_Catastro.f_Intercepta_FC("Distrito", lo_xMin, lo_yMin, lo_xMax, lo_yMax, m_application)
            cls_Catastro.DefinitionExpression(lo_Filtro_Dist, m_application, "Distrito")
            cls_Catastro.Shade_Poligono("Distrito", m_application)
            'cls_Catastro.Poligono_Color_GDB(gstrFC_Distrito, glo_Stile & "\DISTRITO.style", "NM_DIST", "", m_application)
        End If
        cls_Catastro.ShowLabel_DM("Departamento", m_application)
        cls_Catastro.ShowLabel_DM("Provincia", m_application)
        cls_Catastro.ShowLabel_DM("Distrito", m_application)
        cls_Catastro.Zoom_to_Layer("Catastro")
        Me.Close()
        'DialogResult = Windows.Forms.DialogResult.OK

    End Sub

    Private Sub frm_Graficar_DM_Segun_Limite_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim m_form As New GEOCATMIN_IniLogin ' LoginForm
        m_form.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
        If gloint_Acceso = 0 Then m_form.ShowDialog()
        If m_form.DialogResult = Windows.Forms.DialogResult.OK Or gloint_Acceso = "1" Then
            gloint_Acceso = "1"
            Me.lblUsuario.Text = "Usuario: " & gstrNombre_Usuario 'gstrUsuarioAcceso
            Dim cls_Catastro As New cls_DM_1
            Pintar_Formulario()
            cls_Catastro.Borra_Todo_Feature("", m_application)
            cls_Catastro.Limpiar_Texto_Pantalla(m_application)
            Me.cboZona.SelectedIndex = 1
            Me.txtEsteMin.Focus()
        Else
            Me.Close()
        End If
    End Sub
    Public Sub Pintar_Formulario()
        Me.BackColor = Drawing.Color.FromArgb(242, 242, 240)
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub
End Class