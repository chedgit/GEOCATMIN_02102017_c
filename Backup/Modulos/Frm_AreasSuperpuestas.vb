Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto
Imports PORTAL_Clases
Imports System.Drawing
Imports ESRI.ArcGIS.esriSystem


Public Class Frm_AreasSuperpuestas
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
    Private cls_Prueba As New cls_Prueba
    Private cls_DM_2 As New cls_DM_2

    Public m_Application As IApplication
    
    
    
    Private lodtbLista_DM As New DataTable


    Private Sub Frm_AreasSuperpuestas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        pMap = pMxDoc.FocusMap
        If pMap.Name <> "CATASTRO MINERO" Then
            MsgBox("No es el caso para Calcular Porcentaje de Región", MsgBoxStyle.Information, "BDGEOCATMIN")
            Exit Sub
        End If
        Pinta_Grilla_Dm()
        Dim pQueryFilter As IQueryFilter = Nothing
        Dim pActiveView As IActiveView
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        Dim aFound1 As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If Mid(pMap.Layer(A).Name, 1, 9) = "Areainter" Then
                pFeatureLayer = pMap.Layer(A)
                aFound1 = True
                Exit For
            End If
        Next A
        If Not aFound1 Then
            DialogResult = Windows.Forms.DialogResult.Cancel
            Exit Sub
        End If
        If pFeatureLayer.FeatureClass.FeatureCount(Nothing) = 0 Then
            MsgBox("No hay Areas Superpuestas en el DM.", MsgBoxStyle.Critical, "Observación...")
            Exit Sub
        Else
            Dim pFCursor As IFeatureCursor
            pFCursor = pFeatureLayer.Search(pQueryFilter, False)
            pFeature = pFCursor.NextFeature
            Dim coordenada_DM(300) As Punto_DM
            Dim h, j As Integer
            Dim ptcol As IPointCollection
            Dim pt As IPoint
            Dim l As IPolygon
            Dim lostrCoordenada As String = ""
            Dim lodtTabla As New DataTable
            lodtTabla.Columns.Add("CONTADOR", Type.GetType("System.Double"))
            lodtTabla.Columns.Add("CODIGOU", Type.GetType("System.String"))
            lodtTabla.Columns.Add("CONCESION", Type.GetType("System.String"))
            lodtTabla.Columns.Add("NUM_AREA", Type.GetType("System.Double"))
            lodtTabla.Columns.Add("AREA", Type.GetType("System.Double"))
            Dim dRow As DataRow
            Dim n As Integer
            Dim lo_Fila As Integer = 1
            Dim lo_find As Boolean = True
            Dim lo_valor_Area As Integer = 1
            Do Until pFeature Is Nothing
                l = pFeature.Shape
                ptcol = l
                ReDim coordenada_DM(ptcol.PointCount)
                For j = 0 To ptcol.PointCount - 2
                    pt = ptcol.Point(j)
                    coordenada_DM(j).v = j + 1
                    coordenada_DM(j).x = Format(Math.Round(pt.X, 3), "###,###.00") ' pt.X
                    coordenada_DM(j).y = Format(Math.Round(pt.Y, 3), "###,###.00") 'pt.Y
                Next j
                'Calcular Area
                coordenada_DM(j).x = coordenada_DM(0).x : coordenada_DM(j).y = coordenada_DM(0).y
                Dim d0, d1, dr As Double
                d0 = 0 : d1 = 0 : dr = 0
                For h = 0 To j
                    If coordenada_DM(h).x <> 0 Or coordenada_DM(h).y <> 0 Then
                        d0 = d0 + coordenada_DM(h).x * coordenada_DM(h + 1).y
                        d1 = d1 + coordenada_DM(h).y * coordenada_DM(h + 1).x
                    Else
                        Exit For
                    End If
                Next h
                dr = Format(Math.Round(Math.Abs((d0 - d1) / 2) / 10000, 5), "###,###.0000")
                For n = 0 To UBound(coordenada_DM) - 2
                    Select Case lo_find
                        Case True
                            dRow = lodtTabla.NewRow
                            If (n = 0) Then
                                dRow.Item("CONTADOR") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("FID")) + 1
                                dRow.Item("CODIGOU") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("CODIGOU_1"))
                                dRow.Item("CONCESION") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("CONCESIO_1"))
                                dRow.Item("NUM_AREA") = lo_valor_Area
                                dRow.Item("AREA") = dr
                                lodtTabla.Rows.Add(dRow)
                            End If

                        Case False
                            For a As Integer = 0 To lodtTabla.Rows.Count - 1
                                If lodtTabla.Rows(a).Item("CONCESION") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("CONCESIO_1")) Then
                                    lo_find = True
                                    lo_valor_Area = lodtTabla.Rows(a).Item("NUM_AREA") + 1
                                    Exit For
                                Else
                                    lo_find = True
                                    lo_valor_Area = 1
                                End If
                            Next
                            If (n = 0) Then
                                dRow = lodtTabla.NewRow
                                dRow.Item("CONTADOR") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("FID")) + 1
                                dRow.Item("CODIGOU") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("CODIGOU_1"))
                                dRow.Item("CONCESION") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("CONCESIO_1"))
                                dRow.Item("NUM_AREA") = lo_valor_Area
                                dRow.Item("AREA") = dr
                                lodtTabla.Rows.Add(dRow)
                            End If
                    End Select
                Next
                lo_find = False
                pFeature = pFCursor.NextFeature
                lo_Fila = lo_Fila + 1
            Loop
            Me.dgdDetalle.DataSource = lodtTabla
        End If
    End Sub
    Public Sub Pinta_Grilla_Dm()
        Me.dgdDetalle.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdDetalle.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        Me.dgdDetalle.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        Me.dgdDetalle.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
    End Sub

    Private Sub dgdDetalle_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgdDetalle.DoubleClick

    End Sub
    Private Sub dgdDetalle_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles dgdDetalle.RowColChange
        Dim pFeatLayer As IFeatureLayer
        Dim b As Integer
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Areainter_" & v_codigo Then
                pFeatLayer = pMxDoc.FocusMap.Layer(A)
                b = A
                Exit For
            End If
        Next A
        pFeatLayer = pMxDoc.FocusMap.Layer(b)
        If pFeatLayer.Name = "Areainter_" & v_codigo Then
            Dim lodtRegistro As New DataTable
            cls_Catastro.Seleccionar_Items_x_Codigo_areasup("CODIGOU_1 = '" & dgdDetalle.Item(dgdDetalle.Row, "CODIGOU") & "'" & " And FID = " & dgdDetalle.Item(dgdDetalle.Row, "CONTADOR") - 1, pApp, Me.lstCoordenada, "Areainter_" & v_codigo)
            Me.dgdResultado.Visible = False
            cls_Catastro.DefinitionExpression_areasup("CODIGOU_1 = '" & dgdDetalle.Item(dgdDetalle.Row, "CODIGOU") & "'" & " And FID = " & dgdDetalle.Item(dgdDetalle.Row, "CONTADOR") - 1, pApp, "Areainter_" & v_codigo)
            cls_Catastro.Color_Poligono_Simple(pApp, "Area_sup")
            cls_Catastro.Load_FC_GDB("gpt_Vertice_DM", "", pApp, True)
            cls_Catastro.Delete_Rows_FC_GDB("gpt_Vertice_DM")
            loStrShapefile1 = "Area_sup"
            cls_DM_2.Genera_Polygon_sup(v_codigo, v_zona_dm, pApp, Me.List_coordenadas)
            cls_Catastro.Rotular_texto_DM("gpt_Vertice_DM", v_zona_dm, pApp)
            cls_Catastro.Quitar_Layer("gpt_Vertice_DM", pApp)
            cls_Catastro.Genera_Imagen_DM("VistaPrevia")
            cls_Catastro.Quitar_Layer("Area_sup", pApp)
            Me.img_DM.ImageLocation = glo_pathTMP & "\VistaPrevia.jpg"
            cls_Catastro.Zoom_to_Layer("Catastro")
            For A As Integer = 0 To pMap.LayerCount - 1
                pFeatLayer = pMxDoc.FocusMap.Layer(A)
                pFeatLayer.Visible = True
            Next A
            pMxDoc.ActiveView.Refresh()
            Dim cls_eval As New Cls_evaluacion
            cls_eval.verprioritatios("", "PR", "", "", "", "", "", "", "", pApp)
        End If
    End Sub
    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        cls_Catastro.Zoom_to_Layer("Catastro")
        Me.Close()
    End Sub

    Private Sub btnagregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnagregar.Click
        Dim pActiveView As IActiveView
        Dim pFeatLayer As IFeatureLayer = Nothing
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        Dim afound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Areainter_" & v_codigo Then
                pFeatLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            Exit Sub
        End If
        pMxDoc.UpdateContents()
        pActiveView.Refresh()
        Dim pFeatureSelection As IFeatureSelection
        pFeatureSelection = pFeatLayer
        Dim pQueryFilter As IQueryFilter
        ' Prepare a query filter.
        pQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = p_Filtro1
        ' Refresh or erase any previous selection.
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        Dim pCmdItem As ICommandItem
        Dim pUID As New UID
        pUID.Value = "esriArcMapUI.ZoomToSelectedCommand"
        pCmdItem = pApp.Document.CommandBars.Find(pUID)
        pCmdItem.Execute()
        pMxDoc.ActiveView.Refresh()
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        'Dim lostrSG_D_AreaEvalGIS As String = ""
        'If var_fa_actareasup1 = True Then
        ' lostrSG_D_AreaEvalGIS = cls_Oracle.FT_GENERA_HISTORICO(v_codigo, glo_InformeDM)
        'End If
        'Guarda Area Interceptada de Prioritarios

        Dim clsoracle As New cls_Oracle
        Dim v_EstadoAI As String = "", v_EstadoAD As String = ""
        'Area Interceptada

        Dim lostrSG_D_EVALGIS As String = ""
        'lostrSG_D_EVALGIS = cls_Oracle.FT_SG_D_EVALGIS("INS", v_codigo, IIf(glo_InformeDM = "", "IT", glo_InformeDM), gstrCodigo_Usuario)
        lostrSG_D_EVALGIS = cls_Oracle.FT_SG_CUENTA_REG_IN_EVALGIS(v_codigo, glo_InformeDM)
        If lostrSG_D_EVALGIS = "1" Then


        Else
            lostrSG_D_EVALGIS = cls_Oracle.FT_SG_D_EVALGIS("INS", v_codigo, IIf(glo_InformeDM = "", "IT", glo_InformeDM), gstrCodigo_Usuario)
        End If

        'If var_fa_tipoactualiza = False Then
        'lostrSG_D_EVALGIS = cls_Oracle.FT_SG_D_EVALGIS("INS", v_codigo, glo_InformeDM, gstrCodigo_Usuario)

        'ElseIf var_fa_tipoactualiza = True Then
        'lostrSG_D_EVALGIS = cls_Oracle.FT_SG_D_EVALGIS("ACT", v_codigo, glo_InformeDM, gstrCodigo_Usuario)

        'End If


        'lostrSG_D_CARAC_EVALGIS = cls_Oracle.FT_SG_D_CARAC_EVALGIS("INS", v_codigo, IIf(glo_InformeDM = "", "IT", glo_InformeDM), lostrCgCodigo, lostrZU, _
        '"", lostrArea, lostrClase, gstrUsuarioAcceso, gstrUsuarioClave, gstrDatabase)
        Dim cls_BDEvaluacion_Areasup As New cls_BDEvaluacion
        cls_BDEvaluacion_Areasup.InsertarCoordenadas("Areainter_" & v_codigo, v_codigo, Me.dgdDetalle, pApp, v_EstadoAI)
        'Area Disponible
        cls_BDEvaluacion_Areasup.InsertarCoordenadas_2("Areadispo_" & v_codigo, v_codigo, Me.dgdDetalle, pApp, v_EstadoAD)


        If Val(v_EstadoAI) > 0 Or v_EstadoAD > 0 Then
            MsgBox("La operación se realizó exitosamente.", vbExclamation, "Geocatmin")
            var_fa_AreaSuper = True
        Else
            MsgBox("No se pudo completar la operación, código " & v_codigo & " ya esta Almacenado ", vbExclamation, "[SIGGeocatmin]")
        End If
    End Sub

    Private Sub dgdDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgdDetalle.Click

    End Sub
End Class