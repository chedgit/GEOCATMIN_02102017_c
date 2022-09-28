Imports PORTAL_Clases
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto

Public Class cls_BDEvaluacion
    Private lostrRetorno As String = ""

    Public Sub InsertarCoordenadas_AReserva(ByVal pLayer As String, ByVal pastr_CGCODIGO As String, ByVal pastr_CGCODEVA As String, ByVal p_App As IApplication, ByRef p_Estado As String)
        Dim lostrRetorno_0 As String = "", lostrRetorno_1 As String = ""
        Dim cls_Oracle As New cls_Oracle
        Dim v_IECODIGO As String = ""
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = pLayer Then
                pFeatureLayer = pMap.Layer(A)
                Select Case pLayer
                    Case "AreaReserva_100Ha"
                        v_IECODIGO = "AP"
                    Case "ZonaUrbana_10Ha"
                        v_IECODIGO = "ZU"
                End Select
                aFound = True : Exit For
            End If
        Next A
        If Not aFound Then Exit Sub
        pFeatureClass = pFeatureLayer.FeatureClass
        Dim l As IPolygon
        Dim pt As IPoint
        Dim pFilter As IQueryFilter
        pFilter = New QueryFilter
        pFilter.WhereClause = "" ' "CODIGOU = '" & pastr_CGCODIGO & "'"
        pFeatureCursor = pFeatureClass.Search(pFilter, False)
        pFeature = pFeatureCursor.NextFeature
        Dim lostrCoox As String = "", lostrCooy As String = ""
        Dim pArea As IArea
        Dim lointArea As Double
        Dim lointNumVer As Integer = 0
        Dim lostNumPoligono As String = "", lostAG_TIPO As String = ""
        Dim lointVertice As String = ""
        Dim lostrArea As String = ""
        Dim lodouArea As Double

        Do Until pFeature Is Nothing
            lostrCooy = "" : lostrCoox = ""
            'lostNumPoligono = (Mid(pFeature.Value(pFeature.Fields.FindField("CODIGOU")), InStr(pFeature.Value(pFeature.Fields.FindField("CODIGOU")), "_") + 1))
            lostNumPoligono = (Mid(pFeature.Value(pFeature.Fields.FindField("POLIGONO")), InStr(pFeature.Value(pFeature.Fields.FindField("POLIGONO")), "_") + 1))
            lostrArea = pFeature.Value(pFeature.Fields.FindField("AREA"))
            For z As Integer = 0 To lodtbdgdInterno.Rows.Count - 1
                'If lodtbdgdInterno.Rows(z).Item("CODIGO") = pastr_CGCODEVA And lodtbdgdInterno.Rows(z).Item("ITEM") = lostNumPoligono Then
                If lodtbdgdInterno.Rows(z).Item("CODIGO") = pastr_CGCODEVA Then
                    Select Case lodtbdgdInterno.Rows(z).Item("TIPO")
                        Case "Area Total"
                            'lostAG_TIPO = Single.Parse(lostNumPoligono) + 1 & " | " & "TT" & " ||"
                            lostAG_TIPO = 1 & " | " & "TT" & " ||"
                            ' Exit For
                        Case "Area Libre"
                            'lostAG_TIPO = Single.Parse(lostNumPoligono) + 1 & " | " & "LI" & " ||"
                            lostAG_TIPO = 1 & " | " & "LI" & " ||"
                            'Exit For
                        Case "Area Parcial"
                            'lostAG_TIPO = Single.Parse(lostNumPoligono) + 1 & " | " & "PP" & " ||"
                            lostAG_TIPO = 1 & " | " & "PP" & " ||"
                            'Exit For
                    End Select

                End If
            Next
            lodouArea = 0
            l = pFeature.Shape
            pArea = pFeature.Shape
            lointArea = Math.Abs(pArea.Area) / 10000

            ptcol = l
            lointNumVer = ptcol.PointCount - 1
            For w As Integer = 0 To ptcol.PointCount - 2
                pt = ptcol.Point(w)
                lostrCoox = lostrCoox & w + 1 & " | " & pt.X & " || "
                lostrCooy = lostrCooy & w + 1 & " | " & pt.Y & " || "
            Next w

            If cuentaareasrese = "1" Then

                'lostrRetorno_0 = cls_Oracle.FT_SG_D_AREAS_EVALGIS("ACT", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), 1 & " | " & pastr_CGCODEVA & " ||", _
                '     1 & " | " & lostNumPoligono & " ||", 1 & " | " & v_IECODIGO & " ||", _
                '     1 & " | " & lostrArea & " ||", _
                '      lostAG_TIPO, gstrUsuarioAcceso)

                lostrRetorno_0 = cls_Oracle.FT_SG_D_AREAS_EVALGIS("ACT", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), 1 & " | " & pastr_CGCODEVA & " ||", _
                     1 & " | " & lostNumPoligono & " ||", v_IECODIGO, 1 & " | " & lostrArea & " ||", 1 & "|" & lostAG_TIPO & "||", gstrUsuarioAcceso)


                'lostrRetAreas = cls_Oracle.FT_SG_D_AREAS_EVALGIS("ACT", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), 1 & "|" & lostCGCODEVA & "||", 1 & "|" & lodouArea & "||", v_IECODIGO, _
                '     1 & "|" & lodouHa & "||", 1 & "|" & vTipo & "||", gstrUsuarioAcceso)



            ElseIf cuentaareasrese = "0" Then

                lostrRetorno_0 = cls_Oracle.FT_SG_D_AREAS_EVALGIS("INS", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), 1 & " | " & pastr_CGCODEVA & " ||", _
                    1 & " | " & lostNumPoligono & " ||", 1 & " | " & v_IECODIGO & " ||", _
                     1 & " | " & lostrArea & " ||", _
                      lostAG_TIPO, gstrUsuarioAcceso)
            End If


            'If var_Fa_AreaReserva = True Then
            If cuentaareasrese = "1" Then
                lostrRetorno_1 = cls_Oracle.FT_SG_D_COORD_EVALGIS("ACT", pastr_CGCODIGO, IIf(glo_InformeDM = "", "1", glo_InformeDM), pastr_CGCODEVA, lostNumPoligono, v_IECODIGO, _
                          lostrCoox, lostrCooy, lointArea, gstrUsuarioAcceso)

                'ElseIf var_Fa_AreaReserva = False Then
            ElseIf cuentaareasrese = "0" Then
                lostrRetorno_1 = cls_Oracle.FT_SG_D_COORD_EVALGIS("INS", pastr_CGCODIGO, IIf(glo_InformeDM = "", "1", glo_InformeDM), pastr_CGCODEVA, lostNumPoligono, v_IECODIGO, _
                          lostrCoox, lostrCooy, lointArea, gstrUsuarioAcceso)

            End If
            pFeature = pFeatureCursor.NextFeature
            If lostrRetorno_0 = "0" And lostrRetorno_1 = "0" Then
                Exit Do
            End If
        Loop
        If lostrRetorno_0 = "1" And lostrRetorno_1 = "1" Then
            p_Estado = "OK"
        Else
            p_Estado = "XX"
        End If
    End Sub

    Public Sub InsertarCoordenadas(ByVal pLayer As String, ByVal pastr_CGCODIGO As String, ByVal p_ListBox As Object, ByVal p_App As IApplication, ByRef p_Estado As String)
        Dim cls_Oracle As New cls_Oracle
        Dim num_area As String = ""
        Dim lostrRetorno As String
        Dim lostrRetorno1 As String
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        Dim contador As Integer = 0
        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = pLayer Then
                pFeatureLayer = pMap.Layer(A)
                aFound = True : Exit For
            End If
        Next A
        If Not aFound Then Exit Sub
        pFeatureClass = pFeatureLayer.FeatureClass
        Dim l As IPolygon
        Dim pt As IPoint
        Dim pFilter As IQueryFilter
        pFilter = New QueryFilter
        pFilter.WhereClause = "CODIGOU = '" & pastr_CGCODIGO & "'"
        pFeatureCursor = pFeatureClass.Search(pFilter, False)
        pFeature = pFeatureCursor.NextFeature
        Dim lostrCoox As String = "", lostrCooy As String = ""
        Dim pArea As IArea
        Dim lointArea As Double
        Dim lointNumVer As Integer = 0
        Dim lostCODEVA As String = ""
        Dim lointVertice As String = ""
        Dim lodouArea As Double
        Dim pForm As New Frm_AreasSuperpuestas
        lostrRetorno = cls_Oracle.FT_SG_CUENTA_REG_IN_AREAS_EVALGIS(pastr_CGCODIGO, "PR", glo_InformeDM)

        For i As Integer = 0 To p_ListBox.RowCount - 1

            'Do Until pFeature Is Nothing
            lostrCooy = "" : lostrCoox = ""
            ' For w As Integer = 0 To Me.dgdDetalle.RowCount - 1
            'lostCODEVA = pFeature.Value(pFeature.Fields.FindField("CODIGOU_1"))
            lostCODEVA = p_ListBox.Item(i, "CODIGOU").ToString
            'lodouArea = pFeature.Value(pFeature.Fields.FindField("AREA_FINAL"))
            lodouArea = p_ListBox.Item(i, "AREA").ToString
            num_area = p_ListBox.Item(i, "NUM_AREA").ToString
            l = pFeature.Shape
            pArea = pFeature.Shape   'pArea.Area
            lointArea = pArea.Area
            ptcol = l
            lointNumVer = ptcol.PointCount - 1
            For w As Integer = 0 To ptcol.PointCount - 2
                pt = ptcol.Point(w)
                lostrCoox = lostrCoox & w + 1 & " | " & pt.X & " || "
                lostrCooy = lostrCooy & w + 1 & " | " & pt.Y & " || "
            Next w
            pFeature = pFeatureCursor.NextFeature
            'lostrRetorno = cls_Oracle.FT_SG_D_AREAS_EVALGIS("INS", pastr_CGCODIGO, IIf(glo_InformeDM = "", "IT", glo_InformeDM), 1 & "|" & lostCODEVA & "||", 1 & "|" & lodouArea & "||", "PR", _
            ' lostrRetorno = cls_Oracle.FT_SG_CUENTA_REG_IN_AREAS_EVALGIS(pastr_CGCODIGO, glo_InformeDM, "PR")
            If lostrRetorno = "1" Then

                'MsgBox("YA EXISTE")
                If i = 0 Then
                    lostrRetorno1 = cls_Oracle.FT_SG_D_AREAS_EVALGIS("DEL", pastr_CGCODIGO, glo_InformeDM, pastr_CGCODIGO, "1", "PR", _
                    "", "", "")

                    lostrRetorno1 = cls_Oracle.FT_SG_D_CARAC_EVALGIS("DEL", pastr_CGCODIGO, glo_InformeDM, pastr_CGCODIGO, "AD", _
                   "", "", "", "")
                    'lostrRetorno = 1
                End If

                lostrRetorno = cls_Oracle.FT_SG_D_AREAS_EVALGIS("ACT", pastr_CGCODIGO, IIf(glo_InformeDM = "", "1", glo_InformeDM), 1 & "|" & lostCODEVA & "||", 1 & "|" & num_area & "||", "PR", _
                1 & "|" & lodouArea & "||", "", gstrUsuarioAcceso)


                lostrRetorno = cls_Oracle.FT_SG_D_COORD_EVALGIS("ACT", pastr_CGCODIGO, glo_InformeDM, lostCODEVA, num_area, "PR", _
                            lostrCoox, lostrCooy, 0, gstrUsuarioAcceso)
                var_fa_tipoactualiza = True
            Else
                'MsgBox("INSERTA")

                'lostrRetorno1 = cls_Oracle.FT_SG_D_AREAS_EVALGIS("INS", pastr_CGCODIGO, glo_InformeDM, 1 & "|" & lostCODEVA & "||", 1 & "|" & num_area & "||", "PR", _
                '1 & "|" & lodouArea & "||", "", gstrUsuarioAcceso)


                lostrRetorno1 = cls_Oracle.FT_SG_D_AREAS_EVALGIS("INS", pastr_CGCODIGO, IIf(glo_InformeDM = "", "1", glo_InformeDM), 1 & "|" & lostCODEVA & "||", 1 & "|" & num_area & "||", "PR", _
                1 & "|" & lodouArea & "||", "", gstrUsuarioAcceso)

               

                'lostrRetorno1 = cls_Oracle.FT_SG_D_AREAS_EVALGIS("INS", pastr_CGCODIGO, IIf(glo_InformeDM = "", "1", glo_InformeDM), 1 & " | " &lostCODEVA & " ||", 1 & " | " & num_area & " ||", "PR",
                '1 & "|" & lodouArea & "||", "", gstrUsuarioAcceso)


                lostrRetorno1 = cls_Oracle.FT_SG_D_COORD_EVALGIS("INS", pastr_CGCODIGO, glo_InformeDM, lostCODEVA, num_area, "PR", _
                            lostrCoox, lostrCooy, 0, gstrUsuarioAcceso)



                'If cuentaareasrese = "1" Then

                '    lostrRetorno_0 = cls_Oracle.FT_SG_D_AREAS_EVALGIS("ACT", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), 1 & " | " & pastr_CGCODEVA & " ||", _
                '         1 & " | " & lostNumPoligono & " ||", 1 & " | " & v_IECODIGO & " ||", _
                '         1 & " | " & lostrArea & " ||", _
                '          lostAG_TIPO, gstrUsuarioAcceso)
                'ElseIf cuentaareasrese = "0" Then

                '    lostrRetorno_0 = cls_Oracle.FT_SG_D_AREAS_EVALGIS("INS", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), 1 & " | " & pastr_CGCODEVA & " ||", _
                '        1 & " | " & lostNumPoligono & " ||", 1 & " | " & v_IECODIGO & " ||", _
                '         1 & " | " & lostrArea & " ||", _
                '          lostAG_TIPO, gstrUsuarioAcceso)
                'End If




                var_fa_tipoactualiza = False


            End If
            'If var_fa_tipoactualiza = False Then

            'lostrRetorno = cls_Oracle.FT_SG_D_AREAS_EVALGIS("INS", pastr_CGCODIGO, glo_InformeDM, 1 & "|" & lostCODEVA & "||", 1 & "|" & num_area & "||", "PR", _
            '1 & "|" & lodouArea & "||", "", gstrUsuarioAcceso)
            'ElseIf var_fa_tipoactualiza = True Then
            'lostrRetorno = cls_Oracle.FT_SG_D_AREAS_EVALGIS("ACT", pastr_CGCODIGO, glo_InformeDM, 1 & "|" & lostCODEVA & "||", 1 & "|" & num_area & "||", "PR", _
            '1 & "|" & lodouArea & "||", "", gstrUsuarioAcceso)
            'End If

            '--'lostrRetorno = cls_Oracle.FT_SG_D_COORD_EVALGIS("INS", pastr_CGCODIGO, IIf(glo_InformeDM = "", "IT", glo_InformeDM), lostCODEVA, lodouArea, "PR", _
            'If var_fa_tipoactualiza = False Then
            '--'lostrRetorno = cls_Oracle.FT_SG_D_COORD_EVALGIS("INS", pastr_CGCODIGO, glo_InformeDM, lostCODEVA, lodouArea, "PR", _
            '--'            lostrCoox, lostrCooy, 0, gstrUsuarioAcceso)
            'lostrRetorno = cls_Oracle.FT_SG_D_COORD_EVALGIS("INS", pastr_CGCODIGO, glo_InformeDM, lostCODEVA, num_area, "PR", _
            '           lostrCoox, lostrCooy, 0, gstrUsuarioAcceso)
            'ElseIf var_fa_tipoactualiza = True Then
            'lostrRetorno = cls_Oracle.FT_SG_D_COORD_EVALGIS("ACT", pastr_CGCODIGO, glo_InformeDM, lostCODEVA, num_area, "PR", _
            '           lostrCoox, lostrCooy, 0, gstrUsuarioAcceso)
            'End If

            p_Estado = lostrRetorno
            ' Loop
        Next i
    End Sub
    Public Sub InsertarCoordenadas_2(ByVal pLayer As String, ByVal pastr_CGCODIGO As String, ByVal p_ListBox As Object, ByVal p_App As IApplication, ByRef p_Estado As String)
        Dim cls_Oracle As New cls_Oracle
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = pLayer Then
                pFeatureLayer = pMap.Layer(A)
                aFound = True : Exit For
            End If
        Next A
        If Not aFound Then Exit Sub
        pFeatureClass = pFeatureLayer.FeatureClass
        'Dim l As IPolygon
        'Dim pt As IPoint
        Dim pFilter As IQueryFilter
        pFilter = New QueryFilter
        pFilter.WhereClause = "CODIGOU = '" & pastr_CGCODIGO & "'"
        pFeatureCursor = pFeatureClass.Search(pFilter, False)
        pFeature = pFeatureCursor.NextFeature
        Dim lostrCoox As String = "", lostrCooy As String = "", lostrArea As String = ""
        'Dim pArea As IArea
        'Dim lointArea As Double
        Dim lointNumVer As Integer = 0
        Dim lostCODEVA As String = ""
        Dim lointVertice As String = ""
        Dim lodouArea As Double
        Do Until pFeature Is Nothing
            lostrCooy = "" : lostrCoox = ""
            lostCODEVA = pFeature.Value(pFeature.Fields.FindField("CODIGOU"))
            lodouArea = pFeature.Value(pFeature.Fields.FindField("AREA_FINAL"))
            lostrArea = lostrArea & 1 & " | " & lodouArea & " || "
            'lostrRetorno = cls_Oracle.FT_INS_AREA_RESTRINGIDA("INS", pastr_CGCODIGO, "IT", pastr_CGCODIGO, "AD", _
            '"", lostrArea, 0, gstrUsuarioAcceso, gstrUsuarioClave, gstrDatabase)
            'lostrRetorno = cls_Oracle.FT_SG_D_CARAC_EVALGIS("INS", pastr_CGCODIGO, IIf(glo_InformeDM = "", "IT", glo_InformeDM), "1 | " & pastr_CGCODIGO & " ||", "1 | AD ||", _

            If var_fa_tipoactualiza = False Then
                lostrRetorno = cls_Oracle.FT_SG_D_CARAC_EVALGIS("INS", pastr_CGCODIGO, glo_InformeDM, "1 | " & pastr_CGCODIGO & " ||", "1 | AD ||", _
                "", lostrArea, "", gstrUsuarioAcceso)
            ElseIf var_fa_tipoactualiza = True Then
                lostrRetorno = cls_Oracle.FT_SG_D_CARAC_EVALGIS("ACT", pastr_CGCODIGO, glo_InformeDM, "1 | " & pastr_CGCODIGO & " ||", "1 | AD ||", _
                "", lostrArea, "", gstrUsuarioAcceso)
            End If


            pFeature = pFeatureCursor.NextFeature
        Loop
        p_Estado = lostrRetorno
    End Sub

    Public Sub Show_Vertices(ByVal pLayer As String, ByVal pCodigou As String, ByVal p_App As IApplication, _
    ByVal loCooX As Object, ByVal loCooY As Object, ByVal loNumVer As Object, ByVal loArea As Object)
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = pLayer Then
                pFeatureLayer = pMap.Layer(A)
                aFound = True : Exit For
            End If
        Next A
        If Not aFound Then Exit Sub
        'pMxDoc.ActiveView.Refresh()
        pFeatureClass = pFeatureLayer.FeatureClass
        Dim l As IPolygon
        Dim pt As IPoint
        Dim pFilter As IQueryFilter
        pFilter = New QueryFilter
        pFilter.WhereClause = "CODIGOU = '" & pCodigou & "'"
        pFeatureCursor = pFeatureClass.Search(pFilter, False)
        pFeature = pFeatureCursor.NextFeature
        Dim lostrCoox As String = "", lostrCooy As String = ""
        Dim pArea As IArea
        Do Until pFeature Is Nothing
            Dim cod As String = pFeature.Value(pFeature.Fields.FindField("CODIGO"))
            l = pFeature.Shape
            pArea = pFeature.Shape   'pArea.Area
            loArea.text = pArea.Area
            ptcol = l
            loNumVer.text = ptcol.PointCount - 1

            For w As Integer = 0 To ptcol.PointCount - 2
                pt = ptcol.Point(w)
                'lostrCoo = lostrCoo & ("Codigou: " & pCodigou & " Vertices: " & ptcol.PointCount - 1 & "Vértice: " & w + 1 & " =>   x: " & pt.X & "   y: " & pt.Y) & vbNewLine
                lostrCoox = lostrCoox & w + 1 & " | " & pt.X & " || "
                lostrCooy = lostrCooy & w + 1 & " | " & pt.Y & " || "
            Next w
            pFeature = pFeatureCursor.NextFeature
            'MsgBox(lostrCoo)
        Loop
        loCooX.text = lostrCoox
        loCooY.text = lostrCooy
    End Sub
    Public Sub InsertarCoordenadas_OP4(ByVal pLayer As String, ByVal pastr_CGCODIGO As String, ByVal pastr_CGCODEVA As String, _
       ByVal p_IECodigo As String, ByVal p_App As IApplication)
        Dim lostrRetorno_0 As String = "", lostrRetorno_1 As String = ""
        Dim cls_Oracle As New cls_Oracle
        'Dim v_IECODIGO As String = ""
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = pLayer Then
                pFeatureLayer = pMap.Layer(A)
                Select Case pLayer
                    'Case "AreaRese_super"
                    Case "AreaReseva_100Ha"
                        'v_IECODIGO = "PY"
                End Select
                aFound = True : Exit For
            End If
        Next A
        If Not aFound Then Exit Sub
        pFeatureClass = pFeatureLayer.FeatureClass
        Dim l As IPolygon
        Dim pt As IPoint
        Dim pFilter As IQueryFilter
        pFilter = New QueryFilter
        pFilter.WhereClause = ""
        pFeatureCursor = pFeatureClass.Search(pFilter, False)
        pFeature = pFeatureCursor.NextFeature
        Dim lostrCoox As String = "", lostrCooy As String = ""
        Dim pArea As IArea
        Dim lointArea As Double
        Dim lointNumVer As Integer = 0
        Dim lostNumPoligono As String = "", lostAG_TIPO As String = ""
        Dim lointVertice As String = ""
        Dim lodouArea As Double
        Do Until pFeature Is Nothing
            lostrCooy = "" : lostrCoox = ""
            lostNumPoligono = pFeature.Value(pFeature.Fields.FindField("CODIGOU")) ', InStr(pFeature.Value(pFeature.Fields.FindField("CODIGOU")), "_") + 1))
            'lodouArea = pFeature.Value(pFeature.Fields.FindField("HAS"))
            lodouArea = pFeature.Value(pFeature.Fields.FindField("Area"))
            l = pFeature.Shape
            pArea = pFeature.Shape
            lointArea = Math.Abs(pArea.Area) / 10000
            ptcol = l
            lointNumVer = ptcol.PointCount - 1
            For w As Integer = 0 To ptcol.PointCount - 2
                pt = ptcol.Point(w)
                lostrCoox = lostrCoox & w + 1 & " | " & pt.X & " || "
                lostrCooy = lostrCooy & w + 1 & " | " & pt.Y & " || "
            Next w
            lostrRetorno_1 = cls_Oracle.FT_SG_D_COORD_EVALGIS("INS", pastr_CGCODIGO, glo_InformeDM, pastr_CGCODEVA, _
            lostNumPoligono, p_IECodigo, lostrCoox, lostrCooy, lointArea, gstrUsuarioAcceso)
            'lostrRetorno_1 = cls_Oracle.FT_SG_D_COORD_EVALGIS("INS", pastr_CGCODIGO, glo_InformeDM, pastr_CGCODEVA, _
            'lodouArea, p_IECodigo, lostrCoox, lostrCooy, lointArea, gstrUsuarioAcceso)
            pFeature = pFeatureCursor.NextFeature
        Loop
    End Sub
End Class
