<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Reporte
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Reporte))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cboReporte = New System.Windows.Forms.ComboBox()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.rb_Repo1 = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.rb_Repo4 = New System.Windows.Forms.RadioButton()
        Me.rb_Repo3 = New System.Windows.Forms.RadioButton()
        Me.rb_Repo2 = New System.Windows.Forms.RadioButton()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cboReporte)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 473)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(241, 81)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'cboReporte
        '
        Me.cboReporte.FormattingEnabled = True
        Me.cboReporte.Items.AddRange(New Object() {"---------  Seleccionar  ---------", "Reporte de Derechos Mineros", "Reporte de Areas Disponibles", "Reporte DM Según Plano de Evaluación"})
        Me.cboReporte.Location = New System.Drawing.Point(9, 19)
        Me.cboReporte.Name = "cboReporte"
        Me.cboReporte.Size = New System.Drawing.Size(219, 21)
        Me.cboReporte.TabIndex = 0
        '
        'btnCerrar
        '
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Image)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCerrar.Location = New System.Drawing.Point(117, 215)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(104, 26)
        Me.btnCerrar.TabIndex = 154
        '
        'btnAceptar
        '
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Image)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAceptar.Location = New System.Drawing.Point(224, 214)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(104, 28)
        Me.btnAceptar.TabIndex = 155
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(4, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(325, 59)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 146
        Me.PictureBox1.TabStop = False
        '
        'rb_Repo1
        '
        Me.rb_Repo1.AutoSize = True
        Me.rb_Repo1.Checked = True
        Me.rb_Repo1.Location = New System.Drawing.Point(58, 30)
        Me.rb_Repo1.Name = "rb_Repo1"
        Me.rb_Repo1.Size = New System.Drawing.Size(167, 17)
        Me.rb_Repo1.TabIndex = 156
        Me.rb_Repo1.TabStop = True
        Me.rb_Repo1.Text = "Reporte de Derechos Mineros"
        Me.rb_Repo1.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rb_Repo4)
        Me.GroupBox3.Controls.Add(Me.rb_Repo3)
        Me.GroupBox3.Controls.Add(Me.rb_Repo2)
        Me.GroupBox3.Controls.Add(Me.rb_Repo1)
        Me.GroupBox3.Location = New System.Drawing.Point(4, 70)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(324, 138)
        Me.GroupBox3.TabIndex = 157
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Seleccione Reporte"
        '
        'rb_Repo4
        '
        Me.rb_Repo4.AutoSize = True
        Me.rb_Repo4.Location = New System.Drawing.Point(58, 80)
        Me.rb_Repo4.Name = "rb_Repo4"
        Me.rb_Repo4.Size = New System.Drawing.Size(190, 17)
        Me.rb_Repo4.TabIndex = 159
        Me.rb_Repo4.Text = "Reporte Resultados de Evaluación"
        Me.rb_Repo4.UseVisualStyleBackColor = True
        '
        'rb_Repo3
        '
        Me.rb_Repo3.AutoSize = True
        Me.rb_Repo3.Location = New System.Drawing.Point(58, 55)
        Me.rb_Repo3.Name = "rb_Repo3"
        Me.rb_Repo3.Size = New System.Drawing.Size(218, 17)
        Me.rb_Repo3.TabIndex = 158
        Me.rb_Repo3.Text = "Reporte D.M. Según Plano de Evaluaión"
        Me.rb_Repo3.UseVisualStyleBackColor = True
        '
        'rb_Repo2
        '
        Me.rb_Repo2.AutoSize = True
        Me.rb_Repo2.Location = New System.Drawing.Point(58, 105)
        Me.rb_Repo2.Name = "rb_Repo2"
        Me.rb_Repo2.Size = New System.Drawing.Size(165, 17)
        Me.rb_Repo2.TabIndex = 157
        Me.rb_Repo2.Text = "Reporte de Áreas Disponibles"
        Me.rb_Repo2.UseVisualStyleBackColor = True
        '
        'Frm_Reporte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(333, 244)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "Frm_Reporte"
        Me.Text = "Frm_Reporte"
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents cboReporte As System.Windows.Forms.ComboBox
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents rb_Repo1 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_Repo4 As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Repo3 As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Repo2 As System.Windows.Forms.RadioButton
End Class
