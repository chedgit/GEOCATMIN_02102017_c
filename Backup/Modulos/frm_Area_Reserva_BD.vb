Imports PORTAL_Clases
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geometry
Imports System.Drawing

Public Class frm_Area_Reserva_BD
    Public m_Application As IApplication
    Private cls_Catastro As New cls_DM_1
    Private cls_DM_2 As New cls_DM_2
    Private Const Col_Poligono = 0
    Private Const Col_Codigo = 1
    Private Const Col_Tipo = 2

    Private Const Col_Num = 0
    Private Const Col_Cod = 1
    Private Const Col_Nom = 2
    Private Const Col_Cla = 3
    Private Const Col_Tip = 4
    Private Const Col_Cat = 5
    Private Const Col_Are = 6
    Private Const Col_Uso = 7
    Private lodtbLista_DM As New DataTable

    Private Sub frm_Area_Reserva_BD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim cls_Wurbina As New cls_wurbina
        Dim odt As New DataTable
        Dim loint_Rpta As Integer = 0
        Dim cls_eval As New Cls_evaluacion
        Dim capa_sele As ISelectionSet
        PT_Inicializar_Grilla_Reserva_1()
        Dim lostrGeneraAR As String = Generar_Area_Reserva_BD()
        Select Case lostrGeneraAR
            Case "OK"
                pMap = pMxDoc.FocusMap
                Dim aFound1 As Boolean = False
                cls_Catastro.Add_ShapeFile1("Zona Reservada" & fecha_archi, m_Application, "RESERVA")
                cls_eval.consultacapaDM("", "RESERVA", "Area_Reserva_" & fecha_archi)
                For A As Integer = 0 To pMap.LayerCount - 1
                    If pMap.Layer(A).Name = "Area_Reserva_" & fecha_archi Then
                        pFeatureLayer = pMap.Layer(A)
                        aFound1 = True
                        Exit For
                    End If
                Next A
                If Not aFound1 Then
                    MsgBox("No se encuentra la capa generada en la vista", MsgBoxStyle.Information, "[GEOCATMIN]")
                    Exit Sub
                End If


                pFeatureSelection = pFeatureLayer
                capa_sele = pFeatureSelection.SelectionSet
                If capa_sele.Count = 0 Then
                    MsgBox("No hay Areas en este tema ", MsgBoxStyle.Critical, "Observación...")
                    Exit Sub
                Else
                    Dim pFCursor As IFeatureCursor
                    pFCursor = pFeatureLayer.Search(Nothing, False)
                    capa_sele.Search(Nothing, True, pFCursor)
                    pFeature = pFCursor.NextFeature
                    Dim lostrCoordenada As String = ""
                    Dim lodtTabla As New DataTable
                    odt.Columns.Add("NUMERO", Type.GetType("System.Double"))
                    odt.Columns.Add("CODIGO", Type.GetType("System.String"))
                    odt.Columns.Add("NOMBRE", Type.GetType("System.String"))
                    odt.Columns.Add("CLASE", Type.GetType("System.String"))
                    odt.Columns.Add("TIPO", Type.GetType("System.String"))
                    odt.Columns.Add("CATEGORIA", Type.GetType("System.String"))
                    odt.Columns.Add("AREA", Type.GetType("System.Double"))
                    odt.Columns.Add("USO", Type.GetType("System.String"))
                    Dim dRow As DataRow
                    Dim lo_valor_Area As Integer = 0
                    Do Until pFeature Is Nothing
                        lo_valor_Area = lo_valor_Area + 1
                        dRow = odt.NewRow
                        dRow.Item("NUMERO") = lo_valor_Area
                        dRow.Item("CODIGO") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("CODIGO"))
                        dRow.Item("NOMBRE") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("NM_RESE"))
                        dRow.Item("CLASE") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("CLASE"))
                        dRow.Item("TIPO") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("TP_RESE"))
                        dRow.Item("CATEGORIA") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("CATEGORI"))
                        dRow.Item("AREA") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("HAS"))
                        dRow.Item("USO") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("USO"))
                        odt.Rows.Add(dRow)
                        pFeature = pFCursor.NextFeature
                    Loop
                End If
                pFeatureLayer.Visible = False
                dgdDetalle.DataSource = odt
                PT_Agregar_Funciones_Reserva_1() : PT_Forma_Grilla_Reserva_1()

            Case "TIPO_PE", "NO_EXISTE"
                DialogResult = Windows.Forms.DialogResult.Cancel
        End Select
    End Sub
    Private Sub FeatureClassToDatagrid()
        Dim conta As Integer
        Dim dRow As DataRow
        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "AreaReserva_100Ha" Then
                pLayer = pMap.Layer(A)
                aFound = True
                Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("No existe Capa de AreaReserva_100Ha", MsgBoxStyle.Information, "BDGEOCATMIN")
            DialogResult = Windows.Forms.DialogResult.Cancel
            Exit Sub
        End If
        pMap = pMxDoc.FocusMap
        If pLayer.Name = "AreaReserva_100Ha" Then
            pFeatureLayer = pLayer
            pFeatureClass = pFeatureLayer.FeatureClass
            pFeatureCursor = pFeatureClass.Update(Nothing, False)
            pFeature = pFeatureCursor.NextFeature
            Do Until pFeature Is Nothing
                conta = conta + 1
                dRow = lodtbLista_DM.NewRow
                dRow.Item("POLIGONO") = pFeature.Value(pFeature.Fields.FindField("POLIGONO")).ToString
                dRow.Item("CODIGOU") = pFeature.Value(pFeature.Fields.FindField("CODIGOU")).ToString
                dRow.Item("TIPO") = pFeature.Value(pFeature.Fields.FindField("TIPO")).ToString
                lodtbLista_DM.Rows.Add(dRow)
                pFeature = pFeatureCursor.NextFeature
            Loop
            Me.dgdListaAreas.DataSource = lodtbLista_DM
            PT_Agregar_Funciones_Reserva() : PT_Forma_Grilla_Reserva()
        End If
    End Sub
    Public Sub PT_Inicializar_Grilla_Reserva()
        lodtbLista_DM.Columns.Add("POLIGONO", GetType(String))
        lodtbLista_DM.Columns.Add("CODIGOU", GetType(String))
        lodtbLista_DM.Columns.Add("TIPO", GetType(String))
        PT_Estilo_Grilla_Reserva(lodtbLista_DM) : PT_Cargar_Grilla_Reserva(lodtbLista_DM)
        PT_Agregar_Funciones_Reserva() : PT_Forma_Grilla_Reserva()

    End Sub

    Private Sub PT_Estilo_Grilla_Reserva(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(Col_Codigo).DefaultValue = 0
        padtbDetalle.Columns.Item(Col_Poligono).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Tipo).DefaultValue = ""
    End Sub

    Private Sub PT_Cargar_Grilla_Reserva(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdListaAreas.DataSource = dvwDetalle
        Pinta_Grilla_Ubigeo()
    End Sub
    Private Sub PT_Agregar_Funciones_Reserva()
        Me.dgdListaAreas.Columns(Col_Codigo).DefaultValue = 0
        Me.dgdListaAreas.Columns(Col_Poligono).DefaultValue = ""
        Me.dgdListaAreas.Columns(Col_Tipo).DefaultValue = ""
    End Sub
    Public Sub Pinta_Grilla_Ubigeo()
        Me.dgdListaAreas.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdListaAreas.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        Me.dgdListaAreas.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        Me.dgdListaAreas.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
    End Sub

    Private Sub PT_Forma_Grilla_Reserva()
        Me.dgdListaAreas.Splits(0).DisplayColumns(Col_Codigo).Width = 130
        Me.dgdListaAreas.Splits(0).DisplayColumns(Col_Poligono).Width = 100
        Me.dgdListaAreas.Splits(0).DisplayColumns(Col_Tipo).Width = 200
        Me.dgdListaAreas.Columns("CODIGOU").Caption = "Código"
        Me.dgdListaAreas.Columns("POLIGONO").Caption = " Polígono"
        Me.dgdListaAreas.Columns("TIPO").Caption = "Tipo"
        Me.dgdListaAreas.Splits(0).DisplayColumns(Col_Codigo).Locked = True
        Me.dgdListaAreas.Splits(0).DisplayColumns(Col_Poligono).Locked = True
        Me.dgdListaAreas.Splits(0).DisplayColumns(Col_Tipo).Locked = True
        
        Me.dgdListaAreas.Splits(0).HeadingStyle.ForeColor = Color.Black
        Me.dgdListaAreas.Splits(0).DisplayColumns(Col_Codigo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdListaAreas.Splits(0).DisplayColumns(Col_Poligono).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdListaAreas.Splits(0).DisplayColumns(Col_Tipo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

        Me.dgdListaAreas.Splits(0).DisplayColumns(Col_Codigo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdListaAreas.Splits(0).DisplayColumns(Col_Poligono).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdListaAreas.Splits(0).DisplayColumns(Col_Tipo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
    End Sub

    Private Function Generar_Area_Reserva_BD() As String
        Dim clswurbina As New cls_wurbina
        Dim odtZonaUrbana As DataTable = Nothing
        Dim odtAreaReserva As DataTable = Nothing
        Dim lo_Step As Integer = 0
        Dim cls_Oracle As New cls_Oracle
        Dim lodtbDatos As New DataTable
        Dim lostrCodigoInterceptado As String
        Dim k As Integer = 0
        Dim loint_l As Integer = 0
        Dim contador As Integer
        If v_tipo_exp = "PE" Then
            cls_Catastro.Quitar_Layer("AreaReserva_100Ha", m_Application)
            Dim lodbtExisteAR As New DataTable
            Try
                lodbtExisteAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(2, gstrFC_Catastro_Minero & gloZona, gstrFC_AReservada & gloZona, v_codigo)
            Catch ex As Exception
                'lodbtExisteAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(2, gstrFC_Catastro_Minero & gloZona, gstrFC_AReservada & gloZona, v_codigo, gstrUsuarioAcceso, gstrUsuarioClave, gstrDatabaseGIS)
            End Try
            If lodbtExisteAR.Rows.Count >= 1 Then
                For contador = 0 To lodbtExisteAR.Rows.Count - 1
                    lostrCodigoInterceptado = lodbtExisteAR.Rows(contador).Item("CODIGO")
                    If contador = 0 Then
                        Consulta_Areas_rese = "CODIGO =  '" & lostrCodigoInterceptado & "'"
                    Else
                        Consulta_Areas_rese = Consulta_Areas_rese & " OR " & "CODIGO =  '" & lostrCodigoInterceptado & "'"
                    End If
                Next contador
                dgdDetalle.DataSource = lodbtExisteAR
                lo_Step = 100 * 10
                lodtbDatos = New DataTable
                k = 0
                For i As Integer = gloEsteMin To gloEsteMax - 1 Step lo_Step
                    Select Case k
                        Case 0
                            lodtbDatos.Columns.Add("CG_CODIGO", Type.GetType("System.String"))
                            lodtbDatos.Columns.Add("PE_NOMDER", Type.GetType("System.String"))
                            lodtbDatos.Columns.Add("CD_COREST", Type.GetType("System.Double"))
                            lodtbDatos.Columns.Add("CD_CORNOR", Type.GetType("System.Double"))
                            lodtbDatos.Columns.Add("CD_NUMVER", Type.GetType("System.Double"))
                    End Select
                    For j As Integer = gloNorteMin To gloNorteMax - 1 Step lo_Step
                        k = k + 1
                        loint_l = 1
                        Dim dRow As DataRow
                        dRow = lodtbDatos.NewRow
                        dRow.Item("CG_CODIGO") = v_codigo & "_" & k '&"Area_" & k
                        dRow.Item("PE_NOMDER") = "Area_"
                        dRow.Item("CD_COREST") = i : dRow.Item("CD_CORNOR") = j : dRow.Item("CD_NUMVER") = loint_l
                        lodtbDatos.Rows.Add(dRow)
                        dRow = lodtbDatos.NewRow
                        dRow.Item("CG_CODIGO") = v_codigo & "_" & k ' "Area_" & k
                        dRow.Item("PE_NOMDER") = "Area_"
                        dRow.Item("CD_COREST") = i + lo_Step : dRow.Item("CD_CORNOR") = j : dRow.Item("CD_NUMVER") = loint_l + 1
                        lodtbDatos.Rows.Add(dRow)
                        dRow = lodtbDatos.NewRow
                        dRow.Item("CG_CODIGO") = v_codigo & "_" & k '"Area_" & k
                        dRow.Item("PE_NOMDER") = "Area_"
                        dRow.Item("CD_COREST") = i + lo_Step : dRow.Item("CD_CORNOR") = j + lo_Step : dRow.Item("CD_NUMVER") = loint_l + 2
                        lodtbDatos.Rows.Add(dRow)
                        dRow = lodtbDatos.NewRow
                        dRow.Item("CG_CODIGO") = v_codigo & "_" & k '"Area_" & k
                        dRow.Item("PE_NOMDER") = "Area_"
                        dRow.Item("CD_COREST") = i : dRow.Item("CD_CORNOR") = j + lo_Step : dRow.Item("CD_NUMVER") = loint_l + 3
                        lodtbDatos.Rows.Add(dRow)
                    Next
                Next
                cls_Catastro.Delete_Rows_FC_GDB("AreaReserva_100Ha") '& gloZona)
                cls_Catastro.Load_FC_GDB("Catastro", "", m_Application, True)
                cls_DM_2.Genera_Poligono_100Ha(lodtbDatos, gloZona, m_Application, 3)
                cls_Catastro.Quitar_Layer("Catastro_1", m_Application)
                lodtbDatos = Nothing
                Dim lobtbReserva As New DataTable
                lobtbReserva.Columns.Add("CODIGO", Type.GetType("System.String"))
                lobtbReserva.Columns.Add("RESERVA", Type.GetType("System.String"))
                lobtbReserva.Columns.Add("TIPO", Type.GetType("System.String"))
                lobtbReserva.Columns.Add("CUADRICULA", Type.GetType("System.Double"))
                lobtbReserva.Columns.Add("AREA", Type.GetType("System.Double"))
                '---------------
                Dim dRow1 As DataRow
                lodtbdgdInterno = New DataTable
                lodtbdgdInterno.Columns.Add("ITEM", Type.GetType("System.String"))
                lodtbdgdInterno.Columns.Add("CODIGO", Type.GetType("System.String"))
                lodtbdgdInterno.Columns.Add("TIPO", Type.GetType("System.String"))
                lodtbdgdInterno.Columns.Add("CUADRICULA", Type.GetType("System.Double"))
                lodtbdgdInterno.Columns.Add("AREA", Type.GetType("System.Double"))
                For w As Integer = 0 To lodbtExisteAR.Rows.Count - 1
                    odtAreaReserva = clswurbina.CalculaInterseccion_0(1, "AreaReserva_100Ha", m_Application, gloZona, gstrFC_AReservada & gloZona, lodbtExisteAR.Rows(w).Item("CODIGO"))
                    For r As Integer = 0 To odtAreaReserva.Rows.Count - 1
                        dRow1 = lobtbReserva.NewRow
                        dRow1.Item("CODIGO") = odtAreaReserva.Rows(r).Item("CODIGO")
                        dRow1.Item("RESERVA") = odtAreaReserva.Rows(r).Item("RESERVA")
                        dRow1.Item("TIPO") = odtAreaReserva.Rows(r).Item("TIPO")
                        dRow1.Item("CUADRICULA") = odtAreaReserva.Rows(r).Item("CUADRICULA")
                        dRow1.Item("AREA") = odtAreaReserva.Rows(r).Item("AREA")
                        lobtbReserva.Rows.Add(dRow1)
                    Next
                Next
                '--------------
                cls_Catastro.Shade_Poligono("AreaReserva_100Ha", m_Application)
                dgdAreaReserva.DataSource = lobtbReserva 'odtAreaReserva
                Return "OK"

            Else
                MsgBox("No existe áreas interceptadas de Reservas en DM...", MsgBoxStyle.Information, "[BDGeocatmin]")
                'DialogResult = Windows.Forms.DialogResult.Cancel
                Return "NO_EXISTE"
                Exit Function
            End If
        Else
            MsgBox("El cálculo de cuadrículas solo es para DM tipo PE...", MsgBoxStyle.Information, "[BDGeocatmin]")
            'DialogResult = Windows.Forms.DialogResult.Cancel
            Return "TIPO_PE"
            Exit Function
        End If

    End Function

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

        Dim cls_oracle As New cls_Oracle

        Dim lostrCoox As String = "", lostrCooy As String = ""
        Dim lointNumVer As Integer = 0
        Dim lostAG_TIPO As String = ""
        Dim lointVertice As String = ""
        Dim lodouArea As String = ""
        'Dim lodouHa As String = ""
        Dim contador As Integer = 0
        Dim lostrZU1 As String = ""
        'Dim lostrZU As String = ""
        Dim lostCGCODEVA1 As String = ""
        Dim lostrNumPoligono As String = ""
        Dim loContador As Integer = 0
        Dim loContadorTR As Integer = 0
        Dim vTipo As String = "", vCGCodeva As String = ""
        Dim l As IPolygon

        Dim pt As IPoint

        Dim clsWUrbina As New cls_BDEvaluacion
        pMxDoc = m_Application.Document
        pMap = pMxDoc.FocusMap
        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1

            If pMap.Layer(A).Name = "AreaReserva_100Ha" Then
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


        Dim lostrRetCarac As String = "", lostrRetAreas As String = "", lostrRetCoord As String = "", lodouHa As String = ""
        Dim lostrRetorno As String = "", lostrCgCodigo As String = "", lostrArea As String = ""
        Dim vDescrip As String = "", vCuadricula As String = "", vArea As String = "", v_codEva As String = "", v_Clase As String = ""
        Dim lostrClase As String = "", lostrZU As String = "", lostrReserva As String = ""
        'Detalle de la Grilla
        'For w As Integer = 0 To Me.dgdDetalle.RowCount - 1
        '    lostrCgCodigo = lostrCgCodigo & (w + 1 & " | " & dgdDetalle.Item(w, "CODIGO") & " || ")
        '    lostrArea = lostrArea & (w + 1 & " | " & dgdDetalle.Item(w, "AREA") & " || ")
        '    If dgdDetalle.Item(w, "CLASE") = "ANP" Then  'Modificado porque ya no existe Nucleo
        '        lostrClase = lostrClase & w + 1 & " | " & "AN" & " || "
        '        lostrZU = lostrZU & w + 1 & " | " & "AP" & " || "
        '    ElseIf dgdDetalle.Item(w, "CLASE") = "AMORTIGUAMIENTO" Then
        '        lostrClase = lostrClase & w + 1 & " | " & "AA" & " || "
        '        lostrZU = lostrZU & w + 1 & " | " & "AP" & " || "
        '    Else
        '        lostrClase = lostrClase & w + 1 & " | " & "" & " || "
        '        lostrZU = lostrZU & w + 1 & " | " & "AP" & " || "
        '    End If
        'Next
        Dim lostrSG_D_EVALGIS As String = ""

        lostrSG_D_EVALGIS = cls_oracle.FT_SG_CUENTA_REG_IN_EVALGIS(v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM))
        If lostrSG_D_EVALGIS = "1" Then
            'For w As Integer = 0 To Me.dgdDetalle.RowCount - 1
            '    lostrZU = "AP"
            '    lostrCgCodigo = dgdDetalle.Item(w, "CODIGO")
            '    lostrRetorno = cls_oracle.FT_SG_D_CARAC_EVALGIS("DEL1", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), lostrCgCodigo, "AP", _
            '          "", "", "", "")
            '    lostrSG_D_EVALGIS = cls_oracle.FT_SG_D_EVALGIS("DEL1", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), gstrCodigo_Usuario)
            'Next

            'lostrCgCodigo = ""
            'lostrArea = ""
            'lostrClase = ""
            'lostrZU = ""

            'For w As Integer = 0 To Me.dgdDetalle.RowCount - 1
            '    lostrCgCodigo = lostrCgCodigo & (w + 1 & " | " & dgdDetalle.Item(w, "CODIGO") & " || ")
            '    lostrArea = lostrArea & (w + 1 & " | " & dgdDetalle.Item(w, "AREA") & " || ")
            '    If dgdDetalle.Item(w, "CLASE") = "ANP" Then  'Modificado porque ya no existe Nucleo
            '        lostrClase = lostrClase & w + 1 & " | " & "AN" & " || "
            '        lostrZU = lostrZU & w + 1 & " | " & "AP" & " || "
            '    ElseIf dgdDetalle.Item(w, "CLASE") = "AMORTIGUAMIENTO" Then
            '        lostrClase = lostrClase & w + 1 & " | " & "AA" & " || "
            '        lostrZU = lostrZU & w + 1 & " | " & "AP" & " || "
            '    Else
            '        lostrClase = lostrClase & w + 1 & " | " & "" & " || "
            '        lostrZU = lostrZU & w + 1 & " | " & "AP" & " || "
            '    End If
            'Next

            ' lostrSG_D_EVALGIS = cls_oracle.FT_SG_D_EVALGIS("ACT", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), gstrCodigo_Usuario)
            'lostrRetorno = cls_oracle.FT_SG_D_CARAC_EVALGIS("ACT", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), lostrCgCodigo, lostrZU, _
            ' "", lostrArea, lostrClase, gstrUsuarioAcceso)
        Else

            lostrSG_D_EVALGIS = cls_oracle.FT_SG_D_EVALGIS("INS", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), gstrCodigo_Usuario)
            var_Fa_AreaReserva = False
            lostrRetorno = cls_oracle.FT_SG_D_CARAC_EVALGIS("INS", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), lostrCgCodigo, lostrZU, _
            "", lostrArea, lostrClase, gstrUsuarioAcceso)

        End If
        Dim lostrRetorno1 As String
        cuentaareasrese = cls_oracle.FT_SG_CUENTA_REG_IN_AREAS_EVALGIS(v_codigo, "RV", IIf(glo_InformeDM = "", "1", glo_InformeDM))
        lostrRetorno1 = cls_oracle.FT_SG_D_AREAS_EVALGIS("DEL", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), v_codigo, "1", "AP", _
                   "", "", "")
        lostrReserva = "" : lostrArea = ""

        lostrZU = ""
        'Dim vTipo As String = "", vCGCodeva As String = ""
        'Dim lostrNumPoligono As String = ""
        'Dim loContador As Integer = 0
        ' Dim clsWUrbina As New cls_BDEvaluacion
        Dim v_EstadoRes As String = ""

        If cuentaareasrese = "1" Then
            'lostrCgCodigo = ""
            'lostrArea = ""
            'lostrZU = ""
            'For v As Integer = 0 To dgdDetalle.RowCount - 1
            '    lostrCgCodigo = dgdDetalle.Item(v, "CODIGO")
            '    lostrArea = dgdDetalle.Item(v, "AREA")
            '    lostrZU = "AP"
            '    lostrRetorno1 = cls_oracle.FT_SG_D_AREAS_EVALGIS("DEL", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), lostrCgCodigo, lostrArea, "AP", _
            '                   "", "", "")
            '    lostrRetorno1 = cls_oracle.FT_SG_D_CARAC_EVALGIS("DEL", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), lostrCgCodigo, "AP", _
            '   "", "", "", "")
            'Next v
        End If
        'Else

        For v As Integer = 0 To dgdDetalle.RowCount - 1
            'lostrCgCodigo = lostrCgCodigo & (v + 1 & " | " & dgdDetalle.Item(v, "CODIGO") & " || ")
            'lodouHa = lodouHa & (v + 1 & " | " & dgdDetalle.Item(v, "AREA") & " || ")
            'lostrZU = lostrZU & v + 1 & " | " & "AP" & " || "

            lostrCgCodigo = dgdDetalle.Item(v, "CODIGO")
            'lodouHa = dgdDetalle.Item(v, "AREA")
            lostrZU = "AP"


            For w As Integer = 0 To lodtbdgdInterno.Rows.Count - 1
                If lodtbdgdInterno.Rows(w).Item("CODIGO") = dgdDetalle.Item(v, "CODIGO") Then
                    'If lodtbdgdInterno.Rows(w).Item("RESERVA") = dgdDetalle.Item(w, "CODIGO") Then
                    Select Case lodtbdgdInterno.Rows(w).Item("TIPO")
                        Case "Area Parcial"
                            loContador += 1
                            'vTipo = vTipo & loContador & " | " & "PP" & " || "
                            vTipo = "PP"
                        Case "Area Libre"
                            loContador += 1
                            'vTipo = vTipo & loContador & " | " & "LI" & " || "
                            vTipo = "LI"
                        Case "Area Total"
                            loContador += 1
                            'vTipo = vTipo & loContador & " | " & "TT" & " || "
                            vTipo = "TT"
                    End Select
                    'lostrReserva = lostrReserva & loContador & " | " & lodtbdgdInterno.Rows(w).Item("CODIGO") & " || "
                    'lostrArea = lostrArea & loContador & " | " & lodtbdgdInterno.Rows(w).Item("AREA") & " || "
                    'lostrNumPoligono = lostrNumPoligono & loContador & " | " & loContador & " || "

                    lostrReserva = lodtbdgdInterno.Rows(w).Item("CODIGO")
                    'lostrArea = lodtbdgdInterno.Rows(w).Item("AREA")

                    'lostrNumPoligono = lostrNumPoligono & loContador & " | " & loContador & " || "
                    lostrNumPoligono = loContador

                    l = pFeature.Shape
                    'pArea = pFeature.Shape   'pArea.Area
                    'lointArea1 = pArea.Area / 10000
                    lodouHa = 100.0
                    'lointArea1 = (Format(Math.Round(lointArea1, 2), "###,###.00")).ToString
                    ptcol = l
                    lointNumVer = ptcol.PointCount - 1
                    'MsgBox(lointNumVer)
                    For J As Integer = 0 To ptcol.PointCount - 2
                        pt = ptcol.Point(J)
                        lostrCoox = lostrCoox & J + 1 & " | " & pt.X & " || "
                        lostrCooy = lostrCooy & J + 1 & " | " & pt.Y & " || "
                    Next J


                    lostrRetAreas = cls_oracle.FT_SG_D_AREAS_EVALGIS("INS", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), 1 & "|" & lostrReserva & "||", 1 & "|" & lostrNumPoligono & "||", lostrZU, _
                                          1 & "|" & lodouHa & "||", 1 & "|" & vTipo & "||", gstrUsuarioAcceso)




                    lostrRetCoord = cls_oracle.FT_SG_D_COORD_EVALGIS("INS", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), lostrReserva, lostrNumPoligono, lostrZU, _
                               lostrCoox, lostrCooy, lodouHa, gstrUsuarioAcceso)

                    lostrCooy = ""
                    lostrCoox = ""
                End If
            Next w

            'clsWUrbina.InsertarCoordenadas_AReserva("AreaReserva_100Ha", v_codigo, dgdDetalle.Item(v, "CODIGO"), m_Application, v_EstadoRes)
            'loContador = 0 : vTipo = "" : lostrReserva = "" : lostrArea = "" : lostrNumPoligono = ""

            ''clsWUrbina.InsertarCoordenadas_OP4("AreaReserva_100Ha", v_codigo, dgdDetalle.Item(v, "CODIGO"), lostrZU, m_Application)
            ''loContador = 0 : vTipo = "" : lostrReserva = "" : lostrArea = "" : lostrNumPoligono = ""
        Next v
        ' End If
        
        Dim lostrRetorno_0 As String = ""
        cls_Catastro.Quitar_Layer("Area_Reserva_" & fecha_archi, m_Application)
        Select Case v_EstadoRes
            Case "OK"
                MsgBox("La Información se guardó satisfactoriamente..", vbExclamation, "[Geocatmin]")
            Case "XX"
                MsgBox("No se pudo completar la operación, ya existe Código: " & v_codigo, vbExclamation, "[SIGGeocatmin]")
        End Select
    End Sub
    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim cls_demis As New cls_Demis
        cls_demis.PC_AddWMSLayer("http://www2.demis.nl/wms/wms.asp?wms=WorldMap&Demis World Map", m_Application)
        cls_demis.PC_AddWMSLayer("http://geocatmin.ingemmet.gob.pe/arcgis/services/SERV_ANOMALIA_BOUGUER/MapServer/WMSServer?", m_Application)
    End Sub

    Public Sub PT_Inicializar_Grilla_Reserva_1()
        lodtbLista_DM.Columns.Add("NUMERO", GetType(String))
        lodtbLista_DM.Columns.Add("CODIGO", GetType(String))
        lodtbLista_DM.Columns.Add("NOMBRE", GetType(String))
        lodtbLista_DM.Columns.Add("CLASE", GetType(String))
        lodtbLista_DM.Columns.Add("TIPO", GetType(String))
        lodtbLista_DM.Columns.Add("CATEGORIA", GetType(String))
        lodtbLista_DM.Columns.Add("AREA", GetType(Decimal))
        lodtbLista_DM.Columns.Add("USO", GetType(String))
        PT_Estilo_Grilla_Reserva_1(lodtbLista_DM) : PT_Cargar_Grilla_Reserva_1(lodtbLista_DM)
        PT_Agregar_Funciones_Reserva_1() : PT_Forma_Grilla_Reserva_1()
    End Sub

    Private Sub PT_Estilo_Grilla_Reserva_1(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(Col_Num).DefaultValue = 0
        padtbDetalle.Columns.Item(Col_Cod).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Nom).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Cla).DefaultValue = 0
        padtbDetalle.Columns.Item(Col_Tip).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Cat).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Are).DefaultValue = 0
        padtbDetalle.Columns.Item(Col_Uso).DefaultValue = ""

    End Sub

    Private Sub PT_Cargar_Grilla_Reserva_1(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdDetalle.DataSource = dvwDetalle
        'Me.dgdDetalle.Columns(Col_Sel_R).Value = False
        Pinta_Grilla_dgdDetalle_1()
    End Sub
    Private Sub PT_Agregar_Funciones_Reserva_1()
        Me.dgdDetalle.Columns(Col_Num).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Cod).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Nom).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Cla).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Tip).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Cat).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Are).DefaultValue = 0
        Me.dgdDetalle.Columns(Col_Uso).DefaultValue = ""

    End Sub
    Private Sub PT_Forma_Grilla_Reserva_1()
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Num).Width = 60
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cod).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nom).Width = 150
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cla).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Tip).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cat).Width = 120
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Are).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Uso).Width = 80

        Me.dgdDetalle.Columns("NUMERO").Caption = "Número"
        Me.dgdDetalle.Columns("CODIGO").Caption = " Código"
        Me.dgdDetalle.Columns("NOMBRE").Caption = "Nombre"
        Me.dgdDetalle.Columns("CLASE").Caption = "Clase"
        Me.dgdDetalle.Columns("TIPO").Caption = "Tipo"
        Me.dgdDetalle.Columns("CATEGORIA").Caption = "Categoria"
        Me.dgdDetalle.Columns("AREA").Caption = "Area"
        Me.dgdDetalle.Columns("USO").Caption = "Uso"

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Num).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cod).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nom).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cla).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Tip).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cat).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Are).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Uso).Locked = True

        Me.dgdDetalle.Splits(0).HeadingStyle.ForeColor = Color.Black
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Num).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cod).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nom).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cla).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Tip).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cat).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Are).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Uso).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Num).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cod).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nom).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cla).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Tip).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cat).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Are).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Uso).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
    End Sub
    Public Sub Pinta_Grilla_dgdDetalle_1()
        Me.dgdDetalle.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdDetalle.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        Me.dgdDetalle.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        Me.dgdDetalle.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
    End Sub
    Private Sub Generar_Area_Reserva_BD_1()
        Dim clswurbina As New cls_wurbina
        Dim odtZonaUrbana As DataTable = Nothing
        Dim odtAreaReserva As DataTable = Nothing
        Dim lo_Step As Integer = 0
        Dim cls_Oracle As New cls_Oracle
        Dim lodtbDatos As New DataTable
        Dim lostrCodigoInterceptado As String
        Dim k As Integer = 0
        Dim loint_l As Integer = 0
        'Dim lo_step As Integer
        If v_tipo_exp = "PE" Then
            cls_Catastro.Quitar_Layer("AreaReserva_100Ha", m_Application)
            'cls_Catastro.Quitar_Layer("ZonaUrbana_10Ha", m_Application)
            Dim lodbtExisteAR As New DataTable
            Try
                lodbtExisteAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(3, gstrFC_Catastro_Minero & gloZona, gstrFC_AReservada & gloZona, v_codigo)
            Catch ex As Exception
                lodbtExisteAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(2, gstrFC_Catastro_Minero & gloZona, gstrFC_AReservada & gloZona, v_codigo)
            End Try
            If lodbtExisteAR.Rows.Count >= 1 Then
                lostrCodigoInterceptado = lodbtExisteAR.Rows(0).Item("CODIGO")
                Me.dgdDetalle.DataSource = lodbtExisteAR
                lo_Step = 100 * 10
                lodtbDatos = New DataTable
                'odtZonaUrbana = clswurbina.CalculaInterseccion("ZonaUrbana_10Ha", m_Application, gloZona, gstrFC_ZUrbana & gloZona)
                k = 0
                For i As Integer = gloEsteMin To gloEsteMax - 1 Step lo_Step
                    Select Case k
                        Case 0
                            lodtbDatos.Columns.Add("CG_CODIGO", Type.GetType("System.String"))
                            lodtbDatos.Columns.Add("PE_NOMDER", Type.GetType("System.String"))
                            lodtbDatos.Columns.Add("CD_COREST", Type.GetType("System.Double"))
                            lodtbDatos.Columns.Add("CD_CORNOR", Type.GetType("System.Double"))
                            lodtbDatos.Columns.Add("CD_NUMVER", Type.GetType("System.Double"))
                    End Select
                    For j As Integer = gloNorteMin To gloNorteMax - 1 Step lo_Step
                        k = k + 1
                        loint_l = 1
                        Dim dRow As DataRow
                        dRow = lodtbDatos.NewRow
                        dRow.Item("CG_CODIGO") = v_codigo & "_" & k '&"Area_" & k
                        dRow.Item("PE_NOMDER") = "Area_"
                        dRow.Item("CD_COREST") = i : dRow.Item("CD_CORNOR") = j : dRow.Item("CD_NUMVER") = loint_l
                        lodtbDatos.Rows.Add(dRow)
                        dRow = lodtbDatos.NewRow
                        dRow.Item("CG_CODIGO") = v_codigo & "_" & k ' "Area_" & k
                        dRow.Item("PE_NOMDER") = "Area_"
                        dRow.Item("CD_COREST") = i + lo_Step : dRow.Item("CD_CORNOR") = j : dRow.Item("CD_NUMVER") = loint_l + 1
                        lodtbDatos.Rows.Add(dRow)
                        dRow = lodtbDatos.NewRow
                        dRow.Item("CG_CODIGO") = v_codigo & "_" & k '"Area_" & k
                        dRow.Item("PE_NOMDER") = "Area_"
                        dRow.Item("CD_COREST") = i + lo_Step : dRow.Item("CD_CORNOR") = j + lo_Step : dRow.Item("CD_NUMVER") = loint_l + 2
                        lodtbDatos.Rows.Add(dRow)
                        dRow = lodtbDatos.NewRow
                        dRow.Item("CG_CODIGO") = v_codigo & "_" & k '"Area_" & k
                        dRow.Item("PE_NOMDER") = "Area_"
                        dRow.Item("CD_COREST") = i : dRow.Item("CD_CORNOR") = j + lo_Step : dRow.Item("CD_NUMVER") = loint_l + 3
                        lodtbDatos.Rows.Add(dRow)
                    Next
                Next
                cls_Catastro.Delete_Rows_FC_GDB("AreaReserva_100Ha") '& gloZona)
                cls_Catastro.Load_FC_GDB("Catastro", "", m_Application, True)
                cls_DM_2.Genera_Poligono_100Ha(lodtbDatos, gloZona, m_Application, 3)
                cls_Catastro.Quitar_Layer("Catastro_1", m_Application)
                lodtbDatos = Nothing
                odtAreaReserva = clswurbina.CalculaInterseccion(4, "AreaReserva_100Ha", m_Application, gloZona, gstrFC_AReservada & gloZona, lostrCodigoInterceptado)
            Else
                MsgBox("No existe áreas interceptadas de Reservas en DM...", MsgBoxStyle.Information, "[BDGeocatmin]")
                DialogResult = Windows.Forms.DialogResult.Cancel
                Exit Sub
            End If
        Else
            MsgBox("El cálculo de cuadrículas solo es para DM tipo PE...", MsgBoxStyle.Information, "[BDGeocatmin]")
            DialogResult = Windows.Forms.DialogResult.Cancel
            Exit Sub
        End If
        Me.dgdAreaReserva.DataSource = odtAreaReserva
    End Sub

    Private Sub dgdAreaReserva_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgdAreaReserva.DoubleClick
        cls_Catastro.DefinitionExpression_Campo("TIPO = '" & dgdAreaReserva.Item(dgdAreaReserva.Row, "TIPO") & "'", m_Application, "AreaReserva_100Ha")
    End Sub

    Private Sub dgdDetalle_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgdDetalle.DoubleClick
        cls_Catastro.DefinitionExpression_Campo("CODIGO = '" & dgdDetalle.Item(dgdDetalle.Row, "CODIGO") & "'", m_Application, "Zona Reservada")
    End Sub

    Private Sub dgdAreaReserva_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgdAreaReserva.Click

    End Sub
End Class