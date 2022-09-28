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

Public Class cls_Dominio
    Private cls_Oracle As New cls_Oracle
    Public Sub Conexion_GeoDatabase()
        pWorkspaceFactory = New AccessWorkspaceFactory
        pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(glo_PathGDB, 0)
    End Sub
    Public Sub PC_CargarDominio(ByVal lodtbModuloDominio As DataTable, ByVal lodtbDetalleDominio As DataTable, ByVal paStrNombreDominio As String, ByVal paCtrObjeto As Object, ByVal paStrCodPadre As String)

        '***********************
        ' Descripcion : Permite almacenar el detalle de un dominio almacenado en tablas y 
        '   sea cargado en un cotrol Combobox, ListBox, etc
        Dim loDtvDominio As DataView = lodtbDetalleDominio.DefaultView
        Dim tipo As Type
        tipo = paCtrObjeto.GetType

        loDtvDominio.RowFilter = "DESCRIPCION = '" & paStrNombreDominio & "'"
        Dim loDtbDominoFiltro As DataTable = loDtvDominio.ToTable
        If loDtbDominoFiltro.Rows.Count = 1 Then
            'MsgBox(loDtbDominoFiltro.Rows(0).Item("CODIGO_ITEM").ToString)
            loDtvDominio.RowFilter = Nothing
            ''*************************
            'Obtiene los registros Relacionados con el Dominio
            Dim viewDetalle As DataView = lodtbModuloDominio.DefaultView
            viewDetalle.RowFilter = "DOMINIO = '" & loDtbDominoFiltro.Rows(0).Item("CODIGO_ITEM").ToString & "'"
            Dim newTableDetalle As DataTable = viewDetalle.ToTable("UniqueLastNames", True, "CORRELATIVO")
            'Obtiene la Descripcion de los Registros del Dominio
            Dim lodtbDetalle As New DataTable
            lodtbDetalle.Columns.Add("OBJECTID", GetType(String)) '1
            lodtbDetalle.Columns.Add("CODIGO_ITEM", GetType(String)) '1
            lodtbDetalle.Columns.Add("DESCRIPCION", GetType(String)) '1
            lodtbDetalle.Columns.Add("TIPO", GetType(String)) '1
            lodtbDetalle.Columns.Add("CORRELATIVO", GetType(String)) '1
            Dim viewDetalle1 As DataView = lodtbDetalleDominio.DefaultView
            For X As Integer = 0 To newTableDetalle.Rows.Count - 1
                viewDetalle1.RowFilter = "CORRELATIVO = '" & newTableDetalle.Rows(X).Item(0).ToString & "'"
                If viewDetalle1.Count > 0 Then
                    lodtbDetalle.ImportRow(viewDetalle1.Item(0).Row)
                End If
            Next
            'Dim tipo As Type
            'tipo = paCtrObjeto.GetType
            Dim lodtrDetalle As DataRow
            lodtrDetalle = lodtbDetalle.NewRow()
            lodtrDetalle(0) = ""
            lodtrDetalle(1) = ""
            '    If tipo.Name <> "ListBox" Then
            lodtrDetalle(2) = " -- Seleccione -- "
            'End If
            lodtrDetalle(3) = "Recursivo"
            lodtbDetalle.Rows.Add(lodtrDetalle)
            Dim viewMod As DataView = lodtbDetalle.DefaultView
            'PAra Filtar los registros recursivos * Necesitamos el Codigo Padre
            If Not IsDBNull(lodtbDetalle.Rows(0).Item("TIPO")) Then
                If UCase(lodtbDetalle.Rows(0).Item("TIPO")) = "RECURSIVO" Then
                    viewMod.RowFilter = " (CODIGO_ITEM like '" & paStrCodPadre & "%' or CODIGO_ITEM ='' ) and (len(CODIGO_ITEM) = " & Len(paStrCodPadre) + 2 & " or len(CODIGO_ITEM) = 0)"
                End If
            End If
            viewMod.Sort = "DESCRIPCION ASC"
            'Dim tipo As Type
            If viewMod.Count > 1 Then
                tipo = paCtrObjeto.GetType
                If tipo.Name = "ComboBox" Or tipo.Name = "ListBox" Then
                    'paCtrObjeto.items.clear()
                    paCtrObjeto.DisplayMember = "DESCRIPCION"
                    paCtrObjeto.ValueMember = "CODIGO_ITEM"
                    paCtrObjeto.DataSource = viewMod
                    paCtrObjeto.SelectedValue = ""
                End If
                If tipo.Name = "C1TrueDBDropdown" Then
                    'Dim lodtvDominio As New DataView(dt1)
                    paCtrObjeto.ListField = "DESCRIPCION"
                    paCtrObjeto.DataField = "CODIGO_ITEM"
                    paCtrObjeto.DataSource = viewMod
                    paCtrObjeto.ColumnHeaders = False
                    paCtrObjeto.ValueTranslate = True
                    paCtrObjeto.BackColor = Drawing.Color.FromArgb(242, 242, 240)
                    paCtrObjeto.HeadingStyle.BackColor = Drawing.Color.FromArgb(207, 209, 221) 'Color.Black CFD1DD
                    paCtrObjeto.OddRowStyle.BackColor = Drawing.Color.FromArgb(229, 232, 239) 'E5E8EF
                    paCtrObjeto.EvenRowStyle.BackColor = Drawing.Color.FromArgb(242, 242, 240)
                    paCtrObjeto.DisplayColumns(0).Width = 0
                    paCtrObjeto.DisplayColumns(1).Width = 0
                    paCtrObjeto.DisplayColumns(2).Width = 20 * 5.5
                    paCtrObjeto.DisplayColumns(3).Width = 0
                    paCtrObjeto.DisplayColumns(4).Width = 0
                    paCtrObjeto.Height = 150
                    paCtrObjeto.Width = (10 * 5.5) + 35 + 26
                End If
            Else
                paCtrObjeto.DATASOURCE = Nothing
                paCtrObjeto.Enabled = False
            End If
        End If
    End Sub
    Public Function FT_CargarTabla(ByVal paloITable As String) As DataTable
        Dim lodtRegistros As New DataTable
        Try
            pTable = pFeatureWorkspace.OpenTable(paloITable)
            Dim pFeatureCursor As ICursor
            Dim pQueryFilter As IQueryFilter
            Dim loNumCol, loNumColTemp, sw As Int16
            pFields = pTable.Fields
            loNumCol = pFields.FieldCount
            For c As Int16 = 0 To loNumCol - 1
                If pFields.Field(c).Name = "Shape" Then
                    lodtRegistros.Columns.Add("SHAPE")
                    loNumColTemp = c
                    sw = 1
                Else
                    lodtRegistros.Columns.Add(pFields.Field(c).Name)
                End If
            Next
            pQueryFilter = New QueryFilter
            pQueryFilter.WhereClause = Nothing
            pFeatureCursor = pTable.Search(pQueryFilter, False)
            Dim pRow As IRow
            pRow = pFeatureCursor.NextRow
            Do Until pRow Is Nothing
                Dim c1 As Integer = -1
                Dim dr As DataRow
                dr = lodtRegistros.NewRow
                For c As Int16 = 0 To loNumCol - 1
                    If loNumColTemp <> c Then
                        c1 = c1 + 1
                        If Not IsDBNull(pRow.Value(c)) Then dr.Item(c) = CType(pRow.Value(c), String)
                    Else
                        If sw = 1 Then c1 = c1 + 1
                    End If
                Next
                lodtRegistros.Rows.Add(dr)
                pRow = pFeatureCursor.NextRow
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return lodtRegistros
    End Function
    Public Function f_Genera_Reporte(ByVal loFeature As String, ByVal p_App As IApplication) As DataTable
        Dim x As Integer = 0
        Dim lodtRegistros As New DataTable
        Dim lodtbUbigeo As New DataTable
        'Dim pRow As IRow
        Dim pRow As IFeature
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        Dim afound As Boolean
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = loFeature Then
                pFeatureLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("Layer Catastro No Existe.", MsgBoxStyle.Information, "BDGEOCATMIN")
            Return lodtRegistros
            Exit Function
        End If
        'pFeatureCursor = pFeatureLayer.Search(Nothing, True)
        pFeatureCursor = pFeatureLayer.FeatureClass.Update(Nothing, True)
        pRow = pFeatureCursor.NextFeature
        If pRow Is Nothing Then
            MsgBox(".:: No Hay Datos para el Reporte ::.", MsgBoxStyle.Information, "BDGEOCATMIN")
            Return lodtRegistros
            Exit Function
        End If
        Do Until pRow Is Nothing
            If x = 0 Then
                lodtRegistros.Columns.Add("NUM")
                lodtRegistros.Columns.Add("CODIGO")
                lodtRegistros.Columns.Add("ESTADO")
                lodtRegistros.Columns.Add("NOMBRE")
                lodtRegistros.Columns.Add("TITULAR")
                lodtRegistros.Columns.Add("SUSTANCIA")
                lodtRegistros.Columns.Add("DEPARTAMENTO")
                lodtRegistros.Columns.Add("PROVINCIA")
                lodtRegistros.Columns.Add("DISTRITO")
                lodtRegistros.Columns.Add("HECTAREA")
                x = 1
            End If
            Dim dr As DataRow
            dr = lodtRegistros.NewRow
            dr.Item(0) = (pRow.Value(pRow.Fields.FindField("CONTADOR")))
            dr.Item(1) = (pRow.Value(pRow.Fields.FindField("CODIGOU")))
            dr.Item(2) = pRow.Value(pRow.Fields.FindField("D_ESTADO"))
            dr.Item(3) = pRow.Value(pRow.Fields.FindField("CONCESION"))
            dr.Item(4) = pRow.Value(pRow.Fields.FindField("TIT_CONCES"))
            Select Case pRow.Value(pRow.Fields.FindField("NATURALEZA"))
                Case "M"
                    dr.Item(5) = "Metálico"
                Case Else '"N"
                    dr.Item(5) = "No Metálico"
            End Select
            Try
                'dr.Item(5) = pRow.Value(pRow.Fields.FindField("NATURALEZA"))
                lodtbUbigeo = cls_Oracle.F_Obtiene_Datos_UBIGEO(Mid(pRow.Value(pRow.Fields.FindField("DEMAGIS")), 1, 6))
                If lodtbUbigeo.Rows.Count <> 0 Then
                    dr.Item(6) = lodtbUbigeo.Rows(0).Item("DPTO").ToString
                    dr.Item(7) = lodtbUbigeo.Rows(0).Item("PROV").ToString
                    dr.Item(8) = lodtbUbigeo.Rows(0).Item("DIST").ToString
                    dr.Item(9) = pRow.Value(pRow.Fields.FindField("HECTAREA"))
                    'pFeatureCursor.UpdateFeature(pFeatureCursor.FindField("DPTO") = dr.Item(6).ToString
                    pRow.Value(pFeatureCursor.FindField("DPTO")) = lodtbUbigeo.Rows(0).Item("DPTO").ToString
                    'pFeatureCursor.UpdateFeature(pRow)
                    'pUpdateFeatures.UpdateFeature(pFeature)
                    'pFeature.Value(pUpdateFeatures.FindField("LEYENDA")) = ""
                    pRow.Value(pFeatureCursor.FindField("PROV")) = lodtbUbigeo.Rows(0).Item("PROV").ToString
                    'pFeatureCursor.UpdateFeature(pRow)
                    pRow.Value(pFeatureCursor.FindField("DIST")) = lodtbUbigeo.Rows(0).Item("DIST").ToString
                    pFeatureCursor.UpdateFeature(pRow)

                End If
            Catch ex As Exception
                MsgBox(lodtbUbigeo)
            End Try
            'dr.Item(6) = pRow.Value(pRow.Fields.FindField("DPTO")) 'lodtbUbigeo.Rows(0).Item("DPTO")
            'dr.Item(7) = pRow.Value(pRow.Fields.FindField("PROV")) 'lodtbUbigeo.Rows(0).Item("PROV")
            'dr.Item(8) = pRow.Value(pRow.Fields.FindField("DIST")) 'lodtbUbigeo.Rows(0).Item("DIST")

            lodtRegistros.Rows.Add(dr)
            pRow = pFeatureCursor.NextFeature
        Loop
        Return lodtRegistros
    End Function
    Public Function f_Genera_Reporte1(ByVal loFeature As String, ByVal p_App As IApplication) As DataTable
        Dim x As Integer = 0
        Dim lodtRegistros As New DataTable
        Dim lodtbUbigeo As New DataTable
        Dim pRow As IRow
        pMxDoc = p_App.Document
        pMap = pMxDoc.FocusMap
        Dim afound As Boolean
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = loFeature Then
                pFeatureLayer = pMxDoc.FocusMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("Layer Catastro No Existe.", MsgBoxStyle.Information, "BDGEOCATMIN")
            Return lodtRegistros
            Exit Function
        End If
        pFeatureCursor = pFeatureLayer.Search(Nothing, True)
        pRow = pFeatureCursor.NextFeature
        If pRow Is Nothing Then
            MsgBox(".:: No Hay Datos para el Reporte ::.", MsgBoxStyle.Information, "BDGEOCATMIN")
            Return lodtRegistros
            Exit Function
        End If
        Do Until pRow Is Nothing
            If x = 0 Then
                lodtRegistros.Columns.Add("NUM")
                lodtRegistros.Columns.Add("CODIGO")
                lodtRegistros.Columns.Add("NOMBRE")
                lodtRegistros.Columns.Add("ZONA")
                lodtRegistros.Columns.Add("TE")
                lodtRegistros.Columns.Add("TP")
                lodtRegistros.Columns.Add("PUBL")
                lodtRegistros.Columns.Add("INCOR")
                lodtRegistros.Columns.Add("SUST")
                x = 1
            End If
            Dim dr As DataRow
            dr = lodtRegistros.NewRow
            dr.Item(0) = (pRow.Value(pRow.Fields.FindField("CONTADOR")))
            dr.Item(1) = (pRow.Value(pRow.Fields.FindField("CODIGOU")))
            dr.Item(2) = pRow.Value(pRow.Fields.FindField("CONCESION"))
            dr.Item(3) = pRow.Value(pRow.Fields.FindField("ZONA"))
            dr.Item(4) = pRow.Value(pRow.Fields.FindField("TIPO_EX"))
            dr.Item(5) = pRow.Value(pRow.Fields.FindField("ESTADO"))
            dr.Item(6) = pRow.Value(pRow.Fields.FindField("DE_PUBL")).ToString
            If Trim(pRow.Value(pRow.Fields.FindField("DE_PUBL"))) = "" Then
                dr.Item(6) = "NP"
            End If

            dr.Item(7) = pRow.Value(pRow.Fields.FindField("DE_IDEN")).ToString

            If Trim(pRow.Value(pRow.Fields.FindField("DE_IDEN"))) = "" Then
                dr.Item(7) = "NI"
            End If
            dr.Item(8) = pRow.Value(pRow.Fields.FindField("NATURALEZA"))
            lodtRegistros.Rows.Add(dr)
            pRow = pFeatureCursor.NextFeature
        Loop
        Return lodtRegistros
    End Function
End Class


