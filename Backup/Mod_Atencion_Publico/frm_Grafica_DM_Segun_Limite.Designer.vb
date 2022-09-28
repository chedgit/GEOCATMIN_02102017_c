<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Grafica_DM_Segun_Limite
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Grafica_DM_Segun_Limite))
        Me.txtEsteMax = New System.Windows.Forms.TextBox
        Me.txtEsteMin = New System.Windows.Forms.TextBox
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.btnGraficar = New System.Windows.Forms.Button
        Me.gpoEste = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.gpoNorte = New System.Windows.Forms.GroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtNorteMax = New System.Windows.Forms.TextBox
        Me.txtNorteMin = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboZona = New System.Windows.Forms.ComboBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lblUsuario = New System.Windows.Forms.Label
        Me.gpoEste.SuspendLayout()
        Me.gpoNorte.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtEsteMax
        '
        Me.txtEsteMax.Location = New System.Drawing.Point(221, 28)
        Me.txtEsteMax.MaxLength = 7
        Me.txtEsteMax.Name = "txtEsteMax"
        Me.txtEsteMax.Size = New System.Drawing.Size(52, 20)
        Me.txtEsteMax.TabIndex = 5
        Me.txtEsteMax.Text = "350000"
        '
        'txtEsteMin
        '
        Me.txtEsteMin.Location = New System.Drawing.Point(89, 28)
        Me.txtEsteMin.MaxLength = 6
        Me.txtEsteMin.Name = "txtEsteMin"
        Me.txtEsteMin.Size = New System.Drawing.Size(52, 20)
        Me.txtEsteMin.TabIndex = 1
        Me.txtEsteMin.Text = "300000"
        '
        'btnCerrar
        '
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Image)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCerrar.Location = New System.Drawing.Point(212, 229)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(104, 26)
        Me.btnCerrar.TabIndex = 142
        '
        'btnGraficar
        '
        Me.btnGraficar.Image = CType(resources.GetObject("btnGraficar.Image"), System.Drawing.Image)
        Me.btnGraficar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnGraficar.Location = New System.Drawing.Point(322, 229)
        Me.btnGraficar.Name = "btnGraficar"
        Me.btnGraficar.Size = New System.Drawing.Size(104, 26)
        Me.btnGraficar.TabIndex = 141
        '
        'gpoEste
        '
        Me.gpoEste.Controls.Add(Me.Label7)
        Me.gpoEste.Controls.Add(Me.Label2)
        Me.gpoEste.Controls.Add(Me.txtEsteMax)
        Me.gpoEste.Controls.Add(Me.txtEsteMin)
        Me.gpoEste.Location = New System.Drawing.Point(2, 85)
        Me.gpoEste.Name = "gpoEste"
        Me.gpoEste.Size = New System.Drawing.Size(430, 56)
        Me.gpoEste.TabIndex = 144
        Me.gpoEste.TabStop = False
        Me.gpoEste.Text = "Ingrese Coordenada Este:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(161, 31)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 13)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Este Max.:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(29, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Este Min.:"
        '
        'gpoNorte
        '
        Me.gpoNorte.Controls.Add(Me.Label8)
        Me.gpoNorte.Controls.Add(Me.Label9)
        Me.gpoNorte.Controls.Add(Me.txtNorteMax)
        Me.gpoNorte.Controls.Add(Me.txtNorteMin)
        Me.gpoNorte.Location = New System.Drawing.Point(2, 147)
        Me.gpoNorte.Name = "gpoNorte"
        Me.gpoNorte.Size = New System.Drawing.Size(430, 56)
        Me.gpoNorte.TabIndex = 145
        Me.gpoNorte.TabStop = False
        Me.gpoNorte.Text = "Ingrese Coordenada Norte:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(161, 31)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(62, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Norte Max.:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(29, 31)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 13)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Norte Min.:"
        '
        'txtNorteMax
        '
        Me.txtNorteMax.Location = New System.Drawing.Point(221, 28)
        Me.txtNorteMax.MaxLength = 7
        Me.txtNorteMax.Name = "txtNorteMax"
        Me.txtNorteMax.Size = New System.Drawing.Size(52, 20)
        Me.txtNorteMax.TabIndex = 5
        Me.txtNorteMax.Text = "8850000"
        '
        'txtNorteMin
        '
        Me.txtNorteMin.Location = New System.Drawing.Point(89, 28)
        Me.txtNorteMin.MaxLength = 7
        Me.txtNorteMin.Name = "txtNorteMin"
        Me.txtNorteMin.Size = New System.Drawing.Size(52, 20)
        Me.txtNorteMin.TabIndex = 1
        Me.txtNorteMin.Text = "8800000"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(31, 212)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Zona:"
        '
        'cboZona
        '
        Me.cboZona.FormattingEnabled = True
        Me.cboZona.Items.AddRange(New Object() {"17", "18", "19"})
        Me.cboZona.Location = New System.Drawing.Point(91, 209)
        Me.cboZona.Name = "cboZona"
        Me.cboZona.Size = New System.Drawing.Size(52, 21)
        Me.cboZona.TabIndex = 9
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(2, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(430, 65)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'lblUsuario
        '
        Me.lblUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsuario.Location = New System.Drawing.Point(5, 259)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(215, 13)
        Me.lblUsuario.TabIndex = 146
        Me.lblUsuario.Text = "lblUsuario"
        '
        'frm_Grafica_DM_Segun_Limite
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(438, 277)
        Me.Controls.Add(Me.lblUsuario)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.gpoNorte)
        Me.Controls.Add(Me.gpoEste)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.cboZona)
        Me.Controls.Add(Me.btnGraficar)
        Me.Controls.Add(Me.Label4)
        Me.Name = "frm_Grafica_DM_Segun_Limite"
        Me.Text = "Graficar DM según Límite"
        Me.gpoEste.ResumeLayout(False)
        Me.gpoEste.PerformLayout()
        Me.gpoNorte.ResumeLayout(False)
        Me.gpoNorte.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnGraficar As System.Windows.Forms.Button
    Friend WithEvents txtEsteMax As System.Windows.Forms.TextBox
    Friend WithEvents txtEsteMin As System.Windows.Forms.TextBox
    Friend WithEvents gpoEste As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents gpoNorte As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtNorteMax As System.Windows.Forms.TextBox
    Friend WithEvents txtNorteMin As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboZona As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblUsuario As System.Windows.Forms.Label
End Class
