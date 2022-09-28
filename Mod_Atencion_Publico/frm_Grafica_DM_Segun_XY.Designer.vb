<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Grafica_DM_Segun_XY
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Grafica_DM_Segun_XY))
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.btnGraficar = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.imgCarta = New System.Windows.Forms.PictureBox
        Me.cboZona = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtRadio = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtNorte = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtEste = New System.Windows.Forms.TextBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lblUsuario = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        CType(Me.imgCarta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCerrar
        '
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Image)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCerrar.Location = New System.Drawing.Point(222, 285)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(104, 26)
        Me.btnCerrar.TabIndex = 136
        '
        'btnGraficar
        '
        Me.btnGraficar.Image = CType(resources.GetObject("btnGraficar.Image"), System.Drawing.Image)
        Me.btnGraficar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnGraficar.Location = New System.Drawing.Point(332, 285)
        Me.btnGraficar.Name = "btnGraficar"
        Me.btnGraficar.Size = New System.Drawing.Size(104, 26)
        Me.btnGraficar.TabIndex = 135
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.imgCarta)
        Me.GroupBox1.Controls.Add(Me.cboZona)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtRadio)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtNorte)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtEste)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 84)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(430, 195)
        Me.GroupBox1.TabIndex = 133
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Datos de la Coordenada"
        '
        'imgCarta
        '
        Me.imgCarta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.imgCarta.Image = Global.SIGCATMIN.My.Resources.Resources.Grafico_SegunXY
        Me.imgCarta.Location = New System.Drawing.Point(244, 10)
        Me.imgCarta.Name = "imgCarta"
        Me.imgCarta.Size = New System.Drawing.Size(180, 180)
        Me.imgCarta.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgCarta.TabIndex = 13
        Me.imgCarta.TabStop = False
        '
        'cboZona
        '
        Me.cboZona.FormattingEnabled = True
        Me.cboZona.Items.AddRange(New Object() {"17", "18", "19"})
        Me.cboZona.Location = New System.Drawing.Point(116, 128)
        Me.cboZona.Name = "cboZona"
        Me.cboZona.Size = New System.Drawing.Size(52, 21)
        Me.cboZona.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(31, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Radio (Km.):"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(31, 131)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Zona:"
        '
        'txtRadio
        '
        Me.txtRadio.Location = New System.Drawing.Point(116, 91)
        Me.txtRadio.MaxLength = 2
        Me.txtRadio.Name = "txtRadio"
        Me.txtRadio.Size = New System.Drawing.Size(36, 20)
        Me.txtRadio.TabIndex = 7
        Me.txtRadio.Text = "10"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(31, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Este (x):"
        '
        'txtNorte
        '
        Me.txtNorte.Location = New System.Drawing.Point(116, 57)
        Me.txtNorte.MaxLength = 7
        Me.txtNorte.Name = "txtNorte"
        Me.txtNorte.Size = New System.Drawing.Size(52, 20)
        Me.txtNorte.TabIndex = 5
        Me.txtNorte.Text = "8640000"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(31, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Norte (y):"
        '
        'txtEste
        '
        Me.txtEste.Location = New System.Drawing.Point(116, 23)
        Me.txtEste.MaxLength = 6
        Me.txtEste.Name = "txtEste"
        Me.txtEste.Size = New System.Drawing.Size(52, 20)
        Me.txtEste.TabIndex = 1
        Me.txtEste.Text = "455700"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(430, 65)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'lblUsuario
        '
        Me.lblUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsuario.Location = New System.Drawing.Point(3, 309)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(215, 13)
        Me.lblUsuario.TabIndex = 137
        Me.lblUsuario.Text = "lblUsuario"
        '
        'frm_Grafica_DM_Segun_XY
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(443, 332)
        Me.Controls.Add(Me.lblUsuario)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.btnGraficar)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frm_Grafica_DM_Segun_XY"
        Me.Text = "[GEOCATMIN]  Graficar_DM_Segun_XY"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.imgCarta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnGraficar As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNorte As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtEste As System.Windows.Forms.TextBox
    Friend WithEvents cboZona As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtRadio As System.Windows.Forms.TextBox
    Friend WithEvents imgCarta As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblUsuario As System.Windows.Forms.Label
End Class
