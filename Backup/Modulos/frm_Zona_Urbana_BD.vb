Imports System.Drawing
Imports PORTAL_Clases
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Framework



Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem





Public Class frm_Zona_Urbana_BD
    Public m_Application As IApplication
    Private cls_Catastro As New cls_DM_1
    Private cls_DM_2 As New cls_DM_2
    Private lodtbLista_DM As New DataTable
    Private Const Col_Num = 0
    Private Const Col_Cod = 1
    Private Const Col_Nom = 2
    Private Const Col_Cla = 3
    Private Const Col_Tip = 4
    Private Const Col_Cat = 5
    Private Const Col_Are = 6
    Private Const Col_Uso = 7

    Private Sub frm_Zona_Urbana_BD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Generar_Zona_Urbana()
    End Sub
   
    Private Sub Generar_Zona_Urbana()
        Dim lo_step As Integer
        Dim cls_Wurbina As New cls_wurbina
        Dim odt As New DataTable
        Dim lointRpta As Integer = 0
        Dim odtZonaUrbana As DataTable = Nothing
        Dim odtAreaReserva As DataTable = Nothing
        Dim ptot As Integer
        Dim lostrCodigoInterceptado As String
        Dim cls_Oracle As New cls_Oracle
        Dim lodtbDatos As New DataTable
        Dim k As Integer = 0
        Dim loint_l As Integer = 0
        If v_tipo_exp = "PE" Then
            PT_Inicializar_Grilla_Reserva_1()
            'Try
            ' odt = cls_Wurbina.Lista("3", gstrFC_Catastro_Minero & gloZona, gstrFC_ZUrbana & gloZona, v_codigo)
            'Catch ex As Exception
            'odt = cls_Wurbina.Lista("4", gstrFC_Catastro_Minero & gloZona, gstrFC_ZUrbana & gloZona, v_codigo)
            'End Try
            '*********************
            Dim lodbtExisteAR As New DataTable
            Try
                lodbtExisteAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(3, gstrFC_Catastro_Minero & gloZona, gstrFC_ZUrbana & gloZona, v_codigo)
            Catch ex As Exception
            End Try
            Dim contador As Integer
            If lodbtExisteAR.Rows.Count >= 1 Then
                For contador = 0 To lodbtExisteAR.Rows.Count - 1
                    lostrCodigoInterceptado = lodbtExisteAR.Rows(contador).Item("CODIGO")
                    If contador = 0 Then
                        Consulta_Areas_rese = "CODIGO =  '" & lostrCodigoInterceptado & "'"
                    Else
                        Consulta_Areas_rese = Consulta_Areas_rese & " OR " & "CODIGO =  '" & lostrCodigoInterceptado & "'"
                    End If
                Next contador
            End If
            Dim cls_eval As New Cls_evaluacion
            Dim aFound1 As Boolean = False
            Dim capa_sele As ISelectionSet
            cls_Catastro.Add_ShapeFile1("Zona Urbana" & fecha_archi, m_Application, "URBANA")
            cls_eval.consultacapaDM("", "URBANA", "Area_Urbana_" & fecha_archi)
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = "Area_Urbana_" & fecha_archi Then
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
                pFeatureSelection = pFeatureLayer
                capa_sele = pFeatureSelection.SelectionSet
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
                    dRow.Item("NOMBRE") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("NM_URBA"))
                    'dRow.Item("CLASE") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("CLASE"))
                    dRow.Item("CLASE") = ""
                    dRow.Item("TIPO") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("TP_URBA"))
                    dRow.Item("CATEGORIA") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("CATEGORI"))
                    dRow.Item("AREA") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("HAS"))
                    dRow.Item("USO") = pFeature.Value(pFeatureLayer.FeatureClass.Fields.FindField("USO"))
                    odt.Rows.Add(dRow)
                    pFeature = pFCursor.NextFeature
                Loop
            End If
            '**************
            If odt.Rows.Count > 0 Then
                lointRpta = MsgBox("¿ Desea Realizar Areas de 100 Ha " & vbNewLine & _
                                        "(caso contrario Areas de 10 Ha ?", MsgBoxStyle.YesNo, "BDGEOCATMIN")
                dgdDetalle.DataSource = odt
                PT_Agregar_Funciones_Reserva_1() : PT_Forma_Grilla_Reserva_1()
                cls_Catastro.Quitar_Layer("ZonaUrbana_10Ha", m_Application)
                lodtbDatos = New DataTable
                lostrCodigoInterceptado = odt.Rows(0).Item("CODIGO")
                Select Case lointRpta
                    Case 6 '--Genera caja de 100Ha
                        lo_step = 100 * 10
                        k = 0
                        For i As Integer = gloEsteMin To gloEsteMax - 1 Step lo_step
                            Select Case k
                                Case 0
                                    lodtbDatos.Columns.Add("CG_CODIGO", Type.GetType("System.String"))
                                    lodtbDatos.Columns.Add("PE_NOMDER", Type.GetType("System.String"))
                                    lodtbDatos.Columns.Add("CD_COREST", Type.GetType("System.Double"))
                                    lodtbDatos.Columns.Add("CD_CORNOR", Type.GetType("System.Double"))
                                    lodtbDatos.Columns.Add("CD_NUMVER", Type.GetType("System.Double"))
                            End Select
                            For j As Integer = gloNorteMin To gloNorteMax - 1 Step lo_step
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
                                dRow.Item("CD_COREST") = i + lo_step : dRow.Item("CD_CORNOR") = j : dRow.Item("CD_NUMVER") = loint_l + 1
                                lodtbDatos.Rows.Add(dRow)
                                dRow = lodtbDatos.NewRow
                                dRow.Item("CG_CODIGO") = v_codigo & "_" & k '"Area_" & k
                                dRow.Item("PE_NOMDER") = "Area_"
                                dRow.Item("CD_COREST") = i + lo_step : dRow.Item("CD_CORNOR") = j + lo_step : dRow.Item("CD_NUMVER") = loint_l + 2
                                lodtbDatos.Rows.Add(dRow)
                                dRow = lodtbDatos.NewRow
                                dRow.Item("CG_CODIGO") = v_codigo & "_" & k '"Area_" & k
                                dRow.Item("PE_NOMDER") = "Area_"
                                dRow.Item("CD_COREST") = i : dRow.Item("CD_CORNOR") = j + lo_step : dRow.Item("CD_NUMVER") = loint_l + 3
                                lodtbDatos.Rows.Add(dRow)
                            Next
                        Next
                    Case 7 'Genera Caja de 10Ha
                        If odt.Rows.Count >= 1 Then
                            'lo_step = 100 * 10
                            If gloEsteMin <> 0 And gloEsteMax <> 0 And gloNorteMin <> 0 And gloNorteMax <> 0 Then
                                ptot = 0
                                For i As Integer = gloEsteMin To gloEsteMax - 1 Step 200
                                    Select Case k
                                        Case 0
                                            lodtbDatos.Columns.Add("CG_CODIGO", Type.GetType("System.String"))
                                            lodtbDatos.Columns.Add("PE_NOMDER", Type.GetType("System.String"))
                                            lodtbDatos.Columns.Add("CD_COREST", Type.GetType("System.Double"))
                                            lodtbDatos.Columns.Add("CD_CORNOR", Type.GetType("System.Double"))
                                            lodtbDatos.Columns.Add("CD_NUMVER", Type.GetType("System.Double"))
                                    End Select
                                    For j As Integer = gloNorteMin To gloNorteMax - 1 Step 500
                                        k = k + 1
                                        loint_l = 1
                                        Dim dRow As DataRow
                                        dRow = lodtbDatos.NewRow
                                        dRow.Item("CG_CODIGO") = "DM_" & k
                                        dRow.Item("PE_NOMDER") = "DM"
                                        dRow.Item("CD_COREST") = i : dRow.Item("CD_CORNOR") = j : dRow.Item("CD_NUMVER") = loint_l
                                        lodtbDatos.Rows.Add(dRow)
                                        dRow = lodtbDatos.NewRow
                                        dRow.Item("CG_CODIGO") = "DM_" & k
                                        dRow.Item("PE_NOMDER") = "DM"
                                        dRow.Item("CD_COREST") = i + 200 : dRow.Item("CD_CORNOR") = j : dRow.Item("CD_NUMVER") = loint_l + 1
                                        lodtbDatos.Rows.Add(dRow)
                                        dRow = lodtbDatos.NewRow
                                        dRow.Item("CG_CODIGO") = "DM_" & k
                                        dRow.Item("PE_NOMDER") = "DM"
                                        dRow.Item("CD_COREST") = i + 200 : dRow.Item("CD_CORNOR") = j + 500 : dRow.Item("CD_NUMVER") = loint_l + 2
                                        lodtbDatos.Rows.Add(dRow)
                                        dRow = lodtbDatos.NewRow
                                        dRow.Item("CG_CODIGO") = "DM_" & k
                                        dRow.Item("PE_NOMDER") = "DM"
                                        dRow.Item("CD_COREST") = i : dRow.Item("CD_CORNOR") = j + 500 : dRow.Item("CD_NUMVER") = loint_l + 3
                                        lodtbDatos.Rows.Add(dRow)
                                    Next j
                                Next i
                            End If
                        Else
                            MsgBox("No existe áreas interceptadas de Zonas Urbanas en DM...", MsgBoxStyle.Information, "[BDGeocatmin]")
                            DialogResult = Windows.Forms.DialogResult.Cancel
                            Exit Sub
                        End If
                End Select
            Else
                MsgBox("No existe áreas interceptadas de Zonas Urbanas en DM...", MsgBoxStyle.Information, "[BDGeocatmin]")
                DialogResult = Windows.Forms.DialogResult.Cancel
                Exit Sub
            End If
        Else
            MsgBox("El cálculo de cuadrículas solo es para DM tipo PE...", MsgBoxStyle.Information, "[BDGeocatmin]")
            DialogResult = Windows.Forms.DialogResult.Cancel
            Exit Sub
        End If
        lodtbdgdInterno = New DataTable
        lodtbdgdInterno.Columns.Add("ITEM", Type.GetType("System.String"))
        lodtbdgdInterno.Columns.Add("CODIGO", Type.GetType("System.String"))
        lodtbdgdInterno.Columns.Add("TIPO", Type.GetType("System.String"))
        lodtbdgdInterno.Columns.Add("CUADRICULA", Type.GetType("System.Double"))
        lodtbdgdInterno.Columns.Add("AREA", Type.GetType("System.Double"))
        cls_Catastro.Delete_Rows_FC_GDB("ZonaUrbana_10Ha") '& gloZona)
        cls_Catastro.Load_FC_GDB("Catastro", "", m_Application, True)
        cls_DM_2.Genera_Poligono_100Ha(lodtbDatos, gloZona, m_Application, 2)
        odtZonaUrbana = cls_Wurbina.CalculaInterseccion(2, "ZonaUrbana_10Ha", m_Application, gloZona, gstrFC_ZUrbana & gloZona, lostrCodigoInterceptado)
        dgdZonaUrbana.DataSource = odtZonaUrbana
        cls_Catastro.Quitar_Layer("Catastro_1", m_Application)
        Dim lodtbLeyenda As New DataTable
        lodtbLeyenda = cls_Wurbina.f_Genera_Leyenda_DM("ZonaUrbana_10Ha", "", m_Application)
        'cls_Wurbina.Poligono_Color_1("ZonaUrbana_10Ha", lodtbLeyenda, glo_pathStyle & "\F_AREA_RESERVA.style", "TIPO", "", m_Application)
        cls_Catastro.Quitar_Layer("Area_Urbana_" & fecha_archi, m_Application)
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim cls_oracle As New cls_Oracle
        Dim lostrRetorno As String = "", lostrCgCodigo As String = "", lostrArea As String = "2"
        Dim vDescrip As String = "", vCuadricula As String = "", vArea As String = "", v_codEva As String = "", v_Clase As String = ""
        Dim lostrClase As String = "", lostrZU As String = "", lostrReserva As String = ""
        'Detalle de la Grilla
        For w As Integer = 0 To Me.dgdDetalle.RowCount - 1
            lostrCgCodigo = lostrCgCodigo & (w + 1 & " | " & dgdDetalle.Item(w, "CODIGO") & " || ")
            lostrArea = lostrArea & (w + 1 & " | " & dgdDetalle.Item(w, "AREA") & " || ")
            If dgdDetalle.Item(w, "CLASE") = "ANP" Then
                lostrClase = lostrClase & w + 1 & " | " & "AN" & " || "
                lostrZU = lostrZU & w + 1 & " | " & "ZU" & " || "
            Else
                lostrClase = lostrClase & w + 1 & " | " & "" & " || "
                lostrZU = lostrZU & w + 1 & " | " & "ZU" & " || "
            End If
        Next
        Dim lostrSG_D_EVALGIS As String = ""
        'glo_InformeDM = "1"

        lostrSG_D_EVALGIS = cls_oracle.FT_SG_CUENTA_REG_IN_EVALGIS(v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM))
        'lostrSG_D_EVALGIS = cls_oracle.FT_SG_D_EVALGIS("INS", v_codigo, glo_InformeDM, gstrCodigo_Usuario)
        '/////////////
        If lostrSG_D_EVALGIS = "1" Then
            For w As Integer = 0 To Me.dgdDetalle.RowCount - 1
                lostrZU = "ZU"
                lostrCgCodigo = dgdDetalle.Item(w, "CODIGO")
                lostrRetorno = cls_oracle.FT_SG_D_CARAC_EVALGIS("DEL1", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), lostrCgCodigo, "ZU", _
                      "", "", "", "")
                lostrSG_D_EVALGIS = cls_oracle.FT_SG_D_EVALGIS("DEL1", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), gstrCodigo_Usuario)
            Next

            lostrCgCodigo = ""
            lostrArea = ""
            lostrClase = ""
            lostrZU = ""

            For w As Integer = 0 To Me.dgdDetalle.RowCount - 1
                lostrCgCodigo = lostrCgCodigo & (w + 1 & " | " & dgdDetalle.Item(w, "CODIGO") & " || ")
                lostrArea = lostrArea & (w + 1 & " | " & dgdDetalle.Item(w, "AREA") & " || ")
                If dgdDetalle.Item(w, "CLASE") = "ANP" Then  'Modificado porque ya no existe Nucleo
                    lostrClase = lostrClase & w + 1 & " | " & "AN" & " || "
                    lostrZU = lostrZU & w + 1 & " | " & "ZU" & " || "
                ElseIf dgdDetalle.Item(w, "CLASE") = "AMORTIGUAMIENTO" Then
                    lostrClase = lostrClase & w + 1 & " | " & "AA" & " || "
                    lostrZU = lostrZU & w + 1 & " | " & "ZU" & " || "
                Else
                    lostrClase = lostrClase & w + 1 & " | " & "" & " || "
                    lostrZU = lostrZU & w + 1 & " | " & "ZU" & " || "
                End If
            Next

            lostrSG_D_EVALGIS = cls_oracle.FT_SG_D_EVALGIS("ACT", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), gstrCodigo_Usuario)
            lostrRetorno = cls_oracle.FT_SG_D_CARAC_EVALGIS("ACT", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), lostrCgCodigo, lostrZU, _
            "", lostrArea, lostrClase, gstrUsuarioAcceso)
        Else

            lostrSG_D_EVALGIS = cls_oracle.FT_SG_D_EVALGIS("INS", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), gstrCodigo_Usuario)
            var_Fa_AreaReserva = False
            lostrRetorno = cls_oracle.FT_SG_D_CARAC_EVALGIS("INS", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), lostrCgCodigo, lostrZU, _
            "", lostrArea, lostrClase, gstrUsuarioAcceso)

        End If

        '--------------------


        '&&&
        Dim lostrRetorno1 As String
        cuentaareasrese = cls_oracle.FT_SG_CUENTA_REG_IN_AREAS_EVALGIS(v_codigo, "ZU", IIf(glo_InformeDM = "", "1", glo_InformeDM))
        lostrReserva = "" : lostrArea = ""
        lostrZU = ""
        Dim vTipo As String = "", vCGCodeva As String = ""
        Dim lostrNumPoligono As String = ""
        Dim loContador As Integer = 0
        Dim clsWUrbina As New cls_BDEvaluacion
        Dim v_EstadoRes As String = ""

        If cuentaareasrese = "1" Then
            lostrCgCodigo = ""
            lostrArea = ""
            lostrZU = ""
            For v As Integer = 0 To dgdDetalle.RowCount - 1
                lostrCgCodigo = dgdDetalle.Item(v, "CODIGO")
                lostrArea = dgdDetalle.Item(v, "AREA")
                lostrZU = "ZU"
                lostrRetorno1 = cls_oracle.FT_SG_D_AREAS_EVALGIS("DEL", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), lostrCgCodigo, lostrArea, "ZU", _
                               "", "", "")
                lostrRetorno1 = cls_oracle.FT_SG_D_CARAC_EVALGIS("DEL", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), lostrCgCodigo, "ZU", _
               "", "", "", "")
            Next v
        End If


        If lostrSG_D_EVALGIS = 0 Then
            var_Fa_AreaReserva = True
            cls_oracle.FT_SG_D_EVALGIS("DEL", v_codigo, glo_InformeDM, gstrCodigo_Usuario)
            cls_oracle.FT_SG_D_EVALGIS("ACT", v_codigo, glo_InformeDM, gstrCodigo_Usuario)
            lostrRetorno = cls_oracle.FT_SG_D_CARAC_EVALGIS("ACT", v_codigo, glo_InformeDM, lostrCgCodigo, lostrZU, _
        "", lostrArea, lostrClase, gstrUsuarioAcceso)
        Else
            var_Fa_AreaReserva = False
            lostrRetorno = cls_oracle.FT_SG_D_CARAC_EVALGIS("INS", v_codigo, glo_InformeDM, lostrCgCodigo, lostrZU, _
        "", lostrArea, lostrClase, gstrUsuarioAcceso)
        End If
        lostrReserva = ""
        lostrArea = ""
        'Dim vTipo As String = "", vCGCodeva As String = ""
        'Dim lostrNumPoligono As String = ""
        'Dim loContador As Integer = 0
        For w As Integer = 0 To Me.dgdZonaUrbana.RowCount - 1
            If dgdZonaUrbana.Item(w, "AREA") > 0 Then
                Select Case Me.dgdZonaUrbana.Item(w, "TIPO")
                    Case "Area Parcial"
                        loContador += 1
                        vTipo = vTipo & loContador + 1 & " | " & "PP" & " || "
                    Case "Area Libre"
                        loContador += 1
                        vTipo = vTipo & loContador + 1 & " | " & "LI" & " || "
                    Case "Area Total"
                        loContador += 1
                        vTipo = vTipo & loContador + 1 & " | " & "TT" & " || "
                End Select
                lostrReserva = lostrReserva & w + 1 & " | " & dgdZonaUrbana.Item(w, "URBANA") & " || "
                lostrArea = lostrArea & w + 1 & " | " & dgdZonaUrbana.Item(w, "AREA") & " || "
                'lostrArea = lostrArea & w + 1 & " | " & "100"
                lostrNumPoligono = lostrNumPoligono & w + 1 & " | " & w + 1 & " || "
            End If
            Dim cls_BDEvaluacion As New cls_BDEvaluacion
            'Dim v_EstadoRes As String = ""
            cls_BDEvaluacion.InsertarCoordenadas_AReserva("ZonaUrbana_10Ha", v_codigo, dgdDetalle.Item(dgdDetalle.Row, "CODIGO"), m_Application, v_EstadoRes)

        Next
        'Dim cls_BDEvaluacion As New cls_BDEvaluacion
        'Dim v_EstadoRes As String = ""
        'cls_BDEvaluacion.InsertarCoordenadas_AReserva("ZonaUrbana_10Ha", v_codigo, dgdDetalle.Item(dgdDetalle.Row, "CODIGO"), m_Application, v_EstadoRes)
        'cls_Catastro.Quitar_Layer("Zona Urbana_" & fecha_archi, m_Application)
        Dim lostrRetorno_0 As String = ""
        Select Case v_EstadoRes
            Case "OK"
                MsgBox("La operación se realizó exitosamente.", vbExclamation, "[SIGGeocatmin]")
                var_Fa_Zonaurbana = True
            Case "XX"
                MsgBox("No se pudo completar la operación, ya existe Código: " & v_codigo, vbExclamation, "[SIGGeocatmin]")
        End Select
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Close()
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

    Private Sub dgdDetalle_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgdDetalle.DoubleClick
        cls_Catastro.DefinitionExpression_Campo("CODIGO = '" & dgdDetalle.Item(dgdDetalle.Row, "CODIGO") & "'", m_Application, "Zona Urbana")
    End Sub

    Private Sub dgdZonaUrbana_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgdZonaUrbana.DoubleClick
        cls_Catastro.DefinitionExpression_Campo("TIPO = '" & dgdZonaUrbana.Item(dgdZonaUrbana.Row, "TIPO") & "'", m_Application, "ZonaUrbana_10Ha")
    End Sub

    

    Private Sub dgdDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgdDetalle.Click

    End Sub

    Private Sub dgdDetalle_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles dgdDetalle.RowColChange
       

    End Sub

    Private Sub dgdZonaUrbana_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgdZonaUrbana.Click

    End Sub
End Class