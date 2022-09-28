Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto
Imports PORTAL_Clases
Imports System.Drawing
Imports ESRI.ArcGIS.esriSystem
Public Class Frm_Datos_AreaDispo
    Public papp As IApplication
    Public pEste As Double
    Public pNorte As Double
 

    Private Sub Frm_Datos_AreaDispo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim dr As DataRow
        pMap = pMxDoc.FocusMap
        Dim pFeatureCursor As IFeatureCursor
        Dim pFields As IFields
        Dim pFeatLayer As IFeatureLayer = Nothing
        Dim pfeatureclass As IFeatureClass
        Dim afound As Boolean = False
        Dim codigo_ev As String
        Dim conce_ev As String
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Areadispo_" & v_codigo Then
                pFeatLayer = pMap.Layer(A)
                afound = True
                Exit For
            End If
        Next A
        If Not afound Then
            MsgBox("No se encuentra el Layer")
            Exit Sub
        End If
        Dim v_area_ev1 As Double = 0
        Dim v_area_ev As Double
        pfeatureclass = pFeatLayer.FeatureClass
        pFields = pfeatureclass.Fields
        pFeatureCursor = pfeatureclass.Search(Nothing, False)
        pFeature = pFeatureCursor.NextFeature
        
        Try
            v_area_ev = 0
            Do Until pFeature Is Nothing

                codigo_ev = pFeature.Value(pFields.FindField("CODIGOU"))
                ' conce_ev = pFeature.Value(pFields.FindField("CONCESION"))
                v_area_ev1 = pFeature.Value(pFields.FindField("AREA_FINAL"))
                v_area_ev = v_area_ev1 + v_area_ev
                pFeature = pFeatureCursor.NextFeature
            Loop

            txtcodigo.Text = v_codigo
            txtnombre.Text = v_nombre_dm
            txtarea.Text = Format(Math.Round(v_area_ev, 4), "###,####.0000")


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

 
    Private Sub btncerrar_Click(sender As Object, e As EventArgs) Handles btncerrar.Click
        Me.Close()

    End Sub
End Class