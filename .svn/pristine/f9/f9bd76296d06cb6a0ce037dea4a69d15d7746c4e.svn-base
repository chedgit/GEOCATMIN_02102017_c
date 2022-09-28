Imports System.IO
Imports System.Collections
Imports PORTAL_Clases
Imports ESRI.ArcGIS.Framework
Imports System.Drawing
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.DataSourcesOleDB
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.esriSystem

Imports ESRI.ArcGIS.GISClient
Imports ESRI.ArcGIS.Display
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Carto
Imports stdole
Public Class Form_DatosProcesados
    Public m_application As IApplication
    Private cls_Oracle As New cls_Oracle
    

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        'Accion_proceso = True

        Me.Close()
        If valida_informe = "" Then
            Dim lostrOpcion As String = ""
            Dim lostrRetorno_val As String

            lostrOpcion = MsgBox("¿Aun no ha Terminado de Guardar todos sus datos para su Informe Tecnico de Evaluación .. ?" & vbNewLine & "Si:   Para Guardar lo existente evaluado y generar otra busqueda " & vbNewLine & "No:  Realizar otra busqueda sin guardar ningun dato evaluado para el informe", MsgBoxStyle.YesNoCancel, "Evaluacion de DM...")

            Select Case lostrOpcion
                Case "2" 'Cancel no hace nada
                    Me.Close()
                    Exit Sub
                Case "6" 'Si, continua otra busqueda grabando lo existente
                Case "7" 'No, vuelve a validar su evaluacion

                    lostrRetorno_val = cls_Oracle.FT_SG_D_EVALGIS("DEL", v_codigo, glo_InformeDM, gstrCodigo_Usuario)

            End Select
        Else
            Accion_proceso = True

        End If

    End Sub

    
    Private Sub Form_DatosProcesados_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Accion_proceso = False

        Me.lbldato.Text = "Se ha Procesado en el código :" & v_codigo & " lo siguiente "
        If var_fa_validaeval = True Then
            Me.Check_dato1.Checked = True
        Else
            Me.Check_dato1.Checked = False
        End If

        If var_fa_AreaSuper = True Then
            Me.Check_dato2.Checked = True
        Else
            Me.Check_dato2.Checked = False
        End If

        If var_fa_Coord_SuperAreaReserva = True Then
            Me.Check_dato3.Checked = True
        Else
            Me.Check_dato3.Checked = False
        End If

        If var_Fa_AreaReserva = True Then
            Me.Check_dato4.Checked = True
        Else
            Me.Check_dato4.Checked = False

        End If
       
        If var_Fa_Zonaurbana = True Then
            Me.Check_dato5.Checked = True
        Else
            Me.Check_dato5.Checked = False

        End If
        If var_Fa_AreasNaturales = True Then
            Me.Check_dato6.Checked = True
        Else
            Me.Check_dato6.Checked = False

        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Accion_proceso = False
        MsgBox("Ha cancelado, vuelva a intentarlo...", MsgBoxStyle.Critical, "BDGEOCATMIN")

        Me.Close()
        Exit Sub

    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub Check_dato5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_dato5.CheckedChanged

    End Sub
End Class