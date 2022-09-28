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
    Public clsData As New cls_Utilidades
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
    Friend WithEvents TXTUSUARIO As System.Windows.Forms.TextBox
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
        Me.lblNombreUsuario = New System.Windows.Forms.Label()
        Me.lblContrasena = New System.Windows.Forms.Label()
        Me.TXTUSUARIO = New System.Windows.Forms.TextBox()
        Me.txtContrasena = New System.Windows.Forms.TextBox()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.lblMensaje = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.gpo_Imagen = New System.Windows.Forms.GroupBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
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
        Me.lblNombreUsuario.Location = New System.Drawing.Point(26, 12)
        Me.lblNombreUsuario.Name = "lblNombreUsuario"
        Me.lblNombreUsuario.Size = New System.Drawing.Size(166, 33)
        Me.lblNombreUsuario.TabIndex = 0
        Me.lblNombreUsuario.Text = "Usuario"
        Me.lblNombreUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblContrasena
        '
        Me.lblContrasena.BackColor = System.Drawing.Color.Transparent
        Me.lblContrasena.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContrasena.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblContrasena.Location = New System.Drawing.Point(26, 82)
        Me.lblContrasena.Name = "lblContrasena"
        Me.lblContrasena.Size = New System.Drawing.Size(102, 33)
        Me.lblContrasena.TabIndex = 1
        Me.lblContrasena.Text = "Contraseña"
        Me.lblContrasena.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TXTUSUARIO
        '
        Me.TXTUSUARIO.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTUSUARIO.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.TXTUSUARIO.Location = New System.Drawing.Point(26, 47)
        Me.TXTUSUARIO.Name = "TXTUSUARIO"
        Me.TXTUSUARIO.Size = New System.Drawing.Size(179, 27)
        Me.TXTUSUARIO.TabIndex = 2
        '
        'txtContrasena
        '
        Me.txtContrasena.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContrasena.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtContrasena.Location = New System.Drawing.Point(26, 117)
        Me.txtContrasena.Name = "txtContrasena"
        Me.txtContrasena.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtContrasena.Size = New System.Drawing.Size(179, 27)
        Me.txtContrasena.TabIndex = 3
        '
        'btnAceptar
        '
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Image)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAceptar.Location = New System.Drawing.Point(262, 12)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(166, 41)
        Me.btnAceptar.TabIndex = 4
        '
        'btnCancelar
        '
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancelar.Location = New System.Drawing.Point(90, 6)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(166, 41)
        Me.btnCancelar.TabIndex = 5
        '
        'lblMensaje
        '
        Me.lblMensaje.BackColor = System.Drawing.Color.Transparent
        Me.lblMensaje.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMensaje.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblMensaje.Location = New System.Drawing.Point(26, 118)
        Me.lblMensaje.Name = "lblMensaje"
        Me.lblMensaje.Size = New System.Drawing.Size(448, 24)
        Me.lblMensaje.TabIndex = 6
        Me.lblMensaje.Text = "Ingrese usuario, contraseña y luego presione 'Aceptar'"
        Me.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(243, 153)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(200, 141)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblContrasena)
        Me.GroupBox1.Controls.Add(Me.lblNombreUsuario)
        Me.GroupBox1.Controls.Add(Me.txtContrasena)
        Me.GroupBox1.Controls.Add(Me.TXTUSUARIO)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 142)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(448, 163)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnCancelar)
        Me.GroupBox2.Controls.Add(Me.btnAceptar)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 305)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(448, 59)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        '
        'gpo_Imagen
        '
        Me.gpo_Imagen.Controls.Add(Me.PictureBox2)
        Me.gpo_Imagen.Location = New System.Drawing.Point(5, 3)
        Me.gpo_Imagen.Name = "gpo_Imagen"
        Me.gpo_Imagen.Size = New System.Drawing.Size(448, 111)
        Me.gpo_Imagen.TabIndex = 152
        Me.gpo_Imagen.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(10, 18)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(433, 86)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 0
        Me.PictureBox2.TabStop = False
        '
        'GEOCATMIN_IniLogin
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(8, 19)
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(464, 381)
        Me.Controls.Add(Me.gpo_Imagen)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.lblMensaje)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(474, 419)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(474, 419)
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
        'PT_Validar_Acceso2()
    End Sub
    Public Sub Pintar_Formulario()
        Me.BackColor = Drawing.Color.FromArgb(242, 242, 240)
    End Sub
    Private Sub PT_Validar_Acceso_PROD()
        Dim lostrContrasenaInvalida As String
        pgloUsuConexionOracle = clsData.ConxGis(TXTUSUARIO.Text, txtContrasena.Text, gstrDatabase)
        lostrContrasenaInvalida = ContrasenaInvalida(Trim(TXTUSUARIO.Text), Trim(txtContrasena.Text))
        'Asegurarse de que el usuario ingrese el Nombre de Usuario y Contraseña
        If TXTUSUARIO.Text.Length = 0 Then
            MsgBox("Debe ingresar un Usuario", MsgBoxStyle.Exclamation, "SIGCATMIN")
            TXTUSUARIO.Focus()
        ElseIf txtContrasena.Text.Length = 0 Then
            MsgBox("Debe ingresar una Contraseña", MsgBoxStyle.Exclamation, "SIGCATMIN")
            txtContrasena.Focus()
        ElseIf lostrContrasenaInvalida = "1" Then
            'El Nombre de Usuario/Contraseña es inválido
            MsgBox("Nombre de Usuario/Contraseña inválida", MsgBoxStyle.Exclamation, "SIGCATMIN")
            TXTUSUARIO.Focus()
        ElseIf lostrContrasenaInvalida = "2" Then
            MsgBox("No tiene permiso para acceder al Sistema", MsgBoxStyle.Exclamation, "SIGCATMIN")
            TXTUSUARIO.Focus()
        ElseIf lostrContrasenaInvalida = "3" Then
            MsgBox("Error de Conexión a la Base de Datos Oracle, ingrese Usuario y Clave válidos " & vbNewLine & "Intente de Nuevo", MsgBoxStyle.Information, "[BDGeocatmin]")
            TXTUSUARIO.Focus()
        Else
            gstrUsuarioAcceso = Me.TXTUSUARIO.Text
            gstrUsuarioClave = Me.txtContrasena.Text

            pgloUsuConexionGIS = clsData.ConxGis(TXTUSUARIO.Text, txtContrasena.Text, gstrDatabaseGIS)
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
                'Case "DESA"
                Case "GAMMAD"
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

    Private Sub PT_Validar_Acceso()  'CAMBIAR PT_Validar_Acceso_PROD POR PT_Validar_Acceso PARA EJECUTAR
        val_acceso = ""
        Dim lostrContrasenaInvalida As String
        pgloUsuConexionOracle = clsData.ConxGis(TXTUSUARIO.Text, txtContrasena.Text, gstrDatabase)
        lostrContrasenaInvalida = ContrasenaInvalida(Trim(TXTUSUARIO.Text), Trim(txtContrasena.Text))
        'Asegurarse de que el usuario ingrese el Nombre de Usuario y Contraseña
        If TXTUSUARIO.Text.Length = 0 Then
            MsgBox("Debe ingresar un Usuario", MsgBoxStyle.Exclamation, "[SIGCATMIN]")
            TXTUSUARIO.Focus()
        ElseIf txtContrasena.Text.Length = 0 Then
            MsgBox("Debe ingresar una Contraseña", MsgBoxStyle.Exclamation, "[SIGCATMIN]")
            txtContrasena.Focus()
        ElseIf lostrContrasenaInvalida = "1" Then
            'El Nombre de Usuario/Contraseña es inválido
            MsgBox("Nombre de Usuario/Contraseña inválida", MsgBoxStyle.Exclamation, "[SIGCATMIN]")
            TXTUSUARIO.Focus()
        ElseIf lostrContrasenaInvalida = "2" Then
            MsgBox("No tiene permiso para acceder al Sistema", MsgBoxStyle.Exclamation, "[SIGCATMIN]")
            TXTUSUARIO.Focus()
        ElseIf lostrContrasenaInvalida = "3" Then
            MsgBox("Error de Conexión a la Base de Datos Oracle, ingrese Usuario y Clave válidos " & vbNewLine & "Intente de Nuevo", MsgBoxStyle.Information, "[SIGCATMIN]")
            TXTUSUARIO.Focus()
        Else
            gstrUsuarioAcceso = Me.TXTUSUARIO.Text
            gstrUsuarioClave = Me.txtContrasena.Text
            pgloUsuConexionGIS = clsData.ConxGis(TXTUSUARIO.Text, txtContrasena.Text, gstrDatabaseGIS)
            val_acceso = "1"
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
                ' Case "DESA"
                Case "GAMMA02"
                    'Case "GAMMAD"
                    glo_Desarrollo_BD = "Srv BD: Desarrollo / SrvGIS: Produccion"
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


    Private Sub PT_Validar_Acceso2()  'CAMBIAR PT_Validar_Acceso_PROD POR PT_Validar_Acceso PARA EJECUTAR
        Dim lostrContrasenaInvalida As String
        pgloUsuConexionOracle = clsData.ConxGis(TXTUSUARIO.Text, txtContrasena.Text, gstrDatabase)
        lostrContrasenaInvalida = ContrasenaInvalida(Trim(TXTUSUARIO.Text), Trim(txtContrasena.Text))
        'Asegurarse de que el usuario ingrese el Nombre de Usuario y Contraseña
        If TXTUSUARIO.Text.Length = 0 Then
            MsgBox("Debe ingresar un Usuario", MsgBoxStyle.Exclamation, "[BDGeocatmin]")
            TXTUSUARIO.Focus()
        ElseIf txtContrasena.Text.Length = 0 Then
            MsgBox("Debe ingresar una Contraseña", MsgBoxStyle.Exclamation, "[BDGeocatmin]")
            txtContrasena.Focus()
        ElseIf lostrContrasenaInvalida = "1" Then
            'El Nombre de Usuario/Contraseña es inválido
            MsgBox("Nombre de Usuario/Contraseña inválida", MsgBoxStyle.Exclamation, "[BDGeocatmin]")
            TXTUSUARIO.Focus()
        ElseIf lostrContrasenaInvalida = "2" Then
            MsgBox("No tiene permiso para acceder al Sistema", MsgBoxStyle.Exclamation, "[BDGeocatmin]")
            TXTUSUARIO.Focus()
        ElseIf lostrContrasenaInvalida = "3" Then
            MsgBox("Error de Conexión a la Base de Datos Oracle, ingrese Usuario y Clave válidos " & vbNewLine & "Intente de Nuevo", MsgBoxStyle.Information, "[BDGeocatmin]")
            TXTUSUARIO.Focus()
        Else
            gstrUsuarioAcceso = Me.TXTUSUARIO.Text
            gstrUsuarioClave = Me.txtContrasena.Text
            pgloUsuConexionGIS = clsData.ConxGis(TXTUSUARIO.Text, txtContrasena.Text, gstrDatabaseGIS)

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
                ' Case "DESA"
                Case "GAMMAD"
                    glo_Desarrollo_BD = "Srv BD: Desarrollo / SrvGIS: Produccion"
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


    Public Sub PT_Validar_Acceso1()

        'ESTO PARA CONECTARSE DIRECTAMENTE FECHA 03/11/2014
        Dim lostrValidando As String = ""
        Dim m_Codigo_NumReg As Integer
        Dim clave_valida As Boolean
        If Me.TXTUSUARIO.Text.Length = 0 Then



            MsgBox("Debe ingresar Usuario", MsgBoxStyle.Exclamation, "SIGCATMIN")
            Me.TXTUSUARIO.Focus()
            Exit Sub
        ElseIf Me.txtContrasena.Text.Length = 0 Then
            MsgBox("Debe ingresar Contraseña", MsgBoxStyle.Exclamation, "SIGCATMIN")
            txtContrasena.Focus()
            Exit Sub
        Else
            gstrUsuarioAcceso = Me.TXTUSUARIO.Text.ToUpper
            gstrUsuarioClave = Me.txtContrasena.Text.ToUpper
            glo_User_SDE = Me.TXTUSUARIO.Text.ToUpper
            glo_Password_SDE = Me.txtContrasena.Text.ToUpper

        End If
        Try
            pPropset = New PropertySet


            '  pgloUsuConexionGIS = clsData.ConxGis(TXTUSUARIO.Text, txtContrasena.Text, gstrDatabaseGIS)

            Dim c_usuario As String
            glo_User_SDE = Me.TXTUSUARIO.Text.ToUpper
            glo_Password_SDE = Me.txtContrasena.Text.ToUpper

            pPropset.SetProperty("CONNECTSTRING", "Provider = MSDAORA.1; Data source = ORACLE; User ID = " & _
                                  glo_User_SDE & ";Password = " & glo_Password_SDE)
            'Creando sentencias del OLEDB
            Dim pWorkspaceFact As IWorkspaceFactory
            pWorkspaceFact = New OLEDBWorkspaceFactory
            pWorkspace = pWorkspaceFact.Open(pPropset, 0)
            Dim pFeatureWorkspace As IFeatureWorkspace
            pFeatureWorkspace = pWorkspace
            Dim pQueryDef As IQueryDef
            pQueryDef = pFeatureWorkspace.CreateQueryDef
            'consulta
            pQueryDef.Tables = " SG_M_USUARIOS "
            pQueryDef.SubFields = " US_LOGUSE, US_NOMUSE "
            pQueryDef.WhereClause = " US_LOGUSE = '" & gstrUsuarioAcceso & "'"
            Dim cursor_filas As ICursor
            Dim filas_dm As IRow
            cursor_filas = pQueryDef.Evaluate
            filas_dm = cursor_filas.NextRow
            Dim i As Long
            i = 0
            m_Codigo_NumReg = i
            Do Until filas_dm Is Nothing
                c_usuario = filas_dm.Value(0) & ""
                c_user_name = filas_dm.Value(1) & ""
                i = i + 1
                filas_dm = cursor_filas.NextRow
                m_Codigo_NumReg = i
            Loop
            If m_Codigo_NumReg = 0 Then
                MsgBox("Usuario es Incorrecto para realizar la consulta", vbCritical, "OBSERVACION...")
                Exit Sub
            End If
            clave_valida = True


            If clave_valida = True Then

                DialogResult = Windows.Forms.DialogResult.OK ''
            End If

        Catch ex As Exception
            clave_valida = False
            MsgBox("El Usuario y/o Password es incorrecto para acceder a la Aplicación," & _
                   "Verificar si sus Datos son Correctos", vbInformation, "[ SIGCATMIN ]")
            glo_Validado = 0
            Exit Sub
        End Try
        glo_Validado = 1

        '    Me.Close()
    End Sub


    Private Function ContrasenaInvalida(ByVal pastrUsuario As String, ByVal pastrContrasena As String) As String
        Dim cls_Conexion As New cls_Oracle ' clsBD_Seguridad
        Dim lodtbUsuario As New DataTable
        Dim lodtbAcceso As New DataTable
        Try
            lodtbUsuario = cls_Conexion.F_Verifica_Usuario(TXTUSUARIO.Text.ToUpper, pgloUsuConexionOracle)
            If lodtbUsuario.Rows.Count = 1 Then
                gstrCodigo_Usuario = lodtbUsuario.Rows(0).Item("USUARIO")
                gstrNombre_Usuario = Texto_Alta_Baja(lodtbUsuario.Rows(0).Item("USERNAME"))
                gstr_Codigo_Oficina = lodtbUsuario.Rows(0).Item("OFICINA")


                'gstrCodigo_Usuario = "CQUI0543"
                'gstrNombre_Usuario = "CQUI0545"
                'gstr_Codigo_Oficina = "01"

                '   gstr_Codigo_Oficina = "65"
                'VALIDAD USUARIO DE REGIONES 'AUMENTADO ULTIMO
                ' Lima = 15     Tumbes = 24       Huancavelica = 09    Apurimac = 53
                '   gstr_Codigo_Oficina = "59"  'probando con oficina de prueba,luego comentar la linea
                Select Case gstr_Codigo_Oficina
                    Case "51"
                        cd_region_sele = "01"
                    Case "52"
                        cd_region_sele = "02"
                    Case "53"
                        cd_region_sele = "03"
                    Case "54"
                        cd_region_sele = "04"
                    Case "55"
                        cd_region_sele = "05"
                    Case "56"
                        cd_region_sele = "06"
                    Case "57"
                        cd_region_sele = "07"
                    Case "58"
                        cd_region_sele = "08"
                    Case "59"
                        cd_region_sele = "09"
                    Case "60"
                        cd_region_sele = "10"
                    Case "61"
                        cd_region_sele = "11"
                    Case "62"
                        cd_region_sele = "12"
                    Case "63"
                        cd_region_sele = "13"
                    Case "64"
                        cd_region_sele = "14"
                    Case "65"
                        cd_region_sele = "15"
                    Case "66"
                        cd_region_sele = "16"
                    Case "67"
                        cd_region_sele = "17"
                    Case "68"
                        cd_region_sele = "18"
                    Case "69"
                        cd_region_sele = "19"
                    Case "70"
                        cd_region_sele = "20"
                    Case "71"
                        cd_region_sele = "21"
                    Case "72"
                        cd_region_sele = "22"
                    Case "73"
                        cd_region_sele = "23"
                    Case "74"
                        cd_region_sele = "24"
                    Case "75"
                        cd_region_sele = "25"
                    Case "76"
                        cd_region_sele = "26"
                    Case Else
                        cd_region_sele = "00"  'PARA LIMA
                        cd_region_encontrado = "00"
                End Select
            End If

        Catch Ex As Exception
            Return "3"
        End Try
    End Function

    Private Sub txtContrasena_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtContrasena.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            PT_Validar_Acceso()
            'PT_Validar_Acceso_ANT()
        End If
    End Sub

    Private Sub GEOCATMIN_IniLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Pintar_Formulario()
    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub
End Class