Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto
Public Class frm_Generar_Malla_100
    Public m_Application As IApplication
    Private cls_Catastro As New cls_DM_1
    Private cls_DM_2 As New cls_DM_2


    Private Sub frm_Generar_Malla_100_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.cboPoligono.SelectedIndex = 0
        If sele_opcion_cuadri = True Then
            'Me.cboPoligono.Items.Add("100 Ha.")
            Me.cboPoligono.Items.Add("10 Ha.")
        ElseIf sele_opcion_cuadri = False Then
            ' Me.cboPoligono.Items.Add("100 Ha.")
        End If
        ' Me.cboPoligono.SelectedText("100 Ha.")
        'Me.cboPoligono.SelectedIndex = -1
    End Sub

    Private Sub btnGraficar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGraficar.Click
        If v_tipo_exp = "PE" Then

            Dim lodtbDatos As New DataTable
            Dim k As Integer = 0
            Dim loint_l As Integer = 0
            Dim lo_step As Integer = CType(Mid(Me.cboPoligono.Text, 1, InStr(cboPoligono.Text, " ") - 1), Integer) * 10
            If gloEsteMin <> 0 And gloEsteMax <> 0 And gloNorteMin <> 0 And gloNorteMax <> 0 Then
                Select Case lo_step
                    Case 1000
                        sele_cuadri = "100" 'Para 100has
                        For i As Integer = gloEsteMin To gloEsteMax - 1 Step lo_step
                            Select Case k
                                Case 0
                                    lodtbDatos.Columns.Add("CG_CODIGO", Type.GetType("System.String"))
                                    lodtbDatos.Columns.Add("PE_NOMDER", Type.GetType("System.String"))
                                    lodtbDatos.Columns.Add("CD_COREST", Type.GetType("System.Double"))
                                    lodtbDatos.Columns.Add("CD_CORNOR", Type.GetType("System.Double"))
                                    lodtbDatos.Columns.Add("CD_NUMVER", Type.GetType("System.Double"))
                            End Select
                            For j As Integer = gloNorteMin To gloNorteMax - 1 Step lo_step
                                k = k + 1
                                loint_l = 1
                                Dim dRow As DataRow
                                dRow = lodtbDatos.NewRow
                                dRow.Item("CG_CODIGO") = "DM_" & k
                                dRow.Item("PE_NOMDER") = "DM"
                                dRow.Item("CD_COREST") = i : dRow.Item("CD_CORNOR") = j : dRow.Item("CD_NUMVER") = loint_l
                                lodtbDatos.Rows.Add(dRow)
                                dRow = lodtbDatos.NewRow
                                dRow.Item("CG_CODIGO") = "DM_" & k
                                dRow.Item("PE_NOMDER") = "DM"
                                dRow.Item("CD_COREST") = i + lo_step : dRow.Item("CD_CORNOR") = j : dRow.Item("CD_NUMVER") = loint_l + 1
                                lodtbDatos.Rows.Add(dRow)
                                dRow = lodtbDatos.NewRow
                                dRow.Item("CG_CODIGO") = "DM_" & k
                                dRow.Item("PE_NOMDER") = "DM"
                                dRow.Item("CD_COREST") = i + lo_step : dRow.Item("CD_CORNOR") = j + lo_step : dRow.Item("CD_NUMVER") = loint_l + 2
                                lodtbDatos.Rows.Add(dRow)
                                dRow = lodtbDatos.NewRow
                                dRow.Item("CG_CODIGO") = "DM_" & k
                                dRow.Item("PE_NOMDER") = "DM"
                                dRow.Item("CD_COREST") = i : dRow.Item("CD_CORNOR") = j + lo_step : dRow.Item("CD_NUMVER") = loint_l + 3
                                lodtbDatos.Rows.Add(dRow)
                            Next
                        Next
                    Case 100
                        sele_cuadri = "10" 'Para 10has
                        'If sele_opcion_cuadri = True Then  'Para Plano Cuadricula
                        For i As Integer = gloEsteMin To gloEsteMax - 1 Step 200
                            Select Case k
                                Case 0
                                    lodtbDatos.Columns.Add("CG_CODIGO", Type.GetType("System.String"))
                                    lodtbDatos.Columns.Add("PE_NOMDER", Type.GetType("System.String"))
                                    lodtbDatos.Columns.Add("CD_COREST", Type.GetType("System.Double"))
                                    lodtbDatos.Columns.Add("CD_CORNOR", Type.GetType("System.Double"))
                                    lodtbDatos.Columns.Add("CD_NUMVER", Type.GetType("System.Double"))
                            End Select
                            For j As Integer = gloNorteMin To gloNorteMax - 1 Step 500
                                k = k + 1
                                loint_l = 1
                                Dim dRow As DataRow
                                dRow = lodtbDatos.NewRow
                                dRow.Item("CG_CODIGO") = "DM_" & k
                                dRow.Item("PE_NOMDER") = "DM"
                                dRow.Item("CD_COREST") = i : dRow.Item("CD_CORNOR") = j : dRow.Item("CD_NUMVER") = loint_l
                                lodtbDatos.Rows.Add(dRow)
                                dRow = lodtbDatos.NewRow
                                dRow.Item("CG_CODIGO") = "DM_" & k
                                dRow.Item("PE_NOMDER") = "DM"
                                dRow.Item("CD_COREST") = i + 200 : dRow.Item("CD_CORNOR") = j : dRow.Item("CD_NUMVER") = loint_l + 1
                                lodtbDatos.Rows.Add(dRow)
                                dRow = lodtbDatos.NewRow
                                dRow.Item("CG_CODIGO") = "DM_" & k
                                dRow.Item("PE_NOMDER") = "DM"
                                dRow.Item("CD_COREST") = i + 200 : dRow.Item("CD_CORNOR") = j + 500 : dRow.Item("CD_NUMVER") = loint_l + 2
                                lodtbDatos.Rows.Add(dRow)
                                dRow = lodtbDatos.NewRow
                                dRow.Item("CG_CODIGO") = "DM_" & k
                                dRow.Item("PE_NOMDER") = "DM"
                                dRow.Item("CD_COREST") = i : dRow.Item("CD_CORNOR") = j + 500 : dRow.Item("CD_NUMVER") = loint_l + 3
                                lodtbDatos.Rows.Add(dRow)
                            Next j
                        Next i
                        'Else
                        'MsgBox("Este Dato no es para Generar Plano Cuadricula", MsgBoxStyle.Critical, "Observación...")
                        'Exit Sub

                        'End If

                End Select

                If sele_opcion_cuadri = False Then
                    'cls_Catastro.Delete_Rows_FC_GDB("AreaReserva_100Ha") '& gloZona)
                    cls_Catastro.Delete_Rows_FC_GDB("Recta_" & gloZona)
                    cls_Catastro.Load_FC_GDB("Catastro", "", m_Application, True)
                    cls_DM_2.Genera_Poligono_100Ha(lodtbDatos, gloZona, m_Application, "4")
                    cls_Catastro.Quitar_Layer("Catastro_1", m_Application)
                    lodtbDatos = Nothing

                    Dim pFeatLayer As IFeatureLayer = Nothing
                    Dim afound As Boolean = False
                    For A As Integer = 0 To pMap.LayerCount - 1
                        'If pMap.Layer(A).Name = "AreaReserva_100Ha" Then
                        If pMap.Layer(A).Name = "Recta_" & gloZona Then
                            pFeatLayer = pMap.Layer(A)
                            pFeatLayer.Name = "Cuadriculas"
                            afound = True
                            Exit For
                        End If
                    Next A
                    If Not afound Then
                        MsgBox("No se encuentra la capa de cuadriculas")
                        Exit Sub
                    End If
                    pMxDoc.ActiveView.Refresh()

                    cls_Catastro.Shade_Poligono("Cuadriculas", m_Application)
                    'cls_Catastro.Zoom_to_Layer("Catastro")
                    'cls_Catastro.ShowLabel_DM("Cuadriculas", m_Application)
                    'cls_Catastro.anotaciones_caparese("AreaReserva_100Ha", m_Application)
                    cls_Catastro.rotulatexto_dm("Cuadriculas", m_Application)
                    cls_Catastro.Zoom_to_Layer("Catastro")
                    'cls_Catastro.Label_Feature("AreaReserva_100Ha", "[CODIGOU]", "Arial Narrow", 8, 0, 0, 0, m_Application)
                    Me.Close()
                    Dim cls_planos As New Cls_planos
                    caso_consulta = "CATASTRO MINERO"

                    cls_planos.genera_planocuadriculas(m_Application)
                ElseIf sele_opcion_cuadri = True Then
                    cls_Catastro.Delete_Rows_FC_GDB("Recta_" & gloZona)
                    cls_Catastro.Load_FC_GDB("Catastro", "", m_Application, True)
                    cls_DM_2.Genera_Poligono_100Ha(lodtbDatos, gloZona, m_Application, "4")
                    cls_Catastro.Quitar_Layer("Catastro_1", m_Application)
                    lodtbDatos = Nothing
                    Me.Close()
                    Dim pFeatLayer As IFeatureLayer = Nothing
                    Dim afound As Boolean = False
                    For A As Integer = 0 To pMap.LayerCount - 1
                        'If pMap.Layer(A).Name = "AreaReserva_100Ha" Then
                        If pMap.Layer(A).Name = "Recta_" & gloZona Then
                            pFeatLayer = pMap.Layer(A)
                            pFeatLayer.Name = "Cuadriculas_" & sele_cuadri & "HAS"
                            afound = True
                            Exit For
                        End If
                    Next A
                    If Not afound Then
                        MsgBox("No se encuentra la capa de cuadriculas")
                        Exit Sub
                    End If
                    cls_Catastro.Shade_Poligono("Cuadriculas_" & sele_cuadri & "HAS", m_Application)


                End If
            Else
                MsgBox("No se Genero el cálulo de cuadriculas...", MsgBoxStyle.Information, "[BDGeocatmin]")
                Exit Sub
            End If

        Else
            MsgBox("El cálulo de cuadriculas solo es para DM tipo PE...", MsgBoxStyle.Information, "[BDGeocatmin]")
            Exit Sub

        End If


    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub cboPoligono_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPoligono.SelectedIndexChanged

    End Sub
End Class