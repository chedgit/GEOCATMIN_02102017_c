<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Grafica_Area_Reserva
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Grafica_Area_Reserva))
        Me.lblUsuario = New System.Windows.Forms.Label
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.gbxDatosUbicacion = New System.Windows.Forms.GroupBox
        Me.lsvListaAreas = New System.Windows.Forms.ListView
        Me.gbxDatosGenerales = New System.Windows.Forms.GroupBox
        Me.btnBuscar = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtConsulta = New System.Windows.Forms.TextBox
        Me.cboConsulta = New System.Windows.Forms.ComboBox
        Me.gpo_Imagen = New System.Windows.Forms.GroupBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.btnGraficar = New System.Windows.Forms.Button
        Me.txtExiste = New System.Windows.Forms.TextBox
        Me.Filtro = New System.Windows.Forms.TextBox
        Me.xMax = New System.Windows.Forms.TextBox
        Me.yMin = New System.Windows.Forms.TextBox
        Me.xMin = New System.Windows.Forms.TextBox
        Me.yMax = New System.Windows.Forms.TextBox
        Me.btnOtraConsulta = New System.Windows.Forms.Button
        Me.gbxDatosUbicacion.SuspendLayout()
        Me.gbxDatosGenerales.SuspendLayout()
        Me.gpo_Imagen.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblUsuario
        '
        Me.lblUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsuario.Location = New System.Drawing.Point(3, 349)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(215, 13)
        Me.lblUsuario.TabIndex = 160
        Me.lblUsuario.Text = "lblUsuario"
        '
        'btnCerrar
        '
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Image)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCerrar.Location = New System.Drawing.Point(405, 330)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(104, 26)
        Me.btnCerrar.TabIndex = 159
        '
        'gbxDatosUbicacion
        '
        Me.gbxDatosUbicacion.Controls.Add(Me.lsvListaAreas)
        Me.gbxDatosUbicacion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbxDatosUbicacion.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.gbxDatosUbicacion.Location = New System.Drawing.Point(4, 115)
        Me.gbxDatosUbicacion.Name = "gbxDatosUbicacion"
        Me.gbxDatosUbicacion.Size = New System.Drawing.Size(505, 209)
        Me.gbxDatosUbicacion.TabIndex = 155
        Me.gbxDatosUbicacion.TabStop = False
        '
        'lsvListaAreas
        '
        Me.lsvListaAreas.CheckBoxes = True
        Me.lsvListaAreas.FullRowSelect = True
        Me.lsvListaAreas.GridLines = True
        Me.lsvListaAreas.Location = New System.Drawing.Point(6, 20)
        Me.lsvListaAreas.Name = "lsvListaAreas"
        Me.lsvListaAreas.Size = New System.Drawing.Size(492, 183)
        Me.lsvListaAreas.TabIndex = 0
        Me.lsvListaAreas.UseCompatibleStateImageBehavior = False
        Me.lsvListaAreas.View = System.Windows.Forms.View.List
        '
        'gbxDatosGenerales
        '
        Me.gbxDatosGenerales.Controls.Add(Me.btnBuscar)
        Me.gbxDatosGenerales.Controls.Add(Me.Label3)
        Me.gbxDatosGenerales.Controls.Add(Me.Label2)
        Me.gbxDatosGenerales.Controls.Add(Me.txtConsulta)
        Me.gbxDatosGenerales.Controls.Add(Me.cboConsulta)
        Me.gbxDatosGenerales.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbxDatosGenerales.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.gbxDatosGenerales.Location = New System.Drawing.Point(4, 67)
        Me.gbxDatosGenerales.Name = "gbxDatosGenerales"
        Me.gbxDatosGenerales.Size = New System.Drawing.Size(505, 43)
        Me.gbxDatosGenerales.TabIndex = 154
        Me.gbxDatosGenerales.TabStop = False
        Me.gbxDatosGenerales.Text = "Datos Generales"
        '
        'btnBuscar
        '
        Me.btnBuscar.Image = CType(resources.GetObject("btnBuscar.Image"), System.Drawing.Image)
        Me.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnBuscar.Location = New System.Drawing.Point(411, 14)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(87, 26)
        Me.btnBuscar.TabIndex = 134
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(203, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 106
        Me.Label3.Text = "Ingrese Dato:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Buscar por:"
        '
        'txtConsulta
        '
        Me.txtConsulta.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtConsulta.Location = New System.Drawing.Point(282, 18)
        Me.txtConsulta.Name = "txtConsulta"
        Me.txtConsulta.Size = New System.Drawing.Size(119, 21)
        Me.txtConsulta.TabIndex = 105
        Me.txtConsulta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboConsulta
        '
        Me.cboConsulta.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.cboConsulta.FormattingEnabled = True
        Me.cboConsulta.Items.AddRange(New Object() {"CODIGO", "NOMBRE"})
        Me.cboConsulta.Location = New System.Drawing.Point(84, 18)
        Me.cboConsulta.Name = "cboConsulta"
        Me.cboConsulta.Size = New System.Drawing.Size(99, 21)
        Me.cboConsulta.TabIndex = 14
        '
        'gpo_Imagen
        '
        Me.gpo_Imagen.Controls.Add(Me.PictureBox1)
        Me.gpo_Imagen.Location = New System.Drawing.Point(4, -10)
        Me.gpo_Imagen.Name = "gpo_Imagen"
        Me.gpo_Imagen.Size = New System.Drawing.Size(505, 76)
        Me.gpo_Imagen.TabIndex = 153
        Me.gpo_Imagen.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(499, 59)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'btnGraficar
        '
        Me.btnGraficar.Image = CType(resources.GetObject("btnGraficar.Image"), System.Drawing.Image)
        Me.btnGraficar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnGraficar.Location = New System.Drawing.Point(295, 330)
        Me.btnGraficar.Name = "btnGraficar"
        Me.btnGraficar.Size = New System.Drawing.Size(104, 26)
        Me.btnGraficar.TabIndex = 161
        '
        'txtExiste
        '
        Me.txtExiste.Location = New System.Drawing.Point(95, 451)
        Me.txtExiste.Name = "txtExiste"
        Me.txtExiste.Size = New System.Drawing.Size(10, 20)
        Me.txtExiste.TabIndex = 167
        '
        'Filtro
        '
        Me.Filtro.Location = New System.Drawing.Point(76, 451)
        Me.Filtro.Name = "Filtro"
        Me.Filtro.ReadOnly = True
        Me.Filtro.Size = New System.Drawing.Size(13, 20)
        Me.Filtro.TabIndex = 166
        Me.Filtro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'xMax
        '
        Me.xMax.Location = New System.Drawing.Point(25, 451)
        Me.xMax.Name = "xMax"
        Me.xMax.ReadOnly = True
        Me.xMax.Size = New System.Drawing.Size(13, 20)
        Me.xMax.TabIndex = 165
        Me.xMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'yMin
        '
        Me.yMin.Location = New System.Drawing.Point(41, 451)
        Me.yMin.Name = "yMin"
        Me.yMin.ReadOnly = True
        Me.yMin.Size = New System.Drawing.Size(13, 20)
        Me.yMin.TabIndex = 164
        Me.yMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'xMin
        '
        Me.xMin.Location = New System.Drawing.Point(57, 451)
        Me.xMin.Name = "xMin"
        Me.xMin.ReadOnly = True
        Me.xMin.Size = New System.Drawing.Size(13, 20)
        Me.xMin.TabIndex = 163
        Me.xMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'yMax
        '
        Me.yMax.Location = New System.Drawing.Point(6, 451)
        Me.yMax.Name = "yMax"
        Me.yMax.ReadOnly = True
        Me.yMax.Size = New System.Drawing.Size(13, 20)
        Me.yMax.TabIndex = 162
        Me.yMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnOtraConsulta
        '
        Me.btnOtraConsulta.Image = CType(resources.GetObject("btnOtraConsulta.Image"), System.Drawing.Image)
        Me.btnOtraConsulta.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnOtraConsulta.Location = New System.Drawing.Point(185, 330)
        Me.btnOtraConsulta.Name = "btnOtraConsulta"
        Me.btnOtraConsulta.Size = New System.Drawing.Size(104, 26)
        Me.btnOtraConsulta.TabIndex = 168
        '
        'frm_Grafica_Area_Reserva
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(514, 369)
        Me.Controls.Add(Me.btnOtraConsulta)
        Me.Controls.Add(Me.txtExiste)
        Me.Controls.Add(Me.Filtro)
        Me.Controls.Add(Me.xMax)
        Me.Controls.Add(Me.yMin)
        Me.Controls.Add(Me.xMin)
        Me.Controls.Add(Me.yMax)
        Me.Controls.Add(Me.btnGraficar)
        Me.Controls.Add(Me.lblUsuario)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.gbxDatosUbicacion)
        Me.Controls.Add(Me.gbxDatosGenerales)
        Me.Controls.Add(Me.gpo_Imagen)
        Me.Name = "frm_Grafica_Area_Reserva"
        Me.Text = "Graficación de DM según  Area de Reserva"
        Me.gbxDatosUbicacion.ResumeLayout(False)
        Me.gbxDatosGenerales.ResumeLayout(False)
        Me.gbxDatosGenerales.PerformLayout()
        Me.gpo_Imagen.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblUsuario As System.Windows.Forms.Label
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents gbxDatosUbicacion As System.Windows.Forms.GroupBox
    Friend WithEvents gbxDatosGenerales As System.Windows.Forms.GroupBox
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtConsulta As System.Windows.Forms.TextBox
    Friend WithEvents cboConsulta As System.Windows.Forms.ComboBox
    Friend WithEvents gpo_Imagen As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnGraficar As System.Windows.Forms.Button
    Friend WithEvents lsvListaAreas As System.Windows.Forms.ListView
    Friend WithEvents txtExiste As System.Windows.Forms.TextBox
    Friend WithEvents Filtro As System.Windows.Forms.TextBox
    Friend WithEvents xMax As System.Windows.Forms.TextBox
    Friend WithEvents yMin As System.Windows.Forms.TextBox
    Friend WithEvents xMin As System.Windows.Forms.TextBox
    Friend WithEvents yMax As System.Windows.Forms.TextBox
    Friend WithEvents btnOtraConsulta As System.Windows.Forms.Button
End Class
