Imports ESRI.ArcGIS.Framework
Imports System.Data.OleDb
Imports System.Windows.Forms
Imports System.IO
Public Class frm_Grafica_Excel
    'Private cls_Libreria As New cls_Libreria
    Private cls_Libreria As New cls_DM_1
    Dim cls_Prueba As New cls_Prueba
    Dim cls_DM_2 As New cls_DM_2
    Public m_Application As IApplication

    ' Private cls_Libreria As New cls_Libreria
    'Public m_Application As IApplication


    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Sub Cargar( _
        ByVal lstCoordenada As ListBox, _
        ByVal SLibro As String, _
        ByVal sHoja As String)
        Dim datos As New DataTable
        'HDR=YES : Con encabezado
        Dim cs As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                           "Data Source=" & SLibro & ";" & _
                           "Extended Properties=""Excel 8.0;HDR=YES"""
        Try
            ' cadena de conexión
            Dim cn As New OleDbConnection(cs)

            If Not System.IO.File.Exists(SLibro) Then
                MsgBox("No se encontró el Libro: " & _
                        SLibro, MsgBoxStyle.Critical, _
                        "Ruta inválida")
                Exit Sub
            End If
            ' se conecta con la hoja sheet 1
            Dim dAdapter As New OleDbDataAdapter("Select * From [" & sHoja & "$]", cs)

            ' agrega los datos
            dAdapter.Fill(datos)
            With dgdListaDM '
                ' llena el DataGridView
                .DataSource = datos
            End With
            For r As Integer = 0 To datos.Rows.Count - 1
                'lstCoordenada.Items.Add("Punto " & datos.Rows(r).Item("NUMERO") & ":  " & datos.Rows(r).Item("ESTE") & "; " & datos.Rows(r).Item("NORTE"))
                'lstCoordenada.Items.Add("&  " & datos.Rows(r).Item("CODIGO") & ":  " & datos.Rows(r).Item("ESTE") & "; " & datos.Rows(r).Item("NORTE"))
                Try
                    If datos.Rows(r).Item("CODIGO") <> "" Then
                        lstCoordenada.Items.Add(datos.Rows(r).Item("CODIGO") & ":  " & datos.Rows(r).Item("ESTE") & "; " & datos.Rows(r).Item("NORTE"))
                    Else
                        
                    End If
                Catch MES1 As Exception

                End Try

            Next
        Catch oMsg As Exception
            MsgBox(oMsg.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    'Private Sub Clear_Screen()
    ' Dim cls_Utilidades As New cls_Utilidades
    '     cls_Libreria.Borra_Todo_Feature("", m_Application)
    '    cls_Libreria.Limpiar_Texto_Pantalla(m_Application)
    '   cls_Utilidades.Eliminadataframe()
    'End Sub
 
    Private Sub frm_Grafica_Excel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.cboZona.SelectedIndex = 0
        'btnGenera_graficar.Enabled = False
        Me.Cbotipo.SelectedIndex = 0
    End Sub

    Private Sub btnSelArchivo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelArchivo.Click
        'Para Cualquier version del excel
        Dim openFD As New OpenFileDialog
        With openFD
            .Title = "Seleccionar archivos"
            .Filter = "Archivos Excel(*.xls;*.xlsx)|*.xls;*.xlsx|Todos los archivos (*.*)|*.*"
            .Multiselect = False
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                glo_Path = .FileName
            End If
        End With
        Call importar()
        'Antes version 2003 inferior

        'Dim myStream As IO.Stream = Nothing
        'Dim openFileDialog1 As New OpenFileDialog()
        'openFileDialog1.InitialDirectory = "c:\"
        'openFileDialog1.Filter = "txt files (*.xls)|*.xls|All files (*.xls)|*.xls"
        'openFileDialog1.FilterIndex = 2
        'openFileDialog1.RestoreDirectory = True
        'If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        '    Try
        '        myStream = openFileDialog1.OpenFile()
        '        If (myStream IsNot Nothing) Then

        '        End If
        '    Catch Ex As Exception

        '    Finally

        '        If (myStream IsNot Nothing) Then

        '            myStream.Close()
        '        End If
        '    End Try
        'End If
        'Try
        '    ' Cargar(DataGridView1, openFileDialog1.FileName, "Hoja1")
        'Catch ex As Exception
        'End Try
        'Cargar(lstCoordenada, openFileDialog1.FileName, "Hoja1")
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Close()
    End Sub

    Private Sub btnLimpia1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        lstCoordenada.Items.Clear()
        cboZona.Enabled = True
    End Sub
    Private Sub importar()
        Dim stConexion As String = ("Provider=Microsoft.ACE.OLEDB.12.0;" & ("Data Source=" & (glo_Path & ";Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=2"";")))
        Dim cnConex As New OleDbConnection(stConexion)
        Dim Cmd As New OleDbCommand("Select * from [Hoja1$]")
        Dim Ds As New DataSet
        Dim Da As New OleDbDataAdapter
        Dim Dt As New DataTable
        Try
            cnConex.Open()
            Cmd.Connection = cnConex
            Da.SelectCommand = Cmd
            Da.Fill(Ds)
            Dt = Ds.Tables(0)
            'Catch ex As Exception
            '   MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'End Try
            For r As Integer = 0 To Dt.Rows.Count - 1
                Try
                    If Dt.Rows(r).Item("CODIGO") <> " " Then
                        lstCoordenada.Items.Add(Dt.Rows(r).Item("CODIGO") & ":  " & Dt.Rows(r).Item("ESTE") & "; " & Dt.Rows(r).Item("NORTE"))
                    Else

                    End If
                Catch MES1 As Exception

                End Try

            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Private Sub btnGraficar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGraficar.Click
        'Dim cls_Genera_Circulo As New cls_Utilidades
        'Dim cls_Libreria As New cls_DM_2
        Dim cls_Libreria As New cls_DM_1
        Dim cls_catastro As New cls_DM_1
        Dim dRow As DataRow
        'Clear_Screen()
        pMxDoc = m_Application.Document
        pMap = pMxDoc.FocusMap
        pMxDoc.UpdateContents()
        If lstCoordenada.Items.Count <= 0 Then
            MsgBox("Ingresar coordenadas para realizar esta opción...", MsgBoxStyle.Information, "[Aviso]")
            Exit Sub
        End If
        If Cbotipo.SelectedIndex = 0 Then
            MsgBox("Seleccione Tipo Punto, Poligono, Polylinea... ", MsgBoxStyle.Information, "[Aviso]")
            Exit Sub
        End If
        If cboZona.SelectedIndex = 0 Then
            MsgBox("Seleccione Zona ", MsgBoxStyle.Information, "[Aviso]")
            Exit Sub
        End If
        ' Dim loStrShapefile As String = "Poligono" & DateTime.Now.Ticks.ToString()
        'glo_codigou = "000000"
        Dim lodtRegistro As New DataTable
        Dim lo_zona As String
        v_Zona = Integer.Parse(cboZona.Text) : g_Zona = v_Zona : lo_zona = v_Zona
        Select Case Cbotipo.Text.ToUpper
            Case "POLIGONO"
                Dim loStrShapefile As String = "Poligono" & DateTime.Now.Ticks.ToString()
                If lstCoordenada.Items.Count < 2 Then Exit Sub
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
                    dRow = lodtRegistro.NewRow
                    Dim lostrCodigo As String = Mid(Me.lstCoordenada.Text, 1, InStr(Me.lstCoordenada.Text, ":") - 1)
                    dRow.Item("CG_CODIGO") = lostrCodigo
                    dRow.Item("PE_NOMDER") = "Poligono"
                    Dim lostrEste As String = Mid(Me.lstCoordenada.Text, 1, InStr(Me.lstCoordenada.Text, ";") - 1)
                    dRow.Item("CD_COREST") = CType(Mid(lostrEste, InStr(lostrEste, ":") + 3), Double)
                    dRow.Item("CD_CORNOR") = CType(Mid(Me.lstCoordenada.Text, InStr(Me.lstCoordenada.Text, ";") + 2), Double)
                    dRow.Item("CD_NUMVER") = i + 1
                    lodtRegistro.Rows.Add(dRow)
                Next
                Dim lodtvOrdena_xEste As New DataView(lodtRegistro, Nothing, "CD_COREST ASC", DataViewRowState.CurrentRows)
                lo_glo_xMin = lodtvOrdena_xEste.Item(0).Row("CD_COREST")
                lo_glo_xMax = lodtvOrdena_xEste.Item(lodtvOrdena_xEste.Count - 1).Row("CD_COREST")
                Dim lodtvOrdena_y As New DataView(lodtRegistro, Nothing, "CD_CORNOR ASC", DataViewRowState.CurrentRows)
                lo_glo_yMin = lodtvOrdena_y.Item(0).Row("CD_CORNOR")
                lo_glo_yMax = lodtvOrdena_y.Item(lodtvOrdena_y.Count - 1).Row("CD_CORNOR")
                cls_Libreria.Load_FC_GDB("gpt_Vertice_DM", "", m_Application, True)
                cls_Libreria.Delete_Rows_FC_GDB("gpt_Vertice_DM")
                cls_Prueba.Create_Shapefile_DM(glo_pathTMP, loStrShapefile, Me.cboZona.Text, "Poligono")
                cls_DM_2.Genera_Catastro_Nuevo_po(loStrShapefile, lodtRegistro, Me.cboZona.Text, m_Application)
                'cls_Libreria.Shade_Poligono("Poligono", m_Application)

                cls_catastro.Actualizar_DM(lo_zona)

                cls_Libreria.Color_Poligono_Simple(m_Application, "Poligono")
                cls_Libreria.Quitar_Layer("gpt_Vertice_DM", m_Application)
                Me.cboZona.Enabled = True
                Me.Close()

            Case "PUNTO"
                Dim loStrShapefile As String = "Puntosss" & DateTime.Now.Ticks.ToString()
                v_Zona = cboZona.Text
                If lstCoordenada.Items.Count < 1 Then
                    MsgBox("Para Graficar por la opción punto (x,y) se necesita al menos 1 Vértice...", MsgBoxStyle.Information, "GEOCATMIN")
                    Exit Sub
                End If

                For i As Integer = 0 To Me.lstCoordenada.Items.Count - 1
                    lstCoordenada.SelectedIndex = i
                    Select Case i
                        Case 0
                            lodtRegistro.Columns.Add("CG_CODIGO", Type.GetType("System.String"))
                            lodtRegistro.Columns.Add("PE_NOMDER", Type.GetType("System.String"))
                            lodtRegistro.Columns.Add("CD_COREST", Type.GetType("System.Double"))
                            lodtRegistro.Columns.Add("CD_CORNOR", Type.GetType("System.Double"))
                            lodtRegistro.Columns.Add("CD_NUMVER", Type.GetType("System.Double"))
                    End Select
                    dRow = lodtRegistro.NewRow
                    Dim lostrCodigo As String = Mid(Me.lstCoordenada.Text, 1, InStr(Me.lstCoordenada.Text, ":") - 1)
                    dRow.Item("CG_CODIGO") = lostrCodigo
                    dRow.Item("PE_NOMDER") = "Point"
                    Dim lostrEste As String = Mid(Me.lstCoordenada.Text, 1, InStr(Me.lstCoordenada.Text, ";") - 1)
                    dRow.Item("CD_COREST") = CType(Mid(lostrEste, InStr(lostrEste, ":") + 3), Double)
                    dRow.Item("CD_CORNOR") = CType(Mid(Me.lstCoordenada.Text, InStr(Me.lstCoordenada.Text, ";") + 2), Double)
                    dRow.Item("CD_NUMVER") = i + 1
                    lodtRegistro.Rows.Add(dRow)
                Next

                Dim lodtvOrdena_xEste As New DataView(lodtRegistro, Nothing, "CD_COREST ASC", DataViewRowState.CurrentRows)
                lo_glo_xMin = lodtvOrdena_xEste.Item(0).Row("CD_COREST")
                lo_glo_xMax = lodtvOrdena_xEste.Item(lodtvOrdena_xEste.Count - 1).Row("CD_COREST")
                Dim lodtvOrdena_y As New DataView(lodtRegistro, Nothing, "CD_CORNOR ASC", DataViewRowState.CurrentRows)
                lo_glo_yMin = lodtvOrdena_y.Item(0).Row("CD_CORNOR")
                lo_glo_yMax = lodtvOrdena_y.Item(lodtvOrdena_y.Count - 1).Row("CD_CORNOR")
                cls_Libreria.Load_FC_GDB("gpt_Vertice_DM", "", m_Application, True)
                cls_Libreria.Delete_Rows_FC_GDB("gpt_Vertice_DM")
                cls_Prueba.Create_Shapefile_DM(glo_pathTMP, loStrShapefile, Me.cboZona.Text, "Point")
                cls_DM_2.Genera_Catastro_Nuevo_Point(loStrShapefile, lodtRegistro, Me.cboZona.Text, m_Application)

                'cls_Libreria.Shade_Poligono("Poligono", m_Application)
                cls_Libreria.Quitar_Layer("gpt_Vertice_DM", m_Application)
                Me.cboZona.Enabled = True
                Me.Close()

            Case "POLYLINEA"
                Dim loStrShapefile As String = "Polyline" & DateTime.Now.Ticks.ToString()
                If lstCoordenada.Items.Count < 2 Then
                    MsgBox("Para Graficar por la opción Polyline se necesita mínimo 2 Vértices...", MsgBoxStyle.Information, "[Aviso]")
                    Exit Sub
                End If
                For i As Integer = 0 To Me.lstCoordenada.Items.Count - 1
                    lstCoordenada.SelectedIndex = i
                    Select Case i
                        Case 0
                            lodtRegistro.Columns.Add("CG_CODIGO", Type.GetType("System.String"))
                            lodtRegistro.Columns.Add("PE_NOMDER", Type.GetType("System.String"))
                            lodtRegistro.Columns.Add("CD_COREST", Type.GetType("System.Double"))
                            lodtRegistro.Columns.Add("CD_CORNOR", Type.GetType("System.Double"))
                            lodtRegistro.Columns.Add("CD_NUMVER", Type.GetType("System.Double"))
                    End Select
                    dRow = lodtRegistro.NewRow
                    Dim lostrCodigo As String = Mid(Me.lstCoordenada.Text, 1, InStr(Me.lstCoordenada.Text, ":") - 1)
                    dRow = lodtRegistro.NewRow
                    dRow.Item("CG_CODIGO") = lostrCodigo
                    dRow.Item("PE_NOMDER") = "Polylinea"
                    Dim lostrEste As String = Mid(Me.lstCoordenada.Text, 1, InStr(Me.lstCoordenada.Text, ";") - 1)
                    dRow.Item("CD_COREST") = CType(Mid(lostrEste, InStr(lostrEste, ":") + 3), Double)
                    dRow.Item("CD_CORNOR") = CType(Mid(Me.lstCoordenada.Text, InStr(Me.lstCoordenada.Text, ";") + 2), Double)
                    dRow.Item("CD_NUMVER") = i + 1
                    lodtRegistro.Rows.Add(dRow)
                Next

                Dim lodtvOrdena_xEste As New DataView(lodtRegistro, Nothing, "CD_COREST ASC", DataViewRowState.CurrentRows)
                lo_glo_xMin = lodtvOrdena_xEste.Item(0).Row("CD_COREST")
                lo_glo_xMax = lodtvOrdena_xEste.Item(lodtvOrdena_xEste.Count - 1).Row("CD_COREST")
                Dim lodtvOrdena_y As New DataView(lodtRegistro, Nothing, "CD_CORNOR ASC", DataViewRowState.CurrentRows)
                lo_glo_yMin = lodtvOrdena_y.Item(0).Row("CD_CORNOR")
                lo_glo_yMax = lodtvOrdena_y.Item(lodtvOrdena_y.Count - 1).Row("CD_CORNOR")

                cls_Libreria.Load_FC_GDB("gpt_Vertice_DM", "", m_Application, True)
                cls_Libreria.Delete_Rows_FC_GDB("gpt_Vertice_DM")
                cls_Prueba.Create_Shapefile_DM(glo_pathTMP, loStrShapefile, Me.cboZona.Text, "Polylinea")
                cls_DM_2.Genera_Catastro_Nuevo_polylinea(loStrShapefile, lodtRegistro, Me.cboZona.Text, m_Application)
                'cls_Libreria.Shade_Poligono("Poligono", m_Application)
                cls_Libreria.Quitar_Layer("gpt_Vertice_DM", m_Application)
                Me.cboZona.Enabled = True
                Me.Close()

        End Select
        Close()
    End Sub
   
    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()

    End Sub

    Private Sub btnLimpia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpia.Click
        lstCoordenada.Items.Clear()

    End Sub

    Private Sub GroupBox3_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox3.Enter

    End Sub

    Private Sub Cbotipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbotipo.SelectedIndexChanged

    End Sub

    Private Sub cboZona_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboZona.SelectedIndexChanged

    End Sub
End Class