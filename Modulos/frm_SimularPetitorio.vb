Imports ESRI.ArcGIS.GISClient
Imports ESRI.ArcGIS.Display
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Output
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.CartoUI
Imports ESRI.ArcGIS.GeoDatabaseUI
Imports stdole
Imports ESRI.ArcGIS.Framework
Public Class Frm_simularPetitorio
    Public m_application As IApplication
    Public papp As IApplication
    Private cls_DM_2 As New cls_DM_2
    Private cls_Catastro As New cls_DM_1
    Private cls_eval As New Cls_evaluacion
    Private Sub Frm_simularPetitorio_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.cboZona.SelectedIndex = 1
        'pMxDoc = papp.Document
        'pMap = pMxDoc.FocusMap
        'pMap.Name = "CATASTRO MINERO"
        'pMxDoc.ActivatedView.Refresh()
        
    End Sub
    Public Sub Pintar_Formulario()
        Me.BackColor = Drawing.Color.FromArgb(242, 242, 240)
    End Sub

    Private Sub btnagregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnagregar.Click
        Dim valor As String
        If Not IsNumeric(Me.txtEste.Text) Or Len(Me.txtEste.Text) <> 6 Then
            MsgBox("Ingrese Correctamente la Coordenada Este", MsgBoxStyle.Critical, "Observación...")
            Me.txtEste.Focus()
            Exit Sub
        End If
        If Not IsNumeric(Me.txtNorte.Text) Or Len(Me.txtNorte.Text) <> 7 Then
            MsgBox("Ingrese Correctamente la Coordenada Norte", MsgBoxStyle.Critical, "Observación...")
            Me.txtNorte.Focus()
            Exit Sub
        End If
        valor = Val(txtEste.Text) & "; " & Val(txtNorte.Text)
        Me.lboCoordenada.Items.Add(valor)
    End Sub

    Private Sub btnGraficar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGraficar.Click
        V_zona_simu = Me.cboZona.Text
        Me.Close()
        V_caso_simu = "SI"
        Codigo_dm_v = "000000001"

        Dim lodtRegistro As New DataTable
        If lboCoordenada.Items.Count = 2 Then
            MsgBox("Para Evaluar mínimo 3 Vértices...", MsgBoxStyle.Information, "[BDGEOCATMIN]")
            Exit Sub
        End If
        For i As Integer = 0 To Me.lboCoordenada.Items.Count - 1
            Me.lboCoordenada.SelectedIndex = i
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
            dRow.Item("PE_NOMDER") = "DM Simulado"
            dRow.Item("CD_COREST") = Mid(Me.lboCoordenada.Text, 1, InStr(Me.lboCoordenada.Text, ";") - 1)
            dRow.Item("CD_CORNOR") = Mid(Me.lboCoordenada.Text, InStr(Me.lboCoordenada.Text, ";") + 2)
            dRow.Item("CD_NUMVER") = i + 1
            lodtRegistro.Rows.Add(dRow)
        Next

        cls_DM_2.Genera_Catastro_NuevoM("", lodtRegistro, V_zona_simu, m_application, "")
        Dim pForm As New Frm_Eval_segun_codigo

        pForm.m_application = m_application
        pForm.Show()
        cls_Catastro.Load_FC_GDB("gpt_Vertice_DM", "", m_application, True)
        cls_Catastro.Delete_Rows_FC_GDB("gpt_Vertice_DM")
        cls_Catastro.Rotular_texto_DM("gpt_Vertice_DM", V_zona_simu, m_application)
        cls_Catastro.Quitar_Layer("gpt_Vertice_DM", m_application)
        cls_eval.obtienelimitesmaximos("Catastro")
        cls_Catastro.Color_Poligono_Simple(m_application, "Catastro")
        cls_Catastro.Genera_Imagen_DM("VistaPrevia", "VistaPrevia")
        pForm.img_DM.ImageLocation = glo_pathTMP & "\VistaPrevia.jpg"

        cls_Catastro.Shade_Poligono("Catastro", m_application)
        cls_Catastro.Genera_Imagen_DM("VistaPrevia_1", "VistaPrevia")


    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub
    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If lboCoordenada.Text <> "" Then
            Me.lboCoordenada.Items.RemoveAt(Me.lboCoordenada.SelectedIndex)
            Me.btnEliminar.Enabled = False
        End If
    End Sub

    Private Sub lboCoordenada_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lboCoordenada.Click
        If lboCoordenada.Text <> "" Then
            Me.btnEliminar.Enabled = True
        End If

    End Sub

   
End Class