<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Estadisticas_Caram
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Estadisticas_Caram))
        Me.chkEstado = New System.Windows.Forms.CheckBox()
        Me.lbl_nombre1 = New System.Windows.Forms.Label()
        Me.cbotipo = New System.Windows.Forms.ComboBox()
        Me.cbodetalle = New System.Windows.Forms.ComboBox()
        Me.lblregistro = New System.Windows.Forms.Label()
        Me.cboZona = New System.Windows.Forms.ComboBox()
        Me.lblZona = New System.Windows.Forms.Label()
        Me.btncalcular = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cbotiporese = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnCargar = New System.Windows.Forms.Button()
        Me.btnExcel = New System.Windows.Forms.Button()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.txtPorcentaje = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtArea2 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgdDetalle = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.imgMenu = New System.Windows.Forms.PictureBox()
        Me.txtArea1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgdDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkEstado
        '
        Me.chkEstado.AutoSize = True
        Me.chkEstado.Location = New System.Drawing.Point(45, 94)
        Me.chkEstado.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkEstado.Name = "chkEstado"
        Me.chkEstado.Size = New System.Drawing.Size(128, 26)
        Me.chkEstado.TabIndex = 246
        Me.chkEstado.Text = "chkEstado"
        Me.chkEstado.UseVisualStyleBackColor = True
        Me.chkEstado.Visible = False
        '
        'lbl_nombre1
        '
        Me.lbl_nombre1.AutoSize = True
        Me.lbl_nombre1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_nombre1.Location = New System.Drawing.Point(9, 33)
        Me.lbl_nombre1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_nombre1.Name = "lbl_nombre1"
        Me.lbl_nombre1.Size = New System.Drawing.Size(130, 17)
        Me.lbl_nombre1.TabIndex = 248
        Me.lbl_nombre1.Text = "Seleccione el tipo:"
        '
        'cbotipo
        '
        Me.cbotipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbotipo.FormattingEnabled = True
        Me.cbotipo.Items.AddRange(New Object() {"-- Selec. --", "SEGUN DEPARTAMENTO", "A NIVEL NACIONAL"})
        Me.cbotipo.Location = New System.Drawing.Point(165, 31)
        Me.cbotipo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cbotipo.Name = "cbotipo"
        Me.cbotipo.Size = New System.Drawing.Size(193, 24)
        Me.cbotipo.TabIndex = 247
        '
        'cbodetalle
        '
        Me.cbodetalle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbodetalle.FormattingEnabled = True
        Me.cbodetalle.Items.AddRange(New Object() {"-- Selec. --"})
        Me.cbodetalle.Location = New System.Drawing.Point(539, 30)
        Me.cbodetalle.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cbodetalle.Name = "cbodetalle"
        Me.cbodetalle.Size = New System.Drawing.Size(193, 24)
        Me.cbodetalle.TabIndex = 249
        '
        'lblregistro
        '
        Me.lblregistro.AutoSize = True
        Me.lblregistro.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblregistro.ForeColor = System.Drawing.Color.Red
        Me.lblregistro.Location = New System.Drawing.Point(45, 182)
        Me.lblregistro.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblregistro.Name = "lblregistro"
        Me.lblregistro.Size = New System.Drawing.Size(0, 17)
        Me.lblregistro.TabIndex = 250
        Me.lblregistro.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cboZona
        '
        Me.cboZona.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboZona.FormattingEnabled = True
        Me.cboZona.Location = New System.Drawing.Point(607, 94)
        Me.cboZona.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cboZona.Name = "cboZona"
        Me.cboZona.Size = New System.Drawing.Size(125, 24)
        Me.cboZona.TabIndex = 251
        '
        'lblZona
        '
        Me.lblZona.AutoSize = True
        Me.lblZona.Location = New System.Drawing.Point(445, 94)
        Me.lblZona.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblZona.Name = "lblZona"
        Me.lblZona.Size = New System.Drawing.Size(110, 17)
        Me.lblZona.TabIndex = 252
        Me.lblZona.Text = "Seleccion Zona:"
        '
        'btncalcular
        '
        Me.btncalcular.BackColor = System.Drawing.SystemColors.Control
        Me.btncalcular.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncalcular.ForeColor = System.Drawing.Color.Blue
        Me.btncalcular.Location = New System.Drawing.Point(585, 22)
        Me.btncalcular.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btncalcular.Name = "btncalcular"
        Me.btncalcular.Size = New System.Drawing.Size(127, 28)
        Me.btncalcular.TabIndex = 253
        Me.btncalcular.Text = "CALCULAR"
        Me.btncalcular.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cbotiporese)
        Me.GroupBox1.Controls.Add(Me.lbl_nombre1)
        Me.GroupBox1.Controls.Add(Me.cbotipo)
        Me.GroupBox1.Controls.Add(Me.cbodetalle)
        Me.GroupBox1.Controls.Add(Me.cboZona)
        Me.GroupBox1.Controls.Add(Me.lblZona)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 112)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(766, 123)
        Me.GroupBox1.TabIndex = 254
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Busqueda Por Pais/Dpto/Tipo Reserva"
        '
        'cbotiporese
        '
        Me.cbotiporese.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbotiporese.FormattingEnabled = True
        Me.cbotiporese.Items.AddRange(New Object() {"-- Selec. --"})
        Me.cbotiporese.Location = New System.Drawing.Point(165, 83)
        Me.cbotiporese.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cbotiporese.Name = "cbotiporese"
        Me.cbotiporese.Size = New System.Drawing.Size(193, 24)
        Me.cbotiporese.TabIndex = 250
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnCargar)
        Me.GroupBox2.Controls.Add(Me.btnExcel)
        Me.GroupBox2.Controls.Add(Me.btncalcular)
        Me.GroupBox2.Controls.Add(Me.chkEstado)
        Me.GroupBox2.Controls.Add(Me.btnCerrar)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 577)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(727, 62)
        Me.GroupBox2.TabIndex = 255
        Me.GroupBox2.TabStop = False
        '
        'btnCargar
        '
        Me.btnCargar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCargar.ForeColor = System.Drawing.Color.Blue
        Me.btnCargar.Location = New System.Drawing.Point(36, 20)
        Me.btnCargar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnCargar.Name = "btnCargar"
        Me.btnCargar.Size = New System.Drawing.Size(175, 32)
        Me.btnCargar.TabIndex = 255
        Me.btnCargar.Text = "Cargar Información"
        Me.btnCargar.UseVisualStyleBackColor = True
        '
        'btnExcel
        '
        Me.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExcel.Image = CType(resources.GetObject("btnExcel.Image"), System.Drawing.Image)
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExcel.Location = New System.Drawing.Point(397, 18)
        Me.btnExcel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(139, 32)
        Me.btnExcel.TabIndex = 254
        Me.btnExcel.UseVisualStyleBackColor = True
        '
        'btnCerrar
        '
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Image)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCerrar.Location = New System.Drawing.Point(235, 20)
        Me.btnCerrar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(132, 32)
        Me.btnCerrar.TabIndex = 177
        '
        'txtPorcentaje
        '
        Me.txtPorcentaje.Location = New System.Drawing.Point(665, 662)
        Me.txtPorcentaje.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtPorcentaje.Name = "txtPorcentaje"
        Me.txtPorcentaje.Size = New System.Drawing.Size(91, 22)
        Me.txtPorcentaje.TabIndex = 261
        Me.txtPorcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(571, 665)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 17)
        Me.Label5.TabIndex = 260
        Me.Label5.Text = "% ARES:"
        '
        'txtArea2
        '
        Me.txtArea2.Location = New System.Drawing.Point(433, 661)
        Me.txtArea2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtArea2.Name = "txtArea2"
        Me.txtArea2.Size = New System.Drawing.Size(115, 22)
        Me.txtArea2.TabIndex = 259
        Me.txtArea2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 661)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 17)
        Me.Label2.TabIndex = 256
        Me.Label2.Text = "Area Total (Ha):"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(273, 667)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(151, 17)
        Me.Label3.TabIndex = 257
        Me.Label3.Text = "Area Total ARES (Ha):"
        '
        'dgdDetalle
        '
        Me.dgdDetalle.AllowArrows = False
        Me.dgdDetalle.AllowColMove = False
        Me.dgdDetalle.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.dgdDetalle.AllowSort = False
        Me.dgdDetalle.AllowUpdate = False
        Me.dgdDetalle.AlternatingRows = True
        Me.dgdDetalle.CaptionHeight = 17
        Me.dgdDetalle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgdDetalle.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.dgdDetalle.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgdDetalle.Images.Add(CType(resources.GetObject("dgdDetalle.Images"), System.Drawing.Image))
        Me.dgdDetalle.Location = New System.Drawing.Point(7, 242)
        Me.dgdDetalle.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dgdDetalle.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.dgdDetalle.Name = "dgdDetalle"
        Me.dgdDetalle.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgdDetalle.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgdDetalle.PreviewInfo.ZoomFactor = 75.0R
        Me.dgdDetalle.PrintInfo.PageSettings = CType(resources.GetObject("dgdDetalle.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgdDetalle.RowHeight = 15
        Me.dgdDetalle.Size = New System.Drawing.Size(766, 407)
        Me.dgdDetalle.TabIndex = 174
        Me.dgdDetalle.Text = "C1TrueDBGrid1"
        Me.dgdDetalle.PropBag = resources.GetString("dgdDetalle.PropBag")
        '
        'imgMenu
        '
        Me.imgMenu.Image = CType(resources.GetObject("imgMenu.Image"), System.Drawing.Image)
        Me.imgMenu.Location = New System.Drawing.Point(7, 15)
        Me.imgMenu.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.imgMenu.Name = "imgMenu"
        Me.imgMenu.Size = New System.Drawing.Size(766, 87)
        Me.imgMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgMenu.TabIndex = 173
        Me.imgMenu.TabStop = False
        '
        'txtArea1
        '
        Me.txtArea1.Location = New System.Drawing.Point(137, 657)
        Me.txtArea1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtArea1.Name = "txtArea1"
        Me.txtArea1.Size = New System.Drawing.Size(127, 22)
        Me.txtArea1.TabIndex = 258
        Me.txtArea1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(401, 34)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 17)
        Me.Label1.TabIndex = 262
        Me.Label1.Text = "Seleccione Region :"
        '
        'Frm_Estadisticas_Caram
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(786, 698)
        Me.Controls.Add(Me.txtPorcentaje)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtArea2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtArea1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.lblregistro)
        Me.Controls.Add(Me.dgdDetalle)
        Me.Controls.Add(Me.imgMenu)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "Frm_Estadisticas_Caram"
        Me.Text = "ESTADISTICAS DE AREAS RESTRINGIDAS A LA ACTIVIDAD MINERA"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgdDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents imgMenu As System.Windows.Forms.PictureBox
    Friend WithEvents dgdDetalle As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents chkEstado As System.Windows.Forms.CheckBox
    Friend WithEvents lbl_nombre1 As System.Windows.Forms.Label
    Friend WithEvents cbotipo As System.Windows.Forms.ComboBox
    Friend WithEvents cbodetalle As System.Windows.Forms.ComboBox
    Friend WithEvents lblregistro As System.Windows.Forms.Label
    Friend WithEvents cboZona As System.Windows.Forms.ComboBox
    Friend WithEvents lblZona As System.Windows.Forms.Label
    Friend WithEvents btncalcular As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPorcentaje As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtArea2 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnExcel As System.Windows.Forms.Button
    Friend WithEvents btnCargar As System.Windows.Forms.Button
    Friend WithEvents cbotiporese As System.Windows.Forms.ComboBox
    Friend WithEvents txtArea1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
