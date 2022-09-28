Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Display
Imports Oracle.DataAccess.Client
Imports PORTAL_Clases
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem
Imports stdole

Public Class cls_wurbina
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
            dRow.Item("CUADRICULA") = loTotLibre : dRow.Item("AREA") = loTotLibre * 10
            lodtbSeleccion.Rows.Add(dRow)
            lointcontador = lointcontador + 1
        End If
        If loTotParcial > 0 Then
            dRow = lodtbSeleccion.NewRow
            dRow.Item("CODIGO") = v_codigo : dRow.Item(loCampo) = pastrCodInterceptado : dRow.Item("TIPO") = "Area Parcial"
            dRow.Item("CUADRICULA") = loTotParcial : dRow.Item("AREA") = loTotParcial * 10
            lodtbSeleccion.Rows.Add(dRow)
            lointcontador = lointcontador + 1
        End If
        If loTotLleno > 0 Then
            dRow = lodtbSeleccion.NewRow
            dRow.Item("CODIGO") = v_codigo : dRow.Item(loCampo) = pastrCodInterceptado : dRow.Item("TIPO") = "Area Total"
            dRow.Item("CUADRICULA") = loTotLleno : dRow.Item("AREA") = loTotLleno * 10
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
            MsgBox("No existe el Layer: " & pLayerIn, MsgBoxStyle.Information, "[GEOCATMIN]")
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
        Dim dRowInterno As DataRow
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
            'MsgBox(pFeature.Value(3))
            dRowInterno = lodtbdgdInterno.NewRow
            dRowInterno.Item("ITEM") = pFeature.Value(3)
            dRowInterno.Item("CODIGO") = pastrCodInterceptado
            dRowInterno.Item("CUADRICULA") = 1
            dRowInterno.Item("AREA") = 100
            If lodbtTotal.Rows.Count > 0 Then
                pFeature.Value(pUpdateFeatures.FindField("TIPO")) = "Area Total"
                pFeature.Value(pUpdateFeatures.FindField("Area")) = "100"
                loTotLleno = loTotLleno + 1
                dRowInterno.Item("TIPO") = "Area Total"
            Else
                lodbtParcial = cls_oracle.FT_Int_Parcial(pTipo, loStrCoordenada, p_Zona, p_LayerInt)
                If lodbtParcial.Rows.Count > 0 Then
                    pFeature.Value(pUpdateFeatures.FindField("TIPO")) = "Area Parcial"
                    pFeature.Value(pUpdateFeatures.FindField("Area")) = "100"
                    loTotParcial = loTotParcial + 1
                    dRowInterno.Item("TIPO") = "Area Parcial"
                End If
            End If
            If (lodbtTotal.Rows.Count = 0 And lodbtParcial.Rows.Count = 0) Then
                pFeature.Value(pUpdateFeatures.FindField("TIPO")) = "Area Libre"
                pFeature.Value(pUpdateFeatures.FindField("Area")) = "100"

                dRowInterno.Item("TIPO") = "Area Libre"
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
End Class
