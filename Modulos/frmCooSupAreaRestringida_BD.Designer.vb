<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCooSupAreaRestringida_BD
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCooSupAreaRestringida_BD))
        Me.btnGrabar = New System.Windows.Forms.Button
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.dgdlista = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        CType(Me.dgdlista, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnGrabar
        '
        Me.btnGrabar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGrabar.Image = CType(resources.GetObject("btnGrabar.Image"), System.Drawing.Image)
        Me.btnGrabar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnGrabar.Location = New System.Drawing.Point(380, 235)
        Me.btnGrabar.Name = "btnGrabar"
        Me.btnGrabar.Size = New System.Drawing.Size(99, 26)
        Me.btnGrabar.TabIndex = 166
        '
        'btnCerrar
        '
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Image)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCerrar.Location = New System.Drawing.Point(270, 236)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(104, 26)
        Me.btnCerrar.TabIndex = 165
        '
        'dgdlista
        '
        Me.dgdlista.AllowArrows = False
        Me.dgdlista.AllowColMove = False
        Me.dgdlista.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.dgdlista.AllowSort = False
        Me.dgdlista.AllowUpdate = False
        Me.dgdlista.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgdlista.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.dgdlista.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgdlista.Images.Add(CType(resources.GetObject("dgdlista.Images"), System.Drawing.Image))
        Me.dgdlista.Location = New System.Drawing.Point(6, 19)
        Me.dgdlista.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.dgdlista.Name = "dgdlista"
        Me.dgdlista.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgdlista.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgdlista.PreviewInfo.ZoomFactor = 75
        Me.dgdlista.PrintInfo.PageSettings = CType(resources.GetObject("dgdlista.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgdlista.Size = New System.Drawing.Size(453, 192)
        Me.dgdlista.TabIndex = 161
        Me.dgdlista.Text = "C1TrueDBGrid1"
        Me.dgdlista.PropBag = resources.GetString("dgdlista.PropBag")
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgdlista)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(465, 217)
        Me.GroupBox1.TabIndex = 167
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Vértices:"
        '
        'frmCooSupAreaRestringida_BD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(486, 270)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnGrabar)
        Me.Controls.Add(Me.btnCerrar)
        Me.Name = "frmCooSupAreaRestringida_BD"
        Me.Text = "Coordenadas Superpuesto a Areas Restringidas"
        CType(Me.dgdlista, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnGrabar As System.Windows.Forms.Button
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents dgdlista As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
