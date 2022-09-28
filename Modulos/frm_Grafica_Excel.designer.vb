<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Grafica_Excel
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Grafica_Excel))
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnSelArchivo = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Cbotipo = New System.Windows.Forms.ComboBox()
        Me.lblZona = New System.Windows.Forms.Label()
        Me.cboZona = New System.Windows.Forms.ComboBox()
        Me.lstCoordenada = New System.Windows.Forms.ListBox()
        Me.btnLimpia = New System.Windows.Forms.Button()
        Me.dgdListaDM = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.btnGraficar = New System.Windows.Forms.Button()
        Me.GroupBox3.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelArchivo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgdListaDM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.PictureBox2)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.btnSelArchivo)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Cbotipo)
        Me.GroupBox3.Controls.Add(Me.lblZona)
        Me.GroupBox3.Controls.Add(Me.cboZona)
        Me.GroupBox3.Controls.Add(Me.lstCoordenada)
        Me.GroupBox3.Location = New System.Drawing.Point(4, 79)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(345, 231)
        Me.GroupBox3.TabIndex = 169
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Grafica Según Coordenadas. desde archivo Excel"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(209, 121)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 13)
        Me.Label2.TabIndex = 179
        Me.Label2.Text = " Estructura Excel"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(212, 137)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(136, 88)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 178
        Me.PictureBox2.TabStop = False
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(43, 27)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(154, 21)
        Me.Label12.TabIndex = 177
        Me.Label12.Text = "Seleccione Arcchivo Excel:"
        '
        'btnSelArchivo
        '
        Me.btnSelArchivo.Image = CType(resources.GetObject("btnSelArchivo.Image"), System.Drawing.Image)
        Me.btnSelArchivo.Location = New System.Drawing.Point(215, 20)
        Me.btnSelArchivo.Name = "btnSelArchivo"
        Me.btnSelArchivo.Size = New System.Drawing.Size(119, 28)
        Me.btnSelArchivo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnSelArchivo.TabIndex = 175
        Me.btnSelArchivo.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(216, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 159
        Me.Label1.Text = "Tipo:"
        '
        'Cbotipo
        '
        Me.Cbotipo.BackColor = System.Drawing.SystemColors.Window
        Me.Cbotipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cbotipo.FormattingEnabled = True
        Me.Cbotipo.Items.AddRange(New Object() {"-- Seleccione --", "PUNTO", "POLIGONO", "POLYLINEA"})
        Me.Cbotipo.Location = New System.Drawing.Point(252, 61)
        Me.Cbotipo.Name = "Cbotipo"
        Me.Cbotipo.Size = New System.Drawing.Size(87, 21)
        Me.Cbotipo.TabIndex = 158
        '
        'lblZona
        '
        Me.lblZona.AutoSize = True
        Me.lblZona.Location = New System.Drawing.Point(212, 103)
        Me.lblZona.Name = "lblZona"
        Me.lblZona.Size = New System.Drawing.Size(35, 13)
        Me.lblZona.TabIndex = 157
        Me.lblZona.Text = "Zona:"
        '
        'cboZona
        '
        Me.cboZona.BackColor = System.Drawing.SystemColors.Window
        Me.cboZona.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboZona.FormattingEnabled = True
        Me.cboZona.Items.AddRange(New Object() {"-- Seleccione --", "17", "18", "19"})
        Me.cboZona.Location = New System.Drawing.Point(252, 99)
        Me.cboZona.Name = "cboZona"
        Me.cboZona.Size = New System.Drawing.Size(87, 21)
        Me.cboZona.TabIndex = 156
        '
        'lstCoordenada
        '
        Me.lstCoordenada.BackColor = System.Drawing.SystemColors.Window
        Me.lstCoordenada.Font = New System.Drawing.Font("Tahoma", 6.45!, System.Drawing.FontStyle.Bold)
        Me.lstCoordenada.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lstCoordenada.FormattingEnabled = True
        Me.lstCoordenada.ItemHeight = 11
        Me.lstCoordenada.Location = New System.Drawing.Point(10, 61)
        Me.lstCoordenada.Name = "lstCoordenada"
        Me.lstCoordenada.Size = New System.Drawing.Size(196, 158)
        Me.lstCoordenada.TabIndex = 113
        '
        'btnLimpia
        '
        Me.btnLimpia.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.btnLimpia.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLimpia.Location = New System.Drawing.Point(31, 317)
        Me.btnLimpia.Name = "btnLimpia"
        Me.btnLimpia.Size = New System.Drawing.Size(85, 24)
        Me.btnLimpia.TabIndex = 179
        Me.btnLimpia.Text = "Limpiar"
        Me.btnLimpia.UseVisualStyleBackColor = True
        '
        'dgdListaDM
        '
        Me.dgdListaDM.AlternatingRows = True
        Me.dgdListaDM.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgdListaDM.Images.Add(CType(resources.GetObject("dgdListaDM.Images"), System.Drawing.Image))
        Me.dgdListaDM.Location = New System.Drawing.Point(74, 362)
        Me.dgdListaDM.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.dgdListaDM.Name = "dgdListaDM"
        Me.dgdListaDM.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgdListaDM.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgdListaDM.PreviewInfo.ZoomFactor = 75.0R
        Me.dgdListaDM.PrintInfo.PageSettings = CType(resources.GetObject("dgdListaDM.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgdListaDM.Size = New System.Drawing.Size(42, 21)
        Me.dgdListaDM.TabIndex = 174
        Me.dgdListaDM.Text = "C1TrueDBGrid1"
        Me.dgdListaDM.PropBag = resources.GetString("dgdListaDM.PropBag")
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(337, 70)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 175
        Me.PictureBox1.TabStop = False
        '
        'btnCerrar
        '
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Image)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCerrar.Location = New System.Drawing.Point(132, 316)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(104, 26)
        Me.btnCerrar.TabIndex = 176
        '
        'btnGraficar
        '
        Me.btnGraficar.Image = CType(resources.GetObject("btnGraficar.Image"), System.Drawing.Image)
        Me.btnGraficar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnGraficar.Location = New System.Drawing.Point(242, 316)
        Me.btnGraficar.Name = "btnGraficar"
        Me.btnGraficar.Size = New System.Drawing.Size(101, 26)
        Me.btnGraficar.TabIndex = 177
        '
        'frm_Grafica_Excel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(355, 349)
        Me.Controls.Add(Me.btnLimpia)
        Me.Controls.Add(Me.btnGraficar)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.dgdListaDM)
        Me.Controls.Add(Me.GroupBox3)
        Me.Name = "frm_Grafica_Excel"
        Me.Text = "Graficar desde Archivo Excel"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelArchivo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgdListaDM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Cbotipo As System.Windows.Forms.ComboBox
    Friend WithEvents lblZona As System.Windows.Forms.Label
    Friend WithEvents cboZona As System.Windows.Forms.ComboBox
    Friend WithEvents lstCoordenada As System.Windows.Forms.ListBox
    Friend WithEvents dgdListaDM As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents btnSelArchivo As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnGraficar As System.Windows.Forms.Button
    Friend WithEvents btnLimpia As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
End Class
