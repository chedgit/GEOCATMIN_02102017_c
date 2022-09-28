
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto
Imports PORTAL_Clases
Imports System.Drawing
Imports ESRI.ArcGIS.esriSystem
'Imports ESRI.ArcGIS.Geodatabase



Structure Punto_DM1
    Dim v As Integer
    Dim x As Double
    Dim y As Double
End Structure

Public Class Frm_DMxAreaNatural

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
    Private Const Col_caso As Integer = 2
    Private Const Col_Numero As Integer = 3
    Private Const Col_Codigo As Integer = 4
    Private Const Col_Nombre As Integer = 5
    Private Const Col_priori As Integer = 6
    Private Const Col_area As Integer = 7
    Private Const Col_tipo As Integer = 8
    Private Const Col_estado As Integer = 9
    Private Const Col_tiprese As Integer = 10

    Private Sub Frm_DMxAreaNatural_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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
            lodtTabla.Columns.Add("CASO", Type.GetType("System.String"))
            lodtTabla.Columns.Add("CODIGOU", Type.GetType("System.String"))
            lodtTabla.Columns.Add("NUM_AREA", Type.GetType("System.Double"))
            lodtTabla.Columns.Add("COD_RESE", Type.GetType("System.String"))
            lodtTabla.Columns.Add("TIP_RESE", Type.GetType("System.String"))
            lodtTabla.Columns.Add("NM_RESE", Type.GetType("System.String"))
            lodtTabla.Columns.Add("CLASE", Type.GetType("System.String"))
            'lodtTabla.Columns.Add("SUPER", Type.GetType("System.String"))
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
                        'd0 = d0 + coordenada_DM(h).x * coordenada_DM(h + 1).y
                        'd1 = d1 + coordenada_DM(h).y * coordenada_DM(h + 1).x

                        d0 = d0 + Math.Round(coordenada_DM(h).x, 2) * Math.Round(coordenada_DM(h + 1).y, 2)
                        d1 = d1 + Math.Round(coordenada_DM(h).y, 2) * Math.Round(coordenada_DM(h + 1).x, 2)

                    Else
                        Exit For
                    End If
                Next h
                dr = Format(Math.Round(Math.Abs((d0 - d1) / 2) / 10000, 4), "###,###.0000")
                dRow = lodtTabla.NewRow
                dRow.Item("CONTADOR") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("FID")) + 1
                dRow.Item("CASO") = "COMPATIBLE"
                dRow.Item("CODIGOU") = v_codigo
                dRow.Item("COD_RESE") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("CODIGO"))
                dRow.Item("TIP_RESE") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("TP_RESE"))
                dRow.Item("NM_RESE") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("NM_RESE"))
                dRow.Item("CLASE") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("CLASE"))
                'dRow.Item("SUPER") = "PARCIAL"
                dRow.Item("NUM_AREA") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("HAS"))
                dRow.Item("AREA") = dr

                'If colecciones_anat.Contains(pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("CODIGO"))) = False Then
                colecciones_anat.Add(pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("CODIGO")))
                'End If
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
        padtbDetalle.Columns.Item(Col_caso).DefaultValue = ""
        ' padtbDetalle.Columns.Item(Col_Codigo).DefaultValue = ""
        ' padtbDetalle.Columns.Item(Col_Nombre).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_priori).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_area).DefaultValue = 0
        padtbDetalle.Columns.Item(Col_tipo).DefaultValue = ""
        ' padtbDetalle.Columns.Item(Col_caso).DefaultValue = ""
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
        Me.dgdDetalle.Columns(Col_caso).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Codigo).DefaultValue = ""
        '  Me.dgdDetalle.Columns(Col_tiprese).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Nombre).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_priori).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_area).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_tipo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_estado).DefaultValue = ""
        ' Me.dgdDetalle.Columns(Col_caso).DefaultValue = ""
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
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_caso).Width = 100
        ' Me.dgdDetalle.Splits(0).DisplayColumns(Col_Numero).Width = 70
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Width = 100
        'Me.dgdDetalle.Splits(0).DisplayColumns(Col_tiprese).Width = 100
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_priori).Width = 100
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_area).Width = 100
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tipo).Width = 100
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_estado).Width = 100
        ' Me.dgdDetalle.Splits(0).DisplayColumns(Col_caso).Width = 30
        Me.dgdDetalle.Columns("SELEC").Caption = "SELEC"
        Me.dgdDetalle.Columns("CONTADOR").Caption = "CONTADOR"
        Me.dgdDetalle.Columns("CASO").Caption = "CASO"
        Me.dgdDetalle.Columns("CODIGOU").Caption = "CODIGOU"
        Me.dgdDetalle.Columns("NUM_AREA").Caption = "NUM_AREA"
        Me.dgdDetalle.Columns("COD_RESE").Caption = "COD_RESE"
        Me.dgdDetalle.Columns("NM_RESE").Caption = "NM_RESE"
        Me.dgdDetalle.Columns("TIP_RESE").Caption = "TIP_RESE"
        Me.dgdDetalle.Columns("CLASE").Caption = "CLASE"
        Me.dgdDetalle.Columns("AREA").Caption = "AREA"
        Me.dgdDetalle.Splits(0).HeadingStyle.ForeColor = Color.Blue

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Numero).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_caso).Locked = False
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_area).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tipo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_estado).Locked = True

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_caso).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Numero).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_priori).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_area).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Justify
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tipo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_estado).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_caso).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Numero).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_priori).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_area).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tipo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_estado).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

    End Sub

    Private Sub dgdDetalle_Change(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgdDetalle.Change
      
    End Sub

    Private Sub dgdDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgdDetalle.Click
        'cls_Catastro.DefinitionExpression_Campo_Zoom("FID = " & dgdDetalle.Item(dgdDetalle.Row, "CONTADOR") - 1 & "", m_application, "AreaRese_super")
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

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
           
            p_Filtro1 = "FID = " & dgdDetalle.Item(dgdDetalle.Row, "CONTADOR") - 1
            cls_Catastro.Seleccionar_Items_x_Codigo_areasup("FID = " & dgdDetalle.Item(dgdDetalle.Row, "CONTADOR") - 1 & "", pApp, Me.lstCoordenada, "AreaRese_super")
            ' Me.dgdResultado.Visible = False

        End If
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

    Private Sub btnGrabar_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.DoubleClick
        cls_Catastro.DefinitionExpression_Campo_Zoom("FID = " & dgdDetalle.Item(dgdDetalle.Row, "CONTADOR") - 1 & "", m_application, "AreaRese_super")
    End Sub

    Private Sub btnvermapa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnvermapa.Click
        'cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".AREA_NAT_VALIDADA_" & v_zona_dm, m_application, "1", False)
        Dim cls_eval As New Cls_evaluacion
        'cls_Catastro.PT_CargarFeatureClass_SDE("GPO_ANV_AREA_NAT_VALIDA_" & v_zona_dm, m_application, "1", False)
        cls_eval.AddLayerFromFile1(m_application, "Area_Natural_" & v_zona_dm)
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click

    End Sub

    Private Sub btnagregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnagregar.Click

    End Sub
End Class