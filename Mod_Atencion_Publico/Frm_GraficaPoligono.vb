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

Public Class Frm_GraficaPoligono
    Public m_application As IApplication
    Private cls_Catastro As New cls_DM_1
    Private cls_DM_2 As New cls_DM_2
    Private cls_Prueba As New cls_Prueba
    Private cls_eval As New Cls_evaluacion
    Private cls_planos As New Cls_planos
    Private cls_Usuario As New cls_Usuario
    Private Sub btnagregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnagregar.Click
        'Select Case Me.Cbotipo.SelectedValue
        'Case "Poligono"
        Dim valor As String
        'If Not IsNumeric(Me.txtNorte.Text) Or Len(Me.txtNorte.Text) <> 7 Then 'NORTE
        '    MsgBox("Ingrese Correctamente la Coordenada Este", MsgBoxStyle.Critical, "Observación...")
        '    Me.txtNorte.Focus()
        '    Exit Sub
        'End If
        'If Not IsNumeric(Me.txtEste.Text) Or Len(Me.txtEste.Text) <> 6 Then
        '    MsgBox("Ingrese Correctamente la Coordenada Norte", MsgBoxStyle.Critical, "Observación...")
        '    Me.txtEste.Focus()
        '    Exit Sub
        'End If
        valor = "Punto " & Me.lstCoordenada.Items.Count + 1 & ":  " & Val(txtEste.Text) & "; " & Val(txtNorte.Text)
        Me.lstCoordenada.Items.Add(valor)

        'End Select
        If lstCoordenada.Items.Count >= 3 Then Me.btnGenera_graficar.Enabled = True
        Me.cboZona.Enabled = True


    End Sub

    Private Sub btnGenera_Poligono_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenera_graficar.Click
        caso_consulta = "CATASTRO MINERO"
        pMxDoc = m_application.Document
        pMap = pMxDoc.FocusMap
        If pMap.Name <> "CATASTRO MINERO" Then
            cls_planos.buscaadataframe(caso_consulta, False)
            If valida = False Then
                pMap.Name = "CATASTRO MINERO"
                pMxDoc.UpdateContents()
            End If
            cls_eval.ActivaDataframe_Opcion(caso_consulta, m_application)
            pMxDoc.UpdateContents()
        End If

        cls_planos.buscaadataframe("CATASTRO MINERO", False)
        If valida = False Then
            pMap.Name = "CATASTRO MINERO"
        End If
        pMxDoc.UpdateContents()

        If Me.lstCoordenada.Items.Count < 2 Then Exit Sub
        If Me.cboZona.SelectedIndex = 0 Then
            MsgBox("Seleccione una Zona ", MsgBoxStyle.Information, "[BDGEOCIENTÍFICA]")
            Exit Sub
        End If
        Dim loStrShapefile As String = "Poligono" & DateTime.Now.Ticks.ToString()
        fecha_archi2 = DateTime.Now.Ticks.ToString
        glo_Layer_Simulado = loStrShapefile
        Codigo_dm_v = "000000001"
        Dim lodtRegistro As New DataTable
        If Me.lstCoordenada.Items.Count = 2 Then
            MsgBox("Para Graficar se necesita mínimo 3 Vértices...", MsgBoxStyle.Information, "[BDGEOCATMIN]")
            Exit Sub
        End If
        For i As Integer = 0 To Me.lstCoordenada.Items.Count - 1
            Me.lstCoordenada.SelectedIndex = i
            Select Case i
                Case 0
                    lodtRegistro.Columns.Add("CG_CODIGO", Type.GetType("System.String"))
                    lodtRegistro.Columns.Add("PE_NOMDER", Type.GetType("System.String"))
                    lodtRegistro.Columns.Add("CD_COREST", Type.GetType("System.Double"))
                    lodtRegistro.Columns.Add("CD_CORNOR", Type.GetType("System.Double"))
                    lodtRegistro.Columns.Add("CD_NUMVER", Type.GetType("System.Double"))
            End Select
            Dim dRow As DataRow
            dRow = lodtRegistro.NewRow
            dRow.Item("CG_CODIGO") = Codigo_dm_v
            dRow.Item("PE_NOMDER") = "Poligono"
            Dim lostrEste As String = Mid(Me.lstCoordenada.Text, 1, InStr(Me.lstCoordenada.Text, ";") - 1)
            dRow.Item("CD_COREST") = CType(Mid(lostrEste, InStr(lostrEste, ":") + 3), Double)
            dRow.Item("CD_CORNOR") = CType(Mid(Me.lstCoordenada.Text, InStr(Me.lstCoordenada.Text, ";") + 2), Double)
            dRow.Item("CD_NUMVER") = i + 1
            lodtRegistro.Rows.Add(dRow)
        Next
        Dim lodtvOrdena_xEste As New DataView(lodtRegistro, Nothing, "CD_COREST ASC", DataViewRowState.CurrentRows)
        glo_xMin = lodtvOrdena_xEste.Item(0).Row("CD_COREST")
        glo_xMax = lodtvOrdena_xEste.Item(lodtvOrdena_xEste.Count - 1).Row("CD_COREST")
        Dim lodtvOrdena_y As New DataView(lodtRegistro, Nothing, "CD_CORNOR ASC", DataViewRowState.CurrentRows)
        glo_yMin = lodtvOrdena_y.Item(0).Row("CD_CORNOR")
        glo_yMax = lodtvOrdena_y.Item(lodtvOrdena_y.Count - 1).Row("CD_CORNOR")
        cls_Catastro.Load_FC_GDB("gpt_Vertice_DM", "", m_application, True)
        cls_Catastro.Delete_Rows_FC_GDB("gpt_Vertice_DM")
        cls_Prueba.Create_Shapefile_DM(glo_pathTMP, loStrShapefile, Me.cboZona.Text, "Poligono")
        cls_DM_2.Genera_Catastro_Nuevo_po(loStrShapefile, lodtRegistro, Me.cboZona.Text, m_application)
        'cls_Catastro.Shade_Poligono("Poligono", m_application)
        cls_Catastro.Color_Poligono_Simple(m_application, "Poligono")
        cls_Catastro.Quitar_Layer("gpt_Vertice_DM", m_application)
        Me.cboZona.Enabled = True
        Me.Close()
    End Sub

    Private Sub txtNorte_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNorte.KeyPress

    End Sub

    Private Sub txtNorte_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNorte.TextChanged

    End Sub

    Private Sub btnElimina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElimina.Click
        'Select Case Me.Cbotipo.SelectedValue
        'Case "Poligono"
        Me.lstCoordenada.Items.RemoveAt(lstCoordenada.SelectedIndex)
        'End Select
    End Sub

    Private Sub btnLimpia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpia.Click
        lstCoordenada.Items.Clear()
        Me.cboZona.Enabled = True
    End Sub

    Private Sub cboZona_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboZona.SelectedIndexChanged
        Select Case Me.Cbotipo.SelectedValue
            Case "Poligono"
                btnGenera_graficar.Enabled = False
            Case Else
                If Me.cboZona.SelectedIndex <> 0 Then
                    btnGenera_graficar.Enabled = True
                Else
                    btnGenera_graficar.Enabled = False
                End If
        End Select
    End Sub

    Private Sub Cbotipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbotipo.SelectedIndexChanged
        Select Case Me.Cbotipo.SelectedValue
            Case "Poligono"
                btnGenera_graficar.Enabled = False
            Case Else
                If Me.cboZona.SelectedIndex <> 0 Then
                    btnGenera_graficar.Enabled = True
                Else
                    btnGenera_graficar.Enabled = False
                End If
        End Select
    End Sub

    Private Sub Frm_GraficaPoligono_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.cboZona.SelectedIndex = 0
        btnGenera_graficar.Enabled = False
        Me.Cbotipo.SelectedIndex = 0
    End Sub

    Private Sub txtEste_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEste.KeyDown

    End Sub

    Private Sub txtEste_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEste.KeyPress

    End Sub

    Private Sub txtEste_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEste.TextChanged

    End Sub

    Private Sub txtEste_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEste.Validated

    End Sub
End Class