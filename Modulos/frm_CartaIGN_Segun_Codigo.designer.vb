<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_CartaIGN_Segun_Codigo
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_CartaIGN_Segun_Codigo))
        Me.gpo_Imagen = New System.Windows.Forms.GroupBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.gbxDatosUbicacion = New System.Windows.Forms.GroupBox
        Me.img_DM = New System.Windows.Forms.PictureBox
        Me.btnGraficar = New System.Windows.Forms.Button
        Me.gbxDatosGenerales = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtConsulta = New System.Windows.Forms.TextBox
        Me.cboConsulta = New System.Windows.Forms.ComboBox
        Me.lblUsuario = New System.Windows.Forms.Label
        Me.gpo_Imagen.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbxDatosUbicacion.SuspendLayout()
        CType(Me.img_DM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbxDatosGenerales.SuspendLayout()
        Me.SuspendLayout()
        '
        'gpo_Imagen
        '
        Me.gpo_Imagen.Controls.Add(Me.PictureBox1)
        Me.gpo_Imagen.Location = New System.Drawing.Point(2, 1)
        Me.gpo_Imagen.Name = "gpo_Imagen"
        Me.gpo_Imagen.Size = New System.Drawing.Size(443, 76)
        Me.gpo_Imagen.TabIndex = 140
        Me.gpo_Imagen.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(433, 59)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'btnCerrar
        '
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Image)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCerrar.Location = New System.Drawing.Point(46, 408)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(104, 26)
        Me.btnCerrar.TabIndex = 146
        '
        'gbxDatosUbicacion
        '
        Me.gbxDatosUbicacion.Controls.Add(Me.img_DM)
        Me.gbxDatosUbicacion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbxDatosUbicacion.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.gbxDatosUbicacion.Location = New System.Drawing.Point(2, 127)
        Me.gbxDatosUbicacion.Name = "gbxDatosUbicacion"
        Me.gbxDatosUbicacion.Size = New System.Drawing.Size(443, 264)
        Me.gbxDatosUbicacion.TabIndex = 142
        Me.gbxDatosUbicacion.TabStop = False
        '
        'img_DM
        '
        Me.img_DM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.img_DM.Image = CType(resources.GetObject("img_DM.Image"), System.Drawing.Image)
        Me.img_DM.InitialImage = Nothing
        Me.img_DM.Location = New System.Drawing.Point(10, 20)
        Me.img_DM.Name = "img_DM"
        Me.img_DM.Size = New System.Drawing.Size(427, 238)
        Me.img_DM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.img_DM.TabIndex = 117
        Me.img_DM.TabStop = False
        '
        'btnGraficar
        '
        Me.btnGraficar.Image = CType(resources.GetObject("btnGraficar.Image"), System.Drawing.Image)
        Me.btnGraficar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnGraficar.Location = New System.Drawing.Point(330, 411)
        Me.btnGraficar.Name = "btnGraficar"
        Me.btnGraficar.Size = New System.Drawing.Size(104, 26)
        Me.btnGraficar.TabIndex = 144
        '
        'gbxDatosGenerales
        '
        Me.gbxDatosGenerales.Controls.Add(Me.Label3)
        Me.gbxDatosGenerales.Controls.Add(Me.Label2)
        Me.gbxDatosGenerales.Controls.Add(Me.txtConsulta)
        Me.gbxDatosGenerales.Controls.Add(Me.cboConsulta)
        Me.gbxDatosGenerales.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbxDatosGenerales.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.gbxDatosGenerales.Location = New System.Drawing.Point(2, 78)
        Me.gbxDatosGenerales.Name = "gbxDatosGenerales"
        Me.gbxDatosGenerales.Size = New System.Drawing.Size(443, 43)
        Me.gbxDatosGenerales.TabIndex = 141
        Me.gbxDatosGenerales.TabStop = False
        Me.gbxDatosGenerales.Text = "Datos Generales"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(241, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 106
        Me.Label3.Text = "Dato :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Consultar"
        '
        'txtConsulta
        '
        Me.txtConsulta.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtConsulta.Location = New System.Drawing.Point(318, 18)
        Me.txtConsulta.Name = "txtConsulta"
        Me.txtConsulta.Size = New System.Drawing.Size(119, 21)
        Me.txtConsulta.TabIndex = 105
        Me.txtConsulta.Text = "010164906"
        Me.txtConsulta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboConsulta
        '
        Me.cboConsulta.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.cboConsulta.FormattingEnabled = True
        Me.cboConsulta.Items.AddRange(New Object() {"CODIGO DM", "NOMBRE DM"})
        Me.cboConsulta.Location = New System.Drawing.Point(88, 18)
        Me.cboConsulta.Name = "cboConsulta"
        Me.cboConsulta.Size = New System.Drawing.Size(119, 21)
        Me.cboConsulta.TabIndex = 14
        Me.cboConsulta.Text = "CODIGO DM"
        '
        'lblUsuario
        '
        Me.lblUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsuario.Location = New System.Drawing.Point(4, 434)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(215, 13)
        Me.lblUsuario.TabIndex = 152
        Me.lblUsuario.Text = "lblUsuario"
        '
        'frm_CartaIGN_Segun_Codigo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(453, 453)
        Me.Controls.Add(Me.lblUsuario)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.gbxDatosUbicacion)
        Me.Controls.Add(Me.btnGraficar)
        Me.Controls.Add(Me.gbxDatosGenerales)
        Me.Controls.Add(Me.gpo_Imagen)
        Me.Name = "frm_CartaIGN_Segun_Codigo"
        Me.Text = "GRAFICAR DM SEGUN CARTA IGN"
        Me.gpo_Imagen.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbxDatosUbicacion.ResumeLayout(False)
        CType(Me.img_DM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbxDatosGenerales.ResumeLayout(False)
        Me.gbxDatosGenerales.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gpo_Imagen As System.Windows.Forms.GroupBox
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents gbxDatosUbicacion As System.Windows.Forms.GroupBox
    Friend WithEvents btnGraficar As System.Windows.Forms.Button
    Friend WithEvents gbxDatosGenerales As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtConsulta As System.Windows.Forms.TextBox
    Friend WithEvents cboConsulta As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblUsuario As System.Windows.Forms.Label
    Friend WithEvents img_DM As System.Windows.Forms.PictureBox
End Class
