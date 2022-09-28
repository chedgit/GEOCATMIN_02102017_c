Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto
Imports PORTAL_Clases
Imports System.Drawing


Public Class frm_Grafica_Consulta_XY
    Structure Punto_DM
        Dim v As Integer
        Dim x As Double
        Dim y As Double
    End Structure

    Public pApp As IApplication
    Public pEste As Double
    Public pNorte As Double
    Private lodtbUbigeo As New DataTable
    Private cls_Oracle As New cls_Oracle

    Private Const Col_Numero = 0
    Private Const Col_Codigo = 1
    Private Const Col_Nombre = 2
    Private Const Col_Area = 3
    Private cls_Catastro As New cls_DM_1
    Private cls_DM_2 As New cls_DM_2

    Private Sub frm_Grafica_Consulta_XY_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        pMap = pMxDoc.FocusMap
        If pMap.Name <> "CATASTRO MINERO" Then
            MsgBox("No es el caso para Calcular Porcentaje de Región", MsgBoxStyle.Information, "BDGEOCATMIN")
            Exit Sub
        End If
        Dim cls_Usuario As New cls_Usuario
        'cls_Usuario.Activar_Layer_True_False_1(True, pApp)
        Dim lodtbDatos As New DataTable
        Me.lblUsuario.Text = "Usuario: " & gstrNombre_Usuario 'gstrUsuarioAcceso
        Dim pFeature As IFeature
        Dim pMxApp As IMxApplication
        Dim K As Integer = 0
        Pinta_Grilla_Dm()
        pMxApp = pApp
        pMxDoc = pApp.Document
        Dim pPoint As IPoint
        Dim pSpatialFilter As ISpatialFilter
        Dim aFound As Boolean = False
        pLayer = pMxDoc.SelectedLayer
        If caso_opcion_tools = "Por XY" Then
            'pLayer = pMxDoc.SelectedLayer
            If pLayer Is Nothing Then  ' capa no seleccionada
                MsgBox("NO HA SELECCIONADO NINGUNA CAPA PARA CONSULTAR LAS COORDENADAS", vbCritical, "BDGEOCATMIN...")
                caso_opcion_tools = ""
                DialogResult = Windows.Forms.DialogResult.Cancel
                Exit Sub
            End If
            'If pLayer.Name = "Catastro" Or pLayer.Name = "Areainter_" & v_codigo Or pLayer.Name = "Areadispo_" & v_codigo Then
            pFeatureLayer = pLayer
            'End If
        Else
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Catastro" Then 'Or pMap.Layer(A).Name = "Areadispo_" & v_codigo Or pMap.Layer(A).Name = "Areainter_" & v_codigo Then
                pLayer = pMap.Layer(A)
                pFeatureLayer = pLayer
                aFound = True
                Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("No existe Capa de Catastro Minero", MsgBoxStyle.Information, "BDGEOCATMIN")
            DialogResult = Windows.Forms.DialogResult.Cancel
            Exit Sub
        End If
        End If
        pMap = pMxDoc.FocusMap
        'If pFeatureLayer.Name = "Catastro" Or pFeatureLayer.Name = "Areainter_" & v_codigo Or pFeatureLayer.Name = "Areadispo_" & v_codigo Then
        pPoint = pMxApp.Display.DisplayTransformation.ToMapPoint(pEste, pNorte)
        pSpatialFilter = New SpatialFilter
        pSpatialFilter.Geometry = pPoint
        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects ' esriSpatialRelIntersects
        Dim pFCursor As IFeatureCursor
        pFCursor = pFeatureLayer.Search(pSpatialFilter, True)
        pFeature = pFCursor.NextFeature
        'MsgBox(pFeatureLayer.FeatureClass.FeatureCount(nothing))
        If pFeature Is Nothing Then
            caso_opcion_tools = ""
            MsgBox("No Seleccionó ningún Derecho Minero ", vbCritical, "Observacion...")
            DialogResult = Windows.Forms.DialogResult.Cancel
            Exit Sub
        End If
        Dim pgeodataset As IGeoDataset
        Dim pSpatialRef As ISpatialReference
        pgeodataset = pFeatureLayer.FeatureClass
        pSpatialRef = pgeodataset.SpatialReference
        Dim pSelectionEnv As ISelectionEnvironment
        Dim intSelMethod As Integer
        pSelectionEnv = pMxApp.SelectionEnvironment
        intSelMethod = pSelectionEnv.CombinationMethod
        pSelectionEnv.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew
        pMap.SelectByShape(pPoint, pSelectionEnv, False)  ' Para seleccionar varios poligonos interceptados
        pSelectionEnv.CombinationMethod = intSelMethod
        pMxDoc.ActiveView.Refresh()
        'pFeatureLayer = pFeatureLayer
        pFeatureSelection = pFeatureLayer
        pSelectionSet = pFeatureSelection.SelectionSet
        pFeatureClass = pFeatureLayer.FeatureClass
        pSelectionSet.Search(Nothing, True, pFCursor)
        Dim dr As DataRow
        dr = lodtbDatos.NewRow

        lodtbDatos.Columns.Add("NUMERO", Type.GetType("System.String"))
        lodtbDatos.Columns.Add("CODIGO", Type.GetType("System.String"))
        lodtbDatos.Columns.Add("NOMBRE", Type.GetType("System.String"))
        lodtbDatos.Columns.Add("AREA", Type.GetType("System.Double"))
        

        If pSelectionSet.Count > 0 Then
            pFeature = pFCursor.NextFeature
            Do Until pFeature Is Nothing
                Me.lblFound.Text = "Encontre:  " & pSelectionSet.Count & "  Derecho(s) Minero(s)"
                K = K + 1
                Dim dRow As DataRow
                dRow = lodtbDatos.NewRow
                If pFeatureLayer.Name = "Areainter_" & v_codigo Then
                    dRow.Item("NUMERO") = pFeature.Value(pFeature.Fields.FindField("FID"))
                    dRow.Item("CODIGO") = pFeature.Value(pFeature.Fields.FindField("CODIGOU_1"))
                    dRow.Item("NOMBRE") = pFeature.Value(pFeature.Fields.FindField("CONCESIO_1"))
                    dRow.Item("AREA") = Format(Math.Round(pFeature.Value(pFeature.Fields.FindField("AREA_FINAL")), 4), "###,###.#000")
                ElseIf pFeatureLayer.Name = "Areadispo_" & v_codigo Then
                    dRow.Item("CODIGO") = pFeature.Value(pFeature.Fields.FindField("CODIGOU"))
                    dRow.Item("AREA") = Format(Math.Round(pFeature.Value(pFeature.Fields.FindField("AREA_FINAL")), 4), "###,###.#000")
                    'Else
                ElseIf pFeatureLayer.Name = "Catastro" Then
                    dRow.Item("NUMERO") = pFeature.Value(pFeature.Fields.FindField("CONTADOR"))
                    dRow.Item("CODIGO") = pFeature.Value(pFeature.Fields.FindField("CODIGOU"))
                    dRow.Item("NOMBRE") = pFeature.Value(pFeature.Fields.FindField("CONCESION"))
                    dRow.Item("AREA") = Format(Math.Round(pFeature.Value(pFeature.Fields.FindField("HECTAREA")), 4), "###,###.#000")
                Else
                    dRow.Item("NUMERO") = pFeature.Value(pFeature.Fields.FindField("FID"))

                End If
                lodtbDatos.Rows.Add(dRow)
                K = K + 1
                'Listar_Vertice_Area()
                pFeature = pFCursor.NextFeature
            Loop
            Me.dgdDetalle.DataSource = lodtbDatos
            PT_Agregar_Funciones_XY() : PT_Forma_Grilla_XY()
            ' If pSelectionSet.Count = 1 Then
            Dim losender As New System.Object
            Dim loe As New System.EventArgs
            ' Me.dgdDetalle_DoubleClick(losender, loe)
            'End If
        End If
        'pFeatureSelection.Clear()
        pMxDoc.ActiveView.Refresh()

        'Else
        'MsgBox("USTED HA SELECCIONADO LA CAPA --" & pLayer.Name & "-- EL CUAL ES INCORRECTO PARA CONSULTAR DATOS GENERALES DEL DERECHO MINERO, SELECCIONE LA CAPA DE DERECHOS MINEROS", vbCritical, "OBSERVACION")
        'DialogResult = Windows.Forms.DialogResult.Cancel
        'Exit Sub
        'End If

        'cls_Catastro.guardar_Listbox_txt(dgdResultado, "c:\Listado.txt", "save") 'ejecuta procedimiento



    End Sub

   

    Private Sub PT_Agregar_Funciones_XY()
        Me.dgdDetalle.Columns(Col_Numero).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Codigo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Nombre).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Area).DefaultValue = ""
    End Sub
    Private Sub PT_Forma_Grilla_XY()
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Numero).Width = 40
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Width = 120
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Area).Width = 45
        Me.dgdDetalle.Columns("NUMERO").Caption = "Num."
        Me.dgdDetalle.Columns("CODIGO").Caption = "Código"
        Me.dgdDetalle.Columns("NOMBRE").Caption = "Nombre"
        Me.dgdDetalle.Columns("AREA").Caption = "Area"
        Me.dgdDetalle.Splits(0).HeadingStyle.ForeColor = Color.Black
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Numero).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Area).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Numero).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Area).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General

    End Sub
    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub dgdDetalle_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgdDetalle.DoubleClick
    End Sub
    Public Sub Pinta_Grilla_Dm()
        Me.dgdDetalle.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdDetalle.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        Me.dgdDetalle.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        Me.dgdDetalle.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
    End Sub
    Public Sub Pinta_Grilla(ByVal p_dgdGrilla As Object)
        p_dgdGrilla.BackColor = Color.FromArgb(242, 242, 240)
        p_dgdGrilla.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        p_dgdGrilla.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        p_dgdGrilla.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
    End Sub
    Private Sub PT_Agregar_Funciones_Resultado()
        Me.dgdResultado.Columns(0).DefaultValue = ""
        Me.dgdResultado.Columns(1).DefaultValue = ""
        Me.dgdResultado.Columns(2).DefaultValue = ""
    End Sub
    Private Sub PT_Forma_Grilla_Resultado()
        Me.dgdResultado.Splits(0).DisplayColumns(0).Width = 90
        Me.dgdResultado.Splits(0).DisplayColumns(1).Width = 85
        Me.dgdResultado.Splits(0).DisplayColumns(2).Width = 85
        Me.dgdResultado.Columns("DPTO").Caption = "Departamento"
        Me.dgdResultado.Columns("HECTA").Caption = "Hectáreas (Ha)"
        Me.dgdResultado.Columns("PORCE").Caption = "Porcentaje (%)"

        Me.dgdResultado.Splits(0).HeadingStyle.ForeColor = Color.Black
        Me.dgdResultado.Splits(0).DisplayColumns(0).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdResultado.Splits(0).DisplayColumns(1).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdResultado.Splits(0).DisplayColumns(2).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center


        Me.dgdResultado.Splits(0).DisplayColumns(0).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdResultado.Splits(0).DisplayColumns(1).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdResultado.Splits(0).DisplayColumns(2).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
    End Sub

    Private Sub dgdDetalle_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles dgdDetalle.RowColChange
        Select Case caso_opcion_tools
            Case "Por Region"
                cls_Catastro.Seleccionar_Items_x_Codigo("CODIGOU = '" & dgdDetalle.Item(dgdDetalle.Row, "CODIGO") & "'", pApp, Me.lstCoordenada)
                Me.Text = "Cálculo por Regiones"
                Me.dgdResultado.Enabled = True
                Pinta_Grilla(Me.dgdResultado)
                Me.dgdResultado.DataSource = lodtTotales
                PT_Agregar_Funciones_Resultado() : PT_Forma_Grilla_Resultado()
                cls_Catastro.Zoom_to_Layer("Catastro")
                cls_Catastro.DefinitionExpression_Campo("CODIGOU = '" & dgdDetalle.Item(dgdDetalle.Row, "CODIGO") & "'", pApp, "Catastro")
            Case "Por XY"
                If pFeatureLayer.Name = "Catastro" Or pFeatureLayer.Name = "Areadispo_" & v_codigo Then
                    cls_Catastro.Seleccionar_Items_x_Codigo("CODIGOU = '" & dgdDetalle.Item(dgdDetalle.Row, "CODIGO") & "'", pApp, Me.lstCoordenada)
                    Me.dgdResultado.Visible = False
                ElseIf pFeatureLayer.Name = "Areainter_" & v_codigo Then
                    cls_Catastro.Seleccionar_Items_x_Codigo("CODIGOU_1 = '" & dgdDetalle.Item(dgdDetalle.Row, "CODIGO") & "'" & " And FID = " & dgdDetalle.Item(dgdDetalle.Row, "NUMERO"), pApp, Me.lstCoordenada)
                    Me.dgdResultado.Visible = False
                Else
                    cls_Catastro.Seleccionar_Items_x_Codigo("FID = " & dgdDetalle.Item(dgdDetalle.Row, "NUMERO"), pApp, Me.lstCoordenada)
                    Me.dgdResultado.Visible = False
                End If
        End Select

    End Sub

    Private Sub btnReporte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReporte.Click
        Dim p As New Process()
        p.StartInfo = New ProcessStartInfo("notepad.exe", "C:\LISTADO.txt")
        p.Start()
    End Sub
End Class