<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_datos_Evaluacion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_datos_Evaluacion))
        Me.dgdDetalle = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.chkEstado = New System.Windows.Forms.CheckBox
        Me.btnGrabar = New System.Windows.Forms.Button
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.btnActualizar = New System.Windows.Forms.Button
        Me.imgMenu = New System.Windows.Forms.PictureBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblRHistorico = New System.Windows.Forms.Label
        Me.lblRInforme = New System.Windows.Forms.Label
        Me.cboInforme = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.lbl_codigo = New System.Windows.Forms.Label
        Me.txtCodigo = New System.Windows.Forms.TextBox
        Me.txtNombre = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnAceptar = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.gbxDatosevaluacion = New System.Windows.Forms.GroupBox
        Me.Tabdatos = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.GroupBox12 = New System.Windows.Forms.GroupBox
        Me.Check_dato5 = New System.Windows.Forms.CheckBox
        Me.Check_dato6 = New System.Windows.Forms.CheckBox
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.Check_dato8 = New System.Windows.Forms.CheckBox
        Me.Check_dato7 = New System.Windows.Forms.CheckBox
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.Txtlaguna = New System.Windows.Forms.TextBox
        Me.Txtrio = New System.Windows.Forms.TextBox
        Me.txt_dato20 = New System.Windows.Forms.TextBox
        Me.Check_dato20 = New System.Windows.Forms.CheckBox
        Me.Check_dato19 = New System.Windows.Forms.CheckBox
        Me.Check_dato18 = New System.Windows.Forms.CheckBox
        Me.Check_dato17 = New System.Windows.Forms.CheckBox
        Me.Check_dato16 = New System.Windows.Forms.CheckBox
        Me.Check_dato15 = New System.Windows.Forms.CheckBox
        Me.Check_dato14 = New System.Windows.Forms.CheckBox
        Me.Check_dato13 = New System.Windows.Forms.CheckBox
        Me.Check_dato11 = New System.Windows.Forms.CheckBox
        Me.Check_dato12 = New System.Windows.Forms.CheckBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Check_dato1 = New System.Windows.Forms.CheckBox
        Me.Check_dato2 = New System.Windows.Forms.CheckBox
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.Check_dato9 = New System.Windows.Forms.CheckBox
        Me.Check_dato10 = New System.Windows.Forms.CheckBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.Check_dato3 = New System.Windows.Forms.CheckBox
        Me.Check_dato4 = New System.Windows.Forms.CheckBox
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Txtpaises = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txt_reserva = New System.Windows.Forms.TextBox
        Me.txt_urbana = New System.Windows.Forms.TextBox
        Me.txtfrontera = New System.Windows.Forms.TextBox
        Me.txtvEste = New System.Windows.Forms.TextBox
        Me.txtvNorte = New System.Windows.Forms.TextBox
        Me.txtNumVer = New System.Windows.Forms.TextBox
        Me.txtArea = New System.Windows.Forms.TextBox
        CType(Me.dgdDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.gbxDatosevaluacion.SuspendLayout()
        Me.Tabdatos.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox12.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
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
        Me.dgdDetalle.Font = New System.Drawing.Font("Tahoma", 7.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgdDetalle.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.dgdDetalle.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgdDetalle.Images.Add(CType(resources.GetObject("dgdDetalle.Images"), System.Drawing.Image))
        Me.dgdDetalle.Location = New System.Drawing.Point(3, 6)
        Me.dgdDetalle.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.dgdDetalle.Name = "dgdDetalle"
        Me.dgdDetalle.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgdDetalle.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgdDetalle.PreviewInfo.ZoomFactor = 75
        Me.dgdDetalle.PrintInfo.PageSettings = CType(resources.GetObject("dgdDetalle.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgdDetalle.RowHeight = 15
        Me.dgdDetalle.Size = New System.Drawing.Size(557, 208)
        Me.dgdDetalle.TabIndex = 137
        Me.dgdDetalle.Text = "C1TrueDBGrid1"
        Me.dgdDetalle.PropBag = resources.GetString("dgdDetalle.PropBag")
        '
        'chkEstado
        '
        Me.chkEstado.AutoSize = True
        Me.chkEstado.Location = New System.Drawing.Point(132, 129)
        Me.chkEstado.Name = "chkEstado"
        Me.chkEstado.Size = New System.Drawing.Size(77, 17)
        Me.chkEstado.TabIndex = 163
        Me.chkEstado.Text = "chkEstado"
        Me.chkEstado.UseVisualStyleBackColor = True
        Me.chkEstado.Visible = False
        '
        'btnGrabar
        '
        Me.btnGrabar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGrabar.Image = CType(resources.GetObject("btnGrabar.Image"), System.Drawing.Image)
        Me.btnGrabar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnGrabar.Location = New System.Drawing.Point(18, 11)
        Me.btnGrabar.Name = "btnGrabar"
        Me.btnGrabar.Size = New System.Drawing.Size(99, 26)
        Me.btnGrabar.TabIndex = 164
        Me.btnGrabar.Visible = False
        '
        'btnCerrar
        '
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Image)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCerrar.Location = New System.Drawing.Point(259, 12)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(104, 26)
        Me.btnCerrar.TabIndex = 165
        '
        'btnActualizar
        '
        Me.btnActualizar.Image = CType(resources.GetObject("btnActualizar.Image"), System.Drawing.Image)
        Me.btnActualizar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnActualizar.Location = New System.Drawing.Point(368, 12)
        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(104, 26)
        Me.btnActualizar.TabIndex = 166
        '
        'imgMenu
        '
        Me.imgMenu.Image = CType(resources.GetObject("imgMenu.Image"), System.Drawing.Image)
        Me.imgMenu.Location = New System.Drawing.Point(5, 12)
        Me.imgMenu.Name = "imgMenu"
        Me.imgMenu.Size = New System.Drawing.Size(576, 71)
        Me.imgMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgMenu.TabIndex = 167
        Me.imgMenu.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblRHistorico)
        Me.GroupBox1.Controls.Add(Me.lblRInforme)
        Me.GroupBox1.Controls.Add(Me.cboInforme)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lbl_codigo)
        Me.GroupBox1.Controls.Add(Me.txtCodigo)
        Me.GroupBox1.Controls.Add(Me.txtNombre)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 94)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(587, 64)
        Me.GroupBox1.TabIndex = 168
        Me.GroupBox1.TabStop = False
        '
        'lblRHistorico
        '
        Me.lblRHistorico.AutoSize = True
        Me.lblRHistorico.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRHistorico.ForeColor = System.Drawing.Color.Red
        Me.lblRHistorico.Location = New System.Drawing.Point(398, 19)
        Me.lblRHistorico.MaximumSize = New System.Drawing.Size(403, 13)
        Me.lblRHistorico.MinimumSize = New System.Drawing.Size(403, 13)
        Me.lblRHistorico.Name = "lblRHistorico"
        Me.lblRHistorico.Size = New System.Drawing.Size(403, 13)
        Me.lblRHistorico.TabIndex = 180
        Me.lblRHistorico.Text = "xxx"
        '
        'lblRInforme
        '
        Me.lblRInforme.AutoSize = True
        Me.lblRInforme.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRInforme.ForeColor = System.Drawing.Color.Red
        Me.lblRInforme.Location = New System.Drawing.Point(398, 4)
        Me.lblRInforme.MaximumSize = New System.Drawing.Size(403, 13)
        Me.lblRInforme.MinimumSize = New System.Drawing.Size(403, 13)
        Me.lblRInforme.Name = "lblRInforme"
        Me.lblRInforme.Size = New System.Drawing.Size(403, 13)
        Me.lblRInforme.TabIndex = 179
        Me.lblRInforme.Text = "xxx"
        '
        'cboInforme
        '
        Me.cboInforme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInforme.FormattingEnabled = True
        Me.cboInforme.Location = New System.Drawing.Point(400, 34)
        Me.cboInforme.Name = "cboInforme"
        Me.cboInforme.Size = New System.Drawing.Size(177, 21)
        Me.cboInforme.TabIndex = 178
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(345, 38)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 177
        Me.Label6.Text = "Informe :"
        '
        'lbl_codigo
        '
        Me.lbl_codigo.AutoSize = True
        Me.lbl_codigo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_codigo.Location = New System.Drawing.Point(15, 16)
        Me.lbl_codigo.Name = "lbl_codigo"
        Me.lbl_codigo.Size = New System.Drawing.Size(57, 13)
        Me.lbl_codigo.TabIndex = 172
        Me.lbl_codigo.Text = "Coidigo :"
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(74, 9)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(126, 20)
        Me.txtCodigo.TabIndex = 174
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(74, 35)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(265, 20)
        Me.txtNombre.TabIndex = 175
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 173
        Me.Label1.Text = "Nombre :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnAceptar)
        Me.GroupBox2.Controls.Add(Me.btnGrabar)
        Me.GroupBox2.Controls.Add(Me.btnActualizar)
        Me.GroupBox2.Controls.Add(Me.chkEstado)
        Me.GroupBox2.Controls.Add(Me.btnCerrar)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 441)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(587, 46)
        Me.GroupBox2.TabIndex = 169
        Me.GroupBox2.TabStop = False
        '
        'btnAceptar
        '
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Image)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAceptar.Location = New System.Drawing.Point(477, 10)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(104, 28)
        Me.btnAceptar.TabIndex = 167
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.imgMenu)
        Me.GroupBox3.Location = New System.Drawing.Point(5, 5)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(587, 89)
        Me.GroupBox3.TabIndex = 170
        Me.GroupBox3.TabStop = False
        '
        'gbxDatosevaluacion
        '
        Me.gbxDatosevaluacion.Controls.Add(Me.Tabdatos)
        Me.gbxDatosevaluacion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbxDatosevaluacion.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.gbxDatosevaluacion.Location = New System.Drawing.Point(5, 164)
        Me.gbxDatosevaluacion.Name = "gbxDatosevaluacion"
        Me.gbxDatosevaluacion.Size = New System.Drawing.Size(587, 271)
        Me.gbxDatosevaluacion.TabIndex = 176
        Me.gbxDatosevaluacion.TabStop = False
        Me.gbxDatosevaluacion.Text = "Datos de Evaluación"
        '
        'Tabdatos
        '
        Me.Tabdatos.Controls.Add(Me.TabPage1)
        Me.Tabdatos.Controls.Add(Me.TabPage2)
        Me.Tabdatos.Controls.Add(Me.TabPage3)
        Me.Tabdatos.Location = New System.Drawing.Point(6, 20)
        Me.Tabdatos.Name = "Tabdatos"
        Me.Tabdatos.SelectedIndex = 0
        Me.Tabdatos.Size = New System.Drawing.Size(575, 246)
        Me.Tabdatos.TabIndex = 39
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgdDetalle)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(567, 220)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Evaluación DM"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox12)
        Me.TabPage2.Controls.Add(Me.GroupBox10)
        Me.TabPage2.Controls.Add(Me.GroupBox9)
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Controls.Add(Me.GroupBox7)
        Me.TabPage2.Controls.Add(Me.GroupBox5)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(567, 220)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Carta IGN"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox12
        '
        Me.GroupBox12.Controls.Add(Me.Check_dato5)
        Me.GroupBox12.Controls.Add(Me.Check_dato6)
        Me.GroupBox12.Location = New System.Drawing.Point(172, 146)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(160, 68)
        Me.GroupBox12.TabIndex = 11
        Me.GroupBox12.TabStop = False
        Me.GroupBox12.Text = "Carretera"
        '
        'Check_dato5
        '
        Me.Check_dato5.AutoSize = True
        Me.Check_dato5.Location = New System.Drawing.Point(8, 19)
        Me.Check_dato5.Name = "Check_dato5"
        Me.Check_dato5.Size = New System.Drawing.Size(122, 17)
        Me.Check_dato5.TabIndex = 0
        Me.Check_dato5.Text = "Carretera Asfaltada"
        Me.Check_dato5.UseVisualStyleBackColor = True
        '
        'Check_dato6
        '
        Me.Check_dato6.AutoSize = True
        Me.Check_dato6.Location = New System.Drawing.Point(8, 43)
        Me.Check_dato6.Name = "Check_dato6"
        Me.Check_dato6.Size = New System.Drawing.Size(119, 17)
        Me.Check_dato6.TabIndex = 1
        Me.Check_dato6.Text = "Carretera Afirmada"
        Me.Check_dato6.UseVisualStyleBackColor = True
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.Check_dato8)
        Me.GroupBox10.Controls.Add(Me.Check_dato7)
        Me.GroupBox10.Location = New System.Drawing.Point(172, 6)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(160, 70)
        Me.GroupBox10.TabIndex = 10
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Zona de Bosques"
        '
        'Check_dato8
        '
        Me.Check_dato8.AutoSize = True
        Me.Check_dato8.Location = New System.Drawing.Point(8, 43)
        Me.Check_dato8.Name = "Check_dato8"
        Me.Check_dato8.Size = New System.Drawing.Size(137, 17)
        Me.Check_dato8.TabIndex = 1
        Me.Check_dato8.Text = "Zona de Bosque Parcial"
        Me.Check_dato8.UseVisualStyleBackColor = True
        '
        'Check_dato7
        '
        Me.Check_dato7.AutoSize = True
        Me.Check_dato7.Location = New System.Drawing.Point(8, 19)
        Me.Check_dato7.Name = "Check_dato7"
        Me.Check_dato7.Size = New System.Drawing.Size(130, 17)
        Me.Check_dato7.TabIndex = 0
        Me.Check_dato7.Text = "Zona de Bosque Total"
        Me.Check_dato7.UseVisualStyleBackColor = True
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.Txtlaguna)
        Me.GroupBox9.Controls.Add(Me.Txtrio)
        Me.GroupBox9.Controls.Add(Me.txt_dato20)
        Me.GroupBox9.Controls.Add(Me.Check_dato20)
        Me.GroupBox9.Controls.Add(Me.Check_dato19)
        Me.GroupBox9.Controls.Add(Me.Check_dato18)
        Me.GroupBox9.Controls.Add(Me.Check_dato17)
        Me.GroupBox9.Controls.Add(Me.Check_dato16)
        Me.GroupBox9.Controls.Add(Me.Check_dato15)
        Me.GroupBox9.Controls.Add(Me.Check_dato14)
        Me.GroupBox9.Controls.Add(Me.Check_dato13)
        Me.GroupBox9.Controls.Add(Me.Check_dato11)
        Me.GroupBox9.Controls.Add(Me.Check_dato12)
        Me.GroupBox9.Location = New System.Drawing.Point(338, 6)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(222, 215)
        Me.GroupBox9.TabIndex = 9
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Otros"
        '
        'Txtlaguna
        '
        Me.Txtlaguna.Location = New System.Drawing.Point(80, 34)
        Me.Txtlaguna.Multiline = True
        Me.Txtlaguna.Name = "Txtlaguna"
        Me.Txtlaguna.Size = New System.Drawing.Size(136, 25)
        Me.Txtlaguna.TabIndex = 12
        Me.Txtlaguna.Visible = False
        '
        'Txtrio
        '
        Me.Txtrio.Location = New System.Drawing.Point(80, 11)
        Me.Txtrio.Multiline = True
        Me.Txtrio.Name = "Txtrio"
        Me.Txtrio.Size = New System.Drawing.Size(136, 25)
        Me.Txtrio.TabIndex = 11
        Me.Txtrio.Visible = False
        '
        'txt_dato20
        '
        Me.txt_dato20.Location = New System.Drawing.Point(5, 180)
        Me.txt_dato20.Multiline = True
        Me.txt_dato20.Name = "txt_dato20"
        Me.txt_dato20.Size = New System.Drawing.Size(211, 25)
        Me.txt_dato20.TabIndex = 10
        '
        'Check_dato20
        '
        Me.Check_dato20.AutoSize = True
        Me.Check_dato20.Location = New System.Drawing.Point(6, 159)
        Me.Check_dato20.Name = "Check_dato20"
        Me.Check_dato20.Size = New System.Drawing.Size(187, 17)
        Me.Check_dato20.TabIndex = 9
        Me.Check_dato20.Text = "Posible Area Urbana/Exp. Urbana"
        Me.Check_dato20.UseVisualStyleBackColor = True
        '
        'Check_dato19
        '
        Me.Check_dato19.AutoSize = True
        Me.Check_dato19.Location = New System.Drawing.Point(99, 65)
        Me.Check_dato19.Name = "Check_dato19"
        Me.Check_dato19.Size = New System.Drawing.Size(111, 17)
        Me.Check_dato19.TabIndex = 8
        Me.Check_dato19.Text = "Línea de Frontera"
        Me.Check_dato19.UseVisualStyleBackColor = True
        '
        'Check_dato18
        '
        Me.Check_dato18.AutoSize = True
        Me.Check_dato18.Location = New System.Drawing.Point(6, 136)
        Me.Check_dato18.Name = "Check_dato18"
        Me.Check_dato18.Size = New System.Drawing.Size(109, 17)
        Me.Check_dato18.TabIndex = 7
        Me.Check_dato18.Text = "Zona de Traslape"
        Me.Check_dato18.UseVisualStyleBackColor = True
        '
        'Check_dato17
        '
        Me.Check_dato17.AutoSize = True
        Me.Check_dato17.Location = New System.Drawing.Point(6, 113)
        Me.Check_dato17.Name = "Check_dato17"
        Me.Check_dato17.Size = New System.Drawing.Size(129, 17)
        Me.Check_dato17.TabIndex = 6
        Me.Check_dato17.Text = "Restos Arqueologicos"
        Me.Check_dato17.UseVisualStyleBackColor = True
        '
        'Check_dato16
        '
        Me.Check_dato16.AutoSize = True
        Me.Check_dato16.Location = New System.Drawing.Point(6, 90)
        Me.Check_dato16.Name = "Check_dato16"
        Me.Check_dato16.Size = New System.Drawing.Size(171, 17)
        Me.Check_dato16.TabIndex = 5
        Me.Check_dato16.Text = "Línea de Alta Tensión Eléctrica"
        Me.Check_dato16.UseVisualStyleBackColor = True
        '
        'Check_dato15
        '
        Me.Check_dato15.AutoSize = True
        Me.Check_dato15.Location = New System.Drawing.Point(6, 65)
        Me.Check_dato15.Name = "Check_dato15"
        Me.Check_dato15.Size = New System.Drawing.Size(86, 17)
        Me.Check_dato15.TabIndex = 4
        Me.Check_dato15.Text = "Línea Ferrea"
        Me.Check_dato15.UseVisualStyleBackColor = True
        '
        'Check_dato14
        '
        Me.Check_dato14.AutoSize = True
        Me.Check_dato14.Location = New System.Drawing.Point(138, 136)
        Me.Check_dato14.Name = "Check_dato14"
        Me.Check_dato14.Size = New System.Drawing.Size(78, 17)
        Me.Check_dato14.TabIndex = 3
        Me.Check_dato14.Text = "Reservorio"
        Me.Check_dato14.UseVisualStyleBackColor = True
        '
        'Check_dato13
        '
        Me.Check_dato13.AutoSize = True
        Me.Check_dato13.Location = New System.Drawing.Point(6, 42)
        Me.Check_dato13.Name = "Check_dato13"
        Me.Check_dato13.Size = New System.Drawing.Size(74, 17)
        Me.Check_dato13.TabIndex = 2
        Me.Check_dato13.Text = "Laguna(s)"
        Me.Check_dato13.UseVisualStyleBackColor = True
        '
        'Check_dato11
        '
        Me.Check_dato11.AutoSize = True
        Me.Check_dato11.Location = New System.Drawing.Point(6, 19)
        Me.Check_dato11.Name = "Check_dato11"
        Me.Check_dato11.Size = New System.Drawing.Size(54, 17)
        Me.Check_dato11.TabIndex = 0
        Me.Check_dato11.Text = "Rio(s)"
        Me.Check_dato11.UseVisualStyleBackColor = True
        '
        'Check_dato12
        '
        Me.Check_dato12.AutoSize = True
        Me.Check_dato12.Location = New System.Drawing.Point(138, 113)
        Me.Check_dato12.Name = "Check_dato12"
        Me.Check_dato12.Size = New System.Drawing.Size(53, 17)
        Me.Check_dato12.TabIndex = 1
        Me.Check_dato12.Text = "Canal"
        Me.Check_dato12.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Check_dato1)
        Me.GroupBox4.Controls.Add(Me.Check_dato2)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(160, 70)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Zona Agricola"
        '
        'Check_dato1
        '
        Me.Check_dato1.AutoSize = True
        Me.Check_dato1.Location = New System.Drawing.Point(8, 19)
        Me.Check_dato1.Name = "Check_dato1"
        Me.Check_dato1.Size = New System.Drawing.Size(118, 17)
        Me.Check_dato1.TabIndex = 0
        Me.Check_dato1.Text = "Zona Agricola Total"
        Me.Check_dato1.UseVisualStyleBackColor = True
        '
        'Check_dato2
        '
        Me.Check_dato2.AutoSize = True
        Me.Check_dato2.Location = New System.Drawing.Point(8, 42)
        Me.Check_dato2.Name = "Check_dato2"
        Me.Check_dato2.Size = New System.Drawing.Size(125, 17)
        Me.Check_dato2.TabIndex = 1
        Me.Check_dato2.Text = "Zona Agricola Parcial"
        Me.Check_dato2.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.Check_dato9)
        Me.GroupBox7.Controls.Add(Me.Check_dato10)
        Me.GroupBox7.Location = New System.Drawing.Point(6, 83)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(160, 131)
        Me.GroupBox7.TabIndex = 5
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Recubrimiento Aerofotografico"
        '
        'Check_dato9
        '
        Me.Check_dato9.AutoSize = True
        Me.Check_dato9.Location = New System.Drawing.Point(2, 45)
        Me.Check_dato9.Name = "Check_dato9"
        Me.Check_dato9.Size = New System.Drawing.Size(138, 17)
        Me.Check_dato9.TabIndex = 2
        Me.Check_dato9.Text = "Sin Recubrimiento Total"
        Me.Check_dato9.UseVisualStyleBackColor = True
        '
        'Check_dato10
        '
        Me.Check_dato10.AutoSize = True
        Me.Check_dato10.Location = New System.Drawing.Point(2, 68)
        Me.Check_dato10.Name = "Check_dato10"
        Me.Check_dato10.Size = New System.Drawing.Size(150, 17)
        Me.Check_dato10.TabIndex = 3
        Me.Check_dato10.Text = "Con Recubrimiento Parcial"
        Me.Check_dato10.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Check_dato3)
        Me.GroupBox5.Controls.Add(Me.Check_dato4)
        Me.GroupBox5.Location = New System.Drawing.Point(172, 77)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(160, 63)
        Me.GroupBox5.TabIndex = 6
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Dominio Maritimo"
        '
        'Check_dato3
        '
        Me.Check_dato3.AutoSize = True
        Me.Check_dato3.Location = New System.Drawing.Point(8, 19)
        Me.Check_dato3.Name = "Check_dato3"
        Me.Check_dato3.Size = New System.Drawing.Size(133, 17)
        Me.Check_dato3.TabIndex = 0
        Me.Check_dato3.Text = "Dominio Maritimo Total"
        Me.Check_dato3.UseVisualStyleBackColor = True
        '
        'Check_dato4
        '
        Me.Check_dato4.AutoSize = True
        Me.Check_dato4.Location = New System.Drawing.Point(8, 42)
        Me.Check_dato4.Name = "Check_dato4"
        Me.Check_dato4.Size = New System.Drawing.Size(140, 17)
        Me.Check_dato4.TabIndex = 1
        Me.Check_dato4.Text = "Dominio Maritimo Parcial"
        Me.Check_dato4.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Label5)
        Me.TabPage3.Controls.Add(Me.Label3)
        Me.TabPage3.Controls.Add(Me.Label2)
        Me.TabPage3.Controls.Add(Me.Txtpaises)
        Me.TabPage3.Controls.Add(Me.Label4)
        Me.TabPage3.Controls.Add(Me.txt_reserva)
        Me.TabPage3.Controls.Add(Me.txt_urbana)
        Me.TabPage3.Controls.Add(Me.txtfrontera)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(567, 220)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Otros Datos"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 57)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 59
        Me.Label5.Text = "Pais(es) :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 163)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 13)
        Me.Label3.TabIndex = 58
        Me.Label3.Text = "Zona Urbana :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 113)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 13)
        Me.Label2.TabIndex = 57
        Me.Label2.Text = "Zona Reservada :"
        '
        'Txtpaises
        '
        Me.Txtpaises.Location = New System.Drawing.Point(196, 54)
        Me.Txtpaises.Multiline = True
        Me.Txtpaises.Name = "Txtpaises"
        Me.Txtpaises.Size = New System.Drawing.Size(353, 30)
        Me.Txtpaises.TabIndex = 56
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(192, 13)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "Linites Fronterizos (Fuenta IGN) :"
        '
        'txt_reserva
        '
        Me.txt_reserva.Location = New System.Drawing.Point(154, 150)
        Me.txt_reserva.Multiline = True
        Me.txt_reserva.Name = "txt_reserva"
        Me.txt_reserva.Size = New System.Drawing.Size(395, 54)
        Me.txt_reserva.TabIndex = 54
        '
        'txt_urbana
        '
        Me.txt_urbana.Location = New System.Drawing.Point(154, 90)
        Me.txt_urbana.Multiline = True
        Me.txt_urbana.Name = "txt_urbana"
        Me.txt_urbana.Size = New System.Drawing.Size(395, 54)
        Me.txt_urbana.TabIndex = 53
        '
        'txtfrontera
        '
        Me.txtfrontera.Location = New System.Drawing.Point(196, 15)
        Me.txtfrontera.Multiline = True
        Me.txtfrontera.Name = "txtfrontera"
        Me.txtfrontera.Size = New System.Drawing.Size(353, 30)
        Me.txtfrontera.TabIndex = 47
        '
        'txtvEste
        '
        Me.txtvEste.Location = New System.Drawing.Point(12, 496)
        Me.txtvEste.Name = "txtvEste"
        Me.txtvEste.Size = New System.Drawing.Size(10, 20)
        Me.txtvEste.TabIndex = 177
        '
        'txtvNorte
        '
        Me.txtvNorte.Location = New System.Drawing.Point(23, 496)
        Me.txtvNorte.Name = "txtvNorte"
        Me.txtvNorte.Size = New System.Drawing.Size(10, 20)
        Me.txtvNorte.TabIndex = 178
        '
        'txtNumVer
        '
        Me.txtNumVer.Location = New System.Drawing.Point(30, 496)
        Me.txtNumVer.Name = "txtNumVer"
        Me.txtNumVer.Size = New System.Drawing.Size(10, 20)
        Me.txtNumVer.TabIndex = 179
        '
        'txtArea
        '
        Me.txtArea.Location = New System.Drawing.Point(39, 496)
        Me.txtArea.Name = "txtArea"
        Me.txtArea.Size = New System.Drawing.Size(10, 20)
        Me.txtArea.TabIndex = 180
        '
        'Frm_datos_Evaluacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(597, 494)
        Me.Controls.Add(Me.txtArea)
        Me.Controls.Add(Me.txtNumVer)
        Me.Controls.Add(Me.txtvNorte)
        Me.Controls.Add(Me.txtvEste)
        Me.Controls.Add(Me.gbxDatosevaluacion)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximumSize = New System.Drawing.Size(605, 528)
        Me.MinimumSize = New System.Drawing.Size(605, 528)
        Me.Name = "Frm_datos_Evaluacion"
        Me.Text = "Datos de Evaluación"
        CType(Me.dgdDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.gbxDatosevaluacion.ResumeLayout(False)
        Me.Tabdatos.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox12.ResumeLayout(False)
        Me.GroupBox12.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgdDetalle As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents chkEstado As System.Windows.Forms.CheckBox
    Friend WithEvents btnGrabar As System.Windows.Forms.Button
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnActualizar As System.Windows.Forms.Button
    Friend WithEvents imgMenu As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNombre As System.Windows.Forms.TextBox
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbl_codigo As System.Windows.Forms.Label
    Friend WithEvents gbxDatosevaluacion As System.Windows.Forms.GroupBox
    Friend WithEvents Tabdatos As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents txtfrontera As System.Windows.Forms.TextBox
    Friend WithEvents txt_reserva As System.Windows.Forms.TextBox
    Friend WithEvents txt_urbana As System.Windows.Forms.TextBox
    Friend WithEvents Txtpaises As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Check_dato1 As System.Windows.Forms.CheckBox
    Friend WithEvents Check_dato2 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Check_dato3 As System.Windows.Forms.CheckBox
    Friend WithEvents Check_dato4 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_dato20 As System.Windows.Forms.TextBox
    Friend WithEvents Check_dato20 As System.Windows.Forms.CheckBox
    Friend WithEvents Check_dato19 As System.Windows.Forms.CheckBox
    Friend WithEvents Check_dato18 As System.Windows.Forms.CheckBox
    Friend WithEvents Check_dato17 As System.Windows.Forms.CheckBox
    Friend WithEvents Check_dato16 As System.Windows.Forms.CheckBox
    Friend WithEvents Check_dato15 As System.Windows.Forms.CheckBox
    Friend WithEvents Check_dato14 As System.Windows.Forms.CheckBox
    Friend WithEvents Check_dato13 As System.Windows.Forms.CheckBox
    Friend WithEvents Check_dato11 As System.Windows.Forms.CheckBox
    Friend WithEvents Check_dato12 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents Check_dato7 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents Check_dato5 As System.Windows.Forms.CheckBox
    Friend WithEvents Check_dato6 As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Check_dato9 As System.Windows.Forms.CheckBox
    Friend WithEvents Check_dato10 As System.Windows.Forms.CheckBox
    Friend WithEvents txtvEste As System.Windows.Forms.TextBox
    Friend WithEvents txtvNorte As System.Windows.Forms.TextBox
    Friend WithEvents txtNumVer As System.Windows.Forms.TextBox
    Friend WithEvents txtArea As System.Windows.Forms.TextBox
    Friend WithEvents Txtlaguna As System.Windows.Forms.TextBox
    Friend WithEvents Txtrio As System.Windows.Forms.TextBox
    Friend WithEvents cboInforme As System.Windows.Forms.ComboBox
    Friend WithEvents Check_dato8 As System.Windows.Forms.CheckBox
    Friend WithEvents lblRInforme As System.Windows.Forms.Label
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents lblRHistorico As System.Windows.Forms.Label
End Class
