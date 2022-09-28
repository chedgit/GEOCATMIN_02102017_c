
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto
Imports PORTAL_Clases
Imports System.Drawing
Imports ESRI.ArcGIS.esriSystem
Imports Microsoft.Office.Interop
Imports System.Data
Imports System.IO

Imports System.Windows.Forms
Imports ESRI.ArcGIS.DataSourcesFile


Structure Punto_DM2
    Dim v As Integer
    Dim x As Double
    Dim y As Double
End Structure

Public Class Frm_Estadisticas_Caram

    Private dt As New DataTable
    Public pApp As IApplication
    Public pEste As Double
    Public pNorte As Double
    Private lodtbUbigeo As New DataTable
    Private cls_Oracle As New cls_Oracle
    Private cls_Catastro As New cls_DM_1
    Private cls_Prueba As New cls_Prueba
    Private cls_DM_2 As New cls_DM_2
    Public m_application As IApplication
    Private Const Col_Sel_R As Integer = 0
    Private Const Col_Codigo As Integer = 1
    Private Const Col_Nombre As Integer = 2
    Private Const Col_tprese As Integer = 3
    Private Const Col_nm_tprese As Integer = 4

    'Se aumento debido a 2 columnas mas
    Private Const Col_Area As Integer = 5
    Private Const Col_Cantidad As Integer = 6
    Private Const Col_Areasup As Integer = 7
    Private Const Col_Por_Sup As Integer = 8






    'se comento este parte 03-
    'Private Const Col_Area As Integer = 3
    'Private Const Col_Cantidad As Integer = 4
    'Private Const Col_Areasup As Integer = 5
    'Private Const Col_Por_Sup As Integer = 6
    Private lodtbReporte_Excel As New DataTable

    Private Property tipo_selec_catnomin As String



    'PROCESOS
    Private Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Long, ByVal bInheritHandle As Long, ByVal dwProcessId As Long) As Long
    Private Declare Function GetExitCodeProcess Lib "kernel32" (ByVal hProcess As Long, ByVal lpExitCode As Long) As Long
    ' Private dt As New DataTable
    'PAUSA
    Private Declare Function GetTickCount Lib "Kernel32.dll" () As Long
    Const STILL_ACTIVE = &H103
    Const PROCESS_QUERY_INFORMATION = &H400

    ''FUNCION DE SLEEP PARA ESPERAR UNOS MILISEGUNDOS
    Sub Pausa(ByVal HowLong As Long)
        Dim u, tick As Long
        tick = GetTickCount()
        Do
            Application.DoEvents()
        Loop Until tick + HowLong < GetTickCount
    End Sub
    Function ExeEspera(ByVal COMANDO As String)
        Dim hProcess As Long
        Dim RetVal As Long
        hProcess = OpenProcess(PROCESS_QUERY_INFORMATION, False, Shell(COMANDO, vbMinimizedNoFocus))
        Do
            GetExitCodeProcess(hProcess, RetVal)
            Application.DoEvents()
            Pausa(100)
        Loop While RetVal = STILL_ACTIVE
    End Function
    Private Sub Frm_Estadisticas_Caram_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        btncalcular.Enabled = False

        Me.cbotipo.SelectedIndex = 1
        Me.cboZona.Items.Clear()
        Me.cboZona.Enabled = False

        Me.btnExcel.Enabled = True
        ' Me.btnCargar.Enabled = False
        'Dim ds As New DataSet
        'Dim s1 As Decimal = 0
        'Dim s2 As Decimal = 0
        'Try
        '    If Dir(glo_Path & "\arestringida.xml", vbArchive) <> "" Then
        '        ds.ReadXml(glo_Path & "\arestringida.xml")
        '        dgdDetalle.DataSource = ds.Tables(0)
        '        dt = ds.Tables(0)
        '        'fn_Grilla(dgdDetalle, dt)
        '        'PT_Agregar_Funciones_Detalle_02()
        '        'PT_Forma_Grilla_Detalle_02()
        '        'cboRegion.Items.Add("TOTAL PERÚ")
        '        For r As Integer = 0 To dt.Rows.Count - 1
        '            'Select Case dt.Rows(r).Item(0)
        '            '   Case "MAR", "FUERA DEL PERU"
        '            '  Case Else
        '            s1 = s1 + dt.Rows(r).Item("AREA")
        '            s2 = s2 + dt.Rows(r).Item("AREA_SUP")
        '            'End Select
        '            ' cboRegion.Items.Add(dt.Rows(r).Item("NM_DEPA"))
        '        Next

        '        txtArea1.Text = Format(s1, "###,###,###,###.##")
        '        txtArea2.Text = Format(s2, "###,###,###,###.##")
        '        txtPorcentaje.Text = Format((s2 / s1) * 100, "###,###,###,###.##")
        '        ' cboRegion.SelectedIndex = 0
        '    End If

        'Catch ex As Exception
        'End Try
        'DATOS DE PROVINCIA
        'cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Provincia, m_application, "2", False)
        'Dim v_nm_PROV1 As String
        'Dim pFeature As IFeature
        'Dim pfeaturecurso As IFeatureCursor

        'pFeatureClass = pFeatureLayer_prov.FeatureClass

        'pFeatureCursor = pFeatureClass.Search(Nothing, False)

        'Do Until pFeature Is Nothing
        '    v_codigo = pFeature.Value(pFeatureCursor.FindField("CD_PROV"))
        '    v_nm_PROV1 = pFeature.Value(pFeatureCursor.FindField("NM_PROV"))
        '    ComboBox1.Items.Add(v_nm_PROV1)
        '    pFeatureCursor = pFeature.nextfeature



        'Loop


    End Sub

    Private Sub PT_Estilo_Grilla_EVAL(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(Col_Sel_R).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Codigo).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Nombre).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_tprese).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_nm_tprese).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Area).DefaultValue = 0
        padtbDetalle.Columns.Item(Col_Cantidad).DefaultValue = 0
        padtbDetalle.Columns.Item(Col_Areasup).DefaultValue = 0
        padtbDetalle.Columns.Item(Col_Por_Sup).DefaultValue = 0

    End Sub
    Private Sub PT_Cargar_Grilla_EVAL(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdDetalle.DataSource = dvwDetalle
        Me.dgdDetalle.Columns(Col_Sel_R).Value = True
    End Sub

    'Private Function fn_Grilla(ByVal p_DataGridView As DataGridView, ByVal p_lodbtTabla As DataTable) As DataGridView
    '    With p_DataGridView
    '        .DataSource = p_lodbtTabla
    '        .AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue
    '        '.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '        .AutoResizeColumns() ' ------->>> ESTA ES LA INSTRUCCION QUE NECESITAS..
    '        .DefaultCellStyle.WrapMode = DataGridViewTriState.True
    '        .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '    End With
    '    Return p_DataGridView
    'End Function


    Private Sub PT_Agregar_Funciones_EVAL()
        Me.dgdDetalle.Columns(Col_Sel_R).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Codigo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Nombre).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_tprese).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_nm_tprese).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Area).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Cantidad).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Areasup).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Por_Sup).DefaultValue = ""

    End Sub
    Private Sub chkEstado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEstado.CheckedChanged
        Dim items As C1.Win.C1TrueDBGrid.ValueItems = Me.dgdDetalle.Columns("SELEC").ValueItems
        If Me.chkEstado.Checked Then
            items.Translate = True
            items.CycleOnClick = True
            items.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("0", False)) ' unchecked
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("1", True)) ' checked
        Else
            items.Translate = True
            items.CycleOnClick = True
            items.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("0", False)) ' unchecked
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("1", True)) ' checked
        End If
    End Sub

    Private Sub PT_Forma_Grilla_EVAL()
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).Width = 25
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Width = 30
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Width = 100
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tprese).Width = 30
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_nm_tprese).Width = 100
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Area).Width = 50
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cantidad).Width = 40
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Areasup).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Por_Sup).Width = 80
        Me.dgdDetalle.Columns("SELEC").Caption = "SELEC"
        Me.dgdDetalle.Columns("CODIGO").Caption = "CODIGO"
        Me.dgdDetalle.Columns("NOMBRE").Caption = "NOMBRE"
        Me.dgdDetalle.Columns("TP_RESE").Caption = "TP_RESE"
        Me.dgdDetalle.Columns("NM_TPRESE").Caption = "NM_TPRESE"
        Me.dgdDetalle.Columns("AREA").Caption = "AREAINI"
        Me.dgdDetalle.Columns("CANTI").Caption = "CANTI"
        ' Me.dgdDetalle.Columns("AREA_SUP").Caption = "AREA_SUP"
        Me.dgdDetalle.Columns("AREA_NETA").Caption = "AREA_NETA"
        Me.dgdDetalle.Columns("PORCEN").Caption = "PORCEN"
       
        Me.dgdDetalle.Splits(0).HeadingStyle.ForeColor = Color.Blue
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tprese).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_nm_tprese).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Area).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cantidad).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Areasup).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Por_Sup).Locked = True
       
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tprese).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_nm_tprese).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Area).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cantidad).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Areasup).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Por_Sup).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tprese).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_nm_tprese).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Area).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cantidad).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Areasup).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Por_Sup).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center


    End Sub

    Private Sub dgdDetalle_Change(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgdDetalle.Change

    End Sub

    Private Sub dgdDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgdDetalle.Click
        'cls_Catastro.DefinitionExpression_Campo_Zoom("FID = " & dgdDetalle.Item(dgdDetalle.Row, "CONTADOR") - 1 & "", m_application, "AreaRese_super")
    End Sub
    Public Sub ExportarDatosExcel(ByVal p_ListBox As Object, ByVal titulo As String)

        'Generando datos en txt
        Const fic As String = "C:\reporte.txt"
        Dim sw As New System.IO.StreamWriter(fic)
        'escrile los elementos al txt

        For i As Integer = 0 To p_ListBox.ListCount - 1
            p_ListBox.Selected(i) = True

            sw.WriteLine(p_ListBox.List(p_ListBox.ListIndex))

        Next
        'Close #1  'cierra txt
        sw.Close()



        Dim m_Excel As New Excel.Application
        m_Excel.Cursor = Excel.XlMousePointer.xlWait
        m_Excel.Visible = True
        Dim objLibroExcel As Excel.Workbook = m_Excel.Workbooks.Add
        Dim objHojaExcel As Excel.Worksheet = objLibroExcel.Worksheets(1)
        With objHojaExcel
            .Visible = Excel.XlSheetVisibility.xlSheetVisible
            .Activate()
            'Encabezado.
            .Range("A1:L1").Merge()
            .Range("A1:L1").Value = "INGEMMET"
            .Range("A1:L1").Font.Bold = True
            .Range("A1:L1").Font.Size = 16
            'Texto despues del encabezado.
            .Range("A2:L2").Merge()
            .Range("A2:L2").Value = titulo
            .Range("A2:L2").Font.Bold = True
            .Range("A2:L2").Font.Size = 10
            'Nombres
            'Estilo a titulos de la tabla.
            .Range("A6:P6").Font.Bold = True
            'Establecer tipo de letra al rango determinado.
            .Range("A1:P37").Font.Name = "Tahoma"
            'Los datos se registran a partir de la columna A, fila 4.
            Const primeraLetra As Char = "A"
            Const primerNumero As Short = 6
            Dim Letra As Char, UltimaLetra As Char
            Dim Numero As Integer, UltimoNumero As Integer
            Dim cod_letra As Byte = Asc(primeraLetra) - 1
            Dim sepDec As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
            Dim sepMil As String = Application.CurrentCulture.NumberFormat.NumberGroupSeparator
            Dim strColumna As String = ""
            Dim LetraIzq As String = ""
            Dim cod_LetraIzq As Byte = Asc(primeraLetra) - 1
            Letra = primeraLetra
            Numero = primerNumero
            Dim objCelda As Excel.Range
            '  Dim lodbtExiste_SupAR As New DataTable
            ' lodtTabla.Columns.Add("SELEC", Type.GetType("System.String"))
            ' For t As Integer = 0 To lodbtExiste_SupAR.Columns
            'v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
            'v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
            'v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
            'v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
            'dRow = lodtTabla.NewRow
            'dRow.Item("CODIGO") = v_codigo_depa
            'dRow.Item("NOMBRE") = v_nm_depa1
            'dRow.Item("AREA") = v_areaini_depa
            'dRow.Item("CANTIDAD") = v_cantidad
            'dRow.Item("AREA_SUP") = v_areasup_rese
            'dRow.Item("PORCEN") = (Format(Math.Round((v_areasup_rese / v_areaini_depa) * 100, 2), "###,###.00")).ToString

            'lodtTabla.Rows.Add(dRow)
            ' Next a


            ' For w As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1

            'lostCGCODEVA = lostCGCODEVA & w + 1 & " | " & dgdDetalle.Item(w, "COD_RESE") & " || "
            'valida_caso = dgdDetalle.Item(w, "CASO").ToString

            'If valida_caso = "COMPATIBLE" Then

            'ElseIf valida_caso = "INCOMPATIBLE" Then

            'Else
            '    MsgBox("NO ESTA INDICADO SI ES COMPATIBLE O INCOMPATIBLE UNAS DE LAS AREAS DEL LISTADO, POR FAVOR VERIFICAR..", MsgBoxStyle.Critical, "GEOCATMIN...")
            '    Exit Sub
            'End If

            'For w As Integer = 0 To dgdDetalle.DataSource.
            '    C1.Win.C1TrueDBGrid.C1DataColumn = 5
            '    For c As Integer = 0 To 5


            'For Each c As DataGridViewColumn In DataGridView1.Columns
            For Each c As C1.Win.C1TrueDBGrid.C1DisplayColumn In dgdDetalle.Splits(0).DisplayColumns
                'Col_Area.style.frecolor = Color.bkacl

                'If c.Visible Then
                If Letra = "Z" Then
                    Letra = primeraLetra
                    cod_letra = Asc(primeraLetra)
                    cod_LetraIzq += 1
                    LetraIzq = Chr(cod_LetraIzq)
                Else
                    cod_letra += 1
                    Letra = Chr(cod_letra)
                End If
                strColumna = LetraIzq + Letra + Numero.ToString
                objCelda = .Range(strColumna, Type.Missing)
                'objCelda.Value = c.HeaderText
                objCelda.EntireColumn.Font.Size = 10
                'Establece un formato a los numeros por Default.
                'objCelda.EntireColumn.NumberFormat = c.DefaultCellStyle.Format
                ' If c.ValueType Is GetType(Decimal) OrElse c.ValueType Is GetType(Double) Then
                objCelda.EntireColumn.NumberFormat = "#" + sepMil + "0" + sepDec + "00"
                'End If
                'End If

            Next
            Dim objRangoEncab As Excel.Range = .Range(primeraLetra + Numero.ToString, LetraIzq + Letra + Numero.ToString)
            objRangoEncab.BorderAround(1, Excel.XlBorderWeight.xlMedium)
            UltimaLetra = Letra
            Dim UltimaLetraIzq As String = LetraIzq
            'Cargar Datos del DataGridView.
            Dim i As Integer = Numero + 1
            ' For reg As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
            'For Each reg As DataGridViewRow In DataGridView1.Rows
            '   C1.Win.C1TrueDBGrid()
            ' For Each reg As C1.Win.C1TrueDBGrid In C1.Win.C1TrueDBGrid.C1TrueDBGrid.

            LetraIzq = ""
            cod_LetraIzq = Asc(primeraLetra) - 1
            Letra = primeraLetra
            cod_letra = Asc(primeraLetra) - 1
            'For Each c As DataGridViewColumn In DataGridView1.Columns
            For w1 As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
                ' If c.Visible Then
                If Letra = "Z" Then
                    Letra = primeraLetra
                    cod_letra = Asc(primeraLetra)
                    cod_LetraIzq += 1
                    LetraIzq = Chr(cod_LetraIzq)
                Else
                    cod_letra += 1
                    Letra = Chr(cod_letra)
                End If
                strColumna = LetraIzq + Letra
                'Aqui se realiza la carga de datos.
                '.Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", reg.Cells(c.Index).Value)
                .Cells(i, strColumna) = dgdDetalle.Item(w1, "COD_RESE").ToString







                'Establece las propiedades de los datos del DataGridView por Default.
                '.Cells(i, strColumna) = IIf(IsDBNull(reg.(c.DataPropertyName)), c.DefaultCellStyle.NullValue, reg(c.DataPropertyName))
                '.Range(strColumna + i, strColumna + i).In()
                ' End If
            Next
            Dim objRangoReg As Excel.Range = .Range(primeraLetra + i.ToString, strColumna + i.ToString)
            objRangoReg.Rows.BorderAround()
            objRangoReg.Select()
            i += 1
            '  Next
            UltimoNumero = i
            'Dibujar las líneas de las columnas.
            LetraIzq = ""
            cod_LetraIzq = Asc("A")
            cod_letra = Asc(primeraLetra)
            Letra = primeraLetra
            ' For Each c As DataGridViewColumn In DataGridView1.Columns
            For w1 As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1

                'If c.Visible Then
                objCelda = .Range(LetraIzq + Letra + primerNumero.ToString, LetraIzq + Letra + (UltimoNumero - 1).ToString)
                objCelda.BorderAround()
                If Letra = "Z" Then
                    Letra = primeraLetra
                    cod_letra = Asc(primeraLetra)
                    LetraIzq = Chr(cod_LetraIzq)
                    cod_LetraIzq += 1
                Else
                    cod_letra += 1
                    Letra = Chr(cod_letra)
                End If
                ' End If
            Next
            'Dibujar el border exterior grueso de la tabla.
            Dim objRango As Excel.Range = .Range(primeraLetra + primerNumero.ToString, UltimaLetraIzq + UltimaLetra + (UltimoNumero - 1).ToString)
            objRango.Select()
            objRango.Columns.AutoFit()
            objRango.Columns.BorderAround(1, Excel.XlBorderWeight.xlMedium)
        End With
        m_Excel.Cursor = Excel.XlMousePointer.xlDefault

    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Validando si esta escrito correctamente el incompatible ó compatible
        Try
            Dim valida_caso As String
            Dim CONTADOR_CASO As Integer

            Dim v_cdrese1 As String = ""
            Dim v_nmrese As String = ""
            Dim v_cdrese As String = ""
            Dim v_nombre As String = ""
            Dim v_area As Double = 0.0
            Dim v_tprese As String = ""
            Dim v_categori As String = ""
            Dim v_clase As String = ""
            Dim v_titular As String = ""
            Dim v_zona As String = ""
            Dim v_zonex As String = ""
            Dim v_obs As String = ""
            Dim v_norma As String = ""
            Dim v_fechaing As String = ""
            Dim v_entidad As String = ""
            Dim v_uso As String = ""
            Dim v_estado As String = ""
            Dim canti_reg As Integer
            Dim lostrObervacion As String = ""
            Dim cadena As String

            Dim cls_consulta As New cls_DM_1
            Dim fecha As String
            ' Dim pFeatureTable As ITable
            Dim contador As Integer
            Dim MyDate As Date
            Dim v_leyenda As String = ""
            Dim pFeatureLayer As IFeatureLayer
            Dim pFeatureCursorpout As IFeatureCursor
            Dim pFeaturepout As IFeature
            Dim pfeaturlayeroutput As FeatureLayer
            Dim pFeatureTable As ITable

            For w As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1

                'lostCGCODEVA = lostCGCODEVA & w + 1 & " | " & dgdDetalle.Item(w, "COD_RESE") & " || "
                valida_caso = dgdDetalle.Item(w, "CASO").ToString

                If valida_caso = "COMPATIBLE" Then

                ElseIf valida_caso = "INCOMPATIBLE" Then

                Else
                    MsgBox("NO ESTA INDICADO SI ES COMPATIBLE O INCOMPATIBLE UNAS DE LAS AREAS DEL LISTADO, POR FAVOR VERIFICAR..", MsgBoxStyle.Critical, "GEOCATMIN...")
                    Exit Sub
                End If
            Next w


            'cls_Catastro.PT_CargarFeatureClass_SDE("CQUI0543.AREA_NAT_VALIDADA_" & v_zona_dm, pApp, "1", True)  'psad 56

            'pMap = pMxDoc.FocusMap
            'pFeatureLayer = pMap.Layer(0)
            'pOutFeatureClass = pFeatureLayer.FeatureClass
            'pMap.DeleteLayer(pFeatureLayer)

            'Dim afound As Boolean = False
            'For A As Integer = 0 To pMap.LayerCount - 1
            '    If pMap.Layer(A).Name = "AreaRese_super" Then
            '        pFeatureLayer = pMxDoc.FocusMap.Layer(A)
            '        pInFeatureClass = pFeatureLayer.FeatureClass
            '        aFound = True
            '        Exit For
            '    End If
            'Next A

            'If Not afound Then
            '    MsgBox("No esta la capa superpuestas de Areas Naturales...")
            '    Exit Sub
            'End If

            Dim pQueryFilter As IQueryFilter
            Dim pFeatureSelection As IFeatureSelection

            Dim fclas_tema As IFeatureClass
            Dim pActiveView As IActiveView
            Dim pFSel As IFeatureSelection
            Dim pFeatureCursor As IFeatureCursor
            Dim valor_area As String
            Dim valor_numarea As String


            'Barriendo las 3 zonas

            'For k As Integer = 17 To 19

            '    cls_Catastro.PT_CargarFeatureClass_SDE("CQUI0543.AREA_NAT_VALIDADA_" & v_zona_dm, pApp, "1", True)  'psad 56

            '    pMap = pMxDoc.FocusMap
            '    pfeaturlayeroutput = pMap.Layer(0)
            '    pOutFeatureClass = pfeaturlayeroutput.FeatureClass
            '    pMap.DeleteLayer(pfeaturlayeroutput)
            'Next k


            Dim lostCGCODEVA As String = ""
            Dim lostrRetCarac As String = "", lostrRetAreas As String = "", lostrRetCoord As String = ""
            Dim v_IECODIGO As String = ""
            Dim clsWUrbina As New cls_BDEvaluacion
            pMxDoc = m_application.Document
            pMap = pMxDoc.FocusMap
            Dim aFound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "AreaRese_super" Then
                    pFeatureLayer = pMap.Layer(A)
                    aFound = True : Exit For
                End If
            Next A
            If Not aFound Then Exit Sub
            pFeatureClass = pFeatureLayer.FeatureClass
            Dim pFilter As IQueryFilter
            pFilter = New QueryFilter
            pFilter.WhereClause = ""
            pFeatureCursor = pFeatureClass.Search(pFilter, False)
            pFeature = pFeatureCursor.NextFeature
            Dim lostrCoox As String = "", lostrCooy As String = ""
            'Dim lostrCoox As CLOB.

            Dim lointNumVer As Integer = 0
            Dim lostAG_TIPO As String = ""
            Dim lointVertice As String = ""
            Dim lodouArea As String = ""
            Dim lodouHa As String = ""

            Dim lostrZU1 As String = ""
            Dim lostrZU As String = ""
            Dim lostCGCODEVA1 As String = ""
            Dim lostrNumPoligono As String = ""
            Dim loContador As Integer = 0
            Dim loContadorTR As Integer = 0
            Dim vTipo As String = "", vCGCodeva As String = ""
            Dim l As IPolygon
            Dim pArea As IArea
            Dim pt As IPoint
            Dim lointArea1 As Double
            Dim lostrRetorno As String
            Dim lostrRetorno1 As String
            '*****


            If glo_InformeDM = "" Then glo_InformeDM = 1

            Dim lostrSG_D_EVALGIS As String = ""
            'lostrSG_D_EVALGIS = cls_Oracle.FT_SG_CUENTA_REG_IN_EVALGIS(v_codigo, glo_InformeDM)
            lostrSG_D_EVALGIS = cls_Oracle.FT_SG_CUENTA_REG_IN_EVALGIS("SG_D_EVALGIS", v_codigo, glo_InformeDM, "", "")

            If lostrSG_D_EVALGIS = "1" Then
            Else
                lostrSG_D_EVALGIS = cls_Oracle.FT_SG_D_EVALGIS("INS", v_codigo, glo_InformeDM, gstrUsuarioAcceso)
            End If
            v_IECODIGO = "NA"

            'cuentaareasrese = cls_Oracle.FT_SG_CUENTA_REG_IN_CARACEVALGIS(v_codigo, v_IECODIGO, glo_InformeDM, lostCGCODEVA)
            cuentaareasrese = cls_Oracle.FT_SG_CUENTA_REG_IN_EVALGIS("AREA_PROTEGIDA", v_codigo, glo_InformeDM, lostCGCODEVA, v_IECODIGO)
            'lostrRetorno = cls_Oracle.FT_SG_D_CARAC_EVALGIS("INS", v_codigo, glo_InformeDM, "1 | " & v_codigo & " ||", "1 | AP ||", _
            '   "", "", "", gstrUsuarioAcceso)
            If cuentaareasrese > 0 Then
                ' lostrRetCarac = cls_Oracle.FT_SG_D_CARAC_EVALGIS("DEL", v_codigo, glo_InformeDM, lostCGCODEVA, _
                ' lostrZU, "", lodouHa, "", gstrUsuarioAcceso)
            End If
            Dim cuenta_areas As Integer = 0

            'cuenta_areas = cls_Oracle.FT_SG_CUENTA_REG_IN_AREAS_EVALGIS(v_codigo, "NA", glo_InformeDM)
            cuenta_areas = cls_Oracle.FT_SG_CUENTA_REG_IN_EVALGIS("SG_D_AREAS_EVALGIS", v_codigo, glo_InformeDM, "", "NA")
            Dim valor2 As String = "", valor4 As String = "", valor5 As String = "", valor6 As String = "", valor7 As String = ""
            Dim cod_eval_act As String = "", valor_cod_act As String = "", v_IECODIGO_act As String = ""
            For w As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
                lostCGCODEVA = dgdDetalle.Item(w, "COD_RESE").ToString
                valor2 = dgdDetalle.Item(w, "CODIGOU").ToString
                lodouArea = dgdDetalle.Item(w, "NUM_AREA").ToString
                valor4 = valor4 & dgdDetalle.Item(w, "CLASE").ToString
                valor7 = valor7 & dgdDetalle.Item(w, "NM_RESE").ToString
                lodouHa = dgdDetalle.Item(w, "AREA").ToString
                valor5 = dgdDetalle.Item(w, "TIP_RESE").ToString

                ' cod_eval_act = dgdDetalle.Item(h, "COD_RESE").ToString
                ' valor_cod_act = dgdDetalle.Item(h, "CODIGOU").ToString

                Select Case dgdDetalle.Item(w, "TIP_RESE")
                    Case "AREA NATURAL"
                        Select Case Me.dgdDetalle.Item(w, "CLASE")
                            Case "ANP"
                                v_IECODIGO = "NA"
                                loContador += 1
                                lostrZU = "NA"
                            Case "AMORTIGUAMIENTO"
                                v_IECODIGO = "NA"
                                loContador += 1
                                lostrZU = "NA"
                            Case " "
                                v_IECODIGO = "NA"
                                loContador += 1
                                lostrZU = "NA"
                        End Select
                End Select

                'v_IECODIGO_act = v_IECODIGO

                l = pFeature.Shape
                'pArea = pFeature.Shape   'pArea.Area
                'lointArea1 = pArea.Area / 10000
                'lointArea1 = (Format(Math.Round(lointArea1, 4), "###,###.00")).ToString
                'lointArea1 = (Format(Math.Round(lointArea1, 2), "###,###.00")).ToString
                ptcol = l
                lointNumVer = ptcol.PointCount - 1

                If cuenta_areas = "1" Then

                    If lodouArea = "1" Then
                        lostrRetorno1 = cls_Oracle.FT_SG_D_AREAS_EVALGIS("DEL", v_codigo, glo_InformeDM, v_codigo, "", v_IECODIGO, _
                        "", "", "")
                        lostrRetorno1 = cls_Oracle.FT_SG_D_CARAC_EVALGIS("DEL", v_codigo, glo_InformeDM, v_codigo, v_IECODIGO, _
                       "", "", "", "")
                    End If
                    lostrRetorno = cls_Oracle.FT_SG_D_CARAC_EVALGIS("ACT", v_codigo, glo_InformeDM, "1 | " & lostCGCODEVA & " ||", "1 |" & v_IECODIGO & " ||", _
                     "", "", "", gstrUsuarioAcceso)


                    lostrRetAreas = cls_Oracle.FT_SG_D_AREAS_EVALGIS("ACT", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), lostCGCODEVA, lodouArea, v_IECODIGO, _
                 lodouHa, vTipo, gstrUsuarioAcceso)

                    Dim vertices As Integer = 0
                    For k As Integer = 0 To ptcol.PointCount - 2
                        vertices = vertices + 1
                        pt = ptcol.Point(k)
                        lostrCoox = pt.X
                        lostrCooy = pt.Y
                        lostrRetCoord = cls_Oracle.FT_SG_D_COORD_EVALGIS("ACT", v_codigo, glo_InformeDM, lostCGCODEVA, lodouArea, vertices, v_IECODIGO, _
                        lostrCoox, lostrCooy, lointArea1, gstrUsuarioAcceso)
                        lostrCoox = ""
                        lostrCooy = ""

                    Next k

                    pFeature = pFeatureCursor.NextFeature

                Else

                    If cuentaareasrese = 0 Then
                        lostrRetorno = cls_Oracle.FT_SG_D_CARAC_EVALGIS("INS", v_codigo, glo_InformeDM, "1 | " & lostCGCODEVA & " ||", "1 |" & v_IECODIGO & " ||", _
                           "", "", "", gstrUsuarioAcceso)
                    End If

                    lostrRetAreas = cls_Oracle.FT_SG_D_AREAS_EVALGIS("INS", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), lostCGCODEVA, lodouArea, v_IECODIGO, _
                    lodouHa, vTipo, gstrUsuarioAcceso)

                    Dim vertices As Integer = 0
                    For k As Integer = 0 To ptcol.PointCount - 2
                        vertices = vertices + 1
                        pt = ptcol.Point(k)

                        lostrCoox = pt.X
                        lostrCooy = pt.Y


                        lostrRetCoord = cls_Oracle.FT_SG_D_COORD_EVALGIS("INS", v_codigo, glo_InformeDM, lostCGCODEVA, lodouArea, vertices, v_IECODIGO, _
                            lostrCoox, lostrCooy, lointArea1, gstrUsuarioAcceso)
                        lostrCoox = ""
                        lostrCooy = ""
                    Next k

                    pFeature = pFeatureCursor.NextFeature

                End If

            Next w


            If lostrRetAreas > 0 And lostrRetCoord > 0 Then
                'MsgBox("Se actualizado la base de datos exitosamente.", vbExclamation, "BdGeocatmin")
            Else
                var_fa_Coord_SuperAreaReserva = False
                MsgBox("No se pudo guardar completamente a la base de datos, Verificar sus datos: " & v_codigo, vbExclamation, "[Geocatmin]")
                var_Fa_AreasNaturales = False
                Exit Sub

            End If

            'ACTUALIZANDO PARTE ESPACIAL - geodatabase

            'carga capa de areas naturales segun zona
            'cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".AREA_NAT_VALIDADA_" & v_zona_dm, pApp, "1", True)  'psad 56 por usuario
            cls_Catastro.PT_CargarFeatureClass_SDE("GPO_ANV_AREA_NAT_VALIDA_" & v_zona_dm, pApp, "1", False) 'por datagis

            pMap = pMxDoc.FocusMap
            pFeatureLayer = pMap.Layer(0)
            pOutFeatureClass = pFeatureLayer.FeatureClass
            pMap.DeleteLayer(pFeatureLayer)

            'verifica capa de areas naturales superpueto

            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "AreaRese_super" Then
                    pFeatureLayer = pMxDoc.FocusMap.Layer(A)
                    pInFeatureClass = pFeatureLayer.FeatureClass
                    aFound = True
                    Exit For
                End If
            Next A

            If Not aFound Then
                MsgBox("No esta la capa superpuestas de Areas Naturales...")
                Exit Sub
            End If

            'lee los registros
            For j As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1

                CONTADOR_CASO = dgdDetalle.Item(j, "CONTADOR").ToString
                valida_caso = dgdDetalle.Item(j, "CASO").ToString
                valor_area = dgdDetalle.Item(j, "AREA").ToString
                valor_numarea = dgdDetalle.Item(j, "NUM_AREA").ToString
                ' p_Filtro1 = "FID = " & dgdDetalle.Item(dgdDetalle.Row, "CONTADOR") - 1
                pMap = pMxDoc.FocusMap
                pActiveView = pMap
                pFeatureSelection = pFeatureLayer
                pQueryFilter = New QueryFilter
                'pQueryFilter.WhereClause = p_Filtro1
                pQueryFilter.WhereClause = "FID = " & CONTADOR_CASO - 1
                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pMxDoc.ActiveView.Refresh()

                pFSel = pFeatureLayer
                fclas_tema = pFeatureLayer.FeatureClass
                pFeatureCursor = pFeatureLayer.Search(pQueryFilter, True)
                pFeature = pFeatureCursor.NextFeature

                'pQueryFilter = New QueryFilter
                'pFeatureCursor = pInFeatureClass.Search(pQueryFilter, False)
                'pFeature = pFeatureCursor.NextFeature

                Do Until pFeature Is Nothing


                    If pFeatureCursor.FindField("CODIGO") = -1 Then
                        v_cdrese = " "
                    Else
                        v_cdrese = pFeature.Value(pFeatureCursor.FindField("CODIGO")).ToString
                    End If
                    If pFeatureCursor.FindField("NM_RESE") = -1 Then
                        v_nmrese = " "
                    Else
                        v_nmrese = pFeature.Value(pFeatureCursor.FindField("NM_RESE")).ToString
                    End If
                    If pFeatureCursor.FindField("NOMBRE") = -1 Then
                        v_nombre = " "
                    Else
                        v_nombre = pFeature.Value(pFeatureCursor.FindField("NOMBRE")).ToString
                    End If

                    If pFeatureCursor.FindField("TP_RESE") = -1 Then
                        v_tprese = " "
                    Else
                        v_tprese = pFeature.Value(pFeatureCursor.FindField("TP_RESE")).ToString
                    End If
                    If pFeatureCursor.FindField("CATEGORI") = -1 Then
                        v_categori = " "
                    Else
                        v_categori = pFeature.Value(pFeatureCursor.FindField("CATEGORI")).ToString
                    End If
                    If pFeatureCursor.FindField("CLASE") = -1 Then
                        v_clase = " "
                    Else
                        v_clase = pFeature.Value(pFeatureCursor.FindField("CLASE")).ToString
                    End If

                    If pFeatureCursor.FindField("TITULAR") = -1 Then
                        v_titular = " "
                    Else
                        v_titular = pFeature.Value(pFeatureCursor.FindField("TITULAR")).ToString
                    End If
                    If pFeatureCursor.FindField("ZONA") = -1 Then
                        v_zona = " "
                    Else
                        v_zona = pFeature.Value(pFeatureCursor.FindField("ZONA")).ToString
                    End If
                    If pFeatureCursor.FindField("ZONEX") = -1 Then
                        v_zonex = " "
                    Else
                        v_zonex = pFeature.Value(pFeatureCursor.FindField("ZONEX")).ToString
                    End If

                    If pFeatureCursor.FindField("NORMA") = -1 Then
                        v_norma = " "
                    Else
                        v_norma = pFeature.Value(pFeatureCursor.FindField("NORMA")).ToString
                    End If


                    If pFeatureCursor.FindField("FEC_ING_1") = -1 Then
                        v_fechaing = " "
                    Else
                        v_fechaing = pFeature.Value(pFeatureCursor.FindField("FEC_ING_1")).ToString
                    End If

                    If pFeatureCursor.FindField("ENTIDAD") = -1 Then
                        v_entidad = " "
                    Else
                        v_entidad = pFeature.Value(pFeatureCursor.FindField("ENTIDAD")).ToString
                    End If

                    If pFeatureCursor.FindField("USO") = -1 Then
                        v_uso = " "
                    Else
                        v_uso = pFeature.Value(pFeatureCursor.FindField("USO")).ToString
                    End If

                    If pFeatureCursor.FindField("EST_GRAF") = -1 Then
                        v_estado = " "
                    Else
                        v_estado = pFeature.Value(pFeatureCursor.FindField("EST_GRAF")).ToString
                    End If

                    If pFeatureCursor.FindField("LEYENDA_1") = -1 Then
                        v_leyenda = " "
                    Else
                        ' v_estado = pFeature.Value(pFeatureCursor.FindField("ESTADO")).ToString
                        v_leyenda = pFeature.Value(pFeatureCursor.FindField("LEYENDA_1")).ToString
                    End If
                    If cuenta_areas = "1" Then 'actualizacion capa geodatbaase

                        cls_Catastro.CreaFeature(pOutFeatureClass, pFeature.Shape)
                        'If v_cdrese <> v_cdrese1 Then
                        'CONTADOR_CASO = 1
                        'End If
                        ' v_cdrese1 = v_cdrese
                        If CONTADOR_CASO = 1 Then
                            Dim v_cdrese2 As String = ""
                            For contadorx As Integer = 1 To colecciones_anat.Count
                                v_cdrese2 = colecciones_anat.Item(contadorx)

                                ' Next contadorx
                                ''colecciones_codurba.Clear()

                                pQueryFilter.WhereClause = "CODIGO = '" & v_cdrese2 & "'"
                                pFeatureCursorpout = pOutFeatureClass.Update(pQueryFilter, True)

                                'pFeaturepout = pFeatureCursorpout.NextFeature


                                ' Do While pFeaturepout IsNot Nothing
                                pFeatureTable = pOutFeatureClass
                                cls_Catastro.Eliminafesture(pFeatureTable, pQueryFilter)
                            Next contadorx
                            'pFeaturepout = pFeatureCursorpout.NextFeature
                            'Loop

                            'pFeatureTable = pOutFeatureClass
                            'pFeaturepout = pFeatureCursorpout.NextFeature
                            'cls_Catastro.Eliminafesture(pFeatureTable, pQueryFilter)
                            'pFeaturepout = pFeatureCursorpout.NextFeature

                            ' cls_Oracle.P_EliminaRegistros(v_cdrese, "GPO_ANV_AREA_NAT_VALIDA_" & v_zona, "1")
                            'cls_Oracle.P_EliminaRegistros(v_cdrese, "DATA_GIS.GPO_ANV_AREA_NAT_VALIDA_" & v_zona, "1")

                            ' pFeaturepout = pFeatureCursorpout.NextFeature

                            '    cls_Catastro.CreaFeature(pOutFeatureClass, pFeature.Shape)
                        End If

                    Else
                        'si es Nuevo ingreso
                        cls_Catastro.CreaFeature(pOutFeatureClass, pFeature.Shape)
                    End If

                    pQueryFilter = New QueryFilter
                    pQueryFilter.WhereClause = "CODIGO IS NULL"
                    pFeatureCursorpout = pOutFeatureClass.Update(pQueryFilter, False)
                    pFeaturepout = pFeatureCursorpout.NextFeature
                    pFeaturepout.Value(pFeatureCursorpout.FindField("CODIGO")) = v_cdrese
                    pFeaturepout.Value(pFeatureCursorpout.FindField("CASO")) = valida_caso
                    pFeaturepout.Value(pFeatureCursorpout.FindField("NM_RESE")) = v_nmrese
                    pFeaturepout.Value(pFeatureCursorpout.FindField("NOMBRE")) = v_nombre
                    pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = valor_area
                    pFeaturepout.Value(pFeatureCursorpout.FindField("TP_RESE")) = v_tprese
                    pFeaturepout.Value(pFeatureCursorpout.FindField("CATEGORI")) = v_categori
                    pFeaturepout.Value(pFeatureCursorpout.FindField("CLASE")) = v_clase
                    pFeaturepout.Value(pFeatureCursorpout.FindField("ZONA")) = v_zona
                    pFeaturepout.Value(pFeatureCursorpout.FindField("ZONEX")) = v_zonex
                    pFeaturepout.Value(pFeatureCursorpout.FindField("NORMA")) = v_norma
                    pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_ING")) = v_fechaing
                    pFeaturepout.Value(pFeatureCursorpout.FindField("ENTIDAD")) = v_entidad
                    pFeaturepout.Value(pFeatureCursorpout.FindField("USO")) = v_uso
                    pFeaturepout.Value(pFeatureCursorpout.FindField("EST_GRAF")) = v_estado
                    pFeaturepout.Value(pFeatureCursorpout.FindField("LEYENDA")) = v_leyenda
                    pFeaturepout.Value(pFeatureCursorpout.FindField("NUM_AREA")) = valor_numarea
                    pFeaturepout.Value(pFeatureCursorpout.FindField("USU_REG")) = glo_User_SDE
                    pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_REG")) = DateTime.Now.ToString
                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                    pFeaturepout = pFeatureCursorpout.NextFeature
                    pFeature = pFeatureCursor.NextFeature
                Loop

            Next j


            pMxDoc.ActiveView.Refresh()
            colecciones_anat.Clear()
            MsgBox("Se Actualizó Correctamente su información al GEODATABASE..., Verificar ", MsgBoxStyle.Information, "GEOCATMIN")
            var_Fa_AreasNaturales = True

        Catch ex As Exception
            MsgBox("No ha terminado el proceso correctamente", vbCritical, "Observacion  ")
        End Try

    End Sub

    Private Sub dgdDetalle_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles dgdDetalle.RowColChange
        'Dim pFeatLayer As IFeatureLayer
        'Dim b As Integer

        'For A As Integer = 0 To pMap.LayerCount - 1
        '    If pMap.Layer(A).Name = "AreaRese_super" Then
        '        pFeatLayer = pMxDoc.FocusMap.Layer(A)
        '        b = A
        '        Exit For
        '    End If
        'Next A

        'pFeatLayer = pMxDoc.FocusMap.Layer(b)
        'If pFeatLayer.Name = "AreaRese_super" Then
        '    loStrShapefile1 = "AreaRese_super"
        '    Dim lodtRegistro As New DataTable

        '    p_Filtro1 = "FID = " & dgdDetalle.Item(dgdDetalle.Row, "CONTADOR") - 1
        '    cls_Catastro.Seleccionar_Items_x_Codigo_areasup("FID = " & dgdDetalle.Item(dgdDetalle.Row, "CONTADOR") - 1 & "", pApp, Me.lstCoordenada, "AreaRese_super")
        '    ' Me.dgdResultado.Visible = False

        'End If
    End Sub

    'Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
    '    Me.Close()

    'End Sub

    'Private Sub btnagregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnagregar.Click
    '    Dim pActiveView As IActiveView
    '    Dim pFeatLayer As IFeatureLayer = Nothing
    '    pMap = pMxDoc.FocusMap
    '    pActiveView = pMap
    '    pMxDoc = m_application.Document

    '    Dim afound As Boolean = False
    '    For A As Integer = 0 To pMap.LayerCount - 1
    '        If pMap.Layer(A).Name = "AreaRese_super" Then
    '            pFeatLayer = pMxDoc.FocusMap.Layer(A)
    '            afound = True
    '            Exit For
    '        End If
    '    Next A
    '    If Not afound Then
    '        Exit Sub
    '    End If
    '    pMxDoc.UpdateContents()
    '    pActiveView.Refresh()
    '    Dim pFeatureSelection As IFeatureSelection
    '    pFeatureSelection = pFeatLayer
    '    Dim pQueryFilter As IQueryFilter
    '    ' Prepare a query filter.
    '    pQueryFilter = New QueryFilter
    '    pQueryFilter.WhereClause = p_Filtro1

    '    ' Refresh or erase any previous selection.
    '    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
    '    pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
    '    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
    '    Dim pCmdItem As ICommandItem
    '    Dim pUID As New UID
    '    pUID.Value = "esriArcMapUI.ZoomToSelectedCommand"
    '    pCmdItem = m_application.Document.CommandBars.Find(pUID)

    '    pCmdItem.Execute()
    '    pMxDoc.ActiveView.Refresh()
    'End Sub

    Private Sub btnGrabar_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        cls_Catastro.DefinitionExpression_Campo_Zoom("FID = " & dgdDetalle.Item(dgdDetalle.Row, "CONTADOR") - 1 & "", m_application, "AreaRese_super")
    End Sub

    Private Sub btnvermapa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".AREA_NAT_VALIDADA_" & v_zona_dm, m_application, "1", False)
        Dim cls_eval As New Cls_evaluacion
        'cls_Catastro.PT_CargarFeatureClass_SDE("GPO_ANV_AREA_NAT_VALIDA_" & v_zona_dm, m_application, "1", False)
        cls_eval.AddLayerFromFile1(m_application, "Area_Natural_" & v_zona_dm)
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click

        Me.Close()
        'cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "2", False)


        'pFeatureClass = pFeatureLayer_depa.FeatureClass
        'pFeatureCursor = pFeatureClass.Search(Nothing, False)
        'Dim pFeature As IFeature
        'pFeature = pFeatureCursor.NextFeature
        'Dim lodbtExiste_SupAR As New DataTable

        'Dim v_areasup_rese As String
        'Dim v_codigo_depa As String
        'Do Until pFeature Is Nothing

        '    v_codigo = pFeature.Value(pFeatureCursor.FindField("CD_DEPA"))
        '    If v_codigo <> "99" Then
        '        ' gloZona = pFeature.Value(pFeatureCursor.FindField("ZONA"))

        '        Try
        '            lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(50, gstrFC_Departamento, "CQUI0543.GPO_CARAM_18", v_codigo)
        '            If lodbtExiste_SupAR.Rows.Count = 0 Then

        '            Else
        '                If lodbtExiste_SupAR.Rows.Count > 0 Then
        '                    For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
        '                        v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
        '                        v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
        '                        MsgBox(v_areasup_rese, MsgBoxStyle.Critical, v_codigo_depa)
        '                    Next contador1
        '                End If
        '            End If

        '        Catch ex As Exception

        '        End Try
        '    End If
        '    pFeature = pFeatureCursor.NextFeature
        'Loop

    End Sub

    Private Sub btnagregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cbotipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbotipo.SelectedIndexChanged

        'cboZona.Items.Clear()
        'cboZona.Enabled = FalsecboZona.Items.Clear()
        ' cboZona.Enabled = False
        cbodetalle.Enabled = False
        btncalcular.Enabled = False

        If cbotipo.SelectedIndex > -1 Then
            '   pMap = pMxDoc.FocusMap


            'cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "2", False)


            Dim pFeature As IFeature

            Dim lodbtExiste_SupAR As New DataTable
            Dim dRow As DataRow
            Dim v_areasup_rese As Integer
            Dim v_codigo_depa As String
            Dim v_areaini_depa As Integer
            Dim v_cantidad As Integer
            Dim v_nm_depa As String

            Dim lodtTabla As New DataTable
            lodtTabla.Columns.Add("SELEC", Type.GetType("System.String"))
            lodtTabla.Columns.Add("CODIGO", Type.GetType("System.String"))
            lodtTabla.Columns.Add("NOMBRE", Type.GetType("System.String"))
            lodtTabla.Columns.Add("AREA", Type.GetType("System.Double"))
            lodtTabla.Columns.Add("CANTIDAD", Type.GetType("System.Double"))
            lodtTabla.Columns.Add("AREA_SUP", Type.GetType("System.Double"))
            lodtTabla.Columns.Add("PORCEN", Type.GetType("System.Double"))
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "2", False)
            pFeatureClass = pFeatureLayer_depa.FeatureClass
            pFeatureCursor = pFeatureClass.Search(Nothing, False)

            If cbotipo.SelectedItem = "SEGUN DEPARTAMENTO" Then
                tipo_selec_catnomin = "SEGUN DEPARTAMENTO"
                cbodetalle.Enabled = True
                cbodetalle.Items.Clear()
                cboZona.Enabled = False
                btnCargar.Enabled = False

                Me.cboZona.Items.Clear()
                pFeature = pFeatureCursor.NextFeature
                Do Until pFeature Is Nothing

                    v_codigo = pFeature.Value(pFeatureCursor.FindField("CD_DEPA"))
                    v_nm_depa = pFeature.Value(pFeatureCursor.FindField("NM_DEPA"))
                    If v_nm_depa = "LAGUNA UMAYO" Or v_nm_depa = "LAGO DE ARAPA" Or v_nm_depa = "LAGUNA LANGUI LAYO" Or v_nm_depa = "LAGUNA DE PARINACOCHAS" Or v_nm_depa = "LAGUNA DE JUNIN" Or v_nm_depa = "LAGUNA SALINAS" Or v_nm_depa = "LAGO TITICACA" Then

                    Else
                        cbodetalle.Items.Add(v_nm_depa)
                    End If
                    pFeature = pFeatureCursor.NextFeature
                Loop
                Me.cbodetalle.Enabled = True
                Me.cbodetalle.SelectedIndex = 1


                'actualizado control para listar los tipos de reservas



                cbotiporese.Enabled = True
                btnCargar.Enabled = True
                cbotiporese.Items.Clear()
                Me.cboZona.Items.Clear()
                Me.lblZona.Visible = False
                cboZona.Enabled = False
                Dim lodbtExiste_tipo As DataTable
                Try
                    lodbtExiste_tipo = cls_Oracle.F_Obtiene_Tipo_AreaRestringida()
                    If lodbtExiste_tipo.Rows.Count = 0 Then
                    Else
                        For contador1 As Integer = 0 To lodbtExiste_tipo.Rows.Count - 1
                            v_nm_depa = lodbtExiste_tipo.Rows(contador1).Item("TN_DESTIP")
                            If v_nm_depa = "EXPEDIENTE DE CATASTRO" Or v_nm_depa = "RESERVA TURISTICA" Or v_nm_depa = "CONCESION DE LABOR GENERAL" Or v_nm_depa = "ANAD" Then
                            Else

                                cbotiporese.Items.Add(v_nm_depa)
                            End If

                        Next contador1
                    End If
                Catch ex As Exception
                End Try

                Me.cbotiporese.Enabled = True
                Me.cbotiporese.SelectedIndex = 0




            ElseIf cbotipo.SelectedItem = "TIPO DE RESERVA" Then
                tipo_selec_catnomin = "TIPO DE RESERVA"
                cbodetalle.Enabled = True
                btnCargar.Enabled = True
                cbodetalle.Items.Clear()
                Me.cboZona.Items.Clear()
                Me.lblZona.Visible = False
                cboZona.Enabled = False
                Dim lodbtExiste_tipo As DataTable
                Try
                    lodbtExiste_tipo = cls_Oracle.F_Obtiene_Tipo_AreaRestringida()
                    If lodbtExiste_tipo.Rows.Count = 0 Then
                    Else
                        For contador1 As Integer = 0 To lodbtExiste_tipo.Rows.Count - 1
                            v_nm_depa = lodbtExiste_tipo.Rows(contador1).Item("TN_DESTIP")
                            cbodetalle.Items.Add(v_nm_depa)

                        Next contador1
                    End If
                Catch ex As Exception
                End Try

                Me.cbodetalle.Enabled = True
                Me.cbodetalle.SelectedIndex = 0
                'Me.cboZona.Visible = True
                'cboZona.Items.Add("-Selec")
                'cboZona.Items.Add("17")
                'cboZona.Items.Add("18")
                'cboZona.Items.Add("19")
                'Me.cboZona.SelectedIndex = 0

            ElseIf cbotipo.SelectedItem = "A NIVEL NACIONAL" Then
                tipo_selec_catnomin = "A NIVEL NACIONAL"
                btncalcular.Enabled = False
                cbodetalle.Enabled = False
                btnCargar.Enabled = True
                Me.cboZona.Items.Clear()
                Me.lblZona.Visible = False

                Me.lblZona.Location = New System.Drawing.Point(210, 60)
                cboZona.Items.Add("-Selec")
                cboZona.Items.Add("17")
                cboZona.Items.Add("18")
                cboZona.Items.Add("19")

                Me.cboZona.Enabled = True
                Me.cboZona.Visible = True
                '  Me.lblZona.Visible = True

                'Me.cboZona.Enabled = True
                Me.cboZona.SelectedIndex = 0

                'v_Zona = cboZona.SelectedValue


                'pFeature = pFeatureCursor.NextFeature
                'Do Until pFeature Is Nothing

                '    v_codigo = pFeature.Value(pFeatureCursor.FindField("CD_DEPA"))
                '    m_application.Caption = "GENERANDO ESTADISTICAS DE AREAS RESTRINGIDAS A NIVEL NACIONAL :  " & v_codigo


                '    If v_codigo <> "99" Then

                '        Try
                '            lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(53, gstrFC_Departamento, "CQUI0543.GPO_CARAM_18", v_codigo)
                '            If lodbtExiste_SupAR.Rows.Count = 0 Then
                '            Else
                '                For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                '                    v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                '                    v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                '                    v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
                '                    v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
                '                    dRow = lodtTabla.NewRow
                '                    dRow.Item("CODIGO") = v_codigo_depa
                '                    dRow.Item("NOMBRE") = "DEPA"
                '                    dRow.Item("AREA") = v_areaini_depa
                '                    dRow.Item("CANTIDAD") = v_cantidad
                '                    dRow.Item("AREA_SUP") = v_areasup_rese
                '                    dRow.Item("PORCEN") = (Format(Math.Round((v_areasup_rese / v_areaini_depa) * 100, 2), "###,###.00")).ToString
                '                    lodtTabla.Rows.Add(dRow)
                '                Next contador1
                '            End If
                '        Catch ex As Exception
                '        End Try
                '    End If
                '    pFeature = pFeatureCursor.NextFeature

                'Loop

                'Me.dgdDetalle.DataSource = lodtTabla
                'PT_Agregar_Funciones_EVAL() : PT_Forma_Grilla_EVAL()
                'Me.chkEstado.Checked = False : Me.chkEstado.Checked = True
                'For i As Integer = 0 To Me.dgdDetalle.RowCount - 1
                '    dgdDetalle.Item(i, "SELEC") = True
                'Next
                'Me.dgdDetalle.AllowUpdate = True
                'dgdDetalle.Focus()
            End If
        ElseIf cbotipo.SelectedIndex = -1 Then

            MsgBox("No ha seleccionado ningun tipo de consulta", MsgBoxStyle.Information, "OBSERVACION")

        End If

    End Sub

    Private Sub cbodetalle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbodetalle.SelectedIndexChanged
        '    Exit Sub

        Dim lodtTabla As New DataTable
        If tipo_selec_catnomin = "SEGUN DEPARTAMENTO" Then
            btncalcular.Enabled = False
            Dim zona_sele As String
            cboZona.Items.Clear()

            pMap = pMxDoc.FocusMap
            Dim pFeature As IFeature
            Dim v_nm_depa As String

           

            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "2", False)
            pFeatureClass = pFeatureLayer_depa.FeatureClass
            v_nm_depa = cbodetalle.SelectedItem
            Dim lo_Filtro_Ubigeo As String
            Dim lodbtZona As New DataTable : Dim lodbtUbigeo As New DataTable

            lo_Filtro_Ubigeo = "NM_DEPA = '" & v_nm_depa.ToUpper & "'"
            lodbtUbigeo = cls_Oracle.FT_Selecciona_x_Ubigeo(1, v_nm_depa.ToUpper)

            If lodbtUbigeo.Rows.Count = 0 Then
                MsgBox("No existe registro en la capa, verificar datos ", MsgBoxStyle.Information, "[SIGCATMIN]")
            End If

            Me.lblregistro.Text = "Se ha encontrado:  " & lodbtUbigeo.Rows.Count & "  Registro(s)"
            If lodbtUbigeo.Rows.Count = 0 Then
                '  MsgBox("No se ha encontrado ningun registro", MsgBoxStyle.Critical, "Observacion")
                Exit Sub
            End If
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "2", True)
            lo_Filtro_Ubigeo = "CD_DEPA = '" & lodbtUbigeo.Rows(0).Item("UBIGEO") & "'"
            lodbtZona = cls_Oracle.FT_Selecciona_x_Zona(lodbtUbigeo.Rows(0).Item("UBIGEO") & "0000")

            'Dim cls_zona As New cls_Prueba

            If lodbtZona.Rows.Count = 2 Then 'And lodbtUbigeo.Rows.Count = 1 Then

                Select Case lodbtZona.Rows.Count
                    Case 0
                        MsgBox("No existe  la capa, ingrese nuevamente", MsgBoxStyle.Information, "[SIGCATMIN]")
                        Exit Sub
                    Case 2
                        Me.cboZona.Enabled = True
                        Me.lblZona.Location = New System.Drawing.Point(290, 97)
                        Me.lblZona.Text = "Zona: "
                        ' cboZona.DataSource = lodbtZona
                        'llenando la zona del datagrid al combox

                        For w As Integer = 0 To lodbtZona.Rows.Count - 1
                            zona_sele = lodbtZona.Rows(w).Item("DESCRIPCION").ToString
                            cboZona.Items.Add(zona_sele)

                        Next w

                        v_Zona = cboZona.SelectedValue
                        'cboZona.DisplayMember = "DESCRIPCION"
                        'cboZona.ValueMember = "CODIGO"
                        Me.cboZona.SelectedIndex = 1
                        Me.cboZona.Enabled = False

                        cls_Catastro.Actualizar_DM(Me.cboZona.SelectedValue)
                    Case 3
                        Me.lblZona.Location = New System.Drawing.Point(210, 60)
                        Me.cboZona.Visible = True
                        Me.cboZona.Enabled = True
                        Me.lblZona.Visible = True
                        Me.lblZona.Text = "Este Departamento seleccionado se encuentra entre " & vbNewLine & "2 ZONAS, seleccione una ZONA"
                        'Dim lodtvZona As New DataView(p_Zona, Nothing, "DESCRIPCION ASC", DataViewRowState.CurrentRows)
                        ' cboZona.DataSource = lodbtZona
                        For w As Integer = 0 To lodbtZona.Rows.Count - 1
                            zona_sele = lodbtZona.Rows(w).Item("DESCRIPCION").ToString
                            cboZona.Items.Add(zona_sele)
                        Next w
                        v_Zona = cboZona.SelectedValue
                        'cboZona.DisplayMember = "DESCRIPCION"
                        'cboZona.ValueMember = "CODIGO"
                        Me.cboZona.Enabled = True
                End Select
            Else

                '  Me.lblZona.Location = New System.Drawing.Point(236, 80)
                Me.lblZona.Location = New System.Drawing.Point(210, 60)
                Me.cboZona.Visible = True
                Me.cboZona.Enabled = True
                Me.lblZona.Visible = True
                Me.lblZona.Text = "Este Departamento seleccionado se encuentra entre " & vbNewLine & "2 ZONAS, seleccione una ZONA"
                'Dim lodtvZona As New DataView(lodbtZona, Nothing, "DESCRIPCION ASC", DataViewRowState.CurrentRows)
                'cboZona.DataSource = lodbtZona
                For w As Integer = 0 To lodbtZona.Rows.Count - 1
                    zona_sele = lodbtZona.Rows(w).Item("DESCRIPCION").ToString
                    cboZona.Items.Add(zona_sele)
                Next w
                'cboZona.DisplayMember = "DESCRIPCION"
                'cboZona.ValueMember = "CODIGO"
                Me.cboZona.Enabled = True
                Me.cboZona.SelectedIndex = 1
                cls_Catastro.Actualizar_DM(Me.cboZona.SelectedValue)
                Me.btncalcular.Enabled = False
                v_Zona = cboZona.SelectedValue
            End If
        ElseIf tipo_selec_catnomin = "TIPO DE RESERVA" Then
            btncalcular.Enabled = False
            Me.cboZona.Enabled = True
            cboZona.Items.Clear()

            '   Me.cboZona.Items.Clear()
            'pMap = pMxDoc.FocusMap
            'Dim pFeature As IFeature
            'Dim v_nm_depa As String

            'cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "2", False)
            'pFeatureClass = pFeatureLayer_depa.FeatureClass
            'v_nm_depa = cbodetalle.SelectedItem
            'Dim lo_Filtro_Ubigeo As String
            'Dim lodbtZona As New DataTable : Dim lodbtUbigeo As New DataTable

            'lo_Filtro_Ubigeo = "NM_DEPA = '" & v_nm_depa.ToUpper & "'"
            'lodbtUbigeo = cls_Oracle.FT_Selecciona_x_Ubigeo(1, v_nm_depa.ToUpper)

            'If lodbtUbigeo.Rows.Count = 0 Then
            '    MsgBox("No existe registro en la capa, verificar datos ", MsgBoxStyle.Information, "[BDGeocatmin]")
            'End If

            'Me.lblregistro.Text = "Se ha encontrado:  " & lodbtUbigeo.Rows.Count & "  Registro(s)"
            'If lodbtUbigeo.Rows.Count = 0 Then
            '    '  MsgBox("No se ha encontrado ningun registro", MsgBoxStyle.Critical, "Observacion")
            '    Exit Sub
            'End If
            'cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "2", True)
            'lo_Filtro_Ubigeo = "CD_DEPA = '" & lodbtUbigeo.Rows(0).Item("UBIGEO") & "'"
            'lodbtZona = cls_Oracle.FT_Selecciona_x_Zona(lodbtUbigeo.Rows(0).Item("UBIGEO") & "0000")

            ''Dim cls_zona As New cls_Prueba

            'If lodbtZona.Rows.Count = 2 Then 'And lodbtUbigeo.Rows.Count = 1 Then

            '    Select Case lodbtZona.Rows.Count
            '        Case 0
            '            MsgBox("No existe  la capa, ingrese nuevamente", MsgBoxStyle.Information, "[BDGEOCATMIN]")
            '            Exit Sub
            '        Case 2
            '            Me.cboZona.Enabled = True
            '            Me.lblZona.Location = New System.Drawing.Point(290, 97)
            '            Me.lblZona.Text = "Zona: "
            '            cboZona.DataSource = lodbtZona
            '            v_Zona = cboZona.SelectedValue
            '            cboZona.DisplayMember = "DESCRIPCION"
            '            cboZona.ValueMember = "CODIGO"
            '            Me.cboZona.SelectedIndex = 1
            '            Me.cboZona.Enabled = False
            '            cls_Catastro.Actualizar_DM(Me.cboZona.SelectedValue)
            '        Case 3
            '            Me.lblZona.Location = New System.Drawing.Point(210, 60)
            '            Me.cboZona.Visible = True
            '            Me.cboZona.Enabled = True
            '            Me.lblZona.Visible = True
            '            Me.lblZona.Text = "Este Departamento seleccionado se encuentra entre " & vbNewLine & "2 ZONAS, seleccione una ZONA"
            '            'Dim lodtvZona As New DataView(p_Zona, Nothing, "DESCRIPCION ASC", DataViewRowState.CurrentRows)
            '            cboZona.DataSource = lodbtZona
            '            v_Zona = cboZona.SelectedValue
            '            cboZona.DisplayMember = "DESCRIPCION"
            '            cboZona.ValueMember = "CODIGO"
            '            Me.cboZona.Enabled = True
            '    End Select
            'Else

            '  Me.lblZona.Location = New System.Drawing.Point(236, 80)
            Me.cboZona.Visible = True

            Me.cboZona.Enabled = True
            Me.lblZona.Location = New System.Drawing.Point(210, 60)
            cboZona.Items.Add("-Selec")
            cboZona.Items.Add("17")
            cboZona.Items.Add("18")
            cboZona.Items.Add("19")
            Me.cboZona.Visible = True


            ' Me.lblZona.Text = "Este Departamento seleccionado se encuentra entre " & vbNewLine & "2 ZONAS, seleccione una ZONA"
            'Dim lodtvZona As New DataView(lodbtZona, Nothing, "DESCRIPCION ASC", DataViewRowState.CurrentRows)
            'cboZona.DataSource = lodbtZona
            'cboZona.DisplayMember = "DESCRIPCION"
            'cboZona.ValueMember = "CODIGO"
            Me.cboZona.Enabled = True
            Me.cboZona.SelectedIndex = 0
            'cls_Catastro.Actualizar_DM(Me.cboZona.SelectedValue)
            'Me.btncalcular.Enabled = False
            v_Zona = cboZona.SelectedValue
        End If



    End Sub

    Private Sub btncalcular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncalcular.Click
        pMap = pMxDoc.FocusMap
        Me.btnExcel.Enabled = True
        Dim pFeature As IFeature
        Dim lodbtExiste_SupAR As New DataTable
        Dim dRow As DataRow
        Dim v_areasup_rese As Double
        Dim v_codigo_depa As String
        Dim v_areaini_depa As Double
        Dim v_cantidad As Integer
        Dim v_nm_depa As String
        Dim v_nm_depa1 As String
        Dim v_nm_laguna As String
        Dim v_areaini_depa1 As Double
        Dim v_areasup_rese1 As Double
        Dim v_areasup_rese_fin As Double
        Dim v_porcen As Double
        Dim lodtTabla As New DataTable
        Dim s1 As Double = 0
        Dim s2 As Double = 0
        Dim lodbtExiste_tipo As New DataTable


        Dim ruta As String = glo_Path & "reporte.txt"
        Dim sw As New System.IO.StreamWriter(ruta)

        lodtTabla.Columns.Add("SELEC", Type.GetType("System.String"))
        lodtTabla.Columns.Add("CODIGO", Type.GetType("System.String"))
        lodtTabla.Columns.Add("NOMBRE", Type.GetType("System.String"))
        lodtTabla.Columns.Add("TP_RESE", Type.GetType("System.String"))
        lodtTabla.Columns.Add("NM_TPRESE", Type.GetType("System.String"))
        lodtTabla.Columns.Add("AREA", Type.GetType("System.Double"))
        lodtTabla.Columns.Add("CANTI", Type.GetType("System.Double"))
        lodtTabla.Columns.Add("AREA_NETA", Type.GetType("System.Double"))
        lodtTabla.Columns.Add("PORCEN", Type.GetType("System.Double"))

        If tipo_selec_catnomin = "XXXSEGUN DEPARTAMENTO" Then
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "2", False)
            pFeatureClass = pFeatureLayer_depa.FeatureClass
            v_nm_depa = cbodetalle.SelectedItem
            v_Zona = cboZona.SelectedItem
            pFeatureClass = pFeatureLayer_depa.FeatureClass
            Dim pqueryfilter As IQueryFilter
            pqueryfilter = New QueryFilter

            pqueryfilter.WhereClause = "NM_DEPA = '" & v_nm_depa & "'"
            pFeatureCursor = pFeatureClass.Search(pqueryfilter, True)
            pFeature = pFeatureCursor.NextFeature
            Do Until pFeature Is Nothing
                v_codigo = pFeature.Value(pFeatureCursor.FindField("CD_DEPA"))
                v_nm_depa1 = pFeature.Value(pFeatureCursor.FindField("NM_DEPA"))
                If v_codigo = "99" Then  'SOLO PARA MAR Y FRONTERA
                    If v_nm_depa1 = "MAR" Or v_nm_depa1 = "FUERA DEL PERU" Then

                        'Select Case v_Zona
                        '    Case "18"
                        ''  lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, gstrFC_Departamento, "CQUI0543.GPO_CARAM_" & v_Zona, v_nm_depa1)
                        lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_depa1)
                        '  Case "17"
                        'lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, "GPO_DEP_DEPARTAMENTO_17", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_depa1)
                        '   Case "19"
                        'lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, "GPO_DEP_DEPARTAMENTO_19", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_depa1)

                        'End Select


                        If lodbtExiste_SupAR.Rows.Count = 0 Then
                            sw.WriteLine(v_nm_depa1)
                            sw.WriteLine(v_codigo)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                        Else
                            For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
                                v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
                            Next contador1

                            dRow = lodtTabla.NewRow
                            dRow.Item("CODIGO") = v_codigo
                            dRow.Item("NOMBRE") = v_nm_depa1
                            dRow.Item("AREA") = v_areaini_depa
                            dRow.Item("CANTIDAD") = v_cantidad
                            dRow.Item("AREA_SUP") = v_areasup_rese
                            'dRow.Item("AREA_NETA") = Format(Math.Round(v_areasup_rese), "###,###.0000").ToString        ' v_areasup_rese
                            dRow.Item("PORCEN") = (Format(Math.Round(((v_areasup_rese) / (v_areaini_depa)) * 100, 2), "###,###.00")).ToString
                            v_porcen = (Format(Math.Round(((v_areasup_rese) / (v_areaini_depa)) * 100, 2), "###,###.00")).ToString
                            lodtTabla.Rows.Add(dRow)
                            sw.WriteLine(v_nm_depa1)
                            sw.WriteLine(v_codigo)
                            sw.WriteLine(v_areaini_depa)
                            sw.WriteLine(v_cantidad)
                            sw.WriteLine(v_areasup_rese)
                            sw.WriteLine((Format(Math.Round(((v_areasup_rese) / (v_areaini_depa)) * 100, 2), "###,###.00")).ToString)

                        End If
                    Else


                    End If
                ElseIf v_codigo <> "99" Then  'NORMAL

                    ' gloZona = pFeature.Value(pFeatureCursor.FindField("ZONA"))

                    Try
                        ' lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(53, gstrFC_Departamento, "CQUI0543.GPO_CARAM_18", v_codigo)

                        'Select Case v_Zona
                        '   Case "18"
                        lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(53, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_codigo)
                        '  Case "17"
                        'lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(53, "GPO_DEP_DEPARTAMENTO_17", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_codigo)
                        '   Case "19"
                        'lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(53, "GPO_DEP_DEPARTAMENTO_19", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_codigo)

                        'End Select

                        If lodbtExiste_SupAR.Rows.Count = 0 Then

                            sw.WriteLine(v_nm_depa1)
                            sw.WriteLine(v_codigo)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                            sw.WriteLine(0)


                        Else
                            If v_codigo = "04" Or v_codigo = "12" Or v_codigo = "21" Or v_codigo = "05" Or v_codigo = "08" Then  '
                                'CONSULTA LOS DPTO QUE TIENEN LAGUNA
                                For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                    v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                    v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                    v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
                                    v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
                                Next contador1

                                If v_codigo = "05" Then
                                    v_nm_laguna = "LAGUNA DE PARINACOCHAS"
                                ElseIf v_codigo = "04" Then
                                    v_nm_laguna = "LAGUNA SALINAS"
                                ElseIf v_codigo = "08" Then
                                    v_nm_laguna = "LAGUNA LAGUI LAYO"
                                ElseIf v_codigo = "12" Then
                                    v_nm_laguna = "LAGUNA DE JUNIN"
                                ElseIf v_codigo = "21" Then
                                    v_nm_laguna = "LAGUNAS DE PUNO"
                                End If
                                If v_nm_laguna = "LAGUNAS DE PUNO" Then  'SOLO PARA DPTO PUNO SUMAR SUS 3 LAGOS
                                    Dim v_areaini_depa2 As Integer = 0
                                    Dim v_areasup_rese2 As Integer = 0

                                    For j As Integer = 1 To 3
                                        If j = 1 Then
                                            v_nm_laguna = "LAGUNA UMAYO"
                                        ElseIf j = 2 Then
                                            v_nm_laguna = "LAGO DE ARAPA"
                                        ElseIf j = 3 Then
                                            v_nm_laguna = "LAGO TITICACA"
                                        End If
                                        'Select Case v_Zona
                                        '   Case "18"
                                        lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna)
                                        '   Case "17"
                                        'lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, "GPO_DEP_DEPARTAMENTO_17", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna)
                                        '   Case "19"
                                        'lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, "GPO_DEP_DEPARTAMENTO_19", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna)
                                        ' End Select

                                        For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                            v_areasup_rese1 = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                            v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                            v_areaini_depa1 = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")

                                        Next contador1
                                        v_areaini_depa2 = v_areaini_depa2 + v_areaini_depa1
                                        v_areasup_rese2 = v_areasup_rese2 + v_areasup_rese1
                                        'Sumando Areas

                                    Next j

                                    dRow = lodtTabla.NewRow
                                    dRow.Item("CODIGO") = v_codigo_depa
                                    dRow.Item("NOMBRE") = v_nm_depa1
                                    dRow.Item("AREA") = v_areaini_depa + v_areaini_depa2
                                    dRow.Item("CANTIDAD") = v_cantidad
                                    dRow.Item("AREA_SUP") = v_areasup_rese + v_areasup_rese2
                                    ''dRow.Item("AREA_NETA") = Format(Math.Round(v_areasup_rese + v_areasup_rese2), "###,###.0000").ToString()
                                    dRow.Item("PORCEN") = (Format(Math.Round(((v_areasup_rese + v_areasup_rese2) / (v_areaini_depa + v_areaini_depa2)) * 100, 2), "###,###.00")).ToString
                                    lodtTabla.Rows.Add(dRow)

                                    sw.WriteLine(v_nm_depa1)
                                    sw.WriteLine(v_codigo)
                                    sw.WriteLine(v_areaini_depa + v_areaini_depa2)
                                    sw.WriteLine(v_cantidad)
                                    sw.WriteLine(v_areasup_rese + v_areasup_rese2)
                                    sw.WriteLine((Format(Math.Round(((v_areasup_rese + v_areasup_rese2) / (v_areaini_depa + v_areaini_depa2)) * 100, 2), "###,###.00")).ToString)


                                Else  'DIFERENTE DE PUNO, SUMAR LAGO INDIVIDUAL

                                    'Select Case v_Zona
                                    '  Case "18"
                                    lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna)
                                    '    Case "17"
                                    ' lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, "GPO_DEP_DEPARTAMENTO_17", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna)
                                    '  Case "19"
                                    'lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, "GPO_DEP_DEPARTAMENTO_19", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna)
                                    ' End Select

                                    For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                        v_areasup_rese1 = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                        v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                        v_areaini_depa1 = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")

                                    Next contador1

                                    'Sumando Areas

                                    dRow = lodtTabla.NewRow
                                    dRow.Item("CODIGO") = v_codigo_depa
                                    dRow.Item("NOMBRE") = v_nm_depa1
                                    dRow.Item("AREA") = v_areaini_depa + v_areaini_depa1
                                    dRow.Item("CANTIDAD") = v_cantidad
                                    dRow.Item("AREA_SUP") = v_areasup_rese + v_areasup_rese1
                                    'dRow.Item("AREA_NETA") = Format(Math.Round(v_areasup_rese + v_areasup_rese1), "###,###.0000").ToString()
                                    dRow.Item("PORCEN") = (Format(Math.Round(((v_areasup_rese + v_areasup_rese1) / (v_areaini_depa + v_areaini_depa1)) * 100, 2), "###,###.00")).ToString
                                    lodtTabla.Rows.Add(dRow)

                                    sw.WriteLine(v_nm_depa1)
                                    sw.WriteLine(v_codigo)
                                    sw.WriteLine(v_areaini_depa + v_areaini_depa1)
                                    sw.WriteLine(v_cantidad)
                                    sw.WriteLine(v_areasup_rese + v_areasup_rese1)
                                    sw.WriteLine((Format(Math.Round(((v_areasup_rese + v_areasup_rese1) / (v_areaini_depa + v_areaini_depa1)) * 100, 2), "###,###.00")).ToString)


                                End If


                            Else  'DPTO NORMAL QUE NO TIENEN  LAGUNA

                                For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                    v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                    v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                    v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
                                    v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
                                    dRow = lodtTabla.NewRow
                                    dRow.Item("CODIGO") = v_codigo_depa
                                    dRow.Item("NOMBRE") = v_nm_depa1
                                    dRow.Item("AREA") = v_areaini_depa
                                    dRow.Item("CANTIDAD") = v_cantidad
                                    dRow.Item("AREA_SUP") = v_areasup_rese
                                    ' dRow.Item("AREA_NETA") = Format(Math.Round(v_areasup_rese), "###,###.0000").ToString()
                                    dRow.Item("PORCEN") = (Format(Math.Round((v_areasup_rese / v_areaini_depa) * 100, 2), "###,###.00")).ToString
                                    lodtTabla.Rows.Add(dRow)


                                    sw.WriteLine(v_nm_depa1)
                                    sw.WriteLine(v_codigo)
                                    sw.WriteLine(v_areaini_depa)
                                    sw.WriteLine(v_cantidad)
                                    sw.WriteLine(v_areasup_rese)
                                    sw.WriteLine((Format(Math.Round((v_areasup_rese / v_areaini_depa) * 100, 2), "###,###.00")).ToString)


                                Next contador1
                            End If
                        End If
                    Catch ex As Exception
                    End Try
                End If
                pFeature = pFeatureCursor.NextFeature

            Loop
            sw.Close()
            Me.btnExcel.Enabled = False
            'End If

            '-----------------------------------------------

            ' v_nm_depa = cbodetalle.SelectedItem
            '  v_Zona = cboZona.SelectedValue

        ElseIf tipo_selec_catnomin = "SEGUN DEPARTAMENTO" Then


            Dim sele_elemento As String
            Dim v_tipo_rese As String
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "2", False)
            pFeatureClass = pFeatureLayer_depa.FeatureClass
            v_nm_depa = cbodetalle.SelectedItem
            v_Zona = cboZona.SelectedItem
            pFeatureClass = pFeatureLayer_depa.FeatureClass
            Dim pqueryfilter As IQueryFilter
            pqueryfilter = New QueryFilter

            pqueryfilter.WhereClause = "NM_DEPA = '" & v_nm_depa & "'"
            pFeatureCursor = pFeatureClass.Search(pqueryfilter, True)
            pFeature = pFeatureCursor.NextFeature
            Do Until pFeature Is Nothing
                v_codigo = pFeature.Value(pFeatureCursor.FindField("CD_DEPA"))
                v_nm_depa1 = pFeature.Value(pFeatureCursor.FindField("NM_DEPA"))


                Dim lostrRetorno_cuenta As String

                ' lostrRetorno_cuenta = cls_Oracle.FT_SG_CUENTA_REG_TABLA_TPRESE("DEPARTAMENTAL", v_codigo)
                'If lostrRetorno_cuenta > 0 Then
                '  cls_Oracle.FT_SG_D_REGION_X_AR("DEL", "", "", v_codigo, "", "", "", "")
                ' End If

                If v_codigo = "99" Then  'SOLO PARA MAR Y FRONTERA
                    If v_nm_depa1 = "MAR" Or v_nm_depa1 = "FUERA DEL PERU" Then
                        For elemento As Integer = 0 To cbotiporese.Items.Count - 1

                            sele_elemento = cbotiporese.Items.Item(elemento)


                            'obteniendo el codigo del tipo de reservva
                            Try
                                lodbtExiste_tipo = cls_Oracle.FT_OBTIENE_TIPORESE(sele_elemento)
                            Catch ex As Exception
                                MsgBox("error ", MsgBoxStyle.Critical, "1")
                            End Try

                            '   Dim v_tipo_rese As String


                            For contador2 As Integer = 0 To lodbtExiste_tipo.Rows.Count - 1
                                v_tipo_rese = lodbtExiste_tipo.Rows(contador2).Item("TN_CODTIP")

                            Next contador2


                            lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(4, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_depa1, sele_elemento)

                            '  lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_depa1)




                        Next elemento

                        If lodbtExiste_SupAR.Rows.Count = 0 Then
                            sw.WriteLine(v_nm_depa1)
                            sw.WriteLine(sele_elemento)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                        Else
                            For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
                                v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
                            Next contador1

                            dRow = lodtTabla.NewRow
                            dRow.Item("CODIGO") = v_codigo
                            dRow.Item("NOMBRE") = v_nm_depa1
                            dRow.Item("TP_RESE") = v_tipo_rese
                            dRow.Item("NM_TPRESE") = sele_elemento
                            dRow.Item("AREA") = (Format(Math.Round(v_areaini_depa, 4), "###,###.0000")).ToString 'v_areaini_depa
                            dRow.Item("CANTI") = v_cantidad
                            '  dRow.Item("AREA_SUP") = v_areasup_rese
                            dRow.Item("AREA_NETA") = (Format(Math.Round((v_areasup_rese), 4), "###,###.0000")).ToString        ' v_areasup_rese
                            dRow.Item("PORCEN") = (Format(Math.Round(((v_areasup_rese) / (v_areaini_depa)) * 100, 2), "###,###.00")).ToString






                            lodtTabla.Rows.Add(dRow)
                            sw.WriteLine(v_nm_depa1)
                            sw.WriteLine(sele_elemento)
                            sw.WriteLine((Format(Math.Round(v_areaini_depa, 4), "###,###.0000")).ToString)
                            sw.WriteLine(v_cantidad)
                            sw.WriteLine((Format(Math.Round((v_areasup_rese), 4), "###,###.0000")).ToString)
                            sw.WriteLine((Format(Math.Round(((v_areasup_rese) / (v_areaini_depa)) * 100, 2), "###,###.00")).ToString)

                            '  Dim lostrRetorno_cuenta As String = ""

                            lostrRetorno_cuenta = cls_Oracle.FT_SG_CUENTA_REG_TABLA_TPRESE("DEPARTALMENTAL", v_codigo)
                            If lostrRetorno_cuenta > 0 Then
                                cls_Oracle.FT_SG_D_REGION_X_AR("DEL", "", "", v_codigo_depa, "", "", "", gstrUsuarioAcceso, "")
                            End If

                            cls_Oracle.FT_SG_D_REGION_X_AR("INS", v_tipo_rese, sele_elemento, v_codigo_depa, v_areaini_depa1.ToString, v_areasup_rese1.ToString, v_porcen, gstrUsuarioAcceso, "Fuera del Peru")

                            'End If


                        End If
                    Else


                    End If
                ElseIf v_codigo <> "99" Then  'NORMAL

                    ' gloZona = pFeature.Value(pFeatureCursor.FindField("ZONA"))

                    Try
                        ' lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(53, gstrFC_Departamento, "CQUI0543.GPO_CARAM_18", v_codigo)

                        For elemento As Integer = 0 To cbotiporese.Items.Count - 1

                            sele_elemento = cbotiporese.Items.Item(elemento)  'captura tipo de reserva
                            '  If sele_elemento <> "EXPEDIENTE DE CATASTRO" Then


                            'obteniendo el codigo del tipo de reservva
                            Try
                                lodbtExiste_tipo = cls_Oracle.FT_OBTIENE_TIPORESE(sele_elemento)
                            Catch ex As Exception
                                MsgBox("error ", MsgBoxStyle.Critical, "SIGCATMIN")
                            End Try

                            '   Dim v_tipo_rese As String


                            For contador2 As Integer = 0 To lodbtExiste_tipo.Rows.Count - 1
                                v_tipo_rese = lodbtExiste_tipo.Rows(contador2).Item("TN_CODTIP")

                            Next contador2

                            'Realizando la superposicion de los tipos de reservas vs el departamento


                            ' lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(53, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_codigo)
                            lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(1, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_codigo, sele_elemento)




                            If lodbtExiste_SupAR.Rows.Count = 0 Then

                                sw.WriteLine(v_nm_depa1)
                                sw.WriteLine(sele_elemento)
                                sw.WriteLine(0)
                                sw.WriteLine(0)
                                sw.WriteLine(0)
                                sw.WriteLine(0)


                            Else
                                If v_codigo = "04" Or v_codigo = "12" Or v_codigo = "21" Or v_codigo = "05" Or v_codigo = "08" Then  '
                                    'CONSULTA LOS DPTO QUE TIENEN LAGUNA
                                    For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                        v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                        v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                        v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
                                        v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
                                    Next contador1

                                    If v_codigo = "05" Then
                                        v_nm_laguna = "LAGUNA DE PARINACOCHAS"
                                    ElseIf v_codigo = "04" Then
                                        v_nm_laguna = "LAGUNA SALINAS"
                                    ElseIf v_codigo = "08" Then
                                        v_nm_laguna = "LAGUNA LAGUI LAYO"
                                    ElseIf v_codigo = "12" Then
                                        v_nm_laguna = "LAGUNA DE JUNIN"
                                    ElseIf v_codigo = "21" Then
                                        v_nm_laguna = "LAGUNAS DE PUNO"
                                    End If
                                    If v_nm_laguna = "LAGUNAS DE PUNO" Then  'SOLO PARA DPTO PUNO SUMAR SUS 3 LAGOS
                                        Dim v_areaini_depa2 As Integer = 0
                                        Dim v_areasup_rese2 As Integer = 0

                                        For j As Integer = 1 To 3
                                            If j = 1 Then
                                                v_nm_laguna = "LAGUNA UMAYO"
                                            ElseIf j = 2 Then
                                                v_nm_laguna = "LAGO DE ARAPA"
                                            ElseIf j = 3 Then
                                                v_nm_laguna = "LAGO TITICACA"
                                            End If
                                            'Select Case v_Zona

                                            '     lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna)
                                            lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(4, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna, sele_elemento)

                                            For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                                v_areasup_rese1 = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                                v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                                v_areaini_depa1 = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")

                                            Next contador1
                                            v_areaini_depa2 = v_areaini_depa2 + v_areaini_depa1
                                            v_areasup_rese2 = v_areasup_rese2 + v_areasup_rese1
                                            'Sumando Areas

                                        Next j

                                        dRow = lodtTabla.NewRow
                                        dRow.Item("CODIGO") = v_codigo_depa
                                        dRow.Item("NOMBRE") = v_nm_depa1
                                        dRow.Item("TP_RESE") = v_tipo_rese
                                        dRow.Item("NM_TPRESE") = sele_elemento
                                        dRow.Item("AREA") = (Format(Math.Round(((v_areaini_depa + v_areaini_depa2)), 4), "###,###.0000")).ToString 'v_areaini_depa + v_areaini_depa2
                                        dRow.Item("CANTI") = v_cantidad
                                        ' dRow.Item("AREA_SUP") = v_areasup_rese + v_areasup_rese2
                                        dRow.Item("AREA_NETA") = (Format(Math.Round(((v_areasup_rese + v_areasup_rese2)), 4), "###,###.0000")).ToString
                                        dRow.Item("PORCEN") = (Format(Math.Round(((v_areasup_rese + v_areasup_rese2) / (v_areaini_depa + v_areaini_depa2)) * 100, 2), "###,###.00")).ToString


                                        v_areasup_rese_fin = (Format(Math.Round(((v_areasup_rese + v_areasup_rese2)), 4), "###,###.0000")).ToString
                                        v_porcen = (Format(Math.Round(((v_areasup_rese + v_areasup_rese2) / (v_areaini_depa + v_areaini_depa2)) * 100, 2), "###,###.00")).ToString
                                        lodtTabla.Rows.Add(dRow)
                                        ' Dim lostrRetorno_cuenta As String

                                        'If lostrRetorno_cuenta > 0 Then
                                        '    cls_Oracle.FT_SG_D_REGION_X_AR("DEL", "", "", v_codigo_depa, "", "", "", "")
                                        'End If

                                        cls_Oracle.FT_SG_D_REGION_X_AR("INS", v_tipo_rese, sele_elemento, v_codigo_depa, v_areaini_depa.ToString, v_areasup_rese_fin.ToString, v_porcen.ToString, gstrUsuarioAcceso, "Lago Puno")

                                        sw.WriteLine(v_nm_depa1)
                                        sw.WriteLine(sele_elemento)
                                        sw.WriteLine((Format(Math.Round((v_areaini_depa + v_areaini_depa2), 4), "###,###.0000")).ToString)
                                        sw.WriteLine(v_cantidad)
                                        sw.WriteLine(v_areasup_rese_fin)
                                        sw.WriteLine(v_porcen)



                                    Else  'DIFERENTE DE PUNO, SUMAR LAGO INDIVIDUAL


                                        'lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna)
                                        lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(4, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna, sele_elemento)





                                        'If lodbtExiste_SupAR.Rows.Count = 0 Then

                                        '    sw.WriteLine(v_nm_depa1)
                                        '    sw.WriteLine(v_codigo)
                                        '    sw.WriteLine(0)
                                        '    sw.WriteLine(0)
                                        '    sw.WriteLine(0)
                                        '    sw.WriteLine(0)


                                        'Else
                                        For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                            v_areasup_rese1 = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                            v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                            v_areaini_depa1 = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")

                                        Next contador1

                                        'Sumando Areas

                                        dRow = lodtTabla.NewRow
                                        dRow.Item("CODIGO") = v_codigo_depa
                                        dRow.Item("NOMBRE") = v_nm_depa1
                                        dRow.Item("TP_RESE") = v_tipo_rese
                                        dRow.Item("NM_TPRESE") = sele_elemento
                                        dRow.Item("AREA") = (Format(Math.Round(((v_areaini_depa + v_areaini_depa1)), 4), "###,###.0000")).ToString   'v_areaini_depa + v_areaini_depa1
                                        dRow.Item("CANTI") = v_cantidad
                                        ' dRow.Item("AREA_SUP") = v_areasup_rese + v_areasup_rese1
                                        dRow.Item("AREA_NETA") = (Format(Math.Round(((v_areasup_rese + v_areasup_rese1)), 4), "###,###.0000")).ToString
                                        dRow.Item("PORCEN") = (Format(Math.Round(((v_areasup_rese + v_areasup_rese1) / (v_areaini_depa + v_areaini_depa1)) * 100, 2), "###,###.00")).ToString
                                        v_porcen = (Format(Math.Round(((v_areasup_rese + v_areasup_rese1) / (v_areaini_depa + v_areaini_depa1)) * 100, 2), "###,###.00")).ToString
                                        v_areasup_rese_fin = (Format(Math.Round(((v_areasup_rese + v_areasup_rese1)), 4), "###,###.0000")).ToString
                                        lodtTabla.Rows.Add(dRow)

                                        'Dim lostrRetorno_cuenta As String

                                        'If lostrRetorno_cuenta > 0 Then
                                        '    cls_Oracle.FT_SG_D_REGION_X_AR("DEL", "", "", v_codigo_depa, "", "", "", "")
                                        'End If

                                        cls_Oracle.FT_SG_D_REGION_X_AR("INS", v_tipo_rese, sele_elemento, v_codigo_depa, v_areaini_depa.ToString, v_areasup_rese_fin.ToString, v_porcen.ToString, gstrUsuarioAcceso, "Lagunas")


                                        sw.WriteLine(v_nm_depa1)
                                        sw.WriteLine(sele_elemento)
                                        sw.WriteLine((Format(Math.Round((v_areaini_depa + v_areaini_depa1), 4), "###,###.0000")).ToString)
                                        sw.WriteLine(v_cantidad)
                                        sw.WriteLine(v_areasup_rese_fin)
                                        sw.WriteLine(v_porcen)

                                    End If
                                    ' End If


                                Else  'DPTO NORMAL QUE NO TIENEN  LAGUNA

                                    For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                        v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                        v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                        v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
                                        v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
                                        dRow = lodtTabla.NewRow
                                        dRow.Item("CODIGO") = v_codigo_depa
                                        dRow.Item("NOMBRE") = v_nm_depa1
                                        dRow.Item("TP_RESE") = v_tipo_rese
                                        dRow.Item("NM_TPRESE") = sele_elemento
                                        dRow.Item("AREA") = (Format(Math.Round((v_areaini_depa), 4), "###,###.0000")).ToString()   'v_areaini_depa
                                        dRow.Item("CANTI") = v_cantidad
                                        'dRow.Item("AREA_SUP") = v_areasup_rese
                                        dRow.Item("AREA_NETA") = (Format(Math.Round((v_areasup_rese), 4), "###,###.0000")).ToString()
                                        dRow.Item("PORCEN") = (Format(Math.Round((v_areasup_rese / v_areaini_depa) * 100, 2), "###,###.00")).ToString
                                        v_porcen = (Format(Math.Round((v_areasup_rese / v_areaini_depa) * 100, 2), "###,###.00")).ToString
                                        lodtTabla.Rows.Add(dRow)


                                        'Dim lostrRetorno_cuenta As String

                                        'If lostrRetorno_cuenta > 0 Then
                                        '    cls_Oracle.FT_SG_D_REGION_X_AR("DEL", "", "", v_codigo_depa, "", "", "", "")
                                        'End If

                                        cls_Oracle.FT_SG_D_REGION_X_AR("INS", v_tipo_rese, sele_elemento, v_codigo_depa, v_areaini_depa.ToString, v_areasup_rese.ToString, v_porcen.ToString, gstrUsuarioAcceso, "")


                                        sw.WriteLine(v_nm_depa1)
                                        sw.WriteLine(sele_elemento)
                                        sw.WriteLine((Format(Math.Round(v_areaini_depa, 4), "###,###.0000")).ToString)
                                        sw.WriteLine(v_cantidad)
                                        sw.WriteLine((Format(Math.Round((v_areasup_rese), 4), "###,###.0000")).ToString())
                                        sw.WriteLine((Format(Math.Round((v_areasup_rese / v_areaini_depa) * 100, 2), "###,###.00")).ToString)


                                    Next contador1
                                End If
                            End If

                        Next elemento
                    Catch ex As Exception
                    End Try
                End If
                pFeature = pFeatureCursor.NextFeature

            Loop
            sw.Close()
            '   Me.btnExcel.Enabled = False

            Me.btnExcel.Enabled = True



            'Dim v_tipoar As String
            'cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "2", False)
            'pFeatureClass = pFeatureLayer_depa.FeatureClass
            'v_nm_depa = cbodetalle.SelectedItem
            'v_Zona = cboZona.SelectedItem
            'v_tipoar = cbodetalle.SelectedItem
            'pFeatureClass = pFeatureLayer_depa.FeatureClass
            ''Dim pqueryfilter As IQueryFilter
            ''pqueryfilter = New QueryFilter

            ''pqueryfilter.WhereClause = "NM_DEPA = '" & v_nm_depa & "'"
            'pFeatureCursor = pFeatureClass.Search(Nothing, False)
            'pFeature = pFeatureCursor.NextFeature
            'Do Until pFeature Is Nothing
            '    v_codigo = pFeature.Value(pFeatureCursor.FindField("CD_DEPA"))
            '    v_nm_depa1 = pFeature.Value(pFeatureCursor.FindField("NM_DEPA"))
            '    If v_codigo = "99" Then  'SOLO PARA MAR Y FRONTERA
            '        If v_nm_depa1 = "MAR" Or v_nm_depa1 = "FUERA DEL PERU" Then
            '            'Select Case v_Zona
            '            '   Case "18"
            '            lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(2, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_depa1, v_tipoar)
            '            '    Case "17"
            '            'lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(2, "GPO_DEP_DEPARTAMENTO_17", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_depa1, v_tipoar)
            '            '    Case "19"
            '            'lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(2, "GPO_DEP_DEPARTAMENTO_19", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_depa1, v_tipoar)

            '            'End Select

            '            If lodbtExiste_SupAR.Rows.Count = 0 Then
            '                sw.WriteLine(v_nm_depa1)
            '                sw.WriteLine(v_codigo)
            '                sw.WriteLine(0)
            '                sw.WriteLine(0)
            '                sw.WriteLine(0)
            '                sw.WriteLine(0)
            '            Else
            '                For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
            '                    v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
            '                    v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
            '                    v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
            '                    v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
            '                Next contador1

            '                dRow = lodtTabla.NewRow
            '                dRow.Item("CODIGO") = v_codigo
            '                dRow.Item("NOMBRE") = v_nm_depa1
            '                dRow.Item("AREA") = v_areaini_depa
            '                dRow.Item("CANTIDAD") = v_cantidad
            '                dRow.Item("AREA_SUP") = v_areasup_rese
            '                dRow.Item("PORCEN") = (Format(Math.Round(((v_areasup_rese) / (v_areaini_depa)) * 100, 2), "###,###.00")).ToString

            '                lodtTabla.Rows.Add(dRow)
            '                sw.WriteLine(v_nm_depa1)
            '                sw.WriteLine(v_codigo)
            '                sw.WriteLine(v_areaini_depa)
            '                sw.WriteLine(v_cantidad)
            '                sw.WriteLine(v_areasup_rese)
            '                sw.WriteLine((Format(Math.Round(((v_areasup_rese) / (v_areaini_depa)) * 100, 2), "###,###.00")).ToString)

            '            End If
            '        End If

            '    ElseIf v_codigo <> "99" Then  'NORMAL

            '        ' gloZona = pFeature.Value(pFeatureCursor.FindField("ZONA"))

            '        Try
            '            ' lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(53, gstrFC_Departamento, "CQUI0543.GPO_CARAM_18", v_codigo)

            '            'Select Case v_Zona
            '            '  Case "18"
            '            lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(1, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_codigo, v_tipoar)
            '            ' Case "17"
            '            '    lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(1, "GPO_DEP_DEPARTAMENTO_17", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_codigo, v_tipoar)
            '            ' Case "19"
            '            '    lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(1, "GPO_DEP_DEPARTAMENTO_19", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_codigo, v_tipoar)

            '            'End Select

            '            If lodbtExiste_SupAR.Rows.Count = 0 Then
            '                sw.WriteLine(v_nm_depa1)
            '                sw.WriteLine(v_codigo)
            '                sw.WriteLine(0)
            '                sw.WriteLine(0)
            '                sw.WriteLine(0)
            '                sw.WriteLine(0)
            '            Else
            '                If v_codigo = "04" Or v_codigo = "12" Or v_codigo = "21" Or v_codigo = "05" Or v_codigo = "08" Then  '
            '                    'CONSULTA LOS DPTO QUE TIENEN LAGUNA
            '                    For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
            '                        v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
            '                        v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
            '                        v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
            '                        v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
            '                    Next contador1

            '                    If v_codigo = "05" Then
            '                        v_nm_laguna = "LAGUNA DE PARINACOCHAS"
            '                    ElseIf v_codigo = "04" Then
            '                        v_nm_laguna = "LAGUNA SALINAS"
            '                    ElseIf v_codigo = "08" Then
            '                        v_nm_laguna = "LAGUNA LAGUI LAYO"
            '                    ElseIf v_codigo = "12" Then
            '                        v_nm_laguna = "LAGUNA DE JUNIN"
            '                    ElseIf v_codigo = "21" Then
            '                        v_nm_laguna = "LAGUNAS DE PUNO"
            '                    End If
            '                    If v_nm_laguna = "LAGUNAS DE PUNO" Then  'SOLO PARA DPTO PUNO SUMAR SUS 3 LAGOS
            '                        Dim v_areaini_depa2 As Integer = 0
            '                        Dim v_areasup_rese2 As Integer = 0

            '                        For j As Integer = 1 To 3
            '                            If j = 1 Then
            '                                v_nm_laguna = "LAGUNA UMAYO"
            '                            ElseIf j = 2 Then
            '                                v_nm_laguna = "LAGO DE ARAPA"
            '                            ElseIf j = 3 Then
            '                                v_nm_laguna = "LAGO TITICACA"
            '                            End If
            '                            'Select Case v_Zona
            '                            'Case "18"
            '                            lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(2, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna, v_tipoar)
            '                            '    Case "17"
            '                            ' lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(2, "GPO_DEP_DEPARTAMENTO_17", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna, v_tipoar)
            '                            '    Case "19"
            '                            ' lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(2, "GPO_DEP_DEPARTAMENTO_19", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna, v_tipoar)
            '                            ' End Select

            '                            For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
            '                                v_areasup_rese1 = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
            '                                v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
            '                                v_areaini_depa1 = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")

            '                            Next contador1
            '                            v_areaini_depa2 = v_areaini_depa2 + v_areaini_depa1
            '                            v_areasup_rese2 = v_areasup_rese2 + v_areasup_rese1
            '                            'Sumando Areas

            '                        Next j

            '                        dRow = lodtTabla.NewRow
            '                        dRow.Item("CODIGO") = v_codigo
            '                        dRow.Item("NOMBRE") = v_nm_depa1
            '                        dRow.Item("AREA") = v_areaini_depa + v_areaini_depa2
            '                        dRow.Item("CANTIDAD") = v_cantidad
            '                        dRow.Item("AREA_SUP") = v_areasup_rese + v_areasup_rese2
            '                        dRow.Item("PORCEN") = (Format(Math.Round(((v_areasup_rese + v_areasup_rese2) / (v_areaini_depa + v_areaini_depa2)) * 100, 2), "###,###.00")).ToString
            '                        lodtTabla.Rows.Add(dRow)

            '                        sw.WriteLine(v_nm_depa1)
            '                        sw.WriteLine(v_codigo)
            '                        sw.WriteLine(v_areaini_depa + v_areaini_depa2)
            '                        sw.WriteLine(v_cantidad)
            '                        sw.WriteLine(v_areasup_rese + v_areasup_rese2)
            '                        sw.WriteLine((Format(Math.Round(((v_areasup_rese + v_areasup_rese2) / (v_areaini_depa + v_areaini_depa2)) * 100, 2), "###,###.00")).ToString)
            '                    Else  'DIFERENTE DE PUNO, SUMAR LAGO INDIVIDUAL

            '                        ' Select Case v_Zona
            '                        '    Case "18"
            '                        lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(2, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna, v_tipoar)
            '                        '    Case "17"
            '                        'lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(2, "GPO_DEP_DEPARTAMENTO_17", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna, v_tipoar)
            '                        '   Case "19"
            '                        'lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(2, "GPO_DEP_DEPARTAMENTO_19", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna, v_tipoar)
            '                        ' End Select

            '                        For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
            '                            v_areasup_rese1 = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
            '                            ' v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
            '                            v_areaini_depa1 = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")

            '                        Next contador1

            '                        'Sumando Areas

            '                        dRow = lodtTabla.NewRow
            '                        dRow.Item("CODIGO") = v_codigo
            '                        dRow.Item("NOMBRE") = v_nm_depa1
            '                        dRow.Item("AREA") = v_areaini_depa + v_areaini_depa1
            '                        dRow.Item("CANTIDAD") = v_cantidad
            '                        dRow.Item("AREA_SUP") = v_areasup_rese + v_areasup_rese1
            '                        dRow.Item("PORCEN") = (Format(Math.Round(((v_areasup_rese + v_areasup_rese1) / (v_areaini_depa + v_areaini_depa1)) * 100, 2), "###,###.00")).ToString
            '                        lodtTabla.Rows.Add(dRow)

            '                        sw.WriteLine(v_nm_depa1)
            '                        sw.WriteLine(v_codigo)
            '                        sw.WriteLine(v_areaini_depa + v_areaini_depa1)
            '                        sw.WriteLine(v_cantidad)
            '                        sw.WriteLine(v_areasup_rese + v_areasup_rese1)
            '                        sw.WriteLine((Format(Math.Round(((v_areasup_rese + v_areasup_rese1) / (v_areaini_depa + v_areaini_depa1)) * 100, 2), "###,###.00")).ToString)

            '                    End If


            '                Else  'DPTO NORMAL QUE NO TIENEN  LAGUNA

            '                    For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
            '                        v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
            '                        v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
            '                        v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
            '                        v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
            '                        dRow = lodtTabla.NewRow
            '                        dRow.Item("CODIGO") = v_codigo_depa
            '                        dRow.Item("NOMBRE") = v_nm_depa1
            '                        dRow.Item("AREA") = v_areaini_depa
            '                        dRow.Item("CANTIDAD") = v_cantidad
            '                        dRow.Item("AREA_SUP") = v_areasup_rese
            '                        dRow.Item("PORCEN") = (Format(Math.Round((v_areasup_rese / v_areaini_depa) * 100, 2), "###,###.00")).ToString
            '                        lodtTabla.Rows.Add(dRow)

            '                        sw.WriteLine(v_nm_depa1)
            '                        sw.WriteLine(v_codigo_depa)
            '                        sw.WriteLine(v_areaini_depa)
            '                        sw.WriteLine(v_cantidad)
            '                        sw.WriteLine(v_areasup_rese)
            '                        sw.WriteLine((Format(Math.Round((v_areasup_rese / v_areaini_depa) * 100, 2), "###,###.00")).ToString)


            '                    Next contador1
            '                End If
            '            End If
            '        Catch ex As Exception

            '        End Try
            '    End If
            '    pFeature = pFeatureCursor.NextFeature

            'Loop
            'sw.Close()
            'Me.btnExcel.Enabled = True


            ' ´--------------------------




















        ElseIf tipo_selec_catnomin = "TIPO DE RESERVA" Then
            ' v_nm_depa = cbodetalle.SelectedItem
            '  v_Zona = cboZona.SelectedValue
            Dim v_tipoar As String
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "2", False)
            pFeatureClass = pFeatureLayer_depa.FeatureClass
            v_nm_depa = cbodetalle.SelectedItem
            v_Zona = cboZona.SelectedItem
            v_tipoar = cbodetalle.SelectedItem
            pFeatureClass = pFeatureLayer_depa.FeatureClass
            'Dim pqueryfilter As IQueryFilter
            'pqueryfilter = New QueryFilter

            'pqueryfilter.WhereClause = "NM_DEPA = '" & v_nm_depa & "'"
            pFeatureCursor = pFeatureClass.Search(Nothing, False)
            pFeature = pFeatureCursor.NextFeature
            Do Until pFeature Is Nothing
                v_codigo = pFeature.Value(pFeatureCursor.FindField("CD_DEPA"))
                v_nm_depa1 = pFeature.Value(pFeatureCursor.FindField("NM_DEPA"))
                If v_codigo = "99" Then  'SOLO PARA MAR Y FRONTERA
                    If v_nm_depa1 = "MAR" Or v_nm_depa1 = "FUERA DEL PERU" Then
                        'Select Case v_Zona
                        '   Case "18"
                        lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(2, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_depa1, v_tipoar)
                        '    Case "17"
                        'lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(2, "GPO_DEP_DEPARTAMENTO_17", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_depa1, v_tipoar)
                        '    Case "19"
                        'lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(2, "GPO_DEP_DEPARTAMENTO_19", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_depa1, v_tipoar)

                        'End Select

                        If lodbtExiste_SupAR.Rows.Count = 0 Then
                            sw.WriteLine(v_nm_depa1)
                            sw.WriteLine(v_codigo)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                        Else
                            For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
                                v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
                            Next contador1

                            dRow = lodtTabla.NewRow
                            dRow.Item("CODIGO") = v_codigo
                            dRow.Item("NOMBRE") = v_nm_depa1
                            dRow.Item("AREA") = v_areaini_depa
                            dRow.Item("CANTIDAD") = v_cantidad
                            dRow.Item("AREA_SUP") = v_areasup_rese
                            dRow.Item("PORCEN") = (Format(Math.Round(((v_areasup_rese) / (v_areaini_depa)) * 100, 2), "###,###.00")).ToString

                            lodtTabla.Rows.Add(dRow)
                            sw.WriteLine(v_nm_depa1)
                            sw.WriteLine(v_codigo)
                            sw.WriteLine(v_areaini_depa)
                            sw.WriteLine(v_cantidad)
                            sw.WriteLine(v_areasup_rese)
                            sw.WriteLine((Format(Math.Round(((v_areasup_rese) / (v_areaini_depa)) * 100, 2), "###,###.00")).ToString)

                        End If
                    End If

                ElseIf v_codigo <> "99" Then  'NORMAL

                    ' gloZona = pFeature.Value(pFeatureCursor.FindField("ZONA"))

                    Try
                        ' lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(53, gstrFC_Departamento, "CQUI0543.GPO_CARAM_18", v_codigo)

                        'Select Case v_Zona
                        '  Case "18"
                        lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(1, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_codigo, v_tipoar)
                        ' Case "17"
                        '    lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(1, "GPO_DEP_DEPARTAMENTO_17", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_codigo, v_tipoar)
                        ' Case "19"
                        '    lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(1, "GPO_DEP_DEPARTAMENTO_19", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_codigo, v_tipoar)

                        'End Select

                        If lodbtExiste_SupAR.Rows.Count = 0 Then
                            sw.WriteLine(v_nm_depa1)
                            sw.WriteLine(v_codigo)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                        Else
                            If v_codigo = "04" Or v_codigo = "12" Or v_codigo = "21" Or v_codigo = "05" Or v_codigo = "08" Then  '
                                'CONSULTA LOS DPTO QUE TIENEN LAGUNA
                                For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                    v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                    v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                    v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
                                    v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
                                Next contador1

                                If v_codigo = "05" Then
                                    v_nm_laguna = "LAGUNA DE PARINACOCHAS"
                                ElseIf v_codigo = "04" Then
                                    v_nm_laguna = "LAGUNA SALINAS"
                                ElseIf v_codigo = "08" Then
                                    v_nm_laguna = "LAGUNA LAGUI LAYO"
                                ElseIf v_codigo = "12" Then
                                    v_nm_laguna = "LAGUNA DE JUNIN"
                                ElseIf v_codigo = "21" Then
                                    v_nm_laguna = "LAGUNAS DE PUNO"
                                End If
                                If v_nm_laguna = "LAGUNAS DE PUNO" Then  'SOLO PARA DPTO PUNO SUMAR SUS 3 LAGOS
                                    Dim v_areaini_depa2 As Integer = 0
                                    Dim v_areasup_rese2 As Integer = 0

                                    For j As Integer = 1 To 3
                                        If j = 1 Then
                                            v_nm_laguna = "LAGUNA UMAYO"
                                        ElseIf j = 2 Then
                                            v_nm_laguna = "LAGO DE ARAPA"
                                        ElseIf j = 3 Then
                                            v_nm_laguna = "LAGO TITICACA"
                                        End If
                                        'Select Case v_Zona
                                        'Case "18"
                                        lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(2, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna, v_tipoar)
                                        '    Case "17"
                                        ' lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(2, "GPO_DEP_DEPARTAMENTO_17", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna, v_tipoar)
                                        '    Case "19"
                                        ' lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(2, "GPO_DEP_DEPARTAMENTO_19", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna, v_tipoar)
                                        ' End Select

                                        For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                            v_areasup_rese1 = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                            v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                            v_areaini_depa1 = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")

                                        Next contador1
                                        v_areaini_depa2 = v_areaini_depa2 + v_areaini_depa1
                                        v_areasup_rese2 = v_areasup_rese2 + v_areasup_rese1
                                        'Sumando Areas

                                    Next j

                                    dRow = lodtTabla.NewRow
                                    dRow.Item("CODIGO") = v_codigo
                                    dRow.Item("NOMBRE") = v_nm_depa1
                                    dRow.Item("AREA") = v_areaini_depa + v_areaini_depa2
                                    dRow.Item("CANTIDAD") = v_cantidad
                                    dRow.Item("AREA_SUP") = v_areasup_rese + v_areasup_rese2
                                    dRow.Item("PORCEN") = (Format(Math.Round(((v_areasup_rese + v_areasup_rese2) / (v_areaini_depa + v_areaini_depa2)) * 100, 2), "###,###.00")).ToString
                                    lodtTabla.Rows.Add(dRow)

                                    sw.WriteLine(v_nm_depa1)
                                    sw.WriteLine(v_codigo)
                                    sw.WriteLine(v_areaini_depa + v_areaini_depa2)
                                    sw.WriteLine(v_cantidad)
                                    sw.WriteLine(v_areasup_rese + v_areasup_rese2)
                                    sw.WriteLine((Format(Math.Round(((v_areasup_rese + v_areasup_rese2) / (v_areaini_depa + v_areaini_depa2)) * 100, 2), "###,###.00")).ToString)
                                Else  'DIFERENTE DE PUNO, SUMAR LAGO INDIVIDUAL

                                    ' Select Case v_Zona
                                    '    Case "18"
                                    lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(2, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna, v_tipoar)
                                    '    Case "17"
                                    'lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(2, "GPO_DEP_DEPARTAMENTO_17", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna, v_tipoar)
                                    '   Case "19"
                                    'lodbtExiste_SupAR = cls_Oracle.FT_Int_tiporesexdepa(2, "GPO_DEP_DEPARTAMENTO_19", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna, v_tipoar)
                                    ' End Select

                                    For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                        v_areasup_rese1 = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                        ' v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                        v_areaini_depa1 = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")

                                    Next contador1

                                    'Sumando Areas

                                    dRow = lodtTabla.NewRow
                                    dRow.Item("CODIGO") = v_codigo
                                    dRow.Item("NOMBRE") = v_nm_depa1
                                    dRow.Item("AREA") = v_areaini_depa + v_areaini_depa1
                                    dRow.Item("CANTIDAD") = v_cantidad
                                    dRow.Item("AREA_SUP") = v_areasup_rese + v_areasup_rese1
                                    dRow.Item("PORCEN") = (Format(Math.Round(((v_areasup_rese + v_areasup_rese1) / (v_areaini_depa + v_areaini_depa1)) * 100, 2), "###,###.00")).ToString
                                    lodtTabla.Rows.Add(dRow)

                                    sw.WriteLine(v_nm_depa1)
                                    sw.WriteLine(v_codigo)
                                    sw.WriteLine(v_areaini_depa + v_areaini_depa1)
                                    sw.WriteLine(v_cantidad)
                                    sw.WriteLine(v_areasup_rese + v_areasup_rese1)
                                    sw.WriteLine((Format(Math.Round(((v_areasup_rese + v_areasup_rese1) / (v_areaini_depa + v_areaini_depa1)) * 100, 2), "###,###.00")).ToString)

                                End If


                            Else  'DPTO NORMAL QUE NO TIENEN  LAGUNA

                                For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                    v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                    v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                    v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
                                    v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
                                    dRow = lodtTabla.NewRow
                                    dRow.Item("CODIGO") = v_codigo_depa
                                    dRow.Item("NOMBRE") = v_nm_depa1
                                    dRow.Item("AREA") = v_areaini_depa
                                    dRow.Item("CANTIDAD") = v_cantidad
                                    dRow.Item("AREA_SUP") = v_areasup_rese
                                    dRow.Item("PORCEN") = (Format(Math.Round((v_areasup_rese / v_areaini_depa) * 100, 2), "###,###.00")).ToString
                                    lodtTabla.Rows.Add(dRow)

                                    sw.WriteLine(v_nm_depa1)
                                    sw.WriteLine(v_codigo_depa)
                                    sw.WriteLine(v_areaini_depa)
                                    sw.WriteLine(v_cantidad)
                                    sw.WriteLine(v_areasup_rese)
                                    sw.WriteLine((Format(Math.Round((v_areasup_rese / v_areaini_depa) * 100, 2), "###,###.00")).ToString)


                                Next contador1
                            End If
                        End If
                    Catch ex As Exception

                    End Try
                End If
                pFeature = pFeatureCursor.NextFeature

            Loop
            sw.Close()
            Me.btnExcel.Enabled = True




        ElseIf tipo_selec_catnomin = "A NIVEL NACIONAL" Then
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "2", False)
            pFeatureClass = pFeatureLayer_depa.FeatureClass
            pFeatureCursor = pFeatureClass.Search(Nothing, False)
            pFeature = pFeatureCursor.NextFeature
            Do Until pFeature Is Nothing

                v_codigo = pFeature.Value(pFeatureCursor.FindField("CD_DEPA"))
                ' m_application.Caption = "GENERANDO ESTADISTICAS DE AREAS RESTRINGIDAS A NIVEL NACIONAL :  " & v_codigo
                v_nm_depa1 = pFeature.Value(pFeatureCursor.FindField("NM_DEPA"))
                m_application.Caption = "GENERANDO ESTADISTICAS DE AREAS RESTRINGIDAS A NIVEL NACIONAL :  " & v_nm_depa1
                'v_codigo = pFeature.Value(pFeatureCursor.FindField("CD_DEPA"))
                '-v_nm_depa1 = pFeature.Value(pFeatureCursor.FindField("NM_DEPA"))
                If v_codigo = "99" Then  'SOLO PARA MAR Y FRONTERA
                    If v_nm_depa1 = "MAR" Or v_nm_depa1 = "FUERA DEL PERU" Then

                        'Select Case v_Zona
                        'Case "18"
                        lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_depa1)
                        'Case "17"
                        '    lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, "GPO_DEP_DEPARTAMENTO_17", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_depa1)
                        'Case "19"
                        '   lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, "GPO_DEP_DEPARTAMENTO_19", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_depa1)

                        ' End Select


                        If lodbtExiste_SupAR.Rows.Count = 0 Then
                            sw.WriteLine(v_nm_depa1)
                            sw.WriteLine(v_codigo)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                        Else
                            For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
                                v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
                            Next contador1

                            dRow = lodtTabla.NewRow
                            dRow.Item("CODIGO") = v_codigo
                            dRow.Item("NOMBRE") = v_nm_depa1
                            dRow.Item("AREA") = v_areaini_depa
                            dRow.Item("CANTI") = v_cantidad
                            dRow.Item("AREA_NETA") = v_areasup_rese
                            dRow.Item("PORCEN") = (Format(Math.Round(((v_areasup_rese) / (v_areaini_depa)) * 100, 2), "###,###.00")).ToString

                            lodtTabla.Rows.Add(dRow)
                            sw.WriteLine(v_nm_depa1)
                            sw.WriteLine(v_codigo)
                            sw.WriteLine(v_areaini_depa)
                            sw.WriteLine(v_cantidad)
                            sw.WriteLine(v_areasup_rese)
                            sw.WriteLine((Format(Math.Round(((v_areasup_rese) / (v_areaini_depa)) * 100, 2), "###,###.00")).ToString)

                        End If
                    Else


                    End If
                ElseIf v_codigo <> "99" Then  'NORMAL

                    ' gloZona = pFeature.Value(pFeatureCursor.FindField("ZONA"))

                    Try
                        ' lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(53, gstrFC_Departamento, "CQUI0543.GPO_CARAM_18", v_codigo)

                        '  Select Case v_Zona
                        '   Case "18"
                        lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(53, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_codigo)
                        '   Case "17"
                        'lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(53, "GPO_DEP_DEPARTAMENTO_17", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_codigo)
                        '    Case "19"
                        ' lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(53, "GPO_DEP_DEPARTAMENTO_19", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_codigo)

                        ' End Select

                        If lodbtExiste_SupAR.Rows.Count = 0 Then
                            sw.WriteLine(v_nm_depa1)
                            sw.WriteLine(v_codigo)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                            sw.WriteLine(0)
                            sw.WriteLine(0)

                        Else
                            If v_codigo = "04" Or v_codigo = "12" Or v_codigo = "21" Or v_codigo = "05" Or v_codigo = "08" Then  '
                                'CONSULTA LOS DPTO QUE TIENEN LAGUNA
                                For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                    v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                    v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                    v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
                                    v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
                                Next contador1

                                If v_codigo = "05" Then
                                    v_nm_laguna = "LAGUNA DE PARINACOCHAS"
                                ElseIf v_codigo = "04" Then
                                    v_nm_laguna = "LAGUNA SALINAS"
                                ElseIf v_codigo = "08" Then
                                    v_nm_laguna = "LAGUNA LAGUI LAYO"
                                ElseIf v_codigo = "12" Then
                                    v_nm_laguna = "LAGUNA DE JUNIN"
                                ElseIf v_codigo = "21" Then
                                    v_nm_laguna = "LAGUNAS DE PUNO"
                                End If
                                If v_nm_laguna = "LAGUNAS DE PUNO" Then  'SOLO PARA DPTO PUNO SUMAR SUS 3 LAGOS
                                    Dim v_areaini_depa2 As Integer = 0
                                    Dim v_areasup_rese2 As Integer = 0

                                    For j As Integer = 1 To 3
                                        If j = 1 Then
                                            v_nm_laguna = "LAGUNA UMAYO"
                                        ElseIf j = 2 Then
                                            v_nm_laguna = "LAGO DE ARAPA"
                                        ElseIf j = 3 Then
                                            v_nm_laguna = "LAGO TITICACA"
                                        End If
                                        'Select Case v_Zona
                                        '   Case "18"
                                        lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna)
                                        '   Case "17"
                                        'lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, "GPO_DEP_DEPARTAMENTO_17", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna)
                                        '   Case "19"
                                        ' lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, "GPO_DEP_DEPARTAMENTO_19", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna)
                                        ' End Select

                                        For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                            v_areasup_rese1 = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                            v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                            v_areaini_depa1 = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")

                                        Next contador1
                                        v_areaini_depa2 = v_areaini_depa2 + v_areaini_depa1
                                        v_areasup_rese2 = v_areasup_rese2 + v_areasup_rese1
                                        'Sumando Areas

                                    Next j

                                    dRow = lodtTabla.NewRow
                                    dRow.Item("CODIGO") = v_codigo
                                    dRow.Item("NOMBRE") = v_nm_depa1
                                    dRow.Item("AREA") = v_areaini_depa + v_areaini_depa2
                                    dRow.Item("CANTI") = v_cantidad
                                    dRow.Item("AREA_NETA") = v_areasup_rese + v_areasup_rese2
                                    dRow.Item("PORCEN") = (Format(Math.Round(((v_areasup_rese + v_areasup_rese2) / (v_areaini_depa + v_areaini_depa2)) * 100, 2), "###,###.00")).ToString
                                    lodtTabla.Rows.Add(dRow)

                                    sw.WriteLine(v_nm_depa1)
                                    sw.WriteLine(v_codigo)
                                    sw.WriteLine(v_areaini_depa + v_areaini_depa2)
                                    sw.WriteLine(v_cantidad)
                                    sw.WriteLine(v_areasup_rese + v_areasup_rese2)
                                    sw.WriteLine((Format(Math.Round(((v_areasup_rese + v_areasup_rese2) / (v_areaini_depa + v_areaini_depa2)) * 100, 2), "###,###.00")).ToString)

                                Else  'DIFERENTE DE PUNO, SUMAR LAGO INDIVIDUAL

                                    'Select Case v_Zona
                                    '   Case "18"
                                    lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, gstrFC_Departamento_Z & v_Zona, "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna)
                                    '    Case "17"
                                    'lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, "GPO_DEP_DEPARTAMENTO_17", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna)
                                    '   Case "19"
                                    'lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(52, "GPO_DEP_DEPARTAMENTO_19", "DATA_GIS.GPO_CAR_CARAM_" & v_Zona, v_nm_laguna)
                                    'End Select

                                    For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                        v_areasup_rese1 = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                        v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                        v_areaini_depa1 = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")

                                    Next contador1

                                    'Sumando Areas

                                    dRow = lodtTabla.NewRow
                                    dRow.Item("CODIGO") = v_codigo
                                    dRow.Item("NOMBRE") = v_nm_depa1
                                    dRow.Item("AREA") = v_areaini_depa + v_areaini_depa1
                                    dRow.Item("CANTI") = v_cantidad
                                    dRow.Item("AREA_NETA") = v_areasup_rese + v_areasup_rese1
                                    dRow.Item("PORCEN") = (Format(Math.Round(((v_areasup_rese + v_areasup_rese1) / (v_areaini_depa + v_areaini_depa1)) * 100, 2), "###,###.00")).ToString
                                    lodtTabla.Rows.Add(dRow)

                                    sw.WriteLine(v_nm_depa1)
                                    sw.WriteLine(v_codigo)
                                    sw.WriteLine(v_areaini_depa + v_areaini_depa1)
                                    sw.WriteLine(v_cantidad)
                                    sw.WriteLine(v_areasup_rese + v_areasup_rese1)
                                    sw.WriteLine((Format(Math.Round(((v_areasup_rese + v_areasup_rese1) / (v_areaini_depa + v_areaini_depa1)) * 100, 2), "###,###.00")).ToString)

                                End If

                            Else  'DPTO NORMAL QUE NO TIENEN  LAGUNA

                                For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                                    v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                                    v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                                    v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
                                    v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
                                    dRow = lodtTabla.NewRow
                                    dRow.Item("CODIGO") = v_codigo_depa
                                    dRow.Item("NOMBRE") = v_nm_depa1
                                    dRow.Item("AREA") = v_areaini_depa
                                    dRow.Item("CANTI") = v_cantidad
                                    dRow.Item("AREA_NETA") = v_areasup_rese
                                    dRow.Item("PORCEN") = (Format(Math.Round((v_areasup_rese / v_areaini_depa) * 100, 2), "###,###.00")).ToString
                                    lodtTabla.Rows.Add(dRow)


                                    sw.WriteLine(v_nm_depa1)
                                    sw.WriteLine(v_codigo)
                                    sw.WriteLine(v_areaini_depa)
                                    sw.WriteLine(v_cantidad)
                                    sw.WriteLine(v_areasup_rese)
                                    sw.WriteLine((Format(Math.Round((v_areasup_rese / v_areaini_depa) * 100, 2), "###,###.00")).ToString)



                                Next contador1
                            End If
                        End If

                    Catch ex As Exception
                    End Try
                End If


                'If v_codigo <> "99" Then

                '    Try
                '        lodbtExiste_SupAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(53, gstrFC_Departamento, "CQUI0543.GPO_CARAM_18", v_codigo)
                '        If lodbtExiste_SupAR.Rows.Count = 0 Then
                '        Else
                '            For contador1 As Integer = 0 To lodbtExiste_SupAR.Rows.Count - 1
                '                v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
                '                v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
                '                v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
                '                v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
                '                dRow = lodtTabla.NewRow
                '                dRow.Item("CODIGO") = v_codigo_depa
                '                dRow.Item("NOMBRE") = "DEPA"
                '                dRow.Item("AREA") = v_areaini_depa
                '                dRow.Item("CANTIDAD") = v_cantidad
                '                dRow.Item("AREA_SUP") = v_areasup_rese
                '                dRow.Item("PORCEN") = (Format(Math.Round((v_areasup_rese / v_areaini_depa) * 100, 2), "###,###.00")).ToString
                '                lodtTabla.Rows.Add(dRow)
                '            Next contador1
                '        End If
                '    Catch ex As Exception
                '    End Try
                'End If
                pFeature = pFeatureCursor.NextFeature

            Loop
            sw.Close()
            Me.btnExcel.Enabled = True
        End If
        Me.dgdDetalle.DataSource = lodtTabla
        PT_Agregar_Funciones_EVAL() : PT_Forma_Grilla_EVAL()
        Me.chkEstado.Checked = False : Me.chkEstado.Checked = True
        For i As Integer = 0 To Me.dgdDetalle.RowCount - 1
            dgdDetalle.Item(i, "SELEC") = True
        Next
        Me.dgdDetalle.AllowUpdate = True
        dgdDetalle.Focus()

        'Agregando el xml

        Dim dt As New DataTable
        dt = dgdDetalle.DataSource
        Dim glodstControl1 As New DataSet
        glodstControl1.Tables.Add(dt.Copy)

        glodstControl1.WriteXml(glo_Path & "\arestringida.xml")



        For r As Integer = 0 To dt.Rows.Count - 1
            'Select Case dt.Rows(r).Item(0)
            ' Case "MAR", "FUERA DEL PERU"
            ' Case Else
            s1 = s1 + dt.Rows(r).Item("AREA")
            s2 = s2 + dt.Rows(r).Item("AREA_NETA")
            'End Select
        Next

        txtArea1.Text = Format(v_areaini_depa, "###,###,###,###.####")
        txtArea2.Text = Format(s2, "###,###,###,###.####")
        '  txtPorcentaje.Text = Format((s2 / s1) * 100, "###,###,###,###.##")
        txtPorcentaje.Text = Format((s2 / v_areaini_depa) * 100, "###,###,###,###.##")
        ' Me.btnExcel.Enabled = True
    End Sub

    Private Sub cboZona_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboZona.SelectedIndexChanged
        If cboZona.SelectedIndex <> 0 Then
            btncalcular.Enabled = True
            v_Zona = cboZona.SelectedItem
        Else
            btncalcular.Enabled = False
        End If
    End Sub

    Private Sub btnCargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCargar.Click
        Me.cbotipo.SelectedIndex = 1
        Me.cboZona.Items.Clear()
        Me.cboZona.Enabled = False

        ' Me.btnCargar.Enabled = False
        Dim ds As New DataSet
        Dim s1 As Decimal = 0
        Dim s2 As Decimal = 0
        Try
            If Dir(glo_Path & "\arestringida.xml", vbArchive) <> "" Then
                ds.ReadXml(glo_Path & "\arestringida.xml")
                dgdDetalle.DataSource = ds.Tables(0)
                dt = ds.Tables(0)
                'fn_Grilla(dgdDetalle, dt)
                'PT_Agregar_Funciones_Detalle_02()
                'PT_Forma_Grilla_Detalle_02()


                PT_Agregar_Funciones_EVAL() : PT_Forma_Grilla_EVAL()
                Me.chkEstado.Checked = False : Me.chkEstado.Checked = True
                For i As Integer = 0 To Me.dgdDetalle.RowCount - 1
                    dgdDetalle.Item(i, "SELEC") = True
                Next
                Me.dgdDetalle.AllowUpdate = True
                dgdDetalle.Focus()

                'cboRegion.Items.Add("TOTAL PERÚ")
                For r As Integer = 0 To dt.Rows.Count - 1
                    ' Select Case dt.Rows(r).Item(0)
                    'Case "MAR", "FUERA DEL PERU"
                    'Case Else
                    '  s1 = s1 + dt.Rows(r).Item("AREA")
                    s1 = dt.Rows(r).Item("AREA")
                    ' s2 = s2 + dt.Rows(r).Item("AREA_SUP")
                    s2 = s2 + dt.Rows(r).Item("AREA_NETA")


                    'End Select
                    ' cboRegion.Items.Add(dt.Rows(r).Item("NM_DEPA"))
                Next

                txtArea1.Text = Format(s1, "###,###,###,###.####")

                txtArea2.Text = Format(s2, "###,###,###,###.####")
                txtPorcentaje.Text = Format((s2 / s1) * 100, "###,###,###,###.##")


                'Me.chkEstado.Checked = False : Me.chkEstado.Checked = True

                'For i As Integer = 0 To Me.dgdDetalle.RowCount - 1
                '    dgdDetalle.Item(i, "SELEC") = True
                'Next
                'Me.dgdDetalle.AllowUpdate = True
                'dgdDetalle.Focus()

                ' cboRegion.SelectedIndex = 0
            Else
                MsgBox("NO existe informacion previa para cargar informacion de Areas Restringidas, Procesar...", MsgBoxStyle.Information, "Estadisticas..")
                Exit Sub


            End If

        Catch ex As Exception
        End Try
        Me.btnExcel.Enabled = True
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        ' Try
        Application.DoEvents()
        Dim RetVal
        If tipo_selec_catnomin = "A NIVEL NACIONAL" Then
            RetVal = Shell(glo_Path_EXE & "procesareportetodo.bat", 1)
        ElseIf tipo_selec_catnomin = "SEGUN DEPARTAMENTO" Then

            RetVal = Shell(glo_Path_EXE & "procesareporte.bat", 1)
        End If



        '   ExportarDatosExcel(dgdDetalle, "Reporte de Areas Restringidas ")
        ' Catch ex As Exception
        'Si el intento es fallido, mostrar MsgBox.
        'MsgBox("No se puede generar el documento Excel.", MsgBoxStyle.Critical, "Observación")
        'End Try
    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub cbotiporese_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbotiporese.SelectedIndexChanged

    End Sub
End Class