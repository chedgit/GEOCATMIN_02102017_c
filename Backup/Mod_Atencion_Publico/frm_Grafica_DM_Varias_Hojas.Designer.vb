<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Grafica_DM_Varias_Hojas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Grafica_DM_Varias_Hojas))
        Dim Label1 As System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lstHojas = New System.Windows.Forms.ListBox
        Me.xMax = New System.Windows.Forms.TextBox
        Me.yMin = New System.Windows.Forms.TextBox
        Me.xMin = New System.Windows.Forms.TextBox
        Me.yMax = New System.Windows.Forms.TextBox
        Me.txtExiste = New System.Windows.Forms.TextBox
        Me.gpo_Imagen = New System.Windows.Forms.GroupBox
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.btnGraficar = New System.Windows.Forms.Button
        Me.lblUsuario = New System.Windows.Forms.Label
        Label1 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.gpo_Imagen.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lstHojas)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 94)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(234, 201)
        Me.GroupBox1.TabIndex = 140
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Seleccionar Grupo"
        '
        'lstHojas
        '
        Me.lstHojas.FormattingEnabled = True
        Me.lstHojas.Location = New System.Drawing.Point(7, 20)
        Me.lstHojas.Name = "lstHojas"
        Me.lstHojas.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstHojas.Size = New System.Drawing.Size(219, 173)
        Me.lstHojas.TabIndex = 0
        '
        'xMax
        '
        Me.xMax.Location = New System.Drawing.Point(24, 318)
        Me.xMax.Name = "xMax"
        Me.xMax.ReadOnly = True
        Me.xMax.Size = New System.Drawing.Size(10, 20)
        Me.xMax.TabIndex = 147
        Me.xMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'yMin
        '
        Me.yMin.Location = New System.Drawing.Point(14, 318)
        Me.yMin.Name = "yMin"
        Me.yMin.ReadOnly = True
        Me.yMin.Size = New System.Drawing.Size(10, 20)
        Me.yMin.TabIndex = 146
        Me.yMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'xMin
        '
        Me.xMin.Location = New System.Drawing.Point(44, 318)
        Me.xMin.Name = "xMin"
        Me.xMin.ReadOnly = True
        Me.xMin.Size = New System.Drawing.Size(10, 20)
        Me.xMin.TabIndex = 145
        Me.xMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'yMax
        '
        Me.yMax.Location = New System.Drawing.Point(34, 318)
        Me.yMax.Name = "yMax"
        Me.yMax.ReadOnly = True
        Me.yMax.Size = New System.Drawing.Size(10, 20)
        Me.yMax.TabIndex = 144
        Me.yMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtExiste
        '
        Me.txtExiste.Location = New System.Drawing.Point(12, 428)
        Me.txtExiste.Name = "txtExiste"
        Me.txtExiste.Size = New System.Drawing.Size(10, 20)
        Me.txtExiste.TabIndex = 148
        '
        'gpo_Imagen
        '
        Me.gpo_Imagen.Controls.Add(Label1)
        Me.gpo_Imagen.Location = New System.Drawing.Point(4, 7)
        Me.gpo_Imagen.Name = "gpo_Imagen"
        Me.gpo_Imagen.Size = New System.Drawing.Size(366, 76)
        Me.gpo_Imagen.TabIndex = 149
        Me.gpo_Imagen.TabStop = False
        '
        'btnCerrar
        '
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Image)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCerrar.Location = New System.Drawing.Point(276, 146)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(104, 26)
        Me.btnCerrar.TabIndex = 142
        '
        'btnGraficar
        '
        Me.btnGraficar.Image = CType(resources.GetObject("btnGraficar.Image"), System.Drawing.Image)
        Me.btnGraficar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnGraficar.Location = New System.Drawing.Point(276, 114)
        Me.btnGraficar.Name = "btnGraficar"
        Me.btnGraficar.Size = New System.Drawing.Size(104, 26)
        Me.btnGraficar.TabIndex = 141
        '
        'lblUsuario
        '
        Me.lblUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsuario.Location = New System.Drawing.Point(4, 302)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(215, 13)
        Me.lblUsuario.TabIndex = 150
        Me.lblUsuario.Text = "lblUsuario"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Label1.Location = New System.Drawing.Point(131, 40)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(84, 13)
        Label1.TabIndex = 0
        Label1.Text = "SIMULTANEOS"
        '
        'frm_Grafica_DM_Varias_Hojas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(392, 317)
        Me.Controls.Add(Me.lblUsuario)
        Me.Controls.Add(Me.gpo_Imagen)
        Me.Controls.Add(Me.txtExiste)
        Me.Controls.Add(Me.xMax)
        Me.Controls.Add(Me.yMin)
        Me.Controls.Add(Me.xMin)
        Me.Controls.Add(Me.yMax)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.btnGraficar)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frm_Grafica_DM_Varias_Hojas"
        Me.Text = "Graficar DM  SIMULTANENOS"
        Me.GroupBox1.ResumeLayout(False)
        Me.gpo_Imagen.ResumeLayout(False)
        Me.gpo_Imagen.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnGraficar As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lstHojas As System.Windows.Forms.ListBox
    Friend WithEvents xMax As System.Windows.Forms.TextBox
    Friend WithEvents yMin As System.Windows.Forms.TextBox
    Friend WithEvents xMin As System.Windows.Forms.TextBox
    Friend WithEvents yMax As System.Windows.Forms.TextBox
    Friend WithEvents txtExiste As System.Windows.Forms.TextBox
    Friend WithEvents gpo_Imagen As System.Windows.Forms.GroupBox
    Friend WithEvents lblUsuario As System.Windows.Forms.Label
End Class
