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
Imports System.Drawing

Public Class frm_Grafica_Consulta_DM
    Public pApp As IApplication
    Public pEste As Double
    Public pNorte As Double
    Private lodtbUbigeo As New DataTable
    Private cls_Oracle As New cls_Oracle
    Private cls_Catastro As New cls_DM_1
    Private Const Col_Numero = 0
    Private Const Col_Codigo = 1
    Private Const Col_Nombre = 2
    Private Const Col_Area = 3

    Private Sub frm_Grafica_Consulta_DM_DockChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DockChanged

    End Sub
    Private Sub frm_Grafica_Consulta_DM_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub frm_Grafica_Consulta_DM_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

    End Sub

    Private Sub frm_Grafica_Consulta_XY_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Prioridad_dm = ""
        colecciones_indi.Clear()
        Dim cls_Usuario As New cls_Usuario
        cls_Catastro.Pinta_Grilla_Dm(dgdDetalle)
        cls_Usuario.Activar_Layer_True_False_1(True, pApp)
        Dim lodtbDatos As New DataTable
        Dim k As Integer = 0
        'TODO: Add Tool1.OnMouseDown implementation
        Me.lblUsuario.Text = "Usuario: " & gstrNombre_Usuario 'gstrUsuarioAcceso
        Dim pMxApp As IMxApplication
        Dim capa_sele As ISelectionSet
        pMxApp = pApp
        pMxDoc = pApp.Document
        Dim pPoint As IPoint
        Dim pSpatialFilter As ISpatialFilter
        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Catastro" Then
                pLayer = pMap.Layer(A)
                aFound = True
                Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("No existe Capa de Catastro Minero", MsgBoxStyle.Information, "BDGEOCATMIN")
            DialogResult = Windows.Forms.DialogResult.Cancel
            Exit Sub
        End If
        pMap = pMxDoc.FocusMap
        If pLayer.Name = "Catastro" Then
            pFeatureLayer = pLayer
            pPoint = pMxApp.Display.DisplayTransformation.ToMapPoint(pEste, pNorte)
            pSpatialFilter = New SpatialFilter
            pSpatialFilter.Geometry = pPoint
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects ' esriSpatialRelIntersects
            Dim pFCursor As IFeatureCursor
            pFCursor = pFeatureLayer.Search(pSpatialFilter, True)
            pFeature = pFCursor.NextFeature
            If pFeature Is Nothing Then
                MsgBox("No Seleccionó ningún Derecho Minero ", MsgBoxStyle.Information, "Observacion...")
                DialogResult = Windows.Forms.DialogResult.Cancel : Exit Sub
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
            pMap.SelectByShape(pPoint, pSelectionEnv, False)
            pSelectionEnv.CombinationMethod = intSelMethod
            pMxDoc.ActiveView.Refresh()
            pFeatureSelection = pFeatureLayer
            capa_sele = pFeatureSelection.SelectionSet
            pFeatureClass = pFeatureLayer.FeatureClass
            capa_sele.Search(Nothing, True, pFCursor)
            lodtbDatos.Columns.Add("NUMERO", Type.GetType("System.String"))
            lodtbDatos.Columns.Add("CODIGO", Type.GetType("System.String"))
            lodtbDatos.Columns.Add("NOMBRE", Type.GetType("System.String"))
            lodtbDatos.Columns.Add("AREA", Type.GetType("System.Double"))
            'If capa_sele.Count > 1 Then
            pFeature = pFCursor.NextFeature
            Do Until pFeature Is Nothing
                Me.lblFound.Text = "Encontre:  " & capa_sele.Count & "  Derecho(s) Minero(s)"
                k = k + 1
                Dim dRow As DataRow
                dRow = lodtbDatos.NewRow
                dRow.Item("NUMERO") = pFeature.Value(pFeature.Fields.FindField("CONTADOR"))
                dRow.Item("CODIGO") = pFeature.Value(pFeature.Fields.FindField("CODIGOU"))
                dRow.Item("NOMBRE") = pFeature.Value(pFeature.Fields.FindField("CONCESION"))
                Prioridad_dm = pFeature.Value(pFeature.Fields.FindField("EVAL"))
                dRow.Item("AREA") = Format(Math.Round(pFeature.Value(pFeature.Fields.FindField("HECTAREA")), 4), "###,###.#000")
                lodtbDatos.Rows.Add(dRow)
                pFeature = pFCursor.NextFeature
            Loop
            Me.dgdDetalle.DataSource = lodtbDatos
            PT_Agregar_Funciones_XY() : PT_Forma_Grilla_XY()
            'If capa_sele.Count = 1 Then
            Dim losender As New System.Object
            Dim loe As New System.EventArgs
            Me.dgdDetalle_DoubleClick(losender, loe)
            Prioridad_dm = TxtPrioridad.Text
            Me.dgdDetalle.Focus()
            'End If
            pMxDoc.ActiveView.Refresh()
        Else
            MsgBox("USTED HA SELECCIONADO LA CAPA --" & pLayer.Name & "-- EL CUAL ES INCORRECTO PARA CONSULTAR DATOS GENERALES DEL DERECHO MINERO, SELECCIONE LA CAPA DE DERECHOS MINEROS", vbCritical, "OBSERVACION")
            Exit Sub
        End If
        cls_Usuario.Activar_Lista_Layer(pApp)
        ' pFeatureSelection.Clear()
    End Sub
    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        DialogResult = Windows.Forms.DialogResult.OK
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

    Private Sub dgdDetalle_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgdDetalle.AfterUpdate
        Dim v_priori As String
        Dim v_codigo As String
        v_priori = Me.dgdDetalle.Item(Me.dgdDetalle.Row, "PRIORIDAD")
        v_codigo = Me.dgdDetalle.Item(Me.dgdDetalle.Row, "CODIGO")
        Me.dgdDetalle.Item(Me.dgdDetalle.Row, "PRIORIDAD") = UCase(Me.dgdDetalle.Item(Me.dgdDetalle.Row, "PRIORIDAD")).ToString
        colecciones_indi.Add(v_codigo & v_priori)
    End Sub
    Private Sub dgdDetalle_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgdDetalle.DoubleClick
        Get_Item_DM()
    End Sub
    Private Sub dgdDetalle_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles dgdDetalle.RowColChange
        Get_Item_DM()
    End Sub
    Private Sub Get_Item_DM()
        Dim pFeature_dm As IFeature
        Try
            pFeature_dm = cls_Catastro.Seleccionar_Items_x_Codigo_1("CODIGOU = '" & dgdDetalle.Item(dgdDetalle.Row, "CODIGO") & "'", pApp)
            Me.txtContador.Text = pFeature_dm.Value(pFeature_dm.Fields.FindField("CONTADOR"))
            Me.txtCodigo.Text = pFeature.Value(pFeature.Fields.FindField("CODIGOU"))
            Me.txtNombre.Text = pFeature.Value(pFeature.Fields.FindField("CONCESION"))
            Me.txtFecha.Text = pFeature.Value(pFeature.Fields.FindField("FEC_DENU"))
            Me.txtHora.Text = pFeature.Value(pFeature.Fields.FindField("HOR_DENU"))
            Me.txtHectForm.Text = pFeature.Value(pFeature.Fields.FindField("HECTAREA"))
            Me.txtTitular.Text = pFeature.Value(pFeature.Fields.FindField("TIT_CONCES"))
            Me.txtTipo.Text = pFeature.Value(pFeature.Fields.FindField("D_ESTADO"))
            Me.TxtPrioridad.Text = pFeature.Value(pFeature.Fields.FindField("EVAL"))

            Dim lodtbDemarca As New DataTable
            lodtbDemarca = cls_Oracle.F_Obtiene_Datos_UBIGEO(Mid(pFeature.Value(pFeature.Fields.FindField("DEMAGIS")), 1, 6))
            'If lodtbDemarca.Rows.Count > 0 Then
            '    pFeature.Value(pUpdateFeatures.FindField("DPTO")) = lodtbDemarca.Rows(0).Item("DPTO")
            '    pFeature.Value(pUpdateFeatures.FindField("PROV")) = lodtbDemarca.Rows(0).Item("PROV")
            '    pFeature.Value(pUpdateFeatures.FindField("DIST")) = lodtbDemarca.Rows(0).Item("DIST")
            'End If
            'Me.txtDpto.Text = pFeature.Value(pFeature.Fields.FindField("DPTO"))
            'Me.txtProv.Text = pFeature.Value(pFeature.Fields.FindField("PROV"))
            'Me.txtDist.Text = pFeature.Value(pFeature.Fields.FindField("DIST"))
            Me.txtDpto.Text = lodtbDemarca.Rows(0).Item("DPTO")
            Me.txtProv.Text = lodtbDemarca.Rows(0).Item("PROV")
            Me.txtDist.Text = lodtbDemarca.Rows(0).Item("DIST")
            Me.dgdDetalle.Focus()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim cls_eval As New cls_DM_1
        Dim p_codigo As String
        Dim P_prioridad As String
        Me.Close()
        TxtPrioridad.Text = UCase(TxtPrioridad.Text)
        p_codigo = txtCodigo.Text
        P_prioridad = TxtPrioridad.Text
        If TxtPrioridad.Text <> Prioridad_dm Then
            If Len(TxtPrioridad.Text) < 2 Then
                MsgBox("La prioridad que ha modificado corresponde al indicador inicial de prioridad", MsgBoxStyle.Information, "Observación")
                Exit Sub
            Else
                colecciones_indi.Add(p_codigo & P_prioridad)
                'MsgBox(colecciones_indi.Count)
                cls_eval.actualiza_criterioDM(pApp, p_codigo, P_prioridad)
            End If
        Else
            MsgBox("La prioridad que ha modificado corresponde al indicador inicial de prioridad", MsgBoxStyle.Information, "Observación")
            Exit Sub
        End If
    End Sub

    Private Sub TxtPrioridad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPrioridad.Click

    End Sub

    Private Sub TxtPrioridad_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPrioridad.DoubleClick

    End Sub

    Private Sub TxtPrioridad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtPrioridad.TextChanged
        If Len(TxtPrioridad.Text) > 1 Then
            Select Case TxtPrioridad.Text
                Case "PR"
                Case "PO"
                Case "SI"
                Case "RP"
                Case "EX"
                Case "RD"
                Case "CO"
                Case Else
                    TxtPrioridad.Enabled = False
                    ' MsgBox("No es posible considerar este indicador para modifcar resultados de evaluación", MsgBoxStyle.Information, "Observación")
            End Select
        End If
    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub dgdDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgdDetalle.Click

    End Sub
End Class