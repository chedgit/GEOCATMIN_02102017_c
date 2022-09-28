Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto
Imports PORTAL_Clases
Imports System.Drawing
Imports ESRI.ArcGIS.esriSystem
'Imports ESRI.ArcGIS.Geodatabase



Structure Punto_DM
    Dim v As Integer
    Dim x As Double
    Dim y As Double
End Structure

Public Class Frm_DMxAreaRestringida
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
    Private Const Col_conta As Integer = 1
    Private Const Col_Numero As Integer = 2
    Private Const Col_Codigo As Integer = 3
    Private Const Col_Nombre As Integer = 4
    Private Const Col_priori As Integer = 5
    Private Const Col_area As Integer = 6
    Private Const Col_tipo As Integer = 7
    Private Const Col_estado As Integer = 8
    Private Const Col_tiprese As Integer = 9

    Private Sub Frm_DMxAreaRestringida_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        pMap = pMxDoc.FocusMap
        If pMap.Name <> "CATASTRO MINERO" Then
            MsgBox("No es el caso para calcular Zona Reservada Superpuesta a DM Evaluado", MsgBoxStyle.Information, "BDGEOCATMIN")
            Exit Sub
        End If
        Dim pQueryFilter As IQueryFilter = Nothing
        Dim pActiveView As IActiveView
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        Dim aFound1 As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "AreaRese_super" Then
                pFeatureLayer = pMap.Layer(A)
                aFound1 = True
                Exit For
            End If
        Next A
        If Not aFound1 Then
            MsgBox("Para esta opción debe realizar lo siguiente: " & vbNewLine & "1.- Ejecutar Icono Cálculo de Área Reservada Superpuuesta...", MsgBoxStyle.Information, "[BDGEOCATMIN]")
            Exit Sub
        End If
        If pFeatureLayer.FeatureClass.FeatureCount(Nothing) = 0 Then
            MsgBox("No hay Areas Superpuestas en este tema ", MsgBoxStyle.Critical, "Observación...")
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
            lodtTabla.Columns.Add("SELEC", Type.GetType("System.String"))
            lodtTabla.Columns.Add("CONTADOR", Type.GetType("System.Double"))
            lodtTabla.Columns.Add("CODIGOU", Type.GetType("System.String"))
            lodtTabla.Columns.Add("NUM_AREA", Type.GetType("System.Double"))
            lodtTabla.Columns.Add("COD_RESE", Type.GetType("System.String"))
            lodtTabla.Columns.Add("TIP_RESE", Type.GetType("System.String"))
            lodtTabla.Columns.Add("NM_RESE", Type.GetType("System.String"))
            lodtTabla.Columns.Add("CLASE", Type.GetType("System.String"))
            lodtTabla.Columns.Add("SUPER", Type.GetType("System.String"))
            lodtTabla.Columns.Add("AREA", Type.GetType("System.Double"))
            Dim dRow As DataRow
            'Dim cuenta As Integer
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
                dRow = lodtTabla.NewRow
                dRow.Item("CONTADOR") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("FID")) + 1
                dRow.Item("CODIGOU") = v_codigo
                dRow.Item("COD_RESE") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("CODIGO"))
                dRow.Item("TIP_RESE") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("TP_RESE"))
                dRow.Item("NM_RESE") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("NM_RESE"))
                dRow.Item("CLASE") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("CLASE"))
                dRow.Item("SUPER") = "PARCIAL"
                dRow.Item("NUM_AREA") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("HAS"))
                dRow.Item("AREA") = dr
                lodtTabla.Rows.Add(dRow)
                pFeature = pFCursor.NextFeature
                lo_Fila = lo_Fila + 1
            Loop

            Me.dgdDetalle.DataSource = lodtTabla

            PT_Estilo_Grilla_EVAL(lodtTabla)
            PT_Agregar_Funciones_EVAL() : PT_Forma_Grilla_EVAL()
            Me.chkEstado.Checked = False : Me.chkEstado.Checked = True
            For i As Integer = 0 To Me.dgdDetalle.RowCount - 1
                dgdDetalle.Item(i, "SELEC") = True
            Next
            Me.dgdDetalle.AllowUpdate = True
            dgdDetalle.Focus()
        End If
    End Sub
    Private Sub PT_Estilo_Grilla_EVAL(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(Col_Sel_R).DefaultValue = ""
        ' padtbDetalle.Columns.Item(Col_conta).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Numero).DefaultValue = ""
        ' padtbDetalle.Columns.Item(Col_Codigo).DefaultValue = ""
        ' padtbDetalle.Columns.Item(Col_Nombre).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_priori).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_area).DefaultValue = 0
        padtbDetalle.Columns.Item(Col_tipo).DefaultValue = ""
        ' padtbDetalle.Columns.Item(Col_estado).DefaultValue = ""
    End Sub
    Private Sub PT_Cargar_Grilla_EVAL(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdDetalle.DataSource = dvwDetalle
        Me.dgdDetalle.Columns(Col_Sel_R).Value = True
    End Sub
    Private Sub PT_Agregar_Funciones_EVAL()
        Me.dgdDetalle.Columns(Col_Sel_R).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Numero).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Codigo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_tiprese).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Nombre).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_priori).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_area).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_tipo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_estado).DefaultValue = ""
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
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).Width = 50
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).Width = 70
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Numero).Width = 70
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Width = 100
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tiprese).Width = 100
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_priori).Width = 100
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_area).Width = 100
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tipo).Width = 100
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_estado).Width = 100
        Me.dgdDetalle.Columns("SELEC").Caption = "SELEC"
        Me.dgdDetalle.Columns("CONTADOR").Caption = "CONTADOR"
        Me.dgdDetalle.Columns("CODIGOU").Caption = "CODIGOU"
        Me.dgdDetalle.Columns("NUM_AREA").Caption = "NUM_AREA"
        Me.dgdDetalle.Columns("COD_RESE").Caption = "COD_RESE"
        Me.dgdDetalle.Columns("NM_RESE").Caption = "NM_RESE"
        Me.dgdDetalle.Columns("TIP_RESE").Caption = "TIP_RESE"
        Me.dgdDetalle.Columns("CLASE").Caption = "CLASE"
        Me.dgdDetalle.Columns("SUPER").Caption = "SUPER"
        Me.dgdDetalle.Columns("AREA").Caption = "AREA"
        Me.dgdDetalle.Splits(0).HeadingStyle.ForeColor = Color.Red

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Numero).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_area).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tipo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tipo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_estado).Locked = True

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Numero).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_priori).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_area).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tipo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_estado).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Numero).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_priori).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_area).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tipo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_estado).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
    End Sub

    Private Sub dgdDetalle_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgdDetalle.DoubleClick
        cls_Catastro.DefinitionExpression_Campo_Zoom("FID = " & dgdDetalle.Item(dgdDetalle.Row, "CONTADOR") - 1 & "", m_application, "AreaRese_super")
    End Sub

    Private Sub dgdDetalle_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles dgdDetalle.RowColChange
        Dim pFeatLayer As IFeatureLayer
        Dim b As Integer

        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "AreaRese_super" Then
                pFeatLayer = pMxDoc.FocusMap.Layer(A)
                b = A
                Exit For
            End If
        Next A
        pFeatLayer = pMxDoc.FocusMap.Layer(b)
        If pFeatLayer.Name = "AreaRese_super" Then
            loStrShapefile1 = "AreaRese_super"
            Dim lodtRegistro As New DataTable
            cls_Catastro.Seleccionar_Items_x_Codigo_areasup("FID = " & dgdDetalle.Item(dgdDetalle.Row, "CONTADOR") - 1 & "", pApp, Me.lstCoordenada, "AreaRese_super")
            Me.dgdResultado.Visible = False
          
        End If
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
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
        Dim contador As Integer = 0
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
        'Dim lostCGCODEVA As String = "", 


        If glo_InformeDM = "" Then glo_InformeDM = 1
        '******************

        Dim lostrSG_D_EVALGIS As String = ""
        lostrSG_D_EVALGIS = cls_Oracle.FT_SG_CUENTA_REG_IN_EVALGIS(v_codigo, glo_InformeDM)
        If lostrSG_D_EVALGIS = "1" Then
        Else
            lostrSG_D_EVALGIS = cls_Oracle.FT_SG_D_EVALGIS("INS", v_codigo, glo_InformeDM, gstrUsuarioAcceso)
        End If

        cuentaareasrese = cls_Oracle.FT_SG_CUENTA_REG_IN_CARACEVALGIS(v_codigo, v_IECODIGO, glo_InformeDM, lostCGCODEVA)

        If cuentaareasrese = 0 Then

            lostrRetCarac = cls_Oracle.FT_SG_D_CARAC_EVALGIS("INS", v_codigo, glo_InformeDM, lostCGCODEVA, _
             lostrZU, "", lodouHa, "", gstrUsuarioAcceso)
        Else
            lostrRetCarac = cls_Oracle.FT_SG_D_CARAC_EVALGIS("DEL", v_codigo, glo_InformeDM, lostCGCODEVA, _
             lostrZU, "", lodouHa, "", gstrUsuarioAcceso)
        End If

        lostrRetorno = cls_Oracle.FT_SG_CUENTA_REG_IN_AREAS_EVALGIS(v_codigo, "AP", glo_InformeDM)

        Dim valor2 As String = "", valor4 As String = "", valor5 As String = "", valor6 As String = "", valor7 As String = ""
        For w As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
            'lostCGCODEVA = lostCGCODEVA & w + 1 & " | " & dgdDetalle.Item(w, "COD_RESE") & " || "
            lostCGCODEVA = dgdDetalle.Item(w, "COD_RESE")
            'valor2 = valor2 & w + 1 & " | " & dgdDetalle.Item(w, "CODIGOU") & " || "
            valor2 = dgdDetalle.Item(w, "CODIGOU")
            'lodouArea = lodouArea & w + 1 & " | " & dgdDetalle.Item(w, "NUM_AREA") & " || "
            lodouArea = dgdDetalle.Item(w, "NUM_AREA")
            valor4 = valor4 & w + 1 & " | " & dgdDetalle.Item(w, "CLASE") & " || "
            valor7 = valor7 & w + 1 & " | " & dgdDetalle.Item(w, "NM_RESE") & " || "
            'lodouHa = lodouHa & w + 1 & " | " & dgdDetalle.Item(w, "AREA") & " || "
            lodouHa = dgdDetalle.Item(w, "AREA")
            valor5 = dgdDetalle.Item(w, "TIP_RESE")
            Select Case dgdDetalle.Item(w, "TIP_RESE")
                Case "PROYECTO ESPECIAL"
                    v_IECODIGO = "PY"
                    loContadorTR += 1
                    'lostrZU = lostrZU & loContadorTR & " | " & "PY" & " || "
                    lostrZU = "PY"
                Case "ZONA DE RESERVA TURISTICA"
                    v_IECODIGO = "ZT"
                    loContadorTR += 1
                    'lostrZU = lostrZU & loContadorTR & " | " & "ZT" & " || "
                    lostrZU = "ZT"
                Case "RESTOS ARQUEOLOGICOS"
                    v_IECODIGO = "PQ"
                    loContadorTR += 1
                    'lostrZU = lostrZU & loContadorTR & " | " & "PQ" & " || "
                    lostrZU = "PQ"
                Case "OTRA AREA RESTRINGIDA"
                    v_IECODIGO = "OT"
                    loContadorTR += 1
                    'lostrZU = lostrZU & loContadorTR & " | " & "OT" & " || "
                    lostrZU = "OT"
                Case "ZONA ARQUEOLOGICA"
                    v_IECODIGO = "ZA"
                    loContadorTR += 1
                    'lostrZU = lostrZU & loContadorTR & " | " & "ZA" & " || "
                    lostrZU = "ZA"
                Case "AREA NATURAL"
                    Select Case Me.dgdDetalle.Item(w, "CLASE")
                        Case "ANP"
                            v_IECODIGO = "AP"
                            loContador += 1
                            'lostrZU = lostrZU & loContador & " | " & "AP" & " || "
                            lostrZU = "AP"
                        Case "AMORTIGUAMIENTO"
                            v_IECODIGO = "AP"
                            loContador += 1
                            'lostrZU = lostrZU & loContador & " | " & "AP" & " || "
                            lostrZU = "AP"
                        Case ""
                            v_IECODIGO = "AP"
                            loContador += 1
                            'lostrZU = lostrZU & loContador & " | " & "AP" & " || "
                            lostrZU = "AP"
                    End Select
            End Select
            Select Case Me.dgdDetalle.Item(w, "SUPER")
                Case "PARCIAL"
                    loContador += 1
                    'vTipo = vTipo & loContador & " | " & "PP" & " || "
                    vTipo = "PP"
                Case "LIBRE"
                    loContador += 1
                    'vTipo = vTipo & loContador & " | " & "LI" & " || "
                    vTipo = "LI"
                Case "TOTAL"
                    loContador += 1
                    'vTipo = vTipo & loContador & " | " & "TT" & " || "
                    vTipo = "TT"

            End Select
            'Next

            l = pFeature.Shape
            pArea = pFeature.Shape   'pArea.Area
            lointArea1 = pArea.Area / 10000
            'lointArea1 = (Format(Math.Round(lointArea1, 4), "###,###.00")).ToString
            lointArea1 = (Format(Math.Round(lointArea1, 2), "###,###.00")).ToString
            ptcol = l
            lointNumVer = ptcol.PointCount - 1
            'MsgBox(lointNumVer)
            For J As Integer = 0 To ptcol.PointCount - 2
                pt = ptcol.Point(J)
                lostrCoox = lostrCoox & J + 1 & " | " & pt.X & " || "
                lostrCooy = lostrCooy & J + 1 & " | " & pt.Y & " || "
            Next J

         
           
            'pFeature = pFeatureCursor.NextFeature
            'If glo_InformeDM = "" Then glo_InformeDM = 1
            '******************

            'Dim lostrSG_D_EVALGIS As String = ""
            'lostrSG_D_EVALGIS = cls_Oracle.FT_SG_CUENTA_REG_IN_EVALGIS(v_codigo, glo_InformeDM)
            'If lostrSG_D_EVALGIS = "1" Then
            'Else
            'lostrSG_D_EVALGIS = cls_Oracle.FT_SG_D_EVALGIS("INS", v_codigo, glo_InformeDM, gstrUsuarioAcceso)
            'End If

            'cuentaareasrese = cls_Oracle.FT_SG_CUENTA_REG_IN_CARACEVALGIS(v_codigo, v_IECODIGO, glo_InformeDM, lostCGCODEVA)
            'If cuentaareasrese = 0 Then
            '
            '           lostrRetCarac = cls_Oracle.FT_SG_D_CARAC_EVALGIS("INS", v_codigo, glo_InformeDM, lostCGCODEVA, _
            '           lostrZU, "", lodouHa, "", gstrUsuarioAcceso)
            '         Else
            '        lostrRetCarac = cls_Oracle.FT_SG_D_CARAC_EVALGIS("DEL", v_codigo, glo_InformeDM, lostCGCODEVA, _
            '        lostrZU, "", lodouHa, "", gstrUsuarioAcceso)
            '      End If

            'lostrRetAreas = cls_Oracle.FT_SG_D_AREAS_EVALGIS("INS", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), lostCGCODEVA, lodouArea, vTipo, _
            'lodouHa, lostrZU, gstrUsuarioAcceso)
            If lostrRetorno = "1" Then

                If w = 0 Then
                    lostrRetorno1 = cls_Oracle.FT_SG_D_AREAS_EVALGIS("DEL", v_codigo, glo_InformeDM, v_codigo, "1", "AP", _
                    "", "", "")

                End If
                lostrRetAreas = cls_Oracle.FT_SG_D_AREAS_EVALGIS("ACT", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), 1 & "|" & lostCGCODEVA & "||", 1 & "|" & lodouArea & "||", v_IECODIGO, _
                      1 & "|" & lodouHa & "||", 1 & "|" & vTipo & "||", gstrUsuarioAcceso)



                'lostrRetorno = cls_Oracle.FT_SG_D_AREAS_EVALGIS("ACT", pastr_CGCODIGO, IIf(glo_InformeDM = "", "1", glo_InformeDM), 1 & "|" & lostCODEVA & "||", 1 & "|" & num_area & "||", "PR", _
                '1 & "|" & lodouArea & "||", "", gstrUsuarioAcceso)

                If lointNumVer <= 90 Then

                    lostrRetCoord = cls_Oracle.FT_SG_D_COORD_EVALGIS("ACT", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), lostCGCODEVA, lodouArea, v_IECODIGO, _
                               lostrCoox, lostrCooy, lointArea1, gstrUsuarioAcceso)


                    'lostrRetorno_1 = cls_Oracle.FT_SG_D_COORD_EVALGIS("INS", pastr_CGCODIGO, glo_InformeDM, pastr_CGCODEVA, _
                    'lostNumPoligono, p_IECodigo, lostrCoox, lostrCooy, lointArea, gstrUsuarioAcceso)
                    lostrCoox = ""
                    lostrCooy = ""

                Else

                    l = pFeature.Shape
                    pArea = pFeature.Shape   'pArea.Area
                    lointArea1 = pArea.Area / 10000
                    'lointArea1 = (Format(Math.Round(lointArea1, 4), "###,###.00")).ToString
                    lointArea1 = (Format(Math.Round(lointArea1, 2), "###,###.00")).ToString
                    ptcol = l
                    lostrCoox = ""
                    lostrCooy = ""

                    For k As Integer = 0 To ptcol.PointCount - 2
                        pt = ptcol.Point(k)
                        'lostrCoox = lostrCoox & k + 1 & pt.X
                        'lostrCooy = lostrCooy & k + 1 & pt.Y

                        lostrCoox = lostrCoox & k + 1 & " | " & pt.X & " || "
                        lostrCooy = lostrCooy & k + 1 & " | " & pt.Y & " || "

                        'Next k
                        lostrRetCoord = cls_Oracle.FT_SG_D_COORD_EVALGIS2("ACT", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), lostCGCODEVA, lodouArea, k + 1, v_IECODIGO, _
                                  lostrCoox, lostrCooy, lointArea1, gstrUsuarioAcceso)
                        lostrCoox = ""
                        lostrCooy = ""

                    Next k
                    ' lostrCoox = ""
                    'lostrCooy = ""

                End If


                'lostrCoox = ""
                'lostrCooy = ""
        var_fa_Coord_SuperAreaReserva = True

            Else
        lostrRetAreas = cls_Oracle.FT_SG_D_AREAS_EVALGIS("INS", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), 1 & "|" & lostCGCODEVA & "||", 1 & "|" & lodouArea & "||", v_IECODIGO, _
        1 & "|" & lodouHa & "||", 1 & "|" & vTipo & "||", gstrUsuarioAcceso)


        lostrRetCoord = cls_Oracle.FT_SG_D_COORD_EVALGIS("INS", v_codigo, glo_InformeDM, lostCGCODEVA, lodouArea, v_IECODIGO, _
                    lostrCoox, lostrCooy, lointArea1, gstrUsuarioAcceso)
        lostrCoox = ""
        lostrCooy = ""
        var_fa_Coord_SuperAreaReserva = False

            End If


        ' lostrRetorno = cls_Oracle.FT_SG_D_AREAS_EVALGIS("ACT", pastr_CGCODIGO, IIf(glo_InformeDM = "", "1", glo_InformeDM), 1 & "|" & lostCODEVA & "||", 1 & "|" & num_area & "||", "PR", _
        '      1 & "|" & lodouArea & "||", "", gstrUsuarioAcceso)
        pFeature = pFeatureCursor.NextFeature
        Next
        'MsgBox(lostrCoox)
        'MsgBox(lostrCooy)
        'lodouArea = "1"
        'lostrRetCoord = cls_Oracle.FT_SG_D_COORD_EVALGIS("INS", v_codigo, glo_InformeDM, lostCGCODEVA, lodouArea, v_IECODIGO, _
        '                lostrCoox, lostrCooy, lointArea1, gstrUsuarioAcceso)
        'lostrCoox = ""
        'lostrCooy = ""
        'var_fa_Coord_SuperAreaReserva = False


        'For v As Integer = 0 To dgdDetalle.RowCount - 1
        'clsWUrbina.InsertarCoordenadas_OP4("AreaRese_super", v_codigo, dgdDetalle.Item(v, "COD_RESE"), v_IECODIGO, m_application)
        'Next v
        'If lostrRetCarac > 0 And lostrRetAreas > 0 And lostrRetCoord > 0 Then
        If lostrRetAreas > 0 And lostrRetCoord > 0 Then
            MsgBox("La operación se realizó exitosamente.", vbExclamation, "[SIGGeocatmin]")
            'var_fa_Coord_SuperAreaReserva = True = True
            var_fa_Coord_SuperAreaReserva = True
        Else
            MsgBox("No se pudo completar la operación, Verificar sus datos: " & v_codigo, vbExclamation, "[Geocatmin]")
        End If
    End Sub

    Private Sub dgdDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgdDetalle.Click
        cls_Catastro.Zoom_to_Layer("Catastro")
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Close()
    End Sub

    Private Sub lstCoordenada_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCoordenada.SelectedIndexChanged

    End Sub
End Class