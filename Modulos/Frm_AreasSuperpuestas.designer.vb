<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_AreasSuperpuestas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_AreasSuperpuestas))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.imgMenu = New System.Windows.Forms.PictureBox()
        Me.lstCoordenada = New System.Windows.Forms.ListBox()
        Me.List_coordenadas = New System.Windows.Forms.ListBox()
        Me.btnagregar = New System.Windows.Forms.Button()
        Me.btnGrabar = New System.Windows.Forms.Button()
        Me.img_DM = New System.Windows.Forms.PictureBox()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.dgdResultado = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.dgdDetalle = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.GroupBox1.SuspendLayout()
        CType(Me.imgMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_DM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgdResultado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgdDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.imgMenu)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 22)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(514, 77)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'imgMenu
        '
        Me.imgMenu.Image = CType(resources.GetObject("imgMenu.Image"), System.Drawing.Image)
        Me.imgMenu.Location = New System.Drawing.Point(0, 1)
        Me.imgMenu.Name = "imgMenu"
        Me.imgMenu.Size = New System.Drawing.Size(514, 71)
        Me.imgMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgMenu.TabIndex = 168
        Me.imgMenu.TabStop = False
        '
        'lstCoordenada
        '
        Me.lstCoordenada.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCoordenada.FormattingEnabled = True
        Me.lstCoordenada.Location = New System.Drawing.Point(161, 303)
        Me.lstCoordenada.Name = "lstCoordenada"
        Me.lstCoordenada.Size = New System.Drawing.Size(252, 121)
        Me.lstCoordenada.TabIndex = 144
        '
        'List_coordenadas
        '
        Me.List_coordenadas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.List_coordenadas.FormattingEnabled = True
        Me.List_coordenadas.Location = New System.Drawing.Point(25, 366)
        Me.List_coordenadas.Name = "List_coordenadas"
        Me.List_coordenadas.Size = New System.Drawing.Size(126, 56)
        Me.List_coordenadas.TabIndex = 166
        Me.List_coordenadas.Visible = False
        '
        'btnagregar
        '
        Me.btnagregar.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.btnagregar.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnagregar.Image = Global.SIGCATMIN.My.Resources.Resources.ver_Area_Superpuesta
        Me.btnagregar.Location = New System.Drawing.Point(419, 347)
        Me.btnagregar.Name = "btnagregar"
        Me.btnagregar.Size = New System.Drawing.Size(99, 27)
        Me.btnagregar.TabIndex = 167
        Me.btnagregar.UseVisualStyleBackColor = True
        '
        'btnGrabar
        '
        Me.btnGrabar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGrabar.Image = CType(resources.GetObject("btnGrabar.Image"), System.Drawing.Image)
        Me.btnGrabar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnGrabar.Location = New System.Drawing.Point(419, 315)
        Me.btnGrabar.Name = "btnGrabar"
        Me.btnGrabar.Size = New System.Drawing.Size(99, 26)
        Me.btnGrabar.TabIndex = 165
        '
        'img_DM
        '
        Me.img_DM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.img_DM.Image = CType(resources.GetObject("img_DM.Image"), System.Drawing.Image)
        Me.img_DM.Location = New System.Drawing.Point(25, 303)
        Me.img_DM.Name = "img_DM"
        Me.img_DM.Size = New System.Drawing.Size(126, 121)
        Me.img_DM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.img_DM.TabIndex = 145
        Me.img_DM.TabStop = False
        '
        'btnCerrar
        '
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Image)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCerrar.Location = New System.Drawing.Point(419, 380)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(99, 26)
        Me.btnCerrar.TabIndex = 143
        '
        'dgdResultado
        '
        Me.dgdResultado.AllowArrows = False
        Me.dgdResultado.AllowColMove = False
        Me.dgdResultado.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.dgdResultado.AllowSort = False
        Me.dgdResultado.AllowUpdate = False
        Me.dgdResultado.AlternatingRows = True
        Me.dgdResultado.CaptionHeight = 17
        Me.dgdResultado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgdResultado.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.dgdResultado.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgdResultado.Images.Add(CType(resources.GetObject("dgdResultado.Images"), System.Drawing.Image))
        Me.dgdResultado.Location = New System.Drawing.Point(12, 293)
        Me.dgdResultado.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.dgdResultado.Name = "dgdResultado"
        Me.dgdResultado.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgdResultado.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgdResultado.PreviewInfo.ZoomFactor = 75.0R
        Me.dgdResultado.PrintInfo.PageSettings = CType(resources.GetObject("dgdResultado.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgdResultado.RowHeight = 15
        Me.dgdResultado.Size = New System.Drawing.Size(514, 139)
        Me.dgdResultado.TabIndex = 138
        Me.dgdResultado.Text = "C1TrueDBGrid1"
        Me.dgdResultado.PropBag = resources.GetString("dgdResultado.PropBag")
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
        Me.dgdDetalle.Location = New System.Drawing.Point(12, 105)
        Me.dgdDetalle.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.dgdDetalle.Name = "dgdDetalle"
        Me.dgdDetalle.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgdDetalle.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgdDetalle.PreviewInfo.ZoomFactor = 75.0R
        Me.dgdDetalle.PrintInfo.PageSettings = CType(resources.GetObject("dgdDetalle.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgdDetalle.RowHeight = 15
        Me.dgdDetalle.Size = New System.Drawing.Size(514, 182)
        Me.dgdDetalle.TabIndex = 137
        Me.dgdDetalle.Text = "C1TrueDBGrid1"
        Me.dgdDetalle.PropBag = resources.GetString("dgdDetalle.PropBag")
        '
        'Frm_AreasSuperpuestas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(530, 440)
        Me.Controls.Add(Me.btnagregar)
        Me.Controls.Add(Me.List_coordenadas)
        Me.Controls.Add(Me.btnGrabar)
        Me.Controls.Add(Me.img_DM)
        Me.Controls.Add(Me.lstCoordenada)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.dgdResultado)
        Me.Controls.Add(Me.dgdDetalle)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximumSize = New System.Drawing.Size(546, 478)
        Me.MinimumSize = New System.Drawing.Size(546, 478)
        Me.Name = "Frm_AreasSuperpuestas"
        Me.Text = "Areas Superpuestas"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.imgMenu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_DM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgdResultado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgdDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents imgMenu As System.Windows.Forms.PictureBox
    Friend WithEvents dgdDetalle As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents dgdResultado As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents lstCoordenada As System.Windows.Forms.ListBox
    Friend WithEvents img_DM As System.Windows.Forms.PictureBox
    Friend WithEvents btnGrabar As System.Windows.Forms.Button
    Friend WithEvents List_coordenadas As System.Windows.Forms.ListBox
    Friend WithEvents btnagregar As System.Windows.Forms.Button
End Class
