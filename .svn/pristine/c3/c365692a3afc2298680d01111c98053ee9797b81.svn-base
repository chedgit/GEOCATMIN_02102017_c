Imports System.Drawing
Imports PORTAL_Clases
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Framework

Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem
Public Class Frm_Demarca_Urbana
    Public m_Application As IApplication
    Private cls_Catastro As New cls_DM_1
    Private cls_DM_2 As New cls_DM_2
    Private lodtbLista_DM As New DataTable
    Private Const Col_Num = 0
    Private Const Col_ZU = 1
    Private Const Col_Cod = 2
    Private Const Col_Nom = 3
    Private Const Col_Cla = 4
    Private Const Col_Tip = 5
    Private Const Col_Are = 6


    Private Sub Frm_Demarca_Urbana_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Generar_Zona_Urbana()
    End Sub
    Private Sub Generar_Zona_Urbana()
        'Dim lo_step As Integer
        Dim cls_utulidades As New cls_Utilidades
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
        Dim lostrCodigo As String

        PT_Inicializar_Grilla_Distrito()
        Dim lodbtExisteAR As New DataTable
        Dim cod_urba As String
        Dim area As Double
        Dim nm_dist As String
        Dim nm_prov As String
        Dim nm_depa As String
        Try
            For contador1 As Integer = 1 To colecciones_codurba.Count
                cod_urba = colecciones_codurba.Item(contador1)
                lodbtExisteAR = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(26, gstrFC_ZUrbana & v_zona_dm, gstrFC_Distrito, cod_urba)
            Next contador1
        Catch ex As Exception
        End Try

        Dim contador As Integer
        If lodbtExisteAR.Rows.Count >= 1 Then
            For contador = 0 To lodbtExisteAR.Rows.Count - 1
                lostrCodigoInterceptado = lodbtExisteAR.Rows(contador).Item("CD_DIST")
                If contador = 0 Then
                    Consulta_Areas_rese = "CD_DIST =  '" & lostrCodigoInterceptado & "'"
                    lista_nm_depa = Consulta_Areas_rese
                Else
                    Consulta_Areas_rese = Consulta_Areas_rese & " OR " & "CD_DIST =  '" & lostrCodigoInterceptado & "'"
                    lista_nm_depa = Consulta_Areas_rese
                End If
            Next contador
        End If

        Dim cls_eval As New Cls_evaluacion
        cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Distrito, m_Application, "1", False)
        cls_eval.DefinitionExpressiontema(lista_nm_depa, m_Application, "Distrito")
        'cls_Catastro.Shade_Poligono("Distrito", m_Application)
        cls_Catastro.Color_Poligono_Simple(m_Application, "Distrito")
        Dim lodtTabla As New DataTable
        odt.Columns.Add("NUMERO", Type.GetType("System.Double"))
        odt.Columns.Add("COD_ZU", Type.GetType("System.String"))
        odt.Columns.Add("CODIGO", Type.GetType("System.String"))
        odt.Columns.Add("DISTRITO", Type.GetType("System.String"))
        odt.Columns.Add("AREAINT", Type.GetType("System.String"))
        odt.Columns.Add("PROVINCIA", Type.GetType("System.String"))
        odt.Columns.Add("DEPARTAMENTO", Type.GetType("System.String"))

        Dim dRow As DataRow
        Dim lo_valor_Area As Integer = 0
        For contador = 0 To lodbtExisteAR.Rows.Count - 1
            lostrCodigoInterceptado = lodbtExisteAR.Rows(contador).Item("CD_DIST")
            lostrCodigo = lodbtExisteAR.Rows(contador).Item("CODIGO")
            area = lodbtExisteAR.Rows(contador).Item("AREAINT")
            nm_dist = lodbtExisteAR.Rows(contador).Item("NM_DIST")
            nm_prov = lodbtExisteAR.Rows(contador).Item("NM_PROV")
            nm_depa = lodbtExisteAR.Rows(contador).Item("NM_DEPA")
            
            lo_valor_Area = lo_valor_Area + 1
            dRow = odt.NewRow
            dRow.Item("NUMERO") = lo_valor_Area
            dRow.Item("COD_ZU") = lostrCodigo
            dRow.Item("CODIGO") = lostrCodigoInterceptado
            dRow.Item("DISTRITO") = nm_dist
            dRow.Item("PROVINCIA") = nm_prov
            dRow.Item("DEPARTAMENTO") = nm_depa
            dRow.Item("AREAINT") = area
            odt.Rows.Add(dRow)

        Next contador

        dgdDetalle.DataSource = odt
        
    End Sub

    Public Sub PT_Inicializar_Grilla_Distrito()
        lodtbLista_DM.Columns.Add("NUMERO", GetType(String))
        lodtbLista_DM.Columns.Add("COD_ZU", GetType(String))
        lodtbLista_DM.Columns.Add("CODIGO", GetType(String))
        lodtbLista_DM.Columns.Add("DISTRITO", GetType(String))
        lodtbLista_DM.Columns.Add("PROVINCIA", GetType(String))
        lodtbLista_DM.Columns.Add("DEPARTAMENTO", GetType(String))
        lodtbLista_DM.Columns.Add("AREAINT", GetType(String))
        PT_Estilo_Grilla_Distrito(lodtbLista_DM) : PT_Cargar_Grilla_distrito(lodtbLista_DM)
        PT_Agregar_Funciones_Distrito() : PT_Forma_Grilla_Distrito()
    End Sub

    Private Sub PT_Estilo_Grilla_Distrito(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(Col_Num).DefaultValue = 0
        padtbDetalle.Columns.Item(Col_ZU).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Cod).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Nom).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Cla).DefaultValue = 0
        padtbDetalle.Columns.Item(Col_Tip).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Are).DefaultValue = ""

    End Sub

    Private Sub PT_Cargar_Grilla_distrito(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdDetalle.DataSource = dvwDetalle
        Pinta_Grilla_dgdDetalle_1()
    End Sub
    
    Private Sub PT_Agregar_Funciones_Distrito()
        Me.dgdDetalle.Columns(Col_Num).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_ZU).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Cod).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Nom).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Cla).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Tip).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Are).DefaultValue = ""

    End Sub
    Public Sub Pinta_Grilla_dgdDetalle_1()
        Me.dgdDetalle.BackColor = Color.FromArgb(242, 242, 240)
        Me.dgdDetalle.HeadingStyle.BackColor = Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
        Me.dgdDetalle.OddRowStyle.BackColor = Color.FromArgb(229, 232, 239) 'E5E8EF
        Me.dgdDetalle.EvenRowStyle.BackColor = Color.FromArgb(242, 242, 240)
    End Sub
    
    Private Sub PT_Forma_Grilla_Distrito()
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Num).Width = 10
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_ZU).Width = 10
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cod).Width = 10
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nom).Width = 120
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Are).Width = 60
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cla).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Tip).Width = 80


        Me.dgdDetalle.Columns("NUMERO").Caption = "Número"
        Me.dgdDetalle.Columns("COD_ZU").Caption = "Cód_zu"
        Me.dgdDetalle.Columns("CODIGO").Caption = "Código"
        Me.dgdDetalle.Columns("DISTRITO").Caption = "Distrito"
        Me.dgdDetalle.Columns("AREAINT").Caption = "AreaInt"
        Me.dgdDetalle.Columns("PROVINCIA").Caption = "Provincia"
        Me.dgdDetalle.Columns("DEPARTAMENTO").Caption = "Departamento"

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Num).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_ZU).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cod).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nom).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cla).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Tip).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Are).Locked = True
        Me.dgdDetalle.Splits(0).HeadingStyle.ForeColor = Color.Black
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Num).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_ZU).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cod).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cla).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Tip).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Are).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        'Me.dgdDetalle.Splits(0).DisplayColumns(Col_Num).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        'Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cod).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        'Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nom).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        'Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cla).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        'Me.dgdDetalle.Splits(0).DisplayColumns(Col_Tip).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        'Me.dgdDetalle.Splits(0).DisplayColumns(Col_Are).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

        
    End Sub



    Private Sub btnagregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnagregar.Click
        Dim pActiveView As IActiveView
        Dim pFeatLayer As IFeatureLayer = Nothing
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        Dim afound As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Distrito" Then
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
        Dim pQueryFilter As IQueryFilter
        pFeatureSelection = pFeatLayer

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
        pCmdItem = m_Application.Document.CommandBars.Find(pUID)
        pCmdItem.Execute()
        pMxDoc.ActiveView.Refresh()
    End Sub

    Private Sub dgdDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgdDetalle.Click

    End Sub

    Private Sub dgdDetalle_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles dgdDetalle.RowColChange
        Dim pFeatLayer As IFeatureLayer
        Dim b As Integer
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Distrito" Then
                pFeatLayer = pMxDoc.FocusMap.Layer(A)
                b = A
                Exit For
            End If
        Next A
        pFeatLayer = pMxDoc.FocusMap.Layer(b)
        If pFeatLayer.Name = "Distrito" Then
            Dim lodtRegistro As New DataTable
            cls_Catastro.Seleccionar_Items_x_Codigo_dist("CD_DIST = '" & dgdDetalle.Item(dgdDetalle.Row, "CODIGO") & "'", m_Application, "Distrito")
            pMxDoc.ActiveView.Refresh()
        End If
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()

    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

        Dim v_codigo_dist As String = ""
        Dim v_area As String = ""
        Dim val_codigo As String = ""
        
        Dim lostrRetorno As String = "", lostrCgCodigo As String = "", lostrArea As String = "", lostrZU As String = "", lostrtipo As String = ""
        Dim w As Integer = 0
        'val_codigo = "010174809"
        'lostrCgCodigo = "ZU150204"
        'v_codigo_dist = dgdDetalle.Item(dgdDetalle.Row, "CODIGO")
        'lostrZU = "ZP"
        'lostrtipo = "ZU"
        'v_codigo_dist = v_codigo_dist & (w + 1 & " | " & dgdDetalle.Item(w, "CODIGO") & " || ")
        v_codigo_dist = v_codigo_dist & " | " & (dgdDetalle.Item(dgdDetalle.Row, "CODIGO") & " || ")
        lostrCgCodigo = lostrCgCodigo & " | " & (dgdDetalle.Item(dgdDetalle.Row, "COD_ZU") & " || ")
        'lostrCgCodigo = (w + 1 & " | " & lostrCgCodigo & " || ")
        'v_area = v_area & (w + 1 & " | " & dgdDetalle.Item(w, "AREAINT") & " || ")
        v_area = v_area & " | " & (dgdDetalle.Item(dgdDetalle.Row, "AREAINT") & " || ")

        'lostrZU = lostrZU & w + 1 & " | " & "ZP" & " || "
        lostrZU = lostrZU & " | " & "ZP" & " || "
        'lostrtipo = lostrtipo & w + 1 & " | " & "ZU" & " || "
        lostrtipo = lostrtipo & " | " & "ZU" & " || "

        Dim cls_oracle As New cls_Oracle

        lostrRetorno = cls_oracle.FT_SG_D_CARAC_EVALGIS("INS", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), v_codigo_dist, lostrZU, _
               "", v_area, lostrtipo, gstrUsuarioAcceso)

        If lostrRetorno = 0 Then
            v_codigo_dist = dgdDetalle.Item(dgdDetalle.Row, "CODIGO")
            lostrZU = "ZP"
            lostrRetorno = cls_oracle.FT_SG_D_CARAC_EVALGIS("DEL", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), v_codigo_dist, lostrZU, _
                  "", "", "", "")
            v_codigo_dist = ""
            lostrCgCodigo = ""
            v_area = ""
            lostrZU = ""

            v_codigo_dist = v_codigo_dist & " | " & (dgdDetalle.Item(dgdDetalle.Row, "CODIGO") & " || ")
            lostrCgCodigo = lostrCgCodigo & " | " & (dgdDetalle.Item(dgdDetalle.Row, "COD_ZU") & " || ")
            v_area = v_area & " | " & (dgdDetalle.Item(dgdDetalle.Row, "AREAINT") & " || ")
            lostrZU = lostrZU & " | " & "ZP" & " || "
            lostrtipo = lostrtipo & " | " & "ZU" & " || "
            'v_codigo_dist = v_codigo_dist & (w + 1 & " | " & dgdDetalle.Item(w, "CODIGO") & " || ")
            'lostrCgCodigo = lostrCgCodigo & (w + 1 & " | " & dgdDetalle.Item(w, "COD_ZU") & " || ")
            'v_area = v_area & (w + 1 & " | " & dgdDetalle.Item(w, "AREAINT") & " || ")
            'lostrZU = lostrZU & w + 1 & " | " & "ZP" & " || "

            lostrRetorno = cls_oracle.FT_SG_D_CARAC_EVALGIS("ACT", v_codigo, IIf(glo_InformeDM = "", "1", glo_InformeDM), v_codigo_dist, lostrZU, _
               "", v_area, lostrtipo, gstrUsuarioAcceso)
        End If


    End Sub
End Class