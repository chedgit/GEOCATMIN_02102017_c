<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_GraficaPoligono
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_GraficaPoligono))
        Me.btnGenera_graficar = New System.Windows.Forms.Button()
        Me.btnLimpia = New System.Windows.Forms.Button()
        Me.btnElimina = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblConsultar = New System.Windows.Forms.Label()
        Me.txtNorte = New System.Windows.Forms.TextBox()
        Me.txtEste = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Cbotipo = New System.Windows.Forms.ComboBox()
        Me.lblZona = New System.Windows.Forms.Label()
        Me.cboZona = New System.Windows.Forms.ComboBox()
        Me.btnagregar = New System.Windows.Forms.Button()
        Me.lstCoordenada = New System.Windows.Forms.ListBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnGenera_graficar
        '
        Me.btnGenera_graficar.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.btnGenera_graficar.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenera_graficar.Location = New System.Drawing.Point(227, 171)
        Me.btnGenera_graficar.Name = "btnGenera_graficar"
        Me.btnGenera_graficar.Size = New System.Drawing.Size(68, 21)
        Me.btnGenera_graficar.TabIndex = 141
        Me.btnGenera_graficar.Text = "Graficar"
        Me.btnGenera_graficar.UseVisualStyleBackColor = True
        '
        'btnLimpia
        '
        Me.btnLimpia.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.btnLimpia.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLimpia.Location = New System.Drawing.Point(232, 74)
        Me.btnLimpia.Name = "btnLimpia"
        Me.btnLimpia.Size = New System.Drawing.Size(67, 21)
        Me.btnLimpia.TabIndex = 140
        Me.btnLimpia.Text = "Limpiar"
        Me.btnLimpia.UseVisualStyleBackColor = True
        '
        'btnElimina
        '
        Me.btnElimina.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.btnElimina.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnElimina.Location = New System.Drawing.Point(232, 47)
        Me.btnElimina.Name = "btnElimina"
        Me.btnElimina.Size = New System.Drawing.Size(67, 21)
        Me.btnElimina.TabIndex = 139
        Me.btnElimina.Text = "Eliminar"
        Me.btnElimina.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 9)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(307, 59)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 144
        Me.PictureBox1.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.lblConsultar)
        Me.GroupBox3.Controls.Add(Me.txtNorte)
        Me.GroupBox3.Controls.Add(Me.txtEste)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Cbotipo)
        Me.GroupBox3.Controls.Add(Me.lblZona)
        Me.GroupBox3.Controls.Add(Me.cboZona)
        Me.GroupBox3.Controls.Add(Me.btnagregar)
        Me.GroupBox3.Controls.Add(Me.lstCoordenada)
        Me.GroupBox3.Controls.Add(Me.btnGenera_graficar)
        Me.GroupBox3.Controls.Add(Me.btnElimina)
        Me.GroupBox3.Controls.Add(Me.btnLimpia)
        Me.GroupBox3.Location = New System.Drawing.Point(18, 79)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(308, 205)
        Me.GroupBox3.TabIndex = 153
        Me.GroupBox3.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(117, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 163
        Me.Label2.Text = "Norte"
        '
        'lblConsultar
        '
        Me.lblConsultar.AutoSize = True
        Me.lblConsultar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConsultar.Location = New System.Drawing.Point(7, 22)
        Me.lblConsultar.Name = "lblConsultar"
        Me.lblConsultar.Size = New System.Drawing.Size(31, 13)
        Me.lblConsultar.TabIndex = 162
        Me.lblConsultar.Text = "Este"
        '
        'txtNorte
        '
        Me.txtNorte.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNorte.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtNorte.Location = New System.Drawing.Point(159, 19)
        Me.txtNorte.MaxLength = 12
        Me.txtNorte.Name = "txtNorte"
        Me.txtNorte.Size = New System.Drawing.Size(65, 20)
        Me.txtNorte.TabIndex = 161
        Me.txtNorte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtEste
        '
        Me.txtEste.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtEste.Location = New System.Drawing.Point(44, 19)
        Me.txtEste.MaxLength = 11
        Me.txtEste.Name = "txtEste"
        Me.txtEste.Size = New System.Drawing.Size(65, 20)
        Me.txtEste.TabIndex = 160
        Me.txtEste.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(170, 136)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 159
        Me.Label1.Text = "Tipo:"
        '
        'Cbotipo
        '
        Me.Cbotipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cbotipo.FormattingEnabled = True
        Me.Cbotipo.Items.AddRange(New Object() {"-- Selec. --", "Poligono", "Circulo"})
        Me.Cbotipo.Location = New System.Drawing.Point(227, 128)
        Me.Cbotipo.Name = "Cbotipo"
        Me.Cbotipo.Size = New System.Drawing.Size(71, 21)
        Me.Cbotipo.TabIndex = 158
        '
        'lblZona
        '
        Me.lblZona.AutoSize = True
        Me.lblZona.Location = New System.Drawing.Point(170, 104)
        Me.lblZona.Name = "lblZona"
        Me.lblZona.Size = New System.Drawing.Size(35, 13)
        Me.lblZona.TabIndex = 157
        Me.lblZona.Text = "Zona:"
        '
        'cboZona
        '
        Me.cboZona.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboZona.FormattingEnabled = True
        Me.cboZona.Items.AddRange(New Object() {"-- Selec. --", "17", "18", "19"})
        Me.cboZona.Location = New System.Drawing.Point(227, 101)
        Me.cboZona.Name = "cboZona"
        Me.cboZona.Size = New System.Drawing.Size(71, 21)
        Me.cboZona.TabIndex = 156
        '
        'btnagregar
        '
        Me.btnagregar.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.btnagregar.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnagregar.Location = New System.Drawing.Point(230, 20)
        Me.btnagregar.Name = "btnagregar"
        Me.btnagregar.Size = New System.Drawing.Size(68, 21)
        Me.btnagregar.TabIndex = 155
        Me.btnagregar.Text = "Agregar"
        Me.btnagregar.UseVisualStyleBackColor = True
        '
        'lstCoordenada
        '
        Me.lstCoordenada.Font = New System.Drawing.Font("Tahoma", 6.45!, System.Drawing.FontStyle.Bold)
        Me.lstCoordenada.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lstCoordenada.FormattingEnabled = True
        Me.lstCoordenada.ItemHeight = 11
        Me.lstCoordenada.Location = New System.Drawing.Point(6, 45)
        Me.lstCoordenada.Name = "lstCoordenada"
        Me.lstCoordenada.Size = New System.Drawing.Size(149, 147)
        Me.lstCoordenada.TabIndex = 113
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(305, 74)
        Me.GroupBox1.TabIndex = 154
        Me.GroupBox1.TabStop = False
        '
        'Frm_GraficaPoligono
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(337, 292)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Name = "Frm_GraficaPoligono"
        Me.Text = "Graficación de Poligono"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnGenera_graficar As System.Windows.Forms.Button
    Friend WithEvents btnLimpia As System.Windows.Forms.Button
    Friend WithEvents btnElimina As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lstCoordenada As System.Windows.Forms.ListBox
    Friend WithEvents btnagregar As System.Windows.Forms.Button
    Friend WithEvents cboZona As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Cbotipo As System.Windows.Forms.ComboBox
    Friend WithEvents lblZona As System.Windows.Forms.Label
    Friend WithEvents txtNorte As System.Windows.Forms.TextBox
    Friend WithEvents txtEste As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblConsultar As System.Windows.Forms.Label
End Class
