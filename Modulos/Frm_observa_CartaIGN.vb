Imports System.Drawing
Imports PORTAL_Clases
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.GISClient
Imports ESRI.ArcGIS.Display
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Output
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.CartoUI
Imports ESRI.ArcGIS.GeoDatabaseUI
Imports stdole

Public Class Frm_observa_CartaIGN
    'Public m_application As IApplication
    'Private cls_Oracle As New cls_Oracle
    Private cls_Catastro As New cls_DM_1
    Private cls_DM_2 As New cls_DM_2
    Private cls_Prueba As New cls_Prueba
    Private lodtbDatos As New DataTable
    Private cls_eval As New Cls_evaluacion
    Private cls_planos As New Cls_planos
    Public m_application As IApplication
    Public papp As IApplication
    Private Sub Frm_observa_CartaIGN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txt_dato20.Visible = False
        Me.Txt_Rio.Visible = False
        Me.Txt_Laguna.Visible = False
        v_observa_carta = ""
        v_checkbox1 = ""
        v_checkbox2 = ""
        v_checkbox3 = ""
        v_checkbox4 = ""
        v_checkbox5 = ""
        v_checkbox6 = ""
        v_checkbox7 = ""
        v_checkbox8 = ""
        v_checkbox9 = ""
        v_checkbox10 = ""
        v_checkbox11 = ""
        v_checkbox12 = ""
        v_checkbox13 = ""
        v_checkbox14 = ""
        v_checkbox15 = ""
        v_checkbox16 = ""
        v_checkbox17 = ""
        v_checkbox18 = ""
        v_checkbox19 = ""
        v_checkbox20 = ""
        v_checkbox21 = ""
    End Sub

    Private Sub Check_dato1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato1.CheckedChanged
        If Me.Check_dato1.Checked = True Then
            v_checkbox1 = Me.Check_dato1.Text
            Me.Check_dato2.Checked = False
            v_checkbox2 = ""
        Else
            v_checkbox1 = ""
        End If
    End Sub

    Private Sub Check_dato2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato2.CheckedChanged
        If Me.Check_dato2.Checked = True Then
            v_checkbox2 = Me.Check_dato2.Text
            Me.Check_dato1.Checked = False
            v_checkbox1 = ""
        Else
            v_checkbox2 = ""

        End If
    End Sub

    Private Sub Check_dato3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato3.CheckedChanged

        If Me.Check_dato3.Checked = True Then
            v_checkbox3 = Me.Check_dato3.Text
            Me.Check_dato4.Checked = False
            v_checkbox4 = ""
        Else
            v_checkbox3 = ""
        End If
    End Sub

    Private Sub Check_dato4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato4.CheckedChanged
        If Me.Check_dato4.Checked = True Then
            v_checkbox4 = Me.Check_dato4.Text
            Me.Check_dato3.Checked = False
            v_checkbox3 = ""
        Else
            v_checkbox4 = ""
        End If
    End Sub

    Private Sub Check_dato5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato5.CheckedChanged
        If Me.Check_dato5.Checked = True Then
            v_checkbox5 = Me.Check_dato5.Text
        Else
            v_checkbox5 = ""
        End If

    End Sub

    Private Sub Check_dato6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato6.CheckedChanged
        If Me.Check_dato6.Checked = True Then
            v_checkbox6 = Me.Check_dato6.Text
        Else
            v_checkbox6 = ""
        End If
    End Sub

    Private Sub Check_dato7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato7.CheckedChanged
        If Me.Check_dato7.Checked = True Then
            v_checkbox7 = Me.Check_dato7.Text
            Me.Check_dato8.Checked = False
            v_checkbox8 = ""
        Else
            v_checkbox7 = ""
        End If
    End Sub

    Private Sub Check_dato8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato8.CheckedChanged
        If Me.Check_dato8.Checked = True Then
            v_checkbox8 = Me.Check_dato8.Text
            Me.Check_dato7.Checked = False
            v_checkbox7 = ""
        Else
            v_checkbox8 = ""
        End If
    End Sub

    Private Sub Check_dato9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato9.CheckedChanged
        If Me.Check_dato9.Checked = True Then
            v_checkbox9 = Me.Check_dato9.Text
            Me.Check_dato10.Checked = False
            v_checkbox10 = ""
        Else
            v_checkbox9 = ""
        End If
    End Sub

    Private Sub Check_dato10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato10.CheckedChanged
        If Me.Check_dato10.Checked = True Then
            v_checkbox10 = Me.Check_dato10.Text
            Me.Check_dato9.Checked = False
            v_checkbox9 = ""
        Else
            v_checkbox10 = ""
        End If
    End Sub

    Private Sub Check_dato11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato11.CheckedChanged

        'If Me.Check_dato11.Checked = True Then
        'v_checkbox11 = Me.Check_dato11.Text
        'Else
        'v_checkbox11 = ""
        'End If

        If Me.Check_dato11.Checked = True Then
            Me.Txt_Rio.Visible = True
        Else
            Me.Txt_Rio.Visible = False
            v_txt_rio = ""
        End If

    End Sub

    Private Sub Check_dato12_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato12.CheckedChanged
        If Me.Check_dato12.Checked = True Then
            v_checkbox12 = Me.Check_dato12.Text
        Else
            v_checkbox12 = ""
        End If

    End Sub

    Private Sub Check_dato13_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato13.CheckedChanged
        'If Me.Check_dato13.Checked = True Then
        'v_checkbox13 = Me.Check_dato13.Text
        'Else
        'v_checkbox13 = ""
        'End If
        If Me.Check_dato13.Checked = True Then
            Me.Txt_Laguna.Visible = True
        Else
            Me.Txt_Laguna.Visible = False
            v_txt_laguna = ""
        End If

    End Sub

    Private Sub Check_dato14_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato14.CheckedChanged
        If Me.Check_dato14.Checked = True Then
            v_checkbox14 = Me.Check_dato14.Text
        Else
            v_checkbox14 = ""
        End If
    End Sub

    Private Sub Check_dato15_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato15.CheckedChanged
        If Me.Check_dato15.Checked = True Then
            v_checkbox15 = Me.Check_dato15.Text
        Else
            v_checkbox15 = ""
        End If
    End Sub

    Private Sub Check_dato19_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato19.CheckedChanged
        If Me.Check_dato19.Checked = True Then
            v_checkbox19 = Me.Check_dato19.Text
        Else
            v_checkbox19 = ""
        End If
    End Sub

    Private Sub Check_dato16_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato16.CheckedChanged
        If Me.Check_dato16.Checked = True Then
            v_checkbox16 = Me.Check_dato16.Text
        Else
            v_checkbox16 = ""
        End If
    End Sub

    Private Sub Check_dato17_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato17.CheckedChanged
        If Me.Check_dato17.Checked = True Then
            v_checkbox17 = Me.Check_dato17.Text
        Else
            v_checkbox17 = ""
        End If

    End Sub

    Private Sub Check_dato18_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato18.CheckedChanged
        If Me.Check_dato18.Checked = True Then
            v_checkbox18 = Me.Check_dato18.Text
        Else
            v_checkbox18 = ""
        End If
    End Sub

    Private Sub Check_dato20_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato20.CheckedChanged
        If Me.Check_dato20.Checked = True Then
            Me.txt_dato20.Visible = True
        Else
            Me.txt_dato20.Visible = False
            v_checkbox20 = ""
        End If
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

        v_checkbox20 = Me.txt_dato20.Text
        v_checkbox11 = Me.Txt_Rio.Text
        v_checkbox13 = Me.Txt_Laguna.Text


        If v_observa_carta <> "" Then  'Solo si marca la observacion carta en ninguno lista los indicaodores para actualizar
            'Abriendo el formulario donde estaran los indicadores a actualizar

            Dim formulario As New frm_observa_CartaIGN_val
            formulario.pApp = m_application
            formulario.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
            formulario.Show()

        Else


            If pMap.LayerCount = 0 Then
                MsgBox("No Existe las capas en Carta IGN para generar observación Carta IGN...", MsgBoxStyle.Information, "BDGEOCATMN")
                Exit Sub
            End If

            Dim cls_planos As New Cls_planos

            colecciones_obs.Clear()
            'v_checkbox20 = Me.txt_dato20.Text
            'v_checkbox11 = Me.Txt_Rio.Text
            'v_checkbox13 = Me.Txt_Laguna.Text

            'Exit Sub

            cls_planos.Genera_ObservacionesCarta()
        End If

        Me.Close()

    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub Check_dato21_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato21.CheckedChanged

        If Me.Check_dato21.Checked = True Then
            v_observa_carta = "Seleccionado"
        Else
            v_observa_carta = ""
        End If

    End Sub

    Private Sub GroupBox5_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox5.Enter

    End Sub
End Class