Imports System.Windows.Forms
Imports System.Data.OleDb
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Geodatabase
Imports Microsoft.Office.Interop

Public Class frmImportar_excel
    Public MxDocument As IMxDocument
    Private clsClase As New cls_excel
    Private lodtRegistro As New DataTable
    Private loNombreHoja As String = ""
    Public m_Application As IApplication
    

    Private Sub frmImportar_Archivos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lodtRegistro = New DataTable
        '  gloLinea = ""
        ' cboGeometria.SelectedIndex = 0
        ' MsgBox(lodtRegistro.Rows.Count)





    End Sub

    Private Sub btnSelArchivo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Para Cualquier version del excel
        Try
            Dim openFD As New OpenFileDialog
            With openFD
                .Title = "Seleccionar archivos"
                .Filter = "Archivos Excel(*.xls;*.xlsx)|*.xls;*.xlsx|Todos los archivos (*.*)|*.*"
                .Multiselect = False
                .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    glo_PathXLS = .FileName

                Else
                    Exit Sub
                End If
            End With
            Call importar1()
        Catch ex As Exception
        End Try

    End Sub



    Private Sub btnGraficar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    'Private Sub cboGeometria_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboGeometria.SelectedIndexChanged
    '    Select Case cboGeometria.Text
    '        Case "POLIGONO", "LINEA"
    '            lblOrden.Enabled = True
    '            lblOrden.Enabled = True
    '            cboAgrupar.Enabled = True
    '            cboOrdenar.Enabled = True
    '        Case "PUNTO"
    '            lblOrden.Enabled = False
    '            lblOrden.Enabled = False
    '            cboAgrupar.Enabled = False
    '            cboOrdenar.Enabled = False
    '    End Select

    'End Sub

    Private Sub importar1()
        Dim stConexion As String = ""
        Try
            stConexion = ("Provider=Microsoft.ACE.OLEDB.12.0;" & ("Data Source=" & (glo_PathXLS & ";Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=2"";")))
        Catch ex As Exception
            stConexion = ("Provider=Microsoft.ACE.OLEDB.12.0;" & ("Data Source=" & (glo_PathXLS & ";Extended Properties=""Excel 8.0 Xml;HDR=YES;IMEX=1"";")))
        End Try
        lodtRegistro = New DataTable
        Dim cnConex As New OleDbConnection(stConexion)
        ' Dim Cmd As New OleDbCommand("Select * from [Hoja1$]")
        Dim Cmd As New OleDbCommand("Select * from [" & loNombreHoja & "$]")
        Dim Ds As New DataSet
        Dim Da As New OleDbDataAdapter
        Dim Dt As New DataTable
        Try
            cnConex.Open()
            Cmd.Connection = cnConex
            Da.SelectCommand = Cmd
            Da.Fill(Ds)
            Dt = Ds.Tables(0)


            cbocodigo.Items.Add("--Seleccionar--")
            'cboNorte.Items.Add("--Seleccionar--")
            'cboOrdenar.Items.Add("--Seleccionar--")
            'cboAgrupar.Items.Add("--Seleccionar--")
            For i As Integer = 0 To Dt.Columns.Count - 1
                cbocodigo.Items.Add(Dt.Columns.Item(i).ColumnName)
                '   cboNorte.Items.Add(Dt.Columns.Item(i).ColumnName)
                '  cboOrdenar.Items.Add(Dt.Columns.Item(i).ColumnName)
                ' cboAgrupar.Items.Add(Dt.Columns.Item(i).ColumnName)

                lodtRegistro.Columns.Add(Dt.Columns.Item(i).ColumnName, Type.GetType("System.String"))

            Next



            Dim dRow As DataRow
            For i As Integer = 0 To Dt.Rows.Count - 1
                dRow = lodtRegistro.NewRow
                For J As Integer = 0 To Dt.Columns.Count - 1
                    dRow.Item(J) = Dt.Rows(i).Item(J)
                Next
                lodtRegistro.Rows.Add(dRow)
                '   lstlistado.Items.Add(Dt.Rows(r).Item("CODIGO")
            Next
            'MsgBox(Dt.Rows.Count)


            cbocodigo.SelectedIndex = 0

        Catch ex As Exception
            cnConex.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
        cnConex.Close()
    End Sub

    Private Sub btnArchivo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArchivo.Click
        'Para Cualquier version del excel
        Try
            Dim openFD As New OpenFileDialog
            With openFD
                .Title = "Seleccionar archivos"
                .Filter = "Archivos Excel(*.xls;*.xlsx)|*.xls;*.xlsx|Todos los archivos (*.*)|*.*"
                .Multiselect = False
                .InitialDirectory = "C:\" ' My.Computer.FileSystem.SpecialDirectories.Desktop
                '.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    glo_PathXLS = .FileName
                    txtNomArchivo.Text = glo_PathXLS
                    loNombreHoja = NombreHoja(.FileName)
                Else
                    Exit Sub
                End If
            End With
            cbocodigo.Items.Clear()
            'cboNorte.Items.Clear()
            'cboAgrupar.Items.Clear()
            'cboOrdenar.Items.Clear()
            Call importar1()
        Catch ex As Exception
        End Try
    End Sub


    Private Sub btnGraficar__Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim pDatum As String = ""
        'Dim pZona As Integer = 0
        'If cboGeometria.SelectedIndex = 0 Then
        '    cboGeometria.Focus()
        '    MsgBox("Seleccione Tipo Punto, Poligono .. ", MsgBoxStyle.Information, "[Aviso]")
        '    Exit Sub
        'End If

        If cbocodigo.SelectedIndex = 0 Then
            cbocodigo.Focus()
            MsgBox("Seleccione campo Este..", MsgBoxStyle.Information, "[Aviso]")
            Exit Sub
        End If
        ' If cboNorte.SelectedIndex = 0 Then
        'cboNorte.Focus()
        'MsgBox("Seleccione campo NORTE..", MsgBoxStyle.Information, "[Aviso]")
        'Exit Sub
        'End If
        'If cboGeometria.Text = "POLIGONO" Or cboGeometria.Text = "LINEA" Then
        'If cboOrdenar.SelectedIndex = 0 Then
        ' cboOrdenar.Focus()
        ' MsgBox("Seleccione campo Ordenar..", MsgBoxStyle.Information, "[Aviso]")
        ' Exit Sub
        ' End If
        'End If
        gloNameCodigo = cbocodigo.Text
        'gloNameNorte = cboNorte.Text
        ' gloNameCodigo = cboAgrupar.Text ' "CODIGO"
        Dim loStrShapefile As String = "P_" & DateTime.Now.Ticks.ToString()
        Try
            'If cboGeometria.Text = "POLIGONO" Or cboGeometria.Text = "LINEA" Then
            '    If cboAgrupar.SelectedIndex <> 0 And cboOrdenar.SelectedIndex <> 0 Then

            '        For i As Integer = 0 To lodtRegistro.Rows.Count - 1

            '            Dim loCadenaOrdenar As String = lodtRegistro.Rows(i).Item(cboOrdenar.Text)
            '            Dim loCadenaAgrupar As String = lodtRegistro.Rows(i).Item(cboAgrupar.Text)

            '            Dim loCadenaOrdenarF As String = Mid$("00000", 1, 6 - Len(Trim$(loCadenaOrdenar))) & Trim$(loCadenaOrdenar)
            '            Dim loCadenaAgruparF As String = Mid$("00000", 1, 6 - Len(Trim$(loCadenaAgrupar))) & Trim$(loCadenaAgrupar)

            '            lodtRegistro.Rows(i).Item(cboOrdenar.Text) = loCadenaOrdenarF
            '            lodtRegistro.Rows(i).Item(cboAgrupar.Text) = loCadenaAgruparF
            '        Next


            '        Dim lodtvOrdena As New DataView(lodtRegistro, Nothing, cboAgrupar.Text & " ASC, " & cboOrdenar.Text & " ASC", DataViewRowState.CurrentRows)
            '        lodtRegistro = lodtvOrdena.ToTable
            '    ElseIf cboAgrupar.SelectedIndex = 0 And cboOrdenar.SelectedIndex <> 0 Then
            '        Try
            '            lodtRegistro.Columns.Add("AAA", Type.GetType("System.String"))
            '        Catch ex As Exception
            '        End Try
            '        For r As Integer = 0 To lodtRegistro.Rows.Count - 1
            '            lodtRegistro.Rows(r).Item("AAA") = "Prueba"
            '        Next
            '        Dim lodtvOrdena As New DataView(lodtRegistro, Nothing, cboOrdenar.Text & " ASC", DataViewRowState.CurrentRows)
            '        lodtRegistro = lodtvOrdena.ToTable
            '        gloNameCodigo = "AAA"
            '    End If
            'End If

            pDatum = "UTM WGS84 18S"
            'Select Case cboGeometria.Text.ToUpper
            '    Case "POLIGONO"
            '        clsClase.CreateShapefile(glo_PathTMP, loStrShapefile, pDatum, cboGeometria.Text)
            '        clsClase.Genera_POLIGONO(loStrShapefile, lodtRegistro, cboGeometria.Text, MxDocument)
            '        clsClase.SelectMapFeatures("Poligono", MxDocument)
            '        clsClase.ZoomToSelectedFeatures(MxDocument)
            '    Case "PUNTO"
            '        clsClase.CreateShapefile(glo_PathTMP, loStrShapefile, pDatum, cboGeometria.Text)
            '        clsClase.Genera_Punto(loStrShapefile, lodtRegistro, pDatum, MxDocument)
            '        clsClase.SelectMapFeatures("Punto", MxDocument)
            '        clsClase.ZoomToSelectedFeatures(MxDocument)
            '    Case "LINEA"
            '        clsClase.CreateShapefile(glo_PathTMP, loStrShapefile, pDatum, cboGeometria.Text)
            '        'clsClase.Genera_POLIGONO(loStrShapefile, lodtRegistro, cboGeometria.Text, MxDocument)
            '        clsClase.GeneraLinea(loStrShapefile, lodtRegistro, loStrShapefile, cboEste.Text, cboNorte.Text,
            '                             MxDocument, My.ArcMap.Application)

            '        clsClase.SelectMapFeatures("Linea", MxDocument)
            '        clsClase.ZoomToSelectedFeatures(MxDocument)
            'End Select
            DialogResult = Windows.Forms.DialogResult.OK
            '   MsgBox("Se creo Layer: " & cboGeometria.Text & "..", MsgBoxStyle.Information, "Aviso")
        Catch ex As Exception
            MsgBox("Error en creación de Archivo (c:\temp)", MsgBoxStyle.Information, "Aviso")
        End Try
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCerrar__Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    'Private Sub cboOrden_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAgrupar.SelectedIndexChanged
    '    gloNameCodigo = cboAgrupar.Text
    'End Sub

    'Private Sub cboVertice_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboOrdenar.SelectedIndexChanged

    'End Sub

    Private Sub cboEste_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbocodigo.SelectedIndexChanged
        gloNameCodigo = cbocodigo.Text




    End Sub

    'Private Sub cboNorte_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNorte.SelectedIndexChanged
    '    gloNameNorte = cboNorte.Text
    'End Sub


    Private Function NombreHoja(ByVal fileName As String) As String
        Try
            Cursor = Cursors.WaitCursor
            Dim xlApp As New Excel.Application
            Dim wb As Excel.Workbook
            wb = xlApp.Workbooks.Open(fileName)
            ' cbo_Hojas.Items.Clear()
            For Each sheet As Excel.Worksheet In wb.Worksheets
                Return sheet.Name
                Exit For
            Next
            wb.Close()
            wb = Nothing
            xlApp.Quit()
            xlApp = Nothing
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Microsoft")
        End Try
    End Function

    'Private Sub btn_Cargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cargar.Click
    '    Try
    '        If cbo_Hojas.Items.Count = 0 Or cbo_Hojas.Text = "" Then
    '            MessageBox.Show("No existe o no se ha seleccionado una hoja en el archivo.", "Microsoft Dynamics")
    '        Else
    '            Cursor = Cursors.WaitCursor
    '            Dim dt As DataTable = GetDataExcel(txt_Directorio.Text, cbo_Hojas.Text)
    '            dgv_Listado.DataSource = dt
    '            _Count = dt.Rows.Count
    '            lbl_DetalleCarga.Text = "Total de registros encontrados: " & dt.Rows.Count
    '            dtp_Fecha_Actualizacion.Enabled = False
    '            btn_Cargar.Enabled = True
    '            btn_eliminar.Enabled = False
    '            btn_borrar.Enabled = True
    '            btn_guardar.Enabled = True
    '            Cursor = Cursors.Default
    '        End If
    '    Catch ex As Exception
    '        Cursor = Cursors.Default
    '        MessageBox.Show(ex.Message, "Microsoft")
    '    End Try
    'End Sub

    'Private Function GetDataExcel(ByVal fileName As String, ByVal sheetName As String) As DataTable
    '    If ((String.IsNullOrEmpty(fileName)) OrElse (String.IsNullOrEmpty(sheetName))) Then _
    '      Throw New ArgumentNullException()
    '    Try
    '        Dim extension As String = IO.Path.GetExtension(fileName)
    '        Dim connString As String = "Data Source=" & fileName

    '        If (extension = ".xls") Then
    '            connString &= ";Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties='Excel 8.0;HDR=YES;IMEX=1'"

    '        ElseIf (extension = ".xlsx") Then
    '            connString &= ";Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'"
    '        Else
    '            Throw New ArgumentException("La extensión " & extension & " del archivo no está permitida.")
    '        End If

    '        Using conexion As New OleDbConnection(connString)
    '            Dim sql As String = "SELECT * FROM [" & sheetName & "$]"
    '            Dim adaptador As New OleDbDataAdapter(sql, conexion)
    '            Dim dt As New DataTable("Excel")
    '            adaptador.Fill(dt)
    '            Return dt
    '        End Using

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function


    Private Sub btnGraficar_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGraficar.Click
        Dim pDatum As String = ""
        Dim vCodigo As String = ""
        Dim cls_planos As New Cls_planos
        Dim cls_catastro As New cls_DM_1


        If cbocodigo.SelectedIndex = 0 Then
            cbocodigo.Focus()
            MsgBox("Seleccione campo Este..", MsgBoxStyle.Information, "[Aviso]")
            Exit Sub
        End If
     
        gloNameCodigo = cbocodigo.Text
    
        Dim loStrShapefile As String = "P_" & DateTime.Now.Ticks.ToString()
        Try
           
            Me.Close()
            procesoautmatico_eval = "automatico"
            clsClase.Procesa_datosexcel(loStrShapefile, lodtRegistro, m_Application)

            MsgBox("El Proceso Termino Satisfactoriamente...", MsgBoxStyle.Exclamation, "SIGCATMIN...")

        
            DialogResult = Windows.Forms.DialogResult.OK

        Catch ex As Exception
            MsgBox("Error en creación de Archivo (c:\temp)", MsgBoxStyle.Information, "Aviso")
        End Try
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class