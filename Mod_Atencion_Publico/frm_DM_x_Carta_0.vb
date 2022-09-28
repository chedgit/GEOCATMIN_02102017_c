Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Framework
Imports PORTAL_Clases


Public Class frm_DM_x_Carta_0
    Public pApp As IApplication
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim frm As New frm_DM_x_Carta_1
        Dim cls_Catastro As New cls_DM_1
        Try
            frm.p_Campo = Me.cboCarta.SelectedItem
            frm.p_Dato = Me.txtDato.Text
            frm.m_application = pApp
            frm.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
            frm.ShowDialog()
            Select Case frm.DialogResult
                Case Windows.Forms.DialogResult.OK
                    cls_Catastro.Borra_Todo_Feature("", pApp)
                    cls_Catastro.Limpiar_Texto_Pantalla(pApp)
                Case Windows.Forms.DialogResult.Cancel
                    Me.Close()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub cboCarta_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCarta.SelectedIndexChanged
        Me.txtDato.Text = ""
    End Sub

    Private Sub frm_DM_x_Carta_0_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim cls_Catastro As New cls_DM_1
        Dim cls_Evaluacion As New Cls_evaluacion
        Dim m_form As New GEOCATMIN_IniLogin ' LoginForm
        m_form.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
        If gloint_Acceso = 0 Then m_form.ShowDialog()
        If m_form.DialogResult = Windows.Forms.DialogResult.OK Or gloint_Acceso = "1" Then
            gloint_Acceso = "1"
            Me.lblUsuario.Text = "Usuario: " & gstrNombre_Usuario 'gstrUsuarioAcceso
            Pintar_Formulario()
            'cls_Evaluacion.Eliminadataframe()
            cls_Catastro.Borra_Todo_Feature("", pApp)
            cls_Catastro.Limpiar_Texto_Pantalla(pApp)
            Me.cboCarta.SelectedIndex = 0
        Else
            Me.Close()
        End If
    End Sub
    Public Sub Pintar_Formulario()
        Me.BackColor = Drawing.Color.FromArgb(242, 242, 240)
    End Sub
    Private Sub txtDato_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDato.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            Dim losender As New System.Object
            Dim loe As New System.EventArgs
            Me.btnBuscar_Click(losender, loe)
        End If
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub btnBuscar_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.VisibleChanged

    End Sub
End Class