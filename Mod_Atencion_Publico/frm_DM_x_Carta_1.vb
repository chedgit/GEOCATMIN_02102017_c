Imports ESRI.ArcGIS.GISClient
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Display
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Output
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Carto
Imports stdole
Imports PORTAL_Clases
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.DataSourcesRaster
Imports System.IO
Public Class frm_DM_x_Carta_1
    Public p_Campo As String
    Public p_Dato As String
    Public m_application As IApplication
    Private cls_Oracle As New cls_Oracle
    Private cls_Catastro As New cls_DM_1
    Private cls_Prueba As New cls_Prueba

    Private Sub frm_DM_x_Carta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lodtbOracle As New DataTable
        Me.lblUsuario.Text = "Usuario: " & gstrNombre_Usuario 'gstrUsuarioAcceso
        ' lodtbOracle = cls_Oracle.F_Obtiene_Carta(p_Campo, RellenarComodin(p_Dato.ToUpper, 4, "0"))
        lodtbOracle = cls_Oracle.F_Obtiene_Carta_oficial(p_Campo, RellenarComodin(p_Dato.ToUpper, 4, "0"), v_sistema)
        If lodtbOracle.Rows.Count = 0 Then
            MsgBox(".. No Existe Hoja, " & p_Dato, MsgBoxStyle.Information, "BDGEOCATMIN")
            DialogResult = Windows.Forms.DialogResult.OK
            Exit Sub
        End If
        Pintar_Formulario()
        Try
            Me.txtCodigo.Text = lodtbOracle.Rows(0).Item("CODIGO")
            Me.txtNombre.Text = lodtbOracle.Rows(0).Item("NOMBRE")
            Me.txtZona.Text = lodtbOracle.Rows(0).Item("ZONA")
            Me.txtXMin.Text = lodtbOracle.Rows(0).Item("XMIN")
            Me.txtXMax.Text = lodtbOracle.Rows(0).Item("XMAX")
            Me.txtYMin.Text = lodtbOracle.Rows(0).Item("YMIN")
            Me.txtyMax.Text = lodtbOracle.Rows(0).Item("YMAX")
            cls_Catastro.Actualizar_DM(Me.txtZona.Text)
            cls_Catastro.AddImagen(glo_pathIGN & Me.txtCodigo.Text.Replace("-", "") & ".ecw", "", Me.txtCodigo.Text, m_application, True)
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & Me.txtZona.Text, m_application, "1", True)
            'cls_Catastro.Conexion_SDE(m_application)
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_LHojas, m_application, "1", False)
            cls_Catastro.DefinitionExpression("CARTA = '" & p_Dato.ToUpper & "'", m_application, "Catastro")
            cls_Catastro.Poligono_Color_GDB(gstrFC_Catastro_Minero & Me.txtZona.Text, glo_pathStyle & "\CATASTRO1.style", "ESTADO", "", "Cadena", "", m_application, "")
            'cls_Catastro.Conexion_SDE(m_application)
            cls_Catastro.HazZoom(lodtbOracle.Rows(0).Item("XMIN") * 1000, lodtbOracle.Rows(0).Item("YMIN") * 1000, lodtbOracle.Rows(0).Item("XMAX") * 1000, lodtbOracle.Rows(0).Item("YMAX") * 1000, 0, m_application)
            cls_Catastro.Genera_Imagen_DM("VistaPrevia", "VistaPrevia")
            Me.imgCarta.ImageLocation = glo_pathTMP & "\VistaPrevia.jpg"
            lodtbOracle = Nothing
            cls_Oracle = Nothing
        Catch ex As Exception
        End Try
        cls_Catastro.Borra_Todo_Feature("", m_application)
        Me.clbCapas.SetItemChecked(0, True)
        Me.clbCapas.SetItemChecked(1, True)
    End Sub
    Private Sub btnOtraConsulta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOtraConsulta.Click
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub
    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
    Public Sub Pintar_Formulario()
        Me.BackColor = Drawing.Color.FromArgb(242, 242, 240)
    End Sub

    Private Sub btnGraficar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGraficar.Click
        Dim lodtbLeyenda As New DataTable
        Dim cls_Demis As New cls_Demis
        Dim v_Boo_Dpto As Boolean = False
        Dim v_Boo_Prov As Boolean = False
        Dim v_Boo_Dist As Boolean = False
        For i As Integer = 0 To clbCapas.Items.Count - 1
            Select Case clbCapas.GetItemText(i)
                Case 2
                    If clbCapas.GetItemChecked(i) = True Then
                        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "1", False)
                        v_Boo_Dpto = True
                    End If
                Case 3
                    If clbCapas.GetItemChecked(i) = True Then
                        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Provincia, m_application, "1", False)
                        v_Boo_Prov = True
                    End If
                Case 4
                    If clbCapas.GetItemChecked(i) = True Then
                        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito, m_application, "1", False)
                        v_Boo_Dist = True
                    End If
                Case 5
                    If clbCapas.GetItemChecked(i) = True Then
                        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Frontera, m_application, "1", False)
                    End If
                Case 6
                    If clbCapas.GetItemChecked(i) = True Then
                        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Cuadricula, m_application, "1", False)
                    End If
                Case 7
                    If clbCapas.GetItemChecked(i) = True Then
                        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Rios, m_application, "1", False)
                    End If
                Case 8
                    If clbCapas.GetItemChecked(i) = True Then
                        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Carretera, m_application, "1", False)
                    End If
                Case 9
                    If clbCapas.GetItemChecked(i) = True Then
                        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_CPoblado, m_application, "1", False)
                    End If
            End Select
        Next i
        cls_Catastro.Delete_Rows_FC_GDB("Malla_" & Me.txtZona.Text)
        cls_Catastro.Delete_Rows_FC_GDB("Mallap_" & Me.txtZona.Text)
        cls_Catastro.Load_FC_GDB("Malla_" & Me.txtZona.Text, "", m_application, True)
        cls_Catastro.Load_FC_GDB("Mallap_" & Me.txtZona.Text, "", m_application, True)
        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_AReservada & Me.txtZona.Text, m_application, "1", False)
        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_ZUrbana & Me.txtZona.Text, m_application, "1", False)
        Dim loStrShapefile As String = "Catastro" & DateTime.Now.Ticks.ToString()
        'cls_Catastro.PT_CargarFeatureClass_SDE("GPO_GEO_GEOLOGIA", m_application, "1", False)
        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & Me.txtZona.Text, m_application, "1", False)
        'cls_Catastro.Conexion_SDE(m_application)
        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_LHojas, m_application, "1", False)
        cls_Catastro.Actualizar_DM(Me.txtZona.Text)
        Dim lo_Filtro As String = "hoja = '" & txtCodigo.Text.ToLower & "'"
        cls_Prueba.IntersectSelect_por_Limite(m_application, lo_Filtro, "ESTADO <> 'Y'", "Cuadrangulo", xMin, yMin, xMax, yMax, txtExiste)
        Select Case txtExiste.Text
            Case -1
                cls_Catastro.Borra_Todo_Feature("", m_application)
                cls_Catastro.Limpiar_Texto_Pantalla(m_application)
                MsgBox("..No existe Derechos Mineros en esta Hoja..", MsgBoxStyle.Information, "BDGEOTCATMIN")
                DialogResult = Windows.Forms.DialogResult.Cancel
            Case Else
                'cls_Catastro.Expor_Tema(loStrShapefile, "Denun=Yes", m_application)
                cls_Catastro.Expor_Tema(loStrShapefile, sele_denu, m_application)
                Dim lo_Filtro_Zona_Urbana As String = cls_Catastro.f_Intercepta_FC("Zona Urbana", CType(txtXMin.Text, Double) * 1000, CType(txtYMin.Text, Double) * 1000, CType(txtXMax.Text, Double) * 1000, CType(txtyMax.Text, Double) * 1000, m_application)
                If lo_Filtro_Zona_Urbana = "" Then
                    cls_Catastro.Quitar_Layer("Zona Urbana", m_application)
                Else
                    cls_Catastro.DefinitionExpression(lo_Filtro_Zona_Urbana, m_application, "Zona Urbana")
                    Try
                        'cls_Catastro.Poligono_Color_GDB(gstrFC_ZUrbana & Me.txtZona.Text, glo_pathStyle & "\ZONA_URBANA.style", "NOMBRE", "", "Cadena", "", m_application, lo_Filtro_Zona_Urbana)
                    Catch ex As Exception
                    End Try
                End If

                Dim lo_Filtro_Area_Reserva As String = cls_Catastro.f_Intercepta_FC("Zona Reservada", CType(txtXMin.Text, Double) * 1000, CType(txtYMin.Text, Double) * 1000, CType(txtXMax.Text, Double) * 1000, CType(txtyMax.Text, Double) * 1000, m_application)
                If lo_Filtro_Area_Reserva = "" Then
                    cls_Catastro.Quitar_Layer("Zona Reservada", m_application)
                Else
                    cls_Catastro.DefinitionExpression(lo_Filtro_Area_Reserva, m_application, "Zona Reservada")
                    'cls_Catastro.Poligono_Color_GDB(gstrFC_AReservada & Me.txtZona.Text, glo_pathStyle & "\AREA_RESERVA.style", "NM_RESE", "", "Cadena", "", m_application, lo_Filtro_Area_Reserva)
                End If
                cls_Catastro.ClearLayerSelection(pFeatureLayer)
                cls_Catastro.AddImagen(glo_pathImaSat & "\" & Me.txtCodigo.Text.Replace("-", "") & ".jp2", 2, Me.txtCodigo.Text, m_application, False)
                cls_Catastro.Quitar_Layer("Catastro", m_application)
                cls_Catastro.Quitar_Layer("Cuadrangulo", m_application)
                cls_Prueba.AddFieldDM(loStrShapefile) : cls_Catastro.Quitar_Layer(loStrShapefile, m_application)
                cls_Catastro.Add_ShapeFile(loStrShapefile, m_application)
                cls_Catastro.UpdateValue(lo_Filtro, m_application, "Catastro", Me.txtCodigo.Text)
                lodtbLeyenda = cls_Prueba.f_Genera_Leyenda_DM("Catastro", lo_Filtro, m_application)
                cls_Prueba.Poligono_Color_1("Catastro", lodtbLeyenda, glo_pathStyle & "\Catastro.style", "LEYENDA", "Default", m_application)
                lodtbLeyenda = Nothing
                cls_Catastro.ShowLabel_DM("Catastro", m_application)
                cls_Catastro.Genera_Malla_UTM(CType(txtXMin.Text, Double) * 1000, CType(txtYMin.Text, Double) * 1000, CType(txtXMax.Text, Double) * 1000, CType(txtyMax.Text, Double) * 1000, Me.txtZona.Text, m_application)
                cls_Catastro.Rotular_texto_DM("Mallap_" & Me.txtZona.Text, Me.txtZona.Text, m_application)
                cls_Catastro.Quitar_Layer("Mallap_" & Me.txtZona.Text, m_application)
                If v_Boo_Dpto = True Then
                    Dim lo_Filtro_Dpto As String = cls_Catastro.f_Intercepta_FC("Departamento", CType(txtXMin.Text, Double) * 1000, CType(txtYMin.Text, Double) * 1000, CType(txtXMax.Text, Double) * 1000, CType(txtyMax.Text, Double) * 1000, m_application)
                    cls_Catastro.DefinitionExpression(lo_Filtro_Dpto, m_application, "Departamento")
                    cls_Catastro.Poligono_Color_GDB(gstrFC_Departamento, glo_pathStyle & "\DEPARTAMENTO.style", "NM_DEPA", "", "Cadena", "", m_application, lo_Filtro_Dpto)
                End If
                If v_Boo_Prov = True Then
                    Dim lo_Filtro_Prov As String = cls_Catastro.f_Intercepta_FC("Provincia", CType(txtXMin.Text, Double) * 1000, CType(txtYMin.Text, Double) * 1000, CType(txtXMax.Text, Double) * 1000, CType(txtyMax.Text, Double) * 1000, m_application)
                    cls_Catastro.DefinitionExpression(lo_Filtro_Prov, m_application, "Provincia")
                    cls_Catastro.Poligono_Color_GDB(gstrFC_Provincia, glo_pathStyle & "\PROVINCIA.style", "NM_PROV", "", "Cadena", "", m_application, lo_Filtro_Prov)
                End If
                If v_Boo_Dist = True Then
                    Dim lo_Filtro_Dist As String = cls_Catastro.f_Intercepta_FC("Distrito", CType(txtXMin.Text, Double) * 1000, CType(txtYMin.Text, Double) * 1000, CType(txtXMax.Text, Double) * 1000, CType(txtyMax.Text, Double) * 1000, m_application)
                    cls_Catastro.DefinitionExpression(lo_Filtro_Dist, m_application, "Distrito")
                    cls_Catastro.Shade_Poligono("Distrito", m_application)
                End If
                'cls_Demis.PC_AddWMSLayer("http://www2.demis.nl/wms/wms.asp?wms=WorldMap&Demis World Map", m_application)
                pMxDoc.ActivatedView.Refresh()
                DialogResult = Windows.Forms.DialogResult.Cancel
        End Select
    End Sub
End Class