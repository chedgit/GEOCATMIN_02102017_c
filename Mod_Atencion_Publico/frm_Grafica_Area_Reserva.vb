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


Public Class frm_Grafica_Area_Reserva
    Public m_application As IApplication
    Private cls_Oracle As New cls_Oracle
    Private cls_Catastro As New cls_DM_1
    Private cls_Prueba As New cls_Prueba

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
    '    Private Sub frm_Grafica_Area_Reserva_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '        Dim cls_Catastro As New cls_DM_1
    '        Dim m_form As New GEOCATMIN_IniLogin ' LoginForm
    '        If gloint_Acceso = 0 Then m_form.ShowDialog()
    '        If m_form.DialogResult = Windows.Forms.DialogResult.OK Or gloint_Acceso = "1" Then
    '            gloint_Acceso = "1"
    '            escalaf = 0
    '            Call validareservas()
    '            Call borratemastodo()
    '            Call borrandotextos()
    '            '222FRM_CONSULTAAREARESERVA.Show()
    '        Else
    '            'FRM_PERMISO.Show()
    '        End If
    '    End Sub
    '    Public Sub borratemastodo()
    '        On Error GoTo EH
    '        Dim m_pEnumLayers As IEnumLayer
    '        Dim m_pLayer As ILayer
    '        pMxDoc = p_App.Document 'ThisDocument
    '        pMap = pMxDoc.FocusMap
    '        If pMap.Name = "CATASTRO MINERO" Then
    '            If pMap.LayerCount > 0 Then
    '                m_pEnumLayers = pMap.Layers
    '                m_pLayer = m_pEnumLayers.Next
    '                Do Until m_pLayer Is Nothing
    '                    pMap.DeleteLayer(m_pLayer)
    '                    m_pLayer = m_pEnumLayers.Next
    '                Loop
    '            End If
    '        End If
    '        pMxDoc.UpdateContents()
    '        Exit Sub
    'EH:
    '        MsgBox("No se ha se eliminado los temas correctamente", vbCritical, "Observacion")

    '    End Sub

    '    Public Sub validareservas()
    '        pMxDoc = p_App.Document ' ThisDocument
    '        pMap = pMxDoc.FocusMap
    '        If pMap.LayerCount > 0 Then
    '            Nom_archivo = "ZONA RESERVADA"
    '            Call buscatema()
    '            If nombre2 = Nom_archivo Then
    '                pFeatureLayer = pMap.Layer(INDICADOR)
    '                pFeatureLayer.Visible = False
    '                pMxDoc.UpdateContents()
    '                pMap.DeleteLayer(pFeatureLayer)
    '            End If
    '        End If
    '    End Sub
    '    Public Sub borrandotextos()
    '        On Error GoTo EH
    '        'PROGRAMA PARA BORRAR TEXTOS EN LA VISTA DEL ARCMAP
    '        '***************************************************
    '        pMxDoc = p_App.Document 'ThisDocument
    '        '= Application.Document
    '        Dim pElement As IElement
    '        Dim pGraphicsContainer As IGraphicsContainer
    '        Dim pGraphicsContainerSel As IGraphicsContainerSelect
    '        pGraphicsContainer = pMxDoc.ActiveView
    '        pGraphicsContainerSel = pMxDoc.ActiveView
    '        pGraphicsContainer.Reset()
    '        Dim pTextElement As ITextElement
    '        pElement = pGraphicsContainer.Next
    '        While Not pElement Is Nothing
    '            'While pElement Is Nothing
    '            If TypeOf pElement Is ITextElement Then
    '                pTextElement = pElement
    '                Dim pElementProps As IElementProperties
    '                pElementProps = pTextElement
    '                If pElementProps.Type = "Text" Then
    '                    pGraphicsContainerSel.SelectElement(pElement)
    '                    pGraphicsContainer.DeleteElement(pTextElement)
    '                    pGraphicsContainer.Reset()
    '                    pTextElement = Nothing
    '                End If
    '            End If
    '            pElement = pGraphicsContainer.Next
    '        End While
    '        pMxDoc.ActiveView.Refresh()
    '        Exit Sub
    'EH:
    '        MsgBox("No se ha eliminado los textos del arcmap", vbCritical, "Observacion")
    '    End Sub

    '    Public Sub buscatema()
    '        On Error GoTo EH
    '        'PROGRAMA PARA OBTENER NOMBRE DEL TEMA DEL DM EN LA VISTA
    '        Dim NOMBRE1 As String
    '        Dim NOMBRE As String
    '        Dim i As Long
    '        pMxDoc = p_App.Document ' ThisDocument
    '        pMap = pMxDoc.FocusMap
    '        NOMBRE = Nom_archivo
    '        For i = 0 To pMap.LayerCount - 1
    '            NOMBRE1 = UCase(pMap.Layer(i).Name)
    '            If (NOMBRE1 = (UCase(NOMBRE))) Then
    '                nombre2 = Nom_archivo
    '                INDICADOR = i
    '                'MsgBox INDICADOR
    '            End If
    '        Next
    '        Exit Sub
    'EH:
    '        MsgBox("Error en busquedas de temas en la vista", vbCritical, "Observacion")
    '    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        If Len(Me.txtConsulta.Text) < 2 Then
            MsgBox("Insuficiente número de carácteres de búsqueda..", MsgBoxStyle.Information, "[BDGEOCATMIN]")
            Exit Sub
        End If
        lsvListaAreas.Items.Clear()
        Dim lodtbArea_Reserva As New DataTable
        Select Case Me.cboConsulta.SelectedIndex
            Case 0
                'lodtbArea_Reserva = cls_Oracle.F_Obtiene_Area_Reserva(iif(cboConsulta.SelectedIndex=0 then "CODIGO", "NOMBRE"), Me.txtConsulta.Text)
                lodtbArea_Reserva = cls_Oracle.F_Obtiene_Area_Reserva("CODIGO", Me.txtConsulta.Text)
            Case 1
                lodtbArea_Reserva = cls_Oracle.F_Obtiene_Area_Reserva("NOMBRE", Me.txtConsulta.Text)
        End Select
        Dim item As Windows.Forms.ListViewItem
        If lodtbArea_Reserva.Rows.Count = 0 Then
            MsgBox("No Existe ningún Item con esta consulta ", MsgBoxStyle.Information, "[BDGEOCIENTÍFICA]")
            Me.txtConsulta.Text = ""
            Me.btnGraficar.Enabled = False
            Exit Sub
        End If
        Me.btnGraficar.Enabled = True
        For i As Integer = 0 To lodtbArea_Reserva.Rows.Count - 1
            'Me.dgdListaArea.DataSource = lodtbArea_Reserva
            Dim ArrFila(7) As String
            'Dim itm As Windows.Forms.ListViewItem
            ArrFila(0) = lodtbArea_Reserva.Rows(i).Item("CG_CODIGO").ToString
            ArrFila(1) = lodtbArea_Reserva.Rows(i).Item("PE_NOMARE").ToString
            ArrFila(2) = lodtbArea_Reserva.Rows(i).Item("ZA_ZONA").ToString
            ArrFila(3) = lodtbArea_Reserva.Rows(i).Item("PA_DESCRI").ToString
            ArrFila(4) = lodtbArea_Reserva.Rows(i).Item("TN_DESTIP").ToString
            ArrFila(5) = lodtbArea_Reserva.Rows(i).Item("CA_DESCAT").ToString
            ArrFila(6) = lodtbArea_Reserva.Rows(i).Item("SE_SITUEX").ToString
            item = New Windows.Forms.ListViewItem(ArrFila)
            lsvListaAreas.Items.Add(item)
        Next

    End Sub

    Private Sub frm_Grafica_Area_Reserva_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cls_Catastro.Borra_Todo_Feature("", m_application)
        cls_Catastro.Limpiar_Texto_Pantalla(m_application)
        Me.btnGraficar.Enabled = False
        Me.cboConsulta.SelectedIndex = 0
        Me.txtConsulta.Text = "PARACAS"
        With lsvListaAreas
            .View = Windows.Forms.View.Details
            .FullRowSelect = True
            .GridLines = True
            .LabelEdit = False
            .Columns.Clear()
            .Items.Clear()
            .Columns.Add("CODIGO", 85, Windows.Forms.HorizontalAlignment.Center)
            .Columns.Add("NOMBRE", 130, Windows.Forms.HorizontalAlignment.Left)
            .Columns.Add("ZONA", 50, Windows.Forms.HorizontalAlignment.Center)
            .Columns.Add("DESCRIPCION", 100, Windows.Forms.HorizontalAlignment.Left)
            .Columns.Add("TIPO", 100, Windows.Forms.HorizontalAlignment.Left)
            .Columns.Add("CATEGORIA", 100, Windows.Forms.HorizontalAlignment.Left)
            .Columns.Add("SITUACION", 50, Windows.Forms.HorizontalAlignment.Center)
        End With
    End Sub

    Private Sub btnGraficar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGraficar.Click
        Dim lodtbLeyenda As New DataTable
        Dim lostrZona As String = ""
        Dim lo_Inicio As Integer = 0
        Dim cod_rese As String
        Dim cod_rese1 As String
        Dim zona_sele As String = ""
        Dim zona_sele1 As String
        Dim clase_sele As String
        Dim clase_sele1 As String
        Dim cod_opcion As String = ""
        Dim nom_rese As String = ""
        Dim cadena_query_ubica As String = ""
        Dim cadena_query As String = ""
        Dim num As Long = 0
        If lsvListaAreas.CheckedItems.Count = 0 Then
            MsgBox("Seleccione un Item de la Lista.", MsgBoxStyle.Information, "[BDGEOCATMIN]")
            Exit Sub
        End If
        Dim lostrFiltro As String = ""
        Dim loStrShapefile As String = "Catastro" & DateTime.Now.Ticks.ToString()
        'Verificando la seleccion multiple del usuario - hasta 2 registros maximos
        cod_rese1 = ""
        zona_sele1 = ""
        clase_sele1 = ""
        '*****************************
        For i As Integer = 0 To lsvListaAreas.Items.Count - 1
            If lsvListaAreas.Items.Item(i).Checked = True Then
                num = num + 1
                If num > 2 Then
                    MsgBox("Solo debe seleccionar hasta 2 registros : Nucleo y Amortiguamiento del Mismo Codigo Seleccionado..", vbCritical, "Observacion...")
                    Exit Sub
                Else
                    cod_rese = lsvListaAreas.Items(i).SubItems(0).Text
                    lostrZona = lsvListaAreas.Items(i).SubItems(2).Text
                    clase_sele = lsvListaAreas.Items(i).SubItems(3).Text
                    nom_rese = lsvListaAreas.Items(i).SubItems(1).Text
                    'If lo_Inicio = 0 Then
                    '    cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_AReservada & lsvListaAreas.Items(i).SubItems(2).Text, m_application, "1", False)
                    '    cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & lsvListaAreas.Items(i).SubItems(2).Text, m_application, "1", False)
                    '    lo_Inicio = 1
                    'End If
                    'valida codigo de reserva el cual debe ser el mismo a graficar
                    If cod_rese1 = cod_rese Then
                        cod_rese1 = cod_rese
                        'valida zona el cual debe ser el mismo
                        If zona_sele1 = zona_sele Then
                            zona_sele1 = zona_sele
                        Else
                            If zona_sele1 <> "" Then
                                MsgBox("Usted está seleccionando diferentes zonas, debe seleccionar la misma zona para graficar", vbCritical, "Verificar...")
                                lsvListaAreas.Clear()
                                Exit Sub
                            Else
                                zona_sele1 = zona_sele
                            End If
                        End If
                        'haciendo la cadena de consulta
                        cod_opcion = Microsoft.VisualBasic.Left(cod_rese, 2)
                        If cod_opcion <> "ZU" Then
                            If num = 1 Then
                                cadena_query_ubica = "CODIGO =  '" & cod_rese & "' AND CLASE =  '" & clase_sele & "'"
                                cadena_query = cadena_query_ubica
                            Else
                                cadena_query = cadena_query & " OR " & "CODIGO =  '" & cod_rese & "' AND CLASE =  '" & clase_sele & "'"
                            End If
                        ElseIf cod_opcion = "ZU" Then
                            If num = 1 Then
                                cadena_query_ubica = "CODIGO =  '" & cod_rese & "'"
                                cadena_query = cadena_query_ubica
                            Else
                                cadena_query = cadena_query & " OR " & "CODIGO =  '" & cod_rese & "'"
                            End If
                        End If

                    Else
                        'validando codigo area reservada
                        If cod_rese1 <> "" Then
                            MsgBox("Usted está seleccionando diferentes Areas, debe seleccionar el mismo codigo area reserva", vbCritical, "Verificar...")
                            lsvListaAreas.Clear()
                            Exit Sub
                        Else
                            cod_rese1 = cod_rese
                            If zona_sele1 = zona_sele Then
                                zona_sele1 = zona_sele
                            Else
                                If zona_sele1 <> "" Then
                                    MsgBox("Usted está seleccionando diferentes zonas, debe seleccionar solo el mismo", vbCritical, "Verificar...")
                                    lsvListaAreas.Clear()
                                    Exit Sub
                                Else
                                    zona_sele1 = zona_sele
                                End If
                            End If
                            cod_opcion = Microsoft.VisualBasic.Left(cod_rese, 2)
                            If cod_opcion <> "ZU" Then
                                If num = 1 Then
                                    cadena_query_ubica = "CODIGO =  '" & cod_rese & "' AND CLASE =  '" & clase_sele & "'"
                                    cadena_query = cadena_query_ubica
                                Else
                                    cadena_query = cadena_query & " OR " & "CODIGO =  '" & cod_rese & "' AND CLASE =  '" & clase_sele & "'"
                                End If
                            ElseIf cod_opcion = "ZU" Then
                                If num = 1 Then
                                    cadena_query_ubica = "CODIGO =  '" & cod_rese & "'"
                                    cadena_query = cadena_query_ubica
                                Else
                                    cadena_query = cadena_query & " OR " & "CODIGO =  '" & cod_rese & "'"
                                End If
                            End If
                        End If
                    End If

                End If
            End If
        Next i
        '*********************
        lostrFiltro = cadena_query
        cls_Catastro.Delete_Rows_FC_GDB("Malla_" & lostrZona)
        cls_Catastro.Delete_Rows_FC_GDB("Mallap_" & lostrZona)
        cls_Catastro.Load_FC_GDB("Malla_" & lostrZona, "", m_application, True)
        cls_Catastro.Load_FC_GDB("Mallap_" & lostrZona, "", m_application, True)
        cls_Catastro.Actualizar_DM(lostrZona)
        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & lostrZona, m_application, "1", False)
        'cls_Catastro.Conexion_SDE(m_application)
        Select Case cod_opcion
            Case "ZU"
                cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_ZUrbana & lostrZona, m_application, "1", False)
                cls_Prueba.IntersectSelect_por_Limite(m_application, lostrFiltro, "", "Zona Urbana", xMin, yMin, xMax, yMax, txtExiste)
            Case Else 'Varios "AN"
                cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_AReservada & lostrZona, m_application, "1", False)
                cls_Prueba.IntersectSelect_por_Limite(m_application, lostrFiltro, "", "Zona Reservada", xMin, yMin, xMax, yMax, txtExiste)
        End Select


        'cls_Prueba.IntersectSelect_por_Limite(m_application, lo_Filtro, "ESTADO <> 'Y'", "Cuadrangulo", xMin, yMin, xMax, yMax, txtExiste)
        'End Select
        Select Case txtExiste.Text
            Case -1
                cls_Catastro.Borra_Todo_Feature("", m_application)
                cls_Catastro.Limpiar_Texto_Pantalla(m_application)
                MsgBox("..No existe Derechos Mineros en esta Hoja..", MsgBoxStyle.Information, "BDGEOTCATMIN")
                DialogResult = Windows.Forms.DialogResult.Cancel
            Case Else
                'cls_Catastro.Expor_Tema(loStrShapefile, "Denun=Yes", m_application)
                cls_Catastro.Expor_Tema(loStrShapefile, sele_denu, m_application)

                Select Case cod_opcion
                    Case "ZU"
                        cls_Catastro.DefinitionExpression(lostrFiltro, m_application, "Zona Urbana")
                        'cls_Catastro.Poligono_Color_GDB(gstrFC_AReservada & lostrZona, glo_pathStyle & "\AREA_RESERVA.style", "NM_RESE", "", "Cadena", "", m_application, lostrFiltro)
                        ' cls_Catastro.Poligono_Color_GDB(gstrFC_ZUrbana & lostrZona, glo_pathStyle & "\ZONA_URBANA.style", "NOMBRE", "", "Cadena", "", m_application, lostrFiltro)
                    Case "PR"
                        cls_Catastro.DefinitionExpression(lostrFiltro, m_application, "Zona Reservada")
                        'cls_Catastro.Poligono_Color_GDB(gstrFC_AReservada & lostrZona, glo_pathStyle & "\AREA_RESERVA.style", "NM_RESE", "", "Cadena", "", m_application, lostrFiltro)
                End Select

                cls_Catastro.ClearLayerSelection(pFeatureLayer)
                cls_Catastro.Quitar_Layer("Catastro", m_application)
                cls_Prueba.AddFieldDM(loStrShapefile) : cls_Catastro.Quitar_Layer(loStrShapefile, m_application)
                cls_Catastro.Add_ShapeFile(loStrShapefile, m_application)
                cls_Catastro.UpdateValue(lostrFiltro, m_application, "Catastro", "")
                lodtbLeyenda = cls_Prueba.f_Genera_Leyenda_DM("Catastro", lostrFiltro, m_application)
                cls_Prueba.Poligono_Color_1("Catastro", lodtbLeyenda, glo_pathStyle & "\Catastro.style", "LEYENDA", "Default", m_application)
                lodtbLeyenda = Nothing
                cls_Catastro.ShowLabel_DM("Catastro", m_application)
                cls_Catastro.Genera_Malla_UTM(CType(xMin.Text, Double), CType(yMin.Text, Double), CType(xMax.Text, Double), CType(yMax.Text, Double), lostrZona, m_application)
                cls_Catastro.Rotular_texto_DM("Mallap_" & lostrZona, lostrZona, m_application)
                cls_Catastro.Quitar_Layer("Mallap_" & lostrZona, m_application)
                ' cls_Catastro.Style_Linea_GDB("Malla_" & lostrZona, glo_pathStyle & "\malla.style", "CLASE", m_application, "GDB")
                pMxDoc.ActivatedView.Refresh()
                Me.btnGraficar.Enabled = False
                DialogResult = Windows.Forms.DialogResult.Cancel
        End Select
        'MsgBox("Usted está seleccionando diferentes zonas, debe seleccionar la misma zona para graficar", MsgBoxStyle.Information, "[BDGEOCATMIN]")
    End Sub

    Private Sub btnOtraConsulta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOtraConsulta.Click
        'cls_Catastro.Borra_Todo_Feature("", m_application)
        'cls_Catastro.Limpiar_Texto_Pantalla(m_application)
        Me.btnGraficar.Enabled = True
    End Sub

    Private Sub lsvListaAreas_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lsvListaAreas.SelectedIndexChanged

    End Sub
End Class