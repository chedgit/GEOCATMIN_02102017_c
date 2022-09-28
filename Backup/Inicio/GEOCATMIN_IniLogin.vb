Imports ESRI.ArcGIS.DataSourcesOleDB
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem
Imports stdole

'Option Explicit On
'Imports Oracle.DataAccess.Client
Imports PORTAL_Clases
Imports PORTAL_Configuracion

Public Class GEOCATMIN_IniLogin
    Inherits System.Windows.Forms.Form
    Public pPropset As IPropertySet
    Public pWorkspace As IWorkspace
    Public clsData As New cls_wurbina
    Private cls_Oracle As New cls_Oracle


#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents lblContrasena As System.Windows.Forms.Label
    Friend WithEvents txtUsuario As System.Windows.Forms.TextBox
    Friend WithEvents txtContrasena As System.Windows.Forms.TextBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents lblMensaje As System.Windows.Forms.Label
    Friend WithEvents lblNombreUsuario As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents gpo_Imagen As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GEOCATMIN_IniLogin))
        Me.lblNombreUsuario = New System.Windows.Forms.Label
        Me.lblContrasena = New System.Windows.Forms.Label
        Me.txtUsuario = New System.Windows.Forms.TextBox
        Me.txtContrasena = New System.Windows.Forms.TextBox
        Me.btnAceptar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.lblMensaje = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.gpo_Imagen = New System.Windows.Forms.GroupBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.gpo_Imagen.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblNombreUsuario
        '
        Me.lblNombreUsuario.BackColor = System.Drawing.Color.Transparent
        Me.lblNombreUsuario.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombreUsuario.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblNombreUsuario.Location = New System.Drawing.Point(16, 8)
        Me.lblNombreUsuario.Name = "lblNombreUsuario"
        Me.lblNombreUsuario.Size = New System.Drawing.Size(104, 23)
        Me.lblNombreUsuario.TabIndex = 0
        Me.lblNombreUsuario.Text = "Usuario"
        Me.lblNombreUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblContrasena
        '
        Me.lblContrasena.BackColor = System.Drawing.Color.Transparent
        Me.lblContrasena.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContrasena.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblContrasena.Location = New System.Drawing.Point(16, 56)
        Me.lblContrasena.Name = "lblContrasena"
        Me.lblContrasena.Size = New System.Drawing.Size(64, 23)
        Me.lblContrasena.TabIndex = 1
        Me.lblContrasena.Text = "Contraseña"
        Me.lblContrasena.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUsuario
        '
        Me.txtUsuario.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsuario.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtUsuario.Location = New System.Drawing.Point(16, 32)
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.Size = New System.Drawing.Size(112, 21)
        Me.txtUsuario.TabIndex = 2
        '
        'txtContrasena
        '
        Me.txtContrasena.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContrasena.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtContrasena.Location = New System.Drawing.Point(16, 80)
        Me.txtContrasena.Name = "txtContrasena"
        Me.txtContrasena.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtContrasena.Size = New System.Drawing.Size(112, 21)
        Me.txtContrasena.TabIndex = 3
        '
        'btnAceptar
        '
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Image)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAceptar.Location = New System.Drawing.Point(170, 6)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(104, 28)
        Me.btnAceptar.TabIndex = 4
        '
        'btnCancelar
        '
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancelar.Location = New System.Drawing.Point(66, 6)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(104, 28)
        Me.btnCancelar.TabIndex = 5
        '
        'lblMensaje
        '
        Me.lblMensaje.BackColor = System.Drawing.Color.Transparent
        Me.lblMensaje.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMensaje.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblMensaje.Location = New System.Drawing.Point(16, 81)
        Me.lblMensaje.Name = "lblMensaje"
        Me.lblMensaje.Size = New System.Drawing.Size(280, 16)
        Me.lblMensaje.TabIndex = 6
        Me.lblMensaje.Text = "Ingrese usuario, contraseña y luego presione 'Aceptar'"
        Me.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(152, 105)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(125, 96)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblContrasena)
        Me.GroupBox1.Controls.Add(Me.lblNombreUsuario)
        Me.GroupBox1.Controls.Add(Me.txtContrasena)
        Me.GroupBox1.Controls.Add(Me.txtUsuario)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 97)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(280, 112)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnCancelar)
        Me.GroupBox2.Controls.Add(Me.btnAceptar)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 209)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(280, 40)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        '
        'gpo_Imagen
        '
        Me.gpo_Imagen.Controls.Add(Me.PictureBox2)
        Me.gpo_Imagen.Location = New System.Drawing.Point(3, 2)
        Me.gpo_Imagen.Name = "gpo_Imagen"
        Me.gpo_Imagen.Size = New System.Drawing.Size(280, 76)
        Me.gpo_Imagen.TabIndex = 152
        Me.gpo_Imagen.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(6, 12)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(271, 59)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 0
        Me.PictureBox2.TabStop = False
        '
        'GEOCATMIN_IniLogin
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(286, 251)
        Me.Controls.Add(Me.gpo_Imagen)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.lblMensaje)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(296, 287)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(296, 287)
        Me.Name = "GEOCATMIN_IniLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ingreso de Usuario"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.gpo_Imagen.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        DialogResult = Windows.Forms.DialogResult.Cancel ' DialogResult.Cancel
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        PT_Validar_Acceso()
    End Sub
    Public Sub Pintar_Formulario()
        Me.BackColor = Drawing.Color.FromArgb(242, 242, 240)
    End Sub

    Private Sub PT_Validar_Acceso()
        Dim lostrContrasenaInvalida As String
        pgloUsuConexionOracle = clsData.ConxGis(txtUsuario.Text, txtContrasena.Text, gstrDatabase)
        lostrContrasenaInvalida = ContrasenaInvalida(Trim(txtUsuario.Text), Trim(txtContrasena.Text))
        'Asegurarse de que el usuario ingrese el Nombre de Usuario y Contraseña
        If txtUsuario.Text.Length = 0 Then
            MsgBox("Debe ingresar un Usuario", MsgBoxStyle.Exclamation, "[BDGeocatmin]")
            txtUsuario.Focus()
        ElseIf txtContrasena.Text.Length = 0 Then
            MsgBox("Debe ingresar una Contraseña", MsgBoxStyle.Exclamation, "[BDGeocatmin]")
            txtContrasena.Focus()
        ElseIf lostrContrasenaInvalida = "1" Then
            'El Nombre de Usuario/Contraseña es inválido
            MsgBox("Nombre de Usuario/Contraseña inválida", MsgBoxStyle.Exclamation, "[BDGeocatmin]")
            txtUsuario.Focus()
        ElseIf lostrContrasenaInvalida = "2" Then
            MsgBox("No tiene permiso para acceder al Sistema", MsgBoxStyle.Exclamation, "[BDGeocatmin]")
            txtUsuario.Focus()
        ElseIf lostrContrasenaInvalida = "3" Then
            MsgBox("Error de Conexión a la Base de Datos Oracle, ingrese Usuario y Clave válidos " & vbNewLine & "Intente de Nuevo", MsgBoxStyle.Information, "[BDGeocatmin]")
            txtUsuario.Focus()
        Else
            gstrUsuarioAcceso = Me.txtUsuario.Text
            gstrUsuarioClave = Me.txtContrasena.Text
            pgloUsuConexionGIS = clsData.ConxGis(txtUsuario.Text, txtContrasena.Text, gstrDatabaseGIS)
            Select Case glo_Server_SDE
                Case "10.102.0.12"
                    glo_Desarrollo_BD = "Servidor: Desarrollo"
                    glo_User_SDE = "BDGINGE"
                    glo_Password_SDE = "BDGINGE"
                Case "SRVDESA03", "10.102.11.35"
                    glo_Desarrollo_BD = "Servidor: Desarrollo"
                    glo_User_SDE = gstrUsuarioAcceso
                    glo_Password_SDE = gstrUsuarioClave
                Case Else '"10.102.0.30"
                    If glo_Version_SDE = "SDE.DEFAULT" Then
                        glo_Desarrollo_BD = "Servidor: Producción"
                        glo_User_SDE = gstrUsuarioAcceso
                        glo_Password_SDE = gstrUsuarioClave
                    End If
            End Select
            Select Case gstrDatabase
                Case "DESA"
                    glo_Desarrollo_BD = "Srv BD: Desarrollo / SrvGIS: Producción"
                    glo_User_SDE = gstrUsuarioAcceso
                    glo_Password_SDE = gstrUsuarioClave
                Case "ORACLE"
                    If glo_Version_SDE = "SDE.DEFAULT" Then
                        glo_Desarrollo_BD = "Srv BD: Producción / SrvGIS: Producción"
                        glo_User_SDE = gstrUsuarioAcceso
                        glo_Password_SDE = gstrUsuarioClave
                    End If
            End Select
            DialogResult = Windows.Forms.DialogResult.OK ' DialogResult.OK
        End If
    End Sub

    Private Function ContrasenaInvalida(ByVal pastrUsuario As String, ByVal pastrContrasena As String) As String
        Dim cls_Conexion As New cls_Oracle ' clsBD_Seguridad
        Dim lodtbUsuario As New DataTable
        Dim lodtbAcceso As New DataTable
        Try
            lodtbUsuario = cls_Conexion.F_Verifica_Usuario(txtUsuario.Text.ToUpper, pgloUsuConexionOracle)
            If lodtbUsuario.Rows.Count = 1 Then
                gstrCodigo_Usuario = lodtbUsuario.Rows(0).Item("USUARIO")
                gstrNombre_Usuario = Texto_Alta_Baja(lodtbUsuario.Rows(0).Item("USERNAME"))
                Return ""
            Else
                Return "2"
            End If
        Catch Ex As Exception
            Return "3"
        End Try
    End Function

    Private Sub txtContrasena_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtContrasena.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            PT_Validar_Acceso()
        End If
    End Sub

    Private Sub GEOCATMIN_IniLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Pintar_Formulario()
    End Sub
End Class