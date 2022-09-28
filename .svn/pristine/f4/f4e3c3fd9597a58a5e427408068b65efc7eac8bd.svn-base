Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.DataSourcesFile
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Output
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Catalog
Imports stdole

Imports ESRI.ArcGIS.Display
Imports Oracle.DataAccess.Client
Imports PORTAL_Clases




Public Class cls_Utilidades
    Private DataInfo() As FieldData
    Private DataXY As VerXY
    Private Structure FieldData
        Dim Index As Integer
        Dim Campo As String
    End Structure
    Private Structure VerXY
        Dim sCodigoID As String
        Dim sEsteX As String
        Dim sNorteY As String
        Dim iEsteX As Integer
        Dim iNorteY As Integer
        Dim iCodigoID As Integer
    End Structure

    Dim cnx_Oracle As New OracleConnection("Data Source= " & gstrDatabaseGIS & ";USER ID= " & gstrUsuarioAcceso & "; PASSWORD=" & gstrUsuarioClave)

    Public Function CalculaInterseccion(ByVal pTipo As String, ByVal pLayerIn As String, ByVal p_App As IApplication, ByVal p_Zona As String, _
       ByVal p_LayerInt As String, ByVal pastrCodInterceptado As String)
        Dim cls_oracle As New cls_Oracle
        pMxDoc = p_App.Document
        Dim pOriginalLayer As IFeatureLayer = Nothing
        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = pLayerIn Then
                pOriginalLayer = pMap.Layer(A)
                aFound = True
                Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("No existe el Layer: " & pLayerIn, MsgBoxStyle.Information, "[BDGEOCATMIN]")
            Return 0
            Exit Function
        End If
        Dim pPoint As IPoint
        Dim pPoly As IPolygon
        Dim pPolyPointColl As IPointCollection
        Dim i As Integer
        Dim lostrZU As String = ""
        '-Dim pQF As IQueryFilter
        pQFilter = New QueryFilter
        pQFilter.WhereClause = "" ' to get all features
        Dim pFeatCursor As IFeatureCursor
        pFeatCursor = pOriginalLayer.FeatureClass.Search(pQFilter, False)
        Dim pFeat As IFeature
        pFeat = pFeatCursor.NextFeature
        Dim lostrClase As String = ""
        Dim loStrCoordenada As String
        Dim loTotPoligono As Integer = 0
        Dim loTotParcial As Integer = 0
        Dim loTotLleno As Integer = 0
        Dim loTotLibre As Integer = 0
        Dim lodtbSeleccion As New Data.DataTable
        loTotPoligono = 0
        Dim loCampo As String = ""
        If pLayerIn = "ZonaUrbana_10Ha" Then
            loCampo = "URBANA"
        Else
            loCampo = "RESERVA"
        End If
        '-------------------
        pFeatureClass = pOriginalLayer.FeatureClass
        Dim pUpdateFeatures As IFeatureCursor
        pUpdateFeatures = pFeatureClass.Update(pQFilter, False)
        Dim pFeature As IFeature
        pFeature = pUpdateFeatures.NextFeature
        '------------------

        Do While Not pFeat Is Nothing
            If loTotPoligono = 0 Then
                lodtbSeleccion.Columns.Add("CODIGO", Type.GetType("System.String"))
                lodtbSeleccion.Columns.Add(loCampo, Type.GetType("System.String"))
                lodtbSeleccion.Columns.Add("TIPO", Type.GetType("System.String"))
                lodtbSeleccion.Columns.Add("CUADRICULA", Type.GetType("System.Double"))
                'lodtbSeleccion.Columns.Add("NUCLEO", Type.GetType("System.String"))
                lodtbSeleccion.Columns.Add("AREA", Type.GetType("System.Double"))
            End If
            loStrCoordenada = ""
            pPoly = pFeat.Shape
            pPolyPointColl = pPoly
            ' loop through all points in feature polygon
            For i = 0 To pPolyPointColl.PointCount - 1
                pPoint = pPolyPointColl.Point(i)
                loStrCoordenada = loStrCoordenada & pPoint.X & " " & pPoint.Y & ", "
            Next i  ' get next point in polygon
            loStrCoordenada = Mid(loStrCoordenada, 1, Len(loStrCoordenada) - 2)
            pFeat = pFeatCursor.NextFeature
            Dim lodbtParcial As New DataTable
            Dim lodbtTotal = cls_oracle.FT_Int_Total(pTipo, loStrCoordenada, p_Zona, p_LayerInt, pastrCodInterceptado)
            Dim dRowInterno As DataRow
            dRowInterno = lodtbdgdInterno.NewRow
            dRowInterno.Item("ITEM") = pFeature.Value(5)
            dRowInterno.Item("CODIGO") = pastrCodInterceptado
            dRowInterno.Item("CUADRICULA") = 1
            dRowInterno.Item("AREA") = 100
            If lodbtTotal.Rows.Count = 1 Then
                pFeature.Value(pUpdateFeatures.FindField("TIPO")) = "Area Total"
                loTotLleno = loTotLleno + 1
                dRowInterno.Item("TIPO") = "Area Total"
            Else
                lodbtParcial = cls_oracle.FT_Int_Parcial(pTipo, loStrCoordenada, p_Zona, p_LayerInt)
                If lodbtParcial.Rows.Count = 1 Then
                    pFeature.Value(pUpdateFeatures.FindField("TIPO")) = "Area Parcial"
                    loTotParcial = loTotParcial + 1
                    dRowInterno.Item("TIPO") = "Area Parcial"
                Else
                    dRowInterno.Item("TIPO") = "Area Libre"
                End If

            End If
            If (lodbtTotal.Rows.Count = 0 And lodbtParcial.Rows.Count = 0) Then
                pFeature.Value(pUpdateFeatures.FindField("TIPO")) = "Area Libre"
            End If
            loTotPoligono = loTotPoligono + 1
            pUpdateFeatures.UpdateFeature(pFeature)
            pFeature = pUpdateFeatures.NextFeature
            lodtbdgdInterno.Rows.Add(dRowInterno)
        Loop
        loTotLibre = loTotPoligono - (loTotLleno + loTotParcial)
        Dim lointcontador As Integer = 1
        Dim dRow As DataRow
        If loTotLibre > 0 Then
            dRow = lodtbSeleccion.NewRow
            dRow.Item("CODIGO") = v_codigo : dRow.Item(loCampo) = pastrCodInterceptado : dRow.Item("TIPO") = "Area Libre"
            dRow.Item("CUADRICULA") = loTotLibre : dRow.Item("AREA") = loTotLibre * 100
            lodtbSeleccion.Rows.Add(dRow)
            lointcontador = lointcontador + 1
        End If
        If loTotParcial > 0 Then
            dRow = lodtbSeleccion.NewRow
            dRow.Item("CODIGO") = v_codigo : dRow.Item(loCampo) = pastrCodInterceptado : dRow.Item("TIPO") = "Area Parcial"
            dRow.Item("CUADRICULA") = loTotParcial : dRow.Item("AREA") = loTotParcial * 100
            lodtbSeleccion.Rows.Add(dRow)
            lointcontador = lointcontador + 1
        End If
        If loTotLleno > 0 Then
            dRow = lodtbSeleccion.NewRow
            dRow.Item("CODIGO") = v_codigo : dRow.Item(loCampo) = pastrCodInterceptado : dRow.Item("TIPO") = "Area Total"
            dRow.Item("CUADRICULA") = loTotLleno : dRow.Item("AREA") = loTotLleno * 100
            lodtbSeleccion.Rows.Add(dRow)
        End If
        Return lodtbSeleccion
    End Function

    Public Function CalculaInterseccion_neto(ByVal pTipo)
        Dim cls_oracle As New cls_Oracle
        'pMxDoc = p_App.Document
        Dim pOriginalLayer As IFeatureLayer = Nothing
        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "" Then
                pOriginalLayer = pMap.Layer(A)
                aFound = True
                Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("No existe el Layer: ", MsgBoxStyle.Information, "[BDGEOCATMIN]")
            Return 0
            Exit Function
        End If


        Dim pActiveView As IActiveView
        pActiveView = pMap
        Dim pQueryFilter As IQueryFilter
        Dim pFeatureSelection As IFeatureSelection = pOriginalLayer
        pQueryFilter = New QueryFilter
        pQueryFilter.WhereClause = "CODIGOU = '09009686X01' OR CODIGOU = '09852814Y01' OR CODIGOU = '09009685X01'"

        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, False)
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
        ' Dim pFSel As IFeatureSelection
        'pFSel = pFeatureLayer
        'Dim pFCursor As IFeatureCursor
        'fclas_tema = pFeatureLayer.FeatureClass
        'pFCursor = pFeatureLayer.Search(pQueryFilter, True)
        'pFeature = pFCursor.NextFeature
        'If pFSel.SelectionSet.Count = 0 Then
        '    MsgBox("No hay ninguna Selección")
        '    Exit Function
        'End If
        'Dim val_codigo As String
        'pFields = fclas_tema.Fields

        Dim pPoint As IPoint
        Dim pPoly As IPolygon
        Dim pPolyPointColl As IPointCollection
        Dim i As Integer
        Dim PSELE As ISelectionSet

        'Dim lostrZU As String = ""
        '-Dim pQF As IQueryFilter
        'pQFilter = New QueryFilter
        'pQFilter.WhereClause = "" ' to get all features
        Dim pFeatCursor As IFeatureCursor
        pFeatCursor = pOriginalLayer.FeatureClass.Search(pQueryFilter, True)
        Dim pFeat As IFeature
        pFeat = pFeatCursor.NextFeature
        Dim lostrClase As String = ""
        Dim loStrCoordenada As String
        Dim loTotPoligono As Integer = 0
        Dim loTotParcial As Integer = 0
        Dim loTotLleno As Integer = 0
        Dim loTotLibre As Integer = 0
        Dim lodtbSeleccion As New Data.DataTable
        loTotPoligono = 0
        Dim loCampo As String = ""
        'Dim pfeatureselecion As IFeatureSelection
        'pfeatureselecion = pOriginalLayer
        PSELE = pFeatureSelection.SelectionSet
        MsgBox(PSELE.Count)

        Do While Not pFeat Is Nothing

            loStrCoordenada = ""
            pPoly = pFeat.Shape
            pPolyPointColl = pPoly
            v_codigo = pFeat.Value(pFeatCursor.FindField("CODIGOU"))
            ' loop through all points in feature polygon
            For i = 0 To pPolyPointColl.PointCount - 1
                pPoint = pPolyPointColl.Point(i)
                loStrCoordenada = loStrCoordenada & pPoint.X & " " & pPoint.Y & ", "
            Next i  ' get next point in polygon
            loStrCoordenada = Mid(loStrCoordenada, 1, Len(loStrCoordenada) - 2)
            pFeat = pFeatCursor.NextFeature
        Loop
        ''Dim lodbtTotal = cls_oracle.FT_Int_Total(pTipo, loStrCoordenada, p_Zona, p_LayerInt, pastrCodInterceptado)
        'Dim lodbtExisteAR As New DataTable
        'Try
        '    lodbtExisteAR = cls_oracle.FT_Intersecta_Fclass_Oracle_1(8, "DATA_GIS.CATA_T", loStrCoordenada, v_codigo)
        'Catch ex As Exception
        '    'lodbtExisteAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(2, gstrFC_Catastro_Minero & gloZona, gstrFC_AReservada & gloZona, v_codigo, gstrUsuarioAcceso, gstrUsuarioClave, gstrDatabaseGIS)
        'End Try

    End Function
    Public Function ConxGis(ByVal USerID As String, ByVal Pass As String, ByVal DataSource As String) As String
        Return "User Id=" & USerID.Trim.ToUpper.ToString & ";Password=" & Pass.Trim.ToUpper.ToString & ";Data Source=" & DataSource & ";Connection Timeout=60;"

    End Function
    Public Function CalculaInterseccion_0(ByVal pTipo As String, ByVal pLayerIn As String, ByVal p_App As IApplication, ByVal p_Zona As String, _
            ByVal p_LayerInt As String, ByVal pastrCodInterceptado As String, ByVal lostrCodigo_clase_reserva As String)
        ' Dim codigo_sup As String
        ' Dim sup_cod_rese As String
        'Genera cuadriculas en la geodatabase personal
        Dim cls_oracle As New cls_Oracle
        Dim Consulta_Areas_rese As String
        pMxDoc = p_App.Document
        Dim pOriginalLayer As IFeatureLayer = Nothing
        Dim aFound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = pLayerIn Then
                pOriginalLayer = pMap.Layer(A)
                aFound = True
                Exit For
            End If
        Next A
        If Not aFound Then
            MsgBox("No existe el Layer: " & pLayerIn, MsgBoxStyle.Information, "GEOCATMIN")
            Return 0
            Exit Function
        End If
        Dim pPoint As IPoint
        Dim pPoly As IPolygon
        Dim pPolyPointColl As IPointCollection
        Dim i As Integer
        Dim lostrZU As String = ""
        '-Dim pQF As IQueryFilter
        pQFilter = New QueryFilter
        'pQFilter.WhereClause = "" ' to get all features
        'Consulta_Areas_rese = "CODIGO =  '" & pastrCodInterceptado & "' AND CLASE = '" & lostrCodigo_clase_reserva & "'"
        'pQFilter.WhereClause = Consulta_Areas_rese 'Selecciona area reservada y clase

        Dim pFeatCursor As IFeatureCursor
        'pFeatCursor = pOriginalLayer.FeatureClass.Search(pQFilter, False)
        pFeatCursor = pOriginalLayer.FeatureClass.Search(Nothing, True)
        Dim pFeat As IFeature
        pFeat = pFeatCursor.NextFeature
        Dim lostrClase As String = ""
        Dim loStrCoordenada As String
        Dim loTotPoligono As Integer = 0
        Dim loTotParcial As Integer = 0
        Dim loTotLleno As Integer = 0
        Dim loTotLibre As Integer = 0
        Dim lodtbSeleccion As New Data.DataTable
        loTotPoligono = 0
        Dim loCampo As String = ""
        'If pLayerIn = "ZonaUrbana_10Ha" Then
        If pLayerIn = "ZonaUrbana_10Ha_" & p_Zona Then

            loCampo = "URBANA"
            pTipo = 2
            Consulta_Areas_rese = "CODIGO =  '" & pastrCodInterceptado & "' AND CATEGORI = '" & lostrCodigo_clase_reserva & "'"
            pQFilter.WhereClause = Consulta_Areas_rese 'Selecciona area reservada y clase
        Else
            loCampo = "RESERVA"
            pTipo = 1
            Consulta_Areas_rese = "CODIGO =  '" & pastrCodInterceptado & "' AND CLASE = '" & lostrCodigo_clase_reserva & "'"
            pQFilter.WhereClause = Consulta_Areas_rese 'Selecciona area reservada y clase
        End If
        '-------------------
        pFeatureClass = pOriginalLayer.FeatureClass
        Dim pUpdateFeatures As IFeatureCursor
        'pUpdateFeatures = pFeatureClass.Update(pQFilter, False)
        pUpdateFeatures = pFeatureClass.Update(Nothing, True)
        Dim pFeature As IFeature
        pFeature = pUpdateFeatures.NextFeature
        Dim dRowInterno As DataRow
        '------------------
        Do While Not pFeat Is Nothing
            If loTotPoligono = 0 Then
                lodtbSeleccion.Columns.Add("CODIGO", Type.GetType("System.String"))
                lodtbSeleccion.Columns.Add(loCampo, Type.GetType("System.String"))
                lodtbSeleccion.Columns.Add("CLASE", Type.GetType("System.String"))
                lodtbSeleccion.Columns.Add("TIPO", Type.GetType("System.String"))
                lodtbSeleccion.Columns.Add("CUADRICULA", Type.GetType("System.Double"))
                'lodtbSeleccion.Columns.Add("POLIGONO", Type.GetType("System.Double"))
                lodtbSeleccion.Columns.Add("AREA", Type.GetType("System.Double"))
            End If
            loStrCoordenada = ""
            pPoly = pFeat.Shape
            pPolyPointColl = pPoly
            ' loop through all points in feature polygon
            For i = 0 To pPolyPointColl.PointCount - 1
                pPoint = pPolyPointColl.Point(i)
                loStrCoordenada = loStrCoordenada & pPoint.X & " " & pPoint.Y & ", "
            Next i  ' get next point in polygon
            loStrCoordenada = Mid(loStrCoordenada, 1, Len(loStrCoordenada) - 2)
            pFeat = pFeatCursor.NextFeature
            Dim lodbtParcial As New DataTable
            ' MsgBox(pastrCodInterceptado, MsgBoxStyle.Exclamation, "rese")
            Dim candea_ar As String = ""
            'Dim lodbtTotal = cls_oracle.FT_Int_Total(pTipo, loStrCoordenada, p_Zona, p_LayerInt, pastrCodInterceptado)
            Dim lodbtTotal = cls_oracle.P_Int_Total(pTipo, loStrCoordenada, p_Zona, p_LayerInt, pastrCodInterceptado, lostrCodigo_clase_reserva)
            'MsgBox(pFeature.Value(3))
            dRowInterno = lodtbdgdInterno.NewRow
            dRowInterno.Item("ITEM") = pFeature.Value(3)
            dRowInterno.Item("CODIGO") = pastrCodInterceptado

            If loCampo = "RESERVA" Then
                dRowInterno.Item("CLASE") = lostrCodigo_clase_reserva
            ElseIf loCampo = "URBANA" Then
                dRowInterno.Item("CATEGORIA") = lostrCodigo_clase_reserva
            End If
            dRowInterno.Item("CUADRICULA") = 1
            dRowInterno.Item("AREA") = 100
            'dRowInterno.Item("POLIGONO") = pFeature.Value(pFeature.Fields.FindField("POLIGONO"))

            '  MsgBox(pFeature.Value(pFeature.Fields.FindField("POLIGONO")))
            If lodbtTotal.Rows.Count > 0 Then
                pFeature.Value(pUpdateFeatures.FindField("TIPO")) = "Area Total"
                pFeature.Value(pUpdateFeatures.FindField("Area")) = "100"
                If loCampo = "RESERVA" Then
                    If pFeature.Value(pUpdateFeatures.FindField("RESERVA")).ToString = "" Then
                        ' If sup_cod_rese <> pastrCodInterceptado Then
                        pFeature.Value(pUpdateFeatures.FindField("RESERVA")) = pastrCodInterceptado
                        'End If
                        'pFeature.Value(pUpdateFeatures.FindField("RESERVA")) = pastrCodInterceptado
                        'candea_ar = pFeature.Value(pUpdateFeatures.FindField("RESERVA")).ToString

                    Else
                        ' If pastrCodInterceptado <> codigo_sup Then
                        candea_ar = pastrCodInterceptado
                        candea_ar = candea_ar & "," & pFeature.Value(pUpdateFeatures.FindField("RESERVA")).ToString
                        pFeature.Value(pUpdateFeatures.FindField("RESERVA")) = candea_ar
                        'codigo_sup = pastrCodInterceptado


                        'End If
                    End If
                End If


                loTotLleno = loTotLleno + 1
                dRowInterno.Item("TIPO") = "Area Total"

                If loCampo = "RESERVA" Then
                    dRowInterno.Item("CLASE") = lostrCodigo_clase_reserva
                ElseIf loCampo = "URBANA" Then
                    dRowInterno.Item("CATEGORIA") = lostrCodigo_clase_reserva
                End If

            Else
                'CASOS SUPERPOSICION PARCIAL
                ' lodbtParcial = cls_oracle.FT_Int_Parcial(pTipo, loStrCoordenada, p_Zona, p_LayerInt)
                lodbtParcial = cls_oracle.P_Int_Parcial(pTipo, loStrCoordenada, p_Zona, p_LayerInt, pastrCodInterceptado, lostrCodigo_clase_reserva)
                If lodbtParcial.Rows.Count > 0 Then
                    pFeature.Value(pUpdateFeatures.FindField("TIPO")) = "Area Parcial"
                    pFeature.Value(pUpdateFeatures.FindField("Area")) = "100"
                    If loCampo = "RESERVA" Then
                        If pFeature.Value(pUpdateFeatures.FindField("RESERVA")).ToString = "" Then
                            ' If sup_cod_rese <> pastrCodInterceptado Then
                            pFeature.Value(pUpdateFeatures.FindField("RESERVA")) = pastrCodInterceptado
                            'End If

                        Else
                            'If pastrCodInterceptado <> codigo_sup Then
                            candea_ar = pastrCodInterceptado
                            candea_ar = candea_ar & "," & pFeature.Value(pUpdateFeatures.FindField("RESERVA")).ToString
                            pFeature.Value(pUpdateFeatures.FindField("RESERVA")) = candea_ar
                            'codigo_sup = pastrCodInterceptado
                            'End If
                        End If
                    End If



                    loTotParcial = loTotParcial + 1
                    dRowInterno.Item("TIPO") = "Area Parcial"
                    If loCampo = "RESERVA" Then
                        dRowInterno.Item("CLASE") = lostrCodigo_clase_reserva
                    ElseIf loCampo = "URBANA" Then
                        dRowInterno.Item("CATEGORIA") = lostrCodigo_clase_reserva
                    End If

                End If
            End If
            If (lodbtTotal.Rows.Count = 0 And lodbtParcial.Rows.Count = 0) Then
                pFeature.Value(pUpdateFeatures.FindField("TIPO")) = "Area Libre"
                pFeature.Value(pUpdateFeatures.FindField("Area")) = "100"
                If loCampo = "RESERVA" Then

                    If pFeature.Value(pUpdateFeatures.FindField("RESERVA")).ToString = "" Then
                        'If sup_cod_rese <> pastrCodInterceptado Then
                        pFeature.Value(pUpdateFeatures.FindField("RESERVA")) = pastrCodInterceptado
                        'End If
                    Else
                        'If pastrCodInterceptado <> codigo_sup Then
                        candea_ar = pastrCodInterceptado
                        candea_ar = candea_ar & "," & pFeature.Value(pUpdateFeatures.FindField("RESERVA")).ToString
                        pFeature.Value(pUpdateFeatures.FindField("RESERVA")) = candea_ar
                        'codigo_sup = pastrCodInterceptado
                        'End If
                    End If
                End If

                ' pFeature.Value(pUpdateFeatures.FindField("RESERVA")) = candea_ar

                dRowInterno.Item("TIPO") = "Area Libre"
                If loCampo = "RESERVA" Then
                    dRowInterno.Item("CLASE") = lostrCodigo_clase_reserva
                ElseIf loCampo = "URBANA" Then
                    dRowInterno.Item("CATEGORIA") = lostrCodigo_clase_reserva
                End If

            End If
            loTotPoligono = loTotPoligono + 1

            pUpdateFeatures.UpdateFeature(pFeature)
            pFeature = pUpdateFeatures.NextFeature
            lodtbdgdInterno.Rows.Add(dRowInterno)
        Loop


        ' sup_cod_rese = pastrCodInterceptado
        'codigo_sup = pastrCodInterceptado
        loTotLibre = loTotPoligono - (loTotLleno + loTotParcial)
        Dim lointcontador As Integer = 1
        Dim dRow As DataRow
        If loTotLibre > 0 Then
            dRow = lodtbSeleccion.NewRow
            dRow.Item("CODIGO") = v_codigo : dRow.Item(loCampo) = pastrCodInterceptado : dRow.Item("TIPO") = "Area Libre"
            dRow.Item("CUADRICULA") = loTotLibre : dRow.Item("AREA") = loTotLibre * 100
            lodtbSeleccion.Rows.Add(dRow)
        End If
        If loTotParcial > 0 Then
            lointcontador = lointcontador + 1
            dRow = lodtbSeleccion.NewRow
            dRow.Item("CODIGO") = v_codigo : dRow.Item(loCampo) = pastrCodInterceptado : dRow.Item("TIPO") = "Area Parcial"
            dRow.Item("CUADRICULA") = loTotParcial : dRow.Item("AREA") = loTotParcial * 100
            lodtbSeleccion.Rows.Add(dRow)
        End If
        If loTotLleno > 0 Then
            lointcontador = lointcontador + 1
            dRow = lodtbSeleccion.NewRow
            dRow.Item("CODIGO") = v_codigo : dRow.Item(loCampo) = pastrCodInterceptado : dRow.Item("TIPO") = "Area Total"
            dRow.Item("CUADRICULA") = loTotLleno : dRow.Item("AREA") = loTotLleno * 100
            lodtbSeleccion.Rows.Add(dRow)
        End If
        Return lodtbSeleccion

    End Function


    Public Sub CONEXION_ORACLE(ByVal pastrTipo As String, ByVal pastrLayer1 As String, ByVal pastrLayer2 As String, ByVal pastrCodigo As String, ByRef dgdDato As Object)
        Dim ds As New DataSet
        cnx_Oracle.Open()
        ds.Clear()
        ds.EnforceConstraints = False
        Dim myCmd As New OracleCommand
        myCmd.Connection = cnx_Oracle
        myCmd.CommandText = "PACK_DBA_GIS.p_Int_Catastro"
        myCmd.CommandType = CommandType.StoredProcedure
        myCmd.Parameters.Add(New OracleParameter("v_tipo", OracleDbType.Varchar2)).Value = pastrTipo
        myCmd.Parameters.Add(New OracleParameter("v_layer_1", OracleDbType.Varchar2)).Value = pastrLayer1
        myCmd.Parameters.Add(New OracleParameter("v_layer_2", OracleDbType.Varchar2)).Value = pastrLayer2
        myCmd.Parameters.Add(New OracleParameter("v_codigo", OracleDbType.Varchar2)).Value = pastrCodigo
        myCmd.Parameters.Add(New OracleParameter("vo_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
        Dim myDa As New OracleDataAdapter(myCmd)
        Try
            myDa.Fill(ds)
            If ds.Tables.Count = 0 Then
                'MsgBox("nothing found")
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        ' Me.dgdDetalle.DataSource = ds.Tables(0)
        dgdDato.datasource = ds.Tables(0)
        cnx_Oracle.Close()
    End Sub

    Public Function Lista(ByVal pastrTipo As String, ByVal pastrLayer1 As String, ByVal pastrLayer2 As String, ByVal pastrCodigo As String) As DataTable
        Dim oCn As New OracleConnection(cnx_Oracle.ToString) ' cnx_oracle1
        Dim oCmd As New OracleCommand()
        Dim oDa As New OracleDataAdapter()
        Try
            oCmd.Connection = oCn
            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "PACK_DBA_GIS.p_Intersecta_Layers"
            oCmd.Parameters.Add(New OracleParameter("v_tipo", OracleDbType.Varchar2)).Value = pastrTipo
            oCmd.Parameters.Add(New OracleParameter("v_layer_1", OracleDbType.Varchar2)).Value = pastrLayer1
            oCmd.Parameters.Add(New OracleParameter("v_layer_2", OracleDbType.Varchar2)).Value = pastrLayer2
            oCmd.Parameters.Add(New OracleParameter("v_codigo", OracleDbType.Varchar2)).Value = pastrCodigo
            oCmd.Parameters.Add(New OracleParameter("vo_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            oDa.SelectCommand = oCmd
            Dim odt As New DataTable("Problemas")
            oDa.Fill(odt)
            Return odt
        Catch ex As Exception
            Throw New Exception("Error en Base de Datos " & ex.Message)
        End Try
    End Function
    Public Sub Poligono_Color_1(ByVal lo_FeatureClass As String, ByVal lo_Tabla As DataTable, ByVal loStyle As String, ByVal loCampo As String, ByVal p_Categoria As String, ByVal p_App As IApplication)
        Dim pUniqueValueRenderer As IUniqueValueRenderer
        pUniqueValueRenderer = New UniqueValueRenderer
        pUniqueValueRenderer.FieldCount = 1
        pUniqueValueRenderer.Field(0) = loCampo
        Dim pStyleStorage As IStyleGalleryStorage
        Dim pStyleGallery As IStyleGallery
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        pStyleGallery = New StyleGallery
        pStyleStorage = pStyleGallery
        Dim pEnumStyleGall As IEnumStyleGalleryItem
        Dim pStyleItem As IStyleGalleryItem
        Dim pFillSymbol As IFillSymbol = Nothing
        Dim pStyleGlry As IStyleGallery
        pStyleGlry = New StyleGallery
        Dim pStylStor As IStyleGalleryStorage
        pStylStor = pStyleGlry
        pStylStor.AddFile(loStyle)
        pEnumStyleGall = pStyleGallery.Items("Fill Symbols", loStyle, p_Categoria)
        pEnumStyleGall.Reset()
        pStyleItem = pEnumStyleGall.Next
        'Do Until pRow Is Nothing
        For i As Integer = 0 To lo_Tabla.Rows.Count - 1
            If pFillSymbol Is Nothing Then
                pEnumStyleGall.Reset()
                pStyleItem = pEnumStyleGall.Next
            End If
            Do While Not pStyleItem Is Nothing   'Loop through and access each marker
                pFillSymbol = Nothing
                If pStyleItem.Name = lo_Tabla.Rows(i).Item(loCampo) Then
                    'If pStyleItem.Name = pRow.Value(0) Then
                    pFillSymbol = pStyleItem.Item
                    Exit Do
                End If
                If pStyleItem.Name > lo_Tabla.Rows(i).Item(loCampo) Then
                Else
                    pStyleItem = pEnumStyleGall.Next
                End If
            Loop
            If pStyleItem Is Nothing Then
            Else
                'If Len(pStyleItem.Name) > 0 And Not (pRow Is Nothing) Then
                pUniqueValueRenderer.AddValue(lo_Tabla.Rows(i).Item(loCampo), "", pFillSymbol)
                Select Case pStyleItem.Name
                    Case "G1"
                        pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "D.M. en Trámite" 'Color Tramite
                    Case "G2"
                        pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "D.M. en Trámite D.L. 109" 'Color Tramite 109
                    Case "G3"
                        pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "D.M. Titulados" 'Color Titulado
                    Case "G4"
                        pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "D.M. Extinguido" 'Color Extinguido
                    Case "G5"
                        pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "D.M. - Otros" ' Color Planta, deposito
                    Case "G6"
                        pUniqueValueRenderer.Label(lo_Tabla.Rows(i).Item(loCampo)) = "DM_" & v_codigo
                End Select
            End If
        Next 'Loop
        Dim pLayer As IFeatureLayer = Nothing
        Dim pGeoFeatureLayer As IGeoFeatureLayer
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        Dim Afound As Boolean
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = lo_FeatureClass Then
                pLayer = pMap.Layer(A)
                Afound = True
                Exit For
            End If
        Next A
        If Not Afound Then
            MsgBox("No se encuentra el Layer")
            Exit Sub
        End If
        pGeoFeatureLayer = pLayer
        pGeoFeatureLayer.Renderer = pUniqueValueRenderer
        pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, Nothing)
        pMxDoc.UpdateContents()
    End Sub
    Function f_Genera_Leyenda_DM(ByVal p_Layer As String, ByVal lo_Filtro_DM As String, ByVal p_App As IApplication)
        Dim lo_Campo As String = ""
        Dim lodtOrdena As New DataTable
        Select Case p_Layer
            Case "AreaReserva_100Ha", "ZonaUrbana_10Ha"
                lo_Campo = "TIPO"
            Case Else
                lo_Campo = "CODI"
        End Select
        Dim pQueryFilter As IQueryFilter
        Dim lostrCadena As String = ""
        Dim lodtRegistros As New DataTable
        Try
            pMxDoc = p_App.Document
            pMap = pMxDoc.FocusMap
            Dim pLayer As ILayer = Nothing
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = p_Layer Then
                    pLayer = pMxDoc.FocusMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("Layer No Existe.")
                Return ""
                Exit Function
            End If
            'Dim pflayer As IFeatureLayer
            pFeatureLayer = pLayer
            Dim pLyr As IGeoFeatureLayer
            pLyr = pFeatureLayer
            'Dim pFeatureclass As IFeatureClass
            pFeatureClass = pFeatureLayer.FeatureClass
            pQueryFilter = New QueryFilter
            Dim pFeatCursor As IFeatureCursor
            pFeatCursor = pFeatureClass.Search(pQueryFilter, False)
            Dim rx As IRandomColorRamp
            rx = New RandomColorRamp
            Dim pRender As IUniqueValueRenderer, n As Long
            pRender = New UniqueValueRenderer
            Dim symd As ISimpleFillSymbol
            symd = New SimpleFillSymbol
            'Leyenda para la parte inicial de la leyenda (color Blanco)
            symd.Style = esriSimpleFillStyle.esriSFSSolid ' esriSFSSolid
            symd.Outline.Width = 0.4
            Dim myColor As IColor
            myColor = New RgbColor
            myColor.RGB = RGB(255, 255, 255)
            symd.Color = myColor
            pRender.FieldCount = 1
            pRender.Field(0) = lo_Campo
            pRender.DefaultSymbol = symd
            pRender.UseDefaultSymbol = True
            'Barriendo los registros
            Dim pFeat As IFeature
            n = pFeatureClass.FeatureCount(pQueryFilter)
            Dim i As Integer = 0
            Dim ValFound As Boolean
            Dim NoValFound As Boolean
            Dim uh As Integer
            Dim pFields As IFields
            Dim iField As Integer
            'Dim pField As IField
            pFields = pFeatureClass.Fields
            iField = pFields.FindField(lo_Campo)
            Do Until i = n
                Dim symx As ISimpleFillSymbol
                symx = New SimpleFillSymbol
                symx.Style = esriSimpleFillStyle.esriSFSForwardDiagonal ' esriSFSForwardDiagonal
                pFeat = pFeatCursor.NextFeature
                Dim X As String
                X = pFeat.Value(iField)
                If X <> "" Then
                    ValFound = False
                    For uh = 0 To (pRender.ValueCount - 1)
                        If pRender.Value(uh) = X Then
                            NoValFound = True
                            Exit For
                        End If
                    Next uh
                    If Not ValFound Then
                        pRender.AddValue(X, lo_Campo, symx)
                        pRender.Label(X) = X
                        pRender.Symbol(X) = symx
                    End If
                End If
                i = i + 1
            Loop
            rx.Size = pRender.ValueCount
            rx.CreateRamp(True)
            Dim RColors As IEnumColors, ny As Long
            RColors = rx.Colors
            RColors.Reset()
            For ny = 0 To (pRender.ValueCount - 1)
                'Dim xv As String = pRender.Value(ny)
                If ny = 0 Then lodtRegistros.Columns.Add(lo_Campo)
                Dim dr As DataRow
                dr = lodtRegistros.NewRow
                dr.Item(0) = pRender.Value(ny)
                lodtRegistros.Rows.Add(dr)
            Next
            Dim lodtvOrdena As New DataView(lodtRegistros, Nothing, lo_Campo & " ASC", DataViewRowState.CurrentRows)
            'MsgBox(lodtvOrdena.Item(0).Row(0))
            For i = 0 To lodtvOrdena.Count - 1
                If i = 0 Then lodtOrdena.Columns.Add(lo_Campo)
                Dim dr As DataRow
                dr = lodtOrdena.NewRow
                dr.Item(0) = lodtvOrdena.Item(i).Row(0)
                lodtOrdena.Rows.Add(dr)
            Next

        Catch ex As Exception
            ' MsgBox("No se realizo Distinct de Leyenda de " & p_Layer, MsgBoxStyle.Information, "Observacion  ")
        End Try
        Return lodtOrdena
    End Function
    Public Sub FT_Botones_EVA(ByVal lo_Layer As String, ByVal m_Application As IApplication)
        Dim pRow As IRow
        Dim lostrEVAPR As Integer = 0
        Dim lostrEVAPO As Integer = 0
        Dim lostrEVASI As Integer = 0
        Dim lostrEVAEX As Integer = 0
        Dim lostrEVARD As Integer = 0
        Dim lostrEVACO As Integer = 0
        Try
            glo_Tool_BT_5 = False 'ver Anterior, Posterior, Colindante
            glo_Tool_BT_6 = False 'Posterior
            glo_Tool_BT_7 = False 'Simultaneo
            glo_Tool_BT_8 = False 'Extinguido
            glo_Tool_BT_11 = False 'Colindante
            glo_Tool_BT_22 = False 'Anterior - Prioritaros
            glo_Tool_BT_23 = False 'Anterior y Posterior
            glo_Tool_BT_24 = False 'Redenuncio
            pMxDoc = m_Application.Document
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name = lo_Layer Then
                    pLayer = pMxDoc.FocusMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("Layer No Existe.")
                Exit Sub
            End If
            pTable = pLayer
            Dim pFeatureCursor As ICursor
            pFeatureCursor = pTable.Search(Nothing, False)
            pRow = pFeatureCursor.NextRow
            Do Until pRow Is Nothing
                Select Case pRow.Value(36)
                    Case "PR"
                        glo_Tool_BT_22 = True 'PR
                        lostrEVAPR = 1
                    Case "PO"
                        glo_Tool_BT_6 = True 'PO
                        lostrEVAPO = 1
                    Case "SI"
                        glo_Tool_BT_7 = True 'SI
                        lostrEVASI = 1
                    Case "EX"
                        glo_Tool_BT_8 = True 'EX
                        lostrEVAEX = 1
                    Case "RD"
                        glo_Tool_BT_24 = True 'RD
                        lostrEVARD = 1
                    Case "CO"
                        glo_Tool_BT_11 = True 'CO
                        lostrEVACO = 1
                End Select
                pRow = pFeatureCursor.NextRow
            Loop
            If lostrEVAPR = 1 And lostrEVAPO = 1 And lostrEVACO = 1 Then
                glo_Tool_BT_5 = True
            End If
            If lostrEVAPR = 1 And lostrEVAPO = 1 Then
                glo_Tool_BT_23 = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub Cargar_FeatureClass_SDE()
        Dim cls_Oracle As New cls_Oracle
        Dim ldtLayer As New DataTable
        'ldtLayer = cls_Oracle.F_Obtiene_Lista_Layer("1", "SIGGEOCATMIN_LAYERS")
        Try
            ldtLayer = cls_Oracle.FT_SEL_LISTA_DESCRIPCION("1", "SIGGCTMIN_FC")
            For r As Integer = 0 To ldtLayer.Rows.Count - 1
                Select Case ldtLayer.Rows(r).Item(0)
                    Case "CDIST"
                        gstrFC_CDistrito = ldtLayer.Rows(r).Item(1)
                    Case "DPTO"
                        gstrFC_Departamento = ldtLayer.Rows(r).Item(1)
                    Case "PROV"
                        gstrFC_Provincia = ldtLayer.Rows(r).Item(1)
                    Case "DIST"
                        gstrFC_Distrito = ldtLayer.Rows(r).Item(1)
                    Case "RIOS"
                        gstrFC_Rios = ldtLayer.Rows(r).Item(1)
                    Case "CARRETERA"
                        gstrFC_Carretera = ldtLayer.Rows(r).Item(1)
                    Case "CPOBLADO"
                        gstrFC_CPoblado = ldtLayer.Rows(r).Item(1)
                    Case "CMINERO"
                        gstrFC_Catastro_Minero = ldtLayer.Rows(r).Item(1)
                    Case "CUADRICULA"
                        gstrFC_Cuadricula = ldtLayer.Rows(r).Item(1)
                    Case "ARESERVADA"
                        gstrFC_AReservada = ldtLayer.Rows(r).Item(1)
                    Case "ZURBANA"
                        gstrFC_ZUrbana = ldtLayer.Rows(r).Item(1)
                    Case "ZURBANA56"
                        gstrFC_ZUrbana56 = ldtLayer.Rows(r).Item(1)
                    Case "ARESERVA56"
                        gstrFC_AReservada56 = ldtLayer.Rows(r).Item(1)
                    Case "ZTRASLAPE"
                        gstrFC_ZTraslape = ldtLayer.Rows(r).Item(1)
                    Case "FRONTERA"
                        gstrFC_Frontera = ldtLayer.Rows(r).Item(1)
                    Case "LHOJAS"
                        gstrFC_LHojas = ldtLayer.Rows(r).Item(1)
                    Case "CARTA"
                        gstrFC_Carta = ldtLayer.Rows(r).Item(1)
                End Select
            Next
        Catch ex As Exception
            MsgBox("Error de Asignación de Feature Class.... ", MsgBoxStyle.Information, "[BDGEOCATMIN]")
        End Try
    End Sub
    Public Sub UniqueSymbols(ByVal m_Application As IApplication)
        ' Part 1: Prepare a unique value renderer.
        Dim pUniqueValueRenderer As IUniqueValueRenderer
        Dim pSym1 As ILineSymbol
        ' Define the renderer.
        pUniqueValueRenderer = New UniqueValueRenderer
        pUniqueValueRenderer.FieldCount = 1
        pUniqueValueRenderer.Field(0) = "DEPARTA"
        ' Add the first symbol to the renderer.
        pSym1 = New SimpleLineSymbol
        pSym1.Color = GetRGBColor(0, 112, 255)
        pSym1.Width = 1
        pUniqueValueRenderer.AddValue("ANCASH", "", pSym1)
        ' Part 2: Draw the feature layer and refresh the table of contents.
        'Dim pMxDoc As IMxDocument
        'Dim pMap As IMap
        'Dim pLayer As IFeatureLayer
        Dim pGeoFeatureLayer As IGeoFeatureLayer
        pMxDoc = m_Application.Document 'ThisDocument
        pMap = pMxDoc.FocusMap
        pLayer = pMap.Layer(0)
        pGeoFeatureLayer = pLayer
        pGeoFeatureLayer.Renderer = pUniqueValueRenderer
        pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, Nothing)
        pMxDoc.UpdateContents()
    End Sub

    Private Function GetRGBColor(ByVal R As Long, ByVal G As Long, ByVal B As Long)
        Dim pColor As IRgbColor
        pColor = New RgbColor
        pColor.Red = R
        pColor.Green = G
        pColor.Blue = B
        GetRGBColor = pColor
    End Function



    ''Private Sub SetParam(ByVal lodtbAsigna As DataTable)
    ''    Dim f As Integer
    ''    For f = 0 To lodtbAsigna.Columns.Count - 1 '(f).ColumnName - 1 '.Rows.Count - 1 '.Fields.FieldCount - 1
    ''        If lodtbAsigna.Columns(f).ColumnName = "ESTE" Then '.Fields.Field(f).Name = "ESTE" Then 'DataXY.sEsteX Then
    ''            DataXY.iEsteX = f
    ''        ElseIf lodtbAsigna.Columns(f).ColumnName = "NORTE" Then 'DataXY.sNorteY Then
    ''            DataXY.iNorteY = f
    ''        ElseIf lodtbAsigna.Columns(f).ColumnName = "CODIGO" Then 'DataXY.sCodigoID Then
    ''            DataXY.iCodigoID = f
    ''        End If
    ''    Next f
    ''End Sub

    'se puso

    'Private DataXY As VerXY
    'Private Structure FieldData
    '    Dim Index As Integer
    '    Dim Campo As String
    'End Structure
    'Private DataInfo() As FieldData

    'Public Sub Genera_Circulo(ByVal lodtbDatos As DataTable, ByVal p_Zona As String, ByVal pApp As IApplication, Optional ByVal p_Flag As String = "")
    '    Dim z As Integer = 0
    '    Dim Poligono_Ok As String = ""
    '    Dim pArea As IArea
    '    Dim pStatusBar As ESRI.ArcGIS.esriSystem.IStatusBar
    '    Dim pProgbar As ESRI.ArcGIS.esriSystem.IStepProgressor
    '    Dim vCodigo, vCodigoSalvado As String
    '    Dim vEste, vNorte As Double
    '    Dim pFCLass As IFeatureClass = Open_Feature_GDB(pApp, "", "POLIGONO_" & v_Zona)
    '    Dim pEditor As IEditor = ActivarEditor(pApp, pFCLass)
    '    Dim Conta As Integer = 0
    '    Dim Vertices(200) As Point
    '    Dim vEstado As String = ""
    '    Dim pFeatureDM As IFeature
    '    pFeatureDM = pFCLass.CreateFeature
    '    Dim contador As Integer = 1
    '    SetParam(lodtbDatos)
    '    Dim FieldDataCount As Integer = lodtbDatos.Columns.Count ' - 4
    '    ReDim DataInfo(FieldDataCount)
    '    Dim n As Integer = 0
    '    pStatusBar = pApp.StatusBar
    '    pProgbar = pStatusBar.ProgressBar
    '    pProgbar.Position = 0
    '    vCodigoSalvado = lodtbDatos.Rows(0).Item(DataXY.iCodigoID)
    '    vEste = lodtbDatos.Rows(0).Item(DataXY.iEsteX)
    '    vNorte = lodtbDatos.Rows(0).Item(DataXY.iNorteY)
    '    Vertices(0) = New Point
    '    Vertices(0).PutCoords(vEste, vNorte)
    '    Try
    '        For i As Integer = 0 To lodtbDatos.Rows.Count - 1
    '            pStatusBar.ShowProgressBar("Processing...", 0, lodtbDatos.Rows.Count, 1, True)
    '            vEste = lodtbDatos.Rows(i).Item(DataXY.iEsteX)
    '            vNorte = lodtbDatos.Rows(i).Item(DataXY.iNorteY)
    '            vCodigo = lodtbDatos.Rows(i).Item(DataXY.iCodigoID)
    '            ''********************
    '            If vCodigo <> vCodigoSalvado Then 'si es diferente Grabar los datos
    '                pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta)
    '                pArea = pFeatureDM.Shape
    '                If p_Flag <> "1" Then
    '                    Poligono_Ok = Verifica_Existencia_XY("Catastro_1", pArea.Centroid.X, pArea.Centroid.Y, p_Zona, pApp)
    '                End If
    '                Select Case Poligono_Ok
    '                    Case ""
    '                        pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = vCodigoSalvado
    '                        pFeatureDM.Store()
    '                    Case "Si"
    '                        z = z + 1
    '                        If z = 188 Then
    '                        End If
    '                        pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "DM_1"
    '                        pFeatureDM.Value(pFeatureDM.Fields.FindField("POLIGONO")) = z
    '                        pFeatureDM.Store()
    '                    Case "No"
    '                        pFeatureDM.Delete()
    '                End Select
    '                vCodigoSalvado = vCodigo
    '                Conta = 0
    '                Vertices(Conta) = New Point
    '                Vertices(Conta).PutCoords(vEste, vNorte)
    '                vCodigoSalvado = lodtbDatos.Rows(i).Item(DataXY.iCodigoID)
    '                pFeatureDM = pFCLass.CreateFeature
    '                contador = contador + 1
    '            Else
    '                Vertices(Conta) = New Point
    '                Vertices(Conta).PutCoords(vEste, vNorte)
    '            End If
    '            Conta = Conta + 1
    '            pStatusBar.StepProgressBar()
    '        Next i
    '        pFeatureDM.Shape = AdicionarConcesion2(pFCLass, Vertices, Conta) 'pPolygon
    '        pArea = pFeatureDM.Shape
    '        If p_Flag <> "1" Then
    '            Poligono_Ok = Verifica_Existencia_XY("Catastro_1", pArea.Centroid.X, pArea.Centroid.Y, p_Zona, pApp)
    '        End If
    '        Select Case Poligono_Ok
    '            Case ""
    '                pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = vCodigoSalvado '"DM_" & z + 1
    '                pFeatureDM.Store()
    '            Case "Si"
    '                If pArea.Area <> 0 Then
    '                Else
    '                    MsgBox("No creo el Poligono, revisar coordenadas y ejecutar nuevamente", MsgBoxStyle.Information, "[BDGEOCATMIN]")
    '                    pFeatureDM.Delete()
    '                    pEditor.StopOperation("Delete Polygon")
    '                    pEditor.StopEditing(True)
    '                    Exit Sub
    '                End If
    '                pFeatureDM.Value(pFeatureDM.Fields.FindField("CODIGOU")) = "DM_1" ' lodtbDatos.Rows(i).Item(DataXY.iCodigoID) ' pRow.Value(DataInfo(L + 1).Index)
    '                pFeatureDM.Store()
    '            Case "No"
    '                pFeatureDM.Delete()
    '        End Select
    '        pEditor.StopOperation("Add Polygon")
    '        pEditor.StopEditing(True)
    '        pStatusBar.HideProgressBar()
    '        pStatusBar.Message(0) = "Operación Completado"

    '        pFeatureDM = Nothing
    '        pFCLass = Nothing
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try
    '    '**************** 
    'End Sub
    'Private Function AdicionarConcesion2(ByRef pFCLass As IFeatureClass, ByRef Vertices() As Point, ByVal Conta As Short) As IPolygon
    '    Dim i As Integer
    '    Dim pRings(40) As IRing  'Maximo 40 anillos
    '    Dim pPolygon As IPolygon = New ESRI.ArcGIS.Geometry.PolygonClass
    '    Dim P As Integer, Parts As Integer = 0, SubPolygono As Integer = 0
    '    Dim Puntos As Point
    '    Dim pPointCollection As IPointCollection = New RingClass()
    '    Puntos = New Point
    '    Puntos.PutCoords(Vertices(0).X, Vertices(0).Y)
    '    pPointCollection.AddPoint(Puntos)
    '    For i = 1 To Conta - 1
    '        SubPolygono = NuevoSubPolygono(Vertices, Vertices(i), i - 1)
    '        Puntos = New Point
    '        Puntos.PutCoords(Vertices(i).X, Vertices(i).Y)
    '        pPointCollection.AddPoint(Puntos)
    '        If SubPolygono <> -1 Then
    '            pRings(Parts) = pPointCollection
    '            Parts = Parts + 1
    '            pPointCollection = New RingClass()
    '        End If
    '    Next
    '    If Parts <> 0 Then 'Poligono con anillos
    '        Dim pGeometryCollection As IGeometryCollection
    '        pGeometryCollection = New Polygon
    '        For P = 0 To Parts - 1
    '            pGeometryCollection.AddGeometry(pRings(P))
    '            'Debug.Print("------>" + Parts.ToString)
    '        Next
    '        pPolygon = pGeometryCollection
    '    Else ' Poligono Normal
    '        pPointCollection = New ESRI.ArcGIS.Geometry.PolygonClass 'spPolygon
    '        For i = 0 To Conta - 1
    '            Puntos = New Point
    '            Puntos.PutCoords(Vertices(i).X, Vertices(i).Y)
    '            pPointCollection.AddPoint(Puntos)
    '        Next
    '        Puntos = New Point
    '        Puntos.PutCoords(Vertices(0).X, Vertices(0).Y)
    '        pPointCollection.AddPoint(Puntos)
    '        pPolygon = pPointCollection
    '    End If
    '    AdicionarConcesion2 = pPolygon
    'End Function
    'Private Function NuevoSubPolygono(ByRef ChekearVertice() As Point, ByRef punto As Point, ByVal actual As Short) As Short
    '    Dim j As Short, Resultado As Short
    '    Resultado = -1
    '    For j = 0 To actual
    '        If punto.X = ChekearVertice(j).X And punto.Y = ChekearVertice(j).Y Then
    '            Resultado = actual + 2
    '            Exit For
    '        End If
    '    Next
    '    NuevoSubPolygono = Resultado
    'End Function
    'Function Verifica_Existencia_XY(ByVal p_Layer As String, ByVal p_x As Double, ByVal p_y As Double, ByVal p_Zona As String, ByVal pApp As IApplication)
    '    'Dim temp As Double
    '    Dim pPoint As IPoint
    '    pPoint = New Point
    '    Dim pFLayer As IFeatureLayer = Nothing
    '    pMxDoc = pApp.Document
    '    pMap = pMxDoc.FocusMap
    '    Dim afound As Boolean
    '    For A As Integer = 0 To pMap.LayerCount - 1
    '        If pMap.Layer(A).Name = p_Layer Then
    '            pFLayer = pMxDoc.FocusMap.Layer(A)
    '            afound = True
    '            Exit For
    '        End If
    '    Next A
    '    If Not afound Then
    '        MsgBox("Layer No Existe.") : Return "No" : Exit Function
    '    End If
    '    pPoint.X = p_x : pPoint.Y = p_y
    '    Dim pSpatialFilter As ISpatialFilter
    '    pSpatialFilter = New SpatialFilter
    '    With pSpatialFilter
    '        .Geometry = pPoint
    '        .SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects ' esriSpatialRelIntersects
    '    End With
    '    Dim pFeatureCursor As IFeatureCursor = pFLayer.Search(pSpatialFilter, True)
    '    Dim pRow As IRow = pFeatureCursor.NextFeature
    '    If pRow Is Nothing Then
    '        Return "No"
    '        'MsgBox("Las coordenadas Este, Norte, ó la Zona están fuera del Límite")
    '    End If
    '    Return "Si"
    'End Function
    'Public Function Open_Feature_GDB(ByRef m_application As IApplication, ByRef PathDestino As String, ByVal mFeatureClass As String) As IFeatureClass
    '    Dim pFeatureClass As IFeatureClass
    '    Dim pFeatureLayer As IFeatureLayer
    '    Dim pFeatureWorkspace As IFeatureWorkspace
    '    Dim pWorkspaceFactory As IWorkspaceFactory
    '    pMxDoc = m_application.Document
    '    pMap = pMxDoc.FocusMap
    '    pWorkspaceFactory = New AccessWorkspaceFactory 'ShapefileWorkspaceFactory
    '    pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_PathGDB, 0)
    '    pFeatureClass = pFeatureWorkspace.OpenFeatureClass(mFeatureClass)
    '    pFeatureLayer = New FeatureLayerClass
    '    pFeatureLayer.FeatureClass = pFeatureClass
    '    pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName
    '    pMap.AddLayer(pFeatureLayer)
    '    pMxDoc.ActiveView.Refresh()
    '    Open_Feature_GDB = pFeatureClass
    'End Function
    'Private Function ActivarEditor(ByRef m_application As IApplication, ByRef pFCLass As IFeatureClass) As IEditor
    '    Dim pWorkspace As IWorkspace
    '    Dim pEditor As IEditor
    '    Dim pID As New UID
    '    Dim pDataset As IDataset
    '    pID.Value = "esriCore.Editor"
    '    pEditor = m_application.FindExtensionByCLSID(pID)
    '    pDataset = pFCLass
    '    pWorkspace = pDataset.Workspace
    '    pEditor.StartEditing(pWorkspace)
    '    pEditor.StartOperation()
    '    ActivarEditor = pEditor
    'End Function
    'Private Sub SetParam(ByVal lodtbAsigna As DataTable)
    '    Dim f As Integer
    '    For f = 0 To lodtbAsigna.Columns.Count - 1
    '        If lodtbAsigna.Columns(f).ColumnName = "ESTE" Then
    '            DataXY.iEsteX = f
    '        ElseIf lodtbAsigna.Columns(f).ColumnName = "NORTE" Then 'DataXY.sNorteY Then
    '            DataXY.iNorteY = f
    '        ElseIf lodtbAsigna.Columns(f).ColumnName = "CODIGO" Then 'DataXY.sCodigoID Then
    '            DataXY.iCodigoID = f
    '        End If
    '    Next f
    'End Sub

    Public Sub Genera_Linea(ByVal lodtbTabla As DataTable, ByVal lo_Zona As String, ByVal m_Application As IApplication)
        Dim directorio As IWorkspaceFactory
        Dim tema As IFeatureLayer
        Dim fclas_tema As IFeatureClass
        Dim carpeta As IWorkspace
        'Dim pfeature As IFeature
        Dim pPoint(10, 10) As IPoint
        Try
            pMxDoc = m_Application.Document
            pMap = pMxDoc.FocusMap
            Dim afound As Boolean = False
            For A As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(A).Name.ToUpper = "LINEA" Then
                    tema = pMap.Layer(A)
                    afound = True
                    Exit For
                End If
            Next A
            If Not afound Then
                MsgBox("No se encuentra el Layer")
                Exit Sub
            End If
            directorio = New AccessWorkspaceFactory
            carpeta = pWorkspaceFactory.OpenFromFile(glo_PathGDB, 0)
            Dim pID As New UID
            Dim pEditor As IEditor
            fclas_tema = tema.FeatureClass
            pID.Value = "esriCore.Editor"
            pEditor = m_Application.FindExtensionByCLSID(pID)
            'Objeto de edicion del tema 
            Dim pdataset As IDataset
            pdataset = fclas_tema
            ' Esto asignara el mismo directorio del tema 
            carpeta = pdataset.Workspace
            pEditor.StartEditing(carpeta)
            pEditor.StartOperation()
            'Declarando la coleccion de puntos 
            Dim pPointColl As IPointCollection
            Dim pLine As ILine
            Dim pGeometryColl As IGeometryCollection 'Objeto de tipo de geometria del tema.
            Dim pSegmentColl As ISegmentCollection 'Objeto de segmento de coleccion.
            pID.Value = "esriCore.Editor"
            pEditor = m_Application.FindExtensionByCLSID(pID)
            For i As Integer = 0 To lodtbTabla.Rows.Count - 2
                pfeature = fclas_tema.CreateFeature
                pPointColl = New Polyline ' Definiendo el tipo de geometria 
                pGeometryColl = New Polyline 'Asignando 
                pPoint(i, 0) = New ESRI.ArcGIS.Geometry.Point
                pPoint(i, 1) = New ESRI.ArcGIS.Geometry.Point
                pPoint(i, 0).PutCoords(lodtbTabla.Rows(i).Item("CD_COREST"), lodtbTabla.Rows(i).Item("CD_CORNOR"))
                pPoint(i, 1).PutCoords(lodtbTabla.Rows(i + 1).Item("CD_COREST"), lodtbTabla.Rows(i + 1).Item("CD_CORNOR"))
                pSegmentColl = New ESRI.ArcGIS.Geometry.Path 'Generando segmento de linea 
                pLine = New Line
                pLine.PutCoords(pPoint(i, 0), pPoint(i, 1))
                pSegmentColl.AddSegment(pLine)
                'Añadiendo la linea 
                pGeometryColl.AddGeometry(pSegmentColl)
                pfeature.Shape = pGeometryColl
                pfeature.Store()
            Next i
            pEditor.StopOperation("Add Line")
            pEditor.StopEditing(True)
            pMxDoc.ActivatedView.Refresh()
        Catch ex As Exception
            MsgBox("Generación de Línea falló", MsgBoxStyle.Information, "GisCatMin")
        End Try
    End Sub



End Class


