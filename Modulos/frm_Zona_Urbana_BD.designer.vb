<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Zona_Urbana_BD
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Zona_Urbana_BD))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnGrabar = New System.Windows.Forms.Button()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.dgdDetalle = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.dgdZonaUrbana = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.dgdDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgdZonaUrbana, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(23, 187)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(297, 13)
        Me.Label2.TabIndex = 162
        Me.Label2.Text = "Zona Urbana:  Cuadriculas Libres / Parcial  / Total"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 86)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(145, 13)
        Me.Label1.TabIndex = 161
        Me.Label1.Text = "Cálculo de Zona Urbana"
        '
        'btnGrabar
        '
        Me.btnGrabar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGrabar.Image = CType(resources.GetObject("btnGrabar.Image"), System.Drawing.Image)
        Me.btnGrabar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnGrabar.Location = New System.Drawing.Point(385, 307)
        Me.btnGrabar.Name = "btnGrabar"
        Me.btnGrabar.Size = New System.Drawing.Size(99, 26)
        Me.btnGrabar.TabIndex = 164
        '
        'btnCerrar
        '
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Image)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCerrar.Location = New System.Drawing.Point(246, 307)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(104, 26)
        Me.btnCerrar.TabIndex = 163
        '
        'dgdDetalle
        '
        Me.dgdDetalle.AllowArrows = False
        Me.dgdDetalle.AllowColMove = False
        Me.dgdDetalle.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.dgdDetalle.AllowSort = False
        Me.dgdDetalle.AllowUpdate = False
        Me.dgdDetalle.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgdDetalle.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.dgdDetalle.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgdDetalle.Images.Add(CType(resources.GetObject("dgdDetalle.Images"), System.Drawing.Image))
        Me.dgdDetalle.Location = New System.Drawing.Point(12, 102)
        Me.dgdDetalle.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.dgdDetalle.Name = "dgdDetalle"
        Me.dgdDetalle.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgdDetalle.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgdDetalle.PreviewInfo.ZoomFactor = 75.0R
        Me.dgdDetalle.PrintInfo.PageSettings = CType(resources.GetObject("dgdDetalle.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgdDetalle.Size = New System.Drawing.Size(472, 82)
        Me.dgdDetalle.TabIndex = 160
        Me.dgdDetalle.Text = "C1TrueDBGrid1"
        Me.dgdDetalle.PropBag = resources.GetString("dgdDetalle.PropBag")
        '
        'dgdZonaUrbana
        '
        Me.dgdZonaUrbana.AllowArrows = False
        Me.dgdZonaUrbana.AllowColMove = False
        Me.dgdZonaUrbana.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.dgdZonaUrbana.AllowSort = False
        Me.dgdZonaUrbana.AllowUpdate = False
        Me.dgdZonaUrbana.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgdZonaUrbana.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.dgdZonaUrbana.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgdZonaUrbana.Images.Add(CType(resources.GetObject("dgdZonaUrbana.Images"), System.Drawing.Image))
        Me.dgdZonaUrbana.Location = New System.Drawing.Point(12, 203)
        Me.dgdZonaUrbana.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.dgdZonaUrbana.Name = "dgdZonaUrbana"
        Me.dgdZonaUrbana.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgdZonaUrbana.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgdZonaUrbana.PreviewInfo.ZoomFactor = 75.0R
        Me.dgdZonaUrbana.PrintInfo.PageSettings = CType(resources.GetObject("dgdZonaUrbana.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgdZonaUrbana.Size = New System.Drawing.Size(472, 98)
        Me.dgdZonaUrbana.TabIndex = 159
        Me.dgdZonaUrbana.Text = "C1TrueDBGrid1"
        Me.dgdZonaUrbana.PropBag = resources.GetString("dgdZonaUrbana.PropBag")
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(468, 71)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 165
        Me.PictureBox1.TabStop = False
        '
        'frm_Zona_Urbana_BD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(484, 332)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnGrabar)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgdDetalle)
        Me.Controls.Add(Me.dgdZonaUrbana)
        Me.MaximumSize = New System.Drawing.Size(500, 370)
        Me.MinimumSize = New System.Drawing.Size(500, 370)
        Me.Name = "frm_Zona_Urbana_BD"
        Me.Text = "ZONAS URBANAS"
        CType(Me.dgdDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgdZonaUrbana, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnGrabar As System.Windows.Forms.Button
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgdDetalle As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents dgdZonaUrbana As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
