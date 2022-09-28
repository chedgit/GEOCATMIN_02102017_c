Imports ESRI.ArcGIS.Framework

Public Class Frm_Reporte
    Public m_Application As iapplication
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim lodtbReporteDM As New DataTable
        Dim RetVal
        RetVal = Shell(glo_Path & "\estilos\Barra_Proceso.exe", 1)
        Dim cls_Planos As New Cls_planos
        Dim cls_Catastro As New cls_DM_1
        Dim cls_Dominio As New cls_Dominio
        Dim cls_Prueba As New cls_Prueba
        Dim cls_Evaluacion As New Cls_evaluacion
        If Me.rb_Repo1.Checked = True Then
            Dim lo_Carta As String = cls_Prueba.Get_Unique_Values_FC("Catastro", "CARTA", m_Application, tipo_opcion)
          


            lodtbReporteDM = cls_Dominio.f_Genera_Reporte("Catastro", m_Application)
            If lodtbReporteDM.Rows.Count <> 0 Then
                Me.Close()
                Dim frm_Rpt As New rpt_Reporte_2
                frm_Rpt.C1Report1.DataSource.Recordset = New DataView(lodtbReporteDM)
                Dim m_ReportDefinitionFile As String
                m_ReportDefinitionFile = glo_pathREP & "rpt_Reporte_DM.xml"
                frm_Rpt.C1Report1.Load(m_ReportDefinitionFile, "Reporte_Detalle_DM")
                frm_Rpt.C1Report1.DataSource.Recordset = New DataView(lodtbReporteDM)
                frm_Rpt.C1Report1.Fields("FECHA_DOCUMENTO").Text = Date.Now
                frm_Rpt.C1Report1.Fields("LBLTITULO_1").Text = "Consulta por: " & loglo_Titulo
                If tipo_opcion = "1" Then
                    frm_Rpt.C1Report1.Fields("lblCarta").Text = "Cartas : " & v_codigo
                Else
                    frm_Rpt.C1Report1.Fields("lblCarta").Text = "Cartas : " & lo_Carta
                End If

                frm_Rpt.Show()
                frm_Rpt.C1Report1.Render()
            End If
        ElseIf Me.rb_Repo2.Checked = True Then
            pMap = pMxDoc.FocusMap
            Dim aFound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If Mid(pMap.Layer(A).Name, 1, 9) = "Areainter" Then
                    pFeatureLayer = pMap.Layer(A) 'Layer de Dpto
                    aFound = True
                    Exit For
                End If
            Next A
            If Not aFound Then
                cls_Evaluacion.cierra_ejecutable()
                MsgBox("Para esta opción debe realizar lo siguiente: " & vbNewLine & "1.- Ejecutar Icono Cálculo de Área Disponible" & vbNewLine & "Luego realizar el Reporte..", MsgBoxStyle.Information, "[BDGEOCATMIN]")
                Exit Sub
            End If
            cls_Catastro.Seleccionar_Items_Layer(pFeatureLayer.Name, "", m_Application)
        ElseIf Me.rb_Repo3.Checked = True Then
            lodtbReporteDM = cls_Dominio.f_Genera_Reporte1("Catastro", m_Application)
            If lodtbReporteDM.Rows.Count <> 0 Then
                Dim frm_Rpt As New rpt_Reporte_2
                frm_Rpt.C1Report1.DataSource.Recordset = New DataView(lodtbReporteDM)
                Dim m_ReportDefinitionFile As String
                m_ReportDefinitionFile = glo_pathREP & "\rpt_Reporte_DM_03.xml"
                frm_Rpt.C1Report1.Load(m_ReportDefinitionFile, "Reporte_Detalle_DM")
                frm_Rpt.C1Report1.DataSource.Recordset = New DataView(lodtbReporteDM)
                frm_Rpt.C1Report1.Fields("FECHA_DOCUMENTO").Text = Date.Now
                cls_Evaluacion.cierra_ejecutable()
                frm_Rpt.Show()
                frm_Rpt.C1Report1.Render()
            Else
                cls_Evaluacion.cierra_ejecutable()
            End If
        ElseIf Me.rb_Repo4.Checked = True Then
            Select Case tipo_seleccion
                Case "OP_11"
                    lodtbReporteDM = cls_Planos.Leer_Resultados_Eval_Reporte()
                    If lodtbReporteDM.Rows.Count <> 0 Then
                        Dim frm_Rpt As New rpt_Reporte_2
                        frm_Rpt.C1Report1.DataSource.Recordset = New DataView(lodtbReporteDM)
                        Dim m_ReportDefinitionFile As String
                        m_ReportDefinitionFile = glo_pathREP & "\rpt_Reporte_DM_04.xml"
                        frm_Rpt.C1Report1.Load(m_ReportDefinitionFile, "Reporte_Detalle_DM")
                        frm_Rpt.C1Report1.DataSource.Recordset = New DataView(lodtbReporteDM)
                        frm_Rpt.C1Report1.Fields("FECHA_DOCUMENTO").Text = Date.Now
                        frm_Rpt.C1Report1.Fields("CODIGOU_DM").Text = v_codigo
                        frm_Rpt.C1Report1.Fields("CONCESION_DM").Text = v_nombre_dm
                        frm_Rpt.C1Report1.Fields("AREA DISPONIBLE_DM").Text = v_area_eval & " Ha."
                        frm_Rpt.C1Report1.Fields("LBLTITULO_1").Text = "Zona Urbana: " & lista_urba
                        frm_Rpt.C1Report1.Fields("LBLTITULO_2").Text = "Area de Reserva: " & lista_rese
                        frm_Rpt.C1Report1.Fields("LBLTITULO_3").Text = "Límites fronterizos (Fuente IGN): Distancia de la línea de frontera de: " & distancia_fron & " Km."
                        cls_Evaluacion.cierra_ejecutable()
                        frm_Rpt.Show()

                        frm_Rpt.C1Report1.Render()
                    Else
                        MsgBox("No Existe Registros, para el Reporte.... ", MsgBoxStyle.Information, "[BDGeocatmin]")
                    End If
                Case Else
                    cls_Evaluacion.cierra_ejecutable()
                    MsgBox("El Reporte es ejecutado con la opción, " & vbNewLine & vbNewLine & "1.- Evaluación de DM " & vbNewLine, MsgBoxStyle.Information, "[BDGeocatmin]")
            End Select
        End If
        cls_Evaluacion.cierra_ejecutable()
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub
    Private Sub Frm_Reporte_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.cboReporte.SelectedIndex = 0
        Select Case tipo_seleccion
            Case "OP_11", "OP_12"
                Me.rb_Repo2.Enabled = True
                Me.rb_Repo3.Enabled = True
                Me.rb_Repo4.Enabled = True
            Case Else
                Me.rb_Repo2.Enabled = False
                Me.rb_Repo3.Enabled = False
                Me.rb_Repo4.Enabled = False
        End Select
    End Sub

    Private Sub rb_Repo4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_Repo4.CheckedChanged

    End Sub
End Class