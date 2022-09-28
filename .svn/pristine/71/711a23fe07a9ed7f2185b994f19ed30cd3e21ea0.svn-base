Imports ESRI.ArcGIS.Framework

Public Class frm_Grafica_DM_Varias_Hojas
    Dim cls_Catastro As New cls_DM_1
    Dim cls_Prueba As New cls_Prueba
    Public m_application As IApplication

    Private Sub frm_Graficar_DM_Varias_Hojas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim m_form As New GEOCATMIN_IniLogin ' LoginForm
        m_form.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
        If gloint_Acceso = 0 Then m_form.ShowDialog()
        If m_form.DialogResult = Windows.Forms.DialogResult.OK Or gloint_Acceso = "1" Then
            gloint_Acceso = "1"
            Me.lblUsuario.Text = "Usuario: " & gstrNombre_Usuario 'gstrUsuarioAcceso
            Pintar_Formulario()
            cls_Catastro.Borra_Todo_Feature("", m_application)
            cls_Catastro.Limpiar_Texto_Pantalla(m_application)
            cls_Catastro.Conexion_SDE(m_application)
            cls_Catastro.Cargar_ListBox("DOM_CUADRANGULO", lstHojas)
        Else
            Me.Close()
        End If
    End Sub
    Public Sub Pintar_Formulario()
        Me.BackColor = Drawing.Color.FromArgb(242, 242, 240)
        Me.lstHojas.BackColor = Drawing.Color.FromArgb(242, 242, 240)
        'Me.dgd_Orden_Trabajo.BackColor = Color.FromArgb(242, 242, 240)
        'Me.dgd_Orden_Trabajo.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        'Me.dgd_Orden_Trabajo.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        'Me.dgd_Orden_Trabajo.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
    End Sub
    Private Sub btnGraficar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGraficar.Click
        ' Exit Sub
        Dim lostr_Zona As String = ""
        Dim loStrShapefile As String = "Catastro" & DateTime.Now.Ticks.ToString()
        Dim lo_Filtro_cartas As String = ""
        For i As Integer = 0 To Me.lstHojas.Items.Count - 1
            If Me.lstHojas.GetSelected(i) Then
                lo_Filtro_cartas = lo_Filtro_cartas & "'" & Mid(lstHojas.Items(i).ToString, 3, 4) & "',"
            End If
            If i = 0 Then
                'Dim loBuscar01 As String = ""
                'Dim loBuscar02 As String = ""
                'Dim loBuscar03 As String = ""
                ''loBuscar01 = InStr("ABCDEFG", Mid(Me.txtCarta.Text.ToUpper, InStr(Me.txtCarta.Text.ToUpper, "-") + 1, 1))
                ''loBuscar02 = InStr("HIJKLMNÑOPQR", Mid(Me.txtCarta.Text.ToUpper, InStr(Me.txtCarta.Text.ToUpper, "-") + 1, 1))
                ''loBuscar03 = InStr("STUVWXYZ", Mid(Me.txtCarta.Text.ToUpper, InStr(Me.txtCarta.Text.ToUpper, "-") + 1, 1))
                'If loBuscar01 >= 1 Then lostr_Zona = 17
                'If loBuscar02 >= 1 Then lostr_Zona = 18
                'If loBuscar03 >= 1 Then lostr_Zona = 19
                lostr_Zona = "18"
            End If

        Next
        lo_Filtro_cartas = "CD_HOJA IN (" & Mid(lo_Filtro_cartas, 1, Len(lo_Filtro_cartas) - 1) & ")"
        cls_Catastro.Delete_Rows_FC_GDB("Malla_" & lostr_Zona)
        cls_Catastro.Delete_Rows_FC_GDB("Mallap_" & lostr_Zona)
        cls_Catastro.Load_FC_GDB("Malla_" & lostr_Zona, "", m_application, True)
        cls_Catastro.Load_FC_GDB("Mallap_" & lostr_Zona, "", m_application, True)

        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_LHojas, m_application, "1", False)
        cls_Catastro.PT_CargarFeatureClass_SDE("GPO_CMI_CATASTRO_MINERO_18", m_application, "1", False)

        cls_Prueba.IntersectSelect_por_Limite(m_application, lo_Filtro_cartas, "", "Cuadrangulo", xMin, yMin, xMax, yMax, txtExiste)
        'cls_Catastro.Expor_Tema(loStrShapefile, "Denun=No", m_application)
        cls_Catastro.Expor_Tema(loStrShapefile, sele_denu, m_application)
        cls_Catastro.Quitar_Layer("Catastro", m_application)
        cls_Catastro.Quitar_Layer("Cuadrangulo", m_application)
        cls_Prueba.AddFieldDM(loStrShapefile) : cls_Catastro.Quitar_Layer(loStrShapefile, m_application)
        cls_Catastro.Add_ShapeFile(loStrShapefile, m_application)
        cls_Catastro.UpdateValue(lo_Filtro_cartas, m_application, "Catastro", "")
        Dim lodtbLeyenda As New DataTable
        lodtbLeyenda = cls_Prueba.f_Genera_Leyenda_DM("Catastro", lo_Filtro_cartas, m_application)
        cls_Prueba.Poligono_Color_1("Catastro", lodtbLeyenda, glo_pathStyle & "\Catastro.style", "LEYENDA", "", m_application)
        lodtbLeyenda = Nothing
        cls_Catastro.ShowLabel_DM("Catastro", m_application)
        cls_Catastro.Genera_Malla_UTM(CType(Me.xMin.Text, Double), CType(yMin.Text, Double), CType(xMax.Text, Double), CType(yMax.Text, Double), lostr_Zona, m_application)
        cls_Catastro.Rotular_texto_DM("Mallap_" & lostr_Zona, lostr_Zona, m_application)
        cls_Catastro.Quitar_Layer("Mallap_" & lostr_Zona, m_application)
        ' cls_Catastro.Style_Linea_GDB("Malla_" & lostr_Zona, glo_pathStyle & "\malla.style", "CLASE", m_application, "GDB")
    End Sub

    Private Sub xMax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xMax.TextChanged

    End Sub

    Private Sub lstHojas_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstHojas.SelectedIndexChanged
        'Dim losender As New System.Object
        'Dim loe As New System.EventArgs
        'Me.btnGraficar_Click(losender, loe)
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click

    End Sub
End Class